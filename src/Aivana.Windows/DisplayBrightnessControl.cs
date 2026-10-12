using System.Runtime.InteropServices;
using Aivana.Core;

namespace Aivana.Windows;

internal static class DisplayBrightnessControl
{
    private sealed record Target(string Id, string Name, uint Minimum, uint Current, uint Maximum, Func<uint> Read, Action<uint> Write);
    internal static (uint Before, uint Changed, uint Restored)? LastRoundTrip { get; private set; }

    internal static int Apply(BrightnessCommand command, CancellationToken token, bool probeOnly = false, bool roundTrip = false, string? deviceId = null, List<DisplayBrightnessInfo>? displays = null)
    {
        var targets = new List<Target>();
        var handles = new List<IntPtr>();
        var com = new List<object>();
        try
        {
            token.ThrowIfCancellationRequested();
            ReadInternalTargets(targets, com, deviceId);
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (monitor, _, _, _) =>
            {
                if (token.IsCancellationRequested) return false;
                if (!GetNumberOfPhysicalMonitorsFromHMONITOR(monitor, out var count) || count is 0 or > 128) return true;
                var physical = new PhysicalMonitor[count];
                if (!GetPhysicalMonitorsFromHMONITOR(monitor, count, physical)) return true;
                var monitorInfo = new MonitorInfo { Size = Marshal.SizeOf<MonitorInfo>() };
                GetMonitorInfo(monitor, ref monitorInfo);
                var index = 0;
                foreach (var display in physical)
                {
                    var handle = display.Handle;
                    var device = new DisplayDevice { Size = Marshal.SizeOf<DisplayDevice>() };
                    var hasDevice = EnumDisplayDevices(monitorInfo.Device, (uint)index, ref device, 1);
                    var id = "ddc:" + (hasDevice && !string.IsNullOrEmpty(device.DeviceId) ? device.DeviceId : monitorInfo.Device) + ":" + index++;
                    if (!string.IsNullOrEmpty(deviceId) && !string.Equals(deviceId, id, StringComparison.OrdinalIgnoreCase)) { handles.Add(handle); continue; }
                    handles.Add(handle);
                    if (ReadBrightness(handle, token, out var min, out var current, out var max) && max > min && current >= min && current <= max)
                        targets.Add(new Target(id, display.Description, min, current, max, () =>
                        {
                            if (!GetMonitorBrightness(handle, out _, out var value, out _)) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                            return value;
                        }, value =>
                        {
                            if (!SetMonitorBrightness(handle, value)) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                        }));
                }
                return true;
            }, IntPtr.Zero);
            token.ThrowIfCancellationRequested();
            if (displays != null) displays.AddRange(targets.Select(t => new DisplayBrightnessInfo(t.Id, t.Name, (int)Math.Round((t.Current - (double)t.Minimum) * 100 / (t.Maximum - t.Minimum)))));
            if (probeOnly) return targets.Count;
            if (targets.Count == 0) throw new InvalidOperationException(Aivana.Core.Localization.Translate("Kein Display unterstützt die Helligkeitssteuerung. Bei externen Monitoren DDC/CI aktivieren."));
            if (roundTrip)
            {
                LastRoundTrip = null;
                var target = targets[0];
                var before = target.Read();
                var requested = new BrightnessCommand(null, before == target.Maximum ? -10 : 10).Apply(target.Minimum, before, target.Maximum);
                uint changed = before;
                try
                {
                    target.Write(requested);
                    for (var i = 0; i < 10 && changed == before; i++) { Thread.Sleep(100); changed = target.Read(); }
                    if (changed == before) throw new InvalidOperationException("Brightness did not change during the hardware test.");
                }
                finally
                {
                    target.Write(before);
                    var restored = target.Read();
                    for (var i = 0; i < 10 && restored != before; i++) { Thread.Sleep(100); restored = target.Read(); }
                    if (restored != before) throw new InvalidOperationException("Brightness restoration could not be verified.");
                    LastRoundTrip = (before, changed, restored);
                }
                return 1;
            }
            var failures = 0;
            // Snapshot every current value before writing, avoiding double relative changes when APIs overlap.
            foreach (var target in targets)
            {
                token.ThrowIfCancellationRequested();
                try { target.Write(command.Apply(target.Minimum, target.Current, target.Maximum)); }
                catch (Exception ex) when (ex is COMException or System.ComponentModel.Win32Exception or InvalidOperationException) { failures++; }
            }
            if (failures > 0) throw new InvalidOperationException(Aivana.Core.Localization.Format("Die Helligkeit konnte bei {0} von {1} Display-Anbindungen nicht geändert werden.", failures, targets.Count));
            return targets.Count;
        }
        finally
        {
            foreach (var handle in handles) DestroyPhysicalMonitor(handle);
            foreach (var item in com.AsEnumerable().Reverse().Distinct(ReferenceEqualityComparer.Instance)) if (Marshal.IsComObject(item)) Marshal.FinalReleaseComObject(item);
        }
    }

    private static bool ReadBrightness(IntPtr handle, CancellationToken token, out uint minimum, out uint current, out uint maximum)
    {
        minimum=current=maximum=0;
        for(var attempt=0;attempt<3;attempt++)
        {
            if(token.IsCancellationRequested) return false;
            if(GetMonitorBrightness(handle,out minimum,out current,out maximum)) return true;
            if(attempt<2) token.WaitHandle.WaitOne(75);
        }
        return false;
    }

    private static void ReadInternalTargets(List<Target> targets, List<object> resources, string? deviceId)
    {
        object Keep(object value) { resources.Add(value); return value; }
        try
        {
            var type = Type.GetTypeFromProgID("WbemScripting.SWbemLocator");
            if (type is null) return;
            dynamic locator = Keep(Activator.CreateInstance(type)!);
            dynamic service = Keep(locator.ConnectServer(".", @"root\wmi"));
            dynamic readings = Keep(service.ExecQuery("SELECT * FROM WmiMonitorBrightness WHERE Active = TRUE"));
            var levels = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase);
            foreach (dynamic item in readings)
            {
                Keep(item);
                levels[(string)item.InstanceName] = Convert.ToUInt32(item.CurrentBrightness);
            }
            dynamic methods = Keep(service.ExecQuery("SELECT * FROM WmiMonitorBrightnessMethods WHERE Active = TRUE"));
            foreach (dynamic method in methods)
            {
                Keep(method);
                if (!levels.TryGetValue((string)method.InstanceName, out var current) || current > 100) continue;
                object captured = method;
                var instance = (string)method.InstanceName;
                var id = "wmi:" + instance;
                if (!string.IsNullOrEmpty(deviceId) && !string.Equals(deviceId, id, StringComparison.OrdinalIgnoreCase)) continue;
                targets.Add(new Target(id, instance, 0, current, 100, () => ReadInternalCurrent(service, instance, resources), value =>
                {
                    dynamic writer = captured;
                    if (Convert.ToUInt32(writer.WmiSetBrightness(0, (byte)value)) != 0)
                        throw new InvalidOperationException("WmiSetBrightness failed.");
                }));
            }
        }
        catch (Exception ex) when (ex is COMException or InvalidCastException or Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
        { /* Many desktop PCs have no WMI brightness provider; physical monitor APIs remain available. */ }
    }

    private static uint ReadInternalCurrent(dynamic service, string instance, List<object> resources)
    {
        object query = service.ExecQuery("SELECT * FROM WmiMonitorBrightness WHERE Active = TRUE");
        resources.Add(query);
        foreach (dynamic item in (dynamic)query)
        {
            resources.Add(item);
            if (string.Equals((string)item.InstanceName, instance, StringComparison.OrdinalIgnoreCase)) return Convert.ToUInt32(item.CurrentBrightness);
        }
        throw new InvalidOperationException("Display is no longer available.");
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct MonitorInfo
    {
        public int Size;
        public int Left, Top, Right, Bottom, WorkLeft, WorkTop, WorkRight, WorkBottom;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string Device;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct DisplayDevice
    {
        public int Size;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceString;
        public uint StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceKey;
    }
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern bool GetMonitorInfo(IntPtr monitor, ref MonitorInfo info);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern bool EnumDisplayDevices(string device, uint number, ref DisplayDevice display, uint flags);
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct PhysicalMonitor
    {
        public IntPtr Handle;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string Description;
    }
    private delegate bool MonitorCallback(IntPtr monitor, IntPtr dc, IntPtr rectangle, IntPtr data);
    [DllImport("user32.dll")] private static extern bool EnumDisplayMonitors(IntPtr dc, IntPtr rectangle, MonitorCallback callback, IntPtr data);
    [DllImport("dxva2.dll", SetLastError = true)] private static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr monitor, out uint count);
    [DllImport("dxva2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)] private static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr monitor, uint count, [Out] PhysicalMonitor[] monitors);
    [DllImport("dxva2.dll", SetLastError = true)] private static extern bool GetMonitorBrightness(IntPtr monitor, out uint minimum, out uint current, out uint maximum);
    [DllImport("dxva2.dll", SetLastError = true)] private static extern bool SetMonitorBrightness(IntPtr monitor, uint value);
    [DllImport("dxva2.dll")] private static extern bool DestroyPhysicalMonitor(IntPtr monitor);
}

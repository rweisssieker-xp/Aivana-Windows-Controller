using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Aivana.Core;
using Localization = Aivana.Core.Localization;

namespace Aivana.Windows;

public sealed class DeviceService
{
    private const uint RidInput = 0x10000003;
    private const uint RidiDeviceInfo = 0x2000000B;
    private const uint RidiDeviceName = 0x20000007;
    private const uint RimTypeHid = 2;
    private const uint RimTypeKeyboard = 1;

    public IReadOnlyList<DeviceInfo> GetDevices() => GetDevices(null);

    /// <summary>Includes iCUE model names when its inventory identifies one unambiguous Corsair keyboard.</summary>
    public IReadOnlyList<DeviceInfo> GetDevices(IReadOnlyList<CorsairKeyboardInfo>? corsairKeyboards)
    {
        uint count = 0;
        var itemSize = (uint)Marshal.SizeOf<RawInputDeviceList>();
        if (GetRawInputDeviceList(IntPtr.Zero, ref count, itemSize) == uint.MaxValue || count == 0) return [];
        var buffer = Marshal.AllocHGlobal(checked((int)(count * itemSize)));
        try
        {
            if (GetRawInputDeviceList(buffer, ref count, itemSize) == uint.MaxValue) return [];
            var result = new List<DeviceInfo>();
            var corsairModels = corsairKeyboards?.Where(k => !string.IsNullOrWhiteSpace(k.Name)).ToArray() ?? [];
            for (var i = 0; i < count; i++)
            {
                var entry = Marshal.PtrToStructure<RawInputDeviceList>(IntPtr.Add(buffer, checked((int)(i * itemSize))));
                if (!TryGetDeviceName(entry.Device, out var path) || string.IsNullOrWhiteSpace(path)) continue;
                var info = new RawInputDeviceInfo { Size = (uint)Marshal.SizeOf<RawInputDeviceInfo>(), Type = 0 };
                var infoSize = info.Size;
                var isHid = GetRawInputDeviceInfo(entry.Device, RidiDeviceInfo, ref info, ref infoSize) != uint.MaxValue && info.Type == RimTypeHid;
                var isKeyboard = info.Type == RimTypeKeyboard || (isHid && info.Hid.UsagePage == 1 && info.Hid.Usage == 6);
                if (!isKeyboard) continue;
                var (vid, pid, manufacturer, friendlyName) = GetIdentity(path, isHid ? info.Hid.VendorId : null, isHid ? info.Hid.ProductId : null);
                if (vid == "1B1C" && corsairModels.Length == 1) friendlyName = corsairModels[0].Name;
                var capabilities = info.Type == RimTypeKeyboard
                    ? Localization.Translate("Tastatur (Windows Keyboard Input)")
                    : Localization.Translate("Tastatur (HID)");
                var name = string.IsNullOrWhiteSpace(friendlyName) ? manufacturer : friendlyName;
                result.Add(new DeviceInfo($"{name} ({vid}:{pid})".Trim(), path, vid, pid, manufacturer, capabilities));
            }
            return result;
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }

    public static string GetForegroundProcessName()
    {
        var hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return "";
        GetWindowThreadProcessId(hwnd, out var processId);
        if (processId == 0) return "";
        try { using var process = Process.GetProcessById((int)processId); return process.ProcessName; }
        catch (ArgumentException) { return ""; }
        catch (InvalidOperationException) { return ""; }
        catch (System.ComponentModel.Win32Exception) { return ""; }
    }

    /// <summary>Returns the visible title of the current foreground window, bounded to avoid oversized native results.</summary>
    public static string GetForegroundWindowTitle()
    {
        var hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return "";
        var length = Math.Clamp(GetWindowTextLength(hwnd), 0, 512);
        if (length == 0) return "";
        var title = new StringBuilder(length + 1);
        var copied = GetWindowText(hwnd, title, title.Capacity);
        return copied <= 0 ? "" : title.ToString(0, Math.Min(copied, 512));
    }

    private static bool TryGetDeviceName(IntPtr device, out string name)
    {
        uint chars = 0;
        name = "";
        if (GetRawInputDeviceInfo(device, RidiDeviceName, IntPtr.Zero, ref chars) == uint.MaxValue || chars == 0) return false;
        var memory = Marshal.AllocHGlobal(checked((int)chars * 2));
        try
        {
            var actual = chars;
            if (GetRawInputDeviceInfo(device, RidiDeviceName, memory, ref actual) == uint.MaxValue) return false;
            name = Marshal.PtrToStringUni(memory) ?? "";
            return name.Length > 0;
        }
        finally { Marshal.FreeHGlobal(memory); }
    }

    private static (string Vid, string Pid, string Manufacturer, string FriendlyName) GetIdentity(string path, uint? rawVid, uint? rawPid)
    {
        var pieces = path.TrimStart('\\').Split('#');
        var enumId = pieces.Length > 2 ? pieces[1] : "";
        var instanceId = pieces.Length > 2 ? pieces[2] : "";
        var match = Regex.Match(enumId, @"VID_(?<vid>[0-9A-F]{4})&PID_(?<pid>[0-9A-F]{4})", RegexOptions.IgnoreCase);
        var vid = match.Success ? match.Groups["vid"].Value.ToUpperInvariant() : rawVid?.ToString("X4") ?? "";
        var pid = match.Success ? match.Groups["pid"].Value.ToUpperInvariant() : rawPid?.ToString("X4") ?? "";
        var manufacturer = "";
        var friendlyName = "";
        if (enumId.Length > 0 && instanceId.Length > 0)
        {
            var deviceKey = $@"SYSTEM\CurrentControlSet\Enum\HID\{enumId}\{instanceId}";
            try
            {
                using var key = Registry.LocalMachine.OpenSubKey(deviceKey);
                manufacturer = (key?.GetValue("Manufacturer") as string) ?? (key?.GetValue("Mfg") as string) ?? "";
                friendlyName = (key?.GetValue("FriendlyName") as string) ?? "";
                if (friendlyName.Length == 0)
                {
                    var description = key?.GetValue("DeviceDesc") as string ?? "";
                    if (!description.StartsWith('@')) friendlyName = description;
                }
            }
            catch (System.Security.SecurityException) { }
            catch (UnauthorizedAccessException) { }
            catch (System.IO.IOException) { }
        }
        if (string.IsNullOrWhiteSpace(manufacturer)) manufacturer = vid.ToUpperInvariant() switch
        {
            "1B1C" => "Corsair", "046D" => "Logitech", "045E" => "Microsoft", "04D9" => "Holtek",
            "0C45" => "Microdia", "04F2" => "Chicony", "1532" => "Razer", "17EF" => "Lenovo", _ => "Tastatur"
        };
        if (manufacturer.StartsWith('@') && manufacturer.Contains(';')) manufacturer = manufacturer[(manufacturer.LastIndexOf(';') + 1)..];
        return (vid, pid, manufacturer.Trim(), friendlyName.Trim());
    }

    [StructLayout(LayoutKind.Sequential)] private struct RawInputDeviceList { public IntPtr Device; public uint Type; }
    [StructLayout(LayoutKind.Explicit, Size = 32)] private struct RawInputDeviceInfo
    {
        [FieldOffset(0)] public uint Size;
        [FieldOffset(4)] public uint Type;
        [FieldOffset(8)] public HidInfo Hid;
    }
    [StructLayout(LayoutKind.Sequential)] private struct HidInfo { public uint VendorId, ProductId, VersionNumber; public ushort UsagePage, Usage; }

    [DllImport("user32.dll", SetLastError = true)] private static extern uint GetRawInputDeviceList(IntPtr list, ref uint count, uint size);
    [DllImport("user32.dll", EntryPoint = "GetRawInputDeviceInfoW", SetLastError = true, CharSet = CharSet.Unicode)] private static extern uint GetRawInputDeviceInfo(IntPtr device, uint command, IntPtr data, ref uint size);
    [DllImport("user32.dll", EntryPoint = "GetRawInputDeviceInfoW", SetLastError = true, CharSet = CharSet.Unicode)] private static extern uint GetRawInputDeviceInfo(IntPtr device, uint command, ref RawInputDeviceInfo data, ref uint size);
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll", SetLastError = true)] private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
    [DllImport("user32.dll", EntryPoint = "GetWindowTextLengthW", CharSet = CharSet.Unicode, SetLastError = true)] private static extern int GetWindowTextLength(IntPtr hwnd);
    [DllImport("user32.dll", EntryPoint = "GetWindowTextW", CharSet = CharSet.Unicode, SetLastError = true)] private static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int maxCount);
}

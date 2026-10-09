using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Localization = Aivana.Core.Localization;

namespace Aivana.Windows;

public sealed record CorsairKeyboardInfo(string Name, string DeviceId, int LedCount);

/// <summary>Optional adapter for the official Corsair iCUE SDK. No SDK or iCUE is required for the app.</summary>
public sealed partial class CorsairService : IDisposable
{
    private const int DeviceCountMax = 64;
    private const int DeviceLedCountMax = 512;
    private const uint LoadWithAlteredSearchPath = 0x00000008;
    private readonly object _sync = new();
    private IntPtr _library;
    private bool _connected;
    private bool _connectStarted;
    private bool _disposed;
    private readonly SessionStateChanged? _stateHandler;
    private readonly Connect? _connect;
    private readonly Disconnect? _disconnect;
    private readonly GetDevices? _getDevices;
    private readonly GetLedPositions? _getLedPositions;
    private readonly SetLedColors? _setLedColors;
    private string? _sdkPath;
    private string _status = Localization.Translate("iCUE SDK wird gesucht …");
    private CorsairKeyboardInfo[] _keyboards = [];
    private Dictionary<string, uint[]> _controllableKeyboards = new(StringComparer.Ordinal);

    public string Status { get { lock (_sync) return _status; } }
    public string? SdkPath { get { lock (_sync) return _sdkPath; } }
    public IReadOnlyList<CorsairKeyboardInfo> Keyboards { get { lock (_sync) return Array.AsReadOnly((CorsairKeyboardInfo[])_keyboards.Clone()); } }
    public bool IsAvailable { get { lock (_sync) return _library != IntPtr.Zero && _connected && !_disposed && _controllableKeyboards.Count > 0; } }

    public CorsairService()
    {
        var path = FindSdk();
        if (path is null) { _status = Localization.Translate("Das iCUE SDK ist nicht installiert; Corsair-Beleuchtung ist nicht verfügbar."); return; }
        _sdkPath = path;
        _library = LoadLibraryEx(path, IntPtr.Zero, LoadWithAlteredSearchPath);
        if (_library == IntPtr.Zero) { _status = Localization.Translate("Die iCUE-SDK-DLL wurde gefunden, konnte aber nicht geladen werden."); return; }
        try
        {
            _stateHandler = OnSessionStateChanged;
            _connect = Export<Connect>("CorsairConnect");
            _disconnect = Export<Disconnect>("CorsairDisconnect");
            _getDevices = Export<GetDevices>("CorsairGetDevices");
            _getLedPositions = Export<GetLedPositions>("CorsairGetLedPositions");
            _setLedColors = Export<SetLedColors>("CorsairSetLedColors");
            LoadMacroKeySdkExports();
            _status = Localization.Translate("Verbindung mit iCUE wird aufgebaut …");
            _connectStarted = true;
            var result = _connect(_stateHandler, IntPtr.Zero);
            if (result != 0) _status = Localization.Translate((FormattableString)$"Die Verbindung mit iCUE wurde abgelehnt (SDK-Fehler {result}).");
        }
        catch (Exception ex) when (ex is EntryPointNotFoundException or BadImageFormatException or MarshalDirectiveException)
        {
            _status = Localization.Translate("Die iCUE-SDK-DLL ist inkompatibel oder erforderliche Funktionen fehlen.");
            FreeLibrary(_library); _library = IntPtr.Zero;
        }
    }

    public void Refresh()
    {
        lock (_sync)
        {
            if (_library == IntPtr.Zero || !_connected || _disposed) return;
            try
            {
                RefreshDevices();
                UpdateConnectedStatus();
                try
                {
                    RefreshMacroKeyCapabilities();
                    ApplyMacroKeyBindingsSafely();
                }
                catch (Exception keyCapabilityError) when (keyCapabilityError is not OutOfMemoryException)
                {
                    _macroKeyStatus = keyCapabilityError.Message;
                }
            }
            catch (Exception ex) when (ex is not OutOfMemoryException)
            {
                ClearDevices();
                _status = Localization.Translate("Das iCUE-Inventar konnte nicht gelesen werden.");
            }
        }
    }

    public void SetColor(string hex) => SetColor(hex, null);

    public void SetColor(string hex, string? deviceId)
    {
        var match = Regex.Match(hex ?? "", @"^#?(?<rgb>[0-9a-fA-F]{6})(?<alpha>[0-9a-fA-F]{2})?$");
        if (!match.Success) throw new ArgumentException(Localization.Translate("Die Farbe muss als RGB-Hexwert angegeben werden, zum Beispiel #25C4B6."), nameof(hex));
        lock (_sync)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (_library == IntPtr.Zero || !_connected) throw new InvalidOperationException(_status);
            RefreshDevices();
            if (_controllableKeyboards.Count == 0) throw new InvalidOperationException(_status);

            string targetId;
            if (string.IsNullOrWhiteSpace(deviceId)) targetId = _controllableKeyboards.Keys.First();
            else if (!_controllableKeyboards.ContainsKey(deviceId)) throw new InvalidOperationException(Localization.Translate("Die ausgewählte Corsair-Tastatur ist nicht mehr verbunden oder unterstützt keine LED-Steuerung."));
            else targetId = deviceId;

            var ledIds = _controllableKeyboards[targetId];
            var rgb = Convert.ToUInt32(match.Groups["rgb"].Value, 16);
            var colors = new LedColor[ledIds.Length];
            for (var i = 0; i < colors.Length; i++) colors[i] = new LedColor { Id = ledIds[i], R = (byte)(rgb >> 16), G = (byte)(rgb >> 8), B = (byte)rgb, A = 255 };
            var result = _setLedColors!(targetId, colors.Length, colors);
            if (result != 0) throw new InvalidOperationException(Localization.Translate((FormattableString)$"iCUE hat die Beleuchtungsanforderung abgelehnt (SDK-Fehler {result})."));
            _status = Localization.Translate("Corsair-Tastaturbeleuchtung wurde aktualisiert.");
        }
    }

    public void Dispose()
    {
        IntPtr library;
        Disconnect? disconnect;
        lock (_sync)
        {
            if (_disposed) return;
            _disposed = true;
            ReleaseConfiguredMacroKeyBindings();
            library = _library;
            disconnect = _connectStarted ? _disconnect : null;
            _library = IntPtr.Zero;
            _connected = false;
            ClearDevices();
            if (library == IntPtr.Zero) return;
        }
        try { disconnect?.Invoke(); } catch { }
        FreeLibrary(library);
    }

    private void OnSessionStateChanged(IntPtr context, IntPtr eventData)
    {
        if (eventData == IntPtr.Zero) return;
        try
        {
            lock (_sync)
            {
                if (_library == IntPtr.Zero || _disposed) return;
                var state = Marshal.PtrToStructure<SessionState>(eventData).State;
                _connected = state == 6;
                if (_connected)
                {
                    ClearDevices();
                    _status = Localization.Translate("Mit iCUE verbunden; Geräteinventar wird aktualisiert.");
                }
                else
                {
                    ResetMacroKeySessionState();
                    ClearDevices();
                    _status = state switch
                    {
                        1 => Localization.Translate("iCUE-SDK-Verbindung geschlossen."),
                        2 => Localization.Translate("Verbindung mit iCUE wird aufgebaut …"),
                        3 => Localization.Translate("iCUE antwortet nicht; der SDK-Verbindungsversuch läuft weiter."),
                        4 => Localization.Translate("iCUE hat die SDK-Verbindung abgelehnt."),
                        5 => Localization.Translate("Die Verbindung zu iCUE wurde getrennt; der SDK-Verbindungsversuch läuft weiter."),
                        _ => Localization.Translate("iCUE meldet einen unbekannten Verbindungsstatus.")
                    };
                }
            }
        }
        catch { lock (_sync) _status = Localization.Translate("Der iCUE-Status konnte nicht aktualisiert werden."); }
    }

    private void RefreshDevices()
    {
        var keyboards = new List<CorsairKeyboardInfo>();
        var controllable = new Dictionary<string, uint[]>(StringComparer.Ordinal);
        var filter = new DeviceFilter { DeviceTypeMask = 1 };
        var size = Marshal.SizeOf<DeviceInfoNative>();
        var devicesMemory = Marshal.AllocHGlobal(checked(size * DeviceCountMax));
        try
        {
            var count = 0;
            var error = _getDevices!(ref filter, DeviceCountMax, devicesMemory, ref count);
            if (error != 0) throw new InvalidOperationException(Localization.Translate((FormattableString)$"CorsairGetDevices meldete SDK-Fehler {error}."));
            count = Math.Clamp(count, 0, DeviceCountMax);
            for (var i = 0; i < count; i++)
            {
                var device = Marshal.PtrToStructure<DeviceInfoNative>(IntPtr.Add(devicesMemory, checked(i * size)));
                if (device.Type != 1) continue;
                var id = GetNullTerminatedAscii(device.Id);
                if (string.IsNullOrWhiteSpace(id)) continue;
                var model = GetNullTerminatedAscii(device.Model).Trim();
                if (model.Length == 0) model = Localization.Translate("Corsair-Tastatur");
                var ledIds = device.LedCount > 0 ? GetLedIds(id) : [];
                keyboards.Add(new CorsairKeyboardInfo(model, id, Math.Max(0, device.LedCount)));
                if (ledIds.Length > 0) controllable[id] = ledIds;
            }
        }
        finally { Marshal.FreeHGlobal(devicesMemory); }

        _keyboards = keyboards.ToArray();
        _controllableKeyboards = controllable;
    }

    private uint[] GetLedIds(string id)
    {
        var positionsMemory = Marshal.AllocHGlobal(checked(Marshal.SizeOf<LedPosition>() * DeviceLedCountMax));
        try
        {
            var ledCount = 0;
            var error = _getLedPositions!(id, DeviceLedCountMax, positionsMemory, ref ledCount);
            if (error != 0 || ledCount <= 0) return [];
            ledCount = Math.Min(ledCount, DeviceLedCountMax);
            var ids = new uint[ledCount];
            var ledSize = Marshal.SizeOf<LedPosition>();
            for (var i = 0; i < ledCount; i++) ids[i] = Marshal.PtrToStructure<LedPosition>(IntPtr.Add(positionsMemory, checked(i * ledSize))).Id;
            return ids;
        }
        finally { Marshal.FreeHGlobal(positionsMemory); }
    }

    private void UpdateConnectedStatus() => _status = _controllableKeyboards.Count > 0
        ? Localization.Translate((FormattableString)$"Mit iCUE verbunden; {_controllableKeyboards.Count} Corsair-Tastatur(en) für Beleuchtung verfügbar.")
        : _keyboards.Length > 0
            ? Localization.Translate("iCUE erkennt Corsair-Tastaturen, meldet aber keine steuerbaren LEDs.")
            : Localization.Translate("iCUE ist verbunden, meldet aber keine Corsair-Tastatur.");

    private void ClearDevices()
    {
        _keyboards = [];
        _controllableKeyboards = new Dictionary<string, uint[]>(StringComparer.Ordinal);
    }

    private T Export<T>(string name) where T : Delegate
    {
        var address = GetProcAddress(_library, name);
        if (address == IntPtr.Zero) throw new EntryPointNotFoundException($"iCUE SDK export '{name}' was not found.");
        return Marshal.GetDelegateForFunctionPointer<T>(address);
    }

    private static string GetNullTerminatedAscii(byte[] bytes)
    {
        var length = Array.IndexOf(bytes, (byte)0);
        return Encoding.ASCII.GetString(bytes, 0, length < 0 ? bytes.Length : length);
    }

    private static string? FindSdk()
    {
        var roots = new[] { AppContext.BaseDirectory, Environment.CurrentDirectory, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) };
        var names = new[] { "iCUESDK.x64_2019.dll", "iCUESDK.x64.dll" };
        foreach (var root in roots.Where(Directory.Exists))
        {
            foreach (var name in names)
            {
                var adjacent = Path.Combine(root, name);
                if (File.Exists(adjacent)) return adjacent;
            }
            foreach (var relative in new[] { "artifacts\\win-x64", "Corsair\\CUE SDK", "Corsair\\CORSAIR iCUE5 Software\\iCUE SDK", "Corsair\\CORSAIR iCUE Software\\iCUE SDK", "Corsair\\iCUE SDK" })
                foreach (var name in names)
                {
                    var candidate = Path.Combine(root, relative, name);
                    if (File.Exists(candidate)) return candidate;
                }
        }
        return null;
    }

    [StructLayout(LayoutKind.Sequential)] private struct DeviceFilter { public int DeviceTypeMask; }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] private struct DeviceInfoNative
    {
        public int Type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] public byte[] Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] public byte[] Serial;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] public byte[] Model;
        public int LedCount, ChannelCount;
    }
    [StructLayout(LayoutKind.Sequential)] private struct LedPosition { public uint Id; public double Cx, Cy; }
    [StructLayout(LayoutKind.Sequential)] private struct LedColor { public uint Id; public byte R, G, B, A; }
    [StructLayout(LayoutKind.Sequential)] private struct SessionState { public int State; }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void SessionStateChanged(IntPtr context, IntPtr eventData);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int Connect(SessionStateChanged callback, IntPtr context);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int Disconnect();
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int GetDevices(ref DeviceFilter filter, int maxSize, IntPtr devices, ref int size);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)] private delegate int GetLedPositions([MarshalAs(UnmanagedType.LPStr)] string deviceId, int maxSize, IntPtr positions, ref int size);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)] private delegate int SetLedColors([MarshalAs(UnmanagedType.LPStr)] string deviceId, int size, [In] LedColor[] colors);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern IntPtr LoadLibraryEx(string path, IntPtr file, uint flags);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)] private static extern IntPtr GetProcAddress(IntPtr module, string name);
    [DllImport("kernel32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool FreeLibrary(IntPtr module);
}

using System.Runtime.InteropServices;
using Aivana.Core;

namespace Aivana.Windows;

public sealed record CorsairMacroKeyBinding(string DeviceId, int KeyId);
public sealed record CorsairMacroKeyPress(string DeviceId, int KeyId);

public sealed partial class CorsairService
{
    private const int MacroKeyArrayProperty = 8;
    private const int Int32ArrayDataType = 17;
    private const int KeyEventId = 2;
    private const int ExclusiveKeyEventsListening = 2;
    private readonly Dictionary<string, int[]> _supportedMacroKeys = new(StringComparer.Ordinal);
    private HashSet<CorsairMacroKeyBinding> _requestedMacroKeyBindings = [];
    private HashSet<CorsairMacroKeyBinding> _configuredMacroKeyBindings = [];
    private readonly HashSet<string> _claimedMacroKeyDevices = new(StringComparer.Ordinal);
    private readonly HashSet<CorsairMacroKeyBinding> _currentlyPressedMacroKeys = [];
    private EventHandler? _macroKeyEventHandler;
    private SubscribeForEvents? _subscribeForEvents;
    private UnsubscribeFromEvents? _unsubscribeFromEvents;
    private RequestControl? _requestControl;
    private ConfigureKeyEvent? _configureKeyEvent;
    private ReadDeviceProperty? _readDeviceProperty;
    private FreeProperty? _freeProperty;
    private bool _macroKeyExportsAvailable;
    private bool _macroEventsSubscribed;
    private string _macroKeyStatus = Localization.Translate("Corsair-G-Tasten sind nicht verfügbar.");

    /// <summary>Raised for a selected G-key press, after the iCUE SDK callback has returned.</summary>
    public event Action<CorsairMacroKeyPress>? MacroKeyPressed;

    /// <summary>Whether iCUE reports at least one supported G1-G6 key and the SDK can listen for key events.</summary>
    public bool KeysAvailable { get { lock (_sync) return !_disposed && _connected && _macroKeyExportsAvailable && _supportedMacroKeys.Values.Any(keys => keys.Any(IsSupportedMacroKeyId)); } }

    /// <summary>Localized status for Corsair G-key event support.</summary>
    public string KeyStatus { get { lock (_sync) return _macroKeyStatus; } }

    /// <summary>Gets the G-key IDs the SDK reports for a connected Corsair device.</summary>
    public IReadOnlyList<int> GetSupportedMacroKeyIds(string deviceId)
    {
        lock (_sync)
            return _supportedMacroKeys.TryGetValue(deviceId, out var keys)
                ? Array.AsReadOnly((int[])keys.Clone())
                : Array.Empty<int>();
    }

    public bool IsMacroKeyBindingConfigured(string deviceId, int keyId)
    {
        lock (_sync)
            return !_disposed && _configuredMacroKeyBindings.Contains(new CorsairMacroKeyBinding(deviceId, keyId));
    }

    /// <summary>Intercepts only the supplied per-device G-keys and emits presses for matching events.</summary>
    public void ConfigureMacroKeyBindings(IReadOnlyCollection<CorsairMacroKeyBinding> bindings)
    {
        ArgumentNullException.ThrowIfNull(bindings);
        var requested = new HashSet<CorsairMacroKeyBinding>();
        foreach (var binding in bindings)
        {
            if (binding is null || string.IsNullOrWhiteSpace(binding.DeviceId) || !IsSupportedMacroKeyId(binding.KeyId))
                throw new ArgumentException(Localization.Translate("Eine Corsair-Tastenzuordnung muss ein Gerät und eine G-Taste von G1 bis G6 enthalten."), nameof(bindings));
            if (!requested.Add(binding))
                throw new ArgumentException(Localization.Translate("Eine Corsair-G-Taste darf nur einmal zugeordnet werden."), nameof(bindings));
        }

        lock (_sync)
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            _requestedMacroKeyBindings = requested;
            if (_library == IntPtr.Zero || !_connected)
            {
                _configuredMacroKeyBindings.Clear();
                _macroKeyStatus = Localization.Translate("Corsair-G-Tasten warten auf eine iCUE-Verbindung.");
                return;
            }
            RefreshMacroKeyCapabilities();
            ApplyMacroKeyBindingsSafely();
        }
    }

    private void ApplyMacroKeyBindingsSafely()
    {
        var previousBindings = new HashSet<CorsairMacroKeyBinding>(_configuredMacroKeyBindings);
        var previousClaims = new HashSet<string>(_claimedMacroKeyDevices, StringComparer.Ordinal);
        try { ApplyConfiguredMacroKeyBindings(); }
        catch (Exception error) when (error is not OutOfMemoryException)
        {
            RollBackMacroKeyBindings(previousBindings, previousClaims);
            _macroKeyStatus = error.Message;
        }
    }

    private void RollBackMacroKeyBindings(HashSet<CorsairMacroKeyBinding> previousBindings, HashSet<string> previousClaims)
    {
        foreach (var binding in _requestedMacroKeyBindings.Except(previousBindings).ToArray())
        {
            try
            {
                var configuration = new NativeKeyEventConfiguration { KeyId = binding.KeyId, IsIntercepted = 0 };
                if (_configureKeyEvent!(binding.DeviceId, ref configuration) == 0)
                {
                    _configuredMacroKeyBindings.Remove(binding);
                    _currentlyPressedMacroKeys.Remove(binding);
                }
            }
            catch { }
        }
        foreach (var binding in previousBindings.Except(_configuredMacroKeyBindings).ToArray())
        {
            try
            {
                if (_requestControl!(binding.DeviceId, ExclusiveKeyEventsListening) != 0) continue;
                _claimedMacroKeyDevices.Add(binding.DeviceId);
                var configuration = new NativeKeyEventConfiguration { KeyId = binding.KeyId, IsIntercepted = 1 };
                if (_configureKeyEvent!(binding.DeviceId, ref configuration) == 0)
                    _configuredMacroKeyBindings.Add(binding);
            }
            catch { }
        }
        foreach (var deviceId in _claimedMacroKeyDevices.Except(previousClaims).ToArray())
        {
            try
            {
                if (_requestControl!(deviceId, 0) == 0) _claimedMacroKeyDevices.Remove(deviceId);
            }
            catch { }
        }
        foreach (var deviceId in previousClaims.Except(_claimedMacroKeyDevices))
        {
            try
            {
                if (_requestControl!(deviceId, ExclusiveKeyEventsListening) == 0)
                    _claimedMacroKeyDevices.Add(deviceId);
            }
            catch { }
        }
        if (previousBindings.Count == 0 && _macroEventsSubscribed)
        {
            try { _unsubscribeFromEvents!(); } catch { }
            _macroEventsSubscribed = false;
        }
    }

    private void LoadMacroKeySdkExports()
    {
        try
        {
            _macroKeyEventHandler = OnMacroKeyEvent;
            _subscribeForEvents = Export<SubscribeForEvents>("CorsairSubscribeForEvents");
            _unsubscribeFromEvents = Export<UnsubscribeFromEvents>("CorsairUnsubscribeFromEvents");
            _requestControl = Export<RequestControl>("CorsairRequestControl");
            _configureKeyEvent = Export<ConfigureKeyEvent>("CorsairConfigureKeyEvent");
            _readDeviceProperty = Export<ReadDeviceProperty>("CorsairReadDeviceProperty");
            _freeProperty = Export<FreeProperty>("CorsairFreeProperty");
            _macroKeyExportsAvailable = true;
            _macroKeyStatus = Localization.Translate("iCUE-G-Tastenereignisse sind verfügbar.");
        }
        catch (EntryPointNotFoundException)
        {
            _macroKeyExportsAvailable = false;
            _macroKeyStatus = Localization.Translate("Diese iCUE-SDK-Version unterstützt keine Corsair-G-Tastenereignisse.");
        }
    }

    private void RefreshMacroKeyCapabilities()
    {
        if (!_macroKeyExportsAvailable) return;
        var supported = new Dictionary<string, int[]>(StringComparer.Ordinal);
        foreach (var keyboard in _keyboards)
        {
            var keys = ReadMacroKeyIds(keyboard.DeviceId);
            if (keys.Length > 0) supported[keyboard.DeviceId] = keys;
        }
        _supportedMacroKeys.Clear();
        foreach (var pair in supported) _supportedMacroKeys.Add(pair.Key, pair.Value);

        if (!_connected)
            _macroKeyStatus = Localization.Translate("Corsair-G-Tasten warten auf eine iCUE-Verbindung.");
        else if (_supportedMacroKeys.Values.Any(keys => keys.Any(IsSupportedMacroKeyId)))
            _macroKeyStatus = Localization.Translate("iCUE meldet unterstützte Corsair-G-Tasten G1 bis G6.");
        else if (_keyboards.Length > 0)
            _macroKeyStatus = Localization.Translate("iCUE meldet für die verbundenen Tastaturen keine unterstützten G-Tasten G1 bis G6.");
        else
            _macroKeyStatus = Localization.Translate("iCUE meldet keine Corsair-Tastatur mit unterstützten G-Tasten.");
    }

    private int[] ReadMacroKeyIds(string deviceId)
    {
        var property = default(NativeProperty);
        try
        {
            if (_readDeviceProperty!(deviceId, MacroKeyArrayProperty, 0, ref property) != 0 || property.Type != Int32ArrayDataType)
                return [];
            var count = Math.Min(property.Value.Int32Array.Count, 32u);
            if (count == 0 || property.Value.Int32Array.Items == IntPtr.Zero) return [];
            var keys = new HashSet<int>();
            for (var index = 0; index < count; index++)
            {
                var keyId = Marshal.ReadInt32(property.Value.Int32Array.Items, checked((int)index * sizeof(int)));
                if (keyId > 0 && keyId <= 20) keys.Add(keyId);
            }
            return keys.Order().ToArray();
        }
        catch { return []; }
        finally
        {
            if (_freeProperty is not null)
            {
                try { _freeProperty(ref property); } catch { }
            }
        }
    }

    private void ApplyConfiguredMacroKeyBindings()
    {
        if (!_macroKeyExportsAvailable || !_connected) return;
        var desired = new HashSet<CorsairMacroKeyBinding>();
        var hasUnavailableBinding = false;
        foreach (var binding in _requestedMacroKeyBindings)
        {
            if (!_supportedMacroKeys.TryGetValue(binding.DeviceId, out var supported) || !supported.Contains(binding.KeyId))
                hasUnavailableBinding = true;
            else
                desired.Add(binding);
        }

        var previous = _configuredMacroKeyBindings;
        string? releaseFailure = null;
        var devices = previous.Select(binding => binding.DeviceId)
            .Concat(desired.Select(binding => binding.DeviceId))
            .Concat(_claimedMacroKeyDevices).Distinct(StringComparer.Ordinal).ToArray();
        if (desired.Count > 0 && !_macroEventsSubscribed)
        {
            var subscribeError = _subscribeForEvents!(_macroKeyEventHandler!, IntPtr.Zero);
            if (subscribeError != 0)
                throw new InvalidOperationException(Localization.Translate((FormattableString)$"iCUE konnte G-Tastenereignisse nicht abonnieren (SDK-Fehler {subscribeError})."));
            _macroEventsSubscribed = true;
        }

        foreach (var deviceId in devices)
        {
            var oldKeys = previous.Where(binding => binding.DeviceId == deviceId).ToHashSet();
            var newKeys = desired.Where(binding => binding.DeviceId == deviceId).ToHashSet();
            if (newKeys.Count > 0)
            {
                var controlError = _requestControl!(deviceId, ExclusiveKeyEventsListening);
                if (controlError != 0)
                    throw new InvalidOperationException(Localization.Translate((FormattableString)$"iCUE hat den Zugriff auf G-Tasten von {deviceId} abgelehnt (SDK-Fehler {controlError})."));
                _claimedMacroKeyDevices.Add(deviceId);
            }

            foreach (var binding in oldKeys.Except(newKeys))
            {
                ConfigureKey(deviceId, binding.KeyId, intercepted: false);
                _configuredMacroKeyBindings.Remove(binding);
                _currentlyPressedMacroKeys.Remove(binding);
            }
            foreach (var binding in newKeys.Except(oldKeys))
            {
                ConfigureKey(deviceId, binding.KeyId, intercepted: true);
                _configuredMacroKeyBindings.Add(binding);
            }

            if (newKeys.Count == 0 && _claimedMacroKeyDevices.Contains(deviceId))
            {
                var releaseError = _requestControl!(deviceId, 0);
                if (releaseError != 0)
                    releaseFailure = Localization.Translate((FormattableString)$"iCUE konnte den G-Tastenzugriff für {deviceId} nicht freigeben (SDK-Fehler {releaseError}).");
                else
                    _claimedMacroKeyDevices.Remove(deviceId);
            }
        }

        if (desired.Count == 0 && _macroEventsSubscribed)
        {
            try { _unsubscribeFromEvents!(); } catch { }
            _macroEventsSubscribed = false;
        }
        if (releaseFailure is not null)
            _macroKeyStatus = releaseFailure;
        else if (hasUnavailableBinding)
            _macroKeyStatus = Localization.Translate("Einige zugeordnete Corsair-G-Tasten sind nicht verfügbar; die übrigen werden überwacht.");
        else if (desired.Count > 0)
            _macroKeyStatus = Localization.Translate((FormattableString)$"iCUE überwacht {desired.Count} zugeordnete Corsair-G-Taste(n).");
        else if (_supportedMacroKeys.Values.Any(keys => keys.Any(IsSupportedMacroKeyId)))
            _macroKeyStatus = Localization.Translate("Corsair-G-Tasten sind verfügbar; derzeit ist keine Taste zugeordnet.");
    }

    private void ConfigureKey(string deviceId, int keyId, bool intercepted)
    {
        var configuration = new NativeKeyEventConfiguration { KeyId = keyId, IsIntercepted = intercepted ? (byte)1 : (byte)0 };
        var result = _configureKeyEvent!(deviceId, ref configuration);
        if (result != 0)
        {
            var error = intercepted
                ? Localization.Translate((FormattableString)$"iCUE konnte Corsair-G{keyId} nicht aktivieren (SDK-Fehler {result}).")
                : Localization.Translate((FormattableString)$"iCUE konnte Corsair-G{keyId} nicht freigeben (SDK-Fehler {result}).");
            throw new InvalidOperationException(error);
        }
    }

    private void OnMacroKeyEvent(IntPtr context, IntPtr eventPointer)
    {
        if (eventPointer == IntPtr.Zero) return;
        try
        {
            var nativeEvent = Marshal.PtrToStructure<NativeEvent>(eventPointer);
            if (nativeEvent.Id != KeyEventId || nativeEvent.Data == IntPtr.Zero) return;
            var nativeKey = Marshal.PtrToStructure<NativeKeyEvent>(nativeEvent.Data);
            var deviceId = GetNullTerminatedAscii(nativeKey.DeviceId);
            if (string.IsNullOrWhiteSpace(deviceId) || !IsSupportedMacroKeyId(nativeKey.KeyId)) return;
            var press = new CorsairMacroKeyPress(deviceId, nativeKey.KeyId);
            var binding = new CorsairMacroKeyBinding(press.DeviceId, press.KeyId);
            lock (_sync)
            {
                if (_disposed) return;
                if (nativeKey.IsPressed == 0)
                {
                    _currentlyPressedMacroKeys.Remove(binding);
                    return;
                }
                // iCUE may repeat key-down events while a G-key is held; dispatch only the edge.
                if (!_requestedMacroKeyBindings.Contains(binding) || !_currentlyPressedMacroKeys.Add(binding)) return;
            }
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Action<CorsairMacroKeyPress>? handler;
                lock (_sync)
                {
                    if (_disposed || !_requestedMacroKeyBindings.Contains(binding)) return;
                    handler = MacroKeyPressed;
                }
                try { handler?.Invoke(press); } catch { }
            });
        }
        catch { }
    }

    private void ResetMacroKeySessionState()
    {
        _supportedMacroKeys.Clear();
        _configuredMacroKeyBindings.Clear();
        _currentlyPressedMacroKeys.Clear();
        _claimedMacroKeyDevices.Clear();
        _macroEventsSubscribed = false;
        _macroKeyStatus = Localization.Translate("Corsair-G-Tasten warten auf eine iCUE-Verbindung.");
    }

    private void ReleaseConfiguredMacroKeyBindings()
    {
        if (!_macroKeyExportsAvailable || !_connected) return;
        var bindingsToRelease = _configuredMacroKeyBindings.Concat(_requestedMacroKeyBindings).ToHashSet();
        foreach (var deviceId in bindingsToRelease.Select(binding => binding.DeviceId)
                     .Concat(_claimedMacroKeyDevices).Distinct(StringComparer.Ordinal))
        {
            foreach (var binding in bindingsToRelease.Where(binding => binding.DeviceId == deviceId))
            {
                try
                {
                    var configuration = new NativeKeyEventConfiguration { KeyId = binding.KeyId, IsIntercepted = 0 };
                    _configureKeyEvent!(deviceId, ref configuration);
                }
                catch { }
            }
            try { _requestControl!(deviceId, 0); } catch { }
        }
        if (_macroEventsSubscribed) { try { _unsubscribeFromEvents!(); } catch { } }
        _configuredMacroKeyBindings.Clear();
        _currentlyPressedMacroKeys.Clear();
        _claimedMacroKeyDevices.Clear();
        _macroEventsSubscribed = false;
    }

    private static bool IsSupportedMacroKeyId(int keyId) => keyId is >= 1 and <= 6;

    [StructLayout(LayoutKind.Sequential)] private struct NativeEvent { public int Id; public IntPtr Data; }
    [StructLayout(LayoutKind.Sequential)] private struct NativeKeyEvent
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)] public byte[] DeviceId;
        public int KeyId;
        public byte IsPressed;
    }
    [StructLayout(LayoutKind.Sequential)] private struct NativeKeyEventConfiguration { public int KeyId; public byte IsIntercepted; }
    [StructLayout(LayoutKind.Sequential)] private struct NativeInt32Array { public IntPtr Items; public uint Count; }
    [StructLayout(LayoutKind.Explicit, Size = 16)] private struct NativeDataValue
    {
        [FieldOffset(0)] public NativeInt32Array Int32Array;
    }
    [StructLayout(LayoutKind.Sequential)] private struct NativeProperty { public int Type; public NativeDataValue Value; }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void EventHandler(IntPtr context, IntPtr eventData);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int SubscribeForEvents(EventHandler callback, IntPtr context);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int UnsubscribeFromEvents();
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)] private delegate int RequestControl([MarshalAs(UnmanagedType.LPStr)] string deviceId, int accessLevel);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)] private delegate int ConfigureKeyEvent([MarshalAs(UnmanagedType.LPStr)] string deviceId, ref NativeKeyEventConfiguration configuration);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)] private delegate int ReadDeviceProperty([MarshalAs(UnmanagedType.LPStr)] string deviceId, int propertyId, uint index, ref NativeProperty property);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int FreeProperty(ref NativeProperty property);
}

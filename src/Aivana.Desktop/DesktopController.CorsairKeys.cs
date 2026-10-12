using Aivana.Core;
using Aivana.Windows;

namespace Aivana.Desktop;

public sealed partial class DesktopController
{
    private readonly KeyGestureRouter _keyGestures = new();
    private System.Windows.Threading.DispatcherTimer? _gestureTimer;
    private string _gestureBindingSnapshot = "";
    public string ActiveKeyLayer => _keyGestures.ActiveLayer;
    private Action<CorsairMacroKeyPress>? _specialKeyCapture;
    private string? _specialKeyCaptureDevice;
    private int _specialKeyCaptureGeneration;

    public IDisposable BeginSpecialKeyCapture(string deviceId, Action<CorsairMacroKeyPress> captured)
    {
        ArgumentNullException.ThrowIfNull(captured);
        var supportedKeys = GetCorsairMacroKeys(deviceId).ToArray();
        if (_disposed || _specialKeyCapture is not null || !CorsairKeysAvailable || supportedKeys.Length == 0)
            throw new InvalidOperationException(CorsairKeyStatus);
        _specialKeyCaptureDevice = deviceId;
        _specialKeyCapture = captured;
        Interlocked.Increment(ref _specialKeyCaptureGeneration);
        try
        {
            RefreshCorsairKeyBindings();
            if (!supportedKeys.All(key => _corsair.IsMacroKeyBindingConfigured(deviceId, key)))
                throw new InvalidOperationException(CorsairKeyStatus);
        }
        catch
        {
            _specialKeyCapture = null; _specialKeyCaptureDevice = null;
            Interlocked.Increment(ref _specialKeyCaptureGeneration);
            try { RefreshCorsairKeyBindings(); } catch { /* Preserve the capture-start error after clearing its state. */ }
            throw;
        }
        return new SpecialKeyCaptureLease(this);
    }

    private sealed class SpecialKeyCaptureLease(DesktopController owner) : IDisposable
    {
        private DesktopController? _owner = owner;
        public void Dispose()
        {
            var controller = Interlocked.Exchange(ref _owner, null);
            if (controller is null) return;
            controller._specialKeyCapture = null; controller._specialKeyCaptureDevice = null;
            Interlocked.Increment(ref controller._specialKeyCaptureGeneration);
            controller.RefreshCorsairKeyBindings();
        }
    }
    public bool CorsairKeysAvailable => _corsair.KeysAvailable;
    public string CorsairKeyStatus => _corsair.KeyStatus;
    public IReadOnlyList<int> GetCorsairMacroKeys(string deviceId) => _corsair.GetSupportedMacroKeyIds(deviceId);

    private void InitializeCorsairKeys()
    {
        _corsair.MacroKeyChanged += OnCorsairMacroKeyChanged;
        _keyGestures.Triggered += tile => { if (!_disposed && !Paused && _inputInjectionDepth == 0) _ = ExecuteAsync(tile); };
        _gestureTimer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(25) };
        _gestureTimer.Tick += (_,_) => { if (!_corsair.KeysAvailable && ActiveProfile.Tiles.All(t => t.KeyboardBinding is null)) ResetKeyGestures(); else _keyGestures.Advance(Environment.TickCount64, _inputInjectionDepth == 0); };
        _gestureTimer.Start();
    }
    private void ResetKeyGestures() { _keyGestures.Reset(); Interlocked.Increment(ref _specialKeyCaptureGeneration); }
    private void OnCorsairMacroKeyChanged(CorsairMacroKeyEdge edge)
    {
        if (edge.IsPressed) { OnCorsairMacroKeyPressed(new(edge.DeviceId, edge.KeyId)); return; }
        var dispatcher = System.Windows.Application.Current?.Dispatcher;
        if (_disposed || dispatcher is null || dispatcher.HasShutdownStarted) return;
        var generation = Volatile.Read(ref _specialKeyCaptureGeneration);
        dispatcher.BeginInvoke(new Action(() =>
        {
            if (_disposed || Paused || generation != Volatile.Read(ref _specialKeyCaptureGeneration)) return;
            _keyGestures.Up(edge.DeviceId, edge.KeyId, Environment.TickCount64, _inputInjectionDepth == 0);
        }));
    }

    private void RefreshCorsairKeyBindings()
    {
        if (_disposed) return;
        var snapshot = System.Text.Json.JsonSerializer.Serialize(new { Settings.ActiveProfileId, Paused, _specialKeyCaptureDevice, Available = _corsair.KeysAvailable,
            Tiles = ActiveProfile.Tiles.Select(t => new { t.Id,t.Enabled,t.CorsairBinding,t.KeyboardBinding,t.KeyGesture,t.KeyLayer,t.HeldLayer,t.HoldMilliseconds,t.DoubleTapMilliseconds }) });
        if (snapshot != _gestureBindingSnapshot) { ResetKeyGestures(); _gestureBindingSnapshot = snapshot; Interlocked.Increment(ref _specialKeyCaptureGeneration); }
        var bindings = _specialKeyCaptureDevice is { } captureDevice
            ? GetCorsairMacroKeys(captureDevice).Select(key => new CorsairMacroKeyBinding(captureDevice, key)).ToArray()
            : Paused
            ? Array.Empty<CorsairMacroKeyBinding>()
            : ActiveProfile.Tiles.Where(tile => tile.Enabled && tile.CorsairBinding is not null)
                .Select(tile => new CorsairMacroKeyBinding(tile.CorsairBinding!.DeviceId, tile.CorsairBinding.KeyId))
                .Distinct().ToArray();
        _corsair.ConfigureMacroKeyBindings(bindings);
    }

    private void OnCorsairMacroKeyPressed(CorsairMacroKeyPress press)
    {
        var dispatcher = System.Windows.Application.Current?.Dispatcher;
        if (_disposed || dispatcher is null || dispatcher.HasShutdownStarted) return;
        var captureGeneration = Volatile.Read(ref _specialKeyCaptureGeneration);
        dispatcher.BeginInvoke(new Action(() =>
        {
            if (captureGeneration != Volatile.Read(ref _specialKeyCaptureGeneration)) return;
            if (!_disposed && _specialKeyCapture is { } capture)
            {
                if (StringComparer.OrdinalIgnoreCase.Equals(press.DeviceId, _specialKeyCaptureDevice)) capture(press);
                return;
            }
            if (_disposed || Paused || _inputInjectionDepth > 0) return;
            _keyGestures.Down(press.DeviceId, press.KeyId, ActiveProfile.Tiles, Environment.TickCount64);
        }));
    }

    private void DisposeCorsairKeys()
    {
        _specialKeyCapture = null; _specialKeyCaptureDevice = null;
        Interlocked.Increment(ref _specialKeyCaptureGeneration);
        _corsair.MacroKeyChanged -= OnCorsairMacroKeyChanged;
        _gestureTimer?.Stop();
        ResetKeyGestures();
    }
}

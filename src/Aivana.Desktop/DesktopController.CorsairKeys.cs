using Aivana.Core;
using Aivana.Windows;

namespace Aivana.Desktop;

public sealed partial class DesktopController
{
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

    private void InitializeCorsairKeys() => _corsair.MacroKeyPressed += OnCorsairMacroKeyPressed;

    private void RefreshCorsairKeyBindings()
    {
        if (_disposed) return;
        var bindings = _specialKeyCaptureDevice is { } captureDevice
            ? GetCorsairMacroKeys(captureDevice).Select(key => new CorsairMacroKeyBinding(captureDevice, key)).ToArray()
            : Paused || _inputInjectionDepth > 0
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
            var tile = ActiveProfile.Tiles.FirstOrDefault(candidate => candidate.Enabled
                && candidate.CorsairBinding is { } binding
                && binding.DeviceId == press.DeviceId && binding.KeyId == press.KeyId);
            if (tile is not null) _ = ExecuteAsync(tile);
        }));
    }

    private void DisposeCorsairKeys()
    {
        _specialKeyCapture = null; _specialKeyCaptureDevice = null;
        Interlocked.Increment(ref _specialKeyCaptureGeneration);
        _corsair.MacroKeyPressed -= OnCorsairMacroKeyPressed;
    }
}

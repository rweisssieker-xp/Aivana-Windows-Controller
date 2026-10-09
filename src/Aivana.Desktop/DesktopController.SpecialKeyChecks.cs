using Aivana.Windows;
using System.Windows.Threading;

namespace Aivana.Desktop;

public sealed partial class DesktopController
{
    internal async Task RunSpecialKeyCaptureChecksAsync()
    {
        var device = CorsairKeyboards.FirstOrDefault(keyboard => GetCorsairMacroKeys(keyboard.DeviceId).Count > 0);
        if (device is null || !CorsairKeysAvailable) return;
        var keys = GetCorsairMacroKeys(device.DeviceId).ToArray();
        var received = 0;
        using (BeginSpecialKeyCapture(device.DeviceId, _ => received++))
        {
            if (!keys.All(key => _corsair.IsMacroKeyBindingConfigured(device.DeviceId, key)))
                throw new InvalidOperationException("Special-key capture did not configure the connected device.");
            OnCorsairMacroKeyPressed(new CorsairMacroKeyPress(device.DeviceId, keys[0]));
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() => { }, DispatcherPriority.ApplicationIdle);
            if (received != 1) throw new InvalidOperationException("Special-key capture did not forward its callback.");
        }
        received = 0;
        var pending = BeginSpecialKeyCapture(device.DeviceId, _ => received++);
        OnCorsairMacroKeyPressed(new CorsairMacroKeyPress(device.DeviceId, keys[0]));
        pending.Dispose();
        await System.Windows.Application.Current.Dispatcher.InvokeAsync(() => { }, DispatcherPriority.ApplicationIdle);
        if (received != 0) throw new InvalidOperationException("Cancelled capture forwarded a stale callback.");
        if (Paused && keys.Any(key => _corsair.IsMacroKeyBindingConfigured(device.DeviceId, key)))
            throw new InvalidOperationException("Special-key capture did not restore paused bindings.");
    }
}

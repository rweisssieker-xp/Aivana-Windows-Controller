namespace Aivana.Desktop;

public sealed partial class DesktopController
{
    public void SetAutoStart(bool enabled) =>
        throw new InvalidOperationException("Autostart is unavailable in this Store release.");
}

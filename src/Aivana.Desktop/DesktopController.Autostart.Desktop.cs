using Microsoft.Win32;

namespace Aivana.Desktop;

public sealed partial class DesktopController
{
    public void SetAutoStart(bool enabled)
    {
        var executable = Environment.ProcessPath ?? throw new InvalidOperationException("Programmpfad unbekannt.");
        using var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
        if (enabled) key.SetValue("AivanaController", "\"" + executable + "\"");
        else key.DeleteValue("AivanaController", false);
        Settings.AutoStart = enabled; Save();
    }
}

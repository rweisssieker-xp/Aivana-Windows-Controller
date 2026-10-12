using System.Drawing;

namespace Aivana.Desktop;

internal static class AppIcons
{
    internal static Icon Load(bool paused = false)
    {
        var name = paused ? "makrivo-paused" : "makrivo";
        using var stream = typeof(AppIcons).Assembly.GetManifestResourceStream($"Aivana.Desktop.Assets.{name}.ico")
            ?? throw new InvalidOperationException("Application icon resource is missing.");
        using var icon = new Icon(stream);
        return (Icon)icon.Clone();
    }
}

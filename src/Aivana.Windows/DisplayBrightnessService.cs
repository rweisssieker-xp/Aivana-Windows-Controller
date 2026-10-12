using Aivana.Core;
namespace Aivana.Windows;

public sealed record DisplayBrightnessInfo(string Id, string Name, int Percent);

public static class DisplayBrightnessService
{
    // Native monitor I/O may take hundreds of milliseconds; callers should use a worker thread.
    public static IReadOnlyList<DisplayBrightnessInfo> GetDisplays(CancellationToken token = default)
    {
        var displays = new List<DisplayBrightnessInfo>();
        DisplayBrightnessControl.Apply(default, token, probeOnly: true, displays: displays);
        return displays;
    }

    public static int Apply(BrightnessCommand command, string? deviceId, CancellationToken token = default)
        => DisplayBrightnessControl.Apply(command, token, deviceId: deviceId);
}

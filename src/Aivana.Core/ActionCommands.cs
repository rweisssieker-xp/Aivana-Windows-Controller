namespace Aivana.Core;

public static class ActionCommands
{
    public static IReadOnlyList<string> Obs { get; } = Array.AsReadOnly(new[]
    {
        "StartRecord", "StopRecord", "ToggleRecord", "PauseRecord", "ResumeRecord",
        "StartStream", "StopStream", "ToggleStream", "SetCurrentProgramScene", "ToggleInputMute"
    });

    public static bool IsObsEndpoint(string endpoint) =>
        Uri.TryCreate(string.IsNullOrWhiteSpace(endpoint) ? "ws://127.0.0.1:4455" : endpoint, UriKind.Absolute, out var uri)
        && string.IsNullOrEmpty(uri.UserInfo) && string.IsNullOrEmpty(uri.Query) && string.IsNullOrEmpty(uri.Fragment)
        && (uri.Scheme == "wss" || uri.Scheme == "ws" && uri.IsLoopback);

    public static bool IsExtendedVolume(string value) =>
        new[] { "MuteOn", "MuteOff", "ToggleMute", "MicMuteOn", "MicMuteOff", "MicToggleMute" }.Contains(value, StringComparer.OrdinalIgnoreCase)
        || value.StartsWith("Set:", StringComparison.OrdinalIgnoreCase)
        && int.TryParse(value.AsSpan(4), System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out var percent)
        && percent is >= 0 and <= 100;
}

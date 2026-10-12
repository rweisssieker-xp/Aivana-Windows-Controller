namespace Aivana.Core;
public enum WorkspaceKind { Workplace, Meeting, Streaming, Research, Project }
public sealed record WorkspaceSelection(WorkspaceKind Kind, string Name, IReadOnlyList<string> Programs, IReadOnlyList<string> Urls, string? AudioDeviceId, string? DisplayDeviceId, string ObsScene, string Hotkey = "");
public static class WorkspaceSetup
{
    public static Profile CreateProfile(WorkspaceSelection selection)
    {
        if (string.IsNullOrWhiteSpace(selection.Name)) throw new ArgumentException("Profilname fehlt.");
        if (selection.Kind == WorkspaceKind.Streaming && string.IsNullOrWhiteSpace(selection.ObsScene)) throw new ArgumentException("OBS-Szene auswählen.");
        var steps = new List<ActionStep>();
        foreach (var program in selection.Programs)
        {
            if (string.IsNullOrWhiteSpace(program)) throw new ArgumentException("Programmpfad fehlt.");
            steps.Add(new() { Kind = ActionKind.Launch, Value = program, Verification = new(true) });
        }
        foreach (var url in selection.Urls)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var address) || address.Scheme is not ("https" or "http")) throw new ArgumentException("Ungültige Webadresse.");
            steps.Add(new() { Kind = ActionKind.Url, Value = url });
        }
        if (!string.IsNullOrEmpty(selection.AudioDeviceId)) steps.Add(new() { Kind = ActionKind.Volume, Value = "Set:50", AudioDeviceId = selection.AudioDeviceId, Verification = new(true) });
        if (!string.IsNullOrEmpty(selection.DisplayDeviceId)) steps.Add(new() { Kind = ActionKind.Brightness, Value = "Set:50", DisplayDeviceId = selection.DisplayDeviceId, Verification = new(true) });
        if (selection.Kind == WorkspaceKind.Meeting) steps.Add(new() { Kind = ActionKind.Volume, Value = "MicMuteOn", Verification = new(true) });
        if (selection.Kind == WorkspaceKind.Streaming) steps.Add(new() { Kind = ActionKind.Obs, Value = "SetCurrentProgramScene", Arguments = selection.ObsScene, Verification = new(true) });
        if (steps.Count == 0) throw new ArgumentException("Mindestens ein Programm, eine Webseite oder ein Gerät auswählen.");
        var profile = new Profile { Name = selection.Name.Trim(), Tiles = [new() { Name = selection.Name.Trim(), Hotkey = selection.Hotkey, Steps = steps }] };
        var errors = ConfigurationValidator.ValidateTile(profile.Tiles[0]);
        if (errors.Count != 0) throw new ArgumentException(string.Join("\n", errors));
        return profile;
    }
}

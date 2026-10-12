using Aivana.Core;
internal static class WorkspaceSetupChecks
{
    public static Task RunAsync()
    {
        foreach (var kind in Enum.GetValues<WorkspaceKind>())
        {
            var selection = new WorkspaceSelection(kind, "Work", ["C:\\Windows\\notepad.exe"], ["https://example.com"], "audio", "monitor", "Scene");
            var first = WorkspaceSetup.CreateProfile(selection);
            var second = WorkspaceSetup.CreateProfile(selection);
            if (first.Id == second.Id || first.Tiles.Count == 0 || first.Tiles[0].Steps.Any(s => s.Kind == ActionKind.Launch && string.IsNullOrEmpty(s.Value))) throw new Exception("Invalid workspace");
        }
        try { WorkspaceSetup.CreateProfile(new(WorkspaceKind.Streaming, "Stream", [], [], null, null, "")); throw new Exception("Missing scene accepted"); }
        catch (ArgumentException) { }
        return Task.CompletedTask;
    }
}

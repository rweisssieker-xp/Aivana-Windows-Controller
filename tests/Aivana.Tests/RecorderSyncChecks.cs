using Aivana.Core;

internal static class RecorderSyncChecks
{
    public static Task RunAsync()
    {
        var recording = new MacroRecording();
        recording.Capture(0x41, false, false, false, false, 0);
        recording.Capture(0x42, false, false, true, false, 50);
        if (recording.Steps.Count != 0) throw new Exception("Typed text was retained.");
        recording.Capture(0x43, true, false, false, false, 100);
        recording.Capture(0x56, true, false, false, false, 450);
        if (recording.Steps.Count != 3 || recording.Steps[0].Value != "Ctrl+C" || recording.Steps[1].DelayMs != 350 || recording.Steps[2].Value != "Ctrl+V") throw new Exception("Recording lost chord or wait order.");
        var source = ConfigStore.CreateDefaults();
        source.Profiles[0].Tiles.Add(new Tile { Name = "Test", CorsairBinding = new("machine-a", 1), Steps = [new() { Kind = ActionKind.Text, Value = "SECRET", Arguments = "SECRET" }, new() { Kind = ActionKind.Volume, Value = "Up", AudioDeviceId = "machine-a" }] });
        var portable = ProfileSync.PreparePortable(source, out var cleared, out var excluded);
        if (cleared != 2 || excluded != 1 || portable.Profiles.SelectMany(p => p.Tiles).Any(t => t.Enabled || t.CorsairBinding != null)) throw new Exception("Portable profile retained device state or enabled actions.");
        if (System.Text.Json.JsonSerializer.Serialize(portable).Contains("SECRET")) throw new Exception("Portable export retained free payload.");
        if (source.Profiles[0].Tiles[^1].Steps[0].Value != "SECRET") throw new Exception("Portable preparation mutated local data.");
        var complete = ProfileSync.PreparePortable(source, out _, out _, includeActionContent: true);
        if (complete.Profiles[0].Tiles[^1].Steps[0].Value != "SECRET") throw new Exception("Reviewed complete export lost action contents.");
        var folder = Path.Combine(Path.GetTempPath(), "makrivo-sync-test-" + Guid.NewGuid().ToString("N"));
        try
        {
            var first = ProfileSync.Push(portable, folder, "");
            var read = ProfileSync.Pull(folder, first);
            if (read.Profiles.Count != portable.Profiles.Count) throw new Exception("Portable pull lost profiles.");
            var conflict = false;
            try { ProfileSync.Push(portable, folder, "stale"); } catch (InvalidOperationException) { conflict = true; }
            if (!conflict || ProfileSync.Revision(Path.Combine(folder, ProfileSync.FileName)) != first) throw new Exception("Stale revision overwrote remote state.");
        }
        finally { if (Directory.Exists(folder)) Directory.Delete(folder, true); }
        return Task.CompletedTask;
    }
}

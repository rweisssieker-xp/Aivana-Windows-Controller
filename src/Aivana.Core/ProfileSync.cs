using System.Security.Cryptography;
using System.Text.Json;

namespace Aivana.Core;

/// <summary>User-driven folder exchange with explicit content review and optimistic conflict checks.</summary>
public static class ProfileSync
{
    public const string FileName = "makrivo-portable-profiles.json";
    public static string Revision(string path)
    {
        if (!File.Exists(path)) return "";
        using var stream = File.OpenRead(path);
        if (stream.Length > 10 * 1024 * 1024) throw new InvalidDataException(Localization.Translate("Die Konfigurationsdatei überschreitet die zulässige Größe von 10 MiB."));
        return Convert.ToHexString(SHA256.HashData(stream));
    }
    public static AppSettings PreparePortable(AppSettings source, out int clearedBindings, out int excludedSteps, bool includeActionContent = false)
    {
        var result = JsonSerializer.Deserialize<AppSettings>(JsonSerializer.Serialize(source))!;
        clearedBindings = 0; excludedSteps = 0;
        result.VendorIntegrations = new();
        result.MeetingAssistantEnabled = false;
        result.AutoStart = false; result.AutoProfiles = false;
        if (!includeActionContent) foreach (var profile in result.Profiles) { profile.ProcessName = ""; profile.WindowTitleContains = ""; }
        foreach (var tile in result.Profiles.SelectMany(p => p.Tiles))
        {
            tile.Enabled = false;
            if (tile.VendorBinding != null) { tile.VendorBinding = null; clearedBindings++; }
            if (tile.KeyboardBinding != null) { tile.KeyboardBinding = null; clearedBindings++; }
            if (tile.CorsairBinding != null) { tile.CorsairBinding = null; clearedBindings++; }
            if (tile.Steps.Count == 0) tile.Steps.Add(new ActionStep { Kind = ActionKind.Delay, DelayMs = 100 });
            foreach (var step in tile.Steps)
            {
                // Only closed command vocabularies can be exchanged without arbitrary secret-bearing payloads.
                if (!includeActionContent && step.Kind is not (ActionKind.Shortcut or ActionKind.Media or ActionKind.Volume or ActionKind.Delay or ActionKind.Window or ActionKind.System or ActionKind.Brightness))
                { step.UiAutomation = null; step.Plugin = null; step.Verification = null; step.Kind = ActionKind.Delay; step.Value = ""; step.DelayMs = 100; excludedSteps++; }
                if (!includeActionContent) { step.Arguments = ""; step.WorkingDirectory = ""; step.Condition = null; step.OnConditionFalseSkip = 0; }
                if (step.DisplayDeviceId != null) { step.DisplayDeviceId = null; clearedBindings++; }
                if (step.AudioDeviceId != null) { step.AudioDeviceId = null; clearedBindings++; }
            }
        }
        return result;
    }
    public static string Push(AppSettings portable, string folder, string expectedRevision)
    {
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, FileName);
        // Cooperating clients serialize the compare-and-write operation. Atomic replacement prevents partial reads.
        using var lease = new FileStream(path + ".lock", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        if (Revision(path) != expectedRevision) throw new InvalidOperationException(Localization.Translate("Synchronisierungskonflikt: Der Ordner wurde geändert. Vorschau neu laden."));
        new ConfigStore().Export(portable, path);
        return Revision(path);
    }
    public static AppSettings Pull(string folder, string expectedRevision)
    {
        var path = Path.Combine(folder, FileName);
        using var lease = new FileStream(path + ".lock", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        if (Revision(path) != expectedRevision) throw new InvalidOperationException(Localization.Translate("Synchronisierungskonflikt: Der Ordner wurde geändert. Vorschau neu laden."));
        var imported = new ConfigStore().ReadImport(path);
        return PreparePortable(imported, out _, out _, includeActionContent: true);
    }
}

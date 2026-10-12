using System.IO.Compression;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace Aivana.Core.Plugins;
public sealed record ReviewedPlugin(string Path, string Digest, PluginManifest Manifest);
public static class PluginPackage
{
    public static ReviewedPlugin Inspect(string path)
    {
        using var file = File.OpenRead(path);
        var digest = Convert.ToHexString(SHA256.HashData(file)); file.Position = 0;
        using var zip = new ZipArchive(file, ZipArchiveMode.Read);
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase); long total = 0;
        foreach (var entry in zip.Entries)
        {
            var name = entry.FullName;
            if (name.Equals("enabled",StringComparison.OrdinalIgnoreCase) || name.Equals("package.zip",StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Reserved package path.");
            if (name.Length > 240 || name.Contains('\\') || name.Contains(':') || name.StartsWith('/') || name.Split('/').Any(p => p is ".." or "." or "") || !names.Add(name) || (entry.ExternalAttributes >> 16 & 0xF000) == 0xA000) throw new InvalidDataException("Unsafe package path.");
            total = checked(total + entry.Length); if (total > 100 * 1024 * 1024 || zip.Entries.Count > 2000) throw new InvalidDataException("Package too large.");
        }
        var manifestEntry = zip.GetEntry("manifest.json") ?? throw new InvalidDataException("Manifest missing.");
        if (manifestEntry.Length > 65536) throw new InvalidDataException("Manifest too large.");
        using var manifestStream = manifestEntry.Open();
        var manifest = JsonSerializer.Deserialize<PluginManifest>(manifestStream) ?? throw new InvalidDataException("Manifest invalid.");
        ValidateManifest(manifest);
        if (names.Count != manifest.Hashes.Count + 1) throw new InvalidDataException("Unhashed package content.");
        foreach (var hash in manifest.Hashes)
        {
            var entry = zip.GetEntry(hash.Key) ?? throw new InvalidDataException("Missing package file."); using var input = entry.Open();
            if (!Convert.ToHexString(SHA256.HashData(input)).Equals(hash.Value, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Package hash mismatch.");
        }
        return new(System.IO.Path.GetFullPath(path), digest, manifest);
    }
    public static void ValidateManifest(PluginManifest manifest)
    {
        if (!Regex.IsMatch(manifest.Id ?? "", "^[a-z0-9][a-z0-9.-]{0,63}$") || manifest.Id!.EndsWith(".rollback") || !System.Version.TryParse(manifest.Version, out _) || manifest.ProtocolVersion != 1 || manifest.Actions == null || manifest.Capabilities == null || manifest.Hashes == null || manifest.Actions.Length == 0) throw new InvalidDataException("Unsupported manifest.");
        foreach (var dependency in manifest.Dependencies ?? []) if (!Regex.IsMatch(dependency,"^[a-z0-9][a-z0-9.-]{0,63}$") || dependency.EndsWith(".rollback")) throw new InvalidDataException("Invalid dependency.");
        if (string.IsNullOrEmpty(manifest.EntryPoint) || manifest.EntryPoint.Contains('/') || manifest.EntryPoint.Contains('\\') || !manifest.EntryPoint.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) || !manifest.Hashes.ContainsKey(manifest.EntryPoint)) throw new InvalidDataException("Invalid plugin executable.");
        if (manifest.Actions.Select(a => a.Id).Distinct(StringComparer.Ordinal).Count() != manifest.Actions.Length) throw new InvalidDataException("Duplicate plugin action.");
        foreach (var action in manifest.Actions) { using var schema = JsonDocument.Parse(action.ParameterSchema); if (schema.RootElement.ValueKind != JsonValueKind.Object) throw new InvalidDataException("Invalid parameter schema."); }
    }
    public static void Install(ReviewedPlugin reviewed, string root)
    {
        var fresh = Inspect(reviewed.Path);
        if (fresh.Digest != reviewed.Digest) throw new InvalidDataException("Package changed since preview.");
        Directory.CreateDirectory(root); RejectReparse(root);
        var destination = System.IO.Path.Combine(root, fresh.Manifest.Id);
        var backup = destination + ".rollback"; var staging = System.IO.Path.Combine(root, ".stage-" + Guid.NewGuid().ToString("N"));
        if (Directory.Exists(destination))
        {
            RejectReparse(destination);
            var existing = JsonSerializer.Deserialize<PluginManifest>(File.ReadAllText(System.IO.Path.Combine(destination,"manifest.json")))!;
            if (System.Version.Parse(existing.Version) > System.Version.Parse(fresh.Manifest.Version)) throw new InvalidDataException("Plugin downgrade rejected.");
        }
        foreach (var dependency in fresh.Manifest.Dependencies ?? []) if (!File.Exists(System.IO.Path.Combine(root, dependency, "manifest.json"))) throw new InvalidDataException("Missing dependency: " + dependency);
        try
        {
            Directory.CreateDirectory(staging);
            using (var source = new FileStream(fresh.Path,FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                if (Convert.ToHexString(SHA256.HashData(source)) != fresh.Digest) throw new InvalidDataException("Package changed before extraction.");
                source.Position=0;
                using (var archive = new ZipArchive(source,ZipArchiveMode.Read,true)) archive.ExtractToDirectory(staging);
                source.Position=0; using var copy=File.Create(System.IO.Path.Combine(staging,"package.zip"));source.CopyTo(copy);
            }
            if (Directory.Exists(backup)) throw new InvalidDataException("Previous rollback exists; inspect before upgrading.");
            if (Directory.Exists(destination)) Directory.Move(destination, backup);
            Directory.Move(staging, destination); // imports are disabled: no enabled marker is copied
        }
        catch { if (!Directory.Exists(destination) && Directory.Exists(backup)) Directory.Move(backup, destination); throw; }
        finally { if (Directory.Exists(staging)) Directory.Delete(staging, true); }
    }
    private static string PackageFolder(string root, string id)
    {
        if (!Regex.IsMatch(id, "^[a-z0-9][a-z0-9.-]{0,63}$") || id.EndsWith(".rollback")) throw new ArgumentException("Invalid package id.");
        RejectReparse(root); var folder = System.IO.Path.Combine(System.IO.Path.GetFullPath(root), id); RejectReparse(folder); return folder;
    }
    public static void RestoreRollback(string root, string id)
    {
        var destination = PackageFolder(root,id); var backup = destination + ".rollback"; RejectReparse(backup);
        if (!Directory.Exists(backup)) throw new InvalidOperationException("Keine vorherige Version vorhanden.");
        var staging = System.IO.Path.Combine(System.IO.Path.GetFullPath(root), ".stage-" + Guid.NewGuid().ToString("N"));
        if (Directory.Exists(destination)) Directory.Move(destination,staging);
        try { Directory.Move(backup,destination); }
        catch { if (Directory.Exists(staging)) Directory.Move(staging,destination); throw; }
        if (Directory.Exists(staging)) Directory.Move(staging,backup);
        File.Delete(System.IO.Path.Combine(destination,"enabled")); // restored packages require explicit approval again
    }
    public static void DiscardRollback(string root, string id)
    {
        var backup = PackageFolder(root,id) + ".rollback"; RejectReparse(backup);
        if (Directory.Exists(backup)) Directory.Delete(backup,true);
    }
    public static void RejectReparse(string path)
    {
        for (var directory = new DirectoryInfo(System.IO.Path.GetFullPath(path)); directory != null; directory = directory.Parent)
            if (directory.Exists && (directory.Attributes & FileAttributes.ReparsePoint) != 0) throw new InvalidDataException("Reparse points are not allowed in plugin paths.");
    }
}

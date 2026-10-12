using Aivana.Core.Plugins;
using System.IO.Compression;
using System.Text.Json;
internal static class PluginChecks
{
    public static Task RunAsync()
    {
        var root = Path.Combine(Path.GetTempPath(), "MakrivoPackage-" + Guid.NewGuid().ToString("N")); Directory.CreateDirectory(root);
        try
        {
            var bad = Path.Combine(root, "bad.zip");
            using (var zip = ZipFile.Open(bad, ZipArchiveMode.Create)) { using var writer = new StreamWriter(zip.CreateEntry("../escape.exe").Open()); writer.Write("bad"); }
            try { PluginPackage.Inspect(bad); throw new Exception("Traversal accepted"); } catch (InvalidDataException) { }
            var manifest = new PluginManifest("demo", "1.0.0", "demo.exe", [new("demo.wait", "Wait", "{\"type\":\"object\"}")], [], new Dictionary<string,string> { ["demo.exe"] = new string('0',64) });
            var valid = Path.Combine(root, "valid.zip");
            using (var zip = ZipFile.Open(valid, ZipArchiveMode.Create))
            {
                using (var writer = new StreamWriter(zip.CreateEntry("manifest.json").Open())) writer.Write(JsonSerializer.Serialize(manifest));
                using (var writer = new StreamWriter(zip.CreateEntry("demo.exe").Open())) writer.Write("fake");
            }
            try { PluginPackage.Inspect(valid); throw new Exception("Hash mismatch accepted"); } catch (InvalidDataException) { }
            var installed = Path.Combine(root,"installed"); Directory.CreateDirectory(Path.Combine(installed,"demo")); Directory.CreateDirectory(Path.Combine(installed,"demo.rollback"));
            File.WriteAllText(Path.Combine(installed,"demo","version"),"2"); File.WriteAllText(Path.Combine(installed,"demo.rollback","version"),"1");
            PluginPackage.RestoreRollback(installed,"demo");
            if (File.ReadAllText(Path.Combine(installed,"demo","version")) != "1") throw new Exception("Rollback restore failed");
            PluginPackage.DiscardRollback(installed,"demo");
            if (Directory.Exists(Path.Combine(installed,"demo.rollback"))) throw new Exception("Rollback discard failed");

        }
        finally { Directory.Delete(root, true); }
        return Task.CompletedTask;
    }
}

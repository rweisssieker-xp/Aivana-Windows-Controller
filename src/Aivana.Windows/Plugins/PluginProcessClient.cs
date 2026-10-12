using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;
using Aivana.Core.Plugins;
namespace Aivana.Windows.Plugins;
public static class PluginProcessClient
{
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<Guid,(string Package, CancellationTokenSource Cancellation)> Active = new();
    public static void Stop(string package) { foreach (var entry in Active.Values.Where(x => x.Package == package)) try { entry.Cancellation.Cancel(); } catch (ObjectDisposedException) { } }
    public static void Disable(string folder) { File.Delete(Path.Combine(folder,"enabled")); Stop(Path.GetFileName(folder)); }
    public static string Root => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MAKRIVO", "Plugins");
    public static async Task<JsonElement> InvokeAsync(PluginInvocation invocation, CancellationToken token)
    {
        if (invocation.TimeoutMs is < 100 or > 60000 || invocation.PackageId.IndexOfAny(['/', '\\', ':']) >= 0 || invocation.PackageId is ".." or ".") throw new ArgumentException("Invalid plugin invocation.");
        var folder = Path.Combine(Root, invocation.PackageId); PluginPackage.RejectReparse(folder);
        var manifestPath = Path.Combine(folder, "manifest.json");
        var manifest = JsonSerializer.Deserialize<PluginManifest>(File.ReadAllText(manifestPath)) ?? throw new InvalidDataException("Manifest invalid.");
        PluginPackage.ValidateManifest(manifest);
        var digest = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(manifestPath)));
        if (!File.Exists(Path.Combine(folder,"enabled")) || File.ReadAllText(Path.Combine(folder,"enabled")) != digest) throw new InvalidOperationException("Plugin ist deaktiviert oder wurde verändert.");
        foreach (var hash in manifest.Hashes)
        {
            var target = Path.GetFullPath(Path.Combine(folder, hash.Key));
            if (!target.StartsWith(Path.GetFullPath(folder) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Unsafe plugin path.");
            PluginPackage.RejectReparse(Path.GetDirectoryName(target)!);
            if ((File.GetAttributes(target) & FileAttributes.ReparsePoint) != 0 || !Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(target))).Equals(hash.Value, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("Installed plugin changed.");
        }
        foreach (var dependency in manifest.Dependencies ?? []) if (!File.Exists(Path.Combine(Root,dependency,"manifest.json"))) throw new InvalidOperationException("Missing dependency: " + dependency);
        var action = manifest.Actions.SingleOrDefault(a => a.Id == invocation.ActionId) ?? throw new InvalidOperationException("Plugin action unavailable.");
        PluginParameters.Validate(action.ParameterSchema, invocation.Parameters);
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token); timeout.CancelAfter(invocation.TimeoutMs);
        var pipeName = "MakrivoPlugin-" + Guid.NewGuid().ToString("N");
        await using var pipe = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);
        using var process = new Process { StartInfo = new(Path.Combine(folder, manifest.EntryPoint)) { UseShellExecute = false, CreateNoWindow = true, WorkingDirectory = folder } };
        process.StartInfo.ArgumentList.Add("--pipe"); process.StartInfo.ArgumentList.Add(pipeName);
        var activeId = Guid.NewGuid(); Active[activeId] = (invocation.PackageId,timeout);
        using var registration = timeout.Token.Register(() => { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } });
        try
        {
            process.Start();
            await pipe.WaitForConnectionAsync(timeout.Token);
            var id = Guid.NewGuid();
            await PluginWire.WriteAsync(pipe, new(1,id,"hello",JsonSerializer.SerializeToElement(new { package = manifest.Id })),timeout.Token);
            var hello = await PluginWire.ReadAsync(pipe, timeout.Token);
            if (hello.Id != id || hello.Method != "hello" || !hello.Success) throw new InvalidDataException("Plugin handshake failed.");
            id = Guid.NewGuid();
            await PluginWire.WriteAsync(pipe, new(1,id,invocation.ActionId,invocation.Parameters),timeout.Token);
            var response = await PluginWire.ReadAsync(pipe, timeout.Token);
            if (response.Id != id || response.Method != invocation.ActionId) throw new InvalidDataException("Mismatched plugin reply.");
            if (!response.Success) throw new InvalidOperationException("Plugin action failed.");
            return response.Parameters;
        }
        finally { Active.TryRemove(activeId,out _); try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } }
    }
    public static void Enable(string folder)
    {
        PluginPackage.RejectReparse(folder);
        var path = Path.Combine(folder,"manifest.json");
        File.WriteAllText(Path.Combine(folder,"enabled"), Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path))));
    }
}

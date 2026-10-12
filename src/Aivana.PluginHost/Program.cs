using System.IO.Pipes;
using System.Text.Json;
using Aivana.Core.Plugins;
// Working reference plugin: one isolated invocation per worker lifetime.
if (args.Length != 2 || args[0] != "--pipe") return 2;
await using var pipe = new NamedPipeClientStream(".", args[1], PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);
using var lifetime = new CancellationTokenSource(TimeSpan.FromSeconds(65));
await pipe.ConnectAsync(10000, lifetime.Token);
var hello = await PluginWire.ReadAsync(pipe, lifetime.Token);
if (hello.Method != "hello") return 3;
await PluginWire.WriteAsync(pipe, hello, lifetime.Token);
var request = await PluginWire.ReadAsync(pipe, lifetime.Token);
if (request.Method == "example.countdown")
{
    var seconds = request.Parameters.GetProperty("seconds").GetInt32();
    if (seconds is < 0 or > 30) return 4;
    await Task.Delay(seconds * 1000, lifetime.Token);
    await PluginWire.WriteAsync(pipe, request with { Parameters = JsonSerializer.SerializeToElement(new { completed = true, remaining = 0 }) }, lifetime.Token);
}
else if (request.Method == "example.state") await PluginWire.WriteAsync(pipe, request with { Parameters = JsonSerializer.SerializeToElement(new { available = true }) }, lifetime.Token);
else await PluginWire.WriteAsync(pipe, request with { Success = false, Error = "Unknown action" }, lifetime.Token);
return 0;

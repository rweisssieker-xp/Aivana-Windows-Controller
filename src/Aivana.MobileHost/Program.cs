using System.IO.Pipes;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;
using Aivana.Core;
using Aivana.Core.Plugins;

if (args.Length != 4 || args[0] != "--pipe" || args[2] != "--bind" || !IPAddress.TryParse(args[3], out var bind)) return;
var pipeName = args[1];
var folder = Environment.GetEnvironmentVariable("MAKRIVO_MOBILE_DATA") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MAKRIVO", "MobilePanel"); Directory.CreateDirectory(folder);
var security = new MobilePanelSecurity();
var tokenPath = Path.Combine(folder,"clients.json");
if (File.Exists(tokenPath)) security.ImportHashes(JsonSerializer.Deserialize<string[]>(File.ReadAllText(tokenPath)) ?? []);
var code = security.NewPairingCode();
using var certificate = CertificateStore.LoadOrCreate(folder,bind);
File.WriteAllBytes(Path.Combine(folder,"panel.cer"),certificate.Export(X509ContentType.Cert));
var builder = WebApplication.CreateBuilder(new WebApplicationOptions { Args = [], ContentRootPath = AppContext.BaseDirectory });
builder.Logging.ClearProviders();
if (Environment.GetEnvironmentVariable("MAKRIVO_MOBILE_DIAGNOSTICS") == "1") builder.Logging.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Trace).SetMinimumLevel(LogLevel.Debug);
builder.WebHost.ConfigureKestrel(server => { server.Limits.MaxRequestBodySize = 65536; server.Limits.MaxConcurrentConnections=30; server.Limits.MaxConcurrentUpgradedConnections=10; server.Listen(bind, 47831, options => options.UseHttps(certificate)); });
builder.Services.AddRateLimiter(options => options.AddPolicy("commands", context => RateLimitPartition.GetTokenBucketLimiter(context.Connection.RemoteIpAddress?.ToString() ?? "unknown", _ => new TokenBucketRateLimiterOptions { TokenLimit = 30, TokensPerPeriod = 10, ReplenishmentPeriod = TimeSpan.FromSeconds(1), AutoReplenishment = true, QueueLimit = 0 })));
var app = builder.Build();
app.Use(async (context,next) =>
{
    context.Response.Headers["Content-Security-Policy"] = "default-src 'self'; connect-src 'self'; script-src 'self'; style-src 'self'; frame-ancestors 'none'";
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["Cache-Control"] = "no-store";
    var origin = context.Request.Headers.Origin.ToString();
    if (origin.Length > 0 && origin != "https://" + context.Request.Host) { context.Response.StatusCode = 403; return; }
    if (context.Request.Path.StartsWithSegments("/api") && context.Request.Path != "/api/pair" && !security.Authenticate(context.Request.Cookies["makrivo"])) { context.Response.StatusCode = 401; return; }
    await next();
});
app.UseRateLimiter(); app.UseWebSockets(); app.UseDefaultFiles(); app.UseStaticFiles();
app.MapPost("/api/pair", async (HttpContext context) =>
{
    var request = await context.Request.ReadFromJsonAsync<PairRequest>();
    var token = security.Pair(request?.Code ?? ""); if (token == null) return Results.Unauthorized();
    File.WriteAllText(tokenPath,JsonSerializer.Serialize(security.ExportHashes()));
    context.Response.Cookies.Append("makrivo",token,new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, MaxAge = TimeSpan.FromDays(30) }); return Results.Ok();
}).RequireRateLimiting("commands");
app.MapGet("/api/state", async (HttpContext context) => Results.Json(await Controller("snapshot",JsonSerializer.SerializeToElement(new {}),context.RequestAborted))).RequireRateLimiting("commands");
app.MapPost("/api/command", async (HttpContext context) =>
{
    var request = await context.Request.ReadFromJsonAsync<MobileCommand>(); if (request == null) return Results.BadRequest();
    return Results.Json(await Controller("command",JsonSerializer.SerializeToElement(request),context.RequestAborted));
}).RequireRateLimiting("commands");
app.Map("/api/live", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest || context.Request.Headers.Origin.ToString() != "https://" + context.Request.Host) { context.Response.StatusCode = 400; return; }
    using var socket = await context.WebSockets.AcceptWebSocketAsync();
    using var disconnected = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted);
    var receive = Task.Run(async () => { var buffer = new byte[65536]; while (socket.State == WebSocketState.Open) { var message = await socket.ReceiveAsync(buffer.AsMemory(),disconnected.Token); if (message.MessageType == WebSocketMessageType.Close || !message.EndOfMessage) break; } disconnected.Cancel(); });
    try
    {
        while (!disconnected.IsCancellationRequested && security.Authenticate(context.Request.Cookies["makrivo"]))
        {
            var state = await Controller("snapshot",JsonSerializer.SerializeToElement(new {}),disconnected.Token);
            await socket.SendAsync(JsonSerializer.SerializeToUtf8Bytes(state),WebSocketMessageType.Text,true,disconnected.Token); await Task.Delay(1000,disconnected.Token);
        }
    }
    catch (OperationCanceledException) { }
    finally { disconnected.Cancel(); socket.Abort(); try { await receive; } catch { } }
});
app.Lifetime.ApplicationStarted.Register(() => { Console.WriteLine(JsonSerializer.Serialize(new { code, url = $"https://{(bind.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ? "["+bind+"]" : bind)}:47831", fingerprint = certificate.GetCertHashString(HashAlgorithmName.SHA256) })); Console.Out.Flush(); });
if (int.TryParse(Environment.GetEnvironmentVariable("MAKRIVO_PARENT_PID"),out var parentId) && long.TryParse(Environment.GetEnvironmentVariable("MAKRIVO_PARENT_START"),out var parentStart))
    _ = Task.Run(async () =>
    {
        try
        {
            while (!app.Lifetime.ApplicationStopping.IsCancellationRequested)
            {
                using var parent = System.Diagnostics.Process.GetProcessById(parentId);
                if (parent.HasExited || parent.StartTime.ToUniversalTime().Ticks != parentStart) break;
                await Task.Delay(2000,app.Lifetime.ApplicationStopping);
            }
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or OperationCanceledException or System.ComponentModel.Win32Exception) { }
        finally { app.Lifetime.StopApplication(); }
    });
await app.RunAsync();

async Task<JsonElement> Controller(string method, JsonElement data, CancellationToken token)
{
    using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token); timeout.CancelAfter(5000);
    await using var pipe = new NamedPipeClientStream(".",pipeName,PipeDirection.InOut,PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);
    await pipe.ConnectAsync(3000,timeout.Token); var id = Guid.NewGuid();
    await PluginWire.WriteAsync(pipe,new(1,id,method,data),timeout.Token);
    var reply = await PluginWire.ReadAsync(pipe,timeout.Token); if (reply.Id != id || !reply.Success) throw new InvalidOperationException("Controller unavailable."); return reply.Parameters;
}
record PairRequest(string Code);

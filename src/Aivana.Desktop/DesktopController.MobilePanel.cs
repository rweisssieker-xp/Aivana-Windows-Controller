using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Text.Json;
using Aivana.Core;
using Aivana.Core.Plugins;
namespace Aivana.Desktop;
public sealed partial class DesktopController
{
    private CancellationTokenSource? _mobileCancellation;
    private Process? _mobileProcess;
    private HashSet<Guid> _mobileProfiles = [];
    private readonly CommandDeduplicator _mobileCommands = new();
    public bool MobilePanelRunning => _mobileProcess is { HasExited: false };
    public async Task<string> StartMobilePanelAsync(string address, IEnumerable<Guid> profiles)
    {
        StopMobilePanel();
        if (!System.Net.IPAddress.TryParse(address,out var ip) || ip.Equals(System.Net.IPAddress.Any) || ip.Equals(System.Net.IPAddress.IPv6Any)) throw new ArgumentException("Eine konkrete lokale IP-Adresse auswählen.");
        _mobileProfiles = profiles.ToHashSet(); if (_mobileProfiles.Count == 0) throw new ArgumentException("Mindestens ein Profil freigeben.");
        var executable = Path.Combine(AppContext.BaseDirectory,"Aivana.MobileHost.exe"); if (!File.Exists(executable)) throw new FileNotFoundException("Mobilpanel-Host fehlt. Vollständiges Paket verwenden.");
        var pipeName = "MakrivoMobile-" + Guid.NewGuid().ToString("N"); _mobileCancellation = new();
        _ = ListenMobileAsync(pipeName,_mobileCancellation.Token);
        _mobileProcess = new Process { StartInfo = new(executable) { UseShellExecute=false,CreateNoWindow=true,RedirectStandardOutput=true } };
        _mobileProcess.StartInfo.Environment["MAKRIVO_PARENT_PID"] = Environment.ProcessId.ToString();
        _mobileProcess.StartInfo.Environment["MAKRIVO_PARENT_START"] = Process.GetCurrentProcess().StartTime.ToUniversalTime().Ticks.ToString();
        foreach (var argument in new[] { "--pipe",pipeName,"--bind",address }) _mobileProcess.StartInfo.ArgumentList.Add(argument);
        try { _mobileProcess.Start(); return await _mobileProcess.StandardOutput.ReadLineAsync().WaitAsync(TimeSpan.FromSeconds(10)) ?? throw new InvalidOperationException("Mobilpanel konnte nicht gestartet werden."); }
        catch { StopMobilePanel(); throw; }
    }
    public void StopMobilePanel()
    {
        _mobileCancellation?.Cancel(); _mobileCancellation?.Dispose(); _mobileCancellation = null;
        try { if (_mobileProcess is { HasExited:false }) _mobileProcess.Kill(true); } catch (InvalidOperationException) { }
        _mobileProcess?.Dispose(); _mobileProcess=null;
    }
    public void RevokeMobileClients()
    {
        StopMobilePanel();
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MAKRIVO","MobilePanel","clients.json");
        if (File.Exists(path)) File.Delete(path);
    }
    private async Task ListenMobileAsync(string name, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await using var pipe = new NamedPipeServerStream(name,PipeDirection.InOut,1,PipeTransmissionMode.Byte,PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);
                await pipe.WaitForConnectionAsync(token);
                using var deadline=CancellationTokenSource.CreateLinkedTokenSource(token); deadline.CancelAfter(5000);
                var request=await PluginWire.ReadAsync(pipe,deadline.Token);
                var reply = await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    deadline.Token.ThrowIfCancellationRequested();
                    if (_disposed || token.IsCancellationRequested) return JsonSerializer.SerializeToElement(new { Accepted=false,Message="Panel stopped" });
                    if (request.Method=="snapshot")
                    {
                        var profiles=Settings.Profiles.Where(p=>_mobileProfiles.Contains(p.Id)).ToArray();
                        var ids=profiles.SelectMany(p=>p.Tiles).Select(t=>t.Id).ToHashSet();
                        return JsonSerializer.SerializeToElement(new MobileSnapshot(Paused,profiles.Select(p=>new MobileProfile(p.Id,p.Name,p.Tiles.Select(t=>new MobileAction(t.Id,t.Name,t.Icon,t.Enabled && string.IsNullOrWhiteSpace(t.HeldLayer),GetTileStatus(t.Id))).ToArray())).ToArray(),VerificationHistory.Where(r=>ids.Contains(r.TileId)).TakeLast(50).ToArray()));
                    }
                    if(request.Method=="command")
                    {
                        var command=request.Parameters.Deserialize<MobileCommand>();
                        var tile=Settings.Profiles.Where(p=>_mobileProfiles.Contains(p.Id)).SelectMany(p=>p.Tiles).FirstOrDefault(t=>t.Id==command?.ActionId);
                        if(command==null || tile==null || command.Operation is not ("run" or "cancel") || command.Operation=="run" && (Paused || !tile.Enabled || !string.IsNullOrWhiteSpace(tile.HeldLayer)) || !_mobileCommands.TryAccept(command.CommandId)) return JsonSerializer.SerializeToElement(new { Accepted=false,Message="Befehl nicht verfügbar oder bereits verarbeitet." });
                        if(command.Operation=="cancel") Cancel(tile.Id); else _=ExecuteAsync(tile);
                        return JsonSerializer.SerializeToElement(new { Accepted=true,Message="Accepted" });
                    }
                    return JsonSerializer.SerializeToElement(new { Accepted=false,Message="Unknown request" });
                }, System.Windows.Threading.DispatcherPriority.Normal, deadline.Token);
                await PluginWire.WriteAsync(pipe,request with { Parameters=reply },deadline.Token);
            }
            catch(OperationCanceledException) when(token.IsCancellationRequested) { return; }
            catch(Exception) { if (!token.IsCancellationRequested) await Task.Delay(100,token).ConfigureAwait(false); }
        }
    }
}

using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Runtime.InteropServices;
namespace Aivana.Windows.KeyboardAdapters;
public sealed class VendorLighting : IDisposable
{
    private readonly HttpClient _http=new(new HttpClientHandler { AllowAutoRedirect=false,UseProxy=false }) { Timeout=TimeSpan.FromSeconds(5) };
    private readonly SemaphoreSlim _gate=new(1,1);
    private Uri? _razer;
    private ApprovedNativeLibrary? _logitech;
    private LedShutdown? _shutdown;
    private CancellationTokenSource? _heartbeat;
    private Uri? _steelSeries;
    private Task? _heartbeatTask;
    public async Task SetAsync(string adapter,string color,string? approvedHash,CancellationToken token)
    {
        if(color.Length!=7 || color[0]!='#') throw new ArgumentException("Farbe: #RRGGBB.");
        var red=Convert.ToInt32(color.Substring(1,2),16); var green=Convert.ToInt32(color.Substring(3,2),16);var blue=Convert.ToInt32(color.Substring(5,2),16);
        await _gate.WaitAsync(token);
        try
        {
            if(adapter=="razer")
            {
                if(_razer==null)
                {
                    using var response=await _http.PostAsJsonAsync("http://127.0.0.1:54235/razer/chromasdk",new { title="MAKRIVO",description="Local keyboard workflow controls",author=new { name="Aivana",contact="" },device_supported=new[] { "keyboard" },category="application" },token);
                    response.EnsureSuccessStatusCode(); using var json=JsonDocument.Parse(await response.Content.ReadAsStringAsync(token));
                    if(!Uri.TryCreate(json.RootElement.GetProperty("uri").GetString(),UriKind.Absolute,out var uri) || !uri.IsLoopback || uri.Scheme!="http") throw new InvalidDataException("Invalid Chroma session.");
                    _razer=uri;
                }
                using var effect=await _http.PutAsJsonAsync(_razer.AbsoluteUri+"/keyboard",new { effect="CHROMA_STATIC",param=new { color=red | green<<8 | blue<<16 } },token);effect.EnsureSuccessStatusCode();
                using var data=JsonDocument.Parse(await effect.Content.ReadAsStringAsync(token));if(data.RootElement.TryGetProperty("result",out var result) && result.GetInt32()!=0) throw new InvalidOperationException("Chroma keyboard effect unavailable.");
            }
            else if(adapter=="steelseries")
            {
                var path=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),"SteelSeries","SteelSeries Engine 3","coreProps.json");
                using var properties=JsonDocument.Parse(await File.ReadAllTextAsync(path,token));
                var uri=new Uri("http://"+properties.RootElement.GetProperty("address").GetString());if(!uri.IsLoopback) throw new InvalidDataException("GameSense endpoint must be local.");_steelSeries=uri;
                await PostSteel("game_metadata",new { game="MAKRIVO",game_display_name="MAKRIVO",developer="Aivana" },token);
                await PostSteel("bind_game_event",new { game="MAKRIVO",@event="STATUS",min_value=0,max_value=100,handlers=new[] { new Dictionary<string,object> { ["device-type"]="keyboard",["zone"]="function-keys",["mode"]="color",["color"]=new { red,green,blue } } } },token);
                await PostSteel("game_event",new { game="MAKRIVO",@event="STATUS",data=new { value=100 } },token);
            }
            else if(adapter=="logitech-led")
            {
                if(string.IsNullOrEmpty(approvedHash)) throw new InvalidOperationException("Logitech LED SDK zuerst freigeben.");
                if(_logitech==null)
                {
                    _logitech=new(adapter,approvedHash);_shutdown=_logitech.Get<LedShutdown>("LogiLedShutdown");
                    if(!_logitech.Get<LedInitialize>("LogiLedInit")()) { _logitech.Dispose();_logitech=null;throw new InvalidOperationException("Logitech LED SDK unavailable."); }
                }
                if(!_logitech.Get<LedSet>("LogiLedSetLighting")(red*100/255,green*100/255,blue*100/255)) throw new InvalidOperationException("Logitech lighting command failed.");
            }
            else throw new ArgumentException("Unknown lighting adapter.");
            if((_heartbeatTask==null || _heartbeatTask.IsCompleted) && (_razer!=null || _steelSeries!=null)) { _heartbeat?.Dispose(); _heartbeat=new(); _heartbeatTask=Heartbeat(_heartbeat.Token); }
        }
        finally { _gate.Release(); }
    }
    private async Task PostSteel(string route,object data,CancellationToken token) { using var response=await _http.PostAsJsonAsync(new Uri(_steelSeries!,route),data,token);response.EnsureSuccessStatusCode(); }
    private async Task Heartbeat(CancellationToken token)
    {
        try
        {
            while(!token.IsCancellationRequested)
            {
                await Task.Delay(1000,token); await _gate.WaitAsync(token);
                try
                {
                    if(_razer!=null) { using var response=await _http.PutAsync(_razer.AbsoluteUri+"/heartbeat",null,token); response.EnsureSuccessStatusCode(); }
                    if(_steelSeries!=null) await PostSteel("game_heartbeat",new { game="MAKRIVO" },token);
                }
                catch { _razer=null; _steelSeries=null; throw; }
                finally { _gate.Release(); }
            }
        }
        catch(Exception) { /* A later action creates a fresh session and heartbeat task. */ }
    }
    public void Dispose() { _heartbeat?.Cancel();_heartbeat?.Dispose();_shutdown?.Invoke();_logitech?.Dispose();_http.Dispose(); }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.Bool)] private delegate bool LedInitialize();
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.Bool)] private delegate bool LedSet(int red,int green,int blue);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void LedShutdown();
}

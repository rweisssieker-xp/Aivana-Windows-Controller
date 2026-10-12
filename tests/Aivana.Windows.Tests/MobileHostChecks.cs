using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
internal static class MobileHostChecks
{
    public static async Task RunAsync()
    {
        var folder=Path.Combine(Path.GetTempPath(),"MakrivoMobileTest-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(folder);
        using var process=new Process { StartInfo=new(Path.Combine(Environment.GetEnvironmentVariable("MAKRIVO_TEST_HOST_DIR") ?? AppContext.BaseDirectory,"Aivana.MobileHost.exe")) { UseShellExecute=false,CreateNoWindow=true,RedirectStandardOutput=true,RedirectStandardError=true } };
        process.StartInfo.Environment["MAKRIVO_MOBILE_DATA"]=folder;
        process.StartInfo.Environment["MAKRIVO_MOBILE_DIAGNOSTICS"]="1";
        Task<string>? diagnostic = null;
        foreach(var argument in new[] { "--pipe","missing-test-controller","--bind","127.0.0.1" })process.StartInfo.ArgumentList.Add(argument);
        try
        {
            process.Start(); diagnostic = process.StandardError.ReadToEndAsync(); var line=await process.StandardOutput.ReadLineAsync().WaitAsync(TimeSpan.FromSeconds(15));
            if(line==null)throw new Exception("Mobile host failed: "+await process.StandardError.ReadToEndAsync());
            using var ready=JsonDocument.Parse(line);var fingerprint=ready.RootElement.GetProperty("fingerprint").GetString();var code=ready.RootElement.GetProperty("code").GetString();
            using var handler=new HttpClientHandler { CookieContainer=new CookieContainer(),UseProxy=false,ServerCertificateCustomValidationCallback=(_,cert,_,_)=>cert?.GetCertHashString(HashAlgorithmName.SHA256)==fingerprint };
            using var http=new HttpClient(handler) { BaseAddress=new Uri("https://127.0.0.1:47831"),Timeout=TimeSpan.FromSeconds(5) };
            var response=await http.GetAsync("/api/state");Require(response.StatusCode==HttpStatusCode.Unauthorized,"Unpaired snapshot blocked");
            response=await http.PostAsJsonAsync("/api/pair",new { Code="wrong" });Require(response.StatusCode==HttpStatusCode.Unauthorized,"Wrong pairing code blocked");
            response=await http.PostAsJsonAsync("/api/pair",new { Code=code });Require(response.IsSuccessStatusCode,"HTTPS pairing");
            response=await http.PostAsJsonAsync("/api/pair",new { Code=code });Require(response.StatusCode==HttpStatusCode.Unauthorized,"One-time code");
            using var crossOrigin=new HttpRequestMessage(HttpMethod.Post,"/api/command") { Content=JsonContent.Create(new {}) };crossOrigin.Headers.Add("Origin","https://attacker.invalid");
            response=await http.SendAsync(crossOrigin);Require(response.StatusCode==HttpStatusCode.Forbidden,"Cross-origin commands blocked");
            var html=await http.GetStringAsync("/");Require(html.Contains("MAKRIVO"),"Mobile assets packaged");
            var saved=File.ReadAllText(Path.Combine(folder,"clients.json"));var token=handler.CookieContainer.GetCookies(http.BaseAddress)["makrivo"]?.Value;Require(token!=null && !saved.Contains(token),"Only token hashes persisted");
            Require(Directory.GetFiles(folder,"certificate-*.bin").Length==1,"Protected TLS key persisted");
            Console.WriteLine("PASS real HTTPS host, pinned certificate, pairing, origin checks and mobile assets");
        }
        catch { if(!process.HasExited)process.Kill(true); if(diagnostic != null) Console.Error.WriteLine(await diagnostic); throw; }
        finally { if(!process.HasExited) { process.Kill(true);await process.WaitForExitAsync(); }Directory.Delete(folder,true); }
    }
    private static void Require(bool condition,string message){if(!condition)throw new Exception(message);}
}

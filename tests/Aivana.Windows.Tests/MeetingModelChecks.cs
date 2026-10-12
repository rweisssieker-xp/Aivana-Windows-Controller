using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Aivana.Core;
using Aivana.Windows;
internal static class MeetingModelChecks
{
    public static async Task RunAsync()
    {
        var handler=new Fake();using var client=new MeetingModelClient(handler);
        var resources=new[] { new MeetingResource("app","Teams",MeetingResourceKind.Program,@"C:\Apps\Teams.exe") };
        var plan=await client.PlanAsync(true,"Meeting vorbereiten","local:latest",resources,CancellationToken.None);
        if(MeetingPlanner.Compile(plan,resources).Steps.Count!=1 || !handler.Checked)throw new Exception("Structured local model planning failed");
        var blocked=false;try { await client.PlanAsync(false,"goal","local",resources,CancellationToken.None); }catch(ArgumentException){blocked=true;}if(!blocked)throw new Exception("Feature gate bypassed");
        blocked=false;try { await client.PlanAsync(true,"goal","model:cloud",resources,CancellationToken.None); }catch(ArgumentException){blocked=true;}if(!blocked)throw new Exception("Cloud model accepted");
        handler.Remote=true;blocked=false;try { await client.PlanAsync(true,"goal","local-alias",resources,CancellationToken.None); }catch(ArgumentException){blocked=true;}if(!blocked)throw new Exception("Remote alias accepted");handler.Remote=false;
        handler.Malformed=true;blocked=false;try { await client.PlanAsync(true,"goal","local",resources,CancellationToken.None); }catch(JsonException){blocked=true;}if(!blocked)throw new Exception("Malformed model output accepted");handler.Malformed=false;
        handler.Huge=true;blocked=false;try { await client.PlanAsync(true,"goal","local",resources,CancellationToken.None); }catch(InvalidDataException){blocked=true;}if(!blocked)throw new Exception("Oversized response accepted");
        handler.Huge=false;using var cancelled=new CancellationTokenSource();cancelled.Cancel();blocked=false;try { await client.PlanAsync(true,"goal","local",resources,cancelled.Token); }catch(OperationCanceledException){blocked=true;}if(!blocked)throw new Exception("Cancellation ignored");
        Console.WriteLine("PASS bounded local model transport, cloud rejection, payload limit and cancellation (fake model)");
    }
    private sealed class Fake:HttpMessageHandler
    {
        public bool Checked,Huge,Remote,Malformed;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)
        {
            if(request.RequestUri!.Host!="127.0.0.1")throw new Exception("Nonlocal endpoint");
            if(request.RequestUri.AbsolutePath=="/api/show")return new(HttpStatusCode.OK){Content=new StringContent(Remote?"{\"remote_host\":\"https://remote.invalid\"}":"{}")};
            using var body=JsonDocument.Parse(await request.Content!.ReadAsStringAsync(token));
            if(body.RootElement.GetProperty("stream").GetBoolean() || body.RootElement.GetProperty("format").ValueKind!=JsonValueKind.Object)throw new Exception("Schema/budget missing");
            if((await request.Content.ReadAsStringAsync(token)).Contains("C:\\\\Apps"))throw new Exception("Filesystem target sent to model");
            Checked=true;
            return new(HttpStatusCode.OK) { Content=new StringContent(Huge?new string('x',70000):Malformed?"{not-json}":JsonSerializer.Serialize(new {done=true,message=new {content="{\"name\":\"Meeting\",\"resourceIds\":[\"app\"],\"muteMicrophone\":false,\"unresolved\":[]}"}}),Encoding.UTF8,"application/json") };
        }
    }
}

using Aivana.Core;
using System.Text.Json;
internal static class MeetingAssistantChecks
{
    public static Task RunAsync()
    {
        var resources=new[] { new MeetingResource("app","Teams",MeetingResourceKind.Program,@"C:\Apps\Teams.exe"), new MeetingResource("doc","Agenda",MeetingResourceKind.Document,@"C:\Docs\Agenda.pdf") };
        var plan=new MeetingPlan("Besprechung",["app","doc"],true,[]);
        var tile=MeetingPlanner.Compile(plan,resources);
        Require(tile.Steps.Select(s=>s.Kind).SequenceEqual(new[] { ActionKind.Launch,ActionKind.OpenPath,ActionKind.Volume }),"Only allowlisted action kinds");
        Require(tile.Hotkey=="" && tile.Steps[0].Arguments=="" && tile.Repeat==1,"No injected execution options");
        Reject(()=>MeetingPlanner.Compile(plan with { ResourceIds=["unknown"] },resources));
        Reject(()=>MeetingPlanner.Compile(plan with { ResourceIds=["app","app"] },resources));
        Reject(()=>MeetingPlanner.Compile(plan with { ResourceIds=[],MuteMicrophone=false },resources));
        Reject(()=>MeetingPlanner.Compile(plan,[resources[0],resources[1] with { Target=@"C:\Docs\evil.ps1" }]));
        Reject(()=>MeetingPlanner.Compile(plan,[resources[0],resources[1] with { Kind=MeetingResourceKind.Website,Target="file:///C:/bad" }]));
        Reject(()=>MeetingPlanner.Parse("{\"name\":\"Meeting\",\"resourceIds\":[\"app\"],\"muteMicrophone\":false,\"unresolved\":[],\"command\":\"evil\"}"));
        Reject(()=>MeetingPlanner.ValidateRequest(false,"goal",resources));
        Reject(()=>MeetingPlanner.ValidateRequest(true,new string('x',2001),resources));
        var longUrl="https://meet.example/meeting?token="+new string('a',300);
        var website=MeetingPlanner.WebsiteResource("web",longUrl);
        Require(website.Target==longUrl && website.Name.Length<=200,"Long meeting links retain full target");
        var round=MeetingPlanner.Parse("{\"name\":\"Meeting\",\"resourceIds\":[\"app\"],\"muteMicrophone\":false,\"unresolved\":[\"Headsetwechsel nicht unterstützt\"]}");
        Require(round.Unresolved.Length==1,"Unresolved requirements retained");
        Require(!new AppSettings().MeetingAssistantEnabled,"Feature defaults off");
        var folder=Path.Combine(Path.GetTempPath(),"MeetingMetrics-"+Guid.NewGuid().ToString("N"));
        try
        {
            var settings=ConfigStore.CreateDefaults();settings.MeetingAssistantEnabled=true;
            var store=new ConfigStore(folder);store.Save(settings);
            Require(store.Load().MeetingAssistantEnabled,"Feature flag persists locally");
            using(var locked=new FileStream(Path.Combine(folder,"settings.json"),FileMode.Open,FileAccess.Read,FileShare.None))
            {
                var release=Task.Run(async()=>{await Task.Delay(150);locked.Dispose();});
                store.Save(settings);release.GetAwaiter().GetResult();
            }
            Require(store.Load().MeetingAssistantEnabled,"Atomic save survives a brief reader lock");
            Require(!store.ReadImport(Path.Combine(folder,"settings.json")).MeetingAssistantEnabled,"Imported configuration cannot opt into AI");
            Require(!ProfileSync.PreparePortable(settings,out _,out _).MeetingAssistantEnabled,"Portable configuration cannot opt into AI");
            var metrics=new MeetingExperiment(folder);metrics.Record(Guid.NewGuid(),tile.Id,"draft_saved",100,true);
            var logged=File.ReadAllText(metrics.PathName);
            Require(!logged.Contains("Teams") && !logged.Contains("Agenda") && logged.Contains("draft_saved"),"Metrics omit resource content");
            var previews=new MeetingPreviewTracker(metrics,Environment.TickCount64);
            previews.Set(Guid.NewGuid(),tile.Id);previews.Discard();previews.Discard();
            Require(File.ReadLines(metrics.PathName).Count(line=>line.Contains("draft_discarded"))==1,"Preview rejection recorded exactly once");
            previews.Set(Guid.NewGuid(),tile.Id);previews.Handoff();previews.Discard();
            Require(File.ReadLines(metrics.PathName).Count(line=>line.Contains("draft_discarded"))==1,"Editor handoff not counted as discard");
            Reject(()=>metrics.Record(Guid.NewGuid(),null,"goal-secret"));
        }
        finally { if(Directory.Exists(folder))Directory.Delete(folder,true); }
        return Task.CompletedTask;
    }
    private static void Reject(Action action) { try { action(); } catch (Exception ex) when(ex is ArgumentException or JsonException or InvalidDataException) { return; } throw new Exception("Unsafe plan accepted"); }
    private static void Require(bool value,string message) { if(!value)throw new Exception(message); }
}

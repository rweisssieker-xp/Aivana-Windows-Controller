using System.Text.Json;
using System.Text.Json.Serialization;
namespace Aivana.Core;
public enum MeetingResourceKind { Program, Document, Website }
public sealed record MeetingResource(string Id,string Name,MeetingResourceKind Kind,string Target);
public sealed record MeetingPlan(string Name,string[] ResourceIds,bool MuteMicrophone,string[] Unresolved);
public static class MeetingPlanner
{
    public static MeetingResource WebsiteResource(string id,string target)
    {
        target=target.Trim();
        if(!Uri.TryCreate(target,UriKind.Absolute,out var uri))throw new ArgumentException("Ungültige Webseite.");
        var label=uri.GetLeftPart(UriPartial.Authority);if(label.Length>200)label=label[..197]+"…";
        var resource=new MeetingResource(id,label,MeetingResourceKind.Website,target);ValidateResources([resource]);return resource;
    }
    public static void ValidateRequest(bool enabled,string goal,IReadOnlyList<MeetingResource> resources)
    {
        if(!enabled) throw new ArgumentException("Besprechungsassistent ist deaktiviert.");
        if(string.IsNullOrWhiteSpace(goal) || goal.Length>2000) throw new ArgumentException("Ziel mit 1 bis 2000 Zeichen eingeben.");
        ValidateResources(resources);
    }
    private static void ValidateResources(IReadOnlyList<MeetingResource> resources)
    {
        if(resources.Count>24 || resources.Select(r=>r.Id).Distinct(StringComparer.Ordinal).Count()!=resources.Count) throw new ArgumentException("Maximal 24 eindeutige Ressourcen zulassen.");
        foreach(var r in resources)
        {
            if(string.IsNullOrWhiteSpace(r.Id) || r.Id.Length>64 || string.IsNullOrWhiteSpace(r.Name) || r.Name.Length>200 || string.IsNullOrWhiteSpace(r.Target) || r.Target.Length>4096) throw new ArgumentException("Ungültige Ressource.");
            if(r.Kind==MeetingResourceKind.Website)
            {
                if(!Uri.TryCreate(r.Target,UriKind.Absolute,out var uri) || uri.Scheme is not ("https" or "http") || uri.UserInfo.Length>0) throw new ArgumentException("Nur HTTP(S)-Webseiten ohne Zugangsdaten zulassen.");
            }
            else
            {
                if(!Path.IsPathFullyQualified(r.Target) || r.Target.StartsWith(@"\\") || r.Target.StartsWith("//") || r.Target[2..].Contains(':')) throw new ArgumentException("Nur lokale absolute Dateipfade zulassen.");
                var extension=Path.GetExtension(r.Target).ToLowerInvariant();
                if(r.Kind==MeetingResourceKind.Program ? extension!=".exe" : r.Kind!=MeetingResourceKind.Document || extension is not (".pdf" or ".docx" or ".xlsx" or ".pptx" or ".txt" or ".md")) throw new ArgumentException("Dokumenttyp nicht unterstützt; keine Skripte oder Verknüpfungen zulassen.");
            }
        }
    }
    public static MeetingPlan Parse(string json)
    {
        if(json.Length>16384) throw new ArgumentException("Modellantwort zu groß.");
        return JsonSerializer.Deserialize<MeetingPlan>(json,new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase,UnmappedMemberHandling=JsonUnmappedMemberHandling.Disallow,RespectRequiredConstructorParameters=true }) ?? throw new JsonException("Leerer Plan.");
    }
    public static Tile Compile(MeetingPlan plan,IReadOnlyList<MeetingResource> resources)
    {
        ValidateResources(resources);
        if(string.IsNullOrWhiteSpace(plan.Name) || plan.Name.Length>100 || plan.ResourceIds==null || plan.Unresolved==null || plan.ResourceIds.Length>12 || plan.Unresolved.Length>10 || plan.Unresolved.Any(s=>s==null || s.Length>300) || plan.ResourceIds.Distinct(StringComparer.Ordinal).Count()!=plan.ResourceIds.Length) throw new ArgumentException("Ungültiger Besprechungsplan.");
        var tile=new Tile { Name=plan.Name.Trim() };
        foreach(var id in plan.ResourceIds)
        {
            var r=resources.SingleOrDefault(r=>r.Id==id) ?? throw new ArgumentException("Modell hat eine nicht freigegebene Ressource gewählt.");
            tile.Steps.Add(new() { Kind=r.Kind switch { MeetingResourceKind.Program=>ActionKind.Launch,MeetingResourceKind.Document=>ActionKind.OpenPath,_=>ActionKind.Url },Value=r.Target,Verification=new(true) });
        }
        if(plan.MuteMicrophone) tile.Steps.Add(new() { Kind=ActionKind.Volume,Value="MicMuteOn",Verification=new(true) });
        if(tile.Steps.Count==0) throw new ArgumentException("Kein unterstützter Schritt. Ressourcen ergänzen oder Ziel anpassen.");
        var errors=ConfigurationValidator.ValidateTile(tile);if(errors.Count>0)throw new ArgumentException(string.Join("\n",errors));
        return tile;
    }
    public static string Schema => """
    {"type":"object","additionalProperties":false,"properties":{"name":{"type":"string","maxLength":100},"resourceIds":{"type":"array","maxItems":12,"uniqueItems":true,"items":{"type":"string"}},"muteMicrophone":{"type":"boolean"},"unresolved":{"type":"array","maxItems":10,"items":{"type":"string","maxLength":300}}},"required":["name","resourceIds","muteMicrophone","unresolved"]}
    """;
}

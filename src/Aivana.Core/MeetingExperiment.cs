using System.Text.Json;
namespace Aivana.Core;
public sealed record MeetingMeasurement(DateTimeOffset Time,Guid Attempt,Guid? Tile,string Event,long Milliseconds,bool? Corrected=null);
public sealed class MeetingExperiment(string directory)
{
    private readonly object _gate=new();
    public string PathName=>Path.Combine(directory,"meeting-experiment.jsonl");
    public string? Error { get; private set; }
    public void Record(Guid attempt,Guid? tile,string eventName,long milliseconds=0,bool? corrected=null)
    {
        if(eventName is not ("manual_baseline" or "generated" or "generation_failed" or "generation_cancelled" or "draft_saved" or "draft_discarded" or "run_completed" or "run_failed" or "run_cancelled" or "run_rejected")) throw new ArgumentException("Unknown measurement event");
        lock(_gate)
        {
            try
            {
                Directory.CreateDirectory(directory);
                if(File.Exists(PathName) && new FileInfo(PathName).Length>2*1024*1024)File.Move(PathName,PathName+".previous",true);
                File.AppendAllText(PathName,JsonSerializer.Serialize(new MeetingMeasurement(DateTimeOffset.UtcNow,attempt,tile,eventName,Math.Max(0,milliseconds),corrected))+Environment.NewLine);
                Error=null;
            }
            catch(Exception ex) when(ex is IOException or UnauthorizedAccessException) { Error="Lokale Messung konnte nicht gespeichert werden."; }
        }
    }
}

public sealed class MeetingPreviewTracker(MeetingExperiment metrics,long started)
{
    private (Guid Attempt,Guid Tile)? _pending;
    public void Set(Guid attempt,Guid tile) { Discard();_pending=(attempt,tile); }
    public void Handoff() => _pending=null;
    public void Discard()
    {
        if(_pending is not {} value)return;
        _pending=null;
        metrics.Record(value.Attempt,value.Tile,"draft_discarded",Environment.TickCount64-started);
    }
}

using Aivana.Core;
namespace Aivana.Desktop;
public sealed partial class DesktopController
{
    private void RecordMeetingExecution(ExecutionProgress progress)
    {
        if(progress.Phase is not (ExecutionPhase.Completed or ExecutionPhase.Failed or ExecutionPhase.Cancelled or ExecutionPhase.Rejected))return;
        // Read settings on the UI thread; runner callbacks can arrive on workers.
        System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            if(_disposed)return;
            var tile=Settings.Profiles.SelectMany(p=>p.Tiles).FirstOrDefault(t=>t.Id==progress.TileId);
            if(tile?.MeetingExperimentId is not Guid attempt)return;
            var eventName=progress.Phase switch { ExecutionPhase.Completed=>"run_completed",ExecutionPhase.Failed=>"run_failed",ExecutionPhase.Cancelled=>"run_cancelled",_=>"run_rejected" };
            MeetingExperiment.Record(attempt,tile.Id,eventName);
        }));
    }
}

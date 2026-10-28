#if MAKRIVO_STORE_NO_AI
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void RecordMeetingDraft(bool saved) { }
    internal void RunMeetingDraftSmokeChecks() => throw new System.NotSupportedException("The AI assistant is not included in this Store release.");
}
#endif

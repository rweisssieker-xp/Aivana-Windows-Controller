using Aivana.Core;
namespace Aivana.Desktop;
public sealed partial class DesktopController
{
    private readonly List<StepVerification> _verificationHistory = [];
    public IReadOnlyList<StepVerification> VerificationHistory { get { lock (_verificationHistory) return _verificationHistory.ToArray(); } }
    private void RecordVerification(StepVerification result)
    {
        lock (_verificationHistory) { _verificationHistory.Add(result); if (_verificationHistory.Count > 500) _verificationHistory.RemoveAt(0); }
    }
}

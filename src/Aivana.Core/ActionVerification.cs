namespace Aivana.Core;

public enum VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }
public sealed record VerificationOptions(bool Enabled = false, int TimeoutMs = 5000);
public sealed record ActionVerificationResult(VerificationState State, string Code, string Message);
public sealed record StepVerification(Guid RunId, Guid TileId, int Step, ActionVerificationResult Result);
public interface IActionVerificationProvider
{
    Task<ActionVerificationResult> PreflightAsync(ActionStep step, CancellationToken token);
    Task<ActionVerificationResult> VerifyAsync(ActionStep step, CancellationToken token);
}

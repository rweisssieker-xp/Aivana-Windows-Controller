using Aivana.Core;

internal static class VerificationChecks
{
    public static async Task RunAsync()
    {
        var executor = new Probe();
        var runner = new MacroRunner(executor);
        var results = new List<StepVerification>();
        runner.VerificationChanged += results.Add;
        var tile = new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "secret", Verification = new() { Enabled = true } }] };
        executor.Reject = true;
        await runner.RunAsync(tile);
        Require(executor.Calls == 0 && results.Last().Result.State == VerificationState.Failed, "Preflight must prevent execution");
        executor.Reject = false;
        await runner.RunAsync(tile);
        Require(executor.Calls == 1 && results.Last().Result.State == VerificationState.Verified, "Verify actual result");
        executor.Throw = true;
        await runner.RunAsync(tile);
        Require(results.Last().Result.State == VerificationState.Failed, "Execution failure must replace successful preflight");
        executor.Throw = false;
        Require(!System.Text.Json.JsonSerializer.Serialize(results).Contains("secret"), "History must omit payloads");
        tile.Steps[0].Verification = new(true, 0);
        Require(ConfigurationValidator.ValidateTile(tile).Count > 0, "Reject invalid timeout");
        tile.Steps[0].Verification = null;
        Require(ConfigurationValidator.ValidateTile(tile).Count == 0, "Legacy options stay optional");
    }
    private static void Require(bool value, string message) { if (!value) throw new InvalidOperationException(message); }
    private sealed class Probe : IActionExecutor, IActionVerificationProvider
    {
        public bool Throw; public bool Reject; public int Calls;
        public Task ExecuteAsync(ActionStep step, CancellationToken token) { Calls++; if (Throw) throw new InvalidOperationException("secret execution error"); return Task.CompletedTask; }
        public Task<ActionVerificationResult> PreflightAsync(ActionStep step, CancellationToken token) => Task.FromResult(new ActionVerificationResult(Reject ? VerificationState.Failed : VerificationState.Completed, "preflight", "ready"));
        public Task<ActionVerificationResult> VerifyAsync(ActionStep step, CancellationToken token) => Task.FromResult(new ActionVerificationResult(VerificationState.Verified, "state", "verified"));
    }
}

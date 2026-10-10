using Aivana.Core;

internal static class WindowWaitChecks
{
    public static async Task RunAsync()
    {
        Check(WindowWait.Matches("notepad", "Project Notes", "NOTEPAD.exe", "notes"), "process extension and case should normalize");
        Check(!WindowWait.Matches("notepad", "Notes", "obs64", "Notes"), "both filters must match");
        Check(WindowWait.Matches("notepad", "Notes", "", "Notes"), "title-only waits should work");
        Check(!WindowWait.Matches("notepad", "Notes", "", ""), "empty filters must not match arbitrary windows");
        Check(WindowWait.Matches("Aivana.Windows.Tests", "Fixture", "Aivana.Windows.Tests", "Fixture"), "dots in process names must not be removed as extensions");
        var probes = 0;
        await WindowWait.UntilAsync(() => ++probes == 2, 1000, CancellationToken.None);
        Check(probes == 2, "wait should resume when the target becomes ready");
        var timeout = false;
        try { await WindowWait.UntilAsync(() => false, 10, CancellationToken.None); }
        catch (TimeoutException) { timeout = true; }
        Check(timeout, "missing windows must time out");
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();
        probes = 0;
        try { await WindowWait.UntilAsync(() => { probes++; return true; }, 1000, cancellation.Token); throw new Exception("Cancellation ignored"); }
        catch (OperationCanceledException) { }
        Check(probes == 0, "cancelled waits must not inspect or activate windows");
        using var duringWait = new CancellationTokenSource(10);
        try { await WindowWait.UntilAsync(() => false, 10000, duringWait.Token); throw new Exception("Cancellation ignored"); }
        catch (OperationCanceledException) { }
        var executor = new TimeoutExecutor();
        var runner = new MacroRunner(executor);
        var failed = false;
        runner.ProgressChanged += progress => failed |= progress.Phase == ExecutionPhase.Failed;
        await runner.RunAsync(new Tile { Steps = [new() { Kind = ActionKind.WaitWindow, Value = "missing", DelayMs = 10 }, new() { Kind = ActionKind.Text, Value = "must not type" }] });
        Check(failed && executor.Calls == 1, "timed-out wait must stop the macro before subsequent input");
        var encoded = System.Text.Json.JsonSerializer.Serialize(new ActionStep { Kind = ActionKind.WaitWindow, Value = "notepad", Arguments = "Notes", DelayMs = 12345 });
        var decoded = System.Text.Json.JsonSerializer.Deserialize<ActionStep>(encoded)!;
        Check(decoded.Kind == ActionKind.WaitWindow && decoded.DelayMs == 12345 && decoded.Arguments == "Notes", "window filters and timeout must persist");
    }
    private static void Check(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
    private sealed class TimeoutExecutor : IActionExecutor
    {
        public int Calls;
        public Task ExecuteAsync(ActionStep step, CancellationToken token) { Calls++; return WindowWait.UntilAsync(() => false, step.DelayMs, token); }
    }
}

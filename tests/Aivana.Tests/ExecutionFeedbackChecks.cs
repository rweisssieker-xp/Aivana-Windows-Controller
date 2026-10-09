using Aivana.Core;

internal static class ExecutionFeedbackChecks
{
    internal static async Task RunAsync()
    {
        var runner = new MacroRunner(new FeedbackExecutor());
        var events = new List<ExecutionProgress>();
        runner.ProgressChanged += events.Add;
        runner.ProgressChanged += _ => throw new InvalidOperationException("broken feedback listener");
        var tile = new Tile { Name = "private-title", Repeat = 2, Steps = [new() { Kind = ActionKind.Text, Value = "private-content" }] };
        await runner.RunAsync(tile);
        Check(events.Select(e => e.Phase).SequenceEqual(new[] { ExecutionPhase.Waiting, ExecutionPhase.Running, ExecutionPhase.Step, ExecutionPhase.Step, ExecutionPhase.Completed }), "Typed execution phases are incorrect.");
        Check(events[3] is { Step: 1, Steps: 1, Repetition: 2, Repetitions: 2 }, "Typed progress lost step or repetition.");
        Check(!string.Join(" ", events).Contains("private", StringComparison.Ordinal), "Feedback contains action content.");
        events.Clear();
        tile.Steps[0].Value = "fail";
        await runner.RunAsync(tile);
        Check(events.Last().Phase == ExecutionPhase.Failed && events.All(e => e.Phase != ExecutionPhase.Completed), "Failed execution reported completion.");
        events.Clear();
        tile.Steps = [new() { Kind = ActionKind.Delay, DelayMs = 1000 }];
        using var cancelled = new CancellationTokenSource();
        cancelled.Cancel();
        await runner.RunAsync(tile, cancelled.Token);
        Check(events.Last().Phase == ExecutionPhase.Cancelled, "Cancelled execution did not report cancellation.");
        events.Clear();
        tile.Enabled = false;
        await runner.RunAsync(tile);
        Check(events.Single().Phase == ExecutionPhase.Rejected, "Disabled execution did not report rejection.");
        using var lastStepCancellation = new CancellationTokenSource();
        var cancellingRunner = new MacroRunner(new LastStepCancelExecutor(lastStepCancellation));
        var terminal = new List<ExecutionProgress>();
        cancellingRunner.ProgressChanged += terminal.Add;
        await cancellingRunner.RunAsync(new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "test" }] }, lastStepCancellation.Token);
        Check(terminal.Last().Phase == ExecutionPhase.Cancelled && terminal.All(progress => progress.Phase != ExecutionPhase.Completed),
            "Cancellation during the final step must not report completion.");
    }

    private static void Check(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
    private sealed class LastStepCancelExecutor(CancellationTokenSource cancellation) : IActionExecutor
    {
        public Task ExecuteAsync(ActionStep step, CancellationToken token) { cancellation.Cancel(); return Task.CompletedTask; }
    }
    private sealed class FeedbackExecutor : IActionExecutor
    {
        public Task ExecuteAsync(ActionStep step, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (step.Value == "fail") throw new InvalidOperationException("private failure reason");
            return Task.CompletedTask;
        }
    }
}

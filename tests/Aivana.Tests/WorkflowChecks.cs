using Aivana.Core;

internal static class WorkflowChecks
{
    public static async Task RunAsync()
    {
        var executor = new Executor(); var runner = new MacroRunner(executor);
        string finalStatus = "";
        runner.StatusChanged += (_, status) => finalStatus = status;
        var tile = new Tile { Steps = [
            new() { Kind = ActionKind.Text, Value = "skip", Condition = new(WorkflowConditionKind.FileExists, "no"), OnConditionFalseSkip = 1 },
            new() { Kind = ActionKind.Text, Value = "also skip" },
            new() { Kind = ActionKind.Text, Value = "yes", Condition = new(WorkflowConditionKind.ProcessRunning, "no", true) },
            new() { Kind = ActionKind.Text, Value = "fail", OnError = StepErrorPolicy.Continue },
            new() { Kind = ActionKind.Text, Value = "last" } ] };
        await runner.RunAsync(tile);
        Check(executor.Values.SequenceEqual(new[] { "yes", "fail", "last" }), "conditional skip, negation and error continuation must execute correctly");
        Check(finalStatus.Contains("1 übersprungenen Fehlern"), "continued failures must remain visible in final status");
        executor.Values.Clear(); tile.Steps[3].OnError = StepErrorPolicy.Stop;
        await runner.RunAsync(tile);
        Check(executor.Values.SequenceEqual(new[] { "yes", "fail" }), "stop policy must stop downstream steps");
        tile.Steps[0].OnConditionFalseSkip = 99;
        Check(ConfigurationValidator.ValidateTile(tile).Count > 0, "out of range branch must not save");
        tile.Steps[0].OnConditionFalseSkip = -1;
        Check(ConfigurationValidator.ValidateTile(tile).Count > 0, "backward branches must not save");
        var step = new ActionStep { Condition = new(WorkflowConditionKind.WindowTitleContains, "Report", true), OnError = StepErrorPolicy.Continue, OnConditionFalseSkip = 2 };
        var restored = System.Text.Json.JsonSerializer.Deserialize<ActionStep>(System.Text.Json.JsonSerializer.Serialize(step))!;
        Check(restored.Condition == step.Condition && restored.OnError == step.OnError && restored.OnConditionFalseSkip == 2, "workflow control must survive persistence");
        var delayed = new DelayedEvaluator(); var delayedRunner = new MacroRunner(delayed);
        var original = new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "snapshot", Condition = new(WorkflowConditionKind.FileExists, "original", true) }] };
        var execution = delayedRunner.RunAsync(original);
        await delayed.Entered.Task;
        original.Steps[0].Condition = new(WorkflowConditionKind.FileExists, "edited", false);
        original.Steps[0].OnError = StepErrorPolicy.Continue;
        delayed.Release.TrySetResult();
        await execution;
        Check(delayed.ConditionValue == "original" && delayed.ExecutionCount == 1, "workflow condition must be captured before editing queued run");
        delayed = new DelayedEvaluator(); delayedRunner = new MacroRunner(delayed);
        using var cancel = new CancellationTokenSource();
        execution = delayedRunner.RunAsync(original, cancel.Token);
        await delayed.Entered.Task; cancel.Cancel(); await execution;
        Check(delayed.ExecutionCount == 0, "cancellation during condition must stop execution");
    }
    private static void Check(bool value, string message) { if (!value) throw new InvalidOperationException(message); }
    private sealed class Executor : IActionExecutor, IWorkflowConditionEvaluator
    {
        public List<string> Values { get; } = [];
        public Task<bool> EvaluateAsync(WorkflowCondition condition, CancellationToken token) { token.ThrowIfCancellationRequested(); return Task.FromResult(false); }
        public Task ExecuteAsync(ActionStep step, CancellationToken token)
        {
            Values.Add(step.Value);
            if (step.Value == "fail") throw new IOException("expected test failure");
            return Task.CompletedTask;
        }
    }
    private sealed class DelayedEvaluator : IActionExecutor, IWorkflowConditionEvaluator
    {
        public TaskCompletionSource Entered { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public TaskCompletionSource Release { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public string? ConditionValue { get; private set; }
        public int ExecutionCount { get; private set; }
        public async Task<bool> EvaluateAsync(WorkflowCondition condition, CancellationToken token)
        {
            ConditionValue = condition.Value; Entered.TrySetResult();
            await Release.Task.WaitAsync(token); return false;
        }
        public Task ExecuteAsync(ActionStep step, CancellationToken token) { ExecutionCount++; return Task.CompletedTask; }
    }
}

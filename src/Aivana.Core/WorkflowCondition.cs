namespace Aivana.Core;

public enum WorkflowConditionKind { FileExists, ProcessRunning, WindowTitleContains }
public enum StepErrorPolicy { Stop, Continue }
public sealed record WorkflowCondition(WorkflowConditionKind Kind, string Value, bool Negate = false);
public interface IWorkflowConditionEvaluator
{
    Task<bool> EvaluateAsync(WorkflowCondition condition, CancellationToken cancellationToken);
}

public static class WorkflowRules
{
    public static IEnumerable<string> Validate(ActionStep step, int remainingSteps)
    {
        if (!Enum.IsDefined(step.OnError)) yield return Localization.Translate("Ungültige Fehlerbehandlung im Ablauf.");
        if (step.OnConditionFalseSkip < 0 || step.OnConditionFalseSkip > remainingSteps)
            yield return Localization.Translate("Der Verzweigungssprung liegt außerhalb des Ablaufs.");
        if (step.Condition is { } condition && (!Enum.IsDefined(condition.Kind) || string.IsNullOrWhiteSpace(condition.Value)))
            yield return Localization.Translate("Eine Ablaufbedingung braucht einen gültigen Typ und einen Wert.");
    }
}

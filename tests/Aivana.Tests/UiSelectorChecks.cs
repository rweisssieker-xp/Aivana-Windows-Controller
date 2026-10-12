using Aivana.Core;
internal static class UiSelectorChecks
{
    public static Task RunAsync()
    {
        var valid = new UiAutomationAction(UiOperation.Invoke, new("notepad", "Save", "Button"));
        if (UiAutomationRules.Validate(valid).Count > 0) throw new Exception("Valid selector rejected");
        if (UiAutomationRules.Validate(valid with { Selector = new("", "", "") }).Count == 0) throw new Exception("Unbounded selector accepted");
        if (UiAutomationRules.Validate(valid with { TimeoutMs = 0 }).Count == 0) throw new Exception("Invalid timeout accepted");
        return Task.CompletedTask;
    }
}

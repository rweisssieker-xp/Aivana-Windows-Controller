namespace Aivana.Core;
public enum UiOperation { Invoke, SetValue, Select, Toggle, Focus, WaitState }
public sealed record UiAncestor(string AutomationId, string ControlType);
public sealed record UiElementSelector(string ProcessName, string AutomationId, string ControlType, string Name = "", UiAncestor[]? Ancestors = null);
public sealed record UiAutomationAction(UiOperation Operation, UiElementSelector Selector, string Value = "", int TimeoutMs = 5000);
public static class UiAutomationRules
{
    public static List<string> Validate(UiAutomationAction? action)
    {
        var errors = new List<string>();
        if (action == null || action.Selector == null) { errors.Add("UI-Element auswählen."); return errors; }
        if (!Enum.IsDefined(action.Operation)) errors.Add("Unbekannte UI-Aktion.");
        if (action.TimeoutMs is < 100 or > 60000) errors.Add("UI-Zeitlimit: 100–60000 ms.");
        if (string.IsNullOrWhiteSpace(action.Selector.ProcessName) || string.IsNullOrWhiteSpace(action.Selector.ControlType) || string.IsNullOrWhiteSpace(action.Selector.AutomationId) && string.IsNullOrWhiteSpace(action.Selector.Name)) errors.Add("Prozess, Elementtyp und ID oder Name erforderlich.");
        if (action.Operation == UiOperation.WaitState && action.Value is not ("Enabled" or "Exists" or "Off" or "On")) errors.Add("UI-Wartezustand: Enabled, Exists, On oder Off.");
        return errors;
    }
}

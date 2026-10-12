using System.Diagnostics;
using System.Windows.Automation;
using System.Windows;
using Aivana.Core;
using Condition = System.Windows.Automation.Condition;
namespace Aivana.Windows;

/// <summary>Runs only in a disposable worker process; providers may block inside COM.</summary>
public static class UiAutomationService
{
    public static UiElementSelector CaptureAt(double x, double y)
    {
        var element = AutomationElement.FromPoint(new Point(x, y));
        var current = element.Current;
        if (current.IsPassword) throw new InvalidOperationException("Passwortfelder sind ausgeschlossen.");
        using var process = Process.GetProcessById(current.ProcessId);
        if (process.ProcessName.Equals("MAKRIVO", StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Externes Programm auswählen.");
        var ancestors = new List<UiAncestor>();
        var parent = TreeWalker.ControlViewWalker.GetParent(element);
        for (var i = 0; i < 4 && parent != null && parent.Current.ProcessId == current.ProcessId; i++, parent = TreeWalker.ControlViewWalker.GetParent(parent))
            ancestors.Add(new(parent.Current.AutomationId, parent.Current.ControlType.ProgrammaticName.Replace("ControlType.", "")));
        return new(process.ProcessName, current.AutomationId, current.ControlType.ProgrammaticName.Replace("ControlType.", ""), current.Name, ancestors.ToArray());
    }

    public static List<AutomationElement> Resolve(UiElementSelector selector)
    {
        var result = new List<AutomationElement>();
        var processName = System.IO.Path.GetFileName(selector.ProcessName);
        if (processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) processName = processName[..^4];
        foreach (var process in Process.GetProcessesByName(processName))
        using (process)
        {
            var conditions = new List<Condition> { new PropertyCondition(AutomationElement.ProcessIdProperty, process.Id) };
            if (!string.IsNullOrEmpty(selector.AutomationId)) conditions.Add(new PropertyCondition(AutomationElement.AutomationIdProperty, selector.AutomationId));
            else conditions.Add(new PropertyCondition(AutomationElement.NameProperty, selector.Name));
            foreach (AutomationElement element in AutomationElement.RootElement.FindAll(TreeScope.Descendants, new AndCondition(conditions.ToArray())))
            {
                if (element.Current.IsPassword || element.Current.ControlType.ProgrammaticName != "ControlType." + selector.ControlType) continue;
                var parent = TreeWalker.ControlViewWalker.GetParent(element); var matches = true;
                foreach (var expected in selector.Ancestors ?? [])
                {
                    if (parent == null || parent.Current.ControlType.ProgrammaticName != "ControlType." + expected.ControlType || parent.Current.AutomationId != expected.AutomationId) { matches = false; break; }
                    parent = TreeWalker.ControlViewWalker.GetParent(parent);
                }
                if (matches) result.Add(element);
            }
        }
        return result;
    }

    public static async Task ExecuteAsync(UiAutomationAction action, CancellationToken token)
    {
        var errors = UiAutomationRules.Validate(action);
        if (errors.Count > 0) throw new ArgumentException(string.Join("\n", errors));
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token); timeout.CancelAfter(action.TimeoutMs);
        while (true)
        {
            timeout.Token.ThrowIfCancellationRequested();
            var matches = Resolve(action.Selector);
            if (matches.Count > 1) throw new InvalidOperationException("UI-Auswahl ist mehrdeutig.");
            if (matches.Count == 0) { await Task.Delay(100, timeout.Token); continue; }
            var element = matches[0];
            if (action.Operation == UiOperation.WaitState)
            {
                var ready = action.Value switch { "Exists" => true, "Enabled" => element.Current.IsEnabled, "On" => ((TogglePattern)element.GetCurrentPattern(TogglePattern.Pattern)).Current.ToggleState == ToggleState.On, "Off" => ((TogglePattern)element.GetCurrentPattern(TogglePattern.Pattern)).Current.ToggleState == ToggleState.Off, _ => false };
                if (ready) return;
                await Task.Delay(100, timeout.Token); continue;
            }
            if (!element.Current.IsEnabled) throw new InvalidOperationException("UI-Element ist deaktiviert.");
            timeout.Token.ThrowIfCancellationRequested();
            switch (action.Operation)
            {
                case UiOperation.Invoke: ((InvokePattern)element.GetCurrentPattern(InvokePattern.Pattern)).Invoke(); break;
                case UiOperation.SetValue: ((ValuePattern)element.GetCurrentPattern(ValuePattern.Pattern)).SetValue(action.Value); break;
                case UiOperation.Select: ((SelectionItemPattern)element.GetCurrentPattern(SelectionItemPattern.Pattern)).Select(); break;
                case UiOperation.Toggle: ((TogglePattern)element.GetCurrentPattern(TogglePattern.Pattern)).Toggle(); break;
                case UiOperation.Focus: element.SetFocus(); break;
            }
            return;
        }
    }
}

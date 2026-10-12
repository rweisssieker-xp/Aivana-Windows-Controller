using System.Text.Json;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
using Aivana.Windows;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void AddUiAutomationFields(Panel target, ActionStep step)
    {
        step.UiAutomation ??= new(UiOperation.Invoke, new("", "", "Button"));
        var operation = new ComboBox { ItemsSource = Enum.GetValues<UiOperation>(), SelectedItem = step.UiAutomation.Operation };
        operation.SelectionChanged += (_, _) => { step.UiAutomation = step.UiAutomation with { Operation = (UiOperation)operation.SelectedItem }; ScheduleEditorHistoryCapture(); };
        target.Children.Add(operation);
        var selector = new TextBox { Text = JsonSerializer.Serialize(step.UiAutomation.Selector), AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, MinHeight = 70 };
        var value = new TextBox { Text = step.UiAutomation.Value, ToolTip = L("Wert oder Wartezustand: Exists, Enabled, On, Off") };
        var status = Label("", 12, "#F2B66D");
        selector.TextChanged += (_, _) =>
        {
            try { var parsed = JsonSerializer.Deserialize<UiElementSelector>(selector.Text); if (parsed == null) throw new JsonException(); step.UiAutomation = step.UiAutomation with { Selector = parsed }; status.Text = ""; }
            catch (JsonException) { step.UiAutomation = step.UiAutomation with { Selector = new("", "", "") }; status.Text = L("Ungültige Elementauswahl."); }
            ScheduleEditorHistoryCapture();
        };
        value.TextChanged += (_, _) => { step.UiAutomation = step.UiAutomation with { Value = value.Text }; ScheduleEditorHistoryCapture(); };
        target.Children.Add(selector); target.Children.Add(value);
        target.Children.Add(Button(L("Element unter Maus aufnehmen (3 s)"), async () =>
        {
            try
            {
                status.Text = L("Maus über das Zielelement bewegen …");
                await Task.Delay(3000);
                GetCursorPos(out var point);
                var captured = await AutomationWorkerClient.RequestAsync(new("capture", X: point.X, Y: point.Y), CancellationToken.None);
                selector.Text = captured.GetRawText(); status.Text = L("Auswahl übernommen; nichts ausgeführt.");
            } catch (Exception ex) { status.Text = ex.Message; }
        }));
        target.Children.Add(Button(L("Auswahl prüfen"), async () =>
        {
            try { var count = await AutomationWorkerClient.RequestAsync(new("inspect", step.UiAutomation), CancellationToken.None); status.Text = L("Gefundene Elemente") + ": " + count; }
            catch (Exception ex) { status.Text = ex.Message; }
        }));
        target.Children.Add(status);
    }
    private void ShowUiAutomationDraft()
    {
        if (!ResolvePendingDraft()) return;
        AddTile(ActiveProfile);
        if (_draftTile == null) return;
        _draftTile.Name = L("UI-Automation"); _draftTile.Steps = [new() { Kind = ActionKind.UiAutomation, Value = "Invoke", UiAutomation = new(UiOperation.Invoke, new("", "", "Button")) }];
        _selectedStep = 0; _page = "Dashboard"; Refresh(forceRender: true);
    }
    [StructLayout(LayoutKind.Sequential)] private struct CursorPoint { public int X, Y; }
    [DllImport("user32.dll")] private static extern bool GetCursorPos(out CursorPoint point);
}

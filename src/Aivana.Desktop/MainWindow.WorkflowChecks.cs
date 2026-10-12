using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    public void RunWorkflowEditorSmokeChecks(Action<Window, string> snapshot)
    {
        var settingsBefore = JsonSerializer.Serialize(_controller.Settings);
        var previousDraft = _draftTile; var previousTile = _selectedTileId; var previousProfile = _draftProfileId;
        var previousBaseline = _draftBaseline; var previousBaselineTile = _draftBaselineTileId; var previousBaselineProfile = _draftBaselineProfileId;
        var previousStep = _selectedStep; var previousPage = _page;
        Exception? failure = null;
        try
        {
            var tile = new Tile { Name = "Workflow GUI smoke", Enabled = false, Steps = [
                new() { Kind = ActionKind.Text, Value = "first" },
                new() { Kind = ActionKind.Text, Value = "branch" },
                new() { Kind = ActionKind.Delay, DelayMs = 0 } ] };
            _draftTile = tile; _selectedTileId = tile.Id; _draftProfileId = ActiveProfile!.Id;
            _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = _draftProfileId;
            _selectedStep = 0; _page = "Dashboard";
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var dialog = OwnedWindows.Cast<Window>().Single(window => window.Title == L("Ablaufeditor"));
                try
                {
                    dialog.UpdateLayout();
                    var combos = FindKeyboardVisualChildren<ComboBox>(dialog).ToList();
                    var boxes = FindKeyboardVisualChildren<TextBox>(dialog).ToList();
                    combos[0].SelectedIndex = 1;
                    boxes[0].Text = "C:\\workflow-smoke-condition.txt";
                    boxes[1].Text = "1";
                    FindKeyboardVisualChildren<CheckBox>(dialog).First().IsChecked = true;
                    combos[1].SelectedIndex = 1;
                    dialog.UpdateLayout(); snapshot(dialog, "Ablaufeditor");
                    FindKeyboardVisualChildren<Button>(dialog).Single(button => button.Content as string == L("In Entwurf übernehmen"))
                        .RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                catch (Exception exception) { failure = exception; }
                finally { if (dialog.IsVisible) dialog.Close(); }
            }));
            ShowWorkflowDesigner();
            if (failure != null) throw new InvalidOperationException("Workflow dialog GUI failed.", failure);
            var first = tile.Steps[0];
            if (first.Condition is not { Kind: WorkflowConditionKind.FileExists, Negate: true } || first.Condition.Value != "C:\\workflow-smoke-condition.txt"
                || first.OnConditionFalseSkip != 1 || first.OnError != StepErrorPolicy.Continue)
                throw new InvalidOperationException("Workflow dialog did not adopt the edited branch and error policy into the draft.");
            if (JsonSerializer.Serialize(_controller.Settings) != settingsBefore)
                throw new InvalidOperationException("Workflow dialog modified saved profiles before saving the draft.");
            var adopted = JsonSerializer.Serialize(tile);
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var dialog = OwnedWindows.Cast<Window>().Single(window => window.Title == L("Ablaufeditor"));
                try
                {
                    dialog.UpdateLayout(); FindKeyboardVisualChildren<ComboBox>(dialog).First().SelectedIndex = 0;
                    FindKeyboardVisualChildren<Button>(dialog).Single(button => button.Content as string == L("Abbrechen"))
                        .RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                }
                catch (Exception exception) { failure = exception; }
                finally { if (dialog.IsVisible) dialog.Close(); }
            }));
            ShowWorkflowDesigner();
            if (failure != null) throw new InvalidOperationException("Workflow dialog cancellation GUI failed.", failure);
            if (JsonSerializer.Serialize(tile) != adopted) throw new InvalidOperationException("Cancelling workflow edits changed the draft.");
        }
        finally
        {
            _draftTile = previousDraft; _selectedTileId = previousTile; _draftProfileId = previousProfile;
            _draftBaseline = previousBaseline; _draftBaselineTileId = previousBaselineTile; _draftBaselineProfileId = previousBaselineProfile;
            _selectedStep = previousStep; _page = previousPage;
            ResetDraftHistory(); Refresh(forceRender: true);
        }
    }
}

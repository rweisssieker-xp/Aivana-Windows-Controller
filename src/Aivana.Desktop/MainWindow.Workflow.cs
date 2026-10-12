using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private void ShowWorkflowDesigner()
    {
        if (SelectedTile is not { } tile) { MessageBox.Show(this, L("Wähle zuerst eine Kachel aus.")); return; }
        var steps = JsonSerializer.Deserialize<List<ActionStep>>(JsonSerializer.Serialize(tile.Steps))!;
        var dialog = new Window { Owner = this, Title = L("Ablaufeditor"), Width = 850, Height = 650, MinWidth = 780, MinHeight = 450,
            Background = Brush("#101719"), Foreground = Brush("#E8F0F1"), FontFamily = FontFamily,
            WindowStartupLocation = WindowStartupLocation.CenterOwner, ShowInTaskbar = false, UseLayoutRounding = true };
        dialog.Resources.MergedDictionaries.Add(Resources);
        var root = new DockPanel { Margin = new Thickness(18) };
        var footer = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        DockPanel.SetDock(footer, Dock.Bottom); root.Children.Add(footer);
        var body = new StackPanel();
        body.Children.Add(new TextBlock { Text = L("Bedingungen prüfen vor jedem Schritt. Bei Nein wird der Schritt übersprungen; zusätzliche Sprünge führen nur vorwärts. Für einen Sonst-Zweig dieselbe Bedingung umkehren."), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 12) });
        var rows = new StackPanel(); body.Children.Add(rows);
        void Render()
        {
            rows.Children.Clear();
            for (var i = 0; i < steps.Count; i++)
            {
                var index = i; var step = steps[i];
                var card = new StackPanel { Margin = new Thickness(0, 4, 0, 12) };
                var title = new StackPanel { Orientation = Orientation.Horizontal };
                title.Children.Add(new TextBlock { Text = $"{i + 1}. {ActionName(step.Kind)} · {step.Value}", Width = 570, TextTrimming = TextTrimming.CharacterEllipsis });
                title.Children.Add(Button("↑", () => { if (index > 0) { (steps[index - 1], steps[index]) = (steps[index], steps[index - 1]); Render(); } }));
                title.Children.Add(Button("↓", () => { if (index < steps.Count - 1) { (steps[index + 1], steps[index]) = (steps[index], steps[index + 1]); Render(); } }));
                card.Children.Add(title);
                var modes = new[] { L("Immer"), L("Datei vorhanden"), L("Prozess läuft"), L("Fenstertitel enthält") };
                var condition = new ComboBox { ItemsSource = modes, SelectedIndex = step.Condition == null ? 0 : (int)step.Condition.Kind + 1, Margin = new Thickness(0, 5, 0, 5) };
                var value = new TextBox { Text = step.Condition?.Value ?? "", ToolTip = L("Bedingungswert"), IsEnabled = step.Condition != null };
                var negate = new CheckBox { Content = L("Bedingung umkehren"), IsChecked = step.Condition?.Negate == true };
                void UpdateCondition() => step.Condition = condition.SelectedIndex <= 0 ? null : new WorkflowCondition((WorkflowConditionKind)(condition.SelectedIndex - 1), value.Text, negate.IsChecked == true);
                condition.SelectionChanged += (_, _) => { value.IsEnabled = condition.SelectedIndex > 0; UpdateCondition(); };
                value.TextChanged += (_, _) => UpdateCondition(); negate.Checked += (_, _) => UpdateCondition(); negate.Unchecked += (_, _) => UpdateCondition();
                card.Children.Add(condition); card.Children.Add(value); card.Children.Add(negate);
                var skip = new TextBox { Text = step.OnConditionFalseSkip.ToString(), Width = 60, HorizontalAlignment = HorizontalAlignment.Left };
                card.Children.Add(new TextBlock { Text = L("Bei Nein zusätzlich folgende Schritte überspringen:") }); card.Children.Add(skip);
                skip.TextChanged += (_, _) => step.OnConditionFalseSkip = int.TryParse(skip.Text, out var count) ? count : -1;
                var errors = new ComboBox { ItemsSource = new[] { L("Bei Fehler stoppen"), L("Bei Fehler fortsetzen") }, SelectedIndex = (int)step.OnError, Margin = new Thickness(0, 5, 0, 0) };
                errors.SelectionChanged += (_, _) => step.OnError = (StepErrorPolicy)errors.SelectedIndex;
                card.Children.Add(errors); rows.Children.Add(card);
            }
        }
        footer.Children.Add(Button(L("Abbrechen"), () => dialog.Close()));
        footer.Children.Add(Button(L("In Entwurf übernehmen"), () =>
        {
            var candidate = JsonSerializer.Deserialize<Tile>(JsonSerializer.Serialize(tile))!; candidate.Steps = steps;
            var errors = ConfigurationValidator.ValidateTile(candidate);
            if (errors.Count > 0) { MessageBox.Show(dialog, string.Join(Environment.NewLine, errors), L("Ablauf prüfen")); return; }
            tile.Steps = steps; ScheduleEditorHistoryCapture(); dialog.Close(); Refresh();
        }));
        root.Children.Add(new ScrollViewer { Content = body, VerticalScrollBarVisibility = ScrollBarVisibility.Auto });
        Render(); dialog.Content = root; dialog.ShowDialog();
    }
}

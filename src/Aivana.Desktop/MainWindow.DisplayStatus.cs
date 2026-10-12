using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Aivana.Core;
using Aivana.Windows;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private sealed record DisplayChoice(string? Id, string Label);

    private void AddDisplayBrightnessFields(Panel target, ActionStep step)
    {
        target.Children.Add(FieldLabel(L("Zielbildschirm")));
        var picker = new ComboBox { Name = "DisplayDevicePicker", DisplayMemberPath = "Label", SelectedValuePath = "Id", Margin = new Thickness(0, 2, 0, 8) };
        var status = Label(L("Helligkeit wird gelesen …"), 11, "#91A3A5");
        var choices = new List<DisplayChoice> { new(null, L("Alle unterstützten Displays")) };
        if (!string.IsNullOrEmpty(step.DisplayDeviceId)) choices.Add(new(step.DisplayDeviceId, L("Gespeicherter Bildschirm") + " · " + step.DisplayDeviceId));
        picker.ItemsSource = choices;
        picker.SelectedItem = choices.First(c => c.Id == (string.IsNullOrEmpty(step.DisplayDeviceId) ? null : step.DisplayDeviceId));
        var updating = false;
        var reading = false;
        var detached = false;
        picker.SelectionChanged += (_, _) =>
        {
            if (!updating && picker.SelectedItem is DisplayChoice choice) { step.DisplayDeviceId = choice.Id; Refresh(); }
        };
        async void Read(object? sender, EventArgs e)
        {
            if (reading || detached || !picker.IsVisible) return;
            reading = true;
            try
            {
                var displays = await Task.Run(() => DisplayBrightnessService.GetDisplays());
                if (detached) return;
                var selection = string.IsNullOrEmpty(step.DisplayDeviceId) ? null : step.DisplayDeviceId;
                var options = new List<DisplayChoice> { new(null, L("Alle unterstützten Displays")) };
                options.AddRange(displays.Select(d => new DisplayChoice(d.Id, d.Name + " · " + d.Percent + "%")));
                if (!string.IsNullOrEmpty(selection) && options.All(d => d.Id != selection)) options.Add(new(selection, L("Bildschirm nicht verbunden") + " · " + selection));
                // Do not replace the open dropdown while the user is selecting a monitor.
                if (!picker.IsDropDownOpen)
                {
                    updating = true;
                    picker.ItemsSource = options;
                    picker.SelectedItem = options.First(d => d.Id == selection);
                    updating = false;
                }
                var selected = displays.Where(d => string.IsNullOrEmpty(selection) || d.Id == selection).ToArray();
                status.Text = selected.Length == 0 ? L("Keine unterstützte Anzeige verbunden. DDC/CI prüfen.")
                    : string.Join(" · ", selected.Select(d => d.Name + ": " + d.Percent + "%"));
            }
            catch (Exception ex) { if (!detached) status.Text = ex.Message; }
            finally { updating = false; reading = false; }
        }
        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
        timer.Tick += Read;
        picker.Loaded += (_, _) => { detached = false; Read(null, EventArgs.Empty); timer.Start(); };
        picker.Unloaded += (_, _) => { detached = true; timer.Stop(); };
        target.Children.Add(picker);
        target.Children.Add(status);
        target.Children.Add(Button(L("Helligkeit aktualisieren"), () => Read(null, EventArgs.Empty), false));
    }
}

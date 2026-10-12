using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
using Aivana.Windows;
using Microsoft.Win32;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private async void ShowWorkspaceWizard()
    {
        if (!ResolvePendingDraft()) return;
        var baseline = JsonSerializer.Serialize(_controller.Settings);
        var window = new Window { Owner = this, Title = L("Arbeitsplatz einrichten"), Width = 720, Height = 780, Background = _panel, Foreground = System.Windows.Media.Brushes.White, WindowStartupLocation = WindowStartupLocation.CenterOwner };
        window.Resources.MergedDictionaries.Add(Resources);
        var body = new StackPanel { Margin = new Thickness(20) };
        void Heading(string text) => body.Children.Add(Label(L(text), 14, "#E8F0F1", true, new Thickness(0, 10, 0, 6)));
        Heading("1 · Ablauf auswählen");
        var kinds = new ComboBox { ItemsSource = Enum.GetValues<WorkspaceKind>().Select(k => new { Kind=k, Name=L(k switch { WorkspaceKind.Workplace=>"Arbeitsplatz", WorkspaceKind.Meeting=>"Besprechung", WorkspaceKind.Streaming=>"Streaming", WorkspaceKind.Research=>"Recherche", _=>"Projekt" }) }).ToArray(), DisplayMemberPath="Name", SelectedValuePath="Kind", SelectedIndex = 0 }; body.Children.Add(kinds);
        var name = new TextBox { Text = L("Mein Arbeitsplatz"), Margin = new Thickness(0, 6, 0, 6) }; body.Children.Add(name);
        Heading("2 · Programme und Ziele auswählen");
        var programs = new ListBox { SelectionMode = SelectionMode.Multiple, DisplayMemberPath = "Name", Height = 100 }; body.Children.Add(programs);
        var additional = new List<string>();
        Action invalidateAdditional = () => { };
        var picked = Label("", 12, "#91A3A5"); body.Children.Add(picked);
        body.Children.Add(Button(L("Programm auswählen"), () => { var picker = new OpenFileDialog { Filter = "Programme|*.exe" }; if (picker.ShowDialog(window) == true) { additional.Add(picker.FileName); invalidateAdditional(); picked.Text = string.Join("\n", additional); } }));
        Heading("Webadressen (eine pro Zeile)");
        var urls = new TextBox { AcceptsReturn = true, Height = 65 }; body.Children.Add(urls);
        var audio = new ComboBox { DisplayMemberPath = "Name", SelectedValuePath = "Id", Margin = new Thickness(0, 6, 0, 6) }; body.Children.Add(Label(L("Ausgabe · optional"), 12)); body.Children.Add(audio);
        var display = new ComboBox { DisplayMemberPath = "Name", SelectedValuePath = "Id", Margin = new Thickness(0, 6, 0, 6) }; body.Children.Add(Label(L("Bildschirm · optional"), 12)); body.Children.Add(display);
        var scene = new ComboBox { IsEditable = true }; body.Children.Add(Label(L("OBS-Szene · nur Streaming"), 12)); body.Children.Add(scene);
        var state = Label("", 12, "#F2B66D"); body.Children.Add(state);
        body.Children.Add(Button(L("OBS-Szenen laden"), async () => { try { scene.ItemsSource = (await ObsActionClient.DiscoverAsync(null, CancellationToken.None)).Scenes; } catch (Exception ex) { state.Text = ex.Message; } }));
        Heading("3 · Belegung und Vorschau");
        var hotkey = new TextBox { ToolTip = "Ctrl+Alt+F9" }; body.Children.Add(hotkey);
        var preview = new TextBox { IsReadOnly = true, AcceptsReturn = true, Height = 110, VerticalScrollBarVisibility = ScrollBarVisibility.Auto }; body.Children.Add(preview);
        Profile? candidate = null;
        var save = Button(L("Profil speichern"), () =>
        {
            if (candidate == null) return;
            if (JsonSerializer.Serialize(_controller.Settings) != baseline) { state.Text = L("Konfiguration wurde geändert. Assistent erneut öffnen."); return; }
            _controller.Settings.Profiles.Add(candidate);
            try { _controller.Save(); window.Close(); SwitchPage("Profile"); }
            catch (Exception ex) { _controller.Settings.Profiles.Remove(candidate); state.Text = ex.Message; }
        }, true); save.IsEnabled = false;
        invalidateAdditional = () => { candidate = null; save.IsEnabled = false; };
        void Invalidate(object? sender, RoutedEventArgs e) { candidate = null; save.IsEnabled = false; }
        name.TextChanged += Invalidate; urls.TextChanged += Invalidate; hotkey.TextChanged += Invalidate;
        kinds.SelectionChanged += Invalidate; programs.SelectionChanged += Invalidate; audio.SelectionChanged += Invalidate; display.SelectionChanged += Invalidate; scene.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler((_, _) => { candidate = null; save.IsEnabled = false; }));
        body.Children.Add(Button(L("Vorschau erstellen"), () =>
        {
            try
            {
                candidate = WorkspaceSetup.CreateProfile(new((WorkspaceKind)kinds.SelectedValue, name.Text, programs.SelectedItems.OfType<ProgramCandidate>().Select(p => p.Path).Concat(additional).Distinct().ToArray(), urls.Text.Split(['\r','\n'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), audio.SelectedValue as string, display.SelectedValue as string, scene.Text, hotkey.Text));
                preview.Text = string.Join("\n", candidate.Tiles[0].Steps.Select((s,i) => $"{i+1}. {s.Kind}: {s.Value} {s.Arguments}")); save.IsEnabled = true; state.Text = L("Beim Speichern wird nichts ausgeführt.");
            } catch (Exception ex) { state.Text = ex.Message; }
        }));
        body.Children.Add(save); window.Content = new ScrollViewer { Content = body, VerticalScrollBarVisibility = ScrollBarVisibility.Auto }; window.Show();
        try { programs.ItemsSource = await Task.Run(WorkspaceDiscovery.GetPrograms); audio.ItemsSource = await Task.Run(() => AudioEndpointControl.GetDevices(false)); display.ItemsSource = await Task.Run(() => DisplayBrightnessService.GetDisplays()); }
        catch (Exception ex) { state.Text = ex.Message; }
    }
}

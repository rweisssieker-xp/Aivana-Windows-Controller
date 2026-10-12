using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
using Microsoft.Win32;

namespace Aivana.Desktop;
public partial class MainWindow
{
    private void ShowProfileSync() => ShowProfileSync(null, null);
    private void ShowProfileSync(string? selectedFolder, Action<Window>? opened = null)
    {
        if (!ResolvePendingDraft()) return;
        if (selectedFolder == null)
        {
            var picker = new OpenFolderDialog { Title = L("Synchronisierungsordner wählen") };
            if (picker.ShowDialog(this) != true) return;
            selectedFolder = picker.FolderName;
        }
        var folder = selectedFolder;
        var path = System.IO.Path.Combine(folder, ProfileSync.FileName);
        var revision = ProfileSync.Revision(path);
        var local = ProfileSync.PreparePortable(_controller.Settings, out var bindings, out var excluded);
        var window = new Window { Owner = this, Title = L("Portable Profile synchronisieren"), Width = 720, Height = 660, WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = _panel, Foreground = System.Windows.Media.Brushes.White };
        var panel = new StackPanel { Margin = new Thickness(20) };
        panel.Children.Add(Label(folder, 12));
        panel.Children.Add(Label(L("Manueller Austausch über einen lokalen, OneDrive- oder Netzwerkordner. Gerätebindungen werden entfernt; übernommene Aktionen bleiben deaktiviert. Ohne Freigabe werden freie Aktionsinhalte durch Pausen ersetzt. Vollständige Aktionsinhalte können private Texte oder Tokens enthalten. Namen und Tastenkürzel bleiben sichtbar: Vorschau vor dem Austausch prüfen."), 12));
        var summary = Label(LF($"Exportvorschau: {local.Profiles.Count} Profile · {bindings} Gerätebindungen entfernt · {excluded} Aktionsinhalte ausgeschlossen"), 13); panel.Children.Add(summary);
        var preview = new TextBox { IsReadOnly = true, Height = 130, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, Text = string.Join("\n", local.Profiles.Select(p => p.Name + ": " + string.Join(", ", p.Tiles.Select(t => t.Name)))) };
        panel.Children.Add(preview);
        var contents = new CheckBox { Content = L("Vollständige Aktionsinhalte einschließen (kann private Texte oder Tokens enthalten)"), Margin = new Thickness(0, 10, 0, 10) };
        void PreparePreview()
        {
            local = ProfileSync.PreparePortable(_controller.Settings, out bindings, out excluded, contents.IsChecked == true);
            summary.Text = LF($"Exportvorschau: {local.Profiles.Count} Profile · {bindings} Gerätebindungen entfernt · {excluded} Aktionsinhalte ausgeschlossen");
            preview.Text = string.Join("\n", local.Profiles.SelectMany(p => p.Tiles.Select(t => p.Name + " / " + t.Name + "\n" + string.Join("\n", t.Steps.Select(s => s.Kind + ": " + s.Value + " " + s.Arguments + " " + s.WorkingDirectory)))));
        }
        contents.Checked += (_, _) => PreparePreview(); contents.Unchecked += (_, _) => PreparePreview();
        panel.Children.Add(contents);
        PreparePreview();
        var watch = new CheckBox { Content = L("Ordner während dieses Fensters automatisch auf Änderungen prüfen"), IsChecked = true };
        panel.Children.Add(watch);
        var remoteState = Label(L("Ordnerstand geprüft"), 11); panel.Children.Add(remoteState);
        var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        timer.Tick += (_, _) =>
        {
            if (watch.IsChecked != true) return;
            try { remoteState.Text = ProfileSync.Revision(path) == revision ? L("Ordnerstand unverändert") : L("Änderung erkannt · Ordnerstand neu laden und Vorschau prüfen"); }
            catch (Exception ex) when (ex is System.IO.IOException or UnauthorizedAccessException) { remoteState.Text = ex.Message; }
        };
        panel.Children.Add(Button(L("Ordnerstand neu laden"), () => { try { revision = ProfileSync.Revision(path); remoteState.Text = L("Ordnerstand geprüft"); } catch (Exception ex) { ShowError(ex); } }, false));
        timer.Start(); window.Closed += (_, _) => timer.Stop();
        panel.Children.Add(Button(L("Lokale Profile in Ordner schreiben"), () =>
        {
            try
            {
                if (MessageBox.Show(window, L("Geprüfte Exportvorschau schreiben und vorhandene Ordnerdatei ersetzen?"), L("Exportvorschau"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
                revision = ProfileSync.Push(local, folder, revision);
                MessageBox.Show(window, L("Portable Profile gespeichert."));
            }
            catch (Exception ex) { ShowError(ex); }
        }, false));
        panel.Children.Add(Button(L("Profile aus Ordner prüfen und übernehmen"), () =>
        {
            try
            {
                var imported = ProfileSync.Pull(folder, revision);
                var names = string.Join("\n", imported.Profiles.SelectMany(p => p.Tiles.Select(t => p.Name + " / " + t.Name + "\n" + string.Join("\n", t.Steps.Select(s => s.Kind + ": " + s.Value + " " + s.Arguments)))));
                if (MessageBox.Show(window, L("Diese Profile ersetzen die lokalen Profile. Alle importierten Aktionen bleiben deaktiviert. Geräte müssen neu zugeordnet werden.") + "\n\n" + names, L("Importvorschau"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
                if (!ResolvePendingDraft()) return;
                var settings = _controller.Settings;
                var previous = settings.Profiles; var active = settings.ActiveProfileId; var fallback = settings.DefaultProfileId; var automatic = settings.AutoProfiles;
                try
                {
                    settings.Profiles = imported.Profiles; settings.ActiveProfileId = imported.ActiveProfileId; settings.DefaultProfileId = imported.DefaultProfileId; settings.AutoProfiles = false;
                    _controller.CancelAll(); _controller.Save();
                }
                catch { settings.Profiles = previous; settings.ActiveProfileId = active; settings.DefaultProfileId = fallback; settings.AutoProfiles = automatic; throw; }
                _selectedTileId = null; _draftTile = null; _draftProfileId = null;
                window.Close(); Refresh();
            }
            catch (Exception ex) { ShowError(ex); }
        }, false));
        window.Content = new ScrollViewer { Content = panel, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        if (opened != null) window.Loaded += (_, _) => Dispatcher.BeginInvoke(() => opened(window));
        window.ShowDialog();
    }
}

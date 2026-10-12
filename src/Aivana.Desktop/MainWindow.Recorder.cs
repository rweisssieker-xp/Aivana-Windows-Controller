using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Aivana.Core;
using Aivana.Windows;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private void ShowMacroRecorder() => ShowMacroRecorder(null);
    private void ShowMacroRecorder(Action<Window>? opened)
    {
        if (!ResolvePendingDraft() || ActiveProfile == null) return;
        var profile = ActiveProfile;
        var window = new Window { Owner = this, Title = L("Aktionsrekorder"), Width = 650, Height = 500, WindowStartupLocation = WindowStartupLocation.CenterOwner, Background = _panel, Foreground = System.Windows.Media.Brushes.White };
        var panel = new StackPanel { Margin = new Thickness(20) };
        panel.Children.Add(Label(L("Erfasst nur Tastenkombinationen mit Strg, Alt oder Win sowie Funktionstasten. Kein Text wird aufgezeichnet. Escape stoppt. Vor dem Speichern prüfen."), 12));
        var status = Label(L("Bereit · Aufnahme ist aus"), 14); panel.Children.Add(status);
        var list = new ListBox { Height = 250, Margin = new Thickness(0, 12, 0, 12) }; panel.Children.Add(list);
        KeyboardMacroRecorder? recorder = null;
        List<ActionStep> captured = [];
        var paused = _controller.Paused;
        void Stop()
        {
            if (recorder != null) { captured = recorder.Recording.Steps.ToList(); recorder.Dispose(); recorder = null; }
            _controller.SetPaused(paused);
            status.Text = L("Gestoppt · Entwurf prüfen");
            list.ItemsSource = captured.Select(s => s.Kind == ActionKind.Delay ? $"⏱ {s.DelayMs} ms" : s.Value).ToList();
        }
        panel.Children.Add(Button(L("Aufnahme starten"), () =>
        {
            if (recorder != null) return;
            try
            {
                captured = []; list.ItemsSource = null;
                _controller.SetPaused(true);
                recorder = new KeyboardMacroRecorder();
                recorder.Changed += () => Dispatcher.BeginInvoke(() => { if (recorder != null) { status.Text = LF($"Aufnahme aktiv · {recorder.Recording.Steps.Count} Schritte · Escape stoppt"); list.ItemsSource = recorder.Recording.Steps.Select(s => s.Kind == ActionKind.Delay ? $"⏱ {s.DelayMs} ms" : s.Value).ToList(); } });
                recorder.StopRequested += () => Dispatcher.BeginInvoke(Stop);
                status.Text = L("Aufnahme aktiv · Escape stoppt");
            }
            catch (Exception ex) { Stop(); ShowError(ex); }
        }, false));
        panel.Children.Add(Button(L("Aufnahme stoppen"), Stop, false));
        panel.Children.Add(Button(L("Als Kachelentwurf übernehmen"), () =>
        {
            Stop(); if (captured.Count == 0) return;
            var tile = new Tile { Name = L("Aufgenommenes Makro"), Steps = captured };
            _selectedTileId = tile.Id; _draftTile = tile; _draftProfileId = profile.Id; _selectedStep = 0;
            _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = profile.Id;
            window.Close(); _page = "Dashboard"; Refresh();
        }, true));
        window.PreviewKeyDown += (_, e) => { if (e.Key == Key.Escape) { Stop(); e.Handled = true; } };
        window.Closed += (_, _) => Stop();
        window.Content = panel;
        if (opened != null) window.Loaded += (_, _) => Dispatcher.BeginInvoke(() => opened(window));
        window.ShowDialog();
    }
}

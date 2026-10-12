using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
namespace Aivana.Desktop;
public partial class MainWindow
{
    public void ShowDeviceDiagnostics()
    {
        var dialog = new Window { Title = L("Geräte- und Tastendiagnose"), Owner = this, Width = 720, Height = 640,
            Background = Brush("#101719"), Foreground = Brush("#E8F0F1"), WindowStartupLocation = WindowStartupLocation.CenterOwner };
        dialog.Resources.MergedDictionaries.Add(Resources);
        var panel = new StackPanel { Margin = new Thickness(24) };
        dialog.Content = new ScrollViewer { Content = panel, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        panel.Children.Add(Label(L("Geräte- und Tastendiagnose"), 24, "#E8F0F1", true));
        var status = new TextBlock { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0,16,0,16) };
        panel.Children.Add(status);
        void Refresh()
        {
            var lines = new List<string> { _controller.CorsairKeyStatus, _controller.GenericKeyStatus };
            lines.AddRange(_controller.Devices.Select(d => d.Name + " — " + d.Capabilities));
            if (_controller.LastGenericKey is { } edge) lines.Add(L("Zuletzt erkannte Standardtaste") + ": " + KeyboardBinding.KeyName(edge.VirtualKey) + "\n" + edge.DevicePath);
            foreach (var tile in _controller.ActiveProfile.Tiles)
                lines.Add("\n" + tile.Name + ": " + string.Join(" · ", _controller.DiagnoseTile(tile)));
            status.Text = string.Join("\n", lines);
        }
        panel.Children.Add(new TextBlock { Text = L("Herstellerübergreifend werden Standardtasten F13–F24 und Medientasten erkannt. Proprietäre Fn-, Profil- oder Radtasten benötigen einen passenden Herstellertreiber oder eine Zuordnung zu einer Standardtaste. Normale Tastatureingaben werden nicht blockiert."), TextWrapping = TextWrapping.Wrap });
        panel.Children.Add(Button(L("Aktualisieren"), () => { _controller.RefreshDevices(); Refresh(); }, false));
        panel.Children.Add(Button(L("Schließen"), dialog.Close, false));
        Refresh(); dialog.ShowDialog();
    }
    private void AddGenericKeyFields(StackPanel panel, Tile tile)
    {
        panel.Children.Add(FieldLabel(L("Herstellerübergreifende Sondertaste")));
        var info = new TextBlock { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0,4,0,8) };
        void Update() => info.Text = tile.KeyboardBinding is { } binding
            ? KeyboardBinding.KeyName(binding.VirtualKey) + " — " + (_controller.Devices.FirstOrDefault(d => StringComparer.OrdinalIgnoreCase.Equals(d.Path,binding.DevicePath))?.Name ?? L("Tastatur nicht angeschlossen"))
            : L("Keine Standard-Sondertaste zugeordnet.");
        Update(); panel.Children.Add(info);
        panel.Children.Add(Button(L("Standard-Sondertaste erkennen"), () =>
        {
            var dialog = new Window { Title = L("Standard-Sondertaste erkennen"), Owner = this, Width = 600, Height = 340,
                Background = Brush("#101719"), Foreground = Brush("#E8F0F1"), WindowStartupLocation = WindowStartupLocation.CenterOwner };
            dialog.Resources.MergedDictionaries.Add(Resources);
            var content = new StackPanel { Margin = new Thickness(24) }; dialog.Content = content;
            var devices = new ComboBox { ItemsSource = _controller.Devices.ToArray(), DisplayMemberPath = "Name", SelectedIndex = 0 };
            content.Children.Add(devices);
            var message = new TextBlock { Text = L("Wähle die Tastatur und drücke nach dem Start eine Taste. Die normale Windows-Eingabe bleibt aktiv. Proprietäre Herstellerfunktionen müssen gegebenenfalls zuerst eine Standardtaste senden."), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0,12,0,12) };
            content.Children.Add(message);
            IDisposable? capture = null;
            content.Children.Add(Button(L("Tastendruck erkennen"), () =>
            {
                capture?.Dispose(); capture = null;
                if (devices.SelectedItem is not DeviceInfo device) return;
                capture = _controller.BeginGenericKeyCapture(device.Path, edge =>
                {
                    tile.KeyboardBinding = new(edge.DevicePath, edge.VirtualKey) { ScanCode = edge.ScanCode, Extended = edge.Extended, Extended1 = edge.Extended1 };
                    capture?.Dispose(); capture = null; Update(); Refresh(); dialog.Close();
                });
                message.Text = L("Erkennung aktiv. Drücke jetzt die Standard-Sondertaste auf dem gewählten Gerät.");
            }, true));
            devices.SelectionChanged += (_,_) => { capture?.Dispose(); capture = null; };
            content.Children.Add(Button(L("Abbrechen"), dialog.Close, false));
            dialog.Closed += (_,_) => capture?.Dispose();
            dialog.ShowDialog();
        }, false));
        panel.Children.Add(Button(L("Standard-Sondertaste entfernen"), () => { tile.KeyboardBinding = null; Update(); Refresh(); }, false));
    }
}

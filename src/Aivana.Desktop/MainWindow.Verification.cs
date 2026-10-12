using System.Windows;
using System.Windows.Controls;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void ShowVerificationHistory()
    {
        var list = new ListBox();
        void Update() => list.ItemsSource = _controller.VerificationHistory.Select(r => $"{r.RunId.ToString()[..8]} · {r.TileId.ToString()[..8]} · {r.Step} · {r.Result.State}: {r.Result.Message}").Reverse().ToArray();
        var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) => Update();
        var window = new Window { Owner = this, Title = L("Ausführungsprüfung"), Width = 850, Height = 540, Content = list, Background = _panel, Foreground = System.Windows.Media.Brushes.White };
        window.Resources.MergedDictionaries.Add(Resources);
        window.Closed += (_, _) => timer.Stop(); Update(); timer.Start(); window.Show();
    }
}

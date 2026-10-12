using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Aivana.Desktop;
public partial class MainWindow
{
    public void RunRecorderSyncSmokeChecks(Action<Window, string> snapshot)
    {
        var originalPause = _controller.Paused;
        Exception? failure = null;
        ShowMacroRecorder(window =>
        {
            try
            {
                var panel = (StackPanel)window.Content;
                var buttons = panel.Children.OfType<Button>().ToArray();
                buttons[0].RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                window.UpdateLayout();
                if (!_controller.Paused) throw new InvalidOperationException("Recorder did not suspend actions.");
                snapshot(window, "recorder-active");
                buttons[1].RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                window.UpdateLayout();
                snapshot(window, "recorder-stopped");
            }
            catch (Exception ex) { failure = ex; }
            finally { window.Close(); }
        });
        if (_controller.Paused != originalPause) throw new InvalidOperationException("Recorder changed the original pause state.");
        if (failure != null) throw new InvalidOperationException("Recorder GUI check failed.", failure);
        var folder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "makrivo-sync-ui-" + Guid.NewGuid().ToString("N"));
        System.IO.Directory.CreateDirectory(folder);
        try
        {
            ShowProfileSync(folder, window =>
            {
                try
                {
                    window.UpdateLayout(); snapshot(window, "profile-sync-preview");
                    var scroll = (ScrollViewer)window.Content;
                    var panel = (StackPanel)scroll.Content;
                    var include = panel.Children.OfType<CheckBox>().First();
                    include.IsChecked = true;
                    window.UpdateLayout(); snapshot(window, "profile-sync-complete-preview");
                }
                catch (Exception ex) { failure = ex; }
                finally { window.Close(); }
            });
            if (System.IO.Directory.EnumerateFileSystemEntries(folder).Any()) throw new InvalidOperationException("Sync preview wrote a profile without confirmation.");
            if (failure != null) throw new InvalidOperationException("Sync GUI check failed.", failure);
        }
        finally { System.IO.Directory.Delete(folder); }
    }
}

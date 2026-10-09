using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private Window? _executionFeedbackWindow;
    private TextBlock? _executionFeedbackText;
    private ProgressBar? _executionFeedbackProgress;
    private readonly DispatcherTimer _executionFeedbackTimer = new() { Interval = TimeSpan.FromSeconds(4) };
    private bool _executionFeedbackInitialized;

    private void InitializeExecutionFeedback()
    {
        if (_executionFeedbackInitialized) return;
        _executionFeedbackInitialized = true;
        _controller.ExecutionFeedbackChanged += ShowExecutionFeedback;
        _executionFeedbackTimer.Tick += HideExecutionFeedback;
    }

    private void DisposeExecutionFeedback()
    {
        _controller.ExecutionFeedbackChanged -= ShowExecutionFeedback;
        _executionFeedbackTimer.Stop();
        _executionFeedbackTimer.Tick -= HideExecutionFeedback;
        _executionFeedbackWindow?.Close();
        _executionFeedbackWindow = null;
        _executionFeedbackInitialized = false;
    }

    private static string FeedbackMessage(ExecutionProgress progress) => progress.Phase switch
    {
        ExecutionPhase.Waiting => L("Befehle warten auf Ausführung"),
        ExecutionPhase.Running => L("Befehle werden ausgeführt"),
        ExecutionPhase.Step => Aivana.Core.Localization.Format("Schritt {0}/{1} · Durchlauf {2}/{3}", progress.Step, progress.Steps, progress.Repetition, progress.Repetitions),
        ExecutionPhase.Completed => L("Befehle ausgeführt"),
        ExecutionPhase.Cancelled => L("Ausführung abgebrochen"),
        ExecutionPhase.Failed => L("Ausführung fehlgeschlagen · Details im Aktivitätsverlauf"),
        _ => L("Befehl nicht gestartet · Details im Aktivitätsverlauf")
    };

    private static double FeedbackPercentage(ExecutionProgress progress) => progress.Phase == ExecutionPhase.Completed ? 100
        : progress.Phase == ExecutionPhase.Step && progress.Steps > 0 && progress.Repetitions > 0
            ? Math.Clamp(100d * ((double)(progress.Repetition - 1) * progress.Steps + progress.Step - 1) / ((double)progress.Steps * progress.Repetitions), 0, 100)
            : 0;

    private void ShowExecutionFeedback(ExecutionProgress progress)
    {
        if (!_executionFeedbackInitialized) return;
        if (_executionFeedbackWindow is null)
        {
            _executionFeedbackText = new TextBlock { TextWrapping = TextWrapping.Wrap, Foreground = Brushes.White, FontSize = 14, Margin = new Thickness(0, 0, 0, 12) };
            _executionFeedbackProgress = new ProgressBar { Height = 4, Minimum = 0, Maximum = 100, Foreground = _accent };
            var panel = new StackPanel();
            panel.Children.Add(new TextBlock { Text = "AIVANA", Foreground = _accent, FontSize = 11, Margin = new Thickness(0, 0, 0, 8) });
            panel.Children.Add(_executionFeedbackText);
            panel.Children.Add(_executionFeedbackProgress);
            _executionFeedbackWindow = new Window
            {
                Width = 360, SizeToContent = SizeToContent.Height, WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize, ShowActivated = false, ShowInTaskbar = false,
                Topmost = true, IsHitTestVisible = false, Focusable = false, Background = _panel,
                Content = new Border { Padding = new Thickness(18), BorderBrush = _line, BorderThickness = new Thickness(1), Child = panel }
            };
            _executionFeedbackWindow.SourceInitialized += (_, _) =>
            {
                var handle = new WindowInteropHelper(_executionFeedbackWindow).Handle;
                var style = GetFeedbackWindowStyle(handle, -20).ToInt64();
                SetFeedbackWindowStyle(handle, -20, new IntPtr(style | 0x08000000L | 0x00000080L | 0x00000020L));
            };
            _executionFeedbackWindow.SizeChanged += (_, _) => PositionExecutionFeedback();
        }
        _executionFeedbackText!.Text = FeedbackMessage(progress);
        _executionFeedbackProgress!.IsIndeterminate = progress.Phase is ExecutionPhase.Waiting or ExecutionPhase.Running;
        _executionFeedbackProgress.Value = FeedbackPercentage(progress);
        PositionExecutionFeedback();
        _executionFeedbackWindow.Show();
        _executionFeedbackTimer.Stop();
        if (progress.Phase is ExecutionPhase.Completed or ExecutionPhase.Cancelled or ExecutionPhase.Failed or ExecutionPhase.Rejected)
            _executionFeedbackTimer.Start();
    }

    private void PositionExecutionFeedback()
    {
        if (_executionFeedbackWindow is not { } window) return;
        var area = SystemParameters.WorkArea;
        window.Left = Math.Max(area.Left, area.Right - window.Width - 20);
        window.Top = Math.Max(area.Top, area.Bottom - Math.Max(110, window.ActualHeight) - 20);
    }

    private void HideExecutionFeedback(object? sender, EventArgs e)
    {
        _executionFeedbackTimer.Stop();
        _executionFeedbackWindow?.Hide();
    }

    internal void RunExecutionFeedbackSmokeChecks()
    {
        foreach (var phase in Enum.GetValues<ExecutionPhase>())
            if (string.IsNullOrWhiteSpace(FeedbackMessage(new ExecutionProgress(Guid.NewGuid(), phase))))
                throw new InvalidOperationException("Missing execution feedback phase.");
        if (FeedbackPercentage(new ExecutionProgress(Guid.NewGuid(), ExecutionPhase.Step, 2, 4, 2, 2)) != 62.5
            || FeedbackPercentage(new ExecutionProgress(Guid.NewGuid(), ExecutionPhase.Completed)) != 100)
            throw new InvalidOperationException("Incorrect execution feedback progress.");
        InitializeExecutionFeedback();
        var foreground = GetFeedbackForegroundWindow();
        ShowExecutionFeedback(new ExecutionProgress(Guid.NewGuid(), ExecutionPhase.Step, 1, 3, 1, 1));
        if (_executionFeedbackWindow is not { ShowActivated: false, ShowInTaskbar: false, Focusable: false, IsHitTestVisible: false })
            throw new InvalidOperationException("Execution feedback can steal input focus.");
        if (foreground != GetFeedbackForegroundWindow())
            throw new InvalidOperationException("Execution feedback changed the foreground window.");
        HideExecutionFeedback(null, EventArgs.Empty);
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")] private static extern IntPtr GetFeedbackWindowStyle(IntPtr hwnd, int index);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")] private static extern IntPtr SetFeedbackWindowStyle(IntPtr hwnd, int index, IntPtr value);
    [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")] private static extern IntPtr GetFeedbackForegroundWindow();
}

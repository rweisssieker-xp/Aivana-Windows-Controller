using Aivana.Core;

namespace Aivana.Desktop;

public sealed partial class DesktopController
{
    public event Action<ExecutionProgress>? ExecutionFeedbackChanged;

    private void InitializeExecutionFeedback() => _runner.ProgressChanged += ForwardExecutionFeedback;
    private void DisposeExecutionFeedback() => _runner.ProgressChanged -= ForwardExecutionFeedback;

    private void ForwardExecutionFeedback(ExecutionProgress progress)
    {
        var dispatcher = System.Windows.Application.Current?.Dispatcher;
        if (dispatcher is null || dispatcher.HasShutdownStarted) return;
        dispatcher.BeginInvoke(() =>
        {
            if (!_disposed) ExecutionFeedbackChanged?.Invoke(progress);
        });
    }
}

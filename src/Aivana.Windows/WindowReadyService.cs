using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Aivana.Core;

namespace Aivana.Windows;

internal static class WindowReadyService
{
    internal static Task WaitAsync(ActionStep step, CancellationToken token) => WindowWait.UntilAsync(
        () => TryFocusReadyWindow(step.Value ?? "", step.Arguments ?? "", token), step.DelayMs, token);

    private static bool TryFocusReadyWindow(string processFilter, string titleFilter, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(processFilter) && string.IsNullOrWhiteSpace(titleFilter))
            throw new ArgumentException(Aivana.Core.Localization.Translate("Für das Zielfenster ist ein Prozessname oder Fenstertitel erforderlich."));
        var ready = false;
        EnumWindows((window, _) =>
        {
            if (token.IsCancellationRequested) return false;
            if (!IsWindowVisible(window)) return true;
            GetWindowThreadProcessId(window, out var pid);
            if (pid == 0 || pid == Environment.ProcessId) return true;
            try
            {
                using var process = Process.GetProcessById((int)pid);
                var title = new StringBuilder(Math.Min(4096, GetWindowTextLength(window)) + 1);
                GetWindowText(window, title, title.Capacity);
                if (!WindowWait.Matches(process.ProcessName, title.ToString(), processFilter, titleFilter)) return true;
                if (SendMessageTimeout(window, 0, IntPtr.Zero, IntPtr.Zero, 3, 100, out _) == IntPtr.Zero) return true;
                if (token.IsCancellationRequested) return false;
                if (IsIconic(window)) ShowWindowAsync(window, 9);
                if (token.IsCancellationRequested) return false;
                if (GetForegroundWindow() != window) SetForegroundWindow(window);
                ready = GetForegroundWindow() == window;
                return !ready;
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or System.ComponentModel.Win32Exception)
            { return true; }
        }, IntPtr.Zero);
        token.ThrowIfCancellationRequested();
        return ready;
    }

    private delegate bool EnumWindowCallback(IntPtr window, IntPtr parameter);
    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowCallback callback, IntPtr parameter);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr window);
    [DllImport("user32.dll")] private static extern bool IsIconic(IntPtr window);
    [DllImport("user32.dll")] private static extern bool ShowWindowAsync(IntPtr window, int command);
    [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr window);
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr window, out uint processId);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetWindowTextLength(IntPtr window);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetWindowText(IntPtr window, StringBuilder text, int maxCount);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr SendMessageTimeout(IntPtr window, uint message, IntPtr wParam, IntPtr lParam, uint flags, uint timeout, out IntPtr result);
}

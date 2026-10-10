using System.Diagnostics;

namespace Aivana.Core;

public static class WindowWait
{
    public static bool Matches(string process, string title, string processFilter, string titleFilter) =>
        (!string.IsNullOrWhiteSpace(processFilter) || !string.IsNullOrWhiteSpace(titleFilter)) &&
        (string.IsNullOrWhiteSpace(processFilter) || string.Equals(NormalizeProcess(processFilter), process, StringComparison.OrdinalIgnoreCase)) &&
        (string.IsNullOrWhiteSpace(titleFilter) || title.Contains(titleFilter.Trim(), StringComparison.OrdinalIgnoreCase));

    private static string NormalizeProcess(string filter)
    {
        var name = Path.GetFileName(filter.Trim());
        return name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ? name[..^4] : name;
    }

    public static async Task UntilAsync(Func<bool> ready, int timeoutMs, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(ready);
        if (timeoutMs is < 1 or > 600_000) throw new ArgumentOutOfRangeException(nameof(timeoutMs));
        var timer = Stopwatch.StartNew();
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var isReady = ready();
            cancellationToken.ThrowIfCancellationRequested();
            var remaining = timeoutMs - timer.ElapsedMilliseconds;
            if (isReady && remaining >= 0) return;
            if (remaining <= 0) throw new TimeoutException(Localization.Translate("Das Zielfenster wurde innerhalb des Zeitlimits nicht bereit."));
            await Task.Delay((int)Math.Min(100, remaining), cancellationToken).ConfigureAwait(false);
        }
    }
}

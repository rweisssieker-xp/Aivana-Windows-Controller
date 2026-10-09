using Aivana.Core;

namespace Aivana.Desktop;

/// <summary>Cancellation for executions that may still be waiting on target-window restoration.</summary>
internal sealed class PendingExecutionCancellation : IDisposable
{
    internal sealed class Generation
    {
        public readonly CancellationTokenSource Source = new();
        public int Leases;
        public bool Retired;
    }

    internal sealed class Lease : IDisposable
    {
        private PendingExecutionCancellation? _owner;
        private readonly Generation _generation;

        internal Lease(PendingExecutionCancellation owner, Generation generation)
        {
            _owner = owner;
            _generation = generation;
            Token = generation.Source.Token;
        }

        public CancellationToken Token { get; }

        public void Dispose() => Interlocked.Exchange(ref _owner, null)?.Release(_generation);
    }

    private readonly object _sync = new();
    private Generation _current = new();
    private bool _disposed;

    public bool TryCapture(out Lease lease)
    {
        lock (_sync)
        {
            if (_disposed)
            {
                lease = null!;
                return false;
            }
            _current.Leases++;
            lease = new Lease(this, _current);
            return true;
        }
    }

    public void CancelPending()
    {
        lock (_sync)
        {
            if (_disposed) return;
            var old = _current;
            _current = new Generation();
            old.Retired = true;
            old.Source.Cancel();
            DisposeIfRetiredAndUnused(old);
        }
    }

    public void Dispose()
    {
        lock (_sync)
        {
            if (_disposed) return;
            _disposed = true;
            var old = _current;
            old.Retired = true;
            old.Source.Cancel();
            DisposeIfRetiredAndUnused(old);
        }
    }

    private void Release(Generation generation)
    {
        lock (_sync)
        {
            generation.Leases--;
            DisposeIfRetiredAndUnused(generation);
        }
    }

    private static void DisposeIfRetiredAndUnused(Generation generation)
    {
        if (generation.Retired && generation.Leases == 0) generation.Source.Dispose();
    }
}

/// <summary>Regression checks for invalidating executions queued behind target restoration.</summary>
internal static class DesktopControllerExecutionChecks
{
    public static async Task RunAsync()
    {
        await PauseDuringRestoreCancelsEvenAfterResume();
        await CancelAndImportInvalidatePendingRuns();
        await DisposeInvalidatesPendingRuns();
    }

    private static async Task PauseDuringRestoreCancelsEvenAfterResume()
    {
        using var cancellation = new PendingExecutionCancellation();
        Check(cancellation.TryCapture(out var lease), "A pending execution should acquire a cancellation lease.");
        using (lease)
        {
            var restoreStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var finishRestore = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var paused = false;
            var executions = 0;
            var dispatch = DesktopController.DispatchPendingExecutionAsync(lease.Token, () => !paused,
                async () =>
                {
                    restoreStarted.SetResult();
                    await finishRestore.Task;
                }, _ =>
                {
                    Interlocked.Increment(ref executions);
                    return Task.CompletedTask;
                });

            await restoreStarted.Task.WaitAsync(TimeSpan.FromSeconds(2));
            paused = true;
            cancellation.CancelPending();
            paused = false;
            finishRestore.SetResult();
            await ExpectCanceled(dispatch, "Pause during restore must invalidate the captured token even if the app resumes before dispatch.");
            Check(executions == 0, "A paused pending execution reached the executor after resume.");
        }
    }

    private static async Task CancelAndImportInvalidatePendingRuns()
    {
        foreach (var invalidation in new[] { "CancelAll", "Import" })
        {
            using var cancellation = new PendingExecutionCancellation();
            Check(cancellation.TryCapture(out var lease), $"{invalidation} case should acquire a cancellation lease.");
            using (lease)
            {
                var restoreStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
                var finishRestore = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
                var executions = 0;
                var dispatch = DesktopController.DispatchPendingExecutionAsync(lease.Token, static () => true,
                    async () =>
                    {
                        restoreStarted.SetResult();
                        await finishRestore.Task;
                    }, _ =>
                    {
                        Interlocked.Increment(ref executions);
                        return Task.CompletedTask;
                    });

                await restoreStarted.Task.WaitAsync(TimeSpan.FromSeconds(2));
                cancellation.CancelPending();
                finishRestore.SetResult();
                await ExpectCanceled(dispatch, $"{invalidation} must invalidate a pending target restore.");
                Check(executions == 0, $"{invalidation} allowed a pending execution to reach the executor.");
            }
        }
    }

    private static async Task DisposeInvalidatesPendingRuns()
    {
        var cancellation = new PendingExecutionCancellation();
        Check(cancellation.TryCapture(out var lease), "Dispose case should acquire a cancellation lease.");
        using (lease)
        {
            var restoreStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var finishRestore = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            var executions = 0;
            var dispatch = DesktopController.DispatchPendingExecutionAsync(lease.Token, static () => true,
                async () =>
                {
                    restoreStarted.SetResult();
                    await finishRestore.Task;
                }, _ =>
                {
                    Interlocked.Increment(ref executions);
                    return Task.CompletedTask;
                });

            await restoreStarted.Task.WaitAsync(TimeSpan.FromSeconds(2));
            cancellation.Dispose();
            finishRestore.SetResult();
            await ExpectCanceled(dispatch, "Dispose must invalidate a pending target restore.");
            Check(executions == 0, "Dispose allowed a pending execution to reach the executor.");
        }
    }

    private static async Task ExpectCanceled(Task task, string message)
    {
        try { await task.WaitAsync(TimeSpan.FromSeconds(2)); }
        catch (OperationCanceledException) { return; }
        throw new InvalidOperationException(message);
    }

    private static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}

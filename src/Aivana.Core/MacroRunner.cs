namespace Aivana.Core;

public sealed class MacroRunner
{
    private readonly IActionExecutor _executor;
    private readonly SemaphoreSlim _executionGate = new(1, 1);
    private readonly object _sync = new();
    private readonly Dictionary<Guid, CancellationTokenSource> _runs = [];

    public MacroRunner(IActionExecutor executor) =>
        _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public event Action<Guid, string>? StatusChanged;
    public event Action<ExecutionProgress>? ProgressChanged;
    public event Action<StepVerification>? VerificationChanged;

    public bool IsRunning(Guid tileId)
    {
        lock (_sync) return _runs.ContainsKey(tileId);
    }

    public bool HasRunningActions { get { lock (_sync) return _runs.Count > 0; } }

    public void Cancel(Guid tileId)
    {
        CancellationTokenSource? source;
        lock (_sync) _runs.TryGetValue(tileId, out source);
        try { source?.Cancel(); } catch (ObjectDisposedException) { }
    }

    public void CancelAll()
    {
        CancellationTokenSource[] sources;
        lock (_sync) sources = _runs.Values.ToArray();
        foreach (var source in sources)
        {
            try { source.Cancel(); }
            catch (ObjectDisposedException) { }
        }
    }

    public async Task RunAsync(Tile tile, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(tile);
        // Capture editable inputs before queuing so UI edits cannot change a run
        // that is already waiting for the shared input-execution gate.
        var runTile = Snapshot(tile);
        var runId = Guid.NewGuid();
        if (!runTile.Enabled)
        {
            Publish(runTile.Id, Localization.Translate("Aktion ist deaktiviert."), ExecutionPhase.Rejected);
            return;
        }
        var errors = ConfigurationValidator.ValidateTile(runTile);
        if (errors.Count > 0)
        {
            Publish(runTile.Id, Localization.Translate((FormattableString)$"Ungültige Aktion: {errors[0]}"), ExecutionPhase.Rejected);
            return;
        }

        var runCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        lock (_sync)
        {
            if (_runs.ContainsKey(runTile.Id))
            {
                runCancellation.Dispose();
                Publish(runTile.Id, Localization.Translate("Makro läuft bereits."), ExecutionPhase.Rejected);
                return;
            }
            _runs.Add(runTile.Id, runCancellation);
        }

        var ownsGate = false;
        try
        {
            Publish(runTile.Id, Localization.Translate("Makro wartet."), ExecutionPhase.Waiting);
            await _executionGate.WaitAsync(runCancellation.Token).ConfigureAwait(false);
            ownsGate = true;
            Publish(runTile.Id, Localization.Translate("Makro läuft."), ExecutionPhase.Running);

            var continuedErrors = 0;
            for (var repetition = 0; repetition < runTile.Repeat; repetition++)
            {
                for (var index = 0; index < runTile.Steps.Count; index++)
                {
                    runCancellation.Token.ThrowIfCancellationRequested();
                    var step = runTile.Steps[index];
                    Publish(runTile.Id, Localization.Translate((FormattableString)$"Schritt {index + 1}/{runTile.Steps.Count} · Durchlauf {repetition + 1}/{runTile.Repeat}"), ExecutionPhase.Step, index + 1, runTile.Steps.Count, repetition + 1, runTile.Repeat);
                    try
                    {
                        if (step.Condition is { } condition)
                        {
                            if (_executor is not IWorkflowConditionEvaluator evaluator)
                                throw new InvalidOperationException(Localization.Translate("Der Aktionsdienst unterstützt keine Ablaufbedingungen."));
                            var matches = await evaluator.EvaluateAsync(condition, runCancellation.Token).ConfigureAwait(false);
                            if (condition.Negate) matches = !matches;
                            if (!matches) { index += step.OnConditionFalseSkip; continue; }
                        }
                        if (step.Verification?.Enabled == true)
                            await CheckVerificationAsync(step, runId, runTile.Id, index + 1, true, runCancellation.Token).ConfigureAwait(false);
                        if (step.Kind == ActionKind.Delay)
                            await Task.Delay(step.DelayMs, runCancellation.Token).ConfigureAwait(false);
                        else
                            await _executor.ExecuteAsync(step, runCancellation.Token).ConfigureAwait(false);
                        if (step.Verification?.Enabled == true)
                            await CheckVerificationAsync(step, runId, runTile.Id, index + 1, false, runCancellation.Token).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException) when (runCancellation.IsCancellationRequested)
                    {
                        if (step.Verification?.Enabled == true) PublishVerification(runId, runTile.Id, index + 1, new(VerificationState.Cancelled, "execution-cancelled", "Execution cancelled."));
                        throw;
                    }
                    catch (Exception exception)
                    {
                        if (step.Verification?.Enabled == true && exception is not VerificationFailureException) PublishVerification(runId, runTile.Id, index + 1, new(VerificationState.Failed, "execution-failed", "Action failed; inspect the target connection."));
                        if (step.OnError == StepErrorPolicy.Continue)
                        {
                            continuedErrors++;
                            Publish(runTile.Id, Localization.Translate((FormattableString)$"Schritt {index + 1} fehlgeschlagen; Ablauf wird fortgesetzt: {exception.Message}"), ExecutionPhase.Step, index + 1, runTile.Steps.Count, repetition + 1, runTile.Repeat);
                            continue;
                        }
                        var reason = string.IsNullOrWhiteSpace(exception.Message) ? Localization.Translate("Unbekannter Fehler") : exception.Message;
                        Publish(runTile.Id, Localization.Translate((FormattableString)$"Fehler in Schritt {index + 1}: {reason} Makro wurde beendet."), ExecutionPhase.Failed, index + 1, runTile.Steps.Count, repetition + 1, runTile.Repeat);
                        return;
                    }
                }
            }
            runCancellation.Token.ThrowIfCancellationRequested();
            Publish(runTile.Id, continuedErrors == 0 ? Localization.Translate("Makro abgeschlossen.") : Localization.Format("Ablauf mit {0} übersprungenen Fehlern abgeschlossen.", continuedErrors), ExecutionPhase.Completed);
        }
        catch (OperationCanceledException) when (runCancellation.IsCancellationRequested)
        {
            Publish(runTile.Id, Localization.Translate("Makro abgebrochen."), ExecutionPhase.Cancelled);
        }
        finally
        {
            if (ownsGate) _executionGate.Release();
            lock (_sync)
            {
                if (_runs.TryGetValue(runTile.Id, out var current) && ReferenceEquals(current, runCancellation))
                    _runs.Remove(runTile.Id);
            }
            runCancellation.Dispose();
        }
    }

    private async Task CheckVerificationAsync(ActionStep step, Guid runId, Guid tileId, int index, bool preflight, CancellationToken token)
    {
        ActionVerificationResult result;
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token);
        timeout.CancelAfter(step.Verification!.TimeoutMs);
        try
        {
            result = _executor is IActionVerificationProvider provider
                ? await (preflight ? provider.PreflightAsync(step, timeout.Token) : provider.VerifyAsync(step, timeout.Token)).WaitAsync(timeout.Token).ConfigureAwait(false)
                : new(VerificationState.Unverifiable, "unsupported", "No verification provider.");
        }
        catch (OperationCanceledException)
        {
            result = new(token.IsCancellationRequested ? VerificationState.Cancelled : VerificationState.TimedOut, "cancelled", "Verification interrupted.");
        }
        catch (Exception) { result = new(VerificationState.Failed, "provider", "State check failed; check the target connection."); }
        PublishVerification(runId, tileId, index, result);
        token.ThrowIfCancellationRequested();
        if (result.State is VerificationState.Failed or VerificationState.TimedOut or VerificationState.Cancelled)
            throw new VerificationFailureException(result.Message);
    }

    private sealed class VerificationFailureException(string message) : InvalidOperationException(message);

    private void PublishVerification(Guid runId, Guid tileId, int index, ActionVerificationResult result)
    {
        if (VerificationChanged is { } handlers)
            foreach (Action<StepVerification> handler in handlers.GetInvocationList())
                try { handler(new(runId, tileId, index, result)); } catch { }
    }

    private static Tile Snapshot(Tile tile) => new()
    {
        Id = tile.Id,
        Name = tile.Name,
        Icon = tile.Icon,
        Color = tile.Color,
        Hotkey = tile.Hotkey,
        Enabled = tile.Enabled,
        Repeat = tile.Repeat,
        Steps = tile.Steps?.Select(step => step is null ? null! : new ActionStep
        {
            Kind = step.Kind,
            Verification = step.Verification is { } verification ? verification with { } : null,
            UiAutomation = step.UiAutomation is { } ui ? ui with { Selector = ui.Selector with { Ancestors = ui.Selector.Ancestors?.ToArray() } } : null,
            Plugin = step.Plugin is { } plugin ? plugin with { Parameters = plugin.Parameters.Clone() } : null,
            Value = step.Value,
            Arguments = step.Arguments,
            WorkingDirectory = step.WorkingDirectory,
            AudioDeviceId = step.AudioDeviceId,
            DisplayDeviceId = step.DisplayDeviceId,
            DelayMs = step.DelayMs,
            Condition = step.Condition is { } condition ? condition with { } : null,
            OnConditionFalseSkip = step.OnConditionFalseSkip,
            OnError = step.OnError,
        }).ToList()!,
    };

    private void Publish(Guid tileId, string status, ExecutionPhase phase, int step = 0, int steps = 0, int repetition = 0, int repetitions = 0)
    {
        var progressHandlers = ProgressChanged;
        if (progressHandlers != null)
            foreach (Action<ExecutionProgress> handler in progressHandlers.GetInvocationList())
            {
                try { handler(new ExecutionProgress(tileId, phase, step, steps, repetition, repetitions)); }
                catch { /* Feedback must never interrupt execution. */ }
            }
        var handlers = StatusChanged;
        if (handlers is null) return;
        foreach (Action<Guid, string> handler in handlers.GetInvocationList())
        {
            try { handler(tileId, status); }
            catch { /* Status listeners must not interrupt action execution. */ }
        }
    }
}

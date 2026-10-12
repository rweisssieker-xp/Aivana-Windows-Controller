using System.Windows.Controls;
using System.Windows.Threading;
using Aivana.Core;
using Aivana.Windows;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private readonly Dictionary<TextBlock, Tile> _liveStatusSubscribers = [];
    private DispatcherTimer? _liveStatusTimer;
    private readonly CancellationTokenSource _liveStatusCancellation = new();
    private bool _liveStatusReading;
    private DateTime _lastLiveStatusRead;
    private Dictionary<Guid, string> _liveStatusCache = [];

    private void AddTileLiveStatus(Panel panel, Tile tile)
    {
        if (!tile.Steps.Any(s => s.Kind is ActionKind.Brightness or ActionKind.Volume or ActionKind.Obs)) return;
        var text = Label(_liveStatusCache.GetValueOrDefault(tile.Id) ?? L("Status wird gelesen …"), 10, "#69D8CC");
        text.ToolTip = L("Live-Status · wird alle 5 Sekunden aktualisiert");
        panel.Children.Add(text);
        text.Loaded += (_, _) =>
        {
            _liveStatusSubscribers[text] = tile;
            if (_liveStatusTimer == null)
            {
                _liveStatusTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
                _liveStatusTimer.Tick += (_, _) => RefreshTileLiveStatus();
                Closed += (_, _) => { _liveStatusTimer.Stop(); _liveStatusCancellation.Cancel(); _liveStatusSubscribers.Clear(); };
            }
            _liveStatusTimer.Start();
            RefreshTileLiveStatus();
        };
        text.Unloaded += (_, _) =>
        {
            _liveStatusSubscribers.Remove(text);
            if (_liveStatusSubscribers.Count == 0) _liveStatusTimer?.Stop();
        };
    }

    private async void RefreshTileLiveStatus()
    {
        if (!IsVisible || _liveStatusReading || _liveStatusCancellation.IsCancellationRequested || _liveStatusSubscribers.Count == 0 || DateTime.UtcNow - _lastLiveStatusRead < TimeSpan.FromSeconds(4)) return;
        _liveStatusReading = true;
        _lastLiveStatusRead = DateTime.UtcNow;
        // Copy only needed values before entering the worker; UI drafts may change meanwhile.
        var requests = _liveStatusSubscribers.Values.DistinctBy(t => t.Id).Select(t => (t.Id, Steps: t.Steps.Where(s => s.Kind is ActionKind.Brightness or ActionKind.Volume or ActionKind.Obs).Select(s => (s.Kind, s.Value, s.AudioDeviceId, s.DisplayDeviceId, s.WorkingDirectory)).ToArray())).ToArray();
        try
        {
            var cache = await Task.Run(() =>
            {
                IReadOnlyList<DisplayBrightnessInfo> displays = [];
                string? displayError = null;
                if (requests.Any(t => t.Steps.Any(s => s.Kind == ActionKind.Brightness)))
                    try { displays = DisplayBrightnessService.GetDisplays(_liveStatusCancellation.Token); }
                    catch (Exception ex) { displayError = ex.Message; }
                var audio = new Dictionary<(bool, string), string>();
                var obs = new Dictionary<string, string>();
                var result = new Dictionary<Guid, string>();
                foreach (var tile in requests)
                {
                    _liveStatusCancellation.Token.ThrowIfCancellationRequested();
                    var states = new List<string>();
                    foreach (var step in tile.Steps)
                    {
                        if (step.Kind == ActionKind.Brightness)
                        {
                            var selected = displays.Where(d => string.IsNullOrEmpty(step.DisplayDeviceId) || string.Equals(d.Id, step.DisplayDeviceId, StringComparison.OrdinalIgnoreCase)).ToArray();
                            states.Add(displayError ?? (selected.Length == 0 ? L("Bildschirm nicht verfügbar") : L("Helligkeit") + ": " + string.Join(" / ", selected.Select(d => d.Percent + "%"))));
                        }
                        else if (step.Kind == ActionKind.Obs)
                        {
                            var endpoint = step.WorkingDirectory ?? "";
                            if (!obs.TryGetValue(endpoint, out var state))
                            {
                                try
                                {
                                    using var timeout = CancellationTokenSource.CreateLinkedTokenSource(_liveStatusCancellation.Token);
                                    timeout.CancelAfter(TimeSpan.FromSeconds(3));
                                    var value = ObsActionClient.ReadStatusAsync(endpoint, timeout.Token).GetAwaiter().GetResult();
                                    state = "OBS: " + (value.Recording ? L("Aufnahme läuft") : L("Keine Aufnahme")) + " · " + (value.Streaming ? L("Stream läuft") : L("Kein Stream")) + " · " + value.Scene;
                                }
                                catch { state = L("OBS nicht verbunden"); }
                                obs[endpoint] = state;
                            }
                            states.Add(state);
                        }
                        else
                        {
                            var mic = step.Value.StartsWith("Mic", StringComparison.OrdinalIgnoreCase);
                            var key = (mic, step.AudioDeviceId ?? "");
                            if (!audio.TryGetValue(key, out var state))
                            {
                                try { var value = AudioEndpointControl.ReadState(mic, step.AudioDeviceId); state = (mic ? L("Mikrofon") : L("Lautstärke")) + ": " + (value.Muted ? L("Stumm") : value.VolumePercent + "%"); }
                                catch { state = L("Audiogerät nicht verfügbar"); }
                                audio[key] = state;
                            }
                            states.Add(state);
                        }
                    }
                    result[tile.Id] = string.Join(" · ", states.Distinct());
                }
                return result;
            }, _liveStatusCancellation.Token);
            if (_liveStatusCancellation.IsCancellationRequested) return;
            _liveStatusCache = cache;
            foreach (var entry in _liveStatusSubscribers) entry.Key.Text = cache.GetValueOrDefault(entry.Value.Id) ?? L("Status wird gelesen …");
        }
        catch (OperationCanceledException) { }
        finally { _liveStatusReading = false; }
    }
}

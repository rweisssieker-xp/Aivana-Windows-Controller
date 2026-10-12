namespace Aivana.Core;

public enum KeyGesture { Press, Tap, Hold, DoubleTap }

/// <summary>Deterministic edge-based gesture router. The owner calls Advance with monotonic milliseconds.</summary>
public sealed class KeyGestureRouter
{
    private sealed class DownState(List<Tile> tiles, long at, long order)
    {
        public List<Tile> Tiles = tiles;
        public long At = at;
        public long Order = order;
        public bool Held;
    }
    private sealed record PendingTap(Tile Tile, string Key, long Deadline);
    private readonly Dictionary<string, DownState> _down = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<PendingTap> _pending = [];
    private long _order;
    private long _generation;
    public event Action<Tile>? Triggered;
    public string ActiveLayer => _down.Values.OrderByDescending(s => s.Order)
        .SelectMany(s => s.Tiles).Select(t => t.HeldLayer).FirstOrDefault(s => !string.IsNullOrWhiteSpace(s)) ?? "";
    public void Reset() { _generation++; _down.Clear(); _pending.Clear(); }
    private static string Key(string device, int id) => device + "\0" + id;
    public void Down(string device, int id, IEnumerable<Tile> tiles, long now)
    {
        var key = Key(device, id);
        if (_down.ContainsKey(key)) return;
        Advance(now);
        var layer = ActiveLayer;
        var matches = tiles.Where(t => t.Enabled && ((t.CorsairBinding is { } b && b.KeyId == id
            && string.Equals(b.DeviceId, device, StringComparison.OrdinalIgnoreCase))
            || (t.KeyboardBinding is { } k && k.InputKey == id && string.Equals(k.DevicePath, device, StringComparison.OrdinalIgnoreCase))
            || (t.VendorBinding is { } vendor && vendor.KeyCode == id && string.Equals(vendor.Adapter + ":" + vendor.DeviceId, device, StringComparison.OrdinalIgnoreCase)))
            && string.Equals((t.KeyLayer ?? "").Trim(), layer.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
        var state = new DownState(matches, now, ++_order);
        _down[key] = state;
        foreach (var tile in matches.Where(t => t.KeyGesture == KeyGesture.Press && string.IsNullOrWhiteSpace(t.HeldLayer)))
            Triggered?.Invoke(tile);
    }
    public void Up(string device, int id, long now, bool allowTriggers = true)
    {
        var key = Key(device, id);
        Advance(now, allowTriggers);
        if (!_down.Remove(key, out var state)) return;
        if (!allowTriggers) { _pending.RemoveAll(p => p.Key == key); return; }
        if (state.Tiles.Any(t => !string.IsNullOrWhiteSpace(t.HeldLayer))) return;
        var tap = state.Tiles.FirstOrDefault(t => t.KeyGesture == KeyGesture.Tap);
        var twice = state.Tiles.FirstOrDefault(t => t.KeyGesture == KeyGesture.DoubleTap);
        var hold = state.Tiles.FirstOrDefault(t => t.KeyGesture == KeyGesture.Hold);
        if (state.Held || hold != null && now - state.At >= hold.HoldMilliseconds) return;
        var prior = _pending.FirstOrDefault(p => p.Key == key);
        if (twice != null && prior != null && now <= prior.Deadline)
        {
            _pending.RemoveAll(p => p.Key == key);
            Triggered?.Invoke(twice);
        }
        else if (twice != null)
            _pending.Add(new PendingTap(tap ?? twice, key, now + twice.DoubleTapMilliseconds));
        else if (tap != null) Triggered?.Invoke(tap);
    }
    public void Advance(long now, bool allowTriggers = true)
    {
        var generation = _generation;
        foreach (var state in _down.Values.ToArray())
        {
            if (generation != _generation) return;
            var hold = state.Tiles.FirstOrDefault(t => t.KeyGesture == KeyGesture.Hold && string.IsNullOrWhiteSpace(t.HeldLayer));
            if (!state.Held && hold != null && now - state.At >= hold.HoldMilliseconds)
            { state.Held = true; if (allowTriggers) Triggered?.Invoke(hold); }
        }
        var due = _pending.Where(p => now > p.Deadline).ToArray();
        foreach (var pending in due)
        {
            if (generation != _generation) return;
            _pending.Remove(pending);
            if (allowTriggers && pending.Tile.KeyGesture == KeyGesture.Tap) Triggered?.Invoke(pending.Tile);
        }
    }
}

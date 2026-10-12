using Aivana.Core;
using Aivana.Windows;
namespace Aivana.Desktop;
public sealed partial class DesktopController
{
    private readonly RawKeyboardBindingService _genericKeys = new();
    private Action<RawKeyboardEdge>? _genericCapture;
    private string? _genericCapturePath;
    private string _genericSnapshot = "";
    public RawKeyboardEdge? LastGenericKey => _genericKeys.LastEvent;
    public string GenericKeyStatus => _genericKeys.Status;
    private void AttachGenericKeys(IntPtr hwnd)
    {
        try { _genericKeys.Attach(hwnd); _genericKeys.Changed += OnGenericKey; _genericKeys.DevicesChanged += ResetKeyGestures; }
        catch (Exception ex) { Log(Aivana.Core.Localization.Translate("Geräte"), ex.Message); }
    }
    private void RefreshGenericKeys()
    {
        var snapshot = System.Text.Json.JsonSerializer.Serialize(new { Settings.ActiveProfileId, Paused, _genericCapturePath,
            Tiles = ActiveProfile.Tiles.Select(t => new { t.Id,t.Enabled,t.KeyboardBinding,t.KeyGesture,t.KeyLayer,t.HeldLayer,t.HoldMilliseconds,t.DoubleTapMilliseconds }) });
        if (_genericSnapshot == snapshot) return;
        _genericSnapshot = snapshot; _genericKeys.Reset(); ResetKeyGestures();
    }
    private void OnGenericKey(RawKeyboardEdge edge)
    {
        if (_disposed) return;
        if (_inputInjectionDepth > 0)
        {
            if (!edge.IsPressed) { _keyGestures.Up(edge.DevicePath, edge.VirtualKey, Environment.TickCount64, allowTriggers: false); _keyGestures.Up(edge.DevicePath, edge.InputKey, Environment.TickCount64, allowTriggers: false); }
            return;
        }
        if (_genericCapture is { } capture)
        {
            if (edge.IsPressed && StringComparer.OrdinalIgnoreCase.Equals(_genericCapturePath, edge.DevicePath)) capture(edge);
            return;
        }
        if (Paused) return;
        if (edge.InputKey != edge.VirtualKey)
        {
            if (edge.IsPressed) _keyGestures.Down(edge.DevicePath, edge.InputKey, ActiveProfile.Tiles, Environment.TickCount64);
            else _keyGestures.Up(edge.DevicePath, edge.InputKey, Environment.TickCount64);
        }
        if (edge.IsPressed) _keyGestures.Down(edge.DevicePath, edge.VirtualKey, ActiveProfile.Tiles, Environment.TickCount64);
        else _keyGestures.Up(edge.DevicePath, edge.VirtualKey, Environment.TickCount64);
    }
    public IDisposable BeginGenericKeyCapture(string path, Action<RawKeyboardEdge> captured)
    {
        if (_disposed || _genericCapture is not null) throw new InvalidOperationException(Aivana.Core.Localization.Translate("Tastenerkennung ist bereits aktiv."));
        _genericCapturePath = path; _genericCapture = captured;
        Register();
        return new GenericCaptureLease(this);
    }
    private sealed class GenericCaptureLease(DesktopController owner) : IDisposable
    {
        private DesktopController? _owner = owner;
        public void Dispose()
        {
            var controller = Interlocked.Exchange(ref _owner, null);
            if (controller is null) return;
            controller._genericCapture = null; controller._genericCapturePath = null; controller.Register();
        }
    }
    public IReadOnlyList<string> DiagnoseTile(Tile tile)
    {
        var result = new List<string>();
        void Add(string source) => result.Add(Aivana.Core.Localization.Translate(source));
        if (!tile.Enabled) Add("Diese Kachel ist deaktiviert.");
        if (Paused) Add("Die Steuerung ist pausiert.");
        if (!ActiveProfile.Tiles.Any(t => t.Id == tile.Id)) Add("Die Kachel gehört nicht zum aktiven Profil.");
        if (!string.Equals((tile.KeyLayer ?? "").Trim(), ActiveKeyLayer.Trim(), StringComparison.OrdinalIgnoreCase)) Add("Die benötigte Tastenebene ist nicht aktiv.");
        if (HotkeyErrors.TryGetValue(tile.Id, out var error)) result.Add(error);
        if (tile.CorsairBinding is { } corsair)
        {
            result.Add(CorsairKeyStatus);
            if (!_corsair.IsMacroKeyBindingConfigured(corsair.DeviceId, corsair.KeyId)) Add("Die Corsair-Taste ist aktuell nicht im SDK registriert.");
        }
        if (tile.KeyboardBinding is { } keyboard)
        {
            if (!Devices.Any(d => StringComparer.OrdinalIgnoreCase.Equals(d.Path, keyboard.DevicePath))) Add("Die zugeordnete Tastatur ist nicht angeschlossen.");
            result.Add(KeyboardBinding.KeyName(keyboard.VirtualKey));
        }
        if (tile.CorsairBinding is null && tile.KeyboardBinding is null && string.IsNullOrWhiteSpace(tile.Hotkey)) Add("Keine Taste zugeordnet. Die Kachel bleibt per Mausklick ausführbar.");
        if (result.Count == 0) Add("Keine erkennbaren Zuordnungsprobleme.");
        return result;
    }
}

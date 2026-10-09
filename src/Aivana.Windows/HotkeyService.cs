using System.Runtime.InteropServices;
using Aivana.Core;
using Localization = Aivana.Core.Localization;

namespace Aivana.Windows;

public sealed class HotkeyService : IDisposable
{
    private const int WmHotkey = 0x0312;
    private readonly IntPtr _hwnd;
    private readonly Dictionary<int, Guid> _ids = [];
    private readonly List<int> _registered = [];
    private const int FirstId = 0x5100;
    private int _nextId = FirstId;
    private int? _cancelId;
    private int? _paletteId;
    private bool _disposed;
    private bool _tilesSuspended;

    public HotkeyService(IntPtr hwnd) => _hwnd = hwnd != IntPtr.Zero ? hwnd : throw new ArgumentException("A valid window handle is required.", nameof(hwnd));
    public event Action<Guid>? Triggered;
    public event Action? CancelRequested;
    public event Action? PaletteRequested;

    public void SuspendTiles()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (_tilesSuspended) return;
        foreach (var id in _ids.Keys) UnregisterHotKey(_hwnd, id);
        _tilesSuspended = true;
    }

    public Dictionary<Guid, string> Register(IEnumerable<Tile> tiles, string cancelHotkey, string paletteHotkey = "")
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Clear();
        var errors = new Dictionary<Guid, string>();
        var comboOwners = new HashSet<(uint Mod, uint Key)>();
        // The emergency cancel chord owns its combination before tiles are considered.
        if (!string.IsNullOrWhiteSpace(cancelHotkey))
        {
            if (!HotkeyParser.TryParse(cancelHotkey, out var cancelMod, out var cancelKey, out var cancelError)) errors[Guid.Empty] = Localization.Translate((FormattableString)$"Abbruch-Hotkey: {cancelError}");
            else if (cancelKey == 0x7B) errors[Guid.Empty] = Localization.Translate("F12 ist von Windows für den Debugger reserviert und kann nicht als Hotkey verwendet werden.");
            else
            {
                comboOwners.Add((cancelMod, cancelKey));
                var id = _nextId++;
                if (RegisterHotKey(_hwnd, id, cancelMod | HotkeyParser.ModNoRepeat, cancelKey)) { _cancelId = id; _registered.Add(id); }
                else errors[Guid.Empty] = Localization.Translate((FormattableString)$"Windows konnte den Abbruch-Hotkey nicht registrieren (Fehler {Marshal.GetLastWin32Error()}).");
            }
        }
        if (!string.IsNullOrWhiteSpace(paletteHotkey))
        {
            if (!HotkeyParser.TryParse(paletteHotkey, out var mod, out var key, out var error)) errors[Guid.Empty] = Localization.Translate((FormattableString)$"Befehlspalette: {error}");
            else if (key == 0x7B || !comboOwners.Add((mod, key))) errors[Guid.Empty] = Localization.Translate("Das Kürzel der Befehlspalette ist reserviert oder bereits belegt.");
            else
            {
                var id = _nextId++;
                if (RegisterHotKey(_hwnd, id, mod | HotkeyParser.ModNoRepeat, key)) { _paletteId = id; _registered.Add(id); }
                else errors[Guid.Empty] = Localization.Translate((FormattableString)$"Windows konnte die Befehlspalette nicht registrieren (Fehler {Marshal.GetLastWin32Error()}).");
            }
        }
        foreach (var tile in tiles.Where(t => t.Enabled && !string.IsNullOrWhiteSpace(t.Hotkey)))
        {
            if (!HotkeyParser.TryParse(tile.Hotkey, out var mod, out var key, out var error)) { errors[tile.Id] = error; continue; }
            if (key == 0x7B) { errors[tile.Id] = Localization.Translate("F12 ist von Windows für den Debugger reserviert und kann nicht als Hotkey verwendet werden."); continue; }
            if (!comboOwners.Add((mod, key))) { errors[tile.Id] = Localization.Translate("Dieser Hotkey ist bereits vergeben."); continue; }
            var id = _nextId++;
            if (!RegisterHotKey(_hwnd, id, mod | HotkeyParser.ModNoRepeat, key))
            { errors[tile.Id] = Localization.Translate((FormattableString)$"Windows konnte {HotkeyParser.Normalize(tile.Hotkey)} nicht registrieren (Fehler {Marshal.GetLastWin32Error()})."); continue; }
            _ids[id] = tile.Id; _registered.Add(id);
        }
        return errors;
    }

    public bool ProcessMessage(int msg, IntPtr wParam, IntPtr lParam)
    {
        if (msg != WmHotkey) return false;
        var id = unchecked((int)wParam.ToInt64());
        if (_cancelId == id) CancelRequested?.Invoke();
        else if (_paletteId == id) PaletteRequested?.Invoke();
        else if (_ids.TryGetValue(id, out var tileId)) { if (!_tilesSuspended) Triggered?.Invoke(tileId); }
        else return false;
        return true;
    }

    public void Clear()
    {
        foreach (var id in _registered) UnregisterHotKey(_hwnd, id);
        _registered.Clear(); _ids.Clear(); _cancelId = null; _paletteId = null;
        _tilesSuspended = false;
        _nextId = FirstId;
    }

    public void Dispose() { if (_disposed) return; Clear(); _disposed = true; }

    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
}

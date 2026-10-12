namespace Aivana.Core;

/// <summary>Records command chords only. Plain character input is never retained.</summary>
public sealed class MacroRecording
{
    private readonly List<ActionStep> _steps = [];
    private long? _last;
    public IReadOnlyList<ActionStep> Steps => _steps;
    public void Capture(uint virtualKey, bool control, bool alt, bool shift, bool windows, long milliseconds)
    {
        // Require Ctrl/Alt/Win, or a function key: Shift+letters are text, not commands.
        if (!(control || alt || windows) && virtualKey is not (>= 0x70 and <= 0x87)) return;
        var key = virtualKey switch
        {
            >= 0x41 and <= 0x5A => ((char)virtualKey).ToString(),
            >= 0x30 and <= 0x39 => ((char)virtualKey).ToString(),
            >= 0x70 and <= 0x87 => "F" + (virtualKey - 0x6F),
            0x0D => "Enter", 0x09 => "Tab", 0x20 => "Space", 0x2E => "Delete",
            0x25 => "Left", 0x26 => "Up", 0x27 => "Right", 0x28 => "Down",
            0x24 => "Home", 0x23 => "End", 0x21 => "PageUp", 0x22 => "PageDown",
            _ => ""
        };
        if (key.Length == 0 || _steps.Count >= 1000) return;
        if (_last is { } prior && milliseconds > prior + 50)
            _steps.Add(new ActionStep { Kind = ActionKind.Delay, DelayMs = (int)Math.Min(60000, milliseconds - prior) });
        var modifiers = new List<string>();
        if (control) modifiers.Add("Ctrl"); if (alt) modifiers.Add("Alt");
        if (shift) modifiers.Add("Shift"); if (windows) modifiers.Add("Win");
        modifiers.Add(key);
        _steps.Add(new ActionStep { Kind = ActionKind.Shortcut, Value = string.Join("+", modifiers) });
        _last = milliseconds;
    }
}

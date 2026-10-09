using System.Globalization;
using Localization = Aivana.Core.Localization;

namespace Aivana.Windows;

public static class HotkeyParser
{
    public const uint ModAlt = 0x0001;
    public const uint ModControl = 0x0002;
    public const uint ModShift = 0x0004;
    public const uint ModWin = 0x0008;
    public const uint ModNoRepeat = 0x4000;

    public static string Normalize(string text)
    {
        if (!TryParse(text, out var modifiers, out var key, out _)) return (text ?? "").Trim();
        var names = new List<string>();
        if ((modifiers & ModControl) != 0) names.Add("Ctrl");
        if ((modifiers & ModAlt) != 0) names.Add("Alt");
        if ((modifiers & ModShift) != 0) names.Add("Shift");
        if ((modifiers & ModWin) != 0) names.Add("Win");
        names.Add(KeyName(key));
        return string.Join("+", names);
    }

    public static IReadOnlyList<string> ValidateShortcut(string text) =>
        TryParseShortcut(text, out _, out _, out var error) ? Array.Empty<string>() : new[] { error };

    public static bool TryParse(string text, out uint modifiers, out uint key, out string error) =>
        TryParseCore(text, requireModifier: true, out modifiers, out key, out error);

    public static bool TryParseShortcut(string text, out uint modifiers, out uint key, out string error) =>
        TryParseCore(text, requireModifier: false, out modifiers, out key, out error);

    private static bool TryParseCore(string text, bool requireModifier, out uint modifiers, out uint key, out string error)
    {
        modifiers = 0; key = 0; error = "";
        var parts = (text ?? "").Split('+', StringSplitOptions.TrimEntries);
        if (parts.Any(string.IsNullOrWhiteSpace)) { error = Localization.Translate("Die Tastenkombination enthält einen leeren Bestandteil. Verwende zum Beispiel:") + " Strg+Umschalt+A."; return false; }
        if (requireModifier && parts.Length < 2) { error = Localization.Translate("Der Hotkey benötigt mindestens einen Modifikator und eine Taste."); return false; }
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var part in parts[..^1])
        {
            if (!seen.Add(part)) { error = Localization.Translate((FormattableString)$"Doppelter Hotkey-Bestandteil: {part}."); return false; }
            var flag = part.ToLowerInvariant() switch
            {
                "ctrl" or "control" or "strg" => ModControl,
                "alt" => ModAlt,
                "shift" or "umschalt" => ModShift,
                "win" or "windows" or "meta" or "super" => ModWin,
                _ => 0u
            };
            if (flag == 0) { error = Localization.Translate((FormattableString)$"Unbekannter Modifikator: {part}."); return false; }
            if ((modifiers & flag) != 0) { error = Localization.Translate((FormattableString)$"Doppelter Modifikator: {part}."); return false; }
            modifiers |= flag;
        }
        if (!TryKey(parts[^1], out key)) { error = Localization.Translate((FormattableString)$"Nicht unterstützte Taste: {parts[^1]}."); return false; }
        if (key is 0x10 or 0x11 or 0x12 or 0x5B or 0x5C)
        { error = Localization.Translate("Die letzte Taste darf kein Modifikator sein. Füge eine normale Taste hinzu, zum Beispiel:") + " Strg+Umschalt+A."; return false; }
        if (requireModifier && modifiers == 0) { error = Localization.Translate("Mindestens ein Modifikator ist erforderlich."); return false; }
        return true;
    }

    internal static bool TryKey(string value, out uint key)
    {
        key = 0;
        var name = value.Trim().ToUpperInvariant();
        if (name.Length == 1 && name[0] is >= 'A' and <= 'Z') { key = name[0]; return true; }
        if (name.Length == 1 && name[0] is >= '0' and <= '9') { key = name[0]; return true; }
        if (name.Length == 2 && name[0] == 'D' && name[1] is >= '0' and <= '9') { key = name[1]; return true; }
        if (name.Length == 7 && name.StartsWith("NUMPAD", StringComparison.Ordinal) && name[6] is >= '0' and <= '9') { key = (uint)(0x60 + name[6] - '0'); return true; }
        if (name.Length >= 2 && name[0] == 'F' && int.TryParse(name.AsSpan(1), NumberStyles.None, CultureInfo.InvariantCulture, out var f) && f is >= 1 and <= 24)
        { key = (uint)(0x70 + f - 1); return true; }
        key = name switch
        {
            "ESC" or "ESCAPE" => 0x1B, "SPACE" or "LEERTASTE" => 0x20, "ENTER" or "RETURN" => 0x0D,
            "TAB" => 0x09, "BACKSPACE" or "BACK" => 0x08, "DELETE" or "DEL" => 0x2E,
            "INSERT" or "INS" => 0x2D, "HOME" => 0x24, "END" => 0x23, "PAGEUP" or "PGUP" => 0x21,
            "PAGEDOWN" or "PGDN" => 0x22, "UP" or "ARROWUP" => 0x26, "DOWN" or "ARROWDOWN" => 0x28,
            "LEFT" or "ARROWLEFT" => 0x25, "RIGHT" or "ARROWRIGHT" => 0x27,
            "PRINTSCREEN" or "PRTSC" or "SNAPSHOT" => 0x2C, "PAUSE" => 0x13, "CAPSLOCK" => 0x14, "APPS" => 0x5D, "SLEEP" => 0x5F,
            "NUMLOCK" => 0x90, "SCROLL" or "SCROLLLOCK" => 0x91, "HELP" => 0x2F, "SELECT" => 0x29, "EXECUTE" => 0x2B, "CANCEL" => 0x03, "CLEAR" => 0x0C,
            "CTRL" or "CONTROL" or "STRG" => 0x11, "SHIFT" or "UMSCHALT" => 0x10, "ALT" => 0x12,
            "WIN" or "WINDOWS" or "META" or "SUPER" => 0x5B,
            "BROWSERBACK" => 0xA6, "BROWSERFORWARD" => 0xA7, "BROWSERREFRESH" => 0xA8, "BROWSERSTOP" => 0xA9, "BROWSERSEARCH" => 0xAA, "BROWSERFAVORITES" => 0xAB, "BROWSERHOME" => 0xAC,
            "VOLUMEMUTE" => 0xAD, "VOLUMEDOWN" => 0xAE, "VOLUMEUP" => 0xAF, "MEDIANEXTTRACK" => 0xB0, "MEDIAPREVIOUSTRACK" => 0xB1,
            "MEDIASTOP" => 0xB2, "MEDIAPLAYPAUSE" => 0xB3, "LAUNCHMAIL" => 0xB4, "SELECTMEDIA" => 0xB5, "LAUNCHAPPLICATION1" => 0xB6, "LAUNCHAPPLICATION2" => 0xB7,
            "NUM0" => 0x60, "NUM1" => 0x61, "NUM2" => 0x62, "NUM3" => 0x63, "NUM4" => 0x64,
            "NUM5" => 0x65, "NUM6" => 0x66, "NUM7" => 0x67, "NUM8" => 0x68, "NUM9" => 0x69,
            "NUMMULTIPLY" => 0x6A, "NUMADD" => 0x6B, "NUMSUBTRACT" => 0x6D, "NUMDECIMAL" => 0x6E, "NUMDIVIDE" => 0x6F,
            "MULTIPLY" => 0x6A, "ADD" => 0x6B, "SUBTRACT" => 0x6D, "DECIMAL" => 0x6E, "DIVIDE" => 0x6F,
            "OEM1" or "SEMICOLON" => 0xBA, "OEMPLUS" or "PLUS" => 0xBB, "OEMCOMMA" or "COMMA" => 0xBC,
            "OEMMINUS" => 0xBD, "OEMPERIOD" or "PERIOD" => 0xBE, "OEM2" or "OEMQUESTION" or "SLASH" => 0xBF,
            "OEM3" or "BACKTICK" or "OEMTILDE" => 0xC0, "OEM4" or "LBRACKET" or "OEMOPENBRACKETS" => 0xDB, "OEM5" or "BACKSLASH" or "OEMPIPE" => 0xDC,
            "OEM6" or "RBRACKET" or "OEMCLOSEBRACKETS" => 0xDD, "OEM7" or "QUOTE" or "OEMQUOTES" => 0xDE, "OEM8" => 0xDF, "OEM102" or "OEMBACKSLASH" => 0xE2,
            _ => 0
        };
        return key != 0;
    }

    internal static string KeyName(uint key) => key switch
    {
        0x1B => "Escape", 0x20 => "Space", 0x0D => "Enter", 0x09 => "Tab", 0x08 => "Backspace",
        0x2E => "Delete", 0x2D => "Insert", 0x24 => "Home", 0x23 => "End", 0x21 => "PageUp", 0x22 => "PageDown", 0x2C => "PrintScreen", 0x13 => "Pause",
        0x26 => "Up", 0x28 => "Down", 0x25 => "Left", 0x27 => "Right", >= 0x70 and <= 0x87 => $"F{key - 0x70 + 1}",
        0x14 => "CapsLock", 0x5D => "Apps", 0x5F => "Sleep", 0x90 => "NumLock", 0x91 => "Scroll", 0x2F => "Help", 0x29 => "Select", 0x2B => "Execute", 0x03 => "Cancel", 0x0C => "Clear",
        0xA6 => "BrowserBack", 0xA7 => "BrowserForward", 0xA8 => "BrowserRefresh", 0xA9 => "BrowserStop", 0xAA => "BrowserSearch", 0xAB => "BrowserFavorites", 0xAC => "BrowserHome",
        0xAD => "VolumeMute", 0xAE => "VolumeDown", 0xAF => "VolumeUp", 0xB0 => "MediaNextTrack", 0xB1 => "MediaPreviousTrack", 0xB2 => "MediaStop", 0xB3 => "MediaPlayPause",
        0xB4 => "LaunchMail", 0xB5 => "SelectMedia", 0xB6 => "LaunchApplication1", 0xB7 => "LaunchApplication2",
        >= 0x60 and <= 0x69 => $"NumPad{key - 0x60}", 0x6A => "Multiply", 0x6B => "Add", 0x6D => "Subtract", 0x6E => "Decimal", 0x6F => "Divide",
        0xBA => "Oem1", 0xBB => "OemPlus", 0xBC => "OemComma", 0xBD => "OemMinus", 0xBE => "OemPeriod", 0xBF => "Oem2", 0xC0 => "Oem3",
        0xDB => "Oem4", 0xDC => "Oem5", 0xDD => "Oem6", 0xDE => "Oem7", 0xE2 => "Oem102",
        >= 0x41 and <= 0x5A => ((char)key).ToString(), >= 0x30 and <= 0x39 => ((char)key).ToString(), _ => $"VK{key:X2}"
    };
}

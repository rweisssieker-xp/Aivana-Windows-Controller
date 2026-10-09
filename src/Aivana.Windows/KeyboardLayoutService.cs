using System.Runtime.InteropServices;
using System.Text;

namespace Aivana.Windows;

/// <summary>A scan-code mapping under the current thread's Windows keyboard layout.</summary>
public sealed record KeyboardKeyMapping(
    ushort ScanCode,
    bool Extended,
    uint VirtualKey,
    string HotkeyToken,
    string Legend,
    string ShiftedLegend,
    bool IsDeadKey,
    bool ShiftedIsDeadKey,
    bool SharesGlobalHotkeyToken,
    string LayoutId);

/// <summary>
/// Maps a physical Set-1 scan code to the virtual key and display legends Windows currently uses.
/// This is a layout query only; it does not capture or suppress keyboard input.
/// </summary>
public sealed class KeyboardLayoutService
{
    private const uint MapVkFromScanCodeEx = 3;
    private const uint ToUnicodeNoStateChange = 0x0004;

    /// <summary>
    /// Resolves a raw Set-1 make code using the calling thread's current keyboard layout.
    /// For E0-prefixed codes, pass the low scan byte and set <paramref name="extended"/> to true.
    /// </summary>
    public bool TryGetMapping(ushort scanCode, bool extended, out KeyboardKeyMapping mapping)
    {
        mapping = null!;
        if (scanCode is 0 or > 0xFF) return false;

        var layout = GetKeyboardLayout(0);
        if (layout == IntPtr.Zero) return false;
        var scan = (uint)scanCode | (extended ? 0xE000u : 0u);
        var virtualKey = MapVirtualKeyEx(scan, MapVkFromScanCodeEx, layout);
        if (!extended && TryGetNumpadVirtualKey(scanCode, out var numpadKey)) virtualKey = numpadKey;
        if (extended && scanCode == 0x35 && virtualKey == 0) virtualKey = 0x6F; // Numpad divide's E0 35 make code.
        if (extended && scanCode == 0x37 && virtualKey == 0) virtualKey = 0x2C; // Print Screen's E0 37 make code.
        if (virtualKey == 0) return false;

        var token = GetHotkeyToken(virtualKey);
        if (token is null) return false;
        var baseLegend = Translate(virtualKey, scanCode, shifted: false, layout);
        var shiftedLegend = Translate(virtualKey, scanCode, shifted: true, layout);
        var hkl = unchecked((ulong)layout.ToInt64());
        mapping = new KeyboardKeyMapping(
            scanCode,
            extended,
            virtualKey,
            token,
            baseLegend.Text.Length > 0 ? baseLegend.Text : token,
            shiftedLegend.Text.Length > 0 ? shiftedLegend.Text : baseLegend.Text.Length > 0 ? baseLegend.Text : token,
            baseLegend.IsDeadKey,
            shiftedLegend.IsDeadKey,
            SharesGlobalToken(virtualKey, scanCode),
            $"0x{hkl:X}");
        return true;
    }

    private static string? GetHotkeyToken(uint virtualKey)
    {
        var token = virtualKey switch
        {
            0x10 or 0xA0 or 0xA1 => "Shift",
            0x11 or 0xA2 or 0xA3 => "Ctrl",
            0x12 or 0xA4 or 0xA5 => "Alt",
            0x5B or 0x5C => "Win",
            _ => HotkeyParser.KeyName(virtualKey)
        };
        if (token is "Shift" or "Ctrl" or "Alt" or "Win") return token;
        return HotkeyParser.TryKey(token, out var parsed) && parsed == virtualKey ? token : null;
    }

    private static bool TryGetNumpadVirtualKey(ushort scanCode, out uint virtualKey)
    {
        virtualKey = scanCode switch
        {
            0x37 => 0x6A, // Multiply
            0x47 => 0x67, // NumPad7
            0x48 => 0x68, // NumPad8
            0x49 => 0x69, // NumPad9
            0x4A => 0x6D, // Subtract
            0x4B => 0x64, // NumPad4
            0x4C => 0x65, // NumPad5
            0x4D => 0x66, // NumPad6
            0x4E => 0x6B, // Add
            0x4F => 0x61, // NumPad1
            0x50 => 0x62, // NumPad2
            0x51 => 0x63, // NumPad3
            0x52 => 0x60, // NumPad0
            0x53 => 0x6E, // Decimal
            _ => 0
        };
        return virtualKey != 0;
    }

    private static bool SharesGlobalToken(uint virtualKey, ushort scanCode) =>
        (virtualKey == 0x0D && scanCode == 0x1C) || virtualKey is 0x10 or 0x11 or 0x12 or 0xA0 or 0xA1 or 0xA2 or 0xA3 or 0xA4 or 0xA5 or 0x5B or 0x5C;

    private static (string Text, bool IsDeadKey) Translate(uint virtualKey, ushort scanCode, bool shifted, IntPtr layout)
    {
        var keyState = new byte[256];
        if (shifted) keyState[0x10] = 0x80;
        var output = new StringBuilder(8);
        var count = ToUnicodeEx(virtualKey, scanCode, keyState, output, output.Capacity, ToUnicodeNoStateChange, layout);
        var deadKey = count < 0;
        var length = Math.Min(Math.Abs(count), output.Length);
        if (length == 0) return ("", deadKey);
        var text = output.ToString(0, length);
        return text.Any(char.IsControl) ? ("", deadKey) : (text, deadKey);
    }

    [DllImport("user32.dll", EntryPoint = "GetKeyboardLayout", SetLastError = true)]
    private static extern IntPtr GetKeyboardLayout(uint threadId);

    [DllImport("user32.dll", EntryPoint = "MapVirtualKeyExW", SetLastError = true)]
    private static extern uint MapVirtualKeyEx(uint code, uint mapType, IntPtr keyboardLayout);

    [DllImport("user32.dll", EntryPoint = "ToUnicodeEx", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int ToUnicodeEx(uint virtualKey, uint scanCode, byte[] keyboardState, StringBuilder output, int outputCapacity, uint flags, IntPtr keyboardLayout);
}

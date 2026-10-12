namespace Aivana.Core;

/// <summary>A physical keyboard path and a standard Windows special virtual key.</summary>
public sealed record KeyboardBinding(string DevicePath, int VirtualKey)
{
    public int ScanCode { get; init; }
    public bool Extended { get; init; }
    public bool Extended1 { get; init; }
    public int InputKey => ScanCode > 0 ? PhysicalKey(ScanCode, Extended, Extended1) : VirtualKey;
    public static int PhysicalKey(int scanCode, bool extended, bool extended1 = false) => 0x10000 | scanCode | (extended ? 0x100 : 0) | (extended1 ? 0x200 : 0);
    public bool IsValid => ScanCode is >= 1 and <= 255 || ScanCode == 0 && IsSupported(VirtualKey);
    public static bool IsSupported(int key) => key is >= 0x7C and <= 0x87 or >= 0xAD and <= 0xB3;
    public static string KeyName(int key) => key is >= 0x7C and <= 0x87 ? $"F{key - 0x70 + 1}" : key switch
    { 0xAD => "Mute", 0xAE => "Volume down", 0xAF => "Volume up", 0xB0 => "Next track", 0xB1 => "Previous track", 0xB2 => "Stop", 0xB3 => "Play / Pause", _ => $"VK {key:X2}" };
}

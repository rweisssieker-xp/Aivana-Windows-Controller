using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Aivana.Core;
namespace Aivana.Windows;

public sealed record RawKeyboardEdge(string DevicePath, int VirtualKey, bool IsPressed, int ScanCode = 0, bool Extended = false, bool Extended1 = false)
{
    public int InputKey => ScanCode > 0 ? KeyboardBinding.PhysicalKey(ScanCode, Extended, Extended1) : VirtualKey;
}
/// <summary>Observes standard keyboard input without consuming or disabling normal typing.</summary>
public sealed class RawKeyboardBindingService : IDisposable
{
    private readonly HashSet<(IntPtr Device, int Key)> _held = [];
    private readonly Dictionary<IntPtr, string> _paths = [];
    private bool _attached;
    public event Action<RawKeyboardEdge>? Changed;
    public event Action? DevicesChanged;
    public RawKeyboardEdge? LastEvent { get; private set; }
    public string Status { get; private set; } = "";
    public void Attach(IntPtr hwnd)
    {
        var devices = new[] { new RawDevice { UsagePage = 1, Usage = 6, Flags = 0x100 | 0x2000, Target = hwnd } };
        if (!RegisterRawInputDevices(devices, 1, (uint)Marshal.SizeOf<RawDevice>())) throw new Win32Exception(Marshal.GetLastWin32Error());
        _attached = true;
        Status = "Windows Raw Input";
    }
    public void Reset() => _held.Clear();
    public void ProcessMessage(int message, IntPtr wParam, IntPtr lParam, bool suppress)
    {
        if (message == 0xFE) { _paths.Clear(); Reset(); DevicesChanged?.Invoke(); return; }
        if (message != 0xFF) return;
        uint size = 0;
        var headerSize = (uint)Marshal.SizeOf<Header>();
        if (GetRawInputData(lParam, 0x10000003, IntPtr.Zero, ref size, headerSize) == uint.MaxValue || size < headerSize + 16 || size > 65536) return;
        var buffer = Marshal.AllocHGlobal((int)size);
        try
        {
            if (GetRawInputData(lParam, 0x10000003, buffer, ref size, headerSize) == uint.MaxValue) return;
            var header = Marshal.PtrToStructure<Header>(buffer);
            // Synthetic SendInput events have no physical device and cannot recursively execute bindings.
            if (header.Type != 1 || header.Device == IntPtr.Zero) return;
            var keyboard = Marshal.PtrToStructure<Keyboard>(IntPtr.Add(buffer, (int)headerSize));
            if (keyboard.VirtualKey == 255 || keyboard.MakeCode > 255 || keyboard.MakeCode == 0 && !KeyboardBinding.IsSupported(keyboard.VirtualKey)) return;
            var pressed = (keyboard.Flags & 1) == 0;
            var inputKey = keyboard.MakeCode > 0 ? KeyboardBinding.PhysicalKey(keyboard.MakeCode, (keyboard.Flags & 2) != 0, (keyboard.Flags & 4) != 0) : keyboard.VirtualKey;
            if (!TryAcceptEdge(header.Device, inputKey, pressed, suppress)) return;
            if (!_paths.TryGetValue(header.Device, out var path))
            {
                uint chars = 0;
                GetRawInputDeviceInfo(header.Device, 0x20000007, null, ref chars);
                if (chars == 0 || chars > 32768) return;
                var name = new StringBuilder((int)chars + 1);
                if (GetRawInputDeviceInfo(header.Device, 0x20000007, name, ref chars) == uint.MaxValue) return;
                _paths[header.Device] = path = name.ToString();
            }
            LastEvent = new(path, keyboard.VirtualKey, pressed, keyboard.MakeCode, (keyboard.Flags & 2) != 0, (keyboard.Flags & 4) != 0);
            Changed?.Invoke(LastEvent);
        }
        finally { Marshal.FreeHGlobal(buffer); }
    }
    internal bool TryAcceptEdge(IntPtr device, int key, bool pressed, bool suppress)
    {
        // Physical releases must still clear held layers while our own shortcut is being sent.
        // Ignored repeat downs must not discard a key that was held before injection started.
        if (device == IntPtr.Zero || !(KeyboardBinding.IsSupported(key) || key is > 0x10000 and <= 0x103FF) || (suppress && pressed)) return false;
        var pair = (device, key);
        return pressed ? _held.Add(pair) : _held.Remove(pair);
    }
    public void Dispose()
    {
        if (_attached) RegisterRawInputDevices([new RawDevice { UsagePage = 1, Usage = 6, Flags = 1 }], 1, (uint)Marshal.SizeOf<RawDevice>());
        _attached = false; Reset(); _paths.Clear();
    }
    [StructLayout(LayoutKind.Sequential)] private struct RawDevice { public ushort UsagePage, Usage; public uint Flags; public IntPtr Target; }
    [StructLayout(LayoutKind.Sequential)] private struct Header { public uint Type, Size; public IntPtr Device, Param; }
    [StructLayout(LayoutKind.Sequential)] private struct Keyboard { public ushort MakeCode, Flags, Reserved, VirtualKey; public uint Message, ExtraInformation; }
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool RegisterRawInputDevices(RawDevice[] devices, uint count, uint size);
    [DllImport("user32.dll")] private static extern uint GetRawInputData(IntPtr input, uint command, IntPtr data, ref uint size, uint headerSize);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern uint GetRawInputDeviceInfo(IntPtr device, uint command, StringBuilder? data, ref uint size);
}

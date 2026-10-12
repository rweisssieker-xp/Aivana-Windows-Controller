using System.ComponentModel;
using System.Runtime.InteropServices;
using Aivana.Core;

namespace Aivana.Windows;

/// <summary>Explicit, temporary keyboard hook. Never retains typed text or injected events.</summary>
public sealed class KeyboardMacroRecorder : IDisposable
{
    private readonly HookProc _callback;
    private readonly HashSet<uint> _pressed = [];
    private IntPtr _hook;
    public MacroRecording Recording { get; } = new();
    public event Action? Changed;
    public event Action? StopRequested;
    public KeyboardMacroRecorder()
    {
        _callback = OnKey;
        _hook = SetWindowsHookEx(13, _callback, GetModuleHandle(null), 0);
        if (_hook == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
    }
    private IntPtr OnKey(int code, IntPtr message, IntPtr data)
    {
        if (code >= 0)
        {
            var key = Marshal.PtrToStructure<KeyboardData>(data);
            if ((key.Flags & 0x12) == 0)
            {
                var down = message.ToInt64() is 0x100 or 0x104;
                var up = message.ToInt64() is 0x101 or 0x105;
                if (up) _pressed.Remove(key.Key);
                if (down && _pressed.Add(key.Key))
                {
                    if (key.Key == 0x1B) { StopRequested?.Invoke(); }
                    else
                    {
                        Recording.Capture(key.Key, Held(0x11, 0xA2, 0xA3), Held(0x12, 0xA4, 0xA5), Held(0x10, 0xA0, 0xA1), Held(0x5B, 0x5C), Environment.TickCount64);
                        Changed?.Invoke();
                    }
                }
            }
        }
        return CallNextHookEx(_hook, code, message, data);
    }
    private bool Held(params uint[] keys) => keys.Any(k => (GetAsyncKeyState((int)k) & 0x8000) != 0 || _pressed.Contains(k));
    public void Dispose() { if (_hook != IntPtr.Zero) { UnhookWindowsHookEx(_hook); _hook = IntPtr.Zero; } _pressed.Clear(); }
    private delegate IntPtr HookProc(int code, IntPtr message, IntPtr data);
    [StructLayout(LayoutKind.Sequential)] private struct KeyboardData { public uint Key, Scan, Flags, Time; public UIntPtr Extra; }
    [DllImport("user32.dll", SetLastError = true)] private static extern IntPtr SetWindowsHookEx(int id, HookProc callback, IntPtr module, uint thread);
    [DllImport("user32.dll")] private static extern bool UnhookWindowsHookEx(IntPtr hook);
    [DllImport("user32.dll")] private static extern IntPtr CallNextHookEx(IntPtr hook, int code, IntPtr message, IntPtr data);
    [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int key);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string? module);
}

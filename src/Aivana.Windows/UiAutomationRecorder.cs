using System.Runtime.InteropServices;
namespace Aivana.Windows;
/// <summary>Explicit, temporary click observation; never captures text or suppresses input.</summary>
public sealed class UiAutomationRecorder : IDisposable
{
    private readonly Hook _callback;
    private IntPtr _handle;
    public event Action<int,int,long>? Clicked;
    public UiAutomationRecorder()
    {
        _callback=OnMouse;_handle=SetWindowsHookEx(14,_callback,GetModuleHandle(null),0);
        if(_handle==IntPtr.Zero)throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
    }
    private IntPtr OnMouse(int code,IntPtr wParam,IntPtr lParam)
    {
        if(code>=0 && wParam.ToInt32()==0x202)
        {
            var data=Marshal.PtrToStructure<MouseData>(lParam);
            if((data.Flags & 1)==0)try { Clicked?.Invoke(data.X,data.Y,Environment.TickCount64); } catch { }
        }
        return CallNextHookEx(_handle,code,wParam,lParam);
    }
    public void Dispose() { if(_handle!=IntPtr.Zero) { UnhookWindowsHookEx(_handle);_handle=IntPtr.Zero; }GC.KeepAlive(_callback); }
    [StructLayout(LayoutKind.Sequential)] private struct MouseData { public int X,Y;public uint Data,Flags,Time;public UIntPtr Extra; }
    private delegate IntPtr Hook(int code,IntPtr wParam,IntPtr lParam);
    [DllImport("user32.dll",SetLastError=true)] private static extern IntPtr SetWindowsHookEx(int id,Hook callback,IntPtr module,uint thread);
    [DllImport("user32.dll")] private static extern bool UnhookWindowsHookEx(IntPtr hook);
    [DllImport("user32.dll")] private static extern IntPtr CallNextHookEx(IntPtr hook,int code,IntPtr wp,IntPtr lp);
    [DllImport("kernel32.dll",CharSet=CharSet.Unicode)] private static extern IntPtr GetModuleHandle(string? name);
}

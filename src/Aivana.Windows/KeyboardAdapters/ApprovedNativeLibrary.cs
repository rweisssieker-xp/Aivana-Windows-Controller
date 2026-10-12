using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
namespace Aivana.Windows.KeyboardAdapters;
public sealed class ApprovedNativeLibrary : IDisposable
{
    private IntPtr _handle;
    public static string Root => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MAKRIVO","SDKs");
    public ApprovedNativeLibrary(string adapter, string expectedHash)
    {
        var file=Path.Combine(Root,adapter,"adapter.dll");
        if(!File.Exists(file) || !Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(file))).Equals(expectedHash,StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("SDK fehlt oder wurde verändert. Erneut freigeben.");
        _handle=LoadLibraryEx(file,IntPtr.Zero,0x100 | 0x800);
        if(_handle==IntPtr.Zero) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(),"SDK konnte nicht geladen werden (x64 und Abhängigkeiten prüfen).");
    }
    public T Get<T>(string name) where T:Delegate => Marshal.GetDelegateForFunctionPointer<T>(NativeLibrary.GetExport(_handle,name));
    public void Dispose() { if(_handle!=IntPtr.Zero) { NativeLibrary.Free(_handle);_handle=IntPtr.Zero; } }
    [DllImport("kernel32.dll",EntryPoint="LoadLibraryExW",CharSet=CharSet.Unicode,SetLastError=true)] private static extern IntPtr LoadLibraryEx(string path,IntPtr file,uint flags);
}

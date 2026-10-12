using System.Runtime.InteropServices;
using Aivana.Core;
namespace Aivana.Windows.KeyboardAdapters;
public sealed class LogitechGAdapter : IDisposable
{
    private readonly ApprovedNativeLibrary _library;
    private readonly Callback _callback;
    private readonly Shutdown _shutdown;
    private bool _initialized;
    public event Action<VendorKeyEdge>? Changed;
    public LogitechGAdapter(string hash)
    {
        _library=new("logitech-g",hash);_callback=OnKey;
        try { _shutdown=_library.Get<Shutdown>("LogiGkeyShutdown"); _initialized=_library.Get<Initialize>("LogiGkeyInitWithoutContext")(_callback); if(!_initialized) throw new InvalidOperationException("Logitech G-Key SDK ist nicht verfügbar. G HUB/LGS und Modell prüfen."); }
        catch { _library.Dispose();throw; }
    }
    public static (int Key,int Mode,bool Pressed,bool Mouse) Decode(uint code) => ((int)(code & 255),(int)((code>>9)&3),(code & 256)!=0,(code & 2048)!=0);
    private void OnKey(uint code,IntPtr name,IntPtr context)
    {
        try { var key=Decode(code);if(!key.Mouse && key.Key is >=1 and <=29) Changed?.Invoke(new("logitech-g","gkeys",key.Key | key.Mode<<8,key.Pressed)); } catch { }
    }
    public void Dispose() { if(_initialized) { _initialized=false;_shutdown(); } GC.KeepAlive(_callback);_library.Dispose(); }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void Callback(uint code,IntPtr name,IntPtr context);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.Bool)] private delegate bool Initialize(Callback callback);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void Shutdown();
}

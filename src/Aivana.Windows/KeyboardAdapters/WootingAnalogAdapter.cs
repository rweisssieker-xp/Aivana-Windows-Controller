using System.Runtime.InteropServices;
using Aivana.Core;
namespace Aivana.Windows.KeyboardAdapters;
public sealed class WootingAnalogAdapter : IDisposable
{
    private readonly ApprovedNativeLibrary _library;
    private readonly NoArgs _uninitialize;
    private readonly Devices _devices;
    private readonly ReadBuffer _read;
    private readonly AnalogThreshold _threshold=new();
    private readonly VendorIntegrationSettings _settings;
    public event Action<VendorKeyEdge>? Changed;
    public WootingAnalogAdapter(string hash,VendorIntegrationSettings settings)
    {
        _settings=settings;_library=new("wooting",hash);
        try
        {
            _uninitialize=_library.Get<NoArgs>("wooting_analog_uninitialise");
            _devices=_library.Get<Devices>("wooting_analog_get_connected_devices_info");
            _read=_library.Get<ReadBuffer>("wooting_analog_read_full_buffer_device");
            if(_library.Get<NoArgs>("wooting_analog_initialise")()<0) throw new InvalidOperationException("Wooting SDK: keine Analog-Geräte/Plugins verfügbar.");
        }
        catch { _library.Dispose();throw; }
    }
    public void Poll()
    {
        var pointers=new IntPtr[16];var count=_devices(pointers,(uint)pointers.Length);var seen=new HashSet<(string,int)>();
        var keys=new ushort[256];var values=new float[256];
        for(var i=0;i<Math.Clamp(count,0,16);i++)
        {
            var device=Marshal.PtrToStructure<DeviceInfo>(pointers[i]);var id=device.Id.ToString("X16");
            var length=_read(keys,values,256,device.Id);
            for(var k=0;k<Math.Clamp(length,0,256);k++)
            {
                seen.Add((id,keys[k])); var edge=_threshold.Update(id,keys[k],values[k],_settings.AnalogPress,_settings.AnalogRelease);
                if(edge is bool pressed) Changed?.Invoke(new("wooting",id,keys[k],pressed));
            }
        }
        // SDK buffer returns active values; reconcile releases, including unplugged devices.
        foreach(var held in _held.Except(seen).ToArray()) if(_threshold.Update(held.Item1,held.Item2,0,_settings.AnalogPress,_settings.AnalogRelease)==false) Changed?.Invoke(new("wooting",held.Item1,held.Item2,false));
        _held=seen;
    }
    private HashSet<(string,int)> _held=[];
    public void Dispose() { foreach(var held in _threshold.Reset()) Changed?.Invoke(new("wooting",held.Device,held.Key,false));_uninitialize();_library.Dispose(); }
    [StructLayout(LayoutKind.Sequential)] private struct DeviceInfo { public ushort Vid,Pid;public IntPtr Manufacturer,Name;public ulong Id;public int Type,Support; }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int NoArgs();
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int Devices([Out] IntPtr[] devices,uint length);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate int ReadBuffer([Out] ushort[] keys,[Out] float[] values,uint length,ulong deviceId);
}

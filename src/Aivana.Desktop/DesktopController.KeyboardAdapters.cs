using Aivana.Core;
using Aivana.Windows.KeyboardAdapters;
using System.Windows.Threading;
namespace Aivana.Desktop;
public sealed partial class DesktopController
{
    private LogitechGAdapter? _logitech;
    private WootingAnalogAdapter? _wooting;
    private readonly DispatcherTimer _analogTimer=new() { Interval=TimeSpan.FromMilliseconds(20) };
    public VendorKeyEdge? LastVendorKey { get; private set; }
    public string VendorAdapterStatus { get; private set; }="";
    public event Action<VendorKeyEdge>? VendorKeyObserved;
    public void ReloadKeyboardAdapters()
    {
        _analogTimer.Stop();_logitech?.Dispose();_wooting?.Dispose();_logitech=null;_wooting=null;ResetKeyGestures();
        var statuses=new List<string>();
        if(Settings.VendorIntegrations.ApprovedLibraries.TryGetValue("logitech-g",out var logitech))
            try { _logitech=new(logitech);_logitech.Changed+=OnVendorKey;statuses.Add("Logitech G-Key SDK initialisiert; Modell-Unterstützung und Eingabe am Gerät prüfen."); } catch(Exception ex) { statuses.Add(ex.Message); }
        if(Settings.VendorIntegrations.ApprovedLibraries.TryGetValue("wooting",out var wooting))
            try { _wooting=new(wooting,Settings.VendorIntegrations);_wooting.Changed+=OnVendorKey;_analogTimer.Tick-=PollAnalog;_analogTimer.Tick+=PollAnalog;_analogTimer.Start();statuses.Add("Wooting Analog SDK initialisiert."); } catch(Exception ex) { statuses.Add(ex.Message); }
        VendorAdapterStatus=string.Join("\n",statuses);
        _executor.VendorLibraryHash=adapter=>Settings.VendorIntegrations.ApprovedLibraries.GetValueOrDefault(adapter);
    }
    private void PollAnalog(object? sender,EventArgs e) { try { _wooting?.Poll(); } catch(Exception ex) { _analogTimer.Stop();ResetKeyGestures();VendorAdapterStatus=ex.Message; } }
    private void OnVendorKey(VendorKeyEdge edge)
    {
        var dispatcher=System.Windows.Application.Current.Dispatcher;
        if(!dispatcher.CheckAccess()) { dispatcher.BeginInvoke(()=>OnVendorKey(edge));return; }
        if(_disposed) return;
        LastVendorKey=edge;VendorKeyObserved?.Invoke(edge);
        if(Paused || _inputInjectionDepth>0) { if(!edge.Pressed) _keyGestures.Up(edge.Adapter+":"+edge.DeviceId,edge.KeyCode,Environment.TickCount64,false);return; }
        if(edge.Pressed) _keyGestures.Down(edge.Adapter+":"+edge.DeviceId,edge.KeyCode,ActiveProfile.Tiles,Environment.TickCount64);
        else _keyGestures.Up(edge.Adapter+":"+edge.DeviceId,edge.KeyCode,Environment.TickCount64);
    }
    private void DisposeKeyboardAdapters() { _analogTimer.Stop();_logitech?.Dispose();_wooting?.Dispose(); }
}

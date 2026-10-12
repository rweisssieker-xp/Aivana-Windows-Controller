using Aivana.Core;
namespace Aivana.Desktop;
public sealed partial class DesktopController
{
    internal void RunGestureInjectionChecks()
    {
        var originalDepth = _inputInjectionDepth;
        try
        {
            Register();
            var shift = new Tile { Name="Injection-layer fixture", CorsairBinding=new("injection-fixture",6), HeldLayer="Injection test" };
            _keyGestures.Down("injection-fixture",6,[shift],Environment.TickCount64);
            if (ActiveKeyLayer!="Injection test") throw new InvalidOperationException("Held layer fixture did not activate.");
            _inputInjectionDepth = originalDepth + 1;
            Register();
            if (ActiveKeyLayer!="Injection test") throw new InvalidOperationException("Input injection registration discarded the physically held layer.");
            _keyGestures.Advance(Environment.TickCount64,allowTriggers:false);
            _inputInjectionDepth = originalDepth;
            Register();
            if (ActiveKeyLayer!="Injection test") throw new InvalidOperationException("Input injection completion discarded the physically held layer.");
            _keyGestures.Up("injection-fixture",6,Environment.TickCount64,allowTriggers:false);
            if (ActiveKeyLayer!="") throw new InvalidOperationException("Release during suppressed execution left a held layer stuck.");
        }
        finally { _inputInjectionDepth=originalDepth; ResetKeyGestures(); Register(); }
    }
}

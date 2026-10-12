using Aivana.Core;
internal static class GestureChecks
{
    public static Task RunAsync()
    {
        static Tile Make(KeyGesture gesture, int key = 1, string layer = "", string held = "") => new() { Name = gesture.ToString(), KeyGesture=gesture, CorsairBinding=new("device",key), KeyLayer=layer,HeldLayer=held,Steps=[new() {Kind=ActionKind.Delay}] };
        var router = new KeyGestureRouter(); var hits = new List<Tile>(); router.Triggered += hits.Add;
        var press=Make(KeyGesture.Press); router.Down("device",1,[press],0); router.Down("device",1,[press],50); Check(hits.SequenceEqual(new[]{press}),"default press immediate, repeat suppressed"); router.Up("device",1,100);
        hits.Clear(); var tap=Make(KeyGesture.Tap); var hold=Make(KeyGesture.Hold); var twice=Make(KeyGesture.DoubleTap); Tile[] gestures=[tap,hold,twice];
        router.Down("device",1,gestures,1000); router.Up("device",1,1100); router.Advance(1401); Check(hits.SequenceEqual(new[]{tap}),"single tap delayed only when double configured");
        hits.Clear(); router.Down("device",1,gestures,2000); router.Up("device",1,2050); router.Down("device",1,gestures,2100); router.Up("device",1,2150); router.Advance(2600); Check(hits.SequenceEqual(new[]{twice}),"double fires without extra tap");
        hits.Clear(); router.Down("device",1,gestures,3000); router.Advance(3500); router.Advance(3600); router.Up("device",1,3700); router.Advance(4100); Check(hits.SequenceEqual(new[]{hold}),"hold once without tap");
        hits.Clear(); var shift=Make(KeyGesture.Press,6,held:"Media"); var baseTile=Make(KeyGesture.Press); var layered=Make(KeyGesture.Press,layer:"Media"); Tile[] layers=[shift,baseTile,layered];
        router.Down("device",6,layers,5000); Check(router.ActiveLayer=="Media","held layer activated"); router.Down("device",1,layers,5100); router.Up("device",1,5150); router.Up("device",6,5200); router.Down("device",1,layers,5300); router.Up("device",1,5350); Check(hits.SequenceEqual(new[]{layered,baseTile}) && router.ActiveLayer=="","release restores base, shift executes no macro");
        hits.Clear(); router.Down("device",1,gestures,6000); router.Up("device",1,6050); router.Reset(); router.Advance(7000); Check(hits.Count==0 && router.ActiveLayer=="","reset cancels pending taps and layers");
        router.Down("device",6,layers,7100); router.Reset(); Check(router.ActiveLayer=="","reset releases held layer");
        Check(CorsairBindingValidator.ValidateProfileBindings(new Profile{Tiles=[tap,hold,twice]}).Count==0,"distinct gestures permitted");
        Check(CorsairBindingValidator.ValidateProfileBindings(new Profile{Tiles=[tap,press]}).Count>0,"immediate and deferred gesture conflict rejected");
        Check(CorsairBindingValidator.ValidateProfileBindings(new Profile{Tiles=layers.ToList()}).Count==0,"separate layers permitted");
        Check(CorsairBindingValidator.ValidateProfileBindings(new Profile{Tiles=[layered]}).Count>0,"unreachable layer rejected");
        hits.Clear(); router.Reset(); router.Down("device",6,layers,5500);
        router.Advance(5600,allowTriggers:false); Check(router.ActiveLayer=="Media","input injection preserves physical held layer");
        router.Up("device",6,5700,allowTriggers:false); Check(router.ActiveLayer=="" && hits.Count==0,"release during injection clears held layer without macro");
        router.Down("device",1,gestures,5800); router.Advance(6400,allowTriggers:false); router.Up("device",1,6500,allowTriggers:false); router.Advance(6900);
        Check(hits.Count==0,"injected interval suppresses hold and tap without replay after injection");
        var layerOnly = Make(KeyGesture.Press,6,held:"Media"); layerOnly.Steps.Clear();
        Check(ConfigurationValidator.ValidateTile(layerOnly).Count==0,"layer-only physical tile needs no filler macro");
        layerOnly.CorsairBinding=null; Check(ConfigurationValidator.ValidateTile(layerOnly).Count>0,"unbound empty tile still rejected");
        var resetting = new KeyGestureRouter(); var staleHits = 0;
        resetting.Triggered += _ => { staleHits++; resetting.Reset(); };
        resetting.Down("device",1,[hold],8000); resetting.Down("device",2,[Make(KeyGesture.Hold,2)],8000); resetting.Advance(8500);
        Check(staleHits==1,"reset during hold callback prevents another stale gesture from same tick");
        var generic=Make(KeyGesture.Press); generic.CorsairBinding=null; generic.KeyboardBinding=new("hid-keyboard",0x7C);
        hits.Clear(); router.Down("HID-KEYBOARD",0x7C,[generic],9000); router.Up("hid-keyboard",0x7C,9050); Check(hits.SequenceEqual(new[]{generic}),"generic physical-key routing is case insensitive");
        var restored=System.Text.Json.JsonSerializer.Deserialize<Tile>(System.Text.Json.JsonSerializer.Serialize(shift))!; Check(restored.HeldLayer=="Media","gesture layer persisted");
        return Task.CompletedTask;
    }
    private static void Check(bool pass,string message) { if(!pass) throw new InvalidOperationException(message); }
}

using Aivana.Core;
internal static class PhysicalKeyboardChecks
{
    public static Task RunAsync()
    {
        var main = new KeyboardBinding("keyboard",0x0D) { ScanCode=0x1C };
        var pad = main with { Extended=true };
        if (main.InputKey == pad.InputKey) throw new Exception("Enter distinction lost");
        var router=new KeyGestureRouter(); var count=0; router.Triggered+=_=>count++;
        var tile=new Tile { KeyboardBinding=main,Steps=[new() { Kind=ActionKind.Text,Value="" }] };
        router.Down("other",main.InputKey,[tile],0); router.Down("keyboard",pad.InputKey,[tile],0);
        if(count!=0) throw new Exception("Wrong physical keyboard/key triggered");
        router.Down("keyboard",main.InputKey,[tile],0); if(count!=1) throw new Exception("Physical mapping failed");
        var profile=new Profile { Tiles=[new() { VendorBinding=new("wooting","device",4),Steps=[new() { Kind=ActionKind.Delay,DelayMs=1 }] },new() { VendorBinding=new("wooting","device",4),Steps=[new() { Kind=ActionKind.Delay,DelayMs=1 }] }] };
        if (!VendorBindingValidator.ValidateProfile(profile).Any()) throw new Exception("Duplicate vendor gestures accepted");
        return Task.CompletedTask;
    }
}

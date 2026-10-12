using Aivana.Core;
internal static class GenericKeyboardChecks
{
    public static Task RunAsync()
    {
        static void Check(bool ok,string why) { if (!ok) throw new InvalidOperationException(why); }
        Check(KeyboardBinding.KeyName(0x7C)=="F13" && KeyboardBinding.KeyName(0x87)=="F24","special F key names");
        Check(!KeyboardBinding.IsSupported(0x41) && !KeyboardBinding.IsSupported(0x70),"ordinary typing and F1 are not bindable raw keys");
        var tile = new Tile { KeyboardBinding = new("keyboard-one",0x7C), Steps = [new() { Kind=ActionKind.Delay, DelayMs=1 }] };
        var json = System.Text.Json.JsonSerializer.Serialize(tile);
        Check(System.Text.Json.JsonSerializer.Deserialize<Tile>(json)!.KeyboardBinding == tile.KeyboardBinding,"device path persists");
        var router = new KeyGestureRouter(); var count=0; router.Triggered += _ => count++;
        router.Down("keyboard-two",0x7C,[tile],0); Check(count==0,"other device cannot trigger same virtual key");
        router.Down("KEYBOARD-ONE",0x7C,[tile],1); router.Down("keyboard-one",0x7C,[tile],2);
        Check(count==1,"case insensitive device match and repeat suppression");
        router.Up("keyboard-one",0x7C,3); router.Down("keyboard-one",0x7C,[tile],4); Check(count==2,"release rearms binding");
        var profile = new Profile { Tiles = [tile,new Tile { KeyboardBinding=tile.KeyboardBinding }] };
        Check(KeyboardBindingValidator.ValidateProfile(profile).Any(),"duplicate gesture fails validation");
        profile.Tiles[1].KeyboardBinding = new("keyboard-two",0x7C);
        Check(!KeyboardBindingValidator.ValidateProfile(profile).Any(),"different physical devices coexist");
        tile.KeyboardBinding = new("keyboard-one",0x41);
        Check(KeyboardBindingValidator.ValidateProfile(profile).Any(),"ordinary key cannot be imported as special binding");
        return Task.CompletedTask;
    }
}

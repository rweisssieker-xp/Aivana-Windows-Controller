using System.Text.Json;
using Aivana.Core;

internal static class CorsairBindingChecks
{
    public static Task RunAsync()
    {
        var tile = new Tile { CorsairBinding = new CorsairBinding("device-a", 6) };
        Assert(CorsairBindingValidator.ValidateTile(tile).Count == 0, "G6 binding should be valid even on a disabled tile.");
        Assert(CorsairBindingValidator.ValidateTile(new Tile { CorsairBinding = new CorsairBinding("", 1) }).Count == 1,
            "A binding without a device id should fail validation.");
        Assert(CorsairBindingValidator.ValidateTile(new Tile { CorsairBinding = new CorsairBinding("device-a", 7) }).Count == 1,
            "Macro key IDs outside G1-G6 should fail validation.");

        var profile = new Profile { Tiles = [tile, new Tile { CorsairBinding = new CorsairBinding("DEVICE-A", 6) }] };
        Assert(CorsairBindingValidator.ValidateProfileBindings(profile).Count == 1,
            "A physical key should not be assigned twice in one profile, regardless of device-id casing.");
        profile.Tiles[1].CorsairBinding = new CorsairBinding("device-b", 6);
        Assert(CorsairBindingValidator.ValidateProfileBindings(profile).Count == 0,
            "Different devices may use the same macro key id.");

        tile.Enabled = false;
        var json = JsonSerializer.Serialize(tile);
        var reloaded = JsonSerializer.Deserialize<Tile>(json)!;
        Assert(!reloaded.Enabled && reloaded.CorsairBinding == tile.CorsairBinding,
            "Disabled tiles and per-device Corsair bindings should survive serialization.");
        Assert(new Tile().CorsairBinding is null, "Existing tiles without the new optional binding should remain unbound.");
        return Task.CompletedTask;
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}

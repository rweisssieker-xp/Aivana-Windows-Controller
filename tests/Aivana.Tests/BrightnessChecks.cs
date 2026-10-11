using Aivana.Core;
internal static class BrightnessChecks
{
    public static Task RunAsync()
    {
        Check(BrightnessCommand.TryParse("Up", out var up) && up.Apply(0, 95, 100) == 100, "brightness increase must clamp at maximum");
        Check(BrightnessCommand.TryParse("Down", out var down) && down.Apply(0, 5, 100) == 0, "brightness decrease must clamp at minimum");
        Check(up.Apply(20, 100, 220) == 120 && down.Apply(20, 100, 220) == 80, "ten percentage points must use the hardware range");
        Check(BrightnessCommand.TryParse("Set:50", out var exact) && exact.Apply(20, 200, 220) == 120, "fixed percentage must map to hardware range");
        foreach (var invalid in new[] { "Set:-1", "Set:101", "Set:1.5", "Set:", "whatever" })
        {
            Check(!BrightnessCommand.TryParse(invalid, out _), "invalid brightness command must fail");
            Check(ConfigurationValidator.ValidateTile(new Tile { Steps = [new() { Kind = ActionKind.Brightness, Value = invalid }] }).Count > 0, "invalid brightness must not save");
        }
        var step = new ActionStep { Kind = ActionKind.Brightness, Value = "Set:70" };
        var json = System.Text.Json.JsonSerializer.Serialize(step);
        var restored = System.Text.Json.JsonSerializer.Deserialize<ActionStep>(json)!;
        Check(restored.Kind == ActionKind.Brightness && restored.Value == "Set:70", "brightness must persist");
        Check(MacroPreview.Build(new Tile { Steps = [step] }).Any(line => line.Contains("Set:70")), "preview must describe the configured brightness");
        return Task.CompletedTask;
    }
    private static void Check(bool value, string message) { if (!value) throw new InvalidOperationException(message); }
}

using Aivana.Core;
using System.Text.Json;
using System.Text.Json.Nodes;

internal static class KeyboardLayoutChecks
{
    public static Task RunAsync()
    {
        CheckCatalogAndReferenceProvenance();
        CheckLanguageLabelsKeepPhysicalIdentity();
        CheckScanCodeGeometry();
        CheckK100ControlsMatchDocumentedPlacement();
        CheckSettingsRejectUnknownLayouts();
        return Task.CompletedTask;
    }

    private static void CheckCatalogAndReferenceProvenance()
    {
        Assert(KeyboardLayoutCatalog.All.Count == 6, "catalog should provide four generic layouts and two K100 variants");
        var isoFull = KeyboardLayoutCatalog.Get("generic-iso-de-fullsize");
        var isoTkl = KeyboardLayoutCatalog.Get("generic-iso-de-tkl");
        var ansiFull = KeyboardLayoutCatalog.Get("generic-ansi-us-fullsize");
        var ansiTkl = KeyboardLayoutCatalog.Get("generic-ansi-us-tkl");
        Assert(isoFull.Keys.Count == 105 && ansiFull.Keys.Count == 104, "fullsize catalogs should have ISO 105-key and ANSI 104-key sets");
        Assert(isoTkl.Keys.Count == 88 && ansiTkl.Keys.Count == 87, "TKL catalogs should omit only the numpad");
        Assert(KeyboardLayoutCatalog.All.Take(4).All(layout => layout.Provenance == KeyboardLayoutProvenance.GenericReference),
            "standard layouts must be identified as generic references");
        Assert(KeyboardLayoutCatalog.Get("corsair-k100-iso-de").Provenance == KeyboardLayoutProvenance.ManufacturerInformed,
            "K100 should not claim measured model verification");
        Assert(KeyboardLayoutCatalog.Get("corsair-k100-iso-de").ProvenanceDescription.Contains("nicht in Millimetern vermessen"),
            "K100 geometry should disclose its schematic dimensions");

        foreach (var layout in KeyboardLayoutCatalog.All)
        {
            Assert(layout.Keys.Select(key => key.PhysicalKeyId).Distinct(StringComparer.Ordinal).Count() == layout.Keys.Count,
                $"{layout.Id} should not reuse physical IDs");
            Assert(layout.Keys.All(key => key.Width > 0 && key.Height > 0), $"{layout.Id} contains invalid key dimensions");
            Assert(layout.Keys.Where(key => key.PhysicalKeyId.StartsWith("HID:", StringComparison.Ordinal))
                .All(key => key.ScanCode > 0 || key.HotkeyOutput == "Pause"),
                $"{layout.Id} should have a scan code or explicit special-key fallback for standard keys");
        }
    }

    private static void CheckLanguageLabelsKeepPhysicalIdentity()
    {
        var ansi = KeyboardLayoutCatalog.Get("generic-ansi-us-tkl");
        var iso = KeyboardLayoutCatalog.Get("generic-iso-de-tkl");
        var ansiYPosition = Find(ansi, "HID:0x1C");
        var germanZPosition = Find(iso, "HID:0x1C");
        Assert(ansiYPosition.PhysicalKeyId == germanZPosition.PhysicalKeyId, "the key identity should not be derived from its printed character");
        Assert(ansiYPosition.TopLegend == "Y" && ansiYPosition.HotkeyOutput == "Y", "ANSI Y position should show its output separately");
        Assert(germanZPosition.TopLegend == "Z" && germanZPosition.HotkeyOutput == "Z", "German QWERTZ should relabel the same physical position as Z");
        Assert(Find(ansi, "HID:0x1D").TopLegend == "Z" && Find(iso, "HID:0x1D").TopLegend == "Y",
            "the lower-left letter key should follow ANSI-Z versus ISO-DE-Y legends");

        var enter = iso.Keys.Single(key => key.Shape == KeyboardKeyShape.IsoEnterL);
        Assert(enter.PhysicalKeyId == "HID:0x28" && enter.Width == 1.5 && enter.Height == 2,
            "ISO Enter should be represented as one tall, shaped physical key");
        var hash = Find(iso, "HID:0x32");
        Assert(hash.TopLegend == "#" && hash.Y == 3 && hash.X == 12.75,
            "ISO-DE hash key should follow Ä on the home row");
    }

    private static void CheckScanCodeGeometry()
    {
        var ansi = KeyboardLayoutCatalog.Get("generic-ansi-us-fullsize");
        var k100 = KeyboardLayoutCatalog.Get("corsair-k100-ansi-us");
        Assert(Find(ansi, "HID:0x29").X == 0 && Find(ansi, "HID:0x3A").X == 2
            && Find(ansi, "HID:0x45").X + Find(ansi, "HID:0x45").Width == 15,
            "standard function row should align with Escape and the 15u main block");
        Assert(Find(k100, "HID:0x29").X == 1.25 && Find(k100, "HID:0x3A").X == 3.25
            && Find(k100, "HID:0x45").X + Find(k100, "HID:0x45").Width == 16.25,
            "K100 function row should retain the same geometry after the G-key column");
        Assert(Find(ansi, "HID:0x44").ScanCode == 0x57, "F11 should use Set-1 scan code 0x57");
        Assert(Find(ansi, "HID:0x45").ScanCode == 0x58, "F12 should use Set-1 scan code 0x58");
        Assert(Find(ansi, "HID:0x5F").ScanCode == 0x47, "numpad 7 should map to 0x47");
        Assert(Find(ansi, "HID:0x5C").ScanCode == 0x4B, "numpad 4 should map to 0x4B");
        Assert(Find(ansi, "HID:0x59").ScanCode == 0x4F, "numpad 1 should map to 0x4F");
        Assert(Find(ansi, "HID:0x49").ScanCode == 0x52 && Find(ansi, "HID:0x49").Extended,
            "Insert should use the E0-prefixed Set-1 code");
        Assert(Find(ansi, "HID:0x4C").ScanCode == 0x53 && Find(ansi, "HID:0x4C").Extended,
            "Delete should use the E0-prefixed Set-1 code");
        Assert(Find(ansi, "HID:0x58").ScanCode == 0x1C && Find(ansi, "HID:0x58").Extended,
            "numpad Enter should be distinct from main Enter");
    }

    private static void CheckK100ControlsMatchDocumentedPlacement()
    {
        foreach (var id in new[] { "corsair-k100-iso-de", "corsair-k100-ansi-us" })
        {
            var layout = KeyboardLayoutCatalog.Get(id);
            var escape = Find(layout, "HID:0x29");
            var wheel = Find(layout, "CORSAIR:K100:ICUE_WHEEL");
            var profile = Find(layout, "CORSAIR:K100:PROFILE");
            var winLock = Find(layout, "CORSAIR:K100:WIN_LOCK");
            Assert(wheel.Y < escape.Y && Math.Abs(wheel.X - escape.X) < 0.001, "iCUE wheel should sit directly above Escape");
            Assert(profile.X < wheel.X && winLock.X > wheel.X, "profile and Win-lock should flank the wheel");
            Assert(layout.Keys.Count(key => key.PhysicalKeyId.StartsWith("CORSAIR:K100:G", StringComparison.Ordinal)) == 6,
                "K100 should show six dedicated G keys");
            for (var index = 1; index <= 6; index++)
            {
                var macro = Find(layout, $"CORSAIR:K100:G{index}");
                Assert(macro.Y == index - 1 && macro.X == 0 && !macro.AvailableForHotkey && macro.Tooltip.Length > 0,
                    $"G{index} should align with a keyboard row and explain its current mapping limit");
            }
            Assert(!layout.Keys.Any(key => key.PhysicalKeyId == "CORSAIR:K100:ICUE_WHEEL_BUTTON"),
                "the wheel press is part of the iCUE wheel control, not a separate distant key");

            var numpad = Find(layout, "HID:0x53");
            var functionLayer = Find(layout, "CORSAIR:K100:FN");
            Assert(!layout.Keys.Any(key => key.PhysicalKeyId == "HID:0xE7")
                && functionLayer.Y == 5 && !functionLayer.AvailableForHotkey,
                "K100 has a firmware Fn key in place of the right Windows key");
            var stop = Find(layout, "CORSAIR:K100:MEDIA_STOP");
            var previous = Find(layout, "CORSAIR:K100:MEDIA_PREVIOUS");
            var playPause = Find(layout, "CORSAIR:K100:MEDIA_PLAYPAUSE");
            var next = Find(layout, "CORSAIR:K100:MEDIA_NEXT");
            Assert(stop.Y < numpad.Y && stop.X < previous.X && previous.X < playPause.X && playPause.X < next.X,
                "media controls should appear in order above the numpad");
            Assert(stop.HotkeyOutput == "MediaStop" && previous.HotkeyOutput == "MediaPreviousTrack"
                && playPause.HotkeyOutput == "MediaPlayPause" && next.HotkeyOutput == "MediaNextTrack"
                && stop.AvailableForHotkey && next.AvailableForHotkey,
                "standard media virtual-key outputs should stay available for mapping");
            Assert(Find(layout, "CORSAIR:K100:VOLUME_MUTE").Y < stop.Y
                && Find(layout, "CORSAIR:K100:VOLUME_ROLLER").Y < stop.Y,
                "mute and volume roller should be on the upper control row");
        }
    }

    private static void CheckSettingsRejectUnknownLayouts()
    {
        var settings = ConfigStore.CreateDefaults();
        Assert(settings.KeyboardLayoutId == "generic-iso-de-fullsize", "new settings should select the generic ISO-DE fullsize layout");
        settings.KeyboardLayoutId = "detected-exact-model";
        Assert(ConfigurationValidator.Validate(settings).Any(error => error.Contains("Tastaturlayout")),
            "unknown keyboard layout selections should fail validation");

        settings.KeyboardLayoutId = "corsair-k100-ansi-us";
        var directory = Path.Combine(Path.GetTempPath(), "aivana-layout-settings-" + Guid.NewGuid().ToString("N"));
        try
        {
            var store = new ConfigStore(directory);
            store.Save(settings);
            Assert(store.Load().KeyboardLayoutId == "corsair-k100-ansi-us", "explicit keyboard layout choice should persist");

            var legacy = ConfigStore.CreateDefaults();
            var legacyNode = JsonNode.Parse(JsonSerializer.Serialize(legacy))!.AsObject();
            legacyNode.Remove("KeyboardLayoutId");
            File.WriteAllText(Path.Combine(directory, "settings.json"), legacyNode.ToJsonString());
            Assert(store.Load().KeyboardLayoutId == "generic-iso-de-fullsize",
                "settings without a layout selection should receive the safe generic default");
        }
        finally
        {
            if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
        }
    }

    private static KeyboardKeyGeometry Find(KeyboardLayoutDefinition layout, string id) =>
        layout.Keys.Single(key => key.PhysicalKeyId == id);

    private static void Assert(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}

using Aivana.Core;

internal static class ActionCatalogChecks
{
    public static Task RunAsync()
    {
        var presets = ActionCatalog.Presets;
        Assert(presets.Count >= 50, "the catalog should offer a broad set of concrete presets");
        Assert(presets.Select(preset => preset.Id).Distinct(StringComparer.Ordinal).Count() == presets.Count,
            "preset identifiers should be unique");
        Assert(presets.All(preset => !string.IsNullOrWhiteSpace(preset.Name) && !string.IsNullOrWhiteSpace(preset.Category) &&
            !string.IsNullOrWhiteSpace(preset.Description) && !string.IsNullOrWhiteSpace(preset.Icon)),
            "every preset should have searchable labels and a visible icon");

        foreach (var preset in presets)
        {
            var first = preset.CreateStep();
            var second = preset.CreateStep();
            Assert(!ReferenceEquals(first, second), $"{preset.Id} should create an independent ActionStep each time");
            Assert(ConfigurationValidator.ValidateTile(new Tile { Name = preset.Name, Icon = preset.Icon, Steps = [first] }).Count == 0,
                $"{preset.Id} should produce a step accepted by the configuration validator");
            if (first.Kind == ActionKind.Delay)
            {
                first.DelayMs++;
                Assert(second.DelayMs != first.DelayMs, $"{preset.Id} should not share mutable delay state");
            }
            else
            {
                first.Value = "changed by caller";
                Assert(second.Value != first.Value, $"{preset.Id} should not share mutable step values");
            }
        }

        Assert(ActionCatalog.Find("app.notepad")?.Name == Localization.Translate("Editor öffnen"),
            "stable identifiers should find presets and expose localized labels");
        Assert(ActionCatalog.Search("YouTube").Any(preset => preset.Id == "web.youtube"),
            "search should include translated preset names");
        Assert(ActionCatalog.Search("Audio und Medien").Count >= 7,
            "search should include translated category labels");
        Assert(ActionCatalog.Search("einen Bildschirmausschnitt").Any(preset => preset.Id == "edit.screenshot"),
            "search should include translated descriptions");
        Assert(ActionCatalog.Search("audio.volume-up").Count == 1,
            "stable identifiers should also be searchable");
        Assert(ActionCatalog.Search("OBS Aufnahme").Any(preset => preset.Id == "obs.record-start"),
            "multiword search should find concrete functions across searchable labels");
        Assert(ActionCatalog.Search(null).Count == presets.Count && ActionCatalog.Search(" ").Count == presets.Count,
            "empty searches should return the full catalog");
        Assert(ActionCatalog.Find("macro.clipboard-copy")?.CreateStep() is { Kind: ActionKind.Text, Value: "{{clipboard}}" },
            "paste clipboard preset should type expanded clipboard text rather than overwrite the clipboard");
        Assert(!ActionCommands.IsExtendedVolume("Set:101") && !ActionCommands.IsExtendedVolume("Set:-1") && ActionCommands.IsExtendedVolume("Set:0"),
            "exact audio volume should enforce percentage bounds");
        Assert(!ActionCommands.IsObsEndpoint("ws://remote.example:4455") && !ActionCommands.IsObsEndpoint("ws://user:pass@localhost:4455") && ActionCommands.IsObsEndpoint("ws://localhost:4455"),
            "OBS endpoints should validate locally before saving");
        Assert(ConfigurationValidator.ValidateTile(new Tile { Steps = [new ActionStep { Kind = ActionKind.Obs, Value = "SetCurrentProgramScene" }] }).Count > 0,
            "OBS scene action needs a scene name");
        var priorLanguage = Localization.CurrentLanguage;
        try
        {
            Localization.SetLanguage("de");
            var sourceKeys = presets.SelectMany(preset => new[] { preset.Category, preset.Name, preset.Description }).Distinct().ToArray();
            foreach (var language in Localization.SupportedLanguages)
            {
                using var stream = typeof(Localization).Assembly.GetManifestResourceStream($"Aivana.Core.Localization.{language}.json")!;
                using var document = System.Text.Json.JsonDocument.Parse(stream);
                Assert(sourceKeys.All(key => document.RootElement.TryGetProperty(key, out _)), $"all actual preset labels need translations in {language}");
            }
            Localization.SetLanguage("en");
            Assert(ActionCatalog.Find("app.notepad")!.Name != "Editor öffnen", "catalog names must be translated in English");
            Assert(ActionCatalog.Search("record").Any(preset => preset.Id == "obs.record-start"), "catalog search must use translated descriptions");
        }
        finally { Localization.SetLanguage(priorLanguage); }

        return Task.CompletedTask;
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}

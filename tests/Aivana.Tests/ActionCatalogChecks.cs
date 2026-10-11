using Aivana.Core;

internal static class ActionCatalogChecks
{
    public static Task RunAsync()
    {
        Assert(ActionCatalog.Find("display.brighter") != null && ActionCatalog.Find("display.dimmer") != null, "brightness presets should be available");
        var wait = new Tile { Steps = [new ActionStep { Kind = ActionKind.WaitWindow, Value = "notepad.exe", DelayMs = 5000 }] };
        Assert(ConfigurationValidator.ValidateTile(wait).Count == 0, "window wait should accept process and bounded timeout");
        wait.Steps[0].DelayMs = 0;
        Assert(ConfigurationValidator.ValidateTile(wait).Count > 0, "window wait needs a positive timeout");
        Assert(ActionCatalog.Find("workflow.workspace") != null, "workspace workflow should be available");
        var presets = ActionCatalog.Presets;
        Assert(presets.Count >= 50, "the catalog should offer a broad set of concrete presets");
        Assert(presets.Select(preset => preset.Id).Distinct(StringComparer.Ordinal).Count() == presets.Count,
            "preset identifiers should be unique");
        Assert(presets.All(preset => !string.IsNullOrWhiteSpace(preset.Name) && !string.IsNullOrWhiteSpace(preset.Category) &&
            !string.IsNullOrWhiteSpace(preset.Description) && !string.IsNullOrWhiteSpace(preset.Icon)),
            "every preset should have searchable labels and a visible icon");

        foreach (var preset in presets)
        {
            var first = preset.CreateSteps();
            var second = preset.CreateSteps();
            Assert(first.Count > 0 && first.Count == second.Count, "preset should retain every step");
            Assert(!preset.IsWorkflow || first.Count > 1, "workflows must contain multiple steps");
            for (var i = 0; i < first.Count; i++)
            {
                Assert(!ReferenceEquals(first[i], second[i]), "preset steps must be independent");
                var originalValue = second[i].Value;
                first[i].Value = "changed by caller";
                Assert(second[i].Value == originalValue, "editing a step must not mutate another draft");
            }
            foreach (var step in second)
            {
                if (step.Kind == ActionKind.SwitchProfile) step.Value = "Standard";
                if (step.Kind == ActionKind.Obs && string.IsNullOrWhiteSpace(step.Arguments)) step.Arguments = "Test";
            }
            Assert(ConfigurationValidator.ValidateTile(new Tile { Name = preset.Name, Steps = second }).Count == 0,
                "configured preset must validate: " + preset.Id);
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

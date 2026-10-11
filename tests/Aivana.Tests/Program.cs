using Aivana.Core;
using System.Text.Json;
using System.Text.Json.Nodes;

// Diagnostic wording checks deliberately use German; localization checks exercise all supported cultures separately.
Localization.SetLanguage("de");

var checks = new (string Name, Func<Task> Run)[]
{
    ("window waits match, cancel, time out and stop subsequent macro steps", WindowWaitChecks.RunAsync),
    ("display brightness clamps, scales, validates and persists", BrightnessChecks.RunAsync),
    ("defaults are useful and do not auto-run", DefaultsAreUseful),
    ("action catalog presets are searchable and create independent steps", ActionCatalogChecks.RunAsync),
    ("localization supports language settings and culture-aware formatting", LocalizationChecks.RunAsync),
    ("validation catches bad IDs, repeats and hotkey conflicts", ValidationCatchesErrors),
    ("Corsair G-key bindings validate and persist", CorsairBindingChecks.RunAsync),
    ("German modifier aliases validate and detect conflicts", GermanHotkeyAliases),
    ("media aliases used by the editor remain compatible", MediaAliases),
    ("validation rejects duplicate IDs and null JSON objects", InvalidConfigIsRejected),
    ("profile selection honors order and fallback", ProfileSelection),
    ("profile selection combines process and title conditions", ProfileSelectionUsesTitle),
    ("templates expand supported variables and preserve unknown tokens", TemplatesExpandVariables),
    ("macro preview describes execution without running actions", MacroPreviewIsDescriptive),
    ("new actions and palette hotkey validate and persist", NewActionsAndPalettePersist),
    ("keyboard catalog preserves physical IDs and accurate reference geometry", KeyboardLayoutChecks.RunAsync),
    ("configuration saves atomically and imports safely", ConfigRoundTrip),
    ("macro steps run in order and repeat", MacroOrderAndRepeat),
    ("macro progress identifies each step and repetition", MacroProgress),
    ("execution feedback reports safe typed phases and actual outcomes", ExecutionFeedbackChecks.RunAsync),
    ("one macro can be cancelled without cancelling queued work", CancelOneMacro),
    ("macro cancellation stops a delay", MacroCancellation),
    ("same tile cannot launch twice", DuplicateMacroIsIgnored),
    ("different macros execute serially", MacrosAreSerialized),
    ("running macro uses an input snapshot", MacroUsesSnapshot),
    ("step errors stop the macro and identify the step", MacroErrorStops),
};

var failed = 0;
foreach (var check in checks)
{
    try
    {
        await check.Run();
        Console.WriteLine($"PASS {check.Name}");
    }
    catch (Exception exception)
    {
        failed++;
        Console.Error.WriteLine($"FAIL {check.Name}: {exception.Message}");
    }
}

return failed == 0 ? 0 : 1;

static Task DefaultsAreUseful()
{
    var settings = ConfigStore.CreateDefaults();
    Assert(settings.Profiles.Count > 0, "defaults need a profile");
    Assert(settings.Profiles[0].Tiles.Count >= 3, "defaults need useful tiles");
    Assert(settings.Profiles.SelectMany(profile => profile.Tiles).All(tile => tile.Steps.Count > 0), "default tiles need actions");
    return Task.CompletedTask;
}

static Task ValidationCatchesErrors()
{
    var settings = ConfigStore.CreateDefaults();
    var profile = settings.Profiles[0];
    profile.Tiles[0].Hotkey = "Ctrl+Alt+K";
    profile.Tiles[1].Hotkey = "Alt+Control+K";
    profile.Tiles[2].Repeat = 101;
    var errors = ConfigurationValidator.Validate(settings);
    Assert(errors.Count >= 2, "expected duplicate hotkey and repeat errors");
    Assert(ConfigurationValidator.ValidateTile(new Tile { Id = Guid.Empty }).Count > 0, "empty tile id should fail");
    profile.Tiles[1].Enabled = false;
    Assert(!ConfigurationValidator.Validate(settings).Any(error => error.Contains("mehrfach belegt")), "disabled duplicate shortcut should not reserve a key");
    return Task.CompletedTask;
}

static async Task InvalidConfigIsRejected()
{
    var settings = ConfigStore.CreateDefaults();
    settings.Profiles.Add(settings.Profiles[0]);
    Assert(ConfigurationValidator.Validate(settings).Any(error => error.Contains("Profil-ID")), "duplicate profile id should fail");

    var directory = Path.Combine(Path.GetTempPath(), "aivana-invalid-" + Guid.NewGuid().ToString("N"));
    Directory.CreateDirectory(directory);
    try
    {
        var path = Path.Combine(directory, "nulls.json");
        File.WriteAllText(path, """{"version":1,"profiles":null}""");
        var rejected = false;
        try { _ = new ConfigStore(directory).ReadImport(path); }
        catch (InvalidDataException) { rejected = true; }
        Assert(rejected, "null profile collections should be rejected");

        File.WriteAllText(path, """{"version":1,"profiles":[null]}""");
        rejected = false;
        try { _ = new ConfigStore(directory).ReadImport(path); }
        catch (InvalidDataException) { rejected = true; }
        Assert(rejected, "null profile objects should be rejected");

        File.WriteAllText(path, """{"version":1,"profiles":[{"id":"00000000-0000-0000-0000-000000000001","name":"x","processName":null,"tiles":null}],"activeProfileId":"00000000-0000-0000-0000-000000000001","defaultProfileId":"00000000-0000-0000-0000-000000000001"}""");
        rejected = false;
        try { _ = new ConfigStore(directory).ReadImport(path); }
        catch (InvalidDataException) { rejected = true; }
        Assert(rejected, "null tile collections should be rejected");
    }
    finally
    {
        Directory.Delete(directory, recursive: true);
    }
    await Task.CompletedTask;
}

static Task GermanHotkeyAliases()
{
    var settings = ConfigStore.CreateDefaults();
    settings.Profiles[0].Tiles[0].Hotkey = "Strg+Umschalt+A";
    Assert(ConfigurationValidator.Validate(settings).Count == 0, "German modifier aliases were rejected");
    settings.Profiles[0].Tiles[1].Hotkey = "Shift+Control+A";
    Assert(ConfigurationValidator.Validate(settings).Any(e => e.Contains("mehrfach belegt")), "equivalent German modifier chord wasn't recognized as conflict");
    return Task.CompletedTask;
}

static Task MediaAliases()
{
    var settings = ConfigStore.CreateDefaults();
    settings.Profiles[0].Tiles[0].Steps[0].Value = "play/pause";
    settings.Profiles[0].Tiles[2].Steps[0].Value = "volumeup";
    Assert(ConfigurationValidator.Validate(settings).Count == 0, "valid media aliases were rejected");
    settings.Profiles[0].Tiles[2].Steps[0].Value = "unexpected";
    Assert(ConfigurationValidator.Validate(settings).Count > 0, "invalid volume action was accepted");
    return Task.CompletedTask;
}

static Task ProfileSelection()
{
    var settings = ConfigStore.CreateDefaults();
    var first = new Profile { Name = "Editor", ProcessName = "code.exe" };
    var second = new Profile { Name = "Code alternate", ProcessName = "Code" };
    settings.Profiles.Insert(0, first);
    settings.Profiles.Insert(1, second);
    settings.DefaultProfileId = settings.Profiles[^1].Id;
    Assert(ProfileSelector.Select(settings, "Code").Id == first.Id, "first matching profile should win");
    Assert(ProfileSelector.Select(settings, "unrelated.exe").Id == settings.DefaultProfileId, "default profile should be fallback");
    return Task.CompletedTask;
}

static Task ProfileSelectionUsesTitle()
{
    var settings = ConfigStore.CreateDefaults();
    var app = new Profile { Name = "Editor window", ProcessName = "code.exe", WindowTitleContains = "README" };
    var titleOnly = new Profile { Name = "Title only", WindowTitleContains = "Preview" };
    settings.Profiles.Insert(0, app);
    settings.Profiles.Insert(1, titleOnly);
    Assert(ProfileSelector.Select(settings, "Code", "Project - README.md - Visual Studio Code").Id == app.Id,
        "process and title requirements should match together");
    Assert(ProfileSelector.Select(settings, "code", "Other - Visual Studio Code").Id != app.Id,
        "a profile with a title condition should not match a different title");
    Assert(ProfileSelector.Select(settings, "anything", "Image Preview").Id == titleOnly.Id,
        "a title-only profile should match");
    Assert(ProfileSelector.Select(settings, "unrelated", "plain window").Id == settings.DefaultProfileId,
        "default profile should remain the fallback");
    return Task.CompletedTask;
}

static Task TemplatesExpandVariables()
{
    var expanded = TemplateExpander.Expand("{{date}} {{time}} {{datetime}} {{clipboard}} {{profile}} {{process}} {{user}} {{unknown}}",
        new DateTime(2026, 10, 8, 14, 5, 6), "copied", "Work", "editor.exe");
    Assert(expanded.Contains("2026-10-08"), "date token should use ISO date format");
    Assert(expanded.Contains("14:05"), "time token should use 24-hour time");
    Assert(expanded.Contains("copied Work editor.exe " + Environment.UserName), "context tokens should be expanded");
    Assert(expanded.EndsWith("{{unknown}}", StringComparison.Ordinal), "unknown tokens should be preserved");
    Assert(TemplateExpander.Tokens.Contains("{{clipboard}}"), "supported token names should be discoverable");
    return Task.CompletedTask;
}

static Task MacroPreviewIsDescriptive()
{
    var tile = new Tile
    {
        Repeat = 3,
        Steps =
        [
            new() { Kind = ActionKind.Text, Value = "Hallo {{user}}" },
            new() { Kind = ActionKind.Delay, DelayMs = 1500 },
            new() { Kind = ActionKind.Clipboard, Value = "{{clipboard}}" },
        ],
    };
    var preview = MacroPreview.Build(tile);
    Assert(preview.Any(line => line.Contains("3") && (line.Contains("Durchlauf") || line.Contains("Durchläuf"))), "preview should show repeat count");
    Assert(preview.Any(line => line.Contains("1,5") || line.Contains("1500")), "preview should explain delays");
    Assert(preview.Any(line => line.Contains("{{user}}")) && preview.Any(line => line.Contains("{{clipboard}}")),
        "preview should show variable placeholders");
    Assert(tile.Repeat == 3 && tile.Steps.Count == 3 && tile.Steps[0].Value == "Hallo {{user}}",
        "preview must not alter the macro it describes");
    return Task.CompletedTask;
}

static async Task NewActionsAndPalettePersist()
{
    var settings = ConfigStore.CreateDefaults();
    Assert(settings.PaletteHotkey == "Ctrl+Shift+Space", "palette should have the requested default shortcut");
    Assert((int)ActionKind.CorsairColor == 9 && (int)ActionKind.Clipboard == 10,
        "new action kinds should be appended without changing existing numeric values");
    var tile = settings.Profiles[0].Tiles[0];
    tile.Favorite = true;
    settings.Profiles[0].WindowTitleContains = "Dashboard";
    tile.Steps =
    [
        new() { Kind = ActionKind.Clipboard, Value = "" },
        new() { Kind = ActionKind.Window, Value = "left" },
        new() { Kind = ActionKind.System, Value = "lock" },
    ];
    var validErrors = ConfigurationValidator.Validate(settings);
    Assert(validErrors.Count == 0, "valid new actions should pass validation: " + string.Join(" | ", validErrors));
    tile.Steps[1].Value = "close";
    Assert(ConfigurationValidator.Validate(settings).Count > 0, "unknown window actions should fail validation");
    tile.Steps[1].Value = "left";
    tile.Steps[2].Value = "shutdown";
    Assert(ConfigurationValidator.Validate(settings).Count > 0, "system actions beyond lock should fail validation");
    tile.Steps[2].Value = "lock";

    var directory = Path.Combine(Path.GetTempPath(), "aivana-new-settings-" + Guid.NewGuid().ToString("N"));
    try
    {
        var store = new ConfigStore(directory);
        store.Save(settings);
        var loaded = store.Load();
        Assert(loaded.PaletteHotkey == settings.PaletteHotkey && loaded.Profiles[0].Tiles[0].Steps[1].Kind == ActionKind.Window
            && loaded.Profiles[0].Tiles[0].Favorite && loaded.Profiles[0].WindowTitleContains == "Dashboard",
            "new settings and enum values should survive a JSON round trip");
        settings.PaletteHotkey = "bad shortcut";
        Assert(ConfigurationValidator.Validate(settings).Any(error => error.Contains("Palette")), "invalid palette shortcut should fail validation");
        settings.PaletteHotkey = "Ctrl+Shift+Space";
        settings.CancelHotkey = settings.PaletteHotkey;
        Assert(ConfigurationValidator.Validate(settings).Any(error => error.Contains("Palette")), "palette and cancel shortcuts should conflict");
        settings.CancelHotkey = "Ctrl+Alt+Escape";
        tile.Hotkey = settings.PaletteHotkey;
        Assert(ConfigurationValidator.Validate(settings).Any(error => error.Contains("Palette")), "palette and enabled tile shortcuts should conflict");
        tile.Enabled = false;
        Assert(!ConfigurationValidator.Validate(settings).Any(error => error.Contains("Palette") && error.Contains("Kachel")),
            "disabled tiles should not conflict with the palette shortcut");

        var legacyDirectory = Path.Combine(Path.GetTempPath(), "aivana-legacy-settings-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(legacyDirectory);
        try
        {
            var legacy = ConfigStore.CreateDefaults();
            legacy.Profiles[0].Tiles[0].Hotkey = "Ctrl+Shift+Space";
            var legacyNode = JsonNode.Parse(JsonSerializer.Serialize(legacy))!.AsObject();
            legacyNode.Remove("PaletteHotkey");
            File.WriteAllText(Path.Combine(legacyDirectory, "settings.json"), legacyNode.ToJsonString());
            var legacyLoaded = new ConfigStore(legacyDirectory).Load();
            Assert(legacyLoaded.Profiles[0].Tiles[0].Hotkey == "Ctrl+Shift+Space" && string.IsNullOrEmpty(legacyLoaded.PaletteHotkey),
                "older settings without palette hotkey should remain loadable");
        }
        finally
        {
            if (Directory.Exists(legacyDirectory)) Directory.Delete(legacyDirectory, recursive: true);
        }
    }
    finally
    {
        if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
    }
    await Task.CompletedTask;
}

static async Task ConfigRoundTrip()
{
    var directory = Path.Combine(Path.GetTempPath(), "aivana-tests-" + Guid.NewGuid().ToString("N"));
    try
    {
        var store = new ConfigStore(directory);
        var settings = ConfigStore.CreateDefaults();
        settings.Profiles[0].Tiles[0].Steps = [new ActionStep { Kind = ActionKind.Volume, Value = "Set:50", AudioDeviceId = "saved-specific-endpoint" }];
        store.Save(settings);
        Assert(store.Load().Profiles.Count == settings.Profiles.Count, "saved configuration did not load");
        settings.AutoStart = true;
        store.Save(settings);
        Assert(File.Exists(Path.Combine(directory, "settings.json.bak")), "save should retain a known-good backup");
        var export = Path.Combine(directory, "export.json");
        store.Export(settings, export);
        var imported = store.ReadImport(export);
        Assert(imported.Profiles[0].Tiles[0].Steps[0].AudioDeviceId == "saved-specific-endpoint", "import and export should preserve selected audio devices");
        Assert(imported.Profiles.SelectMany(profile => profile.Tiles).All(tile => !tile.Enabled), "imported actions must await explicit activation");
        Assert(!imported.AutoStart && !imported.AutoProfiles, "imported automation settings should be inactive");

        File.WriteAllText(Path.Combine(directory, "broken.json"), "{bad json");
        var rejected = false;
        try { store.ReadImport(Path.Combine(directory, "broken.json")); }
        catch (InvalidDataException) { rejected = true; }
        Assert(rejected, "malformed import should be rejected");
        Assert(File.Exists(Path.Combine(directory, "settings.json")), "import should not disturb saved settings");

        File.WriteAllText(Path.Combine(directory, "settings.json"), "broken current settings");
        rejected = false;
        try { _ = store.Load(); }
        catch (InvalidDataException) { rejected = true; }
        Assert(rejected, "damaged saved configuration should be reported");
        var recovered = store.RecoverBackup();
        Assert(!recovered.AutoStart, "recovery should restore the last saved good configuration");
        Assert(Directory.GetFiles(directory, "settings.json.corrupt-*.json").Length == 1, "recovery should preserve the damaged file");
    }
    finally
    {
        if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
    }
}

static async Task MacroOrderAndRepeat()
{
    var executor = new RecordingExecutor();
    var runner = new MacroRunner(executor);
    var tile = new Tile { Repeat = 2, Steps = [new() { Kind = ActionKind.Text, Value = "a" }, new() { Kind = ActionKind.Text, Value = "b" }] };
    await runner.RunAsync(tile);
    Assert(string.Join("", executor.Values) == "abab", "macro order or repeat count was wrong");
    Assert(!runner.IsRunning(tile.Id), "completed macro should no longer be running");
}

static async Task MacroCancellation()
{
    var executor = new RecordingExecutor();
    var runner = new MacroRunner(executor);
    var tile = new Tile { Steps = [new() { Kind = ActionKind.Delay, DelayMs = 10_000 }] };
    var run = runner.RunAsync(tile);
    await WaitUntil(() => runner.IsRunning(tile.Id));
    runner.CancelAll();
    await run.WaitAsync(TimeSpan.FromSeconds(2));
    Assert(!runner.IsRunning(tile.Id), "cancelled macro should no longer be running");
}

static async Task MacroProgress()
{
    var statuses = new List<string>();
    var runner = new MacroRunner(new RecordingExecutor());
    runner.StatusChanged += (_, status) => statuses.Add(status);
    await runner.RunAsync(new Tile { Repeat = 2, Steps = [new() { Kind = ActionKind.Text, Value = "a" }, new() { Kind = ActionKind.Text, Value = "b" }] });
    Assert(statuses.Any(s => s.Contains("Schritt 1/2") && s.Contains("Durchlauf 1/2")), "first step progress missing");
    Assert(statuses.Any(s => s.Contains("Schritt 2/2") && s.Contains("Durchlauf 2/2")), "last repeat progress missing");
}

static async Task CancelOneMacro()
{
    var executor = new RecordingExecutor();
    var runner = new MacroRunner(executor);
    var waiting = new Tile { Steps = [new() { Kind = ActionKind.Delay, DelayMs = 10000 }] };
    var later = new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "later" }] };
    var first = runner.RunAsync(waiting);
    var second = runner.RunAsync(later);
    var cancel = typeof(MacroRunner).GetMethod("Cancel", [typeof(Guid)]);
    try
    {
        Assert(cancel != null, "individual cancellation is not implemented");
        cancel!.Invoke(runner, [waiting.Id]);
        await Task.WhenAll(first, second).WaitAsync(TimeSpan.FromSeconds(2));
        Assert(executor.Values.SequenceEqual(["later"]), "queued work was cancelled with the first macro");
    }
    finally { runner.CancelAll(); await Task.WhenAll(first, second); }
}

static async Task DuplicateMacroIsIgnored()
{
    var executor = new BlockingExecutor();
    var runner = new MacroRunner(executor);
    var tile = new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "once" }] };
    var first = runner.RunAsync(tile);
    await WaitUntil(() => executor.Started);
    await runner.RunAsync(tile);
    Assert(executor.Calls == 1, "duplicate trigger should not execute again");
    executor.Release();
    await first;
}

static async Task MacrosAreSerialized()
{
    var executor = new ConcurrencyCheckingExecutor();
    var runner = new MacroRunner(executor);
    var first = new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "first" }] };
    var second = new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "second" }] };
    await Task.WhenAll(runner.RunAsync(first), runner.RunAsync(second));
    Assert(executor.MaximumConcurrentCalls == 1, "different macros must not overlap executor calls");
}

static async Task MacroErrorStops()
{
    var executor = new RecordingExecutor { FailOn = "bad" };
    var runner = new MacroRunner(executor);
    string? status = null;
    runner.StatusChanged += (_, value) => status = value;
    var tile = new Tile { Steps = [new() { Kind = ActionKind.Text, Value = "bad" }, new() { Kind = ActionKind.Text, Value = "later" }] };
    await runner.RunAsync(tile);
    Assert(executor.Values.SequenceEqual(["bad"]), "macro continued after a failed step");
    Assert(status?.Contains("Schritt 1") == true && status.Contains("expected failure"), "error status should identify the step and reason");
}

static async Task MacroUsesSnapshot()
{
    var executor = new FirstCallBlockingExecutor();
    var runner = new MacroRunner(executor);
    var tile = new Tile { Repeat = 2, Steps = [new() { Kind = ActionKind.Volume, Value = "Set:50", AudioDeviceId = "original-device" }] };
    var run = runner.RunAsync(tile);
    await WaitUntil(() => executor.Started);
    tile.Repeat = 3;
    tile.Steps[0].Value = "edited";
    tile.Steps[0].AudioDeviceId = "edited-device";
    tile.Steps.Add(new ActionStep { Kind = ActionKind.Text, Value = "added" });
    executor.Release();
    await run;
    Assert(executor.Values.SequenceEqual(["Set:50", "Set:50"]), "editing should not change a queued or running macro");
    Assert(executor.DeviceIds.SequenceEqual(["original-device", "original-device"]), "editing the device must not retarget a queued or running macro");
}

static async Task WaitUntil(Func<bool> predicate)
{
    var until = DateTime.UtcNow + TimeSpan.FromSeconds(2);
    while (!predicate() && DateTime.UtcNow < until) await Task.Delay(5);
    Assert(predicate(), "condition did not become true");
}

static void Assert(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

sealed class RecordingExecutor : IActionExecutor
{
    public List<string> Values { get; } = [];
    public string? FailOn { get; init; }
    public Task ExecuteAsync(ActionStep step, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Values.Add(step.Value);
        if (step.Value == FailOn) throw new InvalidOperationException("expected failure");
        return Task.CompletedTask;
    }
}

sealed class BlockingExecutor : IActionExecutor
{
    private readonly TaskCompletionSource _release = new(TaskCreationOptions.RunContinuationsAsynchronously);
    public bool Started { get; private set; }
    public int Calls { get; private set; }
    public async Task ExecuteAsync(ActionStep step, CancellationToken cancellationToken)
    {
        Calls++;
        Started = true;
        await _release.Task.WaitAsync(cancellationToken);
    }
    public void Release() => _release.TrySetResult();
}

sealed class ConcurrencyCheckingExecutor : IActionExecutor
{
    private int _active;
    private int _maximum;
    public int MaximumConcurrentCalls => _maximum;
    public async Task ExecuteAsync(ActionStep step, CancellationToken cancellationToken)
    {
        var active = Interlocked.Increment(ref _active);
        int observed;
        do
        {
            observed = _maximum;
            if (active <= observed) break;
        } while (Interlocked.CompareExchange(ref _maximum, active, observed) != observed);
        try { await Task.Delay(20, cancellationToken); }
        finally { Interlocked.Decrement(ref _active); }
    }
}

sealed class FirstCallBlockingExecutor : IActionExecutor
{
    private readonly TaskCompletionSource _release = new(TaskCreationOptions.RunContinuationsAsynchronously);
    public List<string> Values { get; } = [];
    public List<string?> DeviceIds { get; } = [];
    public bool Started { get; private set; }
    public async Task ExecuteAsync(ActionStep step, CancellationToken cancellationToken)
    {
        Values.Add(step.Value);
        DeviceIds.Add(step.AudioDeviceId);
        if (!Started)
        {
            Started = true;
            await _release.Task.WaitAsync(cancellationToken);
        }
    }
    public void Release() => _release.TrySetResult();
}

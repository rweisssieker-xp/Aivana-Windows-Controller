namespace Aivana.Core;

/// <summary>A localized, reusable description of one ready-to-use action step.</summary>
public sealed class ActionPreset
{
    private readonly Func<ActionStep> _createStep;
    private readonly Func<List<ActionStep>>? _createWorkflow;
    public bool IsWorkflow => _createWorkflow is not null;
    private readonly string _categorySource;
    private readonly string _nameSource;
    private readonly string _descriptionSource;

    internal ActionPreset(string id, string category, string name, string description, string icon, Func<ActionStep> createStep)
    {
        Id = id;
        _categorySource = category;
        _nameSource = name;
        _descriptionSource = description;
        Icon = icon;
        _createStep = createStep;
    }

    /// <summary>Stable, language-independent identifier for storage and filtering.</summary>
    public string Id { get; }
    /// <summary>Stable source-language category key for grouping across UI locales.</summary>
    public string CategoryId => _categorySource;
    public string Category => Localization.Translate(_categorySource);
    public string Name => Localization.Translate(_nameSource);
    public string Description => Localization.Translate(_descriptionSource);
    public string Icon { get; }

    /// <summary>Creates a new step each time so edits to one tile cannot mutate the catalog or another tile.</summary>
    public ActionStep CreateStep() => IsWorkflow
        ? throw new InvalidOperationException("Use CreateSteps for a multi-step workflow.") : _createStep();
    public List<ActionStep> CreateSteps() => _createWorkflow?.Invoke() ?? [_createStep()];

    internal ActionPreset(string id, string name, string description, string icon, Func<List<ActionStep>> createWorkflow)
        : this(id, "Ablaufvorlagen", name, description, icon, () => throw new InvalidOperationException())
        => _createWorkflow = createWorkflow;

    internal string CategorySource => _categorySource;
    internal string NameSource => _nameSource;
    internal string DescriptionSource => _descriptionSource;
}

/// <summary>Concrete action presets supported by the Windows action executor.</summary>
public static class ActionCatalog
{
    private static readonly IReadOnlyList<ActionPreset> PresetList = Array.AsReadOnly(new[]
    {
        // Apps and folders
        App("custom.program", "Apps und Ordner", "Programm starten", "Programmdatei auswählen; Argumente und Arbeitsordner sind eigene Felder.", "▣", "notepad.exe"),
        Folder("custom.path", "Apps und Ordner", "Datei / Ordner öffnen", "Windows öffnet den Pfad mit der zugeordneten Anwendung.", "▤", Environment.SpecialFolder.UserProfile),
        App("app.notepad", "Apps und Ordner", "Editor öffnen", "Startet den integrierten Windows-Editor.", "▤", "notepad.exe"),
        App("app.calculator", "Apps und Ordner", "Taschenrechner öffnen", "Startet den Windows-Taschenrechner.", "⊞", "calc.exe"),
        App("app.paint", "Apps und Ordner", "Paint öffnen", "Startet Microsoft Paint, sofern installiert.", "▧", "mspaint.exe"),
        App("app.explorer", "Apps und Ordner", "Datei-Explorer öffnen", "Öffnet den Windows-Datei-Explorer.", "▣", "explorer.exe"),
        Folder("folder.home", "Apps und Ordner", "Benutzerordner öffnen", "Öffnet den persönlichen Windows-Benutzerordner.", "⌂", Environment.SpecialFolder.UserProfile),
        Folder("folder.documents", "Apps und Ordner", "Dokumente öffnen", "Öffnet den persönlichen Dokumenteordner.", "▤", Environment.SpecialFolder.MyDocuments),
        Folder("folder.downloads", "Apps und Ordner", "Downloads öffnen", "Öffnet den persönlichen Downloadordner.", "⇩", "Downloads"),
        Folder("folder.desktop", "Apps und Ordner", "Desktop öffnen", "Öffnet den persönlichen Desktopordner.", "▱", Environment.SpecialFolder.DesktopDirectory),
        Folder("folder.pictures", "Apps und Ordner", "Bilder öffnen", "Öffnet den persönlichen Bilderordner.", "▧", Environment.SpecialFolder.MyPictures),
        Folder("folder.music", "Apps und Ordner", "Musik öffnen", "Öffnet den persönlichen Musikordner.", "♫", Environment.SpecialFolder.MyMusic),

        // Writing and editing shortcuts
        Text("custom.text", "Schreiben und Bearbeiten", "Text eingeben", "Unicode-Text wird eingegeben. Variablen werden beim Ausführen ersetzt.", "T", ""),
        Key("custom.shortcut", "Schreiben und Bearbeiten", "Tastenkombination", "Beispiel: Ctrl+Shift+S oder Alt+F4.", "⌘", "Ctrl+Shift+S"),
        Clipboard("custom.clipboard", "Schreiben und Bearbeiten", "Zwischenablage setzen", "Text wird mit ersetzten Variablen in die Zwischenablage kopiert.", "▤", ""),
        Key("edit.save", "Schreiben und Bearbeiten", "Speichern", "Sendet Strg+S an das aktive Fenster.", "▣", "Ctrl+S"),
        Key("edit.save-as", "Schreiben und Bearbeiten", "Speichern unter", "Sendet Strg+Umschalt+S an das aktive Fenster.", "▣", "Ctrl+Shift+S"),
        Key("edit.open", "Schreiben und Bearbeiten", "Datei öffnen", "Sendet Strg+O an das aktive Fenster.", "↗", "Ctrl+O"),
        Key("edit.new", "Schreiben und Bearbeiten", "Neues Dokument", "Sendet Strg+N an das aktive Fenster.", "＋", "Ctrl+N"),
        Key("edit.undo", "Schreiben und Bearbeiten", "Rückgängig", "Macht die letzte Änderung im aktiven Fenster rückgängig.", "↶", "Ctrl+Z"),
        Key("edit.redo", "Schreiben und Bearbeiten", "Wiederholen", "Wiederholt die letzte rückgängig gemachte Änderung.", "↷", "Ctrl+Y"),
        Key("edit.cut", "Schreiben und Bearbeiten", "Ausschneiden", "Sendet Strg+X an das aktive Fenster.", "✂", "Ctrl+X"),
        Key("edit.copy", "Schreiben und Bearbeiten", "Kopieren", "Sendet Strg+C an das aktive Fenster.", "▢", "Ctrl+C"),
        Key("edit.paste", "Schreiben und Bearbeiten", "Einfügen", "Sendet Strg+V an das aktive Fenster.", "▤", "Ctrl+V"),
        Key("edit.select-all", "Schreiben und Bearbeiten", "Alles auswählen", "Sendet Strg+A an das aktive Fenster.", "▧", "Ctrl+A"),
        Key("edit.find", "Schreiben und Bearbeiten", "Suchen", "Öffnet die Suche im aktiven Fenster.", "⌕", "Ctrl+F"),
        Key("edit.print", "Schreiben und Bearbeiten", "Drucken", "Sendet Strg+P an das aktive Fenster.", "▤", "Ctrl+P"),
        Key("edit.screenshot", "Schreiben und Bearbeiten", "Bildschirmausschnitt", "Öffnet die Windows-Auswahl für einen Bildschirmausschnitt.", "▧", "Win+Shift+S"),

        // Windows and keyboard
        Key("window.task-view", "Fenster und Tastatur", "Fensterübersicht", "Zeigt die Windows-Fensterübersicht.", "▦", "Win+Tab"),
        Key("window.desktop", "Fenster und Tastatur", "Desktop anzeigen", "Minimiert alle Fenster mit dem Windows-Kürzel.", "▱", "Win+D"),
        Key("window.switch", "Fenster und Tastatur", "Fenster wechseln", "Wechselt zum nächsten Fenster.", "⇥", "Alt+Tab"),
        Key("window.task-manager", "Fenster und Tastatur", "Task-Manager öffnen", "Öffnet den Windows-Task-Manager.", "▦", "Ctrl+Shift+Escape"),
        WinWindow("window.minimize", "Fenster und Tastatur", "Aktives Fenster minimieren", "Minimiert das aktuell fokussierte Fenster.", "▁", "minimize"),
        WinWindow("window.maximize", "Fenster und Tastatur", "Aktives Fenster maximieren", "Maximiert das aktuell fokussierte Fenster.", "□", "maximize"),
        WinWindow("window.restore", "Fenster und Tastatur", "Fenster wiederherstellen", "Stellt Größe und Position des aktiven Fensters wieder her.", "▢", "restore"),
        WinWindow("window.left", "Fenster und Tastatur", "Fenster links anordnen", "Ordnet das aktive Fenster links auf dem Monitor an.", "◧", "left"),
        WinWindow("window.right", "Fenster und Tastatur", "Fenster rechts anordnen", "Ordnet das aktive Fenster rechts auf dem Monitor an.", "◨", "right"),
        System("window.lock", "Fenster und Tastatur", "Windows sperren", "Sperrt die aktuelle Windows-Sitzung.", "🔒"),

        Value("display.brighter", "Fenster und Tastatur", "Bildschirm heller", "Erhöht die Helligkeit unterstützter Displays um 10 Prozentpunkte.", "☀", ActionKind.Brightness, "Up"),
        Value("display.dimmer", "Fenster und Tastatur", "Bildschirm dunkler", "Verringert die Helligkeit unterstützter Displays um 10 Prozentpunkte.", "◐", ActionKind.Brightness, "Down"),
        Value("display.brightness-half", "Fenster und Tastatur", "Bildschirmhelligkeit auf 50 Prozent", "Setzt die Helligkeit unterstützter Displays auf 50 Prozent. Der Wert ist bearbeitbar.", "☀", ActionKind.Brightness, "Set:50"),

        // Audio and media
        Value("audio.volume-up", "Audio und Medien", "Lautstärke erhöhen", "Erhöht die Windows-Systemlautstärke.", "🔊", ActionKind.Volume, "Up"),
        Value("audio.volume-down", "Audio und Medien", "Lautstärke verringern", "Verringert die Windows-Systemlautstärke.", "🔉", ActionKind.Volume, "Down"),
        Value("audio.mute", "Audio und Medien", "Stummschaltung umschalten", "Schaltet die Windows-Systemlautstärke stumm oder wieder ein.", "🔇", ActionKind.Volume, "Mute"),
        Value("audio.volume-half", "Audio und Medien", "Lautstärke auf 50 Prozent", "Setzt die Windows-Systemlautstärke auf 50 Prozent.", "🔉", ActionKind.Volume, "Set:50"),
        Value("audio.unmute", "Audio und Medien", "Stummschaltung aufheben", "Schaltet die Windows-Systemlautstärke ein.", "🔊", ActionKind.Volume, "MuteOff"),
        Value("audio.mute-on", "Audio und Medien", "Stummschaltung einschalten", "Schaltet die Windows-Systemlautstärke stumm.", "🔇", ActionKind.Volume, "MuteOn"),
        Value("audio.microphone-mute", "Audio und Medien", "Mikrofon stummschalten", "Schaltet das Standardmikrofon stumm.", "🎙", ActionKind.Volume, "MicMuteOn"),
        Value("audio.microphone-unmute", "Audio und Medien", "Mikrofon aktivieren", "Hebt die Stummschaltung des Standardmikrofons auf.", "🎙", ActionKind.Volume, "MicMuteOff"),
        Value("audio.microphone-toggle", "Audio und Medien", "Mikrofon umschalten", "Schaltet die Stummschaltung des Standardmikrofons um.", "🎙", ActionKind.Volume, "MicToggleMute"),
        Value("media.play-pause", "Audio und Medien", "Wiedergabe / Pause", "Schaltet die Medienwiedergabe um.", "▶", ActionKind.Media, "PlayPause"),
        Value("media.next", "Audio und Medien", "Nächster Titel", "Springt zum nächsten Medientitel.", "⏭", ActionKind.Media, "Next"),
        Value("media.previous", "Audio und Medien", "Vorheriger Titel", "Springt zum vorherigen Medientitel.", "⏮", ActionKind.Media, "Previous"),
        Value("media.stop", "Audio und Medien", "Wiedergabe stoppen", "Sendet den Medienbefehl Stopp.", "■", ActionKind.Media, "Stop"),

        // Web
        Web("custom.url", "Web", "Webseite öffnen", "HTTP- und HTTPS-Adressen werden geöffnet.", "↗", "https://example.com"),
        Web("web.search", "Web", "Websuche öffnen", "Öffnet die Google-Websuche.", "⌕", "https://www.google.com/"),
        Web("web.wikipedia", "Web", "Wikipedia öffnen", "Öffnet die deutschsprachige Wikipedia.", "W", "https://de.wikipedia.org/"),
        Web("web.youtube", "Web", "YouTube öffnen", "Öffnet YouTube im Standardbrowser.", "▷", "https://www.youtube.com/"),
        Web("web.github", "Web", "GitHub öffnen", "Öffnet GitHub im Standardbrowser.", "⌘", "https://github.com/"),
        Web("web.office", "Web", "Microsoft 365 öffnen", "Öffnet Microsoft 365 im Standardbrowser.", "▦", "https://www.microsoft365.com/"),
        Web("web.translate", "Web", "Übersetzer öffnen", "Öffnet Google Übersetzer im Standardbrowser.", "文", "https://translate.google.com/"),

        // Macros and device actions
        Text("macro.date-time", "Makros und Gerät", "Datum und Uhrzeit eingeben", "Tippt Datum und Uhrzeit an der aktuellen Cursorposition.", "◷", "{{datetime}}"),
        Text("macro.user", "Makros und Gerät", "Windows-Benutzername eingeben", "Tippt den Namen des aktuellen Windows-Benutzers.", "♙", "{{user}}"),
        Text("macro.process", "Makros und Gerät", "Aktiven Prozessnamen eingeben", "Tippt den Namen des aktuell fokussierten Prozesses.", "▣", "{{process}}"),
        Text("macro.clipboard-copy", "Makros und Gerät", "Zwischenablage einfügen", "Tippt den aktuellen Inhalt der Zwischenablage ein.", "▤", "{{clipboard}}"),
        Wait("custom.wait", "Makros und Gerät", "Pause", "Wartezeit zwischen Schritten (0–600.000 ms).", "◷", 100),
        Value("custom.profile", "Makros und Gerät", "Profil wechseln", "Profil nach Namen auswählen.", "▦", ActionKind.SwitchProfile, "Standard"),
        Clipboard("macro.clipboard-date", "Makros und Gerät", "Datum in Zwischenablage", "Kopiert das heutige Datum in die Zwischenablage.", "▣", "{{date}}"),
        Clipboard("macro.clipboard-user", "Makros und Gerät", "Benutzername in Zwischenablage", "Kopiert den Windows-Benutzernamen in die Zwischenablage.", "▣", "{{user}}"),
        Wait("macro.wait-short", "Makros und Gerät", "Kurz warten", "Wartet 250 Millisekunden zwischen Makroschritten.", "◷", 250),
        Wait("macro.wait-second", "Makros und Gerät", "Eine Sekunde warten", "Wartet eine Sekunde zwischen Makroschritten.", "◷", 1000),
        Color("device.corsair-teal", "Makros und Gerät", "Corsair-Beleuchtung Türkis", "Setzt die Corsair-Beleuchtung auf Türkis, wenn ein kompatibles iCUE-Gerät verfügbar ist.", "●", "#25C4B6"),
        Color("device.corsair-white", "Makros und Gerät", "Corsair-Beleuchtung Weiß", "Setzt die Corsair-Beleuchtung auf Weiß, wenn ein kompatibles iCUE-Gerät verfügbar ist.", "●", "#FFFFFF"),

        // OBS and streaming controls. Scene and input targets can be edited after adding the preset.
        Obs("obs.record-start", "Streaming und OBS", "OBS-Aufnahme starten", "Startet die Aufnahme in einer laufenden OBS-WebSocket-Sitzung.", "●", "StartRecord"),
        Obs("obs.record-stop", "Streaming und OBS", "OBS-Aufnahme stoppen", "Stoppt die laufende OBS-Aufnahme.", "■", "StopRecord"),
        Obs("obs.record-toggle", "Streaming und OBS", "OBS-Aufnahme umschalten", "Startet oder stoppt die OBS-Aufnahme.", "◉", "ToggleRecord"),
        Obs("obs.record-pause", "Streaming und OBS", "OBS-Aufnahme pausieren", "Pausiert eine laufende OBS-Aufnahme.", "Ⅱ", "PauseRecord"),
        Obs("obs.record-resume", "Streaming und OBS", "OBS-Aufnahme fortsetzen", "Setzt eine pausierte OBS-Aufnahme fort.", "▶", "ResumeRecord"),
        Obs("obs.stream-start", "Streaming und OBS", "OBS-Stream starten", "Startet den konfigurierten OBS-Stream.", "↗", "StartStream"),
        Obs("obs.stream-stop", "Streaming und OBS", "OBS-Stream stoppen", "Stoppt den laufenden OBS-Stream.", "■", "StopStream"),
        Obs("obs.stream-toggle", "Streaming und OBS", "OBS-Stream umschalten", "Startet oder stoppt den OBS-Stream.", "◉", "ToggleStream"),
        Obs("obs.scene", "Streaming und OBS", "OBS-Szene wechseln", "Wechselt zur angegebenen OBS-Szene; den Szenennamen nach dem Hinzufügen anpassen.", "▣", "SetCurrentProgramScene", "Szene"),
        Obs("obs.microphone-toggle", "Streaming und OBS", "OBS-Mikrofon umschalten", "Schaltet den angegebenen OBS-Eingang stumm oder aktiv; den Eingabenamen anpassen.", "🎙", "ToggleInputMute", "Mikrofon"),

        new ActionPreset("workflow.workspace", "Arbeitsplatz starten", "Öffnet Editor, Dokumente und Projektseite. Pfade und Webseite vor dem Speichern anpassen.", "▦", () => [
            new() { Kind = ActionKind.Launch, Value = "notepad.exe" },
            new() { Kind = ActionKind.WaitWindow, Value = "notepad", DelayMs = 15000 },
            new() { Kind = ActionKind.OpenPath, Value = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) },
            new() { Kind = ActionKind.Url, Value = "https://example.com" }]),
        new ActionPreset("workflow.meeting", "Meeting vorbereiten", "Stoppt Medien, schaltet das Mikrofon stumm und öffnet den anpassbaren Meeting-Link.", "🎙", () => [
            new() { Kind = ActionKind.Media, Value = "Stop" }, new() { Kind = ActionKind.Volume, Value = "MicMuteOn" },
            new() { Kind = ActionKind.Url, Value = "https://example.com" }]),
        new ActionPreset("workflow.streaming", "Streaming vorbereiten", "Startet OBS und wartet auf sein Fenster. Programmpfad und Szenenname vor dem Speichern anpassen.", "●", () => [
            new() { Kind = ActionKind.Launch, Value = "obs64.exe" },
            new() { Kind = ActionKind.WaitWindow, Value = "obs64", DelayMs = 30000 },
            new() { Kind = ActionKind.Obs, Value = "SetCurrentProgramScene", Arguments = "", WorkingDirectory = "ws://127.0.0.1:4455" }]),
        new ActionPreset("workflow.research", "Recherche starten", "Öffnet Wikipedia und einen Editor; beginnt die Notiz mit Datum und Uhrzeit.", "⌕", () => [
            new() { Kind = ActionKind.Url, Value = "https://www.wikipedia.org" },
            new() { Kind = ActionKind.Launch, Value = "notepad.exe" },
            new() { Kind = ActionKind.WaitWindow, Value = "notepad", DelayMs = 15000 },
            new() { Kind = ActionKind.Text, Value = "{{datetime}}\n" }]),
        new ActionPreset("workflow.project", "Projekt wechseln", "Öffnet den Projektordner und wechselt zum gewählten Controller-Profil. Ordner und Profil anpassen.", "▤", () => [
            new() { Kind = ActionKind.OpenPath, Value = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) },
            new() { Kind = ActionKind.SwitchProfile, Value = "" }]),
        new ActionPreset("workflow.snippet", "Textbaustein einfügen", "Kopiert eine anpassbare Vorlage mit Datum und fügt sie im aktiven Fenster ein.", "T", () => [
            new() { Kind = ActionKind.Clipboard, Value = "{{date}} · {{user}}" },
            new() { Kind = ActionKind.Shortcut, Value = "Ctrl+V" }]),
        new ActionPreset("custom.wait-window", "Makros und Gerät", "Auf Zielfenster warten", "Wartet auf ein sichtbares, reagierendes Zielfenster und fokussiert es. Prozess, Titel und Zeitlimit anpassen.", "◷", () => new ActionStep { Kind = ActionKind.WaitWindow, Value = "notepad", DelayMs = 15000 }),

    });

    public static IReadOnlyList<ActionPreset> Presets => PresetList;

    /// <summary>Searches the translated name, category, and description using the active UI language.</summary>
    public static IReadOnlyList<ActionPreset> Search(string? query)
    {
        if (string.IsNullOrWhiteSpace(query)) return PresetList;
        var terms = query.Split((char[]?)null, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        return Array.AsReadOnly(PresetList.Where(preset => terms.All(term =>
            preset.Name.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
            preset.Category.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
            preset.Description.Contains(term, StringComparison.CurrentCultureIgnoreCase) ||
            preset.Id.Contains(term, StringComparison.OrdinalIgnoreCase))).ToArray());
    }

    public static ActionPreset? Find(string id) => PresetList.FirstOrDefault(preset =>
        preset.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

    private static ActionPreset App(string id, string category, string name, string description, string icon, string command) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.Launch, Value = command });

    private static ActionPreset Folder(string id, string category, string name, string description, string icon, Environment.SpecialFolder folder) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.OpenPath, Value = Environment.GetFolderPath(folder) });

    private static ActionPreset Folder(string id, string category, string name, string description, string icon, string relativeToUser) =>
        new(id, category, name, description, icon, () => new ActionStep
        {
            Kind = ActionKind.OpenPath,
            Value = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), relativeToUser),
        });

    private static ActionPreset Key(string id, string category, string name, string description, string icon, string shortcut) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.Shortcut, Value = shortcut });

    private static ActionPreset WinWindow(string id, string category, string name, string description, string icon, string operation) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.Window, Value = operation });

    private static ActionPreset System(string id, string category, string name, string description, string icon) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.System, Value = "lock" });

    private static ActionPreset Web(string id, string category, string name, string description, string icon, string url) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.Url, Value = url });

    private static ActionPreset Value(string id, string category, string name, string description, string icon, ActionKind kind, string value) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = kind, Value = value });

    private static ActionPreset Text(string id, string category, string name, string description, string icon, string value) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.Text, Value = value });

    private static ActionPreset Clipboard(string id, string category, string name, string description, string icon, string value) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.Clipboard, Value = value });

    private static ActionPreset Wait(string id, string category, string name, string description, string icon, int milliseconds) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.Delay, DelayMs = milliseconds });

    private static ActionPreset Color(string id, string category, string name, string description, string icon, string color) =>
        new(id, category, name, description, icon, () => new ActionStep { Kind = ActionKind.CorsairColor, Value = color });

    private static ActionPreset Obs(string id, string category, string name, string description, string icon, string command, string arguments = "") =>
        new(id, category, name, description, icon, () => new ActionStep
        {
            Kind = ActionKind.Obs,
            Value = command,
            Arguments = arguments,
            WorkingDirectory = "ws://127.0.0.1:4455",
        });
}

using Aivana.Core;
using System.Windows;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private void AddPaletteCommands(List<PaletteEntry> entries)
    {
        var app = new Profile { Name = "MAKRIVO" };
        void Add(string name, string description, Action command, string terms = "") => entries.Add(
            new PaletteEntry(new Tile { Name = name }, app, description, false, "", L("BEREIT"), true,
                Command: command, SearchTerms: terms));
        foreach (var section in new[] { "Dashboard", "Profile", "Geräte", "Einstellungen", "Tastatur" })
            Add(L(section), L(section), () => NavigateToSection(section), section);
#if !MAKRIVO_STORE_NO_AI
        Add(L("KI-Besprechungsassistent"), L("Lokal einen prüfbaren Besprechungsablauf entwerfen"), ShowMeetingAssistant, "ai ki meeting besprechung ollama outcome");
#endif
        Add(L("Arbeitsplatz einrichten"), L("Geführte Einrichtung"), ShowWorkspaceWizard, "setup wizard workspace meeting streaming");
        Add(L("Tastaturhersteller und SDKs"), L("Razer, Logitech, Wooting und weitere Hersteller"), ShowKeyboardAdapters, "razer razor logitech steelseries wooting qmk via keychron");
        Add(L("Mobilpanel"), L("Telefon und Tablet lokal koppeln"), ShowMobilePanel, "mobile phone tablet panel");
        Add(L("Erweiterungen"), L("Plugins importieren und verwalten"), ShowPlugins, "plugin packages integrations");
        Add(L("UI-Ablauf aufnehmen"), L("Klicks auf UI-Elemente aufnehmen"), ShowUiRecorder, "uia recorder automation");
        Add(L("UI-Automation"), L("Elemente auswählen und bedienen"), ShowUiAutomationDraft, "uia automation picker");
        Add(L("Ausführungsprüfung"), L("Ergebnisse und Voraussetzungen"), ShowVerificationHistory, "verification history results");
        Add(L("Ablaufeditor"), L("Bedingungen, Verzweigungen und Fehlerbehandlung"), ShowWorkflowDesigner, "workflow condition branch");
        Add(L("Makro aufnehmen"), L("Tastenkombinationen und Wartezeiten aufnehmen"), ShowMacroRecorder, "record recorder");
        Add(L("Profile synchronisieren"), L("Profile über einen gemeinsamen Ordner austauschen"), ShowProfileSync, "sync backup cloud");
        Add(L("Tastendiagnose"), L("Geräte, Belegungen und Konflikte prüfen"), ShowDeviceDiagnostics, "diagnostics devices keys");
        Add(L("Aktionskatalog"), L("Alle Funktionen"), () => ShowActionCatalog(ActiveProfile));
        Add(L("Sondertasten-Assistent"), _controller.CorsairKeyStatus, ShowSpecialKeyWizard, "corsair G1 G2 G3 G4 G5 G6");
        Add(L("Tastatur-Overlay"), L("Ctrl+Alt+F1 halten · Overlay über die Befehlspalette ein-/ausblenden."), ToggleKeyboardOverlay, "overlay keyboard");
        Add(L("＋ Aktion"), L("Aktion bearbeiten"), () => { SwitchPage("Dashboard"); AddTile(ActiveProfile); });
        Add(L("Mehrschritt-Makro erstellen"), L("Aktionskatalog"), () => { SwitchPage("Dashboard"); AddMacroTemplate(ActiveProfile); });
        Add(L("Makros abbrechen"), L("Abbruch angefordert"), _controller.CancelAll, "cancel stop abort");
        Add(_controller.Paused ? L("Hotkeys aktivieren") : L("Hotkeys pausieren"),
            L("HOTKEYS"), () => _controller.SetPaused(!_controller.Paused), "pause resume");
        Add(_controller.ProfileLocked ? L("🔓 Entsperren") : L("🔒 Profil sperren"),
            L("Profile"), () => _controller.SetProfileLocked(!_controller.ProfileLocked));
        Add(L("⇧ Konfiguration importieren"), L("Profile"), ImportProfiles, "import");
        Add(L("⇩ Konfiguration exportieren"), L("Profile"), () => _controller.Export(), "export backup");
        foreach (var profile in _controller.Settings.Profiles)
            Add(profile.Name, L("Profil öffnen"), () =>
            {
                if (!ResolvePendingDraft()) return;
                _controller.SelectProfile(profile.Id); SwitchPage("Dashboard");
            }, L("Profile"));
        foreach (var preset in ActionCatalog.Presets)
        {
            var entry = new PaletteEntry(new Tile { Name = preset.Name, Icon = preset.Icon, Steps = preset.CreateSteps() },
                new Profile { Name = preset.Category }, preset.Description, false, "", L("Funktion auswählen"), true,
                Preset: preset, SearchTerms: preset.Id + " " + preset.CategoryId);
            entries.Add(entry with { CanRun = entry.NeedsConfiguration || !_controller.Paused,
                State = entry.NeedsConfiguration ? L("Funktion auswählen") : _controller.Paused ? L("PAUSIERT · Hotkeys sind pausiert") : L("BEREIT") });
        }
    }

    private async Task ExecutePaletteEntryAsync(PaletteEntry entry)
    {
        if (entry.Command != null)
        {
            Show(); WindowState = WindowState.Normal; Activate();
            entry.Command(); Refresh(forceRender: true);
        }
        else if (entry.Preset != null && entry.NeedsConfiguration) CreatePalettePreset(entry.Preset);
        else await _controller.ExecuteFromPaletteAsync(entry.Tile);
    }

    private void CreatePalettePreset(ActionPreset preset)
    {
        if (!ResolvePendingDraft() || ActiveProfile == null) return;
        var steps = preset.CreateSteps();
        foreach (var step in steps.Where(step => step.Kind == ActionKind.SwitchProfile)) step.Value = "";
        var tile = new Tile { Name = preset.Name, Icon = preset.Icon, Steps = steps };
        _page = "Dashboard"; _selectedTileId = tile.Id; _draftTile = tile; _draftProfileId = ActiveProfile.Id; _selectedStep = 0;
        _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = ActiveProfile.Id;
        ResetDraftHistory(); Show(); WindowState = WindowState.Normal; Activate(); Refresh(forceRender: true);
    }
}

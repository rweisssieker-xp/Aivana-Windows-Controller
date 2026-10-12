using System.Globalization;
using System.Text.RegularExpressions;

namespace Aivana.Core;

public static class ConfigurationValidator
{
    public static List<string> Validate(AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        var errors = new List<string>();
        if (settings.VendorIntegrations is not { ApprovedLibraries: not null } integrations || !(integrations.AnalogRelease >= 0 && integrations.AnalogRelease < integrations.AnalogPress && integrations.AnalogPress <= 1)) errors.Add("Ungültige Hersteller-Einstellungen.");
        else foreach (var library in integrations.ApprovedLibraries) if (library.Key is not ("logitech-g" or "logitech-led" or "wooting") || !Regex.IsMatch(library.Value ?? "", "^[0-9A-Fa-f]{64}$")) errors.Add("Ungültige SDK-Freigabe.");
        if (settings.Version != 1)
            errors.Add(Localization.Translate((FormattableString)$"Nicht unterstützte Konfigurationsversion: {settings.Version}."));
        if (!KeyboardLayoutCatalog.TryGet(settings.KeyboardLayoutId, out _))
            errors.Add(Localization.Translate((FormattableString)$"Das Tastaturlayout „{settings.KeyboardLayoutId}“ ist unbekannt."));
        if (!Localization.IsSupportedLanguageSetting(settings.UiLanguage))
            errors.Add(Localization.Translate((FormattableString)$"Die Spracheinstellung „{settings.UiLanguage}“ ist unbekannt."));

        if (settings.Profiles is null)
        {
            errors.Add(Localization.Translate("Die Profilliste fehlt."));
            return errors;
        }
        if (settings.Profiles.Count == 0)
        {
            errors.Add(Localization.Translate("Mindestens ein Profil ist erforderlich."));
            return errors;
        }

        var profileIds = new HashSet<Guid>();
        var tileIds = new HashSet<Guid>();
        var paletteKey = NormalizeHotkey(settings.PaletteHotkey);
        foreach (var profile in settings.Profiles)
        {
            if (profile is null)
            {
                errors.Add(Localization.Translate("Die Profilliste enthält einen leeren Eintrag."));
                continue;
            }
            if (profile.Id == Guid.Empty)
                errors.Add(Localization.Translate((FormattableString)$"Profil „{profile.Name ?? "(ohne Namen)"}“ hat keine gültige ID."));
            else if (!profileIds.Add(profile.Id))
                errors.Add(Localization.Translate((FormattableString)$"Die Profil-ID {profile.Id} ist mehrfach vorhanden."));

            if (string.IsNullOrWhiteSpace(profile.Name))
                errors.Add(Localization.Translate((FormattableString)$"Profil {profile.Id} braucht einen Namen."));
            if (profile.Tiles is null)
            {
                errors.Add(Localization.Translate((FormattableString)$"Profil „{profile.Name}“ enthält keine gültige Kachelliste."));
                continue;
            }

            errors.AddRange(CorsairBindingValidator.ValidateProfileBindings(profile));
            errors.AddRange(KeyboardBindingValidator.ValidateProfile(profile));
            errors.AddRange(VendorBindingValidator.ValidateProfile(profile));

            var hotkeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var tile in profile.Tiles)
            {
                if (tile is null)
                {
                    errors.Add(Localization.Translate((FormattableString)$"Profil „{profile.Name}“ enthält eine leere Kachel."));
                    continue;
                }
                if (tile.Id == Guid.Empty)
                    errors.Add(Localization.Translate((FormattableString)$"Kachel „{tile.Name ?? "(ohne Namen)"}“ hat keine gültige ID."));
                else if (!tileIds.Add(tile.Id))
                    errors.Add(Localization.Translate((FormattableString)$"Die Kachel-ID {tile.Id} ist mehrfach vorhanden."));

                foreach (var tileError in ValidateTile(tile))
                    errors.Add(Localization.Translate((FormattableString)$"Kachel „{tile.Name ?? "(ohne Namen)"}“: {tileError}"));
                errors.AddRange(CorsairBindingValidator.ValidateTile(tile));

                var key = NormalizeHotkey(tile.Hotkey);
                if (tile.Enabled && key.Length > 0 && !hotkeys.Add(key))
                    errors.Add(Localization.Translate((FormattableString)$"Das Kürzel „{tile.Hotkey}“ ist im Profil „{profile.Name}“ mehrfach belegt."));
                if (tile.Enabled && key.Length > 0 && key.Equals(NormalizeHotkey(settings.CancelHotkey), StringComparison.OrdinalIgnoreCase))
                    errors.Add(Localization.Translate((FormattableString)$"Das Abbruchkürzel „{settings.CancelHotkey}“ ist auch der Kachel „{tile.Name}“ zugeordnet."));
                if (tile.Enabled && paletteKey.Length > 0 && key.Equals(paletteKey, StringComparison.OrdinalIgnoreCase))
                    errors.Add(Localization.Translate((FormattableString)$"Das Palette-Kürzel „{settings.PaletteHotkey}“ ist auch der Kachel „{tile.Name}“ zugeordnet."));

                foreach (var step in tile.Steps ?? [])
                {
                    if (step?.Kind == ActionKind.SwitchProfile && !string.IsNullOrWhiteSpace(step.Value))
                    {
                        var targetExists = Guid.TryParse(step.Value, out var targetId)
                            ? settings.Profiles.Any(candidate => candidate?.Id == targetId)
                            : settings.Profiles.Any(candidate => candidate?.Name?.Equals(step.Value.Trim(), StringComparison.OrdinalIgnoreCase) == true);
                        if (!targetExists)
                            errors.Add(Localization.Translate((FormattableString)$"Kachel „{tile.Name}“: Das Zielprofil „{step.Value}“ ist nicht vorhanden."));
                    }
                }
            }
        }

        if (!profileIds.Contains(settings.DefaultProfileId))
            errors.Add(Localization.Translate("Das Standardprofil verweist auf keine vorhandene Profil-ID."));
        if (!profileIds.Contains(settings.ActiveProfileId))
            errors.Add(Localization.Translate("Das aktive Profil verweist auf keine vorhandene Profil-ID."));
        if (!string.IsNullOrWhiteSpace(settings.CancelHotkey) && !IsPlausibleHotkey(settings.CancelHotkey))
            errors.Add(Localization.Translate((FormattableString)$"Das Abbruchkürzel „{settings.CancelHotkey}“ ist ungültig."));
        if (!string.IsNullOrWhiteSpace(settings.PaletteHotkey) && !IsPlausibleHotkey(settings.PaletteHotkey))
            errors.Add(Localization.Translate((FormattableString)$"Das Palette-Kürzel „{settings.PaletteHotkey}“ ist ungültig."));
        if (paletteKey.Length > 0 && paletteKey.Equals(NormalizeHotkey(settings.CancelHotkey), StringComparison.OrdinalIgnoreCase))
            errors.Add(Localization.Translate((FormattableString)$"Das Palette-Kürzel „{settings.PaletteHotkey}“ kollidiert mit dem Abbruchkürzel „{settings.CancelHotkey}“."));
        return errors;
    }

    public static List<string> ValidateTile(Tile tile)
    {
        ArgumentNullException.ThrowIfNull(tile);
        var errors = new List<string>();
        if (tile.Id == Guid.Empty)
            errors.Add(Localization.Translate("Die ID darf nicht leer sein."));
        if (string.IsNullOrWhiteSpace(tile.Name))
            errors.Add(Localization.Translate("Ein Name ist erforderlich."));
        if (string.IsNullOrWhiteSpace(tile.Icon))
            errors.Add(Localization.Translate("Ein Symbol ist erforderlich."));
        if (tile.Color is null || !Regex.IsMatch(tile.Color, "^#[0-9A-Fa-f]{6}$", RegexOptions.CultureInvariant))
            errors.Add(Localization.Translate("Die Kachelfarbe muss im Format #RRGGBB angegeben sein."));
        if (tile.Repeat is < 1 or > 100)
            errors.Add(Localization.Translate("Die Wiederholung muss zwischen 1 und 100 liegen."));
        if (tile.VendorBinding is { } vendor && (vendor.Adapter is not ("logitech-g" or "wooting") || string.IsNullOrWhiteSpace(vendor.DeviceId) || vendor.KeyCode is < 1 or > 65535)) errors.Add("Ungültige Hersteller-Tastenbindung.");
        if (tile.Steps is null)
        {
            errors.Add(Localization.Translate("Die Aktionsliste fehlt."));
            return errors;
        }
        if (tile.Steps.Count == 0 && (string.IsNullOrWhiteSpace(tile.HeldLayer) || tile.CorsairBinding is null && tile.KeyboardBinding is null && tile.VendorBinding is null))
            errors.Add(Localization.Translate("Mindestens ein Aktionsschritt ist erforderlich."));
        for (var index = 0; index < tile.Steps.Count; index++)
        {
            var step = tile.Steps[index];
            if (step is null)
            {
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} ist leer."));
                continue;
            }
            errors.AddRange(WorkflowRules.Validate(step, tile.Steps.Count - index - 1));
            if (step.Verification is { TimeoutMs: < 100 or > 60000 }) errors.Add("Verification timeout must be 100–60000 ms.");
            if (!Enum.IsDefined(step.Kind))
            {
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} hat eine unbekannte Aktionsart."));
                continue;
            }
            if (step.Value is null)
            {
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} enthält keinen gültigen Wert."));
                continue;
            }
            if (step.Kind == ActionKind.UiAutomation) { errors.AddRange(UiAutomationRules.Validate(step.UiAutomation)); continue; }
            if (step.Kind == ActionKind.Plugin)
            {
                if (step.Plugin is not { TimeoutMs: >= 100 and <= 60000 } plugin || string.IsNullOrWhiteSpace(plugin.PackageId) || string.IsNullOrWhiteSpace(plugin.ActionId) || plugin.Parameters.ValueKind != System.Text.Json.JsonValueKind.Object) errors.Add("Plugin, Aktion und Parameter prüfen.");
                continue;
            }
            if (step.Kind == ActionKind.VendorLighting)
            {
                if (!Regex.IsMatch(step.Value, "^#[0-9A-Fa-f]{6}$") || step.Arguments is not ("razer" or "steelseries" or "logitech-led")) errors.Add("Hersteller und RGB-Farbe prüfen.");
                continue;
            }
            if (step.Kind == ActionKind.WaitWindow)
            {
                if (string.IsNullOrWhiteSpace(step.Value) && string.IsNullOrWhiteSpace(step.Arguments))
                    errors.Add(Localization.Translate("Für das Zielfenster ist ein Prozessname oder Fenstertitel erforderlich."));
                if (step.DelayMs is < 1 or > 600_000)
                    errors.Add(Localization.Translate("Das Zeitlimit muss zwischen 1 und 600000 ms liegen."));
                continue;
            }
            if (step.Kind == ActionKind.Delay)
            {
                if (step.DelayMs is < 0 or > 600_000)
                    errors.Add(Localization.Translate((FormattableString)$"Die Pause in Schritt {index + 1} muss zwischen 0 und 600000 ms liegen."));
                continue;
            }
            if (step.Kind is ActionKind.Text or ActionKind.Clipboard)
                continue;
            if (string.IsNullOrWhiteSpace(step.Value))
            {
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} braucht einen Wert."));
                continue;
            }
            if (step.Kind == ActionKind.Url &&
                (!Uri.TryCreate(step.Value, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)))
                errors.Add(Localization.Translate((FormattableString)$"Die URL in Schritt {index + 1} muss mit http:// oder https:// beginnen."));
            if (step.Kind == ActionKind.Media && !IsOneOf(step.Value, "PlayPause", "play/pause", "toggle", "wiedergabe/pause", "Next", "nexttrack", "nächster titel", "Previous", "prev", "previoustrack", "vorheriger titel", "Stop", "stopp"))
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} hat eine unbekannte Medienaktion."));
            if (step.Kind == ActionKind.Brightness && !BrightnessCommand.TryParse(step.Value, out _))
                errors.Add(Localization.Translate("Helligkeit: Up, Down oder Set:0 bis Set:100 verwenden."));
            if (step.Kind == ActionKind.Volume && !ActionCommands.IsExtendedVolume(step.Value) && !IsOneOf(step.Value, "Up", "higher", "höher", "lauter", "volumeup", "Down", "lower", "niedriger", "leiser", "volumedown", "Mute", "stumm", "toggle", "volumemute"))
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} hat eine unbekannte Lautstärkeaktion."));
            if (step.Kind == ActionKind.Obs)
            {
                if (!ActionCommands.Obs.Contains(step.Value, StringComparer.Ordinal))
                    errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} hat eine unbekannte OBS-Aktion."));
                if (!ActionCommands.IsObsEndpoint(step.WorkingDirectory ?? ""))
                    errors.Add(Localization.Translate("Die OBS-Adresse muss ws://localhost oder eine gültige wss://Adresse verwenden."));
                if (step.Value is "SetCurrentProgramScene" or "ToggleInputMute" && string.IsNullOrWhiteSpace(step.Arguments))
                    errors.Add(Localization.Translate("Für diese OBS-Aktion ist ein Szenen- oder Eingabename erforderlich."));
            }
            if (step.Kind == ActionKind.CorsairColor && !Regex.IsMatch(step.Value, "^#[0-9A-Fa-f]{6}$", RegexOptions.CultureInvariant))
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} braucht eine Farbe im Format #RRGGBB."));
            if (step.Kind == ActionKind.Window && !IsOneOf(step.Value, "minimize", "maximize", "restore", "left", "right"))
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} hat eine unbekannte Fensteraktion."));
            if (step.Kind == ActionKind.System && !IsOneOf(step.Value, "lock"))
                errors.Add(Localization.Translate((FormattableString)$"Schritt {index + 1} hat eine unbekannte Systemaktion."));
        }

        if (!string.IsNullOrWhiteSpace(tile.Hotkey) && !IsPlausibleHotkey(tile.Hotkey))
            errors.Add(Localization.Translate("Das Kürzel muss mindestens eine Taste und darf nur einmal jeden Modifikator enthalten."));
        return errors;
    }

    private static string NormalizeHotkey(string? value)
    {
        var parts = (value ?? string.Empty).Split('+', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return string.Empty;
        var modifiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        string? key = null;
        foreach (var part in parts)
        {
            var canonical = part.ToLower(CultureInfo.InvariantCulture) switch
            {
                "ctrl" or "control" or "strg" => "Ctrl",
                "alt" => "Alt",
                "shift" or "umschalt" => "Shift",
                "win" or "windows" or "meta" => "Win",
                _ => null,
            };
            if (canonical is null) key = part;
            else modifiers.Add(canonical);
        }
        var ordered = new[] { "Ctrl", "Alt", "Shift", "Win" }.Where(modifiers.Contains).ToList();
        if (key is not null) ordered.Add(key);
        return string.Join('+', ordered);
    }

    private static bool IsOneOf(string value, params string[] choices) =>
        choices.Contains(value, StringComparer.OrdinalIgnoreCase);

    private static bool IsPlausibleHotkey(string value)
    {
        var parts = value.Split('+', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0 || parts.Any(part => part.Any(char.IsWhiteSpace))) return false;
        var modifiers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var keyCount = 0;
        foreach (var part in parts)
        {
            var canonical = part.ToLower(CultureInfo.InvariantCulture) switch
            {
                "strg" or "control" => "ctrl", "umschalt" => "shift", "meta" or "windows" => "win", _ => part.ToLower(CultureInfo.InvariantCulture)
            };
            if (canonical is "ctrl" or "control" or "alt" or "shift" or "win" or "windows")
            {
                if (!modifiers.Add(canonical is "control" ? "ctrl" : canonical is "windows" ? "win" : canonical)) return false;
            }
            else keyCount++;
        }
        return keyCount == 1;
    }
}

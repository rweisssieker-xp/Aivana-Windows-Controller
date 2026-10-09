using System.Text.Json;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    internal void RunLocalizationSmokeChecks(Action<System.Windows.Window, string> snapshot)
    {
        var settings = _controller.Settings;
        var previousLanguage = settings.UiLanguage;
        var previousLayout = settings.KeyboardLayoutId;
        var previousPage = _page;
        var originalProfiles = JsonSerializer.Serialize(settings.Profiles);
        try
        {
            foreach (var language in Localization.SupportedLanguages.Concat(new[] { "en-US", "de-DE", "fr-FR", "it-IT" }))
            {
                settings.UiLanguage = language;
                Localization.SetLanguage(language);
                LocalizeChrome();
                foreach (var page in new[] { "Dashboard", "Einstellungen", "Tastatur" })
                {
                    _page = page;
                    RenderPage(ActiveProfile);
                    UpdateLayout();
                }
                if (JsonSerializer.Serialize(settings.Profiles) != originalProfiles || settings.KeyboardLayoutId != previousLayout)
                    throw new InvalidOperationException($"Language {language} changed user profile data or physical keyboard layout.");
                if (language is "en-US" or "de-DE" or "fr-FR" or "it-IT" or "pl" or "el" or "uk")
                {
                    if (language is "en-US" or "de-DE" or "fr-FR" or "it-IT")
                    {
                        var expected = language switch
                        {
                            "en-US" => ("New action", "New macro", "Copy"),
                            "fr-FR" => ("Nouvelle action", "Nouvelle macro", "Copie"),
                            "it-IT" => ("Nuova azione", "Nuova macro", "Copia"),
                            _ => ("Neue Aktion", "Neues Makro", "Kopie")
                        };
                        DiscardDraft();
                        AddTile(ActiveProfile);
                        if (_draftTile?.Name != expected.Item1) throw new InvalidOperationException("New action name not localized: " + language);
                        DiscardDraft();
                        AddMacroTemplate(ActiveProfile);
                        if (_draftTile?.Name != expected.Item2) throw new InvalidOperationException("New macro name not localized: " + language);
                        DiscardDraft();
                        var source = new Tile { Name = "User supplied name", Steps = [new ActionStep { Kind = ActionKind.Delay }] };
                        DuplicateTile(source);
                        if (_draftTile?.Name != "User supplied name (" + expected.Item3 + ")" || source.Name != "User supplied name")
                            throw new InvalidOperationException("Copy name not localized or source modified: " + language);
                        DiscardDraft();
                        var originalIds = settings.Profiles.Select(profile => profile.Id).ToHashSet();
                        var originalActive = settings.ActiveProfileId;
                        try
                        {
                            var number = settings.Profiles.Count + 1;
                            CreateProfile();
                            var created = ActiveProfile ?? throw new InvalidOperationException("New profile missing");
                            var profileLabel = language switch { "en-US" => "Profile", "it-IT" => "Profilo", _ => "Profil" };
                            if (created.Name != profileLabel + " " + number) throw new InvalidOperationException("New profile name not localized: " + language);
                            DuplicateProfile();
                            if (ActiveProfile?.Name != created.Name + " (" + expected.Item3 + ")" || ActiveProfile.Id == created.Id)
                                throw new InvalidOperationException("Profile copy name not localized: " + language);
                        }
                        finally
                        {
                            settings.Profiles.RemoveAll(profile => !originalIds.Contains(profile.Id));
                            _controller.SelectProfile(originalActive);
                        }
                        if (JsonSerializer.Serialize(settings.Profiles) != originalProfiles)
                            throw new InvalidOperationException("Localized creation modified existing profiles.");
                    }
                    _page = "Einstellungen";
                    RenderPage(ActiveProfile);
                    UpdateLayout();
                    snapshot(this, "Sprache-" + language);
                    var catalog = BuildActionCatalogWindow(_ => { }, null);
                    catalog.Show(); catalog.UpdateLayout();
                    snapshot(catalog, "Aktionskatalog-" + language);
                    catalog.Close();
                }
            }
        }
        finally
        {
            DiscardDraft();
            settings.UiLanguage = previousLanguage;
            Localization.SetLanguage(previousLanguage);
            LocalizeChrome();
            _page = previousPage;
            RenderPage(ActiveProfile);
            UpdateLayout();
        }
    }
}

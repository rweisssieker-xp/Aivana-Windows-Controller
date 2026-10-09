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
            settings.UiLanguage = previousLanguage;
            Localization.SetLanguage(previousLanguage);
            LocalizeChrome();
            _page = previousPage;
            RenderPage(ActiveProfile);
            UpdateLayout();
        }
    }
}

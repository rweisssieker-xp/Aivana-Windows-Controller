using Aivana.Core;
using System.Globalization;

internal static class LocalizationChecks
{
    public static Task RunAsync()
    {
        var previousCulture = CultureInfo.CurrentUICulture;
        var previousLanguage = Localization.CurrentLanguage;
        try
        {
            Assert(Localization.SupportedLanguages.Count == 27, "all 27 supported languages should be exposed");
            Assert(Localization.SupportedLanguages.Contains("sr"), "Serbian should be supported");
            var resourceIssues = Localization.ValidateResources();
            Assert(resourceIssues.Count == 0, "locale resources must be complete and preserve placeholders: " + string.Join("; ", resourceIssues.Take(8)));
            Assert(Localization.LanguageNames["de"] == "Deutsch" && Localization.LanguageNames["sr"].Contains("Srpski"), "language labels should be native");

            Localization.SetLanguage("en");
            Assert(Localization.CurrentLanguage == "en", "explicit language choice should take effect");
            Assert(Localization.Translate("Uncatalogued German source") == "Uncatalogued German source", "missing keys should fall back to source text");
            Assert(Localization.Format("Number {0:N1}", 1234.5) == "Number 1,234.5", "formatting should use the selected UI culture");
            Assert(Localization.Translate((FormattableString)$"Count {1234.5:N1}") == "Count 1,234.5", "FormattableString should preserve the template and active culture");

            var settings = ConfigStore.CreateDefaults();
            Assert(settings.UiLanguage == "auto", "new settings should follow the operating system language");
            Assert(ConfigurationValidator.Validate(settings).Count == 0, "auto should be a valid setting");
            settings.UiLanguage = "sr";
            Assert(ConfigurationValidator.Validate(settings).Count == 0, "supported language should validate");
            settings.UiLanguage = "xx";
            Assert(ConfigurationValidator.Validate(settings).Count > 0, "unknown language should fail validation");

            // Pre-localization settings omit the new property and must retain the auto default.
            var migrated = System.Text.Json.JsonSerializer.Deserialize<AppSettings>("""{"version":1,"profiles":[]}""");
            Assert(migrated?.UiLanguage == "auto", "older settings should migrate to auto");

            var invalidRejected = false;
            try { Localization.SetLanguage("xx"); }
            catch (ArgumentException) { invalidRejected = true; }
            Assert(invalidRejected, "unsupported language selection should be rejected");
            return Task.CompletedTask;
        }
        finally
        {
            Localization.SetLanguage(previousLanguage);
            CultureInfo.CurrentUICulture = previousCulture;
        }
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}

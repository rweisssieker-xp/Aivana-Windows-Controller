using System.Globalization;
using System.Text.Json;

namespace Aivana.Core;

/// <summary>Loads the built-in UI translations and resolves German source strings at runtime.</summary>
public static class Localization
{
    private const string ResourcePrefix = "Aivana.Core.Localization.";
    private static readonly string[] Languages =
    [
        "de", "en", "fr", "es", "it", "pt", "nl", "pl", "cs", "sk", "hu", "ro", "bg", "el",
        "da", "sv", "nb", "fi", "et", "lv", "lt", "sl", "hr", "uk", "ru", "tr", "sr",
    ];

    private static readonly IReadOnlyDictionary<string, string> RegionalCodes = new Dictionary<string, string>
    {
        ["en"] = "en-US", ["de"] = "de-DE", ["fr"] = "fr-FR", ["it"] = "it-IT",
    };

    public static string PreferredLanguageSetting(string language)
    {
        var normalized = NormalizeSetting(language);
        return RegionalCodes.GetValueOrDefault(normalized,
            RegionalCodes.Values.FirstOrDefault(code => code.Equals(normalized, StringComparison.OrdinalIgnoreCase)) ?? normalized);
    }

    public static IReadOnlyList<string> SupportedLanguageSettings { get; } =
        Array.AsReadOnly(Languages.Select(PreferredLanguageSetting).ToArray());

    private static readonly IReadOnlyDictionary<string, string> NativeNames = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        ["de-DE"] = "Deutsch (Deutschland)", ["en-US"] = "English (United States)",
        ["fr-FR"] = "Français (France)", ["it-IT"] = "Italiano (Italia)",
        ["de"] = "Deutsch", ["en"] = "English", ["fr"] = "Français", ["es"] = "Español", ["it"] = "Italiano",
        ["pt"] = "Português", ["nl"] = "Nederlands", ["pl"] = "Polski", ["cs"] = "Čeština", ["sk"] = "Slovenčina",
        ["hu"] = "Magyar", ["ro"] = "Română", ["bg"] = "Български", ["el"] = "Ελληνικά", ["da"] = "Dansk",
        ["sv"] = "Svenska", ["nb"] = "Norsk bokmål", ["fi"] = "Suomi", ["et"] = "Eesti", ["lv"] = "Latviešu",
        ["lt"] = "Lietuvių", ["sl"] = "Slovenščina", ["hr"] = "Hrvatski", ["uk"] = "Українська", ["ru"] = "Русский",
        ["tr"] = "Türkçe", ["sr"] = "Srpski (latinica)",
    };

    private static readonly Lazy<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>> Catalogs =
        new(LoadCatalogs, LazyThreadSafetyMode.ExecutionAndPublication);
    private static string _languageSetting = "auto";

    public static IReadOnlyList<string> SupportedLanguages { get; } = Array.AsReadOnly(Languages);
    public static IReadOnlyDictionary<string, string> LanguageNames => NativeNames;

    /// <summary>The active language code, resolving "auto" from the operating system UI culture.</summary>
    public static string CurrentLanguage => ResolveLanguage(Volatile.Read(ref _languageSetting));

    /// <summary>Select a supported language code, or "auto" to follow the operating system UI culture.</summary>
    public static void SetLanguage(string? language)
    {
        var value = NormalizeSetting(language);
        if (!IsSupportedLanguageSetting(value))
            throw new ArgumentException($"Unsupported UI language: {language}", nameof(language));
        Volatile.Write(ref _languageSetting, value);
    }

    public static bool IsSupportedLanguageSetting(string? language) =>
        language is not null && (language.Trim().Equals("auto", StringComparison.OrdinalIgnoreCase) ||
            Languages.Contains(language.Trim(), StringComparer.OrdinalIgnoreCase) ||
            RegionalCodes.Values.Contains(language.Trim(), StringComparer.OrdinalIgnoreCase));

    /// <summary>Translate a German source string; absent keys fall back to English and then the source.</summary>
    public static string Translate(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        var language = CurrentLanguage;
        if (TryGet(language, key, out var translated)) return translated;
        if (language != "en" && TryGet("en", key, out translated)) return translated;
        if (language != "de" && TryGet("de", key, out translated)) return translated;
        return key;
    }

    /// <summary>Translate and format a composite-format source string using the active UI culture.</summary>
    public static string Format(string template, params object?[] args)
    {
        ArgumentNullException.ThrowIfNull(template);
        ArgumentNullException.ThrowIfNull(args);
        return string.Format(GetCulture(CurrentLanguage), Translate(template), args);
    }

    /// <summary>Translate an interpolated source template while preserving its format arguments.</summary>
    public static string Translate(FormattableString text)
    {
        ArgumentNullException.ThrowIfNull(text);
        return string.Format(GetCulture(CurrentLanguage), Translate(text.Format), text.GetArguments());
    }

    /// <summary>
    /// Reports missing translation keys and mismatched composite-format placeholders for every locale.
    /// German keys define the source catalog; numeric placeholders may be reordered by translators.
    /// </summary>
    public static IReadOnlyList<string> ValidateResources()
    {
        var catalogs = Catalogs.Value;
        var issues = new List<string>();
        if (!catalogs.TryGetValue("de", out var source) || source.Count == 0)
            return ["German source catalog is missing or empty."];

        foreach (var language in Languages)
        {
            if (!catalogs.TryGetValue(language, out var catalog))
            {
                issues.Add($"{language}: embedded locale catalog is missing.");
                continue;
            }
            foreach (var key in source.Keys)
            {
                if (!catalog.TryGetValue(key, out var value))
                {
                    issues.Add($"{language}: missing key {key}");
                    continue;
                }
                if (!TryGetPlaceholders(key, out var sourcePlaceholders) || !TryGetPlaceholders(value, out var translatedPlaceholders))
                    issues.Add($"{language}: invalid composite format string for key {key}");
                else if (!sourcePlaceholders.SetEquals(translatedPlaceholders))
                    issues.Add($"{language}: placeholder mismatch for key {key} (expected {{{string.Join(",", sourcePlaceholders.Order())}}}, found {{{string.Join(",", translatedPlaceholders.Order())}}})");
            }
        }
        return issues;
    }

    private static bool TryGet(string language, string key, out string value)
    {
        if (Catalogs.Value.TryGetValue(language, out var catalog) && catalog.TryGetValue(key, out value!))
            return true;
        value = string.Empty;
        return false;
    }

    private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> LoadCatalogs()
    {
        var result = new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.Ordinal);
        var assembly = typeof(Localization).Assembly;
        foreach (var resourceName in assembly.GetManifestResourceNames().Where(name => name.StartsWith(ResourcePrefix, StringComparison.Ordinal) && name.EndsWith(".json", StringComparison.OrdinalIgnoreCase)))
        {
            var language = resourceName[ResourcePrefix.Length..^5];
            if (!Languages.Contains(language, StringComparer.Ordinal))
                continue;
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidDataException($"Embedded localization resource {resourceName} could not be opened.");
            using var document = JsonDocument.Parse(stream);
            if (document.RootElement.ValueKind != JsonValueKind.Object)
                throw new InvalidDataException($"Embedded localization resource {resourceName} must contain a JSON object.");
            var entries = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var property in document.RootElement.EnumerateObject())
            {
                if (property.Value.ValueKind != JsonValueKind.String)
                    throw new InvalidDataException($"Embedded localization key {property.Name} in {resourceName} must have a string value.");
                if (!entries.TryAdd(property.Name, property.Value.GetString()!))
                    throw new InvalidDataException($"Embedded localization resource {resourceName} contains duplicate key {property.Name}.");
            }
            result.Add(language, entries);
        }
        return result;
    }

    private static string ResolveLanguage(string setting)
    {
        if (setting != "auto") return RegionalCodes.FirstOrDefault(pair => pair.Value.Equals(setting, StringComparison.OrdinalIgnoreCase)).Key ?? setting;
        try
        {
            var osLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToLowerInvariant();
            return Languages.Contains(osLanguage, StringComparer.Ordinal) ? osLanguage : "en";
        }
        catch (CultureNotFoundException)
        {
            return "en";
        }
    }

    private static CultureInfo GetCulture(string language)
    {
        // Serbian UI copy is Latin-script; Serbian culture fallback may otherwise select Cyrillic conventions.
        var cultureName = language == "sr" ? "sr-Latn" : RegionalCodes.GetValueOrDefault(language, language);
        try { return CultureInfo.GetCultureInfo(cultureName); }
        catch (CultureNotFoundException) { return CultureInfo.GetCultureInfo("en"); }
    }

    private static string NormalizeSetting(string? language) => string.IsNullOrWhiteSpace(language)
        ? "auto"
        : language.Trim().ToLowerInvariant();

    private static bool TryGetPlaceholders(string format, out HashSet<int> placeholders)
    {
        placeholders = [];
        for (var index = 0; index < format.Length; index++)
        {
            if (format[index] == '{')
            {
                if (index + 1 < format.Length && format[index + 1] == '{') { index++; continue; }
                var close = format.IndexOf('}', index + 1);
                if (close < 0) return false;
                var token = format.AsSpan(index + 1, close - index - 1);
                var end = token.IndexOfAny(',', ':');
                if (end >= 0) token = token[..end];
                if (!int.TryParse(token, NumberStyles.None, CultureInfo.InvariantCulture, out var argumentIndex)) return false;
                placeholders.Add(argumentIndex);
                index = close;
            }
            else if (format[index] == '}' && (index + 1 >= format.Length || format[index + 1] != '}'))
            {
                return false;
            }
            else if (format[index] == '}' && index + 1 < format.Length)
            {
                index++;
            }
        }
        return true;
    }
}

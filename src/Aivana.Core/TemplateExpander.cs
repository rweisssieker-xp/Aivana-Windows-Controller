using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Aivana.Core;

public static class TemplateExpander
{
    private static readonly string[] TokenValues = ["{{date}}", "{{time}}", "{{datetime}}", "{{clipboard}}", "{{profile}}", "{{process}}", "{{user}}"];
    private static readonly IReadOnlyList<string> ReadOnlyTokenValues = new ReadOnlyCollection<string>(TokenValues);
    private static readonly Regex TokenPattern = new(@"\{\{(?<name>[a-z]+)\}\}", RegexOptions.CultureInvariant);

    public static IReadOnlyList<string> Tokens => ReadOnlyTokenValues;

    public static string Expand(string template, DateTime dateTime, string clipboard = "", string profile = "", string process = "")
    {
        ArgumentNullException.ThrowIfNull(template);
        var values = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["date"] = dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            ["time"] = dateTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            ["datetime"] = dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
            ["clipboard"] = clipboard,
            ["profile"] = profile,
            ["process"] = process,
            ["user"] = Environment.UserName,
        };

        return TokenPattern.Replace(template, match =>
            values.TryGetValue(match.Groups["name"].Value, out var value) ? value : match.Value);
    }
}

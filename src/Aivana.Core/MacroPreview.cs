using System.Text.RegularExpressions;

namespace Aivana.Core;

public static class MacroPreview
{
    private static readonly Regex TokenPattern = new(@"\{\{[a-z]+\}\}", RegexOptions.CultureInvariant);

    public static IReadOnlyList<string> Build(Tile tile)
    {
        ArgumentNullException.ThrowIfNull(tile);
        var repeatLine = tile.Repeat == 1
            ? Localization.Translate((FormattableString)$"Wiederholung: {tile.Repeat} Durchlauf")
            : Localization.Translate((FormattableString)$"Wiederholung: {tile.Repeat} Durchläufe");
        var lines = new List<string> { repeatLine };
        if (tile.Steps is null) return lines;

        foreach (var step in tile.Steps)
        {
            if (step is null)
            {
                lines.Add(Localization.Translate("Leerer Schritt"));
                continue;
            }

            var value = step.Value ?? "";
            var description = step.Kind switch
            {
                ActionKind.Launch => Localization.Translate((FormattableString)$"Programm starten: {value}"),
                ActionKind.OpenPath => Localization.Translate((FormattableString)$"Pfad öffnen: {value}"),
                ActionKind.Url => Localization.Translate((FormattableString)$"Webseite öffnen: {value}"),
                ActionKind.Text => Localization.Translate((FormattableString)$"Text eingeben: „{value}“"),
                ActionKind.Shortcut => Localization.Translate((FormattableString)$"Tastenkürzel senden: {value}"),
                ActionKind.Media => Localization.Translate((FormattableString)$"Medienaktion: {value}"),
                ActionKind.Brightness => Localization.Format("Bildschirmhelligkeit: {0}", value),
                ActionKind.Volume => Localization.Translate((FormattableString)$"Lautstärkeaktion: {value}"),
                ActionKind.SwitchProfile => Localization.Translate((FormattableString)$"Profil wechseln: {value}"),
                ActionKind.WaitWindow => Localization.Format("Auf Zielfenster warten: Prozess {0} · Titel {1} · Zeitlimit {2} ms", value, step.Arguments, step.DelayMs),
                ActionKind.Delay => DescribeDelay(step.DelayMs),
                ActionKind.CorsairColor => Localization.Translate((FormattableString)$"Corsair-Farbe setzen: {value}"),
                ActionKind.Clipboard => Localization.Translate((FormattableString)$"Zwischenablage setzen: {value}"),
                ActionKind.Window => Localization.Translate((FormattableString)$"Fensteraktion: {value}"),
                ActionKind.System => Localization.Translate((FormattableString)$"Systemaktion: {value}"),
                ActionKind.Obs => DescribeObs(step),
                _ => Localization.Translate((FormattableString)$"Unbekannte Aktion: {value}"),
            };
            var tokens = TokenPattern.Matches(value).Select(match => match.Value).Distinct(StringComparer.Ordinal).ToArray();
            if (tokens.Length > 0) description += Localization.Translate((FormattableString)$" · Variablen: {string.Join(", ", tokens)}");
            lines.Add(description);
        }
        return lines;
    }

    private static string DescribeObs(ActionStep step)
    {
        var source = step.Value switch
        {
            "StartRecord" => "OBS-Aufnahme starten", "StopRecord" => "OBS-Aufnahme stoppen", "ToggleRecord" => "OBS-Aufnahme umschalten",
            "PauseRecord" => "OBS-Aufnahme pausieren", "ResumeRecord" => "OBS-Aufnahme fortsetzen", "StartStream" => "OBS-Stream starten",
            "StopStream" => "OBS-Stream stoppen", "ToggleStream" => "OBS-Stream umschalten", "SetCurrentProgramScene" => "OBS-Szene wechseln",
            "ToggleInputMute" => "OBS-Mikrofon umschalten", _ => step.Value
        };
        return string.Join(" · ", new[] { Localization.Translate(source), step.Arguments,
            string.IsNullOrWhiteSpace(step.WorkingDirectory) ? "ws://127.0.0.1:4455" : step.WorkingDirectory }.Where(value => !string.IsNullOrWhiteSpace(value)));
    }

    private static string DescribeDelay(int milliseconds) => milliseconds % 1000 == 0
        ? Localization.Translate((FormattableString)$"Warten: {milliseconds / 1000} Sekunden")
        : Localization.Translate((FormattableString)$"Warten: {milliseconds / 1000d:0.##} Sekunden");
}

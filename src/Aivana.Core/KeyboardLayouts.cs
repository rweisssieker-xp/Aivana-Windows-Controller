namespace Aivana.Core;

public enum KeyboardLayoutProvenance
{
    GenericReference,
    ManufacturerInformed,
    ModelVerified,
}

public enum KeyboardFormFactor
{
    FullSize,
    Tenkeyless,
}

public enum KeyboardKeyShape
{
    Rectangle,
    IsoEnterL,
    Circle,
    Wheel,
}

/// <summary>A keyboard reference geometry. Coordinates and sizes use key units, not millimetres.</summary>
public sealed record KeyboardLayoutDefinition(
    string Id,
    string DisplayName,
    string Locale,
    KeyboardFormFactor FormFactor,
    KeyboardLayoutProvenance Provenance,
    string ProvenanceDescription,
    IReadOnlyList<KeyboardKeyGeometry> Keys);

/// <summary>
/// A physical key or special control. PhysicalKeyId is a USB HID usage identity for standard keys;
/// HotkeyOutput is a separate value used by the hotkey editor. ScanCode is a Set-1 make code.
/// </summary>
public sealed record KeyboardKeyGeometry(
    string PhysicalKeyId,
    string TopLegend,
    string BottomLegend,
    double X,
    double Y,
    double Width,
    double Height,
    KeyboardKeyShape Shape,
    ushort ScanCode,
    bool Extended,
    string HotkeyOutput,
    bool AvailableForHotkey,
    string Tooltip);

/// <summary>Static generic keyboard geometries and documented K100 control arrangements.</summary>
public static class KeyboardLayoutCatalog
{
    private static readonly IReadOnlyList<KeyboardLayoutDefinition> Layouts = Array.AsReadOnly(
    [
        BuildStandard("generic-iso-de-fullsize", "ISO-DE · Fullsize (generisch)", "ISO-DE", KeyboardFormFactor.FullSize, german: true),
        BuildStandard("generic-iso-de-tkl", "ISO-DE · TKL (generisch)", "ISO-DE", KeyboardFormFactor.Tenkeyless, german: true),
        BuildStandard("generic-ansi-us-fullsize", "ANSI-US · Fullsize (generisch)", "ANSI-US", KeyboardFormFactor.FullSize, german: false),
        BuildStandard("generic-ansi-us-tkl", "ANSI-US · TKL (generisch)", "ANSI-US", KeyboardFormFactor.Tenkeyless, german: false),
        BuildK100("corsair-k100-iso-de", "Corsair K100 RGB · ISO-DE", "ISO-DE", german: true),
        BuildK100("corsair-k100-ansi-us", "Corsair K100 RGB · ANSI-US", "ANSI-US", german: false),
    ]);

    public static IReadOnlyList<KeyboardLayoutDefinition> All => Layouts;

    public static KeyboardLayoutDefinition Get(string id) =>
        TryGet(id, out var layout) ? layout : throw new KeyNotFoundException($"Unbekanntes Tastaturlayout: {id}");

    public static bool TryGet(string? id, out KeyboardLayoutDefinition layout)
    {
        layout = Layouts.FirstOrDefault(candidate => candidate.Id.Equals(id, StringComparison.OrdinalIgnoreCase))!;
        return layout is not null;
    }

    private static KeyboardLayoutDefinition BuildStandard(string id, string name, string locale, KeyboardFormFactor form, bool german)
    {
        var keys = BuildBaseKeys(german);
        AddNavigationAndFunctionKeys(keys);
        if (form == KeyboardFormFactor.FullSize) AddNumpad(keys);
        return Definition(id, name, locale, form, KeyboardLayoutProvenance.GenericReference,
            "Generische Referenzgeometrie; reale Tastaturen können in Abständen und Tastenformen abweichen.", keys);
    }

    private static KeyboardLayoutDefinition BuildK100(string id, string name, string locale, bool german)
    {
        var mainOffset = 1.25;
        var keys = BuildBaseKeys(german, leftOffset: mainOffset);
        AddNavigationAndFunctionKeys(keys, mainOffset);
        AddNumpad(keys, mainOffset);

        // The K100 manual shows Fn between right Alt and Menu, rather than a right Windows key.
        var rightWindowsIndex = keys.FindIndex(key => key.PhysicalKeyId == "HID:0xE7");
        var rightWindows = keys[rightWindowsIndex];
        keys[rightWindowsIndex] = VendorKey("CORSAIR:K100:FN", "Fn", rightWindows.X, rightWindows.Y,
            rightWindows.Width, rightWindows.Height,
            "Fn ist eine geräteinterne Ebenentaste und wird nicht als eigenständiger Windows-Hotkey ausgegeben.");

        for (var index = 0; index < 6; index++)
        {
            var label = $"G{index + 1}";
            keys.Add(VendorKey($"CORSAIR:K100:{label}", label, 0, index, 1, 0.88,
                "Diese K100-Zusatztaste ist kein normales Windows-Tastaturereignis. Eine Zuordnung benötigt eine bestätigte iCUE-SDK-Unterstützung."));
        }
        keys.Add(VendorKey("CORSAIR:K100:PROFILE", "Profil", 0.2, -1.2, 0.8, 0.7,
            "Profilwahltaste des Geräts; sie ist nicht als normaler Windows-Hotkey verfügbar."));

        keys.Add(VendorKey("CORSAIR:K100:ICUE_WHEEL", "iCUE", mainOffset, -1.2, 1, 1,
            "Programmierbares iCUE-Drehrad mit integrierter Druckfunktion; eine Zuordnung benötigt eine bestätigte iCUE-SDK-Unterstützung.", KeyboardKeyShape.Wheel));
        keys.Add(VendorKey("CORSAIR:K100:WIN_LOCK", "Win-Lock", mainOffset + 1.3, -1.2, 0.9, 0.7,
            "Windows-Sperrtaste des Geräts; kein normales Tastaturereignis."));

        var numpadX = mainOffset + 18.5;
        keys.Add(MediaKey("CORSAIR:K100:MEDIA_STOP", "■", "MediaStop", numpadX, 0));
        keys.Add(MediaKey("CORSAIR:K100:MEDIA_PREVIOUS", "⏮", "MediaPreviousTrack", numpadX + 1, 0));
        keys.Add(MediaKey("CORSAIR:K100:MEDIA_PLAYPAUSE", "⏯", "MediaPlayPause", numpadX + 2, 0));
        keys.Add(MediaKey("CORSAIR:K100:MEDIA_NEXT", "⏭", "MediaNextTrack", numpadX + 3, 0));
        keys.Add(MediaKey("CORSAIR:K100:VOLUME_MUTE", "Mute", "VolumeMute", numpadX - 1.2, -1.2));
        keys.Add(VendorKey("CORSAIR:K100:VOLUME_ROLLER", "Lautstärke", numpadX + 1, -1.2, 3, 0.65,
            "Lautstärkerolle; Drehbewegungen werden nicht als einzelne Tastenkombination abgebildet.", KeyboardKeyShape.Wheel));

        return Definition(id, name, locale, KeyboardFormFactor.FullSize, KeyboardLayoutProvenance.ManufacturerInformed,
            "Corsair dokumentiert das K100 RGB mit G-Tasten, Profilwahl, iCUE-Drehrad, Medien-/Lautstärkesteuerung und Win-Lock. Die Proportionen sind schematisch, nicht in Millimetern vermessen. Die Sprachvariante muss ausdrücklich gewählt werden.", keys);
    }

    private static KeyboardLayoutDefinition Definition(string id, string name, string locale, KeyboardFormFactor form,
        KeyboardLayoutProvenance provenance, string provenanceDescription, List<KeyboardKeyGeometry> keys)
    {
        EnsureUniquePhysicalIds(id, keys);
        return new KeyboardLayoutDefinition(id, name, locale, form, provenance, provenanceDescription, keys.AsReadOnly());
    }

    private static void EnsureUniquePhysicalIds(string layoutId, IReadOnlyList<KeyboardKeyGeometry> keys)
    {
        var duplicate = keys.GroupBy(key => key.PhysicalKeyId, StringComparer.Ordinal).FirstOrDefault(group => group.Count() > 1);
        if (duplicate is not null) throw new InvalidOperationException($"Das Layout {layoutId} enthält die Taste {duplicate.Key} mehrfach.");
    }

    private static List<KeyboardKeyGeometry> BuildBaseKeys(bool german, double leftOffset = 0)
    {
        var keys = new List<KeyboardKeyGeometry>();
        double x = leftOffset;
        Add(keys, "29", "Esc", "", x, 0, 1, 0.7, 0x01, "Escape");
        x = leftOffset + 2.0;
        var functionCodes = new ushort[] { 0x3B, 0x3C, 0x3D, 0x3E, 0x3F, 0x40, 0x41, 0x42, 0x43, 0x44, 0x57, 0x58 };
        for (var group = 0; group < 3; group++)
        {
            for (var index = 0; index < 4; index++)
            {
                var number = group * 4 + index + 1;
                Add(keys, ((ushort)(0x39 + number)).ToString("X2"), $"F{number}", "", x, 0, 1, 0.7, functionCodes[number - 1], $"F{number}");
                x += 1;
            }
            if (group < 2) x += 0.5;
        }

        var numberRow = german
            ? new (string id, string top, string bottom, ushort scan, string output)[]
            {
                ("35", "°", "^", 0x29, "Oem3"), ("1E", "!", "1", 0x02, "D1"), ("1F", "\"", "2", 0x03, "D2"), ("20", "§", "3", 0x04, "D3"),
                ("21", "$", "4", 0x05, "D4"), ("22", "%", "5", 0x06, "D5"), ("23", "&", "6", 0x07, "D6"), ("24", "/", "7", 0x08, "D7"),
                ("25", "(", "8", 0x09, "D8"), ("26", ")", "9", 0x0A, "D9"), ("27", "=", "0", 0x0B, "D0"), ("2D", "?", "ß", 0x0C, "OemMinus"),
                ("2E", "`", "´", 0x0D, "OemPlus"),
            }
            : new (string id, string top, string bottom, ushort scan, string output)[]
            {
                ("35", "~", "`", 0x29, "Oem3"), ("1E", "!", "1", 0x02, "D1"), ("1F", "@", "2", 0x03, "D2"), ("20", "#", "3", 0x04, "D3"),
                ("21", "$", "4", 0x05, "D4"), ("22", "%", "5", 0x06, "D5"), ("23", "^", "6", 0x07, "D6"), ("24", "&", "7", 0x08, "D7"),
                ("25", "*", "8", 0x09, "D8"), ("26", "(", "9", 0x0A, "D9"), ("27", ")", "0", 0x0B, "D0"), ("2D", "_", "-", 0x0C, "OemMinus"),
                ("2E", "+", "=", 0x0D, "OemPlus"),
            };
        x = leftOffset;
        AddRow(keys, numberRow, ref x, 1, width: 1);
        Add(keys, "2A", "Backspace", "", x, 1, 2, 1, 0x0E, "Backspace");

        var qRowX = leftOffset;
        Add(keys, "2B", "Tab", "", qRowX, 2, 1.5, 1, 0x0F, "Tab");
        qRowX += 1.5;
        AddLetterRow(keys, ["Q", "W", "E", "R", "T", german ? "Z" : "Y", "U", "I", "O", "P"], qRowX, 2,
            [0x14, 0x1A, 0x08, 0x15, 0x17, 0x1C, 0x18, 0x0C, 0x12, 0x13], [0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19]);
        qRowX += 10;
        if (german)
        {
            Add(keys, "2F", "Ü", "", qRowX++, 2, 1, 1, 0x1A, "Oem4");
            Add(keys, "30", "+", "*", qRowX++, 2, 1, 1, 0x1B, "Oem6");
            Add(keys, "28", "Enter", "", qRowX, 2, 1.5, 2, 0x1C, "Enter", KeyboardKeyShape.IsoEnterL);
        }
        else
        {
            Add(keys, "2F", "{", "[", qRowX++, 2, 1, 1, 0x1A, "Oem4");
            Add(keys, "30", "}", "]", qRowX++, 2, 1, 1, 0x1B, "Oem6");
            Add(keys, "31", "|", "\\", qRowX, 2, 1.5, 1, 0x2B, "Oem5");
        }

        var homeX = leftOffset;
        Add(keys, "39", "Caps Lock", "", homeX, 3, 1.75, 1, 0x3A, "CapsLock");
        homeX += 1.75;
        AddLetterRow(keys, ["A", "S", "D", "F", "G", "H", "J", "K", "L"], homeX, 3,
            [0x04, 0x16, 0x07, 0x09, 0x0A, 0x0B, 0x0D, 0x0E, 0x0F], [0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26]);
        homeX += 9;
        if (german)
        {
            Add(keys, "33", "Ö", "", homeX++, 3, 1, 1, 0x27, "Oem1");
            Add(keys, "34", "Ä", "", homeX++, 3, 1, 1, 0x28, "Oem7");
            Add(keys, "32", "#", "'", homeX++, 3, 1, 1, 0x2B, "Oem102");
        }
        else
        {
            Add(keys, "33", ":", ";", homeX++, 3, 1, 1, 0x27, "Oem1");
            Add(keys, "34", "\"", "'", homeX++, 3, 1, 1, 0x28, "Oem7");
            Add(keys, "28", "Enter", "", homeX, 3, 2.25, 1, 0x1C, "Enter");
        }

        var bottomX = leftOffset;
        if (german)
        {
            Add(keys, "E1", "Shift", "", bottomX, 4, 1.25, 1, 0x2A, "Shift");
            bottomX += 1.25;
            Add(keys, "64", "<", ">", bottomX++, 4, 1, 1, 0x56, "Oem102");
        }
        else
        {
            Add(keys, "E1", "Shift", "", bottomX, 4, 2.25, 1, 0x2A, "Shift");
            bottomX += 2.25;
        }
        var bottomLetters = german ? new[] { "Y", "X", "C", "V", "B", "N", "M" } : new[] { "Z", "X", "C", "V", "B", "N", "M" };
        AddLetterRow(keys, bottomLetters, bottomX, 4, [0x1D, 0x1B, 0x06, 0x19, 0x05, 0x11, 0x10], [0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32]);
        bottomX += 7;
        Add(keys, "36", german ? "," : "<", german ? ";" : ",", bottomX++, 4, 1, 1, 0x33, "OemComma");
        Add(keys, "37", german ? "." : ">", german ? ":" : ".", bottomX++, 4, 1, 1, 0x34, "OemPeriod");
        Add(keys, "38", german ? "-" : "?", german ? "_" : "/", bottomX++, 4, 1, 1, 0x35, "Oem2");
        Add(keys, "E5", "Shift", "", bottomX, 4, german ? 2.75 : 2.75, 1, 0x36, "Shift");

        var modX = leftOffset;
        Add(keys, "E0", "Ctrl", "", modX, 5, 1.25, 1, 0x1D, "Ctrl"); modX += 1.25;
        Add(keys, "E3", "Win", "", modX, 5, 1.25, 1, 0x5B, "Win", extended: true); modX += 1.25;
        Add(keys, "E2", "Alt", "", modX, 5, 1.25, 1, 0x38, "Alt"); modX += 1.25;
        Add(keys, "2C", "Space", "", modX, 5, 5.5, 1, 0x39, "Space"); modX += 5.5;
        Add(keys, "E6", "AltGr", "", modX, 5, 1.25, 1, 0x38, "Alt", extended: true); modX += 1.25;
        Add(keys, "E7", "Win", "", modX, 5, 1.25, 1, 0x5C, "Win", extended: true); modX += 1.25;
        Add(keys, "65", "Menu", "", modX, 5, 1.25, 1, 0x5D, "Apps", extended: true); modX += 1.25;
        Add(keys, "E4", "Ctrl", "", modX, 5, 1.25, 1, 0x1D, "Ctrl", extended: true);
        return keys;
    }

    private static void AddNavigationAndFunctionKeys(List<KeyboardKeyGeometry> keys, double mainOffset = 0)
    {
        var navX = mainOffset + 15.25;
        var navY = 0.0;
        Add(keys, "46", "Print", "", navX, navY, 1, 0.7, 0x37, "PrintScreen", extended: true);
        Add(keys, "47", "Scroll", "", navX + 1, navY, 1, 0.7, 0x46, "ScrollLock");
        Add(keys, "48", "Pause", "", navX + 2, navY, 1, 0.7, 0, "Pause",
            tooltip: "Pause verwendet die Windows-Taste Pause. Ihre E1-Scan-Sequenz ist im Scan-Code-Feld nicht darstellbar.");
        AddNavigationBlock(keys, navX, 1, ["Insert", "Home", "PageUp"], [0x52, 0x47, 0x49], ["49", "4A", "4B"], [true, true, true]);
        AddNavigationBlock(keys, navX, 2, ["Delete", "End", "PageDown"], [0x53, 0x4F, 0x51], ["4C", "4D", "4E"], [true, true, true]);
        Add(keys, "52", "↑", "", navX + 1, 4, 1, 1, 0x48, "Up", extended: true);
        Add(keys, "50", "←", "", navX, 5, 1, 1, 0x4B, "Left", extended: true);
        Add(keys, "51", "↓", "", navX + 1, 5, 1, 1, 0x50, "Down", extended: true);
        Add(keys, "4F", "→", "", navX + 2, 5, 1, 1, 0x4D, "Right", extended: true);
    }

    private static void AddNavigationBlock(List<KeyboardKeyGeometry> keys, double x, double y, string[] labels, ushort[] scans, string[] ids, bool[] extended)
    {
        for (var index = 0; index < labels.Length; index++)
            Add(keys, ids[index], labels[index], "", x + index, y, 1, 1, scans[index], labels[index], extended: extended[index]);
    }

    private static void AddNumpad(List<KeyboardKeyGeometry> keys, double mainOffset = 0)
    {
        var x = mainOffset + 18.5;
        var top = 1.0;
        Add(keys, "53", "Num", "", x, top, 1, 1, 0x45, "NumLock");
        Add(keys, "54", "/", "", x + 1, top, 1, 1, 0x35, "Divide", extended: true);
        Add(keys, "55", "*", "", x + 2, top, 1, 1, 0x37, "Multiply");
        Add(keys, "56", "−", "", x + 3, top, 1, 1, 0x4A, "Subtract");
        ushort[,] keypadScans = { { 0x47, 0x48, 0x49 }, { 0x4B, 0x4C, 0x4D }, { 0x4F, 0x50, 0x51 } };
        for (var row = 0; row < 3; row++)
        {
            for (var column = 0; column < 3; column++)
            {
                var digit = 7 - row * 3 + column;
                Add(keys, ((ushort)(0x59 + digit - 1)).ToString("X2"), digit.ToString(), "", x + column, top + 1 + row, 1, 1,
                    keypadScans[row, column], $"NumPad{digit}");
            }
        }
        Add(keys, "57", "+", "", x + 3, top + 1, 1, 2, 0x4E, "Add");
        Add(keys, "62", "0", "", x, top + 4, 2, 1, 0x52, "NumPad0");
        Add(keys, "63", ".", "", x + 2, top + 4, 1, 1, 0x53, "Decimal");
        Add(keys, "58", "Enter", "", x + 3, top + 3, 1, 2, 0x1C, "Enter", extended: true);
    }

    private static void AddLetterRow(List<KeyboardKeyGeometry> keys, string[] labels, double x, double y, ushort[] hidUsages, ushort[] scans)
    {
        for (var index = 0; index < labels.Length; index++)
            Add(keys, hidUsages[index].ToString("X2"), labels[index], "", x + index, y, 1, 1, scans[index], labels[index]);
    }

    private static void AddRow(List<KeyboardKeyGeometry> keys, (string id, string top, string bottom, ushort scan, string output)[] row, ref double x, double y, double width)
    {
        foreach (var key in row)
        {
            Add(keys, key.id, key.top, key.bottom, x, y, width, 1, key.scan, key.output);
            x += width;
        }
    }

    private static KeyboardKeyGeometry VendorKey(string id, string label, double x, double y, double width, double height,
        string tooltip, KeyboardKeyShape shape = KeyboardKeyShape.Rectangle) =>
        new(id, label, "", x, y, width, height, shape, 0, false, "", false, tooltip);

    private static KeyboardKeyGeometry MediaKey(string id, string label, string output, double x, double y) =>
        new(id, label, "", x, y, 0.85, 0.75, KeyboardKeyShape.Rectangle, 0, false, output, true,
            "Dedizierte Mediensteuerung; Hotkey-Ausgabe verwendet den Windows-Medien-Tastencode.");

    private static void Add(List<KeyboardKeyGeometry> keys, string hidUsage, string top, string bottom, double x, double y,
        double width, double height, ushort scanCode, string output, KeyboardKeyShape shape = KeyboardKeyShape.Rectangle,
        bool extended = false, bool available = true, string tooltip = "") =>
        keys.Add(new KeyboardKeyGeometry($"HID:0x{hidUsage}", top, bottom, x, y, width, height, shape, scanCode, extended,
            output, available, tooltip));
}

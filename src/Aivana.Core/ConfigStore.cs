using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aivana.Core;

public sealed class ConfigStore
{
    private const long MaximumConfigBytes = 10 * 1024 * 1024;
    private readonly string _directory;
    private readonly string _settingsPath;
    private static readonly JsonSerializerOptions JsonOptions = CreateJsonOptions();

    public ConfigStore(string? directory = null)
    {
        _directory = string.IsNullOrWhiteSpace(directory)
            ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AivanaWindowsController")
            : Path.GetFullPath(directory);
        _settingsPath = Path.Combine(_directory, "settings.json");
    }

    public AppSettings Load()
    {
        if (!File.Exists(_settingsPath))
        {
            var defaults = CreateDefaults();
            Save(defaults);
            return defaults;
        }
        return ReadAndValidate(_settingsPath);
    }

    public AppSettings RecoverBackup()
    {
        var backupPath = _settingsPath + ".bak";
        if (!File.Exists(backupPath))
            throw new FileNotFoundException(Localization.Translate("Es ist keine Sicherungskopie vorhanden."), backupPath);

        // Validate the backup before changing either file. Keep the damaged current
        // file beside the restored one so recovery remains reversible.
        var backup = ReadAndValidate(backupPath);
        if (File.Exists(_settingsPath))
        {
            var preservedPath = _settingsPath + ".corrupt-" + DateTimeOffset.Now.ToString("yyyyMMdd-HHmmss-fff") + ".json";
            if (File.Exists(preservedPath)) preservedPath = _settingsPath + ".corrupt-" + Guid.NewGuid().ToString("N") + ".json";
            File.Copy(_settingsPath, preservedPath, overwrite: false);
        }
        WriteAtomically(_settingsPath, backup);
        return backup;
    }

    public void Save(AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ThrowIfInvalid(settings, Localization.Translate("Die Konfiguration kann nicht gespeichert werden"));

        Directory.CreateDirectory(_directory);
        if (File.Exists(_settingsPath))
        {
            // Preserve the previous known-good version. Do not replace a damaged file
            // without first making that state visible to the caller.
            _ = ReadAndValidate(_settingsPath);
            var backupPath = _settingsPath + ".bak";
            var backupTemp = backupPath + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try
            {
                File.Copy(_settingsPath, backupTemp, overwrite: true);
                File.Move(backupTemp, backupPath, overwrite: true);
            }
            finally
            {
                TryDelete(backupTemp);
            }
        }
        WriteAtomically(_settingsPath, settings);
    }

    public AppSettings ReadImport(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        var imported = ReadAndValidate(Path.GetFullPath(path));
        foreach (var tile in imported.Profiles.SelectMany(profile => profile.Tiles))
            tile.Enabled = false;
        imported.AutoStart = false;
        imported.AutoProfiles = false;
        return imported;
    }

    public void Export(AppSettings settings, string path)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ThrowIfInvalid(settings, Localization.Translate("Die Konfiguration kann nicht exportiert werden"));
        WriteAtomically(Path.GetFullPath(path), settings);
    }

    public static AppSettings CreateDefaults()
    {
        var standard = new Profile { Name = "Standard" };
        standard.Tiles =
        [
            new Tile { Name = "Wiedergabe", Icon = "▶", Steps = [new ActionStep { Kind = ActionKind.Media, Value = "PlayPause" }] },
            new Tile { Name = "Nächster Titel", Icon = "⏭", Steps = [new ActionStep { Kind = ActionKind.Media, Value = "Next" }] },
            new Tile { Name = "Lauter", Icon = "🔊", Steps = [new ActionStep { Kind = ActionKind.Volume, Value = "Up" }] },
            new Tile { Name = "Leiser", Icon = "🔉", Steps = [new ActionStep { Kind = ActionKind.Volume, Value = "Down" }] },
            new Tile { Name = "Stumm", Icon = "🔇", Steps = [new ActionStep { Kind = ActionKind.Volume, Value = "Mute" }] },
        ];
        return new AppSettings
        {
            Profiles = [standard],
            DefaultProfileId = standard.Id,
            ActiveProfileId = standard.Id,
            AutoProfiles = false,
            AutoStart = false,
            CloseToTray = true,
            CancelHotkey = "Ctrl+Alt+Escape",
            PaletteHotkey = "Ctrl+Shift+Space",
        };
    }

    private static AppSettings ReadAndValidate(string path)
    {
        try
        {
            var fileInfo = new FileInfo(path);
            if (fileInfo.Length > MaximumConfigBytes)
                throw new InvalidDataException(Localization.Translate("Die Konfigurationsdatei überschreitet die zulässige Größe von 10 MiB."));
            using var stream = File.OpenRead(path);
            var settings = JsonSerializer.Deserialize<AppSettings>(stream, JsonOptions)
                ?? throw new InvalidDataException(Localization.Translate("Die JSON-Datei enthält kein Konfigurationsobjekt."));
            ThrowIfInvalid(settings, Localization.Translate("Die Konfiguration ist ungültig"));
            return settings;
        }
        catch (InvalidDataException) { throw; }
        catch (JsonException exception)
        {
            throw new InvalidDataException(Localization.Translate((FormattableString)$"Die JSON-Konfiguration „{path}“ ist beschädigt."), exception);
        }
    }

    private static void ThrowIfInvalid(AppSettings settings, string message)
    {
        var errors = ConfigurationValidator.Validate(settings);
        if (errors.Count > 0)
            throw new InvalidDataException(message + ": " + string.Join(" ", errors));
    }

    private static void WriteAtomically(string path, AppSettings settings)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
        var temporaryPath = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.WriteThrough))
            {
                JsonSerializer.Serialize(stream, settings, JsonOptions);
                stream.Flush(flushToDisk: true);
            }
            File.Move(temporaryPath, path, overwrite: true);
        }
        finally
        {
            TryDelete(temporaryPath);
        }
    }

    private static JsonSerializerOptions CreateJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = false,
            ReadCommentHandling = JsonCommentHandling.Disallow,
        };
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }

    private static void TryDelete(string path)
    {
        try { if (File.Exists(path)) File.Delete(path); }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }
}

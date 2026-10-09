namespace Aivana.Core;

public enum ActionKind { Launch, OpenPath, Url, Text, Shortcut, Media, Volume, SwitchProfile, Delay, CorsairColor, Clipboard, Window, System, Obs }
public sealed class ActionStep
{
    public ActionKind Kind { get; set; }
    public string Value { get; set; } = "";
    public string Arguments { get; set; } = "";
    public string WorkingDirectory { get; set; } = "";
    // Empty follows Windows' default multimedia endpoint. Explicit IDs never fall back silently.
    public string? AudioDeviceId { get; set; }
    public int DelayMs { get; set; } = 100;
}
public sealed class Tile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Neue Aktion";
    public string Icon { get; set; } = "▶";
    public string Color { get; set; } = "#25C4B6";
    public string Hotkey { get; set; } = "";
    public CorsairBinding? CorsairBinding { get; set; }
    public bool Enabled { get; set; } = true;
    public bool Favorite { get; set; }
    public int Repeat { get; set; } = 1;
    public List<ActionStep> Steps { get; set; } = [];
}
public sealed record CorsairBinding(string DeviceId, int KeyId);
public sealed class Profile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Standard";
    public string ProcessName { get; set; } = "";
    public string WindowTitleContains { get; set; } = "";
    public List<Tile> Tiles { get; set; } = [];
}
public sealed class AppSettings
{
    public int Version { get; set; } = 1;
    public Guid ActiveProfileId { get; set; }
    public Guid DefaultProfileId { get; set; }
    public bool AutoProfiles { get; set; }
    public bool AutoStart { get; set; }
    public bool CloseToTray { get; set; } = true;
    public string CancelHotkey { get; set; } = "Ctrl+Alt+Escape";
    // Empty keeps older JSON files without the new palette setting valid.
    public string PaletteHotkey { get; set; } = "";
    public string KeyboardLayoutId { get; set; } = "generic-iso-de-fullsize";
    // "auto" follows Windows' display culture; language selection is independent of keyboard layout.
    public string UiLanguage { get; set; } = "auto";
    public List<Profile> Profiles { get; set; } = [];
}
public sealed record DeviceInfo(string Name, string Path, string VendorId, string ProductId, string Manufacturer, string Capabilities);
public sealed record ActivityEntry(DateTime Time, string Name, string Status);
public interface IActionExecutor { Task ExecuteAsync(ActionStep step, CancellationToken cancellationToken); }

using Aivana.Core;
using Aivana.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Aivana.Desktop;

public sealed partial class DesktopController : IDisposable
{
    private readonly ConfigStore _store;
    private readonly DeviceService _devices = new();
    private readonly CorsairService _corsair = new();
    private readonly MacroRunner _runner;
    private readonly WinActionExecutor _executor;
    private int _executionEpoch;
    private int _inputInjectionDepth;
    private readonly List<ActivityEntry> _activity = [];
    private readonly Dictionary<Guid, string> _tileStatuses = [];
    private readonly Dictionary<Guid, (int Count, DateTime Last)> _usage = [];
    private readonly PendingExecutionCancellation _pendingExecutions = new();
    private IntPtr _lastExternalWindow;
    private readonly AsyncLocal<string?> _executionProfile = new();
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };
    private HotkeyService? _hotkeys;
    private HwndSource? _source;
    private bool _disposed;
    private int _tick;
    public AppSettings Settings { get; private set; }
    public Profile ActiveProfile => Settings.Profiles.First(p => p.Id == Settings.ActiveProfileId);
    public bool Paused { get; private set; }
    public IReadOnlyList<DeviceInfo> Devices { get; private set; } = [];
    public string CorsairStatus => _corsair.Status;
    public bool CorsairAvailable => _corsair.IsAvailable;
    public IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards => _corsair.Keyboards;
    public string ForegroundProcess { get; private set; } = "";
    public string ForegroundWindowTitle { get; private set; } = "";
    public bool ProfileLocked { get; private set; }
    public int GetRunCount(Guid id) => _usage.TryGetValue(id, out var usage) ? usage.Count : 0;
    public DateTime? GetLastRun(Guid id) => _usage.TryGetValue(id, out var usage) ? usage.Last : null;
    public string LastStatus { get; private set; } = Aivana.Core.Localization.Translate("Bereit");
    public bool HasRunningActions => _runner.HasRunningActions;
    public bool IsRunning(Guid id) => _runner.IsRunning(id);
    public string GetTileStatus(Guid id) => _tileStatuses.GetValueOrDefault(id, Aivana.Core.Localization.Translate("Bereit"));
    public IReadOnlyList<ActivityEntry> Activity => _activity;
    public Dictionary<Guid, string> HotkeyErrors { get; private set; } = [];
    public event Action? Changed;
    public event Action? PaletteRequested;
    public DesktopController(string? configurationDirectory = null)
    {
        _store = new ConfigStore(configurationDirectory);
        try { Settings = _store.Load(); }
        catch (InvalidDataException ex) when (configurationDirectory == null)
        {
            if (System.Windows.MessageBox.Show(ex.Message + "\n\n" + Aivana.Core.Localization.Translate("Die letzte gültige Sicherung wiederherstellen? Die beschädigte Datei wird separat erhalten."), Aivana.Core.Localization.Translate("Konfiguration beschädigt"), MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) throw;
            Settings = _store.RecoverBackup();
        }
        _executor = new WinActionExecutor(name =>
        {
            var epoch = _executionEpoch;
            return System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (_disposed || epoch != _executionEpoch) return;
                var profile = Settings.Profiles.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || p.Id.ToString() == name);
                if (profile == null) throw new InvalidOperationException(Aivana.Core.Localization.Translate("Das Zielprofil wurde nicht gefunden."));
                SelectProfile(profile.Id);
            }).Task;
        }, _corsair, () => _executionProfile.Value ?? ActiveProfile.Name, () => ForegroundProcess);
        _executor.InputInjectionChanged += injecting =>
        {
            if (_disposed) return;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                if (_disposed) return;
                if (injecting) { _inputInjectionDepth++; Register(); }
                else
                {
                    _inputInjectionDepth = Math.Max(0, _inputInjectionDepth - 1);
                    _ = RestoreAfterInjectionAsync();
                }
            });
        };
        _runner = new MacroRunner(_executor);
        InitializeExecutionFeedback();
        InitializeCorsairKeys();
        _runner.StatusChanged += (id, status) => System.Windows.Application.Current.Dispatcher.BeginInvoke(() =>
        {
            if (_disposed) return;
            _tileStatuses[id] = status;
            if (status == Aivana.Core.Localization.Translate("Makro läuft.")) _usage[id] = (GetRunCount(id) + 1, DateTime.Now);
            var tile = Settings.Profiles.SelectMany(p => p.Tiles).FirstOrDefault(t => t.Id == id);
            Log(tile?.Name ?? "Makro", status);
        });
        RefreshDevices();
        _timer.Tick += (_, _) => OnTimer();
    }
    public void Attach(IntPtr hwnd)
    {
        _source = HwndSource.FromHwnd(hwnd);
        _source?.AddHook(WindowHook);
        _hotkeys = new HotkeyService(hwnd);
        _hotkeys.Triggered += id =>
        {
            var tile = ActiveProfile.Tiles.FirstOrDefault(t => t.Id == id);
            if (tile != null) _ = ExecuteAsync(tile);
        };
        _hotkeys.CancelRequested += CancelAll;
        _hotkeys.PaletteRequested += RequestPalette;
        Register(); _timer.Start();
    }
    private IntPtr WindowHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (_hotkeys?.ProcessMessage(msg, wParam, lParam) == true) handled = true;
        if (msg is 0x219 or 0x00FE) RefreshDevices();
        return IntPtr.Zero;
    }
    private void Register()
    {
        RefreshCorsairKeyBindings();
        if (_hotkeys == null) return;
        if (Paused) { _hotkeys.Clear(); HotkeyErrors = []; }
        else HotkeyErrors = _hotkeys.Register(_inputInjectionDepth > 0 ? [] : ActiveProfile.Tiles,
            Settings.CancelHotkey, _inputInjectionDepth > 0 ? "" : Settings.PaletteHotkey);
    }
    private async Task RestoreAfterInjectionAsync()
    {
        await Task.Delay(100);
        if (!_disposed && _inputInjectionDepth == 0) { Register(); Changed?.Invoke(); }
    }
    public void Save()
    {
        var errors = ConfigurationValidator.Validate(Settings);
        foreach (var tile in Settings.Profiles.SelectMany(p => p.Tiles).Where(t => t.Enabled && !string.IsNullOrWhiteSpace(t.Hotkey)))
            if (!HotkeyParser.TryParse(tile.Hotkey, out _, out _, out var reason)) errors.Add($"{tile.Name}: {reason}");
        foreach (var profile in Settings.Profiles)
        {
            var hotkeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var tile in profile.Tiles.Where(t => t.Enabled && !string.IsNullOrWhiteSpace(t.Hotkey)))
            {
                var normalized = HotkeyParser.Normalize(tile.Hotkey);
                if (!hotkeys.Add(normalized)) errors.Add($"{profile.Name}: Hotkey {normalized} ist mehrfach belegt.");
                if (normalized.Equals(HotkeyParser.Normalize(Settings.CancelHotkey), StringComparison.OrdinalIgnoreCase)) errors.Add($"{tile.Name}: Dieser Hotkey wird zum Abbrechen verwendet.");
            }
        }
        if (!string.IsNullOrWhiteSpace(Settings.CancelHotkey) && !HotkeyParser.TryParse(Settings.CancelHotkey, out _, out _, out var cancelError)) errors.Add("Abbruch-Hotkey: " + cancelError);
        if (!string.IsNullOrWhiteSpace(Settings.PaletteHotkey) && !HotkeyParser.TryParse(Settings.PaletteHotkey, out _, out _, out var paletteError)) errors.Add("Befehlspalette: " + paletteError);
        if (errors.Count > 0) throw new InvalidOperationException(string.Join(Environment.NewLine, errors));
        _store.Save(Settings); Register(); Changed?.Invoke();
    }
    public async Task ExecuteAsync(Tile tile)
    {
        if (!tile.Enabled || _disposed) return;
        if (!_pendingExecutions.TryCapture(out var pendingExecution)) return;
        using (pendingExecution)
        {
            if (Paused || _disposed) return;
            var previousContext = _executionProfile.Value;
            _executionProfile.Value = Settings.Profiles.FirstOrDefault(p => p.Tiles.Any(t => t.Id == tile.Id))?.Name ?? ActiveProfile.Name;
            var snapshot = new Tile
            {
                Id = tile.Id, Name = tile.Name, Icon = tile.Icon, Color = tile.Color,
                Hotkey = tile.Hotkey, Enabled = tile.Enabled, Repeat = tile.Repeat, Favorite = tile.Favorite,
                Steps = tile.Steps.Select(s => new ActionStep { Kind = s.Kind, Value = s.Value, Arguments = s.Arguments, WorkingDirectory = s.WorkingDirectory, AudioDeviceId = s.AudioDeviceId, DelayMs = s.DelayMs }).ToList()
            };
            try
            {
                var needsTargetRestore = snapshot.Steps.Any(s => s.Kind is ActionKind.Text or ActionKind.Shortcut or ActionKind.Window);
                if (needsTargetRestore)
                {
                    GetWindowThreadProcessId(GetForegroundWindow(), out var pid);
                    needsTargetRestore = pid == Environment.ProcessId;
                }
                await DispatchPendingExecutionAsync(pendingExecution.Token, () => !Paused && !_disposed,
                    needsTargetRestore ? RestoreActionTargetAsync : static () => Task.CompletedTask,
                    token => _runner.RunAsync(snapshot, token));
            }
            catch (OperationCanceledException) { ForwardExecutionFeedback(new ExecutionProgress(tile.Id, ExecutionPhase.Cancelled)); }
            catch (Exception ex)
            {
                ForwardExecutionFeedback(new ExecutionProgress(tile.Id, ExecutionPhase.Failed));
                Log(tile.Name, Aivana.Core.Localization.Format("Fehler: {0}", ex.Message));
            }
            finally { _executionProfile.Value = previousContext; }
        }
    }
    public void CancelAll() { _pendingExecutions.CancelPending(); _runner.CancelAll(); Log(Aivana.Core.Localization.Translate("Ausführung"), Aivana.Core.Localization.Translate("Abbruch angefordert")); }
    public void Cancel(Guid id) { _runner.Cancel(id); }
    public void SetPaused(bool paused) { Paused = paused; if (paused) { _pendingExecutions.CancelPending(); _runner.CancelAll(); } Register(); Changed?.Invoke(); }
    public void SetProfileLocked(bool locked) { ProfileLocked = locked; Changed?.Invoke(); }
    public void RequestPalette()
    {
        RememberExternalWindow();
        PaletteRequested?.Invoke();
    }
    public async Task ExecuteFromPaletteAsync(Tile tile)
    {
        if (Paused) throw new InvalidOperationException(Aivana.Core.Localization.Translate("Aktiviere die Hotkeys, um Aktionen auszuführen."));
        await ExecuteAsync(tile);
    }
    private async Task RestoreActionTargetAsync()
    {
        if (_lastExternalWindow == IntPtr.Zero || !IsWindow(_lastExternalWindow))
            throw new InvalidOperationException(Aivana.Core.Localization.Translate("Kein Zielfenster verfügbar. Öffne die Palette aus dem gewünschten Programm."));
        if (_lastExternalWindow != IntPtr.Zero && IsWindow(_lastExternalWindow))
        {
            SetForegroundWindow(_lastExternalWindow);
            await Task.Delay(100);
            if (GetForegroundWindow() != _lastExternalWindow)
                throw new InvalidOperationException(Aivana.Core.Localization.Translate("Windows konnte das Zielfenster nicht aktivieren. Öffne die Palette direkt aus diesem Programm."));
        }
    }
    private void RememberExternalWindow()
    {
        var hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return;
        GetWindowThreadProcessId(hwnd, out var pid);
        if (pid != Environment.ProcessId)
        {
            _lastExternalWindow = hwnd;
            ForegroundProcess = DeviceService.GetForegroundProcessName();
            ForegroundWindowTitle = DeviceService.GetForegroundWindowTitle();
        }
    }
    public void SelectProfile(Guid id)
    {
        if (!Settings.Profiles.Any(p => p.Id == id)) return;
        Settings.ActiveProfileId = id; Save();
    }
    public void RefreshDevices()
    {
        var changed = false;
        try
        {
            var oldStatus = _corsair.Status;
            _corsair.Refresh();
            RefreshCorsairKeyBindings();
            var devices = _devices.GetDevices(_corsair.Keyboards);
            changed = !Devices.SequenceEqual(devices) || oldStatus != _corsair.Status;
            Devices = devices;
        }
        catch (Exception ex) { Log(Aivana.Core.Localization.Translate("Geräte"), Aivana.Core.Localization.Format("Fehler: {0}", ex.Message)); }
        if (changed) Changed?.Invoke();
    }
    private void OnTimer()
    {
        if (++_tick % 5 == 0) RefreshDevices();
        try
        {
            var process = DeviceService.GetForegroundProcessName();
            RememberExternalWindow();
            if (process.Equals(Process.GetCurrentProcess().ProcessName, StringComparison.OrdinalIgnoreCase)) return;
            ForegroundProcess = process;
            ForegroundWindowTitle = DeviceService.GetForegroundWindowTitle();
            if (!Settings.AutoProfiles || ProfileLocked || Paused || _inputInjectionDepth > 0) return;
            var next = ProfileSelector.Select(Settings, process, ForegroundWindowTitle);
            if (next.Id != Settings.ActiveProfileId) SelectProfile(next.Id);
        }
        catch (Exception ex) { Log(Aivana.Core.Localization.Translate("Profilwechsel"), Aivana.Core.Localization.Format("Fehler: {0}", ex.Message)); }
    }
    public async Task TestCorsairColorAsync(string color)
    {
        if (!CorsairAvailable) throw new InvalidOperationException(CorsairStatus);
        try
        {
            await Task.Run(() => _corsair.SetColor(color));
            Log(Aivana.Core.Localization.Translate("Corsair"), Aivana.Core.Localization.Translate("Beleuchtungsfarbe angewendet"));
        }
        catch (Exception ex) { Log(Aivana.Core.Localization.Translate("Corsair"), Aivana.Core.Localization.Format("Fehler: {0}", ex.Message)); throw; }
    }
    public void OpenIcue()
    {
        var paths = new[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) }
            .Select(root => Path.Combine(root, "Corsair", "Corsair iCUE5 Software", "iCUE.exe"));
        var path = paths.FirstOrDefault(File.Exists) ?? throw new InvalidOperationException(Aivana.Core.Localization.Translate("iCUE wurde nicht gefunden. Installiere es über die offizielle Corsair-Seite."));
        using var process = Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
    }
    public void Import()
    {
        var dialog = new OpenFileDialog { Filter = "Aivana Profile (*.json)|*.json", Title = Aivana.Core.Localization.Translate("Profile importieren") };
        if (dialog.ShowDialog() != true) return;
        var imported = _store.ReadImport(dialog.FileName);
        var tiles = imported.Profiles.Sum(p => p.Tiles.Count);
        if (System.Windows.MessageBox.Show(Aivana.Core.Localization.Translate((FormattableString)$"{imported.Profiles.Count} Profile und {tiles} Belegungen übernehmen?\nDie aktuelle Konfiguration wird ersetzt. Importierte Aktionen bleiben deaktiviert, bis du sie aktivierst."), Aivana.Core.Localization.Translate("Importvorschau"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
        foreach (var tile in imported.Profiles.SelectMany(p => p.Tiles)) tile.Enabled = false;
        imported.AutoStart = Settings.AutoStart;
        _store.Save(imported); _executionEpoch++; _pendingExecutions.CancelPending(); _runner.CancelAll(); Settings = imported; Register(); Changed?.Invoke();
    }
    public void Export()
    {
        var dialog = new SaveFileDialog { Filter = "Aivana Profile (*.json)|*.json", FileName = "aivana-profile.json", Title = Aivana.Core.Localization.Translate("Profile exportieren") };
        if (dialog.ShowDialog() == true) _store.Export(Settings, dialog.FileName);
    }
    public void SetAutoStart(bool enabled)
    {
        var executable = Environment.ProcessPath ?? throw new InvalidOperationException("Programmpfad unbekannt.");
        using var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
        if (enabled) key.SetValue("AivanaController", "\"" + executable + "\"");
        else key.DeleteValue("AivanaController", false);
        Settings.AutoStart = enabled; Save();
    }
    private void Log(string name, string status)
    {
        if (!System.Windows.Application.Current.Dispatcher.CheckAccess()) { System.Windows.Application.Current.Dispatcher.BeginInvoke(() => Log(name, status)); return; }
        _activity.Insert(0, new ActivityEntry(DateTime.Now, name, status));
        LastStatus = status;
        if (_activity.Count > 100) _activity.RemoveAt(_activity.Count - 1);
        Changed?.Invoke();
    }
    public void Dispose()
    {
        if (_disposed) return; _disposed = true;
        DisposeExecutionFeedback();
        _executionEpoch++;
        _pendingExecutions.Dispose();
        _timer.Stop(); _runner.CancelAll(); _hotkeys?.Dispose();
        _source?.RemoveHook(WindowHook); _executor.Dispose(); DisposeCorsairKeys(); _corsair.Dispose();
    }
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool SetForegroundWindow(IntPtr hwnd);
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(IntPtr hwnd);

    internal static async Task DispatchPendingExecutionAsync(CancellationToken cancellationToken, Func<bool> canExecute,
        Func<Task> restoreTarget, Func<CancellationToken, Task> execute)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await restoreTarget();
        cancellationToken.ThrowIfCancellationRequested();
        if (!canExecute()) return;
        await Task.Run(() => execute(cancellationToken), cancellationToken);
    }
}

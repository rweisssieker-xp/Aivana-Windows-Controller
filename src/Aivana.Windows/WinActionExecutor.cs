using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;
using Aivana.Core;
using Localization = Aivana.Core.Localization;

namespace Aivana.Windows;

public sealed class WinActionExecutor : IActionExecutor, IWorkflowConditionEvaluator, IActionVerificationProvider, IDisposable
{
    private readonly Aivana.Windows.KeyboardAdapters.VendorLighting _vendorLighting = new();
    public Func<string,string?>? VendorLibraryHash { get; set; }
    public Task<ActionVerificationResult> PreflightAsync(ActionStep step, CancellationToken token) => WindowsActionVerification.PreflightAsync(step, token);
    public Task<ActionVerificationResult> VerifyAsync(ActionStep step, CancellationToken token) => WindowsActionVerification.VerifyAsync(step, token);
    private readonly Func<string, Task> _switchProfile;
    private readonly CorsairService? _sharedCorsair;
    private readonly Func<string>? _activeProfile;
    private readonly Func<string>? _contextProcess;
    private CorsairService? _ownedCorsair;
    private static readonly Regex ClipboardToken = new(@"\{\{clipboard\}\}", RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public WinActionExecutor(Func<string, Task> switchProfile, CorsairService? corsairService = null, Func<string>? activeProfile = null, Func<string>? contextProcess = null)
    { _switchProfile = switchProfile ?? throw new ArgumentNullException(nameof(switchProfile)); _sharedCorsair = corsairService; _activeProfile = activeProfile; _contextProcess = contextProcess; }
    public event Action<bool>? InputInjectionChanged;

    public Task<bool> EvaluateAsync(WorkflowCondition condition, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (condition.Kind == WorkflowConditionKind.FileExists)
                return File.Exists(Environment.ExpandEnvironmentVariables(condition.Value));
            var processName = Path.GetFileName(condition.Value.Trim());
            if (processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) processName = processName[..^4];
            var processes = Process.GetProcesses();
            try
            {
                foreach (var process in processes)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    try
                    {
                        if (condition.Kind == WorkflowConditionKind.ProcessRunning && process.ProcessName.Equals(processName, StringComparison.OrdinalIgnoreCase)) return true;
                        if (condition.Kind == WorkflowConditionKind.WindowTitleContains && !string.IsNullOrWhiteSpace(process.MainWindowTitle) && process.MainWindowTitle.Contains(condition.Value, StringComparison.OrdinalIgnoreCase)) return true;
                    }
                    catch (InvalidOperationException) { }
                    catch (System.ComponentModel.Win32Exception) { }
                }
                return false;
            }
            finally { foreach (var process in processes) process.Dispose(); }
        }, cancellationToken);
    }

    public async Task ExecuteAsync(ActionStep step, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(step);
        cancellationToken.ThrowIfCancellationRequested();
        switch (step.Kind)
        {
            case ActionKind.VendorLighting:
                await _vendorLighting.SetAsync(step.Arguments, step.Value, VendorLibraryHash?.Invoke(step.Arguments), cancellationToken);
                break;
            case ActionKind.Plugin:
                _ = await Aivana.Windows.Plugins.PluginProcessClient.InvokeAsync(step.Plugin ?? throw new ArgumentException("Plugin fehlt."), cancellationToken);
                break;
            case ActionKind.UiAutomation:
                _ = await AutomationWorkerClient.RequestAsync(new("execute", step.UiAutomation), cancellationToken);
                break;
            case ActionKind.Launch:
                if (string.IsNullOrWhiteSpace(step.Value)) throw new ArgumentException(Localization.Translate("Der Programmpfad ist leer."));
                StartProcess(new ProcessStartInfo { FileName = step.Value, Arguments = step.Arguments ?? "", WorkingDirectory = step.WorkingDirectory ?? "", UseShellExecute = false });
                break;
            case ActionKind.OpenPath:
                if (string.IsNullOrWhiteSpace(step.Value)) throw new ArgumentException(Localization.Translate("Der Pfad ist leer."));
                StartProcess(new ProcessStartInfo { FileName = step.Value, UseShellExecute = true });
                break;
            case ActionKind.Url:
                if (!Uri.TryCreate(step.Value, UriKind.Absolute, out var uri) || uri.Scheme is not ("http" or "https")) throw new ArgumentException(Localization.Translate("Es können nur gültige HTTP- und HTTPS-Adressen geöffnet werden."));
                StartProcess(new ProcessStartInfo { FileName = uri.AbsoluteUri, UseShellExecute = true });
                break;
            case ActionKind.Text:
                var text = ExpandValue(step.Value ?? "", includeClipboard: true);
                await WithInputInjectionAsync(async () =>
                {
                    await WaitForPhysicalModifiersAsync(cancellationToken).ConfigureAwait(false);
                    await SendUnicodeTextAsync(text, cancellationToken).ConfigureAwait(false);
                }).ConfigureAwait(false);
                break;
            case ActionKind.Clipboard:
                var clipboardText = ExpandValue(step.Value ?? "", includeClipboard: true);
                SetClipboardText(clipboardText);
                break;
            case ActionKind.Shortcut:
                await WithInputInjectionAsync(() => SendShortcutAsync(step.Value ?? "", cancellationToken)).ConfigureAwait(false);
                break;
            case ActionKind.Media:
                await WithInputInjectionAsync(() => SendMediaAsync(step.Value ?? "", cancellationToken)).ConfigureAwait(false);
                break;
            case ActionKind.Brightness:
                if (!BrightnessCommand.TryParse(step.Value, out var brightness)) throw new ArgumentException(Localization.Translate("Helligkeit: Up, Down oder Set:0 bis Set:100 verwenden."));
                await Task.Run(() => DisplayBrightnessService.Apply(brightness, step.DisplayDeviceId, cancellationToken), cancellationToken).ConfigureAwait(false);
                break;
            case ActionKind.Volume:
                if (AudioEndpointControl.TryParseCommand(step.Value, out var audio))
                {
                    switch (audio.Kind)
                    {
                        case AudioCommandKind.SetOutputVolume: AudioEndpointControl.SetOutputVolume(audio.Percent!.Value, step.AudioDeviceId); break;
                        case AudioCommandKind.SetOutputMute: AudioEndpointControl.SetOutputMute(audio.Muted, step.AudioDeviceId); break;
                        case AudioCommandKind.ToggleOutputMute: AudioEndpointControl.SetOutputMute(null, step.AudioDeviceId); break;
                        case AudioCommandKind.SetMicrophoneMute: AudioEndpointControl.SetMicrophoneMute(audio.Muted, step.AudioDeviceId); break;
                        case AudioCommandKind.ToggleMicrophoneMute: AudioEndpointControl.SetMicrophoneMute(null, step.AudioDeviceId); break;
                    }
                    break;
                }
                if (!string.IsNullOrEmpty(step.AudioDeviceId))
                {
                    switch (step.Value.ToLowerInvariant())
                    {
                        case "up": case "higher": case "höher": case "lauter": case "volumeup":
                            AudioEndpointControl.AdjustOutputVolume(2, step.AudioDeviceId); break;
                        case "down": case "lower": case "niedriger": case "leiser": case "volumedown":
                            AudioEndpointControl.AdjustOutputVolume(-2, step.AudioDeviceId); break;
                        case "mute": case "stumm": case "toggle": case "volumemute":
                            AudioEndpointControl.SetOutputMute(null, step.AudioDeviceId); break;
                        default: throw new ArgumentException(Localization.Translate("Unbekannte Lautstärkeaktion."));
                    }
                    break;
                }
                await WithInputInjectionAsync(() => SendMediaAsync(step.Value.ToLowerInvariant() switch { "up" or "higher" or "höher" or "lauter" => "volumeup", "down" or "lower" or "niedriger" or "leiser" => "volumedown", "mute" or "stumm" or "toggle" => "volumemute", _ => step.Value }, cancellationToken)).ConfigureAwait(false);
                break;
            case ActionKind.Obs:
                await ObsActionClient.ExecuteAsync(step, cancellationToken).ConfigureAwait(false);
                break;
            case ActionKind.SwitchProfile:
                await _switchProfile(step.Value ?? "").WaitAsync(cancellationToken).ConfigureAwait(false);
                break;
            case ActionKind.WaitWindow:
                await WindowReadyService.WaitAsync(step, cancellationToken).ConfigureAwait(false);
                break;
            case ActionKind.Delay:
                await Task.Delay(Math.Clamp(step.DelayMs, 0, 600_000), cancellationToken).ConfigureAwait(false);
                break;
            case ActionKind.CorsairColor:
                (_sharedCorsair ?? (_ownedCorsair ??= new CorsairService())).SetColor(step.Value);
                break;
            case ActionKind.Window:
                ExecuteWindowAction(step.Value ?? "");
                break;
            case ActionKind.System:
                ExecuteSystemAction(step.Value ?? "");
                break;
            default: throw new NotSupportedException(Localization.Translate((FormattableString)$"Nicht unterstützte Aktionsart: {step.Kind}."));
        }
    }

    private string ExpandValue(string value, bool includeClipboard)
    {
        // Resolve the foreground context at the point the action executes. The palette
        // controller restores the previously focused external window before dispatch.
        var process = DeviceService.GetForegroundProcessName();
        GetWindowThreadProcessId(GetForegroundWindow(), out var foregroundPid);
        if (foregroundPid == Environment.ProcessId) process = _contextProcess?.Invoke() ?? process;
        return includeClipboard
            ? ExpandTemplate(value, DateTime.Now, _activeProfile?.Invoke() ?? "", process, GetClipboardText)
            : TemplateExpander.Expand(value, DateTime.Now, "", _activeProfile?.Invoke() ?? "", process);
    }

    private static void ExecuteWindowAction(string action)
    {
        var operation = NormalizeWindowAction(action);
        // Capture once, immediately before operating, so we cannot accidentally target
        // a stale handle after another window gained focus.
        var hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero || !IsWindow(hwnd))
            throw new InvalidOperationException(Localization.Translate("Es ist kein gültiges Vordergrundfenster vorhanden."));
        GetWindowThreadProcessId(hwnd, out var processId);
        if (processId == 0 || processId == (uint)Environment.ProcessId)
            throw new InvalidOperationException(Localization.Translate("Das eigene Fenster kann mit dieser Aktion nicht verändert werden. Bitte zuerst das Zielfenster fokussieren."));

        switch (operation)
        {
            case WindowOperation.Minimize: ShowWindowAsyncChecked(hwnd, 6, Localization.Translate("minimieren")); break; // SW_MINIMIZE
            case WindowOperation.Maximize: ShowWindowAsyncChecked(hwnd, 3, Localization.Translate("maximieren")); break; // SW_MAXIMIZE
            case WindowOperation.Restore: ShowWindowAsyncChecked(hwnd, 9, Localization.Translate("wiederherstellen")); break; // SW_RESTORE
            case WindowOperation.Left:
            case WindowOperation.Right:
                ShowWindowAsyncChecked(hwnd, 9, Localization.Translate("wiederherstellen")); // SW_RESTORE before positioning a maximized/minimized window.
                var monitor = MonitorFromWindow(hwnd, 2); // MONITOR_DEFAULTTONEAREST
                var info = new MonitorInfo { Size = Marshal.SizeOf<MonitorInfo>() };
                if (monitor == IntPtr.Zero || !GetMonitorInfo(monitor, ref info))
                    throw new InvalidOperationException(Localization.Translate("Der Arbeitsbereich des aktiven Monitors konnte nicht ermittelt werden."));
                var bounds = CalculateWindowHalf(info.WorkArea, operation == WindowOperation.Right);
                if (!SetWindowPos(hwnd, IntPtr.Zero, bounds.Left, bounds.Top, bounds.Width, bounds.Height, 0x4014))
                    throw new InvalidOperationException(Localization.Translate((FormattableString)$"Das Fenster konnte nicht positioniert werden (Windows-Fehler {Marshal.GetLastWin32Error()}).")); // SWP_NOZORDER | SWP_NOACTIVATE | SWP_ASYNCWINDOWPOS
                break;
        }
    }

    private static void ShowWindowAsyncChecked(IntPtr hwnd, int command, string action)
    {
        if (!ShowWindowAsync(hwnd, command))
            throw new InvalidOperationException(Localization.Translate((FormattableString)$"Das Fenster konnte nicht {action} werden."));
    }

    internal enum WindowOperation { Minimize, Maximize, Restore, Left, Right }
    internal readonly record struct WindowBounds(int Left, int Top, int Width, int Height);
    [StructLayout(LayoutKind.Sequential)] internal struct NativeRect { public int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)] private struct MonitorInfo
    {
        public int Size;
        public NativeRect MonitorArea;
        public NativeRect WorkArea;
        public uint Flags;
    }

    internal static WindowOperation NormalizeWindowAction(string action) => action.Trim().ToLowerInvariant() switch
    {
        "minimize" or "minimise" or "minimieren" => WindowOperation.Minimize,
        "maximize" or "maximise" or "maximieren" => WindowOperation.Maximize,
        "restore" or "wiederherstellen" => WindowOperation.Restore,
        "left" or "links" => WindowOperation.Left,
        "right" or "rechts" => WindowOperation.Right,
        _ => throw new ArgumentException(Localization.Translate((FormattableString)$"Nicht unterstützte Fensteraktion: {Bound(action)}."), nameof(action))
    };

    internal static WindowBounds CalculateWindowHalf(NativeRect workArea, bool right)
    {
        var width = workArea.Right - workArea.Left;
        var height = workArea.Bottom - workArea.Top;
        if (width <= 0 || height <= 0) throw new ArgumentException(Localization.Translate("Der Monitor-Arbeitsbereich ist ungültig."), nameof(workArea));
        var half = width / 2;
        return right
            ? new WindowBounds(workArea.Left + half, workArea.Top, width - half, height)
            : new WindowBounds(workArea.Left, workArea.Top, half, height);
    }

    private static void ExecuteSystemAction(string action)
    {
        if (action.Trim().Equals("lock", StringComparison.OrdinalIgnoreCase) || action.Trim().Equals("sperren", StringComparison.OrdinalIgnoreCase))
        {
            if (!LockWorkStation()) throw new InvalidOperationException(Localization.Translate((FormattableString)$"Windows konnte die Sitzung nicht sperren (Fehler {Marshal.GetLastWin32Error()})."));
            return;
        }
        throw new ArgumentException(Localization.Translate((FormattableString)$"Nicht unterstützte Systemaktion: {Bound(action)}."), nameof(action));
    }

    internal static string ExpandTemplate(string value, DateTime now, string profile, string process, Func<string>? readClipboard = null)
    {
        ArgumentNullException.ThrowIfNull(value);
        var clipboard = ClipboardToken.IsMatch(value) ? (readClipboard ?? throw new InvalidOperationException(Localization.Translate("Ein Clipboard-Platzhalter benötigt einen Clipboard-Lesezugriff.")))() : "";
        return TemplateExpander.Expand(value, now, clipboard, profile, process);
    }

    private static string GetClipboardText()
    {
        try { return InvokeOnApplicationStaDispatcher(Clipboard.GetText); }
        catch (Exception ex) when (ex is InvalidOperationException or ExternalException or OperationCanceledException or TimeoutException)
        {
            throw new InvalidOperationException(Localization.Translate((FormattableString)$"Der Windows-Clipboard konnte nicht gelesen werden: {Bound(ex.Message)}"), ex);
        }
    }

    private static void SetClipboardText(string text)
    {
        try { InvokeOnApplicationStaDispatcher(() => { Clipboard.SetText(text); return true; }); }
        catch (Exception ex) when (ex is InvalidOperationException or ExternalException or OperationCanceledException or TimeoutException)
        {
            throw new InvalidOperationException(Localization.Translate((FormattableString)$"Der Windows-Clipboard konnte nicht beschrieben werden: {Bound(ex.Message)}"), ex);
        }
    }

    private static T InvokeOnApplicationStaDispatcher<T>(Func<T> operation)
    {
        var dispatcher = Application.Current?.Dispatcher;
        if (dispatcher is null || dispatcher.HasShutdownStarted || dispatcher.HasShutdownFinished || dispatcher.Thread.GetApartmentState() != ApartmentState.STA)
            throw new InvalidOperationException(Localization.Translate("Der STA-Dispatcher der Anwendung ist nicht verfügbar."));
        if (dispatcher.CheckAccess()) return operation();
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        return dispatcher.Invoke(operation, DispatcherPriority.Send, timeout.Token, TimeSpan.FromSeconds(2));
    }

    private static string Bound(string? message)
    {
        var compact = string.IsNullOrWhiteSpace(message) ? Localization.Translate("Unbekannter Fehler") : message.Replace('\r', ' ').Replace('\n', ' ').Trim();
        return compact.Length <= 240 ? compact : compact[..240];
    }

    private async Task WithInputInjectionAsync(Func<Task> action)
    {
        var notified = false;
        try { notified = true; InputInjectionChanged?.Invoke(true); await action().ConfigureAwait(false); }
        finally { if (notified) InputInjectionChanged?.Invoke(false); }
    }

    private static async Task SendUnicodeTextAsync(string text, CancellationToken token)
    {
        foreach (var ch in text)
        {
            token.ThrowIfCancellationRequested();
            try { Send([Input.Unicode(ch, false)]); }
            finally { Send([Input.Unicode(ch, true)]); }
            await Task.Yield();
        }
    }

    private static async Task SendShortcutAsync(string shortcut, CancellationToken token)
    {
        if (!HotkeyParser.TryParseShortcut(shortcut, out var modifiers, out var baseKey, out var error)) throw new ArgumentException(error, nameof(shortcut));
        await WaitForPhysicalModifiersAsync(token).ConfigureAwait(false);
        var vk = new List<ushort>(4);
        if ((modifiers & HotkeyParser.ModControl) != 0) vk.Add(0xA2);
        if ((modifiers & HotkeyParser.ModAlt) != 0) vk.Add(0xA4);
        if ((modifiers & HotkeyParser.ModShift) != 0) vk.Add(0xA0);
        if ((modifiers & HotkeyParser.ModWin) != 0) vk.Add(0x5B);
        vk.Add(checked((ushort)baseKey));
        var pressed = new List<ushort>();
        try
        {
            foreach (var code in vk) { token.ThrowIfCancellationRequested(); pressed.Add(code); Send([Input.Key(code, false)]); }
            await Task.Delay(25, token).ConfigureAwait(false);
        }
        finally { ReleaseKeys(pressed); }
    }

    private static async Task SendMediaAsync(string action, CancellationToken token)
    {
        var vk = action.Trim().ToLowerInvariant() switch
        {
            "playpause" or "play/pause" or "toggle" or "wiedergabe/pause" => 0xB3, "next" or "nexttrack" or "nächster titel" => 0xB0, "previous" or "prev" or "previoustrack" or "vorheriger titel" => 0xB1,
            "stop" or "stopp" => 0xB2, "volumeup" or "lauter" => 0xAF, "volumedown" or "leiser" => 0xAE, "volumemute" or "mute" or "stumm" => 0xAD,
            _ => throw new ArgumentException(Localization.Translate((FormattableString)$"Nicht unterstützte Medien- oder Lautstärkeaktion: {action}."))
        };
        await WaitForPhysicalModifiersAsync(token).ConfigureAwait(false);
        token.ThrowIfCancellationRequested();
        try { Send([Input.Key((ushort)vk, false)]); }
        finally { Send([Input.Key((ushort)vk, true)]); }
    }

    private static async Task WaitForPhysicalModifiersAsync(CancellationToken token)
    {
        // Do not inject a macro while the user still holds a modifier from the triggering chord.
        int[] modifiers = [0x10, 0x11, 0x12, 0x5B, 0x5C, 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5];
        await WaitForModifiersToReleaseAsync(() => modifiers.Any(vk => (GetAsyncKeyState(vk) & 0x8000) != 0), token, TimeSpan.FromSeconds(3)).ConfigureAwait(false);
    }

    internal static async Task WaitForModifiersToReleaseAsync(Func<bool> isModifierDown, CancellationToken token, TimeSpan timeout)
    {
        ArgumentNullException.ThrowIfNull(isModifierDown);
        if (timeout <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(timeout));
        token.ThrowIfCancellationRequested();
        var timer = Stopwatch.StartNew();
        while (isModifierDown())
        {
            token.ThrowIfCancellationRequested();
            if (timer.Elapsed >= timeout)
                throw new TimeoutException(Localization.Translate((FormattableString)$"Eine Modifikatortaste wurde länger als {Math.Ceiling(timeout.TotalSeconds)} Sekunden gehalten. Lass Strg, Alt, Umschalt oder Windows los und versuch es erneut."));
            await Task.Delay(TimeSpan.FromMilliseconds(Math.Min(10, Math.Max(1, (timeout - timer.Elapsed).TotalMilliseconds))), token).ConfigureAwait(false);
        }
    }

    private static void ReleaseKeys(List<ushort> keys)
    {
        for (var i = keys.Count - 1; i >= 0; i--) { try { Send([Input.Key(keys[i], true)]); } catch { } }
    }

    private static void Send(Input[] inputs)
    {
        var sent = SendInput((uint)inputs.Length, inputs, Marshal.SizeOf<Input>());
        if (sent != inputs.Length) throw new InvalidOperationException(Localization.Translate((FormattableString)$"Windows hat {sent} von {inputs.Length} simulierten Tastendrücken angenommen (Fehler {Marshal.GetLastWin32Error()})."));
    }

    private static void StartProcess(ProcessStartInfo startInfo)
    {
        try { using var process = Process.Start(startInfo); }
        catch (System.ComponentModel.Win32Exception ex) { throw new InvalidOperationException(Localization.Translate((FormattableString)$"Das Programm oder der Pfad konnte nicht geöffnet werden: {startInfo.FileName}"), ex); }
        catch (FileNotFoundException ex) { throw new InvalidOperationException(Localization.Translate((FormattableString)$"Die Datei wurde nicht gefunden: {startInfo.FileName}"), ex); }
        catch (DirectoryNotFoundException ex) { throw new InvalidOperationException(Localization.Translate((FormattableString)$"Der Ordner wurde nicht gefunden: {startInfo.FileName}"), ex); }
    }

    public void Dispose() { _vendorLighting.Dispose(); _ownedCorsair?.Dispose(); _ownedCorsair = null; }

    internal static int InputSize => Marshal.SizeOf<Input>();
    internal static int KeyboardInputSize => Marshal.SizeOf<KeyboardInput>();
    [StructLayout(LayoutKind.Explicit, Size = 40)] private struct Input
    {
        [FieldOffset(0)] public uint Type;
        [FieldOffset(8)] public KeyboardInput Keyboard;
        public static Input Key(ushort vk, bool up) => new() { Type = 1, Keyboard = new KeyboardInput { VirtualKey = vk, Flags = (uint)((IsExtended(vk) ? 0x0001 : 0) | (up ? 0x0002 : 0)) } };
        public static Input Unicode(char ch, bool up) => new() { Type = 1, Keyboard = new KeyboardInput { Scan = ch, Flags = (uint)(0x0004 | (up ? 0x0002 : 0)) } };
        private static bool IsExtended(ushort vk) => vk is 0x21 or 0x22 or 0x23 or 0x24 or 0x25 or 0x26 or 0x27 or 0x28 or 0x2D or 0x2E or 0x5B or 0x5C or 0x6F or 0xA3 or 0xA5 or >= 0xAD and <= 0xB7;
    }
    [StructLayout(LayoutKind.Sequential)] private struct KeyboardInput { public ushort VirtualKey, Scan; public uint Flags, Time; public UIntPtr ExtraInfo; }
    [DllImport("user32.dll", SetLastError = true)] private static extern uint SendInput(uint count, [In] Input[] inputs, int size);
    [DllImport("user32.dll")] private static extern short GetAsyncKeyState(int virtualKey);
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool IsWindow(IntPtr hwnd);
    [DllImport("user32.dll", SetLastError = true)] private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool ShowWindowAsync(IntPtr hwnd, int command);
    [DllImport("user32.dll")] private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint flags);
    [DllImport("user32.dll", EntryPoint = "GetMonitorInfoW", CharSet = CharSet.Unicode, SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool GetMonitorInfo(IntPtr monitor, ref MonitorInfo info);
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool SetWindowPos(IntPtr hwnd, IntPtr insertAfter, int x, int y, int width, int height, uint flags);
    [DllImport("user32.dll", SetLastError = true)] [return: MarshalAs(UnmanagedType.Bool)] private static extern bool LockWorkStation();

}

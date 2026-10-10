using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Aivana.Core;
using Aivana.Windows;

internal static class Program
{
    private static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RawInputDeviceList { public IntPtr Device; public uint Type; }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetRawInputDeviceList(IntPtr list, ref uint count, uint size);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PostMessage(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")] private static extern bool ShowWindowAsync(IntPtr window, int command);
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr window);

    [STAThread]
    private static void Main(string[] args)
    {
        try { Run(args); }
        catch (Exception ex) { Console.Error.WriteLine(ex); Environment.ExitCode = 1; }
    }

    private static void Run(string[] args)
    {
        if (args is ["--window-wait-fixture", var title])
        {
            using var fixture = new HwndSource(new HwndSourceParameters(title)
            {
                Width = 320, Height = 120, WindowStyle = unchecked((int)0x10CF0000)
            });
            ShowWindowAsync(fixture.Handle, 5);
            SetForegroundWindow(fixture.Handle);
            fixture.RootVisual = new System.Windows.Controls.TextBlock { Text = "Aivana window wait test", FontSize = 18 };
            System.Windows.Threading.Dispatcher.Run();
            return;
        }
        using (var executor = new WinActionExecutor(_ => Task.CompletedTask))
        {
            var timeout = false;
            try { executor.ExecuteAsync(new ActionStep { Kind = ActionKind.WaitWindow, Value = "AivanaMissingWindow-" + Guid.NewGuid().ToString("N"), DelayMs = 50 }, CancellationToken.None).GetAwaiter().GetResult(); }
            catch (TimeoutException) { timeout = true; }
            Check(timeout, "Native window search must time out when no matching process exists.");
            using var cancellation = new CancellationTokenSource(); cancellation.Cancel();
            var cancelled = false;
            try { executor.ExecuteAsync(new ActionStep { Kind = ActionKind.WaitWindow, Value = "notepad", DelayMs = 1000 }, cancellation.Token).GetAwaiter().GetResult(); }
            catch (OperationCanceledException) { cancelled = true; }
            Check(cancelled, "Native window wait must honor cancellation before activating anything.");
        }
        var previousWindow = GetForegroundWindow();
        using (var fixture = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = Environment.ProcessPath!, Arguments = "--window-wait-fixture AivanaWait-" + Guid.NewGuid().ToString("N"),
            UseShellExecute = false, CreateNoWindow = true, WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
        }) ?? throw new InvalidOperationException("Test window process failed to start."))
        {
            try
            {
                // The title filter isolates our own fixture from every live user window.
                var fixtureTitle = fixture.StartInfo.Arguments.Split(' ')[1];
                using var executor = new WinActionExecutor(_ => Task.CompletedTask);
                executor.ExecuteAsync(new ActionStep { Kind = ActionKind.WaitWindow, Value = fixture.ProcessName, Arguments = fixtureTitle, DelayMs = 10000 }, CancellationToken.None).GetAwaiter().GetResult();
                fixture.Refresh();
                Check(!fixture.HasExited && GetForegroundWindow() == fixture.MainWindowHandle && fixture.MainWindowTitle == fixtureTitle, "Window wait should find and focus the separate test window.");
            }
            finally
            {
                if (!fixture.HasExited) { fixture.Kill(); fixture.WaitForExit(5000); }
                if (previousWindow != IntPtr.Zero) SetForegroundWindow(previousWindow);
            }
        }
        Console.WriteLine("Window wait native success, timeout and cancellation checks passed using an isolated test window.");
        AudioEndpointChecks.Run();
        ObsProtocolChecks.Run();
        var leftHalf = WinActionExecutor.CalculateWindowHalf(new WinActionExecutor.NativeRect { Left = -1920, Top = 40, Right = 0, Bottom = 1040 }, right: false);
        var rightHalf = WinActionExecutor.CalculateWindowHalf(new WinActionExecutor.NativeRect { Left = -1920, Top = 40, Right = 0, Bottom = 1040 }, right: true);
        Check(leftHalf == new WinActionExecutor.WindowBounds(-1920, 40, 960, 1000), "Left window placement should use the monitor work area, including negative coordinates.");
        Check(rightHalf == new WinActionExecutor.WindowBounds(-960, 40, 960, 1000), "Right window placement should use the monitor work area.");
        Check(WinActionExecutor.NormalizeWindowAction("minimieren") == WinActionExecutor.WindowOperation.Minimize, "German minimize action should normalize.");
        Check(WinActionExecutor.NormalizeWindowAction("maximize") == WinActionExecutor.WindowOperation.Maximize, "English maximize action should normalize.");
        Check(WinActionExecutor.NormalizeWindowAction("rechts") == WinActionExecutor.WindowOperation.Right, "German right action should normalize.");
        var invalidWorkAreaRejected = false;
        try { _ = WinActionExecutor.CalculateWindowHalf(new WinActionExecutor.NativeRect { Left = 0, Top = 0, Right = 0, Bottom = 10 }, right: false); }
        catch (ArgumentException) { invalidWorkAreaRejected = true; }
        Check(invalidWorkAreaRejected, "Invalid monitor work areas should be rejected by the pure placement helper.");
        var clipboardReads = 0;
        var expanded = WinActionExecutor.ExpandTemplate("{{date}} {{time}} {{datetime}} {{clipboard}} {{profile}} {{process}}", new DateTime(2026, 10, 8, 9, 7, 6), "Design", "notepad", () => { clipboardReads++; return "clipboard value"; });
        Check(expanded == "2026-10-08 09:07 2026-10-08 09:07:06 clipboard value Design notepad", "Template values should be expanded from the supplied context.");
        Check(clipboardReads == 1, "Clipboard should be read once when its token is present.");
        var withoutClipboard = WinActionExecutor.ExpandTemplate("{{profile}}", new DateTime(2026, 10, 8), "Design", "notepad", () => { clipboardReads++; return "unused"; });
        Check(withoutClipboard == "Design" && clipboardReads == 1, "Clipboard should not be read when its token is absent.");

        Check(HotkeyParser.TryParse("Control+Alt+Escape", out var modifiers, out var key, out _), "Ctrl/Alt/Escape should parse.");
        Check(modifiers == (HotkeyParser.ModControl | HotkeyParser.ModAlt) && key == 0x1B, "Parsed virtual key or modifiers are incorrect.");
        Check(!HotkeyParser.TryParse("Ctrl+Control+A", out _, out _, out _), "Equivalent modifier aliases must be recognized as duplicates.");
        Check(HotkeyParser.TryParse("Strg+Umschalt+Escape", out var germanModifiers, out var germanKey, out _), "German modifier aliases should parse.");
        Check(germanModifiers == (HotkeyParser.ModControl | HotkeyParser.ModShift) && germanKey == 0x1B, "German aliases should map to the expected modifiers.");
        Check(HotkeyParser.Normalize("Strg+Umschalt+Escape") == "Ctrl+Shift+Escape", "Aliases should normalize to stable canonical names.");
        Check(!HotkeyParser.TryParse("Ctrl+Shift", out _, out _, out _), "A modifier must not be accepted as the base key.");
        Check(!HotkeyParser.TryParse("Umschalt+Shift+A", out _, out _, out _), "Equivalent German and English modifiers must be recognized as duplicates.");
        Check(!HotkeyParser.TryParse("Ctrl++", out _, out _, out _), "An empty chord component must be rejected.");
        Check(HotkeyParser.ValidateShortcut("Strg+Umschalt+A").Count == 0, "The validation helper should accept valid shortcuts.");
        Check(HotkeyParser.ValidateShortcut("Ctrl+Shift").Count == 1, "The validation helper should return a clear error for invalid shortcuts.");
        foreach (var bareShortcut in new[] { "Enter", "F5", "A" })
        {
            Check(HotkeyParser.TryParseShortcut(bareShortcut, out var bareModifiers, out var bareKey, out var shortcutError), $"Bare shortcut '{bareShortcut}' should parse: {shortcutError}");
            Check(bareModifiers == 0 && bareKey != 0, $"Bare shortcut '{bareShortcut}' should map to a key without modifiers.");
            Check(HotkeyParser.ValidateShortcut(bareShortcut).Count == 0, $"Bare shortcut '{bareShortcut}' should validate.");
        }
        Check(!HotkeyParser.TryParse("Enter", out _, out _, out _), "Global hotkeys should still require a modifier.");
        Check(!HotkeyParser.TryParseShortcut("Ctrl+Alt", out _, out _, out _), "Shortcut actions must reject modifier-only chords.");
        Check(!HotkeyParser.TryParse("Ctrl+NotAKey", out _, out _, out _), "Unknown keys must fail parsing.");
        foreach (var hotkey in new[] { "Ctrl+F24", "Alt+Num1", "Ctrl+NumPad1", "Ctrl+D1", "Ctrl+PrintScreen", "Ctrl+Pause", "Ctrl+OemPlus", "Ctrl+OemQuestion", "Ctrl+MediaPlayPause", "Ctrl+BrowserBack", "Win+PageDown" })
        {
            Check(HotkeyParser.TryParse(hotkey, out _, out _, out var parseError), $"{hotkey} should parse: {parseError}");
            var normalized = HotkeyParser.Normalize(hotkey);
            Check(HotkeyParser.TryParse(normalized, out _, out _, out parseError), $"Normalized hotkey '{normalized}' should parse: {parseError}");
        }
        foreach (var lockKey in new[] { "Ctrl+CapsLock", "Alt+NumLock", "Shift+ScrollLock" })
            Check(HotkeyParser.TryParse(lockKey, out _, out _, out var parseError), $"Lock key '{lockKey}' should remain supported: {parseError}");
        Check(HotkeyParser.TryParse("Ctrl+Plus", out _, out var plusKey, out _) && HotkeyParser.TryParse("Ctrl+OemPlus", out _, out var oemPlusKey, out _) && plusKey == oemPlusKey,
            "WPF/OEM aliases should resolve to one virtual key.");
        Check(new KeyboardLayoutService().TryGetMapping(0x15, extended: false, out var layoutMapping), "The active Windows layout should map the standard top-row scan code.");
        Check(layoutMapping.Legend.Length > 0 && layoutMapping.HotkeyToken.Length > 0, "A layout mapping should contain a display legend and parser token.");
        Check(HotkeyParser.TryParseShortcut(layoutMapping.HotkeyToken, out _, out var layoutKey, out var layoutError) && layoutKey == layoutMapping.VirtualKey,
            $"The layout token should round-trip through the shortcut parser: {layoutError}");
        Console.WriteLine($"Layout {layoutMapping.LayoutId}: scan 15 -> {layoutMapping.Legend}/{layoutMapping.ShiftedLegend} (token {layoutMapping.HotkeyToken})");
        var keyboardLayout = new KeyboardLayoutService();
        Check(keyboardLayout.TryGetMapping(0x47, extended: false, out var numPad7) && numPad7.VirtualKey == 0x67 && numPad7.HotkeyToken == "NumPad7",
            "Unextended numpad 7 must remain distinct from Home.");
        Check(keyboardLayout.TryGetMapping(0x47, extended: true, out var home) && home.VirtualKey == 0x24 && home.HotkeyToken == "Home",
            "E0 47 must map to Home, not numpad 7.");
        Check(keyboardLayout.TryGetMapping(0x1C, extended: false, out var enter), "Main Enter should have a readable layout mapping.");
        Check(keyboardLayout.TryGetMapping(0x1C, extended: true, out var numPadEnter), "E0 numpad Enter should have a readable layout mapping.");
        Check(enter.VirtualKey == numPadEnter.VirtualKey && enter.HotkeyToken == numPadEnter.HotkeyToken && enter.SharesGlobalHotkeyToken && numPadEnter.SharesGlobalHotkeyToken,
            "Main Enter and numpad Enter should advertise their shared RegisterHotKey identity.");
        Check(enter.Legend == "Enter" && numPadEnter.Legend == "Enter", "Control-character output should use friendly key legends.");
        Check(keyboardLayout.TryGetMapping(0x0F, extended: false, out var tab) && tab.Legend == "Tab", "Tab should use a friendly legend instead of a control character.");
        Check(keyboardLayout.TryGetMapping(0x37, extended: true, out var printScreen) && printScreen.VirtualKey == 0x2C && printScreen.HotkeyToken == "PrintScreen",
            "E0 37 should map to PrintScreen.");

        WinActionExecutor.WaitForModifiersToReleaseAsync(() => false, CancellationToken.None, TimeSpan.FromMilliseconds(40)).GetAwaiter().GetResult();
        var timeoutObserved = false;
        try { WinActionExecutor.WaitForModifiersToReleaseAsync(() => true, CancellationToken.None, TimeSpan.FromMilliseconds(40)).GetAwaiter().GetResult(); }
        catch (TimeoutException ex) { timeoutObserved = ex.Message.Contains("los", StringComparison.OrdinalIgnoreCase); }
        Check(timeoutObserved, "A held modifier should time out with actionable guidance.");

        var windowsAssembly = typeof(WinActionExecutor).Assembly;
        var inputSize = (int)typeof(WinActionExecutor).GetProperty("InputSize", BindingFlags.Static | BindingFlags.NonPublic)!.GetValue(null)!;
        var keyboardInputSize = (int)typeof(WinActionExecutor).GetProperty("KeyboardInputSize", BindingFlags.Static | BindingFlags.NonPublic)!.GetValue(null)!;
        if (Environment.Is64BitProcess)
        {
            Check(inputSize == 40, $"x64 INPUT should be 40 bytes, got {inputSize}.");
            Check(keyboardInputSize == 24, $"x64 KEYBDINPUT should be 24 bytes, got {keyboardInputSize}.");
            var rawInfo = typeof(DeviceService).GetNestedType("RawInputDeviceInfo", BindingFlags.NonPublic)!;
            var rawList = typeof(DeviceService).GetNestedType("RawInputDeviceList", BindingFlags.NonPublic)!;
            Check(Marshal.SizeOf(rawInfo) == 32, "x64 RID_DEVICE_INFO layout should be 32 bytes.");
            Check(Marshal.SizeOf(rawList) == 16, "x64 RAWINPUTDEVICELIST layout should be 16 bytes.");
        }
        Check(Marshal.SizeOf(windowsAssembly.GetType("Aivana.Windows.CorsairService")!.GetNestedType("LedPosition", BindingFlags.NonPublic)!) == 24, "CorsairLedPosition layout should be 24 bytes.");
        Check(Marshal.SizeOf(windowsAssembly.GetType("Aivana.Windows.CorsairService")!.GetNestedType("LedColor", BindingFlags.NonPublic)!) == 8, "CorsairLedColor layout should be 8 bytes.");
        Check(Marshal.SizeOf(windowsAssembly.GetType("Aivana.Windows.CorsairService")!.GetNestedType("DeviceInfoNative", BindingFlags.NonPublic)!) == 396, "CorsairDeviceInfo layout should be 396 bytes.");
        IReadOnlyList<CorsairKeyboardInfo> corsairKeyboards;
        using (var corsair = new CorsairService())
        {
            Check(!string.IsNullOrWhiteSpace(corsair.Status), "Optional iCUE adapter should always provide an availability status.");
            Check(corsair.SdkPath is null || File.Exists(corsair.SdkPath), "Reported SDK path should point to an existing DLL.");
            for (var attempt = 0; attempt < 12 && !corsair.IsAvailable; attempt++)
            {
                Task.Delay(250).GetAwaiter().GetResult();
                corsair.Refresh();
            }
            corsairKeyboards = corsair.Keyboards;
            CorsairSdkKeyChecks.Run(corsair);
            Check(corsairKeyboards.All(k => !string.IsNullOrWhiteSpace(k.Name) && !string.IsNullOrWhiteSpace(k.DeviceId) && k.LedCount >= 0), "Every SDK keyboard should include a usable name, device id, and LED count.");
            Console.WriteLine($"iCUE SDK: {(corsair.SdkPath is null ? "not found" : "found")}; {corsair.Status}; SDK keyboards: {corsairKeyboards.Count}");
        }

        var devices = new DeviceService().GetDevices(corsairKeyboards);
        Check(devices.Select(d => d.Path).Distinct(StringComparer.OrdinalIgnoreCase).Count() == devices.Count, "Device inventory should not duplicate raw-input paths.");
        Check(devices.All(d => d.Capabilities.Contains("Tastatur", StringComparison.Ordinal)), "Inventory must exclude non-keyboard devices.");
        uint rawCount = 0;
        var rawSize = (uint)Marshal.SizeOf<RawInputDeviceList>();
        Check(GetRawInputDeviceList(IntPtr.Zero, ref rawCount, rawSize) != uint.MaxValue, "Raw Input device count should be available.");
        var rawMemory = Marshal.AllocHGlobal(checked((int)(rawCount * rawSize)));
        try
        {
            Check(GetRawInputDeviceList(rawMemory, ref rawCount, rawSize) != uint.MaxValue, "Raw Input list should be available.");
            var keyboardCount = 0;
            for (var i = 0; i < rawCount; i++)
                if (Marshal.PtrToStructure<RawInputDeviceList>(IntPtr.Add(rawMemory, checked((int)(i * rawSize)))).Type == 1) keyboardCount++;
            Check(devices.Count >= keyboardCount, $"Inventory returned {devices.Count} entries for {keyboardCount} Windows keyboard interfaces.");
            Console.WriteLine($"Raw Input keyboard interfaces: {keyboardCount}; listed keyboard entries: {devices.Count}");
        }
        finally { Marshal.FreeHGlobal(rawMemory); }

        var source = new HwndSource(new HwndSourceParameters("Aivana hotkey smoke") { Width = 1, Height = 1, WindowStyle = 0 });
        var cancel = new Tile { Hotkey = "Ctrl+Alt+Shift+F10" };
        var tile = new Tile { Hotkey = "Ctrl+Alt+Shift+F11" };
        var conflict = new Tile { Hotkey = "Ctrl+Alt+Shift+F10" };
        var reserved = new Tile { Hotkey = "Ctrl+F12" };
        using (var hotkeys = new HotkeyService(source.Handle))
        {
            var errors = hotkeys.Register([tile, conflict, reserved], cancel.Hotkey);
            Check(errors.ContainsKey(conflict.Id), "A tile must not take over the cancel hotkey.");
            Check(errors.ContainsKey(reserved.Id), "F12 should report its Windows debugger reservation.");
            Check(!errors.ContainsKey(tile.Id), "An unused safe smoke hotkey should register.");
            var tileTriggered = false;
            var cancelTriggered = false;
            var frame = new System.Windows.Threading.DispatcherFrame();
            hotkeys.Triggered += id => { if (id == tile.Id) { tileTriggered = true; frame.Continue = false; } };
            hotkeys.CancelRequested += () => { cancelTriggered = true; frame.Continue = false; };
            source.AddHook((IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
            {
                if (hotkeys.ProcessMessage(msg, wParam, lParam)) handled = true;
                return IntPtr.Zero;
            });
            Check(PostMessage(source.Handle, 0x0312, new IntPtr(0x5101), new IntPtr(unchecked((int)0x007A0007))), "Synthetic WM_HOTKEY post failed.");
            System.Windows.Threading.Dispatcher.PushFrame(frame);
            Check(tileTriggered, "WM_HOTKEY should dispatch the registered tile id.");
            frame = new System.Windows.Threading.DispatcherFrame();
            Check(PostMessage(source.Handle, 0x0312, new IntPtr(0x5100), new IntPtr(unchecked((int)0x00790007))), "Synthetic cancel WM_HOTKEY post failed.");
            System.Windows.Threading.Dispatcher.PushFrame(frame);
            Check(cancelTriggered, "WM_HOTKEY should dispatch the cancel id.");
            tileTriggered = false; cancelTriggered = false;
            hotkeys.SuspendTiles();
            Check(hotkeys.ProcessMessage(0x0312, new IntPtr(0x5101), IntPtr.Zero), "Suspended tile WM_HOTKEY should be consumed.");
            Check(!tileTriggered, "Suspended tile WM_HOTKEY must not run a tile.");
            Check(hotkeys.ProcessMessage(0x0312, new IntPtr(0x5100), IntPtr.Zero) && cancelTriggered, "Cancel hotkey must remain available while tiles are suspended.");
            hotkeys.Clear();
            errors = hotkeys.Register([tile], cancel.Hotkey);
            Check(!errors.ContainsKey(tile.Id) && !errors.ContainsKey(Guid.Empty), "Clear should unregister hotkeys so they can be registered again.");

            hotkeys.Clear();
            tileTriggered = false;
            var paletteTriggered = false;
            hotkeys.PaletteRequested += () => { paletteTriggered = true; frame.Continue = false; };
            tile.Hotkey = "Ctrl+Alt+Shift+F11";
            errors = hotkeys.Register([tile], cancel.Hotkey, "Ctrl+Alt+Shift+F9");
            Check(!errors.ContainsKey(tile.Id) && !errors.ContainsKey(Guid.Empty), "Cancel, palette, and tile hotkeys should all register without collision.");
            frame = new System.Windows.Threading.DispatcherFrame();
            Check(PostMessage(source.Handle, 0x0312, new IntPtr(0x5101), new IntPtr(unchecked((int)0x00780007))), "Synthetic palette WM_HOTKEY post failed.");
            System.Windows.Threading.Dispatcher.PushFrame(frame);
            Check(paletteTriggered && !tileTriggered, "The palette WM_HOTKEY should dispatch PaletteRequested without triggering a tile.");
            paletteTriggered = false;
            frame = new System.Windows.Threading.DispatcherFrame();
            Check(PostMessage(source.Handle, 0x0312, new IntPtr(0x5102), new IntPtr(unchecked((int)0x007A0007))), "Synthetic shifted tile WM_HOTKEY post failed.");
            System.Windows.Threading.Dispatcher.PushFrame(frame);
            Check(tileTriggered && !paletteTriggered, "The tile registered after the palette should use its own id and dispatch normally.");

            hotkeys.Clear();
            tile.Hotkey = "Ctrl+Alt+Shift+F9";
            errors = hotkeys.Register([tile], cancel.Hotkey, tile.Hotkey);
            Check(errors.ContainsKey(tile.Id) && !errors.ContainsKey(Guid.Empty), "The palette should own its chord and report a tile collision.");
            tileTriggered = false;
            paletteTriggered = false;
            Check(hotkeys.ProcessMessage(0x0312, new IntPtr(0x5101), IntPtr.Zero), "A registered palette id should be consumed.");
            Check(paletteTriggered && !tileTriggered, "A palette and colliding tile must never dispatch as the same tile.");
            hotkeys.Clear();
            paletteTriggered = false;
            Check(!hotkeys.ProcessMessage(0x0312, new IntPtr(0x5101), IntPtr.Zero), "Clear should unregister and forget the palette id.");
            errors = hotkeys.Register([tile], cancel.Hotkey);
            Check(!errors.ContainsKey(tile.Id) && !errors.ContainsKey(Guid.Empty), "Clear should allow subsequent tile registration after a palette hotkey.");
        }
        source.Dispose();
        System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeShutdown();
        Console.WriteLine("Windows adapter smoke checks passed.");
    }
}

using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private Window? _keyboardOverlay;
    private DispatcherTimer? _keyboardOverlayTimer;
    private bool _keyboardOverlayPinned;
    private string? _keyboardOverlaySignature;
    private readonly List<(Border Border, uint VirtualKey, bool Assigned)> _overlayKeyStates = [];

    private void InitializeKeyboardOverlay()
    {
        _keyboardOverlayTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(90) };
        _keyboardOverlayTimer.Tick += (_, _) => UpdateKeyboardOverlay();
        _keyboardOverlayTimer.Start();
    }

    private void ToggleKeyboardOverlay()
    {
        _keyboardOverlayPinned = !_keyboardOverlayPinned;
        UpdateKeyboardOverlay();
    }

    private void UpdateKeyboardOverlay()
    {
        var held = OverlayKeyDown(0x11) && OverlayKeyDown(0x12) && OverlayKeyDown(0x70);
        if (!_keyboardOverlayPinned && !held)
        {
            _keyboardOverlay?.Hide();
            return;
        }
        if (_keyboardOverlay == null)
        {
            _keyboardOverlay = new Window
            {
                Title = L("Tastatur-Overlay"), ShowActivated = false, ShowInTaskbar = false, Topmost = true,
                WindowStyle = WindowStyle.None, ResizeMode = ResizeMode.NoResize, AllowsTransparency = true,
                Background = Brushes.Transparent, IsHitTestVisible = false, Focusable = false,
                Width = Math.Min(1250, SystemParameters.WorkArea.Width - 24), Height = Math.Min(390, SystemParameters.WorkArea.Height - 24)
            };
            _keyboardOverlay.SourceInitialized += (_, _) =>
            {
                var handle = new WindowInteropHelper(_keyboardOverlay).Handle;
                var style = OverlayGetWindowLong(handle, -20);
                OverlaySetWindowLong(handle, -20, style | 0x08000000 | 0x00000020 | 0x00000080);
            };
        }
        var profile = ActiveProfile;
        var signature = _controller.Settings.KeyboardLayoutId + "|" + _controller.Settings.UiLanguage + "|" + _controller.Paused
            + "|" + profile?.Name + "|" + string.Join(";", profile?.Tiles.Select(tile => $"{tile.Id}:{tile.Name}:{tile.Hotkey}:{tile.Enabled}:{tile.CorsairBinding}") ?? []);
        if (_keyboardOverlaySignature != signature)
        {
            _keyboardOverlaySignature = signature;
            _keyboardOverlay.Content = BuildKeyboardOverlayContent(profile);
        }
        _keyboardOverlay.Left = SystemParameters.WorkArea.Left + (SystemParameters.WorkArea.Width - _keyboardOverlay.Width) / 2;
        _keyboardOverlay.Top = SystemParameters.WorkArea.Bottom - _keyboardOverlay.Height - 12;
        if (!_keyboardOverlay.IsVisible) _keyboardOverlay.Show();
        foreach (var key in _overlayKeyStates)
            key.Border.BorderBrush = key.VirtualKey != 0 && OverlayKeyDown((int)key.VirtualKey)
                ? Brush("#FFFFFF") : key.Assigned ? _accent : _line;
    }

    private UIElement BuildKeyboardOverlayContent(Profile? profile)
    {
        _overlayKeyStates.Clear();
        var layout = KeyboardLayoutCatalog.TryGet(_controller.Settings.KeyboardLayoutId, out var selected)
            ? selected : KeyboardLayoutCatalog.All[0];
        var root = new DockPanel { Margin = new Thickness(16) };
        var header = new StackPanel { Margin = new Thickness(0, 0, 0, 12) };
        header.Children.Add(Label(L("Tastatur-Overlay") + " · " + profile?.Name + " · " + L(layout.DisplayName), 16, "#E8F0F1", true));
        header.Children.Add(Label(L("Ctrl+Alt+F1 halten · Overlay über die Befehlspalette ein-/ausblenden."), 11, "#91A3A5"));
        if (_controller.Paused) header.Children.Add(Label(L("PAUSIERT · Hotkeys sind pausiert"), 11, "#F2B66D"));
        DockPanel.SetDock(header, Dock.Top); root.Children.Add(header);
        var minX = layout.Keys.Min(key => key.X); var minY = layout.Keys.Min(key => key.Y);
        const double unit = 55;
        var canvas = new Canvas { Width = (layout.Keys.Max(key => key.X + key.Width) - minX) * unit,
            Height = (layout.Keys.Max(key => key.Y + key.Height) - minY) * unit };
        foreach (var key in layout.Keys)
        {
            var mapped = TryResolveKeyboardMapping(key, layout, out var mapping);
            var gKey = GetCorsairGKeyId(key.PhysicalKeyId);
            var bindings = gKey != null ? profile?.Tiles.Where(tile => tile.Enabled && tile.CorsairBinding?.KeyId == gKey).ToList() ?? []
                : mapped ? FindKeyboardHotkeyMatches(profile, mapping.VirtualKey, null) : [];
            var stack = new StackPanel { Margin = new Thickness(3) };
            stack.Children.Add(new TextBlock { Text = FormatPrintedLegend(key), FontSize = 10, Foreground = Brush("#DCE6E7"), TextTrimming = TextTrimming.CharacterEllipsis });
            foreach (var tile in bindings.Take(2))
                stack.Children.Add(new TextBlock { Text = tile.Name + (string.IsNullOrEmpty(tile.Hotkey) ? "" : " · " + tile.Hotkey),
                    FontSize = 8, Foreground = _accent, TextWrapping = TextWrapping.Wrap, MaxHeight = 23 });
            if (bindings.Count > 2) stack.Children.Add(Label("+" + (bindings.Count - 2), 8));
            var border = new Border { Width = key.Width * unit - 3, Height = key.Height * unit - 3,
                Background = bindings.Count > 0 ? Brush("#214642") : Brush("#171F21"), BorderBrush = bindings.Count > 0 ? _accent : _line,
                BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(5), Child = stack,
                Clip = CreateKeyboardKeyClip(key.Shape, key.Width * unit - 3, key.Height * unit - 3) };
            Canvas.SetLeft(border, (key.X - minX) * unit); Canvas.SetTop(border, (key.Y - minY) * unit); canvas.Children.Add(border);
            _overlayKeyStates.Add((border, mapped ? mapping.VirtualKey : 0, bindings.Count > 0));
        }
        root.Children.Add(new Viewbox { Child = canvas, Stretch = Stretch.Uniform });
        return new Border { Background = Brush("#F0111719"), BorderBrush = _accent, BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(14), Child = root };
    }

    private void DisposeKeyboardOverlay()
    {
        _keyboardOverlayTimer?.Stop(); _keyboardOverlayTimer = null;
        _keyboardOverlay?.Close(); _keyboardOverlay = null; _overlayKeyStates.Clear();
    }

    internal void RunKeyboardOverlaySmokeChecks()
    {
        foreach (var layout in KeyboardLayoutCatalog.All)
        {
            var original = _controller.Settings.KeyboardLayoutId;
            try
            {
                _controller.Settings.KeyboardLayoutId = layout.Id;
                var content = BuildKeyboardOverlayContent(ActiveProfile);
                if (_overlayKeyStates.Count != layout.Keys.Count) throw new InvalidOperationException("Overlay loses physical key geometry.");
                if (content is not Border) throw new InvalidOperationException("Overlay content is missing.");
            }
            finally { _controller.Settings.KeyboardLayoutId = original; }
        }
        _keyboardOverlayPinned = true; UpdateKeyboardOverlay();
        if (_keyboardOverlay?.IsVisible != true || _keyboardOverlay.ShowActivated || _keyboardOverlay.IsHitTestVisible)
            throw new InvalidOperationException("Overlay must remain nonactivating and ignore clicks.");
        _keyboardOverlayPinned = false; UpdateKeyboardOverlay();
        if (_keyboardOverlay.IsVisible) throw new InvalidOperationException("Overlay does not hide after release.");
    }

    internal void RunUspVisualSmokeChecks(Action<Window, string> snapshot)
    {
        var wizard = BuildSpecialKeyWizardWindow();
        try { wizard.Show(); wizard.UpdateLayout(); snapshot(wizard, "special-key-wizard"); }
        finally { wizard.Close(); }
        var fixture = new Tile { Name = "OBS", Hotkey = "Ctrl+Alt+G", Steps = [new ActionStep { Kind = ActionKind.Delay, DelayMs = 0 }] };
        ActiveProfile!.Tiles.Add(fixture);
        _keyboardOverlayPinned = true;
        try { UpdateKeyboardOverlay(); _keyboardOverlay!.UpdateLayout(); snapshot(_keyboardOverlay, "keyboard-overlay"); }
        finally { ActiveProfile.Tiles.Remove(fixture); _keyboardOverlayPinned = false; UpdateKeyboardOverlay(); }
        ShowExecutionFeedback(new ExecutionProgress(Guid.NewGuid(), ExecutionPhase.Step, 2, 4, 1, 2));
        try { _executionFeedbackWindow!.UpdateLayout(); snapshot(_executionFeedbackWindow, "execution-feedback"); }
        finally { HideExecutionFeedback(null, EventArgs.Empty); }
    }

    private static bool OverlayKeyDown(int virtualKey) => (OverlayGetAsyncKeyState(virtualKey) & 0x8000) != 0;
    [DllImport("user32.dll", EntryPoint = "GetAsyncKeyState")] private static extern short OverlayGetAsyncKeyState(int virtualKey);
    [DllImport("user32.dll", EntryPoint = "GetWindowLongW")] private static extern int OverlayGetWindowLong(nint handle, int index);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongW")] private static extern int OverlaySetWindowLong(nint handle, int index, int value);
}

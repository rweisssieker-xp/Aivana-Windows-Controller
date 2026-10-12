using System.Windows;
using System.Windows.Controls;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    public void RunExtensionSmokeChecks(Action<Window, string> snapshot)
    {
        using var icon = AppIcons.Load();
        using var paused = AppIcons.Load(true);
        if (icon.Width < 16 || paused.Width < 16) throw new InvalidOperationException("Tray icon resources are invalid.");
        RunGestureEditorChecks();
        _controller.RunGestureInjectionChecks();
        var fields = new StackPanel();
        var tile = new Tile { KeyboardBinding = new("smoke-keyboard", 0x7C) };
        AddGenericKeyFields(fields, tile);
        var buttons = fields.Children.OfType<Button>().ToArray();
        if (buttons.Length != 2) throw new InvalidOperationException("Generic key editor missing.");
        buttons[1].RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        if (tile.KeyboardBinding != null) throw new InvalidOperationException("Generic key binding removal failed.");
        RunWorkflowEditorSmokeChecks(snapshot);
        RunRecorderSyncSmokeChecks(snapshot);
    }
}

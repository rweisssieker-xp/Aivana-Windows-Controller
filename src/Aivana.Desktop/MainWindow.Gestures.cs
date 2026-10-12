using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private sealed record GestureChoice(KeyGesture Value, string Label);
    private void AddGestureFields(StackPanel content, Tile tile)
    {
        content.Children.Add(Label(L("Tastengeste und Ebene"), 12, "#91A3A5"));
        var choices = new[] { new GestureChoice(KeyGesture.Press,L("Sofortdruck")), new GestureChoice(KeyGesture.Tap,L("Kurz drücken")), new GestureChoice(KeyGesture.Hold,L("Gedrückt halten")), new GestureChoice(KeyGesture.DoubleTap,L("Doppelt drücken")) };
        var mode = new ComboBox { ItemsSource = choices, DisplayMemberPath = "Label", SelectedValuePath = "Value", SelectedValue = tile.KeyGesture, Margin = new Thickness(0,4,0,6) };
        mode.SelectionChanged += (_,_) => { if (mode.SelectedValue is KeyGesture gesture) tile.KeyGesture = gesture; Refresh(); };
        content.Children.Add(mode);
        content.Children.Add(Label(L("Nur in Ebene (leer = Basis)"), 11, "#91A3A5"));
        var layer = new TextBox { Text = tile.KeyLayer, MaxLength = 40, Margin = new Thickness(0,4,0,6) };
        layer.TextChanged += (_,_) => { tile.KeyLayer = layer.Text.Trim(); Refresh(); }; content.Children.Add(layer);
        content.Children.Add(Label(L("Während diese Taste gehalten wird: Ebene aktivieren"),11,"#91A3A5"));
        var held = new TextBox { Text = tile.HeldLayer, MaxLength = 40, Margin = new Thickness(0,4,0,6) };
        held.TextChanged += (_,_) => { tile.HeldLayer = held.Text.Trim(); Refresh(); }; content.Children.Add(held);
        content.Children.Add(Label(L("Ebenentasten führen keine Makros aus. Loslassen stellt die Basisebene wieder her."),11,"#748688"));
        content.Children.Add(Label(L("Halten ab (ms)"),11,"#91A3A5"));
        var hold = new EditorNumericUpDown(tile.HoldMilliseconds, min: 150, max: 5000); hold.ValueChanged += value => { tile.HoldMilliseconds=value; Refresh(); }; content.Children.Add(hold);
        content.Children.Add(Label(L("Doppeldruck innerhalb (ms)"),11,"#91A3A5"));
        var twice = new EditorNumericUpDown(tile.DoubleTapMilliseconds, min: 150, max: 1000); twice.ValueChanged += value => { tile.DoubleTapMilliseconds=value; Refresh(); }; content.Children.Add(twice);
    }
}

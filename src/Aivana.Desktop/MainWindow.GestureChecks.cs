using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void RunGestureEditorChecks()
    {
        var tile = new Tile { CorsairBinding = new("gesture-fixture",1), Steps=[new(){Kind=ActionKind.Delay}] };
        var panel = new StackPanel(); AddGestureFields(panel,tile);
        var picker = panel.Children.OfType<ComboBox>().Single();
        picker.SelectedValue = KeyGesture.Hold;
        if(tile.KeyGesture != KeyGesture.Hold) throw new InvalidOperationException("Gesture editor did not persist hold choice.");
        picker.SelectedValue = KeyGesture.DoubleTap;
        if(tile.KeyGesture != KeyGesture.DoubleTap) throw new InvalidOperationException("Gesture editor did not persist double-tap choice.");
        var texts=panel.Children.OfType<TextBox>().ToArray(); texts[0].Text="Media"; texts[1].Text="Tools";
        if(tile.KeyLayer!="Media" || tile.HeldLayer!="Tools") throw new InvalidOperationException("Gesture editor did not persist layer fields.");
        var invalid=CorsairBindingValidator.ValidateTile(tile);
        if(invalid.Count==0) throw new InvalidOperationException("Layer activator must reject deferred gesture and nested layer.");
        texts[0].Text=""; picker.SelectedValue=KeyGesture.Press;
        if(CorsairBindingValidator.ValidateTile(tile).Count>0) throw new InvalidOperationException("Base immediate layer activator should validate.");
    }
}

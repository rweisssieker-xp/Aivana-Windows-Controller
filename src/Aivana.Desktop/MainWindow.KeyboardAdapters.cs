using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Aivana.Core;
using Aivana.Windows.KeyboardAdapters;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void ShowKeyboardAdapters()
    {
        var window=new Window { Owner=this,Title=L("Tastaturhersteller und SDKs"),Width=850,Height=730,Background=_panel,Foreground=System.Windows.Media.Brushes.White };
        window.Resources.MergedDictionaries.Add(Resources);
        var body=new StackPanel { Margin=new Thickness(20) };
        body.Children.Add(Label(L("Hersteller-Anbindungen"),22,"#E8F0F1",true));
        foreach(var text in new[] {
            "Corsair: vorhandene iCUE-G-Tasten und RGB-Anbindung.",
            "Logitech G: natives G-Key-SDK (M1–M3); SDK liefert keine eindeutige Identität mehrerer Tastaturen. Modellunterstützung prüfen. Beleuchtung über separates LED-SDK.",
            "Razer: Chroma-RGB; proprietäre Tasten in Synapse auf F13–F24 oder ein Windows-Kürzel legen und anschließend erkennen.",
            "SteelSeries: GameSense-RGB für unterstützte Funktionstasten; proprietäre Tasten über GG auf Standardtasten legen.",
            "Wooting: Analog-SDK mit Druckschwellen und gerätebezogenen IDs; HID-Tasteneingabe bleibt aktiv.",
            "Keychron / QMK / VIA / Ducky / ASUS / HyperX / Roccat / weitere HID-Tastaturen: physische Standardtasten erkennen. Fn, Räder und proprietäre Bedienelemente sind modellabhängig.",
            "Exklusives Makropad: noch nicht verfügbar. Erfordert einen für Windows 11 geprüften, signierten und passend lizenzierten Filtertreiber. Raw Input unterdrückt keine Eingaben."
        }) body.Children.Add(Label(L(text),13,"#AABABC",false,new Thickness(0,6,0,8)));
        var adapter=new ComboBox { ItemsSource=new[] { "logitech-g","logitech-led","wooting" },SelectedIndex=0 };body.Children.Add(adapter);
        var status=Label(_controller.VendorAdapterStatus,13,"#F2B66D");body.Children.Add(status);
        body.Children.Add(Button(L("Offizielle x64-SDK-DLL auswählen und freigeben"),()=>
        {
            var picker=new OpenFileDialog { Filter="SDK DLL|*.dll" };if(picker.ShowDialog(window)!=true)return;
            if(MessageBox.Show(window,L("Nur eine originale Hersteller-SDK-DLL auswählen. Sie wird mit deinen Benutzerrechten geladen. Fortfahren?"),L("SDK freigeben"),MessageBoxButton.YesNo)!=MessageBoxResult.Yes)return;
            try
            {
                var id=(string)adapter.SelectedItem;var directory=Path.Combine(ApprovedNativeLibrary.Root,id);Directory.CreateDirectory(directory);
                var bytes=File.ReadAllBytes(picker.FileName);var hash=Convert.ToHexString(SHA256.HashData(bytes));
                File.WriteAllBytes(Path.Combine(directory,"adapter.dll"),bytes);_controller.Settings.VendorIntegrations.ApprovedLibraries[id]=hash;_controller.Save();_controller.ReloadKeyboardAdapters();status.Text=_controller.VendorAdapterStatus;
            }catch(Exception ex){status.Text=ex.Message;}
        }));
        body.Children.Add(Button(L("SDK deaktivieren"),()=> { _controller.Settings.VendorIntegrations.ApprovedLibraries.Remove((string)adapter.SelectedItem);_controller.Save();_controller.ReloadKeyboardAdapters();status.Text=_controller.VendorAdapterStatus; }));
        body.Children.Add(Label(L("Wooting: Druckschwellen (0–1, Loslassen < Drücken)"),13));
        var press=new TextBox { Text=_controller.Settings.VendorIntegrations.AnalogPress.ToString(System.Globalization.CultureInfo.InvariantCulture) };var release=new TextBox { Text=_controller.Settings.VendorIntegrations.AnalogRelease.ToString(System.Globalization.CultureInfo.InvariantCulture) };body.Children.Add(press);body.Children.Add(release);
        body.Children.Add(Button(L("Schwellen speichern"),()=> { if(float.TryParse(press.Text,System.Globalization.CultureInfo.InvariantCulture,out var p) && float.TryParse(release.Text,System.Globalization.CultureInfo.InvariantCulture,out var r) && r>=0 && r<p && p<=1) { _controller.Settings.VendorIntegrations.AnalogPress=p;_controller.Settings.VendorIntegrations.AnalogRelease=r;_controller.Save(); } else status.Text=L("Ungültige Druckschwellen."); }));
        window.Content=new ScrollViewer { Content=body,VerticalScrollBarVisibility=ScrollBarVisibility.Auto };window.Show();
    }
    private void AddVendorBindingFields(Panel body,Tile tile)
    {
        body.Children.Add(Label(L("Hersteller-SDK-Taste"),12,"#91A3A5"));
        var state=Label(tile.VendorBinding?.ToString() ?? L("Keine Bindung"),12,"#91A3A5");body.Children.Add(state);
        body.Children.Add(Button(L("SDK-Taste erkennen"),()=>
        {
            var window=new Window { Owner=this,Title=L("SDK-Taste erkennen"),Width=600,Height=280,Background=_panel,Foreground=System.Windows.Media.Brushes.White };
        window.Resources.MergedDictionaries.Add(Resources);
            var text=Label(L("Logitech-G-Taste oder Wooting-Analogtaste drücken. SDK muss vorher freigegeben sein. Während der Erkennung ist die Steuerung pausiert."),14);
            window.Content=new ScrollViewer { Content=text,Margin=new Thickness(20) };
            var paused=_controller.Paused;_controller.SetPaused(true);
            void Capture(VendorKeyEdge edge) { if(!edge.Pressed)return;tile.VendorBinding=new(edge.Adapter,edge.DeviceId,edge.KeyCode);state.Text=tile.VendorBinding.ToString();ScheduleEditorHistoryCapture();window.Close(); }
            _controller.VendorKeyObserved+=Capture;
            window.Closed+=(_,_)=> { _controller.VendorKeyObserved-=Capture;_controller.SetPaused(paused); };window.ShowDialog();
        }));
        body.Children.Add(Button(L("SDK-Bindung entfernen"),()=> { tile.VendorBinding=null;state.Text=L("Keine Bindung");ScheduleEditorHistoryCapture(); }));
    }
    private void AddVendorLightingFields(Panel body,ActionStep step)
    {
        var picker=new ComboBox { ItemsSource=new[] { "razer","steelseries","logitech-led" },SelectedItem=string.IsNullOrEmpty(step.Arguments)?"razer":step.Arguments };
        step.Arguments=(string)picker.SelectedItem;
        picker.SelectionChanged+=(_,_)=> { step.Arguments=(string)picker.SelectedItem;ScheduleEditorHistoryCapture(); };body.Children.Add(picker);
        body.Children.Add(EditorInput(step.Value,v=>step.Value=v,"#35D0C1"));
    }
}

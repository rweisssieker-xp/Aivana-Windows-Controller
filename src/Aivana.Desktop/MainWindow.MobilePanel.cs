using System.Windows;
using System.Windows.Controls;
using System.Net.NetworkInformation;
using System.Net.Sockets;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void ShowMobilePanel()
    {
        var window=new Window { Owner=this,Title=L("Mobilpanel"),Width=760,Height=620,Background=_panel,Foreground=System.Windows.Media.Brushes.White };
        window.Resources.MergedDictionaries.Add(Resources);
        var body=new StackPanel { Margin=new Thickness(20) };
        body.Children.Add(Label(L("Lokales HTTPS-Panel · nur freigegebene Profile"),18,"#E8F0F1",true));
        var addresses=NetworkInterface.GetAllNetworkInterfaces().Where(n=>n.OperationalStatus==OperationalStatus.Up).SelectMany(n=>n.GetIPProperties().UnicastAddresses).Where(a=>a.Address.AddressFamily==AddressFamily.InterNetwork).Select(a=>a.Address.ToString()).Prepend("127.0.0.1").Distinct().ToArray();
        var bind=new ComboBox { ItemsSource=addresses,SelectedIndex=0 }; body.Children.Add(bind);
        var profiles=new ListBox { ItemsSource=_controller.Settings.Profiles,DisplayMemberPath="Name",SelectionMode=SelectionMode.Multiple,Height=130 }; body.Children.Add(profiles);
        var status=new TextBox { IsReadOnly=true,AcceptsReturn=true,TextWrapping=TextWrapping.Wrap,MinHeight=130 }; body.Children.Add(status);
        body.Children.Add(Button(L("Panel starten und Kopplungscode anzeigen"),async ()=> { try { status.Text=await _controller.StartMobilePanelAsync((string)bind.SelectedItem,profiles.SelectedItems.OfType<Aivana.Core.Profile>().Select(p=>p.Id)); } catch(Exception ex) { status.Text=ex.Message; } },true));
        body.Children.Add(Button(L("Panel stoppen"),()=> { _controller.StopMobilePanel();status.Text=L("Gestoppt"); }));
        body.Children.Add(Button(L("Alle gekoppelten Geräte widerrufen"),()=> { _controller.RevokeMobileClients();status.Text=L("Geräte widerrufen; Panel gestoppt."); }));
        body.Children.Add(Label(L("Die angezeigte HTTPS-Adresse auf dem Mobilgerät öffnen. Zertifikat-Fingerabdruck vergleichen und das lokale Zertifikat bewusst vertrauen. Zertifikat: %LOCALAPPDATA%\\MAKRIVO\\MobilePanel\\panel.cer. Bei LAN-Zugriff kann eine manuelle Firewallfreigabe nötig sein. Keine Internet-Portweiterleitung einrichten."),13,"#AABABC"));
        window.Content=new ScrollViewer { Content=body,VerticalScrollBarVisibility=ScrollBarVisibility.Auto };window.Show();
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace Aivana.Desktop;

public sealed class RetailLicenseDialog : Window
{
    public RetailLicenseDialog(DesktopController controller)
    {
        Title = "MAKRIVO – " + RetailText.Get("License"); Width = 620; Height = 430; MinWidth = 420; MinHeight = 300; WindowStartupLocation = WindowStartupLocation.CenterOwner;
        var panel = new StackPanel { Margin = new Thickness(22) }; Content = new ScrollViewer { Content = panel, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        panel.Children.Add(new TextBlock { Text = RetailText.Get("29 EUR · up to 4 own PCs") + "\n" + RetailText.Get("One-time purchase, no subscription") + "\n" + RetailText.Get("Purchased major version works offline permanently."), TextWrapping = TextWrapping.Wrap });
        var status = new TextBlock { Margin = new Thickness(0,16,0,12), TextWrapping = TextWrapping.Wrap }; panel.Children.Add(status);
        void Refresh() { try { var result = controller.RetailLicense.Validate(); status.Text = result.IsValid ? RetailText.Get("License valid") : RetailText.Get("License required") + ": " + result.Error; } catch (Exception ex) { status.Text = ex.Message; } }
        Refresh();
        panel.Children.Add(new TextBlock { Text = RetailText.Get("Device code for web activation:") });
        var code = new TextBox { IsReadOnly = true, Margin = new Thickness(0,6,0,12) }; panel.Children.Add(code);
        try { code.Text = controller.RetailLicense.DeviceId; } catch (Exception ex) { status.Text = ex.Message; }
        var purchase = new Button { Content = RetailText.Get("Buy / web activation"), Margin = new Thickness(0,4,0,4) }; panel.Children.Add(purchase);
        purchase.Click += (_,_) => { try { var url = typeof(App).Assembly.GetCustomAttributes<AssemblyMetadataAttribute>().FirstOrDefault(a => a.Key == "MakrivoCommerceUrl")?.Value; if (!Uri.TryCreate(url,UriKind.Absolute,out var uri) || uri.Scheme != "https") throw new InvalidOperationException(RetailText.Get("Commerce URL unavailable.")); Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true }); } catch (Exception ex) { status.Text = ex.Message; } };
        var import = new Button { Content = RetailText.Get("Import license file"), Margin = new Thickness(0,4,0,4) }; panel.Children.Add(import);
        import.Click += (_,_) => { try { var picker = new Microsoft.Win32.OpenFileDialog { Filter = "MAKRIVO license (*.json)|*.json" }; if (picker.ShowDialog(this) == true) { if (new FileInfo(picker.FileName).Length > 65536) throw new InvalidDataException(RetailText.Get("License file too large.")); controller.RetailLicense.Import(File.ReadAllText(picker.FileName)); Refresh(); } } catch (Exception ex) { status.Text = ex.Message; } };
        panel.Children.Add(new TextBlock { Margin = new Thickness(0,12,0,0), Text = RetailText.Get("Editing profiles, export and cancellation remain available."), TextWrapping = TextWrapping.Wrap });
    }
}

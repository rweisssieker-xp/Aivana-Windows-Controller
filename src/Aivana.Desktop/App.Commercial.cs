using System.Windows;
using System.Windows.Threading;

namespace Aivana.Desktop;

public partial class App
{
    public App()
    {
        Startup += (_,_) => Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
        {
            if (_window == null || _controller == null) return;
            if (!DesktopController.IsRetailBuild) { _window.Title += " [" + RetailText.Get("Development build") + "]"; return; }
            // Persistently reachable in the tray menu, including when the main window is hidden.
            AddRetailTrayItem();
            _controller.Changed += AddRetailTrayItem;
            _window.ContextMenu ??= new System.Windows.Controls.ContextMenu();
            var item = new System.Windows.Controls.MenuItem { Header = "MAKRIVO – " + RetailText.Get("License") };
            item.Click += (_,_) => ShowRetailLicense();
            _window.ContextMenu.Items.Add(item);
            ShowRetailLicense();
        }));
    }
    private void AddRetailTrayItem()
    {
        if (!Dispatcher.CheckAccess()) { Dispatcher.BeginInvoke(AddRetailTrayItem); return; }
        if (_tray?.ContextMenuStrip is {} menu && !menu.Items.Cast<System.Windows.Forms.ToolStripItem>().Any(i => (string?)i.Tag == "MAKRIVO.License"))
        { var item = menu.Items.Add(RetailText.Get("License"), null, (_,_) => ShowRetailLicense()); item.Tag = "MAKRIVO.License"; }
    }
    private void ShowRetailLicense()
    {
        if (_controller == null) return;
        var dialog = new RetailLicenseDialog(_controller);
        if (_window?.IsVisible == true) dialog.Owner = _window;
        dialog.ShowDialog();
    }
}

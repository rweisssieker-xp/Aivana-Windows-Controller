using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using Localization = Aivana.Core.Localization;
using Forms = System.Windows.Forms;

namespace Aivana.Desktop;

public partial class App : System.Windows.Application
{
    private Mutex? _mutex;
    private Forms.NotifyIcon? _tray;
    private System.Drawing.Icon? _activeIcon;
    private System.Drawing.Icon? _pausedIcon;
    private DesktopController? _controller;
    private MainWindow? _window;
    private bool _exiting;
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        if (e.Args.Contains("--smoke-test")) { RunSmokeTest(); return; }
        _mutex = new Mutex(true, "Local\\Aivana.Windows.Controller", out var first);
        if (!first) { System.Windows.MessageBox.Show(Localization.Translate("MAKRIVO läuft bereits. Öffne ihn über das Symbol im Infobereich."), "MAKRIVO"); Shutdown(); return; }
        DispatcherUnhandledException += (_, args) => { System.Windows.MessageBox.Show(args.Exception.Message, Localization.Translate("MAKRIVO – Fehler")); args.Handled = true; };
        try
        {
            _controller = new DesktopController();
            Localization.SetLanguage(_controller.Settings.UiLanguage);
            _window = new MainWindow(_controller);
            MainWindow = _window;
            _window.SourceInitialized += (_, _) => _controller.Attach(new WindowInteropHelper(_window).Handle);
            _window.Closing += OnClosing;
            _controller.Changed += UpdateTray;
            _activeIcon = AppIcons.Load(); _pausedIcon = AppIcons.Load(paused: true);
            _window.Icon = System.Windows.Media.Imaging.BitmapFrame.Create(new Uri("pack://application:,,,/Assets/makrivo.png"));
            _tray = new Forms.NotifyIcon { Icon = _activeIcon, Text = "MAKRIVO", Visible = true };
            _tray.DoubleClick += (_, _) => ShowWindow();
            UpdateTray();
            _window.Show();
        }
        catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message, Localization.Translate("MAKRIVO – Startfehler")); ExitApp(); }
    }
    private void ShowWindow() { _window?.Show(); if (_window != null) { _window.WindowState = WindowState.Normal; _window.Activate(); } }
    private async void RunSmokeTest()
    {
        var output = System.IO.Path.Combine(AppContext.BaseDirectory, "smoke");
        System.IO.Directory.CreateDirectory(output);
        var testDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "AivanaSmoke-" + Guid.NewGuid().ToString("N"));
        DesktopController? controller = null;
        try
        {
            controller = new DesktopController(testDirectory);
            controller.Settings.UiLanguage = "de";
            controller.Save();
            Localization.SetLanguage("de");
            var window = new MainWindow(controller);
            window.Show();
            window.UpdateLayout();
            controller.Attach(new WindowInteropHelper(window).Handle);
            controller.SetPaused(true);
            await DesktopControllerExecutionChecks.RunAsync();
            await controller.RunSpecialKeyCaptureChecksAsync();
            window.RunFunctionalRegressionChecks();
            window.RunExtensionSmokeChecks((target, name) => SaveSnapshot(target, System.IO.Path.Combine(output, name + ".png")));
            window.RunKeyboardOverlaySmokeChecks();
            window.RunSpecialKeyWizardSmokeChecks();
            window.RunExecutionFeedbackSmokeChecks();
            window.RunUspVisualSmokeChecks((target, name) => SaveSnapshot(target, System.IO.Path.Combine(output, name + ".png")));
            window.RunUiSmokeChecks();
            window.RunSixExtensionsSmokeChecks((target,name)=>SaveSnapshot(target,System.IO.Path.Combine(output,name+".png")));
            window.RunActionCatalogSmokeChecks((dialog, name) => SaveSnapshot(dialog, System.IO.Path.Combine(output, name + ".png")));
            window.RunLocalizationSmokeChecks((target, name) => SaveSnapshot(target, System.IO.Path.Combine(output, name + ".png")));
            controller.ActiveProfile.Name = "Smoke-Profil";
            controller.Save();
            var reloaded = new Aivana.Core.ConfigStore(testDirectory).Load();
            if (reloaded.Profiles[0].Name != "Smoke-Profil") throw new InvalidOperationException("Profil wurde nicht gespeichert.");
            controller.ActiveProfile.Name = "Standard";
            controller.Save();
            controller.SetPaused(false);
            window.Refresh(); window.UpdateLayout();
            SaveSnapshot(window, System.IO.Path.Combine(output, "dashboard.png"));
            window.EditTile(controller.ActiveProfile.Tiles[0].Id); window.UpdateLayout();
            SaveSnapshot(window, System.IO.Path.Combine(output, "editor.png"));
            foreach (var section in new[] { "Profile", "Geräte", "Einstellungen", "Tastatur" })
            {
                window.NavigateToSection(section); window.UpdateLayout();
                SaveSnapshot(window, System.IO.Path.Combine(output, section + ".png"));
            }
            var originalKeyboardLayout = controller.Settings.KeyboardLayoutId;
            window.SelectKeyboardLayoutForSmoke("corsair-k100-iso-de"); window.UpdateLayout();
            SaveSnapshot(window, System.IO.Path.Combine(output, "Tastatur-K100.png"));
            window.SelectKeyboardLayoutForSmoke(originalKeyboardLayout); window.UpdateLayout();
            window.NavigateToSection("Dashboard");
            controller.SetProfileLocked(true);
            if (!controller.ProfileLocked) throw new InvalidOperationException("Profilsperre wurde nicht übernommen.");
            controller.SetProfileLocked(false);
            window.ShowCommandPalette();
            var palette = System.Windows.Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.Title == "Befehlspalette")
                ?? throw new InvalidOperationException("Befehlspalette wurde nicht angezeigt.");
            palette.UpdateLayout(); SaveSnapshot(palette, System.IO.Path.Combine(output, "palette.png")); palette.Close();
            window.Width = 900; window.Height = 720; window.UpdateLayout();
            SaveSnapshot(window, System.IO.Path.Combine(output, "dashboard-compact.png"));
            if (window.HasUnsavedChanges) throw new InvalidOperationException("Das Rendern hat einen gespeicherten Editorentwurf verändert.");
            System.IO.File.WriteAllText(System.IO.Path.Combine(output, "result.txt"), $"PASS: WPF-Fenster und alle Ansichten gerendert; Kachel-CRUD und Makroeditor geprüft; Profil gespeichert und geladen; {controller.Devices.Count} Geräte; Hotkeys registriert und pausiert; {controller.CorsairStatus}");
            controller.Dispose(); window.Close(); Shutdown(0);
        }
        catch (Exception ex)
        {
            System.IO.File.WriteAllText(System.IO.Path.Combine(output, "result.txt"), "FAIL: " + ex);
            controller?.Dispose(); Shutdown(1);
        }
    }
    private static void SaveSnapshot(Window window, string path)
    {
        var content = (FrameworkElement)window.Content;
        var bitmap = new System.Windows.Media.Imaging.RenderTargetBitmap((int)Math.Ceiling(content.ActualWidth), (int)Math.Ceiling(content.ActualHeight), 96, 96, System.Windows.Media.PixelFormats.Pbgra32);
        bitmap.Render(content);
        var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
        encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmap));
        using var file = System.IO.File.Create(path); encoder.Save(file);
    }
    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (e.Cancel || _exiting || _controller == null) return;
        if (!_controller.Settings.CloseToTray) { e.Cancel = true; ExitApp(); return; }
        e.Cancel = true; _window?.Hide();
        _tray?.ShowBalloonTip(2200, Localization.Translate("MAKRIVO bleibt aktiv"), Localization.Translate("Hotkeys laufen weiter. Doppelklick auf das Symbol öffnet das Fenster."), Forms.ToolTipIcon.Info);
    }
    private void UpdateTray()
    {
        if (!Dispatcher.CheckAccess()) { Dispatcher.BeginInvoke(UpdateTray); return; }
        if (_tray == null || _controller == null) return;
        _tray.Icon = _controller.Paused ? _pausedIcon : _activeIcon;
        var trayText = "MAKRIVO · " + (_controller.Paused ? Localization.Translate("Pausiert") : Localization.Translate("Aktiv")) + " · " + _controller.ActiveProfile.Name;
        _tray.Text = trayText.Length > 63 ? trayText[..63] : trayText;
        var menu = new Forms.ContextMenuStrip();
        menu.Items.Add(Localization.Translate("Öffnen"), null, (_, _) => ShowWindow());
        menu.Items.Add(_controller.Paused ? Localization.Translate("Hotkeys aktivieren") : Localization.Translate("Hotkeys pausieren"), null, (_, _) => _controller.SetPaused(!_controller.Paused));
        var profiles = new Forms.ToolStripMenuItem(Localization.Translate("Profil"));
        foreach (var profile in _controller.Settings.Profiles)
        {
            var id = profile.Id;
            var item = new Forms.ToolStripMenuItem(profile.Name) { Checked = id == _controller.ActiveProfile.Id };
            item.Click += (_, _) => _controller.SelectProfile(id);
            profiles.DropDownItems.Add(item);
        }
        menu.Items.Add(profiles);
        menu.Items.Add(Localization.Translate("Makros abbrechen"), null, (_, _) => _controller.CancelAll());
        menu.Items.Add(Localization.Translate("Beenden"), null, (_, _) => ExitApp());
        var old = _tray.ContextMenuStrip; _tray.ContextMenuStrip = menu; old?.Dispose();
    }
    private void ExitApp()
    {
        if (_window != null && !_window.PrepareToClose()) return;
        if (_exiting) return; _exiting = true;
        _controller?.Dispose(); _tray?.Dispose(); _window?.Close(); Shutdown();
    }
    protected override void OnExit(ExitEventArgs e)
    {
        _controller?.Dispose(); _tray?.Dispose(); _activeIcon?.Dispose(); _pausedIcon?.Dispose(); _mutex?.Dispose(); base.OnExit(e);
    }
}

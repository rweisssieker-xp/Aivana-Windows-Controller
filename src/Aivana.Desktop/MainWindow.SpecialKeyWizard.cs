using System.Windows;
using System.Windows.Controls;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    public void ShowSpecialKeyWizard()
    {
        if (ActiveProfile is null || !ResolvePendingDraft()) return;
        BuildSpecialKeyWizardWindow().ShowDialog();
    }

    private Window BuildSpecialKeyWizardWindow()
    {
        var dialog = new Window
        {
            Title = L("Sondertasten-Assistent"), Owner = this, Width = 700, Height = 650,
            MinWidth = 580, MinHeight = 550, WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = Brush("#101719"), Foreground = Brush("#E8F0F1"), FontFamily = FontFamily,
            ShowInTaskbar = false
        };
        dialog.Resources.MergedDictionaries.Add(Resources);
        var panel = new StackPanel { Margin = new Thickness(24) };
        dialog.Content = new ScrollViewer { Content = panel, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        panel.Children.Add(Label(L("Sondertasten-Assistent"), 24, "#E8F0F1", true));
        panel.Children.Add(new TextBlock { Text = L("Wähle dein Gerät und eine unterstützte G-Taste. Die Zuordnung wird als Entwurf geöffnet."), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 12, 0, 12) });
        panel.Children.Add(FieldLabel(L("Gerät")));
        var devices = new ComboBox { Name = "WizardDevice", DisplayMemberPath = "Name", ItemsSource = _controller.CorsairKeyboards.ToArray(), Margin = new Thickness(0, 4, 0, 12) };
        panel.Children.Add(devices);
        panel.Children.Add(FieldLabel(L("Taste")));
        var keys = new ComboBox { Name = "WizardKey", DisplayMemberPath = "Name", SelectedValuePath = "KeyId", Margin = new Thickness(0, 4, 0, 12) };
        panel.Children.Add(keys);
        var status = new TextBlock { Text = _controller.CorsairKeyStatus, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 8, 0, 12) };
        panel.Children.Add(status);
        panel.Children.Add(new TextBlock { Text = L("Fn, Profil-, Sperr- und iCUE-Radtasten werden vom G-Tasten-SDK nicht als belegbare Tasten bereitgestellt. Andere Hersteller können Standard-Hotkeys senden; diese zeichnest du im Aktionseditor auf."), TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 0, 0, 16) });
        IDisposable? capture = null;
        Button? listen = null;
        void StopCapture() { capture?.Dispose(); capture = null; if (listen is not null) listen.Content = L("Tastendruck erkennen"); }
        listen = Button(L("Tastendruck erkennen"), () =>
        {
            if (capture is not null) { StopCapture(); return; }
            if (devices.SelectedItem is not Aivana.Windows.CorsairKeyboardInfo device) return;
            try
            {
                capture = _controller.BeginSpecialKeyCapture(device.DeviceId, press =>
                {
                    keys.SelectedValue = press.KeyId;
                    status.Text = FindSpecialKeyConflict(press.DeviceId, press.KeyId) ?? LF($"G{press.KeyId} erkannt. Wähle jetzt eine Aktion.");
                    StopCapture();
                });
                listen!.Content = L("Erkennung stoppen");
                status.Text = L("Drücke eine G-Taste. Bestehende G-Tasten-Aktionen sind während der Erkennung ausgesetzt.");
            }
            catch (Exception ex) { StopCapture(); status.Text = ex.Message; }
        }, false);
        panel.Children.Add(listen);
        panel.Children.Add(FieldLabel(L("Aktion")));
        var actions = new ComboBox { Name = "WizardAction", ItemsSource = ActionCatalog.Presets.ToArray(), DisplayMemberPath = "Name", SelectedIndex = 0, Margin = new Thickness(0, 4, 0, 16) };
        panel.Children.Add(actions);
        void UpdateBindingStatus()
        {
            if (devices.SelectedItem is Aivana.Windows.CorsairKeyboardInfo device && keys.SelectedValue is int key
                && FindSpecialKeyConflict(device.DeviceId, key) is { } conflict)
                status.Text = conflict;
        }
        keys.SelectionChanged += (_, _) => UpdateBindingStatus();
        var apply = Button(L("Im Editor öffnen"), () =>
        {
            if (devices.SelectedItem is not Aivana.Windows.CorsairKeyboardInfo device || keys.SelectedValue is not int key || actions.SelectedItem is not ActionPreset preset) return;
            StopCapture();
            if (!_controller.GetCorsairMacroKeys(device.DeviceId).Contains(key)) { status.Text = _controller.CorsairKeyStatus; return; }
            if (FindSpecialKeyConflict(device.DeviceId, key) is { } conflict) { status.Text = conflict; return; }
            ApplySpecialKeyDraft(device.DeviceId, key, preset);
            dialog.Close();
        }, true);
        panel.Children.Add(apply);
        panel.Children.Add(Button(L("Schließen"), dialog.Close, false));
        devices.SelectionChanged += (_, _) =>
        {
            StopCapture();
            var device = devices.SelectedItem as Aivana.Windows.CorsairKeyboardInfo;
            status.Text = _controller.CorsairKeyStatus;
            keys.ItemsSource = device is null ? [] : _controller.GetCorsairMacroKeys(device.DeviceId).Select(key => new CorsairKeyChoice(key, $"G{key}")).ToArray();
            keys.SelectedIndex = keys.Items.Count > 0 ? 0 : -1;
            listen.IsEnabled = _controller.CorsairKeysAvailable && keys.Items.Count > 0;
            apply.IsEnabled = keys.Items.Count > 0;
            UpdateBindingStatus();
        };
        listen.IsEnabled = apply.IsEnabled = false;
        if (devices.Items.Count > 0) devices.SelectedIndex = 0;
        dialog.Closed += (_, _) => StopCapture();
        return dialog;
    }

    private void ApplySpecialKeyDraft(string deviceId, int key, ActionPreset preset)
    {
        var profile = ActiveProfile ?? throw new InvalidOperationException();
        var steps = preset.CreateSteps();
        foreach (var step in steps.Where(step => step.Kind == ActionKind.SwitchProfile)) step.Value = "";
        var tile = new Tile { Name = preset.Name, Icon = preset.Icon, CorsairBinding = new CorsairBinding(deviceId, key), Steps = steps };
        _selectedTileId = tile.Id; _draftTile = tile; _draftProfileId = profile.Id; _selectedStep = 0;
        _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = profile.Id;
        ResetDraftHistory();
        _page = "Dashboard"; Refresh(forceRender: true);
    }

    private string? FindSpecialKeyConflict(string deviceId, int key)
    {
        var profile = ActiveProfile;
        if (profile is null) return null;
        var probe = new Tile { CorsairBinding = new CorsairBinding(deviceId, key) };
        return CorsairBindingValidator.ValidateProfileBindings(new Profile { Tiles = [.. profile.Tiles, probe] }).FirstOrDefault();
    }

    internal void RunSpecialKeyWizardSmokeChecks()
    {
        var draft = _draftTile; var selected = _selectedTileId; var profile = _draftProfileId;
        var baseline = _draftBaseline; var baselineTile = _draftBaselineTileId; var baselineProfile = _draftBaselineProfileId;
        var step = _selectedStep; var page = _page; var count = ActiveProfile!.Tiles.Count;
        try
        {
            var window = BuildSpecialKeyWizardWindow();
            var actions = FindKeyboardVisualChildren<ComboBox>(window).FirstOrDefault(combo => combo.Name == "WizardAction");
            // Inspect the logical content before the window is shown; no SDK capture or action execution.
            var panel = (StackPanel)((ScrollViewer)window.Content).Content;
            actions ??= panel.Children.OfType<ComboBox>().Single(combo => combo.Name == "WizardAction");
            if (actions.Items.Count != ActionCatalog.Presets.Count) throw new InvalidOperationException("Special-key wizard loses catalog actions.");
            var devicePicker = panel.Children.OfType<ComboBox>().Single(combo => combo.Name == "WizardDevice");
            var keyPicker = panel.Children.OfType<ComboBox>().Single(combo => combo.Name == "WizardKey");
            if (devicePicker.Items.Count == 0 && keyPicker.Items.Count != 0)
                throw new InvalidOperationException("Special-key wizard invents keys for an unsupported device.");
            var conflictTile = new Tile { CorsairBinding = new CorsairBinding("smoke-conflict", 3) };
            ActiveProfile.Tiles.Add(conflictTile);
            try
            {
                if (FindSpecialKeyConflict("SMOKE-CONFLICT", 3) is null || FindSpecialKeyConflict("smoke-conflict", 4) is not null)
                    throw new InvalidOperationException("Special-key wizard does not report duplicate bindings correctly.");
            }
            finally { ActiveProfile.Tiles.Remove(conflictTile); }
            ApplySpecialKeyDraft("smoke-device", 2, ActionCatalog.Presets[0]);
            if (_draftTile?.CorsairBinding != new CorsairBinding("smoke-device", 2) || ActiveProfile.Tiles.Count != count)
                throw new InvalidOperationException("Special-key wizard must create a bound draft without saving.");
        }
        finally
        {
            _draftTile = draft; _selectedTileId = selected; _draftProfileId = profile; _draftBaseline = baseline;
            _draftBaselineTileId = baselineTile; _draftBaselineProfileId = baselineProfile; _selectedStep = step; _page = page;
            ResetDraftHistory();
            Refresh(forceRender: true);
        }
    }
}

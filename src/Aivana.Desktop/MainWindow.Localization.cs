using Aivana.Core;
using System.Windows.Controls;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private sealed record LanguageOption(string Code, string Name);

    private static string L(string source) => Localization.Translate(source);
    private static string LF(FormattableString source) => Localization.Translate(source);

    private void LocalizeChrome()
    {
        SetNavigationContent(NavDashboard, L("◫   Dashboard"), "M1,1 H7 V7 H1 Z M11,1 H17 V7 H11 Z M1,11 H7 V17 H1 Z M11,11 H17 V17 H11 Z");
        SetNavigationContent(NavProfiles, L("▦   Profile"), "M1,4 H7 L9,7 H17 V17 H1 Z");
        SetNavigationContent(NavDevices, L("⌨   Geräte"), "M1,3 H17 V15 H1 Z M4,7 H6 M9,7 H11 M14,7 H15 M4,11 H14");
        SetNavigationContent(NavSettings, L("⚙   Einstellungen"), "M2,4 H16 M2,9 H16 M2,14 H16 M6,1 V7 M12,6 V12 M7,11 V17");
        SetNavigationContent(NavKeyboard, L("⌗   Tastatur"), "M1,3 H17 V15 H1 Z M4,7 H6 M8,7 H10 M12,7 H14 M4,11 H14");
        SidebarHotkeysTitle.Text = L("HOTKEYS");
        WorkspaceLabel.Text = L("ARBEITSPLATZ");
        BrandController.Text = "by Aivana";
        SearchBox.ToolTip = L("Aktionen nach Name, Hotkey oder Typ filtern");
        ProfileLabel.Text = L("Profil");
        SearchLabel.Text = L("Suche");
        PaletteButton.ToolTip = LF($"⌕  Befehlspalette  {(string.IsNullOrWhiteSpace(_controller.Settings.PaletteHotkey) ? "Ctrl+K" : _controller.Settings.PaletteHotkey)}");
        FooterBrand.Text = L("MAKRIVO  ·  AIVANA");
        PauseButton.Content = _controller.Paused ? L("▶  Hotkeys fortsetzen") : L("Ⅱ  Hotkeys pausieren");
        PaletteButton.Content = LF($"⌕  Befehlspalette  {(string.IsNullOrWhiteSpace(_controller.Settings.PaletteHotkey) ? "Ctrl+K" : _controller.Settings.PaletteHotkey)}");
        SidebarHotkeyState.Text = _controller.Paused ? L("Pausiert") : L("Aktiv");
        SidebarHotkeyDetail.Text = _controller.HasRunningActions ? L("Aktionen laufen gerade") : L("Lokale Verarbeitung");
        FooterStatus.Text = string.IsNullOrWhiteSpace(_controller.LastStatus) ? L("Bereit") : L(_controller.LastStatus);
    }

    private ComboBox BuildLanguagePicker()
    {
        var options = new List<LanguageOption>
        {
            new("auto", L("Automatisch (Windows-Sprache)"))
        };
        options.AddRange(Localization.SupportedLanguageSettings.Select(code => new LanguageOption(code, Localization.LanguageNames[code])));
        var picker = new ComboBox
        {
            ItemsSource = options,
            DisplayMemberPath = nameof(LanguageOption.Name),
            SelectedValuePath = nameof(LanguageOption.Code),
            SelectedValue = Localization.PreferredLanguageSetting(_controller.Settings.UiLanguage),
            MinWidth = 220,
            Margin = new System.Windows.Thickness(0, 2, 0, 8)
        };
        picker.SelectionChanged += (_, _) =>
        {
            if (_refreshing || picker.SelectedValue is not string language || language == _controller.Settings.UiLanguage) return;
            var prior = _controller.Settings.UiLanguage;
            try
            {
                _controller.Settings.UiLanguage = language;
                Localization.SetLanguage(language);
                _controller.Save();
                LocalizeChrome();
                _renderedPage = null;
                RenderPage(ActiveProfile);
            }
            catch (Exception ex)
            {
                _controller.Settings.UiLanguage = prior;
                Localization.SetLanguage(prior);
                ShowError(ex);
            }
        };
        return picker;
    }
}

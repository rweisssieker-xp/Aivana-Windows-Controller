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
        NavDashboard.Content = L("◫   Dashboard");
        NavProfiles.Content = L("▦   Profile");
        NavDevices.Content = L("⌨   Geräte");
        NavSettings.Content = L("⚙   Einstellungen");
        NavKeyboard.Content = L("⌗   Tastatur");
        SidebarHotkeysTitle.Text = L("HOTKEYS");
        WorkspaceLabel.Text = L("ARBEITSPLATZ");
        BrandController.Text = L("CONTROLLER");
        SearchBox.ToolTip = L("Aktionen nach Name, Hotkey oder Typ filtern");
        ProfileLabel.Text = L("Profil");
        SearchLabel.Text = L("Suche");
        PaletteButton.ToolTip = L("Alle Aktionen profileübergreifend suchen · Ctrl+K");
        FooterBrand.Text = L("AIVANA  ·  WINDOWS CONTROLLER");
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
        options.AddRange(Localization.SupportedLanguages.Select(code => new LanguageOption(code, Localization.LanguageNames[code])));
        var picker = new ComboBox
        {
            ItemsSource = options,
            DisplayMemberPath = nameof(LanguageOption.Name),
            SelectedValuePath = nameof(LanguageOption.Code),
            SelectedValue = _controller.Settings.UiLanguage,
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

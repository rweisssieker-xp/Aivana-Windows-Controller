using System.Globalization;
using System.Text.Json;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow : Window
{
    private readonly DesktopController _controller;
    private string _page = "Dashboard";
    private Guid? _selectedTileId;
    private Guid? _draftProfileId;
    private Tile? _draftTile;
    private int _selectedStep = -1;
    private bool _refreshing;
    private bool _capturing;
    private bool _wasPaused;
    private string? _editorError;
    private AppSettings? _observedSettings;
    private string? _profilePickerSignature;
    private string? _renderedPage;
    private bool _dashboardCompact;
    private bool _favoritesOnly;
    private Window? _paletteWindow;
    private TextBox? _paletteSearch;
    private ListBox? _paletteResults;
    private Guid? _tileDragId;
    private Point _tileDragStart;
    private readonly Brush _panel = Brush("#171E20");
    private readonly Brush _panel2 = Brush("#20292B");
    private readonly Brush _line = Brush("#2A3739");
    private readonly Brush _muted = Brush("#91A3A5");
    private readonly Brush _accent = Brush("#35D0C1");

    public MainWindow(DesktopController controller)
    {
        InitializeComponent();
        _controller = controller;
        _controller.Changed += Controller_Changed;
        _controller.PaletteRequested += Controller_PaletteRequested;
        InitializeKeyboardOverlay();
        InitializeExecutionFeedback();
        SizeChanged += MainWindow_SizeChanged;
        Closing += (_, e) =>
        {
            if (_capturing) FinishCapture();
            if (HasUnsavedChanges && !ResolvePendingDraft()) { e.Cancel = true; return; }
            _paletteWindow?.Close();
        };
        Closed += (_, _) => { _controller.PaletteRequested -= Controller_PaletteRequested; DisposeKeyboardOverlay(); DisposeExecutionFeedback(); };
        ProfilePicker.LostKeyboardFocus += (_, _) => Dispatcher.BeginInvoke(new Action(Refresh), System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        Refresh();
    }

    private int _editorBackgroundWorkCount;

    public void Refresh() => Refresh(forceRender: false);

    private void Refresh(bool forceRender)
    {
        if (!Dispatcher.CheckAccess()) { Dispatcher.BeginInvoke(new Action(Refresh)); return; }
        _refreshing = true;
        var settings = _controller.Settings;
        Aivana.Core.Localization.SetLanguage(settings.UiLanguage);
        LocalizeChrome();
        if (_observedSettings != null && !ReferenceEquals(_observedSettings, settings)) { _selectedTileId = null; _draftTile = null; _draftProfileId = null; _selectedStep = -1; }
        _observedSettings = settings;
        var profiles = settings.Profiles ?? [];
        var active = profiles.FirstOrDefault(p => p.Id == settings.ActiveProfileId) ?? profiles.FirstOrDefault();
        if (_selectedTileId is { } id && _draftProfileId == active?.Id && (active?.Tiles.All(t => t.Id != id) ?? true) && !HasUnsavedChanges) { _selectedTileId = null; _draftTile = null; _draftProfileId = null; }
        if (_selectedTileId is { } selectedId && _draftTile == null && _draftProfileId == active?.Id && active?.Tiles.FirstOrDefault(t => t.Id == selectedId) is { } source)
            _draftTile = Clone(source);
        var profileSignature = string.Join("|", profiles.Select(p => $"{p.Id:N}:{p.Name}"));
        if (_profilePickerSignature != profileSignature && !ProfilePicker.IsKeyboardFocusWithin)
        {
            ProfilePicker.ItemsSource = null;
            ProfilePicker.ItemsSource = profiles;
            ProfilePicker.DisplayMemberPath = "Name";
            ProfilePicker.SelectedValuePath = "Id";
            _profilePickerSignature = profileSignature;
        }
        if (!ProfilePicker.IsKeyboardFocusWithin && !Equals(ProfilePicker.SelectedValue, active?.Id))
            ProfilePicker.SelectedValue = active?.Id;
        PauseButton.Content = _controller.Paused ? L("▶  Hotkeys fortsetzen") : L("Ⅱ  Hotkeys pausieren");
        PaletteButton.Content = LF($"⌕  Befehlspalette  {(string.IsNullOrWhiteSpace(settings.PaletteHotkey) ? "Ctrl+K" : settings.PaletteHotkey)}");
        SidebarHotkeyState.Text = _controller.Paused ? L("Pausiert") : L("Aktiv");
        SidebarHotkeyState.Foreground = _controller.Paused ? Brush("#F2B66D") : Brush("#69D8CC");
        SidebarHotkeyDetail.Text = _controller.HasRunningActions ? L("Aktionen laufen gerade") : L("Lokale Verarbeitung");
        var globalError = _controller.HotkeyErrors.GetValueOrDefault(Guid.Empty);
        FooterStatus.Text = !string.IsNullOrWhiteSpace(globalError) ? globalError
            : !string.IsNullOrWhiteSpace(_controller.LastStatus) ? _controller.LastStatus
            : LF($"{active?.Name ?? L("Kein Profil")}  ·  {active?.Tiles.Count(t => t.Enabled) ?? 0} aktive Aktionen");
        UpdateNavigationState();
        var editing = Keyboard.FocusedElement is TextBox or ComboBox or PasswordBox or Slider || _editorBackgroundWorkCount > 0;
        if (!_capturing && (forceRender || !editing))
            RenderPage(active);
        _refreshing = false;
    }

    private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var compact = ActualWidth < 1180;
        if (compact == _dashboardCompact) return;
        _dashboardCompact = compact;
        if (!_refreshing && !_capturing && Keyboard.FocusedElement is not TextBox and not ComboBox)
            RenderPage(ActiveProfile);
    }

    private void UpdateNavigationState()
    {
        foreach (var (button, page) in new[]
        {
            (NavDashboard, "Dashboard"), (NavProfiles, "Profile"), (NavDevices, "Geräte"),
            (NavSettings, "Einstellungen"), (NavKeyboard, "Tastatur")
        })
        {
            var active = _page == page;
            button.Background = active ? Brush("#203A39") : Brushes.Transparent;
            button.BorderBrush = active ? Brush("#28534F") : Brushes.Transparent;
            button.Foreground = active ? Brush("#67E0D4") : Brush("#C4D0D1");
            button.FontWeight = active ? FontWeights.SemiBold : FontWeights.Normal;
        }
    }

    public void NavigateToSection(string section)
    {
        if (section is not ("Dashboard" or "Profile" or "Geräte" or "Einstellungen" or "Tastatur"))
            throw new ArgumentOutOfRangeException(nameof(section), section, "Unbekannter Bereich.");
        SwitchPage(section);
    }

    public void RunUiSmokeChecks()
    {
        if (!Dispatcher.CheckAccess()) throw new InvalidOperationException("UI-Smokechecks müssen auf dem WPF-UI-Thread laufen.");
        RunPaletteSearchSmokeChecks();
        var profile = ActiveProfile ?? throw new InvalidOperationException("Kein aktives Profil vorhanden.");
        var originalPage = _page;
        var originalCount = profile.Tiles.Count;
        var temporaryIds = new List<Guid>();
        try
        {
            _page = "Dashboard";
            AddTile(profile);
            var first = _draftTile ?? throw new InvalidOperationException("Testkachel wurde nicht angelegt.");
            if (profile.Tiles.Count != originalCount || !HasUnsavedChanges) throw new InvalidOperationException("Neue Kachel wurde nicht als Entwurf angelegt.");
            if (!CommitDraft()) throw new InvalidOperationException("Neue Testkachel ließ sich nicht speichern: " + _editorError);
            temporaryIds.Add(first.Id);
            if (profile.Tiles.Count != originalCount + 1) throw new InvalidOperationException("Kachel wurde nicht angelegt.");
            first.Favorite = true;
            if (!CommitDraft()) throw new InvalidOperationException("Favorit ließ sich nicht speichern: " + _editorError);
            first = profile.Tiles.First(t => t.Id == first.Id);

            DuplicateTile(first);
            var duplicate = _draftTile ?? throw new InvalidOperationException("Testkachel wurde nicht dupliziert.");
            if (!duplicate.Favorite) throw new InvalidOperationException("Duplikat hat den Favoritenstatus verloren.");
            if (profile.Tiles.Count != originalCount + 1 || !HasUnsavedChanges) throw new InvalidOperationException("Duplikat wurde nicht als Entwurf angelegt.");
            if (!CommitDraft()) throw new InvalidOperationException("Duplikat ließ sich nicht speichern: " + _editorError);
            temporaryIds.Add(duplicate.Id);
            if (profile.Tiles.Count != originalCount + 2 || duplicate.Hotkey.Length != 0)
                throw new InvalidOperationException("Duplikat wurde nicht konfliktfrei angelegt.");

            MoveTileTo(first, duplicate.Id);
            if (profile.Tiles.FindIndex(t => t.Id == first.Id) <= profile.Tiles.FindIndex(t => t.Id == duplicate.Id))
                throw new InvalidOperationException($"Kacheln ließen sich nicht neu anordnen ({profile.Tiles.FindIndex(t => t.Id == first.Id)} / {profile.Tiles.FindIndex(t => t.Id == duplicate.Id)}).");
            MoveTileTo(duplicate, first.Id);
            if (profile.Tiles.FindIndex(t => t.Id == duplicate.Id) <= profile.Tiles.FindIndex(t => t.Id == first.Id))
                throw new InvalidOperationException("Kachelreihenfolge ließ sich nicht zurücksetzen.");

            DeleteTile(new Tile { Id = first.Id });
            temporaryIds.Remove(first.Id);
            if (profile.Tiles.Count != originalCount + 1) throw new InvalidOperationException("Testkachel wurde nicht entfernt.");

            _selectedTileId = duplicate.Id; _draftTile = Clone(duplicate); _draftProfileId = profile.Id; _selectedStep = 0;
            _draftTile.Name = "UI Smoke Entwurf";
            Refresh();
            if (SelectedTile?.Name != "UI Smoke Entwurf") throw new InvalidOperationException("Refresh hat den Editorentwurf verworfen.");
            if (!CommitDraft()) throw new InvalidOperationException("Testkachel ließ sich nicht speichern: " + _editorError);
            duplicate = profile.Tiles.First(t => t.Id == duplicate.Id);
            duplicate.Steps[0].Kind = ActionKind.Text;
            duplicate.Steps[0].Value = "SMOKE SAFE";
            _draftTile = Clone(duplicate);
            if (!CommitDraft()) throw new InvalidOperationException("Testaktion ließ sich nicht speichern: " + _editorError);

            AddActionTemplate(profile, ActionKind.Text);
            var template = _draftTile ?? throw new InvalidOperationException("Textvorlage wurde nicht angelegt.");
            if (template.Steps.Count != 1 || template.Steps[0].Kind != ActionKind.Text)
                throw new InvalidOperationException("Die Textvorlage hat keinen passenden Aktionsschritt.");
            if (profile.Tiles.Count != originalCount + 1 || !HasUnsavedChanges) throw new InvalidOperationException("Vorlage wurde nicht als Entwurf angelegt.");
            if (!CommitDraft()) throw new InvalidOperationException("Textvorlage ließ sich nicht speichern: " + _editorError);
            temporaryIds.Add(template.Id);
            template.Name = "UI Smoke Vorlage";
            if (!CommitDraft()) throw new InvalidOperationException("Vorlage ließ sich nicht bearbeiten: " + _editorError);
            RunEditorSmokeChecks();
            RunKeyboardSmokeChecks();

            foreach (var section in new[] { "Dashboard", "Profile", "Geräte", "Einstellungen", "Tastatur" })
            {
                _page = section;
                RenderPage(ActiveProfile);
                UpdateLayout();
            }
            _controller.SelectProfile(profile.Id);
            _selectedTileId = null; _draftTile = null; _draftProfileId = null; _selectedStep = -1;
        }
        finally
        {
            profile.Tiles.RemoveAll(t => temporaryIds.Contains(t.Id));
            _selectedTileId = null; _draftTile = null; _draftProfileId = null; _selectedStep = -1;
            _controller.Save();
            _page = originalPage;
            RenderPage(ActiveProfile);
            UpdateLayout();
        }
    }

    private static void RunPaletteSearchSmokeChecks()
    {
        var favoriteProfile = new Profile { Name = "Design" };
        var otherProfile = new Profile { Name = "Browser" };
        var favorite = new PaletteEntry(new Tile { Name = "Figma Entwurf", Favorite = true, Hotkey = "Ctrl+Alt+G", Steps = [new ActionStep { Kind = ActionKind.Url }] }, favoriteProfile, "Webseite öffnen", true, "Ctrl+Alt+G", "BEREIT", true);
        var other = new PaletteEntry(new Tile { Name = "Browser öffnen", Hotkey = "Ctrl+B", Steps = [new ActionStep { Kind = ActionKind.Launch }] }, otherProfile, "Programm starten", false, "Ctrl+B", "BEREIT", true);
        var all = new[] { other, favorite };
        if (FilterPaletteItems(all, "").FirstOrDefault() != favorite) throw new InvalidOperationException("Befehlspalette zeigt Favoriten nicht zuerst.");
        if (FilterPaletteItems(all, "design alt+g").Count != 1 || FilterPaletteItems(all, "design alt+g")[0] != favorite)
            throw new InvalidOperationException("Mehrwortsuche der Befehlspalette stimmt nicht.");
        if (FilterPaletteItems(all, "kein treffer").Count != 0) throw new InvalidOperationException("Befehlspalette filtert unbekannte Begriffe nicht heraus.");
    }

    private void Controller_Changed() => Refresh();

    private void Controller_PaletteRequested() => ShowCommandPalette();

    public void ShowCommandPalette()
    {
        if (!Dispatcher.CheckAccess()) { Dispatcher.BeginInvoke(new Action(ShowCommandPalette)); return; }
        if (_paletteWindow is { IsVisible: true })
        {
            _paletteWindow.Activate(); _paletteSearch?.Focus();
            return;
        }

        var window = new Window
        {
            Title = L("Befehlspalette"), Width = 760, Height = 630, MinWidth = 600, MinHeight = 470,
            WindowStyle = WindowStyle.None, ResizeMode = ResizeMode.NoResize, AllowsTransparency = true,
            Background = Brushes.Transparent, ShowInTaskbar = false, Topmost = true,
            WindowStartupLocation = Visibility == Visibility.Visible && IsLoaded ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen
        };
        window.Resources.MergedDictionaries.Add(Resources);
        if (Visibility == Visibility.Visible && IsLoaded) window.Owner = this;

        var frame = new Border { Background = Brush("#111719"), BorderBrush = Brush("#426361"), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(18), Padding = new Thickness(20), Effect = new System.Windows.Media.Effects.DropShadowEffect { BlurRadius = 28, ShadowDepth = 5, Opacity = .42, Color = Colors.Black } };
        var layout = new Grid();
        layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        layout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var heading = new DockPanel { Margin = new Thickness(2, 0, 2, 13) };
        var close = Button(L("Esc  ×"), () => window.Close(), false);
        close.Padding = new Thickness(10, 5, 10, 5); DockPanel.SetDock(close, Dock.Right); heading.Children.Add(close);
        var titleStack = new StackPanel();
        titleStack.Children.Add(Label(L("BEFEHLSPALETTE"), 10, "#67D8CC", true));
        titleStack.Children.Add(Label(L("Alle Funktionen"), 17, "#E8F0F1", true, new Thickness(0, 4, 0, 0)));
        titleStack.Children.Add(Label(string.IsNullOrWhiteSpace(_controller.ForegroundProcess)
            ? L("Für Texte und Fensteraktionen: Palette aus dem gewünschten Programm öffnen.")
            : LF($"Zielfenster: {_controller.ForegroundProcess} · {Trim(_controller.ForegroundWindowTitle, 64)}"),
            10, "#91A3A5", false, new Thickness(0, 5, 0, 0)));
        titleStack.MouseLeftButtonDown += (_, e) => { if (e.ButtonState == MouseButtonState.Pressed) window.DragMove(); };
        if (_controller.Paused) titleStack.Children.Add(Label(L("Hotkeys sind pausiert. Aktionen können über die Palette nicht gestartet werden."), 10, "#F2B66D", false, new Thickness(0, 4, 0, 0)));
        heading.Children.Add(titleStack); layout.Children.Add(heading);

        var search = new TextBox { FontSize = 17, Padding = new Thickness(13, 11, 13, 11), Margin = new Thickness(0, 0, 0, 11), ToolTip = L("Aktion, Hotkey, Typ oder Profil suchen") };
        search.Name = "PaletteSearch";
        var searchRow = new DockPanel();
        var filter = new ComboBox { Name = "PaletteFilter", Width = 155, Margin = new Thickness(0, 0, 9, 11),
            ItemsSource = new[] { L("Alle Funktionen"), L("AKTION"), L("Aktionskatalog"), "Aivana" }, SelectedIndex = 0 };
        DockPanel.SetDock(filter, Dock.Left); searchRow.Children.Add(filter); searchRow.Children.Add(search);
        Grid.SetRow(searchRow, 1); layout.Children.Add(searchRow);
        var list = new ListBox { BorderThickness = new Thickness(0), Background = Brushes.Transparent, Padding = new Thickness(0, 2, 0, 4) };
        list.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
        list.ItemTemplate = PaletteEntryTemplate();
        Grid.SetRow(list, 2); layout.Children.Add(list);

        var footer = new Grid { Margin = new Thickness(0, 13, 0, 0) };
        footer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        var selectedInfo = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
        selectedInfo.Children.Add(Label(L("Wähle eine Aktion aus."), 11, "#A5B6B7", true));
        var hint = Label(L("↑↓ navigieren   ·   Enter starten   ·   Esc schließen"), 10, "#718284"); hint.Margin = new Thickness(0, 3, 0, 0); selectedInfo.Children.Add(hint);
        footer.Children.Add(selectedInfo);
        var actions = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
        var profileButton = Button(L("Profil öffnen"), () => { if (list.SelectedItem is PaletteEntry entry) OpenPaletteEntry(entry, false); }, false, 0);
        var editButton = Button(L("Bearbeiten"), () => { if (list.SelectedItem is PaletteEntry entry) OpenPaletteEntry(entry, true); }, false, 7);
        var runButton = ButtonAsync(L("▶  Starten"), async () =>
        {
            if (list.SelectedItem is not PaletteEntry entry || !entry.CanRun) return;
            window.Close();
            await ExecutePaletteEntryAsync(entry);
        }, true, 7);
        actions.Children.Add(profileButton); actions.Children.Add(editButton); actions.Children.Add(runButton);
        Grid.SetColumn(actions, 1); footer.Children.Add(actions);
        Grid.SetRow(footer, 3); layout.Children.Add(footer);
        frame.Child = layout; window.Content = frame;

        void RefreshResults()
        {
            var entries = BuildPaletteEntries();
            var visible = FilterPaletteItems(entries.Where(entry => filter.SelectedIndex switch
            {
                1 => entry.Command == null && entry.Preset == null,
                2 => entry.Preset != null,
                3 => entry.Command != null,
                _ => true
            }), search.Text);
            list.ItemsSource = visible;
            if (visible.Count > 0) list.SelectedIndex = 0;
            else selectedInfo.Children[0] = Label(L("Keine passende Aktion gefunden."), 11, "#A5B6B7", true);
            UpdateSelection();
        }
        void UpdateSelection()
        {
            var entry = list.SelectedItem as PaletteEntry;
            selectedInfo.Children.Clear();
            selectedInfo.Children.Add(Label(entry == null ? L("Keine Aktion gefunden.") : LF($"{entry.Profile.Name}  ·  {entry.Tile.Name}"), 11, "#DCE6E7", true));
            selectedInfo.Children.Add(Label(entry?.Preview ?? L("Passe den Suchbegriff an."), 10, "#829395", false, new Thickness(0, 3, 0, 0)));
            selectedInfo.Children.Add(Label(entry?.State ?? "", 9, entry?.CanRun == true ? "#67D8CC" : "#F2B66D", false, new Thickness(0, 3, 0, 0)));
            runButton.IsEnabled = entry?.CanRun == true;
            runButton.Content = entry?.NeedsConfiguration == true ? L("Funktion auswählen") : L("▶  Starten");
            profileButton.IsEnabled = entry?.Command == null && entry?.Preset == null && entry != null;
            editButton.IsEnabled = entry?.Command == null && entry != null;
        }
        search.TextChanged += (_, _) => RefreshResults();
        filter.SelectionChanged += (_, _) => RefreshResults();
        list.SelectionChanged += (_, _) => UpdateSelection();
        list.MouseDoubleClick += async (_, _) =>
        {
            if (list.SelectedItem is not PaletteEntry entry || !entry.CanRun) return;
            window.Close(); await ExecutePaletteEntryAsync(entry);
        };
        window.PreviewKeyDown += (_, e) =>
        {
            if (e.Key == Key.Escape) { window.Close(); e.Handled = true; return; }
            if (e.Key is Key.Down or Key.Up && list.Items.Count > 0)
            {
                var next = Math.Clamp(list.SelectedIndex + (e.Key == Key.Down ? 1 : -1), 0, list.Items.Count - 1);
                list.SelectedIndex = next; list.ScrollIntoView(list.SelectedItem); e.Handled = true; return;
            }
            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control && list.SelectedItem is PaletteEntry entry && entry.Command == null) OpenPaletteEntry(entry, true);
                else if (list.SelectedItem is PaletteEntry runEntry && runEntry.CanRun) runButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                e.Handled = true;
            }
        };
        window.Closed += (_, _) => { if (ReferenceEquals(_paletteWindow, window)) { _paletteWindow = null; _paletteSearch = null; _paletteResults = null; } };
        _paletteWindow = window; _paletteSearch = search; _paletteResults = list;
        RefreshResults(); window.Show(); window.Activate(); search.Focus();
    }

    private List<PaletteEntry> BuildPaletteEntries()
    {
        var entries = new List<PaletteEntry>();
        foreach (var profile in _controller.Settings.Profiles)
        foreach (var tile in profile.Tiles)
        {
            var conflict = _controller.HotkeyErrors.GetValueOrDefault(tile.Id);
            var preview = string.Join(Environment.NewLine, MacroPreview.Build(tile));
            var canRun = tile.Enabled && !_controller.Paused && string.IsNullOrWhiteSpace(conflict);
            var state = !string.IsNullOrWhiteSpace(conflict) ? LF($"KONFLIKT  ·  {conflict}") : !tile.Enabled ? L("DEAKTIVIERT") : _controller.Paused ? L("PAUSIERT · Hotkeys sind pausiert") : L("BEREIT");
            entries.Add(new PaletteEntry(tile, profile, preview, tile.Favorite, tile.Hotkey, state, canRun));
        }
        AddPaletteCommands(entries);
        return entries.OrderByDescending(x => x.Favorite).ThenBy(x => x.Command != null || x.Preset != null).ThenBy(x => x.Tile.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
    }

    internal static List<PaletteEntry> FilterPaletteItems(IEnumerable<PaletteEntry> items, string? query)
    {
        var terms = (query ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return items.Where(entry => terms.All(term => entry.Tile.Name.Contains(term, StringComparison.CurrentCultureIgnoreCase)
            || entry.Hotkey.Contains(term, StringComparison.CurrentCultureIgnoreCase)
            || entry.Profile.Name.Contains(term, StringComparison.CurrentCultureIgnoreCase)
            || entry.Preview.Contains(term, StringComparison.CurrentCultureIgnoreCase)
            || entry.SearchTerms.Contains(term, StringComparison.CurrentCultureIgnoreCase)
            || entry.State.Contains(term, StringComparison.CurrentCultureIgnoreCase)))
            .OrderByDescending(entry => entry.Favorite).ThenBy(entry => entry.Profile.Name, StringComparer.CurrentCultureIgnoreCase)
            .ThenBy(entry => entry.Tile.Name, StringComparer.CurrentCultureIgnoreCase).ToList();
    }

    private DataTemplate PaletteEntryTemplate()
    {
        var template = new DataTemplate(typeof(PaletteEntry));
        var border = new FrameworkElementFactory(typeof(Border)); border.SetValue(Border.BackgroundProperty, Brush("#171F21")); border.SetValue(Border.BorderBrushProperty, Brush("#2A3B3D")); border.SetValue(Border.BorderThicknessProperty, new Thickness(1)); border.SetValue(Border.CornerRadiusProperty, new CornerRadius(10)); border.SetValue(Border.PaddingProperty, new Thickness(12, 9, 12, 9)); border.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 0, 6));
        var grid = new FrameworkElementFactory(typeof(DockPanel));
        var left = new FrameworkElementFactory(typeof(StackPanel));
        var title = new FrameworkElementFactory(typeof(TextBlock)); title.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Tile.Name")); title.SetValue(TextBlock.FontSizeProperty, 14d); title.SetValue(TextBlock.FontWeightProperty, FontWeights.SemiBold); title.SetValue(TextBlock.ForegroundProperty, Brush("#E8F0F1")); left.AppendChild(title);
        var meta = new FrameworkElementFactory(typeof(TextBlock)); meta.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Meta")); meta.SetValue(TextBlock.FontSizeProperty, 10d); meta.SetValue(TextBlock.ForegroundProperty, Brush("#91A3A5")); meta.SetValue(TextBlock.MarginProperty, new Thickness(0, 3, 0, 0)); left.AppendChild(meta);
        var right = new FrameworkElementFactory(typeof(StackPanel)); right.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Right);
        var badge = new FrameworkElementFactory(typeof(TextBlock)); badge.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Badge")); badge.SetValue(TextBlock.FontSizeProperty, 9d); badge.SetValue(TextBlock.FontWeightProperty, FontWeights.SemiBold); badge.SetValue(TextBlock.ForegroundProperty, Brush("#67D8CC")); badge.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right); right.AppendChild(badge);
        var state = new FrameworkElementFactory(typeof(TextBlock)); state.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("State")); state.SetValue(TextBlock.FontSizeProperty, 9d); state.SetValue(TextBlock.ForegroundProperty, Brush("#829395")); state.SetValue(TextBlock.MarginProperty, new Thickness(0, 4, 0, 0)); state.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right); right.AppendChild(state);
        right.SetValue(DockPanel.DockProperty, Dock.Right); grid.AppendChild(right); grid.AppendChild(left); border.AppendChild(grid); template.VisualTree = border; return template;
    }

    private void OpenPaletteEntry(PaletteEntry entry, bool edit)
    {
        _paletteWindow?.Close();
        if (entry.Preset != null) { CreatePalettePreset(entry.Preset); return; }
        if (!ResolvePendingDraft()) return;
        try
        {
            if (_controller.Settings.ActiveProfileId != entry.Profile.Id) _controller.SelectProfile(entry.Profile.Id);
            _page = "Dashboard";
            if (edit) EditTile(entry.Tile.Id); else { _selectedTileId = null; _draftTile = null; _draftProfileId = null; }
            RenderPage(ActiveProfile); Show(); WindowState = WindowState.Normal; Activate();
        }
        catch (Exception ex) { ShowError(ex); }
    }

    private void PaletteButton_Click(object sender, RoutedEventArgs e) => _controller.RequestPalette();

    private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (_capturing) return;
        if ((Keyboard.Modifiers & ModifierKeys.Control) != 0 && e.Key == Key.K)
        { _controller.RequestPalette(); e.Handled = true; }
        else if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.Space)
        { _controller.RequestPalette(); e.Handled = true; }
    }

    internal sealed record PaletteEntry(Tile Tile, Profile Profile, string Preview, bool Favorite, string Hotkey, string State, bool CanRun,
        Action? Command = null, ActionPreset? Preset = null, string SearchTerms = "")
    {
        public bool NeedsConfiguration => Preset != null && (Preset.IsWorkflow || Preset.Id.StartsWith("custom.", StringComparison.Ordinal)
            || ConfigurationValidator.ValidateTile(Tile).Count > 0);
        public string Meta => Command != null ? Profile.Name : string.Join("  ·  ", new[] { Profile.Name, string.IsNullOrWhiteSpace(Hotkey) ? ActionName(Tile.Steps.FirstOrDefault()?.Kind ?? ActionKind.Launch) : Hotkey });
        public string Badge => Preset != null ? L("Aktionskatalog") : Command != null ? "Aivana" : Favorite ? L("★ FAVORIT") : L("AKTION");
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!_refreshing && _page == "Dashboard") RenderPage(ActiveProfile);
    }

    private Profile? ActiveProfile => _controller.Settings.Profiles.FirstOrDefault(p => p.Id == _controller.Settings.ActiveProfileId)
        ?? _controller.Settings.Profiles.FirstOrDefault();
    private Tile? SelectedTile => _draftProfileId == ActiveProfile?.Id ? _draftTile : null;

    private void RenderPage(Profile? profile)
    {
        var scrollPositions = _renderedPage == _page ? FindScrollViewers(PageHost.Content as DependencyObject).Select(s => s.VerticalOffset).ToArray() : [];
        PageHost.Content = _page switch
        {
            "Profile" => BuildProfilesPage(),
            "Geräte" => BuildDevicesPage(),
            "Einstellungen" => BuildSettingsPage(),
            "Tastatur" => BuildKeyboardPage(profile),
            _ => BuildDashboard(profile)
        };
        _renderedPage = _page;
        if (scrollPositions.Length > 0)
            Dispatcher.BeginInvoke(new Action(() => FindScrollViewers(PageHost.Content as DependencyObject).Take(scrollPositions.Length).Select((scroll, i) => (scroll, i)).ToList().ForEach(x => x.scroll.ScrollToVerticalOffset(scrollPositions[x.i]))), System.Windows.Threading.DispatcherPriority.Loaded);
        var title = _page == "Profile" ? "Profile" : _page;
        PageTitle.Text = L(title);
        PageSubtitle.Text = L(_page switch
        {
            "Dashboard" => "Deine Aktionen und Belegungen auf einen Blick",
            "Profile" => "Arbeitsbereiche, Priorität und automatische Zuordnung",
            "Geräte" => "Erkannte Tastaturen und verfügbare Herstellerfunktionen",
            "Einstellungen" => "Startverhalten, Import und lokale Steuerung",
            "Tastatur" => "Tastenpositionen und Belegungen im gewählten Layout",
            _ => ""
        });
        UpdateNavigationState();
    }

    private UIElement BuildDashboard(Profile? profile)
    {
        var total = profile?.Tiles.Count ?? 0;
        var enabled = profile?.Tiles.Count(t => t.Enabled) ?? 0;
        var assigned = profile?.Tiles.Count(t => t.Enabled && !string.IsNullOrWhiteSpace(t.Hotkey)) ?? 0;
        var running = profile?.Tiles.Count(t => _controller.IsRunning(t.Id)) ?? 0;
        var summary = new UniformGrid { Columns = 4, Margin = new Thickness(0, 0, 0, 14) };
        summary.Children.Add(SummaryCard(L("AKTIONEN"), total.ToString(), L("in diesem Profil")));
        summary.Children.Add(SummaryCard(L("AKTIV"), enabled.ToString(), L("einsatzbereit")));
        summary.Children.Add(SummaryCard(L("BELEGT"), assigned.ToString(), L("mit Hotkey")));
        summary.Children.Add(SummaryCard(L("LÄUFT"), running.ToString(), running > 0 ? L("wird ausgeführt") : L("gerade nichts")));

        var tileArea = BuildTileArea(profile);
        var editor = BuildEditor(profile);
        if (ActualWidth < 1180)
        {
            var content = new StackPanel();
            content.Children.Add(summary);
            content.Children.Add(tileArea);
            editor.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 12, 0, 0));
            content.Children.Add(editor);
            content.Children.Add(BuildActivityStrip());
            return new ScrollViewer { Content = content, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled };
        }

        var root = new Grid();
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star), MinWidth = 430 });
        root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(400), MinWidth = 360 });
        Grid.SetColumnSpan(summary, 2); root.Children.Add(summary);
        Grid.SetRow(tileArea, 1); tileArea.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 14, 0)); root.Children.Add(tileArea);
        Grid.SetRow(editor, 1); Grid.SetColumn(editor, 1); root.Children.Add(editor);
        var activity = BuildActivityStrip(); Grid.SetRow(activity, 2); Grid.SetColumnSpan(activity, 2); root.Children.Add(activity);
        return root;
    }

    private Border SummaryCard(string title, string value, string detail)
    {
        var card = Card(); card.Padding = new Thickness(12, 10, 12, 10); card.Margin = new Thickness(0, 0, 8, 0);
        var content = new StackPanel(); content.Children.Add(Label(title, 9, "#829597", true));
        var row = new DockPanel { Margin = new Thickness(0, 4, 0, 0) };
        var number = Label(value, 20, "#E8F0F1", true); row.Children.Add(number);
        var note = Label(detail, 10, "#829597"); note.VerticalAlignment = VerticalAlignment.Bottom; note.Margin = new Thickness(7, 0, 0, 3); row.Children.Add(note);
        content.Children.Add(row); card.Child = content; return card;
    }

    private UIElement BuildTileArea(Profile? profile)
    {
        var root = new Grid(); root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        var header = new DockPanel { Margin = new Thickness(0, 0, 0, 10) };
        var toolbar = new StackPanel { Orientation = Orientation.Horizontal };
        var favoriteCount = profile?.Tiles.Count(t => t.Favorite) ?? 0;
        var favoriteFilter = Button(_favoritesOnly ? LF($"★ Favoriten ({favoriteCount})") : LF($"☆ Favoriten ({favoriteCount})"), () => { _favoritesOnly = !_favoritesOnly; RenderPage(ActiveProfile); }, _favoritesOnly, 0);
        favoriteFilter.ToolTip = L("Dashboard auf gespeicherte Favoriten einschränken");
        toolbar.Children.Add(favoriteFilter);
        toolbar.Children.Add(AddActionButton(profile));
        toolbar.Children.Add(Button(L("⇧ Import"), ImportProfiles, false, 7));
        toolbar.Children.Add(Button(L("⇩ Export"), () => _controller.Export(), false, 7));
        DockPanel.SetDock(toolbar, Dock.Right); header.Children.Add(toolbar);
        header.Children.Add(Label(L("AKTIONSKACHELN"), 10, "#91A3A5", true)); root.Children.Add(header);
        var filtered = (profile?.Tiles ?? []).Where(tile => (!_favoritesOnly || tile.Favorite) && (string.IsNullOrWhiteSpace(SearchBox.Text.Trim())
            || tile.Name.Contains(SearchBox.Text.Trim(), StringComparison.OrdinalIgnoreCase)
            || tile.Hotkey.Contains(SearchBox.Text.Trim(), StringComparison.OrdinalIgnoreCase)
            || tile.Steps.Any(step => ActionName(step.Kind).Contains(SearchBox.Text.Trim(), StringComparison.OrdinalIgnoreCase)))).ToList();
        UIElement items;
        if (filtered.Count == 0)
        {
            var empty = Card(); empty.Margin = new Thickness(0, 4, 12, 8); empty.Padding = new Thickness(18);
            var emptyContent = new StackPanel(); emptyContent.Children.Add(Label(_favoritesOnly && string.IsNullOrWhiteSpace(SearchBox.Text) ? L("Noch keine Favoriten") : string.IsNullOrWhiteSpace(SearchBox.Text) ? L("Dein Profil ist noch leer") : L("Keine Aktion gefunden"), 16, "#E8F0F1", true));
            emptyContent.Children.Add(Label(_favoritesOnly && string.IsNullOrWhiteSpace(SearchBox.Text) ? L("Markiere eine Aktion mit dem Stern, damit du sie hier schnell wiederfindest.") : string.IsNullOrWhiteSpace(SearchBox.Text) ? L("Lege eine Aktion an. Wähle einen passenden Startpunkt wie Programm, Webseite, Text oder Mediensteuerung.") : L("Ändere den Suchbegriff, um weitere Aktionen anzuzeigen."), 11, "#91A3A5", false, new Thickness(0, 6, 0, 12)));
            if (string.IsNullOrWhiteSpace(SearchBox.Text) && !_favoritesOnly) emptyContent.Children.Add(AddActionButton(profile));
            empty.Child = emptyContent; items = empty;
        }
        else
        {
            var wrap = new WrapPanel { Orientation = Orientation.Horizontal };
            foreach (var tile in filtered) wrap.Children.Add(BuildTileCard(tile));
            items = new ScrollViewer { Content = wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled };
        }
        Grid.SetRow(items, 1); root.Children.Add(items); return root;
    }

    private Button AddActionButton(Profile? profile)
    {
        return Button(L("＋ Aktion"), () => ShowActionCatalog(profile), true);
    }

    private void AddMacroTemplate(Profile? profile)
    {
        if (profile == null || !ResolvePendingDraft()) return;
        var tile = new Tile { Name = L("Neues Makro"), Icon = "⌘", Steps = [new ActionStep { Kind = ActionKind.Delay, DelayMs = 100 }] };
        _selectedTileId = tile.Id; _draftTile = Clone(tile); _draftProfileId = profile.Id; _selectedStep = 0;
        _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = profile.Id; Refresh();
    }

    private void AddActionTemplate(Profile? profile, ActionKind kind)
    {
        if (profile == null || !ResolvePendingDraft()) return;
        var step = new ActionStep { Kind = kind, DelayMs = 100, Value = kind switch
        {
            ActionKind.Launch => "notepad.exe", ActionKind.OpenPath => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ActionKind.Url => "https://example.com", ActionKind.Shortcut => "Ctrl+Alt+K", ActionKind.Media => "PlayPause",
            ActionKind.Volume => "Up", ActionKind.CorsairColor => "#35D0C1",
            ActionKind.Clipboard => "Stand: {{datetime}}", ActionKind.Window => "left", ActionKind.System => "lock",
            ActionKind.SwitchProfile => profile.Id.ToString(), _ => ""
        }};
        var tile = new Tile { Name = ActionName(kind), Icon = kind switch { ActionKind.Launch => "▣", ActionKind.Url => "↗", ActionKind.Text => "T", ActionKind.Media => "♫", ActionKind.Shortcut => "⌘", ActionKind.Clipboard => "▤", ActionKind.Window => "▱", ActionKind.System => "⏻", _ => "▶" }, Steps = [step] };
        _selectedTileId = tile.Id; _draftTile = Clone(tile); _draftProfileId = profile.Id; _selectedStep = 0;
        _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = profile.Id; Refresh();
    }

    private UIElement BuildTileCard(Tile tile)
    {
        var selected = tile.Id == _selectedTileId;
        var running = _controller.IsRunning(tile.Id);
        var hotkeyError = _controller.HotkeyErrors.GetValueOrDefault(tile.Id);
        var status = _controller.GetTileStatus(tile.Id);
        var border = new Border { Width = 194, MinHeight = 152, Margin = new Thickness(0, 0, 10, 10), Padding = new Thickness(12), CornerRadius = new CornerRadius(12), Background = selected ? Brush("#203432") : _panel, BorderBrush = selected ? _accent : running ? Brush("#3A9E92") : _line, BorderThickness = new Thickness(1), Focusable = true, AllowDrop = true, Cursor = Cursors.Hand, ToolTip = L("Auswählen · ziehen zum Sortieren · Rechtsklick für weitere Optionen") };
        var top = new DockPanel();
        var icon = new Border { Width = 34, Height = 34, CornerRadius = new CornerRadius(10), Background = SafeColor(tile.Color, "#25C4B6"), Child = Label(tile.Icon, 18, "#101719", true, HorizontalAlignment.Center) };
        top.Children.Add(icon);
        var run = ButtonAsync(running ? "■" : "▶", async () => { var latest = ActiveProfile?.Tiles.FirstOrDefault(t => t.Id == tile.Id) ?? tile; await RunTile(latest); }, false, 0);
        run.Padding = new Thickness(9, 5, 9, 5); run.ToolTip = running ? L("Aktion läuft") : L("Aktion jetzt starten"); run.IsEnabled = tile.Enabled && !_controller.Paused && !running;
        run.Click += (_, e) => e.Handled = true; DockPanel.SetDock(run, Dock.Right); top.Children.Add(run);
        var more = new Button { Content = "···", Padding = new Thickness(8, 5, 8, 5), Margin = new Thickness(5, 0, 5, 0), ToolTip = L("Weitere Optionen") };
        more.ContextMenu = BuildTileContextMenu(tile); more.Click += (_, e) => { more.ContextMenu!.PlacementTarget = more; more.ContextMenu.IsOpen = true; e.Handled = true; };
        DockPanel.SetDock(more, Dock.Right); top.Children.Add(more);

        var body = new StackPanel { Margin = new Thickness(0, 9, 0, 7) };
        body.Children.Add(Label($"{(tile.Favorite ? "★  " : "")}{tile.Name}", 14, tile.Enabled ? "#E8F0F1" : "#829395", true));
        body.Children.Add(Label(tile.Steps.Count > 1 ? LF($"{tile.Steps.Count} Schritte · {tile.Repeat}×") : ActionName(tile.Steps.FirstOrDefault()?.Kind ?? ActionKind.Launch), 10, "#91A3A5", false, new Thickness(0, 2, 0, 0)));
        var bottom = new DockPanel();
        var hotkey = Label(string.IsNullOrWhiteSpace(tile.Hotkey) ? L("Keine Belegung") : tile.Hotkey, 10, string.IsNullOrWhiteSpace(hotkeyError) ? "#AFC2C2" : "#FF9A91", true);
        hotkey.ToolTip = hotkeyError; bottom.Children.Add(hotkey);
        var stateText = running ? L("LÄUFT") : !string.IsNullOrWhiteSpace(hotkeyError) ? L("KONFLIKT") : !tile.Enabled ? L("AUS") : status;
        var stateColor = running ? "#67E0D4" : !string.IsNullOrWhiteSpace(hotkeyError) ? "#FF9A91" : !tile.Enabled ? "#F2B66D" : "#91A3A5";
        var badge = Label(Trim(stateText.ToUpperInvariant(), 12), 8, stateColor, true); badge.Margin = new Thickness(6, 0, 0, 0); badge.ToolTip = status; DockPanel.SetDock(badge, Dock.Right); bottom.Children.Add(badge);
        var layout = new StackPanel(); layout.Children.Add(top); layout.Children.Add(body); layout.Children.Add(bottom); border.Child = layout;
        border.ContextMenu = BuildTileContextMenu(tile);
        border.PreviewMouseRightButtonUp += (_, e) => { border.ContextMenu!.PlacementTarget = border; border.ContextMenu.IsOpen = true; e.Handled = true; };
        border.PreviewMouseLeftButtonDown += (_, e) => { _tileDragId = tile.Id; _tileDragStart = e.GetPosition(border); };
        border.PreviewMouseMove += (_, e) =>
        {
            if (_tileDragId != tile.Id || e.LeftButton != MouseButtonState.Pressed) return;
            var current = e.GetPosition(border);
            if (Math.Abs(current.X - _tileDragStart.X) < SystemParameters.MinimumHorizontalDragDistance && Math.Abs(current.Y - _tileDragStart.Y) < SystemParameters.MinimumVerticalDragDistance) return;
            var data = new DataObject("Aivana.TileId", tile.Id);
            DragDrop.DoDragDrop(border, data, DragDropEffects.Move); _tileDragId = null;
        };
        border.DragOver += (_, e) => { e.Effects = e.Data.GetDataPresent("Aivana.TileId") ? DragDropEffects.Move : DragDropEffects.None; e.Handled = true; };
        border.Drop += (_, e) =>
        {
            if (e.Data.GetData("Aivana.TileId") is Guid sourceId && ActiveProfile?.Tiles.FirstOrDefault(t => t.Id == sourceId) is { } source)
                MoveTileTo(source, tile.Id);
            _tileDragId = null; e.Handled = true;
        };
        border.PreviewKeyDown += (_, e) =>
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == 0) return;
            if (e.Key is Key.Up or Key.Down)
            {
                MoveTile(tile, e.Key == Key.Up ? -1 : 1); e.Handled = true;
            }
        };
        border.MouseLeftButtonUp += (_, e) =>
        {
            if (IsWithinButton(e.OriginalSource as DependencyObject)) return;
            SelectTile(tile); RenderPage(ActiveProfile); e.Handled = true;
        };
        return border;
    }

    private ContextMenu BuildTileContextMenu(Tile tile)
    {
        var menu = new ContextMenu();
        void Add(string text, Action action, string? gesture = null)
        {
            var item = new MenuItem { Header = L(text), InputGestureText = gesture };
            item.Click += (_, _) => action(); menu.Items.Add(item);
        }
        Add("Bearbeiten", () => { SelectTile(tile); RenderPage(ActiveProfile); });
        Add(tile.Favorite ? "☆ Aus Favoriten entfernen" : "★ Als Favorit speichern", () => SetTileFavorite(tile));
        Add("Duplizieren", () => DuplicateTile(tile));
        Add("Nach oben", () => MoveTile(tile, -1), "Strg+↑");
        Add("Nach unten", () => MoveTile(tile, 1), "Strg+↓");
        menu.Items.Add(new Separator());
        Add("Löschen", () => DeleteTile(tile));
        return menu;
    }

    private void SetTileFavorite(Tile tile)
    {
        if (!ResolvePendingDraft()) return;
        var profile = ActiveProfile;
        var persisted = profile?.Tiles.FirstOrDefault(t => t.Id == tile.Id);
        if (persisted == null) return;
        var prior = persisted.Favorite;
        persisted.Favorite = !prior;
        try
        {
            _controller.Save();
            if (_draftTile?.Id == persisted.Id)
            {
                _draftTile = Clone(persisted); _draftBaseline = SerializeTile(_draftTile);
                _draftBaselineTileId = persisted.Id; _draftBaselineProfileId = profile!.Id;
            }
            Refresh();
        }
        catch (Exception ex) { persisted.Favorite = prior; ShowError(ex); }
    }

    private static bool IsWithinButton(DependencyObject? source)
    {
        while (source != null)
        {
            if (source is ButtonBase) return true;
            source = source is Visual ? VisualTreeHelper.GetParent(source) : LogicalTreeHelper.GetParent(source);
        }
        return false;
    }

    private UIElement BuildActivityStrip()
    {
        var border = new Border { Background = _panel, BorderBrush = _line, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(10), Padding = new Thickness(12, 9, 12, 9), Margin = new Thickness(0, 12, 0, 0), MinHeight = 82, MaxHeight = 100 };
        var grid = new Grid(); grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) }); grid.ColumnDefinitions.Add(new ColumnDefinition());
        var stack = new StackPanel { VerticalAlignment = VerticalAlignment.Center }; stack.Children.Add(Label(L("LETZTE AKTIVITÄT"), 10, "#91A3A5", true)); stack.Children.Add(Label(L("Lokal · begrenzter Verlauf"), 10, "#657779")); grid.Children.Add(stack);
        var list = new ItemsControl { ItemsSource = _controller.Activity.Take(4).ToList() };
        list.ItemTemplate = TemplateForActivity();
        var panelFactory = new FrameworkElementFactory(typeof(WrapPanel)); panelFactory.SetValue(WrapPanel.OrientationProperty, Orientation.Horizontal); list.ItemsPanel = new ItemsPanelTemplate(panelFactory);
        var activityScroll = new ScrollViewer { Content = list, VerticalScrollBarVisibility = ScrollBarVisibility.Disabled, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, VerticalAlignment = VerticalAlignment.Center };
        Grid.SetColumn(activityScroll, 1); grid.Children.Add(activityScroll);
        border.Child = grid; return border;
    }

    private DataTemplate TemplateForActivity()
    {
        var template = new DataTemplate(typeof(ActivityEntry));
        var card = new FrameworkElementFactory(typeof(Border)); card.SetValue(Border.BackgroundProperty, Brush("#192123")); card.SetValue(Border.CornerRadiusProperty, new CornerRadius(7)); card.SetValue(Border.PaddingProperty, new Thickness(9, 6, 9, 6)); card.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 7, 0)); card.SetValue(FrameworkElement.WidthProperty, 160d);
        var panel = new FrameworkElementFactory(typeof(StackPanel));
        var name = new FrameworkElementFactory(typeof(TextBlock)); name.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Name")); name.SetValue(TextBlock.ForegroundProperty, Brush("#DCE6E7")); name.SetValue(TextBlock.FontSizeProperty, 10d); name.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis); panel.AppendChild(name);
        var status = new FrameworkElementFactory(typeof(TextBlock)); status.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Status")); status.SetValue(TextBlock.ForegroundProperty, Brush("#69D8CC")); status.SetValue(TextBlock.FontSizeProperty, 9d); status.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis); panel.AppendChild(status);
        card.AppendChild(panel); template.VisualTree = card; return template;
    }

    private UIElement BuildProfilesPage()
    {
        var root = new Grid(); root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 260 }); root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(340), MinWidth = 310 });
        var left = Card(); var stack = new StackPanel();
        var toolbar = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 12) };
        toolbar.Children.Add(Button(L("＋ Profil"), CreateProfile, true)); toolbar.Children.Add(Button(L("⧉ Duplizieren"), DuplicateProfile, false, 8));
        stack.Children.Add(toolbar);
        var settings = _controller.Settings;
        var list = new ListBox { ItemsSource = settings.Profiles, SelectedValuePath = "Id", SelectedValue = settings.ActiveProfileId, MinHeight = 120 };
        list.ItemTemplate = ProfileTemplate(); list.SelectionChanged += (_, _) =>
        {
            if (_refreshing || list.SelectedValue is not Guid id || id == _controller.Settings.ActiveProfileId) return;
            if (!ResolvePendingDraft()) { list.SelectedValue = _controller.Settings.ActiveProfileId; return; }
            try { _controller.SelectProfile(id); } catch (Exception ex) { list.SelectedValue = _controller.Settings.ActiveProfileId; ShowError(ex); }
        };
        stack.Children.Add(list);
        var order = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
        order.Children.Add(Button(L("↑ Priorität"), () => MoveProfile(list.SelectedValue as Guid?, -1), false)); order.Children.Add(Button(L("↓"), () => MoveProfile(list.SelectedValue as Guid?, 1), false, 6));
        order.Children.Add(Button(L("Als Standard"), () => SetDefault(list.SelectedValue as Guid?), false, 14)); order.Children.Add(Button(L("Löschen"), () => DeleteProfile(list.SelectedValue as Guid?), false, 6)); stack.Children.Add(order);
        left.Child = stack; root.Children.Add(left);
        var details = Card(); var detailStack = new StackPanel(); detailStack.Children.Add(Label(L("PROFILDETAILS"), 11, "#91A3A5", true));
        var lockRow = new DockPanel { Margin = new Thickness(0, 7, 0, 0) };
        var lockState = Label(_controller.ProfileLocked ? L("GESPERRT · automatischer Wechsel aus") : L("AUTOMATISCHER WECHSEL FREI"), 9, _controller.ProfileLocked ? "#F2B66D" : "#67D8CC", true);
        lockState.VerticalAlignment = VerticalAlignment.Center;
        var lockButton = Button(_controller.ProfileLocked ? L("🔓 Entsperren") : L("🔒 Profil sperren"), () => { _controller.SetProfileLocked(!_controller.ProfileLocked); RenderPage(ActiveProfile); }, _controller.ProfileLocked);
        lockButton.Padding = new Thickness(8, 5, 8, 5); DockPanel.SetDock(lockButton, Dock.Right); lockRow.Children.Add(lockButton); lockRow.Children.Add(lockState); detailStack.Children.Add(lockRow);
        var selected = settings.Profiles.FirstOrDefault(p => p.Id == settings.ActiveProfileId) ?? settings.Profiles.FirstOrDefault();
        if (selected != null)
        {
            var index = settings.Profiles.FindIndex(p => p.Id == selected.Id) + 1;
            detailStack.Children.Add(Label($"{L(selected.Id == settings.ActiveProfileId ? "AKTIV" : "INAKTIV")}   ·   {(selected.Id == settings.DefaultProfileId ? L("STANDARDPROFIL") : L("PRIORITÄT ") + index)}", 10, selected.Id == settings.DefaultProfileId ? "#67D8CC" : "#91A3A5", true, new Thickness(0, 7, 0, 4)));
            detailStack.Children.Add(FieldLabel(L("NAME"))); detailStack.Children.Add(Input(selected.Name, v => { var prior = selected.Name; SaveSetting(() => selected.Name = v, () => selected.Name = prior); Refresh(); }, L("Arbeitsprofil")));
            detailStack.Children.Add(FieldLabel(L("PROZESSNAME (OPTIONAL)"))); detailStack.Children.Add(Input(selected.ProcessName, v => { var prior = selected.ProcessName; SaveSetting(() => selected.ProcessName = v, () => selected.ProcessName = prior); Refresh(); }, "code.exe"));
            detailStack.Children.Add(FieldLabel(L("FENSTERTITEL ENTHÄLT (OPTIONAL)"))); detailStack.Children.Add(Input(selected.WindowTitleContains, v => { var prior = selected.WindowTitleContains; SaveSetting(() => selected.WindowTitleContains = v, () => selected.WindowTitleContains = prior); Refresh(); }, L("z. B. Projektname")));
            detailStack.Children.Add(Label(L("Prozessname und Fenstertitel müssen beide passen (UND). Ein leerer Wert wird ignoriert. Nutze den Fenstertitel, wenn ein Programm mehrere Arbeitsbereiche über verschiedene Fenster verwaltet."), 10, "#8A9B9D", false, new Thickness(0, 7, 0, 0)));
            detailStack.Children.Add(Button(L("⌖ Bedingungen aus Vordergrund übernehmen"), () =>
            {
                if (string.IsNullOrWhiteSpace(_controller.ForegroundProcess) && string.IsNullOrWhiteSpace(_controller.ForegroundWindowTitle))
                { MessageBox.Show(this, L("Es ist kein Vordergrundfenster verfügbar."), L("Bedingungen erfassen"), MessageBoxButton.OK, MessageBoxImage.Information); return; }
                var oldProcess = selected.ProcessName; var oldTitle = selected.WindowTitleContains;
                try { SaveSetting(() => { selected.ProcessName = _controller.ForegroundProcess; selected.WindowTitleContains = _controller.ForegroundWindowTitle; }, () => { selected.ProcessName = oldProcess; selected.WindowTitleContains = oldTitle; }); Refresh(); }
                catch (Exception ex) { ShowError(ex); }
            }, false, 0, null, new Thickness(0, 8, 0, 0)));
            detailStack.Children.Add(Label(L("Wenn automatische Profile aktiv sind, wechselt die App bei passenden Vordergrundbedingungen. Ohne Treffer bleibt das Standardprofil aktiv. Bei mehreren Treffern gilt die Listenreihenfolge."), 10, "#8A9B9D", false, new Thickness(0, 10, 0, 0)));
            detailStack.Children.Add(Label(LF($"Aktuelles Vordergrundprogramm: {EmptyAsDash(_controller.ForegroundProcess)}"), 10, "#67D8CC", true, new Thickness(0, 8, 0, 0)));
            detailStack.Children.Add(Label(LF($"Aktueller Fenstertitel: {EmptyAsDash(_controller.ForegroundWindowTitle)}"), 10, "#67D8CC", false, new Thickness(0, 3, 0, 0)));
            detailStack.Children.Add(Label(L("Prozessnamen ohne Pfad eintragen, z. B. premiere.exe."), 9, "#687A7C", false, new Thickness(0, 7, 0, 0)));
        }
        details.Child = detailStack; Grid.SetColumn(details, 1); root.Children.Add(details); return root;
    }

    private DataTemplate ProfileTemplate()
    {
        var t = new DataTemplate(typeof(Profile)); var root = new FrameworkElementFactory(typeof(DockPanel));
        var sub = new FrameworkElementFactory(typeof(TextBlock)); sub.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("ProcessName") { StringFormat = "{0}" }); sub.SetValue(TextBlock.FontSizeProperty, 10d); sub.SetValue(TextBlock.ForegroundProperty, Brush("#829395")); sub.SetValue(DockPanel.DockProperty, Dock.Right); root.AppendChild(sub);
        var name = new FrameworkElementFactory(typeof(TextBlock)); name.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Name")); name.SetValue(TextBlock.FontSizeProperty, 13d); name.SetValue(TextBlock.FontWeightProperty, FontWeights.SemiBold); root.AppendChild(name); t.VisualTree = root; return t;
    }

    private UIElement BuildDevicesPage()
    {
        var root = new Grid(); root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        var header = new DockPanel { Margin = new Thickness(0, 0, 0, 12) };
        var refresh = Button(L("↻ Aktualisieren"), () => { _controller.RefreshDevices(); Refresh(); }, false); DockPanel.SetDock(refresh, Dock.Right); header.Children.Add(refresh);
        header.Children.Add(Label(LF($"{_controller.Devices.Count} erkannte Geräteeinträge"), 13, "#B7C5C6")); root.Children.Add(header);
        var body = new StackPanel();
        var corsair = Card(); corsair.Padding = new Thickness(16); var cs = new StackPanel();
        var corsairTitle = new DockPanel(); corsairTitle.Children.Add(Label(L("CORSAIR / iCUE"), 13, "#E8F0F1", true));
        var availability = Label(_controller.CorsairAvailable ? L("VERBUNDEN") : L("NICHT VERFÜGBAR"), 9, _controller.CorsairAvailable ? "#67D8CC" : "#F2B66D", true); DockPanel.SetDock(availability, Dock.Right); corsairTitle.Children.Add(availability); cs.Children.Add(corsairTitle);
        cs.Children.Add(Label(_controller.CorsairStatus, 11, "#91A3A5", false, new Thickness(0, 6, 0, 10)));
        if (_controller.CorsairKeyboards.Count == 0)
        {
            cs.Children.Add(Label(L("iCUE meldet momentan keine steuerbare Tastatur. Die normalen Hotkeys funktionieren unabhängig davon weiter."), 11, "#A0AEAF", false, new Thickness(0, 0, 0, 10)));
        }
        else
        {
            foreach (var keyboard in _controller.CorsairKeyboards)
            {
                var deviceCard = new Border { Background = Brush("#192526"), BorderBrush = _line, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(8), Padding = new Thickness(11), Margin = new Thickness(0, 0, 0, 7) };
                var line = new DockPanel(); line.Children.Add(Label(keyboard.Name, 12, "#E8F0F1", true));
                var leds = Label(LF($"{keyboard.LedCount} LEDs"), 10, "#67D8CC", true); DockPanel.SetDock(leds, Dock.Right); line.Children.Add(leds); deviceCard.Child = line; cs.Children.Add(deviceCard);
            }
            var colorRow = new WrapPanel { Margin = new Thickness(0, 3, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            colorRow.Children.Add(Label(L("Farbe testen"), 11, "#AFC0C1", false, new Thickness(0, 0, 9, 0)));
            var color = new TextBox { Text = "#35D0C1", Width = 100, MaxLength = 9, ToolTip = L("RGB-Hexfarbe, zum Beispiel #35D0C1"), Margin = new Thickness(0, 0, 8, 0) }; colorRow.Children.Add(color);
            colorRow.Children.Add(ButtonAsync(L("Beleuchtung setzen"), async () => { await _controller.TestCorsairColorAsync(color.Text); Refresh(); }, true));
            colorRow.Children.Add(Button(L("iCUE öffnen"), () => _controller.OpenIcue(), false, 8)); cs.Children.Add(colorRow);
        }
        var advanced = new Expander { Header = L("Technische Geräteinformationen") };
        var corsairDetail = new StackPanel();
        corsairDetail.Children.Add(Label(L("Der Gerätestatus stammt aus dem Windows-HID-Inventar; Beleuchtungssteuerung ist nur verfügbar, wenn das optionale iCUE-SDK ein kompatibles Gerät meldet."), 10, "#829395", false, new Thickness(0, 4, 0, 8)));
        advanced.Content = corsairDetail; cs.Children.Add(advanced); corsair.Child = cs; body.Children.Add(corsair);

        var deviceGroups = _controller.Devices.GroupBy(d => (d.Name, d.Manufacturer, d.VendorId, d.ProductId)).ToList();
        if (deviceGroups.Count == 0)
        {
            var empty = Card(); empty.Margin = new Thickness(0, 10, 0, 0); var es = new StackPanel(); es.Children.Add(Label(L("Noch keine Tastatur erkannt"), 15, "#DCE6E7", true)); es.Children.Add(Label(L("Prüfe die Verbindung und aktualisiere die Geräteliste. Das normale Hotkey-System bleibt davon unabhängig."), 12, "#91A3A5", false, new Thickness(0, 7, 0, 0))); empty.Child = es; body.Children.Add(empty);
        }
        else
        {
            body.Children.Add(Label(L("ERKANNTE TASTATUREN"), 10, "#91A3A5", true, new Thickness(3, 14, 0, 2)));
            foreach (var group in deviceGroups)
            {
                var devices = group.ToList(); var device = devices[0];
                var card = Card(); card.Margin = new Thickness(0, 7, 0, 0); var ds = new StackPanel();
                var line = new DockPanel(); line.Children.Add(Label(device.Name, 13, "#E8F0F1", true));
                if (devices.Count > 1) { var count = Label(LF($"{devices.Count} Schnittstellen"), 9, "#8FA1A3", true); DockPanel.SetDock(count, Dock.Right); line.Children.Add(count); }
                ds.Children.Add(line);
                ds.Children.Add(Label(LF($"{EmptyAsDash(device.Manufacturer)}  ·  USB {EmptyAsDash(device.VendorId)}:{EmptyAsDash(device.ProductId)}"), 10, "#91A3A5", false, new Thickness(0, 4, 0, 0)));
                var details = new Expander { Header = L("Details und Schnittstellen") };
                var detailStack = new StackPanel();
                foreach (var entry in devices)
                {
                    detailStack.Children.Add(Label(LF($"Fähigkeiten: {EmptyAsDash(entry.Capabilities)}"), 10, "#91A3A5", false, new Thickness(0, 4, 0, 0)));
                    detailStack.Children.Add(Label(Trim(entry.Path, 180), 9, "#627375", false, new Thickness(0, 2, 0, 0)));
                }
                details.Content = detailStack; ds.Children.Add(details); card.Child = ds; body.Children.Add(card);
            }
        }
        var scroll = new ScrollViewer { Content = body, VerticalScrollBarVisibility = ScrollBarVisibility.Auto }; Grid.SetRow(scroll, 1); root.Children.Add(scroll); return root;
    }

    private UIElement BuildSettingsPage()
    {
        var root = new Grid(); root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); root.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        var general = Card(); var g = new StackPanel(); g.Children.Add(Label(L("START & VERHALTEN"), 12, "#E8F0F1", true));
        g.Children.Add(FieldLabel(L("ANZEIGESPRACHE")));
        g.Children.Add(BuildLanguagePicker());
        var auto = SettingCheck(L("Mit Windows starten"), _controller.Settings.AutoStart, v =>
        {
            var prior = _controller.Settings.AutoStart;
            try { _controller.SetAutoStart(v); }
            catch { try { _controller.SetAutoStart(prior); } catch { _controller.Settings.AutoStart = prior; } throw; }
        }); g.Children.Add(auto);
        g.Children.Add(Label(L("Autostart ist standardmäßig aus und wird als Benutzeroption verwaltet."), 10, "#718284"));
        var autoProfiles = SettingCheck(L("Profil anhand des Vordergrundprogramms wechseln"), _controller.Settings.AutoProfiles, v =>
        { var prior = _controller.Settings.AutoProfiles; SaveSetting(() => _controller.Settings.AutoProfiles = v, () => _controller.Settings.AutoProfiles = prior); }); g.Children.Add(autoProfiles);
        g.Children.Add(SettingCheck(L("Beim Schließen in den Infobereich minimieren"), _controller.Settings.CloseToTray, v =>
        { var prior = _controller.Settings.CloseToTray; SaveSetting(() => _controller.Settings.CloseToTray = v, () => _controller.Settings.CloseToTray = prior); }));
        g.Children.Add(FieldLabel(L("ABBRUCH-HOTKEY"))); g.Children.Add(Input(_controller.Settings.CancelHotkey, v => { var prior = _controller.Settings.CancelHotkey; SaveSetting(() => _controller.Settings.CancelHotkey = v, () => _controller.Settings.CancelHotkey = prior); }, "Ctrl+Alt+Escape"));
        g.Children.Add(Label(L("Stoppt alle laufenden Makros und gibt simulierte Tasten frei."), 10, "#718284", false, new Thickness(0, 5, 0, 0)));
        g.Children.Add(FieldLabel(L("BEFEHLSPALETTE · GLOBALER HOTKEY"))); g.Children.Add(Input(_controller.Settings.PaletteHotkey, v => { var prior = _controller.Settings.PaletteHotkey; SaveSetting(() => _controller.Settings.PaletteHotkey = v, () => _controller.Settings.PaletteHotkey = prior); Refresh(); }, "Ctrl+Shift+Space"));
        var paletteError = _controller.HotkeyErrors.GetValueOrDefault(Guid.Empty);
        g.Children.Add(Label(!string.IsNullOrWhiteSpace(paletteError) ? LF($"HOTKEY NICHT VERFÜGBAR · {paletteError}") : LF($"Aktiv: {_controller.Settings.PaletteHotkey}"), 10, !string.IsNullOrWhiteSpace(paletteError) ? "#FF9A91" : "#67D8CC", true, new Thickness(0, 4, 0, 0)));
        g.Children.Add(Label(L("Über die Palette findest du Aktionen in allen Profilen. Ctrl+K ist zusätzlich verfügbar, wenn dieses Fenster den Fokus hat."), 10, "#718284", false, new Thickness(0, 5, 0, 0)));
        general.Child = g; root.Children.Add(general);
        var data = Card(); data.Margin = new Thickness(12, 0, 0, 0); var d = new StackPanel(); d.Children.Add(Label(L("KONFIGURATION"), 12, "#E8F0F1", true)); d.Children.Add(Label(L("Profile und Aktionen werden lokal auf diesem PC gespeichert."), 11, "#91A3A5", false, new Thickness(0, 7, 0, 14)));
        d.Children.Add(Button(L("⇧ Konfiguration importieren"), ImportProfiles, false)); d.Children.Add(Button(L("⇩ Konfiguration exportieren"), () => _controller.Export(), false, 0, null, new Thickness(0, 8, 0, 0)));
        d.Children.Add(Label(L("Der Import wird vor dem Übernehmen geprüft. Export enthält Profile und Belegungen."), 10, "#718284", false, new Thickness(0, 12, 0, 0)));
        var privacy = new Border { Background = Brush("#192526"), CornerRadius = new CornerRadius(8), Padding = new Thickness(11), Margin = new Thickness(0, 18, 0, 0) };
        privacy.Child = Label(L("DATENSCHUTZ\nKeine Cloud und keine Telemetrie. Der Verlauf enthält Aktionsstatus, aber keine eingegebenen Textinhalte."), 11, "#9EB1B2"); d.Children.Add(privacy); data.Child = d; Grid.SetColumn(data, 1); root.Children.Add(data); return root;
    }

    private UIElement BuildKeyboardPage(Profile? profile) => BuildKeyboardLayoutPage(profile);

    private void AddTile(Profile? profile)
    {
        if (profile == null) return;
        var tile = new Tile { Name = L("Neue Aktion"), Icon = "▶", Color = "#25C4B6", Steps = [new ActionStep { Kind = ActionKind.Delay, DelayMs = 100 }] };
        if (!ResolvePendingDraft()) return;
        _selectedTileId = tile.Id; _draftTile = Clone(tile); _draftProfileId = profile.Id; _selectedStep = 0;
        _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = profile.Id; Refresh();
    }
    private void DuplicateTile(Tile source)
    {
        var profile = ActiveProfile; if (profile == null || !ResolvePendingDraft()) return;
        var clone = Clone(source); clone.Id = Guid.NewGuid(); clone.Name = Aivana.Core.Localization.Format("{0} (Kopie)", source.Name); clone.Hotkey = ""; clone.Favorite = source.Favorite; _selectedTileId = clone.Id; _draftTile = Clone(clone); _draftProfileId = profile.Id; _selectedStep = 0;
        _draftBaseline = ""; _draftBaselineTileId = clone.Id; _draftBaselineProfileId = profile.Id; Refresh();
    }
    private void DeleteTile(Tile tile)
    {
        if (!ResolvePendingDraft()) return;
        var profile = ActiveProfile; var index = profile?.Tiles.FindIndex(t => t.Id == tile.Id) ?? -1;
        if (profile == null || index < 0) return;
        var removed = profile.Tiles[index]; profile.Tiles.RemoveAt(index);
        try { _controller.Save(); _selectedTileId = null; _draftTile = null; _draftProfileId = null; _selectedStep = -1; Refresh(); }
        catch (Exception ex) { profile.Tiles.Insert(index, removed); ShowError(ex); }
    }
    private void MoveTile(Tile tile, int delta)
    {
        if (!ResolvePendingDraft()) return; var list = ActiveProfile?.Tiles; if (list == null) return;
        var from = list.FindIndex(t => t.Id == tile.Id); var to = from + delta; if (from < 0 || to < 0 || to >= list.Count) return;
        MoveTileToIndex(list, from, to);
    }
    private void MoveTileTo(Tile tile, Guid targetId)
    {
        if (!ResolvePendingDraft()) return; var list = ActiveProfile?.Tiles; if (list == null) return;
        var from = list.FindIndex(t => t.Id == tile.Id); var to = list.FindIndex(t => t.Id == targetId);
        if (from < 0 || to < 0 || from == to) return;
        MoveTileToIndex(list, from, to);
    }
    private void MoveTileToIndex(List<Tile> list, int from, int to)
    {
        var source = list[from]; list.RemoveAt(from); list.Insert(to, source);
        try { _controller.Save(); Refresh(); }
        catch (Exception ex) { list.Remove(source); list.Insert(from, source); ShowError(ex); }
    }
    public void EditTile(Guid tileId)
    {
        var tile = ActiveProfile?.Tiles.FirstOrDefault(t => t.Id == tileId);
        if (tile == null) return;
        SelectTile(tile); RenderPage(ActiveProfile);
    }
    private void SelectTile(Tile tile) { if (!ResolvePendingDraft()) return; _selectedTileId = tile.Id; var latest = ActiveProfile?.Tiles.FirstOrDefault(t => t.Id == tile.Id) ?? tile; _draftTile = Clone(latest); _draftProfileId = ActiveProfile?.Id; _selectedStep = -1; }
    private void CreateProfile()
    {
        if (!ResolvePendingDraft()) return;
        var oldActive = _controller.Settings.ActiveProfileId;
        var p = new Profile { Name = Aivana.Core.Localization.Format("Profil {0}", _controller.Settings.Profiles.Count + 1), Tiles = [] }; _controller.Settings.Profiles.Add(p);
        try { _controller.SelectProfile(p.Id); Refresh(); } catch (Exception ex) { _controller.Settings.ActiveProfileId = oldActive; _controller.Settings.Profiles.Remove(p); ShowError(ex); }
    }
    private void DuplicateProfile()
    {
        if (!ResolvePendingDraft()) return; var source = ActiveProfile; if (source == null) return; var p = Clone(source); p.Id = Guid.NewGuid(); p.Name = Aivana.Core.Localization.Format("{0} (Kopie)", source.Name); p.WindowTitleContains = source.WindowTitleContains; foreach (var t in p.Tiles) t.Id = Guid.NewGuid();
        var oldActive = _controller.Settings.ActiveProfileId;
        _controller.Settings.Profiles.Add(p);
        try { _controller.SelectProfile(p.Id); Refresh(); } catch (Exception ex) { _controller.Settings.ActiveProfileId = oldActive; _controller.Settings.Profiles.Remove(p); ShowError(ex); }
    }
    private void DeleteProfile(Guid? id)
    {
        if (id == null) return;
        if (!ResolvePendingDraft()) return;
        if (_controller.Settings.Profiles.Count <= 1) { MessageBox.Show(this, L("Das letzte Profil kann nicht gelöscht werden."), L("Profil behalten"), MessageBoxButton.OK, MessageBoxImage.Information); return; }
        var profile = _controller.Settings.Profiles.FirstOrDefault(p => p.Id == id); if (profile == null) return;
        var references = _controller.Settings.Profiles.Where(p => p.Id != id)
            .SelectMany(p => p.Tiles.SelectMany(t => t.Steps.Select(s => (Profile: p.Name, Tile: t.Name, Step: s))))
            .Where(x => x.Step.Kind == ActionKind.SwitchProfile && Guid.TryParse(x.Step.Value, out var target) && target == id)
            .ToList();
        if (references.Count > 0)
        {
            MessageBox.Show(this, LF($"Dieses Profil wird in {references.Count} Profilaktion(en) als Ziel verwendet. Ändere diese Verweise zuerst."), L("Profil wird verwendet"), MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var oldDefault = _controller.Settings.DefaultProfileId;
        var oldActive = _controller.Settings.ActiveProfileId;
        var oldIndex = _controller.Settings.Profiles.IndexOf(profile);
        _controller.Settings.Profiles.RemoveAt(oldIndex);
        if (_controller.Settings.DefaultProfileId == id) _controller.Settings.DefaultProfileId = _controller.Settings.Profiles[0].Id;
        if (_controller.Settings.ActiveProfileId == id) _controller.Settings.ActiveProfileId = _controller.Settings.DefaultProfileId;
        try { _controller.Save(); Refresh(); }
        catch (Exception ex)
        {
            _controller.Settings.Profiles.Insert(oldIndex, profile);
            _controller.Settings.DefaultProfileId = oldDefault;
            _controller.Settings.ActiveProfileId = oldActive;
            ShowError(ex);
        }
    }
    private void MoveProfile(Guid? id, int delta)
    {
        if (id == null || !ResolvePendingDraft()) return; var profiles = _controller.Settings.Profiles; var from = profiles.FindIndex(p => p.Id == id); var to = from + delta; if (from < 0 || to < 0 || to >= profiles.Count) return;
        (profiles[from], profiles[to]) = (profiles[to], profiles[from]);
        try { _controller.Save(); Refresh(); } catch (Exception ex) { (profiles[from], profiles[to]) = (profiles[to], profiles[from]); ShowError(ex); }
    }
    private void SetDefault(Guid? id)
    {
        if (id == null || !ResolvePendingDraft()) return;
        var prior = _controller.Settings.DefaultProfileId; _controller.Settings.DefaultProfileId = id.Value;
        try { _controller.Save(); Refresh(); } catch (Exception ex) { _controller.Settings.DefaultProfileId = prior; ShowError(ex); }
    }

    private CheckBox SettingCheck(string text, bool value, Action<bool> onChange)
    {
        var check = new CheckBox { Content = text, IsChecked = value, Margin = new Thickness(0, 13, 0, 4) };
        var lastSaved = value; var restoring = false;
        void Apply(bool next)
        {
            if (restoring) return;
            try { onChange(next); lastSaved = next; }
            catch (Exception ex)
            {
                restoring = true; check.IsChecked = lastSaved; restoring = false; ShowError(ex);
            }
        }
        check.Checked += (_, _) => Apply(true);
        check.Unchecked += (_, _) => Apply(false); return check;
    }

    private void SaveSetting(Action apply, Action rollback)
    {
        try { apply(); _controller.Save(); }
        catch { rollback(); throw; }
    }
    private bool ResolvePendingDraft()
    {
        if (!HasUnsavedChanges) return true;
        var choice = MessageBox.Show(this,
            L("Du hast Änderungen an dieser Aktion. Möchtest du sie speichern, verwerfen oder hier weiterarbeiten?"),
            L("Änderungen an der Aktion"), MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
        if (choice == MessageBoxResult.Yes) return CommitDraft();
        if (choice == MessageBoxResult.No) { DiscardDraft(); return true; }
        return false;
    }

    public bool PrepareToClose() => ResolvePendingDraft();

    private void SwitchPage(string page)
    {
        if (page == _page || !ResolvePendingDraft()) return;
        if (_capturing) FinishCapture(); _page = page; RenderPage(ActiveProfile);
    }
    private void ImportProfiles() { if (ResolvePendingDraft()) _controller.Import(); }
    private void NavDashboard_Click(object sender, RoutedEventArgs e) => SwitchPage("Dashboard");
    private void NavProfiles_Click(object sender, RoutedEventArgs e) => SwitchPage("Profile");
    private void NavDevices_Click(object sender, RoutedEventArgs e) => SwitchPage("Geräte");
    private void NavSettings_Click(object sender, RoutedEventArgs e) => SwitchPage("Einstellungen");
    private void NavKeyboard_Click(object sender, RoutedEventArgs e) => SwitchPage("Tastatur");
    private void ProfilePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_refreshing || ProfilePicker.SelectedValue is not Guid id) return;
        if (id == _controller.Settings.ActiveProfileId) return;
        if (!ResolvePendingDraft()) { ProfilePicker.SelectedValue = _controller.Settings.ActiveProfileId; return; }
        _selectedTileId = null; _draftTile = null; _draftProfileId = null; _selectedStep = -1;
        try { _controller.SelectProfile(id); RenderPage(ActiveProfile); } catch (Exception ex) { ProfilePicker.SelectedValue = _controller.Settings.ActiveProfileId; ShowError(ex); }
    }
    private void PauseButton_Click(object sender, RoutedEventArgs e) { _controller.SetPaused(!_controller.Paused); Refresh(); }

    private Border Card() => new() { Background = _panel, BorderBrush = _line, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(12), Padding = new Thickness(16) };
    private static Brush Brush(string color) => new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
    private Brush SafeColor(string color, string fallback) => Brush(IsHexColor(color) ? color : fallback);
    private Button Button(string text, Action action, bool primary = false, double leftMargin = 0, string? background = null, Thickness? margin = null)
    {
        var b = new Button { Content = text, Background = background != null ? Brush(background) : primary ? Brush("#245C57") : _panel2, BorderBrush = primary ? Brush("#347D75") : _line, Foreground = primary ? Brush("#D7FFFA") : Brush("#DFE8E9"), Margin = margin ?? new Thickness(leftMargin, 0, 0, 0) };
        b.Click += async (_, _) => { try { action(); } catch (Exception ex) { ShowError(ex); } }; return b;
    }
    private Button ButtonAsync(string text, Func<Task> action, bool primary = false, double leftMargin = 0)
    {
        var b = new Button { Content = text, Background = primary ? Brush("#245C57") : _panel2, BorderBrush = primary ? Brush("#347D75") : _line, Foreground = primary ? Brush("#D7FFFA") : Brush("#DFE8E9"), Margin = new Thickness(leftMargin, 0, 0, 0) };
        b.Click += async (_, _) => { try { await action(); } catch (Exception ex) { ShowError(ex); } }; return b;
    }
    private void ShowError(Exception ex) { FooterStatus.Text = L(ex.Message); MessageBox.Show(this, L(ex.Message), L("Fehler"), MessageBoxButton.OK, MessageBoxImage.Warning); }
    private TextBlock Label(string text, double size = 12, string color = "#E8F0F1", bool bold = false, HorizontalAlignment alignment = HorizontalAlignment.Left)
        => new() { Text = text, FontSize = size, Foreground = Brush(color), FontWeight = bold ? FontWeights.SemiBold : FontWeights.Normal, HorizontalAlignment = alignment, TextWrapping = TextWrapping.Wrap };
    private TextBlock Label(string text, double size, string color, bool bold, Thickness margin)
    { var l = Label(text, size, color, bold); l.Margin = margin; return l; }
    private TextBlock FieldLabel(string text) { var t = Label(text, 9, "#829597", true); t.Margin = new Thickness(0, 9, 0, 3); return t; }
    private TextBox Input(string value, Action<string> changed, string placeholder = "")
    {
        var box = new TextBox { Text = value, ToolTip = placeholder, Margin = new Thickness(0, 2, 0, 3) };
        box.LostKeyboardFocus += (_, _) =>
        {
            var failed = false;
            if (box.Text != value)
            {
                _editorError = null;
                try { changed(box.Text); } catch (Exception ex) { _editorError = ex.Message; }
                failed = !string.IsNullOrWhiteSpace(_editorError);
                box.BorderBrush = failed ? Brush("#FF7777") : Brush("#314143");
                box.ToolTip = failed ? _editorError : placeholder;
                if (failed) FooterStatus.Text = _editorError!;
            }
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (!failed && Keyboard.FocusedElement is not TextBox and not ComboBox) RenderPage(ActiveProfile);
            }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        };
        return box;
    }
    private static bool IsHexColor(string? text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length != 7 || text[0] != '#') return false;
        return int.TryParse(text.AsSpan(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _);
    }
    private static string ActionName(ActionKind kind) => kind switch
    {
        ActionKind.Launch => L("Programm starten"), ActionKind.OpenPath => L("Datei / Ordner öffnen"), ActionKind.Url => L("Webseite öffnen"), ActionKind.Text => L("Text eingeben"), ActionKind.Shortcut => L("Tastenkombination"), ActionKind.Media => L("Mediensteuerung"), ActionKind.Volume => L("Lautstärke"), ActionKind.SwitchProfile => L("Profil wechseln"), ActionKind.Delay => L("Pause"), ActionKind.CorsairColor => L("Corsair-Beleuchtung"), ActionKind.Clipboard => L("Zwischenablage setzen"), ActionKind.Window => L("Fenster anordnen"), ActionKind.System => L("Windows sperren"), ActionKind.Obs => "OBS Studio", ActionKind.WaitWindow => L("Auf Zielfenster warten"), _ => kind.ToString()
    };
    private static bool HotkeyContainsKey(string hotkey, string key)
    {
        var normalized = hotkey.ToUpperInvariant();
        var alias = key switch { "←" => "LEFT", "→" => "RIGHT", "↑" => "UP", "↓" => "DOWN", "⌫" => "BACKSPACE", "´" => "OEM3", "ß" => "OEM1", _ => key.ToUpperInvariant() };
        return normalized.EndsWith("+" + alias, StringComparison.Ordinal);
    }
    private static string KeyboardKeyDisplay(string key) => key switch
    {
        "PrintScreen" => "Druck", "NumAdd" => "+", "NumSubtract" => "−", "NumMultiply" => "×", "NumDivide" => "÷", "NumDecimal" => ",",
        "←" => "←", "→" => "→", "↑" => "↑", "↓" => "↓", _ => key
    };
    private static string EmptyAsDash(string? value) => string.IsNullOrWhiteSpace(value) ? "—" : value;
    private static string Trim(string? value, int length) => string.IsNullOrEmpty(value) || value.Length <= length ? value ?? "" : value[..length] + "…";
    private static T Clone<T>(T source) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source))!;
    private sealed record ActionChoice(ActionKind Kind, string Label);
    private static List<ScrollViewer> FindScrollViewers(DependencyObject? root)
    {
        var result = new List<ScrollViewer>();
        if (root == null) return result;
        if (root is ScrollViewer viewer) result.Add(viewer);
        var count = VisualTreeHelper.GetChildrenCount(root);
        for (var i = 0; i < count; i++) result.AddRange(FindScrollViewers(VisualTreeHelper.GetChild(root, i)));
        return result;
    }
}

internal sealed class NumericUpDownFallback : Border
{
    private readonly TextBox _value;
    public event Action<int>? ValueChanged;
    public NumericUpDownFallback(int value)
    {
        Width = 96; Height = 34; CornerRadius = new CornerRadius(6); BorderBrush = Brush("#314143"); BorderThickness = new Thickness(1); Background = Brush("#111719");
        var grid = new Grid(); grid.ColumnDefinitions.Add(new ColumnDefinition()); grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(25) });
        _value = new TextBox { Text = value.ToString(), BorderThickness = new Thickness(0), Padding = new Thickness(7, 4, 2, 4), VerticalContentAlignment = VerticalAlignment.Center, Background = Brushes.Transparent };
        _value.LostKeyboardFocus += (_, _) => Emit(); _value.PreviewKeyDown += (_, e) => { if (e.Key == Key.Enter) { Emit(); e.Handled = true; } };
        grid.Children.Add(_value);
        var buttons = new StackPanel(); var up = new Button { Content = "▴", Padding = new Thickness(0), FontSize = 9, BorderThickness = new Thickness(0), Background = Brushes.Transparent }; var down = new Button { Content = "▾", Padding = new Thickness(0), FontSize = 9, BorderThickness = new Thickness(0), Background = Brushes.Transparent };
        up.Click += (_, _) => Step(1); down.Click += (_, _) => Step(-1); buttons.Children.Add(up); buttons.Children.Add(down); Grid.SetColumn(buttons, 1); grid.Children.Add(buttons); Child = grid;
    }
    private void Step(int amount) { var current = int.TryParse(_value.Text, out var n) ? n : 1; _value.Text = (current + amount).ToString(); Emit(); }
    private void Emit() { if (int.TryParse(_value.Text, out var number)) ValueChanged?.Invoke(number); }
    private static Brush Brush(string c) => new SolidColorBrush((Color)ColorConverter.ConvertFromString(c));
}

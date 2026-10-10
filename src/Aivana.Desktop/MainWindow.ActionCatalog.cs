using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private void ShowActionCatalog(Profile? profile, Tile? draft = null, bool replaceStep = false)
    {
        if (profile is null) return;
        var dialog = BuildActionCatalogWindow(preset =>
        {
            var steps = preset.CreateSteps();
            foreach (var step in steps.Where(step => step.Kind == ActionKind.SwitchProfile)) step.Value = "";
            if (draft is not null)
            {
                if (replaceStep && _selectedStep >= 0 && _selectedStep < draft.Steps.Count)
                {
                    draft.Steps.RemoveAt(_selectedStep);
                    draft.Steps.InsertRange(_selectedStep, steps);
                }
                else { draft.Steps.AddRange(steps); _selectedStep = draft.Steps.Count - steps.Count; }
                Refresh();
                return;
            }
            if (!ResolvePendingDraft()) return;
            var tile = new Tile { Name = preset.Name, Icon = preset.Icon, Steps = steps };
            _selectedTileId = tile.Id; _draftTile = Clone(tile); _draftProfileId = profile.Id; _selectedStep = 0;
            _draftBaseline = ""; _draftBaselineTileId = tile.Id; _draftBaselineProfileId = profile.Id;
            Refresh();
        }, draft is null ? () => AddMacroTemplate(profile) : null);
        dialog.ShowDialog();
    }

    private Window BuildActionCatalogWindow(Action<ActionPreset> choose, Action? macro)
    {
        var dialog = new Window
        {
            Title = L("Aktionskatalog"), Owner = this, Width = 1060, Height = 740,
            MinWidth = 850, MinHeight = 560, WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = Brush("#101719"), Foreground = Brush("#E8F0F1"), ShowInTaskbar = false,
            FontFamily = FontFamily, UseLayoutRounding = true, SnapsToDevicePixels = true
        };
        dialog.Resources.MergedDictionaries.Add(Resources);
        var root = new Grid();
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var header = new StackPanel { Margin = new Thickness(0, 0, 0, 18) };
        header.Children.Add(Label(L("Aktionskatalog"), 24, "#E8F0F1", true));
        header.Children.Add(Label(L("Wähle eine Funktion. Alle Optionen bleiben vor dem Speichern bearbeitbar."), 12, "#91A3A5", false, new Thickness(0, 5, 0, 10)));
        var search = new TextBox { Name = "CatalogSearch", Padding = new Thickness(12, 10, 12, 10), ToolTip = L("Funktionen suchen"), MinHeight = 40 };
        header.Children.Add(FieldLabel(L("Funktionen suchen")));
        header.Children.Add(search); root.Children.Add(header);

        var body = new Grid(); Grid.SetRow(body, 1); root.Children.Add(body);
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        body.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(275) });
        var categories = new ListBox { Name = "CatalogCategories", Margin = new Thickness(0, 0, 14, 0), BorderThickness = new Thickness(0), Background = _panel };
        ScrollViewer.SetHorizontalScrollBarVisibility(categories, ScrollBarVisibility.Disabled);
        var all = L("Alle Funktionen");
        categories.Items.Add(new ListBoxItem { Content = Label(all, 12), Tag = null });
        foreach (var preset in ActionCatalog.Presets.DistinctBy(p => p.CategoryId))
            categories.Items.Add(new ListBoxItem { Content = Label(preset.Category, 12), Tag = preset.CategoryId });
        categories.SelectedIndex = 0; body.Children.Add(categories);
        var list = new ListBox { Name = "CatalogResults", Margin = new Thickness(0, 0, 14, 0), Background = _panel, BorderThickness = new Thickness(0), HorizontalContentAlignment = HorizontalAlignment.Stretch };
        ScrollViewer.SetHorizontalScrollBarVisibility(list, ScrollBarVisibility.Disabled);
        Grid.SetColumn(list, 1); body.Children.Add(list);
        var detail = new StackPanel { Margin = new Thickness(16) };
        var detailsBorder = new Border { Background = _panel, BorderBrush = _line, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(10), Child = new ScrollViewer { Content = detail, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled } };
        Grid.SetColumn(detailsBorder, 2); body.Children.Add(detailsBorder);
        var count = Label("", 11, "#91A3A5");
        var footer = new DockPanel { Margin = new Thickness(0, 16, 0, 0) };
        var buttons = new WrapPanel { HorizontalAlignment = HorizontalAlignment.Right };
        var add = Button(L("Übernehmen"), () =>
        {
            if (list.SelectedItem is ListBoxItem { Tag: ActionPreset preset }) { dialog.Close(); choose(preset); }
        }, true);
        add.IsEnabled = false;
        buttons.Children.Add(Button(L("Abbrechen"), () => dialog.Close(), false, 0));
        if (macro is not null) buttons.Children.Add(Button(L("Mehrschritt-Makro erstellen"), () => { dialog.Close(); macro(); }, false, 8));
        buttons.Children.Add(add); DockPanel.SetDock(buttons, Dock.Right); footer.Children.Add(buttons); footer.Children.Add(count);
        Grid.SetRow(footer, 2); root.Children.Add(footer);

        void Select()
        {
            detail.Children.Clear();
            if (list.SelectedItem is not ListBoxItem { Tag: ActionPreset preset })
            { add.IsEnabled = false; detail.Children.Add(Label(L("Keine Aktion gefunden"), 16, "#E8F0F1", true)); return; }
            add.IsEnabled = true;
            detail.Children.Add(Label(preset.Icon, 32, "#69D8CC", true));
            detail.Children.Add(Label(preset.Category, 11, "#69D8CC", false, new Thickness(0, 12, 0, 4)));
            detail.Children.Add(Label(preset.Name, 20, "#E8F0F1", true));
            detail.Children.Add(Label(preset.Description, 12, "#B5C2C3", false, new Thickness(0, 12, 0, 14)));
            var steps = preset.CreateSteps();
            detail.Children.Add(FieldLabel(L("Vorschau")));
            var preview = MacroPreview.Build(new Tile { Steps = steps }).Skip(1);
            foreach (var line in preview)
                detail.Children.Add(Label(line, 11, "#91A3A5", false, new Thickness(0, 5, 0, 0)));
            detail.Children.Add(Label(L("Die Auswahl führt keine Aktion aus."), 11, "#69D8CC", false, new Thickness(0, 18, 0, 0)));
        }
        void Filter()
        {
            var selectedId = (list.SelectedItem as ListBoxItem)?.Tag is ActionPreset previous ? previous.Id : null;
            list.Items.Clear();
            foreach (var preset in ActionCatalog.Search(search.Text).Where(p => categories.SelectedIndex == 0 || p.CategoryId == (categories.SelectedItem as ListBoxItem)?.Tag as string))
            {
                var card = new StackPanel { Margin = new Thickness(10, 8, 10, 8) };
                card.Children.Add(Label($"{preset.Icon}  {preset.Name}", 14, "#E8F0F1", true));
                card.Children.Add(Label(preset.Description, 11, "#91A3A5", false, new Thickness(0, 4, 0, 0)));
                list.Items.Add(new ListBoxItem { Tag = preset, Content = card, HorizontalContentAlignment = HorizontalAlignment.Stretch });
            }
            count.Text = LF($"{list.Items.Count} Funktionen");
            list.SelectedItem = list.Items.Cast<ListBoxItem>().FirstOrDefault(item => (item.Tag as ActionPreset)?.Id == selectedId) ?? list.Items.Cast<ListBoxItem>().FirstOrDefault();
            Select();
        }
        search.TextChanged += (_, _) => Filter(); categories.SelectionChanged += (_, _) => Filter(); list.SelectionChanged += (_, _) => Select();
        list.MouseDoubleClick += (_, _) =>
        {
            if (list.SelectedItem is ListBoxItem { Tag: ActionPreset preset }) { dialog.Close(); choose(preset); }
        };
        dialog.PreviewKeyDown += (_, e) =>
        {
            if (e.Key == Key.Escape) { dialog.Close(); e.Handled = true; }
            else if (e.Key == Key.Enter && list.SelectedItem is ListBoxItem { Tag: ActionPreset preset })
            { dialog.Close(); choose(preset); e.Handled = true; }
        };
        dialog.Content = new Border { Background = Brush("#101719"), Padding = new Thickness(24), Child = root };
        dialog.Loaded += (_, _) => search.Focus(); Filter(); return dialog;
    }
}

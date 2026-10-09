using System.Globalization;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Aivana.Core;
using Aivana.Windows;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private readonly KeyboardLayoutService _keyboardLayoutService = new();
    private ModifierKeys _keyboardModifiers = ModifierKeys.Control;
    private string? _keyboardSelectedPhysicalKeyId;
    private Guid? _keyboardTargetTileId;

    private UIElement BuildKeyboardLayoutPage(Profile? profile)
    {
        var settings = _controller.Settings;
        var layouts = KeyboardLayoutCatalog.All;
        var selectedLayout = layouts.FirstOrDefault(layout => layout.Id.Equals(settings.KeyboardLayoutId, StringComparison.OrdinalIgnoreCase))
            ?? layouts.First(layout => layout.Id == "generic-iso-de-fullsize");
        var root = new Grid();
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        var controlsCard = Card();
        controlsCard.Margin = new Thickness(0, 0, 0, 12);
        var controls = new StackPanel();
        var helpers = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
        helpers.Children.Add(Button(L("Sondertasten-Assistent"), ShowSpecialKeyWizard, false));
        helpers.Children.Add(Button(L("Tastatur-Overlay"), ToggleKeyboardOverlay, false, 8));
        controls.Children.Add(helpers);
        var head = new Grid();
        head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        head.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(330) });
        var title = new StackPanel();
        title.Children.Add(Label(L("TASTATURLAYOUT"), 10, "#67D8CC", true));
        title.Children.Add(Label(L("Wähle ein Referenzlayout für die Tastenansicht."), 14, "#E8F0F1", true, new Thickness(0, 4, 8, 0)));
        head.Children.Add(title);
        var layoutPicker = new ComboBox
        {
            ItemsSource = layouts.Select(layout => new KeyboardLayoutChoice(layout.Id, L(layout.DisplayName))).ToList(),
            DisplayMemberPath = nameof(KeyboardLayoutChoice.DisplayName),
            SelectedValuePath = nameof(KeyboardLayoutChoice.Id),
            SelectedValue = selectedLayout.Id,
            Margin = new Thickness(8, 0, 0, 0),
            VerticalAlignment = VerticalAlignment.Center,
            ToolTip = L("Das Layout wird in den lokalen Einstellungen gespeichert.")
        };
        layoutPicker.SelectionChanged += (_, _) =>
        {
            if (layoutPicker.SelectedValue is not string id || id == settings.KeyboardLayoutId) return;
            var previous = settings.KeyboardLayoutId;
            try
            {
                SaveSetting(() => settings.KeyboardLayoutId = id, () => settings.KeyboardLayoutId = previous);
                RenderPage(ActiveProfile);
            }
            catch (Exception ex) { ShowError(ex); }
        };
        Grid.SetColumn(layoutPicker, 1);
        head.Children.Add(layoutPicker);
        controls.Children.Add(head);

        var provenance = selectedLayout.ProvenanceDescription;
        controls.Children.Add(Label(L(selectedLayout.ProvenanceDescription) + " " + L("Das Sprach- und Formlayout wird ausdrücklich gewählt; es wird nicht automatisch aus einem angeschlossenen Gerät abgeleitet."), 10, "#91A3A5", false, new Thickness(0, 8, 0, 0)));

        var assignment = new Grid { Margin = new Thickness(0, 10, 0, 0) };
        assignment.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        assignment.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(285) });
        var left = new StackPanel();
        left.Children.Add(Label(L("MODIFIKATOREN FÜR DIE TASTENAUSWAHL"), 9, "#829597", true));
        var modifierPanel = new WrapPanel { Margin = new Thickness(0, 3, 0, 0) };
        var modifierChecks = new List<(CheckBox Check, string Token)>();
        foreach (var (label, token, flag) in new[]
        {
            ("Ctrl", "Ctrl", ModifierKeys.Control), ("Alt", "Alt", ModifierKeys.Alt), ("Shift", "Shift", ModifierKeys.Shift), ("Win", "Win", ModifierKeys.Windows)
        })
        {
            var check = new CheckBox { Content = label, IsChecked = (_keyboardModifiers & flag) != 0, Margin = new Thickness(0, 0, 12, 0), ToolTip = LF($"{label} als Teil des vorgeschlagenen Hotkeys verwenden.") };
            modifierChecks.Add((check, token));
            modifierPanel.Children.Add(check);
        }
        left.Children.Add(modifierPanel);
        left.Children.Add(Label(L("Die Auswahl erstellt keine Aktion. Ein bestehender Hotkey öffnet seine Aktion; ein neuer Vorschlag bleibt ungespeichert, bis du ihn im Editor speicherst."), 10, "#91A3A5", false, new Thickness(0, 4, 12, 0)));
        assignment.Children.Add(left);

        var right = new StackPanel();
        right.Children.Add(Label(L("HOTKEY-VORSCHLAG"), 9, "#829597", true));
        var proposal = new TextBox { IsReadOnly = true, Text = L("Taste anklicken"), Margin = new Thickness(0, 3, 0, 5), ToolTip = L("Wird erst nach Speichern im Editor übernommen.") };
        right.Children.Add(proposal);
        var targetPicker = new ComboBox
        {
            ItemsSource = profile?.Tiles.Select(tile => new KeyboardTileChoice(tile, $"{tile.Name} · {L(tile.Enabled ? "aktiv" : "deaktiviert")}")).ToList() ?? [],
            DisplayMemberPath = nameof(KeyboardTileChoice.Label),
            SelectedValuePath = nameof(KeyboardTileChoice.Id),
            SelectedValue = profile?.Tiles.Any(tile => tile.Id == _keyboardTargetTileId) == true ? _keyboardTargetTileId : null,
            IsEditable = true,
            IsReadOnly = true,
            Text = profile?.Tiles.Any(tile => tile.Id == _keyboardTargetTileId) == true ? string.Empty : L("Zielaktion wählen"),
            Margin = new Thickness(0, 0, 0, 5),
            ToolTip = L("Zielaktion auswählen; das Anklicken einer Taste ändert keine gespeicherte Aktion.")
        };
        right.Children.Add(targetPicker);
        right.Children.Add(Label(L("Corsair-Tastatur für G-Tasten"), 9, "#829597", true));
        var corsairDevices = _controller.CorsairKeyboards
            .Where(device => _controller.GetCorsairMacroKeys(device.DeviceId).Count > 0)
            .Select(device => new CorsairKeyboardChoice(device.DeviceId, LF($"{device.Name} · {device.DeviceId}")))
            .ToList();
        var corsairDevicePicker = new ComboBox
        {
            ItemsSource = corsairDevices,
            DisplayMemberPath = nameof(CorsairKeyboardChoice.Name),
            SelectedValuePath = nameof(CorsairKeyboardChoice.DeviceId),
            SelectedValue = profile?.Tiles.FirstOrDefault(tile => tile.Id == _keyboardTargetTileId)?.CorsairBinding?.DeviceId
                ?? corsairDevices.FirstOrDefault()?.DeviceId,
            Margin = new Thickness(0, 2, 0, 5),
            IsEnabled = _controller.CorsairKeysAvailable && corsairDevices.Count > 0,
            ToolTip = L("Wähle ein verbundenes Gerät mit unterstützten Corsair-G-Tasten aus.")
        };
        right.Children.Add(corsairDevicePicker);
        var corsairStatus = Label(_controller.CorsairKeyStatus, 10, "#91A3A5", false, new Thickness(0, 0, 0, 5));
        corsairStatus.TextWrapping = TextWrapping.Wrap;
        right.Children.Add(corsairStatus);
        var prepare = Button(L("Im Editor vorbereiten"), () =>
        {
            if (profile != null && targetPicker.SelectedItem is KeyboardTileChoice corsairTarget && proposal.Tag is CorsairKeyProposal corsairProposal)
            {
                PrepareCorsairMacroKeyDraft(profile, corsairTarget.Tile.Id, corsairProposal.DeviceId, corsairProposal.KeyId);
                return;
            }
            if (profile == null || targetPicker.SelectedItem is not KeyboardTileChoice target || proposal.Tag is not string hotkey)
            {
                FooterStatus.Text = L("Wähle zuerst eine Taste mit gültigem Hotkey und eine Zielaktion aus.");
                return;
            }
            PrepareKeyboardHotkeyDraft(profile, target.Tile.Id, hotkey);
        }, false, 0);
        prepare.IsEnabled = false;
        right.Children.Add(prepare);
        Grid.SetColumn(right, 1);
        assignment.Children.Add(right);
        controls.Children.Add(assignment);

        var status = Label(L("Klicke eine Taste, um ihre Beschriftung und Windows-Zuordnung zu sehen."), 10, "#91A3A5", false, new Thickness(0, 7, 0, 0));
        controls.Children.Add(status);
        Grid.SetRow(controlsCard, 0);
        controlsCard.Child = controls;
        root.Children.Add(controlsCard);

        var keyboardCard = Card();
        Grid.SetRow(keyboardCard, 1);
        var unit = 44d;
        var inset = 5d;
        var minX = selectedLayout.Keys.Min(key => key.X);
        var minY = selectedLayout.Keys.Min(key => key.Y);
        var maxX = selectedLayout.Keys.Max(key => key.X + key.Width);
        var maxY = selectedLayout.Keys.Max(key => key.Y + key.Height);
        var canvas = new Canvas
        {
            Width = (maxX - minX) * unit + inset * 2,
            Height = (maxY - minY) * unit + inset * 2,
            Background = Brushes.Transparent,
            ClipToBounds = false
        };

        KeyboardKeyGeometry? selectedKey = null;
        KeyboardKeyMapping? selectedMapping = null;
        void UpdateProposal()
        {
            var corsairKeyId = selectedKey == null ? null : GetCorsairGKeyId(selectedKey.PhysicalKeyId);
            if (selectedKey != null && corsairKeyId is { } gKeyId)
            {
                var deviceId = corsairDevicePicker.SelectedValue as string;
                var supported = !string.IsNullOrEmpty(deviceId) && _controller.CorsairKeysAvailable
                    && _controller.GetCorsairMacroKeys(deviceId).Contains(gKeyId);
                proposal.Text = LF($"Corsair G{gKeyId}");
                var canPrepareCorsair = supported && targetPicker.SelectedItem is KeyboardTileChoice;
                proposal.Tag = canPrepareCorsair ? new CorsairKeyProposal(deviceId!, gKeyId) : null;
                prepare.IsEnabled = canPrepareCorsair;
                status.Text = canPrepareCorsair
                    ? L("Corsair-G-Taste und Zielaktion sind ausgewählt. Die Zuordnung wird als ungespeicherter Entwurf vorbereitet.")
                    : supported ? L("Wähle eine Zielaktion, um diese Corsair-G-Taste im Editor vorzubereiten.")
                    : L("Corsair-G-Tasten: ") + _controller.CorsairKeyStatus;
                status.ToolTip = BuildKeyboardKeyToolTip(selectedKey, null);
                return;
            }
            if (selectedKey == null || selectedMapping == null)
            {
                proposal.Text = L("Taste anklicken");
                proposal.Tag = null;
                prepare.IsEnabled = false;
                return;
            }

            var modifiers = modifierChecks.Where(entry => entry.Check.IsChecked == true).Select(entry => entry.Token).ToList();
            var candidateParts = modifiers.Append(selectedMapping.HotkeyToken).ToArray();
            var candidate = string.Join("+", candidateParts);
            var valid = modifiers.Count > 0 && HotkeyParser.TryParse(candidate, out _, out _, out _);
            proposal.Text = candidate;
            proposal.Tag = valid ? candidate : null;
            prepare.IsEnabled = valid && targetPicker.SelectedItem is KeyboardTileChoice;

            var notes = new List<string> { LF($"Gewählt: {FormatPrintedLegend(selectedKey)} · Windows-Hotkey: {candidate}.") };
            if (selectedMapping.SharesGlobalHotkeyToken) notes.Add(L("Haupt-Enter und Nummernblock-Enter teilen eine Hotkey-Kennung und sind nicht getrennt belegbar."));
            var matches = FindKeyboardHotkeyMatches(profile, selectedMapping.VirtualKey, modifiers);
            if (matches.Count == 1)
            {
                notes.Add(LF($"Belegt: {matches[0].Name}. Klicke erneut, um diese Aktion im Editor zu öffnen."));
            }
            else if (matches.Count > 1)
            {
                notes.Add(L("Mehrere Aktionen verwenden diese Tastenkombination. Wähle eine Zielaktion für die Bearbeitung."));
            }
            else
            {
                notes.Add(L("Noch keiner aktiven Aktion zugeordnet. Ein Vorschlag wird nicht automatisch gespeichert."));
            }
            status.Text = string.Join(" ", notes);
            status.ToolTip = BuildKeyboardKeyToolTip(selectedKey, selectedMapping);
        }

        foreach (var modifier in modifierChecks)
        {
            var flag = modifier.Token switch { "Ctrl" => ModifierKeys.Control, "Alt" => ModifierKeys.Alt, "Shift" => ModifierKeys.Shift, _ => ModifierKeys.Windows };
            modifier.Check.Checked += (_, _) => { _keyboardModifiers |= flag; UpdateProposal(); };
            modifier.Check.Unchecked += (_, _) => { _keyboardModifiers &= ~flag; UpdateProposal(); };
        }
        targetPicker.SelectionChanged += (_, _) =>
        {
            _keyboardTargetTileId = (targetPicker.SelectedItem as KeyboardTileChoice)?.Id;
            var bindingDeviceId = (targetPicker.SelectedItem as KeyboardTileChoice)?.Tile.CorsairBinding?.DeviceId;
            if (!string.IsNullOrEmpty(bindingDeviceId) && corsairDevices.Any(device => device.DeviceId.Equals(bindingDeviceId, StringComparison.OrdinalIgnoreCase)))
                corsairDevicePicker.SelectedValue = bindingDeviceId;
            if (selectedKey != null) UpdateProposal();
        };
        corsairDevicePicker.SelectionChanged += (_, _) => { if (selectedKey != null) UpdateProposal(); };

        void SelectPhysicalKey(KeyboardKeyGeometry key, KeyboardKeyMapping mapping, bool activateMatchingAction)
        {
            _keyboardSelectedPhysicalKeyId = key.PhysicalKeyId;
            selectedKey = key;
            selectedMapping = mapping;
            UpdateProposal();
            if (!activateMatchingAction) return;
            var modifiers = modifierChecks.Where(entry => entry.Check.IsChecked == true).Select(entry => entry.Token).ToList();
            var assigned = FindKeyboardHotkeyMatches(profile, mapping.VirtualKey, modifiers);
            if (assigned.Count == 1 && profile != null) OpenKeyboardAction(profile, assigned[0]);
        }

        foreach (var key in selectedLayout.Keys)
        {
            var isCorsairMacroKey = GetCorsairGKeyId(key.PhysicalKeyId) is not null;
            var hasMapping = TryResolveKeyboardMapping(key, selectedLayout, out var mapping);
            var matches = hasMapping ? FindKeyboardHotkeyMatches(profile, mapping.VirtualKey, modifiers: null) : [];
            var keyButton = new Button
            {
                Width = Math.Max(22, key.Width * unit - 3),
                Height = Math.Max(22, key.Height * unit - 3),
                Padding = new Thickness(3),
                Margin = new Thickness(0),
                Background = matches.Count > 0 ? Brush("#214642") : Brush("#20292B"),
                BorderBrush = matches.Count > 0 ? _accent : _line,
                Foreground = matches.Count > 0 ? Brush("#67E0D4") : Brush("#DCE6E7"),
                FontSize = Math.Clamp(11 / Math.Max(1, key.Width / 1.4), 7, 11),
                IsEnabled = hasMapping || isCorsairMacroKey,
                Focusable = hasMapping || isCorsairMacroKey,
                Clip = CreateKeyboardKeyClip(key.Shape, Math.Max(22, key.Width * unit - 3), Math.Max(22, key.Height * unit - 3)),
                ToolTip = BuildKeyboardKeyToolTip(key, hasMapping ? mapping : null),
                Tag = key.PhysicalKeyId
            };
            AutomationProperties.SetName(keyButton, $"{FormatPrintedLegend(key)} · {key.PhysicalKeyId}");
            ToolTipService.SetShowOnDisabled(keyButton, true);
            keyButton.Content = BuildKeyboardKeyContent(key, hasMapping ? mapping : null);
            if (isCorsairMacroKey)
            {
                keyButton.Click += (_, _) =>
                {
                    _keyboardSelectedPhysicalKeyId = key.PhysicalKeyId;
                    selectedKey = key;
                    selectedMapping = null;
                    UpdateProposal();
                };
            }
            else if (hasMapping)
            {
                keyButton.Click += (_, _) =>
                {
                    if (TryGetModifierFlag(mapping.VirtualKey, out var modifierFlag))
                    {
                        var modifierCheck = modifierChecks.FirstOrDefault(entry => entry.Token == ModifierToken(modifierFlag)).Check;
                        if (modifierCheck != null) modifierCheck.IsChecked = modifierCheck.IsChecked != true;
                        status.Text = LF($"{FormatPrintedLegend(key)} ändert die Modifikatorauswahl oben.");
                        return;
                    }
                    SelectPhysicalKey(key, mapping, activateMatchingAction: true);
                };
            }
            Canvas.SetLeft(keyButton, (key.X - minX) * unit + inset);
            Canvas.SetTop(keyButton, (key.Y - minY) * unit + inset);
            canvas.Children.Add(keyButton);
        }

        if (_keyboardSelectedPhysicalKeyId is { } selectedPhysicalKeyId)
        {
            var savedKey = selectedLayout.Keys.FirstOrDefault(key => key.PhysicalKeyId == selectedPhysicalKeyId);
            if (savedKey != null && TryResolveKeyboardMapping(savedKey, selectedLayout, out var savedMapping))
                SelectPhysicalKey(savedKey, savedMapping, activateMatchingAction: false);
            else _keyboardSelectedPhysicalKeyId = null;
        }

        var view = new Viewbox { Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Child = canvas, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
        keyboardCard.Child = view;
        root.Children.Add(keyboardCard);
        return root;
    }

    private static string FormatPrintedLegend(KeyboardKeyGeometry key) =>
        string.IsNullOrWhiteSpace(key.BottomLegend) ? key.TopLegend : $"{key.TopLegend} / {key.BottomLegend}";

    private static List<string> GetModifierTokens(ModifierKeys modifiers) =>
        new[]
        {
            (ModifierKeys.Control, "Ctrl"), (ModifierKeys.Alt, "Alt"),
            (ModifierKeys.Shift, "Shift"), (ModifierKeys.Windows, "Win")
        }.Where(pair => (modifiers & pair.Item1) != 0).Select(pair => pair.Item2).ToList();

    private static bool TryGetModifierFlag(uint virtualKey, out ModifierKeys flag)
    {
        flag = virtualKey switch
        {
            0x10 or 0xA0 or 0xA1 => ModifierKeys.Shift,
            0x11 or 0xA2 or 0xA3 => ModifierKeys.Control,
            0x12 or 0xA4 or 0xA5 => ModifierKeys.Alt,
            0x5B or 0x5C => ModifierKeys.Windows,
            _ => ModifierKeys.None
        };
        return flag != ModifierKeys.None;
    }

    private static string ModifierToken(ModifierKeys flag) => flag switch
    {
        ModifierKeys.Control => "Ctrl",
        ModifierKeys.Alt => "Alt",
        ModifierKeys.Shift => "Shift",
        _ => "Win"
    };

    private static UIElement BuildKeyboardKeyContent(KeyboardKeyGeometry key, KeyboardKeyMapping? mapping)
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var legend = new TextBlock
        {
            Text = string.IsNullOrWhiteSpace(key.BottomLegend) ? key.TopLegend : $"{key.TopLegend}\n{key.BottomLegend}",
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = key.Shape == KeyboardKeyShape.IsoEnterL ? HorizontalAlignment.Right : HorizontalAlignment.Center,
            VerticalAlignment = key.Shape == KeyboardKeyShape.IsoEnterL ? VerticalAlignment.Bottom : VerticalAlignment.Center,
            FontWeight = FontWeights.SemiBold,
            FontSize = key.Width < .85 ? 8 : 10,
            Margin = key.Shape == KeyboardKeyShape.IsoEnterL ? new Thickness(0, 0, 9, 8) : new Thickness(1)
        };
        grid.Children.Add(legend);
        var visibleLetterMismatch = mapping != null
            && key.TopLegend.Length == 1 && char.IsLetter(key.TopLegend[0])
            && mapping.Legend.Length == 1 && char.IsLetter(mapping.Legend[0])
            && !key.TopLegend.Equals(mapping.Legend, StringComparison.OrdinalIgnoreCase);
        if (visibleLetterMismatch && mapping != null)
        {
            var osLegend = new TextBlock
            {
                Text = mapping.Legend,
                FontSize = 10,
                Foreground = Brush("#65D6CA"),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 2, 1),
                ToolTip = LF($"Gedruckte Taste {key.TopLegend}; Windows-Ausgabe {mapping.Legend} (Umschalt: {mapping.ShiftedLegend})")
            };
            Grid.SetRow(osLegend, 1);
            grid.Children.Add(osLegend);
        }
        return grid;
    }

    private static string BuildKeyboardKeyToolTip(KeyboardKeyGeometry key, KeyboardKeyMapping? mapping)
    {
        var lines = new List<string> { FormatPrintedLegend(key), key.PhysicalKeyId };
        if (!string.IsNullOrWhiteSpace(key.Tooltip)) lines.Add(L(key.Tooltip));
        if (mapping != null)
        {
            lines.Add(mapping.ScanCode == 0
                ? LF($"Katalog-Hotkey: {mapping.HotkeyToken} (ohne individuellen Windows-Scan-Code)")
                : LF($"Windows: {mapping.HotkeyToken} · {mapping.Legend} · Umschalt: {mapping.ShiftedLegend}"));
            if (mapping.ScanCode != 0) lines.Add(LF($"Tastaturlayout: {mapping.LayoutId}"));
            if (mapping.IsDeadKey || mapping.ShiftedIsDeadKey) lines.Add(L("Tottaste: Hotkeys beziehen sich auf die physische Taste."));
            if (mapping.SharesGlobalHotkeyToken) lines.Add(L("Diese Position teilt sich eine globale Hotkey-Kennung mit einer weiteren Taste und ist nicht separat unterscheidbar."));
        }
        else if (key.AvailableForHotkey)
        {
            lines.Add(L("Windows kann diese physische Position derzeit keiner Hotkey-Taste zuordnen."));
        }
        else
        {
            lines.Add(L("Diese Gerätefunktion ist nicht als normales Windows-Hotkeyereignis verfügbar."));
        }
        return string.Join(Environment.NewLine, lines);
    }

    private static Geometry CreateKeyboardKeyClip(KeyboardKeyShape shape, double width, double height)
    {
        if (shape is KeyboardKeyShape.Circle or KeyboardKeyShape.Wheel)
            return new EllipseGeometry(new Rect(0, 0, width, height));
        if (shape != KeyboardKeyShape.IsoEnterL)
            return new RectangleGeometry(new Rect(0, 0, width, height), 6, 6);

        var inset = width / 6;
        var path = new StreamGeometry();
        using (var context = path.Open())
        {
            context.BeginFigure(new Point(0, 0), isFilled: true, isClosed: true);
            context.LineTo(new Point(width, 0), isStroked: true, isSmoothJoin: false);
            context.LineTo(new Point(width, height), isStroked: true, isSmoothJoin: false);
            context.LineTo(new Point(inset, height), isStroked: true, isSmoothJoin: false);
            context.LineTo(new Point(inset, height * .5), isStroked: true, isSmoothJoin: false);
            context.LineTo(new Point(0, height * .5), isStroked: true, isSmoothJoin: false);
        }
        if (path.CanFreeze) path.Freeze();
        return path;
    }

    private bool TryResolveKeyboardMapping(KeyboardKeyGeometry key, KeyboardLayoutDefinition layout, out KeyboardKeyMapping mapping)
    {
        if (key.AvailableForHotkey && key.ScanCode != 0 && _keyboardLayoutService.TryGetMapping(key.ScanCode, key.Extended, out mapping))
            return true;

        // Pause uses an E1 sequence rather than a single Set-1 scan code. Its catalog token is explicit and
        // remains selectable without pretending that Windows resolved a scan code for it.
        if (key.AvailableForHotkey
            && key.ScanCode == 0
            && !string.IsNullOrWhiteSpace(key.HotkeyOutput)
            && HotkeyParser.TryParseShortcut(key.HotkeyOutput, out _, out var virtualKey, out _))
        {
            mapping = new KeyboardKeyMapping(0, false, virtualKey, key.HotkeyOutput, key.TopLegend, key.TopLegend,
                false, false, false, LF($"Katalog · {layout.Locale}"));
            return true;
        }

        mapping = null!;
        return false;
    }

    private List<Tile> FindKeyboardHotkeyMatches(Profile? profile, uint virtualKey, IReadOnlyCollection<string>? modifiers)
    {
        if (profile == null) return [];
        var expected = new List<uint>();
        if (modifiers != null)
        {
            if (modifiers.Contains("Ctrl", StringComparer.Ordinal)) expected.Add(HotkeyParser.ModControl);
            if (modifiers.Contains("Alt", StringComparer.Ordinal)) expected.Add(HotkeyParser.ModAlt);
            if (modifiers.Contains("Shift", StringComparer.Ordinal)) expected.Add(HotkeyParser.ModShift);
            if (modifiers.Contains("Win", StringComparer.Ordinal)) expected.Add(HotkeyParser.ModWin);
        }
        return profile.Tiles.Where(tile => tile.Enabled && HotkeyParser.TryParse(tile.Hotkey, out var actualModifiers, out var actualKey, out _)
            && actualKey == virtualKey && (modifiers == null || actualModifiers == expected.Aggregate(0u, (combined, flag) => combined | flag))).ToList();
    }

    private void OpenKeyboardAction(Profile profile, Tile tile)
    {
        if (!ResolvePendingDraft()) return;
        var currentProfile = _controller.Settings.Profiles.FirstOrDefault(candidate => candidate.Id == profile.Id);
        var currentTile = currentProfile?.Tiles.FirstOrDefault(candidate => candidate.Id == tile.Id);
        if (currentProfile == null || currentTile == null) return;
        _selectedTileId = currentTile.Id;
        _draftProfileId = currentProfile.Id;
        _draftTile = Clone(currentTile);
        _selectedStep = 0;
        _page = "Dashboard";
        RenderPage(ActiveProfile);
        FooterStatus.Text = LF($"{currentTile.Name} wurde im Editor geöffnet. Der gespeicherte Hotkey wurde nicht geändert.");
    }

    private void PrepareKeyboardHotkeyDraft(Profile profile, Guid tileId, string candidate)
    {
        if (!HotkeyParser.TryParse(candidate, out _, out _, out var error))
        {
            FooterStatus.Text = error;
            return;
        }
        if (!ResolvePendingDraft()) return;
        var currentProfile = _controller.Settings.Profiles.FirstOrDefault(candidate => candidate.Id == profile.Id);
        var persisted = currentProfile?.Tiles.FirstOrDefault(tile => tile.Id == tileId);
        if (currentProfile == null || persisted == null) return;
        _selectedTileId = persisted.Id;
        _draftProfileId = currentProfile.Id;
        _draftTile = Clone(persisted);
        _draftBaseline = SerializeTile(persisted);
        _draftBaselineTileId = persisted.Id;
        _draftBaselineProfileId = profile.Id;
        _draftTile.Hotkey = candidate;
        _selectedStep = 0;
        _page = "Dashboard";
        RenderPage(ActiveProfile);
        FooterStatus.Text = LF($"Hotkey-Vorschlag für {persisted.Name} ist ein ungespeicherter Entwurf. Prüfe ihn und speichere im Editor.");
    }

    private void PrepareCorsairMacroKeyDraft(Profile profile, Guid tileId, string deviceId, int keyId)
    {
        if (!_controller.CorsairKeysAvailable || !_controller.GetCorsairMacroKeys(deviceId).Contains(keyId))
        {
            FooterStatus.Text = L("Corsair-G-Tasten: ") + _controller.CorsairKeyStatus;
            return;
        }
        if (!ResolvePendingDraft()) return;
        var currentProfile = _controller.Settings.Profiles.FirstOrDefault(candidate => candidate.Id == profile.Id);
        var persisted = currentProfile?.Tiles.FirstOrDefault(tile => tile.Id == tileId);
        if (currentProfile == null || persisted == null) return;
        _selectedTileId = persisted.Id;
        _draftProfileId = currentProfile.Id;
        _draftTile = Clone(persisted);
        _draftBaseline = SerializeTile(persisted);
        _draftBaselineTileId = persisted.Id;
        _draftBaselineProfileId = currentProfile.Id;
        _draftTile.CorsairBinding = new CorsairBinding(deviceId, keyId);
        _selectedStep = 0;
        _page = "Dashboard";
        RenderPage(ActiveProfile);
        FooterStatus.Text = LF($"Corsair G{keyId} ist ein ungespeicherter Entwurf für {persisted.Name}. Prüfe ihn und speichere im Editor.");
    }

    private static int? GetCorsairGKeyId(string physicalKeyId)
    {
        const string prefix = "CORSAIR:K100:G";
        return physicalKeyId.StartsWith(prefix, StringComparison.Ordinal)
            && int.TryParse(physicalKeyId.AsSpan(prefix.Length), out var keyId) && keyId is >= 1 and <= 6
                ? keyId : null;
    }

    internal void SelectKeyboardLayoutForSmoke(string layoutId)
    {
        if (!KeyboardLayoutCatalog.TryGet(layoutId, out _)) throw new ArgumentOutOfRangeException(nameof(layoutId));
        _controller.Settings.KeyboardLayoutId = layoutId;
        _page = "Tastatur";
        RenderPage(ActiveProfile);
    }

    public void RunKeyboardSmokeChecks()
    {
        var originalLayoutId = _controller.Settings.KeyboardLayoutId;
        var profile = ActiveProfile ?? throw new InvalidOperationException("Kein Profil für den Tastaturlayout-Smokecheck vorhanden.");
        var originalSelection = _selectedTileId;
        var originalDraft = _draftTile == null ? null : Clone(_draftTile);
        var originalDraftProfile = _draftProfileId;
        var originalBaseline = _draftBaseline;
        var originalBaselineTile = _draftBaselineTileId;
        var originalBaselineProfile = _draftBaselineProfileId;
        var originalPage = _page;
        var originalModifiers = _keyboardModifiers;
        var originalSelectedPhysicalKeyId = _keyboardSelectedPhysicalKeyId;
        var originalKeyboardTargetId = _keyboardTargetTileId;
        var originalTargetIndex = -1;
        Tile? originalTarget = null;
        try
        {
            foreach (var layout in KeyboardLayoutCatalog.All)
            {
                _controller.Settings.KeyboardLayoutId = layout.Id;
                var renderedPage = BuildKeyboardLayoutPage(profile);
                if (renderedPage is not Grid) throw new InvalidOperationException($"Layout {layout.Id} wurde nicht gerendert.");
                var isoEnter = layout.Keys.FirstOrDefault(key => key.Shape == KeyboardKeyShape.IsoEnterL);
                if (layout.Locale == "ISO-DE" && isoEnter == null) throw new InvalidOperationException($"ISO-Enter fehlt in {layout.Id}.");
                if (layout.Locale == "ANSI-US" && isoEnter != null) throw new InvalidOperationException($"ANSI-Layout {layout.Id} enthält fälschlich eine ISO-Enter-Taste.");
                if (isoEnter != null)
                {
                    var isoClip = CreateKeyboardKeyClip(isoEnter.Shape, isoEnter.Width * 44, isoEnter.Height * 44);
                    if (isoClip.FillContains(new Point(5, 70)) || !isoClip.FillContains(new Point(42, 70)))
                        throw new InvalidOperationException($"ISO-Enter-Aussparung ist falsch ausgerichtet in {layout.Id}.");
                }
                if (layout.FormFactor == KeyboardFormFactor.FullSize)
                {
                    var expectedScans = new Dictionary<string, ushort>(StringComparer.Ordinal)
                    {
                        ["HID:0x5F"] = 0x47, ["HID:0x60"] = 0x48, ["HID:0x61"] = 0x49,
                        ["HID:0x5C"] = 0x4B, ["HID:0x5D"] = 0x4C, ["HID:0x5E"] = 0x4D,
                        ["HID:0x59"] = 0x4F, ["HID:0x5A"] = 0x50, ["HID:0x5B"] = 0x51,
                        ["HID:0x62"] = 0x52
                    };
                    foreach (var (physicalId, scanCode) in expectedScans)
                    {
                        var key = layout.Keys.FirstOrDefault(candidate => candidate.PhysicalKeyId == physicalId)
                            ?? throw new InvalidOperationException($"Numpadtaste {physicalId} fehlt in {layout.Id}.");
                        if (key.ScanCode != scanCode) throw new InvalidOperationException($"Numpadtaste {physicalId} hat den falschen Scan-Code in {layout.Id}.");
                    }
                }
                if (layout.Id.StartsWith("corsair-k100-", StringComparison.Ordinal))
                {
                    foreach (var vendorKey in layout.Keys.Where(key => key.PhysicalKeyId is "CORSAIR:K100:G1" or "CORSAIR:K100:G2" or "CORSAIR:K100:G3" or "CORSAIR:K100:G4" or "CORSAIR:K100:G5" or "CORSAIR:K100:G6" or "CORSAIR:K100:PROFILE" or "CORSAIR:K100:ICUE_WHEEL" or "CORSAIR:K100:WIN_LOCK" or "CORSAIR:K100:VOLUME_ROLLER"))
                    {
                        if (vendorKey.AvailableForHotkey || !string.IsNullOrEmpty(vendorKey.HotkeyOutput))
                            throw new InvalidOperationException($"K100-Gerätefunktion {vendorKey.PhysicalKeyId} wurde fälschlich als Hotkey angeboten.");
                    }
                    if (!layout.Keys.Where(key => key.PhysicalKeyId.StartsWith("CORSAIR:K100:MEDIA_", StringComparison.Ordinal) || key.PhysicalKeyId == "CORSAIR:K100:VOLUME_MUTE")
                        .All(key => key.AvailableForHotkey && HotkeyParser.TryParseShortcut(key.HotkeyOutput, out _, out _, out _)))
                        throw new InvalidOperationException("K100-Medientasten verwenden keine gültigen Windows-Media-Hotkeys.");
                }
            }

            var target = profile.Tiles.FirstOrDefault(tile => tile.Enabled)
                ?? throw new InvalidOperationException("Für den Tastatur-Smokecheck fehlt eine aktive Aktion.");
            originalTargetIndex = profile.Tiles.FindIndex(tile => tile.Id == target.Id);
            originalTarget = Clone(target);
            var replacement = Clone(target);
            replacement.Name = "Tastatur-Smoke Ersatzobjekt";
            replacement.Hotkey = "Ctrl+Alt+F24";
            profile.Tiles[originalTargetIndex] = replacement;
            var testLayout = KeyboardLayoutCatalog.Get("generic-ansi-us-fullsize");
            var testKey = testLayout.Keys.FirstOrDefault(key => key.PhysicalKeyId == "HID:0x04")
                ?? throw new InvalidOperationException("ANSI-Tastatur enthält keine physische A-Taste.");
            if (!_keyboardLayoutService.TryGetMapping(testKey.ScanCode, testKey.Extended, out var mapping))
                throw new InvalidOperationException("Windows konnte die physische A-Taste nicht zuordnen.");
            var beforeIds = profile.Tiles.Select(tile => tile.Id).ToHashSet();
            var modifierCandidates = new[]
            {
                ModifierKeys.Control, ModifierKeys.Alt, ModifierKeys.Shift, ModifierKeys.Windows,
                ModifierKeys.Control | ModifierKeys.Shift, ModifierKeys.Control | ModifierKeys.Alt,
                ModifierKeys.Alt | ModifierKeys.Shift, ModifierKeys.Control | ModifierKeys.Windows,
                ModifierKeys.Alt | ModifierKeys.Windows, ModifierKeys.Shift | ModifierKeys.Windows,
                ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift,
                ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Windows,
                ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Windows,
                ModifierKeys.Alt | ModifierKeys.Shift | ModifierKeys.Windows,
                ModifierKeys.Control | ModifierKeys.Alt | ModifierKeys.Shift | ModifierKeys.Windows
            };
            _keyboardModifiers = modifierCandidates.FirstOrDefault(candidateModifiers =>
                FindKeyboardHotkeyMatches(profile, mapping.VirtualKey, GetModifierTokens(candidateModifiers)).Count == 0);
            if (_keyboardModifiers == ModifierKeys.None)
                throw new InvalidOperationException("Kein konfliktfreier Modifikator für den Tastatur-Smokecheck gefunden.");

            var page = BuildKeyboardLayoutPage(profile);
            PageHost.Content = page;
            UpdateLayout();
            var comboBoxes = FindKeyboardVisualChildren<ComboBox>(page).ToList();
            var targetPicker = comboBoxes.FirstOrDefault(combo => combo.ItemsSource is IEnumerable<KeyboardTileChoice>)
                ?? throw new InvalidOperationException("Zielaktionsauswahl im Tastatur-Smokecheck fehlt.");
            targetPicker.SelectedItem = targetPicker.ItemsSource is IEnumerable<KeyboardTileChoice> choices
                ? choices.First(choice => choice.Id == target.Id)
                : null;
            var keyButton = FindKeyboardVisualChildren<Button>(page).FirstOrDefault(button => button.Tag as string == testKey.PhysicalKeyId)
                ?? throw new InvalidOperationException("Klickbare A-Taste im Tastatur-Smokecheck fehlt.");
            keyButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            var proposal = FindKeyboardVisualChildren<TextBox>(page).FirstOrDefault()
                ?? throw new InvalidOperationException("Hotkey-Vorschlag im Tastatur-Smokecheck fehlt.");
            var candidate = proposal.Text;
            var prepare = FindKeyboardVisualChildren<Button>(page).FirstOrDefault(button => button.Content as string == "Im Editor vorbereiten")
                ?? throw new InvalidOperationException("Schaltfläche zum Vorbereiten des Hotkeys fehlt.");
            if (!prepare.IsEnabled) throw new InvalidOperationException($"Tastenklick hat keinen gültigen Hotkey-Vorschlag erzeugt: Vorschlag='{proposal.Text}', Ziel='{targetPicker.SelectedValue}', Element='{targetPicker.SelectedItem?.GetType().Name}', Status='{FooterStatus.Text}'.");
            prepare.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            var currentSavedTarget = profile.Tiles.First(tile => tile.Id == target.Id);
            if (_draftTile?.Id != target.Id || _draftTile.Name != replacement.Name || _draftTile.Hotkey != candidate || !HasUnsavedChanges)
                throw new InvalidOperationException("Tastenauswahl hat keinen ungespeicherten Hotkey-Entwurf vorbereitet.");
            if (!profile.Tiles.Select(tile => tile.Id).ToHashSet().SetEquals(beforeIds) || currentSavedTarget.Name != replacement.Name || currentSavedTarget.Hotkey != replacement.Hotkey)
                throw new InvalidOperationException("Tastenauswahl hat eine gespeicherte Aktion geändert.");

            _controller.Settings.KeyboardLayoutId = "corsair-k100-iso-de";
            var k100 = BuildKeyboardLayoutPage(profile);
            var vendor = KeyboardLayoutCatalog.Get("corsair-k100-iso-de").Keys.First(key => key.PhysicalKeyId == "CORSAIR:K100:G1");
            if (vendor.AvailableForHotkey || k100 is not Grid)
                throw new InvalidOperationException("K100-G-Taste wird fälschlich als normaler Hotkey gebunden.");
            _keyboardTargetTileId = target.Id;
            PageHost.Content = k100;
            UpdateLayout();
            var g1Button = FindKeyboardVisualChildren<Button>(k100).FirstOrDefault(button => button.Tag as string == "CORSAIR:K100:G1")
                ?? throw new InvalidOperationException("K100-G1-Taste fehlt in der klickbaren Tastaturansicht.");
            var runsBeforeG1Click = _controller.GetRunCount(target.Id);
            g1Button.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            if (_controller.GetRunCount(target.Id) != runsBeforeG1Click || _controller.IsRunning(target.Id))
                throw new InvalidOperationException("Der physische K100-G1-Klick hat unerwartet eine Aktion ausgeführt.");

            var supportedDevice = _controller.CorsairKeyboards.FirstOrDefault(device => _controller.GetCorsairMacroKeys(device.DeviceId).Contains(1));
            if (_controller.CorsairKeysAvailable && supportedDevice != null)
            {
                DiscardDraft();
                var savedBinding = target.CorsairBinding;
                PrepareCorsairMacroKeyDraft(profile, target.Id, supportedDevice.DeviceId, 1);
                if (_draftTile?.CorsairBinding != new CorsairBinding(supportedDevice.DeviceId, 1)
                    || !HasUnsavedChanges || target.CorsairBinding != savedBinding
                    || _controller.GetRunCount(target.Id) != runsBeforeG1Click)
                    throw new InvalidOperationException("Die Corsair-G1-Auswahl hat die gespeicherte Aktion geändert oder keine sichere Zuordnung im Entwurf vorbereitet.");
            }
        }
        finally
        {
            _controller.Settings.KeyboardLayoutId = originalLayoutId;
            _selectedTileId = originalSelection;
            _draftProfileId = originalDraftProfile;
            _draftTile = originalDraft;
            _draftBaseline = originalBaseline;
            _draftBaselineTileId = originalBaselineTile;
            _draftBaselineProfileId = originalBaselineProfile;
            _page = originalPage;
            _keyboardModifiers = originalModifiers;
            _keyboardSelectedPhysicalKeyId = originalSelectedPhysicalKeyId;
            _keyboardTargetTileId = originalKeyboardTargetId;
            if (originalTarget != null && originalTargetIndex >= 0 && originalTargetIndex < profile.Tiles.Count)
                profile.Tiles[originalTargetIndex] = originalTarget;
            RenderPage(ActiveProfile);
        }
    }

    private static IEnumerable<T> FindKeyboardVisualChildren<T>(DependencyObject root) where T : DependencyObject
    {
        if (root is T match) yield return match;
        for (var index = 0; index < VisualTreeHelper.GetChildrenCount(root); index++)
        {
            foreach (var child in FindKeyboardVisualChildren<T>(VisualTreeHelper.GetChild(root, index))) yield return child;
        }
    }

    private sealed record KeyboardTileChoice(Tile Tile, string Label)
    {
        public Guid Id => Tile.Id;
    }

    private sealed record KeyboardLayoutChoice(string Id, string DisplayName);
    private sealed record CorsairKeyboardChoice(string DeviceId, string Name);
    private sealed record CorsairKeyProposal(string DeviceId, int KeyId);
}

using System.Globalization;
using System.Text.Json;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Aivana.Core;
using Aivana.Windows;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private string? _draftBaseline;
    private Guid? _draftBaselineTileId;
    private Guid? _draftBaselineProfileId;
    private DispatcherTimer? _captureTimeout;
    private TextBox? _captureBox;
    private Guid? _captureTileId;
    private string _captureOriginal = "";
    private ActionStep? _capturingStep;
    private TextBox? _stepCaptureBox;
    private string _stepCaptureOriginal = "";
    private KeyEventHandler? _stepCaptureHandler;
    private TextBlock? _draftValidationDisplay;
    private TextBlock? _draftSaveErrorDisplay;
    private readonly Dictionary<ActionStep, Dictionary<ActionKind, ActionStep>> _stepValueCache = [];

    public bool HasUnsavedChanges
    {
        get
        {
            EnsureDraftBaseline();
            return _draftTile != null && _draftBaseline != null && SerializeTile(_draftTile) != _draftBaseline;
        }
    }

    private void EnsureDraftBaseline()
    {
        if (_draftTile == null || _draftProfileId == null) return;
        if (_draftBaselineTileId == _draftTile.Id && _draftBaselineProfileId == _draftProfileId && _draftBaseline != null) return;
        var persisted = _controller.Settings.Profiles.FirstOrDefault(p => p.Id == _draftProfileId)?.Tiles.FirstOrDefault(t => t.Id == _draftTile.Id);
        _draftBaseline = persisted == null ? "" : SerializeTile(persisted);
        _draftBaselineTileId = _draftTile.Id; _draftBaselineProfileId = _draftProfileId;
    }

    private UIElement BuildEditor(Profile? profile)
    {
        EnsureEditorHistoryInput();
        ObserveDraftHistory();
        var card = Card();
        var outer = new Grid();
        outer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        outer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        outer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var header = new DockPanel { Margin = new Thickness(0, 0, 0, 12) };
        var controls = new StackPanel { Orientation = Orientation.Horizontal };
        if (SelectedTile is { } current)
        {
            controls.Children.Add(Button("⧉", () => DuplicateTile(current), false, 4));
            controls.Children.Add(Button("↑", () => MoveTile(current, -1), false, 4));
            controls.Children.Add(Button("↓", () => MoveTile(current, 1), false, 4));
            controls.Children.Add(Button("⌫", () => DeleteTile(current), false, 4));
        }
        DockPanel.SetDock(controls, Dock.Right); header.Children.Add(controls);
        header.Children.Add(Label(SelectedTile == null ? L("Aktion einrichten") : L("Aktion bearbeiten"), 15, "#E8F0F1", true));
        outer.Children.Add(header);

        var scroll = new ScrollViewer { VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled };
        var content = new StackPanel();
        WrapPanel? footer = null;
        if (SelectedTile is not { } tile)
        {
            content.Children.Add(Label(L("Wähle eine Kachel aus oder lege eine neue Aktion an."), 13, "#91A3A5"));
            content.Children.Add(Label(L("Eine Belegung besteht aus einer einzelnen Aktion oder einer geordneten Makrofolge."), 11, "#748688"));
        }
        else
        {
            _draftValidationDisplay = null;
            _draftSaveErrorDisplay = null;
            if (!string.IsNullOrWhiteSpace(_editorError))
            {
                _draftSaveErrorDisplay = Label(_editorError, 11, "#FF8A8A", false, new Thickness(0, 0, 0, 8));
                content.Children.Add(_draftSaveErrorDisplay);
            }
            if (HasUnsavedChanges)
                content.Children.Add(Label(L("Nicht gespeicherte Änderungen"), 11, "#F2B66D", true, new Thickness(0, 0, 0, 8)));
            content.Children.Add(FieldLabel(L("NAME")));
            content.Children.Add(EditorInput(tile.Name, v => tile.Name = v, L("z. B. Musik starten")));
            var favorite = new CheckBox { Content = L("Als Favorit markieren"), IsChecked = tile.Favorite, Margin = new Thickness(0, 5, 0, 4), Foreground = Brush("#DCE6E7") };
            favorite.Checked += (_, _) => { tile.Favorite = true; Refresh(); };
            favorite.Unchecked += (_, _) => { tile.Favorite = false; Refresh(); };
            content.Children.Add(favorite);
            var corsairSection = new StackPanel { Margin = new Thickness(0, 4, 0, 0) };
            corsairSection.Children.Add(FieldLabel(L("CORSAIR G-TASTE")));
            var corsairStatus = Label(_controller.CorsairKeyStatus, 10, "#77898B");
            corsairStatus.TextWrapping = TextWrapping.Wrap;
            corsairStatus.Margin = new Thickness(0, 0, 0, 4);
            corsairSection.Children.Add(corsairStatus);
            corsairSection.Children.Add(Label(L("Gerät"), 10, "#91A3A5"));
            var corsairDevicePicker = new ComboBox { Margin = new Thickness(0, 2, 0, 3), DisplayMemberPath = nameof(CorsairDeviceChoice.Name), SelectedValuePath = nameof(CorsairDeviceChoice.DeviceId) };
            var corsairDevices = new List<CorsairDeviceChoice> { new("", L("Keine Bindung")) };
            corsairDevices.AddRange(_controller.CorsairKeyboards.Select(device => new CorsairDeviceChoice(device.DeviceId, LF($"{device.Name} · {device.DeviceId}"))));
            if (tile.CorsairBinding is { DeviceId.Length: > 0 } savedBinding && !corsairDevices.Any(device => device.DeviceId.Equals(savedBinding.DeviceId, StringComparison.OrdinalIgnoreCase)))
                corsairDevices.Add(new CorsairDeviceChoice(savedBinding.DeviceId, LF($"Nicht verbunden · {savedBinding.DeviceId}")));
            corsairDevicePicker.ItemsSource = corsairDevices;
            corsairDevicePicker.SelectedValue = tile.CorsairBinding?.DeviceId ?? "";
            corsairSection.Children.Add(corsairDevicePicker);
            corsairSection.Children.Add(Label(L("Taste"), 10, "#91A3A5"));
            var corsairKeyPicker = new ComboBox { Margin = new Thickness(0, 2, 0, 3), DisplayMemberPath = nameof(CorsairKeyChoice.Name), SelectedValuePath = nameof(CorsairKeyChoice.KeyId) };
            var refreshingCorsairKeyChoices = false;
            void RefreshCorsairKeyChoices()
            {
                var deviceId = corsairDevicePicker.SelectedValue as string ?? "";
                var supported = string.IsNullOrEmpty(deviceId) ? Array.Empty<int>() : _controller.GetCorsairMacroKeys(deviceId);
                var choices = new List<CorsairKeyChoice> { new(0, L("Keine Taste")) };
                choices.AddRange(supported.Select(keyId => new CorsairKeyChoice(keyId, $"G{keyId}")));
                if (tile.CorsairBinding is { } binding && binding.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase)
                    && binding.KeyId is >= 1 and <= 6 && !supported.Contains(binding.KeyId))
                    choices.Add(new CorsairKeyChoice(binding.KeyId, LF($"G{binding.KeyId} · derzeit nicht verfügbar")));
                refreshingCorsairKeyChoices = true;
                try
                {
                    corsairKeyPicker.ItemsSource = choices;
                    corsairKeyPicker.SelectedValue = tile.CorsairBinding?.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase) == true
                        ? tile.CorsairBinding.KeyId : 0;
                    corsairKeyPicker.IsEnabled = !string.IsNullOrEmpty(deviceId) && (_controller.CorsairKeysAvailable || tile.CorsairBinding?.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase) == true);
                }
                finally { refreshingCorsairKeyChoices = false; }
            }
            RefreshCorsairKeyChoices();
            corsairDevicePicker.SelectionChanged += (_, _) =>
            {
                var deviceId = corsairDevicePicker.SelectedValue as string ?? "";
                if (string.IsNullOrEmpty(deviceId)) tile.CorsairBinding = null;
                else if (tile.CorsairBinding?.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase) != true) tile.CorsairBinding = null;
                RefreshCorsairKeyChoices();
                ScheduleEditorHistoryCapture(); UpdateEditorValidationDisplay();
            };
            corsairKeyPicker.SelectionChanged += (_, _) =>
            {
                if (refreshingCorsairKeyChoices) return;
                if (corsairKeyPicker.SelectedValue is int keyId && keyId > 0 && corsairDevicePicker.SelectedValue is string deviceId && !string.IsNullOrEmpty(deviceId))
                    tile.CorsairBinding = new CorsairBinding(deviceId, keyId);
                else if (corsairKeyPicker.SelectedValue is int)
                    tile.CorsairBinding = null;
                ScheduleEditorHistoryCapture(); UpdateEditorValidationDisplay();
            };
            corsairSection.Children.Add(corsairKeyPicker);
            content.Children.Add(corsairSection);
            AddVendorBindingFields(content, tile);
            AddGestureFields(content, tile);
            AddGenericKeyFields(content, tile);
            var iconColor = new Grid { Margin = new Thickness(0, 8, 0, 0) };
            iconColor.ColumnDefinitions.Add(new ColumnDefinition());
            iconColor.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(126) });
            var iconPanel = new StackPanel(); iconPanel.Children.Add(FieldLabel(L("SYMBOL")));
            iconPanel.Children.Add(EditorInput(tile.Icon, v => tile.Icon = v, "▶"));
            iconColor.Children.Add(iconPanel);
            var colorPanel = new StackPanel { Margin = new Thickness(10, 0, 0, 0) }; colorPanel.Children.Add(FieldLabel(L("FARBE")));
            colorPanel.Children.Add(EditorInput(tile.Color, v => tile.Color = v, "#25C4B6"));
            Grid.SetColumn(colorPanel, 1); iconColor.Children.Add(colorPanel); content.Children.Add(iconColor);
            var swatches = new WrapPanel { Margin = new Thickness(0, 3, 0, 7) };
            foreach (var color in new[] { "#25C4B6", "#4C8DFF", "#A878E8", "#F2A65A", "#E86D82", "#79B76A" })
            {
                var swatch = Button("●", () => { tile.Color = color; Refresh(); }, false, 4, color);
                swatch.ToolTip = color; swatch.Width = 36; swatch.Height = 32; swatches.Children.Add(swatch);
            }
            var pickColor = Button(L("Farbe wählen…"), () => PickTileColor(tile), false, 4);
            swatches.Children.Add(pickColor);
            content.Children.Add(swatches);

            content.Children.Add(FieldLabel(L("HOTKEY")));
            var captureRow = new DockPanel();
            var clearHotkey = Button("×", () => { tile.Hotkey = ""; Refresh(); }, false, 5);
            clearHotkey.ToolTip = L("Hotkey entfernen"); DockPanel.SetDock(clearHotkey, Dock.Right); captureRow.Children.Add(clearHotkey);
            var capture = new TextBox
            {
                Text = string.IsNullOrWhiteSpace(tile.Hotkey) ? L("Klicken und Kombination drücken") : HotkeyParser.Normalize(tile.Hotkey),
                IsReadOnly = true, Cursor = Cursors.Hand, Margin = new Thickness(0, 2, 0, 2),
                Background = Brush("#111719"), BorderBrush = _line, Padding = new Thickness(9, 8, 9, 8)
            };
            capture.GotKeyboardFocus += (_, _) => BeginCapture(tile, capture);
            capture.PreviewMouseLeftButtonDown += (_, _) => { if (!capture.IsKeyboardFocusWithin) { capture.Focus(); BeginCapture(tile, capture); } };
            capture.PreviewKeyDown += (_, e) => CaptureKey(tile, capture, e);
            capture.LostKeyboardFocus += (_, _) => { if (_capturing && _captureTileId == tile.Id) FinishCapture(restoreOriginal: true); };
            captureRow.Children.Add(capture); content.Children.Add(captureRow);
            var hotkeyErrors = GetDraftHotkeyErrors(tile);
            content.Children.Add(Label(hotkeyErrors ?? L("Tipp: Nutze Strg, Alt, Umschalt oder Win zusammen mit einer Taste."), 10, hotkeyErrors == null ? "#77898B" : "#FF8A8A"));
            var enabled = new CheckBox { Content = L("Aktion aktiviert"), IsChecked = tile.Enabled, Margin = new Thickness(0, 8, 0, 7) };
            enabled.Checked += (_, _) => { tile.Enabled = true; Refresh(); };
            enabled.Unchecked += (_, _) => { tile.Enabled = false; Refresh(); };
            content.Children.Add(enabled);

            content.Children.Add(new Border { Height = 1, Background = _line, Margin = new Thickness(0, 8, 0, 10) });
            var stepHeader = new StackPanel { Margin = new Thickness(0, 0, 0, 7) };
            stepHeader.Children.Add(Label(LF($"MAKRO-SCHRITTE · {tile.Steps.Count}"), 10, "#91A3A5", true));
            var stepButtons = new WrapPanel { HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(0, 5, 0, 0) };
            var add = Button(L("＋ Hinzufügen"), () => ShowActionCatalog(ActiveProfile, tile), false, 0);
            stepButtons.Children.Add(add);
            stepButtons.Children.Add(Button(L("Ablaufeditor"), ShowWorkflowDesigner, false, 4));
            stepButtons.Children.Add(Button("⧉", () => DuplicateStep(tile), false, 4));
            stepButtons.Children.Add(Button("↑", () => MoveStep(tile, -1), false, 4));
            stepButtons.Children.Add(Button("↓", () => MoveStep(tile, 1), false, 4));
            stepButtons.Children.Add(Button("−", () => RemoveStep(tile), false, 4));
            stepHeader.Children.Add(stepButtons); content.Children.Add(stepHeader);
            if (tile.Steps.Count == 0)
                content.Children.Add(Label(L("Noch kein Schritt. Wähle oben eine Aktion aus."), 11, "#91A3A5"));
            else
            {
                _selectedStep = Math.Clamp(_selectedStep, 0, tile.Steps.Count - 1);
                var list = new ListBox
                {
                    Height = Math.Clamp(tile.Steps.Count * 48, 52, 210),
                    ItemsSource = tile.Steps.Select((s, i) => new StepRow(i, s, ActionName(s.Kind))).ToList(),
                    SelectedIndex = _selectedStep, Margin = new Thickness(0, 0, 0, 7),
                    AllowDrop = true, Tag = tile.Id
                };
                list.ItemTemplate = StepTemplate();
                list.PreviewMouseLeftButtonDown += StepListMouseDown;
                list.PreviewMouseMove += StepListMouseMove;
                list.DragOver += StepListDragOver;
                list.Drop += StepListDrop;
                list.SelectionChanged += (_, _) =>
                {
                    if (!_refreshing && list.SelectedIndex >= 0 && list.SelectedIndex != _selectedStep)
                    { _selectedStep = list.SelectedIndex; RenderPage(ActiveProfile); }
                };
                content.Children.Add(list);
                var step = tile.Steps[_selectedStep];
                content.Children.Add(FieldLabel(LF($"SCHRITT {_selectedStep + 1} · {ActionName(step.Kind).ToUpperInvariant()}")));
                var kindPicker = new ComboBox
                {
                    ItemsSource = Enum.GetValues<ActionKind>().Select(kind => new EditorActionChoice(kind, ActionName(kind))).ToList(),
                    DisplayMemberPath = "Label", SelectedValuePath = "Kind", SelectedValue = step.Kind, Margin = new Thickness(0, 2, 0, 5)
                };
                kindPicker.SelectionChanged += (_, _) =>
                {
                    if (_refreshing || kindPicker.SelectedValue is not ActionKind kind || step.Kind == kind) return;
                    if (!_stepValueCache.TryGetValue(step, out var values)) _stepValueCache[step] = values = [];
                    values[step.Kind] = Clone(step);
                    var restored = values.GetValueOrDefault(kind);
                    step.Kind = kind; step.Value = restored?.Value ?? DefaultStepValue(kind);
                    step.Arguments = restored?.Arguments ?? ""; step.WorkingDirectory = restored?.WorkingDirectory ?? "";
                    step.AudioDeviceId = restored?.AudioDeviceId;
                    step.DisplayDeviceId = restored?.DisplayDeviceId;
                    step.UiAutomation = restored?.UiAutomation; step.Plugin = restored?.Plugin;
                    step.DelayMs = kind == ActionKind.WaitWindow ? Math.Clamp(restored?.DelayMs ?? 15000, 1, 600000) : kind == ActionKind.Delay ? Math.Clamp(restored?.DelayMs ?? step.DelayMs, 0, 600000) : step.DelayMs;
                    Refresh(forceRender: true);
                };
                content.Children.Add(kindPicker);
                content.Children.Add(Button(L("Funktion auswählen"), () => ShowActionCatalog(ActiveProfile, tile, replaceStep: true), false, 0));
                AddStepFields(content, tile, step);
                var verify = new CheckBox { Content = L("Ergebnis überprüfen"), IsChecked = step.Verification?.Enabled == true, Margin = new Thickness(0, 8, 0, 4) };
                verify.Checked += (_, _) => { step.Verification = new(true, step.Verification?.TimeoutMs ?? 5000); ScheduleEditorHistoryCapture(); };
                verify.Unchecked += (_, _) => { step.Verification = null; ScheduleEditorHistoryCapture(); };
                content.Children.Add(verify);
            }
            var repeatRow = new DockPanel { Margin = new Thickness(0, 8, 0, 5) };
            repeatRow.Children.Add(Label(L("Makro-Wiederholungen (1–100)"), 12, "#B5C2C3"));
            var repeat = new EditorNumericUpDown(tile.Repeat, 1, 100);
            repeat.ValueChanged += value => { tile.Repeat = value; Refresh(); };
            DockPanel.SetDock(repeat, Dock.Right); repeatRow.Children.Add(repeat); content.Children.Add(repeatRow);
            content.Children.Add(Label(L("Der Abbruch-Hotkey stoppt alle laufenden Makros."), 10, "#77898B"));
            if (_controller.HotkeyErrors.GetValueOrDefault(Guid.Empty) is { Length: > 0 } cancelError)
                content.Children.Add(Label(L("Globaler Hotkey: ") + cancelError, 10, "#FF8A8A", false, new Thickness(0, 4, 0, 0)));
            _draftValidationDisplay = Label("", 11, "#FF8A8A", false, new Thickness(0, 9, 0, 0));
            content.Children.Add(_draftValidationDisplay);
            UpdateEditorValidationDisplay();
            footer = new WrapPanel { Margin = new Thickness(0, 10, 0, 4), HorizontalAlignment = HorizontalAlignment.Left };
            var undo = Button(L("↶ Rückgängig"), UndoDraft, false, 0);
            var redo = Button(L("↷ Wiederholen"), RedoDraft, false, 6);
            _undoDraftButton = undo;
            _redoDraftButton = redo;
            RefreshHistoryButtons();
            footer.Children.Add(undo);
            footer.Children.Add(redo);
            footer.Children.Add(Button(L("Vorschau"), () => ShowMacroPreview(tile), false, 6));
            footer.Children.Add(Button(L("Speichern"), () => { if (CommitDraft()) { FooterStatus.Text = L("Änderungen gespeichert"); Refresh(); } else Refresh(); }, true));
            footer.Children.Add(Button(L("Verwerfen"), DiscardDraft, false, 8));
            footer.Children.Add(ButtonAsync(L("▶ Testen"), async () =>
            {
                if (HasUnsavedChanges)
                {
                    FooterStatus.Text = L("Bitte speichere oder verwerfe den Entwurf, bevor du ihn testest.");
                    return;
                }
                if (ValidateDraft(tile) is { } validationError)
                {
                    FooterStatus.Text = validationError;
                    return;
                }
                var persisted = _draftProfileId is { } pid
                    ? _controller.Settings.Profiles.FirstOrDefault(p => p.Id == pid)?.Tiles.FirstOrDefault(t => t.Id == tile.Id)
                    : null;
                if (persisted != null) await RunTile(persisted);
            }, false, 8));
            footer.Children.Add(Button(L("Makros abbrechen"), () => _controller.CancelAll(), false, 8));
        }
        scroll.Content = content;
        Grid.SetRow(scroll, 1); outer.Children.Add(scroll);
        if (footer != null) { Grid.SetRow(footer, 2); outer.Children.Add(footer); }
        card.Child = outer;
        return card;
    }

    private void AddStepFields(Panel target, Tile tile, ActionStep step)
    {
        var kind = step.Kind;
        if (kind == ActionKind.VendorLighting) { AddVendorLightingFields(target, step); return; }
        if (kind == ActionKind.Plugin) { AddPluginFields(target, step); return; }
        if (kind == ActionKind.UiAutomation) { AddUiAutomationFields(target, step); return; }
        if (kind == ActionKind.Brightness)
        {
            target.Children.Add(FieldLabel(L("Bildschirmhelligkeit")));
            var mode = new ComboBox { ItemsSource = new[] { new BrightnessChoice("Up", L("Bildschirm heller")), new BrightnessChoice("Down", L("Bildschirm dunkler")), new BrightnessChoice("Set", L("Feste Helligkeit")) }, DisplayMemberPath = "Label", SelectedValuePath = "Value", Margin = new Thickness(0, 2, 0, 8) };
            mode.SelectedValue = step.Value.StartsWith("Set:", StringComparison.OrdinalIgnoreCase) ? "Set" : step.Value;
            var numeric = new EditorNumericUpDown(BrightnessCommand.TryParse(step.Value, out var command) ? command.Percent ?? 50 : 50, 0, 100);
            numeric.IsEnabled = mode.SelectedValue as string == "Set";
            var percent = command.Percent ?? 50;
            mode.SelectionChanged += (_, _) => { numeric.IsEnabled = mode.SelectedValue as string == "Set"; step.Value = numeric.IsEnabled ? "Set:" + percent : mode.SelectedValue as string ?? "Up"; Refresh(); };
            numeric.ValueChanged += value => { percent = value; step.Value = "Set:" + value; Refresh(); };
            target.Children.Add(mode); target.Children.Add(numeric);
            AddDisplayBrightnessFields(target, step);
            target.Children.Add(Label(L("Wirkt auf unterstützte Displays. Externe Monitore benötigen DDC/CI; heller/dunkler ändert 10 Prozentpunkte."), 11, "#91A3A5"));
            return;
        }
        if (kind == ActionKind.WaitWindow)
        {
            target.Children.Add(FieldLabel(L("PROZESSNAME (OPTIONAL)")));
            target.Children.Add(EditorInput(step.Value, value => step.Value = value, "notepad.exe"));
            target.Children.Add(FieldLabel(L("FENSTERTITEL ENTHÄLT (OPTIONAL)")));
            target.Children.Add(EditorInput(step.Arguments, value => step.Arguments = value, L("z. B. Projektname")));
            target.Children.Add(FieldLabel(L("ZEITLIMIT (MS)")));
            var timeout = new EditorNumericUpDown(step.DelayMs, 1, 600000);
            timeout.ValueChanged += value => { step.DelayMs = value; Refresh(); };
            target.Children.Add(timeout);
            target.Children.Add(Label(L("Mindestens einen Filter angeben. Das Makro wartet, bis das Zielfenster sichtbar ist, reagiert und fokussiert wurde."), 11, "#91A3A5"));
            return;
        }
        if (kind == ActionKind.Obs) { AddObsStepFields(target, step); return; }
        if (kind == ActionKind.Volume) { AddAudioStepFields(target, step); return; }
        var (valueLabel, hint) = kind switch
        {
            ActionKind.Launch => (L("PROGRAMMPFAD"), L("Programmdatei auswählen; Argumente und Arbeitsordner sind eigene Felder.")),
            ActionKind.OpenPath => (L("DATEI ODER ORDNER"), L("Windows öffnet den Pfad mit der zugeordneten Anwendung.")),
            ActionKind.Url => (L("WEBADRESSE"), L("HTTP- und HTTPS-Adressen werden geöffnet.")),
            ActionKind.Text => (L("TEXT"), L("Unicode-Text wird eingegeben. Variablen werden beim Ausführen ersetzt.")),
            ActionKind.Clipboard => (L("TEXT FÜR ZWISCHENABLAGE"), L("Text wird mit ersetzten Variablen in die Zwischenablage kopiert.")),
            ActionKind.Window => (L("FENSTERAKTION"), L("Eine der unterstützten Aktionen für das aktive Fenster auswählen.")),
            ActionKind.System => (L("SYSTEMAKTION"), L("Nur Sperren ist verfügbar.")),
            ActionKind.Shortcut => (L("TASTENKOMBINATION"), L("Beispiel: Ctrl+Shift+S oder Alt+F4.")),
            ActionKind.Media => (L("MEDIENBEFEHL"), L("Wähle Wiedergabe/Pause, Weiter, Zurück oder Stopp.")),
            ActionKind.Volume => (L("LAUTSTÄRKE"), L("Wähle lauter, leiser oder stumm.")),
            ActionKind.SwitchProfile => (L("ZIELPROFIL"), L("Profil nach Namen auswählen.")),
            ActionKind.Delay => (L("PAUSE (MS)"), L("Wartezeit zwischen Schritten (0–600.000 ms).")),
            ActionKind.CorsairColor => (L("FARBE"), L("Nur mit iCUE-SDK und kompatiblem Gerät verfügbar.")),
            _ => (L("WERT"), L("Wert für diese Aktion."))
        };
        target.Children.Add(FieldLabel(valueLabel));
        if (kind is ActionKind.Media or ActionKind.Volume or ActionKind.Window or ActionKind.System)
        {
            var options = kind == ActionKind.Media
                ? new[] { new EditorValueChoice("PlayPause", L("Wiedergabe / Pause")), new EditorValueChoice("Next", L("Nächster Titel")), new EditorValueChoice("Previous", L("Vorheriger Titel")), new EditorValueChoice("Stop", L("Stopp")) }
                : kind == ActionKind.Volume
                    ? new[] { new EditorValueChoice("Up", L("Lauter")), new EditorValueChoice("Down", L("Leiser")), new EditorValueChoice("Mute", L("Stumm")) }
                    : kind == ActionKind.Window
                        ? new[] { new EditorValueChoice("Minimize", L("Minimieren")), new EditorValueChoice("Maximize", L("Maximieren")), new EditorValueChoice("Restore", L("Wiederherstellen")), new EditorValueChoice("Left", L("Links anordnen")), new EditorValueChoice("Right", L("Rechts anordnen")) }
                        : new[] { new EditorValueChoice("Lock", L("Windows sperren")) };
            var picker = new ComboBox { ItemsSource = options, DisplayMemberPath = "Label", SelectedValuePath = "Value", SelectedValue = NormalizeActionValue(kind, step.Value), Margin = new Thickness(0, 2, 0, 4) };
            picker.SelectionChanged += (_, _) => { if (!_refreshing && picker.SelectedValue is string value) { step.Value = value; Refresh(); } };
            target.Children.Add(picker);
        }
        else if (kind == ActionKind.SwitchProfile)
        {
            var picker = new ComboBox { ItemsSource = _controller.Settings.Profiles, DisplayMemberPath = "Name", SelectedValuePath = "Id", Margin = new Thickness(0, 2, 0, 4) };
            var targetProfile = Guid.TryParse(step.Value, out var profileId)
                ? _controller.Settings.Profiles.FirstOrDefault(p => p.Id == profileId)
                : _controller.Settings.Profiles.FirstOrDefault(p => p.Name.Equals(step.Value.Trim(), StringComparison.OrdinalIgnoreCase));
            if (targetProfile != null) picker.SelectedValue = targetProfile.Id;
            picker.SelectionChanged += (_, _) => { if (!_refreshing && picker.SelectedValue is Guid id) { step.Value = id.ToString(); Refresh(); } };
            target.Children.Add(picker);
        }
        else if (kind == ActionKind.Delay)
        {
            var num = new EditorNumericUpDown(step.DelayMs, 0, 600000);
            num.ValueChanged += value => { step.DelayMs = value; Refresh(); };
            target.Children.Add(num);
        }
        else if (kind is ActionKind.Text or ActionKind.Clipboard)
        {
            var text = new TextBox { Text = step.Value, AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, MinHeight = 86, MaxHeight = 190, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, Margin = new Thickness(0, 2, 0, 4) };
            text.TextChanged += (_, _) => { if (!_refreshing) { step.Value = text.Text; ScheduleEditorHistoryCapture(); UpdateEditorValidationDisplay(); } };
            target.Children.Add(text);
            var tokenPanel = new WrapPanel { Margin = new Thickness(0, 2, 0, 5) };
            foreach (var token in TemplateExpander.Tokens)
            {
                var tokenText = token.StartsWith("{{", StringComparison.Ordinal) ? token : "{{" + token + "}}";
                var insert = Button(tokenText, () => InsertTemplateToken(text, tokenText), false, 3);
                insert.ToolTip = L("An Cursorposition einfügen");
                tokenPanel.Children.Add(insert);
            }
            target.Children.Add(tokenPanel);
            target.Children.Add(Label(L("Verfügbar: {{date}} · {{time}} · {{datetime}} · {{clipboard}} · {{profile}} · {{process}} · {{user}}"), 10, "#7E9092"));
        }
        else if (kind is ActionKind.Launch or ActionKind.OpenPath)
        {
            var row = new DockPanel();
            row.Children.Add(EditorInput(step.Value, v => step.Value = v, kind == ActionKind.Launch ? "C:\\Programme\\App\\app.exe" : L("Datei oder Ordner")));
            row.Children.Add(Button(L("Datei…"), () => BrowseFile(step), false, 5));
            if (kind == ActionKind.OpenPath) row.Children.Add(Button(L("Ordner…"), () => BrowseFolder(step), false, 5));
            target.Children.Add(row);
        }
        else
        {
            var input = EditorInput(step.Value, v => step.Value = v,
                kind == ActionKind.Url ? "https://…" : kind == ActionKind.Shortcut ? "Ctrl+Shift+S" : kind == ActionKind.CorsairColor ? "#25C4B6" : L("Pfad oder Wert"));
            target.Children.Add(input);
            if (kind == ActionKind.Shortcut)
            {
                input.LostKeyboardFocus += (_, _) =>
                {
                    if (_capturing && ReferenceEquals(_stepCaptureBox, input)) FinishCapture(restoreOriginal: true);
                };
                var capture = Button(L("Tastenkombination aufnehmen"), () => BeginStepShortcutCapture(step, input), false, 0, null, new Thickness(0, 3, 0, 0));
                target.Children.Add(capture);
            }
        }
        target.Children.Add(Label(hint, 10, "#7E9092"));
        if (kind == ActionKind.Launch)
        {
            target.Children.Add(FieldLabel(L("ARGUMENTE"))); target.Children.Add(EditorInput(step.Arguments, v => step.Arguments = v, "--profile produktiv"));
            target.Children.Add(FieldLabel(L("ARBEITSORDNER (OPTIONAL)")));
            var workingDirectory = new DockPanel();
            workingDirectory.Children.Add(EditorInput(step.WorkingDirectory, v => step.WorkingDirectory = v, "C:\\…"));
            workingDirectory.Children.Add(Button(L("Ordner…"), () => BrowseWorkingDirectory(step), false, 5));
            target.Children.Add(workingDirectory);
        }
    }

    private TextBox EditorInput(string value, Action<string> changed, string placeholder)
    {
        var box = new TextBox { Text = value, ToolTip = placeholder, Margin = new Thickness(0, 2, 0, 3) };
        box.TextChanged += (_, _) =>
        {
            if (_refreshing) return;
            changed(box.Text);
            ScheduleEditorHistoryCapture();
            if (_editorError != null)
            {
                _editorError = null;
                if (_draftSaveErrorDisplay != null) { _draftSaveErrorDisplay.Text = ""; _draftSaveErrorDisplay.Visibility = Visibility.Collapsed; }
            }
            box.BorderBrush = _line;
            UpdateEditorValidationDisplay();
        };
        return box;
    }

    private void BrowseFile(ActionStep step)
    {
        var dialog = new OpenFileDialog { Filter = step.Kind == ActionKind.Launch ? L("Programme (*.exe)|*.exe|Alle Dateien (*.*)|*.*") : L("Alle Dateien (*.*)|*.*"), Title = step.Kind == ActionKind.Launch ? L("Programm auswählen") : L("Datei auswählen") };
        if (dialog.ShowDialog(this) == true) { step.Value = dialog.FileName; Refresh(); }
    }
    private void PickTileColor(Tile tile)
    {
        var current = IsHexColor(tile.Color) ? (Color)ColorConverter.ConvertFromString(tile.Color) : (Color)ColorConverter.ConvertFromString("#25C4B6");
        using var dialog = new System.Windows.Forms.ColorDialog
        {
            Color = System.Drawing.Color.FromArgb(current.R, current.G, current.B),
            FullOpen = true,
            AnyColor = true
        };
        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        tile.Color = $"#{dialog.Color.R:X2}{dialog.Color.G:X2}{dialog.Color.B:X2}";
        Refresh();
    }
    private void BrowseFolder(ActionStep step)
    {
        var dialog = new OpenFolderDialog { Title = L("Ordner auswählen"), Multiselect = false };
        if (dialog.ShowDialog(this) == true) { step.Value = dialog.FolderName; Refresh(); }
    }
    private void BrowseWorkingDirectory(ActionStep step)
    {
        var dialog = new OpenFolderDialog { Title = L("Arbeitsordner auswählen"), Multiselect = false };
        if (dialog.ShowDialog(this) == true) { step.WorkingDirectory = dialog.FolderName; Refresh(); }
    }

    private void AddStep(Tile tile, ActionKind kind)
    {
        var step = new ActionStep { Kind = kind, Value = DefaultStepValue(kind), DelayMs = kind == ActionKind.WaitWindow ? 15000 : 100 };
        tile.Steps.Add(step); _selectedStep = tile.Steps.Count - 1; Refresh();
    }
    private sealed record BrightnessChoice(string Value, string Label);
    private static string DefaultStepValue(ActionKind kind) => kind switch
    {
        ActionKind.Media => "PlayPause", ActionKind.Volume => "Up", ActionKind.Url => "https://", ActionKind.Delay => "", ActionKind.CorsairColor => "#25C4B6",
        ActionKind.Brightness => "Up", ActionKind.Window => "Minimize", ActionKind.System => "Lock", ActionKind.Obs => "ToggleRecord", _ => ""
    };
    private static string? NormalizeActionValue(ActionKind kind, string value)
    {
        var normalized = value.Trim().Replace(" ", "", StringComparison.Ordinal).ToLowerInvariant();
        return kind switch
        {
            ActionKind.Media => normalized switch { "playpause" or "play/pause" or "toggle" => "PlayPause", "next" or "nexttrack" => "Next", "previous" or "prev" or "previoustrack" => "Previous", "stop" or "stopp" => "Stop", _ => null },
            ActionKind.Volume => ActionCommands.IsExtendedVolume(value) ? value : normalized switch { "up" or "volumeup" or "lauter" => "Up", "down" or "volumedown" or "leiser" => "Down", "mute" or "volumemute" or "stumm" => "Mute", _ => null },
        ActionKind.Window => normalized switch { "minimize" or "minimieren" => "Minimize", "maximize" or "maximieren" => "Maximize", "restore" or "wiederherstellen" => "Restore", "left" or "links" => "Left", "right" or "rechts" => "Right", _ => null },
        ActionKind.System => normalized is "lock" or "sperren" ? "Lock" : null,
            _ => value
        };
    }
    private void DuplicateStep(Tile tile)
    {
        if (tile.Steps.Count == 0) return;
        var from = Math.Clamp(_selectedStep, 0, tile.Steps.Count - 1);
        tile.Steps.Insert(from + 1, Clone(tile.Steps[from])); _selectedStep = from + 1; Refresh();
    }
    private void RemoveStep(Tile tile)
    {
        if (tile.Steps.Count == 0) return;
        tile.Steps.RemoveAt(Math.Clamp(_selectedStep, 0, tile.Steps.Count - 1));
        _selectedStep = Math.Clamp(_selectedStep, 0, Math.Max(0, tile.Steps.Count - 1)); Refresh();
    }
    private void MoveStep(Tile tile, int delta)
    {
        if (tile.Steps.Count < 2) return;
        var from = Math.Clamp(_selectedStep, 0, tile.Steps.Count - 1); var to = Math.Clamp(from + delta, 0, tile.Steps.Count - 1);
        if (from == to) return;
        (tile.Steps[from], tile.Steps[to]) = (tile.Steps[to], tile.Steps[from]); _selectedStep = to; Refresh();
    }
    private async Task RunTile(Tile tile)
    {
        try { FooterStatus.Text = LF($"Führe {tile.Name} aus …"); await _controller.ExecuteAsync(tile); }
        catch (Exception ex) { FooterStatus.Text = LF($"Aktion fehlgeschlagen: {ex.Message}"); MessageBox.Show(this, ex.Message, L("Aktion fehlgeschlagen"), MessageBoxButton.OK, MessageBoxImage.Warning); }
    }

    private bool CommitDraft()
    {
        if (_draftTile == null || _draftProfileId == null) return true;
        var profile = _controller.Settings.Profiles.FirstOrDefault(p => p.Id == _draftProfileId);
        if (profile == null) { _editorError = L("Das Profil dieses Entwurfs ist nicht mehr verfügbar."); return false; }
        var index = profile.Tiles.FindIndex(t => t.Id == _draftTile.Id);
        if (ValidateDraft(_draftTile) is { } error) { FooterStatus.Text = error; return false; }
        if (!HasUnsavedChanges) { _editorError = null; return true; }
        var isNew = index < 0;
        var previous = isNew ? null : profile.Tiles[index];
        if (isNew) profile.Tiles.Add(Clone(_draftTile));
        else profile.Tiles[index] = Clone(_draftTile);
        try
        {
            _controller.Save();
            RecordMeetingDraft(saved:true);
            _draftBaseline = SerializeTile(_draftTile); _draftBaselineTileId = _draftTile.Id; _draftBaselineProfileId = _draftProfileId;
            ResetDraftHistory();
            _editorError = null;
            return true;
        }
        catch (Exception ex)
        {
            if (isNew) profile.Tiles.RemoveAll(t => t.Id == _draftTile.Id);
            else profile.Tiles[index] = previous!;
            _editorError = ex.Message; FooterStatus.Text = ex.Message;
            return false;
        }
    }

    public void DiscardDraft()
    {
        RecordMeetingDraft(saved:false);
        if (_capturing) FinishCapture(restoreOriginal: true);
        if (_draftProfileId is not { } profileId || _selectedTileId is not { } tileId) return;
        var persisted = _controller.Settings.Profiles.FirstOrDefault(p => p.Id == profileId)?.Tiles.FirstOrDefault(t => t.Id == tileId);
        if (persisted == null) { _draftTile = null; _draftProfileId = null; _selectedTileId = null; _draftBaseline = null; _draftBaselineTileId = null; _draftBaselineProfileId = null; }
        else { _draftTile = Clone(persisted); _draftBaseline = SerializeTile(persisted); _draftBaselineTileId = persisted.Id; _draftBaselineProfileId = profileId; }
        ResetDraftHistory();
        _editorError = null; Refresh();
    }

    private string? ValidateDraft(Tile tile)
    {
        if (string.IsNullOrWhiteSpace(tile.Name)) return L("Bitte gib der Aktion einen Namen.");
        if (string.IsNullOrWhiteSpace(tile.Icon)) return L("Bitte gib ein Symbol ein.");
        if (!IsHexColor(tile.Color)) return L("Farbe bitte im Format #RRGGBB eingeben.");
        if (tile.Repeat is < 1 or > 100) return L("Wiederholungen müssen zwischen 1 und 100 liegen.");
        if (!string.IsNullOrWhiteSpace(tile.Hotkey))
        {
            if (!HotkeyParser.TryParse(tile.Hotkey, out _, out _, out var hotkeyError)) return L("Hotkey: ") + hotkeyError;
            if (tile.Enabled && HotkeysEqual(tile.Hotkey, _controller.Settings.CancelHotkey)) return L("Dieser Hotkey ist dem globalen Abbruch-Hotkey zugewiesen.");
            var conflict = _controller.Settings.Profiles.FirstOrDefault(p => p.Id == _draftProfileId)?.Tiles
                .FirstOrDefault(other => other.Id != tile.Id && other.Enabled && !string.IsNullOrWhiteSpace(other.Hotkey)
                    && HotkeyParser.TryParse(other.Hotkey, out var mod, out var key, out _)
                    && HotkeyParser.TryParse(tile.Hotkey, out var thisMod, out var thisKey, out _)
                    && mod == thisMod && key == thisKey);
            if (tile.Enabled && conflict != null) return LF($"Dieser Hotkey ist bereits {conflict.Name} zugewiesen.");
        }
        for (var i = 0; i < tile.Steps.Count; i++)
        {
            var step = tile.Steps[i];
            if (step.Kind == ActionKind.Delay && step.DelayMs is < 0 or > 600000) return LF($"Schritt {i + 1}: Pause muss zwischen 0 und 600.000 ms liegen.");
            if (step.Kind is ActionKind.Launch or ActionKind.OpenPath && string.IsNullOrWhiteSpace(step.Value)) return LF($"Schritt {i + 1}: Bitte einen Programm-, Datei- oder Ordnerpfad eingeben.");
            if (step.Kind == ActionKind.Shortcut && string.IsNullOrWhiteSpace(step.Value)) return LF($"Schritt {i + 1}: Tastenkombination: Bitte eine Taste eingeben.");
            if (step.Kind == ActionKind.Shortcut && HotkeyParser.ValidateShortcut(step.Value).FirstOrDefault() is { } shortcutError) return LF($"Schritt {i + 1}: Tastenkombination: {shortcutError}");
            if (step.Kind == ActionKind.Url
                && (!Uri.TryCreate(step.Value, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)))
                return LF($"Schritt {i + 1}: Bitte eine vollständige HTTP- oder HTTPS-Adresse eingeben.");
            if (step.Kind == ActionKind.Media && NormalizeActionValue(step.Kind, step.Value) == null) return LF($"Schritt {i + 1}: Bitte einen Medienbefehl auswählen.");
            if (step.Kind == ActionKind.Volume && NormalizeActionValue(step.Kind, step.Value) == null) return LF($"Schritt {i + 1}: Bitte eine Lautstärkeaktion auswählen.");
            if (step.Kind == ActionKind.Window && NormalizeActionValue(step.Kind, step.Value) == null) return LF($"Schritt {i + 1}: Bitte eine unterstützte Fensteraktion auswählen.");
            if (step.Kind == ActionKind.System && NormalizeActionValue(step.Kind, step.Value) == null) return LF($"Schritt {i + 1}: Bitte Windows sperren auswählen.");
            if (step.Kind == ActionKind.SwitchProfile && !(_controller.Settings.Profiles.Any(p => Guid.TryParse(step.Value, out var targetId) && p.Id == targetId) || _controller.Settings.Profiles.Any(p => p.Name.Equals(step.Value.Trim(), StringComparison.OrdinalIgnoreCase)))) return LF($"Schritt {i + 1}: Bitte ein vorhandenes Zielprofil auswählen.");
            if (step.Kind == ActionKind.CorsairColor && !IsHexColor(step.Value)) return LF($"Schritt {i + 1}: Farbe bitte im Format #RRGGBB eingeben.");
        }
        var validationError = ConfigurationValidator.ValidateTile(tile).FirstOrDefault()
            ?? CorsairBindingValidator.ValidateTile(tile).FirstOrDefault();
        if (validationError != null) return validationError;
        if (tile.Enabled && HotkeysEqual(tile.Hotkey, _controller.Settings.PaletteHotkey))
            return LF($"Das Palette-Kürzel „{_controller.Settings.PaletteHotkey}“ ist auch der Kachel „{tile.Name}“ zugeordnet.");
        var profile = _controller.Settings.Profiles.FirstOrDefault(p => p.Id == _draftProfileId);
        return profile == null ? null : CorsairBindingValidator.ValidateProfileBindings(new Profile
        {
            Id = profile.Id, Name = profile.Name,
            Tiles = profile.Tiles.Where(other => other.Id != tile.Id).Append(tile).ToList()
        }).FirstOrDefault();
    }

    private void UpdateEditorValidationDisplay()
    {
        if (_draftValidationDisplay == null || _draftTile == null) return;
        var message = ValidateDraft(_draftTile);
        _draftValidationDisplay.Text = message ?? "";
        _draftValidationDisplay.Visibility = message == null ? Visibility.Collapsed : Visibility.Visible;
    }

    private string? GetDraftHotkeyErrors(Tile tile)
    {
        if (!string.IsNullOrWhiteSpace(tile.Hotkey) && !HotkeyParser.TryParse(tile.Hotkey, out _, out _, out var error)) return L("Hotkey: ") + error;
        if (tile.Enabled && HotkeysEqual(tile.Hotkey, _controller.Settings.CancelHotkey)) return L("Konflikt mit dem globalen Abbruch-Hotkey.");
        var conflict = _controller.Settings.Profiles.FirstOrDefault(p => p.Id == _draftProfileId)?.Tiles
            .FirstOrDefault(other => other.Id != tile.Id && other.Enabled && !string.IsNullOrWhiteSpace(other.Hotkey)
                && HotkeyParser.TryParse(other.Hotkey, out var mod, out var key, out _)
                && HotkeyParser.TryParse(tile.Hotkey, out var thisMod, out var thisKey, out _)
                && mod == thisMod && key == thisKey);
        return tile.Enabled && conflict != null ? LF($"Hotkey-Konflikt mit {conflict.Name}.") : null;
    }

    private static bool HotkeysEqual(string left, string right) =>
        HotkeyParser.TryParse(left, out var leftModifiers, out var leftKey, out _) &&
        HotkeyParser.TryParse(right, out var rightModifiers, out var rightKey, out _) &&
        leftModifiers == rightModifiers && leftKey == rightKey;

    private void BeginCapture(Tile tile, TextBox box)
    {
        if (_capturing) return;
        _capturing = true; _captureBox = box; _captureTileId = tile.Id; _captureOriginal = tile.Hotkey;
        _wasPaused = _controller.Paused; _controller.SetPaused(true);
        box.Text = L("Kombination drücken …");
        _captureTimeout = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
        _captureTimeout.Tick += (_, _) => FinishCapture(restoreOriginal: true);
        _captureTimeout.Start();
    }
    private void CaptureKey(Tile tile, TextBox box, KeyEventArgs e)
    {
        if (!_capturing || _captureTileId != tile.Id) return;
        e.Handled = true;
        var key = e.Key == Key.System ? e.SystemKey : e.Key;
        if (key is Key.LeftCtrl or Key.RightCtrl or Key.LeftAlt or Key.RightAlt or Key.LeftShift or Key.RightShift or Key.LWin or Key.RWin) return;
        if (key == Key.Escape && Keyboard.Modifiers == ModifierKeys.None) { FinishCapture(restoreOriginal: true); return; }
        var mods = Keyboard.Modifiers;
        if (mods == ModifierKeys.None) { box.Text = L("Bitte mit Strg, Alt, Umschalt oder Win aufnehmen"); return; }
        var parts = new List<string>();
        if (mods.HasFlag(ModifierKeys.Control)) parts.Add("Ctrl"); if (mods.HasFlag(ModifierKeys.Alt)) parts.Add("Alt");
        if (mods.HasFlag(ModifierKeys.Shift)) parts.Add("Shift"); if (mods.HasFlag(ModifierKeys.Windows)) parts.Add("Win");
        parts.Add(KeyLabel(key));
        var candidate = string.Join("+", parts);
        if (!HotkeyParser.TryParse(candidate, out _, out _, out _)) { box.Text = L("Diese Taste wird nicht unterstützt"); return; }
        tile.Hotkey = candidate; box.Text = HotkeyParser.Normalize(candidate); UpdateEditorValidationDisplay();
        FinishCapture(restoreOriginal: false); Refresh();
    }
    private void BeginStepShortcutCapture(ActionStep step, TextBox box)
    {
        if (_capturing) FinishCapture(restoreOriginal: true);
        _capturing = true; _capturingStep = step; _stepCaptureBox = box; _stepCaptureOriginal = step.Value;
        _wasPaused = _controller.Paused; _controller.SetPaused(true);
        box.ToolTip = L("Drücke eine Taste wie Enter oder F5, oder eine Kombination wie Ctrl+Shift+S. Escape bricht ab.");
        box.Focus(); box.SelectAll();
        _stepCaptureHandler = (sender, e) =>
        {
            e.Handled = true;
            var key = e.Key == Key.System ? e.SystemKey : e.Key;
            if (key is Key.LeftCtrl or Key.RightCtrl or Key.LeftAlt or Key.RightAlt or Key.LeftShift or Key.RightShift or Key.LWin or Key.RWin) return;
            var mods = Keyboard.Modifiers;
            if (key == Key.Escape && mods == ModifierKeys.None) { FinishCapture(restoreOriginal: true); return; }
            var parts = new List<string>();
            if (mods.HasFlag(ModifierKeys.Control)) parts.Add("Ctrl"); if (mods.HasFlag(ModifierKeys.Alt)) parts.Add("Alt"); if (mods.HasFlag(ModifierKeys.Shift)) parts.Add("Shift"); if (mods.HasFlag(ModifierKeys.Windows)) parts.Add("Win");
            parts.Add(KeyLabel(key));
            var candidate = string.Join("+", parts);
            if (!HotkeyParser.TryParseShortcut(candidate, out _, out _, out var error)) { box.ToolTip = error; return; }
            step.Value = candidate; box.Text = HotkeyParser.Normalize(candidate); UpdateEditorValidationDisplay();
            FinishCapture(restoreOriginal: false);
        };
        box.PreviewKeyDown += _stepCaptureHandler;
        _captureTimeout = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
        _captureTimeout.Tick += (_, _) => FinishCapture(restoreOriginal: true);
        _captureTimeout.Start();
    }
    private void FinishCapture(bool restoreOriginal)
    {
        if (!_capturing) return;
        _captureTimeout?.Stop(); _captureTimeout = null;
        if (_stepCaptureBox is { } stepBox)
        {
            if (_stepCaptureHandler != null) stepBox.PreviewKeyDown -= _stepCaptureHandler;
            if (restoreOriginal && _capturingStep != null)
            {
                _capturingStep.Value = _stepCaptureOriginal;
                stepBox.Text = _stepCaptureOriginal;
            }
            stepBox.ToolTip = L("Enter, F5 oder Ctrl+Shift+S");
            _stepCaptureBox = null; _capturingStep = null; _stepCaptureHandler = null;
        }
        else
        {
            if (restoreOriginal && _captureTileId == _selectedTileId && _draftTile != null) _draftTile.Hotkey = _captureOriginal;
            if (_captureBox != null && restoreOriginal) _captureBox.Text = string.IsNullOrWhiteSpace(_captureOriginal) ? L("Klicken und Kombination drücken") : HotkeyParser.Normalize(_captureOriginal);
        }
        _capturing = false; _captureBox = null; _captureTileId = null;
        _controller.SetPaused(_wasPaused);
    }
    private void FinishCapture() => FinishCapture(restoreOriginal: true);

    private static string KeyLabel(Key key)
    {
        if (key is >= Key.A and <= Key.Z) return key.ToString();
        if (key is >= Key.D0 and <= Key.D9) return ((int)key - (int)Key.D0).ToString(CultureInfo.InvariantCulture);
        if (key is >= Key.NumPad0 and <= Key.NumPad9) return "Num" + ((int)key - (int)Key.NumPad0).ToString(CultureInfo.InvariantCulture);
        return key switch
        {
            Key.Return => "Enter", Key.Back => "Backspace", Key.Delete => "Delete", Key.Escape => "Escape", Key.Space => "Space", Key.Tab => "Tab",
            Key.PageUp => "PageUp", Key.PageDown => "PageDown", Key.Left => "Left", Key.Right => "Right", Key.Up => "Up", Key.Down => "Down",
            Key.Insert => "Insert", Key.Home => "Home", Key.End => "End", Key.PrintScreen => "PrintScreen", Key.Pause => "Pause",
            Key.Multiply => "NumMultiply", Key.Add => "NumAdd", Key.Subtract => "NumSubtract", Key.Decimal => "NumDecimal", Key.Divide => "NumDivide",
            Key.Oem1 => "Oem1", Key.OemPlus => "OemPlus", Key.OemComma => "OemComma", Key.OemMinus => "OemMinus", Key.OemPeriod => "OemPeriod", Key.Oem2 => "Oem2", Key.Oem3 => "Oem3", Key.Oem4 => "Oem4", Key.Oem5 => "Oem5", Key.Oem6 => "Oem6", Key.Oem7 => "Oem7", Key.Oem102 => "Oem102",
            _ => key.ToString()
        };
    }

    public void RunEditorSmokeChecks()
    {
        var profile = ActiveProfile ?? throw new InvalidOperationException("Kein aktives Profil vorhanden.");
        var originalPage = _page; var originalSelection = _selectedTileId; var originalDraft = _draftTile == null ? null : Clone(_draftTile);
        var originalDraftProfile = _draftProfileId; var originalBaseline = _draftBaseline; var originalBaselineTile = _draftBaselineTileId; var originalBaselineProfile = _draftBaselineProfileId; var originalStep = _selectedStep;
        var smokeHotkey = (from modifier in new[] { "Ctrl+Shift", "Ctrl+Alt", "Alt+Shift" }
                           from functionKey in Enumerable.Range(1, 24)
                           let candidate = $"{modifier}+F{functionKey}"
                           where !string.Equals(HotkeyParser.Normalize(_controller.Settings.CancelHotkey), candidate, StringComparison.OrdinalIgnoreCase)
                              && !profile.Tiles.Any(existing => existing.Enabled && string.Equals(HotkeyParser.Normalize(existing.Hotkey), candidate, StringComparison.OrdinalIgnoreCase))
                           select candidate).FirstOrDefault() ?? throw new InvalidOperationException("Kein freier Hotkey für den Editor-Smokecheck gefunden.");
        var tile = new Tile { Name = "Editor Smoke", Hotkey = "", Steps = [new ActionStep { Kind = ActionKind.Delay, DelayMs = 100 }] };
        profile.Tiles.Add(tile); _selectedTileId = tile.Id; _draftProfileId = profile.Id; _draftTile = Clone(tile); _draftBaseline = SerializeTile(tile); _selectedStep = 0;
        try
        {
            Refresh();
            _draftTile.Name = "Staged smoke"; _draftTile.Hotkey = "definitely-invalid";
            if (!HasUnsavedChanges) throw new InvalidOperationException("Änderung wurde nicht als Entwurf erkannt.");
            if (CommitDraft() != false) throw new InvalidOperationException("Ungültige Hotkey-Daten wurden gespeichert.");
            if (profile.Tiles.Single(t => t.Id == tile.Id).Name != "Editor Smoke") throw new InvalidOperationException("Ungültiger Entwurf hat persistierte Daten geändert.");
            _draftTile.Hotkey = smokeHotkey;
            if (!CommitDraft()) throw new InvalidOperationException("Gültiger Entwurf ließ sich nicht speichern: " + _editorError);
            if (profile.Tiles.Single(t => t.Id == tile.Id).Name != "Staged smoke") throw new InvalidOperationException("Gültiger Entwurf wurde nicht gespeichert.");
            ResetDraftHistory();
            ObserveDraftHistory();
            var fieldBefore = _draftTile.Name;
            _draftTile.Name = "History field smoke";
            CaptureDraftHistoryState();
            if (_undoDraftButton?.IsEnabled != true) throw new InvalidOperationException("Eine erfasste Feldänderung hat Rückgängig nicht aktiviert.");
            UndoDraft();
            if (_draftTile.Name != fieldBefore) throw new InvalidOperationException("Rückgängig hat die Feldänderung nicht entfernt.");
            RedoDraft();
            if (_draftTile.Name != "History field smoke") throw new InvalidOperationException("Wiederholen hat die Feldänderung nicht erneut angewendet.");
            UndoDraft();
            if (_draftTile.Name != fieldBefore) throw new InvalidOperationException("Der Feld-Smokecheck hat den Ausgangsnamen nicht wiederhergestellt.");
            var historyStart = SerializeTile(_draftTile);
            _draftTile.Favorite = true;
            CaptureDraftHistoryState();
            UndoDraft();
            if (_draftTile.Favorite || SerializeTile(_draftTile) != historyStart) throw new InvalidOperationException("Rückgängig hat den Entwurfsstand nicht wiederhergestellt.");
            RedoDraft();
            if (!_draftTile.Favorite || !CommitDraft() || !profile.Tiles.Single(t => t.Id == tile.Id).Favorite) throw new InvalidOperationException("Favorit wurde im Entwurf oder gespeichert nicht übernommen.");
            if (MacroPreview.Build(_draftTile).Count == 0) throw new InvalidOperationException("Makro-Vorschau hat keine Timeline erzeugt.");
            _draftTile.Name = "discard me"; DiscardDraft();
            if (_draftTile?.Name != "Staged smoke" || HasUnsavedChanges) throw new InvalidOperationException("Verwerfen hat den gespeicherten Stand nicht wiederhergestellt.");
            _draftTile.Steps.Add(new ActionStep { Kind = ActionKind.Text, Value = "SAFE" });
            _draftTile.Steps.Add(new ActionStep { Kind = ActionKind.Delay, DelayMs = 0 });
            _selectedStep = 2; MoveStep(_draftTile, -1);
            if (_draftTile.Steps[1].Kind != ActionKind.Delay || _draftTile.Steps[2].Kind != ActionKind.Text) throw new InvalidOperationException("Makroschritte ließen sich nicht sortieren.");
            var existingDefaults = new Tile
            {
                Name = "Compatibility probe", Color = "#25C4B6",
                Steps = [
                    new ActionStep { Kind = ActionKind.Media, Value = "PlayPause" },
                    new ActionStep { Kind = ActionKind.Media, Value = "Next" },
                    new ActionStep { Kind = ActionKind.Volume, Value = "Up" },
                    new ActionStep { Kind = ActionKind.Volume, Value = "Down" },
                    new ActionStep { Kind = ActionKind.Volume, Value = "Mute" },
                    new ActionStep { Kind = ActionKind.Shortcut, Value = "F5" }
                ]
            };
            if (ValidateDraft(existingDefaults) != null) throw new InvalidOperationException("Ein gespeicherter Aktionsstandard oder einzelner Tastendruck wird abgelehnt.");
            existingDefaults.Steps[^1].Value = "Ctrl+Alt";
            if (ValidateDraft(existingDefaults) == null) throw new InvalidOperationException("Eine Kombination ohne Taste wurde akzeptiert.");
        }
        finally
        {
            profile.Tiles.RemoveAll(t => t.Id == tile.Id);
            _controller.Save();
            _selectedTileId = originalSelection; _draftProfileId = originalDraftProfile; _draftTile = originalDraft; _draftBaseline = originalBaseline; _draftBaselineTileId = originalBaselineTile; _draftBaselineProfileId = originalBaselineProfile; _selectedStep = originalStep; _page = originalPage;
            ResetDraftHistory();
            _editorError = null; Refresh();
        }
    }

    private static string SerializeTile(Tile tile) => JsonSerializer.Serialize(tile);
    private static DataTemplate StepTemplate()
    {
        var template = new DataTemplate(typeof(StepRow));
        var border = new FrameworkElementFactory(typeof(Border));
        border.SetValue(Border.PaddingProperty, new Thickness(7, 5, 7, 5));
        var dock = new FrameworkElementFactory(typeof(DockPanel));
        var index = new FrameworkElementFactory(typeof(TextBlock)); index.SetValue(TextBlock.WidthProperty, 38d); index.SetValue(TextBlock.ForegroundProperty, Brush("#65D6CA")); index.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding(nameof(StepRow.IndexText))); dock.AppendChild(index);
        var title = new FrameworkElementFactory(typeof(TextBlock)); title.SetValue(TextBlock.ForegroundProperty, Brush("#DCE6E7")); title.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding(nameof(StepRow.Title))); dock.AppendChild(title);
        var detail = new FrameworkElementFactory(typeof(TextBlock)); detail.SetValue(TextBlock.ForegroundProperty, Brush("#819395")); detail.SetValue(TextBlock.MarginProperty, new Thickness(10, 0, 0, 0)); detail.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding(nameof(StepRow.Detail))); detail.SetValue(DockPanel.DockProperty, Dock.Right); dock.AppendChild(detail);
        border.AppendChild(dock); template.VisualTree = border; return template;
    }
    private sealed record EditorActionChoice(ActionKind Kind, string Label);
    private sealed record EditorValueChoice(string Value, string Label);
    private sealed record CorsairDeviceChoice(string DeviceId, string Name);
    private sealed record CorsairKeyChoice(int KeyId, string Name);
    private sealed record StepRow(int Index, ActionStep Step, string KindName)
    {
        public string IndexText => $"{Index + 1:00}";
        public string Title => KindName;
        public string Detail => Step.Kind switch { ActionKind.Delay => $"{Step.DelayMs} ms", _ => Trim(Step.Value, 28) };
    }

    private Point _stepDragStart;
    private void StepListMouseDown(object sender, MouseButtonEventArgs e) => _stepDragStart = e.GetPosition(null);
    private void StepListMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed || sender is not ListBox list || list.SelectedIndex < 0) return;
        if (Math.Abs(e.GetPosition(null).Y - _stepDragStart.Y) < 8) return;
        DragDrop.DoDragDrop(list, list.SelectedIndex, DragDropEffects.Move);
    }
    private void StepListDragOver(object sender, DragEventArgs e) => e.Effects = e.Data.GetDataPresent(typeof(int)) ? DragDropEffects.Move : DragDropEffects.None;
    private void StepListDrop(object sender, DragEventArgs e)
    {
        if (sender is not ListBox list || !e.Data.GetDataPresent(typeof(int)) || list.Tag is not Guid tileId) return;
        var tile = _draftTile;
        if (tile == null || tile.Id != tileId) return;
        var from = (int)e.Data.GetData(typeof(int))!;
        var container = FindAncestor<ListBoxItem>(e.OriginalSource as DependencyObject);
        var target = container == null ? list.Items.Count - 1 : list.ItemContainerGenerator.IndexFromContainer(container);
        if (from < 0 || from >= tile.Steps.Count || target < 0 || target >= tile.Steps.Count || from == target) return;
        var item = tile.Steps[from]; tile.Steps.RemoveAt(from); tile.Steps.Insert(target, item); _selectedStep = target; Refresh();
    }
    private static T? FindAncestor<T>(DependencyObject? current) where T : DependencyObject
    {
        while (current != null) { if (current is T found) return found; current = VisualTreeHelper.GetParent(current); }
        return null;
    }
}

internal sealed class EditorNumericUpDown : Border
{
    private readonly TextBox _value;
    private readonly int _min, _max;
    public event Action<int>? ValueChanged;
    public EditorNumericUpDown(int value, int min = 1, int max = 100)
    {
        _min = min; _max = max; Width = 104; Height = 34; CornerRadius = new CornerRadius(6); BorderBrush = Brush("#314143"); BorderThickness = new Thickness(1); Background = Brush("#111719");
        var grid = new Grid(); grid.ColumnDefinitions.Add(new ColumnDefinition()); grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(25) });
        _value = new TextBox { Text = Math.Clamp(value, min, max).ToString(), BorderThickness = new Thickness(0), Padding = new Thickness(7, 4, 2, 4), VerticalContentAlignment = VerticalAlignment.Center, Background = Brushes.Transparent };
        _value.LostKeyboardFocus += (_, _) => Emit(); _value.PreviewKeyDown += (_, e) => { if (e.Key == Key.Enter) { Emit(); e.Handled = true; } };
        grid.Children.Add(_value);
        var buttons = new StackPanel();
        var up = new Button { Content = "▴", Padding = new Thickness(0), FontSize = 9, BorderThickness = new Thickness(0), Background = Brushes.Transparent };
        var down = new Button { Content = "▾", Padding = new Thickness(0), FontSize = 9, BorderThickness = new Thickness(0), Background = Brushes.Transparent };
        up.Click += (_, _) => Step(1); down.Click += (_, _) => Step(-1); buttons.Children.Add(up); buttons.Children.Add(down); Grid.SetColumn(buttons, 1); grid.Children.Add(buttons); Child = grid;
    }
    private void Step(int amount) { var current = int.TryParse(_value.Text, out var n) ? n : _min; _value.Text = Math.Clamp(current + amount, _min, _max).ToString(); Emit(); }
    private void Emit() { if (int.TryParse(_value.Text, out var number)) { number = Math.Clamp(number, _min, _max); _value.Text = number.ToString(); ValueChanged?.Invoke(number); } }
    private static Brush Brush(string c) => new SolidColorBrush((Color)ColorConverter.ConvertFromString(c));
}

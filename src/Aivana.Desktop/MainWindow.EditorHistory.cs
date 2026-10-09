using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private readonly List<string> _draftHistory = [];
    private int _draftHistoryIndex = -1;
    private Guid? _historyTileId;
    private Guid? _historyProfileId;
    private DispatcherTimer? _historyCaptureTimer;
    private bool _historyInputHooked;
    private Button? _undoDraftButton;
    private Button? _redoDraftButton;

    private void EnsureEditorHistoryInput()
    {
        if (_historyInputHooked) return;
        PreviewKeyDown += EditorHistory_PreviewKeyDown;
        _historyInputHooked = true;
    }

    private void ObserveDraftHistory()
    {
        if (_draftTile == null || _draftProfileId == null)
        {
            ResetDraftHistory();
            return;
        }

        if (_historyTileId != _draftTile.Id || _historyProfileId != _draftProfileId)
        {
            ResetDraftHistory();
            _historyTileId = _draftTile.Id;
            _historyProfileId = _draftProfileId;
            _draftHistory.Add(SerializeTile(_draftTile));
            _draftHistoryIndex = 0;
            return;
        }

        CaptureDraftHistoryState();
    }

    private void ScheduleEditorHistoryCapture()
    {
        if (_draftTile == null) return;
        _historyCaptureTimer ??= new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(350) };
        _historyCaptureTimer.Stop();
        _historyCaptureTimer.Tick -= HistoryCaptureTimer_Tick;
        _historyCaptureTimer.Tick += HistoryCaptureTimer_Tick;
        _historyCaptureTimer.Start();
    }

    private void HistoryCaptureTimer_Tick(object? sender, EventArgs e)
    {
        _historyCaptureTimer?.Stop();
        CaptureDraftHistoryState();
    }

    private void CaptureDraftHistoryState()
    {
        if (_draftTile == null || _draftProfileId == null)
        {
            RefreshHistoryButtons();
            return;
        }
        if (_historyTileId != _draftTile.Id || _historyProfileId != _draftProfileId)
        {
            ResetDraftHistory();
            _historyTileId = _draftTile.Id;
            _historyProfileId = _draftProfileId;
        }

        var snapshot = SerializeTile(_draftTile);
        if (_draftHistoryIndex >= 0 && _draftHistory[_draftHistoryIndex] == snapshot)
        {
            RefreshHistoryButtons();
            return;
        }
        if (_draftHistoryIndex + 1 < _draftHistory.Count)
            _draftHistory.RemoveRange(_draftHistoryIndex + 1, _draftHistory.Count - _draftHistoryIndex - 1);
        _draftHistory.Add(snapshot);
        if (_draftHistory.Count > 100) _draftHistory.RemoveAt(0);
        _draftHistoryIndex = _draftHistory.Count - 1;
        RefreshHistoryButtons();
    }

    private void UndoDraft()
    {
        CaptureDraftHistoryState();
        if (_draftTile == null || _draftHistoryIndex <= 0) return;
        ApplyDraftHistoryState(--_draftHistoryIndex);
    }

    private void RedoDraft()
    {
        CaptureDraftHistoryState();
        if (_draftTile == null || _draftHistoryIndex < 0 || _draftHistoryIndex >= _draftHistory.Count - 1) return;
        ApplyDraftHistoryState(++_draftHistoryIndex);
    }

    private void ApplyDraftHistoryState(int index)
    {
        if (index < 0 || index >= _draftHistory.Count) return;
        _historyCaptureTimer?.Stop();
        var restored = JsonSerializer.Deserialize<Tile>(_draftHistory[index]);
        if (restored == null) return;
        _draftTile = restored;
        _selectedTileId = restored.Id;
        _draftProfileId = _historyProfileId;
        _selectedStep = Math.Clamp(_selectedStep, 0, Math.Max(0, restored.Steps.Count - 1));
        RefreshHistoryButtons();
        Refresh();
    }

    private void ResetDraftHistory()
    {
        _historyCaptureTimer?.Stop();
        _draftHistory.Clear();
        _draftHistoryIndex = -1;
        _historyTileId = null;
        _historyProfileId = null;
        RefreshHistoryButtons();
    }

    private void RefreshHistoryButtons()
    {
        if (_undoDraftButton != null) _undoDraftButton.IsEnabled = _draftHistoryIndex > 0;
        if (_redoDraftButton != null) _redoDraftButton.IsEnabled = _draftHistoryIndex >= 0 && _draftHistoryIndex < _draftHistory.Count - 1;
    }

    private void EditorHistory_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if ((Keyboard.Modifiers & ModifierKeys.Control) == 0) return;
        if (Keyboard.FocusedElement is TextBox or PasswordBox) return;
        if (Keyboard.FocusedElement is ComboBox combo && combo.IsEditable) return;
        if (e.Key == Key.Z)
        {
            UndoDraft();
            e.Handled = true;
        }
        else if (e.Key == Key.Y)
        {
            RedoDraft();
            e.Handled = true;
        }
    }

    private static void InsertTemplateToken(TextBox text, string token)
    {
        var start = text.SelectionStart;
        text.SelectedText = token;
        text.CaretIndex = start + token.Length;
        text.Focus();
    }

    private void ShowMacroPreview(Tile tile)
    {
        var lines = MacroPreview.Build(tile);
        var dialog = new Window
        {
            Owner = this,
            Title = LF($"Makro-Vorschau · {tile.Name}"),
            Width = 560,
            Height = 520,
            MinWidth = 420,
            MinHeight = 320,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Background = Brush("#111719"),
            Foreground = Brush("#DCE6E7"),
            ResizeMode = ResizeMode.CanResize
        };
        dialog.Resources.MergedDictionaries.Add(Resources);
        var layout = new DockPanel { Margin = new Thickness(18) };
        var footer = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 12, 0, 0) };
        var close = new Button { Content = L("Schließen"), Padding = new Thickness(14, 7, 14, 7), MinWidth = 96, IsDefault = true };
        close.Click += (_, _) => dialog.Close();
        footer.Children.Add(close);
        DockPanel.SetDock(footer, Dock.Bottom);
        layout.Children.Add(footer);
        var content = new StackPanel();
        content.Children.Add(new TextBlock
        {
            Text = LF($"{tile.Steps.Count} Schritte · {tile.Repeat}×"),
            FontSize = 15,
            FontWeight = FontWeights.SemiBold,
            Foreground = Brush("#65D6CA"),
            Margin = new Thickness(0, 0, 0, 10)
        });
        content.Children.Add(new TextBlock
        {
            Text = L("Nur Vorschau: Es werden keine Aktionen ausgeführt."),
            FontSize = 11,
            Foreground = Brush("#91A3A5"),
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 10)
        });
        var timeline = new TextBox
        {
            Text = string.Join(Environment.NewLine, lines.Select((line, index) => $"{index + 1:00}  {line}")),
            IsReadOnly = true,
            AcceptsReturn = true,
            TextWrapping = TextWrapping.Wrap,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            Background = Brush("#171E20"),
            Foreground = Brush("#DCE6E7"),
            BorderBrush = Brush("#2A3739"),
            Padding = new Thickness(8),
            FontFamily = new System.Windows.Media.FontFamily("Consolas"),
            FontSize = 12
        };
        content.Children.Add(timeline);
        layout.Children.Add(content);
        dialog.Content = layout;
        dialog.ShowDialog();
    }
}

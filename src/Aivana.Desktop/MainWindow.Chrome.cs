using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Automation;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private bool? _sidebarExpanded;

    private void SidebarToggle_Click(object sender, RoutedEventArgs e)
    {
        _sidebarExpanded = Sidebar.Visibility != Visibility.Visible;
        UpdateSidebarLayout();
    }

    private void UpdateSidebarLayout()
    {
        var expanded = _sidebarExpanded ?? ActualWidth >= 1100;
        Sidebar.Visibility = expanded ? Visibility.Visible : Visibility.Collapsed;
        SidebarColumn.Width = new GridLength(expanded ? 196 : 0);
        AutomationProperties.SetName(SidebarToggle, L("ARBEITSPLATZ"));
        SidebarToggle.Content = expanded ? "‹" : "☰";
    }

    private static void SetNavigationContent(Button button, string translated, string geometry)
    {
        var label = translated.TrimStart('◫', '▦', '⌨', '⚙', '⌗', ' ');
        var row = new StackPanel { Orientation = Orientation.Horizontal };
        row.Children.Add(new System.Windows.Shapes.Path
        {
            Data = Geometry.Parse(geometry), Width = 18, Height = 18,
            Stretch = Stretch.Uniform, StrokeThickness = 1.5,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 10, 0)
        });
        var icon = (System.Windows.Shapes.Path)row.Children[0];
        icon.SetBinding(System.Windows.Shapes.Shape.StrokeProperty, new System.Windows.Data.Binding("Foreground") { Source = button });
        row.Children.Add(new TextBlock { Text = label, VerticalAlignment = VerticalAlignment.Center });
        button.Content = row;
        button.ToolTip = label;
        AutomationProperties.SetName(button, label);
    }

    private void UpdatePrimaryAction()
    {
        PrimaryAction.Content = L(_page switch
        {
            "Profile" => "＋ Profil",
            "Geräte" => "↻ Aktualisieren",
            _ => "＋ Aktion"
        });
        PrimaryAction.Visibility = _page == "Einstellungen" ? Visibility.Collapsed : Visibility.Visible;
        PrimaryAction.IsEnabled = _page is "Profile" or "Geräte" || ActiveProfile != null;
        UpdateSidebarLayout();
    }

    private UIElement AdaptTwoColumnPage(Grid root)
    {
        if (Width < 1180 || ActualWidth < 1180)
        {
            root.ColumnDefinitions.Clear();
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            for (var i = 0; i < root.Children.Count; i++)
            {
                Grid.SetColumn(root.Children[i], 0);
                Grid.SetRow(root.Children[i], i);
                if (root.Children[i] is FrameworkElement element)
                    element.Margin = new Thickness(0, 0, 0, 12);
            }
            return new ScrollViewer { Content = root, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled };
        }
        return root;
    }

    private void PrimaryAction_Click(object sender, RoutedEventArgs e)
    {
        if (_page == "Profile") CreateProfile();
        else if (_page == "Geräte") { _controller.RefreshDevices(); Refresh(); }
        else if (ActiveProfile is { } profile)
        {
            if (!ResolvePendingDraft()) return;
            _page = "Dashboard";
            AddTile(profile);
            RenderPage(profile);
        }
    }
}

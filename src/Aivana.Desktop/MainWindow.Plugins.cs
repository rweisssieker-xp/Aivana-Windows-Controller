using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
using Aivana.Core.Plugins;
using Aivana.Windows.Plugins;
using Microsoft.Win32;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void ShowPlugins()
    {
        var root = PluginProcessClient.Root; Directory.CreateDirectory(root);
        var window = new Window { Owner = this, Title = L("Erweiterungen"), Width = 850, Height = 650, Background = _panel, Foreground = System.Windows.Media.Brushes.White };
        window.Resources.MergedDictionaries.Add(Resources);
        var body = new StackPanel { Margin = new Thickness(18) };
        var packages = new ListBox { Height = 120 }; body.Children.Add(packages);
        var description = new TextBox { IsReadOnly = true, AcceptsReturn = true, Height = 150, TextWrapping = TextWrapping.Wrap }; body.Children.Add(description);
        var status = Label("", 12, "#F2B66D"); body.Children.Add(status);
        void RefreshPackages() => packages.ItemsSource = Directory.EnumerateDirectories(root).Where(p => File.Exists(Path.Combine(p,"manifest.json")) && !p.EndsWith(".rollback")).Select(Path.GetFileName).ToArray();
        string? SelectedFolder() => packages.SelectedItem is string id ? Path.Combine(root,id) : null;
        PluginManifest? SelectedManifest() => SelectedFolder() is {} folder ? JsonSerializer.Deserialize<PluginManifest>(File.ReadAllText(Path.Combine(folder,"manifest.json"))) : null;
        packages.SelectionChanged += (_, _) => { try { var manifest = SelectedManifest(); description.Text = manifest == null ? "" : JsonSerializer.Serialize(manifest, new JsonSerializerOptions { WriteIndented = true }); } catch (Exception ex) { status.Text = ex.Message; } };
        body.Children.Add(Button(L("Paket importieren"), () =>
        {
            var picker = new OpenFileDialog { Filter = "MAKRIVO Plugin|*.zip" }; if (picker.ShowDialog(window) != true) return;
            try
            {
                var reviewed = PluginPackage.Inspect(picker.FileName);
                if (MessageBox.Show(window, $"{reviewed.Manifest.Id} {reviewed.Manifest.Version}\n\n{string.Join(", ", reviewed.Manifest.Capabilities)}\n\n" + L("Ausführbare Erweiterung. Nur aus vertrauenswürdiger Quelle installieren. Import bleibt deaktiviert."), L("Paket prüfen"), MessageBoxButton.OKCancel) != MessageBoxResult.OK) return;
                PluginPackage.Install(reviewed, root); RefreshPackages(); status.Text = L("Installiert und deaktiviert.");
            } catch (Exception ex) { status.Text = ex.Message; }
        }));
        body.Children.Add(Button(L("Aktivieren"), () =>
        {
            try { if (SelectedFolder() is {} folder && MessageBox.Show(window, L("Diese Erweiterung kann mit deinen Benutzerrechten Programme ausführen und Dateien lesen. Aktivieren?"), L("Erweiterungen"), MessageBoxButton.YesNo) == MessageBoxResult.Yes) { PluginProcessClient.Enable(folder); status.Text = L("Aktiv"); } } catch (Exception ex) { status.Text = ex.Message; }
        }));
        body.Children.Add(Button(L("Deaktivieren"), () => { try { if (SelectedFolder() is {} folder) PluginProcessClient.Disable(folder); status.Text = L("Deaktiviert"); } catch (Exception ex) { status.Text = ex.Message; } }));
        body.Children.Add(Button(L("Vorherige Version wiederherstellen"), () => { try { if (packages.SelectedItem is not string id) return; PluginProcessClient.Disable(Path.Combine(root,id)); PluginPackage.RestoreRollback(root,id); RefreshPackages(); status.Text = L("Vorherige Version wiederhergestellt und deaktiviert."); } catch (Exception ex) { status.Text = ex.Message; } }));
        body.Children.Add(Button(L("Vorherige Version verwerfen"), () => { try { if (packages.SelectedItem is string id && MessageBox.Show(window,L("Gesicherte vorherige Version endgültig entfernen?"),L("Erweiterungen"),MessageBoxButton.YesNo) == MessageBoxResult.Yes) { PluginPackage.DiscardRollback(root,id); status.Text = L("Sicherung entfernt. Weitere Updates sind möglich."); } } catch (Exception ex) { status.Text = ex.Message; } }));
        body.Children.Add(Button(L("Aktion als Entwurf hinzufügen"), () =>
        {
            try
            {
                var manifest = SelectedManifest(); if (manifest == null || !ResolvePendingDraft()) return;
                AddTile(ActiveProfile); if (_draftTile == null) return;
                _draftTile.Name = manifest.Actions[0].Name;
                _draftTile.Steps = [new() { Kind = ActionKind.Plugin, Value = manifest.Actions[0].Id, Plugin = new(manifest.Id, manifest.Actions[0].Id, JsonSerializer.SerializeToElement(new { })) }];
                _selectedStep = 0; window.Close(); Refresh(forceRender: true);
            } catch (Exception ex) { status.Text = ex.Message; }
        }));
        body.Children.Add(Button(L("Exportieren"), () => { try { if (SelectedFolder() is not {} folder) return; var picker = new SaveFileDialog { Filter = "Plugin|*.zip", FileName = Path.GetFileName(folder) + ".zip" }; if (picker.ShowDialog(window) == true) File.Copy(Path.Combine(folder,"package.zip"),picker.FileName,true); } catch (Exception ex) { status.Text = ex.Message; } }));
        body.Children.Add(Button(L("Deinstallieren"), () => { try { if (SelectedFolder() is {} folder && MessageBox.Show(window,L("Erweiterung entfernen?"),L("Erweiterungen"),MessageBoxButton.YesNo) == MessageBoxResult.Yes) { PluginProcessClient.Disable(folder); PluginPackage.RejectReparse(folder); Directory.Delete(folder,true); RefreshPackages(); } } catch (Exception ex) { status.Text = ex.Message; } }));
        RefreshPackages(); window.Content = new ScrollViewer { Content = body, VerticalScrollBarVisibility = ScrollBarVisibility.Auto }; window.Show();
    }
    private void AddPluginFields(Panel target, ActionStep step)
    {
        var root = PluginProcessClient.Root;
        var choices = Directory.Exists(root) ? Directory.EnumerateDirectories(root).Where(p => !p.EndsWith(".rollback") && File.Exists(Path.Combine(p,"manifest.json"))).Select(p => { try { return JsonSerializer.Deserialize<PluginManifest>(File.ReadAllText(Path.Combine(p,"manifest.json"))); } catch { return null; } }).Where(p => p != null).Cast<PluginManifest>().ToArray() : [];
        var picker = new ComboBox { ItemsSource = choices, DisplayMemberPath = "Id", SelectedValuePath = "Id", SelectedValue = step.Plugin?.PackageId };
        var actions = new ComboBox { DisplayMemberPath = "Name", SelectedValuePath = "Id" };
        var parameters = new TextBox { Text = step.Plugin?.Parameters.GetRawText() ?? "{}", AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, MinHeight = 70 };
        var status = Label("",12,"#F2B66D");
        void Update()
        {
            try { using var json = JsonDocument.Parse(parameters.Text); step.Plugin = new(picker.SelectedValue as string ?? "", actions.SelectedValue as string ?? "", json.RootElement.Clone()); step.Value = step.Plugin.ActionId; status.Text = ""; ScheduleEditorHistoryCapture(); }
            catch (JsonException) { step.Plugin = null; status.Text = L("Ungültige Parameter."); }
        }
        picker.SelectionChanged += (_, _) => { actions.ItemsSource = (picker.SelectedItem as PluginManifest)?.Actions; Update(); };
        actions.ItemsSource = choices.FirstOrDefault(p => p.Id == step.Plugin?.PackageId)?.Actions;
        actions.SelectedValue = step.Plugin?.ActionId;
        actions.SelectionChanged += (_, _) => Update(); parameters.TextChanged += (_, _) => Update();
        target.Children.Add(picker); target.Children.Add(actions); target.Children.Add(parameters); target.Children.Add(status);
    }
}

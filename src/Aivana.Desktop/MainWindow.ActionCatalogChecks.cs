using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    internal void RunActionCatalogSmokeChecks(Action<Window, string> snapshot)
    {
        var original = JsonSerializer.Serialize(_controller.Settings);
        ActionPreset? chosen = null;
        var dialog = BuildActionCatalogWindow(preset => chosen = preset, null);
        dialog.Show(); dialog.UpdateLayout();
        var search = FindKeyboardVisualChildren<TextBox>(dialog).Single(control => control.Name == "CatalogSearch");
        var results = FindKeyboardVisualChildren<ListBox>(dialog).Single(control => control.Name == "CatalogResults");
        var categories = FindKeyboardVisualChildren<ListBox>(dialog).Single(control => control.Name == "CatalogCategories");
        if (results.Items.Count != ActionCatalog.Presets.Count || results.Items.Count < 60) throw new InvalidOperationException("Catalog presets missing from GUI.");
        snapshot(dialog, "Aktionskatalog");
        search.Text = "OBS"; dialog.UpdateLayout();
        if (results.Items.Count < 10) throw new InvalidOperationException("OBS catalog search failed.");
        snapshot(dialog, "Aktionskatalog-OBS");
        search.Text = "not-a-real-function-789xyz";
        if (results.Items.Count != 0) throw new InvalidOperationException("Catalog no-results state failed.");
        search.Text = ""; categories.SelectedIndex = 1;
        if (results.Items.Count == 0 || results.Items.Count == ActionCatalog.Presets.Count) throw new InvalidOperationException("Catalog category filter failed.");
        categories.SelectedIndex = 0; search.Text = "OBS";
        results.SelectedItem = results.Items.Cast<ListBoxItem>().First(item => item.Tag is ActionPreset { Id: "obs.record-start" });
        var accept = FindKeyboardVisualChildren<Button>(dialog).Single(button => button.Content as string == L("Übernehmen"));
        accept.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
        if (chosen?.CreateStep().Kind != ActionKind.Obs) throw new InvalidOperationException("Catalog selection did not return an OBS preset.");
        var obsStep = ActionCatalog.Find("obs.scene")!.CreateStep();
        var obsFields = new StackPanel(); AddObsStepFields(obsFields, obsStep);
        var preview = new Window { Owner = this, Width = 650, Height = 720, Background = Brush("#101719"),
            Content = new Border { Padding = new Thickness(24), Child = obsFields }, ShowInTaskbar = false };
        preview.Resources.MergedDictionaries.Add(Resources); preview.Show(); preview.UpdateLayout();
        if (FindKeyboardVisualChildren<PasswordBox>(preview).Count() != 1) throw new InvalidOperationException("OBS password field missing.");
        var obsNames = FindKeyboardVisualChildren<ComboBox>(preview).Single(combo => combo.Name == "ObsNames");
        obsNames.ItemsSource = new[] { "Test-Szene" }; obsNames.SelectedIndex = 0;
        if (obsStep.Arguments != "Test-Szene") throw new InvalidOperationException("OBS scene selection did not update its draft.");
        snapshot(preview, "OBS-Einstellungen"); preview.Close();
        var audioStep = ActionCatalog.Find("audio.volume-half")!.CreateStep();
        var audioFields = new StackPanel(); AddAudioStepFields(audioFields, audioStep);
        var slider = audioFields.Children.OfType<Slider>().Single(); slider.Value = 57;
        if (audioStep.Value != "Set:57") throw new InvalidOperationException("Audio slider did not update exact percentage.");
        var savedAudio = new ActionStep { Kind = ActionKind.Volume, Value = "Set:50", AudioDeviceId = "smoke-missing-device" };
        var missingFields = new StackPanel(); AddAudioStepFields(missingFields, savedAudio);
        var devices = missingFields.Children.OfType<ComboBox>().Single(combo => combo.Name == "AudioDevicePicker");
        if (devices.SelectedValue as string != "smoke-missing-device" || savedAudio.AudioDeviceId != "smoke-missing-device")
            throw new InvalidOperationException("Missing saved audio device was silently retargeted.");
        devices.SelectedValue = "";
        if (savedAudio.AudioDeviceId is not null) throw new InvalidOperationException("Choosing default audio device did not clear the saved endpoint ID.");
        var workflow = ActionCatalog.Find("workflow.research")!;
        var workflowEntry = new PaletteEntry(new Tile { Name = workflow.Name, Steps = workflow.CreateSteps() }, new Profile(), workflow.Description, false, "", "", true, Preset: workflow);
        if (!workflowEntry.NeedsConfiguration) throw new InvalidOperationException("Workflow must open as an editable draft, never run on selection.");
        DiscardDraft();
        try
        {
            CreatePalettePreset(workflow);
            if (_draftTile?.Steps.Count != 4 || !HasUnsavedChanges || _draftTile.Steps[2].Kind != ActionKind.WaitWindow)
                throw new InvalidOperationException("Palette workflow lost steps or was not staged.");
            _selectedStep = 2; Refresh(forceRender: true); UpdateLayout();
            var waitStep = _draftTile.Steps[2];
            var waitFields = new StackPanel(); AddStepFields(waitFields, _draftTile, waitStep);
            var waitWindow = new Window { Owner = this, Width = 650, Height = 440, Background = Brush("#101719"),
                Content = new Border { Padding = new Thickness(24), Child = waitFields }, ShowInTaskbar = false };
            waitWindow.Resources.MergedDictionaries.Add(Resources); waitWindow.Show(); waitWindow.UpdateLayout();
            try
            {
                var inputs = FindKeyboardVisualChildren<TextBox>(waitWindow).ToList();
                if (inputs.Count != 3) throw new InvalidOperationException("Window wait filter or timeout editor missing.");
                inputs[0].Text = "notepad.exe"; inputs[1].Text = "Notes";
                var increase = FindKeyboardVisualChildren<Button>(waitWindow).Single(button => button.Content as string == "▴");
                increase.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
                if (waitStep.Value != "notepad.exe" || waitStep.Arguments != "Notes" || waitStep.DelayMs != 15001)
                    throw new InvalidOperationException("Window wait edits did not update the staged step.");
                snapshot(waitWindow, "workflow-wait-editor");
            }
            finally { waitWindow.Close(); }
            DiscardDraft();
            ApplySpecialKeyDraft("smoke-device", 1, workflow);
            if (_draftTile?.Steps.Count != 4) throw new InvalidOperationException("Special key workflow lost steps.");
        }
        finally { DiscardDraft(); }
        var workflowCatalog = BuildActionCatalogWindow(_ => { }, null);
        workflowCatalog.Show(); workflowCatalog.UpdateLayout();
        var workflowSearch = FindKeyboardVisualChildren<TextBox>(workflowCatalog).Single(control => control.Name == "CatalogSearch");
        workflowSearch.Text = "workflow."; workflowCatalog.UpdateLayout();
        if (FindKeyboardVisualChildren<ListBox>(workflowCatalog).Single(control => control.Name == "CatalogResults").Items.Count != 6)
            throw new InvalidOperationException("Workflow catalog search missing templates.");
        snapshot(workflowCatalog, "workflow-catalog"); workflowCatalog.Close();
        if (JsonSerializer.Serialize(_controller.Settings) != original) throw new InvalidOperationException("Catalog selection changed saved settings.");
    }
}

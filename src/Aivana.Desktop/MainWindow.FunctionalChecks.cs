using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    internal void RunFunctionalRegressionChecks()
    {
        var previousDraft = _draftTile; var previousTile = _selectedTileId; var previousProfile = _draftProfileId;
        var previousBaseline = _draftBaseline; var previousBaselineTile = _draftBaselineTileId; var previousBaselineProfile = _draftBaselineProfileId;
        var previousStep = _selectedStep; var previousPage = _page;
        try
        {
            var paletteEntries = BuildPaletteEntries();
            if (paletteEntries.Count(entry => entry.Preset != null) != ActionCatalog.Presets.Count)
                throw new InvalidOperationException("Palette does not expose every catalog preset.");
            if (paletteEntries.Count(entry => entry.Command != null) < 12)
                throw new InvalidOperationException("Palette app commands are missing.");
            if (paletteEntries.Single(entry => entry.Preset?.Id == "app.calculator").NeedsConfiguration
                || !paletteEntries.Single(entry => entry.Preset?.Id == "custom.program").NeedsConfiguration)
                throw new InvalidOperationException("Palette does not distinguish ready commands from configurable templates.");
            if (_controller.Paused && paletteEntries.Single(entry => entry.Preset?.Id == "app.calculator").CanRun)
                throw new InvalidOperationException("Ready catalog commands ignore pause.");
            if (FilterPaletteItems(paletteEntries, "obs.scene").Count(entry => entry.Preset?.Id == "obs.scene") != 1)
                throw new InvalidOperationException("Palette cannot search catalog identifiers.");
            ShowCommandPalette();
            try
            {
                var filter = FindKeyboardVisualChildren<ComboBox>(_paletteWindow!).Single(combo => combo.Name == "PaletteFilter");
                filter.SelectedIndex = 2;
                if (_paletteResults!.Items.Count != ActionCatalog.Presets.Count)
                    throw new InvalidOperationException("Palette catalog filter loses presets.");
                _paletteSearch!.Text = "obs.scene";
                if (_paletteResults.Items.Count != 1)
                    throw new InvalidOperationException("Palette GUI search loses catalog results.");
                _paletteSearch.Text = ""; filter.SelectedIndex = 3;
                if (_paletteResults.Items.Cast<PaletteEntry>().Any(entry => entry.Command == null))
                    throw new InvalidOperationException("Palette app filter mixes unrelated results.");
            }
            finally { _paletteWindow?.Close(); }
            var persistedCount = ActiveProfile!.Tiles.Count;
            CreatePalettePreset(ActionCatalog.Find("obs.scene")!);
            if (_draftTile?.Steps.Single().Kind != ActionKind.Obs || ActiveProfile.Tiles.Count != persistedCount)
                throw new InvalidOperationException("Palette preset should open an unsaved configurable draft.");
            _draftTile = null; _selectedTileId = null; _draftProfileId = null;
            _draftTile = new Tile { Name = "Functional regression", Enabled = false,
                Steps = [new ActionStep { Kind = ActionKind.Volume, Value = "Up" }] };
            _draftProfileId = ActiveProfile!.Id; _selectedTileId = _draftTile.Id; _selectedStep = 0; _page = "Dashboard";
            _draftBaseline = ""; _draftBaselineTileId = _draftTile.Id; _draftBaselineProfileId = _draftProfileId;
            ResetDraftHistory(); RenderPage(ActiveProfile); UpdateLayout();
            var audio = FindKeyboardVisualChildren<ComboBox>(PageHost).Single(combo => combo.Items.OfType<EditorValueChoice>().Any(choice => choice.Value == "MicMuteOn"));
            audio.Focus(); Keyboard.Focus(audio);
            if (Keyboard.FocusedElement != audio) throw new InvalidOperationException("Regression test could not focus audio selector.");
            audio.SelectedValue = "Set"; UpdateLayout();
            if (!FindKeyboardVisualChildren<Slider>(PageHost).Any())
                throw new InvalidOperationException("Selecting exact volume while the selector has keyboard focus does not show the percentage slider.");

            ComboBox KindPicker() => FindKeyboardVisualChildren<ComboBox>(PageHost).Single(combo => combo.Items.OfType<EditorActionChoice>().Any());
            void ChangeKind(ActionKind kind)
            {
                var selector = KindPicker(); selector.Focus(); Keyboard.Focus(selector); selector.SelectedValue = kind; UpdateLayout();
            }
            ChangeKind(ActionKind.Launch);
            var step = _draftTile.Steps[0];
            step.Value = "notepad.exe"; step.Arguments = "--example"; step.WorkingDirectory = "C:\\tmp";
            Refresh(forceRender: true); UpdateLayout();
            ChangeKind(ActionKind.Text);
            if (!FindKeyboardVisualChildren<TextBox>(PageHost).Any(box => box.AcceptsReturn))
                throw new InvalidOperationException("Changing kind to Text leaves the previous action's fields visible.");
            ChangeKind(ActionKind.Launch);
            if (step.Value != "notepad.exe" || step.Arguments != "--example" || step.WorkingDirectory != "C:\\tmp")
                throw new InvalidOperationException("Changing kind and returning loses launch fields.");
            ChangeKind(ActionKind.Volume); step.AudioDeviceId = "retained-device";
            ChangeKind(ActionKind.Text); ChangeKind(ActionKind.Volume);
            if (step.AudioDeviceId != "retained-device") throw new InvalidOperationException("Changing kind loses selected audio endpoint.");

            ChangeKind(ActionKind.Obs);
            step.WorkingDirectory = "wss://obs.example.test:4455"; step.Arguments = "Studio";
            Refresh(forceRender: true); UpdateLayout();
            var obsCommand = FindKeyboardVisualChildren<ComboBox>(PageHost).Single(combo => combo.Items.OfType<EditorValueChoice>().Any(choice => choice.Value == "SetCurrentProgramScene"));
            obsCommand.Focus(); Keyboard.Focus(obsCommand); obsCommand.SelectedValue = "SetCurrentProgramScene"; UpdateLayout();
            if (!FindKeyboardVisualChildren<ComboBox>(PageHost).Any(combo => combo.Name == "ObsNames"))
                throw new InvalidOperationException("Selecting OBS scene change does not show scene fields.");
            ChangeKind(ActionKind.Text); ChangeKind(ActionKind.Obs);
            if (step.WorkingDirectory != "wss://obs.example.test:4455" || step.Arguments != "Studio")
                throw new InvalidOperationException("Changing kind loses OBS connection fields.");
            var password = FindKeyboardVisualChildren<PasswordBox>(PageHost).Single();
            password.Focus(); Keyboard.Focus(password); password.Password = "regression-only-not-a-real-password";
            Refresh(); UpdateLayout();
            if (!FindKeyboardVisualChildren<PasswordBox>(PageHost).Any(box => ReferenceEquals(box, password)) || password.Password.Length == 0)
                throw new InvalidOperationException("Background refresh clears the focused password editor.");
            password.Clear(); Keyboard.ClearFocus();
            var beforeBackgroundWork = PageHost.Content;
            _editorBackgroundWorkCount++;
            try { Refresh(); if (!ReferenceEquals(beforeBackgroundWork, PageHost.Content)) throw new InvalidOperationException("Background refresh destroys a pending OBS setup operation."); }
            finally { _editorBackgroundWorkCount--; }

            step.Arguments = "";
            if (ValidateDraft(_draftTile) is null) throw new InvalidOperationException("Editor accepts an OBS scene change without a scene name.");
            _draftTile.Steps.Clear();
            if (ValidateDraft(_draftTile) is null) throw new InvalidOperationException("Editor accepts an empty macro.");
            _draftTile.Steps.Add(new ActionStep { Kind = ActionKind.Text, Value = "test" });
            _draftTile.Enabled = true;
            _draftTile.Hotkey = _controller.Settings.PaletteHotkey;
            if (ValidateDraft(_draftTile) is null) throw new InvalidOperationException("Editor accepts the reserved palette hotkey.");
            _draftTile.Hotkey = "";
            _draftTile.CorsairBinding = new CorsairBinding("", 7);
            if (ValidateDraft(_draftTile) is null) throw new InvalidOperationException("Editor accepts an unsupported Corsair binding.");
            _draftTile.CorsairBinding = new CorsairBinding("regression-device", 1);
            var conflicting = new Tile { Name = "Conflict fixture", CorsairBinding = _draftTile.CorsairBinding,
                Steps = [new ActionStep { Kind = ActionKind.Text, Value = "test" }] };
            ActiveProfile.Tiles.Add(conflicting);
            try
            {
                if (ValidateDraft(_draftTile) is null) throw new InvalidOperationException("Editor accepts a duplicate Corsair G-key binding.");
            }
            finally { ActiveProfile.Tiles.Remove(conflicting); }
        }
        finally
        {
            Keyboard.ClearFocus();
            _draftTile = previousDraft; _selectedTileId = previousTile; _draftProfileId = previousProfile;
            _draftBaseline = previousBaseline; _draftBaselineTileId = previousBaselineTile; _draftBaselineProfileId = previousBaselineProfile;
            _selectedStep = previousStep; _page = previousPage; ResetDraftHistory(); RenderPage(ActiveProfile);
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using Aivana.Core;

namespace Aivana.Desktop;

public partial class MainWindow
{
    private static string ObsCommandName(string command) => command switch
    {
        "StartRecord" => L("Aufnahme starten"), "StopRecord" => L("Aufnahme stoppen"),
        "ToggleRecord" => L("Aufnahme umschalten"), "PauseRecord" => L("Aufnahme pausieren"),
        "ResumeRecord" => L("Aufnahme fortsetzen"), "StartStream" => L("Stream starten"),
        "StopStream" => L("Stream stoppen"), "ToggleStream" => L("Stream umschalten"),
        "SetCurrentProgramScene" => L("OBS-Szene wechseln"), "ToggleInputMute" => L("OBS-Mikrofon umschalten"), _ => command
    };

    private void AddObsStepFields(Panel target, ActionStep step)
    {
        target.Children.Add(FieldLabel("OBS Studio"));
        var picker = new ComboBox
        {
            ItemsSource = ActionCommands.Obs.Select(command => new EditorValueChoice(command, ObsCommandName(command))).ToArray(),
            DisplayMemberPath = "Label", SelectedValuePath = "Value", SelectedValue = step.Value, Margin = new Thickness(0, 2, 0, 8)
        };
        picker.SelectionChanged += (_, _) => { if (!_refreshing && picker.SelectedValue is string command) { step.Value = command; Refresh(forceRender: true); } };
        target.Children.Add(picker);
        target.Children.Add(FieldLabel(L("OBS-WebSocket-Adresse")));
        CancellationTokenSource? discovery = null;
        var connectionStatus = Label("", 11, "#91A3A5");
        ComboBox? names = null;
        TextBox? nameEditor = null;
        var endpointEditor = EditorInput(step.WorkingDirectory, value =>
        {
            step.WorkingDirectory = value; discovery?.Cancel();
            if (names is not null) names.ItemsSource = null;
            connectionStatus.Text = "";
        }, "ws://127.0.0.1:4455");
        target.Children.Add(endpointEditor);
        if (step.Value is "SetCurrentProgramScene" or "ToggleInputMute")
        {
            target.Children.Add(FieldLabel(step.Value == "SetCurrentProgramScene" ? L("Szenenname") : L("Eingabename")));
            names = new ComboBox { Name = "ObsNames", Margin = new Thickness(0, 2, 0, 4), ToolTip = L("Namen aus OBS laden") };
            nameEditor = EditorInput(step.Arguments, value => step.Arguments = value, L("Name in OBS"));
            names.SelectionChanged += (_, _) => { if (names.SelectedItem is string name) nameEditor.Text = name; };
            target.Children.Add(names); target.Children.Add(nameEditor);
        }
        var inspect = Button(L("Verbindung prüfen und Namen laden"), () => { }, false, 0);
        inspect.Click += async (_, _) =>
        {
            if (discovery is not null) return;
            var endpoint = step.WorkingDirectory;
            var command = step.Value;
            var pending = new CancellationTokenSource(); discovery = pending;
            _editorBackgroundWorkCount++;
            inspect.IsEnabled = false; connectionStatus.Text = L("Verbindung wird geprüft…");
            try
            {
                var inventory = await Aivana.Windows.ObsActionClient.DiscoverAsync(endpoint, pending.Token);
                if (pending.IsCancellationRequested || endpoint != step.WorkingDirectory || command != step.Value) return;
                if (names is not null)
                {
                    names.ItemsSource = command == "SetCurrentProgramScene" ? inventory.Scenes : inventory.Inputs;
                    names.SelectedItem = names.Items.Cast<string>().FirstOrDefault(name => name == step.Arguments);
                }
                connectionStatus.Text = LF($"Mit OBS {inventory.Version} verbunden: {inventory.Scenes.Count} Szenen, {inventory.Inputs.Count} Eingaben.");
            }
            catch (OperationCanceledException) when (pending.IsCancellationRequested) { }
            catch (Exception ex)
            {
                if (!pending.IsCancellationRequested && endpoint == step.WorkingDirectory && command == step.Value)
                    connectionStatus.Text = ex.Message;
            }
            finally { discovery = null; pending.Dispose(); inspect.IsEnabled = true; _editorBackgroundWorkCount--; }
        };
        target.Unloaded += (_, _) => discovery?.Cancel();
        target.Children.Add(inspect); target.Children.Add(connectionStatus);
        target.Children.Add(FieldLabel(L("OBS-Passwort")));
        var password = new PasswordBox { Margin = new Thickness(0, 2, 0, 4), Padding = new Thickness(9, 8, 9, 8),
            Background = Brush("#111719"), Foreground = Brush("#E8F0F1"), BorderBrush = _line };
        target.Children.Add(password);
        var credentialStatus = Label(L("Das Passwort wird in der Windows-Anmeldeinformationsverwaltung gespeichert."), 10, "#91A3A5");
        var credentials = new WrapPanel();
        credentials.Children.Add(Button(L("Passwort speichern"), () =>
        {
            try
            {
                Aivana.Windows.ObsCredentials.Save(step.WorkingDirectory, password.Password);
                password.Clear(); credentialStatus.Text = L("Passwort gespeichert.");
            }
            catch (Exception ex) { credentialStatus.Text = ex.Message; }
        }, false, 0));
        credentials.Children.Add(Button(L("Passwort entfernen"), () =>
        {
            try { Aivana.Windows.ObsCredentials.Delete(step.WorkingDirectory); password.Clear(); credentialStatus.Text = L("Passwort entfernt."); }
            catch (Exception ex) { credentialStatus.Text = ex.Message; }
        }, false, 8));
        target.Children.Add(credentials); target.Children.Add(credentialStatus);
        target.Children.Add(Label(L("OBS: WebSocket-Server unter Werkzeuge aktivieren. Adresse und Szenen- oder Eingabename müssen mit OBS übereinstimmen."), 11, "#91A3A5", false, new Thickness(0, 8, 0, 0)));
    }

    private void AddAudioStepFields(Panel target, ActionStep step)
    {
        target.Children.Add(FieldLabel(L("LAUTSTÄRKE")));
        var isSet = step.Value.StartsWith("Set:", StringComparison.OrdinalIgnoreCase);
        var current = isSet ? "Set" : NormalizeActionValue(ActionKind.Volume, step.Value);
        var options = new[]
        {
            new EditorValueChoice("Up", L("Lauter")), new EditorValueChoice("Down", L("Leiser")), new EditorValueChoice("Mute", L("Stumm")),
            new EditorValueChoice("Set", L("Lautstärke setzen")), new EditorValueChoice("MuteOn", L("Stummschaltung einschalten")),
            new EditorValueChoice("MuteOff", L("Stummschaltung aufheben")), new EditorValueChoice("ToggleMute", L("Stummschaltung umschalten")),
            new EditorValueChoice("MicMuteOn", L("Mikrofon stummschalten")), new EditorValueChoice("MicMuteOff", L("Mikrofon aktivieren")),
            new EditorValueChoice("MicToggleMute", L("Mikrofon umschalten"))
        };
        var picker = new ComboBox { ItemsSource = options, DisplayMemberPath = "Label", SelectedValuePath = "Value", SelectedValue = current, Margin = new Thickness(0, 2, 0, 8) };
        picker.SelectionChanged += (_, _) =>
        {
            if (_refreshing || picker.SelectedValue is not string command) return;
            var wasMicrophone = step.Value.StartsWith("Mic", StringComparison.OrdinalIgnoreCase);
            step.Value = command == "Set" ? "Set:50" : command;
            if (wasMicrophone != step.Value.StartsWith("Mic", StringComparison.OrdinalIgnoreCase)) step.AudioDeviceId = null;
            Refresh(forceRender: true);
        };
        target.Children.Add(picker);
        AddAudioDevicePicker(target, step);
        if (isSet)
        {
            var percent = int.TryParse(step.Value.AsSpan(4), out var value) ? Math.Clamp(value, 0, 100) : 50;
            target.Children.Add(FieldLabel(L("Lautstärke in Prozent")));
            var slider = new Slider { Minimum = 0, Maximum = 100, Value = percent, TickFrequency = 1, IsSnapToTickEnabled = true, Margin = new Thickness(0, 8, 0, 4) };
            var display = Label(percent + " %", 13, "#69D8CC");
            slider.ValueChanged += (_, _) =>
            { step.Value = "Set:" + ((int)slider.Value).ToString(System.Globalization.CultureInfo.InvariantCulture); display.Text = (int)slider.Value + " %"; ScheduleEditorHistoryCapture(); UpdateEditorValidationDisplay(); };
            target.Children.Add(slider); target.Children.Add(display);
        }
        target.Children.Add(Label(L("Ohne Geräteauswahl wird das Windows-Standardgerät verwendet. Ein gespeichertes Gerät wird auch nach einem Wechsel des Standardgeräts gezielt angesprochen."), 11, "#91A3A5", false, new Thickness(0, 8, 0, 0)));
    }

    private sealed record AudioDeviceChoice(string Id, string Name);

    private void AddAudioDevicePicker(Panel target, ActionStep step)
    {
        var microphone = step.Value.StartsWith("Mic", StringComparison.OrdinalIgnoreCase);
        target.Children.Add(FieldLabel(microphone ? L("Mikrofongerät") : L("Ausgabegerät")));
        var choices = new List<AudioDeviceChoice> { new("", L("Windows-Standardgerät")) };
        var status = Label("", 10, "#91A3A5");
        var devicePicker = new ComboBox { Name = "AudioDevicePicker", DisplayMemberPath = "Name", SelectedValuePath = "Id", Margin = new Thickness(0, 2, 0, 4) };
        var loading = false;
        void LoadDevices()
        {
            loading = true;
            try
            {
                choices.RemoveRange(1, choices.Count - 1);
                choices.AddRange(Aivana.Windows.AudioEndpointControl.GetDevices(microphone).Select(device => new AudioDeviceChoice(device.Id, device.Name)));
                if (!string.IsNullOrEmpty(step.AudioDeviceId) && !choices.Any(choice => choice.Id == step.AudioDeviceId))
                    choices.Add(new AudioDeviceChoice(step.AudioDeviceId, L("Gespeichertes Gerät derzeit nicht verfügbar")));
                devicePicker.ItemsSource = choices.ToArray(); devicePicker.SelectedValue = step.AudioDeviceId ?? "";
                status.Text = "";
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(step.AudioDeviceId) && !choices.Any(choice => choice.Id == step.AudioDeviceId))
                    choices.Add(new AudioDeviceChoice(step.AudioDeviceId, L("Gespeichertes Gerät derzeit nicht verfügbar")));
                devicePicker.ItemsSource = choices.ToArray(); devicePicker.SelectedValue = step.AudioDeviceId ?? "";
                status.Text = ex.Message;
            }
            finally { loading = false; }
        }
        devicePicker.SelectionChanged += (_, _) =>
        {
            if (loading || _refreshing || devicePicker.SelectedValue is not string id) return;
            step.AudioDeviceId = id.Length == 0 ? null : id; ScheduleEditorHistoryCapture(); UpdateEditorValidationDisplay();
        };
        LoadDevices(); target.Children.Add(devicePicker);
        target.Children.Add(Button(L("Geräte aktualisieren"), LoadDevices, false, 0)); target.Children.Add(status);
    }
}

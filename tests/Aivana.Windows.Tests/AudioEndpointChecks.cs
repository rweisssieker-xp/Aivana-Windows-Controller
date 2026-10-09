using Aivana.Windows;

internal static class AudioEndpointChecks
{
    public static void Run()
    {
        Check("Set:0", AudioCommandKind.SetOutputVolume, percent: 0);
        Check("Set:100", AudioCommandKind.SetOutputVolume, percent: 100);
        Check("set:50", AudioCommandKind.SetOutputVolume, percent: 50);
        Check("MuteOn", AudioCommandKind.SetOutputMute, muted: true);
        Check("muteoff", AudioCommandKind.SetOutputMute, muted: false);
        Check("ToggleMute", AudioCommandKind.ToggleOutputMute);
        Check("MicMuteOn", AudioCommandKind.SetMicrophoneMute, muted: true);
        Check("MicMuteOff", AudioCommandKind.SetMicrophoneMute, muted: false);
        Check("MicToggleMute", AudioCommandKind.ToggleMicrophoneMute);

        foreach (var invalid in new[] { "Set:-1", "Set:101", "Set:", "Set: 50", "Set:1.5", "Up", "Down", "Mute", "MicMute", "Other", "Set:999999999999999999999" })
        {
            if (AudioEndpointControl.TryParseCommand(invalid, out _))
                throw new InvalidOperationException($"Audio command parser accepted invalid input '{invalid}'.");
        }

        if (OperatingSystem.IsWindows())
        {
            var renderDevices = AudioEndpointControl.GetDevices(microphone: false);
            var captureDevices = AudioEndpointControl.GetDevices(microphone: true);
            CheckDeviceList(renderDevices, "render");
            CheckDeviceList(captureDevices, "capture");

            if (renderDevices.Count > 0)
                CheckReadableState(renderDevices[0], microphone: false);
            if (captureDevices.Count > 0)
                CheckReadableState(captureDevices[0], microphone: true);

            var invalidDeviceRejected = false;
            try { _ = AudioEndpointControl.ReadState(microphone: false, "{audio-endpoint-check-invalid-id}"); }
            catch (InvalidOperationException error) when (error.Message.Contains("nicht mehr verfügbar", StringComparison.OrdinalIgnoreCase))
            {
                invalidDeviceRejected = true;
            }
            if (!invalidDeviceRejected)
                throw new InvalidOperationException("A missing selected audio device must fail rather than read the default endpoint.");

            if (captureDevices.Count > 0)
            {
                var wrongDirectionRejected = false;
                try { _ = AudioEndpointControl.ReadState(microphone: false, captureDevices[0].Id); }
                catch (InvalidOperationException error) when (error.Message.Contains("kein Wiedergabegerät", StringComparison.OrdinalIgnoreCase))
                {
                    wrongDirectionRejected = true;
                }
                if (!wrongDirectionRejected)
                    throw new InvalidOperationException("A capture device must not be accepted as an output endpoint.");
            }
        }

        Console.WriteLine("Audio command and read-only endpoint checks passed; no live audio endpoint state was changed.");
    }

    private static void CheckDeviceList(IReadOnlyList<AudioDevice> devices, string kind)
    {
        foreach (var device in devices)
        {
            if (string.IsNullOrWhiteSpace(device.Id) || string.IsNullOrWhiteSpace(device.Name))
                throw new InvalidOperationException($"The {kind} endpoint list contains a blank ID or friendly name.");
        }
    }

    private static void CheckReadableState(AudioDevice device, bool microphone)
    {
        try
        {
            var state = AudioEndpointControl.ReadState(microphone, device.Id);
            if (state.VolumePercent is < 0 or > 100)
                throw new InvalidOperationException("A live audio endpoint reported a volume outside 0–100 percent.");
        }
        catch (InvalidOperationException error) when (error.Message.Contains("stellt keine Lautstärke- oder Stummschaltungssteuerung bereit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Read-only state probe skipped for unsupported endpoint '{device.Name}'.");
        }
    }

    private static void Check(string text, AudioCommandKind kind, int? percent = null, bool? muted = null)
    {
        if (!AudioEndpointControl.TryParseCommand(text, out var command) ||
            command.Kind != kind || command.Percent != percent || command.Muted != muted)
            throw new InvalidOperationException($"Audio command parser returned an unexpected result for '{text}'.");
    }
}

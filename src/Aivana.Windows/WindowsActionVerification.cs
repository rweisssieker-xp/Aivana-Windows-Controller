using Aivana.Core;
using System.IO;
using System.Diagnostics;

namespace Aivana.Windows;

public static class WindowsActionVerification
{
    public static async Task<ActionVerificationResult> PreflightAsync(ActionStep step, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        if (step.Kind == ActionKind.Launch && Path.IsPathRooted(step.Value) && !File.Exists(Environment.ExpandEnvironmentVariables(step.Value)))
            return new(VerificationState.Failed, "missing-program", "Programm nicht gefunden. Pfad im Editor prüfen.");
        if (step.Kind == ActionKind.Brightness)
        {
            var displays = await Task.Run(() => DisplayBrightnessService.GetDisplays(token), token);
            if (!displays.Any(d => string.IsNullOrEmpty(step.DisplayDeviceId) || d.Id == step.DisplayDeviceId))
                return new(VerificationState.Failed, "missing-display", "Zielbildschirm nicht verfügbar. Verbindung und DDC/CI prüfen.");
        }
        if (step.Kind == ActionKind.Volume && ActionCommands.IsExtendedVolume(step.Value))
            _ = await Task.Run(() => AudioEndpointControl.ReadState(step.Value.StartsWith("Mic", StringComparison.OrdinalIgnoreCase), step.AudioDeviceId), token);
        if (step.Kind == ActionKind.Obs) _ = await ObsActionClient.DiscoverAsync(step.WorkingDirectory, token);
        return new(VerificationState.Completed, "ready", "Voraussetzungen geprüft.");
    }

    public static async Task<ActionVerificationResult> VerifyAsync(ActionStep step, CancellationToken token)
    {
        while (true)
        {
            token.ThrowIfCancellationRequested();
            bool? matches = null;
            if (step.Kind == ActionKind.Volume)
            {
                var value = step.Value.ToLowerInvariant();
                if (value.StartsWith("set:") || value is "muteon" or "muteoff" or "micmuteon" or "micmuteoff")
                {
                    var state = await Task.Run(() => AudioEndpointControl.ReadState(value.StartsWith("mic"), step.AudioDeviceId), token);
                    matches = value.StartsWith("set:") ? Math.Abs(state.VolumePercent - int.Parse(value[4..])) <= 1 : state.Muted == value.EndsWith("on");
                }
            }
            else if (step.Kind == ActionKind.Brightness && step.Value.StartsWith("Set:", StringComparison.OrdinalIgnoreCase))
            {
                var targets = (await Task.Run(() => DisplayBrightnessService.GetDisplays(token), token)).Where(d => string.IsNullOrEmpty(step.DisplayDeviceId) || d.Id == step.DisplayDeviceId).ToArray();
                matches = targets.Length > 0 && targets.All(d => Math.Abs(d.Percent - int.Parse(step.Value[4..])) <= 2);
            }
            else if (step.Kind == ActionKind.Obs)
            {
                var status = await ObsActionClient.ReadStatusAsync(step.WorkingDirectory, token);
                matches = step.Value switch { "StartRecord" => status.Recording, "StopRecord" => !status.Recording, "StartStream" => status.Streaming, "StopStream" => !status.Streaming, "SetCurrentProgramScene" => status.Scene == step.Arguments, _ => null };
            }
            else if (step.Kind == ActionKind.Launch)
            {
                var processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(step.Value));
                matches = false;
                try
                {
                    if (!Path.IsPathFullyQualified(step.Value)) matches = null;
                    else foreach (var process in processes)
                    {
                        try { if (string.Equals(Path.GetFullPath(process.MainModule!.FileName), Path.GetFullPath(step.Value), StringComparison.OrdinalIgnoreCase)) { matches = true; break; } }
                        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException or NotSupportedException) { matches = null; }
                    }
                }
                finally { foreach (var process in processes) process.Dispose(); }
            }
            else if (step.Kind is ActionKind.Delay or ActionKind.WaitWindow) matches = true;
            if (matches == null) return new(VerificationState.Unverifiable, "no-postcondition", "Befehl ausgeführt; Wirkung in der Zielanwendung nicht überprüfbar.");
            if (matches == true) return new(VerificationState.Verified, "observed", "Erwarteter Zustand bestätigt.");
            await Task.Delay(150, token);
        }
    }
}

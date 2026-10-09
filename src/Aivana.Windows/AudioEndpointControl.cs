using System.Globalization;
using System.Runtime.InteropServices;
using Aivana.Core;

namespace Aivana.Windows;

/// <summary>Core Audio endpoint operations exposed by the audio command parser.</summary>
public static class AudioEndpointControl
{
    private const int ClassContextAll = 0x17;
    private const int PropertyStoreReadOnly = 0;
    private const uint DeviceStateActive = 0x1;
    private const ushort VariantTypeWideString = 31;
    private static readonly PropertyKey FriendlyNameKey = new(
        new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"), 14);

    /// <summary>Lists active render or capture endpoint devices without changing device state.</summary>
    public static IReadOnlyList<AudioDevice> GetDevices(bool microphone)
    {
        EnsureWindows();
        var flow = microphone ? DataFlow.Capture : DataFlow.Render;
        IMMDeviceEnumerator? enumerator = null;
        IMMDeviceCollection? devices = null;
        try
        {
            enumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            CheckHResult(enumerator.EnumAudioEndpoints(flow, DeviceStateActive, out devices), "Audioendgeräte auflisten");
            if (devices is null)
                throw new InvalidOperationException(Localization.Translate("Windows hat keine Audioendgeräteliste zurückgegeben."));

            CheckHResult(devices.GetCount(out var count), "Anzahl der Audioendgeräte lesen");
            var result = new List<AudioDevice>(checked((int)count));
            for (uint index = 0; index < count; index++)
            {
                IMMDevice? device = null;
                try
                {
                    CheckHResult(devices.Item(index, out device), "Audioendgerät lesen");
                    if (device is null)
                        continue;

                    var id = GetDeviceId(device);
                    var name = GetFriendlyName(device);
                    result.Add(new AudioDevice(id, name));
                }
                finally
                {
                    Release(device);
                }
            }

            return result;
        }
        catch (COMException error)
        {
            throw WrapComError(error);
        }
        finally
        {
            Release(devices);
            Release(enumerator);
        }
    }

    /// <summary>Sets the selected render endpoint, or default multimedia endpoint, to an exact percentage.</summary>
    public static void SetOutputVolume(int percent, string? deviceId = null)
    {
        if (percent is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(percent), percent,
                Localization.Translate("Die Audio-Lautstärke muss zwischen 0 und 100 Prozent liegen."));

        WithEndpoint(DataFlow.Render, deviceId, endpoint =>
        {
            var context = Guid.Empty;
            CheckHResult(endpoint.SetMasterVolumeLevelScalar(percent / 100f, ref context), "Lautstärke setzen");
        });
    }

    /// <summary>Changes the selected render endpoint volume by a percentage delta, clamped to 0–100.</summary>
    public static void AdjustOutputVolume(int deltaPercent, string? deviceId = null)
    {
        WithEndpoint(DataFlow.Render, deviceId, endpoint =>
        {
            CheckHResult(endpoint.GetMasterVolumeLevelScalar(out var scalar), "Lautstärke lesen");
            var context = Guid.Empty;
            CheckHResult(endpoint.SetMasterVolumeLevelScalar(Math.Clamp(scalar + deltaPercent / 100f, 0, 1), ref context), "Lautstärke setzen");
        });
    }

    /// <summary>Sets or toggles mute on the selected render endpoint, or default multimedia endpoint.</summary>
    public static void SetOutputMute(bool? muted, string? deviceId = null) =>
        SetMute(DataFlow.Render, muted, deviceId, "Ausgabe stummschalten");

    /// <summary>Sets or toggles mute on the selected capture endpoint, or default multimedia endpoint.</summary>
    public static void SetMicrophoneMute(bool? muted, string? deviceId = null) =>
        SetMute(DataFlow.Capture, muted, deviceId, "Mikrofon stummschalten");

    /// <summary>Reads volume and mute state from the selected endpoint without changing it.</summary>
    public static AudioDeviceState ReadState(bool microphone, string? deviceId = null)
    {
        var flow = microphone ? DataFlow.Capture : DataFlow.Render;
        var state = default(AudioDeviceState);
        WithEndpoint(flow, deviceId, endpoint =>
        {
            CheckHResult(endpoint.GetMasterVolumeLevelScalar(out var scalar), "Lautstärke lesen");
            CheckHResult(endpoint.GetMute(out var muted), "Stummschaltung lesen");
            state = new AudioDeviceState(
                Math.Clamp((int)Math.Round(scalar * 100f, MidpointRounding.AwayFromZero), 0, 100), muted);
        });
        return state;
    }

    /// <summary>Parses richer audio commands. The legacy Up, Down, and Mute actions remain media-key fallbacks.</summary>
    public static bool TryParseCommand(string? input, out AudioCommand command)
    {
        command = default;
        if (input is null) return false;

        var value = input.Trim();
        if (value.StartsWith("Set:", StringComparison.OrdinalIgnoreCase))
        {
            var number = value.AsSpan(4);
            if (!int.TryParse(number, NumberStyles.None, CultureInfo.InvariantCulture, out var percent) || percent is < 0 or > 100)
                return false;
            command = new AudioCommand(AudioCommandKind.SetOutputVolume, percent);
            return true;
        }

        if (value.Equals("MuteOn", StringComparison.OrdinalIgnoreCase))
            command = new AudioCommand(AudioCommandKind.SetOutputMute, Muted: true);
        else if (value.Equals("MuteOff", StringComparison.OrdinalIgnoreCase))
            command = new AudioCommand(AudioCommandKind.SetOutputMute, Muted: false);
        else if (value.Equals("ToggleMute", StringComparison.OrdinalIgnoreCase))
            command = new AudioCommand(AudioCommandKind.ToggleOutputMute);
        else if (value.Equals("MicMuteOn", StringComparison.OrdinalIgnoreCase))
            command = new AudioCommand(AudioCommandKind.SetMicrophoneMute, Muted: true);
        else if (value.Equals("MicMuteOff", StringComparison.OrdinalIgnoreCase))
            command = new AudioCommand(AudioCommandKind.SetMicrophoneMute, Muted: false);
        else if (value.Equals("MicToggleMute", StringComparison.OrdinalIgnoreCase))
            command = new AudioCommand(AudioCommandKind.ToggleMicrophoneMute);
        else
            return false;

        return true;
    }

    private static void SetMute(DataFlow flow, bool? muted, string? deviceId, string operation)
    {
        WithEndpoint(flow, deviceId, endpoint =>
        {
            var context = Guid.Empty;
            var target = muted;
            if (target is null)
            {
                CheckHResult(endpoint.GetMute(out var current), operation);
                target = !current;
            }
            CheckHResult(endpoint.SetMute(target.Value, ref context), operation);
        });
    }

    private static void WithEndpoint(DataFlow flow, string? deviceId, Action<IAudioEndpointVolume> operation)
    {
        EnsureWindows();
        IMMDeviceEnumerator? enumerator = null;
        IMMDevice? device = null;
        IAudioEndpointVolume? endpoint = null;
        try
        {
            enumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            if (deviceId is null)
            {
                CheckHResult(enumerator.GetDefaultAudioEndpoint(flow, Role.Multimedia, out device), "Standardgerät suchen");
                if (device is null)
                    throw new InvalidOperationException(Localization.Translate("Es ist kein Standard-Audioendpunkt verfügbar."));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(deviceId))
                    throw new InvalidOperationException(Localization.Translate("Die gespeicherte Audioendgeräte-ID ist ungültig oder nicht mehr verfügbar."));
                var result = enumerator.GetDevice(deviceId, out device);
                if (result < 0 || device is null)
                    throw new InvalidOperationException(Localization.Translate(
                        "Das ausgewählte Audioendgerät ist nicht mehr verfügbar. Wähle ein aktives Gerät aus."));
            }

            // This cast is an alias for the device RCW; release it once through device below.
            CheckHResult(device.GetState(out var deviceState), "Audioendgerät lesen");
            if (deviceState != DeviceStateActive)
                throw new InvalidOperationException(Localization.Translate("Das ausgewählte Audioendgerät ist nicht mehr verfügbar. Wähle ein aktives Gerät aus."));
            var endpointInfo = (IMMEndpoint)device;
            CheckHResult(endpointInfo.GetDataFlow(out var actualFlow), "Geräterichtung prüfen");
            if (actualFlow != flow)
                throw new InvalidOperationException(Localization.Translate(
                    flow == DataFlow.Render
                        ? "Das ausgewählte Gerät ist kein Wiedergabegerät."
                        : "Das ausgewählte Gerät ist kein Aufnahmegerät."));

            var interfaceId = typeof(IAudioEndpointVolume).GUID;
            var activationResult = device.Activate(ref interfaceId, ClassContextAll, IntPtr.Zero, out endpoint);
            if (activationResult == unchecked((int)0x80004002))
                throw new InvalidOperationException(Localization.Translate("Das ausgewählte Audioendgerät stellt keine Lautstärke- oder Stummschaltungssteuerung bereit."));
            CheckHResult(activationResult, "Audioendpunkt öffnen");
            if (endpoint is null)
                throw new InvalidOperationException(Localization.Translate("Das ausgewählte Audioendgerät stellt keine Lautstärke- oder Stummschaltungssteuerung bereit."));

            operation(endpoint);
        }
        catch (COMException error)
        {
            throw WrapComError(error);
        }
        finally
        {
            Release(endpoint);
            Release(device);
            Release(enumerator);
        }
    }

    private static string GetDeviceId(IMMDevice device)
    {
        IntPtr pointer = IntPtr.Zero;
        try
        {
            CheckHResult(device.GetId(out pointer), "Audioendgeräte-ID lesen");
            var id = Marshal.PtrToStringUni(pointer);
            return string.IsNullOrEmpty(id)
                ? throw new InvalidOperationException(Localization.Translate("Windows hat eine leere Audioendgeräte-ID zurückgegeben."))
                : id;
        }
        finally
        {
            if (pointer != IntPtr.Zero)
                Marshal.FreeCoTaskMem(pointer);
        }
    }

    private static string GetFriendlyName(IMMDevice device)
    {
        IPropertyStore? properties = null;
        var value = default(PropVariant);
        var valueInitialized = false;
        try
        {
            CheckHResult(device.OpenPropertyStore(PropertyStoreReadOnly, out properties), "Gerätenamen lesen");
            if (properties is null)
                throw new InvalidOperationException(Localization.Translate("Windows hat keinen Eigenschaftsspeicher für das Audioendgerät zurückgegeben."));
            var key = FriendlyNameKey;
            var result = properties.GetValue(ref key, out value);
            valueInitialized = result >= 0;
            CheckHResult(result, "Gerätenamen lesen");
            if (value.Type != VariantTypeWideString || value.Pointer == IntPtr.Zero)
                throw new InvalidOperationException(Localization.Translate("Windows hat keinen gültigen Anzeigenamen für das Audioendgerät zurückgegeben."));
            return Marshal.PtrToStringUni(value.Pointer) ?? string.Empty;
        }
        finally
        {
            if (valueInitialized)
                _ = PropVariantClear(ref value);
            Release(properties);
        }
    }

    private static void EnsureWindows()
    {
        if (!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException(Localization.Translate("Die Windows-Audioendpunktsteuerung ist nur unter Windows verfügbar."));
    }

    private static InvalidOperationException WrapComError(COMException error) => new(
        Localization.Translate((FormattableString)$"Die Audioendpunktsteuerung ist fehlgeschlagen (Windows-Fehler 0x{error.HResult:X8}): {error.Message}"), error);

    private static void CheckHResult(int result, string operation)
    {
        if (result < 0)
            throw new InvalidOperationException(Localization.Translate(
                (FormattableString)$"Windows konnte den Audioendpunkt nicht steuern: {Localization.Translate(operation)} (Fehler 0x{result:X8})."));
    }

    private static void Release(object? value)
    {
        if (value is not null && Marshal.IsComObject(value))
            Marshal.ReleaseComObject(value);
    }

    [DllImport("ole32.dll", PreserveSig = true)]
    private static extern int PropVariantClear(ref PropVariant value);

    private enum DataFlow { Render = 0, Capture = 1 }
    private enum Role { Multimedia = 1 }

    [StructLayout(LayoutKind.Sequential)]
    private readonly struct PropertyKey(Guid formatId, uint propertyId)
    {
        public readonly Guid FormatId = formatId;
        public readonly uint PropertyId = propertyId;
    }

    // PROPVARIANT is 16 bytes on x86 and 24 bytes on x64. The union only needs
    // its first pointer-sized member here; the full 16-byte union preserves ABI size.
    [StructLayout(LayoutKind.Sequential)]
    private struct PropVariant
    {
        public ushort Type;
        private readonly ushort _reserved1;
        private readonly ushort _reserved2;
        private readonly ushort _reserved3;
        private PropVariantUnion _value;
        public readonly IntPtr Pointer => _value.Pointer;
    }

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    private struct PropVariantUnion
    {
        [FieldOffset(0)] public IntPtr Pointer;
    }

    [ComImport]
    [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    private class MMDeviceEnumerator { }

    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDeviceEnumerator
    {
        [PreserveSig] int EnumAudioEndpoints(DataFlow dataFlow, uint stateMask, out IMMDeviceCollection? devices);
        [PreserveSig] int GetDefaultAudioEndpoint(DataFlow dataFlow, Role role, out IMMDevice? endpoint);
        [PreserveSig] int GetDevice([MarshalAs(UnmanagedType.LPWStr)] string deviceId, out IMMDevice? device);
        [PreserveSig] int RegisterEndpointNotificationCallback(IntPtr client);
        [PreserveSig] int UnregisterEndpointNotificationCallback(IntPtr client);
    }

    [ComImport]
    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDeviceCollection
    {
        [PreserveSig] int GetCount(out uint count);
        [PreserveSig] int Item(uint index, out IMMDevice? device);
    }

    [ComImport]
    [Guid("D666063F-1587-4E43-81F1-B948E807363F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDevice
    {
        [PreserveSig] int Activate(ref Guid interfaceId, int classContext, IntPtr activationParameters, out IAudioEndpointVolume? instance);
        [PreserveSig] int OpenPropertyStore(int access, out IPropertyStore? properties);
        [PreserveSig] int GetId(out IntPtr deviceId);
        [PreserveSig] int GetState(out uint state);
    }

    [ComImport]
    [Guid("1BE09788-6894-4089-8586-9A2A6C265AC5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMEndpoint
    {
        [PreserveSig] int GetDataFlow(out DataFlow dataFlow);
    }

    [ComImport]
    [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IPropertyStore
    {
        [PreserveSig] int GetCount(out uint count);
        [PreserveSig] int GetAt(uint index, out PropertyKey key);
        [PreserveSig] int GetValue(ref PropertyKey key, out PropVariant value);
        [PreserveSig] int SetValue(ref PropertyKey key, ref PropVariant value);
        [PreserveSig] int Commit();
    }

    [ComImport]
    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAudioEndpointVolume
    {
        [PreserveSig] int RegisterControlChangeNotify(IntPtr notify);
        [PreserveSig] int UnregisterControlChangeNotify(IntPtr notify);
        [PreserveSig] int GetChannelCount(out uint channelCount);
        [PreserveSig] int SetMasterVolumeLevel(float levelDb, ref Guid eventContext);
        [PreserveSig] int SetMasterVolumeLevelScalar(float level, ref Guid eventContext);
        [PreserveSig] int GetMasterVolumeLevel(out float levelDb);
        [PreserveSig] int GetMasterVolumeLevelScalar(out float level);
        [PreserveSig] int SetChannelVolumeLevel(uint channel, float levelDb, ref Guid eventContext);
        [PreserveSig] int SetChannelVolumeLevelScalar(uint channel, float level, ref Guid eventContext);
        [PreserveSig] int GetChannelVolumeLevel(uint channel, out float levelDb);
        [PreserveSig] int GetChannelVolumeLevelScalar(uint channel, out float level);
        [PreserveSig] int SetMute([MarshalAs(UnmanagedType.Bool)] bool muted, ref Guid eventContext);
        [PreserveSig] int GetMute([MarshalAs(UnmanagedType.Bool)] out bool muted);
        [PreserveSig] int GetVolumeStepInfo(out uint step, out uint stepCount);
        [PreserveSig] int VolumeStepUp(ref Guid eventContext);
        [PreserveSig] int VolumeStepDown(ref Guid eventContext);
        [PreserveSig] int QueryHardwareSupport(out uint hardwareSupportMask);
        [PreserveSig] int GetVolumeRange(out float minDb, out float maxDb, out float incrementDb);
    }
}

public sealed record AudioDevice(string Id, string Name);

public readonly record struct AudioDeviceState(int VolumePercent, bool Muted);

public enum AudioCommandKind
{
    SetOutputVolume,
    SetOutputMute,
    ToggleOutputMute,
    SetMicrophoneMute,
    ToggleMicrophoneMute,
}

public readonly record struct AudioCommand(AudioCommandKind Kind, int? Percent = null, bool? Muted = null);

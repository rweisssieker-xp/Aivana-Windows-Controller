using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Concurrent;
using Aivana.Core;
using Aivana.Windows;

internal static class CorsairSdkKeyChecks
{
    public static void Run(CorsairService service)
    {
        var serviceType = typeof(CorsairService);
        Check(service.KeysAvailable || !string.IsNullOrWhiteSpace(service.KeyStatus),
            "Corsair G-key capability detection should always publish a useful status.");
        var reportedKeys = service.Keyboards.SelectMany(keyboard => service.GetSupportedMacroKeyIds(keyboard.DeviceId));
        Check(reportedKeys.All(keyId => keyId is >= 1 and <= 20),
            "The SDK macro-key array should contain only valid Corsair macro-key IDs.");
        foreach (var keyboard in service.Keyboards)
            Console.WriteLine($"iCUE macro keys for {keyboard.DeviceId}: [{string.Join(", ", service.GetSupportedMacroKeyIds(keyboard.DeviceId).Select(keyId => $"G{keyId}"))}]");

        Check(Marshal.SizeOf(serviceType.GetNestedType("NativeEvent", BindingFlags.NonPublic)!) == 16,
            "x64 CorsairEvent should contain an enum and an aligned event-data pointer.");
        Check(Marshal.SizeOf(serviceType.GetNestedType("NativeKeyEvent", BindingFlags.NonPublic)!) == 136,
            "CorsairKeyEvent should contain a 128-byte id, enum, bool, and native padding.");
        Check(Marshal.SizeOf(serviceType.GetNestedType("NativeKeyEventConfiguration", BindingFlags.NonPublic)!) == 8,
            "CorsairKeyEventConfiguration should use native enum/bool alignment.");
        Check(Marshal.SizeOf(serviceType.GetNestedType("NativeProperty", BindingFlags.NonPublic)!) == 24,
            "x64 CorsairProperty should preserve the int-array pointer/count union ABI.");

        var invalidBindingRejected = false;
        try { service.ConfigureMacroKeyBindings([new CorsairMacroKeyBinding("device", 7)]); }
        catch (ArgumentException) { invalidBindingRejected = true; }
        Check(invalidBindingRejected, "Unsupported macro-key IDs must be rejected before reaching the SDK.");
        CheckSyntheticKeyEdges(service, new CorsairMacroKeyBinding("synthetic-device", 1));

        var candidate = service.Keyboards
            .SelectMany(keyboard => service.GetSupportedMacroKeyIds(keyboard.DeviceId)
                .Where(CorsairBindingValidator.IsSupportedKeyId)
                .Select(keyId => new CorsairMacroKeyBinding(keyboard.DeviceId, keyId)))
            .FirstOrDefault();
        if (candidate is null)
        {
            Console.WriteLine($"iCUE native G-key probe: no connected device reported G1-G6; {service.KeyStatus}");
            return;
        }

        Exception? configurationError = null;
        try
        {
            service.ConfigureMacroKeyBindings([candidate]);
            Console.WriteLine($"iCUE native G-key configure probe: {service.KeyStatus}");
        }
        catch (Exception error) { configurationError = error; }
        finally
        {
            try
            {
                service.ConfigureMacroKeyBindings([]);
                Console.WriteLine($"iCUE native G-key release probe: {service.KeyStatus}");
            }
            catch (Exception releaseError)
            {
                Console.WriteLine($"iCUE native G-key release probe failed: {releaseError.Message}");
            }
        }
        if (configurationError is not null)
            Console.WriteLine($"iCUE native G-key configure probe failed: {configurationError.Message}");
    }

    private static void CheckSyntheticKeyEdges(CorsairService service, CorsairMacroKeyBinding binding)
    {
        var events = new ConcurrentQueue<CorsairMacroKeyPress>();
        using var received = new ManualResetEventSlim();
        var requestedBindingsField = typeof(CorsairService).GetField("_requestedMacroKeyBindings", BindingFlags.Instance | BindingFlags.NonPublic)!;
        var previousRequestedBindings = requestedBindingsField.GetValue(service)!;
        requestedBindingsField.SetValue(service, new HashSet<CorsairMacroKeyBinding> { binding });
        void OnPress(CorsairMacroKeyPress press)
        {
            events.Enqueue(press);
            if (events.Count >= 2) received.Set();
        }

        service.MacroKeyPressed += OnPress;
        try
        {
            var serviceType = typeof(CorsairService);
            var keyType = serviceType.GetNestedType("NativeKeyEvent", BindingFlags.NonPublic)!;
            var eventType = serviceType.GetNestedType("NativeEvent", BindingFlags.NonPublic)!;
            var keyMemory = Marshal.AllocHGlobal(Marshal.SizeOf(keyType));
            var eventMemory = Marshal.AllocHGlobal(Marshal.SizeOf(eventType));
            try
            {
                var id = new byte[128];
                Encoding.ASCII.GetBytes(binding.DeviceId, id);
                var key = Activator.CreateInstance(keyType)!;
                keyType.GetField("DeviceId", BindingFlags.Instance | BindingFlags.Public)!.SetValue(key, id);
                keyType.GetField("KeyId", BindingFlags.Instance | BindingFlags.Public)!.SetValue(key, binding.KeyId);
                keyType.GetField("IsPressed", BindingFlags.Instance | BindingFlags.Public)!.SetValue(key, (byte)1);
                Marshal.StructureToPtr(key, keyMemory, false);

                var nativeEvent = Activator.CreateInstance(eventType)!;
                eventType.GetField("Id", BindingFlags.Instance | BindingFlags.Public)!.SetValue(nativeEvent, 2);
                eventType.GetField("Data", BindingFlags.Instance | BindingFlags.Public)!.SetValue(nativeEvent, keyMemory);
                Marshal.StructureToPtr(nativeEvent, eventMemory, false);
                var callback = serviceType.GetMethod("OnMacroKeyEvent", BindingFlags.Instance | BindingFlags.NonPublic)!;
                callback.Invoke(service, [IntPtr.Zero, eventMemory]);
                callback.Invoke(service, [IntPtr.Zero, eventMemory]); // repeated key-down while held

                keyType.GetField("IsPressed", BindingFlags.Instance | BindingFlags.Public)!.SetValue(key, (byte)0);
                Marshal.StructureToPtr(key, keyMemory, false);
                callback.Invoke(service, [IntPtr.Zero, eventMemory]); // release clears the edge without dispatching

                keyType.GetField("IsPressed", BindingFlags.Instance | BindingFlags.Public)!.SetValue(key, (byte)1);
                Marshal.StructureToPtr(key, keyMemory, false);
                callback.Invoke(service, [IntPtr.Zero, eventMemory]);

                Check(received.Wait(TimeSpan.FromSeconds(2)), "Synthetic SDK key-down callbacks should dispatch both press edges.");
                Check(events.Count == 2 && events.All(press => press.DeviceId == binding.DeviceId && press.KeyId == binding.KeyId),
                    "Repeated key-down should dispatch once until release, and release itself should not dispatch.");
                Console.WriteLine("iCUE key callback edge probe passed: repeat suppressed; release ignored.");
            }
            finally
            {
                Marshal.FreeHGlobal(eventMemory);
                Marshal.FreeHGlobal(keyMemory);
            }
        }
        finally
        {
            service.MacroKeyPressed -= OnPress;
            requestedBindingsField.SetValue(service, previousRequestedBindings);
        }
    }

    private static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}

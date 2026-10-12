using Aivana.Windows;

internal static class RawKeyboardEdgeChecks
{
    internal static void Run()
    {
        using var input = new RawKeyboardBindingService();
        var device = new IntPtr(123);
        void Check(bool value, string reason) { if (!value) throw new InvalidOperationException(reason); }
        Check(input.TryAcceptEdge(device, 0x7C, true, false), "Initial physical press missing.");
        Check(!input.TryAcceptEdge(device, 0x7C, true, true), "Injected-period repeat should be ignored.");
        Check(input.TryAcceptEdge(device, 0x7C, false, true), "Physical release during injection must reach the layer router.");
        Check(!input.TryAcceptEdge(device, 0x7C, false, false), "Duplicate release should be ignored.");
        Check(!input.TryAcceptEdge(device, 0x7D, true, true), "New down during injection should be ignored.");
        Check(!input.TryAcceptEdge(device, 0x7D, false, false), "Suppressed down must not leave held state.");
        Check(input.TryAcceptEdge(device, 0x7C, true, false), "Released binding must rearm.");
        Check(!input.TryAcceptEdge(IntPtr.Zero, 0x7C, true, false), "Synthetic input must not be accepted.");
        Check(!input.TryAcceptEdge(device, 0x41, true, false), "Ordinary text must not be accepted.");
        Console.WriteLine("Raw keyboard release during injection and repeat suppression checks passed.");
    }
}

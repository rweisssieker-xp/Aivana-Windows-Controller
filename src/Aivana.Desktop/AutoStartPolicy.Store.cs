namespace Aivana.Desktop;

internal static class AutoStartPolicy
{
    public static bool Supported => false;

    public static void Apply(Action operation) =>
        throw new InvalidOperationException("Autostart is unavailable in this Store release.");
}

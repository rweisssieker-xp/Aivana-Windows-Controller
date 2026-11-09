namespace Aivana.Desktop;

internal static class AutoStartPolicy
{
    public static bool Supported => true;

    public static void Apply(Action operation) => operation();
}

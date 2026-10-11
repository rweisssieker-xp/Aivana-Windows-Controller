using System.Globalization;
namespace Aivana.Core;

public readonly record struct BrightnessCommand(int? Percent, int Delta)
{
    public static bool TryParse(string? value, out BrightnessCommand command)
    {
        command = default;
        if (string.Equals(value, "Up", StringComparison.OrdinalIgnoreCase)) { command = new(null, 10); return true; }
        if (string.Equals(value, "Down", StringComparison.OrdinalIgnoreCase)) { command = new(null, -10); return true; }
        if (value is not null && value.StartsWith("Set:", StringComparison.OrdinalIgnoreCase) &&
            int.TryParse(value.AsSpan(4), NumberStyles.None, CultureInfo.InvariantCulture, out var percent) && percent is >= 0 and <= 100)
        { command = new(percent, 0); return true; }
        return false;
    }
    public uint Apply(uint minimum, uint current, uint maximum)
    {
        if (maximum <= minimum || current < minimum || current > maximum) throw new ArgumentOutOfRangeException(nameof(current));
        var span = (double)maximum - minimum;
        var percent = Percent ?? Math.Clamp((current - (double)minimum) * 100 / span + Delta, 0, 100);
        return (uint)Math.Clamp(Math.Round(minimum + span * percent / 100, MidpointRounding.AwayFromZero), minimum, maximum);
    }
}

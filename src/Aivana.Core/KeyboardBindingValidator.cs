namespace Aivana.Core;
public static class KeyboardBindingValidator
{
    public static IEnumerable<string> ValidateProfile(Profile profile)
    {
        var tiles = profile.Tiles.Where(t => t is not null && t.Enabled && t.KeyboardBinding is not null).ToArray();
        foreach (var tile in profile.Tiles.Where(t => t is not null && t.KeyboardBinding is not null))
        {
            var binding = tile.KeyboardBinding!;
            if (string.IsNullOrWhiteSpace(binding.DevicePath) || (binding.DevicePath?.Length ?? 0) > 32768 || !binding.IsValid)
                yield return Localization.Translate("Die Standard-Sondertastenzuordnung ist ungültig.");
        }
        var layers = profile.Tiles.Where(t => t is { Enabled: true } && (t.KeyboardBinding != null || t.CorsairBinding != null || t.VendorBinding != null) && !string.IsNullOrWhiteSpace(t.HeldLayer))
            .Select(t => t.HeldLayer.Trim()).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var tile in tiles)
        {
            if (!string.IsNullOrWhiteSpace(tile.HeldLayer) && (tile.KeyGesture != KeyGesture.Press || !string.IsNullOrWhiteSpace(tile.KeyLayer)))
                yield return Localization.Translate("Eine gehaltene Ebenentaste muss auf der Basisebene mit Sofortdruck belegt sein.");
            if (!string.IsNullOrWhiteSpace(tile.KeyLayer) && !layers.Contains(tile.KeyLayer.Trim()))
                yield return Localization.Translate("Die Standard-Sondertaste verwendet eine Ebene ohne gehaltene Ebenentaste.");
        }
        foreach (var group in tiles.GroupBy(t => ((t.KeyboardBinding!.DevicePath ?? "").ToUpperInvariant(), t.KeyboardBinding.InputKey, (t.KeyLayer ?? "").Trim().ToUpperInvariant())))
        {
            if (group.GroupBy(t => t.KeyGesture).Any(g => g.Count() > 1) || group.Any(t => t.KeyGesture == KeyGesture.Press) && group.Count() > 1)
                yield return Localization.Translate("Eine Standard-Sondertaste ist mit widersprüchlichen Gesten mehrfach belegt.");
        }
    }
}

namespace Aivana.Core;

public static class CorsairBindingValidator
{
    public static IReadOnlyList<string> ValidateTile(Tile tile)
    {
        ArgumentNullException.ThrowIfNull(tile);
        var errors = new List<string>();
        if (!Enum.IsDefined(tile.KeyGesture) || tile.HoldMilliseconds is < 150 or > 5000 || tile.DoubleTapMilliseconds is < 150 or > 1000)
            errors.Add(Localization.Translate("Tastengesten benötigen gültige Zeiten: Halten 150–5000 ms, Doppeldruck 150–1000 ms."));
        if ((tile.KeyLayer?.Length ?? 0) > 40 || (tile.HeldLayer?.Length ?? 0) > 40)
            errors.Add(Localization.Translate("Tastenebenen dürfen höchstens 40 Zeichen enthalten."));
        if (tile.CorsairBinding is not { } binding) return errors;
        if (string.IsNullOrWhiteSpace(binding.DeviceId)) errors.Add(Localization.Translate("Die Corsair-Tastenzuordnung braucht eine Geräte-ID."));
        if (!IsSupportedKeyId(binding.KeyId)) errors.Add(Localization.Translate("Die Corsair-Tastenzuordnung muss eine unterstützte G-Taste G1 bis G6 verwenden."));
        if (!string.IsNullOrWhiteSpace(tile.HeldLayer) && (tile.KeyGesture != KeyGesture.Press || !string.IsNullOrWhiteSpace(tile.KeyLayer)))
            errors.Add(Localization.Translate("Eine gehaltene Ebenentaste muss auf der Basisebene mit Sofortdruck belegt sein."));
        return errors;
    }
    public static IReadOnlyList<string> ValidateProfileBindings(Profile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);
        var errors = new List<string>();
        var groups = (profile.Tiles ?? []).Where(t => t is { Enabled: true, CorsairBinding: not null })
            .GroupBy(t => ((t.CorsairBinding!.DeviceId ?? "").ToUpperInvariant(), t.CorsairBinding.KeyId, (t.KeyLayer ?? "").Trim().ToUpperInvariant()));
        foreach (var group in groups)
        {
            var tiles = group.ToArray();
            if (tiles.Length > 1 && (tiles.Any(t => t.KeyGesture == KeyGesture.Press || !string.IsNullOrWhiteSpace(t.HeldLayer))
                || tiles.Select(t => t.KeyGesture).Distinct().Count() != tiles.Length))
                errors.Add(Localization.Translate((FormattableString)$"Die Corsair-G{group.Key.KeyId}-Taste auf Gerät „{tiles[0].CorsairBinding!.DeviceId}“ ist im Profil mehrfach zugeordnet."));
        }
        var layers = (profile.Tiles ?? []).Where(t => t is { Enabled: true } && (t.CorsairBinding != null || t.KeyboardBinding != null || t.VendorBinding != null) && !string.IsNullOrWhiteSpace(t.HeldLayer)).Select(t => t.HeldLayer.Trim()).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var tile in profile.Tiles ?? [])
            if (tile is { Enabled: true, CorsairBinding: not null } && !string.IsNullOrWhiteSpace(tile.KeyLayer) && !layers.Contains(tile.KeyLayer.Trim()))
                errors.Add(Localization.Translate((FormattableString)$"Die Tastenebene „{tile.KeyLayer}“ hat keine gehaltene Ebenentaste in diesem Profil."));
        return errors;
    }
    public static bool IsSupportedKeyId(int keyId) => keyId is >= 1 and <= 6;
}

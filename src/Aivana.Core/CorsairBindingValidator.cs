namespace Aivana.Core;

/// <summary>Validates persistent bindings to the Corsair iCUE SDK's supported K100 G1-G6 keys.</summary>
public static class CorsairBindingValidator
{
    public static IReadOnlyList<string> ValidateTile(Tile tile)
    {
        ArgumentNullException.ThrowIfNull(tile);
        if (tile.CorsairBinding is not { } binding) return Array.Empty<string>();

        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(binding.DeviceId))
            errors.Add(Localization.Translate("Die Corsair-Tastenzuordnung braucht eine Geräte-ID."));
        if (!IsSupportedKeyId(binding.KeyId))
            errors.Add(Localization.Translate("Die Corsair-Tastenzuordnung muss eine unterstützte G-Taste G1 bis G6 verwenden."));
        return errors;
    }

    /// <summary>Rejects duplicate enabled physical-key assignments within a profile.</summary>
    public static IReadOnlyList<string> ValidateProfileBindings(Profile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);
        var errors = new List<string>();
        var assigned = new HashSet<(string DeviceId, int KeyId)>(CorsairBindingKeyComparer.Instance);
        foreach (var tile in profile.Tiles ?? [])
        {
            if (tile is not { Enabled: true, CorsairBinding: { } binding } || string.IsNullOrWhiteSpace(binding.DeviceId) || !IsSupportedKeyId(binding.KeyId))
                continue;
            if (!assigned.Add((binding.DeviceId, binding.KeyId)))
                errors.Add(Localization.Translate((FormattableString)$"Die Corsair-G{binding.KeyId}-Taste auf Gerät „{binding.DeviceId}“ ist im Profil mehrfach zugeordnet."));
        }
        return errors;
    }

    public static bool IsSupportedKeyId(int keyId) => keyId is >= 1 and <= 6;

    private sealed class CorsairBindingKeyComparer : IEqualityComparer<(string DeviceId, int KeyId)>
    {
        public static CorsairBindingKeyComparer Instance { get; } = new();
        public bool Equals((string DeviceId, int KeyId) x, (string DeviceId, int KeyId) y) =>
            x.KeyId == y.KeyId && StringComparer.OrdinalIgnoreCase.Equals(x.DeviceId, y.DeviceId);
        public int GetHashCode((string DeviceId, int KeyId) value) =>
            HashCode.Combine(StringComparer.OrdinalIgnoreCase.GetHashCode(value.DeviceId), value.KeyId);
    }
}

namespace Aivana.Core;

public static class ProfileSelector
{
    public static Profile Select(AppSettings settings, string foregroundProcess, string windowTitle = "")
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(foregroundProcess);
        ArgumentNullException.ThrowIfNull(windowTitle);
        if (settings.Profiles is null || settings.Profiles.Count == 0)
            throw new InvalidOperationException("Es ist kein Profil konfiguriert.");

        var process = NormalizeProcessName(foregroundProcess);
        foreach (var profile in settings.Profiles)
        {
            if (profile is null) continue;
            var hasProcessCondition = !string.IsNullOrWhiteSpace(profile.ProcessName);
            var hasTitleCondition = !string.IsNullOrWhiteSpace(profile.WindowTitleContains);
            if (!hasProcessCondition && !hasTitleCondition) continue;
            if (hasProcessCondition && !NormalizeProcessName(profile.ProcessName).Equals(process, StringComparison.OrdinalIgnoreCase)) continue;
            if (hasTitleCondition && !windowTitle.Contains(profile.WindowTitleContains.Trim(), StringComparison.OrdinalIgnoreCase)) continue;
            return profile;
        }

        return settings.Profiles.FirstOrDefault(profile => profile?.Id == settings.DefaultProfileId)
            ?? settings.Profiles.FirstOrDefault(profile => profile is not null)
            ?? throw new InvalidOperationException("Es ist kein Profil konfiguriert.");
    }

    private static string NormalizeProcessName(string process)
    {
        var value = process.Trim();
        var separator = Math.Max(value.LastIndexOf('/'), value.LastIndexOf('\\'));
        if (separator >= 0) value = value[(separator + 1)..];
        return value.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ? value[..^4] : value;
    }
}

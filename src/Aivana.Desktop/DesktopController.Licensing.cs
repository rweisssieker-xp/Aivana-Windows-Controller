using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Aivana.Licensing;

namespace Aivana.Desktop;

public sealed class RetailLicenseStore(string directory, IReadOnlyDictionary<string,string> keys, string deviceId)
{
    public string DeviceId => deviceId;
    public LicenseValidationResult Validate()
    {
        try
        {
            var path = Path.Combine(directory, "license.json");
            return LicenseVerifier.Verify(File.Exists(path) && new FileInfo(path).Length <= 65536 ? File.ReadAllText(path) : "{}", keys, deviceId, 1);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        { return LicenseVerifier.Verify("{}", keys, deviceId, 1); }
    }
    public void Import(string json)
    {
        var result = LicenseVerifier.Verify(json, keys, deviceId, 1);
        if (!result.IsValid) throw new InvalidDataException(result.Error);
        Directory.CreateDirectory(directory);
        var temporary = Path.Combine(directory, Guid.NewGuid().ToString("N") + ".tmp");
        try { File.WriteAllText(temporary, json); File.Move(temporary, Path.Combine(directory,"license.json"), true); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}

#if !RETAIL_STORE_TEST
public sealed partial class DesktopController
{
    public static bool IsRetailBuild =>
#if MAKRIVO_RETAIL
        true;
#else
        false;
#endif
    private RetailLicenseStore? _retailLicense;
    public RetailLicenseStore RetailLicense => _retailLicense ??= CreateRetailLicenseStore();
    private static RetailLicenseStore CreateRetailLicenseStore()
    {
        using var machineKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");
        var identifier = machineKey?.GetValue("MachineGuid") as string;
        if (string.IsNullOrWhiteSpace(identifier)) throw new InvalidDataException(RetailText.Get("Windows device identity unavailable."));
        var deviceId = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("MAKRIVO:" + identifier))).ToLowerInvariant();
        using var stream = typeof(DesktopController).Assembly.GetManifestResourceStream("MAKRIVO.Retail.PublicKeys.json");
        var keys = stream == null ? new Dictionary<string,string>() : JsonSerializer.Deserialize<Dictionary<string,string>>(stream) ?? [];
        return new RetailLicenseStore(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"AivanaWindowsController"), keys, deviceId);
    }
    public bool CheckRetailEntitlement(string name)
    {
        if (!IsRetailBuild) return true;
        try { var result = RetailLicense.Validate(); if (result.IsValid) return true; Log(name, "MAKRIVO: " + RetailText.Get("License required") + ". " + result.Error); }
        catch (Exception ex) { Log(name, "MAKRIVO: " + ex.Message); }
        return false;
    }
}
#endif


public sealed partial class DesktopController
{
    internal static async Task DispatchPendingExecutionAsync(CancellationToken cancellationToken, Func<bool> canExecute,
        Func<Task> restoreTarget, Func<CancellationToken, Task> execute)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await restoreTarget();
        cancellationToken.ThrowIfCancellationRequested();
        if (!canExecute()) return;
        await Task.Run(() => canExecute() ? execute(cancellationToken) : Task.CompletedTask, cancellationToken);
    }
}

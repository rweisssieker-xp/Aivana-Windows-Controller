using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Aivana.Licensing;

public sealed record LicensePayload(int SchemaVersion, string Product, string LicenseId, int MajorVersion, int MaxDevices, string DeviceId, DateTimeOffset IssuedAt);
public sealed record LicenseValidationResult(bool IsValid, string? Error, LicensePayload? Payload);

public static class LicenseSigner
{
    public static string Sign(LicensePayload payload, string keyId, string privateKeyPem)
    {
        ArgumentNullException.ThrowIfNull(payload);
        if (string.IsNullOrWhiteSpace(keyId) || keyId.Length > 128) throw new ArgumentException("Invalid key ID.", nameof(keyId));
        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKeyPem);
        var bytes = JsonSerializer.SerializeToUtf8Bytes(payload, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        return JsonSerializer.Serialize(new { payload = Wire.Encode(bytes), signature = Wire.Encode(rsa.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)), keyId });
    }
}

public static class LicenseVerifier
{
    // Five minutes accommodates normal issuer/client clock drift. There is no expiry check.
    public static LicenseValidationResult Verify(string envelopeJson, IReadOnlyDictionary<string, string> publicKeys, string expectedDeviceId, int currentMajorVersion)
    {
        try
        {
            if (envelopeJson is null || envelopeJson.Length > 16384 || Encoding.UTF8.GetByteCount(envelopeJson) > 16384)
                return Invalid("License envelope exceeds its size limit.");
            using var envelope = JsonDocument.Parse(envelopeJson, new JsonDocumentOptions { MaxDepth = 4 });
            var fields = Wire.Fields(envelope.RootElement, ["payload", "signature", "keyId"]);
            var keyId = fields["keyId"].GetString();
            if (string.IsNullOrWhiteSpace(keyId) || keyId.Length > 128 || publicKeys is null || !publicKeys.TryGetValue(keyId, out var pem)) return Invalid("Unknown signing key.");
            // ImportFromPem accepts private keys too; restrict the PEM envelope before import.
            if (pem is null || pem.Length > 16384 || pem.Contains("PRIVATE", StringComparison.OrdinalIgnoreCase)) return Invalid("Trusted keys must contain public material only.");
            var trimmed = pem.Trim();
            bool spki = trimmed.StartsWith("-----BEGIN PUBLIC KEY-----", StringComparison.Ordinal) && trimmed.EndsWith("-----END PUBLIC KEY-----", StringComparison.Ordinal);
            bool pkcs1 = trimmed.StartsWith("-----BEGIN RSA PUBLIC KEY-----", StringComparison.Ordinal) && trimmed.EndsWith("-----END RSA PUBLIC KEY-----", StringComparison.Ordinal);
            if (!spki && !pkcs1) return Invalid("Invalid public key format.");
            using var rsa = RSA.Create();
            rsa.ImportFromPem(trimmed);
            if (rsa.KeySize < 3072) return Invalid("RSA key must have at least 3072 bits.");
            var bytes = Wire.Decode(fields["payload"].GetString(), 4096);
            var signature = Wire.Decode(fields["signature"].GetString(), 1024);
            if (!rsa.VerifyData(bytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)) return Invalid("Invalid license signature.");
            var json = new UTF8Encoding(false, true).GetString(bytes);
            using var document = JsonDocument.Parse(json, new JsonDocumentOptions { MaxDepth = 4 });
            var p = Wire.Fields(document.RootElement, ["schemaVersion", "product", "licenseId", "majorVersion", "maxDevices", "deviceId", "issuedAt"]);
            var issued = p["issuedAt"].GetDateTimeOffset();
            var payload = new LicensePayload(p["schemaVersion"].GetInt32(), p["product"].GetString()!, p["licenseId"].GetString()!, p["majorVersion"].GetInt32(), p["maxDevices"].GetInt32(), p["deviceId"].GetString()!, issued);
            if (payload.SchemaVersion != 1 || payload.Product != "MAKRIVO" || !Guid.TryParseExact(payload.LicenseId,"D",out var id) || id == Guid.Empty || payload.MajorVersion < 1 || payload.MajorVersion != currentMajorVersion || payload.MaxDevices != 4 || payload.DeviceId is null || !Regex.IsMatch(payload.DeviceId,"\\A[0-9a-f]{64}\\z") || payload.DeviceId != expectedDeviceId || issued.Offset != TimeSpan.Zero || issued > DateTimeOffset.UtcNow.AddMinutes(5)) return Invalid("License entitlement does not match this product, device, version or issuance policy.");
            return new(true, null, payload);
        }
        catch (Exception ex) when (ex is JsonException or FormatException or CryptographicException or ArgumentException or InvalidOperationException or KeyNotFoundException or OverflowException)
        {
            return Invalid("Malformed license or public key.");
        }
    }
    private static LicenseValidationResult Invalid(string error) => new(false, error, null);
}

internal static class Wire
{
    internal static string Encode(byte[] bytes) => Convert.ToBase64String(bytes).TrimEnd('=').Replace('+','-').Replace('/','_');
    internal static byte[] Decode(string? value, int maxBytes)
    {
        if (string.IsNullOrEmpty(value) || value.Length > (maxBytes * 4 + 2) / 3 || !Regex.IsMatch(value,"\\A[A-Za-z0-9_-]+\\z") || value.Length % 4 == 1) throw new FormatException();
        var decoded = Convert.FromBase64String(value.Replace('-','+').Replace('_','/') + new string('=',(4-value.Length%4)%4));
        if (decoded.Length > maxBytes || Encode(decoded) != value) throw new FormatException();
        return decoded;
    }
    internal static Dictionary<string,JsonElement> Fields(JsonElement element, string[] names)
    {
        if(element.ValueKind != JsonValueKind.Object) throw new FormatException();
        var fields = new Dictionary<string,JsonElement>(StringComparer.Ordinal);
        foreach(var property in element.EnumerateObject())
            if(!names.Contains(property.Name,StringComparer.Ordinal) || !fields.TryAdd(property.Name,property.Value)) throw new FormatException();
        if(fields.Count != names.Length) throw new FormatException();
        return fields;
    }
}

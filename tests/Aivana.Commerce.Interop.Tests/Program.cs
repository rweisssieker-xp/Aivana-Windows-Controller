using System.Security.Cryptography;
using System.Text.Json;
using Aivana.Licensing;

if (args.Length != 2) throw new ArgumentException("Usage: interop-tests <worker-public-fixture.json> <dotnet-public-fixture.json>");
using var fixture = JsonDocument.Parse(File.ReadAllText(args[0]));
var root = fixture.RootElement;
var envelope = root.GetProperty("envelopeJson").GetString()!;
var keys = JsonSerializer.Deserialize<Dictionary<string, string>>(root.GetProperty("publicKeys"))!;
var device = root.GetProperty("deviceId").GetString()!;
var result = LicenseVerifier.Verify(envelope, keys, device, 1);
if (!result.IsValid) throw new Exception("Worker-issued license rejected by .NET: " + result.Error);
if (LicenseVerifier.Verify(envelope, keys, new string('b', 64), 1).IsValid) throw new Exception("Foreign device accepted.");
if (LicenseVerifier.Verify(envelope, keys, device, 2).IsValid) throw new Exception("Unpurchased major version accepted.");
using var parsed = JsonDocument.Parse(envelope);
var payload = parsed.RootElement.GetProperty("payload").GetString()!;
var mutated = (payload[0] == 'A' ? "B" : "A") + payload[1..];
if (LicenseVerifier.Verify(envelope.Replace(payload, mutated, StringComparison.Ordinal), keys, device, 1).IsValid)
    throw new Exception("Tampered worker license accepted.");
Console.WriteLine("PASS worker-issued license verifies in .NET; tampering, wrong device and wrong major rejected");

using var rsa = RSA.Create(3072);
const string keyId = "ephemeral-dotnet-interop";
var signed = LicenseSigner.Sign(result.Payload!, keyId, rsa.ExportPkcs8PrivateKeyPem());
File.WriteAllText(args[1], JsonSerializer.Serialize(new { envelopeJson = signed, publicKeyPem = rsa.ExportSubjectPublicKeyInfoPem() }));
Console.WriteLine("Created .NET interop fixture with public material only.");

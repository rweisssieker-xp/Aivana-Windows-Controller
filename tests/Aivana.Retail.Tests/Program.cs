using System.Security.Cryptography;
using Aivana.Desktop;
using Aivana.Licensing;
var directory = Path.Combine(Path.GetTempPath(), "makrivo-retail-" + Guid.NewGuid());
try {
 using var rsa = RSA.Create(3072);
 var keys = new Dictionary<string,string> { ["test"] = rsa.ExportSubjectPublicKeyInfoPem() };
 var device = new string('a',64);
 var store = new RetailLicenseStore(directory, keys, device);
 if (store.Validate().IsValid) throw new Exception("Missing license accepted");
 var json = LicenseSigner.Sign(new LicensePayload(1,"MAKRIVO",Guid.NewGuid().ToString(),1,4,device,DateTimeOffset.UtcNow),"test",rsa.ExportRSAPrivateKeyPem());
 store.Import(json);
 if (!store.Validate().IsValid) throw new Exception("Valid persisted license rejected");
 try { store.Import(json.Replace("test", "unknown")); throw new Exception("Invalid import accepted"); } catch (InvalidDataException) { }
 if (!store.Validate().IsValid) throw new Exception("Invalid import destroyed valid file");
 if (new RetailLicenseStore(directory,keys,new string('b',64)).Validate().IsValid) throw new Exception("Wrong device accepted");
 if (new RetailLicenseStore(directory,new Dictionary<string,string>(),device).Validate().IsValid) throw new Exception("Missing keys accepted");
 var executed = false;
 await DesktopController.DispatchPendingExecutionAsync(CancellationToken.None, () => store.Validate().IsValid,
     () => { File.WriteAllText(Path.Combine(directory,"license.json"),"{}"); return Task.CompletedTask; },
     _ => { executed = true; return Task.CompletedTask; });
 if (executed) throw new Exception("License removed during queued restore still executed");
 store.Import(json);
 await DesktopController.DispatchPendingExecutionAsync(CancellationToken.None, () => store.Validate().IsValid, () => Task.CompletedTask, _ => { executed = true; return Task.CompletedTask; });
 if (!executed) throw new Exception("Licensed queued dispatch blocked");
 File.WriteAllText(Path.Combine(directory,"license.json"),"{}");
 if (store.Validate().IsValid) throw new Exception("Tampered file accepted");
 Console.WriteLine("Retail license file checks passed");
} finally { if (Directory.Exists(directory)) Directory.Delete(directory,true); }



// Exercise the same translation lookup used by the native retail dialog.
var applicationLanguages = Aivana.Core.Localization.SupportedLanguages.ToHashSet(StringComparer.Ordinal);
if (!applicationLanguages.SetEquals(RetailText.SupportedLanguages)) throw new Exception("Retail/app language inventories differ");
foreach (var language in applicationLanguages)
{
 Aivana.Core.Localization.SetLanguage(language);
 foreach (var key in RetailText.Keys)
  if (string.IsNullOrWhiteSpace(RetailText.Get(key))) throw new Exception("Empty retail translation: " + language + "/" + key);
 if (RetailText.Get("unknown-test-key") != "unknown-test-key") throw new Exception("Unknown retail key was changed");
}
Aivana.Core.Localization.SetLanguage("auto");
Console.WriteLine("PASS retail lookup for every application language and unknown-key fallback");

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
// The first-run price line must identify one person and applicable included taxes
// in every supported language, in addition to the price and four own PCs.
var offerTerms = new Dictionary<string,(string person,string tax)>(StringComparer.Ordinal)
{
 ["en"]=("one person","applicable taxes"), ["de"]=("eine Person","anwendbarer Steuern"),
 ["fr"]=("une personne","taxes applicables"), ["es"]=("una persona","impuestos aplicables"),
 ["it"]=("una persona","imposte applicabili"), ["pt"]=("uma pessoa","impostos aplicáveis"),
 ["nl"]=("één persoon","toepasselijke belastingen"), ["pl"]=("jedna osoba","należne podatki"),
 ["cs"]=("jedna osoba","příslušných daní"), ["sk"]=("jedna osoba","príslušných daní"),
 ["hu"]=("egy személy","alkalmazandó adókkal"), ["ro"]=("o persoană","taxele aplicabile"),
 ["bg"]=("едно лице","приложими данъци"), ["el"]=("ένα άτομο","ισχυόντων φόρων"),
 ["da"]=("én person","gældende skatter og afgifter"), ["sv"]=("en person","tillämpliga skatter"),
 ["nb"]=("én person","gjeldende skatter og avgifter"), ["fi"]=("yksi henkilö","sovellettavat verot"),
 ["et"]=("üks isik","kohaldatavate maksudega"), ["lv"]=("viena persona","piemērojamos nodokļus"),
 ["lt"]=("vienas asmuo","įskaitant taikomus mokesčius"), ["sl"]=("ena oseba","veljavnimi davki"),
 ["hr"]=("jedna osoba","primjenjivim porezima"), ["uk"]=("одна особа","включно із застосовними податками"),
 ["ru"]=("один человек","включая применимые налоги"), ["tr"]=("bir kişi","geçerli vergiler"),
 ["sr"]=("jedna osoba","primenljivim porezima")
};
if (!applicationLanguages.SetEquals(offerTerms.Keys)) throw new Exception("Offer-language inventory differs");
foreach (var (language, terms) in offerTerms)
{
 Aivana.Core.Localization.SetLanguage(language);
 var offer = RetailText.Get("29 EUR including applicable taxes · one person · up to 4 own PCs");
 if (!offer.Contains("29 EUR",StringComparison.Ordinal) || !offer.Contains("4",StringComparison.Ordinal) ||
     !offer.Contains(terms.person,StringComparison.Ordinal) || !offer.Contains(terms.tax,StringComparison.Ordinal))
  throw new Exception("Incomplete first-run offer: " + language);
}
Aivana.Core.Localization.SetLanguage("de");
if (RetailText.Get("One-time purchase, no subscription") != "Einmaliger Kauf, kein Abonnement") throw new Exception("German one-time purchase terms differ");
Aivana.Core.Localization.SetLanguage("en");
if (RetailText.Get("One-time purchase, no subscription") != "One-time purchase, no subscription") throw new Exception("English one-time purchase terms differ");
Console.WriteLine("PASS first-run price, one-person and applicable-tax disclosures in every application language");
Aivana.Core.Localization.SetLanguage("auto");
Console.WriteLine("PASS retail lookup for every application language and unknown-key fallback");

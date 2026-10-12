<!-- generated-doc locale=sr-Latn source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Proširenja Plan implementacije

> **Za agentske radnike: ** POTREBNO SLUŽBA: Upotreba supermoći:Planovi izvršenja za lokalnu izvršenje ili supermoć:Službeni radnik-upravljen razvoj nakon što korisnik izabere delegaciju. Sledite korake sa čekovima.

**Cilj:** Dostavite šest integrisanih proširenja i podršku multi-proizvođača na bazi kapaciteta.

**Arhitektura:** Sačuvajte jezgra / Windows / WPF Uvodite izolovani plugin i mobilne usluge procesa sa uskom autentifikovanom IPC; sve izvršenje prolazi kroz postojeći kontroler i MacroRunner kueu..

**Tehnički stak:** C# , .net 10, WPF, Windows UI automatizacija, Win32 Raw Input, Dokumentirani provajder SDK, ASP.NET Core HTTPS/WebSocket, Sistem.tekst.Jon.

**Spec: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (prihvaćen od strane korisnika).

## Globalni ograničenja

- Postojeća konfiguracija mora biti nepromenjena; dodajte vrste akcija umesto da ponose svoje trajne vrednosti.
- Sačuvajte postojeće profile, G1/G2 zadatke, lokalnu operaciju i besplatnu upotrebu.
- Raw Input Ekskluzivno uklanjanje po uređaju zahteva stvarnu tastaturu filtera.
- Importirani izvršni plagini ne bi trebalo da se automatski izvršavaju.
- Definisano da deaktiviraju i ograničavaju konfigurisane interfejse za mobilnu uslugu; nema paunda autentifikacije žalbe.
- Kratki / tekstualna isporuka nije dokaz da je ciljna aplikacija prihvatila komandu.
- Nemojte zameniti instaliranu aplikaciju, instalirati vozače, menjati stvarne tastature ili otvoreni pristup mreži kao incident.
- Sačuvajte neproporcionalne promene. Nemojte angažovati postojeće zajedničke uređaje kao deo ove funkcije. Stadijum samo nezavisno pregledane funkcije ako se zahtevaju posvećenosti.

## Pregled Fokus

1. Dvostruki identični modeli tastature: dvosmisleno rekontektiranje mora zahtevati ponovno odabir (Tasks 10–12).
2. Ukidanje tokom UIA / provajdera / plugina / mreže čeka: nema odloženih akcija nakon ukidanja (Zadaci 1, 4, 6, 8).
3. Korisnici uređuju dok je čarobnjak ili pregled paketa otvoren: revizija greška treba da spreči tiho prepisivanje (Zadaci 3, 7).
4. Jezik i fizički raspored se razlikuju: selektori i skeniranje kodova ne bi trebalo da zavise od lokalizovanih imena prikazivanja (Zadaci 4, 10).
5. Mreža retri i plugin ponovo pokreće: komande ne moraju tiho izvršiti dva puta (Zadaci 6, 8).

## Podeljeni ugovori i poredak zavisnosti

Zadaci 1–3 uspostavljaju izvršenje i postavljanje. Zadaci 4–5 izgradnja UIA. Zadaci 6–7 isporučuju plagine. Zadaci 8–9 pružaju mobilni pristup. Zadaci 10–12 isporučuju adaptere tastature i integraciju filtera. Zadatak 13 potvrđuje ceo proizvod.

Svaka zadatak prati neuspeh test → minimalna implementacija → prolazak test ciklus. Zadatak je završen samo kada je dostupan kroz aplikaciju i njegove provere prolaze. Skafoldi, mock adapteri i dokumentacija sami ne zadovoljavaju isporučivo.

### Zadatak 1: Tipirana verifikacija i kompatibilna upornost

**Datoteke:** Kreirajte `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Modifikovanje `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfejs:** Proizvodnja `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` i `VerifyAsync(ActionStep, CancellationToken)`, Oba vraćanja `Task<ActionVerificationResult>`. Dodajte opcionalno serializovane `VerificationOptions` Uključivanje znamena i vremenskog (100–60000 ms), po podrazumevanju deaktiviran. objavljivanje štampanih rezultata po koraku, uključujući ID pokretanja, ID tale i Indeks koraka bez akcija plaćanja.

- [ ] Pisanje provere: nasleđe JSON zadržava postojeće brojne vrednosti akcije; odsutni opcije onemogućavaju verifikaciju; nevalifikacija neuspjeh vremena; preflajt neuspjeha sprečava poziv izvršioca; neuspješna post-kondicija nikada ne proizvodi verifikovani uspeh; nepodržan izvršioc je Neverifikovana; otklanjanje zaustavlja verifikacije i sledeće korake.
- [ ] Upravljanje `dotnet run --project tests/Aivana.Tests -c Release`; potvrditi nove provere ne uspevaju pre implementacije.
- [ ] Uvođenje ugovora, validacija, kloniranje/snapshot konzervacija i integracija MakroRunner. Poštujte OnError stop/continue politike; sačuvajte postojeće povratne informacije pretplatnika.
- [ ] Izvršite testove; potrebno je da prođe sve provere.

### Zadatak 2: Windows Provajder verifikacije i UI rezultata

**Datoteke:** Kreirajte `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Modifikovanje `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Test ulazak.

**Interfejs: ** VindovsActionVirifikacija implementira Trak 1 provajder koristeći postojeće audio, ekran, proces / prozor i OBS klijenti. Preflet testira dostupnost i identifikatori; verifikacija upoređuje željeno stanje nakon ograničenog istraživanja. Proizvodi kontrolera rezultata snimaka potrošenih po istorijskom pogledu WPF.

- [ ] Dodajte neuspješne testove za nedostupne eksplicitne audio/displej ID, OBS vreme, pogrešno primećeno stanje, nepoznati efekti skraćenja, osetljiva opterećenja i otkazana anketa, koristeći provajdere za ubrizgavanje.
- [ ] Implementacija stvarnog čitanja potvrde za apsolutni volumen, mut, apsolutna sjajnost, proces / prozor spremnost i podržane OBS komande. Relativne / tegge akcije zahtevaju zarobljenu pre-državu; inače izveštaj Neproverivo.
- [ ] Dodajte korak po korak kontrole i bezbedan istoriju izvršenja koji pokazuju stanje, neuspeh zahteva i tekst oporavka.
- [ ] Upravljanje jezgra / Windows provera i WPF dim sa ubrizganim državnim provajderima; nema stvarne svetlosti ili audio pisanja.

### Zadatak 3: Upravljen radni prostor

**Datoteke:** Kreirajte `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modifikovanje palete i tačaka za ulazak.

**Interfejs: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; izbor sadrži ID šablona, izabrane izvršne puteve, URL-a, audio/displej ID, OBS scenu i izabrane veze. Discovery vraća imenovane kandidate sa stabilnim ID-ovima i dostupnošću. Projekat revizije konfiguracije za otkrivanje konflikta.

- [ ] Dodajte neuspješne testove za sve pet zahtevanih porodica čarobnjaka, nezavisno generisane korake, nedostaju potrebne ciljeve, otklanjaju bez mutacije i štede od promene revizije.
- [ ] Otkrijte aplikacije putem registrovanih aplikacija i Start menija skraćenica; uređaji kroz postojeće provajdere; scene kroz samo čitanje OBS pitanje. Držite ručnu selekciju dostupnu za nepoznate aplikacije.
- [ ] Izbor radnog toka → Izbor ciljeva → Izbor kontrola → Pregled/validacija → Sačuvaj. Uzmite uređujuće profile i nikada ne radite tokom podešavanja.
- [ ] Dodajte stvarne WPF provere za pušenje za završetak/ukidanje/konflikt; sprovodite sve testove.

### Zadatak 4: UIA selektori i ograničene akcije

**Datoteke:** Kreirajte `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modifikovanje modela, validacija konfiguracije i VinActionEkcetor.

**Interfejs: ** Dodavanje `ActionKind.UiAutomation`; `UiElementSelector` skladištenje procesa, identifikator automatizacije, tip kontrole i kriterijumi za narudžbinu. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` vraća `Task`; operacije Invoke, SetValle, Izbor, Fokus i Čekaj. Rezolucija selektora mora vratiti tačno jedan prihvatljiv element.

- [ ] Dodajte neuspešne testove protiv posvećenog prozora za premeštanje / rezize, dupliranje utakmica, nedostajuće elemente, nepodržene obrasce, deaktivirane kontrole, vrednosti lozinke, Etike i vreme izlaska / otklanjanja.
- [ ] Implementacija UIA na posvećenog radnika sa ograničenim zahtevima i procesom ciljanja. Potencijalno povezane provajdere trčaju iza ponovnog granice radnika; napuštanje zahteva ne bi trebalo da ostavi neželjene efekte.
- [ ] Isključite vrednosti lozinke; prijavite ograničenja pristupa / integriteta umesto da ih prelazite.
- [ ] Izvodite Windows provere koristeći izolovane fikcije; dodajte samo vrednosti i proverite upornost nasleđa.

### Zadatak 5: UIA picker i pregledano snimanje

**Datoteke: ** Kreirajte `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modifikuje katalog akcija, uređaj i integraciju snimaka; kreirajte UIA WPF kontrole dima.

**Interfejs: ** Picker vraća `UiElementSelector`; Rekorder vraća predložene korake UIA i nikada ne piše direktno u sačuvan profil. Komanda test-izbornik vraća broj utakmice bez poziva.

- [ ] Dodajte neuspješne provere: picker isključuje MAKRIVO i lozinku-vrednost uhvatiti; zapis / otklanjanje listovi postojeći profil nepromenjen; dvosmisleni pregled izveštaja u skladu sa brojem; prihvatanje zapisanih koraka stvara uređujuće dizajn.
- [ ] Implementacija elementa naglasak/picker, operacija-specifični parametri, pregled selektora i prozor pregleda za snimanje. Održava postojeće ponašanje kratkotrajnog rekorda.
- [ ] Dodajte katalog ulaze i palete pristup; validirajte greške uređaja pre nego što sačuvate.
- [ ] Izvodite Windows i WPF kontrole dima na fiksiranju; inspektirajte snimak ekrana pickera / uređaja.

### Zadatak 6: Izolisani plugin host i protokol

**Datoteke:** Kreirajte `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Modifikovanje rešenja i distribucija.

**Interfejs:** Version-1 manifest određuje identifikator paketa / verziju, ulaznu tačku, šeme parametara, akcije, državne provajdere i izjave o mogućnostima. IPC poruke sadrže verziju protokola, jedinstvenu identifikaciju zahteva, metodu, JSON parametre i tipovan rezultat. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Povratak rezultata. dodavanje `ActionKind.Plugin`.

- [ ] Dodajte neuspješne testove za nekompatibilan protokol, poremećeno / prevelike poruke (>1 MiB), dupliranje ID zahteva, ukidanje, nesreća, neodgovorni proces i nedostaje akcija.
- [ ] Implementacija istoimenovanog IPC sa 10-sekund ručno i konfigurisano vreme izvršenja uhvaćeno na 60 sekundi. Prekinite neodgovorni radnik; ne igrajte automatski akciju u letu nakon ponovnog pokretanja.
- [ ] Uvođenje shema i dozvoljene kontrole. Objasnite izvršivo-plagin poverenje; procesna izolacija nije pesak OS.
- [ ] Izvršite testove i testove integracije protiv stvarnih procesa testiranja.

### Zadatak 7: Plugin pakovanja i primjer integracije

**Datoteke:** Kreirajte `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, testiranje paketa; modifikovanje paleta / podešavanja.

**Interfejs:** Pakovanje izložbe `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` i `UninstallAsync(packageId)`. Pregledani paket zapisi haše, zavisnosti, verzije i deklarisane mogućnosti.

- [ ] Dodajte neuspješne testove za zip prolaz, apsolutne puteve, simlink / reparse izbegavanje, dupliranje unosa, >100 MiB prošireni podaci, hash pogrešnost, spuštanje, pregled stola i invalidirani uvozi.
- [ ] Implementacija pregled i eksplicitno omogućavanje, greške zavisnosti, stadijum instalacije i ažuriranja; otklanjanje radnika pre uklanjanja.
- [ ] Dostaviti stvarni primjer plugin vraćajući konfigurisane akcije računanja i živog stanja kroz IPC, uključujući i otkazivanje; pakovanje / izvoz / instalacija / deinstalacija kroz UI.
- [ ] Izvodite pakete / testove domaćinstva i WPF krajnje provere. Proverite da nema uvoza izvršnih trka pre odobrenja.

### Zadatak 8: HTTPS mobilni servis i parenje

**Datoteke:** Kreirajte `src/Aivana.MobileHost/` ASP.NET Core Projekat, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Istraživanje integracije; modifikovanje rešenja i kontrolera.

**Interfejs:** Mobilni servis razgovori za kontrolera preko istog korisnika IPC. Putevi: parenje, dozvoljeni profili / akcije, autentifikovani poziv za akciju, anulacija i WebSocket status. Invokacija uključuje jedinstvenu identifikaciju komande; kontroler poseduje ograničenu deduplikaciju. Potvrde se mogu povratiti i čuvati putem zaštite Windows.

- [ ] Dodajte neuspješne testove za neiscrpnu listu/ispostavljanje, iscrpljen kod, ponovljeno korišćeni kod, otklonjeni token, nevažeći poreklo, dupliranje komande, preveliki okvir i ograničenja navodnjavanja.
- [ ] Implementacija HTTPS-samo slušalac, korisnički izabrani interfejsi, lokalni sertifikat pružanje / poverenje uputstva, jednokratno 6-digitalno pare kodove istekuju nakon 120 sekundi, maksimum pet pokušaja po kodu i tokena povlačenje. Skladištenje hašenih slučajnih tokena; nikada ne prijavljuju se.
- [ ] Implementacija omogućava navedene stabilne identifikatore akcije i postojeće routing; odbijati proizvoljne šale, puteve i komande za instalaciju paketa. Podesite eksplicitne ograničenja stope po klijentu (10 komande/sek, 30 bust) i 64 KiB poruka.
- [ ] Proverite testove integracije bez dodavanja pravila vatrenog zida ili omogućavajući pristup LAN-u; testirajte zatvaranje i upornost tokena.

### Zadatak 9: Mobilni panel i desktop kontrole

**Datoteke:** Kreirajte `src/Aivana.MobileHost/wwwroot/` Reaktivni panel i `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Dodajte pretraživač / mobilni verifikacioni skripti i WPF Dim pokriven.

**Interfejs: ** Potrošnja zadataka 8 putevi samo. Pokažite mrežu akcije dodir, profil picker, živa vrednost / napredak, otklanjanje, status veze i parenje. Desktop kontroliše usluge početak / zaustavljanje, adresa / sertifikat uputstva, dozvoljeni profili i povlačenje klijenta.

- [ ] Dodajte neispravne kontrole integracije za auth toka, stale / izgubljene identifikatore akcije, ponovo povezivanje bez reprodukcije akcije, povlačenje klijenta i prekid usluge.
- [ ] Uvođenje pristupačnog korisničkog interneta koristeći lokalne imovine, bez skripta treće strane ili internet zavisnosti. Tokeni se nikada ne pojavljuju u URL-u.
- [ ] Test raspored pretraživača na 360/768/1280 CSS pk; inspektiraju stvarne snimake. Proverite sa pravim telefonom / tabletom samo nakon što korisnik izričito omogućava LAN uslugu.
- [ ] Dokument i izveštaj mobilne hardverske verifikacije odvojeno od emulacije pretraživača.

### Zadatak 10: Kapacitet uređaja i uobičajeni ključ za posmatranje

**Datoteke:** Kreirajte `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modifikovati RawKeyboardBindingService, KeyboardBinding, validator i dijagnoza uređaja; dodati testove.

**Interfejs: ** `IKeyboardAdapter` izlaga kapacitet snimaka, stabilnu identitet uređaja i fizičke ključne ivice (skaniranje koda, proširene zname, štampanje / oslobađanje). Mode razlikuju posmatranje, mapirani standardni ulaz, indijanski SDK i ekskluzivni vozač. Postojeći virtuelni ključ posebne veze zadržavaju svoje predstavništvo i migraciono ponašanje.

- [ ] Dodajte neuspješne testove za stare veze, Unesite razlike, nezavisnost jezika / rasporeda, ponavljajuće spuštanje, odbacivanje dok se drži, dvostrukost modela i odbijanje unosa.
- [ ] Proširite posmatranje na uobičajene fizičke ključeve bez suzbijanja. Dodajte eksplicitne uhvati i ponovo poveziti identitet.
- [ ] Pokazati stvarne kapacitete adaptera i hardversko-neprobano stanje u UI uređaja; nikada ne inferirati Fn/dial podršku iz brendova.
- [ ] Provođenje jezgra/Windows/WPF provera sa simuliranim izveštajima i čitanjem samo uređaja.

### Zadatak 11: Proizvođač adaptera

**Filovi:** Kreirajte odvojene datoteke pod `src/Aivana.Windows/KeyboardAdapters/` za LogitechG, RazerHroma, StilSersiGemSense, VotingAnalig i generalno KmkVia vodič; modifikovati registar/UI i dodati protokol kontrole adaptera.

**Interfejs:** Konzum zadatak 10 Adapter ugovora. Rođene biblioteke se preuzimaju samo sa validiranih lokacija aplikacije / prodavca; odsustvo SDK je nemogućnost. Kapacitet osvetljenja i ulaza su odvojeni.

- [ ] Proverite trenutne zvanične SDK naslove, uslove redistribucije, zavisnosti softvera i podržane modele pre implementacije svakog adaptera; zapis izvora i tačne verzije.
- [ ] Dodajte neuspješne testove za SDK odsutan/neprihvatljiv arhitekturu, događaj presa/reliz/ponavljanje, dekonnekt, pogrešan odgovor i nepodržan uređaj. Koristite injektivne abstrakcije za determinističke preglede.
- [ ] implementacija Logitech G-key ulaz i podržava osvetljenje odvojeno; Razer Hroma osvetljenje plus Synapse Standardni ključ za mapiranje; SteelSeries GameSense država / svetlosti; Wooting Analgična anketa sa konfigurisanim granicama / histerezom; QMK/VIA Standardni ključ podešavanja vodič. Ne zahtevaju vlasnički ulaz iz API osvetljenja.
- [ ] Validirajte kompatibilne realne uređaje gde su dostupne. Identifikujte druge implementirane / hardver-neprobani, sa model / hardvera dokaz; nedostajući korisni SDK je otkrivena spoljna vrata.

### Zadatak 12: Ekskluzivni filter tastature

**Datoteke:** Kreirajte `src/Aivana.Windows/KeyboardFilter/`, Instalirajte izvor `tools/keyboard-filter/`, Završena integracija testova i dokumentacija za raspoređivanje. posvećen potpisani projekat je potreban ako nije pogodan licenciran pozadinu prolazi procena.

**Interfejs: ** Backend izveštaje instaliran/kompatibilan/podpisani stanje; izlaga uređaj-specifični ulazak / zaustavljanje, ključne ivice i hitne oslobađanja. Odvojeni nadzornik vraća prolaz kroz gubitak kontrolera. Glavni tastatura nikada ne postaje ekskluzivna po podrazumevanju.

- [ ] Ocenite kandidatsku vozačku licencu, potpisani binarni izvor i trenutnu Windows 11/Secure kompatibilnost. Odluka o odlasku/no-go Nema instalacije vozača tokom običnih testova.
- [ ] Napišite ugovorne testove za izbor filtera, neuspeh-otvoreno, iscrpljenje srca, gubitak IPC, ponovo povezivanje nedvosmislenosti, pauza / otklanjanje, držeći ključ oslobođenje i uklanjanje.
- [ ] Implementacija backend, eksplicitni izbor/aktivacija UI, hitno oslobađanje i instalator/neinstalator. Nemojte zameniti nisko nivo hok/Raw Input vremenske hjuristike.
- [ ] Validirajte u ponovnom testu sa dve tastature: izabrani ključ je ukinut samo na izabranom uređaju; glavni štampanje nije pogođeno; povraćanje / suspendiranje / deinstalacija ulaza.
- [ ] Završena proizvodnja vrata na stvarnom potpisanom / licenciranom pozadini i fizičkim testovima. Ako spoljni potpis / licenca nije dostupan, zadatak je nepotpuno sa preciznim dokazom zavisnosti.

### Zadatak 13: Integrisana verifikacija, prevodi i izdavanje paketa

**Datoteke: ** Dodajte `docs/testing/2026-10-12-six-extensions.md`; ažurirajte relevantnu DE/EN/FR/IT dokumentaciju, kataloge lokacije i tačke ulaza za pregled dima. Sačuvajte druge dokumentacije.

- [ ] Integrirajte sve ulazne tačke i snimak; završite prevode koristeći radni tok kataloga na postojećem 27 jeziku i otkrijte neproverene prevode.
- [ ] trčanje `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, i `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Ispitivanje desktop / mobilni ekran snimci i proveriti 100/150/200 procenat Windows skaliranje, konfiguracija kružne putovanja, život plugging, mrežne revolucije i stvarne tastature ponovo povezivanje.
- [ ] Objavljuje se u novu direktorijum sa `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; uključuje usluge partnera / domaćina i obaveštenja o licenci. Nemojte prepisati instaliranu aplikaciju.
- [ ] Izveštaj završenih testova, hardver-neprobani adapteri i spoljne vrata za oslobađanje. Svi šest proizvođača moraju ispuniti kriterijume prihvatanja pre nego što pozove celu zahtev.

## Plan samo-revizije

Svi šest sekcija spek u mapi za Zadaci 1–12; Zadaci13 pokrivaju lokalizaciju, integrisane verifikacije i oslobađanja. Priprema za izveštavanje, serijalizacija izvršenja, konfiguracija nasleđa, tajne, poverenje paketa i fizičko-priborne nedvosmislenosti imaju eksplicitne vlasnika i testove. Spoljni vozač / SDK / hardverske vrata ostaju nepotpune dok se ne dokaže, umesto da se računa kao uspeh softvera.

## Izbor izvršenja za korisnički pregled

Preporučuje se: originalna izvršenja u ovom chat, zadatak po zadatku, jer većina modula deli ugovore i trenutni radni prostor sadrži značajne postojeće uređenja. Delegirana izvršenje je dostupno ako korisnik izabere. Pregledajte ovaj plan i izaberite metodu izvršenja pre implementacije, kao što zahteva radni tok pisanja planova.

<!-- generated-doc locale=sl-SI source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Razširitev načrta za izvajanje

> **Za agentične delavce:** PREDVEDNOST PODPOVEDNOST: Uporaba supermoči:izvajanje načrti za domačo izvedbo ali supermoči. Spremljajte korake s kontrolnimi škatlami.

**Cilj:** Dostava šest integriranih razširitev in podporo več proizvajalcev na osnovi zmogljivosti.

**Arhitektura:** Ohranite jedro / Windows / WPF ločitev. uvajanje izoliranih plugin in mobilne storitve procesov s tesno preverjeno IPC; vse izvedbe gredo skozi obstoječi upravljavec in MacroRunner..

**Tehnični stack:** C# in .NET 10, WPF, Windows UI avtomatizacija, Win32 Raw Input, Podatkovani ponudnik SDK, ASP.NET Core HTTPS/WebSocket, Sestavine.Json.

**Spec: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (pooblaščena s strani uporabnika)

## Globalne omejitve

- Obstoječa konfiguracija mora naložiti nespremenjeno; dodajanje vrst ukrepov namesto ponovnega urejanja njihovih neprekinjenih vrednosti enuma.
- Ohranite obstoječe profile, naloge G1/G2, lokalno delovanje in brezplačno uporabo.
- Raw Input Izključno odstranitev na napravo zahteva dejanski filter za tipkovnico.
- Uvoženi izvedljivi vtičniki se ne smejo izvajati samodejno.
- Privzeto onemogočite in omejite konfigurirane vmesnike za mobilno storitev; nobenih pritožbenih preverjanj.
- Kratek/tekstno dostavo ni dokaz, da je ciljna aplikacija sprejela ukaz.
- Ne nadomestite nameščene aplikacije, namestite gonilnike, spremenite resnične mape na tipkovnici ali odprte omrežje kot naključnega testa.
- ohraniti nezakonite spremembe. Ne zavezujte se obstoječim skupnim urejanjem kot del te funkcije. Stage samo neodvisno pregledljive funkcije, če so povpraševanja.

## Pregled fokus

1. Podvojite enake tipkovnice: dvomljiva ponovna povezava zahteva ponovno izbiro (Tasks 10–12).
2. Preklic med UIA / ponudnik / plugin / omrežje čaka: nobenih ukrepov po preklicu (Uredi 1, 4, 6, 8).
3. Uporabnik ureja, medtem ko je predogled čarovnika ali paketa odprt: revizija mora preprečiti tiho prepisovanje (Tasks 3, 7).
4. Jezik in fizična postavitev se razlikujejo: izbranci in skenerji ne smejo biti odvisni od lokaliziranih imen prikazovanja (Zadnje 4, 10).
5. Omrežna vrnitev in ponovno zagon: ukazi se ne smejo tiho izvajati dvakrat (Zadnje 6, 8).

## Skupni pogodbi in odvisnostni red

Task 1–3 vzpostaviti izvedbo in namestitev. Task 4–5 graditi UIA. Task 6–7 zagotavlja dodatke. Task 8–9 zagotavlja mobilni dostop. Task 10–12 zagotavlja tipkovnico adapterjev in filtracijo. Task 13 potrjuje celoten izdelek.

Vsaka naloga sledi neuspešnemu testiranju → minimalnemu izvajanju → testnemu ciklu. Delo je dokončano le, ko je dostopno prek prijave in njene kontrole potekajo. Scaffolds, mock adapterji in dokumentacija sami ne zadovoljujejo dosegljivega.

### Naloga 1: Tipirana preverjanja in združljiva trajnost

**datoteke:** Ustvarite `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Modifikacija `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Vmesniki:** Proizvodnja `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` in `VerifyAsync(ActionStep, CancellationToken)`, obeh vrnitve `Task<ActionVerificationResult>`. Dodajanje opcijskega serija `VerificationOptions` z omogočenim zastavom in časom (100–60000 ms), privzeto onemogočeno. objavite rezultate po korakih, vključno z ID-jem za izvedbo, ID-jem in indeksom korakov brez plačilnih nalog.

- [ ] Pisanje preverjanja: dediščina JSON ohranja obstoječe številne vrednosti ukrepa; odsotne možnosti onemogočajo preverjanje; neupravičeno časovno odpravljanje ne potrjuje; neuspešna poizvedba nikoli ne povzroča preverjenega uspeha; nepodprt izvršitelj ni mogoče preveriti; preklic ustavi preverjanje in nadaljnje korake.
- [ ] Izvedite `dotnet run --project tests/Aivana.Tests -c Release`; potrdite, da nove kontrole ne uspejo pred izvajanjem.
- [ ] Uvedba pogodb, validacija, kloniranje/snapshot ohranjanje in integracija MacroRunner. Upoštevajte politike OnError stop/continue; ohranite obstoječe povratne informacije naročnikov.
- [ ] Izvedite jedrske teste; vse preglede so potrebne.

### naloga 2: Windows preverjanje ponudniki in UI

**datoteke:** Ustvarite `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Modifikacija `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Preizkušeni vstopni točki.

**Spremenke: ** WindowsActionVerification implementira opravilo 1 ponudnik z obstoječimi avdio, prikaz, proces / okno in OBS stranke. Preflight testiranja razpoložljivosti in identifikacije; preverjanje primerja želeno stanje po omejenem pregledu. Izdelava kontrolor rezultatov snemanja porabljenih z WPF zgodovinsko pogled.

- [ ] Dodajte neuspešne teste za neobstoječe izrecne avdio/izgledne ID, OBS časovni izid, nezadostno opazno stanje, neznane učinke kratkega odtisa, občutljivo obremenitev in odpovedan pregled, z uporabo injektivnih ponudnikov branja.
- [ ] Uvedba resnične preverjanja za absolutno količino, neumnost, absolutno svetlost, pripravljenost procesa / okna in podprte ukaze OBS. Relativne/toggle ukrepe zahtevajo ujeto pre-stanje; drugače poročati nepreverljivo.
- [ ] Dodaj korak za korakom preverjanja in varnostno zgodovino izvedbe, ki prikazuje stanje, neuspešno zahtevo in besedilo za obnovitev.
- [ ] Izvedite jedro/Windows pregledi in WPF kajenje z injiciranimi državnimi ponudniki; nobena resnična svetlost ali avdio pisanje.

### Task 3: Ustvarjanje usmerjenega delovnega prostora

**datoteke:** Ustvarite `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; spremenite paleto in vstopne točke.

**Vmesniki: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; izbira vsebuje ID predloga, izbrane izvedljive poti, URL-je, audio/display IDs, OBS scene in izbrane povezave. Discovery vrača imenovane kandidate z stabilnimi ID-ji in razpoložljivostjo. Načrt zapisov konfiguracije revizije za odkrivanje konfliktov.

- [ ] Dodajte neuspešne teste za vse pet zahtevane čarovnice, neodvisne korake, manjkajoče zahtevane cilje, preklic brez mutacije in shranite proti spremenjenemu pregledu.
- [ ] Odkrijte aplikacije prek registriranih aplikacijskih poti in skrajšanj Start Menu; naprave skozi obstoječe ponudnike; scene skozi samo OBS poizvedbo. Upoštevajte ročno izbiro za neobjavljene aplikacije.
- [ ] Implement Izberite delovni tok → Izberite cilje → Izberete kontrolnike → Pregled/validacija → Shrani. Ustvarite urejljive profile in nikoli ne delujte med nastavitvijo.
- [ ] Dodajte dejansko WPF čarovnik smoke preverjanja za dokončanje / preklic / konflikt; opravite vse teste.

### Naloga 4: UIA selektorji in omejene ukrepe

**datoteke:** Ustvarite `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modifikacija modelov, validacija konfiguracije in WinActionExecutor.

**Vmesniki:** Dodajanje `ActionKind.UiAutomation`; `UiElementSelector` Storitve proces, avtomatizacija ID, kontrolni tip in naročen predhodnik merila. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Vrne se `Task`; operacije Invoke, SetValue, Select, Toggle, Focus in WaitState..

- [ ] Dodajte neuspešne teste proti posvečeni testiranju za premikanje/odstranitev, dvakratne tekme, manjkajoče elemente, nepodprte vzorce, onemogočene kontrolnike, vrednosti gesla, Unicode oznake in časovnik/odklonitev.
- [ ] Implementacija UIA na posvečenem delavcu z omejenimi zahtevami in procesno usmerjenostjo. Potencialno zavese ponudniki tečejo za obnovljivim delovno mejo; zapuščanje zahteve ne sme zapustiti neželenih učinkov.
- [ ] Izključite vrednosti gesla; poročajte o omejitvah dostopa/integriteta namesto da bi jih prečkali.
- [ ] Izvedite Windows preverjanja z izolacijskimi nastavitvami; dodajte enum vrednosti samo in preverjanje trdnosti dediščine.

### Zadeva 5: UIA picker in pregledano snemanje

**File:** Ustvarite `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; spremenite katalog ukrepov, urednik in integracijo zapisnikov; ustvarite UIA WPF smoke kontrol.

**Vmesniki: ** Picker vrne `UiElementSelector`; zapisnik vrne predlagane korake UIA in nikoli ne piše neposredno v shranjen profil. Test-selektor ukaz vrača število tekme brez povabila.

- [ ] Dodaj neuspešne preverjanja: pickers izključuje MAKRIVO in geslo-vrednost; zapis/odstranitev listov obstoječega profila se ne spremeni; dvomljiv predogled poročil se šteje; sprejemanje zapisovanih korakov ustvarja urejljiv projekt.
- [ ] Izvajanje elementov poudarek/pič, parametri, ki so specifični za delovanje, predogled izbire in pregled oken zapisnika. Upoštevajte obstoječe vedenje kratkotrajnih zapisov.
- [ ] Dodajte vnose v katalog in dostop do palete; preverite napake urejanja, preden shranite.
- [ ] Izvedite Windows in WPF preverjanje dimov na fiksni plošči; preverite slikar/redaktor zaslona.

### Zadeva 6: Izolacijski gostitelj in protokol

**datoteke:** Ustvarite `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Spreminjanje rešitev in oddajanje ukrepov.

**Vmesniki:** Version-1 manifest določa paket ID / različica, vhodna točka, parametri sheme, ukrepi, državni ponudniki in izjave o zmogljivosti. IPC sporočila vsebujejo protokolno različico, edinstveno ID zahteve, metodo, JSON parametri in tipkovano rezultat. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` vrne tipiran rezultat. dodatek `ActionKind.Plugin`.

- [ ] Dodajte neuspešne teste za nepravilno protokol, zlomljeno/preveliko sporočilo (>1 MiB), podvojeno ID zahteve, preklic, nesreča, neodgovoren proces in manjkajoče ukrepe.
- [ ] Uvedba istega uporabnika imenovano cevi IPC z 10-sekundno ročno in konfigurirano izvedbo časovnega časa, ki je zajeta na 60 sekundah. Odstranite neodgovornega delavca; po ponovnem zagonu ne ponovite samodejnega delovanja.
- [ ] Izvajanje shem in dovoljenih storitev upravljavca. Razložite izvršljivo-plugin zaupanje; procesna izolacija ni operacijska pesek.
- [ ] Izvedite jedrske teste in integracijske teste proti dejanskim procesom testiranja.

### Zadeva 7: Plugin paketov in primer integracije

**datoteke:** Ustvarite `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, test paketa; spremenite paleto/nastavitev vnosov.

**Vmesniki:** Pakiranje storitve `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` in `UninstallAsync(packageId)`. Pregled paketov zapisov hashes, odvisnosti, različice in deklarirane zmogljivosti.

- [ ] Dodaj neuspešne teste za zip traversal, absolutne poti, simlink/reparse pobeg, podvojene vnose, >100 MiB razširjene podatke, hash nepravilnosti, znižanje, pregledi in onemogočenih uvozov.
- [ ] Uvedba predogled in izrecno omogočanje, napake odvisnosti, stopnje namestitve in nadgradnje; odstranitev delavca pred odstranitvijo.
- [ ] Dostava pravi primer plugin vrača konfiguriran računanje dejanja in živega stanja prek IPC, vključno z razveljavitvijo; paket / izvoz / namestitev / oddajanje prek UI.
- [ ] Izvedite paket/host testov in WPF končne kontrole. Preverite, da ni uvoženih izvedljivih potekov pred omogočanjem.

### Task 8: HTTPS mobilna storitev in parovanje

**datoteke:** Ustvarite `src/Aivana.MobileHost/` ASP.NET Core projekt, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Preizkušnje za integracijo; sprememba rešitve in nadzornik.

**Vmesniki:** Mobilni pogovori za upravljavca prek istega uporabnika IPC. Ceste: pariranje, dovoljeno profili/akcije, pooblaščeno akcijo, preklic in status WebSocket. Vabilo vključuje edinstveno ID ukaza; upravljavec ima omejeno dedupliciranje. Potrdila so preklicljiva in shranjena prek zaščite Windows.

- [ ] Dodajte neuspešne teste za nepravilno seznam/izvedbo, izginil kodo, ponovno uporabljen kodo, preklicano token, neupravičeno poreklo, podvajanje ukaza, pretirano okvir in omejitve poplava.
- [ ] Uvedba HTTPS-samo poslušalnik, uporabnik izbrani vmesniki, lokalni certifikat zagotavljanje/uporabljanje navodila, enkrat 6-digitalne kodo, ki potekajo po 120 sekundah, največ pet poskusov na kodo in token povračilo. Shranjevanje naključnih tokenov; nikoli ne prijavite potrdil.
- [ ] Uvedba omogoča, da so navedene stabilne ID-je in obstoječe usmerjanje; zavrnitev samovoljnih shell, poti in paket-instalirajo ukaze. Nastavite izrecne omejitve stopnje na stranko (10 ukazi/sek, 30 burst) in 64 KiB sporočilo omejitev.
- [ ] Preverite preizkušnje za integracijo brez dodajanja pravil ognja ali omogočanja dostopa do LAN; preizkušanje zaprtja in token trajnosti.

### Task 9: Mobilni panel in namizni servis

**datoteke:** Ustvarite `src/Aivana.MobileHost/wwwroot/` Odgovorno ploščo in `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; dodajanje brskalnikov/mobilnih preverjanja in WPF Smoke pokritost.

**Vmesniki: ** Potrošnja naloga 8 le poti. Razkrijte dotik akcijsko omrežje, profilni piker, vrednosti / napredek v živo, razveljavitev, stanje povezave in parovanje. Desktop upravlja storitev za zagon/stop, naslov/potrdilo navodila, dovoljeno profili in povračilo strank.

- [ ] Dodaj neuspešne preverjanja za avt flow, stop / missing IDs, ponovno povezovanje brez ponovne akcije, preklic klienta in storitev ustavitev.
- [ ] Uvedba dostopnega odzivnega uporabnika z lokalnimi sredstvi, brez skriptov tretjih oseb ali internetnih odvisnosti. Token se nikoli ne prikaže v URL-jih.
- [ ] Preizkusite postavitev brskalnika na 360/768/1280 CSS px; preverite dejanske zaslone. Preverite z realnim telefonom/tabletom le, ko uporabnik izrecno omogoča storitev LAN.
- [ ] Dokumentacija in poročanje preverjanje mobilne opreme ločeno od emulacije brskalnika.

### Naloga 10: Naprave in običajno ključno opazovanje

**datoteke:** Ustvarite `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; spremenite RawKeyboardBindingService, KeyboardBinding, validacijo in diagnostiko naprave; dodajte teste.

**Vmesniki: ** `IKeyboardAdapter` izpostavlja zmogljivost snemanja, stabilno identiteto naprave in fizične ključne robove (skanje kode, razširjene zastave, tisk/izdaj). Modi razlikujejo opazovanje, mapirani standardni vnos, domači SDK in izključno z vozniki. Obstoječe virtualne ključne posebne vezave ohranijo svoje predstavitve in migracijsko vedenje.

- [ ] Dodaj neuspešne teste za stare vezave, vnesite razlike, jezikovno/razporedno neodvisnost, ponavljajoče se razpoke, odklonite med držo, dvakratna modelnost in zavrnitev vnosa.
- [ ] Razširite opazovanje na običajne fizične ključe, ne da bi jih zavirali. Dodajte izrecno ujetje in ponovno povezati identifikacijo.
- [ ] Prikaži dejanske zmogljivosti adapterja in stanje, ki ga ni mogoče preizkusiti v UI naprave; nikoli ne navedite podpore Fn/dial iz blagovnih znamk.
- [ ] Run core/Windows/WPF preverja s simuliranimi poročili in ločenim seznamom naprav.

### Task 11: Proizvajalec adapterjev

**File:** Ustvarite ločene datoteke pod `src/Aivana.Windows/KeyboardAdapters/` za LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog in splošno vodilo QmkVia; spremenite register/UI in dodajte spremembe protokola adapterja.

**Vmesniki: ** Potrošniška naloga 10 pogodbe o adapterju. Domače knjižnice naložijo le iz potrjenih lokacij aplikacije/prodajalca; odsotnost SDK je nedostopna možnost. Razsvetljava in vhodna zmogljivost so ločena.

- [ ] Preverite trenutne uradne naslovnice SDK, pogoje za redistribucijo, odvisnosti programske opreme in podprte modele pred izvajanjem vsakega adaptera; zapisovalni viri in natančne različice.
- [ ] Dodaj neuspešne teste za odsotnost/izbrisano arhitekturo SDK, dogodek tisk/izdaj/prevod, razpona, motnje odziva in nepodprto napravo. Uporabite injekcijske abstrakcije za deterministične preglede.
- [ ] izvajanje Logitech G ključ vnosa in podprta razsvetljava ločeno; Razer Chroma razsvetljava plus Synapse Standardni ključ za mapiranje čarovnika; SteelSeries GameSense Svetloba / Lighting; Wooting Analogno polnjenje z konfiguriranimi pragovi / histerezi; QMK/VIA standardna ključna nastavitev navodila. ne zahtevajte lastniških vnosov iz APIs.
- [ ] Potrdite združljive realne naprave, kjer so na voljo. Označite druge izvedene/hardware-neizkušene, z model / firmware dokaz; manjkajoče uporabne SDK je razkrita zunanja vrata.

### Task 12: Ekskluzivni tipkovnica filter

**Datoteke:** Ustvarite `src/Aivana.Windows/KeyboardFilter/`, vgrajen vir pod `tools/keyboard-filter/`, testov za integracijo in dokumentacije za namestitev. Posebno podpisani projekt je potreben, če ni ustrezne licencirane podpore prehaja oceno.

**Vmesniki: ** Backend poročila nameščen / združljiv / podpisano stanje; razkriva naprave-specifični zagon / ustavitev, ključne robove in izpuščanje v nujnih razmerah. Ločen opazovalec obnovi izgubo kontrolorja. Glavna tipkovnica nikoli ne postane izključna.

- [ ] Oceni kandidata vozniško dovoljenje, podpisano binarno izvirnost in sedanje Windows 11/Secure združljivost. Odločitev za g/no g. Vodenje voznikov v rednih testih.
- [ ] Pisanje pogodbenih testov za filtriranje cilja, neuspeh, izginotje srčnega utripa, izgubo IPC, ponovno povezovanje dvomljivosti, odmor / preklic, drža ključ sproščanja in razrešnice.
- [ ] Implement backend, izrecno izbiro/aktiviranje UI, sproščanje nujnosti in namestnik/uninstaller. Ne nadomestite nizko stopnjo hitrosti/Raw Input časovnih ur.
- [ ] Validacija v obnovljivem testiranju z dvema tipkovnicama: izbran ključ, ki je zavrnjen le na izbranem napravi; glavna pisanja ni vplivala; vnosa se je poškodovala / odložila / odstranila.
- [ ] Zavrti proizvodnje na dejanski podpisani/licencirani ozadje in fizične teste. Če zunanji podpis/licenciranje ni na voljo, je naloga nepopolna z natančnimi dokazi o odvisnosti.

### Zadeva 13: Integrirana preverjanja, prevodi in izdaje paket

**File: ** Dodaj `docs/testing/2026-10-12-six-extensions.md`; posodobite ustrezno dokumentacijo DE/EN/FR/IT, kataloge lokacije in točke za preverjanje dimov. Pridobite druge dokumentacije.

- [ ] Vključite vse vnosne točke in posnetke; dokončajte prevode z obstoječim 27 jezikovnim delovnim tokom in razkrite nepregledne prevode.
- [ ] teče `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, in `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Preverite namizne/mobilne zaslone in preverite 100/150/200 odstotkov Windows razsežnosti, konfiguracije krog potovanja, življenjski cikel plugin, omrežje preusmeritev in resnično tipkovnico ponovno povezovanje.
- [ ] Objavite v novem katalogu z `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; vključujejo spremljevalne storitve/host binarnih in licenčnih obvestil. Ne pretiravajte nameščene aplikacije.
- [ ] Poročilo zaključenih testov, strojno preizkušeni adapterji in zunanji vrati za oddajo. Vsi šest dobaviteljev morajo izpolnjevati kriterije sprejemanja, preden povabijo celotno zahtevo.

## Samodejni pregled

Vse štiri oddelke v zemljevidu za naloge 1–12; naloge 13 pokriva lokacijo, integrirano preverjanje in sproščanje. pripravljenost poročanja, izvedba serialization, konfiguracija dediščine, skrivnosti, zaupanje paketa in fizično-družinski ambiguiteto imajo izrecne lastnike in testi. zunanji pogon / SDK / strojna vrata ostanejo nepopolna, dokler ni dokazano, namesto da se šteje za uspeh programske opreme.

## Izbira izvedbe za uporabniško pregled

Priporočljivo: domača izvedba v tem klepu, naloga po nalogi, ker večina modulov deli pogodbe in trenutni delovni prostor vsebuje bistvene obstoječe urejanja. Delegirana izvedba je na voljo, če jo uporabnik izbere. Preverite ta načrt in izberite izvedbeno metodo pred izvajanjem, kot ga zahteva delovni tok pisnih načrtov.

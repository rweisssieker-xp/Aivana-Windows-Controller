<!-- generated-doc locale=hr-HR source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Proizvodnja proširenja

> **Za agentičke radnike:** ZADARAN SUB-SKILJ: Koristite supermoć:izvođenje planova za domaću izvršavanje ili supermoćima:podagentički upravlja razvoj nakon što korisnik odabere delegaciju. Slijedeći korak s kontrolnim kutijama.

**Cilj:** Dostava šest integriranih proširenja i kapaciteta multi-proizvođač tipkovnice podrška.

**Arhitektura:** Održavanje jezera / Windows / WPF Uvođenje izoliranog plugina i mobilnih usluga procesima s uskom autentificiranom IPC-om; sve izvršenje prolazi kroz postojeće kontrolore i MacroRunner quue..

**Tehnički stack:** C# i .NET 10, WPF, Windows UI automatizacija, Win32 Raw Input, Sljedeći članakDokumentirane SDK-e, ASP.NET Core HTTPS/WebSocket, Sljedeći članakSvijet.Json.

**Spec: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (potvrđeno od strane korisnika)

## Globalni ograničenja

- Postojeće konfiguracije moraju biti nepromijenjene; dodajte vrste djelovanja umjesto da naručite njihove trajne vrijednosti.
- Održavanje postojećih profila, G1/G2 zadaća, lokalnog djelovanja i korištenja bez računa.
- Raw Input Ekskluzivno uklanjanje po uređaju zahtijeva stvarni filter za tipkovnicu.
- Uvozni izvršljivi plugini ne bi trebali biti automatski izvršeni.
- Podrazumevano onemogućiti i ograničiti konfigurirane sučelje za mobilnu uslugu; nema upada autentifikacije pritužbe.
- Kratka isporuka/teks nije dokaz da je ciljna aplikacija prihvatila zapovijed.
- Nemojte zamijeniti instaliranu aplikaciju, instalirati vozače, promijeniti stvarne mape na tipkovnici ili otvoriti pristup mreži kao slučajni test.
- Održavanje neprimjerenih promjena. Ne obvezujte se na postojeće dijeljene editacije kao dio ove funkcije. Stage samo neovisno revidirane funkcije traže ako se zahtjeva.

## Pregled fokusiranja

1. Dvostruki identični modeli tipkovnice: nejasno ponovno povezivanje mora zahtijevati ponovnu selekciju (Tasks 10–12).
2. Otkaz tijekom UIA/proizvođača/plugin/network čekanja: bez odlaganja nakon otkazivanja (Task) 1, 4, 6, 8).
3. Korisnik redigira dok je čarobnjak ili paket predviđanja otvorena: revizija mora spriječiti tiho prepisivanje (Tasks 3, 7).
4. Jezik i fizički raspored se razlikuju: selektor i skan-koda ne moraju ovisiti o lokalnim imenima prikazivanja (Tasks 4, 10).
5. Povratak mreže i ponovno pokretanje plugina: zapovijedi ne moraju tiho izvršavati dva puta (Tasks 6, 8).

## Zajednički ugovori i zapovijed ovisnosti

Zahtjevi 1–3 uspostavljaju izvršenje i postavljanje. Zaposlenost 4–5 izgradnja UIA. Task 6–7 isporučuje plugine. Zadatke 8–9 pružaju mobilni pristup. Zadatke 10–12 pružaju adaptere za tipkovnicu i integraciju filtera. Task 13 potvrđuje cijeli proizvod.

Svaka zadaća slijedi neuspješan test → minimalna implementacija → testni ciklus. Zadatak je dovršen samo kada je dostupan putem prijave i njezinih provjera prolazi. Scaffolds, mock adapteri i dokumentacija sami ne zadovoljavaju isporučujuće.

### Zadatak 1: Tipirana provjera i kompatibilna upornost

**Filije:** Stvoriti `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Modifikacija `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfejs:** Proizvodnja `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` i `VerifyAsync(ActionStep, CancellationToken)`, Obojica se vraćaju `Task<ActionVerificationResult>`. Dodajte opcionalni serijal `VerificationOptions` S zastavom i vremenom (100–60000 ms), privremeno onemogućeno. objaviti tipirane rezultate po koraku, uključujući ID rada, ID tile i indeks koraka bez plaćanja akcije.

- [ ] Pisanje provjere: naslijeđe JSON zadržava postojeće brojne vrijednosti djelovanja; odsutne opcije onemogućuje provjeru; nevažeći timout ne validiraju; neuspjeh prevladava poziv izvršitelja; neuspješan postkondition nikada ne proizvodi provjereni uspjeh; nepomoćni izvršitelj je Neproverljivo; otkazivanje zaustavlja provjeru i sljedeće korake.
- [ ] Provedite `dotnet run --project tests/Aivana.Tests -c Release`; potvrdite nove provjere neuspjeh prije implementacije.
- [ ] Uvođenje ugovora, validacija, kloniranje/snapshot očuvanje i integracija MacroRunner. Poštujte OnError stop/continue politike; sačuvajte postojeće povratne informacije pretplatnika.
- [ ] Provedite osnovne testove; zahtijevaju sve provjere.

### Zadatak 2: Windows provjeravajući pružatelji i rezultati UI

**Filije:** Stvoriti `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Modifikacija `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Testna točka.

**Interfejsi: ** WindowsActionVerification implementira Task 1 pružatelj pomoću postojećih audio, prikaz, proces/prst i OBS klijenata. Preflight testiraju dostupnost i identifikacije; provjera usporediti željeni status nakon ograničenog ispitivanja. Proizvodi kontrolor rezultat snimke potrošen od WPF povijesnog pogleda.

- [ ] Dodajte neuspješne testove za nedostupne izričite audio/display ID-e, OBS timeout, nepravilno opaženo stanje, nepoznati učinci skraćenja, osjetljiva opterećenja i otkazano ispitivanje, koristeći injektivne pružatelje čitanja.
- [ ] Uvođenje realnog čitanja za provjeru za apsolutni volumen, mute, apsolutnu svjetlost, proces / prozora spremnost i podržane zapovijedi OBS. Relativne/toggle akcije zahtijevaju zarobljenu pre-stanu; inače izvješćuje Unverifiable.
- [ ] Dodajte kontrolu koraka i sigurnu povijest izvršenja koja prikazuje stanje, neuspješne zahtjeve i tekst oporavka.
- [ ] Provedite jezgre/Windows provjere i WPF pušenje s injekcijskim državnim pružateljima; nema stvarne svjetlosti ili audio pisanja.

### Zadatak 3: Kreiranje vodjenog radnog prostora

**Filije:** Stvoriti `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modifikacija palete i točaka za ulazak.

**Interfejsi: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; izbor sadrži ID predmeta, odabrane izvedive staze, URL-ove, audio/display ID-ove, OBS scenu i odabrane veze. Discovery vraća imenovane kandidate sa stabilnim ID-ovima i dostupnošću. Proizvodi konfiguracije za otkrivanje sukoba.

- [ ] Dodajte neuspješne testove za sve pet zatražene čarobnih obitelji, neovisno generirane korake, nedostaju potrebni ciljevi, otkazu bez mutacije i štedjeti protiv izmijenjenog revizije.
- [ ] Otkrijte aplikacije putem registriranih aplikacija i Start Menu kratice; uređaji kroz postojeće pružatelje; scene kroz samo OBS upitnik. Držite ručni izbor dostupan za nedostajuće aplikacije.
- [ ] Implement Izaberite tok rada → Izaberite ciljeve → Izaberete kontrolu → Pregled/validacija → Sačuvajte. Stvorite uređujuće profile i nikada ne radite tijekom postavljanja.
- [ ] Dodajte stvarne WPF čarobni dima provjere za završetak / otkazivanje / sukob; provodite sve testove.

### Zadatak 4: UIA selektor i ograničene akcije

**Filije:** Stvoriti `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modifikacija modela, validacija konfiguracije i WinActionExecutor.

**Interfejs:** Dodatno `ActionKind.UiAutomation`; `UiElementSelector` Procesi trgovine, automatizacija ID, kontrolni tip i naručeni kriteriji prednika. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Povratak `Task`; operacije Invoke, SetValue, Select, Toggle, Focus i WaitState..

- [ ] Dodajte neuspješne testove protiv posvećenog prozornog prozora za premještanje/odbijanje, duplikiranje utakmica, nestale elemente, nepomoćne uzorke, isključene kontrolne vrijednosti, Unicode oznake i timeout/anulacije.
- [ ] Uvođenje UIA-e na posvećenog radnika s ograničenim zahtjevima i ciljanjem procesa. Potencijalno viski pružatelji trče iza obnovljivih radnih granica; odustajanje od zahtjeva ne smije ostaviti oštećene nuspojave.
- [ ] Izbjegavajte vrijednosti lozinke; prijavite ograničenja pristupa/integriteta umjesto da ih prekoračite.
- [ ] Provedite Windows provjere pomoću izoliranih fiksiranja; dodajte samo enum vrijednosti i rerun provjere usklađenosti.

### Zadatak 5: UIA picker i preispitivanje snimke

**File: ** Stvorite `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modify katalog djelovanja, urednik i integracija snimaka; stvaranje UIA WPF dima provjere.

**Interfejsi: ** Picker vraća `UiElementSelector`; zapisnik vraća predložene korake UIA i nikada ne piše izravno u sačuvani profil. Test-selektor zapovijed vraća broj utakmica bez poziva.

- [ ] Dodajte neuspješne provjere: piker isključuje MAKRIVO i lozinku vrijednost uhvaćanja; zapis / otkaz stranice postojećeg profila nepromijenjene; nejasni pregleda izvješća u skladu s brojem; prihvaćanje zabilježene korake stvara urediv projekt.
- [ ] Uvođenje elementa naglasak / slikar, parametri specifični za rad, predviđanje selektor i prozor snimke. Održavanje postojećeg ponašanja kratkog snimka.
- [ ] Dodajte ulazke u katalog i paletu; potvrdite pogreške urednika prije čuvanja.
- [ ] Provedite Windows i WPF kontrolu dima na fiksiranju; provjerite snimke slikara/redaktora.

### Zadatak 6: Izolacijski hosting i protokol

**Filije:** Stvoriti `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Modifikacija rješenja i djelovanja.

**Interfejs:** Version-1 manifesti određuje ID paketa/versiju, točku ulaska, parametre sheme, akcije, državni pružatelji i izjave o mogućnostima. IPC poruke sadrže protokol verziju, jedinstvenu ID zahtjeva, metodu, JSON parametre i tipiran rezultat. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Povratak rezultata - Dodatno `ActionKind.Plugin`.

- [ ] Dodajte neuspješne testove za neusklađen protokol, poruku (>1 MiB), duplicirani ID zahtjeva, otkazivanje, nesreća, neodgovarajući proces i nedostajuće djelovanje.
- [ ] Uvođenje istog korisnika nazvanog IPC-a s 10 sekundi rukometa i konfiguriranim vremenom izvršenja u 60 sekundi. Završetak neodgovarajućeg radnika; nemojte automatski ponovno igrati akciju u letu nakon ponovnog pokretanja.
- [ ] Izvođenje shema i dopuštenih usluga kontrolora. Objasnite povjerenje izvršljivog plugina; procesna izolacija nije OS sandbox.
- [ ] Provedite testove i testove integracije protiv stvarnih procesa testiranja.

### Zadatak 7: Plugin paketi i primjer integracije

**Filije:** Stvoriti `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, test paketa; izmijeniti paletu/uredi.

**Interfejs:** Pakiranje izložbe `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` i `UninstallAsync(packageId)`. Pregled paketa zapisa hashes, ovisnosti, verzije i proglašene sposobnosti.

- [ ] Dodajte neuspješne testove za zip traversal, apsolutne staze, simlink/reparse escape, duplicate entries, >100 MiB prošireni podaci, hash mršavljenje, smanjivanje, revizija stale i onemogućiti uvoz.
- [ ] Uvođenje predviđanja i izričito omogućavanje, ovisnosti o pogreškama, stajališta instalacije i nadogradnje; uklanjanje radnika prije uklanjanja.
- [ ] Dostavi pravi primjer plugin vraćajući konfiguriranu akciju računanja i živog stanja putem IPC-a, uključujući otkazivanje; pakiranje / izvoz / instalacija / uklanjanje putem UI-a.
- [ ] Provedite testove paketa/host i WPF završne provjere. Provjerite da uvozni izvršljivi tečajevi nisu uvezeni prije odobrenja.

### Task 8: HTTPS mobilni servis i pariranje

**Filije:** Stvoriti `src/Aivana.MobileHost/` ASP.NET osnovni projekt, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Test integracije; modifikacija rješenja i kontrolor.

**Interfejsi: ** Mobilni servis razgovara s kontrolorom putem istog korisnika IPC. Putovi: pariranje, dopušteni profili/akcije, autentificirani poziv na akciju, otkazivanje i status WebSocket. Invokacija uključuje jedinstvenu ID zapovijedi; kontrolor posjeduje ograničenu deduplaciju. Potvrde se mogu povući i pohraniti putem zaštite Windows.

- [ ] Dodajte neuspješne testove za neprimjerenu popis/izvođenje, istekli kod, ponovno korišteno kod, otkazani token, nevažeći izvor, duplicirani zapovijed, prekomjerni okvir i ograničenja poplava.
- [ ] Uvođenje HTTPS-samo slušalica, korisnički odabranih sučelja, lokalnih certifikata/povjerenja upute, jednom 6-digit par kodovi isteka nakon 120 sekundi, maksimalno pet pokušaja po kodu i token povlačenje. Skladište slučajnih tokenova; nikada se ne prijavljuju potvrde.
- [ ] Uvođenje omogućuje stabilne akcijske ID-e i postojeće usmjeravanje; odbijanje arbitrarnih shell, staze i paketa-instalirati zapovijedi. Konfigurirajte izričito ograničenje stope po klijentu (10 zapovijedi/sek, 30 burst) i 64 KiB poruka ograničenje.
- [ ] Provjerite testove integracije bez dodavanja pravila vatrogasne zidine ili omogućavanja pristupa LAN-u; testiranje zatvaranja i token trajnosti.

### Zadatak 9: Mobilni panel i stolni servis

**Filije:** Stvoriti `src/Aivana.MobileHost/wwwroot/` Odgovarajući panel i `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Dodajte browsere/mobilne verifikacijske skriptove i WPF Pokrivanje dima.

**Interfejsi: ** Potrošnja zadaća 8 putova samo. Izložite mrežu za dodir, profilni piker, živu vrijednost/progres, otkazivanje, stanje povezivanja i pariranje. Desktop kontrolira službu start/stop, adresu/certificiranje upute, dopušteno profila i povlačenje klijenta.

- [ ] Dodajte neuspješne provjere integracije za auth tok, stale/missing akcijske ID-e, ponovno povezivanje bez ponovnog preuzimanja akcije, povlačenog klijenta i zaustavljanja usluge.
- [ ] Uvođenje pristupačnog odgovarajućeg korisnika pomoću lokalnih imovine, bez skriptova trećih strana ili internetskih ovisnosti. Tokeni se nikada ne pojavljuju u URL-ovima.
- [ ] Testiranje rasporeda pretraživača na 360/768/1280 CSS px; provjerite stvarne snimke zaslona. Provjerite sa stvarnim telefonom/tabletom tek nakon što korisnik izričito omogućuje LAN uslugu.
- [ ] Dokumentacija i izvješće o mobilnom hardveru odvojeno od emulacije pretraživača.

### Zadatak 10: kapaciteti uređaja i obična ključna opažanja

**Filije:** Stvoriti `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; izmjena RawKeyboardBindingService, KeyboardBinding, validator i dijagnoza uređaja; dodajte testove.

**Interfejsi: ** `IKeyboardAdapter` prikazuje snimke kapaciteta, stabilnu identitet uređaja i fizičke ključne rande (skanje koda, proširene zastave, tiskanje/izlazak). Modovi razlikuju promatranje, mapirani standardni ulaz, native SDK i vozač-ekskluzivni. Postojeći virtualni ključni posebni vezovi zadržavaju svoje predstavljanje i migracijsko ponašanje.

- [ ] Dodajte neuspješne testove za stare veze, Enter razlike, jezik / raspored neovisnosti, ponavljajuće padove, unplug dok se drži, duplikat-modela nesigurnost i injekcije-odbijanje ulaza.
- [ ] Proširite promatranje na obične fizičke ključeve bez suzbijanja. Dodajte izričito uhvaćanje i ponovno povezati identifikacijsku obradu.
- [ ] Prikazuje stvarne kapacitete adaptera i hardverski status u UI uređaju; nikada ne upućuje podršku Fn/dial iz imena marke.
- [ ] Run core/Windows/WPF provjerava s simuliranim izvješćima i čitanjem uređaja.

### Task 11: Proizvođač adaptera

**File:** Stvorite odvojene datoteke pod `src/Aivana.Windows/KeyboardAdapters/` za LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog i generic QmkVia smjernice; izmijenite registar/UI i dodajte protokole provjere adaptera.

**Interfejsi: ** Potrošnja zadaća 10 ugovori o adapteru. Domaće knjižnice se nalaze samo s validiranih lokacija aplikacije/prodavatelja; odsutnost SDK-a je nedostupna mogućnost. Osvijetljenje i ulazne kapacitete su odvojene.

- [ ] Provjerite trenutačne službene naslovnice SDK-a, uvjete redistribucije, ovisnosti o softveru i podržane modele prije implementacije svakog adaptera; snimke izvora i točne verzije.
- [ ] Dodajte neuspješne testove za SDK odsutnost / otpad arhitekture, događaj pritisak / ponavljanje, isključivanje, poremećaj odgovora i nepomoćna uređaj. Koristite injektivne abstrakcije za determinističke provjere.
- [ ] Uvođenje Logitech G ključ ulaz i podupiranje svjetla odvojeno; Razer Krom svjetla plus Synapse Standardni ključ za čarobnjak; SteelSeries GameSense Svijet / svjetla; Wooting Analogno ispitivanje s konfiguracijskim ograničenjima / histerezom; QMK/VIA standardna ključna smjernica za postavljanje. ne zahtijevajte vlastiti ulaz od API-a.
- [ ] Validirajte kompatibilne stvarne uređaje gdje su dostupne. Označite druge implementirane/hardware-neizvještene, s modelom/firmware dokaz; nedostaje korisni SDK je otkrivena vanjska vrata.

### Task 12: Ekskluzivni filter za tipkovnicu

**File: ** Stvorite `src/Aivana.Windows/KeyboardFilter/`, izvor instaliranja pod `tools/keyboard-filter/`, testovi integracije i dokumentacija za implementaciju. Potrebno je posvećeno potpisano vozilo ako ne prođe ocjenjivanje odgovarajućeg licenciranog podruma.

**Interfejsi: ** Backend izvješća instaliran/kompatibilan/podnesen stanje; prikazuje uređaj-specifičan početak/stop, ključne rupe i izlazak u hitnu situaciju. Odvojen nadzornik vraća gubitak kontrolora. Glavna tipkovnica nikada ne postaje isključiva.

- [ ] Procijenite kandidate vozačke dozvole, potpisani binarni izvor i trenutačna kompatibilnost Windows 11/Secure. Odluka o odlasku / odlasku. Nema vozača tijekom redovnih ispitivanja.
- [ ] Napisati testove ugovora za odabir cilja filtera, neuspjeh, istek srčanog udara, gubitak IPC-a, ponovno povezivanje, pauza/odbijanje, zadržavanje ključa i uklanjanje.
- [ ] Uvođenje backend, izričito odabir/aktiviranje UI, hitno oslobađanje i instalator/uninstaller. Nemojte zamijeniti nizku razinu Raw Input vremenske heuristike.
- [ ] Validirati u oporavljivom testiranju s dvije tipkovnice: odabrane ključeve isključivo na odabranom uređaju; glavni tip nije utjecao; crash/suspend/uninstall vratiti ulaz.
- [ ] Završetak proizvodnje vrata na stvarnom potpisanom/licenciranom pozadini i fizičkim testovima. Ako je vanjski potpis/licencija nedostupna, zadatak se čuva nepotpuno s točnim dokazima o ovisnosti.

### Zadatak 13: Integracijski paket provjere, prijevoda i izdanja

**File: ** Dodajte `docs/testing/2026-10-12-six-extensions.md`; ažurirati relevantnu dokumentaciju DE/EN/FR/IT, kataloge lokacije i točke ulaza za kontrolu dima. Održavajte druge dokumentacije.

- [ ] Integrirajte sve točke ulaska i snimke; završite prijevode pomoću postojećeg kataloga jezika 27 i otkrijte neprogledane prijevode.
- [ ] Trčanje `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, i `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Provjerite desktop/mobile snimke i provjerite 100/150/200 procenta Windows skala, konfiguracije putovanja, životni ciklus plugina, mrežnu revokaciju i stvarno ponovno povezivanje tipkovnice.
- [ ] Objavljujte u novom katalogu s `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; uključuju usluge partnera/host binarije i licencijske obavijesti. Nemojte prepisati instaliranu aplikaciju.
- [ ] Izvješće je završio testove, hardver-testirane adaptere i vanjske vrata izdanja. Sve šest isporučitelja mora ispuniti kriterije prihvaćanja prije nego što se pozove cijeli zahtjev.

## Samostalni pregled

Sve šest odjeljaka spe za zadatke 1–12; Zadatak 13 pokriva lokaciju, integriranu provjeru i oslobađanje. izvješćivanje o spremnosti, serializiranje izvršenja, konfiguracija naslijeđa, tajne, povjerenje paketa i fizička nesigurnost uređaja imaju izričito vlasnike i testove. vanjski vozač/SDK/hardware vrata ostaju nepotpuna sve dok se ne dokaže, umjesto da se računaju kao uspjeh softvera.

## Izbor izvršenja za pregled korisnika

Preporučuje se: domaća izvedba u ovom razgovoru, zadatak po zadatku, jer većina modula dijeli ugovore i trenutni radni prostor sadrži značajne postojeće editacije. Delegirana provedba je dostupna ako ga korisnik odabere. Pregledajte ovaj plan i odaberite metodu izvršenja prije provedbe, kao što zahtijeva radni protok pisanja planova.

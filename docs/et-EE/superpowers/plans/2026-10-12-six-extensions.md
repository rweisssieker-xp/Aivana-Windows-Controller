<!-- generated-doc locale=et-EE source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Rakenduskava laiendamine

> **Agentside töötajate jaoks: ** nõutav sub-oskuste kasutamine: kohalike täitmise rakenduskavad või supervõimud: subagentide juhtimine pärast kasutaja valimist delegatsiooni. Järgige sammud kontrollikodega.

**Eesmärk: ** Toodab kuus integreeritud laiendust ja võimsuse põhjal mitme tootja klaviatuuri tugi.

**Arhitektuur:** Säilitage tuum / Windows / WPF eraldamine. tutvustada isoleeritud plugin ja mobiilside protsessid tihe autentimine IPC; kõik rakendused läheb läbi olemasoleva juhendaja ja MacroRunner kuju..

**Tehniline stack:** C# ja .NET 10, WPF, Windows UI automatiseerimine, Win32 Raw Input, Dokumenteeritud müüja SDK, ASP.NET Core HTTPS/WebSocket, Kirjandus.Json.

**Spec: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (kasutaja poolt heakskiidetud)

## Ülemaailmsed piirangud

- Olemasolev konfiguratsioon peab laadima muutumatult; lisage tegevusviisid selle asemel, et reordida nende püsivaid väärtusi.
- Säilitage olemasolevad profiilid, G1/G2 ülesanded, kohalik toimimine ja kontoteta kasutamine.
- Raw Input Eraldi seadme eemaldamine nõuab tegelikku klaviatuuri filtri tausta.
- Importimine ei tohi automaatselt toimuda.
- Mobiilteenuse konfigureeritud vahendite eelarve ja piiramine; puudub kaebuste autentimine.
- Lühidalt/teksti tarnimine ei ole tõend, et sihipärane rakendus on käsu vastu võtnud.
- Ärge asendage installitud rakendust, paigaldage juhid, muutke tegelikku klaviatuuri või avage võrgu juurdepääs juhuslikuks katseks.
- Hoidke sellega seotud mittekuuluvaid muudatusi. Ärge pühenduge olemasolevatele jagatud koodidele selle funktsiooni osana. Stage iseseisvalt läbivaatavad funktsioonid ainult siis, kui nõutakse kohustusi.

## Ülevaade Focus

1. Duplitsed identse klaviatuuri mudelid: kahtlemata taasühendamine peab nõudma uuesti valimist (Tasks 10–12).
2. Tühistamine UIA / pakkuja / plugin / võrgu oote ajal: tühistamise järel ei ole tagasilükkamist (Task 1, 4, 6, 8).
3. Kasutaja redigeerib, kui võlur või pakendi eelvaade on avatud: läbivaatamine peab vältima vaikset üle kirjutamist (Tasks 3, 7).
4. Keel ja füüsiline paigutus erinevad: valijad ja skaneerimiskodi sidemed ei pea sõltuvust lokaliseeritud ekraani nimedest (Tasks 4, 10).
5. Võrgu retry ja plugin taaskäivitamine: käsk ei tohi vaikimisi täita kaks korda (Tasks 6, 8).

## Ühislepingud ja sõltuvusord

Ülesanded 1–3 kehtestavad rakenduse ja seadistamise. Üldkohtad 4–5 ehitada UIA. Töö 6–7 pakkuda plugins. Ülesanded 8–9 pakkuda mobiilset juurdepääsu. Task 10–12 annab klaviatuuri adapterid ja filtri integreerimine. Task 13 kinnitab kogu toote.

Iga ülesanne järgneb ebaõnnestumiseks → minimaalne rakendamine → läbiviimiseks. Üks ülesanne on täidetud ainult siis, kui see on taotluse ja selle kontrollide kaudu kättesaadav. Scaffoldid, mooki adapterid ja dokumendid üksi ei rahulda kättesaadav.

### Task 1: tüübikinnitus ja kokkusobiv püsivus

**Fileduure:** Looge `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Muudatused `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaadid:** Tootmine `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` ja `VerifyAsync(ActionStep, CancellationToken)`, Mõlemad tagasipöördumine `Task<ActionVerificationResult>`. Lisage valikuvõimalus `VerificationOptions` Vabariigi lipu ja aja väljalülitamine (100–60000 ms), varakult välja lülitatud. avaldada trükitud sammutulemused, sealhulgas käivitamise ID, tile ID ja sammindeks ilma tegevusmaksete.

- [ ] Kirjelduskontrolli: pärand JSON säilitab olemasolevaid tegevuse numbrilisi väärtusi; puuduvad võimalused kinnitust keelata; ebaõnnestunud aegumine takistab täitev kutse; ebaühtlane postkindlus ei tooda kunagi tõendatud edu; toetamata täitev on kontrollimatu; tühistamine peatab kinnitust ja järgnevad sammud.
- [ ] Juurdepääs `dotnet run --project tests/Aivana.Tests -c Release`; kinnitab, et uued kontrollid ebaõnnestuvad enne rakendamist.
- [ ] Rakenduslepingud, valideerimine, kloonimise/snapshot säilitamine ja MacroRunner integreerimine. Järgige OnError-poliitikat ja säilitage olemasolevad tagasisidet.
- [ ] Juurdepääs põhikatsetele; kõik kontrollid on vajalikud.

### Task 2: Windows kontrolliteenuse pakkujad ja tulemused UI

**Fileduure:** Looge `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Muudatused `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Test sissepääsu punktid.

**Vahendid: ** WindowsActionVerification rakendab ülesande 1 pakkuja, kasutades olemasolevaid audio, ekraan, protsess / akna ja OBS kliente. Preflight testid kättesaadavus ja identifitseerivad; kontrollimine võrdleb soovitud seisund pärast piiratud uuring. Toode kontrolli tulemused snapshots tarbitakse ajalugu vaate WPF.

- [ ] Lisage ebaõnnestunud testid kättesaamatuteks sõnaselgete audio/display ID-de, OBS aja väljalülitamiseks, valesti täheldatud seisundi, tundmatu lühendamise efektide, tundliku maksekäitumise ja tühistatud uurimise jaoks, kasutades süstidavaid loenguid.
- [ ] Rakendage reaalset lugemisega kinnitust absoluutse mahtu, müütuse, absoluutne heledus, protsessi/window valmisoleku ja toetatud OBS käskude jaoks. Relatiivsed/toggle meetmed nõuavad vangistatud eeltingimust; muidu aruanded On kontrollimatu.
- [ ] Lisage samm-sammult kontrollimise ja ohutu rakendamise ajalugu, mis näitab seisundit, ebaõnnestunud nõuet ja taastamise teksti.
- [ ] Run core/Windows kontrollid ja WPF suits süstetud riigi pakkujad; ei ole reaalne heledus või audio kirjutamine.

### Task 3: juhitud tööruumi loomine

**Fileduure:** Looge `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; muudetud palett ja dashboard sissepääsu punktid.

**Interfaadid:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; valik sisaldab templite ID, valitud rakendatavate teede, URL-d, audio/display IDs, OBS Scene ja valitud sidemed. Discovery tagastab nimetatud kandidaadid stabiilse ID ja kättesaadavus..

- [ ] Lisage ebaõnnestunud katsed kõigile viisele nõutud võlurite peredele, sõltumatute sammudega, nõutavate eesmärkide puudumine, mutatsioonideta tühistamine ja muudetud läbivaatamise eest säästmine.
- [ ] Avastage rakendusi registreeritud rakenduste teed ja Start menüü lühendid; seadmed olemasolevate pakkujate kaudu; stseenid läbi lugemise ainult OBS küsimus. Hoidke manuaalne valik avastamata rakenduste jaoks kättesaadav.
- [ ] Rakendamine Valige töövool → Valige sihtmärgid → Valige kontrollid → Valideerige → Save. Looge redigeeritavad profiilid ja ei tööta seadistamise ajal.
- [ ] Lisage tegelikud WPF võlurivahendid lõpetamiseks / tühistamiseks / konflikti; teed kõik testid.

### Task 4: UIA valikud ja piiratud tegevused

**Fileduure:** Looge `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; muudetud mudelid, konfigureerimine ja WinActionExecutor.

**Interfaadid:** Appendiks `ActionKind.UiAutomation`; `UiElementSelector` kauplusprotsess, automaatne ID, kontrolltüüp ja tellitud esivanem kriteeriumid. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Tagasi `Task`; funktsioonid Invoke, SetValue, Select, Toggle, Focus ja WaitState..

- [ ] Lisage ebaõnnestunud testid pühendatud katse akna ümberpaigutamise/tähelepanu, kahekordse mänge, puuduvad elemendid, toetamata mustrid, keelatud kontrollid, parooli väärtused, Unicode märgistused ja timeout / tühistamine.
- [ ] UIA rakendamine pühendatud töötaja suhtes, millel on piiratud taotlused ja protsesside siirdamine. Potentsiaalselt tõmmatud teenusepakkujad jooksvad tagasiulatuva töötajate piiri taga; taotluse loobumine ei tohi jätta kahjustatud kõrvaltoimeid.
- [ ] Vältige parooli väärtused; teatage juurdepääsu/integriteedi piirangud, mitte neid ületada.
- [ ] Käivitage Windows kontrollid, kasutades eraldatud kinnitused; lisage ainult enum väärtused ja rerun pärandi püsivust kontrollid.

### Task 5: UIA pikki ja ülevaadetud salvestamine

**failid: ** Luua `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; muudetakse tegevuskataloogi, redigeeri ja salvestuse integreerimist; luua UIA WPF suitsukontrolli.

**Vahendid: ** Picker tagastab `UiElementSelector`; rekordöör tagastab soovitatud UIA sammud ja ei kirjuta kunagi otse salvestatud profiili. Test-selektor käsk tagastab mängu ilma kutsumiseta.

- [ ] Lisage ebaõnnestunud kontrollid: pikker välistab MAKRIVO ja parooli väärtuse kinnipidamine; registreerimine / tühistamine lehed olemasoleva profiili muutmata; kahtlane eelvaate aruanded vastav arv; vastuvõtmine salvestatud sammud luuakse redigeeritav projekt.
- [ ] Rakendus elemendi rõhuasetus / pilt, operatsioonipõhised parameetrid, valikuväljavaade ja salvestamise läbivaatamise aken. Hoidke olemasolevat lühikese salvestamise käitumist.
- [ ] Lisage kataloogi sissemakseid ja juurdepääsu palettile; enne salvestamist valideerige redaktori vigu.
- [ ] Juurdepääs Windows ja WPF suitsukontrolli fikseerimisel; inspekteerige pikki/editor ekraanilõikmeid.

### Task 6: Isoleeritud plugin host ja protokoll

**Fileduure:** Looge `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Muudatused ja tegevus.

**Interfaadid:** Version-1 manifest määrab pakendi ID/versioon, sisendpunkti, parameetrite skeemid, meetmed, riigi pakkujad ja võimsuse deklaratsioonid. IPC sõnumid sisaldavad protokolli versiooni, unikaalse taotluse ID, meetod, JSON parameetrid ja tüüpitud tulemus. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Tagastage täheldatud tulemus.Apsend `ActionKind.Plugin`.

- [ ] Lisage ebaõnnestunud testid ebaõiglase protokollide, ebaõige / ületatud (>1 MiB) sõnumi, duplikat taotluse ID, tühistamine, õnnetus, vastamata protsess ja puuduvad meetmed.
- [ ] Rakendamine sama kasutaja nimega IPC-ga 10 sekundi käepide ja konfigureeritava täitmise ajakava, mis on kaetud 60 sekundi jooksul. Lõpetage vastutustundetu töötaja; ärge automaatselt uuesti mängige lennu toimingut pärast taaskäivitamist.
- [ ] Rakendada skeeme ja lubatud juhendajate teenuseid. Selgitage rakendatav plugini usaldust; protsessi isolatsioon ei ole operatsioonisüsteemi liivakasti.
- [ ] Juurdepääs põhikatsetele ja integreerimiskatsetele reaalse testiplugini protsesside vastu.

### Task 7: Plugin pakendid ja näide integreerimine

**Fileduure:** Looge `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, pakendi testid; modifitseerige paletit / sätete sissepääsu.

**Interfaadid:** Pakenditeenindus `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` ja `UninstallAsync(packageId)`. Revideeritud paketi salvestused, sõltuvused, versioon ja deklareeritud võimeid.

- [ ] Lisage ebaõnnestunud katsed zip traversal, absoluutsed teed, simlink/reparse väljumine, duplikate sissepääs, >100 MiB laiendatud andmed, hash mismatch, downgrade, stale läbivaatamine ja puudulik import.
- [ ] Rakendamise eelvaade ja selgelt lubatud, sõltuvuse vigu, staadiumide paigaldamine ja uuendamine; töötaja eemaldamine enne eemaldamist.
- [ ] Tõeline näide plugin tagastab konfigureeritava arvutamise tegevuse ja elava seisundi kaudu IPC, sealhulgas tühistamine; pakend / eksportimine / installimine / kustutamine läbi UI.
- [ ] Käivitage pakett / host testid ja WPF lõpp-lõppude kontrollimine. Kontrollige, et enne lubamist ei ole imporditud rakendust.

### Task 8: HTTPS mobiiltelefoni teenus ja paari

**Fileduure:** Looge `src/Aivana.MobileHost/` ASP.NET põhiprojekti, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, integratsioonitestid; modifitseerimisküsimused ja kontroll.

**Sõrmed: ** mobiiltelefonide teenuse vestlused kontrolli kaudu sama kasutaja IPC. Routes: paarimine, lubatud profiilid / tegevused, autentimatud tegevus, tühistamine ja WebSocket staatuse. Kutsumine hõlmab ainulaadset juhendi ID-d; kontrollija omab piiratud deduplikatsiooni. Tunnistused on tagasi võetavad ja salvestatud Windows kaitse kaudu.

- [ ] Lisage ebaõnnestunud testid ebaühtlase loendamise/täitmise, aegunud koodi, uuesti kasutatud koodi, tühistatud token, ebaõiglane päritolu, duplikatseeritud käsk, ülekaalulised raamistikud ja üleujutused.
- [ ] Rakendamine HTTPS-ainult kuulaja, kasutaja valitud liidesed, kohalikud sertifikaadid / usalduse juhised, üheaegne 6 numbriline paari koodid pärast 120 sekundit, maksimaalselt viis katset koodi ja token tagasi. Säilitage juhuslikud tokenid; mitte kunagi registreeruda.
- [ ] Rakendamine võimaldab loetletud stabiilse tegevuse ID-d ja olemasolevat reisi; keeldub tahtlikuks shell, marsruudid ja pakendi paigaldamise juhised. Määrake selgelt kliendi taseme piirid (10 käsk / sekund, 30 tõus) ja 64 KiB sõnumi piir.
- [ ] Kontrollige loopback integreerimise testid ilma tuletõrje reeglite lisamiseta või lubades LAN-i juurdepääsu; test sulgemine ja token püsivus.

### Task 9: Mobiilne paneel ja töölaua teenuse juhtimine

**Fileduure:** Looge `src/Aivana.MobileHost/wwwroot/` Reageeriv paneel ja `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; lisada brauseri/mobiilse kontrollimise skriptid ja WPF Suitsukindlus.

**Vahendid: ** tarbimine ülesanded 8 ainult teed. Näita puudutusmeetmete võrgustik, profiili pikkija, elavad väärtused / progressi, tühistamine, ühenduse seisund ja paarierimine. Desktop kontrollib teenuse algust / peatamist, aadressi / sertifitseerimise juhiseid, lubatud profiile ja kliendi tagasivõtmist.

- [ ] Lisage ebaõnnestunud integreerimise kontrollid auth-voolu, jäämise/lõikamise tegevuse ID-d, taasühendamine ilma tegevuse uuesti mängimiseta, tühistatud kliend ja teenuse peatamine.
- [ ] Rakendamine juurdepääsetav vastupidav kasutaja, kasutades kohalikke varasid, mitte kolmandate osapoolte skripte või interneti sõltuvusi. Tokenid ei ilmu kunagi URL-desse.
- [ ] Test brauseri paigaldamine 360/768/1280 CSS px; kontrollida tegelikku ekraani. Kontrollige reaalset telefoni/tabletti ainult siis, kui kasutaja selgelt lubab LAN-teenust.
- [ ] Dokumendi ja aruande mobiilsete seadmete kontrollimine eraldi brauseri emulatsioonist.

### Task 10: seadme võimed ja tavaline võtme vaatamine

**Fileduure:** Looge `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; muudab RawKeyboardBindingService, KeyboardBinding, validator ja seadme diagnoos; lisage testid.

**Interfaadid:** `IKeyboardAdapter` näitab võime pildid, stabiilne seadme identiteet ja füüsilised võtmed (skaneerimiskode, laiendatud lipud, press / vabastamine). moodid eristavad vaatlemist, kaardistatud standard sissepääsu, kodumaise SDK ja driver-exclusive. olemasolevad virtuaalse võtme spetsiaalsed sidemed säilitavad oma esinduse ja rännaku käitumist.

- [ ] Lisage ebaõnnestunud testid vanade sidemete, Enter eristused, keele / paigutuse sõltumatus, korduvad allahindlused, unplug ajal hoida, duplikat-mudeli ebamugavust ja süsineku tagasilükkamine.
- [ ] Laiendage tähelepanu tavalistele füüsilistele võtmetele ilma nende pärssimiseta. Lisage selge kinnipidamine ja taasühendage identiteedi käitlemine.
- [ ] Näita seadme UI-s tegelikku adapterite võimekust ja hardware-testimatu seisundit; mitte kunagi Fn/dial tugi brändi nimedest.
- [ ] Run core/Windows/WPF kontrollib simuleeritud aruandeid ja loetavate seadmete loend.

### Task 11: Tootja adapterid

**failid: ** Looge eraldi faile all `src/Aivana.Windows/KeyboardAdapters/` LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog ja üldine QmkVia juhend; muuda registri/UI ja lisada adapter protokoll kontrolli.

**Sõrmed: ** tarbimise ülesanded 10 adapteri lepingud. Kodumaised raamatukogud laadivad ainult valideeritud rakenduse/müügikohtadest; SDK puudumine on kättesaadav. Valguse ja sisendvõime on eraldi.

- [ ] Kontrollige praeguseid ametlikke SDK-i pealkirju, ümberjaotustingimusi, tarkvara sõltuvusi ja toetatud mudeleid enne iga adapteri rakendamist; salvestatakse allikad ja täpsed versioonid.
- [ ] Lisage ebaõnnestunud testid SDK puuduvate / puuduvate arhitektuuride, sündmuste press / vabastamine / kordamine, ühenduseta, halva vastuse ja toetamata seadme jaoks. Kasutage indiviidseid abstrakte deterministlikuks kontrollimiseks.
- [ ] Rakendamine Logitech G-tähelepanu ja toetatav valgustus eraldi; Razer Chroma valgustus Synapse Standard-tõmme kaardistamine; SteelSeries GameSense Valgustamine / Lighting; Wooting Analoogne küsitlus konfiguratiivsete piirmääradega / hüsteeridega; QMK/VIA Standardse võtme seadistamise juhend. Ära nõua oma sisendit valgustuse API-dest.
- [ ] Valideerige sobivad reaalsed seadmed, kui need on saadaval. Märkige teised rakendatud/hardware-testi, mudeli/firmware tõendid; puudub kasutatav SDK on avalikult välisport.

### Task 12: Eksklusiivne klaviatuuri filtreerimine

**failid: ** Luua `src/Aivana.Windows/KeyboardFilter/`, installeri alusel `tools/keyboard-filter/`, tagasiside integreerimise testid ja käivitamise dokumendid. Eesmärgitud allkirjastatud juhtimise projekt on vajalik, kui ei ole sobivat litsentseeritud tagasisidet läbi hindamise.

**Vahendid: ** Backend aruanded paigaldatud / ühilduv / allkirjastatud seisund; näitab seade-spetsiifilisi algust / peatust, võtmepiirkondi ja hädaolukorra vabastamist. Eraldi vaatleja taastab kontrolli kaotuse läbi. Peamine klaviatuur ei muutu kunagi privileegiliseks.

- [ ] Hindage kandidaadi juhiluba, allkirjastatud binaarne päritolu ja praegune Windows 11/Secure Boot ühilduvus. Otsus ei tule / ei tule. Sõiduk ei ole tavaliste katsete ajal paigaldatud.
- [ ] Kirjuta lepingu testide jaoks filtri eesmärgi valik, ebaõnnestumine, südame lööve lõppu, IPC kaotamine, uuesti ühendatud ebamugavus, paus / tühistamine, võtme vabastamine ja kõrvaldamine.
- [ ] Rakendamise tagasiside, selge valik / aktiveerimine UI, hädaabikandja ja installeri / uninstaller. Ärge asendage madala taseme heuristidRaw Input.
- [ ] Valideerige taastatava testimasinas kahe klaviatuuri abil: valitud võtmed on välja lülitatud ainult valitud seadmes; peamine trükkimine puudub; sissetung / katkestamine / installimata sissetulek.
- [ ] Gate tootmise lõpetamine tegelikul allkirjastatud / litsentseeritud tagasiside ja füüsiliste katsete. Kui väline allkirjastamine/litsentsimine ei ole kättesaadav, hoiage ülesanne ebaõiglaseks ja täpsed sõltuvuse tõendid.

### Task 13: integreeritud kontrollimine, tõlked ja vabastamine pakett

**failid: ** Lisada `docs/testing/2026-10-12-six-extensions.md`; ajakohastada asjakohast DE/EN/FR/IT dokumendid, asukoha kataloogid ja suitsu kontrollpunktid. Hoidke teised dokumendid.

- [ ] Integreerige kõik sisendpunktid ja pildid; täiendage tõlkeid olemasoleva 27 keele kataloogi töövoogu abil ja avalikustage läbivaatamata tõlked.
- [ ] jookseb `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, ja `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Kontrollige töölaua/mobiilse ekraani ja kontrollige 100/150/200 protsenti Windows skaal, konfiguratsioonid, plugin elutsükkel, võrgustiku ümberlõikamine ja tegelik klaviatuuri taasühendamine.
- [ ] Publish in a new artifact directory with `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; hõlmab kaasteenuse / host binaarid ja litsentsi teated. Ärge liialdage installitud rakendust.
- [ ] Raport lõpetatud testid, hardware-testamata adapterid ja välised vabastamise uksed. Kõik kuus tarnijat peavad vastama nende vastuvõtu kriteeriumidele enne kogu taotluse täitmist.

## Enesehinnang

Kõik kuus spetsifikatsiooni kaarti ülesanded 1–12; ülesanne 13 hõlmab asukoha, integreeritud kontrollimise ja vabastamise. Valmisuse aruandlus, rakenduse seerialiseerimine, pärandi konfigureerimine, saladused, pakendi usaldus ja füüsilise seadme ebamugavus on selged omanikud ja testid. Välised juhid/SDK/hardware uksed jäävad ebatäielikuks, kuni tõestatud, mitte loetletud tarkvara edu.

## Kasutaja läbivaatamise valik

Soovitatav: native täitmine selles vestluses, ülesanne ülesandeks, sest enamik mooduleid jagada lepinguid ja praegune tööruum sisaldab olulisi olemasolevaid kohandusi. Delegeeritud täitmine on kättesaadav, kui kasutaja seda valib. Vaadake seda plaani ja valige rakendamise meetod enne rakendamist, nagu on nõutav plaanide töövool.

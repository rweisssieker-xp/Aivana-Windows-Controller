<!-- generated-doc locale=lv-LV source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Izplūdes īstenošanas plāns

> **Aģentūras darbiniekiem: ** SUB-SKILL: Izmanto superpower:izpildes plānus vietējai izpildei vai superpowers:subagent-drived-development pēc lietotāja izvēles delegācija. Sekojiet soļiem ar pārbaudes kastēm.

**Cilvēks: ** Piedāvā sešas integrētas paplašināšanās un spējas balstītas multi-izgatavotāju tastatūras atbalsts.

**Arhitektūra:** Saglabājiet kodolu / Windows / WPF ievietojot izolētus plugins un mobilo pakalpojumu procesus ar smagi autentificētu IPC; visa veikšana iet caur esošo kontrolieri un MacroRunner..

**Tehniskā stacija:** C# , .NET 10, WPF, Windows UI automātiski, Win32 Raw Input, Dokumentēts piegādātājs SDK, ASP.NET Core HTTPS/WebSocket, Sistēma.Text.Json.

** Spēks: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (izmantotāja apstiprināts)

## Globālie ierobežojumi

- Pastāvošajai konfigurācijai jābūt nemainīgai; papildināt darbības veidu, nevis atjaunot to pastāvīgās enum vērtības.
- Saglabājiet esošos profilus, G1/G2 uzdevumus, vietējo darbību un bezapmaksas lietošanu.
- Raw Input nodrošina tikai novērošanu. ekskluzīva uz ierīci izslēgšana prasa reālu tastatūras filtru aizmugurē.
- Importētie izpildāmie plugins nedrīkst darboties automātiski.
- Iekārtots un ierobežots konfigurētie saskarnes mobilo pakalpojumu; nav sūdzības par autentifikāciju.
- Īsuma/teksta piegāde nav pierādījums tam, ka mērķa pieteikums ir pieņēmis pasūtījumu.
- Neaizstādiet instalēto pieteikumu, instalējiet vadītājus, mainīsiet reālus tastatūras mapēšanas vai atvērtā tīkla piekļuvi kā nejaušu testu.
- Aizsargājiet neatbilstošas izmaiņas. Nelietojiet esošos kopīgos rediģējumus kā daļu no šīs funkcijas. Stage tikai neatkarīgi pārskatāmās funkcijas, ja tiek pieprasīti pasākumi.

## Pārskats Focus

1. Duplikāti identiski tastatūras modeļi: abpusēji jāpārslēdz (Tasks 10–12).
2. Atcelšana UIA/Provider/Plugin/Network gaidīšanas laikā: nav veikta rīcība pēc atcelšanas (Darbības 1, 4, 6, 8).
3. Lietotājs rediģē, kamēr burvju vai iepakojuma priekšlikums ir atvērts: pārskatīšanas nepareizi jāizvairās no klusā pārrakstīšanas (Tasks 3, 7).
4. Valoda un fiziskā izkārtojums atšķiras: izvēlētājiem un skenēšanas kodu saites nedrīkst būt atkarīgas no lokalizētajiem displeja nosaukumiem (Tasks 4, 10).
5. Tīkla atgriešanās un plugins: komandām nevajadzētu klusumā izpildīt divas reizes (Tasks 6, 8).

## Kopīgi līgumi un atkarības kārtība

Uzdevumi 1–3 izveido īstenošanu un uzstādīšanu. Darbs 4–5 būvēt UIA. Uzdevumi 6–7 piegādā plugins. Uzdevumi 8–9 nodrošina mobilo piekļuvi. Uzdevumi 10–12 nodrošina tastatūras adapterus un filtra integrāciju. Task 13 apstiprina pilnīgu produktu.

Katrs uzdevums ir nepilnības tests → minimālais īstenošanas cikls → pārejas tests. Uzdevums ir pabeigts tikai tad, ja tas ir pieejams, izmantojot pieteikumu un tās pārbaudes. Scaffolds, mock adapteris un dokumentācija vien nav apmierinoši.

### Uzdevums 1: tipa pārbaude un saderīga izturība

**Files:** radīt `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Modifikācija `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Starpība:** Ražošana `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` un `VerifyAsync(ActionStep, CancellationToken)`, Abas atgriešanās `Task<ActionVerificationResult>`. Pievienojiet iespējamo seriālu `VerificationOptions` Ar spēkā esošo karogu un laika izslēgšanu (100–60000 ms), pamatoti izslēgts. publicēt rakstītus rezultātus pa solim, tostarp Run ID, Tile ID un Step Index bez darbības maksas.

- [ ] Rakstot pārbaudes: mantojums JSON saglabā esošās rīcības numeriskās vērtības; trūkstošas iespējas neļauj verifikāciju; nevēlamas laika izbeigšanas neveiksme novērš izpildītāja iebildumu; neveiksmīga pēc nosacījuma nekad neizraisa verificētu panākumu; nepieņemts izpildītājs nav pārbaudāms; atcelšana pārtrauc verifikāciju un turpmākos soļus.
- [ ] Run `dotnet run --project tests/Aivana.Tests -c Release`; apstiprina jaunas pārbaudes neveiksmes pirms ieviešanas.
- [ ] īstenošanas līgumi, validācija, klonēšanas/snapshot saglabāšana un MacroRunner integrācija. Respektēt OnError stop/continue politiku; saglabāt esošos izpildes atsauksmes abonentiem.
- [ ] Izveidojiet kodolu testus; nepieciešams, lai visi pārbaudes pabeigtu.

### Darbs 2: Windows verifikācijas pakalpojumu sniedzēji un rezultāts UI

**Files:** radīt `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Modifikācija `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Testēšanas punkti.

**Savienojumi: ** WindowsActionVerification īsteno uzdevumu 1 pakalpojumu sniedzējs, izmantojot esošos audio, displeju, procesu / logu un OBS klientu. Iepriekšēja lidojuma pieejamības pārbaudes un identifikācijas; verifikācija salīdzina vēlamo stāvokli pēc ierobežotā pārbaudes. Ražotājs rezultātu snapshots patērēts ar WPF vēsturisko skatījumu.

- [ ] Pievienojiet neveiksmīgus testus, kas nav pieejami skaidri audio/izrāde ID, OBS laika iznākšana, nepareizi novērots stāvoklis, nezināmi īsziņas efekti, jutīga maksājuma izslēgšana un atcelta pārbaude, izmantojot injekcijas lasīšanas pakalpojumu sniedzējus.
- [ ] Īsteno reālu lasīšanas atbalsta verifikāciju absolūtā apjoma, mīkstā, absolūtā spilgtuma, procesa/vēstura gatavības un atbalstītajām komandām OBS. Relatīvo/togļu darbību prasa uzņemtā pre-valsts; pretējā gadījumā ziņot Nepārliecinoši.
- [ ] Pievienojiet pakāpenisku verifikāciju un drošu izpildes vēsturi, kas norāda uz stāvokli, neveiksmīgu prasību un atgūšanas tekstu.
- [ ] Run core/Windows pārbaudes un WPF smēķēšana ar iebūvētiem valsts piegādātājiem; nav reāla spilgtuma vai audio rakstīšanas.

### Darbs 3: vadīts darba vietas radīšana

**Files:** radīt `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modifikācijas paletes un dashboard ievadīšanas punkti.

**Interfesi: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; izvēle ietver veidnes ID, izvēlētās izpildāmās maršrutus, URL, audio/izrāde ID, OBS skatuves un izvēlētās saites. Discovery atgriežas nosauktos kandidātus ar stabilu ID un pieejamību. Konflikta atklāšanas konfigurācijas pārskatīšana.

- [ ] Pievienojiet neveiksmīgus testus visiem pieciem pieprasītajiem burvju ģimenēm, neatkarīgi radītos soļus, trūkst nepieciešamo mērķi, atcelt bez mutācijas un ietaupīt pret mainītiem pārskatījumiem.
- [ ] Atrodiet lietojumprogrammas, izmantojot reģistrētus pieteikumu maršrutus un Start Menu īsziņas; ierīces, izmantojot esošos pakalpojumu sniedzējus; skatuves, lasot tikai OBS pieprasījumu. Ievadiet manuālo izvēli neatradītajām lietotnēm.
- [ ] Izvēlēties darba plūsmu → Izvēlēti mērķi → Izvēlieties kontroles → Ievērojums/Validēt → Iegūt. Izveidojiet rediģējamus profilus un nekad neizmantojiet instalācijas laikā.
- [ ] Pievienojiet reālu WPF burvju smēķēšanas pārbaudes pabeigšanai / atcelšanai / konfliktu; veikt visus testus.

### Uzdevums 4: UIA selektoru un ierobežotu darbību

**Files:** radīt `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modifikācijas modeļi, konfigurācijas validācija un WinActionExecutor.

**Starpība:** Pielikums `ActionKind.UiAutomation`; `UiElementSelector` veikalu process, automātisko ID, kontroles veids un pasūtītais priekšnieks kritērijus. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Atgriešanās `Task`; Operācijas Invoke, SetValue, Select, Toggle, Focus un WaitState..

- [ ] Pievienojiet neveiksmīgus testus pret īpašu pārbaudes logu, lai pārvietotu / atceltu, duplikātu spēles, trūkstošos elementus, nepieņemamus modeļus, neuzturētus kontroles, paroles vērtības, Unicode etiketes un Timeout / anulēšanu.
- [ ] UIA īstenošana uz īpašu darba ņēmēju ar ierobežotu pieprasījumu un procesu mērķtiecību. Iespējami pakļaujot piegādātājiem ir atjaunojams darba ņēmēju robežsardzes; pieprasījuma atteikšanās nedrīkst atstāt nevēlamas blakusparādības.
- [ ] Izslēdz paroles vērtības; ziņo piekļuves/integritātes ierobežojumus, nevis pārsniedz tos.
- [ ] Izveidojiet Windows pārbaudes, izmantojot izolētus fiksācijas; pievienojiet tikai enum vērtības un atkārtoti pārbaudiet mantojuma izturību.

### Darbs 5: UIA picker un pārskatīts ieraksts

**Files:** izveidot `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modificēt darbību katalogu, redaktoru un ierakstu integrāciju; izveidot UIA WPF smēķēšanas pārbaudes.

**Savienojumi: ** Picker atgriežas `UiElementSelector`; ieraksts atgriežas ierosinātos UIA soļus un nekad rakstīt tieši uz glabāto profilu. Test-selektor komanda atgriežas spēļu skaitīšanai bez iebildumiem.

- [ ] Pievienojiet neveiksmīgus pārbaudījumus: pikers izslēdz MAKRIVO un paroles vērtību; ieraksts / atcelšanas lapas pastāvošo profilu nesamazina; neskaidri iepriekš redzēt ziņojumus; pieņemot ierakstītos soļus, tiek izveidots rediģējams projekts.
- [ ] Izpildes elementu izcelsme / attēls, darbības specifiskie parametri, selektoru pārskats un ieraksta pārskats. Atlikušo ierakstu uzvedība.
- [ ] Pievienojiet katalogus un piekļuvi paletei; pirms saglabāšanas validējiet redaktora kļūdas.
- [ ] Izveidojiet Windows un WPF smēķēšanas pārbaudes uz fiksāciju; pārbaudiet pikera / redaktora ekrāna attēlus.

### Darbs 6: izolēts plugins hosts un protokols

**Files:** radīt `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Modificēt risinājumu un rīcību.

**Starpība:** Version-1 manifests nosaka iepakojuma ID/versiju, ieejas punktu, parametru shēmas, darbības, valsts sniedzēji un spēju deklarācijas. IPC ziņojumi satur protokola versiju, unikālu pieprasījuma ID, metodi, JSON parametri un rakstīto rezultātu. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Atgriež iespaidīgu rezultātu. pielikums `ActionKind.Plugin`.

- [ ] Pievienojiet neveiksmīgus testus nesaderīgajam protokolam, kļūdainu/pārmērīgu (>1 MiB) ziņojumu, duplikātu pieprasījumu ID, atcelšanu, krīzi, neatbildīgu procesu un trūkstošo darbību.
- [ ] Izveidojiet vienlīdz lietotāja nosaukumu IPC ar 10 sekundes rokasgrāmatu un konfigurējamu izpildes laiku, kas nokārtota 60 sekundēs. Atcerieties neatbildīgu darba ņēmēju; pēc atkārtošanās automātiski neuzrāda darbību lidmašīnā.
- [ ] Izpilda shēmas un atļauts kontrolieru pakalpojumus. Izskaidrojiet izpildāmās plugins uzticību; procesa izolācija nav operētājsistēmas smilts.
- [ ] Izveidojiet galveno testu un integrācijas testus pret reālajiem testu plugins.

### Darbs 7: Plugin paketi un piemērs integrācija

**Files:** radīt `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, iepakojuma testi; modifikācijas paletes / iestatījumus.

**Starpība:** Pakalpojumu izstāde `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` un `UninstallAsync(packageId)`. Pārskatītais iepakojums ieraksta hashes, atkarības, versija un deklarētās iespējas.

- [ ] Pievienojiet neveiksmīgus testus zip traversal, absolūtās maršrutus, simlink/reparse bēgšanu, duplikācijas, >100 MiB paplašinātus datus, hash nepareizi, lejupielādēt, staļina pārskatus un traucēt importu.
- [ ] Ievērojums un izteikta atļauja, atkarības kļūdas, stacionārā uzstādīšana un atjaunināšana; darba ņēmējs pirms noņemšanas.
- [ ] Dodot īstu piemēru plugins, kas atgriežas konfigurējamu skaitīšanas darbību un dzīvo stāvokli caur IPC, tostarp atcelšanu; iepakojumu / eksportu / instalēšanu / izinstalēšanu caur UI.
- [ ] Run pakete / host testi un WPF end-to-end pārbaudes. Pārliecinieties, ka pirms atļaujas saņemšanas nav ievadītas izpildāmās runas.

### Uzdevums 8: HTTPS mobilo pakalpojumu un pāriet

**Files:** radīt `src/Aivana.MobileHost/` ASP.NET kods, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Integrācijas testi; modifikācijas risinājums un kontrolieris.

**Savienojumi: ** Mobilais pakalpojums sarunās ar kontrolieri caur to pašu lietotāju IPC. Ceļi: pāris, atļauts profili / rīcība, autentificēta rīcības atsaukšana, atcelšana un WebSocket statuss. Iesniegums ietver unikālu komandas ID; kontrolierim ir ierobežota deduplikācija. Kredīti ir atcelti un glabāti ar Windows aizsardzību.

- [ ] Pievienojiet neveiksmīgus testus par nepārtrauktu sarakstu/izpildīšanu, izbeigto kodu, atkārtoti izmantoto kodu, atcelto tokenu, neesošu izcelsmi, duplikātu komandas, pārmērīgu rāmju un plūdu robežām.
- [ ] Ievietojiet HTTPS tikai klausītājs, lietotāja izvēlētās saskarnes, vietējās sertifikācijas / uzticamības instrukcijas, vienreiz 6 skaitļu pārsniedz kodus pēc 120 sekundēm, maksimāli piecas mēģinājumi par kodu un token atcelšanu. Uzglabāt hashed nejaušus tokenus; nekad reģistrēt uzticamības.
- [ ] Ievietošana ļauj uzskaitīt stabilu rīcības ID un pastāvīgu ceļa virzienu; atteikties no arbitrāžas shell, ceļiem un paketes instalēšanas komandām. Iestatīt skaidras klientu likmes robežas (10 komandas/sek, 30 burst) un 64 KiB ziņojuma robežas.
- [ ] Pārbaudiet loopback integrācijas testus, nesniedzot Firewall noteikumus vai atļaujot LAN piekļuvi; pārbaudiet slēgšanu un tokenu izturību.

### Uzdevums 9: Mobilais panelis un desktop pakalpojumu kontroles

**Files:** radīt `src/Aivana.MobileHost/wwwroot/` Reakcijas panelis un `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Pievienojiet pārlūkprogrammu/mobiļu verifikācijas skriptu un WPF Smēķēšana.

**Interfēri: ** Patēriņa uzdevums 8 ceļus tikai. Ietekme rīcības tīkla, profila pikers, dzīvo vērtību / progresa, atcelšana, savienojuma stāvoklis un pāriet. Desktop kontrolē pakalpojuma sākumu / apturēšanu, adreses / sertifikācijas instrukcijas, atļautu profilu un klientu atsaukšanu.

- [ ] Pievienojiet neveiksmīgas integrācijas pārbaudes auth plūsmai, stale/missing rīcības IDs, atkārtoti savieno bez rīcības pārspīlēšanas, atcelts klients un pakalpojumu apturēšana.
- [ ] Ieviest pieejamu atbildīgu lietotāju, izmantojot vietējos aktīvus, ne trešo pušu skriptus vai interneta atkarības. Tokens nekad nenotiek URL.
- [ ] Pārbaudiet pārlūkprogrammas izkārtojumu 360/768/1280 CSS px; pārbaudiet faktiskās ekrāna attēlus. Pārbaudiet ar reālu tālruni/tableti tikai pēc tam, kad lietotājs skaidri atļauj LAN pakalpojumu.
- [ ] Dokumentu un ziņojumu mobilo aparatūras verifikācija atsevišķi no pārlūkprogrammas emulācijas.

### Darbs 10: Ierīces iespējas un parastais atslēga novērošana

**Files:** radīt `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; izmaiņas RawKeyboardBindingService, KeyboardBinding, validators un ierīces diagnoze; pievienojiet testus.

**Interfēri: ** `IKeyboardAdapter` izpaužas spēja snapshots, stabilu ierīces identitāti un fiziskas atslēgas (skābekļa kods, paplašinātas karoga, preses/izlaišanas). Modi atšķiras novērošanu, kartēta standarta ieejas, dzimto SDK un vadītāju ekskluzīvu. Pastāvīgās virtuālās atslēgas īpašās saites saglabā savu pārstāvību un migrācijas uzvedību.

- [ ] Pievienojiet neveiksmīgus testus vecajām saistībām, ievadiet atšķirības, valodas / izkārtojuma neatkarību, atkārtoti lejupielādēt, aizturēt, dubultā modelīšanās un injekcijas atteikums.
- [ ] Paplašiniet novērošanu uz parasto fizisko atslēgu, nepieņemot tos. Pievienojiet skaidru uzņemšanu un savieno identitātes apstrādi.
- [ ] Parādiet reālo adaptera jaudu un ierīces lietojumprogrammas nepārbaudīto stāvokli; nekad nerunājiet par Fn/dial atbalstu no zīmola nosaukumiem.
- [ ] Run core/Windows/WPF pārbaudes ar simulētiem ziņojumiem un lasīt vienīgi ierīces sarakstu.

### Uzdevums 11: Ražotājs adapteris

**File:** Izveidojiet atsevišķus failus zem `src/Aivana.Windows/KeyboardAdapters/` LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog un ģenētisko QmkVia vadlīniju; modificēt reģistru/UI un pievienot adaptera protokola pārbaudes.

**Savienojumi: ** Patēriņa uzdevums 10 adaptera līgumi. Nīderlandes bibliotēkas uzliek tikai no validētiem lietojumprogrammu/pārdevēja vietām; SDK trūkums ir pieejams. Apgaismojuma un ieejas iespējas ir atsevišķas.

- [ ] Pārbaudiet pašreizējos oficiālos SDK nosaukumus, pārdales noteikumus, programmatūras atkarības un atbalstītos modeļus pirms katra adaptera ieviešanas; ierakstu avoti un precīzas versijas.
- [ ] Pievienojiet neveiksmīgus testus SDK trūkuma / atgriešanās arhitektūrai, notikumu presē / atkārtojumam, izslēgšanai, nepareizai atbildībai un nepieņemtai ierīcei. Izmantojiet injekcijas native-call abstrakcijas determinist pārbaudes.
- [ ] īstenošana Logitech G atslēga un atbalsta apgaismojums atsevišķi; Razer Chroma apgaismojums Synapse Standarta atslēga mape; SteelSeries GameSense Valsts / Apgaismojums; Wooting Analoga pārbaude ar konfigurējamiem robežām / histerēzi; QMK/VIA standarta atslēga iestatīšanas vadlīnijas. neuzskatiet īpašumtiesības no apgaismojuma APIs.
- [ ] Validēt saderīgus reālus ierīces, kur pieejamas. Marķēt citus īstenotu/hardware pārbaudīts, ar modelis/firmware pierādījumu; trūkst izmantojamu SDK ir atklāts ārējais portāls.

### Uzdevums 12: Ekskluzīvs tastatūras filtra aizmugurējais

**Files:** radīt `src/Aivana.Windows/KeyboardFilter/`, Ievadiet avotu zem `tools/keyboard-filter/`, Backend integrācijas testi un izvietošanas dokumentācija. ir nepieciešama īpaša parakstīta vadītāja projekts, ja neviena atbilstoša licencēta backend nepārsniedz novērtējumu.

**Interfesi: ** Backend ziņojumi uzstādīts / saderīgs / parakstīts stāvoklis; izpaužas ierīces specifiskas uzņemšanas sākums / apturēšana, atslēgvārdi un ārkārtas izlaišana. Atsevišķa uzraudzības sistēma atjauno kontroliera zaudējumus. Galvenā tastatūra nekad kļūst ekskluzīva.

- [ ] izvērtēt kandidātu vadītāja licences, parakstīts bināro izcelsmi un pašreizējo Windows 11/Secure boot saderību. Iepazīstieties ar lēmumu “no-go”. Autovadītāja instalācija parastās pārbaudes laikā.
- [ ] Rakstīt līguma testus, lai filtrētu mērķa izvēli, neveiksmi, sirdsdarbības izbeigšanu, IPC zudumu, atkārtoti savienotu nevienlīdzību, pārtraukšanu/atceļošanu, atbrīvošanu un izbeigšanos.
- [ ] Implement backend, explicit selection/activation UI, ārkārtas izlaišanas un instalatoru/uninstaller. Neaizstāj zemas līmeņa/Raw Input laika heuristiku.
- [ ] Validēt atjaunojamā testa mašīnā ar divām tastatūrām: izvēlētais atslēga tiek izslēgta tikai uz izvēlēto ierīci; galvenais rakstīšana nav ietekmēta; crash/suspend/uninstall atgriežas.
- [ ] Gatavošanas pabeigšana par faktisko parakstīto/licencēto apakšsaziņas līdzekļu un fizisko testu. Ja ārējais paraksts / licencēšana nav pieejama, uzdevums ir nepilnīgs ar precīzu atkarības pierādījumu.

### Darbs 13: Integrēta verifikācija, tulkojumi un izlaišanas pakete

**File: ** Pievienojiet `docs/testing/2026-10-12-six-extensions.md`; atjauniniet attiecīgo DE/EN/FR/IT dokumentāciju, atrašanās vietas katalogu un smēķēšanas pārbaudes punktu. Saglabājiet citus dokumentus.

- [ ] Integrējiet visus ieejas punktus un snapshots; pilnīgu tulkojumu, izmantojot esošo 27 valodas katalogu un atklājot nepārskatītus tulkojumus.
- [ ] Runāšana `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, un `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Pārbaudiet desktop / mobilo ekrānu attēlus un pārbaudīt 100/150/200 procentuālo Windows skaitu, konfigurācijas braucieni, plugins dzīves ciklu, tīkla atcelšanu un reālu tastatūras savienojumu.
- [ ] Publicēt jaunā katalogā ar `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; ietver līdzdalības pakalpojumus/host binārus un licences paziņojumus. Nelietojiet pārrakstīt instalēto aplikāciju.
- [ ] Ziņojums ir pabeigti testi, neparedzēti adapteri un ārējie atbrīvošanas vārti. Visi seši piegādātāji atbilst saviem pieņemšanas kritērijiem, pirms sazinieties ar visu pieprasījumu.

## Pašpārskats

Visi seši spec sadaļas kartes uz uzdevumi 1–12; uzdevums 13 aptver atrašanās vietu, integrētu verifikāciju un izlaišanu. Sagatavotības ziņošana, izpildes serijalizācija, mantojuma konfigurācija, noslēpumi, iepakojuma uzticība un fiziskā ierīce ir skaidri īpašnieki un testi. Ārējais vadītājs / SDK / aparatūras durvis paliek nepilnīgas, kamēr nav pierādīts, nevis uzskatāms par programmatūras panākumiem.

## Izvēle lietotāja atsauksmēm

Ieteicams: dzimtas izpildes šajā tērzē, uzdevums pēc uzdevuma, jo lielākā daļa moduļu dalās līgumos un pašreizējā darba telpā ir būtiski esošie redakcijas. Delegēta izpilde ir pieejama, ja lietotājs to izvēlas. Pārskatiet šo plānu un izvēlieties izpildes metodi pirms īstenošanas, kā to prasa rakstīšanas plāni darba plūsma.

<!-- generated-doc locale=da-DK source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Udvidelse af gennemførelsesplanen

> **For agentarbejdere: ** BEDTIGT SUB-SKILL: Brug superpower:udførelsesplaner for indfødte udførelser eller superpower-udvikling efter brugeren vælger delegation. Følg trin med checkboxes.

** Mål: ** Leverer seks integrerede udvidelser og kapacitetsbaseret multi-producent tastaturstøtte.

**Arkitektur:** Beskytt kernen / Windows / WPF indførelse af isoleret plugin og mobile service processer med tæt autentiseret IPC; alle udførelser går gennem den eksisterende kontroller og MacroRunner quue..

**Teknisk Stack:** C og .NET 10, WPF, Windows UI Automation, Win32 Raw Input, Dokumenteret leverandør SDK'er, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**SPEC:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (Godkendt af brugeren).

## Globale begrænsninger

- Den eksisterende konfiguration skal lastes uændret; tilføje handlingstyper i stedet for at genordne deres vedvarende enum værdier.
- Bevar eksisterende profiler, G1/G2 opgaver, lokal drift og kontofri brug.
- Raw Input Exklusiv fjerning pr. enhed kræver en reel tastaturfilter backend.
- Importerede implementerbare plugins skal ikke køre automatisk.
- Default til disabled og begrænsede konfigurerede grænser for den mobile service; ingen klageautentisering fallback.
- Shortcut/tekstlevering er ikke et bevis på, at den pågældende ansøgning har accepteret kommandoen.
- Du skal ikke erstatte den installerede applikation, installere drivere, ændre virkelige tastaturmappinger eller åbne netværkstilgang som en tilfældig test.
- Beskytt uforholdsmæssige ændringer. Du må ikke indgå eksisterende delte redigeringer som en del af denne funktion. Stage kun uafhængigt reviderbare funktioner, hvis forpligtelser er anmodet.

## Oversigt fokus

1. Duplicate identiske tastaturmodeller: en ambigu re-forbindelse skal kræve genvalg (Tasks 10–12).
2. Cancellation under UIA/leverandør/plugin/netværksvækst: Ingen aflyst handling efter annullering (Task 1, 4, 6, 8).
3. Brugeren redigerer, mens trollet eller pakken forudsigelse er åben: revision mismatch skal forhindre stille overskrivning (Tasks 3, 7).
4. Sprog og fysisk layout adskiller sig: Selektorer og scan-kode bindinger må ikke afhænge af lokaliserede displaynavn (Tasks 4, 10).
5. Netværksretning og plugin genstart: kommandoer må ikke udføres to gange (Tasks 6, 8).

## Delte kontrakter og afhængighed

Udførelse af opgaver 1–3 og indførelse. Arbejdet 4–5 opbygger UIA. Task 6–7 leverer plugins. Opgaver 8–9 giver mobil adgang. Task 10–12 leverer tastaturadapter og filterintegration. Task 13 validerer hele produktet.

Hver opgave følger en fejlagtig test → minimal implementering → passing-test cyklus. En opgave er kun afsluttet, når den er tilgængelig gennem ansøgningen og dens kontroller passerer. Scaffold, mock adaptere og dokumentation alene tilfredsstiller ikke en leverbar.

### Task 1: Typisk verifikation og kompatibel holdbarhed

**filer:** Skabelse `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Ændring `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaces:** Produktion `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` og `VerifyAsync(ActionStep, CancellationToken)`, Begge vender tilbage `Task<ActionVerificationResult>`. Tilføj valgfri serialiserbar `VerificationOptions` Med flag og timeout (100–60000 Udgiv typede resultater pr. trin, herunder Run ID, Tile ID og Step Index uden aktionsbelastninger.

- [ ] Skriv kontroller: arv JSON bevarer eksisterende handlingsnumre værdier; fraværende muligheder inaktiverer verifikation; invalid timeout fejler validering; foreløbig fejl forhindrer udføreren invokation; mislykkede postcondition aldrig producerer verifiseret succes; uafhængig udføreren er Uverifierbar; annullering stopper verifikation og efterfølgende skridt.
- [ ] Run `dotnet run --project tests/Aivana.Tests -c Release`; bekræftelse af nye kontroller fejler før implementering.
- [ ] gennemførelse af kontrakter, validering, kloning/snapshot bevarelse og MacroRunner integration. Overhold OnError stop/continue politikker; bevar eksisterende eksekutions feedback abonnenter.
- [ ] Udfør kernetester; kræver alle kontroller pass.

### opgave 2: Windows verifikation leverandører og resultat UI

**filer:** Skabelse `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Ændring `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Test indgangspunkter.

**Interfaces: ** WindowsActionVerification implementerer Task 1 leverandør ved hjælp af eksisterende lyd, display, proces/vindue og OBS klienter. Forudflyvning tester tilgængelighed og identificeringer; verifikation sammenligner den ønskede tilstand efter begrænset spørgeskema. Produktion af kontrollerne resulterer i snapshots, der forbruges af WPF historievisning.

- [ ] Tilføj mislykkede tests for utilgængelige eksplicite audio/display-ID'er, OBS timeout, mismatched observeret tilstand, ukendte kortskæringseffekter, følsom betalingsladelse og annulleret spørgeskema, ved hjælp af injektable læsere.
- [ ] Implementering af reel læsningsbaseret verifikation for absolut volumen, blødhed, absolut lysstyrke, proces/vindue klarhed og understøttede OBS kommandoer. Relative/toggle handlinger kræver en fanget for-stat; ellers rapporter Unverifiable.
- [ ] Tilføj skridtkontrol og sikker eksekutionshistorie, der viser status, mislykkede krav og genopretning tekst.
- [ ] Run core/Windows checks og WPF røg med injektion stat leverandører; ingen virkelige lys eller lydskrivninger.

### Task 3: Guide til arbejdsområde skabelse

**filer:** Skabelse `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modifikation af palet og dashboardindgangspunkter.

**Interfaces: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; udvalget indeholder template ID, udvalgte eksekuterbare veje, URL'er, audio/display ID'er, OBS scene og udvalgte bindinger. Discovery returnerer navngivne kandidater med stabile ID'er og tilgængelighed. Udkast til konfigurationsrevision for konfliktdetektion.

- [ ] Tilføj mislykkede tests for alle fem anmodede trollfamilier, uafhængige skridt genereret, manglende anmodede mål, annullering uden mutation og spar mod ændret revision.
- [ ] Udforsk applikationer gennem registrerede app-veje og Start-menuer; enheder gennem eksisterende leverandører; scener gennem læsning alene OBS spørgeskema. Hold manuelt udvalg tilgængeligt for ukendte apps.
- [ ] Implementér Vælg arbejdsproces → Vælg mål → Vælg kontroller → Validering/Validering → Save. Generere redigerbare profiler og køre aldrig under indstilling.
- [ ] Tilføj virkelige WPF magisk røg kontroller for afslutning/anvendelse/konflikt; køre alle tests.

### opgave 4: UIA selektorer og begrænsede handlinger

**filer:** Skabelse `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modifikation af modeller, konfiguration validation og WinActionExecutor.

**Interfaces:** Tilføjelse `ActionKind.UiAutomation`; `UiElementSelector` Store processer, automatisering ID, kontrol type og bestilte forfædre kriterier. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Tilbage `Task`; Operationer Invoke, SetValue, Select, Toggle, Focus og WaitState..

- [ ] Tilføj mislykkede tests mod et dedikeret testvindue for genoptagelse/aftagelse, duplicerede kampe, savnede elementer, ustøttede mønstre, deaktiverede kontroller, adgangskoder, Unicode-etiketter og timeout/annullering.
- [ ] Implementering af UIA på en dedikeret arbejdstager med begrænsede anmodninger og procesmåling. Potentielt hængende udbydere kører bag en tilbagetrukket arbejdstagergrænse; forlader en anmodning må ikke efterlade skræmte bivirkninger.
- [ ] Undgå adgangskoder; rapporter adgang/integritet begrænsninger i stedet for at overskride dem.
- [ ] Run Windows kontroller ved hjælp af isolerede fixer; tilføje enum værdier kun og rerun arv persistens kontroller.

### Task 5: UIA picker og anmeldt optagelse

**Filer: ** Skab `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; ændre handlingskategori, editor og optagere integration; skab UIA WPF røgkontrol.

**Interfaces: ** Picker returnerer en `UiElementSelector`; optegneren returnerer de foreslåede skridt i UIA og skriver aldrig direkte til en gemt profil. Test-selektorkommandoen returnerer match count uden invocation.

- [ ] Tilføj manglende kontroller: picker udelukker MAKRIVO og adgangskode-værdi optagelse; optagelse/anvendelse af eksisterende profil uændret; ambigu forudsigelsesrapporter match count; acceptering af optegnede skridt skaber en redigerbar udgave.
- [ ] Implementation element highlight/picker, operation-specifik parameter, selektor forudsigelse og optegnelsesvindue. Fortsæt det eksisterende kortsnit.
- [ ] Tilføj katalogindtægter og palet adgang; valider redigeringsfejl før gemt.
- [ ] Run Windows og WPF røgkontrol på fastgørelsen; inspicere picker/editor screenshots.

### Task 6: Isoleret plugin-host og protokol

**filer:** Skabelse `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Modifikation og handling.

**Interfaces:** Version-1 manifest angiver pakke-ID/version, indgangspunkter, parameter skemaer, handlinger, statlige leverandører og kapacitetserklæringer. IPC-meddelelser indeholder protokolversion, unikke anmodning ID, metode, JSON-parametre og typiske resultater. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Tilbage af et resultat. append `ActionKind.Plugin`.

- [ ] Tilføj mislykkede tests for uforenelig protokol, malformet/overformet (>1 MiB) meddelelse, duplikeret anmodning ID, annullering, ulykke, uansvarlig proces og manglende handling.
- [ ] Implementation af samme bruger-navnet IPC med 10-sekunder håndshake og konfigurerbar udførelsestid kappet på 60 sekunder. Slutt uansvarlige arbejdere; genoptage ikke automatisk en handling i flyvning efter genstart.
- [ ] Anvendelse af ordninger og tilladte kontroller. Forklare executable-plugin tillid; procesisolering er ikke en sandboks.
- [ ] Udfør kernetests og integrationstests mod virkelige testpluginsprocesser.

### Task 7: Plugin pakker og eksempel integration

**filer:** Skabelse `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, pakkeprøver; ændre palet/indstillinger.

**Interfaces:** Pakkeudstillinger `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` og `UninstallAsync(packageId)`. Revideret pakke rekord hash, afhængigheder, version og deklarerede kapaciteter.

- [ ] Tilføj mislykkede tests for zip traversal, absolutte veje, symlink/reparse undladelse, duplicate indtægter, >100 MiB udvidet data, hash mismatch, nedgang, stale anmeldelse og disabled import.
- [ ] Implementation forudsigelse og udtrykkelig aktivering, afhængighed fejl, trin installation og opgradering rollback; aflader arbejderen før fjernelse.
- [ ] Leverer en ægte eksempel plugin, der returnerer en konfigurerbar countdown handling og live status gennem IPC, herunder annullering; pakke/eksport/install/uninstall via UI.
- [ ] Run pakke/host tests og WPF end-to-end kontroller. Kontroller ingen importerede udførlige ruter før aktivering.

### Task 8: HTTPS mobil service og paring

**filer:** Skabelse `src/Aivana.MobileHost/` ASP.NET Core projekt, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Integrationstest; ændre løsning og kontroller.

**Interfaces: ** Mobilt service samtale til den samme bruger IPC. Router: paring, tilladt profiler/aktioner, autentiseret handling invokation, annullering og WebSocket status. Indkaldelse omfatter en unik kommando-ID; kontrolleren ejer en begrænset deduplication. Kravene kan tilbagekaldes og lagres via Windows beskyttelse.

- [ ] Tilføj mislykkede tests for uovertruffen liste/udførelse, udløbet kode, genanvendt kode, tilbagekaldt token, ulovlig oprindelse, duplikeret kommando, oversigtsramme og oversvømmelsegrænser.
- [ ] Implementering af HTTPS-kun lytter, brugeren valgte grænseflader, lokale certificerings/trust instruktioner, en gang 6-digit parende koder udløber efter 120 sekunder, max fem forsøg pr. kode og token tilbagekaldelse. Spare hashede tilfældige tokener; aldrig log på bekræftelser.
- [ ] Implementation tillader listede stabile handlingsid og eksisterende routing; afviser vilkårlige shell, veje og pakke-installation kommandoer. Konfigurer eksplicite pr. kundegrænser (10 kommandoer/sek, 30 burst) og 64 KiB-meddelelsesgrænse.
- [ ] Kontroller loopback integration tests uden at tilføje firewall regler eller tillade LAN adgang; test lukning og token persistens.

### Task 9: Mobile panel og desktop service kontroller

**filer:** Skabelse `src/Aivana.MobileHost/wwwroot/` Det responsive panel og `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Brug af browser/mobil verifikationskripter og WPF Røykoverførsel.

**Interfaces: ** Forbrug opgaver 8 kun ruter. Eksponere touch action grid, profil piker, live værdier/progress, annullering, forbindelse status og paring. Desktop kontrollerer service start/stop, adresse/certificering instruktioner, tilladte profiler og tilbagekaldelse af klient.

- [ ] Tilføj fejltagne integrationskontroller for auth flow, stale/missing action IDs, genforbindelse uden handling replay, tilbagekaldt klient og service stop.
- [ ] Implementering af tilgængelig responsiv UI ved hjælp af lokale aktiver, ingen tredjeparts skript eller internetafhængighed. Tokener vises aldrig i URL'er.
- [ ] Test browser layout på 360/768/1280 CSS px; inspektion af virkelige skærmbilleder. Kontroller med en reel telefon/tablet kun, når brugeren udtrykkeligt tillader LAN-tjenesten.
- [ ] Dokumentation og rapportering af mobile hardware separat fra browser emulation.

### opgave 10: Enhedsfunktioner og almindelig nøgleobservation

**filer:** Skabelse `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; ændre RawKeyboardBindingService, KeyboardBinding, validator og enhedsdiagnose; tilføje tests.

**Interfaces: ** `IKeyboardAdapter` udsætter kapacitetsskærm, stabil enhed identitet og fysiske nøgle kanter (skanning kode, udvidede flag, press/release). Moderne adskiller observation, mappet standardindgang, indfødt SDK og driver-eksklusiv. De eksisterende virtuelle nøgle-særlige bånd bevarer deres repræsentation og migrationshed.

- [ ] Tilføj mislykkede tests for gamle bindinger, indsendelse forskelligheder, sprog/layout uafhængighed, gentagne nedsættelser, unplug mens holdes, duplicate-model ambiguitet og injektion-input afvisning.
- [ ] Udvid observation til almindelige fysiske nøgler uden at undertrykke dem. Tilføj eksplicit fange og genforbind identitetsbehandling.
- [ ] Vis de faktiske adapteregenskaber og hardware-testeret status i enhedens UI; aldrig indgive Fn/dial support fra varemærker.
- [ ] Run core/Windows/WPF kontrollerer med simulerede rapporter og læs-only enhed.

### Task 11: Fabrikant adaptere

**Filer:** Skab separate filer under `src/Aivana.Windows/KeyboardAdapters/` for LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog og generisk QmkVia vejledning; ændre register/UI og tilføje adapter protokolkontrol.

**Interfaces: ** Forbruget opgave 10 Adapter kontrakter. Native biblioteker lastes kun fra validerede app/sælgersteder; mangel på SDK er en utilgængelig kapacitet. Belysning og indtastning er adskilt.

- [ ] Kontroller de aktuelle officielle SDK-hovedstoffer, omfordelingskrav, softwareafhængigheder og understøttede modeller, før du implementerer hver adapter; optegnelse kilder og nøjagtige versioner.
- [ ] Tilføj mislykkede tests for SDK-arkitektur, begivenhedspress/udgivelse/optagelse, opløsning, forstyrret respons og ustøttet enhed. Brug indfødte abstraktioner til deterministiske kontroller.
- [ ] Implementation Logitech G-key indtastning og støttet belysning separat; Razer Krom lyset Synapse Standard nøgle til magi; SteelSeries GameSense Stats / lys; Wooting Analogsundersøgelse med konfigurerbare grænser/hysteres; QMK/VIA Standard-nøgle indstilling vejledning. Ikke kræve ejendomsindtægter fra belysning APIs.
- [ ] Validerer kompatible reelle enheder, hvor de er tilgængelige. Markér andre implementeret/hardware-testeret, med model/firmware bevis; manglende brugbar SDK er en afsløret ekstern port.

### Task 12: Eksklusivt tastaturfilter baggrunde

**Filer:** Skab `src/Aivana.Windows/KeyboardFilter/`, installationskilde under `tools/keyboard-filter/`, baggrundsintegrationstest og implementeringsdokumentation. Et dedikeret undertegnet projekt er påkrævet, hvis ingen passende licenseret backend passerer evalueringen.

**Interfaces: ** Backend rapporter installeret/kompatible/signet tilstand; udsender enhedspecifik optagelse start/stop, nøglefronter og nødsituation. En separat overvågning genopretter pass-through på kontroller tab. Den vigtigste tastatur bliver aldrig eksklusiv ved standard.

- [ ] Vurderer kandidater driver licens, underskrevet binær kilde og nuværende Windows 11/Secure Boot kompatibilitet. Det er en go/no-go beslutning. Ingen kørekort under almindelige tests.
- [ ] Skriv kontraktprøver for filtermålsvalg, mislykkede åbninger, hjerteslag udløber, tab af IPC, genforbindelse ambiguitet, pause / annullering, hold-key frigivelse og fjernelse.
- [ ] Implementering backend, udtrykkelig udvælgelse/aktivering UI, nødsituation og installatør/uninstaller. Ikke erstatte lav-niveau-høj/Raw Input timing heuristik.
- [ ] Validerer i en genanvendelig testmaskine med to tastaturer: valgt nøgle slukkes kun på det valgte enhed; hovedskrivning ikke påvirket; crash/suspend/uninstall returnerer input.
- [ ] Gate produktion afsluttet på den faktiske undertegnede/licensede bagend og fysiske tests. Hvis ekstern signatur/licens ikke er tilgængelig, skal opgaven holdes ufuldstændig med nøjagtige bevis for afhængighed.

### Task 13: Integreret verifikation, oversættelser og udgivelsespakke

**Filer: ** Tilføj `docs/testing/2026-10-12-six-extensions.md`; opdater relevante DE/EN/FR/IT dokumentation, lokalisering kataloger og røg check indgangspunkter. Beskytt andre dokumenter.

- [ ] Integrere alle indtægtspunkter og snapshots; udfylde oversættelser ved hjælp af eksisterende 27-talekategori og afsløre uoversættelser.
- [ ] løb `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, og `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Inspektere desktop/mobile skærmbilleder og verificere 100/150/200 procent Windows skalaer, konfiguration runde rejser, plugin livscykel, netværk revocation og virkelige tastatur genforbindelse.
- [ ] Udgiv i en ny artificiel katalog med `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; inkluderer partner service/host binære og licensmeddelelser. Du skal ikke overskride den installerede app.
- [ ] Rapporten afsluttede tests, hardware-testerede adaptere og eksterne frigivelsesdøre. Alle seks leverancer skal opfylde deres acceptskriterier, før de ringer hele anmodningen fuldstændig.

## Planlægning af selv

Alle seks afsnit kort til opgaver 1–12; opgave 13 dækker lokalisering, integreret verifikation og frigivelse. Klarhed rapportering, udførelse serialisering, arv konfiguration, hemmeligheder, pakke tillid og fysisk-udstyr ambiguitet har udtrykkelige ejere og tests. Eksterne drivere/SDK/hardware gader forbliver ufuldstændige, indtil de er bevist, snarere end at regnes som software succes.

## Udførelsesvalg for brugeren anmeldelse

Anbefalet: Native udførelse i denne chat, opgave efter opgave, fordi de fleste moduler deler kontrakter og det nuværende arbejdsområde indeholder væsentlige eksisterende redigeringer. Delegeret udførelse er tilgængelig, hvis brugeren vælger det. Overvej denne plan og vælg udførelsesmetoden før implementeringen, som det kræves af skrivningsplaner.

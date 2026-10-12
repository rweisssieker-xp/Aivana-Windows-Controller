<!-- generated-doc locale=nb-NO source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Utvidelser gjennomføring plan

> **For agentarbeidere:** FORVARNED SUB-SKILL: Bruk superpower:utføringsplaner for innfødte utførelser eller superpower-utvikling etter at brukeren velger delegasjon. Følg trinnene med sjekkbokser.

** Mål:** Leverer seks integrerte utvidelser og kapasitetsbasert multi-fabrikkers tastaturstøtte.

**Arkitektur:** Oppbevar kjernen / Windows / WPF Introduksjon av isolert plugin og mobiltjenesteprosesser med smal autentiserte IPC; all utførelse går gjennom den eksisterende kontrolleren og MacroRunner quue..

**Teknisk Stack:** C# og .NET 10, WPF, Windows Ui Automasjon, Win32 Raw Input, Dokumentert leverandør SDK, ASP.NET Core HTTPS/WebSocket, System.tekst.Json.

**Spesjon:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (Godkjent av brukeren).

## Globale begrensninger

- Den eksisterende konfigurasjonen må lastes uendret; legge til handlingstyper i stedet for å redigere sine vedvarende enumverdier.
- Oppbevar eksisterende profiler, G1/G2 oppgaver, lokal drift og kontofri bruk.
- Raw Input Eksklusiv per-device fjerning krever en ekte tastaturfilter bakgrunnen.
- Importerte utførbare plugins må ikke utføres automatisk.
- Default til deaktivering og begrensning av konfigurerte grensesnitt for den mobile tjenesten; ingen klageautentisering fallback.
- Kort/tekstlevering er ikke bevis på at målsøkelsen godkjente kommandoen.
- Ikke erstatte den installerte appen, installere drivere, endre virkelige tastaturmappinger eller åpne nettverkstilgang som en tilfeldig test.
- Oppbevar uberørte endringer. Ikke engasjere eksisterende delte redigeringer som en del av denne funksjonen. Stage kun uavhengige funksjonell søk hvis forpliktelser er bedt om.

## Oversikt fokus

1. Duplikate identiske tastaturmodeller: Ambiguous gjenkobling må kreve re-seleksjon (Tasks 10–12).
2. Avbestillinger under UIA/leverandør/plugin/nettverk vent: ingen avbestillinger etter avbestillingen 1, 4, 6, 8).
3. Brukeren redigerer mens forhåndsvisning av troll eller pakke er åpen: revisjonsfeil må forhindre stille overskrift (Tasks 3, 7).
4. Språk og fysisk layout er forskjellig: Selektorer og skannekodbindinger må ikke avhenge av lokaliserte skjermenavn (Tasks 4, 10).
5. Nettverksretning og plugin gjenoppstart: kommandoer må ikke utføres to ganger (Tasks 6, 8).

## Delte kontrakter og avhengighet

Oppgaver 1–3 etablere utførelse og installasjon. Oppgaver 4–5 bygge UIA. Oppgaver 6–7 leverer plugins. Oppgaver 8–9 gir mobil tilgang. Oppgaver 10–12 leverer tastaturadapter og filterintegrasjon. Task 13 validerer hele produktet.

Hver oppgave følger en feiltest → minimal implementering → pass-test syklus. En oppgave er fullført bare når den er tilgjengelig gjennom søknaden og sjekkene passerer. Scaffolds, mock adaptere og dokumentasjon alene tilfredsstiller ikke en leverbar.

### Oppgave 1: Typed verifisering og kompatibel utholdenhet

**Filene:** Skaper `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Modifisert `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Grensesnitt:** Produksjon `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` og `VerifyAsync(ActionStep, CancellationToken)`, Begge returnerer `Task<ActionVerificationResult>`. Legg til valgfri serialiserbar `VerificationOptions` Med flagg og timeout (100–60000 Publisere trykte per-step resultater inkludert kjøre ID, tile ID og steg indeks uten aksjonsbelastninger.

- [ ] Skriv sjekker: legacy JSON beholder eksisterende handling numeriske verdier; fraværende alternativer inaktiverer verifikasjon; invalid timeout feil validasjon; forhåndsvisning mislykkes utføreren invitasjon; mislykkede postbetingelse aldri produserer verifisert suksess; ustøttet utføreren er Unverifierbar; avbestillingen stopper verifisering og etterfølgende trinn.
- [ ] Run `dotnet run --project tests/Aivana.Tests -c Release`; bekreft nye kontroller mislykkes før implementering.
- [ ] Implementeringskontrakter, validering, kloning/snapshot bevaring og MacroRunner integrasjon. Respektere OnError stop/continue politikk; beholde eksisterende eksekusjonsfeedback-abonnenter.
- [ ] Kjør kjernestester; krever alle sjekker pass.

### Oppgave 2: Windows verifiseringsleverandører og resultat UI

**Filene:** Skaper `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Modifisert `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Testing av inngangspunkter.

**grensesnitt: ** WindowsActionVerification implementerer oppgave 1 leverandør ved hjelp av eksisterende lyd, skjerm, prosess/vindu og OBS klienter. Forflytningstester tilgjengelighet og identifiserer; verifisering sammenligner ønsket tilstand etter begrenset spørreundersøkelser. Produksjon av kontroller resulterer i snapshots forbruket av WPF historievisning.

- [ ] Legg til mislykkede tester for ikke tilgjengelige eksplicite audio/display IDs, OBS timeout, feilaktig observert tilstand, ukjente kortskudd effekter, følsom betalingsløsning og kansellert spørreundersøkelser, ved hjelp av injiserbare lesere.
- [ ] Implementere ekte lesesikker verifisering for absolutt volum, myk, absolutt lys, prosess/vindue klarhet og støttet OBS kommandoer. Relative/toggle handlinger krever en fanget pre-state; ellers rapporter Unverifiable.
- [ ] Legg til trinnsverifiseringskontroller og sikker utførelseshistorie som viser status, feil krav og gjenoppretting tekst.
- [ ] Run core/Windows sjekker og WPF røyk med injiserte statlige leverandører; ingen ekte lys eller lyd skriver.

### Oppgave 3: Guided workspace skapelse

**Filene:** Skaper `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modifiser palett og dashboard inngangspunkter.

**grensesnitt: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; utvalg inneholder template ID, valgte utførbare veier, URL-er, lyd/display ID, OBS scene og utvalgte bånd. Discovery returnerer navngitte kandidater med stabile ID og tilgjengelighet. Planlegging av konfigurasjonsrevisjon for konfliktdeteksjon.

- [ ] Legg til mislykkede tester for alle fem søkte trollfamilier, uavhengige skudd, manglende nødvendige mål, kansellering uten mutasjon og spar mot endret revision.
- [ ] Oppdag applikasjoner gjennom registrerte app-spor og Start-meny-kort; enheter gjennom eksisterende leverandører; scener gjennom lesing alene OBS spørreundersøkelser. Hold manuell utvalg tilgjengelig for ukjente apper.
- [ ] Implement Velg arbeidsflyt → Velg mål → Velg kontroller → Forhåndsvisning/validering → Spare. Generere redigerbare profiler og aldri kjøre under installasjonen.
- [ ] Legg til virkelige WPF magisk røyke sjekker for fullføring/annull/konflikt; kjør alle tester.

### Oppgave 4: UIA selektorer og begrensede handlinger

**Filene:** Skaper `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modifiserer modeller, konfigurasjon validering og WinActionExecutor.

**Grensesnitt:** Appen `ActionKind.UiAutomation`; `UiElementSelector` Store prosess, automatisering ID, kontrolltype og bestilte forfedre kriterier. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Tilbake `Task`; Operasjoner Invoke, SetValue, Select, Toggle, Focus og WaitState..

- [ ] Legg til mislykkede tester mot et dedikert testvindu for overføring/resize, duplisert kamper, savnede elementer, ustøttet mønstre, deaktiverte kontroller, passordverdier, Unicode-etiketter og timeout/annulling.
- [ ] Implementering av UIA på en dedikert arbeidstaker med begrensede forespørsler og prosessmåling. Potensielt hengende leverandører kjører bak en tilbakekallelig arbeidsgrense; å forlate en forespørsel må ikke forlates bivirkninger.
- [ ] Unngå passordverdier; rapporter tilgang/integritet begrensninger i stedet for å overskride dem.
- [ ] Run Windows sjekker ved hjelp av isolerte fixturer; legge til enum verdier bare og rerun arv persistens sjekker.

### Task 5: UIA picker og gjennomgått opptak

**Filer:** Skap `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modifisere handling katalog, redaktør og innspillingsintegrasjon; opprette UIA WPF røyk sjekker.

**grensesnitt: ** Picker returnerer en `UiElementSelector`; registreren returnerer foreslåtte UIA trinn og skriver aldri direkte til en spart profil. Test-selektorkommandoen returnerer match count uten invitasjon.

- [ ] Legg til mislykkede sjekker: picker utelukker MAKRIVO og passord-verdi fanget; registrer/anneller eksisterende profil uendret; tvetydig forhåndsvisning rapporter match count; akseptere registrerte trinn skaper en redigerbar utkast.
- [ ] Implementering element highlight/picker, driftsspesifikke parametere, selektor forhåndsvisning og registreringsvindu. Oppbevar eksisterende kortskudd oppførsel.
- [ ] Legg til kataloginnlegg og palett tilgang; valider redigeringsfeil før du sparer.
- [ ] Run Windows og WPF røyk sjekker på fastsettelsen; sjekke picker/editor skjermbilder.

### Oppgave 6: Isolert plugin-host og protokoll

**Filene:** Skaper `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Endring av løsningen og handling.

**Grensesnitt:** Version-1 manifest angir pakke-ID/versjon, inngangspunkter, parameter skjemmer, handlinger, statlige leverandører og kapasitetserklæringer. IPC-meldinger inneholder protokollversjon, unik forespørsel ID, metode, JSON-parametre og typiske resultater. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Returnerer et typisk resultat. `ActionKind.Plugin`.

- [ ] Legg til mislykkede tester for uforenlig protokoll, feilformet/overformet (>1 MiB) melding, duplikatforespørsel ID, kansellering, kollisjon, ikke-svarende prosess og manglende handling.
- [ ] Implementere samme-bruker-navnet IPC med 10-sekunder håndshake og konfigurerbar utføringstid kappet på 60 sekunder. Avslutt uansvarlig arbeidere; ikke automatisk spille en innflytelsesaksjon etter å ha startet på nytt.
- [ ] Oppfylle ordninger og tillatte kontrolltjenester. Forklare executable-plugin tillit; prosessisolering er ikke en sandbox.
- [ ] Kjør kjerne-tester og integrasjonstester mot virkelige testplugins prosesser.

### Oppgave 7: Plugin pakker og eksempler integrasjon

**Filene:** Skaper `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, pakkeprøver; modifisere palett/innstillinger.

**Grensesnitt:** Pakkeutstillinger `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` og `UninstallAsync(packageId)`. Revisert pakkeinntegn hasher, avhengigheter, versjon og deklarerte evner.

- [ ] Legg til mislykkede tester for zip traversal, absolutte veier, symlink/reparse fjerning, dupliserte innspill, >100 MiB utvidet data, hash mismatch, downgrade, stale review og disabled import.
- [ ] Implementering forhåndsvisning og eksplisitt tillatelse, avhengighet feil, stages installasjon og oppgradering rollback; unload arbeideren før fjerning.
- [ ] Leverer et ekte eksempel plugin som returnerer en konfigurerbar countdown handling og live status gjennom IPC, inkludert avbestilling; pakke/eksport/installasjon/uninstallasjon gjennom UI.
- [ ] Run pakke/host tester og WPF end-to-end sjekker. Kontroller ingen importerte utførbare ruter før aktivering.

### Task 8: HTTPS mobiltjenester og paring

**Filene:** Skaper `src/Aivana.MobileHost/` ASP.NET Core prosjektet, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Integrasjonstester; modifiserende løsning og kontroll.

**grensesnitt: ** Mobiltjeneste samtaler til kontrolløren gjennom samme brukeren IPC. Ruter: Paring, tillatte profiler/aksjoner, autentisert handling invitasjon, kansellering og WebSocket status. Invitasjon inkluderer en unik kommando-ID; kontrolleren eier begrenset deduplikasjon. Kravene kan tilbakekalles og lagres via Windows beskyttelse.

- [ ] Legg til mislykkede tester for uoppfylt liste/førsel, utløst kode, gjenbrukte koder, tilbakekallede tokener, ulovlig opprinnelse, duplikatkommando, oversikt ramme og oversikt grenser.
- [ ] Implementering av HTTPS-enbart lytter, brukervalgte grensesnitt, lokale sertifisering/trust instruksjoner, en gang 6-digit paring koder utløper etter 120 sekunder, max fem forsøk per kode og token tilbakekallelse. Lagre hashede tilfeldige tokener; aldri logg på bekreftelser.
- [ ] Implementering tillater stabil handling IDs og eksisterende routing; avviser vilkårlig shell, veier og pakke-installasjon kommandoer. Konfigurere eksplicite per-klient rategrenser (10 kommandoer/sec, 30 burst) og 64 KiB melding grense.
- [ ] Verifikere loopback integrasjon tester uten å legge til firewall regler eller tillate LAN tilgang; test lukking og token utholdenhet.

### Oppgave 9: Mobilpanel og skrivebordstjenester

**Filene:** Skaper `src/Aivana.MobileHost/wwwroot/` responsiv panel og `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Legg til nettleser/mobil verifiseringsskripter og WPF Røykdekk.

**grensesnitt: ** Forbruket oppgave 8 ruter bare. Eksponere touch action grid, profil piker, live verdier/progress, kansellering, tilkoblingsstatus og paring. Desktop kontrollerer service start/stop, adresse/sertifiseringsinstruksjoner, tillatte profiler og paired-klient tilbakekalling.

- [ ] Legg til feil integrasjon sjekker for auth flow, stale/missing action IDs, gjenoppkobling uten handling replay, tilbakekalt klient og service stop.
- [ ] Implementering av tilgjengelig responsiv internett ved hjelp av lokale eiendeler, ingen tredjeparts skript eller Internett-avhengigheter. Tokene vises aldri i URL-er.
- [ ] Test nettleser layout på 360/768/1280 CSS px; sjekke virkelige skjermbilder. Kontroller med en ekte telefon/tablet bare etter at brukeren uttrykkelig tillater LAN-tjenesten.
- [ ] Dokumenter og rapporter mobil maskinvare verifisering separat fra nettleser emulering.

### Oppgave 10: Enhetskapasitet og vanlig nøkkelobservasjon

**Filene:** Skaper `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modifiserer RawKeyboardBindingService, KeyboardBinding, validator og enhetsdiagnose; legge til tester.

**grensesnitt: ** `IKeyboardAdapter` viser kapasitet snapshots, stabil enhetsidentitet og fysiske nøkkelrender (skannekode, utvidede flagg, press/utgivelse). Modene skiller observasjon, kartlagt standardinnføring, innfødte SDK og driver-eksklusive. Eksisterende virtuelle nøkkel spesielle bånd opprettholder deres representasjon og migrasjon atferd.

- [ ] Legg til mislykkede tester for gamle bånd, Enter forskjeller, språk/layout uavhengighet, gjentatte nedturer, unplug mens holdes, duplikat-modell ambiguitet og injisert-input avvisning.
- [ ] Utvid observasjon til vanlige fysiske nøkler uten å undertrykke dem. Legg til eksplisitt fange og gjenkoble identitetshåndtering.
- [ ] Vis virkelige adapterfunksjoner og hardwarestet status i enhetens UI; aldri infer Fn/dial støtte fra merkevare.
- [ ] Run core/Windows/WPF sjekker med simulerte rapporter og les-only enhetsinntegnelse.

### Task 11: Anleggsadapter

**Filer:** Skap separate filer under `src/Aivana.Windows/KeyboardAdapters/` for LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog og generisk QmkVia veiledning; endre register/UI og legge til adapter protokoll sjekker.

**grensesnitt:** Konsum oppgave 10 adapterkontrakter. Native biblioteker lastes bare fra validerte apper/sendere; mangel på SDK er en utilgjengelig kapasitet. Belysning og inngangskapasitet er separate.

- [ ] Sjekk de nåværende offisielle SDK-hovedene, redistribueringsbetingelsene, programvareavhengighetene og støttet modeller før du implementerer hver adapter; oppdater kilder og nøyaktige versjoner.
- [ ] Legg til mislykkede tester for SDK fraværende / brudd arkitektur, event press / utgivelse / repetisjon, avkobling, feilaktig respons og ustøttet enhet. Bruk injiserbare innfødte abstraksjoner for deterministiske kontroller.
- [ ] Implementasjon Logitech G-tasten og støttet belysning separat; Razer Krombelysning Plus Synapse Standard-nøkkel kartlegging; SteelSeries GameSense Stats / lys; Wooting Analog utforskning med konfigurerbare grenser/hysteres; QMK/VIA Standard-nøkkelinnstilling guide. Ikke kreve eiendomsinnføring fra APIs.
- [ ] Validerer kompatible virkelige enheter der tilgjengelig. Markere andre implementert/hardware-tester, med modell/firmware bevis; manglende brukbar SDK er en avslørt ekstern gate.

### Oppgave 12: Eksklusiv tastaturfilter bakgrunnen

**Filer:** Skap `src/Aivana.Windows/KeyboardFilter/`, installasjonskilde under `tools/keyboard-filter/`, backend integrasjon tester og implementeringsdokumentasjon. En dedikert signert driverprosjekt er nødvendig hvis ingen egnet lisens bakgrunn passerer evaluering.

**grensesnitt: ** Backend rapporter installert / kompatibel / signert tilstand; viser enhetsspesifikk fange start / stopp, nøkkelrender og nødsituasjon. En separat overvåking gjenoppretter pass-through på kontrolleren tap. Hovedknappen blir aldri eksklusiv ved standard.

- [ ] Evaluerer kandidat førerkort, signert binær kilde og nåværende Windows 11/Secure Boot kompatibilitet. Det er en go/no-go avgjørelse. Ingen kjøretøy under vanlige tester.
- [ ] Skriv kontrakter for filtrering mål, mislykkes, hjerteskudd utløper, tap av IPC, gjenoppkobling, pause / avbestillelse, hold-key frigjøring og disposal.
- [ ] Implementering backend, eksplisitt utvalg/aktivering UI, nødutgivelse og installatør/uninstaller. Ikke erstatte lavnivå-hook/Raw Input timing heuristikker.
- [ ] Validerer i en gjenvunnet testmaskin med to tastaturer: valgt nøkkel undertrykt bare på det valgte enheten; hovedskriving ikke påvirket; crash/suspend/uninstall retur.
- [ ] Gate produksjon fullført på faktiske signert/licensert backend og fysiske tester. Hvis ekstern signering/licensing ikke er tilgjengelig, hold oppgaven ufullstendig med nøyaktig avhengighet bevis.

### Oppgave 13: Integrert verifisering, oversettelser og utgivelse pakke

**Filer:** Legg til `docs/testing/2026-10-12-six-extensions.md`; oppdater relevante DE/EN/FR/IT dokumentasjon, lokaliseringskataloger og røyk sjekk inngangspunkter. Oppbevar andre dokumenter.

- [ ] Integrere alle inngangspunkter og snapshots; fullføre oversettelser ved hjelp av eksisterende 27 språk katalog arbeidsflyt og avsløre unreviewed oversettelser.
- [ ] løp `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, og `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Kontrollere skrivebord/mobil skjermbilder og verifisere 100/150/200 prosent Windows skala, konfigurasjon runde reiser, plugin livssyklus, nettverksrevisjon og ekte tastatur gjenkobling.
- [ ] publisere i en ny katalog med `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; inkluderer partner-tjeneste/host-binarer og lisensmeldinger. Ikke oversett installert app.
- [ ] Rapporten er fullført tester, maskinvaretesterte adaptere og eksterne utgivelsesdører. Alle seks leverandører må oppfylle sine aksepteringskriterier før de ringer hele forespørselen fullstendig.

## Plan for selvrevisjon

Alle seks spesialavdelinger kart til oppgaver 1–12; Oppgaver 13 dekker lokalisering, integrert verifisering og frigjøring. klarhet rapportering, utførelse serialisering, arv konfigurasjon, hemmeligheter, pakke tillit og fysisk-enhet ambiguitet har uttrykkelige eiere og tester. eksterne driver / SDK / maskinvare portene forblir ufullstendige til å bevise, i stedet for å regnes som programvarens suksess.

## Valg av brukeren for gjennomgang

Rekommenderes: Native utførelse i denne chatten, oppgave etter oppgave, fordi de fleste moduler deler kontrakter og den nåværende arbeidsplassen inneholder betydelige eksisterende redigeringer. Delegert utførelse er tilgjengelig hvis brukeren velger det. Sjekk ut denne planen og velg utførelsesmetoden før implementeringen, som kreves av skrivingsplaner.

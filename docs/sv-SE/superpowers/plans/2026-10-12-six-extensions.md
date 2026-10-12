<!-- generated-doc locale=sv-SE source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Utvidgningsplan

> **För agentarbetare:** NÄSTA SUB-SKILL: Använd superpower:exekutionsplaner för inhemska verkställighet eller superpowers:subagent-driven utveckling efter användaren väljer delegation. Följ stegen med checkboxar.

**Syfte: ** Leverera sex integrerade förlängningar och kapacitetsbaserad multi-tillverkare tangentbordstöd.

**Arkitektur:** Förvara kärnan / Windows / WPF Separation. Introduktion av isolerade plugin och mobila serviceprocesser med smal autentiserad IPC; all utförande går genom den befintliga kontroller och MacroRunner quue..

**Tekniska Stack:** C# och .NET 10, WPF, Windows UI Automation, Win32 Raw Input, Dokumenterad leverantör SDK, ASP.NET Core HTTPS/WebSocket, Författare.Json.

**Speglar:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (Godkänd av användaren).

## Globala restriktioner

- Den befintliga konfigurationen måste ladda oförändrad; bifoga åtgärdstyper istället för att ordna sina bestående enumvärden.
- Förvara befintliga profiler, G1/G2 uppdrag, lokal drift och kontofri användning.
- Raw Input Exklusiv avstängning per enhet kräver en riktig tangentbordfilter bakgrundsgräns.
- Importerade utförbara plugins ska inte utföras automatiskt.
- Standard till inaktiverad och begränsa konfigurerade gränssnitt för den mobila tjänsten; ingen klagomål autentisering fallback.
- Shortcut/text leverans är inte ett bevis på att målinriktad ansökan accepterade kommandot.
- Ersätt inte den installerade appen, installera drivrutiner, ändra verkliga tangentbord mappningar eller öppna nätverksåtkomst som en incidentell testning.
- Förvara icke-relaterade ändringar. Förplikta inte befintliga delade redigeringar som en del av denna funktion. Stage endast oberoende granskbara funktioner om kommittéer begärs.

## Översikt fokus

1. Duplicerade identiska tangentbordsmodeller: en tveksam återkoppling måste kräva omval (Tasks 10–12).
2. Annullering under UIA/leverantör/plugin/nätverk väntar: ingen åtgärd efter annullering (Task 1, 4, 6, 8).
3. Användare redigerar medan trollkarlen eller förhandsvisningen är öppen: revision missmatch måste förhindra tyst överskrivning (Tasks 3, 7).
4. Språket och den fysiska layouten skiljer sig: Selektorer och skannekodbindningar ska inte bero på lokaliserade displaynamn (Tasks 4, 10).
5. Nätverksretry och plugin omstart: kommandon måste inte tyst utföras två gånger (Tasks 6, 8).

## Delade avtal och beroendeorder

Uppgifter 1–3 upprätthålla genomförande och installation. Uppgifter 4–5 bygga UIA. Uppgifter 6–7 levererar plugins. Uppgifter 8–9 ger mobil tillgång. Uppgifter 10–12 levererar tangentbordadapter och filterintegration. Uppgift 13 validerar hela produkten.

Varje uppgift följer en misslyckad test → minsta genomförande → passande testcykel. En uppgift är klar endast när den kan nås genom ansökan och dess kontroller passerar. Scaffoldar, mockadapter och dokumentation ensam tillfredsställer inte en leverans.

### Uppgift 1: Typad verifiering och kompatibel uthållighet

**filer:** Skapa `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; modifierade `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**gränssnitt:** Producera `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` och `VerifyAsync(ActionStep, CancellationToken)`, Båda återvänder `Task<ActionVerificationResult>`. Lägg till valfritt serialiserat `VerificationOptions` med aktiverad flagga och tid (100–60000 ms), standardaktiverad. Publicera tryckta steg-för-resultat inklusive kör ID, tile ID och steg index utan åtgärdsladdningar.

- [ ] Skriva kontroller: arv JSON behåller befintliga åtgärd numeriska värden; frånvarande alternativ inaktiverar verifiering; invalid timout misslyckas validering; förskjutnings misslyckande förhindrar exekutor invokation; misslyckad postcondition aldrig producerar verifierad framgång; okontrollerad exekutor är Overifierbar; annullering stoppar verifiering och efterföljande steg.
- [ ] Kör `dotnet run --project tests/Aivana.Tests -c Release`; bekräfta nya kontroller misslyckas innan implementering.
- [ ] Genomförandeavtal, validering, kloning/snapshot bevarande och MacroRunner integration. Respektera OnError stop/continue policy; bevara befintliga genomförande feedback abonnenter.
- [ ] Kör kärnprov; kräver alla kontroller pass.

### Uppgift 2: Windows verifieringsleverantörer och resultat UI

**filer:** Skapa `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; modifierade `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Testning inträdespoäng.

**Interfaces: ** WindowsActionVerification implementerar Task 1-leverantör med hjälp av befintliga ljud, display, process/fönster och OBS-klienter. Flight test tillgänglighet och identifierare; verifiering jämför önskat tillstånd efter begränsad undersökning. Producera kontrollerna resultat snapshots som konsumeras av WPF historia visning.

- [ ] Lägg till misslyckade tester för otillgängliga uttryckliga ljud/display-ID, OBS timout, felaktigt observerat tillstånd, okända kortskärmseffekter, känslig laddningsförlust och annullerad undersökning, med hjälp av injektabla läsare.
- [ ] Implementera verklig läs-stödda verifiering för absolut volym, mjukhet, absolut ljusstyrka, process/fönsterberedskap och stödda OBS kommandon. Relativa/toggle-åtgärder kräver en fångad pre-state; annars rapporterar Overifierbar.
- [ ] Lägg till stegvisa verifieringskontroller och säker exekutionshistoria som visar status, misslyckade krav och återställande text.
- [ ] Kör kärna/Windows kontroller och WPF rök med injicerade statliga leverantörer; ingen verklig ljusstyrka eller ljud skrivar.

### Uppgift 3: Skapad arbetsplats

**filer:** Skapa `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; modifierar palett och dashboard ingångspunkter.

**Interfaces: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; urvalet innehåller mall-ID, valda utförbara vägar, URL-adresser, ljud/display-ID, OBS scenen och valda länkar. Discovery returnerar namngivna kandidater med stabila ID och tillgänglighet. Förslag till konfigurationsrevision för konfliktdetektion.

- [ ] Lägg till misslyckade tester för alla fem begärda trollkarlfamiljer, oberoende genererade steg, saknar krävande mål, avbryter utan mutation och sparar mot ändrad revision.
- [ ] Upptäck applikationer genom registrerade appvägar och Start-menyn förkortningar; enheter genom befintliga leverantörer; scener genom läs-enbart OBS fråga. Håll manuell urval tillgänglig för okända appar.
- [ ] Implement Välj arbetsflöde → Välj mål → Välj kontroller → Förhandsvisning/validering → Spara. Generera redigerbara profiler och kör aldrig under installationen.
- [ ] Lägg till verkliga WPF trollkontroll för fullföljning/annullering/konflikt; kör alla tester.

### Uppgift 4: UIA-selektorer och begränsade åtgärder

**filer:** Skapa `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modifiera modeller, konfiguration validation och WinActionExecutor.

**gränssnitt:** Appendera `ActionKind.UiAutomation`; `UiElementSelector` Försäljningsprocess, automatisering ID, kontrolltyp och beställda förfäder kriterier. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` återvänder `Task`; Operationer Invoke, SetValue, Select, Toggle, Focus och WaitState..

- [ ] Lägga till misslyckade tester mot ett dedikerat testfönster för omplacering/resize, dubbla matcher, saknade element, understödda mönster, inaktiverade kontroller, lösenordvärden, Unicode-etiketter och timout/annullering.
- [ ] Implementera UIA på en dedikerad arbetare med begränsade förfrågningar och processmålning. Potentiellt hängande leverantörer körs bakom en återanvändbar arbetarräning; avstå från en begäran får inte lämna utomordnade biverkningar.
- [ ] Utesluta lösenord; rapportera åtkomst/integritetsbegränsningar istället för att överskrida dem.
- [ ] Kör Windows kontroller med isolerade fixeringar; lägga till enumvärden endast och rerun arv persistens kontroller.

### Uppgift 5: UIA picker och granskad inspelning

**Filer: ** Skapa `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modifiera åtgärdskatalog, redigerare och inspelningsintegration; skapa UIA WPF rökcheck.

**Interfaces: ** Picker returnerar en `UiElementSelector`; inspelaren returnerar de föreslagna UIA-stegen och skriver aldrig direkt till en sparat profil. Test-selektorkommandot returnerar match räkning utan inbjudan.

- [ ] Lägg till misslyckade kontroller: Pickers utesluter MAKRIVO och lösenord-värde fången; rekord/annullerar befintliga profiler oförändrade; dubbla förhandsrapporter match räkning; accepterar inspelade steg skapar en redigerbar utkast.
- [ ] Implementation element highlight/picker, operationspecifika parametrar, selektor förhandsvisning och inspelningsfönstret. Håll kvar det befintliga kortskärmshandtaget.
- [ ] Lägg till katalogen och palettåtkomst; bekräfta redaktörsfel innan du sparar.
- [ ] Kör Windows och WPF rökkontroll på fästningen; inspekterar picker/redigerare skärmdumpar.

### Uppgift 6: Isolerad plugin-host och protokoll

**filer:** Skapa `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; modifierad lösning och åtgärd.

**gränssnitt:** Version-1 manifest anger paket-ID/version, ingångspunkt, parametrar scheman, åtgärder, statliga leverantörer och kapacitetsdeklarationer. IPC-meddelanden innehåller protokollversion, unik begäran ID, metod, JSON-parameter och tryckt resultat. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` returnerar ett typiskt resultat.Add `ActionKind.Plugin`.

- [ ] Lägg till misslyckade tester för oförenliga protokoll, felformade/överförda (>1 MiB) meddelanden, duplicerad begäran ID, uppsägning, kollaps, oansvarig process och saknad åtgärd.
- [ ] Implementera samma-användarnamn-rör IPC med 10-sekund handshake och konfigurerbar utförande tid kapad på 60 sekunder. Avsluta oansvarig arbetare; spela inte automatiskt en in-flyg åtgärd efter att ha startat om.
- [ ] Genomföra scheman och tillåtna kontroller. Förklara executable-plugin förtroende; processisoleringen är inte en OS sandbox.
- [ ] Kör kärnprövningar och integrationstester mot verkliga testprocesser.

### Uppgift 7: Plugin-paket och exempelintegration

**filer:** Skapa `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, paketprov; ändra palett/inställningar.

**gränssnitt:** Paketet exponeras `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` och `UninstallAsync(packageId)`. Översikt paket rekord hashar, beroende, version och deklarerade kapacitet.

- [ ] Lägga till misslyckade tester för zip traversal, absoluta vägar, symlink/reparse escape, duplicate entries, >100 MiB utökade data, hash missmatch, downgrade, stallöversikt och funktionshindrade import.
- [ ] Implementation förhandsvisning och uttrycklig aktivering, beroendefel, stegvis installation och uppgradering rollback; avlastning arbetare före avlägsnande.
- [ ] Leverera ett verkligt exempel plugin som returnerar en konfigurbar räkning åtgärd och live-status via IPC, inklusive avbokning; paket / export / installera / avinstallera via UI.
- [ ] Run package/host tester och WPF end-to-end kontroller. Kontrollera att det inte finns några importerade körningar innan aktivering.

### Uppgift 8: HTTPS mobil service och paring

**filer:** Skapa `src/Aivana.MobileHost/` ASP.NET kärnprojekt, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Integrationstester; modifierad lösning och kontroll.

**gränssnitt: ** Mobiltjänst samtal till kontroller via samma användare IPC. Router: parande, tillåtna profiler/åtgärder, autentiserad åtgärd inbjudan, uppsägning och WebSocket status. Inbjudan inkluderar en unik kommandotod; kontrollören äger begränsad deduplicering. Uppgifterna kan återkallas och lagras via Windows skydd.

- [ ] Lägg till misslyckade tester för ofullständig list/exekution, utgångsskod, återanvänd kod, återkallad token, invalid ursprung, dubbelkommandon, överskattade ram och översvämningsgränser.
- [ ] Implementera HTTPS-enbart lyssnare, användarvalda gränssnitt, lokala certifiering/förtroende instruktioner, en gång 6-digit parande koder upphör efter 120 sekunder, max fem försök per kod och token återkallelse. Lagra hashade slumpmässiga tokens; aldrig loggkrediter.
- [ ] Implementation tillåter listade stabila åtgärd ID och befintlig routing; vägra vilseledande shell, vägar och paket-installation kommandon. Konfigurera uttryckliga gränser per kund (10 kommandon/sek, 30 burst) och 64 KiB-meddelande gränsen.
- [ ] Kontrollera loopback-integrationstester utan att lägga till brandväggregler eller möjliggöra LAN-åtkomst; testning av stängning och token persistens.

### Uppgift 9: Mobilpanel och skrivbordstjänstkontroll

**filer:** Skapa `src/Aivana.MobileHost/wwwroot/` responsiv panel och `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Lägg till webbläsare/mobile verifieringsskript och WPF Rök täckning.

**gränssnitt: ** Förbrukningsuppgift 8 rutor endast. Exponera touch action gränssnitt, profil pick, levande värden/progress, avbryta, anslutningsstatus och paring. Desktop kontrollerar service start/stopp, adress/certifiering instruktioner, tillåtna profiler och paired-klient återkallande.

- [ ] Lägg till misslyckade integrationskontroller för auth flöde, stale/missing action IDs, återkoppling utan åtgärd replay, återkallad klient och service stop.
- [ ] Implementera tillgängligt responsivt UI med hjälp av lokala tillgångar, inga tredjepartsskript eller internetberoende. Token visas aldrig i URL:er.
- [ ] Testar webbläsarens layout på 360/768/1280 CSS px; inspekterar verkliga skärmdumpar. Kontrollera med en riktig telefon/tablett endast efter att användaren uttryckligen har aktiverat LAN-tjänsten.
- [ ] Dokument och rapportering av mobila hårdvaru verifiering separat från webbläsare emulering.

### Uppgift 10: Enhetsförmåga och vanlig nyckelobservation

**filer:** Skapa `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modifiera RawKeyboardBindingService, KeyboardBinding, validator och enhetsdiagnos; lägga till tester.

**gränssnitt: ** `IKeyboardAdapter` visar kapacitetsinspelningar, stabil enhet identitet och fysiska nyckelgränser (skanningskod, utökade flaggor, press/utgivning). Moderna skiljer observation, mappad standardinspelning, inhemska SDK och drivrutiner exklusiv. De befintliga virtuella nyckelförbindelserna behåller sin representation och migrationsbeteende.

- [ ] Lägg till misslyckade tester för gamla bindningar, Enter skillnader, språk / layout oberoende, upprepade nedgångar, upplug medan hålls, dubbelmodell ambiguitet och injektionsavslag.
- [ ] Utöka observationen till vanliga fysiska nycklar utan att undertrycka dem. Lägg till explicit fångst och återkoppling av identitetshandling.
- [ ] Visa den faktiska adapternas kapacitet och hårdvara-testad status i enhetens UI; aldrig ange Fn/dial-stöd från varumärken.
- [ ] Run core/Windows/WPF kontrollerar med simulerade rapporter och läs-enbart enhetsförteckning.

### Uppgift 11: Tillverkare adapter

**Filer:** Skapa separata filer under `src/Aivana.Windows/KeyboardAdapters/` för LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog och generisk QmkVia guide; modifiera registret/UI och lägga till adapterprotokoll kontroller.

**Interfaces: ** Konsum Task 10 Adapteravtal. Inhemska bibliotek laddas endast från validerade app/säljare; SDK-brist är en otillgänglig kapacitet. Belysning och ingångskapacitet är separat.

- [ ] Kontrollera aktuella officiella SDK-högtalare, omfördelningskrav, programvaruberoende och stödda modeller innan du implementerar varje adapter; inspelningskällor och exakta versioner.
- [ ] Lägga till misslyckade tester för SDK frånvarande / bristande arkitektur, händelse press / utgivning / upprepning, avkoppling, felaktig respons och okontrollerad enhet. Använd injektiva native-call abstraktioner för determinist kontroller.
- [ ] genomförandet Logitech G-nyckel och stödjande belysning separat; Razer Chroma ljus plus Synapse Standardnyckel kartläggning; SteelSeries GameSense Läge / Ljus; Wooting Analoga undersökningar med konfigurerbara gränser/hysteres; QMK/VIA Standardnyckelinställningshandledning. kräva inte äganderätt från belysning APIs.
- [ ] Validerar kompatibla verkliga enheter där tillgängliga. Markera andra implementerade/hardware-testerade, med modell/firmware bevis; saknas användbar SDK är en avslöjad extern port.

### Uppgift 12: Exklusiv tangentbord filter bakgrunds

**Filer: ** Skapa `src/Aivana.Windows/KeyboardFilter/`, installations källor under `tools/keyboard-filter/`, bakgrundsintegrationstester och implementeringsdokumentation. Ett dedikerat undertecknat projekt krävs om ingen lämplig licensierad backend passerar utvärderingen.

**Interfaces: ** Backend rapporter installerad/kompatibel/signat tillstånd; exponerar enhetsspecifik upptagning start/stopp, nyckelgränser och nöd release. En separat övervakning återställer pass-through på kontrollör förlust. Den huvudsakliga tangentbordet blir aldrig exklusivt.

- [ ] Utvärdera kandidat körlicens, undertecknade binära ursprung och nuvarande Windows 11/Secure Boot kompatibilitet. Det är ett go/no-go beslut. Ingen förare under vanliga tester.
- [ ] Skriv kontraktstester för filter mål, misslyckande, hjärtslag upphör, förlust av IPC, återkoppling av tvivel, paus / avbokning, håll nyckel release och borttagning.
- [ ] Implementation backend, explicit urval/aktivering UI, nöd release och installatör/uninstaller. Inte ersätta låg-nivå-höghet/Raw Input timing heuristik.
- [ ] Valideras i en återställbar testmaskin med två tangentbord: valda nycklar stängs endast på den valda enheten; huvudskrivning inte påverkas; crash/suspend/uninstall återställer input.
- [ ] Gate produktion avslutad på faktiska undertecknade / licensierade bakgrunder och fysiska tester. Om extern undertecknande/licens inte är tillgänglig, behåll uppgiften ofullständig med exakt beroende bevis.

### Uppgift 13: Integrerad verifiering, översättning och släpppaket

**Filer:** Lägg till `docs/testing/2026-10-12-six-extensions.md`; uppdatering av relevant DE/EN/FR/IT dokumentation, lokaliseringskategorier och rökcheckning. Förvara andra dokumentation.

- [ ] Integrera alla ingångspunkter och snapshots; komplettera översättningar med hjälp av befintliga 27-språkiga katalogarbetsflöden och avslöja okänd översättning.
- [ ] Running `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, och `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Inspektera skrivbord/mobil skärmdumpar och verifiera 100/150/200 procent Windows skala, konfigurationsrunda resor, plugin livscykel, nätverk återkoppling och verklig tangentbord återkopplingen.
- [ ] Publicera i en ny katalog med `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; inkluderar sällskapstjänst / värd binära och licensmeddelanden. Överskriv inte den installerade appen.
- [ ] Rapporten slutfört tester, hårdvaru-testade adaptrar och externa släppgator. Alla sex leverantörer måste uppfylla sina acceptanskriterier innan hela ansökan är fullständig.

## Självöversikt

Alla sex spec avsnitt kart till Uppgifter 1–12; Uppgift 13 täcker lokalisering, integrerad verifiering och frisättning. Klarhet rapportering, verkställande serialisering, arvkonfiguration, hemligheter, förtroende för paket och fysiska enheter har uttryckliga ägare och tester. Externa drivrutiner / SDK / hårdvara dörrar förblir ofullständiga tills de bevisas, snarare än räknas som mjukvaru framgång.

## Utförande val för användarrecension

Rekommenderas: inhemska genomförande i denna chatt, uppgift per uppgift, eftersom de flesta moduler delar avtal och den aktuella arbetsytan innehåller väsentliga befintliga redigeringar. Delegerad genomförande är tillgänglig om användaren väljer det. Se över denna plan och välj utförandemetoden före genomförandet, som krävs av skrivplaner arbetsflöde.

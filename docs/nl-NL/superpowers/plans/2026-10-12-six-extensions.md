<!-- generated-doc locale=nl-NL source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Implementatieplan voor uitbreidingen

> **Voor agentische werkers:** VEREISTE SUB-VAARDIGHEID: Gebruik superkrachten: uitvoeringsplannen voor native uitvoering of superkrachten: subagent-gestuurde ontwikkeling nadat de gebruiker delegatie heeft geselecteerd. Volg stappen met selectievakjes.

**Doel:** Leveren van zes geïntegreerde extensies en op mogelijkheden gebaseerde toetsenbordondersteuning van meerdere fabrikanten.

**Architectuur:** Behoud de scheiding Kern / Windows / WPF. Introduceer geïsoleerde plug-in- en mobiele serviceprocessen met beperkte, geverifieerde IPC; alle uitvoering verloopt via de bestaande controller en MacroRunner-wachtrij. Hardwareadapters adverteren gedetecteerde mogelijkheden in plaats van afgeleide merkbrede ondersteuning.

**Tech Stack: ** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, gedocumenteerde leveranciers-SDK's, ASP.NET Core HTTPS/WebSocket, Systeem.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (goedgekeurd door gebruiker).

## Mondiale beperkingen

- Bestaande configuratie moet ongewijzigd worden geladen; voeg actiesoorten toe in plaats van de blijvende enum-waarden opnieuw te ordenen.
- Behoud bestaande profielen, G1/G2-toewijzingen, lokale bediening en accountvrij gebruik.
- Raw Input biedt alleen observatie. Exclusieve onderdrukking per apparaat vereist een daadwerkelijke toetsenbordfilter-backend.
- Geïmporteerde uitvoerbare plug-ins mogen niet automatisch worden uitgevoerd.
- Standaard uitgeschakeld en geconfigureerde interfaces voor de mobiele service beperken; geen fallback voor authenticatie in platte tekst.
- De levering van snelkoppelingen/teksten is geen bewijs dat de doeltoepassing de opdracht heeft geaccepteerd.
- Vervang de geïnstalleerde applicatie niet, installeer geen stuurprogramma's, wijzig geen echte toetsenbordtoewijzingen of open netwerktoegang als een incidentele test.
- Bewaar niet-gerelateerde, niet-vastgelegde wijzigingen. Voer geen bestaande gedeelde bewerkingen uit als onderdeel van deze functie. Voer alleen onafhankelijk te beoordelen feature-hunks uit als commits worden gevraagd.

## Bekijk Focus

1. Dupliceer identieke toetsenbordmodellen: dubbelzinnige herverbinding vereist herselectie (Taken 10–12).
2. Annulering tijdens wachten bij UIA/provider/plug-in/netwerk: geen uitgestelde actie na annulering (Taken 1, 4, 6, 8).
3. Bewerkingen door gebruiker terwijl wizard of pakketvoorbeeld geopend is: niet-overeenkomende revisies moeten stille overschrijving voorkomen (taken 3, 7).
4. Taal en fysieke lay-out verschillen: selectors en scancodebindingen mogen niet afhankelijk zijn van gelokaliseerde weergavenamen (Taken 4, 10).
5. Netwerk opnieuw proberen en plug-in opnieuw opstarten: opdrachten mogen niet twee keer stilzwijgend worden uitgevoerd (Taken 6, 8).

## Gedeelde contracten en afhankelijkheidsvolgorde

Taken 1–3 uitvoering en inrichting vaststellen. Taken 4–5 UIA bouwen. Taken 6–7 plugins opleveren. Taken 8–9 leveren mobiele toegang. Taken 10–12 toetsenbordadapters en filterintegratie leveren. Taak 13 valideert het volledige product.

Elke taak volgt een cyclus van falende test → minimale implementatie → slagende test. Een taak is pas voltooid als deze via de applicatie kan worden bereikt en de controles zijn geslaagd. Steigers, nep-adapters en documentatie alleen voldoen niet aan het gewenste resultaat.

### Taak 1: Getypte verificatie en compatibele persistentie

**Bestanden:** `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs` aanmaken; wijzig `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaces:** Produceer `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` en `VerifyAsync(ActionStep, CancellationToken)`, beide retourneren `Task<ActionVerificationResult>`. Voeg optionele serialiseerbare `VerificationOptions` toe met ingeschakelde vlag en time-out (100–60000 ms), standaard uitgeschakeld. Publiceer getypte resultaten per stap, inclusief run-ID, tegel-ID en stapindex zonder actiepayloads.

- [ ] Schrijfcontroles: oudere JSON behoudt bestaande numerieke actiewaarden; afwezige opties schakelen verificatie uit; ongeldige time-out mislukt validatie; preflight-fout verhindert het aanroepen van de uitvoerder; mislukte postconditie levert nooit geverifieerd succes op; niet-ondersteunde uitvoerder is niet verifieerbaar; Annulering stopt de verificatie en de daaropvolgende stappen.
- [ ] Voer `dotnet run --project tests/Aivana.Tests -c Release` uit; bevestig dat nieuwe controles mislukken vóór implementatie.
- [ ] Implementeer contracten, validatie, klonen/bewaren van momentopnamen en MacroRunner-integratie. Respecteer het OnError-beleid voor stoppen/voortzetten; behoud bestaande abonnees van uitvoeringsfeedback.
- [ ] Kerntests uitvoeren; vereisen dat alle controles slagen.

### Taak 2: Windows verificatieproviders en resultaat-UI

**Bestanden:** `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs` aanmaken; wijzig `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, testingangspunten.

**Interfaces:** WindowsActionVerification implementeert taak 1-provider met behulp van bestaande audio-, weergave-, proces-/venster- en OBS-clients. Beschikbaarheid en identificatiegegevens van preflighttests; verificatie vergelijkt de gewenste status na begrensde polling. Produceer momentopnamen van controllerresultaten die worden gebruikt door de geschiedenisweergave WPF.

- [ ] Voeg mislukte tests toe voor niet-beschikbare expliciete audio-/weergave-ID's, OBS time-out, niet-overeenkomende waargenomen status, onbekende snelkoppelingseffecten, weglating van gevoelige payload en geannuleerde polling, met behulp van injecteerbare leesproviders.
- [ ] Implementeer echte read-backed verificatie voor absoluut volume, dempen, absolute helderheid, proces-/venstergereedheid en ondersteunde OBS-opdrachten. Relatieve/schakelacties vereisen een vastgelegde pre-status; rapporteer anders niet verifieerbaar.
- [ ] Voeg controles voor verificatie per stap toe en een geschiedenis van veilige uitvoering met de status, mislukte vereiste en hersteltekst.
- [ ] Voer kern/Windows-controles uit en WPF-rook met geïnjecteerde overheidsaanbieders; geen echte helderheid of audio schrijft.

### Taak 3: Begeleide creatie van werkruimte

**Bestanden:** `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs` maken; wijzig palet- en dashboardingangspunten.

**Interfaces:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; selectie bevat sjabloon-ID, gekozen uitvoerbare paden, URL's, audio-/weergave-ID's, OBS-scène en geselecteerde bindingen. Discovery retourneert benoemde kandidaten met stabiele ID's en beschikbaarheid. Configuratierevisie van conceptrecords voor conflictdetectie.

- [ ] Voeg mislukte tests toe voor alle vijf de gevraagde wizardfamilies, onafhankelijk gegenereerde stappen, ontbrekende vereiste doelen, annuleer zonder mutatie en sla op tegen gewijzigde revisie.
- [ ] Ontdek applicaties via geregistreerde app-paden en snelkoppelingen in het Startmenu; apparaten via bestaande providers; scènes via alleen-lezen OBS-query. Houd handmatige selectie beschikbaar voor onontdekte apps.
- [ ] Implementeren Kies workflow → Selecteer doelen → Kies besturingselementen → Voorbeeld/valideren → Opslaan. Genereer bewerkbare profielen en voer deze nooit uit tijdens de installatie.
- [ ] Voeg daadwerkelijke WPF wizard-rookcontroles toe voor voltooiing/annuleren/conflict; voer alle tests uit.

### Taak 4: UIA-selectors en begrensde acties

**Bestanden:** `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs` aanmaken; modellen aanpassen, configuratievalidatie en WinActionExecutor.

**Interfaces:** Toevoegen `ActionKind.UiAutomation`; `UiElementSelector` slaat proces-, automatiserings-ID, controletype en geordende vooroudercriteria op. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` retourneert `Task`; bewerkingen Invoke, SetValue, Select, Toggle, Focus en WaitState. Selectorresolutie moet precies één in aanmerking komend element retourneren.

- [ ] Voeg mislukte tests toe aan een speciaal testvenster voor verplaatsing/grootte wijzigen, dubbele overeenkomsten, ontbrekende elementen, niet-ondersteunde patronen, uitgeschakelde bedieningselementen, wachtwoordwaarden, Unicode-labels en time-out/annulering.
- [ ] Implementeer UIA op een specifieke medewerker met begrensde verzoeken en procestargeting. Potentieel hangende providers lopen achter een herstartbare werknemersgrens; het verlaten van een verzoek mag geen neveneffecten in de wachtrij achterlaten.
- [ ] Wachtwoordwaarden uitsluiten; rapporteer toegangs-/integriteitsbeperkingen in plaats van deze te omzeilen.
- [ ] Voer Windows-controles uit met geïsoleerde armaturen; voeg alleen enum-waarden toe en voer verouderde persistentiecontroles opnieuw uit.

### Taak 5: UIA-kiezer en beoordeelde opname

**Bestanden:** Maak `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; actiecatalogus, editor en recorderintegratie wijzigen; maak UIA WPF-rookcontroles.

**Interfaces:** Picker retourneert een `UiElementSelector`; recorder retourneert voorgestelde UIA-stappen en schrijft nooit rechtstreeks naar een opgeslagen profiel. De opdracht Test-selector retourneert het aantal overeenkomsten zonder aanroep.

- [ ] Mislukte controles toevoegen: kiezer sluit MAKRIVO en vastleggen van wachtwoordwaarden uit; opnemen/annuleren laat het bestaande profiel ongewijzigd; dubbelzinnige voorbeeldrapporten, aantal wedstrijden; Door opgenomen stappen te accepteren, ontstaat een bewerkbaar concept.
- [ ] Markering/kiezer van werktuigelementen, bewerkingsspecifieke parameters, selectorvoorbeeld en recorderoverzichtsvenster. Behoud het bestaande gedrag van de snelkoppelingsrecorder.
- [ ] Catalogusitems en palettoegang toevoegen; valideer editorfouten voordat u deze opslaat.
- [ ] Voer Windows en WPF rookcontroles uit op het armatuur; inspecteer schermafbeeldingen van kiezer/editor.

### Taak 6: Geïsoleerde plug-inhost en protocol

** Bestanden: ** `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; oplossing en actieverzending wijzigen.

**Interfaces:** Version-1 manifest specificeert pakket-ID/-versie, toegangspunt, parameterschema's, acties, statusproviders en capaciteitsdeclaraties. IPC-berichten bevatten de protocolversie, het unieke verzoek-ID, de methode, JSON-parameters en het getypte resultaat. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` retourneert een getypt resultaat. Voeg `ActionKind.Plugin` toe.

- [ ] Voeg mislukte tests toe voor incompatibel protocol, verkeerd opgemaakt/te groot bericht (>1 MiB), dubbele aanvraag-ID, annulering, crash, niet-reagerend proces en ontbrekende actie.
- [ ] Implementeer IPC voor dezelfde gebruiker met een 10-tweede handshake en een configureerbare uitvoeringstime-out met een maximum van 60 seconden. Beëindig niet-reagerende werknemer; speel een actie tijdens de vlucht niet automatisch opnieuw na het opnieuw opstarten.
- [ ] Schema's en toegestane controllerservices afdwingen. Het vertrouwen in uitvoerbare plug-ins uitleggen; procesisolatie is geen OS-sandbox.
- [ ] Voer kerntests en integratietests uit tegen echte testplug-inprocessen.

### Taak 7: Pluginpakketten en voorbeeldintegratie

**Bestanden:** `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/` maken, pakkettests; wijzig palet-/instellingenitems.

**Interfaces:** Pakketservice toont `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` en `UninstallAsync(packageId)`. Beoordeelde pakketrecords-hashes, afhankelijkheden, versie en gedeclareerde mogelijkheden.

- [ ] Voeg mislukte tests toe voor zip-traversal, absolute paden, symlink/reparse-escape, dubbele invoer, >100 MiB uitgebreide gegevens, hash-mismatch, downgrade, verouderde beoordeling en uitgeschakelde import.
- [ ] Implementeer preview en expliciete inschakeling, afhankelijkheidsfouten, gefaseerde installatie en terugdraaien van upgrades; Ontlaad de werknemer voordat deze wordt verwijderd.
- [ ] Lever een echte voorbeeldplug-in die een configureerbare aftelactie en livestatus retourneert via IPC, inclusief annulering; verpakken/exporteren/installeren/verwijderen via de gebruikersinterface.
- [ ] Voer pakket-/hosttests en WPF end-to-end-controles uit. Controleer of er geen geïmporteerde uitvoerbare uitvoeringen zijn voordat deze wordt ingeschakeld.

### Taak 8: HTTPS mobiele service en koppeling

**Bestanden:** `src/Aivana.MobileHost/` ASP.NET-kernproject, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, integratietests maken; oplossing en controller aanpassen.

**Interfaces:** Mobiele service praat met de controller via IPC van dezelfde gebruiker. Routes: koppelen, toegestane profielen/acties, geauthenticeerde actie-aanroep, annulering en WebSocket-status. Aanroep omvat een unieke opdracht-ID; controller is eigenaar van begrensde deduplicatie. Inloggegevens zijn intrekbaar en worden opgeslagen via Windows-beveiliging.

- [ ] Voeg mislukte tests toe voor ongepaarde opsomming/uitvoering, verlopen code, hergebruikte code, ingetrokken token, ongeldige oorsprong, dubbele opdracht, te groot frame en overstromingslimieten.
- [ ] Implementeer een HTTPS-only luisteraar, door de gebruiker geselecteerde interfaces, lokale certificaatinrichting/vertrouwensinstructies, eenmalige 6-cijferige koppelingscodes die verlopen na 120 seconden, maximaal vijf pogingen per code en tokenintrekking. Bewaar gehashte willekeurige tokens; log nooit inloggegevens in.
- [ ] Implementeer stabiele actie-ID's op de toelatingslijst en bestaande wachtrijroutering; weiger willekeurige shell-, paden- en pakketinstallatieopdrachten. Configureer expliciete snelheidslimieten per client (10-opdrachten/sec, 30 burst) en 64 KiB-berichtlimiet.
- [ ] Loopback-integratietests verifiëren zonder firewallregels toe te voegen of LAN-toegang in te schakelen; test afsluiten en tokenpersistentie.

### Taak 9: Bedieningselementen voor mobiele paneel- en desktopservices

**Bestanden:** Creëer `src/Aivana.MobileHost/wwwroot/` responsief paneel en `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; voeg browser-/mobiele verificatiescripts en WPF rookdekking toe.

**Interfaces:** Gebruik alleen Taak 8-routes. Toon aanraakactieraster, profielkiezer, live waarden/voortgang, annuleren, verbindingsstatus en koppeling. Desktop beheert het starten/stoppen van de service, adres-/certificaatinstructies, toegestane profielen en intrekking van gekoppelde clients.

- [ ] Voeg mislukte integratiecontroles toe voor de authenticatiestroom, verouderde/ontbrekende actie-ID's, opnieuw verbinding maken zonder actieherhaling, ingetrokken client en servicestop.
- [ ] Implementeer een toegankelijke, responsieve gebruikersinterface met behulp van lokale middelen, zonder scripts van derden of internetafhankelijkheden. Tokens verschijnen nooit in URL's.
- [ ] Browserindeling testen op 360/768/1280 CSS px; inspecteer daadwerkelijke schermafbeeldingen. Verifieer alleen met een echte telefoon/tablet nadat de gebruiker de LAN-service expliciet heeft ingeschakeld.
- [ ] Documenteer en rapporteer mobiele hardwareverificatie afzonderlijk van browseremulatie.

### Taak 10: Apparaatmogelijkheden en observatie met gewone toetsen

**Bestanden:** `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs` aanmaken; wijzigen van RawKeyboardBindingService, KeyboardBinding, validator en apparaatdiagnose; testen toevoegen.

**Interfaces:** `IKeyboardAdapter` maakt snapshots van mogelijkheden, stabiele apparaatidentiteit en fysieke sleutelranden (scancode, uitgebreide vlaggen, indrukken/vrijgeven) zichtbaar. Modi onderscheiden observatie, in kaart gebrachte standaardinvoer, native SDK en exclusief stuurprogramma. Bestaande speciale bindingen voor virtuele sleutels behouden hun representatie- en migratiegedrag.

- [ ] Voeg mislukte tests toe voor oude bindingen, voer onderscheidingen in, taal-/lay-outonafhankelijkheid, herhaalde downs, ontkoppelen tijdens vasthouden, dubbelzinnigheid van duplicaatmodellen en afwijzing van geïnjecteerde invoer.
- [ ] Breid observatie uit naar gewone fysieke sleutels zonder ze te onderdrukken. Voeg expliciete vastlegging toe en verbind identiteitsverwerking opnieuw.
- [ ] Toon actuele adaptermogelijkheden en niet-geteste hardwarestatus in de gebruikersinterface van het apparaat; Leid nooit Fn/dial-ondersteuning af van merknamen.
- [ ] Voer core/Windows/WPF-controles uit met gesimuleerde rapporten en alleen-lezen apparaatopsomming.

### Taak 11: Fabrikantadapters

**Files:** Maak afzonderlijke bestanden onder `src/Aivana.Windows/KeyboardAdapters/` voor LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog en generieke QmkVia-begeleiding; wijzig het register/UI en voeg adapterprotocolcontroles toe.

**Interfaces:** Consumeer-taak 10-adaptercontracten. Native bibliotheken worden alleen geladen vanaf gevalideerde app-/leverancierslocaties; SDK-afwezigheid is een niet-beschikbare mogelijkheid. Verlichting en invoermogelijkheden zijn gescheiden.

- [ ] Controleer de huidige officiële SDK-headers, herdistributievoorwaarden, softwareafhankelijkheden en ondersteunde modellen voordat u elke adapter implementeert; bronnen en exacte versies vastleggen.
- [ ] Voeg mislukte tests toe voor ontbrekende/verkeerde architectuur van SDK, pers/vrijgave/herhaling van gebeurtenis, verbinding verbroken, verkeerd opgemaakte reactie en niet-ondersteund apparaat. Gebruik injecteerbare native call-abstracties voor deterministische controles.
- [ ] Implementeer Logitech G-key-invoer en ondersteunde verlichting afzonderlijk; Razer Chroma-verlichting plus Synapse standaardsleuteltoewijzingswizard; SteelSeries GameSense staat/verlichting; Wooting analoge polling met configureerbare drempels/hysteresis; QMK/VIA Richtlijnen voor het instellen van standaardsleutels. Claim geen eigen input van verlichtings-API's.
- [ ] Valideer compatibele echte apparaten, indien beschikbaar. Markeer andere geïmplementeerd/hardware-niet-getest, met bewijs van model/firmware; ontbrekende bruikbare SDK is een onthulde externe poort.

### Taak 12: Exclusieve toetsenbordfilter-backend

**Bestanden:** Maak `src/Aivana.Windows/KeyboardFilter/`, installatiebron onder `tools/keyboard-filter/`, backend-integratietests en implementatiedocumentatie. Een speciaal ondertekend stuurprogrammaproject is vereist als geen geschikte gelicentieerde backend de evaluatie doorstaat.

**Interfaces:** Backend rapporteert geïnstalleerde/compatibele/ondertekende status; onthult apparaatspecifieke start/stop, sleutelranden en noodontgrendeling. Een aparte watchdog herstelt de pass-through bij verlies van de controller. Het hoofdtoetsenbord wordt standaard nooit exclusief.

- [ ] Evalueer licenties voor kandidaat-chauffeurs, ondertekende binaire herkomst en huidige Windows 11/Secure Boot-compatibiliteit. Leg een go/no-go-beslissing vast. Geen installatie van stuurprogramma's tijdens gewone tests.
- [ ] Schrijf contracttests voor filterdoelselectie, fail-open, hartslagverloop, verlies van IPC, dubbelzinnigheid opnieuw verbinden, pauzeren/annuleren, vrijgave van vastgehouden sleutel en verwijdering.
- [ ] Implementeer backend, expliciete selectie/activatie-UI, noodontgrendeling en installer/uninstaller. Vervang de low-level-hook/Raw Input-timingheuristieken niet.
- [ ] Valideren in een herstelbare testmachine met twee toetsenborden: geselecteerde toets alleen onderdrukt op geselecteerd apparaat; hoofdtypering blijft onaangetast; crash/suspend/uninstall herstelt de invoer.
- [ ] Voltooiing van de gate-productie op daadwerkelijke ondertekende/gelicentieerde backend en fysieke tests. Als externe ondertekening/licentie niet beschikbaar is, zorg er dan voor dat de taak onvolledig blijft met nauwkeurig bewijs van afhankelijkheid.

### Taak 13: Geïntegreerd verificatie-, vertalingen- en releasepakket

**Bestanden:** Voeg `docs/testing/2026-10-12-six-extensions.md` toe; relevante DE/EN/FR/IT-documentatie, lokalisatiecatalogi en rookcontrole-ingangspunten bijwerken. Bewaar andere documentatiebewerkingen.

- [ ] Integreer alle toegangspunten en snapshots; voltooi vertalingen met behulp van de bestaande catalogusworkflow in de 27-taal en maak niet-beoordeelde vertalingen openbaar.
- [ ] Voer `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` en `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test` uit.
- [ ] Inspecteer desktop/mobiele schermafbeeldingen en verifieer 100/150/200 procent Windows schaling, configuratierondes, levenscyclus van plug-ins, netwerkintrekking en opnieuw verbinding maken met het echte toetsenbord.
- [ ] Publiceer in een nieuwe artefactmap met `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; omvatten begeleidende service/host binaire bestanden en licentiekennisgevingen. Overschrijf de geïnstalleerde app niet.
- [ ] Rapporteer voltooide tests, hardware-ongeteste adapters en externe release-poorten. Alle zes de deliverables moeten aan hun acceptatiecriteria voldoen voordat het gehele verzoek als voltooid kan worden beschouwd.

## Plan zelfbeoordeling

Alle zes specificatiesecties zijn toegewezen aan Taken 1–12; Taak 13 heeft betrekking op lokalisatie, geïntegreerde verificatie en vrijgave. Gereedheidsrapportage, serialisatie van uitvoering, verouderde configuratie, geheimen, pakketvertrouwen en ambiguïteit van fysieke apparaten hebben expliciete eigenaren en tests. Externe driver/SDK/hardware-poorten blijven onvolledig totdat ze zijn bewezen, in plaats van te worden geteld als softwaresucces.

## Uitvoeringskeuze voor gebruikersbeoordeling

Aanbevolen: native uitvoering in deze chat, taak voor taak, omdat de meeste modules contracten delen en de huidige werkruimte substantiële bestaande bewerkingen bevat. Gedelegeerde uitvoering is beschikbaar als de gebruiker daarvoor kiest. Controleer dit plan en selecteer vóór de implementatie de uitvoeringsmethode, zoals vereist door de workflow voor het schrijven van plannen.

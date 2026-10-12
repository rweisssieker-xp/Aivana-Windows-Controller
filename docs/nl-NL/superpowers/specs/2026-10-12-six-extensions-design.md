<!-- generated-doc locale=nl-NL source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: zes productuitbreidingen en toetsenbordadapters

Datum: 2026-10-12
Status: voorgesteld ontwerp voor gebruikersbeoordeling; de uitvoering is nog niet begonnen.

## Doel
Lever alle zes gevraagde extensies in de bestaande Windows/WPF applicatie, met behoud van bestaande profielen, G1/G2 toewijzingen, lokale bediening en accountvrij gebruik. Breid de toetsenbordintegratie uit naar Razer en Logitech, met een herbruikbare, op mogelijkheden gebaseerde architectuur voor andere fabrikanten. Een fabrikantnaam of toetsenbordindeling is nooit een bewijs dat bedrijfseigen bedieningselementen worden ondersteund.

## Architectuur besluit
Breid Aivana.Core uit met serialiseerbare contracten en validatie, Aivana.Windows met Windows en leveranciersadapters, en Aivana.Desktop met transactionele editors en statusweergaven. Gebruik afzonderlijke begeleidende projecten voor het uitvoeren van geïsoleerde plug-ins en de lokale mobiele panelservice. Vermijd herschrijven of één bevoorrecht proces dat elke extensie host. Bestaande configuratie moet ongewijzigd worden geladen; voeg actiesoorten toe in plaats van de blijvende enum-waarden opnieuw te ordenen.

Overwogen alternatieven: een monolithische implementatie zou de uitvoering van hardware, netwerk en workflow verwarren; het vervangen van WPF zou niet-gerelateerd migratiewerk toevoegen. Modulaire uitbreidingen worden aanbevolen.

## 1. Onafhankelijke toetsenbordmacropad
Bewaar bindingen op apparaatidentiteit plus fysieke scancode en uitgebreide sleutelvlaggen. Voeg expliciete observatie- en exclusieve modi, apparaatdetectie, sleutelregistratie, herhalingsonderdrukking, gebaren, vastgehouden lagen toe en maak diagnostiek opnieuw verbinding. Behoud hoofd-/numpad Voer onderscheidingen in en test Duitse/Amerikaanse lay-outs. Bindingen richten zich op een geselecteerd fysiek toetsenbord; dubbelzinnige herverbindingsidentiteiten vereisen herselectie in plaats van het besturen van een ander apparaat.

Raw Input biedt alleen observatie. Exclusieve onderdrukking per apparaat vereist een daadwerkelijke toetsenbordfilter-backend; correleer nooit mondiale low-level hooks met Raw Input en claim betrouwbare apparaatspecifieke onderdrukking. Zorg voor een waakhond, noodontgrendeling, fail-open gedrag en installer/uninstaller. Leg geen beveiligde desktopinvoer vast. Installatie van stuurprogramma's en activering van de exclusieve modus zijn expliciet afzonderlijke gebruikersacties.

Drivergate: selecteer een ondersteunde, correct gelicentieerde en ondertekende backend die compatibel is met de doelconfiguraties Windows 11, of ontwikkel/onderteken een speciaal filter. Interceptie is een kandidaat om te evalueren, geen toegewijde productieafhankelijkheid: de officiële README beschrijft testen via Windows 10 en afzonderlijke commerciële licenties. Totdat aan deze poort is voldaan, moet de exclusieve modus als niet beschikbaar worden weergegeven; het onderdeel kan niet compleet worden verklaard.

Acceptatie: met twee fysieke toetsenborden voert dezelfde toets een macro uit en produceert geen normale applicatie-invoer alleen op het geselecteerde exclusieve apparaat; andere toetsenbordinvoer blijft onaangetast. Controleer de verbinding, crash, annulering, onderbreken/hervatten en verwijdering van het stuurprogramma op de daadwerkelijke hardware.

## 2. Begeleide inrichting van de werkplek
Voeg een wizard toe voor werkplek, vergadering, streaming, onderzoek en projectwisseling. Ontdek geïnstalleerde applicaties, audioapparaten, monitoren en OBS-scènes. Gebruikers kiezen doelen en bindingen; bekijk een voorbeeld van de resulterende stappen en valideer ontbrekende vereisten. Sla alleen op na beoordeling, zonder dat er acties worden uitgevoerd tijdens de installatie. Gegenereerde profielen blijven bewerkbaar in bestaande editors.

Acceptatie: maak een werkprofiel zonder handmatig uitvoerbare paden of apparaat-ID's te bewerken; bij annulering blijven de instellingen ongewijzigd; ontbrekende software heeft een bruikbare verklaring.

## 3. Geverifieerde uitvoering
Introduceer preflight-controles, getypte resultaten per stap en optionele postcondities. Onderscheid de aangevraagde, voltooide, geverifieerde, mislukte, time-out, geannuleerde en niet-verifieerbare statussen. Controleer audio/microfoon, helderheid, proces-/venstergereedheid en OBS-status met begrensde asynchrone leesbewerkingen. De levering van snelkoppelingen/teksten is geen bewijs dat de doeltoepassing de opdracht heeft geaccepteerd. Externe bijwerkingen zijn niet universeel omkeerbaar; alleen compensatie aanbieden als dit ten uitvoer wordt gelegd.

Acceptatie: losgekoppelde expliciete doelen vallen nooit stilletjes terug; verkeerd waargenomen toestand rapporteert nooit geverifieerd succes; annulering stopt de peiling; fouten identificeren de stap en de herstelactie. In de uitvoeringsgeschiedenis worden tekstpayloads en geheimen weggelaten.

## 4. Plug-ins en deelbare pakketten
Versiebeheerde JSON-manifesten declareren acties, parameterschema's, statusproviders, afhankelijkheden en mogelijkheden. Voer uitvoerbare plug-ins uit in een afzonderlijk proces via een begrensde lokale IPC met handshake, deadlines en annulering. Misvormde berichten en incompatibele versies weigeren; omgaan met crashes zonder de controller te beëindigen. Capaciteitsverklaringen zijn openbaarmaking en bemiddeling, en geen claim van OS-sandboxing.

Bied pakketexport/-importvoorbeelden, inhoudshashes, pad-traversal-bescherming, groottelimieten, versie-/afhankelijkheidscontroles, expliciete inschakeling en upgrade-rollback. Geïmporteerde uitvoerbare plug-ins mogen niet automatisch worden uitgevoerd. Voeg een werkende voorbeeldplug-in toe met een end-to-end installatie/actie/status/verwijderingsstroom. Voor deze release is geen openbare marktplaatsservice vereist.

Acceptatie: een echte voorbeeldintegratie maken, exporteren, installeren en uitvoeren; demonstreer time-out, crashisolatie, afhankelijkheidsfout, traversal-afwijzing en uitgeschakelde import.

## 5. Windows UI-automatisering
Voeg een elementkiezer en begrensde acties toe voor aanroepen, waarde, selectie, schakelen, focus en wachten op een element/status. Persistente selectors met behulp van proces, automatiserings-ID, controletype en vooroudercontext; vermijd opname met alleen coördinaten. Toon het aantal selectorovereenkomsten en test selectors alleen-lezen. Wachtwoordvelden zijn uitgesloten van registratie. Behandel niet-ondersteunde controles en Windows-beperkingen op integriteitsniveau expliciet.

Breid de opname uit met beoordeelbare UIA-acties, waarbij de huidige snelkoppelingsopname behouden blijft. Dubbelzinnige selectors mislukken in plaats van op een willekeurig besturingselement te klikken. Een normale browsertoegankelijkheidsboom is geen belofte van een volledige DOM-automatiseringsengine.

Acceptatie: automatiseer een speciale testtoepassing met stabiele selectors voor positie-/grootteveranderingen; test dubbelzinnigheid, ontbrekende elementen, uitgeschakelde bedieningselementen, time-outs en annulering.

## 6. Lokaal telefoon-/tabletpaneel
Bedien een responsief paneel via een opt-in LAN-service. Koppel expliciet met behulp van een eenmalige code van korte duur en geef herroepbare inloggegevens uit die lokaal zijn beschermd. Gebruik HTTPS met gedocumenteerde lokale certificaatvertrouwen/vastzetten; geen terugval bij het verzenden van inloggegevens in leesbare tekst. Standaard uitgeschakeld en geconfigureerde interfaces beperken. Oorsprongvalidatie, geauthenticeerde opdrachten, snelheidslimieten en begrensde berichten zijn verplicht.

Laat toegestane profielen/acties zien, live status, voortgang en annuleren; gebruik stabiele ID's en dezelfde uitvoeringswachtrij als desktop- en fysieke sleutels. Maak geen willekeurige opdrachten, uitvoerbare paden, geheimen of de installatie van plug-ins openbaar. Zorg voor verbindings-, koppelings- en intrekkingsschermen. Er is geen cloudaccount of internetrelay inbegrepen.

Acceptatie: opereren vanaf een echte telefoon/tablet op hetzelfde LAN; ongepaarde clients kunnen geen acties weergeven of uitvoeren; ingetrokken tokens mislukken; dubbele/opnieuw afgespeelde opdrachten worden niet dubbel uitgevoerd; Disconnect probeert acties niet stilletjes opnieuw.

## Toepassingsgebied van toetsenbordfabrikant

| Adapter/familie | Geplande integratie | Expliciete grens |
| --- | --- | --- |
| Corsair | Behoud de huidige iCUE G-key-invoer en RGB-integratie | Detectie van model-/softwaremogelijkheden blijft vereist |
| Logitech G | G-key SDK-adapter met controles op pers/vrijgave en runtime-beschikbaarheid; verlichting via de afzonderlijke SDK waar dit wordt ondersteund | Stel G HUB-detectie niet op elk model gelijk aan de ondersteunde G-key API |
| Razer | Chroma-verlichtingsadapter en begeleide Synapse-naar-standaard-sleuteltoewijzing, plus generieke HID-ingang | Chroma is een verlichtings-API; noem het niet native propriëtaire macrotoetsinvoer |
| SteelSeries | GameSense status-/verlichtingsadapter plus generieke HID-ingang | GameSense-gebeurtenisinzending is geen algemene eigen API voor het vastleggen van sleutels |
| Wooting | Analoge SDK-adapter voor ondersteunde analoge invoer, plus normale HID-sleutels | Mogelijkheden en SDK-distributievoorwaarden moeten per ondersteund model worden geverifieerd |
| Keychron/QMK/VIA | Standaard HID-sleutels en instructies voor het in kaart brengen van firmware; bekende lay-outs alleen met bewijs | VIA-configuratieondersteuning is geen universele runtime-macrosleutel-API |
| Andere standaard HID-toetsenborden | Ontdekking en mapping van fysieke sleutels per apparaat; exclusieve modus alleen met de gevalideerde filterbackend | Geen gefabriceerde modelspecifieke Fn-, draaiknop- of eigen sleutelondersteuning |

Elk apparaat toont de status gedetecteerd, waargenomen, in kaart gebracht via software van de leverancier, native SDK, exclusieve driver, niet beschikbaar of hardware-niet-getest, zoals van toepassing. Behoud capaciteitsbewijs met adapterversies en geteste model-/firmware-ID's.

## Bezorgopdracht
1. Configuratiecompatibiliteit, capaciteitscontracten en geverifieerde uitvoering.
2. Begeleide installatie met behulp van echte ontdekking en bestaande actie-uitvoerders.
3. UI Automation en zijn recorder/testarmatuur.
4. Pluginhost, pakketstroom en voorbeeldintegratie.
5. Gekoppeld LAN-paneel en mobiele verificatie.
6. Uitgebreide leveranciersadapters en exclusieve toetsenbord-backend-integratie, met daadwerkelijke hardwarevalidatie.

Ze blijven alle zes nodig. Het ondertekenen/licentieren van stuurprogramma's, ontoegankelijke SDK's of ontbrekende testhardware zijn vrijgavepoorten en geen reden om schijngedrag te vervangen en het werk voltooid te noemen.

## Verificatie- en vrijgavecriteria
Voer bestaande kern-, Windows- en WPF-controles uit en voeg gerichte integratietests toe voor elke nieuwe grens. JSON-rondreizen en verouderde profielen verifiëren; test geïmporteerde uitvoerbare inhoud, netwerkverificatie, annulering, crashes van plug-ins en opnieuw verbinden van apparaten. Inspecteer desktop- en mobiele lay-outs. Valideer de hoge Windows-schaling afzonderlijk. Bouw release-artefacten alleen nadat u de controles heeft doorstaan; onderscheid geïmplementeerde/geteste, geïmplementeerde/hardware-ongeteste en extern geblokkeerde functionaliteit in release-opmerkingen. Vervang de geïnstalleerde applicatie niet, installeer geen stuurprogramma's, wijzig geen echte toetsenbordtoewijzingen of open netwerktoegang als een incidentele test.

## Bewijs
- Microsoft Raw Input semantiek: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G-SDK's voor ontwikkelaars: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Veelgestelde vragen over Chroma SDK: https://developer.razer.com/works-with-chroma/chroma-faq/
- Voorwaarden voor opslagplaats voor onderschepping en afhankelijkheid: https://github.com/oblitum/Interception
- Bestaande inventaris van de fabrikant van de repository: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Beslissingen herzien
Voorafgaand aan het implementatieplan wordt de gebruiker gevraagd dit schriftelijke ontwerp goed te keuren. De belangrijkste externe afhankelijkheid is een productieklare driver-backend met exclusieve invoer. De aanbevolen reikwijdte behoudt dit als een afgesloten resultaat in plaats van het stilzwijgend te reduceren tot Raw Input-observatie.

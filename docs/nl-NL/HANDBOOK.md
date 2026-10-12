<!-- generated-doc locale=nl-NL source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../HANDBOOK.md)

# MAKRIVO — Gebruikershandleiding

Fabrikant: **Aivana**. Momentopname van de documentatie: **2026-10-12**. Dit handboek beschrijft de huidige werkboomimplementatie. Experimentele integraties en hardware-afhankelijke functies worden expliciet geïdentificeerd. Historische rapporten beschrijven hun gedateerde momentopname, geen garantie voor een nieuwere versie.

## Documentatie en talen

Gebruik de documentatietaalkiezer om de volledige handleiding, functie-inventaris, probleemoplossing, ontwikkelingsnotities, onderzoek en historische testrapporten te openen in een van de 27-toepassingstalen. Gegenereerde vertalingen worden machinaal ondersteund en ondergaan structurele controles; ze hebben niet allemaal een native speaker-beoordeling gekregen. Technische identificatiegegevens en uitvoerbare voorbeelden blijven ongewijzigd. De originele bron blijft ter vergelijking beschikbaar. Vertaalde licentieanalyses zijn informatief en vervangen niet de oorspronkelijke juridische voorwaarden van derden of een definitieve EULA.

## Installatie en eerste lancering

Het initiële doel is Windows 11 x64. Houd elk bestand en elke submap van de distributie bij elkaar. Start `MAKRIVO.exe`. Een source checkout is geen installatieprogramma. Op zichzelf staande pakketten bevatten hun .NET-runtime; raamwerkafhankelijke pakketten vereisen de juiste .NET 10 Desktop- en ASP.NET-runtimes. ONNX en begeleidende hosts moeten worden opgenomen vanwege hun respectieve kenmerken.

1. Start MAKRIVO en kies de interfacetaal in Instellingen. Automaat volgt Windows. Toetsenbordindeling is een aparte instelling.
2. Kies een profiel en voeg vervolgens een tegel toe via de doorzoekbare actiecatalogus. Geef het een herkenbare naam en kies de acties.
3. Bekijk de stappen en wijs indien nodig een snelkoppeling toe. Vermijd combinaties die al worden gebruikt door Windows of uw doeltoepassing.
4. Bewaar het concept. Niet-opgeslagen wijzigingen zijn niet de opgeslagen configuratie.
5. Test met een ongevaarlijke actie, zoals het openen van een document, en bekijk de uitvoeringsfeedback.

Als u het hoofdvenster sluit, blijft de toepassing normaal gesproken in het systeemvak staan. Dubbelklik op het ladepictogram om het opnieuw te openen, of gebruik het lademenu om af te sluiten. Beginnen met Windows is optioneel en in eerste instantie uitgeschakeld. De pauzecontrole schort de bindingen op; het pictogram toont een pauzebadge. Annuleer actieve macro's via de gebruikersinterface, lade of de geconfigureerde annuleringssnelkoppeling; de standaardwaarde is `Ctrl+Alt+Escape`.

## Tegels, acties en het commandopalet

Tegels behoren tot profielen en kunnen meerdere geordende stappen bevatten. De editor ondersteunt opslaan/weggooien, bekijken, ongedaan maken/opnieuw uitvoeren en stapsgewijs opnieuw ordenen. De catalogus biedt voorinstellingen voor programma's, documenten, URL's, tekst, klembord, snelkoppelingen, media, audio, vensters, systeemacties en ondersteunde integraties. Een voorinstelling waarvoor parameters nodig zijn, wordt geopend als concept.

Open het palet met `Ctrl+K` terwijl MAKRIVO focus heeft. Zoek naar opgeslagen acties, presets, profielwisseling en tools. Pijltjestoetsen selecteren; Enter activeert; `Ctrl+Enter` bewerkt een ondersteund item; De ontsnapping is gesloten. Sommige kant-en-klare opdrachten worden onmiddellijk uitgevoerd, terwijl geconfigureerde acties beoordeling vereisen. Er kan een aparte globale paletsnelkoppeling worden geconfigureerd; nieuwe configuraties gebruiken `Ctrl+Shift+Space`.

Tekst- en klembordstappen ondersteunen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` en `{{user}}`. Datumnotatie is `yyyy-MM-dd`; tijdnotatie is `HH:mm`. Onbekende variabelen blijven ongewijzigd. Het lezen van het klembord gebeurt alleen wanneer daarom wordt gevraagd. Met een klembordactie wordt tekst geschreven zonder deze automatisch te plakken.

Acties die vanuit de gebruikersinterface worden gestart, kunnen zich richten op de laatste externe applicatie. Als het doel verdwijnt of Windows de activering op de voorgrond blokkeert, activeer het doel dan handmatig en probeer het opnieuw. Succesvolle sleuteloverdracht bewijst niet dat het doelwit zijn taak heeft voltooid.

## Profielen en werkplekinrichting

Profielen kunnen overeenkomen met een procesnaam en een deel van een venstertitel. Als beide zijn ingesteld, moeten beide overeenkomen. Profielvolgorde lost concurrerende wedstrijden op; het standaardprofiel is de fallback. Schakel automatische selectie expliciet in. Profiel vergrendelen voorkomt automatische wijzigingen voor de huidige sessie.

Open Werkplekinstellingen vanuit Tools of het palet. Selecteer programma's, documenten, websites, een audio-/weergavedoel of een OBS-scène. De assistent maakt werkplek-, vergader-, streaming-, onderzoeks- of projectprofielen op. Controleer de gegenereerde stappen voordat u deze opslaat. Setup voert de voorbereide acties niet uit.

## Macro's, omstandigheden en opname

Macro's voeren hun stappen in volgorde uit en ondersteunen wachten, herhalingen en annuleringen. De algemene herhalingslimiet is 100. Gebruik Preview voordat u acties uitvoert die in een andere applicatie typen of de systeemstatus wijzigen.

Wachten op doelvenster accepteert een proces, een titelsubstring en een time-out van 1 tot 600,000 ms. Er is minimaal één filter vereist. Een bijpassend zichtbaar venster moet reageren en op de voorgrond komen voordat de volgende stappen worden voortgezet. Een gereedvenster bewijst niet dat een document of dienst klaar is met laden.

De workflow-editor voegt voorwaarden voor het bestaan van bestanden, lopende processen en venstertitels toe. Een valse voorwaarde slaat de stap over en kan extra stappen vooruit overslaan. Negeer de voorwaarde om een ​​alternatieve tak uit te drukken. Kies bewust voor stoppen bij fout of doorgaan bij fout; doorgaan kan latere stappen achterlaten zonder de verwachte randvoorwaarden.

De actierecorder legt snelkoppelingen vast en wacht, geen gewoon typen. Het ondersteunt modificatiesnelkoppelingen en functietoetsen. Escape stopt met opnemen. Bekijk de lijst, neem deze over als een niet-opgeslagen tegelconcept en sla deze vervolgens expliciet op. UI-klikopname is een afzonderlijk hulpmiddel dat hieronder wordt beschreven.

## Toetsenborden, gebaren en lagen

Op de toetsenbordpagina worden gedetecteerde interfaces en referentielay-outs vermeld. Eén enkel fysiek toetsenbord kan meerdere interfaces blootleggen. Selecteer de juiste taal en geometrie; een productnaam alleen identificeert de ISO- of ANSI-indeling niet.

Standaard globale sneltoetsen zijn van toepassing op alle toetsenborden. Apparaatspecifieke Raw Input-bindingen observeren een bepaald apparaat en een specifieke scancode, inclusief ondersteunde functie- en mediatoetsen. **Raw Input onderdrukt de gewone Windows-invoer niet. Een exclusief tweede toetsenbord is nog niet productieklaar.** Een ondertekende, gelicentieerde en geteste filterdriver-backend is nog steeds vereist; geen enkele wordt geïnstalleerd door MAKRIVO.

Ondersteunde speciale toetsbindingen kunnen direct indrukken, tikken, vasthouden of dubbeltikken gebruiken. Vastgehouden laagtoetsen selecteren tijdelijk een andere mapping en herstellen de basislaag bij het loslaten; laagselectietoetsen voeren geen eigen macro uit. Hold-drempels variëren van 150 tot 5,000 ms; dubbeltik op vensters van 150 tot 1,000 ms. Deze toewijzingen verschillen van gewone globale sneltoetsen.

De wizard voor speciale sleutels detecteert compatibele sleutels zonder de toegewezen acties uit te voeren tijdens het vastleggen. Diagnostische gegevens verklaren niet-beschikbare apparaten, onderbroken bindingen, niet-overeenkomende profielen en conflicten. De overlay toont de geselecteerde lay-out en bindingen; de standaard sneltoets voor vasthouden is `Ctrl+Alt+F1`.

| Integratie | Vereisten en beperkingen |
| --- | --- |
| Corsair | Met iCUE, SDK-toegang en een compatibel model. G1–G6 en ondersteunde RGB-functies; eigen Fn-, wiel-, slot- en profielbedieningen worden over het algemeen niet zichtbaar door de G-key API. |
| Logitech G | Originele lokale x64 G-key/LED SDK-bibliotheken vereisen expliciete goedkeuring. G-toetsgebeurtenissen identificeren niet op unieke wijze meerdere identieke fysieke toetsenborden. |
| Razer | Chroma RGB en standaardingang; breng bedrijfseigen besturingselementen in Synapse in kaart waar nodig. |
| SteelSeries | GameSense RGB- en standaardingang; breng bedrijfseigen controles in GG in kaart waar nodig. |
| Wooting | Compatibele originele lokale x64 SDK voor analoge waarden en drempels; normale HID-ingang blijft actief. |
| Andere toetsenborden | Standaardsneltoetsen of ondersteunde fysieke scancodebindingen. Fn en eigen bedieningselementen zijn afhankelijk van het model en de leveranciersconfiguratie. |

Onderzoek van de fabrikant beschrijft gedocumenteerde mogelijkheden, niet geïmplementeerde of geteste compatibiliteit met elk vermeld apparaat. Nieuwe adapters hebben nog steeds echte hardware-acceptatie nodig. Download geen binaire SDK-bestanden van onbetrouwbare bronnen; SDK-distributierechten staan ​​los van technische compatibiliteit.

## Audio, displays en OBS

Audio stappen set volume van 0 om te 100 percentage en mute/unmute/toggle ondersteunde output of microfoon endpoints. Windows een vermiste beveiligde doelstelling produceert een fout in plaats van stil te controleren een ander apparaat. Verfrissing van apparaten na herverbinding. Endpoint control is geen claim van onafhankelijke mixer ondersteuning per applicatie.

Weergaveacties verhogen/verlagen de helderheid met 10 procentpunten of stellen een waarde in van 0 tot 100. Interne beeldschermen gebruiken ondersteunde WMI-interfaces; externe monitoren vereisen DDC/CI-ondersteuning en meestal de monitormenu-instelling. Docks, adapters en kabels kunnen de bediening blokkeren. Kies alle ondersteunde beeldschermen of één monitor. Opnieuw verbinden kan beschikbare identiteiten veranderen; bekijk het opgeslagen doel. Ontbrekende metingen worden weergegeven als niet-beschikbare waarden in plaats van als gemeten waarden.

OBS vereist dat WebSocket v5 is ingeschakeld in OBS Tools. Configureer het eindpunt, test de verbinding en laad scène-/ingangsnamen voordat u opslaat. Ondersteunde acties zijn onder meer scènewisseling, stream/opname starten/stoppen/wisselen, opname pauzeren/hervatten en input dempen. Wachtwoorden kunnen worden opgeslagen in Windows Credential Manager en zijn uitgesloten van configuratie-exports. Een succesvolle OBS-opdracht brengt niet elk stroomafwaarts streaming-effect tot stand.

## Resultaatverificatie en uitvoeringsgeschiedenis

Schakel Resultaat verifiëren in voor geschikte stappen. De app maakt onderscheid tussen preflightcontroles, bevestigde effecten, mislukkingen, annuleringen en niet-verifieerbare effecten. Het bestaan ​​van een proces betekent niet dat de applicatie gereed is. Relatieve wijzigingen, snelkoppelingen en tekstinvoer kunnen geen algemene succesclaim krijgen. Inspecteer het activiteitenlogboek en de verificatiegeschiedenis wanneer een actie mislukt. Configuratiefouten moeten worden gecorrigeerd voordat de volledige macro opnieuw wordt uitgevoerd.

## UI-automatisering en klikopname

Voeg een UI Automation-stap toe, selecteer de operatie en vallen de doelgroep onder de indicator na de weergave. alleen vallen niets uit. Check de selector en vereist een unieke match. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` en `WaitState`. Passpoorten, ingeschakelde controles en ambiguïte selecteurs worden afgewezen.

UI-automatisering wordt uitgevoerd in een begrensd begeleidend proces. Dit hangt af van de toegankelijkheidsondersteuning en machtigingen Windows van de doeltoepassing. De klikrecorder legt ondersteunde acties en pauzes vast; het registreert geen algemene tekstinvoer. Beoordeel en valideer het ontwerp voordat u het aanneemt. UI-wijzigingen in een externe app kunnen selectors ongeldig maken. Procesisolatie beperkt crashes en annuleringen, maar is geen beveiligingssandbox.

## Plug-ins

Open Extensies om een ​​pakket te inspecteren voordat u het importeert. Pakketstructuur, manifest en hashes worden gecontroleerd. Een import start uitgeschakeld. Lees de gedeclareerde acties en vertrouw de uitgever expliciet voordat u deze inschakelt. Plug-ins worden uitgevoerd met uw gebruikersrechten in een afzonderlijk proces; dat proces is **geen sandbox**. Installeer alleen pakketten die u vertrouwt.

De manager ondersteunt het exporteren, uitschakelen, verwijderen en herstellen/verwijderen van een eerdere versie. Ontbrekende afhankelijkheden blokkeren de aanroep. Het voorbeeldpakket biedt aftel- en lokale statusacties. Het ondersteunde schema is een gedocumenteerde JSON-Schema-subset, niet elke mogelijke JSON-Schema-functie. Er is geen openbare marktplaats voor plug-ins.

## Mobiel paneel

1. Open Mobile Panel, selecteer een lokaal IP-adres en kies expliciet de profielen die u wilt weergeven. Het standaard loopback-adres is alleen bereikbaar op de pc zelf; selecteer het juiste LAN-adres voor een telefoon of tablet.
2. Start het paneel en lees het HTTPS-adres, de vingerafdruk van het certificaat en de zescijferige koppelingscode voor eenmalig gebruik.
3. Open het adres op een apparaat in hetzelfde vertrouwde netwerk. Vergelijk de SHA-256-vingerafdruk via een vertrouwd kanaal voordat u het lokale certificaat vertrouwt. De app installeert niet automatisch een vertrouwd certificaat.
4. Koppel met behulp van de weergegeven code en test één onschadelijke tegel. Voor LAN-toegang kan handmatige Windows Firewall-toestemming nodig zijn. Stel de poort niet bloot aan internet.
5. Stop het paneel als u klaar bent, of trek gekoppelde clients in via de app.

Privéserversleutels worden beveiligd met Windows DPAPI; klantreferenties worden opgeslagen als hashes. Het wijzigen van het IP-adres of het vernieuwen van het certificaat kan hernieuwd vertrouwen vereisen. Het paneel toont actie- en verificatiestatussen, niet elke mogelijke live-sensor. Browserindeling en lokale HTTPS-controles vervangen de echte LAN-acceptatie van telefoons/tablets niet.

## Lokale AI-vergaderassistent

Deze optionele experimentele assistent is standaard uitgeschakeld. Open het vanuit Tools, schakel het expliciet in, selecteer een runtime/model en voeg alleen de bronnen toe waarmee u rekening wilt houden. Geef het doel aan, maak een concept, inspecteer onopgeloste verzoeken en draag het resultaat over aan de normale editor. Niets wordt automatisch uitgevoerd; opslaan en uitvoeren blijven expliciete acties. Er is geen automatische runtime-fallback.

| Looptijd | Installatie |
| --- | --- |
| Ollama | Aparte lokale service en een lokaal geïnstalleerd model op `127.0.0.1:11434`. Aangegeven externe/cloudmodellen worden afgewezen. |
| LM Studio | Lokale server met een lokaal geladen model en JSON-schema-ondersteuning op `127.0.0.1:1234`. Schakel externe routering niet in voor lokale verwerking. |
| llama.cpp | Lokaal `llama-server` en compatibel model met JSON-schema-ondersteuning op `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Vertrouwde compatibele lokale GenAI tekstmodelmap; selecteer `genai_config.json`. Inference gebruikt een gebundeld begeleidend proces zonder HTTP. |

HTTP-clients gebruiken alleen loopback, gebruiken geen proxy's en volgen geen omleidingen. MAKRIVO kan niet op betrouwbare wijze vaststellen of een compatibele lokale server intern een externe service aanroept. Geauthenticeerde lokale servers kunnen in deze versie niet worden geconfigureerd. Modelgewichten, Python en lokale inferentieservers worden niet automatisch gedownload of geïnstalleerd.

ONNX Pakketten nodig `tokenizer.json` en een geldig chat-template. ondersteunde template-formaten in `tokenizer_config.json` Een string `chat_template`Een woordenboek met een string `default`, of een onduidelijke naam/default template lijst. als het template ontbreekt of nul, `chat_template.jinja` Vrije, ambiguur, overschot of onjuist getypt templates worden afgewezen. ONNX bestanden, inbound/visie modellen en GPU-specifieke pakketten zijn niet uitwisseld met compatibele GenAI De tekstmodellen. de bundel GenAI De runtime versie is 0.17.1Gebruik alleen vertrouwde modellen; procesafscheiding is geen zandbox.

De assistent ondersteunt geselecteerde programma's zonder argumenten, geselecteerde documenten en websites en het dempen van de microfoon wanneer dit uitdrukkelijk wordt gevraagd. Het creëert geen berichten, afspraken, willekeurige scripts, gratis klikken, downloads of wisselen van headset. De doel- en resourcenamen worden naar de geselecteerde lokale runtime verzonden; bronpaden en bestandsinhoud worden niet afzonderlijk geleverd. Een door de gebruiker getypt doel kan zelf persoonsgegevens bevatten. Het eigen netwerkgedrag van een lokale runtime blijft de verantwoordelijkheid van de gebruiker.

Het ONNX-aanvraagbudget bedraagt ​​90 seconden inclusief het laden van het model, tot aan 3,072-prompttokens en 1,024 gegenereerde tokens, ook beperkt door de modelcontext. Verkeerd opgemaakte/afgekapte JSON en onbekende resource-ID's worden afgewezen. Annuleer of sluit om een ​​verzoek te beëindigen. Een runtimetest met een willekeurig model stelt de uitvoering vast, en niet de bruikbare planningskwaliteit of tijdbesparing. Lokale experimentlogboeken bevatten gebeurtenis-ID's en tijdstippen, geen doeltekst of documentinhoud; ze worden niet automatisch geüpload.

De latere kleine Instruct-modelbeoordeling produceerde nul geldig plannen in vier Duitse/Engelse proeven. Native inference werkte, maar het model mislukt de strikte planningcontract. Dit is een negatief kwaliteit resultaat, niet een aanbevolen model of een bewezen nuttige assistent.

## Importeren, exporteren, synchronisatie en back-up

Bekijk importvoorbeelden voordat u een configuratie vervangt. Geïmporteerde acties worden uitgeschakeld en draagbare synchronisatie verwijdert hardwarespecifieke bindingen. Wijs apparaten opnieuw toe en schakel acties bewust in na inspectie. Controleer de inhoud van tekst/acties voordat u een export deelt; het uitsluiten van wachtwoorden betekent niet dat alle door de gebruiker gemaakte actie-inhoud niet-gevoelig is.

Profile synchronisatie vervangt bestanden in een geselecteerde map, optie een beheerd door uw bestaande cloud client. MAKRIVO geen cloud-account levert en geen cloud-creditials opslaat. Revisions voorkomen stille overwinningen. Inclusie van actieinhoud is een uitdrukkelijke keuze. Backup uw configuratie voordat u een vervanging importeren. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`De historische directory naam wordt bewaard voor compatibiliteit. sluit de app voordat u het handmatig kopiërt/herstelt. bewaard SDK-approvals en OS-beschermde credentials kunnen op een andere PC worden hersteld.

## Problemen oplossen

| Symptoom | Wat te controleren |
| --- | --- |
| Snelkoppeling doet niets | App actief, bindingen niet gepauzeerd, correct actief profiel, opgeslagen/ingeschakelde tegel, snelkoppelingsconflicten en beschikbaar doel. |
| Doelvenster kan niet worden geactiveerd | Activeer het handmatig. Windows beperkingen op de voorgrond en verschillen in rechten kunnen automatisering blokkeren. |
| Speciale sleutel wordt niet gedetecteerd | Leveranciersservice/SDK/modelondersteuning; configureer eigen besturingselementen om, indien mogelijk, een ondersteunde standaardsleutel uit te zenden. |
| Tweede toetsenbord typt ook normaal | Verwacht met Raw Input. Exclusieve onderdrukking wordt niet geïmplementeerd voor de productie. |
| Audio of helderheid niet beschikbaar | Apparaten vernieuwen; inspecteer de geselecteerde identiteit, eindpuntsessies, WMI/DDC/CI-ondersteuning en dock-/kabelbeperkingen. |
| OBS offline | WebSocket v5 ingeschakeld, correct eindpunt/wachtwoord, bereikbare service en geldige scène-/invoernamen. |
| UI-automatisering mislukt | Inclusief begeleidende host, unieke geldige selector, toegankelijk ingeschakeld doel, ondersteunde controle en machtigingen. Probeer een tijdelijke time-out alleen opnieuw nadat u het doel hebt bekeken. |
| Mobiel paneel onbereikbaar | LAN in plaats van loopback-adres, hetzelfde netwerk, draaiende server, goedgekeurde profielen, certificaatvingerafdruk/vertrouwen en handmatige firewall-machtigingen. |
| Plug-in mislukt | Pakketintegriteit, expliciete vertrouwens-/ingeschakelde status, afhankelijkheden, protocolcompatibiliteit en uitvoeringslogboek. |
| AI-verzoek mislukt | Corrigeer de geselecteerde lokale runtime, gereed voor service/model, ondersteund JSON-schema, vertrouwde compatibele modelmap en verzoeklimieten. |
| Lay-out of tekst lijkt verkeerd | Scheid de UI-taal van de toetsenbordgeometrie; wijzig het formaat van het venster, controleer de weergaveschaal en rapporteer de exacte pagina/taal/build. |

Voor een nuttig bugrapport zijn de Windows-versie, app-build/commit, taal, weergaveschaling, hardwaremodel, een minimale reeks en het relevante geredigeerde logbestand opgenomen. Verwijder persoonlijke paden, actie-inhoud en inloggegevens voordat u deze publiceert.

## Bouwen, testen en verpakken

Gebruik de SDK die is vastgezet in `global.json`. Vanuit de root van de repository:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows-integratietests kunnen geïsoleerde vensters openen en ondersteunde leveranciersbindingen tijdelijk configureren/vrijgeven. Voer ze uit zonder dat een andere controller eigenaar is van deze bindingen. De standaard helderheids-/audiosondes zijn alleen-lezen; expliciete hardware round-trip-tests zijn afzonderlijk. Mock-protocollen, weergegeven schermafbeeldingen en willekeurige gevolgtrekkingsgrafieken mogen niet worden voorgesteld als echte acceptatie van hardware/modelkwaliteit.

De volledige uitgever verzamelt begeleidende hosts, mobiele middelen, de voorbeeldplug-in, de ONNX-runtime en beschikbare afhankelijkheidskennisgevingen. Behoud de submappen. Een succesvolle build is geen ondertekend installatieprogramma of wettelijke goedkeuring. Bron en builds kunnen afzonderlijk evolueren; vergelijk de documentatie-snapshot en voer de commit uit met de geïnstalleerde versie.

## Licentie-, privacy- en releasestatus

Het beoogde commerciële model is een eenmalige aankoop met eeuwigdurend gebruik van de gekochte hoofdversie; de uiteindelijke prijs, het apparaatbedrag en de upgradevoorwaarden moeten worden vastgelegd in de daadwerkelijke verkoopvoorwaarden. In dit handboek wordt geen productieaankoop-/activeringssysteem vastgelegd. Er is geen vereist cloudaccount voor dagelijkse controle.

Verspreid geen bedrijfseigen SDK-binaire bestanden zonder bewezen toestemming. Bewaar licentiekennisgevingen en kennisgevingen van derden voor elke verzonden afhankelijkheid, inclusief .NET- en ONNX-pakketten. Modelgewichten hebben hun eigen voorwaarden. De gedateerde licentie-audit dateert van vóór enkele nieuwe afhankelijkheden en is geen goedkeuring voor deze toevoegingen. Omdat de repository openbaar is, wordt op zichzelf geen algemene open-sourcelicentie verleend.

Hardware van echte leveranciers, LAN-gebruik via telefoon/tablet, overgangen tussen monitoren/DPI, langdurig gebruik, volledige taalkundige beoordeling, exclusieve driverondersteuning en nuttige AI-planningskwaliteit blijven afzonderlijke acceptatiewerkzaamheden. Leid nooit goedkeuring af op basis van de aanwezigheid van een kenmerk alleen.

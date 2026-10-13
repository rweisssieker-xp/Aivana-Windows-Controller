<!-- generated-doc locale=nl-NL source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../HANDBOOK.md)

# MAKRIVO-gebruikershandboek

Fabrikant: **Aivana**. Documentatiemomentopname: **2026-10-12**. Dit handboek beschrijft de huidige implementatie in de werkboom. Experimentele integraties en hardwareafhankelijke functies worden expliciet aangeduid. Historische rapporten beschrijven hun gedateerde momentopname en geven geen garantie voor een nieuwere build.

## Documentatie en talen

Gebruik de documentatietaalkeuze om de volledige handleiding, functie-inventaris, probleemoplossing, ontwikkelnotities, onderzoek en historische testrapporten in elk van de 27 toepassingstalen te openen. Gegenereerde vertalingen zijn machinaal ondersteund en structureel gecontroleerd; ze zijn niet allemaal door moedertaalsprekers beoordeeld. Technische identifiers en uitvoerbare voorbeelden blijven ongewijzigd. De oorspronkelijke bron blijft beschikbaar ter vergelijking. Vertaalde licentieanalyse is informatief en vervangt niet de oorspronkelijke juridische voorwaarden van derden of een definitieve EULA.

## Installatie en eerste lancering

Het eerste doelplatform is Windows 11 x64. Houd alle bestanden en submappen van de distributie bij elkaar. Start `MAKRIVO.exe`. Een checkout van de broncode is geen installatieprogramma. Zelfstandige pakketten bevatten hun .NET-runtime; frameworkafhankelijke pakketten vereisen de juiste .NET 10 Desktop- en ASP.NET-runtimes. ONNX en begeleidende hosts moeten voor hun bijbehorende functies worden meegeleverd.

1. Start MAKRIVO en kies de interfacetaal in Instellingen. Automatisch volgt Windows. De toetsenbordindeling is een afzonderlijke instelling.
2. Kies een profiel en voeg een tegel toe via de doorzoekbare actiecatalogus. Geef de tegel een herkenbare naam en kies de acties.
3. Controleer de stappen en wijs zo nodig een sneltoets toe. Vermijd combinaties die Windows of de doeltoepassing al gebruikt.
4. Sla het concept op. Niet-opgeslagen wijzigingen maken geen deel uit van de opgeslagen configuratie.
5. Test met een onschuldige actie, bijvoorbeeld een document openen, en controleer de uitvoeringsfeedback.

Als u het hoofdvenster sluit, blijft de toepassing gewoonlijk in het systeemvak actief. Dubbelklik op het systeemvakpictogram om het venster te heropenen of sluit af via het systeemvakmenu. Starten met Windows is optioneel en aanvankelijk uitgeschakeld. De pauzeknop onderbreekt bindingen; het pictogram toont een pauzebadge. Annuleer lopende macro’s via de interface, het systeemvak of de ingestelde annuleringssneltoets; standaard is dit `Ctrl+Alt+Escape`.

## Tegels, acties en het opdrachtenpalet

Tegels behoren tot profielen en kunnen meerdere geordende stappen bevatten. De editor ondersteunt opslaan/verwerpen, voorbeeldweergave, ongedaan maken/opnieuw en herschikken van stappen. De catalogus levert voorinstellingen voor programma’s, documenten, URL’s, tekst, klembord, sneltoetsen, media, audio, vensters, systeemacties en ondersteunde integraties. Een voorinstelling die parameters vereist, opent als concept.

Open het palet met `Ctrl+K` terwijl MAKRIVO focus heeft. Zoek opgeslagen acties, voorinstellingen, profielwisselingen en hulpmiddelen. Selecteer met de pijltjestoetsen; Enter activeert; `Ctrl+Enter` bewerkt een ondersteund item; Escape sluit. Sommige kant-en-klare opdrachten worden meteen uitgevoerd, terwijl geconfigureerde acties controle vereisen. Er kan een afzonderlijke globale paletsneltoets worden ingesteld; nieuwe configuraties gebruiken `Ctrl+Shift+Space`.

Tekst- en klembordstappen ondersteunen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` en `{{user}}`. Het datumformaat is `yyyy-MM-dd`; het tijdformaat is `HH:mm`. Onbekende variabelen blijven ongewijzigd. Het klembord wordt alleen gelezen wanneer daarom wordt gevraagd. Een klembordactie schrijft tekst zonder die automatisch te plakken.

Acties die vanuit de interface worden gestart, kunnen de laatste externe toepassing als doel hebben. Als het doel verdwijnt of Windows activering op de voorgrond blokkeert, activeert u het doel handmatig en probeert u het opnieuw. Succesvolle levering van toetsaanslagen bewijst niet dat het doel zijn taak heeft voltooid.

## Profielen en werkplekinrichting

Profielen kunnen overeenkomen met een procesnaam en een deel van een venstertitel. Als beide zijn ingesteld, moeten beide overeenkomen. De profielvolgorde bepaalt welk profiel bij meerdere overeenkomsten wint; het standaardprofiel is de terugval. Schakel automatische selectie expliciet in. Profiel vergrendelen voorkomt automatische wijzigingen tijdens de huidige sessie.

Open Werkplekinrichting vanuit Hulpmiddelen of het palet. Selecteer programma’s, documenten, websites, een audio-/beeldschermdoel of een OBS-scène. De assistent bereidt werkplek-, vergader-, streaming-, onderzoeks- of projectprofielen voor. Controleer de gegenereerde stappen voordat u opslaat. De inrichting voert de voorbereide acties niet uit.

## Macro’s, voorwaarden en opname

Macro’s voeren stappen in volgorde uit en ondersteunen wachttijden, herhalingen en annulering. De algemene herhalingslimiet is 100. Gebruik de voorbeeldweergave voordat u acties uitvoert die in een andere toepassing typen of de systeemstatus wijzigen.

Wachten op doelvenster accepteert een proces, een deel van een titel en een time-out van 1 tot 600000 ms. Minstens één filter is vereist. Een overeenkomend zichtbaar venster moet reageren en op de voorgrond komen voordat volgende stappen doorgaan. Een gereed venster bewijst niet dat een document of dienst klaar is met laden.

De workflow-editor voegt voorwaarden toe voor het bestaan van een bestand, een actief proces en de venstertitel. Als een voorwaarde onwaar is, wordt de stap overgeslagen en kunnen extra volgende stappen worden overgeslagen. Keer de voorwaarde om voor een alternatieve tak. Kies bewust tussen stoppen bij een fout en doorgaan bij een fout; bij doorgaan kunnen latere stappen hun vereiste voorwaarden missen.

De actierecorder legt sneltoetscombinaties en wachttijden vast, geen gewone tekstinvoer. Hij ondersteunt sneltoetsen met modificatietoetsen en functietoetsen. Escape stopt de opname. Controleer de lijst en neem die over als niet-opgeslagen tegelconcept; sla vervolgens expliciet op. UI-klikopname is een afzonderlijk hulpmiddel dat hieronder wordt beschreven.

## Toetsenborden, gebaren en lagen

De toetsenbordpagina vermeldt gedetecteerde interfaces en referentie-indelingen. Eén fysiek toetsenbord kan meerdere interfaces aanbieden. Kies de juiste taal en geometrie; alleen een productnaam identificeert geen ISO- of ANSI-indeling.

Standaard globale sneltoetsen gelden voor alle toetsenborden. Apparaatspecifieke Raw Input-bindingen observeren een bepaald apparaat en een scancode, inclusief ondersteunde functie- en mediatoetsen. **Raw Input onderdrukt gewone Windows-invoer niet. Een exclusief tweede toetsenbord is niet productieklaar.** Hiervoor is nog een ondertekende, gelicentieerde en geteste filterdriver-backend nodig; MAKRIVO installeert er geen.

Ondersteunde speciale toetsbindingen kunnen reageren op direct indrukken, tikken, vasthouden of dubbeltikken. Laagtoetsen selecteren tijdens het vasthouden tijdelijk een andere toewijzing en herstellen de basislaag bij loslaten; laagselectietoetsen voeren geen eigen macro uit. Vasthouddrempels lopen van 150 tot 5000 ms; tijdvensters voor dubbeltikken van 150 tot 1000 ms. Deze toewijzingen staan los van gewone globale sneltoetsen.

De wizard voor speciale toetsen detecteert compatibele toetsen zonder tijdens het vastleggen hun toegewezen acties uit te voeren. Diagnostiek verklaart niet-beschikbare apparaten, gepauzeerde bindingen, profielmismatches en conflicten. De overlay toont de gekozen indeling en bindingen; de standaard vasthoudsneltoets is `Ctrl+Alt+F1`.

| Integratie | Vereisten en beperkingen |
| --- | --- |
| Corsair | Actieve iCUE, SDK-toegang en een compatibel model. G1–G6 en ondersteunde RGB-functies; bedrijfseigen Fn-, wiel-, vergrendelings- en profielbediening worden in het algemeen niet aangeboden via de G-key API. |
| Logitech G | Originele lokale x64 G-key/LED SDK-bibliotheken vereisen expliciete goedkeuring. G-key-gebeurtenissen identificeren meerdere identieke fysieke toetsenborden niet uniek. |
| Razer | Chroma RGB en standaardinvoer; wijs bedrijfseigen bedieningselementen zo nodig toe in Synapse. |
| SteelSeries | GameSense RGB en standaardinvoer; wijs bedrijfseigen bedieningselementen zo nodig toe in GG. |
| Wooting | Compatibele originele lokale x64 SDK voor analoge waarden en drempels; gewone HID-invoer blijft actief. |
| Andere toetsenborden | Standaardsneltoetsen of ondersteunde fysieke scancodebindingen. Fn en bedrijfseigen bediening hangen af van het model en de leveranciersconfiguratie. |

Fabrikantenonderzoek beschrijft gedocumenteerde mogelijkheden, geen geïmplementeerde of geteste compatibiliteit met elk vermeld apparaat. Nieuwe adapters vereisen nog acceptatietests met echte hardware. Download geen binaire SDK-bestanden van onbetrouwbare bronnen; SDK-distributierechten staan los van technische compatibiliteit.

## Audio, displays en OBS

Audiostappen stellen het volume in van 0 tot 100 procent en dempen ondersteunde uitvoer- of microfooneindpunten, heffen het dempen op of wisselen de dempstatus. Kies een specifiek eindpunt of de Windows-standaard. Als een opgeslagen doel ontbreekt, volgt een fout in plaats van stilzwijgend een ander apparaat te bedienen. Vernieuw de apparaten na opnieuw verbinden. Eindpuntbesturing betekent niet dat onafhankelijke mixerbesturing per toepassing wordt ondersteund.

Beeldschermacties verhogen/verlagen de helderheid met 10 procentpunten of stellen een waarde van 0 tot 100 in. Interne beeldschermen gebruiken ondersteunde WMI-interfaces; externe monitoren vereisen DDC/CI-ondersteuning en meestal inschakeling via het monitormenu. Docks, adapters en kabels kunnen de bediening blokkeren. Kies alle ondersteunde beeldschermen of één monitor. Opnieuw verbinden kan de beschikbare identiteiten veranderen; controleer het opgeslagen doel. Ontbrekende uitlezingen worden als niet beschikbaar getoond, niet als gemeten waarden.

OBS vereist dat WebSocket v5 is ingeschakeld via Hulpmiddelen in OBS. Configureer het eindpunt, test de verbinding en laad scène- en invoernamen voordat u opslaat. Ondersteunde acties zijn scènewisselingen, stream/opname starten/stoppen/wisselen, opname pauzeren/hervatten en invoer dempen. Wachtwoorden kunnen in Windows Credential Manager worden opgeslagen en worden niet opgenomen in configuratie-exports. Een geslaagde OBS-opdracht bewijst niet elk daaropvolgend streamingeffect.

## Resultaatverificatie en uitvoeringsgeschiedenis

Schakel Resultaat verifiëren in voor geschikte stappen. De app onderscheidt controles vooraf, bevestigde effecten, mislukkingen, annulering en effecten die niet kunnen worden geverifieerd. Het bestaan van een proces betekent niet dat een toepassing gereed is. Relatieve wijzigingen, sneltoetsen en tekstinvoer kunnen geen algemene succesbevestiging krijgen. Controleer het activiteitenlogboek en de verificatiegeschiedenis als een actie mislukt. Corrigeer configuratiefouten voordat u de volledige macro opnieuw uitvoert.

## UI-automatisering en klikopname

Voeg een UI Automation-stap toe, selecteer de bewerking en leg na de weergegeven vertraging het doel onder de muisaanwijzer vast. Alleen vastleggen voert niets uit. Controleer de selector en vereis een unieke overeenkomst. Ondersteunde bewerkingen zijn `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` en `WaitState`. Wachtwoordvelden, uitgeschakelde besturingselementen en niet-eenduidige selectors worden afgewezen.

UI-automatisering draait in een begrensd begeleidend proces. Dit hangt af van de Windows-toegankelijkheidsondersteuning en machtigingen van de doeltoepassing. De klikrecorder legt ondersteunde acties en pauzes vast; hij neemt geen algemene tekstinvoer op. Controleer en valideer het concept voordat u het overneemt. Interfacewijzigingen in een externe app kunnen selectors ongeldig maken. Procesisolatie begrenst crashes en annulering, maar is geen beveiligingssandbox.

## Plug-ins

Open Extensies om een pakket te inspecteren voordat u het importeert. Pakketstructuur, manifest en hashes worden gecontroleerd. Na import is het pakket uitgeschakeld. Lees de gedeclareerde acties en vertrouw de uitgever expliciet voordat u het inschakelt. Plug-ins draaien met uw gebruikersrechten in een afzonderlijk proces; dat proces is **geen sandbox**. Installeer alleen pakketten die u vertrouwt.

De beheerder ondersteunt exporteren, uitschakelen, verwijderen en het herstellen/verwerpen van een vorige versie. Ontbrekende afhankelijkheden blokkeren de aanroep. Het voorbeeldpakket biedt aftelacties en lokale statusacties. Het ondersteunde schema is een gedocumenteerde subset van JSON-Schema, niet elke mogelijke JSON-Schema-functie. Er is geen openbare marktplaats voor plug-ins.

## Mobiel paneel

1. Open Mobiel paneel, selecteer een lokaal IP-adres en kies expliciet de profielen die toegankelijk worden. Het standaard loopback-adres is alleen op de pc zelf bereikbaar; kies het juiste LAN-adres voor een telefoon of tablet.
2. Start het paneel en lees het HTTPS-adres, de certificaatvingerafdruk en de eenmalige zescijferige koppelcode.
3. Open het adres op een apparaat in hetzelfde vertrouwde netwerk. Vergelijk de SHA-256-vingerafdruk via een vertrouwd kanaal voordat u het lokale certificaat vertrouwt. De app installeert niet automatisch een vertrouwd certificaat.
4. Koppel met de weergegeven code en test één onschuldige tegel. Voor LAN-toegang kan handmatige Windows Firewall-toestemming nodig zijn. Stel de poort niet bloot aan internet.
5. Stop het paneel wanneer u klaar bent of trek de toegang van gekoppelde clients in via de app.

Privéserversleutels worden beveiligd met Windows DPAPI; clientreferenties worden als hashes opgeslagen. Een gewijzigd IP-adres of vernieuwd certificaat kan opnieuw vertrouwen vereisen. Het paneel toont actie- en verificatiestatussen, niet elke mogelijke live-sensor. Controles van de browserindeling en lokale HTTPS vervangen geen echte LAN-acceptatietests op telefoons/tablets.

## Lokale AI-vergaderassistent

Deze optionele experimentele assistent is standaard uitgeschakeld. Open hem vanuit Hulpmiddelen, schakel hem expliciet in, selecteer een runtime/model en voeg alleen de resources toe die hij mag meenemen. Geef het doel aan, maak een concept, controleer onopgeloste verzoeken en draag het resultaat over aan de gewone editor. Niets wordt automatisch uitgevoerd; opslaan en uitvoeren blijven expliciete acties. Er is geen automatische runtime-terugval.

| Looptijd | Installatie |
| --- | --- |
| Ollama | Afzonderlijke lokale dienst en een lokaal geïnstalleerd model op `127.0.0.1:11434`. Aangemelde externe/cloudmodellen worden afgewezen. |
| LM Studio | Lokale server met een lokaal geladen model en JSON-schema-ondersteuning op `127.0.0.1:1234`. Schakel externe routering niet in voor lokale verwerking. |
| llama.cpp | Lokale `llama-server` en compatibel model met JSON-schema-ondersteuning op `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Vertrouwde compatibele lokale GenAI-tekstmodelmap; selecteer `genai_config.json`. Inferentie gebruikt een gebundeld begeleidend proces zonder HTTP. |

HTTP-clients gebruiken alleen loopback, geen proxy’s en volgen geen omleidingen. MAKRIVO kan niet betrouwbaar vaststellen of een compatibele lokale server intern een externe dienst aanroept. Geauthenticeerde lokale servers zijn in deze versie niet configureerbaar. Modelgewichten, Python en lokale inferentieservers worden niet automatisch gedownload of geïnstalleerd.

ONNX-pakketten hebben `tokenizer.json` en een geldige chatsjabloon nodig. Ondersteunde sjabloonvormen in `tokenizer_config.json` zijn een tekenreeks `chat_template`, een woordenboek met een tekenreeks `default`, of een eenduidige lijst met benoemde/standaardsjablonen. Als de sjabloon ontbreekt of null is, wordt `chat_template.jinja` als terugval gebruikt. Lege, niet-eenduidige, te grote of verkeerd getypeerde sjablonen worden afgewezen. Willekeurige ONNX-bestanden, embedding-/visiemodellen en pakketten uitsluitend voor GPU zijn niet uitwisselbaar met compatibele GenAI-tekstmodellen. De gebundelde GenAI-runtimeversie is 0.17.1, CPU. Gebruik alleen vertrouwde modellen; processcheiding is geen sandbox.

De assistent ondersteunt geselecteerde programma’s zonder argumenten, geselecteerde documenten en websites en het dempen van de microfoon wanneer dit uitdrukkelijk wordt gevraagd. Hij maakt geen berichten, afspraken, willekeurige scripts, vrije klikacties of downloads en wisselt geen headset. Het doel en de resourcenamen worden naar de geselecteerde lokale runtime gestuurd; resourcepaden en bestandsinhoud worden niet afzonderlijk verstrekt. Een door de gebruiker ingevoerd doel kan zelf persoonsgegevens bevatten. Het eigen netwerkgedrag van een lokale runtime blijft de verantwoordelijkheid van de gebruiker.

Het ONNX-verzoekbudget is 90 seconden inclusief het laden van het model, maximaal 3072 prompttokens en 1024 gegenereerde tokens, daarnaast begrensd door de modelcontext. Verkeerd gevormde/afgekapte JSON en onbekende resource-ID’s worden afgewezen. Annuleer of sluit om een verzoek te beëindigen. Een runtimetest met een willekeurig model toont uitvoering aan, geen nuttige planningskwaliteit of tijdwinst. Lokale experimentlogboeken bevatten gebeurtenis-ID’s en tijdmetingen, geen doeltekst of documentinhoud; ze worden niet automatisch geüpload.

De latere evaluatie van een klein Instruct-model leverde nul geldige plannen op in vier Duitse/Engelse proeven. Native inferentie werkte, maar het model voldeed niet aan het strikte planningscontract. Dit is een negatief kwaliteitsresultaat, geen aanbevolen model of bewezen nuttige assistent. Zie het gedateerde testrapport over lokale runtimes voor de exacte modelrevisie en beperkingen.

## Importeren, exporteren, synchronisatie en back-up

Bekijk de importvoorbeelden voordat u een configuratie vervangt. Geïmporteerde acties zijn aanvankelijk uitgeschakeld en draagbare synchronisatie verwijdert hardwarespecifieke bindingen. Wijs apparaten opnieuw toe en schakel acties na controle bewust in. Controleer tekst- en actie-inhoud voordat u een export deelt; het uitsluiten van wachtwoorden maakt niet alle door gebruikers gemaakte actie-inhoud ongevoelig.

Profielsynchronisatie wisselt bestanden uit in een geselecteerde map, eventueel beheerd door uw bestaande cloudclient. MAKRIVO biedt geen cloudaccount en bewaart geen cloudreferenties. Revisies voorkomen stilzwijgend overschrijven. Het opnemen van actie-inhoud is een expliciete keuze. Maak een back-up van uw configuratie voordat u die door een import vervangt. De standaardconfiguratie is `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; de historische mapnaam blijft behouden voor compatibiliteit. Sluit de app voordat u deze handmatig kopieert/herstelt. Opgeslagen SDK-goedkeuringen en door het besturingssysteem beveiligde referenties moeten op een andere pc mogelijk opnieuw worden aangemaakt.

## Problemen oplossen

| Symptoom | Wat te controleren |
| --- | --- |
| Sneltoets doet niets | App actief, bindingen niet gepauzeerd, juist actief profiel, opgeslagen/ingeschakelde tegel, sneltoetsconflicten en beschikbaar doel. |
| Doelvenster kan niet worden geactiveerd | Activeer het handmatig. Windows-beperkingen op voorgrondactivering en verschillen in rechten kunnen automatisering blokkeren. |
| Speciale toets wordt niet gedetecteerd | Leveranciersdienst/SDK/modelondersteuning; configureer bedrijfseigen bedieningselementen zo mogelijk om een ondersteunde standaardtoets uit te sturen. |
| Tweede toetsenbord typt ook gewoon | Verwacht bij Raw Input. Exclusieve onderdrukking is niet geïmplementeerd voor productiegebruik. |
| Audio of helderheid niet beschikbaar | Vernieuw apparaten; controleer de gekozen identiteit, eindpuntsessies, WMI/DDC/CI-ondersteuning en dock-/kabelbeperkingen. |
| OBS offline | WebSocket v5 ingeschakeld, juist eindpunt/wachtwoord, bereikbare dienst en geldige scène-/invoernamen. |
| UI-automatisering mislukt | Begeleidende host meegeleverd, unieke geldige selector, toegankelijk ingeschakeld doel, ondersteund besturingselement en machtigingen. Probeer een tijdelijke time-out alleen opnieuw nadat u het doel hebt gecontroleerd. |
| Mobiel paneel onbereikbaar | LAN-adres in plaats van loopback, hetzelfde netwerk, actieve server, goedgekeurde profielen, certificaatvingerafdruk/vertrouwen en handmatige firewallmachtigingen. |
| Plug-in mislukt | Pakketintegriteit, expliciete vertrouwens-/inschakelstatus, afhankelijkheden, protocolcompatibiliteit en uitvoeringslogboek. |
| AI-verzoek mislukt | Juist geselecteerde lokale runtime, dienst/model gereed, ondersteund JSON-schema, vertrouwde compatibele modelmap en verzoeklimieten. |
| Lay-out of tekst lijkt verkeerd | Houd interfacetaal en toetsenbordgeometrie gescheiden; wijzig de venstergrootte, controleer de beeldschermschaal en meld de exacte pagina/taal/build. |

Vermeld voor een nuttig foutrapport de Windows-versie, app-build/commit, taal, beeldschermschaal, hardwaremodel, een minimale reeks stappen en het relevante geschoonde logboek. Verwijder persoonlijke paden, actie-inhoud en referenties voordat u publiceert.

## Bouwen, testen en verpakken

Gebruik de SDK-versie die is vastgezet in `global.json`. Vanuit de hoofdmap van de repository:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows-integratietests kunnen geïsoleerde vensters openen en ondersteunde leveranciersbindingen tijdelijk configureren/vrijgeven. Voer ze uit zonder een andere controller die deze bindingen beheert. De standaard helderheids-/audiotests lezen alleen; expliciete hardwaretests die waarden wijzigen en terugzetten staan apart. Mockprotocollen, gerenderde schermafbeeldingen en willekeurige inferentiegrafen mogen niet worden voorgesteld als echte hardware-/modelkwaliteitsacceptatie.

Het volledige publicatiehulpmiddel verzamelt begeleidende hosts, mobiele bestanden, de voorbeeldplug-in, de ONNX-runtime en beschikbare afhankelijkheidsverklaringen. Behoud de submappen. Een geslaagde build is geen ondertekend installatieprogramma of juridische releasegoedkeuring. Broncode en builds kunnen afzonderlijk evolueren; vergelijk de documentatiemomentopname en commit met de geïnstalleerde versie.

## Licenties, privacy en releasestatus

Het beoogde commerciële model is een eenmalige aankoop met blijvend gebruik van de gekochte hoofdversie; definitieve prijs, toegestaan aantal apparaten en upgradevoorwaarden moeten in de daadwerkelijke verkoopvoorwaarden worden vastgelegd. Dit handboek stelt geen productiegeschikt aankoop-/activeringssysteem vast. Voor dagelijkse bediening is geen cloudaccount vereist.

Verspreid geen bedrijfseigen binaire SDK-bestanden zonder aantoonbare toestemming. Bewaar licentieverklaringen en verklaringen van derden voor elke meegeleverde afhankelijkheid, inclusief .NET- en ONNX-pakketten. Modelgewichten hebben hun eigen voorwaarden. De gedateerde licentie-audit dateert van vóór sommige nieuwe afhankelijkheden en verleent geen toestemming voor die toevoegingen. Dat de repository openbaar is, verleent op zichzelf geen algemene opensourcelicentie.

Echte leveranciershardware, LAN-gebruik op telefoons/tablets, overgangen tussen monitoren/DPI-instellingen, langdurig gebruik, volledige taalkundige controle, exclusieve driverondersteuning en nuttige AI-planningskwaliteit vereisen nog afzonderlijke acceptatietests. Leid nooit goedkeuring af uit alleen de aanwezigheid van een functie.

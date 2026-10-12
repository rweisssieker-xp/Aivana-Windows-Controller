<!-- generated-doc locale=nl-NL source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../README.md)

# MAKRIVO

[Volledige documentatie in alle 27 talen](README.md) · [Volledige gebruikershandboek](HANDBOOK.md)

**door Aivana.** [De huidige inventaris en release beperkingen](FEATURES.md) · [Uitbreidingsverslag](testing/2026-10-12-six-extensions.md) · [Lokale AI assistent prototype](testing/2026-10-12-local-meeting-assistant.md)

**Engels (VS)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Een lokale Windows-sneltoetscontroller met tegels, profielen, macro's en toetsenborddetectie in Stream Deck-stijl. De interface ondersteunt 27-talen. Voor dagelijks gebruik is geen account of cloudverbinding vereist.

[Documentatie](README.md) · [Verificatie](VERIFICATION.md) · [Feature testverslag](testing/2026-10-09-usp-release.md)

## Aan de slag

Na publicatie bevindt de applicatie zich op `artifacts/win-x64/MAKRIVO.exe`. Houd de hele uitvoermap bij elkaar; je kunt het ergens anders kopiëren. Windows 11 x64 is het initiële doel. Build-artefacten zijn uitgesloten van Git; een bronafhandeling bevat geen vooraf gebouwd uitvoerbaar bestand.

Installeer de .NET 10 SDK die is opgegeven in `global.json` om vanaf de broncode te bouwen en uit te voeren:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publiceer een op zichzelf staande build zonder dat een afzonderlijk geïnstalleerde .NET-runtime nodig is:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Basiswerkstroom

1. Selecteer een profiel en maak een actietegel.
2. Stel de naam, het pictogram, de kleur en de sneltoets in. Ctrl/Alt/Shift-combinaties verminderen onbedoelde activering tijdens het typen.
3. Voeg een of meer stappen toe: applicaties, bestanden, websites, tekst, snelkoppelingen, media, volume, profielwisseling of vertragingen.
4. Opslaan. Wijzigingen blijven een concept totdat ze worden opgeslagen; negeren herstelt de opgeslagen versie. De tegel Afspeelknop en de sneltoets ervan gebruiken hetzelfde uitvoeringspad.
5. Herschik tegels en macrostappen opnieuw met slepen en neerzetten of met de pijlknoppen. Sjablonen vereenvoudigen de installatie. Macro's kunnen maximaal 100 keer worden herhaald. Annuleer standaard via de interface, het systeemvakmenu of Ctrl+Alt+Escape.

Toepassingsspecifieke profielen gebruiken een procesnaam zonder het volledige pad. Profielbestelling verbreekt de banden; het standaardprofiel is de fallback. Automatische profielselectie moet zijn ingeschakeld in Instellingen.

Als u het hoofdvenster sluit, blijft de app standaard in het systeemvak staan. Dubbelklik op het pictogram in het systeemvak om het opnieuw te openen; gebruik het lademenu om af te sluiten. Opstarten met Windows is optioneel en standaard uitgeschakeld. Sneltoetsen kunnen centraal worden gepauzeerd.

## Acties en opdrachtpalet

Actie toevoegen opent een doorzoekbare catalogus met 90-voorinstellingen in acht categorieën. Elk heeft een beschrijving en een voorbeeld. De macro-editor gebruikt dezelfde catalogus om stappen toe te voegen of te vervangen. Door een voorinstelling in deze editorworkflow te selecteren, wordt een concept gemaakt zonder uitvoering.

Het opdrachtenpalet wordt geopend vanuit de koptekst of met Ctrl+K in de app. Het doorzoekt opgeslagen acties in profielen, catalogusvoorinstellingen, wisselen tussen profielen, navigatie, pauzeren/hervatten, macro-annulering en importeren/exporteren. Filter op opgeslagen acties, catalogusfuncties of app-opdrachten. Met de pijltjestoetsen wordt geselecteerd, Enter wordt geactiveerd, Ctrl+Enter bewerkt een bewerkbaar item en Escape wordt gesloten. Favorieten verschijnen eerst. Kant-en-klare catalogusopdrachten kunnen onmiddellijk worden uitgevoerd; Voorinstellingen die instellingen vereisen, openen een concept. Instellingen kunnen een globale paletsnelkoppeling definiëren. Nieuwe configuraties gebruiken Ctrl+Shift+Spatie; bestaande configuraties krijgen niet automatisch een nieuwe globale snelkoppeling.

Audioacties stellen het volume nauwkeurig in, van 0 tot 100 procent, en dempen, dempen op of schakelen uitvoer- en microfoonapparaten. Kies een specifiek eindpunt of laat het niet ingesteld voor de standaard Windows. Een niet beschikbaar opgeslagen eindpunt veroorzaakt een fout in plaats van een ander apparaat te besturen. Apparaten vernieuwen laadt de aangesloten apparaten opnieuw.

OBS Studio gebruikt WebSocket v5: opname starten/stoppen/schakelen/pauzeren/hervatten; streamen starten/stoppen/schakelen; scènes veranderen; schakel de invoer dempen in. Schakel de WebSocket-server in onder Extra in OBS en stel het eindpunt ervan in de editor in. Check Connection and Load Names leest de OBS-versie, scènes en invoer zonder een opname of stream te starten. Kies namen uit de lijst of voer ze handmatig in. Wachtwoorden kunnen worden opgeslagen in Windows Credential Manager en worden uitgesloten van geëxporteerde configuraties. Niet beschikbare OBS-verbindingen melden een fout.

Profielen kunnen overeenkomen met een procesnaam en een deel van een venstertitel. Beide opgegeven voorwaarden moeten overeenkomen. Profiel vergrendelen voorkomt automatisch overschakelen voor de sessie. Voor het vastleggen van omstandigheden vanaf de voorgrond wordt de laatst gerichte externe toepassing gebruikt.

Tekst- en klembordacties ondersteunen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` en `{{user}}`. Datums gebruiken `yyyy-MM-dd`; keer gebruik `HH:mm`. Onbekende tijdelijke aanduidingen blijven ongewijzigd. `{{profile}}` identificeert het profiel van de gelanceerde actie; `{{process}}` identificeert de externe applicatiecontext. Klembordinhoud is alleen-lezen als die tijdelijke aanduiding wordt gebruikt. Met de klembordactie wordt tekst geschreven, maar deze wordt niet automatisch geplakt.

Vensteracties minimaliseren, maximaliseren, herstellen of rangschikken een venster aan de linker-/rechterkant van het werkgebied van de monitor. De systeemactie vergrendelt Windows. Deze acties worden alleen uitgevoerd als ze expliciet worden aangeroepen.

Voorbeeld toont stappen, herhalingen en variabelen zonder uitvoering. Ongedaan maken en Opnieuw uitvoeren behouden maximaal 100-conceptstatussen; tekstvakken behouden de oorspronkelijke Ctrl+Z-bewerking. Markeer favorieten in de editor of het tegelmenu en filter ze op het dashboard. Het aantal uitvoeringen geldt alleen voor de huidige sessie.

Tekst-, snelkoppelings- en vensteracties die vanuit de interface worden gestart, activeren het laatste externe doelvenster. Het palet geeft die doelcontext weer. Als het doel is gesloten of Windows de activering weigert, stopt de uitvoering met een bericht.

## Toetsenbordhulp en feedback

De wizard Speciale toetsen bevindt zich op de toetsenbordpagina en in het palet. Het vermeldt de G-toetsen gerapporteerd door iCUE. Detect Key Press legt ze tijdelijk vast zonder de toegewezen acties uit te voeren. Normale bindingen keren terug na detectie, annulering of sluiting. Een catalogusfunctie en geselecteerde sleutel openen als een niet-opgeslagen concept; dubbele bindingen worden gecontroleerd.

De toetsenbordoverlay toont de geselecteerde referentielay-out en actieve bindingen. Houd Ctrl+Alt+F1 ingedrukt of schakel deze in via het palet of de toetsenbordpagina. Het vergt geen focus en laat muisklikken door. Profiel- en bindingswijzigingen worden bijgewerkt terwijl ze zichtbaar zijn; gewone Windows-toetsaanslagen zijn gemarkeerd. Fabrikantspecifieke bedieningselementen leveren mogelijk geen gelijkwaardig Windows-sleutelsignaal.

Uitvoeringsfeedback toont wachten, voortgang van stappen/herhalingen, voltooiing, annulering of mislukking zonder de focus te verliezen of de ingevoerde tekst bloot te leggen. Uitgevoerde opdrachten bevestigt de uitvoering van opdrachten, en niet een onafhankelijk opgevraagde livestatus in een toepassing van derden. Foutdetails verschijnen in het activiteitenlogboek.

## Apparaten en Corsair

Corsair G1–G6 kan rechtstreeks worden toegewezen via de officiële iCUE SDK. Selecteer het apparaat en de sleutel in de actie-editor en sla het op, of maak een concept vanuit de K100-toetsenbordweergave. Bindingen zijn van toepassing op het geselecteerde apparaat en actieve profiel; pauze schakelt ze ook uit. iCUE moet actief zijn en toegang toestaan. Fn, de iCUE wiel-, profiel- en vergrendelingsbedieningen kunnen niet in kaart worden gebracht via deze G-key API.

De pagina Apparaten vermeldt Windows-toetsenbordinterfaces en wordt periodiek vernieuwd en wanneer apparaten verbinding maken. Een fysiek toetsenbord kan meerdere HID-interfaces blootleggen. Standaard wereldwijde sneltoetsen werken voor alle fabrikanten.

Corsair-detectie maakt gebruik van beschikbare apparaatinformatie. RGB-besturing vereist compatibele hardware, met iCUE, en de officiële iCUE SDK; de app rapporteert de daadwerkelijke beschikbaarheid. Andere fabrikanten ontvangen momenteel detectie- en standaardacties. Buiten Corsair G-sleutelintegratie werken eigen bedieningselementen wanneer Windows deze als reguliere invoer ontvangt. De app installeert geen toetsenbordstuurprogramma.

## Talen

Instellingen biedt 27-talen: Duits, Engels, Frans, Spaans, Italiaans, Portugees, Nederlands, Pools, Tsjechisch, Slowaaks, Hongaars, Roemeens, Bulgaars, Grieks, Deens, Zweeds, Noors, Fins, Ests, Lets, Litouws, Sloveens, Kroatisch, Oekraïens, Russisch, Turks en Servisch in Latijns schrift. Automatisch volgt de weergavetaal Windows. Vertalingen worden lokaal gebundeld. Lay-outs, sneltoetsen en door de gebruiker gemaakte namen zijn onafhankelijk van de interfacetaal.

De volledige documentatie van de repository is beschikbaar in alle 27 toepassingstaal via de [Taalindex](README.md)Het is De vertalingen zijn machine-assisted, met originele bronnen en structurele controles; de native speaker review is gescheiden. De app accepteert ook de regionale codes `en-US`, `de-DE`, `fr-FR` en `it-IT` en gebruikt hun nummers. De bestaande taalcodes blijven compatibel.

## Lay-outs en fabrikantonderzoek

De toetsenbordweergave biedt ISO-DE/ANSI-US-referenties op volledige grootte en zonder toetsen plus Corsair K100 RGB-indelingen. Geometrie vertegenwoordigt posities, brede/hoge toetsen en ISO Enter. Standaardlay-outs zijn generieke referenties. K100-indelingen volgen gedocumenteerde bedieningselementen zonder aanspraak te maken op millimeternauwkeurige afmetingen. Kies de juiste taal/geometrievariant: een modelnaam of USB VID/PID vestigt geen specifieke variant.

Mapping lost fysieke scancodes op met behulp van de actieve Windows-invoerlay-out. De workflow bereidt een concept voor en behoudt controles op het gebied van opslaan/conflicten. Standaardbindingen zijn algemene Windows-sneltoetsen, niet geïsoleerd op één aangesloten toetsenbord. Corsair G1–G6 gebruik apparaatspecifieke iCUE-integratie. Andere bedrijfseigen bedieningselementen vereisen een adapter of door de fabrikant geconfigureerde snelkoppelingen. Hoofd-Enter en numeriek toetsenblok Enter delen een virtuele sleutelidentiteit voor globale sneltoetsen.

De [Fabrikant onderzoek](research/tastaturhersteller-sonderfunktionen.md) en [CSV](research/tastaturhersteller-sonderfunktionen.csv) de 74 merken/fabrikanten groepen, onderscheidende configuratoren, input SDK's en verlichting API's. [Layout bewijs](research/keyboard-layout-evidence.md) Model, SKU en module verschillen. Onderzoekscovering betekent niet dat hardware ondersteuning wordt geïmplementeerd.

## Gegevens

De configuratie wordt opgeslagen in lokale applicatiegegevens. Import/export gebruik JSON. Importeren geeft een voorbeeld van de configuratie weer, vervangt de huidige configuratie na bevestiging en schakelt geïmporteerde bindingen uit totdat ze handmatig worden ingeschakeld. Tekstactie-inhoud wordt niet verzameld als toetsaanslaglogboek. Credential Manager-wachtwoorden staan ​​los van geëxporteerde profielen.

## Verificatie

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows-controles kunnen hardware inspecteren en ondersteunde iCUE-bindingen tijdelijk configureren/vrijgeven. Voer ze uit zonder een andere controller met behulp van die bindingen. Audio-eindpuntcontroles hebben geen invloed op het livevolume of de mute-instellingen.

Native invoer kan de Windows-rechten niet omzeilen. Een normaal gestarte controller verzendt mogelijk geen invoer naar een beheerderstoepassing. Gereserveerde of bezette sneltoetsen verschijnen als conflicten.

De [Documentatie index](README.md) Link historische ontwerpen, plannen, onderzoek en getagde testrapporten. Historische plannen beschrijven hun geregistreerde omvang, niet een huidige garantie van functie ondersteuning.

### Workflow-sjablonen en wachttijden voor doelvensters

De catalogus bevat zes bewerkbare workflows: werkruimte, vergadering, streaming, onderzoek, projectwisseling en tekstfragment. Zoek naar `workflow.` in de catalogus of het opdrachtenpalet. Als u een workflow selecteert, wordt een niet-opgeslagen concept geopend; het loopt nooit onmiddellijk. Alle stappen blijven behouden wanneer u een workflow toevoegt aan een bestaande macro of deze toewijst aan een Corsair-sleutel. Configureer voorbeeld-URL's, programmapaden, profielen en OBS-scènes voordat u deze opslaat.

De stap **Wachten op doelvenster** accepteert een procesnaam (met of zonder `.exe`), een hoofdletterongevoelige titelsubtekenreeks en een time-out van 1–600,000 ms. Er is minimaal één filter vereist; beide filters moeten bij levering overeenkomen. De macro gaat pas verder nadat een passend zichtbaar venster reageert en de voorgrond bereikt. Gebruik een specifieke titel wanneer meerdere vensters een proces delen. Annulering of time-out stopt de volgende stappen. Venstergereedheid garandeert niet dat een document is geladen of dat een externe service zoals OBS WebSocket is aangesloten.

Schermhelderheid: zoek naar ** Schermhelderheid verhogen **, ** Schermhelderheid verlagen ** of ** Schermhelderheid op 50 procent** in de actiecatalogus. Relatieve acties veranderen de helderheid met 10 procentpunten; vaste waarden variëren van 0 tot 100. Ondersteunde interne beeldschermen gebruiken WMI; Voor compatibele externe monitoren is DDC/CI vereist in hun menu. Standaard heeft de actie invloed op alle ondersteunde beeldschermen; in de editor kan een expliciete monitor worden geselecteerd. Verbindingen en docks kunnen de ondersteuning beperken.

### Uitgebreide controles

Open het opdrachtenpalet met **Ctrl+K terwijl het app-venster focus** heeft. Zoek naar **Workflowontwerper**, **Record macro**, **Profielen synchroniseren** of **Sleuteldiagnostiek** (de tekst volgt de door u geselecteerde taal). De workflow-ontwerper bewerkt het geselecteerde tegelconcept: bestand-/proces-/venstervoorwaarden, aantal overgeslagen stappen en foutafhandeling stoppen/doorgaan. De recorder legt snelkoppelingsakkoorden vast en wacht, geen gewoon typen; Escape stopt met opnemen. Controleer het resultaat voordat u het overneemt.

Selecteer in een helderheidsstap alle ondersteunde beeldschermen of een specifieke monitor. Een niet-beschikbare geselecteerde monitor produceert een fout in plaats van stilletjes een ander beeldscherm te wijzigen. Het dashboard toont live helderheid, audio en OBS-metingen wanneer hun respectieve integraties beschikbaar zijn; een niet-beschikbare meting wordt niet als meetwaarde weergegeven.

Wijs in de tegeleditor een ondersteunde fysieke speciale toets toe en kies voor onmiddellijk indrukken, tikken, vasthouden of dubbeltikken. Vastgehouden laagkiezers activeren tijdelijk een andere mapping en herstellen de vorige laag zodra deze wordt vrijgegeven. Gebaren en lagen zijn van toepassing op Corsair SDK G-toetsen en apparaatspecifieke standaard speciale toetsen, niet op gewone globale sneltoetsen. Het vastleggen van standaardsleutels ondersteunt F13–F24 en mediatoetsen op het toetsenbord. Voor eigen Fn-, profiel- en kiesbedieningen is nog steeds een compatibele leveranciersadapter of een toewijzing van een leverancier aan een standaardsleutel vereist. Raw Input observeert de sleutel en onderdrukt het normale Windows-gedrag niet. Belangrijke diagnostiek verklaart profiel-, pauze-, verbindings- en bindingsproblemen.

Profielsynchronisatie wisselt bestanden uit via een map die u kiest, inclusief een map die wordt beheerd door uw bestaande cloudclient. Bekijk het voorbeeld voordat u importeert; import start uitgeschakeld en verwijdert hardwarespecifieke toewijzingen. Revisies voorkomen stille overschrijvingen, en het opnemen van actie-inhoud is een expliciete keuze. MAKRIVO levert geen cloudaccount en bewaart geen cloudreferenties. De lade gebruikt nu het blauwgroen M-pictogram van MAKRIVO en toont een oranje pauzebadge wanneer deze wordt gepauzeerd.

<!-- generated-doc locale=nl-NL source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../README.md)

# MAKRIVO

[Volledige documentatie in alle 27 talen](README.md) · [Volledige gebruikershandboek](HANDBOOK.md)

**door Aivana.** [De huidige inventaris en release beperkingen](FEATURES.md) · [Uitbreidingsverslag](testing/2026-10-12-six-extensions.md) · [Lokale AI assistent prototype](testing/2026-10-12-local-meeting-assistant.md)

**Engels (VS)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Een lokale Windows-sneltoetscontroller met tegels in Stream Deck-stijl, profielen, macro’s en toetsenborddetectie. De interface ondersteunt 27 talen. Voor dagelijks gebruik is geen account of cloudverbinding vereist.

[Documentatie](README.md) · [Verificatie](VERIFICATION.md) · [Feature testverslag](testing/2026-10-09-usp-release.md)

## Aan de slag

Na publicatie staat de toepassing op `artifacts/win-x64/MAKRIVO.exe`. Houd de volledige uitvoermap bij elkaar; u kunt die elders kopiëren. Windows 11 x64 is het eerste doelplatform. Buildbestanden zijn uitgesloten van Git; een broncodecheckout bevat geen vooraf gebouwd uitvoerbaar bestand.

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
2. Stel naam, pictogram, kleur en sneltoets in. Ctrl/Alt/Shift-combinaties verminderen onbedoelde activering tijdens het typen.
3. Voeg een of meer stappen toe: toepassingen, bestanden, websites, tekst, sneltoetsen, media, volume, profielwisselingen of vertragingen.
4. Opslaan. Wijzigingen blijven een concept totdat ze worden opgeslagen; verwerpen herstelt de opgeslagen versie. De afspeelknop van de tegel en de sneltoets gebruiken hetzelfde uitvoeringspad.
5. Herschik tegels en macrostappen door slepen of met de pijlknoppen. Sjablonen vereenvoudigen de inrichting. Macro’s kunnen tot 100 keer worden herhaald. Annuleer via de interface, het systeemvakmenu of standaard Ctrl+Alt+Escape.

Toepassingsspecifieke profielen gebruiken een procesnaam zonder volledig pad. De profielvolgorde beslist bij gelijke overeenkomsten; het standaardprofiel is de terugval. Automatische profielselectie moet in Instellingen worden ingeschakeld.

Het hoofdvenster sluiten houdt de app standaard actief in het systeemvak. Dubbelklik op het systeemvakpictogram om te heropenen; sluit af via het systeemvakmenu. Starten met Windows is optioneel en standaard uitgeschakeld. Sneltoetsen kunnen centraal worden gepauzeerd.

## Acties en opdrachtpalet

Actie toevoegen opent een doorzoekbare catalogus met 90 voorinstellingen in acht categorieën. Elk heeft een beschrijving en voorbeeldweergave. De macro-editor gebruikt dezelfde catalogus om stappen toe te voegen of te vervangen. Een voorinstelling kiezen in deze editorworkflow maakt een concept zonder uitvoering.

Het opdrachtenpalet opent vanuit de kop of met Ctrl+K in de app. Het doorzoekt opgeslagen acties in alle profielen, catalogusvoorinstellingen, profielwisselingen, navigatie, pauzeren/hervatten, macroannulering en import/export. Filter op opgeslagen acties, catalogusfuncties of appopdrachten. Pijltjestoetsen selecteren, Enter activeert, Ctrl+Enter bewerkt een bewerkbaar item en Escape sluit. Favorieten verschijnen eerst. Kant-en-klare catalogusopdrachten kunnen meteen worden uitgevoerd; voorinstellingen die inrichting vereisen openen een concept. Instellingen kunnen een globale paletsneltoets definiëren. Nieuwe configuraties gebruiken Ctrl+Shift+Space; bestaande configuraties krijgen niet automatisch een nieuwe globale sneltoets.

Audioacties stellen het volume precies in van 0 tot 100 procent en dempen uitvoer- en microfoonapparaten, heffen demping op of wisselen dempstatus. Kies een specifiek eindpunt of laat het leeg voor de Windows-standaard. Een onbeschikbaar opgeslagen eindpunt geeft een fout in plaats van een ander apparaat te bedienen. Apparaten vernieuwen herlaadt de aangesloten apparaten.

OBS Studio gebruikt WebSocket v5: opname starten/stoppen/wisselen/pauzeren/hervatten; streaming starten/stoppen/wisselen; scènes wijzigen; demping van invoer wisselen. Schakel de WebSocket-server in onder Hulpmiddelen in OBS en stel het eindpunt in de editor in. Verbinding controleren en Namen laden lezen de OBS-versie, scènes en invoer zonder opname of stream te starten. Kies namen uit de lijst of voer ze handmatig in. Wachtwoorden kunnen in Windows Credential Manager worden opgeslagen en worden uitgesloten van geëxporteerde configuraties. Onbeschikbare OBS-verbindingen melden een fout.

Profielen kunnen overeenkomen met een procesnaam en een deel van een venstertitel. Beide opgegeven voorwaarden moeten overeenkomen. Profiel vergrendelen voorkomt automatisch wisselen tijdens de sessie. Voorwaarden vastleggen vanaf voorgrond gebruikt de laatste externe toepassing die focus had.

Tekst- en klembordacties ondersteunen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` en `{{user}}`. Datums gebruiken `yyyy-MM-dd`; tijden gebruiken `HH:mm`. Onbekende placeholders blijven ongewijzigd. `{{profile}}` identificeert het profiel van de gestarte actie; `{{process}}` identificeert de externe toepassingscontext. Klembordinhoud wordt alleen gelezen wanneer die placeholder wordt gebruikt. De klembordactie schrijft tekst maar plakt niet automatisch.

Vensteracties minimaliseren, maximaliseren, herstellen of plaatsen een venster aan de linker-/rechterkant van het werkgebied van de monitor. De systeemactie vergrendelt Windows. Deze acties worden alleen uitgevoerd wanneer ze expliciet worden aangeroepen.

Voorbeeldweergave toont stappen, herhalingen en variabelen zonder uitvoering. Ongedaan maken en Opnieuw bewaren tot 100 conceptstatussen; tekstvakken behouden de eigen Ctrl+Z-bewerking. Markeer favorieten in de editor of het tegelmenu en filter ze op het dashboard. Uitvoeringstellingen gelden alleen voor de huidige sessie.

Tekst-, sneltoets- en vensteracties die vanuit de interface worden gestart, activeren het laatste externe doelvenster. Het palet toont die doelcontext. Als het doel gesloten is of Windows activering weigert, stopt de uitvoering met een melding.

## Toetsenbordhulp en feedback

De wizard voor speciale toetsen staat op de toetsenbordpagina en in het palet. Hij vermeldt de door iCUE gemelde G-toetsen. Toetsaanslag detecteren legt die tijdelijk vast zonder hun toegewezen acties uit te voeren. Normale bindingen keren terug na detectie, annulering of sluiten. Een catalogusfunctie en geselecteerde toets openen als niet-opgeslagen concept; dubbele bindingen worden gecontroleerd.

De toetsenbordoverlay toont de gekozen referentie-indeling en actieve bindingen. Houd Ctrl+Alt+F1 ingedrukt of schakel hem in/uit via het palet of de toetsenbordpagina. Hij neemt geen focus over en laat muisklikken door. Profiel- en bindingswijzigingen worden zichtbaar bijgewerkt; gewone Windows-toetsaanslagen worden gemarkeerd. Fabrikantspecifieke bediening geeft mogelijk geen gelijkwaardig Windows-toetssignaal.

Uitvoeringsfeedback toont wachten, stap-/herhalingsvoortgang, voltooiing, annulering of mislukking zonder focus over te nemen of ingevoerde tekst te onthullen. Opdrachten uitgevoerd bevestigt opdrachtuitvoering, geen onafhankelijk opgevraagde livestatus in een toepassing van derden. Foutdetails staan in het activiteitenlogboek.

## Apparaten en Corsair

Corsair G1–G6 kunnen rechtstreeks via de officiële iCUE SDK worden toegewezen. Selecteer apparaat en toets in de actie-editor en sla op, of bereid een concept voor vanuit de K100-toetsenbordweergave. Bindingen gelden voor het geselecteerde apparaat en actieve profiel; pauzeren schakelt ze ook uit. iCUE moet actief zijn en toegang toestaan. Fn, het iCUE-wiel, profiel- en vergrendelingsbediening kunnen niet via deze G-key API worden toegewezen.

De pagina Apparaten vermeldt Windows-toetsenbordinterfaces en wordt periodiek vernieuwd en wanneer apparaten verbinding maken. Een fysiek toetsenbord kan meerdere HID-interfaces blootleggen. Standaard wereldwijde sneltoetsen werken voor alle fabrikanten.

Corsair-detectie gebruikt beschikbare apparaatinformatie. RGB-besturing vereist compatibele hardware, actieve iCUE en de officiële iCUE SDK; de app meldt de werkelijke beschikbaarheid. Andere fabrikanten krijgen momenteel detectie en standaardacties. Buiten Corsair G-key-integratie werken bedrijfseigen bedieningselementen wanneer Windows ze als gewone invoer ontvangt. De app installeert geen toetsenborddriver.

## Talen

Instellingen biedt 27 talen: Duits, Engels, Frans, Spaans, Italiaans, Portugees, Nederlands, Pools, Tsjechisch, Slowaaks, Hongaars, Roemeens, Bulgaars, Grieks, Deens, Zweeds, Noors, Fins, Estisch, Lets, Litouws, Sloveens, Kroatisch, Oekraïens, Russisch, Turks en Servisch in Latijns schrift. Automatisch volgt de Windows-weergavetaal. Vertalingen worden lokaal meegeleverd. Indelingen, sneltoetsen en door gebruikers gemaakte namen zijn onafhankelijk van de interfacetaal.

De volledige documentatie van de repository is beschikbaar in alle 27 toepassingstaal via de [Taalindex](README.md). Vertalingen zijn machinaal ondersteund, met links naar de oorspronkelijke bron en structurele controles; beoordeling door moedertaalsprekers staat apart. De app accepteert ook de regiocodes `en-US`, `de-DE`, `fr-FR` en `it-IT` en gebruikt hun getalnotaties. Bestaande taalcodes blijven compatibel.

## Lay-outs en fabrikantonderzoek

De toetsenbordweergave biedt ISO-DE/ANSI-US-referenties op volledig formaat en zonder numeriek blok, plus Corsair K100 RGB-indelingen. De geometrie toont posities, brede/hoge toetsen en ISO Enter. Standaardindelingen zijn generieke referenties. K100-indelingen volgen gedocumenteerde bedieningselementen zonder millimeternauwkeurige afmetingen te claimen. Kies de juiste taal-/geometrievariant: een modelnaam of USB VID/PID bepaalt geen specifieke variant.

Toewijzing bepaalt fysieke scancodes met de actieve Windows-invoerindeling. De workflow bereidt een concept voor en behoudt opslag-/conflictcontroles. Standaardbindingen zijn globale Windows-sneltoetsen, niet geïsoleerd tot één aangesloten toetsenbord. Corsair G1–G6 gebruiken apparaatspecifieke iCUE-integratie. Andere bedrijfseigen bedieningselementen vereisen een adapter of door de fabrikant geconfigureerde sneltoetsen. Hoofd-Enter en Enter op het numerieke toetsenblok delen voor globale sneltoetsen dezelfde virtuele toetsidentiteit.

De [Fabrikant onderzoek](research/tastaturhersteller-sonderfunktionen.md) en [CSV](research/tastaturhersteller-sonderfunktionen.csv) omvatten 74 merk-/fabrikantgroepen en onderscheiden configurators, invoer-SDK’s en verlichtings-API’s. [Layout bewijs](research/keyboard-layout-evidence.md) legt verschillen in model, SKU en modules vast. Onderzoeksdekking betekent niet dat hardwareondersteuning geïmplementeerd is.

## Gegevens

Configuratie wordt opgeslagen in lokale toepassingsgegevens. Import/export gebruikt JSON. Import toont een voorbeeld van de configuratie, vervangt na bevestiging de huidige configuratie en schakelt geïmporteerde bindingen uit totdat ze handmatig worden ingeschakeld. De inhoud van tekstacties wordt niet als toetsaanslaglogboek verzameld. Credential Manager-wachtwoorden staan los van geëxporteerde profielen.

## Verificatie

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows-controles kunnen hardware inspecteren en ondersteunde iCUE-bindingen tijdelijk configureren/vrijgeven. Voer ze uit zonder een andere controller die deze bindingen gebruikt. Controles van audio-eindpunten wijzigen geen actueel volume of dempinstellingen.

Native invoer kan Windows-rechten niet omzeilen. Een normaal gestarte controller kan mogelijk geen invoer naar een beheerderstoepassing sturen. Gereserveerde of bezette sneltoetsen verschijnen als conflicten.

De [Documentatie index](README.md) Link historische ontwerpen, plannen, onderzoek en getagde testrapporten. Historische plannen beschrijven hun geregistreerde omvang, niet een huidige garantie van functie ondersteuning.

### Workflow-sjablonen en wachttijden voor doelvensters

De catalogus bevat zes bewerkbare workflows: werkplek, vergadering, streaming, onderzoek, projectwisseling en tekstfragment. Zoek naar `workflow.` in de catalogus of het opdrachtenpalet. Een workflow selecteren opent een niet-opgeslagen concept; hij wordt nooit meteen uitgevoerd. Alle stappen blijven behouden wanneer u een workflow toevoegt aan een bestaande macro of toewijst aan een Corsair-toets. Configureer voorbeeld-URL’s, programmapaden, profielen en OBS-scènes vóór het opslaan.

De stap **Wachten op doelvenster** accepteert een procesnaam (met of zonder `.exe`), een hoofdletterongevoelig deel van de titel en een time-out van 1–600000 ms. Minstens één filter is vereist; als beide worden opgegeven, moeten beide overeenkomen. De macro gaat pas verder nadat een overeenkomend zichtbaar venster reageert en op de voorgrond komt. Gebruik een specifieke titel als meerdere vensters hetzelfde proces delen. Annulering of time-out stopt de volgende stappen. Een gereed venster garandeert niet dat een document geladen is of een externe dienst zoals OBS WebSocket verbonden is.

Schermhelderheid: zoek naar **Schermhelderheid verhogen**, **Schermhelderheid verlagen** of **Schermhelderheid op 50 procent** in de actiecatalogus. Relatieve acties wijzigen de helderheid met 10 procentpunten; vaste waarden lopen van 0 tot 100. Ondersteunde interne schermen gebruiken WMI; compatibele externe monitoren vereisen ingeschakelde DDC/CI in hun menu. Standaard beïnvloedt de actie alle ondersteunde schermen; een specifieke monitor kan in de editor worden gekozen. Verbindingen en docks kunnen ondersteuning beperken.

### Uitgebreide controles

Open het opdrachtenpalet met **Ctrl+K terwijl het appvenster focus heeft**. Zoek naar **Workflowontwerper**, **Macro opnemen**, **Profielen synchroniseren** of **Toetsdiagnostiek** (de tekst volgt uw gekozen taal). De workflowontwerper bewerkt het geselecteerde tegelconcept: bestands-/proces-/venstervoorwaarden, aantallen vooruit over te slaan stappen en stoppen/doorgaan bij fouten. De recorder legt sneltoetscombinaties en wachttijden vast, geen gewone tekstinvoer; Escape stopt de opname. Controleer het resultaat voordat u het overneemt.

Selecteer in een helderheidsstap alle ondersteunde beeldschermen of een specifieke monitor. Een niet-beschikbare geselecteerde monitor produceert een fout in plaats van stilletjes een ander beeldscherm te wijzigen. Het dashboard toont live helderheid, audio en OBS-metingen wanneer hun respectieve integraties beschikbaar zijn; een niet-beschikbare meting wordt niet als meetwaarde weergegeven.

Wijs in de tegeleditor een ondersteunde fysieke speciale toets toe en kies direct indrukken, tikken, vasthouden of dubbeltikken. Laagkiezers die worden vastgehouden activeren tijdelijk een andere toewijzing en herstellen bij loslaten de vorige laag. Gebaren en lagen gelden voor Corsair SDK G-toetsen en apparaatspecifieke standaard speciale toetsen, niet voor gewone globale sneltoetsen. Standaardtoetsregistratie ondersteunt F13–F24 en mediatoetsen. Bedrijfseigen Fn-, profiel- en draaiknopbediening vereisen nog een compatibele leveranciersadapter of leverancierstoewijzing aan een standaardtoets. Raw Input observeert de toets en onderdrukt het normale Windows-gedrag niet. Toetsdiagnostiek verklaart profiel-, pauze-, verbindings- en bindingsproblemen.

Profielsynchronisatie wisselt bestanden uit via een map die u kiest, ook een map beheerd door uw bestaande cloudclient. Controleer het voorbeeld vóór import; importen starten uitgeschakeld en verwijderen hardwarespecifieke toewijzingen. Revisies voorkomen stilzwijgend overschrijven en het opnemen van actie-inhoud is een expliciete keuze. MAKRIVO biedt geen cloudaccount en bewaart geen cloudreferenties. Het systeemvak gebruikt nu MAKRIVO’s eigen blauwgroene M-pictogram en toont bij pauzeren een amberkleurige pauzebadge.

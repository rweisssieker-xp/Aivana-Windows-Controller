<!-- generated-doc locale=nl-NL source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Concept

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Status: 8. Oktober 2026. Status: ter beoordeling vóór implementatie.

## Doel en overeengekomen reikwijdte

Een zelfstandige Windows-app verandert een normaal pc-toetsenbord in een vrij configureerbaar actiepaneel, vergelijkbaar met een streamdeck. De gebruiker bevestigde alle voorgestelde actietypen, macro's, apparaatdetectie, profielen en een goede GUI. Aangesloten Corsair en andere toetsenborden worden herkend; extra fabrikantfuncties worden alleen aangeboden als ze daadwerkelijk worden ondersteund.

Aannames: Duitse interface, lokaal gebruik zonder account, Windows 11 x64 als eerste doel, geen installatie van stuurprogramma's. Het toetsenbord blijft bruikbaar voor normaal typen. Standaard gebruiken globale toewijzingen sneltoetsen met modifiers. Individuele lettertoetsen zijn standaard niet globaal gereserveerd.

## Technische beslissing

Native C#/.NET-10-App met WPF en MVVM. In de werkruimte is de stabiele .NET-10-SDK aanwezig. WPF maakt een aangepaste desktopinterface en directe Windows-integratie mogelijk. Alternatieven zijn een webinterface met een aparte Windows-service of een platformonafhankelijke desktop-app; beide brengen extra runtime en communicatiecomplexiteit met zich mee voor dit Windows-project. De native variant is dan ook de gekozen basis.

Divisie: Desktopproject voor views en tray, kernproject voor configuratie- en uitvoeringslogica, Windows-project voor sneltoetsen, invoersimulatie en apparaten, testproject voor platformonafhankelijke regels. Fabrikantadapters zijn afhankelijk van een vaste capaciteitsinterface en zijn onafhankelijk van de standaardacties.

## oppervlak

Donker design met rustige antracietkleurige vlakken, goed leesbaar lettertype en turquoise accenten. Linksnavigatie voor dashboard, profielen, apparaten en instellingen. Actief profiel, zoeken en schakelen om alle sneltoetsen bovenaan te pauzeren. De interface blijft bruikbaar bij 100–200 procent Windows schaling.

Het dashboard bevat een aanpasbaar tegelraster. Elke tegel toont het pictogram, de naam, de sneltoets en de uitvoeringsstatus. Klikken voert dezelfde actie uit als de sneltoets. Aan de rechterkant wordt een editor geopend met actietype, parameters, sneltoetsopname, symbool/kleur, activering en testknop. Nieuw, Dupliceren, Verwijderen en Opnieuw ordenen zijn beschikbaar. Een toetsenbordweergave toont schematisch de toewijzingen voor de standaard Duitse lay-out; het claimt niet de exacte geometrie van het gedetecteerde apparaat.

Profielen kunnen handmatig worden aangemaakt, gekopieerd, hernoemd en geactiveerd. Optioneel wordt aan een profiel een procesnaam toegewezen. Een geschikt voorgrondprogramma activeert zijn profiel, anders geldt het globale standaardprofiel. Als er meerdere overeenkomende profielen zijn, geldt een expliciet zichtbare volgorde. De editor legt sneltoetsconflicten direct bij de opdracht uit.

## Sneltoetsen en ingangen

Windows RegisterHotKey verwerkt normale globale combinaties waarbij toetsherhaling wordt onderdrukt. Alleen het actieve profiel wordt geregistreerd. Gereserveerde of reeds bezette combinaties verschijnen als fouten; de app rapporteert het niet als succesvol geactiveerd. Opname in de editor veroorzaakt geen acties.

Vóór gesimuleerde invoer worden de triggerende modificatietoetsen losgelaten of wordt gewacht totdat ze fysiek worden vrijgegeven. Unicode-tekst en toetscombinaties worden verzonden met SendInput. Elke toets die u indrukt, wordt weer vrijgegeven, zelfs als u annuleert. Acties worden buiten de UI-thread uitgevoerd; Uitvoering en profielwijzigingen mogen de ondergrond niet blokkeren.

## Acties en macro's

Actietypen: Programma starten met argumenten en werkmap, bestand/map openen via Windows, HTTP/HTTPS-website openen, Unicode-tekst invoeren, toetscombinatie verzenden, media afspelen/pauzeren/volgende/vorige/stoppen, volume omhoog/omlaag/mute en van profiel wisselen. Parameters worden gevalideerd voordat ze worden opgeslagen en opnieuw vóór uitvoering. Programma's worden direct gestart; Argumenten worden niet impliciet geïnterpreteerd als shell-opdrachten.

Macro's bevatten een geordende lijst met benoemde acties en expliciete pauzestappen. Stappen kunnen worden toegevoegd, bewerkt en verplaatst. Herhalingen zijn eindelijk configureerbaar (1–100); Oneindige lussen maken geen deel uit van de eerste versie. Een actieve macro kan worden beëindigd via de interface of uw eigen annuleersneltoets. Herhaaldelijke activering van dezelfde huidige toewijzing start geen parallelle run. Verschillende macro's met invoersimulatie zijn geserialiseerd, zodat de toetsaanslagen niet vermengen. Als er een stapfout optreedt, stopt de macro en wordt de betrokken stap weergegeven.

## Apparaten en fabrikanten

Apparaatinventarisatie via Windows Raw Input en aanvullende apparaatinformatie: weergavenaam, apparaatpad, fabrikant/product-ID indien beschikbaar, verbindingsstatus en gedetecteerde mogelijkheden. Door het aansluiten/loskoppelen wordt de lijst bijgewerkt zonder opnieuw op te starten. Een fysiek apparaat kan meerdere HID-vermeldingen hebben; Deze zijn gegroepeerd op basis van betrouwbare identificatiegegevens, anders worden ze zichtbaar als afzonderlijke vermeldingen bewaard.

Normale sneltoetsen zijn van toepassing op alle toetsenborden. De apparaatweergave biedt informatie en mogelijkheden; een apart toetsenbord met uitsluitend onderschepte toetsen maakt geen deel uit van deze overeengekomen modus.

Corsair-detectie werkt ook zonder iCUE. Een optionele iCUE SDK-adapter biedt de verbindingsstatus en ondersteunde verlichtingsacties wanneer SDK, iCUE en een compatibel apparaat aanwezig zijn. De gebruikersinterface maakt onderscheid tussen herkend, SDK beschikbaar, verbonden en daadwerkelijk bestuurbaar. De SDK documenteert voornamelijk LED-besturing; Eigen G-toetsen worden alleen ondersteund als normale sneltoetsen als Windows de juiste invoer ontvangt. Er is geen algemene belofte voor alle speciale sleutels.

Andere fabrikanten krijgen in eerste instantie generieke detectie en standaardacties. De adapterinterface maakt latere integraties mogelijk; niet-ondersteunde vaardigheden worden eerlijk weergegeven. Hardwaretests voor niet-bestaande modellen worden niet als geslaagd gerapporteerd.

## Opslag en het dagelijks leven

JSON-configuratie met versieversie onder de lokale app-gegevensmap. Atomaire opslag met laatste geldige back-up; onjuiste bestanden worden niet stilletjes overschreven. Import controleert het formaat, de versie, ID's en parameters en toont een voorbeeld voordat het wordt toegepast. Geïmporteerde acties kunnen alleen worden uitgevoerd na expliciete activering. Export bevat profielen en opdrachten.

Lademenu: Openen, profielselectie, sneltoetsen pauzeren, macro's annuleren, afsluiten. Venster sluiten minimaliseert standaard naar lade en legt dit de eerste keer uit. Autostart is optioneel en standaard uitgeschakeld. Slechts één exemplaar beheert sneltoetsen. Exit registreert sneltoetsen, breekt uitvoeringen af ​​en geeft bronnen vrij.

Lokale activiteitenweergave met beperkte geschiedenis van succes, mislukking en annulering. Geen log van alle toetsaanslagen. De inhoud van tekstacties wordt niet gespecificeerd in foutmeldingen. Geen cloudservice en geen telemetrie.

## Foutafhandeling en limieten

Ontbrekende programma's, ongeldige URL's, bezette sneltoetsen, beschadigde profielen en onbereikbare fabrikant SDK ontvangen specifieke Duitse berichten. Een adapterstoring heeft geen invloed op de standaardacties. Inzendingen voor toepassingen met hogere bevoegdheden kunnen mislukken vanwege Windows-machtigingslimieten; Fouten worden uitgelegd, de app heeft standaard geen beheerdersrechten nodig.

## Testen en accepteren

Geautomatiseerde controles op profielprioriteit, conflictdetectie, configuratie-import, atomaire opslag, macro-bestelling, nieuwe poging, annulering en foutpropagatie. Windows rooktests voor echte sneltoetsen, tekst- en toetsenbordsneltoetsen, media-acties, apparaatinventaris, lade en schone uitgang. Geen tests die per ongeluk willekeurige programma's of websites starten.

GUI-controle: toewijzing maken en bewerken, opnemen zonder uitvoering, klikken/sneltoets met hetzelfde effect, macro-annulering, conflictmelding, profielwijziging, apparaatweergave, import/export en schaling. Fabrikantspecifieke hardwaretests worden afzonderlijk bij het daadwerkelijke apparaat gedocumenteerd.

Levering: Broncode, reproduceerbare bouw-/opstartinstructies, Duitse gebruikershandleiding en lokale Windows-x64-Publish-Artefakt. Succes betekent een uitvoerbare app met alle generieke functies; Corsair-mogelijkheden zijn alleen gemarkeerd als beschikbaar met bewezen SDK-/apparaatondersteuning.

## Technische bronnen

- Microsoft Toetsenbordinvoer/RegistrerenHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Invoer: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Officiële Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Volgende stap

Na beoordeling van dit concept: maak een concreet implementatieplan en dien dit in ter selectie voor implementatie. Er is nog geen Git-repository in de lege werkmap; Dit document is derhalve nog niet vastgelegd.

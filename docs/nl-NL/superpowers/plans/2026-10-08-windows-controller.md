<!-- generated-doc locale=nl-NL source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Implementatieplan controller

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Voor agenten: gebruik parallel geïsoleerd eigendom; de gebruiker vroeg expliciet om volledige implementatie met maximaal bruikbare subagenten.

**Doel:** Lever het native Windows sneltoetspaneel in het goedgekeurde ontwerp.
**Architectuur:** Kernconfiguratie en opzegbare uitvoering; Windows-adapters; WPF bureaubladcompositie.
**Tech-stapel: ** .NET 10, WPF, Win32, JSON. Geen betaalde afhankelijkheden.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Mondiale beperkingen
Duitse gebruikersinterface; lokale opslag; Windows x64; waarheidsgetrouwe hardwarecapaciteitsstatus; geen toetsenbordregistratie; geen standaard autostart.

## Bekijk Focus
Misvormde importen mogen de opgeslagen instellingen niet vervangen; vastgehouden modifiers mogen de output niet vervuilen; losgekoppelde hardware mag niet crashen; bij sluiting moet de beschikbaarheid van de trays behouden blijven; geannuleerde macro's moeten bronnen vrijgeven.

## Taken en eigendom
- [x] Kernagent: src/Aivana.Core en tests/Aivana.Tests. Modellen, JSON-persistentie, validatie, profielselectie en MacroRunner. Voer falende gedragstests uit en implementeer en voer vervolgens alle kerntests uit.
- [x] Windows-agent: src/Aivana.Windows. RegisterHotKey, apparaatinventaris, SendInput, voorgrondproces en optionele Corsair verlichtingsadapter. Controleer de oorspronkelijke structuurgroottes en het opschonen van de registratie met veilige rooktests.
- [x] GUI-agent: alleen src/Aivana.Desktop/MainWindow.xaml en .cs plus UI-helpers. Donker dashboard, volledige editor, profielen, apparaten, instellingen, toetsenbordoverzicht en macro-editor. Bouw zodra er contracten bestaan.
- [x] Bovenliggend: gedeelde contracten, projectbestanden, samenstelling van DesktopController, levenscyclus van applicaties/tray, dialoogvensters voor importeren/exporteren, documentatie, integratiecontroles, publiceren en uitvoeren van app-rooktest.

## Gedeelde interfaces
Contracten staan in Core/Models.cs. De controllerimplementatie bevindt zich in Desktop/DesktopController.cs. UI roept controllermethoden aan via die concrete klasse. Agenten ontvangen exacte handtekeningen in hun taken. Elk bezit afzonderlijke bestanden. Parent lost mislukkingen tussen projecten op nadat alle agenten klaar zijn.

## Verificatie en levering
- [x] dotnet build Aivana.slnx -c Release: nul fouten.
- [x] dotnet run --project tests/Aivana.Tests -c Release: gedragscontroles zijn geslaagd.
- [x] dotnet publishing src/Aivana.Desktop -c Release -r win-x64 --op zichzelf staand true -o artefacten/win-x64.
- [x] Gepubliceerd uitvoerbaar bestand starten, Windows UI en processtabiliteit inspecteren, veilige profiel-/bewerkings-/persistentiestroom uitoefenen.
- [x] Schrijf README met bouw-/startinstructies en hardwarebeperkingen; recordcontroles en resterende hardware-only tests.

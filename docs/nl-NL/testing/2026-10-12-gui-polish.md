<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-12-gui-polish.md)

# GUI-revisie van 12.10.2026

- Directe toegang tot “Tools” in de navigatie; drie groepen met gedeclareerde acties voor instellen, maken en testen.
- Veertien bestaande dialooggebieden gebruiken een gemeenschappelijke dialoogklasse WPF met de bronnen van het hoofdvenster, een consistent lettertype en positionering bij de eigenaar.
- De grootte van het dialoogvenster is beperkt tot de werkruimte Windows; lange inhoud krijgt schuifbalken. Instellingen en profielpagina's blijven zelfs op lage hoogte scrollbaar.
- Actiefilter verschijnt alleen op het dashboard. Profielselectie en zoeken hebben expliciete namen voor ondersteunende technologieën. Veldlabels zijn contrastrijker.
- Dialoogvenster voor rooktest bevat gereedschapsoverzicht en uitbreidingsdialoogvensters in 560 × 440 en normale grootte; Bestaande recorder- en taaltesten zijn aangepast aan de scrollstructuur.

Beperkingen: Compacte vensters zijn geen vervanging voor echte Windows DPI-tests met 150/200-percentage. De nieuwe Duitse teksten zijn nog niet volledig vertaald voor alle 27-talen. Geen nieuwe hardwarevalidatie en geen uitspraak over gemeten bruikbaarheid.

Testbank: build vrijgeven zonder waarschuwingen/fouten; Geslaagd voor de volledige WPF-rooktest, inclusief compacte tool- en extensiedialogen en taalwisselcontroles. git diff --check geslaagd.

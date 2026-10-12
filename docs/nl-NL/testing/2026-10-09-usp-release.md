<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-09-usp-release.md)

# Toetsenbordondersteuning, overlay en uitvoeringsfeedback

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

De drie geprioriteerde verbeteringen geïmplementeerd in de bestaande Windows app:

- Wizard voor speciale sleutels: daadwerkelijke iCUE-ondersteunde inventaris van apparaten/sleutels, tijdelijke vastlegging van G-sleutels, verificatie van configuratiesucces, conflictvalidatie en een niet-opgeslagen actieconcept. Leg het terugdraaien vast, maak ongeldige callbacks in de wachtrij en herstel van normale bindingen, dek fouten af ​​en sluit dialoogvensters.
- Toetsenbordoverlay: fysieke referentiegeometrie, actieve profielbindingen, live profiel-/bewerkingsvernieuwing en gewone Windows-toetsmarkering. Houd Ctrl+Alt+F1 ingedrukt of blader door de palet-/toetsenbordpagina. Niet-activerend, doorklikbaar en afwezig op de taakbalk.
- Feedback over de uitvoering: getypte fasen Wachten/Rennen/Stap/Voltooid/Geannuleerd/Mislukt/Afgewezen, voortgang stap/herhaling en een niet-activerende toast. Feedback sluit actie-inhoud en uitzonderingsberichten uit. Bij annulering bij de laatste stap kan voltooiing niet worden gerapporteerd. Voltooiing beschrijft de uitvoering, niet een onafhankelijke OBS-statusquery.

Verificatie:

- 24 Kerncontroles zijn geslaagd, inclusief fasevolgorde, fout-/annuleringsresultaten, annulering van de laatste stap, isolatie van luisteraaruitzonderingen en uitsluiting van privéactiewaarden.
- De build van de release-oplossing is zonder waarschuwingen/fouten geslaagd.
- Windows controles geslaagd; de verbonden K100 rapporteerde G1–G6 en de native configuratie/release is geslaagd. Audio-eindpunten zijn geïnspecteerd zonder hun status te wijzigen.
- WPF-rook ging voorbij met alle referentie-overlay-indelingen, wizard/catalogus/conflicten/conceptpersistentie, live native capture-configuratie, terugbellen doorsturen, verouderde terugbelafwijzing en herstel van gepauzeerde bindingen. Native callback-invoer werd gesimuleerd; een fysieke menselijke G-toetsaanslag blijft een handmatige controle.
- De werkelijke invariantie van het voorgrondvenster werd gecontroleerd voor de uitvoeringstoost. Overlay-interactie-eigenschappen en tonen/verbergen zijn gecontroleerd; Er zijn schermafbeeldingen gemaakt voor de wizard, overlay en toast.
- 27-talen bevatten alle 819 canonieke tekenreeksen; offline tijdelijke aanduiding/sleutelvalidatie geslaagd.

Snapshots en het volledige GUI-resultaat staan in artefacten/usp-win-x64/smoke. Tests gebruiken tijdelijke profielen; Er wordt een back-up gemaakt van de productie-instellingen en tijdens de implementatie wordt een hash-check uitgevoerd.

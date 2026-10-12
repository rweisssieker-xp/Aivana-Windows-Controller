<!-- generated-doc locale=nl-NL source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Implementatieplan controlleruitbreiding

**Doel:** Implementeer de gevraagde monitor, gebaren, workflow, opname, apparaatdiagnose, draagbare profieluitwisseling en ladeverbeteringen.

**Architectuur:** Breid de bestaande Core/Windows/Desktop-lagen uit. Pure state-machines en draagbare configuratielogica leven in Core, native integraties in Windows en modale concepteditors in Desktop. Behoud bestaande vuile werkruimtebewerkingen en geïmplementeerde gebruikersconfiguratie.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitorservice, doelpersistentie en asynchrone livestatus; test niet-beschikbare ID's en annulering.
- [x] Gebaarstatusmachine en vastgehouden lagen; randen testen, herhalen, resetten en bestaande directe bindingen.
- [x] Workflowvoorwaarden, forward branches en foutenbeleid; testmomentopnamen en annulering.
- [x] Expliciete snelkoppelingsopname met conceptbeoordeling; testfiltering en haakverwijdering.
- [x] Profieluitwisseling van gedeelde mappen met revisiecontroles en previews; testconflicten en draagbare mappings.
- [x] Generieke standaard speciale sleutels en diagnose per apparaat; testparsering, dubbele toewijzingen en pauzegedrag.
- [x] Lade-/venster-/uitvoerbare pictogrambronnen en gepauzeerde weergave; verifieer bronnen in gepubliceerd uitvoerbaar bestand.
- [x] Integreer alle invoerpunten van het palet/editor en gelokaliseerde tekst; voer Core-, Windows- en WPF-controles uit.
- [x] Publiceer naar een aparte stagingmap, behoud de gebruikersinstellingen en start de bijgewerkte app opnieuw.

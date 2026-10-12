<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-12-github-publication.md)

# GitHub release van de MAKRIVO-ontwikkelingsstatus

Status: 12. Oktober 2026. Broncoderelease, geen ondertekende retailrelease.

- Oplossingsrelease-build: succesvol, geen waarschuwingen en fouten.
- Kernexamens: succesvol.
- WPF rooktest: succesvol; nieuw resultatenrapport in de lokale buildmap.
- Eerste volledige Windows-suite: controles voor venster op de voorgrond, audio, scherm en iCUE zijn geslaagd; Vervolgens treedt er een time-out op in de reactie van de UI Automation-werknemer.
- Gerichte nieuwe poging `--extensions-only`: succesvol, inclusief UI-automatisering, dubbelzinnigheid, uitgeschakelde doelen, wachtwoorduitsluiting, plug-inproces en echte HTTPS mobiele host. De oorspronkelijke time-out werd niet verdoezeld door een time-outwijziging; de oorzaak is nog niet overtuigend bewezen.
- Daaropvolgende volledige herhaling van Windows: succesvol, inclusief voorgrondvenster en alle extensiecontroles. Een succesvolle herhaling sluit sporadische time-outs niet uit.
- Vóór publicatie werden er geen typische privésleutels of productieve Stripe/GitHub/AWS-sleutels gevonden in de gecontroleerde broninventaris. Deze beperkte steekproefcontrole is geen volledig geheime controle.
- Build-artefacten en lokale orkestratiegegevens worden niet gepubliceerd.

De werkingssfeer en de resterende aftrekken: [Featurelijst](../FEATURES.md)Het is Eerdere testrapporten behouden hun tijdig geldigheid; ze zijn geen garantie voor elke hardware of desktopcontext.

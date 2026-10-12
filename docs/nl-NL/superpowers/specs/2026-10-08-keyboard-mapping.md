<!-- generated-doc locale=nl-NL source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fysieke toetsenbordweergave voor mapping

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Doel

De toetsenbordweergave is bedoeld om de daadwerkelijke toetsposities weer te geven bij het toewijzen van acties. Lijnen van dezelfde breedte zijn niet voldoende: Enter, Shift, Space, Navigation, numeriek toetsenblok en aanvullende bedieningselementen van de fabrikant verschillen.

## Implementatie

- Catalogus met gescheiden fysieke ID's, geometrie in sleuteleenheden, gedrukte legenda's en Windows-invoercodes.
- ISO-DE en ANSI-US elk als volledig formaat en TKL; bovendien Corsair K100 RGB, dat via iCUE als aangesloten model werd herkend.
- ISO Enter met L-vorm, numeriek toetsenbord met hoge plus/enter-toetsen en brede nul; Navigatie en pijlen in hun eigen posities.
- Model/indeling expliciet selecteren. USB VID/PID en modelnaam bewijzen geen taalvariant. Generieke profielen en profielen gemodelleerd op basis van documenten van de fabrikant zijn duidelijk anders gelabeld; geen garantie op millimeterschaal of firmware-compatibiliteit.
- Wijs de scancode toe aan de actieve Windows-invoerindeling, zodat een fysieke toetsklik niet per ongeluk een andere toets in beslag neemt, zoals Y/Z bij het schakelen tussen US en DE.
- Opdracht opent een concept in de bestaande actie-editor. Opslaan, conflicten controleren en weggooien blijven de bestaande mechanismen. Klikken initiëren geen acties.
- Eigen G-toetsen, profiel-/vergrendeltoetsen en iCUE-wiel worden weergegeven, maar worden niet uitgevoerd als normale Windows-sneltoetsen zonder een geïmplementeerde invoeradapter. Configureerbare fabrikantsnelkoppelingen blijven een mogelijke route.
- Toewijzingen blijven globale Windows-sneltoetsen; een apparaatweergave impliceert geen apparaatspecifieke onderdrukking of exclusieve toewijzing van een enkel fysiek toetsenbord.

## Bewijs

[K100-Handbuch met behulp van besturingselementen](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI/ISO verklaring](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Examen

testen van geometrie, varianten en invoercodes; Bouwt en bestaande kern/Windows-tests; WPF smoke voor weergave en mapping zonder de echte gebruikersconfiguratie uit te voeren of te wijzigen. Fabrikantonderzoek documenteert aanvullende model-/SKU-vereisten, onafhankelijk van de momenteel geïmplementeerde lay-outcatalogus.

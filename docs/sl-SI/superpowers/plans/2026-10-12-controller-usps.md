<!-- generated-doc locale=sl-SI source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Nadzorni načrt za razširitev

**Cilj:** Uvedba zahtevanega monitorja, gibanja, poteka dela, snemanja, diagnoze naprave, prenosne izmenjave profila in izboljšav traja.

**Arhitektura:** Razširite obstoječe Core/Windows/Desktop plasti. Čista strojna in prenosna konfiguracijska logika živi v jedru, domače integracije v Windows in modalne urednike zaslona v namizju. Ohranite obstoječe prljavega delovnega prostora in uporabniško konfiguracijo.

**Spec:** ../specs/2026-10-12-kontroler-usps-design.md

- [x] Storitev spremljanja, usmerjenost in asinkronen status; preizkusite razpoložljive ID-je in preklic.
- [x] Gesture stroj in držanje plasti; preizkusne robove, ponovitev, ponovno nastavitev in obstoječe takojšnje vezave.
- [x] Pogoji delovnega toka, napredne veje in pravilnik o napaki; preizkusni snopi in preklic.
- [x] Razloženo kratko snemanje s pregledom načrta; preskusno filtriranje in odlaganje.
- [x] Izmenjava profila v skupni mapi z revizijami in predogledom; preskusne konflikte in prenosne mapiranje.
- [x] Generic per-device standardne posebne ključe in diagnoze; testni parsing, duplicate maping in počitek vedenja.
- [x] Vzorec/vzorec/izvedljiva ikona virov in prekinjen videz; preverite virov v objavljenih izvedljivih.
- [x] Vključite vse točke vnosa palete/redaktorja in lokaliziranega besedila; izvedite Core, Windows in WPF preverjanja.
- [x] Objavite v ločen katalog, shranite nastavitve uporabnika in ponovno zagon posodobljene aplikacije.

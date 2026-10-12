<!-- generated-doc locale=hr-HR source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Plan provedbe proširenja kontrolora

**Cilj:** Uvođenje zatraženog monitora, gesta, radnog toka, snimanja, dijagnoze uređaja, prenosive razmjene profila i poboljšanja traja.

**Arhitektura: ** Proširenje postojećih slojeva Core/Windows/Desktop. Čisto stanje strojeva i prenosiva konfiguracija logika živi u Core, native integracije u Windows i modalne urednike na Desktop. Održavanje postojećih štetnih uređaja radnog prostora i raspoređene konfiguracije korisnika.

**Spec:** ../specs/2026-10-12-kontroler-usps-design.md

- [x] Služba za praćenje, usmjerena upornost i asinkronni status; testiranje nedostupnih ID-a i otkazivanje.
- [x] Gestiranje stanja stroja i držanje slojeva; testiranje krajeva, ponavljanje, ponovno postavljanje i postojeće neposredne vezanosti.
- [x] Uslovi radnog toka, napredne grane i politika pogreške; testiranje snimaka i otkazivanje.
- [x] Izričito kratko snimanje s pregledom prijedloga; testiranje filtracije i raspodjele.
- [x] Razmjena profila s revizijskim pregledima i predviđanjima; testiranje sukoba i prenosivih mapiranja.
- [x] Generic per-device standardni posebni ključi i dijagnoza; test parsing, duplicate maping i pauze ponašanje.
- [x] Preuzimanje / prozor / izvršljiva ikona resursa i prekida izgled; provjeriti resurse u objavljenom izvršljiv.
- [x] Uključite sve palete/editor ulazne točke i lokalizirani tekst; pokrenuti Core, Windows i WPF provjere.
- [x] Objavljujte u odvojenom redoslijedu, sačuvajte postavke korisnika i ponovno pokrenite ažuriranu aplikaciju.

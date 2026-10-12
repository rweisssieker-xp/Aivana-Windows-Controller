<!-- generated-doc locale=sr-Latn source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Proširenje kontrolera plana

**Cel:** Uvođenje zahtevanog monitora, gestova, radnog toka, snimanja, dijagnoze uređaja, prenosive promene profila i poboljšanja trajanja.

**Arhitektura:** Proširite postojeće slojeve Core/Windows/Desktop. Čiste državne mašine i prenosiva konfiguracija logike žive u Core, indijanske integracije u Windows i modalni dizajn uređaji u Desktop. Sačuvajte postojeće prljave radne prostore i raspoređene korisničke konfiguracije.

**Spec:** ../specs/2026-10-12-kontroler-ups-dizajn.md

- [x] Monitoring usluga, ciljna upornost i asinhronni status; testiranje nedostupnih ID-a i ukidanje.
- [x] Mašina za stanje i drže slojeve; testiraju ivice, ponavljaju, resetuju i postojeće neposredne veze.
- [x] Uslovi radnog toka, napredne grane i politika grešaka; test snimak i ukidanje.
- [x] Ekskluzivno skraćeno snimanje sa pregledom projekta; testiranje filtriranja i raspoređivanje.
- [x] Profil razmena zajedničke fascikla sa revizijskim proverima i pregledima; testiranje konflikata i prenosive mapiranje.
- [x] Generični po uređaju standardni specijalni ključevi i dijagnoza; test parsing, dupliranje mapiranje i pauza ponašanje.
- [x] Istraživanje / prozor / izvršivo ikone resurse i prekinuto pojavljivanje; proverite resurse u objavljenom izvršivo.
- [x] Integrirajte sve tačke ulaza palete/redaktora i lokalizovani tekst; pokrenite Core, Windows i WPF provere.
- [x] Objavljuje se u odvojenom katalogu, čuva se podešavanja korisnika i ponovo pokreće ažuriranu aplikaciju.

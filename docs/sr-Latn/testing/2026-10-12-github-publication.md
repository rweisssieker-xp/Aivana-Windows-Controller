<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-12-github-publication.md)

# GitHub - Objavljivanje stanja razvoja MAKRIVO

Stani: 12. oktobar 2026. Izvorni kod objavljivanja, bez potpisanog izdanja prodaje.

- Izgradnja rešenja: uspešan, nule upozorenja i greške.
- Testiranje je uspešno.
- WPF-Smoke test: uspešan; novi izveštaj o rezultatima u lokalnom rasporedu.
- Prvi kompletni Windows suit: uspješno ispitivanje prednjih prozora, audio, ekrana i iCUE; zatim vreme na UI automatizaciju radnika.
- Ciljan reprodukcija `--extensions-only`: Uspešno uključujući automatizaciju II-a, multimedijalnost, deaktivirane ciljeve, isključivanje lozinke, proces plugina i pravi HTTPS mobilni host. Prvobitni vremenski izlaz nije skriven promenom vremenskog ograničenja; uzrok još nije konačno utvrđen.
- Sledeći kompletni Windows recikliranje: uspešno uključujući prozor i sve testove za proširenje. Uspešna reprodukcija ne isključuje sporadične vreme.
- Pre objavljivanja nije pronađen tipičan privatni ključ ili produktivni Stripe/GitHub/AVS ključ u proverenom izvoru. Ovaj ograničeni pregled uzorka nije potpuna tajna.
- Izgradnja činjenica i lokalni orkestarski podaci nisu objavljeni.

Opseg i ostali troškovi: [Fejsbuk lista](../FEATURES.md). Prethodni izveštaji o ispitivanju zadržavaju svoju vremensku validnost; oni nisu garancija za bilo koji hardver ili desktop kontekst.

<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-12-github-publication.md)

# GitHub - Objavljivanje stanja razvoja MAKRIVO

Stanište: 12. listopad 2026. Izdanje izvornog koda, bez potpisivanja prodaje.

- Release izgradnja rješenja: uspješno, nula upozorenja i pogreške.
- Temeljni testovi: uspjeh
- WPF-Smoke test: uspješno; sveže izvješće o rezultatima u lokalnom redoslijedu.
- Prva potpuna Windows kutija: uspješno ispitivanje prednjeg prozora, audio, zaslona i iCUE; zatim Timeout na UI automatizacijskom radniku odgovori.
- Ciljna ponavljanja `--extensions-only`: uspješno uključujući automatizaciju UI-a, višestruku opremu, deaktivirane ciljeve, isključivanje lozinke, proces plugina i pravi HTTPS mobilni hosting. Originalni Timeout nije prikriven promjenom vremenskog ograničenja; uzrok još nije konačno dokazan.
- Sljedeći potpuni Windows recikliranje: uspješno uključujući prozore i sve proširenja. Uspješna ponavljanja ne isključuju sporadne Timeout.
- Prije objavljivanja nisu pronađeni tipični privatni ključi ili produktivni Stripe/GitHub/AWS ključi u provjereni izvorni sadržaj. Ova ograničena provjera uzoraka nije potpuna tajna ispitivanja.
- Izgradnja činjenica i lokalnih podataka o orkestru nisu objavljeni.

Opseg i preostali popusti: [Featuristička lista](../FEATURES.md)I to. Prethodni izvješća o ispitivanju zadržavaju svoju vremensku valjanost; oni nisu jamstvo za bilo koji hardver ili desktop kontekst.

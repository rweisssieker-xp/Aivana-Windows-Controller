<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-12-github-publication.md)

# GitHub-Publikiranje MAKRIVO Razvojnega stanja

Stanišče: 12. Oktobra 2026. Izdaja virov, brez podpisov prodaje.

- Release-Build rešitev: uspešno, nič opozorila in napake.
- Core preizkušnje: uspešno
- WPF-Smoke test: uspešno; sveže poročilo o rezultatih v lokalnem gradbenem uradu.
- Prva polna Windows soba: uspešna prednja okna, avdio, zaslon in iCUE preizkušnje; nato Timeout v UI avtomatizacijski delavec odgovor.
- Uspešno ponovitev `--extensions-only`: vključno z avtomatizacijo omrežja, večkratnostjo, deaktiviranimi cilji, izključitvijo gesla, procesom vtičnika in resničnim HTTPS mobilnim gostovanjem. Izvirni časovni izhod ni bil prikazan s spremembo časovnega roka; vzrok še ni končno dokazano.
- Naslednji polni Windows ponovni tečaji: uspešno vključno z okni v ozadju in vsemi testi razširitve. Uspešno ponovitev ne izključuje sporadnih časov.
- Pred objavo ni bilo običajnih zasebnih ključev ali produktivnih ključev Stripe/GitHub/AWS v preverjenem viru. Ta omejen vzorčni pregled ni popolna skrivnostna preiskava.
- Gradbeni podatki in lokalne podatke o orkestru niso objavljeni.

Obseg delovanja in preostali popusti: [Featuristični seznam](../FEATURES.md). je Prejšnje poročila o testiranju ohranjajo svojo časovno veljavnost; niso jamstvo za vsako strojno opremo ali namizni kontekst.

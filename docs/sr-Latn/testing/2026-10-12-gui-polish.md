<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-12-gui-polish.md)

# Prerada GUI-a od 12.10.2026

- Direktan ulaz "instrumenti" u navigaciji; tri grupe sa objašnjenim akcijama za uspostavljanje, stvaranje i ispitivanje.
- Četirinaest postojećih dijalogova koristi zajednički WPF dijalog sa resursima glavnog prozora, jedinstvenog pisanja i pozicioniranja kod vlasnika.
- Dijalog je ograničen na radno područje Windows; dugi sadržaj dobija scroll linije. Podešavanja i stranice profila ostaju skretajuće čak i na niskoj visini.
- Filter se pojavljuje samo na tastaturi. Izbor profila i pretraga imaju jasne imena za pomoćne tehnologije. Poljski opisi su kontrastniji.
- Test dijaloga uključuje pregled alata i dijaloge proširenja u 560 × 440, kao i u normalnoj veličini; postojeći testovi za snimanje i ispitivanje jezika su prilagođeni strukturi pregledanja.

Ograničenja: kompaktne prozore nisu zamena za stvarne Windows DPI testove sa 150/200 procenat. Novi nemački tekstovi još nisu potpuno prevedeni na sve 27 jezike. Nema novih hardverskih validacija i nema izjave o merenoj korisnosti.

Test stanje: Izgradnja izlaza bez upozorenja / grešaka; potpuni WPF test za mršavljenje, uključujući kompaktne dijaloge sa alatima i proširenjem, kao i testove za menjanje jezika. Git dif - proverite.

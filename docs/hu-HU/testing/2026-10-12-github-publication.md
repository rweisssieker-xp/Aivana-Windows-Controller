<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-12-github-publication.md)

# GitHub - A MAKRIVO fejlesztési állapot közzététele

Állás: 12. október 2026. forráskód kiadás, nincs aláírt értékesítési kiadás.

- Release-Build a megoldás: sikeres, null figyelmeztetések és hibák.
- Core tesztelés: sikeres
- WPF-Smoke teszt: sikeres; friss eredményjelentés a helyi építőrendben.
- Első teljes Windows Suite: előzetes ablakok, hangok, képernyők és iCUE tesztek sikeresen; majd a Timeout az UI automatizálási munkavállaló válasz.
- Célszerű ismétlés `--extensions-only`: sikeresen beleértve az UI automatizálását, a többszörösséget, a letiltott célokat, a jelszó kizárását, a plug-in folyamatot és a valódi HTTPS mobilhostot. Az eredeti időzítés nem rejtett meg az időzítés változásával; az ok még nem bizonyult.
- Következő teljes Windows ismétlődő futam: sikeresen beleértve az előzetes ablakokat és az összes kiterjesztési tesztet. sikeres ismétlődő futam nem zárja ki a spontán időzítéseket.
- A közzététel előtt nem találtak tipikus magán kulcsot vagy produktív Stripe/GitHub/AWS kulcsot a vizsgált forráskészletben.
- Az építési tények és a helyi szervezés adatait nem közzéteszik.

Funkciók és a fennmaradó eltérések: [Featurlista](../FEATURES.md)A . A korábbi vizsgálati jelentések időben érvényesek maradnak; nem garantálják a hardvereket vagy az asztali kontextust.

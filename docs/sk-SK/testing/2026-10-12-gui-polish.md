<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-12-gui-polish.md)

# Revízia GUI od 12.10.2026

- Priamy prístup k „Nástrojom“ v navigácii; tri skupiny s deklarovanými akciami na nastavenie, vytvorenie a testovanie.
- Štrnásť existujúcich dialógových oblastí používa spoločnú triedu dialógov WPF so zdrojmi hlavného okna, konzistentným písmom a umiestnením u vlastníka.
- Veľkosť dialógového okna je obmedzená na pracovný priestor Windows; dlhý obsah dostane posuvníky. Nastavenia a stránky profilu zostávajú rolovateľné aj pri nízkych výškach.
- Filter akcií sa zobrazuje iba na ovládacom paneli. Výber profilu a vyhľadávanie majú jasné názvy pre asistenčné technológie. Označenia polí sú kontrastnejšie.
- Dialógový test dymu zahŕňa prehľad nástrojov a dialógové okná rozšírenia v 560 × 440 a normálnej veľkosti; Existujúce rekordérske a jazykové testy boli prispôsobené štruktúre rolovania.

Obmedzenia: Kompaktné okná nie sú náhradou za skutočné testy Windows DPI s percentami 150/200. Nové nemecké texty ešte nie sú úplne preložené do všetkých jazykov 27. Žiadne nové hardvérové ​​overenie a žiadne vyhlásenie o meranej použiteľnosti.

Testovacia stolica: Uvoľnite zostavu bez varovaní/chýb; Prešiel úplným dymovým testom WPF, vrátane kompaktných nástrojov a dialógov rozšírenia a kontroly prepínania jazykov. git diff --kontrola prebehla.

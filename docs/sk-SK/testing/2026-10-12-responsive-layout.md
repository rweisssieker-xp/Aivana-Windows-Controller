<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-12-responsive-layout.md)

# Responzívne rozhranie WPF

Hlavné stránky sú založené na využiteľnej šírke obsahu, nie na pevnej šírke okna. Pod šírkou obsahu 900 DIPs sú palubná doska a editor pod sebou. Pod DIP 620 sú kľúčové čísla v dvoch stĺpcoch. Bočný panel je zbalený pod šírkou okna 820 DIPs. Minimálna veľkosť: 640 × 480 DIP.

Panely profilov a nastavení a ovládacie prvky klávesnice prepínajú svoje stĺpce na základe skutočnej šírky mriežky. Existujúce ovládacie prvky sú preusporiadané namiesto vytvárania nových položiek. Akčné karty rozdeľujú dostupnú šírku čiary. Panely s nástrojmi sa zlomia. Strana klávesnice je dostupná vertikálne, fyzická klávesnica si zachováva svoje proporcie.

Viac automaticky WPF-Test: všetkých päť hlavných stránok 640 × 480, 900 × 620 a 1440 × 900; Snímky obrazovky na stránku; Zmena stĺpca v oboch smeroch s kontrolou prijatého textu. DIPy sú logické WPF-Jednotky. Skutočný Windows-Scaling 150/200 Percento a viac monitorov ešte neboli overené.

Testovacia stolica: Uvoľnite zostavu bez chýb/upozornení. Kompletný dymový test WPF prešiel všetkými veľkosťami, zmenami stĺpcov, rozložením klávesnice a jazykovými kontrolami. git diff --kontrola prebehla.

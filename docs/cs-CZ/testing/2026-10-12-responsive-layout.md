<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-12-responsive-layout.md)

# Responzivní rozhraní WPF

Hlavní stránky vycházejí z použitelné šířky obsahu, nikoli z pevné šířky okna. Pod šířkou obsahu 900 DIPs jsou řídicí panel a editor pod sebou. Pod DIPy 620 jsou klíčové údaje ve dvou sloupcích. Postranní panel je sbalený pod šířkou okna 820 DIPs. Minimální velikost: 640 × 480 DIPy.

Panely profilů a nastavení a ovládací prvky klávesnice přepínají své sloupce podle skutečné šířky mřížky. Stávající ovládací prvky jsou přeuspořádány namísto vytváření nových položek. Akční karty rozdělují dostupnou šířku čáry. Panely nástrojů se zlomí. Strana klávesnice je dosažitelná vertikálně, fyzická klávesnice si zachovává své proporce.

Více automatické WPF-Test: všech pět hlavních stránek 640 × 480, 900 × 620 a 1440 × 900; Snímky obrazovky na stránku; Změna sloupce v obou směrech s kontrolou přijatého textu. DIPy jsou logické WPF-Jednotky. Nemovitý Windows- Měřítko 150/200 Procento a více monitorů ještě nebylo ověřeno.

Testovací stolice: Uvolněte sestavení bez chyb/varování. Kompletní kouřový test WPF prošel všemi velikostmi, změnami sloupců, rozložením klávesnice a jazykovými kontrolami. git diff --kontrola prošla.

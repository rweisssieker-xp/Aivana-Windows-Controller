<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-12-responsive-layout.md)

# Responsiva WPF - Yttring

Huvudsidorna riktar sig till användbar innehållsbredd, inte en fastställd fönsterbredd. Under 900 DIPs innehållsbredd finns Dashboard och Editor. Under 620 DIPs blir indikatorer tvåsidiga. Under 820 DIPs fönstersbredd är sidlistan inkopplad. Minsta storlek: 640 × 480 DIPs.

Profil- och inställningsområden samt tangentbordskontroller byter kolumner baserat på den faktiska gridbredden. De befintliga kontrollerna omrörs i stället för att lägga in dem på nytt. Aktionskort delar tillgängliga rader. Verktyget bryts. tangentbordets sida är vertikalt tillgänglig, den fysiska tangentbordet behåller sina proportioner.

Automatiska WPF-Test: Alla fem huvudsakliga sidor 640 × 480, 900 × 620 och 1440 × 900; Screenshots per sida; kolumnväxling i båda riktningarna med kontroll av den erhållna texten. WPF-Föreningar - Verkliga Windows-Skala med 150/200 Fler och fler skärmar är inte validerade..

Testläge: Release-Build utan fel/varningar. Fullständig WPF smoke-test med alla storlekar, kolumner, tangentbordslayout och språkprov. Git diff -check överlevde.

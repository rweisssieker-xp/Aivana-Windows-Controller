<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-12-gui-polish.md)

# GUI omarbetning från 12.10.2026

- Direkt introduktion av ”verktyg” i navigering; tre grupper med deklarerade åtgärder för inrättande, skapande och testning.
- Fyra dialogområden använder en gemensam WPF dialogklass med resurserna i huvudfönstret, enhetlig skrivning och positionering hos ägaren.
- Dialogstorleken är begränsad till Windows-arbetsområdet; Långt innehåll får spår. Inställningar och profiler förblir skräddarsydda även vid låg höjd.
- Filter visas endast på Dashboard. Profilval och sökning har uttryckliga namn för hjälpteknik. Fältbeskrivningarna är mer kontrasterande.
- Dialog-Smoke-testet omfattar verktygsöversikt och utvidgningsdialoger i 560 × 440 samt normala storlekar; befintliga inspelningar och språkprov har anpassats till spårstrukturen.

Gränser: Kompakta fönster är ingen ersättning för verkliga Windows-DPI-test med 150/200 procent. De nya tyska texterna är inte helt översatta till alla 27 språk. Inga nya hårdvaruvalideringar och inga uttalanden om mätbarhet.

Teststatus: Release-Build utan varningar/fel; fullständig WPF smoke-test, inklusive kompakta verktygs- och utvidgningsdialoger och språkutvärderingar. Git diff -check överlevde.

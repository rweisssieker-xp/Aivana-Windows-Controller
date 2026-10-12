<!-- generated-doc locale=sv-SE source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fysisk tangentbild för kartläggning

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Målet

Tastaturvyn ska visa de faktiska tangentpositionerna när du tilldelar åtgärder. Samma breda linjer räcker inte: Enter, Shift, Space, Navigation, Number Block och tillverkare-tillbehör är olika.

## genomförandet

- Katalog med separata fysiska ID:er, geometri i tangentbord, tryckta legender och Windows inkoder.
- ISO-DE och ANSI-US vardera som fullformat och TKL; dessutom Corsair K100 RGB, som erkänns som en ansluten modell över iCUE.
- ISO Enter med L-form, nummerblock med höga plus-/inträngknappar och bredare noll; navigering och pilar på sina egna positioner.
- Välj modell / layout. USB VID/PID och modellnamn visar inte någon språkvariant. Generiska profiler och profiler som är modellerade enligt tillverkardokumenten kan identifieras på olika sätt; ingen garanti för millimeter eller firmware-kompatibilitet.
- Skanna koden på den aktiva Windows-inställningen så att en fysisk knappklick inte slumpmässigt lägger på en annan knapp, till exempel Y/Z när du byter mellan US och DE.
- Tilldelning öppnar ett utkast i den befintliga åtgärdsredaktören. Lagring, kollision och avvisning förblir befintliga mekanismer. Klickar inte på åtgärder.
- Ägaren G-knappar, profil-/lock-knappor och iCUE rader visas, men utan implementerad inputadapter utfärdas inte som vanliga Windows hotkeys. Konfigurerbar tillverkare korthet förblir en möjlig väg.
- Beläggningar förblir globala Windows hotkeys; en enhetssyn innebär inte en enhetspecifik undertryckning eller exklusiv beläggning av en enda fysisk tangentbord.

## Bevisa

[K100-Handbuch med kontrollelement](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI/ISO-förklaring](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Prövning

Geometriska, varianter och inloggnings-test; bygg och befintliga kärn-/Windows test; WPF smuts för att visa och tilldela utan att utföra eller ändra den verkliga användarkonfigurationen. Tillverkarens forskning dokumenterar ytterligare modell-/SKU-krav oberoende av den aktuella layoutkatalogen.

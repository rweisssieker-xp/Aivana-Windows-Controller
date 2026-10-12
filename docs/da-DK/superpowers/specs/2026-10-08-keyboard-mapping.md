<!-- generated-doc locale=da-DK source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fysisk tastaturvisning til mapping

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Målet

Tastaturindsigtet skal vise de faktiske tastpositioner, når du tildeler handlinger. Ligeledes brede linjer er ikke tilstrækkelige: Enter, Shift, Space, Navigation, Number Block og Producer-tilskud kontroller er forskellige.

## gennemførelse

- Katalog med adskilte fysiske ID'er, geometri i tastaturer, trykte legender og Windows indtagskoder.
- ISO-DE og ANSI-US hver som fuldformat og TKL; derudover Corsair K100 RGB, der blev anerkendt som en tilknyttet model over iCUE.
- ISO Enter med L-form, nummerblok med høje plus/in-tasten og bredere null; navigation og spor på deres egne positioner.
- Vælg udtrykkeligt model/layout. USB VID/PID og modelnavnet viser ingen sprogvariant. Generiske profiler og profiler, der er modelleret efter producentdokumenter, er mærkbart forskellige; ingen garanti for millimeter eller firmware kompatibilitet.
- Scan-kode på den aktive Windows indstillinger, så et fysisk knap ikke tilfældigt lægger en anden knap, f.eks. Y/Z, når du skifter mellem US og DE.
- Tildeling åbner et projekt i den eksisterende aktionsrediger. Opbevaring, konfliktundersøgelse og afvisning forbliver de eksisterende mekanismer. Klik starter ikke aktioner.
- Egen G-knapper, profil-/lock-knappe og iCUE-rader vises, men uden implementeret indtastning adapter udstedes ikke som normale Windows hotkeys. Konfigurerbare producenter kortsætninger forbliver en mulig måde.
- Belægninger forbliver globale Windows hotkeys; en apparatvisning betyder ikke en enhedspecifik undertrykkelse eller eksklusiv belægning af en enkelt fysisk tastatur.

## Bevis

[K100-Handbuch med styrelementer](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO-erklæring](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Undersøgelse

Geometriske, variante- og indtastningstest; byggede og eksisterende Core-/Windows-tester; WPF-smoke til præsentation og tildeling uden at udføre eller ændre den virkelige brugerkonfiguration. Fabrikantforskning dokumenterer yderligere model-/SKU-krav uafhængigt af den nu implementerede layoutkatalog.

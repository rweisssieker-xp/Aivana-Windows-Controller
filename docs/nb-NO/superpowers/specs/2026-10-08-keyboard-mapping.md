<!-- generated-doc locale=nb-NO source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fysisk tastaturvisning for kartlegging

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Målet

Tastaturvisningen skal vise de faktiske tastposisjonene når du tildeler handlinger. Det samme brede linjene er ikke nok: Enter, Shift, Space, Navigation, Number Block og produsent-tilskudd kontroller er forskjellige.

## Implementering

- Katalog med separate fysiske ID, geometri i tastaturer, uttrykt legender og Windows innsendingskoder.
- ISO-DE og ANSI-US hver som fullformat og TKL; i tillegg Corsair K100 RGB, som ble anerkjent som en tilknyttet modell over iCUE.
- ISO Enter med L-form, tallblokk med høye plus/enter-knapper og bredere null; navigasjon og tråder i sine egne posisjoner.
- Velg uttrykkelig modell / layout. USB VID/PID og modellnavnet viser ingen språkvariant. Generiske profiler og modellerte profiler basert på produsentdokumenter er synlig forskjellig; ingen millimeter eller firmware kompatibilitet garanti.
- Skannekode på den aktive Windows innstillingen slik at et fysisk knapp ikke tilfeldig legger en annen knapp, for eksempel Y/Z når du bytter mellom US og DE.
- Tildeling åpner et utkast i den eksisterende handlingsredaktoren. Lagre, kontrollere og avvise konflikter forblir eksisterende mekanismer. Klikker starter ingen handlinger.
- Egen G-knapper, profil-/lokk-knappe og iCUE-rader vises, men uten implementert inntak adapter blir ikke utgitt som normale Windows-hotkeys. Konfigurerbare produsentkort forblir en mulig måte.
- Belegninger forblir globale Windows hotkeys; en enhetssyn betyr ikke en enhetspesifikk undertrykkelse eller eksklusiv belegning av en enkelt fysisk tastatur.

## Bevisene

[K100-Handbuch med styrelementer](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO-erklæring](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Prøve

Geometri-, varianter- og innkodtester; bygg og eksisterende kjernekoder/Windows tester; WPF smaker for å vise og tilpasse uten å utføre eller endre den virkelige brukerkonfigurasjonen. Produsentforskning dokumenterer ytterligere modell-/SKU-krav uavhengig av den nå implementerte layoutkatalogen.

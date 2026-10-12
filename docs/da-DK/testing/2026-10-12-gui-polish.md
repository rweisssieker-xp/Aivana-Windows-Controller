<!-- generated-doc locale=da-DK source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-12-gui-polish.md)

# GUI omarbejdning af 12.10.2026

- Direkte indgang "værktøjer" i navigation; tre grupper med erklærede handlinger til oprettelse, oprettelse og testning.
- Fire ti eksisterende dialogområder bruger en fælles WPF dialogklasse med hovedvinduets ressourcer, enhed og placering hos ejeren.
- Dialogstørrelsen er begrænset til Windows arbejdsområdet; langt indhold modtager scrolllister. Indstillinger og profiler forbliver scrollable selv ved lav højde.
- Aktionsfilter vises kun på dashboardet. Profilindvælgelse og søgning har udtrykkelige navne for assistive teknologier. Fællesforskrifterne er mere kontradiktoriske.
- Dialog-Smoke-testen omfatter værktøjsoversigt og udvidelsesdialoger i 560 × 440 samt normal størrelse; eksisterende rekord- og sprogprøver er blevet tilpasset til scrollstrukturen.

Begrænsninger: Kompakt vinduer er ikke en erstatning for virkelige Windows-DPI-tester med 150/200 procent. De nye tyske tekster er endnu ikke fuldt oversat til alle 27 sprog. Ingen ny hardwarevalidation og ingen erklæring om målt anvendelighed.

Teststand: Release-build uden advarsler/fejl; fuldstændig WPF smoke test, herunder kompakte værktøj og udvidelsesdialoger samt sprogudveksling. Git diff -check er klar.

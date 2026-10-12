<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-12-gui-polish.md)

# GUI omarbeiding fra 12.10.2026

- Direkte inngang "verktøy" i navigasjon; tre grupper med erklærte handlinger for etablering, opprettelse og testing.
- Fire ti eksisterende dialogområder bruker en felles WPF dialogklasse med ressursene til hovedvinduet, enhetlig skriving og posisjonering hos eieren.
- Dialogstørrelsen er begrenset til Windows arbeidsområdet; lang innhold får skrålinjer. Institusjoner og profiler forblir skruelige selv ved lav høyde.
- Aktionsfilter vises bare på dashboardet. Profilvalg og søk har uttrykkelige navn for assisterende teknologier. Feltbeskrivelsene er mer kontrastrike.
- Dialog-Smoke-testen inkluderer verktøyoversikt og utvidelsesdialoger i 560 × 440 samt normal størrelse; eksisterende inspeksjoner og språk er tilpasset til Scroll-strukturen.

Grenser: Kompakt vinduer er ingen erstatning for ekte Windows-DPI-tester med 150/200 prosent. De nye tyske tekstene er ennå ikke oversatt til alle 27 språk. Ingen ny maskinvarevalidering og ingen uttalelse om målt brukbarhet.

Teststand: Release-build uten advarsler/feil; fullstendig WPF smoke-test, inkludert kompakte verktøy og utvidelsesdialoger og språkvekslingstester. Git diff - sjekk over.

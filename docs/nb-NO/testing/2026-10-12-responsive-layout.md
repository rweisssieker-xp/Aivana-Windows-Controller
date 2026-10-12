<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-12-responsive-layout.md)

# Responsive WPF overflaten

De viktigste sidene er rettet etter brukbar innholdsbredde, ikke etter en fast bestemt vindubredde. Under 900 DIPs innhold er Dashboard og Editor hverandre. Under 620 DIPs blir indikatorer topart. Under 820 DIPs vindubredde er sidelinjen slått inn. Minimum størrelse: 640 × 480 DIPs.

Profil- og innstillinger samt tastaturkontroller skifter kolonnene basert på den faktiske gridbredden. I tillegg vil de eksisterende kontrollene bli omordnet, i stedet for å legge inn innsendinger. Aktionskort deler tilgjengelig rekkevidde. Verktøyene bryter. Tastatursiden er vertikalt tilgjengelig, den fysiske tastaturen beholder sine proporsjoner.

Automatisk WPF-Test: alle fem hovedsider 640 × 480, 900 × 620 og 1440 × 900; Screenshots per side; bytte kolonner i begge retninger med kontroll av den mottatte teksten. WPF-Enheter – ekte Windows-Skalaer med 150/200 prosent og flere monitorer er ikke validert.

Teststand: Release-Build uten feil/varsel. Fullstendig WPF smoke-test med alle størrelser, kolonnutvikler, tastaturlayouts og språkprøver. Git diff - sjekk over.

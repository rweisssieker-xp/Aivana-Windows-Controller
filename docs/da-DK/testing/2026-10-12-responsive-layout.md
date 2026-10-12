<!-- generated-doc locale=da-DK source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-12-responsive-layout.md)

# Responsive WPF overflade

De vigtigste sider er rettet efter den anvendelige indholdsbredde, ikke efter en fast fast vinduesbredde. Under 900 DIPs indholdsbredde er Dashboard og Editor hinanden. Under 620 DIP'er bliver indikatorer toparts. Under 820 DIPs vinduebredde er sidelinjen indtastet. Minimum størrelse: 640 × 480 DIPs.

Profil- og indstillingerne samt tastaturkontrolelementerne skifter deres kolonne baseret på den faktiske gridbredde. Derudover omordnes de eksisterende kontrolelementer i stedet for at genoprette deres indtægter. Aktionskort deler de tilgængelige linjer. Værktøjer bryder sig. Tastatursiden er opnåelig vertikalt, den fysiske tastatur bevarer dens proportioner.

Automatisk WPF-Test: Alle fem hovedsider 640 × 480, 900 × 620 og 1440 × 900; Screenshots pr. side; skift kolonne i begge retninger med at tjekke den modtagne tekst. WPF-Enheder - Virkeligt Windows-Skalaer til 150/200 procent og flere monitorer er endnu ikke valideret.

Teststand: Release-Build uden fejl/varsel. Komplet WPF smoke test med alle størrelser, kolonneændringer, tastaturlayouts og sprogprøver. Git diff -check er klar.

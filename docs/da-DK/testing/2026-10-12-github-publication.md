<!-- generated-doc locale=da-DK source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-12-github-publication.md)

# GitHub - Udgivelse af MAKRIVO udviklingsstatus

Stand: 12. Oktober 2026. Kildekodeudgivelse, ingen underskrevet salg.

- Release-bygning af løsningen: vellykket, nul advarsler og fejl.
- Core test: Der er succes.
- WPF-Smoke test: vellykket; ny rapport om resultater i den lokale byggeri.
- Første komplet Windows-swit: fremragende vinduer, lyd, skærm og iCUE-tester er vellykket; derefter Timeout på UI-automation arbejdsgiver svar.
- Målrettet gentagelse `--extensions-only`: vellykket herunder UI-automation, flersigtighed, deaktiverede mål, adgangskodeudelukkelse, pluginsproces og ægte HTTPS-mobilhost. Den oprindelige timeout er ikke skjult af en ændring af tidsgrænsen; årsagen er endnu ikke afsluttet.
- Næste fuldstændig Windows genopretning: med succes inklusive forgrundsvinduer og alle udvidelsesprøver. En vellykket gentagelse udelukker ikke sporadiske timeouts.
- Forud for offentliggørelsen blev der ikke fundet nogen typiske private nøgler eller produktive Stripe-/GitHub-/AWS nøgler i den kontrollerede kilde. Denne begrænsede mønsterkontrol er ikke en fuldstændig hemmelighedsundersøgelse.
- Konstruktionsfakter og lokale orkesteringsdata offentliggøres ikke med.

Funktionsdækning og resterende aftagelser: [Featureliste](../FEATURES.md)Det er Tidligere testrapporter bevarer deres gyldighed; de er ikke en garanti for enhver hardware eller desktop kontekst.

<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-12-github-publication.md)

# GitHub - Utgivelse av MAKRIVO utviklingsstatus

Stand: 12. Oktober 2026. Kildekode publisering, ingen signert salgsløsning.

- Release-Build løsningen: vellykket, null advarsler og feil.
- Core tester: vellykket
- WPF Smoke-test: vellykket; fersk resultatrapport i lokal byggordner.
- Første fullstendige Windows-switten: fremgangsmåte, lyd, skjerm og iCUE-tester, deretter Timeout i UI-automatiseringsarbeiderens respons.
- Målrettet repetisjon `--extensions-only`: vellykket inkludert UI-automasjon, merverdighet, deaktivert mål, passordutvidelse, plugin-prosess og ekte HTTPS-mobilhost. Den opprinnelige timeouten ble ikke skjult av en endring i tidsgrensen; årsaken er ennå ikke konkludert.
- Følgende fullstendig Windows gjenoppretting: vellykket inkludert forhåndsvisning og alle utvidelsesundersøkelser. En vellykket repetisjon utelukker ikke sporadiske timeouter.
- Før publiseringen ble det ikke funnet noen typiske private nøkler eller produktive Stripe-/GitHub-/AWS nøkler i den kontrollerte kildeinnholdet. Denne begrensede mønster sjekken er ikke en fullstendig hemmelighet.
- Byggefakter og lokale orkesteringsdata blir ikke publisert.

Funksjonalitet og resterende avtak: [Featureliste](../FEATURES.md)Det er Tidligere testrapporter opprettholder sin gyldighet; de er ingen garanti for enhver maskinvare eller skrivebord.

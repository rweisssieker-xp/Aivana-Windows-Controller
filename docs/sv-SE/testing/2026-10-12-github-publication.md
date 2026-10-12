<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-12-github-publication.md)

# GitHub - Publicering av MAKRIVO utvecklingsstatus

Stannar: 12. oktober 2026. Källkod publicering, ingen undertecknad försäljning.

- Release-Build av lösningen: framgångsrik, noll varningar och fel.
- Kernprov: Det är framgångsrikt
- WPF Smoke-test: framgångsrik; färsk resultatrapport i den lokala byggordningen.
- Första fullständiga Windows-switten: framgångsrika frontfönster, ljud, skärm och iCUE-tester; sedan Timeout i UI-automationssvaret.
- Målad upprepning `--extensions-only`: framgångsrikt inklusive UI-automation, mångsidighet, inaktiverade mål, lösenordutsläpp, plugin-process och verklig HTTPS-mobilhost. Den ursprungliga tidsläget har inte dolts av en ändring av tidsgränsen; orsaken är ännu inte slutgiltig.
- Efterföljande fullständiga Windows återhämtning: framgångsrikt inklusive förgrundsfönster och alla utvidgningar. En framgångsrik upprepning utesluter inte sporadiska timing.
- Inga typiska privata nycklar eller produktiva Stripe-/GitHub-/AWS-nycklar hittades i den kontrollerade källbeställningen före publiceringen. Denna begränsade mönsterkontroll är inte en fullständig hemlighetskontroll.
- Byggnadsfakta och lokala orkesterdata publiceras inte med.

Funktionsområde och återstående avdrag: [Featurlista](../FEATURES.md)Det är Tidigare testrapporter behåller sin giltighet; de är inte en garanti för någon hårdvara eller skrivbordskontext.

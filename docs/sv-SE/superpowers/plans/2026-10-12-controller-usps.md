<!-- generated-doc locale=sv-SE source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Kontrollplan för utvidgning

** Mål: ** Genomföra den begärda monitoren, gesturen, arbetsflödet, inspelningen, enhetens diagnos, bärbar profilutbyte och spårförbättringar.

**Arkitektur:** Utöka befintliga kärn/Windows/Desktop lager. Ren stat maskiner och bärbar konfigurationslogik lever i Core, inhemska integrationer i Windows och modala utkast redaktörer i Desktop. Bevara befintliga smutsiga arbetsplatsredigeringar och användarkonfiguration.

**Spec:** ../specs/2026-10-12-kontroll-usps-design.md

- [x] Övervakningstjänst, målresistens och asynkron levande status; test icke-tillgängliga ID och avbokning.
- [x] Gestur ställning maskin och håll lager; test kantar, upprepning, omställning och befintliga omedelbara bindningar.
- [x] Arbetsflödesvillkor, framåtskedjor och felpolicy; test snapshots och avbokning.
- [x] Explicit kortskärm med utkast översyn; testfiltering och hook disposal.
- [x] Delad fil profilväxling med granskningar och förhandsvisningar; test konflikter och bärbara mappningar.
- [x] Generisk per-device standard speciella nycklar och diagnos; testparsning, duplicerade mappningar och paus beteende.
- [x] Tray/window/executable ikonresurser och paus; verifiera resurser i publicerad executable.
- [x] Integrera alla palett/redigeringsinsatser och lokaliserad text; kör Core, Windows och WPF kontroller.
- [x] Publicera till en separat spårad katalog, spara användarinställningar och starta om den uppdaterade appen.

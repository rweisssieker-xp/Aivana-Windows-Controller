<!-- generated-doc locale=nb-NO source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Kontroller utvidelsesplan

** Mål:** Implementere den anmodede monitoren, gesturen, arbeidsflyten, inspeksjonen, enhetsdiagnosen, bærbar profilutveksling og tray forbedringer.

**Arkitektur:** Utvid eksisterende Core/Windows/Desktop lag. Pure state maskiner og bærbar konfigurasjonslogikk lever i Core, innfødte integrasjoner i Windows og modale utformingsredaktører i Desktop. Oppbevar eksisterende dirty workspace-redigering og implementert brukerkonfigurasjon.

**Spec:** ../specs/2026-10-12-kontrollør-usps-design.md

- [x] Overvåkningstjeneste, målbestandighet og asynkron live status; testing av utilgjengelige IDs og kansellering.
- [x] Gesture stasjon maskin og hold lag; test edder, gjentakelse, resett og eksisterende umiddelbare bånd.
- [x] Arbeidsflyten, fremre grener og feilpolicy; test snapshots og kansellering.
- [x] Eksplicit kortskudd med utkast gjennomgang; testfiltering og hook disposal.
- [x] Delte mapper utveksling med revisjons sjekker og forhåndsvisninger; test konflikter og bærbare mapper.
- [x] Generisk per-device standard spesielle nøkler og diagnose; test parsing, duplikat mapping og pause oppførsel.
- [x] Tray/window/executable ikon ressurser og pause utseende; verifisere ressurser i publisert executable.
- [x] Integrere alle palett/redigeringspunkter og lokalisert tekst; kjøre Core, Windows og WPF sjekker.
- [x] publisere til en separat stasjonær katalog, beholde brukerinnstillingene og starte oppdaterte apper.

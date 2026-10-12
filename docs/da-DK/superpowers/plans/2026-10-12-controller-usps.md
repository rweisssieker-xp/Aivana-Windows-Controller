<!-- generated-doc locale=da-DK source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Udvidelsesplan for kontroller

** Mål: ** Implementering af den anmodede monitor, gestus, arbejdsproces, optagelse, enhedsdiagnose, bærbar profiludveksling og tray forbedringer.

**Arkitektur:** Udvid eksisterende Core/Windows/Desktop lag. Pure state maskiner og bærbar konfiguration logik lever i Core, indfødte integrationer i Windows og modale design editorer i Desktop. Beskytt eksisterende dirty workspace-rediger og implementeret brugerkonfiguration.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Overvågningstjeneste, målbestandighed og asynkron live status; test utilgængelige ID'er og annullering.
- [x] Gesture tilstand maskine og hold lag; test edder, gentagelse, genoprettelse og eksisterende øjeblikkelige bindinger.
- [x] Arbejdsfløjens betingelser, fremadskridt og fejlpolitik; test snapshots og annullering.
- [x] Eksplicit kortskæring med udkast anmeldelse; testfiltrering og hook disposal.
- [x] Udveksling af profiler med revisionskontrol og forudsigelser; testkonflikter og bærbare mapper.
- [x] Generisk per-device standard specielle nøgler og diagnose; test parning, duplikat mappinger og pause adfærd.
- [x] Tray/window/executable ikon ressourcer og pause udseende; verifikere ressourcer i offentliggjort executable.
- [x] Integrere alle palette/editor indtastningspunkter og lokaliseret tekst; køre Core, Windows og WPF kontroller.
- [x] Udgiv til en separat stage-diagram, bevar brugerindstillinger og genstart den opdaterede app.

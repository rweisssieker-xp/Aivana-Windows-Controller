<!-- generated-doc locale=nb-NO source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokale KI-tid og resterende tester

Målet er å faktisk koble flere lokale AI-tid, få utformingsundersøkelser og gjøre åpne utgivelser synlige. Brukeren tillater implementering uten forespørsler. eksisterende endringer forblir.

1. Tester for loopback-transport og ONNX pakkekontroll før implementering; sjekke nettverksadresser, utgiftsgrenser, avbrudd og ukjente ressurser.
2. Samme planleggingsmeddelelser; Ollama Oppbevares, LM Studio (127.0.0.1:1234) og llama.cpp (127.0.0.1:8080) Ingen cloud-URLer eller automatiske leverandørskifter. lokale servere kan videreføre seg selv; drift av dem forblir synlig ansvaret for operatøren.
3. ONNX Runtime GenAI CPU i egen prosess med tidsgrense, begrenset JSON-protokoll og prosessavbrudd. Velg modellordene uttrykkelig; ingen modell lastes ned automatisk. Bare GenAI kompatible tekstmodeller, ingen .onnx nettverk.
4. WPF valg for driftstid, modell, ONNX plater; alle leverer det samme strengt testet design.
5. Teststand med klare grenser for maskinvare, DPI, mobilpanel og eksklusive makropad. Fysiske tester og en undertegnet/licensert driver kan ikke erstattes med programvare her.
6. Release-Build, Core/Windows Test, WPF Smoke, Worker/Package Testing og uavhengig gjennomgang. Lag et nytt komplett pakke; meld kvaliteten på modellene bare for faktisk eksisterende modell som testet.

Resultatet: fire løptid implementert, ONNX nativ separat testet, leverandørvalg og teststand i pakket WPF smoke. Core/Windows kjøkken er grønn. Revisjonsbefund Unicodegrense rettet med felles konstant og regresjon. Virkelige hardware/mobile/DPI-utvekslinger, drivere, komplette oversettelser og levende instruksjon kvalitet forblir eksplisitt åpent arbeid; ikke som fullstendig uttalt.

Fortsettelse: Separert Jinja og utnevnt/default mønstre implementert; Typ-/grense/Fallback-regresjonsundersøkelser gjennomgått. Verktøy navigasjon i 27 kataloger supplert. Riktig instrukt modell lastet ned separat og sjekket SHA256: native CPU-inferanse fungerer, men 0/4 planlegging tilfeller oppfyller JSON-avtalen; ingen handlinger utføres. Uavhengige anmeldelse: null-Fallback korrigert og testet, deretter ingen konkrete ytterligere oppdagelser. Core og Windows KI-tester gjennomgått, Release-Build 0 advarsler/feil.

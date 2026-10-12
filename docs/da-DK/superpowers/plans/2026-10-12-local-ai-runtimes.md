<!-- generated-doc locale=da-DK source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokale KI-tidspunkter og resterende undersøgelser

Målet er at gøre mere lokale AI-timer virkeligt tilsluttet, få udformningsundersøgelser og gøre åbne frigivelser synlige. Brugeren tillader implementering uden forespørgsler. De eksisterende ændringer forbliver.

1. Tester for loopback transport og ONNX pakkeundersøgelse før implementering; netværksadresser, udgiftsgrænser, afbrydelser og ukendte ressourcer.
2. Fælles planlægning meddelelser; Ollama Fået, LM Studio (127.0.0.1:1234) og llama.cpp (127.0.0.1:8080) Ingen cloud-URL'er eller automatiske udvekslinger af leverandører. lokale servere kan videreføre sig selv; deres drift forbliver synlig ansvaret for operatøren.
3. ONNX Runtime GenAI CPU i egen proces med tidsgrænse, begrænset JSON-protokoll og processudbrud. Vælg udtrykkeligt modelordrer; ingen model downloades automatisk. Kun GenAI kompatible tekstmodeller, ingen .onnx netværk.
4. WPF valg for køretid, model, ONNX ordre; alle leverer den samme strengt kontrollerede design.
5. Teststand med klare grænser for hardware, DPI, mobilpanel og eksklusive makropad. Fysiske tests og en undertegnet/licenseret driver kan ikke erstattes af software.
6. Release-Build, Core/Windows Test, WPF Smoke, Worker/Package Test og uafhængig anmeldelse. Oprette et nyt komplet pakke; modelkvalitet kun rapporteres med den faktiske eksisterende model som testet.

Resultatet: fire løbtider implementeret, ONNX indfødt testet separat, udvælgelse af leverandør og teststand i pakket WPF smoke. Core/Windows-Suiter er grønne Reviewbefund Unicodegrense ved hjælp af fælles konstant og regression rettet. Virkelige hardware/mobile/DPI-udvekslinger, drivere, komplette oversættelser og live-instruktion kvalitet forbliver udtrykkeligt åbent arbejde; ikke som fuldstændig udbetalt.

Fortsættelse: Indførte separate Jinja- og udpegede/default-mønstre; Typ-/grænse-/Fallback-regressionsundersøgelser er gennemgået. Værktøj navigation suppleret i 27 kataloger. Virkelige instruktionsmodeller separat downloadet og kontrolleret SHA256: Native CPU-inference fungerer, men 0/4 planlagte tilfælde opfylder JSON-aftalen; ingen handlinger udføres. Uafhængig anmeldelse: nul-Fallback korrigeret og testet, efter at der ikke er konkret yderligere resultater. Core og Windows KI-tester passeret, Release-Build 0 advarsler/fejl.

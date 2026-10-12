<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokale KI-tider

## Tjenestene

Værktøy → Åpne AI-forespørselassistent, eksplisitt aktiverer og velger AI-tid. Å dele ressurser, formulere mål, opprette et design og sjekke i redaktøren. Ingen leverandører utfører prosjektet automatisk. Det er ingen automatisk bytte av leverandører.

| Driftstid | Forutsetninger | Forbindelse |
|---|---|---|
| Ollama | Lokalt installert modell | 127.0.0.1:11434 |
| LM Studio | løpende lokal server med lokalt lastet modell og JSON-støtte | 127.0.0.1:1234 |
| llama.cpp | Funksjonell llama-server med lokal modell og JSON-støtte | 127.0.0.1:8080 |
| ONNX Runntid GenAI CPU | kompatibel, pålitelig lokal GenAI tekstmodellordner | separert bundet prosess, ikke HTTP |

HTTP-klienter bruker ikke Proxys og følger ikke videre retninger. Lokale servere kan også bruke andre tjenester, spesielt LM Link/Remote Routing kan ikke brukes til lokal behandling. Dette kan MAKRIVO ikke oppdages pålitelig gjennom det kompatible grensesnittet. Ollama fjerndata og skyenavn er ytterligere avvist. Autentiserte lokale servere kan ikke konfigureres i denne versjonen.

## ONNX

Velg genai_config.json i dialogen. Ordningen må inneholde tokenizer.json og en chat-skjema. Chat_template støttes som et tegnkjede, en nevnt standardmønster (Array eller Dictionary) og en separat chat_templat.jinja hvis det ikke er noen inline-mønster eller dette er null. Mye merker er avvist; maksimalt 65536 tegn. Populære .onnx-filer, visjons-/innbyggingsmodeller eller ren GPU/NPU-eksport er ikke møttmodeller.

Kombinasjon: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Leverandørkonfigurasjonen blir returnert til CPU. Ingen nedlasting og ingen modellvekt i pakken. Ingen Python installasjon er nødvendig. Bruk bare pålitelige modellpakker; den separate prosessen er en nedbrudd/utbrudd grense, ikke sikkerhetssandbok.

Grenser: 90 sekunder inkludert modelllasting, maksimalt 3072 prompttokener og 1024 generert tokener, tilleggsmodell kontekstgrense; strengt testet JSON og kjente ressurs-ID. Forkortet/forkortet svar vil bli avvist. Avslut/avslut slutter den startet arbeidsprosessen. Ingen gratis modellkommandoer og ingen programmer/filer i den separate ressurslisten for Prompten; målet som er skrevet av brukeren, kan inneholde slike data selv.

## Verifikasjon

- Core-Suite, Loopback transporttester med simulert LM-studio-/llama.cpp-/Ollama svar, skema/avbrudd/ressursprøve.
- Unicode-protokollregresjon med maksimalt lange mål og 24 ressursnavne.
- Virkelig innfødte CPU-inferanse med offisiell tiny-random-gpt2-fp32-Testgraph fra microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testmønsteret er supplert og byte tokenizer tilpasset den lille tilfeldig graf. Denne tilfeldighetsmodellen sjekker ikke kvaliteten og er ikke i utgivelse.
- Reproduksjon av Native Runtime Test: Tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-modellordner>.
- WPF-Smoke undersøker valget av alle fire leverandører og testnivået; Valget alene skaper ingen etterspørsel.

## Resterende reelle frigjøringer

Hardware/smartphone/tablet/monitor-utvekslingstester er fortsatt nødvendig. Eksklusiv tastaturavtrykk forblir låst uten signert, lisensiert og verifisert filterdriver. Ikke erstattet av dummytreiber eller Raw Input Fokusskiften kan fortsatt blokkeres av Windows; feilen kaller nå manuell aktivering av målvinduet. Sentrale AI-forbindelser ble lagt til oversettelser i alle 27 kataloger; detaljerte nye tekster og faglige oversettelsesavdrag er fortsatt åpne. Livskvalitet med en egnet instruksjonsmodell, varig drift og målt tid er ikke bevist av tilfeldig modell/fake HTTP-tester.

## Kildene

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Endelig teststand: Release-build uten advarsler/feil; full core og Windows-swit, inkludert native focus fixture, HTTPS, UIA, plug-in IPC og kompatible AI transporttester. Nytt pakke frigjøring20261012-190618: WPF smoke (19:07 time), koblet ONNX CPU-inferanse med testgraph overlevde. Uavhengige gjennomgang: Unicode protokoll grense korrigert, ingen ytterligere konkrete oppdagelser. Ingen fysiske produsent/telefon/monitoravfall er avledet av dette.

## Fortsettelse: ekte modellkvalitet og maler

Ytterligere CPU-løp med allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: innfødte inferanse. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Kommunen eksporterer bare i testområdet, ikke i utgivelsen.

Fire virkelige planleggingstester (tysk/engelsk, dokument, program, eksplisitt mikrofon støy): **0/4 Gjeldende planer**; JsonException ved strenge responsundersøkelser 63501, 6376, 72118 og 6774 Denne lille modellen oppfyller ikke planleggingsavtalen og anbefales ikke som egnet. rapport: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Tests --evaluate-onnx <modellordner> <rapporteringsteg> Funksjonen for utløp av kjennetegn viser ikke modellkvalitet.

Verktøynavigasjon og testrapporter ble ytterligere oversatt til alle 27 språkkataloger. Langtekster og språkkurs forblir åpne. Modellerregresjon undersøker også feil JSON-typer, tomme/mellang/overlange maler og null med Jinja Fallback.

Oppdatert pakke: Artifacts/release-20261012-201134 Pakket WPF-smoke på 12.10.2026 rundt 20:12; innfødte CPU-inferanse med den separate testmodellen i den kombinerte Worker. Core og Windows KI-tester gjennomgått; Bygg uten advarsler/feil. Modellplanlegging kvalitet fortsetter 0/4.

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

Velg genai_config.json i dialogboksen. Mappen må inneholde tokenizer.json og en chatmal. Støttede former er chat_template som streng, en navngitt default-mal (array eller ordbok) samt en separat chat_template.jinja hvis inline-malen mangler eller er null. Tvetydige maler avvises; maksimalt 65536 tegn. Vilkårlige .onnx-filer, syns-/embeddingmodeller eller eksporter bare for GPU/NPU er ikke møtemodeller.

Kombinasjon: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Leverandørkonfigurasjonen blir returnert til CPU. Ingen nedlasting og ingen modellvekt i pakken. Ingen Python installasjon er nødvendig. Bruk bare pålitelige modellpakker; den separate prosessen er en nedbrudd/utbrudd grense, ikke sikkerhetssandbok.

Grenser: 90 sekunder inkludert modelllasting, maksimalt 3072 prompttokener og 1024 generert tokener, tilleggsmodell kontekstgrense; strengt testet JSON og kjente ressurs-ID. Forkortet/forkortet svar vil bli avvist. Avslut/avslut slutter den startet arbeidsprosessen. Ingen gratis modellkommandoer og ingen programmer/filer i den separate ressurslisten for Prompten; målet som er skrevet av brukeren, kan inneholde slike data selv.

## Verifikasjon

- Core-Suite, Loopback transporttester med simulert LM-studio-/llama.cpp-/Ollama svar, skema/avbrudd/ressursprøve.
- Unicode-protokollregresjon med maksimalt lange mål og 24 ressursnavn.
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

Endelig teststatus: Release-bygg uten advarsler/feil; full Core- og Windows-suite bestått, inkludert native fokusfixture, HTTPS, UIA, Plugin-IPC og kompatible AI-transporttester. Ny pakke release-20261012-190618: WPF-smoke bestått (kl. 19:07), medfølgende ONNX-CPU-inferens med testgraf bestått. Uavhengig gjennomgang: Unicode-protokollgrense rettet, ingen ytterligere konkrete funn. Ingen fysisk produsent-/telefon-/skjermaksept utledet av dette.

## Fortsettelse: ekte modellkvalitet og maler

Ekstra CPU-kjøring med allama-ai/SmolLM-360M-Instruct-onnx-int4, revisjon 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native inferens bestått. Modellvektenes SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Community-eksport bare i testområdet, ikke i utgivelsen.

Fire reelle planleggingstester (tysk/engelsk, dokument, program, uttrykkelig mikrofondemping): **0/4 gyldige planer**; JsonException ved streng svarkontroll i hvert forsøk. Kjøretider 63501, 6376, 72118 og 6774 ms. Ingen handlinger utført. Denne lille modellen oppfyller ikke planleggingskontrakten og anbefales ikke som egnet. Rapport: artifacts/local-ai-evaluation/smollm360-results.json. Reproduksjon: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. At runtime fungerer, dokumenterer ikke modellkvalitet.

Verktøynavigasjon og testrapport er dessuten oversatt i alle 27 språkkataloger. Lange tekster og språklig godkjenning gjenstår. Malregresjonen kontrollerer også feil JSON-typer, tomme/tvetydige/for lange maler og null med Jinja-reserve.

Oppdatert pakke: artifacts/release-20261012-201134. Pakket WPF-smoke bestått 12.10.2026 kl. 20:12; native CPU-inferens med den separate Instruct-testmodellen i den medfølgende workeren bestått. Core- og Windows-KI-tester bestått; bygg uten advarsler/feil. Modellens planleggingskvalitet fortsatt 0/4.

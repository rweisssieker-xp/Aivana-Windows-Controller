<!-- generated-doc locale=da-DK source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokale KI-tider

## Servicen

Værktøjer → Åbne AI-spørgselassistent, aktivere udtrykkeligt og vælge AI-tid. Del ressourcerne, formulere målet, oprette et design og tjekke i redaktøren. Ingen leverandør udfører projektet automatisk. Der er ingen automatisk udskiftning.

| løbtid | Forudsætningen | Forbindelse |
|---|---|---|
| Ollama | Lokalt installeret model | 127.0.0.1:11434 |
| LM Studio | løbende lokale servere med lokalt opladt model og JSON-støtte | 127.0.0.1:1234 |
| llama.cpp | Funktionelle llama-server med lokal model og JSON-støtte | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | kompatibel, pålidelig lokal GenAI tekstmodeller | separat bundet proces, ikke HTTP |

HTTP-klienter bruger ikke Proxys og følger ikke henvisninger. Lokale servere kan selv anvende yderligere tjenester; især LM Link/Remote Routing må ikke bruges til lokal behandling. Dette kan MAKRIVO ikke opdages pålideligt gennem den kompatible grænseflade. Ollama fjerntliggende data og cloudnavne afvises yderligere. Autentiserede lokale servere kan ikke konfigureres i denne version.

## ONNX

Vælg genai_config.json i dialogen. Folderen skal indeholde tokenizer.json og en chatmode. Chat_template understøttes som et tegnskæde, en udnævnt standardmønster (Array eller Dictionary) samt en separat chat_templat.jinja, hvis der ikke er en inline-mønster eller den er null. Mængder af tegn afvises; maksimalt 65536 tegn. Populære .onnx-filer, vision-/embedsmodeller eller ren GPU/NPU-eksport er ikke mødemodeller.

Kombineret: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Leverandørkonfigurationen returneres til CPU. Ingen download og ingen modelvægt i pakken. Der kræves ingen Pythoninstallation. Brug kun pålidelige modelpakker; den separate proces er en nedbrydnings- / nedbrydningsgrænse, ikke sikkerhedssandbox.

Grænser: 90 sekunder, herunder modelladning, maksimalt 3072 prompttokens og 1024 genererede tokens, yderligere modelkontekstgrænse; strengt kontrolleret JSON og kendte ressource-IDs. Den forkerte/falske svar afvises. Lukning / afbrydelse afslutter den indledte arbejdsproces. Der er ingen gratis modelmodeller og ingen programmer/filer i den adskilte ressourceliste i Prompten; brugeren skriver målet kan indeholde sådanne data selv.

## Verifikation

- Core-Suite, Loopback Transporttests med simuleret LM-studio-/llama.cpp-/Ollama svar, Schema/Abbruch/Ressourcen test.
- Unicode-protokollregression med maksimalt lange mål og 24 ressourcenavne.
- Virkelige indfødte CPU-inference med den officielle tiny-random-gpt2-fp32-Testgraph fra microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testmønsteret er suppleret og byte tokeniser tilpasset den lille tilfældig graf. Denne tilfældige model kontrollerer ikke kvaliteten og er ikke i udgivelse.
- Reproduktiv indfødt test: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-modeller>.
- WPF-Smoke kontrollerer valget af alle fire leverandører og teststand; Valget alene skaber ingen efterspørgsel.

## Resten af virkelige frigivelser

Hardware/smartphone/tablet/monitorudskiftningstests er fortsat nødvendige. Eksklusiv tastaturindtrykning forbliver blokeret uden en undertegnet, licenseret og kontrolleret filterdrivende. Ikke udskiftet af dummytreber eller Raw Input. Fokusændringen kan fortsat blokeres ved at Windows; fejlen kaldes nu manuelt aktivering af målvinduet. Centrale AI-forbindelser er blevet tilføjet oversættelser i alle 27 kataloger; detaljerede nye tekster og faglige oversættelsesaftagelser er stadig åbne. Live-kvalitet med en passende instruktionsmodel, varig drift og målt tidsindtjening er ikke dokumenteret af tilfældig model/fake HTTP-test.

## Kildene

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Final test: Release-build uden advarsler/fejl; fuld Core og Windows-suiet, herunder native Focus Fixture, HTTPS, UIA, plug-in IPC og kompatible AI transporttester. Ny pakke frigivelse20261012-190618: WPF smoke (19:07 time), bundet ONNX CPU-inference med testgraph. Uafhængig anmeldelse: Unicode-protokoll grænse korrigeret, ingen yderligere konkrete resultater. Der er ingen fysiske producenter/telefon/monitorindtagelse af dette.

## Fortsættelse: ægte modelkvalitet og modeller

Yderligere CPU-løbet med allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: indfødte inference. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Eksport kun i testområdet, ikke i udgivelsen.

Fire reelle planlægningstester (tysk/engelsk, dokument, program, eksplisiv mikrofon støj): **0/4 Gyldige planer**; JsonException ved strenge svarundersøgelser. 63501, 6376, 72118 og 6774 Denne lille model opfylder ikke planlægningsaftalen og anbefales ikke som egnet. rapport: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Tests --evaluate-onnx <modellordner> <rapporteringstejlen>. Funktionen for udløbet dokumenterer ikke modelkvalitet.

Værktøjnavigation og testrapporter er yderligere oversat til alle 27 sprogkataloger. Langtekster og sproglige aftag forbliver åbne. Modellen regression kontrollerer også falske JSON-typer, tomme/mængde/overlange modeller og nul med Jinja-fallback.

Opdateret pakke: Artifacts/release-20261012-201134 Pakket WPF smoke på 12.10.2026 omkring 20:12; indfødte CPU-inference med den separate instruct-testmodel i den bundede Worker. Core og Windows KI-tester gennemgået; opbygge uden advarsler/fejl. Kvalitet af planlægning fortsætter 0/4.

<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokala KI-tider

## Tjänst

Verktyg → Öppna AI-konversationsassistenten, aktivera uttryckligen och välja AI-tid. Dela resurser, formulera mål, skapa design och kontrollera i redaktören. Ingen leverantör utför projektet automatiskt. Det finns ingen automatisk byte av leverantörer.

| Drifttid | Förutsättning | Förbindelse |
|---|---|---|
| Ollama | Lokalt installerad modell | 127.0.0.1:11434 |
| LM Studio | körande lokala servrar med lokalt laddat modell och JSON-schema stöd | 127.0.0.1:1234 |
| llama.cpp | körande llama-server med lokal modell och JSON-schema stöd | 127.0.0.1:8080 |
| ONNX körtid GenAI CPU | kompatibla, tillförlitliga lokala GenAI textmodeller | separata processer, inte HTTP |

HTTP-klienterna använder inte Proxys och följer inga riktlinjer. Lokala servrar kan även ansöka om ytterligare tjänster, särskilt LM Link/Remote Routing får inte användas för lokal bearbetning. Detta kan inte pålitligt upptäcka MAKRIVO genom det kompatibla gränssnittet. Ollama fjärrdata och molnnamn avvisas ytterligare. Autentiserade lokala servrar är inte konfigurerbara i den här versionen.

## ONNX

Välj genai_config.json i dialogen. Fliken måste innehålla tokenizer.json och en chattmall. Chat_template stöds som en tecken kedja, en standardmall (Array eller Dictionary) samt en separat chat_templat.jinja om det inte finns någon inlinemall eller den är noll. Det finns flera tecken som avvisas, högst 65536 tecken. Populära .onnx-filer, vision/förpackningsmodeller eller ren GPU/NPU-export är inte mötesmodeller.

Kombinerad: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Leverantörskonfigurationen återställs till CPU. Inga nedladdningar och inga modellvikter i paketet. Det behövs ingen Pythoninstallation. Använd endast tillförlitliga modeller; den separata processen är en nedbrytningsgräns, inte en säkerhetssandbox.

Gränser: 90 sekunder inklusive modellladdning, högst 3072 prompttokens och 1024 genererade tokens, ytterligare modellkontextgräns; strikt kontrollerad JSON och kända resurs-ID. Ett felaktigt/avskräckt svar avvisas. Avsluta / avsluta arbetsprocessen som startas. Inga fria modeller och inga program/filer i den separata resurslistan för prompten; användarens måltext kan innehålla sådana uppgifter själva.

## Verifikation

- Core-Suite, Loopback transporttest med simulerade LM-studio-/llama.cpp-/Ollama svar, schema/avbrott/resursprovning.
- Unicode-protokollregression med maximala långsiktiga mål och resursnamn 24.
- Verkliga inhemska CPU-inferens med officiell tiny-random-gpt2-fp32-Testgraph från microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testmallen kompletteras och byte-tokeniser anpassas till den lilla slumpmässiga diagrammet. Denna slumpmässiga modell kontrollerar inte kvaliteten och är inte i release.
- Reproduktiv nativ körtest: tools/check-onnx-runtime.ps1 -ModellDirectory <GenAI-modellordnare>.
- WPF-Smoke undersöker valet av alla fyra leverantörer och testet; valet ensamt genererar ingen modellfråga.

## Resterande verkliga avgifter

Hardware/Smartphone/Tablet/Monitorändringstester behövs fortfarande. Exklusiv tangenttryckning förblir blockerad utan undertecknade, licensierade och kontrollerade filter. Inte ersatt av Dummytrivar eller Raw Input Fokusväxlingen kan fortfarande blockeras av Windows; felet kallas nu manuell aktivering av målfönstret. Centrala AI-komponenter har läggts till översättningar i alla 27 kataloger; detaljerade nya texter och tekniska översättningsavdrag är fortfarande öppna. Livskvalitet med en lämplig instruktionsmodell, varaktig drift och mätad tidsperiod är inte bevisad av slumpmässiga / falska HTTP-test.

## Källor

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Slutstopp: Release-bygg utan varningar/fel; fullständiga kärn- och Windows-serier, inklusive nativ fokusfixture, HTTPS, UIA, plug-in IPC och kompatibla AI-transporttester. Ny paket release-20261012-190618: WPF smoke överlevde (19:07 klocka), bundna ONNX CPU-inferens överlevde med testgraph. Oberoende granskning: Unicode-protokoll gränsen korrigeras, inga ytterligare konkreta fynd. Inga fysiska tillverkare/telefon/monitoravdrag härrör från det.

## Fortsätt: Verklig modellkvalitet och mönster

Ytterligare CPU körning med allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: inhemska inflation. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Exporteras endast i testområdet, inte i utgivningen.

Fyra verkliga planeringstester (tyska/engelska, dokument, program, uttrycklig mikrofonstryck): **0/4 Gällande planer**; JsonException vid strikt responsprov. 63501, 6376, 72118 och 6774 ms. Inga åtgärder utförs. Denna lilla modell uppfyller inte planeringsavtalet och rekommenderas inte som lämplig. rapport: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Tests --evaluate-onnx <modellordnare> <rapporteringsteg> Funktionen inte visar modellkvalitet.

Verktygsnavigering och testrapporter har dessutom översatts till alla 27 språkkataloger. Längre texter och språkkoppling är öppna. Modellerregression undersöker också felaktiga JSON-typer, tomma/multilångt/överlängda mönster och noll med Jinja-fallback.

Uppdaterad paket: Artifacts/release-20261012-201134 Förpackade WPF-smoken överlevde 12.10.2026 runt 20:12; inhemska CPU-inferens överlevde med den separata instruct-testmodellen i den kombinerade arbetaren. Core och Windows KI-test passerade; Bygg utan varningar/fel. Modellplanering kvalitet fortsätter 0/4.

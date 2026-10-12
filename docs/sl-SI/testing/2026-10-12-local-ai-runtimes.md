<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokalni časovni čas

## Storitev

Orodja → Odprite pomočnika za razpravo o inteligenci, izrecno aktivirajte in izberite čas trajanja inteligence. Razdelite vire, oblikujte cilje, ustvarite načrt in preverite v uredniku. Noben ponudnik načrta ne izvaja samodejno. Ni avtomatske zamenjave ponudnikov.

| Čas poteka | Predpogoj | Povezava |
|---|---|---|
| Ollama | lokalno nameščen model | 127.0.0.1:11434 |
| LM Studio | lokalni strežnik z lokalno naloženim modelom in podporo za shemo JSON | 127.0.0.1:1234 |
| llama.cpp | Deluje na strežniku z lokalnim modelom in podporo za shemo JSON | 127.0.0.1:8080 |
| ONNX Čas delovanja GenAI CPU | združljiv, zanesljiv lokalni modelni sistem GenAI | ločen proces, ne HTTP |

HTTP stranke ne uporabljajo proxy in ne sledijo nadaljnjim smernicam. Lokalni strežniki se lahko obrnejo tudi na druge storitve, zlasti LM Link/Remote Routing se ne sme uporabljati za lokalno obdelavo. To ni mogoče zanesljivo zaznati MAKRIVO prek združljivega vmesnika. Ollama daljinski podatki in imena v oblaku so dodatno zavrnjeni. Avtentizirani lokalni strežniki niso konfigurirani v tej različici.

## ONNX

V dialogu izberite genai_config.json. V mapi mora biti tokenizer.json in šablona za klepet. Podpira se chat_template kot znakov, imenovana predloga (Array ali slovar) in ločena chat_t template.jinja, če ni vstopnega predloga ali je nula. Večnamenski vzorci so zavrnjeni; največ 65536 znakov. Priljubljene datoteke .onnx, vizualni/oblikovalni modeli ali čisti izvoz GPU/NPU niso modeli srečanja.

Združeno: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Konfiguracija ponudnika se vrne na CPU. Brez prenosa in brez teže v paketu. Ni potrebe po namestitvi Python. Uporabite samo zaupanja vredne pakete; ločen proces je omejitev razpadanja, ne varnostna škatla.

Omejitve: 90 sekunde, vključno s polnjenjem modelov, največ 3072 tokenov in 1024 generiranih tokenov, dodatni okvir modelov; strogo preverjeni JSON in znani ID virov. Napačen/izkrivljen odgovor se zavrne. Zaprtje / zaprtje konča začetni proces. Brez brezplačnih ukazov modela in nobenih programov/vrstice v ločenem seznamu virov; ciljni tekst, ki ga je napisal uporabnik, lahko vsebuje te podatke.

## preverjanje

- Core Suite, Loopback prevozni testi s simuliranim LM studio-/llama.cpp-/Ollama odzivom, shema/prehod/pregled virov.
- Unicode regresija protokola z najdaljimi cilji in 24 imena virov.
- Resnično native CPU reference z uradno tiny-random-gpt2-fp32-Testgraph iz microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testni vzorec dopolnjuje in prilagodi tokenizator za majhno naključno grafiko. Ta naključni model ne pregleda kakovosti in ni v objavi.
- Reproduktivni nativni test: orodja/kontrolirajte-onnx-runtime.ps1 -ModelDirectory <GenAI-modelni red>.
- WPF-Smoke preuči izbiro vseh štirih ponudnikov in preskus; samo izbira ne ustvarja povpraševanja po modelu.

## Resnične izpuste

Preizkušnje za zamenjavo strojne opreme/pametnih telefonov/tabletov/monitorjev so še vedno potrebne. Ekskluzivna tipkovnica ostane blokirana brez podpisanih, licenciranih in preverjenih filtrov. Ne nadomestite z gumijastimi vozili ali Raw Input. Spreminjanje osredotočenosti se lahko še naprej blokira z Windows; napaka se zdaj imenuje ročno aktiviranje ciljnega okna. Osrednji elementi inteligence so bili dodani prevodi v vseh katalogih 27; podrobne nove besedila in strokovni prevodi so še vedno odprti. Kakovost v živo z ustreznim modelom navodil, trajnostjo in izmerjenim časom ni dokazano s naključnimi modeli/fake HTTP testiranja.

## Viri

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Končni test: Release-build brez opozoril / napake; polni Core in Windows suite, vključno z native Focus Fixture, HTTPS, UIA, plug-in IPC in združljivih prevoznih testov. Novi paket izdaje20261012-190618: WPF-Smoke (19:07 uro), povezana ONNX-CPU reference s test graf. Neodvisna pregleda: Unicode protokola omejitev popravljena, nobenih drugih konkretnih ugotovitev. Iz tega ni fizičnega proizvajalca/telefona/monitorja.

## Nadaljevanje: resnična kakovost in vzorci

Dodatno CPU tek z allama-ai/SmolLM-360M-Instruct-onnx-int4, revizija 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native infertility. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Skupnost izvoz samo v testnem območju, ne v razpisu.

Štirje resnični testi načrtovanja (nemščina/angleščina, dokument, program, izrecna mikrofonska zvoka): **0/4 veljavni načrti**; JsonException pri strogem pregledu odgovorov. 63501, 6376, 72118 in 6774 ms. Ni ukrepov. ta majhen model ne izpolnjuje pogodbe o načrtovanju in ni priporočljivo. poročilo: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Tests --evaluate-onnx <modelni red> <poročni korak>. funkcija trajanja ne dokazuje kakovosti modela.

Navigacija orodij in poročila o preskušanju so bile dodane v vseh 27 jezikovnih katalogov. Dolgotrajni besedila in jezikovni odtisi ostanejo odprti. Regresija predloge preuči tudi napačne vrste JSON, prazne/množice/dolgotrajne predloge in nič z Jinja-Fallback.

Posodobljen paket: Artifacts/release- 20261012-201134 Pakirani WPF smoke so preživeli na 12.10.2026 okoli 20:12; native CPU reference z ločenim testnim modelom v povezanem delavcu. Core in Windows testov; Izdelava brez opozoril / napake. Kakovost načrtovanja modela še naprej 0/4.

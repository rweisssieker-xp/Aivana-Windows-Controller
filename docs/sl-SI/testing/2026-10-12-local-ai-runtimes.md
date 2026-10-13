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
| ONNX Runtime GenAI CPU | združljiv, zanesljiv lokalni modelni sistem GenAI | ločen proces, ne HTTP |

HTTP stranke ne uporabljajo proxy in ne sledijo nadaljnjim smernicam. Lokalni strežniki se lahko obrnejo tudi na druge storitve, zlasti LM Link/Remote Routing se ne sme uporabljati za lokalno obdelavo. To ni mogoče zanesljivo zaznati MAKRIVO prek združljivega vmesnika. Ollama daljinski podatki in imena v oblaku so dodatno zavrnjeni. Avtentizirani lokalni strežniki niso konfigurirani v tej različici.

## ONNX

V pogovornem oknu izberite genai_config.json. Mapa mora vsebovati tokenizer.json in predlogo klepeta. Podprti so chat_template kot niz, poimenovana predloga default (polje ali slovar) ter ločena chat_template.jinja, če vgrajene predloge ni ali je null. Dvoumne predloge so zavrnjene; največ 65536 znakov. Poljubne datoteke .onnx, modeli za vid/vektorske vložitve ali izvozi samo za GPU/NPU niso modeli za sestanke.

Priloženo: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Nastavitev ponudnika se ponastavi na CPU. V paketu ni prenosov ali uteži modelov. Namestitev Python ni potrebna. Uporabljajte samo zaupanja vredne pakete modelov; ločeni proces predstavlja mejo za zrušitve/preklic, ne varnostnega peskovnika.

Omejitve: 90 sekund, vključno z nalaganjem modela, največ 3072 vhodnih žetonov in 1024 ustvarjenih žetonov, dodatno omejeno s kontekstom modela; strogo preverjen JSON in znani identifikatorji virov. Napačen/odrezan odgovor se zavrže. Zapiranje/preklic konča zagnani delovni proces. Ni poljubnih ukazov modela ter programov/poti datotek v ločenem seznamu virov poziva; uporabniško besedilo cilja lahko samo vsebuje take podatke.

## preverjanje

- Core Suite, Loopback prevozni testi s simuliranim LM studio-/llama.cpp-/Ollama odzivom, shema/prehod/pregled virov.
- Regresijski preizkus protokola Unicode z najdaljšimi dovoljenimi cilji in 24 imeni virov.
- Pravo nativno sklepanje na CPU z uradnim preizkusnim grafom tiny-random-gpt2-fp32 iz microsoft/onnxruntime-genai, revizija 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Dodana je bila preizkusna predloga, bajtni tokenizator pa prilagojen majhnemu naključnemu grafu. Ta naključni model ne preverja kakovosti in ni del izdaje.
- Reproduktivni nativni test: orodja/kontrolirajte-onnx-runtime.ps1 -ModelDirectory <GenAI-modelni red>.
- WPF-Smoke preuči izbiro vseh štirih ponudnikov in preskus; samo izbira ne ustvarja povpraševanja po modelu.

## Resnične izpuste

Preizkušnje za zamenjavo strojne opreme/pametnih telefonov/tabletov/monitorjev so še vedno potrebne. Ekskluzivna tipkovnica ostane blokirana brez podpisanih, licenciranih in preverjenih filtrov. Ne nadomestite z gumijastimi vozili ali Raw Input. Spreminjanje osredotočenosti se lahko še naprej blokira z Windows; napaka se zdaj imenuje ročno aktiviranje ciljnega okna. Osrednji elementi inteligence so bili dodani prevodi v vseh katalogih 27; podrobne nove besedila in strokovni prevodi so še vedno odprti. Kakovost v živo z ustreznim modelom navodil, trajnostjo in izmerjenim časom ni dokazano s naključnimi modeli/fake HTTP testiranja.

## Viri

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Končno stanje preverjanja: gradnja Release brez opozoril/napak; celotni zbirki Core in Windows uspešni, vključno z nativnim preizkusnim okoljem fokusa, HTTPS, UIA, Plugin-IPC in združljivimi transportnimi preizkusi umetne inteligence. Novi paket release-20261012-190618: WPF-Smoke uspešen (19:07), priloženo sklepanje ONNX na CPU s preizkusnim grafom uspešno. Neodvisni pregled: popravljena meja protokola Unicode, brez nadaljnjih konkretnih ugotovitev. Iz tega ne izhaja fizično sprejemno preverjanje naprav proizvajalcev/telefonov/monitorjev.

## Nadaljevanje: prava kakovost modela in predloge

Dodaten preizkus CPU z allama-ai/SmolLM-360M-Instruct-onnx-int4, revizija 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: nativno sklepanje je uspelo. SHA256 uteži modela: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Izvoz skupnosti samo v preizkusnem območju, ne v izdaji.

Štirje pravi preizkusi načrtovanja (nemščina/angleščina, dokument, program, izrecno utišanje mikrofona): **0/4 veljavnih načrtov**; vsakič JsonException pri strogem preverjanju odgovora. Časi izvajanja 63501, 6376, 72118 in 6774 ms. Nobeno dejanje ni bilo izvedeno. Ta majhni model ne izpolnjuje zahtev načrtovanja in ni priporočen kot primeren. Poročilo: artifacts/local-ai-evaluation/smollm360-results.json. Ponovitev: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Delovanje izvajalnega okolja ne dokazuje kakovosti modela.

Navigacija orodij in poročilo o preverjanju sta bila dodatno prevedena v vseh 27 jezikovnih katalogih. Daljša besedila in jezikovna odobritev ostajajo odprti. Regresijski preizkus predlog preverja tudi napačne tipe JSON, prazne/dvoumne/predolge predloge ter null z nadomestno predlogo Jinja.

Posodobljeni paket: artifacts/release-20261012-201134. Preizkus zapakiranega WPF-Smoke je uspel 12.10.2026 ob 20:12; nativno sklepanje CPU z ločenim preizkusnim modelom Instruct v priloženem delovnem procesu je uspelo. Preizkusi Core in Windows za umetno inteligenco so uspeli; gradnja brez opozoril/napak. Kakovost načrtovanja modela ostaja 0/4.

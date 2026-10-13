<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-12-local-ai-runtimes.md)

# Vietos IA laikotarpis

## aptarnavimas

Įrankiai → Atidarykite žvalgybos asistentą, aiškiai aktyvuokite ir pasirinkite žvalgymo laiką. Pasidalinkite ištekliais, formuluokite tikslą, sukurkite dizainą ir peržiūrėkite redaktorių. Nė vienas pardavėjas automatiškai nesinaudoja projektu. Nėra automatinio paslaugų keitimo.

| Laiko vykdymas | Prielaida | Ryšys |
|---|---|---|
| Ollama | Vietos įrengtas modelis | 127.0.0.1:11434 |
| LM Studio | vietinis serveris su vietiniu įkrovimu ir JSON schemos palaikymu | 127.0.0.1:1234 |
| llama.cpp | veikiantis „Fla“ serveris su vietiniu modeliu ir JSON schemos palaikymu | 127.0.0.1:8080 |
| ONNX Veiksmo laikas GenAI | suderinamas, patikimas vietinis GenAI teksto modelis | atskiras susietas procesas, ne HTTP |

HTTP klientai nenaudoja proksų ir nesilaiko tolesnių gairių. Vietiniai serveriai gali kreiptis į papildomas paslaugas, ypač LM ryšys / nuotolinis maršrutas negali būti naudojamas vietiniam apdorojimui. Tai negali būti patikimai nustatyta MAKRIVO suderinamu sąsaju. Ollama nuotoliniai duomenys ir debesų pavadinimai yra atmesti. Autentizuoti vietiniai serveriai šioje versijoje nėra konfigūruojami.

## ONNX

Dialoge pasirinkite genai_config.json. Aplanke turi būti tokenizer.json ir pokalbio šablonas. Palaikomi chat_template kaip eilutė, pavadintas default šablonas (masyvas arba žodynas), taip pat atskiras chat_template.jinja, jei įterpto šablono nėra arba jo reikšmė yra null. Nevienareikšmiai šablonai atmetami; daugiausia 65536 simboliai. Bet kokie .onnx failai, regos/įterpinių modeliai ar vien GPU/NPU eksportai nėra susitikimų modeliai.

Sujungtas: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Pardavėjo konfigūracija grąžinama į CPU. Nėra atsisiuntimo ir jokių modelio svorį. Nereikalaujama Python įdiegimo. Naudokite tik patikimus modelio paketus; atskiras procesas yra griovimo / griovimo riba, o ne saugos sandėlis.

Ribos: 90 sekundės, įskaitant modelio įkrovimą, didžiausią 3072 prompto tokenų ir 1024 generuojamų tokenų, papildomai modelio kontekstą; griežtai patikrinti JSON ir žinomų išteklių ID. Neteisingas / netinkamas atsakymas yra atmestas. Uždarymas / uždarymas baigiasi pradėtu darbuotojo procesu. Nėra nemokamų modelio komandų ir jokių programų / failų etapų atskirame išteklių sąraše; naudotojo parašytas tikslinis tekstas gali turėti tokius duomenis.

## Tikrinimas

- Core Suite, Loopback transporto testai su simuliuotais LM studijos / llama.cpp / Ollama atsakymais, schema / atnaujinimas / išteklių patikrinimas.
- Unicode protokolo regresijos testas su didžiausio ilgio tikslais ir 24 išteklių pavadinimais.
- Tikras gimtoji CPU su oficialiu tiny-random-gpt2-fp32-Testgraph iš Microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Bandymo šablonas papildomas ir byto tokenizatorius pritaikytas prie mažo atsitiktinio grafiko. Šis atsitiktinis modelis nekelia kokybės ir nėra išleidžiamas.
- Reproduktyvus gimtojo paleidimo testas: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI modelis>.
- WPF Smoke nagrinėja visų keturių tiekėjų pasirinkimą ir bandomąją padėtį; vienintelis pasirinkimas nesukelia modelio paklausos.

## Likusios tikros laisvės

Hardware/Smartphone/Tablet/Monitor keitimo testai vis dar reikalingi. Išskirtinis klaviatūros spausdinimas lieka užrakintas be pasirašytų, licencijuotų ir tikrinamų filtravimo laidų. Nenaudoti dulkių variklis arba Raw Input Fokusavimo keitimas gali būti blokuojamas Windows; klaida dabar vadinama rankiniu tikslinio lango aktyvavimu. Pagrindiniai IRT elementai buvo įtraukti į visus 27 katalogus; išsamūs nauji tekstai ir profesionalūs vertimo duomenys vis dar yra atviri. Gyvoji kokybė, turinti tinkamą instruktavimo modelį, nuolatinę veiklą ir išmatuotą laiko pelną, nėra įrodyta atsitiktiniais / klaidingomis HTTP bandymais.

## Šaltiniai

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Galutinė patikros būsena: Release-Build be įspėjimų/klaidų; visos Core ir Windows testų rinkinių patikros sėkmingos, įskaitant savąją fokuso bandymų aplinką, HTTPS, UIA, Plugin-IPC ir suderinamų AI transportų testus. Naujas paketas release-20261012-190618: WPF-Smoke sėkmingas (19:07), įtrauktos ONNX-CPU aplinkos išvedimas su bandymų grafu sėkmingas. Nepriklausoma peržiūra: Unicode protokolo riba pataisyta, kitų konkrečių radinių nėra. Tai nepatvirtina fizinės gamintojų aparatūros/telefonų/monitorių priėmimo.

## Toliau: tikroji kokybė ir modeliai

Papildomas CPU vykdymas su allama-ai/SmolLM-360M-Instruct-onnx-int4, revizija 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: savasis išvedimas sėkmingas. Modelio svorių SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Bendruomenės eksportas naudojamas tik bandymų srityje, ne leidime.

Keturi tikri planavimo bandymai (vokiečių/anglų kalbomis, dokumentas, programa, aiškiai prašomas mikrofono nutildymas): **0/4 galiojantys planai**; kiekvienu atveju griežtai tikrinant atsakymą kilo JsonException. Vykdymo laikai: 63501, 6376, 72118 ir 6774 ms. Jokie veiksmai nevykdyti. Šis mažas modelis neatitinka planavimo sutarties ir nerekomenduojamas kaip tinkamas. Ataskaita: artifacts/local-ai-evaluation/smollm360-results.json. Atkūrimas: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Vykdymo aplinkos veikimas neįrodo modelio kokybės.

Įrankių navigacija ir patikros ataskaita papildomai išverstos visuose 27 kalbų kataloguose. Ilgieji tekstai ir kalbinis patvirtinimas dar nebaigti. Šablonų regresijos testas taip pat tikrina neteisingus JSON tipus, tuščius/nevienareikšmius/per ilgus šablonus ir null su atsarginiu Jinja šablonu.

Atnaujintas paketas: artifacts/release-20261012-201134. Supakuotas WPF-Smoke bandymas sėkmingas 12.10.2026 20:12; savasis CPU išvedimas su atskiru Instruct bandymų modeliu įtrauktame darbiniame procese sėkmingas. Core ir Windows-KI testai sėkmingi; surinkta be įspėjimų/klaidų. Modelio planavimo kokybė vis dar 0/4.

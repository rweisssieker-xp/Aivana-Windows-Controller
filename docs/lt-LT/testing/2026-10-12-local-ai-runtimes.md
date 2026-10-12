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

Dialoge pasirinkite genai_config.json. Slapukas turi būti tokenizer.json ir pokalbių šablonas. Chat_template palaikomas kaip ženklo grandinė, vadinamasis numatytuoju pavyzdžiu (Array arba žodynas), taip pat atskiras chat_templates.jinja, jei nėra inline pavyzdžio arba jis yra nulinis. Daugiafunkcinių pavyzdžių atmetama; maksimalus 65536 ženklas. Populiariausi .onnx failai, vizijos / įtraukimo modeliai arba grynos GPU / NPU eksportai nėra susitikimo modeliai.

Sujungtas: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Pardavėjo konfigūracija grąžinama į CPU. Nėra atsisiuntimo ir jokių modelio svorį. Nereikalaujama Python įdiegimo. Naudokite tik patikimus modelio paketus; atskiras procesas yra griovimo / griovimo riba, o ne saugos sandėlis.

Ribos: 90 sekundės, įskaitant modelio įkrovimą, didžiausią 3072 prompto tokenų ir 1024 generuojamų tokenų, papildomai modelio kontekstą; griežtai patikrinti JSON ir žinomų išteklių ID. Neteisingas / netinkamas atsakymas yra atmestas. Uždarymas / uždarymas baigiasi pradėtu darbuotojo procesu. Nėra nemokamų modelio komandų ir jokių programų / failų etapų atskirame išteklių sąraše; naudotojo parašytas tikslinis tekstas gali turėti tokius duomenis.

## Tikrinimas

- Core Suite, Loopback transporto testai su simuliuotais LM studijos / llama.cpp / Ollama atsakymais, schema / atnaujinimas / išteklių patikrinimas.
- Unicode protokolo regresija su didžiausiais tikslais ir 24 išteklių pavadinimais.
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

Galutinis bandymas: išleidimas be įspėjimų / klaidų; pilnas pagrindinis ir Windows rinkinys, įskaitant gimtąjį fokusą, HTTPS, UIA, "Plugin" IPC ir suderinamus AI transporto testus. Naujas paketas išleidimas20261012-190618: WPF nuovargis (19:07 laikrodis), susietas ONNX CPU su testų grafika. Nepriklausomas apžvalga: Unicode protokolo riba ištaisyta, jokių papildomų konkrečių rezultatų. Nėra fizinio gamintojo / telefono / monitoriaus nuostolių.

## Toliau: tikroji kokybė ir modeliai

Papildomas CPU paleidimas su allama-ai/SmolLM-360M-Instruct-onnx-int4, peržiūra 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: gimtoji infuzija. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Bendrijos eksportas tik bandymo srityje, o ne išleidimo.

Keturi tikri planavimo testai (vokietiškai / anglų kalba, dokumentas, programa, aiškus mikrofonas): **0/4 Galiojantys planai**; kiekvienas JsonException griežtas atsakymo patikrinimas. 63501, 6376, 72118 ir 6774 Šis mažas modelis neatitinka planavimo sutarties ir nerekomenduojamas kaip tinkamas. ataskaita: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Testai --evaluate-onnx <modelų tvarkyklė> <reporto kelias>. Funkcija negali įrodyti modelio kokybės.

Įrankis navigacija ir bandymų ataskaitos buvo išversti į visus 27 kalbų katalogus. Ilgų tekstų ir kalbų atsisakymas lieka atviras. Šablonų regresija taip pat tikrina klaidingus JSON tipus, tuščias / daugialypės / pernelyg ilgas šablonus ir nulį su Jinja Fallback.

Atnaujintas paketas: artifaktai / išleidimas 20261012-201134. Pakuotės WPF smoke išliko 12.10.2026 aplink 20:12; gimtoji CPU infuzija su atskira instrukcijos bandymo modeliu buvo sujungtas darbuotojas. Pagrindiniai ir Windows KI testai buvo atlikti; Build be įspėjimų / klaidų. Modelio planavimo kokybė tęsiasi 0/4.

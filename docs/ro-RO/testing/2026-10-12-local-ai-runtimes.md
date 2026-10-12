<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-12-local-ai-runtimes.md)

# Timpurile locale de AI

## Serviciul

Instrumentele → A se deschide un asistent de informare AI, activarea explicită și selecția timpului de informare AI. A se dezvălui resurse, a se formula obiectiv, a se crea un proiect și a se verifica în editor. Nu există furnizor care să execute proiectul automat. Nu există schimb automat de furnizor.

| Timpul de desfășurare | Previziunea | legătura |
|---|---|---|
| Ollama | Modelul local instalat | 127.0.0.1:11434 |
| LM Studio | Server local în curs cu modelul încărcat local și sprijinul pentru schema JSON | 127.0.0.1:1234 |
| llama.cpp | un server de flame în curs cu model local și suport pentru schema JSON | 127.0.0.1:8080 |
| ONNX timp de rulare GenAI CPU | Compatibilă, fiabilă ordinea de text locală GenAI | Procesul separat, fără HTTP |

Clienții HTTP nu utilizează proxy-uri și nu urmează instrucțiuni. Serverele locale pot acorda servicii suplimentare; în special, LM Link/Remote Routing nu poate fi utilizat pentru prelucrarea locală. Acest lucru nu poate fi detectat în mod fiabil de către interfața compatibilă MAKRIVO. Ollama date de la distanță și nume cloud sunt refuzate în plus. Serverele locale autentificate nu pot fi configurate în această versiune.

## ONNX

În dialog, selectați genai_config.json. Folderul trebuie să conțină tokenizer.json și un șablon de chat. Chat_template sunt susținute ca lanț de caractere, un templu numit (Array sau Dictionary) și un templu separat chat_template.jinja, dacă nu există un templu inline sau acesta este zero. Se refuză modelele multiple; maximum 65536 semne. Fișierele .onnx populare, modelele de vizionare / ambedare sau exporturile pure GPU / NPU nu sunt modele de întâlnire.

Combinat: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Configurația furnizorului este returnată la CPU. Nu există descărcări și nu există greutăți de model în pachet. Nu este necesară instalarea Python. Folosiți numai pachete de modele de încredere; procesul separat este o limită de prăbușire / prăbușire, nu o cutie de siguranță.

Limitele: 90 secunde, inclusiv încărcarea de modele, maximum 3072 tokenuri generate și 1024, în plus, limitele de context ale modelului; JSON strict verificat și ID-uri de resurse cunoscute. Răspunsul greșit/receptat este refuzat. Închiderea/închiderea procesului de lucru inițiat. Nu există comenzi gratuite de model și nu există programe/file în lista de resurse separată a promptei; textul de destinație scris de utilizator poate conține aceste date în sine.

## Verificarea

- Core-Suite, testele de transport Loopback cu răspunsuri simulate LM-Studio-/llama.cpp-/Ollama, schema/dezvoltare/testarea resurselor.
- Unicode-regresie cu obiective maxime și 24 nume de resurse.
- Adevărata referință nativă a CPU cu oficial tiny-random-gpt2-fp32-Testgraph de la microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Modelul de testare este completat și tocenizatorul de byte este adaptat la micul grafic aleatoriu. Acest model accidental nu verifică calitatea și nu este în lansare.
- Testul de rulare nativă reproduse: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-Modelordner>.
- WPF-Smoke verifică selecția tuturor celor patru furnizori și starea de testare; selecția singură nu generează o cerere de model.

## Rămâne liberă reală

Testele de schimbare a hardware-ului/smartphone-ului/tabletului/monitorului sunt în continuare necesare. Exclusivă de tipărit rămâne blocată fără un filtru semnat, licențiat și verificat. Nu este înlocuită cu un driver de zgomot sau Raw Input. Schimbarea de focalizare poate fi blocată prin Windows; eroarea numeste acum activarea manuală a ferestrului de destinație. Elementele de bază ale AI au fost adăugate traduceri în toate catalogurile 27; textele noi detaliate și descărcările de traducere sunt încă deschise. Calitatea live cu un model de instrucție adecvat, funcționarea durabilă și câștigul de timp măsurat nu sunt dovedite de testele de model accidental / false HTTP.

## Sursă

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Testarea finală: Construirea de lansare fără avertize/erocuri; completă seturi de bază și Windows, inclusiv fixarea focală nativă, HTTPS, UIA, IPC plug-in și testele de transport AI compatibile. Noul pachet de eliberare20261012-190618: WPF-Smoke a trecut (19:07 oră), conectat ONNX-CPU-a trecut cu test graph. Revizuirea independentă: Unicode-limită de protocol corectat, nu mai multe descoperiri concrete. Nu există nici o pierdere fizică a producătorului/telefonului/monitorului.

## Continuare: modelare reală și modele

Procesor suplimentar cu allama-ai/SmolLM-360M-Instruct-onnx-int4, revizuire 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native infert. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429.

Patru teste reale de planificare (germană/engleză, document, program, schiță explicită de microfon): **0/4 Planuri valabile**; JsonException în cazul unui test de răspuns strict. 63501, 6376, 72118 şi 6774 Acest model mic nu îndeplinește contractul de planificare și nu este recomandat ca fiind potrivit. raport: artifacts/local-ai-evaluation/smollm360-results.json. reproducere: Windows-Testele --evaluate-onnx <ordinar de model> <calea de raportare> Funcția timpului nu dovedește calitatea modelului.

Navigația de instrumente și rapoartele de testare au fost traduse în plus în toate catalogurile de limbă 27. textele lungi și descărcările lingvistice rămân deschise. regresia de șablonă verifică, de asemenea, tipuri false JSON, șablonele goale / multi-deformate / lungi și zero cu Jinja Fallback.

Pachetul actualizat: artifacte/release-20261012-201134 Spray-ul WPF a rămas în 12.10.2026 în jurul 20:12; infertilitatea nativă a CPU-ului a rămas cu modelul de testare separat de instrucțiune în laboratorul combinat. Testele de bază și Windows au trecut; construiți fără avertize/erocuri. Calitatea modelului continuă 0/4.

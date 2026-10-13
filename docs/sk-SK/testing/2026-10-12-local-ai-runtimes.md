<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-12-local-ai-runtimes.md)

# Miestne runtime AI

## servis

Nástroje → Otvorte asistenta stretnutia AI, aktivujte ho explicitne a vyberte AI runtime. Uvoľnite zdroje, sformulujte cieľ, vytvorte koncept a skontrolujte ho v editore. Žiadny poskytovateľ nespúšťa návrh automaticky. Nedochádza k automatickej zmene poskytovateľa.

| Trvanie | Požiadavka | spojenie |
|---|---|---|
| Ollama | lokálne nainštalovaný model | 127.0.0.1:11434 |
| LM Studio | spustený lokálny server s lokálne načítaným modelom a podporou schémy JSON | 127.0.0.1:1234 |
| llama.cpp | spustený lama server s lokálnym modelom a podporou schémy JSON | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | kompatibilný, dôveryhodný lokálny priečinok textového modelu GenAI | samostatný proces, bez HTTP |

Klienti HTTP nepoužívajú proxy a nesledujú presmerovania. Lokálne servery môžu samy adresovať doplnkové služby; Najmä LM Link/Remote Routing sa nesmie použiť na lokálne spracovanie. MAKRIVO to nedokáže spoľahlivo zistiť prostredníctvom kompatibilného rozhrania. Ollama vzdialené metadáta a názvy cloudu sú dodatočne odmietnuté. V tejto verzii nie je možné konfigurovať overené lokálne servery.

## ONNX

V dialógu vyberte genai_config.json. Priečinok musí obsahovať tokenizer.json a šablónu chatu. Podporovaný je chat_template ako reťazec, pomenovaná šablóna default (pole alebo slovník) a samostatný chat_template.jinja, ak vložená šablóna chýba alebo má hodnotu null. Nejednoznačné šablóny sa odmietajú; maximum je 65536 znakov. Ľubovoľné súbory .onnx, vizuálne/embeddingové modely alebo exporty iba pre GPU/NPU nie sú modelmi pre schôdze.

Súčasťou balenia: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Konfigurácia poskytovateľa je resetovaná na CPU. Bez sťahovania a bez modelových závaží v balení. Nevyžaduje sa žiadna inštalácia Pythonu. Používajte iba balíky dôveryhodných modelov; samostatný proces je hranica zlyhania/prerušenia, nie bezpečnostná karanténa.

Limity: 90 sekúnd vrátane načítania modelu, maximálny počet tokenov výzvy 3072 a tokenov generovaných 1024, dodatočný limit kontextu modelu; dôsledne skontroloval JSON a známe ID prostriedkov. Nesprávna/skrátená odpoveď bude vyradená. Zavrieť/Zrušiť ukončí spustený pracovný proces. Žiadne príkazy voľného modelu a žiadne cesty k programom/súborom v samostatnom zozname zdrojov výzvy; samotný cieľový text napísaný používateľom môže obsahovať takéto údaje.

## Overenie

- Základná sada, testy prenosu slučky so simulovanými odpoveďami LM-Studio/llama.cpp/Ollama, kontrola schémy/zrušenia/zdroja.
- Regresný test protokolu Unicode s cieľmi maximálnej dĺžky a 24 názvami prostriedkov.
- Skutočné natívne odvodenie CPU s oficiálnym tiny-random-gpt2-fp32-Testgraph od microsoft/onnxruntime-genai, odovzdajte 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Pridaná testovacia šablóna a bajtový tokenizér prispôsobený malému náhodnému grafu. Tento náhodný model nekontroluje kvalitu a nie je dostupný.
- Reprodukovateľný natívny test spustenia: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI modelový priečinok>.
- WPF-Smoke skontroluje výber všetkých štyroch poskytovateľov a skúšobnú stolicu; Samotný výber negeneruje požiadavku na model.

## Zostávajúce skutočné akcie

Stále sa vyžaduje testovanie výmeny hardvéru/smartfónu/tabletu/monitoru. Bez podpísaného, ​​licencovaného a overeného ovládača filtra zostáva exkluzívne potlačenie klávesnice blokované. Nenahradené figurínami vodičov alebo Raw Input. Zmena zamerania môže byť stále blokovaná pomocou Windows; chyba teraz spomína manuálnu aktiváciu cieľového okna. Preklady boli pridané do centrálnych ovládacích prvkov AI vo všetkých katalógoch 27; Podrobné nové texty a schválenie technického prekladu sú stále otvorené. Živá kvalita s vhodným inštrukčným modelom, nepretržitá prevádzka a nameraná úspora času nie sú dokázané náhodnými modelovými/falošnými HTTP testami.

## Zdroje

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Záverečný test: vydanie zostavy bez varovaní/chýb; Prešiel úplným jadrom a balíkom Windows vrátane natívneho zaostrovacieho prípravku, HTTPS, UIA, plugin IPC a kompatibilných testov prenosu AI. Vydanie nového balíka-20261012-190618: Prešiel dym WPF (hodiny 19:07), pribalený záver procesora ONNX s testovacím grafom prešiel. Nezávislá recenzia: Opravený limit protokolu Unicode, žiadne ďalšie konkrétne zistenia. Z toho nevyplýva žiadny fyzický súhlas výrobcu/telefónu/monitoru.

## Ďalšie: Skutočná kvalita a vzorky

Dodatočný beh na CPU s allama-ai/SmolLM-360M-Instruct-onnx-int4, revízia 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: natívna inferencia úspešná. SHA256 váh modelu: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Komunitný export iba v testovacom priestore, nie vo vydaní.

Štyri skutočné testy plánovania (nemčina/angličtina, dokument, program, výslovné stlmenie mikrofónu): **0/4 platných plánov**; zakaždým JsonException pri prísnej kontrole odpovede. Časy behu 63501, 6376, 72118 a 6774 ms. Žiadne akcie sa nevykonali. Tento malý model nespĺňa kontrakt plánovania a nie je odporúčaný ako vhodný. Správa: artifacts/local-ai-evaluation/smollm360-results.json. Reprodukcia: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Fungovanie runtime prostredia nedokladá kvalitu modelu.

Navigácia nástrojov a testovacia správa boli navyše preložené vo všetkých 27 jazykových katalógoch. Dlhé texty a jazykové schválenie zostávajú otvorené. Regresný test šablón kontroluje aj nesprávne typy JSON, prázdne/nejednoznačné/príliš dlhé šablóny a null s náhradnou šablónou Jinja.

Aktualizovaný balíček: artifacts/release-20261012-201134. WPF-Smoke zabalenej aplikácie prešiel dňa 12.10.2026 o 20:12; natívna inferencia na CPU so samostatným testovacím modelom Instruct v pribalenom workeri prešla. Testy Core a Windows-KI prešli; zostavenie bez varovaní/chýb. Kvalita plánovania modelu naďalej 0/4.

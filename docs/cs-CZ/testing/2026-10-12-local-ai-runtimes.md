<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-12-local-ai-runtimes.md)

# Místní běhové prostředí AI

## Servis

Nástroje → Otevřete asistenta schůzky AI, aktivujte jej explicitně a vyberte AI runtime. Uvolněte zdroje, formulujte cíl, vytvořte koncept a zkontrolujte jej v editoru. Žádný poskytovatel nespouští návrh automaticky. Nedochází k automatické změně poskytovatele.

| Doba trvání | Požadavek | spojení |
|---|---|---|
| Ollama | lokálně instalovaný model | 127.0.0.1:11434 |
| LM Studio | běžící lokální server s lokálně načteným modelem a podporou schématu JSON | 127.0.0.1:1234 |
| llama.cpp | běžící lama server s lokálním modelem a podporou schématu JSON | 127.0.0.1:8080 |
| ONNX Doba běhu GenAI CPU | kompatibilní, důvěryhodná místní složka textového modelu GenAI | samostatný proces, žádný HTTP |

Klienti HTTP nepoužívají proxy a nesledují přesměrování. Lokální servery mohou samy adresovat další služby; Zejména nelze použít LM Link/Remote Routing pro místní zpracování. MAKRIVO to nemůže spolehlivě detekovat prostřednictvím kompatibilního rozhraní. Ollama vzdálená metadata a názvy cloudu jsou navíc odmítnuty. V této verzi nelze konfigurovat ověřené místní servery.

## ONNX

V dialogu genai_config.json vyberte. složka musí obsahovat tokenizer.json a šablonu chat. chat_template je podporován jako znakový řetězec, předvolený vzor (Array nebo slovník) a samostatný chat_template.jinja, pokud neexistuje žádná vnitřní vzor nebo je nulová. víceúčelové šablony jsou odmítnuty; maximálně 65536 Známky. populární soubory .onnx, modely Vision/Embedding nebo čisté GPU/NPU exporty nejsou modely schůzky.

Balení: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Konfigurace poskytovatele je resetována na CPU. Bez stahování a bez modelových závaží v balení. Není nutná žádná instalace Pythonu. Používejte pouze balíčky důvěryhodných modelů; samostatný proces je hranice zhroucení/zrušení, nikoli bezpečnostní karanténa.

Limity: 90 sekund včetně načítání modelu, maximální počet tokenů výzvy 3072 a tokenů generovaných 1024, další limit kontextu modelu; důkladně zkontrolovali JSON a známá ID prostředků. Nesprávná/zkrácená odpověď bude zahozena. Zavřít/Zrušit ukončí spuštěný pracovní proces. Žádné volné modelové příkazy a žádné programy/cesty k souborům v samostatném seznamu zdrojů výzvy; cílový text napsaný uživatelem může sám obsahovat taková data.

## Ověření

- Základní sada, testy přenosu zpětné smyčky se simulovanými odpověďmi LM-Studio/llama.cpp/Ollama, kontrola schématu/zrušení/zdroje.
- Regrese protokolu Unicode s maximálními dlouhými cíli a názvy prostředků 24.
- Skutečné nativní odvození CPU s oficiálním tiny-random-gpt2-fp32-Testgraph od microsoft/onnxruntime-genai, commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Přidána testovací šablona a bajtový tokenizér přizpůsobený malému náhodnému grafu. Tento náhodný model nekontroluje kvalitu a není v prodeji.
- Reprodukovatelný nativní test běhu: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI modelová složka>.
- WPF-Smoke kontroluje výběr všech čtyř poskytovatelů a testovací stolice; Samotný výběr negeneruje požadavek na model.

## Zbývající skutečné akcie

Stále je vyžadováno testování výměny hardwaru/smartphonu/tabletu/monitoru. Bez podepsaného, ​​licencovaného a ověřeného ovladače filtru zůstává výhradní potlačení klávesnice blokováno. Nenahrazeno falešnými řidiči nebo Raw Input. Změna ohniska může být stále blokována pomocí Windows; chyba nyní zmiňuje ruční aktivaci cílového okna. Překlady byly přidány do centrálních ovládacích prvků AI ve všech katalozích 27; Podrobné nové texty a schválení technického překladu jsou stále otevřené. Živá kvalita s vhodným instrukčním modelem, nepřetržitý provoz a naměřená úspora času není prokázána náhodnými modelovými/falešnými HTTP testy.

## Zdroje

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Závěrečný test: vydání sestavení bez varování/chyb; Prošel úplným jádrem a sadou Windows, včetně nativního zaostřovacího přípravku, HTTPS, UIA, plugin IPC a kompatibilních transportních testů AI. Vydání nového balíčku-20261012-190618: WPF kouř prošel (hodiny 19:07), přibalený ONNX CPU inference s testovacím grafem prošel. Nezávislá recenze: Opraven limit protokolu Unicode, žádná další konkrétní zjištění. Z toho nevyplývá žádný fyzický souhlas výrobce/telefonu/monitoru.

## Další: Reálná kvalita a vzorky

Další CPU běh allama-ai/SmolLM-360M-Instruct-onnx-int4Revize 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: Native Inference přežil. Modellgewichte-SHA25648e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429.

Čtyři skutečné testy plánování (němčina/angličtina, dokument, program, výslovné mikrofonové zvukové nastavení): **0/4 Platné plány**JsonException při přísném zkoušení odpovědi. 63501, 6376, 72118 a 6774 MS. Žádné akce nejsou provedeny. Tento malý model nesplňuje plánovací smlouvu a není doporučen jako vhodný. Zpráva: artifacts/local-ai-evaluation/smollm360-results.json. WindowsTestování --evaluate-onnx <modelový řádek> <přesvědčení>. funkce času neprokázala kvalitu modelu.

Navigační nástroje a zkušební zprávy byly navíc ve všech 27 Překlady jazyků. dlouhé texty a jazykové snížení zůstávají otevřené. regresní vzorky také zkoumají falešné typy JSON, prázdné/více dlouhé/překlady a nulové s Jinja Fallback.

Aktualizace: Artifacts / Release20261012-201134• balení WPFSmoke na 12.10.2026 O 20:12 Native CPU reference s odděleným testovacím modelem v propojeném Workeru. Windows-Přešel testy KI; Stavět bez upozornění/chyb. 0/4.

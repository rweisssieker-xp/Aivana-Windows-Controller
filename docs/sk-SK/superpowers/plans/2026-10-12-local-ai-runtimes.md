<!-- generated-doc locale=sk-SK source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokálne AI runtime a zostávajúce kontroly

Cieľ: skutočne pripojiť ďalšie miestne runtime AI, získať recenzie dizajnu a zviditeľniť otvorené vydania. Používateľ povoľuje implementáciu bez otázok. Existujúce zmeny sa zachovajú.

1. Slučkový transport a ONNX predimplementačné testy kontroly paketov; Skontrolujte sieťové adresy, výstupné limity, prerušenie a neznáme zdroje.
2. Zdieľané správy plánovača; Prijaté Ollama, pridané LM Studio (127.0.0.1:1234) a llama.cpp (127.0.0.1:8080). Žiadne cloudové adresy URL ani automatické prepínanie poskytovateľov. Lokálne servery sa môžu posielať ďalej; Za ich prevádzku zodpovedá prevádzkovateľ.
3. ONNX Runtime GenAI CPU vo vlastnom procese s časovým limitom, obmedzeným protokolom JSON a ukončením procesu. Explicitne vyberte priečinok modelu; žiadny model sa nestiahne automaticky. Iba textové modely kompatibilné so GenAI, žiadne ľubovoľné siete .onnx.
4. WPF výber pre runtime, model, priečinok ONNX; všetky poskytujú rovnaký prísne testovaný dizajn.
5. Testovacia stolica s jasnými limitmi pre hardvér, DPI, mobilný panel a exkluzívnu makro podložku. Fyzické kontroly a podpísaný/licencovaný ovládač tu nemožno nahradiť softvérom.
6. Zostava vydania, testovanie jadra/Windows, dym WPF, testovanie pracovníkov/balíkov a nezávislé hodnotenie. Vytvorte nový kompletný balík; Otestujte kvalitu modelu iba vtedy, ak model skutočne existuje.

Výsledok: implementované štyri runtime, ONNX testované natívne samostatne, výber poskytovateľa a testovacia stolica v zbalenom WPF dym prešiel. Core/Windows suity zelené. Skontrolujte, či nájdete limit Unicode so spoločnou konštantou a opravenou regresiou. Skutočné schválenie zmien hardvéru/mobilu/DPI, vydanie ovládača, úplné technické preklady a kvalita živých pokynov zostávajú explicitnou otvorenou prácou; nevydané ako úplné.

Pokračovanie: oddelené Jinja a menované/default vzorky implementované; typ/hraničné/falback regresívne testy. 27 Katalóg doplnený. skutočný model inštrukcie stiahnuť samostatne a SHA256 overené: native CPU referencie funguje, ale 0/4 Plánovanie prípadov spĺňa zmluvu JSON; žiadne akcie vykonávané. nezávislý prehľad: nulový spätný pohľad opravený a testovaný, potom žiadne konkrétne ďalšie nálezy. Windows-KI testy prešli, Release-Build 0 Varovanie / chyby

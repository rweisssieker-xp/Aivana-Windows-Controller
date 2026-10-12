<!-- generated-doc locale=sk-SK source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plán na zlepšenie použiteľnosti ovládača a jeho schopností

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Požiadavka používateľa: zlepšiť vzhľad, použiteľnosť a funkčnosť; predchádzajúca autorizácia vyžaduje maximálny počet užitočných subagentov na potrebných menších modeloch a autonómne dokončovanie.

## Rozsah
Zachovať existujúce profily a fungujúcu integráciu Corsair SDK. Namiesto vytvárania samostatného prototypu vylepšite existujúcu aplikáciu WPF.

- [x] Pracovník prostredia/dashboardu: responzívne rozloženie, aktívna navigácia, čitateľné ovládacie prvky, kompaktná aktivita/stav, usporiadanie dlaždíc presúvaním myšou, užitočné prázdne stavy a šablóny. Vlastniť MainWindow.xaml, MainWindow.xaml.cs a MainWindow.Dashboard.cs.
- [x] Pracovník editora: MainWindow.Editor.cs a voliteľné čiastočné súbory iba pre editor. Explicitné postupné úpravy, uloženie/zahodenie, overenie vložené do textu, vyhradený zoznam makier s pretiahnutím myšou, duplicitné kroky, spoľahlivé zachytenie klávesových skratiek a polia súborov/URL.
- [x] Pracovník Windows: natívna validácia, robustné názvy zariadení a možnosti SDK, ohraničené čakanie modifikátora a bezpečné vstrekovanie; vlastné testy Windows a Windows.
- [x] Nadradený prvok: priebeh vykonávania jadra, rozhrania radiča, uchovanie konfigurácie, testovanie integrácie, aktuálne vykreslené snímky obrazovky, zverejnenie/balík a nahradenie spustenej aplikácie.

## Zmluva o rozhraní
Controller poskytuje existujúce API plus reťazec LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorA), Open(stringColorA), v. Editor odhaľuje bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); existujúce názvy polí/metód zostávajú dostupné v rámci čiastkových tried.

## Prijatie
Žiadny reset konfigurácie skutočného používateľa; platné etapové zmeny uložia raz, neplatné koncepty zostanú upraviteľné, zahodenie obnoví uložené údaje. Natívne aliasy klávesových skratiek overené bez vloženia testovacích vstupov do nesúvisiacich okien. Testované poradie dlaždíc a makier; všetky zobrazenia sa vykreslia vo veľkostiach pracovnej plochy; bežiaci/neúspešný/úplný stav viditeľný; zobrazený skutočný model zariadenia SDK. Zostavenie/testy a zverejnený dymový prechod a spustená aktualizovaná aplikácia.

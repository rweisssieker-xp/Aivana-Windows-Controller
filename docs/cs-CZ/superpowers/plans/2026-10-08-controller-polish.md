<!-- generated-doc locale=cs-CZ source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plán zlepšení použitelnosti a schopností kontroléru

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Požadavek uživatele: zlepšit vzhled, použitelnost a funkčnost; předchozí autorizace vyžaduje maximum užitečných subagentů na nezbytných menších modelech a autonomní kompletaci.

## Rozsah
Zachování stávajících profilů a funkční integrace Corsair SDK. Vylepšete stávající aplikaci WPF místo vytváření samostatného prototypu.

- [x] Pracovník prostředí/dashboard: responzivní rozvržení, aktivní navigace, čitelné ovládací prvky, kompaktní aktivita/stav, uspořádání dlaždic přetažením, užitečné prázdné stavy a šablony. Vlastní MainWindow.xaml, MainWindow.xaml.cs a MainWindow.Dashboard.cs.
- [x] Pracovník editoru: MainWindow.Editor.cs a volitelné dílčí soubory pouze pro editor. Explicitní postupné úpravy, uložení/zahození, vložená validace, vyhrazený seznam maker s možností přetažení, duplicitní kroky, spolehlivé zachycení klávesových zkratek a pole souborů/URL.
- [x] Pracovník Windows: nativní ověřování, robustní názvy zařízení a možnosti SDK, omezené čekání modifikátoru a bezpečné vkládání; vlastní testy Windows a Windows.
- [x] Nadřazený: průběh provádění jádra, rozhraní řadiče, uchování konfigurace, testování integrace, skutečné vykreslené snímky obrazovky, publikování/balíček a nahrazení běžící aplikace.

## Smlouva o rozhraní
Controller poskytuje existující rozhraní API plus řetězec LastStatus, bool HasRunningActions, bool IsRunning(Guid), řetězec GetTileStatus(Guid), řetězec ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorA), Open(stringColorA), v. Editor zpřístupní bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); existující názvy polí/metod zůstávají dostupné napříč dílčími třídami.

## Přijetí
Žádné resetování skutečné uživatelské konfigurace; platné nastudované změny uložit jednou, neplatné koncepty zůstanou upravitelné, zahodit obnoví uložená data. Nativní aliasy klávesových zkratek ověřené bez vkládání testovacích vstupů do nesouvisejících oken. Testováno pořadí dlaždic a maker; všechna zobrazení se vykreslují ve velikostech plochy; viditelný stav běžící/neúspěšný/úplný; zobrazen skutečný model zařízení SDK. Sestavení/testy a zveřejněný kouřový průchod a aktualizovaná aplikace je spuštěna.

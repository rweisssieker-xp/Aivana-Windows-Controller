<!-- generated-doc locale=sk-SK source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Plán implementácie kontroléra Windows

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Pre agentských pracovníkov: použite paralelné izolované vlastníctvo; používateľ výslovne požadoval úplnú implementáciu s maximálnym počtom užitočných subagentov.

**Cieľ:** Dodajte natívny panel klávesových skratiek Windows v schválenom dizajne.
**Architektúra:** Konfigurácia jadra a zrušiteľné vykonávanie; adaptéry Windows; WPF zloženie pracovnej plochy.
**Tech zásobník:** .NET 10, WPF, Win32, JSON. Žiadne platené závislosti.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Globálne obmedzenia
nemecké používateľské rozhranie; miestne úložisko; Windows x64; pravdivý stav hardvérových schopností; žiadne protokolovanie klávesnice; žiadne predvolené automatické spustenie.

## Zameranie na recenziu
Poškodené importy nesmú nahradiť uložené nastavenia; zadržiavané modifikátory nesmú kontaminovať výstup; odpojený hardvér nesmie havarovať; zatváranie musí zachovať dostupnosť zásobníka; zrušené makrá musia uvoľniť zdroje.

## Úlohy a vlastníctvo
- [x] Základný agent: src/Aivana.Jadro a testy/Aivana.Test. Modely, JSON perzistencia, validácia, výber profilu a MacroRunner. Spustite neúspešné behaviorálne testy a potom implementujte a spustite všetky základné testy.
- [x] Windows agent: src/Aivana.Windows. RegisterHotKey, inventár zariadení, SendInput, proces v popredí a voliteľný adaptér osvetlenia Corsair. Overte veľkosti natívnych štruktúr a vyčistenie registrácie pomocou testov bezpečného dymu.
- [x] Agent GUI: src/Aivana.Desktop/MainWindow.xaml a .cs plus iba pomocníci používateľského rozhrania. Tmavý prístrojový panel, úplný editor, profily, zariadenia, nastavenia, prehľad klávesnice a editor makier. Stavať, keď existujú zmluvy.
- [x] Nadradená položka: zdieľané zmluvy, projektové súbory, zloženie DesktopController, životný cyklus aplikácie/zásobníka, dialógové okná importu/exportu, dokumentácia, kontroly integrácie, zverejnenie a test spustenia aplikácie.

## Zdieľané rozhrania
Zmluvy žijú v Core/Models.cs. Implementácia ovládača žije v Desktop/DesktopController.cs. UI volá metódy radiča cez túto konkrétnu triedu. Agenti pri svojich úlohách dostávajú presné podpisy. Každý vlastní samostatné súbory. Rodič rieši zlyhania medzi projektmi po dokončení všetkých agentov.

## Overenie a doručenie
- [x] dotnet build Aivana.slnx -c Vydanie: nulové chyby.
- [x] dotnet run --project tests/Aivana.Tests -c Release: Behaviorálne kontroly prejdú.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --samostatné true -o artefakty/win-x64.
- [x] Spustite zverejnený spustiteľný súbor, skontrolujte používateľské rozhranie Windows a stabilitu procesu, vykonajte bezpečný tok profilu/úpravy/pretrvávania.
- [x] Napíšte README s pokynmi na zostavenie/spustenie a hardvérovými obmedzeniami; zaznamenávať kontroly a zostávajúce testy iba hardvéru.

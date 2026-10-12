<!-- generated-doc locale=cs-CZ source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Plán implementace regulátoru Windows

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Pro agentní pracovníky: použijte paralelní izolované vlastnictví; uživatel výslovně požadoval kompletní implementaci s maximálním množstvím užitečných subagentů.

**Cíl:** Dodejte nativní panel klávesových zkratek Windows ve schváleném designu.
**Aarchitektura:** Konfigurace jádra a zrušitelné provedení; adaptéry Windows; WPF složení pracovní plochy.
**Tech Zásobník:** .NET 10, WPF, Win32, JSON. Žádné placené závislosti.
**Sspec:** ../specs/2026-10-08-windows-controller-design.md

## Globální omezení
německé uživatelské rozhraní; místní úložiště; Windows x64; pravdivý stav hardwarových schopností; žádné protokolování klávesnice; žádný výchozí automatický start.

## Zaměření na recenzi
Poškozené importy nesmí nahrazovat uložená nastavení; zadržené modifikátory nesmí kontaminovat výstup; odpojený hardware nesmí havarovat; uzavření musí zachovat dostupnost zásobníku; zrušená makra musí uvolnit zdroje.

## Úkoly a vlastnictví
- [x] Základní agent: src/Aivana.Jádro a testy/Aivana.Test. Modely, JSON persistence, validace, výběr profilu a MacroRunner. Spusťte neúspěšné behaviorální testy a poté implementujte a spusťte všechny základní testy.
- [x] Windows agent: src/Aivana.Windows. RegisterHotKey, inventář zařízení, SendInput, proces na popředí a volitelný adaptér osvětlení Corsair. Ověřte velikosti nativních struktur a vyčištění registrace pomocí testů bezpečného kouře.
- [x] Agent GUI: src/Aivana.Desktop/MainWindow.xaml a .cs plus pouze pomocníci uživatelského rozhraní. Tmavý řídicí panel, úplný editor, profily, zařízení, nastavení, přehled klávesnice a editor maker. Stavět, jakmile existují smlouvy.
- [x] Nadřazená položka: sdílené smlouvy, soubory projektu, složení DesktopControlleru, životní cyklus aplikace/zásobníku, dialogová okna importu/exportu, dokumentace, kontroly integrace, publikování a kouřový test spuštěných aplikací.

## Sdílená rozhraní
Smlouvy fungují v Core/Models.cs. Implementace ovladače žije v Desktop/DesktopController.cs. UI volá metody řadiče prostřednictvím této konkrétní třídy. Agenti dostávají ve svých úkolech přesné podpisy. Každý vlastní samostatné soubory. Rodič řeší selhání mezi projekty po dokončení všech agentů.

## Ověření a doručení
- [x] dotnet build Aivana.slnx -c Release: žádné chyby.
- [x] dotnet run --project tests/Aivana.Tests -c Release: Behaviorální kontroly prošly.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --samostatné true -o artefakty/win-x64.
- [x] Spusťte publikovaný spustitelný soubor, zkontrolujte uživatelské rozhraní Windows a stabilitu procesu, proveďte bezpečný tok profilů/úprav/perzistence.
- [x] Napište README s instrukcemi pro sestavení/spuštění a hardwarovými omezeními; zaznamenat kontroly a zbývající testy pouze hardwaru.

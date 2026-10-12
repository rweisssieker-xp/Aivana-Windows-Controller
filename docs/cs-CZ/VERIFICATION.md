<!-- generated-doc locale=cs-CZ source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../VERIFICATION.md)

# Zkouška – 8. října 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Aplikace byla postavena na Windows x64 se stabilním .NET SDK 10.0.202 a byla vydána jako samostatná Windows-x64-Ausgabe.

## Kontrolováno automaticky

- Celkové sestavení: žádná varování a žádné chyby.
- Testy základního chování 19: Výchozí konfigurace, ověření, nulová/duplicitní data, priorita profilu, import/export a obnovení zálohy, pořadí maker a opakování, přerušení, dvojité spouštění, serializace, snímek neměnného provedení a selhání kroku.
- Windows kontrola: normalizace klávesových zkratek a aliasů, nativní velikosti struktur, seznam skutečných klávesnicových zařízení, bezpečná registrace a opětovné sdílení, syntetické WM_HOTKEY pro dlaždice a zrušení, deaktivovaná registrace dlaždic během simulace vstupu.
- Samostatný EXE byl spuštěn pomocí `--smoke-test`: všech pět pohledů WPF je vykresleno, dlaždice lze vytvářet/kopírovat/mazat, koncepty jsou zachovány během aktualizace, konfigurace je uložena a znovu načtena. Tento test používá izolovaný dočasný konfigurační adresář a neprovádí žádné textové nebo mediální akce.
- Dashboard, editor a kompaktní reprezentace (900 × 720) byly vizuálně zkontrolovány jako skutečné rendery WPF. Kontroluje se ukládání konceptu, zahození, pořadí dlaždic/makra, německé aliasy klávesových zkratek, zrušení jednotlivých maker a postup kroků.

## Limity závislé na hardwaru

Poslední místní běh detekoval čtyři rozhraní klávesnice Windows. Toto číslo popisuje rozhraní HID/Windows, ne nutně čtyři fyzické klávesnice.

Oficiální iCUE SDK je nainstalováno a je umístěno vedle EXE. Živý test se připojí k iCUE a detekuje klávesnici Corsair s informacemi o modelu a LED. Změny osvětlení nebyly spouštěny automaticky; Speciální klávesy závislé na modelu a další výrobci nejsou hardwarově testovány. Obecné klávesové zkratky platí pro všechny klávesnice.

Vstup textu/médií nebyl vložen do jiných otevřených uživatelských agentů. Byly zkontrolovány nativní datové struktury a logika chyb/uvolnění; Chování v konkrétním cílovém programu závisí mimo jiné na úrovni jeho oprávnění.

## Opakujte

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Test GUI zapíše výsledek a snímek obrazovky do `smoke` vedle testovaného EXE. Je určen jako diagnostický režim a po dokončení nespouští trvalou instanci na pozadí.


## Pokročilé ovládání

Dodatečně zkontrolovány: podmínky profilu s procesem a názvem okna, nahrazení proměnných bez rekurzivní expanze, náhled maker bez provedení, ověření nové akce a zpětná kompatibilita starých souborů JSON. Nativní kontroly pokrývají paletu příkazů s registrací, konflikty, odesláním a uvolněním WM_HOTKEY, stejně jako čisté rozdělení monitoru a přístup pro čtení ze schránky na vyžádání. Test WPF otevře a vykreslí skutečnou paletu, zkontroluje zpět/znovu a oblíbené položky, zkontroluje čisté koncepty po vykreslení a proces úspěšně dokončí.

Zamykání Windows, změny schránky a pohyb oken nebyly během testování aplikovány na uživatelské agenty. Akce po okně Windows požadavky asynchronně; Specifický cílový program může mít své vlastní minimální velikosti nebo limity oprávnění. Statistiky uzamčení profilu a provádění se vztahují na aktuální relaci.


Rozšiřující zkouška ze 12.10.2026: složila základní zkoušky 30; Adaptér Windows včetně uvolnění během vstupní injekce, detekce monitoru pouze pro čtení a simulace stavu OBS prošel. WPF kontroluje potvrzení/zrušení toku, spuštění/zastavení záznamníku, náhled výměny profilu, gesta a prostředky symbolů. Lokalizace: Katalogy 27 každý s klíči 967; Dokumentace: Stránky 84 ve čtyřech jazycích. Proprietární tlačítka jiná než Corsair nebyla testována na hardwaru; Obecná podpora zahrnuje standardní události specifické pro zařízení F13–F24 a mediální klíče. Cloudový přenos probíhá prostřednictvím vybrané synchronizované složky; Importy jsou zkontrolovány a zůstávají zakázány.

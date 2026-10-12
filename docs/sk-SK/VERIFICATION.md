<!-- generated-doc locale=sk-SK source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../VERIFICATION.md)

# Skúška – 8. október 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Aplikácia bola postavená na Windows x64 so stabilným .NET SDK 10.0.202 a vydaná ako samostatná Windows-x64-Ausgabe.

## Skontrolované automaticky

- Celková zostava: žiadne varovania a žiadne chyby.
- Testy základného správania 19: predvolená konfigurácia, overenie, nulové/duplicitné údaje, priorita profilu, import/export a obnovenie zálohy, poradie makra a opakovanie, prerušenie, dvojité spustenie, serializácia, snímka nemenného vykonania a zlyhanie kroku.
- Kontrola Windows: normalizácia klávesových skratiek a aliasy, natívne veľkosti štruktúry, zoznam skutočných klávesnicových zariadení, bezpečná registrácia a opätovné zdieľanie, syntetický WM_HOTKEY pre dlaždice a zrušenie, zakázaná registrácia dlaždíc počas simulácie vstupu.
- Samostatný EXE bol spustený pomocou `--smoke-test`: všetkých päť zobrazení WPF je vykreslených, dlaždice je možné vytvárať/kopírovať/vymazávať, koncepty sa uchovávajú počas obnovy, konfigurácia sa uloží a znova načíta. Tento test používa izolovaný dočasný konfiguračný adresár a nevykonáva žiadne textové ani mediálne akcie.
- Dashboard, editor a kompaktná reprezentácia (900 × 720) boli vizuálne skontrolované ako skutočné vykresľovanie WPF. Kontroluje sa ukladanie konceptu, zahodenie, poradie dlaždíc/makro, nemecké aliasy klávesových skratiek, zrušenie jednotlivých makier a postup krokov.

## Limity závislé od hardvéru

Posledný lokálny chod zistil štyri rozhrania klávesnice Windows. Toto číslo popisuje rozhrania HID/Windows, nie nevyhnutne štyri fyzické klávesnice.

Oficiálne iCUE SDK je nainštalované a nachádza sa vedľa EXE. Živý test sa pripojí k iCUE a deteguje klávesnicu Corsair s informáciami o modeli a LED. Zmeny osvetlenia sa nespúšťali automaticky; Špeciálne klávesy závislé od modelu a od iných výrobcov nie sú hardvérovo testované. Všeobecné klávesové skratky sa vzťahujú na všetky klávesnice.

Vstup textu/médií nebol vložený do iných otvorených používateľských agentov. Boli skontrolované natívne dátové štruktúry a logika chyby/uvoľnenia; Správanie v konkrétnom cieľovom programe závisí okrem iného od úrovne jeho autorizácie.

## Opakujte

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Test GUI zapíše výsledok a snímku obrazovky do `smoke` vedľa testovaného EXE. Je určený ako diagnostický režim a po dokončení nespustí trvalú inštanciu na pozadí.


## Pokročilé ovládacie prvky

Dodatočne skontrolované: podmienky profilu s názvom procesu a okna, nahradenie premennej bez rekurzívneho rozširovania, náhľad makra bez vykonania, overenie novej akcie a spätná kompatibilita starých súborov JSON. Natívne kontroly pokrývajú paletu príkazov s registráciou, konfliktmi, odoslaním a uvoľnením WM_HOTKEY, ako aj čistým rozdelením monitora a prístupom na čítanie zo schránky na požiadanie. Test WPF otvorí a vykreslí aktuálnu paletu, skontroluje vrátenie/znovu a obľúbené položky, skontroluje čisté koncepty po vykreslení a úspešne dokončí proces.

Uzamknutie Windows, zmeny schránky a pohyb okien neboli počas testovania aplikované na používateľských agentov. Akcie po okne Windows požaduje asynchrónne; Špecifický cieľový program môže mať svoje vlastné minimálne veľkosti alebo autorizačné limity. Štatistiky uzamknutia profilu a spustenia sa vzťahujú na aktuálnu reláciu.


Rozširujúca skúška zo 12.10.2026: absolvované základné skúšky 30; Adaptér Windows vrátane uvoľnenia počas vstupného vstrekovania, detekcie monitora iba na čítanie a simulácie stavu OBS prešiel. WPF kontroluje potvrdenie/zrušenie toku, spustenie/zastavenie záznamníka, náhľad výmeny profilu, gestá a prostriedky symbolov. Lokalizácia: katalógy 27 každý s kľúčmi 967; Dokumentácia: strany 84 v štyroch jazykoch. Iné proprietárne tlačidlá ako Corsair neboli testované na hardvéri; Všeobecná podpora zahŕňa štandardné udalosti špecifické pre zariadenie F13–F24 a mediálne kľúče. Prenos v cloude prebieha prostredníctvom vybratého synchronizovaného priečinka; Importy sa skontrolujú a zostanú zakázané.

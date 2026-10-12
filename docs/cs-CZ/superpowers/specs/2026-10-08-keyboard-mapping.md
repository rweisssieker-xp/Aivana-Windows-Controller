<!-- generated-doc locale=cs-CZ source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Zobrazení fyzické klávesnice pro mapování

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cíl

Zobrazení klávesnice je určeno k zobrazení skutečné polohy kláves při přiřazování akcí. Řádky stejné šířky nestačí: Enter, Shift, Space, Navigation, numerická klávesnice a další ovládací prvky výrobce se liší.

## Implementace

- Katalog s oddělenými fyzickými ID, geometrií v klíčových jednotkách, tištěnými legendami a vstupními kódy Windows.
- ISO-DE a ANSI-US každý jako plný formát a TKL; navíc Corsair K100 RGB, který byl rozpoznán jako připojený model přes iCUE.
- ISO Enter s tvarem L, numerická klávesnice s vysokými klávesami plus/enter a širokou nulou; Navigace a šipky ve vlastních pozicích.
- Explicitně vyberte model/rozvržení. USB VID/PID a název modelu nedokazují jazykovou variantu. Obecné profily a profily modelované na základě dokumentů výrobce jsou jasně označeny odlišně; žádná milimetrová stupnice ani záruka kompatibility firmwaru.
- Mapujte skenovací kód na aktivní rozložení vstupu Windows, aby fyzické kliknutí na klávesu náhodně neobsadilo jinou klávesu, například Y/Z při přepínání mezi USA a DE.
- Úkol otevře koncept ve stávajícím editoru akcí. Ukládání, kontrola konfliktů a vyřazování zůstávají stávajícími mechanismy. Kliknutí nespouští akce.
- Jsou zde zastoupeny proprietární klávesy G-key, profilové/uzamykací klávesy a kolečko iCUE, ale nemají výstup jako normální klávesové zkratky Windows bez implementovaného vstupního adaptéru. Možnou cestou zůstávají konfigurovatelné zkratky výrobce.
- Přiřazení zůstávají globálními klávesovými zkratkami Windows; zobrazení zařízení neznamená potlačení specifické pro zařízení nebo výhradní přiřazení jediné fyzické klávesnice.

## Důkazy

[K100-Handbuch s ovládacími prvky](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO prohlášení](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Zkouška

testování geometrie, variant a vstupního kódu; Sestavy a stávající testy jádra/Windows; WPF kouř pro zobrazení a mapování bez provádění nebo změny skutečné uživatelské konfigurace. Výzkum výrobce dokumentuje další požadavky na model/SKU nezávisle na aktuálně implementovaném katalogu rozvržení.

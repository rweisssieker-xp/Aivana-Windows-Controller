<!-- generated-doc locale=sk-SK source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Zobrazenie fyzickej klávesnice na mapovanie

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cieľ

Zobrazenie klávesnice je určené na zobrazenie skutočných pozícií kláves pri priraďovaní akcií. Riadky rovnakej šírky nestačia: líšia sa Enter, Shift, Space, Navigácia, numerická klávesnica a ďalšie ovládacie prvky výrobcu.

## Implementácia

- Katalóg s oddelenými fyzickými ID, geometriou v kľúčových jednotkách, tlačenými legendami a vstupnými kódmi Windows.
- ISO-DE a ANSI-US každý ako plný formát a TKL; dodatočne Corsair K100 RGB, ktorý bol rozpoznaný ako pripojený model cez iCUE.
- ISO Enter s tvarom L, numerická klávesnica s vysokými klávesmi plus/enter a širokou nulou; Navigácia a šípky vo vlastných pozíciách.
- Explicitne vyberte model/rozloženie. USB VID/PID a názov modelu nesvedčia o jazykovej variante. Všeobecné profily a profily modelované na základe dokumentov výrobcu sú jasne označené odlišne; žiadna milimetrová stupnica ani záruka kompatibility firmvéru.
- Namapujte skenovací kód na aktívne rozloženie vstupu Windows, aby fyzické kliknutie na kláves náhodne neobsadilo iný kláves, napríklad Y/Z pri prepínaní medzi USA a DE.
- Zadanie otvorí koncept v existujúcom editore akcií. Ukladanie, kontrola konfliktov a vyhadzovanie zostávajú existujúcimi mechanizmami. Kliknutia nespúšťajú akcie.
- Sú zastúpené proprietárne G-klávesy, profilové/uzamykacie klávesy a koliesko iCUE, ale nie sú na výstupe ako bežné klávesové skratky Windows bez implementovaného vstupného adaptéra. Možnou cestou zostávajú konfigurovateľné skratky výrobcu.
- Priradenia zostávajú globálne klávesové skratky Windows; zobrazenie zariadenia neznamená potlačenie špecifické pre zariadenie alebo výhradné priradenie jedinej fyzickej klávesnice.

## Dôkazy

[K100-Handbuch s ovládacími prvkami](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI/ISO vyhlásenie](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Skúška

testovanie geometrie, variantu a vstupného kódu; Zostavy a existujúce testy jadra/Windows; WPF dym pre zobrazenie a mapovanie bez vykonania alebo zmeny konfigurácie skutočného používateľa. Prieskum výrobcu dokumentuje dodatočné požiadavky na model/SKU nezávisle od aktuálne implementovaného katalógu rozloženia.

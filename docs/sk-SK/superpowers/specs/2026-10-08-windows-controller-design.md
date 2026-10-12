<!-- generated-doc locale=sk-SK source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Návrh

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stav: 8. október 2026. Stav: na kontrolu pred implementáciou.

## Cieľ a dohodnutý rozsah

Samostatná aplikácia Windows premení normálnu PC klávesnicu na voľne konfigurovateľný akčný panel podobný stream decku. Používateľ potvrdil všetky navrhované typy akcií, makrá, detekciu zariadenia, profily a dobré GUI. Pripojené Corsair a ďalšie klávesnice sú rozpoznané; dodatočné funkcie výrobcu sú ponúkané len vtedy, ak sú skutočne podporované.

Predpoklady: nemecké rozhranie, lokálne použitie bez účtu, Windows 11 x64 ako prvý cieľ, žiadna inštalácia ovládača. Klávesnica zostáva použiteľná na bežné písanie. Globálne mapovania štandardne používajú klávesové skratky s modifikátormi. Jednotlivé klávesy písmen nie sú štandardne globálne rezervované.

## Technické rozhodnutie

Natívne C#/.NET-10-App so WPF a MVVM. V pracovnom priestore je k dispozícii stajňa .NET-10-SDK. WPF umožňuje prispôsobené rozhranie pracovnej plochy a priamu integráciu Windows. Alternatívou by bolo webové rozhranie so samostatnou službou Windows alebo multiplatformová desktopová aplikácia; oba prinášajú dodatočnú časovú a komunikačnú zložitosť tomuto projektu Windows. Natívny variant je teda zvoleným základom.

Divízia: Desktopový projekt pre pohľady a zásobník, hlavný projekt pre logiku konfigurácie a vykonávania, projekt Windows pre klávesové skratky, vstupnú simuláciu a zariadenia, testovací projekt pre pravidlá nezávislé od platformy. Adaptéry výrobcu závisia od rozhrania s pevnou schopnosťou a sú nezávislé od štandardných akcií.

## povrch

Tmavý dizajn s pokojnými antracitovými povrchmi, ľahko čitateľným písmom a tyrkysovými akcentmi. Prepája navigáciu pre dashboard, profily, zariadenia a nastavenia. Aktívny profil, vyhľadávanie a prepnutie na pozastavenie všetkých klávesových skratiek v hornej časti. Rozhranie zostáva použiteľné pri škálovaní 100–200 percent Windows.

Prístrojová doska obsahuje prispôsobiteľnú mriežku dlaždíc. Každá dlaždica zobrazuje ikonu, názov, klávesovú skratku a stav vykonávania. Kliknutie vykoná rovnakú akciu ako klávesová skratka. Vpravo sa otvorí editor s typom akcie, parametrami, záznamom klávesovej skratky, symbolom/farbou, aktivačným a testovacím tlačidlom. K dispozícii sú Nové, Duplikovať, Odstrániť a Zmeniť poradie. Zobrazenie klávesnice schematicky zobrazuje priradenia pre štandardné rozloženie v nemčine; nenárokuje si presnú geometriu detekovaného zariadenia.

Profily je možné vytvárať, kopírovať, premenovať a aktivovať manuálne. Názov procesu je voliteľne priradený k profilu. Vhodný program v popredí aktivuje jeho profil, inak platí globálny štandardný profil. Ak existuje niekoľko zodpovedajúcich profilov, platí výslovne viditeľné poradie. Editor vysvetľuje konflikty klávesových skratiek priamo na zadaní.

## Klávesové skratky a vstupy

Windows RegisterHotKey zvláda bežné globálne kombinácie s potlačeným opakovaním klávesov. Zaregistruje sa iba aktívny profil. Rezervované alebo už obsadené kombinácie sa objavia ako chyby; aplikácia nehlási, že bola úspešne aktivovaná. Nahrávanie v editore nespúšťa žiadne akcie.

Pred simulovanými vstupmi sa uvoľnia modifikačné klávesy spúšťania alebo sa čaká na ich fyzické uvoľnenie. Unicode text a kombinácie klávesov sa odosielajú pomocou SendInput. Každý kláves, ktorý stlačíte, sa opäť uvoľní, aj keď ho zrušíte. Akcie prebiehajú mimo vlákna používateľského rozhrania; Zmeny vyhotovenia a profilu nesmú blokovať povrch.

## Akcie a makrá

Typy akcií: Spustenie programu s argumentmi a pracovným adresárom, otvorenie súboru/priečinku cez Windows, otvorenie webovej stránky HTTP/HTTPS, zadanie textu Unicode, odoslanie kombinácie klávesov, prehrávanie médií/pozastavenie/nasledujúce/predchádzajúce/zastavenie, zvýšenie/zníženie/stlmenie a prepnutie profilu. Parametre sú overené pred uložením a znova pred spustením. Programy sa spúšťajú priamo; Argumenty nie sú implicitne interpretované ako príkazy shellu.

Makrá obsahujú usporiadaný zoznam pomenovaných akcií a explicitné kroky pozastavenia. Kroky je možné pridávať, upravovať a presúvať. Opakovania sú konečne konfigurovateľné (1–100); Nekonečné slučky nie sú súčasťou prvej verzie. Spustené makro je možné ukončiť cez rozhranie alebo vlastnú klávesovú skratku zrušenia. Opakované spustenie rovnakého priradenia prúdu nespustí paralelný chod. Rôzne makrá so simuláciou vstupu sú serializované, aby sa ich stlačenia kláves nemiešali. Ak sa vyskytne chyba kroku, makro sa zastaví a zobrazí ovplyvnený krok.

## Zariadenia a výrobcovia

Inventarizácia zariadení prostredníctvom Windows Raw Input a ďalšie informácie o zariadení: zobrazovaný názov, cesta k zariadeniu, identifikátor výrobcu/produktu, ak je k dispozícii, stav pripojenia a zistené možnosti. Pripojenie/odpojenie aktualizuje zoznam bez reštartovania. Fyzické zariadenie môže mať viacero záznamov HID; Tieto sú zoskupené podľa spoľahlivých identifikátorov, inak sú viditeľne vedené ako samostatné záznamy.

Normálne klávesové skratky sa vzťahujú na všetky klávesnice. Zobrazenie zariadenia poskytuje informácie a možnosti; samostatná klávesnica s výlučne zachytenými klávesmi nie je súčasťou tohto dohodnutého režimu.

Detekcia Corsair funguje aj bez iCUE. Voliteľný adaptér SDK iCUE poskytuje stav pripojenia a podporované činnosti osvetlenia, keď sú prítomné SDK, iCUE a kompatibilné zariadenie. Používateľské rozhranie rozlišuje rozpoznané, dostupné SDK, pripojené a skutočne ovládateľné. SDK primárne dokumentuje ovládanie LED; Proprietárne G-klávesy sú podporované ako normálne klávesové skratky len vtedy, ak Windows dostane príslušný vstup. Neexistuje žiadny všeobecný prísľub pre všetky špeciálne kľúče.

Ostatní výrobcovia spočiatku dostávajú všeobecnú detekciu a štandardné akcie. Rozhranie adaptéra umožňuje neskoršie integrácie; nepodporované zručnosti sú čestne zobrazené. Hardvérové ​​testy pre neexistujúce modely nie sú hlásené ako úspešné.

## Skladovanie a každodenný život

Verzia konfigurácie JSON v adresári údajov lokálnej aplikácie. Atómové uloženie s poslednou platnou zálohou; nesprávne súbory nie sú ticho prepísané. Import skontroluje formát, verziu, ID a parametre a pred použitím zobrazí náhľad. Importované akcie je možné vykonať až po explicitnej aktivácii. Export obsahuje profily a priradenia.

Ponuka zásobníka: Otvoriť, vybrať profil, pozastaviť klávesové skratky, zrušiť makrá, ukončiť. Zatvoriť okno sa v predvolenom nastavení minimalizuje na zásobník a vysvetľuje to prvýkrát. Autostart je voliteľný a predvolene vypnutý. Iba jedna inštancia spravuje klávesové skratky. Exit registruje klávesové skratky, preruší vykonanie a uvoľní zdroje.

Zobrazenie miestnej aktivity s obmedzenou históriou úspechu, zlyhania a zrušenia. Žiadny záznam všetkých stlačení klávesov. Obsah textovej akcie nie je uvedený v chybových správach. Žiadna cloudová služba a žiadna telemetria.

## Spracovanie chýb a limity

Chýbajúce programy, neplatné adresy URL, obsadené klávesové skratky, poškodené profily a nedostupná súprava SDK od výrobcu dostávajú špecifické nemecké správy. Porucha adaptéra neovplyvní štandardné akcie. Vstupy do aplikácií s vyšším oprávnením môžu zlyhať kvôli limitom povolení Windows; Chyby sú vysvetlené, aplikácia štandardne nevyžaduje práva správcu.

## Testovanie a prijímanie

Automatické kontroly priority profilu, detekcia konfliktov, import konfigurácie, atómové úložisko, zoradenie makier, opakovanie, zrušenie a šírenie chýb. Windows dymové testy pre skutočné klávesové skratky, textové a klávesové skratky, mediálne akcie, inventár zariadení, zásobník a čistý výstup. Žiadne testy, ktoré náhodne spúšťajú ľubovoľné programy alebo webové stránky.

Kontrola GUI: Vytvorenie a úprava priradenia, nahrávanie bez vykonania, kliknutie/klávesová skratka s rovnakým efektom, zrušenie makra, správa o konflikte, zmena profilu, zobrazenie zariadenia, import/export a škálovanie. Testovanie hardvéru špecifické pre výrobcu je zdokumentované samostatne so skutočným zariadením.

Dodávka: Zdrojový kód, reprodukovateľné pokyny na zostavenie/spustenie, nemecká používateľská príručka a miestne Windows-x64-Publish-Artefakt. Úspech znamená spustiteľnú aplikáciu so všetkými všeobecnými funkciami; Možnosti Corsair sú označené ako dostupné len s overenou podporou SDK/zariadení.

## Technické zdroje

- Microsoft Klávesnicový vstup / Register HotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Vstup: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Oficiálne Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Ďalší krok

Po preštudovaní tohto návrhu: vytvorte konkrétny plán implementácie a predložte ho na výber implementácie. V prázdnom pracovnom priečinku zatiaľ nie je žiadne úložisko Git; Tento dokument preto ešte nebol zaviazaný.

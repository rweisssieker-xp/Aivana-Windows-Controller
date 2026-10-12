<!-- generated-doc locale=sk-SK source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Skontrolujte, či ide o komerčný jednorazový predaj

Stav: 12. október 2026. Výrobca: Aivana.

## Výsledok

V kontrolovanom inventári nebola zistená žiadna všeobecná povinnosť zverejňovať vlastný zdrojový kód MAKRIVO. Vlastný jednorazový predaj sa javí ako možný, ale ešte nie je úplne pripravený. Najmä nie je zdokumentované povolenie na redistribúciu pre existujúcu Corsair-SDK-DLL. Toto preskúmanie nenahrádza zákonné schválenie.

## Technické a licenčné zistenia

| komponent | Zistenia | Požadovaný krok |
| --- | --- | --- |
| Vlastný zdrojový kód | V preskúmanom inventári Git a jeho lokálne dostupnej histórii licenčných súborov sa nenašla žiadna vlastná LICENCIA, KOPÍROVANIE ani OZNÁMENIE. | Potvrdiť vlastníctvo práv vrátane príspevkov, prijatých fragmentov kódu a možných práv zamestnávateľa; vytvorte si vlastné podmienky používania. |
| .NET/Windows Desktop | Skúmaný samostatný balík pod `artifacts/makrivo-win-x64` obsahuje verziu 10.0.6. Lokálne runtime balíky obsahujú licencie MIT, ktoré umožňujú komerčnú distribúciu a vyžadujú, aby boli zahrnuté upozornenia. V skúmaných výstupných priečinkoch sa nenašli žiadne zodpovedajúce súbory licencií/upozornení. | Zahrňte presné licenčné texty a príslušné informácie tretích strán pre runtime skutočne dodané v predajnom balíku. |
| Závislosti NuGet | V skúmaných projektových súboroch sa nenašli žiadne priame položky PackageReference. Súbor aktív pracovnej plochy, ktorý sa skúma, obsahuje svoje vlastné závislosti projektu. Runtime balíky stále zostávajú relevantné pre licenciu. | Po konečnom vydaní znovu inventarujte všetky projekty, prechodné balíky a pribalené súbory. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, verzia súboru 4.0.84.0. Lokálny referenčný súbor SDK vo formáte PDF obsahuje na vytlačených stránkach 35–38 EULA: používanie bez licenčných poplatkov, ale obmedzenia prevodu/postúpenia. Neexistovalo žiadne jasné povolenie na odovzdanie knižnice DLL zákazníkom. | Pred združením získajte spoľahlivé povolenie na redistribúciu. Prípadne skontrolujte balík bez knižnice DLL SDK, ktorý používa súpravu SDK nainštalovanú zákazníkom; Na to sa vzťahujú aj SDK podmienky. |
| Rôzne balíčky | `artifacts/makrivo-win-x64` neobsahuje Corsair DLL; Obsahuje ich `artifacts/win-x64`. Aplikácia dynamicky hľadá SDK na viacerých miestach. | Nastavte skutočný maloobchodný inštalačný program a skontrolujte jeho obsah. Samotná existencia balíka bez DLL nepotvrdzuje všetky integračné práva. |
| OBS | Vlastný klient WebSocket v skúmanom kóde C#; nebol identifikovaný žiadny klientsky balík OBS, binárny kód OBS alebo prijatý kód GPL. | Pozorovaná integrácia protokolu sama osebe neoprávňuje k žiadnej ustanovenej povinnosti GPL zverejňovania. Pôvod akýchkoľvek fragmentov, ktoré mohli byť adoptované, je potrebné potvrdiť. |
| Ikony aplikácií | Lokálny skript Python kreslí geometrické symboly pomocou Pillow. Nástroj je závislý na zostavovaní. | Zdokumentujte pôvod a práva všetkých skutočne dodaných obrázkov, zvukov, fontov a iných aktív. Samotný scenár nedokazuje práva ku každému dielu. |
| Správa licencií | V kóde skúmanej aplikácie nebola identifikovaná žiadna úplná kontrola nákupu/aktivácie. | V prípade potreby implementujte podpísané offline licenčné súbory. Technická aktivácia a právne podmienky používania sú samostatné úlohy. |

## Rozumný model nákupu – zatiaľ neimplementovaný

- jednorazová platba; Trvalé právo na používanie zakúpenej hlavnej verzie.
- Jedna osoba až na dvoch vlastných počítačoch; Výslovne umožňujú profesionálne použitie.
- Zahrňte opravy chýb a menšie aktualizácie; Voliteľne ponúknite neskoršie hlavné verzie za poplatok.
- Digitálne podpísaný licenčný súbor, ktorý je možné skontrolovať offline; S aplikáciou nikdy nedoručujte súkromné ​​podpisové kľúče.
- Zákonné práva a požadované aktualizácie zabezpečenia nemôžu byť všeobecne vylúčené pravidlom hlavnej verzie.

## Otvorené pred prvým predajom

1. Corsair Ujasnite si distribučné práva alebo sa pripravte na predaj bez pribalenej knižnice SDK.
2. Zahrňte licenciu a texty tretích strán pre presné závislosti vydania a získajte ich v inštalačnom programe.
3. Potvrďte vlastníctvo práv na svoj vlastný kód a aktíva; Vytvorte si EULA s jasnými pravidlami pre zariadenie, používateľa a upgrade.
4. Pri spotrebiteľskom predaji spravujte platbu vrátane požiadaviek na informácie a zrušenia. Všeobecné vyhlásenie „žiadne náhrady za sťahovanie“ nestačí. Zánik práva na odstúpenie od zmluvy v prípade okamžitého sprístupnenia tovaru si vyžaduje zákonné požiadavky.
5. Znova skontrolujte dokončený inštalačný program vrátane doplnkov a všetkých prechodných závislostí. Pracovná kópia sa rozširuje paralelne; Tento inventár nie je vydaním budúcich súborov.

## GitHub

Preskúmané úložisko je verejné. Bez licencie s otvoreným zdrojovým kódom sa kód automaticky nestane voľným pre ďalšie použitie. Stále je potrebné dodržiavať práva platformy GitHub pre verejné úložiská. Neskoršie prepnutie na súkromné ​​neodstráni existujúce kópie alebo rozvetvenia. Počas tejto kontroly sa nezmenila viditeľnosť ani licencia.

## Dôkazy a zdroje

- Lokálne licencie .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` a `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Lokálne .NET oznámenia tretích strán: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Lokálne Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; extrahované referenčné PDF na `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT licencia](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentácia s EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licenčné repozície](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Aktualizácie](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – právo na zrušenie](https://www.gesetze-im-internet.de/bgb/__356.html)

Nekontrolované: úplná história externých príspevkov a reťazce práv, uvoľnenie ochrannej známky, existujúca predajná pokladňa, budúce balíčky doplnkov, plná právna účinnosť zatiaľ nedostupnej EULA.

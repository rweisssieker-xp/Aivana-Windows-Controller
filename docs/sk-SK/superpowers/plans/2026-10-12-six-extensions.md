<!-- generated-doc locale=sk-SK source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Plán implementácie rozšírenia

> **Pre agentov:** Požadované sub-skúsenosti: Použite superpower:plány na vykonávanie domácich alebo superpower:subagent-driven-development po tom, čo používateľ vyberie delegáciu.

**Cieľom je:** Poskytuje šesť integrovaných rozšírení a kapacitu založenú na multi-výrobcov klávesnice.

**Architektúra :** Zachráňte jadro Windows / WPF oddelenie. zavádza izolované plugin a mobilné služby procesy s úzky autentifikovaný IPC; všetky výkony prechádzajú existujúcim ovládačom a MacroRunner quue. Hardware adaptéry inzerujú zistené schopnosti namiesto infirmatizované širokej podpory.

**Technické stavy :** C# , .NET 10, WPF, Windows a automatizácia , Win32 Raw Input, dokumentované SDK predávajúceho, ASP.NET Core HTTPS/WebSocket, Systém .Text.Json

**Spoločnosť Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (predložené podľa užívateľa)

## Globálne obmedzenia

- Existujúca konfigurácia sa musí načítať nezmenená; namiesto toho, aby ste zmenili poradie ich trvalých hodnôt enum, pridajte druhy akcií.
- Zachovať existujúce profily, priradenia G1/G2, lokálnu prevádzku a používanie bez účtu.
- Raw Input poskytuje iba pozorovanie. Výhradné potlačenie pre jednotlivé zariadenia vyžaduje skutočný backend filtra klávesnice.
- Importované spustiteľné doplnky sa nesmú spúšťať automaticky.
- Predvolene je zakázané a obmedzuje nakonfigurované rozhrania pre mobilnú službu; žiadna núdzová autentifikácia pomocou obyčajného textu.
- Skratka/doručenie textu nie je dôkazom, že cieľová aplikácia príkaz prijala.
- Nenahrádzajte nainštalovanú aplikáciu, neinštalujte ovládače, nemeňte skutočné mapovanie klávesnice ani neotvárajte prístup k sieti ako náhodný test.
- Zachovať nesúvisiace nepotvrdené zmeny. V rámci tejto funkcie nepotvrdzujte existujúce zdieľané úpravy. Stage iba nezávisle skontrolovateľné časti funkcií, ak sú požadované odovzdania.

## Zameranie na recenziu

1. Duplicitné identické modely klávesnice: nejednoznačné opätovné pripojenie musí vyžadovať opätovný výber (Úlohy 10–12).
2. Zrušenie počas čakania UIA/poskytovateľ/doplnok/sieť: po zrušení žiadna odložená akcia (Úlohy 1, 4, 6, 8).
3. Úpravy používateľa, keď je otvorený sprievodca alebo náhľad balíka: nesúlad revízií musí zabrániť tichému prepísaniu (úlohy 3, 7).
4. Jazyk a fyzické rozloženie sa líšia: selektory a väzby skenovacieho kódu nesmú závisieť od lokalizovaných zobrazovaných názvov (Úlohy 4, 10).
5. Opakovanie siete a reštart doplnku: príkazy sa nesmú ticho vykonať dvakrát (úlohy 6, 8).

## Zdieľané zmluvy a závislosť

Úlohy 1–3 zavedú spustenie a nastavenie. Úlohy 4–5 zostaviť UIA. Úlohy 6–7 dodať pluginy. Úlohy 8–9 poskytujú mobilný prístup. Úlohy 10–12 dodávajú adaptéry klávesnice a integráciu filtrov. Úloha 13 overí celý produkt.

Každá úloha nasleduje po teste zlyhaní → minimálna implementácia → cyklus úspešného testu. Úloha je dokončená iba vtedy, keď je dosiahnuteľná prostredníctvom aplikácie a jej kontroly prejdú. Samotné lešenia, falošné adaptéry a dokumentácia nespĺňajú výstup.

### Úloha 1: Typové overenie a kompatibilná perzistencia

**Súbory:** Vytvorenie `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; upraviť `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Rozhrania:** Produkovať `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` a `VerifyAsync(ActionStep, CancellationToken)`, obaja sa vracajú `Task<ActionVerificationResult>`. Pridajte voliteľné serializovateľné `VerificationOptions` so zapnutým príznakom a časovým limitom (100–60000 ms), predvolene vypnuté. Zverejňujte zadané výsledky po jednotlivých krokoch vrátane ID spustenia, ID dlaždíc a indexu krokov bez obsahu akcií.

- [ ] Kontroly zápisu: starší JSON zachováva existujúce číselné hodnoty akcií; chýbajúce možnosti zakázať overovanie; neplatný časový limit zlyhá pri overení; zlyhanie predletovej kontroly zabraňuje privolaniu vykonávateľa; neúspešná postcondition nikdy neprinesie overený úspech; nepodporovaný exekútor je neoveriteľný; zrušením sa zastaví overovanie a následné kroky.
- [ ] Spustiť `dotnet run --project tests/Aivana.Tests -c Release`; potvrdiť zlyhanie nových kontrol pred implementáciou.
- [ ] Implementujte zmluvy, validáciu, klonovanie/uchovanie snímky a integráciu MacroRunner. Rešpektujte zásady zastavenia/pokračovania pri OnError; zachovať existujúcich predplatiteľov spätnej väzby pri vykonávaní.
- [ ] Spustiť základné testy; vyžadovať, aby všetky kontroly prešli.

### Úloha 2: Poskytovatelia overenia Windows a používateľské rozhranie výsledkov

**Súbory:** Vytvorenie `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; upraviť `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, skúšobné vstupné body.

**Rozhrania:** WindowsActionVerification implementuje poskytovateľa úlohy 1 pomocou existujúcich klientov zvuku, zobrazenia, procesu/okna a OBS. dostupnosť a identifikátory predletových testov; overenie porovnáva požadovaný stav po obmedzenom pollingu. Vytvárajte snímky výsledkov ovládača spotrebované zobrazením histórie WPF.

- [ ] Pridajte neúspešné testy pre nedostupné explicitné ID zvuku/zobrazenia, časový limit OBS, nezhodný pozorovaný stav, neznáme skratové efekty, vynechanie citlivého užitočného zaťaženia a zrušené dopytovanie pomocou injektovateľných poskytovateľov čítania.
- [ ] Implementujte skutočné overenie so spätným čítaním pre absolútnu hlasitosť, stlmenie zvuku, absolútny jas, pripravenosť procesu/okna a podporované príkazy OBS. Relatívne/prepínacie akcie vyžadujú zachytený predbežný stav; inak nahlásiť Neoveriteľné.
- [ ] Pridajte ovládacie prvky na overenie jednotlivých krokov a históriu bezpečného spustenia zobrazujúcu stav, neúspešnú požiadavku a text obnovy.
- [ ] Spustite kontroly jadra/Windows a dym WPF so vstreknutými poskytovateľmi stavu; žiadny skutočný jas ani zápis zvuku.

### Úloha 3: Riadená tvorba pracovného priestoru

**Súbory:** Vytvorenie `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; upraviť vstupné body palety a dashboardu.

**Rozhrania:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; výber obsahuje ID šablóny, zvolené cesty k spusteniu, adresy URL, ID zvuku/zobrazenia, scénu OBS a vybrané väzby. Discovery vráti pomenovaných kandidátov so stabilnými ID a dostupnosťou. Revízia konfigurácie konceptov záznamov na detekciu konfliktov.

- [ ] Pridajte neúspešné testy pre všetkých päť požadovaných rodín sprievodcov, nezávisle vygenerované kroky, chýbajúce požadované ciele, zrušte bez mutácie a uložte pred zmenenou revíziou.
- [ ] Objavte aplikácie prostredníctvom registrovaných ciest aplikácií a skratiek ponuky Štart; zariadení prostredníctvom existujúcich poskytovateľov; scény prostredníctvom dotazu OBS iba na čítanie. Nechajte manuálny výber k dispozícii pre neobjavené aplikácie.
- [ ] Implementovať Vyberte pracovný postup → Vyberte ciele → Vyberte ovládacie prvky → Ukážka/overenie → Uložiť. Generujte upraviteľné profily a nikdy ich nespúšťajte počas nastavovania.
- [ ] Pridať skutočné kontroly dymu sprievodcu WPF na dokončenie/zrušenie/konflikt; spustiť všetky testy.

### Úloha 4: Selektory UIA a ohraničené akcie

**Súbory:** Vytvorenie `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modifikácia modelov, overenie konfigurácie a WinActionExecutor.

**Rozhrania:** Pripojiť `ActionKind.UiAutomation`; `UiElementSelector` ukladá proces, ID automatizácie, typ riadenia a usporiadané kritériá predchodcu. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` vracia `Task`; operácie Invoke, SetValue, Select, Toggle, Focus a WaitState. Rozlíšenie výberu musí vrátiť presne jeden vhodný prvok.

- [ ] Pridajte neúspešné testy proti vyhradenému testovaciemu oknu pre premiestnenie/zmenu veľkosti, duplicitné zhody, chýbajúce prvky, nepodporované vzory, zakázané ovládacie prvky, hodnoty hesiel, štítky Unicode a časový limit/zrušenie.
- [ ] Implementujte UIA na špecializovanom pracovníkovi s ohraničenými požiadavkami a zacielením procesov. Potenciálne visiaci poskytovatelia bežia za hranicou reštartovateľného pracovníka; opustenie žiadosti nesmie zanechať vedľajšie účinky v rade.
- [ ] Vylúčiť hodnoty hesla; nahlásiť obmedzenia prístupu/integrity namiesto ich obchádzania.
- [ ] Spustite kontroly Windows pomocou izolovaných prípravkov; pripojiť iba hodnoty enum a zopakovať staršie kontroly stálosti.

### Úloha 5: Výber UIA a skontrolovaná nahrávka

**Súbory:** Vytvorenie `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; upraviť katalóg akcií, integráciu editora a záznamníka; vytvorte dymové kontroly UIA WPF.

**Rozhrania:** Picker vráti `UiElementSelector`; rekordér vracia navrhované kroky UIA a nikdy nezapisuje priamo do uloženého profilu. Príkaz Test-selector vráti počet zhôd bez vyvolania.

- [ ] Pridať neúspešné kontroly: výber vylučuje MAKRIVO a zachytávanie hodnoty hesla; záznam/zrušenie ponechá existujúci profil nezmenený; počet zhodných prehľadov s nejednoznačnými náhľadmi; prijatím zaznamenaných krokov sa vytvorí upraviteľný koncept.
- [ ] Implementujte zvýraznenie/výber prvku, parametre špecifické pre operáciu, náhľad selektora a okno kontroly záznamníka. Zachovať existujúce správanie zapisovača skratiek.
- [ ] Pridať katalógové položky a prístup k palete; pred uložením overte chyby editora.
- [ ] Spustite kontroly dymu Windows a WPF na zariadení; skontrolujte snímky obrazovky výberu/editora.

### Úloha 6: Izolovaný hostiteľ zásuvného modulu a protokol

**Súbory:** Vytvorenie `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`PH000006ZZZ upraviť riešenie a odoslanie akcie.

**Rozhrania:** Manifest Version-1 špecifikuje ID/verziu balíka, vstupný bod, schémy parametrov, akcie, poskytovateľov stavu a deklarácie schopností. Správy IPC obsahujú verziu protokolu, jedinečné ID požiadavky, metódu, parametre JSON a zadaný výsledok. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` vráti zadaný výsledok. Pripojte `ActionKind.Plugin`.

- [ ] Pridajte neúspešné testy pre nekompatibilný protokol, chybnú/nadrozmernú správu (>1 MiB), duplicitné ID požiadavky, zrušenie, zlyhanie, nereagujúci proces a chýbajúcu akciu.
- [ ] Implementujte IPC s pomenovaným potrubím pre rovnakého používateľa so 10-sekundovým handshake a konfigurovateľným časovým limitom vykonania obmedzeným na 60 sekúnd. Ukončiť nereagujúceho pracovníka; po reštarte automaticky neprehrávať akciu počas letu.
- [ ] Presadzovať schémy a povolené služby správcu. Vysvetlite dôveryhodnosť spustiteľného doplnku; izolácia procesov nie je karanténa OS.
- [ ] Spustite základné testy a integračné testy so skutočnými procesmi testovacích doplnkov.

### Úloha 7: Balíky doplnkov a príklad integrácie

**Súbory:** Vytvorenie `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, testy balíkov; upraviť položky palety/nastavení.

**Rozhrania:** Balíčková služba odhaľuje `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` a `UninstallAsync(packageId)`. Revidovaný balík zaznamenáva hash, závislosti, verziu a deklarované schopnosti.

- [ ] Pridajte neúspešné testy na prechod cez zip, absolútne cesty, symbolický odkaz/reparse escape, duplicitné položky, >100 rozšírené údaje MiB, nesúlad hash, downgrade, zastaralú kontrolu a zakázané importy.
- [ ] Implementujte ukážku a explicitné povolenie, chyby závislostí, postupnú inštaláciu a vrátenie aktualizácie; vyložiť pracovníka pred odstránením.
- [ ] Poskytnite skutočný príklad zásuvného modulu, ktorý vráti konfigurovateľnú akciu odpočítavania a živý stav prostredníctvom IPC, vrátane zrušenia; zabaliť/exportovať/inštalovať/odinštalovať cez používateľské rozhranie.
- [ ] Spustite testy balíkov/hostiteľov a komplexné kontroly WPF. Pred povolením skontrolujte, či nie sú spustené žiadne importované spustiteľné súbory.

### Úloha 8: Mobilná služba HTTPS a párovanie

**Súbory:** Vytvorenie `src/Aivana.MobileHost/` Hlavný projekt ASP.NET, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, integračné testy; upraviť riešenie a ovládač.

**Rozhrania:** Mobilná služba komunikuje s kontrolórom cez IPC rovnakého používateľa. Trasy: párovanie, povolené profily/akcie, vyvolanie overenej akcie, zrušenie a stav WebSocket. Vyvolanie obsahuje jedinečné ID príkazu; kontrolór vlastní ohraničenú deduplikáciu. Prihlasovacie údaje sú odvolateľné a uložené prostredníctvom ochrany Windows.

- [ ] Pridajte neúspešné testy na nespárované vymenovanie/spustenie, kód s vypršanou platnosťou, opätovne použitý kód, odvolaný token, neplatný pôvod, duplicitný príkaz, nadmerne veľký rámec a limity záplav.
- [ ] Implementujte iba poslucháč HTTPS, používateľské rozhrania, pokyny na poskytovanie lokálnych certifikátov/dôvery, jednorazové párovacie kódy 6, ktorých platnosť vyprší po sekundách 120, maximálne päť pokusov na kód a zrušenie tokenu. Ukladajte hašované náhodné tokeny; nikdy neprihlasujte poverenia.
- [ ] Implementujte stabilné identifikátory akcií na zozname povolených a existujúce smerovanie frontov; odmietnuť ľubovoľný shell, cesty a príkazy na inštaláciu balíka. Nakonfigurujte explicitné limity rýchlosti na klienta (príkazy 10/s, zhluk 30) a limit správ 64 KiB.
- [ ] Overte integračné testy spätnej slučky bez pridania pravidiel brány firewall alebo povolenia prístupu k sieti LAN; test vypnutia a perzistencie tokenu.

### Úloha 9: Ovládanie služieb mobilného panela a pracovnej plochy

**Súbory:** Vytvorte `src/Aivana.MobileHost/wwwroot/` responzívny panel a `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; pridajte skripty na overenie prehliadača/mobilu a pokrytie dymom WPF.

**Rozhrania:** Konzumujte iba trasy 8. Odhaľte mriežku dotykovej akcie, výber profilu, aktuálne hodnoty/pokrok, zrušenie, stav pripojenia a párovanie. Desktop riadi spustenie/zastavenie služby, inštrukcie adresy/certifikátu, povolené profily a zrušenie spárovaného klienta.

- [ ] Pridajte zlyhávajúce integračné kontroly toku autorizácie, zastarané/chýbajúce ID akcií, opätovné pripojenie bez prehratia akcie, odvolaného klienta a zastavenie služby.
- [ ] Implementujte prístupné responzívne používateľské rozhranie pomocou miestnych aktív, bez skriptov tretích strán alebo internetových závislostí. Tokeny sa nikdy nezobrazujú v adresách URL.
- [ ] Test rozloženia prehliadača na 360/768/1280 CSS px; skontrolujte skutočné snímky obrazovky. Overte pomocou skutočného telefónu/tabletu až potom, čo používateľ explicitne povolí službu LAN.
- [ ] Zdokumentujte a nahláste overenie mobilného hardvéru oddelene od emulácie prehliadača.

### Úloha 10: Možnosti zariadenia a pozorovanie pomocou bežného kľúča

**Súbory:** Vytvorenie `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modifikovať RawKeyboardBindingService, KeyboardBinding, validátor a diagnostiku zariadenia; pridať testy.

**Rozhrania:** `IKeyboardAdapter` odhaľuje snímky schopností, stabilnú identitu zariadenia a okraje fyzických kľúčov (kód skenovania, rozšírené príznaky, stlačenie/spustenie). Režimy rozlišujú pozorovanie, mapovaný štandardný vstup, natívny SDK a exkluzívny ovládač. Existujúce špeciálne väzby virtuálnych kľúčov si zachovávajú svoju reprezentáciu a správanie pri migrácii.

- [ ] Pridajte neúspešné testy pre staré väzby, rozdiely v zadávaní, nezávislosť jazyka/rozloženia, opakované výpadky, odpojenie pri držaní, nejednoznačnosť duplicitného modelu a odmietnutie vloženého vstupu.
- [ ] Rozšírte pozorovanie na bežné fyzické klávesy bez ich potlačenia. Pridajte explicitné zachytenie a opätovné pripojenie spracovania identity.
- [ ] Zobrazte aktuálne možnosti adaptéra a netestovaný stav hardvéru v používateľskom rozhraní zariadenia; nikdy neodvodzujte podporu Fn/dial z názvov značiek.
- [ ] Spustite kontroly jadra/Windows/WPF so simulovanými správami a enumeráciou zariadení len na čítanie.

### Úloha 11: Výrobca adaptérov

**Súbory:** Vytvorte samostatné súbory pod `src/Aivana.Windows/KeyboardAdapters/` pre LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog a všeobecné pokyny QmkVia; upraviť register/UI a pridať kontroly protokolu adaptéra.

**Rozhrania:** Úloha spotreby 10 zmluvy adaptéra. Natívne knižnice sa načítavajú iba z overených umiestnení aplikácií/dodávateľov; Neprítomnosť súpravy SDK je nedostupná funkcia. Možnosti osvetlenia a vstupu sú oddelené.

- [ ] Pred implementáciou každého adaptéra si overte aktuálne oficiálne hlavičky SDK, podmienky redistribúcie, softvérové závislosti a podporované modely; zdroje záznamov a presné verzie.
- [ ] Pridajte neúspešné testy na chýbajúcu/nesprávnu architektúru súpravy SDK, stlačenie/vydanie/opakovanie udalosti, odpojenie, chybnú odpoveď a nepodporované zariadenie. Na deterministické kontroly použite injektovateľné abstrakcie natívneho volania.
- [ ] Implementujte Logitech G-key vstup a podporované osvetlenie samostatne; Razer Chroma osvetlenie plus sprievodca mapovaním štandardných kláves Synapse; SteelSeries GameSense stav/osvetlenie; Wooting analógový polling s konfigurovateľnými prahmi/hysterézou; Návod na nastavenie štandardného kľúča QMK/VIA. Nenárokujte si vlastný vstup z rozhraní API osvetlenia.
- [ ] Overte kompatibilné skutočné zariadenia, ak sú k dispozícii. Označte ostatné implementované/netestované hardvérom s dôkazom modelu/firmvéru; chýbajúce použiteľné SDK je zverejnená vonkajšia brána.

### Úloha 12: Exkluzívny backend filtra klávesnice

**Súbory:** Vytvorte `src/Aivana.Windows/KeyboardFilter/`, inštalačný zdroj pod `tools/keyboard-filter/`, backendové integračné testy a dokumentáciu nasadenia. Ak žiadny vhodný licencovaný backend neprejde hodnotením, vyžaduje sa vyhradený projekt s podpísaným ovládačom.

**Rozhrania:** Backend hlási nainštalovaný/kompatibilný/podpísaný stav; odhaľuje spustenie/zastavenie snímania špecifické pre zariadenie, okraje kľúčov a núdzové uvoľnenie. Samostatný strážny pes obnoví priechod pri strate ovládača. Hlavná klávesnica sa v predvolenom nastavení nikdy nestane exkluzívnou.

- [ ] Vyhodnoťte licencovanie kandidátov na ovládače, podpísaný binárny pôvod a aktuálnu kompatibilitu Windows 11/Secure Boot. Zaznamenajte rozhodnutie ísť/nie. Žiadna inštalácia ovládača počas bežných testov.
- [ ] Napíšte testy zmluvy na výber cieľa filtra, otvorenie zlyhania, uplynutie platnosti srdcového tepu, stratu IPC, nejednoznačnosť opätovného pripojenia, pozastavenie/zrušenie, uvoľnenie podržaného kľúča a likvidáciu.
- [ ] Implementujte backend, používateľské rozhranie pre explicitný výber/aktiváciu, núdzové uvoľnenie a inštalačný/odinštalačný program. Nenahrádzajte heuristiky časovania low-level-hook/Raw Input.
- [ ] Overenie na obnoviteľnom testovacom zariadení s dvoma klávesnicami: vybraný kláves je potlačený iba na vybranom zariadení; hlavné písanie neovplyvnené; pád/pozastavenie/odinštalovanie obnoví vstup.
- [ ] Dokončenie výroby brány na skutočne podpísanom/licencovanom backende a fyzických testoch. Ak nie je k dispozícii externé podpisovanie/licencovanie, ponechajte úlohu neúplnú s presnými dôkazmi o závislosti.

### Úloha 13: Integrovaná verifikácia, preklady a vydanie balíka

**Súbory:** Pridať `docs/testing/2026-10-12-six-extensions.md`; aktualizovať príslušnú dokumentáciu DE/EN/FR/IT, lokalizačné katalógy a vstupné body kontroly dymu. Zachovať ostatné úpravy dokumentácie.

- [ ] Integrujte všetky vstupné body a snímky; dokončiť preklady pomocou existujúceho pracovného postupu katalógu v jazyku 27 a zverejniť neskontrolované preklady.
- [ ] Spustite `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` a `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Prezrite si snímky obrazovky počítača/mobilu a overte 100/150/200 percento Windows, cykly konfigurácie, životný cyklus doplnkov, zrušenie siete a skutočné opätovné pripojenie klávesnice.
- [ ] Publikovanie do nového adresára artefaktov pomocou `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; obsahovať binárne súbory sprievodnej služby/hostiteľa a licenčné upozornenia. Neprepisujte nainštalovanú aplikáciu.
- [ ] Hlásiť dokončené testy, hardvérovo netestované adaptéry a externé uvoľňovacie brány. Všetkých šesť výstupov musí spĺňať kritériá prijatia predtým, ako bude celá žiadosť vyplnená.

## Naplánujte si sebakontrolu

Všetkých šesť sekcií špecifikácií je namapovaných na úlohy 1–12; Úloha 13 pokrýva lokalizáciu, integrované overenie a uvoľnenie. Hlásenie pripravenosti, serializácia vykonávania, staršia konfigurácia, tajomstvá, dôveryhodnosť balíkov a nejednoznačnosť fyzického zariadenia majú explicitných vlastníkov a testy. Externé ovládače/SDK/hardvérové ​​brány zostávajú neúplné, kým sa nepreukážu, a nie sú považované za softvérový úspech.

## Voľba vykonania pre používateľskú kontrolu

Odporúčané: natívne vykonávanie v tomto rozhovore, úloha po úlohe, pretože väčšina modulov zdieľa zmluvy a aktuálny pracovný priestor obsahuje značné existujúce úpravy. Delegované vykonávanie je dostupné, ak si to používateľ vyberie. Prezrite si tento plán a pred implementáciou vyberte spôsob vykonania, ako to vyžaduje pracovný postup písania plánov.

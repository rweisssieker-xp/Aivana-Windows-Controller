<!-- generated-doc locale=cs-CZ source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/plans/2026-10-12-six-extensions.md)

# Plán implementace rozšíření MAKRIVO

> **Pro agentní pracovníky:** POŽADOVANÁ KOMPLETNÍ DOVEDNOST: Použijte superschopnosti:prováděcí-plány pro nativní provádění nebo superschopnosti:vývoj řízený subagentem poté, co uživatel vybere delegování. Sledujte kroky pomocí zaškrtávacích políček.

**Cíl:** Poskytněte šest integrovaných rozšíření a podporu klávesnice od různých výrobců na základě schopností.

**AArchitektura:** Zachovejte jádro / Windows / WPF oddělení. Zaveďte izolované procesy zásuvných modulů a mobilních služeb s úzkým autentizovaným IPC; veškeré provádění prochází stávajícím řadičem a frontou MacroRunner. Hardwarové adaptéry propagují zjištěné schopnosti spíše než odvozenou celoznačkovou podporu.

**Tech Stack:** C#, .NET 10, WPF, Windows automatizace uživatelského rozhraní, Win32 Raw Input, dokumentované sady SDK dodavatele, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (schváleno uživatelem).

## Globální omezení

- Stávající konfigurace se musí načíst beze změny; připojovat druhy akcí místo změny pořadí jejich trvalých hodnot výčtu.
- Zachovat stávající profily, přiřazení G1/G2, místní provoz a použití bez účtu.
- Raw Input poskytuje pouze pozorování. Výhradní potlačení pro jednotlivá zařízení vyžaduje skutečný backend filtru klávesnice.
- Importované spustitelné pluginy se nesmí spustit automaticky.
- Výchozí je zakázáno a omezuje nakonfigurovaná rozhraní pro mobilní službu; žádná záložní autentizace v prostém textu.
- Doručení zkratky/textu není důkazem, že cílová aplikace příkaz přijala.
- Nenahrazujte nainstalovanou aplikaci, neinstalujte ovladače, neměňte skutečné mapování klávesnice ani neotevírejte síťový přístup jako náhodný test.
- Zachovat nesouvisející nepotvrzené změny. V rámci této funkce nepotvrdzujte existující sdílené úpravy. Stage pouze nezávisle kontrolovatelné části funkcí, pokud jsou požadovány odevzdání.

## Zaměření na recenzi

1. Duplicitní identické modely klávesnice: nejednoznačné opětovné připojení musí vyžadovat opětovný výběr (Úlohy 10–12).
2. Zrušení během čekání UIA/poskytovatel/plugin/síť: žádná odložená akce po zrušení (Úlohy 1, 4, 6, 8).
3. Úpravy uživatele, když je otevřený náhled průvodce nebo balíčku: Neshoda revizí musí zabránit tichému přepsání (Úlohy 3, 7).
4. Jazyk a fyzické rozvržení se liší: selektory a vazby skenovacího kódu nesmí záviset na lokalizovaných zobrazovaných názvech (Úlohy 4, 10).
5. Opakování sítě a restart pluginu: Příkazy se nesmí tiše spustit dvakrát (Úlohy 6, 8).

## Sdílené smlouvy a závislostní příkaz

Úkoly 1–3 zřídí provedení a nastavení. Úkoly 4–5 sestavení UIA. Úkoly 6–7 dodat pluginy. Úkoly 8–9 poskytují mobilní přístup. Úkoly 10–12 dodávají adaptéry klávesnice a integraci filtrů. Úloha 13 ověřuje kompletní produkt.

Každý úkol následuje test selhání → minimální implementace → cyklus úspěšného testu. Úloha je dokončena pouze tehdy, je-li dosažitelná prostřednictvím aplikace a její kontroly projdou. Samotné lešení, falešné adaptéry a dokumentace nesplňují plnění.

### Úloha 1: Typové ověření a kompatibilní persistence

**Soubory:** Vytvořit `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; upravit `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Rozhraní:** Produkce `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` a `VerifyAsync(ActionStep, CancellationToken)`, oba se vracejí `Task<ActionVerificationResult>`. Přidejte volitelné serializovatelné `VerificationOptions` s povoleným příznakem a časovým limitem (100–60000 ms), ve výchozím nastavení zakázáno. Publikování výsledků zadaných po jednotlivých krocích, včetně ID běhu, ID dlaždic a indexu kroků, bez obsahu akcí.

- [ ] Kontroly zápisu: starší JSON zachovává stávající číselné hodnoty akcí; chybějící možnosti zakázat ověřování; neplatný časový limit selže při ověření; selhání před výstupem zabrání vyvolání exekutora; neúspěšná postcondition nikdy nepřinese ověřený úspěch; nepodporovaný exekutor je neověřitelný; zrušením se zastaví ověřování a následné kroky.
- [ ] Spustit `dotnet run --project tests/Aivana.Tests -c Release`; potvrdit selhání nových kontrol před implementací.
- [ ] Implementujte smlouvy, ověřování, klonování/uchování snímků a integraci MacroRunner. Respektujte zásady zastavení/pokračování při chybě OnError; zachovat stávající předplatitele zpětné vazby k provedení.
- [ ] Spustit základní testy; vyžadovat, aby všechny kontroly prošly.

### Úkol 2: Poskytovatelé ověření Windows a uživatelské rozhraní výsledků

**Soubory:** Vytvořit `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; upravit `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, testovací vstupní body.

**Rozhraní:** WindowsActionVerification implementuje poskytovatele úlohy 1 pomocí stávajících klientů zvuku, zobrazení, procesu/okna a klientů OBS. dostupnost a identifikátory předletových testů; ověření porovnává požadovaný stav po omezeném dotazování. Vytvářejte snímky výsledků kontroléru spotřebované zobrazením historie WPF.

- [ ] Přidejte neúspěšné testy pro nedostupná explicitní zvuková/zobrazovací ID, časový limit OBS, neodpovídající pozorovaný stav, neznámé zkratové efekty, vynechání citlivého užitečného zatížení a zrušené dotazování pomocí injektovatelných poskytovatelů čtení.
- [ ] Implementujte skutečné zpětné čtení pro absolutní hlasitost, ztlumení, absolutní jas, připravenost procesu/okna a podporované příkazy OBS. Relativní/přepínací akce vyžadují zachycený před-stav; jinak nahlásit Neověřitelné.
- [ ] Přidejte ovládací prvky ověření po jednotlivých krocích a historii bezpečného spuštění zobrazující stav, neúspěšný požadavek a text pro obnovení.
- [ ] Spusťte kontroly jádra/Windows a kouře WPF s poskytovateli vstřikovaného stavu; žádný skutečný jas ani zápis zvuku.

### Úkol 3: Řízené vytváření pracovního prostoru

**Soubory:** Vytvořit `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; upravit vstupní body palety a řídicího panelu.

**Rozhraní:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; výběr obsahuje ID šablony, zvolené cesty ke spustitelným souborům, URL, ID zvuku/zobrazení, scénu OBS a vybrané vazby. Discovery vrací pojmenované kandidáty se stabilními ID a dostupností. Revize konfigurace konceptů záznamů pro detekci konfliktů.

- [ ] Přidat neúspěšné testy pro všech pět požadovaných rodin průvodců, nezávisle generované kroky, chybějící požadované cíle, zrušit bez mutace a uložit proti změněné revizi.
- [ ] Objevte aplikace prostřednictvím registrovaných cest aplikací a zástupců v nabídce Start; zařízení prostřednictvím stávajících poskytovatelů; scény prostřednictvím dotazu OBS pouze pro čtení. U neobjevených aplikací mějte k dispozici ruční výběr.
- [ ] Implementovat Vybrat pracovní postup → Vybrat cíle → Vybrat ovládací prvky → Náhled/ověření → Uložit. Generujte upravitelné profily a nikdy je nespouštějte během instalace.
- [ ] Přidat skutečné kontroly kouře WPF průvodce pro dokončení/zrušení/konflikt; spustit všechny testy.

### Úloha 4: Selektory UIA a ohraničené akce

**Soubory:** Vytvořit `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; úpravy modelů, ověřování konfigurace a WinActionExecutor.

**Rozhraní:** Přidat `ActionKind.UiAutomation`; `UiElementSelector` ukládá proces, ID automatizace, typ řízení a uspořádaná kritéria předchůdce. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` vrací `Task`; operace Invoke, SetValue, Select, Toggle, Focus a WaitState. Rozlišení selektoru musí vrátit přesně jeden způsobilý prvek.

- [ ] Přidejte neúspěšné testy proti vyhrazenému testovacímu oknu pro přemístění/změnu velikosti, duplicitní shody, chybějící prvky, nepodporované vzory, zakázané ovládací prvky, hodnoty hesel, štítky Unicode a časový limit/zrušení.
- [ ] Implementujte UIA na vyhrazeném pracovníkovi s omezenými požadavky a zacílením procesů. Potenciálně visící poskytovatelé běží za hranicí restartovatelného pracovníka; opuštění požadavku nesmí zanechat ve frontě vedlejší účinky.
- [ ] Vyloučit hodnoty hesla; hlásit omezení přístupu/integrity namísto jejich obcházení.
- [ ] Spusťte kontroly Windows pomocí izolovaných přípravků; připojit pouze hodnoty výčtu a znovu spustit starší kontroly perzistence.

### Úloha 5: Výběr UIA a zkontrolovaná nahrávka

**Soubory:** Vytvořit `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; upravit katalog akcí, integraci editoru a záznamníku; vytvořit UIA WPF kouřové kontroly.

**Rozhraní:** Picker vrátí `UiElementSelector`; rekordér vrací navrhované kroky UIA a nikdy nezapisuje přímo do uloženého profilu. Příkaz Test-selector vrací počet shod bez vyvolání.

- [ ] Přidat neúspěšné kontroly: výběr vylučuje MAKRIVO a zachycení hodnoty hesla; záznam/zrušení ponechá stávající profil beze změny; nejednoznačné náhledové sestavy odpovídají počtu; přijetím zaznamenaných kroků vytvoříte upravitelný koncept.
- [ ] Implementujte zvýraznění/výběr prvku, parametry specifické pro operaci, náhled selektoru a okno kontroly záznamníku. Zachovat stávající chování záznamníku zkratek.
- [ ] Přidat položky katalogu a přístup k paletě; ověřte chyby editoru před uložením.
- [ ] Spusťte kontroly kouře Windows a WPF na zařízení; zkontrolovat snímky obrazovky pro výběr/editor.

### Úloha 6: Izolovaný hostitel a protokol pluginu

**Soubory:** Vytvořit `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`PH000007ZZ upravit řešení a odeslat akci.

**Rozhraní:** Manifest Version-1 určuje ID/verzi balíčku, vstupní bod, schémata parametrů, akce, poskytovatele stavu a deklarace schopností. Zprávy IPC obsahují verzi protokolu, jedinečné ID požadavku, metodu, parametry JSON a zadaný výsledek. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` vrátí zadaný výsledek. Připojte `ActionKind.Plugin`.

- [ ] Přidejte neúspěšné testy pro nekompatibilní protokol, chybnou/nadměrnou (>1 MiB) zprávu, duplicitní ID požadavku, zrušení, selhání, nereagující proces a chybějící akci.
- [ ] Implementujte IPC pojmenovaného potrubí stejného uživatele se 10 sekundovým handshake a konfigurovatelným časovým limitem provedení omezeným na 60 sekund. Ukončit nereagujícího pracovníka; po restartu automaticky nepřehrávat akci za letu.
- [ ] Vynucovat schémata a povolené služby správce. Vysvětlete důvěryhodnost spustitelného pluginu; izolace procesů není karanténa operačního systému.
- [ ] Spusťte základní testy a integrační testy se skutečnými procesy testovacích pluginů.

### Úloha 7: Balíčky pluginů a příklad integrace

**Soubory:** Vytvořit `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, testy balíků; upravit položky palety/nastavení.

**Rozhraní:** Balíčková služba odhaluje `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` a `UninstallAsync(packageId)`. Zkontrolovaný balíček zaznamenává hashe, závislosti, verzi a deklarované schopnosti.

- [ ] Přidejte neúspěšné testy pro procházení zipem, absolutní cesty, symbolický odkaz/reparse escape, duplicitní položky, >100 rozšířená data MiB, nesoulad hash, downgrade, zastaralou kontrolu a zakázané importy.
- [ ] Implementujte náhled a explicitní povolení, chyby závislostí, postupnou instalaci a vrácení upgradu; vyložit pracovníka před odstraněním.
- [ ] Poskytněte skutečný příklad pluginu, který vrací konfigurovatelnou akci odpočítávání a živý stav prostřednictvím IPC, včetně zrušení; balíček/export/instalace/odinstalace prostřednictvím uživatelského rozhraní.
- [ ] Spusťte testy balíčků/hostitelů a WPF end-to-end kontroly. Před povolením ověřte, že nejsou spuštěny žádné importované spustitelné soubory.

### Úloha 8: Mobilní služba HTTPS a párování

**Soubory:** Vytvoření `src/Aivana.MobileHost/` Základní projekt ASP.NET, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, integrační testy; upravit řešení a ovladač.

**Rozhraní:** Mobilní služba komunikuje s kontrolérem prostřednictvím IPC stejného uživatele. Cesty: párování, povolené profily/akce, vyvolání ověřené akce, zrušení a stav WebSocket. Vyvolání zahrnuje jedinečné ID příkazu; řadič vlastní omezenou deduplikaci. Přihlašovací údaje jsou odvolatelné a uložené prostřednictvím ochrany Windows.

- [ ] Přidejte neúspěšné testy pro nespárovaný výčet/spuštění, kód s vypršením platnosti, znovu použitý kód, odvolaný token, neplatný původ, duplicitní příkaz, příliš velký rámec a limity zaplavení.
- [ ] Implementujte pouze posluchače HTTPS, uživatelsky vybraná rozhraní, pokyny pro poskytování/důvěru místního certifikátu, jednorázové párovací kódy 6 s vypršením platnosti po 120 sekundách, maximálně pět pokusů na kód a odvolání tokenu. Ukládejte hashované náhodné tokeny; nikdy nezaznamenávat přihlašovací údaje.
- [ ] Implementujte stabilní ID akcí na seznamu povolených a stávající směrování fronty; odmítnout libovolný shell, cesty a příkazy k instalaci balíčku. Nakonfigurujte explicitní limity rychlosti na klienta (příkazy 10/s, shluk 30) a limit zpráv 64 KiB.
- [ ] Ověřte testy integrace zpětné smyčky bez přidání pravidel brány firewall nebo povolení přístupu k síti LAN; testovací vypnutí a perzistence tokenu.

### Úloha 9: Ovládací prvky mobilního panelu a desktopu

**Soubory:** Vytvořte `src/Aivana.MobileHost/wwwroot/` responzivní panel a `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; přidejte ověřovací skripty prohlížeče/mobilu a pokrytí kouřem WPF.

**Rozhraní:** Konzumujte pouze trasy 8. Odhalte mřížku dotykových akcí, výběr profilu, aktuální hodnoty/postup, zrušení, stav připojení a párování. Desktop řídí spouštění/zastavování služby, pokyny k adrese/certifikátu, povolené profily a odvolání spárovaného klienta.

- [ ] Přidejte neúspěšné integrační kontroly toku ověřování, zastaralá/chybějící ID akcí, opětovné připojení bez přehrání akce, odvolaného klienta a zastavení služby.
- [ ] Implementujte přístupné responzivní uživatelské rozhraní pomocí místních aktiv, bez skriptů třetích stran nebo závislostí na internetu. Tokeny se v adresách URL nikdy neobjevují.
- [ ] Test rozložení prohlížeče na 360/768/1280 CSS px; zkontrolovat skutečné snímky obrazovky. Ověřte se skutečným telefonem/tabletem až poté, co uživatel výslovně povolí službu LAN.
- [ ] Dokumentujte a oznamujte ověření mobilního hardwaru odděleně od emulace prohlížeče.

### Úloha 10: Možnosti zařízení a sledování pomocí běžného klíče

**Soubory:** Vytvořit `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; upravit RawKeyboardBindingService, KeyboardBinding, validátor a diagnostiku zařízení; přidat testy.

**Rozhraní:** `IKeyboardAdapter` odhaluje snímky schopností, stabilní identitu zařízení a okraje fyzických klíčů (skenovací kód, rozšířené příznaky, tisk/uvolnění). Režimy rozlišují pozorování, mapovaný standardní vstup, nativní SDK a exkluzivní ovladač. Stávající speciální vazby virtuálního klíče si zachovávají svou reprezentaci a chování při migraci.

- [ ] Přidejte neúspěšné testy starých vazeb, rozlišení zadání, nezávislost na jazyku/rozvržení, opakované výpadky, odpojení při držení, nejednoznačnost duplicitního modelu a odmítnutí vloženého vstupu.
- [ ] Rozšiřte pozorování na běžné fyzické klíče bez jejich potlačení. Přidejte explicitní zachycení a opětovné připojení zpracování identity.
- [ ] Ukázat skutečné možnosti adaptéru a netestovaný stav hardwaru v uživatelském rozhraní zařízení; nikdy neodvozujte podporu Fn/dial z názvů značek.
- [ ] Spusťte kontroly jádra/Windows/WPF se simulovanými zprávami a výčtem zařízení pouze pro čtení.

### Úloha 11: Výrobce adaptérů

**Soubory:** Vytvářejte samostatné soubory pod `src/Aivana.Windows/KeyboardAdapters/` pro LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog a obecné pokyny QmkVia; upravit registr/UI a přidat kontroly protokolu adaptéru.

**Rozhraní:** Úkol spotřeby 10 smlouvy o adaptéru. Nativní knihovny se načítají pouze z ověřených umístění aplikací/dodavatelů; Absence SDK je nedostupná funkce. Možnosti osvětlení a vstupu jsou samostatné.

- [ ] Před implementací každého adaptéru ověřte aktuální oficiální záhlaví SDK, podmínky redistribuce, softwarové závislosti a podporované modely; zdroje záznamů a přesné verze.
- [ ] Přidejte neúspěšné testy pro chybějící/nesprávnou architekturu SDK, tisk/uvolnění/opakování události, odpojení, chybnou odpověď a nepodporované zařízení. Pro deterministické kontroly použijte injektovatelné abstrakce nativního volání.
- [ ] Implementujte samostatně vstup G-key Logitech a podporované osvětlení; Razer Chroma osvětlení plus průvodce mapováním standardních kláves Synapse; SteelSeries GameSense stav/osvětlení; Wooting analogové dotazování s konfigurovatelnými prahy/hysterezí; Návod k nastavení standardního klíče QMK/VIA. Nenárokujte si proprietární vstup z rozhraní API pro osvětlení.
- [ ] Ověřte kompatibilní skutečná zařízení, pokud jsou k dispozici. Označte ostatní implementované/netestované hardwaru s důkazy o modelu/firmwaru; chybí použitelné SDK je zveřejněná externí brána.

### Úloha 12: Exkluzivní backend filtru klávesnice

**Soubory:** Vytvořte `src/Aivana.Windows/KeyboardFilter/`, instalační zdroj pod `tools/keyboard-filter/`, backendové integrační testy a dokumentaci k nasazení. Pokud žádný vhodný licencovaný backend neprojde hodnocením, je vyžadován vyhrazený projekt s podepsaným ovladačem.

**Rozhraní:** Backend hlásí nainstalovaný/kompatibilní/podepsaný stav; odhaluje start/stop zachycení specifické pro zařízení, hrany kláves a nouzové uvolnění. Samostatný hlídací pes obnovuje průchod při ztrátě ovladače. Hlavní klávesnice se ve výchozím nastavení nikdy nestane exkluzivní.

- [ ] Vyhodnoťte licencování kandidátů na ovladače, podepsanou binární provenienci a aktuální kompatibilitu Windows 11/Secure Boot. Zaznamenejte rozhodnutí jít/nejít. Žádná instalace ovladače během běžných testů.
- [ ] Napište smluvní testy pro výběr cíle filtru, otevření při selhání, vypršení srdečního tepu, ztrátu IPC, nejednoznačnost opětovného připojení, pozastavení/zrušení, uvolnění přidrženého klíče a likvidaci.
- [ ] Implementujte backend, explicitní výběr/aktivační uživatelské rozhraní, nouzové uvolnění a instalační/odinstalační program. Nenahrazujte heuristiku časování low-level-hook/Raw Input.
- [ ] Ověření na obnovitelném testovacím stroji se dvěma klávesnicemi: vybraná klávesa je potlačena pouze na vybraném zařízení; hlavní psaní nedotčeno; crash/suspend/uninstall obnoví vstup.
- [ ] Dokončení výroby brány na skutečném podepsaném/licencovaném backendu a fyzických testech. Pokud není k dispozici externí podepisování/licencování, ponechte úlohu neúplnou s přesným důkazem závislosti.

### Úloha 13: Integrované ověření, překlady a balíček vydání

**Soubory:** Přidat `docs/testing/2026-10-12-six-extensions.md`; aktualizovat příslušnou dokumentaci DE/EN/FR/IT, lokalizační katalogy a vstupní body pro kontrolu kouře. Zachovat ostatní úpravy dokumentace.

- [ ] Integrujte všechny vstupní body a snímky; dokončit překlady pomocí stávajícího pracovního postupu katalogu v jazyce 27 a zveřejnit nezkontrolované překlady.
- [ ] Spusťte `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` a `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Prohlédněte si snímky obrazovky počítače/mobilu a ověřte procento 100/150/200 škálování Windows, okružní cesty konfigurace, životní cyklus pluginu, zrušení sítě a skutečné opětovné připojení klávesnice.
- [ ] Publikování do nového adresáře artefaktů pomocí `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; obsahovat binární soubory doprovodné služby/hostitele a licenční upozornění. Nepřepisujte nainstalovanou aplikaci.
- [ ] Hlásit dokončené testy, hardwarově netestované adaptéry a externí uvolňovací brány. Všech šest výstupů musí splňovat kritéria přijatelnosti, než bude celý požadavek prohlášen za dokončený.

## Naplánujte si sebekontrolu

Všech šest speciálních sekcí je namapováno na Tasks 1–12; Úloha 13 pokrývá lokalizaci, integrované ověření a uvolnění. Hlášení připravenosti, serializace provádění, starší konfigurace, tajemství, důvěryhodnost balíčku a nejednoznačnost fyzického zařízení mají explicitní vlastníky a testy. Externí ovladače/SDK/hardwarové brány zůstávají nekompletní, dokud se neprokážou, spíše než se počítají jako softwarový úspěch.

## Volba provedení pro uživatelskou kontrolu

Doporučeno: nativní provádění v tomto chatu, úkol po úkolu, protože většina modulů sdílí smlouvy a aktuální pracovní prostor obsahuje značné existující úpravy. Delegované provádění je dostupné, pokud si to uživatel vybere. Zkontrolujte tento plán a před implementací vyberte způsob provedení, jak to vyžaduje pracovní postup psaní plánů.

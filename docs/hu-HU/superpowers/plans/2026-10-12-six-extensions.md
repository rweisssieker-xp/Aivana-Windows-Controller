<!-- generated-doc locale=hu-HU source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO A bővítések végrehajtási terv

> **A hivatalos munkavállalók számára:** KÉLKEZTETÉS: Szuperhatalmak használata: Végrehajtási tervek a hazai végrehajtáshoz vagy szuperhatalmakhoz:A felhasználó kiválasztása után a delegációt fejlesztjük ki.

**Cél:** Hat integrált kiterjesztés és kapacitás alapú több gyártó billentyűzet támogatása.

**Architecture:** Megőrizze a Core / Windows / WPF szétválasztás. bevezeti a szűk hitelesített IPC-vel ellátott izolált plugin és mobil szolgáltatási folyamatokat; az összes végrehajtás a meglévő vezérlővel és a MacroRunner-vel megy keresztül.

**Tech Stack:** C és .NET 10, WPF, Windows UI automatizálás, Win32 Raw Input, Az ASP.NET Core HTTPS/WebSocket, Szövegek.Json.

**Spec: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (a felhasználó által jóváhagyott)

## Globális korlátok

- A meglévő konfigurációt változatlanul kell feltölteni; az intézkedések típusát a meglévő enum értékek átalakításának helyett.
- Megőrzése meglévő profilok, G1/G2 feladatok, helyi üzemeltetési és számla-mentes használat.
- Raw Input csak megfigyelést biztosít. exkluzív készülékenként eltávolítása tényleges billentyűzetszűrő hátterét igényli.
- Az importált végrehajtható plug-inek nem futnak automatikusan.
- A mobilszolgáltatás konfigurált felületének megszakítása és korlátozása; nincs panaszos hitelesítés.
- A rövidítés/szövegszállítás nem bizonyítja, hogy a cél alkalmazás elfogadta a parancsot.
- Ne helyettesítse a telepített alkalmazást, telepítse a vezetékeseket, változtassa meg a valós billentyűzetet vagy a nyílt hálózati hozzáférést, mint egy véletlen tesztet.
- Tartsa a nem kapcsolódó nem megrendelt változásokat. Ne kövesse a meglévő megosztott szerkesztéseket ennek a funkciónak a részeként. A funkciók csak függetlenül felülvizsgálhatók, ha a megrendelések kérésre kerülnek.

## Fókusz áttekintése

1. Duplikált azonos billentyűzet modellek: a kétértelmű újraindításnak újra kell kiválasztania (Tasks 10–12).
2. A törlés az UIA/szolgáltató/plugin/hálózati várakozás során: nem indult meg a törlés után (Tasks 1, 4, 6, 8).
3. A felhasználó szerkesztése a varázsló vagy a csomag előzetes megjelenése során: a felülvizsgálat hibásnak meg kell akadályoznia a csendben való túlírást (Tasks 3, 7).
4. A nyelv és a fizikai elrendezés eltér: a kiválasztók és a szkennelési kódkötők nem függhetnek a lokalizált kijelzőnevekről (Tasks 4, 10).
5. Hálózati visszavonás és plugin újraindítása: a parancsok nem hajthatók végre kétszer (Tasks 6, 8).

## Megosztott szerződések és függőségrend

feladat 1–3 A végrehajtás és a beállítás. feladatok 4–5 Térképek - Térképek 6–7 Plugins - feladatok 8–9 Mobil hozzáférés - feladatok 10–12 A billentyűzet adapterek és a szűrő integrációja. 13 A teljes termék érvényesítése.

Minden feladat elvégzi a hibás tesztet → minimális végrehajtást → átmenet teszt ciklusát. A feladat csak akkor fejeződik be, ha elérhető az alkalmazáson keresztül és ellenőrzéseit átadja.

### Feladat 1: Tipp ellenőrzés és kompatibilis tartósság

**fájl:** Teremtés `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; módosítás `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfészek:** Termelés `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` és `VerifyAsync(ActionStep, CancellationToken)`, Mindkettő visszatér `Task<ActionVerificationResult>`. Választható szerializálható `VerificationOptions` A zászló és az időzítés (100–60000 MS), alapértelmezett eltávolítás. közzéteszik a nyomtatott lépésenként eredményeket, beleértve a futó azonosítót, a tile azonosítót és a lépésindexet cselekvési fizetések nélkül.

- [ ] Írja ellenőrzések: örökség JSON megtartja a meglévő cselekvési számértékeket; hiányzó opciók kikapcsolja a hitelesítést; érvénytelen időtávolság meghibásodik; előzetes hiba megakadályozza a végrehajtó hivatkozását; sikertelen utólagos feltételek soha nem eredményezik a hitelesített siker; támogatatlan végrehajtó nem hitelesíthető; törlés megállítja a hitelesítést és a későbbi lépéseket.
- [ ] Run `dotnet run --project tests/Aivana.Tests -c Release`; új ellenőrzések megerősítése nem történik meg a végrehajtás előtt.
- [ ] Végrehajtási szerződések, validáció, klónozás/snapshot megőrzése és MacroRunner integráció. tiszteletben tartja a OnError stop/continue politikát; megőrizze a meglévő végrehajtási visszajelzés előfizetők.
- [ ] A testületet elvégezzük; minden ellenőrzést meg kell haladnia.

### Task 2: Windows ellenőrzési szolgáltatók és eredmények UI

**fájl:** Teremtés `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; módosítás `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Bevezetési pontok.

**Interfészek:** WindowsActionVerification végrehajtása feladat 1 a szolgáltató a meglévő hang, a kijelző, a folyamat / ablak használatával és OBS kliensek. előrepülés tesztelhetőség és azonosító; ellenőrzés összehasonlítja a kívánt állapot után korlátozott felmérés. WPF történelmi látás.

- [ ] Hozzáadjuk a hiányzó teszteket a nem elérhető kifejezett audio/display ID-khoz, OBS időzítéshez, a nem megfelelő megfigyelési állapothoz, a nem ismert rövidítésekhez, az érzékeny fizetési terhelés elhanyagolásához és a törölt felméréshez, injekciós olvasási szolgáltatók használatával.
- [ ] Valódi olvasás-támogatott ellenőrzés végrehajtása abszolút térfogat, csúnya, abszolút fényesség, folyamat/télen készség és támogatott OBS parancsok.
- [ ] Hozzáadása lépésről lépésre ellenőrzés és biztonságos végrehajtási története mutatja az állapot, a kudarc követelmény és a helyreállítási szöveg.
- [ ] A futó központ/Windows ellenőrzések és WPF füst injekciós állami szolgáltatók; nincs valódi fény vagy hang írás.

### Task 3: Útmutató munkaterület létrehozása

**fájl:** Teremtés `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Változtassa meg a palettát és a dashboard bejárati pontokat.

**Interfészek:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; kiválasztás tartalmazza a sablon azonosító, kiválasztott végrehajtható útvonalak, URL-k, audio/display ID-k, OBS A Scene és a kiválasztott kötések.A Discovery visszatér a megnevezett jelöltekhez stabil azonosítóval és rendelkezésre állással.A konfliktus felismeréséhez készült felvételi felülvizsgálat.

- [ ] Hozzáadjuk a sikertelen teszteket az összes öt kérett varázsló családhoz, független lépéseket hozunk létre, hiányoznak a szükséges célok, mutáció nélkül törlődnek, és megtakarítják a módosított felülvizsgálat ellen.
- [ ] Fedezze fel az alkalmazásokat a regisztrált alkalmazás útvonalakon keresztül és a Start menü rövidítéseken keresztül; a meglévő szolgáltatókon keresztül; a jelenetek az olvasáson keresztül OBS lekérdezés.
- [ ] Implement Válassza ki a munkafolyamatot → Válassza ki a célokat → Válassza ki a vezérlőket → Előzetes megjelenítés/validálás → Megtakarítás.
- [ ] Hozzáadva a valós WPF varázslatos füstellenőrzések befejezése / törlés / konfliktus; fut minden tesztet.

### Task 4: UIA kiválasztók és korlátozott akciók

**Fájlok: ** Hozzon létre `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; módosítsa a modelleket, a konfiguráció érvényesítése és a WinActionExecutor.

**Interfészek:** Append `ActionKind.UiAutomation`; `UiElementSelector` áruházak, automatizálási azonosító, ellenőrzési típus és a megrendelt elődök kritériumai. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Visszatérés `Task`; A Invoke, SetValue, Select, Toggle, Focus és WaitState műveletek..

- [ ] Hozzáadjuk a hibás teszteket egy dedikált tesztpanelre a helyreállítás/megtakarítás, a duplikált mérkőzések, a hiányzó elemek, a nem támogatott minták, a tiltott ellenőrzések, a jelszó értékeinek, az Unicode címkéinek és a Timeout/Cancellation-nak.
- [ ] UIA végrehajtása egy dedikált munkavállaló korlátozott kérésekkel és folyamat célzással. potenciálisan függő szolgáltatók futnak egy visszafogható munkavállalói határ mögött; a kérés elhagyása nem hagyja a mellékhatásokat.
- [ ] A jelszó értékek kizárása; a hozzáférés/integritás korlátozásainak jelentése helyett.
- [ ] A Windows ellenőrzések elszigetelt rögzítések segítségével futnak; csak az enum értékeket adják hozzá, és a hagyomány tartósági ellenőrzések.

### Task 5: UIA picker és felülvizsgált felvétel

**fájl:** Teremtés `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; módosítja a katalógus, a szerkesztő és a rögzítő integrációját; létrehozza az UIA-t WPF Dohány ellenőrzés.

**Interfaces: ** A Picker visszatér egy `UiElementSelector`; a rekord visszatér az UIA javasolt lépéseit, és soha nem ír közvetlenül egy mentett profilra.

- [ ] Hozzáadjuk a hibás ellenőrzéseket: a picker kizárja MAKRIVO és a jelszó-érték rögzítését; a meglévő profilok felvétele / törlése változatlan; a kétértelmű előrejelzési jelentések összehasonlítása; a rögzített lépések elfogadása szerkeszthető tervezetet hoz létre.
- [ ] A végrehajtási elem kiemelkedése/csillapító, a műveleti paraméterek, a kiválasztó előzetes megjelenítése és a rögzítő felülvizsgálata.
- [ ] Hozzáadjuk a katalógus bejegyzéseit és a palettához való hozzáférést; a szerkesztő hibáit megerősítjük, mielőtt menteni.
- [ ] A Windows és WPF füstellenőrzések futtatása a rögzítésen; ellenőrizze a picker/szerkesztő képernyőfelvételt.

### Task 6: Elszigetelt plugin host és protokoll

**fájl:** Teremtés `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Módosítás és cselekvési diszpatch.

**Interfészek:** Version-1 manifesto jelzi a csomag ID/verszió, a bejárati pont, a paraméterek, a cselekvések, az állami szolgáltatók és a kapacitás nyilatkozatok. IPC üzenetek tartalmaznak a protokoll verzió, egyedi kérés ID, módszer, JSON paraméterek és a nyomtatott eredmény. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Visszaad egy tipikus eredményt. kiegészítés `ActionKind.Plugin`.

- [ ] Hozzáadjuk a nem kompatibilis protokollok, a rosszindulatú/felesleges (>1 MiB) üzenet, a duplikált kérés azonosítója, a törlés, a baleset, a nem felelős folyamat és a hiányzó cselekvés.
- [ ] Végezze el ugyanazt a felhasználót, amely a 10 másodperc kézzel és konfigurálható végrehajtási időt kapta 60 másodperc.
- [ ] Végrehajtása rendszerek és engedélyezett vezérlő szolgáltatások. magyarázza végrehajtható plug-in bizalom; a folyamat szigetelés nem egy OS homokdoboz.
- [ ] A kernel tesztelés és az integrációs tesztelés valós teszt plugin folyamatok ellen.

### Task 7: Plugin csomagok és példa integráció

**fájl:** Teremtés `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, csomagvizsgálat; módosítsa a palettát / beállításokat.

**Interfészek:** csomag szolgáltatás kiállítás `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` és `UninstallAsync(packageId)`. A felülvizsgált csomagok feljegyzései hashek, függőségek, verziók és kijelölt képességek.

- [ ] Hozzáadjuk a hibás teszteket a zip-térképhez, az abszolút útvonalhoz, a simlink/reparse meneküléshez, a duplikált bejegyzésekhez, >100 MiB bővített adatokhoz, a hash-meghibásodáshoz, a letöltéshez, a szilárd felülvizsgálathoz és a fogyatékkal élő importhoz.
- [ ] Végrehajtás előzetes és kifejezett engedélyezése, függőség hiba, lépésről lépésre telepítés és frissítés visszavonása; eltávolítása munkavállaló előtt.
- [ ] A valódi példa a plugin visszatérő konfigurálható számlázási akció és élő állapot IPC, beleértve a törlés; csomagolás / export / telepítés / letiltás keresztül a felhasználói felület.
- [ ] A csomag/szállító tesztelés és a WPF végső ellenőrzések futtatása.

### Task 8: HTTPS mobil szolgáltatás és párosítás

**fájl:** Teremtés `src/Aivana.MobileHost/` ASP.NET alapú projekt, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, integrációs teszt; módosító megoldás és vezérlő.

**Interfészek:** Mobil szolgáltatás beszélgetés a vezérlő keresztül azonos felhasználó IPC. útvonalak: párosítás, engedélyezett profilok / akciók, hitelesített cselekvési hívás, törlés és WebSocket állapot. a meghívás tartalmaz egyedi parancs azonosítót; a vezérlő rendelkezik korlátozott deduplicációval. a hitelezések visszavonhatók és tárolhatók a Windows védelem.

- [ ] Hozzáadjuk a hibás teszteket a nem teljesített listázáshoz/végzéshez, a kimerült kódhoz, az újrahasznált kódhoz, a visszavont tokenhez, a nem érvényes eredethez, a duplikált parancshoz, a túlzott kerethez és a felhő határainak.
- [ ] HTTPS-csak hallgató, a felhasználó által kiválasztott interfészek, a helyi tanúsítvány/bizalom utasítások, egy alkalommal 6-számú párosító kódok kimerülnek után 120 másodperc, max öt kísérletet kód és token visszavonása.
- [ ] Végrehajtás lehetővé teszi a listázott stabil cselekvési azonosítókat és a meglévő routing; elutasítja az önkényes shell, útvonal és csomag-install parancsokat. Állítsa be a kifejezett kliens-szint korlátozását (10 parancsok/s, 30 burst) és 64 KiB üzenet korlátozását.
- [ ] Ellenőrizze a loopback integrációs tesztek hozzáadásával tűzfal szabályok vagy lehetővé LAN hozzáférés; teszt zárolás és token állhatóság.

### Task 9: Mobil panel és asztali szolgáltatás ellenőrzése

**fájl:** Teremtés `src/Aivana.MobileHost/wwwroot/` A válaszpanel és `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; böngésző/mobil ellenőrzési írásokat adj hozzá és WPF Dohányzás fedélzet.

**Interfaces: ** Consume Task 8 útvonalak csak. Kimutatja a touch action grid, profil picker, élő értékek / progress, törlés, kapcsolat állapot és párosítás. Desktop vezérli a szolgáltatás indítás / megállás, cím / tanúsítási utasítások, engedélyezett profilok és a párokat-ügyfél visszavonása.

- [ ] Hozzáadjuk a hibás integrációs ellenőrzéseket az auth áramláshoz, a megálló/megszűnt akció azonosítókhoz, a cselekvés újrahasznosításához, a visszavonott ügyfélhez és a szolgáltatás megállásához.
- [ ] A hozzáférhető választható felhasználó telepítése helyi eszközökkel, harmadik fél scripts vagy internetfüggőségek nélkül.
- [ ] A böngésző elrendezése a 360/768/1280 CSS px-on; ellenőrizze a valós képernyőfelvételek. Ellenőrizze a valós telefont/tablettát csak akkor, ha a felhasználó kifejezetten engedélyezi a LAN szolgáltatást.
- [ ] Dokumentum és jelentés mobil hardver ellenőrzése külön a böngésző emuláció.

### Feladat 10: eszközkapacitás és a szokásos kulcs megfigyelése

**Fájlok: ** Hozzon létre `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; módosítsa a RawKeyboardBindingService, KeyboardBinding, validátor és eszköz diagnosztika; adja meg a tesztet.

**Interfaces: ** `IKeyboardAdapter` feltárja a képességet, stabil eszköz identitás és fizikai kulcsvonal (scan kód, kiterjesztett zászló, sajtó / kiadás). módok különbözteti meg a megfigyelést, mapped szabványos beviteli, szülői SDK és a driver-exclusive.

- [ ] Hozzáadjuk a sikertelen teszteket a régi kötésekhez, a Enter különbségekhez, a nyelv/rendezés függetlenségéhez, a megismételt lejáratokhoz, a tartás közben történő feloldásához, a duplikált modell kétségességéhez és az injekciós beviteli elutasításhoz.
- [ ] Tovább a megfigyelés a szokásos fizikai kulcsok, anélkül, hogy elnyomja őket.
- [ ] Mutassa meg a készülék alkalmazásában a valós adapter képességeket és a hardver-vizsgált állapotot; soha nem adja meg a Fn/dial támogatást a márkanevekből.
- [ ] Run core/Windows/WPF ellenőrzések szimulált jelentések és olvasó-csak eszköz felsorolás.

### Task 11: gyártó adapterek

**Fájlok: ** Hozzon létre külön fájlokat a `src/Aivana.Windows/KeyboardAdapters/` a LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog és a generikus QmkVia útmutató; módosítsa a nyilvántartást / UI és adja hozzá az adapter protokoll ellenőrzéseit.

**Interfaces: ** Fogyasztási feladat 10 adapter szerződések. a szülői könyvtárak csak érvényesített alkalmazás/értékesítési helyeken terhelnek; SDK hiánya nem elérhető.

- [ ] Ellenőrizze a jelenlegi hivatalos SDK címkéket, az újratelepítési feltételeket, a szoftverfüggőségeket és a támogatott modelleket, mielőtt minden adapteret végrehajtanák; rögzítse a forrásokat és a pontos verziókat.
- [ ] Adjunk hibás teszteket az SDK hiányzó/csúszott architektúrájára, az esemény sajtó/kiadás/újítására, a kapcsolódás megszüntetésére, a rosszindulatú válaszra és a nem támogatott eszközre.
- [ ] Végrehajtása Logitech G-kulcs bevitele és támogatott világítás külön; Razer Chroma világítás plusz Synapse szabvány kulcs mappázó varázsló; SteelSeries GameSense állapot/világítás; Wooting analóg vizsgálat konfigurálható küszöbértékekkel/hiszterézise; QMK/VIA szabvány kulcs beállítási útmutató.
- [ ] Validálja a kompatibilis valós eszközök, ahol elérhető. jelölje meg a többi bevezetett/hardware-teszteződés, a modell/hardware bizonyíték; hiányzó használható SDK egy közzétett külső kapu.

### Task 12: Exclusive billentyűzet szűrő hátter

**fájl:** Teremtés `src/Aivana.Windows/KeyboardFilter/`, forrás beállítása alatt `tools/keyboard-filter/`, A backend integrációs tesztek és a telepítési dokumentáció. egy dedikált aláírt járművet kell elvégezni, ha nincs megfelelő engedélyezett backend átvizsgálás.

**Interfaces: ** Backend jelentések telepített/kompatibilis/jelölt állapot; kiemeli a készülék-specifikus fogás kezdő/megállás, kulcsoldat és vészhelyzeti kiadás. Egy külön figyelő visszaállítja a vezérlő veszteség. A fő billentyűzet soha nem válik kizárólagos alapértelmezett.

- [ ] Értékelje a jelölt járművezetői engedélyt, aláírt bináris származást és jelenlegi Windows 11/Secure Boot kompatibilitást.
- [ ] Írja meg a szerződés tesztjeit a célválasztás, a hibás nyitás, a szívroham lejárta, az IPC elvesztése, a kétségbeesés újraindítása, a szünet / törlés, a kulcsok felszabadítása és a felszabadulás.
- [ ] Végrehajtás backend, kifejezett kiválasztás/aktiválás UI, vészhelyzeti felszabadítás és telepítő/uninstallátor. Ne helyettesítse az alacsony szintű hang/Raw Input időzítő.
- [ ] Validáljon egy helyreállítható tesztelők két billentyűzet: kiválasztott kulcsot csak a kiválasztott eszközön; a főnyomtatás nem érintett; a csapás / felfüggesztés / telepítés visszaállítása.
- [ ] Kapu gyártás befejezése a tényleges aláírt/licencelt háttér és fizikai vizsgálatok. ha a külső aláírás/licencelés nem elérhető, tartsa a feladat hiányos pontos függőség bizonyíték.

### Task 13: Integrált ellenőrzés, fordítások és kiadás csomag

**Fájlok: ** Hozzáadva `docs/testing/2026-10-12-six-extensions.md`; frissítve a releváns DE/EN/FR/IT dokumentációt, a lokalizációs katalógusokat és a füst ellenőrzési pontokat.

- [ ] Integrálja az összes bevezető pontot és a snapshots-t; teljes fordításokat a meglévő 27 nyelven a katalógus munkafolyamatával és feltárja a felülvizsgált fordításokat.
- [ ] futás `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, és `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Ellenőrizze az asztali/mobil képernyőfelvételek és ellenőrizze 100/150/200 százalékos Windows méretezés, konfigurációs körút, plugin életciklus, hálózati visszavonás és valós billentyűzet újraindítás.
- [ ] Közzéteszi egy új műanyag-direktor `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; tartalmazza a társszerző szolgáltatás/szállító bináris és licenc értesítések.
- [ ] Jelentés befejezett tesztek, hardver-vizsgált adapterek és külső kiadás kapuk. Minden hat szállító meg kell felelnie a fogadási kritériumok előtt az egész kérés teljes.

## Öntudat terv

Minden hat műszaki szakasz térképet a Task 1–12; Task 13 fedezi a lokalizáció, integrált ellenőrzés és kibocsátás. készségjelentés, végrehajtás serializáció, örökség konfiguráció, titkok, csomag bizalom és a fizikai eszközök kétértelműség kifejezett tulajdonosok és tesztek. Külső vezérlő / SDK / hardver kapuk maradnak hiányos, amíg bizonyított, ahelyett, hogy számítva a szoftver siker.

## Végrehajtási választás a felhasználói felülvizsgálathoz

Ajánlott: a csatában a hazai végrehajtás, a feladat a feladat, mert a legtöbb modul megosztja a szerződéseket és a jelenlegi munkaterület tartalmaz jelentős meglévő szerkesztéseket. A felhatalmazott végrehajtás elérhető, ha a felhasználó azt választja.

<!-- generated-doc locale=hu-HU source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../README.md)

# MAKRIVO

[Teljes dokumentáció minden 27 nyelven](README.md) · [Teljes felhasználói kézikönyv](HANDBOOK.md)

**A Aivana.** [Jelenlegi felsorolás és felszabadítás korlátozások](FEATURES.md) · [Kiterjesztett tesztjelentés](testing/2026-10-12-six-extensions.md) · [Helyi asszisztensek](testing/2026-10-12-local-meeting-assistant.md)

** angol (USA) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

A helyi Windows hotkey vezérlő a Stream Deck stílusú lemezekkel, profilokkal, makrókkal és billentyűzet felderítésével. A felület támogatja a 27 nyelvet.

[Dokumentáció](README.md) · [Ellenőrzés](VERIFICATION.md) · [Feature tesztjelentés](testing/2026-10-09-usp-release.md)

## Kezdődik a

A kiadás után az alkalmazás a `artifacts/win-x64/MAKRIVO.exe` oldalon van. tartsa össze az egész kimeneti mappát; másolhatja máshol. Windows 11 x64 a kezdeti cél.

Telepítse a .NET 10 SDK-t, amelyet a `global.json`-ben megadnak, hogy a forrásból építsenek és futjanak:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Az öntartalmú építkezés közzététele külön telepített .NET futtatási időt nem igényel:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Alapvető munkafolyamat

1. Válasszon egy profilot, és hozzon létre egy cselekvési tálat.
2. A név, ikon, szín és billentyűzet rövidítése.A Ctrl/Alt/Shift kombinációk csökkentik a véletlenszerű aktiválást a nyomtatás során.
3. Hozzon hozzá egy vagy több lépést: alkalmazások, fájlok, weboldalak, szöveg, rövidítések, média, térfogat, profil váltás, vagy késések.
4. A változások megmaradnak, amíg meg nem mentették; a discard visszaállítja a mentett verziót. A játék gomb és a forró kulcs ugyanazt a végrehajtási utat használja.
5. A szabványok egyszerűsítik a beállításokat. a macrosok legfeljebb 100 alkalommal ismételhetik meg. törölje a felületet, a rendszer nyomás menüjét, vagy a Ctrl+Alt+Escape alapértelmezett módon.

A profilok alkalmazási jellemzői a folyamat nevét használják, teljes út nélkül.A profilrendelés megszakítja a kapcsolatokat; a szabványos profil a visszaesés.A profil automatikus kiválasztása a beállításokban engedélyezhető.

A fő ablak bezárása a rendszerben lévő alkalmazást alapértelmezett módon tartja. dupla kattintson a nyomtatási ikonra, hogy újra megnyissa; használja a nyomtatási menüt a kilépéshez. A Windows indítás opcionális és alapértelmezett módon letiltva.

## Támogatás és parancs palettája

Add Action nyolc kategóriában nyílik meg egy kereshető katalógus, amelyben 90 előzetes beállítások találhatók.Minden leírás és előzetes megjelenítéssel rendelkezik.A macro szerkesztő ugyanazt a katalógust használja a lépések hozzáadásához vagy cseréjéhez.Ezen szerkesztői munkafolyamban előzetes beállítás kiválasztása végrehajtás nélkül létrehoz egy tervezetet.

A parancspálita a címről vagy a Ctrl+K-val az alkalmazáson belül megnyílik. A profilok, a katalógus előzetes beállításai, a profil váltása, a navigáció, a szünet/nyitás, a makro törlés és az import/export. A mentett akciók, a katalógus funkciók vagy az alkalmazás parancsai alapján a szűrő. A kulcsok kiválasztása, a beállítás aktiválása, a Ctrl+Enter szerkeszthető beállítás, és a Escape zárva. A kedvencek először jelenik meg. A használatra kész katalógus parancsok azonnal futhatnak; a beállítások egy tervezetet nyitnak. A beállítások globális rövidítést határoznak meg. Új konfigurációk Ctrl+Shift+Space-t használják; a meglévő konfigurációk nem kapnak új globális rövidítést.

Audio akciók pontosan a 0 és 100 százalékos és csúnya, csúnya, vagy a kimenetel és a mikrofon eszközök. Válasszon egy adott végpontot, vagy hagyja, hogy a Windows alapértelmezett. Egy nem elérhető mentett végpont okoz hibát, ahelyett, hogy ellenőrizze egy másik eszköz.

OBS A stúdió használata WebSocket v5: Start/stop/toggle/pause/resume rögzítés; Start/stop/toggle streaming; változó jelenetek; toggle bejárati csúszás. WebSocket A szerver az eszközök OBS és beállítja a végpontot a szerkesztőben. Check Connection és Load Noms olvassa el a OBS verziók, jelenetek és bejegyzések, anélkül, hogy elkezdené a felvételt vagy áramlást. Válassza ki a listából a neveket, vagy adja meg őket kézzel. Windows A hitelminősítő és kizárt az exportált konfigurációktól. OBS A kapcsolatok hiba.

A profilok egy folyamat neve és egy ablak címe egy része. Mindkét meghatározott feltételnek meg kell felelnie. A Lock Profile megakadályozza az automatikus váltást a szezonra.

Text és clipboard támogatás `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, és `{{user}}`. dátumok használata `yyyy-MM-dd`; Időjárás használata `HH:mm`. A nem ismert helyiek változatlanok. `{{profile}}` A megindított akció profilja; `{{process}}` azonosítja a külső alkalmazási kontextust. a klipboard tartalmát csak akkor olvashatják, ha azt a helytartó használja. a klipboard akció szöveget ír, de nem automatikusan rögzíti.

Az ablakok cselekedetei minimalizálják, maximalizálják, helyreállítják vagy rendezik a monitor munkaterületének bal/jobb oldalán lévő ablakot.A rendszer cselekedete zárol Windows.Ezek a cselekedések csak akkor futnak, ha kifejezetten felhívják.

Az előzetes megjelenítése lépéseket, ismétléseket és változók futtatása nélkül. Undo és Redo tartja a 100 tervezési állapotokat; szövegdobozok tartják a Ctrl+Z szerkesztését. Jelölje ki a kedvenceket a szerkesztőben vagy a lap menüben, és szűrje őket a táblázaton.

A szöveg, a rövidítés és az ablakok a felületről indított akciók aktiválják az utolsó külső cél ablakot. A paletta azt mutatja, hogy a cél kontextus. Ha a cél zárva van, vagy Windows megtagadja az aktiválást, a végrehajtás megáll egy üzenettel.

## Keyboard támogatás és visszajelzés

A Special-Key Wizard a Keyboard oldalon és a palettában található. A G-kulcsokat iCUE által jelentette be. A Detect Key Press ideiglenesen rögzíti őket az elkülönített akciók végrehajtása nélkül. A normál kötések a felismerés, a törlés vagy a zárás után visszatérnek. A katalógus funkció és a kiválasztott kulcs nyitva van, mint egy nem mentett tervezet; a duplikált kötések ellenőrizhetők.

A billentyűzet átlátszó megjeleníti a kiválasztott referenciamutatót és az aktív kötéseket. tartsa Ctrl+Alt+F1 vagy húzza ki a palettából vagy a billentyűzet oldalából. nem veszi figyelmet és lehetővé teszi az egér kattintásainak átlépését. profil és a kötelező változások frissítése látható; a szokásos Windows kulcsszavak kiemelkednek. a gyártó-specifikus ellenőrzések nem adhatnak egyenértékű Windows kulcsszavakat.

A végrehajtás visszajelzései várakozás, lépés/másodperc előrehaladás, befejezése, törlése vagy kudarc a bejegyzett szöveg figyelembevételével vagy feltárásával. a végrehajtott parancsok megerősítik a parancs végrehajtását, nem egy önállóan keresett élő állapotot egy harmadik fél alkalmazásában.

## A készülékek és Corsair

Corsair G1–G6 közvetlenül a hivatalos iCUE SDK. Válassza ki a készüléket és a kulcsot a cselekvési szerkesztőben, és mentse meg, vagy készítsen egy tervezetet a K100 billentyűzet megjelenése. a kapcsolódások a kiválasztott eszközre és az aktív profilra vonatkoznak; a szünet is kikapcsolja őket. iCUE A hozzáférést meg kell hajtani, és a iCUE A kerék, a profil és a zár vezérletei nem térképezhetők a G-key API-n keresztül.

A Devices oldal Windows billentyűzet felületeket és frissíti időszakosan és amikor a készülékek csatlakoznak. A fizikai billentyűzet több HID felületet mutathat.

Corsair A RGB vezérlés kompatibilis hardvereket igényel, iCUE, és a hivatalos iCUE SDK; az alkalmazás jelentése a tényleges rendelkezésre állás. más gyártók jelenleg kapnak felismerést és szabványos intézkedéseket. Corsair G-key integráció, tulajdonellenőrzés működik, amikor Windows rendszeres beviteli formában kapja meg őket. az alkalmazás nem telepíti a billentyűzetvezetőt.

## Nyelvek

A beállítások 27 nyelvek: német, angol, francia, spanyol, olasz, portugál, holland, lengyel, cseh, szlovák, magyar, román, görög, dán, svéd, finn, észt, lett, szlovén, horvát, ukrán, orosz, török és szerb latin írásban. automatikusan követi a Windows megjelenítési nyelvet. fordítások helyi összefüggésben vannak.

Teljes tároló dokumentáció elérhető az összes 27 alkalmazási nyelven a [Nyelv index](README.md)A A fordítások gépi segítséget nyújtanak, eredeti forráskódú linkekkel és szerkezeti ellenőrzésekkel; az anyanyelvű felülvizsgálat elkülönül. Az alkalmazás elfogadja a regionális kódokat `en-US`, `de-DE`, `fr-FR` és `it-IT`, és számformátumokat használ. A meglévő nyelvi kódok továbbra is kompatibilisek.

## Layouts és gyártó kutatás

A billentyűzet megjelenítése teljes méretű és gondolatlan ISO-DE/ANSI-US referenciákat és Corsair K100 RGB elrendezéseket biztosít. A geometria pozíciókat, széles/tömeges kulcsokat és az ISO Enter-t képviseli. A szabványos elrendezések generikus referenciákat jelentenek. K100 a elrendezések dokumentált ellenőrzéseket követnek anélkül, hogy milliméter-pontos dimenziókat igényelnének. Válassza ki a megfelelő nyelvet/geometria változatot: egy modellnev vagy USB VID/PID nem állapít meg egy adott változatot.

A térképezés a fizikai szkennelési kódokat az aktív Windows beviteli elrendezéssel oldja meg. A munkafolyamat egy tervezetet készít és megtartja a mentési/konfliktus ellenőrzéseket. A szabványos kötések globális Windows hotkeys, amelyek nem egy csatlakoztatott billentyűzetre vannak elszigetelve. Corsair G1-G6 készülék-specifikus iCUE integrációt használnak. Más tulajdonos vezérlések adapter vagy gyártó-konfigurált rövidítéseket igényelnek.

A [Gyártó kutatás](research/tastaturhersteller-sonderfunktionen.md) és [CSV](research/tastaturhersteller-sonderfunktionen.csv) fedezze 74 márka/gyártó csoportok, megkülönböztetve konfigurátorok, belépési SDK-k, és világítási API-k. [Layout bizonyítékok](research/keyboard-layout-evidence.md) rekord modell, SKU, és modul különbségek. A kutatási lefedettség nem jelenti azt, hogy a hardver támogatást végrehajtják.

## adatai

A konfigurációt helyi alkalmazási adatokban tárolják. import/export használja a JSON-t. import előrejelzi a konfigurációt, helyettesíti a jelenlegi konfigurációt a megerősítés után, és kikapcsolja az importált kötéseket, amíg manuálisan nem engedélyezhető. szöveg-munkakönyvi tartalmak nem kerülnek gyűjtésre kulcsfontosságú naplóként.

## Ellenőrzés

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows ellenőrzések ellenőrizhetik a hardvereket és ideiglenesen konfigurálhatják/kijelenthetik a támogatott iCUE kötéseket. futtathatják őket más vezérlő nélkül az ilyen kötések használatával.

A szülői beviteli nem haladhatja meg a Windows előnyöket.A rendszeresen indított vezérlő nem küldheti be a bevitelt egy adminisztrátor alkalmazáshoz.A fenntartott vagy elfoglalt hotkeys konfliktusként jelennek meg.

A [Dokumentációs index](README.md) linkek történelmi tervezések, tervek, kutatás, és dátumos tesztjelentések. A történelmi tervek leírják a rögzített hatótávolságukat, nem pedig a funkciók jelenlegi garanciáját.

### A munkafolyamatok és a célzó ablakok

A katalógus hat szerkeszthető munkafolyamatot tartalmaz: munkaterület, találkozó, streaming, kutatás, projekt váltó és szövegszerkesztés. `workflow.` a katalógusban vagy parancspalettában. a munkafolyamat kiválasztása nem mentett tervezetet nyit meg; soha nem fut azonnal. Minden lépés megmarad, amikor egy munkafolyamatot hozzáad egy meglévő makrohoz vagy a Corsair URL-k, programútok, profilok beállítása, és OBS Színházak mentése előtt.

A **Várjon a cél ablak** A lépés elfogadja a folyamat nevét (a folyamatgal vagy anélkül) `.exe`), A címek érzéketlen szubsztringje, és a 1–600,000 ms. Legalább egy szűrő szükséges; mindkét szűrőnek megfelelnie kell, amikor szállítják. A makrot csak akkor folytatják, ha egy megfelelõ látható ablak válaszol és eléri az előzményt. Használjon egy adott címet, amikor több ablak megosztja a folyamatot. A törlés vagy időzítés megáll a következő lépésekben. A ablak készenlét nem garantálja, hogy egy dokumentum töltött vagy külső szolgáltatás, mint például OBS WebSocket Kapcsolódó.

A kijelző fényessége: keresés **A kijelző fényessége**, **A kijelző fényessége**, vagy **A kijelző fényessége 50 százalékban** az akciókatalógusban.A relatív akciók fényessége 10 százalékpontokkal változik; a rögzített értékek tartománya 0 és 100 között.A támogatott belső kijelzők WMI-t használnak; a kompatibilis külső monitorok DDC/CI engedélyezése a menüjükben.

### Továbbfejlesztett ellenőrzések

Nyissa meg a parancspalettát **Ctrl+K, míg az alkalmazás ablakon van a fókusz**. Keresés a **Workflow designer**, **Record macro**, **Synchronize profiles**, vagy **Key diagnostics** (a szerkesztés követi a kiválasztott nyelvet). A munkafolyamat tervező szerkeszt a kiválasztott lapterv: fájl/folyamat/ablak feltételei, előre repül és megáll/folyamatos hiba kezelése. A rekord rögzíti a rövid időt és vár, nem szokásos nyomtatás; Escape megáll a rögzítés.

A fényesség lépésben válassza ki az összes támogatott kijelzőt vagy egy adott monitorot. Egy nem elérhető kiválasztott monitor hibát okoz, ahelyett, hogy csendesen megváltoztatja a másik kijelzőt. A lemez élő fényességet, hangot és OBS olvasást mutat, amikor az egyes integrációk rendelkezésre állnak; egy nem elérhető olvasás nem jelenik meg mérett értékként.

A tál szerkesztőben adjon hozzá egy támogatott fizikai speciális kulcsot, és válasszon azonnali nyomtatást, tapintást, tartást vagy kettős tapintást. A Held réteg kiválasztók ideiglenesen aktiválják a másik mappát és helyreállítják a korábbi réteget a kiadáson. A gesztusok és rétegek alkalmazhatók az Corsair SDK G-kulcsokra és a készülék-specifikus speciális kulcsokra, nem a szokásos globális kulcsokra. A standard kulcsfogadás támogatja F13–F24 és a billentyűzet média kulcsokat. A tulajdonos Fn, a profil és a dial vezérlő még mindig kompatibilis adattár vagy a szállító mappát egy szabvány kulcsra követik. Raw Input figyelembe veszi a kulcsot és nem szünteti a normális Windows viselkedését.

A profilszinkronizáció a választott mappán keresztül fájlokat cserél, beleértve a meglévő felhőkliens által kezelt mappát is. A import előtt felülvizsgálja a előrejelzést; a import elindul, és eltávolítja a hardver-specifikus mappákat.. MAKRIVO nem nyújt felhőfiókot vagy megtartja a felhőazonosításokat. MAKRIVO’saját teal M ikon és mutat egy amber szünet jelzés, amikor szünet.

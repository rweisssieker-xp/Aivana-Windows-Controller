<!-- generated-doc locale=hu-HU source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../README.md)

# MAKRIVO

[Teljes dokumentáció mind a 27 nyelven](README.md) · [Teljes felhasználói kézikönyv](HANDBOOK.md)

**Készítette: Aivana.** [Aktuális funkciójegyzék és kiadási korlátok](FEATURES.md) · [Bővítményteszt-jelentés](testing/2026-10-12-six-extensions.md) · [Helyi AI-asszisztens prototípusa](testing/2026-10-12-local-meeting-assistant.md)

**Angol (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Helyi Windows-gyorsbillentyűvezérlő Stream Deck stílusú csempékkel, profilokkal, makrókkal és billentyűzet-felismeréssel. A felület 27 nyelvet támogat. A mindennapi használathoz nem szükséges fiók vagy felhőkapcsolat.

[Dokumentáció](README.md) · [Ellenőrzés](VERIFICATION.md) · [Funkcióteszt-jelentés](testing/2026-10-09-usp-release.md)

## Első lépések

Publikálás után az alkalmazás helye `artifacts/win-x64/MAKRIVO.exe`. Tartsa együtt a teljes kimeneti mappát; máshová is másolhatja. A kezdeti célkörnyezet Windows 11 x64. A buildtermékek ki vannak zárva a Gitből; a forráskód helyi másolata nem tartalmaz előre elkészített futtatható fájlt.

A forrásból történő buildeléshez és futtatáshoz telepítse a `global.json` fájlban megadott .NET 10 SDK-t:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publikáljon önálló buildet, amely nem igényel külön telepített .NET futtatókörnyezetet:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Alapvető munkafolyamat

1. Válasszon profilt, és hozzon létre műveletcsempét.
2. Állítsa be a nevét, ikonját, színét és gyorsbillentyűjét. A Ctrl/Alt/Shift kombinációk csökkentik a gépelés közbeni véletlen aktiválást.
3. Adjon hozzá egy vagy több lépést: alkalmazások, fájlok, webhelyek, szöveg, gyorsbillentyűk, média, hangerő, profilváltás vagy késleltetések.
4. Mentsen. A módosítások mentésig vázlatok maradnak; az elvetés visszaállítja a mentett verziót. A csempe lejátszásgombja és gyorsbillentyűje ugyanazt a végrehajtási útvonalat használja.
5. A csempéket és makrólépéseket fogd és vidd módszerrel vagy nyílgombokkal rendezheti át. A sablonok egyszerűsítik a beállítást. A makrók legfeljebb 100 alkalommal ismétlődhetnek. Megszakítás a felületről, a tálcamenüből vagy alapértelmezés szerint a Ctrl+Alt+Escape billentyűkkel.

Az alkalmazásspecifikus profilok folyamatnevet használnak a teljes elérési út nélkül. Több egyezés esetén a profilok sorrendje dönt; az alapértelmezett profil a tartalék. Az automatikus profilválasztást a Beállításokban kell engedélyezni.

A főablak bezárása alapértelmezés szerint a tálcán hagyja az alkalmazást. Újranyitáshoz kattintson duplán a tálcaikonra; kilépéshez használja a tálcamenüt. A Windows rendszerrel történő indítás opcionális és alapértelmezés szerint letiltott. A gyorsbillentyűk központilag szüneteltethetők.

## Műveletek és parancspaletta

A Művelet hozzáadása kereshető katalógust nyit 90 előbeállítással, nyolc kategóriában. Mindegyikhez leírás és előnézet tartozik. A makrószerkesztő ugyanebből a katalógusból ad hozzá vagy cserél lépéseket. Ebben a szerkesztési munkafolyamatban az előbeállítás kiválasztása végrehajtás nélkül hoz létre vázlatot.

A parancspaletta a fejlécből vagy az alkalmazáson belül a Ctrl+K billentyűkkel nyílik meg. A profilok mentett műveletei, katalógus-előbeállítások, profilváltás, navigáció, szüneteltetés/folytatás, makrómegszakítás és import/export között keres. Szűrhet mentett műveletekre, katalógusfunkciókra vagy alkalmazásparancsokra. A nyílbillentyűk választanak, az Enter aktivál, a Ctrl+Enter szerkeszti a szerkeszthető bejegyzést, az Escape bezár. A kedvencek jelennek meg először. A kész katalógusparancsok azonnal futhatnak; a beállítást igénylő előbeállítások vázlatot nyitnak. A Beállításokban globális palettagyorsbillentyű adható meg. Új konfigurációknál ez Ctrl+Shift+Space; a meglévő konfigurációk nem kapnak automatikusan új globális gyorsbillentyűt.

A hangműveletek pontosan 0 és 100 százalék közötti hangerőt állítanak be, és némítják a kimeneti/mikrofoneszközöket, feloldják vagy átváltják a némítást. Válasszon konkrét végpontot, vagy hagyja üresen a Windows alapértelmezéséhez. Az elérhetetlen mentett végpont hibát okoz másik eszköz vezérlése helyett. Az eszközfrissítés újratölti a csatlakoztatott eszközök listáját.

Az OBS Studio WebSocket v5 protokollt használ: felvétel indítása/leállítása/átváltása/szüneteltetése/folytatása; közvetítés indítása/leállítása/átváltása; jelenetváltás; bemenetnémítás átváltása. Engedélyezze a WebSocket kiszolgálót az OBS Eszközök menüjében, és állítsa be a végpontját a szerkesztőben. A kapcsolatellenőrzés és névbetöltés az OBS verzióját, jeleneteit és bemeneteit olvassa be felvétel vagy közvetítés indítása nélkül. Válasszon neveket a listából, vagy írja be kézzel. A jelszavak a Windows hitelesítőadat-kezelőjében menthetők, és kimaradnak az exportált konfigurációkból. Az elérhetetlen OBS-kapcsolatok hibát jeleznek.

A profilok folyamatnévre és ablakcímrészletre illeszthetők. Mindkét megadott feltételnek egyeznie kell. A profil zárolása megakadályozza az automatikus váltást a munkamenetben. Az előtérből történő feltételrögzítés az utolsó fókuszált külső alkalmazást használja.

A szöveg- és vágólapműveletek támogatják a `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` és `{{user}}` változókat. A dátumformátum `yyyy-MM-dd`; az időformátum `HH:mm`. Az ismeretlen helyőrzők változatlanok. A `{{profile}}` az indított művelet profilját azonosítja; a `{{process}}` a külső alkalmazás kontextusát. A vágólaptartalom csak az adott helyőrző használatakor kerül olvasásra. A vágólapművelet szöveget ír, de nem illeszti be automatikusan.

Az ablakműveletek minimalizálják, maximalizálják, visszaállítják az ablakot, vagy a monitor munkaterületének bal/jobb oldalára rendezik. A rendszerművelet zárolja a Windows rendszert. Ezek a műveletek csak kifejezett meghíváskor futnak.

Az előnézet végrehajtás nélkül mutatja a lépéseket, ismétléseket és változókat. A visszavonás és újraalkalmazás legfeljebb 100 vázlatállapotot őriz meg; a szövegmezők megtartják a natív Ctrl+Z szerkesztést. Jelöljön kedvenceket a szerkesztőben vagy a csempemenüben, és szűrje őket az irányítópulton. A végrehajtásszámok csak az aktuális munkamenetre vonatkoznak.

A felületről indított szöveg-, gyorsbillentyű- és ablakműveletek aktiválják az utolsó külső célablakot. A paletta mutatja ezt a célkontextust. Ha a cél bezárult vagy a Windows elutasítja az aktiválást, a végrehajtás üzenettel leáll.

## Billentyűzetsegítség és visszajelzés

A speciális billentyűk varázslója a Billentyűzet oldalon és a palettán található. Felsorolja az iCUE által jelentett G-billentyűket. A billentyűlenyomás észlelése ideiglenesen rögzíti őket a hozzárendelt műveletek végrehajtása nélkül. A normál hozzárendelések az észlelés, megszakítás vagy bezárás után visszatérnek. A katalógusfunkció és a kiválasztott billentyű nem mentett vázlatként nyílik meg; a duplikált hozzárendelések ellenőrzésre kerülnek.

A billentyűzetfedvény a kiválasztott referenciakiosztást és aktív hozzárendeléseket mutatja. Tartsa nyomva a Ctrl+Alt+F1 billentyűket, vagy kapcsolja át a palettáról vagy a Billentyűzet oldalról. Nem veszi át a fókuszt, és átengedi az egérkattintásokat. A profil- és hozzárendelés-változások látható állapotban frissülnek; a normál Windows-billentyűlenyomások kiemelve jelennek meg. A gyártóspecifikus vezérlők nem feltétlenül adnak egyenértékű Windows-billentyűjelet.

A végrehajtási visszajelzés várakozást, lépés-/ismétlés-előrehaladást, befejezést, megszakítást vagy hibát mutat a fókusz átvétele és a bevitt szöveg felfedése nélkül. A végrehajtott parancsok jelzése a parancs végrehajtását igazolja, nem egy harmadik fél alkalmazásában külön lekérdezett élő állapotot. A hibarészletek a tevékenységnaplóban jelennek meg.

## Eszközök és Corsair

A Corsair G1–G6 közvetlenül hozzárendelhető a hivatalos iCUE SDK-n keresztül. Válassza ki az eszközt és billentyűt a műveletszerkesztőben, majd mentsen, vagy készítsen vázlatot a K100 billentyűzetnézetből. A hozzárendelések a kiválasztott eszközre és aktív profilra érvényesek; a szüneteltetés ezeket is letiltja. Az iCUE-nak futnia kell és engedélyeznie kell a hozzáférést. Az Fn, az iCUE görgő, a profil- és zárolásvezérlők nem rendelhetők hozzá ezen a G-key API-n keresztül.

Az Eszközök oldal felsorolja a Windows billentyűzetinterfészeit, és időszakosan, valamint eszközcsatlakozáskor frissül. Egy fizikai billentyűzet több HID-interfészt biztosíthat. A szokásos globális gyorsbillentyűk gyártókon át működnek.

A Corsair felismerése az elérhető eszközinformációkat használja. Az RGB-vezérlés kompatibilis hardvert, futó iCUE-t és hivatalos iCUE SDK-t igényel; az alkalmazás a tényleges elérhetőséget jelzi. Más gyártóknál jelenleg felismerés és szokásos műveletek állnak rendelkezésre. A Corsair G-key integráción kívül a gyártóspecifikus vezérlők akkor működnek, ha a Windows normál bevitelként fogadja őket. Az alkalmazás nem telepít billentyűzet-illesztőprogramot.

## Nyelvek

A Beállítások 27 nyelvet kínál: német, angol, francia, spanyol, olasz, portugál, holland, lengyel, cseh, szlovák, magyar, román, bolgár, görög, dán, svéd, norvég, finn, észt, lett, litván, szlovén, horvát, ukrán, orosz, török és latin betűs szerb. Az automatikus választás a Windows megjelenítési nyelvét követi. A fordítások helyileg mellékeltek. A kiosztások, gyorsbillentyűk és felhasználói nevek függetlenek a felület nyelvétől.

A tároló teljes dokumentációja az alkalmazás mind a 27 nyelvén elérhető itt: [nyelvi index](README.md). A fordítások gépi segítséggel készültek, eredetiforrás-hivatkozásokkal és szerkezeti ellenőrzéssel; az anyanyelvi lektorálás külön történik. Az alkalmazás a `en-US`, `de-DE`, `fr-FR` és `it-IT` regionális kódokat is elfogadja, és azok számformátumát használja. A meglévő nyelvkódok kompatibilisek maradnak.

## Kiosztások és gyártói kutatás

A billentyűzetnézet teljes méretű és numerikus blokk nélküli ISO-DE/ANSI-US referenciákat, valamint Corsair K100 RGB kiosztásokat biztosít. A geometria a pozíciókat, széles/magas billentyűket és az ISO Entert ábrázolja. A szabványos kiosztások általános referenciák. A K100 kiosztások dokumentált vezérlőket követnek, milliméterpontos méretek állítása nélkül. Válassza a megfelelő nyelvi/geometriai változatot: a modellnév vagy USB VID/PID nem igazol konkrét változatot.

A hozzárendelés az aktív Windows beviteli kiosztással értelmezi a fizikai scan-kódokat. A munkafolyamat vázlatot készít, és megtartja a mentési/ütközési ellenőrzéseket. A szabványos hozzárendelések globális Windows-gyorsbillentyűk, nem korlátozódnak egy csatlakoztatott billentyűzetre. A Corsair G1–G6 eszközspecifikus iCUE-integrációt használ. Más gyártóspecifikus vezérlőkhöz adapter vagy gyártó által konfigurált gyorsbillentyű kell. A fő Enter és a numerikus blokk Enterje azonos virtuális billentyűazonosítót használ a globális gyorsbillentyűknél.

A [gyártói kutatás](research/tastaturhersteller-sonderfunktionen.md) és [CSV](research/tastaturhersteller-sonderfunktionen.csv) 74 márka-/gyártócsoportot fed le, megkülönböztetve a konfigurátorokat, beviteli SDK-kat és világítási API-kat. [Kiosztási bizonyítékok](research/keyboard-layout-evidence.md) rögzíti a modell-, SKU- és modulkülönbségeket. A kutatási lefedettség nem jelenti a hardvertámogatás megvalósítását.

## Adatok

A konfiguráció a helyi alkalmazásadatokban tárolódik. Az import/export JSON-t használ. Az import előnézetet mutat, megerősítés után lecseréli a jelenlegi konfigurációt, és kézi engedélyezésig letiltja az importált hozzárendeléseket. A szövegműveletek tartalmát nem gyűjti billentyűleütési naplóként. A hitelesítőadat-kezelő jelszavai elkülönülnek az exportált profiloktól.

## Ellenőrzés

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

A Windows-ellenőrzések vizsgálhatják a hardvert, és ideiglenesen konfigurálhatják/felszabadíthatják a támogatott iCUE-hozzárendeléseket. Más, ezeket használó vezérlő nélkül futtassa őket. A hangvégpont-ellenőrzések nem változtatják meg az élő hangerő- vagy némításbeállításokat.

A natív bevitel nem kerülheti meg a Windows jogosultságait. A normálisan indított vezérlő nem feltétlenül küldhet bevitelt rendszergazdai alkalmazásnak. A fenntartott vagy foglalt gyorsbillentyűk ütközésként jelennek meg.

A [dokumentációs index](README.md) korábbi tervekre, kutatásokra és dátumozott tesztjelentésekre hivatkozik. A korábbi tervek a rögzített hatókörüket írják le, nem garantálják az aktuális funkciótámogatást.

### Munkafolyamat-sablonok és célablakra várakozás

A katalógus hat szerkeszthető munkafolyamatot tartalmaz: munkaterület, értekezlet, közvetítés, kutatás, projektváltás és szövegrészlet. Keressen a `workflow.` kifejezésre a katalógusban vagy parancspalettán. A munkafolyamat kiválasztása nem mentett vázlatot nyit; soha nem fut azonnal. Minden lépés megmarad meglévő makróhoz adáskor vagy Corsair-billentyűhöz rendeléskor. Mentés előtt állítsa be a példa-URL-eket, programútvonalakat, profilokat és OBS-jeleneteket.

A **Várakozás célablakra** lépés folyamatnevet (`.exe` utótaggal vagy anélkül), kis- és nagybetűtől független ablakcímrészletet és 1–600000 ms időkorlátot fogad el. Legalább egy szűrő kell; ha mindkettő megadott, mindkettőnek egyeznie kell. A makró csak akkor folytatódik, ha az illeszkedő látható ablak válaszol és előtérbe kerül. Használjon konkrét címet, ha több ablak tartozik egy folyamathoz. A megszakítás vagy időtúllépés leállítja a további lépéseket. Az ablak készenléte nem garantálja a dokumentum betöltését vagy egy külső szolgáltatás, például az OBS WebSocket kapcsolódását.

Kijelzőfényerő: keresse a **Képernyő fényerejének növelése**, **Képernyő fényerejének csökkentése** vagy **Képernyő fényereje 50 százalék** műveletet a katalógusban. A relatív műveletek 10 százalékponttal változtatják a fényerőt; a rögzített értékek 0 és 100 közöttiek. A támogatott belső kijelzők WMI-t használnak; a kompatibilis külső monitorok menüjében engedélyezni kell a DDC/CI-t. Alapértelmezés szerint a művelet minden támogatott kijelzőre hat; a szerkesztőben konkrét monitor választható. A csatlakozások és dokkolók korlátozhatják a támogatást.

### Kiterjesztett vezérlők

Nyissa meg a parancspalettát a **Ctrl+K billentyűkkel, amikor az alkalmazás ablaka van fókuszban**. Keresse a **Munkafolyamat-tervező**, **Makró rögzítése**, **Profilok szinkronizálása** vagy **Billentyűdiagnosztika** elemet (a megnevezés a kiválasztott nyelvet követi). A munkafolyamat-tervező a kiválasztott csempevázlatot szerkeszti: fájl-/folyamat-/ablakfeltételek, előre kihagyandó lépések száma és leállítás/folytatás hibakezelés. A rögzítő billentyűkombinációkat és várakozásokat rögzít, nem szokásos gépelést; az Escape leállítja a rögzítést. Átvétel előtt ellenőrizze az eredményt.

A fényerőlépésben válassza az összes támogatott kijelzőt vagy egy konkrét monitort. Az elérhetetlen kiválasztott monitor hibát okoz, ahelyett hogy észrevétlenül másik kijelzőt változtatna. Az irányítópult élő fényerő-, hang- és OBS-adatokat mutat, ha az adott integráció elérhető; az elérhetetlen adat nem jelenik meg mért értékként.

A csempeszerkesztőben rendeljen hozzá támogatott fizikai speciális billentyűt, és válasszon azonnali lenyomást, rövid lenyomást, nyomva tartást vagy dupla lenyomást. A nyomva tartott rétegválasztók ideiglenesen másik hozzárendelést aktiválnak, felengedéskor visszaállítják az előző réteget. A gesztusok és rétegek a Corsair SDK G-billentyűire és eszközspecifikus szabványos speciális billentyűkre érvényesek, nem a szokásos globális gyorsbillentyűkre. A szabványos billentyűrögzítés támogatja az F13–F24 és médiabillentyűket. A gyártóspecifikus Fn-, profil- és tárcsavezérlőkhöz továbbra is kompatibilis gyártói adapter vagy szabványos billentyűre történő gyártói hozzárendelés kell. A Raw Input megfigyeli a billentyűt, és nem nyomja el szokásos Windows-viselkedését. A billentyűdiagnosztika megmagyarázza a profil-, szüneteltetési, kapcsolati és hozzárendelési problémákat.

A profilszinkronizálás a választott mappán keresztül cserél fájlokat, beleértve a meglévő felhőkliens által kezelt mappát. Importálás előtt ellenőrizze az előnézetet; az importált hozzárendelések letiltottan indulnak, a hardverspecifikus hozzárendelések pedig törlődnek. A revíziók megakadályozzák az észrevétlen felülírást, a művelettartalom bevonása kifejezett választás. A MAKRIVO nem biztosít felhőfiókot és nem őriz felhős hitelesítő adatokat. A tálca most a MAKRIVO saját türkiz M ikonját használja, szüneteltetéskor borostyánszínű szünetjelzéssel.

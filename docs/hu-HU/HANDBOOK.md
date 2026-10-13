<!-- generated-doc locale=hu-HU source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../HANDBOOK.md)

# MAKRIVO felhasználói kézikönyv

Gyártó: **Aivana**. Dokumentációs pillanatkép: **2026-10-12**. Ez a kézikönyv a munkafa jelenlegi megvalósítását írja le. A kísérleti integrációkat és a hardverfüggő funkciókat külön jelöljük. A korábbi jelentések a saját dátumozott állapotukat írják le, nem garantálják az újabb buildek működését.

## Dokumentáció és nyelvek

A dokumentáció nyelvválasztójával a teljes kézikönyv, funkciójegyzék, hibaelhárítás, fejlesztési jegyzetek, kutatás és korábbi tesztjelentések megnyithatók az alkalmazás 27 nyelvének bármelyikén. A generált fordítások gépi segítséggel készültek, és szerkezeti ellenőrzésen estek át; nem mindegyiket lektorálta anyanyelvi beszélő. A technikai azonosítók és futtatható példák változatlanok. Az eredeti forrás összehasonlításra továbbra is elérhető. A lefordított licencelemzés tájékoztató jellegű, és nem helyettesíti a harmadik felek eredeti jogi feltételeit vagy a végleges EULA-t.

## Beállítás és első indítás

A kezdeti célkörnyezet Windows 11 x64. Tartsa együtt a terjesztési csomag összes fájlját és alkönyvtárát. Indítsa el a `MAKRIVO.exe` fájlt. A forráskód helyi másolata nem telepítő. Az önálló csomagok tartalmazzák a .NET futtatókörnyezetet; a keretrendszerfüggő csomagokhoz a megfelelő .NET 10 Desktop és ASP.NET futtatókörnyezet szükséges. Az ONNX és a kísérő hosztfolyamatok a megfelelő funkciók használatához szükségesek a csomagban.

1. Indítsa el a MAKRIVO alkalmazást, és válassza ki a felület nyelvét a Beállításokban. Az automatikus beállítás a Windows nyelvét követi. A billentyűzetkiosztás külön beállítás.
2. Válasszon profilt, majd adjon hozzá csempét a kereshető műveletkatalógusból. Adjon neki felismerhető nevet, és válassza ki a műveleteit.
3. Ellenőrizze a lépéseket, és szükség esetén rendeljen hozzá gyorsbillentyűt. Kerülje a Windows vagy a célalkalmazás által már használt kombinációkat.
4. Mentse a vázlatot. A nem mentett módosítások nem részei a mentett konfigurációnak.
5. Teszteljünk egy ártalmatlan akcióval, például egy dokumentum megnyitásával, és ellenőrizzük a végrehajtás visszajelzését.

A főablak bezárása általában az értesítési területen hagyja az alkalmazást. Dupla kattintással nyissa meg újra a tálcaikonról, vagy a tálcamenüből lépjen ki. A Windows rendszerrel való indítás opcionális és kezdetben letiltott. A szüneteltetés felfüggeszti a hozzárendeléseket; az ikon szünetjelzést mutat. A futó makrókat a felületről, a tálcáról vagy a beállított megszakító gyorsbillentyűvel szakíthatja meg; az alapértelmezés `Ctrl+Alt+Escape`.

## Csempék, műveletek és parancspaletta

A csempék profilokhoz tartoznak, és több sorrendbe rendezett lépést tartalmazhatnak. A szerkesztő támogatja a mentést/elvetést, előnézetet, visszavonást/újraalkalmazást és a lépések átrendezését. A katalógus előbeállításokat biztosít programokhoz, dokumentumokhoz, URL-ekhez, szöveghez, vágólaphoz, gyorsbillentyűkhöz, médiához, hanghoz, ablakokhoz, rendszerműveletekhez és támogatott integrációkhoz. A paramétert igénylő előbeállítás vázlatként nyílik meg.

Nyissa meg a parancspalettát a `Ctrl+K` billentyűkkel, amikor a MAKRIVO van fókuszban. Keressen mentett műveleteket, előbeállításokat, profilváltást és eszközöket. A nyílbillentyűk választanak; az Enter aktivál; a `Ctrl+Enter` szerkeszti a támogatott elemet; az Escape bezár. Egyes kész parancsok azonnal futnak, míg a konfigurált műveleteket ellenőrizni kell. Külön globális palettagyorsbillentyű állítható be; új konfigurációknál ez `Ctrl+Shift+Space`.

A szöveg- és vágólaplépések támogatják a `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` és `{{user}}` változókat. A dátumformátum `yyyy-MM-dd`; az időformátum `HH:mm`. Az ismeretlen változók változatlanok maradnak. A vágólap olvasása csak kérésre történik. A vágólapművelet szöveget ír a vágólapra, de nem illeszti be automatikusan.

A felületről indított műveletek az utolsó külső alkalmazást célozhatják. Ha a cél eltűnik vagy a Windows blokkolja az előtérbe hozást, aktiválja kézzel a célt, és próbálja újra. A billentyűk sikeres továbbítása nem bizonyítja a célfeladat befejezését.

## Profilok és munkahely-beállítás

A profilok folyamatnévre és ablakcímrészletre illeszthetők. Ha mindkettő meg van adva, mindkettőnek egyeznie kell. Több egyezés esetén a profilok sorrendje dönt; egyezés hiányában az alapértelmezett profil érvényes. Az automatikus kiválasztást külön engedélyezze. A profil zárolása az aktuális munkamenetben megakadályozza az automatikus váltást.

Nyissa meg a munkahely-beállító varázslót az Eszközök menüből vagy a palettáról. Válasszon programokat, dokumentumokat, webhelyeket, hang-/kijelzőcélt vagy OBS-jelenetet. A varázsló munkahelyi, értekezleti, közvetítési, kutatási vagy projektprofilokat készít elő. Mentés előtt ellenőrizze a generált lépéseket. A beállítás nem hajtja végre az előkészített műveleteket.

## Makrók, feltételek és rögzítés

A makrók sorrendben hajtják végre lépéseiket, és támogatják a várakozást, ismétlést és megszakítást. Az általános ismétlési korlát 100. Használjon előnézetet a másik alkalmazásba gépelő vagy rendszerállapotot módosító műveletek futtatása előtt.

A célablakra várakozás folyamatszűrőt, az ablakcím részletét és 1 és 600000 ms közötti időkorlátot fogad el. Legalább egy szűrő szükséges. Az illeszkedő látható ablaknak válaszolnia kell, és előtérbe kell kerülnie, mielőtt a következő lépések folytatódnak. Az ablak készenléte nem bizonyítja a dokumentum vagy szolgáltatás betöltésének befejezését.

A munkafolyamat-szerkesztőben a fájl létezésére, a folyamat futására és az ablak címére vonatkozó feltételek adhatók meg. A hamis feltétel kihagyja a lépést, és további későbbi lépéseket is kihagyhat. Alternatív ág kifejezéséhez tagadja a feltételt. Tudatosan válasszon a hibánál leállítás és a hiba utáni folytatás között; folytatáskor a későbbi lépések szükséges előfeltételei hiányozhatnak.

A műveletrögzítő billentyűkombinációkat és várakozásokat rögzít, nem szokásos gépelést. Támogatja a módosítóbillentyűs kombinációkat és a funkcióbillentyűket. Az Escape leállítja a rögzítést. Ellenőrizze a listát, majd vegye át nem mentett csempevázlatként, és külön mentse el. A felület kattintásainak rögzítése külön eszköz, amelyet alább ismertetünk.

## Billentyűzetek, gesztusok és rétegek

A billentyűzetoldal felsorolja az észlelt eszközinterfészeket és a referenciakiosztásokat. Egy fizikai billentyűzet több interfészt is biztosíthat. Válassza ki a megfelelő nyelvet és geometriát; a terméknév önmagában nem határozza meg az ISO vagy ANSI kiosztást.

A szokásos globális gyorsbillentyűk minden billentyűzetre érvényesek. Az eszközspecifikus Raw Input hozzárendelések egy adott eszközt és scan-kódot figyelnek, beleértve a támogatott funkció- és médiabillentyűket. **A Raw Input nem nyomja el a szokásos Windows bevitelt. A kizárólagos második billentyűzet nem kész éles használatra.** Továbbra is szükséges egy aláírt, licencelt és tesztelt szűrőillesztőprogram-háttérrendszer; a MAKRIVO ilyet nem telepít.

A támogatott speciális billentyűhozzárendelések azonnali lenyomást, rövid lenyomást, nyomva tartást vagy dupla lenyomást használhatnak. A nyomva tartott rétegváltó billentyű ideiglenesen másik kiosztást választ, felengedéskor pedig visszaállítja az alapréteget; a rétegválasztó billentyűk nem futtatják saját makrójukat. A nyomva tartás küszöbe 150 és 5000 ms közötti, a dupla lenyomás időablaka 150 és 1000 ms közötti. Ezek a hozzárendelések különböznek a szokásos globális gyorsbillentyűktől.

A speciális billentyűk varázslója felismeri a kompatibilis billentyűket anélkül, hogy rögzítés közben végrehajtaná a hozzájuk rendelt műveleteket. A diagnosztika megmagyarázza az elérhetetlen eszközöket, szüneteltetett hozzárendeléseket, profileltéréseket és ütközéseket. A fedvény megjeleníti a kiválasztott kiosztást és hozzárendeléseket; az alapértelmezett nyomva tartandó gyorsbillentyű `Ctrl+Alt+F1`.

| integráció | Kötelezettségek és korlátozások |
| --- | --- |
| Corsair | Futó iCUE, SDK-hozzáférés és kompatibilis modell. G1–G6 és támogatott RGB-funkciók; a gyártóspecifikus Fn-, görgő-, zárolási és profilvezérlők általában nem érhetők el a G-key API-n keresztül. |
| Logitech G | Az eredeti helyi x64 G-key/LED SDK-könyvtárakhoz kifejezett jóváhagyás szükséges. A G-key események nem azonosítanak egyértelműen több azonos fizikai billentyűzetet. |
| Razer | Chroma RGB és szokásos bevitel; szükség esetén rendelje hozzá a gyártóspecifikus vezérlőket a Synapse alkalmazásban. |
| SteelSeries | GameSense RGB és szokásos bevitel; szükség esetén rendelje hozzá a gyártóspecifikus vezérlőket a GG alkalmazásban. |
| Wooting | Kompatibilis eredeti helyi x64 SDK az analóg értékekhez és küszöbökhöz; a normál HID-bevitel aktív marad. |
| Egyéb billentyűzetek | Szokásos gyorsbillentyűk vagy támogatott fizikai scan-kód-hozzárendelések. Az Fn és a gyártóspecifikus vezérlők a modelltől és a gyártói konfigurációtól függenek. |

A gyártói kutatás dokumentált képességeket ír le, nem minden felsorolt eszközzel megvalósított vagy tesztelt kompatibilitást. Az új adapterekhez továbbra is valódi hardveres átvételi teszt szükséges. Ne töltsön le SDK-binárisokat nem megbízható forrásból; az SDK terjesztési jogai külön kérdést jelentenek a technikai kompatibilitástól.

## Audio, kijelzők és OBS

A hangműveletek 0 és 100 százalék közötti hangerőt állítanak be, és a támogatott kimeneti vagy mikrofon-végpontokat némítják, feloldják a némítást, illetve átváltják annak állapotát. Válasszon konkrét végpontot vagy használja a Windows alapértelmezését. A hiányzó mentett cél hibát okoz, ahelyett hogy észrevétlenül másik eszközt vezérelne. Újracsatlakoztatás után frissítse az eszközlistát. A végpontvezérlés nem jelent önálló alkalmazásonkénti keverőtámogatást.

A kijelzőműveletek 10 százalékponttal növelik vagy csökkentik a fényerőt, illetve 0 és 100 közötti értéket állítanak be. A belső kijelzők támogatott WMI interfészeket használnak; a külső monitorokhoz DDC/CI támogatás kell, amelyet általában a monitor menüjében is engedélyezni kell. Dokkolók, adapterek és kábelek blokkolhatják a vezérlést. Válassza az összes támogatott kijelzőt vagy egy monitort. Újracsatlakoztatáskor az elérhető azonosítók változhatnak; ellenőrizze a mentett célt. A hiányzó leolvasások nem elérhetőként jelennek meg, nem mért értékként.

Az OBS használatához engedélyezni kell a WebSocket v5 szolgáltatást az OBS Eszközök menüjében. Állítsa be a végpontot, tesztelje a kapcsolatot, és mentés előtt töltse be a jelenetek/bemenetek neveit. A támogatott műveletek közé tartozik a jelenetváltás, a közvetítés/felvétel indítása, leállítása és átváltása, a felvétel szüneteltetése/folytatása és a bemenet némítása. A jelszavak a Windows hitelesítőadat-kezelőjében tárolhatók, és kimaradnak a konfigurációexportból. Egy sikeres OBS-parancs nem igazolja a közvetítési lánc összes további hatását.

## Eredményellenőrzés és végrehajtási előzmények

A megfelelő lépéseknél engedélyezze az eredmény ellenőrzését. Az alkalmazás megkülönbözteti az előzetes ellenőrzéseket, az igazolt hatásokat, a hibákat, a megszakítást és a nem ellenőrizhető hatásokat. A folyamat létezése nem jelenti az alkalmazás készenlétét. A relatív változások, gyorsbillentyűk és szövegbevitel nem kaphatnak általános sikervisszaigazolást. Hibánál ellenőrizze a tevékenységnaplót és az ellenőrzési előzményeket. A teljes makró újrafuttatása előtt javítsa ki a konfigurációs hibákat.

## Felületautomatizálás és kattintásrögzítés

Adjon hozzá UI Automation lépést, válassza ki a műveletet, majd a kijelzett késleltetés után rögzítse a mutató alatti célt. A cél rögzítése önmagában nem hajt végre semmit. Ellenőrizze a szelektort, és követeljen meg egyetlen egyértelmű találatot. Támogatott műveletek: `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` és `WaitState`. A jelszómezők, letiltott vezérlők és nem egyértelmű szelektorok elutasításra kerülnek.

A felületautomatizálás korlátozott kísérő folyamatban fut. A célalkalmazás Windows akadálymentesítési támogatásától és a jogosultságoktól függ. A kattintásrögzítő támogatott műveleteket és szüneteket rögzít, általános szövegbevitelt nem. Átvétel előtt ellenőrizze és érvényesítse a vázlatot. A külső alkalmazás felületének változásai érvényteleníthetik a szelektorokat. A folyamatizoláció korlátozza az összeomlások és a megszakítás hatásait, de nem biztonsági sandbox.

## Bővítmények

Az importálás előtt nyissa meg a bővítménykezelőt és vizsgálja meg a csomagot. Ellenőrzésre kerül a csomagszerkezet, a jegyzék és a hashértékek. Az importált bővítmény kezdetben letiltott. Olvassa el a deklarált műveleteket, és engedélyezés előtt kifejezetten jelölje megbízhatónak a kiadót. A bővítmények az Ön felhasználói jogosultságaival futnak külön folyamatban; ez a folyamat **nem sandbox**. Csak megbízható csomagot telepítsen.

A kezelő támogatja az exportálást, letiltást, eltávolítást és egy korábbi verzió visszaállítását vagy elvetését. A hiányzó függőségek blokkolják a meghívást. A példacsomag visszaszámlálási és helyi állapotkezelő műveleteket biztosít. A támogatott séma a JSON-Schema dokumentált részhalmaza, nem az összes lehetséges JSON-Schema funkció. Nincs nyilvános bővítménypiactér.

## Mobil panelek

1. Nyissa meg a mobilpanelt, válasszon helyi IP-címet, és kifejezetten jelölje ki a megosztandó profilokat. Az alapértelmezett loopback-cím csak magán a PC-n érhető el; telefonhoz vagy tablethez válassza a megfelelő LAN-címet.
2. Indítsa el a panelt, és olvassa le a HTTPS-címét, a tanúsítvány ujjlenyomatát és az egyszer használatos, hatjegyű párosítási kódot.
3. Az ugyanazon megbízható hálózaton lévő eszközön nyissa meg a címet. A helyi tanúsítvány megbízhatónak jelölése előtt hasonlítsa össze a SHA-256 ujjlenyomatot megbízható csatornán. Az alkalmazás nem telepít automatikusan megbízható tanúsítványt.
4. Párosítson a kijelzett kóddal, és teszteljen egy ártalmatlan csempét. LAN-hozzáféréshez kézi Windows tűzfalengedélyre lehet szükség. Ne tegye elérhetővé a portot az internet felől.
5. Ha végzett, állítsa le a panelt, vagy vonja vissza a párosított kliensek hozzáférését az alkalmazásban.

A kiszolgáló privát kulcsait a Windows DPAPI védi; a kliens hitelesítő adatai hashként tárolódnak. Az IP módosítása vagy a tanúsítvány megújítása új bizalmi jóváhagyást igényelhet. A panel művelet- és ellenőrzési állapotokat mutat, nem minden lehetséges élő érzékelőt. A böngészőelrendezés és a helyi HTTPS ellenőrzése nem helyettesíti a valódi telefonos/tabletes LAN átvételi tesztet.

## Helyi AI-értekezletasszisztens

Ez az opcionális kísérleti asszisztens alapértelmezés szerint le van tiltva. Nyissa meg az Eszközök menüből, külön engedélyezze, válasszon futtatókörnyezetet és modellt, majd csak a figyelembe venni kívánt erőforrásokat adja hozzá. Fogalmazza meg a célt, készítsen vázlatot, vizsgálja meg a megoldatlan kéréseket, és vigye át az eredményt a szokásos szerkesztőbe. Semmi sem fut automatikusan; a mentés és futtatás külön művelet marad. Nincs automatikus áttérés másik futtatókörnyezetre.

| Futtatókörnyezet | Beállítás |
| --- | --- |
| Ollama | Külön helyi szolgáltatás és egy helyi telepített modell a `127.0.0.1:11434`. A kijelölt távoli/felhő modellek elutasítják. |
| LM Studio | Helyi kiszolgáló helyileg betöltött modellel és JSON-sématámogatással a `127.0.0.1:1234` címen. Helyi feldolgozáshoz ne engedélyezze a távoli útválasztást. |
| llama.cpp | Helyi `llama-server` és kompatibilis, JSON-sémát támogató modell a `127.0.0.1:8080` címen. |
| ONNX Runtime GenAI CPU | Megbízható, kompatibilis helyi GenAI-szövegmodellmappa; válassza a `genai_config.json` fájlt. A következtetés a mellékelt kísérő folyamatban, HTTP nélkül fut. |

A HTTP-kliensek csak loopback-címeket használnak, nem használnak proxyt, és nem követnek átirányításokat. A MAKRIVO nem tudja megbízhatóan megállapítani, hogy egy kompatibilis helyi kiszolgáló belsőleg távoli szolgáltatást hív-e. Hitelesítést igénylő helyi kiszolgálók ebben a verzióban nem konfigurálhatók. A modellsúlyok, a Python és a helyi következtetési kiszolgálók nem töltődnek le és nem települnek automatikusan.

Az ONNX csomagokhoz `tokenizer.json` és érvényes csevegősablon szükséges. A `tokenizer_config.json` támogatott sablonformái: szöveges `chat_template`, szöveges `default` értéket tartalmazó szótár, vagy egyértelmű névvel/alapértelmezéssel rendelkező sablonlista. Ha a sablon hiányzik vagy null, tartalékként a `chat_template.jinja` használatos. Az üres, többértelmű, túlméretes vagy hibás típusú sablonok elutasításra kerülnek. Tetszőleges ONNX fájlok, beágyazási/képfeldolgozó modellek és kizárólag GPU-n futó csomagok nem helyettesítik a kompatibilis GenAI szövegmodelleket. A mellékelt GenAI futtatókörnyezet verziója 0.17.1, CPU. Csak megbízható modelleket használjon; a folyamatok szétválasztása nem sandbox.

Az asszisztens a kiválasztott programokat argumentumok nélkül, a kiválasztott dokumentumokat és webhelyeket, valamint kifejezett kérésre a mikrofon némítását támogatja. Nem hoz létre üzeneteket, időpontokat, tetszőleges szkripteket, szabad kattintásokat vagy letöltéseket, és nem vált headsetet. A cél és az erőforrások neve a kiválasztott helyi futtatókörnyezethez kerül; az erőforrások elérési útja és a fájltartalom nem kerül külön átadásra. A felhasználó által beírt cél önmagában tartalmazhat személyes adatot. A helyi futtatókörnyezet saját hálózati viselkedése továbbra is a felhasználó felelőssége.

Az ONNX kérés időkerete a modell betöltésével együtt 90 másodperc, legfeljebb 3072 bemeneti és 1024 generált tokennel; a modell kontextusmérete szintén korlátozza ezeket. A hibás vagy csonkolt JSON és az ismeretlen erőforrás-azonosítók elutasításra kerülnek. Megszakítással vagy bezárással fejezze be a kérést. A véletlen modellel végzett futtatókörnyezet-teszt a végrehajtást igazolja, nem a tervezés hasznosságát vagy az időmegtakarítást. A helyi kísérleti naplók eseményazonosítókat és időzítési adatokat tartalmaznak, nem célszöveget vagy dokumentumtartalmat; nem töltődnek fel automatikusan.

A kis Instruct-modell későbbi értékelése négy német/angol próbában nulla érvényes tervet eredményezett. A natív következtetés működött, de a modell nem teljesítette a szigorú tervezési szerződést. Ez negatív minőségi eredmény, nem modellajánlás vagy bizonyítottan hasznos asszisztens. A pontos modellrevíziót és korlátokat a dátummal ellátott helyi futtatókörnyezet-tesztjelentés tartalmazza.

## Importálás, exportálás, szinkronizálás és biztonsági mentés

A konfiguráció cseréje előtt ellenőrizze az importálás előnézetét. Az importált műveletek kezdetben letiltottak, a hordozható szinkronizálás pedig eltávolítja a hardverspecifikus hozzárendeléseket. Ellenőrzés után rendelje újra hozzá az eszközöket, és tudatosan engedélyezze a műveleteket. Export megosztása előtt vizsgálja meg a szövegek és műveletek tartalmát; a jelszavak kizárása nem teszi az összes felhasználó által létrehozott művelettartalmat érzéketlenné.

A profilszinkronizálás fájlokat cserél egy kiválasztott mappában, amelyet opcionálisan a meglévő felhőkliens kezelhet. A MAKRIVO nem biztosít felhőfiókot és nem tárol felhős hitelesítő adatokat. A revíziók megakadályozzák az észrevétlen felülírást. A művelettartalom bevonása kifejezett választás. Cserélő importálás előtt készítsen biztonsági mentést a konfigurációról. Az alapértelmezett konfiguráció: `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; a korábbi könyvtárnév a kompatibilitás érdekében megmarad. Kézi másolás vagy visszaállítás előtt zárja be az alkalmazást. Másik PC-n a mentett SDK-jóváhagyásokat és az operációs rendszer által védett hitelesítő adatokat újra létre kellhet hozni.

## Problémák megoldása

| tünetek | Mit kell ellenőrizni |
| --- | --- |
| A gyorsbillentyű nem csinál semmit | Az alkalmazás fusson, a hozzárendelések ne legyenek szüneteltetve, legyen megfelelő az aktív profil, a csempe legyen mentve/engedélyezve; ellenőrizze a gyorsbillentyű-ütközéseket és a cél elérhetőségét. |
| A cél ablak nem aktiválható | Aktiválja kézzel. A Windows előtérkorlátozásai és a jogosultságkülönbségek blokkolhatják az automatizálást. |
| A speciális billentyűt nem ismeri fel | Gyártói szolgáltatás-/SDK-/modelltámogatás; ha lehetséges, konfigurálja a gyártóspecifikus vezérlőket támogatott szabványos billentyű küldésére. |
| A második billentyűzet is normálisan gépel | Raw Input esetén ez várható. A kizárólagos bevitelelnyomás nincs megvalósítva éles használatra. |
| Audio vagy fény nem elérhető | Frissítse az eszközöket; ellenőrizze a kiválasztott azonosítót, végpontmunkameneteket, WMI/DDC/CI támogatást és a dokkoló-/kábelkorlátokat. |
| OBS offline | WebSocket v5 aktiválva, helyes végpont/jelszó, elérhető szolgáltatás és érvényes jelenet/bejárati név. |
| A felületautomatizálás hibás | A kísérő hoszt legyen mellékelve; egyedi érvényes szelektor, hozzáférhető és engedélyezett cél, támogatott vezérlő és megfelelő jogosultságok. Átmeneti időtúllépést csak a cél ellenőrzése után próbáljon újra. |
| A mobilpanel nem érhető el | LAN-cím loopback-cím helyett, azonos hálózat, futó kiszolgáló, jóváhagyott profilok, tanúsítvány-ujjlenyomat/megbízhatóság és kézi tűzfalengedélyek. |
| Plugin hibás | Csomagintegritás, kifejezett bizalom/engedélyezett állapot, függőségek, protokollkompatibilitás és végrehajtási napló. |
| Az AI-kérés sikertelen | Megfelelő kiválasztott helyi futtatókörnyezet, kész szolgáltatás/modell, támogatott JSON-séma, megbízható kompatibilis modellmappa és kérési korlátok. |
| A kiosztás vagy a szöveg hibásnak tűnik | Különítse el a felület nyelvét a billentyűzet geometriájától; méretezze át az ablakot, ellenőrizze a kijelzőskálázást, és jelentse a pontos oldalt, nyelvet és buildet. |

Hasznos hibajelentéshez adja meg a Windows verzióját, az alkalmazás buildjét/commitját, nyelvét, kijelzőskálázását, hardvermodelljét, minimális lépéssorát és a releváns, érzékeny adatoktól megtisztított naplót. Közzététel előtt távolítsa el a személyes elérési utakat, művelettartalmakat és hitelesítő adatokat.

## Buildelés, tesztelés és csomagolás

Használja a `global.json` fájlban rögzített SDK-t. A tároló gyökeréből:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

A Windows integrációs tesztek elkülönített ablakokat nyithatnak, és ideiglenesen konfigurálhatják/felszabadíthatják a támogatott gyártói hozzárendeléseket. Úgy futtassa őket, hogy más vezérlő ne birtokolja ezeket. Az alapértelmezett fényerő-/hangvizsgálatok csak olvasnak; a kifejezett hardveres módosítási/visszaállítási tesztek különállók. A szimulált protokollok, renderelt képernyőképek és véletlen következtetési gráfok nem tüntethetők fel valódi hardveres vagy modellminőségi átvételként.

A teljes csomagkészítő összeállítja a kísérő hosztokat, mobil erőforrásokat, példabővítményt, ONNX futtatókörnyezetet és az elérhető függőségi értesítéseket. Tartsa meg az alkönyvtárakat. A sikeres build nem aláírt telepítő vagy jogi kiadási jóváhagyás. A forrás és a buildek külön fejlődhetnek; hasonlítsa össze a dokumentáció pillanatképét és commitját a telepített verzióval.

## Licencelés, adatvédelem és kiadási állapot

A tervezett kereskedelmi modell egyszeri vásárlás, a megvásárolt főverzió tartós használati jogával; a végleges árat, megengedett eszközszámot és frissítési feltételeket a tényleges értékesítési feltételekben kell rögzíteni. Ez a kézikönyv nem igazol éles vásárlási/aktiválási rendszert. A mindennapi vezérléshez nem szükséges felhőfiók.

Igazolt engedély nélkül ne terjesszen zárt SDK-binárisokat. Őrizze meg minden szállított függőség licencét és harmadik féltől származó értesítéseit, beleértve a .NET és ONNX csomagokat. A modellsúlyokra saját feltételek vonatkoznak. A dátumozott licencaudit egyes új függőségeknél korábbi, és nem jelent engedélyt ezek hozzáadására. A tároló nyilvánossága önmagában nem biztosít általános nyílt forráskódú licencet.

A valódi gyártói hardver, telefonos/tabletes LAN-működés, eltérő monitorok/DPI-k közötti átmenetek, tartós használat, teljes nyelvi lektorálás, kizárólagos illesztőprogram-támogatás és hasznos AI-tervezési minőség továbbra is külön átvételi munkát igényel. Egy funkció meglétéből soha ne következtessen jóváhagyásra.

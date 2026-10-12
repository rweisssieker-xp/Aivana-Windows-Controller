<!-- generated-doc locale=hu-HU source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../HANDBOOK.md)

# MAKRIVO — Felhasználói kézikönyv

Gyártó: **Aivana**. Dokumentáció snapshot: **2026-10-12**. Ez a kézikönyv leírja a jelenlegi munka-fák végrehajtását. A kísérleti integrációk és a hardver-függő funkciók kifejezetten azonosulnak. A történelmi jelentések megjelennek a dátumos képekről, nem garantálják az új építkezést.

## Dokumentáció és nyelvek

A dokumentációs nyelvválasztó segítségével nyissa meg a teljes kézikönyv, a funkciók feljegyzését, a problémamegoldást, a fejlesztési jegyzeteket, a kutatást és a történelmi tesztjelentéseket bármely 27 alkalmazási nyelven. A generált fordítások gépi segítséget nyújtanak és szerkezeti ellenőrzéseket tartanak fenn; nem mindegyikük kapta meg a szülőszót. A műszaki azonosítókat és a végrehajtható példákat változatlanul tartják fenn. Az eredeti forrás a hasonlósághoz elérhető. A fordított engedélyezési elemzés informatív és nem helyettesíti az eredeti harmadik fél jogi feltételeit vagy végső EULA-t.

## Beállítás és első indítás

Az eredeti cél Windows 11 x64. Tartsa az összes fájlt és alszámot a forgalmazás együtt. indítás `MAKRIVO.exe`. A forráscsökkentő nem egy telepítő. önálló tartalmú csomagok magukban foglalják a .NET futtatási időt; keretrendszer-függő csomagok igényelnek a megfelelő .NET 10 asztali és ASP.NET futtatási időt. ONNX és társas hostek kell szerepelni a megfelelő funkciók.

1. Kezdje a MAKRIVO és válassza ki a beállítások interfész nyelvét. automatikusan követik Windows.
2. Válasszon egy profilot, majd adjon hozzá egy lapot a kereshető cselekvési katalóguson keresztül. adjon meg egy felismerhető nevet, és válassza ki a cselekvési címet.
3. Ellenőrizze a lépéseket, és adott esetben rövidítse meg. Kerülje a már használt kombinációkat Windows vagy a cél alkalmazás.
4. A mentett változások nem a mentett konfiguráció.
5. Teszteljünk egy ártalmatlan akcióval, például egy dokumentum megnyitásával, és ellenőrizzük a végrehajtás visszajelzését.

A fő ablak zárása általában elhagyja az alkalmazást a bejelentési területen. Kétszeres kattintással nyissa meg a nyomaték ikonját, hogy újra megnyissa, vagy használja a nyomaték menüt, hogy kijusson. A Windows kezdődik opcionális és kezdetben kikapcsolva. A szünet vezérlő felfüggesztette a kötéseket; a ikon egy szünet jelet mutat. A futó makrok az UI, a nyomaték vagy a konfigurált törlés rövidítése; a szabványos `Ctrl+Alt+Escape`.

## Tilták, akciók és a parancs palettája

A fülbevalók profilokhoz tartoznak, és több megrendelt lépést tartalmazhatnak. A szerkesztő támogatja a mentést/megoldást, a előrejelzést, a undo/redo és a lépés újrarendelését. A katalógus előzetes programokat, dokumentumokat, URL-eket, szöveget, billentyűzeteket, rövidítéseket, média, hang, ablakokat, rendszerintézkedéseket és támogatott integrációkat tartalmaz. A paramétereket igénylő előzetes beállítások tervezetként megnyílik.

Nyissa meg a palettát `Ctrl+K` miközben MAKRIVO összpontosít. Keresés mentett akciók, előzetes, profil váltás és eszközök. A kulcsok kiválasztása; A beállítás aktiválja; `Ctrl+Enter` szerkeszt egy támogatott elem; Escape zárja. Néhány készen áll a használathoz parancsokat fut azonnal, míg a konfigurált akciók felülvizsgálatot igényelnek. Külön globális palettát rövidítés lehet konfigurálni; új konfigurációk használata `Ctrl+Shift+Space`.

Text és clipboard lépések támogatása `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` és `{{user}}`. A dátum formázása `yyyy-MM-dd`; Az időformátum `HH:mm`. A nem ismert változók változatlanok maradnak. a klipboard olvasása csak akkor történik, ha kérésre kerül sor. a klipboard akció szövegét írja automatikusan, anélkül, hogy megfelelne.

Az interfészből indított akciók az utolsó külső alkalmazást célzhatják. ha a cél eltűnik vagy Windows blokkolja az előzmény aktiválását, manuálisan aktiválja a célt és visszavonja.

## Profilok és munkahely beállítások

A profilok egy folyamat neve és egy ablak címe egy része. Amikor mindkettő be van állítva, mindkettőnek meg kell felelnie. A profilrendelés a versenyző mérkőzéseket oldja meg; a szabványos profil a visszaesés. Az automatikus kiválasztás kifejezetten engedélyezhető.

Nyissa meg a munkahelyet az Eszközökből vagy a palettából. Válasszon programokat, dokumentumokat, weboldalakot, egy hang/képernyő célpontot vagy egy OBS jelenetet. A munkahely, a találkozó, a streaming, a kutatás vagy a projekt profiljait készíti el. Ellenőrizze a létrehozott lépéseket, mielőtt menteni.

## Macros, feltételek és felvétel

A macrosok a lépéseket rendben hajtják végre, és támogatják a várakozást, a megismétléseket és a törléseket. Az általános ismétlődési határ 100. Használja a Preview-t, mielőtt olyan lépéseket futtatna, amelyek egy másik alkalmazásba vagy rendszerállapotba kerülnek.

A Célnyílás elfogadja a folyamatot, a cím-alávet és a 1 és 600,000 m-es időtartamot. Legalább egy szűrő szükséges. Egy megfelelõ látható ablaknak válaszolnia kell, és előre kell lépnie, mielőtt a következő lépések folytatódnak.

A munkafolyamat szerkesztő hozzáadja a fájl- létező, folyamat- futás és ablak-titkok feltételei. Egy hamis állapot megszakítja a lépést, és elmozdíthatja a további lépéseket előre. Elutasítsa a feltétel, hogy kifejezzék egy alternatív ág. Válassza ki a stop-on-error vagy continu-on-error szándékosan; folytatás hagyhatja a későbbi lépéseket várt előfeltételek nélkül.

A cselekvési rögzítő rögzíti a rövidnadrágokat és várja, nem a szokásos nyomtatást. támogatja a módosító rövidnadrágokat és a funkció kulcsokat. Escape megáll a rögzítés. Ellenőrizze a listát és elfogadja azt, mint egy mentetlen tile tervezet, majd kifejezetten mentse. UI-click rögzítés külön eszköz, amely le van írva alább.

## Kulcsszavak, gesztusok és rétegek

A billentyűzet a felfedezett interfészek és a referenciakörzetek listáját tartalmazza. Egyetlen fizikai billentyűzet több interfészet mutathat be. Válassza ki a helyes nyelvet és a geometriát; egyedül a termék neve nem azonosítja az ISO vagy az ANSI elrendezést.

A szabványos globális kulcsok a billentyűzeteken keresztül alkalmazandók.A készülék-specifikus Raw Input kötések egy adott eszköz és szkennelési kódot követnek, beleértve a támogatott funkciókat és a média kulcsokat. **Raw Input nem szűnik meg a szokásos Windows bevitelt. Egy kizárólagos második billentyűzet nem kész a gyártásra.** Egy aláírt, engedélyezett és tesztelt szűrővezeték-backend még mindig szükséges; egyetlen sem telepíthető MAKRIVO.

A támogatott speciális kulcskapcsolatok azonnal nyomtathatók, tapadhatók, tarthatók vagy dupla tapadhatók. a Held-layer kulcsok ideiglenesen kiválasztják a másik mappát és helyreállítják a bázisréteget a felszabadításon; a layer-selector kulcsok nem futnak saját makro-t. A határértékeket tartsa 150 és 5,000 ms között; a dupla tapadhatók ablakai 150 és 1,000 ms között.

A speciális kulcsbűvész a rögzítés során kijelölt akciók végrehajtása nélkül azonosítja a kompatibilis kulcsokat. A diagnózis a nem elérhető eszközöket, a szünetelt kötéseket, a profil hibákat és a konfliktusokat magyarázza. A túlterhelés a kiválasztott elrendezést és a kötéseket mutatja; a szabványos tartás rövidítése `Ctrl+Alt+F1`.

| integráció | Kötelezettségek és korlátozások |
| --- | --- |
| Corsair | A futás iCUE, az SDK hozzáférés és a kompatibilis modell. G1–G6 és a támogatott RGB funkciók; a saját Fn, kerék, zár és profil vezérlők általában nem kerülnek ki a G-key API. |
| Logitech G | Az eredeti x64 G-key/LED SDK könyvtárak kifejezett jóváhagyást igényelnek.G-key események nem azonosítják egyedül több azonos fizikai billentyűzetet. |
| Razer | Chroma RGB és szabványos beviteli; térkép tulajdonos ellenőrzése a Synapse ahol szükséges. |
| SteelSeries | GameSense RGB és szabványos beviteli; térkép tulajdonos ellenőrzése a GG ahol szükséges. |
| Wooting | Kompatibilis eredeti x64 SDK az analóg értékek és küszöbértékek számára; a normál HID beviteli folyamat aktív marad. |
| Egyéb kulcsszavak | Standard hotkeys vagy támogatott fizikai szkennelési kód kötések. Fn és tulajdonos ellenőrzések függ a modell és a szállító konfiguráció. |

A gyártó kutatása dokumentált képességeket ír le, nem valósították meg vagy tesztelték a kompatibilitást minden felsorolt eszközzel. Új adaptereknek még mindig valódi hardver elfogadása van. Ne töltse le az SDK binárisokat bizalmatlan forrásokból; az SDK terjesztési jogai a műszaki kompatibilitástól elkülönítve vannak.

## Audio, kijelzők és OBS

A hang lépések a 0 és 100 százalékos és a mut/unmute/toggle támogatott kimenetel vagy mikrofon végpontok. Válasszon egy adott végpontot vagy használja a Windows előzetes. Egy hiányzó mentett cél okoz hibát ahelyett, hogy csendesen ellenőrizni egy másik eszköz.

A kijelző akciók növelése/csökkenése fényesség 10 százalékpontok vagy beállítása értéke 0 100. A belső kijelzők használják támogatott WMI interfészek; a külső monitorok igényelnek DDC/CI támogatást és általában a monitor menü beállítása. Docks, adapterek és kábelek blokkolhatják a vezérlés. Válassza ki az összes támogatott kijelző vagy egy monitor. A újra csatlakozás megváltoztathatja a rendelkezésre álló azonosítók; felülvizsgálja a mentett célt.

OBS szükséges WebSocket v5 beállított OBS eszközök. beállítja a végpontot, teszteli a kapcsolatot és a terhelési jelenet / beviteli nevek mentése előtt. támogatott akciók közé tartozik a jelenet váltás, áram / rekord indulás / megállás / beviteli, rögzítés szünet / visszajelzés és beviteli változás. jelszavak tárolható a Windows hitelminősítő és kizárva a konfigurációs exportok. sikeres OBS parancs nem állapítja meg minden áramlatos hatást.

## Az eredmény ellenőrzése és végrehajtása története

A program megkülönbözteti a prefix ellenőrzéseket, a megerősített hatásokat, a törléseket és a hatásokat, amelyek nem igazolhatók. A folyamat létezését nem alkalmazás készenlét. Relatív változások, rövidítések és szövegbejegyzések nem kapnak sikertörténet. Ellenőrizze a tevékenység naplóját és a verifikációs történetet, amikor egy cselekvés kudarc. A konfigurációs hibák ki kell javítani, mielőtt a teljes makrot.

## UI automatizálás és kattintás felvétel

Adjon hozzá egy UI automatizálási lépést, válassza ki a műveletet, és rögzíti a célt a mutató alatt a megjelenített késés után. A rögzítés egyedül nem hajt végre semmit. Ellenőrizze a kiválasztót, és egyedi mérkőzést igényel. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` és `WaitState`. A jelszó mezők, a kikapcsolódott ellenőrzések és az egyértelmű kiválasztók elutasításra kerülnek.

Az UI automatizálása korlátozott társalapú folyamatban zajlik. attól függ, hogy a cél alkalmazás Windows hozzáférhetőség támogatás és engedélyek. A kattintás rögzítő rögzíti a támogatott akciókat és szüneteket; nem rögzíti az általános szövegbevételt. felülvizsgálja és érvényesíti a tervezetet, mielőtt elfogadja azt. A külső alkalmazás változásait a kiválasztók törölhetik. A folyamat szigetelése korlátozza a baleseteket és a törléseket, de nem biztonsági homokdoboz.

## Plugin

Nyissa meg a bővítményeket, hogy ellenőrizze a csomagot, mielőtt importálná. A csomag szerkezete, a manifeszt és a hashek ellenőrizni. A import elkezdődik. Olvassa el a bejelentett akciókat, és kifejezetten bízza a kiadó előtt. Plugins fut a felhasználói engedélyek egy külön folyamatban; ez a folyamat **nem egy homokdoboz**. Csak telepíti a csomagokat, amelyeket bízol.

A menedzser támogatja az export, a letiltás, a letiltás és a visszaállítás/megoldás egy korábbi verzió. Elveszett függőségek blokkolja a hívást. A példa csomag biztosítja a számlázás és a helyi állam intézkedések. A támogatott rendszer egy dokumentált JSON-Számlázó aljzat, nem minden lehetséges JSON-Számlázó funkció. Nincs nyilvános plugin piacon.

## Mobil panelek

1. Nyissa meg a mobilpanelt, válasszon egy helyi IP-t, és kifejezetten válassza ki a profilokat, amelyeket ki kell mutatni. A szabványos loopback-cím csak a PC-n érhető el; válassza ki a megfelelő LAN-címet egy telefonra vagy táblagépre.
2. Indítsa el a panelet, és olvassa el a HTTPS címét, tanúsítsa meg az ujjlenyomatot és egyhangú hat számjegyű páros kódot.
3. Egy eszközön ugyanazon megbízható hálózatban, nyissa meg az címet. Összehasonlítsa a SHA-256 ujjlenyomatot egy megbízható csatornán keresztül, mielőtt megbízható a helyi tanúsítványt.
4. A megjelenített kódot használja, és egy ártalmatlan lapot tesztel. A LAN-hozzáféréshez kézi Windows tűzfal engedélyre lehet szükség.
5. A panel befejezése után állítsa le, vagy visszavonja az alkalmazáson keresztül a csomagolt ügyfeleket.

A magánszolgáltató kulcsok Windows DPAPI védelmet kapnak; az ügyfél hitelesítéseket hashekként tárolják. az IP módosítása vagy a tanúsítvány megújítása megújulhat. a panel megmutatja a cselekvési és ellenőrzési állapotokat, nem minden lehetséges élő érzékelő. a böngésző elrendezése és a helyi HTTPS ellenőrzések nem helyettesítik a valós telefon/tablet LAN elfogadását.

## Helyi asszisztensek találkozása

Ez az opcionális kísérleti asszisztens alapértelmezett. Nyissa meg az eszközt, kifejezetten engedélyezze azt, válasszon egy futamidőt/modellt, és csak az erőforrásokat adja hozzá, amelyeket figyelembe kíván. Állítsa meg a célt, hozzon létre egy tervezetet, ellenőrizze a megoldatlan kéréseket és továbbítsa az eredményt a normál szerkesztőnek. Semmi sem fut automatikusan; mentés és futás marad kifejezett akciók. Nincs automatikus futamidőt.

| Runtime | beállítása |
| --- | --- |
| Ollama | Külön helyi szolgáltatás és egy helyi telepített modell a `127.0.0.1:11434`. A kijelölt távoli/felhő modellek elutasítják. |
| LM Studio | Helyi szerver, amely helyi terhelésű modellvel és JSON-kiszámoló támogatásával rendelkezik `127.0.0.1:1234`. Nem engedélyezhető a távoli útvonal a helyi feldolgozáshoz. |
| llama.cpp | Helyi `llama-server` és kompatibilis modell JSON-kód támogatásával a `127.0.0.1:8080`. |
| ONNX futási idő GenAI CPU | Hiteles kompatibilis GenAI szövegmodell mappát; válassza ki `genai_config.json`. |

A HTTP-kliensek csak loopback-t használnak, nem használnak proxy-t és nem követnek átirányításokat. MAKRIVO nem tudja megbízhatóan megállapítani, hogy egy kompatibilis helyi szerver belsőleg távoli szolgáltatást hív., Python A helyi bevezető szerverek nem letölthetők vagy telepíthetők automatikusan.

ONNX csomagok szükség `tokenizer.json` és érvényes chat templom. támogatott templomok `tokenizer_config.json` A string `chat_template`, Szótár egy szálkal `default`, vagy egy kétértelmű nevű/név alatti sablon listája. ha a sablon hiányzik vagy null, `chat_template.jinja` üres, kétértelmű, túlzott vagy helytelenül megadott minták elutasítják. ONNX fájlok, beépített/nézet modellek és GPU-k nem cserélhető kompatibilis GenAI A szövegek, a csomagolás GenAI A runtime verzió 0.17.1, CPU: Csak megbízható modellek használata; a folyamat szétválasztása nem egy homokdoboz.

A segédprogram támogatja a kiválasztott programokat érvek nélkül, kiválasztott dokumentumok és weboldalak, és a mikrofon mut, amikor kifejezetten kéri. Nem hoz létre üzeneteket, találkozók, önkényes skriptek, ingyenes kattintások, letöltések vagy fejhallgató váltás. A cél és a forrás nevek küldnek a kiválasztott helyi futási idő; forrás útvonalak és fájl tartalmak nem szállítják külön. A cél által írva a felhasználó magában tartalmazhat személyes adatokat. A helyi futási idő saját hálózati viselkedését a felhasználó felelőssége marad.

A ONNX kérés költségvetése 90 másodperc, beleértve a modell töltését, legfeljebb 3,072 prompt tokens és 1,024 generált tokens, szintén korlátozott a modell kontextus. rosszindulatú/csúszott JSON és ismeretlen erőforrás-azonosító elutasítják. törölje vagy közel véget ér egy kérés. A véletlenszerű modell futási idő teszt megállapítja a végrehajtást, nem hasznos tervezési minőség vagy időmegtakarítások. A helyi kísérleti naplók tartalmaznak esemény azonosítók és időpontok, nem cél szöveg vagy dokumentum tartalmak; nem automatikusan feltöltött.

A későbbi kis Instruct-modell értékelés nulláról érvényes tervekből állt négy német/angol vizsgálatban. a szülői következtetés működött, de a modell nem sikerült a szigorú tervezési szerződésben. Ez negatív minőség eredménye, nem ajánlott modell vagy bizonyított hasznos asszisztens. lásd a pontos helyi futási idő tesztjelentést a modell felülvizsgálatára és korlátaira.

## Import, export, szinkronizálás és biztonsági mentés

A behozott akciók beállítását megelőzően felülvizsgálják.A behozott akciók kikapcsolódnak, a hordozható szinkronizáció eltávolítja a hardver-specifikus kötéseket.A készülékek újra felosztása és a vizsgálat után a cselekvések engedélyezése.A szöveg/cselekvés tartalmának felülvizsgálata az export megosztása előtt; a jelszavak kizárása nem teszi az összes felhasználó által létrehozott cselekvés tartalmát érzékenyebbé.

A profilszinkronizáció a fájlokat egy kiválasztott mappában cseréli, amelyeket opcionálisan a meglévő felhőkliens kezeli. MAKRIVO nem szállít felhőfiókot és nem tárolja felhőbejegyzéseket. A felülvizsgálatok megakadályozzák a csendes feliratokat. Az akció tartalmának beépítése egyértelmű választás. A konfiguráció helyettesítése előtt. A szabványos konfiguráció `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; a történelmi címkével kapcsolatos kompatibilitás érdekében. Az alkalmazás bezárása, mielőtt manuálisan másolja/visszaállítja.

## Problémák megoldása

| tünetek | Mit kell ellenőrizni |
| --- | --- |
| A rövidítés nem | Az alkalmazás fut, a kötelékek nem szüntettek, a helyes aktív profil, a mentett/engedélyezett tál, a rövidítések és a rendelkezésre álló célok. |
| A cél ablak nem aktiválható | Kézi aktiválása. Windows előzmény korlátozások és a kiváltságok különbségek blokkolhatják az automatizálást. |
| Különleges kulcs nem észlelhető | Eladó szolgáltatás/SDK/modell támogatás; konfigurálja a tulajdonos vezérlőket, hogy a támogatott szabványos kulcsot lehetővé tegye. |
| A második billentyűzet is normálisan | A Raw Input értékkel várhatóan a kizárólagos szűrés nem történik a gyártáshoz. |
| Audio vagy fény nem elérhető | A készülékek frissítése; a kiválasztott azonosítás, a végpont ülések, a WMI/DDC/CI támogatás és a dokk/kábel korlátozások ellenőrzése. |
| OBS offline | WebSocket v5 aktiválva, helyes végpont/jelszó, elérhető szolgáltatás és érvényes jelenet/bejárati név. |
| Az automatizálás nem sikerül | A társaság tárhelye tartalmazza, egyedi érvényes kiválasztót, hozzáférhető engedélyezett célt, támogatott ellenőrzéseket és engedélyeket. |
| Mobil panel elérhetetlenné vált | LAN helyett loopback cím, ugyanaz a hálózat, a kiszolgáló fut, jóváhagyott profilok, tanúsított ujjlenyomat / bizalom és kézi tűzfal engedélyek. |
| Plugin hibás | A csomag integritása, a bizalom/engedélyezett állapot, a függőségek, a protokollkompatibilitás és a végrehajtás naplója. |
| A kérés nem sikerül | A helyes kiválasztott helyi futási idő, a szolgáltatás/modell készen áll, a JSON rendszer támogatása, a megbízható kompatibilis modellek és a kérési határértékek. |
| A szöveg vagy a szöveg tévesnek tűnik | Különböző nyelvet használ a billentyűzet geometriájától; átirányítsa az ablakot, ellenőrizze a kijelző méretét, és jelentse a pontos oldalt / nyelvet / építkezést. |

A hasznos hibajelentéshez a Windows verzió, az alkalmazás építése/megrendelése, a nyelv, a kijelző skálázása, a hardvermodell, a minimális sorrend és a releváns szerkesztett napló.

## Építés, tesztelés és csomagolás

Használja az SDK-t `global.json`. A raktár gyökere:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integrációs tesztek nyitott ablakok és ideiglenesen konfigurálni / kiadni támogatott szállító kötések. futtatni őket anélkül, hogy egy másik vezérlő, amely rendelkezik ezekkel a kötésekkel. A szabványos fényesség / hangvizsgálatokat olvasható; kifejezett hardver körvonalas tesztek vannak elválasztva. Mock protokollok, rendered képernyőfelvételek és véletlenszerű következtetési grafikonok nem kell képviselni, mint a valódi hardver / modell-minőség elfogadás.

A teljes kiadó társalapokat, mobil eszközöket, példamegoldót, a ONNX futási időt és a rendelkezésre álló függőség értesítéseket gyűjti össze. Tartsa az al-irányítványait. Egy sikeres építmény nem aláírt telepítő vagy jogi kiadás jóváhagyása. Forrás és építmények külön alakulhatnak ki; összehasonlítsa a dokumentációt és követi a telepített verzióval.

## Licenc, magánélet és szabadon bocsátás

A tervezett kereskedelmi modell egy egyszer vásárolt, a megvásárolt fő verzió folyamatos használatával; a végső ár, a készülék támogatása és a frissítési feltételek be kell állítani a tényleges értékesítési feltételek. A gyártási vásárlás / aktiválási rendszer nem állapítható meg ebben a kézikönyvben.

Ne osztja meg a tulajdonos SDK bináris engedély nélkül. Tartsa meg a licencet és a harmadik fél értesítéseket minden küldött függőség, beleértve a .NET és a ONNX csomagokat. A modell súlyai saját feltételeikkel rendelkeznek. Az adott licenc audit néhány új függőséget ad, és nem egy tisztázás az ilyen hozzáadásokra. A nyilvános tároló nem ad általános nyílt forráskódú licencet.

Valódi szállító hardver, telefon/tablet LAN működése, vegyes monitor/DPI átmenetek, hosszú távú használat, teljes nyelvi felülvizsgálat, kizárólagos vezetőtámogatás és hasznos AI tervezési minőség továbbra is külön elfogadási munka.

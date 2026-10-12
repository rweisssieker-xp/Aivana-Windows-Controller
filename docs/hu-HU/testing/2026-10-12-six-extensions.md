<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-12-six-extensions.md)

# Hat bővítés: végrehajtás és vizsgálat

Állapota: 12. október 2026, ipar `codex/six-extensions`. állapota, nincs termelési szabadság.

## Szolgáltatás

A parancspáléta megnyitja a bejegyzéseit **Munkahely beállítások**, **Tastatornák gyártói és SDK**, **Mobil panel**, **Fejlesztések**, **UI automatizálás**, **UI futtatás** és **Megvizsgálat**.

- A munkahelyi asszisztens profilokat hoz létre a munkahelyre, a találkozóra, a streamingre, a kutatásra és a projektre. A regisztráció/indítási menüből vagy a fájlválasztásból származó programok, az URL-ek, a kiadás/ képernyő és a OBS jelenet a tárolás előtt lépésekként jelenik meg.
- A lépésszerkesztőben **Az eredmények megvizsgálása** elővizsgálatot és ellenőrzést indít. Az útvonal és a mobilpanel megkülönbözteti a tényleges megerősítést a nem ellenőrizhető hatástól. A relatív változások, a billentyűzetek és a szövegbejegyzések nem adnak végleges hatáserősítést. A folyamat bizonyítéka nem bizonyítja a szakmai hajlandóságot az alkalmazásnak.
- UI automatizálás használ egy külön, időben korlátozott munka. kiválasztás az egér pozíció, a kiválasztó teszt, Invoke/Select/Toggle/Focus/SetValue/WaitState; több találat, kikapcsolt célok és jelszó mezők elutasítják. A felvétel tartalmazza a támogató kattintási lépéseket és szüneteket; a szöveges bejegyzést nem vágják le.
- Továbbfejlesztések: ZIP tesztelés, hashes, rendszer, deaktivált import, kifejezett bizalom a aktiválás előtt, izolált folyamat korlátozott IPC, export, deaktiválás, visszaállítása/ellentétele korábbi verzió. folyamatszigetelés nem homokdoboz: megosztott plugins fut a felhasználói jogok. példa csomag `makrivo.example.zip` tartalmaz számlázás és állapot akció. támogatott rendszerek alkotnak egy dokumentált egyszerű JSON rendszer részesedés a forrás kód; nincs nyilvános piaci hely.
- Mobil panel: válassza ki a helyi IP és megosztott profilok, nyissa meg a HTTPS címet, ellenőrizze a tanúsítványt a megjelenített SHA-256-Fingerprints és tudatosan bízza, majd adja meg az egyhangú hatjegyű kódot. A magán kulcsok védett Windows-DPAPI; csak a hashes a kliens hozzáférések tárolódnak. A tanúsítványok megváltoznak a megújítás vagy az IP-cserélés után. A Windows tűzfal megosztása a felhasználó által történik, ha szükséges. Nincs automatikus tanúsítvány telepítése, port-továbbítás vagy tűzfal módosítása. A kliens hozzáférések visszavonható. A panel mutatja a cselekvési állapotot és a vizsgálat folyamata; még nincs egyetemes élő érzékelő panel.

## billentyűzet

| Gyártó | végrehajtása | korlátozás |
|---|---|---|
| Corsair | meglévő iCUE G gombok és RGB | helyi iCUE kapcsolat és megfelelő modell szükséges |
| Logitech G | G-Key SDK és külön LED SDK | eredeti helyi x64-DLL kifejezetten kiadása; G-Key SDK nem ad egyéni azonosító több billentyűzet |
| Razer | Chroma RGB és szabványos billentyűfelismerés | A tulajdonosok gombjainak előzetes elrendezése Synapse |
| SteelSeries | GameSenseRGB és szabványos billentyűk felismerése | Előző bejegyzésKövetkező bejegyzésKövetkező bejegyzésKövetkező GG |
| Wooting | berendezéshez kapcsolódó analóg értékek, nyomás/csökkentési hullámok | eredeti helyi x64-SDK-DLL; a normál HID bevitele továbbra is aktív |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/További | fizikai szabványos gombok a készülék és a szkennelési kód szerint | Fn és tulajdonságú vezérlőelemek modelltől függően |

Nincs új, sajátos SDK-DLL-k szállítva. Az új gyártó adapterek még nem érvényesülnek a megfelelő fizikai eszközök nélkül.

**Exclusive második makropad nem készült:** Raw Input megfigyelhető készülékek külön, de nem csökkenti a normál Windows bejutás. A gyártás engedélyezett, aláírt, alatt Windows 11/Secure hajó ellenőrzött szűrővezeték, valamint a vizsgálat a sürgősségi felszabadulás, a szakadás, a felfüggesztés és a újraindítás. Nincs vezérlés telepítve.

## Vizsgálatok

- Release-Build: sikeres, null figyelmeztetések / hiba.
- Core konzolok: minden tesztcsoport sikeres, beleértve a gyártó billentyűtípusok, a plug-in rollback és a tesztfolyamat tényleges végrehajtási hibái.
- Kiterjesztési integráció: valódi UIA munkás WPF tesztelési alkalmazás; egyértelmű invoke, többszörös, letiltott célok, jelszó kizárása. a végső névleges cső-plugin folyamat, protokoll határok, analóg histerezis és Logitech ABI dekódolás sikeres.
- Valódi HTTPS host: nyomtatott tanúsítvány, nem csatlakoztatott hozzáférés, hamis / egyidejű kód, származási védelem, böngésző fájlok, hash tárolás és védett kulcs sikeresen.
- Windows-Suite kifejezetten elhagyott előzetes eredménytesztekkel: sikeres; Audio/OBS/iCUE/Raw Input és a képernyő állapotát ellenőrizte. Az előző futam során a teljes szuita nem sikerült a megtagadott fókuszváltással az izolált teszt ablakhoz. A későbbi futam során az AI prototípushoz a teljes szuita is szerepelt, beleértve a fókuszváltást és a kapcsolódó iCUE-A billentyűzet. Egy másik ismétlődő futás ismét meghibásodott a fókuszváltozáson; a kontextus-alapú teszt nem stabil zöld. Ez a teszt nem gyengült. A rövid távú hiányzó DDC olvasási megközelítések korlátozottan megismétlődnek; a fényesség nem változott.
- WPF-Smoke: minden korábbi és új párbeszédek frissített, konfigurációs változat, szerkesztő és CRUD sikeres; képernyőfelvételek a szerkesztő `smoke`.
- Browser: Playwright Edge, 390×844 és 1024×768, csatlakozási áram/UUID/egy alkalommal továbbítás/nem horizontális áramlás sikeres. API-Mock; valódi HTTPS teszt külön. képek alatt `artifacts/mobile-browser`. böngésző plugin nem volt elérhető, ezért rendszeres Playwright Fallback.
- Független kód felülvizsgálat elvégzett; késett mobil parancsok, hiányzó végső ellenőrzési eredmények, folyamatvizsgálat, plug-in rollback és újraindítása a RGB szívbeat kijavított.

Még mindig nyitva: exkluzív vezérlőintegráció, fizikai gyártó/telefonszám/tablett LAN tesztelés, Windows-Skalifikáció 150/200 Az új dialógus szövegek többnyire németek; a meglévő nyelvi katalógusok továbbra is megmaradnak. **Nem teljesen eltávolították**.

## Termelni csomagot

`./tools/publish-complete.ps1 -SelfContained` Új mappát hoz létre `artifacts/release-*`, beleértve a desktop, UIA munkatárs, HTTPS host, böngésző fájlok, importált példányt és futási engedélyek. A telepített alkalmazás nem írja le. `-SelfContained` Megfelelő csomagot igényel .NET-10-Desktop-/ASP.NET-Runtimes.

A csomag egy helyi tesztelő, nem aláírt értékesítési telepítő. A saját használati feltételek és az SDK értékesítési jogok külön kiadványok; lásd a meglévő dokumentációt a `docs/licensing` oldalon.

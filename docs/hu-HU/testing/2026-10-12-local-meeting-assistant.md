<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-12-local-meeting-assistant.md)

# Helyi intelligencia-tanácsadó – prototípus

Állapota: 12. október 2026. ForgeMind hipotézis „Outcome Agent”, korlátozott találkozási folyamat. Nincs bizonyított versenyelőny és nem mérhető időmegtakarítás valódi összehasonlító kísérletek nélkül.

## Használat

1. Rendelkezési palettája → **KI-megbeszélő asszisztens**. A kísérlet szabványos, és a konfigurációs import vagy a portál export esetén visszavonható.
2. A helyi asszisztensek aktiválása. Ollama külön kell futtatni a `127.0.0.1:11434`-re; a megfelelő helyi modellnek már telepítve kell lennie. **Lokális modellek feltöltése** és a modell kiválasztása. MAKRIVO nem telepíti a modelleket/szolgáltatásokat, nem kapcsolódik az OpenAI/Cloud API-hoz és nem használ API-kulcsot. A helyi Ollama szolgáltatás offline konfigurációja és megbízhatósága a felhasználó számára; a bejelentett távoli/Cloud modellek elutasítják.
3. Célba lépés és programok/dokumentumok/webhelyek kifejezetten hozzáadása. cél, erőforrás-ID, név és típus továbbítják; célút és a kiválasztott fájl tartalma nem továbbítják; a célbevétel önmagában szó szerint továbbítják. webhelyek neve csak a rendszer/host, a teljes link marad helyi.
4. **KI-tervezés**. maximális 90 másodperc, 1024 kimeneti dobozok, korlátozott válaszméret. cél / modell / erőforrás változások elutasítják a előrejelzést; törlés / zárás befejezi a kérelmet. A modell csak kiválasztott azonosítók javasolhatók. ismeretlen azonosítók, szabad parancsok, további JSON mezők és végrehajtható dokumentumtípusok elutasítják.
5. Előrejelzés és nyitott pontok ellenőrzése → **A szerkesztőben**. Az eredmény először nem tárolt tervezés. Csak a meglévő szerkesztőben tárolja és szükség esetén kifejezetten végrehajtja. A normál makrovalidáció, előrejelzés/rejelzés, szünet és törlés továbbra is felelős.

Támogatott: Programok indítása érvek nélkül, kifejezetten kiválasztott helyi PDF/DOCX/XLSX/PPTX/TXT/MD megnyitása, HTTP(S) weboldalak megnyitása, kifejezett kívánságra mikrofon csend. Nincs standard headset változás, nincs üzenet, dátumok, letöltések, forgatókönyvek, ingyenes kattintások vagy szövegbejegyzések. Nem képződhetők a kívánságok nyílt pontként jelennek meg, ha a modell megfelelően felismeri őket; a szakmai célkitűzésnek a felhasználónak kell ellenőriznie.

## Összehasonlító kísérlet

**Mindítások · Kezdés / vég** mérik a teljes kézi összehasonlító futamot. asszisztens fogadások mérik az időt a nyitás a asszisztens az első tárolás a szerkesztő; `generated` azonban csak a tervezési időt.

A helyi események a konfigurációs sorrendben `Experiments/meeting-experiment.jsonl` alatt találhatók: időzónák, véletlenszerű kísérleti/kockázati azonosító, esemény, időtartam, korrekció igen/nem. Nincs célszöveg, erőforrás-nevek, útvonalak, modellek válaszai vagy dokumentumtartalom. A fájl 2 MiB-ről forog, egy korábbi fájl megmarad.

Felfedezze: manuális bázis, modellgeneráció/hibák/megszakítás, elhanyagolható előrejelzési változások/csatlakozás, első tárolás és korrekciók a szerkesztőben, valamint a tárolt zárak tényleges futóállapota. `run_completed` csak azt jelenti, hogy a futó befejeződött; megerősített célhatások külön-külön a tesztfolyamatban. ugyanazon makró ismétlései nem új független beállítás kísérletek. A kezdeti kísérlet folytatható a megindítás után, mert a kísérleti azonosító a tárolt zárakban marad.

Első kísérlet: 20 összehasonlítható találkozó létesítmények vagy két hét önkéntes tesztfelhasználók; először belső használat. manuális telepítési időt összehasonlít a teljes asszisztált telepítési időt; további korrekciós/megtagadási arány, futó hibák és a valós vizsgálati eredmények. hiányzó javítás vagy megsértése védelmi határátlépő kikapcsolja. Nincs használat vagy fizetési készség bizonyított a nyilvántartásban. Nincs automatikus telemetriátadás vagy árat.

## tesztelési állapot

- Core: korlátozások, típusok, ismeretlen/duplicált azonosítók, titkosítás kikapcsolása, hosszú linkek, opcionális alt konfiguráció, import/export-opt-in, adatmegtakarítható mérés, valamint egyedi elutasítás és helyes átadás.
- A hamis modell: Ollama JSON rendszer, Loopback cél, nincs fájlfolyamat a prompt, felhőnevek és távoli elutasítások, sérült/hosszú válasz elutasítás, törlés.
- WPF-Smoke: a párbeszéd csökkent; a tervezés átadása nem változtatja meg a tárolt cseppeket, a felülvizsgálat/tárolás származást és rögzíti a korrekciót, a lemondás nem tárol semmit.
- Független felülvizsgálat: Elutasítva és hosszú linkek megtalálható és kijavítva; Regression teszt RED→GREEN.
- Release-Build null figyelmeztetések/hibák, az egész Core Suite és a WPF Smoke sikeres. A teljes Windows Suite először beleértve a fókuszváltást. Újra sikertelen volt a megtagadott előzményváltás; a teszt összefüggésben van, és változatlan marad. A többi Windows teszt, beleértve a modell szállítás, kifejezetten elutasított előzményváltás teszt.
- A csomagvizsgálat fedett fel egy meglévő hiba atomcsere rövid ideig megnyitott konfigurációs fájlokat. Az olvasó visszautasította ugyanazt a hozzáférést-nem; korlátozott ismétlés csak az atomcsere RED→GREEN ellenőrizte.
- Jelenlegi önálló csomag `artifacts/release-20261012-184105`: WPF-Smoke a memória korrekciót követően sikeres, beleértve az AI párbeszédet és a tervezés elfogadását/megtagadását.
- Nincs valós modellvizsgálat: a helyi Ollama szolgáltatás nem válaszolt a tesztre. Nincs nyilatkozat a modellminőségről, a lassúságról, a szemantikus teljességről vagy az időmegtakarításról.

A protokollforrások: [Ollama Chat](https://docs.ollama.com/api/chat), [Strukturált kimenetel](https://docs.ollama.com/capabilities/structured-outputs), [Hivatalos típusmeghatározások, beleértve a távoli modelladatokat](https://github.com/ollama/ollama/blob/main/api/types.go).

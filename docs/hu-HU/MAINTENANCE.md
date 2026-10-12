<!-- generated-doc locale=hu-HU source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../MAINTENANCE.md)

# Minden dokumentációs nyelv megőrzése

A dokumentációt az összes 27 UI nyelvet követik.A generált mappák regionális kódokat használnak; Norvég térképek `nb-NO` és Szerb latin `sr-Latn`.Minden mappában index, kézikönyv, részletes cselekvési referencia, funkciók készlet, ellenőrzési jegyzetek, kutatás, beleértve a CSV-t, történelmi tervek és engedélyezési elemzések.

## Az igazság forrása

Edit a gyökér `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Az eredeti oldal, vagy az alábbi `docs/research`, `docs/testing`, `docs/superpowers` és `docs/licensing`. Ne írjon újra egy történelmi jelentést, ami azt jelenti, hogy egy későbbi verzió érvényesül. Tartsa a végrehajtást, a szimulációt, a valós eszköz elfogadását és a kereskedelmi jóváhagyást külön.

## Generál és érvényesít

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Az opcionális online generátor dokumentációt küld a Google nyilvános fordítási végpontjába, a meglévő lokalizációs eszközökkel összhangban. Nem olvassa el a felhasználói konfigurációt, titkait vagy modellek fájlokat. Csak a szolgáltatáshoz megfelelő dokumentációra futtatja. A kód blokkja, az inline kód, az URL-k, a táblázat szerkezete és a műszaki azonosító védett. A fordítási cache és a fagyasztott forrás képei figyelmen kívül hagyva tárolódnak `artifacts/documentation-translation`.

Ha az online szolgáltató korlátozott, `tools/translate_documentation_local.py` A cache-t egy kifejezetten megadott helyi M2M100 CTranslate2 A modell 2026-10-12 A Snapshot a MIT-licencet használja facebook/m2m100_418M A modell, amelyet JustFrederik/m2m_100_418m_ct2_int8 A felülvizsgálat `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 és `SentencePiece` 0.2.2 nem épül-csak eszközök. nem modell súlyok vagy fordítási függőségek szállítják MAKRIVO. A generátor futtatása `--cache-only` a cache kitöltésével a hálózati kérések nélkül. a helyi segéd nem tölt le vagy fut a távoli modellek kódját.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Nincs `--refresh-sources`, a futás újra fagyott képe. Használja a frissítési lehetőséget az eredeti szerkesztés után. A rejtett szegmensek újra használhatók. A részleges futás kiválaszthatja a helyi kódokat a `--locales` használatával; befejezi az összes helyi közzététel előtt. A megszakított futások nem csendesen helyettesítik a nem fordított prózát a hibás fordításhoz.

A manifesztus minden forrás és a generált fájl rögzítése SHA-256 ujjlenyomatok normalizált UTF-8 szöveg: LF vonal vége és nincs bajt-rendelési jel. Az offline ellenőrző teljes nyelvi lefedettséget, ujjlenyomatokat, helyi linkeket, kódmintákat, táblázat/CSV szerkezetet, forrás URL-ket és helyettesíthetetlen helytartókat ellenőrzi. Az acélforrások elutasítása. A strukturális ellenőrzés nem a szülőszöveg felülvizsgálata vagy jogi jóváhagyása. A generált oldalak ezt a képesítést szemlélhetően hordozzák.

## Közzététel és csomagolás

Közzéteszi a dokumentációt, a generátor és a checker együtt. Megtartja az eredeti harmadik fél licenc feltételeit; a lefordított licenc elemzése informatív. Építsen művek, a fordítás cache és a helyi orchestrációs fájlok nem tartoznak a Git. A forrás-dokumentációs kiadás nem közzéteszi magát egy Windows telepítő.

A felhasználó-szemléletű alkalmazás fordításai külön JSON katalógusok alatt `src/Aivana.Core/Localization`. A dokumentáció generációja nem módosítja ezeket a katalógusokat. Az alkalmazás építésének és dokumentációjának meg kell osztania a verziózású képeit; az újonnan leírt kísérleti futási idő támogatása előbbre léphet egy telepített csomagot.

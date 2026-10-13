<!-- generated-doc locale=hu-HU source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../MAINTENANCE.md)

# Minden dokumentációs nyelv megőrzése

A dokumentációs állomány mind a 27 felületi nyelvet követi. A generált mappák regionális kódokat használnak; a norvég kódja `nb-NO`, a latin betűs szerbé `sr-Latn`. Minden mappa tartalmaz indexet, kézikönyvet, részletes műveletreferenciát, funkciójegyzéket, ellenőrzési jegyzeteket, kutatást annak CSV-jével, korábbi terveket és licencelemzést.

## Az igazság forrása

A gyökérben található `README.md`, a `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, ez az oldal, illetve a `docs/research`, `docs/testing`, `docs/superpowers` és `docs/licensing` alatti eredeti fájlok szerkeszthetők. Ne írjon át korábbi jelentést úgy, mintha egy későbbi verziót validálna. Különítse el a megvalósítást, szimulációt, valódi eszközös átvételt és kereskedelmi jóváhagyást.

## Generál és érvényesít

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Az opcionális online generátor a meglévő lokalizációs eszközöket követve a dokumentáció prózáját a Google nyilvános fordítási végpontjára küldi. Nem olvas felhasználói konfigurációt, titkokat vagy modellfájlokat. Csak az adott szolgáltatáshoz alkalmas dokumentáción futtassa. A kódblokkok, soron belüli kód, URL-ek, táblázatszerkezet és technikai azonosítók védettek. A fordítási gyorsítótárak és a rögzített forráspillanatkép a verziókezelésből kizárt `artifacts/documentation-translation` alatt tárolódnak.

Ha az online szolgáltató korlátozza a kérések gyakoriságát, a `tools/translate_documentation_local.py` ugyanazt a gyorsítótárat tölti fel egy külön megadott helyi M2M100 CTranslate2 modellel. A 2026-10-12 pillanatkép az MIT-licencű facebook/m2m100_418M modellt használja, JustFrederik/m2m_100_418m_ct2_int8 formára konvertálva, a `1aeed44db4dd61a486bba44acf54c76507082a2c` revízión. A CTranslate2 4.8.2 és a `SentencePiece` 0.2.2 csak buildeszközök. A MAKRIVO nem tartalmaz modellsúlyokat vagy fordítási függőségeket. A gyorsítótár feltöltése után futtassa a generátort `--cache-only` kapcsolóval a hálózati kérések nélküli előállításhoz. A helyi segéd nem tölt le és nem futtat távoli modellkódot.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

A `--refresh-sources` nélkül a futtatás a rögzített pillanatképből folytatódik. Az eredetik szerkesztése után használja a frissítési kapcsolót. A gyorsítótárazott szegmensek újra felhasználhatók. Részleges futtatásnál a `--locales` kapcsolóval választhat nyelvkódokat; közzététel előtt fejezze be az összes nyelvet. A megszakított futtatások nem helyettesítik észrevétlenül a sikertelen fordítást lefordítatlan szöveggel.

A jegyzék minden forrás- és generált fájl normalizált UTF-8 szövegének SHA-256 ujjlenyomatát rögzíti: LF sorvégek, bájtsorrendjel nélkül. Az offline ellenőrző vizsgálja a teljes nyelvenkénti lefedettséget, ujjlenyomatokat, helyi hivatkozásokat, kódpéldákat, táblázat-/CSV-szerkezetet, forrás-URL-eket és a ki nem cserélt helyőrzőket. Az elavult forrásokat elutasítja. A sikeres szerkezeti ellenőrzés nem anyanyelvi lektorálás vagy jogi jóváhagyás. A generált oldalak láthatóan feltüntetik ezt a korlátozást.

## Közzététel és csomagolás

A dokumentációt, generátort és ellenőrzőt együtt tegye közzé. Őrizze meg a harmadik felek eredeti licencfeltételeit; a lefordított licencelemzés tájékoztató jellegű. A buildtermékek, fordítási gyorsítótár és helyi vezérlőfájlok nem tartoznak Git verziókezelésbe. A forrásdokumentáció kiadása önmagában nem tesz közzé Windows-telepítőt.

Az alkalmazás felhasználói fordításai külön JSON-katalógusok a `src/Aivana.Core/Localization` alatt. A dokumentáció generálása nem módosítja ezeket. Az alkalmazás buildjének és dokumentációjának közös, verziózott pillanatképhez kell tartoznia; az újonnan ismertetett kísérleti futtatókörnyezet-támogatás megelőzheti a telepített csomagot.

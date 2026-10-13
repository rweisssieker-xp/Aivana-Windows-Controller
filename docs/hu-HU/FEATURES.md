<!-- generated-doc locale=hu-HU source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../FEATURES.md)

# MAKRIVO – jellemzők áttekintése

Gyártó: **Aivana**. állapot: **12. október 2026**. Ez a fejlesztési állapot, nem értékesítési vagy hardveres kiadás.

## Windows irányítás

- Káblák, globális hotkeys, parancspaletták, kedvencek és alkalmazásfüggő profilok.
- Makrok több lépéssel, ismétlésekkel, szünetekkel, szakadással és előrejelzéssel.
- Programok, fájlok és weboldalak megnyitása; szöveg, közbenső táblázat és billentyűzet használata.
- Az ablakok beállítása, a Windows zárása, a hang kimenetelének és a mikrofonok ellenőrzése, valamint a képernyő világosságának megváltoztatása a támogatott felületeken keresztül.
- OBS-WebSocket-v5-Anbindung forgatókönyvek, rögzítés, streaming és audio.
- Templomok, munkahelyi asszisztens, készülék felismerése és billentyűzet felülvizsgálata.
- Profilkonfigurációk behozatala és exportja; tervezési vizsgálat és konfliktusfelismerés.
- Helyi tárolt beállítások; nincs számla szükséges a mindennapi használat.

## Továbbfejlesztett funkciók a jelenlegi forráskódban

| terület | Funkciók tartalma | Határok |
| --- | --- | --- |
| Az eredményvizsgálat | Elővizsgálat, ellenőrzés és vizsgálat | Csak ellenőrizhető hatások megerősítik; a sikeres billentyűzet nem bizonyítja a célt. |
| Munkahely asszisztens | A kiválasztott programokból, fájlokból, URL-kból és egyéb akciókból származó profilok és folyamatok elkészítése | Előrejelzés és tárolás szükséges; a létesítmény nem vezet ki. |
| Makrofelvételek és munkafolyamatok | Felvételek, feltételek, gesztusok és helyi profilszinkronizáció | A magatartás és a korlátok a végrehajtási és tesztjelentésekben vannak dokumentálva. |
| UI automatizálás | A célok kiválasztása, a kiválasztók megvizsgálása, a cselekvések és a korlátozott kattintások rögzítése | Külön munkafolyamat; a többértelmű, letiltott és jelszócélok elutasításra kerülnek; nincs általános szövegbevitel-rögzítés. |
| kiterjesztések | A csomagok ellenőrzése, importálása, kifejezett bizalommal történő aktiválása, exportálása és visszahívása | A folyamatszigetelés nem egy homokdoboz; nem egy nyilvános plug-in piac. |
| mobilpanel | Helyi HTTPS panel, összekapcsolás, kiválasztott profilok és állapot/vizsgálati történelem | A valódi telefonos/tabletes LAN átvételi teszt még hátravan; nem univerzális érzékelő-műszerfal. |
| Gyártóadapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB és Wooting-Analogadapter | Modell-/SDK-függő; az új adaptereket nem validálták teljesen valódi hardveren. Új, zárt SDK-DLL-ek nincsenek mellékelve. |
| Fizikai billentyűzet | Standard gombok a készülék és a szkennelési kód felismerése | Raw Input nem csökkenti a normál Windows bevitelt. |
| Helyi AI megbeszélő asszisztens | Az opcionális prototípus Ollama, LM Studio, llama.cpp és ONNX futási idő GenAI a CPU kifejezetten kiválasztott erőforrásokból korlátozott tervezéseket hoz létre | Alapértelmezés szerint kikapcsolva; a natív CPU futtatókörnyezetet véletlen modellel tesztelték, de nincs igazolt tervezési minőség vagy időmegtakarítás; modellsúlyokat nem tartalmaz. |

## Még nem kínálnak kész értékesítési funkciókat

- **Exkluzív második lemez / makropád:** Egy aláírt, engedélyezett és hardver ellenőrzött szűrőprogram hiányzik. A szokásos billentyűzetek továbbra is Windows elérés.
- Az összes gyártói adapter teljes hardveres átvételi vizsgálata és a valódi mobileszközös LAN-tesztek.
- Valódi vegyes monitor/DPI váltás és az összes új fordítás szakmai eltávolítása. PerMonitorV2 és a szimulált 150/200-Prozent-Layouts ellenőriztek; a valós kijelzővizsgálatok külön maradnak. A felület 27 nyelvet kínál; a részletes új szövegek még nem teljes nyelvi eltávolításra kerültek.
- Aláírt értékesítési telepítő, végleges EULA, ellenőrzött SDK értékesítési jogok és produktív vásárlási engedély / ellenőrzés integráció.

## Részletek és bizonyítékok

- [Használati dokumentáció](README.md)
- [Kiterjesztések: funkciók, tesztek és nyitott határok](testing/2026-10-12-six-extensions.md)
- [Helyi AI asszisztens: prototípus és vizsgálati állapot](testing/2026-10-12-local-meeting-assistant.md)
- [Helyi AI időtartamok és modell követelmények](testing/2026-10-12-local-ai-runtimes.md)
- [DPI és skálázási vizsgálat](testing/2026-10-12-dpi-scaling.md)
- [Licencvizsgálat](licensing/2026-10-12-commercial-license-audit.md) – történelmi rögzítés; az új közzétett forgatókönyv kiegészíti a futási utasításokat. A végleges csomagot külön kell vizsgálni.
- [Eladási platformok](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Összehasonlító állapot; Stripe egy további lehetséges fizetési platform, lásd a jelenlegi szolgáltatói feltételek.

## Teljes fejlesztési csomag létrehozása

```powershell
./tools/publish-complete.ps1 -SelfContained
```

A csomag tartalmazza az asztali alkalmazást, az UI-automatizálás munkafolyamatát, a mobilhosztot, a böngészőfájlokat, a példabővítményt és a futtatókörnyezet licencértesítéseit. A buildtermékek nem kerülnek Git verziókezelésbe. A GitHub forráskódállapota még nem közzétett Windows-telepítő.

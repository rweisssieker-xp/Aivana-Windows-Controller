<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-09-usp-release.md)

# Keyboard segítség, felülvizsgálat és végrehajtás visszajelzés

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

A jelenlegi Windows alkalmazásban végrehajtott három prioritás javítás:

- Speciális kulcsbűvész: valós iCUE támogatott eszköz/kulcs készlet, ideiglenes G-kulcs rögzítés, konfigurációs sikertörténet, konfliktus validáció és nem mentett cselekvési terv. rögzítés, quued callback érvénytelenség és normál kötődés helyreállítás fedezése hibák és a párbeszéd zárása.
- Keyboard overlay: fizikai referenciamutató, aktív profilkötések, élő profil/szerkesztés frissítés és a szokásos Windows kulcsfény. tartsa Ctrl+Alt+F1 vagy a palettát/keyboard oldalt. nem aktiválható, kattintással és hiányzik a feladatlapból.
- Végrehajtási visszajelzés: beírt Várakozás / futás / lépés / befejezett / törlés / elutasított fázisok, lépés / ismétlés előrehaladás és nem aktiváló tál. Visszajelzés kizárja a cselekvési tartalmat és a kivételes üzeneteket. Végrehajtás nem jelentheti befejezését. Végrehajtás leírja a végrehajtást, nem független OBS állami kérelem.

A vizsgálat:

- 24 A kulcsellenőrzések átültetése, beleértve a fázis megrendelését, a kudarc/törlés eredményeit, a végső lépésű törlését, a hallgatói kivétel elszigeteltségét és a magáncselekmény értékének kizárását.
- A kiadás megoldása ért véget figyelmeztetés / hiba nélkül.
- Windows A felvételek áttekintése; a kapcsolódó K100 ⁇ G1–G6 és a szülői konfiguráció/kiadás sikeres volt. A hangvégpontok ellenőrizték az állapotukat anélkül, hogy megváltoztatták.
- WPF füst átadta az összes referenciamutató, varázsló / katalógus / konfliktus / raft állandóság, élő ősi rögzítés konfiguráció, hívás visszavonása, állandó hívás visszavonása és szünetelt-kötő visszaállítás.
- A végrehajtási tálra ellenőrizte a tényleges előzmény-télen eltérőségét. a Overlay interakciós tulajdonságokat és a show/hide-t ellenőrizte; a varázsló, a overlay és a tálra készült képernyőfelvételek.
- 27 nyelvek tartalmaznak minden 819 kanonikus szalagok; offline helyszíni/kulcs érvényesítés át.

Snapshots és a teljes GUI eredmény az artifaktumok/usp-win-x64/smoke. tesztek ideiglenes profilokat használnak; a termelési beállítások támogatásra kerülnek és a telepítés során hash-ellenőrzésre kerülnek.

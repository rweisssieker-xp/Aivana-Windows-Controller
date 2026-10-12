<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-12-dpi-scaling.md)

# DPI vizsgálat

MAKRIVO nyilatkozott PerMonitorV2 A beépített alkalmazási manifesztumban. a WPF-Tesztelés GetWindowDpiAwarenessContext és AreDpiAwarenessContextsEqual a fő ablak tényleges kontextusát vizsgálja.

A további layout/raster tesztek szimulálása 150 és 200 százalékos egy 1920 × 1080 A feladatlap tartalékával ellátott kijelző: logikus ablakméret 1280 × 693 A kapcsolat 960 × 520 A főoldal öt oldalán 144 A kapcsolat 192 DPI csökkentett PNG. fájlok forró skale-simulated-150-* vagy szimulált-200-*.

Ezek a tesztek nem változtatják meg a Windows beállításokat. nem érvényesítik a valódi WM_DPICHANGED monitorváltozást, nem kevert monitor konfigurációt és nem valós szilárdságot egy másik kijelzőn.

A WinForms elemzési figyelmeztetés WFO0003 kizárólag a WPF indítási projekthez kerül elnyomásra: WinForms az információs terület integrációjához használják, WPF a indítás irányítását; a DPI-t jelzővel állapítják meg az ablak létrehozása előtt.

Microsoft dokumentáció: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Látványvizsgálat: A lemez a 650 DIP-es ablakmagasságban a rögzített megjelenítésre vált, hogy a térképek és a szerkesztők ne kerüljenek túl kicsi középső területen.

Tesztállomás: Release-Build hibák/figyelmeztetések nélkül; teljes WPF smoke tesztet, beleértve a tényleges PerMonitorV2-Fensterkontext és szimulált 150/200-Prozent-Renderings. A 200-Prozent-Dashboardaufnahme vizuálisan ellenőrizte a magasság módosítása után. git diff --check ellenőrizte.

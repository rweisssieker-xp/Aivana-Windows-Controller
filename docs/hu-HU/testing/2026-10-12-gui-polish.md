<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-12-gui-polish.md)

# GUI átalakítása 12.10.2026

- Közvetlen bevezetés „eszközök” a navigáció; három csoportok kijelölt intézkedések létrehozása, létrehozása és vizsgálata.
- Négy meglévő párbeszédterület közös WPF párbeszédosztályot használ a fő ablak erőforrásaival, az egységes írással és a tulajdonos helyezésével.
- A párbeszéd mérete a Windows munkaterületre korlátozódik; a hosszú tartalmak átirányítói. a beállítások és a profiloldalak alacsony magasságban is átirányíthatók.
- A cselekvési szűrő csak a képernyőn jelenik meg. A profilválasztás és a keresés kifejezett neveket tartalmaz a segédtechnológiák számára.
- A Dialógus-Smoke teszt magában foglalja az eszköz áttekintését és a 560 × 440 kiterjesztési dialógusokat, valamint a normál méreteket; a meglévő rögzítő- és nyelvvizsgálatokat a Scroll szerkezethez igazították.

Korlátok: A kompakt ablakok nem helyettesítik a valódi Windows-DPI-teszteket 150/200 százalékos skálázással. Az új német szövegek még nincsenek teljesen lefordítva mind a 27 nyelvre. Nincs új hardvervalidálás vagy állítás a mért használhatóságról.

Tesztállomás: Kiadás-építés figyelmeztetések/hibák nélkül; teljes WPF smoke tesztet, beleértve a kompakt eszköz- és kiterjesztési párbeszédeket, valamint a nyelvi változások tesztelését. git diff --check

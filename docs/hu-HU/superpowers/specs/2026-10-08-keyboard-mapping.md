<!-- generated-doc locale=hu-HU source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fizikai billentyűzet megjelenítése a térképezéshez

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cél

A billentyűzet megjelenítése a cselekvések elosztásakor a tényleges billentyűzeteket jelzi. Ugyanazok a széles sorok nem elegendőek: Enter, Shift, Space, Navigation, Number Block és a gyártó kiegészítő vezérlő elemei különböznek.

## Végrehajtás

- Katalógus külön fizikai azonosítókkal, geometria billentyűzet egységek, nyomtatott legendák és Windows bejegyzési kódok.
- Az ISO-DE és az ANSI-US mind teljes formátumú, mind TKL formátumú; ezenkívül Corsair K100 RGB, amely a iCUE csatlakoztatott modellként ismert.
- ISO Enter L-formátummal, magas plusz/bejárati gombokkal és széles nullával rendelkező számblokkolással; navigáció és vonalak a saját pozíciójukon.
- Az USB VID/PID és a modellek neve nem bizonyítja a nyelvi változást. A generikus profilok és a gyártó dokumentumai alapján modellezett profilok eltérő módon jelennek meg; nincs milliméter vagy firmware kompatibilitási garancia.
- Az aktív Windows beállításra mutató szkennelési kódot, hogy egy fizikai gombnyomtatás ne tegyen véletlenül egy másik gombot, például az Y/Z-t az USA és a DE között.
- Hozzáférés megnyitja a tervezetet a meglévő akciószerkesztőben. tárolja, ellenőrizze a konfliktusokat, és elutasítja a meglévő mechanizmusokat.
- A tulajdonos G gombok, profil/lock gombok és a iCUE vezetékek megjelenik, de a bevezető adapter nélkül nem kerülnek ki normál Windows hotkeys.
- Az elhelyezkedések globális Windows hotkeys maradnak; a készülék látása nem jelenti az egyes fizikai billentyűzetek elnyomását vagy kizárólagos elhelyezkedését.

## bizonyítékok

[K100-Handbuch a vezérlőelemekkel](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO nyilatkozat](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Ellenőrzés

Geometriai, változatos és beágyazott tesztelés; építési és meglévő Core-/Windows tesztelés; WPF szivattyú megjelenítéséhez és elrendezéséhez való tényleges felhasználói konfiguráció végrehajtása vagy módosítása nélkül. A gyártó kutatása további modellek/SKU követelményeket dokumentál, függetlenül attól, hogy a jelenleg végrehajtott elrendezési katalógus.

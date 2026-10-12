<!-- generated-doc locale=hu-HU source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Használhatóság és kapacitásfejlesztési terv

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Felhasználói igény: javítja a megjelenést, a használhatóságot és a funkcionalitást; a lehető legkorszerűbb alvállalkozások igényelése a szükséges kisebb modellek és az autonóm befejezés.

## A cél
Megőrzése meglévő profilok és működő Corsair SDK integráció. javítja a meglévő WPF alkalmazás helyett létrehoz egy külön prototípus.

- [x] Shell/dashboard munkavállaló: responsive layout, aktív navigáció, olvasható ellenőrzések, kompakt tevékenység/állapot, drag-and-drop tile reorder, hasznos üres állapotok és sablonok.
- [x] Szerkesztő munkás: MainWindow.Editor.cs és opcionális szerkesztő-csak részleges fájlok. Explicit lépcső szerkesztések, Save/Discard, validáció inline, dedikált makro listák a drag-and-drop, duplikált lépések, megbízható hotkey fogadás és fájl/URL mezők.
- [x] Windows munkavállaló: szülői validáció, robusztus eszköznevek és SDK képességek, korlátozott módosító várak és biztonságos injekció; saját Windows és Windows tesztek.
- [x] Szülő: a fő végrehajtási előrehaladás, a vezérlő felületek, a konfiguráció megőrzése, az integráció tesztelése, a tényleges renderelt képernyők, a futó alkalmazás kiadása/csomagolása és cseréje.

## Interfész szerződés
A vezérlő rendelkezik a meglévő APIs plusz a szalag LastStatus, a szalag HasRunningActions, a szalag IsRunning(Guid), a szalag GetTileStatus(Guid), a szalag ForegroundProcess, a szalag CorsairAvailable, az IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, a Task TestCorsairColorAsync(string), a vételi OpenIcue().

## elfogadás
Nincs valós felhasználói konfiguráció újratelepítése; érvényes lépésbeli változások mentése egyszer, nem érvényes tervezések maradnak szerkeszthető, discard helyreállítása mentett adatok. Native hotkey illusztrációk validált injekció nélkül teszt bejegyzései kapcsolattartó ablakok. Tile és macro reorder tesztelt; minden nézet render a desktop méretben; futó / hibás / teljes állapot látható; valós SDK eszköz modell megjelenik.

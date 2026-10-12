<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-12-responsive-layout.md)

# Felelősségteljes WPF felület

A fő oldalak a használható tartalmak szélességétől, nem pedig a rögzített ablak szélességétől függnek. A 900 DIP tartalmak szélességének a lemez és a szerkesztő között állnak. A 620 DIP-k között a jelek kétoldalúak. A 820 DIP-k szélességének a lapja be van kapcsolva. A minimális méret: 640 × 480 DIP-k.

A profil és a beállítások, valamint a billentyűzet vezérlő elemei a tényleges hálózati szélesség alapján változnak. A meglévő vezérlő elemek átszervezhetők az elemek újratelepítésének helyett. A cselekvési kártyák megosztják a rendelkezésre álló vonal szélességét. Az eszközlapok megszakadnak. A billentyűzet oldala függőleges, a fizikai billentyűzet megtartja arányát.

Automatikus WPF-Teszt: mind az öt fő oldal 640 × 480, 900 × 620 és 1440 × 900; A képernyőfelvételek oldalonként; A szögek mindkét irányban történő megváltoztatása a kapott szöveg ellenőrzésével. WPF-egységek - valódi Windows-Skaliáció 150/200 A többszázalékos monitorok még nem érvényesültek.

Tesztelési állapot: Release-Build hibák/figyelmeztetések nélkül. Teljes WPF Smoke teszt minden méretű, oszlopváltozás, billentyűzet elrendezése és nyelvvizsgálat. git diff --check

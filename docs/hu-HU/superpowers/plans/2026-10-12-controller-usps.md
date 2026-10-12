<!-- generated-doc locale=hu-HU source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/plans/2026-10-12-controller-usps.md)

# A bővítés tervezete

**Cél:** A megadott monitor, a gesztus, a munkafolyamat, a felvétel, a készülék diagnosztizálása, a hordozható profilcseré és a nyomtatás javítása.

**Architektúra:** Továbbá a meglévő alapokat/Windows/Tiszta állami gépek és hordozható konfigurációs logika élő Core, a szülő integrációk Windows és a modális tervezési szerkesztők a Desktop. megőrizze a meglévő piszkos munkaterület szerkesztéseit és a telepített felhasználói konfigurációt.

**Spec: ** ../specs/ 2026-10-12-kontroll-usps-design.md

- [x] Monitoring szolgáltatás, célállóság és aszinkron élő állapot; tesztelése nem elérhető azonosítók és törlés.
- [x] Gesztenyészeti gépek és tartós rétegek; teszt szélességek, ismétlés, újratelepítés és a meglévő azonnali kötések.
- [x] A munkafolyamat feltételei, a előrehaladott ágak és a hiba politikája; tesztfelvételek és törlés.
- [x] A rövid leírás a tervezési felülvizsgálattal; a tesztszűrő és a hajtás elhelyezése.
- [x] Megosztott mappák profilcserélése felülvizsgálati ellenőrzésekkel és előrejelzésekkel; teszt konfliktusok és hordozható mappák.
- [x] Általános készülékre vonatkozó szabványos speciális kulcsok és diagnosztika; tesztelési parszívó, duplikált mapping és szünet viselkedés.
- [x] Tray/window/executable ikon források és szünetelt megjelenés; ellenőrizze a források közzétett executable.
- [x] Integrálja az összes palettát/szerkesztő bejegyzési pontot és a lokalizált szöveget; fut Core, Windows és WPF ellenőrzések.
- [x] Közzéteszi a külön lépcsőjegyzékben, megőrizze a felhasználói beállításokat, és indítsa újra a frissített alkalmazást.

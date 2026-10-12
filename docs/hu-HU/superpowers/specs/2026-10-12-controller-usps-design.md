<!-- generated-doc locale=hu-HU source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO ellenőrző kiterjesztések

A felhasználó minden kilenc javasolt javítást és egy felismerhető nyom ikonot kért. A meglévő konfigurációknak, különösen a G1/G2 fényességnek, kompatibilisnek kell maradnia. A meglévő nem megrendelt márkák és dokumentációs szerkesztések a megosztott munkaterülethez tartoznak, és meg kell őrizni őket.

## Szállítmányok és határértékek

1. A fényesség intézkedések kiválasztják az összes támogatott monitorot vagy egy stabil specifikus monitor ID-t. A hiányzó kifejezett célok láthatóan nem sikerülnek. A képernyő jelenlegi fényességét, a kimeneti térfogatot, a mikrofon állapotát és a OBS állapotát a korlátozott aszinkron olvasásokon keresztül mutatja.
2. Speciális kulcsok támogatják azonnali nyomtatás, tap, tartás és dupla tap, ismételt eltávolítással. A tartott réteg kiválasztó ideiglenesen megváltoztatja az aktív mapping és szabadítás helyreállítja. A meglévő azonnali kötések megtartják viselkedésüket. Szünet, profilváltozások, kapcsolódások és törlések egyértelmű cselekvések.
3. A munkafolyamat tervező szerkesztése feltételek, előre ágak és a stop/continue hibás politika tranzakciós módon egy tile tervezet. feltételek fedezze a fájlokat, futási folyamatok és ablak címek. Nincs korlátlan ugrás ciklusok.
4. A pontos rögzítés rövid időtartamokat és várakozást rögzít, figyelmen kívül hagyja az injekciót és megáll a Escape-on.
5. A készülékre vonatkozó szabványos speciális kulcsok más gyártók használata Windows Raw Input. A tulajdonos kulcsok támogatott szolgáltató API-t vagy szolgáltató mappát igényelnek egy szabványos kulcsra; a nem támogatott funkcionalitás tisztességesen jelenik meg. A diagnózis a profilot, a szünetet, a kötelező konfliktusokat és a valós adapter állapotot magyarázza.
6. Profilcserélő használ egy felhasználó által kiválasztott megosztott mappát (beleértve a mappákat, amelyeket egy meglévő felhő kliens szinkronizál). A felülvizsgálat ellenőrzése megakadályozza a csendes feliratkozást. A Pull előre látható változások és importálható cselekvések a hardver mapping eltávolítása. A cselekvési tartalom beilleszkedése kifejezetten. Nincs felhő hitelesítés tárolva MAKRIVO.
7. A nyelvtanfolyamok a 27 nyelvi katalógusokhoz tartoznak; a DE/EN/FR/IT továbbra is a felülvizsgált dokumentációs nyelvek minimális szintje.
8. A felismerhető M billentyűzet jelölése helyettesíti a generikus Windows billentyűzet ikonját. a szünetelt módban egy amber szünet jelölés van. a végrehajtható/széljelölés ugyanazt a jelölést használja.

## Ellenőrzés

A kulcsellenőrzések a hátra kompatibilitást, a snapshots, a törlés, a gesztus időzítés, a munkafolyamat ágai / hibái, a rögzítő szűrő és a szinkronizációs konfliktusok fedik le.. Windows ellenőrzése csak olvasási megjelenítési listát, SDK kiadás események és Raw Input Parsó. WPF a füst ellenőrzések fedik az új bejárati pontokat és erőforrásokat anélkül, hogy megváltoztatta a monitor valódi fényességét, más alkalmazásba írva vagy felhasználói profilok felírásával. G1/G2 Konfiguráció.

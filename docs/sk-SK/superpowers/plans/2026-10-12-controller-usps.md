<!-- generated-doc locale=sk-SK source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Plán implementácie rozšírenia ovládača

**Cieľ:** Implementujte požadovaný monitor, gesto, pracovný postup, nahrávanie, diagnostiku zariadenia, prenosnú výmenu profilov a vylepšenia zásobníkov.

**Architektúra:** Rozšírte existujúce vrstvy Core/Windows/Desktop. Čistý stav automatov a prenosná konfiguračná logika fungujú v Core, natívne integrácie v Windows a modálne editory návrhov v Desktop. Zachovať existujúce nečisté úpravy pracovného priestoru a nasadenú používateľskú konfiguráciu.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitorovanie služby, cieľovej perzistencie a asynchrónneho živého stavu; otestovať nedostupné ID a zrušiť.
- [x] Stavový automat a držané vrstvy; testovať hrany, opakovať, resetovať a existujúce okamžité väzby.
- [x] Podmienky pracovného toku, dopredné vetvy a chybová politika; testovacie snímky a zrušenie.
- [x] Explicitné skrátené nahrávanie s návrhom revízie; testovacie filtrovanie a likvidácia háčikov.
- [x] Výmena profilu zdieľaného priečinka s kontrolami revízií a náhľadmi; testovacie konflikty a prenosné mapovania.
- [x] Všeobecné štandardné špeciálne kľúče a diagnostika pre jednotlivé zariadenia; testovacia analýza, duplicitné mapovania a pozastavenie.
- [x] Zásobník/okno/spustiteľné zdroje ikon a pozastavený vzhľad; overiť zdroje v publikovanom spustiteľnom súbore.
- [x] Integrácia všetkých vstupných bodov palety/editorov a lokalizovaného textu; spustite kontroly Core, Windows a WPF.
- [x] Publikovať do samostatného prípravného adresára, zachovať používateľské nastavenia a reštartovať aktualizovanú aplikáciu.

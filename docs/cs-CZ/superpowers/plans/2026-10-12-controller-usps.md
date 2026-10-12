<!-- generated-doc locale=cs-CZ source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Plán implementace rozšíření ovladače

**Cíl:** Implementujte požadovaný monitor, gesto, pracovní postup, nahrávání, diagnostiku zařízení, přenosnou výměnu profilů a vylepšení zásobníků.

**Aarchitektura:** Rozšiřte stávající vrstvy jádra/Windows/desktopu. Čistě stavové stroje a přenosná konfigurační logika fungují v Core, nativní integrace v Windows a modální editory návrhů v Desktopu. Zachovejte stávající nečisté úpravy pracovního prostoru a nasazenou uživatelskou konfiguraci.

**Sspec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitorování služby, cílové perzistence a asynchronního živého stavu; testovat nedostupná ID a zrušení.
- [x] Stavový stroj gest a držené vrstvy; otestujte hrany, opakujte, resetujte a existující okamžité vazby.
- [x] Podmínky pracovního postupu, dopředné větve a chybová politika; zkušební snímky a zrušení.
- [x] Explicitní záznam zkratky s návrhem revize; zkušební filtrování a likvidace háčků.
- [x] Výměna profilu sdílené složky s kontrolami revizí a náhledy; testovací konflikty a přenosná mapování.
- [x] Obecné standardní speciální klíče a diagnostika pro jednotlivá zařízení; testovací analýza, duplicitní mapování a pauza.
- [x] Zásobník/okno/spustitelné zdroje ikon a pozastavený vzhled; ověřit zdroje v publikovaném spustitelném souboru.
- [x] Integrujte všechny vstupní body palety/editoru a lokalizovaný text; spusťte kontroly jádra, Windows a WPF.
- [x] Publikovat do samostatného pracovního adresáře, zachovat uživatelská nastavení a restartovat aktualizovanou aplikaci.

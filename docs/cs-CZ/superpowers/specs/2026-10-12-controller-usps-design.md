<!-- generated-doc locale=cs-CZ source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# Rozšíření ovládání MAKRIVO

Uživatel požadoval všech devět navrhovaných vylepšení a rozpoznatelnou ikonu na hlavním panelu. Stávající konfigurace, zejména jas G1/G2, musí zůstat kompatibilní. Stávající nepotvrzené úpravy značky a dokumentace patří do sdíleného pracovního prostoru a musí být zachovány.

## Dodávky a hranice

1. Akce jasu vyberou všechny podporované monitory nebo stabilní ID konkrétního monitoru. Chybějící explicitní cíle viditelně selhávají. Dashboard zobrazuje aktuální jas, výstupní hlasitost, stav mikrofonu a stav OBS prostřednictvím ohraničených asynchronních čtení.
2. Speciální klávesy podporují okamžité stisknutí, klepnutí, podržení a dvojité klepnutí s potlačením opakování. Podržený volič vrstvy dočasně změní aktivní mapování a uvolněním je obnoví. Stávající okamžité vazby si zachovávají své chování. Pozastavení, změny profilu, odpojení a zrušení jasná čekající gesta.
3. Návrhář pracovního postupu upravuje podmínky, dopředné větve a zásady zastavení/pokračování při selhání transakčně v konceptu dlaždic. Podmínky pokrývají soubory, běžící procesy a názvy oken. Žádné neomezené cykly skoků.
4. Explicitní nahrávání zachycuje zkratkové akordy a čeká, ignoruje vložený vstup a zastaví se na Escape. Zaznamenané akce jsou před přijetím přezkoumány; běžný text se nezaznamenává.
5. Standardní speciální klíče pro jednotlivá zařízení od jiných výrobců používají Windows Raw Input. Proprietární klíče vyžadují podporované API dodavatele nebo mapování dodavatele na standardní klíč; nepodporovaná funkce je zobrazena čestně. Diagnostika vysvětluje profil, pauzu, konflikty vazeb a aktuální stav adaptéru.
6. Výměna profilů používá uživatelem vybranou sdílenou složku (včetně složek synchronizovaných stávajícím cloudovým klientem). Kontroly revizí zabraňují tichému přepsání. Pull zobrazí náhled změn a importuje zakázané akce s odstraněným mapováním hardwaru. Zahrnutí obsahu akce je explicitní. MAKRIVO neukládá žádné přihlašovací údaje cloudu.
7. Řetězce uživatelského rozhraní patří do stávajících katalogů v jazyce 27; DE/EN/FR/IT zůstávají minimálními revidovanými jazyky dokumentace. Překladové mezery musí být zveřejněny, nikoli prohlašovány za úplné.
8. Obecnou ikonu Windows na hlavním panelu nahrazuje rozeznatelná modrozelená značka klávesnice M. Pozastavený režim má oranžový odznak pozastavení. Značka spustitelného souboru/okna používá stejnou značku. Popisek obsahuje stav a profil.

## Ověření

Základní kontroly pokrývají zpětnou kompatibilitu, snímky, zrušení, načasování gest, větvení/chyby pracovního postupu, filtrování záznamníků a konflikty synchronizace. Windows kontroluje výčet zobrazení pouze pro čtení, události vydání sady SDK a analýzu Raw Input. Kontroly kouře WPF pokrývají nové vstupní body a zdroje bez změny skutečného jasu monitoru, psaní do jiné aplikace nebo přepisování uživatelských profilů. Nasazení zachová zálohu a konfiguraci G1/G2.

<!-- generated-doc locale=sk-SK source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# Rozšírenia ovládania MAKRIVO

Používateľ požadoval všetkých deväť navrhovaných vylepšení a rozpoznateľnú ikonu na paneli. Existujúce konfigurácie, najmä jas G1/G2, musia zostať kompatibilné. Existujúce nepotvrdené úpravy značky a dokumentácie patria do zdieľaného pracovného priestoru a musia byť zachované.

## Dodávky a hranice

1. Akcie jasu vyberú všetky podporované monitory alebo stabilné špecifické ID monitora. Chýbajúce explicitné ciele viditeľne zlyhávajú. Prístrojová doska zobrazuje aktuálny jas, výstupnú hlasitosť, stav mikrofónu a stav OBS prostredníctvom ohraničených asynchrónnych čítaní.
2. Špeciálne klávesy podporujú okamžité stlačenie, ťuknutie, podržanie a dvojité ťuknutie s potlačením opakovania. Podržaný volič vrstiev dočasne zmení aktívne mapovanie a uvoľnenie ho obnoví. Existujúce okamžité väzby si zachovávajú svoje správanie. Pozastavenie, zmeny profilu, odpojenia a zrušenie jasné čakajúce gestá.
3. Návrhár pracovného toku transakčne upraví podmienky, dopredné vetvy a politiku zastavenia/pokračovania zlyhania v dlaždicovom koncepte. Podmienky pokrývajú súbory, spustené procesy a názvy okien. Žiadne neobmedzené cykly skokov.
4. Explicitné nahrávanie zachytáva skratkové akordy a čaká, ignoruje vložený vstup a zastaví sa pri Escape. Zaznamenané činnosti sa pred prijatím skontrolujú; obyčajný text sa nezaznamenáva.
5. Štandardné špeciálne kľúče pre jednotlivé zariadenia od iných výrobcov používajú Windows Raw Input. Proprietárne kľúče vyžadujú podporované API dodávateľa alebo mapovanie dodávateľa na štandardný kľúč; nepodporovaná funkcia sa zobrazuje čestne. Diagnostika vysvetľuje profil, pauzu, konflikty väzieb a aktuálny stav adaptéra.
6. Výmena profilov používa zdieľaný priečinok vybratý používateľom (vrátane priečinkov synchronizovaných existujúcim cloudovým klientom). Kontroly revízií zabraňujú tichému prepisovaniu. Pull zobrazí ukážky zmien a importuje zakázané akcie s odstránenými hardvérovými mapovaniami. Zahrnutie obsahu akcie je explicitné. MAKRIVO neukladá žiadne prihlasovacie údaje do cloudu.
7. Reťazce používateľského rozhrania patria do existujúcich katalógov v jazyku 27; DE/EN/FR/IT zostávajú minimálne kontrolované jazyky dokumentácie. Prekladové medzery musia byť zverejnené a nie vyhlásené za úplné.
8. Rozoznateľná modrozelená značka klávesnice M nahrádza generickú ikonu Windows na paneli. Pozastavený režim má žltý odznak pozastavenia. Značka spustiteľného súboru/okna používa rovnakú značku. Popis obsahuje stav a profil.

## Overenie

Základné kontroly pokrývajú spätnú kompatibilitu, snímky, zrušenie, načasovanie gest, vetvenia/chyby pracovného toku, filtrovanie rekordéra a konflikty synchronizácie. Windows kontroluje enumeráciu zobrazenia len na čítanie, udalosti vydania súpravy SDK a analýzu Raw Input. Kontroly dymu WPF pokrývajú nové vstupné body a zdroje bez zmeny skutočného jasu monitora, zadávania do inej aplikácie alebo prepisovania používateľských profilov. Nasadenie zachová zálohu a konfiguráciu G1/G2.

<!-- generated-doc locale=sk-SK source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: šesť rozšírení produktu a adaptéry klávesnice

Dátum: 2026-10-12
Stav: navrhnutý dizajn na kontrolu používateľom; implementácia nezačala.

## Cieľ
Získajte všetky 6 požadovaných rozšírení v existujúcom Windows/WPF - zachovanie existujúcich profilov, G1/G2 úlohy, lokálna prevádzka a bezúčtovná aplikácia. rozšíriť integráciu klávesnice na Razer a Logitech, s opätovným využitím kapacity založenou na architektúre pre iných výrobcov. názov výrobcu alebo rozloženie klávesnice nie je nikdy dôkazom, že sú podporované vlastné ovládacie prvky.

## Architektonické rozhodnutie
rozšírenie Aivana.Core s serializovateľnými zmluvami a validáciou, Aivana.Windows s Windows a dodávateľ adaptérov, a Aivana.Desktop s transakčnými editormi a stavovými zobrazením. Použite samostatné spoločníkové projekty pre izolované plagáty a miestne mobilné panelové služby. Vyhnite sa reštartovaniu alebo jednom privilegovanom procese hostovanie každého rozšírenia. Existujúca konfigurácia musí byť nahraná nezmenené; doplniť akcie namiesto reštartovať svoje trvalé hodnoty.

Alternatívy: monolitická implementácia by zahŕňala hardvér, sieť a prevádzku pracovného toku; WPF by sa pridala nezávislá migračná práca. modulárne rozšírenia sa odporúčajú.

## 1Nezávislá klávesnica Macro Pad
Uložte prepojenia podľa identity zariadenia plus fyzický kód skenovania a rozšírené kľúčové vlajky. Pridajte explicitné pozorovanie a exkluzívne režimy, detekcia zariadenia, kľúčové zachytenie, opakované potlačenie, gesty, držať vrstvy a znova pripojiť diagnostiky. Udržiavať hlavné / numpad rozdiely Enter a testovať nemecké / americké rozloženia.

Raw Input Exkluzívna odstraňovanie na zariadenie vyžaduje skutočný filter; nikdy korelovať globálne nízke úrovne s Raw Input a tvrdí spoľahlivé zariadenie-specifické odstránenie. poskytnúť monitor, núdzové uvoľnenie, behavior-open a inštalátor/uninstaller. Nechytiť bezpečné desktop vstup. ovládacieho zariadenia a exkluzívny režim aktivácie sú explicitné samostatné užívateľské akcie.

Driver gate: vyberte podporovaný, riadne licencovaný a podpísaný zálohový okruh kompatibilný s cieľom Windows 11 konfigurácie, alebo vyvinúť/podpísať špeciálny filter. Interception je kandidát na hodnotenie, nie záväzná produkcia závislosť: jeho oficiálne README popisuje testovanie prostredníctvom Windows 10 a odlišné komerčné licencie. až kým sa tento port nesie, musí sa exkluzívny režim ukázať ako nedostupný; funkcia sa nemôže vyhlásiť ako úplná.

Akceptácia: s dvoma fyzickými klávesnicami, rovnaký kľúč vykonáva makro a nevytvára normálny vstup aplikácie len na vybranom výlučnom zariadení; ostatné vstupy klávesnice zostávajú neovplyvnené.

## 2- nastavenie pracovného priestoru
Pridajte čarodejníka pre pracovisko, stretnutie, streamovanie, výskum a projektové prepínanie. Objavte nainštalované aplikácie, audio zariadenia, monitory a OBS Scény. Používatelia si vyberajú ciele a prepojenia; predvídajú výsledky a validujú chýbajúce požiadavky. Uložiť len po preskúmaní, bez akcií počas nastavenia. Generované profily zostávajú upravené v existujúcich editoroch.

Akceptácia: vytvorenie pracovného profilu bez manuálneho úpravy vykonateľných ciest alebo identifikátorov zariadenia; zrušenie necháva nastavenia nezmenené; chýbajúce softvér má vykonateľné vysvetlenie.

## 3overené vykonanie
Zadajte prehľad, typované výsledky a voliteľné post-podmienky. Rozlišovať požadované, dokončené, overené, neúspešné, časovo ukončené, zrušené a neovplyvnené stavy. Overte audio/mikrofón, jasnosť, proces/prístupnosť okna a OBS stav s obmedzenými asynchrónnymi čítaním. krátke/textové doručenie nie je dôkazom, že cieľová aplikácia prijala príkaz. Vonkajšie vedľajšie účinky nie sú univerzálne reverzibilné; ponúkajú kompenzáciu len v prípade, že sa implementuje.

Akceptácia: odpojiť explicitné ciele nikdy ticho späť; nesprávne pozorovaný stav nikdy neoznámil overený úspech; zrušenie zastaví vyhľadávanie; zlyhanie identifikuje krok a recovery akcie.

## 4Plugins a zdieľateľné balíky
Verziálne JSON prejavy vyhlasujú akcie, parametre schémy, štátnych poskytovateľov, závislosť a schopnosti. Spustiť vykonateľné pluginy v samostatnom procese cez obmedzené miestne IPC s ručným škrupinou, termínmi a zrušením. Odmietnuť zlé správy a nezlučiteľné verzie; riešiť havárie bez ukončenia ovládača.

Poskytnite prehľad balíka export/import, obsahové hashy, ochrana cestovného ruchu, rozmerové limity, verzia/závislosť kontroly, explicitné povolenie a upgrade rollback. Importované plagáty nesmú spustiť automaticky. Zahŕňať pracovný príklad plugin s koncom-to-end inštalácia/aktívna/štátna/uninstall tok. Žiadna verejná služba na trhu nie je potrebná pre toto vydanie.

Akceptácia: vytvoriť, vyvážať, inštalovať a vykonávať skutočnú integráciu; preukázať časový výstup, izoláciu havárií, zlyhanie závislosti, priechodné odmietnutie a zlyhanie dovozu.

## 5. Windows UI automatizácia
Pridajte prvok piker a obmedzené akcie na odvolanie, hodnotu, výber, zameranie, zameranie a čakanie na prvok/stát. Trvať na výberov pomocou procesu, automatizácie ID, kontrolného typu a kontext; vyhnúť sa koordinátovo-pouze zaznamenávanie. Zobraziť výberov súčet a testov výberov čítanie-pouze. Windows Obmedzenia úrovne integrity výslovne.

Rozšíriť nahrávanie s overiteľnými akciami UIA, zachovať aktuálne krátke nahrávanie. Ambigúty selektory nefungujú namiesto kliknutia na dobrovoľnú kontrolu. Normálne prístupné stromčeky nie sú sľubom plného automatizácie DOM.

Akceptácia: automatizácia testovanej aplikácie so stabilnými výbermi pozície/rozmeru; testová ambigua, chýbajúce prvky, funkčné ovládacie prvky, časové výstupy a zrušenie.

## 6Telefón / tabletová tabuľka
Poskytnite odpovedajúci panel z opt-in LAN služby. Objaviť výslovne pomocou krátkodobého jednorazového kódu a vydávať odvolateľné overenia chránené lokálne. Použite HTTPS s dokumentovaným lokálnym certifikátom dôvery / odvolanie; žiadne vrátenie odosielanie overenia v žalobnom texte. Predvolené na zrušené a obmedziť konfigurované rozhrania. Validácia pôvodu, overené príkazy, limity sadzby a obmedzené správy sú povinné.

Vystaviť povolené profily/akcie, živý stav, pokrok a zrušenie; používať stabilné ID a rovnaké výkonové kľúče ako desktop a fyzické kľúče. Nevystaviť dobrovoľné príkazy, vykonateľné cesty, tajomstvá alebo inštalácia pluginu. Poskytovať pripojenie, párovanie a odvolanie obrazovky. Žiadny cloud účet alebo internetový relay je zahrnutý.

Akceptácia: prevádzkuje z reálneho telefónu/tabletu na rovnakej LAN; nepáčaní klienti nemôžu zaznamenať alebo spustiť akcie; odvolané tokeny nefungujú; duplikované/rehrávané príkazy nefungujú dvojnásobne; diskonektívne sa nefunguje.

## Výrobca Keyboard

| adaptér / rodina | Plánovaná integrácia | Vysvetlenie hranice |
| --- | --- | --- |
| Corsair | Zachovať aktuálne iCUE G-kľúč vstup a RGB integrácia | Model/softvér detekcia kapacity zostáva potrebná |
| Logitech G | G-key SDK adaptér s tlačou/odovzdaním a prevádzkovým časom; osvetlenie prostredníctvom samostatného SDK, kde je podporovaný | Nezrovnajte detekciu G HUB s podporovanou G-kľúčovou API na každom modeli |
| Razer | Chrómový osvetľovač a adaptér Synapse- štandardné kľúčové mapovanie, plus generický vstup HID | Chroma je osvetľovací API; nezaznamenajte ho vlastným makro-kľúčovým vstupom |
| SteelSeries | GameSense Stavový/svetľovací adaptér plus generický vstup HID | GameSense udalosť nie je všeobecný vlastný kľúčový úder API |
| Wooting | Analog SDK adaptér pre podporované analógové vstupy, plus normálne kľúče HID | Kapacity a podmienky distribúcie SDK musia byť overené podľa podporovaného modelu |
| Keychron/QMK/VIA | Štandardné kľúče HID a pokyny pre mapovanie firmvéru; známe rozloženia iba s dôkazmi | VIA Podpora konfigurácie nie je univerzálny runtime makro-key API |
| Ďalšie štandardné klávesnice | fyzické kľúčové objavovanie a mapovanie; exkluzívny režim iba s validovaným filtrovým záložným okom | Žiadna výrobná podpora pre modelové, dialové alebo vlastné kľúče |

Každé zariadenie zobrazuje detekované, pozorované, mapované prostredníctvom softvéru dodávateľa, domáceho SDK, ovládača-exkluzívny, nedostupný alebo hardvérový-netestovaný stav ako je to možné.

## Doručenie objednávky
1. Konfigurácia kompatibility, kapacity zmluvy a overené vykonávanie.
2. Sprievodca nastavenia pomocou skutočného objavovania a existujúcich akčných vykonávateľov.
3. UI automatizácia a jej rekordér / test fixácie.
4. Plugin hostiteľ, balíček tok a príklad integrácie.
5. Platené panelové a mobilné overenie.
6. Rozšírené adaptérov a exkluzívnej integrácie klávesnice s skutočnou hardvérovou validáciou.

Driver podpis / licencia, nedostupné SDK alebo neprítomné testovanie hardvéru sú uvoľňovanie brány, nie dôvody na nahradenie mock správanie a volanie práce dokončené.

## Kritériá overovania a uvoľňovania
prebiehajúce jadro, Windows a WPF kontroly a pridať zamerané integrácie testy pre každú novú hranicu. overiť JSON okrúhly výlety a dedičné profily; testovať dovezený vykonateľný obsah, sieťové overenie, zrušenie, plugin havárií a pripojenie zariadenia. overiť desktop a mobilné rozloženia. overiť vysoký Windows rozširovanie samostatne. vydať objekty len po prechode kontrol; rozlišovať implementované/testované, implementované/hardware-testované a externe zablokované funkcie v poznámkach pre vydanie. Nezačnite nahradiť inštalovanú aplikáciu, nainštalovať ovládače, zmeniť skutočné mapovanie klávesnice alebo otvorený prístup k sieti ako incidentálny test.

## Dôkaz
- Microsoft Raw Input Semantika v ňom: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G vývojár SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Intercepčné skladovanie a závislosť podmienky: https://github.com/oblitum/Interception
- Existujúci inventár výrobcu skladov: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Preskúmať rozhodnutia
Používateľ je požiadaný o schválenie tohto písomného návrhu pred plánom implementácie. Hlavnou vonkajšou závislosťou je výrobný-príjemný výlučný vstupný ovládač. Odporúčaný rozsah zachováva tento ako otvor dodateľný namiesto tichého zníženia jeho Raw Input a pozorovania .

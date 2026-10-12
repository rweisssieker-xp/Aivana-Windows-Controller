<!-- generated-doc locale=hu-HU source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: hat termékkiterjesztés és billentyűzet adapterek

dátum: 2026-10-12
Állapota: javasolt tervezés a felhasználói felülvizsgálathoz; a végrehajtás nem kezdődött.

## cél
A meglévő Windows/WPF alkalmazásban a hat kiterjesztés megőrzése, a meglévő profilok megőrzése, a G1/G2 feladatok megőrzése, a helyi működés és a számlátlan használat. A billentyűzet integrációjának kiterjesztése Razer és Logitech, a többi gyártó számára újrahasznosítható kapacitási építészetével. A gyártó neve vagy billentyűzet elrendezése soha nem bizonyítja, hogy a tulajdonosok ellenőrzéseit támogatják.

## Architektúra döntés
Tovább Aivana.Core serializable szerződések és validáció, Aivana Windows a Windows és a kereskedő adapterek, és Aivana.Desktop tranzakciós szerkesztők és állapot nézetek. Használja a külön társas projektek elszigetelt plugin végrehajtás és a helyi mobil panel szolgáltatás. Kerülje a rewrite vagy egy privileged folyamat tárolás minden kiterjesztés.

Alternatívák figyelembe: egy monolitikus végrehajtás beépíti a hardver, a hálózat és a munkafolyamat végrehajtását; helyettesítése WPF hozzá fog járni a nem kapcsolódó migrációs munka.

## 1 Független billentyűzet macro pad
Tárolja a kötéseket a készülék azonosítója mellett a fizikai szkennelési kód és a kiterjesztett kulcsszavak. Hozzáadjon egyértelmű megfigyelési és kizárólagos módokat, a készülék felfedezése, a kulcsfogadás, a megismétlődő elzáródás, a gesztusok, a tartott rétegek és a újraindítás diagnosztikák. Tartsa meg a fő/numpad Enter megkülönböztetést és teszteli a német/amerikai elrendezéseket. A kötések egy kiválasztott fizikai billentyűzetet céloznak; a kétértelmű újraindítás identitása újraválasztást igényel, nem pedig más készülék ellenőrzését.

Raw Input csak megfigyelést biztosít. Exclusive per-device eltávolítása tényleges billentyűzet szűrő háttérre van szükség; soha nem korrelálható globális alacsony szintű csomópontok Raw Input és a megbízható eszköz-specifikus eltávolítása. Biztosít egy figyelemfelkeltő, vészhelyzeti felszabadítás, hibás nyitott viselkedés és telepítő/nem telepítő. Ne rögzíti a biztonságos asztali bevitelt.

A járművezető kapu: válasszon egy támogatott, megfelelően engedélyezett és aláírt háttérképet, amely kompatibilis a cél Windows 11 konfigurációval, vagy fejleszti/jelöl egy dedikált szűrőt. A interception jelölt, hogy értékelje, nem egy elkötelezett gyártási függőség: hivatalos README leírja a tesztet Windows 10 és a különleges kereskedelmi licencet. Amíg ez a kapu teljesül, az exkluzív módot nem kell megmutatni; a funkció nem lehet teljesnek nyilvánítani.

Elfogadhatóság: két fizikai billentyűzet esetén ugyanaz a kulcs egy makrot futtat, és nem termel normális alkalmazásbevételt csak a kiválasztott kizárólagos eszközön; a billentyűzetbevételt nem érinti.

## 2 A munkavégzés útmutatója
Hozzon hozzá egy varázslót a munkahelyre, a találkozókra, a streamingre, a kutatásokra és a projektcserere. Fedezze fel a telepített alkalmazásokat, hangeszközöket, monitorokat és OBS jeleneteket. A felhasználók célokat és kötéseket választanak; előre megtekinthetik az eredményes lépéseket és érvényesíthetik a hiányzó követelményeket. Csak felülvizsgálat után menthetik meg, a beállítás során nem futó cselekedetek. A generált profilok szerkeszthetők a meglévő szerkesztőkben.

Elfogadhatóság: munkaprofil létrehozása manuálisan nem szerkeszthető útvonalak vagy eszköz azonosító; törlés lapok beállítások változatlan; hiányzó szoftver van egy működőképes magyarázatot.

## 3. Ellenőrzött végrehajtás
Bevezetés elővilágosítás ellenőrzések, nyomtatott lépésenként eredmények és opcionális utólagos feltételek. Megkülönbözteti a kért, befejezett, ellenőrizte, nem időzített, törölt és nem ellenőrizhető állapotok. Ellenőrizze a hang / mikrofon, fényesség, folyamat / ablak készség és OBS állapot korlátozott aszinkron olvasások. rövid leírás / szöveg szállítás nem bizonyítja, hogy a cél alkalmazás elfogadta a parancsot. Külső mellékhatások nem univerzálisan visszafordítható; kínál kompenzáció csak akkor, ha végrehajtott.

Elfogadhatóság: a kapcsolódott kifejezett célok soha nem esik vissza csendben; a rossz megfigyelés állapota soha nem jelent meg a sikert; a törlés megáll a keresés; a hibák azonosítják a lépést és a helyreállítási intézkedés.

## 4. Plugins és megosztható csomagok
A verzió JSON jelölések nyilatkozatok cselekvések, paraméterek, állami szolgáltatók, függőségek és képességek. fut végrehajtható plugins egy külön folyamat a korlátozott helyi IPC kézzel, határidő és törlés. Elutasítja a rosszformált üzenetek és a nem kompatibilis verziók; kezelni a balesetek megszüntetése nélkül a vezérlő. Képesség nyilatkozatok a felfedezés és a közvetítés, nem követelés az operációs rendszer homokdoboz.

Javasolja a csomag export/import előzetes megjelenítését, tartalmak hashes, út-távolítási védelem, méretkorlátozások, verzió/függőség ellenőrzések, kifejezett engedélyezése és frissítés visszaállítása. Importált végrehajtható plugins nem fut automatikusan. Tartalmazza a munka példája plugin végső-végső telepítés/tevékenység/állam/nem telepítés áramlással. Nincs nyilvános piaci szolgáltatás szükséges erre a kiadásra.

Elfogadhatóság: létrehozni, exportálni, telepíteni és végrehajtani egy igazi példa integráció; demonstrálni időtöltést, katasztrófa elszigeteltség, függőséghiány, átmeneti elutasítás és a fogyatékkal élő behozatal.

## 5. Windows UI automatizálás
Adjon hozzá egy elem picker és korlátozott intézkedéseket az invoke, érték, kiválasztás, tágítás, fókusz és vár egy elem / állapot. Folyamatos kiválasztók a folyamat, automatizációs azonosító, ellenőrzési típus és az ős kontextus; kerülje a koordinátátus-csak rögzítés. Mutassa meg a kiválasztó mérleg és teszt kiválasztók csak olvasás. A jelszó mezők kizárják a rögzítés. Kezelje a nem támogatott ellenőrzéseket és Windows integritási szint korlátozásokat kifejezetten.

Továbbítsa a felvételt felülvizsgálható UIA akciókkal, megtartva a jelenlegi rövid felvételt. A kétségbeesett kiválasztók nem egy önkényes ellenőrzés helyett sikeresek. A normál böngésző-hozzáférhetőség fája nem ígéretet tesz egy teljes DOM automatizálási motorról.

Elfogadhatóság: automatizálja a testre szánt alkalmazást stabil pozíció/méret változásokkal; teszt kétségesség, hiányzó elemek, kikapcsolódott ellenőrzések, időzítések és törlés.

## 6 Helyi telefon/tablett panel
A válaszpanel egy opt-in LAN szolgáltatás. párosítás kifejezetten használja a rövid időt egy időt kód és kiadás visszavonható hitelesítések helyi védelemben. Használja a HTTPS dokumentált helyi tanúsítvány megbízás / visszavonás; nincs visszafordítás küldeni hitelesítések a plaintext. Előzetes, hogy megszünteti és korlátozza a konfigurált interfészek. A származási hitelesítés, hitelesített parancsok, sebességkorlátozás és korlátozott üzenetek kötelező.

Kinyitja a megengedett profilokat/tevékenységeket, az élő állapotot, a haladást és a törléseket; használja a stabil azonosítókat és ugyanazt a végrehajtási láncot, mint az asztali és a fizikai kulcsok. Ne mutatja ki önkényes parancsokat, végrehajtható útvonalak, titkok vagy plugin telepítés. Hozzon létre kapcsolatot, párosítást és visszavonási képernyőket. Nincs felhőfiók vagy internetes relay.

Elfogadhatóság: egy valós telefonról / táblagépről ugyanazon LAN-on működik; a nem csatlakoztatott ügyfelek nem tudnak listázni vagy futtatni a cselekvéseket; a visszavonott tokenek nem működnek; a duplikált / újrajátszott parancsok nem hajtanak végre; a diszkontek nem csendesen visszavonják a cselekvéseket.

## Keyboard gyártó

| Adapter / család | Tervezett integráció | A határértés |
| --- | --- | --- |
| Corsair | Tartsa meg a iCUE G-key bevitelt és az RGB integrációt | A modell/szoftver kapacitása továbbra is szükséges |
| Logitech G | G-key SDK adapter nyomtatással/kiadással és futási idővel; megvilágítás külön SDK-n keresztül, ahol támogatott | Ne egyenlítsük a G HUB felismerést a támogatott G-key API-val minden modellben |
| Razer | Chroma világítási adapter és útmutató Synapse-a szabványos kulcs mappázás, valamint a generikus HID beviteli | A Chroma egy megvilágítási API; ne jelölje meg a saját makro kulcs beviteleit |
| SteelSeries | GameSense állapot/világítási adapter plusz generikus HID beviteli | GameSense esemény benyújtása nem egy általános tulajdonos kulcsfüzet API |
| Wooting | Analog SDK adapter támogatott analóg beviteli, plusz normális HID kulcsok | A kapacitások és az SDK elosztási feltételei támogatott modelltől függően ellenőrizni kell |
| Keychron/QMK/VIA | Standard HID kulcsszavak és a firmware-mapping utasítások; ismert elrendezések csak bizonyítékokkal | VIA konfigurációs támogatás nem egy univerzális futási macro-kulcs API |
| Egyéb standard HID billentyűzetek | Fizikai kulcs felfedezése és mappázása eszközönként; kizárólagos mód csak a validált szűrő hátterével | Nincs gyártott modell-specifikus Fn, dial vagy tulajdonos kulcs támogatás |

Minden eszköz kimutatott, megfigyelhető, mappált a szállító szoftver, a szülői SDK, a vezető-exkluzív, nem elérhető, vagy a hardver-teszteződés állapotát, mint alkalmazható.

## szállítási rend
1. Konfigurációs kompatibilitás, kapacitási szerződések és ellenőrzött végrehajtás.
2. A valós felfedezés és a meglévő cselekvési végrehajtók segítségével vezérelt beállítások.
3. UI Automatizálás és a rekord / teszt rögzítés.
4. Plugin host, csomagáramlás és példamegszint integráció.
5. A mobilos ellenőrzés és a LAN panel.
6. Kiterjesztett adapterek és exkluzív billentyűzet backend integráció, valós hardver validáció.

A járművezető aláírás/licencing, nem elérhető SDK-k vagy hiányzó teszthardverek szabadulnak, nem okok a mock viselkedés helyettesítésére és a munka teljesítésére.

## Ellenőrzés és felszabadítás kritériumok
A meglévő magok futtatása, Windows és WPF ellenőrzése és az egyes új határon alapuló integrációs tesztek hozzáadása. Ellenőrizze a JSON körútjait és öröklési profiljait; teszteli az importált végrehajtható tartalmat, a hálózati hitelesítést, a törléset, a plugin-károsodásokat és a készülék újraindítását. Ellenőrizze az asztali és mobil elrendezéseket. Validálja a magas Windows méreteket külön. A kiadásokat csak a ellenőrzések után építheti; különböztesse meg a bevezetett/vizsgált, bevezetett/hardware-tesztezett és külsőleg blokkolt funkciókat a kiadásjegyzékekben. Ne helyettesítse a telepített alkalmazást, telepítse a vezérlőket, változtassa a valós billentyűkártyákat vagy a nyílt hálózathoz

## bizonyíték
- Microsoft Raw Input szemantikus: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G fejlesztő SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- A fogadás és a függőség feltételei: https://github.com/oblitum/Interception
- Meglévő raktár gyártó készlet: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Válaszok felülvizsgálata
A felhasználó kérik, hogy jóváhagyja ezt az írásos tervezést a végrehajtási terv előtt. A fő külső függőség egy gyártásra kész kizárólagos belépési vezető hátter. Az ajánlott területen ez tartható, ahelyett, hogy csendesen csökkenti azt Raw Input megfigyelés.

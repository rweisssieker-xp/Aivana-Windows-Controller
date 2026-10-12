<!-- generated-doc locale=et-EE source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: kuus toote laiendust ja klaviatuuri adapterid

kuupäeva: 2026-10-12
Status: kavandatud disain kasutaja läbivaatamiseks; rakendamine ei ole alanud.

## sihtmärk
Saada kõik kuus taotletud laiendused olemasolevas Windows / WPF rakenduses, säilitades olemasolevaid profiile, G1/G2 ülesandeid, kohalikku toimimist ja konto-vaba kasutamist. Laiendage klaviatuuri integreerimist Razer ja Logitech, kasutades teistele tootjatele taaskasutatavat võimsuspõhist arhitektuuri. Tootja nimi või klaviatuuri paigutus ei ole kunagi tõendusmaterjal, et omandatud kontrollid on toetatud.

## Arhitektuur otsus
Laiendage Aivana.Core serialisatsioonilepingute ja valideerimisega, Aivana Windows Windows ja müüja adapteritega ning Aivana.Desktop tehniliste redaktorite ja staatuse vaadetega. Kasutage eraldi kaasprojekte isoleeritud plugini rakendamiseks ja kohaliku mobiilirakenduse teenindamiseks. Vältige iga laienduse uuesti kirjutamist või ühte privileegset protsessi. Olemasolev konfiguratsioon peab laadima muutumatult; lisage tegevusviisid selle asemel, et reordida nende püsivaid väärtusi.

Alternatiivid arvesse võetud: monolüütiline rakendamine hõlmab hardware, võrk ja töövoolu rakendamist; asendamine WPF lisaks seotud rände tööd. Modulaarsed laiendused on soovitatavad.

## 1. Sõltumatu klaviatuuri macro pad
Salvestada sidemeid seadme identiteedi ja füüsilise skaneerimise koodi ja laiendatud võti. Lisage selged vaatamisväärsused ja eksklusiivsed režiimid, seadme avastamine, võtmefektsioon, korduv supressioon, käed, hoitud kihid ja uuesti ühendatud diagnoosid. Säilitage peamine/numpad Enter eristused ja proovige saksa/USA paigutusi. Liidud on suunatud valitud füüsilisele klaviatuurile; kahtlemata taasühendamise identiteedid nõuavad teise seadme kontrollimist.

Raw Input Eksklusiivne seadme eemaldamine nõuab tegelikku klaviatuuri filtreerimist; kunagi ei seostata ülemaailmsete madala taseme hookidega Raw Input ja nõuda usaldusväärset seadme-specifikat. pakkuda vaatlejat, erakorralist vabastamist, ebaõnnestunud käitumist ja installeri/uninstaller. Ärge võta turvalist töölaua sisendit..

Juhi värav: valige toetatav, nõuetekohaselt litsentseeritud ja allkirjastatud tagasiside, mis vastab sihtkonfiguratsioonidele Windows 11, või arendage / allkirjastage pühendatud filter. Interception on kandidaat hinnata, mitte pühendunud tootmis sõltuvus: selle ametlik README kirjeldab testid läbi Windows 10 ja eraldi kaubandusliku litsentsi. Kuni selle uksega on täidetud, peab eksklusiivne režiim olema kättesaamatu; funktsiooni ei saa deklareerida täielikuks.

Vastuvõetavus: kahe füüsilise klaviatuuri korral täidab sama võti makro ja ei tooda normaalset rakendust ainult valitud eksklusiivse seadmega; teised klaviatuuride sised jäävad puudulikuks. Kontrollige lahti lülitamist, õnnetust, tühistamist, peatamist ja juht eemaldamist tegelikul seadmes.

## 2. Juhitud tööruumi seadistamine
Lisage töökoha, kohtumiste, voogude, teadusuuringute ja projekti vahetamise võlur. Avastage installitud rakendused, heli seadmed, monitorid ja OBS stseenid. Kasutajad valivad sihtmärgid ja sidemed; prognoosida tulenevaid samme ja kinnitada puuduvad nõuded. Saada ainult pärast läbivaatamist, mis ei toimu seadistamise ajal. Loodud profiilid jäävad ajakohastatavaks olemasolevates redigeerimisvormides.

Vastuvõetavus: luua tööprofiil ilma käsikäes redigeerimata teed või seadme ID-d; tühistamine jätab seadeid muutumatuks; puuduv tarkvara on aktiveeritav selgitus.

## 3. Tõestatud täitmine
Sisestage eelvaateid, kohandatud sammult tulemusi ja valikuväliseid järeltingimusi. Eristada taotletud, täidetud, kontrollitud, ebaõnnestunud, ajakohastatud, tühistatud ja kontrollimatuid seisundeid. Kontrollige audio/mikrofon, heledus, protsessi/prügikindlus ja OBS seisund piiratud sünkrooniliste lugemistega. Lühidalt/teksti tarnimine ei ole tõend, et sihipärane rakendus on käsu vastu võtnud. Välised kõrvalmõjud ei ole universaalselt pöördumatud; nad pakuvad kompenseerimist ainult siis, kui neid rakendatakse.

Vastuvõetavus: lahti lülitatud selged eesmärgid ei kukkunud kunagi vaikselt tagasi; valesti täheldatud seisund ei ole kunagi tõestatud edu; tühistamine peatab uurimist; ebaõnnestumised tuvastavad sammu ja taastumise meetmeid. Tegevuse ajalugu unustab teksti maksete ja saladuste.

## 4. Plugins ja jagatavad pakendid
Versioonitud JSON manifesteerib tegevusi, parameetreid, riigi pakkujaid, sõltuvusi ja võimeid. Rakendatavate plug-inide käivitamine eraldi protsessis piiratud kohaliku IPC kaudu käsikäes, tähtaegad ja tühistamine. Keelduda ebaõnnestunud sõnumid ja kokkusobimatud versioonid; juhtida õnnetusi ilma lõpetamata kontrolli. Võimsusdeklaratsioonid on avalikustamine ja vahendamine, mitte OS sandboxingu nõue.

Pakkide ekspordi/impordi eelvaade, sisuhašid, teekondlik kaitse, suuruspiirangud, versiooni/sõltuvuse kontrollimine, selgelt lubatud ja uuendatud rollback. Importimine ei tohi automaatselt toimuda. Sisaldab tööjõu plugin, millel on lõpuni install/action/state/uninstall vool. Selle vabastamise jaoks ei ole vaja avalikku turul teenust.

Vastuvõetavus: luua, eksportida, installida ja rakendada tõelist integratsiooni; näidata aja väljalülitamist, õnnetust, sõltuvuse ebaõnnestumist, läbipaistvat tagasilükkamist ja impordit.

## 5. Windows UI automatiseerimine
Lisage elemendi pikki ja piiratud tegevused, et kutsuda, väärtust, valikut, keskenduda ja oodata elemendi/riigi. Püsiv valijad, kasutades protsessi, automaatset ID, kontrolli tüübi ja eelkäija konteksti; vältida koordineerimist ainult salvestamist. Näita selektorit ja testi selektorit ainult lugemiseks. Sõnumeid välistatakse registreerimisest. Töötle toetamata kontrollidega ja Windows terviklikkuse piirangutega selgelt.

Laiendage salvestamist läbivaatavate UIA meetmetega, säilitades praeguse lühiajalise salvestamise. Kahtlikud valijad ebaõnnestuvad selle asemel, et klõpsata tahtlikku kontrolli. Tavaline brauseri kättesaadavuse puu ei ole täielik DOM-mootor.

Vastuvõetavus: automaatselt pühendatud testi rakendus stabiilsete valikutega asukoha / suuruse muutustega; testide ebamugavus, puuduvad elemendid, tühistatud kontrollid, aja väljumised ja tühistamine.

## 6. Kohalik telefon / tablett
Teenindage reageerivat paneeli opt-in LAN teenusest. Paari selgelt kasutades lühiajalist üheaegset koodi ja väljastada tagasi lükatavad tunnused kaitstud kohalikult. Kasutage HTTPS-i dokumenteeritud kohaliku sertifikaadi usalduse/pinningiga; ei tohi tagasisidet avaldada. Eesmärgiks on keelata ja piirata konfigureeritud liidesed. Originaali valideerimine, autentimatud käskud, hindade piirangud ja piiratud sõnumid on kohustuslikud.

Näita lubatud profiilid / tegevused, elava seisundi, edusammud ja tühistamine; kasutada stabiilseid ID-d ja sama täitmise kuju nagu töölaua ja füüsilised võti. Ärge avaldage tahtlikke juhiseid, rakendatavate teed, saladusi või plugini paigaldamist. Pakkuge ühendust, paari ja tagasivõtmise ekraane. Mullakontod ja internetiühendused ei sisaldu.

Vastuvõetavus: tegutseda reaalsest telefonist/tabletti samal LAN-il; ebaõnnestunud kliendid ei saa loetleda ega käivitada tegevusi; tühistatud tokenid ei tööta; duplikat/replayed käsk ei tööta kahekordselt; disconnect ei jäta tegevusi vaikselt tagasi.

## Keyboard tootja ulatus

| Adapter / Perekond | Planeeritud integratsioon | Selgitavad piirid |
| --- | --- | --- |
| Corsair | Säilitage jooksv iCUE G-tõu sissejuhatus ja RGB integreerimine | Mudeli / tarkvara võimekuse tuvastamine on vajalik |
| Logitech G | G-key SDK adapter, millel on trükkimine / vabastamine ja kättesaadavus; valgustus oma eraldi SDK kaudu, kus seda toetatakse | Ärge võrdle G-HUB-i tuvastamist toetatud G-key API-ga igal mudelil |
| Razer | Chroma valgustusadapter ja juhendatud Synapse standard-tõmme kaardistamine, pluss üldine HID sissepääs | Chroma on valgustuspiir; ärge märgistage seda kodumaise makro võtme sissepääsu |
| SteelSeries | GameSense seisund / valgustusadapter pluss üldine HID sisend | GameSense sündmuste esitamine ei ole üldine omandatud võtmefunktsioon |
| Wooting | Analoogne SDK adapter toetatud analoogse sissejuhatuse ja normaalsete HID-tõmbete jaoks | SDK jaotus tingimused peavad olema kinnitatud toetatud mudeli järgi |
| Keychron/QMK/VIA | Standardne HID-tõendid ja firmware-mapping juhised; teadaolevatest paigutustest ainult tõendite abil | VIA konfigureerimise tugi ei ole universaalne käitumisajaga makro võtme API |
| Muud standardsed HID klaviatuurid | Füüsiline võti avastamine ja kaardistamine; eksklusiivne režiim ainult valideeritud filtri taustaga | Ei toodetud mudeli-spetsiifiline Fn, dial või omandatud võtme toetus |

Iga seade näitab tuvastatud, jälgitud, kaardistatud tarnija tarkvara, kodumaise SDK, juhtiv, kättesaamatus või hardware-testimata seisundit vastavalt kohaldamisele. Säilitage võimekuse tõendid adapteri versioonide ja testidava mudeli/firmware identifitseerijate abil.

## Delivery käsk
1. Konfigureerimise ühilduvus, suutlikkuse lepingud ja kontrollitud rakendamine.
2. Juhendatud seade, kasutades tegelikku avastust ja olemasolevaid tegevusjuhti.
3. UI automaat ja selle rekord / testi fikseerimine.
4. Plugin host, pakendivoog ja näide integreerimine.
5. Tasutud LAN-paneel ja mobiilsus.
6. Laiendatud adapterid ja eksklusiivne klaviatuuri tagasiside integreerimine, tegelikud hardware valideerimine.

Kõik kuus on vajalikud. Sõiduki allkirjastamine/litsentsimine, kättesaamatud SDK-d või puuduvad testide seadmed on vabastatud uksed, mitte põhjused, et asendada mock käitumist ja kutsuda tööd täielikuks.

## Kontrolli ja vabastamise kriteeriumid
Käivitage olemasolevad tuumid, Windows ja WPF kontrollivad ja lisavad iga uue piiriga keskendunud integreerimise testid. Kontrollige JSONi ringreisid ja pärandiprofiilid; testige imporditava sisu, võrgustiku autentimist, tühistamist, plugini õnnetusi ja seadme taasühendamist. Kontrollige tööriistad ja mobiilsed seadmed. Valideerige kõrge Windows eraldi. Rakendage vabastamisartikleid alles pärast kontrollimist; eristage rakendatud / katsetatud, rakendamata / tarkvara testimata ja väliselt blokeeritud funktsionaalsust vabastamisaruandes. Ärge asendage installitud rakendust, paigaldage juhid, muutke tegelikku klaviatuuri või avage võrgu juurdepääs juhuslikuks katseks.

## Tõendid
- Microsoft Raw Input Semantika: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G arendaja SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Intertsessi ja sõltuvuse tingimused: https://github.com/oblitum/Interception
- Olemasolevate ladude tootja inventuur: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Vaadake otsuseid
Kasutaja palutakse enne rakenduskava heaks kiita. Peamine välis sõltuvus on tootmiseks valmis eksklusiivne sissetulekujuht. Soovitatav ulatus säilitab selle kui kättesaadavaks, mitte vaikimisi Raw Input vaatluseks.

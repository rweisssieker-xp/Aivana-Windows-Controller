<!-- generated-doc locale=et-EE source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../HANDBOOK.md)

# MAKRIVO — Kasutusjuhend

Tootja: **Aivana**. Dokumentatsiooni seis: **2026-10-12**. Käsiraamat kirjeldab praeguse tööpuu teostust. Eksperimentaalsed integratsioonid ja riistvarast sõltuvad funktsioonid on selgelt märgitud. Ajaloolised aruanded kirjeldavad oma kuupäeva seisu ega anna garantiid uuema järgu kohta.

## Dokumentatsioon ja keeled

Dokumentatsiooni keelevalik avab täieliku käsiraamatu, funktsioonide loendi, tõrkeotsingu, arendusmärkmed, uuringud ja ajaloolised testiaruanded ükskõik millises rakenduse 27 keelest. Loodud tõlked on masintoega ja läbinud struktuurikontrolli; kõiki ei ole emakeelse toimetaja poolt kontrollitud. Tehnilised tunnused ja käivitatavad näited jäävad muutmata. Algallikas on võrdlemiseks kättesaadav. Tõlgitud litsentsianalüüs on teavitav ega asenda kolmandate osapoolte algseid õiguslikke tingimusi ega lõplikku EULA-t.

## Installatsioon ja esimene käivitamine

Esialgne sihtplatvorm on Windows 11 x64. Hoidke kõik levituspaketi failid ja alamkataloogid koos. Käivitage `MAKRIVO.exe`. Lähtekoodi väljavõte ei ole paigaldusprogramm. Iseseisvad paketid sisaldavad oma .NET-i käituskeskkonda; raamistikust sõltuvad paketid vajavad sobivaid .NET 10 Desktop ja ASP.NET käituskeskkondi. ONNX-i ja vastavate funktsioonide abiprotsesside käivitusprogrammid peavad olema paketis kaasas.

1. Käivitage MAKRIVO ja valige Settings jaotises kasutajaliidese keel. Automaatne valik järgib Windows keelt. Klaviatuuripaigutus on eraldi seadistus.
2. Valige profiil ning lisage paan otsinguga toimingukataloogi kaudu. Andke sellele äratuntav nimi ja valige toimingud.
3. Kontrollige samme ja vajaduse korral määrake kiirklahv. Vältige kombinatsioone, mida Windows või sihtrakendus juba kasutab.
4. Salvestage mustand. Salvestamata muudatused ei kuulu salvestatud konfiguratsiooni.
5. Proovige kahjutu tegevus, näiteks dokumendi avamine ja täitmise tagasiside kontrollimine.

Peaakna sulgemisel jääb rakendus tavaliselt teavitusalale tööle. Uuesti avamiseks tehke salveikoonil topeltklõps; väljumiseks kasutage selle menüüd. Koos Windows käivitumine on valikuline ja algselt keelatud. Pausinupp peatab klahviseosed; ikoonile ilmub pausimärk. Töötavaid makrosid saab tühistada kasutajaliidese, salve või seadistatud tühistamise kiirklahvi kaudu; vaikimisi on see `Ctrl+Alt+Escape`.

## Tüübid, tegevused ja komanda palett

Paanid kuuluvad profiilidele ja võivad sisaldada mitut järjestatud sammu. Redaktor toetab salvestamist/loobumist, eelvaadet, tagasivõtmist/uuestitegemist ja sammude ümberjärjestamist. Kataloog pakub eelseadistusi programmide, dokumentide, URL-ide, teksti, lõikelaua, kiirklahvide, meedia, heli, akende, süsteemitoimingute ja toetatud integratsioonide jaoks. Parameetreid nõudev eelseadistus avaneb mustandina.

Avage palett klahvidega `Ctrl+K`, kui MAKRIVO on fookuses. Otsige salvestatud toiminguid, eelseadistusi, profiilivahetust ja tööriistu. Nooleklahvid valivad; Enter aktiveerib; `Ctrl+Enter` redigeerib toetatud üksust; Escape sulgeb. Mõni valmiskäsk käivitub kohe, kuid seadistatud toimingud nõuavad kontrollimist. Eraldi globaalse paleti kiirklahvi saab seadistada; uutes konfiguratsioonides on see `Ctrl+Shift+Space`.

Teksti- ja lõikelauasammud toetavad muutujaid `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ja `{{user}}`. Kuupäevavorming on `yyyy-MM-dd`; kellaajavorming on `HH:mm`. Tundmatud muutujad jäävad muutmata. Lõikelauda loetakse ainult taotluse korral. Lõikelauatoiming kirjutab teksti lõikelauale ilma seda automaatselt kleepimata.

Kasutajaliidesest käivitatud toimingud võivad sihtida viimast välist rakendust. Kui sihtmärk kaob või Windows blokeerib esiplaanile toomise, aktiveerige sihtmärk käsitsi ja proovige uuesti. Klahvisisestuse edukas edastamine ei tõenda sihtrakenduse ülesande täitmist.

## Profiilid ja töökohtade seadistamine

Profiilid võivad vastata protsessi nimele ja akna pealkirja osale. Kui määratud on mõlemad, peavad mõlemad vastama. Mitme vaste korral määrab valiku profiilide järjekord; vaste puudumisel kasutatakse vaikeprofiili. Automaatne valik tuleb eraldi lubada. Lock Profile takistab käimasoleva seansi automaatseid profiilivahetusi.

Avage Workplace Setup menüüst Tools või paletist. Valige programmid, dokumendid, veebisaidid, heli-/kuvasihtmärk või OBS stseen. Assistent valmistab ette töökoha-, koosoleku-, voogedastus-, uurimis- või projektiprofiilid. Enne salvestamist kontrollige loodud samme. Seadistamine ei käivita ettevalmistatud toiminguid.

## Makro, tingimused ja salvestamine

Makrod täidavad samme järjekorras ning toetavad ootamisi, kordusi ja tühistamist. Üldine korduspiir on 100. Kasutage eelvaadet enne toiminguid, mis sisestavad teksti teise rakendusse või muudavad süsteemi olekut.

Wait for Target Window võtab protsessi, pealkirja alamstringi ja ajalõpu vahemikus 1 kuni 600000 ms. Vähemalt üks filter on nõutav. Sobiv nähtav aken peab reageerima ja muutuma esiplaaniaknaks, enne kui järgmised sammud jätkuvad. Valmis aken ei tõenda dokumendi või teenuse laadimise lõppemist.

Töövooredaktor lisab faili olemasolu, protsessi töötamise ja akna pealkirja tingimused. Väär tingimus jätab sammu vahele ja võib jätta vahele ka täiendavaid järgnevaid samme. Alternatiivse haru väljendamiseks pöörake tingimus vastupidiseks. Valige teadlikult, kas vea korral peatuda või jätkata; jätkamisel võivad hilisemate sammude eeldused olla täitmata.

Toimingusalvesti jäädvustab kiirklahvikombinatsioone ja ootamisi, mitte tavalist teksti sisestamist. See toetab muuteklahvidega kombinatsioone ja funktsiooniklahve. Escape lõpetab salvestamise. Kontrollige loendit ja võtke see kasutusele salvestamata paanimustandina, seejärel salvestage eraldi. Kasutajaliidese klõpsusalvesti on allpool kirjeldatud eraldi tööriist.

## Märksõnad, gestid ja kihid

Klaviatuurileht loetleb tuvastatud liidesed ja näidispaigutused. Üks füüsiline klaviatuur võib pakkuda mitut liidest. Valige õige keel ja geomeetria; tootenimi üksi ei määra ISO või ANSI paigutust.

Tavalised globaalsed kiirklahvid kehtivad kõigil klaviatuuridel. Seadmepõhised Raw Input seosed jälgivad kindlat seadet ja skannimiskoodi, sealhulgas toetatud funktsiooni- ja meediaklahve. **Raw Input ei tõkesta tavalist Windows sisendit. Ainult makrodeks kasutatav teine klaviatuur ei ole tootmiskasutuseks valmis.** Endiselt on vaja allkirjastatud, litsentsitud ja testitud filtridraiveri taustsüsteemi; MAKRIVO ei paigalda ühtegi sellist draiverit.

Toetatud eriklahviseosed võivad kasutada kohest vajutust, lühivajutust, allhoidmist või topeltvajutust. Allhoitavad kihiklahvid valivad ajutiselt teise vastenduse ja taastavad vabastamisel põhikihi; kihivalikuklahvid ei käivita oma makrot. Allhoidmise läved on 150 kuni 5000 ms; topeltvajutuse ajavahemikud 150 kuni 1000 ms. Need vastendused erinevad tavalistest globaalsetest kiirklahvidest.

Eriklahvide viisard tuvastab ühilduvad klahvid ilma neile määratud toiminguid jäädvustamise ajal käivitamata. Diagnostika selgitab kättesaamatuid seadmeid, peatatud seoseid, profiilide mittevastavusi ja konflikte. Ülekate näitab valitud paigutust ja seoseid; selle vaikimisi allhoitav kiirklahv on `Ctrl+Alt+F1`.

| Integratsioon | Nõuded ja piirangud |
| --- | --- |
| Corsair | Töötav iCUE, SDK-juurdepääs ja ühilduv mudel. G1–G6 ja toetatud RGB-funktsioonid; tootjapõhised Fn-, ratta-, lukustus- ja profiilijuhtelemendid ei ole üldjuhul G-key API kaudu kättesaadavad. |
| Logitech G | Algupärased kohalikud x64 G-key/LED SDK-teegid vajavad selgesõnalist heakskiitu. G-key sündmused ei erista üheselt mitut identset füüsilist klaviatuuri. |
| Razer | Chroma RGB ja standardsisend; vajaduse korral vastendage tootjapõhised juhtelemendid Synapse kaudu. |
| SteelSeries | GameSense RGB ja standardsisend; vajaduse korral vastendage tootjapõhised juhtelemendid GG kaudu. |
| Wooting | Ühilduv algupärane kohalik x64 SDK analoogväärtuste ja lävede jaoks; tavaline HID-sisend jääb aktiivseks. |
| Muud võti | Tavalised kiirklahvid või toetatud füüsiliste skannimiskoodide seosed. Fn ja tootjapõhised juhtelemendid sõltuvad mudelist ning tootja seadistusest. |

Tootjauuringud kirjeldavad dokumenteeritud võimalusi, mitte iga loetletud seadme rakendatud või testitud ühilduvust. Uued adapterid vajavad endiselt tegeliku riistvara vastuvõtuteste. Ärge laadige SDK binaarfaile alla ebausaldusväärsetest allikatest; SDK levitamisõigused on tehnilisest ühilduvusest eraldi.

## Audio, ekraanid ja OBS

Helisammud määravad helitugevuse vahemikus 0 kuni 100 protsenti ning vaigistavad/lülitavad vaigistuse välja/vahetavad vaigistuse olekut toetatud väljund- või mikrofoniseadmetel. Valige kindel lõpp-punkt või kasutage Windows vaikeseadet. Puuduv salvestatud sihtmärk põhjustab vea, mitte teise seadme vaikset juhtimist. Pärast taasühendamist kasutage Refresh Devices. Lõpp-punkti juhtimine ei tähenda sõltumatu rakendusepõhise helimikseri tuge.

Kuvatoimingud suurendavad/vähendavad heledust 10 protsendipunkti võrra või määravad väärtuse vahemikus 0 kuni 100. Siseekraanid kasutavad toetatud WMI-liideseid; välismonitorid nõuavad DDC/CI tuge ja tavaliselt selle sisselülitamist monitori menüüs. Dokid, adapterid ja kaablid võivad juhtimist takistada. Valige kõik toetatud ekraanid või üks monitor. Taasühendamine võib muuta saadaolevaid tunnuseid; kontrollige salvestatud sihtmärki. Puuduvad näidud kuvatakse kättesaamatuna, mitte mõõdetud väärtustena.

OBS vajab OBS Tools jaotises lubatud WebSocket v5 ühendust. Seadistage lõpp-punkt, testige ühendust ja laadige enne salvestamist stseenide/sisendite nimed. Toetatud on stseenivahetus, voogedastuse/salvestamise alustamine/peatamine/ümberlülitamine, salvestamise paus/jätkamine ja sisendi vaigistamine. Paroole saab hoida Windows Credential Manageris ja need jäetakse konfiguratsioonieksportidest välja. Edukas OBS käsk ei tõenda kõigi järgnevate voogedastuse mõjude toimumist.

## Tulemuste kontrollimise ja täitmise ajalugu

Lubage sobivatel sammudel Verify Result. Rakendus eristab eelkontrolli, kinnitatud mõjusid, tõrkeid, tühistamist ja mõjusid, mida ei saa kontrollida. Protsessi olemasolu ei tähenda rakenduse valmisolekut. Suhtelisi muudatusi, kiirklahve ega tekstisisestust ei saa üldiselt edukaks kuulutada. Tõrke korral kontrollige tegevuslogi ja kontrolliajalugu. Enne kogu makro uuesti käivitamist parandage konfiguratsioonivead.

## UI automatiseerimine ja klõpsamine

Lisage UI Automation samm, valige toiming ja jäädvustage kursori all olev sihtmärk pärast kuvatud viivitust. Üksnes jäädvustamine ei käivita midagi. Kontrollige selektorit ja nõudke üheselt määratud vastet. Toetatud toimingud on `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ja `WaitState`. Parooliväljad, keelatud juhtelemendid ja mitmetähenduslikud selektorid lükatakse tagasi.

Kasutajaliidese automatiseerimine töötab piiratud abiprotsessis. See sõltub sihtrakenduse Windows ligipääsetavustoest ja õigustest. Klõpsusalvesti jäädvustab toetatud toiminguid ja pause, mitte üldist tekstisisestust. Enne kasutuselevõttu kontrollige ja valideerige mustand. Välise rakenduse kasutajaliidese muutused võivad selektorid kehtetuks muuta. Protsessiisolatsioon piirab krahhide mõju ja võimaldab tühistamist, kuid ei ole turvaliiva­kast.

## Pluginid

Avage Extensions ja kontrollige paketti enne importimist. Kontrollitakse paketi struktuuri, manifesti ja räsisid. Imporditud laiendus on algselt keelatud. Lugege deklareeritud toiminguid ja usaldage enne lubamist avaldajat selgesõnaliselt. Pluginad töötavad teie kasutajaõigustega eraldi protsessis; see protsess **ei ole liivakast**. Paigaldage ainult usaldusväärseid pakette.

Haldur toetab eksportimist, keelamist, eemaldamist ning eelmise versiooni taastamist/hülgamist. Puuduvad sõltuvused takistavad käivitamist. Näidispakett pakub pöördloenduse ja kohaliku oleku toiminguid. Toetatud skeem on dokumenteeritud JSON-Schema alamhulk, mitte kõik võimalikud JSON-Schema funktsioonid. Avalikku pluginaturgu ei ole.

## Mobiilne paneel

1. Avage Mobile Panel, valige kohalik IP ja määrake selgesõnaliselt jagatavad profiilid. Vaikimisi loopback-aadress on kättesaadav ainult arvutis endas; telefoni või tahvelarvuti jaoks valige sobiv LAN-aadress.
2. Käivitage paneel ja lugege selle HTTPS-aadressi, sertifikaadi sõrmejälge ning ühekordset kuuekohalist sidumiskoodi.
3. Avage aadress seadmes, mis on samas usaldusväärses võrgus. Enne kohaliku sertifikaadi usaldamist võrrelge SHA-256 sõrmejälge usaldusväärse kanali kaudu. Rakendus ei paigalda usaldatud sertifikaati automaatselt.
4. Siduge seade kuvatud koodi abil ja testige üht ohutut paani. LAN-juurdepääs võib nõuda Windows Firewalli käsitsi antud luba. Ärge tehke porti internetist kättesaadavaks.
5. Lõpetamisel peatage paneel või tühistage seotud klientide luba rakenduses.

Serveri privaatvõtmeid kaitseb Windows DPAPI; klientide autentimisandmed salvestatakse räsidena. IP muutmine või sertifikaadi uuendamine võib nõuda uut usaldamist. Paneel näitab toimingute ja kontrolli olekuid, mitte kõiki võimalikke reaalajaandureid. Brauseripaigutuse ja kohaliku HTTPS-i kontrollid ei asenda tegelike telefonide/tahvelarvutite LAN-vastuvõtuteste.

## Kohalik assistent

See valikuline eksperimentaalne assistent on vaikimisi keelatud. Avage see Tools menüüst, lubage see selgesõnaliselt, valige käituskeskkond/mudel ja lisage ainult ressursid, mida soovite arvesse võtta. Sõnastage eesmärk, looge mustand, kontrollige lahendamata taotlusi ja viige tulemus tavalisse redaktorisse. Midagi ei käivitata automaatselt; salvestamine ja käivitamine jäävad selgesõnalisteks toiminguteks. Automaatset üleminekut teisele käituskeskkonnale ei ole.

| jooksmine | Sissejuhatus |
| --- | --- |
| Ollama | Eraldi kohalik teenus ja kohalikult paigaldatud mudel `127.0.0.1:11434`. Deklaratsioonitud kaug- ja pilvimudelid on keeldutud. |
| LM Studio | Kohalik server, millel on kohalik laaditud mudel ja JSON-kava toetus `127.0.0.1:1234`. Ärge lubage kaugroutinguid kohaliku töötlemise jaoks. |
| llama.cpp | Kohalik `llama-server` ja ühilduv mudel JSON-kava toetusega `127.0.0.1:8080`. |
| ONNX käivitusaeg GenAI CPU | Usaldusväärne kohalik GenAI teksti mudeli faili; valige `genai_config.json`. Inference kasutab kombineeritud kaaslase protsessi ilma HTTP-i. |

HTTP-kliendid kasutavad ainult loopback-ühendusi, ei kasuta puhverservereid ega järgi ümbersuunamisi. MAKRIVO ei saa usaldusväärselt tuvastada, kas ühilduv kohalik server pöördub sisemiselt kaugteenuse poole. Autentimisega kohalikke servereid ei saa selles versioonis seadistada. Mudelikaale, Pythonit ega kohalikke inferentsiservereid ei laadita alla ega paigaldata automaatselt.

ONNX paketid vajavad `tokenizer.json` faili ja kehtivat vestlusmalli. Failis `tokenizer_config.json` toetatakse stringi `chat_template`, stringiga `default` sõnastikku või üheselt määratud nimega/vaikemallide loendit. Kui mall puudub või on null, kasutatakse varulahendusena `chat_template.jinja`. Tühjad, mitmetähenduslikud, liiga suured või vale tüübiga mallid lükatakse tagasi. Suvalised ONNX-failid, vektor-/nägemismudelid ja ainult GPU-le mõeldud paketid ei asenda ühilduvaid GenAI tekstimudeleid. Kaasasoleva GenAI käituskeskkonna versioon on 0.17.1, CPU. Kasutage ainult usaldusväärseid mudeleid; protsesside eraldamine ei ole liivakast.

Assistent toetab valitud programme ilma argumentideta, valitud dokumente ja veebisaite ning mikrofoni vaigistamist selgesõnalise taotluse korral. See ei loo sõnumeid, kohtumisi, suvalisi skripte, vabu klõpse, allalaadimisi ega peakomplekti vahetust. Eesmärk ja ressursinimed saadetakse valitud kohalikule käituskeskkonnale; ressursiteid ja failisisu eraldi ei edastata. Kasutaja sisestatud eesmärk võib ise sisaldada isikuandmeid. Kohaliku käituskeskkonna võrgukäitumine jääb kasutaja vastutuseks.

ONNX päringu ajapiirang on 90 sekundit koos mudeli laadimisega; lubatud on kuni 3072 sisendtokenit ja 1024 genereeritud tokenit, lisaks kehtib mudeli kontekstipiirang. Vigane/kärbitud JSON ja tundmatud ressursi-ID-d lükatakse tagasi. Päringu lõpetamiseks tühistage see või sulgege aken. Juhumudeliga käituskeskkonna test tõendab käivitumist, mitte kasulikku planeerimiskvaliteeti ega ajasäästu. Kohalikud katselogid sisaldavad sündmuste tunnuseid ja ajamõõtmisi, mitte eesmärgi teksti ega dokumentide sisu; neid ei laadita automaatselt üles.

Hilisem väikese Instruct-mudeli hindamine andis neljas saksa/inglise katses null kehtivat plaani. Natiivne inferents töötas, kuid mudel ei täitnud ranget planeerimislepingut. See on negatiivne kvaliteeditulemus, mitte mudelisoovitus ega tõend kasulikust assistendist. Täpse mudelirevisjoni ja piirangute kohta vaadake kuupäevastatud kohaliku käituskeskkonna testiaruannet.

## Impordi, ekspordi, sünkroniseerimise ja backup

Enne konfiguratsiooni asendamist kontrollige impordi eelvaadet. Imporditud toimingud on algselt keelatud ning teisaldatav sünkroonimine eemaldab riistvarapõhised seosed. Pärast kontrollimist määrake seadmed uuesti ja lubage toimingud teadlikult. Enne ekspordi jagamist kontrollige teksti/toimingute sisu; paroolide väljajätmine ei muuda kogu kasutaja loodud toimingute sisu mittetundlikuks.

Profiilide sünkroonimine vahetab faile valitud kaustas, mida võib hallata teie olemasolev pilveklient. MAKRIVO ei paku pilvekontot ega salvesta pilve autentimisandmeid. Revisjonid takistavad vaikseid ülekirjutusi. Toimingusisu kaasamine on selgesõnaline valik. Enne asendavat importi varundage konfiguratsioon. Vaikimisi konfiguratsioon on `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; ajalooline kaustanimi on säilitatud ühilduvuseks. Enne käsitsi kopeerimist/taastamist sulgege rakendus. Salvestatud SDK-heakskiidud ja operatsioonisüsteemi kaitstud autentimisandmed võib teises arvutis vaja olla uuesti luua.

## Probleemide lahendamine

| Sümptomid | Mida kontrollida |
| --- | --- |
| Lühidalt ei tee | Rakenduse käivitamine, sidemed ei pausi, korrektsed aktiivsed profiilid, salvestatud/võimendatud tile, lühikeste konfliktide ja kättesaadavad sihtmärgid. |
| Eesmärk akna ei saa aktiveerida | Manuaalselt aktiveerige seda. Windows eespool olevad piirangud ja privileegide erinevused võivad blokeerida automatiseerimist. |
| Erilise võti ei tuvastata | Pakkuja teenus / SDK / mudeli tugi; konfigureerige omandiõigused, et võimalikult hästi toetatud standardse võtme väljaandmiseks. |
| Teine klaviatuur on ka tavaline | Oodatud on Raw Input Tootmises ei rakendata ainulaadset supressiooni. |
| Audio või heledus ei ole saadaval | Värskendavad seadmed; kontrollivad valitud identiteeti, lõpppunkti istungid, WMI/DDC/CI tugi ja dokk/kaabli piirangud. |
| OBS offline | WebSocket v5 aktiveeritud, õige lõpppunkt / parool, ligipääsetav teenus ja kehtiv koht / sissetuleku nimed. |
| Automaatika ebaõnnestub | Koosoleku host, ainulaadne kehtiv valik, ligipääsetav lubatud sihtmärk, toetatud kontroll ja loa. Taastage üleminekuaeg alles pärast eesmärgi läbivaatamist. |
| Mobiilne paneel | LAN asemel loopback-aadress, sama võrk, serveri käivitamine, kinnitatud profiilid, sertifitseeritud sõrmejälg / usaldus ja manuaalne tuletõrje lubadused. |
| Plugin ebaõnnestub | Pakendi terviklikkus, selge usaldus / lubatud seisund, sõltuvused, protokollide ühilduvus ja rakenduse log. |
| Taotlused ebaõnnestuvad | Õige valitud kohalik käitumisajaga, teenus / mudel valmis, toetatud JSON skeem, usaldusväärne vastav mudeli faili ja nõudmise piirid. |
| Layout või tekst tundub valesti | Eraldage kasutajaliides keelt klaviatuuri geomeetrist; vaadake akna, vaadake ekraani suurust ja teatage täpsest lehtest / keelt / ehitusest. |

Kasulik veaaruanne sisaldab Windows versiooni, rakenduse järku/kommitit, keelt, kuva skaleerimist, riistvaramudelit, minimaalset sammujada ja asjakohast puhastatud logi. Enne avaldamist eemaldage isiklikud failiteed, toimingusisu ja autentimisandmed.

## Ehitus, testimine ja pakendamine

Kasutage SDK, mis on kinnitatud `global.json`. Räägime repositsiooni juurest:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integratsioonitestid võivad avada isoleeritud aknaid ning ajutiselt seadistada/vabastada toetatud tootjate klahviseoseid. Käivitage need ilma teise neid seoseid haldava kontrollerita. Vaikimisi heleduse/heli proovikontrollid on ainult lugemiseks; selgesõnalised riistvara edasi-tagasi testid on eraldi. Imiteeritud protokolle, renderdatud kuvatõmmiseid ja juhuslikke inferentsigraafe ei tohi esitada tegeliku riistvara või mudelikvaliteedi vastuvõtutestidena.

Täielik avaldamisskript koondab abiprotsesside käivitusprogrammid, mobiilseadmefailid, näidisplugina, ONNX-käituskeskkonna ja saadaolevad sõltuvuste teated. Säilitage alamkataloogid. Edukas ehitus ei ole allkirjastatud paigaldusprogramm ega õiguslik väljalaske heakskiit. Lähtekood ja järgud võivad areneda eraldi; võrrelge dokumentatsiooni seisu ja kommitit paigaldatud versiooniga.

## Litsentsimine, privaatsus ja vabastamise seisund

Kavandatud ärimudel on ühekordne ost koos ostetud põhiversiooni tähtajatu kasutamisega; lõpphind, lubatud seadmete arv ja uuendustingimused tuleb määrata tegelikes müügitingimustes. See käsiraamat ei tõenda toimiva ostu-/aktiveerimissüsteemi olemasolu. Igapäevaseks juhtimiseks pole pilvekonto nõutav.

Ärge levitage tootja omandiõigusega SDK binaarfaile ilma tõendatud loata. Säilitage kõigi tarnitavate sõltuvuste litsentsid ja kolmandate osapoolte teated, sealhulgas .NET ja ONNX pakettide omad. Mudelikaaludel on oma tingimused. Kuupäevastatud litsentsiaudit tehti enne mõne uue sõltuvuse lisamist ega anna nende lisanduste levitamiseks heakskiitu. Avalik repositoorium ei anna iseenesest üldist avatud lähtekoodi litsentsi.

Tegelik tootjate riistvara, telefonide/tahvelarvutite LAN-töö, eri monitoride/DPI-de vahelised üleminekud, pikaajaline kasutamine, täielik keelekontroll, eksklusiivse draiveri tugi ja kasulik AI planeerimiskvaliteet vajavad endiselt eraldi vastuvõtuteste. Ärge järeldage heakskiitu üksnes funktsiooni olemasolust.

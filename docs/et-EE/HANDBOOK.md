<!-- generated-doc locale=et-EE source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../HANDBOOK.md)

# MAKRIVO — Kasutusjuhend

Tootja: **Aivana**. Dokumentatsioon snapshot: **2026-10-12**. See käsiraamat kirjeldab praegust tööpuu rakendamist. eksperimentaalsed integratsioonid ja hardware-sõltuvad funktsioonid on selgelt kindlaks määratud. ajaloolised aruanded kirjeldavad nende kuupäeva, mitte uue ehituse tagatist.

## Dokumentatsioon ja keeled

Kasutage dokumendi keele valiku, et avada täielik käsiraamat, funktsioonide loend, probleemide lahendamine, arendusteave, teadusuuringud ja ajaloolised testide aruanded mis tahes 27 rakenduskeeltes. Loodud tõlked on masinabi ja neil on struktuurikontrolli; nad ei ole kõik saanud emakeele ülevaateid. Tehnilised tunnused ja rakendatavad näited jäävad muutumatuks. Originaalne allikas on võrreldav. Tõlgitud litsentsianalüüs on informatiivne ja ei asenda esialgseid kolmandate osapoolte õiguslikke tingimusi ega lõplikku EULA-d.

## Installatsioon ja esimene käivitamine

Algne eesmärk on Windows 11 x64. hoida kõik failid ja alldirektiivid koos. `MAKRIVO.exe`. Allikate kontrollimine ei ole installatsioon. isekontoleeritud pakendid hõlmavad oma .NET-i käivitusaega; raamistiku sõltuvad pakendid vajavad sobivat .NET-d 10 Desktop ja ASP.NET käivitamine. ONNX ja kaaslased peavad olema oma vastavate funktsioonide jaoks kaasatud.

1. Alustage MAKRIVO ja valige Settings'is vahe keele. Automaatne järgimine Windows Keyboard layout on eraldi seade.
2. Valige profiil ja lisage seejärel otsitav tegevuskataloog. Andke talle tunnustatud nimi ja valige oma tegevused.
3. Vaadake sammud ja vajadusel lühidalt. Vältige kombinatsioone, mida on juba kasutatud Windows või teie sihipärane rakendus.
4. Päästa plaani. Salvestamata muudatused ei ole salvestatud konfiguratsioon.
5. Proovige kahjutu tegevus, näiteks dokumendi avamine ja täitmise tagasiside kontrollimine.

Peamine akna sulgemine jätab tavaliselt taotluse teavitamispiirkonnas. Kaksklõpsake oma tray ikoon, et seda uuesti avada või kasutage trey menüü välja. Alustamine Windows on valikuline ja algselt puudulik. Pause kontroll peatab sidemeid; ikoon näitab pausi. Tühistada käivitusmakrood läbi UI, tray või konfigureeritud tühistamise lühend; nõutav on `Ctrl+Alt+Escape`.

## Tüübid, tegevused ja komanda palett

Tilid kuuluvad profiilidele ja võivad sisaldada mitu tellitud sammu. Redaktsioon toetab salvestamist/välja, eelvaadet, undo/redo ja sammu ümberkorraldamist. Kataloog pakub programmide, dokumentide, URL-ide, teksti, klippekäigu, meedia, audio, akna, süsteemi tegevuse ja toetatud integreerimise eelkohad. Parameetrite nõudmisega eelnõu avatakse.

Avage palett `Ctrl+K` ja MAKRIVO on keskendunud. Otsingud salvestatud tegevused, eelnevalt, profiili vahetamine ja tööriistad. Valige löögi võtmed; Sisestage aktiveerib; `Ctrl+Enter` redigeerib toetatut elementi; Väljumine sulgeb. Mõned valmis käskud täidetakse kohe, samal ajal kui konfigureeritud toimingud nõuavad läbivaatamist. Eraldi globaalse palli lühend saab konfigureerida; uued konfiguratsioonid kasutavad `Ctrl+Shift+Space`.

Teksti ja clipboard meetodid `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ja `{{user}}`. Kuupäeva vormistamine on `yyyy-MM-dd`; Aegade vormistamine on `HH:mm`. Tuntud muutused jäävad muutumatuks. klippide lugemine toimub ainult siis, kui seda taotletakse. klippid kirjeldavad teksti ilma automaatselt kohandamata.

UI poolt käivitatud tegevused võivad suunata viimast välist rakendust. Kui sihtmärk kaob või Windows blokeerib esialgset aktiveerimist, aktiveerige manuaalselt sihtmärg ja taganeme. Edukas võtme tarnimine ei tõenda, et eesmärk on oma ülesande täitnud.

## Profiilid ja töökohtade seadistamine

Profiilid võivad vastata protsessi nimele ja akna pealkirja osale. Kui mõlemad on kokku pandud, peavad mõlemad olema kooskõlas. Profiili korraldus lahendab võistlevaid mänge; default profiil on langemine. Võimalik automaatne valik. Lock Profile takistab praeguse istungi automaatseid muudatusi.

Avatud töökoha seadistamine tööriistadest või palettist. Valige programmid, dokumendid, veebisaidid, audio/display eesmärk või OBS stseen. Assistent valmistab ette töökoha, kohtumiste, voogude, teadusuuringute või projektiprofiilide. Kontrollige tekkinud sammud enne säästmist. Seade ei täida ettevalmistatud tegevusi.

## Makro, tingimused ja salvestamine

Macros täidab oma sammud järjekorras ja toetab ootusi, korduvusi ja tühistamist. Üldine korduvus on 100. Kasutage eelvaadet, enne kui teete teisi rakendusi või süsteemi seisundit.

Oodake sihtpinda aktsepteerib protsessi, pealkiri ja aja väljalülitamist 1 kuni 600,000 ms. Vajalik on vähemalt üks filtr. Kooskõlas nähtav aken peab reageerima ja muutuma esiküljel, enne kui järgmised sammud jätkuvad. Valmis aken ei tõenda, et dokument või teenus on laaditud.

Töövoo redigeerija lisab failid, protsessi käivitamine ja akna pealkirja tingimused. Vale seisund viib sammu ja võib edasi lükata täiendavaid samme. Keelduda tingimusest väljendada alternatiivset rühma. Valige tahtlikult viga või jätkuv viga; jätkamine võib jätta edasisi samme ilma nende eeldatavate tingimustega.

Tegevusrekord võtab lühikesed koorid ja ootavad, mitte tavalised trükkimised. See toetab modifitseerivaid lühendusi ja funktsiooni võtmeid. Põgenemine lõpetab salvestamise. Vaadake nimekirja ja võtke selle kui salvestamata tile eelnõu, seejärel salvestage selgelt. UI klõpsamine on allpool kirjeldatud eraldi tööriist.

## Märksõnad, gestid ja kihid

Keyboard leht loetleb tuvastatud liidesid ja viiteartikleid. Üks füüsiline klaviatuur võib esitada mitmeid vahendeid. Valige õige keel ja geomeetria; ainult toote nimi ei tuvasta ISO või ANSI paigutust.

Standard globaalsed hotkeys kehtivad klaviatuuride vahel. seadme-spetsiifiline Raw Input sidemed jälgivad konkreetset seadet ja skaneerimiskodi, sealhulgas toetatud funktsiooni ja meedia võtmeid. **Raw Input ei takista tavapärast Windows Exkluentne teine klaviatuur ei ole tootmiseks valmis.** Allkirjastatud, litsentseeritud ja katsetatud filtreerimisseadme tagasiside on veel vajalik; ükski ei ole installitud MAKRIVO.

Toetatavad spetsiaalsed võtmed võivad kasutada kohe trükkimist, trükkimise, hooldamise või topelttrükkimist. Held-layer võtmed valivad ajutiselt teise kaardistamise ja taastavad vabastatud põhikihti; Layer-selector võtmed ei tööta oma makro. 150 Teda 5,000 MS; kahekordne aken 150 Teda 1,000 MS. Need kaardid eristavad tavalistest globaalsetest hotkeys.

Spetsiaalne võti tuvastab ühilduvad võtted ilma nende määratud tegevuste täitmata. Diagnoosid selgitavad kättesaamatuid seadmeid, katkestatud sidemeid, profiili ebaõnnestumisi ja konflikte. Ülekoormus näitab valitud paigutust ja sidemeid; selle eelnevalt lühendatud on `Ctrl+Alt+F1`.

| Integratsioon | Nõuded ja piirangud |
| --- | --- |
| Corsair | Running iCUE, SDK juurdepääs ja ühilduv mudel. G1–G6 ja toetatavad RGB funktsioonid; omandatud Fn, rattad, lukud ja profiili kontrollid ei ole üldiselt G-lõuna API. |
| Logitech G | Originaalne x64 G-key/LED SDK raamatukogud vajavad selgelt heakskiitu. G-tähelehted ei tuvasta ainulaadselt mitmeid identseid füüsilisi võti. |
| Razer | Chroma RGB ja standardse sisendiga; kaardi omandatud kontrollid Synapse vajaduse korral. |
| SteelSeries | GameSense RGB ja standardse sisendiga; kaardi omandi kontrollid GG vajaduse korral. |
| Wooting | Ühilduv originaal x64 SDK analoogväärtuste ja piirmäärade jaoks; normaalne HID sisend jääb aktiivseks. |
| Muud võti | Standardne hotkeys või toetatakse füüsilise skaneerimise koodi sidemed. Fn ja omandirežiimid sõltuvad mudeli ja müüja konfiguratsioonist. |

Tootja uuring kirjeldab dokumenteeritud võimeid, mis ei ole rakendatud või testitud ühtekuuluvust iga loetletud seade. Uued adapterid vajavad endiselt tegelikku hardware aktsepteerimist. Ärge allalaadige SDK-d ebakindlatest allikatest; SDK-i jaotusõigused on eraldatud tehnilise ühilduvusest.

## Audio, ekraanid ja OBS

Audio sammud määrata maht 0 kuni 100 protsenti ja mute/mute/toggle toetatakse väljund või mikrofoni lõpppunktid. Valige konkreetne lõpppunkt või kasutage Windows nõuet. Puudunud salvestatud eesmärk tekitab vea, mitte vaikselt teise seadme kontrollimist. Värskendavad seadmed pärast taasühendamist. Endpoint Control ei ole sõltumatu rakendusvahendite toetuse nõue.

Näita toimingud suurendavad/vähendavad heledust 10 protsendipunktidega või määrata väärtus 0 kuni 100. Sisemised ekraanid kasutavad toetatavaid WMI-vahendeid; välised monitorid vajavad toetust DDC/CI ja tavaliselt oma monitor-menüü seade. Dokid, adapterid ja kaablid võivad blokeerida. Valige kõik toetatud ekraanid või üks monitor. Taastamine võib muuta olemasolevaid identiteete; vaadata päästetud eesmärk. Puuduvad lugemised on näidatud puudulikuks, mitte mõõdetud väärtusteks.

OBS nõuab WebSocket v5 kättesaadavaks OBS tööriistad. Määrake lõpppunkti, proovige ühendust ja laadige sissetuleku nimed enne salvestamist. Toetatavad meetmed hõlmavad stseenide vahetamist, voolu/rekordit algust/jäätmist/tagustamist, pausi/resume ja sissetuleku muti. Paroolid saab salvestada Windows Credential Manageris ja need on konfigureerimiseksportist välja jäetud. Edukad OBS juhised ei kehtesta iga allveelaevade efekti.

## Tulemuste kontrollimise ja täitmise ajalugu

Võimalik kontrollida tulemusi sobivate sammudega. Rakendus eristab eelvaateid, kinnitatud efekte, ebaõnnestumisi, tühistamist ja efekte, mida ei saa kontrollida. Protsessi olemasolu ei tähenda rakendamise valmisolekut. Suhtelised muudatused, lühidised ja teksti sissepääs ei saa edu avaldust. Kontrollige tegevuse ajakirja ja kontrollimise ajalugu, kui tegevus ebaõnnestub. Konfigureerimise vigu tuleks parandada enne täieliku makro ümberlülitamist.

## UI automatiseerimine ja klõpsamine

Lisage kasutajaliides automaatse samm, valige operatsioon ja võta eesmärk näitajas pärast näidatud viivitust. Üks pildistamine ei toimu midagi. Kontrollige valiku ja vajate ainulaadset kokkulepet. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ja `WaitState`. Paroolid, keelatud kontrollid ja ebamäärased valijad on keeldutud.

UI automatiseerimine toimub piiratud kaasajaga. See sõltub sihtotstarbelise rakenduse Windows kättesaadavuse toetamisest ja loadest. Klõpsur salvestab toetatud tegevusi ja pausi; see ei salvestata üldist teksti sissetulekut. Vaadake ja kinnitage oma eelnõu enne selle vastuvõtmist. Välise rakenduse UI muudatused võivad valikuid tühistada. Protsessi isolatsioon piirab õnnetusi ja tühistamist, kuid see ei ole turvakoti.

## Pluginid

Avage laiendused pakendi kontrollimiseks enne importi. Pakendi struktuur, manifest ja hashid on kontrollitud. Impordi algab puuetega. Loe deklareeritud tegevusi ja usaldage selgelt väljaandjat enne lubamist. Plugins töötavad teie kasutaja loa eraldi protsessis; see protsess on ** mitte liivakasti**. Kasutage ainult pakette, millele usaldate.

Juhendaja toetab varasemate versioonide eksporti, deaktiivsust, eemaldamist ja taastamist. Puuduvad sõltuvused blokeerivad. Näiteks pakett annab arvutamise ja kohaliku riigi tegevuse. Toetatav skeem on dokumenteeritud JSON-Schema allüksus, mitte kõik võimalikud JSON-Skeemi funktsioonid. Plugin turul ei ole.

## Mobiilne paneel

1. Avatud mobiilseadme, valige kohalik IP ja selgesõnaliselt välja profiilid. Eeldatava loopback-aadress on saadaval ainult arvutis; valige sobiv LAN-aadress telefoni või tahvelarvuti jaoks.
2. Alusta paneeli ja loe selle HTTPS-aadress, sertifitseerige sõrmejälgi ja ühe kasutamise kuue numbri paari koodi.
3. Ühes võrgus olevas seadmes avage aadress. Võrdle SHA-256 sõrmejälgi usaldusväärse kanali kaudu enne kohaliku sertifikaadi usaldamist. Rakendus ei installita usaldusväärset sertifikaati automaatselt.
4. Paari, kasutades näidatud koodi ja testida ühe kahjutu tile. Manuaalne Windows tuletõrje luba võib olla vajalik LAN-i juurdepääsuks. Ära pane sadamat internetile.
5. Lõpetage paneel, kui see on lõpetatud, või tühistage kliendid rakenduse kaudu.

Privaatsed serveri võtmed on kaitstud Windows DPAPI; kliendi tunnused salvestatakse hashtidena. IP muutmine või sertifikaadi uuendamine võib nõuda usalduse uuendamist. Paneel näitab tegevuse ja kontrollimise seisundit, mitte kõiki võimalikke elusandreid. Kaubamärk ja kohalikud HTTPS-kontrolli ei asenda reaalset telefoni/tabletti LAN-i aktsepteerimist.

## Kohalik assistent

See vabatahtlik eksperimentaalne assistent on default. Avage see tööriistadest, aktiveerige seda selgelt, valige tööaeg / mudel ja lisage ainult ressursid, mida soovite kaaluda. Pane eesmärk, luua eelnõu, kontrollida lahendamata taotlusi ja edastada tulemus tavalisele redigeerijale. Miski ei toimu automaatselt; säästmine ja käivitamine jäävad selgeks. Automaatne rünnak ei ole olemas.

| jooksmine | Sissejuhatus |
| --- | --- |
| Ollama | Eraldi kohalik teenus ja kohalikult paigaldatud mudel `127.0.0.1:11434`. Deklaratsioonitud kaug- ja pilvimudelid on keeldutud. |
| LM Studio | Kohalik server, millel on kohalik laaditud mudel ja JSON-kava toetus `127.0.0.1:1234`. Ärge lubage kaugroutinguid kohaliku töötlemise jaoks. |
| llama.cpp | Kohalik `llama-server` ja ühilduv mudel JSON-kava toetusega `127.0.0.1:8080`. |
| ONNX käivitusaeg GenAI CPU | Usaldusväärne kohalik GenAI teksti mudeli faili; valige `genai_config.json`. Inference kasutab kombineeritud kaaslase protsessi ilma HTTP-i. |

HTTP kliendid kasutavad ainult loopback, ei kasuta proxy ja ei jälgita ümber suuniseid. MAKRIVO ei saa usaldusväärselt kindlaks teha, kas sobiv kohalik server siseselt helistab kaugteenust. Autentsed kohalikud serverid ei ole selles versioonis konfigureeritavad. Mudeli kaalud, Python ja kohalikud inferents serverid ei ole automaatselt alla laaditud või installitud.

ONNX Pakendid on vajalikud `tokenizer.json` ja kehtiv vestlusvorm. toetatud templite vormid `tokenizer_config.json` See on string `chat_template`, Sõnaraamat koos stringiga `default`, või kahtlemata nimetatud/default templite nimekiri. kui templit puudub või null, `chat_template.jinja` Kasutatakse tagasilöökuna. tühjad, ebamäärased, ülekaalulised või valesti kirjutatud templid on keeldutud. ONNX failid, sisseehitatud/nägemismudelid ja GPU-i pakendid ei ole ühilduvad GenAI Kirjeldus: Ühendatud GenAI Runtime versioon on 0.17.1, Kasutage ainult usaldusväärseid mudeleid; protsessi eraldamine ei ole liivakasti.

Assistent toetab valitud programme ilma argumentideta, valitud dokumente ja veebisaite ning mikrofon mute kui see on sõnaselgelt nõutav. See ei loe sõnumeid, kuupäevi, tahtlikke skripte, tasuta klõpsu, allalaadimised või headseti vahetamine. Eesmärk ja ressursside nimed saadetakse valitud kohalikule tööajale; ressursitöö ja faili sisu ei esine eraldi. Kasutaja poolt määratud eesmärk võib sisaldada isikuandmeid. Kohaliku käitumisajaga võrgustiku käitumine jääb kasutaja vastutuseks.

ONNX nõudmise eelarve on 90 sekundi, sealhulgas mudeli laadimine, kuni 3,072 kiireloomulised tokenid ja 1,024 genereeritud tokenid, samuti piiratud mudeli konteksti. Tühjendatud/pikkunud JSON ja tundmatu ressursi IDid on tagasi lükatud. Tühistada või lõpetada taotlus. Juhuslik mudel tööaeg test määrab täitmise, mitte kasulikku planeerimise kvaliteeti või aja säästmist. Kohalikud eksperimendi logid sisaldavad sündmuste identifitseerimist ja ajakava, mitte sihtteksti või dokumendi sisu; neid ei laadita automaatselt.

Hiljem toimunud väikese Instrukt-mudeli hindamine toodi neljas saksa/inglise uuringus null-valid plaanid. Native inference töötas, kuid mudel ebaõnnestus ranget planeerimiskokkulepet. See on negatiivne kvaliteet, mitte soovitatav mudel või tõestatud kasulik assistent. Vaata kuupäeva kohaliku käitumisajaga testide aruannet täpseks mudeli läbivaatamiseks ja piiranguteks.

## Impordi, ekspordi, sünkroniseerimise ja backup

Vaadake impordi prognoose enne konfiguratsiooni asendamist. Impordi toimingud hakkavad välja lülitama ja kaasaskantav sünkroonimine eemaldab hardware-spetsiifilised sidemed. Taastage seadmed ja võimaldage meetmeid tahtlikult pärast kontrollimist. Vaadake teksti/tegevuse sisu enne ekspordi jagamist; parooli välistamine ei tee kõik kasutaja loodud tegevuse sisu ebameeldivaks.

Profiili sünkroniseerimine vahetab failid valitud failis, valikuliselt teie olemasoleva pilvkliendi poolt. MAKRIVO ei anna pilvkonto ja ei salvestata pilvlood. Revisioonid takistavad vaikseid ülekirjutusi. Tegevuse sisu sisaldamine on selge valik. Tagasi oma konfigureerimist enne asendamise importi. Eeldatav konfigureerimine on `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; ühilduvuseks säilitatakse ajalooline direktori nimi. Sulge rakendus enne selle manuaalset kopeerimist / taastamist. Salvestatud SDK-i kinnitused ja OS-kaitse all olevad loendid võivad olla vajalikud uuesti uuendamiseks mõnes teises arvutis.

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

Kasuliku viga aruande jaoks on Windows versioon, rakenduse ülesehitus/kommitsioon, keel, ekraani skaalimine, tarkvara mudel, minimaalne järjekord ja asjakohane redigeeritud log. Eemaldage isiklikud teed, tegevuse sisu ja loendid enne avaldamist.

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

Windows integreerimise testid võivad avada isoleeritud aknad ja ajutiselt konfigureerida / vabastada toetatud tarnija sidemed. Juurdepääs neile ilma teist kontrollija, kellel on need sidemed. Eeldatavad heledus- ja heliproovid on loetavad ainult; selged hardware ringtrip testid on eraldatud. Mock protokollid, renderitud ekraani ja juhuslikud viitekirjad ei tohi esindada reaalset hardware / mudeli kvaliteedi aktsepteerimist.

Kogu väljaandja koondab kaaslased, mobiilsed varad, näidisplugin, ONNX käivitusaeg ja kättesaadavad sõltuvuse teated. Hoidke oma subdirektorid. Edukas ehitus ei ole allkirjastatud paigaldaja või seadusliku vabastamise heakskiit. Allikas ja ehitused võivad areneda eraldi; võrdle dokumendi pilti ja pühenduge installitud versioonile.

## Litsentsimine, privaatsus ja vabastamise seisund

Ettevalmistatud kaubandussüsteem on üheaegne ostmine, mille ostetud suurversioon on pidevalt kasutusel; lõpphind, seadme maksumus ja uuendamise tingimused tuleb sätestada tegelikes müügitingimustes. Tootmise ostu / aktiveerimise süsteem ei ole käesolevas käsiraamatus kehtestatud. Igapäevaseks kontrollimiseks ei ole vaja pilvakontot.

Ärge levitage omandatud SDK-d ilma tõestatud loata. Säilitage litsents ja kolmandate osapoolte teated iga saadetud sõltuvuse, sealhulgas .NET ja ONNX pakendid. Mudelil on oma kehtivus. Kuupäevane litsentside auditi käivitab mõned uued sõltuvused ja ei ole nende lisade vabastamine. Avalik salvestus ei anna ise üldise avatud lähtekoodiga litsentsi.

Tõeline tarnija hardware, telefon / tahvelarvuti LAN operatsioon, segatud monitor / DPI üleminekud, pikaajaline kasutamine, täielik keeleline läbivaatamine, eksklusiivne juhtimise tugi ja kasulik AI planeerimise kvaliteet jäävad eraldi aktsepteerimise töö. Ma ei anna kunagi heakskiitu üksinda.

<!-- generated-doc locale=et-EE source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../README.md)

# MAKRIVO

[Täisdokumentatsioon kõigis 27 keeltes](README.md) · [Kasutaja käsiraamat](HANDBOOK.md)

**Üheskoos Aivana.** [Praegused omadused ja vabastamise piirangud](FEATURES.md) · [Laiendatud testid](testing/2026-10-12-six-extensions.md) · [Kohalik AI assistent prototüüp](testing/2026-10-12-local-meeting-assistant.md)

** inglise keel (USA) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Kohalik Windows hotkey kontroller Stream Deck stiilis plaatide, profiilide, makro ja klaviatuuri tuvastamisega. Sinu vahend toetab 27 keeli. Ükski konto või pilvühendus ei ole vajalik igapäevaseks kasutamiseks.

[Dokumenteerimine](README.md) · [Kontrollimine](VERIFICATION.md) · [Feature testaruanne](testing/2026-10-09-usp-release.md)

## Alustamine

Pärast avaldamist on taotlus `artifacts/win-x64/MAKRIVO.exe`. Hoidke kogu väljundit koos; saate selle kusagil mujal kopeerida. Windows 11 x64 on algne eesmärk. Ehitatud artefaktid on Gitist välistatud; allikate kontroll ei sisalda eelnevalt ehitatavat.

Installeerige .NET 10 SDK, mis on kirjeldatud `global.json`, et luua ja käivitada allikast:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Avastage enesearvu ehitus, ilma et oleks vaja eraldi installitud .NET-i käivitust:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Põhiline töövool

1. Valige profiil ja looge tegevus.
2. Määrake selle nimi, ikooni, värvi ja klaviatuuri lühend. Ctrl/Alt/Shift kombinatsioonid vähendavad juhuslikku aktiveerimist trükkimise ajal.
3. Lisage üks või mitu sammu: rakendused, failid, veebisaidid, tekst, lühendid, meedia, maht, profiili vahetamine või viivitused.
4. Säästke siis Muudatused jäävad eelnõuks, kuni need on salvestatud; diskard taastab salvestatud versiooni. Tile Play nuppu ja selle hotkey kasutavad sama rakenduse teed.
5. Reorder pilved ja makro sammud drag-and-drop või pliiatsid. Seadmed lihtsustavad seadistamist. Makro võib korrata kuni 100 korda. Tühistada vahenduse, süsteemi tray menüü või Ctrl+Alt+Escape eelnevalt.

Rakenduse spetsiifilised profiilid kasutavad protsessi nime ilma selle täieliku teedeta. Profiili tellimus katkestab sidemeid; default profiil on langemine. Automaatne profiili valik peab olema lubatud Settings'is.

Peamine akna sulgemine hoiab rakenduse süsteemis varakult. Kaksklõpsake oma tray ikoon, et seda uuesti avada; kasutage väljavoolu menüü. Startup Windows on valikuline ja default keelatud. Hotkeys saab keskselt pausi teha.

## Toimingud ja juhtimispalett

Add Action avab otsitav kataloog, millel on 90 eelnimed kaheksa kategoorias. Igaühel on kirjeldus ja ettevaatus. Makroeditor kasutab samat kataloogi sammude lisamiseks või asendamiseks. Selle redigeeri töövoo eelneva seadistuse valimine loob projekti ilma rakendamata.

Komanda palett avab pealkiri või Ctrl+K abil rakenduse sees. See otsib salvestatud tegevusi profiilide, kataloogi eelnevalt, profiili vahetamine, navigatsioon, pausi/resumeerimine, makro tühistamine ja import/eksport. Filtreerige salvestatud tegevuste, kataloogi funktsioonide või rakenduse juhiste abil. Valige trükk, aktiveerib Enter, Ctrl+Enter redigeerib redigeeritavat sissetulekut ja väljumine sulgeb. Esimesed lemmikud ilmuvad. Kasutamiseks valmis kataloogi käskud võivad kohe käivituda; eelnevad seaded, mis nõuavad disainiga. Seadmed võivad määrata globaalse palli lühikese. Uued konfiguratsioonid kasutavad Ctrl+Shift+Space; olemasolevad konfiguratsioonid ei saa automaatselt uut globaalset lühendust.

Audio toimingud määravad täpselt mahtu 0 kuni 100 protsenti ja mute, mute või toggle tooteid ja mikrofonid. Valige konkreetne lõpppunkt või jäta see Windows nõuetele. Kättesaadav salvestatud lõpppunkt tekitab vea teise seadme kontrollimise asemel. Värskendavad seadmed laadivad ühendatud seadmeid.

OBS Studio kasutab WebSocket v5: start/stop/toggle/pause/resume registreerimine; start/stop /toggle voolamine; muutke stseene; toggle sissepääs mute. Määrake WebSocket serveri all tööriistad OBS ja seada selle lõpppunkt redaktoris. Kontrollige ühendust ja laadida nimed loeb OBS versiooni, stseene ja sissemakseid ilma salvestamise või voolu alustamata. Valige nimed loendist või sisestage need manuaalselt. Paroolid saab salvestada Windows ja need on välistatud eksporditud konfiguratsioonidest. Ebaõnnestunud OBS ühendused teatavad vea.

Profiilid võivad vastata protsessi nimele ja akna pealkirja osale. Mõlemad tingimused peavad vastama. Lock Profile takistab seansi automaatset vahetamist. Capture Conditions Foreground kasutab viimast keskendunud välist rakendust.

Teksti- ja klippetoetuste toetus `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, ja `{{user}}`. Kuupäevade kasutamine `yyyy-MM-dd`; Aegade kasutamine `HH:mm`. Tuntud asjad jäävad muutumatuks. `{{profile}}` Ettevalmistatud tegevuse profiil; `{{process}}` Clipboard sisu loetakse ainult siis, kui seda kohapeal kasutatakse.Clipboard tegevus kirjutab teksti, kuid ei pane automaatselt.

Aktsia minimeerimine, maksimeerimine, taastamine või akna paigaldamine oma monitorite tööpiirkonna vasakul / paremal küljel. Süsteemi tegevuslõik Windows. Need meetmed toimuvad ainult selgesõnaliselt kutsutud korral.

Ettepanek näitab samme, kordi ja muutusi ilma rakendamata. Undo ja Redo säilitavad kuni 100 kujunduse seisundit; tekstikotid säilitavad Ctrl+Z-i kohandamist. Märkige lemmikud redaktoris või tile menüüs ja filtreerige need dashboardsil. Rakenduskontod kehtivad ainult praeguse istungi suhtes.

Üksikasjalik tekst, lühend ja akna toimingud, mis on käivitatud vahendist, aktiveerivad viimast välist sihtpinda. Palett näitab seda sihipärast konteksti. Kui sihtmärk on suletud või Windows keelab aktiveerimise, lõpetatakse rakendamine sõnumiga.

## Keyboard abi ja tagasiside

Special-Key Wizard on klaviatuuri lehel ja palettis. See loetleb G-tähti, mis on teatatud iCUE. Detect Key Press ajutiselt neid ilma nende määratud tegevuste täitmata. Tavalised sidemed naasevad pärast avastamist, tühistamist või sulgemist. Kataloogi funktsioon ja valitud võti avatud kui salvestamata disain; duplikate sidemeid kontrollitakse.

Keyboard Overlay näitab valitud viitearvu ja aktiivseid sidemeid. Hoidke Ctrl+Alt+F1 või tõmmake see palli või klaviatuuri lehel. See ei võta tähelepanu ja laseb hiire klikkide läbida. Profiili ja siduvate muudatuste ajakohastamine, kui see on nähtav; tavalised Windows võtmed on rõhutatud. Tootja-spetsiifilised kontrollid ei pruugi pakkuda samaväärset Windows märksignaali.

Täitmise tagasiside näitab ootamist, samm / kordumise edusamme, lõpetamist, tühistamist või ebaõnnestumist ilma keskendumiseta või sisestatud teksti avaldamata. Commands Executed kinnitab käsu täitmist, mitte iseseisvalt soovitud elatist kolmandate osapoolte rakenduses. Vead on loetletud tegevusloogis.

## Seadmed ja Corsair

Corsair G1 – G6 võib määrata otse ametliku iCUE SDK kaudu. Valige seade ja võtmed tegevuse redigeeri ja salvestada või valmistada kavand K100 klaviatuuri vaatenurgast. Seadmed kehtivad valitud seadme ja aktiivse profiili suhtes; pausi keelab ka need. iCUE peab toimuma ja võimaldama juurdepääsu. Fn, iCUE rattad, profiil ja lukud ei saa kaardistada läbi selle G-tähti API.

Seadmete leht loetleb Windows klaviatuuri vahendid ja värskendatakse perioodiliselt ja kui seadmed ühenduvad. Füüsiline klaviatuur võib esile tuua mitmeid HID-vahendeid. Standardne globaalne hotkeys töötab tootjate vahel.

Corsair avastamine kasutab kättesaadavat seadme teavet. RGB-i kontroll vajab ühilduvat seadmet, mis töötab iCUE ja ametlikku iCUE SDK; rakendus teavitab tegelikku kättesaadavust. Teised tootjad saavad praegu tuvastamise ja standardmeetmete. Väljaspool Corsair G-tõu integreerimist töötavad omadused, kui Windows saavad need regulaarseks sisendiks. Rakendus ei paigalda klaviatuuri.

## Keeled

Settings pakub 27 keeli: saksa, inglise, prantsuse, hispaania, itaalia, portugali, hollandi, tšehhi, Slovakkia, rumeenia, kreeka, taani, Rootsi, Norra, Soome, Eesti, Läti, Leedu, Sloveenia, Horvaatia, Ukraina, Vene, Türgi ja Serbiani Ladina skripti. Automaatne jälgib Windows näidiskeelt. Tõlked on koondatud kohalikult. Layouts, hotkeys ja kasutaja loodud nimed on sõltumatu vahendite keelt.

Täisdokumentatsioon on kättesaadav kõigis 27 rakenduskeeltes [Keele indeks](README.md)Ja siis Tõlked on masinabi, originaalallikaga lingid ja struktuurikontrolli; kodaniku kõneleja ülevaade on eraldi. Rakendus aktsepteerib ka piirkondlikke koode `en-US`, `de-DE`, `fr-FR` ja `it-IT` ning kasutab nende numbrivorminguid. Praegused keelekoodid on ühilduvad.

## Layouts ja tootja uuringud

Keyboard View pakub täielikku ja mõtlematu ISO-DE/ANSI-US viiteid pluss Corsair K100 RGB paigutusi. Geomeetria esindab positsioone, lai/tall võtmeid ja ISO Enter. Standardi paigaldused on üldised viited. K100 paigaldused järgivad dokumenteeritud kontrolle ilma millimeetri täpsete mõõtmete nõudmiseta. Valige õige keel/geomeetria variant: mudeli nimi või USB VID/PID ei kehtesta konkreetset varianti.

Mapping lahendab füüsilisi skaneerimiskodeid aktiivse Windows sisendilahenduse abil. Töövool koostab eelnõu ja säilitab salvestuse / konflikti kontrolli. Standardi sidemed on ülemaailmsed Windows kuumad, mis ei ole ühele ühendatud klaviatuurile isoleeritud. Corsair G1 – G6 kasutatakse seade-specifikat iCUE integreerimist. Muud omandirežiimid nõuavad adapteri või tootja-konfiguuride lühendamist. Peamine Enter ja numbriline võtmepank Enter jagavad globaalsete hotkeyside virtuaalse võtme identiteeti.

See on [Tootja uuringud](research/tastaturhersteller-sonderfunktionen.md) ja [CSV](research/tastaturhersteller-sonderfunktionen.csv) katab 74 brändi / tootja rühmad, mis eristavad konfigureerijad, sissejuhatavad SDK-d ja valgustusAPI. [Layout tõendid](research/keyboard-layout-evidence.md) Registreerimismudel, SKU ja moodulite erinevused. Teadusuuringud ei tähenda, et hardware toetus rakendatakse.

## Andmed

Konfigureerimine salvestatakse kohalikus rakenduse andmetes. Impordi ja ekspordi kasutamine JSON. Importimine ennustab konfigureerimist, asendab pärast kinnitust praeguse konfigureeringu ja keelab imporditud sidemeid kuni manuaalse aktiveerimiseni. Tekstiakti sisu ei koguta keistroke logina. Credential Manager'i paroole eraldatakse eksporditud profiilidest.

## Kontrollimine

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows kontrollid võivad kontrollida riistvara ja ajutiselt konfigureerida / vabastada toetatud iCUE sidemeid. Kasutage neid ilma teiseta, kasutades neid sidemeid. Audio endpoint kontrollid ei muuda elava mahtu ega mute seadeid.

Põlisrahv ei saa ületada Windows privileege. Tavaliselt käivitatud juhendaja ei saa siseneda administraatori rakendusse. Hoidud või hõivatud hotkeys ilmuvad konfliktidena.

See on [Dokumenteerimise indeks](README.md) lingid ajaloolised disainid, plaanid, teadusuuringud ja kuupäevane testide aruanded. Ajaloolised plaanid kirjeldavad nende salvestatud ulatust, mitte praegust funktsiooni toetust.

### Töövoogude mudelid ja sihtpinda ootavad

Kataloog sisaldab kuut redigeeritavat töövoolu: tööruum, kohtumine, voolamine, uuringud, projekti vahetamine ja teksti lõikamine. Otsige `workflow.` kataloogis või juhendipalettis. Töövoo valimine avab ebaõnnestunud projekti; see ei tööta kohe. Kõik sammud säilitatakse, kui lisate olemasoleva makro töövoolu või määrate selle Corsair võtmele. Eesmärk URL-d, programmi teed, profiilid ja OBS stseenid enne salvestamist.

See on **Oodake sihtpinda** samm aktsepteerib protsessi nime (ga või ilma `.exe`), ebatõenäoline pealkiri ja ajakava 1–600,000 ms. Vähemalt üks filtreerimine on vajalik; mõlemad filtrid peavad olema kooskõlas, kui need on tarnitud. makro jätkub alles pärast vastav nähtav aken vastab ja jõuab esikohale. Kasutage konkreetset pealkirja, kui mitmed aknad jagavad protsessi. tühistamine või aegumine lõpetab järgnevad sammud. akna valmisolek ei garanteeri, et dokument on laaditud või väline teenus nagu OBS WebSocket on ühendatud.

Ekraani heledus: otsimine **Sekraani kiirguse suurendamine**, **Sekraania kiirguse vähenemine** või **Kraan kiirgus 50 protsendil** tegevuskataloos. Suhtelised toimingud muudavad heledust 10 protsendipunktidega; fikseeritud väärtused ulatuvad 0 kuni 100. Toetatavad sisekujundused kasutavad WMI-d; ühilduvad väliskujundused peavad oma menüüs DDC/CI aktiveerima. Eeldatult mõjutab tegevus kõiki toetatud ekraane; redigeeri saab valida selge monitor. Ühendused ja dokid võivad toetust piirata.

### laiendatud kontroll

Avage käskpalett koos **Ctrl+K, kui rakenduse aken on keskendunud**. Otsingud **Töövoolu disainer**, **Rekordid makro**, **Sünkroonitud profiilid**, või **Peamine diagnoos** (redigeerimine järgib teie valitud keelt). töövoolu disainer kohandab valitud trükist: faili / protsessi / akna tingimused, edasikanded ja lõpetage / jätkake vea käitlemist. salvestus salvestab lühikesed koodid ja ootab, mitte tavaline kirjutamine; Escape lõpetab salvestamise..

Läbipaistvuse sammas valige kõik toetatud ekraanid või konkreetne monitor. Ebaselge valitud monitor tekitab vea, mitte vaikselt muudab teise ekraani. Raamat näitab elavat heledust, heli ja OBS lugemist, kui nende vastavad integratsioonid on kättesaadavad; kättesaamatu lugemine ei esine mõõdetuna.

Tile editoris määrake toetatud füüsiline spetsiaalne võti ja valige kohe trükkimine, trükkimise või topelttrükkimine. Held kihti valikud ajutiselt aktiveerib veel üks kaardistamine ja taastada eelmine kiht vabastamise. Gestid ja kihid kehtivad Corsair SDK G-tõmmetele ja seadme spetsiifilistele spetsiaalsetele võtmetele, mitte tavalistele globaalsetele hotkeylidele. Standardse võtmefunktsiooni toetab F13–F24 ja klaviatuuri meedia võtmeid. Omandatud Fn, profiili ja diali kontrollid vajavad endiselt vastavat tarnija adapteri või standardse võtme kaardistamist. Raw Input jälgib võtme ja ei takista selle normaalset käitumist Windows. Peamised diagnoosid selgitavad profiili, pausi, sidumise ja sidumisprobleeme.

Profiili sünkroniseerimine vahetab failid valitud faili kaudu, sealhulgas faili, mida haldab teie olemasolev pilvklient. Vaadake eelvaadet enne importi; impordid hakkavad välja lülitama ja eemaldage hardware-spetsiifilised kaardid. Revisioonid takistavad vaikseid ülekirjutusi ja tegevus sisaldub selgesõnaliselt. MAKRIVO ei paku pilvakontot ega hoia pilvakrediteeti. Nüüd kasutab tray MAKRIVO oma teal M-i ja näitab pausi amber märgi.

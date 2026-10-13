<!-- generated-doc locale=et-EE source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../README.md)

# MAKRIVO

[Täisdokumentatsioon kõigis 27 keeltes](README.md) · [Kasutaja käsiraamat](HANDBOOK.md)

**Üheskoos Aivana.** [Praegused omadused ja vabastamise piirangud](FEATURES.md) · [Laiendatud testid](testing/2026-10-12-six-extensions.md) · [Kohalik AI assistent prototüüp](testing/2026-10-12-local-meeting-assistant.md)

** inglise keel (USA) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Kohalik Windows kiirklahvikontroller Stream Decki laadsete paanide, profiilide, makrode ja klaviatuurituvastusega. Kasutajaliides toetab 27 keelt. Igapäevaseks kasutamiseks pole vaja kontot ega pilveühendust.

[Dokumenteerimine](README.md) · [Kontrollimine](VERIFICATION.md) · [Feature testaruanne](testing/2026-10-09-usp-release.md)

## Alustamine

Pärast avaldamist asub rakendus failis `artifacts/win-x64/MAKRIVO.exe`. Hoidke kogu väljundkaust koos; võite selle mujale kopeerida. Esialgne sihtplatvorm on Windows 11 x64. Ehitusartefaktid on Gitist välja jäetud; lähtekoodi väljavõte ei sisalda valmis käivitusfaili.

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
4. Salvestage. Muudatused jäävad salvestamiseni mustandiks; loobumine taastab salvestatud versiooni. Paani Play nupp ja selle kiirklahv kasutavad sama täitmisteed.
5. Reorder pilved ja makro sammud drag-and-drop või pliiatsid. Seadmed lihtsustavad seadistamist. Makro võib korrata kuni 100 korda. Tühistada vahenduse, süsteemi tray menüü või Ctrl+Alt+Escape eelnevalt.

Rakenduspõhised profiilid kasutavad protsessi nime ilma täieliku teeta. Mitme vaste korral otsustab profiilide järjekord; vaikeprofiil on varuvalik. Automaatne profiilivalik tuleb Settings jaotises lubada.

Peaakna sulgemisel jääb rakendus vaikimisi süsteemisalve. Uuesti avamiseks tehke salveikoonil topeltklõps; väljumiseks kasutage salve menüüd. Koos Windows käivitumine on valikuline ja vaikimisi keelatud. Kiirklahve saab keskselt peatada.

## Toimingud ja juhtimispalett

Add Action avab otsinguga kataloogi, kus on 90 eelseadistust kaheksas kategoorias. Igal on kirjeldus ja eelvaade. Makroredaktor kasutab sama kataloogi sammude lisamiseks või asendamiseks. Selles redigeerimistöövoos eelseadistuse valimine loob mustandi ilma käivitamiseta.

Käsupalett avaneb päisest või rakenduses klahvidega Ctrl+K. See otsib profiilide salvestatud toiminguid, kataloogi eelseadistusi, profiilivahetust, navigeerimist, pausi/jätkamist, makrode tühistamist ja importi/eksporti. Filtreerige salvestatud toimingute, kataloogifunktsioonide või rakenduskäskude järgi. Nooleklahvid valivad, Enter aktiveerib, Ctrl+Enter redigeerib redigeeritavat kirjet ja Escape sulgeb. Lemmikud kuvatakse esimesena. Valmiskäsud võivad käivituda kohe; seadistamist nõudvad eelseadistused avavad mustandi. Settings võib määrata globaalse paleti kiirklahvi. Uued konfiguratsioonid kasutavad Ctrl+Shift+Space; olemasolevatele ei lisata uut globaalset kiirklahvi automaatselt.

Helitoimingud määravad helitugevuse täpselt vahemikus 0 kuni 100 protsenti ning vaigistavad, tühistavad vaigistuse või vahetavad väljund- ja mikrofoniseadmete vaigistuse olekut. Valige kindel lõpp-punkt või jätke valimata, et kasutada Windows vaikeseadet. Kättesaamatu salvestatud lõpp-punkt põhjustab vea, mitte teise seadme juhtimist. Refresh Devices laadib ühendatud seadmed uuesti.

OBS Studio kasutab WebSocket v5 ühendust: salvestamise alustamine/peatamine/ümberlülitamine/paus/jätkamine; voogedastuse alustamine/peatamine/ümberlülitamine; stseenivahetus; sisendi vaigistuse ümberlülitamine. Lubage OBS-is Tools all WebSocket server ja määrake redaktoris lõpp-punkt. Check Connection and Load Names loeb OBS versiooni, stseenid ja sisendid ilma salvestamist või voogedastust alustamata. Valige nimed loendist või sisestage käsitsi. Paroole saab salvestada Windows Credential Manageris ja need jäetakse eksporditud konfiguratsioonidest välja. Kättesaamatu OBS ühenduse korral teatatakse veast.

Profiilid võivad vastata protsessi nimele ja akna pealkirja osale. Mõlemad määratud tingimused peavad vastama. Lock Profile takistab seansi ajal automaatset profiilivahetust. Capture Conditions from Foreground kasutab viimast fookuses olnud välist rakendust.

Teksti- ja lõikelauatoimingud toetavad `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ja `{{user}}`. Kuupäevad kasutavad vormingut `yyyy-MM-dd`; kellaajad vormingut `HH:mm`. Tundmatud kohatäited jäävad muutmata. `{{profile}}` tähistab käivitatud toimingu profiili; `{{process}}` välise rakenduse konteksti. Lõikelauasisu loetakse ainult selle kohatäite kasutamisel. Lõikelauatoiming kirjutab teksti, kuid ei kleebi seda automaatselt.

Aktsia minimeerimine, maksimeerimine, taastamine või akna paigaldamine oma monitorite tööpiirkonna vasakul / paremal küljel. Süsteemi tegevuslõik Windows. Need meetmed toimuvad ainult selgesõnaliselt kutsutud korral.

Preview näitab samme, kordusi ja muutujaid ilma käivitamiseta. Undo ja Redo säilitavad kuni 100 mustandiolekut; tekstiväljad säilitavad tavapärase Ctrl+Z redigeerimise. Märkige lemmikud redaktoris või paanimenüüs ja filtreerige neid töölaual. Täitmiste loendurid kehtivad ainult praeguse seansi jooksul.

Kasutajaliidesest käivitatud teksti-, kiirklahvi- ja aknatoimingud aktiveerivad viimase välise sihtakna. Palett näitab sihtmärgi konteksti. Kui sihtmärk on suletud või Windows keelab aktiveerimise, peatub täitmine koos teatega.

## Keyboard abi ja tagasiside

Special-Key Wizard asub klaviatuurilehel ja paletis. See loetleb iCUE teatatud G-klahvid. Detect Key Press jäädvustab neid ajutiselt ilma määratud toiminguid käivitamata. Tavalised seosed taastuvad pärast tuvastamist, tühistamist või sulgemist. Kataloogifunktsioon ja valitud klahv avanevad salvestamata mustandina; kontrollitakse korduvaid seoseid.

Keyboard Overlay näitab valitud näidispaigutust ja aktiivseid seoseid. Hoidke all Ctrl+Alt+F1 või lülitage see sisse/välja paletist või klaviatuurilehelt. See ei võta fookust ja laseb hiireklõpsud läbi. Profiili- ja seosemuudatused uuenevad nähtaval olles; tavalised Windows klahvivajutused tõstetakse esile. Tootjapõhised juhtelemendid ei pruugi anda vastavat Windows klahvisignaali.

Täitmistagasiside näitab ootamist, sammu/korduse edenemist, lõpetamist, tühistamist või tõrget ilma fookust võtmata või sisestatud teksti avaldamata. Commands Executed kinnitab käsu täitmist, mitte sõltumatult päritud reaalajaolekut kolmanda osapoole rakenduses. Veateave on tegevuslogis.

## Seadmed ja Corsair

Corsair G1–G6 saab määrata otse ametliku iCUE SDK kaudu. Valige toiminguredaktoris seade ja klahv ning salvestage või valmistage mustand ette K100 klaviatuurivaatest. Seosed kehtivad valitud seadmele ja aktiivsele profiilile; paus keelab ka need. iCUE peab töötama ja lubama juurdepääsu. Fn-, iCUE ratta-, profiili- ja lukustusjuhtelemente ei saa selle G-key API kaudu vastendada.

Seadmete leht loetleb Windows klaviatuuri vahendid ja värskendatakse perioodiliselt ja kui seadmed ühenduvad. Füüsiline klaviatuur võib esile tuua mitmeid HID-vahendeid. Standardne globaalne hotkeys töötab tootjate vahel.

Corsair tuvastamine kasutab saadaolevat seadmeteavet. RGB juhtimine nõuab ühilduvat riistvara, töötavat iCUE-d ja ametlikku iCUE SDK-d; rakendus teatab tegeliku kättesaadavuse. Teiste tootjate jaoks on praegu tuvastamine ja standardtoimingud. Väljaspool Corsair G-key integratsiooni toimivad tootjapõhised juhtelemendid siis, kui Windows saab need tavasisendina. Rakendus ei paigalda klaviatuuridraiverit.

## Keeled

Settings pakub 27 keelt: saksa, inglise, prantsuse, hispaania, itaalia, portugali, hollandi, poola, tšehhi, slovaki, ungari, rumeenia, bulgaaria, kreeka, taani, rootsi, norra, soome, eesti, läti, leedu, sloveeni, horvaadi, ukraina, vene, türgi ja ladina kirjas serbia. Automaatne valik järgib Windows kuvakeelt. Tõlked on kohalikult kaasas. Paigutused, kiirklahvid ja kasutaja loodud nimed ei sõltu kasutajaliidese keelest.

Täisdokumentatsioon on kättesaadav kõigis 27 rakenduskeeltes [Keele indeks](README.md)Ja siis Tõlked on masinabi, originaalallikaga lingid ja struktuurikontrolli; kodaniku kõneleja ülevaade on eraldi. Rakendus aktsepteerib ka piirkondlikke koode `en-US`, `de-DE`, `fr-FR` ja `it-IT` ning kasutab nende numbrivorminguid. Praegused keelekoodid on ühilduvad.

## Layouts ja tootja uuringud

Klaviatuurivaade pakub täissuuruses ja numbriklahvistikuta ISO-DE/ANSI-US näidispaigutusi ning Corsair K100 RGB paigutusi. Geomeetria kujutab asukohti, laiu/kõrgeid klahve ja ISO Enterit. Standardpaigutused on üldised näidised. K100 paigutused järgivad dokumenteeritud juhtelemente ega väida millimeetritäpsust. Valige õige keele-/geomeetriavariant: mudelinimi või USB VID/PID ei määra kindlat varianti.

Vastendus lahendab füüsilised skannimiskoodid aktiivse Windows sisestuspaigutuse abil. Töövoog valmistab ette mustandi ja säilitab salvestamise/konfliktikontrollid. Tavalised seosed on globaalsed Windows kiirklahvid ega piirdu ühe ühendatud klaviatuuriga. Corsair G1–G6 kasutavad seadmepõhist iCUE integratsiooni. Muud tootjapõhised juhtelemendid vajavad adapterit või tootja seadistatud kiirklahve. Põhi-Enter ja numbriklahvistiku Enter jagavad globaalsete kiirklahvide jaoks sama virtuaalklahvi identiteeti.

See on [Tootja uuringud](research/tastaturhersteller-sonderfunktionen.md) ja [CSV](research/tastaturhersteller-sonderfunktionen.csv) katab 74 brändi / tootja rühmad, mis eristavad konfigureerijad, sissejuhatavad SDK-d ja valgustusAPI. [Layout tõendid](research/keyboard-layout-evidence.md) Registreerimismudel, SKU ja moodulite erinevused. Teadusuuringud ei tähenda, et hardware toetus rakendatakse.

## Andmed

Konfiguratsioon salvestatakse kohalikesse rakenduseandmetesse. Import/eksport kasutab JSON-i. Import näitab eelvaadet, asendab pärast kinnitust praeguse konfiguratsiooni ja keelab imporditud seosed kuni nende käsitsi lubamiseni. Tekstitoimingute sisu ei koguta klahvivajutuste logina. Credential Manageri paroolid on eksporditud profiilidest eraldi.

## Kontrollimine

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows kontrollid võivad uurida riistvara ning ajutiselt seadistada/vabastada toetatud iCUE seoseid. Käivitage need ilma teise neid seoseid kasutava kontrollerita. Heli lõpp-punktide kontrollid ei muuda tegelikke helitugevuse ega vaigistuse seadeid.

Natiivne sisend ei saa mööda minna Windows õigustest. Tavaliselt käivitatud kontroller ei pruugi saata sisendit administraatoriõigustega rakendusele. Reserveeritud või hõivatud kiirklahvid kuvatakse konfliktidena.

See on [Dokumenteerimise indeks](README.md) lingid ajaloolised disainid, plaanid, teadusuuringud ja kuupäevane testide aruanded. Ajaloolised plaanid kirjeldavad nende salvestatud ulatust, mitte praegust funktsiooni toetust.

### Töövoogude mudelid ja sihtpinda ootavad

Kataloog sisaldab kuut redigeeritavat töövoogu: tööruum, koosolek, voogedastus, uuring, projektivahetus ja tekstilõik. Otsige kataloogis või käsupaletis `workflow.`. Töövoo valik avab salvestamata mustandi ega käivitu kunagi kohe. Kõik sammud säilivad töövoo lisamisel olemasolevale makrole või määramisel Corsair klahvile. Enne salvestamist seadistage näidis-URL-id, programmiteed, profiilid ja OBS stseenid.

Samm **Wait for target window** võtab protsessinime (koos `.exe` lõpuga või ilma), tõstutundetu pealkirja alamstringi ja ajalõpu 1–600000 ms. Vähemalt üks filter on nõutav; mõlema määramisel peavad mõlemad vastama. Makro jätkub alles siis, kui sobiv nähtav aken reageerib ja jõuab esiplaanile. Kui sama protsess kasutab mitut akent, määrake konkreetne pealkiri. Tühistamine või ajalõpp peatab järgnevad sammud. Akna valmisolek ei garanteeri dokumendi laadimist ega ühendust välisteenusega, näiteks OBS WebSocket.

Ekraani heledus: otsige toimingukataloogist **Increase screen brightness**, **Decrease screen brightness** või **Screen brightness at 50 percent**. Suhtelised toimingud muudavad heledust 10 protsendipunkti võrra; fikseeritud väärtused on vahemikus 0 kuni 100. Toetatud siseekraanid kasutavad WMI-d; ühilduvatel välismonitoridel peab menüüs olema lubatud DDC/CI. Vaikimisi mõjutab toiming kõiki toetatud ekraane; redaktoris saab valida kindla monitori. Ühendused ja dokid võivad tuge piirata.

### laiendatud kontroll

Avage käsupalett klahvidega **Ctrl+K, kui rakenduse aken on fookuses**. Otsige **Workflow designer**, **Record macro**, **Synchronize profiles** või **Key diagnostics** (sõnastus vastab valitud keelele). Töövoodisainer redigeerib valitud paani mustandit: faili-/protsessi-/aknatingimusi, edasisuunaliste vahelejätmiste arvu ning vea korral peatumist/jätkamist. Salvesti jäädvustab kiirklahvikombinatsioone ja ootamisi, mitte tavalist teksti sisestamist; Escape lõpetab salvestamise. Enne kasutuselevõttu kontrollige tulemust.

Heledussammus valige kõik toetatud ekraanid või kindel monitor. Kättesaamatu valitud monitor põhjustab vea, mitte teise ekraani vaikset muutmist. Töölaud näitab heleduse, heli ja OBS reaalajanäite, kui vastavad integratsioonid on kättesaadavad; kättesaamatut näitu ei esitata mõõdetud väärtusena.

Paaniredaktoris määrake toetatud füüsiline eriklahv ning valige kohene vajutus, lühivajutus, allhoidmine või topeltvajutus. Allhoitavad kihivalijad aktiveerivad ajutiselt teise vastenduse ja taastavad vabastamisel eelmise kihi. Žestid ja kihid kehtivad Corsair SDK G-klahvidele ja seadmepõhistele tavalistele eriklahvidele, mitte tavalistele globaalsetele kiirklahvidele. Tavaklahvide jäädvustamine toetab F13–F24 ja klaviatuuri meediaklahve. Tootjapõhised Fn-, profiili- ja pöördjuhtseadised vajavad endiselt ühilduvat tootjaadapterit või tootjapoolset vastendust tavaklahvile. Raw Input jälgib klahvi ega tõkesta selle tavalist Windows käitumist. Klahvidiagnostika selgitab profiili-, pausi-, ühendus- ja seoseprobleeme.

Profiilide sünkroonimine vahetab faile valitud kausta kaudu, sealhulgas olemasoleva pilvekliendi hallatava kausta kaudu. Enne importimist kontrollige eelvaadet; imporditud seosed on algselt keelatud ja riistvarapõhised vastendused eemaldatakse. Revisjonid takistavad vaikseid ülekirjutusi ning toimingusisu kaasamine on selgesõnaline valik. MAKRIVO ei paku pilvekontot ega säilita pilve autentimisandmeid. Salves kasutatakse nüüd MAKRIVO enda sinakasrohelist M-ikooni ja pausi ajal merevaigukollast pausimärki.

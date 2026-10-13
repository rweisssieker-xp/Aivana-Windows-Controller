<!-- generated-doc locale=lv-LV source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../README.md)

# MAKRIVO

[Pilnīga dokumentācija visās 27 valodās](README.md) · [Pilna lietotāja rokasgrāmata](HANDBOOK.md)

**ar Aivana.** [Pašreizējais inventārs un atbrīvošanas ierobežojumi](FEATURES.md) · [Paplašināšanās pārbaudes ziņojums](testing/2026-10-12-six-extensions.md) · [Vietējais AI asistents](testing/2026-10-12-local-meeting-assistant.md)

** Angļu valoda ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Vietējs Windows saīšņu kontrolleris ar Stream Deck stila elementiem, profiliem, makro un tastatūru noteikšanu. Saskarne atbalsta 27 valodas. Ikdienas lietošanai nav vajadzīgs konts vai mākoņsavienojums.

[Dokumentācija](README.md) · [Pārbaude](VERIFICATION.md) · [Feature pārbaudes ziņojums](testing/2026-10-09-usp-release.md)

## Sākt

Pēc publicēšanas lietotne atrodas `artifacts/win-x64/MAKRIVO.exe`. Saglabājiet visu izvades mapi kopā; to var kopēt citur. Sākotnējā mērķplatforma ir Windows 11 x64. Būvējuma artefakti ir izslēgti no Git; pirmkoda kopijā nav iepriekš izveidota izpildfaila.

Instalēt .NET 10 SDK, kas norādīts `global.json`, lai izveidotu un veiktu no avota:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Izveidojiet pašu saturu, nepieprasot atsevišķi uzstādītu .NET darbības laiku:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Bāzes darba plūsma

1. Izvēlieties profilu un izveidojiet rīcības lapu.
2. Ievadiet savu nosaukumu, ikonu, krāsu un tastatūras īsziņu. Ctrl/Alt/Shift kombinācijas samazina nejaušu aktivizāciju.
3. Pievienojiet vienu vai vairākus soļus: lietojumprogrammas, faili, tīmekļa vietnes, teksts, īsziņas, plašsaziņas līdzekļi, profila maiņa vai kavēšanās.
4. Saglabājiet. Līdz saglabāšanai izmaiņas paliek melnrakstā; atmešana atjauno saglabāto versiju. Elementa Play poga un tā saīsne izmanto vienu un to pašu izpildes ceļu.
5. Reorder tilti un makro soļi ar drag-and-drop vai pīles pogu. Šablonis vienkāršojas. Macros var atkārtot līdz 100 reizēm. Atcerieties, izmantojot interfeisu, sistēmu tray meniu vai Ctrl+Alt+Escape par nosacījumu.

Lietotnei specifiskie profili izmanto procesa nosaukumu bez pilnā ceļa. Ja atbilst vairāki profili, izvēli nosaka to secība; noklusējuma profils ir rezerves izvēle. Automātiskā profila izvēle jāiespējo Settings.

Aizverot galveno logu, lietotne pēc noklusējuma paliek sistēmas paziņojumu apgabalā. Atveriet to ar dubultklikšķi uz ikonas; iziešanai izmantojiet ikonas izvēlni. Palaišana kopā ar Windows ir izvēles iespēja un pēc noklusējuma atspējota. Saīsnes var centrāli apturēt.

## Akcijas un komandas paletes

Add Action atver meklējamu katalogu ar 90 sagatavēm astoņās kategorijās. Katrai ir apraksts un priekšskatījums. Makro redaktors izmanto to pašu katalogu, lai pievienotu vai aizstātu soļus. Sagataves izvēle šajā rediģēšanas darbplūsmā izveido melnrakstu bez izpildes.

Komandu palete atveras no galvenes vai ar Ctrl+K lietotnē. Tā meklē saglabātās darbības visos profilos, kataloga sagataves, profilu pārslēgšanu, navigāciju, pauzi/atsākšanu, makro atcelšanu un importu/eksportu. Filtrējiet pēc saglabātajām darbībām, kataloga funkcijām vai lietotnes komandām. Bulttaustiņi izvēlas, Enter aktivizē, Ctrl+Enter rediģē rediģējamu ierakstu un Escape aizver. Izlase parādās pirmā. Gatavās kataloga komandas var izpildīties uzreiz; sagataves, kam vajadzīga iestatīšana, atver melnrakstu. Settings var noteikt globālu paletes saīsni. Jaunās konfigurācijās lieto Ctrl+Shift+Space; esošās konfigurācijas jaunu globālu saīsni automātiski nesaņem.

Audio darbības precīzi iestata skaļumu no 0 līdz 100 procentiem un izslēdz, ieslēdz vai pārslēdz skaņu izvades un mikrofona ierīcēm. Izvēlieties konkrētu galapunktu vai atstājiet to nenorādītu, lai izmantotu Windows noklusējumu. Nepieejams saglabātais galapunkts rada kļūdu, nevis vada citu ierīci. Refresh Devices atkārtoti ielādē pievienotās ierīces.

OBS Studio izmanto WebSocket v5: ierakstīšanas sākšana/apturēšana/pārslēgšana/pauze/atsākšana; straumēšanas sākšana/apturēšana/pārslēgšana; ainu maiņa; ievades skaņas izslēgšanas pārslēgšana. Iespējojiet WebSocket serveri OBS izvēlnē Tools un iestatiet galapunktu redaktorā. Check Connection and Load Names nolasa OBS versiju, ainas un ievades, nesākot ierakstīšanu vai straumēšanu. Izvēlieties nosaukumus sarakstā vai ievadiet manuāli. Paroles var saglabāt Windows Credential Manager, un tās netiek iekļautas eksportētajās konfigurācijās. Nepieejami OBS savienojumi ziņo par kļūdu.

Profili var atbilst procesa nosaukumam un loga virsraksta daļai. Abiem norādītajiem nosacījumiem jāatbilst. Lock Profile novērš automātisku pārslēgšanu sesijas laikā. Capture Conditions from Foreground izmanto pēdējo fokusēto ārējo lietotni.

Teksta un starpliktuves darbības atbalsta `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` un `{{user}}`. Datumi izmanto `yyyy-MM-dd`; laiki izmanto `HH:mm`. Nezināmi vietturi paliek nemainīti. `{{profile}}` identificē palaistās darbības profilu; `{{process}}` identificē ārējās lietotnes kontekstu. Starpliktuves saturs tiek lasīts tikai tad, kad izmanto attiecīgo vietturi. Starpliktuves darbība ieraksta tekstu, bet automātiski neielīmē.

Windows darbības samazina, maksimāli atjauno vai organizē logu uz kreisās / labās puses tās monitoru darba zonā. Sistēmas darbības slēgšana Windows. Šie pasākumi tiek veikti tikai tad, ja tie ir skaidri pieprasīti.

Preview rāda soļus, atkārtojumus un mainīgos bez izpildes. Undo un Redo saglabā līdz 100 melnraksta stāvokļiem; teksta lauki saglabā parasto Ctrl+Z rediģēšanu. Atzīmējiet izlasi redaktorā vai elementa izvēlnē un filtrējiet informācijas panelī. Izpildes skaitītāji attiecas tikai uz pašreizējo sesiju.

Saskarnē palaistās teksta, saīšņu un logu darbības aktivizē pēdējo ārējo mērķa logu. Palete rāda mērķa kontekstu. Ja mērķis ir aizvērts vai Windows atsaka aktivizēšanu, izpilde apstājas ar paziņojumu.

## Keyboard palīdzība un feedback

Special-Key Wizard atrodas tastatūras lapā un paletē. Tas uzskaita iCUE paziņotos G taustiņus. Detect Key Press tos īslaicīgi fiksē, neizpildot piešķirtās darbības. Parastās piesaistes atjaunojas pēc noteikšanas, atcelšanas vai aizvēršanas. Kataloga funkcija un izvēlētais taustiņš atveras kā nesaglabāts melnraksts; tiek pārbaudītas dublētas piesaistes.

Keyboard Overlay rāda izvēlēto paraugizkārtojumu un aktīvās piesaistes. Turiet Ctrl+Alt+F1 vai pārslēdziet to paletē vai tastatūras lapā. Tas nepārņem fokusu un ļauj peles klikšķiem iziet cauri. Profila un piesaistu izmaiņas atjauninās, kamēr tas redzams; parastie Windows taustiņu nospiedieni tiek izcelti. Ražotāja īpašie vadības elementi var nenodrošināt atbilstošu Windows taustiņa signālu.

Izpildes atgriezeniskā saite rāda gaidīšanu, soļa/atkārtojuma progresu, pabeigšanu, atcelšanu vai kļūmi, nepārņemot fokusu un neatklājot ievadīto tekstu. Commands Executed apstiprina komandas izpildi, nevis neatkarīgi nolasītu reāllaika stāvokli trešās puses lietotnē. Kļūdu detaļas redzamas darbību žurnālā.

## Ierīces un Corsair

Corsair G1–G6 var piešķirt tieši ar oficiālo iCUE SDK. Darbību redaktorā izvēlieties ierīci un taustiņu un saglabājiet vai sagatavojiet melnrakstu K100 tastatūras skatā. Piesaistes attiecas uz izvēlēto ierīci un aktīvo profilu; pauze atspējo arī tās. iCUE jādarbojas un jāatļauj piekļuve. Fn, iCUE ritenīša, profila un bloķēšanas vadīklas nevar piesaistīt caur šo G-key API.

Ierīces lapa norāda Windows tastatūras saskarnes un periodiski atjaunojas un kad ierīces ir savienotas. Fiziskā tastatūra var izraisīt vairākas HID saskarnes. Standarta globālie hotkeys darbojas starp ražotājiem.

Corsair noteikšana izmanto pieejamo ierīces informāciju. RGB vadībai vajadzīga saderīga aparatūra, darbojošs iCUE un oficiālais iCUE SDK; lietotne ziņo par faktisko pieejamību. Citiem ražotājiem pašlaik pieejama noteikšana un standarta darbības. Ārpus Corsair G-key integrācijas īpašie vadības elementi darbojas, ja Windows tos saņem kā parastu ievadi. Lietotne neinstalē tastatūras draiveri.

## valodas

Settings piedāvā 27 valodas: vācu, angļu, franču, spāņu, itāļu, portugāļu, nīderlandiešu, poļu, čehu, slovāku, ungāru, rumāņu, bulgāru, grieķu, dāņu, zviedru, norvēģu, somu, igauņu, latviešu, lietuviešu, slovēņu, horvātu, ukraiņu, krievu, turku un serbu latīņu rakstībā. Automātiskais režīms izmanto Windows displeja valodu. Tulkojumi ir iekļauti lokāli. Izkārtojumi, saīsnes un lietotāja veidotie nosaukumi nav atkarīgi no saskarnes valodas.

Pilna reģistra dokumentacija ir pieejama visās 27 pieteikuma valodās, izmantojot [valodas indekss](README.md)Ģeogrāfija Tulkojumi ir mašīnsaistīti, ar oriģinālo avotu saiti un strukturālo pārbaudi; dzimtā runātāja pārskats ir atsevišķs. Programma pieņem arī reģionālos kodus `en-US`, `de-DE`, `fr-FR` un `it-IT` un izmanto to numuru formātus. Pastāvošie valodas kodi paliek saderīgi.

## Layouts un ražotāja pētījumi

Tastatūras skats nodrošina pilna izmēra un bez cipartastatūras ISO-DE/ANSI-US paraugus, kā arī Corsair K100 RGB izkārtojumus. Ģeometrija attēlo pozīcijas, platus/augstus taustiņus un ISO Enter. Standarta izkārtojumi ir vispārīgi paraugi. K100 izkārtojumi atbilst dokumentētajām vadīklām, neapgalvojot milimetra precizitāti. Izvēlieties pareizo valodas/ģeometrijas variantu: modeļa nosaukums vai USB VID/PID nenosaka konkrētu variantu.

Piesaiste nosaka fiziskos skenēšanas kodus, izmantojot aktīvo Windows ievades izkārtojumu. Darbplūsma sagatavo melnrakstu un saglabā saglabāšanas/konfliktu pārbaudes. Standarta piesaistes ir globālas Windows saīsnes, nevis izolētas vienai pievienotai tastatūrai. Corsair G1–G6 izmanto ierīcei specifisku iCUE integrāciju. Citiem īpašajiem vadības elementiem vajadzīgs adapters vai ražotāja konfigurētas saīsnes. Galvenajam Enter un cipartastatūras Enter globālajās saīsnēs ir viena virtuālā taustiņa identitāte.

Tās [Ražotājs pētījums](research/tastaturhersteller-sonderfunktionen.md) un [CSV](research/tastaturhersteller-sonderfunktionen.csv) 74 zīmola / ražotāja grupas, kas atšķiras no konfiguratoriem, ieejas SDK un apgaismojuma API. [Layout pierādījumi](research/keyboard-layout-evidence.md) ierakstu modelis, SKU un moduļu atšķirības. Pētniecības apkopojums nenozīmē, ka tiek īstenota aparatūras atbalsts.

## Dati

Konfigurācija tiek glabāta vietējos lietotnes datos. Imports/eksports izmanto JSON. Imports parāda konfigurācijas priekšskatījumu, pēc apstiprināšanas aizstāj pašreizējo konfigurāciju un atspējo importētās piesaistes līdz manuālai iespējošanai. Teksta darbību saturs netiek apkopots kā taustiņu nospiedienu žurnāls. Credential Manager paroles ir atsevišķi no eksportētajiem profiliem.

## Pārbaude

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows pārbaudes var pārbaudīt aparatūru un īslaicīgi konfigurēt/atbrīvot atbalstītās iCUE piesaistes. Palaidiet tās, kad cits kontrolleris neizmanto šīs piesaistes. Audio galapunktu pārbaudes nemaina pašreizējo skaļumu vai skaņas izslēgšanas iestatījumus.

Vietējā ievade nevar apiet Windows privilēģijas. Parasti palaists kontrolleris var nespēt nosūtīt ievadi administratora lietotnei. Rezervētas vai aizņemtas saīsnes tiek parādītas kā konflikti.

Tās [Dokumentācijas indekss](README.md) saites ar vēsturiskiem dizainiem, plāniem, pētījumiem un datētajiem testa ziņojumiem. Vēsturiskie plāni apraksta to ierakstīto apjomu, nevis pašreizējo funkciju atbalsta garantiju.

### Darba plūsmas veidnes un mērķa logu gaita

Katalogā ir sešas rediģējamas darbplūsmas: darbavieta, sanāksme, straumēšana, pētniecība, projekta pārslēgšana un teksta fragments. Katalogā vai komandu paletē meklējiet `workflow.`. Darbplūsmas izvēle atver nesaglabātu melnrakstu; tā nekad neizpildās uzreiz. Visi soļi saglabājas, pievienojot darbplūsmu esošam makro vai piešķirot Corsair taustiņam. Pirms saglabāšanas konfigurējiet parauga URL, programmu ceļus, profilus un OBS ainas.

Solis **Wait for target window** pieņem procesa nosaukumu (ar vai bez `.exe`), reģistrnejutīgu virsraksta apakšvirkni un taimautu 1–600000 ms. Vajadzīgs vismaz viens filtrs; ja norādīti abi, jāatbilst abiem. Makro turpinās tikai pēc tam, kad atbilstošs redzams logs reaģē un nonāk priekšplānā. Izmantojiet konkrētu virsrakstu, ja vienam procesam ir vairāki logi. Atcelšana vai taimauts aptur turpmākos soļus. Loga gatavība negarantē dokumenta ielādi vai ārēja pakalpojuma, piemēram, OBS WebSocket, savienojumu.

Displeja spilgtums: darbību katalogā meklējiet **Increase screen brightness**, **Decrease screen brightness** vai **Screen brightness at 50 percent**. Relatīvās darbības maina spilgtumu par 10 procentpunktiem; fiksētās vērtības ir no 0 līdz 100. Atbalstītie iekšējie displeji izmanto WMI; saderīgu ārējo monitoru izvēlnē jābūt iespējotam DDC/CI. Pēc noklusējuma darbība ietekmē visus atbalstītos displejus; redaktorā var izvēlēties konkrētu monitoru. Savienojumi un dokstacijas var ierobežot atbalstu.

### Paplašinātas kontroles

Atveriet komandu paleti ar **Ctrl+K, kamēr lietotnes logs ir fokusā**. Meklējiet **Workflow designer**, **Record macro**, **Synchronize profiles** vai **Key diagnostics** (formulējums atbilst izvēlētajai valodai). Darbplūsmas veidotājs rediģē izvēlētā elementa melnrakstu: faila/procesa/loga nosacījumus, uz priekšu izlaižamo soļu skaitu un apstāšanos/turpināšanu kļūdas gadījumā. Ierakstītājs fiksē taustiņu kombinācijas un gaidīšanu, nevis parastu rakstīšanu; Escape aptur ierakstīšanu. Pirms pieņemšanas pārskatiet rezultātu.

Spilgtuma solī izvēlieties visus atbalstītos displejus vai konkrētu monitoru. Nepieejams izvēlētais monitors rada kļūdu, nevis klusi maina citu displeju. Informācijas panelis rāda spilgtuma, audio un OBS reāllaika rādījumus, ja attiecīgās integrācijas ir pieejamas; nepieejams rādījums netiek parādīts kā izmērīta vērtība.

Elementa redaktorā piešķiriet atbalstītu fizisko īpašo taustiņu un izvēlieties tūlītēju nospiešanu, īsu nospiešanu, turēšanu vai dubultnospiešanu. Turētie slāņa atlasītāji īslaicīgi aktivizē citu piesaisti un pēc atlaišanas atjauno iepriekšējo slāni. Žesti un slāņi attiecas uz Corsair SDK G taustiņiem un ierīcei specifiskiem standarta īpašajiem taustiņiem, nevis parastajām globālajām saīsnēm. Standarta taustiņu fiksēšana atbalsta F13–F24 un tastatūras multivides taustiņus. Īpašajiem Fn, profila un grozāmajiem vadības elementiem joprojām vajadzīgs saderīgs ražotāja adapters vai piesaiste standarta taustiņam ražotāja rīkā. Raw Input novēro taustiņu un nenomāc tā parasto Windows darbību. Taustiņu diagnostika izskaidro profila, pauzes, savienojuma un piesaistes problēmas.

Profilu sinhronizācija apmainās ar failiem izvēlētā mapē, tostarp esošā mākoņklienta pārvaldītā mapē. Pirms importēšanas pārskatiet priekšskatījumu; importētās piesaistes sākotnēji ir atspējotas, un aparatūrai specifiskās piesaistes tiek noņemtas. Revīzijas novērš klusu pārrakstīšanu, un darbību satura iekļaušana ir skaidra izvēle. MAKRIVO nenodrošina mākoņkontu un nesaglabā mākoņa akreditācijas datus. Paziņojumu apgabalā tagad izmanto paša MAKRIVO zilganzaļo M ikonu ar dzintarkrāsas pauzes atzīmi, kad darbība apturēta.

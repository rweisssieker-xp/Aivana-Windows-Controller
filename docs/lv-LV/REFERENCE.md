<!-- generated-doc locale=lv-LV source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../README.md)

# MAKRIVO

[Pilnīga dokumentācija visās 27 valodās](README.md) · [Pilna lietotāja rokasgrāmata](HANDBOOK.md)

**ar Aivana.** [Pašreizējais inventārs un atbrīvošanas ierobežojumi](FEATURES.md) · [Paplašināšanās pārbaudes ziņojums](testing/2026-10-12-six-extensions.md) · [Vietējais AI asistents](testing/2026-10-12-local-meeting-assistant.md)

** Angļu valoda ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Vietējais Windows karstā atslēga kontrolieris ar Stream Deck stila plankumiem, profiliem, makro un tastatūras detekciju. Interfeiss atbalsta 27 valodas. Nekas nav nepieciešams ikdienas lietošanai.

[Dokumentācija](README.md) · [Pārbaude](VERIFICATION.md) · [Feature pārbaudes ziņojums](testing/2026-10-09-usp-release.md)

## Sākt

Pēc publicēšanas pieteikums ir `artifacts/win-x64/MAKRIVO.exe`. Saglabājiet visu iznākuma mapi kopā; to var kopēt citur. Windows 11 x64 ir sākotnējais mērķis. Build artefakti ir izslēgti no Git; avota pārbaude neietver iepriekš izgatavotu.

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
4. glābt Izmaiņas paliek projektā, līdz tiek glabāta; atgriež glabāto versiju. Tīkls Spēlēt un tā atslēga izmanto to pašu izpildes ceļu.
5. Reorder tilti un makro soļi ar drag-and-drop vai pīles pogu. Šablonis vienkāršojas. Macros var atkārtot līdz 100 reizēm. Atcerieties, izmantojot interfeisu, sistēmu tray meniu vai Ctrl+Alt+Escape par nosacījumu.

Pieteikuma profili izmanto procesu nosaukumu bez tā pilnas maršrutas. Profila pasūtījums pārtrauc saites; default profils ir atgriešanās. Automātiska profila izvēle jāatbilst iestatījumiem.

Galvenā loga slēgšana saglabā programmu sistēmā. Divkārši noklikšķiniet uz tā tray ikonu, lai to atvērtu; izmantojiet tray meniu, lai iziet. Startup ar Windows ir opcionāls un atslēgts par nosacījumu. Hotkeys var pārtraukt centrāli.

## Akcijas un komandas paletes

Pievienojiet rīcību, atverot meklējamu katalogu ar 90 priekšrakstām astoņās kategorijās. Katram ir apraksts un priekšstats. Makro redaktors izmanto to pašu katalogu, lai pievienotu vai aizstātu soļus. Izvēloties iepriekšējo sadaļu šajā redaktora darba plūsmā, tiek izveidots projekts bez izpildes.

Komandas palete atveras no virsraksta vai ar Ctrl+K aplikācijas iekšpusē. Tas meklē ietaupītus pasākumus caur profiliem, katalogu iepriekšējos noteikumos, profilu pārslēgšanas, navigācijas, pārtraukuma/receses, makro atcelšanas un importēšanas/eksportēšanas. Filtrējiet ar glabātiem pasākumiem, kataloga funkcijām vai lietojumprogrammas komandām. Izvēlies atslēgas, aktivizē Enter, Ctrl+Enter rediģē rediģējamu ievadījumu un slēdz Escape. Pirmkārt parādās mīļākie. Gatavi lietošanai katalogs komandu var veikt nekavējoties; priekšnoteikumi, kas prasa iestatījumu atvērt projektu. Iestatījumi var definēt globālu paletes īsziņu. Jaunas konfigurācijas izmanto Ctrl+Shift+Space; esošās konfigurācijas automātiski nesaņem jaunu globālo īsziņu.

Audio darbība nosaka apjomu tieši no 0 līdz 100 procentiem un mute, bezmute vai toggle iznākumu un mikrofonu ierīces. Izvēlieties konkrētu galamērķi vai atstāj to Windows defaultā. Neizmantots glabāts galamērķis rada kļūdu, nevis kontrolē citu ierīci. Atjaunināt ierīces uzlādē savienotās ierīces.

OBS Studio izmanto WebSocket v5: start/stop/toggle/pause/resume ierakstu; start/stop /toggle plūsmu; mainīt scenārijus; toggle ieejas mute. Ievērojiet WebSocket serveri, kas ir iekļauta Tools OBS un ievietojiet tā galamērķi redaktorā. Pārbaudiet savienojumu un uzlādes nosaukumus, izlasot OBS versiju, skatu un ievades bez ierakstīšanas vai plūsmas. Izvēlieties vārdus no saraksta vai ievadiet tos manuāli. Paroles var glabāt Windows Uzticības pārvaldītājs un ir izslēgts no eksportētajām konfigurācijām. Neizdevīgā OBS saites ziņo par kļūdu.

Profili var atbilst procesa nosaukumam un lauka nosaukuma daļai. Abām nosacījumiem jābūt saderīgām. Lock Profile novērš sesijas automātisko pārtraukšanu. Pieņemšanas nosacījumi no Foreground izmanto pēdējo koncentrēto ārējo pieteikumu.

Teksta un klipu atbalsts `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, un `{{user}}`. Datu izmantošana `yyyy-MM-dd`; Laika izmantošana `HH:mm`. Nezināmas vietas paliek nemainīgas. `{{profile}}` Identificē uzsāktās darbības profilu; `{{process}}` identificē ārējo aplikācijas kontekstu.Clipboard saturs tiek lasīts tikai tad, ja tiek izmantots tas vietnieks.Klipboard rīcība raksta tekstu, bet ne automātiski.

Windows darbības samazina, maksimāli atjauno vai organizē logu uz kreisās / labās puses tās monitoru darba zonā. Sistēmas darbības slēgšana Windows. Šie pasākumi tiek veikti tikai tad, ja tie ir skaidri pieprasīti.

Priekšizrāde parādīs soļus, atkārtojumus un mainīgos bez izpildes. Undo un Redo saglabā līdz 100 dizaina statusu; teksta kastes saglabā oriģinālu Ctrl+Z rediģēšanu. Marķējiet mīļākos redaktorā vai tile izvēlnē un filtrējiet tos uz plāksnes. Izpildes rādītāji attiecas tikai uz pašreizējo sesiju.

Teksta, īsziņas un logu darbības, kas izlaistas no saskarnes, aktivizē pēdējo ārējo mērķa logu. Šīs paletes atspoguļo šo mērķa kontekstu. Ja mērķis ir slēgts vai Windows atteicās aktivizēt, izpilde apstājas ar ziņojumu.

## Keyboard palīdzība un feedback

Special-Key Wizard ir klaviatūras lapā un paletē. Tas norāda uz G-slēgvārdiem, kas ziņoti par iCUE. Detect Key Press pagaidu tos ieguva, neizpildot to piešķirto darbību. Normālas saites atgriežas pēc atklāšanas, atcelšanas vai slēgšanas. Katala funkcija un atlasītais atslēga tiek atvērta kā nepieejams projekts; pārbaudīti duplikāti.

Keyboard Overlay rāda izvēlēto atsauces izkārtojumu un aktīvās saites. Ievadiet Ctrl+Alt+F1 vai izslēdziet to no paletes vai tastatūras lapas. Tas neuzņemas uzmanību un ļauj gultas klikšķi caur. Profila un saistošu izmaiņu atjaunināšana, kamēr redzams; parastās Windows atslēgas tiek uzsvērtas. Ražotāja specifiskās kontroles nedrīkst sniegt ekvivalentu Windows atslēgvārdu.

Izpildes atsauksmes liecina par gaidīšanas, soli / atkārtošanās progresu, pabeigšanu, anulēšanu vai neveiksmi bez koncentrēšanās vai iesniegšanas ievadā tekstā. Komandas izpildīts apstiprina komandas izpildi, nevis neatkarīgi pieprasīto dzīvo stāvokli trešās puses pieteikumā. kļūdas detaļas parādās aktivitātes logā.

## Ierīces un Corsair

Corsair G1 – G6 var piešķirt tieši caur oficiālo iCUE SDK. Izvēlieties ierīci un atslēgu rīcības redaktorā un saglabājiet to vai sagatavojiet projektu no K100 tastatūras skatījuma. Saistības attiecas uz izvēlēto ierīci un aktīvo profilu; pauze tos arī izslēdz. iCUE ir jāveic un jāļauj piekļūt. Fn, iCUE riteņu, profila un slēgšanas kontroles nevar mapēt caur šo G-slēga API.

Ierīces lapa norāda Windows tastatūras saskarnes un periodiski atjaunojas un kad ierīces ir savienotas. Fiziskā tastatūra var izraisīt vairākas HID saskarnes. Standarta globālie hotkeys darbojas starp ražotājiem.

Corsair detekcija izmanto pieejamo ierīces informāciju. RGB kontrolē nepieciešama saderīga aparatūra, kas darbojas iCUE un oficiālais iCUE SDK; lietojumprogramma ziņo par faktisko pieejamību. Citi ražotāji pašlaik saņem detekciju un standarta pasākumus. Ārpus Corsair G atslēgas integrācijas, īpašumtiesību kontroles darbojas, kad Windows saņem tos kā regulāras ieejas. Programma neinstalē tastatūras vadītāju.

## valodas

Iestatījumi piedāvā 27 valodas: vācu, angļu, franču, itāļu, portugāļu, holandiešu, ķīniešu, slovāku, ungāru, bulgāru, grieķu, dāņu, zviedru, norvēģu, finiešu, estoniešu, latviešu, lituāņu, slovēņu, ukraiņu, krievu, turku un serbu latīņu rakstos. Automātiski seko Windows displeja valoda. Tulkojumi tiek veikti vietēji. Layouts, hotkeys un lietotāja radītie nosaukumi ir neatkarīgi no saskarnes valodas.

Pilna reģistra dokumentacija ir pieejama visās 27 pieteikuma valodās, izmantojot [valodas indekss](README.md)Ģeogrāfija Tulkojumi ir mašīnsaistīti, ar oriģinālo avotu saiti un strukturālo pārbaudi; dzimtā runātāja pārskats ir atsevišķs. Programma pieņem arī reģionālos kodus `en-US`, `de-DE`, `fr-FR` un `it-IT` un izmanto to numuru formātus. Pastāvošie valodas kodi paliek saderīgi.

## Layouts un ražotāja pētījumi

Tastāvoklis nodrošina pilnīgu un bezjēdzīgu ISO-DE/ANSI-US atsauces plus Corsair K100 RGB izkārtojumu. Geometrija pārstāv pozīcijas, plašas / skaņas atslēgas un ISO Enter. Standarta izkārtojumi ir ģenētiski atsauces. K100 izkārtojums seko dokumentētiem kontroles veidiem, neuzskatoties par milimetru precīziem izmēriem. Izvēlieties pareizo valodu/geometrijas variantu: modeļa nosaukums vai USB VID/PID nav noteikts konkrēts variants.

Mapping risina fiziskās skenēšanas kodus, izmantojot aktīvo Windows ieejas izkārtojumu. Darba plūsma sagatavo projektu un saglabā glabāšanas/konfliktu pārbaudes. Standarta savienojumi ir globālie Windows sildītāji, kas nav izolēti no vienas savienotas tastatūras. Corsair G1 – G6 izmanto ierīces specifisko iCUE integrāciju. Citas īpašumtiesības prasa adapteru vai ražotāja konfigurētus īsziņas. Main Enter un numeric-keypad Enter dalās ar virtuālo atslēgu identitāti globālajiem atslēgvārdiem.

Tās [Ražotājs pētījums](research/tastaturhersteller-sonderfunktionen.md) un [CSV](research/tastaturhersteller-sonderfunktionen.csv) 74 zīmola / ražotāja grupas, kas atšķiras no konfiguratoriem, ieejas SDK un apgaismojuma API. [Layout pierādījumi](research/keyboard-layout-evidence.md) ierakstu modelis, SKU un moduļu atšķirības. Pētniecības apkopojums nenozīmē, ka tiek īstenota aparatūras atbalsts.

## Dati

Konfigurācija tiek glabāta vietējos pieteikuma datos. Imports / eksports izmanto JSON. Imports paredz konfigurāciju, aizstāj pašreizējo konfigurāciju pēc apstiprināšanas un izslēdz importētos savienojumus līdz manuāli. Teksta darbības saturs netiek savākts kā atslēgvārds. Patiesības vadītāju paroles ir atdalītas no eksportētiem profiliem.

## Pārbaude

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows pārbaudes var pārbaudīt aparatūru un pagaidu konfigurēt/izlaiž atbalstītos iCUE savienojumus. Izveidojiet tos bez cita kontroliera, izmantojot šos savienojumus. Audio endpoint pārbaudes nesamazina dzīvo apjomu vai mute iestatījumus.

Nāves ieņēmumi nevar pārsniegt Windows privilēģijas. Parasti uzsākts kontrolieris nevar sūtīt ievadi administratora pieteikumam. Aizsargāti vai aizņemti hotkeys parādās kā konflikti.

Tās [Dokumentācijas indekss](README.md) saites ar vēsturiskiem dizainiem, plāniem, pētījumiem un datētajiem testa ziņojumiem. Vēsturiskie plāni apraksta to ierakstīto apjomu, nevis pašreizējo funkciju atbalsta garantiju.

### Darba plūsmas veidnes un mērķa logu gaita

Katalogs ietver sešas rediģējamas darba plūsmas: darba telpa, tikšanās, strīds, pētniecība, projekta pārslēgšana un teksta snippet. Meklēt `workflow.` katalogā vai komandas paletē. Izvēloties darba plūsmu, tiek atvērts nepieejams projekts; tas nekad nedarbojas nekavējoties. Visi soļi tiek saglabāti, pievienojot darba plūsmu esošajam makram vai piešķirot to Corsair atslēgam. Iestatīt piemēru URL, programmas ceļus, profili un OBS skatuves pirms ietaupīšanas.

**Cilvēka logam ** pakāpe pieņem procesu nosaukumu (ar vai bez `.exe`), gadījuma necilvēcīgu nosaukuma substrāciju un 1–600,000 ms laiku. Ir nepieciešams vismaz viens filtrs; abiem filtriem ir jāatbilst, kad tie tiek piegādāti. Makrons turpina tikai pēc tam, kad atbilst redzamais loga un sasniedz priekšpusi. Izmantojiet konkrētu nosaukumu, kad vairāki logi dalās ar procesu. Atcelšana vai laika izbeigšana aptur turpmākos soļus. Windows gatavība negarantē, ka ir uzlādēts dokuments vai ir savienots ārējs pakalpojums, piemēram, OBS WebSocket.

Ekrāna spilgtums: meklēt **Skrāsa spilgtumu**, **Skrāss spilgtumu ** vai **Skrāsas spilgtumu par 50 procentiem** darbību katalogā. Relatīvo rīcību skaidrība mainās 10 procentuālos punktos; fiksētās vērtības svārstās no 0 līdz 100. Atbalstītie iekšējie displeji izmanto WMI; savietojamiem ārējiem monitoriem to izvēlnē ir nepieciešams DDC/CI. Parasti darbība ietekmē visus atbalstītos displejus; redaktorā var izvēlēties skaidru monitoru. Saistības un doki var ierobežot atbalstu.

### Paplašinātas kontroles

Atveriet komandas paleti **Ctrl+K, kamēr aplikācijas logs ir fokusēts**. Meklēt **Darba plūsmas dizaineri**, **Makro ierakstu**, **Sinkronizēt profilu**, vai **Galvenais diagnoze** (darba plūsmas dizainers rediģē izvēlētās plūsmas projektu: faila/procesu/vēstures nosacījumus, tālāk skan un pārtrauc/turpina kļūdu apstrādi. ieraksts ieraksta īsziņas un gaida, nevis parasta rakstīšana; Escape pārtrauks ierakstu..

Izvēlieties visu atbalstošo displeju vai konkrētu monitoru. Neizmantots izvēlētais monitors rada kļūdu, nevis klusi mainot citu displeju. Līcīte rāda dzīvu spilgtu, audio un OBS lasījumu, kad ir pieejamas attiecīgās integrācijas; nepieejams lasījums netiek prezentēts kā mērīts vērtība.

Tīkls redaktorā piešķir atbalstošu fizisko īpašo atslēgu un izvēlieties tūlītēju nospiedumu, nospiedumus, aizturēšanu vai dubultus nospiedumiem. Held slāņa izvēlētāji pagaidu aktivizē vēl vienu mapēšanu un atjauno iepriekšējo slāni atbrīvošanas laikā. Gesti un slāņi attiecas uz Corsair SDK G atslēgām un ierīces specifiskajiem īpašiem atslēgiem, nevis parastajiem globālajiem atslēgumiem. Standarta atslēga atbalsta F13–F24 un tastatūras mediju atslēgas. Īpašuma Fn, profila un dial kontroles joprojām prasa saderīgu piegādātāja adapteru vai piegādātājs, kas kartē uz standarta atslēgu. Raw Input novēro atslēgu un nepieļauj tās normālu uzvedību Windows. Galvenās diagnozes izskaidro profilu, pārtraukumu, savienojumu un saistošo problēmu.

Profila sinhronizācija apmainās ar failiem, kuru izvēlaties, tostarp failu, ko pārvalda jūsu esošais mākoņa klients. Pārskatiet iepriekšējo skatījumu pirms importēšanas; imports sāk izslēgt un noņemt aparatūras specifiskas mapēšanas. Revīzijas novērš klusā pārrakstīšanu, un ietver rīcības saturu ir skaidra izvēle. MAKRIVO nesniedz mākoņu kontu vai saglabā mākoņu autentifikācijas. Šobrīd traja izmanto MAKRIVO pašu teju M ikonu un parādās amber pārtraukuma zīme, kad pārtrauc.

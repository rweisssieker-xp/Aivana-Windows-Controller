<!-- generated-doc locale=lv-LV source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../HANDBOOK.md)

# MAKRIVO — Lietotāja rokasgrāmata

Ražotājs: **Aivana**. Dokumentācija snapshot: **2026-10-12**. Šī rokasgrāmata apraksta pašreizējo darba koksnes īstenošanu. eksperimentālās integrācijas un aparatūras atkarīgās funkcijas ir skaidri identificētas. vēstures ziņojumi apraksta to datēto snapshot, nevis jaunu būvniecības garantiju.

## Dokumentācija un valodas

Izmantojiet dokumentācijas valodas izvēlni, lai atvērtu pilnīgu manuālu, funkciju inventāru, problēmu risināšanu, attīstības piezīmes, pētījumu un vēsturisko testu ziņojumus jebkurā no 27 pieteikuma valodām. Izveidotas tulkošanas ir mašīnsaistītas un ir strukturālas pārbaudes; ne visi no tām ir saņēmuši izcelsmes runātāja pārskatus. Tehniskās identifikācijas un izpildāmie piemēri paliek nemainīgi. Sākotnējais avots paliek pieejams salīdzināšanai. Tulkotais licences analīze ir informatīva un neaizstāj oriģinālus trešo pušu juridiskos noteikumus vai galīgo EULA.

## Uzstādīšana un pirmā uzsākšana

Sākotnējais mērķis ir Windows 11 x64. saglabājiet katru failu un apakšsaziņas līdzekli kopā. `MAKRIVO.exe`. Izejvielas pārbaude nav instalētājs. pašu satura paketi ietver savu .NET darbības laiku; paketi, kas ir atkarīgi no rāmja, prasa atbilstošu .NET 10 Desktop un ASP.NET runtimes. ONNX un līdzjutēji jāiekļauj attiecīgajām funkcijām.

1. Sāciet MAKRIVO un izvēlieties saskarnes valodu Settings. Automašīna tiek veikta Windows Keyboard layout ir atsevišķs.
2. Izvēlieties profilu, pēc tam pievienojiet tīli, izmantojot meklējamo rīcības katalogu. Dodiet tam atpazīstamu nosaukumu un izvēlieties tās darbības.
3. Pārbaudiet soļus un vajadzības gadījumā piešķirjiet īsziņu. Izvairieties no kombinācijām, ko jau izmanto Windows vai jūsu mērķa pieteikums.
4. Saglabājiet projektu. Nepieņemtas izmaiņas nav saglabāta konfigurācija.
5. Pārbaudiet ar nekaitīgu rīcību, piemēram, atverot dokumentu, un pārbaudiet izpildes atsauksmes.

Galvenais logs parasti atstāj pieteikumu paziņojuma zonā. Divkārši noklikšķiniet uz tā tray ikonu, lai to atvērtu, vai izmantojiet tray meniu, lai iziet. Sākt ar Windows ir opcionāls un sākotnēji ir atļauts. Pārtraukuma kontrole aptur saikni; ikona parāda pārtraukuma zīmi. Atcerieties, ka darbojas makros, izmantojot IU, tray vai konfigurēto atcelšanas īsziņu; parasti ir `Ctrl+Alt+Escape`.

## Tilti, rīcība un komandas palete

Tilti pieder profilam un var saturēt vairākus pasūtītos soļus. Redaktors atbalsta saglabāt/izslēgt, iepriekšēju skatījumu, undo/redo un soli reorganizāciju. Katalogs nodrošina programmu, dokumentu, URL, teksta, klipu, īsziņas, mediju, audio, logus, sistēmas darbības un atbalstītās integrācijas. Priekšraksts, kas prasa parametrus, atveras kā projektu.

Atvērt paleti ar `Ctrl+K` un MAKRIVO ir koncentrēts. Meklēt glabātās darbības, priekšaprakti, profila pārejas un rīki. Izvēlieties atslēgas; Ievadiet aktivizē; `Ctrl+Enter` rediģē atbalstīto elementu; Escape slēdz. Dažas gatavas komandas tiek izpildītas nekavējoties, bet konfigurētie pasākumi prasa pārskatīšanu. Atsevišķu globālo paletes īsziņu var konfigurēt; jaunas konfigurācijas izmanto `Ctrl+Shift+Space`.

Teksta un klips atbalsts `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` un `{{user}}`. Datums ir `yyyy-MM-dd`; Laika formāts ir `HH:mm`. Nezināmas izmaiņas paliek nemainīgas.Clipboard lasīšana notiek tikai tad, ja to pieprasa.Clip board darbība rakstīs tekstu bez automātisku to pielāgošanu.

Darbības, ko uzsāk no IU, var vērsties uz pēdējo ārējo pieteikumu. Ja mērķis pazūd vai Windows bloķē priekšējo aktivizāciju, manuāli aktivizējiet mērķi un atgrieziet to. Veiksmīga atslēgas piegāde nerada, ka mērķis ir izpildījis savu uzdevumu.

## Profili un darba vieta

Profili var atbilst procesa nosaukumam un lauka nosaukuma daļai. Kad abi ir sadalīti, abiem ir jāsaskaņo. Profila rīkojums izšķir konkurējošus spēles; default profils ir atgriešanās. Iespējami automātiski izvēlēties. Lock Profile novērš pašreizējās sesijas automātisko izmaiņu.

Atvērts darba vietas iestatījums no rīkiem vai paletes. Izvēlieties programmas, dokumentus, tīmekļa vietnes, audio / displeja mērķi vai OBS skatu. Palīgs sagatavo darba vietu, sanāksmes, strīdu, pētījumu vai projekta profilu. Pārbaudiet radītos soļus pirms ietaupīšanas. Iestatījums neizpilda sagatavotos pasākumus.

## Macros, nosacījumi un ieraksts

Macros īsteno savas darbības un atbalsta gaidīšanas, atkārtošanās un atcelšana. Vispārējā atkārtošanās robeža ir 100. Izmantojiet iepriekšējo darbību, pirms to ievadiet citā lietojumā vai mainiet sistēmas stāvokli.

Gaida mērķa logs pieņem procesu, nosaukuma substrāciju un laiku no 1 līdz 600,000 ms. Ir nepieciešams vismaz viens filtrs. Atbilstošs redzamais logs ir jāatbild un jāpārvērš, pirms turpmākie soļi turpināsies. Sagatavots logs nerada, ka dokuments vai pakalpojums ir pabeigts.

Darba plūsmas redaktors pievieno failus, procesus un logus. Fake stāvoklis izslēdz soli un var pārtraukt papildu soļus. Neaizmirstiet nosacījumu, lai izteiktu alternatīvu filiāli. Izvēlieties stop-on-error vai continu-on-err deliberately; turpināt var atstāt vēlākos soļus bez viņu gaidītajām nosacījumiem.

Darbības ieraksts uzņem īsmetrus un gaida, nevis parasta rakstīšanu. Tas atbalsta modifikatoru īsziņas un funkciju atslēgas. Bēgšana pārtrauc ierakstīšanu. Pārskatiet sarakstu un pieņemt to kā neapglabātu tile projektu, pēc tam skaidri glabāt. UI klikšķi ir atsevišķs rīks, kas aprakstīts zemāk.

## Meklēšanas, gestu un slāņu

Tastāvokļa lapa uzskaita konstatētos saskarnes un atsauces izkārtojumus. Vienā fiziskajā tastatūrā var parādīt vairākus saskarnes. Izvēlieties pareizo valodu un ģeometriju; tikai produkta nosaukums nerada ISO vai ANSI izkārtojumu.

Standarta globālie hotkeys attiecas uz klaviatūras. Ierīces specifiskie Raw Input savienojumi ievēro konkrētu ierīci un skenēšanas kodu, tostarp atbalstītās funkcijas un plašsaziņas līdzekļu atslēgas. ** Raw Input netraucē parasto Windows ievietošanu. Ekskluzīva otrā tastatūra nav gatava ražošanai.** Patlaban ir nepieciešama parakstīta, licencēta un pārbaudīta filtra vadības sistēma; neviena no tām nav instalēta MAKRIVO.

Atbalstītie speciālie atslēgas savienojumi var izmantot tūlītēju nospiedumu, nospiedumus vai dubultus nospiedumiem.Held-layer atslēgas īslaicīgi izvēlas citu mapēšanu un atjauno pamata slāni; slāņa-selektoru atslēgas nedarbojas paši makro. 150 Lai 5,000 MS; dubultspiedienu logi no 150 Lai 1,000 MS. Šīs kartes atšķiras no parastām globālajām kartiņām.

Īpašs atslēga varonis atklāj saderīgus atslēgas, neizpildot to piešķirto darbību uzņemšanas laikā. Diagnoze izskaidro nepieejamas ierīces, pārtrauktas saites, profila nepilnības un konflikti. Pārklājums norāda izvēlēto izkārtojumu un saikni; tā pamatoti saglabā īsziņas `Ctrl+Alt+F1`.

| Integrācija | Prasības un ierobežojumi |
| --- | --- |
| Corsair | Running iCUE, SDK piekļuve un saderīgs modelis. G1–G6 un atbalstošās RGB funkcijas; īpašās Fn, riteņi, slēgšanas un profila kontroles parasti nav pakļautas G atslēgas API. |
| Logitech G | Sākotnējās x64 G/LED SDK bibliotēkas prasa skaidru apstiprinājumu. G atslēgas notikumi nevienkārši identificē vairākas identiskas fiziskās tastatūras. |
| Razer | Chroma RGB un standarta ieejas; kartes īpašumtiesību kontroles Synapse, ja nepieciešams. |
| SteelSeries | GameSense RGB un standarta ieejas; kartes īpašuma kontroles GG, ja nepieciešams. |
| Wooting | Kompatīvais oriģināls x64 SDK analogu vērtībām un robežām; normāls HID ievads paliek aktīvs. |
| Citi tastatūras | Standarta sildītāji vai atbalsta fiziskā skenēšanas kods. Fn un īpašumtiesību kontroles ir atkarīgas no modeļa un pārdevēja konfigurācijas. |

Ražotāja pētījums apraksta dokumentētus spēkus, kas nav īstenoti vai pārbaudīti saderību ar katru uzskaitīto ierīci. Jauniem adapteriem joprojām ir nepieciešama reāla aparatūras pieņemšana. Nelietojiet SDK binārus no nenoteiktajiem avotiem; SDK izplatīšanas tiesības ir atdalītas no tehniskās saderības.

## Audio, displeji un OBS

Audio soļi nosaka apjomu no 0 līdz 100 procentiem un mute/unmute/toggle atbalsta iznākumu vai mikrofonu galamērķus. Izvēlieties konkrētu galamērķi vai izmantojiet Windows iestatījumu. Nepazīstamais mērķis rada kļūdu, nevis klusi kontrolē citu ierīci. Atdzesējiet ierīces pēc savienojuma. Endpoint kontroles nav neatkarīgas pa-aplikācijas maisītāja atbalsta prasība.

Rādīt darbības palielinās / samazinās spilgtumu ar 10 procentuālo punktu vai iestatīt vērtību no 0 līdz 100. Iekšējie displeji izmanto atbalstošus WMI saskarnes; ārējiem monitoriem ir nepieciešama DDC/CI atbalsts un parasti to monitoru izvēlne. Dūkas, adapteri un kabeļi var bloķēt kontroli. Izvēlieties visus atbalstītos displejus vai vienu monitoru. Atkopšana var mainīt pieejamās identitātes; pārskatīt glābto mērķi. Nepazīstami lasījumi tiek rādīti kā nepieejami, nevis mērītas vērtības.

OBS prasa WebSocket v5 ievietot OBS rīkus. Konfigurējiet galamērķi, pārbaudiet savienojumu un uzlādi / ievades vārdus pirms ietaupīšanas. Atbalstītie pasākumi ietver skatu pārslēgšanu, starta/pārtraukšanas/pārtraukšanas ierakstu, pārtraukšanas ieraksta un ieejas mute. Paroles var glabāt Windows Uzticības pārvaldītājs un ir izslēgts no konfigurācijas eksporta. Veiksmīga OBS komanda neuzlabo katru lejupslīdes efektu.

## Rezultātu pārbaude un izpildes vēsture

Iespēja pārbaudīt rezultātus atbilstošiem soļiem. Programma atšķiras no iepriekšējas pārbaudes, apstiprinātiem rezultātiem, neveiksmēm, atcelšanu un efektiem, ko nevar pārbaudīt. Procesu eksistence nav gatavība piemērot. Attiecīgie grozījumi, īsziņas un teksta ievades nevar saņemt panākumu apliecību. Pārbaudiet aktivitātes vēsturi un verifikāciju, kad darbība neizdodas. Konfigurācijas kļūdas ir jāatjauno pirms pilnīgas makro.

## UI automātika un klikšķi ieraksts

Pievienojiet UI automācijas posmu, izvēlieties darbību un nokļūstiet mērķim zem rādītāja pēc parādītajām kavēšanās. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` un `WaitState`. Paroles lauki, traucējumi un neskaidri selektoru tiek noraidīti.

UI automatizācija darbojas ierobežotā partnerattiecībā. Tas ir atkarīgs no mērķa pieteikuma Windows pieejamības atbalsta un atļaujas. Spēles ieraksts uzņem atbalstītās darbības un pārtraukumus; tas neuzrāda vispārējo teksta ievietošanu. Pārskatiet un apstipriniet projektu pirms tā pieņemšanas. Izmaiņas ārējā lietojumprogrammā var neesoties. Procesu izolācija ierobežo sadursmes un atcelšanu, bet nav drošības smiltsērkšķis.

## Plugins

Atvērt paplašinājumus, lai pārbaudītu iepakojumu pirms tā importēšanas. Pārbauda iepakojuma struktūru, manifestus un hashes. Ieguvumi sākas ar invaliditāti. Lasīt deklarētos pasākumus un skaidri uzticēt izdevēju pirms atļaujas. Plugins darbojas ar lietotāja atļaujām atsevišķā procesā; šis process ir ** nevis smilšu kastē**. Tikai uzstādīt iepakojumus, kurus jūs uzticaties.

Vadītājs atbalsta iepriekšējās versijas eksportu, deaktivēšanu, izinstalēšanu un atjaunošanu. Atkāpšanās no atkarības bloķē aicinājumu. Piemērs pakete nodrošina skaitīšanas un vietējo valstu darbības. Atbalstītā shēma ir dokumentēta JSON-Schema apakšsēde, ne visas iespējamās JSON-Šema funkcijas. Nav publisko pluginu tirgus.

## Mobilais panelis

1. Atveriet mobilo paneli, izvēlieties vietējo IP un skaidri izvēlieties profilus, kurus izklāstīt. Iepriekš minētais atgriešanās adrese ir pieejama tikai pašā datorā; izvēlieties atbilstošu LAN adresi tālrunim vai planšetdatoram.
2. Sāciet paneli un izlasiet tā HTTPS adresi, sertificējiet pirkstu nospiedumu un vienreizēju sešu skaitļu pāri kodu.
3. Uz ierīci tajā pašā uzticamā tīkla, atverat adresi. Salīdziniet SHA-256 pirkstu nospiedumu caur uzticamu kanālu, pirms uzticaties vietējai sertifikātam. Programma automātiski neinstalē uzticamu sertifikātu.
4. Izmantojiet parādīto kodu un pārbaudiet vienu nekaitīgu tīli. Manual Windows Firewall atļauja var būt nepieciešama, lai piekļūtu LAN. Nelietojiet portālu internetam.
5. Pārtraukt paneli, kad tas ir pabeigts, vai atcelt klientu caur programmu.

Privātās serveru atslēgas ir aizsargātas ar Windows DPAPI; klientu apliecības tiek glabātas kā hashes. Izmaiņa IP vai sertifikāta atjaunošana var prasīt atjaunotu uzticību. Panelis rāda rīcības un verifikācijas stāvokli, nevis visus iespējamos dzīvos sensorus. Tīmekļa pārlūkprogrammas un vietējās HTTPS pārbaudes neaizstāj reālu tālruņa/tableta LAN pieņemšanu.

## Mājas apmācības asistents

Šis opcionālais eksperimentālais asistents ir deficīts. Atveriet to no rīkiem, skaidri ievietojot to, izvēlieties darba laiku/modeli un pievienojiet tikai resursus, kurus vēlaties apsvērt. Nosakiet mērķi, izveidojiet projektu, pārbaudiet neatbilstīgus pieprasījumus un pārsūtīt rezultātu parastam redaktoram. Nekas netiek izpildīts automātiski; glābšana un darbība paliek skaidras darbības. Nav automātisku runtime fallback.

| Runāšana | Iestatīšana |
| --- | --- |
| Ollama | Atsevišķs vietējais pakalpojums un vietēji uzstādīts modelis `127.0.0.1:11434`. Atklāti attālināti / mākoņi ir noraidīti. |
| LM Studio | Vietējais serveris ar vietēji uzlādētu modeli un JSON shēmas atbalstu `127.0.0.1:1234`. Neļauj attālināt vietējo apstrādi. |
| llama.cpp | Vietējais `llama-server` un saderīgs modelis ar JSON shēmas atbalstu `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Uzticams saderīgs vietējais GenAI teksta modeļa mape; izvēlieties `genai_config.json`. Inference izmanto apvienoto partneru procesu bez HTTP. |

HTTP klienti izmanto tikai loopback, neizmanto proxy un neuzrauga rediģējumus. MAKRIVO nevar uzticami noteikt, vai saderīgs vietējais serveris iekšēji sauc tālvadības pakalpojumu. Autentizēti vietējie serveri šajā versijā nav konfigurējami. Modela svari, Python un vietējie inferences serveri netiek automātiski lejupielādēti vai uzstādīti.

ONNX iepakojumi ir nepieciešami `tokenizer.json` un derīgs tērzēšanas veidnes. Atbalstītās veidlapas `tokenizer_config.json` ir riteņa `chat_template`, riteņa ar riteņu `default` vai neskaidra nosaukuma/default veidlapas saraksts. Ja šablonā ir trūkst vai nulle, `chat_template.jinja` tiek izmantots kā atgriešanās. Tīrīti, neskaidri, pārmērīgi vai nepareizi rakstīti veidnes tiek noraidītas. Arbitrāžas ONNX failus, ievadīšanas/izskata modeļus un GPU vienīgos iepakojumus nevar mainīt ar saderīgiem GenAI teksta modeļiem. Uzstādīta GenAI darbā versija ir 0.17.1, CPU. Izmantojiet tikai uzticamus modeļus; procesa atdalīšana nav smilšu kastes.

Palīgs atbalsta izvēlētos programmas bez argumentu, izvēlētus dokumentus un tīmekļa vietnes, un mikrofonu mute, ja tas ir skaidri pieprasīts. Tas nerada ziņojumus, iepazīšanās, savvaļas skriptus, bezmaksas klikšķi, lejupielādes vai headset pāri. Mērķis un resursu nosaukumi tiek nosūtīti uz izvēlēto vietējo darba laiku; resursu ceļus un failu saturu nesniedz atsevišķi. Lietotāja mērķis pats par sevi var saturēt personas datus. Vietējā darba laika tīmekļa uzvedība joprojām ir lietotāja atbildība.

Tās ONNX Prasības budžets ir 90 sekundes, tostarp modelis uzlādes, līdz 3,072 Ātrie tokenus un 1,024 radītie tokeni, arī ierobežoti ar modeli kontekstu. ļaunprātīgi / izkliedēti JSON un nezināmi resursu ID tiek noraidīti. atcelt vai tuvāk pieprasījuma beigām. nejauši modelis darba laika tests nosaka izpildi, nav noderīga plānošanas kvalitāte vai laika ietaupījumi. vietējie eksperimentālo logs satur notikumu identifikatorus un grafiku, nevis mērķa tekstu vai dokumentu saturu; tie netiek automātiski lejupielādēti.

Nākamā maza Instrukt modeļa novērtējums izraisīja nulles spēkā esošos plānus četrās vācu/angļu mēģinājumos. Native inference strādāja, bet modelis neveica stingru plānošanas līgumu. Tas ir negatīvs kvalitātes rezultāts, nevis ieteicamais modelis vai pierādīts noderīgs asistents. Skatīt datēto vietējā braukšanas laika pārbaudes ziņojumu par precīzu modeļa pārskatīšanu un ierobežojumiem.

## Imports, eksports, sinhronizācija un backup

Pārskatiet importēšanas prognozes pirms konfigurācijas aizstāšanas. Importētie pasākumi sākas bez aktivitātes, un pārnēsājama sinhronizācija noņem aparatūras specifiskas saites. Atkārtojiet ierīces un ļaujiet rīcībai domāt pēc pārbaudes. Pārskatiet teksta/akcijas saturu pirms eksporta dalīšanas; paroles izslēgšana nerada visu lietotāja radīto rīcības saturu necilvēcīgu.

Profila sinhronizācija apmainās ar failiem izvēlētajā mapē, opcionāli to pārvalda jūsu esošais mākoņa klients. MAKRIVO nenodrošina mākoņa kontu un nav uzglabā mākoņu autentifikācijas. Pārskati novērš klusos pārrakstījumus. Iekļaut rīcības saturu ir skaidra izvēle. Atjauniniet konfigurāciju pirms aizstājēja importēšanas. Paredzamā konfigurācija ir `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; saderības nolūkā saglabājas vēsturiskais adreses nosaukums. Aizveriet lietotni, pirms to manuāli kopējiet/atjaunojiet. Iegūtie SDK apstiprinājumi un OS aizsargātas autentifikācijas var būt nepieciešamas citā datorā.

## problēmu risināšana

| Simptomi | Ko pārbaudīt |
| --- | --- |
| Īsums nedara neko | Programmas darbojas, saites netiek pārtrauktas, pareizs aktīvs profils, glabāts/atļauts tīrs, īslaicīgi konflikti un pieejamais mērķis. |
| Galvenais logs nav aktivizēts | Manuāli aktivizējiet to. Windows priekšējās robežas un privilēģijas atšķirības var bloķēt automatizāciju. |
| Īpašs atslēga nav atklāta | Pārdevēja pakalpojums/SDK/modelis atbalsts; konfigurē īpašumtiesību kontroles, lai pēc iespējas izdodas atbalsta standarta atslēga. |
| Otrā klavieris arī parasti | Iespējams ar Raw Input Izņēmuma apturēšana ražošanai netiek īstenota. |
| Skaņas vai spilgtums nav pieejams | Atjaunojiet ierīces; pārbaudiet izvēlētās identitātes, galamērķa sesijas, WMI/DDC/CI atbalstu un dot / kabeļa ierobežojumus. |
| OBS offline | WebSocket v5 ir aktivizēts, pareizais galamērķis / paroli, pieejams pakalpojums un derīgie skatuves / ieejas nosaukumi. |
| Automātiski neveiksmīgi | Kompanijas hosts ir iekļauts, unikāls spēkā esošais selektors, pieejams iestatīts mērķis, atbalstīta kontrole un atļaujas. Atgrieziet pārejas laiku tikai pēc mērķa pārskatīšanas. |
| Mobilais panelis nav pieejams | LAN vietā loopback adrese, tāda pati tīkls, serveris darbojas, apstiprināti profili, sertificēt pirkstu nospiedumu / uzticību un manuālo ugunsmūžas atļaujas. |
| Plugins neveiksmīgs | Pakalpojuma integritāte, skaidra uzticēšanās/atļauts stāvoklis, atkarības, protokola saderība un izpilde. |
| Pieprasījums neizdodas | Pareizi izvēlēts vietējais darbības laiks, serviss/modelis gatavs, atbalsta JSON shēma, uzticams saderīgs modeļa mapes un pieprasījuma ierobežojumi. |
| Layout vai teksts šķiet nepareizs | Atsevišķi lietotāja valodu no tastatūras ģeometrijas; pārskatīt logu, pārbaudīt displeja apjomu un ziņot par precīzu lapu / valodu / veidošanu. |

Noderīga kļūda ziņojums ietver Windows versiju, lietojumprogrammu izveidi/komitāciju, valodu, displeja skalēšanu, aparatūras modeli, minimālu secību un attiecīgo redakciju. Noņemiet personīgus ceļus, rīcības saturu un apliecinājumus pirms publicēšanas.

## Uzstādīšana, testēšana un iepakojums

Izmantojiet SDK, kas ir uzstādīts `global.json`. No repozitorijas saknes:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integrācijas testi var atvērt izolētus logus un īslaicīgi konfigurēt/izlaiž atbalstošos piegādātāja saites. Izveidojiet tos bez citas kontroles, kas ir šīs saistības. Iepriekš minētie spilgtuma/audijas pārbaudes ir lasāmas vienīgi; skaidri izstrādāti apkārtējie pārbaudes materiāli ir atsevišķi. Mock protokoli, renderētas ekrāna attēli un nejaušās apgalvojuma grafikas nedrīkst būt reālajās aparatūras/modela kvalitātes pieņemšanas formā.

Pilns izdevējs apkopo uzņēmēju hostus, mobilos aktīvus, piemērs plugins, ONNX darbam un pieejamās atkarības paziņojumus. Saglabājiet savu subdirektori. Veiksmīga būvniecība nav parakstīts instalētājs vai juridisks atbrīvojums. Izejvielas un ēkas var attīstīties atsevišķi; salīdziniet dokumentāciju un apņemiet ar instalēto versiju.

## Licencēšana, privātums un atbrīvošanas statuss

Mērķis ir vienreizēja iepirkšanās ar iegādāto lielāko versiju pastāvīgu izmantošanu; galīgā cena, ierīces atļauja un atjaunināšanas nosacījumi ir jānosaka faktiskajos pārdošanas noteikumos. Produkcijas iegādes/aktivācijas sistēma nav izveidota ar šo rokasgrāmatu. Nav nepieciešama ikdienas pārbaude.

Nelietojiet īpašos SDK binārus bez pierādīta atļauja. Saglabājiet licences un trešo personu paziņojumus par katru nosūtīto atkarību, ieskaitot .NET un ONNX paketes. Modeliem ir savs nosacījums. Datu licences revīzija izpaužas kā dažas jaunas atkarības un nav atskaņošana par šīm pievienošanās. Reģistrācija, kas ir publiska, pati par sevi nedod vispārēju atvērto avotu licences.

Reālā piegādātāja aparatūra, tālruņa/tableta LAN darbība, sajaukts monitoru/DPI pārejas, ilgstoša lietošana, pilnīga valodas pārbaude, ekskluzīva vadītāja atbalsts un noderīga AI plānošanas kvalitāte paliek atsevišķa pieņemšanas darbs. Nekad nepieņemu atļauju tikai no klātbūtnes.

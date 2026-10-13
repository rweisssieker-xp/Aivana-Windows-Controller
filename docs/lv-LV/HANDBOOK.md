<!-- generated-doc locale=lv-LV source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../HANDBOOK.md)

# MAKRIVO — Lietotāja rokasgrāmata

Ražotājs: **Aivana**. Dokumentācijas stāvoklis: **2026-10-12**. Šī rokasgrāmata apraksta pašreizējā darba koka realizāciju. Eksperimentālās integrācijas un no aparatūras atkarīgās funkcijas ir skaidri norādītas. Vēsturiskie pārskati apraksta stāvokli attiecīgajā datumā, negarantējot jaunāka būvējuma darbību.

## Dokumentācija un valodas

Dokumentācijas valodas atlasītājs ļauj atvērt pilno rokasgrāmatu, funkciju sarakstu, problēmu novēršanu, izstrādes piezīmes, pētījumus un vēsturiskos testu pārskatus jebkurā no 27 lietotnes valodām. Ģenerētie tulkojumi ir veidoti ar mašīntulkošanas palīdzību un strukturāli pārbaudīti; ne visus ir pārskatījuši dzimtās valodas lietotāji. Tehniskie identifikatori un izpildāmie piemēri paliek nemainīti. Sākotnējais avots ir pieejams salīdzināšanai. Tulkotā licencēšanas analīze ir informatīva un neaizstāj sākotnējos trešo pušu juridiskos noteikumus vai galīgo EULA.

## Uzstādīšana un pirmā uzsākšana

Sākotnējā mērķplatforma ir Windows 11 x64. Glabājiet visus izplatīšanas pakotnes failus un apakšmapes kopā. Palaidiet `MAKRIVO.exe`. Pirmkoda kopija nav instalētājs. Pašpietiekamās pakotnes ietver savu .NET izpildvidi; no ietvara atkarīgām pakotnēm vajadzīgas atbilstošās .NET 10 Desktop un ASP.NET izpildvides. ONNX un palīgprocesu resursdatoriem jābūt iekļautiem to attiecīgo funkciju lietošanai.

1. Palaidiet MAKRIVO un sadaļā Settings izvēlieties saskarnes valodu. Automātiskais režīms izmanto Windows valodu. Tastatūras izkārtojums ir atsevišķs iestatījums.
2. Izvēlieties profilu un pievienojiet elementu, izmantojot meklējamo darbību katalogu. Piešķiriet tam atpazīstamu nosaukumu un izvēlieties darbības.
3. Pārskatiet soļus un vajadzības gadījumā piešķiriet saīsni. Izvairieties no kombinācijām, ko jau izmanto Windows vai mērķa lietotne.
4. Saglabājiet melnrakstu. Nesaglabātās izmaiņas nav saglabātā konfigurācija.
5. Pārbaudiet ar nekaitīgu rīcību, piemēram, atverot dokumentu, un pārbaudiet izpildes atsauksmes.

Aizverot galveno logu, lietotne parasti turpina darboties paziņojumu apgabalā. Veiciet dubultklikšķi uz tās ikonas, lai atvērtu logu, vai izmantojiet ikonas izvēlni, lai izietu. Palaišana kopā ar Windows ir izvēles iespēja un sākotnēji ir atspējota. Pauzes vadīkla aptur taustiņu piesaistes; ikonā redzama pauzes atzīme. Darbojošos makro atceliet saskarnē, ikonas izvēlnē vai ar konfigurēto atcelšanas saīsni; noklusējums ir `Ctrl+Alt+Escape`.

## Tilti, rīcība un komandas palete

Elementi pieder profiliem un var saturēt vairākus secīgus soļus. Redaktors atbalsta saglabāšanu/atmešanu, priekšskatījumu, atsaukšanu/atjaunošanu un soļu pārkārtošanu. Katalogā ir sagataves programmām, dokumentiem, URL, tekstam, starpliktuvei, saīsnēm, multividei, audio, logiem, sistēmas darbībām un atbalstītajām integrācijām. Sagatave, kurai vajadzīgi parametri, atveras kā melnraksts.

Atveriet paleti ar `Ctrl+K`, kad MAKRIVO ir fokusā. Meklējiet saglabātās darbības, sagataves, profilu pārslēgšanu un rīkus. Bulttaustiņi izvēlas; Enter aktivizē; `Ctrl+Enter` rediģē atbalstītu vienumu; Escape aizver. Dažas gatavās komandas izpildās uzreiz, bet konfigurētās darbības jāpārskata. Var konfigurēt atsevišķu globālu paletes saīsni; jaunās konfigurācijās tā ir `Ctrl+Shift+Space`.

Teksta un starpliktuves soļi atbalsta `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` un `{{user}}`. Datuma formāts ir `yyyy-MM-dd`; laika formāts ir `HH:mm`. Nezināmie mainīgie paliek nemainīti. Starpliktuve tiek lasīta tikai pēc pieprasījuma. Starpliktuves darbība ieraksta tekstu, to automātiski neielīmējot.

Saskarnē palaistās darbības var būt vērstas uz pēdējo ārējo lietotni. Ja mērķis pazūd vai Windows bloķē aktivizēšanu priekšplānā, aktivizējiet to manuāli un mēģiniet vēlreiz. Veiksmīga taustiņu ievades piegāde nepierāda, ka mērķis pabeidza savu uzdevumu.

## Profili un darba vieta

Profili var atbilst procesa nosaukumam un loga virsraksta daļai. Ja iestatīti abi, jāatbilst abiem. Ja atbilst vairāki profili, izvēli nosaka profilu secība; rezerves izvēle ir noklusējuma profils. Automātiskā izvēle ir skaidri jāiespējo. Lock Profile pašreizējā sesijā novērš automātiskas profila izmaiņas.

Atveriet Workplace Setup izvēlnē Tools vai paletē. Izvēlieties programmas, dokumentus, vietnes, audio/displeja mērķi vai OBS ainu. Palīgs sagatavo darbavietas, sanāksmes, straumēšanas, pētniecības vai projekta profilus. Pirms saglabāšanas pārbaudiet ģenerētos soļus. Iestatīšana neizpilda sagatavotās darbības.

## Macros, nosacījumi un ieraksts

Makro izpilda soļus secīgi un atbalsta gaidīšanu, atkārtošanu un atcelšanu. Vispārējais atkārtojumu limits ir 100. Izmantojiet priekšskatījumu, pirms palaižat darbības, kas ievada tekstu citā lietotnē vai maina sistēmas stāvokli.

Wait for Target Window pieņem procesu, virsraksta apakšvirkni un taimautu no 1 līdz 600000 ms. Vajadzīgs vismaz viens filtrs. Atbilstošajam redzamajam logam jāreaģē un jākļūst par priekšplāna logu, pirms turpinās nākamie soļi. Gatavs logs nepierāda, ka dokuments vai pakalpojums ir pabeidzis ielādi.

Darbplūsmas redaktors pievieno faila esamības, procesa darbības un loga virsraksta nosacījumus. Ja nosacījums ir aplams, solis tiek izlaists, un var izlaist arī papildu turpmākos soļus. Lai izteiktu alternatīvu zaru, noliedziet nosacījumu. Apzināti izvēlieties apstāšanos vai turpināšanu kļūdas gadījumā; turpinot vēlākajiem soļiem var nebūt izpildīti nepieciešamie priekšnosacījumi.

Darbību ierakstītājs fiksē taustiņu kombinācijas un gaidīšanu, nevis parastu teksta rakstīšanu. Tas atbalsta saīsnes ar modifikatoriem un funkciju taustiņus. Escape aptur ierakstīšanu. Pārskatiet sarakstu un pieņemiet to kā nesaglabātu elementa melnrakstu, pēc tam skaidri saglabājiet. Saskarnes klikšķu ierakstīšana ir atsevišķs tālāk aprakstīts rīks.

## Meklēšanas, gestu un slāņu

Tastatūras lapā uzskaitītas noteiktās saskarnes un paraugizkārtojumi. Vienai fiziskai tastatūrai var būt vairākas saskarnes. Izvēlieties pareizo valodu un ģeometriju; produkta nosaukums vien nenosaka ISO vai ANSI izkārtojumu.

Standarta globālās saīsnes darbojas visās tastatūrās. Ierīcei specifiskās Raw Input piesaistes novēro konkrētu ierīci un skenēšanas kodu, tostarp atbalstītos funkciju un multivides taustiņus. **Raw Input nenomāc parasto Windows ievadi. Ekskluzīvi makro vajadzībām izmantota otrā tastatūra nav gatava produkcijas lietošanai.** Joprojām vajadzīga parakstīta, licencēta un pārbaudīta filtra draivera aizmugursistēma; MAKRIVO tādu neinstalē.

Atbalstītās īpašo taustiņu piesaistes var izmantot tūlītēju nospiešanu, īsu nospiešanu, turēšanu vai dubultnospiešanu. Turēšanas slāņa taustiņi uz laiku izvēlas citu piesaistu kopu un pēc atlaišanas atjauno pamatslāni; slāņa izvēles taustiņi nepalaid savus makro. Turēšanas slieksnis ir no 150 līdz 5000 ms; dubultnospiešanas intervāls ir no 150 līdz 1000 ms. Šīs piesaistes atšķiras no parastajām globālajām saīsnēm.

Īpašo taustiņu vednis noteikšanas laikā atrod saderīgos taustiņus, neizpildot tiem piešķirtās darbības. Diagnostika izskaidro nepieejamas ierīces, apturētas piesaistes, profilu neatbilstības un konfliktus. Pārklājums rāda izvēlēto izkārtojumu un piesaistes; tā noklusējuma turēšanas saīsne ir `Ctrl+Alt+F1`.

| Integrācija | Prasības un ierobežojumi |
| --- | --- |
| Corsair | Darbojošs iCUE, SDK piekļuve un saderīgs modelis. G1–G6 un atbalstītās RGB funkcijas; īpašie Fn, ritenīša, bloķēšanas un profila vadības elementi parasti nav pieejami G-key API. |
| Logitech G | Oriģinālajām vietējām x64 G-key/LED SDK bibliotēkām vajadzīgs skaidrs apstiprinājums. G-key notikumi unikāli neatšķir vairākas identiskas fiziskas tastatūras. |
| Razer | Chroma RGB un standarta ievade; vajadzības gadījumā piesaistiet īpašos vadības elementus Synapse. |
| SteelSeries | GameSense RGB un standarta ievade; vajadzības gadījumā piesaistiet īpašos vadības elementus GG. |
| Wooting | Saderīgs oriģināls vietējais x64 SDK analogajām vērtībām un sliekšņiem; parastā HID ievade paliek aktīva. |
| Citi tastatūras | Standarta saīsnes vai atbalstītās fizisko skenēšanas kodu piesaistes. Fn un īpašie vadības elementi ir atkarīgi no modeļa un ražotāja konfigurācijas. |

Ražotāju pētījumi apraksta dokumentētās iespējas, nevis īstenotu vai pārbaudītu saderību ar katru uzskaitīto ierīci. Jaunajiem adapteriem joprojām vajadzīgi reālas aparatūras pieņemšanas testi. Nelejupielādējiet SDK bināros failus no neuzticamiem avotiem; SDK izplatīšanas tiesības ir atsevišķas no tehniskās saderības.

## Audio, displeji un OBS

Audio soļi iestata skaļumu no 0 līdz 100 procentiem un izslēdz/ieslēdz/pārslēdz skaņu atbalstītajiem izvades vai mikrofona galapunktiem. Izvēlieties konkrētu galapunktu vai Windows noklusējumu. Ja saglabātā mērķa nav, rodas kļūda, nevis tiek klusi vadīta cita ierīce. Pēc atkārtotas pievienošanas izmantojiet Refresh Devices. Galapunktu vadība nenozīmē neatkarīgu katras lietotnes miksera atbalstu.

Displeja darbības palielina/samazina spilgtumu par 10 procentpunktiem vai iestata vērtību no 0 līdz 100. Iekšējie displeji izmanto atbalstītās WMI saskarnes; ārējiem monitoriem vajadzīgs DDC/CI atbalsts un parasti tā ieslēgšana monitora izvēlnē. Dokstacijas, adapteri un kabeļi var bloķēt vadību. Izvēlieties visus atbalstītos displejus vai vienu monitoru. Atkārtota pievienošana var mainīt pieejamos identifikatorus; pārskatiet saglabāto mērķi. Trūkstoši rādījumi tiek parādīti kā nepieejami, nevis kā izmērītas vērtības.

OBS vajadzīgs WebSocket v5, kas iespējots OBS Tools sadaļā. Konfigurējiet galapunktu, pārbaudiet savienojumu un pirms saglabāšanas ielādējiet ainu/ievažu nosaukumus. Atbalstīta ainu pārslēgšana, straumēšanas/ierakstīšanas sākšana/apturēšana/pārslēgšana, ierakstīšanas pauze/atsākšana un ievades skaņas izslēgšana. Paroles var glabāt Windows Credential Manager, un tās netiek iekļautas konfigurācijas eksportos. Veiksmīga OBS komanda nepierāda visu turpmāko straumēšanas efektu izpildi.

## Rezultātu pārbaude un izpildes vēsture

Piemērotajiem soļiem iespējojiet Verify Result. Lietotne atšķir sākotnējās pārbaudes, apstiprinātus efektus, kļūmes, atcelšanu un nepārbaudāmus efektus. Procesa esamība nenozīmē lietotnes gatavību. Relatīvām izmaiņām, saīsnēm un teksta ievadei nevar vispārīgi apgalvot panākumus. Ja darbība neizdodas, pārbaudiet darbību žurnālu un pārbaudes vēsturi. Pirms visa makro atkārtotas palaišanas izlabojiet konfigurācijas kļūdas.

## UI automātika un klikšķi ieraksts

Pievienojiet UI Automation soli, izvēlieties operāciju un pēc parādītās aizkaves fiksējiet mērķi zem peles rādītāja. Fiksēšana pati par sevi neko neizpilda. Pārbaudiet selektoru un pieprasiet unikālu atbilstību. Atbalstītās operācijas ir `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` un `WaitState`. Paroļu lauki, atspējotas vadīklas un neviennozīmīgi selektori tiek noraidīti.

Saskarnes automatizācija darbojas ierobežotā palīgprocesā. Tā ir atkarīga no mērķa lietotnes Windows pieejamības atbalsta un atļaujām. Klikšķu ierakstītājs fiksē atbalstītās darbības un pauzes, nevis vispārēju teksta ievadi. Pirms pieņemšanas pārskatiet un validējiet melnrakstu. Ārējās lietotnes saskarnes izmaiņas var padarīt selektorus nederīgus. Procesa izolācija ierobežo avāriju ietekmi un atbalsta atcelšanu, taču nav drošības smilškaste.

## Plugins

Atveriet Extensions, lai pārbaudītu pakotni pirms importēšanas. Tiek pārbaudīta pakotnes struktūra, manifests un jaucējvērtības. Importētais paplašinājums sākotnēji ir atspējots. Izlasiet deklarētās darbības un pirms iespējošanas skaidri apstipriniet uzticēšanos izdevējam. Spraudņi darbojas atsevišķā procesā ar jūsu lietotāja atļaujām; šis process **nav smilškaste**. Instalējiet tikai pakotnes, kurām uzticaties.

Pārvaldnieks atbalsta eksportēšanu, atspējošanu, atinstalēšanu un iepriekšējās versijas atjaunošanu/atmešanu. Trūkstošas atkarības bloķē izsaukšanu. Parauga pakotne nodrošina atpakaļskaitīšanas un vietējā stāvokļa darbības. Atbalstītā shēma ir dokumentēta JSON-Schema apakškopa, nevis visas iespējamās JSON-Schema funkcijas. Publiska spraudņu tirgus nav.

## Mobilais panelis

1. Atveriet Mobile Panel, izvēlieties vietējo IP un skaidri norādiet kopīgojamos profilus. Noklusējuma loopback adrese ir sasniedzama tikai pašā datorā; tālrunim vai planšetdatoram izvēlieties atbilstošu LAN adresi.
2. Palaidiet paneli un nolasiet tā HTTPS adresi, sertifikāta pirkstu nospiedumu un vienreizējo sešciparu pārošanas kodu.
3. Atveriet adresi ierīcē tajā pašā uzticamajā tīklā. Pirms uzticaties vietējam sertifikātam, salīdziniet SHA-256 pirkstu nospiedumu pa uzticamu kanālu. Lietotne automātiski neinstalē uzticamu sertifikātu.
4. Savienojiet ierīci pārī ar parādīto kodu un pārbaudiet vienu nekaitīgu elementu. LAN piekļuvei var būt vajadzīga manuāla Windows Firewall atļauja. Neatveriet portu internetam.
5. Kad esat pabeidzis, apturiet paneli vai atsauciet pārī savienoto klientu atļaujas lietotnē.

Servera privātās atslēgas aizsargā Windows DPAPI; klientu akreditācijas dati tiek glabāti kā jaucējvērtības. IP maiņa vai sertifikāta atjaunošana var prasīt atkārtotu uzticēšanās apstiprināšanu. Panelis rāda darbību un pārbaudes stāvokļus, nevis visus iespējamos reāllaika sensorus. Pārlūka izkārtojuma un vietējā HTTPS pārbaudes neaizstāj reālu tālruņu/planšetdatoru LAN pieņemšanas testus.

## Mājas apmācības asistents

Šis izvēles eksperimentālais palīgs pēc noklusējuma ir atspējots. Atveriet to izvēlnē Tools, skaidri iespējojiet, izvēlieties izpildvidi/modeli un pievienojiet tikai resursus, kurus vēlaties ņemt vērā. Norādiet mērķi, izveidojiet melnrakstu, pārbaudiet neatrisinātos pieprasījumus un pārsūtiet rezultātu parastajam redaktoram. Nekas netiek izpildīts automātiski; saglabāšana un palaišana ir skaidri veicamas darbības. Automātiska pāreja uz citu izpildvidi nav paredzēta.

| Runāšana | Iestatīšana |
| --- | --- |
| Ollama | Atsevišķs vietējais pakalpojums un vietēji uzstādīts modelis `127.0.0.1:11434`. Atklāti attālināti / mākoņi ir noraidīti. |
| LM Studio | Vietējais serveris ar vietēji uzlādētu modeli un JSON shēmas atbalstu `127.0.0.1:1234`. Neļauj attālināt vietējo apstrādi. |
| llama.cpp | Vietējais `llama-server` un saderīgs modelis ar JSON shēmas atbalstu `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Uzticams saderīgs vietējais GenAI teksta modeļa mape; izvēlieties `genai_config.json`. Inference izmanto apvienoto partneru procesu bez HTTP. |

HTTP klienti izmanto tikai loopback savienojumus, neizmanto starpniekserverus un neseko pārvirzīšanām. MAKRIVO nevar droši noteikt, vai saderīgs vietējais serveris iekšēji izsauc attālinātu pakalpojumu. Autentificētus vietējos serverus šajā versijā nevar konfigurēt. Modeļu svari, Python un vietējie inferenču serveri netiek automātiski lejupielādēti vai instalēti.

ONNX pakotnēm vajadzīgs `tokenizer.json` un derīga tērzēšanas veidne. Failā `tokenizer_config.json` atbalstīta virkne `chat_template`, vārdnīca ar virkni `default` vai viennozīmīgs nosauktu/noklusējuma veidņu saraksts. Ja veidnes nav vai tā ir null, kā rezerves variants tiek izmantots `chat_template.jinja`. Tukšas, neviennozīmīgas, pārāk lielas vai nepareiza tipa veidnes tiek noraidītas. Patvaļīgi ONNX faili, iegulšanas/redzes modeļi un tikai GPU pakotnes nevar aizstāt saderīgus GenAI teksta modeļus. Iekļautās GenAI izpildvides versija ir 0.17.1, CPU. Izmantojiet tikai uzticamus modeļus; procesu nodalīšana nav smilškaste.

Palīgs atbalsta izvēlētās programmas bez argumentiem, izvēlētos dokumentus un vietnes, kā arī mikrofona skaņas izslēgšanu pēc skaidra pieprasījuma. Tas neveido ziņojumus, tikšanās, patvaļīgus skriptus, brīvus klikšķus, lejupielādes vai austiņu pārslēgšanu. Mērķis un resursu nosaukumi tiek nosūtīti izvēlētajai vietējai izpildvidei; resursu ceļi un failu saturs netiek atsevišķi nodoti. Lietotāja ievadītais mērķis pats var saturēt personas datus. Vietējās izpildvides tīkla darbība paliek lietotāja atbildība.

ONNX pieprasījuma laika limits ir 90 sekundes, ieskaitot modeļa ielādi; atļauti līdz 3072 uzvednes tokeniem un 1024 ģenerētiem tokeniem, ko ierobežo arī modeļa konteksts. Nepareizs/saīsināts JSON un nezināmi resursu ID tiek noraidīti. Atceliet pieprasījumu vai aizveriet logu, lai to pārtrauktu. Izpildvides tests ar nejaušu modeli pierāda izpildi, nevis noderīgu plānošanas kvalitāti vai laika ietaupījumu. Vietējie eksperimentu žurnāli satur notikumu identifikatorus un laika mērījumus, nevis mērķa tekstu vai dokumentu saturu; tie netiek automātiski augšupielādēti.

Vēlākajā mazā Instruct modeļa novērtējumā četros vācu/angļu izmēģinājumos netika iegūts neviens derīgs plāns. Vietējā inference darbojās, taču modelis neizpildīja stingro plānošanas līgumu. Tas ir negatīvs kvalitātes rezultāts, nevis modeļa ieteikums vai pierādījums palīga lietderībai. Precīzu modeļa revīziju un ierobežojumus skatiet datētajā vietējās izpildvides testa pārskatā.

## Imports, eksports, sinhronizācija un backup

Pirms konfigurācijas aizstāšanas pārskatiet importēšanas priekšskatījumu. Importētās darbības sākotnēji ir atspējotas, un pārnesamā sinhronizācija noņem aparatūrai specifiskās piesaistes. Pēc pārbaudes atkārtoti piešķiriet ierīces un apzināti iespējojiet darbības. Pirms eksporta kopīgošanas pārskatiet teksta/darbību saturu; paroļu izslēgšana nenozīmē, ka viss lietotāja veidoto darbību saturs ir nesensitīvs.

Profilu sinhronizācija apmainās ar failiem izvēlētā mapē, ko pēc izvēles pārvalda esošais mākoņklients. MAKRIVO nenodrošina mākoņkontu un neglabā mākoņa akreditācijas datus. Revīzijas novērš klusu pārrakstīšanu. Darbību satura iekļaušana ir skaidra izvēle. Pirms aizstājoša importa dublējiet konfigurāciju. Noklusējuma konfigurācija ir `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; vēsturiskais mapes nosaukums saglabāts saderībai. Pirms manuālas kopēšanas/atjaunošanas aizveriet lietotni. Saglabātie SDK apstiprinājumi un OS aizsargātie akreditācijas dati citā datorā var būt jāizveido no jauna.

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

Noderīgā kļūdas ziņojumā norādiet Windows versiju, lietotnes būvējumu/komitu, valodu, displeja mērogojumu, aparatūras modeli, minimālo darbību secību un attiecīgo rediģēto žurnālu. Pirms publicēšanas noņemiet personiskos ceļus, darbību saturu un akreditācijas datus.

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

Windows integrācijas testi var atvērt izolētus logus un īslaicīgi konfigurēt/atbrīvot atbalstītās ražotāju piesaistes. Palaidiet tos, kad cits kontrolleris nepārvalda šīs piesaistes. Noklusējuma spilgtuma/audio pārbaudes ir tikai lasāmas; skaidri pieprasīti aparatūras stāvokļa maiņas un atjaunošanas testi ir atsevišķi. Imitētus protokolus, renderētus ekrānattēlus un nejaušus inferenču grafus nedrīkst pasniegt kā reālas aparatūras/modeļa kvalitātes pieņemšanu.

Pilnais publicēšanas skripts apkopo palīgprocesu resursdatorus, mobilos resursus, parauga spraudni, ONNX izpildvidi un pieejamos atkarību paziņojumus. Saglabājiet apakšmapes. Veiksmīgs būvējums nav parakstīts instalētājs vai juridisks laidiena apstiprinājums. Pirmkods un būvējumi var attīstīties atsevišķi; salīdziniet dokumentācijas stāvokli un komitu ar instalēto versiju.

## Licencēšana, privātums un atbrīvošanas statuss

Paredzētais komerciālais modelis ir vienreizējs pirkums ar iegādātās galvenās versijas beztermiņa lietošanu; galīgā cena, atļauto ierīču skaits un jaunināšanas noteikumi jānosaka faktiskajos pārdošanas noteikumos. Šī rokasgrāmata neapliecina produkcijas pirkšanas/aktivizācijas sistēmas esamību. Ikdienas vadībai mākoņkonts nav vajadzīgs.

Neizplatiet īpašnieka tiesībām aizsargātus SDK bināros failus bez pierādītas atļaujas. Saglabājiet licences un trešo pušu paziņojumus par katru piegādāto atkarību, tostarp .NET un ONNX pakotnēm. Modeļu svariem ir savi noteikumi. Datētais licenču audits veikts pirms dažu jaunu atkarību pievienošanas un nav šo papildinājumu apstiprinājums. Publiska repozitorija pati par sevi nepiešķir vispārēju atvērtā pirmkoda licenci.

Reālā ražotāju aparatūra, tālruņu/planšetdatoru LAN darbība, pārejas starp dažādiem monitoriem/DPI, ilgstoša lietošana, pilnīga lingvistiskā pārbaude, ekskluzīva draivera atbalsts un noderīga AI plānošanas kvalitāte joprojām prasa atsevišķu pieņemšanu. Nekad neizseciniet apstiprinājumu tikai no funkcijas esamības.

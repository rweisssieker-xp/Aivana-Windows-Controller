<!-- generated-doc locale=lv-LV source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: seši produktu paplašinājumi un tastatūras adapteri

datums: 2026-10-12
Statuss: ierosināts dizains lietotāju pārskatīšanai; īstenošana nav uzsākta.

## Mērķis
Sniedz visus sešus pieprasītos paplašinājumus esošajā Windows/WPF pieteikumā, saglabājot esošos profilus, G1/G2 uzdevumus, vietējo darbību un bezmaksas lietošanu. Paplašiniet tastatūras integrāciju līdz Razer un Logitech, ar atkārtoti izmantojamu jaudas arhitektūru citiem ražotājiem. Ražotāja nosaukums vai tastatūras izkārtojums nekad nav pierādījums, ka tiek atbalstītas īpašumtiesības.

## Arhitektūras lēmums
Paplašiniet Aivana.Core ar serializējamiem līgumiem un validāciju, Aivana.Windows ar Windows un piegādātājs adapteru, un Aivana.Desktop ar darījumu redaktoriem un statusa redzējumiem. Izmantojiet atsevišķus partneru projektus izolētam plug-in izpildei un vietējai mobilo paneli pakalpojumam. Izvairieties no pārrakstīšanas vai privileģētā procesa katra paplašināšanās. Pastāvīga konfigurācija ir jāuzlādē nemainīgi; pievienojiet darbības veidu, nevis pārrakstīt savu pastāvīgo enum vērtību.

Alternatīvas, kas tiek apsvērtas: monolītiska īstenošana iekļūtu aparatūras, tīkla un darba plūsmas izpildei; aizstājot WPF pievienotu neatbilstošu migrācijas darbu. Modulārie paplašinājumi ir ieteicami.

## 1. Neatkarīgs makro pad
Uzglabāt savienojumus ar ierīces identitāti, kā arī fizisko skenēšanas kodu un paplašinātās atslēgas. Pievienojiet izteiktus novērošanas un ekskluzīvos režīmus, ierīces atklāšanu, atslēgvārdu, atkārtoti apspiest, gestus, turēt slāņus un atkal savienot diagnostikas. Saglabājiet galvenās/numpad Enter atšķirības un pārbaudiet Vācijas/US izkārtojumus. Saistības mērķis ir izvēlēts fiziskais tastatūras; nevienmērīga atkopšana identitātes prasa pārvaldību, nevis kontrolēt citu ierīci.

Raw Input nodrošina tikai novērošanu. ekskluzīva uz ierīci izslēgšana prasa reālu tastatūras filtra aizmuguri; nekad saistīt globālo zemas līmeņa sloksnes ar Raw Input un prasīt uzticamu ierīces specifisko izslēgšanu. nodrošināt uzraudzības, ārkārtas atbrīvojumu, fail-open uzvedību un instalatoru/uninstalleru. neuzņemt drošu desktop ievietošanu..

Driver ports: izvēlieties atbalstītu, pienācīgi licencētu un parakstītu backend, kas ir saderīgs ar mērķa Windows 11 konfigurācijām, vai izstrādājiet / paraksta īpašu filtru. Intercepcija ir kandidāts, lai novērtētu, nevis apņemto ražošanas atkarību: tās oficiālais README apraksta testēšanu caur Windows 10 un atšķirīgu komerclicenci. Kamēr šis vārts ir sasniegts, ekskluzīvais režīms ir jāparāda kā nepieejams; funkcija nevar tikt deklarēta kā pilnīga.

Pieņemšana: ar divām fiziskām tastatūrām, tas pats atslēga izdod makro un nerada normālu pieteikumu tikai izvēlētajā ekskluzīvajā ierīcē; citi tastatūras ievietojumi paliek neierobežoti. Pārbaudiet izslēgšanu, crash, atcelšanu, suspend/resume un vadītāju noņemšanu uz faktisko aparatūru.

## 2. Darba telpas izveide
Pievienojiet burvību darba vietai, sanāksmēm, strīdiem, pētījumiem un projektu pārejai. Atrodiet instalētus lietojumus, audio ierīces, monitorus un OBS scenārijus. Lietotāji izvēlas mērķus un saistības; iepriekš skatīt rezultātus un apstiprināt trūkstošos prasības. Tikai pēc pārskatīšanas, bez darbības, kas darbojas uzstādīšanas laikā. Iegūtie profili joprojām ir rediģējami esošajos redaktoros.

Pieņemšana: radīt darba profilu, neradot manuāli izpildāmus ceļus vai ierīces ID; atcelšanas lapas iestatījumus nemainās; trūkstošajā programmatūrā ir izpildāmā izskaidrošana.

## 3. Pārbaudīta izpilde
Ievadiet priekšlaicīgas pārbaudes, tipētus pa solim rezultātus un opcionālus pēc nosacījumus. Atšķir pieprasītos, pabeigtos, pārbaudītos, neveiksmīgos, izbeigtos, anulētos un neatbilstīgos apstākļus. Pārbaudiet audio/mikrofonu, spilgtumu, procesu/vēstura gatavību un OBS stāvokli ar ierobežotu asinkronu lasīšanu. Īsuma/teksta piegāde nav pierādījums tam, ka mērķa pieteikums ir pieņēmis pasūtījumu. Ārējie blakusparādības nav universāli atgriezeniskas; piedāvā kompensāciju tikai tad, ja tas tiek īstenots.

Pieņemšana: izslēgtie skaidri mērķi nekad klusumā neatgriežas; nepareizi novērots stāvoklis nekad ziņo par pārbaudītu panākumu; atcelšana pārtrauc meklēšanu; neveiksmes identificē soli un atgūšanas darbību. Izpildes vēsture izslēdz teksta maksājumus un noslēpumus.

## 4. Plugins un kopīgie paketi
Versija JSON izpaužas, deklarējot darbības, parametrus shēmas, valsts pakalpojumu sniedzēji, atkarības un iespējas. Run executable plugins atsevišķā procesā pār ierobežotu vietējo IPC ar manuālo, termiņu un atcelšanu. Atteikties no kļūdainiem ziņojumiem un nesaderām versijām; pārvaldīt negadījumus bez kontrolera pārtraukšanas. Iespējas deklarācijas ir atklāšana un mediācija, nevis OS sandboxing prasība.

Nodrošināt iepakojuma eksporta/importa pārskatu, satura hashes, ceļa-traversālo aizsardzību, izmēra robežas, versijas/atkarības pārbaudes, skaidru atļauju un atjauninājumu. Importētie izpildāmie plugins nedrīkst darboties automātiski. Ievadiet darba piemērs plugins ar end-to-end instalāciju / darbību / valsts / deinstalācijas plūsmu. Šai izdošanai nav nepieciešama publiskā tirgus pakalpojuma.

Pieņemšana: izveidot, eksportēt, uzstādīt un izpildīt reālu integrāciju; demonstrēt laiku, katastrofas izolāciju, atkarības neveiksmi, pārrobežu noraidīšanu un iebrukumu.

## 5 Windows UI automātiski
Pievienojiet elementu pikeri un ierobežotas darbības, lai atsauktu, izvēlētos, pievērstu uzmanību un gaidītu elementu/statu. Iespējams, ka izvēlētāji izmanto procesu, automātisko ID, kontroles veidu un priekšnieku kontekstu; izvairieties no koordinācijas tikai ierakstīšanas. Izrādīt selektoru spēļu skaitīšanu un test selektorus lasīt vienīgi. Paroles lauki ir izslēgti no ieraksta. Pārvaldīt nepieņemtos kontroles un Windows integritātes līmeņa ierobežojumus.

Paplašiniet ierakstu ar pārskatāmām UIA darbībām, saglabājot pašreizējo īsziņas ierakstu. Abstrakts selektoru neveiksmes vietā klikšķināt uz savvaļas kontroles. Normāls pārlūkprogrammas pieejamības koks nav pilna DOM automātiskā motora solis.

Pieņemšana: automātiski izmantojiet īpašu testa pieteikumu ar stabilām izvēlētājiem pozīcijas / izmēra izmaiņām; testēšanas neskaidrības, trūkst elementu, traucēta kontroles, laika iznākšanas un atcelšanas.

## 6. Vietējais tālrunis / tabletes
Pakalpojums ir reaģējošs panelis no opt-in LAN pakalpojuma. Izskaidrojiet, izmantojot īslaicīgu vienlaicīgu kodu un izdodot atsaucamas apliecības, kas aizsargātas vietēji. Izmantojiet HTTPS ar dokumentētu vietējo sertifikātu uzticamību/pinning; nekavējoties nosūtot apliecinājumus prasībā. Default disabled un ierobežot konfigurētus saskarnes. Sākuma validācija, autentificētas komandas, likmes ierobežojumi un ierobežoti ziņojumi ir obligāti.

Ieteicamā profila / darbību, dzīvo stāvokli, progresu un atcelšanu; izmantojiet stabilus ID un to pašu izpildes slogu kā desktop un fiziskās atslēgas. Neizpaustiet savvaļas komandas, izpildāmās maršrutus, noslēpumus vai plugin instalāciju. Nodrošiniet savienojumu, pāriet un atsauc ekrānus. Nav iekļauts mākoņa konts vai interneta relay.

Pieņemšana: darbojas no reāla telefona/tableta uz tā paša LAN; neuzticami klienti nevar uzskaitīt vai veikt darbības; atceltie tokenus neveic; duplikāti / atkārtoti komandas neveic dubultā; diskonekcija klusumā neatgriežas darbībās.

## Keyboard ražotājs

| Adaptācija / ģimene | Plānotā integrācija | Izskaidrojiet robežas |
| --- | --- | --- |
| Corsair | Saglabājiet iCUE G atslēgas ievades un RGB integrāciju | Modela/programmatūras spēju atklāšana joprojām ir nepieciešama |
| Logitech G | G-slēgvārds SDK adapteris ar preses/izlaišanas un ekspluatācijas laika pieejamības pārbaudi; apgaismojums caur atsevišķu SDK, kur atbalsta | Nelietojiet G HUB atklāšanu ar atbalstošu G-slēgvārdu API katrā modelī |
| Razer | Chroma apgaismojuma adapteris un vadīts Synapse standarta atslēga kartēšana, kā arī ģenētiskais HID ievads | Chroma ir apgaismojuma API; neatzīmējiet to īpašā makro atslēgvārdu |
| SteelSeries | GameSense stāvokļa / apgaismojuma adapteris plus ģenētiskais HID ievietojums | GameSense notikumu iesniegšana nav vispārējs īpašumtiesību atslēga |
| Wooting | Analogs SDK adapteris ar atbalstu analogā ievadīšanai, kā arī normālas HID atslēgas | Kapacitātes un SDK izplatīšanas nosacījumi ir jāpārbauda pēc atbalstītā modeļa |
| Keychron/QMK/VIA | Standarta HID atslēgas un firmware kartēšanas instrukcijas; zināmas izkārtojumi tikai ar pierādījumiem | VIA konfigurācijas atbalsts nav universāls runtime makro atslēga API |
| Citi standarti HID klaviatūras | Fiziskā atslēga atklāšana un mape; ekskluzīvs režīms tikai ar validētu filtru | Nav ražošanas modeļa specifiskā Fn, dial vai īpašumtiesību atslēga atbalsts |

Katrs ierīce tiek atklāta, novērota, kartēta ar piegādātāja programmatūru, oriģinālu SDK, vadītāju ekskluzīvu, nepieejamu vai aparatūras nepārbaudītu statusu. Saglabājiet spējas pierādījumus ar adaptera versijām un pārbaudītajiem modeļiem / firmware identifikatoriem.

## piegādes rīkojums
1. Konfigurācijas saderība, spēju līgumi un pārbaudīta izpilde.
2. Vadīts iestatījums, izmantojot reālu atklāšanu un esošos rīcības izpildītājus.
3. UI automācija un tā ieraksta/pārbaudes fiksācija.
4. Plugin hosts, paketes plūsma un piemērs integrācija.
5. Lāčplēša un mobilo pārbaude.
6. Paplašināts piegādātāja adapteris un ekskluzīva tastatūras backend integrācija, ar faktisko aparatūras validāciju.

Visi seši joprojām ir nepieciešami. Autovadītāja paraksts/licencēšana, nepieejami SDK vai trūkst testa aparatūra ir atbrīvošanas vārti, nevis iemesli, lai aizstātu mock uzvedību un saukt darbu par pilnīgu.

## Pārbaudes un atbrīvošanas kritēriji
Izveido esošo kodolu, Windows un WPF pārbauda un pievieno koncentrētu integrācijas testu katram jaunam robežam. Pārbaudiet JSON braucienu un mantojuma profili; pārbaudiet importēto izpildāmās satura, tīkla autentifikāciju, atcelšanu, plugins trauksmes un ierīces atkopšanu. Pārbaudiet desktop un mobilo ierīci. Augsts Windows skalēšana atsevišķi. Izveidojiet atbrīvošanas materiālus tikai pēc pārbaudes; atšķiras ievietoto/pārbaudīto, ievietotu/hardware-pārbaudīto un ārēji bloķēto funkcionalitāti atbrīvošanās piezīmēs. Neaizstādiet instalēto pieteikumu, instalējiet vadītājus, mainīsiet reālus tastatūras mapēšanas vai atvērtā tīkla piekļuvi kā nejaušu testu.

## pierādījumi
- Microsoft Raw Input semanti: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G izstrādātāja SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Intercepcijas reģistra un atkarības nosacījumi: https://github.com/oblitum/Interception
- Pastāvošās depozīta ražotāja inventārs: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Pārskata lēmumi
Lietotājs tiek lūgts apstiprināt šo rakstisku dizainu pirms īstenošanas plāna. Galvenā ārējā atkarība ir ražošanas gatavs ekskluzīvs ievešanas vadītājam. Ieteicamā apjoma saglabā šo kā caurumu, nevis klusumā samazina to uz Raw Input novērojumiem.

<!-- generated-doc locale=lt-LT source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../README.md)

# MAKRIVO

[Visi dokumentai visomis 27 kalbomis](README.md) · [Visiškas vartotojo rankraštis](HANDBOOK.md)

**pagal Aivana.** [Dabartinis inventorius ir išleidimo apribojimai](FEATURES.md) · [Išplėstinis testas](testing/2026-10-12-six-extensions.md) · [Vietinis prototypų asistentas](testing/2026-10-12-local-meeting-assistant.md)

**Anglų kalba (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Vietinis Windows karštoji klaviatūra su Stream Deck stiliaus plokštelėmis, profiliomis, makromis ir klaviatūros aptikimu. Tarpinis ryšys palaiko 27 kalbą. Nėra sąskaitos ar debesijos ryšys reikalingas kasdieniniam naudojimui.

[Dokumentas](README.md) · [Tikrinimas](VERIFICATION.md) · [Funkcijos bandymų ataskaita](testing/2026-10-09-usp-release.md)

## Pradėti

Po to, kai paskelbta, paraiška yra `artifacts/win-x64/MAKRIVO.exe`. Išsaugokite visą išleidimo aplanką kartu; galite jį kopijuoti kitur. Windows 11 x64 yra pradinis tikslas. Build artefaktai yra išskiriami iš Git; šaltinio patikrinimas neapima iš anksto pastatytos.

Įdiegti .NET 10 SDK, nurodytą `global.json` kurti ir paleisti iš šaltinio:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Išleiskite savarankišką statybą, nereikalaujant atskirai įdiegto .NET darbo laiko:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Pagrindinis darbo srautas

1. Pasirinkite profilį ir sukurkite veiksmų plokštelę.
2. Įveskite savo vardą, ikoną, spalvą ir klaviatūrą. Ctrl/Alt/Shift deriniai sumažina atsitiktinę aktyvaciją spausdinimo metu.
3. Pridėti vieną ar daugiau žingsnių: programos, failai, svetainės, tekstas, trumpai, žiniasklaida, tūris, profilių keitimas, arba vėlavimai.
4. Išsaugoti . Pakeitimai lieka projekto, kol išsaugota; ištrinta atkuria išsaugotą versiją. Žymės mygtukas ir jo karštoji raktas naudoja tą patį vykdymo kelią.
5. Grąžinkite plokšteles ir makro žingsnius su traukimo ir nuotėkio mygtukais. Šablonai supaprastina nustatymą. Makros gali pakartoti iki 100 kartų. Atšaukti per sąsają, sistemos pjovimo meniu arba Ctrl+Alt+Escape pagal numatytuosius.

Taikymo specifiniai profiliai naudoja proceso pavadinimą be jo pilno kelio. Profilio užsakymas nutraukia ryšius; numatytas profilis yra atgal. Automatinis profilio pasirinkimas turi būti įjungtas Settings.

Pagrindinio lango uždarymo metu programa laikoma sistemoje pagal numatytuosius duomenis. Dvigubai paspauskite jo tray ikoną, kad ją vėl atidarytumėte; naudokite tray meniu. Startup su Windows yra pasirinktas ir išankstinis. Hotkeys gali būti sustabdytos centralizuotai.

## Veiksmai ir komandos paletė

Pridėti veiksmus atveria paieškos katalogą su 90 preceitėmis aštuoniose kategorijose. Kiekvienas turi aprašymą ir išvadas. Makro redaktorius naudoja tą patį katalogą, kad pridėtų ar pakeistų žingsnius. Pasirinkus išankstinį nustatymą šiame redaktoriaus darbo sraute sukuriamas projektas be vykdymo.

Įsakymo paletė atidaroma iš antraštės arba su Ctrl+K programoje. Jis ieško išsaugotų veiksmų per profilius, katalogus, profilio keitimąsi, navigaciją, pertrauką / atnaujinimą, makro atšaukimą ir importą / eksportą. Filtruokite sutaupytus veiksmus, katalogų funkcijas ar programos komandas. Rinkitės raktus, įveskite, Ctrl+Enter redaguoja redaguojamą įrašą, o „Escape“ uždaroma. Pirmiausia pasirodo mėgstamiausi. Pasiruošę naudoti katalogas gali būti paleistas nedelsiant; išankstiniai nustatymai reikalauja atidaryti projektą. Nustatymai gali apibrėžti pasaulinę paletę. Naujos konfigūracijos naudoja Ctrl+Shift+Space; esamos konfigūracijas automatiškai negauna naujo pasaulio sutrumpinimo.

Garso veiksmai nustato tūrio tiksliai nuo 0 iki 100 procentų ir mute, nesumažintas arba išleidimo ir mikrofonų prietaisų. Pasirinkite tam tikrą galutinį tašką arba palikite jį nenustatytą Windows. Neįmanoma išsaugoti galutinį tašką sukelia klaidą, o ne kontroliuoti kitą įrenginį. Atnaujinti įrenginiai atsisiunčia prijungtus įrenginius.

OBS Studio naudoja WebSocket v5: start/stop/toggle/pause/resume įrašymas; start/stop /toggle srautas; keisti scenas; toggle įvesties mute. Įjungti WebSocket serverį pagal įrankius OBS ir nustatyti jo galutinį tašką redaktoriuje. Įvadas ir įkrovimo vardai skaityti OBS versiją, scenos ir įrašus be pradėti įrašymo ar srauto. Pasirinkite vardus iš sąrašo arba įveskite juos rankiniu būdu. Slapukai gali būti išsaugoti Windows Įtikinimo vadybininkas ir yra išskiriami iš eksportuotų konfigūracijų. Neįmanoma OBS ryšiai praneša apie klaidą.

Profiliai gali atitikti proceso pavadinimą ir lango pavadinimo dalį. Abi sąlygos turi atitikti. Lock Profile neleidžia automatiškai pereiti prie sesijos. Atsisiųsti sąlygas iš Foreground naudoja paskutinį išorinį programą.

Teksto ir klipų parama `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, ir `{{user}}`. Naudojimo datas `yyyy-MM-dd`; Naudojimo laikas `HH:mm`. Nepažįstami vietovės gyventojai lieka nepasikeitimai. `{{profile}}` Atskleidžia pradėtą veiksmų profilį; `{{process}}` nustato išorinį taikymo kontekstą. klipas yra perskaitytas tik tada, kai naudojamas tas klipas. klipas veiksmas rašo tekstą, bet ne automatiškai.

langų veiksmai sumažinti, maksimaliai, atkurti arba išdėstyti langą kairėje / dešinėje savo monitoriaus darbo srityje. Sistemos veikimo užraktai Windows. Tokie veiksmai vyksta tik tada, kai aiškiai raginami.

Preview rodo žingsnius, pasikartojimus ir kintamuosius be vykdymo. Undo ir Redo išsaugo iki 100 dizaino būsenas; teksto dėžutės išsaugo gimtąjį Ctrl + Z redagavimą. Pažymėkite mėgstamus redaktoriuje arba plokštelės meniu ir filtruoti juos ant plokštės. Įvykdymo skaičiavimai taikomi tik dabartinei sesijai.

Teksto, trumpalaikio ir lango veiksmai, paleisti iš sąsajos, aktyvuoja paskutinį išorinį tikslinį langą. Šiame straipsnyje pateikiamas tikslinis kontekstas. Jei tikslas uždarytas arba Windows atsisako aktyvinti, vykdymas sustoja su pranešimu.

## Pagalba ir grįžtamoji informacija

Special-Key Wizard yra ant klaviatūros puslapio ir paletėje. Jis nurodo G raktus, praneštus pagal iCUE. Detektyvus raktas spaudai laikinai užfiksuoja juos be jų paskirtų veiksmų. Normalios jungtys grįžta po aptikimo, atšaukimo ar uždarymo. Katalogo funkcija ir pasirinktas raktas atidarytas kaip nesuslėptas projektas; patikrinami dvigubai susieti.

Keyboard Overlay rodo pasirinktą nuorodą ir aktyvius ryšius. Laikykite Ctrl+Alt+F1 arba pašalinkite jį iš paletės ar klaviatūros puslapio. Jis neatsiranda dėmesio ir leidžia pelės spustelėjimus. Profiliai ir privalomi pokyčiai atnaujinami, o matomi; pabrėžiami įprasti Windows raktiniai spaudai. Gamintojo specifiniai kontrolės įrenginiai negali pateikti ekvivalentinio Windows raktinio signalo.

Atsiliepimai apie vykdymą rodo laukti, žingsnis / pakartotinis progresas, užbaigimas, atšaukimas ar nesėkmė be dėmesio ar pateikiant įvestą tekstą. Įsakymai vykdomi patvirtina įsakymų vykdymą, o ne nepriklausomai prašomas gyvenimo būdas trečiosios šalies paraiškoje. Klaidų detalės rodomos veiklos loge.

## Įrenginiai ir Corsair

Corsair G1–G6 Galima tiesiogiai perduoti pareigūnams iCUE SDK. Pasirinkite įrenginį ir raktą veiksmų redaktoriuje ir išsaugokite arba paruoškite projektą iš K100 klaviatūros vaizdas. ryšiai taikomi pasirinktam įrenginiui ir aktyviam profiliui; pertrauka taip pat išjungia juos. iCUE turi būti veikiama ir leidžiama. fn, iCUE Roda, profilis ir užrakto kontrolės negali būti žemėlapyje per šį G raktą API.

Įrenginių puslapis reguliariai ir kai prietaisai yra prisijungę, pateikia sąrašą Windows klaviatūros sąsajų ir atnaujina. Fizinė klaviatūra gali atskleisti keletą HID sąsajų. Standartiniai pasauliniai šildytuvai dirba tarp gamintojų.

Corsair aptikimas naudoja turimą įrenginio informaciją. RGB kontrolė reikalauja suderinamos aparatūros, veikia iCUE ir oficialios iCUE SDK; programa praneša apie faktinę prieinamumą. Kiti gamintojai šiuo metu gauna aptikimą ir standartinius veiksmus. Išorėje Corsair G raktas integracija, nuosavybės kontrolės veikia, kai Windows gauna juos kaip įprastą įvedimą. Programoje nėra klaviatūros vairuotojo.

## Kalbos

Settings offers 27 languages: vokiečių, anglų, prancūzų, ispanų, italų, portugalų, olandų, čekų, slovakų, vokietų, bulgarų, graikų, danų, švedų, norvegų, suomių, estų, lotynų, lietuvių, slovėnų, ukrainiečių, rusų, turkų ir serbų latvių scenarijų. Automatiškai sekasi Windows rodomoji kalba. Vertimai yra vietiniai. Layouts, hotkeys ir vartotojo sukurti pavadinimai yra nepriklausomi nuo sąsajos kalbos.

Išsamus saugyklos dokumentas yra prieinamas visomis 27 paraiškos kalbomis per [Kalbos indeksas](README.md)Ir Vertimai yra mašinos pagalba, su originaliomis nuorodomis ir struktūriniais patikrinimais; gimtojo kalbos apžvalga yra atskira. Programa taip pat priima regioninius kodus `en-US`, `de-DE`, `fr-FR` ir `it-IT` ir naudoja jų formatus. Esami kalbos kodai išlieka suderinami.

## Layout ir gamintojo tyrimai

Klaviatūros vaizdas suteikia visą dydį ir neribotą ISO-DE/ANSI-US nuorodą ir Corsair K100 RGB išdėstymus. Geometrija reiškia pozicijas, plačias / dideles raktus ir ISO Enter. Standartiniai išdėstymai yra generiniai nuorodos. K100 išdėstytojai sekasi dokumentuotomis kontrolėmis, nereikalaujant milimetrų tikslumo. Pasirinkite teisingą kalbos / geometrijos variantą: modelio pavadinimas arba USB VID / PID nenustato konkretaus varianto.

Žemėlapis išsprendžia fizinius skenavimo kodus naudojant aktyvią Windows įvesties tvarką. Darbo srautas parengia projektą ir išlaiko taupymo / konflikto patikrinimus. Standartiniai ryšiai yra pasauliniai Windows šildytuvai, kurie nėra izoliuoti į vieną prijungtą klaviatūrą. Corsair G1 – G6 naudojamas įrenginio specifinis iCUE integravimas. Kiti nuosavybės kontrolės reikalauja adapterio arba gamintojo konfigūruotų sutrumpinimų. Pagrindinis „Enter“ ir skaitmeninis klaviatūra „Enter" dalijasi virtualiu raktiniu tapatybe pasauliniams raktams.

Į The [Gamintojo tyrimai](research/tastaturhersteller-sonderfunktionen.md) ir [CSV](research/tastaturhersteller-sonderfunktionen.csv) apima 74 prekės ženklų / gamintojų grupes, išskiriančias konfigūracijas, įvedimo SDK ir apšvietimo API. [Layout įrodymai](research/keyboard-layout-evidence.md) Įrašų modelis, SKU ir modulių skirtumai. Moksliniai tyrimai nereiškia, kad įgyvendinama aparatinė parama.

## Duomenys

Konfiguravimas saugomas vietiniuose paraiškos duomenyse. Importuoti ir eksportuoti JSON. Importuojamas konfigūracija, pakeičia dabartinę konfigūraciją po patvirtinimo ir išjungia importuotus ryšius, kol jie bus įjungti rankiniu būdu. Teksto veiksmo turinys nėra surenkamas kaip raktinis įrašas. „Credential Manager“ slaptažodžiai yra atskirti nuo eksportuotų profilių.

## Tikrinimas

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows patikrinimai gali patikrinti aparatūrą ir laikinai konfigūruoti / išleisti palaikomus iCUE ryšius. Veikia juos be kito valdytojo, naudojant šiuos ryšius. Audio endpoint patikrinimai nesikeičia gyvojo tūrio ar muto nustatymų.

Netiesioginis įvedimas negali viršyti Windows privilegijų. Paprastai paleistas valdytojas negali siųsti įrašo administratoriui. Rekomenduojami ar užimti šildytuvai atrodo kaip konfliktai.

Į The [Dokumentų indeksas](README.md) nuorodos į istorinius dizainus, planus, tyrimus ir datas bandymų ataskaitas. Istoriniai planai apibūdina jų įrašytą apimtį, o ne dabartinę funkcijų palaikymo garantiją.

### Darbo srauto šablonai ir tikslinis langas laukia

Katalogas apima šešis redaguojamus darbo srautus: darbo erdvę, susitikimus, srautus, mokslinius tyrimus, projektus ir tekstą. Ieškoti `workflow.` kataloge arba komandos paletėje. Pasirinkus darbo srautą, atveria nepagrįstą projektą; jis niekada neveikia iš karto. Visi žingsniai išlieka pridedant darbo srautą prie esamos makros arba priskiriant ją Corsair raktui. Prieš išsaugojdami nustatykite pavyzdžių URL, programos maršrutus, profilį ir OBS scenas.

Į The **Laukite tikslinio lango** Žingsnis priima procesą pavadinimą (su ar be `.exe`), Neįtikėtinas pavadinimas ir laiko išleidimas 1–600,000 ms. Reikia bent vieno filtro; abu filtrai turi atitikti, kai tiekiami. makro tęsiasi tik po to, kai atitinkamas matomas langas atsako ir pasiekia priekį. Naudokite tam tikrą pavadinimą, kai keli langai dalijasi procesu. Atšaukimas arba laikas sustoja vėlesnius žingsnius. lango pasirengimas nesuteikia garantijos, kad dokumentas yra įkrautas ar išorinė paslauga, pavyzdžiui, OBS WebSocket Susijęs.

Žymos archyvas: paieškos **Padidinti ekrano ryškumą**, **Sumažėja ekrano ryškumas**, arba **Ekrano šviesa 50 Procentų** Veiksmo katalogas. santykiniai veiksmai keičia ryškumą 10 Procento taškai; fiksuotos vertės 0 Į 100. Palaikomi vidaus ekranai naudoja WMI; suderinami išoriniai monitoriai reikalingi DDC/CI Įjungta jų meniu. numatyta, kad veiksmas paveikia visus palaikomus ekranus; redaktoriuje gali būti pasirinktas aiškus monitorius..

### Išplėstinė kontrolė

Atsisiųskite komandos paletę **Ctrl + K, o programos langas yra**. Ieškoti **Darbo srauto dizaineris**, **Rekordinė makro**, **Sinkronizuoti profilius**, arba **Pagrindinė diagnozė** (darbo srauto dizaineris redaguoja pasirinktą plokštelės dizainą: failą / procesą / langą sąlygas, į priekį perkelti ir sustabdyti / tęsti klaidų tvarkymą. įrašas užfiksuoja trumpalaikius kodus ir laukti, o ne įprastą rašymą; Escape sustabdo įrašymą..

Ryškumo žingsnyje pasirinkite visus palaikomus ekranus arba konkrečią monitorių. Neįmanomas pasirinktas monitorius sukelia klaidą, o ne tyliai keičia kitą ekraną. Lentelėje rodomas gyvas ryškumas, garso ir OBS skaitymas, kai jų atitinkamos integracijos yra prieinamos; neįmanoma skaityti kaip matuojama vertė.

Lauko redaktoriuje priskiriamas palaikomas fizinis specialus raktas ir pasirinkite nedelsiant spausdinti, įtempti, laikyti arba dvigubai. Laikui bėgant slėgio pasirinkėjai laikinai aktyvuoja kitą žemėlapį ir atkuria ankstesnį sluoksnį. Gestavimai ir sluoksniai taikomi Corsair SDK G raktams ir specialiems standartiniams raktams, o ne įprastoms pasaulinėms raktams. Standartinis raktas palaiko F13–F24 ir klaviatūros žiniasklaidos raktus. Savarankiškas Fn, profilio ir dial kontrolės vis dar reikalauja suderinamo pardavėjo adapterį arba pardavėjo žemėlapį į standartinį raktą. Raw Input stebima raktą ir nesumažina jo įprasto elgesio Windows. Pagrindinė diagnozė paaiškina profilį, pertrauką, ryšių ir privalomų problemų.

Profilų sinchronizavimas keičia failus per pasirinktą aplanką, įskaitant aplanką su esamu debesijos klientu. Prieš importuojant peržiūrėkite išankstinį vaizdą; importo pradžia išjungta ir pašalinti aparatūros specifinius žemėlapius. Revizijos neleidžia tyliai rašyti, ir įskaitant veiksmų turinį yra aiškus pasirinkimas. MAKRIVO nesuteikia debesijos paskyros ar saugo debesijos autentifikavimo. Dabar traukinys naudoja MAKRIVO savąjį M tvenkinį ir rodo amberą poilsio ženklą.

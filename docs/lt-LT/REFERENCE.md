<!-- generated-doc locale=lt-LT source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../README.md)

# MAKRIVO

[Visi dokumentai visomis 27 kalbomis](README.md) · [Visiškas vartotojo rankraštis](HANDBOOK.md)

**pagal Aivana.** [Dabartinis inventorius ir išleidimo apribojimai](FEATURES.md) · [Išplėstinis testas](testing/2026-10-12-six-extensions.md) · [Vietinis prototypų asistentas](testing/2026-10-12-local-meeting-assistant.md)

**Anglų kalba (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Vietinis Windows sparčiųjų klavišų valdiklis su Stream Deck stiliaus plytelėmis, profiliais, makrokomandomis ir klaviatūrų aptikimu. Sąsaja palaiko 27 kalbas. Kasdieniam naudojimui nereikia paskyros ar debesijos ryšio.

[Dokumentas](README.md) · [Tikrinimas](VERIFICATION.md) · [Funkcijos bandymų ataskaita](testing/2026-10-09-usp-release.md)

## Pradėti

Publikavus programa yra `artifacts/win-x64/MAKRIVO.exe`. Visą išvesties aplanką laikykite kartu; galite jį nukopijuoti kitur. Pradinė tikslinė platforma yra Windows 11 x64. Surinkimo artefaktai neįtraukiami į Git; pirmkodo kopijoje nėra iš anksto parengto vykdomojo failo.

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
4. Išsaugokite. Iki išsaugojimo pakeitimai lieka juodraštyje; atmetimas atkuria išsaugotą versiją. Plytelės Play mygtukas ir spartusis klavišas naudoja tą patį vykdymo kelią.
5. Grąžinkite plokšteles ir makro žingsnius su traukimo ir nuotėkio mygtukais. Šablonai supaprastina nustatymą. Makros gali pakartoti iki 100 kartų. Atšaukti per sąsają, sistemos pjovimo meniu arba Ctrl+Alt+Escape pagal numatytuosius.

Programai būdingi profiliai naudoja proceso pavadinimą be viso kelio. Kai atitinka keli profiliai, pasirinkimą lemia jų tvarka; numatytasis profilis yra atsarginis. Automatinį profilio parinkimą reikia įjungti Settings.

Uždarius pagrindinį langą, programa pagal numatytuosius nustatymus lieka pranešimų srityje. Atidarykite ją dukart spustelėdami piktogramą; išeikite per jos meniu. Paleidimas kartu su Windows pasirenkamas ir pagal numatytuosius nustatymus išjungtas. Sparčiuosius klavišus galima centralizuotai pristabdyti.

## Veiksmai ir komandos paletė

Add Action atidaro katalogą su paieška ir 90 ruošinių aštuoniose kategorijose. Kiekvienas turi aprašą ir peržiūrą. Makrokomandų redaktorius naudoja tą patį katalogą veiksmams pridėti ar pakeisti. Pasirinkus ruošinį šiame redagavimo procese sukuriamas juodraštis be vykdymo.

Komandų paletė atidaroma iš antraštės arba su Ctrl+K programoje. Ji ieško išsaugotų veiksmų visuose profiliuose, katalogo ruošinių, profilio perjungimo, navigacijos, pristabdymo/tęsimo, makrokomandų atšaukimo ir importo/eksporto. Filtruokite pagal išsaugotus veiksmus, katalogo funkcijas arba programos komandas. Rodyklių klavišai parenka, Enter aktyvina, Ctrl+Enter redaguoja redaguojamą įrašą, Escape uždaro. Parankiniai rodomi pirmiausia. Parengtos katalogo komandos gali vykdyti iš karto; nustatymo reikalaujantys ruošiniai atidaro juodraštį. Settings galima nustatyti visuotinį paletės spartųjį klavišą. Naujos konfigūracijos naudoja Ctrl+Shift+Space; esamoms naujas visuotinis spartusis klavišas automatiškai nepridedamas.

Garso veiksmai tiksliai nustato garsumą nuo 0 iki 100 procentų ir nutildo, įjungia garsą arba perjungia išvesties bei mikrofono įrenginių nutildymą. Pasirinkite konkretų galinį tašką arba palikite nenustatytą, kad būtų naudojamas Windows numatytasis. Nepasiekiamas išsaugotas galinis taškas sukelia klaidą, užuot valdžius kitą įrenginį. Refresh Devices iš naujo įkelia prijungtus įrenginius.

OBS Studio naudoja WebSocket v5: įrašymo pradėjimas/sustabdymas/perjungimas/pristabdymas/tęsimas; transliacijos pradėjimas/sustabdymas/perjungimas; scenų keitimas; įvesties nutildymo perjungimas. OBS meniu Tools įjunkite WebSocket serverį ir redaktoriuje nustatykite galinį tašką. Check Connection and Load Names nuskaito OBS versiją, scenas ir įvestis nepradėdamas įrašymo ar transliacijos. Pasirinkite pavadinimus sąraše arba įveskite rankiniu būdu. Slaptažodžius galima išsaugoti Windows Credential Manager; jie neįtraukiami į eksportuotas konfigūracijas. Nepasiekiami OBS ryšiai pateikia klaidą.

Profiliai gali atitikti proceso pavadinimą ir lango antraštės dalį. Abi nurodytos sąlygos turi atitikti. Lock Profile neleidžia automatiškai perjungti profilio seanso metu. Capture Conditions from Foreground naudoja paskutinę fokusuotą išorinę programą.

Teksto ir iškarpinės veiksmai palaiko `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ir `{{user}}`. Datoms naudojamas `yyyy-MM-dd`; laikui – `HH:mm`. Nežinomos vietaženklės lieka nepakeistos. `{{profile}}` nurodo paleisto veiksmo profilį; `{{process}}` nurodo išorinės programos kontekstą. Iškarpinės turinys skaitomas tik naudojant tą vietaženklę. Iškarpinės veiksmas įrašo tekstą, bet automatiškai jo neįklijuoja.

langų veiksmai sumažinti, maksimaliai, atkurti arba išdėstyti langą kairėje / dešinėje savo monitoriaus darbo srityje. Sistemos veikimo užraktai Windows. Tokie veiksmai vyksta tik tada, kai aiškiai raginami.

Preview rodo veiksmus, kartojimus ir kintamuosius be vykdymo. Undo ir Redo išsaugo iki 100 juodraščio būsenų; teksto laukuose išlieka įprastas Ctrl+Z redagavimas. Žymėkite parankinius redaktoriuje ar plytelės meniu ir filtruokite skydelyje. Vykdymų skaitikliai taikomi tik dabartiniam seansui.

Iš sąsajos paleisti teksto, sparčiųjų klavišų ir langų veiksmai aktyvina paskutinį išorinį tikslinį langą. Paletė rodo tikslinį kontekstą. Jei tikslas uždarytas arba Windows atsisako aktyvinti, vykdymas sustoja su pranešimu.

## Pagalba ir grįžtamoji informacija

Special-Key Wizard yra klaviatūros puslapyje ir paletėje. Jis išvardija iCUE pateiktus G klavišus. Detect Key Press juos laikinai fiksuoja nevykdydamas priskirtų veiksmų. Įprasti susiejimai grįžta po aptikimo, atšaukimo ar uždarymo. Katalogo funkcija ir pasirinktas klavišas atidaromi kaip neišsaugotas juodraštis; tikrinami pasikartojantys susiejimai.

Keyboard Overlay rodo pasirinktą pavyzdinį išdėstymą ir aktyvius susiejimus. Laikykite Ctrl+Alt+F1 arba perjunkite jį paletėje ar klaviatūros puslapyje. Jis neperima fokuso ir praleidžia pelės paspaudimus. Profilio ir susiejimų pakeitimai atnaujinami, kol jis matomas; įprasti Windows klavišų paspaudimai paryškinami. Gamintojo valdikliai gali nepateikti atitinkamo Windows klavišo signalo.

Vykdymo informacija rodo laukimą, veiksmo/kartojimo eigą, užbaigimą, atšaukimą arba nesėkmę, neperimdama fokuso ir neatskleisdama įvesto teksto. Commands Executed patvirtina komandos vykdymą, o ne nepriklausomai nuskaitytą tiesioginę trečiosios šalies programos būseną. Klaidų informacija pateikiama veiklos žurnale.

## Įrenginiai ir Corsair

Corsair G1–G6 galima priskirti tiesiogiai per oficialų iCUE SDK. Veiksmų redaktoriuje pasirinkite įrenginį ir klavišą bei išsaugokite arba parenkite juodraštį K100 klaviatūros rodinyje. Susiejimai galioja pasirinktam įrenginiui ir aktyviam profiliui; pristabdymas juos taip pat išjungia. iCUE turi veikti ir leisti prieigą. Fn, iCUE ratuko, profilio ir užrakto valdiklių negalima susieti per šią G-key API.

Įrenginių puslapis reguliariai ir kai prietaisai yra prisijungę, pateikia sąrašą Windows klaviatūros sąsajų ir atnaujina. Fizinė klaviatūra gali atskleisti keletą HID sąsajų. Standartiniai pasauliniai šildytuvai dirba tarp gamintojų.

Corsair aptikimas naudoja turimą įrenginio informaciją. RGB valdymui reikia suderinamos aparatūros, veikiančio iCUE ir oficialaus iCUE SDK; programa praneša faktinį pasiekiamumą. Kitiems gamintojams šiuo metu teikiamas aptikimas ir standartiniai veiksmai. Už Corsair G-key integracijos ribų gamintojo valdikliai veikia, kai Windows juos gauna kaip įprastą įvestį. Programa neįdiegia klaviatūros tvarkyklės.

## Kalbos

Settings siūlo 27 kalbas: vokiečių, anglų, prancūzų, ispanų, italų, portugalų, olandų, lenkų, čekų, slovakų, vengrų, rumunų, bulgarų, graikų, danų, švedų, norvegų, suomių, estų, latvių, lietuvių, slovėnų, kroatų, ukrainiečių, rusų, turkų ir serbų lotyniškais rašmenimis. Automatinis režimas naudoja Windows rodymo kalbą. Vertimai įtraukti vietoje. Išdėstymai, spartieji klavišai ir naudotojo sukurti pavadinimai nepriklauso nuo sąsajos kalbos.

Išsamus saugyklos dokumentas yra prieinamas visomis 27 paraiškos kalbomis per [Kalbos indeksas](README.md)Ir Vertimai yra mašinos pagalba, su originaliomis nuorodomis ir struktūriniais patikrinimais; gimtojo kalbos apžvalga yra atskira. Programa taip pat priima regioninius kodus `en-US`, `de-DE`, `fr-FR` ir `it-IT` ir naudoja jų formatus. Esami kalbos kodai išlieka suderinami.

## Layout ir gamintojo tyrimai

Klaviatūros rodinyje pateikiami pilno dydžio ir be skaitmeninio bloko ISO-DE/ANSI-US pavyzdžiai bei Corsair K100 RGB išdėstymai. Geometrija vaizduoja padėtis, plačius/aukštus klavišus ir ISO Enter. Standartiniai išdėstymai yra bendri pavyzdžiai. K100 išdėstymai atitinka dokumentuotus valdiklius, bet neteigia milimetrinio tikslumo. Pasirinkite tinkamą kalbos/geometrijos variantą: modelio pavadinimas arba USB VID/PID nenustato konkretaus varianto.

Susiejimas nustato fizinius skenavimo kodus pagal aktyvų Windows įvesties išdėstymą. Darbo eiga parengia juodraštį ir išlaiko išsaugojimo/konfliktų patikras. Standartiniai susiejimai yra visuotiniai Windows spartieji klavišai, o ne izoliuoti vienai prijungtai klaviatūrai. Corsair G1–G6 naudoja įrenginiui būdingą iCUE integraciją. Kitiems gamintojo valdikliams reikia adapterio arba gamintojo nustatytų sparčiųjų klavišų. Pagrindinis Enter ir skaitmeninės klaviatūros Enter visuotiniams spartiesiems klavišams turi tą pačią virtualiojo klavišo tapatybę.

Į The [Gamintojo tyrimai](research/tastaturhersteller-sonderfunktionen.md) ir [CSV](research/tastaturhersteller-sonderfunktionen.csv) apima 74 prekės ženklų / gamintojų grupes, išskiriančias konfigūracijas, įvedimo SDK ir apšvietimo API. [Layout įrodymai](research/keyboard-layout-evidence.md) Įrašų modelis, SKU ir modulių skirtumai. Moksliniai tyrimai nereiškia, kad įgyvendinama aparatinė parama.

## Duomenys

Konfigūracija saugoma vietiniuose programos duomenyse. Importas/eksportas naudoja JSON. Importas pateikia konfigūracijos peržiūrą, patvirtinus pakeičia dabartinę konfigūraciją ir išjungia importuotus susiejimus, kol jie įjungiami rankiniu būdu. Teksto veiksmų turinys nerenkamas kaip klavišų paspaudimų žurnalas. Credential Manager slaptažodžiai atskirti nuo eksportuotų profilių.

## Tikrinimas

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows patikros gali tikrinti aparatūrą ir laikinai nustatyti/atlaisvinti palaikomus iCUE susiejimus. Vykdykite jas, kai kitas valdiklis nenaudoja šių susiejimų. Garso galinių taškų patikros nekeičia esamo garsumo ar nutildymo nustatymų.

Savoji įvestis negali apeiti Windows teisių. Įprastai paleistas valdiklis gali negalėti siųsti įvesties administratoriaus programai. Rezervuoti arba užimti spartieji klavišai rodomi kaip konfliktai.

Į The [Dokumentų indeksas](README.md) nuorodos į istorinius dizainus, planus, tyrimus ir datas bandymų ataskaitas. Istoriniai planai apibūdina jų įrašytą apimtį, o ne dabartinę funkcijų palaikymo garantiją.

### Darbo srauto šablonai ir tikslinis langas laukia

Kataloge yra šešios redaguojamos darbo eigos: darbo vieta, susitikimas, transliacija, tyrimas, projekto perjungimas ir teksto fragmentas. Kataloge arba komandų paletėje ieškokite `workflow.`. Pasirinkus darbo eigą atidaromas neišsaugotas juodraštis; ji niekada nevykdoma iš karto. Visi veiksmai išlaikomi pridedant darbo eigą prie esamos makrokomandos ar priskiriant ją Corsair klavišui. Prieš išsaugodami nustatykite pavyzdinius URL, programų kelius, profilius ir OBS scenas.

Veiksmas **Wait for target window** priima proceso pavadinimą (su `.exe` arba be jo), antraštės poeilutę neskiriant didžiųjų ir mažųjų raidžių bei 1–600000 ms laukimo limitą. Būtinas bent vienas filtras; nurodžius abu, turi atitikti abu. Makrokomanda tęsiama tik kai atitinkantis matomas langas reaguoja ir atsiduria pirmame plane. Jei vienas procesas turi kelis langus, naudokite konkrečią antraštę. Atšaukimas arba laukimo pabaiga sustabdo tolesnius veiksmus. Lango parengtis negarantuoja, kad dokumentas įkeltas ar išorinė paslauga, pvz., OBS WebSocket, prijungta.

Ekrano ryškumas: veiksmų kataloge ieškokite **Increase screen brightness**, **Decrease screen brightness** arba **Screen brightness at 50 percent**. Santykiniai veiksmai keičia ryškumą 10 procentinių punktų; fiksuotos vertės yra nuo 0 iki 100. Palaikomi vidiniai ekranai naudoja WMI; suderinamų išorinių monitorių meniu turi būti įjungtas DDC/CI. Pagal numatytuosius nustatymus veiksmas veikia visus palaikomus ekranus; redaktoriuje galima pasirinkti konkretų monitorių. Jungtys ir dokai gali riboti palaikymą.

### Išplėstinė kontrolė

Atidarykite komandų paletę su **Ctrl+K, kai programos langas turi fokusą**. Ieškokite **Workflow designer**, **Record macro**, **Synchronize profiles** arba **Key diagnostics** (formuluotė atitinka pasirinktą kalbą). Darbo eigos kūrimo priemonė redaguoja pasirinktos plytelės juodraštį: failo/proceso/lango sąlygas, į priekį praleidžiamų veiksmų skaičių ir sustabdymą/tęsimą įvykus klaidai. Įrašymo priemonė fiksuoja klavišų derinius ir laukimą, ne įprastą rašymą; Escape sustabdo įrašymą. Prieš priimdami peržiūrėkite rezultatą.

Ryškumo veiksme pasirinkite visus palaikomus ekranus arba konkretų monitorių. Nepasiekiamas pasirinktas monitorius sukelia klaidą, užuot tyliai keitus kitą ekraną. Skydelis rodo tiesioginius ryškumo, garso ir OBS rodmenis, kai atitinkamos integracijos pasiekiamos; nepasiekiamas rodmuo nerodomas kaip išmatuota vertė.

Plytelės redaktoriuje priskirkite palaikomą fizinį specialųjį klavišą ir pasirinkite tiesioginį paspaudimą, trumpą paspaudimą, laikymą arba dvigubą paspaudimą. Laikomi sluoksnio parinkikliai laikinai aktyvina kitą susiejimą ir atleidus atkuria ankstesnį sluoksnį. Gestai ir sluoksniai galioja Corsair SDK G klavišams ir įrenginiui būdingiems standartiniams specialiesiems klavišams, o ne įprastiems visuotiniams spartiesiems klavišams. Standartinių klavišų fiksavimas palaiko F13–F24 ir klaviatūros medijos klavišus. Gamintojo Fn, profilio ir ratuko valdikliams vis dar reikia suderinamo gamintojo adapterio arba gamintojo susiejimo su standartiniu klavišu. Raw Input stebi klavišą ir neslopina įprasto jo Windows veikimo. Klavišų diagnostika paaiškina profilio, pristabdymo, ryšio ir susiejimo problemas.

Profilių sinchronizavimas keičiasi failais pasirinktame aplanke, įskaitant esamo debesijos kliento valdomą aplanką. Prieš importuodami peržiūrėkite peržiūrą; importuoti susiejimai iš pradžių išjungti, o aparatūrai būdingi susiejimai pašalinami. Revizijos apsaugo nuo tylaus perrašymo, o veiksmų turinio įtraukimas yra aiškus pasirinkimas. MAKRIVO nesuteikia debesijos paskyros ir nesaugo debesijos prisijungimo duomenų. Pranešimų srityje dabar naudojama paties MAKRIVO melsvai žalia M piktograma su gintarine pristabdymo žyma, kai pristabdyta.

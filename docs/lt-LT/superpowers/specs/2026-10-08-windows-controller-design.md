<!-- generated-doc locale=lt-LT source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – projektas

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Standas: 8. Spalio mėn. 2026. Statusas: patikrinimas prieš įgyvendinimą.

## Tikslas ir susitarimas

Savarankiška Windows programa daro įprastą kompiuterio klaviatūrą laisvai konfigūruojamą veiksmų plokštelę, panašią į srovės dangtį. Vartotojas patvirtino visus siūlomus veiksmus, makrus, prietaiso atpažinimą, profilį ir gerą GUI. Prijungtos Corsair ir kitos klaviatūros yra atpažįstamos; papildomos gamintojo funkcijos siūlomos tik tada, kai jos iš tikrųjų palaikomos.

Įtarimai: Vokietijos paviršius, vietinis naudojimas be sąskaitos, Windows 11 x64 kaip pirmoji vieta, nėra vairuotojo įrengimas. Klaviatūra išlieka įprastai rašyti. Pasauliniai įkrovikliai naudojasi standartiniais klaviatūrais su modifikacijomis. Atskirų raidžių mygtukai standartiniu mastu nėra rezervuoti.

## Techniniai sprendimai

Native C#/ NET-10-App su WPF ir MVVM. Stabilus .NET-10-SDK yra prieinamas darbo vietoje. WPF leidžia individualią darbalaukio sąsają ir tiesioginę integraciją Windows. Alternatyvūs būdai būtų interneto sąsaja su atskira paslauga Windows arba platformoje esančia darbalaukio programa; abi šios Windows projekto veikimo laiko ir komunikacijos sudėtingumo papildymas. Todėl vietinė variantas yra pasirinkta pagrindas.

Skirstymas: darbalaukio projektas vaizdams ir traukiniams, branduolinis projektas konfigūracijai ir vykdymo logikai, Windows projektas „Hotkeys“, įvedimo simuliavimas ir prietaisai, bandymo projektas nepriklausomiems nuo platformos taisyklių. Gamintojo adapteriai yra prijungti prie tvirtos pajėgumo sąsajos ir yra nepriklausomi nuo standartinių veiksmų.

## paviršius

Tamsus dizainas su ramiomis antrazitinėmis plokštelėmis, gerai skaitytu raštu ir turkų spalvų akcentu. Nuotolinis navigacija prie „Dashboard“, profilių, įrenginių ir nustatymų. Viršuje aktyvus profilis, paieškos ir perjungimo visiems hotkeys. Paplūdimys išlieka naudojamas 100–200 procentų Windows skalavimo.

Dashboard yra pritaikytas šaknis. Kiekvienas šaknis rodo simbolį, pavadinimą, šaknis ir vykdymo statusą. Klikimas atlieka tą patį veiksmą kaip ir "Hotkey". Dešinėje atidaromas redaktorius, kuriame yra veiksmų tipas, parametrai, „Hotkey“ įrašas, simbolis / spalva, aktyvavimas ir bandymo mygtukas. Naujas, padvigubinti, ištrinti ir pertvarkyti yra prieinami. Klaviatūros vaizdas rodo Vokietijos standartinio išdėstymo aprašymus; jis nenurodo tikslios prietaiso geometrijos.

Profilius galima sukurti, kopijuoti, pakeisti ir įjungti rankiniu būdu. Galutinis procesas priskiriamas profiliui. Tinkama pagrindinė programa aktyvuoja savo profilį, kitaip taikomas pasaulinis standartinis profilis. Daugeliui tinkamų profilių taikoma aiškiai matoma tvarka. Redaktorius paaiškina Hotkey konfliktus tiesiai į apgyvendinimą.

## Hotkeys ir įvadas

Windows RegistrasHotKey apdorojasi įprastomis pasaulinėmis kombinacijomis su slopintu mygtuku. Registruojamas tik aktyvus profilis. Rezervuotos arba jau užfiksuotos kombinacijos pasirodys kaip klaidos; programa neskelbia, kad jie sėkmingai aktyvuojami. Įrašymas redakcijoje neišsprendžia veiksmų.

Prieš simuliuojamus įvedimus išleidžiami modifikacijos mygtukai arba jų fizinis išleidimas tikimasi. Unicode tekstas ir mygtukų deriniai siunčiami per SendInput. Kiekvienas patį spausdintas mygtukas išleidžiamas, net ir nutraukus. Veiksmai vyksta už UI juostos ribų; vykdymas ir profilio keitimas negali blokuoti paviršiaus.

## Veiksmai ir makros

Veiksmų tipai: pradėti programą su argumentais ir darbo sąrašu, atidaryti failą / plokštę per Windows, atidaryti HTTP / HTTPS svetainę, įveskite Unicode tekstą, siųsti mygtukų derinį, Media Play / Pause / Next / Predecessor / Stop, garsas didesnis / mažesnis / triukšmas ir keisti profilį. Parametrai patvirtinami prieš saugojimą ir dar kartą prieš vykdymą. Programos pradedamos tiesiogiai; argumentai nėra aiškinami kaip Shell įsakymai.

Makros apima išdėstytą minėtų veiksmų sąrašą ir aiškius žingsnius. Žingsnius galima pridėti, redaguoti ir atidėti. Pasikartojimai yra galutinai konfigūruojami (1–100); begalės nėra pirmoji versija. Veikiantis makro gali būti užbaigtas per paviršių arba savo slopinimo slėnis. Pakartotinis to paties vykstančio įkrovimo pradžia nėra lygiagrečiai. Įvairios makros su įvedimo simuliacija serijalizuojamos taip, kad jų mygtukai nebūtų maišomi. Jei žingsnis klaidą, makro sustoja ir rodo paveiktą žingsnį.

## Prietaisai ir gamintojai

Įrangos inventorius Windows Raw Input ir papildoma įrangos informacija: rodiklis, įrangos kelias, gamintojo / produkto atpažinimas, jei yra, ryšio būklė ir pripažintos įgūdžiai. Į/išjungimas atnaujina sąrašą be naujo paleidimo. Fizinis prietaisas gali turėti keletą HID įrašų; jie yra suskirstyti pagal apkrovos ID, kitaip matomi kaip atskiri įrašai.

Paprastas Hotkeys taikomos tarpaskaitmeniniu būdu. Įrenginio vaizdas suteikia informaciją ir įgūdžius; atskira klaviatūra su išskirtiniais mygtukais nėra šio susitarimo režimo dalis.

Corsair atpažinimas taip pat veikia be iCUE. Pasirinktas iCUE SDK adapteris suteikia ryšio statusą ir palaikomas apšvietimo veiksmus, kai yra SDK, iCUE ir suderinamas įrenginys. UI skiriasi atpažįstamas, SDK prieinamas, sujungtas ir iš tikrųjų valdomas. SDK daugiausia dokumentuoja LED kontrolę; savitarpio G mygtukai palaikomi tik tuo atveju, jei Windows gauna atitinkamus įrašus. Nėra pastovių įsipareigojimų visiems spaudimui.

Kiti gamintojai pirmiausia gauna bendrą aptikimą ir standartinius veiksmus. Adapterio sąsaja leidžia vėlesnį integraciją; nesugebėjimas sąžiningai rodomas. Neegzistuojančių modelių aparatūros testai nėra nurodyti kaip išlikę.

## Saugojimas ir kasdienybė

Versija JSON konfigūracija pagal vietinį programos duomenų sąrašą. Atominis saugojimas su paskutiniu galiojančiu saugojimu; klaidingi failai nėra tyli. Importuojamasis tikrina formatą, versiją, ID ir parametrus ir rodo pranašumą prieš perkant. Importuotos akcijos bus vykdomos tik po aiškios aktyvacijos. Eksportas apima profilius ir įskaitymus.

Trajimo meniu: atidaryti, pasirinkti profilį, pertraukti hotkeys, nutraukti makrus, užbaigti. langai uždaryti standartiniu mastu ir paaiškinti tai pirmą kartą. Automobilis yra atviras ir standartinis. Tik viena institucija valdo Hotkeys. Baigimas registruoja hotkeys, nutraukia išvadą ir išleidžia išteklius.

Vietinis veiklos vaizdas su ribotu sėkmės, klaidų ir nutraukimo kursu. Nėra visų klaviatūrų įrašų. Teksto turinys nėra parašytas klaidų pranešimuose. Nėra debesų ir jokių telemetrų.

## Klaidų tvarkymas ir ribos

Trūkstamos programos, negaliojančios URL, užfiksuoti raktai, sugadintas profilius ir nepasiekiamas gamintojo SDK gauna konkrečius Vokietijos pranešimus. Adapterio klaida neturi įtakos standartinėms operacijoms. Įvadas į aukštesnę privilegijuotą programą gali nepavyko Windows leidimų ribas; klaidos paaiškėja, programa nereikalauja standartinių administratoriaus teisių.

## Tyrimas ir sumažinimas

Automatiniai profilio prioriteto, konflikto nustatymo, konfigūracijos importo, atominio saugojimo, makro sekos, pakartotinio, nutraukimo ir klaidų propagavimo testai. Windows Smoke testai realiems raktams, teksto ir mygtukų deriniams, žiniasklaidos veiksmams, prietaiso inventoriui, traukiniui ir grynam pabaigai. Nėra bandymų, kurie atsitiktinai paleisti bet programas ar svetaines.

GUI bandymas: įdiegimas ir redagavimas, įrašas be vykdymo, lygios efektos spustelėjimas / klaviatūra, konflikto pranešimas, profilio keitimas, prietaiso matymas, importas / eksportas ir skalavimas. Gamintojo specifinis aparatūros patikrinimas dokumentuojamas atskirai su faktiniu įrenginiu.

Pardavimas: šaltinio kodas, atkuriamas statybos / pradžios vadovas, Vokietijos naudojimo vadovas ir vietinis Windows-x64-Publish-Artefakt. Sėkmė reiškia vykdomą programą su visomis bendromis funkcijomis; Corsair įgūdžiai yra pažymėti kaip prieinami tik įrodytos SDK / prietaiso paramos.

## Techniniai šaltiniai

- Microsoft klaviatūros įvedimas / registrasHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Įėjimas: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Oficialus Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Kitas žingsnis

Išanalizavus šį projektą: parengti konkretų įgyvendinimo planą ir pateikti įgyvendinimo pasirinkimą. Tuščiame darbo tvarkykloje dar nėra „Git Repository“; todėl šis dokumentas dar nebuvo įsipareigojęs.

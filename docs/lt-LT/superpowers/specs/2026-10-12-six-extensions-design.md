<!-- generated-doc locale=lt-LT source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: šeši produktų plėtiniai ir klaviatūros adapteriai

Datas: 2026-10-12
Statusas: pasiūlytas dizainas naudotojo peržiūros; įgyvendinimas nebuvo pradėtas.

## Tikslas
Siųskite visus šešis prašomus papildymus esamoje Windows / WPF paraiškoje, išsaugojant esamus profilius, G1/G2 paskyrimus, vietinę veiklą ir sąskaitos nemokamą naudojimą. Išplėsti klaviatūros integraciją į Razer ir Logitech, su pakartotiniu gebėjimu pagrįstu architektūra kitiems gamintojams. Gamintojo vardas ar klaviatūros išdėstymas niekada nėra įrodymas, kad palaikoma nuosavybės kontrolė.

## Architektūros sprendimas
Išplėskite Aivana.Core su serializuotomis sutartimis ir patvirtinimu, Aivana Windows su Windows ir pardavėjo adapteriais, ir Aivana.Desktop su sandorių leidėjais ir statuso vaizdais. Naudokite atskirus bendradarbiavimo projektus izoliuotam įkroviklio vykdymui ir vietiniam mobiliojo panelio paslaugai. Venkite perrašymo ar vieno privilegijuoto proceso kiekvieną išplėtimą. Esama konfigūracija turi būti nepakitusi; papildyti veiksmo rūšis, o ne perreguliuoti jų nuolatines vertybes.

Alternatyvos apsvarstytos: monolitinė įgyvendinimas apimtų aparatūros, tinklo ir darbo srautų vykdymą; pakeitimas WPF pridėtų nesusijusią migracijos darbą. Rekomenduojami moduliniai pratęsimai.

## 1. Nepriklausoma klaviatūra „Macro Pad“
Įrenginio tapatybės ir fizinio skenavimo kodo bei išplėstinių raktų vėliavų sandėliavimas. Pridėti aiškius stebėjimo ir išskirtinius režimus, prietaiso atradimą, raktą užfiksuoti, pakartotinai slopinti, gestus, laikyti sluoksnius ir vėl sujungti diagnozę. Išsaugokite pagrindines / numatytos Enter skirtumus ir išbandykite Vokietijos / JAV nustatymus. Ryšiai yra nukreipti į pasirinktą fizinį klaviatūrą; abejingi atkuriami tapatybės reikalauja pakartotinio pasirinkimo, o ne kito įrenginio kontrolės.

Raw Input Išskirtinis kiekvieno įrenginio pašalinimas reikalauja tikro klaviatūros filtro taško; niekada nesusijęs su pasauliniu žemu lygmeniu Raw Input ir reikalauti patikimo įrenginio specifikacijos pašalinimas. teikti stebėjimo, avarinių išleidimų, klaidų atviros elgesys ir installer / uninstaller. Neimkite saugios darbalaukio įvedimo. vairuotojo įdiegimas ir išskirtinis režimas aktyvavimas yra aiškiai atskiri naudotojo veiksmai.

Vairuotojo vartas: pasirinkite palaikytą, tinkamai licencijuotą ir pasirašytą rinkinį, suderinamą su tiksline Windows 11 konfigūracija, arba sukurkite / pasirašykite dedikuotą filtrą. Intercepcija yra vertinimo kandidatas, o ne įsipareigojusi gamybos priklausomybė: jo oficialus README apibūdina bandymus per Windows 10 ir atskirą komercinę licenciją. Kol šis vartas bus patenkintas, išskirtinis režimas turi būti rodomas kaip nepasiekiamas; funkcija negali būti paskelbta pilna.

Priėmimas: su dviem fiziniais klaviatūra, tas pats raktas veikia makro ir gamina ne įprastą paraiškos įvadas tik pasirinktu įrenginiu; kitas klaviatūros įvadas lieka nepažeistas. Įsitikinkite išjungimą, avariją, atšaukimą, sustabdymą ir vairuotojo pašalinimą realiame aparatūroje.

## 2. Vadovaujamas darbo erdvė
Pridėti burtininką darbo vietoje, susitikimų, srauto, mokslinių tyrimų ir projektų keitimo. Atraskite įdiegtas programas, garso įrenginius, monitorius ir OBS scenas. Vartotojai pasirenka tikslus ir ryšius; pažvelgti į rezultatus ir patvirtinti trūkstamus reikalavimus. Išsaugokite tik po peržiūros, be jokių veiksmų nustatymo metu. Gaminti profilius galima redaguoti esamais leidėjais.

Priėmimas: sukurkite darbo profilį be rankiniu būdu redaguojamų maršrutų ar prietaiso ID; atšaukimo lapai nepakeičia nustatymų; trūksta programinės įrangos turi veiksmingą paaiškinimą.

## 3. Tikrinamas vykdymas
Įveskite išankstinius patikrinimus, tipinius per žingsnį rezultatus ir pasirinktines sąlygas. Skiriama prašoma, užbaigtos, patvirtintos, nesėkmingos, atšauktos ir neišspręstos sąlygos. Patikrinkite garso / mikrofoną, ryškumą, procesą / langą ir OBS būseną su apribotais nesinkroniniais skaitymais. Trumpas / teksto pristatymas nėra įrodymas, kad tikslinė paraiška pritarė komandai. Išoriniai šalutiniai poveikiai nėra universalios grįžtamos; siūlo kompensaciją tik ten, kur įgyvendinama.

Priėmimas: išjungti aiškūs tikslai niekada tyliai grįžta; klaidingai stebimas būklė niekada praneša apie patikrintą sėkmę; atšaukimas nustoja apklausą; nesėkmės nustato žingsnį ir atsigavimo veiksmus. Veikimo istorija ignoruoja tekstinius mokėjimus ir paslaptis.

## 4. Plugins ir dalinamieji paketai
Versija JSON apibūdina veiksmus, parametrus schemas, valstybės teikėjus, priklausomybę ir gebėjimus. Veiksmingi įskiepiai vykdomi atskirame procese per ribotą vietinį IPC su rankomis, terminai ir atšaukimas. Atsisakykite klaidinančių pranešimų ir nesuderinamų versijų; tvarkykite avarijas be valdymo pabaigos. Galimybės deklaracijos yra atskleidimas ir tarpininkavimas, o ne OS sandboxing reikalavimas.

Siūlome paketų eksporto / importavimo peržiūrą, turinio hashus, maršruto apsaugą, dydžio apribojimus, versijos / priklausomybės patikrinimus, aiškią įgaliojimą ir atnaujinimą. Importuojami vykdomieji įtaisai neturėtų vykti automatiškai. Įtraukite darbo pavyzdį su galutiniu įdiegimo / veiksmų / valstybės / išdiegimo srautu. Šiam išleidimui nėra reikalingos viešosios rinkos paslaugos.

Priėmimas: sukurti, eksportuoti, įdiegti ir vykdyti tikrąjį pavyzdį integracijos; parodyti laiko, avarijos izoliacija, priklausomybės nesėkmė, perėjimo atsisakymas ir neįgali importuoti.

## 5. Windows Automatija
Pridėkite elementą ir ribotus veiksmus, skirtus skambinti, vertę, pasirinkimą, dėmesį ir laukti elemento / valstybės. Atsisakykite pasirinkėjų, naudojant procesą, automatizavimo ID, kontrolės tipą ir protėvių kontekstą; vengti koordinuoti tik įrašymą. Rodyti rinktinės rinkinį ir bandymų rinktines skaityti tik. Slapukų laukai yra neįtraukti į įrašymą. Išsamiai tvarkykite nepalaikytas kontroles ir Windows vientisumo ribas.

Išplėsti įrašymą su peržiūriniais UIA veiksmais, išlaikant dabartinį trumpalaikį įrašymą. Neabejotini rinkėjai nesugeba, o ne spustelėti savavališką kontrolę. Paprastas naršyklės prieinamumas nėra visiško DOM automatizavimo variklio pažada.

Priėmimas: automatiškai atlikite dedikuotą bandymų programą su stabiliais pasirinkėjais per pozicijos / dydžio pokyčius; bandymų abejingumas, trūkstami elementai, neįjungti kontrolės, laiko išleidimas ir atšaukimas.

## 6. Vietinis telefonas / planšetinis panelis
Paslaugą teikia reaktyvus panelis iš opt-in LAN paslaugos. Pažintys aiškiai naudojant trumpalaikį vienkartinį kodą ir išduodant atšaukiamus patvirtinimus, saugomus vietovėje. Naudokite HTTPS su dokumentuota vietinio sertifikato pasitikėjimo/pinning; jokių atsiliepimų siunčiant įrodymus skundo. Neteisingas įjungti ir riboti konfigūruotus sąsajas. Kilmės patvirtinimas, autentiški užsakymai, ribos ir riboti pranešimai yra privalomi.

Leidžiami profiliai / veiksmai, gyvas būdas, pažanga ir atšaukimas; naudokite stabilius ID ir tą patį vykdymo spragą kaip darbalaukio ir fizinių raktų. Nenaudokite savavališkų komandų, vykdomų maršrutų, paslapčių ar įdiegimo priedų. Siūlome ryšį, poravimą ir atšaukimo ekranus. Nėra jokių debesų sąskaitų ar interneto ryšio.

Priėmimas: veikia iš tikrojo telefono / planšetinio kompiuterio toje pačioje LAN; nesusiję klientai negali sąrašo ar vykdyti veiksmų; atšaukti tokenai neveikia; dvigubai atlikti komandos / atkūrimas neveikia; diskonekcija tyliai atšaukia veiksmus.

## Keyboard gamintojo apimtis

| Adaptai / Šeima | Planuojama integracija | aiškiai apribotas |
| --- | --- | --- |
| Corsair | Išsaugokite dabartinį iCUE G raktą ir RGB integraciją | Modelio / programinės įrangos gebėjimų aptikimas lieka reikalingas |
| Logitech G | G raktas SDK adapteris su spausdinimo / išleidimo ir paleidimo laiko prieinamumo patikrinimas; apšvietimas per atskirą SDK, kur palaikoma | Nereikia lyginti G HUB aptikimo su palaikomais G raktų API kiekviename modeliuje |
| Razer | Chroma apšvietimo adapteris ir vadovaujama Synapse-standartinė raktų žemėlapis, plius generinis HID įvedimas | Chroma yra apšvietimo API; nerašykite savo pačių makro raktų įvesties |
| SteelSeries | GameSense būklės / apšvietimo adapteris ir generinis HID įvedimas | GameSense renginio pateikimas nėra bendras savininkas raktas užfiksuoti API |
| Wooting | Analoginis SDK adapteris palaikytam analoginiam įvedimui, plius normalus HID raktas | Galimybės ir SDK paskirstymo sąlygos turi būti patikrintos pagal palaikytą modelį |
| Keychron/QMK/VIA | Standartiniai HID raktai ir firmware žemėlapio instrukcijos; žinomi išdėstymai tik su įrodymais | VIA konfigūracijos palaikymas nėra universalus paleidimo laiko makro raktas API |
| Kiti standartizuoti klaviatūros | Fizinis raktų atradimas ir žemėlapis; išskirtinis režimas tik su patvirtintu filtru | Nėra pagamintos modelio specifinės Fn, dial ar savitarpio raktų paramos |

Kiekvienas prietaisas rodo aptiktas, stebimas, žemėlapis per pardavėjo programinę įrangą, gimtojo SDK, vairuotojo išskirtinis, nepasiekiamas arba aparatūros nepagrįstas statusas. Išsaugokite pajėgumo įrodymus su adapterio versijomis ir išbandytomis modelio / firmware identifikatoriais.

## Įsakymas pristatyti
1. Konfiguravimo suderinamumas, pajėgumų sutartys ir patikrinamas vykdymas.
2. Vadovaujamas nustatymas naudojant realius atradimus ir esamus veiksmo vykdytojus.
3. UI automatizavimas ir jo įrašymo / bandymų fiksacija.
4. Plugin hostas, paketo srautas ir pavyzdinis integracija.
5. Pagamintas LAN panelis ir mobilus tikrinimas.
6. Išplėstiniai pardavėjo adapteriai ir išskirtinė klaviatūros pagrindinė integracija, su tikra aparatūros patvirtinimu.

Visi šeši reikalingi. Vairuotojo pasirašymas / licencijavimas, nepasiekiami SDK arba nebuvusi bandymo įranga yra išleidimo durys, o ne priežastys pakeisti mock elgesį ir vadinti darbą baigtas.

## Tikrinimo ir išleidimo kriterijai
Įdiegti esamą branduolį, Windows ir WPF patikrinti ir pridėti orientuotą integracijos testus kiekvienam naujam ribas. Patikrinkite „JSON“ kelionės ir paveldėjimo profilius; išbandykite importuotą vykdomą turinį, tinklo autentifikavimą, atšaukimą, įskiepį ir prietaiso atnaujinimą. Išbandykite darbalaukį ir mobiliuosius įrenginius. Aukštos kokybės Windows skalavimas atskirai. Sukurkite išleidimo dokumentus tik po patikrinimų; atskirti įgyvendintą / išbandytą / ne išbandytos ir išorės blokuotą funkciją išleidimo notatų. Nepakeiskite įdiegtos programos, įdiegkite vairuotojus, pakeiskite tikrąjį klaviatūros žemėlapį ar atvirą prieigą prie tinklo kaip incidentinį testą.

## Įrodymai
- Microsoft Raw Input Semantika: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G kūrėjų SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Intercepcijos saugykla ir priklausomybės sąlygos: https://github.com/oblitum/Interception
- Esamas sandėlio gamintojo inventorius: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Apžvalgos sprendimai
Vartotojas prašoma patvirtinti šį rašytinį dizainą prieš įgyvendinimo planą. Pagrindinė išorinė priklausomybė yra gamybos paruoštas ekskluzivus įvedimo vairuotojas. Rekomenduojamas apimtis išlaiko tai kaip atvirą, o ne tyliai sumažina jį iki Raw Input stebėjimo.

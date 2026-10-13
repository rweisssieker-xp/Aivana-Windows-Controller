<!-- generated-doc locale=lt-LT source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../HANDBOOK.md)

# MAKRIVO — Naudotojo vadovas

Gamintojas: **Aivana**. Dokumentacijos būsena: **2026-10-12**. Šis vadovas aprašo dabartinio darbo medžio realizaciją. Eksperimentinės integracijos ir nuo aparatūros priklausančios funkcijos aiškiai pažymėtos. Istorinės ataskaitos aprašo būseną savo datos metu, bet negarantuoja naujesnės versijos veikimo.

## Dokumentas ir kalbos

Dokumentacijos kalbos parinkiklis leidžia atidaryti visą vadovą, funkcijų sąrašą, trikčių šalinimą, kūrimo pastabas, tyrimus ir istorines bandymų ataskaitas bet kuria iš 27 programos kalbų. Sugeneruoti vertimai parengti mašininiu būdu ir patikrinti struktūriškai; ne visus peržiūrėjo gimtakalbiai. Techniniai identifikatoriai ir vykdomieji pavyzdžiai lieka nepakeisti. Originalus šaltinis prieinamas palyginimui. Išversta licencijavimo analizė yra informacinė ir nepakeičia originalių trečiųjų šalių teisinių sąlygų ar galutinės EULA.

## Įrengimas ir pirmasis paleidimas

Pradinė tikslinė platforma yra Windows 11 x64. Visus platinimo paketo failus ir poaplankius laikykite kartu. Paleiskite `MAKRIVO.exe`. Pirmkodo kopija nėra diegimo programa. Savarankiški paketai turi savo .NET vykdymo aplinką; nuo sistemos priklausomiems paketams reikia atitinkamų .NET 10 Desktop ir ASP.NET vykdymo aplinkų. ONNX ir pagalbinių procesų vykdymo programos turi būti įtrauktos, kad veiktų atitinkamos funkcijos.

1. Paleiskite MAKRIVO ir Settings pasirinkite sąsajos kalbą. Automatinis režimas naudoja Windows kalbą. Klaviatūros išdėstymas nustatomas atskirai.
2. Pasirinkite profilį, tada pridėkite plytelę per veiksmų katalogą su paieška. Suteikite jai atpažįstamą pavadinimą ir pasirinkite veiksmus.
3. Peržiūrėkite veiksmus ir, jei reikia, priskirkite spartųjį klavišą. Venkite derinių, kuriuos jau naudoja Windows arba tikslinė programa.
4. Išsaugokite juodraštį. Neišsaugoti pakeitimai nėra išsaugota konfigūracija.
5. Išbandykite nekenksmingą veiksmą, pavyzdžiui, atidaryti dokumentą ir patikrinti vykdymo atsiliepimus.

Uždarius pagrindinį langą, programa paprastai lieka veikti pranešimų srityje. Dukart spustelėkite jos piktogramą, kad vėl atidarytumėte langą, arba išeikite per piktogramos meniu. Paleidimas kartu su Windows yra pasirenkamas ir iš pradžių išjungtas. Pristabdymo valdiklis sustabdo klavišų susiejimus; piktogramoje rodoma pristabdymo žyma. Vykdomas makrokomandas atšaukite sąsajoje, pranešimų srities meniu arba nustatytu atšaukimo sparčiuoju klavišu; numatytasis yra `Ctrl+Alt+Escape`.

## Įsakymai, veiksmai ir komandos paletė

Plytelės priklauso profiliams ir gali turėti kelis nuoseklius veiksmus. Redaktorius palaiko išsaugojimą/atmetimą, peržiūrą, atšaukimą/pakartojimą ir veiksmų tvarkos keitimą. Kataloge yra programų, dokumentų, URL, teksto, iškarpinės, sparčiųjų klavišų, medijos, garso, langų, sistemos veiksmų ir palaikomų integracijų ruošiniai. Parametrų reikalaujantis ruošinys atidaromas kaip juodraštis.

Atidarykite paletę su `Ctrl+K`, kai MAKRIVO turi įvesties fokusą. Ieškokite išsaugotų veiksmų, ruošinių, profilių perjungimo ir įrankių. Rodyklių klavišai parenka; Enter aktyvina; `Ctrl+Enter` redaguoja palaikomą elementą; Escape uždaro. Kai kurios paruoštos komandos vykdomos iš karto, o sukonfigūruotus veiksmus reikia peržiūrėti. Galima nustatyti atskirą visuotinį paletės spartųjį klavišą; naujose konfigūracijose naudojamas `Ctrl+Shift+Space`.

Teksto ir iškarpinės veiksmai palaiko `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ir `{{user}}`. Datos formatas yra `yyyy-MM-dd`; laiko formatas yra `HH:mm`. Nežinomi kintamieji lieka nepakeisti. Iškarpinė skaitoma tik paprašius. Iškarpinės veiksmas įrašo tekstą, bet jo automatiškai neįklijuoja.

Iš sąsajos paleisti veiksmai gali būti nukreipti į paskutinę išorinę programą. Jei tikslas dingsta arba Windows blokuoja aktyvinimą pirmame plane, aktyvinkite jį rankiniu būdu ir bandykite dar kartą. Sėkmingas klavišų įvesties perdavimas neįrodo, kad tikslinė programa atliko užduotį.

## Profiliai ir darbo vieta

Profiliai gali atitikti proceso pavadinimą ir lango antraštės dalį. Jei nustatyti abu, turi atitikti abu. Kai atitinka keli profiliai, pasirinkimą lemia jų tvarka; numatytasis profilis naudojamas, kai kitų atitikmenų nėra. Automatinį parinkimą reikia aiškiai įjungti. Lock Profile neleidžia automatiškai keisti profilio dabartiniame seanse.

Atidarykite Workplace Setup per Tools arba paletę. Pasirinkite programas, dokumentus, svetaines, garso/ekrano tikslą arba OBS sceną. Asistentas parengia darbo vietos, susitikimo, transliacijos, tyrimo arba projekto profilius. Prieš išsaugodami patikrinkite sugeneruotus veiksmus. Sąranka nevykdo parengtų veiksmų.

## Makros, sąlygos ir įrašai

Makrokomandos vykdo veiksmus eilės tvarka ir palaiko laukimą, kartojimą bei atšaukimą. Bendras kartojimo limitas yra 100. Prieš vykdydami veiksmus, kurie įveda tekstą į kitą programą arba keičia sistemos būseną, naudokite peržiūrą.

Wait for Target Window priima procesą, antraštės poeilutę ir laukimo limitą nuo 1 iki 600000 ms. Būtinas bent vienas filtras. Atitinkantis matomas langas turi reaguoti ir tapti pirmojo plano langu, kad būtų tęsiami kiti veiksmai. Parengtas langas neįrodo, kad dokumentas ar paslauga baigė įkėlimą.

Darbo eigos redaktorius prideda failo buvimo, veikiančio proceso ir lango antraštės sąlygas. Netenkinama sąlyga praleidžia veiksmą ir gali praleisti papildomus tolesnius veiksmus. Alternatyviai šakai išreikšti paneikite sąlygą. Sąmoningai pasirinkite sustabdymą arba tęsimą įvykus klaidai; tęsiant gali būti netenkinamos vėlesnių veiksmų prielaidos.

Veiksmų įrašymo priemonė fiksuoja sparčiųjų klavišų derinius ir laukimą, o ne įprastą teksto rašymą. Ji palaiko derinius su modifikavimo klavišais ir funkcinius klavišus. Escape sustabdo įrašymą. Peržiūrėkite sąrašą ir priimkite jį kaip neišsaugotą plytelės juodraštį, tada aiškiai išsaugokite. Sąsajos paspaudimų įrašymas yra atskiras toliau aprašytas įrankis.

## Pagrindiniai ženklai, gestai ir sluoksniai

Klaviatūros puslapyje pateikiamos aptiktos sąsajos ir pavyzdiniai išdėstymai. Viena fizinė klaviatūra gali pateikti kelias sąsajas. Pasirinkite tinkamą kalbą ir geometriją; vien produkto pavadinimas nenustato ISO ar ANSI išdėstymo.

Standartiniai visuotiniai spartieji klavišai galioja visoms klaviatūroms. Įrenginiui būdingi Raw Input susiejimai stebi konkretų įrenginį ir skenavimo kodą, įskaitant palaikomus funkcinius ir medijos klavišus. **Raw Input neslopina įprastos Windows įvesties. Išskirtinai makrokomandoms naudojama antroji klaviatūra nėra parengta produkciniam naudojimui.** Vis dar reikia pasirašytos, licencijuotos ir išbandytos filtro tvarkyklės sistemos; MAKRIVO tokios tvarkyklės neįdiegia.

Palaikomi specialiųjų klavišų susiejimai gali naudoti tiesioginį paspaudimą, trumpą paspaudimą, laikymą arba dvigubą paspaudimą. Laikomo sluoksnio klavišai laikinai parenka kitą susiejimą ir atleidus atkuria pagrindinį sluoksnį; sluoksnio parinkimo klavišai nepaleidžia savo makrokomandos. Laikymo slenksčiai yra nuo 150 iki 5000 ms; dvigubo paspaudimo intervalai – nuo 150 iki 1000 ms. Šie susiejimai skiriasi nuo įprastų visuotinių sparčiųjų klavišų.

Specialiųjų klavišų vedlys aptinka suderinamus klavišus, fiksavimo metu nevykdydamas jiems priskirtų veiksmų. Diagnostika paaiškina nepasiekiamus įrenginius, pristabdytus susiejimus, profilio neatitikimus ir konfliktus. Perdanga rodo pasirinktą išdėstymą ir susiejimus; numatytasis jos laikomas spartusis klavišas yra `Ctrl+Alt+F1`.

| Integracija | Reikalavimai ir apribojimai |
| --- | --- |
| Corsair | Veikiantis iCUE, SDK prieiga ir suderinamas modelis. G1–G6 ir palaikomos RGB funkcijos; gamintojo Fn, ratuko, užrakto ir profilio valdikliai paprastai nepateikiami per G-key API. |
| Logitech G | Originalioms vietinėms x64 G-key/LED SDK bibliotekoms reikia aiškaus patvirtinimo. G-key įvykiai vienareikšmiškai neatskiria kelių vienodų fizinių klaviatūrų. |
| Razer | Chroma RGB ir standartinė įvestis; prireikus susiekite gamintojo valdiklius per Synapse. |
| SteelSeries | GameSense RGB ir standartinė įvestis; prireikus susiekite gamintojo valdiklius per GG. |
| Wooting | Suderinamas originalus vietinis x64 SDK analoginėms reikšmėms ir slenksčiams; įprasta HID įvestis lieka aktyvi. |
| Kitos klaviatūros | Standartiniai spartieji klavišai arba palaikomi fizinių skenavimo kodų susiejimai. Fn ir gamintojo valdikliai priklauso nuo modelio bei gamintojo konfigūracijos. |

Gamintojų tyrimai aprašo dokumentuotas galimybes, o ne įgyvendintą ar išbandytą suderinamumą su kiekvienu išvardytu įrenginiu. Naujiems adapteriams vis dar reikia tikros aparatūros priėmimo bandymų. Neatsisiųskite SDK dvejetainių failų iš nepatikimų šaltinių; SDK platinimo teisės yra atskiros nuo techninio suderinamumo.

## Audio, ekranai ir OBS

Garso veiksmai nustato garsumą nuo 0 iki 100 procentų ir nutildo/įjungia garsą/perjungia nutildymą palaikomuose išvesties arba mikrofono galiniuose taškuose. Pasirinkite konkretų galinį tašką arba Windows numatytąjį. Jei išsaugoto tikslo nėra, pateikiama klaida, užuot tyliai valdžius kitą įrenginį. Prijungę iš naujo naudokite Refresh Devices. Galinio taško valdymas nereiškia nepriklausomo kiekvienos programos garso maišytuvo palaikymo.

Ekrano veiksmai padidina/sumažina ryškumą 10 procentinių punktų arba nustato vertę nuo 0 iki 100. Vidiniai ekranai naudoja palaikomas WMI sąsajas; išoriniams monitoriams reikia DDC/CI palaikymo ir paprastai jo įjungimo monitoriaus meniu. Dokai, adapteriai ir kabeliai gali blokuoti valdymą. Pasirinkite visus palaikomus ekranus arba vieną monitorių. Prijungus iš naujo gali pasikeisti galimi identifikatoriai; peržiūrėkite išsaugotą tikslą. Trūkstami rodmenys rodomi kaip nepasiekiami, o ne kaip išmatuotos vertės.

OBS reikia įjungto WebSocket v5 per OBS Tools. Nustatykite galinį tašką, išbandykite ryšį ir prieš išsaugodami įkelkite scenų/įvesčių pavadinimus. Palaikomi scenų perjungimas, transliacijos/įrašymo pradėjimas/sustabdymas/perjungimas, įrašymo pristabdymas/tęsimas ir įvesties nutildymas. Slaptažodžius galima saugoti Windows Credential Manager ir jie neįtraukiami į konfigūracijos eksportus. Sėkminga OBS komanda nepatvirtina visų tolesnių transliacijos poveikių.

## Rezultatų tikrinimas ir vykdymo istorija

Tinkamiems veiksmams įjunkite Verify Result. Programa atskiria išankstines patikras, patvirtintus poveikius, nesėkmes, atšaukimą ir poveikius, kurių neįmanoma patikrinti. Proceso buvimas nereiškia programos parengties. Santykiniams pakeitimams, spartiesiems klavišams ir teksto įvedimui negalima bendrai patvirtinti sėkmės. Veiksmui nepavykus peržiūrėkite veiklos žurnalą ir tikrinimo istoriją. Prieš iš naujo vykdydami visą makrokomandą ištaisykite konfigūracijos klaidas.

## UI automatizavimas ir spustelėjimas įrašymas

Pridėkite UI Automation veiksmą, pasirinkite operaciją ir po rodomo delsos laiko užfiksuokite objektą po pelės žymikliu. Vien fiksavimas nieko nevykdo. Patikrinkite parinkiklį ir reikalaukite vienintelio atitikmens. Palaikomos operacijos: `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ir `WaitState`. Slaptažodžių laukai, išjungti valdikliai ir nevienareikšmiai parinkikliai atmetami.

Sąsajos automatizavimas veikia ribotame pagalbiniame procese. Jis priklauso nuo tikslinės programos Windows pritaikymo neįgaliesiems palaikymo ir leidimų. Paspaudimų įrašymo priemonė fiksuoja palaikomus veiksmus ir pauzes, bet ne bendrą teksto įvedimą. Prieš priimdami peržiūrėkite ir patikrinkite juodraštį. Išorinės programos sąsajos pakeitimai gali padaryti parinkiklius netinkamus. Proceso izoliavimas riboja strigčių poveikį ir palaiko atšaukimą, tačiau nėra saugumo smėlio dėžė.

## Pluginas

Atidarykite Extensions ir prieš importuodami patikrinkite paketą. Tikrinama paketo struktūra, manifestas ir maišos reikšmės. Importuotas plėtinys iš pradžių yra išjungtas. Perskaitykite deklaruotus veiksmus ir prieš įjungdami aiškiai patvirtinkite pasitikėjimą leidėju. Papildiniai veikia atskirame procese su jūsų naudotojo teisėmis; šis procesas **nėra smėlio dėžė**. Diekite tik patikimus paketus.

Tvarkyklė palaiko eksportavimą, išjungimą, pašalinimą ir ankstesnės versijos atkūrimą/atmetimą. Trūkstamos priklausomybės blokuoja iškvietimą. Pavyzdinis paketas pateikia atgalinio skaičiavimo ir vietinės būsenos veiksmus. Palaikoma schema yra dokumentuotas JSON-Schema poaibis, ne visos galimos JSON-Schema funkcijos. Viešos papildinių prekyvietės nėra.

## Mobilusis panelis

1. Atidarykite Mobile Panel, pasirinkite vietinį IP ir aiškiai nurodykite bendrinamus profilius. Numatytasis loopback adresas pasiekiamas tik pačiame kompiuteryje; telefonui ar planšetiniam kompiuteriui pasirinkite tinkamą LAN adresą.
2. Paleiskite skydelį ir perskaitykite jo HTTPS adresą, sertifikato kontrolinį atspaudą bei vienkartinį šešių skaitmenų susiejimo kodą.
3. Atidarykite adresą įrenginyje tame pačiame patikimame tinkle. Prieš pasitikėdami vietiniu sertifikatu palyginkite SHA-256 kontrolinį atspaudą patikimu kanalu. Programa automatiškai neįdiegia patikimo sertifikato.
4. Susiekite įrenginį naudodami rodomą kodą ir išbandykite vieną nekenksmingą plytelę. LAN prieigai gali reikėti rankinio Windows Firewall leidimo. Neatidarykite prievado internetui.
5. Baigę sustabdykite skydelį arba programoje atšaukite susietų klientų prieigą.

Serverio privačiuosius raktus saugo Windows DPAPI; klientų prisijungimo duomenys saugomi kaip maišos reikšmės. Pakeitus IP arba atnaujinus sertifikatą gali reikėti iš naujo patvirtinti pasitikėjimą. Skydelis rodo veiksmų ir tikrinimo būsenas, o ne visus galimus tiesioginius jutiklius. Naršyklės išdėstymo ir vietinio HTTPS patikros nepakeičia tikrų telefonų/planšetinių kompiuterių LAN priėmimo bandymų.

## Vietos asistentų susitikimas

Šis pasirenkamas eksperimentinis asistentas pagal numatytuosius nustatymus išjungtas. Atidarykite jį per Tools, aiškiai įjunkite, pasirinkite vykdymo aplinką/modelį ir pridėkite tik tuos išteklius, kuriuos norite įtraukti. Nurodykite tikslą, sukurkite juodraštį, patikrinkite neišspręstas užklausas ir perkelkite rezultatą į įprastą redaktorių. Niekas nevykdoma automatiškai; išsaugojimą ir paleidimą reikia atlikti aiškiai. Automatinio perėjimo į kitą vykdymo aplinką nėra.

| Laiko bėgimas | Įdiegti |
| --- | --- |
| Ollama | Atskiras vietinis aptarnavimas ir vietiniu būdu įdiegtas modelis `127.0.0.1:11434`. Paskelbti nuotoliniai / debesų modeliai yra atmesti. |
| LM Studio | Vietinis serveris su vietiniu įkrovimu ir JSON schemos palaikymu `127.0.0.1:1234`. Neleiskite nuotolinio maršruto vietos apdorojimui. |
| llama.cpp | Vietinis `llama-server` ir suderinamas modelis su JSON schemos palaikymu `127.0.0.1:8080`. |
| ONNX Veikimo laikas GenAI CPU | Pasitikima vietos GenAI teksto modelio aplanką; pasirinkite `genai_config.json`. „Inference“ naudoja bendradarbiavimą be HTTP. |

HTTP klientai naudoja tik loopback ryšius, nenaudoja tarpinių serverių ir neseka peradresavimų. MAKRIVO negali patikimai nustatyti, ar suderinamas vietinis serveris viduje kreipiasi į nuotolinę paslaugą. Autentifikuojamų vietinių serverių šioje versijoje negalima konfigūruoti. Modelių svoriai, Python ir vietiniai išvedimo serveriai nėra automatiškai atsisiunčiami ar įdiegiami.

ONNX paketams reikia `tokenizer.json` ir tinkamo pokalbio šablono. Faile `tokenizer_config.json` palaikoma eilutė `chat_template`, žodynas su eilute `default` arba vienareikšmis pavadintų/numatytųjų šablonų sąrašas. Jei šablono nėra arba jis null, kaip atsarginis naudojamas `chat_template.jinja`. Tušti, nevienareikšmiai, per dideli arba netinkamo tipo šablonai atmetami. Bet kokie ONNX failai, įterpinių/regos modeliai ir vien GPU paketai negali pakeisti suderinamų GenAI teksto modelių. Įtrauktos GenAI vykdymo aplinkos versija yra 0.17.1, CPU. Naudokite tik patikimus modelius; procesų atskyrimas nėra smėlio dėžė.

Asistentas palaiko pasirinktas programas be argumentų, pasirinktus dokumentus ir svetaines bei mikrofono nutildymą aiškiai paprašius. Jis nekuria pranešimų, susitikimų, savavališkų scenarijų, laisvų paspaudimų, atsisiuntimų ar ausinių perjungimo. Tikslas ir išteklių pavadinimai siunčiami pasirinktai vietinei vykdymo aplinkai; išteklių keliai ir failų turinys atskirai nepateikiami. Naudotojo įvestame tiksle gali būti asmens duomenų. Už vietinės vykdymo aplinkos tinklo elgesį atsako naudotojas.

ONNX užklausos laiko limitas yra 90 sekundžių, įskaitant modelio įkėlimą; leidžiama iki 3072 užklausos įvesties žetonų ir 1024 sugeneruotų žetonų, taip pat taikomas modelio konteksto limitas. Netaisyklingas/nukirstas JSON ir nežinomi išteklių ID atmetami. Užklausai baigti ją atšaukite arba uždarykite langą. Vykdymo aplinkos bandymas su atsitiktiniu modeliu patvirtina vykdymą, bet ne naudingą planavimo kokybę ar laiko taupymą. Vietiniuose eksperimentų žurnaluose yra įvykių identifikatoriai ir laiko matavimai, o ne tikslo tekstas ar dokumentų turinys; jie nėra automatiškai įkeliami.

Vėlesnis mažo Instruct modelio vertinimas per keturis vokiečių/anglų bandymus nesukūrė nė vieno galiojančio plano. Savasis išvedimas veikė, bet modelis neatitiko griežtos planavimo sutarties. Tai neigiamas kokybės rezultatas, o ne modelio rekomendacija ar asistento naudingumo įrodymas. Tiksli modelio revizija ir ribos pateiktos datuotoje vietinės vykdymo aplinkos bandymų ataskaitoje.

## Importuoti, eksportuoti ir sinchronizuoti

Prieš pakeisdami konfigūraciją peržiūrėkite importo peržiūrą. Importuoti veiksmai iš pradžių išjungti, o perkeliamasis sinchronizavimas pašalina aparatūrai būdingus susiejimus. Patikrinę iš naujo priskirkite įrenginius ir sąmoningai įjunkite veiksmus. Prieš dalydamiesi eksportu peržiūrėkite tekstą/veiksmų turinį; slaptažodžių neįtraukimas nereiškia, kad visas naudotojo sukurtų veiksmų turinys nėra konfidencialus.

Profilių sinchronizavimas keičiasi failais pasirinktame aplanke, kurį pasirinktinai gali valdyti esamas debesijos klientas. MAKRIVO nesuteikia debesijos paskyros ir nesaugo debesijos prisijungimo duomenų. Revizijos apsaugo nuo tylaus perrašymo. Veiksmų turinio įtraukimas yra aiškus pasirinkimas. Prieš pakeičiamąjį importą sukurkite konfigūracijos atsarginę kopiją. Numatytoji konfigūracija yra `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; istorinis aplanko pavadinimas paliktas suderinamumui. Prieš rankiniu būdu kopijuodami/atkurdami uždarykite programą. Išsaugotus SDK patvirtinimus ir OS apsaugotus prisijungimo duomenis kitame kompiuteryje gali reikėti sukurti iš naujo.

## Problemos sprendimas

| Simptomai | Ką patikrinti |
| --- | --- |
| Trumpas nieko nedaro | Taikymas veikia, susietas ne pertrauktas, teisingas aktyvus profilis, išsaugotas / įgalintas plokštė, trumpalaikiai konfliktai ir prieinamas tikslas. |
| Tikslinis langas negali būti aktyvus | Manualinis aktyvavimas. Windows priekio apribojimai ir privilegijos skirtumai gali blokuoti automatizavimą. |
| Specialus raktas nerastas | Pardavėjo aptarnavimas / SDK / modelio parama; konfigūruokite nuosavybės kontrolę, kad galėtumėte išleisti palaikytą standartinį raktą. |
| Antroji klaviatūra taip pat paprastai | Tikimasi, kad Raw Input Išskirtinis sustabdymas nėra taikomas gamybai. |
| Audio ar ryškumas nepasiekiamas | Atnaujinkite prietaisus; patikrinkite pasirinktą tapatybę, galutinę sesiją, palaikymą WMI / DDC/CI ir dubens / kabelio apribojimus. |
| OBS Offline | WebSocket v5 įjungtas, teisingas galutinis taškas / slaptažodis, prieinamas paslauga ir galiojantys scenos / įvesties pavadinimai. |
| Automatizavimas nesėkmingas | Įtrauktas bendradarbiavimas, unikalus galiojantis pasirinkėjas, prieinamas įgalintas tikslas, palaikomas kontrolės ir leidimai. Atšaukti pereinamojo laikotarpio tik peržiūrėjus tikslą. |
| Mobilieji telefonai nepasiekiami | LAN vietoj atgalinio adreso, to paties tinklo, serverio veikimo, patvirtintų profilių, sertifikavimo pirštų atspaudų / pasitikėjimo ir rankinių ugnies sienos leidimų. |
| Pluginas nesėkmingas | Paketo vientisumas, aiškus pasitikėjimas / leidimas, priklausomybės, protokolų suderinamumas ir vykdymo įrašas. |
| Prašymas nesėkmingas | Tinkamai parinktas vietinis darbo laikas, paruoštas aptarnavimas / modelis, palaikoma JSON schema, patikima suderinama modelių aplinka ir prašymo ribos. |
| Layout arba tekstas atrodo neteisingas | Atskiriate žiniatinklio kalbą nuo klaviatūros geometrijos; peržiūrėkite langą, patikrinkite ekrano skalę ir praneškite tikslią puslapį / kalbą / statybą. |

Naudingoje klaidos ataskaitoje nurodykite Windows versiją, programos versiją/komitą, kalbą, ekrano mastelį, aparatūros modelį, minimalią veiksmų seką ir atitinkamą nuasmenintą žurnalą. Prieš skelbdami pašalinkite asmeninius kelius, veiksmų turinį ir prisijungimo duomenis.

## Statybos, bandymų ir pakavimo

Naudokite SDK, užfiksuotą `global.json`. Iš šaknų atsargos:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integracijos testai gali atidaryti izoliuotus langus ir laikinai nustatyti/atlaisvinti palaikomus gamintojų susiejimus. Vykdykite juos, kai kitas valdiklis nevaldo šių susiejimų. Numatytosios ryškumo/garso patikros tik skaito; aiškiai pasirenkami aparatūros būsenos pakeitimo ir atkūrimo testai yra atskiri. Imituoti protokolai, sugeneruotos ekrano kopijos ir atsitiktiniai išvedimo grafai negali būti pateikiami kaip tikros aparatūros/modelio kokybės priėmimas.

Visas publikavimo scenarijus surenka pagalbinių procesų programas, mobiliuosius išteklius, pavyzdinį papildinį, ONNX vykdymo aplinką ir turimus priklausomybių pranešimus. Išsaugokite poaplankius. Sėkmingas surinkimas nėra pasirašyta diegimo programa ar teisinis leidimo patvirtinimas. Pirmkodas ir versijos gali vystytis atskirai; palyginkite dokumentacijos būseną ir komitą su įdiegta versija.

## Licencija, privatumas ir išleidimo būklė

Numatytas komercinis modelis yra vienkartinis pirkimas su neribotos trukmės įsigytos pagrindinės versijos naudojimu; galutinė kaina, leidžiamas įrenginių skaičius ir naujinimo sąlygos turi būti nustatytos tikrosiose pardavimo sąlygose. Šis vadovas nepatvirtina veikiančios produkcinės pirkimo/aktyvinimo sistemos. Kasdieniam valdymui debesijos paskyra neprivaloma.

Neplatinkite nuosavybinių SDK dvejetainių failų be įrodyto leidimo. Išsaugokite kiekvienos platinamos priklausomybės, įskaitant .NET ir ONNX paketus, licencijas ir trečiųjų šalių pranešimus. Modelių svoriams taikomos atskiros sąlygos. Datuotas licencijų auditas atliktas prieš pridedant kai kurias naujas priklausomybes ir nėra šių papildymų patvirtinimas. Vieša saugykla savaime nesuteikia bendros atvirojo kodo licencijos.

Tikra gamintojų aparatūra, telefonų/planšetinių kompiuterių LAN veikimas, perėjimai tarp skirtingų monitorių/DPI, ilgalaikis naudojimas, išsami kalbinė peržiūra, išskirtinės tvarkyklės palaikymas ir naudinga AI planavimo kokybė vis dar reikalauja atskiro priėmimo. Niekada nespręskite apie patvirtinimą vien pagal funkcijos buvimą.

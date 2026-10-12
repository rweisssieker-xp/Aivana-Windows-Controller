<!-- generated-doc locale=lt-LT source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../HANDBOOK.md)

# MAKRIVO — Naudotojo vadovas

Gamintojas: **Aivana**. Dokumentinis snapshot: **2026-10-12**. Šis vadovas apibūdina dabartinę darbo medžio įgyvendinimą. eksperimentinės integracijos ir įrangos funkcijos yra aiškiai identifikuojamos. Istoriniai pranešimai apibūdino jų datą, o ne naujos statybos garantiją.

## Dokumentas ir kalbos

Naudokite dokumentacijos kalbos pasirinktuvą, kad atidarytumėte visą vadybą, funkcijų inventorą, problemų sprendimą, plėtros įrašus, tyrimus ir istorinius bandymų pranešimus bet kurioje iš 27 taikomųjų kalbų. Gaminti vertimai yra mašinų pagalba ir turi struktūrinius patikrinimus; ne visi jie gavo gimtojo kalbos peržiūrą. Techniniai identifikatoriai ir vykdomieji pavyzdžiai lieka nekeičiami. Originalus šaltinis yra palyginamas. Vertimo licencijavimo analizė yra informatyvi ir nepakeičia originalių trečiųjų šalių teisinės sąlygos ar galutinį EULA.

## Įrengimas ir pirmasis paleidimas

Pradinis tikslas yra Windows 11 x64. Laikykite visus failus ir subdirektorių kartu. Išleisti `MAKRIVO.exe` Šaltinis patikrinimas nėra įrenginys. Savarankiškai turimi paketai apima savo .NET paleidimo laiką; rėmuo priklausantys paketai reikalauja atitinkamų .NET 10 darbalaukės ir ASP.NET paleidimų laikotarpių. ONNX ir bendradarbiaujantys hostai turi būti įtraukti į jų atitinkamas funkcijas.

1. Pradėkite MAKRIVO ir pasirinkite sąsajos kalbą nustatymuose. Automatinis sekimas Windows Keyboard layout yra atskiras nustatymas.
2. Pasirinkite profilį, tada įdėkite plokštelę per paieškos veiksmų katalogą. Duokite jam pripažįstamą vardą ir pasirinkite jo veiksmus.
3. Patikrinkite žingsnius ir, jei reikia, paskirkite trumpalaikį. Venkite kombinacijų, kurias jau naudoja Windows arba jūsų tikslinė programa.
4. Išsaugokite projektą. Nesugrįžtamas pokyčiai nėra išsaugotas konfigūracija.
5. Išbandykite nekenksmingą veiksmą, pavyzdžiui, atidaryti dokumentą ir patikrinti vykdymo atsiliepimus.

Pagrindinio lango uždarymas paprastai palieka paraišką pranešimo srityje. Dvigubai paspauskite jo tray ikoną, kad atsidarytumėte arba naudokite tray meniu. Pradedant nuo Windows yra pasirinktinis ir iš pradžių neįmanomas. Pertraukos kontrolė sustabdo ryšius; ikona rodo pertrauką. Atšaukite paleisti makrus per IU, tray arba konfigūruotą atšaukimo trumpą; numatyta `Ctrl+Alt+Escape`.

## Įsakymai, veiksmai ir komandos paletė

Tiltai priklauso profiliai ir gali būti keli užsakomi žingsniai. Redaktorius palaiko išsaugoti / atsisakyti, peržiūrėti, undo /redo ir pakartotinai. Katalogas teikia programų, dokumentų, URL, teksto, klipų, sutrumpinimų, žiniasklaidos, garso, langų, sistemos veiksmų ir palaikytų integracijų pranašumus. Pirmasis nustatymas, reikalaujantis parametrų, atidaromas kaip projektas.

Atidarykite paletę su `Ctrl+K` tuo tarpu MAKRIVO turi dėmesį. Ieškoti išsaugotų veiksmų, preliminarių, profilio keitimo ir įrankių. Rinkti raktus; Įveskite aktyvuoja; `Ctrl+Enter` redaguoja palaikytą elementą; pabėgti uždaroma. Kai kurie paruošti įsakymai atliekami nedelsiant, o konfigūruoti veiksmai reikalauja peržiūrėti. Galima konfigūruoti atskirą pasaulinę paletę; naudojamos naujos konfigūracijos `Ctrl+Shift+Space`.

Teksto ir klipų palaikymas `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ir `{{user}}`. Datų formavimas `yyyy-MM-dd`; Laikas formatuojamas `HH:mm`. Nežinomos kintamosios lieka nepasikeičiamos. „Clipboard“ skaitymo procesas vyksta tik tada, kai paprašoma. „Clickboard“ veiksmas rašo tekstą be to, kad jis automatiškai atitiktų.

Veiksmai, pradėti iš IU, gali būti nukreipti į paskutinį išorinį programą. Jei tikslas išnyksta arba Windows blokuoja aktivaciją, rankiniu būdu aktyvuokite tikslą ir atsitraukite. Sėkmingas raktų pristatymas neparodo, kad tikslas baigė savo užduotį.

## Profiliai ir darbo vieta

Profiliai gali atitikti proceso pavadinimą ir lango pavadinimo dalį. Kai abu yra sudarytos, abu turi atitikti. Profilio tvarka išsprendžia konkurencinius rungtynes; numatytas profilis yra atgal. Automatinis atrankos pasirinkimas. Lock Profile neleidžia automatiškai keisti dabartinę sesiją.

Atviros darbo vietos nustatymas iš įrankių arba paletės. Pasirinkite programas, dokumentus, svetaines, garso / ekrano tikslą arba OBS scenos. Asistentas rengia darbo vietą, susitikimus, streiką, mokslinius tyrimus ar projektus. Įsitikinkite, kad prieš išsaugojdami žingsnius. Įrenginys neveikia parengtų veiksmų.

## Makros, sąlygos ir įrašai

"Macros" vykdo savo žingsnius ir palaiko laukti, kartojimą ir atšaukimą. Bendras pakartotinis ribas yra 100. Naudokite „Preview“ prieš paleidžiant veiksmus, kurias įveskite į kitą programą arba pakeiskite sistemos būseną.

Laukiama, kad tikslinis langas priima procesą, pavadinimo substrą ir laiko išleidimą nuo 1 iki 600,000 ms. Reikia bent vieno filtro. Atitinkamas matomas langas turi atsiliepti ir tapti priekabiavimu, kol tolimesni žingsniai tęsiasi. Pasirengęs langas neįrodo, kad dokumentas ar paslauga baigėsi.

Darbo srauto redaktorius prideda failų egzistavimą, procesą ir lango pavadinimo sąlygas. Neteisingas būklė laipina žingsnį ir gali praeiti papildomų žingsnių. Atsisakyti sąlygos, kad būtų išreikštas alternatyvus sklypas. Išsirinkite „stop-on-error“ arba „continu-on- error“ sąmoningai; tęsinys gali palikti vėlesnius žingsnius be jų numatytų sąlygų.

Veiksmų įrašytojas užfiksuoja trumpalaikius kodus ir laukia, o ne įprastą rašymą. Jis palaiko modifikuojančius trumpalaikius ir funkcinius raktus. Pabėgimas nustoja įrašyti. Persvarstykite sąrašą ir priimkite jį kaip nepagrįstą plokštelės projektą, tada aiškiai išsaugokite. UI spustelėjimo įrašymas yra atskiras įrankis, aprašytas žemiau.

## Pagrindiniai ženklai, gestai ir sluoksniai

Klaviatūros puslapis apibūdina aptiktas sąsajas ir nuorodų išdėstymus. Vienas fizinis klaviatūra gali atskleisti keletą sąsajų. Pasirinkite teisingą kalbą ir geometriją; vien produktų pavadinimas nepaaiškina ISO ar ANSI aprašymo.

Standartiniai pasauliniai raktai taikomi per klaviatūras. Raw Input ryšiai stebėti tam tikrą įrenginį ir skenavimo kodą, įskaitant palaikomą funkciją ir žiniasklaidos raktus. **Raw Input Neįmanoma slopinti įprasto Windows Išskirtinė antroji klaviatūra nėra paruošta.** Pasirašytas, licencijuotas ir išbandytas filtro vairuotojo rinkinys vis dar reikalingas; nė vienas nėra įdiegtas MAKRIVO.

Palaikomi specialūs raktiniai ryšiai gali naudoti nedelsiant spausdinti, paspausti, laikyti arba dvigubai paspausti. įdėtojo sluoksnio raktai laikinai pasirinkti kitą žemėlapį ir atkurti bazinį sluoksnį; įdėtasis raktas neveikia savo makro. 150 Į 5,000 MS; dvigubas langas iš 150 Į 1,000 MS. Šie žemėlapiai skiriasi nuo įprastų pasaulinių šaldiklių.

Specialus raktas stebuklas atpažįsta suderinamus raktus be jų paskirtų veiksmų užfiksuojant. Diagnozė paaiškina neįmanomas prietaisus, sustojimus, profilio sutrikimus ir konfliktus. Viršutinė plokštė rodo pasirinktą išdėstymą ir jungiamumą; jo numatyta trumparegystė yra `Ctrl+Alt+F1`.

| Integracija | Reikalavimai ir apribojimai |
| --- | --- |
| Corsair | Veiksmas iCUE, prieiga prie SDK ir suderinamas modelis. G1–G6 ir palaikomos RGB funkcijos; savarankiškas Fn, ratai, užraktai ir profilių kontrolės paprastai nėra išdėstytos G raktų API. |
| Logitech G | Originalūs x64 G raktas / LED SDK biblioteka reikalauja aiškios patvirtinimo. G raktų renginiai nevienodai identifikuoja keletą identiškų fizinių klaviatūrų. |
| Razer | Chroma RGB ir standartinis įvedimas; žemėlapio nuosavybės kontrolės Synapse kai reikia. |
| SteelSeries | GameSense RGB ir standartinis įvedimas; žemėlapio nuosavybės kontrolės GG, kai reikia. |
| Wooting | Originalus x64 SDK suderinamas su analoginėmis vertėmis ir ribomis; normalus HID įvedimas išlieka aktyvus. |
| Kitos klaviatūros | Standartiniai šildytuvai arba palaikomi fiziniai kodai. Fn ir nuosavybės kontrolės priklauso nuo modelio ir pardavėjo konfigūracijos. |

Gamintojo tyrimai apibūdina įrodytuosius gebėjimus, kurių suderinamumas su kiekvienu išvardytu įrenginiu nebuvo įgyvendintas ar išbandytas. Nauji adapteriai vis dar reikalauja realaus priėmimo. Negalima atsisiųsti SDK binarų iš nepatikimų šaltinių; SDK platinimo teisės yra atskiriamos nuo techninės suderinamumo.

## Audio, ekranai ir OBS

Garso žingsniai nustato tūrį nuo 0 iki 100 procentų ir mute / unmute / Toggle palaikomas kilmės arba mikrofonų galutiniai taškai. Pasirinkite konkrečią tašką arba naudokite numatytuosius Windows. Prarastas tikslas sukelia klaidą, o ne tyliai kontroliuoja kitą įrenginį. Atnaujinkite įrenginius po atnaujinimo. Galutinio taško kontrolė nėra nepriklausomos per paraišką mišrio paramos reikalavimas.

Rodyti veiksmus padidinti / sumažinti ryškumą 10 procentinių taškų arba nustatyti vertę nuo 0 iki 100. Vidiniai ekranai naudoja palaikytus WMI sąsajas; išoriniams monitoriams reikalinga DDC/CI parama ir paprastai monitorių meniu nustatymas. Dėžutės, adapteriai ir kabeliai gali blokuoti kontrolę. Pasirinkite visus palaikomus ekranus arba vieną monitorių. Atnaujinimas gali pakeisti turimas tapatybes; peržiūrėti išsaugotą tikslą. Trūksta skaitymo rodoma kaip neįmanoma, o ne matuojamos vertės.

OBS reikalauja WebSocket v5 įdiegti OBS įrankius. Nustatykite galutinį tašką, išbandykite ryšį ir įkrovimo scenos / įvedimo pavadinimus prieš išsaugodami. Palaikomi veiksmai apima scenos keitimą, srauto / įrašymo pradžią / sustojimą / atnaujinimą, atsipalaidavimo / atnaujinimo įrašymą ir įvedimo mute. Slapukai gali būti saugomi Windows Įtikinimo vadybininkas ir yra išjungti iš konfigūracijos eksporto. Sėkmingas OBS komandos nenustato kiekvieno nuotolinio srauto poveikio.

## Rezultatų tikrinimas ir vykdymo istorija

Galite patikrinti rezultatus tinkamais veiksmais. Programoje atskiriami išankstiniai patikrinimai, patvirtinti poveikiai, gedimai, atšaukimas ir poveikiai, kurių neįmanoma patikrinti. Procedūros egzistavimas yra ne paraiškos pasirengimas. Santykiniai pakeitimai, trumpos ir teksto įrašas negali gauti sėkmės formos. Atkreipkite dėmesį į veiklos logą ir tikrinimo istoriją, kai veiksmas nesėkmingas. Konfiguravimo klaidos turi būti ištaisyta prieš atnaujinant visą makrą.

## UI automatizavimas ir spustelėjimas įrašymas

Pridėkite UI automatizavimo žingsnį, pasirinkite operaciją ir užfiksuokite tikslą po rodomo vėlavimo. pačios užfiksuojimas nieko neveikia. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ir `WaitState`. Slapukų laukai, neįgalūs kontrolieriai ir abejingi pasirinkėjai yra atmesti.

UI automatizavimas vyksta ribotame partnerio procese. Tai priklauso nuo tikslinės programos Windows prieinamumo paramos ir leidimų. Spustelėjimo įrašas užfiksuoja remiamas veiksmus ir pertraukas; jis nesirašo bendro teksto įvedimo. Peržiūrėkite ir patvirtinkite jo projektą prieš jį priimdami. Išorinės programos pakeitimai gali neįtraukti pasirinkėjų. Proceso izoliacija apriboja avarijas ir atšaukimą, tačiau tai nėra saugos smėlio dėžė.

## Pluginas

Atidarykite plėtinius, kad patikrintumėte pakuotę prieš importuojant ją. Pakuotės struktūra, manifestas ir hashai yra tikrinami. Importuojamas neįgaliųjų skaičius. Perskaitykite paskelbtas veiksmus ir aiškiai pasitikėkite leidėjais prieš suteikiant galimybę. Plugins veikia su jūsų naudotojo leidimais atskirame procese; šis procesas yra **ne smėlio dėžutė**. Tik įdiegti paketą, kuriuo pasitiki.

Vadybininkas palaiko ankstesnės versijos eksportą, išjungimą, atkūrimą ir atsigavimą. Pritrūkstamos priklausomybės blokuoja invokaciją. Pavyzdinis paketas suteikia skaičiavimą ir vietos valstybės veiksmus. Palaikoma schema yra dokumentuota JSON schemos subsetė, o ne visos galimos JSON schemas funkcijos. Nėra jokio viešojo „plugin“ rinkos.

## Mobilusis panelis

1. Atidarykite mobiliąją plokštelę, pasirinkite vietinį IP ir aiškiai pasirinkite profilį. Išimtinį atgalinį adresą galima pasiekti tik pačiame kompiuteryje; pasirinkite tinkamą LAN adresą telefonu ar planšetiniame kompiuteryje.
2. Pradėkite panelį ir perskaitykite jo HTTPS adresą, sertifikatą ir vienkartinį šešių skaitmenų kodą.
3. Viename įrenginyje toje pačioje patikimoje tinkle, atidarykite adresą. Palyginkite SHA-256 pirštų atspaudą per patikimą kanalą prieš pasitikėdami vietos sertifikatu. Programa automatiškai neįdiegia patikimo sertifikato.
4. Pora naudojant rodytą kodą ir išbandykite vieną nekenksmingą plokštelę. Manual Windows Firewall leidimas gali būti reikalingas LAN prieigai. Nenaudokite uosto prie interneto.
5. Pasibaigus paneliui arba atšaukite klientus per programą.

Privatūs serverių raktai yra apsaugoti Windows DPAPI; kliento autentiškumas saugomas kaip hashas. Pakeisti IP arba atnaujinti sertifikatą gali reikėti atnaujinti pasitikėjimą. Panelis rodo veiksmo ir tikrinimo būdus, o ne visus galimus gyvus jutiklius. Interneto naršyklė ir vietiniai HTTPS patikrinimai nepakeičia realaus telefono / planšetinio LAN priėmimo.

## Vietos asistentų susitikimas

Šis pasirinktas eksperimentinis asistentas yra neįgalus. Atidarykite jį iš įrankių, aiškiai įjungkite jį, pasirinkite darbo laiką / modelį ir pridėkite tik resursus, kuriuos norite apsvarstyti. Įveskite tikslą, sukurkite projektą, patikrinti neišspręstus prašymus ir perduoti rezultatą įprastam redaktoriui. Nėra nieko, kas automatiškai vykdoma; taupymas ir vykdymas išlieka aiškūs veiksmai. Nėra automatinio paleidimo laiko.

| Laiko bėgimas | Įdiegti |
| --- | --- |
| Ollama | Atskiras vietinis aptarnavimas ir vietiniu būdu įdiegtas modelis `127.0.0.1:11434`. Paskelbti nuotoliniai / debesų modeliai yra atmesti. |
| LM Studio | Vietinis serveris su vietiniu įkrovimu ir JSON schemos palaikymu `127.0.0.1:1234`. Neleiskite nuotolinio maršruto vietos apdorojimui. |
| llama.cpp | Vietinis `llama-server` ir suderinamas modelis su JSON schemos palaikymu `127.0.0.1:8080`. |
| ONNX Veikimo laikas GenAI CPU | Pasitikima vietos GenAI teksto modelio aplanką; pasirinkite `genai_config.json`. „Inference“ naudoja bendradarbiavimą be HTTP. |

HTTP klientai naudoja tik loopback, nenaudoja proxy ir ne sekti peržiūros. MAKRIVO negali patikimai nustatyti, ar suderinamas vietinis serveris vidaus skambina nuotolinę paslaugą. Autentizuoti vietiniai serveriai šioje versijoje neįmanomi. Modelio svoris, Python ir vietiniai indėlių serveriai automatiškai nuskambinami ar įdiegti.

ONNX paketai reikalauja `tokenizer.json` ir galiojančio pokalbio šabloną. Palaikomi šablonų formos `tokenizer_config.json` yra eilutė `chat_template`, žodynas su eilutėmis `default` arba nesąžiningas pavadinimas / numatytoji šablonas sąrašas. Jei šablonas trūksta arba nulis, `chat_template.jinja` naudojamas kaip atgal. Tušti, abejingi, per dideli arba netinkamai įrašiami šablonai yra atmesti. Arbitrinių ONNX failų, įterpimo/vizijos modelių ir GPU vien pakuotės negali būti keičiamos su suderinamomis GenAI tekstiniais modelais. Susieta versija GenAI yra 0.17.1, CPU. Naudokite tik patikimus modelius; proceso atskyrimas nėra smėlio dėžutė.

Asistentas palaiko pasirinktas programas be argumentų, pasirinktus dokumentus ir svetaines, o mikrofoną, kai tai aiškiai paprašyta. Jis nesukuria pranešimų, pasimatymų, savavališkų scenarijų, nemokamų spustelėjimų, atsisiuntimų ar ausinių keitimo. Tikslas ir išteklių pavadinimai siunčiami į pasirinktą vietos darbo laiką; išteklių maršrutai ir failų turinys nėra atskirai. Naudotojo nustatyta paskirtis pati gali turėti asmens duomenis. Vietinio darbo laiko tinklo elgesys lieka naudotojo atsakomybė.

Į The ONNX Prašymas dėl biudžeto 90 2 sekundės, įskaitant modelį, iki 3,072 Skubiai įtempti ir 1,024 generuojami tokenai, taip pat ribotas modelio kontekste. klaidingi / iškraipyti JSON ir nežinomi šaltinio ID yra atmesti. Atšaukti arba netoli užbaigti prašymą. Atsitiktinis modelio vykdymo laikas testas nustato vykdymą, nenaudingą planavimo kokybę ar laiko taupymą. Vietiniai eksperimentiniai logai turi įvykių identifikatorius ir laiką, ne tikslinį tekstą ar dokumentą; jie nėra automatiškai įkelti.

Vėliau mažas "Instrukt" modelio vertinimas sukūrė nulinius galiojančius planus keturiuose Vokietijos / Anglų tyrimuose. Kilmės išvada dirbo, bet modelis nesugebėjo griežto planavimo sutarties. Tai neigiamas kokybės rezultatas, o ne rekomenduojamas modelis ar įrodytas naudingas asistentas. Žiūrėkite datavimą vietos paleidimo laiko bandymų ataskaitą tiksliam modelio peržiūrimui ir apribojimams.

## Importuoti, eksportuoti ir sinchronizuoti

Peržiūrėkite importo prognozes prieš pakeičiant konfigūraciją. Importuoti veiksmai prasideda išjungti, o nešiojamas sinchronizavimas pašalina aparatūros specifinius ryšius. Atskirkite prietaisus ir leiskite veiksmus sąmoningai po patikrinimo. Peržiūrėkite tekstą / veiksmų turinį prieš dalijant eksportu; slaptažodžių neįtraukimas daro visus naudotojo sukurtus veiksmų turinius ne jautrus.

Profilų sinchronizavimas keičia failus pasirinktoje aplinkoje, kurį valdo jūsų esamas debesijos klientas. MAKRIVO nėra debesijos sąskaitos ir nėra debesijos autentifikavimo. Apžvalgos užkerta kelią tyliai perrašyti. Veiksmo turinys yra aiškus pasirinkimas. Atsisiųskite konfigūraciją prieš importuojant pakeitimą. Įprasta konfigūracija yra `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; istorinis adreso pavadinimas išlieka suderinamumui. Uždarykite programą prieš rankiniu būdu kopijuojant / atkurdami ją. Įsaugoti SDK patvirtinimai ir OS apsaugoti autentifikatoriai gali prireikti atkurti kitoje kompiuteryje.

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

Naudinga klaidų ataskaita apima Windows versiją, programėlę / komitetą, kalbą, ekrano skalavimą, aparatūros modelį, minimalią seką ir atitinkamą redakciją. Ištrinkite asmeninius kelius, veiksmo turinį ir autentiškumą prieš paskelbdami.

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

Windows integravimo bandymai gali atidaryti izoliuotus langus ir laikinai konfigūruoti / išleisti palaikomus tiekėją. Jie veikia be kito valdytojo, turinčio tuos ryšius. Išimtiniai ryškumas / garso bandymai yra skaitomi vien; aiškios aparatūros apvalios bandymai yra atskiri. Mock protokolai, atrankos ekranai ir atsitiktiniai grafikai neturėtų būti pateikiami kaip tikras aparatūros / modelio kokybės priėmimas.

Visiškas leidėjas renka kompanionus, mobiliuosius turtus, pavyzdinį priedą, ONNX darbo laiką ir prieinamas priklausomybės pranešimus. Laikykitės savo vadovų. Sėkmingas statymas nėra pasirašytas installeris ar teisinis leidimas. Šaltiniai ir statiniai gali išsivystyti atskirai; palyginkite dokumentacijos nuotrauką ir įsipareigoti su įdiegta versija.

## Licencija, privatumas ir išleidimo būklė

Numatytas komercinis modelis yra vienkartinis pirkimas su nuolatine įsigyta pagrindine versija; galutinė kaina, prietaiso leidimas ir atnaujinimo sąlygos turi būti nustatytos faktinėse pardavimo sąlygose. Produkcijos pirkimo / aktyvavimo sistema nėra nustatyta šioje knygoje. Nėra reikalaujamo debesijos paskyros kasdieniniam valdymo.

Nenaudokite savarankiškų SDK binarų be įrodytos leidimo. Išsaugokite licenciją ir trečiųjų šalių pranešimus kiekvienai siunčiamai priklausomybei, įskaitant .NET ir ONNX paketus. Modelių svoris turi savo terminus. Atnaujintas licencijos auditas išduoda kai kurias naujas priklausomybes ir nėra tų papildymų nuoseklumas. Viešas saugykla pati nesuteikia bendros atviros šaltinio licencijos.

Tikrasis tiekėjas, telefonas / planšetinis LAN veikimas, mišri monitorių / DPI perėjimai, ilgas naudojimas, išsami kalbos apžvalga, išskirtinė vairuotojo parama ir naudingas AI planavimo kokybė lieka atskira priėmimo darbas. Niekada nepateikiu sutikimo iš vienos nuosavybės.

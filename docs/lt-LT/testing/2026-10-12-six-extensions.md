<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-12-six-extensions.md)

# Šešios plėtros priemonės: įgyvendinimas ir bandymas

Standas: 12. Spalio mėn. 2026, pramonė `codex/six-extensions`. Darbo būklė, ne gamybos atleidimas.

## aptarnavimas

Įsakymų rinkinys atveria įrašus **Darbo vieta**, **Slapukų gamintojai ir SDK**, **Mobilusis panelis**, **Išplėtimas**, **UI automatizavimas**, **Įveskite UI pabaigą** ir **vykdymo patikrinimas**.

- Darbo padėjėjas sukuria darbo vietų, susitikimų, srauto, mokslinių tyrimų ir projektų profilį. Programos iš registracijos / pradžios meniu arba failo pasirinkimas, URL, išleidimo / ekrano ir OBS scenos rodomas kaip žingsniai prieš saugojimą. Institucija nedaro jokių veiksmų.
- Veiksmų redaktoriuje **Patikrinkite rezultatus** įveskite išankstinį patikrinimą ir peržiūrą. Tvarkymas ir mobilusis plokštė skiriasi nuo faktinio patvirtinimo nuo nevertinamo poveikio. Santykiniai pakeitimai, klaviatūros sutrumpinimas ir teksto įvadas nesuteikia pastovios poveikio patvirtinimo. Procedūros įrodymas neparodo profesionalios paraiškos.
- UI automatizavimas naudoja atskirą, laiko ribotą darbininką. Pasirinkimas per pelės poziciją, atrankos patikrinimą, Invoke/Select/Toggle/Focus/SetValue/WaitState; atsisakoma kelių paspaudimų, išjungtų tikslų ir slaptažodžių laukų. Įrašas apima palaikomus spustelėjimus ir pertraukas; teksto įrašai nėra sutrumpinti. Prieš įsigijimą patikrinamas priėmimas.
- Plėtros: ZIP testas, šifravimas, schema, išjungtas importo, aiškus pasitikėjimas prieš aktyvavimą, izoliuotas procesas su ribotą IPC, eksportuoti, išjungti, atkurti / atmesti ankstesnę versiją. Proceso izoliacija nėra smėlio dėžutė: paskelbti įskiepiai veikia su naudotojo teisėmis. Pavyzdinis paketas `makrivo.example.zip` apima skaičiavimą ir statuso veiksmus. Palaikomi schemos sudaro dokumentuojamą paprastą JSON schemos dalį šaltinio kodo; nėra viešosios rinkos.
- Mobilusis panelis: pasirinkti vietinį IP ir pasidalinti profilius, atidaryti HTTPS adresą, patikrinti sertifikatą pagal rodomą SHA-256-Fingerprints ir sąmoningai pasitikėti, tada įveskite vienkartinį šešių simbolių kodą. Privatūs raktai yra apsaugoti Windows-DPAPI; saugomi tik kliento prieigos slapukai. Sertifikatai keičiasi po atnaujinimo ar IP keitimo. Naudotojo Windows „Firewall“ išleidimas atliekamas, jei reikia. Nėra automatinio sertifikato įdiegimo, uosto plėtros ar ugnies sienos keitimo. Kliento prieigos gali būti atšauktos. Panelis rodo veiksmų būklę ir bandymų eigą; tai dar nėra universalus gyvasis jutiklis.

## klaviatūra

| Gamintojas | Įgyvendinimas | Apribojimas |
|---|---|---|
| Corsair | esami iCUE G mygtukai ir RGB | Vietinis iCUE ryšys ir tinkamas modelis reikalingas |
| Logitech G | G-Key SDK ir atskiras LED SDK | Originalus vietinis x64-DLL aiškiai išleisti; G-Key SDK nesuteikia individualios tapatybės kelių klaviatūrų |
| Razer | Chroma RGB ir standartinės klaviatūros atpažinimas | Savarankiškieji mygtukai anksčiau paskirti į Synapse |
| SteelSeries | GameSenseRGB ir standartinio mygtuko atpažinimas | Prieš paskirstydami savarankiškus mygtukus į GG |
| Wooting | Analoginės įrangos vertes, slėgio / išmetimo svyravimus | Originalus vietinis x64-SDK-DLL; įprastas HID įrašas lieka aktyvus |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Kiti | fiziniai standartiniai klaviatūra pagal prietaisą ir skenavimo kodas | Fn ir nuosavybės kontrolės elementai pagal modelį |

Nėra naujų savarankiškų SDK DLL. Nauji gamintojų adapteriai dar nėra patvirtinti be tinkamos fizinės įrangos.

**Išskirtinė antroji makropada nėra paruošta:** Raw Input stebėti įrenginius atskirai, bet slopina jų įprastą Windows-Produkcijos išleidimas reikalauja licencijuoto, pasirašyto Windows 11/Secure Laivas patikrinami filtravimo laivai, taip pat bandymai dėl avarinio išleidimo, sustabdymo ir atkūrimo. Nė vienas laivynas buvo įdiegtas..

## Testas

- Release-Build: sėkmingas, nulinis įspėjimai / klaidos.
- Pagrindinė konsole: visos bandymų grupės sėkmingai, įskaitant gamintojo mygtukų konfliktus, „plugin“ atgal ir faktines bandymo klaidas.
- Išplėstinė integracija: tikrasis UIA darbuotojas su WPF testavimo programa; unikalus invoke, daugialypė, išjungti tikslai, slaptažodžio išjungimas. Echter pavadinimo vamzdžio plokštės procesas, protokolo ribos, analoginė histerija ir Logitech ABI dekodavimas sėkmingai.
- Tiesa, HTTPS hostas: pažymėtas sertifikatas, nesusijęs prieiga, klaidingas / vienkartinis kodas, kilmės apsauga, naršyklės failai, slapukų saugojimas ir saugomas raktas sėkmingai. Išspręsta bandymo metu aptikta skanelio klaida su efeminiu raktų.
- Windows-Suite su aiškiai išleistu pagrindu sėkmės testas: sėkmingas; Audio/OBS/iCUE/Raw Input ir ekrano būklė patikrinama. ankstesniame etape visi komplektas nesugebėjo atsisakyti fokusavimo į izoliuotą bandomąją langą. Vėliau prie AI prototipo taip pat buvo visiškas komplektas, įskaitant fokusavimo keitimą ir susijusius iCUE-Tastatas. dar vienas pakartotinis kursas vėl nesugebėjo keisti dėmesį; kontekstinis testas nėra stabilus. Šis testas nebuvo susilpnėjęs. trumpą laiką trūkstamos DDC skaitymo atsiliepimai yra ribojami; ryškumas nepasikeitė.
- WPF šmeižtas: visi ankstesni ir nauji dialogai atnaujinti, konfigūracijos įvairovė, redaktorius ir CRUD sėkmingai; ekrano nuotraukos statybą `smoke`.
- naršyklė: Playwright su Edge, 390×844 ir 1024×768, Prisijungimo srautas / UUID / vienkartinis siuntimo / be horizontalaus srauto sėkmingai. API-Mock; tikras HTTPS testas atskirai. `artifacts/mobile-browser`. naršyklė nebuvo prieinama, todėl reguliariai Playwright Fallback.
- Nepriklausomas kodo peržiūra atliekama; vėluoja mobiliųjų komandų, trūksta terminalo patikrinimo rezultatų, procesinio maršruto patikrinimas, „Plugin“ atkūrimas ir RGB širdies krūvis ištaisyta.

Vis dar atviras: išskirtinė vairuotojo integracija, fiziniai gamintojo / telefono / tablečių LAN testai, Windows skalavimas 150/200 procentų ir visiškas visų naujų dialogo tekstų vertimo patikrinimas. Nauji dialogo tekstai daugiausia yra vokiečių; esami kalbos katalogai išlieka. Todėl bendras reikalavimas ** nėra visiškai sumažintas **.

## Sukurkite paketą

`./tools/publish-complete.ps1 -SelfContained` sukuria naują dėžutę pagal `artifacts/release-*`, įskaitant darbalaukį, UIA darbuotojus, HTTPS hostą, naršyklės failus, importuojamą pavyzdinį priedą ir paleidimo licenciją. Įdiegtos programos nebus perrašytos. Be `-SelfContained` pakuotės reikia tinkamai .NET-10-Desktop-/ASP.NET-Runtimes.

Pakuotė yra vietinis patikrinimas, o ne pasirašytas pardavimo įrenginys. Nuosavos naudojimo sąlygos ir SDK pardavimo teisės yra atskiri leidimai; žr. esamą dokumentą `docs/licensing`.

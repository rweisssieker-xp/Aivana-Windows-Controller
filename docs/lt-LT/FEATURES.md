<!-- generated-doc locale=lt-LT source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../FEATURES.md)

# MAKRIVO – funkcijų apžvalga

Gamintojas: **Aivana**. Standas: **12. Spalio mėn. 2026**. Tai yra vystymosi būdas, ne pardavimo ar aparatūros išleidimas.

## Windows valdymo paslaugos

- Šauliai, pasauliniai šauliai, komandos rinkinys, mėgstamiausi ir pritaikomi profiliai.
- Makros su keliais žingsniais, pasikartojimais, pertraukais ir išankstiniu matavimu.
- Atidarykite programas, failus ir svetaines; naudokite tekstą, tarpinę lentelę ir klaviatūrą.
- Įdėti langus, užblokuoti Windows, valdyti garso išėjimus ir mikrofonus, taip pat keisti ekrano ryškumą per palaikomus sąsajas.
- OBS-WebSocket-v5-Anbindung scenos, įrašymo, srauto ir garso.
- Šablonai, darbo vieta padėjėjas, prietaiso atpažinimas ir klaviatūros perpildymas.
- Profilų konfigūracijų importas ir eksportas; dizaino patikrinimas ir konfliktų nustatymas.
- Vietos saugomi nustatymai; kasdieniniam naudojimui nereikia sąskaitos.

## Išplėstinės funkcijos dabartiniame šaltinio kodo

| sritis | Funkcijos apimtis | sienos |
| --- | --- | --- |
| Rezultatų patikrinimas | Išankstinis patikrinimas, peržiūra ir bandymai | Tik patikrinamas poveikis patvirtina; sėkmingas mygtukas neparodo tikslinio poveikio. |
| Darbo padėjėjas | Paruošti pasirinktų programų, failų, URL ir kitų veiksmų profilį ir procesus | Būtina peržiūra ir išsaugojimas; sąranka nieko nevykdo. |
| Makro įvedimas ir darbo srautas | Įrašas, sąlygos, gestai ir vietinis profiliavimas | Elgesys ir apribojimai pateikiami įgyvendinimo ir bandymų ataskaitose. |
| UI automatizavimas | Rinkti tikslus, patikrinti rinkinius, imtis veiksmų ir ribotų spustelėjimų | Atskiras darbinis procesas; nevienareikšmiai, išjungti ir slaptažodžių tikslai atmetami; bendras teksto įvedimas neįrašomas. |
| Išplėtimas | Pažymėti, importuoti, aktyvuoti, eksportuoti ir grąžinti paketus, aiškiai pasitikėdami | Procesinė izoliacija nėra smėlio dėžutė; nėra viešo rinkos plokštės. |
| Mobilusis panelis | Vietinis HTTPS plokštė, jungimas, pasirinkti profiliai ir statusas / bandymo istorija | Tikri telefonų/planšetinių kompiuterių LAN priėmimo bandymai dar neatlikti; tai nėra universalus jutiklių skydelis. |
| Gamintojas adapteris | Corsair, Logitech-G SDK, Razer/SteelSeries-RGB ir Wooting-Analogadapteriai | Modelis / SDK priklausomybė; naujas adapteris nėra visiškai patvirtintas su realiu aparatūra. Nėra naujų savarankiškų SDK DLL. |
| Fizinė klaviatūra | Standartiniai mygtukai nustatomi naudojant įrenginį ir skenavimo kodą | Raw Input netrukdo įprastam Windows įvedimui. |
| Vietinis AI konsultavimo asistentas | Galutinis prototipas Ollama, LM Studio, llama.cpp ir ONNX paleidimo laikas GenAI CPU gamina ribotas dizainas iš aiškiai pasirinktų išteklių | Pagal numatytuosius nustatymus išjungta; savoji CPU vykdymo aplinka patikrinta atsitiktiniu modeliu, bet planavimo kokybė ar laiko taupymas neįrodyti; modelio svoriai neįtraukti. |

## Nėra pardavimo, kaip pardavimo

- **Išskirtinė antroji klaviatūra/makropultas:** trūksta pasirašytos, licencijuotos ir aparatūroje patikrintos filtro tvarkyklės. Įprasta klavišų įvestis vis dar gali pasiekti Windows.
- Visiškas visų gamintojų adapterio aparatūros nutekėjimas, taip pat realios mobiliųjų prietaisų LAN testavimas.
- Tikroji mišri monitorių / DPI keitimas ir visų naujų vertimų profesionalus atskaitymas. PerMonitorV2 ir simuliuojami 150/200-Prozent-Layouts buvo patikrinta; tikrieji ekranai išlieka atskiri. Šioje srityje yra 27 kalbų; išsami nauji tekstai dar nėra visiškai kalbėti.
- Pasirašytas pardavimo įrenginys, galutinis EULA, patikrintos SDK pardavimo teisės ir produktyvi pirkimo licencija / patikrinimas.

## Detalės ir įrodymai

- [Paslaugų dokumentacija](README.md)
- [Išplėtimas: funkcijos, bandymai ir atviros ribos](testing/2026-10-12-six-extensions.md)
- [Vietinis AI padėjėjas: prototipas ir bandymo stotis](testing/2026-10-12-local-meeting-assistant.md)
- [Vietos IRT trukmės ir modelio reikalavimai](testing/2026-10-12-local-ai-runtimes.md)
- [DPI ir skalavimo patikrinimas](testing/2026-10-12-dpi-scaling.md)
- [Licencijos patikrinimas](licensing/2026-10-12-commercial-license-audit.md) – istorinis įrašas; naujasis publikacijos scenarijus papildo paleidimo instrukcijas. Galutinis paketas turi būti nagrinėjamas atskirai.
- [Pardavimo platformos](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – palyginimas; Stripe yra papildoma mokėjimo platforma, žr. dabartines teikėjo sąlygas.

## Sukurkite išsamią plėtros paketą

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Paketas apima darbalaukio programą, „UI“ automatizavimo darbuotoją, mobilųjį ryšį, naršyklės failus, pavyzdinį priedą ir paleidimo laiką. Statybos faktai nėra patikrinami Git. GitHub šaltinio kodas dar nėra paskelbtas Windows įrenginys.

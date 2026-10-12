<!-- generated-doc locale=lt-LT source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../VERIFICATION.md)

# Tyrimas – 8. Spalio mėn. 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Taikymas buvo sukurtas pagal Windows x64 su stabilia .NET SDK 10.0.202 ir išleistas kaip nepriklausomas Windows-x64-Ausgabe.

## Automatizuota patikrinimas

- Apskritai: jokių įspėjimų ir klaidų.
- 19 Pagrindiniai elgesio testai: standartinė konfigūracija, patvirtinimas, nulinis / dvigubas duomenys, profilio prioritetas, importo / eksporto ir atsargos atkūrimas, makro tvarka ir pasikartojimas, nutraukimas, dvigubai išjungimas, serijalizavimas, nepakeičiamas vykdymo greitis ir žingsnis.
- Windows bandymas: Hotkey normalizavimas ir aljansai, gimtoji struktūra dydžiai, reali klaviatūros įrangos sąrašas, saugi registracija ir atkūrimas, sintetiniai WM_HOTKEY užkandžių ir iškraipymo, deaktyvinamas kandžių registracija įvedimo simuliacijos metu.
- Savarankiškas EXE buvo pradėtas naudojant `--smoke-test`: visi penki WPF vaizdai yra atkurti, užuolaidos gali būti įdiegtos / kopijuojamos / ištrinti, dizainai išlieka atnaujinami, konfigūracija saugoma ir vėl įkeliama. Šis testas naudoja izoliuotą laikiną konfigūracijos sąrašą ir neveikia teksto ar žiniasklaidos veiksmų.
- Duomenų plokštė, redaktorius ir kompaktiškas vaizdas (900 × 720) buvo vizualiai patikrinamas kaip tikras WPF. Projekto saugojimas, atmetimas, makro tvarka, Vokietijos „hotkey“ sąjunga, atskiri makro nuostoliai ir žingsnis į priekį yra išbandyti.

## Hardverio ribos

Paskutinis vietinis perėjimas atpažino keturis Windows klaviatūros sąsajas. Šis skaičius apibūdina HID/Windows sąsajas, o ne keturis fizinius klavišus.

Oficialus iCUE SDK yra įdiegtas ir yra šalia EXE. Gyvas bandymas sujungia iCUE ir atpažįsta Corsair klaviatūrą su modelio ir LED informacija. Apšvietimo pokyčiai nebuvo automatiškai sukurti; modelio atitinkančios spustelėjimai ir kiti gamintojai nebuvo išbandyti. Generiškas Hotkeys taikomos tarpklavijo.

Teksto / žiniasklaidos įrašai nebuvo įterpti į kitas atviras vartotojų programas. Ištirta gimtoji duomenų struktūra ir klaidų/laisvos logika; konkrečioje tikslinėje programoje elgesys, be kita ko, priklauso nuo jos tinkamumo lygio.

## Pakartoti

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI testas nurodo rezultatą ir ekrano nuotrauką pagal `smoke` šalia patikrinto EXE. Jis suprantamas kaip diagnozės būdas ir po to, kai jis baigia, nėra nuolatinės fono.


## Išplėstinė kontrolė

Be to, patikrinama: profilio sąlygos su proceso ir langų pavadinimais, kintamosios be rekursinio plėtros, makro peržiūros be vykdymo, naujos veiksmų patvirtinimo ir senų JSON failų suderinamumo. Native egzaminai apima komandos rinkinį su registracija, konfliktais, WM_HOTKEY išleidimu ir išleidimu, taip pat grynai monitorių skyrimu ir poreikius kontroliuojančiu skaitytuvu. WPF testas atveria ir atveria realią paletę, tikrina grįžtamasis / atkūrimas ir mėgstamiausius, tikrina švarius dizainus po atkūrimo ir sėkmingai baigiasi procesas.

Windows užrakto, tarpinių pakeitimų ir lango judesių bandymų metu nebuvo taikomos naudotojų programoms. langų veiksmai skelbia Windows prašymus asynchronoje; konkreti tikslinė programa gali turėti savo minimalius dydžius arba leidimų ribas. Profilų uždarymo ir statistinių rezultatų taikymas vyksta posėdyje.


Išplėstinis patikrinimas 12.10.2026: 30 Pagrindiniai bandymai; Windows-Adapteriai, įskaitant išleidimą įvedimo metu, rašytinio monitorių atpažinimą ir OBS-Statusas Mock išlikęs. WPF peržiūrėti perėmimo/atšaukimo, įrašų pradžios/stopp, profilio mainų prognozavimas, gestai ir simbolių šaltiniai.: 27 Katalogas su 967 Pagrindinis > Dokumentas: 84 Puslapiai keturiomis kalbomis. kitų gamintojų mygtukai Corsair nebuvo išbandyta įrangos; generinė parama apima standartinius įrenginius F13–F24 debesų perdavimas atliekamas per pasirinktą sinchronizuotą aplanką; importo patikrinimas ir išnykimas.

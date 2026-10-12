<!-- generated-doc locale=lt-LT source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Valdymo įgyvendinimo planas

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Agentūriniams darbuotojams: naudokite izoliuotą nuosavybę; vartotojas aiškiai paprašė visiško įgyvendinimo su maksimaliais naudingais agentais.

**Gali: ** Siųskite gimtąją Windows karštoji plokštė patvirtintame dizaine.
**Architektūra:** Pagrindinė konfigūracija ir atšaukiamas vykdymas; Windows Adaptai; WPF Desktop kompozicija.
**Techninis stalas:** .Net 10, WPF, Win32, JSON. be mokamų priklausomybės.
**Spec: ** ../specs/2026-10-08-Windows-Controller-design.md

## Pasauliniai apribojimai
Vokietijos vartotojo ryšys; vietinis saugojimas; Windows x64; tikrasis aparatūros pajėgumas; be klaviatūros įrašo; be numatyto pradžios.

## Apžvalga Focus
Neteisingas importo priemones neturėtų pakeisti išsaugotų nustatymų; laikomi modifikatoriai neturėtų užteršti gamybos; nesudėtinga įranga neturėtų sugriauti; uždarymo priemones reikia išlaikyti prieinamumą; atšauktos makros turi išleisti išteklius.

## Užduotys ir nuosavybė
- [x] Pagrindinis agentas: src/Aivana.Pagrindinis ir bandymai/Aivana.Testai. Modelis, JSON išlikimas, patvirtinimas, profilio atranka ir „MacroRunner“. Atlikite nesėkmingus elgesio testus, tada įgyvendinkite ir atlikite visus pagrindinius testus.
- [X] Windows Žymė: src/Aivana.Windows. RegisterHotKey, prietaiso inventorius, SendInput, priekinis procesas ir pasirinktas Corsair apšvietimo adapteris. patikrinkite vietinius struktūrų dydžius ir registracijos valymą su saugiu dūmų testu.
- [x] GUI agentas: src/Aivana.Desktop/MainWindow.xaml ir .cs plus UI padėjėjai tik. Tamsus lapas, pilnas redaktorius, profiliai, įrenginiai, klaviatūros apžvalga ir makro redaktorius. Sukurti sutartis egzistuoja.
- [x] Tėvas: bendrų sutarčių, projekto failų, "DesktopController" sudėtis, paraiškos / kelionės gyvenimo ciklas, importo / eksporto dialogai, dokumentacija, integracijos patikrinimai, leidimo ir vykdymo programos dūmų testas.

## Pasidalintas sąsajas
Susitarimai gyvi „Core/Models.cs“. Kontrolierių įgyvendinimas gyvena „Desktop/DesktopController.cs“. UI vadina kontrolierių metodus per šią konkrečią klasę. Pareigūnai gauna tikslius parašus savo užduotims. Kiekvienas turi atskirus failus. Tėvas išsprendžia tarpinio projekto nesėkmes po to, kai visi agentai baigia.

## Tikrinimas ir pristatymas
- [x] dotnet build Aivana.slnx -c Išleidimas: nulinės klaidos.
- [x] dotnet paleisti - projektų bandymai / Aivana.Testų -c išleidimas: elgesio patikrinimai praeina.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Paskelbtas vykdomas, patikrinamas Windows IU ir procesų stabilumas, saugus profilio / redagavimo / pastovumo srautas.
- [x] Parašykite README su statybos / pradžios instrukcijomis ir aparatūros apribojimais; įrašų patikrinimai ir likę tik aparatūros bandymai.

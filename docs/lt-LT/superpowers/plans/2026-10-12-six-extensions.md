<!-- generated-doc locale=lt-LT source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Plėtros įgyvendinimo planas

> ** Agentų darbuotojams: ** REQUIRED SUB-SKILL: Naudokite super galias: vykdymo planus gimtoji veikla arba super galios: sub agentų vadovaujama plėtra, kai vartotojas pasirenka delegaciją. Stebėkite žingsnius su patikrinimo dėžėmis.

**Tikslas: ** Siunčia šešis integruotas plėtiniai ir pajėgumų pagrindu daugiafunkcinis klaviatūros palaikymas.

**Architektūra:** Išsaugokite branduolį / Windows / WPF atskyrimas. įveskite izoliuotus priedus ir mobiliųjų paslaugų procesus su siaura autentiška IPC; visa vykdymas eina per esamą kontrolierių ir MacroRunner..

**Techninis stalas:** C# ir .NET 10, WPF, Windows Automatija, Win32 Raw Input, Skaitmeninis kodas: ASP.NET Core HTTPS/WebSocket, Žymos archyvas.Json.

** Spektaklis: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (pritarta vartotojui)

## Pasauliniai apribojimai

- Esama konfigūracija turi būti nepakitusi; papildyti veiksmo rūšis, o ne perreguliuoti jų nuolatines vertybes.
- Išsaugokite esamus profilius, G1/G2 užduotis, vietinę veiklą ir sąskaitą nemokamą naudojimą.
- Raw Input Išskirtinis per prietaisą pašalinimas reikalauja tikro klaviatūros filtro.
- Importuojami vykdomieji įtaisai neturėtų vykti automatiškai.
- Išimtinai išjungti ir apriboti konfigūruotus sąsajas mobiliesiems įrenginiams; jokių skundų autentifikavimo atsisakymas.
- Trumpas / teksto pristatymas nėra įrodymas, kad tikslinė paraiška pritarė komandai.
- Nepakeiskite įdiegtos programos, įdiegkite vairuotojus, pakeiskite tikrąjį klaviatūros žemėlapį ar atvirą prieigą prie tinklo kaip incidentinį testą.
- Išsaugokite nesusijusius pokyčius. Nenaudokite esamų bendrų leidimų kaip šios funkcijos dalį. Stažuotės tik savarankiškai peržiūrėtos funkcijos, jei prašoma.

## Apžvalga Focus

1. Dvigubai identiški klaviatūros modeliai: abejotiniam perjungimui reikia pakartotinai pasirinkti (Tasks 10–12).
2. Atšaukimas per UIA / paslaugų teikėjas / priedai / tinklo laukia: jokių veiksmų po atšaukimo (Darbas 1, 4, 6, 8).
3. Naudotojas redaguoja, kai atidarytas stebuklas arba pakuotės peržiūra: peržiūros nesuderinamumas turi užkirsti kelią tyliai perrašyti (Tasks 3, 7).
4. Kalba ir fizinis išdėstymas skiriasi: pasirinkėjai ir skenavimo kodai neturėtų priklausyti nuo lokalizuotų rodiklių pavadinimų (Tasks 4, 10).
5. Tinklo atkūrimas ir įkrovimo įkrovimas: komandos neturėtų tyliai vykdyti du kartus (Tasks 6, 8).

## Susitarimai ir priklausomybės tvarka

Užduotys 1–3 nustato vykdymą ir nustatymą. Užduotys 4–5 statyti UIA. Užduotys 6–7 pristatyti įskiepius. Užduotys 8–9 teikia mobilųjį prieigą. Užduotys 10–12 teikia klaviatūros adapteriai ir filtro integracija. Užduotis 13 patvirtina visą produktą.

Kiekviena užduotis yra nesėkmingas testas → minimalus įgyvendinimas → perdavimo testas ciklas. Užduotis yra užbaigta tik tada, kai pasiekiama per paraišką ir jos patikrinimai praeina. Scaffolds, mock adapteriai ir dokumentacija vieni nepatenka į pristatomą.

### Užduotis 1: Tipo patikrinimas ir suderinamas išlikimas

**Įrašai:** Kūryba `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Modifikuoti `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**sąsajos:** Gamybos `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` ir `VerifyAsync(ActionStep, CancellationToken)`, Abiejų grįžimas `Task<ActionVerificationResult>`. Įtraukite serijinę `VerificationOptions` su įgalinta vėliava ir laiko (100–60000 ms), numatyta išjungti. paskelbti įrašo per žingsnį rezultatus, įskaitant paleidimo ID, tile ID ir žingsnio indekso be veiksmų mokėjimo.

- [ ] Rašyti patikrinimai: paveldas JSON išlaiko esamas veiksmų numerinius vertes; nebuvusios galimybės neįmanoma patikrinti; negaliojančio laiko išleidimo patvirtinimas; išankstinis neveiksmas neleidžia vykdytojo skambučio; nesėkmingas po sąlygų niekada nesukelia patikrinto sėkmės; nepalaikytas vykdytojas yra neįtikėtinas; atšaukimas sustabdo patikrinimą ir vėlesnius veiksmus.
- [ ] Veikia `dotnet run --project tests/Aivana.Tests -c Release`; patvirtina naujus patikrinimus prieš įgyvendinimą.
- [ ] Įgyvendinimo sutartys, patvirtinimas, klonavimas / išsaugojimas ir „MacroRunner“ integracija. Pagal „OnError“ sustabdyti / tęsti politiką; išsaugoti esamus vykdymo atsiliepimus.
- [ ] Atlikite pagrindinius testus; reikės visų patikrinimų.

### Užduotis 2: Windows patikrinimo paslaugų teikėjai ir rezultatai

**Įrašai:** Kūryba `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Modifikuoti `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Testas įėjimo taškai.

**sąsajos:** „WindowsActionVerification“ įgyvendina užduotis 1 teikėjas naudojant esamą garso, ekraną, procesą / langą ir OBS klientai. išankstiniai bandymai prieinamumo ir identifikatoriai; patikrinimas palygina pageidaujamą būklę po apribotos apklausos. WPF Istorijos požiūris.

- [ ] Pridėti nesėkmingus bandymus, kad nebūtų prieinamos aiškios garso / ekrano ID, OBS laiko išleidimas, netinkamas stebimas būklė, nežinomi trumpalaikiai poveikiai, jautrus mokėjimo apkrova ir atšauktas apklausas, naudojant injekcinius skaitymo paslaugų teikėjus.
- [ ] Įdiegti realią skaitymo palaikytą patikrinimą absoliučiu tūriu, tyliai, absoliučiai ryškumui, proceso / lango pasirengimui ir palaikytoms komandoms OBS. Relatyvūs / rungtynės veiksmai reikalauja užfiksuotą pre-valstybę; kitaip pranešimas Neįtikėtinas.
- [ ] Pridėti laipsnio patikrinimo kontrolę ir saugią vykdymo istoriją, rodančią būklę, nesėkmingą reikalavimą ir atkūrimo tekstą.
- [ ] Veikia branduoliai / Windows patikrinimai ir WPF dūmai su injektuotais valstybės tiekėjais; nėra tikro ryškumo ar garso rašymo.

### Užduotis 3: Vadovaujamas darbo erdvės kūrimas

**Įrašai:** Kūryba `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modifikuoti paletę ir įėjimo taškus.

**sąsajos:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; Pasirinkimas apima šablonų ID, pasirinktas vykdomas maršrutas, URL, garso / vaizdo ID, OBS Scena ir atrinkti ryšiai. „Discovery“ grąžina paskirtus kandidatus su stabiliomis ID ir prieinamumu..

- [ ] Pridėti nesėkmingus bandymus visiems penkiems prašomiems burtininkų šeimoms, nepriklausomai sukurti žingsniai, trūksta reikalingų tikslų, atšaukti be mutacijos ir sutaupyti nuo pakeisto peržiūros.
- [ ] Rasti paraiškas per registruotus programos maršrutus ir Pradžios meniu trumpalaikius; prietaisai per esamus tiekėjus; scenos per skaityti tik OBS užklausą. Laikykite rankinius pasirinkimus nerastas programas.
- [ ] Įdiegimas Pasirinkite darbo srautą → Pasirinkite tikslus → Pasirinkite kontroles → Įvertinkite/taikykite → Išsaugokite. Sukurkite redaguojamus profilius ir niekada ne paleiskite nustatymų metu.
- [ ] Pridėti tikrąjį WPF burtininkų rūkymo patikrinimus užbaigimo / atšaukimo / konflikto; atlikti visus bandymus.

### Užduotis 4: UIA pasirinkėjai ir riboti veiksmai

**Įrašai:** Kūryba `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modifikuoti modelius, konfigūracijos patvirtinimą ir WinActionExecutor.

**sąsajos:** Apskritai `ActionKind.UiAutomation`; `UiElementSelector` parduotuvės procesas, automatizavimo ID, kontrolės tipas ir užsakytas protėvių kriterijai. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Grįžimas `Task`; operacijos Invoke, SetValue, Select, Toggle, Focus ir WaitState..

- [ ] Pridėti nesėkmingus bandymus prieš dedikuotą bandymo langą perkelti / atšaukti, padvigubinti rungtynes, dingusius elementus, nepalaikytus modelius, neįjungtas kontroles, slaptažodžius, Unicode etiketės ir laikas / atšaukimas.
- [ ] UIA įgyvendinimas su apribotais prašymais ir procesų tikslinimu. Potencialios pakabos teikėjai eina už atkuriamojo darbuotojo ribos; prašymo atsisakymas neturėtų palikti neigiamų šalutinių poveikių.
- [ ] Išbraukti slaptažodžių vertybes; pranešti apie prieigos / vientisumo apribojimus, o ne peržengti juos.
- [ ] Veikia Windows patikrinimai naudojant izoliuotus patikrinimus; pridėkite tik enum vertybes ir atnaujinkite paveldėjimo patikrinimus.

### Užduotis 5: UIA picker ir peržiūrėtas įrašas

**Files: ** Sukurkite `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modifikuokite veiksmų katalogą, redaktorių ir įrašų integraciją; sukurkite UIA WPF rūkymo patikrinimus.

**Susiję: ** Picker grąžina `UiElementSelector`; įrašytojas grąžins siūlomus UIA žingsnius ir niekada nerašys tiesiogiai į išsaugotą profilį. Test-selector komandos grąžina rungtynes be nuorodos.

- [ ] Pridėti nesėkmingų patikrinimų: pikeris išskiria MAKRIVO ir slaptažodžio vertės užfiksuojimą; įrašas / atšaukimas puslapiai esamą profilį nepasikeitė; abejotina peržiūros ataskaitos atitinka skaičių; priimant įrašytus žingsnius sukuria redaguojamą projektą.
- [ ] Įdiegimo elementas pabrėžimas / vaizdas, veikimo parametrai, atrankos peržiūra ir įrašymo peržiūra langas. Atlikite esamą trumpalaikį įrašymo elgesį.
- [ ] Pridėti katalogų įrašus ir prieigą prie paletės; patvirtinti redaktoriaus klaidas prieš išsaugojimą.
- [ ] Veikia Windows ir WPF dūmų patikrinimai ant fiksuotojo; patikrinkite picker / redaktoriaus ekrano nuotraukas.

### Užduotis 6: izoliuotas įkroviklis ir protokolas

**Įrašai:** Kūryba `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Modifikuoti sprendimą ir veiksmų pristatymas.

**sąsajos:** Version-1 manifestas nurodo paketą ID / versija, įėjimo taškas, parametrų schemas, veiksmus, valstybės teikėjus ir gebėjimų deklaracijas. IPC pranešimai yra protokolo versija, unikalus prašymo ID, metodas, JSON parametrai ir tipinis rezultatas. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Grąžina rezultatus. priedas `ActionKind.Plugin`.

- [ ] Įtraukti nesuderinamą protokolą, pažeidžiamą / per didelį (>1 MiB) pranešimą, dvigubą prašymo ID, atšaukimą, avariją, neatsakingą procesą ir trūkstamą veiksmą.
- [ ] Įdiegti tą patį vartotoją pavadinimas vamzdis IPC su 10 sekundžių rankų ir konfigūruojamas vykdymo laikas užfiksuotas 60 sekundžių. Pasitraukite nuo atsakingo darbuotojo; po atkūrimo automatiškai nežaidžiate skrydžio veiksmų.
- [ ] Įdiegti schemas ir leistinas valdytojų paslaugas. Paaiškinkite, kad įdiegti papildas pasitikėjimas; proceso izoliacija nėra operacinė sandbox.
- [ ] Atlikite pagrindinius bandymus ir integracijos bandymus prieš realius bandymų įskiepius.

### Užduotis 7: Plugin paketai ir pavyzdys integracija

**Įrašai:** Kūryba `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, paketų bandymai; modifikuoti paletę / įvadas.

**sąsajos:** Pakuotės eksponatas `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` ir `UninstallAsync(packageId)`. Peržiūrėtas paketas įrašai, priklausomybės, versijos ir deklaruotos galimybės.

- [ ] Pridėti nesėkmingų bandymų zip traversal, absoliučių maršrutų, simlink/reparse pabėgti, duplicate įrašus, >100 MiB išplėstinių duomenų, hash nesuderinamumas, nuvertinimas, stale peržiūra ir neįgali importuoti.
- [ ] Įdiegimas išankstinis ir aiškus įgaliojimas, priklausomybės klaidos, laipsniškas montavimas ir atnaujinimas; iškrauti darbuotoją prieš pašalinimą.
- [ ] Siųsti realią pavyzdį, kad grąžintų konfigūruojamą skaičiavimo veiksmą ir gyvas būsenas per IPC, įskaitant atšaukimą; pakuotės / eksporto / įdiegimo / išjungimo per sąsają.
- [ ] Atlikite pakuotės / host testai ir WPF galutinis patikrinimas. Tikrinkite, kad prieš įvežimą neįvežami vykdomieji kursai.

### Užduotis 8: HTTPS mobiliosios paslaugos ir poros

**Įrašai:** Kūryba `src/Aivana.MobileHost/` ASP.NET pagrindinis projektas, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Integracijos bandymai; modifikuoti sprendimą ir kontrolierius.

**Susiję: ** Mobiliojo ryšio paslaugos pokalbius kontroliuoti per tą patį vartotoją IPC. Keliai: poros, leistinas profilius / veiksmus, autentiškas veiksmų skundas, atšaukimas ir WebSocket būklė. Skundas apima unikalų komandos ID; valdytojas turi ribotą dedupliaciją. Įrodymai gali būti atšaukti ir saugomi per Windows apsaugą.

- [ ] Pridėti nesėkmingus testus nepagrįstai sąraše / vykdymo, pasibaigęs kodas, pakartotinai naudojamas kodas, atšauktas tokenas, negaliojančio kilmės, dvigubo komandos, per didelės apimties ir potvynių ribos.
- [ ] Įdiegti HTTPS tik klausytojas, naudotojo pasirinktas sąsajas, vietos sertifikavimo / pasitikėjimo instrukcijas, vieną kartą 6 skaitmeninis kodas, kuris baigiasi po 120 sekundžių, maksimalus penki bandymai pagal kodą ir tokenų atšaukimas. Įrašykite atsitiktinius tokenus; niekada neprisijungti į autentiškumą.
- [ ] Įgyvendinimas leidžia sąrašą stabilių veiksmų ID ir esamą maršrutą; atsisakyti savavališkai šalinti, maršrutus ir pakuotės įdiegimo komandas. Nustatykite aiškius kliento normos ribas (10 komandos / sekundė, 30 bursas) ir 64 KiB pranešimo ribas.
- [ ] Patikrinkite atgalinės integracijos testus be firewall taisyklių arba leisti LAN prieigą; bandymas uždarymo ir tokenų tęstinumą.

### Užduotis 9: Mobilusis panelis ir darbalaukio aptarnavimo kontrolės

**Įrašai:** Kūryba `src/Aivana.MobileHost/wwwroot/` Atsakymas į panelą ir `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Įtraukti naršyklės/mobiliojo patikrinimo scenarijus ir WPF Dūmų aprėptis.

**Susiję: ** Vartojimo užduotis 8 maršrutai tik. Paaiškinkite ryšio veiksmų tinklą, profilį, gyvas vertybes / pažangą, atšaukimą, ryšio statusą ir porą. Stovykla kontroliuoja paslaugų pradžią / sustojimą, adresą / sertifikavimo instrukcijas, leistinas profilius ir kliento atšaukimą.

- [ ] Pridėti nesėkmingų integracijos patikrinimų auth srautui, stale/missing veiksmų IDs, atnaujinti be veiksmų atkūrimo, atšaukti klientą ir paslaugų sustabdymą.
- [ ] Įdiegti prieinamą atsakingą sąsają naudojant vietinius turtus, jokių trečiųjų šalių scenarijus ar interneto priklausomybę. Tokenai niekada neatsiranda URL.
- [ ] Išbandykite naršyklės išdėstymą 360/768/1280 CSS px; patikrinti faktinius ekrano nuotraukas. Tikrinti tikru telefonu / planšetiniu kompiuteriu tik tada, kai naudotojas aiškiai leidžia LAN paslaugą.
- [ ] Dokumentas ir ataskaitos mobiliosios aparatūros tikrinimas atskirai nuo naršyklės emuliacijos.

### Užduotis 10: Įrenginio gebėjimai ir paprastas raktas stebėjimas

**Įrašai:** Kūryba `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modifikuoti RawKeyboardBindingService, KeyboardBinding, valdymo ir prietaiso diagnozė; pridėti testus.

**sąsajos:** `IKeyboardAdapter` eksponuoja pajėgumus, stabilią įrenginio tapatybę ir fizinius raktus (skanavimo kodas, išplėstinės vėliavos, spaudos / išleidimo). Modai atskiria stebėjimą, žemėlapį standartinį įvedimą, vietinį SDK ir vairuotojo išskirtinę..

- [ ] Pridėti nesėkmingų bandymų senoms jungtims, įveskite skirtumus, kalbos / išdėstymo nepriklausomumą, pakartotinius nuolaidas, neplug laikantis, dvigubą modelio abejingumą ir įvedimo atmetimą.
- [ ] Išplėskite stebėjimą į įprastus fizinius raktus, netrukdydami juos. Pridėti aiškią nuėmimą ir atnaujinti tapatybės tvarkymą.
- [ ] Rodyti realius adapterio gebėjimus ir aparatūros nepagrįstą būseną prietaiso sąsajoje; niekada neįtraukti Fn/dial palaikymo iš prekės ženklų.
- [ ] Run core/Windows/WPF patikrinimai su simuliuotais pranešimais ir skaitymui vien įrenginio sąrašu.

### Užduotis 11: gamintojas adapteriai

**Files: ** Sukurkite atskirus failus pagal `src/Aivana.Windows/KeyboardAdapters/` „LogitechG“, „RazerChroma“, „SteelSeriesGameSense“, „WootingAnalog“ ir bendrą „QmkVia“ vadybą; modifikuokite registrą/UI ir pridėkite adapterio protokolą.

**Susiję: ** Vartojimo užduotis 10 adapterio sutartys. Native bibliotekos įkraunamos tik iš patvirtintų programų / pardavėjo vietų; SDK nebuvimas yra neįmanoma galimybė. Apšvietimo ir įvedimo pajėgumai yra atskiri.

- [ ] Prieš įgyvendinant kiekvieną adapterį, patikrinkite dabartinius oficialius SDK pavadinimus, perpardavimo terminus, programinės įrangos priklausomybes ir palaikomus modelius; įrašykite šaltinius ir tikslius versijas.
- [ ] Pridėti nesėkmingų bandymų SDK nebuvimo / išleidimo architektūra, renginių spausdinimas / pakartotinis, išjungimas, klaidinga atsakas ir nepalaikytas įrenginys. Naudokite injekcines gimtojo skambučių abstrakcijas, skirtas deterministiniams patikrinimams.
- [ ] įgyvendinimas Logitech G raktas ir palaikomas apšvietimas atskirai; Razer Chroma apšvietimas Synapse Standartinis raktas žemėlapyje; SteelSeries GameSense Valstybė / apšvietimas; Wooting Analoginis tyrimas su konfigūruojamais ribomis / histerija; QMK/VIA Standartinis raktas nustatymo vadovas. nereikalaukite nuosavybės įvesties iš apšvietimo API.
- [ ] Validuokite suderinamus realius įrenginius, kai jie yra prieinami. Žymėkite kitus įdiegtus/hardware-testi, su modelio/firmware įrodymų; trūkstamas naudojamas SDK yra atskleistas išorinis vartas.

### Užduotis 12: Išskirtinis klaviatūros filtras

**Įrašai:** Kūryba `src/Aivana.Windows/KeyboardFilter/`, Įdiegti šaltinį `tools/keyboard-filter/`, Backend integracijos bandymai ir eksploatavimo dokumentacija. pasirašytas vairuotojo projektas yra reikalingas, jei nėra tinkamo licencijuoto backend vertinimo.

**Susiejimai: ** Backend ataskaitos įdiegtos / suderinamos / pasirašytos būklės; atskleidžia įrenginio specifinius užfiksuojimo pradžią / sustojimą, raktinius kraštus ir avarinį išleidimą. Atskiras stebėtojas atgauna per kontrolierių praradimą. Pagrindinė klaviatūra niekada nebus išskirtinė.

- [ ] Vertinamas vairuotojo pažymėjimas, pasirašytas dvejetainis kilmės ir dabartinis Windows 11/Secure suderinamumas. Įrašykite sprendimą „go/no“ Nėra vairuotojo įdiegimo įprastų bandymų metu.
- [ ] Parašykite sutarties testus, skirtus filtrų tikslinei parinkčiai, nesėkmei, širdies smūgio pabaigai, IPC praradimui, abejingumui, pertraukui / atšaukimui, raktų išleidimui ir ištrynimui.
- [ ] Įdiegimo taškas, aiškus atrankos / aktyvacijos interjero, avarinis išleidimas ir installer / uninstaller. Nepakeiskite mažo lygio šuolio / Raw Input laikrodžių.
- [ ] Įvertinkite atkūrimo bandymo mašinoje su dviem klaviatūromis: pasirinktas raktas pašalinamas tik pasirinktu įrenginiu; pagrindinis rašymas nepažeistas; sugadintas / sustabdytas / išjungtas įvedimas.
- [ ] Uždaro gamybos užbaigimas realiu pasirašytu / licencijuotu pagrindu ir fiziniais bandymais. Jei išorinis parašas / licencija nėra prieinamas, užduotis turi būti nepilnavertiška su tiksliu priklausomybės įrodymu.

### Užduotis 13: integruotas patikrinimas, vertimai ir išleidimo paketas

**Files: ** Pridėti `docs/testing/2026-10-12-six-extensions.md`; atnaujinti atitinkamą DE/EN/FR/IT dokumentaciją, vietos katalogus ir rūkymo patikrinimo taškus. Išsaugokite kitus dokumentus.

- [ ] Integruokite visus įrašus ir nuotraukas; užbaigti vertimus naudojant esamą 27 kalbos katalogą ir atskleisti neperžiūrėtas vertimus.
- [ ] bėgimas `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, ir `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Peržiūrėkite darbalaukio / mobiliųjų ekranų nuotraukas ir patikrinti 100/150/200 procentą Windows skalavimo, konfigūracijos apyvarta, įskiepio gyvenimo ciklas, tinklo atkūrimas ir tikras klaviatūros perjungimas.
- [ ] Publish in a new artifact directory with `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; įtraukti bendradarbiavimo / svetingumo binarų ir licencijos pranešimus. Nenaudokite įdiegtos programos.
- [ ] Ataskaita baigti bandymai, aparatinės įrangos netikrintos adapteriai ir išoriniai paleidimo vartai. Visi šeši siuntėjai turi atitikti jų priėmimo kriterijus, prieš skambindami visą paraišką.

## Savęs apžvalga

Visi šeši spec skirsniai žemėlapyje užduotys 1–12; užduotis 13 apima lokalizavimą, integruotą tikrinimą ir išleidimą. Pasirengimo ataskaita, vykdymo serializavimas, paveldėjimo konfigūracija, paslaptis, pakuotės pasitikėjimas ir fizinis įtaisas yra aiškūs savininkai ir bandymai. Išorinis vairuotojas / SDK / įrangos durys lieka neišsami, kol bus įrodyta, o ne skaičiuojama kaip programinės įrangos sėkmė.

## Vykdymo pasirinkimas naudotojo peržiūra

Rekomenduojama: gimtoji vykdymas šioje pokalbyje, užduotis pagal užduotį, nes dauguma modulių dalijasi sutartis ir dabartinė darbo vieta turi didelę esamą redakciją. Deleguotas vykdymas yra prieinamas, jei vartotojas jį pasirenka. Peržiūrėkite šį planą ir pasirinkite įgyvendinimo metodą prieš įgyvendinimą, kaip reikalauja rašymo planų darbo srautas.

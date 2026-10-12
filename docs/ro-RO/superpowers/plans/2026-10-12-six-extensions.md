<!-- generated-doc locale=ro-RO source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Planificarea extinderii

> **Pentru lucrătorii agenți: ** REQUIRED SUB-SKILL: Utilizați superputeri:plani de executare pentru execuție nativă sau superputeri:subagent-conducere-dezvoltare după ce utilizatorul selectează delegație. Urmăriți pașii cu checuri.

**Obiectiv:** Livrează șase extinderi integrate și suport de tastatură multi-producător bazat pe capacitate.

**Arhitectura: ** Conservă separarea Core / Windows / WPF Introduceți plugin-uri izolate și procese mobile cu IPC îngust autentificat; toate executările merg prin controlorul existent și curentul MacroRunner. Adaptorii de hardware anunță capacitățile detectate, mai degrabă decât suportul pe scară largă.

**Tehnică Stack:** C# și .NET 10, WPF, Windows UI automatizare, Win32 Raw Input, Cuvânt cheie: ASP.NET Core HTTPS/WebSocket, Cuvânt cheie.Json.

**Spec: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (aprobat de către utilizator).

## Constrângeri globale

- Configurația existentă trebuie să fie încărcată fără schimbare; adăugați tipurile de acțiune în loc de a reordona valorile lor persistente.
- Conservați profilurile existente, atribuțiile G1/G2, operațiunile locale și utilizarea fără cont.
- Raw Input oferă doar observații. Îndepărtarea exclusivă pe dispozitiv necesită un filtru de tastatură reală.
- Plug-in-ul executat nu trebuie să fie executat automat.
- Default pentru a dezactiva și restricționează interfațele configurate pentru serviciul mobil; nu există nici o ruptă de autentificare a textului.
- Livrarea scurtă/text nu este o dovadă că aplicația țintă a acceptat comanda.
- Nu înlocuiți aplicația instalată, instalați driver-uri, schimbați hârtia de tastatură reală sau accesul la rețea deschis ca un test incidental.
- Păstrați modificări neîncărcate. Nu implicați editări partajate existente ca parte a acestei caracteristici. Stage numai funcții care pot fi revizuite independent dacă sunt solicitate comenzi.

## Recenzie Focus

1. Duplicate modele de tastatură identice: reconectarea ambiguă trebuie să necesite re-selecție (Tasks 10–12).
2. Anularea în timpul UIA / furnizor / plug-in / rețea așteaptă: nu se efectuează nicio acțiune după anulare (Tasks 1, 4, 6, 8).
3. Utilizatorul editează în timp ce preavizul de magie sau de pachet este deschis: revizuirea trebuie să împiedice supraîncărcarea silențială (Tasks 3, 7).
4. Limbajul și layoutul fizic diferă: selectorii și legăturile de cod de scan nu trebuie să depindă de numele de afișare localizate (Tasks 4, 10).
5. Retriul rețelei și reîncepe plugin-ul: comenzi nu trebuie să fie executate în tăcere de două ori (Tasks 6, 8).

## Contractele partajate și ordinea dependenței

Tareci 1–3 stabilește executarea și setarea. Lucrări 4–5 construirea UIA. Funcțiile 6–7 oferă plug-in-uri. Funcțiile 8–9 oferă acces mobil. Task 10–12 oferă adaptatoare de tastatură și integrare a filtrelor. Task 13 validă produsul complet.

Fiecare sarcină urmează un test de eșec → un ciclu de implementare minimă → un test de trecere. O sarcină este completă numai atunci când este accesibilă prin intermediul cererii și verificările sale trec. Scaffoldurile, adaptorii de mușchi și documentația singure nu satisfac o livrare.

### Task 1: Verificare tipărită și rezistență compatibilă

**Filele:** Creați `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Modificare `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfață:** producţie `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` şi `VerifyAsync(ActionStep, CancellationToken)`, Amândouă se întorc `Task<ActionVerificationResult>`. Adăugați serializabil opțional `VerificationOptions` cu steagul activ și cu timout (100–60000 Publicați rezultatele tipate pe pas, inclusiv ID-ul de rulare, ID-ul de tipare și indexul de pas fără plăți de acțiune.

- [ ] Scrie verificări: moștenirea JSON păstrează valorile numerice de acțiune existente; opțiuni absente dezactivate de verificare; validarea defectuoasă a timpului; eșecul prealabil împiedică invocarea executorului; postcondiția defectuoasă nu produce nici un succes verificat; executorul necontrolat este necontrolabil; anularea oprește verificarea și pașii ulterioare.
- [ ] Run `dotnet run --project tests/Aivana.Tests -c Release`; confirmă că noile controale nu sunt implementate înainte de implementare.
- [ ] Contractele de implementare, validarea, conservarea clonării/snapshot și integrarea MacroRunner. Respectați politicile OnError stop/continue; păstrați feedback-ul existent al abonaților.
- [ ] Executați testele de bază; toate verificările sunt necesare.

### Task 2: Windows furnizori de verificare și UI de rezultat

**Filele:** Creați `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Modificare `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, puncte de intrare.

**Interfețe: ** WindowsActionVerification implementează Task 1 furnizor folosind audio, afișare, proces / fereastră și clienți OBS existente. Testele de disponibilitate și identificatorii; verificarea compară starea dorită după sondajul limitat. Producerea rezultatelor de controlare a fotografiilor consumate de WPF vizualizare istorică.

- [ ] Adăugați testele eșuante pentru ID-uri explicite de audio/display, OBS timout, starea observată necorespunzătoare, efectele de scurtare necunoscute, omisiunea de plată sensibilă și anulatul de sondaj, folosind furnizori de citire injectabile.
- [ ] Implementarea verificării adevărate de lectură pentru volum absolut, mute, luminozitate absolută, pregătire a procesului / ferestrei și comenzi susținute OBS. Acțiunile relative/toggle necesită un pre-stat capturat; altfel raportarea este neconfirmabilă.
- [ ] Adăugați controale de verificare pas cu pas și istorie de executare sigură care arată starea, cerința eșuată și textul de recuperare.
- [ ] Run core/Windows check-uri și WPF fum cu furnizori de stat injectat; nu există luminozitate reală sau scriere audio.

### Task 3: Crearea unui spațiu de lucru ghidat

**Filele:** Creați `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modificarea paletei și a punctelor de intrare.

**Interfețe: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; selecția conține ID-uri de șablon, căi executabile selectate, URL-uri, ID-uri audio/display, scena OBS și legături selectate. Discovery întoarce candidați cu ID-uri stabile și disponibilitate. Proiectul de revizuire a configurării pentru detectarea conflictelor.

- [ ] Adăugați testele eșuante pentru toate cele cinci familii de vrăjitoare solicitate, pași generate independent, obiective necesare lipsite, anulează fără mutare și salvează împotriva revizuirii modificate.
- [ ] Descoperiți aplicațiile prin căile de aplicații înregistrate și scurte Start Menu; dispozitive prin furnizori existente; scene prin interogare OBS. Păstrați selecția manuală disponibilă pentru aplicații nedescoperite.
- [ ] Implementare Selectați fluxul de lucru → Selectați obiectivele → Selectați controalele → Preview/validate → Save. Generă profiluri editabile și nu rulează niciodată în timpul setării.
- [ ] Adăugați controale de fum de vrăjitoare reale WPF pentru finalizare / anulare / conflict; executați toate testele.

### Sarcina 4: selectorii UIA și acțiunile limitate

**Filele:** Creați `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modele de modificare, validarea configurării și WinActionExecutor.

**Interfață:** Apendă `ActionKind.UiAutomation`; `UiElementSelector` Procesul de stocare, ID-ul de automatizare, tipul de control și criteriile de ancestor comandate. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Întoarcerea `Task`; operațiunile Invoke, SetValue, Select, Toggle, Focus și WaitState. rezoluția selectorului trebuie să returneze exact un element eligibil.

- [ ] Adăugați testele eșuate împotriva unei ferestre de testare dedicate pentru relocare / rezizare, duplicare a meciurilor, elemente lipsite, modele neajutorate, controale dezactivate, valori de parolă, etichete Unicode și timout / anulare.
- [ ] Implementarea UIA pe un angajat dedicat cu cereri limitate și proces de țintă. Furnizorii potențiali care se încadrează în spatele unei granițe lucrătorilor reînnoite; abandonarea unei cereri nu trebuie să lase efecte secundare.
- [ ] Excludeți valorile de parolă; raportează restricțiile de acces/integritate în loc să le depășească.
- [ ] Run Windows controale folosind fixări izolate; adăugați doar valori enum și verificați rezistența moștenirii.

### Task 5: UIA picker și înregistrare revizuită

**File:** Creați `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modificați catalogul de acțiune, editorul și integrarea înregistrărilor; creați UIA WPF controale de fum.

**Interfețe: ** Picker returnează un `UiElementSelector`; înregistratorul returnează pașii propuse ale UIA și nu scrie niciodată direct într-un profil salvat. Comanda de testare returnează numărul de meciuri fără invocare.

- [ ] Adăugați controale neîntrerupte: pickerul exclude MAKRIVO și captura de valoare a parolei; înregistrarea / anularea paginilor de profil existent nu a fost schimbat; ambiguă raporturi de prevedere se potrivește; acceptarea pașilor înregistrate creează un proiect editabil.
- [ ] Implementarea elementului evidențiat/picker, parametrii specifice operației, previziunea selectorului și fereastra de revizuire a înregistrării. Răspundeți comportamentul existent al recordurilor.
- [ ] Adăugați intrări în catalog și acces la paletă; validarea erorilor editorului înainte de a salva.
- [ ] Răspundeți Windows și WPF controale de fumat pe fix; inspectați picker / editor ecran.

### Task 6: Hostul și protocolul plug-in izolat

**Filele:** Creați `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Modificare şi acţiune.

**Interfețe: ** Version-1 manifest specifică ID-ul pachetului / versiunea, punctul de intrare, schemele de parametri, acțiunile, furnizorii de stat și declarațiile de capacitate. Mesajele IPC conțin versiunea de protocol, ID-ul de cerere unic, metoda, parametrii JSON și rezultatul tipat. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` returnează un rezultat tipat. Aplicarea `ActionKind.Plugin`

- [ ] Adăugați testele eșuante pentru un protocol incompatibil, un mesaj defectuos/așezat (>1 MiB), ID-ul de cerere duplicat, anularea, accidentul, procesul nonresponsiv și acțiunea lipsită.
- [ ] Implementarea unui tip de utilizator numit IPC cu 10-secunde de manevră și de executare configurabilă capat la 60 secunde. Închideți lucrătorul neresponsabil; nu repetați automat o acțiune în zbor după redeschiderea.
- [ ] Implementarea schemelor și a serviciilor permise de controlor. Explică executabil-plugin încredere; izolația procesului nu este o cutie de nisip a sistemului de operare.
- [ ] Executați testele de bază și testele de integrare împotriva proceselor de testare reală.

### Task 7: pachete de plug-in și integrare de exemplu

**Filele:** Creați `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, testarea pachetelor; modificarea paletei/seturilor.

**Interfață:** Servicii de expunere `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` şi `UninstallAsync(packageId)`. Recenzii pentru pachetul de înregistrare hash, dependențe, versiune și capacități declarate.

- [ ] Adăugați teste de eșec pentru traversarea zip, căi absolute, simlink/reparse escape, intrări duplicate, >100 MiB date extinse, hash eronate, descărcare, revizuire de stale și importuri disabilitate.
- [ ] Implementare preview și activare explicită, erori de dependență, instalare etapă și upgrade rollback; descărcarea lucrătorului înainte de îndepărtare.
- [ ] Livrați un plugin de exemplu care returnează o acțiune de numărare configurabilă și starea de viață prin IPC, inclusiv anularea; pachetul / exportul / instalarea / dezinstalarea prin intermediul UI.
- [ ] Executați pachetul / testele de gazdă și WPF controale de final. Verificați că nu există runde executabile importate înainte de autorizare.

### Task 8: Serviciul HTTPS pentru telefoane mobile

**Filele:** Creați `src/Aivana.MobileHost/` Proiectul ASP.NET, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Testul de integrare; modificarea soluției și controlorul.

**Interfețe:** Serviciul mobil se adresează controlorului prin intermediul IPC-ului de același utilizator. Rute: cuprinzătoare, profiluri/acțiuni permise, invocare de acțiune autentică, anulare și statutul WebSocket. Invocarea include ID-ul unic de comandă; controlorul deține deduplicarea limitată. Certificatele sunt revocabile și stocate prin protecție Windows.

- [ ] Adăugați testele eșuate pentru enumerarea/execuția neîndeplinită, codul expirat, codul reutilizat, tokenul revocat, originea invalidă, comanda duplicată, cadru excesiv și limitele de inundație.
- [ ] Implementarea HTTPS-un singur ascultator, interfețe selectate de utilizator, certificare locală furnizarea / instrucțiuni de încredere, coduri de pariere de o dată 6 după expirarea 120 secunde, max cinci încercări pe cod și revocarea tokenului. Stocati token-uri aleatorii; nu se inregistreaza niciodata.
- [ ] Implementarea permite identificări de acțiune stabilă listate și routing-ul existent; refuzul arbitrar de șeluri, căi și comenzi de instalare a pachetului. Configurați limitele explicite ale ratei per client (10 comenzi/sec, 30 burst) și 64 limitele de mesaj KiB.
- [ ] Verificați testele de integrare înapoi fără a adăuga reguli de firewall sau a permite accesul LAN; testarea închiderii și persistența tokenului.

### Task 9: Panourile mobile și controlul serviciului de desktop

**File:** Creați `src/Aivana.MobileHost/wwwroot/` panourile de răspuns și `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; adăugați scripturi de verificare a browserului/mobilului și WPF acoperire a fumului.

**Interfețe: ** Tare de consum 8 numai rute. Expoziți rețeaua de acțiune, picerul de profil, valorile live / progres, anularea, starea de conexiune și cuplarea. Desktop controlă serviciul start/stop, adresă/certificare instrucțiuni, profiluri permise și revocarea clientului plătit.

- [ ] Adăugați controale de integrare neîntreruptă pentru auth flow, ID-uri de acțiune stale / pierdute, reîncărcare fără replay de acțiune, client revocat și stop de serviciu.
- [ ] Implementarea accesibilă a UI-urilor responsive folosind active locale, fără scripturi terțe sau dependențe de internet. Nu apare niciodată în URL-uri
- [ ] Testează layout-ul browserului la 360/768/1280 CSS px; inspectează imaginile de ecran reale. Verificați cu un telefon real / tabletă numai după ce utilizatorul permite în mod explicit serviciul LAN.
- [ ] Verificarea hardware-ului mobil pentru documente și rapoarte separat de emulația browserului.

### Task 10: Capacități de dispozitiv și observație de cheie obișnuită

**Filele:** Creați `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modifică RawKeyboardBindingService, KeyboardBinding, validator și diagnosticul dispozitivului; adăugați teste.

**Interfețe: ** `IKeyboardAdapter` expune capabilități, identitate stabilă a dispozitivului și margini de cheie fizice (cod de scanare, steaguri extinse, presă / lansare). Modurile disting observarea, intrarea standard, SDK-ul nativ și driver-exclusiv. Legăturile speciale virtuale existente își păstrează comportamentul de reprezentare și migrație.

- [ ] Adăugați testele eșuante pentru legături vechi, diferențele Enter, independența limbii / plasamentului, descărcările repetate, descărcările în timp ce țin, ambiguitatea modelului duplicat și respingerea intrării.
- [ ] Extindeți observația la cheile fizice obișnuite fără a le suprimă. Adăugați captură explicită și reîncărcați manipularea identității.
- [ ] Afișează capacitățile reale ale adaptorului și starea testată pe hardware în UI-ul dispozitivului; niciodată nu indică suportul Fn/dial din numele mărcii.
- [ ] Run core/Windows/WPF verifică cu rapoarte simulate și un număr de dispozitive doar citite.

### Task 11: Adaptatoare de producător

**File:** Creați fișiere separate în `src/Aivana.Windows/KeyboardAdapters/` pentru LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog și ghidul QmkVia generic; modificați registru / UI și adăugați verificări de protocol adaptor.

**Interfețe: ** Tare de consum 10 Contractele de adaptare. Bibliotecile indigene sunt încărcate numai de la site-urile validate ale aplicației / vânzătorului; absența SDK este o capacitate inaccesibilă. Capacitățile de iluminat și de intrare sunt separate.

- [ ] Verificați titlurile de SDK oficiale actuale, termenii de redistribuire, dependențele software și modelele susținute înainte de implementarea fiecărui adaptor; sursele de înregistrare și versiunile exacte.
- [ ] Adăugați testele de eșec pentru arhitectura SDK absentă / ruptă, presă de evenimente / reluare / repetare, dezconectare, răspuns defectuos și dispozitiv neînsoțit. Utilizați abstracții injectabile pentru controale deterministe.
- [ ] Implementare Logitech intrare G-cheie și iluminare susținută separat; Razer iluminare crom plus Synapse standard-cheie de hărțuire; SteelSeries GameSense starea / iluminare; Wooting analiză analogă cu praguri configurabile / histereză; QMK / VIA ghid de setare standard-cheie. Nu pretindeți intrări proprietare de la APIs de iluminat.
- [ ] Validați dispozitivele reale compatibile acolo unde sunt disponibile. Marcați alții implementat / hardware-netestat, cu dovezi de model / firmware; lipsa de SDK utilizabil este o poartă externă dezvăluită.

### Task 12: Filtrarea cu tastatură exclusivă

**File: ** Creați `src/Aivana.Windows/KeyboardFilter/`, sursă de instalare sub `tools/keyboard-filter/`, testele de integrare și documentația de implementare. Un proiect dedicat semnat-driver este necesar dacă niciun backend licențiat corespunzător nu trece evaluarea.

**Interfețe: ** Backend rapoarte de stat instalate / compatibile / semnate; expune dispozitiv-specific de captură start / stop, margini cheie și eliberare de urgență. Un controlor separat restabilește pierderea controlorului. Keyboard-ul principal nu devine niciodată exclusiv prin default.

- [ ] Avaluați licența de conducere a candidatului, semnată de origine binară și compatibilitatea curentă Windows 11/Secure. Încercați să luați o decizie go/no. Nu se efectuează niciun test de conducere în timpul testelor.
- [ ] Scrieți testele de contract pentru selecția țintă a filtrului, defecțiunea, expirarea loviturii, pierderea IPC, ambiguitatea reconectării, pauza / anularea, eliberarea și eliminarea cheilor.
- [ ] Implementare backend, selecție explicită / activare UI, eliberare de urgență și instalator / dezinstalator. Nu înlocuiți heuristici de nivel scăzut/Raw Input de timp.
- [ ] Validați într-o mașină de testare recuperabilă cu două tastatură: cheia selectată este oprită numai pe dispozitivul selectat; tiparea principală nu este afectată; crash/suspend/uninstall restorează intrarea.
- [ ] Finalizarea producției de porțiune pe fundul real semnat / licențiat și testele fizice. În cazul în care semnarea/licența externă nu este disponibilă, țineți sarcina incompletă cu dovezi precise de dependență.

### Task 13: Verificare integrată, traduceri și pachete de eliberare

**File: ** Adăugați `docs/testing/2026-10-12-six-extensions.md`; actualizați documentația relevantă DE/EN/FR/IT, catalogurile de localizare și punctele de intrare de control al fumatului. păstrați alte edite de documentare.

- [ ] Integrați toate punctele de intrare și snapshots; completați traducerile folosind fluxul de lucru al catalogului 27 existent și dezvăluiți traducerile neprevăzute.
- [ ] Război `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, şi `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Inspectați ecranele desktop/mobile și verificați 100/150/200 procente Windows de scalare, călătoriile de configurare, ciclul de viață al plugin-ului, revocarea rețelei și reîncărcarea tastaturelor reale.
- [ ] Publicați într-un nou director cu `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; include binare de companie / gazdă și notificări de licență. Nu supraîncărcați aplicația instalată.
- [ ] Raportul a finalizat testele, adaptorii hardware-testate și ușile de lansare externe. Toate cele șase livrări trebuie să îndeplinească criteriile lor de acceptare înainte de a apela întreaga cerere completă.

## Planul de auto-review

Toate cele șase secțiuni de spec pentru Task 1–12; Task 13 acoperă localizarea, verificarea integrată și eliberarea. Raportarea disponibilității, serializarea execuției, configurarea moștenirii, secretele, încrederea în pachet și ambiguitatea dispozitivelor fizice au proprietari și teste explicite. Poarta externă/SDK/hardware rămâne incompletă până când este dovedit, mai degrabă decât numită succes software.

## Opțiunea de executare pentru recenzia utilizatorului

Recomandat: execuție nativă în acest chat, sarcină de sarcină, deoarece majoritatea modurilor împărtășesc contracte și spațiul de lucru actual conține edite existente substanțiale. Execuția delegată este disponibilă dacă utilizatorul o alege. Revizuiți acest plan și selectați metoda de executare înainte de implementare, așa cum este necesar de fluxul de lucru al planurilor de scriere.

<!-- generated-doc locale=ro-RO source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../README.md)

# MAKRIVO

[Documentare completă în toate 27 limbi](README.md) · [Manualul de utilizator complet](HANDBOOK.md)

**prin Aivana.** [Inventari si limitari de eliberare](FEATURES.md) · [Raportul de extindere](testing/2026-10-12-six-extensions.md) · [Asistentul de prototip local](testing/2026-10-12-local-meeting-assistant.md)

** engleză ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Un controlor de cheie locală Windows cu plăci în stil Stream Deck, profiluri, macrouri și detecție de tastatură. Interfața suportă 27 limbi. Nu este necesară nici o conexiune la un cont sau la un cloud pentru utilizarea zilnică.

[Documentare](README.md) · [Verificare](VERIFICATION.md) · [Raportul de testare](testing/2026-10-09-usp-release.md)

## să înceapă

După publicare, cererea este la `artifacts/win-x64/MAKRIVO.exe`. Păstrați întregul folder de ieșire împreună; îl puteți copia în altă parte. Windows 11 x64 este ținta inițială. Articolele construite sunt excluse din Git; un checkout de sursă nu include un executabil preconstruit.

Instalați .NET 10 SDK specificat în `global.json` pentru a construi și a rula din sursa:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publicați o ediție autoconținut fără a necesita un timp de funcționare .NET instalat separat:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Fluxul de lucru

1. Selectați un profil și creați un tile de acțiune.
2. Setarea numelui, a pictogramei, a culorii și a tastaturilor. Combinațiile Ctrl/Alt/Shift reduc activarea accidentală în timp ce scrie.
3. Adăugați unul sau mai multe pași: aplicații, fișiere, site-uri web, text, scurtături, media, volumul, schimbarea profilului sau întârzierile.
4. Salvaţi Modificările rămân un proiect până când sunt salvate; discard restabilește versiunea salvată. Butonul Tile Play și cheia sa utilizează aceeași cale de executare.
5. Reorder piese și pași macro cu butonuri drag-and-drop sau aruncă. Modelele simplifică setările. Macro poate repeta până la 100 ori. Anulează prin intermediul interfeței, meniu de urmărire a sistemului sau Ctrl+Alt+Escape în mod implicit.

Profilurile specifice aplicației folosesc un nume de proces fără calea sa completă. Comanda de profil se rup legăturile; profilul de bază este falimentul. Selecția automată a profilului trebuie să fie activată în Setări.

Închiderea ferestrului principal păstrează aplicația în sistem prin defect. Faceți dublu clic pe pictograma pentru a o deschide din nou; folosiți meniul pentru a ieși. Startup cu Windows este opțional și disabil în mod implicit. Hotkeys pot fi întrerupte centralizat.

## Acțiuni și comanda paletei

Adăugați Acțiune deschide un catalog care poate fi căutat cu 90 prețuri în opt categorii. Fiecare are o descriere si o previziune. Editorul macro utilizează același catalog pentru a adăuga sau a înlocui pașii. Alegerea unui pre-set în acest flux de lucru editor creează un proiect fără executare.

Paleta de comenzi se deschide de la titlu sau cu Ctrl+K în interiorul aplicației. Căutați acțiuni salvate prin profiluri, prețuri de catalog, schimbări de profil, navigație, pauze / rezumat, anulare macro și import / export. Filtrarea prin acțiuni salvate, funcții de catalog sau comenzi de aplicație. Selectați cheile Arrow, activați Enter, Ctrl+Enter editează o intrare editabilă și se închide Escape. Favoritele apar mai întâi. Comandele de catalog gata de utilizare pot fi executate imediat; pre-seturi care necesită setare pentru a deschide un proiect. Setările pot defini o scurtă paletă globală. Noile configurații folosesc Ctrl+Shift+Space; configurațiile existente nu primesc automat un nou scurtcircuit global.

Acțiunile audio stabilesc volumul exact de la 0 la 100 procente și dispozitivele mute, imute sau toggle și microfon. Alegeți un punct specific sau lăsați-l dezactivat pentru Windows default. Un punct de final salvat inaccesibil produce o eroare în loc de a controla un alt dispozitiv. Dispozitivele refresh încărcă dispozitivele conectate.

OBS Studio utilizează WebSocket v5: start/stop/toggle/pause/resume înregistrare; start/stop/toggle streaming; schimbarea scenelor; toggle intrare mute. Activarea serverului WebSocket sub Tools în OBS și setarea punctului său final în editor. Verificați Conectarea și Nume de încărcare citește versiunea OBS, scenele și intrările fără a începe înregistrarea sau fluxul. Alegeți numele din listă sau introduceți-le manual. Parolele pot fi salvate în Windows Credential Manager și sunt excluse din configurațiile exportate. Conectarea OBS nu este disponibilă pentru a raporta o eroare

Profile pot corespunde unui nume de proces și unei părți a unui titlu de fereastră. Ambele condiții trebuie să se potrivească. Profilul de blocare previne schimbarea automată a sesiunii. Capture Conditions de la Foreground utilizează ultima aplicație externă.

Text și clipboard acțiuni de sprijin `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, şi `{{user}}`. Datele de utilizare `yyyy-MM-dd`; Timpul de utilizare `HH:mm`. Locuitorii necunoscuți rămân neîncetat. `{{profile}}` identifică profilul acțiunii lansate; `{{process}}` identifică contextul extern al aplicației. conținutul clipboardului este citit numai atunci când este utilizat. acțiunea clipboardului scrie text, dar nu se pastează automat.

Acțiunile ferestrei sunt minimizate, maximizate, restaurate sau organizate printr-o fereastră pe partea stângă/dreapta a zonei de lucru a monitorului. Sistemul de acțiune este închis Windows. Aceste acțiuni se desfășoară numai atunci când sunt invocate în mod explicit.

Preview afișează pași, repetări și variabile fără executare. Undo și Redo păstrează până la 100 stări de proiect; cutii de text păstrează editarea nativă Ctrl+Z. Etichetați preferințele în meniul editor sau tile și filtrați-le pe panou. Conturile de executare se aplică numai sesiunii actuale.

Textul, scurtarea și acțiunile ferestre lansate de la interfața activă ultima fereastră țintă externă. Paleta afișează acest context țintă. Dacă ținta este închisă sau Windows refuză activarea, executarea se oprește cu un mesaj.

## Ajutor și feedback

Special-Key Wizard este pe pagina de tastatură și în paleta. Se înregistrează cheile G raportate de iCUE. Detect Key Press le captează temporar fără a-și executa acțiunile atribuite. Legăturile normale se întorc după detectare, anulare sau închidere. O funcție de catalog și cheia selectată este deschisă ca un proiect neînregistrat; se verifică legăturile duplicate.

Keyboard Overlay afișează layoutul de referință selectat și legăturile active. Păstrați Ctrl+Alt+F1 sau scoateți-l din paleta sau pagina de tastatură. Nu se concentrează și permite clicurilor de mouse să treacă. Modificările de profil și de legătură sunt actualizate în timp ce sunt vizibile; sunt evidențiate tipurile de cheie Windows. Controlurile specifice producătorului nu pot furniza un semnal-cheie echivalent Windows.

Feedback-ul de executare arată progresul de așteptare, etapă/repetare, finalizare, anulare sau eșec fără a se concentra sau expune textul introdus. Comandele executate confirmă executarea comenzii, nu un stat de viață solicitat independent într-o aplicație terță parte. Detalii de eroare apar în jurnalul de activitate.

## Dispozitivele și Corsair

Corsair G1 – G6 poate fi atribuit direct prin intermediul SDK oficial iCUE. Selectați dispozitivul și cheia în editorul de acțiune și salvați, sau pregătiți un proiect din vizualizarea tastatură K100. Legăturile se aplică dispozitivului selectat și profilului activ; pauza le dezactivează, de asemenea. iCUE trebuie să fie în funcțiune și să permită accesul. Fn, roata iCUE, profilul și controlul de blocare nu pot fi hărțuite prin acest API-ul G-key.

Pagina dispozitivelor prezintă Windows interfețe de tastatură și refresh periodic și când dispozitivele sunt conectate. Un tastatură fizică poate expune mai multe interfețe HID. Hotkeys standard funcționează în rândul producătorilor.

Corsair detecția utilizează informațiile disponibile ale dispozitivului. Controlul RGB necesită hardware compatibil, care funcționează iCUE și SDK oficial iCUE; aplicația raportează disponibilitatea reală. Alți producători primesc în prezent acțiuni de detectare și standard. În afara integrării Corsair G-key, controlurile proprietare funcționează atunci când Windows le primește ca intrări regulate. Aplicația nu instala un driver de tastatură.

## Limbile

Setările oferă 27 limbi: germană, engleză, franceză, spaniolă, italiană, portugheză, olandeză, cehă, slovacă, maghiară, bulgară, greacă, daneză, suedeză, norvegiană, finlandeză, estonă, latină, lituană, slovacă, croată, ucraineană, rusă, turcă și serbă în scriptul latin. Limba de afișare Windows urmează automat. Traducerile sunt înlocuite local. Layouts, hotkeys și nume create de utilizator sunt independente de limbajul interfeței.

Documentarea completă este disponibilă în toate 27 limbi de aplicare prin intermediul [Limbă index](README.md). în Traducerile sunt asistate cu mașină, cu link-uri de sursă și verificări structurale; recenziile vorbitorului nativ sunt separate. Aplicația acceptă, de asemenea, codurile regionale `en-US`, `de-DE`, `fr-FR` și `it-IT` și utilizează formatele lor numerice. Codurile lingvistice existente rămân compatibile.

## Layout și cercetarea producătorului

Vizualizarea tastatură oferă referințe ISO-DE/ANSI-US de dimensiuni depline și fără probleme, plus Corsair K100 layout-uri RGB. Geometria reprezintă pozițiile, cheile largi / numerice și ISO Enter. Dispozițiile standard sunt referințe generice. K100 dispozițiile urmează controale documentate fără a pretinde dimensiuni de milimetru exact. Alegeți varianta corectă de limbă/geometrie: un nume de model sau USB VID/PID nu stabilește o varianta specifică.

Mapping rezolvă codurile de scanare fizică folosind layoutul activ Windows. Fluxul de lucru pregătește un proiect și menține controale de salvare / conflict. Bindurile standard sunt hotkeys globale Windows, nu izolați la o singură tastatură conectată. Corsair G1 – G6 utilizează o integrare specifică dispozitivului iCUE. Alte controluri proprietare necesită un adaptor sau scurte configurate de producător. Enter-ul principal și tastatura numerică Enter partajă o identitate virtuală-cheie pentru hotkeys globale.

În cazul în care [Cercetarea producătorului](research/tastaturhersteller-sonderfunktionen.md) şi [CSV](research/tastaturhersteller-sonderfunktionen.csv) acoperă 74 grupuri de mărci / producători, distingând configuratorii, SDK-urile de intrare și API-urile de iluminat. [Layout dovezi](research/keyboard-layout-evidence.md) Modelul de înregistrare, SKU și diferențele de module. Cercetarea nu înseamnă că suportul hardware este implementat.

## date

Configurația este stocată în datele de aplicare locală. Importul/exportul utilizează JSON. Importul prezintă configurația, înlocuiește configurația curentă după confirmare și dezactiva legăturile importate până când sunt activate manual. Conținutul de acțiune textă nu este colectat ca un log de cheie. Cuvintele de creditare sunt separate de profilurile exportate.

## Verificare

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows checurile pot inspecta hardware-ul și configurează / eliberează temporar legăturile susținute iCUE. Luați-le fără un alt controlor folosind aceste legături. Verificările audio nu modifică volumul live sau setările mute.

Intrarea nativă nu poate depăși privilegiile Windows. Un controlor lansat în mod obișnuit nu poate trimite intrări la o aplicație de administrator. Hotkeys rezervat sau ocupat apar ca conflicte.

În cazul în care [Indicele de documentare](README.md) legătură cu proiectele istorice, planurile, cercetarea și rapoartele de testare date. Planurile istorice descriu scara lor înregistrată, nu o garanție actuală a suportului.

### Template de flux de lucru și fereastră țintă așteaptă

Catalogul include șase fluxuri de lucru editabile: spațiul de lucru, întâlnirea, streamingul, cercetarea, schimbarea proiectului și scanarea textului. Căutați `workflow.` în catalog sau în paleta de comenzi. Alegerea unui flux de lucru deschide un proiect neconsumat; nu funcționează imediat. Toate etapele sunt păstrate atunci când se adaugă un flux de lucru la un macro existent sau când se atribuie un cheie Corsair. Configurați URL-uri de exemplu, căi de program, profiluri și scene OBS înainte de a salva.

Pasul **Această fereastră țintă ** acceptă un nume de proces (cu sau fără `.exe`), un substring de titlu necorespunzător de caz și un timp de 1–600,000 ms. Cel puțin un filtru este necesar; ambele filtre trebuie să se potrivească atunci când sunt livrate. Macro-ul continuă numai după ce o fereastră vizibilă se potrivește și ajunge la prealabil. Utilizați un titlu specific atunci când mai multe ferestre împărtășesc un proces. Anularea sau terminarea se oprește următoarele etape. Pregătirea ferestrei nu garantează că un document este încărcat sau că un serviciu extern, cum ar fi OBS WebSocket este conectat.

Cuvânt cheie: căutarea **Crește luminozitatea ecranului**, **Reduce luminozitatea ecranului**, sau **Ecranul de lumină la 50 Procentul** în catalogul de acțiune. acțiuni relative schimbă luminozitatea prin 10 puncte procentuale; valorii fixe din 0 pentru 100. Dispozitivele interne suportate utilizează WMI; monitorele externe compatibile necesită DDC/CI activat în meniul lor. în mod implicit, acțiunea afectează toate ecranele susținute; un monitor explicit poate fi selectat în editor..

### Controlul extins

Deschideți paleta de comenzi cu **Ctrl+K în timp ce fereastra aplicației are focalizare**. Căutați pentru **Designerul fluxului de lucru**, **Record macro**, **Synchronize profili** sau **Cea cheie de diagnostic** (formarea urmează limba selectată). Designerul fluxului de lucru editează proiectul selectat: fișierul / procesul / condițiile ferestrei, numărarea avansată și oprirea / prelucrarea erorilor. Înregistratorul captează coduri scurte și așteaptă, nu tipărirea obișnuită; Escape oprește înregistrarea. Revizuiți rezultatul înainte de a-l adopta.

Într-un pas de lumină, selectați toate ecranele suportate sau un monitor specific. Un monitor selectat inadecvat produce o eroare, in loc sa schimbe silit un alt ecran. Tabloul de bord prezintă luminozitate live, audio și OBS lecturi atunci când sunt disponibile integrările respective; o lectură inaccesibilă nu este prezentată ca o valoare măsurată.

În editorul de cărămidă, atribuieți o cheie fizică specială susținută și alegeți imediat, tipărire, țineți sau dublu. Selectori de straturi de păstrare activează temporar o altă mapare și restabilește stratul anterior la eliberare. Gestiunile și straturile se aplică Corsair cheilor SDK G și cheilor speciale standard specifice dispozitivului, nu cheilor globale obișnuite. Capturile de cheie standard suportă F13 – F24 și cheile de tipărie. Fn-ul proprietar, profilul și controlul de dial necesită încă un adaptor de vânzător compatibil sau un vânzător care mapează la o cheie standard. Raw Input observă cheia și nu suprimă comportamentul normal Windows. Diagnosticele cheie explică profilul, pauza, conexiunea și problemele legate.

Sincronizarea profilului schimbă fișierele printr-o folderă pe care o alegeți, inclusiv o folderă gestionată de clientul dvs. existent în cloud. Revizuiți previziunea înainte de import; importurile încep să fie dezactivate și să elimine hărțile specifice hardware-ului. Revizuirile împiedică supraîncărcări silențiale și includerea conținutului de acțiune este o alegere explicită. MAKRIVO nu furnizează un cont cloud sau păstrează credențiale cloud. Traiul utilizează acum propriul icon M de teal MAKRIVO și afișează un semn de pauză amber atunci când este pauzat.

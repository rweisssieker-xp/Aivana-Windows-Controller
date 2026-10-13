<!-- generated-doc locale=ro-RO source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../README.md)

# MAKRIVO

[Documentare completă în toate 27 limbi](README.md) · [Manualul de utilizator complet](HANDBOOK.md)

**prin Aivana.** [Inventari si limitari de eliberare](FEATURES.md) · [Raportul de extindere](testing/2026-10-12-six-extensions.md) · [Asistentul de prototip local](testing/2026-10-12-local-meeting-assistant.md)

** engleză ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Controler local de comenzi rapide Windows cu dale în stil Stream Deck, profiluri, macrocomenzi și detectarea tastaturii. Interfața acceptă 27 de limbi. Utilizarea zilnică nu necesită cont sau conexiune cloud.

[Documentare](README.md) · [Verificare](VERIFICATION.md) · [Raportul de testare](testing/2026-10-09-usp-release.md)

## să înceapă

După publicare, aplicația se află la `artifacts/win-x64/MAKRIVO.exe`. Păstrați întregul folder de ieșire împreună; îl puteți copia în altă parte. Ținta inițială este Windows 11 x64. Rezultatele compilării sunt excluse din Git; copia codului sursă nu include un executabil precompilat.

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

1. Selectați un profil și creați o dală de acțiune.
2. Setați numele, pictograma, culoarea și comanda rapidă de la tastatură. Combinațiile Ctrl/Alt/Shift reduc activarea accidentală la tastare.
3. Adăugați unul sau mai mulți pași: aplicații, fișiere, site-uri, text, comenzi rapide, media, volum, schimbarea profilului sau întârzieri.
4. Salvați. Modificările rămân schiță până la salvare; renunțarea restabilește versiunea salvată. Butonul Play al dalei și comanda rapidă folosesc aceeași cale de execuție.
5. Reordonați dalele și pașii macrocomenzilor prin glisare și fixare sau butoanele săgeți. Șabloanele simplifică configurarea. Macrocomenzile se pot repeta de până la 100 de ori. Anulați din interfață, meniul zonei de notificare sau implicit cu Ctrl+Alt+Escape.

Profilurile specifice aplicației folosesc numele procesului fără calea completă. Ordinea profilurilor decide între potriviri egale; profilul implicit se folosește în lipsa potrivirii. Selectarea automată a profilului trebuie activată în Setări.

Închiderea ferestrei principale păstrează implicit aplicația în zona de notificare. Faceți dublu clic pe pictogramă pentru redeschidere; folosiți meniul pictogramei pentru ieșire. Pornirea cu Windows este opțională și dezactivată implicit. Comenzile rapide pot fi suspendate centralizat.

## Acțiuni și comanda paletei

Adăugare acțiune deschide un catalog căutabil cu 90 de presetări în opt categorii. Fiecare are descriere și previzualizare. Editorul macrocomenzilor folosește același catalog pentru adăugarea sau înlocuirea pașilor. Selectarea unei presetări în acest flux de editare creează o schiță fără execuție.

Paleta de comenzi se deschide de la titlu sau cu Ctrl+K în interiorul aplicației. Căutați acțiuni salvate prin profiluri, prețuri de catalog, schimbări de profil, navigație, pauze / rezumat, anulare macro și import / export. Filtrarea prin acțiuni salvate, funcții de catalog sau comenzi de aplicație. Selectați cheile Arrow, activați Enter, Ctrl+Enter editează o intrare editabilă și se închide Escape. Favoritele apar mai întâi. Comandele de catalog gata de utilizare pot fi executate imediat; pre-seturi care necesită setare pentru a deschide un proiect. Setările pot defini o scurtă paletă globală. Noile configurații folosesc Ctrl+Shift+Space; configurațiile existente nu primesc automat un nou scurtcircuit global.

Acțiunile audio setează precis volumul între 0 și 100 procente și dezactivează, activează sau comută sunetul dispozitivelor de ieșire și microfon. Selectați un punct final sau lăsați nesetat pentru implicitul Windows. Un punct final salvat indisponibil produce eroare în loc să controleze alt dispozitiv. Reîmprospătare dispozitive reîncarcă dispozitivele conectate.

OBS Studio folosește WebSocket v5: pornire/oprire/comutare/întrerupere/reluare înregistrare; pornire/oprire/comutare transmisie; schimbarea scenelor; comutarea sunetului de intrare. Activați serverul WebSocket din Instrumente în OBS și setați punctul final în editor. Verificare conexiune și Încărcare nume citesc versiunea OBS, scenele și intrările fără pornirea înregistrării sau transmisiei. Selectați numele din listă sau introduceți-le manual. Parolele se pot salva în Windows Credential Manager și sunt excluse din configurațiile exportate. Conexiunile OBS indisponibile raportează eroare.

Profile pot corespunde unui nume de proces și unei părți a unui titlu de fereastră. Ambele condiții trebuie să se potrivească. Profilul de blocare previne schimbarea automată a sesiunii. Capture Conditions de la Foreground utilizează ultima aplicație externă.

Acțiunile de text și clipboard acceptă `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` și `{{user}}`. Datele folosesc `yyyy-MM-dd`; orele `HH:mm`. Substituenții necunoscuți rămân neschimbați. `{{profile}}` identifică profilul acțiunii lansate; `{{process}}` contextul aplicației externe. Clipboardul se citește numai când se folosește acel substituent. Acțiunea de clipboard scrie text, dar nu îl lipește automat.

Acțiunile de fereastră minimizează, maximizează, restaurează sau aranjează fereastra în stânga/dreapta zonei de lucru a monitorului ei. Acțiunea de sistem blochează Windows. Aceste acțiuni rulează numai la invocare explicită.

Previzualizarea afișează pașii, repetările și variabilele fără execuție. Anulare și Refacere păstrează până la 100 de stări ale schiței; casetele de text păstrează editarea nativă Ctrl+Z. Marcați favorite în editor sau meniul dalei și filtrați-le în tablou. Numărul execuțiilor se aplică doar sesiunii curente.

Acțiunile de text, comenzi rapide și ferestre lansate din interfață activează ultima fereastră țintă externă. Paleta afișează acest context țintă. Dacă ținta s-a închis sau Windows refuză activarea, execuția se oprește cu mesaj.

## Ajutor și feedback

Asistentul tastelor speciale se află în pagina Tastatură și în paletă. Listează tastele G raportate de iCUE. Detectare apăsare tastă le capturează temporar fără executarea acțiunilor atribuite. Asocierile normale revin după detectare, anulare sau închidere. O funcție din catalog și tasta selectată se deschid ca schiță nesalvată; se verifică asocierile duplicate.

Suprapunerea tastaturii afișează dispunerea de referință selectată și asocierile active. Mențineți Ctrl+Alt+F1 sau comutați din paletă ori pagina Tastatură. Nu preia focalizarea și lasă clicurile să treacă. Profilurile și asocierile se actualizează cât este vizibilă; apăsările Windows obișnuite sunt evidențiate. Comenzile specifice producătorului pot să nu furnizeze un semnal de tastă Windows echivalent.

Feedbackul execuției arată așteptarea, progresul pasului/repetării, finalizarea, anularea sau eșecul fără preluarea focalizării ori expunerea textului introdus. Comenzi executate confirmă executarea comenzilor, nu o stare live interogată independent într-o aplicație terță. Detaliile erorilor apar în jurnalul activității.

## Dispozitivele și Corsair

Corsair G1–G6 pot fi atribuite direct prin SDK oficial iCUE. Selectați dispozitivul și tasta în editor și salvați sau pregătiți o schiță din vizualizarea K100. Asocierile se aplică dispozitivului selectat și profilului activ; pauza le dezactivează și pe ele. iCUE trebuie să ruleze și să permită accesul. Fn, rotița iCUE, comenzile de profil și blocare nu pot fi mapate prin acest G-key API.

Pagina dispozitivelor prezintă Windows interfețe de tastatură și refresh periodic și când dispozitivele sunt conectate. Un tastatură fizică poate expune mai multe interfețe HID. Hotkeys standard funcționează în rândul producătorilor.

Detectarea Corsair folosește informațiile disponibile despre dispozitiv. Controlul RGB necesită hardware compatibil, iCUE pornit și SDK oficial iCUE; aplicația raportează disponibilitatea reală. Alți producători beneficiază în prezent de detectare și acțiuni standard. În afara integrării Corsair G-key, comenzile proprietare funcționează când Windows le primește ca intrare normală. Aplicația nu instalează un driver de tastatură.

## Limbile

Setările oferă 27 de limbi: germană, engleză, franceză, spaniolă, italiană, portugheză, neerlandeză, polonă, cehă, slovacă, maghiară, română, bulgară, greacă, daneză, suedeză, norvegiană, finlandeză, estonă, letonă, lituaniană, slovenă, croată, ucraineană, rusă, turcă și sârbă cu alfabet latin. Automat urmează limba de afișare Windows. Traducerile sunt incluse local. Dispunerile, comenzile rapide și numele create de utilizatori sunt independente de limba interfeței.

Documentarea completă este disponibilă în toate 27 limbi de aplicare prin intermediul [Limbă index](README.md). în Traducerile sunt asistate cu mașină, cu link-uri de sursă și verificări structurale; recenziile vorbitorului nativ sunt separate. Aplicația acceptă, de asemenea, codurile regionale `en-US`, `de-DE`, `fr-FR` și `it-IT` și utilizează formatele lor numerice. Codurile lingvistice existente rămân compatibile.

## Layout și cercetarea producătorului

Vizualizarea tastaturii oferă referințe ISO-DE/ANSI-US complete și fără bloc numeric, plus dispuneri Corsair K100 RGB. Geometria reprezintă poziții, taste late/înalte și ISO Enter. Dispunerile standard sunt referințe generice. Dispunerile K100 urmează comenzile documentate fără a pretinde precizie milimetrică. Selectați varianta corectă de limbă/geometrie; un nume de model sau USB VID/PID nu stabilește o variantă anume.

Maparea rezolvă codurile fizice de scanare folosind dispunerea Windows activă. Fluxul pregătește o schiță și păstrează verificările de salvare/conflicte. Asocierile standard sunt comenzi rapide Windows globale, neizolate la o singură tastatură conectată. Corsair G1–G6 folosesc integrare iCUE specifică dispozitivului. Alte comenzi proprietare necesită adaptor sau scurtături configurate de producător. Enter principal și Enter numeric au aceeași identitate de tastă virtuală pentru comenzile rapide globale.

În cazul în care [Cercetarea producătorului](research/tastaturhersteller-sonderfunktionen.md) şi [CSV](research/tastaturhersteller-sonderfunktionen.csv) acoperă 74 grupuri de mărci / producători, distingând configuratorii, SDK-urile de intrare și API-urile de iluminat. [Layout dovezi](research/keyboard-layout-evidence.md) Modelul de înregistrare, SKU și diferențele de module. Cercetarea nu înseamnă că suportul hardware este implementat.

## date

Configurația se stochează în datele locale ale aplicației. Importul/exportul folosesc JSON. Importul previzualizează configurația, înlocuiește configurația curentă după confirmare și dezactivează asocierile importate până la activare manuală. Conținutul acțiunilor text nu se colectează ca jurnal de tastare. Parolele Credential Manager sunt separate de profilurile exportate.

## Verificare

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Verificările Windows pot inspecta hardware și configura/elibera temporar asocierile iCUE acceptate. Rulați-le fără alt controler care folosește acele asocieri. Verificările punctelor finale audio nu schimbă volumul curent sau setările de dezactivare a sunetului.

Intrarea nativă nu poate ocoli privilegiile Windows. Un controler lansat normal poate să nu poată trimite intrare unei aplicații de administrator. Comenzile rapide rezervate sau ocupate apar ca conflicte.

În cazul în care [Indicele de documentare](README.md) legătură cu proiectele istorice, planurile, cercetarea și rapoartele de testare date. Planurile istorice descriu scara lor înregistrată, nu o garanție actuală a suportului.

### Template de flux de lucru și fereastră țintă așteaptă

Catalogul include șase fluxuri editabile: spațiu de lucru, întâlnire, streaming, cercetare, schimbare proiect și fragment de text. Căutați `workflow.` în catalog sau paletă. Selectarea unui flux deschide o schiță nesalvată; nu rulează niciodată imediat. Toți pașii se păstrează la adăugarea fluxului într-o macrocomandă existentă sau atribuirea unei taste Corsair. Configurați URL-urile exemplu, căile programelor, profilurile și scenele OBS înainte de salvare.

Pasul **Așteptare fereastră țintă** acceptă numele procesului (cu sau fără `.exe`), fragment de titlu fără diferențiere majuscule/minuscule și expirare de 1–600000 ms. Este necesar cel puțin un filtru; dacă sunt furnizate ambele, ambele trebuie să corespundă. Macrocomanda continuă numai când fereastra vizibilă corespunzătoare răspunde și ajunge în prim-plan. Folosiți titlu specific când mai multe ferestre împart procesul. Anularea sau expirarea opresc pașii ulteriori. Pregătirea ferestrei nu garantează încărcarea documentului sau conectarea unui serviciu extern precum OBS WebSocket.

Luminozitate ecran: căutați **Crește luminozitatea ecranului**, **Scade luminozitatea ecranului** sau **Luminozitate ecran la 50 procente** în catalog. Acțiunile relative schimbă luminozitatea cu 10 puncte procentuale; valorile fixe sunt între 0 și 100. Ecranele interne acceptate folosesc WMI; monitoarele externe compatibile necesită DDC/CI activat în meniu. Implicit sunt afectate toate ecranele acceptate; se poate selecta un monitor explicit în editor. Conexiunile și stațiile de andocare pot restrânge suportul.

### Controlul extins

Deschideți paleta cu **Ctrl+K când fereastra aplicației are focalizarea**. Căutați **Proiectant flux de lucru**, **Înregistrare macrocomandă**, **Sincronizare profiluri** sau **Diagnosticare taste** (formularea urmează limba selectată). Proiectantul editează schița dalei selectate: condiții fișier/proces/fereastră, numărul pașilor următori de omis și oprire/continuare la eroare. Înregistratorul capturează combinații de comenzi rapide și așteptări, nu tastare obișnuită; Escape oprește înregistrarea. Revizuiți rezultatul înainte de preluare.

Într-un pas de luminozitate selectați toate ecranele acceptate sau un monitor. Monitorul selectat indisponibil produce eroare în loc să modifice discret alt ecran. Tabloul afișează citiri live de luminozitate, audio și OBS când integrările sunt disponibile; o citire indisponibilă nu este prezentată ca valoare măsurată.

În editorul dalei atribuiți o tastă fizică specială acceptată și selectați apăsare imediată, atingere, menținere sau atingere dublă. Selectorii de strat menținuți activează temporar altă mapare și restabilesc stratul anterior la eliberare. Gesturile și straturile se aplică tastelor G Corsair SDK și tastelor speciale standard specifice dispozitivului, nu comenzilor rapide globale obișnuite. Capturarea tastelor standard acceptă F13–F24 și tastele media. Comenzile proprietare Fn, profil și rotiță necesită adaptor compatibil al furnizorului sau mapare de către furnizor la tastă standard. Raw Input observă tasta și nu suprimă comportamentul Windows normal. Diagnosticarea explică problemele de profil, pauză, conexiune și asociere.

Sincronizarea profilurilor schimbă fișiere prin folderul ales, inclusiv unul administrat de clientul cloud existent. Revizuiți previzualizarea înainte de import; importurile încep dezactivate și elimină mapările specifice hardware-ului. Reviziile previn suprascrierile neanunțate, iar includerea conținutului acțiunilor este explicită. MAKRIVO nu oferă cont cloud și nu păstrează acreditări cloud. Pictograma din zona de notificare folosește acum M turcoaz al MAKRIVO și un indicator chihlimbariu la pauză.

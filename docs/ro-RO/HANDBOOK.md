<!-- generated-doc locale=ro-RO source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../HANDBOOK.md)

# MAKRIVO — Manual de utilizare

Producător: **Aivana**. Documentare snapshot: **2026-10-12**. Acest manual descrie implementarea curentă a copacului de lucru. Integrările experimentale și funcțiile dependente de hardware sunt identificate în mod explicit. rapoartele istorice descrie fotografiile lor date, nu o garanție pentru o nouă construcție.

## Documentare și limbi

Utilizați selectorul de limbă pentru a deschide manualul complet, inventariul de caracteristici, rezolvarea problemelor, notele de dezvoltare, cercetarea și rapoartele de testare istorică în oricare dintre limbile de aplicare 27. Traducerile generate sunt asistate cu mașină și au controale structurale; nu toate au primit o revizuire a vorbitorului nativ. Identificările tehnice și exemplele executabile rămân neîncărcate. Sursă originală rămâne disponibilă pentru comparație. Analiza de licență tradusă este informativă și nu înlocuiește termenii legali originali sau un EULA final.

## Instalare și lansare

Obiectivul inițial este Windows 11 x64. Păstrați fiecare fișier și subdirector de distribuție împreună. Înfiinţare `MAKRIVO.exe` Un checkout de sursă nu este un instalator. Pachetele auto conținute includ timpul de funcționare .NET; pachetele dependente de cadru necesită timpul de funcționare .NET 10 corespunzător pentru desktop și ASP.NET. ONNX și hosturile companiei trebuie incluse pentru funcțiile lor respective.

1. Începeți MAKRIVO și alegeți limba interfață în Setări. Se efectuează automat Windows. Layout-ul este o setare separată.
2. Alegeți un profil, apoi adăugați o pagină prin catalogul de acțiune căutat. Dă-i un nume recunoscut și alegeți acțiunile sale.
3. Revizuiți pașii și atribuiți o scurtare dacă este necesar. Evitați combinațiile deja utilizate de Windows sau aplicația dvs. țintă.
4. Salvează proiectul. Modificările neînregistrate nu sunt configurarea salvată.
5. Testează cu o acțiune inofensivă, cum ar fi deschiderea unui document și inspectează feedback-ul execuției.

Închiderea fereastră principală lasă în mod normal aplicația în zona de notificare. Faceți dublu clic pe pictograma pentru a o deschide din nou sau folosiți meniul pentru a ieși. Începând cu Windows este opțional și inițial disabil. Controlul pauzelor suspendă legăturile; pictograma afișează un semn de pauză. Anulează macro-urile care funcționează prin intermediul interfață, tray sau a scurtei de anulare configurate; standardul este `Ctrl+Alt+Escape`.

## Tile, acțiuni și paleta de comandă

Tile aparțin profilurilor și pot conține mai multe pași comandate. Editorul susține salvare/descarcare, preview, undo/redo și reordinează pasul. Catalogul furnizează prețuri pentru programe, documente, URL-uri, text, clipboard, scurtături, media, audio, ferestre, acțiuni de sistem și integrări susținute. Un preset care necesită parametri se deschide ca un proiect.

Deschideți paleta cu `Ctrl+K` în timp ce MAKRIVO are un accent. Caută acțiuni salvate, pre-sete, schimbați profil și instrumente. Selectați cheile de aruncă; Enter activate; `Ctrl+Enter` editează un element susținut; Escape se închide. Unele comenzi pregătite de utilizare se execută imediat, în timp ce acțiunile configurate necesită o revizuire. O scurtătură de paletă globală separată poate fi configurată; utilizează noi configurații `Ctrl+Shift+Space`.

Text și clipboard pași de suport `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` şi `{{user}}`. Datele de formare sunt `yyyy-MM-dd`; Timpul de formare este `HH:mm`. Variații necunoscuți rămân neschimbate.Clipboard-ul se întâmplă numai atunci când este solicitat.O acțiune clipboard scrie text fără a se potrivi automat.

Acțiunile lansate de la UI pot fi vizate de ultima aplicație externă. În cazul în care obiectivul dispare sau Windows blochează activarea prealabilă, activați manual obiectivul și retrageți-l. Livrarea de cheie de succes nu dovedește că obiectivul și-a îndeplinit sarcina.

## Profiluri și setări de locuri de muncă

Profile pot corespunde unui nume de proces și unei părți a unui titlu de fereastră. Când ambele sunt stabilite, ambele trebuie să se potrivească. Ordinul de profil rezolvă meciurile concurențiale; profilul standard este declinul. Permite selectarea automată explicită. Lock Profile împiedică modificările automate ale sesiunii curente.

Setarea locului de muncă deschis din Tools sau din paleta. Selectați programe, documente, site-uri web, un obiectiv audio/display sau o scenă OBS. Asistentul pregătește site-ul de lucru, întâlnirea, streamingul, cercetarea sau profilurile de proiect. Verificați pașii generati înainte de a economisi. Instalația nu execută acțiunile pregătite.

## Macro, condiții și înregistrare

Macros își execută pașii în ordine și susține așteptările, repetările și anularea. Limita de repetare este 100. Utilizați Preview înainte de a efectua acțiunile care se încadrează într-o altă aplicație sau schimbarea stării sistemului.

Așteptați pentru Fereastră de țintă acceptă un proces, un substrare de titlu și un timout de la 1 la 600,000 ms. Cel puțin un filtru este necesar. O fereastră vizibilă care se potrivește trebuie să răspundă și să devină prealabilă înainte de următoarele etape. O fereastră pregătită nu dovedește că un document sau un serviciu a terminat încărcarea.

Editorul fluxului de lucru adaugă fișier-exista, proces-currență și condiții de titlu. O stare falsă scapă pasul și poate trece mai departe. Refuză condiția de a exprima o ramură alternativă. Alegeți deliberat stop-on-error sau continu-on-error; continuarea poate lăsa pași ulterioare fără condițiile lor de așteptare.

Recordul de acțiune captează coduri și așteaptă, nu tipărirea obișnuită. Acesta susține modificările și cheile funcționale. Scapă încetează înregistrarea. Revizuiți lista și adoptați-o ca un proiect de țesătură nesădată, apoi salvați explicit. Înregistrarea UI click este un instrument separat descris mai jos.

## Keyboards, gesturi și straturi

Pagina de tastatură conține interfețe detectate și layout-uri de referință. O singură tastatură fizică poate expune mai multe interfețe. Alegeți limba și geometria potrivită; un singur nume de produs nu identifică ISO sau ANSI.

Hotkeys globale standard se aplică peste tastatură. Raw Input legăturile observă un anumit dispozitiv și codul de scanare, inclusiv funcția susținută și cheile media. **Raw Input Nu suprimă regulile Windows Un tastatură exclusivă nu este pregătită pentru producție.** Un backend semnat, licențiat și testat este încă necesar; nu este instalat de către MAKRIVO.

Conectările cu cheie specială pot folosi presiune imediată, tap, hold sau dublu tap. Cheile de strat selectați selectați temporar o altă mapare și restabiliți stratul de bază la eliberare; cheile de strat selectați nu își desfășoară propriul macro. Păstrează pragurile de la 150 la 5,000 ms; dublu-tap ferestre de la 150 la 1,000 ms. Aceste hărți sunt diferite de hotkeys obișnuiți.

Special-key-ul detectează cheile compatibile fără a-și executa acțiunile atribuite în timpul captării. Diagnosticul explică dispozitivele inaccesibile, legăturile pauzate, defectele de profil și conflictele. Suprafața afișează layout-ul selectat și legăturile; scurtarea sa de preferință este `Ctrl+Alt+F1`.

| Integrare | Cerințe și limite |
| --- | --- |
| Corsair | Running iCUE, acces SDK și un model compatibil. G1-G6 și funcții RGB susținute; Fn proprietare, roata, blocul și controlul de profil nu sunt, în general, expuse de API-ul G-key. |
| Logitech G | Bibliotecile locale x64 G-key/LED necesită aprobarea explicită. Evenimentele de cheie G nu identifică în mod unic mai multe tastatură fizică identică. |
| Razer | Chroma RGB și intrarea standardă; controlul proprietății în Synapse acolo unde este necesar. |
| SteelSeries | GameSense RGB și intrare standard; controlul proprietății de hartă în GG acolo unde este necesar. |
| Wooting | Compatibilă cu SDK-ul local original x64 pentru valori și praguri analogice; intrarea HID normală rămâne activă. |
| Alte tastatură | Hotkeys standard sau coduri de scan fizic susținute. Fn și controlul proprietății depind de model și de configurația vânzătorului. |

Cercetarea producătorului descrie capacitățile documentate, compatibilitatea nu este implementată sau testată cu fiecare dispozitiv enumerat. Noile adaptoare au nevoie de acceptare hardware reală. Nu descărcați binarile SDK din surse nesigure; drepturile de distribuție SDK sunt separate de compatibilitate tehnică.

## Audio, afișare și OBS

Etapele audio stabilesc volumul de la 0 la 100 procente și punctele de ieșire mute/unmute/toggle susținute sau microfon. Alegeți un punct specific sau utilizați standardul Windows. O țintă salvată lipsită produce o eroare în loc să controleze în tăcere un alt dispozitiv. Reîncărcați dispozitivele după reconectare. Controlul punctului final nu este o cerere de sprijin independent pe aplicație.

Acțiunile de afișare cresc / scad luminozitatea cu 10 puncte procentuale sau stabilește o valoare de la 0 la 100. Dispozitivele interne folosesc interfețe WMI susținute; monitorele externe necesită suport DDC/CI și, de obicei, setarea de meniu a monitorului. Docurile, adaptorii și cablurile pot bloca controlul. Alegeți toate ecranele suportate sau un singur monitor. Reconectarea poate schimba identitățile disponibile; revizuirea obiectivului salvat. Citirea lipsită este dovedită ca fiind inaccesibilă în loc de valori măsurate.

OBS necesită WebSocket v5 activat în OBS Instrumente. Configurați punctul final, testați conexiunea și încărcați numele de scenă / intrare înainte de a salva. Acțiunile susținute includ schimbarea scenei, înregistrarea start/stop/toggle, înregistrarea pauzei/resumei și mute de intrare. Parolele pot fi stocate în Windows Credential Manager și sunt excluse din exporturile de configurare. O comandă de succes OBS nu stabilește fiecare efect de streaming.

## Verificarea rezultatelor și istoria executării

Permiteți verificarea rezultatelor pentru pașii potriviți. Aplicația distinge controalele prealabile, efectele confirmate, eșecurile, anularea și efectele care nu pot fi verificate. Existența procesului nu este pregătirea de aplicare. Modificările relative, scurtăturile și intrarea textului nu pot primi o cerere de succes. Verificați istoricul activității și verificați atunci când o acțiune eșuează. Erorile de configurare trebuie corectate înainte de a rerunde macro-ul complet.

## Automatizarea și înregistrarea clicurilor

Adăugați o etapă de automatizare a UI, selectați operațiunea și captați ținta sub indicator după întârzierea afișată. capturarea nu execută nimic. verificați selectorul și necesită un match unic. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` şi `WaitState`. Câmpurile de parolă, controlele disabilizate și selectorii ambigu sunt respinse.

Automatizarea UI se desfășoară într-un proces de companie limitat. Depinde de sprijinul și permisiunile de accesibilitate Windows ale aplicației țintă. Înregistratorul de clic captează acțiunile și pauzele susținute; nu înregistrează intrarea generală a textului. revizuirea și validarea proiectului înainte de a-l adopta. Modificările de UI într-o aplicație externă pot dezactiva selectorii. Procesul de izolare limitează accidentele și anularea, dar nu este o cutie de nisip de securitate.

## Plug-uri

Deschideți extinderi pentru a verifica un pachet înainte de import. Structura pachetului, manifestul și hash-urile sunt verificate. Importul începe cu dizabilităţi. Citiți acțiunile declarate și încredințați explicit editorului înainte de a permite. Plug-in-ul funcționează cu permisiunile utilizatorului într-un proces separat; acest proces este **nu o cutie de nisip**. Instalați doar pachete pe care le încred.

Managerul susține exportul, dezactivarea, dezinstalarea și restabilirea unei versiuni anterioare. Dependența lipsită blochează invocarea. Pachetul de exemplu oferă conturi și acțiuni locale-stat. Schema susținută este un subset documentat al schemei JSON, nu toate caracteristicile posibile ale schemei JSON. Nu există piață publică plugin.

## Panourile mobile

1. Deschideţi Panourile mobile, selectaţi un IP local şi selectaţi în mod explicit profilurile de expunere. Adresa de tip default este accesibilă numai pe PC-ul însuși; selectați adresa LAN corespunzătoare pentru un telefon sau tabletă.
2. Începeți panourile și citiți adresa HTTPS, amprenta degetului și codul de pereche de șase cifre de unică utilizare.
3. Pe un dispozitiv din aceeași rețea de încredere, deschideți adresa. Comparați amprenta SHA-256 printr-un canal de încredere înainte de a încred în certificatul local. Aplicația nu instala automat un certificat de încredere.
4. Se utilizează codul afișat și se testează o țesătură inofensive. O permisiune manuală Windows Firewall poate fi necesară pentru accesul la LAN. Nu expuneți portul la internet.
5. Închideți panourile după finalizare sau revocați clienții plătiți prin intermediul aplicației.

Cheile de server private sunt protejate cu Windows DPAPI; credențialele clientului sunt stocate ca hashuri. Modificarea IP sau reînnoirea certificatului poate necesita o încredere reînnoită. Panelul afișează stările de acțiune și de verificare, nu toate posibilele senzori live. Layout-ul browserului și verificările locale HTTPS nu înlocuiesc acceptarea reală a telefonului/tabletului LAN.

## Ajutor de întâlnire

Acest asistent experimental opțional este disabilizat prin default. Deschideți-l din Instrumente, activați-l explicit, selectați un timp de lucru / model și adăugați doar resursele pe care doriți să le luați în considerare. Stabiliți obiectivul, creați un proiect, inspectați solicitările nerezolvate și transmiteți rezultatul editorului normal. Nimic nu se execută automat; salvarea și funcționarea rămân acțiuni explicite. Nu există o ruptura automată.

| Război | setare |
| --- | --- |
| Ollama | Servicii locale separate și un model instalat local la `127.0.0.1:11434`. Modelele de distanță / cloud declarate sunt respinse. |
| LM Studio | Serverul local cu un model încărcat local și suport pentru schema JSON la `127.0.0.1:1234`. Nu permiteți rotirea la distanță pentru prelucrarea locală. |
| llama.cpp | Modelul local `llama-server` și compatibil cu suportul schemei JSON la `127.0.0.1:8080` |
| ONNX Timpul de funcționare GenAI CPU | Folderul de model de text compatibil GenAI; selectați `genai_config.json`. Inference utilizează un proces de companie fără HTTP. |

Clienții HTTP folosesc doar loopback, nu folosesc proxy-uri și nu urmăresc redirecționări. MAKRIVO nu pot stabili cu încredere dacă un server local compatibil apelă intern la un serviciu la distanță. Serverele locale autentificate nu pot fi configurate în această versiune. Pesoarele de model, Python și serverele de inferență locale nu sunt descărcate sau instalate automat.

ONNX pachete au nevoie de `tokenizer.json` și un templu de chat valabil. Formularele de șablon susținute în `tokenizer_config.json` sunt un string `chat_template`, un dicționar cu un string `default`, sau o listă de șablonuri numite/default neambigu. Dacă șablonul este lipsit sau zero, `chat_template.jinja` este utilizat ca o descărcare. Modelele goale, ambigu, exagerate sau tipate incorect sunt respinse. Fișierele arbitrare ONNX, modelele de încorporare/vizualizare și pachetul GPU nu pot fi schimbate cu modelele de text GenAI compatibile. Versiunea de lucru GenAI este 0.17.1, CPU. Folosiți doar modele de încredere; separarea procesului nu este o cutie de nisip.

Asistentul susține programe selectate fără argumente, documente selectate și site-uri web și microfon mute la cerere expresă. Nu creează mesaje, întâlniri, scripte arbitrare, clicuri gratuite, descărcări sau schimbări de headset. Obiectivul și numele resurselor sunt trimise la timpul de funcționare local selectat; căile de resurse și conținutul fișierului nu sunt furnizate separat. Un scop tipizat de utilizator poate conține în sine date cu caracter personal. Comportamentul propriu al unui runtime local rămâne responsabilitatea utilizatorului.

Bugetul de cerere ONNX este 90 secunde, inclusiv încărcarea modelului, până la 3,072 tokenuri rapide și 1,024 generate, de asemenea, limitat de contextul modelului. ID-urile de resurse necunoscute și JSON defavorizate sunt respinse. Anulează sau se apropie de finalizarea unei cereri. Un test de timp aleator stabilește executarea, nu calitatea de planificare utilă sau economisirea timpului. Log-urile locale de experimente conțin identificatori de evenimente și orare, nu conținut text țintă sau document; acestea nu sunt încărcate automat.

Evaluarea ulterioară a modelului Instruct a produs planuri valabile zero în patru teste germane/angleze. Inferența nativă a lucrat, dar modelul a eşuat contractul de planificare strictă. Acesta este un rezultat negativ, nu un model recomandat sau un asistent util dovedit. Vezi raportul de testare în timp local pentru revizuirea exactă a modelului și limitele.

## Import, export, sincronizare și backup

Verificați previziunile de import înainte de a înlocui o configurație. Acțiunile importate încep să fie dezactivate, iar sincronizarea portabilă îndepărtează legăturile specifice hardware-ului. Reîncărcați dispozitivele și permiteți acțiunile deliberate după inspecție. Revizuirea conținutului text/acțiune înainte de a împărtăși un export; excluderea parolelor nu face toate conținutul de acțiune creat de utilizator necorespunzător.

Sincronizarea profilului schimbă fișierele într-o folderă selectată, opțional gestionată de clientul dvs. cloud existent. MAKRIVO nu furnizează conturi cloud și nu stocă credențiale cloud. Revizuirea previne supraîncărcarea tăcută. Includerea conținutului de acțiune este o alegere explicită. Întoarceți configurația înainte de a efectua o importare de schimb. Configurația standard este `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; numele istoric al directoriei este păstrat pentru compatibilitate. Închideți aplicația înainte de a o copia / restabili manual. Autorizațiile SDK stocate și credențialele protejate de OS pot fi necesare pentru a fi recreate pe un alt PC.

## rezolvarea problemelor

| Simptomele | Ce să verifice |
| --- | --- |
| Shortcut nu face nimic | Aplicația funcționează, legăturile nu sunt pauzate, profilul activ corect, tile salvate / activate, conflicte de scurtă durată și obiectivele disponibile. |
| Fereastra nu poate fi activata | Activarea manuală. Windows restricțiile de frontieră și diferențele de privilegiu pot bloca automatizarea. |
| Cheia specială nu este detectată | Serviciul de furnizare/SDK/modele de suport; Configurați controlul proprietății pentru a emite un cheie standard susținut, dacă este posibil. |
| Al doilea tip de tastatură este de asemenea normal | Se așteaptă Raw Input Suprimarea exclusivă nu este implementată pentru producție. |
| Audio sau luminozitate nu este disponibilă | Reîncărcați dispozitivele; verificați identitatea selectată, sesiunile de finisare, suportul WMI/DDC/CI și restricțiile de dotare/kablu. |
| OBS offline | WebSocket v5 activat, punctul final corect / parola, serviciul accesibil și numele de scenă / intrare valabile. |
| Automatizarea eșuează | Companion găzduiește, selector valabil unic, accesibil obiectiv, control susținut și permisiuni. Retrageți o perioadă tranzitorie numai după revizuirea obiectivului. |
| Panouri mobile inaccesibile | LAN, în loc de adresa de întoarcere, aceeași rețea, funcționarea serverului, profilurile aprobate, certificarea de amprenta / încredere și permisiunile firewall-ului manual. |
| Plug-ul eșuează | Integritatea pachetului, statul explicit de încredere/autorizat, dependențele, compatibilitatea protocolului și logul de executare. |
| Cererea eșuează | Timpul de funcționare local selectat corect, serviciul / modelul gata, schema JSON susținută, folderul de model compatibil și limitele de cerere. |
| Layout sau text pare greșit | Separați limba UI de geometria tastaturii; revizuiți fereastra, verificați scara afișării și raportați pagina / limbă / construcție exactă. |

Pentru un raport util, se includ versiunea Windows, construirea / comiterea aplicațiilor, limba, scala de afișare, modelul de hardware, o secvență minimă și jurnalul redactat relevant. Îndepărtează căile personale, conținutul de acțiune și credențiale înainte de publicare.

## Construcție, testare și ambalare

Foloseste SDK-ul in `global.json`. Din rădăcinile de la Robotic:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Testele de integrare Windows pot deschide ferestrele izolate și pot configura / elibera temporar legăturile furnizorului susținute. Luați-le fără un alt controlor care deține aceste legături. Testele de luminozitate/audio ale standardului sunt doar citite; testele de hardware explicite sunt separate. Protocolele de rezoluție, imaginile de ecran și graficele de inferență aleatorie nu trebuie reprezentate ca acceptare reală a hardware-ului/modelului.

Editorul complet colectează host-urile companiei, activele mobile, plugin-ul de exemplu, timpul de funcționare ONNX și notificările de dependență disponibile. Păstrează subdirectoarele. O construcție de succes nu este o aprobare de instalare semnată sau legală. Sursă și structuri pot evolua separat; comparați snapshot-ul documentării și comandați-vă cu versiunea instalată.

## Licență, confidențialitate și statutul de eliberare

Modelul comercial intenționat este o achiziție de o singură dată cu utilizarea permanentă a versiunii majore achiziționate; prețul final, permisiunea dispozitivului și termenii de actualizare trebuie să fie stabilite în termenii de vânzare efectivi. Un sistem de achiziție / activare nu este stabilit de acest manual. Nu este necesar un cont cloud pentru controlul zilnic.

Nu distribuiți binare SDK proprietate fără permisiunea dovedită. Păstrați licențe și notificări de la terți pentru fiecare dependență trimisă, inclusiv pachete .NET și ONNX. Modele de greutate au propriile termene. Auditul de licență datat prezintă unele noi dependențe și nu este o clauză pentru aceste aditivi. Repostitorul nu acordă în sine o licență generală de sursă deschisă.

Hardware-ul real al furnizorului, operațiunile telefonice / tabletă LAN, tranzițiile mixte-monitor/DPI, utilizarea pe termen lung, revizuirea lingvistică completă, suportul exclusiv al conducătorului și calitatea de planificare AI rămân lucrări separate de acceptare. Niciodată să nu dau aprobarea numai de la prezența de caracter.

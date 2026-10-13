<!-- generated-doc locale=ro-RO source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../HANDBOOK.md)

# MAKRIVO — Manual de utilizare

Producător: **Aivana**. Instantaneul documentației: **2026-10-12**. Acest manual descrie implementarea curentă din arborele de lucru al depozitului. Integrările experimentale și funcțiile dependente de hardware sunt identificate explicit. Rapoartele istorice descriu starea de la data lor, nu o garanție pentru o versiune mai nouă.

## Documentare și limbi

Folosiți selectorul limbii documentației pentru a deschide manualul complet, inventarul funcțiilor, depanarea, notele de dezvoltare, cercetarea și rapoartele istorice de testare în oricare dintre cele 27 de limbi ale aplicației. Traducerile generate sunt asistate automat și verificate structural; nu toate au fost revizuite de vorbitori nativi. Identificatorii tehnici și exemplele executabile rămân neschimbate. Sursa originală rămâne disponibilă pentru comparație. Analiza tradusă a licențelor este informativă și nu înlocuiește termenii juridici originali ai terților sau EULA finală.

## Instalare și lansare

Ținta inițială este Windows 11 x64. Păstrați împreună toate fișierele și subdirectoarele distribuției. Lansați `MAKRIVO.exe`. O copie a codului sursă din depozit nu este un program de instalare. Pachetele autonome includ mediul de execuție .NET; pachetele dependente de framework necesită mediile de execuție .NET 10 Desktop și ASP.NET corespunzătoare. ONNX și programele gazdă auxiliare trebuie incluse pentru funcțiile respective.

1. Porniți MAKRIVO și alegeți limba interfeței din Setări. Opțiunea Automat urmează limba Windows. Dispunerea tastaturii este o setare separată.
2. Alegeți un profil, apoi adăugați o dală prin catalogul de acțiuni cu funcție de căutare. Dați-i un nume ușor de recunoscut și alegeți acțiunile.
3. Revizuiți pașii și atribuiți o comandă rapidă dacă este necesar. Evitați combinațiile deja folosite de Windows sau de aplicația țintă.
4. Salvați schița. Modificările nesalvate nu fac parte din configurația salvată.
5. Testează cu o acțiune inofensivă, cum ar fi deschiderea unui document și inspectează feedback-ul execuției.

Închiderea ferestrei principale lasă de obicei aplicația în zona de notificare. Faceți dublu clic pe pictograma ei pentru redeschidere sau folosiți meniul pictogramei pentru ieșire. Pornirea odată cu Windows este opțională și inițial dezactivată. Comanda de pauză suspendă asocierile; pictograma afișează un indicator de pauză. Anulați macrocomenzile în curs din interfață, din zona de notificare sau cu scurtătura configurată pentru anulare; implicit este `Ctrl+Alt+Escape`.

## Dale, acțiuni și paleta de comenzi

Dalele aparțin profilurilor și pot conține mai mulți pași ordonați. Editorul permite salvarea/renunțarea, previzualizarea, anularea/refacerea și reordonarea pașilor. Catalogul oferă presetări pentru programe, documente, URL-uri, text, clipboard, comenzi rapide, media, audio, ferestre, acțiuni de sistem și integrări acceptate. O presetare care necesită parametri se deschide ca schiță.

Deschideți paleta cu `Ctrl+K` când MAKRIVO are focalizarea. Căutați acțiuni salvate, presetări, schimbarea profilului și instrumente. Săgețile selectează; Enter activează; `Ctrl+Enter` editează un element acceptat; Escape închide. Unele comenzi gata de utilizare rulează imediat, iar acțiunile configurate necesită revizuire. Se poate configura o scurtătură globală separată pentru paletă; configurațiile noi folosesc `Ctrl+Shift+Space`.

Pașii de text și clipboard acceptă `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` și `{{user}}`. Formatul datei este `yyyy-MM-dd`; formatul orei este `HH:mm`. Variabilele necunoscute rămân neschimbate. Clipboardul este citit numai la cerere. O acțiune de clipboard scrie text fără să îl lipească automat.

Acțiunile lansate din interfață pot viza ultima aplicație externă. Dacă ținta dispare sau Windows blochează activarea în prim-plan, activați ținta manual și încercați din nou. Transmiterea cu succes a tastelor nu dovedește că ținta și-a încheiat sarcina.

## Profiluri și setări de locuri de muncă

Profilurile pot corespunde unui nume de proces și unei părți din titlul ferestrei. Dacă sunt definite ambele, ambele trebuie să corespundă. Ordinea profilurilor decide între potriviri concurente; profilul implicit se folosește în lipsa unei potriviri. Activați explicit selectarea automată. Lock Profile împiedică schimbările automate în sesiunea curentă.

Deschideți Configurarea spațiului de lucru din Instrumente sau paletă. Selectați programe, documente, site-uri, o țintă audio/ecran sau o scenă OBS. Asistentul pregătește profiluri de lucru, întâlnire, streaming, cercetare sau proiect. Inspectați pașii generați înainte de salvare. Configurarea nu execută acțiunile pregătite.

## Macro, condiții și înregistrare

Macrocomenzile execută pașii în ordine și acceptă așteptări, repetări și anulare. Limita generală de repetare este 100. Folosiți Previzualizare înainte de acțiuni care tastează în altă aplicație sau modifică starea sistemului.

Așteptare fereastră țintă acceptă un proces, un fragment de titlu și un timp de expirare între 1 și 600000 ms. Este necesar cel puțin un filtru. O fereastră vizibilă corespunzătoare trebuie să răspundă și să ajungă în prim-plan înainte de continuarea pașilor. O fereastră pregătită nu dovedește că documentul sau serviciul a terminat încărcarea.

Editorul fluxului de lucru adaugă condiții privind existența fișierelor, rularea proceselor și titlul ferestrei. O condiție falsă omite pasul și poate omite și alți pași următori. Negați condiția pentru a exprima o ramură alternativă. Alegeți deliberat oprirea sau continuarea la eroare; continuarea poate lăsa pașii ulteriori fără condițiile prealabile așteptate.

Înregistratorul de acțiuni capturează combinații de taste rapide și așteptări, nu tastarea obișnuită. Acceptă comenzi rapide cu taste modificatoare și taste funcționale. Escape oprește înregistrarea. Revizuiți lista și preluați-o ca schiță nesalvată de dală, apoi salvați explicit. Înregistrarea clicurilor din interfață este un instrument separat, descris mai jos.

## Keyboards, gesturi și straturi

Pagina tastaturii listează interfețele detectate și dispunerile de referință. O singură tastatură fizică poate expune mai multe interfețe. Selectați limba și geometria corecte; numele produsului singur nu identifică dispunerea ISO sau ANSI.

Comenzile rapide globale standard se aplică tuturor tastaturilor. Asocierile Raw Input specifice dispozitivului urmăresc un anumit dispozitiv și cod de scanare, inclusiv tastele funcționale și media acceptate. **Raw Input nu suprimă introducerea obișnuită în Windows. O a doua tastatură exclusivă nu este pregătită pentru utilizare în producție.** Este încă necesar un backend cu driver de filtrare semnat, licențiat și testat; MAKRIVO nu instalează niciunul.

Asocierile acceptate pentru taste speciale pot folosi apăsare imediată, atingere, menținere sau atingere dublă. Tastele de strat menținute selectează temporar altă mapare și restabilesc stratul de bază la eliberare; tastele de selectare a stratului nu rulează propria macrocomandă. Pragurile de menținere sunt între 150 și 5000 ms; ferestrele de atingere dublă între 150 și 1000 ms. Aceste mapări sunt distincte de comenzile rapide globale obișnuite.

Asistentul pentru taste speciale detectează tastele compatibile fără a executa acțiunile atribuite în timpul capturării. Diagnosticarea explică dispozitivele indisponibile, asocierile suspendate, nepotrivirile profilurilor și conflictele. Suprapunerea afișează dispunerea și asocierile selectate; scurtătura implicită de menținut apăsată este `Ctrl+Alt+F1`.

| Integrare | Cerințe și limite |
| --- | --- |
| Corsair | iCUE în funcțiune, acces SDK și un model compatibil. G1–G6 și funcțiile RGB acceptate; comenzile proprietare Fn, rotiță, blocare și profil nu sunt în general expuse de G-key API. |
| Logitech G | Bibliotecile SDK locale originale x64 G-key/LED necesită aprobare explicită. Evenimentele G-key nu identifică unic mai multe tastaturi fizice identice. |
| Razer | Chroma RGB și intrare standard; mapați comenzile proprietare în Synapse unde este necesar. |
| SteelSeries | GameSense RGB și intrare standard; mapați comenzile proprietare în GG unde este necesar. |
| Wooting | SDK local original x64 compatibil pentru valori și praguri analogice; intrarea HID normală rămâne activă. |
| Alte tastaturi | Comenzi rapide standard sau asocieri acceptate prin coduri fizice de scanare. Fn și comenzile proprietare depind de model și de configurația furnizorului. |

Cercetarea producătorilor descrie capabilități documentate, nu compatibilitate implementată sau testată cu fiecare dispozitiv enumerat. Noile adaptoare necesită încă acceptanță cu hardware real. Nu descărcați binare SDK din surse nesigure; drepturile de distribuție SDK sunt separate de compatibilitatea tehnică.

## Audio, afișare și OBS

Pașii audio setează volumul între 0 și 100 procente și dezactivează/activează/comută sunetul punctelor finale de ieșire sau microfon acceptate. Alegeți un punct final specific sau pe cel implicit Windows. Lipsa țintei salvate produce o eroare în loc să controleze discret alt dispozitiv. Reîmprospătați dispozitivele după reconectare. Controlul punctelor finale nu implică suport pentru mixer independent per aplicație.

Acțiunile de ecran cresc/scad luminozitatea cu 10 puncte procentuale sau stabilesc o valoare între 0 și 100. Ecranele interne folosesc interfețe WMI acceptate; monitoarele externe necesită DDC/CI și de obicei activarea din meniul monitorului. Stațiile de andocare, adaptoarele și cablurile pot bloca controlul. Selectați toate ecranele acceptate sau un monitor. Reconectarea poate schimba identitățile disponibile; verificați ținta salvată. Citirile lipsă apar ca indisponibile, nu ca valori măsurate.

OBS necesită activarea WebSocket v5 din Instrumentele OBS. Configurați punctul final, testați conexiunea și încărcați numele scenelor/intrărilor înainte de salvare. Sunt acceptate schimbarea scenei, pornirea/oprirea/comutarea transmisiei și înregistrării, întreruperea/reluarea înregistrării și dezactivarea sunetului de intrare. Parolele pot fi stocate în Windows Credential Manager și sunt excluse din exporturile configurației. O comandă OBS reușită nu confirmă toate efectele ulterioare ale transmisiei.

## Verificarea rezultatelor și istoria executării

Activați Verificare rezultat pentru pașii potriviți. Aplicația distinge verificările preliminare, efectele confirmate, eșecurile, anularea și efectele neverificabile. Existența procesului nu înseamnă că aplicația este pregătită. Modificările relative, comenzile rapide și introducerea textului nu pot primi o confirmare generală de succes. Inspectați jurnalul activității și istoricul verificărilor când o acțiune eșuează. Corectați erorile de configurare înainte de rerularea întregii macrocomenzi.

## Automatizarea și înregistrarea clicurilor

Adăugați un pas UI Automation, selectați operația și capturați ținta de sub indicator după întârzierea afișată. Capturarea singură nu execută nimic. Verificați selectorul și impuneți o potrivire unică. Sunt acceptate `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` și `WaitState`. Câmpurile de parolă, controalele dezactivate și selectorii ambigui sunt respinși.

Automatizarea interfeței rulează într-un proces auxiliar cu limite. Depinde de suportul de accesibilitate Windows și de permisiunile aplicației țintă. Înregistratorul de clicuri capturează acțiunile acceptate și pauzele, nu introducerea generală de text. Revizuiți și validați schița înainte de preluare. Schimbările interfeței aplicației externe pot invalida selectorii. Izolarea proceselor limitează efectele blocărilor și delimitează anularea, dar nu este un sandbox de securitate.

## Plug-uri

Deschideți Extensii pentru a inspecta un pachet înainte de import. Se verifică structura pachetului, manifestul și valorile hash. După import, pachetul este dezactivat. Citiți acțiunile declarate și acordați explicit încredere editorului înainte de activare. Pluginurile rulează cu permisiunile utilizatorului într-un proces separat; acest proces **nu este un sandbox**. Instalați numai pachete în care aveți încredere.

Managerul permite exportul, dezactivarea, dezinstalarea și restaurarea/eliminarea unei versiuni anterioare. Dependențele lipsă blochează invocarea. Pachetul exemplu oferă acțiuni de numărătoare inversă și stare locală. Schema acceptată este un subset documentat al JSON-Schema, nu toate funcțiile posibile ale JSON-Schema. Nu există o piață publică de pluginuri.

## Panou mobil

1. Deschideți Panoul mobil, selectați un IP local și alegeți explicit profilurile de expus. Adresa implicită loopback este accesibilă numai de pe PC; selectați adresa LAN potrivită pentru telefon sau tabletă.
2. Porniți panoul și citiți adresa HTTPS, amprenta certificatului și codul de asociere de șase cifre, de unică folosință.
3. Pe un dispozitiv din aceeași rețea de încredere, deschideți adresa. Comparați amprenta SHA-256 printr-un canal de încredere înainte de a încred în certificatul local. Aplicația nu instala automat un certificat de încredere.
4. Asociați folosind codul afișat și testați o dală inofensivă. Poate fi necesară o permisiune manuală Windows Firewall pentru acces LAN. Nu expuneți portul la internet.
5. Opriți panoul la final sau revocați clienții asociați din aplicație.

Cheile private ale serverului sunt protejate cu Windows DPAPI; acreditările clienților sunt stocate ca hashuri. Schimbarea IP-ului sau reînnoirea certificatului poate necesita reînnoirea încrederii. Panoul afișează stările acțiunilor și verificărilor, nu orice senzor live posibil. Verificările aspectului în browser și HTTPS local nu înlocuiesc acceptanța LAN cu telefoane/tablete reale.

## Asistent AI local pentru întâlniri

Acest asistent experimental opțional este dezactivat implicit. Deschideți-l din Instrumente, activați-l explicit, selectați mediul de execuție/modelul și adăugați numai resursele dorite. Definiți obiectivul, creați o schiță, inspectați solicitările nerezolvate și transferați rezultatul în editorul normal. Nimic nu se execută automat; salvarea și rularea rămân acțiuni explicite. Nu există trecere automată la un mediu de execuție alternativ.

| Mediu de execuție | setare |
| --- | --- |
| Ollama | Servicii locale separate și un model instalat local la `127.0.0.1:11434`. Modelele de distanță / cloud declarate sunt respinse. |
| LM Studio | Server local cu model încărcat local și suport de schemă JSON la `127.0.0.1:1234`. Nu activați rutarea la distanță pentru procesare locală. |
| llama.cpp | `llama-server` local și model compatibil cu suport de schemă JSON la `127.0.0.1:8080`. |
| ONNX Timpul de funcționare GenAI CPU | Folder local de model text GenAI compatibil și de încredere; selectați `genai_config.json`. Inferența folosește un proces auxiliar inclus, fără HTTP. |

Clienții HTTP folosesc numai loopback, nu folosesc proxy-uri și nu urmăresc redirecționări. MAKRIVO nu poate stabili în mod fiabil dacă un server local compatibil apelează intern un serviciu la distanță. Serverele locale care necesită autentificare nu pot fi configurate în această versiune. Ponderile modelelor, Python și serverele locale de inferență nu sunt descărcate sau instalate automat.

Pachetele ONNX necesită `tokenizer.json` și un șablon de conversație valid. Formele acceptate în `tokenizer_config.json` sunt un șir `chat_template`, un dicționar cu un șir `default` sau o listă neambiguă de șabloane denumite/implicite. Dacă șablonul lipsește sau este null, se folosește `chat_template.jinja` ca alternativă. Șabloanele goale, ambigue, supradimensionate sau cu tip incorect sunt respinse. Fișierele ONNX arbitrare, modelele de embeddings/viziune și pachetele exclusiv GPU nu pot înlocui modelele de text GenAI compatibile. Versiunea mediului GenAI inclus este 0.17.1, CPU. Folosiți numai modele de încredere; separarea proceselor nu este un sandbox.

Asistentul acceptă programele selectate fără argumente, documentele și site-urile selectate și dezactivarea microfonului numai la cerere explicită. Nu creează mesaje, programări, scripturi arbitrare, clicuri libere, descărcări sau comutarea căștilor. Obiectivul și numele resurselor sunt trimise mediului local selectat; căile resurselor și conținutul fișierelor nu sunt furnizate separat. Obiectivul tastat de utilizator poate conține el însuși date personale. Comportamentul propriu de rețea al mediului local rămâne responsabilitatea utilizatorului.

Limita cererii ONNX este de 90 de secunde, inclusiv încărcarea modelului, până la 3072 tokenuri de intrare și 1024 tokenuri generate, în limita contextului modelului. JSON malformat/trunchiat și ID-urile de resurse necunoscute sunt respinse. Anulați sau închideți pentru a termina cererea. Un test de execuție cu un model aleator dovedește execuția, nu o calitate utilă a planificării sau economii de timp. Jurnalele locale de experimente conțin identificatori de evenimente și durate, nu textul obiectivului sau conținutul documentelor; nu sunt încărcate automat.

Evaluarea ulterioară a unui model Instruct mic a produs zero planuri valide în patru probe în germană/engleză. Inferența nativă a funcționat, dar modelul nu a respectat contractul strict de planificare. Acesta este un rezultat negativ de calitate, nu un model recomandat sau un asistent cu utilitate demonstrată. Consultați raportul datat de testare a mediului local pentru revizia exactă a modelului și limite.

## Import, export, sincronizare și backup

Revizuiți previzualizările importului înainte de înlocuirea configurației. Acțiunile importate sunt inițial dezactivate, iar sincronizarea portabilă elimină asocierile specifice hardware-ului. Reatribuiți dispozitivele și activați deliberat acțiunile după inspecție. Revizuiți textele/acțiunile înainte de partajarea exportului; excluderea parolelor nu face ca tot conținutul acțiunilor create de utilizator să fie nesensibil.

Sincronizarea profilurilor schimbă fișiere într-un folder selectat, opțional administrat de clientul cloud existent. MAKRIVO nu oferă cont cloud și nu stochează acreditări cloud. Reviziile împiedică suprascrierile neanunțate. Includerea conținutului acțiunilor este o alegere explicită. Faceți backup configurației înainte de importul care o înlocuiește. Configurația implicită este `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; numele istoric al directorului se păstrează pentru compatibilitate. Închideți aplicația înainte de copierea/restaurarea manuală. Aprobările SDK salvate și acreditările protejate de sistemul de operare pot necesita recreare pe alt PC.

## rezolvarea problemelor

| Simptomele | Ce să verifice |
| --- | --- |
| Shortcut nu face nimic | Aplicația pornită, asocieri nesuspendate, profil activ corect, dală salvată/activată, conflicte de comenzi rapide și țintă disponibilă. |
| Fereastra nu poate fi activata | Activați manual. Restricțiile Windows pentru prim-plan și diferențele de privilegii pot bloca automatizarea. |
| Cheia specială nu este detectată | Serviciul de furnizare/SDK/modele de suport; Configurați controlul proprietății pentru a emite un cheie standard susținut, dacă este posibil. |
| A doua tastatură introduce și text normal | Comportament așteptat cu Raw Input. Suprimarea exclusivă nu este implementată pentru producție. |
| Audio sau luminozitate indisponibile | Reîmprospătați dispozitivele; verificați identitatea selectată, sesiunile punctelor finale, suportul WMI/DDC/CI și restricțiile stației de andocare/cablului. |
| OBS offline | WebSocket v5 activat, punctul final corect / parola, serviciul accesibil și numele de scenă / intrare valabile. |
| Automatizarea eșuează | Program gazdă auxiliar inclus, selector valid unic, țintă accesibilă și activată, control acceptat și permisiuni. Reîncercați după o expirare tranzitorie numai după verificarea țintei. |
| Panoul mobil nu este accesibil | Adresă LAN în loc de loopback, aceeași rețea, server pornit, profiluri aprobate, amprenta/încrederea certificatului și permisiuni manuale de firewall. |
| Plug-ul eșuează | Integritatea pachetului, statul explicit de încredere/autorizat, dependențele, compatibilitatea protocolului și logul de executare. |
| Cererea eșuează | Timpul de funcționare local selectat corect, serviciul / modelul gata, schema JSON susținută, folderul de model compatibil și limitele de cerere. |
| Dispunerea sau textul par greșite | Distingeți limba interfeței de geometria tastaturii; redimensionați fereastra, verificați scalarea ecranului și raportați pagina/limba/versiunea exactă. |

Pentru un raport util includeți versiunea Windows, versiunea/commitul aplicației, limba, scalarea ecranului, modelul hardware, o secvență minimă și jurnalul relevant cu datele sensibile eliminate. Eliminați căile personale, conținutul acțiunilor și acreditările înainte de publicare.

## Construcție, testare și ambalare

Folosiți versiunea SDK fixată în `global.json`. Din rădăcina depozitului:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Testele de integrare Windows pot deschide ferestre izolate și pot configura/elibera temporar asocierile acceptate ale furnizorilor. Rulați-le fără alt controler care deține aceste asocieri. Sondările implicite de luminozitate/audio sunt numai de citire; testele explicite de modificare și restabilire a hardware-ului sunt separate. Protocoalele simulate, capturile randate și grafurile aleatorii de inferență nu trebuie prezentate ca acceptanță reală a hardware-ului/calității modelului.

Instrumentul complet de publicare reunește programele gazdă auxiliare, resursele mobile, pluginul exemplu, mediul ONNX și notificările disponibile pentru dependențe. Păstrați subdirectoarele. O compilare reușită nu constituie un instalator semnat sau aprobare juridică de lansare. Sursele și versiunile compilate pot evolua separat; comparați instantaneul documentației și commitul cu versiunea instalată.

## Licență, confidențialitate și statutul de eliberare

Modelul comercial intenționat este o achiziție unică pentru utilizarea perpetuă a versiunii majore cumpărate; prețul final, numărul permis de dispozitive și condițiile de upgrade trebuie stabilite în termenii efectivi de vânzare. Acest manual nu stabilește existența unui sistem de achiziție/activare în producție. Nu este necesar cont cloud pentru controlul zilnic.

Nu distribuiți fișiere binare SDK proprietare fără permisiune dovedită. Păstrați licențele și notificările terților pentru fiecare dependență distribuită, inclusiv pachetele .NET și ONNX. Ponderile modelelor au propriii termeni. Auditul de licență datat precedă unele dependențe noi și nu constituie autorizare pentru aceste adăugiri. Faptul că depozitul este public nu acordă în sine o licență generală open-source.

Hardware-ul real al furnizorilor, operarea LAN pe telefoane/tablete, tranzițiile între monitoare/DPI diferite, utilizarea îndelungată, revizuirea lingvistică completă, suportul driverului pentru exclusivitate și calitatea utilă a planificării AI rămân activități separate de acceptanță. Nu deduceți aprobarea doar din prezența unei funcții.

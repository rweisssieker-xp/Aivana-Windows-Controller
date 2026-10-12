<!-- generated-doc locale=ro-RO source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: șase extinderi de produs și adaptoare de tastatură

Data în care: 2026-10-12
Status: proiectare propusă pentru recenziile utilizatorului; implementarea nu a început.

## Obiectiv
Livrați toate cele șase extinderi solicitate în aplicația existentă Windows / WPF, păstrând profilurile existente, atribuțiile G1/G2, funcționarea locală și utilizarea fără cont. Extindeți integrarea tastatură la Razer și Logitech, cu o arhitectură bazată pe capacitate reutilizabilă pentru alți producători. Un nume de producător sau un layout de tastatură nu este niciodată dovada faptului că sunt susținute controale proprietare.

## Decizia arhitecturii
Extinde Aivana.Core cu contracte și validare serializabile, Aivana Windows cu Windows și adaptoare de vânzări, și Aivana.Desktop cu editori tranzacționali și vizualizări de statut. Utilizați proiecte separate de companie pentru executarea izolată a plugin-ului și pentru serviciul local de panouri mobile. Evitați o reînnoire sau un singur proces privilegiat de găzduire a fiecărei extinderi. Configurația existentă trebuie să fie încărcată fără schimbare; adăugați tipurile de acțiune în loc de a reordona valorile lor persistente.

Alternativele luate în considerare: o implementare monolitică ar încorporează executarea hardware-ului, a rețelei și a fluxului de lucru; înlocuirea WPF ar adăuga lucrări de migrație necorespunzătoare. Se recomandă extinderea modulară.

## 1. Cuvânt cheie macro pad
Stocarea legăturilor prin identitatea dispozitivului plus codul de scan fizic și steagurile de cheie extinse. Adăugați moduri explicite de observare și de descoperire a dispozitivului, captură cheie, suprimare repetată, gesturi, straturi de țintă și reîncărcare a diagnosticelor. Conservați distincțiile Enter principale/numpad și testați layout-urile german/american. Legăturile sunt ținte de o tastatură fizică selectată; identitățile ambiguă de reconectare necesită re-selecție în loc de controlul unui alt dispozitiv.

Raw Input oferă doar observații. Îndepărtarea exclusivă pe dispozitiv necesită un backend de filtrare a tastaturii; niciodată corelate cu un nivel scăzut global cu Raw Input și pretind îndepărtarea fiabilă a dispozitivului. Oferă un monitor, eliberarea de urgență, comportamentul de dezactivare și instalatorul / dezinstalatorul. Nu capturați intrările de desktop sigure. Instalarea driverului și activarea modului exclusiv sunt acțiuni separate ale utilizatorului.

Poarta de conducere: selectați un backend susținut, licențiat și semnat compatibil cu configurațiile țintă Windows 11 sau dezvoltați / semnați un filtru dedicat. Intercepția este un candidat pentru a evalua, nu o dependență de producție: README-ul său oficial descrie testarea prin Windows 10 și licența comercială distinctă. Până când această porțiune este întâlnită, modul exclusiv trebuie arătat ca fiind inaccesibil; caracteristica nu poate fi declarată completă.

Acceptare: cu două tastatură fizică, aceeași cheie execută un macro și nu produce intrări de aplicație normale numai pe dispozitivul selectat; alte intrări de tastatură rămân neafectate. Verificați dezconectarea, accidentul, anularea, suspendarea/resumarea și îndepărtarea driverului pe hardware-ul real.

## 2. Instalarea spațiului de lucru ghidat
Adăugați un mag pentru locul de muncă, întâlnirea, streamingul, cercetarea și schimbarea proiectului. Descoperiți aplicațiile instalate, dispozitivele audio, monitorele și scenele OBS. Utilizatorii aleg obiective și legături; prezintă pașii rezultate și validă cerințele lipsite. Salvați numai după revizuire, fără acțiuni care rulează în timpul setării. Profile generate rămân editabile în editorii existenti.

Acceptare: Creați un profil de lucru fără a edita manual căile executabile sau ID-urile dispozitivului; paginile de anulare lasă setările inalterate; software-ul lipsit are o explicație executabilă.

## 3. Execuție verificată
Introduceți controalele prealabile, rezultatele tipate pe pas și condițiile opționale. Distingerea statelor solicitate, completate, verificate, eșuate, expirate, anulate și nediferențiate. Verificați audio/microfon, luminozitate, pregătire a procesului/venierului și starea OBS cu lecturi asimetrice limitate. Livrarea scurtă/text nu este o dovadă că aplicația țintă a acceptat comanda. Efectele secundare externe nu sunt reversibile; oferă compensație numai atunci când sunt implementate.

Acceptarea: obiectivele explicite dezconectate nu se întoarce niciodată în tăcere; starea observată greșit nu raportează niciodată succesul verificat; anularea oprește sondajul; eșecurile identifică pasul și acțiunea de recuperare. Istoria executării omite plățile text și secretele.

## 4. Plug-in și pachete partajabile
Versiunile JSON declară acțiuni, scheme de parametri, furnizori de stat, dependențe și capacități. Executați plug-in-urile într-un proces separat peste IPC-uri locale limitate, cu șocuri de mână, termene și anulări. Refuzați mesajele defavorizate și versiunile incompatibile; gestionați accidentele fără a termina controlorul. Declarațiile de capacitate sunt dezvăluiri și mediere, nu o cerere de sandboxing OS.

Furnizează previziuni pentru export/import de pachete, hash-uri de conținut, protecție de călătorie, limite de dimensiune, verificări de versiune/dependență, autorizare explicită și actualizare. Plug-in-ul executat nu trebuie să fie executat automat. Includeți un plugin de lucru cu un flux de instalare / acțiune / stat / dezinstalare. Nu este necesar niciun serviciu de piață publică pentru această eliberare.

Acceptare: creați, exportați, instalați și executați o integrare de exemplu reală; demonstrați timout, izolația accidentelor, eșecul dependenței, respingerea traversală și importul disabil.

## 5 Windows Automatizarea UI
Adăugați un picer de element și acțiuni limitate pentru invocare, valoare, selecție, focalizare și așteptare pentru un element / stat. Persistă selectoarele folosind procesul, ID-ul de automatizare, tipul de control și contextul ancestral; evitați înregistrarea de coordonare. Afișează selectorul de joc și selectorul de testare doar pentru a citi. Câmpurile de parolă sunt excluse din înregistrare. Gândiți-vă la controale neîncărcate și la restricțiile de nivel de integritate Windows.

Extindeți înregistrarea cu acțiuni UIA revizibile, menținând înregistrarea curentă. Selectori ambiguă eșuează în loc de a face clic pe un control arbitrar. Un copac normal de accesibilitate nu este o promisiune a unui motor de automatizare DOM complet.

Acceptare: automatizați o aplicație de testare dedicată cu selectoare stabile în funcție de schimbările de poziție / dimensiune; ambiguitate de testare, elemente lipsite, controluri disabilizate, timing-uri și anulare.

## 6. Telefoane / tablete
Serviciul este un panou de răspuns dintr-un serviciu LAN opt-in. Paie explicit folosind un cod de o singură dată de viață și eliberează credențiale revocabile protejate local. Utilizați HTTPS cu certificat local documentat de încredere/pinning; nu trimiteți credențiale în textul de plângere. Default pentru a dezactiva și restricționează interfețele configurate. Validarea originii, comandele autentificate, limitele ratei și mesajele limitate sunt obligatorii.

Expunerea profilurilor/acțiunilor permise, a stării de viață, a progresului și a anulării; utilizarea ID-urilor stabile și a aceleiași chei de executare ca și cheile fizice. Nu expuneți comenzi arbitrare, căi executabile, secrete sau instalarea plugin-ului. Oferă conexiuni, cupluri și ecrane de revocare. Nu este inclus nici un cont cloud sau internet.

Acceptare: funcționează de la un telefon real/tablet pe aceeași LAN; clienții neîncărcați nu pot înregistra sau executa acțiuni; tokenurile revocate nu funcționează; comandele duplicate/replayate nu funcționează dublu; dezconectarea nu retrage acțiunile în tăcere.

## Capacitatea producătorului

| Adaptare / Familia | Integrarea planificată | Limita explicită |
| --- | --- | --- |
| Corsair | Conservă în curent iCUE intrarea cheie G și integrarea RGB | Detectarea capacității de model/software rămâne necesară |
| Logitech G | G-key SDK adaptor cu presă/release și timp de funcționare verifică disponibilitatea; iluminat prin SDK-ul său separat unde este susținut | Nu echivalați detectarea G HUB cu API-ul G-key susținut pe fiecare model |
| Razer | Adaptorul de iluminat de cromă și ghidul Synapse-to-key-mapping, plus intrarea generică HID | Chroma este o API de iluminat; nu o eticheta intrarea macro-cheie |
| SteelSeries | GameSense Adaptor de stare / iluminat plus intrarea HID generica | GameSense înregistrarea evenimentului nu este o API de captură de cheie generală |
| Wooting | Adaptorul analog SDK pentru intrarea analogă susținută, plus cheile HID normale | Capacitățile și termenii de distribuție SDK trebuie verificate pe model susținut |
| Keychron/QMK/VIA | Chei standard HID și instrucțiuni de hărțuire a firmware-ului; desene cunoscute numai cu dovezi | VIA Suportul pentru configurare nu este o API de macro-cheie universală |
| Alte tastatură standard HID | Descoperirea fizică a cheii pe dispozitiv și hărțuirea; mod exclusiv cu un filtru validat | Nu există un model specific Fn, un suport de cheie sau un suport de cheie propriu |

Fiecare dispozitiv afișează detectat, observat, hărțit prin software-ul furnizorului, SDK-ul nativ, statutul exclus de conducător, inaccesibil sau hardware-ul netestat, după cum este cazul. Conservați dovezile de capacitate cu versiunile de adaptor și identificatorii de model/firmware testate.

## Ordinul de livrare
1. Compatibilitate de configurare, contracte de capacitate și executare verificată.
2. Instalare ghidată folosind descoperirea reală și executorii de acțiune existente.
3. UI Automation și fixarea recordurilor/testurilor sale.
4. Plugin-ul, fluxul de pachete și integrarea de exemplu.
5. Panourile LAN și verificarea mobilă.
6. Adaptoare extinse de furnizor și integrare exclusivă a backend-ului tastaturelor, cu validarea hardware-ului real.

Toate cele șase sunt necesare. Semnarea/licența șoferului, SDK-urile inaccesibile sau hardware-ul de testare absent sunt ușile de eliberare, nu motive pentru a înlocui comportamentul de mock și a chema lucrarea completă.

## Criteriile de verificare și eliberare
Executați core-ul existent, Windows și WPF verificați și adăugați teste de integrare pentru fiecare nou limită. Verificați călătoriile JSON și profilurile de moștenire; testați conținutul executat importat, autentificarea rețelei, anularea, accidentele plugin-ului și reîncărcarea dispozitivului. Verificați desktop-ul și layout-ul mobil. Validare înaltă Windows scalare separat. Construiți articole de eliberare numai după trecerea controalelor; distingeți funcționalitatea implementată/testată, implementată/hardware-testată și blocată extern în notele de eliberare. Nu înlocuiți aplicația instalată, instalați driver-uri, schimbați hârtia de tastatură reală sau accesul la rețea deschis ca un test incidental.

## dovezi
- Microsoft Raw Input semantică: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech SDK-uri de dezvoltare G: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Cuvânt cheie pentru Chroma SDK: https://developer.razer.com/works-with-chroma/chroma-faq/
- Rezervele de intercepție și condițiile de dependență: https://github.com/oblitum/Interception
- Inventarul producătorului de depozite existent: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Revizuirea deciziilor
Utilizatorul este rugat să aprobe acest proiect scris înainte de implementarea planului. Principala dependență externă este un backend de producție-pregătit exclusiv de intrare. Amploarea recomandată menține acest lucru ca o ușă livrată, în loc să o reducă în tăcere la Raw Input observații.

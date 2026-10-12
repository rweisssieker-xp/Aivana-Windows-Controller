<!-- generated-doc locale=ro-RO source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO extinderi de control

Utilizatorul a solicitat toate cele nouă îmbunătățiri propuse și o icoană recunoscută. Configurațiile existente, în special G1/G2 luminozitate, trebuie să rămână compatibile. Edițiile de branding și documentare neînființate aparțin spațiului de lucru comun și trebuie păstrate.

## Livrările și limitele

1. Acțiunile de luminozitate selectează toate monitorele susținute sau un ID stabil al monitorului specific. Obiectivele explicite nu sunt vizibile. Dashboard-ul afișează luminozitatea curentă, volumul de ieșire, starea microfonului și OBS prin citirea asincronă limitată.
2. Cheile speciale susțin presiunea imediată, lipirea, lipirea și lipirea dublu, cu suprimarea repetată. Un selector de strat ținut schimbă temporar maparea activă și o restabilește. Legăturile imediate își păstrează comportamentul. Pauza, modificările de profil, dezconectarea și anularea gesturilor clare.
3. Designerul fluxului de lucru editează condițiile, ramurile și politica de stop/continue failure în mod tranzacțional într-un proiect de tip. Condiții acoperă fișierele, procesele de rulare și titlurile ferestrei. Nu există cicluri de saltare nelimitată.
4. Înregistrarea explicită captează codurile și așteptările scurte, ignorează intrarea injectată și oprește Escape. Acțiunile înregistrate sunt revizuite înainte de adoptare; textul obișnuit nu este înregistrat.
5. Per-device chei speciale de la alți producători folosesc Windows Raw Input. Cheile de proprietate necesită o API de furnizor susținută sau o mapă de furnizor la o cheie standardă; funcționalitatea nesuportată este demonstrată cu sinceritate. Diagnosticul explică profilul, pauza, conflictele obligatorii și starea actuală a adaptorului.
6. Schimbul de profil utilizează o folderă partajată selectată de utilizator (inclusiv folderele sincronizate de un client cloud existent). Verificările de revizuire prevăd supraîncărcări silențiale. Pull prezintă modificările și importul acțiunilor dezactivate cu hârtia hardware îndepărtată. Conținutul de acțiune este explicit. Nu sunt stocate credențiale cloud de MAKRIVO.
7. Strângerile de UI aparțin catalogurilor existente 27-limbi; DE/EN/FR/IT rămâne limbile minime de documentare revizuite. Intervențiile trebuie să fie dezvăluite, mai degrabă decât complet.
8. O marcă de tastatură recunoscută înlocuiește icoana generică Windows. Modul de pauză are un marcaj de pauză amber. Executabil / branding utilizează aceeași marcă. Instrumentul include stat și profil.

## Verificare

Verificările de bază acoperă compatibilitatea înapoi, snapshots, anularea, timing-ul gesturilor, ramurile/erorile de flux de lucru, filtrarea înregistrărilor și conflictele de sincronizare. Windows Verificările acoperă numărul de afișare doar de lectură, evenimentele de lansare SDK și Raw Input parsing. WPF Verificările de fum acoperă noi puncte de intrare și resurse fără a schimba luminozitatea monitorului real, tipărirea într-o altă aplicație sau supraîncărcarea profilurilor utilizatorilor. Instalarea păstrează o backup și o configurare G1/G2.

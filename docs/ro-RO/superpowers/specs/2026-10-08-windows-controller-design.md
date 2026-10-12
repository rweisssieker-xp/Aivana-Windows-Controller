<!-- generated-doc locale=ro-RO source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – proiectul

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Starea: 8. octombrie 2026 Starea: pentru examinare înainte de implementare.

## Obiectivul și scara convenită

O aplicație independentă Windows face ca un tastatură PC obișnuită să fie un panou de acțiune liber configurabil, asemănător cu un acoperiș de flux. Utilizatorul a confirmat toate tipurile de acțiune propuse, macro, recunoașterea dispozitivelor, profiluri și un GUI bun. Tastaturile Corsair conectate și alte tipărire sunt identificate; funcțiile suplimentare ale producătorului sunt oferite numai atunci când sunt în mod efectiv susținute.

Presupuneri: suprafață germană, utilizare locală fără cont, Windows 11 x64 ca primul obiectiv, fără instalare a driverului. Tastatura rămâne utilizabilă pentru scrierea normală. Combinările de tastatură cu modificatori sunt utilizate în mod standard. În mod standard, tastele de litere individuale nu sunt rezervate la nivel global.

## Decizia tehnică

Native C#/NET-10-App cu WPF și MVVM. Stabilul .NET-10-SDK este disponibil în spațiul de lucru. WPF permite o interfață de desktop personalizată și integrarea directă Windows. Alternativele ar fi o interfață web cu un serviciu separat Windows sau o aplicație de desktop interplatform; ambele vor aduce mai mult timp și complexitate de comunicare pentru acest proiect Windows. Varianta nativă este, prin urmare, baza aleasă.

Distribuție: proiect de desktop pentru viziune și trage, proiect de bază pentru configurare și logică de execuție, proiect de Windows pentru hotkeys, simulare de intrare și dispozitive, proiect de testare pentru reguli independente de platformă. Adaptorii de producător sunt legați de o interfață de capacitate solidă și sunt independenți de acțiunile standard.

## Suprafața

Design întunecat cu suprafețe liniștite de culoare antrazită, scrisoare bine citită și accent turc. Link-uri de navigație pentru dashboard, profiluri, dispozitive și setări. În plus, profilul activ, căutarea și interfața pentru a pauza toate hotkeys. Suprafața rămâne utilizabilă la 100–200 procente Windows scale.

Dashboard-ul conține un raster adaptabil. Fiecare capcană afișează simbolul, numele, hotkey-ul și statutul de executare. Click face aceleași acțiuni ca Hotkey. În dreapta se deschide un editor cu tipul de acțiune, parametrii, înregistrarea hotkey, simbolul / culoarea, activarea și butonul de testare. Nou, duplicare, ștergere și reorganizare sunt disponibile. O vizualizare a tastaturii afișează în mod schematic setarea standardă germană; ea nu susține geometria exactă a dispozitivului detectat.

Profile pot fi create, copiate, modificate și activate manual. Opțional, un profil este atribuit unui nume de proces. Un program de bază adecvat activă profilul său, altfel se aplică profilul standard global. Pentru mai multe profiluri adecvate se aplică o ordine clar vizibilă. Editorul explică conflictele hotkey direct în locație.

## Hotkeys și intrări

Windows RegisterHotKey prelucrează combinații globale normale cu reîntoarcerea tastelor suprimate. Numai profilul activ este înregistrat. Combinațiile rezervate sau deja înregistrate apar ca erori; aplicația nu le raportează ca fiind activate cu succes. Înregistrarea în editor nu rezolvă acțiuni.

Înainte de intrările simulate, se eliberează tastele de modificare care declanșează sau se așteaptă eliberarea lor fizică. Textul unicode și combinațiile de tastatură sunt trimise cu SendInput. Fiecare buton imprimat în sine va fi eliberat chiar și atunci când se rupe. Acțiunile se desfășoară în afara lanțului UI; executarea și schimbarea profilului nu trebuie să blocheze suprafața.

## Acțiuni și macro

Tipuri de acțiune: Începeți programul cu argumente și director de lucru, deschideți fișierul / folderul peste Windows, deschideți site-ul HTTP / HTTPS, introduceți text Unicode, trimiteți o combinație de tastatură, jucați / urmăriți / predecesor / opriți, schimbați profilul cu voce mai mare / mai mică / mai mare. Parametrii sunt validate înainte de stocare și din nou înainte de executare. Programele sunt lansate direct; argumentele nu sunt interpretate implicit ca comenzi Shell.

Macro-urile conțin o listă ordonată a acțiunilor menționate și pauze explicite. Pasurile pot fi adăugate, modificate și amânate. Repetările sunt în cele din urmă configurabile (1–100); înregistrările fără sfârșit nu aparțin primei versiuni. Un macro în curs poate fi finalizat pe suprafață sau un propriu hotkey de rupere. Repetarea aceluiași localizare în curs nu începe un curs paralel. Diferitele macrouri cu simulare de intrare sunt serializate, astfel încât tastele lor să nu se amestece. În cazul unei erori, macro-ul se oprește și arată pasul afectat.

## Echipamente și producători

Inventarul dispozitivelor prin Windows Raw Input și informații suplimentare despre dispozitiv: nume de afișare, calea dispozitivului, identificarea producătorului / produsului, în măsura în care este disponibilă, starea de conectare și abilitățile recunoscute. In/de la actualizare lista fara redeschiderea. Un dispozitiv fizic poate avea mai multe intrări HID; acestea sunt grupate după identificări încărcate, altfel vizibile ca intrări separate.

Hotkeys standard se aplică interturbent. vizualizarea dispozitivului oferă informații și abilități; un tastatură separată cu tastatură exclusivă nu face parte din modul convenit.

Recunoașterea Corsair funcționează chiar și fără iCUE. Un adaptor SDK opțional iCUE oferă starea de conectare și acțiunile de iluminare susținute atunci când există SDK, iCUE și un dispozitiv compatibil. UI-ul diferă recunoscut, SDK-ul este disponibil, conectat și efectiv controlabil. SDK este documentat în special cu controlul LED; butonele G proprietare sunt susținute numai ca și hotkeys obișnuite atunci când Windows primesc intrări corespunzătoare. Nu există nici o promisiune pașnică pentru toate presiunile.

Alți producători primesc inițial detectarea generică și acțiuni standard. interfața adaptorului permite integrări ulterioare; abilitățile neînsoțite sunt afișate cu sinceritate. testele de hardware pentru modele neexistente nu sunt indicate ca fiind existente.

## Stocare și viață de zi cu zi

Configurația JSON versiune în lista de date a aplicațiilor locale. Înregistrare nucleară cu ultima securitate valabilă; fișierele eronate nu sunt supraîncărcate. Importul verifică formatul, versiunea, ID-urile și parametrii și afișează o perspectivă înainte de achiziție. Acțiunile importate nu vor fi executate decât după activare expresă. Exportul conține profiluri și dovezi.

Meniu Tray: deschide, selectați profilul, pauzați hotkeys, rupeți macros, încheiați. Fereastra se închide în mod minim în tracțiune și explică acest lucru pentru prima dată. Autostartul este opțional și standard. Numai o instituție gestionează Hotkeys. Încheierea înregistrează hotkeys, rupe executările și eliberează resursele.

Vizualizare locală a activității cu o perioadă limitată de succes, erori și dezavantaje. Nu există un protocol de toate tastaturele. Conținutul de acțiune textă nu este scris în mesaje de eroare. Nu există un serviciu cloud și nici o telemetrie.

## Tratamentul erorilor și limitele

Programele lipsite, URL-urile nevalide, hotkeys, profilurile deteriorate și SDK-ul producătorului inaccesibil primesc mesaje concrete din Germania. O eroare de adaptare nu afectează acțiunile standard. Intrările în aplicații mai privilegiate pot eșua la limitele de autorizare Windows; erorile sunt explicate, aplicația nu necesită drepturi de administrator standard.

## Examinarea și reducerea

Testele automatizate pentru prioritate de profil, detectarea conflictelor, importul de configurare, stocarea atomică, ordinea de macro, repetarea, ruperea și propagarea erorilor. Windows teste de smoke pentru hotkeys reale, text și combinații de tastatură, acțiuni media, inventari de dispozitive, tracțiune și finalizare curată. Nu există teste care să lanseze accidental orice programe sau site-uri web.

Testarea GUI: instalarea și editarea, înregistrarea fără executare, click/hotkey cu același efect, raportarea de conflict, schimbarea profilului, vizualizarea dispozitivelor, importul/exportul și scalarea. Verificarea hardware-ului specific pentru producător este documentată separat cu dispozitivul actual.

Livrare: cod de sursă, ghid de construire/start reprodus, ghid de utilizare germană și local Windows-x64-Publish-Artefakt. Succesul înseamnă o aplicație executată cu toate caracteristicile generale; abilitățile Corsair sunt marcate ca fiind disponibile numai cu suport SDK / dispozitiv dovedit.

## Sursele tehnice

- Microsoft Inchiriere de tastatură / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Introducere: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Oficial Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Următorul pas

După examinarea acestui proiect: să se elaboreze un plan concret de implementare și să se prezinte pentru a alege implementarea. în ordinea de lucru goală nu există încă un repository Git; acest document nu a fost, prin urmare, încă comitted.

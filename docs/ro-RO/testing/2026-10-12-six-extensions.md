<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-12-six-extensions.md)

# Șase extinderi: implementare și testare

Starea: 12. octombrie 2026, industrie `codex/six-extensions`. starea de muncă, nu se eliberează producția.

## Serviciul

Paleta de comandă deschide intrările **Setarea locului de muncă**, **Fabricantele de tastatură și SDK**, **Panelul mobil**, **Extension**, **UI-Automatizare**, **UI-Execut** și **Executare**.

- Asistentul de la locul de muncă creează profiluri pentru locul de muncă, întâlniri, streaming, cercetare și proiect. Programele din meniul de înregistrare/start sau alegere a fișierului, URL-urile, emisiunea/scena și scena OBS sunt afișate ca pași înainte de stocare. Instituţia nu are nicio acţiune.
- În editorul pasului **Rezultatul verifică ** pre-examinarea și re-controlul. Cursul și panourile mobile diferă confirmarea efectivă de efectul necontrolabil. Modificările relative, scurte de tastatură și introducerea textului nu oferă confirmare pauzală a efectului. O dovadă a procesului nu demonstrează disponibilitatea profesională a aplicării.
- Automatizarea UI utilizează un lucrător separat, limitat în timp. Selecția prin poziție de mouse, selecție, Invoke/Select/Toggle/Focus/SetValue/WaitState; mai multe goluri, obiective dezactivate și câmpuri de parolă sunt respinse. Înregistrarea cuprinde clicuri și pauze suportate; înregistrările textului nu sunt tăiate. Înainte de achiziție, se verifică recepția.
- Extinderi: testarea ZIP, hash-uri, schemă, import dezactivat, încredere expresă înainte de activare, proces izolat cu IPC limitat, export, dezactivare, deinstalare, restabilire/refuza versiunea anterioară. Procesul de izolare nu este o cutie de nisip: plugin-urile dezvăluite funcționează cu drepturi de utilizator. Pachetul de exemplu `makrivo.example.zip` conține conturi și acțiuni de status. Schemele susținute formează un cadru simplu documentat al schemei JSON în codul de sursă; nu piața publică.
- Panourile mobile: selectați IP-urile locale și profilurile împărtășite, deschideți adresa HTTPS, verificați certificatul pe baza SHA-256-Fingerprints afișat și încrederea în mod conștient, apoi introduceți un cod unic de șase cifre. Cheile private sunt protejate cu WindowsDPAPI; numai hash-urile accesului client sunt stocate. Certificatele se schimbă după reînnoire sau schimbarea IP. Dezvoltarea firewall-ului Windows este efectuată de către utilizator, dacă este necesar. Nu există instalare automată a certificatelor, direcție de port sau schimbare a firewall-ului. Accesul clientului poate fi retras. Panelul afișează starea de acțiune și cursul de testare; nu este încă un desktop live.

## Tastatură

| Producătorul | Implementarea | Restricția |
|---|---|---|
| Corsair | butonele G iCUE și RGB existente | Conectarea locală iCUE și modelul potrivit necesar |
| Logitech în cazul în care | SDK-ul G-Key și SDK-ul LED separat | original local x64-DLL dezvăluie în mod explicit; G-Key SDK nu oferă identitatea individuală a mai multor tastatură |
| Razer | Recunoașterea cromelor RGB și a tastelor standard | Taste proprii înainte de Synapse Întâlnirea |
| SteelSeries | GameSenseRGB și recunoașterea tastelor standard | Tabelele proprietare sunt atribuite anterior în GG |
| Wooting | Valori analogice legate de dispozitiv, valuri de presiune/de ruptura | original local x64-SDK-DLL; intrarea normală a HID rămâne activă |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Mai multe | Taste fizice standard după dispozitiv și cod de scanare | Fn și elemente de control proprietare în funcție de model |

Nu sunt livrate noi DLL-uri SDK proprietare. noile adaptoare ale producătorului nu sunt încă validate prin hardware fără dispozitive fizice adecvate.

**Exclusiv al doilea macropad nu este finalizat: ** Raw Input observă dispozitivele separat, dar nu suprimă introducerea lor normală Windows. O eliberare de producție necesită un filtru licențiat, semnat, verificat sub Windows 11/Secure și teste de eliberare de urgență, dezintegrare, suspendare și reîncărcare. Nu a fost instalat niciun driver. Aplicația specifică în mod explicit această limită.

## Examenele

- Release-Build: reușită, zero avertisment / erori.
- Core-console: toate grupurile de testare au reușit, inclusiv conflictele cu tastele de producție, plugin-rollback și erorile de executare reale în cursul testării.
- Integrarea extinderii: un operator real UIA cu aplicația de testare WPF; invoke unică, multidimensional, obiective dezactivate, excluderea parolelor. procesul de plug-in de tipărit, limitele de protocol, histereza analogă și decodarea ABI Logitech cu succes.
- Host HTTPS: certificat de tipărit, acces necoplicat, cod fals/unic, protecție de origine, fișiere de browser, stocare hash și cheie protejată cu succes.
- Windows-Suite cu test de succes exprimat: cu succes; Audio/OBS/iCUE/Raw Input În cursul precedent, întreaga suită a eșuat în schimbarea de focalizare refuzată la fereastra de testare izolată. În cursul ulterior, prototipul de inteligență artificială a inclus și întreaga suită, inclusiv schimbarea de focalizare și iCUE-Un alt curs de repetare a eșuat din nou în schimbarea focalizării; testul de context nu este verde stabil. Acest test nu a fost slăbit. în scurt timp, recunoașterea DDC e limitată; nu a fost modificată lumina.
- WPF Smoke: toate dialogurile anterioare și noi reduse, varianta de configurare, editor și CRUD cu succes; ecranuri în ordine de construcție `smoke`.
- Browser: Playwright cu Edge, 390×844 și 1024×768, flux de conectare/UUID/unic transmitere/nu flux orizontal de succes. API-Mock; testare HTTPS reală separat. Imagini de la `artifacts/mobile-browser` Browser-plugin nu a fost disponibil, prin urmare, regulat Playwright Fallback.
- Revizuirea independentă a codului; comenzi mobile întârziate, rezultate de verificare terminale lipsă, testarea procesului, reîntoarcerea plugin-ului și recurentul RGB-Heartbeat corectate.

Încă deschisă: integrarea exclusivă a driverului, testele fizice ale producătorului/telefonului/tabletului, scalarea Windows 150/200 procente și verificarea completă a tuturor noilor texte de dialog. Noile texte de dialog sunt în mare parte engleze; catalogurile lingvistice existente rămân păstrate. De aceea, cererea totală ** nu a fost complet redusă **.

## Produsă pachet

`./tools/publish-complete.ps1 -SelfContained` creează un nou folder sub `artifacts/release-*`, inclusiv desktop, muncitor UIA, HTTPS host, fișiere de browser, plugin-ul de exemplu importat și instrucțiuni de licență de rulare. Aplicația instalată nu este supraîncrisă. Fără `-SelfContained`, pachetul necesită .NET-10-Desktop-/ASP.NET-Runtimes.

Pachetul este un test local, nu un instalator de vânzare semnat. termenii proprii de utilizare și drepturile de vânzare SDK sunt eliberări separate; vezi documentația existentă la `docs/licensing`.

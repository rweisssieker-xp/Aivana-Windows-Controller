<!-- generated-doc locale=ro-RO source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../FEATURES.md)

# MAKRIVO – Revizuirea caracteristicilor

Producător: **Aivana**. starea: **12. octombrie 2026**. Aceasta este starea de dezvoltare, nu este de vânzare sau de eliberare a hardware-ului.

## Windows - controlul

- Cacheluri, hotkeys globale, palete de comenzi, preferate și profiluri dependente de aplicație.
- Macro cu mai multe pași, repetări, pauze, ruperi și previziuni.
- Deschideți programe, fișiere și site-uri web; utilizați text, tabă și scurte tastatură.
- Armonizează ferestrele, închide Windows, controlează ieșirile audio și microfonele, precum și schimbă lumina ecranului prin intermediul interfețelor susținute.
- OBS-WebSocket-v5-Anbindung pentru scenele, înregistrarea, streamingul și audio.
- Şablonuri, asistent de lucru, recunoaştere a dispozitivelor şi suprapunere tastatură.
- Import și export de configurații de profil; testarea proiectului și detectarea conflictelor.
- Setări stocate local; nu este necesar un cont pentru utilizarea zilnică.

## Funcții avansate în codul de sursă actual

| Zona | Amploarea funcției | Frontieră |
| --- | --- | --- |
| Testarea rezultatelor | Verificare prealabilă, monitorizare și testare | Doar efectele verificabile confirmă; o presiune de tip de succes nu dovedește efectul țintă. |
| Asistentul de la locul de muncă | Pregătiți profiluri și proceduri din programe selectate, fișiere, URL-uri și alte acțiuni | Previziune și stocare necesară; instalația nu produce nimic. |
| Înregistrarea macro și fluxurile de lucru | Înregistrare, condiții, gesturi și sincronizare a profilului local | Conducerea și limitele sunt documentate în rapoartele de punere în aplicare și de testare. |
| UI automatizare | Selectați obiectivele, verificați selectorii, acțiunile și fluxurile de clicuri limitate | Lucrătorii separati; obiectivele multiple, dezactivate si parola sunt respinse; nu sunt incluse in textul general. |
| Extinderi | Verificarea, importarea, activarea, exportarea și returnarea pachetelor, după încredere expresă | Procesul de izolare nu este o cutie de nisip; nu este un loc public de piață plug-in. |
| Panouri mobile | Panourile locale HTTPS, conexiunile, profilurile selectate și istoricul de status/test | Realitate de telefon / tabletă LAN de îndepărtare încă deschis; nu un desktop de senzori universale. |
| Adaptator producător | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB și Wooting-Analogadapter | Modele/SDK dependente; noile adaptoare nu sunt complet validate cu hardware real. Nu există noi DLL SDK proprietare. |
| Tastatură fizică | Taste standard pentru a detecta prin dispozitiv și cod de scanare | Raw Input nu suprimă introducerea normală Windows. |
| Asistentul de discuții local | Prototip opțional pentru Ollama, LM Studio, llama.cpp și ONNX Runtime GenAI CPU produce proiecte limitate din resurse selectate în mod explicit | Standard; timpul de funcționare nativ CPU testat cu modelul accidental, dar nu a dovedit calitatea de planificare sau economisirea timpului; nu conține greutatea modelului. |

## Nu oferă încă ca vânzări gata

- **Exclusiv al doilea desktop/macropad: ** Un driver de filtrare semnat, licențiat și testat pe hardware este lipsit. Intrările de tastatură normale pot continua să ajungă la Windows.
- Absorbția completă a hardware-ului tuturor adaptorilor de producător, precum și testele reale ale dispozitivelor mobile.
- Adevărata schimbare a monitorului/DPI și recunoașterea profesională a tuturor traducerilor noi. PerMonitorV2 și simulate 150/200-Prozent-Layouts au fost verificate; testele reale de afișare rămân separate. Interfața oferă 27 limbi; textele noi detaliate nu au fost încă complet eliminate în limbă.
- Instalator de vânzare semnat, EULA finală, drepturi de vânzare verificate SDK și integrare productivă a licenței de cumpărare/checkout.

## Detalii și dovezi

- [Documentația de utilizare](README.md)
- [Extinderi: funcții, teste și limite deschise](testing/2026-10-12-six-extensions.md)
- [Asistent local de inteligență: prototip și testare](testing/2026-10-12-local-meeting-assistant.md)
- [Timpurile de funcționare și cerințele de modelare locale](testing/2026-10-12-local-ai-runtimes.md)
- [Testul DPI și Scalarea](testing/2026-10-12-dpi-scaling.md)
- [Verificarea licenței](licensing/2026-10-12-commercial-license-audit.md) – înregistrare istorică; noul script Publish completează instrucțiunile de funcționare. Pachetul final trebuie examinat separat.
- [Platforme de vânzare](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – starea de comparație; Stripe este, în plus, o platformă de plată posibilă, a se vedea condițiile actuale ale furnizorului.

## Creați un pachet de dezvoltare complet

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Pachetul include aplicații desktop, operatori de automatizare a UI, host-uri mobile, fișiere de browser, plugin-uri de exemplu și informații privind licența de rulare. fapte de construcție nu sunt verificate în Git. Un cod de sursă GitHub nu este încă un instalator Windows publicat.

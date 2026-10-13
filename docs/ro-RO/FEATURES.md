<!-- generated-doc locale=ro-RO source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../FEATURES.md)

# MAKRIVO – Revizuirea caracteristicilor

Producător: **Aivana**. starea: **12. octombrie 2026**. Aceasta este starea de dezvoltare, nu este de vânzare sau de eliberare a hardware-ului.

## Windows - controlul

- Cacheluri, hotkeys globale, palete de comenzi, preferate și profiluri dependente de aplicație.
- Macro cu mai multe pași, repetări, pauze, ruperi și previziuni.
- Deschideți programe, fișiere și site-uri web; utilizați text, tabă și scurte tastatură.
- Aranjarea ferestrelor, blocarea Windows, controlul ieșirilor audio și microfoanelor și modificarea luminozității ecranului prin interfețele acceptate.
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
| UI automatizare | Selectați obiectivele, verificați selectorii, acțiunile și fluxurile de clicuri limitate | Proces de lucru separat; țintele ambigue, dezactivate și câmpurile de parolă sunt respinse; nu se înregistrează introducerea generală de text. |
| Extinderi | Verificarea, importarea, activarea, exportarea și returnarea pachetelor, după încredere expresă | Procesul de izolare nu este o cutie de nisip; nu este un loc public de piață plug-in. |
| Panouri mobile | Panourile locale HTTPS, conexiunile, profilurile selectate și istoricul de status/test | Testele de acceptanță LAN cu telefoane/tablete reale sunt încă nefinalizate; nu este un tablou universal de senzori. |
| Adaptator producător | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB și Wooting-Analogadapter | Modele/SDK dependente; noile adaptoare nu sunt complet validate cu hardware real. Nu există noi DLL SDK proprietare. |
| Tastatură fizică | Taste standard pentru a detecta prin dispozitiv și cod de scanare | Raw Input nu suprimă introducerea normală Windows. |
| Asistentul de discuții local | Prototip opțional pentru Ollama, LM Studio, llama.cpp și ONNX Runtime GenAI CPU produce proiecte limitate din resurse selectate în mod explicit | Dezactivat implicit; mediul nativ CPU a fost testat cu un model aleator, dar calitatea planificării sau economiile de timp nu sunt demonstrate; nu sunt incluse ponderi de modele. |

## Nu oferă încă ca vânzări gata

- **A doua tastatură/macropad exclusivă:** Lipsește un driver de filtrare semnat, licențiat și testat pe hardware. Intrările normale de taste pot ajunge în continuare la Windows.
- Acceptanță hardware completă pentru toate adaptoarele producătorilor și teste LAN reale cu dispozitive mobile.
- Adevărata schimbare a monitorului/DPI și recunoașterea profesională a tuturor traducerilor noi. PerMonitorV2 și simulate 150/200-Prozent-Layouts au fost verificate; testele reale de afișare rămân separate. Interfața oferă 27 limbi; textele noi detaliate nu au fost încă complet eliminate în limbă.
- Instalator comercial semnat, EULA finală, drepturi verificate de distribuție SDK și integrare în producție a licenței de achiziție/checkout.

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

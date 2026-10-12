<!-- generated-doc locale=ro-RO source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Examinare pentru vânzări comerciale unice

Număr de locuri: 12 În luna octombrie 2026 Producătorul este Aivana

## Rezultatul

Nu a fost recunoscută nicio obligație generală de a dezvălui propriul cod de sursă MAKRIVO. O singură vânzare pare posibilă, dar nu este încă complet pregătită. În special, autorizația de transmitere pentru DLL Corsair existentă nu este dovedită. Această examinare nu înlocuiește eliberarea juridică.

## Detalii tehnice și licențiate

| componenta | descoperă | Pasul necesar |
| --- | --- | --- |
| Codul de sursă propriu | Nu se găsește nici o licență, copie sau notificare în conținutul Git examinat și în istoria fișierului de licență disponibil local. | Confirmarea drepturilor de proprietate, inclusiv contribuțiile, fragmentele de cod achiziționate și eventualele drepturi ale angajatorului; crearea propriilor termeni de utilizare. |
| .NET / WindowsDesktop | Pachetul independent studiat în `artifacts/makrivo-win-x64` conține versiunea 10.0.6. Pachetele locale de rulare conțin licențe MIT care permit livrarea comercială și cer furnizarea informațiilor lor. În ordinea de expediere examinată nu au fost găsite fișiere de licență/notificare corespunzătoare. | Textele exacte de licență și informațiile relevante ale terților despre timpul de rulare furnizat efectiv în pachetul de vânzare. |
| NuGet - Întrebări | În fișierele de proiect examinate nu s-au găsit intrări directe de referință a pachetului. fișierul de active de desktop examinat conține propriile obligații de proiect. pachete de timp de desfășurare rămân totuși relevante pentru licență. | La lansarea finală, toate proiectele, pachetele tranzitive și fișierele livrate sunt inventate din nou. |
| Corsair iCUE în cazul în care | `artifacts/win-x64/iCUESDK.x64_2019.dll`, versiune a fișierului 4.0.84.0. PDF-ul de referință locală SDK conține pe paginile de imprimare 35–38 EULA: utilizare gratuită, dar restricții pentru transfer/transfer. | În mod alternativ, verificați un pachet fără DLL SDK, care utilizează un SDK instalat de partea clientului; pentru aceasta se aplică și condițiile SDK. |
| Pacientele diferite | `artifacts/makrivo-win-x64` nu conține DLL Corsair; `artifacts/win-x64` conține. | Determinați instalatorul de vânzări real și verificați conținutul acestuia. Existența pură a unui pachet fără DLL nu confirmă încă toate drepturile de integrare. |
| OBS | Client propriu WebSocket în codul C# studiat; nu este identificat nici un pachet de client OBS, OBS binar sau cod GPL adoptat. | Integrarea observată a protocolului nu justifică o obligație de dezvăluire a GPL stabilită. originea fragmentelor posibile rămâne confirmată. |
| Simbolul de aplicație | Un script local Python descrie simboluri geometrice cu Pillow. instrumentul este o dependență de construcție. | Documentarea originii și a drepturilor tuturor imaginilor, sunetelor, scrierii și a altor active furnizate efectiv. |
| Administrarea licențelor | Nu se identifică o verificare completă a licenței de cumpărare/activare în codul de aplicare examinat. | Dacă este necesar, să se implementeze fișierele de licență offline semnat. Activarea tehnică și condițiile de utilizare legale sunt sarcini separate. |

## Modul de cumpărare – încă nu implementat

- Plătire unică; dreptul de utilizare permanentă la versiunea principală achiziționată.
- O persoană pe până la două PC-uri proprii; utilizarea profesională este explicit permisă.
- Include corectări de eroare și actualizări mai mici; versiuni principale ulterioare oferă opțional cu plata.
- Fișierul de licență verificat offline, semnat digital; cheile de semnătură private nu sunt niciodată livrate cu aplicația.
- Drepturile legale și actualizările de securitate necesare nu pot fi excluse în mod pașnic de regula principală a versiunii.

## Înainte de prima vânzare deschisă

1. Corsair clarifică drepturile de vânzare sau pregătește vânzarea fără SDK-DLL legat.
2. A se adauga si a se primi textele licenței si ale furnizorilor terti ale angajamentelor de lansare exacte.
3. Confirmarea drepturilor de proprietate pe codul propriu și pe active; crearea EULA cu un regulament clar de dispozitive, utilizator și actualizare.
4. Pentru vânzările consumatorilor, prelucrați checkout-ul, inclusiv obligațiile de informare și retragerea. O „rambursare fără descărcare” nu este suficientă. Răscumpărarea dreptului de retragere în cazul furnizării imediate presupune condițiile legale.
5. Instalatorul finalizat, inclusiv plug-in și toate dependențele tranzitive, se verifică din nou. copia de lucru este extinsă în paralel; această înregistrare nu este o distribuție a fișierelor viitoare.

## GitHub

Repositorul studiat este public. Fără o licență de sursă deschisă, codul nu este liber automat pentru orice utilizare ulterioară. Cu toate acestea, drepturile de platformă pentru repositorii publice GitHub trebuie respectate. O schimbare ulterioară în privat nu îndepărtează nici o copie deja existentă sau forc. Vizibilitatea și licența nu au fost modificate în timpul acestui test.

## Dovezi și surse

- Licențe locale .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` și `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Notificare a furnizorului local .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`
- SDK local Corsair: `artifacts/dependencies/iCUESDK_4.0.84.zip`; PDF de referință extractată sub `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 Licență MIT](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair Documentare SDK cu EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licențierea Repositoriilor](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Actualizări](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Dreptul de retragere](https://www.gesetze-im-internet.de/bgb/__356.html)

Nu verificat: istoricul complet al contribuțiilor externe și lanțurile de drepturi, eliberarea mărcii, șecul de vânzare existent, pachetele de plug-in viitoare, eficacitatea juridică completă a unui EULA încă nu este disponibil.

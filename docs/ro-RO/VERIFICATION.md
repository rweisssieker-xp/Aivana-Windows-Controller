<!-- generated-doc locale=ro-RO source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../VERIFICATION.md)

# Examenul – 8. octombrie 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Aplicația a fost construită sub Windows x64 cu SDK stabil 10.0.202 și a fost publicată ca independent Windows-x64-Ausgabe.

## Verificată automat

- Construcție generală: nu există avertizări și nici erori.
- 19 Testele de comportament de bază: configurare standardă, validare, date zero/duplicate, prioritate de profil, import/export și recuperare de siguranță, ordine de macro și repetare, rupere, dublu decontare, serializare, snapshot de executare și erori de pas.
- Windows Test: standardizarea hotkey și aliații, dimensiunile structurii native, lista de tastatură reală, înregistrarea și redistribuirea sigure, WM_HOTKEY sintetică pentru șoc și rupere, înregistrarea șocului dezactivat în timpul simulării de intrare.
- EXE independent a fost lansat cu `--smoke-test`: toate cele cinci vizualizări WPF sunt redate, se pot instala / copia / se pot șterge, proiectele rămân în timp ce refresh, configurația este stocată și încărcată din nou. Acest test utilizează un director de configurare temporară izolat și nu efectuează acțiuni text sau media.
- Dashboard, editor și afișare compactă (900 × 720) au fost verificate vizual ca fiind reale WPF. stocarea de design, respingerea, ordinea de cache/macro, aliaza germană de hotkey, defecțiunile individuale și progresul de pas au fost verificate.

## Limitele hardware

Ultimul curs local a detectat patru interfețe de tastatură Windows. Acest număr descrie interfețele HID/Windows, nu necesar patru tastatură fizică.

SDK-ul oficial iCUE este instalat și se află lângă EXE. Testul live se conectează la iCUE și recunoaște un tastatură Corsair cu informații despre model și LED. Schimbările de iluminat nu au fost efectuate automat; tastele de ecran și alți producători nu au fost testate pe hardware. Hotkeys generice se aplică pe tastatură.

Text/media nu a fost injectat în alte programe de utilizator deschise. structurile de date native și logica erorilor/liberalelor au fost verificate; comportamentul într-un anumit program țintă depinde, printre altele, de nivelul de autorizare al acestuia.

## Repetă

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Testul GUI scrie rezultatul și ecranul după `smoke` lângă EXE verificată. este considerat modul de diagnostic și nu începe o instanta de fundal permanentă după finalizare.


## Controlul extins

Verificate în plus: condiții de profil cu proces și titluri de ferestre, transformare variabilă fără extindere recursivă, vizualizare macro fără execuție, validare nouă de acțiune și compatibilitate de jos a fișierelor JSON vechi. Testele native acoperă paleta de comenzi cu înregistrare, conflicte, WM_HOTKEY dispatch și eliberare, precum și distribuția pură a monitorului și accesul la clipboard-ul controlat de nevoi. Testul WPF deschide și retrage paleta reală, verifică retragerea / retragerea și preferințele, verifică proiectele curate după retragere și încheie cu succes procesul.

Windows blocaj, modificări interioare și mișcări de fereastră nu au fost aplicate programelor de utilizator în timpul testelor. Acțiunile de fereastră postă solicitări Windows asincrone; programul țintă concretă poate avea propriile dimensiuni minime sau limite de autorizare. Profilul și statisticile de execuție se aplică ședinței în curs.


Examinarea de extindere de la 12.10.2026: 30 testele de bază au trecut; Windows adaptor, inclusiv lansare în timpul intrării, recunoașterea monitorului protejat de scris și OBS starea de blocare. WPF verifică recunoașterea / recunoașterea, înregistrarea / oprirea, profilul de schimb, gesturi și resurse simbolice. Localizare: 27 Cataloguri cu fiecare 967 chei; Documentare: 84 pagini în patru limbi. Butonele de proprietate ale altor producători decât Corsair nu au fost testate pe hardware; suportul generic include evenimentele standard specifice dispozitivului F13–F24 și butonele media. Transportul în cloud se efectuează prin intermediul unui folder sincronizat selectat; importurile sunt verificate și rămân dezactivate.

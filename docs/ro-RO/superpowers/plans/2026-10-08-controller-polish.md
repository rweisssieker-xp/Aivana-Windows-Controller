<!-- generated-doc locale=ro-RO source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Planul de utilizare și îmbunătățire a capacității

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Cererea utilizatorului: îmbunătățește aspectul, utilizabilitatea și funcționalitatea; autorizația prealabilă solicită subiecții utile maxime pe modelele mai mici necesare și completarea autonomă.

## Scopul
Conservă profilurile existente și funcționează Corsair integrarea SDK. Îmbunătățiți aplicația WPF existentă în loc să creați un prototip separat.

- [x] Shell/dashboard worker: layout responsiv, navigație activă, controluri citibile, activitate/statutul compact, reordine de țesut, stări și șabloane utile goale. Proprietăți: MainWindow.xaml, MainWindow.xaml.cs și MainWindow.Dashboard.cs
- [x] Editor de lucru: MainWindow.Editor.cs și editor opțional - numai fișiere parțiale. Edite explicite, salvare/descarcare, validare inline, lista de macro dedicate cu drag-and-drop, pași duplicate, captura de cheie fiabilă și câmpurile de fișier/URL.
- [x] Windows lucrător: validare nativă, nume de dispozitiv robuste și capacități SDK, așteptări de modificare limitate și injecții sigure; propriile teste Windows și Windows.
- [x] Parent: progresul de execuție, interfațele de control, conservarea configurării, testarea integrării, fotografiile de ecran renderate reale, publicarea/embalarea și înlocuirea aplicației în curs de desfășurare.

## Contractul de interfață
Controlorul oferă API-uri existente plus stringul LastStatus, boolul HasRunningActions, boolul IsRunning(Guid), stringul GetTileStatus(Guid), stringul ForegroundProcess, boolul CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), void OpenIcue(). Editorul expune boolul HasUnsavedChanges, void DiscardDraft(), boolul CommitDraft(); numele de câmp/metodă existente rămân disponibile în toate clasele parțiale.

## Acceptare
Nu se reîntoarce configurarea utilizatorului real; modificările valabile sunt salvate odată, proiectele nevalide rămân editabile, recuperările discard sunt salvate. Aliasele hotkey native sunt validate fără injectarea intrărilor de testare în ferestre neînsoțite. Tile și macro reorder testat; toate vizualizările sunt vizualizate la dimensiunile desktopului; funcționează / eșuează / este vizibil; modelul de dispozitiv SDK real este afișat. Construiți / testați și publicați pașaportul de fumat, iar aplicația actualizată este lansată.

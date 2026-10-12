<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-09-usp-release.md)

# Ajutor de tastatură, feedback de supraîncărcare și execuție

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

A implementat cele trei îmbunătățiri prioritare în aplicația Windows existentă:

- Special-key magic: inventariul de dispozitive / cheie iCUE susținute, captura G-key temporară, verificarea succesului configurării, validarea conflictului și un proiect de acțiune neînregistrat. Capture rollback, invalidare de apel cu care se întâmplă și redresare normală acoperă erorile și închiderea dialogului.
- Overlay-ul tastaturii: geometrie fizică de referință, legături de profil activ, profil live/edit refresh și evidențiere obișnuită Windows cheie. Păstrați Ctrl+Alt+F1 sau treciți prin pagina de paletă/keyboard. Neactivare, click-through și absență din barul de sarcini.
- Feedback de execuție: tipare așteptare / alergare / pas / finalizat / anulat / respins, progres pas / repetare și un toast neactivat. Feedback exclude conținutul de acțiune și mesajele excepționale. Anularea pasului final nu poate fi raportată. Completion descrie executarea, nu o cerere de stat independentă OBS.

Verificarea :

- 24 Verificările de bază au trecut, inclusiv comanda de fază, rezultatele de eșec/anulare, anularea de etapă finală, izolația ascultătorului și excluderea valorilor de acțiune privată.
- Soluția de eliberare a construcției a trecut fără avertisment / erori.
- Windows Verificarea a fost efectuată; conexiunea K100 ⁇ G1–G6 și configurarea / eliberarea nativă a reușit. puncte audio au fost inspectate fără a schimba starea lor.
- WPF fumatul a trecut cu toate layouturile de referință, magie / catalog / conflict / persistență, configurarea capturilor native live, transferul de apel, refuzul de apel și restaurarea de pauză. Intrarea de apel nativ a fost simulată; o presă umană G-key rămâne o verificare manuală.
- Invarianta actuală a ferestrelor a fost verificată pentru executarea toastului. Proprietățile de interacțiune și afișarea / ascunderea au fost verificate; fotografiile de ecran au fost prezentate pentru vrăjitor, supraîncărcare și toast.
- 27 limbi conțin toate 819 lanțuri canonice; localizator/cheie validare a trecut.

Snapshots și rezultatul GUI complet sunt în artefacte/usp-win-x64/smoke. Testele folosesc profiluri temporare; setările de producție sunt back-up și hash-checate în timpul implementării.

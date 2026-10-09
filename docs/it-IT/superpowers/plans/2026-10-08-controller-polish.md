# Piano di miglioramento dell'usabilità e delle funzionalità del controller

[English (US)](../../../en-US/superpowers/plans/2026-10-08-controller-polish.md) · [Deutsch](../../../de-DE/superpowers/plans/2026-10-08-controller-polish.md) · [Français](../../../fr-FR/superpowers/plans/2026-10-08-controller-polish.md) · [Italiano](../../../it-IT/superpowers/plans/2026-10-08-controller-polish.md)

Richiesta dell'utente: migliorare aspetto, usabilità e funzionalità; l'autorizzazione precedente richiedeva il massimo numero utile di subagenti sui modelli più piccoli necessari e il completamento autonomo.

## Ambito
Conservare i profili esistenti e l'integrazione funzionante con l'SDK Corsair. Migliorare l'applicazione WPF esistente invece di creare un prototipo separato.

- [x] Responsabile shell/dashboard: layout adattivo, navigazione attiva, controlli leggibili, attività/stato compatti, riordino delle riquadri con trascinamento, stati vuoti utili e modelli. Gestisce MainWindow.xaml, MainWindow.xaml.cs e MainWindow.Dashboard.cs.
- [x] Responsabile editor: MainWindow.Editor.cs ed eventuali file parziali dedicati all'editor. Modifiche in bozza esplicite, Salva/Scarta, convalida in linea, elenco macro con riordino tramite trascinamento, duplicazione dei passaggi, acquisizione affidabile degli hotkey e campi per file/URL.
- [x] Responsabile Windows: convalida nativa, nomi dei dispositivi e capacità SDK affidabili, attese limitate dei modificatori e invio sicuro; gestisce Windows e i relativi test.
- [x] Responsabile principale: avanzamento dell'esecuzione, interfacce del controller, conservazione della configurazione, test d'integrazione, schermate effettivamente renderizzate, pubblicazione/pacchetto e sostituzione dell'applicazione in esecuzione.

## Contratto dell'interfaccia
Il controller fornisce le API esistenti e inoltre string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string) e void OpenIcue(). L'editor espone bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); i nomi esistenti dei campi/metodi restano disponibili tra le classi parziali.

## Criteri di accettazione
Nessun ripristino della configurazione reale dell'utente; le modifiche in bozza valide vengono salvate una sola volta, quelle non valide restano modificabili e lo scarto ripristina i dati salvati. Gli alias degli hotkey nativi sono convalidati senza inviare input di prova a finestre non correlate. Riordino di riquadri e passaggi macro verificato; tutte le viste vengono renderizzate alle dimensioni desktop; stato in esecuzione/errore/completamento visibile; modello effettivo del dispositivo SDK mostrato. Build/test e prova del pacchetto pubblicato superano, quindi l'applicazione aggiornata viene avviata.

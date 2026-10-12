<!-- generated-doc locale=it-IT source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Piano di miglioramento dell'usabilità e delle capacità del controller

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Richiesta dell'utente: migliorare aspetto, usabilità e funzionalità; l'autorizzazione preventiva richiede il numero massimo di subagenti utili sui modelli più piccoli necessari e il completamento autonomo.

## Ambito
Conserva i profili esistenti e l'integrazione dell'SDK Corsair funzionante. Migliora l'applicazione WPF esistente anziché creare un prototipo separato.

- [x] Operatore shell/dashboard: layout reattivo, navigazione attiva, controlli leggibili, attività/stato compatti, riordino dei riquadri con trascinamento della selezione, stati vuoti e modelli utili. Possiedi MainWindow.xaml, MainWindow.xaml.cs e MainWindow.Dashboard.cs.
- [x] Operatore editor: MainWindow.Editor.cs e file parziali facoltativi solo per editor. Modifiche graduali esplicite, Salva/Elimina, convalida in linea, elenco macro dedicato con trascinamento della selezione, passaggi duplicati, acquisizione affidabile di tasti di scelta rapida e campi file/URL.
- [x] Windows lavoratore: convalida nativa, nomi di dispositivi affidabili e funzionalità SDK, attese di modificatori limitati e iniezione sicura; propri test Windows e Windows.
- [x] Elemento principale: avanzamento dell'esecuzione del core, interfacce del controller, conservazione della configurazione, test di integrazione, screenshot effettivi renderizzati, pubblicazione/pacchetto e sostituzione dell'applicazione in esecuzione.

## Contratto di interfaccia
Il controller fornisce le API esistenti più la stringa LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), void OpenIcue(). L'editor espone bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); i nomi dei campi/metodi esistenti rimangono disponibili nelle classi parziali.

## Accettazione
Nessun ripristino della configurazione dell'utente reale; le modifiche graduali valide vengono salvate una volta, le bozze non valide rimangono modificabili, l'eliminazione ripristina i dati salvati. Alias ​​di tasti di scelta rapida nativi convalidati senza inserire input di test in finestre non correlate. Testato il riordino di piastrelle e macro; tutte le visualizzazioni vengono visualizzate alle dimensioni del desktop; stato in esecuzione/non riuscito/completo visibile; modello di dispositivo SDK effettivo visualizzato. Creazione/test e passaggio di fumo pubblicato e avvio dell'applicazione aggiornata.

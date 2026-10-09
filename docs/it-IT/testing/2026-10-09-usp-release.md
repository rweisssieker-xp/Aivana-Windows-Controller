# Assistenza tastiera, overlay e feedback dell'esecuzione

[English (US)](../../en-US/testing/2026-10-09-usp-release.md) · [Deutsch](../../de-DE/testing/2026-10-09-usp-release.md) · [Français](../../fr-FR/testing/2026-10-09-usp-release.md) · [Italiano](../../it-IT/testing/2026-10-09-usp-release.md)

Sono stati implementati nell'app Windows esistente i tre miglioramenti prioritari:

- Assistente per i tasti speciali: inventario reale di dispositivi/tasti supportati da iCUE, acquisizione temporanea dei tasti G, verifica del successo della configurazione, convalida dei conflitti e bozza di azione non salvata. Ripristino in caso di errore, invalidazione dei callback in coda e ripristino delle associazioni normali gestiscono gli errori e la chiusura della finestra di dialogo.
- Overlay tastiera: geometria di riferimento fisica, associazioni del profilo attivo, aggiornamento in tempo reale di profilo/modifica ed evidenziazione dei normali tasti Windows. Tenere premuti Ctrl+Alt+F1 o attivare/disattivare tramite la tavolozza/la pagina Tastiera. Non attiva la finestra, lascia passare i clic e non compare nella barra delle applicazioni.
- Feedback dell'esecuzione: fasi tipizzate Waiting/Running/Step/Completed/Cancelled/Failed/Rejected, avanzamento dei passaggi/ripetizioni e toast non attivante. Il feedback esclude il contenuto delle azioni e i messaggi di eccezione. L'annullamento dell'ultimo passaggio non può essere segnalato come completamento. Il completamento descrive l'esecuzione, non una verifica indipendente dello stato OBS.

Verifica:

- Superati 24 controlli Core, inclusi ordine delle fasi, esiti di errore/annullamento, annullamento dell'ultimo passaggio, isolamento delle eccezioni dei listener ed esclusione dei valori privati delle azioni.
- Build della soluzione Release superata senza avvisi/errori.
- Controlli Windows superati; la K100 collegata ha riportato G1–G6 e la configurazione/rilascio nativi sono riusciti. Gli endpoint audio sono stati esaminati senza modificarne lo stato.
- Test smoke WPF superato con tutti i layout di riferimento dell'overlay, assistente/catalogo/conflitti/persistenza delle bozze, configurazione di acquisizione nativa dal vivo, inoltro dei callback, rifiuto dei callback obsoleti e ripristino delle associazioni in pausa. L'input del callback nativo è stato simulato; resta da verificare manualmente la pressione di un tasto G da parte di una persona.
- Verificata l'invarianza della finestra in primo piano per il toast di esecuzione. Verificate le proprietà d'interazione e la visualizzazione/nascondimento dell'overlay; sono state renderizzate schermate per assistente, overlay e toast.
- Le 27 lingue contengono tutte le 819 stringhe canoniche; la convalida offline di chiavi e segnaposto è stata superata.

Snapshot e risultato GUI completo sono in artifacts/usp-win-x64/smoke. I test usano profili temporanei; le impostazioni di produzione vengono salvate e ne viene verificato l'hash durante la distribuzione.

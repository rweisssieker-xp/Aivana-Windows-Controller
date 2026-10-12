<!-- generated-doc locale=it-IT source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-09-usp-release.md)

# Assistenza da tastiera, overlay e feedback sull'esecuzione

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementati i tre miglioramenti prioritari nell'app Windows esistente:

- Procedura guidata per chiavi speciali: inventario effettivo di dispositivi/chiavi supportati da iCUE, acquisizione temporanea di chiavi G, verifica della configurazione riuscita, convalida dei conflitti e bozza di azioni non salvate. Il rollback dell'acquisizione, l'invalidazione della richiamata in coda e il ripristino dell'associazione normale coprono gli errori e la chiusura della finestra di dialogo.
- Overlay tastiera: geometria di riferimento fisico, associazioni del profilo attivo, aggiornamento del profilo live/modifica e normale evidenziazione dei tasti Windows. Tieni premuto Ctrl+Alt+F1 o scorri la pagina della tavolozza/tastiera. Non attivabile, cliccabile e assente dalla barra delle applicazioni.
- Feedback sull'esecuzione: fasi digitate di Attesa/In esecuzione/Passaggio/Completato/Annullato/Non riuscito/Rifiutato, avanzamento del passaggio/ripetizione e un avviso di avviso non attivante. Il feedback esclude il contenuto dell'azione e i messaggi di eccezione. L'annullamento dell'ultimo passaggio non può segnalare il completamento. Il completamento descrive l'esecuzione, non una query di stato OBS indipendente.

Verifica:

- 24 Controlli principali superati, inclusi l'ordinamento delle fasi, i risultati di errore/annullamento, l'annullamento del passaggio finale, l'isolamento delle eccezioni del listener e l'esclusione dei valori delle azioni private.
- La build della soluzione di rilascio è stata completata senza avvisi/errori.
- Windows controlli superati; il K100 connesso ha segnalato G1–G6 e la configurazione/rilascio nativo è riuscito. Gli endpoint audio sono stati ispezionati senza modificarne lo stato.
- WPF fumo superato con tutti i layout di sovrapposizione di riferimento, procedura guidata/catalogo/conflitti/persistenza bozza, configurazione di acquisizione nativa in tempo reale, inoltro di richiamata, rifiuto di richiamata obsoleta e ripristino del collegamento in pausa. È stato simulato l'input di callback nativo; la pressione fisica del tasto G umano rimane un controllo manuale.
- L'effettiva invarianza della finestra in primo piano è stata verificata per il brindisi dell'esecuzione. Sono state controllate le proprietà di interazione della sovrapposizione e mostra/nascondi; sono stati renderizzati gli screenshot per la procedura guidata, l'overlay e il toast.
- Le lingue 27 contengono tutte le stringhe canoniche 819; segnaposto/convalida chiave offline superata.

Le istantanee e il risultato completo della GUI si trovano in artifacts/usp-win-x64/smoke. I test utilizzano profili temporanei; viene eseguito il backup delle impostazioni di produzione e il controllo dell'hash durante la distribuzione.

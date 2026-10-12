<!-- generated-doc locale=it-IT source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Piano di implementazione dell'estensione del controller

**Obiettivo:** Implementare il monitor, i gesti, il flusso di lavoro, la registrazione, la diagnosi del dispositivo, lo scambio di profili portatili e i miglioramenti del vassoio richiesti.

**Architettura:** Estendi i livelli Core/Windows/Desktop esistenti. Macchine a stato puro e logica di configurazione portatile risiedono in Core, integrazioni native in Windows ed editor di bozze modali in Desktop. Conserva le modifiche dirty dell'area di lavoro esistenti e la configurazione utente distribuita.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitorare il servizio, la persistenza del target e lo stato live asincrono; test ID non disponibili e cancellazione.
- [x] Macchina a stati gestuali e livelli mantenuti; testare i bordi, ripetere, ripristinare e gli attacchi immediati esistenti.
- [x] Condizioni del flusso di lavoro, rami forward e politica degli errori; istantanee di prova e cancellazione.
- [x] Registrazione esplicita di scorciatoie con revisione della bozza; testare il filtraggio e lo smaltimento dei ganci.
- [x] Scambio profili di cartelle condivise con controlli di revisione e anteprime; testare i conflitti e le mappature portatili.
- [x] Chiavi speciali standard generiche per dispositivo e diagnosi; testare l'analisi, duplicare le mappature e mettere in pausa il comportamento.
- [x] Risorse vassoio/finestra/icona eseguibile e aspetto in pausa; verificare le risorse nell'eseguibile pubblicato.
- [x] Integrare tutti i punti di ingresso della tavolozza/editor e il testo localizzato; eseguire i controlli Core, Windows e WPF.
- [x] Pubblica in una directory di staging separata, conserva le impostazioni dell'utente e riavvia l'app aggiornata.

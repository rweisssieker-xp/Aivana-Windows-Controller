<!-- generated-doc locale=it-IT source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO estensioni di controllo

L'utente ha richiesto tutti e nove i miglioramenti proposti e un'icona riconoscibile nella barra delle applicazioni. Le configurazioni esistenti, in particolare la luminosità G1/G2, devono rimanere compatibili. Le modifiche esistenti al marchio e alla documentazione non vincolate appartengono all'area di lavoro condivisa e devono essere conservate.

## Risultati finali e confini

1. Le azioni di luminosità selezionano tutti i monitor supportati o un ID monitor specifico stabile. I target espliciti mancanti falliscono visibilmente. Il dashboard mostra la luminosità corrente, il volume di uscita, lo stato del microfono e lo stato OBS attraverso letture asincrone limitate.
2. I tasti speciali supportano la pressione immediata, il tocco, la pressione prolungata e il doppio tocco, con soppressione della ripetizione. Un selettore di livello bloccato modifica temporaneamente la mappatura attiva e il rilascio la ripristina. Le associazioni immediate esistenti mantengono il proprio comportamento. Pausa, modifica del profilo, disconnessione e cancellazione dei gesti in sospeso.
3. Il progettista del flusso di lavoro modifica le condizioni, inoltra i rami e interrompe/continua la politica di errore a livello transazionale in una bozza di riquadro. Le condizioni riguardano file, processi in esecuzione e titoli di finestre. Nessun ciclo di salto illimitato.
4. La registrazione esplicita cattura gli accordi di scelta rapida e attende, ignora l'input inserito e si ferma su Escape. Le azioni registrate vengono esaminate prima dell'adozione; il testo ordinario non viene registrato.
5. Le chiavi speciali standard per dispositivo di altri produttori utilizzano Windows Raw Input. Le chiavi proprietarie richiedono un'API del fornitore supportata o la mappatura del fornitore su una chiave standard; la funzionalità non supportata viene mostrata onestamente. La diagnosi spiega il profilo, la pausa, i conflitti di associazione e lo stato effettivo dell'adattatore.
6. Lo scambio di profili utilizza una cartella condivisa selezionata dall'utente (comprese le cartelle sincronizzate da un client cloud esistente). I controlli di revisione impediscono le sovrascritture silenziose. Pull visualizza le anteprime delle modifiche e importa le azioni disabilitate con le mappature hardware rimosse. L'inclusione del contenuto dell'azione è esplicita. Nessuna credenziale cloud viene archiviata da MAKRIVO.
7. Le stringhe dell'interfaccia utente appartengono ai cataloghi in lingua 27 esistenti; DE/EN/FR/IT rimangono le lingue minime per la documentazione sottoposta a revisione. Le lacune traduttive devono essere rese pubbliche anziché dichiarate complete.
8. Un segno riconoscibile della tastiera M verde acqua sostituisce l'icona generica nella barra delle applicazioni Windows. La modalità in pausa ha un badge di pausa color ambra. Il branding dell'eseguibile/della finestra utilizza lo stesso marchio. La descrizione comando include lo stato e il profilo.

## Verifica

I controlli principali riguardano la compatibilità con le versioni precedenti, le istantanee, la cancellazione, la tempistica dei gesti, i rami/errori del flusso di lavoro, il filtraggio del registratore e i conflitti di sincronizzazione. I controlli Windows riguardano l'enumerazione della visualizzazione di sola lettura, gli eventi di rilascio dell'SDK e l'analisi Raw Input. WPF I controlli di fumo coprono nuovi punti di ingresso e risorse senza modificare la luminosità reale del monitor, digitare in un'altra applicazione o sovrascrivere i profili utente. La distribuzione conserva un backup e la configurazione G1/G2.

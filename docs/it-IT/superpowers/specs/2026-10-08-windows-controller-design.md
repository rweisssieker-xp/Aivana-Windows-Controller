# Aivana Windows Controller — Progetto

[English (US)](../../../en-US/superpowers/specs/2026-10-08-windows-controller-design.md) | [Deutsch](../../../de-DE/superpowers/specs/2026-10-08-windows-controller-design.md) | [Français](../../../fr-FR/superpowers/specs/2026-10-08-windows-controller-design.md) | [Italiano](../../../it-IT/superpowers/specs/2026-10-08-windows-controller-design.md)

Aggiornato: 8 ottobre 2026. Stato: da esaminare prima dell'implementazione.

## Obiettivo e ambito concordato

Un'app Windows autonoma trasforma una normale tastiera PC in un pannello di azioni liberamente configurabile, simile a Stream Deck. L'utente ha approvato tutti i tipi di azione proposti, le macro, il rilevamento dispositivi, i profili e una buona interfaccia grafica. Le tastiere Corsair e di altri produttori collegate vengono rilevate; le funzioni aggiuntive del produttore vengono offerte solo se sono effettivamente supportate.

Presupposti: interfaccia tedesca, uso locale senza account, Windows 11 x64 come primo obiettivo, nessuna installazione di driver. La tastiera resta utilizzabile per la normale digitazione. Per impostazione predefinita, le associazioni globali usano combinazioni con modificatori. I singoli tasti lettera non vengono riservati globalmente.

## Decisione tecnica

App nativa C#/.NET 10 con WPF e MVVM. Nell'area di lavoro è disponibile l'SDK .NET 10 stabile. WPF consente di creare un'interfaccia desktop personalizzata e di integrarsi direttamente con Windows. Le alternative sarebbero un'interfaccia Web con un servizio Windows separato o un'app desktop multipiattaforma; entrambe aggiungono complessità di runtime e comunicazione a questo progetto Windows. La soluzione nativa è quindi la base scelta.

Suddivisione: progetto Desktop per viste e tray, progetto Core per configurazione e logica d'esecuzione, progetto Windows per hotkey, simulazione dell'input e dispositivi, progetto Test per regole indipendenti dalla piattaforma. Gli adattatori dei produttori usano un'interfaccia Capability stabile e restano indipendenti dalle azioni standard.

## Interfaccia

Design scuro con superfici antracite sobrie, testo leggibile e accento turchese. Navigazione a sinistra per dashboard, profili, dispositivi e impostazioni. In alto: profilo attivo, ricerca e interruttore per mettere in pausa tutti gli hotkey. L'interfaccia resta usabile con ridimensionamento Windows dal 100 al 200 per cento.

Il dashboard contiene una griglia di riquadri personalizzabile. Ogni riquadro mostra icona, nome, hotkey e stato d'esecuzione. Il clic esegue la stessa azione dell'hotkey. A destra si apre un editor con tipo di azione, parametri, acquisizione hotkey, icona/colore, attivazione e pulsante di prova. Sono disponibili creazione, duplicazione, eliminazione e riordino. Una vista tastiera mostra le associazioni schematicamente per il layout standard tedesco; non dichiara una geometria esatta del dispositivo rilevato.

I profili si possono creare, copiare, rinominare e attivare manualmente. Facoltativamente si può associare al profilo il nome di un processo. Un programma corrispondente in primo piano attiva il proprio profilo; altrimenti vale il profilo globale predefinito. Se corrispondono più profili, decide un ordine mostrato esplicitamente. L'editor segnala i conflitti hotkey direttamente accanto all'associazione.

## Hotkey e input

Windows RegisterHotKey gestisce le normali combinazioni globali e sopprime le ripetizioni del tasto. Si registra solo il profilo attivo. Le combinazioni riservate o già occupate vengono mostrate come errori; l'app non le segnala come attivate correttamente. L'acquisizione nell'editor non esegue azioni.

Prima di simulare l'input, i tasti modificatori che hanno attivato l'azione vengono rilasciati oppure si attende il loro rilascio fisico. Testo Unicode e combinazioni di tasti vengono inviati tramite SendInput. Ogni tasto premuto dall'app viene rilasciato anche in caso di annullamento. Le azioni vengono eseguite fuori dal thread UI; esecuzione e cambio profilo non devono bloccare l'interfaccia.

## Azioni e macro

Tipi di azione: avviare un programma con argomenti e directory di lavoro; aprire file/cartelle tramite Windows; aprire pagine HTTP/HTTPS; digitare testo Unicode; inviare combinazioni di tasti; controlli media Play/Pausa/Successivo/Precedente/Stop; volume su/giù/silenzioso; cambiare profilo. I parametri vengono convalidati prima del salvataggio e di nuovo prima dell'esecuzione. I programmi vengono avviati direttamente; gli argomenti non vengono interpretati implicitamente come comandi shell.

Le macro contengono un elenco ordinato delle azioni indicate e passaggi di pausa espliciti. Si possono aggiungere, modificare e spostare i passaggi. Le ripetizioni sono configurabili con un numero finito (1–100); i cicli infiniti non fanno parte della prima versione. Una macro in esecuzione può essere arrestata tramite l'interfaccia o un hotkey dedicato. Un'attivazione ripetuta della stessa associazione in esecuzione non avvia un processo parallelo. Le diverse macro che simulano input vengono serializzate per evitare che le pressioni si mescolino. Se un passaggio fallisce, la macro si arresta e indica il passaggio interessato.

## Dispositivi e produttori

Inventario dei dispositivi tramite Windows Raw Input e informazioni aggiuntive: nome visualizzato, percorso del dispositivo, identificativo produttore/prodotto se disponibile, stato di connessione e capacità rilevate. Il collegamento/scollegamento aggiorna l'elenco senza riavvio. Un dispositivo fisico può avere più voci HID; vengono raggruppate con identificatori affidabili oppure, se non disponibili, mostrate come voci separate.

Gli hotkey normali valgono per tutte le tastiere. La vista dispositivi mostra informazioni e capacità; la modalità concordata non include una tastiera separata con tasti intercettati in esclusiva.

Il rilevamento Corsair funziona anche senza iCUE. Un adattatore opzionale per iCUE SDK offre stato di connessione e azioni d'illuminazione supportate se sono presenti SDK, iCUE e un dispositivo compatibile. L'interfaccia distingue tra rilevato, SDK disponibile, connesso e realmente controllabile. L'SDK documenta soprattutto il controllo dei LED; i tasti G proprietari sono supportati come normali hotkey solo se Windows riceve gli input corrispondenti. Non si garantisce il supporto indiscriminato di tutti i tasti speciali.

Gli altri produttori ricevono inizialmente il rilevamento generico e le azioni standard. L'interfaccia adattatore consente integrazioni successive; le capacità non supportate vengono indicate in modo veritiero. Non si dichiara superato un test hardware per modelli non disponibili.

## Salvataggio e uso quotidiano

Configurazione JSON con versione nella cartella dati locale dell'app. Salvataggio atomico con ultimo backup valido; i file non validi non vengono sovrascritti silenziosamente. L'importazione controlla formato, versione, ID e parametri, quindi mostra un'anteprima prima dell'applicazione. Le azioni importate diventano eseguibili solo dopo un'attivazione esplicita. L'export include profili e associazioni.

Menu tray: apri, scegli profilo, metti in pausa gli hotkey, annulla macro, esci. Per impostazione predefinita, chiudere la finestra la riduce nell'area di notifica e lo spiega al primo utilizzo. L'avvio automatico è facoltativo e disattivato. Una sola istanza gestisce gli hotkey. Alla chiusura gli hotkey vengono deregistrati, le esecuzioni annullate e le risorse rilasciate.

Vista attività locale con cronologia limitata di successi, errori e annullamenti. Nessun registro di tutte le pressioni della tastiera. Il contenuto delle azioni di testo non viene riportato nei messaggi di errore. Nessun servizio cloud o telemetria.

## Gestione errori e limiti

Programmi mancanti, URL non validi, hotkey occupati, profili danneggiati e SDK del produttore non raggiungibile generano messaggi espliciti in tedesco. Un errore dell'adattatore non compromette le azioni standard. Gli input nelle applicazioni con privilegi superiori possono essere bloccati dai limiti di autorizzazione Windows; gli errori vengono spiegati e l'app non richiede i privilegi di amministratore per impostazione predefinita.

## Controlli e accettazione

Controlli automatici per priorità dei profili, rilevamento conflitti, importazione configurazione, salvataggio atomico, ordine macro, ripetizione, annullamento e propagazione degli errori. Test smoke Windows per hotkey reali, testo e combinazioni di tasti, azioni multimediali, inventario dispositivi, tray e chiusura pulita. Nessun test deve avviare per errore programmi o siti web arbitrari.

Controllo GUI: creare e modificare un'associazione, acquisirla senza esecuzione, verificare che clic e hotkey abbiano lo stesso effetto, annullare una macro, mostrare un conflitto, cambiare profilo, visualizzare i dispositivi, importare/esportare e provare il ridimensionamento. I controlli hardware specifici del produttore vengono documentati separatamente usando un dispositivo realmente disponibile.

Consegna: codice sorgente, istruzioni riproducibili per build/avvio, manuale utente in tedesco e artefatto locale pubblicato per Windows x64. Il risultato è un'app eseguibile con tutte le funzioni generiche; le capacità Corsair risultano disponibili solo se è dimostrato il supporto dell'SDK/dispositivo.

## Fonti tecniche

- Microsoft Keyboard Input / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Input: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- SDK ufficiale Corsair iCUE: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Passo successivo

Dopo la revisione di questo progetto: preparare un piano d'implementazione concreto e sottoporlo per la selezione della modalità di esecuzione. Nella cartella di lavoro vuota non c'è ancora un repository Git; perciò questo documento non è stato ancora sottoposto a commit.

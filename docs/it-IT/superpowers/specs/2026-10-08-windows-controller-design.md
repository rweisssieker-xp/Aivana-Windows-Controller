<!-- generated-doc locale=it-IT source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Bozza

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stato: 8. Ottobre 2026. Stato: da rivedere prima dell'implementazione.

## Obiettivo e ambito concordato

Un'app Windows autonoma trasforma una normale tastiera per PC in un pannello di azione liberamente configurabile simile a uno stream deck. L'utente ha confermato tutti i tipi di azioni suggeriti, le macro, il rilevamento dei dispositivi, i profili e una buona GUI. Corsair collegato e altre tastiere vengono riconosciute; funzioni aggiuntive del produttore vengono offerte solo se effettivamente supportate.

Presupposti: interfaccia tedesca, utilizzo locale senza account, Windows 11 x64 come prima destinazione, nessuna installazione di driver. La tastiera rimane utilizzabile per la normale digitazione. Per impostazione predefinita, le mappature globali utilizzano scorciatoie da tastiera con modificatori. Per impostazione predefinita, i tasti delle singole lettere non sono riservati a livello globale.

## Decisione tecnica

C#/.NET-10-App nativo con WPF e MVVM. Nell'area di lavoro è disponibile lo stabile .NET-10-SDK. WPF consente un'interfaccia desktop personalizzata e l'integrazione diretta di Windows. Le alternative potrebbero essere un'interfaccia web con un servizio Windows separato o un'app desktop multipiattaforma; entrambi apportano ulteriore complessità in termini di runtime e comunicazione a questo progetto Windows. La variante nativa è quindi la base scelta.

Divisione: progetto desktop per viste e vassoio, progetto principale per la configurazione e la logica di esecuzione, progetto Windows per tasti di scelta rapida, simulazione di input e dispositivi, progetto di test per regole indipendenti dalla piattaforma. Gli adattatori del produttore dipendono da un'interfaccia con funzionalità fisse e sono indipendenti dalle azioni standard.

## superficie

Design scuro con superfici tranquille color antracite, caratteri di facile lettura e accenti turchesi. Collegamenti di navigazione per dashboard, profili, dispositivi e impostazioni. Profilo attivo, cerca e cambia per mettere in pausa tutti i tasti di scelta rapida in alto. L'interfaccia rimane utilizzabile con il ridimensionamento percentuale 100–200 Windows.

Il dashboard include una griglia di riquadri personalizzabile. Ogni riquadro mostra l'icona, il nome, il tasto di scelta rapida e lo stato di esecuzione. Fare clic esegue la stessa azione del tasto di scelta rapida. Sulla destra si apre un editor con tipo di azione, parametri, registrazione dei tasti di scelta rapida, simbolo/colore, pulsante di attivazione e test. Sono disponibili Nuovo, Duplica, Elimina e Riordina. Una visualizzazione tastiera mostra schematicamente le assegnazioni per il layout tedesco standard; non rivendica l'esatta geometria del dispositivo rilevato.

I profili possono essere creati, copiati, rinominati e attivati manualmente. Un nome di processo viene facoltativamente assegnato a un profilo. Un programma in primo piano adatto attiva il suo profilo, altrimenti vale il profilo standard globale. Se esistono più profili corrispondenti, si applica un ordine esplicitamente visibile. L'editor spiega i conflitti dei tasti di scelta rapida direttamente durante l'assegnazione.

## Tasti di scelta rapida e input

Windows RegisterHotKey gestisce le normali combinazioni globali con la ripetizione dei tasti soppressa. Viene registrato solo il profilo attivo. Le combinazioni riservate o già occupate appaiono come errori; l'app non lo segnala come attivato correttamente. La registrazione nell'editor non attiva alcuna azione.

Prima degli input simulati, i tasti modificatori di attivazione vengono rilasciati oppure si attende il rilascio fisico. Il testo Unicode e le combinazioni di tasti vengono inviati utilizzando SendInput. Ogni tasto premuto verrà rilasciato nuovamente anche se si annulla. Le azioni vengono eseguite all'esterno del thread dell'interfaccia utente; L'esecuzione e le modifiche al profilo non devono bloccare la superficie.

## Azioni e macro

Tipi di azioni: avviare il programma con argomenti e directory di lavoro, aprire file/cartella tramite Windows, aprire il sito Web HTTP/HTTPS, inserire testo Unicode, inviare combinazione di tasti, riproduzione multimediale/pausa/successivo/precedente/stop, volume su/giù/disattiva e cambia profilo. I parametri vengono convalidati prima del salvataggio e nuovamente prima dell'esecuzione. I programmi vengono avviati direttamente; Gli argomenti non vengono interpretati implicitamente come comandi di shell.

Le macro contengono un elenco ordinato di azioni denominate e passaggi di pausa espliciti. I passaggi possono essere aggiunti, modificati e spostati. Le ripetizioni sono infine configurabili (1–100); I cicli infiniti non fanno parte della prima versione. Una macro in esecuzione può essere terminata tramite l'interfaccia o tramite il tasto di scelta rapida Annulla. L'attivazione ripetuta della stessa assegnazione corrente non avvia un'esecuzione parallela. Macro diverse con simulazione di input vengono serializzate in modo che la pressione dei tasti non si confonda. Se si verifica un errore di passaggio, la macro si interrompe e visualizza il passaggio interessato.

## Dispositivi e produttori

Inventario dei dispositivi tramite Windows Raw Input e informazioni aggiuntive sul dispositivo: nome visualizzato, percorso del dispositivo, identificatore del produttore/prodotto se disponibile, stato della connessione e funzionalità rilevate. Collegando/scollegando si aggiorna l'elenco senza riavviare. Un dispositivo fisico può avere più voci HID; Questi sono raggruppati in base a identificatori affidabili, altrimenti vengono mantenuti visibilmente come voci separate.

I normali tasti di scelta rapida si applicano a tutte le tastiere. La vista del dispositivo fornisce informazioni e funzionalità; una tastiera separata con tasti esclusivamente intercettati non rientra in questa modalità concordata.

Il rilevamento Corsair funziona anche senza iCUE. Un adattatore SDK iCUE opzionale fornisce lo stato della connessione e le azioni di illuminazione supportate quando sono presenti SDK, iCUE e un dispositivo compatibile. L'interfaccia utente differenzia riconosciuta, SDK disponibile, connessa ed effettivamente controllabile. L'SDK documenta principalmente il controllo LED; I tasti G proprietari sono supportati come i normali tasti di scelta rapida solo se Windows riceve l'input appropriato. Non esiste una promessa generale per tutte le chiavi speciali.

Gli altri produttori inizialmente ricevono un rilevamento generico e azioni standard. L'interfaccia dell'adattatore consente integrazioni successive; le abilità non supportate vengono visualizzate onestamente. I test hardware per modelli inesistenti non vengono segnalati come superati.

## Stoccaggio e vita quotidiana

Configurazione JSON con versione nella directory dei dati dell'app locale. Salvataggio atomico con l'ultimo backup valido; i file errati non vengono sovrascritti silenziosamente. L'importazione controlla formato, versione, ID e parametri e mostra un'anteprima prima dell'applicazione. Le azioni importate possono essere eseguite solo dopo l'attivazione esplicita. L'esportazione contiene profili e assegnazioni.

Menu della barra delle applicazioni: Apri, selezione profilo, pausa tasti di scelta rapida, annullamento macro, uscita. La finestra Chiudi si riduce a icona nel vassoio per impostazione predefinita e lo spiega la prima volta. L'avvio automatico è facoltativo e disattivato per impostazione predefinita. Solo un'istanza gestisce i tasti di scelta rapida. Exit registra i tasti di scelta rapida, interrompe le esecuzioni e rilascia risorse.

Visualizzazione attività locale con cronologia limitata per successi, fallimenti e annullamenti. Nessun registro di tutte le sequenze di tasti. Il contenuto dell'azione di testo non è specificato nei messaggi di errore. Nessun servizio cloud e nessuna telemetria.

## Gestione degli errori e limiti

Programmi mancanti, URL non validi, tasti di scelta rapida occupati, profili danneggiati e SDK del produttore irraggiungibile ricevono messaggi specifici in tedesco. Un guasto dell'adattatore non influisce sulle azioni standard. Le voci ad applicazioni con privilegi più elevati potrebbero non riuscire a causa dei limiti di autorizzazione Windows; Vengono spiegati gli errori, l'app non richiede diritti di amministratore per impostazione predefinita.

## Test e accettazione

Controlli automatizzati per priorità del profilo, rilevamento dei conflitti, importazione della configurazione, archiviazione atomica, ordinamento delle macro, nuovi tentativi, annullamento e propagazione degli errori. Windows test del fumo per tasti di scelta rapida reali, scorciatoie di testo e da tastiera, azioni multimediali, inventario dei dispositivi, vassoio e uscita pulita. Nessun test che avvia accidentalmente programmi o siti Web arbitrari.

Controllo GUI: creazione e modifica assegnazione, registrazione senza esecuzione, clic/tasto di scelta rapida con lo stesso effetto, annullamento macro, messaggio di conflitto, modifica profilo, visualizzazione dispositivo, importazione/esportazione e ridimensionamento. Il test dell'hardware specifico del produttore è documentato separatamente con il dispositivo reale.

Consegna: codice sorgente, istruzioni di compilazione/avvio riproducibili, manuale utente in tedesco e Windows-x64-Publish-Artefakt locale. Il successo significa un'app eseguibile con tutte le funzionalità generiche; Le funzionalità Corsair sono contrassegnate come disponibili solo con supporto SDK/dispositivo comprovato.

## Fonti tecniche

- Microsoft Ingresso tastiera/RegistraHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Ingresso: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Ufficiale Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Prossimo passo

Dopo aver esaminato questa bozza: creare un piano di implementazione concreto e sottoporlo alla selezione dell'implementazione. Non c'è ancora alcun repository Git nella cartella di lavoro vuota; Pertanto questo documento non è stato ancora impegnato.

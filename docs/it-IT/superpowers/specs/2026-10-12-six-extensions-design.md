<!-- generated-doc locale=it-IT source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: sei estensioni di prodotto e adattatori per tastiera

Data: 2026-10-12
Stato: progetto proposto per la revisione dell'utente; l'implementazione non è iniziata.

## Obiettivo
Fornisci tutte e sei le estensioni richieste nell'applicazione Windows/WPF esistente, preservando i profili esistenti, le assegnazioni G1/G2, il funzionamento locale e l'utilizzo senza account. Espandi l'integrazione della tastiera a Razer e Logitech, con un'architettura basata su funzionalità riutilizzabili per altri produttori. Il nome del produttore o il layout della tastiera non costituiscono mai la prova che i controlli proprietari siano supportati.

## Decisione di architettura
Estendi Aivana.Core con contratti serializzabili e convalida, Aivana.Windows con Windows e adattatori fornitore e Aivana.Desktop con editor transazionali e visualizzazioni di stato. Utilizza progetti complementari separati per l'esecuzione isolata del plug-in e il servizio del pannello mobile locale. Evita una riscrittura o un singolo processo privilegiato che ospita ogni estensione. La configurazione esistente deve essere caricata invariata; aggiungere tipi di azioni anziché riordinare i relativi valori enum persistenti.

Alternative considerate: un'implementazione monolitica intreccerebbe l'esecuzione di hardware, rete e flusso di lavoro; la sostituzione di WPF aggiungerebbe lavoro di migrazione non correlato. Si consigliano estensioni modulari.

## 1. Tastierino macro tastiera indipendente
Memorizza i collegamenti in base all'identità del dispositivo, al codice di scansione fisica e ai flag di chiave estesa. Aggiungi modalità di osservazione ed esclusiva esplicite, rilevamento dei dispositivi, acquisizione di chiavi, soppressione delle ripetizioni, gesti, livelli bloccati e diagnostica di riconnessione. Mantieni principale/tastierino numerico Inserisci le distinzioni e testa i layout tedesco/americano. I collegamenti prendono di mira una tastiera fisica selezionata; le identità di riconnessione ambigue richiedono la riselezione anziché il controllo di un altro dispositivo.

Raw Input fornisce solo l'osservazione. La soppressione esclusiva per dispositivo richiede un backend effettivo per il filtro della tastiera; non correlare mai gli hook globali di basso livello con Raw Input e rivendicare una soppressione affidabile specifica del dispositivo. Fornire un watchdog, un rilascio di emergenza, un comportamento di apertura in caso di errore e un programma di installazione/disinstallazione. Non acquisire input desktop protetti. L'installazione del driver e l'attivazione in modalità esclusiva sono azioni esplicite e separate dell'utente.

Gate driver: seleziona un backend supportato, adeguatamente concesso in licenza e firmato compatibile con le configurazioni Windows 11 di destinazione oppure sviluppa/firma un filtro dedicato. L'intercettazione è un candidato da valutare, non una dipendenza dalla produzione impegnata: il suo README ufficiale descrive i test tramite Windows 10 e licenze commerciali distinte. Fino a quando questo limite non viene soddisfatto, la modalità esclusiva deve essere mostrata come non disponibile; la funzionalità non può essere dichiarata completa.

Accettazione: con due tastiere fisiche, lo stesso tasto esegue una macro e non produce alcun normale input applicativo solo sul dispositivo esclusivo selezionato; gli altri input da tastiera rimangono inalterati. Verifica la disconnessione, l'arresto anomalo, l'annullamento, la sospensione/ripresa e la rimozione del driver sull'hardware reale.

## 2. Configurazione guidata dello spazio di lavoro
Aggiungi una procedura guidata per il posto di lavoro, le riunioni, lo streaming, la ricerca e il cambio di progetto. Scopri le applicazioni installate, i dispositivi audio, i monitor e le scene OBS. Gli utenti scelgono target e associazioni; visualizzare in anteprima i passaggi risultanti e convalidare i requisiti mancanti. Salva solo dopo la revisione, senza alcuna azione in esecuzione durante la configurazione. I profili generati rimangono modificabili negli editor esistenti.

Accettazione: crea un profilo di lavoro senza modificare manualmente percorsi eseguibili o ID dispositivo; la cancellazione lascia invariate le impostazioni; il software mancante ha una spiegazione attuabile.

## 3. Esecuzione verificata
Introduci controlli preliminari, risultati digitati per passaggio e postcondizioni opzionali. Distinguere gli stati richiesto, completato, verificato, non riuscito, scaduto, annullato e non verificabile. Verifica l'audio/microfono, la luminosità, la disponibilità del processo/finestra e lo stato OBS con letture asincrone limitate. L'invio di scorciatoie/testo non è una prova che l'applicazione di destinazione abbia accettato il comando. Gli effetti collaterali esterni non sono universalmente reversibili; offrire un compenso solo se implementato.

Accettazione: obiettivi espliciti disconnessi non ripiegano mai silenziosamente; lo stato osservato errato non riporta mai il successo verificato; la cancellazione interrompe il polling; gli errori identificano il passaggio e l'azione di ripristino. La cronologia delle esecuzioni omette payload e segreti di testo.

## 4. Plugin e pacchetti condivisibili
I manifest JSON con versione dichiarano azioni, schemi di parametri, provider di stati, dipendenze e funzionalità. Esegui plug-in eseguibili in un processo separato su IPC locale limitato con handshake, scadenze e cancellazione. Rifiutare messaggi non validi e versioni incompatibili; gestire gli arresti anomali senza terminare il controller. Le dichiarazioni di capacità sono divulgazione e mediazione, non una rivendicazione di sandboxing del sistema operativo.

Fornire anteprima di esportazione/importazione di pacchetti, hash di contenuto, protezione di attraversamento del percorso, limiti di dimensione, controlli di versione/dipendenza, abilitazione esplicita e rollback dell'aggiornamento. I plugin eseguibili importati non devono essere eseguiti automaticamente. Includi un plug-in di esempio funzionante con un flusso di installazione/azione/stato/disinstallazione end-to-end. Per questa versione non è richiesto alcun servizio di mercato pubblico.

Accettazione: creare, esportare, installare ed eseguire un'integrazione di esempio reale; dimostrare il timeout, l'isolamento degli arresti anomali, l'errore delle dipendenze, il rifiuto dell'attraversamento e l'importazione disabilitata.

## 5. Windows Automazione interfaccia utente
Aggiungi un selettore di elementi e azioni limitate per invocazione, valore, selezione, attivazione/disattivazione, messa a fuoco e attesa di un elemento/stato. Selettori persistenti che utilizzano processo, ID di automazione, tipo di controllo e contesto antenato; evitare la registrazione delle sole coordinate. Mostra il conteggio delle corrispondenze del selettore e verifica i selettori in sola lettura. I campi password sono esclusi dalla registrazione. Gestire in modo esplicito i controlli non supportati e le restrizioni a livello di integrità Windows.

Estendi la registrazione con azioni UIA rivedibili, mantenendo la registrazione corrente dei collegamenti. I selettori ambigui falliscono invece di fare clic su un controllo arbitrario. Un normale albero di accessibilità del browser non è la promessa di un motore di automazione DOM completo.

Accettazione: automatizzare un'applicazione di test dedicata con selettori stabili attraverso i cambiamenti di posizione/dimensione; ambiguità dei test, elementi mancanti, controlli disabilitati, timeout e cancellazioni.

## 6. Pannello telefono/tablet locale
Servire un pannello reattivo da un servizio LAN con partecipazione. Associa in modo esplicito utilizzando un codice monouso di breve durata ed emetti credenziali revocabili protette localmente. Utilizzare HTTPS con attendibilità/blocco del certificato locale documentato; nessun fallback che invia credenziali in testo normale. Per impostazione predefinita, le interfacce configurate sono disabilitate e limitate. La convalida dell'origine, i comandi autenticati, i limiti di velocità e i messaggi vincolati sono obbligatori.

Esporre profili/azioni consentiti, stato live, avanzamento e annullamento; utilizzare ID stabili e la stessa coda di esecuzione delle chiavi desktop e fisiche. Non esporre comandi arbitrari, percorsi eseguibili, segreti o installazione di plug-in. Fornire schermate di connessione, associazione e revoca. Non è incluso alcun account cloud o relè Internet.

Accettazione: operare da un telefono/tablet reale sulla stessa LAN; i client non accoppiati non possono elencare o eseguire azioni; i token revocati falliscono; i comandi duplicati/riprodotti non eseguono una doppia esecuzione; disconnettersi non riprova silenziosamente le azioni.

## Ambito del produttore della tastiera

| Adattatore/famiglia | Integrazione pianificata | Confine esplicito |
| --- | --- | --- |
| Corsair | Conserva l'ingresso del tasto G iCUE corrente e l'integrazione RGB | Resta necessario il rilevamento della capacità del modello/software |
| Logitech G | Adattatore SDK G-key con controlli di stampa/rilascio e disponibilità runtime; illuminazione tramite il suo SDK separato, ove supportato | Non equiparare il rilevamento G HUB all'API G-key supportata su ogni modello |
| Razer | Adattatore per illuminazione cromatica e mappatura guidata dei tasti Synapse-to-standard, oltre a ingresso HID generico | Chroma è un'API di illuminazione; non etichettarlo come input macro-tasto proprietario nativo |
| SteelSeries | GameSense adattatore stato/illuminazione più ingresso HID generico | L'invio di eventi GameSense non è un'API di acquisizione di chiavi proprietaria generale |
| Wooting | Adattatore SDK analogico per ingresso analogico supportato, oltre a normali chiavi HID | Le funzionalità e i termini di distribuzione dell'SDK devono essere verificati in base al modello supportato |
| Keychron/QMK/VIA | Chiavi HID standard e istruzioni di mappatura del firmware; tracciati conosciuti solo con evidenza | Il supporto della configurazione VIA non è un'API macro-chiave di runtime universale |
| Altre tastiere HID standard | Individuazione e mappatura delle chiavi fisiche per dispositivo; modalità esclusiva solo con il backend del filtro convalidato | Nessun supporto per Fn, quadrante o tasti proprietari specifici del modello |

Ogni dispositivo mostra lo stato rilevato, osservato, mappato tramite software del fornitore, SDK nativo, driver esclusivo, non disponibile o hardware non testato, a seconda dei casi. Conserva prove di funzionalità con versioni dell'adattatore e identificatori di modello/firmware testati.

## Ordine di consegna
1. Compatibilità della configurazione, contratti di capacità ed esecuzione verificata.
2. Configurazione guidata utilizzando la scoperta reale e gli esecutori di azioni esistenti.
3. UI Automation e il relativo dispositivo di registrazione/test.
4. Host del plugin, flusso del pacchetto ed integrazione di esempio.
5. Pannello LAN accoppiato e verifica mobile.
6. Adattatori del fornitore ampliati e integrazione esclusiva del backend della tastiera, con convalida hardware effettiva.

Rimangono necessari tutti e sei. Firma/licenza del driver, SDK inaccessibili o hardware di test assente sono porte di rilascio, non ragioni per sostituire un comportamento fittizio e considerare il lavoro completato.

## Criteri di verifica e rilascio
Esegui controlli core esistenti, Windows e WPF e aggiungi test di integrazione mirati per ogni nuovo limite. Verificare i viaggi di andata e ritorno JSON e i profili legacy; testare il contenuto eseguibile importato, l'autenticazione di rete, l'annullamento, gli arresti anomali dei plug-in e la riconnessione del dispositivo. Ispeziona i layout desktop e mobili. Convalidare separatamente il ridimensionamento alto Windows. Costruisci gli artefatti di rilascio solo dopo aver superato i controlli; distinguere nelle note di rilascio le funzionalità implementate/testate, implementate/non testate dall'hardware e bloccate esternamente. Non sostituire l'applicazione installata, installare driver, alterare la mappatura reale della tastiera o aprire l'accesso alla rete come test incidentale.

## Prove
- Microsoft Raw Input semantica: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech SDK per sviluppatori G: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Domande frequenti sull'SDK Chroma: https://developer.razer.com/works-with-chroma/chroma-faq/
- Repository di intercettazione e condizioni di dipendenza: https://github.com/oblitum/Interception
- Inventario dei produttori di repository esistenti: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Rivedere le decisioni
All'utente viene chiesto di approvare questo progetto scritto prima del piano di implementazione. La principale dipendenza esterna è un backend del driver di input esclusivo pronto per la produzione. L'ambito consigliato lo mantiene come risultato finale delimitato anziché ridurlo silenziosamente all'osservazione Raw Input.

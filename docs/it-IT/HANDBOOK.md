<!-- generated-doc locale=it-IT source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../HANDBOOK.md)

# MAKRIVO — Manuale utente

Produttore: **Aivana**. Istantanea della documentazione: **2026-10-12**. Questo manuale descrive l'attuale implementazione dell'albero di lavoro. Le integrazioni sperimentali e le funzioni dipendenti dall'hardware sono identificate esplicitamente. I rapporti storici descrivono la loro istantanea datata, non una garanzia per una build più recente.

## Documentazione e lingue

Utilizzare il selettore della lingua della documentazione per aprire il manuale completo, l'inventario delle funzionalità, la risoluzione dei problemi, le note di sviluppo, i rapporti di ricerca e di test storici in una qualsiasi delle lingue dell'applicazione 27. Le traduzioni generate sono assistite da macchina e presentano controlli strutturali; non tutti hanno ricevuto recensioni da parte di madrelingua. Gli identificatori tecnici e gli esempi eseguibili rimangono invariati. La fonte originale rimane disponibile per il confronto. L'analisi della licenza tradotta è informativa e non sostituisce i termini legali originali di terze parti o un EULA finale.

## Installazione e primo avvio

La destinazione iniziale è Windows 11 x64. Mantieni insieme tutti i file e le sottodirectory della distribuzione. Avvia `MAKRIVO.exe`. Un checkout di origine non è un programma di installazione. I pacchetti autonomi includono il runtime .NET; I pacchetti dipendenti dal Framework richiedono i runtime .NET 10 Desktop e ASP.NET appropriati. ONNX e gli host associati devono essere inclusi per le rispettive funzionalità.

1. Avvia MAKRIVO e scegli la lingua dell'interfaccia in Impostazioni. Automatico segue Windows. Il layout della tastiera è un'impostazione separata.
2. Scegli un profilo, quindi aggiungi un riquadro tramite il catalogo delle azioni ricercabili. Dategli un nome riconoscibile e scegliete le sue azioni.
3. Rivedi i passaggi e assegna una scorciatoia se necessario. Evita combinazioni già utilizzate da Windows o dall'applicazione di destinazione.
4. Salva la bozza. Le modifiche non salvate non rappresentano la configurazione salvata.
5. Testare con un'azione innocua, come l'apertura di un documento, e controllare il feedback sull'esecuzione.

La chiusura della finestra principale normalmente lascia l'applicazione nell'area di notifica. Fare doppio clic sull'icona della barra delle applicazioni per riaprirla oppure utilizzare il menu della barra delle applicazioni per uscire. Iniziare con Windows è facoltativo e inizialmente disabilitato. Il controllo della pausa sospende le associazioni; l'icona mostra un badge di pausa. Annulla l'esecuzione delle macro tramite l'interfaccia utente, la barra delle applicazioni o il collegamento di annullamento configurato; il valore predefinito è `Ctrl+Alt+Escape`.

## Riquadri, azioni e tavolozza dei comandi

I riquadri appartengono ai profili e possono contenere più passaggi ordinati. L'editor supporta il salvataggio/eliminazione, l'anteprima, l'annullamento/ripristino e il riordino dei passaggi. Il catalogo fornisce preimpostazioni per programmi, documenti, URL, testo, appunti, scorciatoie, contenuti multimediali, audio, finestre, azioni di sistema e integrazioni supportate. Una preimpostazione che richiede parametri si apre come bozza.

Apri la tavolozza con `Ctrl+K` mentre MAKRIVO è attivo. Cerca azioni salvate, preimpostazioni, cambio di profilo e strumenti. I tasti freccia selezionano; L'invio attiva; `Ctrl+Enter` modifica un elemento supportato; La fuga si chiude. Alcuni comandi pronti all'uso vengono eseguiti immediatamente, mentre le azioni configurate richiedono una revisione. È possibile configurare una scorciatoia globale separata per la tavolozza; le nuove configurazioni utilizzano `Ctrl+Shift+Space`.

I passaggi di testo e appunti supportano `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. La formattazione della data è `yyyy-MM-dd`; la formattazione dell'ora è `HH:mm`. Le variabili sconosciute rimangono invariate. La lettura degli appunti avviene solo quando richiesto. Un'azione negli appunti scrive il testo senza incollarlo automaticamente.

Le azioni avviate dall'interfaccia utente possono avere come target l'ultima applicazione esterna. Se il bersaglio scompare o Windows blocca l'attivazione in primo piano, attiva manualmente il bersaglio e riprova. Il successo della consegna delle chiavi non dimostra che la destinazione abbia completato il suo compito.

## Profili e configurazione del posto di lavoro

I profili possono corrispondere al nome di un processo e a parte del titolo di una finestra. Quando entrambi sono impostati, entrambi devono corrispondere. L'ordine del profilo risolve le partite concorrenti; il profilo predefinito è il fallback. Abilita la selezione automatica in modo esplicito. Blocca profilo impedisce le modifiche automatiche per la sessione corrente.

Apri Configurazione Workplace da Strumenti o dalla tavolozza. Seleziona programmi, documenti, siti Web, una destinazione audio/visualizzata o una scena OBS. L'assistente prepara profili sul posto di lavoro, riunioni, streaming, ricerca o progetto. Ispeziona i passaggi generati prima di salvare. Il programma di installazione non esegue le azioni preparate.

## Macro, condizioni e registrazione

Le macro eseguono i passaggi in ordine e supportano attese, ripetizioni e cancellazioni. Il limite di ripetizione generale è 100. Utilizza l'anteprima prima di eseguire azioni che digitano in un'altra applicazione o modificano lo stato del sistema.

Wait for Target Window accetta un processo, una sottostringa del titolo e un timeout da 1 a 600,000 ms. È richiesto almeno un filtro. Una finestra visibile corrispondente deve rispondere e diventare in primo piano prima di continuare i passaggi successivi. Una finestra pronta non dimostra che un documento o un servizio ha terminato il caricamento.

L'editor del flusso di lavoro aggiunge condizioni di esistenza di file, esecuzione di processi e titolo di finestra. Una condizione falsa salta il passaggio e può saltare ulteriori passaggi successivi. Negare la condizione per esprimere un ramo alternativo. Selezionare deliberatamente Interrompi in caso di errore o Continua in caso di errore; continuando può lasciare i passaggi successivi senza le precondizioni previste.

Il registratore di azioni cattura accordi di scelta rapida e attende, non la normale digitazione. Supporta scorciatoie di modifica e tasti funzione. L'Escape interrompe la registrazione. Esamina l'elenco e adottalo come bozza di riquadro non salvata, quindi salva in modo esplicito. La registrazione dei clic dell'interfaccia utente è uno strumento separato descritto di seguito.

## Tastiere, gesti e livelli

La pagina della tastiera elenca le interfacce rilevate e i layout di riferimento. Una singola tastiera fisica può esporre diverse interfacce. Selezionare la lingua e la geometria corrette; il nome di un prodotto da solo non identifica il layout ISO o ANSI.

I tasti di scelta rapida globali standard si applicano a tutte le tastiere. I collegamenti Raw Input specifici del dispositivo osservano un particolare dispositivo e un codice di scansione, incluse le funzioni supportate e i tasti multimediali. **Raw Input non sopprime l'ingresso ordinario Windows. Una seconda tastiera esclusiva non è pronta per la produzione.** È ancora necessario un backend del driver del filtro firmato, concesso in licenza e testato; nessuno è installato da MAKRIVO.

Le associazioni di tasti speciali supportate possono utilizzare la pressione immediata, il tocco, la pressione o il doppio tocco. Le chiavi del livello bloccato selezionano temporaneamente un'altra mappatura e ripristinano il livello di base al rilascio; i tasti di selezione dei livelli non eseguono la propria macro. Le soglie di mantenimento vanno da 150 a 5,000 ms; toccare due volte le finestre da 150 a 1,000 ms. Queste mappature sono distinte dai normali tasti di scelta rapida globali.

La procedura guidata per la chiave speciale rileva le chiavi compatibili senza eseguire le azioni assegnate durante l'acquisizione. La diagnostica spiega i dispositivi non disponibili, le associazioni in pausa, le mancate corrispondenze del profilo e i conflitti. La sovrapposizione mostra il layout e le associazioni selezionati; la scorciatoia di attesa predefinita è `Ctrl+Alt+F1`.

| Integrazione | Requisiti e limitazioni |
| --- | --- |
| Corsair | Esecuzione di iCUE, accesso all'SDK e un modello compatibile. G1–G6 e funzioni RGB supportate; I controlli proprietari Fn, rotellina, blocco e profilo non sono generalmente esposti dall'API G-key. |
| Logitech G | Le librerie locali originali x64 G-key/LED SDK richiedono l'approvazione esplicita. Gli eventi dei tasti G non identificano in modo univoco diverse tastiere fisiche identiche. |
| Razer | Chroma RGB e ingresso standard; mappare i controlli proprietari in Synapse dove necessario. |
| SteelSeries | GameSense RGB e ingresso standard; mappare i controlli proprietari in GG dove necessario. |
| Wooting | SDK x64 locale originale compatibile per valori e soglie analogici; il normale ingresso HID rimane attivo. |
| Altre tastiere | Tasti di scelta rapida standard o collegamenti fisici del codice di scansione supportati. I controlli Fn e proprietari dipendono dal modello e dalla configurazione del fornitore. |

La ricerca del produttore descrive funzionalità documentate, compatibilità non implementata o testata con ogni dispositivo elencato. I nuovi adattatori necessitano ancora di una reale accettazione dell'hardware. Non scaricare i file binari dell'SDK da fonti non attendibili; I diritti di distribuzione dell'SDK sono separati dalla compatibilità tecnica.

## Audio, display e OBS

Il volume dei passaggi audio 0 per il 100 percentuale e mute/unmute/toggle supportato output o microfon endpoints. Scegli un punto di fine specifico o utilizzare il Windows default. un target salvato mancante produce un errore invece di controllare silenziosamente un altro dispositivo. Rifresca i dispositivi dopo la riconnessione. il controllo del punto finale non è una richiesta di supporto indipendente per applicazione.

Le azioni del display aumentano/diminuiscono la luminosità dei punti percentuali 10 o impostano un valore da 0 a 100. I display interni utilizzano le interfacce WMI supportate; i monitor esterni richiedono il supporto DDC/CI e in genere l'impostazione del menu monitor. Dock, adattatori e cavi possono bloccare il controllo. Scegli tutti i display supportati o un monitor. La riconnessione può modificare le identità disponibili; rivedere il target salvato. Le letture mancanti vengono visualizzate come non disponibili anziché come valori misurati.

OBS richiede WebSocket v5 abilitato in OBS Strumenti. Configura l'endpoint, testa la connessione e carica i nomi di scene/input prima di salvare. Le azioni supportate includono il cambio di scena, l'avvio/arresto/attivazione/disattivazione dello streaming/registrazione, la pausa/ripresa della registrazione e la disattivazione dell'ingresso. Le password possono essere archiviate in Windows Credential Manager e sono escluse dalle esportazioni di configurazione. Un comando OBS riuscito non stabilisce tutti gli effetti di streaming downstream.

## Verifica dei risultati e cronologia dell'esecuzione

Abilita Verifica risultato per i passaggi adatti. L'app distingue i controlli preflight, gli effetti confermati, i guasti, l'annullamento e gli effetti non verificabili. L'esistenza del processo non è la disponibilità dell'applicazione. Le modifiche relative, i collegamenti e l'immissione di testo non possono ricevere una richiesta di successo generale. Esamina il registro delle attività e la cronologia delle verifiche quando un'azione fallisce. Gli errori di configurazione devono essere corretti prima di eseguire nuovamente la macro completa.

## Automazione dell'interfaccia utente e registrazione dei clic

Aggiungi un passo di automatizzazione dell'UI, selezionare l'operazione e catturare l'obiettivo sotto l'indicatore dopo il ritardo visualizzato. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` e `WaitState`. I campi di password, i controlli disabilitati e i selettori ambiguo vengono respinti.

L'automazione dell'interfaccia utente viene eseguita in un processo associato limitato. Dipende dal supporto e dalle autorizzazioni per l'accessibilità Windows dell'applicazione di destinazione. Il registratore di clic cattura le azioni e le pause supportate; non registra l'input di testo generale. Rivedere e convalidare la bozza prima di adottarla. Le modifiche all'interfaccia utente in un'app esterna possono invalidare i selettori. L'isolamento del processo limita gli arresti anomali e l'annullamento, ma non è una sandbox di sicurezza.

## Plugin

Apri Estensioni per ispezionare un pacchetto prima di importarlo. Vengono controllati la struttura del pacchetto, il manifest e gli hash. L'importazione inizia disabilitata. Leggi le azioni dichiarate e fidati esplicitamente dell'editore prima di abilitarlo. I plugin vengono eseguiti con le tue autorizzazioni utente in un processo separato; quel processo è **non un sandbox**. Installa solo pacchetti di cui ti fidi.

Il gestore supporta l'esportazione, la disattivazione, la disinstallazione e il ripristino/eliminazione di una versione precedente. Le dipendenze mancanti bloccano l'invocazione. Il pacchetto di esempio fornisce azioni per il conto alla rovescia e per lo stato locale. Lo schema supportato è un sottoinsieme documentato dello schema JSON, non di tutte le possibili funzionalità dello schema JSON. Non esiste un mercato pubblico dei plugin.

## Pannello mobile

1. Apri Mobile Panel, seleziona un IP locale e scegli esplicitamente i profili da esporre. L'indirizzo di loopback predefinito è raggiungibile solo sul PC stesso; selezionare l'indirizzo LAN appropriato per un telefono o tablet.
2. Avvia il pannello e leggi il suo indirizzo HTTPS, l'impronta digitale del certificato e il codice di accoppiamento a sei cifre monouso.
3. Su un dispositivo nella stessa rete attendibile, apri l'indirizzo. Confronta l'impronta digitale SHA-256 attraverso un canale affidabile prima di fidarsi del certificato locale. L'app non installa automaticamente un certificato attendibile.
4. Accoppia utilizzando il codice visualizzato e prova una tessera innocua. Potrebbe essere necessaria un'autorizzazione manuale Windows Firewall per l'accesso alla LAN. Non esporre la porta a Internet.
5. Arresta il pannello al termine o revoca i client associati tramite l'app.

Le chiavi del server privato sono protette con Windows DPAPI; le credenziali del client vengono archiviate come hash. La modifica dell'IP o il rinnovo del certificato può richiedere una rinnovata fiducia. Il pannello mostra gli stati di azione e verifica, non tutti i possibili sensori attivi. Il layout del browser e i controlli HTTPS locali non sostituiscono la reale accettazione della LAN del telefono/tablet.

## Assistente alla riunione AI locale

Questo assistente sperimentale opzionale è disabilitato per impostazione predefinita. Aprilo da Strumenti, abilitalo esplicitamente, seleziona un runtime/modello e aggiungi solo le risorse che vuoi che prenda in considerazione. Indica l'obiettivo, crea una bozza, esamina le richieste irrisolte e trasferisci il risultato al normale editor. Niente viene eseguito automaticamente; il salvataggio e l'esecuzione rimangono azioni esplicite. Non esiste un fallback automatico in fase di esecuzione.

| Durata | Installazione |
| --- | --- |
| Ollama | Servizio locale separato e modello installato localmente all'indirizzo `127.0.0.1:11434`. I modelli remoti/cloud dichiarati vengono rifiutati. |
| LM Studio | Server locale con un modello caricato localmente e supporto dello schema JSON su `127.0.0.1:1234`. Non abilitare il routing remoto per l'elaborazione locale. |
| llama.cpp | `llama-server` locale e modello compatibile con supporto dello schema JSON su `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Cartella modello di testo locale GenAI compatibile affidabile; selezionare `genai_config.json`. L'inferenza utilizza un processo associato in bundle senza HTTP. |

I client HTTP utilizzano solo il loopback, non utilizzano proxy e non seguono reindirizzamenti. MAKRIVO non è in grado di stabilire in modo affidabile se un server locale compatibile chiama internamente un servizio remoto. I server locali autenticati non sono configurabili in questa versione. I pesi dei modelli, Python e i server di inferenza locale non vengono scaricati o installati automaticamente.

ONNX Il pacchetto è necessario `tokenizer.json` e un modello di chat valido. moduli di modello supportati in `tokenizer_config.json` è una string `chat_template`Un dizionario con una stringa `default`, o un elenco di modello indimenticabile/default. se il modello è mancato o nullo, `chat_template.jinja` I modelli vuoti, ambiguo, eccessivo o sbagliato sono rifiutati. ONNX I file, i modelli di ingresso/visione e i pacchetti GPU-solo non sono intercambiabili con compatibili GenAI Modello di testo: il bundle GenAI La versione runtime è 0.17.1Utilizzare solo modelli affidati; la separazione del processo non è una scatola di sabbia.

L'assistente supporta programmi selezionati senza argomenti, documenti e siti Web selezionati e disattivazione del microfono quando espressamente richiesto. Non crea messaggi, appuntamenti, script arbitrari, clic gratuiti, download o cambio di auricolare. I nomi dell'obiettivo e delle risorse vengono inviati al runtime locale selezionato; i percorsi delle risorse e il contenuto dei file non vengono forniti separatamente. Un obiettivo digitato dall'utente può esso stesso contenere dati personali. Il comportamento di rete del runtime locale rimane responsabilità dell'utente.

Il budget della richiesta ONNX è di 90 secondi incluso il caricamento del modello, fino ai token di richiesta 3,072 e ai token generati 1,024, anch'essi limitati dal contesto del modello. JSON non valido/troncato e ID risorsa sconosciuti vengono rifiutati. Annulla o chiudi per terminare una richiesta. Un test di runtime con modello casuale stabilisce l'esecuzione, non la qualità della pianificazione utile o il risparmio di tempo. I log degli esperimenti locali contengono identificatori e tempistiche degli eventi, non testo dell'obiettivo o contenuti del documento; non vengono caricati automaticamente.

La piccola valutazione del modello di Instruct ha prodotto i piani nulli validi in quattro test tedesco/inglese. la conclusione nativa ha funzionato, ma il modello ha fallito il contratto di pianificazione rigoroso. Questo è un risultato di qualità negativo, non un modello raccomandato o un assistente utile. Vedi il rapporto di prova locale data per la revisione esatta del modello e i limiti.

## Importazione, esportazione, sincronizzazione e backup

Esamina le anteprime di importazione prima di sostituire una configurazione. Le azioni importate vengono avviate disabilitate e la sincronizzazione portatile rimuove i collegamenti specifici dell'hardware. Riassegnare i dispositivi e abilitare deliberatamente le azioni dopo l'ispezione. Esaminare i contenuti di testo/azione prima di condividere un'esportazione; l'esclusione delle password non rende non sensibili tutti i contenuti delle azioni create dall'utente.

La sincronizzazione del profilo scambia i file in una cartella selezionata, opzionale gestita dal tuo cliente cloud esistente. MAKRIVO non fornisce un account cloud e non memorizza credenziali cloud. Revisioni impediscono sovrapporsi silenziosi. Includere contenuti di azione è una scelta esplicita. Backup la tua configurazione prima di un'importazione di sostituzione. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`Il nome storico del directory viene conservato per la compatibilità. chiudere l'app prima di copiarlo manualmente. le approvazioni SDK memorizzate e le credenziali OS protette potrebbero essere ripristinate su un altro PC.

## Risoluzione dei problemi

| Sintomo | Cosa controllare |
| --- | --- |
| La scorciatoia non fa nulla | App in esecuzione, associazioni non sospese, profilo attivo corretto, riquadro salvato/abilitato, conflitti di scorciatoie e destinazione disponibile. |
| Impossibile attivare la finestra di destinazione | Attivalo manualmente. Windows restrizioni in primo piano e differenze di privilegi possono bloccare l'automazione. |
| La chiave speciale non viene rilevata | Servizio del fornitore/SDK/supporto del modello; configurare i controlli proprietari per emettere una chiave standard supportata, se possibile. |
| Anche la seconda tastiera digita normalmente | Previsto con Raw Input. La soppressione esclusiva non è implementata per la produzione. |
| Audio o luminosità non disponibili | Aggiorna i dispositivi; ispezionare l'identità selezionata, le sessioni dell'endpoint, il supporto WMI/DDC/CI e le restrizioni su dock/cavo. |
| OBS offline | WebSocket v5 abilitato, endpoint/password corretti, servizio raggiungibile e nomi di scene/input validi. |
| L'automazione dell'interfaccia utente non riesce | Host complementare incluso, selettore valido univoco, destinazione abilitata accessibile, controllo e autorizzazioni supportati. Riprovare un timeout temporaneo solo dopo aver esaminato la destinazione. |
| Pannello mobile irraggiungibile | Indirizzo LAN anziché di loopback, stessa rete, server in esecuzione, profili approvati, impronta digitale/attendibilità del certificato e autorizzazioni firewall manuali. |
| Il plugin fallisce | Integrità del pacchetto, stato di trust/abilitazione esplicito, dipendenze, compatibilità del protocollo e registro di esecuzione. |
| La richiesta AI fallisce | Correggere il runtime locale selezionato, il servizio/modello pronto, lo schema JSON supportato, la cartella del modello compatibile attendibile e i limiti delle richieste. |
| Il layout o il testo sembrano errati | Lingua dell'interfaccia utente separata dalla geometria della tastiera; ridimensiona la finestra, controlla la scala di visualizzazione e segnala l'esatta pagina/lingua/build. |

Per un'utile segnalazione di bug, includere la versione Windows, la build/commit dell'app, la lingua, il ridimensionamento della visualizzazione, il modello hardware, una sequenza minima e il relativo registro redatto. Rimuovi percorsi personali, contenuti di azioni e credenziali prima della pubblicazione.

## Costruzione, test e confezionamento

Utilizza l'SDK aggiunto in `global.json`. Dalla radice del repository:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

I test di integrazione Windows possono aprire finestre isolate e configurare/rilasciare temporaneamente le associazioni dei fornitori supportati. Eseguili senza che un altro controller possieda tali associazioni. Le sonde di luminosità/audio predefinite sono di sola lettura; i test espliciti di andata e ritorno dell'hardware sono separati. Protocolli fittizi, screenshot renderizzati e grafici di inferenza casuale non devono essere rappresentati come accettazione reale della qualità dell'hardware/modello.

L'editore completo assembla host associati, risorse mobili, plug-in di esempio, runtime ONNX e avvisi sulle dipendenze disponibili. Conserva le sue sottodirectory. Una build riuscita non è un programma di installazione firmato o un'approvazione della versione legale. Sorgente e build possono evolversi separatamente; confrontare l'istantanea della documentazione ed effettuare il commit con la versione installata.

## Licenza, privacy e stato di rilascio

Il modello commerciale previsto è un acquisto una tantum con uso perpetuo della versione principale acquistata; il prezzo finale, la franchigia del dispositivo e i termini di aggiornamento devono essere impostati nei termini di vendita effettivi. Nel presente manuale non è previsto un sistema di acquisto/attivazione della produzione. Non è richiesto un account cloud per il controllo quotidiano.

Non distribuire file binari dell'SDK proprietario senza un'autorizzazione comprovata. Conserva le licenze e gli avvisi di terze parti per ogni dipendenza spedita, inclusi i pacchetti .NET e ONNX. I pesi dei modelli hanno i propri termini. La verifica della licenza datata precede alcune nuove dipendenze e non costituisce un'autorizzazione per tali aggiunte. Essendo pubblico, il repository non garantisce di per sé una licenza open source generale.

L'hardware del fornitore reale, il funzionamento LAN di telefono/tablet, le transizioni tra monitor misti/DPI, l'uso prolungato, la revisione linguistica completa, il supporto esclusivo dei driver e l'utile qualità di pianificazione dell'intelligenza artificiale rimangono compiti di accettazione separati. Non dedurre mai l'approvazione solo dalla presenza di funzionalità.

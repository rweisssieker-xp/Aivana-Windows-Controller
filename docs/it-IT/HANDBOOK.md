<!-- generated-doc locale=it-IT source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../HANDBOOK.md)

# MAKRIVO — Manuale utente

Produttore: **Aivana**. Istantanea della documentazione: **2026-10-12**. Questo manuale descrive l'attuale implementazione dell'albero di lavoro. Le integrazioni sperimentali e le funzioni dipendenti dall'hardware sono identificate esplicitamente. I rapporti storici descrivono la loro istantanea datata, non una garanzia per una build più recente.

## Documentazione e lingue

Usa il selettore della lingua della documentazione per aprire il manuale completo, l'inventario delle funzionalità, la risoluzione dei problemi, le note di sviluppo, la ricerca e i rapporti di test storici in una delle 27 lingue dell'applicazione. Le traduzioni generate sono assistite da macchina e sottoposte a controlli strutturali; non tutte sono state revisionate da madrelingua. Gli identificatori tecnici e gli esempi eseguibili rimangono invariati. La fonte originale resta disponibile per confronto. L'analisi delle licenze tradotta è informativa e non sostituisce i termini legali originali di terze parti né un EULA finale.

## Installazione e primo avvio

La piattaforma iniziale è Windows 11 x64. Mantieni insieme tutti i file e le sottodirectory della distribuzione. Avvia `MAKRIVO.exe`. Una copia del repository sorgente non è un programma di installazione. I pacchetti autonomi includono il runtime .NET; quelli dipendenti dal framework richiedono i runtime .NET 10 Desktop e ASP.NET appropriati. ONNX e gli host ausiliari devono essere inclusi per le rispettive funzionalità.

1. Avvia MAKRIVO e scegli la lingua dell'interfaccia in Impostazioni. Automatico segue Windows. Il layout della tastiera è un'impostazione separata.
2. Scegli un profilo, poi aggiungi un riquadro tramite il catalogo delle azioni ricercabile. Assegna un nome riconoscibile e scegli le azioni.
3. Rivedi i passaggi e assegna una scorciatoia se necessario. Evita combinazioni già utilizzate da Windows o dall'applicazione di destinazione.
4. Salva la bozza. Le modifiche non salvate non rappresentano la configurazione salvata.
5. Testare con un'azione innocua, come l'apertura di un documento, e controllare il feedback sull'esecuzione.

Chiudere la finestra principale normalmente lascia l'applicazione nell'area di notifica. Fai doppio clic sulla relativa icona per riaprirla oppure usa il menu dell'icona per uscire. L'avvio con Windows è facoltativo e inizialmente disabilitato. Il comando di pausa sospende le associazioni; l'icona mostra un indicatore di pausa. Annulla le macro in esecuzione tramite l'interfaccia, l'area di notifica o la scorciatoia di annullamento configurata; quella predefinita è `Ctrl+Alt+Escape`.

## Riquadri, azioni e tavolozza dei comandi

I riquadri appartengono ai profili e possono contenere più passaggi ordinati. L'editor consente di salvare o scartare le modifiche, visualizzare l'anteprima, annullare/ripristinare e riordinare i passaggi. Il catalogo fornisce preimpostazioni per programmi, documenti, URL, testo, appunti, scorciatoie, contenuti multimediali, audio, finestre, azioni di sistema e integrazioni supportate. Una preimpostazione che richiede parametri si apre come bozza.

Apri la tavolozza con `Ctrl+K` mentre MAKRIVO ha il focus. Cerca azioni salvate, preimpostazioni, cambio di profilo e strumenti. I tasti freccia selezionano; Invio attiva; `Ctrl+Enter` modifica un elemento supportato; Escape chiude. Alcuni comandi pronti all'uso vengono eseguiti immediatamente, mentre le azioni configurate richiedono una revisione. È possibile configurare una scorciatoia globale separata per la tavolozza; le nuove configurazioni usano `Ctrl+Shift+Space`.

I passaggi di testo e appunti supportano `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. La formattazione della data è `yyyy-MM-dd`; la formattazione dell'ora è `HH:mm`. Le variabili sconosciute rimangono invariate. La lettura degli appunti avviene solo quando richiesto. Un'azione negli appunti scrive il testo senza incollarlo automaticamente.

Le azioni avviate dall'interfaccia possono rivolgersi all'ultima applicazione esterna. Se la destinazione scompare o Windows ne blocca l'attivazione in primo piano, attivala manualmente e riprova. La consegna riuscita delle pressioni dei tasti non dimostra che l'applicazione abbia completato il suo compito.

## Profili e configurazione del posto di lavoro

I profili possono corrispondere a un nome di processo e a una parte del titolo di una finestra. Se sono impostati entrambi, devono corrispondere entrambi. L'ordine dei profili risolve le corrispondenze concorrenti; il profilo predefinito è quello di riserva. Abilita esplicitamente la selezione automatica. Blocca profilo impedisce i cambiamenti automatici nella sessione corrente.

Apri la configurazione dell'ambiente di lavoro da Strumenti o dalla tavolozza. Seleziona programmi, documenti, siti web, una destinazione audio/display o una scena OBS. L'assistente prepara profili di lavoro, riunione, streaming, ricerca o progetto. Controlla i passaggi generati prima di salvare. La configurazione non esegue le azioni preparate.

## Macro, condizioni e registrazione

Le macro eseguono i passaggi in ordine e supportano attese, ripetizioni e cancellazioni. Il limite di ripetizione generale è 100. Utilizza l'anteprima prima di eseguire azioni che digitano in un'altra applicazione o modificano lo stato del sistema.

L'azione Attendi finestra di destinazione accetta un processo, una sottostringa del titolo e un timeout da 1 a 600000 ms. È richiesto almeno un filtro. Una finestra visibile corrispondente deve rispondere e passare in primo piano prima che i passaggi successivi proseguano. Una finestra pronta non dimostra che un documento o servizio abbia terminato il caricamento.

L'editor del flusso di lavoro aggiunge condizioni di esistenza di file, processo in esecuzione e titolo della finestra. Una condizione falsa salta il passaggio e può saltare anche altri passaggi successivi. Nega la condizione per esprimere un ramo alternativo. Scegli deliberatamente di interrompere o continuare in caso di errore; continuare può lasciare i passaggi successivi senza le precondizioni previste.

Il registratore di azioni acquisisce combinazioni di tasti di scelta rapida e attese, non la normale digitazione. Supporta scorciatoie con tasti modificatori e tasti funzione. Escape interrompe la registrazione. Esamina l'elenco e adottalo come bozza di riquadro non salvata, poi salva esplicitamente. La registrazione dei clic nell'interfaccia utente è uno strumento separato descritto di seguito.

## Tastiere, gesti e livelli

La pagina della tastiera elenca le interfacce rilevate e i layout di riferimento. Una singola tastiera fisica può esporre diverse interfacce. Selezionare la lingua e la geometria corrette; il nome di un prodotto da solo non identifica il layout ISO o ANSI.

I normali tasti di scelta rapida globali si applicano a tutte le tastiere. Le associazioni Raw Input specifiche del dispositivo osservano un determinato dispositivo e codice di scansione, inclusi i tasti funzione e multimediali supportati. **Raw Input non sopprime il normale input di Windows. L'uso esclusivo di una seconda tastiera non è pronto per la produzione.** È ancora necessario un backend con driver filtro firmato, dotato di licenza e testato; MAKRIVO non ne installa alcuno.

Le associazioni supportate dei tasti speciali possono usare pressione immediata, tocco, pressione prolungata o doppio tocco. I tasti di livello tenuti premuti selezionano temporaneamente un'altra mappatura e ripristinano il livello di base al rilascio; i tasti di selezione del livello non eseguono una propria macro. Le soglie di pressione prolungata vanno da 150 a 5000 ms; gli intervalli per il doppio tocco vanno da 150 a 1000 ms. Queste mappature sono distinte dai normali tasti di scelta rapida globali.

La procedura guidata per i tasti speciali rileva i tasti compatibili senza eseguire le azioni assegnate durante l'acquisizione. La diagnostica spiega dispositivi non disponibili, associazioni in pausa, profili non corrispondenti e conflitti. La sovrapposizione mostra il layout e le associazioni selezionati; la scorciatoia predefinita da tenere premuta è `Ctrl+Alt+F1`.

| Integrazione | Requisiti e limitazioni |
| --- | --- |
| Corsair | Esecuzione di iCUE, accesso all'SDK e un modello compatibile. G1–G6 e funzioni RGB supportate; I controlli proprietari Fn, rotellina, blocco e profilo non sono generalmente esposti dall'API G-key. |
| Logitech G | Le librerie locali originali x64 G-key/LED SDK richiedono l'approvazione esplicita. Gli eventi dei tasti G non identificano in modo univoco diverse tastiere fisiche identiche. |
| Razer | Chroma RGB e ingresso standard; mappare i controlli proprietari in Synapse dove necessario. |
| SteelSeries | GameSense RGB e ingresso standard; mappare i controlli proprietari in GG dove necessario. |
| Wooting | SDK x64 locale originale compatibile per valori e soglie analogici; il normale ingresso HID rimane attivo. |
| Altre tastiere | Tasti di scelta rapida standard o collegamenti fisici del codice di scansione supportati. I controlli Fn e proprietari dipendono dal modello e dalla configurazione del fornitore. |

La ricerca sui produttori descrive capacità documentate, non una compatibilità implementata o testata con ogni dispositivo elencato. I nuovi adattatori richiedono ancora una convalida su hardware reale. Non scaricare binari SDK da fonti non attendibili; i diritti di distribuzione degli SDK sono distinti dalla compatibilità tecnica.

## Audio, display e OBS

I passaggi audio impostano il volume da 0 a 100 percento e attivano, disattivano o invertono lo stato di disattivazione dell'audio delle uscite o dei microfoni supportati. Scegli un endpoint specifico oppure quello predefinito di Windows. Una destinazione salvata non disponibile genera un errore anziché controllare un altro dispositivo senza avvisare. Aggiorna i dispositivi dopo la riconnessione. Il controllo degli endpoint non implica il supporto di un mixer indipendente per ogni applicazione.

Le azioni del display aumentano/diminuiscono la luminosità dei punti percentuali 10 o impostano un valore da 0 a 100. I display interni utilizzano le interfacce WMI supportate; i monitor esterni richiedono il supporto DDC/CI e in genere l'impostazione del menu monitor. Dock, adattatori e cavi possono bloccare il controllo. Scegli tutti i display supportati o un monitor. La riconnessione può modificare le identità disponibili; rivedere il target salvato. Le letture mancanti vengono visualizzate come non disponibili anziché come valori misurati.

OBS richiede WebSocket v5 abilitato in OBS Strumenti. Configura l'endpoint, testa la connessione e carica i nomi di scene/input prima di salvare. Le azioni supportate includono il cambio di scena, l'avvio/arresto/attivazione/disattivazione dello streaming/registrazione, la pausa/ripresa della registrazione e la disattivazione dell'ingresso. Le password possono essere archiviate in Windows Credential Manager e sono escluse dalle esportazioni di configurazione. Un comando OBS riuscito non stabilisce tutti gli effetti di streaming downstream.

## Verifica dei risultati e cronologia dell'esecuzione

Abilita Verifica risultato per i passaggi appropriati. L'app distingue controlli preliminari, effetti confermati, errori, annullamenti ed effetti non verificabili. L'esistenza di un processo non significa che l'applicazione sia pronta. Modifiche relative, scorciatoie e immissione di testo non possono essere dichiarate sempre riuscite. Esamina il registro delle attività e la cronologia delle verifiche quando un'azione fallisce. Correggi gli errori di configurazione prima di rieseguire la macro completa.

## Automazione dell'interfaccia utente e registrazione dei clic

Aggiungi un passaggio di automazione dell'interfaccia utente, seleziona l'operazione e acquisisci la destinazione sotto il puntatore dopo il ritardo visualizzato. La sola acquisizione non esegue nulla. Controlla il selettore e richiedi una corrispondenza univoca. Le operazioni supportate includono `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` e `WaitState`. I campi password, i controlli disabilitati e i selettori ambigui vengono rifiutati.

L'automazione dell'interfaccia viene eseguita in un processo ausiliario con limiti definiti. Dipende dal supporto per l'accessibilità Windows e dalle autorizzazioni dell'applicazione di destinazione. Il registratore di clic acquisisce le azioni supportate e le pause; non registra l'immissione generale di testo. Esamina e convalida la bozza prima di adottarla. Le modifiche all'interfaccia di un'app esterna possono invalidare i selettori. L'isolamento del processo limita le conseguenze degli arresti anomali e delimita l'annullamento, ma non è una sandbox di sicurezza.

## Plugin

Apri Estensioni per esaminare un pacchetto prima di importarlo. Vengono verificati la struttura del pacchetto, il manifest e gli hash. Un pacchetto importato è inizialmente disabilitato. Leggi le azioni dichiarate e accorda esplicitamente fiducia all'editore prima di abilitarlo. I plugin vengono eseguiti con le tue autorizzazioni utente in un processo separato; tale processo **non è una sandbox**. Installa solo pacchetti attendibili.

Il gestore supporta l'esportazione, la disattivazione, la disinstallazione e il ripristino/eliminazione di una versione precedente. Le dipendenze mancanti bloccano l'invocazione. Il pacchetto di esempio fornisce azioni per il conto alla rovescia e per lo stato locale. Lo schema supportato è un sottoinsieme documentato dello schema JSON, non di tutte le possibili funzionalità dello schema JSON. Non esiste un mercato pubblico dei plugin.

## Pannello mobile

1. Apri Mobile Panel, seleziona un IP locale e scegli esplicitamente i profili da esporre. L'indirizzo di loopback predefinito è raggiungibile solo sul PC stesso; selezionare l'indirizzo LAN appropriato per un telefono o tablet.
2. Avvia il pannello e leggi il suo indirizzo HTTPS, l'impronta digitale del certificato e il codice di accoppiamento a sei cifre monouso.
3. Su un dispositivo nella stessa rete attendibile, apri l'indirizzo. Confronta l'impronta digitale SHA-256 attraverso un canale affidabile prima di fidarsi del certificato locale. L'app non installa automaticamente un certificato attendibile.
4. Accoppia utilizzando il codice visualizzato e prova una tessera innocua. Potrebbe essere necessaria un'autorizzazione manuale Windows Firewall per l'accesso alla LAN. Non esporre la porta a Internet.
5. Arresta il pannello al termine o revoca i client associati tramite l'app.

Le chiavi private del server sono protette con Windows DPAPI; le credenziali dei client vengono memorizzate come hash. Cambiare l'IP o rinnovare il certificato può richiedere una nuova conferma di attendibilità. Il pannello mostra gli stati delle azioni e delle verifiche, non ogni possibile sensore in tempo reale. Le verifiche del layout del browser e di HTTPS locale non sostituiscono la convalida reale su telefono/tablet in rete LAN.

## Assistente alla riunione AI locale

Questo assistente sperimentale facoltativo è disabilitato per impostazione predefinita. Aprilo da Strumenti, abilitalo esplicitamente, seleziona un runtime/modello e aggiungi solo le risorse da considerare. Indica l'obiettivo, crea una bozza, esamina le richieste irrisolte e trasferisci il risultato all'editor normale. Nulla viene eseguito automaticamente; salvare ed eseguire rimangono azioni esplicite. Non è previsto il passaggio automatico a un altro runtime.

| Ambiente di esecuzione | Installazione |
| --- | --- |
| Ollama | Servizio locale separato e modello installato localmente all'indirizzo `127.0.0.1:11434`. I modelli remoti/cloud dichiarati vengono rifiutati. |
| LM Studio | Server locale con un modello caricato localmente e supporto dello schema JSON su `127.0.0.1:1234`. Non abilitare il routing remoto per l'elaborazione locale. |
| llama.cpp | `llama-server` locale e modello compatibile con supporto dello schema JSON su `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Cartella modello di testo locale GenAI compatibile affidabile; selezionare `genai_config.json`. L'inferenza utilizza un processo associato in bundle senza HTTP. |

I client HTTP utilizzano solo il loopback, non utilizzano proxy e non seguono reindirizzamenti. MAKRIVO non è in grado di stabilire in modo affidabile se un server locale compatibile chiama internamente un servizio remoto. I server locali autenticati non sono configurabili in questa versione. I pesi dei modelli, Python e i server di inferenza locale non vengono scaricati o installati automaticamente.

I pacchetti ONNX richiedono `tokenizer.json` e un template di chat valido. Le forme supportate in `tokenizer_config.json` sono una stringa `chat_template`, un dizionario con una stringa `default` oppure un elenco non ambiguo di template denominati/predefiniti. Se il template è assente o nullo, viene usato `chat_template.jinja` come alternativa. I template vuoti, ambigui, troppo grandi o di tipo errato vengono rifiutati. File ONNX arbitrari, modelli di embedding/visione e pacchetti solo GPU non sono intercambiabili con modelli di testo GenAI compatibili. La versione del runtime GenAI incluso è 0.17.1, CPU. Usa solo modelli attendibili; la separazione dei processi non è una sandbox.

L'assistente supporta i programmi selezionati senza argomenti, i documenti e i siti web selezionati e la disattivazione del microfono quando espressamente richiesta. Non crea messaggi, appuntamenti, script arbitrari, clic liberi, download o cambi di cuffie. L'obiettivo e i nomi delle risorse vengono inviati al runtime locale selezionato; i percorsi delle risorse e il contenuto dei file non vengono forniti separatamente. Un obiettivo scritto dall'utente può contenere dati personali. Il comportamento di rete proprio del runtime locale rimane responsabilità dell'utente.

Il budget di una richiesta ONNX è di 90 secondi, incluso il caricamento del modello, con un massimo di 3072 token di prompt e 1024 token generati, ulteriormente limitati dal contesto del modello. JSON malformato/troncato e ID di risorse sconosciuti vengono rifiutati. Annulla o chiudi per terminare una richiesta. Un test del runtime con un modello casuale dimostra l'esecuzione, non una qualità di pianificazione utile o un risparmio di tempo. I log degli esperimenti locali contengono identificatori di eventi e durate, non il testo degli obiettivi né il contenuto dei documenti; non vengono caricati automaticamente.

La successiva valutazione del piccolo modello Instruct non ha prodotto alcun piano valido in quattro prove in tedesco/inglese. L'inferenza nativa ha funzionato, ma il modello non ha rispettato il rigoroso contratto di pianificazione. È un risultato negativo sulla qualità, non un modello consigliato né un assistente di comprovata utilità. Consulta il rapporto datato sui test dei runtime locali per la revisione esatta del modello e i limiti.

## Importazione, esportazione, sincronizzazione e backup

Esamina le anteprime di importazione prima di sostituire una configurazione. Le azioni importate vengono avviate disabilitate e la sincronizzazione portatile rimuove i collegamenti specifici dell'hardware. Riassegnare i dispositivi e abilitare deliberatamente le azioni dopo l'ispezione. Esaminare i contenuti di testo/azione prima di condividere un'esportazione; l'esclusione delle password non rende non sensibili tutti i contenuti delle azioni create dall'utente.

La sincronizzazione dei profili scambia file in una cartella selezionata, eventualmente gestita dal client cloud già in uso. MAKRIVO non fornisce account cloud e non memorizza credenziali cloud. Le revisioni impediscono sovrascritture silenziose. Includere i contenuti delle azioni è una scelta esplicita. Esegui un backup della configurazione prima di un'importazione sostitutiva. La configurazione predefinita è `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; il nome storico della directory è mantenuto per compatibilità. Chiudi l'app prima di copiarla o ripristinarla manualmente. Le approvazioni SDK memorizzate e le credenziali protette dal sistema operativo potrebbero dover essere ricreate su un altro PC.

## Risoluzione dei problemi

| Sintomo | Cosa controllare |
| --- | --- |
| La scorciatoia non fa nulla | App in esecuzione, associazioni non in pausa, profilo attivo corretto, riquadro salvato/abilitato, conflitti di scorciatoie e destinazione disponibile. |
| Impossibile attivare la finestra di destinazione | Attivalo manualmente. Windows restrizioni in primo piano e differenze di privilegi possono bloccare l'automazione. |
| Il tasto speciale non viene rilevato | Servizio del produttore/SDK/supporto del modello; se possibile, configura i comandi proprietari affinché emettano un tasto standard supportato. |
| Anche la seconda tastiera digita normalmente | Previsto con Raw Input. La soppressione esclusiva non è implementata per la produzione. |
| Audio o luminosità non disponibili | Aggiorna i dispositivi; ispezionare l'identità selezionata, le sessioni dell'endpoint, il supporto WMI/DDC/CI e le restrizioni su dock/cavo. |
| OBS offline | WebSocket v5 abilitato, endpoint/password corretti, servizio raggiungibile e nomi di scene/input validi. |
| L'automazione dell'interfaccia utente non riesce | Host complementare incluso, selettore valido univoco, destinazione abilitata accessibile, controllo e autorizzazioni supportati. Riprovare un timeout temporaneo solo dopo aver esaminato la destinazione. |
| Pannello mobile irraggiungibile | Indirizzo LAN anziché di loopback, stessa rete, server in esecuzione, profili approvati, impronta digitale/attendibilità del certificato e autorizzazioni firewall manuali. |
| Il plugin fallisce | Integrità del pacchetto, stato di trust/abilitazione esplicito, dipendenze, compatibilità del protocollo e registro di esecuzione. |
| La richiesta AI fallisce | Correggere il runtime locale selezionato, il servizio/modello pronto, lo schema JSON supportato, la cartella del modello compatibile attendibile e i limiti delle richieste. |
| Il layout o il testo sembrano errati | Lingua dell'interfaccia utente separata dalla geometria della tastiera; ridimensiona la finestra, controlla la scala di visualizzazione e segnala l'esatta pagina/lingua/build. |

Per una segnalazione utile, includi la versione di Windows, la build/il commit dell'app, la lingua, il ridimensionamento dello schermo, il modello hardware, una sequenza minima e il registro pertinente con i dati sensibili rimossi. Rimuovi percorsi personali, contenuti delle azioni e credenziali prima della pubblicazione.

## Costruzione, test e confezionamento

Usa la versione dell'SDK fissata in `global.json`. Dalla radice del repository:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

I test di integrazione Windows possono aprire finestre isolate e configurare/rilasciare temporaneamente le associazioni dei fornitori supportati. Eseguili senza che un altro controller possieda tali associazioni. Le sonde di luminosità/audio predefinite sono di sola lettura; i test espliciti di andata e ritorno dell'hardware sono separati. Protocolli fittizi, screenshot renderizzati e grafici di inferenza casuale non devono essere rappresentati come accettazione reale della qualità dell'hardware/modello.

Lo script di pubblicazione completa raccoglie gli host ausiliari, le risorse mobili, il plugin di esempio, il runtime ONNX e gli avvisi disponibili sulle dipendenze. Conserva le sottodirectory. Una compilazione riuscita non costituisce un programma di installazione firmato né un'approvazione legale del rilascio. Il codice sorgente e le build possono evolvere separatamente; confronta l'istantanea della documentazione e il commit con la versione installata.

## Licenza, privacy e stato di rilascio

Il modello commerciale previsto è un acquisto una tantum con uso perpetuo della versione principale acquistata; il prezzo finale, il numero di dispositivi consentiti e le condizioni di aggiornamento devono essere definiti nelle condizioni di vendita effettive. Questo manuale non dimostra l'esistenza di un sistema di acquisto/attivazione pronto per la produzione. Non è richiesto un account cloud per il controllo quotidiano.

Non distribuire file binari dell'SDK proprietario senza un'autorizzazione comprovata. Conserva le licenze e gli avvisi di terze parti per ogni dipendenza spedita, inclusi i pacchetti .NET e ONNX. I pesi dei modelli hanno i propri termini. La verifica della licenza datata precede alcune nuove dipendenze e non costituisce un'autorizzazione per tali aggiunte. Essendo pubblico, il repository non garantisce di per sé una licenza open source generale.

La convalida su hardware reale dei produttori, il funzionamento di telefoni/tablet in LAN, i passaggi tra monitor con DPI diversi, l'uso prolungato, la revisione linguistica completa, il supporto di un driver per l'uso esclusivo della tastiera e una qualità utile della pianificazione IA rimangono attività di convalida separate. Non dedurre mai l'approvazione dalla sola presenza di una funzionalità.

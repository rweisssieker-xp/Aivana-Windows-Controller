<!-- generated-doc locale=it-IT source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-12-local-meeting-assistant.md)

# Assistente alla riunione con intelligenza artificiale locale – prototipo

Stato: 12. Ottobre 2026. Ipotesi dell'agente di risultato ForgeMind, flusso di riunioni delimitate. Nessun vantaggio competitivo comprovato e nessun risparmio di tempo misurabile senza veri test comparativi.

## Utilizzo

1. Tavolozza comandi → **KI Assistente riunione**. L'esperimento è disattivato per impostazione predefinita e viene reimpostato durante l'importazione della configurazione o l'esportazione portatile.
2. Abilita l'assistente locale. Ollama deve essere eseguito separatamente su `127.0.0.1:11434`; deve essere già installato un modello locale idoneo. **Carica i modelli locali** e seleziona il modello. MAKRIVO non installa modelli/servizi, non contatta OpenAI/Cloud API né utilizza chiavi API. La configurazione offline e l'affidabilità del servizio locale Ollama spetta all'utente; i modelli remoti/cloud dichiarati vengono rifiutati.
3. Inserisci la destinazione e aggiungi esplicitamente programmi/documenti/siti web. Vengono trasmessi destinazione, ID risorsa, nome e tipo; I percorsi di destinazione e i contenuti dei file selezionati non vengono inoltre trasmessi; la voce di destinazione stessa viene inviata parola per parola. Le etichette del sito Web includono solo schema/host, il collegamento completo rimane locale.
4. **KI crea bozza**. Numero massimo di secondi 90, token di output 1024, dimensione di risposta limitata. Le modifiche al target/modello/risorsa eliminano l'anteprima; Annulla/Chiudi termina la richiesta. Il modello può suggerire solo ID selezionati. ID sconosciuti, comandi gratuiti, campi JSON aggiuntivi e tipi di documenti eseguibili vengono rifiutati.
5. Controlla anteprima e punti aperti → **Controlla nell'editor**. Il risultato è inizialmente una bozza non salvata. Salvare prima nell'editor esistente ed eseguirlo esplicitamente se necessario. La normale convalida macro, il controllo pre/post, la pausa e l'interruzione rimangono responsabili.

Supportato: avvio di programmi senza argomenti, apertura di PDF/DOCX/XLSX/PPTX/TXT/MD locali esplicitamente selezionati, apertura di siti Web HTTP(S), disattivazione del microfono su richiesta esplicita. Nessuna modifica predefinita dell'auricolare, nessun messaggio, appuntamento, download, script, clic gratuiti o immissione di testo. I desideri non rappresentabili appaiono come punti aperti se il modello li riconosce correttamente; L'utente deve verificare se gli obiettivi tecnici sono stati raggiunti. L'apertura del documento non viene verificata automaticamente dal punto di vista tecnico.

## Esperimento di confronto

**Misura configurazione manuale · Inizio/Fine** misura l'intero flusso di confronto manuale. I presupposti della procedura guidata misurano il tempo dall'apertura della procedura guidata al primo salvataggio nell'editor; `generated` contiene invece solo la durata della pianificazione. Non confrontare due durate diverse per risparmiare tempo.

Gli eventi locali si trovano nella cartella di configurazione sotto `Experiments/meeting-experiment.jsonl`: timestamp, tentativo casuale/ID riquadro, evento, durata, correzione sì/no. Nessun testo di destinazione, nomi di risorse, percorsi, risposte modello o contenuto di documenti. Il file ruota da 2 MiB, viene mantenuto un file precedente. Gli errori di misura non devono interrompere il salvataggio delle macro e della configurazione.

Acquisizioni: linea di base manuale, generazione/errori/interruzione del modello, eliminazione delle modifiche/chiusure dell'anteprima, salvataggi iniziali e correzioni nell'editor e stati finali effettivi del corridore per le tessere salvate da essi. `run_completed` significa semplicemente che il corridore ha completato; Gli effetti target confermati sono elencati separatamente nel processo di test. Le ripetizioni della stessa macro non sono nuovi tentativi di installazione indipendenti. L'esperimento iniziale può continuare dopo il riavvio perché l'ID esperimento rimane nel riquadro salvato. Le tessere duplicate condividono questa origine; raggruppare in base all'ID della prova durante la valutazione.

Primo esperimento: 20 strutture per riunioni comparabili o due settimane con utenti volontari del test; inizialmente uso interno. Confronta il tempo di configurazione manuale con il tempo di configurazione completamente assistita; Valuta inoltre il tasso di correzione/rifiuto, gli errori del corridore e i risultati reali dei test. Se non si riscontra alcun miglioramento o se i limiti di protezione vengono violati, disattivare l'interruttore. Nel repository non è stato dimostrato alcun utilizzo o disponibilità a pagare. Nessuna trasmissione telemetrica automatica o adeguamento dei prezzi.

## Banco prova

- Principale: limiti, tipi, ID sconosciuti/duplicati, esclusione di script, collegamenti lunghi, configurazione legacy facoltativa, accettazione esplicita di importazione/esportazione, misurazione del risparmio dei dati ed eliminazione e commit corretto una tantum.
- Trasporto con modello falso: schema Ollama JSON, destinazione di loopback, nessun percorso di file nel prompt, nome cloud e alias remoto rifiutati, risposta danneggiata/troppo lunga rifiutata, interruzione.
- WPF-Fumo: finestra di dialogo renderizzata; L'invio della bozza non modifica i riquadri salvati, la revisione/salvataggio preserva la provenienza e registra la correzione, lo scarto non salva nulla.
- Revisione indipendente: anteprima scartata e collegamenti lunghi trovati e corretti; Test di regressione ROSSO→VERDE.
- Rilascio della build senza avvisi/errori, intera suite principale e WPF smoke riusciti. Inizialmente esisteva la suite completa Windows, comprese le modifiche al focus. Un'altra esecuzione è fallita nuovamente perché la modifica in primo piano è stata rifiutata; il test dipende dal contesto e rimane invariato. I restanti test Windows, incluso il trasporto del modello, passano con il test di successo in primo piano esplicitamente omesso.
- L'ispezione dei pacchetti ha rivelato un bug esistente nello scambio atomico di file di configurazione aperti brevemente. Un blocco in lettura riproduceva lo stesso errore di accesso negato; la ripetizione limitata del solo scambio atomico è spuntata ROSSO→VERDE. Gli errori di accesso permanenti rimangono visibili.
- Pacchetto autonomo attuale `artifacts/release-20261012-184105`: WPF fumo riuscito dopo la correzione della memoria, incluso il dialogo AI e l'accettazione/rifiuto della bozza.
- Non è un vero test del modello: il servizio locale Ollama non ha risposto durante il test. Nessuna dichiarazione sulla qualità del modello, la latenza, la completezza semantica o il risparmio di tempo. Rimangono le limitazioni hardware/driver/scalabilità della versione precedente.

Le fonti di protocollo: [Ollama Il Chat](https://docs.ollama.com/api/chat), [Strutturato Outputs](https://docs.ollama.com/capabilities/structured-outputs), [Definizioni ufficiali di tipo, compresi i dati di modello remoto](https://github.com/ollama/ollama/blob/main/api/types.go).

<!-- generated-doc locale=it-IT source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Piano attuativo estensioni

> **Per gli operatori degli agenti:** COMPETENZA SUBORDINATA RICHIESTA: utilizzare superpoteri:esecuzione di piani per l'esecuzione nativa o superpoteri:sviluppo guidato da subagenti dopo che l'utente ha selezionato la delega. Tieni traccia dei passaggi con le caselle di controllo.

**Obiettivo:** Fornire sei estensioni integrate e supporto per tastiere multiproduttore basato sulle funzionalità.

**Architettura:** Conserva la separazione Core / Windows / WPF. Introdurre plugin isolati e processi di servizi mobili con IPC autenticati ristretti; tutta l'esecuzione passa attraverso il controller esistente e la coda MacroRunner. Gli adattatori hardware pubblicizzano le funzionalità rilevate anziché il supporto dedotto a livello di marchio.

**Tech Stack:** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, SDK del fornitore documentati, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (approvato dall'utente).

## Vincoli globali

- La configurazione esistente deve essere caricata invariata; aggiungere tipi di azioni anziché riordinare i relativi valori enum persistenti.
- Conserva i profili esistenti, le assegnazioni G1/G2, il funzionamento locale e l'utilizzo senza account.
- Raw Input fornisce solo l'osservazione. La soppressione esclusiva per dispositivo richiede un backend effettivo del filtro della tastiera.
- I plugin eseguibili importati non devono essere eseguiti automaticamente.
- Per impostazione predefinita sono disabilitate e limitate le interfacce configurate per il servizio mobile; nessun fallback di autenticazione in testo normale.
- L'invio di scorciatoie/testo non è una prova che l'applicazione di destinazione abbia accettato il comando.
- Non sostituire l'applicazione installata, installare driver, alterare la mappatura reale della tastiera o aprire l'accesso alla rete come test incidentale.
- Conserva le modifiche non correlate e non salvate. Non confermare le modifiche condivise esistenti come parte di questa funzionalità. Metti in scena solo pezzi di funzionalità revisionabili in modo indipendente se sono richiesti commit.

## Rivedi Focus

1. Duplica modelli di tastiera identici: la riconnessione ambigua deve richiedere una nuova selezione (Attività 10–12).
2. Cancellazione durante le attese UIA/provider/plug-in/rete: nessuna azione differita dopo la cancellazione (attività 1, 4, 6, 8).
3. Modifiche dell'utente mentre la procedura guidata o l'anteprima del pacchetto è aperta: la mancata corrispondenza della revisione deve impedire la sovrascrittura silenziosa (attività 3, 7).
4. La lingua e il layout fisico differiscono: i selettori e le associazioni del codice di scansione non devono dipendere dai nomi visualizzati localizzati (Attività 4, 10).
5. Nuovo tentativo di rete e riavvio del plugin: i comandi non devono essere eseguiti silenziosamente due volte (attività 6, 8).

## Contratti condivisi e ordine di dipendenza

I compiti 1–3 stabiliscono l'esecuzione e la configurazione. Attività 4–5 creare UIA. Attività 6–7 fornire plugin. Compiti 8–9 fornire accesso mobile. Compiti 10–12 fornisce adattatori per tastiera e integrazione di filtri. L'attività 13 convalida il prodotto completo.

Ogni attività segue un ciclo di test fallito → implementazione minima → test superato. Un'attività è completata solo quando è raggiungibile tramite l'applicazione e i suoi controlli superano. Impalcature, adattatori simulati e documentazione da soli non soddisfano un risultato finale.

### Attività 1: verifica tipizzata e persistenza compatibile

**File:** Crea `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; modificare `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfacce:** Produci `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` e `VerifyAsync(ActionStep, CancellationToken)`, entrambi restituiscono `Task<ActionVerificationResult>`. Aggiungi `VerificationOptions` serializzabile opzionale con flag e timeout abilitati (100–60000 ms), disabilitato per impostazione predefinita. Pubblica risultati digitati per passaggio, inclusi ID esecuzione, ID riquadro e indice dei passaggi senza payload di azioni.

- [ ] Controlli di scrittura: JSON legacy conserva i valori numerici dell'azione esistente; le opzioni assenti disabilitano la verifica; timeout non valido non valida la convalida; il fallimento del preflight impedisce l'invocazione dell'esecutore; la postcondizione fallita non produce mai un successo verificato; l'esecutore non supportato non è verificabile; la cancellazione interrompe la verifica e i passaggi successivi.
- [] Esegui `dotnet run --project tests/Aivana.Tests -c Release`; confermare che i nuovi controlli falliscono prima dell'implementazione.
- [ ] Implementare contratti, convalida, clonazione/conservazione di istantanee e integrazione MacroRunner. Rispettare le politiche di interruzione/continua di OnError; preservare gli abbonati esistenti al feedback sull'esecuzione.
- [] Esegui test fondamentali; richiedere il superamento di tutti i controlli.

### Attività 2: fornitori di verifica Windows e interfaccia utente dei risultati

**File:** Crea `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; modificare `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, testare i punti di ingresso.

**Interfacce:** WindowsActionVerification implementa il provider Task 1 utilizzando audio, visualizzazione, processo/finestra e client OBS esistenti. Disponibilità e identificatori dei test preliminari; la verifica confronta lo stato desiderato dopo il polling limitato. Produrre istantanee dei risultati del controller consumate dalla vista cronologia WPF.

- [ ] Aggiunti test non riusciti per ID audio/visualizzazione espliciti non disponibili, timeout OBS, stato osservato non corrispondente, effetti di scelta rapida sconosciuti, omissione di carico utile sensibile e polling annullato, utilizzando provider di lettura iniettabili.
- [ ] Implementa la verifica di rilettura reale per volume assoluto, disattivazione audio, luminosità assoluta, disponibilità di processo/finestra e comandi OBS supportati. Le azioni relative/di commutazione richiedono un pre-stato catturato; altrimenti segnala Nonverificabile.
- [ ] Aggiungi controlli di verifica passo-passo e cronologia di esecuzione sicura che mostra lo stato, i requisiti non riusciti e il testo di ripristino.
- [ ] Eseguire controlli core/Windows e fumo WPF con fornitori statali inseriti; nessuna luminosità reale o scrittura audio.

### Attività 3: Creazione guidata dello spazio di lavoro

**File:** Crea `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; modificare i punti di ingresso della palette e del dashboard.

**Interfacce:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; la selezione contiene l'ID modello, i percorsi eseguibili scelti, gli URL, gli ID audio/visualizzazione, la scena OBS e le associazioni selezionate. L'individuazione restituisce i candidati nominati con ID e disponibilità stabili. Revisione della configurazione dei record in bozza per il rilevamento dei conflitti.

- [ ] Aggiunti test non riusciti per tutte e cinque le famiglie di procedure guidate richieste, passaggi generati in modo indipendente, obiettivi richiesti mancanti, annullamento senza mutazione e salvataggio in base alla revisione modificata.
- [] Scopri le applicazioni attraverso i percorsi delle app registrate e le scorciatoie del menu Start; dispositivi tramite fornitori esistenti; scene tramite query OBS di sola lettura. Mantieni disponibile la selezione manuale per le app non ancora scoperte.
- [ ] Implementa Scegli flusso di lavoro → Seleziona obiettivi → Scegli controlli → Anteprima/convalida → Salva. Genera profili modificabili e non eseguirli mai durante la configurazione.
- [ ] Aggiungi effettivi controlli di fumo della procedura guidata WPF per completamento/annullamento/conflitto; eseguire tutti i test.

### Task 4: selettori UIA e azioni limitate

**File:** Crea `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modificare modelli, convalida della configurazione e WinActionExecutor.

**Interfacce:** Aggiungi `ActionKind.UiAutomation`; `UiElementSelector` memorizza processo, ID di automazione, tipo di controllo e criteri antenati ordinati. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` restituisce `Task`; operazioni Invoke, SetValue, Select, Toggle, Focus e WaitState. La risoluzione del selettore deve restituire esattamente un elemento idoneo.

- [ ] Aggiungi test falliti rispetto a una finestra di test dedicata per riposizionamento/ridimensionamento, corrispondenze duplicate, elementi mancanti, modelli non supportati, controlli disabilitati, valori di password, etichette Unicode e timeout/cancellazione.
- [ ] Implementare l'UIA su un lavoratore dedicato con richieste limitate e targeting del processo. I provider potenzialmente sospesi vengono eseguiti all'interno di un limite di lavoro riavviabile; l'abbandono di una richiesta non deve lasciare in coda effetti collaterali.
- [ ] Esclude i valori della password; segnalare le restrizioni di accesso/integrità invece di aggirarle.
- [ ] Eseguire controlli Windows utilizzando dispositivi isolati; aggiungi solo valori enum ed esegui nuovamente i controlli di persistenza legacy.

### Attività 5: selettore UIA e registrazione rivista

**File:** Crea `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modificare il catalogo delle azioni, l'integrazione dell'editor e del registratore; creare controlli di fumo UIA WPF.

**Interfacce:** Il selettore restituisce un `UiElementSelector`; il registratore restituisce i passaggi UIA proposti e non scrive mai direttamente in un profilo salvato. Il comando test-selector restituisce il conteggio delle corrispondenze senza invocazione.

- [ ] Aggiungi controlli non riusciti: il selettore esclude MAKRIVO e l'acquisizione del valore della password; registra/annulla lascia invariato il profilo esistente; conteggio delle corrispondenze dei report di anteprima ambigui; l'accettazione dei passaggi registrati crea una bozza modificabile.
- [ ] Implementa l'evidenziazione/selezione degli elementi, parametri specifici dell'operazione, anteprima del selettore e finestra di revisione del registratore. Mantieni il comportamento esistente del registratore di scorciatoie.
- [] Aggiungi voci di catalogo e accesso alla tavolozza; convalidare gli errori dell'editor prima del salvataggio.
- [ ] Eseguire controlli di fumo Windows e WPF sull'apparecchio; ispeziona gli screenshot del selettore/editor.

### Attività 6: host e protocollo del plugin isolato

**File:** Crea `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; modificare la soluzione e l'invio dell'azione.

**Interfaces:** Version-1 manifest specifica l'ID/versione del pacchetto, il punto di ingresso, gli schemi dei parametri, le azioni, i provider di stato e le dichiarazioni di capacità. I messaggi IPC contengono la versione del protocollo, l'ID di richiesta univoco, il metodo, i parametri JSON e il risultato digitato. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` restituisce un risultato digitato. Aggiungi `ActionKind.Plugin`.

- [ ] Aggiunti test non riusciti per protocollo incompatibile, messaggio non valido/di grandi dimensioni (>1 MiB), ID richiesta duplicato, annullamento, arresto anomalo, processo che non risponde e azione mancante.
- [] Implementare un IPC con nome pipe per lo stesso utente con handshake di 10 secondi e timeout di esecuzione configurabile limitato a 60 secondi. licenziare il lavoratore che non risponde; non riprodurre automaticamente un'azione durante il volo dopo il riavvio.
- [ ] Applicare schemi e servizi del controller consentiti. Spiegare la fiducia del plugin eseguibile; l'isolamento del processo non è una sandbox del sistema operativo.
- [ ] Esegui test di base e test di integrazione rispetto a processi di plug-in di test reali.

### Compito 7: pacchetti di plugin ed integrazione di esempio

**File:** Crea `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, test dei pacchetti; modificare le voci della tavolozza/impostazioni.

**Interfacce:** Il servizio pacchetto espone `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` e `UninstallAsync(packageId)`. Hash dei record del pacchetto revisionati, dipendenze, versione e capacità dichiarate.

- [ ] Aggiunti test non riusciti per zip traversal, percorsi assoluti, escape di collegamento simbolico/reparse, voci duplicate, >100 MiB dati espansi, mancata corrispondenza hash, downgrade, revisione obsoleta e importazioni disabilitate.
- [ ] Implementare l'anteprima e l'abilitazione esplicita, gli errori di dipendenza, l'installazione in fasi e il rollback dell'aggiornamento; scaricare il lavoratore prima della rimozione.
- [] Fornire un plug-in di esempio reale che restituisce un'azione di conto alla rovescia configurabile e uno stato live tramite IPC, inclusa la cancellazione; pacchetto/esportazione/installazione/disinstallazione tramite l'interfaccia utente.
- [ ] Esegui test del pacchetto/host e controlli WPF end-to-end. Verificare che nessun eseguibile importato venga eseguito prima dell'abilitazione.

### Attività 8: servizio mobile HTTPS e abbinamento

**File:** Crea `src/Aivana.MobileHost/` Progetto ASP.NET Core, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, test di integrazione; modificare la soluzione e il controller.

**Interfacce:** Il servizio mobile comunica con il controller tramite IPC dello stesso utente. Percorsi: abbinamento, profili/azioni consentiti, invocazione di azioni autenticate, cancellazione e stato WebSocket. La chiamata include un ID comando univoco; il controller possiede la deduplicazione limitata. Le credenziali sono revocabili e archiviate tramite la protezione Windows.

- [ ] Aggiunti test non riusciti per enumerazione/esecuzione non accoppiata, codice scaduto, codice riutilizzato, token revocato, origine non valida, comando duplicato, frame sovradimensionato e limiti di flusso.
- [ ] Implementa listener solo HTTPS, interfacce selezionate dall'utente, istruzioni di provisioning/attendibilità del certificato locale, codici di accoppiamento a cifre 6 una tantum con scadenza dopo 120 secondi, massimo cinque tentativi per codice e revoca del token. Memorizza token casuali con hash; non registrare mai le credenziali.
- [] Implementare gli ID azione stabili consentiti e l'instradamento delle code esistenti; rifiutare shell, percorsi e comandi di installazione di pacchetti arbitrari. Configurare limiti di velocità espliciti per cliente (comandi 10/sec, burst 30) e limite di messaggi KiB 64.
- [ ] Verifica i test di integrazione di loopback senza aggiungere regole firewall o abilitare l'accesso LAN; testare l'arresto e la persistenza del token.

### Attività 9: pannello mobile e controlli di servizio desktop

**File:** Crea il pannello reattivo `src/Aivana.MobileHost/wwwroot/` e `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; aggiungi script di verifica browser/mobile e copertura antifumo WPF.

**Interfacce:** Consuma attività 8 Solo rotte. Esporre la griglia delle azioni touch, il selettore del profilo, i valori/progressi in tempo reale, l'annullamento, lo stato della connessione e l'accoppiamento. Il desktop controlla l'avvio/arresto del servizio, le istruzioni sull'indirizzo/certificato, i profili consentiti e la revoca del cliente associato.

- [ ] Aggiunti controlli di integrazione non riusciti per flusso di autenticazione, ID azione obsoleti/mancanti, riconnessione senza riproduzione dell'azione, client revocato e arresto del servizio.
- [ ] Implementa un'interfaccia utente reattiva accessibile utilizzando risorse locali, senza script di terze parti o dipendenze Internet. I token non vengono mai visualizzati negli URL.
- [] Prova il layout del browser su 360/768/1280 CSS px; ispezionare gli screenshot reali. Verificare con un telefono/tablet reale solo dopo che l'utente ha abilitato esplicitamente il servizio LAN.
- [ ] Documenta e segnala la verifica dell'hardware mobile separatamente dall'emulazione del browser.

### Compito 10: Funzionalità del dispositivo e osservazione in chiave ordinaria

**File:** Crea `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modificare RawKeyboardBindingService, KeyboardBinding, validatore e diagnosi del dispositivo; aggiungere test.

**Interfacce:** `IKeyboardAdapter` espone istantanee di funzionalità, identità stabile del dispositivo e bordi fisici dei tasti (codice di scansione, flag estesi, pressione/rilascio). Le modalità distinguono l'osservazione, l'input standard mappato, l'SDK nativo e l'esclusivo driver. Le associazioni speciali di chiave virtuale esistenti mantengono la loro rappresentazione e il comportamento di migrazione.

- [] Aggiunti test falliti per vecchi collegamenti, distinzioni di immissione, indipendenza dalla lingua/layout, down ripetuti, scollegamento mentre trattenuto, ambiguità del modello duplicato e rifiuto dell'input inserito.
- [ ] Estendere l'osservazione ai normali tasti fisici senza sopprimerli. Aggiungi l'acquisizione esplicita e riconnetti la gestione dell'identità.
- [] Mostra le capacità effettive dell'adattatore e lo stato di hardware non testato nell'interfaccia utente del dispositivo; non dedurre mai il supporto Fn/dial dai nomi dei marchi.
- [ ] Esegui controlli core/Windows/WPF con report simulati ed enumerazione dei dispositivi di sola lettura.

### Attività 11: adattatori del produttore

**Files:** Crea file separati in `src/Aivana.Windows/KeyboardAdapters/` per LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog e la guida generica di QmkVia; modificare il registro/interfaccia utente e aggiungere controlli del protocollo dell'adattatore.

**Interfacce:** Consuma attività 10 Contratti adattatore. Le librerie native vengono caricate solo da posizioni di app/fornitori convalidate; L'assenza dell'SDK è una funzionalità non disponibile. Le funzionalità di illuminazione e input sono separate.

- [ ] Verificare le attuali intestazioni ufficiali dell'SDK, i termini di ridistribuzione, le dipendenze del software e i modelli supportati prima di implementare ciascun adattatore; registrare fonti e versioni esatte.
- [ ] Aggiunti test non riusciti per architettura assente/errata dell'SDK, pressione/rilascio/ripetizione di eventi, disconnessione, risposta non valida e dispositivo non supportato. Utilizza astrazioni iniettabili di chiamate native per controlli deterministici.
- [] Implementa separatamente l'input del tasto G Logitech e l'illuminazione supportata; Razer Illuminazione cromatica più Synapse procedura guidata per la mappatura dei tasti standard; SteelSeries GameSense stato/illuminazione; Wooting polling analogico con soglie/isteresi configurabili; QMK/VIA Guida alla configurazione dei tasti standard. Non rivendicare input proprietari dalle API di illuminazione.
- [ ] Convalida i dispositivi reali compatibili, ove disponibili. Contrassegnare gli altri implementati/non testati dall'hardware, con prova del modello/firmware; l'SDK utilizzabile mancante è un cancello esterno divulgato.

### Attività 12: backend esclusivo del filtro della tastiera

**File:** Crea `src/Aivana.Windows/KeyboardFilter/`, sorgente del programma di installazione in `tools/keyboard-filter/`, test di integrazione backend e documentazione di distribuzione. Se nessun backend con licenza adeguato supera la valutazione, è necessario un progetto di driver firmato dedicato.

**Interfacce:** Il backend riporta lo stato installato/compatibile/firmato; espone l'avvio/arresto dell'acquisizione specifico del dispositivo, i bordi dei tasti e il rilascio di emergenza. Un watchdog separato ripristina il pass-through in caso di perdita del controller. La tastiera principale non diventa mai esclusiva per impostazione predefinita.

- [ ] Valuta la patente di guida candidata, la provenienza del codice binario firmato e la compatibilità di avvio Windows 11/Secure corrente. Registrare una decisione go/no-go. Nessuna installazione di driver durante i test ordinari.
- [ ] Scrivere test del contratto per la selezione della destinazione del filtro, fail-open, scadenza dell'heartbeat, perdita di IPC, ambiguità di riconnessione, pausa/annullamento, rilascio ed eliminazione del tasto premuto.
- [ ] Implementa il backend, l'interfaccia utente di selezione/attivazione esplicita, il rilascio di emergenza e il programma di installazione/disinstallazione. Non sostituire l'euristica temporale di basso livello/Raw Input.
- [ ] Convalida in una macchina di prova recuperabile con due tastiere: tasto selezionato soppresso solo sul dispositivo selezionato; la digitazione principale non è influenzata; arresto anomalo/sospensione/disinstallazione ripristina l'input.
- [] Completamento della produzione del gate sul backend effettivo firmato/concesso in licenza e sui test fisici. Se la firma/licenza esterna non è disponibile, mantieni l'attività incompleta con prove precise delle dipendenze.

### Compito 13: Pacchetto integrato di verifica, traduzioni e rilascio

**File:** Aggiungi `docs/testing/2026-10-12-six-extensions.md`; aggiornare la documentazione pertinente in DE/EN/FR/IT, i cataloghi di localizzazione e i punti di ingresso per il controllo del fumo. Conserva altre modifiche alla documentazione.

- [] Integra tutti i punti di ingresso e le istantanee; completare le traduzioni utilizzando il flusso di lavoro del catalogo in lingua 27 esistente e divulgare le traduzioni non revisionate.
- [ ] Eseguire `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` e `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Esamina gli screenshot di desktop/dispositivi mobili e verifica il ridimensionamento percentuale 100/150/200 Windows, i round trip di configurazione, il ciclo di vita del plug-in, la revoca della rete e la riconnessione reale della tastiera.
- [ ] Pubblica in una nuova directory degli artefatti con `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; includere i file binari del servizio/host complementare e gli avvisi di licenza. Non sovrascrivere l'app installata.
- [ ] Segnala test completati, adattatori hardware non testati e gate di rilascio esterni. Tutti e sei i risultati finali devono soddisfare i criteri di accettazione prima di poter definire completata l'intera richiesta.

## Pianifica l'autovalutazione

Tutte e sei le sezioni delle specifiche sono mappate su Attività 1–12; L'attività 13 copre la localizzazione, la verifica integrata e il rilascio. I report sulla disponibilità, la serializzazione dell'esecuzione, la configurazione legacy, i segreti, l'attendibilità dei pacchetti e l'ambiguità del dispositivo fisico hanno proprietari e test espliciti. I gate driver/SDK/hardware esterni rimangono incompleti fino a quando non vengono dimostrati, anziché essere conteggiati come successo del software.

## Scelta di esecuzione per la revisione dell'utente

Consigliato: esecuzione nativa in questa chat, attività per attività, poiché la maggior parte dei moduli condivide contratti e l'area di lavoro corrente contiene modifiche esistenti sostanziali. L'esecuzione delegata è disponibile se l'utente lo sceglie. Esamina questo piano e seleziona il metodo di esecuzione prima dell'implementazione, come richiesto dal flusso di lavoro dei piani di scrittura.

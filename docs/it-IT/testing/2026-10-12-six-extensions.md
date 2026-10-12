<!-- generated-doc locale=it-IT source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-12-six-extensions.md)

# Sei estensioni: implementazione e banco di prova

Stato: 12. Ottobre 2026, Filiale `codex/six-extensions`. Stato funzionante, nessuna release di produzione.

## Servizio

La palette dei comandi apre le voci **Configura workstation**, **Produttore tastiera e SDK**, **Pannello mobile**, **Estensioni**, **UI automazione**, **UI flusso di registrazione** e **Test di esecuzione**.

- Workspace Assistant crea profili Workplace, Meeting, Streaming, Research e Project. I programmi dal Registro di sistema/Menu Start o dal Selettore file, URL, Output/Schermo e Scena OBS vengono visualizzati come passaggi prima del salvataggio. La struttura non esegue alcuna azione.
- Nell'editor dei passi **Risultato controllo** attiva il controllo preliminare e il controllo successivo. La cronologia e il pannello mobile distinguono la conferma effettiva dall'effetto non verificabile. Le modifiche relative, le scorciatoie da tastiera e l'immissione di testo non forniscono una conferma generale dell'efficacia. La prova del processo non dimostra la preparazione tecnica della domanda.
- Automazione interfaccia utente utilizza un lavoratore separato e limitato nel tempo. Selezione tramite posizione del mouse, controllo del selettore, Invoke/Select/Toggle/Focus/SetValue/WaitState; risultati multipli, target disabilitati e campi password vengono rifiutati. La registrazione acquisisce le azioni di clic e le pause supportate; Le voci di testo non vengono registrate. L'ammissione sarà verificata prima dell'accettazione.
- Estensioni: ispezione ZIP, hash, schema, importazione disabilitata, trust esplicito prima dell'attivazione, processo isolato con IPC limitato, esportazione, disattivazione, disinstallazione, ripristino/eliminazione della versione precedente. L'isolamento del processo non è una sandbox: i plugin condivisi vengono eseguiti con diritti utente. Il pacchetto di esempio `makrivo.example.zip` contiene il conto alla rovescia e l'azione di stato. Gli schemi supportati rappresentano un sottoinsieme di schemi JSON semplice documentato nel codice sorgente; nessun mercato pubblico.
- Pannello mobile: seleziona IP locale e profili condivisi, apri l'indirizzo HTTPS, controlla il certificato utilizzando SHA-256-Fingerprints visualizzato e fidati consapevolmente di esso, quindi inserisci un codice a sei cifre una tantum. Le chiavi private sono protette con Windows-DPAPI; vengono salvati solo gli hash di accesso client. I certificati cambiano dopo il rinnovo o la modifica dell'IP. Se necessario, il firewall Windows viene abilitato dall'utente. Nessuna installazione automatica del certificato, port forwarding o modifica del firewall. L'accesso del client può essere revocato. Il pannello mostra lo stato dell'azione e la cronologia dei test; non è ancora una dashboard universale con sensore live.

## Tastiere

| Produttore | Attuazione | Limitazione |
|---|---|---|
| Corsair | tasti iCUE-G esistenti e RGB | Sono richiesti connessione locale iCUE e modello adatto |
| Logitech G | SDK G-Key e SDK LED separato | rilasciare esplicitamente il locale originale x64-DLL; G-Key SDK non fornisce l'identità individuale di più tastiere |
| Razer | Chroma RGB e riconoscimento chiave standard | Assegnare le chiavi proprietarie in precedenza in Synapse |
| SteelSeries | GameSense-RGB e riconoscimento tasti standard | assegnare chiavi proprietarie precedentemente in GG |
| Wooting | Valori analogici relativi al dispositivo, soglie di pressione/rilascio | originale locale x64-SDK-DLL; il normale ingresso HID rimane attivo |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/altri | Pulsanti fisici standard per dispositivo e codice di scansione | I controlli Fn e proprietari dipendono dal modello |

Non sono incluse nuove DLL SDK proprietarie. Gli adattatori del nuovo produttore non sono ancora convalidati dall'hardware senza dispositivi fisici adeguati.

**Il secondo macropad esclusivo non è pronto:** Raw Input osserva i dispositivi separatamente, ma non sopprime il loro normale ingresso Windows. Una versione di produzione richiede un driver di filtro firmato e con licenza verificato con Windows 11/Secure Test di avvio e rilascio di emergenza, arresto anomalo, sospensione e riconnessione. Nessun driver è stato installato. L'applicazione contrassegna esplicitamente questo limite.

## Esami

- Build di rilascio: riuscito, zero avvisi/errori.
- Console principale: tutti i gruppi di test vengono superati, inclusi i conflitti con le chiavi del fornitore, il rollback dei plug-in e gli errori di esecuzione effettivi nella cronologia dei test.
- Integrazione dell'estensione: vero lavoratore UIA con applicazione di test WPF; richiamo univoco, ambiguità, target disabilitati, esclusione della password. Processo di plug-in con nome reale, limiti del protocollo, isteresi analogica e decodifica ABI Logitech riuscita.
- Host HTTPS reale: certificato bloccato, accesso non accoppiato, codice falso/univoco, protezione dell'origine, file del browser, archiviazione hash e chiave protetta riuscita. È stato corretto un bug Schannel con chiavi temporanee scoperte durante i test.
- Windows suite con test di successo in primo piano esplicitamente omesso: riuscito; Audio/OBS/iCUE/Raw Input e stato dello schermo controllati. Nell'esecuzione precedente, l'intera suite non è riuscita perché è stato rifiutato il passaggio dello stato attivo alla finestra di test isolata. Il successivo passaggio al prototipo AI comprendeva anche la suite completa, compreso il cambio di messa a fuoco e una tastiera iCUE collegata. Un'altra ripetizione è fallita nuovamente a causa del cambio di messa a fuoco; Il test dipendente dal contesto quindi non è verde stabile. Questo test non è stato attenuato. Gli accessi in lettura DDC che falliscono per un breve periodo vengono ripetuti per un tempo limitato; nessuna luminosità è stata modificata.
- WPF-Smoke: rendering di tutte le finestre di dialogo precedenti e nuove, invarianza di configurazione, editor e CRUD riusciti; Schermate nella cartella di creazione `smoke`.
- Browser: Drammaturgo con Edge, 390×844 e 1024×768, flusso di accoppiamento/UUID/invio una tantum/nessun overflow orizzontale riuscito. API simulata; test HTTPS reale separatamente. Immagini su `artifacts/mobile-browser`. Il plug-in del browser non era disponibile, quindi il normale fallback di Playwright.
- Condotta una revisione indipendente del codice; Risolti i comandi mobili ritardati, i risultati del controllo del terminale mancanti, il controllo del percorso del processo, il rollback del plugin e il riavvio del battito cardiaco RGB.

Ancora aperti: integrazione esclusiva dei driver, test LAN fisico del produttore/telefono/tablet, ridimensionamento Windows 150/200 percentuale e controllo completo della traduzione di tutti i nuovi testi di dialogo. I nuovi testi dei dialoghi sono prevalentemente tedeschi; I cataloghi delle lingue esistenti vengono mantenuti. Pertanto, il requisito complessivo ** non è stato completamente accettato**.

## Genera pacchetto

`./tools/publish-complete.ps1 -SelfContained` crea una nuova cartella in `artifacts/release-*`, inclusi desktop, lavoratore UIA, host HTTPS, file del browser, plug-in di esempio importabili e note sulla licenza runtime. L'applicazione installata non viene sovrascritta. Senza `-SelfContained`, il pacchetto deve corrispondere a .NET-10-Desktop-/ASP.NET-Runtimes.

Il pacchetto è un banco di prova locale, non un programma di installazione al dettaglio firmato. Le Condizioni d'Uso e i diritti di distribuzione dell'SDK sono versioni separate; vedere la documentazione esistente sotto `docs/licensing`.

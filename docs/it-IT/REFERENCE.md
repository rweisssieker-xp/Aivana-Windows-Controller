<!-- generated-doc locale=it-IT source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../README.md)

# MAKRIVO

[Documentazione completa in tutte le lingue 27](README.md) · [Tutto il manuale utente](HANDBOOK.md)

**di Aivana.** [Inventario attuale delle funzionalità e limiti del rilascio](FEATURES.md) · [Rapporto di estensione](testing/2026-10-12-six-extensions.md) · [Prototipo di assistente IA locale](testing/2026-10-12-local-meeting-assistant.md)

**Inglese (USA)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](REFERENCE.md)

Un controller locale con tasti di scelta rapida Windows con riquadri, profili, macro e rilevamento della tastiera in stile Stream Deck. L'interfaccia supporta le lingue 27. Per l'uso quotidiano non è richiesto alcun account o connessione cloud.

[Documentazione](README.md) · [Verifica](VERIFICATION.md) · [Rapporto di test](testing/2026-10-09-usp-release.md)

## Iniziare

Dopo la pubblicazione, l'applicazione si trova in `artifacts/win-x64/MAKRIVO.exe`. Mantieni insieme l'intera cartella di output; puoi copiarla altrove. Windows 11 x64 è la piattaforma iniziale. Gli artefatti di compilazione sono esclusi da Git; una copia del repository sorgente non include un eseguibile precompilato.

Installa l'SDK .NET 10 specificato in `global.json` per compilare ed eseguire dall'origine:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Pubblica una build autonoma senza richiedere un runtime .NET installato separatamente:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Flusso di lavoro di base

1. Seleziona un profilo e crea un riquadro azione.
2. Imposta il nome, l'icona, il colore e la scorciatoia da tastiera. Le combinazioni Ctrl/Alt/Shift riducono l'attivazione accidentale durante la digitazione.
3. Aggiungi uno o più passaggi: applicazioni, file, siti Web, testo, scorciatoie, contenuti multimediali, volume, cambio di profilo o ritardi.
4. Salva. Le modifiche rimangono una bozza finché non vengono salvate; scartare ripristina la versione salvata. Il pulsante Riproduci del riquadro e il relativo tasto di scelta rapida utilizzano lo stesso percorso di esecuzione.
5. Riordina i riquadri e i passaggi macro con i pulsanti drag-and-drop o freccia. I modelli semplificano la configurazione. Le macro possono ripetersi fino a 100 volte. Annulla tramite l'interfaccia, il menu della barra delle applicazioni o Ctrl+Alt+Escape per impostazione predefinita.

I profili specifici dell'applicazione usano un nome di processo senza il percorso completo. L'ordine dei profili risolve le corrispondenze a pari priorità; il profilo predefinito è quello di riserva. La selezione automatica dei profili deve essere abilitata in Impostazioni.

Chiudere la finestra principale mantiene per impostazione predefinita l'app nell'area di notifica. Fai doppio clic sulla relativa icona per riaprirla; usa il menu dell'icona per uscire. L'avvio con Windows è facoltativo e disabilitato per impostazione predefinita. I tasti di scelta rapida possono essere messi in pausa centralmente.

## Azioni e tavolozza dei comandi

Aggiungi azione apre un catalogo ricercabile con preimpostazioni 90 in otto categorie. Ognuno ha una descrizione e un'anteprima. L'editor macro utilizza lo stesso catalogo per aggiungere o sostituire i passaggi. La selezione di una preimpostazione nel flusso di lavoro dell'editor crea una bozza senza esecuzione.

La tavolozza dei comandi si apre dall'intestazione o con Ctrl+K nell'app. Cerca le azioni salvate in tutti i profili, le preimpostazioni del catalogo, il cambio di profilo, la navigazione, pausa/ripresa, annullamento macro e importazione/esportazione. Filtra per azioni salvate, funzioni del catalogo o comandi dell'app. Le frecce selezionano, Invio attiva, Ctrl+Enter modifica una voce modificabile ed Escape chiude. I preferiti appaiono per primi. I comandi del catalogo pronti all'uso possono essere eseguiti subito; le preimpostazioni che richiedono configurazione aprono una bozza. Le impostazioni consentono una scorciatoia globale per la tavolozza. Le nuove configurazioni usano Ctrl+Shift+Space; quelle esistenti non ricevono automaticamente una nuova scorciatoia globale.

Le azioni audio impostano con precisione il volume da 0 a 100 percento e disattivano, riattivano o invertono lo stato di disattivazione dell'audio dei dispositivi di uscita e microfono. Scegli un endpoint specifico o lascialo non impostato per usare quello predefinito di Windows. Un endpoint salvato non disponibile genera un errore anziché controllare un altro dispositivo. Aggiorna dispositivi ricarica i dispositivi collegati.

OBS Studio usa WebSocket v5: avvio/arresto/commutazione/pausa/ripresa della registrazione; avvio/arresto/commutazione dello streaming; cambio di scena; commutazione della disattivazione audio di un ingresso. Abilita il server WebSocket in Strumenti in OBS e imposta l'endpoint nell'editor. Verifica connessione e carica nomi legge la versione OBS, le scene e gli ingressi senza avviare una registrazione o uno streaming. Scegli i nomi dall'elenco o inseriscili manualmente. Le password possono essere salvate in Windows Credential Manager e sono escluse dalle configurazioni esportate. Le connessioni OBS non disponibili segnalano un errore.

I profili possono corrispondere a un nome di processo e a una parte del titolo di una finestra. Entrambe le condizioni specificate devono corrispondere. Blocca profilo impedisce il cambio automatico di profilo durante la sessione. Acquisisci condizioni dal primo piano usa l'ultima applicazione esterna che aveva il focus.

Le azioni di testo e appunti supportano `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. Le date usano `yyyy-MM-dd`; gli orari usano `HH:mm`. I segnaposto sconosciuti rimangono invariati. `{{profile}}` identifica il profilo dell'azione avviata; `{{process}}` identifica il contesto dell'applicazione esterna. Il contenuto degli appunti viene letto solo quando si usa quel segnaposto. L'azione degli appunti scrive testo ma non lo incolla automaticamente.

Le azioni della finestra riducono a icona, massimizzano, ripristinano o dispongono una finestra sul lato sinistro/destro dell'area di lavoro del relativo monitor. L'azione del sistema blocca Windows. Queste azioni vengono eseguite solo se richiamate esplicitamente.

L'anteprima mostra passaggi, ripetizioni e variabili senza esecuzione. Annulla e Ripeti mantengono fino a 100 stati bozza; le caselle di testo mantengono la modifica nativa Ctrl+Z. Contrassegna i preferiti nell'editor o nel menu riquadro e filtrali nella dashboard. I conteggi delle esecuzioni si applicano solo alla sessione corrente.

Le azioni di testo, collegamento e finestra avviate dall'interfaccia attivano l'ultima finestra di destinazione esterna. La tavolozza visualizza il contesto di destinazione. Se il target si è chiuso o Windows rifiuta l'attivazione, l'esecuzione si interrompe con un messaggio.

## Assistenza e feedback sulla tastiera

La procedura guidata per i tasti speciali si trova nella pagina Tastiera e nella tavolozza. Elenca i tasti G segnalati da iCUE. Rileva pressione tasto li acquisisce temporaneamente senza eseguire le azioni assegnate. Le associazioni normali vengono ripristinate dopo rilevamento, annullamento o chiusura. Una funzione del catalogo e un tasto selezionato si aprono come bozza non salvata; vengono controllate le associazioni duplicate.

La sovrapposizione della tastiera mostra il layout di riferimento selezionato e le associazioni attive. Tieni premuto Ctrl+Alt+F1 oppure attivala/disattivala dalla tavolozza o dalla pagina Tastiera. Non acquisisce il focus e lascia passare i clic del mouse. I cambiamenti di profilo e associazioni si aggiornano mentre è visibile; le normali pressioni dei tasti Windows sono evidenziate. I comandi specifici del produttore potrebbero non fornire un segnale di tasto Windows equivalente.

Il feedback dell'esecuzione mostra l'attesa, l'avanzamento del passaggio/ripetizione, il completamento, l'annullamento o il fallimento senza focalizzarsi o esporre il testo immesso. Comandi eseguiti conferma l'esecuzione del comando, non uno stato live interrogato in modo indipendente in un'applicazione di terze parti. I dettagli dell'errore vengono visualizzati nel registro delle attività.

## Dispositivi e Corsair

Corsair G1–G6 possono essere assegnati direttamente tramite l'SDK ufficiale iCUE. Seleziona il dispositivo e il tasto nell'editor delle azioni e salva, oppure prepara una bozza dalla vista della tastiera K100. Le associazioni si applicano al dispositivo selezionato e al profilo attivo; la pausa le disabilita anch'esse. iCUE deve essere in esecuzione e consentire l'accesso. Fn, la rotella iCUE e i comandi di profilo e blocco non possono essere mappati tramite questa API G-key.

La pagina Dispositivi elenca le interfacce della tastiera Windows e si aggiorna periodicamente e quando i dispositivi si connettono. Una tastiera fisica può esporre più interfacce HID. I tasti di scelta rapida globali standard funzionano con tutti i produttori.

Il rilevamento Corsair utilizza le informazioni sul dispositivo disponibili. Il controllo RGB richiede hardware compatibile, con iCUE e l'SDK ufficiale iCUE; l'app segnala la disponibilità effettiva. Altri produttori attualmente ricevono rilevamento e azioni standard. Al di fuori dell'integrazione del tasto G di Corsair, i controlli proprietari funzionano quando Windows li riceve come input normale. L'app non installa un driver della tastiera.

## Lingue

Impostazioni offre le lingue 27: tedesco, inglese, francese, spagnolo, italiano, portoghese, olandese, polacco, ceco, slovacco, ungherese, rumeno, bulgaro, greco, danese, svedese, norvegese, finlandese, estone, lettone, lituano, sloveno, croato, ucraino, russo, turco e serbo in caratteri latini. Automatico segue la lingua di visualizzazione Windows. Le traduzioni sono raggruppate localmente. Layout, tasti di scelta rapida e nomi creati dall'utente sono indipendenti dalla lingua dell'interfaccia.

La documentazione completa del repository è disponibile in tutte le lingue di applicazione 27 attraverso il [Indice linguistico](README.md). Le traduzioni sono assistite da macchina, con collegamenti alle fonti originali e controlli strutturali; la revisione madrelingua è separata. L'app accetta anche i codici regionali `en-US`, `de-DE`, `fr-FR` e `it-IT` e usa i relativi formati numerici. I codici linguistici esistenti restano compatibili.

## Layout e ricerca del produttore

La vista della tastiera offre riferimenti ISO-DE/ANSI-US completi e senza tastierino numerico, oltre ai layout Corsair K100 RGB. La geometria rappresenta posizioni, tasti larghi/alti e il tasto Invio ISO. I layout standard sono riferimenti generici. I layout K100 seguono i comandi documentati senza dichiarare dimensioni precise al millimetro. Scegli la corretta variante di lingua/geometria: un nome di modello o un VID/PID USB non determina una variante specifica.

La mappatura risolve i codici di scansione fisici usando il layout di input Windows attivo. Il flusso di lavoro prepara una bozza e mantiene i controlli di salvataggio e conflitto. Le associazioni standard sono tasti di scelta rapida globali Windows, non isolati su una singola tastiera collegata. Corsair G1–G6 usano l'integrazione iCUE specifica del dispositivo. Gli altri comandi proprietari richiedono un adattatore o scorciatoie configurate dal produttore. L'Invio principale e quello del tastierino numerico condividono la stessa identità di tasto virtuale per i tasti di scelta rapida globali.

Il [Ricerca produttore](research/tastaturhersteller-sonderfunktionen.md) e [CSV](research/tastaturhersteller-sonderfunktionen.csv) coprono 74 gruppi di marchi/produttori, distinguendo configuratori, SDK di input e API di illuminazione. [Documentazione dei layout](research/keyboard-layout-evidence.md) documenta le differenze tra modelli, SKU e moduli. La copertura della ricerca non significa che il supporto hardware sia implementato.

## Dati

La configurazione viene archiviata nei dati dell'applicazione locale. Importa/esporta utilizzando JSON. L'importazione visualizza in anteprima la configurazione, sostituisce la configurazione corrente dopo la conferma e disabilita le associazioni importate finché non vengono abilitate manualmente. Il contenuto delle azioni di testo non viene raccolto come registro delle sequenze di tasti. Le password di Credential Manager sono separate dai profili esportati.

## Verifica

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

I controlli Windows possono ispezionare l'hardware e configurare/rilasciare temporaneamente i collegamenti iCUE supportati. Eseguili senza un altro controller utilizzando tali associazioni. I controlli degli endpoint audio non modificano il volume live o le impostazioni di disattivazione dell'audio.

L'input nativo non può aggirare i privilegi Windows. Un controller avviato normalmente potrebbe non riuscire a inviare input a un'applicazione eseguita come amministratore. I tasti di scelta rapida riservati o già occupati appaiono come conflitti.

Il [Indice di documentazione](README.md) rimanda a progetti storici, piani, ricerche e rapporti di test datati. I piani storici descrivono l'ambito registrato all'epoca, non una garanzia attuale di supporto delle funzionalità.

### Modelli di flusso di lavoro e attese della finestra di destinazione

Il catalogo include sei flussi di lavoro modificabili: area di lavoro, riunione, streaming, ricerca, cambio di progetto e snippet di testo. Cerca `workflow.` nel catalogo o nella tavolozza dei comandi. La selezione di un flusso di lavoro apre una bozza non salvata; non viene mai eseguito immediatamente. Tutti i passaggi vengono mantenuti quando si aggiunge un flusso di lavoro a una macro esistente o lo si assegna a un tasto Corsair. Configura URL di esempio, percorsi di programma, profili e scene OBS prima di salvare.

Il passaggio **Attendi finestra di destinazione** accetta un nome di processo (con o senza `.exe`), una sottostringa del titolo senza distinzione tra maiuscole e minuscole e un timeout di 1–600000 ms. È richiesto almeno un filtro; entrambi devono corrispondere se vengono forniti. La macro continua solo dopo che una finestra visibile corrispondente risponde e passa in primo piano. Usa un titolo specifico quando più finestre condividono un processo. L'annullamento o il timeout interrompono i passaggi successivi. Una finestra pronta non garantisce che un documento sia caricato o che un servizio esterno come OBS WebSocket sia connesso.

Luminosità display: cercare **Aumenta luminosità schermo**, **Diminuisci luminosità schermo** o **Luminosità schermo a 50 percentuale** nel catalogo azioni. Le azioni relative modificano la luminosità di 10 punti percentuali; i valori fissi vanno da 0 a 100. I display interni supportati utilizzano WMI; i monitor esterni compatibili richiedono che DDC/CI sia abilitato nel loro menu. Per impostazione predefinita, l'azione influisce su tutti i display supportati; nell'editor è possibile selezionare un monitor esplicito. Connessioni e dock possono limitare il supporto.

### Controlli estesi

Apri la tavolozza dei comandi con **Ctrl+K mentre la finestra dell'app ha il focus**. Cerca **Progettazione flusso di lavoro**, **Registra macro**, **Sincronizza profili** o **Diagnostica tasti** (il testo dipende dalla lingua selezionata). Il progettista modifica la bozza del riquadro selezionato: condizioni di file/processo/finestra, numero di passaggi da saltare in avanti e interruzione/continuazione in caso di errore. Il registratore acquisisce combinazioni di tasti di scelta rapida e attese, non la normale digitazione; Escape interrompe la registrazione. Esamina il risultato prima di adottarlo.

In un passaggio relativo alla luminosità, seleziona tutti i display supportati o un monitor specifico. Un monitor selezionato non disponibile produce un errore anziché modificare silenziosamente un altro display. La dashboard mostra la luminosità in tempo reale, l'audio e le letture OBS quando sono disponibili le rispettive integrazioni; una lettura non disponibile non viene presentata come valore misurato.

Nell'editor dei riquadri, assegna un tasto speciale fisico supportato e scegli pressione immediata, tocco, pressione prolungata o doppio tocco. I selettori di livello tenuti premuti attivano temporaneamente un'altra mappatura e ripristinano il livello precedente al rilascio. Gesti e livelli si applicano ai tasti G del Corsair SDK e ai tasti speciali standard specifici del dispositivo, non ai normali tasti di scelta rapida globali. L'acquisizione dei tasti standard supporta F13–F24 e i tasti multimediali. I comandi proprietari Fn, profilo e rotella richiedono ancora un adattatore compatibile del produttore o una mappatura del produttore su un tasto standard. Raw Input osserva il tasto e non ne sopprime il normale comportamento Windows. La diagnostica dei tasti spiega problemi di profilo, pausa, connessione e associazione.

La sincronizzazione del profilo scambia i file attraverso una cartella scelta da te, inclusa una cartella gestita dal tuo client cloud esistente. Esaminare l'anteprima prima dell'importazione; le importazioni iniziano disabilitate e rimuovono le mappature specifiche dell'hardware. Le revisioni impediscono le sovrascritture silenziose e l'inclusione dei contenuti delle azioni è una scelta esplicita. MAKRIVO non fornisce un account cloud né conserva le credenziali cloud. Il vassoio ora utilizza l'icona M verde acqua di MAKRIVO e mostra un badge di pausa color ambra quando è in pausa.

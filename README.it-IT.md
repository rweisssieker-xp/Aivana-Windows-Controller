# MAKRIVO

**Di Aivana.** [Funzionalità e limiti attuali (DE)](docs/FEATURES.md) · [Test delle estensioni (DE)](docs/testing/2026-10-12-six-extensions.md)

[English (US)](README.md) | [Deutsch](README.de-DE.md) | [Français](README.fr-FR.md) | **Italiano**

Controller locale per hotkey Windows con riquadri in stile Stream Deck, profili, macro e rilevamento delle tastiere. L'interfaccia supporta 27 lingue. Per l'uso quotidiano non servono account né connessione cloud.

[Documentazione](docs/it-IT/README.md) · [Verifica](docs/it-IT/VERIFICATION.md) · [Rapporto sui test delle funzioni](docs/it-IT/testing/2026-10-09-usp-release.md)

## Per iniziare

Dopo la pubblicazione, l'applicazione si trova in `artifacts/win-x64/MAKRIVO.exe`. Mantieni insieme l'intera cartella di output; puoi copiarla altrove. Windows 11 x64 è il sistema iniziale di destinazione. Gli artefatti di build sono esclusi da Git; un checkout del codice sorgente non include un eseguibile precompilato.

Installa l'SDK .NET 10 indicato in `global.json` per compilare ed eseguire dal codice sorgente:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Pubblica una versione autonoma che non richieda un runtime .NET installato separatamente:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Flusso di base

1. Seleziona un profilo e crea un riquadro azione.
2. Imposta nome, icona, colore e scorciatoia da tastiera. Le combinazioni con Ctrl/Alt/Maiusc riducono le attivazioni accidentali durante la digitazione.
3. Aggiungi uno o più passaggi: applicazioni, file, siti web, testo, scorciatoie, contenuti multimediali, volume, cambio profilo o pause.
4. Salva. Le modifiche restano in bozza finché non vengono salvate; scartandole si ripristina la versione salvata. Il pulsante Play del riquadro e il relativo hotkey usano lo stesso percorso di esecuzione.
5. Riordina riquadri e passaggi macro trascinandoli o usando i pulsanti freccia. I modelli semplificano la configurazione. Le macro possono ripetersi fino a 100 volte. Puoi annullarle dall'interfaccia, dal menu dell'area di notifica o, per impostazione predefinita, con Ctrl+Alt+Escape.

I profili specifici per applicazione usano il nome del processo senza il percorso completo. In caso di parità decide l'ordine dei profili; il profilo predefinito è quello di riserva. La selezione automatica dei profili va attivata nelle Impostazioni.

Chiudendo la finestra principale, l'app resta per impostazione predefinita nell'area di notifica. Fai doppio clic sulla relativa icona per riaprirla; usa il menu dell'area di notifica per uscire. L'avvio con Windows è facoltativo e disattivato per impostazione predefinita. Gli hotkey si possono sospendere centralmente.

## Azioni e tavolozza dei comandi

«Aggiungi azione» apre un catalogo ricercabile con 90 preimpostazioni in otto categorie. Ognuna include descrizione e anteprima. L'editor macro usa lo stesso catalogo per aggiungere o sostituire passaggi. In questo flusso, selezionare una preimpostazione nell'editor crea una bozza senza eseguirla.

La tavolozza dei comandi si apre dall'intestazione oppure con Ctrl+K nell'app. Cerca le azioni salvate nei profili, le preimpostazioni del catalogo, il cambio profilo, la navigazione, pausa/ripresa, l'annullamento delle macro e import/export. Puoi filtrare per azioni salvate, funzioni di catalogo o comandi dell'app. Le frecce selezionano, Invio attiva, Ctrl+Invio modifica una voce modificabile ed Esc chiude. I preferiti appaiono per primi. I comandi di catalogo pronti all'uso si possono eseguire subito; le preimpostazioni che richiedono configurazione aprono una bozza. Nelle Impostazioni puoi definire una scorciatoia globale per la tavolozza. Le nuove configurazioni usano Ctrl+Shift+Space; quelle esistenti non ricevono automaticamente un nuovo hotkey globale.

Le azioni audio impostano il volume con precisione da 0 a 100% e silenziano, riattivano o alternano l'uscita audio e i dispositivi microfono. Scegli un endpoint specifico oppure lascialo vuoto per usare quello predefinito di Windows. Se un endpoint salvato non è disponibile, viene mostrato un errore invece di controllare un altro dispositivo. «Aggiorna dispositivi» ricarica quelli collegati.

OBS Studio usa WebSocket v5: avvia/interrompi/alternare/metti in pausa/riprendi la registrazione; avvia/interrompi/alternare lo streaming; cambia scena; alterna il silenziamento dell'ingresso. Attiva il server WebSocket da Strumenti in OBS e imposta l'endpoint nell'editor. «Controlla connessione e carica nomi» legge versione OBS, scene e ingressi senza avviare registrazioni o streaming. Scegli i nomi dall'elenco o inseriscili manualmente. Le password possono essere salvate in Gestione credenziali di Windows e sono escluse dalle configurazioni esportate. Le connessioni OBS non disponibili generano un errore.

I profili possono corrispondere al nome di un processo e a una parte del titolo della finestra. Se sono indicate entrambe le condizioni, devono corrispondere entrambe. «Blocca profilo» impedisce il cambio automatico per la sessione. «Acquisisci condizioni dalla finestra in primo piano» usa l'ultima applicazione esterna con focus.

Le azioni di testo e appunti supportano `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. Le date usano `yyyy-MM-dd`, gli orari `HH:mm`. I segnaposto sconosciuti restano invariati. `{{profile}}` identifica il profilo dell'azione avviata; `{{process}}` identifica il contesto dell'applicazione esterna. Il contenuto degli appunti viene letto solo se si usa il relativo segnaposto. L'azione appunti scrive testo, ma non lo incolla automaticamente.

Le azioni finestra riducono a icona, ingrandiscono, ripristinano o dispongono una finestra a sinistra/destra nell'area di lavoro del monitor. L'azione di sistema blocca Windows. Queste azioni vengono eseguite solo quando le richiami esplicitamente.

L'anteprima mostra passaggi, ripetizioni e variabili senza eseguirli. Annulla e Ripeti conservano fino a 100 stati della bozza; le caselle di testo mantengono la modifica nativa Ctrl+Z. Puoi impostare i preferiti nell'editor o nel menu del riquadro e filtrarli nel dashboard. I conteggi di esecuzione valgono solo per la sessione corrente.

Le azioni di testo, scorciatoia e finestra avviate dall'interfaccia attivano l'ultima finestra di destinazione esterna. La tavolozza mostra il contesto di destinazione. Se la finestra è stata chiusa o Windows rifiuta di attivarla, l'esecuzione si arresta e mostra un messaggio.

## Assistenza tastiera e feedback

L'assistente per i tasti speciali si trova nella pagina Tastiera e nella tavolozza. Elenca i tasti G segnalati da iCUE. «Rileva pressione tasto» li acquisisce temporaneamente senza eseguire le azioni assegnate. Le associazioni normali vengono ripristinate dopo il rilevamento, l'annullamento o la chiusura. Una funzione del catalogo e il tasto selezionato aprono una bozza non salvata; vengono controllate le associazioni duplicate.

L'overlay tastiera mostra il layout di riferimento scelto e le associazioni attive. Tieni premuti Ctrl+Alt+F1 oppure attivalo/disattivalo dalla tavolozza o dalla pagina Tastiera. Non riceve il focus e lascia passare i clic del mouse. Le modifiche di profilo e associazione si aggiornano mentre l'overlay è visibile; vengono evidenziate le normali pressioni dei tasti Windows. I controlli specifici del produttore potrebbero non inviare un segnale equivalente a Windows.

Il feedback dell'esecuzione mostra attesa, avanzamento dei passaggi/ripetizioni, completamento, annullamento o errore senza ricevere il focus né rivelare il testo inserito. «Comandi eseguiti» conferma l'esecuzione del comando, non uno stato attuale interrogato indipendentemente in un'applicazione di terze parti. I dettagli degli errori compaiono nel registro attività.

## Dispositivi e Corsair

Corsair G1–G6 si possono assegnare direttamente tramite l'SDK ufficiale iCUE. Seleziona il dispositivo e il tasto nell'editor di azioni e salva, oppure crea una bozza dalla vista tastiera K100. Le associazioni valgono per il dispositivo selezionato e il profilo attivo; la pausa disattiva anche questi trigger. iCUE deve essere in esecuzione e consentire l'accesso. Fn, la rotella iCUE, Profilo e Blocco non possono essere mappati tramite questa API dei tasti G.

La pagina Dispositivi elenca le interfacce tastiera Windows e si aggiorna periodicamente e quando si collegano dispositivi. Una tastiera fisica può esporre più interfacce HID. Gli hotkey globali standard funzionano con tutti i produttori.

Il rilevamento Corsair usa le informazioni disponibili sul dispositivo. Il controllo RGB richiede hardware compatibile, iCUE in esecuzione e l'SDK ufficiale iCUE; l'app mostra la disponibilità effettiva. Per gli altri produttori sono disponibili il rilevamento e le azioni standard. Al di fuori dell'integrazione dei tasti G Corsair, i controlli proprietari funzionano quando Windows li riceve come input normali. L'app non installa un driver tastiera.

## Lingue

Le Impostazioni offrono 27 lingue: tedesco, inglese, francese, spagnolo, italiano, portoghese, olandese, polacco, ceco, slovacco, ungherese, rumeno, bulgaro, greco, danese, svedese, norvegese, finlandese, estone, lettone, lituano, sloveno, croato, ucraino, russo, turco e serbo in alfabeto latino. «Automatica» segue la lingua di visualizzazione di Windows. Le traduzioni sono incluse localmente. Layout, hotkey e nomi creati dall'utente sono indipendenti dalla lingua dell'interfaccia.

La documentazione del repository è disponibile in inglese statunitense (`en-US`), tedesco (`de-DE`), francese (`fr-FR`) e italiano (`it-IT`). L'app accetta anche questi quattro codici regionali e usa i relativi formati numerici. I codici lingua esistenti restano compatibili.

## Layout e ricerca sui produttori

La vista Tastiera offre layout di riferimento ISO-DE/ANSI-US a formato completo e TKL, oltre ai layout Corsair K100 RGB. La geometria rappresenta posizioni, tasti larghi/alti e Invio ISO. I layout standard sono riferimenti generici. I layout K100 seguono i controlli documentati senza dichiarare misure precise al millimetro. Scegli la variante corretta di lingua/geometria: il nome del modello o VID/PID USB non dimostra una variante specifica.

L'associazione risolve gli scancode fisici usando il layout di input Windows attivo. Il flusso prepara una bozza e mantiene i controlli di salvataggio/conflitto. Le associazioni standard sono hotkey globali di Windows e non sono limitate a una tastiera collegata. Corsair G1–G6 usano l'integrazione iCUE specifica per dispositivo. Gli altri controlli proprietari richiedono un adattatore o scorciatoie configurate dal produttore. Invio principale e Invio del tastierino numerico condividono lo stesso identificatore di tasto virtuale per gli hotkey globali.

La [ricerca sui produttori](docs/it-IT/research/tastaturhersteller-sonderfunktionen.md) e il relativo [CSV](docs/it-IT/research/tastaturhersteller-sonderfunktionen.csv) coprono 74 gruppi di marchi/produttori e distinguono configuratori, SDK di input e API per l'illuminazione. Le [prove dei layout](docs/it-IT/research/keyboard-layout-evidence.md) registrano differenze di modello, SKU e moduli. La copertura della ricerca non significa che il supporto hardware sia già implementato.

## Dati

La configurazione è salvata nei dati locali dell'applicazione. Import/export usano JSON. L'importazione mostra un'anteprima, sostituisce la configurazione corrente dopo la conferma e disattiva le associazioni importate finché non vengono abilitate manualmente. Il testo delle azioni non viene raccolto come registro delle pressioni dei tasti. Le password in Gestione credenziali sono separate dai profili esportati.

## Verifica

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

I controlli Windows possono esaminare l'hardware e configurare/rilasciare temporaneamente le associazioni iCUE supportate. Eseguirli senza un altro controller che usi tali associazioni. I controlli degli endpoint audio non modificano volume o stato di silenziamento effettivi.

L'input nativo non può aggirare i privilegi di Windows. Un controller avviato normalmente potrebbe non inviare input a un'applicazione amministratore. Gli hotkey riservati o già occupati vengono segnalati come conflitti.

L'[indice della documentazione](docs/it-IT/README.md) collega design, piani, ricerche e rapporti di test storici datati. I piani storici descrivono l'ambito registrato all'epoca, non una garanzia attuale di supporto delle funzioni.

### Modelli di workflow e attesa della finestra di destinazione

Il catalogo include sei workflow modificabili: spazio di lavoro, riunione, streaming, ricerca, cambio progetto e testo predefinito. Cerca `workflow.` nel catalogo o nella palette dei comandi. La selezione apre una bozza non salvata senza eseguire azioni. Tutti i passaggi vengono mantenuti quando si aggiunge un workflow a una macro o lo si assegna a un tasto Corsair. Personalizza URL, percorsi, profili e scene OBS prima di salvare.

**Attendi la finestra di destinazione** accetta un processo con o senza `.exe`, una parte del titolo senza distinzione tra maiuscole e minuscole e un timeout da 1 a 600.000 ms. È richiesto almeno un filtro; se presenti entrambi, devono corrispondere entrambi. La macro continua quando la finestra corrispondente è visibile, risponde ed è in primo piano. Specifica il titolo se più finestre condividono lo stesso processo. Annullamento o timeout interrompono i passaggi successivi. Una finestra pronta non garantisce il caricamento del documento o la connessione a servizi esterni come OBS WebSocket.

Luminosità: cercare **Aumenta luminosità dello schermo**, **Riduci luminosità dello schermo** o **Luminosità dello schermo al 50 per cento** nel catalogo. Le azioni relative modificano la luminosità di 10 punti percentuali; i valori fissi vanno da 0 a 100. Gli schermi interni compatibili utilizzano WMI; i monitor esterni compatibili richiedono DDC/CI attivato nel menu. Per impostazione predefinita, l’azione riguarda tutti gli schermi compatibili; nell’editor è possibile selezionare un monitor specifico. Collegamenti e dock possono limitare il supporto.

### Controlli avanzati

Apri la palette con **Ctrl+K quando la finestra dell’app ha il focus**. Cerca l’**editor dei flussi**, la **registrazione macro**, la **sincronizzazione dei profili** o la **diagnostica dei tasti**; le etichette seguono la lingua scelta. L’editor modifica la bozza della tessera selezionata: condizioni su file/processi/finestre, salti in avanti e arresto o continuazione in caso di errore. Il registratore acquisisce scorciatoie e attese, non testo normale; Esc termina la registrazione. Esamina il risultato prima di adottarlo.

Nel passo di luminosità scegli tutti i display supportati o un monitor specifico. Una destinazione selezionata non disponibile produce un errore invece di modificare un altro display. La dashboard mostra luminosità, audio e OBS quando le relative integrazioni sono disponibili; una lettura assente non viene presentata come misura.

Nell’editor della tessera assegna un tasto speciale fisico supportato e scegli pressione immediata, breve, prolungata o doppia. Un selettore di livello mantenuto attiva temporaneamente altre assegnazioni; il rilascio ripristina il livello precedente. Gesti e livelli si applicano ai tasti G del SDK Corsair e ai tasti standard per dispositivo, non alle normali scorciatoie globali. Il rilevamento standard supporta F13–F24 e i tasti multimediali della tastiera. I comandi proprietari Fn, profilo e rotella richiedono ancora un adattatore compatibile del produttore o un’assegnazione a un tasto standard. Raw Input osserva senza sopprimere il normale comportamento di Windows. La diagnostica spiega problemi di profilo, pausa, connessione e assegnazione.

La sincronizzazione scambia file tramite una cartella scelta, anche gestita da un client cloud già esistente. Esamina l’anteprima prima dell’importazione: le azioni importate sono disattivate e le assegnazioni hardware vengono rimosse. Le revisioni impediscono sovrascritture silenziose; includere i contenuti delle azioni è una scelta esplicita. MAKRIVO non fornisce account cloud e non conserva credenziali cloud. L’area di notifica usa ora l’icona M turchese propria, con indicatore di pausa ambra.

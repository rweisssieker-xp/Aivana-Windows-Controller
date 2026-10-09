# Integrazione con OBS Studio

[English (US)](../../en-US/research/obs-integration.md) · [Deutsch](../../de-DE/research/obs-integration.md) · [Français](../../fr-FR/research/obs-integration.md) · [Italiano](../../it-IT/research/obs-integration.md)

Aivana usa il protocollo obs-websocket v5 di OBS Studio tramite ClientWebSocket; non installa una libreria client OBS di terze parti. Il protocollo inizia con Hello del server (opcode 0), risponde con Identify (opcode 1), attende Identified (opcode 2), invia una richiesta consentita (opcode 6), quindi abbina la relativa RequestResponse (opcode 7) tramite l'ID della richiesta. L'autenticazione con password segue il calcolo challenge in due passaggi SHA-256 documentato da OBS.

In OBS, attivare **Strumenti → Impostazioni server WebSocket**. L'endpoint predefinito è **ws://127.0.0.1:4455**. Per un endpoint diverso, impostare il campo dell'azione **Indirizzo OBS-WebSocket**. Un endpoint remoto deve usare wss://; ws:// non crittografato è accettato solo per un indirizzo di loopback. L'endpoint può includere anche un percorso o una porta personalizzata. Aivana rifiuta le credenziali incorporate nell'URL.

Se OBS richiede l'autenticazione, salvare la password nell'editor dell'azione OBS. Aivana la memorizza in Gestione credenziali di Windows per l'utente corrente, come credenziale generica denominata **Aivana.OBS:<endpoint normalizzato>**. Non viene mai scritta in un'azione o in un file delle impostazioni. Per le distribuzioni gestite, **AIVANA_OBS_PASSWORD** resta disponibile come fallback quando l'endpoint non ha una password salvata. OBS può anche rimanere senza autenticazione; in quel caso non serve né l'una né l'altra. Evitare di passare la password negli argomenti della riga di comando, perché i comandi dei processi possono essere ispezionati.

Il catalogo del controller e la configurazione supportano **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** e **ToggleInputMute**. Le prime otto non accettano argomenti. SetCurrentProgramScene usa il nome della scena indicato nell'editor; ToggleInputMute usa il nome dell'ingresso. Il client del protocollo accetta la stessa lista consentita.

Ogni azione apre una nuova connessione con un limite di 10 secondi e senza tentativi automatici. Questo è importante per i comandi di alternanza: dopo l'invio della richiesta, un timeout o una connessione interrotta possono lasciare ignoto il risultato; un nuovo tentativo potrebbe alternare di nuovo lo stato. Gli errori delle richieste OBS vengono restituiti con il relativo codice di stato e, se presente, il commento del server.

Riferimenti al protocollo:

- [Specifiche del protocollo OBS WebSocket v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [Formula di autenticazione OBS WebSocket e codici opcode dei messaggi](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [Implementazione delle richieste per gli ingressi multimediali OBS WebSocket e valori enum delle azioni accettati](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Le note sul protocollo e i nomi delle operazioni sono stati verificati rispetto alla documentazione del progetto OBS l'8 ottobre 2026.

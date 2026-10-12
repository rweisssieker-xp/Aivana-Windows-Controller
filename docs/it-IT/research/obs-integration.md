<!-- generated-doc locale=it-IT source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../research/obs-integration.md)

# OBS Integrazione Studio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana utilizza il protocollo OBS Studio obs-websocket v5 su ClientWebSocket; non installa una libreria client OBS di terze parti. Il protocollo si apre con il server Hello (codice operativo 0), risponde con Identifica (codice operativo 1), attende Identificato (codice operativo 2), invia una richiesta inserita nella whitelist (codice operativo 6), quindi abbina la sua RequestResponse (codice operativo 7) in base all'ID della richiesta. L'autenticazione della password segue il calcolo della sfida SHA-256 documentato in due fasi di OBS.

Abilita **Strumenti → WebSocket Impostazioni server** in OBS. L'endpoint predefinito è **ws://127.0.0.1:4455**. Imposta il campo **OBS-WebSocket-Adresse** per un altro endpoint. Un endpoint remoto deve utilizzare wss://; ws:// non crittografato è accettato solo per un indirizzo di loopback. L'endpoint può anche includere un percorso o una porta personalizzata. Aivana rifiuta le credenziali URL incorporate.

Quando OBS ha l'autenticazione abilitata, salva la sua password dall'editor di azioni OBS. Aivana lo memorizza nel gestore delle credenziali dell'utente corrente Windows come credenziale generica denominata **Aivana.OBS:<endpoint normalizzato>**. Non viene mai scritto in un file di azioni o impostazioni. Per le distribuzioni gestite, **AIVANA_OBS_PASSWORD** rimane disponibile come fallback quando l'endpoint non ha una password salvata. OBS potrebbe rimanere non autenticato, nel qual caso non è necessario nessuno dei due. Evitare argomenti della riga di comando per la password perché le righe di comando del processo possono essere ispezionate.

Il catalogo del controller e il supporto di configurazione **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **RiprendiRecord**, **StartStream**, **StopStream**, **ToggleStream**, **Imposta scenaprogramma corrente** e **Attiva/disattiva inputMuto**. I primi otto non accettano argomenti. SetCurrentProgramScene utilizza il nome della scena dell'editor; ToggleInputMute utilizza il nome di input. Il client del protocollo accetta la stessa lista consentita.

Ogni azione ottiene una nuova connessione con un limite di 10 secondi e nessun nuovo tentativo automatico. Questo è importante per i comandi di attivazione/disattivazione: dopo che la richiesta è stata inviata, un timeout o una connessione interrotta può lasciare il risultato sconosciuto e un nuovo tentativo potrebbe modificare nuovamente lo stato. OBS richieste non riuscite vengono restituite con il relativo codice di stato e (se fornito) il commento del server.

Riferimenti al protocollo:

- [OBS WebSocket specifica del protocollo](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket formula di autenticazione e codice di messaggio](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket Implementazione della richiesta di input dei media e valori di azione accettati](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Le note del protocollo e i nomi delle operazioni sono stati confrontati con la documentazione del progetto OBS su 2026-10-08.

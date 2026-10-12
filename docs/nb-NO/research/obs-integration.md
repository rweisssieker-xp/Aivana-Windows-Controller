<!-- generated-doc locale=nb-NO source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../research/obs-integration.md)

# OBS Studiet integrasjon

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana bruker OBS Studio obs-websocket v5 protokoll over ClientWebSocket; det installerer ikke en tredjeparts OBS klientbibliotek. Protokollen åpner med serveren Hello (opcode 0), svarer med Identify (opcode1), venter på Identified (opcode] 2), sender en hvitlistenkt forespørsel (opcode]6), og matcher etterspørselen (opcode)7 ved forespørselsID. Passord autentisering følger OBS dokumentert to-stage SHA-256 utfordring beregning.

Muligheten **Verktøy → WebSocket Server innstillinger** I OBS. Den defekte slutten er **WS://127.0.0.1:4455**. Sett opp handlingene **OBS-WebSocket-Adressen** Et fjernt sluttpoeng må bruke wss://; unencrypted ws:// er bare akseptert for en loopback adresse.. Aivana Avviser innebygde URL-krediteringer.

Når OBS har autentisering aktivert, lagre passordet fra OBS Handlingsredaktør. Aivana Lagre det i dagens Windows Brukerens Credential Manager som generisk credential **Aivana.OBS:<Normalisert endpoint>**. Det er aldri skrevet til en handling eller innstillinger fil., **AIVANA_OBS_PASSWORD** forblir tilgjengelig som en fallback når slutten ikke har spart passord. OBS kan forbli uautorisert, i hvilket tilfelle det ikke er nødvendig. unngå kommando-linje argumenter for passordet fordi prosess kommando linjer kan sjekkes.

Kontroller katalog og konfigurasjon støtte **Startrekord**, **Stoprekord**, **Toggrekord**, **Pauserekord**, **Resumé**, **Startstrøm**, **Stoppstrøm**, **ToggleStream**, **Oppdateringsprogrammet**, og **ToggleInputMute**. De første åtte tar ingen argumenter. SetCurrentProgramScene bruker redaktørens sceneavn; ToggleInputMute bruker sin inntjeningsnavn..

Hver handling får en ny forbindelse med en 10-sekund og ingen automatisk tilbakemelding. Dette gjelder for toggle-kommandoer: etter at forespørselen er sendt, kan en timeout eller nedsatt tilkobling forlate resultatet ukjent, og tilbaketrekking kan toggle tilstanden igjen. OBS forespørselsfeil returneres med statuskoden og (når gitt) serverkommentar.

Referanser til protokoll:

- [OBS WebSocket v5 protokoll spesifikasjon](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentisering formel og melding opkoder](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket implementering av medieinnførsel forespørsel og akseptert handling enum verdier](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokoll notater og operasjonsnavn ble sjekket mot prosjektdokumentasjonen OBS på 2026-10-08.

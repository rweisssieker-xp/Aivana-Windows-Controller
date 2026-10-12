<!-- generated-doc locale=da-DK source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../research/obs-integration.md)

# OBS Studiet integration

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana bruger OBS Studio obs-websocket v5 protokol over ClientWebSocket; det installerer ikke en tredjeparts OBS klientbibliotek. Protokollen åbner med serveren Hello (opcode 0), svarer med Identify (opcode1), venter på Identified (opcode] 2), sender en hvidlistet anmodning (opcode]6), og efterfølgende matcher dens RequestResponse (opcode)7) ved anmodning ID. Password autentisering følger OBS dokumenterede to-stage SHA-256 udfordring beregning.

Mulighed for **Værktøjer → WebSocket Serverindstillinger** I OBS. Det ultimative punkt er **WS://127.0.0.1:4455**. Sæt aktionen **OBS-WebSocket-Adressen** et fjernt slutpunkt skal bruge wss://; unencrypted ws:// accepteres kun for en loopback adresse.. Aivana Afviser indbygget URL-kreditering.

Når OBS har autentisering aktiveret, gemmer din adgangskode fra OBS Aktionsredaktør. Aivana Sæt det i nutidens Windows brugerens Credential Manager som en generisk credential **Aivana.OBS:<Normaliseret slutpunkt>**. Det er aldrig skrevet til en handling eller indstillinger fil., **AIVANA_OBS_PASSWORD** er tilgængelig som en fallback, når slutpunktet ikke har et gemt adgangskode. OBS kan forblive uautoriseret, hvor det ikke er nødvendigt. Undgå kommandolinjeargumenter for passordet, fordi processen kommandolinjer kan kontrolleres.

Kontroller katalog og konfiguration støtte **Startindtægter**, **Stoppekort**, **Toggregor**, **Pauseindhold**, **Resumé**, **Startstrøm**, **Stopstrøm**, **Togglestrøm**, **Aktuelt program**, og **ToggleInputMøde**. De første otte tager ingen argumenter. SetCurrentProgramScene bruger redaktørens scene navn; ToggleInputMute bruger dens input navn..

Hver handling får en frisk forbindelse med en 10-sekund og ingen automatisk tilbagetrækning. Dette gælder for toggle-kommandoer: Efter anmodningen sendes, kan en timeout eller tilbagevendende forbindelse forlade resultatet ukendt, og tilbagevenden kan toggle tilstanden igen. OBS anmodning fejler returneres med deres status kode og (hvis givet) server kommentar.

Referencer fra protokollen:

- [OBS WebSocket v5 protokol specifikation](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentisering formel og besked opcoder](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket implementering af medieindtægtsforespørgsel og accepterede handlinger](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokolnoter og operationsnavn er blevet kontrolleret mod OBS Projektdokumentation på 2026-10-08.

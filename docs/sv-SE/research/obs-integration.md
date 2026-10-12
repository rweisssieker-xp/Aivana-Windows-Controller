<!-- generated-doc locale=sv-SE source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../research/obs-integration.md)

# OBS Studien integration

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana använder OBS Studio obs-websocket v5 protokoll över ClientWebSocket; det installerar inte en tredje parts OBS klientbibliotek. Protokollet öppnas med server Hello (opcode 0), svarar med Identify (opcode] 1, väntar på Identified (opcode2), skickar en utvärderad begäran (opcode]6), matchar sedan sin RequestResponse (opcode)7) genom begäran ID. Password autentisering följer OBS dokumenterad tvåsteg SHA-256 utmaning beräkning.

Möjlig **Verktyg → WebSocket Serverinställningar** i OBS. Det slutliga defektpunkten är **Vs://127.0.0.1:4455**. Ställ in åtgärden **OBS-WebSocket-Adressen** Ett fjärrändpunkt måste använda wss://; okrypterad ws:// accepteras endast för en loopbackadress.. Aivana Avvisar inbyggda URL-krediteringar.

När OBS har autentisering aktiverad, spara ditt lösenord från OBS Aktionsredaktör. Aivana Förvara den i nuet Windows användarens Credential Manager som generisk credential **Aivana.OBS:<Normaliserad slutpunkt>**. Det är aldrig skrivet till en åtgärd eller inställningsfil., **AIVANA_OBS_PASSWORD** förblir tillgänglig som fallback när slutpunkten inte har ett sparat lösenord. OBS kan förbli okända, i vilket fall det inte behövs. Undvik kommandolinjeargument för lösenordet eftersom process kommandolinjer kan kontrolleras.

Kontroller katalog och konfigurationsstöd **StartRekord**, **Stoppreglering**, **ToggleRekord**, **PauseRekord**, **Resumérecord**, **Startström**, **Stoppström**, **ToggleStream**, **Aktuellt program**, och **ToggleInputMätt**. De första åtta tar inga argument. SetCurrentProgramScene använder redaktörens scennamn; ToggleInputMute använder sitt inmatningnamn..

Varje åtgärd får en färsk anslutning med en 10-sekund gräns och ingen automatisk återgång. Detta gäller för toggle-kommandon: efter att begäran har skickats kan en timout eller nedsatt anslutning lämna resultatet okänt, och återkallelse kan toggle tillståndet igen. OBS begäran misslyckanden returneras med deras statuskod och (när) serverkommentar.

Referenser från protokollet:

- [OBS WebSocket v5 protokollspecifikation](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentisering formel och meddelande opkoder](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket media input begäran genomförande och accepterade åtgärder enum värden](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokollnoterna och operationsnamnen kontrollerades mot OBS projektdokumentationen på 2026-10-08.

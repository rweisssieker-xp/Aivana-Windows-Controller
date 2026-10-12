<!-- generated-doc locale=nl-NL source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../research/obs-integration.md)

# OBS Studio-integratie

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana gebruikt het OBS Studio obs-websocket v5-protocol via ClientWebSocket; er wordt geen OBS-clientbibliotheek van derden geïnstalleerd. Het protocol wordt geopend met de server Hello (opcode 0), antwoordt met Identificeren (opcode 1), wacht op Identified (opcode 2), verzendt een verzoek op de witte lijst (opcode 6) en matcht vervolgens zijn RequestResponse (opcode 7) op verzoek-ID. Wachtwoordverificatie volgt OBS's gedocumenteerde tweetraps SHA-256 uitdagingsberekening.

Schakel **Tools → WebSocket Serverinstellingen** in OBS in. Het standaardeindpunt is **ws://127.0.0.1:4455**. Stel het veld **OBS-WebSocket-Adresse** van de actie in voor een ander eindpunt. Een extern eindpunt moet wss:// gebruiken; niet-versleutelde ws:// wordt alleen geaccepteerd voor een loopback-adres. Het eindpunt kan ook een pad of aangepaste poort bevatten. Aivana weigert ingebedde URL-gegevens.

Wanneer voor OBS authenticatie is ingeschakeld, slaat u het wachtwoord op via de actie-editor OBS. Aivana slaat deze op in de Credential Manager van de huidige Windows-gebruiker als een algemene referentie met de naam **Aivana.OBS:<genormaliseerd eindpunt>**. Het wordt nooit naar een actie- of instellingenbestand geschreven. Voor beheerde implementaties blijft **AIVANA_OBS_PASSWORD** beschikbaar als reserve wanneer het eindpunt geen opgeslagen wachtwoord heeft. OBS blijft mogelijk niet-geverifieerd, in welk geval geen van beide nodig is. Vermijd opdrachtregelargumenten voor het wachtwoord, omdat procesopdrachtregels kunnen worden geïnspecteerd.

De controllercatalogus en configuratie ondersteunen **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** en **ToggleInputMute**. De eerste acht hebben geen argumenten. SetCurrentProgramScene gebruikt de scènenaam van de editor; ToggleInputMute gebruikt de invoernaam. De protocolclient accepteert dezelfde toelatingslijst.

Elke actie krijgt een nieuwe verbinding met een 10-secondengrens en geen automatische nieuwe poging. Dit is van belang voor schakelopdrachten: nadat het verzoek is verzonden, kan een time-out of verbroken verbinding het resultaat onbekend laten, en als u het opnieuw probeert, kan de status opnieuw worden gewijzigd. OBS-verzoekfouten worden geretourneerd met hun statuscode en (indien opgegeven) servercommentaar.

Protocolreferenties:

- [OBS WebSocket V5 protocol specificatie](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket authenticatie formule en boodschap opcodes](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket Media input aanvraag implementatie en geaccepteerde actie enum waarden](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

De protocolaantekeningen en bewerkingsnamen zijn gecontroleerd aan de hand van de OBS-projectdocumentatie op 2026-10-08.

<!-- generated-doc locale=fi-FI source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../research/obs-integration.md)

# OBS Integrointi Studio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana käyttää OBS Studio obs-websocket v5 -protokollaa ClientWebSocketin kautta; se ei asenna kolmansien osapuolten OBS -asiakaskirjastoa. Protokolla avautuu palvelimella Hello (opcode 0), vastaa Identify (opcode1), odottaa Identified (opcode] 2), lähettää valkoisen pyynnön (opcode]6), ja vastaa sen RequestResponse (opcode)7 pyynnön ID. salasanan todentaminen seuraa OBS dokumentoitua kahden vaiheen SHA-256 haasteen laskemista.

mahdollistaa **Työkalut → WebSocket Palvelin asetukset** Sisään OBS. Epäonnistumisen loppupiste on **WS:n://127.0.0.1:4455**. Aseta toiminta **OBS-WebSocket-osoite** kenttä toiseen loppupisteeseen. kaukaisen loppupisteen on käytettävä wss://; salaamaton ws:// hyväksytään vain loopback-osoitteelle. loppupiste voi myös sisältää reitin tai räätälöity portti. Aivana Kieltäytyy sisäänrakennetuista URL-tunnisteista.

Kun OBS on autenttinen, tallentaa salasansa OBS Toiminnan toimittaja. Aivana Säilytä se nykyisessä Windows Käyttäjän luottamusjohtajan nimi on geneerinen **Aivana.OBS:<Normaali loppupiste>**. Se ei ole koskaan kirjoitettu toiminnalle tai asetuksille., **AIVANA_OBS_PASSWORD** pysyy käytettävissä palautuksena, kun loppupisteellä ei ole salattuja salasanoja. OBS voi pysyä epäasianmukaisena, jolloin sitä ei tarvita. vältä salasanan komentoja, koska käsittelyn komentoja voidaan tarkastaa.

Ohjaajan katalogi ja konfiguraatio tuki **Alkuperäinen**, **Pysähdys**, **ToggleRekord**, **Pausrekisteri**, **ResumeMuokkaa**, **Aloitus**, **StopStream**, **ToggleStream**, **Käytännön ohjelma**, ja **Yhteenveto**. Ensimmäiset kahdeksan ei ota väitteitä. SetCurrentProgramScene käyttää toimittajan kohtauksen nimeä; ToggleInputMute käyttää tulosnimiään..

Jokainen toimenpide saa tuoreen yhteyden 10 sekunnilla eikä automaattista peruuttamista. Tämä koskee pyyntöjä: pyynnön lähettämisen jälkeen aikarajoitus tai poistettu yhteys voi jättää tuloksen tuntemattomaksi, ja peruuttaminen voi palauttaa tilan uudelleen. OBS pyynnön epäonnistumiset palautetaan niiden tila koodi ja (kun se on annettu) palvelimen kommentti.

pöytäkirjan viittaukset:

- [OBS WebSocket v5 pöytäkirjan eritelmä](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket todennäköisyys kaava ja viesti opkoodi](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket median sisäänpääsypyyntöjen täytäntöönpano ja hyväksytyt toimenpiteet](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokollimuistiinpanot ja toimintojen nimet tarkistettiin OBS-projektin dokumentaatiosta 2026-10-08.

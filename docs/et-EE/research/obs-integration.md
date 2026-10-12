<!-- generated-doc locale=et-EE source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../research/obs-integration.md)

# OBS Õppekava integratsioon

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana Kasutage seda OBS Studio obs-websocket v5 protokolli ClientWebSocket; see ei installita kolmandat osapoolt OBS Kliendi raamatukogu. protokoll avab serveri Hello (opcode) 0), Vastused koos Identify (opcode 1), Oodatakse identifitseeritud (opcode) 2), Saadaval valge taotlus (opcode) 6), Seejärel vastab tema RequestResponse (opcode) 7) taotlusega ID. salasõna autentimine järgib OBS'Dokumenteeritud kaks etappi SHA-256 Väljakutse arvutamine.

võimaldab **Tööriistad → WebSocket Serveri seaded** Selles OBS. Defekti lõpppunkt on **WS://127.0.0.1:4455**. Seadke tegevus **OBS-WebSocket-Aadress** väljaspool lõpppunkti peab kasutama wss://; krüpteerimata ws:// aktsepteeritakse ainult loopback aadress.. Aivana Keeldub sisseehitatud URL-i loend.

Kui OBS on autentimine kättesaadav, salvestage oma parooli OBS Tegevusjuht. Aivana Müüa seda praeguses Windows kasutaja Credential Manager kui geneeriline credential nimega **Aivana.OBS:<Normaalne lõpppunkt>**. See ei ole kunagi kirjutatud tegevuse või seadete faili., **AIVANA_OBS_PASSWORD** jäävad kättesaadavaks tagasipöörduna, kui lõpppunktil ei ole salvestatud parooli. OBS võib jääda ebatäpseks, kusjuures seda ei ole vaja. vältige parooli juhendi argumente, sest protsessi juhendi joone saab kontrollida.

Kontrolli kataloog ja konfigureerimine **StartRõõm**, **StopRekord**, **ToggleRedigeeri**, **Pause rekordid**, **Resumeerimiskord**, **Startstreem**, **Lõpetamine**, **Toggle Stream**, **Sissejuhatusprogramm**, ja **ToggleInputMuu**. Esimesed kaheksa ei võta argumente. SetCurrentProgramScene kasutab redaktori stseenime; ToggleInputMute kasutab selle sisendime..

Iga tegevus saab värske ühenduse 10 sekundiga ja automaatne tagasipöördumine ei ole. See tähendab tagasivõtmise käske: pärast taotluse saatmist võib aegumine või langenud ühendus jääda tulemuse tundmatuks ja tagasivõitmine võib uuesti tagasivõttu. OBS nõude ebaõnnestumised tagastatakse nende staatuskodi ja (kui see on esitatud) serveri kommentaariga.

Protokolli viited:

- [OBS WebSocket v5 protokoll spetsifikatsioon](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentimise valem ja sõnumikode](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket meedia sisenditaotluse rakendamine ja aktsepteeritud väärtused](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokolli märkused ja tegevusnimed kontrolliti OBS projekti dokumendi suhtes 2026-10-08.

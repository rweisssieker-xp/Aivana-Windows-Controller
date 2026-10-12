<!-- generated-doc locale=sl-SI source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../research/obs-integration.md)

# OBS Študijska integracija

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana Uporablja se OBS Studio obs-websocket v5 protokol preko ClientWebSocket; ne namesti tretje osebe OBS knjižnica. protokol se odpre s strežnikom Hello (opcode) 0), Odgovori z Identify (opcode) 1), Prepoznavni podatki (opcode) 2), Pošlji beleženo prošnjo (opcode) 6), V tem primeru se izpolnjuje zahtevek (opcode) 7) po zahtevi ID. potrditev gesla sledi OBS'Dvostopenjski dokument SHA-256 Izziv izračun.

Omogoča **orodja → WebSocket Server nastavitve** v OBS. Default endpoint je **Vs://127.0.0.1:4455**. Nastavite akcijo **OBS-WebSocket-Naslovna** polje za drugo končno točko. oddaljena končna točka mora uporabljati wss://; nešifrirani ws:// se sprejme le za naslov.. Aivana zavračanje vgrajene URL-ja.

Kdaj OBS je omogočena preverjanje, shranite geslo iz OBS Uradni urednik. Aivana Skladišči ga v trenutni Windows Uporabniški skrbnik kot generični skrbnik **Aivana.OBS:<Normalizirana končna točka>**. Nikoli ni napisano na datoteko ukrepa ali nastavitev., **AIVANA_OBS_Povezava** ostane na voljo kot padec, ko končna točka nima shranjene gesla. OBS lahko ostanejo neavtentirane, v katerem primeru tudi ni potrebno. izogibajte se argumentom zapovedi za geslo, ker se lahko preverijo zapovedi.

Podpora za katalog in konfiguracijo **začetni rekord**, **Stoprekord**, **ToggleRekord**, **PauseRekord**, **ResumeZgodovina**, **Začetek**, **Stopstream**, **Togglestream**, **Trenutni program**, in **ToggleInputMute**. Prvi osem ne vzame argumentov. SetCurrentProgramScene uporablja ime scene urednika; ToggleInputMute uporablja ime vnosa..

Vsaka akcija dobi svežo povezavo z 10 sekundami in brez samodejnega povračila. To velja za zapovedi za odpravo: po pošiljanju zahteve lahko časovni odhod ali izpad povezave zapusti rezultat neznano, in odpravo lahko ponovno odpravi stanje. OBS zapovedi napake se vrnejo z njihovim kodo stanja in (ko je zagotovljeno) komentarjem strežnika.

Reference za protokol:

- [OBS WebSocket specifikacija protokola v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket avtentikacija formule in sporočila opkode](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket izvedba zahteve za vnos medijev in sprejetih vrednosti enum](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokolne beležke in operacijske imena so bile preverjene proti dokumentaciji projekta OBS na 2026-10-08.

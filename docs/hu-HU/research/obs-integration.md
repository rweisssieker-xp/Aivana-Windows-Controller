<!-- generated-doc locale=hu-HU source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../research/obs-integration.md)

# OBS Stúdió integráció

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana Használja a OBS Studio obs-websocket v5 protokoll ClientWebSocket; nem telepíti a harmadik fél OBS A protokollot Hello (opcode) szerverrel nyitja meg 0), Válaszok azonosítással (opcode) 1), Az azonosított (opcode) 2), A kérelem beadása (opcode) 6), A kérelemre válaszol (opcode) 7) A jelszó hitelesítése követendő OBS'A dokumentált két szakasz SHA-256 kihívás számítás.

lehetővé teszi **eszközök → WebSocket Szerver beállítások** A OBS. A hiányzó végpont **WS://127.0.0.1:4455**. A cselekvés beállítása **OBS-WebSocket-Cím** A távoli végpontot wss:// kell használni; titkosított ws:// csak egy loopback címre fogadható el. A végpont is tartalmazhat egy útvonalat vagy egyéni portot. Aivana Az URL-k elutasítása.

Amikor OBS A hitelesítés engedélyezése, a jelszó mentése a OBS Akciószerkesztő. Aivana A jelenlegi értékesítésben Windows a felhasználó hitelesítő menedzserének, mint a generikus hitelesítő nevét **Aivana.OBS:<Normalizált végpont>**. Soha nem írható be egy cselekvési vagy beállítási fájlra., **Aivana_OBS_PASSWORD** A visszavonás akkor marad elérhető, ha a végpontnak nincs mentett jelszava. OBS nem hitelesíthető, amely esetben sem szükséges. Kerülje a parancsvonal érveit a jelszóhoz, mert a folyamat parancsvonalát ellenőrizheti.

A katalógus és a konfiguráció támogatása **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene**, és **ToggleInputMute**.

Minden egyes lépés friss kapcsolatot kap a 10-Ez a kérdés a visszavonás parancsok: miután a kérés küldött, egy időtartam vagy eltűnt kapcsolat hagyhatja az eredmény ismeretlen, és visszavonás lehet visszavonani az állapot újra. OBS A kérés hiba visszatér a status kód és (amikor biztosított) a szerver megjegyzés.

A jegyzőkönyv referenciái:

- [OBS WebSocket v5 protokoll specifikáció](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket hitelesítési képlet és üzenet kódok](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket média beviteli kérelem végrehajtása és elfogadott cselekvési enum értékek](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

A jegyzőkönyvjegyzékeket és a működési neveket ellenőrizte a OBS projektdokumentációt a 2026-10-08 oldalon.

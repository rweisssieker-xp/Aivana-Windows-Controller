<!-- generated-doc locale=lv-LV source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../research/obs-integration.md)

# OBS Studiju integrācija

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana izmanto OBS Studio obs-websocket v5 protokolu, izmantojot ClientWebSocket; tā nav instalējusi trešo pušu klientu bibliotēku OBS. Protokols atveras ar serveri Hello (opcode 0), atbildes ar Identify (opcode1), gaida Identified (opcode]2), nosūta balto pieprasījumu (opcode] 6), pēc tam atbilst pieprasījuma Atbildes (opcode)7 pieprasījuma ID. Paroles autentifikācija seko OBS dokumentētajam divu posmu SHA-256 izaicinājuma aprēķinam.

Iespējams **rīki → WebSocket Serveru iestatījumi** I OBS. Default punkts ir **WS://127.0.0.1:4455**. Iestatīt darbību **OBS-WebSocket-adrese** laukums citam galamērķim. attālināta galamērķis ir jāizmanto wss://; neslēpts ws:// tiek pieņemts tikai ar loopback adresi.. Aivana Atteikts iebūvēts URL.

Kad OBS ir atļauts autentificēt, saglabāt savu paroli no OBS Akcijas redaktors. Aivana Uzglabājiet to pašreizējā Windows Lietotāja uzticamības pārvaldnieks kā ģenētiskais uzticamība **Aivana.OBS:<Normalizēts galamērķis>**. Tas nekad nav rakstīts uz rīcības vai iestatījumu failu., **Aivāna_OBS_PASSWORD** paliek pieejams kā atgriešanās punkts, ja galamērķim nav glabāta paroli. OBS var palikt neautentizēts, šajā gadījumā nav nepieciešams. izvairīties no komandas līnijas argumentiem par paroli, jo procesā komandas līnija var tikt pārbaudīta.

Kontrolieru katalogs un konfigurācijas atbalsts **Sākotnēji**, **StopRekords**, **ToggleRekords**, **PauseRekords**, **Reģistrācija**, **Startstrēmi**, **StopStream**, **ToggleStream**, **AktualitātesProgrammas**, un **ToggleInputMūžība**. Pirmie astoņi neuzņemas argumentus. SetCurrentProgramScene izmanto redaktora skatuves nosaukumu; ToggleInputMute izmanto savu ievades nosaukumu..

Katra darbība iegūst svaigu savienojumu ar 10 sekundes robežu un bez automātiskas atgriešanās. Tas attiecas uz pieprasījumu: pēc pieprasījuma nosūtīšanas laika izbeigšana vai izbeigta saikne var atstāt rezultātu nezināmu, un atgriešanās varētu atkal atgriezties. OBS pieprasījuma neveiksmes tiek atgrieztas ar savu statusa kodu un (ja sniegts) servera komentārus.

protokola atsauces:

- [OBS WebSocket v5 protokola specifikācija](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentifikācijas formulu un ziņojumu kodus](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket mediālie ievešanas pieprasījumi un pieņemtie pasākumi](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokola piezīmes un darbības nosaukumi tika pārbaudīti pret OBS projekta dokumentāciju 2026-10-08.

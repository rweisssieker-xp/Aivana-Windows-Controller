<!-- generated-doc locale=cs-CZ source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../research/obs-integration.md)

# Integrace OBS Studio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana používá protokol OBS Studio obs-websocket v5 prostřednictvím ClientWebSocket; neinstaluje klientskou knihovnu OBS třetí strany. Protokol začíná zprávou Hello serveru (opcode 0), odpoví Identify (opcode 1), čeká na Identified (opcode 2), odešle požadavek z povoleného seznamu (opcode 6) a poté spáruje RequestResponse (opcode 7) podle ID požadavku. Ověřování heslem používá zdokumentovaný dvoufázový výpočet výzvy SHA-256 OBS.

Povolit **Tools → WebSocket Nastavení serveru** v OBS. Výchozí koncový bod je **ws://127.0.0.1:4455**. Nastavte pole **OBS-WebSocket-Adrese** pro jiný koncový bod. Vzdálený koncový bod musí používat wss://; nešifrované ws:// je akceptováno pouze pro adresu zpětné smyčky. Koncový bod může také zahrnovat cestu nebo vlastní port. Aivana odmítá vložená pověření URL.

Když OBS má povoleno ověřování, uložte jeho heslo z OBS akční editor. Aivana ukládá jej do proudu Windows správce pověření uživatele jako obecné pověření pojmenované **Aivana.OBS:<normalizovaný koncový bod>**. Nikdy se nezapisuje do souboru akce nebo nastavení. Pro spravovaná nasazení, **AIVANA_OBS_PASSWORD** zůstane k dispozici jako záložní, když koncový bod nemá uložené heslo. OBS mohou zůstat neověřené, v takovém případě není potřeba ani jeden. Vyhněte se argumentům příkazového řádku pro heslo, protože příkazové řádky procesu lze kontrolovat.

Katalog řadiče a konfigurace podporují **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** a **ToggleInputMute**. Prvních osm nepřijímá argumenty. SetCurrentProgramScene používá název scény z editoru; ToggleInputMute používá název vstupu. Klient protokolu přijímá stejný seznam povolených operací.

Každá akce získá nové připojení s hranicí 10-sekunda a bez automatického opakování. To je důležité pro přepínací příkazy: po odeslání požadavku může časový limit nebo přerušené připojení nechat výsledek neznámý a opakování by mohlo stav znovu přepnout. Selhání požadavků OBS jsou vrácena se svým stavovým kódem a (pokud je uveden) s komentářem serveru.

Odkazy na protokol:

- [OBS WebSocket specifikace protokolu](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket ověřovací vzorec a kód zprávy](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket implementace žádosti o vstup médií a přijaté hodnoty](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokolové poznámky a názvy operací byly zkontrolovány podle projektové dokumentace OBS na 2026-10-08.

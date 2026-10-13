<!-- generated-doc locale=sk-SK source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../research/obs-integration.md)

# Integrácia OBS Studio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana používa protokol OBS Studio obs-websocket v5 prostredníctvom ClientWebSocket; neinštaluje klientsku knižnicu OBS tretej strany. Protokol začína správou Hello servera (opcode 0), odpovie Identify (opcode 1), čaká na Identified (opcode 2), odošle požiadavku z povoleného zoznamu (opcode 6) a potom spáruje RequestResponse (opcode 7) podľa ID požiadavky. Overovanie heslom používa zdokumentovaný dvojfázový výpočet výzvy SHA-256 OBS.

Povoliť **Nástroje → WebSocket Nastavenia servera** v OBS. Predvolený koncový bod je **ws://127.0.0.1:4455**. Nastavte akcie **OBS-WebSocket-Adresa** pole pre iný koncový bod. Vzdialený koncový bod musí používať wss://; nezašifrované ws:// je akceptované iba pre adresu spätnej slučky. Koncový bod môže zahŕňať aj cestu alebo vlastný port. Aivana odmieta vložené poverenia URL.

Keď OBS je povolená autentifikácia, uloží svoje heslo z OBS Akčný editor Aivana V súčasnosti sa skladá Windows používateľský správca dôvery ako generický dôveryhodný názov **Aivana.OBS„Normalizované koncové body“**Nie je nikdy napísaný na akciu alebo nastavenie súboru. pre spravované odkladanie, **AIVANA_OBS_PASSWORD - Zoznam** zostáva k dispozícii ako padnutie, keď koncová položka nemá uložené heslo. OBS môže zostať neautentizované, v tomto prípade nie je potrebné. vyhýbať sa argumentom príkazovej čiary pre heslo, pretože procesné príkazové čiary môžu byť overené.

Katalóg ovládača a konfigurácia podporujú **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** a **ToggleInputMute**. Prvých osem neprijíma argumenty. SetCurrentProgramScene používa názov scény z editora; ToggleInputMute používa názov vstupu. Klient protokolu prijíma rovnaký zoznam povolených operácií.

Každá akcia má čerstvú väzbu s 10-druhá viazaná a žiadne automatické odvolanie. to je dôležité pre odvolanie príkazov: po odoslaní žiadosti, časový výstup alebo odvolanie pripojenia môže zanechať výsledok neznámy, a odvolanie môže opäť odvolanie stavu. OBS požiadavky sa vrátia s ich kódom stavu a (ak je poskytnutý) komentár servera.

Referencie z protokolu:

- [OBS WebSocket špecifikácia protokolu v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentifikácia vzorec a kód správy](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket implementácia požiadaviek na vstup médií a akceptované hodnoty](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

protokolové poznámky a názvy operácií boli skontrolované proti OBS Dokumentácia projektu na 2026-10-08.

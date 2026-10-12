<!-- generated-doc locale=ro-RO source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../research/obs-integration.md)

# OBS Integrarea studiului

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana Foloseşte OBS Studio obs-websocket v5 protocol prin ClientWebSocket; nu instalează o terță parte OBS Biblioteca clientului. protocolul se deschide cu serverul Hello (opcode) 0), Răspunsuri cu Identify (opcode) 1), Așteptare pentru identificat (opcode) 2), Trimite o cerere de înregistrare (opcode) 6), Apoi se potrivește cu RequestResponse (opcode) 7) prin cerere ID. Password autentificare urmează OBS'S-a înregistrat în două etape SHA-256 Calcularea provocării.

Permite **Instrumente → WebSocket Setări de server** În OBS. Punctul final de defecţiune este **WS://127.0.0.1:4455**. Setarea acţiunii **OBS-WebSocket-Adresa** Un punct de terminare îndepărtat trebuie să utilizeze wss://; un ws:// necryptat este acceptat numai pentru o adresă de întoarcere.. Aivana Răspundeți credențiale URL.

Când OBS a activat autentificarea, salvează parola de la OBS Editor de acțiune. Aivana depozitează în prezent Windows Administratorul de credențial al utilizatorului ca un credențial generic numit **Aivana.OBS:<Punctul final normal>**. Nu este scris niciodată pentru un fișier de acțiune sau setări., **Cuvânt cheie - pasaport** rămâne disponibilă ca un fallback atunci când punctul final nu are un parola salvată. OBS poate rămâne neautentică, în cazul în care nici nu este necesar. Evitați argumentele de comandă pentru parola, deoarece liniile de comandă de proces pot fi verificate.

Catalogul de control și suportul de configurare **StartRecordul**, **StopRecordul**, **TOGLECORD**, **Pauză**, **Rezumatul**, **StartStream**, **StopStream**, **Război Toggle**, **SetCurrentProgramScene**, şi **Întâlnirea**. Primele opt nu iau argumente. SetCurrentProgramScene folosește numele de scenă al editorului; ToggleInputMute folosește numele său de intrare..

Fiecare acțiune primește o conexiune proaspătă cu o legătură 10 secunde și fără retragere automată. Acest lucru se întâmplă pentru comenzi de retragere: după ce cererea este trimisă, o conexiune de retragere sau de retragere poate lăsa rezultatul necunoscut, iar retragerea ar putea retragă din nou statul. OBS eșecurile de cerere sunt returnate cu codul de status și (când sunt furnizate) comentarii server.

Referințe ale protocolului:

- [OBS WebSocket specificația protocolului v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket formulă de autentificare și coduri de mesaj](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket implementarea cererii de intrare a mass-media și valorile de acțiune acceptate](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Notele de protocol și numele de operațiune au fost verificate împotriva documentării proiectului OBS pe 2026-10-08.

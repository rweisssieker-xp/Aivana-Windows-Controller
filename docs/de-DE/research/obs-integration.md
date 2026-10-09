# OBS-Studio-Integration

[English (US)](../../en-US/research/obs-integration.md) · [Deutsch](../../de-DE/research/obs-integration.md) · [Français](../../fr-FR/research/obs-integration.md) · [Italiano](../../it-IT/research/obs-integration.md)

Aivana verwendet das OBS-Studio-Protokoll obs-websocket v5 über ClientWebSocket und installiert keine OBS-Clientbibliothek eines Drittanbieters. Der Protokollablauf beginnt mit Hello vom Server (Opcode 0). Darauf antwortet der Client mit Identify (Opcode 1), wartet auf Identified (Opcode 2), sendet eine freigegebene Anfrage (Opcode 6) und ordnet die RequestResponse (Opcode 7) anhand der Request-ID zu. Die Passwortauthentifizierung folgt der von OBS dokumentierten zweistufigen SHA-256-Challenge-Berechnung.

Aktiviere in OBS **Werkzeuge → WebSocket-Servereinstellungen**. Der Standardendpunkt lautet **ws://127.0.0.1:4455**. Trage für einen anderen Endpunkt im Feld **OBS-WebSocket-Adresse** der Aktion den gewünschten Wert ein. Ein entfernter Endpunkt muss wss:// verwenden; unverschlüsseltes ws:// wird nur für eine Loopback-Adresse akzeptiert. Der Endpunkt kann außerdem einen Pfad oder einen benutzerdefinierten Port enthalten. Aivana lehnt Zugangsdaten ab, die in der URL eingebettet sind.

Wenn die OBS-Authentifizierung aktiviert ist, speichere das Passwort im OBS-Aktionseditor. Aivana legt es im Anmeldeinformations-Manager des aktuellen Windows-Benutzers als generische Anmeldeinformation mit dem Namen **Aivana.OBS:<normalisierter Endpunkt>** ab. Es wird nie in einer Aktion oder Einstellungsdatei gespeichert. Für verwaltete Installationen steht **AIVANA_OBS_PASSWORD** als Rückfalloption bereit, wenn für den Endpunkt kein Passwort gespeichert ist. OBS kann auch ohne Authentifizierung betrieben werden; dann wird keines der beiden Verfahren benötigt. Vermeide Passwortargumente in der Befehlszeile, da Prozessbefehlszeilen einsehbar sein können.

Der Controllerkatalog und die Konfiguration unterstützen **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** und **ToggleInputMute**. Die ersten acht benötigen keine Argumente. SetCurrentProgramScene verwendet den im Editor eingegebenen Szenennamen; ToggleInputMute den dort eingegebenen Eingabenamen. Der Protokollclient akzeptiert dieselbe Freigabeliste.

Jede Aktion öffnet eine neue Verbindung mit einem Zeitlimit von 10 Sekunden und ohne automatische Wiederholung. Das ist bei Umschaltbefehlen wichtig: Nach dem Senden einer Anfrage kann ein Timeout oder ein Verbindungsabbruch zu einem unbekannten Ergebnis führen. Eine Wiederholung könnte den Zustand ein zweites Mal umschalten. OBS-Anfragefehler werden mit Statuscode und, sofern vorhanden, Serverkommentar zurückgegeben.

Protokollreferenzen:

- [OBS-WebSocket-v5-Protokollspezifikation](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS-WebSocket-Authentifizierungsformel und Nachrichten-Opcodes](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [Implementierung der OBS-WebSocket-Medieneingabeanfrage und zulässige Aktions-Enumwerte](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Die Protokollhinweise und Operationsnamen wurden am 8. Oktober 2026 mit der Dokumentation des OBS-Projekts abgeglichen.

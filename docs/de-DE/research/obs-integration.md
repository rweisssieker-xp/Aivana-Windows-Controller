<!-- generated-doc locale=de-DE source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../research/obs-integration.md)

# OBS Studio-Integration

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana verwendet das Protokoll OBS Studio obs-websocket v5 über ClientWebSocket; Es wird keine OBS-Clientbibliothek eines Drittanbieters installiert. Das Protokoll öffnet sich mit Server Hello (Opcode 0), antwortet mit Identify (Opcode 1), wartet auf Identified (Opcode 2), sendet eine Whitelist-Anfrage (Opcode 6) und ordnet dann seine RequestResponse (Opcode 7) der Anforderungs-ID zu. Die Passwortauthentifizierung folgt der dokumentierten zweistufigen SHA-256-Challenge-Berechnung von OBS.

Aktivieren Sie **Tools → WebSocket Server Settings** in OBS. Der Standardendpunkt ist **ws://127.0.0.1:4455**. Legen Sie das Feld **OBS-WebSocket-Adresse** der Aktion für einen anderen Endpunkt fest. Ein Remote-Endpunkt muss wss:// verwenden; unverschlüsseltes ws:// wird nur für eine Loopback-Adresse akzeptiert. Der Endpunkt kann auch einen Pfad oder einen benutzerdefinierten Port enthalten. Aivana lehnt eingebettete URL-Anmeldeinformationen ab.

Wenn für OBS die Authentifizierung aktiviert ist, speichern Sie das Kennwort im Aktionseditor OBS. Aivana speichert es im Anmeldeinformations-Manager des aktuellen Windows-Benutzers als generische Anmeldeinformationen mit dem Namen **Aivana.OBS:<normalisierter Endpunkt>**. Es wird niemals in eine Aktions- oder Einstellungsdatei geschrieben. Für verwaltete Bereitstellungen bleibt **AIVANA_OBS_PASSWORD** als Fallback verfügbar, wenn der Endpunkt kein gespeichertes Passwort hat. OBS bleibt möglicherweise nicht authentifiziert. In diesem Fall ist keines von beidem erforderlich. Vermeiden Sie Befehlszeilenargumente für das Kennwort, da Prozessbefehlszeilen überprüft werden können.

Der Controller-Katalog und die Konfiguration unterstützen **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** und **ToggleInputMute**. Die ersten acht akzeptieren keine Argumente. SetCurrentProgramScene verwendet den Szenennamen des Editors; ToggleInputMute verwendet seinen Eingabenamen. Der Protokoll-Client akzeptiert dieselbe Zulassungsliste.

Jede Aktion erhält eine neue Verbindung mit einer 10-Sekunden-Grenze und ohne automatischen Wiederholungsversuch. Dies ist für Umschaltbefehle wichtig: Nachdem die Anfrage gesendet wurde, kann eine Zeitüberschreitung oder eine unterbrochene Verbindung dazu führen, dass das Ergebnis unbekannt bleibt, und ein erneuter Versuch könnte den Status erneut umschalten. OBS-Anforderungsfehler werden mit ihrem Statuscode und (falls angegeben) einem Serverkommentar zurückgegeben.

Protokollreferenzen:

- [OBS WebSocket V5 Protokoll Spezifikation](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket Authentifizierung Formel und Nachricht Opcodes](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket Verpflichtung zur Umsetzung von Medieninträgen und akzeptierte Aktionswerte](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Die Protokollnotizen und Operationsnamen wurden anhand der Projektdokumentation OBS auf 2026-10-08 überprüft.

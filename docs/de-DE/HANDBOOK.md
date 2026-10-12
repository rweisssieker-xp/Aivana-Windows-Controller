<!-- generated-doc locale=de-DE source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../HANDBOOK.md)

# MAKRIVO – Benutzerhandbuch

Hersteller: **Aivana**. Dokumentationsstand: **2026-10-12**. Dieses Handbuch beschreibt die Implementierung in der aktuellen Arbeitskopie. Experimentelle Integrationen und hardwareabhängige Funktionen sind ausdrücklich gekennzeichnet. Historische Berichte gelten für den jeweils beschriebenen Stand und sind keine Garantie für neuere Builds.

## Dokumentation und Sprachen

Verwenden Sie die Sprachauswahl für die Dokumentation, um das vollständige Handbuch, Funktionsinventar, Fehlerbehebung, Entwicklungshinweise, Forschungsberichte und historische Testberichte in einer der 27-Anwendungssprachen zu öffnen. Die generierten Übersetzungen sind maschinell unterstützt und verfügen über Strukturprüfungen; Sie wurden nicht alle von Muttersprachlern bewertet. Technische Kennungen und ausführbare Beispiele bleiben unverändert. Die Originalquelle bleibt zum Vergleich verfügbar. Die übersetzte Lizenzanalyse dient der Information und ersetzt nicht die ursprünglichen rechtlichen Bestimmungen Dritter oder eine endgültige EULA.

## Installation und erster Start

Das anfängliche Ziel ist Windows 11 x64. Halten Sie alle Dateien und Unterverzeichnisse der Distribution zusammen. Starten Sie `MAKRIVO.exe`. Ein Source Checkout ist kein Installer. In sich geschlossene Pakete enthalten ihre .NET-Laufzeit; Framework-abhängige Pakete erfordern die entsprechenden .NET 10 Desktop- und ASP.NET-Laufzeiten. ONNX und Begleithosts müssen für ihre jeweiligen Funktionen enthalten sein.

1. Starten Sie MAKRIVO und wählen Sie in den Einstellungen die Sprache der Benutzeroberfläche aus. Automatisch folgt Windows. Das Tastaturlayout ist eine separate Einstellung.
2. Wählen Sie ein Profil aus und fügen Sie dann über den durchsuchbaren Aktionskatalog eine Kachel hinzu. Geben Sie ihm einen erkennbaren Namen und wählen Sie seine Aktionen aus.
3. Überprüfen Sie die Schritte und weisen Sie bei Bedarf eine Verknüpfung zu. Vermeiden Sie Kombinationen, die bereits von Windows oder Ihrer Zielanwendung verwendet werden.
4. Speichern Sie den Entwurf. Nicht gespeicherte Änderungen sind nicht die gespeicherte Konfiguration.
5. Testen Sie mit einer harmlosen Aktion, z. B. dem Öffnen eines Dokuments, und überprüfen Sie das Ausführungsfeedback.

Beim Schließen des Hauptfensters verbleibt die Anwendung normalerweise im Infobereich. Doppelklicken Sie auf das Taskleistensymbol, um es erneut zu öffnen, oder verwenden Sie zum Beenden das Taskleistenmenü. Der Beginn mit Windows ist optional und zunächst deaktiviert. Die Pausensteuerung unterbricht Bindungen; Das Symbol zeigt ein Pausenzeichen. Brechen Sie die Ausführung von Makros über die Benutzeroberfläche, die Taskleiste oder die konfigurierte Abbruchverknüpfung ab. Der Standardwert ist `Ctrl+Alt+Escape`.

## Kacheln, Aktionen und Befehlspalette

Kacheln gehören zu Profilen und können mehrere geordnete Schritte enthalten. Der Editor unterstützt Speichern/Verwerfen, Vorschau, Rückgängig/Wiederherstellen und Neuanordnen von Schritten. Der Katalog bietet Voreinstellungen für Programme, Dokumente, URLs, Text, Zwischenablage, Verknüpfungen, Medien, Audio, Fenster, Systemaktionen und unterstützte Integrationen. Eine Voreinstellung, die Parameter erfordert, wird als Entwurf geöffnet.

Öffnen Sie die Palette mit `Ctrl+K`, während MAKRIVO den Fokus hat. Durchsuchen Sie gespeicherte Aktionen, Voreinstellungen, Profilwechsel und Tools. Pfeiltasten auswählen; Enter aktiviert; `Ctrl+Enter` bearbeitet ein unterstütztes Element; Escape schließt sich. Einige gebrauchsfertige Befehle werden sofort ausgeführt, während konfigurierte Aktionen überprüft werden müssen. Eine separate globale Palettenverknüpfung kann konfiguriert werden; Neue Konfigurationen verwenden `Ctrl+Shift+Space`.

Text- und Zwischenablageschritte unterstützen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` und `{{user}}`. Die Datumsformatierung ist `yyyy-MM-dd`; Die Zeitformatierung ist `HH:mm`. Unbekannte Variablen bleiben unverändert. Das Lesen der Zwischenablage erfolgt nur auf Anfrage. Eine Zwischenablageaktion schreibt Text, ohne ihn automatisch einzufügen.

Über die Oberfläche gestartete Aktionen können sich an die zuletzt aktive externe Anwendung richten. Wenn das Ziel geschlossen wurde oder Windows den Vordergrundwechsel blockiert, aktiviere das Zielfenster manuell und versuche es erneut. Die erfolgreiche Zustellung eines Tastendrucks belegt nicht, dass die Zielanwendung ihre Aufgabe erledigt hat.

## Profile und Arbeitsplatz-Assistent

Profile können einem Prozessnamen und einem Teil eines Fenstertitels entsprechen. Wenn beide festgelegt sind, müssen beide übereinstimmen. Die Profilreihenfolge löst konkurrierende Spiele auf. Das Standardprofil ist das Fallback. Aktivieren Sie die automatische Auswahl explizit. Das Sperrprofil verhindert automatische Änderungen für die aktuelle Sitzung.

Öffnen Sie „Workplace Setup“ über „Extras“ oder die Palette. Wählen Sie Programme, Dokumente, Websites, ein Audio-/Anzeigeziel oder eine OBS-Szene aus. Der Assistent erstellt Arbeitsplatz-, Meeting-, Streaming-, Forschungs- oder Projektprofile. Überprüfen Sie die generierten Schritte vor dem Speichern. Setup führt die vorbereiteten Aktionen nicht aus.

## Makros, Bedingungen und Aufnahme

Makros führen ihre Schritte der Reihe nach aus und unterstützen Wartezeiten, Wiederholungen und Abbrüche. Die allgemeine Wiederholungsgrenze beträgt 100. Verwenden Sie die Vorschau, bevor Sie Aktionen ausführen, die in eine andere Anwendung eingreifen oder den Systemstatus ändern.

„Warten auf Zielfenster“ akzeptiert einen Prozess, eine Titelteilzeichenfolge und eine Zeitüberschreitung von 1 bis 600,000 ms. Es ist mindestens ein Filter erforderlich. Ein passendes sichtbares Fenster muss reagieren und in den Vordergrund treten, bevor die nachfolgenden Schritte fortgesetzt werden. Ein Bereitschaftsfenster ist kein Beweis dafür, dass ein Dokument oder ein Dienst vollständig geladen wurde.

Der Workflow-Editor fügt Dateiexistenz-, Prozessausführungs- und Fenstertitelbedingungen hinzu. Eine falsche Bedingung überspringt den Schritt und kann weitere Schritte nach vorne überspringen. Negieren Sie die Bedingung, um einen alternativen Zweig auszudrücken. Wählen Sie bewusst „Stopp bei Fehler“ oder „Fortfahren bei Fehler“. Wenn Sie fortfahren, können spätere Schritte ohne ihre erwarteten Voraussetzungen zurückbleiben.

Der Aktionsrekorder erfasst Tastenkombinationen und Wartezeiten, nicht normales Tippen. Es unterstützt Modifikator-Shortcuts und Funktionstasten. Escape stoppt die Aufzeichnung. Überprüfen Sie die Liste, übernehmen Sie sie als nicht gespeicherten Kachelentwurf und speichern Sie sie dann explizit. Die Aufzeichnung von UI-Klicks ist ein separates Tool, das im Folgenden beschrieben wird.

## Tastaturen, Gesten und Ebenen

Die Tastaturseite listet erkannte Schnittstellen und Referenzlayouts auf. Eine einzelne physische Tastatur kann mehrere Schnittstellen verfügbar machen. Wählen Sie die richtige Sprache und Geometrie aus; Ein Produktname allein identifiziert kein ISO- oder ANSI-Layout.

Normale globale Hotkeys gelten für alle Tastaturen. Gerätespezifische Raw-Input-Zuordnungen erkennen ein bestimmtes Gerät und dessen Scan-Code, einschließlich unterstützter Funktions- und Medientasten. **Raw Input unterdrückt die normale Windows-Eingabe nicht. Eine exklusive Zweittastatur ist noch nicht produktionsreif.** Dafür fehlt ein signierter, lizenzierter und geprüfter Filtertreiber. MAKRIVO installiert keinen solchen Treiber.

Unterstützte Sondertastenbelegungen können sofortiges Drücken, Tippen, Halten oder Doppeltippen verwenden. Angehaltene Layer-Tasten wählen vorübergehend eine andere Zuordnung aus und stellen beim Loslassen den Basis-Layer wieder her; Ebenenauswahltasten führen kein eigenes Makro aus. Die Halteschwellenwerte reichen von 150 bis 5,000 ms; Doppeltippen Sie auf Fenster von 150 bis 1,000 ms. Diese Zuordnungen unterscheiden sich von gewöhnlichen globalen Hotkeys.

Der Spezialschlüssel-Assistent erkennt kompatible Schlüssel, ohne die ihnen zugewiesenen Aktionen während der Erfassung auszuführen. Die Diagnose erklärt nicht verfügbare Geräte, pausierte Bindungen, Profilkonflikte und Konflikte. Das Overlay zeigt das ausgewählte Layout und die Bindungen; Die standardmäßige Halteverknüpfung lautet `Ctrl+Alt+F1`.

| Integration | Anforderungen und Einschränkungen |
| --- | --- |
| Corsair | Mit iCUE, SDK-Zugriff und einem kompatiblen Modell. G1–G6 und unterstützte RGB-Funktionen; Proprietäre Fn-, Rad-, Sperr- und Profilsteuerungen werden im Allgemeinen nicht von der G-Key-API verfügbar gemacht. |
| Logitech G | Ursprüngliche lokale x64-G-Key/LED-SDK-Bibliotheken erfordern eine ausdrückliche Genehmigung. G-Tasten-Ereignisse identifizieren mehrere identische physische Tastaturen nicht eindeutig. |
| Razer | Chroma RGB und Standardeingang; Ordnen Sie bei Bedarf proprietäre Steuerelemente in Synapse zu. |
| SteelSeries | GameSense RGB und Standardeingang; Ordnen Sie bei Bedarf proprietäre Kontrollen in GG zu. |
| Wooting | Kompatibles originales lokales x64-SDK für Analogwerte und Schwellenwerte; Der normale HID-Eingang bleibt aktiv. |
| Andere Tastaturen | Standard-Hotkeys oder unterstützte physische Scan-Code-Bindungen. Fn und proprietäre Steuerungen hängen vom Modell und der Herstellerkonfiguration ab. |

Die Herstellerforschung beschreibt dokumentierte Funktionen, nicht implementierte oder getestete Kompatibilität mit jedem aufgelisteten Gerät. Neue Adapter benötigen noch eine echte Hardware-Akzeptanz. Laden Sie keine SDK-Binärdateien von nicht vertrauenswürdigen Quellen herunter; SDK-Vertriebsrechte sind unabhängig von der technischen Kompatibilität.

## Audio, Displays und OBS

Audio-Schritte Set Volume von 0 zu 100 Prozentsatz und mute/unmute/toggle unterstützt Ausgang oder Mikrofon Endpunkte. Wählen Sie einen bestimmten Endpunkt oder verwenden Sie Windows Ein fehlendes gespeichertes Ziel erzeugt einen Fehler anstelle der stillen Kontrolle eines anderen Geräts. Erneutete Geräte nach der Wiederverbindung. Endpoint-Control ist keine Anforderung für unabhängige Mixer-Support.

Anzeigeaktionen erhöhen/verringern die Helligkeit um 10 Prozentpunkte oder legen Sie einen Wert von 0 bis 100 fest. Interne Displays nutzen unterstützte WMI-Schnittstellen; Externe Monitore erfordern DDC/CI-Unterstützung und normalerweise die entsprechende Monitormenüeinstellung. Docks, Adapter und Kabel können die Steuerung blockieren. Wählen Sie alle unterstützten Displays oder einen Monitor. Durch die erneute Verbindung können verfügbare Identitäten geändert werden. Überprüfen Sie das gespeicherte Ziel. Fehlende Messwerte werden als nicht verfügbar und nicht als Messwerte angezeigt.

OBS erfordert, dass WebSocket v5 in den OBS Tools aktiviert ist. Konfigurieren Sie den Endpunkt, testen Sie die Verbindung und laden Sie Szenen-/Eingabenamen vor dem Speichern. Zu den unterstützten Aktionen gehören Szenenwechsel, Starten/Stoppen/Umschalten von Stream/Aufnahme, Anhalten/Fortsetzen der Aufnahme und Eingangsstummschaltung. Passwörter können im Windows Credential Manager gespeichert werden und sind von Konfigurationsexporten ausgeschlossen. Ein erfolgreicher OBS-Befehl stellt nicht jeden Downstream-Streaming-Effekt her.

## Ergebnisprüfung und Ausführungsverlauf

Aktivieren Sie „Ergebnis überprüfen“ für geeignete Schritte. Die App unterscheidet zwischen Preflight-Checks, bestätigten Effekten, Ausfällen, Abbrüchen und nicht verifizierbaren Effekten. Das Vorhandensein eines Prozesses ist keine Anwendungsbereitschaft. Relative Änderungen, Verknüpfungen und Texteingaben können keinen pauschalen Erfolgsanspruch erhalten. Überprüfen Sie das Aktivitätsprotokoll und den Überprüfungsverlauf, wenn eine Aktion fehlschlägt. Konfigurationsfehler sollten korrigiert werden, bevor das gesamte Makro erneut ausgeführt wird.

## UI-Automation und Klickaufnahme

Füge einen Schritt für die UI-Automation hinzu, wähle die Operation und erfasse das Ziel unter dem Mauszeiger nach der angezeigten Verzögerung. Die Erfassung allein führt keine Aktion aus. Prüfe den Selektor und verlange einen eindeutigen Treffer. Unterstützte Operationen sind `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` und `WaitState`. Passwortfelder, deaktivierte Bedienelemente und mehrdeutige Selektoren werden abgewiesen.

Die UI-Automatisierung wird in einem begrenzten Begleitprozess ausgeführt. Dies hängt von der Windows-Barrierefreiheitsunterstützung und den Berechtigungen der Zielanwendung ab. Der Klickrekorder erfasst unterstützte Aktionen und Pausen; Allgemeine Texteingaben werden nicht aufgezeichnet. Überprüfen und validieren Sie den Entwurf, bevor Sie ihn annehmen. Änderungen an der Benutzeroberfläche in einer externen App können Selektoren ungültig machen. Die Prozessisolation begrenzt Abstürze und Abbrüche, ist jedoch keine Sicherheitssandbox.

## Plugins

Öffnen Sie „Erweiterungen“, um ein Paket vor dem Import zu überprüfen. Paketstruktur, Manifest und Hashes werden überprüft. Ein Import wird deaktiviert gestartet. Lesen Sie die deklarierten Aktionen und vertrauen Sie dem Herausgeber ausdrücklich, bevor Sie sie aktivieren. Plugins werden mit Ihren Benutzerberechtigungen in einem separaten Prozess ausgeführt; Dieser Prozess ist **keine Sandbox**. Installieren Sie nur Pakete, denen Sie vertrauen.

Der Manager unterstützt das Exportieren, Deaktivieren, Deinstallieren und Wiederherstellen/Verwerfen einer früheren Version. Fehlende Abhängigkeiten blockieren den Aufruf. Das Beispielpaket bietet Countdown- und lokale Statusaktionen. Das unterstützte Schema ist eine dokumentierte JSON-Schema-Teilmenge, nicht jede mögliche JSON-Schema-Funktion. Es gibt keinen öffentlichen Plugin-Marktplatz.

## Mobiles Panel

1. Öffnen Sie das Mobile Panel, wählen Sie eine lokale IP aus und wählen Sie explizit die Profile aus, die verfügbar gemacht werden sollen. Die Standard-Loopback-Adresse ist nur auf dem PC selbst erreichbar; Wählen Sie die entsprechende LAN-Adresse für ein Telefon oder Tablet aus.
2. Starten Sie das Panel und lesen Sie dessen HTTPS-Adresse, den Fingerabdruck des Zertifikats und den einmaligen sechsstelligen Kopplungscode.
3. Öffnen Sie die Adresse auf einem Gerät im selben vertrauenswürdigen Netzwerk. Vergleichen Sie den Fingerabdruck SHA-256 über einen vertrauenswürdigen Kanal, bevor Sie dem lokalen Zertifikat vertrauen. Die App installiert ein vertrauenswürdiges Zertifikat nicht automatisch.
4. Koppeln Sie mit dem angezeigten Code und testen Sie eine harmlose Kachel. Für den LAN-Zugriff ist möglicherweise eine manuelle Firewall-Berechtigung Windows erforderlich. Setzen Sie den Port nicht dem Internet aus.
5. Stoppen Sie das Panel, wenn Sie fertig sind, oder widerrufen Sie gekoppelte Clients über die App.

Private Serverschlüssel sind mit Windows DPAPI geschützt. Client-Anmeldeinformationen werden als Hashes gespeichert. Eine Änderung der IP oder eine Erneuerung des Zertifikats kann eine erneute Vertrauenswürdigkeit erfordern. Das Panel zeigt Aktions- und Überprüfungszustände an, nicht jeden möglichen Live-Sensor. Browser-Layout- und lokale HTTPS-Prüfungen ersetzen nicht die echte LAN-Akzeptanz für Telefone/Tablets.

## Lokaler KI-Besprechungsassistent

Dieser optionale experimentelle Assistent ist standardmäßig deaktiviert. Öffnen Sie es über „Tools“, aktivieren Sie es explizit, wählen Sie eine Laufzeit/ein Modell aus und fügen Sie nur die Ressourcen hinzu, die berücksichtigt werden sollen. Geben Sie das Ziel an, erstellen Sie einen Entwurf, prüfen Sie offene Anfragen und übergeben Sie das Ergebnis an den normalen Redakteur. Nichts wird automatisch ausgeführt; Speichern und Ausführen bleiben explizite Aktionen. Es gibt keinen automatischen Laufzeit-Fallback.

| Laufzeit | Einrichtung |
| --- | --- |
| Ollama | Separater lokaler Dienst und ein lokal installiertes Modell unter `127.0.0.1:11434`. Deklarierte Remote-/Cloud-Modelle werden abgelehnt. |
| LM Studio | Lokaler Server mit einem lokal geladenen Modell und JSON-Schema-Unterstützung unter `127.0.0.1:1234`. Aktivieren Sie kein Remote-Routing für die lokale Verarbeitung. |
| llama.cpp | Lokales `llama-server` und kompatibles Modell mit JSON-Schema-Unterstützung unter `127.0.0.1:8080`. |
| ONNX Laufzeit GenAI CPU | Vertrauenswürdiger kompatibler lokaler Textmodellordner GenAI; Wählen Sie `genai_config.json`. Inference verwendet einen gebündelten Begleitprozess ohne HTTP. |

HTTP-Clients verwenden nur Loopback, verwenden keine Proxys und folgen keinen Weiterleitungen. MAKRIVO kann nicht zuverlässig feststellen, ob ein kompatibler lokaler Server intern einen Remote-Dienst aufruft. Authentifizierte lokale Server sind in dieser Version nicht konfigurierbar. Modellgewichte, Python und lokale Inferenzserver werden nicht automatisch heruntergeladen oder installiert.

ONNX Pakete benötigt `tokenizer.json` und eine gültige Chat-Template. unterstützte Template-Formulare in `tokenizer_config.json` Es ist ein String `chat_template`Ein Wörterbuch mit einem String `default`, oder eine zweifelhafte Namens-/Default-Template-Liste. Wenn das Template fehlt oder null, `chat_template.jinja` als Fallback verwendet werden. leere, ambiguierte, übergrößerte oder falsch eingegebenen Vorlagen werden abgelehnt. ONNX Dateien, Einführung/Vision-Modelle und GPU-einige Pakete sind nicht mit kompatiblen GenAI Textmodelle: Die Bündelung GenAI Die runtime Version ist 0.17.1, CPU. Verwenden Sie nur vertrauenswürdige Modelle; Prozessseparation ist keine Sandbox.

Der Assistent unterstützt ausgewählte Programme ohne Argumente, ausgewählte Dokumente und Websites und schaltet das Mikrofon auf ausdrücklichen Wunsch stumm. Es werden keine Nachrichten, Termine, willkürlichen Skripte, kostenlosen Klicks, Downloads oder Headset-Wechsel erstellt. Die Ziel- und Ressourcennamen werden an die ausgewählte lokale Laufzeit gesendet. Ressourcenpfade und Dateiinhalte werden nicht separat bereitgestellt. Ein vom Nutzer eingegebenes Ziel kann selbst personenbezogene Daten enthalten. Das eigene Netzwerkverhalten einer lokalen Laufzeit bleibt in der Verantwortung des Benutzers.

Das Anforderungsbudget ONNX beträgt 90 Sekunden, einschließlich Modellladen, bis zu 3,072 Eingabeaufforderungstoken und 1,024 generierten Token, ebenfalls begrenzt durch den Modellkontext. Fehlerhafter/abgeschnittener JSON-Code und unbekannte Ressourcen-IDs werden abgelehnt. Abbrechen oder schließen, um eine Anfrage zu beenden. Ein Zufallsmodell-Laufzeittest stellt die Ausführung fest, nicht jedoch die nützliche Planungsqualität oder Zeitersparnis. Lokale Experimentprotokolle enthalten Ereigniskennungen und -zeiten, keinen Zieltext oder Dokumentinhalt. Sie werden nicht automatisch hochgeladen.

Die spätere kleine Instruct-Modellbewertung produzierte in vier deutschen/Englischen Prüfungen null gültige Pläne. Native Inference arbeitete, aber das Modell fehlte den strengen Planungsvertrag. Dies ist ein negatives Qualitätsergebnis, nicht ein empfohlener Modell oder ein bewiesen nützlicher Assistant.

## Import, Export, Synchronisierung und Sicherung

Überprüfen Sie die Importvorschau, bevor Sie eine Konfiguration ersetzen. Importierte Aktionen werden deaktiviert gestartet und die portable Synchronisierung entfernt hardwarespezifische Bindungen. Ordnen Sie Geräte neu zu und aktivieren Sie Aktionen gezielt nach der Inspektion. Überprüfen Sie die Text-/Aktionsinhalte, bevor Sie einen Export freigeben. Der Ausschluss von Passwörtern führt nicht dazu, dass alle vom Benutzer erstellten Aktionsinhalte nicht vertraulich sind.

Die Profilsynchronisierung tauscht Dateien in einer ausgewählten Ordnung aus, die optionell von Ihrem bestehenden Cloud-Client verwaltet wird. MAKRIVO keine Cloud-Konto liefert und keine Cloud-Kreditials speichert. Überprüfungen verhindern schweigende Übersetzungen. Inbegriffen von Aktionsinhalt ist eine ausdrückliche Wahl. Backup Ihre Konfiguration vor einem Ersatz-Import. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`• Der historische Direktorenname wird für Kompatibilität beibehalten.Schließen Sie die App vor der manuellen Kopie/Restore.Speicherte SDK-Aufträge und OS-schützte Bestätigungen müssen auf einem anderen PC wiederhergestellt werden.

## Fehlerhilfe

| Symptom | Was zu überprüfen ist |
| --- | --- |
| Die Verknüpfung bewirkt nichts | App läuft, Bindungen nicht angehalten, korrektes aktives Profil, gespeicherte/aktivierte Kachel, Verknüpfungskonflikte und verfügbares Ziel. |
| Zielfenster kann nicht aktiviert werden | Aktivieren Sie es manuell. Windows Vordergrundeinschränkungen und Berechtigungsunterschiede können die Automatisierung blockieren. |
| Sonderschlüssel wird nicht erkannt | Anbieterservice/SDK/Modellunterstützung; Konfigurieren Sie proprietäre Steuerelemente so, dass nach Möglichkeit ein unterstützter Standardschlüssel ausgegeben wird. |
| Die zweite Tastatur tippt auch normal | Erwartet mit Raw Input. Eine ausschließliche Unterdrückung ist für die Produktion nicht implementiert. |
| Audio oder Helligkeit nicht verfügbar | Geräte aktualisieren; Überprüfen Sie die ausgewählte Identität, Endpunktsitzungen, WMI/DDC/CI-Unterstützung und Dock-/Kabeleinschränkungen. |
| OBS offline | WebSocket v5 aktiviert, korrekter Endpunkt/Passwort, erreichbarer Dienst und gültige Szenen-/Eingabenamen. |
| Die UI-Automatisierung schlägt fehl | Begleithost enthalten, eindeutiger gültiger Selektor, zugängliches aktiviertes Ziel, unterstützte Steuerung und Berechtigungen. Versuchen Sie eine vorübergehende Zeitüberschreitung erst erneut, nachdem Sie das Ziel überprüft haben. |
| Mobiles Panel nicht erreichbar | LAN statt Loopback-Adresse, dasselbe Netzwerk, laufender Server, genehmigte Profile, Zertifikat-Fingerabdruck/-Vertrauen und manuelle Firewall-Berechtigungen. |
| Plugin schlägt fehl | Paketintegrität, expliziter Vertrauens-/Aktivierungsstatus, Abhängigkeiten, Protokollkompatibilität und Ausführungsprotokoll. |
| AI-Anfrage schlägt fehl | Korrigieren Sie die ausgewählte lokale Laufzeit, Dienst-/Modellbereitschaft, unterstütztes JSON-Schema, vertrauenswürdigen kompatiblen Modellordner und Anforderungsgrenzen. |
| Layout oder Text scheinen falsch zu sein | Trennen Sie die UI-Sprache von der Tastaturgeometrie. Ändern Sie die Größe des Fensters, überprüfen Sie den Anzeigemaßstab und geben Sie die genaue Seite/Sprache/den genauen Aufbau an. |

Zu einem nützlichen Fehlerbericht gehören die Version Windows, App-Build/Commit, Sprache, Anzeigeskalierung, Hardwaremodell, eine Mindestsequenz und das relevante redigierte Protokoll. Entfernen Sie persönliche Pfade, Aktionsinhalte und Anmeldeinformationen vor der Veröffentlichung.

## Build, Tests und Paketerstellung

Verwenden Sie das in `global.json` angeheftete SDK. Aus dem Repository-Stammverzeichnis:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows-Integrationstests können isolierte Fenster öffnen und unterstützte Anbieterbindungen vorübergehend konfigurieren/freigeben. Führen Sie sie aus, ohne dass ein anderer Controller diese Bindungen besitzt. Die standardmäßigen Helligkeits-/Audiosonden sind schreibgeschützt; Explizite Hardware-Roundtrip-Tests sind separat. Scheinprotokolle, gerenderte Screenshots und zufällige Inferenzdiagramme dürfen nicht als echte Akzeptanz in Hardware-/Modellqualität dargestellt werden.

Der vollständige Herausgeber stellt Begleithosts, mobile Assets, das Beispiel-Plugin, die ONNX-Laufzeit und verfügbare Abhängigkeitshinweise zusammen. Behalten Sie die Unterverzeichnisse bei. Ein erfolgreicher Build ist kein unterzeichnetes Installationsprogramm oder eine rechtliche Freigabegenehmigung. Quelle und Builds können separat weiterentwickelt werden; Vergleichen Sie den Dokumentations-Snapshot und übertragen Sie ihn mit der installierten Version.

## Lizenzierung, Datenschutz und Freigabestand

Das beabsichtigte kommerzielle Modell ist ein einmaliger Kauf mit unbefristeter Nutzung der erworbenen Hauptversion; Endpreis, Gerätezuschlag und Upgrade-Bedingungen müssen in den tatsächlichen Verkaufsbedingungen festgelegt werden. Ein Produktionskauf-/Aktivierungssystem wird in diesem Handbuch nicht festgelegt. Für die alltägliche Kontrolle ist kein Cloud-Konto erforderlich.

Verteilen Sie keine proprietären SDK-Binärdateien ohne nachgewiesene Genehmigung. Behalten Sie Lizenz- und Drittanbieterhinweise für jede gelieferte Abhängigkeit bei, einschließlich .NET- und ONNX-Paketen. Modellgewichte haben ihre eigenen Begriffe. Die datierte Lizenzprüfung ist älter als einige neue Abhängigkeiten und stellt keine Freigabe für diese Ergänzungen dar. Da das Repository öffentlich ist, gewährt es selbst keine allgemeine Open-Source-Lizenz.

Echte Herstellerhardware, Telefon-/Tablet-LAN-Betrieb, gemischte Monitor-/DPI-Übergänge, Langzeitnutzung, vollständige sprachliche Überprüfung, exklusive Treiberunterstützung und nützliche KI-Planungsqualität bleiben separate Abnahmearbeiten. Schließen Sie die Zustimmung niemals allein aus der Anwesenheit von Funktionen ab.

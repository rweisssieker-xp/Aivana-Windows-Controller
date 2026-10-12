<!-- generated-doc locale=de-DE source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../HANDBOOK.md)

# MAKRIVO – Benutzerhandbuch

Hersteller: **Aivana**. Dokumentationsstand: **2026-10-12**. Dieses Handbuch beschreibt die Implementierung in der aktuellen Arbeitskopie. Experimentelle Integrationen und hardwareabhängige Funktionen sind ausdrücklich gekennzeichnet. Historische Berichte gelten für den jeweils beschriebenen Stand und sind keine Garantie für neuere Builds.

## Dokumentation und Sprachen

Verwenden Sie die Sprachauswahl für die Dokumentation, um das vollständige Handbuch, Funktionsinventar, Fehlerbehebung, Entwicklungshinweise, Forschungsberichte und historische Testberichte in einer der 27-Anwendungssprachen zu öffnen. Die generierten Übersetzungen sind maschinell unterstützt und verfügen über Strukturprüfungen; Sie wurden nicht alle von Muttersprachlern bewertet. Technische Kennungen und ausführbare Beispiele bleiben unverändert. Die Originalquelle bleibt zum Vergleich verfügbar. Die übersetzte Lizenzanalyse dient der Information und ersetzt nicht die ursprünglichen rechtlichen Bestimmungen Dritter oder eine endgültige EULA.

## Installation und erster Start

MAKRIVO richtet sich zunächst an Windows 11 x64. Bewahre alle Dateien und Unterordner des ausgelieferten Pakets zusammen auf und starte `MAKRIVO.exe`. Ein ausgechecktes Quellcode-Repository ist kein Installationsprogramm. Eigenständige Pakete enthalten die benötigte .NET-Laufzeit; frameworkabhängige Pakete benötigen die passenden .NET-10-Desktop- und ASP.NET-Laufzeiten. Für ONNX und weitere Begleitfunktionen müssen die jeweiligen Hostprogramme enthalten sein.

1. Starte MAKRIVO und wähle die Oberflächensprache in den Einstellungen. Die automatische Auswahl folgt Windows. Das Tastaturlayout wird unabhängig davon eingestellt.
2. Wähle ein Profil und füge über den durchsuchbaren Aktionskatalog eine Kachel hinzu. Gib ihr einen eindeutigen Namen und wähle ihre Aktionen.
3. Prüfe die Schritte und weise bei Bedarf eine Tastenkombination zu. Vermeide Kombinationen, die Windows oder die Zielanwendung bereits verwendet.
4. Speichere den Entwurf. Ungespeicherte Änderungen sind noch nicht Teil der gespeicherten Konfiguration.
5. Teste zunächst eine harmlose Aktion, etwa das Öffnen eines Dokuments, und prüfe die Rückmeldung zur Ausführung.

Beim Schließen des Hauptfensters verbleibt die Anwendung normalerweise im Infobereich. Doppelklicken Sie auf das Taskleistensymbol, um es erneut zu öffnen, oder verwenden Sie zum Beenden das Taskleistenmenü. Der Beginn mit Windows ist optional und zunächst deaktiviert. Die Pausensteuerung unterbricht Bindungen; Das Symbol zeigt ein Pausenzeichen. Brechen Sie die Ausführung von Makros über die Benutzeroberfläche, die Taskleiste oder die konfigurierte Abbruchverknüpfung ab. Der Standardwert ist `Ctrl+Alt+Escape`.

## Kacheln, Aktionen und Befehlspalette

Kacheln gehören zu Profilen und können mehrere geordnete Schritte enthalten. Der Editor unterstützt Speichern/Verwerfen, Vorschau, Rückgängig/Wiederherstellen und Neuanordnen von Schritten. Der Katalog bietet Voreinstellungen für Programme, Dokumente, URLs, Text, Zwischenablage, Verknüpfungen, Medien, Audio, Fenster, Systemaktionen und unterstützte Integrationen. Eine Voreinstellung, die Parameter erfordert, wird als Entwurf geöffnet.

Öffnen Sie die Palette mit `Ctrl+K`, während MAKRIVO den Fokus hat. Durchsuchen Sie gespeicherte Aktionen, Voreinstellungen, Profilwechsel und Tools. Pfeiltasten auswählen; Enter aktiviert; `Ctrl+Enter` bearbeitet ein unterstütztes Element; Escape schließt sich. Einige gebrauchsfertige Befehle werden sofort ausgeführt, während konfigurierte Aktionen überprüft werden müssen. Eine separate globale Palettenverknüpfung kann konfiguriert werden; Neue Konfigurationen verwenden `Ctrl+Shift+Space`.

Text- und Zwischenablageschritte unterstützen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` und `{{user}}`. Die Datumsformatierung ist `yyyy-MM-dd`; Die Zeitformatierung ist `HH:mm`. Unbekannte Variablen bleiben unverändert. Das Lesen der Zwischenablage erfolgt nur auf Anfrage. Eine Zwischenablageaktion schreibt Text, ohne ihn automatisch einzufügen.

Über die Oberfläche gestartete Aktionen können sich an die zuletzt aktive externe Anwendung richten. Wenn das Ziel geschlossen wurde oder Windows den Vordergrundwechsel blockiert, aktiviere das Zielfenster manuell und versuche es erneut. Die erfolgreiche Zustellung eines Tastendrucks belegt nicht, dass die Zielanwendung ihre Aufgabe erledigt hat.

## Profile und Arbeitsplatz-Assistent

Profile können anhand eines Prozessnamens und eines Teils des Fenstertitels ausgewählt werden. Sind beide Kriterien gesetzt, müssen beide zutreffen. Treffen mehrere Profile zu, entscheidet ihre Reihenfolge; andernfalls greift das Standardprofil. Aktiviere die automatische Auswahl ausdrücklich. Die Profilsperre verhindert automatische Wechsel während der aktuellen Sitzung.

Öffnen Sie „Workplace Setup“ über „Extras“ oder die Palette. Wählen Sie Programme, Dokumente, Websites, ein Audio-/Anzeigeziel oder eine OBS-Szene aus. Der Assistent erstellt Arbeitsplatz-, Meeting-, Streaming-, Forschungs- oder Projektprofile. Überprüfen Sie die generierten Schritte vor dem Speichern. Setup führt die vorbereiteten Aktionen nicht aus.

## Makros, Bedingungen und Aufnahme

Makros führen ihre Schritte der Reihe nach aus und unterstützen Wartezeiten, Wiederholungen und Abbrüche. Die allgemeine Wiederholungsgrenze beträgt 100. Verwenden Sie die Vorschau, bevor Sie Aktionen ausführen, die in eine andere Anwendung eingreifen oder den Systemstatus ändern.

„Warten auf Zielfenster“ akzeptiert einen Prozess, eine Titelteilzeichenfolge und eine Zeitüberschreitung von 1 bis 600,000 ms. Es ist mindestens ein Filter erforderlich. Ein passendes sichtbares Fenster muss reagieren und in den Vordergrund treten, bevor die nachfolgenden Schritte fortgesetzt werden. Ein Bereitschaftsfenster ist kein Beweis dafür, dass ein Dokument oder ein Dienst vollständig geladen wurde.

Der Workflow-Editor fügt Dateiexistenz-, Prozessausführungs- und Fenstertitelbedingungen hinzu. Eine falsche Bedingung überspringt den Schritt und kann weitere Schritte nach vorne überspringen. Negieren Sie die Bedingung, um einen alternativen Zweig auszudrücken. Wählen Sie bewusst „Stopp bei Fehler“ oder „Fortfahren bei Fehler“. Wenn Sie fortfahren, können spätere Schritte ohne ihre erwarteten Voraussetzungen zurückbleiben.

Der Aktionsrekorder erfasst Tastenkombinationen und Wartezeiten, nicht normales Tippen. Es unterstützt Modifikator-Shortcuts und Funktionstasten. Escape stoppt die Aufzeichnung. Überprüfen Sie die Liste, übernehmen Sie sie als nicht gespeicherten Kachelentwurf und speichern Sie sie dann explizit. Die Aufzeichnung von UI-Klicks ist ein separates Tool, das im Folgenden beschrieben wird.

## Tastaturen, Gesten und Ebenen

Die Tastaturseite listet erkannte Schnittstellen und Referenzlayouts auf. Eine einzelne physische Tastatur kann mehrere Schnittstellen verfügbar machen. Wählen Sie die richtige Sprache und Geometrie aus; Ein Produktname allein identifiziert kein ISO- oder ANSI-Layout.

Normale globale Hotkeys gelten für alle Tastaturen. Gerätespezifische Raw-Input-Zuordnungen erkennen ein bestimmtes Gerät und dessen Scan-Code, einschließlich unterstützter Funktions- und Medientasten. **Raw Input unterdrückt die normale Windows-Eingabe nicht. Eine exklusive Zweittastatur ist noch nicht produktionsreif.** Dafür fehlt ein signierter, lizenzierter und geprüfter Filtertreiber. MAKRIVO installiert keinen solchen Treiber.

Unterstützte Sondertasten können beim unmittelbaren Drücken, kurzen Antippen, Halten oder doppelten Antippen auslösen. Eine gehaltene Ebenentaste aktiviert vorübergehend eine andere Belegung; beim Loslassen wird die Grundebene wiederhergestellt. Tasten zur Ebenenauswahl führen kein eigenes Makro aus. Die Halteschwelle lässt sich zwischen 150 und 5,000 ms einstellen, das Zeitfenster für doppeltes Antippen zwischen 150 und 1,000 ms. Diese Belegungen sind von gewöhnlichen globalen Tastenkombinationen getrennt.

Der Sondertasten-Assistent erkennt kompatible Tasten, ohne während der Erfassung deren zugewiesene Aktionen auszuführen. Die Diagnose erklärt nicht verfügbare Geräte, pausierte Belegungen, unpassende Profile und Konflikte. Das Overlay zeigt das gewählte Layout und die Belegungen; standardmäßig wird es durch Halten von `Ctrl+Alt+F1` eingeblendet.

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

Audioaktionen stellen die Lautstärke unterstützter Ausgabe- oder Mikrofon-Endpunkte auf 0 bis 100 Prozent und können deren Stummschaltung ein-, ausschalten oder umschalten. Wähle einen bestimmten Endpunkt oder das Windows-Standardgerät. Fehlt ein gespeichertes Ziel, erscheint ein Fehler; die Aktion steuert nicht stillschweigend ein anderes Gerät. Aktualisiere die Geräteliste nach dem erneuten Anschließen. Die Endpunktsteuerung bietet keinen unabhängigen Lautstärkemixer für einzelne Anwendungen.

Anzeigeaktionen erhöhen/verringern die Helligkeit um 10 Prozentpunkte oder legen Sie einen Wert von 0 bis 100 fest. Interne Displays nutzen unterstützte WMI-Schnittstellen; Externe Monitore erfordern DDC/CI-Unterstützung und normalerweise die entsprechende Monitormenüeinstellung. Docks, Adapter und Kabel können die Steuerung blockieren. Wählen Sie alle unterstützten Displays oder einen Monitor. Durch die erneute Verbindung können verfügbare Identitäten geändert werden. Überprüfen Sie das gespeicherte Ziel. Fehlende Messwerte werden als nicht verfügbar und nicht als Messwerte angezeigt.

OBS erfordert, dass WebSocket v5 in den OBS Tools aktiviert ist. Konfigurieren Sie den Endpunkt, testen Sie die Verbindung und laden Sie Szenen-/Eingabenamen vor dem Speichern. Zu den unterstützten Aktionen gehören Szenenwechsel, Starten/Stoppen/Umschalten von Stream/Aufnahme, Anhalten/Fortsetzen der Aufnahme und Eingangsstummschaltung. Passwörter können im Windows Credential Manager gespeichert werden und sind von Konfigurationsexporten ausgeschlossen. Ein erfolgreicher OBS-Befehl stellt nicht jeden Downstream-Streaming-Effekt her.

## Ergebnisprüfung und Ausführungsverlauf

Aktivieren Sie „Ergebnis überprüfen“ für geeignete Schritte. Die App unterscheidet zwischen Preflight-Checks, bestätigten Effekten, Ausfällen, Abbrüchen und nicht verifizierbaren Effekten. Das Vorhandensein eines Prozesses ist keine Anwendungsbereitschaft. Relative Änderungen, Verknüpfungen und Texteingaben können keinen pauschalen Erfolgsanspruch erhalten. Überprüfen Sie das Aktivitätsprotokoll und den Überprüfungsverlauf, wenn eine Aktion fehlschlägt. Konfigurationsfehler sollten korrigiert werden, bevor das gesamte Makro erneut ausgeführt wird.

## UI-Automation und Klickaufnahme

Füge einen Schritt für die UI-Automation hinzu, wähle die Operation und erfasse das Ziel unter dem Mauszeiger nach der angezeigten Verzögerung. Die Erfassung allein führt keine Aktion aus. Prüfe den Selektor und verlange einen eindeutigen Treffer. Unterstützte Operationen sind `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` und `WaitState`. Passwortfelder, deaktivierte Bedienelemente und mehrdeutige Selektoren werden abgewiesen.

Die UI-Automatisierung läuft in einem Begleitprozess mit begrenzter Ausführungsdauer. Sie benötigt die Windows-Barrierefreiheitsschnittstellen der Zielanwendung und passende Berechtigungen. Der Klickrekorder erfasst unterstützte Aktionen und Pausen, aber keine allgemeine Texteingabe. Prüfe und validiere seinen Entwurf vor der Übernahme. Änderungen an der Oberfläche einer anderen App können Selektoren ungültig machen. Die Prozesstrennung begrenzt Absturzfolgen und ermöglicht den Abbruch, ist aber keine Sicherheits-Sandbox.

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

Dieser optionale, experimentelle Assistent ist standardmäßig deaktiviert. Öffne ihn über die Werkzeuge, aktiviere ihn ausdrücklich, wähle eine Laufzeit und ein Modell und füge nur die Ressourcen hinzu, die er berücksichtigen soll. Beschreibe dein Ziel, erstelle einen Entwurf, prüfe offene Anforderungen und übertrage das Ergebnis in den normalen Editor. Nichts wird automatisch ausgeführt; Speichern und Starten bleiben ausdrückliche Schritte. Es gibt keinen automatischen Wechsel auf eine andere Laufzeit.

| Laufzeit | Einrichtung |
| --- | --- |
| Ollama | Separater lokaler Dienst und ein lokal installiertes Modell unter `127.0.0.1:11434`. Deklarierte Remote-/Cloud-Modelle werden abgelehnt. |
| LM Studio | Lokaler Server mit einem lokal geladenen Modell und JSON-Schema-Unterstützung unter `127.0.0.1:1234`. Aktivieren Sie kein Remote-Routing für die lokale Verarbeitung. |
| llama.cpp | Lokales `llama-server` und kompatibles Modell mit JSON-Schema-Unterstützung unter `127.0.0.1:8080`. |
| ONNX Laufzeit GenAI CPU | Vertrauenswürdiger kompatibler lokaler Textmodellordner GenAI; Wählen Sie `genai_config.json`. Inference verwendet einen gebündelten Begleitprozess ohne HTTP. |

HTTP-Clients verwenden nur Loopback, verwenden keine Proxys und folgen keinen Weiterleitungen. MAKRIVO kann nicht zuverlässig feststellen, ob ein kompatibler lokaler Server intern einen Remote-Dienst aufruft. Authentifizierte lokale Server sind in dieser Version nicht konfigurierbar. Modellgewichte, Python und lokale Inferenzserver werden nicht automatisch heruntergeladen oder installiert.

ONNX-Pakete benötigen `tokenizer.json` und eine gültige Chatvorlage. In `tokenizer_config.json` werden folgende Formen unterstützt: `chat_template` als Zeichenfolge, ein Wörterbuch mit `default` als Zeichenfolge oder eine eindeutig zuordenbare Liste benannter Vorlagen mit Standardauswahl. Fehlt die Vorlage oder ist ihr Wert null, dient `chat_template.jinja` als Ersatz. Leere, mehrdeutige, übergroße Vorlagen und Vorlagen mit falschem Datentyp werden abgewiesen. Beliebige ONNX-Dateien, Embedding- oder Bildmodelle und ausschließlich für GPUs ausgelegte Pakete ersetzen keine kompatiblen GenAI-Textmodelle. Die mitgelieferte GenAI-Laufzeit hat die Version 0.17.1 und nutzt die CPU. Verwende ausschließlich vertrauenswürdige Modelle; die Prozesstrennung ist keine Sandbox.

Der Assistent unterstützt ausgewählte Programme ohne Argumente, ausgewählte Dokumente und Websites sowie das Stummschalten des Mikrofons auf ausdrücklichen Wunsch. Er erstellt keine Nachrichten, Termine, beliebigen Skripte, frei gewählten Klickaktionen oder Downloads und wechselt keine Headsets. Das Ziel und die Ressourcennamen werden an die gewählte lokale Laufzeit übermittelt; Ressourcenpfade und Dateiinhalte werden nicht zusätzlich übergeben. Die eingegebene Zielbeschreibung kann selbst personenbezogene Daten enthalten. Für das Netzwerkverhalten der lokalen Laufzeit bleibt der Nutzer verantwortlich.

Das Anforderungsbudget ONNX beträgt 90 Sekunden, einschließlich Modellladen, bis zu 3,072 Eingabeaufforderungstoken und 1,024 generierten Token, ebenfalls begrenzt durch den Modellkontext. Fehlerhafter/abgeschnittener JSON-Code und unbekannte Ressourcen-IDs werden abgelehnt. Abbrechen oder schließen, um eine Anfrage zu beenden. Ein Zufallsmodell-Laufzeittest stellt die Ausführung fest, nicht jedoch die nützliche Planungsqualität oder Zeitersparnis. Lokale Experimentprotokolle enthalten Ereigniskennungen und -zeiten, keinen Zieltext oder Dokumentinhalt. Sie werden nicht automatisch hochgeladen.

Die spätere Prüfung mit einem kleinen Instruct-Modell lieferte in vier deutschen und englischen Versuchen keinen gültigen Plan. Die native Inferenz funktionierte, das Modell erfüllte jedoch die strengen Anforderungen an das Planformat nicht. Das ist ein negatives Qualitätsergebnis, keine Modellempfehlung und kein Nachweis für einen nützlichen Assistenten. Die genaue Modellrevision und die Grenzen des Versuchs stehen im datierten Testbericht zur lokalen Laufzeit.

## Import, Export, Synchronisierung und Sicherung

Überprüfen Sie die Importvorschau, bevor Sie eine Konfiguration ersetzen. Importierte Aktionen werden deaktiviert gestartet und die portable Synchronisierung entfernt hardwarespezifische Bindungen. Ordnen Sie Geräte neu zu und aktivieren Sie Aktionen gezielt nach der Inspektion. Überprüfen Sie die Text-/Aktionsinhalte, bevor Sie einen Export freigeben. Der Ausschluss von Passwörtern führt nicht dazu, dass alle vom Benutzer erstellten Aktionsinhalte nicht vertraulich sind.

Die Profilsynchronisierung tauscht Dateien über einen ausgewählten Ordner aus. Optional kann dieser von einem bereits vorhandenen Cloud-Client verwaltet werden. MAKRIVO stellt kein Cloud-Konto bereit und speichert keine Cloud-Zugangsdaten. Revisionsstände verhindern unbemerkte Überschreibungen. Aktionsinhalte werden nur nach ausdrücklicher Auswahl einbezogen. Sichere die Konfiguration vor einem Import, der sie ersetzt. Standardmäßig liegt sie unter `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; der historische Ordnername bleibt aus Kompatibilitätsgründen erhalten. Schließe die App, bevor du die Datei manuell kopierst oder wiederherstellst. Auf einem anderen PC müssen gespeicherte SDK-Freigaben und durch das Betriebssystem geschützte Zugangsdaten möglicherweise neu eingerichtet werden.

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

Das vollständige Veröffentlichungsskript stellt die Begleitprogramme, Dateien des mobilen Panels, das Beispiel-Plugin, die ONNX-Laufzeit und vorhandene Hinweise zu Abhängigkeiten zusammen. Bewahre seine Unterordner auf. Ein erfolgreicher Build ist weder ein signiertes Installationsprogramm noch eine rechtliche Vertriebsfreigabe. Quellcode und ausgelieferte Builds können sich unterscheiden; vergleiche Dokumentationsstand und Commit mit der installierten Version.

## Lizenzierung, Datenschutz und Freigabestand

Vorgesehen ist ein einmaliger Kauf mit dauerhaftem Nutzungsrecht für die gekaufte Hauptversion. Endpreis, zulässige Geräteanzahl und Bedingungen für Versionsupgrades müssen in den tatsächlichen Verkaufsbedingungen festgelegt werden. Dieses Handbuch belegt kein produktionsreifes Kauf- oder Aktivierungssystem. Für die alltägliche Steuerung ist kein Cloud-Konto erforderlich.

Verteile proprietäre SDK-Binärdateien nur mit nachgewiesener Erlaubnis. Bewahre Lizenztexte und Hinweise Dritter für jede ausgelieferte Abhängigkeit auf, einschließlich .NET- und ONNX-Paketen. Für Modellgewichte gelten eigene Lizenzbedingungen. Die datierte Lizenzprüfung liegt vor der Aufnahme einiger neuer Abhängigkeiten und erteilt dafür keine Freigabe. Allein die öffentliche Verfügbarkeit des Repositorys gewährt keine allgemeine Open-Source-Lizenz.

Die Abnahme mit echter Herstellerhardware, der LAN-Betrieb mit Telefonen und Tablets, Übergänge zwischen unterschiedlichen Monitoren und DPI-Einstellungen, Dauerbetrieb, vollständige sprachliche Prüfung, exklusive Treiberunterstützung und nützliche KI-Planungsqualität bleiben eigenständige Prüfpunkte. Das bloße Vorhandensein einer Funktion belegt keine erfolgreiche Abnahme.

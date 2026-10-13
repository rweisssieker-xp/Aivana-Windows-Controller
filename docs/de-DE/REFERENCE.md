<!-- generated-doc locale=de-DE source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../README.md)

# MAKRIVO

[Vollständige Dokumentation in allen 27 Sprachen](README.md) · [Vollständiges Benutzerhandbuch](HANDBOOK.md)

**Von Aivana.** [Aktuelle Funktionsübersicht und Freigabegrenzen](FEATURES.md) · [Testbericht zu den Erweiterungen](testing/2026-10-12-six-extensions.md) · [Prototyp des lokalen KI-Assistenten](testing/2026-10-12-local-meeting-assistant.md)

**Englisch (US)** | [Deutsch](REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Ein lokaler Windows-Hotkey-Controller mit Kacheln, Profilen, Makros und Tastaturerkennung im Stream Deck-Stil. Die Schnittstelle unterstützt 27-Sprachen. Für die alltägliche Nutzung ist kein Konto oder eine Cloud-Verbindung erforderlich.

[Dokumentation](README.md) · [Überprüfung](VERIFICATION.md) · [Feature Testbericht](testing/2026-10-09-usp-release.md)

## Erste Schritte

Nach der Veröffentlichung befindet sich die Anwendung unter `artifacts/win-x64/MAKRIVO.exe`. Behalten Sie den gesamten Ausgabeordner bei; Sie können es woanders kopieren. Windows 11 x64 ist das ursprüngliche Ziel. Build-Artefakte sind von Git ausgeschlossen; Ein Quell-Checkout enthält keine vorgefertigte ausführbare Datei.

Installieren Sie das in `global.json` angegebene .NET 10 SDK, um es aus der Quelle zu erstellen und auszuführen:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Veröffentlichen Sie einen eigenständigen Build, ohne dass eine separat installierte .NET-Laufzeitumgebung erforderlich ist:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Grundlegender Arbeitsablauf

1. Wählen Sie ein Profil aus und erstellen Sie eine Aktionskachel.
2. Legen Sie Namen, Symbol, Farbe und Tastenkombination fest. Strg/Alt/Umschalt-Kombinationen reduzieren die versehentliche Aktivierung während der Eingabe.
3. Fügen Sie einen oder mehrere Schritte hinzu: Anwendungen, Dateien, Websites, Text, Verknüpfungen, Medien, Lautstärke, Profilwechsel oder Verzögerungen.
4. Speichern. Änderungen bleiben ein Entwurf, bis sie gespeichert werden. Verwerfen stellt die gespeicherte Version wieder her. Die Play-Taste der Kachel und ihr Hotkey verwenden denselben Ausführungspfad.
5. Ordnen Sie Kacheln und Makroschritte per Drag-and-Drop oder Pfeiltasten neu an. Vorlagen vereinfachen die Einrichtung. Makros können bis zu 100-mal wiederholt werden. Abbrechen über die Benutzeroberfläche, das Taskleistenmenü oder standardmäßig Strg+Alt+Escape.

Anwendungsspezifische Profile verwenden einen Prozessnamen ohne vollständigen Pfad. Profilreihenfolge bricht Bindungen; Das Standardprofil ist das Fallback. Die automatische Profilauswahl muss in den Einstellungen aktiviert sein.

Durch das Schließen des Hauptfensters verbleibt die App standardmäßig in der Taskleiste. Doppelklicken Sie auf das Taskleistensymbol, um es erneut zu öffnen. Verwenden Sie zum Beenden das Taskleistenmenü. Der Start mit Windows ist optional und standardmäßig deaktiviert. Hotkeys können zentral pausiert werden.

## Aktionen und Befehlspalette

Aktion hinzufügen öffnet einen durchsuchbaren Katalog mit 90-Voreinstellungen in acht Kategorien. Zu jedem gibt es eine Beschreibung und eine Vorschau. Der Makroeditor verwendet denselben Katalog, um Schritte hinzuzufügen oder zu ersetzen. Durch die Auswahl einer Voreinstellung in diesem Editor-Workflow wird ein Entwurf ohne Ausführung erstellt.

Die Befehlspalette wird über die Kopfzeile oder mit Strg+K innerhalb der App geöffnet. Es durchsucht gespeicherte Aktionen in Profilen, Katalogvoreinstellungen, Profilwechsel, Navigation, Pause/Fortsetzung, Makroabbruch und Import/Export. Filtern Sie nach gespeicherten Aktionen, Katalogfunktionen oder App-Befehlen. Pfeiltasten wählen aus, Enter aktiviert, Strg+Enter bearbeitet einen bearbeitbaren Eintrag und Escape wird geschlossen. Favoriten werden zuerst angezeigt. Einsatzbereite Katalogbefehle können sofort ausgeführt werden; Voreinstellungen, die eingerichtet werden müssen, öffnen Sie einen Entwurf. Einstellungen können eine globale Palettenverknüpfung definieren. Für neue Konfigurationen verwenden Sie Strg+Umschalt+Leertaste; Bestehende Konfigurationen erhalten nicht automatisch eine neue globale Verknüpfung.

Audioaktionen stellen die Lautstärke präzise von 0 bis 100 Prozent ein und schalten Ausgabe- und Mikrofongeräte stumm, heben die Stummschaltung auf oder schalten sie um. Wählen Sie einen bestimmten Endpunkt aus oder lassen Sie ihn für die Standardeinstellung Windows deaktiviert. Ein nicht verfügbarer gespeicherter Endpunkt erzeugt einen Fehler, anstatt ein anderes Gerät zu steuern. „Geräte aktualisieren“ lädt die angeschlossenen Geräte neu.

OBS Studio verwendet WebSocket v5: Aufnahme starten/stoppen/umschalten/pausieren/fortsetzen; Streaming starten/stoppen/umschalten; Szenen wechseln; Schalten Sie den Eingang stumm. Aktivieren Sie den WebSocket-Server unter Tools in OBS und legen Sie seinen Endpunkt im Editor fest. „Verbindung prüfen und Namen laden“ liest die OBS-Version, Szenen und Eingänge, ohne eine Aufnahme oder einen Stream zu starten. Wählen Sie Namen aus der Liste aus oder geben Sie sie manuell ein. Passwörter können im Windows Credential Manager gespeichert werden und sind von exportierten Konfigurationen ausgeschlossen. Nicht verfügbare OBS-Verbindungen melden einen Fehler.

Profile können einem Prozessnamen und einem Teil eines Fenstertitels entsprechen. Beide angegebenen Bedingungen müssen übereinstimmen. Das Sperrprofil verhindert den automatischen Wechsel für die Sitzung. Capture Conditions from Foreground verwendet die zuletzt fokussierte externe Anwendung.

Text- und Zwischenablageaktionen unterstützen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` und `{{user}}`. Datumsangaben verwenden `yyyy-MM-dd`; mal `HH:mm` verwenden. Unbekannte Platzhalter bleiben unverändert. `{{profile}}` identifiziert das Profil der gestarteten Aktion; `{{process}}` identifiziert den externen Anwendungskontext. Der Inhalt der Zwischenablage wird nur gelesen, wenn dieser Platzhalter verwendet wird. Die Zwischenablage-Aktion schreibt Text, fügt ihn jedoch nicht automatisch ein.

Fensteraktionen minimieren, maximieren, wiederherstellen oder ordnen ein Fenster auf der linken/rechten Seite des Arbeitsbereichs seines Monitors an. Die Systemaktion sperrt Windows. Diese Aktionen werden nur ausgeführt, wenn sie explizit aufgerufen werden.

Die Vorschau zeigt Schritte, Wiederholungen und Variablen ohne Ausführung. Beim Rückgängigmachen und Wiederherstellen bleiben bis zu 100-Entwurfsstatus erhalten; Textfelder behalten die native Bearbeitung mit Strg+Z bei. Markieren Sie Favoriten im Editor oder Kachelmenü und filtern Sie sie im Dashboard. Die Anzahl der Ausführungen gilt nur für die aktuelle Sitzung.

Über die Benutzeroberfläche gestartete Text-, Verknüpfungs- und Fensteraktionen aktivieren das letzte externe Zielfenster. Die Palette zeigt diesen Zielkontext an. Wenn das Ziel geschlossen wurde oder Windows die Aktivierung verweigert, wird die Ausführung mit einer Meldung gestoppt.

## Tastaturunterstützung und Feedback

Der Sondertasten-Assistent befindet sich auf der Tastaturseite und in der Palette. Es listet die von iCUE gemeldeten G-Tasten auf. „Tastendruck erkennen“ erfasst sie vorübergehend, ohne die ihnen zugewiesenen Aktionen auszuführen. Normale Bindungen kehren nach Erkennung, Stornierung oder Schließung zurück. Eine Katalogfunktion und ein ausgewählter Schlüssel werden als nicht gespeicherter Entwurf geöffnet. Doppelte Bindungen werden überprüft.

Das Tastatur-Overlay zeigt das ausgewählte Referenzlayout und die aktiven Bindungen. Halten Sie Strg+Alt+F1 gedrückt oder schalten Sie die Option über die Palette oder die Tastaturseite um. Es nimmt keinen Fokus und lässt Mausklicks durch. Profil- und Bindungsänderungen werden aktualisiert, solange sie sichtbar sind. Normale Windows-Tastendrücke werden hervorgehoben. Herstellerspezifische Steuerungen liefern möglicherweise kein entsprechendes Windows-Tastensignal.

Das Ausführungsfeedback zeigt Warten, Schritt-/Wiederholungsfortschritt, Abschluss, Abbruch oder Fehler an, ohne den Fokus zu nehmen oder eingegebenen Text anzuzeigen. „Befehle ausgeführt“ bestätigt die Befehlsausführung und nicht einen unabhängig abgefragten Live-Status in einer Drittanbieteranwendung. Fehlerdetails werden im Aktivitätsprotokoll angezeigt.

## Geräte und Corsair

Corsair G1–G6 kann direkt über das offizielle iCUE SDK zugewiesen werden. Wählen Sie das Gerät und die Taste im Aktionseditor aus und speichern Sie sie oder bereiten Sie einen Entwurf aus der K100-Tastaturansicht vor. Bindungen gelten für das ausgewählte Gerät und das aktive Profil. Pause deaktiviert sie auch. iCUE muss ausgeführt werden und den Zugriff erlauben. Fn, die Rad-, Profil- und Sperrsteuerungen iCUE können nicht über diese G-Tasten-API zugeordnet werden.

Auf der Seite „Geräte“ werden die Windows-Tastaturschnittstellen aufgelistet und regelmäßig sowie bei der Verbindung von Geräten aktualisiert. Eine physische Tastatur kann mehrere HID-Schnittstellen offenlegen. Standardmäßige globale Hotkeys funktionieren herstellerübergreifend.

Die Corsair-Erkennung verwendet verfügbare Geräteinformationen. Für die RGB-Steuerung sind kompatible Hardware mit iCUE und das offizielle iCUE SDK erforderlich. Die App meldet die tatsächliche Verfügbarkeit. Andere Hersteller erhalten derzeit Erkennung und Standardaktionen. Außerhalb der G-Key-Integration von Corsair funktionieren proprietäre Steuerelemente, wenn Windows sie als reguläre Eingabe empfängt. Die App installiert keinen Tastaturtreiber.

## Sprachen

Die Einstellungen bieten 27-Sprachen: Deutsch, Englisch, Französisch, Spanisch, Italienisch, Portugiesisch, Niederländisch, Polnisch, Tschechisch, Slowakisch, Ungarisch, Rumänisch, Bulgarisch, Griechisch, Dänisch, Schwedisch, Norwegisch, Finnisch, Estnisch, Lettisch, Litauisch, Slowenisch, Kroatisch, Ukrainisch, Russisch, Türkisch und Serbisch in lateinischer Schrift. Automatisch folgt der Anzeigesprache Windows. Übersetzungen werden lokal gebündelt. Layouts, Hotkeys und vom Benutzer erstellte Namen sind unabhängig von der Sprache der Benutzeroberfläche.

Die vollständige Repository-Dokumentation ist in allen 27 Anwendungssprachen über die [Spracheindex](README.md)Das ist Übersetzungen werden mithilfe von Maschinen, mit Original-Quellverbindungen und strukturellen Überprüfungen; Native-Speaker-Überprüfung ist getrennt. Die Anwendung akzeptiert auch die regionalen Codes `en-US`, `de-DE`, `fr-FR` und `it-IT` und verwendet ihre Zahlenformate. Die bestehenden Sprachcodes bleiben kompatibel.

## Layouts und Herstellerrecherche

Die Tastaturansicht bietet vollständige und tenkeylose ISO-DE/ANSI-US-Referenzen sowie Corsair K100 RGB-Layouts. Die Geometrie stellt Positionen, breite/hohe Tasten und ISO-Eingabe dar. Standardlayouts sind generische Referenzen. K100-Layouts folgen dokumentierten Kontrollen, ohne den Anspruch auf millimetergenaue Abmessungen zu erheben. Wählen Sie die richtige Sprach-/Geometrievariante: Ein Modellname oder eine USB-VID/PID legt keine bestimmte Variante fest.

Die Zuordnung löst physische Scancodes mithilfe des aktiven Eingabelayouts Windows auf. Der Workflow bereitet einen Entwurf vor und führt Speicher-/Konfliktprüfungen durch. Standardbindungen sind globale Windows-Hotkeys, die nicht auf eine verbundene Tastatur beschränkt sind. Corsair G1–G6 verwenden die gerätespezifische iCUE-Integration. Andere proprietäre Steuerelemente erfordern einen Adapter oder vom Hersteller konfigurierte Verknüpfungen. Main Enter und Numeric-Keypad Enter teilen sich eine virtuelle Tastenidentität für globale Hotkeys.

Die [Herstellerforschung](research/tastaturhersteller-sonderfunktionen.md) und [CSV](research/tastaturhersteller-sonderfunktionen.csv) Abdecken 74 Marken/Herstellergruppen, die Konfiguratoren, Einträge SDKs und Licht-APIs unterscheiden. [Layout Beweise](research/keyboard-layout-evidence.md) Rekordmodell, SKU und Module Unterschiede. Forschungsabdeckung bedeutet nicht, dass die Hardware-Support implementiert wird.

## Daten

Die Konfiguration wird in lokalen Anwendungsdaten gespeichert. Beim Import/Export wird JSON verwendet. Beim Import wird eine Vorschau der Konfiguration angezeigt, die aktuelle Konfiguration nach Bestätigung ersetzt und importierte Bindungen deaktiviert, bis sie manuell aktiviert werden. Textaktionsinhalte werden nicht als Tastenanschlagprotokoll erfasst. Passwörter für den Anmeldeinformationsmanager sind von den exportierten Profilen getrennt.

## Überprüfung

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows-Prüfungen können die Hardware überprüfen und unterstützte iCUE-Bindungen vorübergehend konfigurieren/freigeben. Führen Sie sie ohne einen anderen Controller mit diesen Bindungen aus. Audio-Endpunktprüfungen ändern die Live-Lautstärke- oder Stummschaltungseinstellungen nicht.

Die native Eingabe kann die Windows-Berechtigungen nicht umgehen. Ein normal gestarteter Controller sendet möglicherweise keine Eingaben an eine Administratoranwendung. Reservierte oder belegte Hotkeys werden als Konflikte angezeigt.

Die [Dokumentation Index](README.md) Link historische Entwürfe, Pläne, Forschung und Datum Testberichte. Historische Pläne beschreiben ihren aufgezeichneten Umfang, nicht eine aktuelle Garantie für die Funktionsunterstützung.

### Workflow-Vorlagen und Zielfenster-Wartezeiten

Der Katalog umfasst sechs bearbeitbare Workflows: Arbeitsbereich, Besprechung, Streaming, Recherche, Projektwechsel und Textausschnitt. Suchen Sie im Katalog oder in der Befehlspalette nach `workflow.`. Wenn Sie einen Workflow auswählen, wird ein nicht gespeicherter Entwurf geöffnet. es läuft nie sofort. Alle Schritte bleiben erhalten, wenn Sie einen Workflow zu einem vorhandenen Makro hinzufügen oder ihn einer Corsair-Taste zuweisen. Konfigurieren Sie vor dem Speichern Beispiel-URLs, Programmpfade, Profile und OBS-Szenen.

Der Schritt **Wait for target window** akzeptiert einen Prozessnamen (mit oder ohne `.exe`), eine Titelteilzeichenfolge ohne Berücksichtigung der Groß-/Kleinschreibung und ein Zeitlimit von 1–600000 ms. Es ist mindestens ein Filter erforderlich; Beide Filter müssen im Auslieferungszustand übereinstimmen. Das Makro wird erst fortgesetzt, nachdem ein passendes sichtbares Fenster reagiert und den Vordergrund erreicht. Verwenden Sie einen bestimmten Titel, wenn mehrere Fenster einen Prozess gemeinsam nutzen. Bei Abbruch oder Zeitüberschreitung werden nachfolgende Schritte gestoppt. Die Fensterbereitschaft garantiert nicht, dass ein Dokument geladen wurde oder ein externer Dienst wie OBS WebSocket verbunden ist.

Displayhelligkeit: Suchen Sie im Aktionskatalog nach **Bildschirmhelligkeit erhöhen**, **Bildschirmhelligkeit verringern** oder **Bildschirmhelligkeit bei 50 Prozent**. Relative Aktionen ändern die Helligkeit um 10 Prozentpunkte; Die festen Werte reichen von 0 bis 100. Unterstützte interne Displays verwenden WMI; Für kompatible externe Monitore muss DDC/CI in ihrem Menü aktiviert sein. Standardmäßig betrifft die Aktion alle unterstützten Displays; Im Editor kann ein expliziter Monitor ausgewählt werden. Anschlüsse und Docks können den Support einschränken.

### Erweiterte Kontrollen

Öffnen Sie die Befehlspalette mit **Strg+K, während das App-Fenster den Fokus ** hat. Suchen Sie nach **Workflow Designer**, **Record Macro**, **Profile synchronisieren** oder **Key Diagnostics** (der Wortlaut folgt Ihrer ausgewählten Sprache). Der Workflow-Designer bearbeitet den ausgewählten Kachelentwurf: Datei-/Prozess-/Fensterbedingungen, Anzahl der Vorwärtssprünge und Fehlerbehandlung beim Stoppen/Fortfahren. Der Rekorder erfasst Abkürzungsakkorde und Wartezeiten, kein gewöhnliches Tippen; Escape stoppt die Aufzeichnung. Überprüfen Sie das Ergebnis, bevor Sie es übernehmen.

Wählen Sie in einer Helligkeitsstufe alle unterstützten Displays oder einen bestimmten Monitor aus. Ein nicht verfügbarer ausgewählter Monitor erzeugt einen Fehler, anstatt stillschweigend eine andere Anzeige zu ändern. Das Dashboard zeigt Live-Helligkeits-, Audio- und OBS-Messwerte an, wenn ihre jeweiligen Integrationen verfügbar sind; Ein nicht verfügbarer Messwert wird nicht als Messwert dargestellt.

Weisen Sie im Kacheleditor eine unterstützte physische Sondertaste zu und wählen Sie „Sofortiges Drücken, Tippen, Halten oder Doppeltippen“. Angehaltene Layer-Selektoren aktivieren vorübergehend eine andere Zuordnung und stellen beim Loslassen den vorherigen Layer wieder her. Gesten und Ebenen gelten für Corsair SDK-G-Tasten und gerätespezifische Standard-Sondertasten, nicht für gewöhnliche globale Hotkeys. Die Standardtastenerfassung unterstützt F13–F24 und Tastatur-Medientasten. Proprietäre Fn-, Profil- und Wählsteuerungen erfordern weiterhin einen kompatiblen Anbieteradapter oder eine Anbieterzuordnung zu einer Standardtaste. Raw Input beobachtet den Schlüssel und unterdrückt sein normales Windows-Verhalten nicht. Die Schlüsseldiagnose erklärt Profil-, Pausen-, Verbindungs- und Bindungsprobleme.

Bei der Profilsynchronisierung werden Dateien über einen von Ihnen ausgewählten Ordner ausgetauscht, einschließlich eines Ordners, der von Ihrem vorhandenen Cloud-Client verwaltet wird. Sehen Sie sich vor dem Importieren die Vorschau an. Importe starten deaktiviert und entfernen hardwarespezifische Zuordnungen. Revisionen verhindern stille Überschreibungen und das Einbeziehen von Aktionsinhalten ist eine explizite Option. MAKRIVO stellt kein Cloud-Konto bereit und speichert keine Cloud-Anmeldeinformationen. Die Taskleiste verwendet jetzt das eigene blaugrüne M-Symbol von MAKRIVO und zeigt bei Pause ein gelbes Pausenzeichen an.

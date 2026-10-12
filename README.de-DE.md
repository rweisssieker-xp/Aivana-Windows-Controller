# MAKRIVO

**Von Aivana.** [Aktuelle Featureliste und offene Freigaben](docs/FEATURES.md) · [Prüfstand der Erweiterungen](docs/testing/2026-10-12-six-extensions.md) · [Lokaler KI-Assistent als Prototyp](docs/testing/2026-10-12-local-meeting-assistant.md)

[English (US)](README.md) | **Deutsch** | [Français](README.fr-FR.md) | [Italiano](README.it-IT.md)

Lokales Hotkey-Panel für Windows mit Stream-Deck-artigen Kacheln, Profilen, Makros und Tastaturerkennung. Oberfläche in 27 Sprachen, kein Konto, keine Cloud.

[Dokumentation](docs/de-DE/README.md) · [Prüfung](docs/de-DE/VERIFICATION.md) · [Testbericht](docs/de-DE/testing/2026-10-09-usp-release.md)

## Start

Die veröffentlichte Anwendung liegt nach dem Build unter `artifacts/win-x64/MAKRIVO.exe`. Der gesamte Ordner wird benötigt; er kann an einen beliebigen Ort kopiert werden. Windows 11 x64 ist das erste Zielsystem. Build-Artefakte sind von Git ausgeschlossen; ein Quellcode-Checkout enthält keine fertige EXE.

Für den Start aus dem Quellcode ist das .NET-10-SDK erforderlich:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Eigenständige Ausgabe ohne vorinstallierte .NET-Laufzeit:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Bedienung

1. Profil auswählen und eine neue Kachel anlegen.
2. Namen, Symbol, Farbe und Tastenkombination einstellen. Kombinationen mit Strg/Alt/Shift vermeiden unbeabsichtigte Aktionen beim normalen Schreiben.
3. Einen oder mehrere Aktionsschritte hinzufügen. Programme, Dateien, Websites, Text, Tastenkombinationen, Medien, Lautstärke, Profilwechsel und Pausen sind verfügbar.
4. Speichern. Änderungen bleiben bis dahin als Entwurf erhalten; Verwerfen stellt den gespeicherten Stand wieder her. Der Play-Knopf einer Kachel oder ihr Hotkey startet dieselbe Ausführung.
5. Kacheln und Makroschritte lassen sich per Drag-and-drop oder Pfeiltasten umordnen. Vorlagen erleichtern neue Aktionen. Makros können bis zu 100-mal wiederholt werden. Abbruch über die Oberfläche, das Tray-Menü oder standardmäßig Strg+Alt+Escape.

Programmspezifische Profile erhalten einen Prozessnamen ohne vollständigen Pfad. Die Reihenfolge der Profile bestimmt die Priorität, wenn mehrere passen. Das Standardprofil gilt als Rückfallprofil. Automatische Profile müssen in den Einstellungen eingeschaltet werden.

Fenster schließen lässt die App standardmäßig im Infobereich weiterlaufen. Doppelklick auf das Tray-Symbol öffnet sie. Beenden erfolgt über das Tray-Menü. Autostart ist optional und standardmäßig deaktiviert. Hotkeys lassen sich zentral pausieren.

## Schnelle Steuerung und neue Aktionen

„＋ Aktion“ öffnet einen durchsuchbaren Aktionskatalog mit 90 Vorlagen in acht Kategorien. Jede Funktion zeigt Beschreibung und Vorschau. Derselbe Katalog steht im Makroeditor über „＋ Hinzufügen“ und „Funktion auswählen“ bereit. Die Auswahl erstellt einen bearbeitbaren Entwurf und führt nichts aus.

Audioaktionen können die Lautstärke genau zwischen 0 und 100 Prozent setzen sowie Ausgabe und Mikrofon stummschalten, aktivieren oder umschalten. Im Editor lässt sich ein konkretes Ausgabe- oder Mikrofongerät auswählen; ohne Auswahl gilt das Windows-Standardgerät. Ein nicht verfügbares gespeichertes Gerät führt zu einer Meldung, statt ein anderes Gerät zu steuern. „Geräte aktualisieren“ liest die aktuell verbundenen Geräte neu ein.

OBS Studio wird direkt über WebSocket v5 gesteuert: Aufnahme starten/stoppen/umschalten/pausieren/fortsetzen, Stream starten/stoppen/umschalten, Szene wechseln und Eingabe-Stummschaltung umschalten. Den WebSocket-Server in OBS unter Werkzeuge aktivieren und die Adresse im Editor einstellen. „Verbindung prüfen und Namen laden“ liest die OBS-Version, Szenen und Eingaben aus; anschließend können Namen aus der Liste übernommen oder manuell eingegeben werden. Diese Prüfung startet keine Aufnahme und keinen Stream. Das Passwort lässt sich in der Windows-Anmeldeinformationsverwaltung speichern und ist nicht Teil der exportierten Konfiguration. Ohne laufendes OBS meldet die Aktion einen Verbindungsfehler.

Die Befehlspalette öffnet sich über den Kopfbereich oder Ctrl+K im App-Fenster. Sie durchsucht gespeicherte Aktionen aller Profile, Katalogfunktionen und App-Befehle: Profilwechsel, Navigation, Pause/Fortsetzen, Makroabbruch sowie Import und Export. Ein Filter trennt gespeicherte Aktionen, Katalog und App-Befehle. Fertige Katalogbefehle lassen sich direkt starten; Funktionen mit fehlenden Angaben öffnen einen Entwurf. Pfeiltasten wählen, Enter startet, Ctrl+Enter bearbeitet einen bearbeitbaren Eintrag und Escape schließt. Favoriten stehen zuerst. Ein globaler Hotkey ist in den Einstellungen konfigurierbar; neue Konfigurationen verwenden Ctrl+Shift+Space. Bestehende Konfigurationen erhalten keinen neuen globalen Hotkey automatisch.

Profile können einen Prozessnamen und einen Teil des Fenstertitels enthalten. Beide angegebenen Bedingungen müssen passen; die Profilreihenfolge entscheidet bei mehreren Treffern. „Profil sperren“ verhindert automatische Wechsel für die laufende Sitzung. „Bedingungen aus Vordergrund übernehmen“ übernimmt das zuletzt fokussierte externe Programm.

Text und Zwischenablage unterstützen `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` und `{{user}}`. Das Datum verwendet yyyy-MM-dd, die Uhrzeit HH:mm. Unbekannte Platzhalter bleiben erhalten. `{{profile}}` bezeichnet das Profil, zu dem die gestartete Aktion gehört; `{{process}}` den externen Programmkontext. Inhalte aus der Zwischenablage werden nur gelesen, wenn der entsprechende Platzhalter vorkommt. Die Zwischenablage-Aktion schreibt Text; sie fügt ihn nicht automatisch ein.

Fensteraktionen minimieren, maximieren, stellen wieder her oder ordnen links/rechts im Arbeitsbereich des aktuellen Monitors an. Die Systemaktion sperrt Windows. Diese Aktionen starten ausschließlich bei einem ausdrücklichen Aktionsaufruf.

„Vorschau“ zeigt die geplanten Makroschritte, Wiederholungen und Variablen ohne Ausführung. „Rückgängig“ und „Wiederholen“ bearbeiten den aktuellen Entwurf mit bis zu 100 Zuständen. In Textfeldern bleibt die native Textbearbeitung über Ctrl+Z erhalten. Favoriten lassen sich im Editor oder Kachelmenü setzen und im Dashboard filtern. Ausführungshäufigkeit wird nur für die laufende Sitzung erfasst.

Für Text-, Tastenkürzel- und Fensteraktionen aus der Oberfläche wird das letzte externe Zielfenster aktiviert. Die Befehlspalette zeigt den Zielkontext an. Ist das Ziel geschlossen oder verweigert Windows die Aktivierung, wird die Ausführung mit einer Meldung gestoppt.

## Geräte und Corsair

„Sondertasten-Assistent“ ist in der Tastaturansicht und Befehlspalette erreichbar. Er zeigt die von iCUE gemeldeten G-Tasten an. „Tastendruck erkennen“ übernimmt diese Tasten vorübergehend, ohne ihre bestehenden Aktionen auszuführen. Nach Erkennung, Abbruch oder Schließen werden die normalen Bindungen wiederhergestellt. Eine Katalogfunktion wird mit der gewählten Taste als ungespeicherter Entwurf geöffnet; doppelte Belegungen werden geprüft.

„Tastatur-Overlay“ zeigt das gewählte Referenzlayout mit den aktiven Belegungen. Ctrl+Alt+F1 gedrückt halten oder das Overlay über die Befehlspalette beziehungsweise Tastaturansicht ein-/ausblenden. Das Overlay nimmt keinen Fokus an und lässt Mausklicks durch. Profil- und Belegungsänderungen erscheinen während der Anzeige; gewöhnliche Windows-Tastendrücke werden hervorgehoben. Nicht jede herstellerspezifische Taste liefert ein solches Windows-Signal.

Während einer Ausführung zeigt eine Statusanzeige Warten, Fortschritt je Schritt/Durchlauf, Abschluss, Abbruch oder Fehler. Sie enthält keine eingegebenen Textinhalte und nimmt keinen Fokus an. „Befehle ausgeführt“ bestätigt den Abschluss der Befehlsausführung, nicht einen unabhängig abgefragten Live-Zustand einer Drittanwendung. Fehlerdetails stehen im Aktivitätsverlauf.

Corsair G1–G6 können über das offizielle iCUE-SDK direkt einer Aktion zugeordnet werden. Im Aktionseditor unter „Corsair-G-Taste“ Gerät und Taste auswählen und speichern, oder in der K100-Tastaturansicht eine G-Taste anklicken und die Zuordnung als Entwurf übernehmen. Die Bindung gilt für das ausgewählte Gerät und das aktive Profil; Pause deaktiviert auch diese Auslöser. iCUE muss laufen und den Zugriff erlauben. Fn, iCUE-Rad, Profil- und Sperrtaste sind über diesen G-Tasten-Zugriff nicht belegbar.

## Sprache

Unter Einstellungen kann die Oberfläche zwischen 27 Sprachen wechseln: Deutsch, Englisch, Französisch, Spanisch, Italienisch, Portugiesisch, Niederländisch, Polnisch, Tschechisch, Slowakisch, Ungarisch, Rumänisch, Bulgarisch, Griechisch, Dänisch, Schwedisch, Norwegisch, Finnisch, Estnisch, Lettisch, Litauisch, Slowenisch, Kroatisch, Ukrainisch, Russisch, Türkisch und Serbisch in lateinischer Schrift. „Automatisch“ verwendet die Windows-Sprache. Die Übersetzungen sind lokal eingebettet; Tastaturlayout, Hotkeys und eigene Aktionsnamen bleiben unabhängig davon.

Die Geräteansicht listet Windows-Tastaturgeräte und aktualisiert sich beim Anstecken sowie regelmäßig. Manche physischen Tastaturen besitzen mehrere HID-Geräteeinträge. Standard-Hotkeys funktionieren unabhängig vom Hersteller auf allen Tastaturen.

Corsair-Geräte werden anhand verfügbarer Geräteinformationen erkannt. RGB-Steuerung setzt ein kompatibles Gerät, laufendes iCUE und das offizielle iCUE-SDK voraus. Ohne diese Voraussetzungen zeigt die App den tatsächlichen Status. Andere Hersteller erhalten zunächst Geräteerkennung und sämtliche Standardaktionen. Außerhalb der implementierten Corsair-G-Tasten-Anbindung funktionieren proprietäre Sondertasten, wenn sie Windows als reguläre Eingaben erreichen; die App installiert keinen Tastaturtreiber.

## Tastaturansicht und Herstellerrecherche

Die Tastaturansicht verwendet getrennte Profile für ISO-DE und ANSI-US, jeweils Vollformat und TKL, sowie Corsair K100 RGB. Tastenpositionen, breite bzw. hohe Tasten und ISO-Enter werden über Geometriedaten dargestellt. Standardprofile sind generische Referenzlayouts; das K100-Profil orientiert sich an den dokumentierten Bedienelementen, ohne Millimetermaße vorzugeben. Die Sprach-/Geometrievariante muss ausgewählt werden: ein erkannter Modellname oder USB-VID/PID beweist keine konkrete Variante.

Beim Zuordnen werden physische Scancodes mit dem aktiven Windows-Eingabelayout abgeglichen. Ein Klick öffnet einen Aktionsentwurf; die vorhandene Speicherung und Konfliktprüfung gelten weiter. Belegungen sind globale Windows-Hotkeys und nicht auf eine einzelne angeschlossene Tastatur beschränkt. Corsair G1–G6 verwenden die gerätebezogene iCUE-Anbindung. Andere proprietäre Bedienelemente benötigen einen passenden Adapter bzw. konfigurierte Hersteller-Shortcuts. Haupt- und Nummernblock-Enter teilen bei globalen Windows-Hotkeys dieselbe virtuelle Taste.

Die [Herstellerrecherche](docs/de-DE/research/tastaturhersteller-sonderfunktionen.md) und die [CSV](docs/de-DE/research/tastaturhersteller-sonderfunktionen.csv) beschreibt 74 Marken-/Herstellergruppen mit Sonderfunktionen und offiziellen Quellen. Sie unterscheidet Konfiguratoren, Eingabe-SDKs und Beleuchtungs-APIs. Die [Layout-Nachweise](docs/de-DE/research/keyboard-layout-evidence.md) erfasst modellabhängige Layout-, SKU- und Modulunterschiede. Diese Recherche ist keine Zusage bereits implementierter Geräteunterstützung.

## Daten

Konfiguration wird im lokalen App-Datenverzeichnis gespeichert. Import/Export verwenden JSON. Ein Import zeigt eine Vorschau, ersetzt nach Zustimmung die aktuelle Konfiguration und deaktiviert importierte Belegungen bis zur manuellen Aktivierung. Inhalte von Textaktionen werden nicht als Tastaturprotokoll erfasst.

## Prüfungen

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Native Eingaben können keine höheren Windows-Berechtigungen umgehen. In einer als Administrator laufenden Zielanwendung kann eine normal gestartete Controller-App deshalb keine Eingaben erzeugen. Belegte oder reservierte Hotkeys werden als Konflikt angezeigt.

Die Windows-Prüfungen können verbundene Hardware auslesen und unterstützte iCUE-G-Tasten vorübergehend registrieren und freigeben. Währenddessen sollte kein anderer Controller diese Belegungen verwenden. Die Audioprüfungen ändern weder Lautstärke noch Stummschaltung.

Das [Dokumentationsverzeichnis](docs/de-DE/README.md) enthält historische Entwürfe, Pläne, Recherche und datierte Testberichte. Historische Pläne beschreiben ihren damaligen Umfang und ersetzen keine aktuelle Übersicht der implementierten Unterstützung.

Die GitHub-Dokumentation ist auf US-Englisch (`en-US`), Deutsch (`de-DE`), Französisch (`fr-FR`) und Italienisch (`it-IT`) verfügbar. Die App akzeptiert auch diese vier regionalen Sprachcodes und verwendet die passenden Zahlenformate. Bestehende Sprachcodes bleiben kompatibel.



### Ablaufvorlagen und Warten auf Zielfenster

Der Katalog enthält sechs bearbeitbare Abläufe: Arbeitsplatz, Meeting, Streaming, Recherche, Projektwechsel und Textbaustein. Suche im Katalog oder in der Befehlspalette nach `workflow.`. Die Auswahl öffnet einen ungespeicherten Entwurf und führt nichts aus. Beim Einfügen in ein Makro oder Zuordnen zu einer Corsair-Taste bleiben alle Schritte erhalten. Beispiel-URLs, Programmpfade, Profile und OBS-Szenen vor dem Speichern anpassen.

**Auf Zielfenster warten** unterstützt Prozessnamen mit oder ohne `.exe`, einen Teil des Fenstertitels ohne Beachtung der Großschreibung und ein Zeitlimit von 1–600.000 ms. Mindestens ein Filter ist erforderlich; bei zwei Filtern müssen beide passen. Das Makro setzt erst fort, wenn ein passendes sichtbares Fenster reagiert und im Vordergrund ist. Bei mehreren Fenstern desselben Prozesses einen eindeutigen Titelfilter verwenden. Abbruch oder Zeitüberschreitung stoppen nachfolgende Schritte. Ein bereites Fenster garantiert weder ein fertig geladenes Dokument noch eine Verbindung zu externen Diensten wie OBS WebSocket.

Bildschirmhelligkeit: Im Aktionskatalog nach **Bildschirm heller**, **Bildschirm dunkler** oder **Bildschirmhelligkeit auf 50 Prozent** suchen. Relative Aktionen ändern die Helligkeit um 10 Prozentpunkte; feste Werte reichen von 0 bis 100. Unterstützte interne Displays verwenden WMI; kompatible externe Monitore benötigen aktiviertes DDC/CI im Monitormenü. Standardmäßig betrifft die Aktion alle unterstützten Displays; im Editor lässt sich ein bestimmter Monitor auswählen. Anschlüsse und Docks können die Unterstützung einschränken.

### Erweiterte Steuerung

Öffne die Befehlspalette mit **Ctrl+K, während das App-Fenster den Fokus hat**. Suche nach **Ablaufeditor**, **Makro aufnehmen**, **Profile synchronisieren** oder **Tastendiagnose**. Der Ablaufeditor bearbeitet den ausgewählten Kachelentwurf: Datei-/Prozess-/Fensterbedingungen, Vorwärtssprünge und Fehlerbehandlung mit Stoppen oder Fortsetzen. Die Aufnahme erfasst Tastenkombinationen und Wartezeiten, keinen normalen Text; Escape beendet sie. Prüfe das Ergebnis vor der Übernahme.

Wähle im Helligkeitsschritt alle unterstützten Displays oder einen bestimmten Monitor. Ist ein gewählter Monitor nicht verfügbar, erscheint ein Fehler, statt einen anderen Monitor zu verändern. Das Dashboard zeigt Live-Werte für Helligkeit, Audio und OBS, sofern die jeweilige Anbindung verfügbar ist. Fehlende Messwerte werden nicht als gemessene Werte ausgegeben.

Ordne im Kacheleditor eine unterstützte physische Sondertaste zu und wähle Sofortdruck, Kurz-, Halte- oder Doppeldruck. Gehaltene Ebenentasten aktivieren vorübergehend eine andere Belegung; Loslassen stellt die vorherige Ebene wieder her. Gesten und Ebenen gelten für Corsair-SDK-G-Tasten und gerätebezogene Standard-Sondertasten, nicht für gewöhnliche globale Hotkeys. Die Standardtastenerkennung unterstützt F13–F24 und Tastatur-Medientasten. Proprietäre Fn-, Profil- und Radtasten benötigen weiterhin einen passenden Herstelleradapter oder eine Herstellerzuordnung zu einer Standardtaste. Raw Input beobachtet die Taste und unterdrückt ihr normales Windows-Verhalten nicht. Die Tastendiagnose erklärt Profil-, Pausen-, Verbindungs- und Belegungsprobleme.

Die Profilsynchronisation tauscht Dateien über einen selbst gewählten Ordner aus, auch über einen bereits durch deinen Cloud-Client verwalteten Ordner. Prüfe die Vorschau vor dem Import; importierte Aktionen sind deaktiviert und Hardwarezuordnungen werden entfernt. Revisionen verhindern stilles Überschreiben; Aktionsinhalte werden nur ausdrücklich eingeschlossen. MAKRIVO stellt kein Cloud-Konto bereit und speichert keine Cloud-Zugangsdaten. Im Infobereich erscheint jetzt das eigene türkise M-Symbol, bei Pause mit bernsteinfarbenem Pausenzeichen.

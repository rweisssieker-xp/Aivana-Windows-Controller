<!-- generated-doc locale=de-DE source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../VERIFICATION.md)

# Prüfung – 8. Oktober 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Die Anwendung wurde unter Windows x64 mit dem stabilen .NET-SDK 10.0.202 gebaut und als eigenständige Windows-x64-Ausgabe veröffentlicht.

## Automatisiert geprüft

- Gesamtbuild: keine Warnungen und keine Fehler.
- 19 Core-Verhaltenstests: Standardkonfiguration, Validierung, Null-/Duplikatdaten, Profilpriorität, Import/Export und Sicherungswiederherstellung, Makroreihenfolge und Wiederholung, Abbruch, doppelte Auslösung, Serialisierung, unveränderlicher Ausführungssnapshot und Schrittfehler.
- Windows-Prüfung: Hotkey-Normalisierung und Aliasnamen, native Strukturgrößen, reale Tastatur-Geräteliste, sichere Registrierung und Wiederfreigabe, synthetisches WM_HOTKEY für Kachel und Abbruch, deaktivierte Kachelregistrierung während Eingabesimulation.
- Die eigenständige EXE wurde mit `--smoke-test` gestartet: alle fünf WPF-Ansichten rendern, Kacheln lassen sich anlegen/kopieren/löschen, Entwürfe bleiben beim Refresh erhalten, Konfiguration wird gespeichert und wieder geladen. Dieser Test verwendet ein isoliertes temporäres Konfigurationsverzeichnis und führt keine Text- oder Medienaktionen aus.
- Dashboard, Editor und kompakte Darstellung (900 × 720) wurden als echte WPF-Renderings visuell geprüft. Entwurfsspeicherung, Verwerfen, Kachel-/Makro-Reihenfolge, deutsche Hotkey-Aliase, einzelne Makroabbrüche und Schrittfortschritt sind geprüft.

## Hardwareabhängige Grenzen

Der letzte lokale Durchlauf erkannte vier Windows-Tastaturschnittstellen. Diese Zahl beschreibt HID-/Windows-Schnittstellen, nicht zwingend vier physische Tastaturen.

Das offizielle iCUE-SDK ist installiert und liegt neben der EXE. Der Live-Test verbindet sich mit iCUE und erkennt eine Corsair-Tastatur mit Modell- und LED-Informationen. Beleuchtungsänderungen wurden nicht automatisch ausgelöst; modellabhängige Sondertasten und andere Hersteller sind nicht hardwaregetestet. Generische Hotkeys gelten tastaturübergreifend.

Text-/Medieneingaben wurden nicht in andere geöffnete Benutzerprogramme injiziert. Die nativen Datenstrukturen und Fehler-/Freigabelogik wurden geprüft; das Verhalten in einem konkreten Zielprogramm hängt unter anderem von dessen Berechtigungsniveau ab.

## Wiederholen

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Der GUI-Test schreibt Ergebnis und Screenshot nach `smoke` neben der geprüften EXE. Er ist als Diagnosemodus gedacht und startet nach Abschluss keine dauerhafte Hintergrundinstanz.


## Erweiterte Steuerung

Zusätzlich geprüft: Profilbedingungen mit Prozess und Fenstertitel, Variablenersetzung ohne rekursive Expansion, Makrovorschau ohne Ausführung, neue Aktionsvalidierung und Abwärtskompatibilität alter JSON-Dateien. Native Prüfungen decken die Befehlspalette mit Registrierung, Konflikten, WM_HOTKEY-Dispatch und Freigabe sowie reine Monitoraufteilung und bedarfsgesteuerten Clipboard-Lesezugriff ab. Der WPF-Test öffnet und rendert die tatsächliche Palette, prüft Rückgängig/Wiederholen und Favoriten, prüft saubere Entwürfe nach dem Rendern und beendet den Prozess erfolgreich.

Windows-Sperre, Zwischenablageänderungen und Fensterbewegungen wurden während der Tests nicht auf Benutzerprogramme angewendet. Fensteraktionen posten Windows-Anfragen asynchron; das konkrete Zielprogramm kann eigene Mindestgrößen oder Berechtigungsgrenzen haben. Profilsperre und Ausführungsstatistik gelten für die laufende Sitzung.


Erweiterungsprüfung vom 12.10.2026: 30 Core-Prüfungen bestanden; Windows-Adapter einschließlich Loslassen während Eingabeinjektion, schreibgeschützter Monitorerkennung und OBS-Status-Mock bestanden. WPF prüft Ablaufübernahme/Abbrechen, Rekorder-Start/Stopp, Profilaustausch-Vorschau, Gesten und Symbolressourcen. Lokalisierung: 27 Kataloge mit je 967 Schlüsseln; Dokumentation: 84 Seiten in vier Sprachen. Proprietäre Tasten anderer Hersteller als Corsair wurden nicht an Hardware getestet; die generische Unterstützung umfasst gerätespezifische Standardereignisse F13–F24 und Medientasten. Cloud-Transport erfolgt über einen ausgewählten synchronisierten Ordner; Importe werden geprüft und bleiben deaktiviert.

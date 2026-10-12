<!-- generated-doc locale=de-DE source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plan zur Verbesserung der Benutzerfreundlichkeit und Funktionalität des Controllers

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Benutzerwunsch: Aussehen, Benutzerfreundlichkeit und Funktionalität verbessern; Vorherige Genehmigung fordert maximal nützliche Subagenten für notwendige kleinere Modelle und autonome Vervollständigung.

## Umfang
Behalten Sie vorhandene Profile und die funktionierende Corsair SDK-Integration bei. Verbessern Sie die vorhandene WPF-Anwendung, anstatt einen separaten Prototyp zu erstellen.

- [x] Shell-/Dashboard-Worker: reaktionsfähiges Layout, aktive Navigation, lesbare Steuerelemente, kompakte Aktivität/Status, Neuordnung der Kacheln per Drag-and-Drop, hilfreiche leere Status und Vorlagen. Eigene MainWindow.xaml, MainWindow.xaml.cs und MainWindow.Dashboard.cs.
- [x] Editor-Worker: MainWindow.Editor.cs und optionale Nur-Editor-Teildateien. Explizite gestaffelte Bearbeitungen, Speichern/Verwerfen, Inline-Validierung, dedizierte Makroliste mit Drag-and-Drop, duplizierte Schritte, zuverlässige Hotkey-Erfassung und Datei-/URL-Felder.
- [x] Windows-Worker: native Validierung, robuste Gerätenamen und SDK-Funktionen, begrenzte Modifikatorwartezeiten und sichere Injektion; eigene Windows- und Windows-Tests.
- [x] Übergeordnet: Kernausführungsfortschritt, Controller-Schnittstellen, Konfigurationserhaltung, Integrationstests, tatsächlich gerenderte Screenshots, Veröffentlichung/Paket und Ersetzung der laufenden Anwendung.

## Schnittstellenvertrag
Der Controller stellt vorhandene APIs sowie die Zeichenfolge LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), void OpenIcue() bereit. Editor macht bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); Vorhandene Feld-/Methodennamen bleiben über Teilklassen hinweg verfügbar.

## Akzeptanz
Kein Zurücksetzen der tatsächlichen Benutzerkonfiguration; Gültige bereitgestellte Änderungen werden einmal gespeichert, ungültige Entwürfe bleiben bearbeitbar, Verwerfen stellt gespeicherte Daten wieder her. Native Hotkey-Aliase werden validiert, ohne dass Testeingaben in nicht verwandte Fenster eingefügt werden. Kachel- und Makro-Neuordnung getestet; alle Ansichten werden in Desktop-Größen gerendert; Status läuft/fehlgeschlagen/abgeschlossen sichtbar; tatsächliches SDK-Gerätemodell angezeigt. Erstellen/Testen und veröffentlichter Rauchpass, und aktualisierte Anwendung wird gestartet.

# Plan zur Verbesserung der Bedienbarkeit und Funktionen des Controllers

[English (US)](../../../en-US/superpowers/plans/2026-10-08-controller-polish.md) | [Deutsch](../../../de-DE/superpowers/plans/2026-10-08-controller-polish.md) | [Français](../../../fr-FR/superpowers/plans/2026-10-08-controller-polish.md) | [Italiano](../../../it-IT/superpowers/plans/2026-10-08-controller-polish.md)

Anfrage des Nutzers: Erscheinungsbild, Bedienbarkeit und Funktionen verbessern; die vorherige Freigabe verlangt die größtmögliche sinnvolle Zahl von Subagents mit den erforderlichen kleineren Modellen sowie die autonome Fertigstellung.

## Umfang
Bestehende Profile und die funktionierende Corsair-SDK-Integration erhalten. Die bestehende WPF-Anwendung verbessern, statt einen separaten Prototyp zu erstellen.

- [x] Shell-/Dashboard-Worker: responsives Layout, aktive Navigation, gut lesbare Steuerelemente, kompakte Aktivitäts-/Statusanzeige, Kachel-Neuanordnung per Drag-and-drop, hilfreiche leere Zustände und Vorlagen. Zuständig für MainWindow.xaml, MainWindow.xaml.cs und MainWindow.Dashboard.cs.
- [x] Editor-Worker: MainWindow.Editor.cs und optionale Editor-Partial-Dateien. Explizite Entwurfsänderungen, Speichern/Verwerfen, Validierung direkt im Editor, eigene Makroliste mit Drag-and-drop, Schritte duplizieren, zuverlässige Hotkey-Aufnahme sowie Datei-/URL-Felder.
- [x] Windows-Worker: native Validierung, robuste Gerätenamen und SDK-Funktionen, begrenztes Warten auf Modifikatoren und sichere Eingabesimulation; zuständig für Windows und Windows-Tests.
- [x] Parent: Fortschritt der Kern-Ausführung, Controller-Schnittstellen, Erhalt der Konfiguration, Integrationstests, tatsächlich gerenderte Screenshots, Veröffentlichung/Paketierung und Austausch der laufenden Anwendung.

## Schnittstellenvertrag
Der Controller stellt die vorhandenen APIs sowie string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string) und void OpenIcue() bereit. Der Editor stellt bool HasUnsavedChanges, void DiscardDraft() und bool CommitDraft() bereit; vorhandene Feld- und Methodennamen bleiben über Partial-Klassen hinweg verfügbar.

## Abnahmekriterien
Die reale Benutzerkonfiguration wird nicht zurückgesetzt; gültige Entwurfsänderungen werden einmal gespeichert, ungültige Entwürfe bleiben bearbeitbar und Verwerfen stellt die gespeicherten Daten wieder her. Native Hotkey-Aliase werden validiert, ohne Testeingaben in fremde Fenster zu senden. Kachel- und Makro-Neuanordnung wird geprüft; alle Ansichten werden in Desktopgrößen gerendert; Status für laufende, fehlgeschlagene und abgeschlossene Aktionen ist sichtbar; das tatsächliche SDK-Gerätemodell wird angezeigt. Build-/Tests und veröffentlichter Smoke-Test bestehen, und die aktualisierte Anwendung wird gestartet.

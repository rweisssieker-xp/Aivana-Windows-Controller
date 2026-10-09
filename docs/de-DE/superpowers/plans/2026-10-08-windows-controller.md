# Implementierungsplan für den Windows Controller

[English (US)](../../../en-US/superpowers/plans/2026-10-08-windows-controller.md) | [Deutsch](../../../de-DE/superpowers/plans/2026-10-08-windows-controller.md) | [Français](../../../fr-FR/superpowers/plans/2026-10-08-windows-controller.md) | [Italiano](../../../it-IT/superpowers/plans/2026-10-08-windows-controller.md)

> Für agentische Worker: getrennte Zuständigkeiten parallel bearbeiten; der Nutzer hat ausdrücklich die vollständige Implementierung mit der größtmöglichen sinnvollen Zahl von Subagents angefordert.

**Ziel:** Das native Windows-Hotkey-Panel gemäß freigegebenem Entwurf bereitstellen.
**Architektur:** Kernkonfiguration und abbrechbare Ausführung; Windows-Adapter; WPF-Desktopanwendung.
**Technologie-Stack:** .NET 10, WPF, Win32, JSON. Keine kostenpflichtigen Abhängigkeiten.
**Spezifikation:** ../specs/2026-10-08-windows-controller-design.md

## Globale Einschränkungen
Deutsche Benutzeroberfläche; lokale Speicherung; Windows x64; wahrheitsgemäße Anzeige der Hardwarefunktionen; keine Tastaturprotokollierung; Autostart standardmäßig aus.

## Prüfschwerpunkte
Fehlerhafte Importe dürfen gespeicherte Einstellungen nicht ersetzen; gehaltene Modifikatoren dürfen die Ausgabe nicht verunreinigen; abgesteckte Hardware darf keinen Absturz verursachen; beim Schließen muss der Tray verfügbar bleiben; abgebrochene Makros müssen Ressourcen freigeben.

## Aufgaben und Zuständigkeiten
- [x] Core-Agent: src/Aivana.Core und tests/Aivana.Tests. Modelle, JSON-Persistenz, Validierung, Profilauswahl und MacroRunner. Zuerst fehlschlagende Verhaltenstests ausführen, dann implementieren und alle Core-Tests ausführen.
- [x] Windows-Agent: src/Aivana.Windows. RegisterHotKey, Geräteinventar, SendInput, Vordergrundprozess und optionaler Corsair-Beleuchtungsadapter. Native Strukturgrößen und Freigabe der Registrierung mit sicheren Smoke-Tests prüfen.
- [x] GUI-Agent: ausschließlich src/Aivana.Desktop/MainWindow.xaml und .cs sowie UI-Hilfen. Dunkles Dashboard, vollständiger Editor, Profile, Geräte, Einstellungen, Tastaturübersicht und Makroeditor. Erstellen, sobald die Verträge vorhanden sind.
- [x] Parent: gemeinsame Verträge, Projektdateien, DesktopController-Komposition, Anwendungs-/Tray-Lebenszyklus, Import-/Exportdialoge, Dokumentation, Integrationstests, Veröffentlichung und Smoke-Test der laufenden Anwendung.

## Gemeinsame Schnittstellen
Die Verträge stehen in Core/Models.cs. Die Controller-Implementierung steht in Desktop/DesktopController.cs. Die UI ruft Controller-Methoden über diese konkrete Klasse auf. Agents erhalten in ihren Aufgaben die exakten Signaturen. Jeder ist für separate Dateien zuständig. Der Parent behebt projektübergreifende Fehler, nachdem alle Agents fertig sind.

## Prüfung und Auslieferung
- [x] dotnet build Aivana.slnx -c Release: keine Fehler.
- [x] dotnet run --project tests/Aivana.Tests -c Release: Verhaltenstests bestanden.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Veröffentlichte EXE starten, Windows-Oberfläche und Prozessstabilität prüfen sowie sichere Abläufe für Profile, Bearbeitung und Speicherung durchführen.
- [x] README mit Build-/Startanleitung und Hardwareeinschränkungen schreiben; Prüfungen und verbleibende hardwareabhängige Tests dokumentieren.

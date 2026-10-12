<!-- generated-doc locale=de-DE source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Controller-Implementierungsplan

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Für Agenten: Verwenden Sie paralleles isoliertes Eigentum; Der Benutzer hat ausdrücklich eine vollständige Implementierung mit maximal nützlichen Subagenten angefordert.

**Goal:** Stellen Sie das native Windows-Hotkey-Panel im genehmigten Design bereit.
**Architecture:** Kernkonfiguration und stornierbare Ausführung; Windows-Adapter; WPF Desktop-Komposition.
**Tech Stack: ** .NET 10, WPF, Win32, JSON. Keine bezahlten Abhängigkeiten.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Globale Einschränkungen
Deutsche Benutzeroberfläche; lokaler Speicher; Windows x64; wahrheitsgemäßer Hardware-Fähigkeitsstatus; keine Tastaturprotokollierung; kein Standard-Autostart.

## Rezensionsfokus
Fehlerhafte Importe dürfen gespeicherte Einstellungen nicht ersetzen; gehaltene Modifikatoren dürfen die Ausgabe nicht verunreinigen; Nicht angeschlossene Hardware darf nicht abstürzen; Beim Schließen muss die Verfügbarkeit des Fachs gewährleistet bleiben. Abgebrochene Makros müssen Ressourcen freigeben.

## Aufgaben und Eigentum
- [x] Kernagent: src/Aivana.Core und Tests/Aivana.Tests. Modelle, JSON-Persistenz, Validierung, Profilauswahl und MacroRunner. Führen Sie fehlgeschlagene Verhaltenstests durch, implementieren Sie dann alle Kerntests und führen Sie sie aus.
- [x] Windows-Agent: src/Aivana.Windows. RegisterHotKey, Geräteinventur, SendInput, Vordergrundprozess und optionaler Beleuchtungsadapter Corsair. Überprüfen Sie die nativen Strukturgrößen und die Registrierungsbereinigung mit sicheren Rauchtests.
- [x] GUI-Agent: src/Aivana.Desktop/MainWindow.xaml und .cs plus nur UI-Helfer. Dunkles Dashboard, vollständiger Editor, Profile, Geräte, Einstellungen, Tastaturübersicht und Makroeditor. Erstellen Sie, sobald Verträge vorliegen.
- [x] Übergeordnet: Gemeinsame Verträge, Projektdateien, DesktopController-Zusammensetzung, Anwendungs-/Tray-Lebenszyklus, Import-/Exportdialoge, Dokumentation, Integrationsprüfungen, Smoke-Test zum Veröffentlichen und Ausführen von Apps.

## Gemeinsame Schnittstellen
Verträge leben in Core/Models.cs. Die Controller-Implementierung befindet sich in Desktop/DesktopController.cs. Die Benutzeroberfläche ruft Controller-Methoden über diese konkrete Klasse auf. Agenten erhalten in ihren Aufgaben exakte Unterschriften. Jeder besitzt separate Dateien. Das übergeordnete Element behebt projektübergreifende Fehler, nachdem alle Agenten fertig sind.

## Überprüfung und Lieferung
- [x] dotnet build Aivana.slnx -c Release: null Fehler.
- [x] dotnet run --project tests/Aivana.Tests -c Release: Verhaltensprüfungen bestanden.
- [x] dotnetPublish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artefakte/win-x64.
- [x] Veröffentlichte ausführbare Datei starten, Windows-Benutzeroberfläche und Prozessstabilität überprüfen, sicheren Profil-/Bearbeitungs-/Persistenzfluss ausüben.
- [x] Schreiben Sie eine README-Datei mit Build-/Startanweisungen und Hardwareeinschränkungen. Aufzeichnungsprüfungen und verbleibende reine Hardwaretests.

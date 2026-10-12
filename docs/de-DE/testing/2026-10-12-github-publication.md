<!-- generated-doc locale=de-DE source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../testing/2026-10-12-github-publication.md)

# GitHub-Veröffentlichung des MAKRIVO-Entwicklungsstands

Stand: 12. Oktober 2026. Quellcode-Veröffentlichung, kein signierter Verkaufsrelease.

- Release-Build der Lösung: erfolgreich, null Warnungen und Fehler.
- Core-Prüfungen: erfolgreich.
- WPF-Smoke-Test: erfolgreich; frischer Ergebnisbericht im lokalen Buildordner.
- Erste vollständige Windows-Suite: Vordergrundfenster-, Audio-, Bildschirm- und iCUE-Prüfungen erfolgreich; anschließend Timeout bei der UI-Automation-Workerantwort.
- Gezielter Wiederholungslauf `--extensions-only`: erfolgreich einschließlich UI-Automation, Mehrdeutigkeit, deaktivierten Zielen, Passwortausschluss, Pluginprozess und echtem HTTPS-Mobilhost. Der ursprüngliche Timeout wurde nicht durch eine Änderung des Zeitlimits verdeckt; die Ursache ist noch nicht abschließend belegt.
- Anschließender vollständiger Windows-Wiederholungslauf: erfolgreich einschließlich Vordergrundfenster und aller Erweiterungsprüfungen. Ein erfolgreicher Wiederholungslauf schließt sporadische Timeouts nicht aus.
- Vor Veröffentlichung wurden keine typischen privaten Schlüssel oder produktiven Stripe-/GitHub-/AWS-Schlüssel im geprüften Quellbestand gefunden. Dieser begrenzte Mustercheck ist keine vollständige Geheimnisprüfung.
- Buildartefakte und lokale Orchestrierungsdaten werden nicht mit veröffentlicht.

Funktionsumfang und verbleibende Abnahmen: [Featureliste](../FEATURES.md). Vorherige Prüfberichte behalten ihre zeitliche Gültigkeit; sie sind keine Garantie für jede Hardware oder jeden Desktopkontext.

# Aivana Windows Controller — Dokumentation

[English (US)](../en-US/README.md) | [Deutsch](../de-DE/README.md) | [Français](../fr-FR/README.md) | [Italiano](../it-IT/README.md)

[Benutzerhandbuch](../../README.de-DE.md)

Die Recherche beschreibt dokumentierte Herstellerfunktionen und garantiert keine implementierte Unterstützung. Datierte Pläne und Testberichte behalten ihren ursprünglichen Umfang und ihr Datum.

## Prüfung und Tests

- [Prüfung – 8. Oktober 2026](VERIFICATION.md)

- [Regression-Prüfung der App — 2026-10-09](testing/2026-10-09-app-audit.md)

- [Tastaturassistenz, Overlay und Ausführungsrückmeldung](testing/2026-10-09-usp-release.md)

## Recherche

- [Tastaturmarken mit Sonderfunktionen – Recherche für Windows-Controller](research/keyboard-custom.md)

- [Gaming-Tastaturen mit Sonderfunktionen und Integrationsschnittstellen](research/keyboard-gaming.md)

- [Physische Layouts und Varianten: Stream-Deck-artige Tastaturkontrollen](research/keyboard-layout-evidence.md)

- [Sonderfunktionen von Tastaturen und Zusatz-Keypads](research/keyboard-productivity.md)

- [OBS-Studio-Integration](research/obs-integration.md)

- [Tastaturhersteller und Sonderfunktionen – Inventar für einen Windows-Controller](research/tastaturhersteller-sonderfunktionen.md)

- [CSV](research/tastaturhersteller-sonderfunktionen.csv)

## Historische Entwürfe und Pläne

- [Plan zur Verbesserung der Bedienbarkeit und Funktionen des Controllers](superpowers/plans/2026-10-08-controller-polish.md)

- [Implementierungsplan für den Windows Controller](superpowers/plans/2026-10-08-windows-controller.md)

- [Physische Tastaturansicht für Mapping](superpowers/specs/2026-10-08-keyboard-mapping.md)

- [Aivana Windows Controller – Entwurf](superpowers/specs/2026-10-08-windows-controller-design.md)

Maschinenlesbare Übersetzungskataloge verbleiben unter docs/localization; ihre Schlüssel sind Implementierungsdaten und werden nicht als Dokumentation übersetzt.

## Dokumentationsprüfung

```powershell
python tools/check_documentation.py
```

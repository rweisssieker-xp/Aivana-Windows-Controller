<!-- generated-doc locale=de-DE source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Implementierungsplan für die Controller-Erweiterung

**Goal:** Implementieren Sie die angeforderten Verbesserungen bei Monitor, Geste, Arbeitsablauf, Aufzeichnung, Gerätediagnose, tragbarem Profilaustausch und Fach.

**Architecture:** Erweitern Sie die vorhandenen Core/Windows/Desktop-Ebenen. Reine Zustandsmaschinen und portable Konfigurationslogik leben in Core, native Integrationen in Windows und modale Entwurfseditoren in Desktop. Behalten Sie vorhandene Dirty-Workspace-Änderungen und bereitgestellte Benutzerkonfigurationen bei.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Überwachen Sie den Dienst, die Zielpersistenz und den asynchronen Live-Status. Testen Sie nicht verfügbare IDs und Stornierungen.
- [x] Gestenzustandsmaschine und gehaltene Ebenen; Testkanten, Wiederholen, Zurücksetzen und vorhandene Sofortbindungen.
- [x] Workflow-Bedingungen, Weiterleitungszweige und Fehlerrichtlinie; Testschnappschüsse und Abbruch.
- [x] Explizite Verknüpfungsaufzeichnung mit Entwurfsüberprüfung; Testfilterung und Hakenentsorgung.
- [x] Austausch von Profilen für freigegebene Ordner mit Revisionsprüfungen und Vorschauen; Testkonflikte und portable Mappings.
- [x] Generische Standard-Sondertasten und -Diagnose pro Gerät; Testparsing, doppelte Zuordnungen und Pausenverhalten.
- [x] Ressourcen für Taskleisten-/Fenster-/ausführbare Symbolsymbole und pausierte Darstellung; Überprüfen Sie die Ressourcen in der veröffentlichten ausführbaren Datei.
- [x] Alle Paletten-/Editor-Einstiegspunkte und lokalisierten Text integrieren; Führen Sie die Core-, Windows- und WPF-Prüfungen aus.
- [x] In einem separaten Staging-Verzeichnis veröffentlichen, Benutzereinstellungen beibehalten und die aktualisierte App neu starten.

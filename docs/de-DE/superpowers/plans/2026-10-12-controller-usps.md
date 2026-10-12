# Implementierungsplan der Steuerungserweiterungen

Die angeforderten Monitor-, Gesten-, Ablauf-, Aufnahme-, Diagnose-, Profilaustausch- und Symbolfunktionen umsetzen.

Die bestehenden Core-/Windows-/Desktop-Schichten erweitern. Reine Zustandsautomaten und portable Konfiguration gehören in Core, native Anbindungen in Windows und modale Entwurfseditoren in Desktop. Vorhandene lokale Änderungen und die bereitgestellte Nutzerkonfiguration erhalten.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitordienst, Zielpersistenz und asynchroner Live-Zustand; fehlende IDs und Abbruch prüfen.
- [x] Gestenautomat und gehaltene Ebenen; Flanken, Wiederholung, Rücksetzen und Sofortbelegung prüfen.
- [x] Ablaufbedingungen, Vorwärtsverzweigungen und Fehlerregeln; Momentaufnahmen und Abbruch prüfen.
- [x] Explizite Aufnahme von Tastenkombinationen mit Entwurfsprüfung; Filter und Hook-Freigabe prüfen.
- [x] Profilaustausch über gemeinsame Ordner mit Revisionen und Vorschauen; Konflikte und portable Zuordnungen prüfen.
- [x] Gerätebezogene Standard-Sondertasten und Diagnose; Verarbeitung, doppelte Belegung und Pause prüfen.
- [x] Ressourcen für Infobereich-, Fenster- und Programmsymbol sowie Pausendarstellung; veröffentlichtes Programm prüfen.
- [x] Alle Palette-/Editor-Einstiege und Übersetzungen integrieren; Core-, Windows- und WPF-Prüfungen ausführen.
- [x] In getrennten Staging-Ordner veröffentlichen, Nutzereinstellungen erhalten und aktualisierte App starten.

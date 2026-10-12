# GUI-Überarbeitung vom 12.10.2026

- Direkter Einstieg „Werkzeuge“ in der Navigation; drei Gruppen mit erklärten Aktionen für Einrichtung, Erstellung und Prüfung.
- Vierzehn vorhandene Dialogbereiche verwenden eine gemeinsame WPF-Dialogklasse mit den Ressourcen des Hauptfensters, einheitlicher Schrift und Positionierung beim Besitzer.
- Dialoggröße wird auf den Windows-Arbeitsbereich begrenzt; lange Inhalte erhalten Scrollleisten. Einstellungen und Profilseiten bleiben auch bei geringer Höhe scrollbar.
- Aktionsfilter erscheint nur auf dem Dashboard. Profilwahl und Suche haben explizite Namen für assistive Technologien. Feldbeschriftungen sind kontrastreicher.
- Dialog-Smoke-Test umfasst Werkzeugübersicht und Erweiterungsdialoge in 560 × 440 sowie normaler Größe; bestehende Recorder- und Sprachprüfungen wurden an die Scrollstruktur angepasst.

Grenzen: Kompakte Fenster sind kein Ersatz für echte Windows-DPI-Tests mit 150/200 Prozent. Die neuen deutschen Texte sind noch nicht vollständig für alle 27 Sprachen übersetzt. Keine neue Hardwarevalidierung und keine Aussage über gemessene Bedienbarkeit.

Prüfstand: Release-Build ohne Warnungen/Fehler; vollständiger WPF-Smoke-Test bestanden, einschließlich kompakter Werkzeug- und Erweiterungsdialoge sowie Sprachwechselprüfungen. git diff --check bestanden.

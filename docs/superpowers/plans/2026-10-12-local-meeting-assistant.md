# Lokaler Besprechungsassistent – 2026-10-12

Autorisiert durch „ok los“ und bestehende Vorgabe ohne Rückfragen. Bestehenden Branch/Änderungen erhalten. Kurzer Prototyp, keine automatische Durchführung.

1. Core: ausgewählte Ressourcen mit IDs; Modellplan nur Ressourcen-IDs, Mikrofon stummschalten, offene Punkte. Strenge Größen-/Typ-/ID-Grenzen; Compiler erzeugt nur Launch/OpenPath/Url/Volume. Tests zuerst: unbekannte IDs, ausführbare Dokumente, nicht-HTTP-URLs, Duplikate, leere Pläne, Feature aus.
2. Windows: Ollama loopback-only HTTP ohne Proxy/Redirect, JSON-Schema, Token-/Antwortgrenzen, Abbruch und 90s-Zeitlimit; kein automatisches Installieren/Herunterladen, keine Cloud-/API-Schlüssel. Fake-Transporttests prüfen Anfrage und Fehler; echtes Modell nur wenn lokal vorhanden.
3. WPF: Befehlspalette, standardmäßig deaktivierter Schalter, Modellauswahl vom lokalen Dienst, Ziel und explizite Ressourcenliste, Vorschau/offene Punkte, Abbruch, Übergabe in vorhandenen Entwurf ohne Ausführung. Änderungen nach Vorschau invalidieren sie; Profilrevision prüfen.
4. Experiment: manuelle Stoppuhr und lokale Messereignisse (ohne Ziel/Pfade), Entwurfsannahme/Korrektur/Verwerfen sowie tatsächliche Makro-Endzustände. Kein Erfolgsnachweis allein aus Completed; bestehende Ergebnisprüfung separat.
5. Tests: Core, Transport, WPF-Smoke; keine Live-Nutzeraktionen. Bericht kennzeichnet fehlenden Live-Modelltest und weiterhin offene alte Release-Gates. Bestehendes sechsteiliges Release nicht als KI-fähig umdeklarieren.

Ollama-Protokollquellen: https://docs.ollama.com/api/chat und https://docs.ollama.com/capabilities/structured-outputs (12.10.2026).

## Ausführungsstand

- Core Compiler/Feature/Datensparsamkeit grün nach initialem Compile-RED.
- Lokaler Modellclient grün nach initialem Compile-RED; Remote-Alias, Cloudname, Schema, fehlender Pfad im Prompt, kaputte/zu große Ausgabe und Abbruch mit Fake-Handler geprüft.
- WPF-Handoff: ungespeichert, kein Ausführen; Review/Speichern/Korrektur und Verwerfen im Smoke grün.
- Fresh-context reviewer /root/meeting_review (read-only): zwei P2-Funde, verworfene Vorschauen und lange Links. Beide mit Regressionen RED→GREEN korrigiert. Keine weiteren wichtigen Befunde berichtet.
- Release-Build null Warnungen/Fehler, alle Core-Gruppen und vollständige Windows-Suite ohne Skip grün. Native Fokusprüfung jetzt ebenfalls grün; vorhandene iCUE-Tastatur verbunden. Keine echten Nutzerprogramme durch Modell gestartet.
- Kein Live-Ollama verfügbar; kein Modell/Dienst installiert und keine Cloud/Schlüssel verwendet. Qualität/Zeitersparnis bleiben experimentell.
- Selfcontained-Paket mit Begleitprogrammen und neuen Bedien-/Prüfhinweisen unter artifacts/release-20261012-183635. Bestehende Artefakte und uncommittete Änderungen erhalten; kein Merge/Push.

<!-- generated-doc locale=de-DE source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokale KI-Laufzeiten und verbleibende Prüfungen

Ziel: weitere lokale KI-Laufzeiten tatsächlich anbinden, Entwurfsprüfung erhalten und offene Freigaben sichtbar machen. Benutzer autorisiert Umsetzung ohne Rückfragen. Bestehende Änderungen bleiben erhalten.

1. Tests für Loopback-Transport und ONNX-Paketprüfung vor Implementierung; Netzwerkadressen, Ausgabegrenzen, Abbruch und unbekannte Ressourcen prüfen.
2. Gemeinsame Planner-Nachrichten; Ollama erhalten, LM Studio (127.0.0.1:1234) und llama.cpp (127.0.0.1:8080) ergänzen. Keine Cloud-URLs oder automatischen Anbieterwechsel. Lokale Server können selbst weiterleiten; deren Betrieb bleibt sichtbar Verantwortung des Betreibers.
3. ONNX Runtime GenAI CPU in eigenem Prozess mit Zeitlimit, begrenztem JSON-Protokoll und Prozessabbruch. Modellordner explizit auswählen; kein Modell wird automatisch heruntergeladen. Nur GenAI-kompatible Textmodelle, keine beliebigen .onnx-Netze.
4. WPF-Auswahl für Laufzeit, Modell, ONNX-Ordner; alle liefern denselben streng geprüften Entwurf.
5. Prüfstand mit klaren Grenzen für Hardware, DPI, Mobilpanel und exklusives Makropad. Physische Prüfungen und ein signierter/lizenzierter Treiber lassen sich hier nicht durch Software ersetzen.
6. Release-Build, Core/Windows-Tests, WPF-Smoke, Worker-/Paketprüfung und unabhängiger Review. Neues vollständiges Paket erstellen; Modellqualität nur bei tatsächlich vorhandenem Modell als getestet melden.

Ergebnis: vier Laufzeiten implementiert, ONNX nativ separat getestet, Anbieterwahl und Prüfstand im gepackten WPF-Smoke bestanden. Core/Windows-Suiten grün. Reviewbefund Unicodegrenze mit gemeinsamer Konstante und Regression behoben. Echte Hardware-/Mobile-/DPI-Wechselabnahmen, Treiberfreigabe, vollständige Fachübersetzungen und Live-Instruct-Qualität bleiben explizite offene Arbeit; nicht als komplett ausgegeben.

Fortsetzung: separate Jinja- und benannte/default-Vorlagen implementiert; Typ-/Grenz-/Fallback-Regressionsprüfungen bestanden. Werkzeug-Navigation in 27 Katalogen ergänzt. Echtes Instruct-Modell separat heruntergeladen und SHA256 geprüft: native CPU-Inferenz funktioniert, aber 0/4 Planungsfälle erfüllen den JSON-Vertrag; keine Aktionen ausgeführt. Unabhängiger Review: null-Fallback korrigiert und getestet, danach keine konkreten weiteren Befunde. Core und Windows-KI-Tests bestanden, Release-Build 0 Warnungen/Fehler.

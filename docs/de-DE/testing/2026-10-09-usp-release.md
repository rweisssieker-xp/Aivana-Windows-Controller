<!-- generated-doc locale=de-DE source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../testing/2026-10-09-usp-release.md)

# Tastaturunterstützung, Overlay und Ausführungsfeedback

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Die drei priorisierten Verbesserungen wurden in der bestehenden Windows-App implementiert:

- Spezialschlüssel-Assistent: tatsächliche iCUE-unterstützte Geräte-/Schlüsselinventarisierung, temporäre G-Key-Erfassung, Überprüfung des Konfigurationserfolgs, Konfliktvalidierung und ein nicht gespeicherter Aktionsentwurf. Capture-Rollback, Invalidierung von Rückrufen in der Warteschlange und Wiederherstellung normaler Bindungen decken Fehler und Dialogschließungen ab.
- Tastatur-Overlay: physische Referenzgeometrie, aktive Profilbindungen, Live-Profil-/Bearbeitungsaktualisierung und normale Windows-Tastenhervorhebung. Halten Sie Strg+Alt+F1 gedrückt oder blättern Sie durch die Paletten-/Tastaturseite. Nicht aktivierend, durchklickbar und nicht in der Taskleiste vorhanden.
- Ausführungsfeedback: eingegebene Phasen „Warten/Läuft/Schritt/Abgeschlossen/Abgebrochen/Fehlgeschlagen/Abgelehnt“, Schritt-/Wiederholungsfortschritt und ein nicht aktivierender Toast. Das Feedback schließt Aktionsinhalte und Ausnahmemeldungen aus. Bei der Stornierung im letzten Schritt kann der Abschluss nicht gemeldet werden. Der Abschluss beschreibt die Ausführung und keine unabhängige OBS-Statusabfrage.

Überprüfung:

- 24 Kernprüfungen bestanden, einschließlich Phasenreihenfolge, Fehler-/Abbruchergebnisse, Abbruch im letzten Schritt, Isolierung von Listener-Ausnahmen und Ausschluss privater Aktionswerte.
- Der Build der Release-Lösung wurde ohne Warnungen/Fehler abgeschlossen.
- Windows-Schecks bestanden; Der verbundene K100 meldete G1–G6 und die native Konfiguration/Freigabe war erfolgreich. Audioendpunkte wurden überprüft, ohne ihren Status zu ändern.
- WPF Smoke wurde mit allen Referenz-Overlay-Layouts, Assistenten-/Katalog-/Konflikt-/Entwurfspersistenz, nativer Live-Erfassungskonfiguration, Rückrufweiterleitung, Ablehnung veralteter Rückrufe und Wiederherstellung der angehaltenen Bindung bestanden. Native Callback-Eingabe wurde simuliert; Ein physischer menschlicher G-Tastendruck bleibt eine manuelle Überprüfung.
- Für den Ausführungs-Toast wurde die tatsächliche Vordergrundfenster-Invarianz überprüft. Eigenschaften der Overlay-Interaktion und Ein-/Ausblenden wurden überprüft; Screenshots wurden für den Assistenten, das Overlay und den Toast gerendert.
- 27-Sprachen enthalten alle 819 kanonischen Zeichenfolgen; Offline-Platzhalter-/Schlüsselvalidierung bestanden.

Snapshots und das vollständige GUI-Ergebnis befinden sich in artefakten/usp-win-x64/smoke. Tests verwenden temporäre Profile; Produktionseinstellungen werden während der Bereitstellung gesichert und einem Hash-Check unterzogen.

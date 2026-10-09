# Tastaturassistenz, Overlay und Ausführungsrückmeldung

[English (US)](../../en-US/testing/2026-10-09-usp-release.md) | [Deutsch](../../de-DE/testing/2026-10-09-usp-release.md) | [Français](../../fr-FR/testing/2026-10-09-usp-release.md) | [Italiano](../../it-IT/testing/2026-10-09-usp-release.md)

Die drei priorisierten Verbesserungen wurden in der bestehenden Windows-App umgesetzt:

- Assistent für Sondertasten: tatsächliches iCUE-unterstütztes Geräte-/Tasteninventar, temporäre G-Tasten-Aufnahme, Prüfung des Konfigurationserfolgs, Konfliktvalidierung und ein ungespeicherter Aktionsentwurf. Rollback der Aufnahme, Ungültigmachen wartender Callbacks und Wiederherstellung der normalen Belegung decken Fehler und das Schließen des Dialogs ab.
- Tastatur-Overlay: physische Referenzgeometrie, Belegungen des aktiven Profils, Live-Aktualisierung bei Profil-/Editoränderungen und Hervorhebung gewöhnlicher Windows-Tasten. Mit Ctrl+Alt+F1 halten oder über die Befehlspalette oder Tastaturansicht umschalten. Übernimmt nicht den Fokus, lässt Klicks durch und erscheint nicht in der Taskleiste.
- Ausführungsrückmeldung: typisierte Phasen Waiting/Running/Step/Completed/Cancelled/Failed/Rejected, Fortschritt für Schritte und Wiederholungen sowie ein nicht aktivierender Hinweis. Die Rückmeldung enthält weder Aktionsinhalte noch Ausnahmemeldungen. Ein Abbruch im letzten Schritt kann nicht als Abschluss gemeldet werden. „Abgeschlossen“ beschreibt die Ausführung, keine unabhängige Abfrage des OBS-Zustands.

Prüfung:

- 24 Core-Prüfungen bestanden, einschließlich Phasenreihenfolge, Fehler-/Abbruchergebnissen, Abbruch im letzten Schritt, Isolation von Listener-Ausnahmen und Ausschluss privater Aktionswerte.
- Release-Solution-Build ohne Warnungen oder Fehler bestanden.
- Windows-Prüfungen bestanden; die verbundene K100 meldete G1–G6 und die native Konfiguration/Freigabe war erfolgreich. Audioendpunkte wurden geprüft, ohne ihren Zustand zu ändern.
- WPF-Smoke-Test mit allen Referenzlayouts des Overlays, Assistent/Katalog/Konflikten/Entwurfspersistenz, Live-Konfiguration der nativen Aufnahme, Callback-Weiterleitung, Zurückweisung veralteter Callbacks und Wiederherstellung pausierter Belegungen bestanden. Native Callback-Eingabe wurde simuliert; ein physischer Tastendruck auf eine G-Taste bleibt manuell zu prüfen.
- Für den Ausführungshinweis wurde geprüft, dass das tatsächliche Vordergrundfenster unverändert bleibt. Overlay-Interaktion und Ein-/Ausblenden wurden geprüft; Screenshots für Assistent, Overlay und Hinweis wurden gerendert.
- 27 Sprachen enthalten alle 819 kanonischen Zeichenfolgen; Offline-Prüfung von Platzhaltern und Schlüsseln bestanden.

Snapshots und das vollständige GUI-Ergebnis liegen in artifacts/usp-win-x64/smoke. Tests verwenden temporäre Profile; Produktionseinstellungen werden während der Bereitstellung gesichert und per Hash geprüft.

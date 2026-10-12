<!-- generated-doc locale=de-DE source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokaler KI-Besprechungsassistent – Prototyp

Stand: 12. Oktober 2026. ForgeMind-Hypothese „Outcome Agent“, begrenzter Besprechungsablauf. Kein nachgewiesener Wettbewerbsvorteil und keine gemessene Zeitersparnis ohne echte Vergleichsversuche.

## Nutzung

1. Befehlspalette → **KI-Besprechungsassistent**. Experiment ist standardmäßig aus und wird bei Konfigurationsimport bzw. portablem Export zurückgesetzt.
2. Lokalen Assistenten aktivieren. Ollama muss separat auf `127.0.0.1:11434` laufen; ein geeignetes lokales Modell muss bereits installiert sein. **Lokale Modelle laden** und Modell auswählen. MAKRIVO installiert keine Modelle/Dienste, kontaktiert keine OpenAI-/Cloud-API und verwendet keine API-Schlüssel. Die Offline-Konfiguration und Vertrauenswürdigkeit des lokalen Ollama-Dienstes liegen beim Benutzer; deklarierte Remote-/Cloudmodelle werden abgewiesen.
3. Ziel eingeben und Programme/Dokumente/Webseiten ausdrücklich hinzufügen. Übermittelt werden Ziel, Ressourcen-ID, Bezeichnung und Typ; Zielpfade und Inhalte ausgewählter Dateien werden nicht zusätzlich übermittelt; die Zieleingabe selbst wird wörtlich gesendet. Webseitenbezeichnungen enthalten nur Schema/Host, der vollständige Link bleibt lokal.
4. **KI-Entwurf erstellen**. Maximal 90 Sekunden, 1024 Ausgabetokens, begrenzte Antwortgröße. Ziel-/Modell-/Ressourcenänderungen verwerfen die Vorschau; Abbruch/Schließen beendet die Anfrage. Das Modell darf nur ausgewählte IDs vorschlagen. Unbekannte IDs, freie Kommandos, zusätzliche JSON-Felder und ausführbare Dokumenttypen werden zurückgewiesen.
5. Vorschau und offene Punkte prüfen → **Im Editor prüfen**. Das Ergebnis ist zunächst ein ungespeicherter Entwurf. Erst im bestehenden Editor speichern und bei Bedarf ausdrücklich ausführen. Normale Makrovalidierung, Vor-/Nachprüfung, Pause und Abbruch bleiben zuständig.

Unterstützt: Programme ohne Argumente starten, ausdrücklich ausgewählte lokale PDF/DOCX/XLSX/PPTX/TXT/MD öffnen, HTTP(S)-Webseiten öffnen, auf ausdrücklichen Wunsch Mikrofon stummschalten. Kein Headset-Standardwechsel, keine Nachrichten, Termine, Downloads, Skripte, freien Klicks oder Texteingaben. Nicht abbildbare Wünsche erscheinen als offene Punkte, sofern das Modell sie korrekt erkennt; die fachliche Zielerfüllung muss der Benutzer prüfen. Dokumentöffnung ist nicht automatisch fachlich verifiziert.

## Vergleichsexperiment

**Manuelle Einrichtung messen · Start / Ende** misst den vollständigen manuellen Vergleichsablauf. Assistentenannahmen messen die Zeit vom Öffnen des Assistenten bis zum ersten Speichern im Editor; `generated` enthält dagegen nur die Planungsdauer. Keine beiden ungleichen Dauern als Zeitersparnis vergleichen.

Lokale Ereignisse liegen im Konfigurationsordner unter `Experiments/meeting-experiment.jsonl`: Zeitstempel, zufällige Versuch-/Kachel-ID, Ereignis, Dauer, Korrektur ja/nein. Keine Zieltexte, Ressourcenbezeichnungen, Pfade, Modellantworten oder Dokumentinhalte. Datei rotiert ab 2 MiB, eine vorherige Datei bleibt erhalten. Messfehler dürfen Makros und Konfigurationsspeicherung nicht abbrechen.

Erfasst: manuelle Baseline, Modellgenerierung/Fehler/Abbruch, verwerfende Vorschauänderungen/Schließen, erstmaliges Speichern und Korrekturen im Editor sowie tatsächliche Runner-Endzustände für daraus gespeicherte Kacheln. `run_completed` bedeutet nur, dass der Runner abgeschlossen hat; bestätigte Zielwirkungen stehen separat im Prüfverlauf. Wiederholungen desselben Makros sind keine neuen unabhängigen Einrichtungsversuche. Das anfängliche Experiment kann nach Neustart fortgeführt werden, weil die Versuch-ID an der gespeicherten Kachel bleibt. Duplizierte Kacheln teilen diese Herkunft; bei der Auswertung nach Versuch-ID gruppieren.

Erstes Experiment: 20 vergleichbare Besprechungseinrichtungen oder zwei Wochen mit freiwilligen Testnutzern; zunächst interne Nutzung. Manuelle Einrichtungszeit gegen vollständige assistierte Einrichtungszeit vergleichen; zusätzlich Korrektur-/Verwerfungsquote, Runner-Fehler und echte Prüfresultate auswerten. Bei fehlender Verbesserung oder Verletzung der Schutzgrenzen Schalter deaktivieren. Keine Nutzung oder Zahlungsbereitschaft wurde im Repository nachgewiesen. Keine automatische Telemetrieübertragung oder Preisanpassung.

## Prüfstand

- Core: Begrenzungen, Typen, unbekannte/duplizierte IDs, Skriptausschluss, lange Links, optionale Alt-Konfiguration, Import-/Export-Opt-in, datensparsame Messung sowie einmaliges Verwerfen und korrekte Übergabe.
- Transport mit Fake-Modell: Ollama-JSON-Schema, Loopback-Ziel, keine Dateipfade im Prompt, Cloudname und Remotealias abgewiesen, beschädigte/überlange Antwort abgewiesen, Abbruch.
- WPF-Smoke: Dialog gerendert; Entwurfsübergabe verändert keine gespeicherten Kacheln, Review/Speichern erhält Herkunft und erfasst Korrektur, Verwerfen speichert nichts.
- Unabhängiger Review: Vorschauverwerfen und lange Links gefunden und korrigiert; Regressionstests RED→GREEN.
- Release-Build mit null Warnungen/Fehlern, gesamte Core-Suite und WPF-Smoke erfolgreich. Die vollständige Windows-Suite bestand zunächst einschließlich Fokuswechsel. Ein erneuter Lauf scheiterte wieder am verweigerten Vordergrundwechsel; der Test ist kontextabhängig und bleibt ungeändert. Die übrigen Windows-Prüfungen einschließlich Modelltransport bestehen mit ausdrücklich ausgelassenem Vordergrund-Erfolgstest.
- Paketprüfung deckte einen bestehenden Fehler beim atomaren Austausch kurzzeitig geöffneter Konfigurationsdateien auf. Eine Lesesperre reproduzierte denselben Zugriff-verweigert-Fehler; begrenzte Wiederholung nur des atomaren Austauschs ist RED→GREEN geprüft. Dauerhafte Zugriffsfehler bleiben sichtbar.
- Aktuelles eigenständiges Paket `artifacts/release-20261012-184105`: WPF-Smoke nach Speicherkorrektur erfolgreich, einschließlich KI-Dialog und Entwurfsannahme/-verwerfen.
- Kein echter Modelltest: der lokale Ollama-Dienst antwortete bei der Prüfung nicht. Keine Aussagen über Modellqualität, Latenz, semantische Vollständigkeit oder Zeitersparnis. Hardware-/Treiber-/Skalierungsgrenzen des bisherigen Releases bleiben bestehen.

Protokollquellen: [Ollama Chat](https://docs.ollama.com/api/chat), [Structured Outputs](https://docs.ollama.com/capabilities/structured-outputs), [offizielle Typdefinitionen einschließlich Remote-Modellmetadaten](https://github.com/ollama/ollama/blob/main/api/types.go).

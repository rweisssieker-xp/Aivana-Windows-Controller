<!-- generated-doc locale=de-DE source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../MAINTENANCE.md)

# Pflege aller Dokumentationssprachen

Die Dokumentation umfasst alle 27 Sprachen der Oberfläche. Die erzeugten Ordner verwenden regionale Sprachcodes; Norwegisch erhält `nb-NO`, Serbisch in lateinischer Schrift `sr-Latn`. Jeder Ordner enthält einen Index, das Handbuch, die ausführliche Funktionsreferenz, die Funktionsübersicht, Prüfnachweise, Recherchen einschließlich CSV, historische Pläne und Lizenzanalysen.

## Maßgebliche Quellen

Bearbeite die zentrale `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, diese Seite oder die Originaldateien unter `docs/research`, `docs/testing`, `docs/superpowers` und `docs/licensing`. Historische Berichte dürfen nicht nachträglich als Prüfung einer neueren Version dargestellt werden. Unterscheide Implementierung, Simulation, echte Geräteabnahme und kommerzielle Freigabe.

## Erzeugen und prüfen

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Der optionale Online-Generator sendet Dokumentationstexte an den öffentlichen Übersetzungsendpunkt von Google, wie die vorhandenen Lokalisierungswerkzeuge. Er liest keine Benutzerkonfiguration, Geheimnisse oder Modelldateien. Verwende ihn nur für Texte, die an diesen Dienst übermittelt werden dürfen. Codeblöcke, Inline-Code, URLs, Tabellenstruktur und technische Kennungen sind geschützt. Übersetzungscache und eingefrorener Quellenstand liegen im ignorierten Verzeichnis `artifacts/documentation-translation`.

Wenn der Online-Anbieter Anfragen begrenzt, füllt `tools/translate_documentation_local.py` denselben Cache mit einem ausdrücklich bereitgestellten lokalen M2M100-CTranslate2-Modell. Der Stand vom 2026-10-12 verwendet das MIT-lizenzierte Modell facebook/m2m100_418M, konvertiert als JustFrederik/m2m_100_418m_ct2_int8 in Revision `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 und `SentencePiece` 0.2.2 dienen nur der Dokumentationserstellung. MAKRIVO enthält keine Gewichte oder Abhängigkeiten dieses Übersetzungsmodells. Nach dem Füllen des Caches erzeugt der Generator mit `--cache-only` die Dateien ohne Netzwerkanfragen. Das lokale Hilfsprogramm lädt keinen entfernten Modellcode herunter und führt solchen Code nicht aus.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Ohne `--refresh-sources` setzt ein Lauf den eingefrorenen Quellenstand fort. Verwende die Aktualisierungsoption nach Änderungen an Originalen. Vorhandene Übersetzungen werden wiederverwendet. Mit `--locales` lassen sich einzelne Sprachen auswählen; vor der Veröffentlichung müssen alle Sprachen fertig sein. Bei Übersetzungsfehlern werden fehlende Texte nicht stillschweigend durch unübersetzte Originale ersetzt.

Das Manifest erfasst jede Quell- und Zieldatei mit SHA-256-Prüfsummen des normalisierten UTF-8-Textes: LF-Zeilenenden, keine Byte-Order-Markierung. Der Offline-Prüfer kontrolliert die vollständige Abdeckung jeder Sprache, Prüfsummen, lokale Links, Codebeispiele, Tabellen- und CSV-Struktur, Quellen-URLs und nicht aufgelöste Platzhalter. Veraltete Quellenstände werden zurückgewiesen. Eine erfolgreiche Strukturprüfung ersetzt keine muttersprachliche oder juristische Prüfung. Dieser Hinweis steht sichtbar auf den erzeugten Seiten.

## Veröffentlichung und Pakete

Veröffentliche Dokumentation, Generator und Prüfer gemeinsam. Bewahre die ursprünglichen Lizenzbedingungen Dritter; übersetzte Lizenzanalysen dienen nur der Information. Build-Ergebnisse, Übersetzungscache und lokale Orchestrierungsdateien gehören nicht in Git. Eine Veröffentlichung der Quelldokumentation veröffentlicht noch keinen Windows-Installer.

Die App-Übersetzungen liegen getrennt als JSON-Kataloge unter `src/Aivana.Core/Localization`. Die Dokumentationserstellung verändert diese Kataloge nicht. App-Build und Dokumentation sollten denselben versionierten Stand beschreiben; neu dokumentierte experimentelle Laufzeitunterstützung kann einem installierten Paket voraus sein.

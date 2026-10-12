# Lokale KI-Laufzeiten

## Bedienung

Werkzeuge → KI-Besprechungsassistent öffnen, explizit aktivieren und KI-Laufzeit auswählen. Ressourcen freigeben, Ziel formulieren, Entwurf erstellen und im Editor prüfen. Kein Anbieter führt den Entwurf automatisch aus. Es gibt keinen automatischen Anbieterwechsel.

| Laufzeit | Voraussetzung | Verbindung |
|---|---|---|
| Ollama | lokal installiertes Modell | 127.0.0.1:11434 |
| LM Studio | laufender lokaler Server mit lokal geladenem Modell und JSON-Schema-Unterstützung | 127.0.0.1:1234 |
| llama.cpp | laufender llama-server mit lokalem Modell und JSON-Schema-Unterstützung | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | kompatibler, vertrauenswürdiger lokaler GenAI-Textmodellordner | separater gebündelter Prozess, kein HTTP |

Die HTTP-Clients nutzen keine Proxys und folgen keinen Weiterleitungen. Lokale Server können selbst weitere Dienste ansprechen; insbesondere LM Link/Remote-Routing darf für lokale Verarbeitung nicht verwendet werden. Dies kann MAKRIVO durch die kompatible Schnittstelle nicht zuverlässig erkennen. Ollama-Remote-Metadaten und Cloudnamen werden zusätzlich abgewiesen. Authentifizierte lokale Server sind in dieser Fassung nicht konfigurierbar.

## ONNX

Im Dialog genai_config.json auswählen. Der Ordner muss tokenizer.json und eine Chatvorlage enthalten. Unterstützt werden chat_template als Zeichenkette, eine benannte default-Vorlage (Array oder Dictionary) sowie eine separate chat_template.jinja, falls keine Inline-Vorlage vorhanden oder diese null ist. Mehrdeutige Vorlagen werden abgewiesen; maximal 65536 Zeichen. Beliebige .onnx-Dateien, Vision-/Embeddingmodelle oder reine GPU-/NPU-Exporte sind keine Besprechungsmodelle.

Gebündelt: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Anbieter-Konfiguration wird auf CPU zurückgesetzt. Kein Download und keine Modellgewichte im Paket. Keine Pythoninstallation erforderlich. Nur vertrauenswürdige Modellpakete verwenden; der separate Prozess ist eine Absturz-/Abbruchgrenze, kein Sicherheitssandbox.

Grenzen: 90 Sekunden inklusive Modellladen, maximal 3072 Prompttokens und 1024 generierte Tokens, zusätzlich Modellkontextgrenze; streng geprüftes JSON und bekannte Ressourcen-IDs. Fehlerhafte/abgeschnittene Antwort wird verworfen. Schließen/Abbrechen beendet den gestarteten Workerprozess. Keine freien Modellbefehle und keine Programme/Dateipfade in der getrennten Ressourcenliste des Prompts; der vom Benutzer geschriebene Zieltext kann solche Daten selbst enthalten.

## Verifikation

- Core-Suite, Loopback-Transporttests mit simulierten LM-Studio-/llama.cpp-/Ollama-Antworten, Schema/Abbruch/Ressourcenprüfung.
- Unicode-Protokollregression mit maximal langen Zielen und 24 Ressourcennamen.
- Echte native CPU-Inferenz mit offiziellem tiny-random-gpt2-fp32-Testgraph aus microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testvorlage ergänzt und Byte-Tokenizer auf den kleinen Zufallsgraph angepasst. Dieses Zufallsmodell prüft keine Qualität und ist nicht im Release.
- Reproduzierbarer nativer Laufzeittest: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-Modellordner>.
- WPF-Smoke prüft die Auswahl aller vier Anbieter und den Prüfstand; Auswahl allein erzeugt keine Modellanfrage.

## Verbleibende echte Freigaben

Hardware-/Smartphone-/Tablet-/Monitorwechseltests sind weiterhin erforderlich. Exklusive Tastaturunterdrückung bleibt ohne signierten, lizenzierten und geprüften Filtertreiber gesperrt. Nicht durch Dummytreiber oder Raw Input ersetzt. Der Fokuswechsel kann weiterhin durch Windows blockiert werden; der Fehler nennt jetzt die manuelle Aktivierung des Zielfensters. Zentralen KI-Bedienelementen wurden Übersetzungen in allen 27 Katalogen hinzugefügt; ausführliche neue Texte und fachliche Übersetzungsabnahme sind noch offen. Live-Qualität mit einem geeigneten Instruct-Modell, Dauerbetrieb und gemessener Zeitgewinn sind nicht durch Zufallsmodell-/Fake-HTTP-Tests belegt.

## Quellen

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Finaler Prüfstand: Release-Build ohne Warnungen/Fehler; vollständige Core- und Windows-Suite bestanden, einschließlich nativem Fokusfixture, HTTPS, UIA, Plugin-IPC und kompatiblen KI-Transporttests. Neues Paket release-20261012-190618: WPF-Smoke bestanden (19:07 Uhr), gebündelte ONNX-CPU-Inferenz mit Testgraph bestanden. Unabhängiger Review: Unicode-Protokollgrenze korrigiert, keine weiteren konkreten Befunde. Keine physische Hersteller-/Telefon-/Monitorabnahme daraus abgeleitet.

## Fortsetzung: echte Modellqualität und Vorlagen

Zusätzlicher CPU-Lauf mit allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native Inferenz bestanden. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Community-Export nur im Testbereich, nicht im Release.

Vier echte Planungstests (deutsch/englisch, Dokument, Programm, explizite Mikrofon-Stummschaltung): **0/4 gültige Pläne**; jeweils JsonException bei strenger Antwortprüfung. Laufzeiten 63501, 6376, 72118 und 6774 ms. Keine Aktionen ausgeführt. Dieses kleine Modell erfüllt den Planungsvertrag nicht und wird nicht als geeignet empfohlen. Bericht: artifacts/local-ai-evaluation/smollm360-results.json. Reproduktion: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Die Laufzeitfunktion belegt keine Modellqualität.

Werkzeug-Navigation und Prüfbericht wurden zusätzlich in allen 27 Sprachkatalogen übersetzt. Langtexte und sprachliche Abnahme bleiben offen. Vorlagenregression prüft auch falsche JSON-Typen, leere/mehrdeutige/überlange Vorlagen und null mit Jinja-Fallback.

Aktualisiertes Paket: artifacts/release-20261012-201134. Gepackter WPF-Smoke am 12.10.2026 um 20:12 bestanden; native CPU-Inferenz mit dem separaten Instruct-Testmodell im gebündelten Worker bestanden. Core und Windows-KI-Tests bestanden; Build ohne Warnungen/Fehler. Modell-Planungsqualität weiterhin 0/4.

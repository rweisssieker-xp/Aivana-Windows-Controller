<!-- generated-doc locale=de-DE source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../FEATURES.md)

# MAKRIVO – Featureübersicht

Hersteller: **Aivana**. Stand: **12. Oktober 2026**. Dies ist der Entwicklungsstand, keine Verkaufs- oder Hardwarefreigabe.

## Windows-Steuerung

- Kacheln, globale Hotkeys, Befehlspalette, Favoriten und anwendungsabhängige Profile.
- Makros mit mehreren Schritten, Wiederholungen, Pausen, Abbruch und Vorschau.
- Programme, Dateien und Webseiten öffnen; Text, Zwischenablage und Tastenkürzel verwenden.
- Fenster anordnen, Windows sperren, Audioausgänge und Mikrofone steuern sowie Bildschirmhelligkeit über unterstützte Schnittstellen ändern.
- OBS-WebSocket-v5-Anbindung für Szenen, Aufnahme, Streaming und Audio.
- Vorlagen, Arbeitsplatz-Assistent, Geräteerkennung und Tastatur-Overlay.
- Import und Export von Profilkonfigurationen; Entwurfsprüfung und Konflikterkennung.
- Lokal gespeicherte Einstellungen; kein Konto für die alltägliche Nutzung erforderlich.

## Erweiterte Funktionen im aktuellen Quellcode

| Bereich | Funktionsumfang | Grenzen |
| --- | --- | --- |
| Ergebnisprüfung | Vorprüfung, Nachkontrolle und Prüfverlauf | Nur überprüfbare Wirkungen bestätigen; ein erfolgreicher Tastendruck beweist nicht die Zielwirkung. |
| Arbeitsplatz-Assistent | Profile und Abläufe aus ausgewählten Programmen, Dateien, URLs und weiteren Aktionen vorbereiten | Vorschau und Speichern erforderlich; Einrichtung führt nichts aus. |
| Makroaufnahme und Workflows | Aufnahme, Bedingungen, Gesten und lokale Profilsynchronisierung | Verhalten und Grenzen sind in den Implementierungs- und Testberichten dokumentiert. |
| UI-Automation | Ziele auswählen, Selektoren prüfen, Aktionen und begrenzte Klickabläufe aufnehmen | Separater Worker; mehrdeutige, deaktivierte und Passwortziele werden abgewiesen; keine allgemeine Texteingabeaufnahme. |
| Erweiterungen | Pakete prüfen, importieren, nach ausdrücklichem Vertrauen aktivieren, exportieren und zurückrollen | Prozessisolation ist keine Sandbox; kein öffentlicher Plugin-Marktplatz. |
| Mobilpanel | Lokales HTTPS-Panel, Kopplung, ausgewählte Profile und Status-/Prüfverlauf | Reale Telefon-/Tablet-LAN-Abnahme noch offen; kein universelles Sensor-Dashboard. |
| Herstelleradapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB und Wooting-Analogadapter | Modell-/SDK-abhängig; neue Adapter nicht vollständig mit realer Hardware validiert. Keine neuen proprietären SDK-DLLs gebündelt. |
| Physische Tastaturzuordnung | Standardtasten anhand Gerät und Scan-Code erkennen | Raw Input unterdrückt die normale Windows-Eingabe nicht. |
| Lokaler KI-Besprechungsassistent | Optionaler Prototyp für Ollama, LM Studio, llama.cpp und ONNX Runtime GenAI CPU erzeugt begrenzte Entwürfe aus ausdrücklich ausgewählten Ressourcen | Standardmäßig aus; native CPU-Laufzeit mit Zufallsmodell geprüft, aber keine belegte Planungsqualität oder Zeitersparnis; keine Modellgewichte enthalten. |

## Noch nicht als fertiges Verkaufsfeature anbieten

- **Exklusive Zweittastatur/Makropad:** Ein signierter, lizenzierter und hardwaregeprüfter Filtertreiber fehlt. Normale Tasteneingaben können weiterhin Windows erreichen.
- Vollständige Hardwareabnahme aller Herstelleradapter sowie reale Mobilgeräte-LAN-Tests.
- Echte gemischte Monitor-/DPI-Wechsel und fachliche Abnahme aller neuen Übersetzungen. PerMonitorV2 und simulierte 150/200-Prozent-Layouts wurden geprüft; reale Displaytests bleiben separat. Die Oberfläche bietet 27 Sprachen; ausführliche neue Texte sind noch nicht vollständig sprachlich abgenommen.
- Signierter Verkaufsinstaller, endgültige EULA, geprüfte SDK-Vertriebsrechte und produktive Kauflizenz-/Checkout-Integration.

## Details und Nachweise

- [Bedienungsdokumentation](README.md)
- [Erweiterungen: Funktionen, Tests und offene Grenzen](testing/2026-10-12-six-extensions.md)
- [Lokaler KI-Assistent: Prototyp und Prüfstand](testing/2026-10-12-local-meeting-assistant.md)
- [Lokale KI-Laufzeiten und Modellvoraussetzungen](testing/2026-10-12-local-ai-runtimes.md)
- [DPI- und Skalierungsprüfung](testing/2026-10-12-dpi-scaling.md)
- [Lizenzprüfung](licensing/2026-10-12-commercial-license-audit.md) – historische Bestandsaufnahme; das neue Publish-Skript ergänzt Runtime-Hinweise. Das endgültige Paket bleibt separat zu prüfen.
- [Vertriebsplattformen](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Vergleichsstand; Stripe ist zusätzlich eine mögliche Zahlungsplattform, siehe aktuelle Anbieterbedingungen.

## Vollständiges Entwicklungspaket erzeugen

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Das Paket enthält Desktop-App, UI-Automation-Worker, Mobilhost, Browserdateien, Beispielplugin und Runtime-Lizenzhinweise. Buildartefakte werden nicht in Git eingecheckt. Ein GitHub-Quellcodestand ist noch kein veröffentlichter Windows-Installer.

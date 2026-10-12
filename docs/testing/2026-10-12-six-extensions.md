# Sechs Erweiterungen: Implementierung und Prüfstand

Stand: 12. Oktober 2026, Branch `codex/six-extensions`. Arbeitsstand, keine Produktionsfreigabe.

## Bedienung

Die Befehlspalette öffnet die Einträge **Arbeitsplatz einrichten**, **Tastaturhersteller und SDKs**, **Mobilpanel**, **Erweiterungen**, **UI-Automation**, **UI-Ablauf aufnehmen** und **Ausführungsprüfung**.

- Der Arbeitsplatz-Assistent erstellt Profile für Arbeitsplatz, Besprechung, Streaming, Recherche und Projekt. Programme aus Registrierung/Startmenü oder Dateiauswahl, URLs, Ausgabe/Bildschirm und OBS-Szene werden vor dem Speichern als Schritte angezeigt. Die Einrichtung führt keine Aktionen aus.
- Im Schritteditor schaltet **Ergebnis überprüfen** die Vorprüfung und Nachkontrolle ein. Verlauf und Mobilpanel unterscheiden tatsächliche Bestätigung von nicht überprüfbarer Wirkung. Relative Änderungen, Tastenkürzel und Texteingabe liefern keine pauschale Wirkungsbestätigung. Ein Prozessnachweis belegt keine fachliche Bereitschaft der Anwendung.
- UI-Automation verwendet einen separaten, zeitlich begrenzten Worker. Auswahl über Mausposition, Selektorprüfung, Invoke/Select/Toggle/Focus/SetValue/WaitState; mehrere Treffer, deaktivierte Ziele und Passwortfelder werden abgewiesen. Die Aufnahme erfasst unterstützte Klickaktionen und Pausen; Texteingaben werden nicht mitgeschnitten. Vor Übernahme wird die Aufnahme geprüft.
- Erweiterungen: ZIP-Prüfung, Hashes, Schema, deaktivierter Import, ausdrückliches Vertrauen vor Aktivierung, isolierter Prozess mit begrenzter IPC, Export, Deaktivierung, Deinstallation, vorherige Version wiederherstellen/verwerfen. Prozessisolation ist keine Sandbox: freigegebene Plugins laufen mit Benutzerrechten. Beispielpaket `makrivo.example.zip` enthält Countdown und Statusaktion. Unterstützte Schemas bilden einen dokumentierten einfachen JSON-Schema-Teilumfang im Quellcode ab; kein öffentlicher Marktplatz.
- Mobilpanel: lokale IP und freigegebene Profile auswählen, HTTPS-Adresse öffnen, Zertifikat anhand des angezeigten SHA-256-Fingerprints prüfen und bewusst vertrauen, dann einmaligen sechsstelligen Code eingeben. Private Schlüssel sind mit Windows-DPAPI geschützt; nur Hashes der Clientzugänge werden gespeichert. Zertifikate ändern sich nach Erneuerung oder IP-Wechsel. Freigabe der Windows-Firewall erfolgt bei Bedarf durch den Benutzer. Keine automatische Zertifikatsinstallation, Portweiterleitung oder Firewalländerung. Clientzugänge lassen sich widerrufen. Das Panel zeigt Aktionszustand und Prüfverlauf; es ist noch kein universelles Live-Sensor-Dashboard.

## Tastaturen

| Hersteller | Implementierung | Einschränkung |
|---|---|---|
| Corsair | bestehende iCUE-G-Tasten und RGB | lokale iCUE-Verbindung und passendes Modell nötig |
| Logitech G | G-Key-SDK und separates LED-SDK | originale lokale x64-DLL ausdrücklich freigeben; G-Key-SDK liefert keine individuelle Identität mehrerer Tastaturen |
| Razer | Chroma-RGB und Standardtasten-Erkennung | proprietäre Tasten zuvor in Synapse zuordnen |
| SteelSeries | GameSense-RGB und Standardtasten-Erkennung | proprietäre Tasten zuvor in GG zuordnen |
| Wooting | gerätebezogene analoge Werte, Druck-/Loslassschwellen | originale lokale x64-SDK-DLL; normale HID-Eingabe bleibt aktiv |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/weitere | physische Standardtasten nach Gerät und Scan-Code | Fn und proprietäre Steuerelemente modellabhängig |

Keine neuen proprietären SDK-DLLs werden mitgeliefert. Die neuen Herstelleradapter sind ohne passende physische Geräte noch nicht hardwarevalidiert.

**Exklusives zweites Makropad ist nicht fertig:** Raw Input beobachtet Geräte getrennt, unterdrückt aber deren normale Windows-Eingabe nicht. Eine Produktionsfreigabe erfordert einen lizenzierten, signierten, unter Windows 11/Secure Boot geprüften Filtertreiber sowie Tests von Notfreigabe, Absturz, Suspend und Wiederanschluss. Kein Treiber wurde installiert. Die Anwendung kennzeichnet diese Grenze ausdrücklich.

## Prüfungen

- Release-Build: erfolgreich, null Warnungen/Fehler.
- Core-Konsole: alle Prüfgruppen erfolgreich, einschließlich Konflikten bei Herstellertasten, Plugin-Rollback und tatsächlichen Ausführungsfehlern im Prüfverlauf.
- Erweiterungsintegration: echter UIA-Worker mit WPF-Testanwendung; eindeutiger Invoke, Mehrdeutigkeit, deaktivierte Ziele, Passwortausschluss. Echter Named-Pipe-Pluginprozess, Protokollgrenzen, Analog-Hysterese und Logitech-ABI-Decodierung erfolgreich.
- Echter HTTPS-Host: gepinntes Zertifikat, ungekoppelter Zugriff, falscher/einmaliger Code, Origin-Schutz, Browserdateien, Hashspeicherung und geschützter Schlüssel erfolgreich. Ein im Test entdeckter Schannel-Fehler mit ephemeren Schlüsseln wurde behoben.
- Windows-Suite mit ausdrücklich ausgelassenem Vordergrund-Erfolgstest: erfolgreich; Audio/OBS/iCUE/Raw Input und Bildschirmstatus geprüft. Beim früheren Lauf scheiterte die vollständige Suite am verweigerten Fokuswechsel zum isolierten Testfenster. Im anschließenden Lauf zum KI-Prototyp bestand auch die vollständige Suite einschließlich Fokuswechsel und verbundener iCUE-Tastatur. Dieser Test wurde nicht abgeschwächt. Kurzzeitig fehlgeschlagene DDC-Lesezugriffe werden begrenzt wiederholt; es wurde keine Helligkeit verändert.
- WPF-Smoke: alle bisherigen und neuen Dialoge gerendert, Konfigurationsinvarianz, Editor und CRUD erfolgreich; Screenshots im Buildordner `smoke`.
- Browser: Playwright mit Edge, 390×844 und 1024×768, Kopplungsfluss/UUID/einmaliges Senden/kein horizontaler Überlauf erfolgreich. API-Mock; echter HTTPS-Test separat. Bilder unter `artifacts/mobile-browser`. Browser-Plugin war nicht verfügbar, daher regulärer Playwright-Fallback.
- Unabhängiger Code-Review durchgeführt; verspätete Mobilbefehle, fehlende terminale Prüfergebnisse, Prozesspfadprüfung, Plugin-Rollback und Wiederanlauf des RGB-Heartbeat korrigiert.

Noch offen: exklusive Treiberintegration, physische Hersteller-/Telefon-/Tablet-LAN-Tests, Windows-Skalierung 150/200 Prozent und vollständige Übersetzungsprüfung aller neuen Dialogtexte. Neue Dialogtexte sind überwiegend deutsch; bestehende Sprachkataloge bleiben erhalten. Deshalb ist die Gesamtanforderung **nicht vollständig abgenommen**.

## Paket erzeugen

`./tools/publish-complete.ps1 -SelfContained` erzeugt einen neuen Ordner unter `artifacts/release-*`, einschließlich Desktop, UIA-Worker, HTTPS-Host, Browserdateien, importierbarem Beispielplugin und Runtime-Lizenzhinweisen. Die installierte Anwendung wird nicht überschrieben. Ohne `-SelfContained` benötigt das Paket passende .NET-10-Desktop-/ASP.NET-Runtimes.

Das Paket ist ein lokaler Prüfstand, kein signierter Verkaufsinstaller. Eigene Nutzungsbedingungen und SDK-Vertriebsrechte sind separate Freigaben; siehe bestehende Dokumentation unter `docs/licensing`.

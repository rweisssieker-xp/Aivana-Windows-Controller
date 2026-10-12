<!-- generated-doc locale=de-DE source=docs/superpowers/plans/2026-10-12-six-extensions-progress.md sha256=aaa5db1c77f8f730220146a230868b9df95d87653b48708f49aa1418db56fa5d -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/plans/2026-10-12-six-extensions-progress.md)

# Ausführungsbuch: 2026-10-12-six-extensions
Vom Benutzer autorisierte native Implementierung ohne weitere Fragen.
Entscheidung: Verwenden Sie den neuen Codex/Six-Extensions-Zweig im aktuellen Checkout, um vorhandene, nicht festgeschriebene Funktionen beizubehalten. Ein neuer Arbeitsbaum würde Abhängigkeiten weglassen. Keine Installations- oder Treiberänderungen während der Tests.
Preflight: Überprüfungs-Feeds-Assistent/UIA/Plugin/mobile Ergebnisansichten; Herstellerfunktionen versorgen die Tastaturzuordnung und das Treiber-Backend. Vorhandene numerische Enum-Werte müssen stabil bleiben.
Aufgabe 1: in Bearbeitung; Nicht bestandener Verifizierungstest eingeführt.

Aufgaben 1–3: Verifizierung Verträge/Anbieter/Herausgeber/Historie und Assistent implementiert; Kernprüfungen bestehen; Die UI-Akzeptanz wird noch erweitert.
Aufgaben 4–5: isolierter UIA-Worker, Picker und überprüfter Klickrekorder implementiert; Echte Fixture-Tests bestanden. Aufruf/Mehrdeutigkeit/Deaktiviert/Passwort-Ausschluss. Gefundene/korrigierte Abschneidung von Prozessnamen mit Punkten.
Aufgaben 6–7: Paketüberprüfung/-import/deaktiviert, aktiviert/exportiert/deinstalliert und isolierter ausführbarer IPC implementiert; echte Muster-IPC-Durchgänge. Kein öffentlicher Marktplatz.
Aufgaben 8–9: HTTPS-Panel, Pairing, gehashte Anmeldeinformationen, geschützter TLS-Schlüssel und zugelassener IPC implementiert; HTTP-/mobile Verifizierung steht aus.
Aufgaben 10–11: physische Scancodes, Logitech G SDK und Wooting Analogadapter sowie RGB-Protokolle implementiert; Es wurde keine kompatible Herstellerhardware über die bestehende Corsair hinaus getestet.
Aufgabe 12: Externes Gate bleibt bestehen: produktionssigniert/lizenziert Windows 11 Exklusivfilter nicht verfügbar. Ersetzen Sie nicht die falsche Unterdrückung.
Entscheidung: Companions mithilfe von MSBuild-Zielen erstellen; Veröffentlichen Sie alle Hosts zusammen, um UIA/Mobile über die App nutzbar zu halten.
Urteil: Keine proprietäre, weitervertreibbare SDK-DLL ohne verifizierte Rechte gebündelt; Durch die explizite lokale SDK-Genehmigung wird die ausgewählte Binärdatei gehasht.

Abschließende Überprüfung: Der neue Prüfer /root/final_review überprüfte den aktuellen Arbeitsbaum, Plan/Spezifikation und Release-Gates. Keine Korrekturen durch den Rezensenten.
Abschließend: Verspätete mobile Dispatcher-Befehle wurden behoben, indem die Anforderungsstornierung beim Dispatch und bei der Rückrufeingabe angewendet wurde.
Final: Fehlender Executor-Fehler/Abbruch-Überprüfungs-Terminalstatus behoben; Ausführungsfehler-Regression ROT→GRÜN; Preflight-Timeout-Status beibehalten.
Abschließend: Die Überprüfung des Nur-Basisnamen-Prozesses durch Vergleich des absoluten MainModule-Pfads oder die Meldung von nicht überprüfbar wurde korrigiert.
Finale: Lebenszyklus des Plugins mit Rollback-Kontrollen zum Wiederherstellen/Verwerfen korrigiert; neue API-Testkompilierung ROT→GRÜN; Durch die Deaktivierung werden aktive Arbeitnehmer storniert. Fehlende Abhängigkeiten lehnen den Aufruf ab.
Final: Heartbeat-Neustart bei fehlgeschlagenen Razer/GameSense-Sitzungen behoben; Kein physischer RGB-Gerätetest verfügbar.
Abschließend: Anbietergesten-/Layer-Konfliktvalidierung hinzugefügt, Testkompilierung ROT→GRÜN.
Endgültig: echtes HTTPS ROT→GRÜN: Schannel lehnt kurzlebige Serverschlüssel ab; Verwenden Sie einen temporären Benutzerschlüsselcontainer und DPAPI-geschütztes persistentes PFX, kein Klartext verwaltetes PFX.
Endgültig: Das Aktionsbudget der UIA umfasst eine begrenzte Starthilfe für Arbeitnehmer; Arbeiter wurde immer bei abnormaler Ausgabe getötet.
Endgültig: Vorübergehender DDC-Lesefehler reproduziert (leerer Snapshot, dann stabil, gleiche Monitor-ID); Der begrenzte Lesewiederholungsversuch besteht den ursprünglichen Stable-Enumeration-Test ohne Schreibhelligkeit.
Aufgabe 13: Build 0 Warnungen/Fehler freigeben; Kernsuite grün; Windows Suite grün mit explizitem --skip-foreground-fixture; Die ursprüngliche vollständige Suite weist einen nativen Aktivierungsfehler im Vordergrund auf und wird beibehalten. Echte UIA, Plugin IPC und HTTPS prüfen grün. WPF rauchgrün. Edge/Playwright-Telefon- und Tablet-Layouts grün mit API-Mock, tatsächliches HTTPS separat grün. Vollständig eigenständiges Paket, generiert von tools/publish-complete.ps1 mit Hinweisen zu hosts/assets/example/runtime.
Entscheidung: keine Zusammenführung/Übertragung gemischter, bereits vorhandener Benutzeränderungen. Behalten Sie die Änderungen und die Verzweigung bei. Kein Abschlussmenü, da der Benutzer keine Fragen gestellt hat und die vollständigen Freigabekriterien nicht erfüllt sind.
Entscheidung: Aufgabe 12 unvollständig: signierter/lizenzierter Produktionsfilter und wiederherstellbare Hardwaretests für zwei Tastaturen nicht verfügbar. Kein Treiber installiert. Keine ausschließliche Unterdrückung beansprucht.
Urteil: Hardware-Akzeptanz, echte mobile LAN-Akzeptanz, High-DPI 150/200 und komplette Neuübersetzungen bleiben ungeprüft; Detaillierte Offenlegung docs/testing/2026-10-12-six-extensions.md. Der Gesamtantrag wurde trotz funktionierender Software-Ergänzungen nicht vollständig angenommen.

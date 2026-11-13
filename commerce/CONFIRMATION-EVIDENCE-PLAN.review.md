# Vertragsbestätigung: Nachweisbaustein – Umsetzungsplan

Ziel: Die bereits gespeicherte Fassung der Kaufbedingungen und die beiden getrennten Erklärungen für genau eine Bestellung unverändert zu einem lokal prüfbaren Datenbaustein zusammenstellen. Grundlage sind der freigegebene Verkaufsstartentwurf und `DIGITAL-CONSENT.review.md`. Dieser Baustein ist keine vollständige Vertragsbestätigung und kein Versand.

Vorgehen: Eine eigenständige, nicht vom Worker importierte Funktion bekommt explizit die zugeordnete Bestellung, die Annahme und die Erklärungen. Sie prüft Bestellzuordnung, Sprachfassung, Art, Anzahl, Zeitstempel, Text-/Versionsgrenzen und die gespeicherten SHA-256-Prüfsummen. Bei Fehlern gibt sie keinen Teilbaustein aus. Erfolgreiche Ausgabe enthält ausschließlich die übermittelten, validierten Nachweise – keine Wiederherstellungscodes, Zahlungskennungen, Gerätekennungen oder fremden Datensätze. Es werden keine Texte, Empfänger, Zahlung oder Versand erfunden. Die Prüfung ist keine Rechtsfreigabe und ersetzt keine Prüfung der Datenherkunft oder Zahlung.

- [x] Fehlertests mit synthetischen DE-/EN-Nachweisen schreiben und zunächst fehlschlagen sehen.
- [x] Funktion ohne Netzwerk, Datenbankzugriff, Endpunkt und Nachrichtenversand implementieren; keine Freigabesperre ändern.
- [x] Vollständigkeit der drei Nachweise, Fremdbestellung, doppelte/fehlende Art, Sprachabweichung, beschädigte Prüfsumme und unsichere Eingaben prüfen.
- [x] Gesamte Commerce-Testsuite ausführen und Grenzen dokumentieren; keine Daten oder Versand behaupten, die nicht vorhanden sind. Am 13. November 2026: 110 Tests erfolgreich, darunter fünf synthetische Prüfungen des neuen Bausteins.

Vor einer verwendbaren Bestätigung zusätzlich nötig: geprüfte vollständige Vertrags-, Widerrufs-, Muster- und Länderinformationen, Zahlung und Empfängerzuordnung, datenschutzgerechter Versand, Zustell-/Fehlerbehandlung und eine unabhängige Ende-zu-Ende-Abnahme. Eine regionale Steuer- oder Verbraucherfreigabe wird aus diesem Baustein nicht abgeleitet.

`confirmation-evidence.mjs` ist nicht in den Worker eingebunden. Seine Funktion validiert eine ausdrücklich übergebene Momentaufnahme, authentisiert aber weder den Datenbankleser noch den Kunden und stellt keine Zahlung fest. Die Prüfsummen sind Konsistenzprüfungen, keine digitale Signatur und kein Beweis, dass ein Kunde die Texte gelesen hat. Das Ergebnis ist nicht für sich allein zur Übersendung als vollständige Vertragsbestätigung freigegeben.

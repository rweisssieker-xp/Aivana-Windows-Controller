# Getrennter Teststatus für den Support

Stand 15. November 2026. Dieser manuelle Statuscheck liest nur die getrennte MAKRIVO-Testumgebung. Er ist kein laufender Alarm, kein externer Lebenszeichencheck und kein Nachweis der Betriebsbereitschaft. Er nimmt keine Kundenanfragen entgegen, ändert keine Daten und versendet keine Nachrichten.

Im Verzeichnis `commerce` ausführen:

```powershell
python monitor_support.py
```

Der Befehl liest das bereits vorhandene Wrangler-OAuth-Zugangstoken aus der lokalen Wrangler-Konfiguration nur in den Speicher und überträgt es als HTTPS-Autorisierungsheader an Cloudflare. Er speichert das Token nicht, schreibt es nicht in die Konsole und gibt keine Fehlermeldungen oder Antworttexte des Providers aus. Der Zugriff erfordert ein lokal berechtigtes Benutzerkonto; die bestehende Token-Datei ist wie ein Zugangsschlüssel zu schützen. Die festen Ressourcen-IDs beschränken den Check auf die MAKRIVO-Testsubscription, die beiden Testqueues und die separate Test-D1. Ein technisch als HTTP-POST ausgeführter D1-Request enthält ausschließlich eine fest vorgegebene SELECT-Aggregatabfrage. Er liest weder Empfänger noch Mailinhalte, einzelne IDs oder Ereignisrohtexte.

Die Ausgabe unterscheidet aggregierte Fehler, ungewisse und hängende Versuche, ausstehende Zustellmeldungen, nicht zugeordnete Ereignisse, Rückstau und die Fehlerwarteschlange. Queuemetriken sind laut Cloudflare Näherungswerte. Ein deaktiviertes Abonnement wird immer gemeldet, auch bei leeren Queues. `status=ok` bedeutet nur, dass die abgefragten Testsignale im Moment der Abfrage keine dieser Auffälligkeiten zeigten. Es belegt weder laufende Mailzustellung noch die Funktion eines Schedulers, eines unabhängigen Alarms oder den Eingang einer Testmail.

Exitcode `0` bedeutet keine der geprüften Auffälligkeiten; `1` bedeutet Handlungsbedarf; `2` bedeutet, dass der Status nicht sicher ermittelt werden konnte und ebenfalls geprüft werden muss. Die Ausgabe enthält keine Anweisung zu einer automatischen Wiederholung oder Lizenzfreigabe. Der Testempfänger und die Verkaufs- und Datenschutzsperren bleiben unverändert.

Am 15. November zeigte der echte Lesezugriff `subscription_disabled`, null Elemente in beiden Testqueues und keine offenen aggregierten Testfälle. Die Quelle blieb nach den vorherigen Tests ausdrücklich deaktiviert. Ein dauerhafter Aufruf an einem Betreiber-PC allein ist kein verlässlicher Alarm: Vor Livebetrieb sind ein unabhängig betriebener Alarm-/Heartbeat-Kanal, zugriffsbeschränkte Fallbearbeitung, Eskalation, sichere Wiederholung und Tests für Ausfälle des Überwachungsdienstes nötig. Der Zugriff auf die Cloudflare-Alarmverwaltung scheiterte mit dem verfügbaren Wrangler-OAuth an HTTP 403.

Der Ablauf und die Grenzen für die Fallbearbeitung stehen in `MAIL-SUPPORT.review.md`. Queue-Metriken und ihre Genauigkeit sind in der [Cloudflare-Dokumentation](https://developers.cloudflare.com/queues/observability/metrics/) beschrieben.

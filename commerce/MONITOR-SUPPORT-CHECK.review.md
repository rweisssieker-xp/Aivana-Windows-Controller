# Getrennter Teststatus für den Support

Stand 15. November 2026. Dieser manuelle Statuscheck liest nur die getrennte MAKRIVO-Testumgebung. Er ist kein laufender Alarm, kein externer Lebenszeichencheck und kein Nachweis der Betriebsbereitschaft. Er nimmt keine Kundenanfragen entgegen, ändert keine Daten und versendet keine Nachrichten.

Im Verzeichnis `commerce` ausführen:

```powershell
python monitor_support.py
```

Für eine begrenzte, pseudonyme Test-Fallliste kann `python monitor_support.py --cases` verwendet werden. Die Ausgabe enthält höchstens 50 synthetische Vorgangs- oder Ereignisreferenzen, Art und Prüfgrund; bei mehr Einträgen wird `truncated` gesetzt und `next_cursor` ausgegeben. Die Liste umfasst Versandversuche und unzugeordnete Ereignisse; Warteschlangenrückstände erscheinen als aggregierte Warnung. Sie enthält keine Anbieterkennungen, Empfänger oder Mailinhalte und erstellt keine Supporttickets. Ungültige oder leere Anbieterkennungen bei `accepted` werden sofort als Prüfungsfall behandelt.

Für die nächste Seite die drei Werte aus `next_cursor` übernehmen, beispielsweise `python monitor_support.py --cases --after-kind purchase --after-reference test-1 --after-as-of 2000000`. Ohne nächsten Cursor ist die Seite vollständig. Der Zeitwert bleibt für alle Seiten eines Durchlaufs gleich. Es handelt sich um eine Schlüsselposition, nicht um einen Datenbank-Snapshot oder einen Zugangsbeleg: Änderungen während des Blätterns können dazukommen oder entfallen; deshalb nach einem Durchlauf wieder bei der ersten Seite beginnen. Der lesende Testcheck enthält keine Bearbeitung oder sichere Identitätsprüfung und ist nicht für echte Kundendaten zugelassen.

Der Befehl liest das bereits vorhandene Wrangler-OAuth-Zugangstoken aus der lokalen Wrangler-Konfiguration nur in den Speicher und überträgt es als HTTPS-Autorisierungsheader an Cloudflare. Er speichert das Token nicht, schreibt es nicht in die Konsole und gibt keine Fehlermeldungen oder Antworttexte des Providers aus. Der Zugriff erfordert ein lokal berechtigtes Benutzerkonto; die bestehende Token-Datei ist wie ein Zugangsschlüssel zu schützen. Die festen Ressourcen-IDs beschränken den Check auf die MAKRIVO-Testsubscription, die beiden Testqueues und die separate Test-D1. Ein technisch als HTTP-POST ausgeführter D1-Request enthält ausschließlich eine fest vorgegebene lesende SELECT-Abfrage. Standardmäßig werden nur aggregierte Werte zurückgegeben; optional liefert `--cases` begrenzte pseudonyme Testreferenzen. Empfänger, Mailinhalte und Ereignisrohtexte werden nicht ausgelesen.

Die Ausgabe unterscheidet aggregierte Fehler, ungewisse und hängende Versuche, ausstehende Zustellmeldungen, nicht zugeordnete Ereignisse, Rückstau und die Fehlerwarteschlange. Queuemetriken sind laut Cloudflare Näherungswerte. Ein deaktiviertes Abonnement wird immer gemeldet, auch bei leeren Queues. Die isolierte Monitoring-Datenbank enthält keine Kauf- und Widerrufsaufträge; deshalb meldet der Check immer `unclaimed_jobs_not_monitored` und damit `attention`. Er kann niemals einen vollständigen fehlerfreien Gesamtzustand bescheinigen. Auch eine leere Fallliste belegt weder laufende Mailzustellung noch die Funktion eines Schedulers, eines unabhängigen Alarms oder den Eingang einer Testmail.

Exitcode `0` bedeutet keine der geprüften Auffälligkeiten; `1` bedeutet Handlungsbedarf; `2` bedeutet, dass der Status nicht sicher ermittelt werden konnte und ebenfalls geprüft werden muss. Die Ausgabe enthält keine Anweisung zu einer automatischen Wiederholung oder Lizenzfreigabe. Der Testempfänger und die Verkaufs- und Datenschutzsperren bleiben unverändert.

Am 16. November zeigte der echte Lesezugriff `unclaimed_jobs_not_monitored` und `subscription_disabled`, null Elemente in beiden Testqueues und keine offenen aggregierten Testfälle. Die Quelle blieb nach den vorherigen Tests ausdrücklich deaktiviert. Ein dauerhafter Aufruf an einem Betreiber-PC allein ist kein verlässlicher Alarm: Vor Livebetrieb sind ein unabhängig betriebener Alarm-/Heartbeat-Kanal, zugriffsbeschränkte Fallbearbeitung, Eskalation, sichere Wiederholung und Tests für Ausfälle des Überwachungsdienstes nötig. Der Zugriff auf die Cloudflare-Alarmverwaltung scheiterte mit dem verfügbaren Wrangler-OAuth an HTTP 403.

Für einen isolierten D1-Funktionstest wurden am selben Tag zwei ausdrücklich synthetische, zufällig bezeichnete Versandversuche in der Monitoring-Testdatenbank angelegt. Die Statusfunktion zeigte die beiden Fälle auf zwei Seiten mit Seitengröße eins; danach wurden ausschließlich diese zwei gerade angelegten Testdatensätze wieder entfernt. Das prüft die D1-Abfrage und Cursorweitergabe, nicht einen echten Supportprozess oder Ereignisfluss.

Das lokale OAuth-Token ist kurzlebig: Am selben Tag wurde ein API-Aufruf zwischenzeitlich mit 401 abgewiesen; erst ein nachfolgendes `wrangler whoami` erneuerte die Sitzung und ermöglichte wieder Lesezugriffe. Der Testcheck versucht jetzt bei HTTP 401 genau einmal, die bestehende lokale Wrangler-Sitzung über das bereits installierte, versionsgebundene Wrangler 4.153.0 zu erneuern, liest danach dessen Token-Datei neu und beginnt die lesende Prüfung noch einmal. Er blendet sämtliche Wrangler-Ausgaben aus und startet keinen interaktiven Login. Erfordert sind Node, npm/npx und die vorhandene Wrangler-Version; mit `--no-install` wird kein Paket nachinstalliert. Bei Fehler, Timeout oder erneutem 401 meldet das Werkzeug Exitcode 2. Ein 403 wird nicht als Tokenablauf behandelt und nicht durch eine erneute Anmeldung umgangen.

Die Einmal-Wiederholung, die ausbleibende Wiederholung bei 403 und der Fehler bei einem zweiten 401 sind automatisiert getestet. Zusätzlich wurde mit einem synthetischen ungültigen Token ein tatsächlicher 401 erzeugt und der Wiederholungsweg über das vorhandene Wrangler bis zur erfolgreichen erneuten Testabfrage ausgeführt. Das beweist **nicht**, dass ein tatsächlich abgelaufener Refresh-Token automatisch erneuert werden kann. Für unbeaufsichtigten Dauerbetrieb reichen diese lokale Benutzeranmeldung und Prüfung weiterhin nicht: Es fehlen ein geeignet eingeschränkter Dienstzugang einschließlich Rotation und ein tatsächlich unabhängiger Alarmkanal. `wrangler login --scopes-list` bietet in der geprüften Wrangler-Version keinen ausdrücklich benannten Alert-/Notification-Scope.

Der Zugriffsfehler lässt sich mit dem vorhandenen lokalen Wrangler-Konto anhand des folgenden **lesenden** Tests nachvollziehen; es werden nur Endpunktname und HTTP-Status angezeigt. Der Tokenwert darf nicht ausgegeben, geteilt oder ins Repository geschrieben werden:

```powershell
@'
import os, tomllib, urllib.request, urllib.error
from pathlib import Path
config = Path(os.environ['APPDATA']) / 'xdg.config' / '.wrangler' / 'config' / 'default.toml'
with config.open('rb') as file:
    token = tomllib.load(file)['oauth_token']
base = 'https://api.cloudflare.com/client/v4/accounts/4b0fd8180d954c7db4b4a0bacbe2594b/alerting/v3/'
for path in ('policies', 'available_alerts'):
    request = urllib.request.Request(base + path, headers={'Authorization': 'Bearer ' + token})
    try:
        with urllib.request.urlopen(request, timeout=15) as response:
            print(path, 'HTTP', response.status)
    except urllib.error.HTTPError as error:
        print(path, 'HTTP', error.code)
'@ | python -
```

Am 15. November lieferten beide geprüften Endpunkte nach Token-Erneuerung `HTTP 403`. Falls stattdessen `401` erscheint, ist der lokale OAuth-Zugang abgelaufen; das allein bestätigt oder widerlegt die Alarmberechtigung nicht.

Der Ablauf und die Grenzen für die Fallbearbeitung stehen in `MAIL-SUPPORT.review.md`. Queue-Metriken und ihre Genauigkeit sind in der [Cloudflare-Dokumentation](https://developers.cloudflare.com/queues/observability/metrics/) beschrieben.

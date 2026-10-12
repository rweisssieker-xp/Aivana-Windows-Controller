<!-- generated-doc locale=de-DE source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Prüfung für kommerziellen Einmalverkauf

Stand: 12. Oktober 2026. Hersteller: Aivana.

## Ergebnis

Im geprüften Bestand wurde keine generelle Pflicht erkannt, den eigenen MAKRIVO-Quellcode offenzulegen. Ein proprietärer Einmalverkauf erscheint möglich, ist aber noch nicht vollständig vorbereitet. Insbesondere ist die Weitergabeerlaubnis für die vorhandene Corsair-SDK-DLL nicht belegt. Diese Prüfung ersetzt keine juristische Freigabe.

## Technische und lizenzbezogene Befunde

| Bestandteil | Befund | Erforderlicher Schritt |
| --- | --- | --- |
| Eigener Quellcode | Keine eigene LICENSE, COPYING oder NOTICE im untersuchten Git-Bestand und seiner lokal verfügbaren Lizenzdatei-Historie gefunden. | Rechteinhaberschaft einschließlich Beiträgen, übernommenen Codefragmenten und möglicher Arbeitgeberrechte bestätigen; eigene Nutzungsbedingungen erstellen. |
| .NET / WindowsDesktop | Das untersuchte eigenständige Paket unter `artifacts/makrivo-win-x64` enthält Version 10.0.6. Die lokalen Runtime-Pakete enthalten MIT-Lizenzen, die kommerzielle Weitergabe erlauben und die Beigabe ihrer Hinweise verlangen. In den untersuchten Ausgabeordnern wurden keine entsprechenden Lizenz-/Notice-Dateien gefunden. | Exakte Lizenztexte und zutreffende Drittanbieterhinweise der tatsächlich ausgelieferten Runtime in das Verkaufspaket aufnehmen. |
| NuGet-Abhängigkeiten | In den untersuchten Projektdateien keine direkten PackageReference-Einträge gefunden. Die untersuchte Desktop-Assets-Datei enthält eigene Projektabhängigkeiten. Runtime-Pakete bleiben dennoch lizenzrelevant. | Beim endgültigen Release alle Projekte, transitive Pakete und mitgelieferte Dateien erneut inventarisieren. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, Dateiversion 4.0.84.0. Das lokale SDK-Referenz-PDF enthält auf Druckseiten 35–38 die EULA: gebührenfreie Nutzung, aber Einschränkungen für Übertragung/Abtretung. Eine eindeutige Erlaubnis zur Weitergabe der DLL an Kunden wurde darin nicht festgestellt. | Vor dem Bündeln eine belastbare Weitergabeerlaubnis beschaffen. Alternativ ein Paket ohne SDK-DLL prüfen, das eine kundenseitig installierte SDK verwendet; auch dafür gelten die SDK-Bedingungen. |
| Unterschiedliche Pakete | `artifacts/makrivo-win-x64` enthält die Corsair-DLL nicht; `artifacts/win-x64` enthält sie. Die Anwendung sucht die SDK dynamisch an mehreren Orten. | Den tatsächlichen Verkaufsinstaller festlegen und dessen Inhalt prüfen. Die bloße Existenz eines Pakets ohne DLL bestätigt noch nicht alle Integrationsrechte. |
| OBS | Eigener WebSocket-Client im untersuchten C#-Code; kein OBS-Clientpaket, OBS-Binary oder übernommener GPL-Code identifiziert. | Die beobachtete Protokollintegration begründet für sich keine festgestellte GPL-Offenlegungspflicht. Herkunft eventuell übernommener Fragmente bleibt zu bestätigen. |
| App-Symbole | Ein lokales Python-Skript zeichnet geometrische Symbole mit Pillow. Das Werkzeug ist eine Build-Abhängigkeit. | Herkunft und Rechte aller tatsächlich ausgelieferten Bilder, Sounds, Schriften und sonstigen Assets dokumentieren. Das Skript allein beweist nicht die Rechte an jedem Asset. |
| Lizenzverwaltung | Keine vollständige Kauflizenz-/Aktivierungsprüfung im untersuchten Anwendungscode identifiziert. | Falls gewünscht, signierte Offline-Lizenzdateien implementieren. Technische Aktivierung und rechtliche Nutzungsbedingungen sind getrennte Aufgaben. |

## Sinnvolles Kaufmodell – noch nicht umgesetzt

- Einmalzahlung; dauerhaftes Nutzungsrecht an der erworbenen Hauptversion.
- Eine Person auf bis zu zwei eigenen PCs; berufliche Nutzung ausdrücklich erlauben.
- Fehlerkorrekturen und kleinere Updates einschließen; spätere Hauptversionen optional kostenpflichtig anbieten.
- Offline prüfbare, digital signierte Lizenzdatei; private Signierschlüssel niemals mit der App ausliefern.
- Gesetzliche Rechte und erforderliche Sicherheitsupdates dürfen durch die Hauptversionsregel nicht pauschal ausgeschlossen werden.

## Vor dem ersten Verkauf offen

1. Corsair-Weitergaberechte klären oder den Verkauf ohne gebündelte SDK-DLL vorbereiten.
2. Lizenz- und Drittanbietertexte der exakten Release-Abhängigkeiten beilegen und im Installer erhalten.
3. Rechteinhaberschaft am eigenen Code und an Assets bestätigen; EULA mit klarer Geräte-, Nutzer- und Upgraderegel erstellen.
4. Für Verbraucherverkäufe den Checkout einschließlich Informationspflichten und Widerruf behandeln. Ein pauschales „keine Erstattung bei Downloads“ genügt nicht. Das Erlöschen des Widerrufsrechts bei sofortiger Bereitstellung setzt die gesetzlichen Voraussetzungen voraus.
5. Den fertigen Installer einschließlich Plugins und aller transitiven Abhängigkeiten erneut prüfen. Die Arbeitskopie wird parallel erweitert; diese Bestandsaufnahme ist keine Freigabe zukünftiger Dateien.

## GitHub

Das untersuchte Repository ist öffentlich. Ohne Open-Source-Lizenz wird der Code nicht automatisch frei zur beliebigen Weiterverwendung. Die GitHub-Plattformrechte für öffentliche Repositories sind dennoch zu beachten. Eine spätere Umstellung auf privat entfernt keine bereits vorhandenen Kopien oder Forks. Sichtbarkeit und Lizenz wurden bei dieser Prüfung nicht geändert.

## Nachweise und Quellen

- Lokale .NET-Lizenzen: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` und `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Lokale .NET-Drittanbieterhinweise: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Lokales Corsair-SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; extrahiertes Referenz-PDF unter `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT-Lizenz](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK-Dokumentation mit EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Repositories lizenzieren](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Aktualisierungen](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Widerrufsrecht](https://www.gesetze-im-internet.de/bgb/__356.html)

Nicht geprüft: vollständige externe Beitragshistorie und Rechteketten, Markenfreigabe, vorhandener Verkaufscheckout, künftige Plugin-Pakete, vollständige juristische Wirksamkeit einer noch nicht vorliegenden EULA.

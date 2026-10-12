<!-- generated-doc locale=de-DE source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO-Steuererweiterungen

Der Benutzer forderte alle neun vorgeschlagenen Verbesserungen und ein erkennbares Taskleistensymbol. Bestehende Konfigurationen, insbesondere die Helligkeit G1/G2, müssen kompatibel bleiben. Vorhandene, nicht festgeschriebene Branding- und Dokumentationsänderungen gehören zum freigegebenen Arbeitsbereich und müssen beibehalten werden.

## Leistungen und Grenzen

1. Helligkeitsaktionen wählen alle unterstützten Monitore oder eine stabile spezifische Monitor-ID aus. Das Fehlen expliziter Ziele scheitert sichtbar. Das Dashboard zeigt die aktuelle Helligkeit, Ausgangslautstärke, den Mikrofonstatus und den OBS-Status durch begrenzte asynchrone Lesevorgänge an.
2. Sondertasten unterstützen sofortiges Drücken, Tippen, Halten und Doppeltippen mit Wiederholungsunterdrückung. Ein gehaltener Layer-Selektor ändert vorübergehend die aktive Zuordnung und wird durch die Freigabe wiederhergestellt. Vorhandene unmittelbare Bindungen behalten ihr Verhalten bei. Anhalten, Profiländerungen, Verbindungsabbrüche und Löschung ausstehender Gesten.
3. Der Workflow-Designer bearbeitet Bedingungen, Weiterleitungsverzweigungen und Stopp-/Fortsetzungsfehlerrichtlinien transaktional in einem Kachelentwurf. Bedingungen umfassen Dateien, laufende Prozesse und Fenstertitel. Keine unbegrenzten Sprungzyklen.
4. Die explizite Aufnahme erfasst Tastenkombinationen und Wartezeiten, ignoriert eingegebene Eingaben und stoppt bei Escape. Aufgezeichnete Maßnahmen werden vor der Annahme überprüft; Normaler Text wird nicht aufgezeichnet.
5. Gerätespezifische Standard-Sonderschlüssel anderer Hersteller verwenden Windows Raw Input. Proprietäre Schlüssel erfordern eine unterstützte Anbieter-API oder eine Anbieterzuordnung zu einem Standardschlüssel. Nicht unterstützte Funktionalität wird ehrlich dargestellt. Die Diagnose erläutert Profil, Pause, Bindungskonflikte und den tatsächlichen Adapterstatus.
6. Der Profilaustausch verwendet einen vom Benutzer ausgewählten freigegebenen Ordner (einschließlich Ordnern, die von einem vorhandenen Cloud-Client synchronisiert wurden). Revisionsprüfungen verhindern stille Überschreibungen. Pull zeigt Änderungen in der Vorschau an und importiert deaktivierte Aktionen mit entfernten Hardwarezuordnungen. Die Einbeziehung von Aktionsinhalten ist explizit. Von MAKRIVO werden keine Cloud-Anmeldeinformationen gespeichert.
7. UI-Strings gehören zu den vorhandenen 27-Sprachkatalogen; DE/EN/FR/IT bleiben die mindestens geprüften Dokumentationssprachen. Übersetzungslücken müssen offengelegt werden, statt Anspruch auf Vollständigkeit zu erheben.
8. Eine erkennbare blaugrüne M-Tastaturmarkierung ersetzt das allgemeine Taskleistensymbol Windows. Der Pausenmodus verfügt über ein gelbes Pausenzeichen. Beim Branding von ausführbaren Dateien/Fenstern wird die gleiche Marke verwendet. Der Tooltip enthält Status und Profil.

## Überprüfung

Kernprüfungen umfassen Abwärtskompatibilität, Snapshots, Abbruch, Gesten-Timing, Workflow-Verzweigungen/-Fehler, Rekorderfilterung und Synchronisierungskonflikte. Windows-Prüfungen umfassen die schreibgeschützte Anzeigeaufzählung, SDK-Release-Ereignisse und Raw Input-Analyse. WPF Smoke Checks decken neue Einstiegspunkte und Ressourcen ab, ohne die tatsächliche Monitorhelligkeit zu ändern, in eine andere Anwendung einzugeben oder Benutzerprofile zu überschreiben. Bei der Bereitstellung werden ein Backup und die G1/G2-Konfiguration beibehalten.

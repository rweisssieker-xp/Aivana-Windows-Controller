<!-- generated-doc locale=de-DE source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: sechs Produkterweiterungen und Tastaturadapter

Datum: 2026-10-12
Status: vorgeschlagenes Design zur Überprüfung durch den Benutzer; Die Umsetzung hat noch nicht begonnen.

## Ziel
Stellen Sie alle sechs angeforderten Erweiterungen in der vorhandenen Windows/WPF-Anwendung bereit und bewahren Sie dabei vorhandene Profile, G1/G2-Zuweisungen, den lokalen Betrieb und die kontofreie Nutzung auf. Erweitern Sie die Tastaturintegration auf Razer und Logitech mit einer wiederverwendbaren, funktionsbasierten Architektur für andere Hersteller. Ein Herstellername oder ein Tastaturlayout sind niemals ein Beweis dafür, dass proprietäre Steuerelemente unterstützt werden.

## Architekturentscheidung
Erweitern Sie Aivana.Core mit serialisierbaren Verträgen und Validierung, Aivana.Windows mit Windows und Anbieteradaptern und Aivana.Desktop mit Transaktionseditoren und Statusansichten. Verwenden Sie separate Begleitprojekte für die isolierte Plugin-Ausführung und den lokalen mobilen Panel-Dienst. Vermeiden Sie ein Umschreiben oder einen einzelnen privilegierten Prozess, der jede Erweiterung hostet. Vorhandene Konfiguration muss unverändert geladen werden; Fügen Sie Aktionsarten hinzu, anstatt ihre beibehaltenen Enum-Werte neu anzuordnen.

In Betracht gezogene Alternativen: Eine monolithische Implementierung würde die Hardware-, Netzwerk- und Workflow-Ausführung verwickeln; Das Ersetzen von WPF würde zusätzliche Migrationsarbeit verursachen, die damit nichts zu tun hat. Modulare Erweiterungen werden empfohlen.

## 1. Unabhängiges Tastatur-Makropad
Speichern Sie Bindungen nach Geräteidentität sowie physischem Scancode und Flags für erweiterte Schlüssel. Fügen Sie explizite Beobachtungs- und exklusive Modi, Geräteerkennung, Schlüsselerfassung, Wiederholungsunterdrückung, Gesten, gehaltene Ebenen und Diagnose für erneute Verbindung hinzu. Haupt-/Nummernblock beibehalten. Unterscheidungen eingeben und deutsche/amerikanische Layouts testen. Bindungen zielen auf eine ausgewählte physische Tastatur ab; Mehrdeutige Wiederverbindungsidentitäten erfordern eine erneute Auswahl, anstatt ein anderes Gerät zu steuern.

Raw Input bietet nur Beobachtung. Die ausschließliche Unterdrückung pro Gerät erfordert ein tatsächliches Tastaturfilter-Backend. Korrelieren Sie globale Low-Level-Hooks niemals mit Raw Input und beanspruchen Sie eine zuverlässige gerätespezifische Unterdrückung. Stellen Sie einen Watchdog, eine Notfallfreigabe, ein Fail-Open-Verhalten und ein Installations-/Deinstallationsprogramm bereit. Erfassen Sie keine sicheren Desktop-Eingaben. Die Treiberinstallation und die Aktivierung im exklusiven Modus sind explizite separate Benutzeraktionen.

Driver Gate: Wählen Sie ein unterstütztes, ordnungsgemäß lizenziertes und signiertes Backend aus, das mit den Zielkonfigurationen Windows 11 kompatibel ist, oder entwickeln/signieren Sie einen dedizierten Filter. Interception ist ein zu evaluierender Kandidat und keine feste Produktionsabhängigkeit: Die offizielle README-Datei beschreibt Tests durch Windows 10 und eindeutige kommerzielle Lizenzierung. Bis dieses Tor erreicht ist, muss der exklusive Modus als nicht verfügbar angezeigt werden; Die Funktion kann nicht als abgeschlossen erklärt werden.

Akzeptanz: Bei zwei physischen Tastaturen führt dieselbe Taste ein Makro aus und erzeugt keine normale Anwendungseingabe nur auf dem ausgewählten exklusiven Gerät. andere Tastatureingaben bleiben davon unberührt. Überprüfen Sie die Trennung, den Absturz, den Abbruch, das Anhalten/Fortsetzen und das Entfernen des Treibers auf der tatsächlichen Hardware.

## 2. Geführte Einrichtung des Arbeitsbereichs
Fügen Sie einen Assistenten für Arbeitsplatz, Besprechung, Streaming, Recherche und Projektwechsel hinzu. Entdecken Sie installierte Anwendungen, Audiogeräte, Monitore und OBS Szenen. Benutzer wählen Ziele und Bindungen; Zeigen Sie eine Vorschau der resultierenden Schritte an und validieren Sie fehlende Anforderungen. Speichern Sie nur nach Überprüfung, ohne dass während der Einrichtung Aktionen ausgeführt werden. Generierte Profile bleiben in vorhandenen Editoren bearbeitbar.

Akzeptanz: Erstellen Sie ein Arbeitsprofil, ohne ausführbare Pfade oder Geräte-IDs manuell zu bearbeiten. Beim Abbruch bleiben die Einstellungen unverändert. Für fehlende Software gibt es eine umsetzbare Erklärung.

## 3. Verifizierte Ausführung
Führen Sie Preflight-Prüfungen, eingegebene Ergebnisse pro Schritt und optionale Nachbedingungen ein. Unterscheiden Sie zwischen den Status „Angefordert“, „Abgeschlossen“, „Verifiziert“, „Fehlgeschlagen“, „Zeitüberschreitung“, „Abgebrochen“ und „Nicht überprüfbar“. Überprüfen Sie Audio/Mikrofon, Helligkeit, Prozess-/Fensterbereitschaft und OBS-Status mit begrenzten asynchronen Lesevorgängen. Die Übermittlung einer Verknüpfung/Text ist kein Beweis dafür, dass die Zielanwendung den Befehl akzeptiert hat. Äußere Nebenwirkungen sind nicht allgemein reversibel; Bieten Sie eine Entschädigung nur an, wenn sie umgesetzt wird.

Akzeptanz: Nicht verbundene explizite Ziele fallen niemals stillschweigend zurück; Ein falsch beobachteter Zustand meldet niemals einen verifizierten Erfolg. Abbruch stoppt die Abfrage; Fehler identifizieren den Schritt und die Wiederherstellungsaktion. Der Ausführungsverlauf lässt Textnutzlasten und Geheimnisse aus.

## 4. Plugins und gemeinsam nutzbare Pakete
Versionierte JSON-Manifeste deklarieren Aktionen, Parameterschemata, Statusanbieter, Abhängigkeiten und Funktionen. Führen Sie ausführbare Plugins in einem separaten Prozess über einen begrenzten lokalen IPC mit Handshake, Fristen und Stornierung aus. Lehnen Sie fehlerhafte Nachrichten und inkompatible Versionen ab; Handle stürzt ab, ohne dass der Controller beendet wird. Bei Fähigkeitserklärungen handelt es sich um Offenlegung und Vermittlung, nicht um einen Anspruch auf OS-Sandboxing.

Bieten Sie eine Paketexport-/-importvorschau, Inhalts-Hashes, Path-Traversal-Schutz, Größenbeschränkungen, Versions-/Abhängigkeitsprüfungen, explizite Aktivierung und Upgrade-Rollback. Importierte ausführbare Plugins dürfen nicht automatisch ausgeführt werden. Fügen Sie ein funktionierendes Beispiel-Plugin mit einem End-to-End-Installations-/Aktions-/Status-/Deinstallationsablauf hinzu. Für diese Version ist kein öffentlicher Marktplatzdienst erforderlich.

Akzeptanz: Erstellen, Exportieren, Installieren und Ausführen einer echten Beispielintegration; Demonstrieren Sie Timeout, Absturzisolation, Abhängigkeitsfehler, Traversal-Ablehnung und deaktivierten Import.

## 5. Windows UI-Automatisierung
Fügen Sie eine Elementauswahl und begrenzte Aktionen für Aufruf, Wert, Auswahl, Umschalten, Fokussieren und Warten auf ein Element/einen Status hinzu. Behalten Sie Selektoren mithilfe von Prozess, Automatisierungs-ID, Steuerelementtyp und Vorgängerkontext bei. Vermeiden Sie die reine Koordinatenaufzeichnung. Anzahl der Selektorübereinstimmungen anzeigen und Selektoren schreibgeschützt testen. Passwortfelder sind von der Aufzeichnung ausgeschlossen. Behandeln Sie nicht unterstützte Steuerelemente und Windows-Integritätsstufeneinschränkungen explizit.

Erweitern Sie die Aufzeichnung mit überprüfbaren UIA-Aktionen und behalten Sie die aktuelle Verknüpfungsaufzeichnung bei. Mehrdeutige Selektoren schlagen fehl, anstatt auf ein beliebiges Steuerelement zu klicken. Ein normaler Browser-Barrierefreiheitsbaum ist kein Versprechen einer vollständigen DOM-Automatisierungs-Engine.

Akzeptanz: Automatisieren Sie eine dedizierte Testanwendung mit stabilen Selektoren über Positions-/Größenänderungen hinweg; Testmehrdeutigkeit, fehlende Elemente, deaktivierte Steuerelemente, Zeitüberschreitungen und Abbruch.

## 6. Lokales Telefon-/Tablet-Panel
Stellen Sie ein reaktionsfähiges Panel über einen optionalen LAN-Dienst bereit. Koppeln Sie explizit mit einem kurzlebigen Einmalcode und geben Sie widerrufliche Anmeldeinformationen aus, die lokal geschützt sind. Verwenden Sie HTTPS mit dokumentierter Vertrauensstellung/Anheftung lokaler Zertifikate. Kein Fallback beim Senden von Anmeldeinformationen im Klartext. Standardmäßig deaktiviert und beschränkt konfigurierte Schnittstellen. Ursprungsvalidierung, authentifizierte Befehle, Ratenbegrenzungen und begrenzte Nachrichten sind obligatorisch.

Erlaubte Profile/Aktionen, Live-Status, Fortschritt und Abbruch anzeigen; Verwenden Sie stabile IDs und dieselbe Ausführungswarteschlange wie Desktop- und physische Schlüssel. Geben Sie keine willkürlichen Befehle, ausführbaren Pfade, Geheimnisse oder Plugin-Installationen preis. Stellen Sie Verbindungs-, Kopplungs- und Widerrufsbildschirme bereit. Es ist kein Cloud-Konto oder Internet-Relay enthalten.

Akzeptanz: Betrieb über ein echtes Telefon/Tablet im selben LAN; Ungepaarte Clients können keine Aktionen auflisten oder ausführen. widerrufene Token schlagen fehl; Duplizierte/wiedergegebene Befehle werden nicht doppelt ausgeführt. Bei „Disconnect“ werden Aktionen nicht stillschweigend wiederholt.

## Geltungsbereich des Tastaturherstellers

| Adapter/Familie | Geplante Integration | Explizite Grenze |
| --- | --- | --- |
| Corsair | Behalten Sie die aktuelle G-Tasten-Eingabe iCUE und die RGB-Integration bei | Die Erkennung von Modell-/Softwarefähigkeiten ist weiterhin erforderlich |
| Logitech G | G-Key-SDK-Adapter mit Prüfung auf Drücken/Freigeben und Laufzeitverfügbarkeit; Beleuchtung über ein separates SDK, sofern unterstützt | Setzen Sie die G HUB-Erkennung nicht bei jedem Modell mit der unterstützten G-Key-API gleich |
| Razer | Chroma-Beleuchtungsadapter und geführte Synapse-zu-Standard-Tastenzuordnung sowie generische HID-Eingabe | Chroma ist eine Beleuchtungs-API; Kennzeichnen Sie es nicht als native proprietäre Makrotasteneingabe |
| SteelSeries | GameSense Zustands-/Beleuchtungsadapter plus generischer HID-Eingang | Bei der GameSense-Ereignisübermittlung handelt es sich nicht um eine allgemeine proprietäre Schlüsselerfassungs-API |
| Wooting | Analoger SDK-Adapter für unterstützten analogen Eingang sowie normale HID-Tasten | Funktionen und SDK-Verteilungsbedingungen müssen für jedes unterstützte Modell überprüft werden |
| Keychron/QMK/VIA | Standard-HID-Schlüssel und Anweisungen zur Firmware-Zuordnung; bekannte Anlagen nur mit Belegen | Die VIA-Konfigurationsunterstützung ist keine universelle Laufzeit-Makroschlüssel-API |
| Andere Standard-HID-Tastaturen | Erkennung und Zuordnung physischer Schlüssel pro Gerät; Exklusivmodus nur mit dem validierten Filter-Backend | Keine modellspezifische Fn-, Wähl- oder proprietäre Tastenunterstützung |

Für jedes Gerät wird je nach Bedarf der Status „Erkannt“, „Beobachtet“, „Zugeordnet durch Herstellersoftware“, „natives SDK“, „Treiber exklusiv“, „Nicht verfügbar“ oder „Hardware nicht getestet“ angezeigt. Bewahren Sie Fähigkeitsnachweise mit Adapterversionen und getesteten Modell-/Firmware-IDs.

## Lieferauftrag
1. Konfigurationskompatibilität, Fähigkeitsverträge und überprüfte Ausführung.
2. Geführte Einrichtung mit echter Erkennung und vorhandenen Aktionsausführern.
3. UI-Automatisierung und ihr Rekorder/Testgerät.
4. Plugin-Host, Paketfluss und Beispielintegration.
5. Gepaartes LAN-Panel und mobile Überprüfung.
6. Erweiterte Herstelleradapter und exklusive Tastatur-Backend-Integration mit tatsächlicher Hardwarevalidierung.

Alle sechs bleiben erforderlich. Treibersignierung/-lizenzierung, nicht zugängliche SDKs oder fehlende Testhardware sind Release-Gates und kein Grund, Scheinverhalten zu ersetzen und die Arbeit als abgeschlossen zu bezeichnen.

## Verifizierungs- und Freigabekriterien
Führen Sie vorhandene Kern-, Windows- und WPF-Prüfungen aus und fügen Sie gezielte Integrationstests für jede neue Grenze hinzu. Überprüfen Sie JSON-Roundtrips und Legacy-Profile. Testen Sie importierte ausführbare Inhalte, Netzwerkauthentifizierung, Abbruch, Plugin-Abstürze und erneute Geräteverbindung. Überprüfen Sie Desktop- und Mobillayouts. Validieren Sie die hohe Windows-Skalierung separat. Erstellen Sie Release-Artefakte erst, nachdem Sie die Prüfungen bestanden haben. Unterscheiden Sie in Versionshinweisen zwischen implementierter/getesteter, implementierter/Hardware-ungetesteter und extern blockierter Funktionalität. Ersetzen Sie die installierte Anwendung nicht, installieren Sie keine Treiber, ändern Sie keine echten Tastaturbelegungen und öffnen Sie den Netzwerkzugriff nicht als zufälligen Test.

## Beweise
- Microsoft Raw Input Semantik: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G-Entwickler-SDKs: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK-FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Abfang-Repository und Abhängigkeitsbedingungen: https://github.com/oblitum/Interception
- Vorhandener Repository-Herstellerbestand: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Überprüfen Sie Entscheidungen
Der Benutzer wird gebeten, diesen schriftlichen Entwurf vor dem Implementierungsplan zu genehmigen. Die wichtigste externe Abhängigkeit ist ein produktionsbereites Treiber-Backend mit exklusiver Eingabe. Der empfohlene Bereich behält dies als geschlossenes Ergebnis bei, anstatt es stillschweigend auf die Beobachtung Raw Input zu reduzieren.

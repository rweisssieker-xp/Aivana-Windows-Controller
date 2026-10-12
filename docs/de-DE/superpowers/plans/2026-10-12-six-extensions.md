<!-- generated-doc locale=de-DE source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO-Erweiterungsimplementierungsplan

> **Für Agentenarbeiter:** ERFORDERLICHE UNTERKUNFT: Verwenden Sie Superpowers:Executing-Plans für die native Ausführung oder Superpowers:Subagent-Driven-Development, nachdem der Benutzer die Delegation ausgewählt hat. Verfolgen Sie Schritte mit Kontrollkästchen.

**Goal:** Bereitstellung von sechs integrierten Erweiterungen und funktionsbasierter Tastaturunterstützung mehrerer Hersteller.

**Architecture:** Bewahren Sie die Trennung des Kerns / Windows / WPF auf. Einführung isolierter Plugin- und mobiler Serviceprozesse mit eng authentifiziertem IPC; Die gesamte Ausführung erfolgt über den vorhandenen Controller und die MacroRunner-Warteschlange. Hardware-Adapter machen erkannte Funktionen bekannt und nicht abgeleitete markenweite Unterstützung.

**Tech Stack: ** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, dokumentierte Anbieter-SDKs, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (vom Benutzer genehmigt).

## Globale Einschränkungen

- Vorhandene Konfiguration muss unverändert geladen werden; Fügen Sie Aktionsarten hinzu, anstatt ihre beibehaltenen Enum-Werte neu anzuordnen.
- Bestehende Profile, G1/G2-Zuweisungen, lokale Bedienung und kontofreie Nutzung beibehalten.
- Raw Input bietet nur Beobachtung. Die ausschließliche Unterdrückung pro Gerät erfordert ein tatsächliches Tastaturfilter-Backend.
- Importierte ausführbare Plugins dürfen nicht automatisch ausgeführt werden.
- Standardmäßig deaktiviert und eingeschränkt konfigurierte Schnittstellen für den mobilen Dienst; Kein Klartext-Authentifizierungs-Fallback.
- Die Übermittlung einer Verknüpfung/Text ist kein Beweis dafür, dass die Zielanwendung den Befehl akzeptiert hat.
- Ersetzen Sie die installierte Anwendung nicht, installieren Sie keine Treiber, ändern Sie keine echten Tastaturbelegungen und öffnen Sie den Netzwerkzugriff nicht als zufälligen Test.
- Behalten Sie nicht zusammenhängende, nicht festgeschriebene Änderungen bei. Übernehmen Sie im Rahmen dieser Funktion keine vorhandenen freigegebenen Änderungen. Stellen Sie nur unabhängig überprüfbare Feature-Hunks bereit, wenn Commits angefordert werden.

## Rezensionsfokus

1. Identische Tastaturmodelle duplizieren: Eine mehrdeutige erneute Verbindung muss eine erneute Auswahl erfordern (Aufgaben 10–12).
2. Abbruch während UIA-/Anbieter-/Plugin-/Netzwerkwartezeiten: keine verzögerte Aktion nach Abbruch (Aufgaben 1, 4, 6, 8).
3. Benutzer bearbeitet, während der Assistent oder die Paketvorschau geöffnet ist: Revisionskonflikte müssen ein stilles Überschreiben verhindern (Aufgaben 3, 7).
4. Sprache und physisches Layout unterscheiden sich: Selektoren und Scancode-Bindungen dürfen nicht von lokalisierten Anzeigenamen abhängen (Aufgaben 4, 10).
5. Netzwerkwiederholung und Plugin-Neustart: Befehle dürfen nicht zweimal stillschweigend ausgeführt werden (Aufgaben 6, 8).

## Gemeinsame Verträge und Abhängigkeitsreihenfolge

Die Aufgaben 1–3 etablieren die Ausführung und Einrichtung. Aufgaben 4–5 erstellen UIA. Aufgaben 6–7 liefern Plugins. Aufgaben 8–9 liefern mobilen Zugriff. Aufgaben 10–12 liefern Tastaturadapter und Filterintegration. Die Aufgabe 13 validiert das komplette Produkt.

Jede Aufgabe folgt einem Zyklus „Nicht bestandener Test → minimale Implementierung → bestandener Test“. Eine Aufgabe ist erst dann abgeschlossen, wenn sie über die Anwendung erreichbar ist und ihre Prüfungen erfolgreich sind. Gerüste, Scheinadapter und Dokumentation allein reichen nicht aus, um ein Ergebnis zu erzielen.

### Aufgabe 1: Typisierte Überprüfung und kompatible Persistenz

**Files:** Erstellen `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; Ändern Sie `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaces:** Produzieren `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` und `VerifyAsync(ActionStep, CancellationToken)`, beide geben `Task<ActionVerificationResult>` zurück. Fügen Sie optionales serialisierbares `VerificationOptions` mit aktiviertem Flag und Timeout (100–60000 ms) hinzu, standardmäßig deaktiviert. Veröffentlichen Sie typisierte Ergebnisse pro Schritt, einschließlich Lauf-ID, Kachel-ID und Schrittindex, ohne Aktionsnutzlasten.

- [ ] Schreibprüfungen: Legacy-JSON behält vorhandene numerische Aktionswerte bei; fehlende Optionen deaktivieren die Überprüfung; ungültiges Timeout schlägt bei der Validierung fehl; Ein Preflight-Fehler verhindert den Aufruf des Executors. Eine fehlgeschlagene Nachbedingung führt nie zu einem verifizierten Erfolg. nicht unterstützter Executor ist nicht überprüfbar; Bei einer Stornierung werden die Überprüfung und die nachfolgenden Schritte gestoppt.
- [ ] Führen Sie `dotnet run --project tests/Aivana.Tests -c Release` aus; Bestätigen Sie, dass neue Prüfungen vor der Implementierung fehlschlagen.
- [ ] Implementieren Sie Verträge, Validierung, Klonen/Snapshot-Aufbewahrung und MacroRunner-Integration. Beachten Sie die Stopp-/Fortsetzungsrichtlinien von OnError. Bestehende Abonnenten für Ausführungsfeedback erhalten.
- [ ] Kerntests ausführen; erfordern, dass alle Schecks bestanden werden.

### Aufgabe 2: Windows-Verifizierungsanbieter und Ergebnis-Benutzeroberfläche

**Dateien:** Erstellen `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; Ändern Sie `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs` und testen Sie die Einstiegspunkte.

**Interfaces:** WindowsActionVerification implementiert den Aufgabenanbieter 1 unter Verwendung vorhandener Audio-, Anzeige-, Prozess-/Fenster- und OBS-Clients. Verfügbarkeit und Identifikatoren von Preflight-Tests; Bei der Verifizierung wird der gewünschte Zustand nach der begrenzten Abfrage verglichen. Erstellen Sie Controller-Ergebnis-Snapshots, die von der Verlaufsansicht WPF verwendet werden.

- [ ] Fügen Sie fehlgeschlagene Tests für nicht verfügbare explizite Audio-/Anzeige-IDs, Zeitüberschreitung OBS, nicht übereinstimmenden beobachteten Status, unbekannte Verknüpfungseffekte, Auslassung sensibler Nutzdaten und abgebrochene Abfragen hinzu, indem Sie injizierbare Leseanbieter verwenden.
- [ ] Implementieren Sie eine echte lesegestützte Überprüfung für absolute Lautstärke, Stummschaltung, absolute Helligkeit, Prozess-/Fensterbereitschaft und unterstützte OBS-Befehle. Relative/Toggle-Aktionen erfordern einen erfassten Vorzustand; Andernfalls melden Sie „Nicht überprüfbar“.
- [ ] Fügen Sie Überprüfungskontrollen pro Schritt und einen sicheren Ausführungsverlauf hinzu, der den Status, die fehlgeschlagene Anforderung und den Wiederherstellungstext anzeigt.
- [ ] Führen Sie core/Windows-Prüfungen und WPF Smoke mit injizierten Statusanbietern durch. Keine wirkliche Helligkeit oder Audio-Schreibvorgänge.

### Aufgabe 3: Geführte Arbeitsbereichserstellung

**Files:** Erstellen `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Ändern Sie Paletten- und Dashboard-Einstiegspunkte.

**Interfaces:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; Die Auswahl enthält die Vorlagen-ID, ausgewählte ausführbare Pfade, URLs, Audio-/Anzeige-IDs, die Szene OBS und ausgewählte Bindungen. Discovery gibt benannte Kandidaten mit stabilen IDs und Verfügbarkeit zurück. Überarbeitung der Entwurfsdatensatzkonfiguration zur Konflikterkennung.

- [ ] Fehlschlagende Tests für alle fünf angeforderten Assistentenfamilien, unabhängig generierte Schritte, fehlende erforderliche Ziele hinzufügen, ohne Mutation abbrechen und gegen geänderte Revision speichern.
- [ ] Entdecken Sie Anwendungen über registrierte App-Pfade und Startmenü-Verknüpfungen; Geräte über bestehende Anbieter; Szenen durch schreibgeschützte OBS-Abfrage. Halten Sie die manuelle Auswahl für unentdeckte Apps verfügbar.
- [ ] Implementieren: Workflow auswählen → Ziele auswählen → Steuerelemente auswählen → Vorschau/Validierung → Speichern. Generieren Sie bearbeitbare Profile und führen Sie sie niemals während der Einrichtung aus.
- [ ] Aktuelle WPF-Rauchprüfungen des Assistenten für Abschluss/Abbruch/Konflikt hinzufügen; Führen Sie alle Tests durch.

### Aufgabe 4: UIA-Selektoren und begrenzte Aktionen

**Files:** Erstellen `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modifizieren von Modellen, Konfigurationsvalidierung und WinActionExecutor.

**Interfaces:** Anhängen `ActionKind.UiAutomation`; `UiElementSelector` speichert Prozess, Automatisierungs-ID, Kontrolltyp und geordnete Vorfahrenkriterien. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` gibt `Task` zurück; Operationen Invoke, SetValue, Select, Toggle, Focus und WaitState. Die Selektorauflösung muss genau ein geeignetes Element zurückgeben.

- [ ] Fügen Sie fehlgeschlagene Tests in einem speziellen Testfenster für Verschiebung/Größenänderung, doppelte Übereinstimmungen, fehlende Elemente, nicht unterstützte Muster, deaktivierte Steuerelemente, Passwortwerte, Unicode-Beschriftungen und Zeitüberschreitung/Abbruch hinzu.
- [ ] Implementieren Sie UIA auf einem dedizierten Worker mit begrenzten Anforderungen und Prozess-Targeting. Potenziell hängende Anbieter laufen hinter einer neustartbaren Worker-Grenze; Das Abbrechen einer Anfrage darf keine Nebenwirkungen in der Warteschlange hinterlassen.
- [ ] Passwortwerte ausschließen; Melden Sie Zugriffs-/Integritätsbeschränkungen, anstatt sie zu umgehen.
- [ ] Führen Sie Windows-Prüfungen mit isolierten Vorrichtungen durch. Hängen Sie nur Enum-Werte an und führen Sie ältere Persistenzprüfungen erneut aus.

### Aufgabe 5: UIA-Auswahl und überprüfte Aufzeichnung

**Files:** Erstellen `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; Aktionskatalog, Editor- und Rekorder-Integration ändern; Erstellen Sie UIA WPF-Rauchkontrollen.

**Interfaces:** Picker gibt einen `UiElementSelector` zurück; Der Rekorder gibt vorgeschlagene UIA-Schritte zurück und schreibt nie direkt in ein gespeichertes Profil. Der Befehl „Test-Selektor“ gibt die Anzahl der Übereinstimmungen ohne Aufruf zurück.

- [ ] Fehlgeschlagene Prüfungen hinzufügen: Auswahl schließt MAKRIVO und Kennwortwerterfassung aus; Aufnehmen/Abbrechen lässt das bestehende Profil unverändert; mehrdeutige Übereinstimmungsanzahl der Vorschauberichte; Durch das Akzeptieren der aufgezeichneten Schritte wird ein bearbeitbarer Entwurf erstellt.
- [ ] Hervorhebung/Auswahl von Implementierungselementen, betriebsspezifische Parameter, Auswahlvorschau und Rekorder-Überprüfungsfenster. Behalten Sie das bestehende Verhalten des Verknüpfungsrekorders bei.
- [ ] Katalogeinträge und Palettenzugriff hinzufügen; Überprüfen Sie Editorfehler vor dem Speichern.
- [ ] Führen Sie die Rauchprüfungen Windows und WPF am Gerät durch; Überprüfen Sie die Screenshots des Auswahl-/Editors.

### Aufgabe 6: Isolierter Plugin-Host und Protokoll

**Dateien:** Erstellen `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Ändern Sie den Lösungs- und Aktionsversand.

**Interfaces:** Version-1-Manifest gibt Paket-ID/-Version, Einstiegspunkt, Parameterschemata, Aktionen, Statusanbieter und Fähigkeitsdeklarationen an. IPC-Nachrichten enthalten die Protokollversion, die eindeutige Anforderungs-ID, die Methode, JSON-Parameter und das typisierte Ergebnis. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` gibt ein typisiertes Ergebnis zurück. Hängen Sie `ActionKind.Plugin` an.

- [ ] Fehlschlagende Tests für inkompatibles Protokoll, fehlerhafte/übergroße (>1 MiB) Nachricht, doppelte Anforderungs-ID, Abbruch, Absturz, nicht reagierender Prozess und fehlende Aktion hinzufügen.
- [ ] Implementieren Sie einen Named-Pipe-IPC für den gleichen Benutzer mit einem 10-Sekunden-Handshake und einem konfigurierbaren Ausführungszeitlimit, das auf 60 Sekunden begrenzt ist. Nicht reagierenden Arbeitnehmer kündigen; Eine Aktion während des Flugs nach dem Neustart nicht automatisch wiedergeben.
- [ ] Erzwingen Sie Schemata und zulässige Controller-Dienste. Erläutern Sie die Vertrauenswürdigkeit von ausführbaren Plugins. Prozessisolation ist keine Betriebssystem-Sandbox.
- [ ] Führen Sie Kerntests und Integrationstests mit echten Test-Plugin-Prozessen durch.

### Aufgabe 7: Plugin-Pakete und Beispielintegration

**Files:** Erstellen Sie `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, Pakettests; Paletten-/Einstellungseinträge ändern.

**Interfaces:** Der Paketdienst stellt `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` und `UninstallAsync(packageId)` bereit. Überprüfte Paketaufzeichnungen, Hashes, Abhängigkeiten, Version und deklarierte Funktionen.

- [ ] Fehlschlagende Tests für Zip-Traversal, absolute Pfade, Symlink/Reparse-Escape, doppelte Einträge, >100 MiB erweiterte Daten, Hash-Nichtübereinstimmung, Downgrade, veraltete Überprüfung und deaktivierte Importe hinzugefügt.
- [ ] Implementieren Sie Vorschau und explizite Aktivierung, Abhängigkeitsfehler, gestaffelte Installation und Upgrade-Rollback; Entladen Sie den Arbeiter vor dem Umzug.
- [ ] Stellen Sie ein echtes Beispiel-Plugin bereit, das eine konfigurierbare Countdown-Aktion und einen Live-Status über IPC zurückgibt, einschließlich Stornierung; Paketieren/Exportieren/Installieren/Deinstallieren über die Benutzeroberfläche.
- [ ] Führen Sie Paket-/Hosttests und WPF End-to-End-Prüfungen durch. Stellen Sie vor der Aktivierung sicher, dass keine importierten ausführbaren Dateien ausgeführt werden.

### Aufgabe 8: HTTPS-Mobilfunkdienst und -Kopplung

**Files:** Erstellen Sie `src/Aivana.MobileHost/` ASP.NET Core-Projekt, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Integrationstests; Lösung und Controller ändern.

**Interfaces:** Der mobile Dienst kommuniziert mit dem Controller über den IPC desselben Benutzers. Routen: Kopplung, zulässige Profile/Aktionen, authentifizierter Aktionsaufruf, Abbruch und WebSocket-Status. Der Aufruf umfasst eine eindeutige Befehls-ID. Der Controller verfügt über eine begrenzte Deduplizierung. Anmeldeinformationen sind widerrufbar und werden über den Windows-Schutz gespeichert.

- [ ] Fügen Sie fehlgeschlagene Tests für ungepaarte Aufzählung/Ausführung, abgelaufenen Code, wiederverwendeten Code, widerrufenes Token, ungültigen Ursprung, doppelten Befehl, übergroßen Frame und Überflutungsgrenzen hinzu.
- [ ] Implementieren Sie einen Nur-HTTPS-Listener, vom Benutzer ausgewählte Schnittstellen, lokale Zertifikatbereitstellungs-/Vertrauensanweisungen, einmalige 6-stellige Paarungscodes, die nach 120 Sekunden ablaufen, maximal fünf Versuche pro Code und Token-Sperrung. Speichern Sie gehashte Zufallstoken; Protokollieren Sie niemals Anmeldeinformationen.
- [ ] Implementieren Sie zugelassene stabile Aktions-IDs und vorhandenes Warteschlangenrouting. Verweigern Sie beliebige Shell-, Pfad- und Paketinstallationsbefehle. Konfigurieren Sie explizite Ratenbegrenzungen pro Client (10 Befehle/Sek., 30 Burst) und 64 KiB-Nachrichtenbegrenzung.
- [ ] Überprüfen Sie Loopback-Integrationstests, ohne Firewall-Regeln hinzuzufügen oder den LAN-Zugriff zu aktivieren; Testen Sie das Herunterfahren und die Token-Persistenz.

### Aufgabe 9: Mobile Panel- und Desktop-Dienststeuerungen

**Files:** Erstellen Sie das responsive Panel `src/Aivana.MobileHost/wwwroot/` und `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Fügen Sie Browser-/mobile Verifizierungsskripte und WPF-Rauchabdeckung hinzu.

**Interfaces:** Nur Routen der Aufgabe 8 konsumieren. Stellen Sie das Touch-Aktionsraster, die Profilauswahl, Live-Werte/Fortschritt, Abbrechen, Verbindungsstatus und Kopplung bereit. Desktop steuert Dienststart/-stopp, Adress-/Zertifikatanweisungen, zulässige Profile und den Widerruf gepaarter Clients.

- [ ] Fügen Sie fehlgeschlagene Integrationsprüfungen für Authentifizierungsfluss, veraltete/fehlende Aktions-IDs, erneute Verbindung ohne Aktionswiedergabe, widerrufenen Client und Dienststopp hinzu.
- [ ] Implementieren Sie eine barrierefreie, responsive Benutzeroberfläche mithilfe lokaler Assets, ohne Skripte von Drittanbietern oder Internetabhängigkeiten. Token erscheinen niemals in URLs.
- [ ] Testen Sie das Browser-Layout bei 360/768/1280 CSS px; Sehen Sie sich aktuelle Screenshots an. Überprüfen Sie dies nur mit einem echten Telefon/Tablet, nachdem der Benutzer den LAN-Dienst explizit aktiviert hat.
- [ ] Dokumentieren und melden Sie die Überprüfung der mobilen Hardware getrennt von der Browseremulation.

### Aufgabe 10: Gerätefunktionen und Normalschlüsselbeobachtung

**Files:** Erstellen `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; Ändern Sie RawKeyboardBindingService, KeyboardBinding, Validator und Gerätediagnose. Tests hinzufügen.

**Interfaces:** `IKeyboardAdapter` stellt Funktions-Snapshots, stabile Geräteidentität und physische Schlüsselkanten (Code scannen, erweiterte Flags, Drücken/Freigeben) bereit. Die Modi unterscheiden zwischen Beobachtung, zugeordneter Standardeingabe, nativem SDK und Treiber-exklusiv. Vorhandene spezielle Bindungen für virtuelle Schlüssel behalten ihr Darstellungs- und Migrationsverhalten bei.

- [ ] Fehlschlagende Tests für alte Bindungen hinzufügen, Unterscheidungen eingeben, Sprach-/Layout-Unabhängigkeit, wiederholtes Herunterfahren, Trennen im gehaltenen Zustand, Mehrdeutigkeit bei Duplikatmodellen und Ablehnung injizierter Eingaben.
- [ ] Erweitern Sie die Beobachtung auf gewöhnliche physische Schlüssel, ohne sie zu unterdrücken. Fügen Sie die explizite Erfassung und Wiederverbindung der Identitätsbehandlung hinzu.
- [ ] Zeigt die tatsächlichen Adapterfunktionen und den Hardware-ungetesteten Status in der Benutzeroberfläche des Geräts an; Schließen Sie niemals die Fn/Dial-Unterstützung von Markennamen ab.
- [ ] Führen Sie core/Windows/WPF-Prüfungen mit simulierten Berichten und schreibgeschützter Geräteaufzählung aus.

### Aufgabe 11: Herstelleradapter

**Files:** Erstellen Sie separate Dateien unter `src/Aivana.Windows/KeyboardAdapters/` für LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog und generische QmkVia-Anleitung; Ändern Sie die Registrierung/Benutzeroberfläche und fügen Sie Adapterprotokollprüfungen hinzu.

**Interfaces:** Consume Task 10 Adapterverträge. Native Bibliotheken werden nur von validierten App-/Anbieterstandorten geladen; Das Fehlen des SDK ist eine nicht verfügbare Funktion. Beleuchtung und Eingabemöglichkeiten sind getrennt.

- [ ] Überprüfen Sie die aktuellen offiziellen SDK-Header, Weitergabebedingungen, Softwareabhängigkeiten und unterstützten Modelle, bevor Sie jeden Adapter implementieren. Aufzeichnungsquellen und genaue Versionen.
- [ ] Fehlschlagende Tests für fehlende/falsche SDK-Architektur, Drücken/Freigeben/Wiederholen des Ereignisses, Trennen der Verbindung, fehlerhafte Antwort und nicht unterstütztes Gerät hinzufügen. Verwenden Sie injizierbare Abstraktionen mit nativen Aufrufen für deterministische Prüfungen.
- [ ] Logitech G-Tasten-Eingabe und unterstützte Beleuchtung separat implementieren; Razer Chroma-Beleuchtung plus Synapse Standardtastenzuordnungsassistent; SteelSeries GameSense Zustand/Beleuchtung; Wooting analoge Abfrage mit konfigurierbaren Schwellenwerten/Hysterese; QMK/VIA Anleitung zur Standardschlüssel-Einrichtung. Beanspruchen Sie keine proprietären Eingaben von Beleuchtungs-APIs.
- [ ] Validieren Sie kompatible reale Geräte, sofern verfügbar. Markieren Sie andere implementierte/ungetestete Hardware mit Modell-/Firmware-Nachweis. Fehlendes verwendbares SDK ist ein offengelegtes externes Tor.

### Aufgabe 12: Exklusives Tastaturfilter-Backend

**Files:** Erstellen Sie `src/Aivana.Windows/KeyboardFilter/`, Installationsquelle unter `tools/keyboard-filter/`, Backend-Integrationstests und Bereitstellungsdokumentation. Ein dediziertes signiertes Treiberprojekt ist erforderlich, wenn kein geeignetes lizenziertes Backend die Prüfung besteht.

**Interfaces:** Backend meldet den Status „installiert/kompatibel/signiert“; Macht gerätespezifischen Aufnahmestart/-stopp, Schlüsselkanten und Notentriegelung verfügbar. Ein separater Watchdog stellt den Pass-Through bei Controller-Verlust wieder her. Die Haupttastatur wird standardmäßig nie exklusiv.

- [ ] Bewerten Sie den Führerschein des Bewerbers, die unterzeichnete binäre Herkunft und den aktuellen Stand Windows 11/Secure Boot-Kompatibilität. Zeichnen Sie eine Go/No-Go-Entscheidung auf. Keine Treiberinstallation während normaler Tests.
- [ ] Schreiben Sie Vertragstests für Filterzielauswahl, Fail-Open, Heartbeat-Ablauf, IPC-Verlust, Wiederverbindungsmehrdeutigkeit, Pause/Abbruch, Freigabe und Entsorgung bei gehaltener Taste.
- [ ] Backend, explizite Auswahl-/Aktivierungs-Benutzeroberfläche, Notfallfreigabe und Installations-/Deinstallationsprogramm implementieren. Ersetzen Sie nicht die Timing-Heuristik „Low-Level-Hook“/Raw Input.
- [ ] Validierung auf einer wiederherstellbaren Testmaschine mit zwei Tastaturen: ausgewählte Taste wird nur auf dem ausgewählten Gerät unterdrückt; Haupteingabe bleibt davon unberührt; Absturz/Suspendieren/Deinstallieren stellt die Eingabe wieder her.
- [ ] Abschluss der Gate-Produktion bei tatsächlich signierten/lizenzierten Backend- und physischen Tests. Wenn keine externe Signierung/Lizenzierung verfügbar ist, halten Sie die Aufgabe unvollständig und führen Sie genaue Abhängigkeitsnachweise durch.

### Aufgabe 13: Integriertes Verifizierungs-, Übersetzungs- und Freigabepaket

**Files:** `docs/testing/2026-10-12-six-extensions.md` hinzufügen; Aktualisieren Sie relevante DE/EN/FR/IT-Dokumentationen, Lokalisierungskataloge und Rauchkontrolleintrittspunkte. Behalten Sie andere Dokumentationsänderungen bei.

- [ ] Integrieren Sie alle Einstiegspunkte und Snapshots; Vervollständigen Sie Übersetzungen mithilfe des bestehenden 27-Sprachkatalog-Workflows und legen Sie nicht überprüfte Übersetzungen offen.
- [ ] Führen Sie `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` und `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test` aus.
- [ ] Untersuchen Sie Desktop-/Mobile-Screenshots und überprüfen Sie die 100/150/200-prozentuale Windows-Skalierung, Konfigurations-Roundtrips, Plugin-Lebenszyklus, Netzwerksperrung und echte Tastatur-Wiederverbindung.
- [ ] In einem neuen Artefaktverzeichnis veröffentlichen mit `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; Dazu gehören Begleitdienst-/Host-Binärdateien und Lizenzhinweise. Überschreiben Sie nicht die installierte App.
- [ ] Melden Sie abgeschlossene Tests, nicht getestete Hardware-Adapter und externe Release-Gates. Alle sechs Leistungen müssen ihre Akzeptanzkriterien erfüllen, bevor die gesamte Anfrage als abgeschlossen gilt.

## Planen Sie eine Selbstüberprüfung

Alle sechs Spezifikationsabschnitte sind den Aufgaben 1–12 zugeordnet; Die Aufgabe 13 umfasst die Lokalisierung, die integrierte Verifizierung und die Freigabe. Bereitschaftsberichte, Ausführungsserialisierung, Legacy-Konfiguration, Geheimnisse, Paketvertrauen und Mehrdeutigkeit des physischen Geräts haben explizite Besitzer und Tests. Externe Treiber-/SDK-/Hardware-Gates bleiben bis zum Nachweis unvollständig und werden nicht als Software-Erfolg gewertet.

## Ausführungsauswahl zur Benutzerüberprüfung

Empfohlen: native Ausführung in diesem Chat, Aufgabe für Aufgabe, da die meisten Module Verträge gemeinsam nutzen und der aktuelle Arbeitsbereich umfangreiche bestehende Änderungen enthält. Die delegierte Ausführung ist verfügbar, wenn der Benutzer sie auswählt. Überprüfen Sie diesen Plan und wählen Sie vor der Implementierung die Ausführungsmethode aus, die für den Arbeitsablauf beim Schreiben von Plänen erforderlich ist.

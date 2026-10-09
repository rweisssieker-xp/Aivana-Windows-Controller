# Aivana Windows Controller – Entwurf

[English (US)](../../en-US/superpowers/specs/2026-10-08-windows-controller-design.md) | [Deutsch](../../de-DE/superpowers/specs/2026-10-08-windows-controller-design.md) | [Français](../../fr-FR/superpowers/specs/2026-10-08-windows-controller-design.md) | [Italiano](../../it-IT/superpowers/specs/2026-10-08-windows-controller-design.md)

Stand: 8. Oktober 2026. Status: zur Prüfung vor Implementierung.

## Ziel und vereinbarter Umfang

Eine eigenständige Windows-App macht eine normale PC-Tastatur zu einem frei konfigurierbaren Aktionspanel ähnlich einem Stream Deck. Der Nutzer hat alle vorgeschlagenen Aktionsarten, Makros, Geräteerkennung, Profile und eine gute GUI bestätigt. Angeschlossene Corsair- und andere Tastaturen werden erkannt; zusätzliche Herstellerfunktionen werden nur angeboten, wenn sie tatsächlich unterstützt werden.

Annahmen: deutsche Oberfläche, lokale Nutzung ohne Konto, Windows 11 x64 als erstes Ziel, keine Treiberinstallation. Die Tastatur bleibt zum normalen Schreiben nutzbar. Globale Belegungen verwenden standardmäßig Tastenkombinationen mit Modifikatoren. Einzelne Buchstabentasten werden nicht standardmäßig global reserviert.

## Technische Entscheidung

Native C#/.NET-10-App mit WPF und MVVM. Das stabile .NET-10-SDK ist im Arbeitsbereich verfügbar. WPF erlaubt eine individuell gestaltete Desktop-Oberfläche und direkte Windows-Integration. Alternativen wären eine Weboberfläche mit separatem Windows-Dienst oder eine plattformübergreifende Desktop-App; beide bringen zusätzliche Laufzeit- und Kommunikationskomplexität für dieses Windows-Projekt. Die native Variante ist deshalb die gewählte Grundlage.

Aufteilung: Desktop-Projekt für Ansichten und Tray, Core-Projekt für Konfiguration und Ausführungslogik, Windows-Projekt für Hotkeys, Eingabesimulation und Geräte, Testprojekt für plattformunabhängige Regeln. Herstelleradapter hängen an einer festen Capability-Schnittstelle und sind von den Standardaktionen unabhängig.

## Oberfläche

Dunkles Design mit ruhigen anthrazitfarbenen Flächen, gut lesbarer Schrift und türkisfarbenem Akzent. Links Navigation für Dashboard, Profile, Geräte und Einstellungen. Oben aktives Profil, Suche und Schalter zum Pausieren aller Hotkeys. Die Oberfläche bleibt bei 100–200 Prozent Windows-Skalierung bedienbar.

Das Dashboard enthält ein anpassbares Kachelraster. Jede Kachel zeigt Symbol, Namen, Hotkey und Ausführungsstatus. Klick führt dieselbe Aktion wie der Hotkey aus. Rechts öffnet sich ein Editor mit Aktionsart, Parametern, Hotkey-Aufnahme, Symbol/Farbe, Aktivierung und Testschaltfläche. Neu, Duplizieren, Löschen und Umordnen sind verfügbar. Eine Tastaturansicht zeigt Belegungen schematisch für deutsches Standardlayout; sie behauptet keine exakte Geometrie des erkannten Geräts.

Profile können angelegt, kopiert, umbenannt und manuell aktiviert werden. Optional wird einem Profil ein Prozessname zugeordnet. Ein passendes Vordergrundprogramm aktiviert sein Profil, ansonsten gilt das globale Standardprofil. Bei mehreren passenden Profilen gilt eine ausdrücklich sichtbare Reihenfolge. Der Editor erklärt Hotkey-Konflikte direkt an der Belegung.

## Hotkeys und Eingaben

Windows RegisterHotKey verarbeitet normale globale Kombinationen mit unterdrückter Tastenwiederholung. Nur das aktive Profil wird registriert. Reservierte oder bereits belegte Kombinationen erscheinen als Fehler; die App meldet sie nicht als erfolgreich aktiviert. Die Aufnahme im Editor löst keine Aktionen aus.

Vor simulierten Eingaben werden die auslösenden Modifikatortasten freigegeben beziehungsweise deren physisches Loslassen abgewartet. Unicode-Text und Tastenkombinationen werden mit SendInput gesendet. Jede selbst gedrückte Taste wird auch bei Abbruch wieder losgelassen. Aktionen laufen außerhalb des UI-Threads; Ausführung und Profilwechsel dürfen die Oberfläche nicht blockieren.

## Aktionen und Makros

Aktionsarten: Programm mit Argumenten und Arbeitsverzeichnis starten, Datei/Ordner über Windows öffnen, HTTP-/HTTPS-Webseite öffnen, Unicode-Text eingeben, Tastenkombination senden, Medien Play/Pause/Nächster/Vorheriger/Stopp, Lautstärke höher/niedriger/Stumm sowie Profil wechseln. Parameter werden vor dem Speichern und erneut vor der Ausführung validiert. Programme werden direkt gestartet; Argumente werden nicht implizit als Shellbefehle interpretiert.

Makros enthalten eine geordnete Liste der genannten Aktionen und explizite Pausenschritte. Schritte lassen sich hinzufügen, bearbeiten und verschieben. Wiederholungen sind endlich konfigurierbar (1–100); Endlosschleifen gehören nicht zur ersten Version. Ein laufendes Makro kann über die Oberfläche oder einen eigenen Abbruchhotkey beendet werden. Wiederholtes Auslösen derselben laufenden Belegung startet keinen parallelen Lauf. Unterschiedliche Makros mit Eingabesimulation werden serialisiert, damit ihre Tastendrücke sich nicht vermischen. Bei einem Schrittfehler stoppt das Makro und zeigt den betroffenen Schritt an.

## Geräte und Hersteller

Geräteinventar über Windows Raw Input und ergänzende Geräteinformationen: Anzeigename, Gerätepfad, Hersteller-/Produktkennung soweit verfügbar, Verbindungsstatus und erkannte Fähigkeiten. An-/Abstecken aktualisiert die Liste ohne Neustart. Ein physisches Gerät kann mehrere HID-Einträge besitzen; diese werden nach belastbaren Kennungen gruppiert, andernfalls sichtbar als getrennte Einträge geführt.

Normale Hotkeys gelten tastaturübergreifend. Die Geräteansicht bietet Informationen und Fähigkeiten; eine separate Tastatur mit exklusiv abgefangenen Tasten ist nicht Teil dieses vereinbarten Modus.

Corsair-Erkennung funktioniert auch ohne iCUE. Ein optionaler iCUE-SDK-Adapter bietet Verbindungsstatus und unterstützte Beleuchtungsaktionen, wenn SDK, iCUE und kompatibles Gerät vorhanden sind. Die UI unterscheidet erkannt, SDK verfügbar, verbunden und tatsächlich steuerbar. Das SDK dokumentiert vor allem LED-Steuerung; proprietäre G-Tasten werden nur wie normale Hotkeys unterstützt, wenn Windows entsprechende Eingaben erhält. Es gibt keine pauschale Zusage für sämtliche Sondertasten.

Andere Hersteller erhalten zunächst generische Erkennung und Standardaktionen. Die Adapter-Schnittstelle erlaubt spätere Integrationen; unsupported Fähigkeiten werden ehrlich angezeigt. Hardwaretests für nicht vorhandene Modelle werden nicht als bestanden angegeben.

## Speicherung und Alltag

Versionierte JSON-Konfiguration unter dem lokalen App-Datenverzeichnis. Atomisches Speichern mit letzter gültiger Sicherung; fehlerhafte Dateien werden nicht still überschrieben. Import prüft Format, Version, IDs und Parameter und zeigt eine Vorschau vor dem Übernehmen. Importierte Aktionen werden erst nach ausdrücklicher Aktivierung ausführbar. Export enthält Profile und Belegungen.

Tray-Menü: Öffnen, Profilwahl, Hotkeys pausieren, Makros abbrechen, Beenden. Fenster schließen minimiert standardmäßig in den Tray und erklärt dies beim ersten Mal. Autostart ist optional und standardmäßig aus. Nur eine Instanz verwaltet Hotkeys. Beenden registriert Hotkeys ab, bricht Ausführungen ab und gibt Ressourcen frei.

Lokale Aktivitätsansicht mit begrenztem Verlauf für Erfolg, Fehler und Abbruch. Kein Protokoll sämtlicher Tastatureingaben. Textaktionsinhalte werden nicht in Fehlermeldungen ausgeschrieben. Kein Cloud-Dienst und keine Telemetrie.

## Fehlerbehandlung und Grenzen

Fehlende Programme, ungültige URLs, belegte Hotkeys, beschädigte Profile und nicht erreichbares Hersteller-SDK erhalten konkrete deutsche Meldungen. Ein Adapterfehler beeinträchtigt keine Standardaktionen. Eingaben in höher privilegierte Anwendungen können an Windows-Berechtigungsgrenzen scheitern; Fehler werden erklärt, die App verlangt nicht standardmäßig Administratorrechte.

## Prüfung und Abnahme

Automatisierte Prüfungen für Profilpriorität, Konflikterkennung, Konfigurationsimport, atomische Speicherung, Makroreihenfolge, Wiederholung, Abbruch und Fehlerpropagation. Windows-Smoke-Tests für reale Hotkeys, Text und Tastenkombinationen, Medienaktionen, Geräteinventar, Tray und sauberes Beenden. Keine Tests, die versehentlich beliebige Programme oder Webseiten starten.

GUI-Prüfung: Belegung anlegen und bearbeiten, Aufnahme ohne Ausführung, Klick/Hotkey mit gleicher Wirkung, Makroabbruch, Konfliktmeldung, Profilwechsel, Geräteansicht, Import/Export und Skalierung. Herstellerspezifische Hardwareprüfung wird separat mit tatsächlich vorhandenem Gerät dokumentiert.

Lieferung: Quellcode, reproduzierbare Build-/Startanleitung, deutsche Bedienungsanleitung und lokales Windows-x64-Publish-Artefakt. Erfolg bedeutet eine ausführbare App mit allen generischen Funktionen; Corsair-Fähigkeiten sind nur bei nachgewiesener SDK-/Geräteunterstützung als verfügbar markiert.

## Technische Quellen

- Microsoft Keyboard Input / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Input: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Offizielles Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Nächster Schritt

Nach Prüfung dieses Entwurfs: konkreten Implementierungsplan erstellen und zur Auswahl der Ausführung vorlegen. Im leeren Arbeitsordner ist noch kein Git-Repository vorhanden; dieses Dokument wurde daher noch nicht committed.

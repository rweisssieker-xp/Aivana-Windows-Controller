# MAKRIVO: Erweiterungen der Steuerung

Der Nutzer hat alle neun vorgeschlagenen Verbesserungen und ein erkennbares Symbol im Infobereich angefordert. Bestehende Konfigurationen, insbesondere die Helligkeitsbelegung auf G1/G2, müssen kompatibel bleiben. Bereits vorhandene, nicht eingecheckte Änderungen an Marke und Dokumentation bleiben erhalten.

## Ergebnisse und Grenzen

1. Helligkeitsaktionen wählen alle unterstützten Monitore oder eine stabile Monitor-ID. Fehlende explizite Ziele führen zu sichtbaren Fehlern. Das Dashboard liest Helligkeit, Ausgabelautstärke, Mikrofon- und OBS-Zustand asynchron mit begrenztem Aufwand aus.
2. Sondertasten unterstützen Sofortdruck, kurzen Druck, Halten und Doppeldruck mit Wiederholungsunterdrückung. Eine gehaltene Ebenentaste wechselt vorübergehend die Belegung; Loslassen stellt sie wieder her. Vorhandene Sofortbelegungen bleiben kompatibel. Pause, Profilwechsel, Trennung und Abbruch löschen ausstehende Gesten.
3. Der Ablaufeditor bearbeitet Bedingungen, Vorwärtsverzweigungen und Fehlerregeln (Stoppen/Fortsetzen) innerhalb eines Kachelentwurfs. Bedingungen prüfen Dateien, laufende Prozesse und Fenstertitel. Unbegrenzte Sprungzyklen sind ausgeschlossen.
4. Die ausdrücklich gestartete Aufnahme erfasst Tastenkombinationen und Wartezeiten, ignoriert injizierte Eingaben und endet mit Escape. Vor der Übernahme werden die Aktionen geprüft; normaler Text wird nicht aufgezeichnet.
5. Gerätebezogene Standard-Sondertasten anderer Hersteller verwenden Windows Raw Input. Proprietäre Tasten benötigen eine unterstützte Hersteller-API oder eine Herstellerzuordnung zu einer Standardtaste. Nicht unterstützte Funktionen werden ehrlich gekennzeichnet. Die Diagnose erklärt Profil, Pause, Belegungskonflikte und tatsächlichen Adapterzustand.
6. Der Profilaustausch nutzt einen gewählten gemeinsamen Ordner, auch einen durch einen vorhandenen Cloud-Client synchronisierten Ordner. Revisionsprüfungen verhindern stilles Überschreiben. Vor dem Abrufen erscheint eine Vorschau; importierte Aktionen sind deaktiviert und Hardwarezuordnungen entfernt. Aktionsinhalte werden nur ausdrücklich eingeschlossen. MAKRIVO speichert keine Cloud-Zugangsdaten.
7. Oberflächentexte gehören in die bestehenden 27 Sprachkataloge; DE/EN/FR/IT bleiben die mindestens geprüften Dokumentationssprachen. Übersetzungslücken müssen benannt werden.
8. Ein türkises M-Tastatursymbol ersetzt das allgemeine Windows-Symbol im Infobereich. Im Pausenmodus erscheint ein bernsteinfarbenes Pausenzeichen. Programm und Fenster verwenden dasselbe Symbol. Der Hinweistext enthält Zustand und Profil.

## Prüfung

Core-Prüfungen umfassen Rückwärtskompatibilität, Momentaufnahmen, Abbruch, Gestenzeiten, Verzweigungen/Fehler, Aufnahmefilter und Synchronisationskonflikte. Windows-Prüfungen umfassen das reine Auslesen der Monitore, SDK-Loslassereignisse und Raw-Input-Verarbeitung. WPF-Prüfungen decken neue Einstiege und Ressourcen ab, ohne reale Monitorhelligkeit zu ändern, in andere Anwendungen zu tippen oder Nutzerprofile zu überschreiben. Die Bereitstellung bewahrt eine Sicherung und die G1/G2-Konfiguration.

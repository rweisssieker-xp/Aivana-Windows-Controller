> PRÜFENTWURF — 26. Oktober 2026. Nicht zur Veröffentlichung freigegeben. Freigabepunkte stehen separat in PRIVACY-REVIEW.md.

# Datenschutzhinweise für MAKRIVO

## Verantwortlicher

Verantwortlich für die hier beschriebene Verarbeitung ist die Aivana GmbH, Paulusstr. 45a – Hinterhaus – LOFT45, 33602 Bielefeld, Deutschland, vertreten durch Udo Bergmann. Sie erreichen uns auch für Datenschutzanfragen unter info@aivana-gmbh.ai.

Diese Hinweise betreffen MAKRIVO für Windows und unseren externen Kauf- und Lizenzdienst. Microsoft verarbeitet Informationen zum Microsoft-Store-Zugriff und App-Erwerb nach seiner eigenen [Datenschutzerklärung](https://www.microsoft.com/de-de/privacy/privacystatement).

## Lokaler App-Betrieb

MAKRIVO speichert Profile, Aktionsdefinitionen, Einstellungen und Sicherungskopien lokal. Darin können Anwendungsnamen, Dateipfade, Internetadressen und eingegebene Texte enthalten sein. Aktionen können Anwendungen starten, Websites öffnen oder Daten mit von Ihnen eingerichteten Diensten austauschen. Diese Dienste erhalten die für die Aktion erforderlichen Informationen; für deren Verarbeitung gelten ihre eigenen Datenschutzhinweise.

Das lokale Protokoll des Meeting-Experiments erfasst Zeitstempel, Experimentkennungen, Dauer, Ereignisse und Korrekturen. Es rotiert bei ungefähr 2 MiB. Lokale Daten und Sicherungskopien bleiben bis zur Löschung oder Ersetzung bestehen. Das Löschen eines Profils entfernt nicht zwangsläufig Kopien in Sicherungen oder synchronisierten Ordnern. Der Kauf- und Lizenzprozess übermittelt diese Konfigurationen und Experimentprotokolle nicht an Aivana.

Wenn Sie die mobile Steuerung aktivieren, können gekoppelte Browser in Ihrem Netzwerk ausgewählte Profile, Aktionsnamen, Statusinformationen und die letzten 50 Verifikationseinträge erhalten. Die Verbindung verwendet HTTPS. Ein Kopplungs-Cookie kann 30 Tage bestehen; Token-Hashes werden lokal gespeichert. Schützen Sie den Zugriff auf gekoppelte Geräte und das Netzwerk.

Die OBS-Anbindung überträgt Befehle und Statusinformationen an den von Ihnen eingestellten OBS-Server. Die Standardverbindung ist lokal; ein eingerichteter entfernter Server erhält dieselben Funktionsdaten. Das OBS-Passwort wird über die Windows-Anmeldeinformationsverwaltung gespeichert. Die Profilsynchronisierung schreibt in einen von Ihnen ausgewählten Ordner. Gehört dieser zu einem Cloud-Dienst, verarbeitet dieser die synchronisierten Inhalte. Plugins laufen als vertrauenswürdige lokale Prozesse mit den ihnen verfügbaren Berechtigungen; eine Netzwerk-Sandbox isoliert sie nicht. Beleuchtungsanbindungen kommunizieren mit lokal installierten Herstellerdiensten.

## Optionale lokale KI, sofern in Ihrer Version aktiviert

Wenn Sie den lokalen KI-Meeting-Assistenten ausdrücklich nutzen, kommuniziert MAKRIVO mit Ollama unter 127.0.0.1:11434 auf Ihrem PC. Übermittelt werden Ihr eingegebenes Ziel sowie Kennungen, Namen und Arten der ausgewählten Ressourcen. Namen können einen Dateinamen oder einen Website-Host erkennen lassen. Über diese Schnittstelle werden keine Ressourcen-Dateiinhalte, vollständigen Ressourcenpfade, Audio- oder Bilddaten gesendet. Ihr Zieltext selbst kann personenbezogene Daten enthalten; geben Sie nur ein, was Sie Ihrem lokalen Modell bereitstellen möchten. Auch zurückgegebene Bezeichnungen und Erläuterungen können personenbezogene Daten enthalten. Ollama und installierte Modelle sind eigenständige Software; deren Speicherung und Konfiguration bestimmen die weitere lokale Verarbeitung. Diese Schnittstelle sendet die Anfrage nicht an einen von Aivana betriebenen Modelldienst.

## Kauf, Zahlung und Aktivierung

Der externe Kaufdienst verwendet einen von Stripe gehosteten Checkout. Stripe erhält Ihre dort angegebenen Kontakt-, Rechnungs- und Zahlungsdaten. Wir verarbeiten Bestell- und Zahlungsreferenzen, Zahlungsstatus und die für Rechnung, Lizenzbereitstellung und Support erforderlichen Informationen. Unser Lizenzdienst speichert keine Kartendaten. Stripe handelt je nach Verarbeitungszweck als Auftragsverarbeiter oder Verantwortlicher; siehe [Datenverarbeitungsvereinbarung](https://stripe.com/legal/dpa) und [Datenschutzerklärung](https://stripe.com/privacy).

Der Lizenzdienst verwendet Cloudflare Workers und D1 für Kaufzuordnung und Aktivierungen. Gespeichert werden Bestell- und Lizenzkennungen, ein Hash des geheimen Wiederherstellungscodes, Zahlungsreferenzen sowie Gerätecodes und Aktivierungsstatus. Ein Gerätecode ist eine mittels SHA-256 abgeleitete Kennung zur Zuordnung der vier von der Lizenz erlaubten PCs. Er ist pseudonym, nicht anonym. Die zugrunde liegende Windows-Kennung wird diesem Dienst nicht übermittelt. Die signierte Lizenzdatei wird lokal gespeichert und offline geprüft; es gibt keine regelmäßige Online-Lizenzprüfung.

Bei Anfragen erhält die Hosting-Infrastruktur Netzwerkdaten einschließlich der IP-Adresse. Der Dienst verwendet kurzlebige Kennungen zur Begrenzung von Anfragen, um Kauf- und Aktivierungsendpunkte zu schützen. Das Cookie zur Kaufzuordnung ist für den Zugriff auf Ihre Bestellung in diesem Browser erforderlich und läuft nach einem Jahr ab, sofern es nicht vorher gelöscht wird. Es wird nur über HTTPS übertragen und ist für Seitenskripte nicht zugänglich. Bewahren Sie Ihren Wiederherstellungscode geheim auf. Cloudflare erläutert seine eigene Verarbeitung in seiner [Datenschutzerklärung](https://www.cloudflare.com/privacypolicy/).

Kauf- und Aktivierungsdaten verarbeiten wir zur Vertragserfüllung (Art. 6 Abs. 1 lit. b DSGVO), erforderliche Buchführungsunterlagen zur Erfüllung gesetzlicher Pflichten (lit. c) und notwendige Missbrauchsschutzdaten aufgrund unseres berechtigten Interesses am Schutz des Dienstes und Lizenzsystems (lit. f). Erforderliche Kauf-, Rechnungs- und Aktivierungsdaten werden für den Abschluss der Transaktion beziehungsweise die Gerätezuordnung benötigt; ohne sie können diese Leistungen nicht erbracht werden.

## Support, Empfänger und Speicherdauer

Wenn Sie uns kontaktieren, verarbeiten wir Ihre Kontaktdaten, Nachricht und Anhänge zur Beantwortung und zum vertraglichen Support (Art. 6 Abs. 1 lit. b DSGVO). Bei anderen Anfragen ist unser berechtigtes Interesse an der Beantwortung die Grundlage (lit. f). Senden Sie nur für das Problem erforderliche Diagnosedaten.

Zugriff erhalten befugte Mitarbeitende und für Zahlung, Hosting, Kommunikation und Buchführung erforderliche Dienstleister sowie Behörden, soweit gesetzlich erforderlich. Vertrags- und Lizenznachweise speichern wir zur Verwaltung Ihres fortbestehenden Nutzungsrechts und zur Bearbeitung von Ansprüchen, Buchführungsunterlagen für die geltenden gesetzlichen Fristen und Supportinformationen, solange sie zur Klärung und Dokumentation des Anliegens benötigt werden. Entfallen diese Zwecke und einschlägige Aufbewahrungspflichten, entfernen wir die Daten. Für lokale Kopien, von Ihnen ausgewählte Synchronisierungsanbieter und eigenständig verantwortliche Anbieter gelten deren jeweilige Aufbewahrungsbedingungen.

## Ihre Rechte

Unter den gesetzlichen Voraussetzungen können Sie Auskunft, Berichtigung, Löschung, Einschränkung und Datenübertragbarkeit verlangen. Gegen eine auf berechtigte Interessen gestützte Verarbeitung können Sie Widerspruch einlegen. Eine etwaige Einwilligung können Sie für die Zukunft widerrufen. Kontakt: info@aivana-gmbh.ai. Sie können sich bei einer Datenschutzaufsichtsbehörde beschweren, insbesondere an Ihrem Wohn- oder Arbeitsort oder am Ort eines vermuteten Verstoßes. Wenden Sie sich an uns, wenn eine automatische Aktivierungsablehnung überprüft werden soll.

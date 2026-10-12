# Responsive WPF-Oberfläche

Die Hauptseiten richten sich nach der nutzbaren Inhaltsbreite, nicht nach einer fest gesetzten Fensterbreite. Unter 900 DIPs Inhaltsbreite stehen Dashboard und Editor untereinander. Unter 620 DIPs werden Kennzahlen zweispaltig. Unter 820 DIPs Fensterbreite wird die Seitenleiste eingeklappt. Mindestgröße: 640 × 480 DIPs.

Profil- und Einstellungsbereiche sowie Tastatur-Steuerelemente wechseln ihre Spalten anhand der tatsächlichen Grid-Breite. Dabei werden vorhandene Steuerelemente umgeordnet, statt ihre Eingaben neu anzulegen. Aktionskarten teilen die verfügbare Zeilenbreite auf. Werkzeugleisten brechen um. Die Tastaturseite ist vertikal erreichbar, die physische Tastatur behält ihre Proportionen.

Automatischer WPF-Test: alle fünf Hauptseiten bei 640 × 480, 900 × 620 und 1440 × 900; Screenshots je Seite; Spaltenwechsel in beide Richtungen mit Prüfung des erhaltenen Textes. DIPs sind logische WPF-Einheiten. Echte Windows-Skalierung bei 150/200 Prozent und mehrere Monitore sind damit noch nicht validiert.

Prüfstand: Release-Build ohne Fehler/Warnungen. Vollständiger WPF-Smoke-Test mit allen Größen, Spaltenwechseln, Tastaturlayouts und Sprachprüfungen bestanden. git diff --check bestanden.

# Physische Tastaturansicht für Mapping

## Ziel

Die Tastaturansicht soll beim Zuordnen von Aktionen die tatsächlichen Tastenpositionen zeigen. Gleich breite Zeilen reichen nicht: Enter, Shift, Space, Navigation, Nummernblock und Hersteller-Zusatzbedienelemente unterscheiden sich.

## Umsetzung

- Katalog mit getrennten physischen IDs, Geometrie in Tasteneinheiten, aufgedruckten Legenden und Windows-Eingabecodes.
- ISO-DE und ANSI-US jeweils als Vollformat und TKL; zusätzlich Corsair K100 RGB, die über iCUE als angeschlossenes Modell erkannt wurde.
- ISO Enter mit L-Form, Nummernblock mit hohen Plus-/Enter-Tasten und breiter Null; Navigation und Pfeile an ihren eigenen Positionen.
- Modell/Layout ausdrücklich auswählen. USB VID/PID und Modellname beweisen keine Sprachvariante. Generische Profile und anhand Herstellerunterlagen modellierte Profile sind erkennbar unterschiedlich bezeichnet; keine Millimetermaß- oder Firmware-Kompatibilitätsgarantie.
- Scancode auf das aktive Windows-Eingabelayout abbilden, damit ein physischer Tastenklick nicht versehentlich eine andere Taste belegt, etwa Y/Z beim Wechsel zwischen US und DE.
- Zuordnung öffnet einen Entwurf im bestehenden Aktionseditor. Speichern, Konfliktprüfung und Verwerfen bleiben die bestehenden Mechanismen. Klicks starten keine Aktionen.
- Proprietäre G-Tasten, Profil-/Lock-Tasten und iCUE-Rad werden dargestellt, aber ohne implementierten Eingabeadapter nicht als normale Windows-Hotkeys ausgegeben. Konfigurierbare Hersteller-Shortcuts bleiben ein möglicher Weg.
- Belegungen bleiben globale Windows-Hotkeys; eine Geräteansicht bedeutet keine gerätespezifische Unterdrückung oder exklusive Belegung einer einzelnen physischen Tastatur.

## Belege

[K100-Handbuch mit Bedienelementen](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI/ISO-Erklärung](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Prüfung

Geometrie-, Varianten- und Eingabecode-Tests; Builds und bestehende Core-/Windows-Tests; WPF-Smoke für Darstellung und Zuordnung ohne Ausführung oder Änderung der echten Nutzerkonfiguration. Herstellerrecherche dokumentiert weitere Modell-/SKU-Anforderungen unabhängig vom aktuell implementierten Layout-Katalog.

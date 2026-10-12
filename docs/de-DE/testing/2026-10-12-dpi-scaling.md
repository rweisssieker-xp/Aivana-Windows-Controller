<!-- generated-doc locale=de-DE source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Maschinell unterstützte Übersetzung. Struktur, Codebeispiele und Links werden geprüft; eine muttersprachliche oder juristische Abnahme ist damit nicht verbunden. Historische Berichte behalten ihr ursprüngliches Datum und ihren damaligen Umfang. [Originalquelle](../../testing/2026-10-12-dpi-scaling.md)

# DPI-Prüfung

MAKRIVO deklariert PerMonitorV2 im eingebetteten Anwendungsmanifest. Der WPF-Test prüft mit GetWindowDpiAwarenessContext und AreDpiAwarenessContextsEqual den tatsächlich wirksamen Kontext des Hauptfensters.

Die zusätzlichen Layout-/Rastertests simulieren 150 und 200 Prozent auf einer 1920 × 1080 Anzeige mit Taskleistenreserve: logische Fenstermaße 1280 × 693 beziehungsweise 960 × 520 DIPs. Alle fünf Hauptseiten werden bei 144 beziehungsweise 192 DPI als PNG gerendert. Dateien heißen scale-simulated-150-* beziehungsweise scale-simulated-200-*.

Diese Tests ändern keine Windows-Einstellungen. Sie validieren keinen echten WM_DPICHANGED-Monitorwechsel, keine gemischte Monitorkonfiguration und keine tatsächliche Schärfe auf einem anderen Display. Die physischen Skalierungstests bleiben offen.

Die WinForms-Analyzerwarnung WFO0003 wird ausschließlich für dieses WPF-Startprojekt unterdrückt: WinForms wird für die Infobereichsintegration verwendet, WPF steuert den Start; DPI wird vor der Fenstererstellung per Manifest festgelegt.

Microsoft-Dokumentation: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Sichtprüfung: Das Dashboard wechselt zusätzlich unter 650 DIPs Fensterhöhe in die gestapelte Darstellung, damit Karten und Editor nicht in einen zu kleinen mittleren Bereich gequetscht werden.

Prüfstand: Release-Build ohne Fehler/Warnungen; vollständiger WPF-Smoke-Test einschließlich tatsächlichem PerMonitorV2-Fensterkontext und simulierten 150/200-Prozent-Renderings bestanden. Die 200-Prozent-Dashboardaufnahme wurde nach der Höhenanpassung visuell geprüft. git diff --check bestanden.

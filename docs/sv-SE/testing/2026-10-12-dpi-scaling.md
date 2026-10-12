<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-12-dpi-scaling.md)

# DPI-provning

MAKRIVO deklarerat PerMonitorV2 i den inbyggda applikationsmanifest. WPF-testet kontrollerar med GetWindowDpiAwarenessContext och AreDpiAwaressContextsEqual den verkligt effektiva sammanhanget i huvudfönstret.

Simulera ytterligare layout/rastertest 150 och 200 Procent på en 1920 × 1080 Visning med Taskbarreserv: Logisk fönsterstorlek 1280 × 693 eller i förhållande 960 × 520 Alla fem huvudsakliga sidor kommer att 144 eller i förhållande 192 DPI reduceras som PNG. filer värmande scale-simulated-150-* eller scale-simulated-200-*.

Dessa tester ändrar inte Windows inställningar. De validerar ingen verklig WM_DPICHANGED-monitorändring, ingen blandad monitorskonfiguration och ingen verklig skärm på en annan skärm. De fysiska skalanterna är öppna.

Den WinForms-Analysvarning WFO0003 Det är uteslutande för denna WPF-Startprojektet undertrycks: WinForms Används för informationssammanfattning, WPF styra start; DPI bestäms före fönstret genom manifest.

Microsoft-Dokumentation: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Visning: Dashboardet växlar ytterligare under 650 DIPs fönsterhöjd till den inbyggda displayen så att kort och redaktörer inte blir för små i ett medium område.

Teststatus: Release-Build utan fel/varningar; fullständig WPF smoke-test inklusive den faktiska PerMonitorV2-Fensterkontext och simulerade 150/200-Prozent-Renderings genomgått. Den 200-Prozent-Dashboardaufnahme har kontrollerats visuellt efter höjdjustering. Git diff -check överlevde.

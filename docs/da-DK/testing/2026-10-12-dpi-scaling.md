<!-- generated-doc locale=da-DK source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-12-dpi-scaling.md)

# DPI-prøve

MAKRIVO erklæret PerMonitorV2 i den indlejrede anvendelsesmanifest. Testet WPF undersøger det faktiske kontekst af hovedvinduet med GetWindowDpiAwarenessContext og AreDpiAwaressContextsEqual.

Simulere de ekstra layout/rastertest 150 og 200 procent af en 1920 × 1080 Visning med opgavebestand: Logisk vindues størrelse 1280 × 693 I forhold til 960 × 520 Alle fem hovedsider vil være 144 I forhold til 192 DPI reduceret som PNG. filer kaldet scale-simulated-150-* eller scale-simulated-200-*.

Disse tests ændrer ikke Windows indstillinger. De validerer ingen virkelige WM_DPICHANGED-monitorændringer, ingen blandede monitors konfiguration og ingen virkelige skarphed på en anden display. De fysiske skalaer er åbne.

Den WinForms analysevarsel WFO0003 undertrykkes udelukkende for dette WPF startprojekt: WinForms anvendes til informationsområdet integration, WPF styrer start; DPI fastsættes ved manifester før vindueindstilling.

Microsoft dokumentation af https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Visning: Dashboardet skifter yderligere under 650 DIPs vinduehøjde til den indtegnede udstilling, så kort og editor ikke bliver for små i et mellemrum.

Teststand: Release-build uden fejl/varsel; fuldstændig WPF smoke-test, herunder den virkelige PerMonitorV2-Fensterkontext og simuleret 150/200-Prozent-Renderings gennemgået. Den 200-Prozent-Dashboardaufnahme blev visuelt testet efter højdeanpassning. Git diff -check er klar.

<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-12-dpi-scaling.md)

# DPI-tester

MAKRIVO erklært PerMonitorV2 i den innebygde anvendelsesmanifestet. WPF-testen undersøker med GetWindowDpiAwarenessContext og AreDpiAwaressContextsEqual den virkelige konteksten i hovedvinduet.

Simulere de ekstra layout/rastertestene 150 og 200 prosent av en 1920 × 1080 Visning med oppgaver reserve: logisk vindu størrelse 1280 × 693 I forhold til 960 × 520 DIPs. alle fem hovedsider vil være med 144 I forhold til 192 DPI redusert som PNG. filer varme scale-simulated-150-* eller scale-simulated-200-*.

Disse testene endrer ikke Windows innstillinger. De validerer ingen ekte WM_DPICHANGED-monitorendringer, ingen blandet monitorkonfigurasjon og ingen faktisk skarphet på en annen skjerm. De fysiske skala testene er åpne.

Den WinForms analysevarsel WFO0003 undertrykkes utelukkende for dette WPF startprosjektet: WinForms brukes til informasjonsområde integrasjon, WPF styrer start; DPI er fastsatt ved manifest før vinduinnstilling.

Microsoft dokumentasjon: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Visning: Dashboardet skifter ytterligere under 650 DIPs vinduhøyde til den gravede utstillingen slik at kart og redaktører ikke blir skåret inn i et for lite mellomområde.

Teststand: Release-build uten feil/varsel; fullstendig WPF smoke test inkludert ekte PerMonitorV2-Fensterkontext og simulert 150/200-Prozent-Renderings gjennomgått. Den 200-Prozent-Dashboardaufnahme ble visuelt testet etter høydejustering. Git diff - sjekk over.

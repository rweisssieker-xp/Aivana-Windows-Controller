<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-12-dpi-scaling.md)

# Kontrola DPI

MAKRIVO deklaruje PerMonitorV2 v manifestu vložené aplikace. Test WPF používá GetWindowDpiAwarenessContext a AreDpiAwarenessContextsEqual ke kontrole skutečného efektivního kontextu hlavního okna.

Simulujte dodatečné testy rozvržení/mřížky 150 a 200 procent na jednoho 1920 × 1080 Displej s rezervou hlavního panelu: logické rozměry okna 1280 × 693 respektive 960 × 520 DIPy. Všech pět hlavních stránek je zahrnuto 144 respektive 192 DPI vykreslené jako PNG. Soubory se nazývají scale-simulated-150-* nebo scale-simulated-200-*.

Tyto testy nemění žádná nastavení Windows. Neověřují skutečné přepínání monitoru WM_DPICHANGED, smíšenou konfiguraci monitoru a skutečnou ostrost na jiném displeji. Testy fyzického měřítka zůstávají otevřené.

Varování analyzátoru WinForms WFO0003 je potlačeno výhradně pro tento projekt spuštění WPF: WinForms se používá pro integraci oznamovací oblasti, WPF řídí spouštění; DPI se nastavuje pomocí manifestu před vytvořením okna.

Dokumentace Microsoft: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Vizuální kontrola: Přístrojová deska se také přepne na skládaný displej pod výškou okna 650 DIPs, takže mapy a editor nejsou stlačeny do příliš malé střední oblasti.

Testovací stolice: Uvolněte sestavení bez chyb/varování; Prošel úplným kouřovým testem WPF včetně skutečného PerMonitorV2-Fensterkontext a simulovaného 150/200-Prozent-Renderings. 200-Prozent-Dashboardaufnahme byl po výškovém nastavení vizuálně zkontrolován. git diff --kontrola prošla.

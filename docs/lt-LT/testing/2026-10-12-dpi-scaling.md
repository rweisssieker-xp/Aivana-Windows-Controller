<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-12-dpi-scaling.md)

# DPI egzaminas

MAKRIVO deklaruota PerMonitorV2 įterptame taikymo manifeste. Testas WPF naudojant GetWindowDpiAwarenessContext ir AreDpiAwaressContextsEqual išnagrinėja pagrindinio lango veiksmingą kontekstą.

Papildomi Layout / Raster testai 150 ir 200 procentinė dalis vienos 1920 × 1080 Vaizdo įrašas su užduočių juosta: loginis lango dydis 1280 × 693 arba santykiai 960 × 520 Visi penki pagrindiniai puslapiai 144 arba santykiai 192 DPI sumažėjo kaip PNG. failų šiltas skalė-simuliuojama-150-* arba skalės simuliuotas-200-*.

Šie bandymai nesikeičia Windows nustatymų. Jie nevaliduoja tikrojo WM_DPICHANGED monitorių keitimo, jokios mišrios monitorių konfigūracijos ir jokios faktinės spinduliuotės kitame ekrane. Fiziniai skalavimo testai lieka atviri.

Šios WinForms-Analizės įspėjimas WFO0003 Tik dėl šios WPF-Pradinis projektas sustabdytas: WinForms Naudojamas informacinės erdvės integracijai, WPF valdo pradžią; DPI nustatomas prieš lango kūrimą.

Microsoft dokumentacija: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Matymo patikrinimas: Dashboard papildomai keičiasi prie lango aukštis 650 į įterptą vaizdą, kad žemėlapiai ir redaktorius nebūtų įtraukti į per mažą vidutinį plotą.

Bandymo būdas: išleidimas be klaidų / įspėjimų; visiškai WPF smoke testas, įskaitant faktinį PerMonitorV2-Fensterkontext ir simuliuojamas 150/200-Prozent-Renderings. Jis 200-Prozent-Dashboardaufnahme buvo vizualiai patikrinamas po aukštumo koregavimo. git diff - patikrinti.

<!-- generated-doc locale=et-EE source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../testing/2026-10-12-dpi-scaling.md)

# DPI eksam

MAKRIVO deklareeritud PerMonitorV2 sisseehitatud rakendusmanifestis. WPF testid kasutavad GetWindowDpiAwarenessContext ja AreDpiAwaressContextsEqual peamise akna tõhusat konteksti.

Täiendavad paigaldamise/rastreerimise katsed 150 ja 200 Protsendiks üks 1920 × 1080 Näitus töölaua reservi: loogiline akna suurus 1280 × 693 Või seoses 960 × 520 Kõik viis peamist lehekülge 144 Või seoses 192 DPI kui PNG vähenenud. failid kuum scale-simulated-150-* või skaleeritud simuleeritud-200-*.

Need testid ei muuda Windows seadeid. Nad ei valideeri tõelist WM_DPICHANGED-monitori muutust, ei segatud monitori konfigureerimist ega tegelikku teravust teises ekraanis. Füüsilised skalierimise testid on avatud.

See on WinForms-Analüüsi hoiatus WFO0003 Ainult selle jaoks WPF-Algatus on surutud: WinForms Kasutatakse infosektorite integreerimiseks, WPF alustamine; DPI määratakse enne akna loomist manifestiga.

Microsoft dokumendid: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Vaatamine: Dashboard liigub 650 akna kõrguse alla graafiliseks näituseks, et kaardid ja redigeerijad ei jääks liiga väikeseks keskmise piirkonnaks.

Testi: vabastamine ilma veadeta / hoiatused; täieliku WPF müra testi, sealhulgas tegelik PerMonitorV2-Fensterkontext ja simuleeritud 150/200-Prozent-Renderings läbi. 200-Prozent-Dashboardaufnahme on visuaalselt kontrollitud pärast kõrguse kohandamist. GIT DIF - kontrollida.

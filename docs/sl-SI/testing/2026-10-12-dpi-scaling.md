<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-12-dpi-scaling.md)

# DPI preiskava

MAKRIVO izjavljeno PerMonitorV2 v vgrajenem manifestu uporabe. Test WPF z GetWindowDpiAwarenessContext in AreDpiAwaressContextsEqual preveri dejansko učinkovit kontekst glavnega okna.

Simulacija dodatnih Layout/Raster testov 150 in 200 Odstotek na enem 1920 × 1080 Prikaz z rezervoarjem delovne plošče: logična velikost okna 1280 × 693 ali v zvezi 960 × 520 DIPs. vse pet glavnih strani bodo vključene 144 ali v zvezi 192 DPI kot PNG. datoteke vroče scale simulated-150-* ali scale simulated-200-*.

Ti testi ne spreminjajo nastavitev Windows. Ne potrjujejo resnične WM_DPICHANGED spremembe monitorja, ne mešajo konfiguracije monitorja in ne dejansko hitrost na drugem zaslonu. Fizični testi so odprti.

ki jih WinForms-Analitična opozorila WFO0003 Izključno za to WPF-Začetek projektov: WinForms Uporablja se za integracijo informacij, WPF upravlja začetek; DPI je določen z manifesto pred ustvarjanjem okna.

Microsoft dokumentacija: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Preverjanje: Dashboard se dodatno premakne pod 650 višino okna v prikaz, tako da se karte in urednik ne pritrdijo na preveliko srednjo območje.

Preizkusni položaj: Release-build brez napake / opozorila; polno WPF preizkusno, vključno z dejanskim PerMonitorV2-Fensterkontext in simuliranim 150/200-Prozent-Renderings. 200-Prozent-Dashboardaufnahme je bil vizualno pregledan po prilagoditvi višine. Git diff - preverjanje je bilo.

<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-12-dpi-scaling.md)

# DPI-controle

MAKRIVO declareert PerMonitorV2 in het ingebedde toepassingsmanifest. De WPF-test gebruikt GetWindowDpiAwarenessContext en AreDpiAwarenessContextsEqual om de daadwerkelijke effectieve context van het hoofdvenster te controleren.

De aanvullende lay-out-/rastertests simuleren het percentage 150 en 200 op een 1920 × 1080-scherm met taakbalkreserve: logische vensterafmetingen 1280 × 693 respectievelijk 960 × 520 DIP's. Alle vijf hoofdpagina's worden weergegeven als PNG met 144 of 192 DPI. Bestanden worden scale-simulated-150-* of scale-simulated-200-* genoemd.

Deze tests veranderen geen Windows-instellingen. Ze valideren geen echte WM_DPICHANGED-monitorwisseling, gemengde monitorconfiguratie en werkelijke scherpte op een ander scherm. De fysieke schaaltests blijven open.

De WinForms-analysatorwaarschuwing WFO0003 wordt exclusief onderdrukt voor dit WPF-opstartproject: WinForms wordt gebruikt voor integratie van het meldingsgebied, WPF regelt het opstarten; DPI wordt ingesteld via manifest voordat het venster wordt gemaakt.

Microsoft-documentatie: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Visuele inspectie: Het dashboard schakelt ook over naar de gestapelde weergave onder 650 DIPs-vensterhoogte, zodat kaarten en editor niet in een te klein middengebied worden geperst.

Testbank: release build zonder fouten/waarschuwingen; Geslaagd voor de volledige WPF-rooktest, inclusief daadwerkelijke PerMonitorV2-Fensterkontext en gesimuleerde 150/200-Prozent-Renderings. De 200-Prozent-Dashboardaufnahme is na de hoogteverstelling visueel geïnspecteerd. git diff --check geslaagd.

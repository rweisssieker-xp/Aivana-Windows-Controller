<!-- generated-doc locale=da-DK source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Kontrollerens gennemførelsesplan

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> For agentarbejdere: Brug parallel isoleret ejendom; brugeren udtrykkeligt anmodet om fuld gennemførelse med maksimalt nyttige underagenter.

** Mål: ** Leverer det indfødte Windows hotkey panel i den godkendte design.
**Arkitektur:** Grundkonfiguration og annullerbar udførelse; Windows Adapter; WPF Desktop sammensætning.
**Teknisk Stack:** .Netværk 10, WPF, Win32, JSON: Ingen betalt afhængighed.
**Spec:** ../specs/2026-10-08-Windows-controller-design.md

## Globale begrænsninger
Tysk UI; lokal opbevaring; Windows x64; troværdig hardware kapacitet status; ingen tastatur logging; ingen standardstart.

## Oversigt fokus
Misformede importer må ikke erstatte sparede indstillinger; holdte modifikatorer må ikke forurene produktionen; unplugged hardware må ikke kollapse; lukning skal bevare tilgængeligheden af spor; annullerede makroer skal frigive ressourcer.

## opgaver og ejerskab
- [x] Core agent: src/Aivana.Core og tests/Aivana.Tests. Modeller, JSON holdbarhed, validering, profiludvalg og MacroRunner. Gennemfør mislykkede adfærdstester og derefter gennemføre og køre alle kernetester.
- [x] Windows agent: src/Aivana Windows RegisterHotKey, enhedsliste, SendInput, foregroundproces og valgfri Corsair lysadapter. Kontroller indfødte strukturstørrelser og registrering rengøring med sikker røgtest.
- [x] GUI-agent: src/Aivana.Desktop/MainWindow.xaml og .cs plus UI-hjælpere kun. Dark dashboard, fuld editor, profiler, enheder, indstillinger, tastatur overblik og macro editor. At opbygge en kontrakt, der eksisterer
- [x] Forældre: delte kontrakter, projektfiler, DesktopController sammensætning, applikation/tray livssyklus, import/eksport dialoger, dokumentation, integrationskontrol, udgivelse og løbende app-røgstest.

## Delte grænser
Kontrakter leveres i Core/Models.cs. Kontroller implementering lever i Desktop/DesktopController.cs. UI kalder kontroller metoder gennem denne konkrete klasse. Agenterne modtager nøjagtige underskrifter i deres opgaver. Alle har separate filer. Forældre løser cross-projektfejl efter at alle agenter er færdige.

## Verifikation og levering
- [x] dotnet build Aivana.slnx -c Udgivelse: nul fejl.
- [x] dotnet kører --projekt tests/Aivana.Tests -c Udgivelse: adfærdskontrol passerer.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Lansering udført, inspektion Windows UI og processtabilitet, øve sikker profil / redigering / persistens flow.
- [x] Skriv README med opbygning/start instruktioner og hardware begrænsninger; registreringer og resterende hardware-kun tests.

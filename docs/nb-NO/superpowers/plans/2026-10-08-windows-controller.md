<!-- generated-doc locale=nb-NO source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Kontrollplan for gjennomføring

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> For agentarbeidere: bruk parallell isolert eiendom; brukeren eksplisitt forespurt fullstendig implementering med maksimalt nyttige underagenter.

** Mål: ** Leverer den opprinnelige Windows hotkey-panelet i godkjent design.
**Arkitektur:** Nøkkelkonfigurasjon og kansellerbar utførelse; Windows Adapter; WPF Desktop sammensetning.
**Teknisk Stack:** .Nett 10, WPF, Win32, JSON: Ingen betalt avhengighet.
**Spec:** ../specs/2026-10-08-Windows-controller-design.md

## Globale begrensninger
Tysk UI; lokal lagring; Windows x64; sann hardwarekapasitet status; ingen tastatur logging; ingen standard oppstart.

## Oversikt fokus
Misformede importer må ikke erstatte sparte innstillinger; holdte modifikatorer må ikke forurenses av produksjonen; uplugget maskinvare må ikke kollapse; lukking må bevare tilgjengelighet; annullerte makroer må frigjøre ressurser.

## Oppgaver og eiendom
- [x] Core agent: src/Aivana.Core og tester/Aivana. Modeller, JSON utholdenhet, validering, profilvalg og MacroRunner. Gjør mislykkede atferdstester og deretter implementere og kjøre alle kjernetestene.
- [x] Windows agent: src/Aivana Windows RegisterHotKey, enhetsinventor, SendInput, foregroundprosess og valgfri Corsair lysadapter. Sjekk innfødte strukturer og registrering rengjøring med trygge røykprøver.
- [x] GUI-agent: src/Aivana.Desktop/MainWindow.xaml og .cs pluss UI assistenter bare. Dark dashboard, full editor, profiler, enheter, innstillinger, tastaturoversikt og makro editor. Bygge en gang kontraktene eksisterer.
- [x] Foreldre: delte kontrakter, prosjektfiler, DesktopController sammensetning, applikasjon/tray livssyklus, import/eksportdialoger, dokumentasjon, integrasjon sjekker, publisere og kjøre app røyktest.

## Delte grenser
Kontrakter lever i Core/Models.cs. Kontroller implementering lever i Desktop/DesktopController.cs. UI kaller kontroller metoder gjennom denne konkrete klassen. Agenter mottar nøyaktige signaturer i oppgavene sine. Hver har separate filer. Foreldre løser kryssprosjektfeil etter at alle agenter er ferdig.

## Verifikasjon og levering
- [x] dotnet build Aivana.slnx -c Release: null feil.
- [x] dotnet kjører -prosjekt tester/Aivana.Test -c Release: atferdskontroll passerer.
- [x] dotnet publiserer src/Aivana.Desktop -c Release -r win-x64 --self-containing true -o artifacts/win-x64.
- [x] Lansering publisert, inspeksjon Windows UI og prosessstabilitet, utøve sikker profil / redigering / utholdenhet.
- [x] Skriv README med bygg/start instruksjoner og maskinvare begrensninger; inspeksjoner og gjenværende maskinvare-enbart tester.

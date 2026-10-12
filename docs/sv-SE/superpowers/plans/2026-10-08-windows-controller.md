<!-- generated-doc locale=sv-SE source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Kontrollplanen genomför

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> För agentarbetare: använd parallellt isolerad ägande; användaren uttryckligen begärde fullständig genomförande med maximalt användbara underagenter.

**God: ** Leverera den inhemska Windows hotkeypanelen i den godkända designen.
**Arkitektur:** Kärnkonfiguration och annullerbar genomförande; Windows Adapterar; WPF Desktop sammansättning.
**Tekniska Stack:** .Nät 10, WPF, Win32, JSON. ingen betald beroende.
**Spec:** ../specs/2026-10-08-Windows-controller-design.md

## Globala restriktioner
Tyska UI; lokal lagring; Windows x64; trovärdig maskinvarukapacitet; ingen tangentbordslogging; ingen standardstart.

## Översikt fokus
Dålig import ska inte ersätta sparade inställningar; hållda modifierare ska inte förorenar produktionen; obegränsad hårdvara ska inte kollapsa; stängning måste bevara tillgänglighet; annullerade makron måste släppa resurser.

## Uppgifter och ägande
- [x] Kärnmedel: src/Aivana.Kore och tester/Aivana.Tester. Modeller, JSON persistens, validering, profilval och MacroRunner. Kör misslyckade beteendest och genomför sedan alla kärntest.
- [x] Windows agent: src/Aivana Windows RegisterHotKey, enhetsinventering, SendInput, foregroundprocess och valfri Corsair ljusadapter. Kontrollera inhemska strukturstorlekar och registrering rengöring med säker rökprov.
- [x] GUI-agenten: src/Aivana.Desktop/MainWindow.xaml och .cs plus endast hjälpmedel. Dark dashboard, full editor, profiler, inställningar, tangentbord översikt och macro editor. Skapa kontrakt när det finns.
- [x] Föräldrar: delade kontrakt, projektfiler, DesktopController sammansättning, applikation/tray livscykel, import/export dialoger, dokumentation, integrationskontroll, publicera och köra app röktest.

## Delade gränssnitt
Kontrakt live på Core/Models.cs. Installation av kontroller lever i Desktop/DesktopController.cs. UI kallar kontroller metoder genom den konkreta klassen. Agenterna får exakta signaturer i sina uppgifter. Var och en har separata filer. Föräldrarna löser projektfel efter att alla agenter är färdiga.

## Verifikation och leverans
- [x] dotnet build Aivana.slnx -c Release: noll fel.
- [x] dotnet kör -projektprov/Aivana.Test -c Release: beteendekontroll passerar.
- [x] dotnet publicerar src/Aivana.Desktop -c Release -r win-x64 --self-content true -o artifacts/win-x64.
- [x] Lansera publicerat genomförbart, inspektera Windows UI och processstabilitet, utöva säker profil / redigering / persistensflöde.
- [x] Skriv README med bygg- och startinstruktioner och hårdvarubegränsningar; inspelningar och återstående hårdvaruprovering.

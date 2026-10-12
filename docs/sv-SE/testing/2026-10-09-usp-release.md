<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-09-usp-release.md)

# Keyboard support, överlämnande och exekution feedback

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

De tre prioriterade förbättringarna genomfördes i den befintliga Windows appen:

- Specialnyckeltråd: verklig iCUE-stödda enhet/nyckelinventyr, tillfällig G-nyckelfång, konfigurations-success verifiering, konfliktvalidering och ett osavgjort handlingsplan. Capture rollback, queued callback invalidation och normalbindande restaurering täcker fel och dialog stängning.
- Keyboard overlay: fysisk referensgeometri, aktiv profilbindningar, levande profil / redigering och vanlig Windows nyckelfärdighet. Håll Ctrl+Alt+F1 eller ta taggen genom paletten/nyckeln. Oaktiverar, klickar genom och saknas från arbetsfältet.
- Exekution feedback: typed Vänta / Running / Steg / Avslutad / Misslyckad / Avvisad faser, steg / repetition framsteg och en icke-aktiverande toast. Feedback utesluter åtgärdsinnehåll och undantagsmeddelanden. Sista steget avbokning kan inte rapporteras fullbordad. Kompletteringen beskriver verkställighet, inte en oberoende OBS statlig fråga.

Verifikation av:

- 24 Kärnkontroll passerade, inklusive fasbeställning, misslyckanden/annulleringsresultat, sista steget annullering, lyssnare undantag isolering och uteslutning av privata åtgärdsvärden.
- Release-lösning byggdes utan varningar/fel.
- Windows kontroller passerade; de anslutna K100 rapporterade G1–G6 och inhemska konfiguration/utgivning lyckades. Ljudändpunkter inspekterades utan att ändra deras tillstånd.
- WPF rök passerade med alla referens överlagrings layouter, trollkarlar/konflikter/drift persistens, live inhemska fångstkonfiguration, callback forwarding, stall callback avslag och paus-bindning restaurering. Native callback input simuleras; en fysisk mänsklig G-nyckel är fortfarande en manuell kontroll.
- Den aktuella invariansen i fönstret kontrollerades för utförandet av toast. Overlay-interaktionsegenskaper och show/hide kontrollerades; skärmdumpar gjordes för trollkarlen, överlay och toast.
- 27 språk innehåller alla 819 kanoniska strängar; offline platsinnehavare/nyckel validering passerade.

Snapshots och det fullständiga GUI-resultatet är i artifakter/usp-win-x64/smoke. Test använder tillfälliga profiler; produktionsinställningar backas upp och hash-checkas under implementeringen.

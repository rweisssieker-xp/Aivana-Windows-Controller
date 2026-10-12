<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-12-six-extensions.md)

# Sex utvidgningar: genomförande och provning

Stannar: 12. oktober 2026, branschen `codex/six-extensions`. Arbetsnivå, ingen produktionsavgift.

## Tjänst

Kommandepaletten öppnar posterna **Ställa in arbetsplats**, **Tastaturtillverkare och SDK**, **Mobilpanel**, **Utvidgningar**, **UI Automation**, **UI-avslutningen** och **genomförandekontroll**.

- Arbetsplatsassistenten skapar profiler för arbetsplatser, möten, strömning, forskning och projekt. Program från registrering/startmenyn eller filval, URL-adresser, utgåva/skärm och OBS scen visas som steg före lagring. Institutionen utför inga åtgärder.
- I stegredigeraren öppnas **resultaten för att kontrollera ** förhands- och efterkontroll. Kurs och mobilpanel skiljer faktiska bekräftelser från otillbörliga effekter. Relativa ändringar, knappkort och textinmatning ger ingen fast effektbekräftelse. Ett bevis på processen visar inte någon professionell beredskap för tillämpningen.
- UI-automation använder en separat, tidsbegränsad arbetare. Välj via musposition, valkontroll, Invoke/Select/Toggle/Focus/SetValue/WaitState; flera träffar, deaktiverade mål och lösenordfält avvisas. Innehållet innehåller stödda klick och paus; textinnehållet skär inte. Innan intagningen granskas.
- Förlängningar: ZIP-test, hash, schema, inaktiverad import, uttrycklig förtroende före aktivering, isolerad process med begränsad IPC, export, inaktivera, avinstallera, återställa/avlägsna tidigare version. Processisolering är inte sandbox: Delade plugins kör med användarskydd. Exempelpaketet `makrivo.example.zip` innehåller Countdown och Statusaction. Stödda scheman utgör en dokumenterad enkel JSON-schema delområde i källkoden; ingen offentlig marknad.
- Mobilpanel: Välj lokala IP- och delade profiler, öppna HTTPS-adressen, kontrollera certifikatet med hjälp av det som visas SHA-256-Fingerprints och lita på det medvetet, sedan ange en enda sex tecken kod. Privat nycklar är skyddade med Windows-DPAPI; endast hashar av kundåtkomst sparas. Certifikat ändras efter förnyelse eller IP-växling. Windows Firewall släpps vid behov av användaren. Inga automatiska certifikatinstallationer, portförändringar eller brandväggändringar. Klienten kan återkallas. Panelen visar status och testflöde; det är ännu inte en universell live-sensor.

## tangentbordet

| Tillverkare | genomförandet | Begränsning |
|---|---|---|
| Corsair | befintliga iCUE G-knappar och RGB | Lokal iCUE-anslutning och lämplig modell behövs |
| Logitech G | G-Key SDK och separata LED-SDK | original lokal x64-DLL uttryckligen släpps; G-Key SDK ger inte en individuell identitet av flera tangentbord |
| Razer | Chroma-RGB och standardknappsidentifiering | ägaren i förväg Synapse Förenade |
| SteelSeries | GameSense-RGB och standardknappsidentifiering | Föregående inläggFöregående GG |
| Wooting | Analogvärden, tryck-/löpsvågor | original lokal x64-SDK-DLL; normal HID-inmatning förblir aktiv |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Övriga | Fysiska standardknappar efter enhet och skanningskod | Fn och proprietära kontroller beroende på modell |

Inga nya SDK-DLL levereras. De nya tillverkaradaptern är ännu inte hårdvaruvaliderade utan lämplig fysisk utrustning.

**Den exklusiva andra makroplanen är inte klar:** Raw Input övervakar enheter separat, men undertrycker deras normala Windows-En produktionsavgift kräver en licensierad, undertecknad Windows 11/Secure Boot kontrollerade filter drivrutiner samt tester av nödsituation, kollaps, suspension och återkoppling. Ingen drivrutine har installerats..

## Prövningar

- Release-Build: framgångsrik, noll varningar/fel.
- Core-konsoler: Alla testgrupper är framgångsrika, inklusive konflikter med tillverkaren, plugins och faktiska provningsfel.
- Utvidgningsintegration: verklig UIA-arbetare med WPF testanvändning; unik invoke, mångsidighet, inaktiverade mål, lösenord undantag. Echter Named-Pipe Plugin Process, protokollgränser, Analog Hysteresis och Logitech ABI dekryptering framgångsrikt.
- Faktisk HTTPS-host: Picked certifikat, obegränsad åtkomst, falska/en gångs kod, ursprungskydd, webbläsarfiler, hashlagring och skyddade nycklar framgångsrikt. Ett skannelsfel som upptäckts i testet med efemiska nycklar har korrigerats.
- Windows-Suite med uttryckligt avbruten framgångstest: framgångsrik; Audio/OBS/iCUE/Raw Input och skärmstatus kontrolleras. Vid tidigare körning misslyckades den fullständiga suiten vid den förnekade fokusväxlingen till det isolerade testfönstret. I den efterföljande körningen till AI-protokollet fanns också den fullständige suiten inklusive fokusväxling och anslutna iCUE-En annan upprepning misslyckades på nytt vid fokusväxling; det sammanhangsbaserade testet är därför inte stabilt grönt. Detta test har inte försvagats. korttids misslyckade DDC-läsningar upprepas begränsat; ingen ljusstyrka har ändrats.
- WPF-Smoke: alla tidigare och nya dialoger, konfigurationsvarians, redigerare och CRUD framgångsrikt; skärmdumpar i byggordningen `smoke`.
- Webbplats: Playwright med Edge, 390×844 och 1024×768, Anslutningsflöde/UUID/en gångsändning/ingen horisontell övergång framgångsrikt. API-Mock; verklig HTTPS-test separat. `artifacts/mobile-browser`. Browser-plugin var inte tillgänglig, därför regelbunden Playwright Fallback.
- Oberoende kod översyn utförd; försenade mobila beställningar, brist på terminal kontroller, processvägar test, plugins rollback och återupptagning av RGB-hjärtbeat korrigeras.

Ännu öppen: exklusiv drivrutinering, fysiska tillverkare/telefon/tablett-LAN-test, Windows-skalancering 150/200 procent och fullständig översättning av alla nya dialogtexter. Nya dialekttexter är främst tyska; befintliga språkkataloger förblir bevarade. Det är därför det totala kravet ** inte helt avlägsnat **.

## För att skapa paket

`./tools/publish-complete.ps1 -SelfContained` skapar en ny mapp under `artifacts/release-*`, inklusive skrivbord, UIA-arbetsare, HTTPS-host, webbläsarfiler, importerbara exempelplugin och körtid licensanvisningar. Den installerade applikationen överskrids inte. Utan `-SelfContained` behöver paketet lämpligt .NET-10-Desktop-/ASP.NET-Runtimes.

Paketet är en lokal teststation, inte en undertecknad försäljningsinstaller. De egna användarvillkoren och SDK-försäljningsrättigheterna är separata upplösningar; se befintlig dokumentation under `docs/licensing`.

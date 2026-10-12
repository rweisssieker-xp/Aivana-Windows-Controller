<!-- generated-doc locale=sv-SE source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../README.md)

# MAKRIVO

[Fullständig dokumentation på alla 27 språk](README.md) · [Full användarhandbok](HANDBOOK.md)

**genom Aivana.** [Aktuell Inventor och Release Limits](FEATURES.md) · [Förlängningsrapport](testing/2026-10-12-six-extensions.md) · [Lokala AI-assistenter](testing/2026-10-12-local-meeting-assistant.md)

** Engelska (USA) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

En lokal Windows hotkey-kontroller med Stream Deck-stil plåtar, profiler, makrar och tangentborddetektion. Gränssnittet stöder 27 språk. Ingen konto eller molnanslutning krävs för daglig användning.

[Dokumentation](README.md) · [Verifikation](VERIFICATION.md) · [Funktion testrapport](testing/2026-10-09-usp-release.md)

## Att börja

Efter publiceringen är ansökan på `artifacts/win-x64/MAKRIVO.exe`. Håll hela utgångsfilen tillsammans; du kan kopiera den någon annanstans. Windows 11 x64 är det ursprungliga målet. Build artefakter är uteslutna från Git; en källcheckning innehåller inte en förbyggd verkställbar.

Installera .NET 10 SDK som anges i `global.json` för att bygga och köra från källan:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publicera en självinnehållsbyggnad utan att behöva en separat installerad .NET-arbetstid:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Grundläggande arbetsflöde

1. Välj en profil och skapa en åtgärd.
2. Ställ in dess namn, ikon, färg och tangentbordskort. Ctrl/Alt/Shift-kombinationer minskar slumpmässig aktivering när du skriver.
3. Lägg till ett eller flera steg: applikationer, filer, webbplatser, text, kortfattningar, media, volym, profilväxling eller förseningar.
4. Rädda . Förändringar förblir en utkast tills sparas; discard återställer den spara versionen. Tilen Play-knappen och dess hotkey använder samma utförande.
5. Återbeställa pilar och makro steg med drag-and-drop eller pilknappar. Templates förenklar installationen. Macros kan upprepa upp till 100 gånger. Avbryt via gränssnittet, systemtray-menyn eller Ctrl+Alt+Escape enligt standard.

Applikationsspecifika profiler använder ett processnamn utan sin fulla väg. Profilen bryter länkar; standardprofilen är fallback. Automatisk profilval måste aktiveras i Inställningar.

Stängningen av huvudfönstret håller appen i systemet som standard. Dubbelklicka på ikonen för att öppna den igen; använd menyn för att komma ut. Startup med Windows är valfri och inaktiverad som standard. Hotkeys kan stängas centralt.

## Handlingar och kommandospalett

Lägg till åtgärd öppnar en sökbar katalog med 90 förinställningar i åtta kategorier. Var och en har en beskrivning och förhandsvisning. Macro-redaktören använder samma katalog för att lägga till eller ersätta steg. Välj en förinställning i den här redigeringsarbetsflödet skapar ett utkast utan genomförande.

Kommandapalet öppnas från header eller med Ctrl+K inuti appen. Det söker sparade åtgärder över profiler, katalogprecifers, profilväxling, navigering, paus/resumé, makroannullering och import/export. Filtrera genom sparade åtgärder, katalogfunktioner eller appkommandon. Tryck på nycklarna, aktiverar Enter, Ctrl+Enter redigerar en redigerbar inmatning och Escape stänger. Favorit visas först. Ready-to-use-kommandon kan köras omedelbart; förinställningar som kräver att du öppnar ett utkast. Inställningar kan definiera en global palettkortning. Nya konfigurationer använder Ctrl+Shift+Space; befintliga konfigurationer får inte automatiskt en ny global nedskärning.

Ljudåtgärder ställer volym exakt från 0 till 100 procent och mute, unmute eller toggle utgång och mikrofonenheter. Välj ett specifikt slutpunkt eller lämna det inställt för Windows standard. En otillgänglig sparad slutpunkt ger ett fel istället för att styra en annan enhet. Refresh Devices laddar upp anslutna enheter.

OBS Studio använder WebSocket v5: start/stop/toggle/pause/resume inspelning; start/stop /toggle streaming; ändra scener; toggle input mute. Aktivera WebSocket-servern under Verktyg i OBS och ange dess slutpunkt i redaktören. Check Connection och Load Names läser OBS version, scener och inmatningar utan att starta en inspelning eller ström. Välj namn från listan eller skriv in dem manuellt. Passord kan sparas i Windows Credential Manager och är uteslutna från exporterade konfigurationer. Otillgängliga OBS anslutningar rapporterar ett fel.

Profiler kan matcha ett processnamn och en del av ett fönstertitel. Båda villkoren måste matcha. Lock Profile förhindrar automatisk växling för sessionen. Förhållanden från Foreground använder den sista fokuserade externa appen.

Text och clipboard stöd `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, och `{{user}}`. Datum användning `yyyy-MM-dd`; Tid att använda `HH:mm`. Okända platsinnehavare förblir oförändrade. `{{profile}}` identifierar den lanserade åtgärdsprofilen; `{{process}}` identifierar den yttre applikationskontexten.Clipboardinnehåll läses endast när platsinnehållet används.Clipboards åtgärd skriver text men inte automatiskt.

Fönstret åtgärder minimera, maximera, återställa eller ordna ett fönster på vänster / höger sida av dess arbetsyta. Systemets åtgärdslås Windows. Dessa åtgärder utförs endast när det uttryckligen inbjuds.

Preview visar steg, upprepningar och variabler utan genomförande. Undo och Redo behåller upp till 100 utkaststater; textlådan behåller inhemska Ctrl+Z redigering. Markera favoriter i redigeraren eller tile-menyn och filtrera dem på dashboardet. Exekutionskonton gäller endast för den aktuella sessionen.

Text, kortskärm och fönsteråtgärder som lanseras från gränssnittet aktiverar det sista externa målfönstret. Paletten visar detta målkontext. Om målet har stängt eller Windows vägrar aktivering, upphör utförandet med ett meddelande.

## Keyboard support och feedback

Den Special-Key Wizard finns på tangentbordets sida och i paletten. Det listar G-nycklarna rapporterade av iCUE. Detect Key Press fångar dem tillfälligt utan att utföra sina åtgärder. Normala bindningar återvänder efter upptäckt, avbokning eller stängning. En katalogfunktion och valda nycklar öppnas som ett okänt utkast; dubbelbindningar kontrolleras.

Den Keyboard Overlay visar den valda referensläget och aktiva bindningar. Håll Ctrl+Alt+F1 eller ta det från paletten eller tangentbordet. Det tar inte fokus och låter musklick passera. Profil och bindande ändringar uppdateras medan synlig; vanliga Windows nyckelpressar betonas. Tillverkare-specifika kontroller kan inte leverera en motsvarande Windows nyckelsignal.

Exekution feedback visar väntan, steg / upprepning framsteg, slutföring, uppsägning eller misslyckande utan att ta fokus eller exponera den ingivna texten. Commands Executed bekräftar kommandotillverkning, inte en oberoende begärd levande status i en tredjepartsansökan. Feldetaljer visas i aktivitetsloggen.

## Anläggningar och Corsair

Corsair G1–G6 kan tilldelas direkt via den officiella iCUE SDK. Välj enheten och nyckeln i åtgärdsredigeraren och spara, eller förbereda ett utkast från K100 tangentbordsutsikt. länkar gäller för den valda enheten och den aktiva profilen; paus inaktiverar dem också. iCUE måste köras och tillåta åtkomst. fn, iCUE Rulla, profil och lås kontroller kan inte kartläggas via denna G-nyckel API.

Enheten listar Windows tangentbordets gränssnitt och uppdateras periodiskt och när enheten ansluter sig. En fysisk tangentbord kan exponera flera HID-gränssnitt. Standard globala hotkeys arbetar över tillverkare.

Corsair detektion använder tillgänglig information om enheten. RGB-kontrollen kräver kompatibel maskinvara, kör iCUE och den officiella iCUE SDK; appen rapporterar verklig tillgänglighet. Andra tillverkare får för närvarande upptäckt och standardåtgärder. Utanför Corsair G-nyckelintegration fungerar proprietära kontroller när Windows får dem som regelbundna inmatningar. Appen installerar inte en tangentbordskärm.

## Språk

Inställningar erbjuder 27 språk: tyska, engelska, franska, spanska, italienska, portugisiska, nederländska, tjeckiska, slovakiska, ungerska, bulgariska, grekiska, danska, svenska, norska, finländska, estniska, latinska, litauiska, slovenska, kroatiska, ukrainska, ryska, turkiska och serbiska i latinsk skript. Automatiskt följer Windows displaysspråk. Översättningarna är lokala. Layouts, hotkeys och användar-skapade namn är oberoende av gränssnittets språk.

Fullständig arkivdokumentation finns tillgänglig på alla 27 ansökningsspråk via [Språkindex](README.md)och Översättningar är maskinassistenterade, med ursprungliga källlänkar och strukturella kontroller; inhemska talare granskning är separat. Appen accepterar också de regionala koderna `en-US`, `de-DE`, `fr-FR` och `it-IT` och använder deras nummerformat. De befintliga språkkoderna är kompatibla.

## Layout och tillverkare forskning

Den tangentbordsutsikt ger fullstorlek och oöverträffade ISO-DE/ANSI-US referenser plus Corsair K100 RGB layouter. Geometri representerar positioner, breda / talnycklar och ISO Enter. Standard layouter är generiska referenser. K100 layouter följer dokumenterade kontroller utan att kräva millimeter exakta dimensioner. Välj rätt språk/geometri: ett modellnamn eller USB-VID/PID anger inte en specifik variant.

Kartläggning löser fysiska skannekoder med hjälp av den aktiva Windows input layout. Arbetsflödet förbereder ett utkast och behåller spara/konfliktcheck. Standardbindningar är globala Windows hotkeys, inte isolerade till en ansluten tangentbord. Corsair G1-G6 använd enhetspecifik integration iCUE. Andra äganderätt kräver en adapter eller tillverkare-konfigurerade korta. Main Enter och numerisk nyckelpad Enter delar en virtuell nyckelidentitet för globala hotkeys.

och den [Tillverkare forskning](research/tastaturhersteller-sonderfunktionen.md) och [CSV](research/tastaturhersteller-sonderfunktionen.csv) täcker 74 varumärkesgrupper/tillverkare, skiljer konfiguratorer, input SDK och belysning APIs. [Layout bevis](research/keyboard-layout-evidence.md) Skärmmodell, SKU och modulskillnader. Forskning täckning betyder inte att hårdvara stöd är genomfört.

## Data är

Konfiguration lagras i lokala ansökningsdata. Import/export använder JSON. Importen förutspår konfigurationen, ersätter den aktuella konfigurationen efter bekräftelsen och inaktiverar importerade bindningar tills de är manuellt aktiverade. Text-åtgärdsinnehåll samlas inte som en nyckellogg. Credential Manager lösenord är separata från exporterade profiler.

## Verifikation

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows kontroller kan inspektera hårdvara och tillfälligt konfigurera/lämna stödda iCUE bindningar. Kör dem utan en annan kontrollerare med dessa bindningar. Audio endpoint kontroller ändrar inte livevolym eller mute inställningar.

Den ursprungliga insatsen kan inte överskrida Windows privilegier. En normalstartad kontroller kan inte skicka inmatning till en administratörsansökan. Reserverade eller ockuperade hotkeys visas som konflikter.

och den [Dokumentation Index](README.md) länkar historiska designar, planer, forskning och datum testrapporter. Historiska planer beskriver deras registrerade omfattning, inte en aktuell garanti för funktionstöd.

### Arbetsflödesmallar och målfönster väntar

Katalogen innehåller sex redigerbara arbetsflöden: arbetsyta, möte, ström, forskning, projektskruv och textskivor. `workflow.` i katalogen eller kommandapaletten.Väljningen av en arbetsflöde öppnar ett oklart utkast; det kör aldrig omedelbart.Alla steg bevaras när du lägger till en arbetsflödet till en befintlig makro eller tilldelar den till en Corsair Konfigurera exempel URL, programvägar, profiler och OBS Skådespelare innan spara.

och den **Väntar på målfönstret** Steg accepterar ett processnamn (med eller utan `.exe`), en fallsensitiv titel substring, och en timout av 1–600,000 ms. Minst ett filter krävs; båda filtren måste matcha när de levereras. makron fortsätter endast efter att ett matchande synligt fönster svarar och når förgrunden. Använd en specifik titel när flera fönster delar en process. Avbrytning eller timing stoppar följande steg. Fönsterberedskap garanterar inte att ett dokument har laddat eller en extern tjänst som OBS WebSocket är ansluten.

Skärmsljus: Sök efter **Increase skärmsljus**, **Drease skärmljus**, eller **Skärmljus vid 50 procent** i handlingskatalogen. Relativa åtgärder ändrar ljusstyrka med 10 procentsatser; fasta värden sträcker sig från 0 till 100. Stödda interna skärmar använder WMI; kompatibla externa skärmar kräver DDC/CI aktiverat i deras meny. Som standard påverkar åtgärden alla stödda skärmar; en uttrycklig skärm kan väljas i redaktören. Anslutningar och docks kan begränsa supporten.

### Utökade kontroller

Öppna kommandapalet med **Ctrl+K medan fönstret fokuserar**. Sök efter **Arbetsflöde Designer**, **Rekord för macro**, **Synkronisera profiler**, eller **Nyckeldiagnostik** (arbetsflödesdesignern redigerar den valda skivan: fil/process/fönstret villkor, framskjutning räknas och stoppa/ fortsätt att hantera felet..

I ett ljussteg väljer du alla stödda skärmar eller en specifik skärm. En otillgänglig markerad bildskärm producerar ett fel istället för att tyst ändra en annan skärm. Dashboardet visar levande ljusstyrka, ljud och OBS läsningar när deras respektive integrationer är tillgängliga; en otillgänglig läsning presenteras inte som ett mätvärde.

I tygredigeraren tilldelar du en stödjande fysisk speciell nyckel och väljer omedelbar tryck, tryck, håll eller dubbel tryck. Held-skiktselektorer aktiverar tillfälligt en annan mappning och återställer det föregående skiktet vid release. Gester och lager gäller för Corsair SDK G-nycklar och enhetspecifika specialnycklar, inte vanliga globala hotkeys. Standardnyckelfaktura stöder F13–F24 och tangentbord media nycklar. Proprietär Fn, profil och dial kontroller kräver fortfarande en kompatibel leverantöradapter eller en leverantör mappning till en standardnyckel. Raw Input observerar nyckeln och undertrycker inte dess normala beteende Windows. Nyckeldiagnostik förklarar profil, paus, anslutning och bindande problem.

Profilsynkronisering utbyter filer genom en mapp du väljer, inklusive en mapp som hanteras av din befintliga molnklient. Översikt över förhandsgränsen innan importen; importen börjar inaktiveras och avlägsnar hårdvara-specifika mappningar. Revisions förebygger tyst överskrivningar, och inklusive åtgärdsinnehåll är ett uttryckligt val. MAKRIVO ger inte ett molnt konto eller behåller molntillstånd. Tråden använder nu MAKRIVO sin egen teal M-ikon och visar en amber paus när den slutar.

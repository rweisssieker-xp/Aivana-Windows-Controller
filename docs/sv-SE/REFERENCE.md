<!-- generated-doc locale=sv-SE source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../README.md)

# MAKRIVO

[Fullständig dokumentation på alla 27 språk](README.md) · [Full användarhandbok](HANDBOOK.md)

**genom Aivana.** [Aktuell Inventor och Release Limits](FEATURES.md) · [Förlängningsrapport](testing/2026-10-12-six-extensions.md) · [Lokala AI-assistenter](testing/2026-10-12-local-meeting-assistant.md)

** Engelska (USA) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

En lokal Windows-kortkommandostyrenhet med paneler i Stream Deck-stil, profiler, makron och tangentbordsdetektering. Gränssnittet stöder 27 språk. Daglig användning kräver inget konto eller molnanslutning.

[Dokumentation](README.md) · [Verifikation](VERIFICATION.md) · [Funktion testrapport](testing/2026-10-09-usp-release.md)

## Att börja

Efter publicering finns programmet på `artifacts/win-x64/MAKRIVO.exe`. Håll hela utdatamappen samlad; den kan kopieras till en annan plats. Windows 11 x64 är det första målet. Byggartefakter utesluts från Git; en källkodsutcheckning innehåller ingen färdigbyggd körbar fil.

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

1. Välj en profil och skapa en åtgärdspanel.
2. Ange namn, ikon, färg och kortkommando. Ctrl/Alt/Shift-kombinationer minskar oavsiktlig aktivering när du skriver.
3. Lägg till ett eller flera steg: program, filer, webbplatser, text, kortkommandon, medier, volym, profilbyten eller fördröjningar.
4. Spara. Ändringar förblir ett utkast tills de sparas; kassera återställer den sparade versionen. Panelens uppspelningsknapp och kortkommando använder samma körningsväg.
5. Ordna om paneler och makrosteg med dra och släpp eller pilknappar. Mallar förenklar inställningen. Makron kan upprepas upp till 100 gånger. Avbryt via gränssnittet, meddelandefältets meny eller som standard Ctrl+Alt+Escape.

Programspecifika profiler använder ett processnamn utan fullständig sökväg. Profilordningen avgör likvärdiga träffar; standardprofilen är reservvalet. Automatiskt profilval måste aktiveras i Inställningar.

När huvudfönstret stängs finns appen som standard kvar i meddelandefältet. Dubbelklicka på ikonen för att öppna igen; avsluta via ikonmenyn. Start med Windows är valfritt och inaktiverat som standard. Kortkommandon kan pausas centralt.

## Handlingar och kommandospalett

Lägg till åtgärd öppnar en sökbar katalog med 90 förinställningar i åtta kategorier. Varje post har en beskrivning och förhandsvisning. Makroredigeraren använder samma katalog för att lägga till eller ersätta steg. Att välja en förinställning i detta redigeringsflöde skapar ett utkast utan körning.

Kommandopaletten öppnas från sidhuvudet eller med Ctrl+K i appen. Den söker bland sparade åtgärder i alla profiler, katalogförinställningar, profilbyten, navigering, paus/återupptagning, makroavbrott och import/export. Filtrera på sparade åtgärder, katalogfunktioner eller appkommandon. Piltangenter väljer, Enter aktiverar, Ctrl+Enter redigerar en redigerbar post och Escape stänger. Favoriter visas först. Färdiga katalogkommandon kan köras direkt; förinställningar som kräver inställning öppnar ett utkast. Inställningar kan definiera en global palettgenväg. Nya konfigurationer använder Ctrl+Shift+Space; befintliga konfigurationer får inte automatiskt en ny global genväg.

Ljudåtgärder ställer in volymen exakt från 0 till 100 procent och stänger av, sätter på eller växlar ljudavstängningen för utgångs- och mikrofonenheter. Välj en viss ändpunkt eller lämna den oangiven för Windows-standardvalet. En otillgänglig sparad ändpunkt ger ett fel i stället för att styra en annan enhet. Uppdatera enheter läser in anslutna enheter på nytt.

OBS Studio använder WebSocket v5: starta/stoppa/växla/pausa/återuppta inspelning; starta/stoppa/växla strömning; byta scener; växla ljudavstängning för indata. Aktivera WebSocket-servern under Verktyg i OBS och ange ändpunkten i redigeraren. Kontrollera anslutning och Läs in namn läser OBS-version, scener och indata utan att starta inspelning eller strömning. Välj namn i listan eller skriv dem manuellt. Lösenord kan sparas i Windows Credential Manager och utesluts från exporterade konfigurationer. Otillgängliga OBS-anslutningar rapporterar ett fel.

Profiler kan matcha ett processnamn och en del av en fönstertitel. Båda angivna villkoren måste matcha. Lås profil förhindrar automatiska byten under sessionen. Fånga villkor från förgrunden använder det senaste fokuserade externa programmet.

Text- och urklippsåtgärder stöder `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` och `{{user}}`. Datum använder `yyyy-MM-dd`; tider använder `HH:mm`. Okända platshållare förblir oförändrade. `{{profile}}` identifierar den startade åtgärdens profil; `{{process}}` identifierar den externa programkontexten. Urklippsinnehåll läses bara när den platshållaren används. Urklippsåtgärden skriver text men klistrar inte in automatiskt.

Fönsteråtgärder minimerar, maximerar, återställer eller placerar ett fönster till vänster/höger i skärmens arbetsyta. Systemåtgärden låser Windows. Dessa åtgärder körs bara när de uttryckligen anropas.

Förhandsvisning visar steg, upprepningar och variabler utan körning. Ångra och Gör om behåller upp till 100 utkasttillstånd; textrutor behåller sin inbyggda Ctrl+Z-redigering. Markera favoriter i redigeraren eller panelmenyn och filtrera dem på instrumentpanelen. Körningsantal gäller bara den aktuella sessionen.

Text-, kortkommando- och fönsteråtgärder som startas från gränssnittet aktiverar det senaste externa målfönstret. Paletten visar den målkontexten. Om målet har stängts eller Windows nekar aktivering stoppas körningen med ett meddelande.

## Keyboard support och feedback

Specialtangentguiden finns på tangentbordssidan och i paletten. Den listar G-tangenter som rapporteras av iCUE. Upptäck tangenttryckning fångar dem tillfälligt utan att köra deras tilldelade åtgärder. Normala bindningar återställs efter upptäckt, avbrott eller stängning. En katalogfunktion och vald tangent öppnas som ett osparat utkast; dubbla bindningar kontrolleras.

Tangentbordsöverlägget visar vald referenslayout och aktiva bindningar. Håll Ctrl+Alt+F1 nere eller växla det från paletten eller tangentbordssidan. Det tar inte fokus och låter musklick passera igenom. Profil- och bindningsändringar uppdateras medan det är synligt; vanliga Windows-tangenttryckningar markeras. Tillverkarspecifika kontroller ger kanske ingen motsvarande Windows-tangentsignal.

Körningsåterkoppling visar väntan, steg-/upprepningsförlopp, slutförande, avbrott eller fel utan att ta fokus eller visa inmatad text. Kommandon utförda bekräftar kommandokörning, inte ett oberoende avläst livetillstånd i ett tredjepartsprogram. Feldetaljer visas i aktivitetsloggen.

## Anläggningar och Corsair

Corsair G1–G6 kan tilldelas direkt via den officiella iCUE SDK. Välj enhet och tangent i åtgärdsredigeraren och spara, eller förbered ett utkast från K100-tangentbordsvyn. Bindningar gäller vald enhet och aktiv profil; paus inaktiverar dem också. iCUE måste köras och tillåta åtkomst. Fn, iCUE-hjulet samt profil- och låskontroller kan inte mappas via denna G-key API.

Enheten listar Windows tangentbordets gränssnitt och uppdateras periodiskt och när enheten ansluter sig. En fysisk tangentbord kan exponera flera HID-gränssnitt. Standard globala hotkeys arbetar över tillverkare.

Corsair-detektering använder tillgänglig enhetsinformation. RGB-styrning kräver kompatibel hårdvara, körande iCUE och den officiella iCUE SDK; appen rapporterar faktisk tillgänglighet. Andra tillverkare får för närvarande detektering och standardåtgärder. Utanför Corsair G-key-integration fungerar proprietära kontroller när Windows tar emot dem som vanlig inmatning. Appen installerar ingen tangentbordsdrivrutin.

## Språk

Inställningar erbjuder 27 språk: tyska, engelska, franska, spanska, italienska, portugisiska, nederländska, polska, tjeckiska, slovakiska, ungerska, rumänska, bulgariska, grekiska, danska, svenska, norska, finska, estniska, lettiska, litauiska, slovenska, kroatiska, ukrainska, ryska, turkiska och serbiska med latinsk skrift. Automatiskt följer Windows-visningsspråket. Översättningar medföljer lokalt. Layouter, kortkommandon och användarskapade namn är oberoende av gränssnittsspråket.

Fullständig arkivdokumentation finns tillgänglig på alla 27 ansökningsspråk via [Språkindex](README.md). Översättningar är maskinassisterade med originalkällslänkar och strukturkontroller; modersmålsgranskning sker separat. Appen accepterar även de regionala koderna `en-US`, `de-DE`, `fr-FR` och `it-IT` och använder deras talformat. Befintliga språkkoder förblir kompatibla.

## Layout och tillverkare forskning

Tangentbordsvyn erbjuder ISO-DE/ANSI-US-referenser i full storlek och utan numerisk del samt Corsair K100 RGB-layouter. Geometrin visar positioner, breda/höga tangenter och ISO Enter. Standardlayouter är generiska referenser. K100-layouter följer dokumenterade kontroller utan att hävda millimeterexakta mått. Välj rätt språk-/geometrivariant: ett modellnamn eller USB VID/PID fastställer ingen specifik variant.

Mappning bestämmer fysiska skanningskoder med den aktiva Windows-inmatningslayouten. Arbetsflödet förbereder ett utkast och behåller spar-/konfliktkontroller. Standardbindningar är globala Windows-kortkommandon, inte isolerade till ett anslutet tangentbord. Corsair G1–G6 använder enhetsspecifik iCUE-integration. Andra proprietära kontroller kräver en adapter eller tillverkarkonfigurerade kortkommandon. Huvud-Enter och numeriska Enter delar virtuell tangentidentitet för globala kortkommandon.

och den [Tillverkare forskning](research/tastaturhersteller-sonderfunktionen.md) och [CSV](research/tastaturhersteller-sonderfunktionen.csv) omfattar 74 varumärkes-/tillverkargrupper och skiljer mellan konfiguratorer, inmatnings-SDK:er och belysnings-API:er. [Layout bevis](research/keyboard-layout-evidence.md) dokumenterar skillnader i modell, SKU och moduler. Forskningstäckning betyder inte att hårdvarustöd är implementerat.

## Data är

Konfiguration lagras i lokala programdata. Import/export använder JSON. Import förhandsvisar konfigurationen, ersätter aktuell konfiguration efter bekräftelse och inaktiverar importerade bindningar tills de aktiveras manuellt. Textåtgärdsinnehåll samlas inte in som en tangenttryckningslogg. Credential Manager-lösenord är separata från exporterade profiler.

## Verifikation

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows-kontroller kan granska hårdvara och tillfälligt konfigurera/frigöra iCUE-bindningar som stöds. Kör dem utan en annan styrenhet som använder dessa bindningar. Kontroller av ljudändpunkter ändrar inte aktuell volym eller ljudavstängning.

Native inmatning kan inte kringgå Windows-behörigheter. En normalt startad styrenhet kanske inte kan skicka inmatning till ett administratörsprogram. Reserverade eller upptagna kortkommandon visas som konflikter.

och den [Dokumentation Index](README.md) länkar historiska designar, planer, forskning och datum testrapporter. Historiska planer beskriver deras registrerade omfattning, inte en aktuell garanti för funktionstöd.

### Arbetsflödesmallar och målfönster väntar

Katalogen innehåller sex redigerbara arbetsflöden: arbetsplats, möte, strömning, forskning, projektbyte och textfragment. Sök efter `workflow.` i katalogen eller kommandopaletten. Att välja ett arbetsflöde öppnar ett osparat utkast; det körs aldrig direkt. Alla steg behålls när ett arbetsflöde läggs till i ett befintligt makro eller tilldelas en Corsair-tangent. Konfigurera exempel-URL:er, programsökvägar, profiler och OBS-scener innan du sparar.

Steget **Vänta på målfönster** accepterar ett processnamn (med eller utan `.exe`), en skiftlägesokänslig del av titeln och en tidsgräns på 1–600000 ms. Minst ett filter krävs; båda måste matcha om båda anges. Makrot fortsätter först när ett matchande synligt fönster svarar och kommer i förgrunden. Använd en specifik titel när flera fönster delar en process. Avbrott eller timeout stoppar följande steg. Ett redo fönster garanterar inte att ett dokument har laddats eller att en extern tjänst som OBS WebSocket är ansluten.

Skärmljusstyrka: sök efter **Öka skärmljusstyrkan**, **Minska skärmljusstyrkan** eller **Skärmljusstyrka på 50 procent** i åtgärdskatalogen. Relativa åtgärder ändrar ljusstyrkan med 10 procentenheter; fasta värden går från 0 till 100. Interna skärmar som stöds använder WMI; kompatibla externa skärmar kräver DDC/CI aktiverat i sin meny. Som standard påverkar åtgärden alla skärmar som stöds; en viss skärm kan väljas i redigeraren. Anslutningar och dockor kan begränsa stödet.

### Utökade kontroller

Öppna kommandopaletten med **Ctrl+K när appfönstret har fokus**. Sök efter **Arbetsflödesdesigner**, **Spela in makro**, **Synkronisera profiler** eller **Tangentdiagnostik** (ordalydelsen följer valt språk). Arbetsflödesdesignern redigerar valt panelutkast: fil-/process-/fönstervillkor, antal steg att hoppa framåt och stoppa/fortsätt vid fel. Inspelaren fångar kortkommandokombinationer och väntetider, inte vanlig textinmatning; Escape stoppar inspelningen. Granska resultatet innan du tar över det.

I ett ljussteg väljer du alla stödda skärmar eller en specifik skärm. En otillgänglig markerad bildskärm producerar ett fel istället för att tyst ändra en annan skärm. Dashboardet visar levande ljusstyrka, ljud och OBS läsningar när deras respektive integrationer är tillgängliga; en otillgänglig läsning presenteras inte som ett mätvärde.

Tilldela en fysisk specialtangent som stöds i panelredigeraren och välj omedelbart tryck, kort tryck, långt tryck eller dubbeltryck. Lagerväljare aktiverar tillfälligt en annan mappning medan de hålls nere och återställer föregående lager när de släpps. Gester och lager gäller Corsair SDK G-tangenter och enhetsspecifika standardspecialtangenter, inte vanliga globala kortkommandon. Standardtangentfångst stöder F13–F24 och medietangenter. Proprietära Fn-, profil- och vridreglage kräver fortfarande en kompatibel leverantörsadapter eller leverantörsmappning till en standardtangent. Raw Input observerar tangenten och undertrycker inte dess normala Windows-beteende. Tangentdiagnostik förklarar profil-, paus-, anslutnings- och bindningsproblem.

Profilsynkronisering utbyter filer via en mapp du väljer, även en mapp som hanteras av din befintliga molnklient. Granska förhandsvisningen före import; importer börjar inaktiverade och tar bort hårdvaruspecifika mappningar. Revisioner förhindrar tysta överskrivningar, och åtgärdsinnehåll tas med endast efter uttryckligt val. MAKRIVO tillhandahåller inget molnkonto och behåller inga molnuppgifter för autentisering. Meddelandefältet använder nu MAKRIVOs egen blågröna M-ikon och visar ett bärnstensfärgat pausmärke vid paus.

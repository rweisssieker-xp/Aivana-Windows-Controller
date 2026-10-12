<!-- generated-doc locale=sv-SE source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../HANDBOOK.md)

# MAKRIVO — Användarhandbok

tillverkare: **Aivana**. Dokumentation snapshot: **2026-10-12**. Den här handboken beskriver den aktuella arbets-träd implementeringen. Experimentella integrationer och hårdvaruberoende funktioner identifieras uttryckligen. Historiska rapporter beskriver deras datum snapshot, inte en garanti för en ny byggnad.

## Dokumentation och språk

Använd dokumentationsspråket för att öppna den fullständiga handboken, funktionsinventören, problemlösningen, utvecklingsanmärkningar, forskning och historiska testrapporter i någon av 27 ansökningsspråk. Genererade översättningar är maskinassistenterade och har strukturella kontroller; de har inte alla fått en inhemska översikt. Tekniska identifierare och verkställbara exempel förblir oförändrade. Den ursprungliga källan är tillgänglig för jämförelse. Översatt licensanalys är informativ och ersätter inte den ursprungliga tredje parts lagliga villkor eller en slutlig EULA.

## Installation och första lanseringen

Det första målet är Windows 11 x64.Håll alla filer och underdirektiv tillsammans. `MAKRIVO.exe`. En källcheckning är inte en installatör. Självinnehållande paket inkluderar sin .NET-körtid; ramberoende paket kräver lämplig .NET 10 Desktop och ASP.NET. ONNX och sällskapshost måste inkluderas för sina respektive egenskaper.

1. Starta MAKRIVO och välj gränssnittsspråket i Inställningar. Automatiskt följer Windows Keyboard layout är en separat inställning.
2. Välj en profil och lägg sedan till ett tag genom den sökbara åtgärdskatalogen. Ge det ett erkänt namn och välj dess handlingar.
3. Övervaka stegen och tilldela en korthet om det behövs. Undvik kombinationer som redan används av Windows eller din målansökan.
4. Rädda utkastet Obesparade förändringar är inte den sparade konfigurationen.
5. Testa med en ofarlig åtgärd, som att öppna ett dokument, och inspektera genomförandet feedback.

Stängning av huvudfönstret lämnar vanligtvis ansökan i meddelandeområdet. Dubbelklicka på ikonen för att öppna den igen, eller använd menyn för att komma ut. Börja med Windows är valfritt och initialt inaktiverat. Pause-kontrollen suspenderar bindningar; ikonen visar en paus. Avbryt körande makron via UI, tray eller konfigurerad avbokning; standard är `Ctrl+Alt+Escape`.

## Tillar, åtgärder och kommandospalet

Tilar tillhör profiler och kan innehålla flera beställda steg. Redaktören stöder Save/Discard, Preview, undo/redo och steg omordnande. Katalogen levererar förinställningar för program, dokument, URL-adresser, text, klippor, kortfattningar, media, ljud, fönster, systemåtgärder och stödda integrationer. En förinställning som kräver parametrar öppnas som ett utkast.

Öppna paletten med `Ctrl+K` medan MAKRIVO har fokus. Sök sparade åtgärder, förinställningar, profilering och verktyg. Välj Strömnycklar; Ange aktiverar; `Ctrl+Enter` redigerar ett stöddat objekt; Escape stänger. Vissa kommandon som är redo att använda utförs omedelbart, medan konfigurerade åtgärder kräver granskning. En separat global palett kan konfigureras; nya konfigurationer används `Ctrl+Shift+Space`.

Text och clipboard steg stöd `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` och `{{user}}`. Datumformat är `yyyy-MM-dd`; Tiden för formatering är `HH:mm`. Okända variabler förblir oförändrade.Clipboard läsning sker endast när begäran.En clipboard handling skriver text utan att automatiskt passa den.

Handlingar som lanseras från UI kan rikta sig mot den sista externa appen. Om målet försvinner eller Windows blockerar förgrunden aktiverar du manuellt målet och vänds tillbaka. En framgångsrik nyckelleverans visar inte att målet har slutförts.

## Profiler och arbetsplatser

Profiler kan matcha ett processnamn och en del av ett fönstertitel. När båda är uppställda måste båda matcha. Profilen ordnar tävlande matcher; standardprofilen är fallback. Automatisk urval kan göras uttryckligen. Lock Profile förhindrar automatiska ändringar för den aktuella sessionen.

Öppna arbetsplatsinställningar från verktyg eller palett. Välj program, dokument, webbplatser, ett ljud/skärmmål eller en OBS scen. Assistenten förbereder arbetsplatser, möten, streaming, forskning eller projektprofiler. Kontrollera de genererade stegen innan du sparar. Inställningen utför inte de förberedda åtgärderna.

## Macros, villkor och inspelning

Macros utför sina steg i ordning och stöder väntetider, upprepningar och avbokningar. Den allmänna upprepningsgränsen är 100. Använd Preview innan du kör åtgärder som skrivs in i en annan applikation eller ändrar systemstatus.

Väntar på Target Window accepterar en process, en titel substring och en timout från 1 till 600,000 ms. Minst ett filter behövs. Ett matchande synligt fönster måste reagera och bli föregående innan de följande stegen fortsätter. Ett färdigt fönster visar inte att ett dokument eller en tjänst har slutfört laddningen.

Arbetsflödesredaktören lägger till fil-existerar, process-körning och fönster-titel villkor. Ett felaktigt tillstånd skapar steget och kan gå vidare. Förneka villkoret att uttrycka en alternativ gren. Välj stop-on-error eller continue-on-errör medvetet; fortsättningen kan lämna senare steg utan deras förväntade förutsättningar.

Aktionsregistret fångar kortskärmar och väntar, inte vanlig skrivning. Det stöder modifierar korthet och funktionella nycklar. Escape stoppar inspelningen. Övervaka listan och anta den som en oavslagen tile-design, spara sedan uttryckligen. UI-klicka är ett separat verktyg som beskrivs nedan.

## Nyckelbord, gesturer och lager

Den tangentbordssidan listar upptäckta gränssnitt och referens layouter. Ett enda fysiskt tangentbord kan exponera flera gränssnitt. Välj rätt språk och geometri; en produktnamn ensam identifierar inte ISO eller ANSI layout.

Standard globala hotkeys tillämpas över tangentbord. Raw Input binder observerar en viss enhet och skanna kod, inklusive stödjande funktion och media nycklar. **Raw Input Det undertrycker inte det vanliga Windows En exklusiv andra tangentbord är inte klar för produktion.** En undertecknad, licensierad och testad filter-drivers backend krävs fortfarande; ingen installeras av MAKRIVO.

Stödda speciella nyckelbindningar kan använda omedelbar tryck, tryck, håll eller dubbel tryck. Held-layer nycklar tillfälligt välja en annan mappning och återställa basskiktet på släpp; lager-selektornycklar kör inte sin egen makro. 150 till 5,000 MS; dubbel-tap fönster från 150 till 1,000 MS. Dessa mappningar skiljer sig från vanliga globala hotkeys.

Den specialnyckeltrådaren upptäcker kompatibla nycklar utan att utföra sina åtgärder under fången. Diagnostik förklarar otillgängliga enheter, pausade bindningar, missförhållanden och konflikter. Överlag visar den valda layouten och bindningarna; dess standardhållning är `Ctrl+Alt+F1`.

| Integration | Krav och begränsningar |
| --- | --- |
| Corsair | Running iCUE, SDK-åtkomst och en kompatibel modell. G1–G6 och stödda RGB-funktioner; proprietära Fn, hjul, lås och profilkontroll är vanligtvis inte exponerade av G-nyckeln API. |
| Logitech G | Original x64 G-key/LED SDK bibliotek kräver uttrycklig godkännande. G-nyckel händelser identifierar inte unikt flera identiska fysiska tangentbord. |
| Razer | Chroma RGB och standardinspelning; kartan ägda kontroller i Synapse där det behövs. |
| SteelSeries | GameSense RGB och standardinträde; kartan äganderättskontroller i GG där det behövs. |
| Wooting | Kompatibel original x64 SDK för analoga värden och tröskelvärden; normal HID-intag förblir aktiv. |
| Andra nyckelbord | Standard hotkeys eller stödda fysiska skannekoder. Fn och proprietära kontroller beror på modellen och leverantörskonfigurationen. |

Tillverkaren forskning beskriver dokumenterade kapaciteter, inte implementerad eller testad kompatibilitet med varje listad enhet. Nya adaptrar behöver fortfarande riktig hårdvara. Ladda inte SDK-binarier från osäkra källor; SDK-distributionskrav är separerade från teknisk kompatibilitet.

## ljud, skärmar och OBS

Ljudsteg ställer in volym från 0 till 100 procent och mute/unmute/toggle stöds utgång eller mikrofon slutpunkter. Välj ett specifikt slutpunkt eller använd Windows standard. Ett saknat mål ger ett fel istället för att tyst kontrollera en annan enhet. Upptäck utrustning efter återkoppling. Endpoint-kontroll är inte ett krav på oberoende mixer stöd.

Visa åtgärder öka / minska ljusstyrkan med 10 procentenheter eller ange ett värde från 0 till 100. Interiörerna använder stödda WMI-gränssnitt; externa monitörer kräver DDC/CI support och vanligtvis dess monitorns menyinställning. Dock, adapter och kablar kan blockera kontroll. Välj alla stödda skärmar eller en skärm. Återkoppling kan ändra tillgängliga identiteter; granska det sparade målet. Saknade läsningar visas som otillgängliga snarare än mätbara värden.

OBS kräver WebSocket v5 aktiverad i OBS Verktyg. Konfigurera slutpunkten, testa anslutningen och ladda scenen/inputnamnen innan du sparar. Stödda åtgärder inkluderar scenväxling, ström/registrering start/stop/toggle, inspelning av paus/resum och input mute. Passord kan lagras i Windows Credential Manager och är uteslutna från konfigurationsexporter. En framgångsrik OBS kommandot ställer inte upp varje nedströmseffekt.

## Resultatkontroll och verkställighetshistoria

Möjlighet att kontrollera resultatet för lämpliga steg. Appen skiljer förskjutningskontroller, bekräftade effekter, misslyckanden, avbokningar och effekter som inte kan verifieras. Processens existens är inte tillämpningsberedskap. Relativa ändringar, korta skivor och textinspelningar kan inte få en framgångsrik ansökan. Kontrollera aktivitetens logg och verifieringshistoria när en åtgärd misslyckas. Konfigureringsfel bör korrigeras innan den fullständiga makron.

## UI automatisering och klick inspelning

Lägg till ett UI Automation-steg, välj operationen och fånga målet under indikatorn efter den visade förseningen. fången ensam utför ingenting. Kontrollera markeringen och kräver en unik match. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` och `WaitState`. Passordfält, inaktiverade kontroller och ambiguella markeringar avvisas.

UI-automation körs i en begränsad följeslagare. Det beror på stödet för tillgänglighet och tillståndet för målet Windows. Klickregistret fångar stödda åtgärder och pauser; det registrerar inte allmän textinmatning. Se över och validera dess utkast innan den antas. UI-ändringar i en extern app kan inaktivera selektorer. Processisolering begränsar kollisioner och avbokning, men är inte en säkerhetssandbox.

## Plugin

Öppna förlängningar för att kontrollera ett paket innan du importerar det. Förpackningsstruktur, manifest och hashes kontrolleras. Importering börjar med funktionsnedsättning. Läs de deklarerade åtgärderna och uttryckligen lita på utgivaren innan den gör det möjligt. Plugins körs med dina användarbehörigheter i en separat process; den processen är **och inte en sandbox**. Installera bara paket som du litar på.

Hantera export, inaktivering, avinstallation och återställning av en tidigare version. Försvunna beroende blockerar inbjudan. Exempelpaketet ger räkningar och lokala åtgärder. Det stödda schemat är en dokumenterad JSON-Schema-subset, inte alla möjliga JSON-schema-funktioner. Det finns inga offentliga plugins.

## Mobilpanel

1. Öppna Mobile Panel, välj en lokal IP och välj uttryckligen de profiler som ska exponeras. Den standard loopback adressen är endast tillgänglig på datorn själv; Välj lämplig LAN-adress för en telefon eller surfplatta.
2. Starta panelen och läs dess HTTPS-adress, certifiera fingeravtrycket och enbart använda sex-digit parande kod.
3. På en enhet i samma pålitliga nätverk, öppna adressen. Jämför SHA-256 fingeravtryck via en pålitlig kanal innan du litar på det lokala certifikatet. Appen installerar inte ett tillförlitligt certifikat automatiskt.
4. Parera med den visade koden och testa en ofarlig tråd. En manuell Windows Firewall-tillstånd kan behövas för LAN-åtkomst. Låt inte porten exponeras för internet.
5. Stanna panelen när den är klar eller återkalla betalda kunder via appen.

Privat servernycklar är skyddade med Windows DPAPI; kundkrediter lagras som hashar. Att ändra IP eller förnya certifikatet kan kräva förnyad förtroende. Panelen visar åtgärd och verifieringsstatus, inte alla möjliga levande sensorer. Webbläsarens layout och lokala HTTPS-check ersätter inte den verkliga telefon/tablett LAN-acceptionen.

## Lokala mötesassistenter

Denna valfria experimentella assistent är inaktiverad av standard. Öppna den från Verktyg, aktivera det uttryckligen, välj en körtid/modell och lägg till bara de resurser du vill att den ska övervägas. Ange målet, skapa ett utkast, inspektera oupplösade förfrågningar och överföra resultatet till den normala redaktören. Inget utförs automatiskt; spara och köra är uttryckliga åtgärder. Det finns ingen automatisk fallback.

| Runttid | Setup |
| --- | --- |
| Ollama | Separerad lokal service och en lokalt installerad modell på `127.0.0.1:11434`. De deklarerade fjärr-/cloudmodellerna avvisas. |
| LM Studio | Lokala servrar med en lokalt laddad modell och JSON-schema stöd vid `127.0.0.1:1234`. Tillåta inte fjärrruttering för lokal bearbetning. |
| llama.cpp | Lokal `llama-server` och kompatibel modell med JSON-schema stöd vid `127.0.0.1:8080`. |
| ONNX körtid GenAI CPU | Tillförlitlig lokal GenAI textmodell mapp; välj `genai_config.json`. Inference använder en kopplad partnerprocess utan HTTP. |

HTTP-klienter använder endast loopback, använder inte proxy och följer inte omdirigeringar. MAKRIVO kan inte på ett tillförlitligt sätt fastställa om en kompatibel lokal server internt ringer en fjärrtjänst. De autentiserade lokala servrarna är inte konfigurerade i den här versionen. Modellvikter, Python och lokala inferensserver laddas inte ner eller installeras automatiskt.

ONNX Paketet behövs `tokenizer.json` och en giltig chattmall. stöds templateformulär i `tokenizer_config.json` är en sträng `chat_template`, En ordbok med en sträng `default`, eller en obestridlig namngivna/default malllista. om mallet saknas eller noll, `chat_template.jinja` Används som en fallback. tomma, tvivelaktiga, överskottade eller felaktigt tryckta mallar avvisas. ONNX filer, inbyggda/visuella modeller och GPU-enbart paket är inte utbytbara med kompatibla GenAI Textmodeller. den bundna GenAI Runtime versionen är 0.17.1, Använd endast tillförlitliga modeller; process separation är inte en sandbox.

Assistenten stöder valda program utan argument, valda dokument och webbplatser, och mikrofonmugg när det uttryckligen begärs. Det skapar inte meddelanden, möten, frivilliga skript, gratis klick, nedladdningar eller headset växling. Mål och resursnamn skickas till den valda lokala löptiden; resursvägar och filinnehåll levereras inte separat. Ett mål som användaren själv anger kan innehålla personuppgifter. Ett lokalt nätverksförhållande förblir användarens ansvar.

Den ONNX begäran budget är 90 sekunder inklusive modell laddning, upp till 3,072 prompt tokens och 1,024 genererade tokens, också begränsat av modell sammanhang. Förfalskade/förstörda JSON och okända källkoder avvisas. Annullerar eller närmar sig en begäran. Ett slumpmässigt test av körtid fastställer genomförandet, inte användbar planeringskvalitet eller tidsbesparingar. Lokala experimentloggar innehåller händelseidentifierare och tidpunkter, inte måltext eller dokumentinnehåll; de laddas inte automatiskt upp.

Den senare lilla utvärderingen av Instruct-modellen producerade noll giltiga planer i fyra tyska/engelska prövningar. Native inference fungerade, men modellen misslyckades med det strikta planeringsavtalet. Detta är ett negativt resultat, inte en rekommenderad modell eller en bevisad användbar assistent. Se den daterade lokala testrapporten för exakt modellrevision och gränser.

## Import, export, synkronisering och backup

Övervaka importförutsättningar innan du ersätter en konfiguration. Importerade åtgärder börjar inaktiveras, och bärbar synkronisering tar bort hårdvara-specifika bindningar. Återuppta enheter och möjliggöra åtgärder avsiktligt efter inspektion. Övervaka text/åtgärd innan du delar en export; uteslutande av lösenord gör inte alla användarskapade åtgärdsinnehåll icke känsliga.

Profilsynkronisering utbyter filer i en markerad mapp, alternativt en som hanteras av din befintliga molnklient. MAKRIVO levererar ingen molnkonto och lagrar inga molnkrediteringar. Revisionen förhindrar tyst överskrivning. Inkludera åtgärdsinnehåll är ett uttryckligt val. Backup din konfiguration före en ersättning import. Standardkonfigurationen är `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; det historiska katalognamnet behålls för kompatibilitet. Stäng appen innan du kopierar / återställer den manuellt. Förvarade SDK-godkännanden och OS-skyddade certifikat kan behöva återställas på en annan dator.

## Problemlösning

| Symtom | Vad att kontrollera |
| --- | --- |
| Shortcut gör ingenting | App körs, bindningar inte avbruten, korrekt aktiv profil, sparat/aktiverat tile, kortskärmkonflikter och tillgängliga mål. |
| Målfönstret kan inte aktiveras | Manuellt aktivera det. Windows förgränser och privilegier kan blockera automatisering. |
| Speciella nycklar har inte upptäckts | Leverantörstjänst/SDK/modellstöd; konfigurera proprietära kontroller för att utfärda en standardnyckel som stöds om möjligt. |
| Andra tangentbordet också typ normalt | förväntas med Raw Input. Exklusiv suppression tillämpas inte för produktion. |
| Ljud eller ljus är inte tillgängliga | Uppdatera enheter; kontrollera valda identiteter, slutpunktsessioner, WMI/DDC/CI support och dock/kabelbegränsningar. |
| OBS offline | WebSocket v5 aktiverad, korrekt slutpunkt/passord, tillgänglig service och giltiga plats/inmatning namn. |
| Automation misslyckas | Companion-host ingår, unik giltig markering, tillgänglig aktiverad mål, stödjande kontroller och tillstånd. Returnerar en övergångstid endast efter att ha granskat målet. |
| Mobilpanel otillgänglig | LAN istället för loopbackadress, samma nätverk, server körning, godkända profiler, certifiering fingeravtryck/trust och manuella brandväggar tillstånd. |
| Plugin misslyckas | Paketintegritet, uttrycklig tillit/tillåtna tillstånd, beroende, protokollkompatibilitet och verkställighetsregister. |
| Förfrågan misslyckas | Korrekt utvalda lokala körtider, service/modell klar, stödjande JSON-schema, tillförlitlig kompatibel mapp och begäran gränser. |
| Layout eller text verkar fel | Separera UI-språket från tangentbordets geometri; återskapa fönstret, kontrollera skärmskalan och rapportera exakt sida/språk/byggnad. |

För en användbar felrapport inkluderar Windows version, app build/commit, språk, skärmskalning, hårdvarumodell, en minimal sekvens och den relevanta redigerade loggen. Ta bort personliga vägar, åtgärdsinnehåll och bekräftelser före publicering.

## Bygg, test och förpackning

Använd SDK som är kopplat till `global.json`. Från repository root:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integrationstester kan öppna isolerade fönster och tillfälligt konfigurera/lämna stödda leverantörsband. Kör dem utan en annan kontrollerare som äger dessa bindningar. De standard ljusstyrka/audio-proverna är bara läsbara; uttryckliga hårdvaru-runda tester är separata. Mockprotokoll, renderade skärmdumpar och slumpmässiga inferensgrafik ska inte representeras som verklig hårdvara/modellkvalitet.

Den kompletta utgivaren samlar medföljande värdar, mobila tillgångar, exemplet plugin, ONNX körtid och tillgängliga beroende meddelanden. Håll dina underdirektioner. En framgångsrik byggnad är inte en undertecknad installatör eller laglig godkännande. Källor och byggnader kan utvecklas separat; jämföra dokumentationen och engagera sig med den installerade versionen.

## Licens, integritet och befrielse

Den avsedda kommersiella modellen är ett engångsköp med ständig användning av den köpta större versionen; slutpriset, utrustningsbidraget och uppgraderingsvillkoren måste fastställas i de faktiska försäljningsvillkoren. Ett produktionsköp/aktiveringssystem är inte etablerat i denna handbok. Det finns inget krav på molntillskott för daglig kontroll.

Distribuera inte ägda SDK-binärer utan bevisad tillåtelse. Förvara licens och tredje parts meddelanden för varje skickad beroende, inklusive .NET och ONNX paket. Modellen har sina egna termer. Den daterade licensrevisionen förmedlar några nya beroende och är inte en clearance för dessa tillägg. Repository är offentlig och ger inte en allmän öppen källlicens.

Verklig leverantörs maskinvara, telefon/tablett LAN-verksamhet, blandad monitor/DPI-övergångar, långvarig användning, fullständig språköversikt, exklusiv drivrutinssupport och användbar AI-planering kvalitet förblir separat acceptansarbete. Aldrig ge godkännande från egen närvaro.

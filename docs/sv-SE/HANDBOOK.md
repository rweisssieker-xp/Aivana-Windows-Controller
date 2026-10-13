<!-- generated-doc locale=sv-SE source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../HANDBOOK.md)

# MAKRIVO-användarhandbok

Tillverkare: **Aivana**. Dokumentationsögonblicksbild: **2026-10-12**. Denna handbok beskriver den aktuella implementeringen i arbetsträdet. Experimentella integrationer och hårdvaruberoende funktioner anges uttryckligen. Historiska rapporter beskriver sin daterade ögonblicksbild, inte en garanti för ett nyare bygge.

## Dokumentation och språk

Använd dokumentationens språkväljare för att öppna hela manualen, funktionsöversikten, felsökningen, utvecklingsanteckningarna, forskningen och historiska testrapporter på något av programmets 27 språk. Genererade översättningar är maskinassisterade och strukturkontrollerade; inte alla har granskats av modersmålstalare. Tekniska identifierare och körbara exempel förblir oförändrade. Originalkällan finns kvar för jämförelse. Översatt licensanalys är informativ och ersätter inte ursprungliga juridiska tredjepartsvillkor eller en slutlig EULA.

## Installation och första lanseringen

Den första målplattformen är Windows 11 x64. Håll distributionens alla filer och undermappar samlade. Starta `MAKRIVO.exe`. En utcheckning av källkoden är inget installationsprogram. Självständiga paket innehåller sin .NET-körtid; ramverksberoende paket kräver lämpliga .NET 10 Desktop- och ASP.NET-körtider. ONNX och kompletterande värdprocesser måste medfölja för respektive funktion.

1. Starta MAKRIVO och välj gränssnittsspråk i Inställningar. Automatiskt följer Windows. Tangentbordslayout är en separat inställning.
2. Välj en profil och lägg till en panel via den sökbara åtgärdskatalogen. Ge den ett igenkännligt namn och välj dess åtgärder.
3. Granska stegen och tilldela ett kortkommando vid behov. Undvik kombinationer som redan används av Windows eller målprogrammet.
4. Spara utkastet. Osparade ändringar ingår inte i den sparade konfigurationen.
5. Testa med en ofarlig åtgärd, som att öppna ett dokument, och granska återkopplingen om körningen.

När huvudfönstret stängs finns programmet normalt kvar i meddelandefältet. Dubbelklicka på ikonen där för att öppna det igen, eller avsluta via ikonens meny. Start med Windows är valfritt och inledningsvis inaktiverat. Pausfunktionen pausar bindningar; ikonen visar ett pausmärke. Avbryt körande makron via gränssnittet, meddelandefältet eller den konfigurerade avbrytningsgenvägen; standarden är `Ctrl+Alt+Escape`.

## Paneler, åtgärder och kommandopaletten

Paneler tillhör profiler och kan innehålla flera ordnade steg. Redigeraren stöder spara/kassera, förhandsvisning, ångra/gör om och omordning av steg. Katalogen innehåller förinställningar för program, dokument, URL:er, text, urklipp, kortkommandon, medier, ljud, fönster, systemåtgärder och integrationer som stöds. En förinställning som kräver parametrar öppnas som ett utkast.

Öppna paletten med `Ctrl+K` när MAKRIVO har fokus. Sök efter sparade åtgärder, förinställningar, profilbyten och verktyg. Piltangenter väljer; Enter aktiverar; `Ctrl+Enter` redigerar ett objekt som stöds; Escape stänger. Vissa färdiga kommandon körs direkt, medan konfigurerade åtgärder kräver granskning. En separat global palettgenväg kan konfigureras; nya konfigurationer använder `Ctrl+Shift+Space`.

Text- och urklippssteg stöder `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` och `{{user}}`. Datumformatet är `yyyy-MM-dd`; tidsformatet är `HH:mm`. Okända variabler lämnas oförändrade. Urklipp läses bara när det begärs. En urklippsåtgärd skriver text utan att automatiskt klistra in den.

Åtgärder som startas från gränssnittet kan riktas mot det senaste externa programmet. Om målet försvinner eller Windows blockerar aktivering i förgrunden ska du aktivera målet manuellt och försöka igen. Levererade tangenttryckningar bevisar inte att målet har slutfört sin uppgift.

## Profiler och arbetsplatser

Profiler kan matcha ett processnamn och en del av en fönstertitel. När båda är angivna måste båda matcha. Profilordningen avgör vilken av konkurrerande träffar som väljs; standardprofilen är reservvalet. Aktivera automatiskt val uttryckligen. Lås profil förhindrar automatiska ändringar under den aktuella sessionen.

Öppna Arbetsplatsinställning från Verktyg eller paletten. Välj program, dokument, webbplatser, ett ljud-/skärmmål eller en OBS-scen. Assistenten förbereder arbetsplats-, mötes-, strömnings-, forsknings- eller projektprofiler. Granska de genererade stegen innan du sparar. Inställningen kör inte de förberedda åtgärderna.

## Makron, villkor och inspelning

Makron utför stegen i ordning och stöder väntetider, upprepningar och avbrott. Den allmänna upprepningsgränsen är 100. Använd förhandsvisning före åtgärder som skriver i ett annat program eller ändrar systemtillstånd.

Vänta på målfönster accepterar en process, en del av en titel och en tidsgräns från 1 till 600000 ms. Minst ett filter krävs. Ett matchande synligt fönster måste svara och komma i förgrunden innan följande steg fortsätter. Ett redo fönster bevisar inte att ett dokument eller en tjänst har laddats färdigt.

Arbetsflödesredigeraren lägger till villkor för att en fil finns, en process körs och en fönstertitel matchar. Ett falskt villkor hoppar över steget och kan hoppa över ytterligare efterföljande steg. Invertera villkoret för att ange en alternativ gren. Välj medvetet mellan att stoppa eller fortsätta vid fel; vid fortsatt körning kan senare steg sakna sina förväntade förutsättningar.

Åtgärdsinspelaren fångar kortkommandokombinationer och väntetider, inte vanlig textinmatning. Den stöder kortkommandon med modifierartangenter och funktionstangenter. Escape stoppar inspelningen. Granska listan och ta över den som ett osparat panelutkast; spara sedan uttryckligen. UI-klickinspelning är ett separat verktyg som beskrivs nedan.

## Tangentbord, gester och lager

Tangentbordssidan listar upptäckta gränssnitt och referenslayouter. Ett fysiskt tangentbord kan exponera flera gränssnitt. Välj rätt språk och geometri; ett produktnamn ensamt identifierar inte ISO- eller ANSI-layout.

Vanliga globala kortkommandon gäller för alla tangentbord. Enhetsspecifika Raw Input-bindningar observerar en viss enhet och skanningskod, inklusive funktionstangenter och medietangenter som stöds. **Raw Input undertrycker inte vanlig Windows-inmatning. Ett exklusivt andra tangentbord är inte produktionsklart.** En signerad, licensierad och testad filterdrivrutinsbackend krävs fortfarande; MAKRIVO installerar ingen.

Specialtangentbindningar som stöds kan använda omedelbart tryck, kort tryck, långt tryck eller dubbeltryck. Lagertangenter väljer tillfälligt en annan mappning medan de hålls nere och återställer baslagret när de släpps; lagerväljartangenter kör inte egna makron. Trösklar för långt tryck går från 150 till 5000 ms; tidsfönster för dubbeltryck från 150 till 1000 ms. Dessa mappningar är skilda från vanliga globala kortkommandon.

Specialtangentguiden upptäcker kompatibla tangenter utan att köra tilldelade åtgärder under fångsten. Diagnostik förklarar otillgängliga enheter, pausade bindningar, profilavvikelser och konflikter. Överlägget visar vald layout och bindningar; standardgenvägen att hålla nere är `Ctrl+Alt+F1`.

| Integration | Krav och begränsningar |
| --- | --- |
| Corsair | Körande iCUE, SDK-åtkomst och en kompatibel modell. G1–G6 och RGB-funktioner som stöds; proprietära Fn-, hjul-, lås- och profilkontroller exponeras normalt inte via G-key API. |
| Logitech G | Ursprungliga lokala x64 G-key/LED SDK-bibliotek kräver uttryckligt godkännande. G-key-händelser identifierar inte entydigt flera identiska fysiska tangentbord. |
| Razer | Chroma RGB och standardinmatning; mappa proprietära kontroller i Synapse vid behov. |
| SteelSeries | GameSense RGB och standardinmatning; mappa proprietära kontroller i GG vid behov. |
| Wooting | Kompatibel ursprunglig lokal x64 SDK för analoga värden och trösklar; vanlig HID-inmatning förblir aktiv. |
| Andra tangentbord | Standardkortkommandon eller fysiska skanningskodbindningar som stöds. Fn och proprietära kontroller beror på modell och leverantörskonfiguration. |

Tillverkarforskning beskriver dokumenterade möjligheter, inte implementerad eller testad kompatibilitet med varje listad enhet. Nya adaptrar kräver fortfarande acceptanstester med verklig hårdvara. Ladda inte ned SDK-binärfiler från opålitliga källor; SDK-distributionsrättigheter är skilda från teknisk kompatibilitet.

## ljud, skärmar och OBS

Ljudsteg ställer in volymen från 0 till 100 procent och stänger av eller sätter på ljudet eller växlar ljudavstängningen för utgångs- eller mikrofonändpunkter som stöds. Välj en viss ändpunkt eller Windows-standardvalet. Ett saknat sparat mål ger ett fel i stället för att tyst styra en annan enhet. Uppdatera enheter efter återanslutning. Ändpunktsstyrning innebär inte stöd för en oberoende mixer per program.

Skärmåtgärder höjer/sänker ljusstyrkan med 10 procentenheter eller anger ett värde från 0 till 100. Interna skärmar använder WMI-gränssnitt som stöds; externa skärmar kräver DDC/CI-stöd och vanligtvis aktivering i skärmmenyn. Dockor, adaptrar och kablar kan blockera styrningen. Välj alla skärmar som stöds eller en skärm. Återanslutning kan ändra tillgängliga identiteter; granska det sparade målet. Saknade avläsningar visas som otillgängliga i stället för uppmätta värden.

OBS kräver att WebSocket v5 aktiveras under Verktyg i OBS. Konfigurera ändpunkten, testa anslutningen och läs in scen-/indatanamn innan du sparar. Åtgärder som stöds är scenbyten, start/stopp/växling av strömning/inspelning, paus/återupptagning av inspelning och ljudavstängning för indata. Lösenord kan lagras i Windows Credential Manager och utesluts från konfigurationsexporter. Ett lyckat OBS-kommando bevisar inte alla efterföljande strömningseffekter.

## Resultatverifiering och körningshistorik

Aktivera Verifiera resultat för lämpliga steg. Appen skiljer mellan förkontroller, bekräftade effekter, fel, avbrott och effekter som inte kan verifieras. Att en process finns betyder inte att programmet är redo. Relativa ändringar, kortkommandon och textinmatning kan inte få en generell framgångsbekräftelse. Granska aktivitetsloggen och verifieringshistoriken när en åtgärd misslyckas. Rätta konfigurationsfel innan hela makrot körs igen.

## UI-automatisering och klickinspelning

Lägg till ett UI Automation-steg, välj åtgärden och fånga målet under muspekaren efter den visade fördröjningen. Enbart fångst utför ingenting. Kontrollera selektorn och kräv en entydig träff. Åtgärder som stöds är `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` och `WaitState`. Lösenordsfält, inaktiverade kontroller och tvetydiga selektorer avvisas.

UI-automatisering körs i en avgränsad hjälpprocess. Den beror på målprogrammets Windows-tillgänglighetsstöd och behörigheter. Klickinspelaren fångar åtgärder som stöds och pauser; den spelar inte in allmän textinmatning. Granska och validera utkastet innan du tar över det. UI-ändringar i en extern app kan göra selektorer ogiltiga. Processisolering begränsar krascher och avbrott men är ingen säkerhetssandbox.

## Plugin

Öppna Tillägg för att granska ett paket före import. Paketstruktur, manifest och hashvärden kontrolleras. Efter import är paketet inaktiverat. Läs de deklarerade åtgärderna och ange uttryckligen att du litar på utgivaren före aktivering. Plugins körs med dina användarbehörigheter i en separat process; processen är **ingen sandbox**. Installera bara paket som du litar på.

Hanteraren stöder export, inaktivering, avinstallation och återställning/kassering av en tidigare version. Saknade beroenden blockerar anrop. Exempelpaketet erbjuder nedräkningsåtgärder och åtgärder för lokalt tillstånd. Schemat som stöds är en dokumenterad delmängd av JSON-Schema, inte alla möjliga JSON-Schema-funktioner. Det finns ingen offentlig pluginmarknadsplats.

## Mobilpanel

1. Öppna Mobilpanel, välj en lokal IP och välj uttryckligen vilka profiler som ska exponeras. Standardadressen för loopback kan bara nås på datorn själv; välj lämplig LAN-adress för en telefon eller surfplatta.
2. Starta panelen och läs HTTPS-adressen, certifikatets fingeravtryck och den sexsiffriga engångskoden för parkoppling.
3. Öppna adressen på en enhet i samma betrodda nätverk. Jämför SHA-256-fingeravtrycket via en betrodd kanal innan du litar på det lokala certifikatet. Appen installerar inte automatiskt ett betrott certifikat.
4. Parkoppla med den visade koden och testa en ofarlig panel. Manuell Windows Firewall-behörighet kan behövas för LAN-åtkomst. Exponera inte porten på internet.
5. Stoppa panelen när du är klar eller återkalla parkopplade klienter via appen.

Privata servernycklar skyddas med Windows DPAPI; klientuppgifter lagras som hashvärden. Ändrad IP eller förnyat certifikat kan kräva förnyat förtroende. Panelen visar åtgärds- och verifieringstillstånd, inte alla möjliga livesensorer. Kontroller av webbläsarlayout och lokal HTTPS ersätter inte verklig LAN-acceptans med telefon/surfplatta.

## Lokal AI-mötesassistent

Denna valfria experimentella assistent är inaktiverad som standard. Öppna den från Verktyg, aktivera den uttryckligen, välj en körtid/modell och lägg bara till de resurser som den ska ta hänsyn till. Ange målet, skapa ett utkast, granska olösta förfrågningar och överför resultatet till den vanliga redigeraren. Ingenting körs automatiskt; att spara och köra förblir uttryckliga åtgärder. Det finns ingen automatisk reservkörtid.

| Runttid | Setup |
| --- | --- |
| Ollama | Separat lokal tjänst och en lokalt installerad modell på `127.0.0.1:11434`. Deklarerade fjärr-/molnmodeller avvisas. |
| LM Studio | Lokal server med en lokalt inläst modell och JSON-schema-stöd på `127.0.0.1:1234`. Aktivera inte fjärrdirigering för lokal bearbetning. |
| llama.cpp | Lokal `llama-server` och kompatibel modell med JSON-schema-stöd på `127.0.0.1:8080`. |
| ONNX körtid GenAI CPU | Betrodd kompatibel lokal GenAI-textmodellmapp; välj `genai_config.json`. Inferens använder en medföljande hjälpprocess utan HTTP. |

HTTP-klienter använder endast loopback, inga proxyer och följer inga omdirigeringar. MAKRIVO kan inte tillförlitligt avgöra om en kompatibel lokal server internt anropar en fjärrtjänst. Autentiserade lokala servrar kan inte konfigureras i denna version. Modellvikter, Python och lokala inferensservrar laddas inte ned eller installeras automatiskt.

ONNX-paket behöver `tokenizer.json` och en giltig chattmall. Mallformer som stöds i `tokenizer_config.json` är en sträng `chat_template`, en ordbok med en sträng `default` eller en entydig lista med namngivna/standardmallar. Om mallen saknas eller är null används `chat_template.jinja` som reserv. Tomma, tvetydiga, för stora eller feltypade mallar avvisas. Godtyckliga ONNX-filer, embedding-/visionsmodeller och paket enbart för GPU är inte utbytbara mot kompatibla GenAI-textmodeller. Den medföljande GenAI-körtidsversionen är 0.17.1, CPU. Använd endast betrodda modeller; processseparation är inte en sandbox.

Assistenten stöder valda program utan argument, valda dokument och webbplatser samt mikrofonavstängning när det uttryckligen begärs. Den skapar inte meddelanden, möten, godtyckliga skript, fria klick eller nedladdningar och byter inte headset. Målet och resursnamnen skickas till den valda lokala körtiden; resurssökvägar och filinnehåll lämnas inte separat. Ett mål som användaren skriver kan i sig innehålla personuppgifter. En lokal körtids eget nätverksbeteende förblir användarens ansvar.

ONNX-begärans budget är 90 sekunder inklusive modellinläsning, högst 3072 prompttoken och 1024 genererade token, även begränsat av modellens kontext. Felaktig/avkortad JSON och okända resurs-ID:n avvisas. Avbryt eller stäng för att avsluta en begäran. Ett körtidstest med en slumpmodell visar att körningen fungerar, inte användbar planeringskvalitet eller tidsbesparing. Lokala experimentloggar innehåller händelse-ID:n och tidsmätningar, inte måltext eller dokumentinnehåll; de laddas inte upp automatiskt.

Den senare utvärderingen av en liten Instruct-modell gav noll giltiga planer i fyra tyska/engelska försök. Native inferens fungerade, men modellen uppfyllde inte det strikta planeringskontraktet. Detta är ett negativt kvalitetsresultat, ingen rekommenderad modell eller bevisat användbar assistent. Se den daterade testrapporten om lokala körtider för exakt modellrevision och begränsningar.

## Import, export, synkronisering och backup

Granska importförhandsvisningar innan du ersätter en konfiguration. Importerade åtgärder är inaktiverade från början och portabel synkronisering tar bort hårdvaruspecifika bindningar. Tilldela enheter på nytt och aktivera åtgärder medvetet efter granskning. Granska text- och åtgärdsinnehåll innan du delar en export; att lösenord utesluts gör inte allt användarskapat åtgärdsinnehåll okänsligt.

Profilsynkronisering utbyter filer i en vald mapp, eventuellt hanterad av din befintliga molnklient. MAKRIVO tillhandahåller inget molnkonto och lagrar inga molnuppgifter för autentisering. Revisioner förhindrar tysta överskrivningar. Att ta med åtgärdsinnehåll är ett uttryckligt val. Säkerhetskopiera konfigurationen före en ersättande import. Standardkonfigurationen är `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; det historiska mappnamnet behålls för kompatibilitet. Stäng appen före manuell kopiering/återställning. Sparade SDK-godkännanden och autentiseringsuppgifter skyddade av operativsystemet kan behöva återskapas på en annan dator.

## Problemlösning

| Symtom | Vad att kontrollera |
| --- | --- |
| Kortkommando gör ingenting | Appen körs, bindningar inte pausade, rätt aktiv profil, sparad/aktiverad panel, kortkommandokonflikter och tillgängligt mål. |
| Målfönstret kan inte aktiveras | Aktivera det manuellt. Windows-förgrundsbegränsningar och behörighetsskillnader kan blockera automatisering. |
| Specialtangent upptäcks inte | Leverantörstjänst/SDK/modellstöd; konfigurera om möjligt proprietära kontroller att avge en standardtangent som stöds. |
| Andra tangentbordet skriver också normalt | Förväntat med Raw Input. Exklusiv undertryckning är inte implementerad för produktionsbruk. |
| Ljud eller ljus är inte tillgängliga | Uppdatera enheter; granska vald identitet, ändpunktssessioner, WMI/DDC/CI-stöd och dock-/kabelbegränsningar. |
| OBS offline | WebSocket v5 aktiverad, rätt ändpunkt/lösenord, nåbar tjänst och giltiga scen-/indatanamn. |
| Automation misslyckas | Hjälpvärd medföljer, entydig giltig selektor, tillgängligt aktiverat mål, kontroll som stöds och behörigheter. Försök igen efter en tillfällig timeout först när målet har granskats. |
| Mobilpanel kan inte nås | LAN-adress i stället för loopback, samma nätverk, körande server, godkända profiler, certifikatfingeravtryck/förtroende och manuella brandväggsbehörigheter. |
| Plugin misslyckas | Paketintegritet, uttryckligt förtroende/aktiveringstillstånd, beroenden, protokollkompatibilitet och körningslogg. |
| AI-begäran misslyckas | Rätt vald lokal körtid, tjänst/modell redo, JSON-schema som stöds, betrodd kompatibel modellmapp och begärans gränser. |
| Layout eller text verkar fel | Skilj gränssnittsspråk från tangentbordsgeometri; ändra fönsterstorlek, kontrollera skärmskalningen och rapportera exakt sida/språk/bygge. |

En användbar felrapport innehåller Windows-version, appbygge/commit, språk, skärmskalning, hårdvarumodell, en minimal stegsekvens och relevant logg med känsliga uppgifter borttagna. Ta bort personliga sökvägar, åtgärdsinnehåll och autentiseringsuppgifter före publicering.

## Byggning, testning och paketering

Använd SDK-versionen som anges i `global.json`. Från kodarkivets rotmapp:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows-integrationstester kan öppna isolerade fönster och tillfälligt konfigurera/frigöra leverantörsbindningar som stöds. Kör dem utan att en annan styrenhet äger dessa bindningar. Standardprober för ljusstyrka/ljud är skrivskyddade; uttryckliga hårdvarutester med ändring och återställning är separata. Mockprotokoll, renderade skärmbilder och slumpmässiga inferensgrafer får inte framställas som verklig acceptans av hårdvara/modellkvalitet.

Det fullständiga publiceringsverktyget samlar kompletterande värdprocesser, mobilfiler, exempelplugin, ONNX-körtid och tillgängliga beroendemeddelanden. Behåll undermapparna. Ett lyckat bygge är inget signerat installationsprogram eller juridiskt utgivningsgodkännande. Källkod och byggen kan utvecklas separat; jämför dokumentationens ögonblicksbild och commit med den installerade versionen.

## Licensiering, integritet och utgivningsstatus

Den avsedda kommersiella modellen är ett engångsköp med permanent användning av den köpta huvudversionen; slutligt pris, tillåtet antal enheter och uppgraderingsvillkor måste fastställas i de faktiska försäljningsvillkoren. Denna handbok fastställer inget produktionsklart köp-/aktiveringssystem. Daglig styrning kräver inget molnkonto.

Distribuera inte proprietära SDK-binärfiler utan styrkt tillstånd. Bevara licens- och tredjepartsmeddelanden för varje medföljande beroende, inklusive .NET- och ONNX-paket. Modellvikter har egna villkor. Den daterade licensgranskningen föregår vissa nya beroenden och godkänner inte dessa tillägg. Att kodarkivet är offentligt innebär inte i sig en allmän öppen källkodslicens.

Verklig leverantörshårdvara, telefon-/surfplattebruk via LAN, övergångar mellan skärmar/DPI-inställningar, långvarig användning, fullständig språkgranskning, exklusivt drivrutinsstöd och användbar AI-planeringskvalitet kräver fortfarande separat acceptansarbete. Dra aldrig slutsatsen att något är godkänt enbart för att funktionen finns.

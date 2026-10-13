<!-- generated-doc locale=da-DK source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../HANDBOOK.md)

# MAKRIVO-brugerhåndbog

Producent: **Aivana**. Dokumentationssnapshot: **2026-10-12**. Denne håndbog beskriver den aktuelle implementering i arbejdstræet. Eksperimentelle integrationer og hardwareafhængige funktioner angives udtrykkeligt. Historiske rapporter beskriver deres daterede snapshot, ikke en garanti for et nyere build.

## Dokumentation og sprog

Brug dokumentationens sprogvælger til at åbne den komplette manual, funktionsoversigt, fejlfinding, udviklingsnoter, forskning og historiske testrapporter på et af programmets 27 sprog. Genererede oversættelser er maskinassisterede og strukturkontrollerede; ikke alle er gennemgået af modersmålstalende. Tekniske identifikatorer og eksekverbare eksempler forbliver uændrede. Originalkilden er fortsat tilgængelig til sammenligning. Oversat licensanalyse er vejledende og erstatter ikke originale juridiske tredjepartsvilkår eller en endelig EULA.

## Installation og første start

Det første mål er Windows 11 x64. Hold alle distributionens filer og undermapper samlet. Start `MAKRIVO.exe`. En kildekode-checkout er ikke et installationsprogram. Selvstændige pakker indeholder deres .NET-runtime; frameworkafhængige pakker kræver de relevante .NET 10 Desktop- og ASP.NET-runtimes. ONNX og ledsagende værtsprocesser skal medfølge til deres respektive funktioner.

1. Start MAKRIVO, og vælg brugerfladesproget i Indstillinger. Automatisk følger Windows. Tastaturlayout er en separat indstilling.
2. Vælg en profil, og tilføj en flise via det søgbare handlingskatalog. Giv den et genkendeligt navn, og vælg dens handlinger.
3. Gennemgå trinnene, og tildel en genvej efter behov. Undgå kombinationer, der allerede bruges af Windows eller målprogrammet.
4. Gem udkastet. Ikke-gemte ændringer indgår ikke i den gemte konfiguration.
5. Test med en harmløs handling, såsom at åbne et dokument, og undersøg tilbagemeldingen om udførelsen.

Når hovedvinduet lukkes, forbliver programmet normalt i meddelelsesområdet. Dobbeltklik på ikonet dér for at åbne det igen, eller brug ikonets menu til at afslutte. Start med Windows er valgfrit og deaktiveret fra starten. Pausefunktionen suspenderer bindinger; ikonet viser et pausemærke. Annullér kørende makroer via brugerfladen, meddelelsesområdet eller den konfigurerede annulleringsgenvej; standarden er `Ctrl+Alt+Escape`.

## Fliser, handlinger og kommandopaletten

Fliser tilhører profiler og kan indeholde flere trin i rækkefølge. Editoren understøtter gem/kassér, forhåndsvisning, fortryd/annullér fortryd og ændring af trinrækkefølge. Kataloget tilbyder forudindstillinger til programmer, dokumenter, URL’er, tekst, udklipsholder, genveje, medier, lyd, vinduer, systemhandlinger og understøttede integrationer. En forudindstilling, som kræver parametre, åbnes som et udkast.

Åbn paletten med `Ctrl+K`, mens MAKRIVO har fokus. Søg efter gemte handlinger, forudindstillinger, profilskift og værktøjer. Piletaster vælger; Enter aktiverer; `Ctrl+Enter` redigerer et understøttet element; Escape lukker. Nogle brugsklare kommandoer udføres straks, mens konfigurerede handlinger kræver gennemgang. En separat global paletgenvej kan konfigureres; nye konfigurationer bruger `Ctrl+Shift+Space`.

Tekst- og udklipsholdertrin understøtter `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` og `{{user}}`. Datoformatet er `yyyy-MM-dd`; tidsformatet er `HH:mm`. Ukendte variabler forbliver uændrede. Udklipsholderen læses kun, når det anmodes. En udklipsholderhandling skriver tekst uden automatisk at indsætte den.

Handlinger startet fra brugerfladen kan målrettes det seneste eksterne program. Hvis målet forsvinder, eller Windows blokerer aktivering i forgrunden, skal du aktivere målet manuelt og prøve igen. Vellykket levering af tastetryk beviser ikke, at målet har fuldført sin opgave.

## Profiler og arbejdspladser

Profiler kan matche et procesnavn og en del af en vinduestitel. Når begge er angivet, skal begge matche. Profilrækkefølgen afgør konkurrerende matches; standardprofilen bruges som reserve. Aktivér automatisk valg udtrykkeligt. Lås profil forhindrer automatiske ændringer i den aktuelle session.

Åbn Arbejdspladsopsætning fra Værktøjer eller paletten. Vælg programmer, dokumenter, websteder, et lyd-/skærmmål eller en OBS-scene. Assistenten forbereder arbejdsplads-, møde-, streaming-, forsknings- eller projektprofiler. Gennemgå de genererede trin før lagring. Opsætningen udfører ikke de forberedte handlinger.

## Makroer, betingelser og optagelse

Makroer udfører trinnene i rækkefølge og understøtter ventetider, gentagelser og annullering. Den generelle gentagelsesgrænse er 100. Brug forhåndsvisning før handlinger, der skriver i et andet program eller ændrer systemtilstand.

Vent på målvindue accepterer en proces, en del af en titel og en timeout fra 1 til 600000 ms. Mindst ét filter kræves. Et matchende synligt vindue skal svare og komme i forgrunden, før efterfølgende trin fortsætter. Et klart vindue beviser ikke, at et dokument eller en tjeneste er færdig med at indlæse.

Workfloweditoren tilføjer betingelser for, om en fil findes, en proces kører, og en vinduestitel matcher. En falsk betingelse springer trinnet over og kan springe yderligere efterfølgende trin over. Invertér betingelsen for at angive en alternativ gren. Vælg bevidst mellem at stoppe eller fortsætte ved fejl; fortsættelse kan efterlade senere trin uden deres forventede forudsætninger.

Handlingsoptageren registrerer genvejskombinationer og ventetider, ikke almindelig tekstindtastning. Den understøtter genveje med modifikationstaster og funktionstaster. Escape stopper optagelsen. Gennemgå listen, og overtag den som et ikke-gemt fliseudkast; gem derefter udtrykkeligt. Optagelse af UI-klik er et separat værktøj beskrevet nedenfor.

## Tastaturer, bevægelser og lag

Tastatursiden viser registrerede grænseflader og referencelayouts. Ét fysisk tastatur kan eksponere flere grænseflader. Vælg korrekt sprog og geometri; et produktnavn alene identificerer ikke ISO- eller ANSI-layout.

Almindelige globale genvejstaster gælder på tværs af tastaturer. Enhedsspecifikke Raw Input-bindinger observerer en bestemt enhed og scanningskode, herunder understøttede funktions- og medietaster. **Raw Input undertrykker ikke almindelig Windows-input. Et eksklusivt ekstra tastatur er ikke produktionsklart.** Der kræves stadig en signeret, licenseret og testet filterdriver-backend; MAKRIVO installerer ingen.

Understøttede bindinger til specialtaster kan reagere på øjeblikkeligt tryk, kort tryk, langt tryk eller dobbelttryk. Lagtaster vælger midlertidigt en anden tilknytning, mens de holdes nede, og gendanner basislaget, når de slippes; lagvælgertaster kører ikke deres egen makro. Tærskler for langt tryk går fra 150 til 5000 ms; tidsvinduer for dobbelttryk fra 150 til 1000 ms. Disse tilknytninger er adskilt fra almindelige globale genvejstaster.

Guiden til specialtaster registrerer kompatible taster uden at udføre deres tildelte handlinger under indfangning. Diagnostik forklarer utilgængelige enheder, pausede bindinger, profilmismatch og konflikter. Overlayet viser det valgte layout og bindingerne; standardgenvejen, der holdes nede, er `Ctrl+Alt+F1`.

| Integration | Krav og begrænsninger |
| --- | --- |
| Corsair | Kørende iCUE, SDK-adgang og en kompatibel model. G1–G6 og understøttede RGB-funktioner; proprietære Fn-, hjul-, låse- og profilkontroller eksponeres generelt ikke via G-key API. |
| Logitech G | Originale lokale x64 G-key/LED SDK-biblioteker kræver udtrykkelig godkendelse. G-key-hændelser identificerer ikke entydigt flere identiske fysiske tastaturer. |
| Razer | Chroma RGB og standardinput; tilknyt proprietære kontroller i Synapse efter behov. |
| SteelSeries | GameSense RGB og standardinput; tilknyt proprietære kontroller i GG efter behov. |
| Wooting | Kompatibel original lokal x64 SDK til analoge værdier og tærskler; normal HID-input forbliver aktiv. |
| Andre tastaturer | Standardgenveje eller understøttede fysiske scanningskodebindinger. Fn og proprietære kontroller afhænger af modellen og leverandørkonfigurationen. |

Producentforskning beskriver dokumenterede muligheder, ikke implementeret eller testet kompatibilitet med hver angivet enhed. Nye adaptere kræver stadig accepttest med reel hardware. Download ikke SDK-binærfiler fra upålidelige kilder; SDK-distributionsrettigheder er adskilt fra teknisk kompatibilitet.

## Lyd, skærm og OBS

Lydtrin indstiller lydstyrken fra 0 til 100 procent og slår lyden fra eller til eller skifter lydløs tilstand for understøttede udgangs- eller mikrofonendepunkter. Vælg et bestemt endepunkt eller Windows-standarden. Et manglende gemt mål giver en fejl i stedet for ubemærket at styre en anden enhed. Opdatér enheder efter gentilslutning. Endepunktstyring indebærer ikke understøttelse af en uafhængig mixer pr. program.

Skærmhandlinger øger/sænker lysstyrken med 10 procentpoint eller indstiller en værdi fra 0 til 100. Interne skærme bruger understøttede WMI-grænseflader; eksterne skærme kræver DDC/CI-understøttelse og normalt aktivering i skærmmenuen. Docks, adaptere og kabler kan blokere styringen. Vælg alle understøttede skærme eller én skærm. Gentilslutning kan ændre de tilgængelige identiteter; gennemgå det gemte mål. Manglende aflæsninger vises som utilgængelige frem for målte værdier.

OBS kræver WebSocket v5 aktiveret under Værktøjer i OBS. Konfigurér endepunktet, test forbindelsen, og indlæs scene-/inputnavne før lagring. Understøttede handlinger omfatter sceneskift, start/stop/skift af streaming/optagelse, pause/genoptagelse af optagelse og muting af input. Adgangskoder kan gemmes i Windows Credential Manager og udelades fra konfigurationseksporter. En vellykket OBS-kommando dokumenterer ikke alle efterfølgende streamingvirkninger.

## Resultatkontrol og udførelseshistorik

Aktivér Kontrollér resultat for egnede trin. Appen skelner mellem forhåndskontrol, bekræftede virkninger, fejl, annullering og virkninger, der ikke kan verificeres. At en proces findes, betyder ikke, at programmet er klar. Relative ændringer, genveje og tekstindtastning kan ikke få en generel succesbekræftelse. Undersøg aktivitetsloggen og verifikationshistorikken, når en handling mislykkes. Ret konfigurationsfejl, før hele makroen køres igen.

## UI-automatisering og klikoptagelse

Tilføj et UI Automation-trin, vælg handlingen, og indfang målet under musemarkøren efter den viste forsinkelse. Indfangning alene udfører intet. Kontrollér selektoren, og kræv et entydigt match. Understøttede handlinger omfatter `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` og `WaitState`. Adgangskodefelter, deaktiverede kontrolelementer og tvetydige selektorer afvises.

UI-automatisering kører i en afgrænset ledsagerproces. Den afhænger af målprogrammets Windows-tilgængelighedsunderstøttelse og tilladelser. Klikoptageren registrerer understøttede handlinger og pauser; den optager ikke almindelig tekstindtastning. Gennemgå og validér udkastet før overtagelse. UI-ændringer i en ekstern app kan gøre selektorer ugyldige. Procesisolering afgrænser nedbrud og annullering, men er ikke en sikkerhedssandbox.

## Plugins

Åbn Udvidelser for at undersøge en pakke før import. Pakkestruktur, manifest og hashes kontrolleres. Efter import er pakken deaktiveret. Læs de angivne handlinger, og angiv udtrykkeligt tillid til udgiveren før aktivering. Plugins kører med dine brugerrettigheder i en separat proces; processen er **ikke en sandbox**. Installér kun pakker, du stoler på.

Administratoren understøtter eksport, deaktivering, afinstallation og gendannelse/kassering af en tidligere version. Manglende afhængigheder blokerer kald. Eksempelpakken tilbyder nedtællingshandlinger og handlinger for lokal tilstand. Det understøttede skema er en dokumenteret delmængde af JSON-Schema, ikke alle mulige JSON-Schema-funktioner. Der findes ingen offentlig pluginmarkedsplads.

## Mobilpanel

1. Åbn Mobilpanel, vælg en lokal IP, og vælg udtrykkeligt de profiler, der skal eksponeres. Standard-loopbackadressen kan kun nås på selve pc’en; vælg den relevante LAN-adresse til en telefon eller tablet.
2. Start panelet, og læs HTTPS-adressen, certifikatets fingeraftryk og den sekscifrede engangsparringskode.
3. Åbn adressen på en enhed i samme betroede netværk. Sammenlign SHA-256-fingeraftrykket via en betroet kanal, før du stoler på det lokale certifikat. Appen installerer ikke automatisk et betroet certifikat.
4. Par med den viste kode, og test én harmløs flise. Manuel Windows Firewall-tilladelse kan være nødvendig til LAN-adgang. Eksponér ikke porten på internettet.
5. Stop panelet, når du er færdig, eller tilbagekald parrede klienter via appen.

Private servernøgler beskyttes med Windows DPAPI; klientlegitimationsoplysninger gemmes som hashes. Ændring af IP eller fornyelse af certifikatet kan kræve fornyet tillid. Panelet viser handlings- og verifikationstilstande, ikke alle mulige livesensorer. Kontrol af browserlayout og lokal HTTPS erstatter ikke reel LAN-accepttest med telefon/tablet.

## Lokal AI-mødeassistent

Denne valgfrie eksperimentelle assistent er deaktiveret som standard. Åbn den fra Værktøjer, aktivér den udtrykkeligt, vælg en runtime/model, og tilføj kun de ressourcer, den skal tage i betragtning. Angiv målet, opret et udkast, undersøg uafklarede anmodninger, og overfør resultatet til den normale editor. Intet udføres automatisk; lagring og kørsel forbliver udtrykkelige handlinger. Der er ingen automatisk runtime-reserve.

| Runtime | Indstillingen |
| --- | --- |
| Ollama | Separat lokal tjeneste og en lokalt installeret model på `127.0.0.1:11434`. Angivne fjern-/cloudmodeller afvises. |
| LM Studio | Lokal server med en lokalt indlæst model og JSON-schema-understøttelse på `127.0.0.1:1234`. Aktivér ikke fjernrouting til lokal behandling. |
| llama.cpp | Lokal `llama-server` og kompatibel model med JSON-schema-understøttelse på `127.0.0.1:8080`. |
| ONNX Udløbet GenAI CPU | Betroet kompatibel lokal GenAI-tekstmodelmappe; vælg `genai_config.json`. Inferens bruger en medfølgende ledsagerproces uden HTTP. |

HTTP-klienter bruger kun loopback, ingen proxyer og følger ingen omdirigeringer. MAKRIVO kan ikke pålideligt fastslå, om en kompatibel lokal server internt kalder en fjerntjeneste. Autentificerede lokale servere kan ikke konfigureres i denne version. Modelvægte, Python og lokale inferensservere downloades eller installeres ikke automatisk.

ONNX-pakker kræver `tokenizer.json` og en gyldig chatskabelon. Understøttede skabelonformer i `tokenizer_config.json` er en streng `chat_template`, en ordbog med en streng `default` eller en entydig liste over navngivne/standardskabeloner. Hvis skabelonen mangler eller er null, bruges `chat_template.jinja` som reserve. Tomme, tvetydige, for store eller forkert typede skabeloner afvises. Vilkårlige ONNX-filer, embedding-/visionsmodeller og pakker kun til GPU kan ikke udskiftes med kompatible GenAI-tekstmodeller. Den medfølgende GenAI-runtimeversion er 0.17.1, CPU. Brug kun betroede modeller; procesadskillelse er ikke en sandbox.

Assistenten understøtter valgte programmer uden argumenter, valgte dokumenter og websteder samt mikrofonmuting, når det udtrykkeligt anmodes. Den opretter ikke beskeder, aftaler, vilkårlige scripts, frie klik eller downloads og skifter ikke headset. Målet og ressourcenavnene sendes til den valgte lokale runtime; ressourcestier og filindhold leveres ikke separat. Et mål indtastet af brugeren kan selv indeholde personoplysninger. En lokal runtimes egen netværksadfærd forbliver brugerens ansvar.

ONNX-anmodningsbudgettet er 90 sekunder inklusive modelindlæsning, højst 3072 prompttokens og 1024 genererede tokens, desuden begrænset af modellens kontekst. Fejlformet/afkortet JSON og ukendte ressource-ID’er afvises. Annullér eller luk for at afslutte en anmodning. En runtimetest med en tilfældig model dokumenterer udførelse, ikke nyttig planlægningskvalitet eller tidsbesparelse. Lokale eksperimentlogfiler indeholder hændelses-ID’er og tidsmålinger, ikke måltekst eller dokumentindhold; de uploades ikke automatisk.

Den senere evaluering af en lille Instruct-model gav nul gyldige planer i fire tyske/engelske forsøg. Native inferens fungerede, men modellen opfyldte ikke den strenge planlægningskontrakt. Dette er et negativt kvalitetsresultat, ikke en anbefalet model eller en dokumenteret nyttig assistent. Se den daterede testrapport om lokale runtimes for den præcise modelrevision og begrænsninger.

## Import, eksport, synkronisering og backup

Gennemgå importforhåndsvisninger, før du erstatter en konfiguration. Importerede handlinger er deaktiveret fra starten, og portabel synkronisering fjerner hardwarespecifikke bindinger. Tildel enheder igen, og aktivér bevidst handlingerne efter kontrol. Gennemgå tekst- og handlingsindhold, før du deler en eksport; udelukkelse af adgangskoder gør ikke alt brugeroprettet handlingsindhold ufølsomt.

Profilsynkronisering udveksler filer i en valgt mappe, eventuelt administreret af din eksisterende cloudklient. MAKRIVO tilbyder ingen cloudkonto og gemmer ingen cloudlegitimationsoplysninger. Revisioner forhindrer ubemærkede overskrivninger. Medtagelse af handlingsindhold er et udtrykkeligt valg. Sikkerhedskopiér konfigurationen før en erstatningsimport. Standardkonfigurationen er `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; det historiske mappenavn bevares for kompatibilitet. Luk appen før manuel kopiering/gendannelse. Gemte SDK-godkendelser og legitimationsoplysninger beskyttet af operativsystemet kan skulle oprettes igen på en anden pc.

## Problemløsning

| Symptomer | Hvad skal man tjekke |
| --- | --- |
| Genvej gør ingenting | Appen kører, bindinger er ikke pauset, korrekt aktiv profil, gemt/aktiveret flise, genvejskonflikter og tilgængeligt mål. |
| Målvinduet kan ikke aktiveres | Aktivér det manuelt. Windows-forgrundsbegrænsninger og forskelle i rettigheder kan blokere automatisering. |
| Specialtast registreres ikke | Leverandørtjeneste/SDK/modelunderstøttelse; konfigurér om muligt proprietære kontroller til at udsende en understøttet standardtast. |
| Ekstra tastatur skriver også normalt | Forventet med Raw Input. Eksklusiv undertrykkelse er ikke implementeret til produktionsbrug. |
| Lyd eller lys ikke tilgængelig | Opdatér enheder; undersøg valgt identitet, endepunktsessioner, WMI/DDC/CI-understøttelse og dock-/kabelbegrænsninger. |
| OBS offline | WebSocket v5 aktiveret, korrekt endepunkt/adgangskode, tilgængelig tjeneste og gyldige scene-/inputnavne. |
| Automatisering fejler | Ledsagende vært medfølger, entydig gyldig selektor, tilgængeligt aktiveret mål, understøttet kontrolelement og tilladelser. Gentag kun efter en midlertidig timeout, når målet er gennemgået. |
| Mobilpanel utilgængeligt | LAN-adresse frem for loopback, samme netværk, kørende server, godkendte profiler, certifikatfingeraftryk/tillid og manuelle firewalltilladelser. |
| Plugin fejler | Pakkeintegritet, udtrykkelig tillids-/aktiveringstilstand, afhængigheder, protokolkompatibilitet og udførelseslog. |
| AI-anmodning mislykkes | Korrekt valgt lokal runtime, tjeneste/model klar, understøttet JSON-schema, betroet kompatibel modelmappe og anmodningsgrænser. |
| Layout eller tekst synes forkert | Skeln mellem brugerfladesprog og tastaturgeometri; tilpas vinduesstørrelsen, kontrollér skærmskaleringen, og rapportér præcis side/sprog/build. |

En nyttig fejlrapport indeholder Windows-version, app-build/commit, sprog, skærmskalering, hardwaremodel, en minimal trinsekvens og den relevante redigerede log. Fjern personlige stier, handlingsindhold og legitimationsoplysninger før offentliggørelse.

## Build, test og pakning

Brug SDK-versionen fastlagt i `global.json`. Fra repositoriets rodmappe:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows-integrationstests kan åbne isolerede vinduer og midlertidigt konfigurere/frigive understøttede leverandørbindinger. Kør dem uden en anden controller, der ejer disse bindinger. Standardprober for lysstyrke/lyd er skrivebeskyttede; udtrykkelige hardwaretests med ændring og gendannelse er separate. Mockprotokoller, renderede skærmbilleder og tilfældige inferensgrafer må ikke fremstilles som reel accept af hardware/modelkvalitet.

Det komplette publiceringsværktøj samler ledsagende værtsprocesser, mobilfiler, eksempelplugin, ONNX-runtime og tilgængelige afhængighedsmeddelelser. Bevar undermapperne. Et vellykket build er ikke et signeret installationsprogram eller juridisk udgivelsesgodkendelse. Kilde og builds kan udvikle sig hver for sig; sammenlign dokumentationens snapshot og commit med den installerede version.

## Licensering, privatliv og udgivelsesstatus

Den tilsigtede kommercielle model er et engangskøb med varig brug af den købte hovedversion; endelig pris, tilladt antal enheder og opgraderingsvilkår skal fastlægges i de faktiske salgsvilkår. Denne håndbog etablerer ikke et produktionsklart købs-/aktiveringssystem. Daglig styring kræver ingen cloudkonto.

Distribuér ikke proprietære SDK-binærfiler uden dokumenteret tilladelse. Bevar licens- og tredjepartsmeddelelser for hver medfølgende afhængighed, herunder .NET- og ONNX-pakker. Modelvægte har deres egne vilkår. Den daterede licensrevision ligger før visse nye afhængigheder og er ikke en godkendelse af disse tilføjelser. At repositoriet er offentligt, giver ikke i sig selv en generel open source-licens.

Reel leverandørhardware, telefon-/tabletbrug via LAN, skift mellem skærme/DPI-indstillinger, langvarig brug, fuldstændig sproglig gennemgang, eksklusiv driverunderstøttelse og nyttig AI-planlægningskvalitet kræver fortsat særskilt acceptarbejde. Udled aldrig godkendelse alene af, at en funktion findes.

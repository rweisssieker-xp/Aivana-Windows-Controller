<!-- generated-doc locale=da-DK source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../HANDBOOK.md)

# MAKRIVO — Brugervejledning

Fabrikant: **Aivana**. Dokumentation af snapshot: **2026-10-12**. Denne håndbog beskriver den aktuelle arbejdsgård implementering. eksperimentelle integrationer og hardware-afhængige funktioner identificeres udtrykkeligt. historiske rapporter beskriver deres dato snapshot, ikke en garanti for en ny bygning.

## Dokumentation og sprog

Brug dokumentationssprogselektoren til at åbne den komplette manual, funktionelle inventory, problemløsning, udvikling noter, forskning og historiske testrapporter i nogen af de 27 ansøgningssprog. Genererede oversættelser er maskinassisteret og har strukturelle kontroller; de har ikke alle modtaget indfødte talere. Tekniske identifikatorer og eksempler forbliver uændrede. Den oprindelige kilde er tilgængelig for sammenligning. Den oversatte licensanalyse er informativ og erstattet ikke de oprindelige tredjeparts lovlige betingelser eller en endelig EULA.

## Installation og første start

Det første mål er Windows 11 x64. Hold alle filer og subdirektiver sammen. Udløbet af `MAKRIVO.exe` En kildekode er ikke en installator. Selvindholdte pakker omfatter deres .NET runtime; rammeafhængige pakker kræver de relevante .NET 10 Desktop og ASP.NET runtime. ONNX og medfølgerhost skal inkluderes for deres respektive funktioner.

1. Start MAKRIVO og vælg grænselandet i Indstillingerne. Det er automatisk at følge Windows. Keyboard layout er en separat indstilling.
2. Vælg en profil, og tilføje et tag gennem den søgbare handlingskategori. Giv det et kendt navn og vælg dets handlinger.
3. Læs om trinene og indsend en korthed, hvis det er nødvendigt. Undgå kombinationer, der allerede bruges af Windows eller din målapplikation.
4. Beskyttelse af projektet. Ubeskyttede ændringer er ikke konfigurationen.
5. Test med en uskadelig handling, såsom åbning af et dokument, og inspektion af udførelsen feedback.

Lukning af hovedvinduet forlader normalt ansøgningen i meddelelsesområdet. Double-klik på sin tray-ikone for at åbne den igen, eller brug tray-menuen for at komme ud. Start med Windows er valgfri og oprindeligt inaktiv. Pausekontrolen suspenderer båndene; ikonet viser en pause. Afbestillede makroer gennem UI, tray eller den konfigurerede afbestillingskort; standard er `Ctrl+Alt+Escape`.

## Tilder, handlinger og kommandopaletten

Tilder tilhører profiler og kan indeholde flere ordnede trin. Redaktøren understøtter Save/Discard, Preview, undo/redo og Step Reordering. Katalogen leverer forudset til programmer, dokumenter, URL'er, tekst, clipboard, kortsætninger, medier, lyd, vinduer, systemaktioner og støttede integrationer. En forudsætning, der kræver parametre, åbnes som et udkast.

Åbn paletten med `Ctrl+K` mens MAKRIVO har fokus. Søg sparet handlinger, præcits, profiloverførsel og værktøjer. Vælg trykklæder; Indtast aktiverer; `Ctrl+Enter` redigerer et støttet element; Escape lukker. Nogle kommandoer, der er klar til brug, udføres umiddelbart, mens konfigurerede handlinger kræver gennemgang. En separat global palette kan konfigureres; nye konfigurationer anvendes `Ctrl+Shift+Space`.

Tekst og clipboard skridt støtte `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` og `{{user}}`. Datoformatet er `yyyy-MM-dd`; Tidsformatet er `HH:mm`. Ukendte variabler forbliver uændrede.Clipboard læsning sker kun, når det er anmodet.En clipboard handling skriver tekst uden automatisk at passe den.

Aktioner, der er lanceret fra UI, kan målrette den sidste eksterne ansøgning. Hvis målet forsvinder eller Windows blokerer foreground aktivering, aktiver manuelt målet og tilbagekaldes. En vellykket nøglelevering viser ikke, at målet har afsluttet sin opgave.

## Profiler og arbejdspladser

Profiler kan matche et procesnavn og en del af et vindue-titel. Når de to er indstillet, skal de begge matche. Profilsætningen løser konkurrerende kampe; standardprofilen er faldet. Det er muligt at udvælge automatisk. Lock Profile forhindrer automatisk ændringer i den aktuelle session.

Åbne arbejdspladser fra værktøjer eller palet. Vælg programmer, dokumenter, websteder, et lyd/display mål eller en scene OBS. Assistenten forbereder arbejdspladser, møder, streaming, forskning eller projektprofiler. Kontroller de genererede trin, før du sparer. Det er ikke muligt at udføre de forberedte handlinger.

## Macros, betingelser og optagelse

Macros udfører deres skridt i orden og understøtter ventetid, gentagelser og annullering. Den generelle gentagelsesgrænse er 100. Brug forudsigelse, før du kører de handlinger, der skriver ind i en anden app eller ændrer systemstatus.

Ventet for Target Window accepterer en proces, et overskrift og en timeout fra 1 til 600,000 ms. Der kræves mindst et filter. Et matchende synligt vindue skal reagere og blive forhåndsfeltet, før de efterfølgende skridt fortsætter. En klar vindue viser ikke, at et dokument eller en tjeneste er færdig med opladning.

Arbejdsflydelsesredaktøren tilføjer fil-eksister, process-running og vindue-titel betingelser. En falsk tilstand skyder skridt og kan gå videre. Nægte betingelsen for at udtrykke en alternativ branche. Vælg stop-on-error eller fortsat-on-errør med vilje; fortsat kan forlade senere skridt uden deres forventede forudsætninger.

Aktionsrekorden fanger kortskærme og venter, ikke almindelig skrive. Det understøtter modifikator shortcuts og funktionske nøgler. Skibet stopper med at optage. Se listen og vedtage den som et ubesvaret tileforslag, så spar det udtrykkeligt. UI-klik er et separat værktøj beskrevet nedenfor.

## Keyboards, gesturer og lag

Tastaturet indeholder opdagede grænser og reference layouter. En enkelt fysiske tastatur kan udstede flere grænser. Vælg det rigtige sprog og geometri; et produktnavne alene identificerer ikke ISO eller ANSI layout.

Standard globale hotkeys anvendes på tværs af tastaturer. Raw Input binder overvåger en bestemt enhed og skanningskode, herunder understøttede funktioner og medienøgler. **Raw Input Det undertrykker ikke almindelig Windows En eksklusiv anden tastatur er ikke klar til produktion.** En underskrevet, licenseret og testet filter-driver er stadig nødvendig; ingen er installeret af MAKRIVO.

Understøttede specielle nøgleforbindelser kan bruge øjeblikkelig tryk, tryk, hold eller dobbelt tryk. Held-lag nøgler midlertidigt vælge en anden mapping og genoprette baselaget på frigivelse; lag-selektor nøgler kører ikke deres egen makro. 150 til 5,000 MS; dobbelte vinduer fra 150 til 1,000 Disse mapper er forskellige fra almindelige globale hotkeys.

Den særlige nøglevarende opdager kompatible nøgler uden at udføre deres tildelte handlinger under fanget. Diagnostik forklarer utilgængelige enheder, stoppet bindinger, profil misforståelser og konflikter. Overlyden viser den valgte layout og bindinger; dens standard holdkort er `Ctrl+Alt+F1`.

| Integration | Krav og begrænsninger |
| --- | --- |
| Corsair | Running iCUE, SDK adgang og en kompatibel model. G1-G6 og støttet RGB funktioner; proprietære Fn, hjul, lås og profilkontrol er generelt ikke udsat for G-key API. |
| Logitech G | Original x64 G-key/LED SDK biblioteker kræver udtrykkelig godkendelse. G-key begivenheder identificerer ikke flere identiske fysiske tastaturer. |
| Razer | Chroma RGB og standardindtægter; mappe proprietære kontroller i Synapse hvor det er nødvendigt. |
| SteelSeries | GameSense RGB og standardindtægter; kort ejendomsstyringer i GG, hvor det er nødvendigt. |
| Wooting | Kompatibel original x64 SDK for analoge værdier og grænser; normal HID indtastning forbliver aktiv. |
| Andre nøgler | Standard hotkeys eller understøttede fysiske skannekoder. Fn og ejendomsstyringer afhænger af modellen og leverandørkonfigurationen. |

Fabrikantforskning beskriver dokumenterede kapaciteter, ikke implementeret eller testet kompatibilitet med hver enhed. Nye adaptere har stadig brug for ægte hardware accept. Download ikke SDK binære fra usikre kilder; SDK distributionsrettigheder er adskilt fra teknisk kompatibilitet.

## Lyd, skærm og OBS

Audio trin indstilles volumen fra 0 til 100 procent og mute/unmute/toggle støttet udgang eller mikrofon endpunkter. Vælg et bestemt slutpunkt eller brug Windows standard. Et tabt mål producerer en fejl i stedet for stille at kontrollere en anden enhed. Forny enhederne efter genforbindelse. Endpoint-kontrol er ikke et krav om uafhængig blandingsstøtte.

Visningshandlinger øge/sænke lysstyrken med 10 procentpoint eller indstille en værdi fra 0 til 100. Interne skærme bruger understøttede WMI-grænser; eksterne monitorer kræver DDC/CI support og normalt dets monitor-menu indstilling. Dock, adaptere og kabler kan blokere kontrol. Vælg alle støttede skærme eller én monitor. Genforbindelse kan ændre de tilgængelige identiteter; gennemgå det redde mål. Manglende læsninger er vist som utilgængelige i stedet for målt værdier.

OBS kræver WebSocket v5 aktiveret i OBS Værktøjer. Konfigurer slutpunktet, test forbindelsen og lad indsendelsesnavn før du sparer. Støtte handlinger omfatter scenen skift, streaming/optagelse start/stop/toggle, optagelse pause/resume og input mute. Passwords kan gemmes i Windows Credential Manager og er udelukket fra konfiguration eksporter. En vellykket OBS kommando etablerer ikke alle downstream-effekter.

## Resultat verifikation og eksekutionshistorie

Det er muligt at kontrollere resultaterne for de rigtige skridt. Appen skiller præflightchecks, bekræftede effekter, fejl, annullering og effekter, der ikke kan verificeres. Eksistensen af processen er ikke tilgængelighed. Relative ændringer, nedskæringer og tekstindtægter kan ikke modtage en form for succes. Kontroller aktivitetsloggen og verifikationshistorien, når en handling fejler. Konfigurationsfejl skal korrigeres, før den komplette makro er udløst.

## Automatisering og klik på optagelse

Tilføj et UI Automation trin, vælg operationen og fange målet under indikatoren efter den vist forsinkelse. Fange alene udfører intet. Tjek selektoren og kræver en unik match. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` og `WaitState`. Passwordfelter, deaktiverede kontroller og ambiguelle selektorer afvises.

UI-automation kører i en begrænset følgesproces. Det afhænger af målapplikationens Windows tilgængelighedstøtte og tilladelser. Klik-optageren fanger støttede handlinger og pauser; den registrerer ikke generelle tekstindtægter. Overvej og valider dit forslag, før det vedtages. Ændringer i en ekstern app kan annullere selektorer. Processisolering begrænser kollisioner og annullering, men er ikke en sikkerhedssandboks.

## Plugins

Åbn udvidelser til at kontrollere et pakke, før du importerer det. Pakkestruktur, manifest og hashes kontrolleres. Der er indførsel af handicap. Læs de erklærede handlinger og stole udtrykkeligt på udgiveren, før du tillader det. Plugins kører med dine brugers tilladelser i en separat proces; denne proces er ** ikke en sandboks**. Kun pakker, du stoler på.

Manageren understøtter eksport, deaktivering, afinstallering og restaurering af en tidligere version. Manglende afhængighed blokerer invokation. Eksempelpakken giver regnskab og lokale aktioner. Den understøttede schema er en dokumenteret JSON-Schema-subset, ikke alle mulige JSON-Skema-funktioner. Der er ingen offentlige plugins.

## Mobilpanel

1. Åbn Mobile Panel, vælg en lokal IP og udtrykkeligt vælge de profiler, der skal eksponeres. Den standard loopback-adresse er kun tilgængelig på selve pc'en; vælg den relevante LAN-adresse for en telefon eller tablet.
2. Start panelet og læs dens HTTPS-adresse, certificer fingeraftryk og en enkelt brug af seks-digit parende kode.
3. På en enhed i det samme betroede netværk, åbne adressen. Sammenlign fingeraftrykket SHA-256 gennem en pålidelig kanal, før du stoler på det lokale certifikat. Appen installerer ikke et pålideligt certifikat automatisk.
4. Brug den angivne kode og prøv en ufarlig tile. En manuel Windows Firewall tilladelse kan være nødvendig for LAN-tilgang. Lad være med at udsætte porten for internettet.
5. Stop panelet, når det er færdigt, eller tilbagekald klient via appen.

Private servernøgler er beskyttet med Windows DPAPI; klientkreditering lagres som hasher. Ændring af IP eller fornyelse af certifikatet kan kræve fornyet tillid. Panelet viser handling og verifikation, ikke alle mulige levende sensorer. Browser-layout og lokale HTTPS-checks erstattes ikke af den virkelige telefon/tablet LAN accept.

## Lokalt møder assistent

Denne valgfrie eksperimentelle assistent er forhindret. Åbn den fra Værktøjer, aktiver det udtrykkeligt, vælg en runtime/model og tilføj kun de ressourcer, du ønsker at overveje. Indstill målet, oprette en udgave, inspicere uløsede anmodninger og overføre resultatet til den normale redaktør. Ingenting udføres automatisk; redning og kørsel forbliver eksplisitte handlinger. Der er ingen automatisk runtime fallback.

| Runtime | Indstillingen |
| --- | --- |
| Ollama | En separat lokal service og en lokalt installeret model på `127.0.0.1:11434`. De udtalte fjern/cloud modeller afvises. |
| LM Studio | Lokalt server med en lokalt opladet model og JSON-schema understøttelse på `127.0.0.1:1234`. Gør ikke fjernbetjening til lokalbehandling. |
| llama.cpp | Lokal `llama-server` og kompatibel model med JSON-schema under `127.0.0.1:8080`. |
| ONNX Udløbet GenAI CPU | Den pålidelige lokale GenAI tekstmodel mappe; vælg `genai_config.json`. Inference bruger en bundet partnerproces uden HTTP. |

HTTP-klienter bruger kun loopback, bruger ikke proxyer og følger ikke omdirigeringer. MAKRIVO kan ikke pålideligt fastslå, om en kompatibel lokal server internt kalder en fjernbetjening. Autentiserede lokale servere kan ikke konfigureres i denne version. Modellen vægter Python og lokale inference servere downloades eller installeres ikke automatisk.

ONNX Pakkerne skal `tokenizer.json` og en gyldig chat-tema. støttet templateformularer i `tokenizer_config.json` Det er en string `chat_template`, En ordbog med en string `default`, eller en ubegrænset navne/default template liste. hvis templatet er savnet eller nul, `chat_template.jinja` De tomme, ambiguøse, oversigtsmæssige eller forkerte maler afvises. ONNX filer, indsendelses/visionsmodeller og GPU-pakker er ikke udvekslbare med kompatible GenAI Tekstmønster: Den bundte GenAI Runtime-versionen er 0.17.1, Brug kun pålidelige modeller; processkillelse er ikke en sandboks.

Hjælpen understøtter udvalgte programmer uden argumenter, udvalgte dokumenter og websteder, og mikrofonen støtter, når det udtrykkeligt anmodes om. Det skaber ikke meddelelser, aftaler, vilkårlige skript, gratis klik, downloads eller headset skift. Formålet og ressourcenavn sendes til den valgte lokale driftstid; ressourceveje og filindhold leveres ikke separat. Et mål, som brugeren selv kan indeholde personlige oplysninger. En lokal runtime egen netværksadfærd forbliver brugerens ansvar.

Den ONNX anmodningsbudget er 90 sekunder herunder modelladning, op til 3,072 prompt tokens og 1,024 genereret tokens, også begrænset af model kontekst. Misformede/trunkede JSON-ID'er og ukendte ressourcer afvises. Afbestill eller afslutte en anmodning. En tilfældig model af runtime-testen fastsætter udførelse, ikke nyttige planlægningskvalitet eller tidsbesparelser. Lokale eksperimenter indeholder begivenhedsidentifikatorer og timing, ikke måltekst eller dokumentindhold; de ophobes ikke automatisk.

Den senere lille Instruct-modelvurdering producerede nul gyldige planer i fire tyske/engelsk forsøg. Native inference fungerede, men modellen mislykkede den strenge planlægningsaftale. Dette er et negativt kvalitetsresultat, ikke en anbefalet model eller en bevidst nyttig assistent. Se den datoerede lokale testrapport for den nøjagtige modelrevision og begrænsninger.

## Import, eksport, synkronisering og backup

Overvej import forudsigelser, før du erstatte en konfiguration. Importerede handlinger begynder at deaktiveres, og den bærbare synkronisering fjerner hardware-specifikke bindinger. Giv enhederne igen og tillade handlingerne med vilje efter inspektion. Læs tekst/actionindholdet, før du deler en eksport; udelukkelse af adgangskoder gør ikke alle brugeren skabte handlingindhold følsomme.

Profilsynkronisering udveksler filer i en valgt mappe, optional en forvaltet af din eksisterende cloud-klient. MAKRIVO leverer ingen cloud-konto og gemmer ingen cloud-krediteringer. Revisioner forhindrer stille overskrivninger. Indholdet af handling er et udtrykkeligt valg. Du kan oprette din konfiguration før du indfører en erstattelse. Den standardkonfiguration er `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; det historiske katalognavne bevares for kompatibilitet. Sluk appen, før du kopierer / genopretter den manuelt. Lagrede SDK-godkendelser og OS-beskyttede akkrediteringer kan være nødvendige for at genoprette på en anden PC.

## Problemløsning

| Symptomer | Hvad skal man tjekke |
| --- | --- |
| Shortcut gør ikke noget | Appen kører, bindinger ikke pause, korrekt aktiv profil, gemt/aktiveret tile, kortsnit konflikter og tilgængelige mål. |
| Målvinduet kan ikke aktiveres | Manuel aktivering af det. Windows foreground restriktioner og privilegier kan blokere automatisering. |
| Specielle nøgler er ikke fundet | Leverandør service/SDK/modelstøtte; konfigurere ejendomsstyringer til at udstede en støttet standardnøgle, hvis det er muligt. |
| Den anden tastatur er også normalt | Forventet med Raw Input. Udelukkende nedbrydning anvendes ikke til produktion. |
| Lyd eller lys ikke tilgængelig | Forny enhederne; inspektion af valgte identiteter, endpoint sessioner, WMI/DDC/CI support og dock/kabel begrænsninger. |
| OBS offline | WebSocket v5 aktiveret, korrekt endpoint / adgangskode, tilgængelig service og gyldige scene / indtjeningsnavn. |
| Automatisering fejler | Companion-host inkluderet, en unik gyldig selektor, tilgængelig aktiveret mål, understøttet kontrol og tilladelser. Returner en overgangstid kun efter at have gennemgået målet. |
| Mobiltelefonen er uacceptabel | LAN i stedet for loopback adresse, samme netværk, serverudstyr, godkendte profiler, certificeret fingeravtryk/trust og manuelle firewall tilladelser. |
| Plugin fejler | Pakkeintegritet, eksplicit tillid/tilladelse, afhængighed, protokolkompatibilitet og gennemførelseslog. |
| Forespørgslen falder | Korrekt udvalgte lokale driftstid, service/model klar, støttet JSON-skema, betroet kompatible modelmappe og anmodningsgrænser. |
| Layout eller tekst synes forkert | Fjern UI-sprog fra tastaturgeometri; genanvend vinduet, tjek skalaen og rapporter det nøjagtige side / sprog / opbygning. |

For en nyttig bugrapport omfatter Windows version, app build/commit, sprog, skalaer, hardware model, en minimal sekvens og den relevante redigerede log. Fjern personlige ruter, handlingsindhold og akkrediteringer før offentliggørelse.

## Bygning, test og emballage

Brug SDK i `global.json`. Fra Røden:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integrationstester kan åbne isolerede vinduer og midlertidigt konfigurere/læs støttet leverandørbindinger. Udfør dem uden en anden kontroller, der ejer disse bånd. De standardlys/audio-prøver er læst alene; udtrykkelige hardware runde-trip tests er adskilt. Mock-protokoller, renderede skærmbilleder og tilfældige inference-grafikker må ikke repræsenteres som en reel hardware/modelkvalitet accept.

Den fulde udgiver indsamler medarbejderhost, mobile aktiver, eksemplet plugin, ONNX runtime og tilgængelige afhængighed meddelelser. Hold underretningerne. En vellykket opbygning er ikke en underskrevet installator eller lovlig frigivelse godkendelse. Kilde og bygninger kan udvikle sig separat; sammenlign dokumentation snapshot og kom med den installerede version.

## Licensing, privatliv og frigivelse

Den hensigtsmæssige kommercielle model er en enkelt køb med permanent brug af den købte store version; den endelige pris, enhedstilladelse og opgraderingsbetingelser skal fastsættes i de faktiske salgsbetingelser. Et produktionskøb/aktiveringssystem er ikke etableret i denne håndbog. Der er ingen cloud-konto til daglig kontrol.

Du må ikke distribuere ejendomsbaserede SDK-binærer uden godkendelse. Beholde licenser og tredjepartsvarslen for hver afhængighed, herunder .NET og ONNX pakker. Modellerne har deres egne vilkår. Den daterede licensaudit giver nogle nye afhængigheder og er ikke en clearance for disse tilføjelser. Den offentlige repository giver sig selv ikke en generel open-source licens.

Virkelig leverandør hardware, telefon/tablet LAN-operation, blandet monitor/DPI-transitioner, langvarig brug, komplet sproglig gennemgang, eksklusiv driverstøtte og nyttig AI-planlægning kvalitet forbliver separat accept arbejde. Jeg giver aldrig godkendelse alene.

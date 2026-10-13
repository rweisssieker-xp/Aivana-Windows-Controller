<!-- generated-doc locale=nb-NO source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../HANDBOOK.md)

# MAKRIVO-brukerhåndbok

Produsent: **Aivana**. Dokumentasjonsøyeblikksbilde: **2026-10-12**. Denne håndboken beskriver den gjeldende implementeringen i arbeidstreet. Eksperimentelle integrasjoner og maskinvareavhengige funksjoner angis uttrykkelig. Historiske rapporter beskriver sitt daterte øyeblikksbilde, ikke en garanti for et nyere bygg.

## Dokumentasjon og språk

Bruk dokumentasjonens språkvelger for å åpne hele manualen, funksjonsoversikten, feilsøkingen, utviklingsnotatene, forskningen og historiske testrapporter på et av programmets 27 språk. Genererte oversettelser er maskinassisterte og strukturkontrollerte; ikke alle er gjennomgått av morsmålsbrukere. Tekniske identifikatorer og kjørbare eksempler forblir uendret. Originalkilden er fortsatt tilgjengelig for sammenligning. Oversatt lisensanalyse er veiledende og erstatter ikke originale juridiske tredjepartsvilkår eller en endelig EULA.

## Installasjon og første lansering

Det første målet er Windows 11 x64. Hold alle filer og undermapper i distribusjonen samlet. Start `MAKRIVO.exe`. En utsjekking av kildekoden er ikke et installasjonsprogram. Selvstendige pakker inneholder sin .NET-runtime; rammeverksavhengige pakker krever passende .NET 10 Desktop- og ASP.NET-runtimer. ONNX og ledsagende vertsprosesser må følge med for de respektive funksjonene.

1. Start MAKRIVO og velg grensesnittspråket i Innstillinger. Automatisk følger Windows. Tastaturoppsett er en separat innstilling.
2. Velg en profil og legg til en flis via den søkbare handlingskatalogen. Gi den et gjenkjennelig navn og velg handlingene.
3. Gjennomgå trinnene og tilordne en hurtigtast ved behov. Unngå kombinasjoner som allerede brukes av Windows eller målprogrammet.
4. Lagre utkastet. Ulagrede endringer inngår ikke i den lagrede konfigurasjonen.
5. Test med en ufarlig handling, som å åpne et dokument, og undersøk tilbakemeldingen om kjøringen.

Når hovedvinduet lukkes, blir programmet vanligvis værende i systemstatusfeltet. Dobbeltklikk på ikonet der for å åpne det igjen, eller avslutt via ikonmenyen. Oppstart med Windows er valgfritt og deaktivert i utgangspunktet. Pausefunksjonen setter bindinger på pause; ikonet viser et pausemerke. Avbryt kjørende makroer via brukergrensesnittet, systemstatusfeltet eller den konfigurerte avbrytelsessnarveien; standarden er `Ctrl+Alt+Escape`.

## Fliser, handlinger og kommandopaletten

Fliser tilhører profiler og kan inneholde flere ordnede trinn. Redigeringen støtter lagre/forkast, forhåndsvisning, angre/gjør om og endring av trinnrekkefølge. Katalogen tilbyr forhåndsinnstillinger for programmer, dokumenter, URL-er, tekst, utklippstavle, hurtigtaster, medier, lyd, vinduer, systemhandlinger og støttede integrasjoner. En forhåndsinnstilling som krever parametere, åpnes som et utkast.

Åpne paletten med `Ctrl+K` mens MAKRIVO har fokus. Søk etter lagrede handlinger, forhåndsinnstillinger, profilbytte og verktøy. Piltaster velger; Enter aktiverer; `Ctrl+Enter` redigerer et støttet element; Escape lukker. Noen bruksklare kommandoer utføres umiddelbart, mens konfigurerte handlinger krever gjennomgang. En separat global palettsnarvei kan konfigureres; nye konfigurasjoner bruker `Ctrl+Shift+Space`.

Tekst- og utklippstavletrinn støtter `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` og `{{user}}`. Datoformatet er `yyyy-MM-dd`; tidsformatet er `HH:mm`. Ukjente variabler forblir uendret. Utklippstavlen leses bare når det er forespurt. En utklippstavlehandling skriver tekst uten automatisk å lime den inn.

Handlinger startet fra brukergrensesnittet kan rettes mot det siste eksterne programmet. Hvis målet forsvinner eller Windows blokkerer aktivering i forgrunnen, aktiver målet manuelt og prøv igjen. Vellykket levering av tastetrykk beviser ikke at målet har fullført oppgaven.

## Profiler og arbeidsplass

Profiler kan samsvare med et prosessnavn og en del av en vindustittel. Når begge er angitt, må begge samsvare. Profilrekkefølgen avgjør konkurrerende treff; standardprofilen brukes som reserve. Aktiver automatisk valg uttrykkelig. Lås profil forhindrer automatiske endringer i den gjeldende økten.

Åpne Arbeidsplassoppsett fra Verktøy eller paletten. Velg programmer, dokumenter, nettsteder, et lyd-/skjermmål eller en OBS-scene. Assistenten forbereder arbeidsplass-, møte-, strømme-, forsknings- eller prosjektprofiler. Gjennomgå de genererte trinnene før lagring. Oppsettet utfører ikke de forberedte handlingene.

## Makroer, vilkår og opptak

Makroer utfører trinnene i rekkefølge og støtter ventetider, gjentakelser og avbrytelse. Den generelle gjentakelsesgrensen er 100. Bruk forhåndsvisning før handlinger som skriver i et annet program eller endrer systemtilstand.

Vent på målvindu godtar en prosess, en del av en tittel og en tidsavbruddsgrense fra 1 til 600000 ms. Minst ett filter kreves. Et samsvarende synlig vindu må svare og komme i forgrunnen før etterfølgende trinn fortsetter. Et klart vindu beviser ikke at et dokument eller en tjeneste er ferdig lastet.

Arbeidsflytredigeringen legger til vilkår for at en fil finnes, en prosess kjører og en vindustittel samsvarer. Et usant vilkår hopper over trinnet og kan hoppe over flere etterfølgende trinn. Inverter vilkåret for å angi en alternativ gren. Velg bevisst mellom å stoppe eller fortsette ved feil; ved fortsatt kjøring kan senere trinn mangle sine forventede forutsetninger.

Handlingsopptakeren registrerer hurtigtastkombinasjoner og ventetider, ikke vanlig tekstinntasting. Den støtter snarveier med modifikasjonstaster og funksjonstaster. Escape stopper opptaket. Gjennomgå listen og overta den som et ulagret flisutkast; lagre deretter uttrykkelig. Opptak av UI-klikk er et separat verktøy beskrevet nedenfor.

## Tastaturer, bevegelser og lag

Tastatursiden viser oppdagede grensesnitt og referanseoppsett. Ett fysisk tastatur kan eksponere flere grensesnitt. Velg riktig språk og geometri; et produktnavn alene identifiserer ikke ISO- eller ANSI-oppsett.

Vanlige globale hurtigtaster gjelder på tvers av tastaturer. Enhetsspesifikke Raw Input-bindinger observerer en bestemt enhet og skannekode, inkludert støttede funksjons- og medietaster. **Raw Input undertrykker ikke vanlig Windows-inndata. Et eksklusivt ekstra tastatur er ikke produksjonsklart.** Det kreves fortsatt en signert, lisensiert og testet filterdriver-backend; MAKRIVO installerer ingen.

Støttede spesialtastbindinger kan bruke umiddelbart trykk, kort trykk, langt trykk eller dobbelttrykk. Lagtaster velger midlertidig en annen tilordning mens de holdes nede, og gjenoppretter grunnlaget når de slippes; lagvelgertaster kjører ikke egne makroer. Terskler for langt trykk går fra 150 til 5000 ms; tidsvinduer for dobbelttrykk fra 150 til 1000 ms. Disse tilordningene er atskilt fra vanlige globale hurtigtaster.

Spesialtastveiviseren oppdager kompatible taster uten å utføre tilordnede handlinger under registreringen. Diagnostikk forklarer utilgjengelige enheter, pausede bindinger, profilavvik og konflikter. Overlegget viser valgt oppsett og bindinger; standard snarvei som holdes nede, er `Ctrl+Alt+F1`.

| Integrasjon | Krav og begrensninger |
| --- | --- |
| Corsair | Kjørende iCUE, SDK-tilgang og en kompatibel modell. G1–G6 og støttede RGB-funksjoner; proprietære Fn-, hjul-, låse- og profilkontroller eksponeres vanligvis ikke via G-key API. |
| Logitech G | Originale lokale x64 G-key/LED SDK-biblioteker krever uttrykkelig godkjenning. G-key-hendelser identifiserer ikke entydig flere identiske fysiske tastaturer. |
| Razer | Chroma RGB og standardinndata; tilordne proprietære kontroller i Synapse ved behov. |
| SteelSeries | GameSense RGB og standardinndata; tilordne proprietære kontroller i GG ved behov. |
| Wooting | Kompatibel original lokal x64 SDK for analoge verdier og terskler; vanlig HID-inndata forblir aktiv. |
| Andre tastaturer | Standardhurtigtaster eller støttede fysiske skannekodebindinger. Fn og proprietære kontroller avhenger av modellen og leverandørkonfigurasjonen. |

Produsentforskning beskriver dokumenterte muligheter, ikke implementert eller testet kompatibilitet med hver oppført enhet. Nye adaptere krever fortsatt aksepttester med reell maskinvare. Ikke last ned SDK-binærfiler fra upålitelige kilder; SDK-distribusjonsrettigheter er atskilt fra teknisk kompatibilitet.

## Lyd, skjermer og OBS

Lydtrinn setter volumet fra 0 til 100 prosent og demper støttede utgangs- eller mikrofonendepunkter, opphever demping eller veksler dempestatus. Velg et bestemt endepunkt eller Windows-standarden. Et manglende lagret mål gir en feil i stedet for ubemerket å styre en annen enhet. Oppdater enheter etter ny tilkobling. Endepunktstyring innebærer ikke støtte for en uavhengig mikser per program.

Skjermhandlinger øker/reduserer lysstyrken med 10 prosentpoeng eller setter en verdi fra 0 til 100. Interne skjermer bruker støttede WMI-grensesnitt; eksterne skjermer krever DDC/CI-støtte og vanligvis aktivering i skjermmenyen. Dokker, adaptere og kabler kan blokkere styringen. Velg alle støttede skjermer eller én skjerm. Ny tilkobling kan endre tilgjengelige identiteter; gjennomgå det lagrede målet. Manglende avlesninger vises som utilgjengelige i stedet for målte verdier.

OBS krever at WebSocket v5 er aktivert under Verktøy i OBS. Konfigurer endepunktet, test tilkoblingen og last inn scene-/inndatanavn før lagring. Støttede handlinger omfatter scenebytte, start/stopp/veksling av strømming/opptak, pause/gjenopptakelse av opptak og demping av inndata. Passord kan lagres i Windows Credential Manager og utelates fra konfigurasjonseksporter. En vellykket OBS-kommando dokumenterer ikke alle etterfølgende strømmevirkninger.

## Resultatverifisering og kjøringshistorikk

Aktiver Verifiser resultat for egnede trinn. Appen skiller mellom forhåndskontroller, bekreftede virkninger, feil, avbrytelse og virkninger som ikke kan verifiseres. At en prosess finnes, betyr ikke at programmet er klart. Relative endringer, hurtigtaster og tekstinntasting kan ikke få en generell suksessbekreftelse. Undersøk aktivitetsloggen og verifiseringshistorikken når en handling mislykkes. Rett konfigurasjonsfeil før hele makroen kjøres igjen.

## UI-automatisering og klikkopptak

Legg til et UI Automation-trinn, velg operasjonen og registrer målet under musepekeren etter den viste forsinkelsen. Registreringen alene utfører ingenting. Kontroller selektoren og krev et entydig treff. Støttede operasjoner omfatter `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` og `WaitState`. Passordfelt, deaktiverte kontroller og tvetydige selektorer avvises.

UI-automatisering kjører i en avgrenset hjelpeprosess. Den avhenger av målprogrammets Windows-tilgjengelighetsstøtte og tillatelser. Klikkopptakeren registrerer støttede handlinger og pauser; den tar ikke opp generell tekstinntasting. Gjennomgå og valider utkastet før overtakelse. UI-endringer i en ekstern app kan gjøre selektorer ugyldige. Prosessisolering avgrenser krasj og avbrytelse, men er ikke en sikkerhetssandbox.

## Plugins

Åpne Utvidelser for å undersøke en pakke før import. Pakkestruktur, manifest og hasher kontrolleres. Etter import er pakken deaktivert. Les de oppgitte handlingene og angi uttrykkelig tillit til utgiveren før aktivering. Plugins kjører med dine brukerrettigheter i en separat prosess; prosessen er **ikke en sandbox**. Installer bare pakker du stoler på.

Behandleren støtter eksport, deaktivering, avinstallering og gjenoppretting/forkasting av en tidligere versjon. Manglende avhengigheter blokkerer kall. Eksempelpakken tilbyr nedtellingshandlinger og handlinger for lokal tilstand. Det støttede skjemaet er en dokumentert delmengde av JSON-Schema, ikke alle mulige JSON-Schema-funksjoner. Det finnes ingen offentlig pluginmarkedsplass.

## Mobilpanel

1. Åpne Mobilpanel, velg en lokal IP og velg uttrykkelig profilene som skal eksponeres. Standard loopback-adresse kan bare nås på PC-en selv; velg passende LAN-adresse for en telefon eller et nettbrett.
2. Start panelet og les HTTPS-adressen, sertifikatets fingeravtrykk og den sekssifrede engangskoden for paring.
3. Åpne adressen på en enhet i samme betrodde nettverk. Sammenlign SHA-256-fingeravtrykket via en betrodd kanal før du stoler på det lokale sertifikatet. Appen installerer ikke automatisk et betrodd sertifikat.
4. Par med den viste koden og test én ufarlig flis. Manuell Windows Firewall-tillatelse kan være nødvendig for LAN-tilgang. Ikke eksponer porten på internett.
5. Stopp panelet når du er ferdig, eller tilbakekall parede klienter via appen.

Private servernøkler beskyttes med Windows DPAPI; klientlegitimasjon lagres som hasher. Endring av IP eller fornyelse av sertifikatet kan kreve fornyet tillit. Panelet viser handlings- og verifiseringstilstander, ikke alle mulige livesensorer. Kontroller av nettleseroppsett og lokal HTTPS erstatter ikke reelle LAN-aksepttester med telefon/nettbrett.

## Lokal AI-møteassistent

Denne valgfrie eksperimentelle assistenten er deaktivert som standard. Åpne den fra Verktøy, aktiver den uttrykkelig, velg en runtime/modell og legg bare til ressursene den skal vurdere. Angi målet, opprett et utkast, undersøk uavklarte forespørsler og overfør resultatet til den vanlige redigeringen. Ingenting utføres automatisk; lagring og kjøring forblir uttrykkelige handlinger. Det finnes ingen automatisk reserve-runtime.

| Runntid | Innstillinger |
| --- | --- |
| Ollama | Separat lokal tjeneste og en lokalt installert modell på `127.0.0.1:11434`. Oppgitte eksterne/skymodeller avvises. |
| LM Studio | Lokal server med en lokalt lastet modell og JSON-schema-støtte på `127.0.0.1:1234`. Ikke aktiver ekstern ruting for lokal behandling. |
| llama.cpp | Lokal `llama-server` og kompatibel modell med JSON-schema-støtte på `127.0.0.1:8080`. |
| ONNX Runntid GenAI CPU | Betrodd kompatibel lokal GenAI-tekstmodellmappe; velg `genai_config.json`. Inferens bruker en medfølgende hjelpeprosess uten HTTP. |

HTTP-klienter bruker bare loopback, ingen proxyer og følger ingen omdirigeringer. MAKRIVO kan ikke pålitelig avgjøre om en kompatibel lokal server internt kaller en ekstern tjeneste. Autentiserte lokale servere kan ikke konfigureres i denne versjonen. Modellvekter, Python og lokale inferensservere lastes ikke ned eller installeres automatisk.

ONNX-pakker trenger `tokenizer.json` og en gyldig chatmal. Støttede malformer i `tokenizer_config.json` er en streng `chat_template`, en ordbok med en streng `default` eller en entydig liste med navngitte/standardmaler. Hvis malen mangler eller er null, brukes `chat_template.jinja` som reserve. Tomme, tvetydige, for store eller feiltypede maler avvises. Vilkårlige ONNX-filer, embedding-/synsmodeller og pakker bare for GPU kan ikke byttes ut med kompatible GenAI-tekstmodeller. Den medfølgende GenAI-runtimeversjonen er 0.17.1, CPU. Bruk bare betrodde modeller; prosessatskillelse er ikke en sandbox.

Assistenten støtter valgte programmer uten argumenter, valgte dokumenter og nettsteder samt mikrofondemping når det uttrykkelig bes om det. Den oppretter ikke meldinger, avtaler, vilkårlige skript, frie klikk eller nedlastinger og bytter ikke hodesett. Målet og ressursnavnene sendes til den valgte lokale runtimen; ressursstier og filinnhold leveres ikke separat. Et mål skrevet av brukeren kan selv inneholde personopplysninger. En lokal runtimes egen nettverksatferd forblir brukerens ansvar.

ONNX-forespørselen har et budsjett på 90 sekunder inkludert modelllasting, opptil 3072 prompttokener og 1024 genererte tokener, også begrenset av modellkonteksten. Feilformet/avkortet JSON og ukjente ressurs-ID-er avvises. Avbryt eller lukk for å avslutte en forespørsel. En runtimetest med en tilfeldig modell dokumenterer utførelse, ikke nyttig planleggingskvalitet eller tidsbesparelse. Lokale eksperimentlogger inneholder hendelses-ID-er og tidsmålinger, ikke måltekst eller dokumentinnhold; de lastes ikke opp automatisk.

Den senere evalueringen av en liten Instruct-modell ga null gyldige planer i fire tyske/engelske forsøk. Native inferens fungerte, men modellen oppfylte ikke den strenge planleggingskontrakten. Dette er et negativt kvalitetsresultat, ikke en anbefalt modell eller en dokumentert nyttig assistent. Se den daterte testrapporten om lokale runtimer for nøyaktig modellrevisjon og begrensninger.

## Import, eksport, synkronisering og backup

Gjennomgå importforhåndsvisninger før du erstatter en konfigurasjon. Importerte handlinger er deaktivert fra starten, og portabel synkronisering fjerner maskinvarespesifikke bindinger. Tilordne enheter på nytt og aktiver handlinger bevisst etter kontroll. Gjennomgå tekst- og handlingsinnhold før du deler en eksport; utelatelse av passord gjør ikke alt brukeropprettet handlingsinnhold ufølsomt.

Profilsynkronisering utveksler filer i en valgt mappe, eventuelt administrert av din eksisterende skyklient. MAKRIVO tilbyr ingen skykonto og lagrer ingen skylegitimasjon. Revisjoner forhindrer ubemerkede overskrivinger. Inkludering av handlingsinnhold er et uttrykkelig valg. Sikkerhetskopier konfigurasjonen før en erstatningsimport. Standardkonfigurasjonen er `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; det historiske mappenavnet beholdes for kompatibilitet. Lukk appen før manuell kopiering/gjenoppretting. Lagrede SDK-godkjenninger og legitimasjon beskyttet av operativsystemet kan måtte opprettes på nytt på en annen PC.

## Problemløsning

| Symptomer | Hva å sjekke |
| --- | --- |
| Hurtigtast gjør ingenting | Appen kjører, bindinger er ikke pauset, riktig aktiv profil, lagret/aktivert flis, hurtigtastkonflikter og tilgjengelig mål. |
| Målvinduet kan ikke aktiveres | Aktiver det manuelt. Windows-forgrunnsbegrensninger og forskjeller i rettigheter kan blokkere automatisering. |
| Spesialtast oppdages ikke | Leverandørtjeneste/SDK/modellstøtte; konfigurer om mulig proprietære kontroller til å sende en støttet standardtast. |
| Ekstra tastatur skriver også normalt | Forventet med Raw Input. Eksklusiv undertrykking er ikke implementert for produksjonsbruk. |
| Lyd eller lys ikke tilgjengelig | Oppdater enheter; undersøk valgt identitet, endepunktøkter, WMI/DDC/CI-støtte og dokk-/kabelbegrensninger. |
| OBS Offline | WebSocket v5 aktivert, riktig endepunkt/passord, tilgjengelig tjeneste og gyldige scene-/inndatanavn. |
| UI automatisering feilsomt | Hjelpevert følger med, entydig gyldig selektor, tilgjengelig aktivert mål, støttet kontroll og tillatelser. Prøv igjen etter et midlertidig tidsavbrudd først når målet er gjennomgått. |
| Mobilpanel kan ikke nås | LAN-adresse fremfor loopback, samme nettverk, kjørende server, godkjente profiler, sertifikatfingeravtrykk/tillit og manuelle brannmurtillatelser. |
| Plugin svikter | Pakkeintegritet, uttrykkelig tillits-/aktiveringstilstand, avhengigheter, protokollkompatibilitet og kjørelogg. |
| AI-forespørsel mislykkes | Riktig valgt lokal runtime, tjeneste/modell klar, støttet JSON-schema, betrodd kompatibel modellmappe og forespørselsgrenser. |
| Layout eller tekst virker feil | Skill grensesnittspråk fra tastaturgeometri; endre vindusstørrelsen, kontroller skjermskaleringen og rapporter nøyaktig side/språk/bygg. |

En nyttig feilrapport inneholder Windows-versjon, appbygg/commit, språk, skjermskalering, maskinvaremodell, en minimal trinnsekvens og den relevante loggen med sensitive opplysninger fjernet. Fjern personlige stier, handlingsinnhold og legitimasjon før publisering.

## Bygging, testing og pakking

Bruk SDK-versjonen fastsatt i `global.json`. Fra rotmappen i repositoriet:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows-integrasjonstester kan åpne isolerte vinduer og midlertidig konfigurere/frigi støttede leverandørbindinger. Kjør dem uten at en annen kontroller eier disse bindingene. Standardprober for lysstyrke/lyd er skrivebeskyttede; eksplisitte maskinvaretester med endring og gjenoppretting er separate. Mockprotokoller, rendrede skjermbilder og tilfeldige inferensgrafer må ikke fremstilles som reell aksept av maskinvare/modellkvalitet.

Det komplette publiseringsverktøyet samler ledsagende vertsprosesser, mobilfiler, eksempelplugin, ONNX-runtime og tilgjengelige avhengighetsmerknader. Behold undermappene. Et vellykket bygg er ikke et signert installasjonsprogram eller juridisk utgivelsesgodkjenning. Kilde og bygg kan utvikle seg hver for seg; sammenlign dokumentasjonens øyeblikksbilde og commit med den installerte versjonen.

## Lisensiering, personvern og utgivelsesstatus

Den tiltenkte kommersielle modellen er et engangskjøp med varig bruk av den kjøpte hovedversjonen; endelig pris, tillatt antall enheter og oppgraderingsvilkår må fastsettes i de faktiske salgsvilkårene. Denne håndboken etablerer ikke et produksjonsklart kjøps-/aktiveringssystem. Daglig styring krever ingen skykonto.

Ikke distribuer proprietære SDK-binærfiler uten dokumentert tillatelse. Bevar lisens- og tredjepartsmerknader for hver medfølgende avhengighet, inkludert .NET- og ONNX-pakker. Modellvekter har egne vilkår. Den daterte lisensgjennomgangen er eldre enn enkelte nye avhengigheter og godkjenner ikke disse tilleggene. At repositoriet er offentlig, gir ikke i seg selv en generell åpen kildekode-lisens.

Reell leverandørmaskinvare, telefon-/nettbrettbruk via LAN, overganger mellom skjermer/DPI-innstillinger, langvarig bruk, fullstendig språklig gjennomgang, eksklusiv driverstøtte og nyttig AI-planleggingskvalitet krever fortsatt separat akseptarbeid. Utled aldri godkjenning bare fordi en funksjon finnes.

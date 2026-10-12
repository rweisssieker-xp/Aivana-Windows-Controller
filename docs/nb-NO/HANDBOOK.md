<!-- generated-doc locale=nb-NO source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../HANDBOOK.md)

# MAKRIVO — Brukerhåndbok

produsent: **Aivana**. Dokumentasjon snapshot: **2026-10-12**. Denne håndboken beskriver den nåværende arbeids-tre implementeringen. eksperimentelle integrasjoner og maskinvare-avhengige funksjoner identifiseres uttrykkelig. Historiske rapporter beskriver deres daterte snapshot, ikke en garanti for en ny bygning.

## Dokumentasjon og språk

Bruk dokumentasjonsspråkselektoren til å åpne den komplette manualen, funksjonsinnlegget, problemløsningen, utviklingsnotatene, forsknings- og historiske testrapporter i noen av 27 applikasjonsspråk. Genererte oversettelser er maskinassistert og har strukturelle kontroller; de har ikke alle mottatt innfødte talere. Tekniske identifiseringer og eksempler forblir uendret. Den opprinnelige kilden er fortsatt tilgjengelig for sammenligning. Oversatt lisensanalyse er informativt og erstattet ikke de opprinnelige tredjeparts juridiske vilkår eller en endelig EULA.

## Installasjon og første lansering

Det første målet er Windows 11 x64.Hold alle filer og subdirektiver sammen. `MAKRIVO.exe`. En kilde sjekk er ikke en installatør. selvinnholdspakker inkluderer deres .NET-løptid; rammeavhengige pakker krever den relevante .NET 10 Desktop og ASP.NET. ONNX og medfølgerhostene må inkluderes for deres respektive funksjoner.

1. Start MAKRIVO og velg grensesnittsspråket i Innstillinger. Den følger automatisk Windows. Keyboard layout er et separat sett.
2. Velg en profil, og legg deretter til en tile gjennom den søkbare handlingskatalogen. Gi det et gjenkjennelig navn og velg sine handlinger.
3. Sjekk ut trinnene og gi en korthet hvis det er nødvendig. Unngå kombinasjoner som allerede er brukt av Windows eller målet ditt.
4. Spar prosjektet. Ikke redde endringer er ikke den redde konfigurasjonen.
5. Test med en uskadelig handling, for eksempel å åpne et dokument, og sjekke utførelsen tilbakemelding.

Å lukke hovedvinduet forlater vanligvis applikasjonen i meldingsområdet. Double-klikk på tray-ikonen for å åpne den igjen, eller bruk tray-menyen for å komme ut. Å starte med Windows er valgfritt og i utgangspunktet funksjonelt. pause-kontrollen suspenderer båndene; ikonet viser en pause-bånd. Avbestill å kjøre makroer gjennom UI, tray eller konfigurert avbestillingskort; standard er `Ctrl+Alt+Escape`.

## Tiler, handlinger og kommandopalett

Tilene tilhører profiler og kan inneholde flere bestilte trinn. Redaktøren støtter Save/Discard, Preview, undo/redo og step reordering. Katalogen leverer forhåndsinnstillinger for programmer, dokumenter, URL-er, tekst, klippebord, skjemaer, media, lyd, vinduer, systemaksjoner og støttet integrasjoner. En forhåndsinnstilling som krever parametere åpnes som et utkast.

Åpne paletten med `Ctrl+K` mens MAKRIVO har fokus. Søk sparte handlinger, forutsetninger, profilering og verktøy. Trykk nøklene; Enter aktiverer; `Ctrl+Enter` redigerer et støttet element; Escape lukker. Noen klare kommandoer utføres umiddelbart, mens konfigurerte handlinger krever gjennomgang. En separat global korthet kan konfigureres; nye konfigurasjoner brukes `Ctrl+Shift+Space`.

Tekst og clipboard skritt støtte `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` og `{{user}}`. Datoen for formatering `yyyy-MM-dd`; Tidsformatet er `HH:mm`. Unna variabler forblir uendret.Clipboard lesing skjer bare når det er bedt om.En clipboard handling skriver tekst uten automatisk å passe den.

Handlinger lansert fra UI kan målrette den siste eksterne applikasjonen. Hvis målet forsvinner eller Windows blokkerer foregroundaktivasjon, aktiver manuelt målet og trekke tilbake. En vellykket nøkkellevering viser ikke at målet har fullført oppgaven.

## Profiler og arbeidsplass

Profiler kan matche et prosessnavne og en del av et vindu-tittel. Når de to er satt, må de to matche. Profileordre løser konkurransedyktige kamper; standardprofilen er fallback. Det er mulig å utvilsomt velge automatisk. Lock Profile forhindrer automatisk endringer for den nåværende sesjonen.

Åpne arbeidsplassinnstilling fra verktøy eller palett. Velg programmer, dokumenter, nettsteder, et lyd/display mål eller en OBS scene. Assistenten forbereder arbeidsplass, møte, streaming, forskning eller prosjektprofiler. Kontroller de genererte trinnene før du sparer. Institusjonen utfører ikke de forberedt handlingene.

## Macros, betingelser og opptak

Macros utfører sine skritt i rekkefølge og støtter ventinger, repetisjoner og avbestillinger. Den generelle gjentakelsegrensen er 100. Bruk forhåndsvisning før du kjører handlinger som skriver inn i en annen applikasjon eller endrer systemstatus.

Ventet for Target Window aksepterer en prosess, et overskrift og en timeout fra 1 til 600,000 ms. Minst ett filter er nødvendig. Et matchende synlig vindu må svare og bli foreground før de neste trinnene fortsetter. Et klart vindu viser ikke at et dokument eller en tjeneste er ferdig ladet.

Workflow-redaktøren legger til fil-eksister, prosess-løp og vindu-tittelvilkår. En falsk tilstand skiller skrittet og kan gå videre. Nektet betingelsen for å uttrykke en alternativ gren. Velg stop-on-error eller continue-on-errore med vilje; fortsettelse kan forlate senere trinn uten deres forventede forutsetninger.

Handlingsregisteret fanger kortskudd og venter, ikke vanlig skrive. Den støtter modifiserende kort og funksjonsnøkler. Flykten slutter å registrere. Sjekk ut listen og vedta den som et ubevarte tile-prosjekt, deretter lagre det uttrykkelig. UI-klikk er et separat verktøy beskrevet nedenfor.

## Keyboards, gesturer og lag

Tastatursiden inneholder oppdagede grensesnitt og referanseplasser. Et enkelt fysiske tastatur kan eksponere flere grensesnitt. Velg riktig språk og geometri; bare et produktnavn identifiserer ikke ISO eller ANSI layout.

Standard globale hotkeys gjelder over tastaturer. Raw Input bånd observerer en bestemt enhet og skannekode, inkludert støttet funksjon og media nøkler. **Raw Input Ikke undertrykker vanlig Windows En eksklusiv annen tastatur er ikke klar for produksjon.** En signert, lisensert og testet filter-driver backend er fortsatt nødvendig; ingen er installert av MAKRIVO.

Støtte spesialnøkkelbindinger kan bruke umiddelbar trykk, tap, hold eller dobbelt tap. Held-lagsnøkler midlertidig velge en annen kartlegging og gjenopprette grunnlaget på frigjøring; Layer-selektornøkler kjører ikke sin egen makro. 150 til 5,000 MS; dobbeltap vinduer fra 150 til 1,000 Disse kartene er forskjellig fra vanlige globale hotkeys.

Den spesielle nøkkelen oppdager kompatible nøkler uten å utføre sine tildelte handlinger under fangenskap. Diagnostikk forklarer utilgjengelige enheter, stoppet bånd, profilsammenheng og konflikter. Overlaget viser den valgte layouten og båndene; dens standard holdkort er `Ctrl+Alt+F1`.

| Integrasjon | Krav og begrensninger |
| --- | --- |
| Corsair | Running iCUE, SDK-tilgang og en kompatibel modell. G1–G6 og støttet RGB-funksjoner; proprietære Fn, hjul, lås og profilkontroller er vanligvis ikke utsatt for G-tasten API. |
| Logitech G | Original x64 G-key/LED SDK biblioteker krever uttrykkelig godkjenning. G-nøkkel hendelser identifiserer ikke flere identiske fysiske tastaturer. |
| Razer | Chroma RGB og standardinnføring; kart proprietære kontroller i Synapse hvor det er nødvendig. |
| SteelSeries | GameSense RGB og standardinnføring; kart proprietære kontroller i GG hvor det er nødvendig. |
| Wooting | Kompatibel opprinnelig x64 SDK for analoge verdier og grenser; normal HID-innføring forblir aktiv. |
| Andre tastaturer | Standard hotkeys eller støttet fysisk skannekode bånd. Fn og eiendomsstyringer avhenger av modellen og leverandørkonfigurasjonen. |

Produsentforskning beskriver dokumenterte ferdigheter, ikke implementert eller testet kompatibilitet med hver enhet som er oppført. Nye adaptere trenger fortsatt ekte hardware-aksept. Ikke laste ned SDK binære fra usikre kilder; SDK distribusjonsrettigheter er adskilt fra teknisk kompatibilitet.

## Lyd, skjermer og OBS

Lydsteg sett volum fra 0 til 100 prosent og mute/unmute/toggle støttet utgang eller mikrofon endpunkter. Velg en bestemt sluttpunkt eller bruk Windows standard. En savnet mål produserer en feil i stedet for å stille kontroll over en annen enhet. Fjern enheten etter gjenoppføring. Endpoint-kontroll er ikke et krav om uavhengig blandingsstøtte per applikasjon.

Vis handling øke/minere lyshet med 10 prosentpoeng eller sette en verdi fra 0 til 100. Interne skjermer bruker støttet WMI-grensesnitt; eksterne skjermer krever støtte DDC/CI og vanligvis monitorn-menyen. Dokker, adaptere og kabler kan blokkere kontroll. Velg alle støttede skjermer eller én monitor. Rekoneksjon kan endre tilgjengelige identiteter; gjennomgå det redde målet. Manglende lesinger er vist som utilgjengelige i stedet for målede verdier.

OBS krever WebSocket v5 aktivert i OBS Verktøy. Konfigurer slutten, sjekk koblingen og last inn/inntjeningene før du sparer. Støtte handlinger inkluderer scene skift, strøm/rekord start/stop/toggle, innspill pause/resume og input mute. Passord kan lagres i Windows Credential Manager og er utelukket fra konfigurasjon eksporter. En vellykket OBS kommando etablerer ikke alle nedstrømningseffekter.

## Resultat verifisering og eksekusjonshistorie

Du kan sjekke resultatene for passende skritt. Appen skiller ut forhåndsvisningskontroller, bekreftede effekter, feil, kansellering og effekter som ikke kan verifiseres. Prosessens eksistens er ikke applikasjonsberedskap. Relative endringer, kortsikter og tekstinnføring kan ikke motta en form for suksess. Sjekk aktivitetsloggen og verifiseringshistorien når en handling mislykkes. Konfigurasjonsfeil bør rettes før full makro.

## UI automatisering og klikk registrering

Legg til en UI Automation-steg, velg operasjonen og fange målet under indikatoren etter den vist forsinkelsen. Fange alene utfører ingenting. sjekk selektoren og krever en unik match. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` og `WaitState`. Passordfelter, funksjonshemmede kontroller og ambisiøse selektorer blir avvist.

UI-automasjon kjører i en begrenset følgesvenn. Det avhenger av tilgangssupporten og tillatelsene til målet Windows. Klikk-opptaket fanger støttede handlinger og pauser; det registrerer ikke generell tekstinnføring. Sjekk ut og validere prosjektet før du vedtar det. Endringer i en ekstern app kan invalidere selektorer. Prosessisolering begrenser kollisjon og avbestilling, men er ikke en sikkerhetssandbokse.

## Plugins

Åpne utvidelser for å sjekke et pakke før du importerer det. Pakkestrukturen, manifest og hasher er sjekket. Importering begynner med funksjonshemning. Les de erklærte handlingene og uttrykkelig stole på utgiveren før du gjør det mulig. Plugins kjører med brukerenes tillatelser i en separat prosess; denne prosessen er ** ikke en sandboks**. Bare installere pakker du stoler på.

Administratoren støtter eksport, deaktivering, avinstallering og gjenoppretting av en tidligere versjon. Manglende avhengighet blokkerer invitasjon. Eksempelpakken gir countdown og lokale statlige handlinger. Den støttet skjemmen er en dokumentert JSON-skema, ikke alle mulige JSON-skjema-funksjoner. Det finnes ingen offentlig pluginmarked.

## Mobilpanel

1. Åpne Mobile Panel, velg en lokal IP og velg uttrykkelig profillene som skal eksponeres. Den standard loopback-adressen er bare tilgjengelig på PC-en selv; velg riktig LAN-adresse for en telefon eller nettbrett.
2. Start panelet og les HTTPS-adressen, sertifiser fingeravtrykket og gjennomsnittlig seks-digit parende koden.
3. På en enhet i samme pålitelige nettverk, åpne adressen. Sammenlign fingeravtrykket SHA-256 gjennom en pålitelig kanal før du stoler på det lokale sertifikatet. Appen installerer ikke et pålitelig sertifikat automatisk.
4. Bruk koden som vises og prøv en ufarlig tile. En manuell Windows brannmur tillatelse kan være nødvendig for LAN-tilgang. Ikke utsett porten for Internett.
5. Stopp panelen når den er ferdig, eller tilbakekall klienter gjennom appen.

Private servernøkler er beskyttet med Windows DPAPI; klientkrediter lagres som hasher. Endring av IP eller fornyelse av sertifikatet kan kreve fornyet tillit. Panelet viser handling og verifiseringstilstander, ikke alle mulige live sensorer. Browser-layout og lokale HTTPS-kontroller erstattes ikke av ekte telefon/tablet LAN-aksept.

## Møte med lokal assistent

Denne valgfrie eksperimentelle assistenten er deaktivert. Åpne den fra Verktøy, eksplisitt aktiverer den, velg en runtime/model og legg til bare de ressursene du ønsker å vurdere. Sett målet, opprette et utkast, sjekk uoppfylte forespørsler og overføre resultatet til den vanlige redaktøren. Ingenting utføres automatisk; sparing og kjøring forblir eksplisitte handlinger. Det er ingen automatisk runtime fallback.

| Runntid | Innstillinger |
| --- | --- |
| Ollama | Separert lokal service og en lokalt installert modell på `127.0.0.1:11434`. Deklarerte fjern/cloud-modeller er avvist. |
| LM Studio | Lokalt server med lokalt lastet modell og JSON-skema støtte på `127.0.0.1:1234`. Ikke aktiverer fjerde routing for lokal behandling. |
| llama.cpp | Lokal `llama-server` og kompatibel modell med JSON-skema støtte på `127.0.0.1:8080`. |
| ONNX Runntid GenAI CPU | Trykt kompatibel lokal GenAI tekstmodell mappe; velg `genai_config.json`. Inference bruker en bundet partnerprosess uten HTTP. |

HTTP-klienter bruker bare loopback, bruker ikke proxyer og følger ikke omdirigeringer. MAKRIVO kan ikke pålitelig bestemme om en kompatibel lokal server internt ringer en fjerntjeneste. Autentiserte lokale servere kan ikke konfigureres i denne versjonen. Modellvekter, Python og lokale inferensserver lastes ikke ned eller installeres automatisk.

ONNX pakker trenger `tokenizer.json` og en gyldig chat-tema. Støtte skjemaer i `tokenizer_config.json` er en linje `chat_template`, en ordbok med en linje`default`, eller en ubestridelig navn/default skjema liste. Hvis malen er savnet eller null, `chat_template.jinja` brukes som en fallback. Dyktige, ambisiøse, overdrevne eller feilaktige maler avvises. Arbitrære ONNX filer, innebygging/visjonsmodeller og GPU-enbart pakker er ikke utvekslbare med kompatible GenAI tekstmodeller. Den bundet GenAI runtime versjonen er 0.17.1, CPU. Bruk bare pålitelige modeller; prosess separasjon er ikke en sandboks.

Assistenten støtter utvalgte programmer uten argumenter, utvalgte dokumenter og nettsider, og mikrofonen støtter når det er uttrykkelig forespurt. Det skaper ikke meldinger, avtaler, vilkårlige skript, gratis klikk, nedlastinger eller headset skift. Målet og ressursnavnet sendes til den valgte lokale driftstiden; ressursveier og filinnhold leveres ikke separat. Et mål som er angitt av brukeren, kan selv inneholde personopplysninger. En lokal runtime egen nettverksadferd forblir brukerens ansvar.

Budsjettet for ONNX forespørsel er 90 sekunder inkludert modelllasting, opp til 3,072 prompt tokener og 1,024 generert tokener, også begrenset av modell kontekst. Misformede/trunkede JSON-IDer og ukjente ressurser er avvist. Avbestill eller avslutte en forespørsel. En tilfeldig modell-runtime test etablerer utførelse, ikke nyttig planlegging kvalitet eller tidsbesparelser. Lokale eksperimentlogger inneholder hendelsesidentifikatorer og timinger, ikke måltekst eller dokumentinnhold; de lastes ikke opp automatisk.

Den senere lille Instruct-modellen produserte null gyldige planer i fire tyske/engelsk forsøk. Native inference fungerte, men modellen mislyktes i den strenge planleggingsavtalen. Dette er et negativt kvalitetsresultat, ikke en anbefalt modell eller en bevist nyttig assistent. Se den datote lokale testrapporten for nøyaktig modellrevisjon og grenser.

## Import, eksport, synkronisering og backup

Sjekk innføringsforutsetninger før du erstattes. Importerte handlinger begynner å slå av, og bærbar synkronisering fjerner maskinvare-spesifikke bånd. Gjennomføre enheter og gjøre det mulig å handle med vilje etter inspeksjon. Oversikt over tekst/handling innhold før deling av en eksport; utelukkelse av passord gjør ikke alle brukeren opprettet handling innhold ikke følsom.

Profilsynkronisering utveksler filer i en valgt mappe, valgfritt en som administreres av din eksisterende cloud-klient. MAKRIVO gir ingen cloud-konto og lagrer ingen cloud-krediteringer. Revisjoner forhindrer stille oversettelser. Inkludering av handlingsinnhold er et uttrykkelig valg. Oppdatere konfigurasjonen din før du importerer en utskiftning. Standardkonfigurasjonen er `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; det historiske katalognavnet blir bevaret for kompatibilitet. Slutt appen før du kopierer/oppretter den manuelt. Lagrede SDK-godkjenninger og OS-beskyttede akkrediteringer kan trenge å gjenopprettes på en annen PC.

## Problemløsning

| Symptomer | Hva å sjekke |
| --- | --- |
| Shortcut gjør ingenting | Appen kjører, båndene er ikke pause, riktig aktiv profil, sparet/aktivert tile, kortskudd og tilgjengelige mål. |
| Målvinduet kan ikke aktiveres | Manuelt aktiverer du det. Windows forhåndsbegrensninger og privilegier kan blokkere automatisering. |
| Spesielle nøkler er ikke funnet | Leverandørstjeneste/SDK/modellstøtte; konfigurere eiendomsstyringer for å utstede en støttet standardnøkkel hvis mulig. |
| Den andre tastaturen er også vanlig | Forventet med Raw Input. Eksklusiv supresjon er ikke implementert for produksjon. |
| Lyd eller lys ikke tilgjengelig | Oppdater enheter; sjekk utvalgt identitet, sluttspill sesjoner, WMI/DDC/CI støtte og dokk/kabel restriksjoner. |
| OBS Offline | WebSocket v5 aktivert, riktig endpoint/passord, tilgjengelig service og gyldige scene/input navn. |
| UI automatisering feilsomt | Companion host inkludert, unik gyldig selektor, tilgjengelig aktivert mål, støttet kontroll og tillatelser. Returne en overgangstid bare etter å ha gjennomgått målet. |
| Mobiltelefonen er uoppnåelig | LAN i stedet for loopback adresser, samme nettverk, server kjører, godkjente profiler, sertifiserte fingeravtrykk/trust og manuelle brannmur tillatelser. |
| Plugin svikter | Pakkeintegritet, eksplisitt tillit/tillit, avhengigheter, protokollkompatibilitet og gjennomføringslogg. |
| Forespørselen svikter | Korrekt valgt lokal driftstime, service/modell klar, støttet JSON-skjema, pålitelig kompatibel modellmapp og forespørselsgrenser. |
| Layout eller tekst virker feil | Skille UI-språket fra tastaturgeometri; revise vinduet, sjekke skjermskalaen og rapportere nøyaktig side/språk/bygg. |

For en nyttig feilmelding inkluderer Windows versjon, app build/commit, språk, skjermskala, maskinvaremodell, en minimal sekvens og den relevante redigerte loggen. Fjern personlige veier, handlingsinnhold og akkrediteringer før publisering.

## Bygge, teste og pakke

Bruk SDK-en i `global.json` Fra repository root:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integrasjonstester kan åpne isolerte vinduer og midlertidig konfigurere/leie støttet leverandørbindinger. Utfør dem uten en annen kontrollør som eier disse båndene. De standard lys/audio-testene er bare lesende; eksplisitt hardware-rundtrip testene er separate. Mock-protokoll, renderte skjermbilder og tilfeldige inferensgrafer må ikke representeres som ekte maskinvare/modellkvalitet akseptering.

Den komplette utgiveren samler medfølgerhost, mobile eiendeler, eksemplet plugin, ONNX runtime og tilgjengelige avhengighet meldinger. Hold underdirektivet ditt. En vellykket bygging er ikke en signert installatør eller lovlig frigjøringsgodkjenning. Kilde og bygninger kan utvikle seg separat; sammenligne dokumentasjon snapshot og engasjere seg med den installerte versjonen.

## Licensing, personvern og frigjøringsstatus

Den hensiktsmessige kommersielle modellen er en enkeltkjøp med permanent bruk av den kjøpte store versjonen; sluttpris, enhetstillatelse og oppgraderingsbetingelser må fastsettes i de faktiske salgsbetingelsene. Et produksjons- og aktiveringssystem er ikke etablert i denne håndboken. Det er ikke nødvendig å ha en skykonto for daglig kontroll.

Ikke distribuere eiendomsbaserte SDK-binarer uten bevist tillatelse. Oppbevar lisens og tredjeparts meldinger for hver sendt avhengighet, inkludert .NET og ONNX pakker. Modellvekt har sine egne vilkår. Den daterte lisensrevisjonen gir noen nye avhengigheter og er ikke en clearance for disse tilleggene. Repository er offentlig, og gir ikke en generell åpen kilde lisens.

Virkelig leverandør maskinvare, telefon/tablett LAN-operasjon, blandet monitor/DPI-overganger, langvarig bruk, fullstendig språkkurs, eksklusiv driverstøtte og nyttig AI-planlegging kvalitet forblir separat aksepteringsarbeid. Aldri gi godkjennelse fra egen tilstedeværelse.

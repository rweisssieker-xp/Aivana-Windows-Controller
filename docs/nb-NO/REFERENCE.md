<!-- generated-doc locale=nb-NO source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../README.md)

# MAKRIVO

[Komplett dokumentasjon på alle 27 språk](README.md) · [Full brukerhåndbok](HANDBOOK.md)

**Ved Aivana.** [Nåværende funksjonsinventor og frigjøringsbegrensninger](FEATURES.md) · [Utvidelsesrapport](testing/2026-10-12-six-extensions.md) · [Lokalt AI-assistent](testing/2026-10-12-local-meeting-assistant.md)

** Engelsk (US) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

En lokal Windows-hurtigtastkontroller med fliser i Stream Deck-stil, profiler, makroer og tastaturoppdagelse. Grensesnittet støtter 27 språk. Daglig bruk krever ingen konto eller skytilkobling.

[Dokumentasjon](README.md) · [Verifikasjon](VERIFICATION.md) · [Feature testrapport](testing/2026-10-09-usp-release.md)

## Å begynne

Etter publisering ligger programmet på `artifacts/win-x64/MAKRIVO.exe`. Hold hele utdatamappen samlet; den kan kopieres andre steder. Windows 11 x64 er det første målet. Byggartefakter er utelatt fra Git; en kildekodeutsjekking inneholder ingen ferdigbygd kjørbar fil.

Installere .NET 10 SDK angitt i `global.json` for å bygge og kjøre fra kilden:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publisere en selvinnholdsbygg uten å kreve en separat installert .NET-løptid:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Basisk arbeidsflyt

1. Velg en profil og opprett en handlingsflis.
2. Angi navn, ikon, farge og hurtigtast. Ctrl/Alt/Shift-kombinasjoner reduserer utilsiktet aktivering under skriving.
3. Legg til ett eller flere trinn: programmer, filer, nettsteder, tekst, hurtigtaster, medier, volum, profilbytte eller forsinkelser.
4. Lagre. Endringer forblir et utkast til de lagres; forkast gjenoppretter den lagrede versjonen. Flisens avspillingsknapp og hurtigtast bruker samme utførelsesvei.
5. Omordne fliser og makrotrinn med dra og slipp eller pilknapper. Maler forenkler oppsettet. Makroer kan gjentas opptil 100 ganger. Avbryt via grensesnittet, systemstatusfeltets meny eller som standard Ctrl+Alt+Escape.

Programspesifikke profiler bruker et prosessnavn uten full sti. Profilrekkefølgen avgjør like treff; standardprofilen er reservevalget. Automatisk profilvalg må aktiveres i Innstillinger.

Lukking av hovedvinduet lar som standard appen kjøre i systemstatusfeltet. Dobbeltklikk på ikonet for å åpne igjen; avslutt via ikonmenyen. Oppstart med Windows er valgfritt og deaktivert som standard. Hurtigtaster kan settes på pause sentralt.

## Handlinger og kommandoer

Legg til handling åpner en søkbar katalog med 90 forhåndsinnstillinger i åtte kategorier. Hver har en beskrivelse og forhåndsvisning. Makroredigeringen bruker samme katalog til å legge til eller erstatte trinn. Valg av en forhåndsinnstilling i denne redigeringsarbeidsflyten oppretter et utkast uten utførelse.

Kommandopaletten åpnes fra toppområdet eller med Ctrl+K i appen. Den søker i lagrede handlinger på tvers av profiler, katalogforhåndsinnstillinger, profilbytte, navigasjon, pause/gjenoppta, makroavbrytelse og import/eksport. Filtrer etter lagrede handlinger, katalogfunksjoner eller appkommandoer. Piltaster velger, Enter aktiverer, Ctrl+Enter redigerer en redigerbar oppføring og Escape lukker. Favoritter vises først. Bruksklare katalogkommandoer kan kjøre umiddelbart; forhåndsinnstillinger som krever oppsett, åpner et utkast. Innstillinger kan definere en global palettsnarvei. Nye konfigurasjoner bruker Ctrl+Shift+Space; eksisterende konfigurasjoner får ikke automatisk en ny global snarvei.

Lydhandlinger setter volumet nøyaktig fra 0 til 100 prosent og demper utgangs- og mikrofonenheter, opphever demping eller veksler dempestatus. Velg et bestemt endepunkt eller la det være uvalgt for Windows-standarden. Et utilgjengelig lagret endepunkt gir en feil fremfor å styre en annen enhet. Oppdater enheter laster tilkoblede enheter på nytt.

OBS Studio bruker WebSocket v5: start/stopp/veksle/pause/gjenoppta opptak; start/stopp/veksle strømming; bytt scener; veksle demping av inndata. Aktiver WebSocket-serveren under Verktøy i OBS og angi endepunktet i redigeringen. Kontroller tilkobling og Last inn navn leser OBS-versjon, scener og inndata uten å starte opptak eller strømming. Velg navn fra listen eller skriv dem manuelt. Passord kan lagres i Windows Credential Manager og utelates fra eksporterte konfigurasjoner. Utilgjengelige OBS-tilkoblinger rapporterer en feil.

Profiler kan samsvare med et prosessnavn og en del av en vindustittel. Begge angitte vilkår må samsvare. Lås profil forhindrer automatisk bytte i økten. Registrer vilkår fra forgrunnen bruker det siste fokuserte eksterne programmet.

Tekst- og utklippstavlehandlinger støtter `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` og `{{user}}`. Datoer bruker `yyyy-MM-dd`; tider bruker `HH:mm`. Ukjente plassholdere forblir uendret. `{{profile}}` identifiserer profilen til den startede handlingen; `{{process}}` identifiserer den eksterne programkonteksten. Utklippstavleinnhold leses bare når denne plassholderen brukes. Utklippstavlehandlingen skriver tekst, men limer ikke inn automatisk.

Vindushandlinger minimerer, maksimerer, gjenoppretter eller plasserer et vindu på venstre/høyre side av skjermens arbeidsområde. Systemhandlingen låser Windows. Disse handlingene kjøres bare ved uttrykkelig kall.

Forhåndsvisning viser trinn, gjentakelser og variabler uten utførelse. Angre og Gjør om beholder opptil 100 utkasttilstander; tekstfelt beholder sin innebygde Ctrl+Z-redigering. Merk favoritter i redigeringen eller flismenyen og filtrer dem på dashbordet. Kjøringstall gjelder bare den gjeldende økten.

Tekst-, hurtigtast- og vindushandlinger startet fra grensesnittet aktiverer det siste eksterne målvinduet. Paletten viser denne målkonteksten. Hvis målet er lukket eller Windows nekter aktivering, stopper kjøringen med en melding.

## Keyboard hjelp og feedback

Spesialtastveiviseren finnes på tastatursiden og i paletten. Den viser G-taster rapportert av iCUE. Oppdag tastetrykk registrerer dem midlertidig uten å utføre tilordnede handlinger. Normale bindinger kommer tilbake etter oppdagelse, avbrytelse eller lukking. En katalogfunksjon og valgt tast åpnes som et ulagret utkast; dupliserte bindinger kontrolleres.

Tastaturoverlegget viser valgt referanseoppsett og aktive bindinger. Hold Ctrl+Alt+F1 nede eller slå det av/på fra paletten eller tastatursiden. Det tar ikke fokus og lar museklikk passere gjennom. Profil- og bindingsendringer oppdateres mens det er synlig; vanlige Windows-tastetrykk fremheves. Produsentspesifikke kontroller gir kanskje ikke et tilsvarende Windows-tastesignal.

Tilbakemelding om kjøring viser venting, trinn-/gjentakelsesfremdrift, fullføring, avbrytelse eller feil uten å ta fokus eller avsløre inntastet tekst. Kommandoer utført bekrefter kommandokjøring, ikke en uavhengig avlest livetilstand i et tredjepartsprogram. Feildetaljer vises i aktivitetsloggen.

## Utstyr og Corsair

Corsair G1–G6 kan tilordnes direkte via den offisielle iCUE SDK. Velg enhet og tast i handlingsredigeringen og lagre, eller forbered et utkast fra K100-tastaturvisningen. Bindinger gjelder valgt enhet og aktiv profil; pause deaktiverer dem også. iCUE må kjøre og tillate tilgang. Fn, iCUE-hjulet samt profil- og låsekontroller kan ikke tilordnes via denne G-key API.

Enhetssiden viser Windows tastaturgrensesnitt og oppdaterer seg periodisk og når enhetene kobles til. En fysisk tastatur kan eksponere flere HID-grensesnitt. Standard globale hotkeys fungerer over produsenter.

Corsair-oppdagelse bruker tilgjengelig enhetsinformasjon. RGB-styring krever kompatibel maskinvare, kjørende iCUE og den offisielle iCUE SDK; appen rapporterer faktisk tilgjengelighet. Andre produsenter får for tiden oppdagelse og standardhandlinger. Utenfor Corsair G-key-integrasjon virker proprietære kontroller når Windows mottar dem som vanlig inndata. Appen installerer ingen tastaturdriver.

## Språkene

Innstillinger tilbyr 27 språk: tysk, engelsk, fransk, spansk, italiensk, portugisisk, nederlandsk, polsk, tsjekkisk, slovakisk, ungarsk, rumensk, bulgarsk, gresk, dansk, svensk, norsk, finsk, estisk, latvisk, litauisk, slovensk, kroatisk, ukrainsk, russisk, tyrkisk og serbisk med latinsk skrift. Automatisk følger Windows-visningsspråket. Oversettelser følger med lokalt. Oppsett, hurtigtaster og brukeropprettede navn er uavhengige av grensesnittspråket.

Komplett lagringsdokumentasjon er tilgjengelig i alle 27 søknadsspråk gjennom [Språk Index](README.md). Oversettelser er maskinassisterte med originalkildelenker og strukturkontroller; morsmålsgjennomgang er separat. Appen godtar også regionkodene `en-US`, `de-DE`, `fr-FR` og `it-IT` og bruker deres tallformater. Eksisterende språkkoder forblir kompatible.

## Layout og produsentforskning

Tastaturvisningen tilbyr ISO-DE/ANSI-US-referanser i full størrelse og uten numerisk del samt Corsair K100 RGB-oppsett. Geometrien viser posisjoner, brede/høye taster og ISO Enter. Standardoppsett er generiske referanser. K100-oppsett følger dokumenterte kontroller uten å hevde millimeternøyaktige mål. Velg riktig språk-/geometrivariant: et modellnavn eller USB VID/PID fastslår ingen bestemt variant.

Tilordning bestemmer fysiske skannekoder med det aktive Windows-inndataoppsettet. Arbeidsflyten forbereder et utkast og beholder lagrings-/konfliktkontroller. Standardbindinger er globale Windows-hurtigtaster, ikke isolert til ett tilkoblet tastatur. Corsair G1–G6 bruker enhetsspesifikk iCUE-integrasjon. Andre proprietære kontroller krever en adapter eller produsentkonfigurerte hurtigtaster. Hoved-Enter og numerisk Enter deler virtuell tastidentitet for globale hurtigtaster.

Den [produsentforskning](research/tastaturhersteller-sonderfunktionen.md) og [CSV](research/tastaturhersteller-sonderfunktionen.csv) dekker 74 merke-/produsentgrupper og skiller mellom konfiguratorer, inndata-SDK-er og belysnings-API-er. [Layout bevis](research/keyboard-layout-evidence.md) dokumenterer forskjeller i modell, SKU og moduler. Forskningsdekning betyr ikke at maskinvarestøtte er implementert.

## Dataene

Konfigurasjon lagres i lokale programdata. Import/eksport bruker JSON. Import forhåndsviser konfigurasjonen, erstatter gjeldende konfigurasjon etter bekreftelse og deaktiverer importerte bindinger til de aktiveres manuelt. Teksthandlingsinnhold samles ikke inn som en tastetrykklogg. Credential Manager-passord er atskilt fra eksporterte profiler.

## Verifikasjon

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows-kontroller kan undersøke maskinvare og midlertidig konfigurere/frigi støttede iCUE-bindinger. Kjør dem uten en annen kontroller som bruker disse bindingene. Kontroller av lydendepunkter endrer ikke gjeldende volum eller dempeinnstillinger.

Native inndata kan ikke omgå Windows-rettigheter. En normalt startet kontroller kan være ute av stand til å sende inndata til et administratorprogram. Reserverte eller opptatte hurtigtaster vises som konflikter.

Den [Dokumentasjon Index](README.md) Linker historiske design, planer, forskning og dato testrapporter. Historiske planer beskriver sin registrerte rekkevidde, ikke en nåværende garanti for funksjonsstøtte.

### Workflow-templer og målvinduer venter

Katalogen inneholder seks redigerbare arbeidsflyter: arbeidsplass, møte, strømming, forskning, prosjektbytte og tekstfragment. Søk etter `workflow.` i katalogen eller kommandopaletten. Valg av en arbeidsflyt åpner et ulagret utkast; den kjører aldri umiddelbart. Alle trinn beholdes når en arbeidsflyt legges til en eksisterende makro eller tilordnes en Corsair-tast. Konfigurer eksempel-URL-er, programstier, profiler og OBS-scener før lagring.

Trinnet **Vent på målvindu** godtar et prosessnavn (med eller uten `.exe`), en del av tittelen uten skille mellom store og små bokstaver og en tidsgrense på 1–600000 ms. Minst ett filter kreves; begge må samsvare hvis begge angis. Makroen fortsetter først når et samsvarende synlig vindu svarer og kommer i forgrunnen. Bruk en bestemt tittel når flere vinduer deler en prosess. Avbrytelse eller tidsavbrudd stopper etterfølgende trinn. Et klart vindu garanterer ikke at et dokument er lastet, eller at en ekstern tjeneste som OBS WebSocket er tilkoblet.

Skjermlysstyrke: søk etter **Øk skjermlysstyrken**, **Reduser skjermlysstyrken** eller **Skjermlysstyrke på 50 prosent** i handlingskatalogen. Relative handlinger endrer lysstyrken med 10 prosentpoeng; faste verdier går fra 0 til 100. Støttede interne skjermer bruker WMI; kompatible eksterne skjermer krever DDC/CI aktivert i menyen. Som standard påvirker handlingen alle støttede skjermer; en bestemt skjerm kan velges i redigeringen. Tilkoblinger og dokker kan begrense støtten.

### Utvidet kontroll

Åpne kommandopaletten med **Ctrl+K mens appvinduet har fokus**. Søk etter **Arbeidsflytdesigner**, **Ta opp makro**, **Synkroniser profiler** eller **Tastdiagnostikk** (ordlyden følger valgt språk). Arbeidsflytdesigneren redigerer det valgte flisutkastet: fil-/prosess-/vindusvilkår, antall trinn som hoppes frem og stopp/fortsett ved feil. Opptakeren registrerer hurtigtastkombinasjoner og ventetider, ikke vanlig tekstinntasting; Escape stopper opptaket. Gjennomgå resultatet før overtakelse.

I et lyssteg velger du alle støttet skjermer eller en spesifikk monitor. En utilgjengelig utvalgte skjerm produserer en feil i stedet for å bytte et annet skjerm. Dashboardet viser levende lys, lyd og OBS lesinger når deres respektive integrasjoner er tilgjengelige; en ikke tilgjengelig lesing presenteres ikke som et målt verdi.

Tilordne en støttet fysisk spesialtast i flisredigeringen og velg umiddelbart trykk, kort trykk, langt trykk eller dobbelttrykk. Lagvelgere aktiverer midlertidig en annen tilordning mens de holdes nede, og gjenoppretter forrige lag når de slippes. Gester og lag gjelder Corsair SDK G-taster og enhetsspesifikke standardspesialtaster, ikke vanlige globale hurtigtaster. Standardtastregistrering støtter F13–F24 og medietaster. Proprietære Fn-, profil- og dreiekontroller krever fortsatt en kompatibel leverandøradapter eller leverandørtilordning til en standardtast. Raw Input observerer tasten og undertrykker ikke dens normale Windows-atferd. Tastdiagnostikk forklarer profil-, pause-, tilkoblings- og bindingsproblemer.

Profilsynkronisering utveksler filer via en mappe du velger, også en mappe administrert av din eksisterende skyklient. Gjennomgå forhåndsvisningen før import; importer starter deaktivert og fjerner maskinvarespesifikke tilordninger. Revisjoner forhindrer ubemerkede overskrivinger, og inkludering av handlingsinnhold er et uttrykkelig valg. MAKRIVO tilbyr ingen skykonto og beholder ingen skylegitimasjon. Systemstatusfeltet bruker nå MAKRIVOs eget blågrønne M-ikon og viser et ravfarget pausemerke ved pause.

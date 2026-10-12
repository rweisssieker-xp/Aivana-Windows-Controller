<!-- generated-doc locale=nb-NO source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../README.md)

# MAKRIVO

[Komplett dokumentasjon på alle 27 språk](README.md) · [Full brukerhåndbok](HANDBOOK.md)

**Ved Aivana.** [Nåværende funksjonsinventor og frigjøringsbegrensninger](FEATURES.md) · [Utvidelsesrapport](testing/2026-10-12-six-extensions.md) · [Lokalt AI-assistent](testing/2026-10-12-local-meeting-assistant.md)

** Engelsk (US) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

En lokal Windows hotkey-kontroller med Stream Deck-stil, profiler, makroer og tastaturdeteksjon. Grensesnittet støtter 27 språk. Ingen konto eller sky-tilkobling er nødvendig for daglig bruk.

[Dokumentasjon](README.md) · [Verifikasjon](VERIFICATION.md) · [Feature testrapport](testing/2026-10-09-usp-release.md)

## Å begynne

Etter utgivelsen er søknaden på `artifacts/win-x64/MAKRIVO.exe`. Hold hele utgangsfilen sammen; du kan kopiere den andre steder. Windows 11 x64 er det opprinnelige målet. Build artefakter er utelukket fra Git; en kilde sjekk inkluderer ikke en forhåndsbygd utførbar.

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

1. Velg en profil og opprett en handlingstille.
2. Sett navn, ikon, farge og tastaturkort. Ctrl/Alt/Shift-kombinasjoner reduserer tilfeldig aktivering mens du skriver.
3. Legg til ett eller flere trinn: applikasjoner, filer, nettsteder, tekst, skjemaer, media, volum, profiloverføring, eller forsinkelser.
4. redde seg. Endringer forblir en utkast til spart; discard gjenoppretter den sparte versjonen. Tile Play-knappen og dens hotkey bruker den samme utføringsveien.
5. Reorder tiltak og makro trinn med drag-and-drop eller armbånd. Modeller forenkler innstillingen. Macros kan gjentas opp til 100 ganger. Avbestill gjennom grensesnittet, system tray-menyen eller Ctrl+Alt+Escape som standard.

Applikasjonspesifikke profiler bruker et prosessnavne uten sin fulle vei. Profileordre bryter forbindelser; standardprofilen er fallback. Automatisk profilvalg må være aktivert i Innstillinger.

Å lukke hovedvinduet holder appen i systemet som standard. Double-klikk på tray-ikonet for å åpne den igjen; bruk tray-menyen for å komme ut. Startup med Windows er valgfritt og deaktiveres som standard. Hotkeys kan stoppes sentralt.

## Handlinger og kommandoer

Legg til handling åpner en søkbar katalog med 90 forhåndsinnstillinger i åtte kategorier. Hver av dem har en beskrivelse og forutsetning. Makro-redaktøren bruker samme katalog for å legge til eller erstatte trinn. Ved å velge en forhåndsinnstilling i denne redigeringsarbeidet skaper du et utkast uten utførelse.

Kommandepallet åpnes fra header eller med Ctrl+K inne i appen. Det ser etter sparte handlinger gjennom profiler, katalogpreciper, profiloverføring, navigasjon, pause/resume, makroannullering og import/eksport. Filtrering av sparte handlinger, katalogfunksjoner eller app-kommandoer. Trykk nøklene, Enter aktiverer, Ctrl+Enter redigerer en redigerbar inngang, og Escape lukker. Favorittene vises først. Forberedte katalogkommandoer kan kjøre umiddelbart; forhåndsinnstillinger som krever installasjon åpne et utkast. Settings kan definere en global palettkort. Nye konfigurasjoner bruker Ctrl+Shift+Space; eksisterende konfigurasjoner mottar ikke en ny global korthet automatisk.

Audio-aksjoner fastsetter volum nøyaktig fra 0 til 100 prosent og myte, unmute eller toggle utgang og mikrofonenheter. Velg en bestemt sluttpunkt eller la den unnsette for Windows standard. En ikke tilgjengelig sparet sluttpunkt produserer en feil i stedet for å kontrollere en annen enhet. Refresh Devices lader tilkoblet enhet.

OBS Studio bruker WebSocket v5: start/stop/toggle/pause/resume opptak; start/stop /toggle streaming; endring av scener; toggle inngang mute. Oppsett WebSocket-serveren under Verktøy i OBS og sette sin sluttpunkt i redaktøren. Sjekk Connection og Load Names leser OBS versjon, scener og innspill uten å starte en opptak eller strøm. Velg navn fra listen eller skriv dem manuelt. Passord kan lagres i Windows Credential Manager og er utelukket fra eksporterte konfigurasjoner. Untilgjengelige OBS koblinger rapporterer en feil.

Profiler kan matche et prosessnavne og en del av et vindu-tittel. Begge spesifiserte forhold må matche. Lock Profile forhindrer automatisk skift for økten. Capture Conditions fra Foreground bruker den siste fokuserte eksterne applikasjonen.

Tekst og clipboard-tak `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, og `{{user}}`. Datoer bruk `yyyy-MM-dd`; Tidsbruk `HH:mm`. Ukjente steder forblir uendret. `{{profile}}` Identifiserer initiativets profil; `{{process}}` identifiserer den eksterne applikasjonskonteksten. Clipboardinnhold leses kun når den plasseringsholderen brukes..

Vinduaksjoner minimere, maksimere, gjenopprette eller arrangere et vindu på venstre/ høyre side av monitorens arbeidsområde. Systemet er låst Windows. Disse handlingene utføres kun når det er uttrykkelig oppfordret.

Forhåndsvisning viser trinn, repetisjoner og variabler uten utførelse. Undo og Redo beholder opptil 100 utkaststater; tekstbokser beholder opprinnelige Ctrl+Z-redigering. Markere favoritter i redigerings- eller tile-menyen og filtrere dem på dashboardet. Utførelsesberegninger gjelder kun for den nåværende sesjonen.

Tekst, kortskudd og vinduhandlinger som lanseres fra grensesnittet aktiverer det siste eksterne målvinduet. Det viser dette målkontekstet. Hvis målet er stengt eller Windows nekter å aktivere, stopper utførelsen med en melding.

## Keyboard hjelp og feedback

Special-Key Wizard er på tastatursiden og i paletten. Det lister G-toppene rapportert av iCUE. Detect Key Press fanger dem midlertidig uten å utføre sine tildelte handlinger. Normale bånd tilbake etter oppdagelse, kansellering, eller lukking. En katalogfunksjon og valgt nøkkel åpnes som et uavsluttet utkast; duplikate bånd blir sjekket.

Keyboard Overlay viser den valgte referanse layouten og aktive bånd. Hold Ctrl+Alt+F1 eller ta den fra paletten eller tastatursiden. Det tar ikke fokus og lar musen klikke gjennom. Profile og bindende endringer oppdateres mens synlig; vanlige Windows nøkler er understreket. Produsent-specifikke kontroller kan ikke gi en tilsvarende Windows nøkkel signal.

Eksekusjonsfeedback viser forventning, skritt/repetisjon fremgang, fullføring, kansellering, eller feil uten å ta fokus eller avsløre innsendt tekst. Commands Executed bekrefter kommandoutførelse, ikke en uavhengig ønsket live status i en tredjeparts applikasjon. Feildetaljer vises i aktivitetsloggen.

## Utstyr og Corsair

Corsair G1 – G6 kan tildeles direkte gjennom den offisielle iCUE SDK. Velg enheten og nøkkelen i handlingsredaktoren og lagre den, eller forbered et utkast fra K100-knappsvisningen. Bindinger gjelder for det valgte enheten og den aktive profilen; pause de også. iCUE må kjøre og tillate tilgang. Fn, iCUE hjul, profil og låk kontroller kan ikke kartlegges gjennom denne G-tasten API.

Enhetssiden viser Windows tastaturgrensesnitt og oppdaterer seg periodisk og når enhetene kobles til. En fysisk tastatur kan eksponere flere HID-grensesnitt. Standard globale hotkeys fungerer over produsenter.

Corsair deteksjon bruker tilgjengelige enhetsinformasjon. RGB-kontroll krever kompatibel maskinvare, kjører iCUE og den offisielle iCUE SDK; appen rapporterer faktisk tilgjengelighet. Andre produsenter mottar for øyeblikket oppdagelse og standardhandlinger. Utenfor Corsair G-nøkkelintegrasjon fungerer proprietærkontrollene når Windows mottar dem som vanlig inngang. Appen installerer ikke en tastaturdriver.

## Språkene

Settings tilbyr 27 språk: tysk, engelsk, fransk, spansk, italiensk, portugisisk, nederlandsk, tsjekkisk, slovakisk, ungarsk, bulgarsk, gresk, dansk, svensk, norsk, finsk, estisk, latin, litauisk, slovensk, kroatisk, ukrainsk, russisk, tyrkisk og serbisk i latin. Automatisk følger Windows visningsspråket. Oversettelser er lokalt knyttet. Layouts, hotkeys og brukervenn er uavhengige av grensesnittsspråket.

Komplett lagringsdokumentasjon er tilgjengelig i alle 27 søknadsspråk gjennom [Språk Index](README.md)Det er Oversettelser er maskinassistert, med opprinnelige kilde koblinger og strukturelle kontroller; innfødte talere vurderinger er separate. Appen aksepterer også de regionale kodene `en-US`, `de-DE`, `fr-FR` og `it-IT` og bruker deres nummerformat. Eksisterende språkkoder forblir kompatible.

## Layout og produsentforskning

Tastaturvisningen gir fullstendige og uforutsette referanser til ISO-DE/ANSI-US pluss Corsair K100 RGB layouter. Geometri representerer posisjoner, bred/tall nøkler og ISO Enter. Standard layouts er generiske referanser. K100 layouts følger dokumenterte kontroller uten å kreve millimeter nøyaktige dimensjoner. Velg riktig språk/geometri: et modellnavne eller USB VID/PID fastsetter ikke en spesifikk variant.

Kartlegging løser fysiske skannekoder ved hjelp av den aktive Windows inngangsløsningen. Arbeidsflyten forbereder et utkast og beholder sparings-/konfliktskontroller. Standardbindinger er globale Windows hotkeys, ikke isolert til en koblet tastatur. Corsair G1-G6 bruker enhetspesifikk iCUE integrasjon. Andre eiendomsstyringer krever en adapter eller produsent-konfigurerte skurker. Main Enter og numerisk keypad Enter deler en virtuell nøkkel-identitet for globale hotkeys.

Den [produsentforskning](research/tastaturhersteller-sonderfunktionen.md) og [CSV](research/tastaturhersteller-sonderfunktionen.csv) dekke 74 merke/produsentgrupper, skiller konfiguratorer, inntak SDK, og belysning APIs. [Layout bevis](research/keyboard-layout-evidence.md) Rekordmodell, SKU og moduler forskjeller. Forskningsdekning betyr ikke at maskinvaresupport implementeres.

## Dataene

Konfigurasjon lagres i lokale applikasjonsdata. Import/eksport bruker JSON. Importering forutser konfigurasjonen, erstattet den nåværende konfigurasjonen etter bekreftelse, og deaktiverer importerte bånd til manuelt aktivert. Text-aksjonsinnhold samles ikke som en nøkkellogg. Credential Manager-passord er adskilt fra eksporterte profiler.

## Verifikasjon

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows sjekker kan inspisere maskinvare og midlertidig konfigurere/leie støttet iCUE bånd. Kjør dem uten en annen kontroller ved hjelp av disse båndene. Audio endpoint sjekker endrer ikke live volum eller mute innstillinger.

Native inntekter kan ikke overskride Windows privilegier. En vanlig starter kontroller kan ikke sende innspill til en administrator-applikasjon. Reserverte eller okkuperte hotkeys vises som konflikter.

Den [Dokumentasjon Index](README.md) Linker historiske design, planer, forskning og dato testrapporter. Historiske planer beskriver sin registrerte rekkevidde, ikke en nåværende garanti for funksjonsstøtte.

### Workflow-templer og målvinduer venter

Katalogen inneholder seks redigerbare arbeidsflyter: arbeidsplass, møte, streaming, forskning, prosjektoverføring og tekstskifte. Søk etter `workflow.` i katalogen eller kommandoen. Å velge en arbeidsflyt åpner et uavhengig prosjekt; det går aldri umiddelbart. Alle trinnene blir bevaret ved å legge til en arbeidsflyt til en eksisterende makro eller tildele den til en Corsair nøkkel. Konfigurer eksempler på URL-er, programveier, profiler og OBS scener før du sparer.

Den **Wait for målvinduet** godtar et prosessnavne (med eller uten `.exe`), en tilfelle-insensitiv overskrift substring, og en timeout av 1–600,000 ms. Minst ett filter er nødvendig; begge filtrene må matche når de leveres. Makroen fortsetter bare etter at et matchende synlig vindu svarer og når forgrunnen. Bruk en bestemt tittel når flere vinduer deler en prosess. Avbestillingen eller timetiden stopper etterfølgende trinn. Fönsterberedskap garanterer ikke at et dokument er lastet eller en ekstern tjeneste som OBS WebSocket er koblet.

Skjermbelysning: Søk etter **Increase skjermbelysningen**, **Decrease skjermbrille**, eller **Skjermbelytningen på 50 prosent** i handlingskatalogen. Relative handlinger endrer lyshet med 10 prosentpoeng; faste verdier varierer fra 0 til 100. Støtt interne skjermer bruker WMI; kompatible eksterne skjermer krever DDC/CI aktivert i menyn. Som standard påvirker handlingen alle støttet skjermer; en eksplisitt skjerm kan velges i redaktøren. Forbindelser og dukker kan begrense støtte.

### Utvidet kontroll

Åpne kommandoen med **Ctrl+K når appvinduet er fokusert**. Søk etter **Workflow designer**, **Rekord makro**, **Synkroniserer profiler**, eller **Nøkkeldiagnostikk** (Wording følger ditt valgte språk). Workflow designer redigerer det valgte tile-avtalen: fil/prosess/vinduevilkår, fremover skiper og stopper/ fortsetter feilbehandling..

I et lyssteg velger du alle støttet skjermer eller en spesifikk monitor. En utilgjengelig utvalgte skjerm produserer en feil i stedet for å bytte et annet skjerm. Dashboardet viser levende lys, lyd og OBS lesinger når deres respektive integrasjoner er tilgjengelige; en ikke tilgjengelig lesing presenteres ikke som et målt verdi.

I tileeditoren, gi en støttet fysisk spesiell nøkkel og velg umiddelbar trykk, tap, hold eller dobbelt tap. Held-laget selektorer midlertidig aktiverer et annet kartlegging og gjenoppretter det tidligere laget på frigjøring. Gester og lag gjelder for Corsair SDK G-tasten og enhetsspesifikke spesielle nøkler, ikke vanlige globale hotkeys. Standard-nøkkelfaktura støtter F13–F24 og tastatur media nøkler. Egen Fn, profil og dial-kontroll krever fortsatt en kompatibel leverandøradapter eller en leverandør som kartlegger til en standardnøkkel. Raw Input observerer nøkkelen og undertrykker ikke dens normale Windows atferd. Nøkkeldiagnostikk forklarer profil, pause, tilkobling og binding problemer.

Profilsynkronisering utveksler filer gjennom en mappe du velger, inkludert en mappe som administreres av din eksisterende cloud-klient. Sjekk ut forhåndsvisningen før import; innføringer starter og fjerner maskinvare-spesifikke kartlegginger. Revisjoner forhindrer stille oversettelser, og inkluderer handling innhold er et uttrykkelig valg. MAKRIVO gir ikke en sky konto eller beholder cloud-krediteringer. Tråden bruker nå MAKRIVO egen teal M-ikon og viser en amber pause-bånd når den er pause.

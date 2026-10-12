<!-- generated-doc locale=da-DK source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../README.md)

# MAKRIVO

[Komplet dokumentation på alle 27 sprog](README.md) · [Fuld brugerhåndbog](HANDBOOK.md)

**Ved Aivana.** [Aktuelle indstillinger og frigivelsesbegrænsninger](FEATURES.md) · [Udvidelse af testrapport](testing/2026-10-12-six-extensions.md) · [Den lokale assistent](testing/2026-10-12-local-meeting-assistant.md)

** Engelsk (US) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

En lokal Windows hotkey-kontroller med Stream Deck-stil tilbehør, profiler, makroer og tastaturdetektion. Interfacet understøtter 27 sprog. Ingen konto eller cloudforbindelse er nødvendig for daglig brug.

[Dokumentation](README.md) · [Verifikation](VERIFICATION.md) · [Funktion testrapport](testing/2026-10-09-usp-release.md)

## At begynde

Efter udgivelsen er ansøgningen på `artifacts/win-x64/MAKRIVO.exe`. Hold hele udgangskortet sammen; du kan kopiere det andre steder. Windows 11 x64 er det oprindelige mål. Build artefakter er udelukket fra Git; en kildecheck indeholder ikke en forudbygget eksekuterbar.

Installér .NET 10 SDK angivet i `global.json` for at opbygge og køre fra kilden:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Udgiv en selvindholdet bygning uden at kræve en separat installeret .NET-drift:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Grundlæggende arbejdsproces

1. Vælg en profil og skab en handling.
2. Indtast navn, ikon, farve og tastaturkort. Ctrl/Alt/Shift-kombineringer reducerer tilfældig aktivering under skrivning.
3. Tilføj et eller flere skridt: applikationer, filer, websteder, tekst, kortsætninger, medier, volumen, profilering eller forsinkelser.
4. Save det. Ændringer forbliver en udkast, indtil den er reddet; discard genopretter den reddede version. Tile Play-knappen og dens hotkey bruger den samme udførelsesvej.
5. Reorder til og macro trin med drag-and-drop eller armbunkter. 100 Afbryde gennem grænsen, system tray menu, eller Ctrl+Alt+Escape som standard.

Applikationsspesifikke profiler bruger et procesnavne uden dens fulde vej. Profilsamlingen bryder forbindelser; standardprofilen er fallback. Automatisk profiludvalg skal være aktiveret i Indstillingerne.

Når du lukker hovedvinduet, holder appen i systemet på forhånd. Double-klik på sin tray ikon for at åbne den igen; brug tray-menuen til udgang. Startup med Windows er valgfri og deaktiveret ved standard. Hotkeys kan stoppes centralt.

## Handlinger og kommandoer

Tilføj Action åbner en søgbare katalog med 90 forudsætninger i otte kategorier. Alle har en beskrivelse og en forudsigelse. Macro-editoren bruger samme katalog til at tilføje eller erstatte trin. Vælg en forudset i denne editor arbejdsproces skaber et udkast uden udførelse.

Kommandoen åbnes fra overskriften eller med Ctrl+K inde i appen. Det søger gemte handlinger over profiler, katalogpræsæt, profil skift, navigation, pause/resume, makro annullering og import/eksport. Filtrering af gemte handlinger, katalogfunktioner eller app-kommandoer. Tryk nøgler, aktiverer Enter, redigerer Ctrl+Enter en redigerbar indtastning, og Escape lukker. Favoritter vises først. Forberedte katalogkommandoer kan køre umiddelbart; forhåndsindstillinger, der kræver indstilling, åbner en udgave. Indstillingerne kan definere en global palette. Nye konfigurationer bruger Ctrl+Shift+Space; eksisterende konfigurationer modtager ikke en ny global kortslutning automatisk.

Audio-aktioner fastsætter volumen præcist fra 0 til 100 procent og mute, unmute eller toggle udgang og mikrofonenheder. Vælg et bestemt slutpunkt eller forlad det for Windows standard. En ikke-tilgængelig indsendte punkt producerer en fejl i stedet for at kontrollere en anden enhed. Refresh Devices genoplader de forbundet enheder.

OBS Studio bruger WebSocket v5: start/stop/toggle/pause/resume optagelse; start/stop /toggle streaming; ændre scener; toggle input mute. Indtast WebSocket-serveren under Værktøjer i OBS og indtast dens slutpunkt i redaktøren. Check Connection og Load Names læser OBS version, scener og indtægter uden at starte en optagelse eller strømning. Vælg navn fra listen eller indtast dem manuelt. Passwords kan gemmes i Windows Credential Manager og er udelukket fra de eksporterede konfigurationer. Ikke tilgængelige OBS forbindelser rapporterer en fejl.

Profiler kan matche et procesnavn og en del af et vindue-titel. Begge betingelser skal være i overensstemmelse. Lock Profile forhindrer automatisk skift til sessionen. Foreground's optagelsesbetingelser bruger den sidste fokuserede eksterne applikation.

Tekst og clipboard aktionsstøtte `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, og `{{user}}`. Brug af datoer `yyyy-MM-dd`; Tiderne bruger `HH:mm`. Udenkendte holdere forbliver uændrede. `{{profile}}` Identifikation af den lancerede aktivitet; `{{process}}` Clipboard-indholdet læses kun, når den placeringsholder anvendes.Clipboard-aktiviteten skriver tekst, men peger ikke automatisk.

Windows-aktiviteter minimerer, maksimerer, genopretter eller arrangerer et vindue på venstre / højre side af monitorens arbejdsområde. Systemet aktiverer lukkerne Windows. Disse handlinger foregår kun, når de udtrykkeligt er anmodet om.

Forhåndsvisning viser trin, gentagelser og variabler uden udførelse. Undo og Redo bevarer op til 100 udkast; tekstboksene bevarer indfødte Ctrl+Z-redigering. Markér favoritter i redigerings- eller tile-menuen og filtrer dem på dashboardet. Udførelsesberegninger gælder kun for den nuværende session.

Tekst, kortskærm og vinduindsatser, der er lanceret fra grænsen, aktiverer det sidste eksterne målvindue. Det viser det målret. Hvis målet er lukket eller Windows nægter aktivering, stopper udførelsen med en besked.

## Keyboard support og feedback

Special-Key Wizard er på tastatursiden og i paletten. Det fortegner de G-søgler rapporteret af iCUE. Detect Key Press fanger dem midlertidigt uden at udføre deres tildelte handlinger. Normale bånd vender tilbage efter opdagelse, annullering eller lukning. En katalogfunktion og den valgte nøgle er åben som et ubesvaret udkast; duplikater kontrolleres.

Keyboard Overlay viser den valgte reference layout og aktive bindinger. Hold Ctrl+Alt+F1 eller tag den fra paletten eller tastatursiden. Det tager ikke fokus og tillader musen at gå igennem. Profile og bindende ændringer opdateres, mens de er synlige; almindelige Windows nøglepres er fremhævet. Fabrikantspecifikke kontroller kan ikke levere en tilsvarende Windows nøglesignal.

Udførelsesoplysninger viser ventet, trin/repetition fremskridt, fuldbyrdelse, annullering eller mislykkelse uden at tage fokus eller udvise den indtastede tekst. Kommandoer udført bekræfter kommandoudførelse, ikke en uafhængig ønskede live-tilstand i en tredjeparts ansøgning. Fejldetaljer vises i aktivitetsloggen.

## Udstyr og Corsair

Corsair G1–G6 Kan udleveres direkte gennem den officielle iCUE SDK. Vælg enheden og nøglen i handlingsredigeren og gem eller forbered et projekt fra K100 Bindinger gælder for det valgte enhed og den aktive profil; pause deaktiverer også. iCUE Det er nødvendigt at gøre det muligt. fn iCUE Runde, profiler og låskontroller kan ikke mappes gennem denne G-key API.

Enhedssiden indeholder Windows tastaturgrænseflader og fornyes periodisk og når enhederne er forbundet. En fysisk tastatur kan udsætte flere HID-grænseflader. Standard globale hotkeys arbejder på tværs af producenter.

Corsair opdagelse bruger tilgængelige enhedsoplysninger. RGB-kontrol kræver kompatibel hardware, der kører iCUE og den officielle iCUE SDK; appen rapporterer den faktiske tilgængelighed. Andre producenter modtager i øjeblikket opdagelses- og standardhandlinger. Udenfor Corsair G-nøgle integration fungerer proprietære kontroller, når Windows modtager dem som regelmæssig input. Appen installerer ikke en tastaturdriver.

## Sprog

Settings tilbyder 27 sprog: tysk, engelsk, fransk, spansk, italiensk, portugisisk, nederlandsk, tjekkisk, slovakisk, rumænsk, græsk, dansk, svensk, norsk, finsk, estisk, latin, litauisk, slovensk, kroatisk, ukrainsk, russisk, tyrkisk og serbisk i latinsk script. Automatisk følger Windows displaysprog. Oversættelser er lokalt bundet. Layout, hotkeys og brugeren skabte navne er uafhængige af grænsefladens sprog.

Komplet arkivering er tilgængelig på alle 27 ansøgningssprog gennem [Sprogindeks](README.md)Det er Oversættelser er maskinstøttet, med originale kildeforbindelser og strukturelle kontroller; indfødte taleundersøgelser er adskilt. Appen accepterer også de regionale koder `en-US`, `de-DE`, `fr-FR` og `it-IT` og bruger deres talformat. De eksisterende sprogkoder er kompatible.

## Layout og fabrikant forskning

Tastaturviden giver fuld størrelse og uforudsigelige ISO-DE/ANSI-US referencer plus Corsair K100 RGB layouter. Geometri repræsenterer positioner, bred/tall nøgler og ISO Enter. Standard layouter er generiske referencer. K100 layouter følger dokumenterede kontroller uden at kræve millimeter nøjagtige dimensioner. Vælg den rigtige sprog/geometri variant: et modelnavne eller USB VID/PID fastsætter ikke en bestemt variant.

Mapping løser fysiske skanningskoder ved hjælp af den aktive Windows input layout. Arbejdsfløjen forbereder en udkast og bevarer spar/konfliktschecks. Standardbindinger er globale Windows hotkeys, ikke isoleret til en tilsluttet tastatur. Corsair G1-G6 bruger enhedspesifik iCUE integration. Andre ejendomsstyringer kræver en adapter eller fabrikant-konfigurerede skurke. Main Enter og numerisk keypad Enter deler en virtuel nøgle-identitet for globale hotkeys.

Den [Fabrikantforskning](research/tastaturhersteller-sonderfunktionen.md) og [CSV](research/tastaturhersteller-sonderfunktionen.csv) dækker 74 brand/producentgrupper, der adskiller konfiguratorer, indtastede SDK'er og belysning APIs. [Layout bevis](research/keyboard-layout-evidence.md) Modeller, SKU og moduler. Forskningsdækning betyder ikke, at hardware-støtte implementeres.

## data

Konfiguration lagres i lokale applikationsdata. Import/eksport bruger JSON. Importen forudser konfigurationen, erstattes den aktuelle konfiguration efter bekræftelse og deaktiverer de importerede bånd, indtil de er manuelt aktiveret. Tekstindhold indsamles ikke som en nøglebog. Credential Manager er adskilt fra de eksporterede profiler.

## Verifikation

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows kontroller kan inspicere hardware og midlertidigt konfigurere/læs støttet iCUE bindinger. Gør dem uden en anden kontroller ved hjælp af disse bånd. Audio endpoint kontroller ændrer ikke live-volumen eller muteindstillinger.

Native indtægter kan ikke overstige Windows privilegier. En normalt lanceret administrator kan ikke sende input til en administratorapplikation. Reserverede eller besatte hotkeys vises som konflikter.

Den [Dokumentation indeks](README.md) link historiske design, planer, forskning og dato testrapporter. Historiske planer beskriver deres registrerede omfang, ikke en nuværende garanti for funktionstjeneste.

### Arbejdsfløj og målvindue venter

Katalogen indeholder seks redigerbare arbejdsprocesser: workspace, møde, streaming, forskning, projektskift og tekst snippet. Søg efter `workflow.` i katalogen eller kommandopaletten. Valget af en arbejdsproces åbner et ubesvaret projekt; det løber aldrig umiddelbart. Alle trin bevares, når du tilføjer en arbejdsproces til en eksisterende makro eller tildeler den en Corsair nøgle. Konfigurer eksempler på URL'er, programmer, profiler og scener OBS før du sparer.

Den **Vent på målvinduet** En procesnavnet accepteres (med eller uden `.exe`), en tilfældig undervisning, og en tidsaftale 1–600,000 ms. Der kræves mindst en filter; begge filtre skal matche, når de leveres. makro fortsætter kun efter at et matchende synligt vindue svarer og når forkantet. Brug en bestemt titel, når flere vinduer deler en proces. Cancellation eller timeout stopper de følgende skridt. OBS WebSocket er forbundet.

Skærm lys: søg efter **Increase skærm lys**, **Dekærm lys ned**, eller **Skærm lys på 50 procent** i handlingskatalogen. Relative handlinger ændrer lys med 10 procentpoint; faste værdier varierer fra 0 til 100. Understøttede interne skærme bruger WMI; kompatible eksterne skærme kræver DDC/CI aktiveret i deres menu. Som standard påvirker handlingen alle de understøttede skærme; en eksplicit monitor kan vælges i redaktøren. Forbindelser og dukker kan begrænse supporten.

### Udvidet kontrol

Åbn kommandoen med **Ctrl+K, mens appvinduerne har fokus**. Søg efter **Arbejdsfløj designer**, **Rekord af macro**, **Synkronisering af profiler**, eller **Nøglediagnostik** (Arbejdsflydelsesdesigneren redigerer det udvalgte tileformular: fil/process/vinduebetingelser, fremskridt tæller og stop/ fortsætter fejlbehandlingen..

I et skærm skal du vælge alle støttede skærme eller en bestemt monitor. En ikke tilgængelig valgt monitor producerer en fejl i stedet for stille at ændre en anden display. Dashboardet viser levende lys, lyd og OBS læsninger, når deres respektive integrationer er tilgængelige; en ikke tilgængelig læsning præsenteres ikke som et målt værdi.

I tileeditoren skal du tildele en understøttet fysisk speciel nøgle og vælge øjeblikkelig tryk, tryk, hold eller dobbelt tryk. Held-skalaer aktiverer midlertidigt en anden mapping og genopretter det foregående lag på frigivelse. Gester og lag gælder for Corsair SDK G-nøgler og enhedspecifik standard specielle nøgler, ikke almindelige globale hotkeys. Standard nøgleoptagelse understøtter F13–F24 og keyboard media nøgler. Egen Fn, profil og dial kontroller kræver stadig en kompatibel leverandøradapter eller en leverandør mapper til en standard nøgle. Raw Input observerer nøglen og undertrykker ikke dens normale Windows adfærd. Nøglediagnostik forklarer profil, pause, forbindelse og bindende problemer.

Profilsynkronisering udveksler filer gennem en mappe, du vælger, herunder en mappe administreret af din eksisterende cloud-klient. Se forudsigelsen før importen; importen begynder at deaktivere og fjerne hardware-specifikke mapper. Revisions forhindrer stille overskrivninger, og inkludering af handling indhold er et udtrykkeligt valg. MAKRIVO leverer ikke en cloud-konto eller bevarer cloud-krediteringer. Træet bruger nu MAKRIVO egen teal M-ikone og viser en amber pause markering, når det er stoppet.

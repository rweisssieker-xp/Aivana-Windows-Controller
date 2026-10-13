<!-- generated-doc locale=da-DK source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../README.md)

# MAKRIVO

[Komplet dokumentation på alle 27 sprog](README.md) · [Fuld brugerhåndbog](HANDBOOK.md)

**Ved Aivana.** [Aktuelle indstillinger og frigivelsesbegrænsninger](FEATURES.md) · [Udvidelse af testrapport](testing/2026-10-12-six-extensions.md) · [Den lokale assistent](testing/2026-10-12-local-meeting-assistant.md)

** Engelsk (US) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

En lokal Windows-genvejscontroller med fliser i Stream Deck-stil, profiler, makroer og tastaturregistrering. Brugerfladen understøtter 27 sprog. Daglig brug kræver ingen konto eller cloudforbindelse.

[Dokumentation](README.md) · [Verifikation](VERIFICATION.md) · [Funktion testrapport](testing/2026-10-09-usp-release.md)

## At begynde

Efter publicering findes programmet på `artifacts/win-x64/MAKRIVO.exe`. Hold hele outputmappen samlet; den kan kopieres andre steder. Windows 11 x64 er det første mål. Buildartefakter er udeladt fra Git; en kildekode-checkout indeholder ikke en færdigbygget eksekverbar fil.

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

1. Vælg en profil, og opret en handlingsflise.
2. Angiv navn, ikon, farve og tastaturgenvej. Ctrl/Alt/Shift-kombinationer reducerer utilsigtet aktivering under skrivning.
3. Tilføj et eller flere trin: programmer, filer, websteder, tekst, genveje, medier, lydstyrke, profilskift eller forsinkelser.
4. Gem. Ændringer forbliver et udkast, indtil de gemmes; kassér gendanner den gemte version. Flisens afspilningsknap og dens genvej bruger samme udførelsesvej.
5. Omarrangér fliser og makrotrin med træk og slip eller pileknapper. Skabeloner forenkler opsætningen. Makroer kan gentages op til 100 gange. Annullér via brugerfladen, meddelelsesområdets menu eller som standard Ctrl+Alt+Escape.

Programspecifikke profiler bruger et procesnavn uden fuld sti. Profilrækkefølgen afgør lige matches; standardprofilen er reservevalget. Automatisk profilvalg skal aktiveres i Indstillinger.

Lukning af hovedvinduet lader som standard appen køre i meddelelsesområdet. Dobbeltklik på ikonet for at genåbne; brug ikonmenuen til at afslutte. Start med Windows er valgfrit og deaktiveret som standard. Genveje kan pauses centralt.

## Handlinger og kommandoer

Tilføj handling åbner et søgbart katalog med 90 forudindstillinger i otte kategorier. Hver har en beskrivelse og forhåndsvisning. Makroeditoren bruger samme katalog til at tilføje eller erstatte trin. Valg af en forudindstilling i denne editorarbejdsgang opretter et udkast uden udførelse.

Kommandopaletten åbnes fra overskriftsområdet eller med Ctrl+K i appen. Den søger gemte handlinger på tværs af profiler, katalogforudindstillinger, profilskift, navigation, pause/genoptag, makroannullering og import/eksport. Filtrér efter gemte handlinger, katalogfunktioner eller appkommandoer. Piletaster vælger, Enter aktiverer, Ctrl+Enter redigerer et redigerbart element, og Escape lukker. Favoritter vises først. Brugsklare katalogkommandoer kan køre straks; forudindstillinger, der kræver opsætning, åbner et udkast. Indstillinger kan definere en global paletgenvej. Nye konfigurationer bruger Ctrl+Shift+Space; eksisterende konfigurationer får ikke automatisk en ny global genvej.

Lydhandlinger indstiller lydstyrken præcist fra 0 til 100 procent og slår lyden fra, til eller skifter lydløs tilstand på udgangs- og mikrofonenheder. Vælg et bestemt endepunkt, eller lad det være uvalgt for Windows-standarden. Et utilgængeligt gemt endepunkt giver en fejl frem for at styre en anden enhed. Opdatér enheder genindlæser de tilsluttede enheder.

OBS Studio bruger WebSocket v5: start/stop/skift/pause/genoptag optagelse; start/stop/skift streaming; skift scener; skift inputmuting. Aktivér WebSocket-serveren under Værktøjer i OBS, og angiv endepunktet i editoren. Kontrollér forbindelse og Indlæs navne læser OBS-version, scener og input uden at starte optagelse eller streaming. Vælg navne fra listen, eller indtast dem manuelt. Adgangskoder kan gemmes i Windows Credential Manager og udelades fra eksporterede konfigurationer. Utilgængelige OBS-forbindelser rapporterer en fejl.

Profiler kan matche et procesnavn og en del af en vinduestitel. Begge angivne betingelser skal matche. Lås profil forhindrer automatisk skift i sessionen. Indfang betingelser fra forgrunden bruger det seneste fokuserede eksterne program.

Tekst- og udklipsholderhandlinger understøtter `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` og `{{user}}`. Datoer bruger `yyyy-MM-dd`; tider bruger `HH:mm`. Ukendte pladsholdere forbliver uændrede. `{{profile}}` identificerer den startede handlings profil; `{{process}}` identificerer den eksterne programkontekst. Udklipsholderindhold læses kun, når denne pladsholder bruges. Udklipsholderhandlingen skriver tekst, men indsætter ikke automatisk.

Vindueshandlinger minimerer, maksimerer, gendanner eller placerer et vindue i venstre/højre side af skærmens arbejdsområde. Systemhandlingen låser Windows. Disse handlinger kører kun ved udtrykkeligt kald.

Forhåndsvisning viser trin, gentagelser og variabler uden udførelse. Fortryd og Annullér fortryd bevarer op til 100 udkasttilstande; tekstfelter beholder deres indbyggede Ctrl+Z-redigering. Markér favoritter i editoren eller flisemenuen, og filtrér dem på dashboardet. Udførelsestællinger gælder kun for den aktuelle session.

Tekst-, genvejs- og vindueshandlinger startet fra brugerfladen aktiverer det seneste eksterne målvindue. Paletten viser denne målkontekst. Hvis målet er lukket, eller Windows nægter aktivering, stopper udførelsen med en meddelelse.

## Keyboard support og feedback

Guiden til specialtaster findes på tastatursiden og i paletten. Den viser G-taster rapporteret af iCUE. Registrér tastetryk indfanger dem midlertidigt uden at udføre deres tildelte handlinger. Normale bindinger vender tilbage efter registrering, annullering eller lukning. En katalogfunktion og valgt tast åbner som et ikke-gemt udkast; dublerede bindinger kontrolleres.

Tastaturoverlayet viser valgt referencelayout og aktive bindinger. Hold Ctrl+Alt+F1 nede, eller slå det til/fra via paletten eller tastatursiden. Det tager ikke fokus og lader museklik passere igennem. Profil- og bindingsændringer opdateres, mens det er synligt; almindelige Windows-tastetryk fremhæves. Producentspecifikke kontroller giver muligvis ikke et tilsvarende Windows-tastesignal.

Tilbagemelding om udførelse viser ventetid, trin-/gentagelsesfremgang, fuldførelse, annullering eller fejl uden at tage fokus eller afsløre indtastet tekst. Kommandoer udført bekræfter kommandoudførelse, ikke en uafhængigt forespurgt livetilstand i et tredjepartsprogram. Fejldetaljer vises i aktivitetsloggen.

## Udstyr og Corsair

Corsair G1–G6 kan tildeles direkte via den officielle iCUE SDK. Vælg enhed og tast i handlingseditoren, og gem, eller forbered et udkast fra K100-tastaturvisningen. Bindinger gælder for valgt enhed og aktiv profil; pause deaktiverer dem også. iCUE skal køre og tillade adgang. Fn, iCUE-hjulet, profil- og låsekontroller kan ikke tilknyttes via denne G-key API.

Enhedssiden indeholder Windows tastaturgrænseflader og fornyes periodisk og når enhederne er forbundet. En fysisk tastatur kan udsætte flere HID-grænseflader. Standard globale hotkeys arbejder på tværs af producenter.

Corsair-registrering bruger tilgængelige enhedsoplysninger. RGB-styring kræver kompatibel hardware, kørende iCUE og den officielle iCUE SDK; appen rapporterer faktisk tilgængelighed. Andre producenter får aktuelt registrering og standardhandlinger. Uden for Corsair G-key-integration virker proprietære kontroller, når Windows modtager dem som almindelig input. Appen installerer ingen tastaturdriver.

## Sprog

Indstillinger tilbyder 27 sprog: tysk, engelsk, fransk, spansk, italiensk, portugisisk, nederlandsk, polsk, tjekkisk, slovakisk, ungarsk, rumænsk, bulgarsk, græsk, dansk, svensk, norsk, finsk, estisk, lettisk, litauisk, slovensk, kroatisk, ukrainsk, russisk, tyrkisk og serbisk med latinsk skrift. Automatisk følger Windows-visningssproget. Oversættelser medfølger lokalt. Layouts, genveje og brugeroprettede navne er uafhængige af brugerfladesproget.

Komplet arkivering er tilgængelig på alle 27 ansøgningssprog gennem [Sprogindeks](README.md). Oversættelser er maskinassisterede med originalkildelinks og strukturkontroller; modersmålsgennemgang er separat. Appen accepterer også de regionale koder `en-US`, `de-DE`, `fr-FR` og `it-IT` og bruger deres talformater. Eksisterende sprogkoder forbliver kompatible.

## Layout og fabrikant forskning

Tastaturvisningen tilbyder ISO-DE/ANSI-US-referencer i fuld størrelse og uden numerisk tastatur samt Corsair K100 RGB-layouts. Geometrien viser positioner, brede/høje taster og ISO Enter. Standardlayouts er generiske referencer. K100-layouts følger dokumenterede kontroller uden at hævde millimeterpræcise mål. Vælg korrekt sprog-/geometrivariant: et modelnavn eller USB VID/PID fastslår ikke en bestemt variant.

Tilknytning bestemmer fysiske scanningskoder via det aktive Windows-inputlayout. Arbejdsgangen forbereder et udkast og bevarer gemme-/konfliktkontroller. Standardbindinger er globale Windows-genveje, ikke isoleret til ét tilsluttet tastatur. Corsair G1–G6 bruger enhedsspecifik iCUE-integration. Andre proprietære kontroller kræver en adapter eller producentkonfigurerede genveje. Hoved-Enter og numerisk Enter deler virtuel tastidentitet for globale genveje.

Den [Fabrikantforskning](research/tastaturhersteller-sonderfunktionen.md) og [CSV](research/tastaturhersteller-sonderfunktionen.csv) dækker 74 mærke-/producentgrupper og skelner mellem konfiguratorer, input-SDK’er og belysnings-API’er. [Layout bevis](research/keyboard-layout-evidence.md) registrerer forskelle i model, SKU og moduler. Forskningsdækning betyder ikke, at hardwareunderstøttelse er implementeret.

## data

Konfiguration gemmes i lokale programdata. Import/eksport bruger JSON. Import forhåndsviser konfigurationen, erstatter den aktuelle konfiguration efter bekræftelse og deaktiverer importerede bindinger indtil manuel aktivering. Teksthandlingsindhold indsamles ikke som en tastetrykslog. Credential Manager-adgangskoder er adskilt fra eksporterede profiler.

## Verifikation

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows-kontroller kan undersøge hardware og midlertidigt konfigurere/frigive understøttede iCUE-bindinger. Kør dem uden en anden controller, der bruger disse bindinger. Kontrol af lydendepunkter ændrer ikke aktuel lydstyrke eller muting.

Native input kan ikke omgå Windows-rettigheder. En normalt startet controller kan muligvis ikke sende input til et administratorprogram. Reserverede eller optagne genveje vises som konflikter.

Den [Dokumentation indeks](README.md) link historiske design, planer, forskning og dato testrapporter. Historiske planer beskriver deres registrerede omfang, ikke en nuværende garanti for funktionstjeneste.

### Arbejdsfløj og målvindue venter

Kataloget indeholder seks redigerbare arbejdsgange: arbejdsplads, møde, streaming, forskning, projektskift og tekstfragment. Søg efter `workflow.` i kataloget eller kommandopaletten. Valg af en arbejdsgang åbner et ikke-gemt udkast; den kører aldrig straks. Alle trin bevares, når en arbejdsgang tilføjes til en eksisterende makro eller tildeles en Corsair-tast. Konfigurér eksempel-URL’er, programstier, profiler og OBS-scener før lagring.

Trinnet **Vent på målvindue** accepterer et procesnavn (med eller uden `.exe`), en del af en titel uden skelnen mellem store og små bogstaver samt en timeout på 1–600000 ms. Mindst ét filter kræves; begge skal matche, hvis begge angives. Makroen fortsætter først, når et matchende synligt vindue svarer og kommer i forgrunden. Brug en specifik titel, når flere vinduer deler en proces. Annullering eller timeout stopper efterfølgende trin. Et klart vindue garanterer ikke, at et dokument er indlæst, eller en ekstern tjeneste såsom OBS WebSocket er forbundet.

Skærmlysstyrke: søg efter **Øg skærmlysstyrke**, **Sænk skærmlysstyrke** eller **Skærmlysstyrke på 50 procent** i handlingskataloget. Relative handlinger ændrer lysstyrken med 10 procentpoint; faste værdier går fra 0 til 100. Understøttede interne skærme bruger WMI; kompatible eksterne skærme kræver DDC/CI aktiveret i deres menu. Som standard påvirker handlingen alle understøttede skærme; en bestemt skærm kan vælges i editoren. Forbindelser og docks kan begrænse understøttelsen.

### Udvidet kontrol

Åbn kommandopaletten med **Ctrl+K, mens appvinduet har fokus**. Søg efter **Workflowdesigner**, **Optag makro**, **Synkroniser profiler** eller **Tastdiagnostik** (ordlyden følger dit valgte sprog). Workflowdesigneren redigerer det valgte fliseudkast: fil-/proces-/vinduesbetingelser, antal trin, der springes frem, og stop/fortsæt ved fejl. Optageren registrerer genvejskombinationer og ventetider, ikke almindelig tekstindtastning; Escape stopper optagelsen. Gennemgå resultatet før overtagelse.

I et skærm skal du vælge alle støttede skærme eller en bestemt monitor. En ikke tilgængelig valgt monitor producerer en fejl i stedet for stille at ændre en anden display. Dashboardet viser levende lys, lyd og OBS læsninger, når deres respektive integrationer er tilgængelige; en ikke tilgængelig læsning præsenteres ikke som et målt værdi.

Tildel en understøttet fysisk specialtast i fliseeditoren, og vælg øjeblikkeligt tryk, kort tryk, langt tryk eller dobbelttryk. Lagvælgere aktiverer midlertidigt en anden tilknytning, mens de holdes nede, og gendanner det tidligere lag, når de slippes. Bevægelser og lag gælder for Corsair SDK G-taster og enhedsspecifikke standardspecialtaster, ikke almindelige globale genveje. Standardtastregistrering understøtter F13–F24 og medietaster. Proprietære Fn-, profil- og drejeknapkontroller kræver stadig en kompatibel leverandøradapter eller leverandørtilknytning til en standardtast. Raw Input observerer tasten og undertrykker ikke dens normale Windows-adfærd. Tastdiagnostik forklarer profil-, pause-, forbindelses- og bindingsproblemer.

Profilsynkronisering udveksler filer via en mappe, du vælger, herunder en mappe administreret af din eksisterende cloudklient. Gennemgå forhåndsvisningen før import; importer starter deaktiveret og fjerner hardwarespecifikke tilknytninger. Revisioner forhindrer ubemærkede overskrivninger, og medtagelse af handlingsindhold er et udtrykkeligt valg. MAKRIVO tilbyder ingen cloudkonto og gemmer ingen cloudlegitimationsoplysninger. Meddelelsesområdet bruger nu MAKRIVOs eget blågrønne M-ikon og viser et ravfarvet pausemærke, når bindinger er pauset.

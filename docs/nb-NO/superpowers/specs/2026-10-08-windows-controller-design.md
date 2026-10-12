<!-- generated-doc locale=nb-NO source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Prosjektet

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stand: 8. Oktober 2026. Status: for undersøkelse før implementering.

## Mål og avtalt omfang

En uavhengig Windows app gjør en vanlig PC-tastatur til et fritt konfigurert handlingspanel som ligner på en strømdekk. Brukeren har bekreftet alle foreslåtte handlingsformer, makroer, enhet gjenkjenning, profiler og en god GUI. Tilslutte Corsair- og andre tastaturer blir gjenkjent; ytterligere produsentfunksjoner tilbys bare når de faktisk støttes.

Antagelser: tysk overflate, lokal bruk uten konto, Windows 11 x64 som det første målet, ingen driverinstallasjon. Tastaturet er fortsatt brukbart for normal skriving. Globale belegger bruker standardknappkombinasjoner med modifikatorer. Enkelte bokstaver er ikke reservert globalt.

## Teknisk avgjørelse

Native C#/NET-10-App med WPF og MVVM. Den stabile .NET-10-SDK er tilgjengelig i arbeidsområdet. WPF tillater en individuell utformet skrivebordsgrensesnitt og direkte integrering Windows. Alternativer vil være en nettverksgrensesnitt med separat Windows-tjeneste eller en plattformoverskridende skrivebordsprogram; begge bringer ekstra driftstid og kommunikasjonskompleksitet for dette Windows-prosjektet. Native variasjon er derfor det valgte grunnlaget.

Distribusjon: Desktop-prosjekt for visjoner og tråder, Core-prosjekt til konfigurasjon og utførelseslogikk, Windows-prosjekt med hotkeys, innsimulering og enheter, testprosjekt for plattformen uavhengige regler. Produsentadapter henger på et fast kapasitetsgrensesnitt og er uavhengige av standardaksjonene.

## Overflaten

Dunkle design med rolig anthrazit farge områder, godt lesbar skrift og turkisk accent. Link navigasjon for dashboard, profiler, enheter og innstillinger. Opp aktiv profil, søk og switch for å pause alle hotkeys. 100–200 prosent Windows-Skalaen kan brukes.

Dashboardet inneholder en justerbar kjevelrasser. Hvert kjell viser symbol, navn, hotkey og utførelsesstatus. Klikk utfører samme handling som Hotkey. Høyre åpner en redaktør med handlingstype, parametere, hotkey-opptak, symbol/farge, aktivering og testknapp. Ny, duplisere, slette og omorganisere er tilgjengelig. En tastaturvisning viser skjematisk belegg for tysk standardlagring; den hevder ikke nøyaktig geometri av det gjenkjente enheten.

Profiler kan opprettes, kopieres, endres og aktiveres manuelt. Alternativt blir en prosessnavne tildelt en profil. Et egnet forgrunnsprogram aktiverer profilen din, ellers gjelder den globale standardprofilen. For flere egnede profiler gjelder en tydelig rekkefølge. Redaktøren forklarer hotkey-konflikter direkte på innsjøen.

## Hotkeys og inntak

Windows RegisterHotKey behandler normale globale kombinasjoner med undertrykket tastatur gjenoppretting. Bare den aktive profilen registreres. Reserverte eller allerede registrerte kombinasjoner vises som feil; appen rapporterer dem ikke som vellykket aktivert. Innsamlingen i redaktøren løser ikke handlinger.

Før simulerte inntak frigjøres de utløsende modifikasjonsknappene eller deres fysiske frigjøring forventes. Unicode-tekst og tastkombinasjoner sendes med SendInput. Hver enkelt trykket knapp blir løslatt selv når det bryter ned. Handlinger går utenfor UI-trådet; utførelse og profil endring må ikke blokkere overflaten.

## Handlinger og makro

Aktionstyper: Start programmet med argumenter og arbeidsbok, åpne fil/foldere over Windows, åpne HTTP/HTTPS-nettsted, skrive inn unicode-tekst, sende en knappkombinasjon, spille/post/forgjengere/stopp media, lyd høyere/dårlig/stummere og endre profil. Parameter valideres før lagring og igjen før utførelse. Programmer starter direkte; Argumenter tolkes ikke implisitt som Shell-kommandoer.

Makroene inneholder en ordnet liste over de nevne handlinger og uttrykkelige skritt. Trinnene kan legges til, redigeres og forsvinner. Opprinnelser er endelig konfigurerbare (1–100); Endless slitasjer tilhører ikke den første versjonen. En løpende makro kan avsluttes over overflaten eller en egen avbruddhotkey. Gjentatt utløser den samme løpende bevegelsen starter ikke en parallell løp. Forskjellige makroer med innsimulering serieres slik at tastetrykkene ikke forveksles. I tilfelle av en trinnfeil stopper makroen og viser det berørte trinnet.

## Enheter og produsenter

Enhetskonto over Windows Raw Input og tilleggsinformasjon: Visningsnavn, enhetsveien, produsent/produktidentifikasjon i den grad det er tilgjengelig, tilkoblingsstatus og gjenkjente ferdigheter. Oppdaterer/oppdaterer listen uten å starte på nytt. En fysisk enhet kan ha flere HID-inntekter; disse er gruppert etter belastbare identifiseringer, ellers synlig som separate inntekter.

Vanlige hotkeys gjelder tastaturoverskridende. Enhetsvisningen gir informasjon og ferdigheter; en separat tastatur med eksklusive fanger er ikke en del av denne avtale modus.

Corsair gjenkjenning fungerer også uten iCUE. En valgfri iCUE SDK-adapter gir tilkoblingsstatus og støttet belysning når SDK, iCUE og kompatible enheter er tilgjengelig. UI skiller seg oppdaget, SDK tilgjengelig, koblet og faktisk kontrollert. SDK dokumenterer hovedsakelig LED-styring; proprietære G-tasten støttes bare som vanlige hotkeys når de mottar Windows tilsvarende inntak. Det er ingen fast forpliktelse for alle sonde.

Andre produsenter får først generisk identifisering og standardaksjoner. Adaptergrensesnittet tillater senere integrasjoner; unsupported ferdigheter vises ærlig. Hardware-tester for ikke eksisterende modeller er ikke angitt som bestående.

## Lagring og dagligdag

Versjonert JSON-konfigurasjon under den lokale App-datalogen. Atomisk lagring med den siste gyldige sikkerhet; feil filer blir ikke overskrivet. Import kontrollerer format, versjon, ID og parametere og viser en forhåndsvisning før overtakelse. Importerte handlinger kan kun utføres etter uttrykkelig aktivering. Eksport inneholder profiler og belegger.

Tray-menyen: Åpne, profilvalg, pause hotkeys, bryte makro, avslutte. Vinduer lukkes minimalt i tråden og forklarer dette på første gang. Starten er valgfri og standard. Bare en institusjon administrerer Hotkeys. Avslutter registrering av hotkeys, bryter ut utførelser og frigjør ressurser.

Lokalt aktivitetsutsikt med begrenset kurs for suksess, feil og avbrudd. Ingen protokoll av alle tastaturinnlegg. Tekstinnholdet blir ikke utskrevet i feilmeldinger. Ingen sky og ingen telemetri.

## Feilbehandling og grenser

Fraværende programmer, ugyldige URL-er, skadet hotkeys, skadede profiler og ikke tilgjengelig produsent SDK får konkrete tyske meldinger. En adapterfeil påvirker ikke standardaksjoner. Inngang i høyere privilegerte apper kan mislykkes på Windows tillatelsesgrenser; feil er forklart, appen krever ikke standard administrasjonsrettigheter.

## Undersøkelse og reduksjon

Automatiserte tester for profilprioritet, konfliktdeteksjon, konfigurasjonsimport, atomlagring, makrorefølge, gjentakelse, avbrudd og feilpropagasjon. Windows Smoke-tester for ekte hotkeys, tekst og tastkombinasjoner, medieaksjoner, enhetsopptak, trail og ren slutt. Ingen tester som tilfeldig lanserer noen programmer eller nettsteder.

GUI-testing: Lagre og redigere, opptak uten utførelse, klikk/hotkey med samme effekt, makroavbrudd, konfliktmeldinger, profilutveksling, enhetssyn, import/eksport og skala. Produsentspesifikk maskinvarekontroll er dokumentert separat med faktisk eksisterende enhet.

Levering: Kildekode, reprodusert bygg-/startlinje, tysk bruksanvisning og lokal Windows-x64-Publish-Artefakt. Suksess betyr en utførbar app med alle generiske funksjoner; Corsair-funksjoner er kun merket som tilgjengelig med bevisst SDK/enhetstøtte.

## Tekniske kilder

- Microsoft Keyboard Input / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Innføring: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Offisielle Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Neste skritt

Etter å ha gjennomgått dette prosjektet: utarbeide en konkret gjennomføringsplan og foreslå utvalget av gjennomføringen. Det er ennå ingen Git-repository i den tomme arbeidsordningen; dette dokumentet er derfor ikke committed.

<!-- generated-doc locale=da-DK source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Udkastet

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stand: 8. Oktober 2026. Status: til undersøgelse før implementering.

## Mål og aftalt omfang

En uafhængig Windows app gør en normal PC-tastatur til en frit konfigurerbar handlingspanel lig med en strømdæk. Brugeren har bekræftet alle de foreslåede handlinger, makroer, enhed genkendelse, profiler og en god GUI. Tilslutte Corsair- og andre tastaturer opdages; yderligere producenterfunktioner tilbydes kun, hvis de faktisk er understøttet.

Antagelser: tysk overflade, lokal brug uden konto, Windows 11 x64 som det første mål, ingen driverinstallation. Tastaturet er til normal skrivning. Globale belægninger bruger standardt tastkombinationer med modifikatorer. Enkelte bogstaver er ikke standardiseret globalt.

## Teknisk beslutning

Native C#/.NET-10-App Med WPF MVV: Det er stabilt .NET-10-SDK Er tilgængelig på arbejdspladsen. WPF tillader en individuelt designet desktop-interface og direkte Windows-Integration. alternativer ville være en webinterface med separat Windows-Tjeneste eller en tværplatform desktop-app; begge bringer yderligere drifts- og kommunikationskompleksitet til denne Windows-Den indfødte variant er derfor den valgte basis.

Uddeling: Desktop-projekt for visninger og trajektorier, Core-projekt for konfiguration og implementeringslogik, Windows-projekt for hotkeys, input simulering og enheder, testprojekt for platformspecifikke regler. Fabrikantadaptere hænger på et fast kapacitetsgrænseflade og er uafhængige af standardaktioner.

## Overflade

Dunkel design med rolige anthrazitfarvede områder, godt læsesum og turkisfarvet accent. Links Navigation til Dashboard, Profiler, Enheder og Indstillinger. Oven aktiv profil, søgning og switch for at stoppe alle hotkeys. Overfladen forbliver anvendelig ved 100–200 procent Windows-skaliering.

Dashboardet indeholder en tilpasningsbar kasse. Hvert kaske viser symbol, navn, hotkey og udførelsesstatus. Klik udfører den samme handling som Hotkey. På højre side åbnes en editor med handlingstype, parametre, hotkey optagelse, symbol/farve, aktivering og testknap. Ny, duplisere, slette og omordne er tilgængelige. Et tastaturindblik viser skema for tysk standardlayout; det hævder ikke den nøjagtige geometri af det identificerede enhed.

Profiler kan oprettes, kopieres, ændres og aktiveres manuelt. Optionelt tildeles en procesnavne til en profil. Et passende forgrundsprogram aktiverer sin profil, ellers gælder den globale standardprofil. For flere relevante profiler gælder en tydeligt synlig rækkefølge. Redaktøren forklarer hotkey-konflikter direkte på placeringen.

## Hotkeys og indtægter

Windows RegisterHotKey behandler normale globale kombinationer med undertrykt tastaturoptagelse. Kun den aktive profil registreres. Reserverede eller allerede registrerede kombinationer vises som fejl; appen rapporterer dem ikke som vellykket. Optagelsen i redaktøren løser ikke handlinger.

Forud for simulerede indtægter frigives de udløsende ændringsskærme eller deres fysiske frigivelse forventes. Unicode-tekst og knapkombinationer sendes med SendInput. Hver selvtrykt knap bliver løsladt, selv når den er nedbrudt. Aktioner kører uden for UI-trådet; udførelse og profilændring må ikke blokere overfladen.

## Aktioner og makro

Aktionstyper: Start programmet med argumenter og arbejdslog, åbne filer/foldere over Windows, åbne HTTP/HTTPS-webside, indtaste Unicode-tekst, send tastkombination, Media Play/Pause/Næste/Forriger/Stop, højere/Low/Stum samt skifte profil. Parametre valideres før opbevaring og igen før udførelse. Programmer starter direkte; Argumenter tolkes ikke implikativt som Shell-bestemmelser.

Makroer indeholder en ordnet liste over de nævnte handlinger og udtrykkelige skridt. trin kan tilføjes, redigeres og forlænges. Repeaterne er endelig konfigurerbare (1–100); Endløshold er ikke en del af den første version. En løbende makro kan afsluttes over overfladen eller en egen afbrydelseshotkey. Gennemsigtig udløsning af den samme løbende besættelse starter ikke en parallel løb. Forskellige makroer med input-simulation serialiseres, så deres tastatur ikke forveksles. I tilfælde af et skridtfejl stopper makroen og angiver det berørte skridt.

## Udstyr og producenter

Udstyrsoplysning over Windows Raw Input og supplerende udstyrsinformation: Navn, udstyrsvej, producent/produktidentifikation, hvor det er tilgængeligt, forbindelsestatus og kendte evner. Til/til opdaterer listen uden genstart. En fysisk enhed kan have flere HID-indtægter; disse grupperes efter belastbare identifikationer, ellers synlige som separate indtægter.

Normale hotkeys gælder tværfagligt. Enhedsvisningen giver oplysninger og færdigheder; en separat tastatur med eksklusivt fangerede knapper er ikke en del af denne aftale.

Corsair-opdagelse fungerer også uden iCUE. En valgfri iCUE SDK-adapter giver tilslutningstilstand og understøttet belysning, hvis der er SDK, iCUE og kompatible enheder. UI adskiller kendt, SDK tilgængelig, forbundet og faktisk kontrollerbar. SDK dokumenterer primært LED-styring; proprietære G-knapper understøttes kun som normale hotkeys, når Windows modtager de relevante indtægter. Der er ingen faste forpligtelser til alle sonde.

Andre producenter får først generisk genkendelse og standardaktioner. Adaptergrænsen tillader senere integrationer; unsupported færdigheder vises ærligt. Hardware-tester for ikke-tilgængelige modeller angives ikke som overlevende.

## Opbevaring og hverdag

Versioneret JSON-konfiguration under den lokale app-datalog. Atomisk opbevaring med den sidste gyldige sikkerhed; fejlfilerne bliver ikke oversteg. Import kontrollerer format, version, ID'er og parametre og viser en forudsigelse før overtagelse. Importerede handlinger kan kun gennemføres efter udtrykkelig aktivering. Eksport indeholder profiler og belægninger.

Tray-menuen: Åbn, profilvalg, stoppe hotkeys, afbryde makroer, afslutte. Vinduer lukker minimeret i træk og forklarer dette første gang. Bilen er optional og standard. Kun én instans forvalter Hotkeys. Slutter registrerer hotkeys, bryder udførelser og frigiver ressourcer.

Lokalt aktivitetsvisninger med begrænset løbetid for succes, fejl og afbrydelse. Ingen protokol over alle tastaturindtægter. Tekstindholdet udskrives ikke i fejlmeldinger. Ingen cloud-tjeneste og ingen telemetri.

## Fejlbehandling og grænser

Manglende programmer, ugyldige URL'er, nedlagt hotkeys, beskadigede profiler og ikke-tilgængelige producenter-SDK modtager konkrete tyske meddelelser. En adapterfejl påvirker ikke standardaktiviteter. Indtægter i højere privilegerede applikationer kan mislykkes på Windows tilladelsesgrænser; fejl forklares, appen kræver ikke standard administrationsrettigheder.

## Undersøgelse og nedtagelse

Automatiserede tests for profilprioritet, konfliktdetektion, konfigurationsimport, atomlagring, makrorefølge, gentagelse, afbrydelse og fejlpropagation. Windows Smoke-tests for virkelige hotkeys, tekst og knapkombinationer, medieaktiviteter, enhedskab, trace og ren slutning. Ingen tests, der tilfældigt starter nogen programmer eller websteder.

GUI-prøve: oprettelse og redigering, optagelse uden udførelse, klik/hotkey med samme effekt, makroudbrud, konfliktmeldelse, profilændring, enhedssyn, import/eksport og skalering. Fabrikantspecifikke hardware-tester dokumenteres separat med det faktiske udstyr.

Levering: Kildekode, reproducerbar Build/Start-vejledning, tysk brugsvejledning og lokal Windows-x64-Publish-Artefakt. Succes betyder en udførlig app med alle generiske funktioner; Corsair-færdigheder er kun markeret som tilgængelige med beviset SDK/enhedstøtte.

## Tekniske kilder

- Microsoft Keyboard Indtast / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Indtastning: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Officielle Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Næste skridt

Efter at have gennemgået dette projekt: udarbejde en konkret gennemførelsesplan og fremsætte udvælgelsen af gennemførelsen. Der er endnu ingen Git-repository i den tomme arbejdsplan; derfor er dette dokument ikke blevet committed.

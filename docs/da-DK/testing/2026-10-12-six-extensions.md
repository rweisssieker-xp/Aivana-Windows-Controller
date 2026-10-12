<!-- generated-doc locale=da-DK source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-12-six-extensions.md)

# Seks udvidelser: gennemførelse og teststand

Stand: 12. Oktober 2026, Branchen `codex/six-extensions`. Arbejdsstil, ingen produktionsfrihed.

## Servicen

Kommandoen åbner indtægterne **Oprettelse af arbejdspladsen**, **Tastaturproducenter og SDK'er**, **Mobilpanel**, **Udvidelser**, **UI-automation**, **UI-afløbet** og **Udførelsesundersøgelse**.

- Arbejdsstilsassistenten opretter profiler for arbejdsplads, møde, streaming, forskning og projekt. Programmer fra registrering/startmenu eller filvalg, URL'er, udgave/skærm og scenen OBS vises som trin før lagring. Institutionen udfører ingen handlinger.
- I trækreditoren **Vurderer resultaterne** indtastes forhåndsundersøgelse og efterkontrol. Kursus og mobilpanel adskiller faktisk bekræftelse fra ukontrollerbar effekt. Relative ændringer, knapkort og tekstindtægter giver ingen fast effektbekræftelse. En procesbevis beviser ikke den faglige tilbøjelighed til anvendelse.
- UI-automation bruger en separat, tidsbegrænset arbejdsgiver. Valg via musposition, selektorprøve, Invoke/Select/Toggle/Focus/SetValue/WaitState; flere hits, deaktiverede mål og adgangskoder afvises. Optagelsen indeholder støttede klik og pauser; tekstindtægter skæres ikke sammen. Inden indtagelse bliver indtagelsen undersøgt.
- Udvidelser: ZIP-test, hash, schema, deaktiveret import, udtrykkelig tillid til aktivering, isoleret proces med begrænset IPC, eksport, deaktivera, afinstallation, genoprette/afvise den tidligere version. Processisolering er ikke sandboks: Delte plugins kører med brugers rettigheder. Eksempelpakken `makrivo.example.zip` indeholder Countdown og Statusaktion. Støtte skemaer udgør en dokumenteret enkel JSON-skema delområde i kildekoden; ingen offentlig markedsplads.
- Mobilpanel: Vælg lokale IP og delte profiler, åbne HTTPS-adressen, tjek certifikatet baseret på det angivne SHA-256-Fingerprints og bevidst stole på, og indtaste derefter en enkelt seks-tegn kode. Private nøgler er beskyttet med Windows-DPAPI; kun hasher af kundeindgangen gemmes. Sertifikater ændres efter fornyelse eller IP-ændring. Udgivelse af Windows Firewall sker efter behov af brugeren. Ingen automatisk certificering, portudvidelse eller firewall ændring. Klienten kan tilbagekaldes. Panelet viser handlingstilstand og testforløb; det er endnu ikke en universel live-sensor-dashboard.

## Tastaturer

| Producenterne | Implementering | Begrænsning |
|---|---|---|
| Corsair | eksisterende iCUE G-knapper og RGB | Lokalt iCUE-forbindelse og passende model |
| Logitech G | G-Key SDK og separate LED-SDK | Original lokal x64-DLL udtrykkeligt frigive; G-Key SDK giver ingen individuel identitet af flere tastaturer |
| Razer | Chroma-RGB og standardknappsidentifikation | Forhåndsindstillingen til Synapse |
| SteelSeries | GameSense-RGB og standardknappsidentifikation | Forudbestemte egenskaber i GG |
| Wooting | Analog værdier relateret til enhed, tryk/slås | original lokal x64-SDK-DLL; normal HID indtagelse forbliver aktiv |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Andre | Fysiske standardknapper efter enhed og skanningskode | Fn og proprietære kontrolelementer afhængig af model |

Der leveres ingen nye proprietære SDK-DLL. De nye adaptere er endnu ikke hardware-valideret uden passende fysiske enheder.

**Den eksklusive anden makropad er ikke færdig:** Raw Input observerer enhederne adskilt, men undertrykker deres normale Windows-En produktionsudgivelse kræver en licenseret, underskrevet, under Windows 11/Secure Boot kontrollerede filtre, samt tests af nødsituation, nedbrydning, suspension og genforbindelse. Ingen drivere er installeret..

## Testerne

- Release-Build: Succesfuld, null advarsler/fejl.
- Core-konsoller: Alle testgrupper er vellykket, herunder konflikt med producenten, plugins og faktiske fejl i testprocessen.
- Udvidelsesintegration: ægte UIA-arbejdere med WPF testapplikation; enestående invoke, flertal, deaktiverede mål, adgangskode undtagelse. Echter Named Pipe Plugin Process, protokolgrænser, Analog Hysteresis og Logitech ABI dekryptering succesfuldt.
- Faktisk HTTPS-host: Tegnede certifikater, uforkoblet adgang, falsk/en-time kode, oprindelsesbeskyttelse, browserfiler, hashopbevaring og beskyttet nøgle succes. En skannelsespørgsel, der blev fundet i testen, med ephemere nøgler, blev rettet.
- Windows-Suite med udtrykkeligt forladt fremgangstest: vellykket; Audio/OBS/iCUE/Raw Input og skærmstatus kontrolleret. i den tidligere løb mislykkede den fulde suite ved den forkastede fokusskift til det isolerede testvindue. i den efterfølgende løb til AI-prototypen bestod også den fulde Suite herunder fokusskift og tilknyttet iCUE-En anden gentagelse mislykkes igen ved fokusskiftet; kontekstbaseret test er dermed ikke stabilt grøn. Denne test blev ikke svækket. korttids mislykkede DDC-læsere gentages begrænset; der blev ikke ændret lys.
- WPF-Smoke: alle tidligere og nye dialoger reduceret, konfigurationsinvariation, editor og CRUD succes; skærmbilleder i byggeri `smoke`.
- Browser: Playwright med Edge, 390×844 og 1024×768, Forbindelsesstrøm/UUID/en gangsendelse/ikke horisontalt overflade vellykket. API-Mock; ægte HTTPS-test separat. `artifacts/mobile-browser`. Browser-plugin var ikke tilgængelig, så regelmæssig Playwright Fallback.
- Uafhængig kode anmeldelse udført; forsinkede mobile bestillinger, manglende terminal testresultater, procesvejskontrol, plug-in rollback og genstart af RGB hjertebevægelse korrigeret.

Endnu åbent: eksklusiv driverintegration, fysiske producenter/telefon/tablet-LAN-tester, Windows scalering 150/200 procent og fuld oversættelse af alle de nye dialogtekster. Nye dialogtekster er hovedsageligt tysk; eksisterende sprogkataloger forbliver bevaret. Det er derfor, at det samlede krav ** ikke er fuldt nedtaget **.

## Produktion af pakke

`./tools/publish-complete.ps1 -SelfContained` skaber en ny mappe under `artifacts/release-*`, herunder skrivebord, UIA-arbejdere, HTTPS-host, browserfiler, importeret eksempelplugin og runtime licensanvisninger. Den installerede applikation overskrives ikke. Uden `-SelfContained` kræver pakken passende .NET-10-Desktop-/ASP.NET-Runtimes.

Pakken er en lokal teststation, ikke en underskrevet salgsinstallator. De egne brugsbetingelser og salgsrettigheder er separate frigivelser; se den eksisterende dokumentation under `docs/licensing`.

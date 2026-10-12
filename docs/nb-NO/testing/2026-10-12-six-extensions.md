<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-12-six-extensions.md)

# Seks utvidelser: gjennomføring og testnivå

Stand: 12. Oktober 2026, Branch `codex/six-extensions`. Arbeidstilstand, ingen produksjonsfrihet.

## Tjenestene

Kommandepalettet åpner innspillene **Oppsett arbeidsplass**, **Tastaturer og SDKs**, **Mobilpanel**, **Utvidelser**, **UI-automasjon**, **UI-avløp** og **Utførelsesundersøkelse**.

- Arbeidsplassassistent oppretter profiler for arbeidsplass, møter, streaming, forskning og prosjekt. Programmer fra registrering/start-menyen eller filvalg, URL-er, utgang/skjerm og OBS scenen vises som trinn før lagring. Instituttet utfører ingen handlinger.
- I trinneditoren slår **resultatet til å sjekke ** forhåndsundersøkelse og etterkontroll. Forløp og mobilpanel skiller faktisk bekreftelse fra ikke-kontrollerbar effekt. Relative endringer, tasteskort og tekstinnføring gir ingen fast effektbekreftelse. En prosessbevis viser ingen faglig forberedelse til bruk.
- UI-automasjon bruker en separat, tidsbegrenset arbeider. Velg via musposisjon, selektorprøve, Invoke/Select/Toggle/Focus/SetValue/WaitState; flere treffer, deaktiverte mål og passord er avvist. Opptaket inneholder støttet klikk og pauser; tekstinntak blir ikke kuttet. Før innkjøp sjekkes innkjøpet.
- Utvidelser: ZIP-test, hash, skema, deaktivert import, uttrykkelig tillit før aktivering, isolert prosess med begrenset IPC, eksport, deaktivering, avinstallasjon, gjenopprette/avvise tidligere versjon. Prosessisolasjon er ikke sandbox: Delte plugins kjører med brukervennlighet. Eksempelpakken `makrivo.example.zip` inneholder Countdown og Statusaction. Støtt skjemmer utgjør et dokumentert enkelt JSON-skjemområde i kildekoden; ingen offentlig markedsplass.
- Mobilpanel: Velg lokale IP- og delte profiler, åpne HTTPS-adressen, sjekk sertifikatet basert på det som vises SHA-256-Fingerprints og bevisst tillit, deretter skriv inn enhetlig seks tegn kode. Private nøkler er beskyttet med Windows-DPAPI; bare hacker av klientinntektene lagres. Sertifikater endres etter fornyelse eller IP-utveksling. Utgivelse av Windows Firewall skjer ved behov av brukeren. Ingen automatisk sertifiseringsinstallasjon, gateway eller firewall endring. Klientenes tilgang kan tilbakekalles. Panelet viser handlingstilstand og testprosess; det er ikke en universell live-sensor-dashboard.

## Tastaturer

| produsent | Implementering | Begrensning |
|---|---|---|
| Corsair | eksisterende iCUE G-tasten og RGB | Lokal iCUE-forbindelse og egnet modell |
| Logitech G | G-Key SDK og separate LED-SDK | opprinnelige lokale x64-DLL uttrykkelig frigjort; G-Key SDK gir ingen individuell identitet av flere tastaturer |
| Razer | Chroma-RGB og standardknapp gjenkjenning | Forhåndsbestemning av Synapse |
| SteelSeries | GameSense-RGB og standardknapp gjenkjenning | Forhåndsutstyr i GG |
| Wooting | Analoger knyttet til enheter, trykk/slåsfaller | original lokal x64-SDK-DLL; normal HID-innføring forblir aktiv |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Andre | Fysiske standardknapper etter enhet og skanningskode | Fn og proprietære kontrollelementer avhengig av modell |

Ingen nye proprietære SDK-DLL leveres med. De nye produsentadaptene er ennå ikke godkjent uten egnet fysisk utstyr.

**Den eksklusive andre makropaden er ikke ferdig:** Raw Input observerer enheter separat, men undertrykker deres normale Windows-En produksjonsutgivelse krever en lisens, signert, under Windows 11/Secure Boot kontrollerte filterdriver, samt tester av nødsituasjon, kollapse, suspension og gjenoppkobling. Ingen drivere er installert..

## Tester

- Release-Build: vellykket, null advarsler/feil.
- Core-konsoler: Alle testgrupper er vellykket, inkludert konflikter med produsentens trykk, plugins og faktiske utføringsfeil i testprosessen.
- Utvidelsesintegrasjon: ekte UIA-arbeider med WPF testapplikasjon; unik invoke, merverdighet, deaktivert mål, passord unntak. Echter Named Pipe Plugin Process, protokollgrenser, Analog Hysteresis og Logitech ABI dekryptering vellykket.
- Virkelig HTTPS-host: Trykkert sertifisering, uoppkoblet tilgang, falsk / enkeltkod, opprinnelsesbeskyttelse, nettleserfiler, hashlagring og beskyttet nøkler vellykket. En skannel feil oppdaget i testen med ephemere nøkler er rettet.
- Windows-Suite med uttrykkelig utløst forgrunnsresultater: vellykket; Audio/OBS/iCUE/Raw Input og skjermstatus sjekket. i den forrige løp mislyktes den komplette suiteen ved den avviste fokusskiftet til det isolerte testvinduet. i den etterfølgende løp til AI-prototypen bestod også den komplette Suite, inkludert fokusskiftet og tilkoblet iCUE-En annen gjentakelse mislykkes igjen i fokusskifte; kontekstbasert test er dermed ikke stabil grønn. Denne testen ble ikke svekket. kortsiktig feil DDC-leser blir begrenset igjen; det ble ikke endret lyshet.
- WPF-Smoke: alle tidligere og nye dialoger, konfigurasjonsinvariasjon, redaktør og CRUD vellykket; skjermbilder i byggordenen `smoke`.
- Nettleser: Playwright med Edge, 390×844 og 1024×768, Koblingstrøm/UUID/en gangsending/ikke horisontell overflod vellykket. API-Mock; ekte HTTPS-test separat. `artifacts/mobile-browser`. Browser-plugin var ikke tilgjengelig, derfor regelmessig Playwright Fallback.
- Uavhengige koden revideres; forsinket mobilkommandoer, manglende terminale testresultater, prosessveiledning, plug-in-rollback og gjenoppstart av RGB-heartbeat korrigert.

Fortsatt åpen: eksklusiv driverintegrasjon, fysiske produsent-/telefon-/tablett-LAN-tester, Windows-skalaering 150/200 prosent og full oversettelse av alle nye dialogtekster. Nye dialogtekster er hovedsakelig tysk; eksisterende språkkataloger forblir bevarte. Det er derfor den samlede kravet ** ikke er fullstendig redusert **.

## Produksjon av pakke

`./tools/publish-complete.ps1 -SelfContained` genererer en ny mappe under `artifacts/release-*`, inkludert skrivebord, UIA-arbeider, HTTPS-host, nettleserfiler, importerbar plugin og runtime-lisensiering. Den installerte applikasjonen blir ikke oversatt. Uten `-SelfContained` trenger pakken passende .NET-10-Desktop-/ASP.NET-Runtimes.

Pakken er et lokalt teststed, ikke en signert salgsinstaller. Egen bruk av vilkår og betingelser og salgsrettigheter er separate utgivelser; se eksisterende dokumentasjon under `docs/licensing`.

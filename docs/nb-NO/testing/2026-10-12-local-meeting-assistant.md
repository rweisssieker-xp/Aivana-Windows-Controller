<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokalt AI-assistent - Prototype

Stand: 12. Oktober 2026. ForgeMind Hypothesis "Outcome Agent", begrenset møtefløyde. Ingen bevist konkurransefordel og ingen målt tidsbesparelse uten virkelige sammenligningsforsøk.

## Bruk av

1. Kommandespalett → **KI-forhandlingshjelp** Eksperimentet er standard og blir ved konfigurasjon eller import. Portrett eksporteres tilbake.
2. Aktivere lokale assistenter. Ollama Må være separat `127.0.0.1:11434` kjører; en egnet lokal modell må allerede være installert. **Lokale modeller** Velg modell. MAKRIVO Ikke installere modeller/tjenester, ikke kontakte OpenAI/Cloud API og ikke bruke API-nøkler. Ollama-Tjenestene er hos brukeren; deklarerte fjern-/kloemodeller avvises.
3. Skriv inn mål og legge til programmer/dokumenter/nettsteder. Formål, ressurs-ID, navn og type overføres; målsettingen og innholdet i utvalgte filer overføres ikke ytterligere; målsettet selv sendes bokstavelig. Nettsidene inneholder bare skema/host, den komplette koblingen forblir lokal.
4. **Gjør AI-prosjekt**. Maksimal 90 Sekunder, 1024 Utgangsbokker, begrenset svarstørrelse. Mål-/modell/ressursendringer avviser forhåndsvisning; Avbrudd/slutt oppforespørselen. Modellen kan kun foreslå utvalgte ID-er. ukjente ID-er, frie kommandoer, ytterligere JSON-felt og utførbare dokumenttyper blir avvist.
5. Forhåndsvisning og åpne poeng sjekk → **I redaktøren sjekk**. Resultatet er først og fremst et uoppbevarte prosjekt. Lagre først i den eksisterende redaktøren og kjøre uttrykkelig hvis det er nødvendig. Normal makrovalidering, for-/over-kontroll, pause og avbrudd forblir ansvarlig.

Støtt: Start programmer uten argumenter, åpne uttrykkelig utvalgte lokale PDF/DOCX/XLSX/TXT/MD, åpne HTTP(S) nettsider, slukke mikrofonen etter uttrykkelig ønske. Ingen headset standard endring, ingen meldinger, datoer, nedlastinger, skript, gratis klikk eller tekstinnføringer. Ikke-bildbare ønsker vises som åpne poeng, hvis modellen gjenkjenner dem riktig; den faglige oppfyllelsen må sjekkes av brukeren. Åpning av dokumenter er ikke automatisk verifisert.

## Sammenlignende eksperiment

**Manuel innstilling måler · Start / End** måler den fullstendige manuelle sammenligningsprosessen. Assistentinntak måler tiden fra åpning av assisten til den første lagringen i redaktøren; `generated` inneholder imidlertid bare planleggingstiden. Ikke sammenligne de to uavhengige varighetene som tidsbesparelser.

Lokale hendelser er i konfigurasjonsordningen under `Experiments/meeting-experiment.jsonl`: tidstempel, tilfeldig forsøk/kachel-ID, hendelse, varighet, korreksjon ja/ne. Ingen måltekster, ressursnavner, veier, svar på modeller eller dokumentinnhold. Filen roterer fra 2 MiB, en tidligere fil blir opprettholdt. Målefeil kan ikke avbryte makro og konfigurasjonslagring.

Inkluderer: manuell baselinje, modellgenerering/feil/avbrudd, forvirrende forhåndsvisninger/sletting, første gang lagring og korreksjoner i redigeringen, og faktiske Runner-tilstander for lagrede låser. `run_completed` betyr bare at Runner er ferdig; bekreftede målvirkninger er separat i testprosessen. Repetisjoner av samme makro er ikke nye uavhengige prosjekter. Det opprinnelige eksperimentet kan fortsettes etter nystart, fordi forsøkets ID forblir på den lagrede kabelen. Dupliserte kjeler deler denne opprinnelsen; i evaluering etter prøve-ID grupper.

Første eksperiment: 20 sammenlignbare møter eller to uker med frivillige testbrukere; først intern bruk. Sammenlign manuell installasjonstid mot fullstendig assisterte installasjonstid; i tillegg vurderer korreksjons-/avbruddssatser, løperefeil og virkelige testresultater. I tilfelle av mangel på forbedring eller brudd på beskyttelsesgrensene deaktivere. Ingen bruk eller tilbakemelding ble vist i repositoryen. Ingen automatisk telemetrioverføring eller prisjustering.

## Testerstand

- Core: Begrensninger, typer, ukjente/dubliserte IDs, utelukkelse av skript, lange koblinger, valgfri alt-konfigurasjon, import/eksport-opt-in, datasparbar måling, samt enstemmig avvisning og riktig overføring.
- Transport med falsk modell: Ollama JSON-skjema, Loopback-mål, ingen filfase i prompt, skynavne og fjerntjenester avvist, skadet/overlenget svar avvist, avbrudd.
- WPF-Smoke: Dialog redusert; Design overføring endrer ikke lagret låser, anmeldelse/lagring får opprinnelse og oppdager korreksjon, avviser ikke lagrer noe.
- Uavhengige anmeldelse: forkastet og lange koblinger funnet og korrigert; Regressjonstester RED→GREEN.
- Release-build med null advarsler/feil, hele Core-Suite og WPF Smoke vellykket. Den komplette Windows-suiet bestod i utgangspunktet, inkludert fokusskifte. Et nytt løp mislyktes igjen ved den avvist forandring; testen er kontekstbasert og forblir uendret. De øvrige Windows testene, inkludert modelltransport, utføres med uttrykkelig forkastet resultatstest.
- Pakkeundersøkelsen oppdaget en eksisterende feil ved atomutveksling av kortvarige konfigurasjonsfiler. En lesesperre gjenopprettet den samme tilgang-avvist feil; begrenset gjentakelse av bare atomutveksling er RED→GREEN sjekket. Vedvarende tilgangsfeil er synlig.
- Nåværende uavhengig pakke `artifacts/release-20261012-184105`: WPF smoke etter lagring korrigering vellykket, inkludert AI-dialog og prosjektadoppsett/avslag.
- Ingen ekte modelltest: Den lokale Ollama-tjenesten svarte ikke på testen. Ingen uttalelser om modellkvalitet, latens, semantisk fullstendighet eller tidsbesparelse. Hardware/driver/skalieringsgrenser for den tidligere utgaven forblir.

Protokoll kilder: [Ollama Chat](https://docs.ollama.com/api/chat), [Strukturerte utganger](https://docs.ollama.com/capabilities/structured-outputs), [Offisielle typedefinisjoner, inkludert fjernmodelldata](https://github.com/ollama/ollama/blob/main/api/types.go).

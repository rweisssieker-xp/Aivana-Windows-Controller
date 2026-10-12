<!-- generated-doc locale=da-DK source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokalt AI-rådgivere – prototype

Stand: 12. Oktober 2026. ForgeMind-hypothese "Outcome Agent", begrænset mødeforløb. Ingen bevist konkurrencefordel og ingen målt tidsbesparelse uden virkelige sammenligningsforsøg.

## Brug af

1. Kommandepalettet → **KI-forespørgselsassistent** Eksperimentet er standard, og det bliver ved indførsel af konfiguration eller Portemærket er tilbagekaldt.
2. Aktivere lokale assistenter. Ollama Måske separat `127.0.0.1:11434` at køre; en passende lokal model skal være installeret. **Lokale modeller** Vælg model. MAKRIVO installerer ikke modeller/tjenester, kontakter ikke OpenAI/Cloud API og bruger ikke API-nøgler. Ollama-Tjenesterne er hos brugeren; erklærede fjern-/cloudmodeller afvises.
3. Indtast mål og tilføje programmer/dokumenter/websteder. Indsendes mål, ressource-ID, navn og type; målvej og indhold af udvalgte filer sendes ikke yderligere; målindsendelsen sendes bogstaveligt. Hjemmesidenavn indeholder kun skema/host, den fulde link forbliver lokal.
4. **Skab en AI-design**. Maximal 90 Sekunder, 1024 Udgangsbokker, begrænset svarstørrelse. mål-/model-/ressourcer ændringer afviser forudsigelsen; afbrydelse/sluk afslut spørgsmålet. modellen kan kun foreslå udvalgte ID'er. ukendte ID's, frie kommandoer, yderligere JSON-felt og udførelige dokumenttyper afvises.
5. Prøv forhåndsvisningen og åbne punkter → **Vurder i redaktøren**. Resultatet er i første omgang et ubesvaret projekt. Spare først i den eksisterende editor og udføre det udtrykkeligt, hvis det er nødvendigt. Normal makrovalidation, for- og efterprøvning, pause og afbrydelse forbliver ansvarlige.

Støttet: Start programmer uden argumenter, åbne udtrykkeligt udvalgte lokale PDF/DOCX/XLSX/TXT/MD, åbne HTTP(S) hjemmesider, lukke mikrofonen efter udtrykkelig ønske. Der er ingen standardudskiftning af headset, ingen meddelelser, datoer, downloads, skript, gratis klik eller tekstindtægter. Ikke-billedlige ønsker vises som åbne punkter, hvis modellen genkender dem korrekt; den faglige opfyldelse må brugeren undersøge. Åbningen af dokumentet er ikke automatisk verifiseret.

## Sammenligningsexperiment

**Manuel indstilling måler · Start / End** måler den fulde manuelle sammenligningsproces. Assistentoptagelser måler tiden fra at åbne assisten til den første lagring i editoren; `generated` indeholder dog kun planlægningstiden. Ikke sammenligne de to uoverensstemmelser som tidsbesparelse.

Lokale begivenheder er i konfigurationsordningen under `Experiments/meeting-experiment.jsonl`: tidstempel, tilfældig forsøg/kage-ID, begivenhed, varighed, korrektion ja/ne. Ingen måltekster, ressourcebetegnelser, veje, modellen svar eller dokumentindhold. Filen roteres fra 2 MiB, en tidligere fil forbliver. Mæringsfejl må ikke afbryde makroer og konfigurationsopbevaring.

Indbefatter: manuel baseline, modelgenerering/fejl/afbrydelse, forvirrende forudsigelser/slukning, førsteindholdsopbevaring og korrektioner i editoren samt faktiske Runner-tilstande for de indbevarede låser. `run_completed` betyder kun, at Runner er færdig; bekræftede målvirkninger er separat i testforløbet. Gentagerne af samme makro er ikke nye uafhængige forsøg. Det oprindelige eksperiment kan fortsættes efter genstart, fordi forsøgs-ID forbliver på den gemte kage. Duplicerede kager deler denne oprindelse; når evaluering efter prøve-ID grupper.

Første eksperiment: 20 sammenlignelige møder eller to uger med frivillige testbrugere; først intern brug. Sammenlign manuelt indstillingstid med fuld assisteret indstillingstiden; derudover evaluerer korrektion/forkastningsrate, Runner-fejl og virkelige testresultater. I tilfælde af manglende forbedring eller krænkelse af beskyttelsesgrænser deaktiverer switch. Ingen brug eller tilbøjelighed er vist i repositoryen. Ingen automatiske telemetrioverførsel eller prisjustering.

## Prøve stand

- Core: Begrænsninger, typer, ukendte/dubliserede ID'er, udelukkelse af skriptet, lange links, valgfri alt-konfiguration, import/eksport-opt-in, databesparende måling samt enkelt afvisning og korrekt overføring.
- Transport med falsk model: Ollama JSON-skema, Loopback-mål, ingen filfejl i prompt, cloud-navne og fjernbetegnelser afvist, beskadiget/overlænget svar afvist, afbrydelse.
- WPF-Smoke: Dialog reduceret; Design overførsel ændrer ikke gemte kager, anmeldelse/lagring får oprindelse og opfanger korrektion, afviser opbevarer intet.
- Uafhængig anmeldelse: forudsigelse og lange links fundet og korrigeret; Regression test RED→GREEN.
- Release-build med nul advarsler/fejl, hele Core-Suite og WPF Smoke succes. Den fulde Windows suite bestod i første omgang, herunder fokusændring. En ny løb mislykkede igen ved den nægtede forandring; testen er kontekstbaseret og forbliver uændret. De øvrige Windows-prøver, herunder modetransport, er udtrykkeligt forladt med fremgangsmåden.
- Pakkeundersøgelsen dækker en eksisterende fejl ved at udveksle kortvarige konfigurationsfiler. En læsebrygger reproduceret den samme adgang-afvist fejl; begrænset gentagelse af kun atomudveksling er RED→GREEN testet. Langvarige fejl i adgangen forbliver synlige.
- Aktuelt uafhængig pakke `artifacts/release-20261012-184105`: WPF smoke efter opbevaring korrigering med succes, herunder AI dialog og design modtagelse/afvisning.
- Ingen reel modeltest: Den lokale Ollama service svarede ikke på testen. Ingen udtalelser om modelkvalitet, latens, semantisk fuldstændighed eller tidsbesparelse. Hardware/driver/skalieringsgrænser for den tidligere udgivelse forbliver.

Protokolkilderne er: [Ollama Chat](https://docs.ollama.com/api/chat), [Strukturerede udbytte](https://docs.ollama.com/capabilities/structured-outputs), [Officielle typedefinitioner, herunder fjernmodeldata](https://github.com/ollama/ollama/blob/main/api/types.go).

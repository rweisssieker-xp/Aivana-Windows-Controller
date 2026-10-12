<!-- generated-doc locale=sv-SE source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokala AI-assistenter – Prototyp

Stannar: 12. oktober 2026. ForgeMind Hypothesis "Outcome Agent", begränsad samtalsprocess. Inga bevisade konkurrensfördelar och ingen mätad tidsbesparing utan verkliga jämförelser.

## Användning

1. Kommandespalett → **KI-förhörsassistent** Experimentet är standard och görs vid konfiguration eller import. Portföljen återkallades.
2. Aktivera lokala assistenter. Ollama Måste separat `127.0.0.1:11434` körning; en lämplig lokal modell måste redan installeras. **Lokala modeller Ladda** Välj en modell. MAKRIVO installerar inte modeller/tjänster, kontaktar inte OpenAI/Cloud API och använder inte API-nyckeln. Ollama-Tjänsterna är hos användaren; deklarerade fjärr-/cloudmodeller avvisas.
3. Ange mål och lägga till program/dokument/webbplatser uttryckligen. Mål, resurs-ID, namn och typ skickas; målvägen och innehållet i de valda filerna skickas inte ytterligare; målsättningen skickas bokstavligen. Webbplatsnamnen innehåller endast schema/host, den fullständiga länken förblir lokal.
4. **Skapa ett AI-projekt**. Maximal 90 sekunder, 1024 Utgångspunkter, begränsad responsstorlek. Mål-/modell/resursändringar vägrar förhandsvisning; Avbrott/Slutning avfrågan. Modellen får endast föreslå utvalda ID:er. Okända ID:ar, fria kommandon, ytterligare JSON-fält och utförbara dokumenttyper vägras.
5. Förhandsgranska och kontrollera öppna punkter → **Visa i redaktören**. Resultatet är i första hand en obefintlig planering. Spara först i den befintliga redaktören och utföra uttryckligen vid behov. Normala makrovalidering, för- och övervakning, paus och avbrott förblir ansvariga.

Stödda: Starta program utan argument, öppna uttryckligen markerade lokala PDF/DOCX/XLSX/TXT/MD, öppna HTTP(S) webbplatser, stänga av mikrofonen på ett uttryckligt önskemål. Inga standardhögtalare, inga meddelanden, datum, nedladdningar, skript, fria klick eller textrådar. Obildbara önskemål visas som öppna punkter, förutsatt att modellen känner igen dem korrekt; den professionella uppfyllandet av målen måste användaren kontrollera. Dokumentet är inte automatiskt verifierat.

## Jämförelse experiment

**Manual mätning · Start / slut** mätning av den fullständiga manuella jämförelsen. Assistentupptagning mäter tiden från att öppna assistenten till den första lagringen i redaktören; `generated` innehåller däremot endast planeringstiden. Inte jämföra de två olika tidsperioderna som tidsbesparing.

Lokala händelser finns i konfigurationsordningen under `Experiments/meeting-experiment.jsonl`: tidsbegränsning, slumpmässigt försök/kabel ID, händelse, varaktighet, korrigering ja/nej. Inga måltexter, resursnamn, vägar, svar på modeller eller dokumentinnehåll. Filen roteras från 2 MiB, en tidigare fil förblir kvar. Mätfel får inte avbryta makron och konfigurationslagring.

Innehåller: manuell baslinje, modellgenerering/fel/avbrott, förvirrande förhandsändringar/sluts, första lagring och korrigeringar i redaktören samt verkliga Runner-ändningar för sparas från den. `run_completed` betyder bara att Runner har slutförts; bekräftade målverkningar är separat i testet. Upprepningar av samma makro är inte nya oberoende installationsförsök. Det ursprungliga experimentet kan fortsätta efter nystart, eftersom försöks-ID förblir på den lagrade kakan. Duplicerade klackar delar denna ursprung; vid utvärdering efter försök ID grupperas.

Första experimentet: 20 jämförbara mötesanläggningar eller två veckor med frivilliga testanvändare; först intern användning. Jämför manuell inställningstid mot full assisterad inställningstiden; dessutom utvärdera korrigering/avbrottsfrekvens, Runner-fel och verkliga testresultat. I händelse av brist på förbättring eller kränkning av skyddsgränser inaktiveras. Inga användnings- eller betalningsförmåner har visats i registret. Ingen automatisk telemetriöverföring eller prisjustering.

## Testning

- Kärn: Begränsningar, typer, okända/dubblerade ID, skriptutesluts, långa länkar, valfri alt-konfiguration, import/export-opt-in, datasparande mätning samt enstaka avslag och korrekt överföring.
- Transport med falsk modell: Ollama JSON-schema, Loopback-målet, ingen filvägar i prompt, molnnamn och fjärrvärden avvisas, skadad/överlängd svar avvisas.
- WPF-Smoke: Dialog reducerad; Design överföring ändrar inte lagrade lockar, recension/lagring får ursprung och registrerar korrigering, avvisar sparar ingenting.
- Oberoende granskning: förkastade och långa länkar hittade och korrigerade; Regressionstester RED→GREEN.
- Release-Build med noll varningar/fel, hela kärn-Suite och WPF Smoke framgångsrikt. Den fullständiga Windows-sidan bestod från början inklusive fokusväxling. Ett nytt lopp misslyckades igen vid den förnekade bakgrunden; testet är kontextuellt och förblir oförändrat. Övriga Windows tester, inklusive modelltransport, utgörs av en uttryckligen uteslutande framgångstest.
- Paketkontroll upptäckte ett befintligt fel vid atomutbyte av kortvariga konfigurationsfiler. En läsare upprepad samma åtkomst-avvisade fel; begränsad upprepning av endast kärnväxling är RED→GREEN kontrolleras. Allvarliga fel kvarstår.
- Aktuellt självständigt paket `artifacts/release-20261012-184105`: WPF smuts efter lagring korrigering framgångsrikt, inklusive AI-dialog och designtagande/avslag.
- Inga verkliga modeller: den lokala Ollama-tjänsten svarade inte vid provningen. Inga uttalanden om modellkvalitet, latens, semantisk fullständighet eller tidsbesparing. Hårdvara/drivrutiner/skalargränser för den tidigare versionen förblir kvar.

protokollkällor : [Ollama Chatt](https://docs.ollama.com/api/chat), [Strukturerade utgångar](https://docs.ollama.com/capabilities/structured-outputs), [Officiella typdefinitioner inklusive fjärrmodelldata](https://github.com/ollama/ollama/blob/main/api/types.go).

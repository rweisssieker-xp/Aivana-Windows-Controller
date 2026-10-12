<!-- generated-doc locale=nb-NO source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../MAINTENANCE.md)

# Oppbevaring av alle dokumentasjonsspråk

Dokumentasjonen følger alle 27 UI språk. de genererte mapper bruker regionale koder; norske kart til `nb-NO` Den serbiske latin `sr-Latn`. Hver mappe inneholder en indeks, håndbok, detaljerte handlingsreferanser, funksjonsinventor, verifiseringsnotater, forskning inkludert CSV, historiske planer og lisensanalyse.

## Kildene til sannheten

Rediger roten `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Denne siden, eller de opprinnelige filene under `docs/research`, `docs/testing`, `docs/superpowers` og `docs/licensing`. Ikke skrive om en historisk rapport som innebærer at den validerer en senere versjon. Hold implementering, simulering, ekte verktøy godkjennelse og kommersiell godkjenning forskjellig.

## Generere og validere

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Den valgfrie nettgeneratoren sender dokumentasjon til Googles offentlige oversettelsesendpunkt, etter eksisterende lokaliseringsverktøy. Det leser ikke brukerkonfigurasjon, hemmeligheter eller modellfiler. Bruk bare dokumentasjon som er egnet for dette. Kodeblokker, inline-koder, URL-adresser, tabellstrukturer og tekniske identifikasjoner er beskyttet. Oversettelseskasjer og et frosnet kilde snapshot lagres under ignorert `artifacts/documentation-translation`.

Når onlineleverandøren er begrenset, fyller `tools/translate_documentation_local.py` den samme cache ved hjelp av en uttrykkelig levert lokal M2M100 CTranslate2 modell. Den 2026-10-12 snapshot bruker MIT-licensert facebook/m2m100_418M modell, konvertert som JustFrederik/m2m_100_418m_ct2_int8 ved revision `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 og `SentencePiece` 0.2.2 er bygg-only verktøy. Ingen modellvekter eller oversettelsesavhengigheter sendes med MAKRIVO. Kjør generatoren med `--cache-only` etter å ha fylt cache for å gi uten nettverksforespørsler. Den lokale assistenten lastes ikke ned eller kjører fjernmodellkoden.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Uten `--refresh-sources` gjenoppretter en løp sin frosne snapshot. Bruk alternativet etter å ha redigert originalene. De skjulte segmentene blir gjenbrukt. En delvis kjøring kan velge lokale koder ved hjelp av `--locales`; avslutte alle lokale før publisering. Avbrutt løp er ikke stille for å erstatte uoversatt prose for en feil oversettelse.

Det manifesterer alle kilder og genererte filer med SHA-256 Fingeravtrykk av normal UTF-8 tekst: LF-linje endinger og ingen byte-order. Den offline sjekker verifiserer fullstendig per-språk dekning, fingeravtrykk, lokale koblinger, kodeksempler, tabell/CSV struktur, kilde URLs og uoppløpt plass holdere. Det nekter stale kilder. En gjennomgående strukturell sjekk er ikke en innfødte talers vurdering eller juridisk godkjenning. Genererte sider bærer denne kvalifikasjonen synlig.

## Publikasjon og pakker

Utgiv dokumentasjonen, generatoren og sjekkeren sammen. Oppbevar opprinnelige tredjeparts lisensbetingelser; oversatt lisensanalyse er informativ. Bygge artefakter, oversettelse cache og lokale orkester filer tilhører ikke Git. En kilde-dokumentasjon publiserer ikke selv en Windows installator.

Brukervisende appoversettelser er separate JSON-kataloger under `src/Aivana.Core/Localization`. Dokumentasjonsgenerasjon endrer ikke disse katalogene. En app-bygging og dens dokumentasjon bør dele en versjonert snapshot; nylig beskrevet eksperimentell runtime-støtte kan gå foran et installert pakke.

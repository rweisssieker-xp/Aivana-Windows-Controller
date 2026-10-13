<!-- generated-doc locale=da-DK source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../MAINTENANCE.md)

# Vedligeholdelse af alle dokumentationssprog

Dokumentationen følger alle 27 UI sprog. de genererede mapper bruger regionale koder; norske kort til `nb-NO` Den serbiske latin til `sr-Latn`. Hver mappe indeholder en indeks, håndbog, detaljeret handling reference, funktion inventar, verifikation noter, forskning herunder CSV, historiske planer og licensanalyse.

## Kilde til sandheden

Rediger rødmen `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Denne side, eller de originale filer under `docs/research`, `docs/testing`, `docs/superpowers` og `docs/licensing`. Ikke genskrive en historisk rapport, der indebærer, at den validerer en senere version. Hold implementering, simulering, virkelige værktøjer og kommercielle godkendelser adskilt.

## Generere og validere

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Den valgfrie online generator sender dokumentationsproget til Googles offentlige oversættelsesendpoint, efter eksisterende lokaliseringsværktøj. Det læser ikke brugerkonfiguration, hemmeligheder eller modelfiler. Det er kun dokumentation, der er egnet til denne service. Kodeblokke, inline-kode, URL'er, tabelstruktur og tekniske identifikatorer er beskyttet. Oversættelse cacher og en frosnet kilde snapshot er gemt under ignoreret `artifacts/documentation-translation`.

Når onlineudbyderen er begrænset, `tools/translate_documentation_local.py` Fyller den samme cache ved hjælp af en udtrykkeligt leveret lokal M2M100 CTranslate2 Modellen er 2026-10-12 Snapshot bruger MIT-licens facebook/m2m100_418M Modellen er konverteret til JustFrederik/m2m_100_418m_ct2_int8 Ved revision `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 og `SentencePiece` 0.2.2 er kun bygningsværktøjer. ingen modelvægt eller oversættelsesafhængigheder sendes med MAKRIVO. Gennemfør generatoren med `--cache-only` efter at have udfyldt cache for at give uden netværksforespørgsler. den lokale assistent downloader ikke eller kører fjernmodellen kode.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Uden `--refresh-sources` genoptager en kørsel sit fastfrosne snapshot. Brug opdateringsindstillingen efter redigering af originaler. Cachede segmenter genbruges. En delvis kørsel kan vælge sprogkoder med `--locales`; færdiggør alle sprog før udgivelse. Afbrudte kørsler erstatter ikke ubemærket en mislykket oversættelse med uoversat tekst.

Manifestet registrerer hver kilde og genereret fil med SHA-256 fingeraftryk af standardiseret UTF-8 tekst: LF-linjeindtægter og ingen byte-ordre. Den offline checker kontrollerer fuldt per-language dækning, fingeraftryk, lokale links, kode eksempler, tabel/CSV struktur, kilde URL'er og ubetinget placering. Det afviser stålkilder. En gennemførende strukturkontrol er ikke en indfødte anmeldelse eller juridisk godkendelse. Genererede sider bærer denne kvalifikation synligt.

## Udgivelse og pakker

Udgiv dokumentation, generator og checker sammen. Bevar originalt tredjeparts licensbetingelser; oversat licensanalyse er informativ. Konstruktion af arkiver, oversættelse cache og lokale orkester filer tilhører ikke Git. En kilde-dokumentation udgivelse offentliggør ikke en Windows installator.

Appoversættelser er separate JSON-kataloger under `src/Aivana.Core/Localization`. Dokumentation generationen ændrer ikke disse kataloger. En app-bygning og dens dokumentation bør dele en version af et snapshot; nyligt beskrevet forsøgstidsstøtte kan forud for et installeret pakke.

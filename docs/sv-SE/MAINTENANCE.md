<!-- generated-doc locale=sv-SE source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../MAINTENANCE.md)

# Hålla alla dokumentationsspråk

Dokumentationen följer alla 27 UI-språk. de genererade mapparna använder regionala koder; Norra kartor för `nb-NO` och serbiska latin till `sr-Latn`. Varje mapp innehåller en index, handbok, detaljerad åtgärdsreferens, egenskapsinventering, verifiering noter, forskning inklusive dess CSV, historiska planer och licensanalys.

## Källa till sanning

Redigera roten `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, den här sidan, eller de ursprungliga filerna under `docs/research`, `docs/testing`, `docs/superpowers` och `docs/licensing`. Skriv inte om en historisk rapport för att innebära att den validerar en senare version. Håll implementering, simulering, verklig utrustning acceptans och kommersiell godkännande separat.

## Generera och validera

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Den valfria generatorn skickar dokumentationsproget till Googles offentliga översättningsändpunkt, enligt befintliga lokaliseringsverktyg. Det läser inte användarkonfiguration, hemligheter eller modellfiler. Använd endast dokumentation som är lämplig för denna tjänst. Kodblock, inlinekod, URL-adresser, tabellstruktur och tekniska identifierare är skyddade. Översättningskassor och ett fryst källsignal lagras under ignorerad `artifacts/documentation-translation`.

När onlineleverantören är ränta begränsad fyller `tools/translate_documentation_local.py` samma cache med en uttryckligen levererad lokal M2M100 CTranslate2 modell. Den 2026-10-12 snapshot använder MIT-licensierad facebook/m2m100_418M modell, konverterad som JustFrederik/m2m_100_418m_ct2_int8 vid revision `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 och `SentencePiece` 0.2.2 är bygg-enbart verktyg. Inga modellvikter eller översättningsberoende skickas med MAKRIVO. Kör generatorn med `--cache-only` efter att ha fyllt cache för att leverera utan nätverksförfrågningar. Den lokala hjälpen laddar ner eller kör fjärrmodellkod.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Utan `--refresh-sources` återställer en kör sin frysta snapshot. Använd alternativet för uppfriskning efter redigering av original. De dolda segmenten återanvänds. En partiell körning kan välja lokala koder med `--locales`; slutföra alla lokala innan publicering. Avbruten körning ersätter inte tyst en oöversatt prosa för en misslyckad översättning.

Manifestet registrerar varje källkod och genererad fil med SHA-256 fingeravtryck av standardiserad UTF-8 text: LF-linjeändningar och ingen byte-order. Offline-checkeren kontrollerar fullständig språkbaserad täckning, fingeravtryck, lokala länkar, kodexempler, tabell/CSV-struktur, källor och obetalda platsinnehavare. Det avvisar stålkällor. En genomgående strukturell kontroll är inte en inhemska översyn eller lagligt godkännande. Genererade sidor bär denna kvalifikation synligt.

## Publicering och paket

Publicera dokumentationen, generator och checker tillsammans. Bevara originallicensvillkor från tredje part; översatt licensanalys är informativ. Bygg artefakter, översättning cache och lokala orkester filer tillhör inte Git. En källdokumentationsutgåva publicerar inte själv en Windows installatör.

Användarvänliga appöversättningar är separata JSON-kataloger under `src/Aivana.Core/Localization`. Dokumentation genererar inte dessa kataloger. En appbyggnad och dess dokumentation bör dela en version av ett snapshot; nyligen beskrivna experimentella körtidstöd kan föregå ett installerat paket.

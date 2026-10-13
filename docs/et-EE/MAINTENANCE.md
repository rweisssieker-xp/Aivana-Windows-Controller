<!-- generated-doc locale=et-EE source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../MAINTENANCE.md)

# Kogu dokumendi keele säilitamine

Dokumentatsioon hõlmab kõiki 27 kasutajaliidese keelt. Loodud kaustad kasutavad piirkonnakoode; norra keele kood on `nb-NO` ja ladina kirjas serbia keele kood `sr-Latn`. Igas kaustas on sisukord, käsiraamat, üksikasjalik toimingute teatmik, funktsioonide loend, kontrollimärkmed, uuringud koos CSV-failiga, ajaloolised plaanid ja litsentsianalüüs.

## Tõe allikas

Edit juure `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, See lehekülg või originaalfailid allpool `docs/research`, `docs/testing`, `docs/superpowers` ja `docs/licensing`. Ärge kirjutage uuesti ajaloolist aruannet, mis tähendab, et see kinnitab hiljem versiooni. Hoidke rakendamine, simulatsioon, reaalne seadme vastuvõtmine ja kaubanduslik heakskiitmine eraldi.

## Sissejuhatus ja valideerimine

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Valikuline veebipõhine generaator saadab dokumendi proosi Google'i avalikule tõlke lõpppunkti, järgides olemasolevat lokaliseerimisvahendit. See ei loe kasutaja konfigureerimist, saladusi või mudeli faile. Tegelege ainult sellise teenuse jaoks sobiva dokumendi alusel. Koodi blokeeringud, sisekoodid, URL-id, tabeli struktuur ja tehnilised identifikaatorid on kaitstud. Tõlkekahjud ja külmutatud allikas snapshot salvestatakse ignoreeritud `artifacts/documentation-translation`.

Kui veebipõhine teenuseosutaja on piiratud, `tools/translate_documentation_local.py` täidab sama cache, kasutades selgelt tarnitud kohalikku M2M100 CTranslate2 Moodil on 2026-10-12 Snapshot kasutab MIT-litsentsi facebook/m2m100_418M Mood, mis on konverteeritud JustFrederik/m2m_100_418m_ct2_int8 Ülevaade `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 ja `SentencePiece` 0.2.2 on ehitatud ainult tööriistad. ei ole mudeli kaalud ega tõlke sõltuvused saadetakse MAKRIVO. Jälgige generaatorit `--cache-only` Pärast koodi täitmist, et esitada ilma võrgu taotluseta. kohalik abistaja ei lae alla ega täida kaugmodu koodi.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Ilma `--refresh-sources` ei tohi käik oma külmutatud pilti taastada. Kasutage värskendamise võimalust pärast originaali redigeerimist. Peidetud segmendid on taaskasutatud. Osaline käivitamine võib valida kohalike koodide abil `--locales`; lõpetada kõik kohalikud enne avaldamist. Katkestatud rünnakud ei asenda vaikselt tõlkimata proosiga ebaõnnestunud tõlget.

Manifest registreerib iga allikat ja genereeritud faili SHA-256 sõrmejälgi standarditud UTF-8 teksti: LF-riigi lõpp ja mitte byte-määrusmärk. Offline-kontrollija kontrollib täielikku keelealast katmist, sõrmejälgi, kohalikke lingid, koodi näiteid, tabeli/CSV struktuuri, allikate URL-d ja asendamatuid kohapealisi. Ta lükkab tagasi staalid. Struktuuriline kontroll ei ole emakeele läbivaatamine või õiguslik heakskiit. Loodud leheküljed toovad selle kvalifikatsiooni nähtavaks.

## Publikatsioon ja pakendid

Kirjeldage dokumendid, generaator ja kontroll. Säilitage originaalsed kolmandate osapoolte litsentsitingimused; tõlgitud litsentsianalüüs on informatiivne. Ehitusartikleid, tõlkekahju ja kohalikke orkestritefaile ei kuulu Git. Allikas dokumenteerimise vabastamine ise ei avalda Windows installeri.

Kasutaja nägu rakenduse tõlked on eraldi JSON kataloogid all `src/Aivana.Core/Localization`. Dokumendi põlvkond ei muuda neid katalooge. Rakenduse ülesehitus ja selle dokumentatsioon peaksid jagama versioonilist pilti; hiljuti kirjeldatud katsetusaegne tugi võib eelistada installitud paketi.

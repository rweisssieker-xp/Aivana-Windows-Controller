<!-- generated-doc locale=hr-HR source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../MAINTENANCE.md)

# Održavanje svih jezika dokumentacije

Dokumentacija obuhvaća svih 27 jezika sučelja. Generirane mape koriste regionalne kodove; norveški odgovara `nb-NO`, a srpski na latinici `sr-Latn`. Svaka mapa sadrži kazalo, priručnik, detaljnu referencu radnji, pregled funkcija, bilješke o provjeri, istraživanja s datotekom CSV, povijesne planove i analizu licenci.

## Izvor istine

Uredite korijenski `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, ovu stranicu ili izvorne datoteke u `docs/research`, `docs/testing`, `docs/superpowers` i `docs/licensing`. Ne prepravljajte povijesno izvješće tako da sugerira provjeru kasnije verzije. Razlikujte implementaciju, simulaciju, prihvatno testiranje stvarnih uređaja i komercijalno odobrenje.

## Proizvoditi i validirati

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Neobvezni mrežni generator šalje tekst dokumentacije na Googleovo javno sučelje za prevođenje u skladu s postojećim alatima za lokalizaciju. Ne čita korisničku konfiguraciju, tajne ni datoteke modela. Pokrećite ga samo na dokumentaciji prikladnoj za tu uslugu. Blokovi koda, kod unutar retka, URL-ovi, struktura tablica i tehnički identifikatori su zaštićeni. Predmemorije prijevoda i zamrznuto stanje izvora pohranjuju se u zanemarenom `artifacts/documentation-translation`.

Kada mrežni pružatelj ograniči učestalost zahtjeva, `tools/translate_documentation_local.py` puni istu predmemoriju izričito dostavljenim lokalnim modelom M2M100 CTranslate2. Stanje od 2026-10-12 koristi model facebook/m2m100_418M s licencom MIT, pretvoren kao JustFrederik/m2m_100_418m_ct2_int8 u reviziji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 i `SentencePiece` 0.2.2 alati su samo za izgradnju. MAKRIVO ne isporučuje težine modela ni ovisnosti za prevođenje. Nakon punjenja predmemorije pokrenite generator s `--cache-only` za izradu stranica bez mrežnih zahtjeva. Lokalni pomoćnik ne preuzima niti izvršava udaljeni kod modela.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources` izvršavanje nastavlja svoje zamrznuto stanje izvora. Koristite osvježavanje nakon uređivanja izvornika. Predmemorirani segmenti ponovno se koriste. Djelomično izvršavanje može odabrati regionalne kodove pomoću `--locales`; dovršite sve jezike prije objavljivanja. Prekinuta izvršavanja ne zamjenjuju neuspjeli prijevod neprimjetno neprevedenim tekstom.

Manifest bilježi svaki izvorni i generirani dokument s otiscima SHA-256 normaliziranog teksta UTF-8: završeci redaka LF i bez oznake redoslijeda bajtova. Izvanmrežni provjerivač provjerava cjelovitu pokrivenost svih jezika, otiske, lokalne poveznice, primjere koda, strukturu tablica/CSV-a, izvorne URL-ove i nezamijenjena rezervirana mjesta. Odbija zastarjele izvore. Uspješna strukturna provjera nije pregled izvornog govornika ili pravno odobrenje. Generirane stranice vidljivo navode tu ogradu.

## Objavljivanje i paketi

Objavite dokumentaciju, generator i provjerivač zajedno. Sačuvajte izvorne uvjete licenci trećih strana; prevedena analiza licenci informativna je. Rezultati izgradnje, predmemorija prijevoda i lokalne datoteke za orkestraciju ne pripadaju u Git. Izdanje izvorne dokumentacije samo po sebi ne objavljuje instalacijski program Windows.

Prijevodi korisničkog sučelja aplikacije zasebni su katalozi JSON u `src/Aivana.Core/Localization`. Generiranje dokumentacije ne mijenja te kataloge. Izgradnja aplikacije i njezina dokumentacija trebaju dijeliti isto verzionirano stanje; novoopisana eksperimentalna podrška izvršnog okruženja može prethoditi instaliranom paketu.

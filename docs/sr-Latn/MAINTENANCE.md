<!-- generated-doc locale=sr-Latn source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../MAINTENANCE.md)

# Održavanje svih jezika dokumentacije

Dokumentacija obuhvata svih 27 jezika interfejsa. Generisane fascikle koriste regionalne kodove; norveški odgovara `nb-NO`, a srpski na latinici `sr-Latn`. Svaka fascikla sadrži indeks, priručnik, detaljnu referencu radnji, pregled funkcija, beleške o proveri, istraživanja sa datotekom CSV, istorijske planove i analizu licenci.

## Izvor istine

Uredite korenski `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, ovu stranicu ili izvorne datoteke u `docs/research`, `docs/testing`, `docs/superpowers` i `docs/licensing`. Ne prepravljajte istorijski izveštaj tako da sugeriše proveru kasnije verzije. Razlikujte implementaciju, simulaciju, prihvatno testiranje stvarnih uređaja i komercijalno odobrenje.

## Provodi i validiše

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Neobavezni mrežni generator šalje tekst dokumentacije na Google-ov javni interfejs za prevođenje u skladu sa postojećim alatima za lokalizaciju. Ne čita korisničku konfiguraciju, tajne ni datoteke modela. Pokrećite ga samo na dokumentaciji prikladnoj za tu uslugu. Blokovi koda, kod unutar reda, URL adrese, struktura tabela i tehnički identifikatori su zaštićeni. Keševi prevoda i zamrznuto stanje izvora čuvaju se u zanemarenom `artifacts/documentation-translation`.

Kada mrežni pružalac ograniči učestalost zahteva, `tools/translate_documentation_local.py` puni isti keš izričito dostavljenim lokalnim modelom M2M100 CTranslate2. Stanje od 2026-10-12 koristi model facebook/m2m100_418M sa licencom MIT, pretvoren kao JustFrederik/m2m_100_418m_ct2_int8 u reviziji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 i `SentencePiece` 0.2.2 su alati samo za izgradnju. MAKRIVO ne isporučuje težine modela ni zavisnosti za prevođenje. Nakon punjenja keša pokrenite generator sa `--cache-only` za pravljenje stranica bez mrežnih zahteva. Lokalni pomoćnik ne preuzima niti izvršava udaljeni kod modela.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources` izvršavanje nastavlja svoje zamrznuto stanje izvora. Koristite osvežavanje nakon uređivanja izvornika. Keširani segmenti se ponovo koriste. Delimično izvršavanje može da izabere regionalne kodove pomoću `--locales`; dovršite sve jezike pre objavljivanja. Prekinuta izvršavanja ne zamenjuju neuspeo prevod neprimetno neprevedenim tekstom.

Manifest beleži svaki izvorni i generisani dokument sa otiscima SHA-256 normalizovanog teksta UTF-8: završeci redova LF i bez oznake redosleda bajtova. Proverivač van mreže proverava potpunu pokrivenost svih jezika, otiske, lokalne veze, primere koda, strukturu tabela/CSV-a, izvorne URL adrese i nezamenjene oznake mesta. Odbija zastarele izvore. Uspešna strukturna provera nije pregled izvornog govornika ili pravno odobrenje. Generisane stranice vidljivo navode tu ogradu.

## Publikacije i paketi

Objavite dokumentaciju, generator i proverivač zajedno. Sačuvajte izvorne uslove licenci trećih strana; prevedena analiza licenci je informativna. Rezultati izgradnje, keš prevoda i lokalne datoteke za orkestraciju ne pripadaju u Git. Izdanje izvorne dokumentacije samo po sebi ne objavljuje instalacioni program Windows.

Prevodi korisničkog interfejsa aplikacije su zasebni katalozi JSON u `src/Aivana.Core/Localization`. Generisanje dokumentacije ne menja te kataloge. Izgradnja aplikacije i njena dokumentacija treba da dele isto verzionisano stanje; novoopisana eksperimentalna podrška izvršnog okruženja može da prethodi instaliranom paketu.

<!-- generated-doc locale=hr-HR source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../MAINTENANCE.md)

# Održavanje svih jezika dokumentacije

Dokumentarni popis slijedi sve 27 UI jezike. generirane dosjee koriste regionalne koda; Norveška zemlja za `nb-NO` Srpska latinski jezik `sr-Latn`. Svaki folder sadrži indeks, rukovodstvo, detaljnu referenciju za akciju, inventariju značajki, beleške za provjeru, istraživanje uključujući CSV, povijesne planove i analizu licencija.

## Izvor istine

Edit je korijen `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Ova stranica ili originalni datoteci ispod `docs/research`, `docs/testing`, `docs/superpowers` i `docs/licensing`. Nemojte ponovno pisati povijesni izvještaj koji podrazumijeva da potvrđuje kasniju verziju. zadržite implementaciju, simulaciju, prihvaćanje stvarnog uređaja i komercijalnu odobrenje odvojeno.

## Proizvoditi i validirati

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Opcionalni online generator šalje dokumentaciju na Googleovo krajnje točko prijevoda, u skladu s postojećim alatima za lokaliziranje. Ne čita korisničke konfiguracije, tajne ili datoteke modela. Samo ga provodite na dokumentaciji koja je prikladna za tu uslugu. Bloki koda, inline koda, URL-ovi, struktura tablice i tehnički identifikatori su zaštićeni. Prevođenje cache i zamrznuti izvor snapshot su pohranjeni pod ignoriranim `artifacts/documentation-translation`.

Kada je internetski pružatelj ograničen, `tools/translate_documentation_local.py` ispunjava istu cache pomoću izričito isporučenog M2M100 CTranslate2 Modeli – to je 2026-10-12 Snapshot koristi MIT licencu facebook/m2m100_418M Modeli, pretvoreni u JustFrederik/m2m_100_418m_ct2_int8 u reviziji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 i `SentencePiece` 0.2.2 Ne isporučuju se nijedna težina modela ili ovisnosti o prijevodu MAKRIVO. Provedite generator s `--cache-only` nakon ispunjavanja cache-a da se prenese bez mrežnih zahtjeva. lokalni pomoćnik ne preuzima ili izvršava kod daljinskog modela.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources`, trka ponovno zamrzava svoj snapshot. Koristite opciju osvježavanja nakon uređivanja originalnih. Preuzeti segmenti se ponovno koriste. Djelomično pokretanje može odabrati lokalne kode pomoću `--locales`; završiti sve lokalne prije objavljivanja. Prekinuti tečajevi ne zamjenjuju neprovedene prose za neuspješni prijevod.

Manifesto zapisuje svaki izvor i generirani datoteka s SHA-256 otisci prstiju normaliziranog UTF-8 teksta: LF linije završetak i bez znakova narudžbe. Offline provjerava cjelovitu pokrivenost po jeziku, otiske prstiju, lokalne poveznice, primjere koda, strukturu tablice/CSV-a, izvorne URL-ove i nepovratne lokacije. Odbijaju stalne izvore. Stručna provjera nije pregled rodnog govornika ili pravno odobrenje. Stranice koje su stvorene imaju ovu kvalifikaciju vidljivo.

## Objavljivanje i paketi

Objavljujte dokumentaciju, generator i provjernik zajedno. Održavajte originalne licencijske uvjete treće strane; prevedeno je licencijsko analizu. Izgradnja artefakata, cache prijevoda i lokalnih orkestra datoteke ne pripadaju Git. Izdanje izvora dokumentacije ne objavljuje sam Windows instalator.

Prevođenje aplikacija za korisnike je odvojeno JSON kataloga pod `src/Aivana.Core/Localization`. Dokumentarna generacija ne mijenja te kataloge. Izgradnja aplikacije i njezina dokumentacija trebaju podijeliti verziju snimke; novo opisana eksperimentalna podrška može biti prethodna instaliranom paketu.

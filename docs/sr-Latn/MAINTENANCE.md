<!-- generated-doc locale=sr-Latn source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../MAINTENANCE.md)

# Održavanje svih jezika dokumentacije

Dokumentacija slede sve 27 UI jezici. generisane fascikle koriste regionalne kodove; Norveška mapa `nb-NO` Latinski srpski u `sr-Latn`. Svaki fasciklus sadrži indeks, rukopis, detaljnu referencu akcije, inventar karakteristika, verifikacione beleške, istraživanje uključujući CSV, istorijske planove i analizu licence.

## Izvor istine

Redaktirajte koren `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Ova stranica, ili originalne datoteke ispod `docs/research`, `docs/testing`, `docs/superpowers` i `docs/licensing`. Nemojte ponovo pisati istorijski izveštaj da bi to potvrdilo kasnije verzije. održavanje implementacije, simulacije, prihvatanja stvarnog uređaja i komercijalnog odobrenja.

## Provodi i validiše

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Opcionalni online generator šalje prozu dokumentacije na javni prevod Google-a, u skladu sa postojećim alatom za lokalizaciju. Ne čita korisničke konfiguracije, tajne ili datoteke modela. Ispostavite je samo na dokumentaciji koja je pogodna za tu uslugu. Blokovi koda, uline kod, URL-a, struktura tabela i tehnički identifikatori su zaštićeni. Prevodilački keši i zamrznuti izvor snimak se čuvaju pod ignorisanim `artifacts/documentation-translation`.

Kada je onlajn provajder ograničen, `tools/translate_documentation_local.py` popunjava isti keš koristeći eksplicitno isporučenu lokalnu M2M100 CTranslate2 Model je 2026-10-12 Snapshot koristi MIT licenciran facebook/m2m100_418M Model, konvertovani kao JustFrederik/m2m_100_418m_ct2_int8 u reviziji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 i `SentencePiece` 0.2.2 nisu izgrađeni samo alati. nema modela težine ili prevođenja zavisnosti se isporučuju sa MAKRIVO. Uključite generator `--cache-only` nakon popunjavanja keše da se isporučuje bez mrežnih zahteva. lokalni pomoćnik ne preuzima ili izvršava kod udaljenog modela.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources`, trka ponovo zamrzava svoj sjajni snimak. Koristite opciju osvežavanja nakon uređivanja originala. Ponovo se koriste skriveni segmenti. Delimično pokretanje može izabrati lokalne kodove koristeći `--locales`; završiti sve lokalne pre objavljivanja. Prekinute trke ne zamenjuju tiho neprovedene proze za neuspešan prevod.

Manifest zapisi svakog izvora i generisanih datoteka sa SHA-256 Pritisak prstiju normalizovan UTF-8 tekst: LF linija završetka i nema bita-poruka. offline pregledač proverava kompletnu po jeziku pokrivenu, prstene, lokalne veze, primere koda, tabele / CSV strukture, izvornih URL-a i nezamenjenih lokalnih drveća. odbacuje izvore. Prolazak strukturnog pregleda nije pregled rodnog govornika ili pravna odobrenja..

## Publikacije i paketi

Objavite dokumentaciju, generator i čeker zajedno. Sačuvajte originalne uslove licence treće strane; prevedena analiza licence je informativna. Izgradnja artefakata, prevod keš i lokalne orkestracije datoteke nisu u Git. Izvor-dokumentacija izdanja sama ne objavljuje Windows instalator.

Korisnički aplikacijski prevodi su odvojeni JSON katalozi pod `src/Aivana.Core/Localization`. Dokumentacija generacija ne menja ove kataloge. Aplikacija izgrađuje i njegova dokumentacija treba da deli verzionisani snimak; nedavno opisana eksperimentalna podrška može prethoditi instaliranom paketu.

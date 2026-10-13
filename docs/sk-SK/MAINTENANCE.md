<!-- generated-doc locale=sk-SK source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../MAINTENANCE.md)

# Udržovanie všetkých jazykov dokumentácie

Súbor dokumentácie pokrýva všetkých 27 jazykov rozhrania. Generované priečinky používajú regionálne kódy; nórčine zodpovedá `nb-NO` a srbčine v latinke `sr-Latn`. Každý priečinok obsahuje index, príručku, podrobnú referenciu akcií, súpis funkcií, poznámky k overeniu, výskum vrátane súboru CSV, historické plány a analýzu licencií.

## Zdroj pravdy

Upravujte koreňový `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, túto stránku alebo pôvodné súbory v `docs/research`, `docs/testing`, `docs/superpowers` a `docs/licensing`. Neprepisujte historickú správu tak, aby naznačovala overenie neskoršej verzie. Rozlišujte implementáciu, simuláciu, akceptáciu skutočného zariadenia a komerčné schválenie.

## Vytvorte a validujte

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Voliteľný online generátor odosiela text dokumentácie na verejný prekladový endpoint Google v súlade s existujúcimi lokalizačnými nástrojmi. Nečíta používateľskú konfiguráciu, tajné údaje ani súbory modelov. Spúšťajte ho iba pre dokumentáciu vhodnú pre túto službu. Bloky kódu, kód v texte, URL, štruktúra tabuliek a technické identifikátory sú chránené. Prekladová cache a zmrazená snímka zdrojov sa ukladajú do ignorovaného adresára `artifacts/documentation-translation`.

Keď online poskytovateľ obmedzuje počet požiadaviek, `tools/translate_documentation_local.py` naplní rovnakú cache pomocou výslovne dodaného lokálneho modelu M2M100 CTranslate2. Snímka z 2026-10-12 používa model facebook/m2m100_418M s licenciou MIT, prevedený ako JustFrederik/m2m_100_418m_ct2_int8 v revízii `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 a `SentencePiece` 0.2.2 sú nástroje iba na zostavenie. S MAKRIVO sa nedodávajú váhy modelov ani prekladové závislosti. Po naplnení cache spustite generátor s `--cache-only` na vykreslenie bez sieťových požiadaviek. Lokálny pomocník nesťahuje ani nespúšťa vzdialený kód modelov.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources` beh pokračuje so zmrazenou snímkou. Po úprave originálov použite možnosť obnovenia. Segmenty z cache sa znova používajú. Čiastočný beh môže vybrať kódy jazykov pomocou `--locales`; pred publikovaním dokončite všetky jazyky. Prerušené behy nenahrádzajú neúspešný preklad potichu nepreloženým textom.

Manifest zaznamenáva všetky zdrojové a generované súbory s odtlačkami SHA-256 normalizovaného textu UTF-8: konce riadkov LF a žiadna značka poradia bajtov. Offline kontrola overuje úplné pokrytie každého jazyka, odtlačky, miestne odkazy, príklady kódu, štruktúru tabuliek/CSV, zdrojové URL a nenahradené zástupné symboly. Odmieta zastarané zdroje. Úspešná štrukturálna kontrola nie je revíziou rodeného hovoriaceho ani právnym schválením. Generované stránky toto obmedzenie viditeľne uvádzajú.

## Publikácia a balíky

Publikujte dokumentáciu, generátor a kontrolný nástroj spoločne. Zachovajte pôvodné licenčné podmienky tretích strán; preložená analýza licencií je informatívna. Artefakty zostavenia, prekladová cache a miestne orchestračné súbory nepatria do Git. Vydanie zdrojovej dokumentácie samo osebe nepublikuje inštalačný program Windows.

Preklady rozhrania aplikácie sú samostatné katalógy JSON v `src/Aivana.Core/Localization`. Generovanie dokumentácie tieto katalógy nemení. Zostavenie aplikácie a jeho dokumentácia by mali zdieľať verziovanú snímku; novo opísaná experimentálna podpora runtime prostredia môže predchádzať nainštalovanému balíčku.

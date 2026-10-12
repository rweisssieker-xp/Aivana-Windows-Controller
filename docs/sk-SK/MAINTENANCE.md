<!-- generated-doc locale=sk-SK source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../MAINTENANCE.md)

# Udržovanie všetkých jazykov dokumentácie

Dokumentácia obsahuje všetky 27 UI jazyky. generované priečinky používajú regionálne kódy; Norské mapy na `nb-NO` a latinsky pre `sr-Latn`Každý priečinok obsahuje index, manuál, podrobné referencie akcie, inventár funkcií, overovací poznámky, výskum vrátane CSV, historické plány a analýzu licencií.

## Zdroj pravdy

Editovať koreň `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, táto stránka, alebo pôvodné súbory pod `docs/research`, `docs/testing`, `docs/superpowers` a `docs/licensing`Neprepíšte historickú správu, ktorá znamená, že potvrdzuje neskoršiu verziu. udržať implementáciu, simuláciu, skutočné zariadenie a komerčné schválenie odlišné.

## Vytvorte a validujte

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Voliteľný online generátor odošle dokumentáciu do verejného prekladu Googlu, podľa existujúceho nástroja na lokalizáciu. Nečítá používateľskú konfiguráciu, tajomstvá alebo modelové súbory. Len ju spustite na dokumentácii vhodnej pre túto službu. Kódové bloky, inline kód, URL, tabuľková štruktúra a technické identifikátory sú chránené. Prekladové cache a zmrazený zdrojový snapshot sú uložené pod ignorovaným `artifacts/documentation-translation`.

Keď je online poskytovateľ limitovaný, `tools/translate_documentation_local.py` vyplní rovnakú cache pomocou explicitne dodaného lokálneho M2M100 CTranslate2 Modelka - The 2026-10-12 Snapshot používa MIT licencované facebook/m2m100_418M Model, ktorý sa konvertoval ako JustFrederik/m2m_100_418m_ct2_int8 Pri revízii `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 a `SentencePiece` 0.2.2 sú postavené len nástroje. žiadne hmotnosti modelu alebo závislosť na preklade nie sú odoslané s MAKRIVO. Spustiť generátor s `--cache-only` po vyplnení cache bez požiadaviek na sieť. miestny pomocník nedodáva alebo neuskutoční vzdialený modelový kód.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources`, spustenie obnovuje zmrazené snapshot. Použite možnosť obnovenia po úprave originálov. skryté segmenty sú opätovne použité. `--locales`- ukončenie všetkých lokalít pred publikáciou. prerušené preteky ticho nahrádza nepreložené prose pre neúspešný preklad.

Manifest zaznamenáva každý zdroj a generovaný súbor s SHA-256 odtlačkov normalizovaného UTF-8 textu: LF line ends a žiadny byte-order mark. Offline kontrolér overuje úplné jazykové pokrytie, odtlačky prstov, lokálne odkazy, kódy príklady, tabuľky/CSV štruktúra, zdrojové URL a nenahradené umiestnenie. Odmieta sa oceľových zdrojov. Prechod štrukturálnej kontroly nie je domáci prehľad alebo právne schválenie. Generované stránky majú túto kvalifikáciu viditeľne.

## Publikácia a balíky

Publikovať dokumentáciu, generátor a kontrolér spolu. Zachovať pôvodné licenčné podmienky tretích strán; preklad licenčnej analýzy je informatívny. Build artefakty, preklad cache a lokálne orchestrácie súbory nepatria do Git. Windows Inštalácia →

Preklady aplikácií sú oddelené katalógy JSON pod `src/Aivana.Core/Localization`Dokumentácia generácia nemení tieto katalógy. aplikácia budovanie a jeho dokumentácia by mali zdieľať verziu snapshot; novopísaná experimentálna prevádzková podpora môže predchádzať inštalovaným balíkom.

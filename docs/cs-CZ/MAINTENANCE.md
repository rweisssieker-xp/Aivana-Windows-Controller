<!-- generated-doc locale=cs-CZ source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../MAINTENANCE.md)

# Udržování všech jazyků dokumentace

Soubor dokumentace pokrývá všech 27 jazyků rozhraní. Generované složky používají regionální kódy; norštině odpovídá `nb-NO` a srbštině v latince `sr-Latn`. Každá složka obsahuje index, příručku, podrobnou referenci akcí, soupis funkcí, poznámky k ověření, výzkum včetně souboru CSV, historické plány a analýzu licencí.

## Zdroj pravdy

Upravujte kořenový `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, tuto stránku nebo původní soubory v `docs/research`, `docs/testing`, `docs/superpowers` a `docs/licensing`. Nepřepisujte historickou zprávu tak, aby naznačovala ověření pozdější verze. Rozlišujte implementaci, simulaci, přejímku skutečného zařízení a komerční schválení.

## Vytvořit a ověřit

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Volitelný online generátor odesílá text dokumentace na veřejný překladový endpoint Google v souladu se stávajícími lokalizačními nástroji. Nečte uživatelskou konfiguraci, tajné údaje ani soubory modelů. Spouštějte jej pouze pro dokumentaci vhodnou pro tuto službu. Bloky kódu, kód v textu, URL, struktura tabulek a technické identifikátory jsou chráněny. Překladová cache a zmrazený snímek zdrojů se ukládají do ignorovaného adresáře `artifacts/documentation-translation`.

Když online poskytovatel omezuje počet požadavků, `tools/translate_documentation_local.py` naplní stejnou cache pomocí výslovně dodaného místního modelu M2M100 CTranslate2. Snímek z 2026-10-12 používá model facebook/m2m100_418M s licencí MIT, převedený jako JustFrederik/m2m_100_418m_ct2_int8 v revizi `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 a `SentencePiece` 0.2.2 jsou nástroje pouze pro sestavení. S MAKRIVO se nedodávají váhy modelů ani překladové závislosti. Po naplnění cache spusťte generátor s `--cache-only` pro vykreslení bez síťových požadavků. Místní pomocník nestahuje ani nespouští vzdálený kód modelů.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources` běh pokračuje se zmrazeným snímkem. Po úpravě originálů použijte možnost obnovení. Segmenty z cache se znovu používají. Částečný běh může vybrat kódy jazyků pomocí `--locales`; před publikováním dokončete všechny jazyky. Přerušené běhy nenahrazují neúspěšný překlad potichu nepřeloženým textem.

Manifest zaznamenává všechny zdrojové a generované soubory s otisky SHA-256 normalizovaného textu UTF-8: konce řádků LF a žádná značka pořadí bajtů. Offline kontrola ověřuje úplné pokrytí každého jazyka, otisky, místní odkazy, příklady kódu, strukturu tabulek/CSV, zdrojové URL a nenahrazené zástupné symboly. Odmítá zastaralé zdroje. Úspěšná strukturální kontrola není revizí rodilého mluvčího ani právním schválením. Generované stránky toto omezení viditelně uvádějí.

## Publikace a balíčky

Publikujte dokumentaci, generátor a kontrolní nástroj společně. Zachovejte původní licenční podmínky třetích stran; přeložená analýza licencí je informativní. Artefakty sestavení, překladová cache a místní orchestrační soubory nepatří do Git. Vydání zdrojové dokumentace samo o sobě nepublikuje instalační program Windows.

Překlady rozhraní aplikace jsou samostatné katalogy JSON v `src/Aivana.Core/Localization`. Generování dokumentace tyto katalogy nemění. Sestavení aplikace a jeho dokumentace by měly sdílet verzovaný snímek; nově popsaná experimentální podpora běhového prostředí může předcházet nainstalovanému balíčku.

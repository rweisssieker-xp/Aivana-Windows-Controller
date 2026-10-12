<!-- generated-doc locale=cs-CZ source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../MAINTENANCE.md)

# Udržování všech jazyků dokumentace

Dokumentární záznam sleduje všechny 27 UI jazyky. generované složky používají regionální kódy; Norské mapy `nb-NO` a latinské slovo `sr-Latn`Každý složka obsahuje index, příručku, podrobné reference k akci, inventář funkcí, poznámky k ověření, výzkum včetně CSV, historické plány a analýzu licencí.

## Zdroj pravdy

Editovat kořen `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, tato stránka, nebo původní soubory pod `docs/research`, `docs/testing`, `docs/superpowers` a `docs/licensing`Nepřepisujte historickou zprávu, která znamená, že potvrzuje pozdější verzi. Udržujte implementaci, simulace, přijetí reálného zařízení a komerční schválení odlišné.

## Vytvořit a ověřit

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Volitelný on-line generátor odesílá dokumentaci do veřejného překladu Googlu, následuje stávající lokalizace nástroj. Nečíst uživatelskou konfiguraci, tajemství nebo vzorové soubory. Pouze to spustit na dokumentaci vhodnou pro tuto službu. Kódové bloky, kódové odkazy, URL, tabulkové struktury a technické identifikátory jsou chráněny. `artifacts/documentation-translation`.

Pokud je internetový poskytovatel omezený, `tools/translate_documentation_local.py` vyplní stejnou cache pomocí výslovně dodaného lokálního M2M100 CTranslate2 Modelka - The 2026-10-12 Snapshot používá MIT-licenční facebook/m2m100_418M Model, převedený jako JustFrederik/m2m_100_418m_ct2_int8 V revizi `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 a `SentencePiece` 0.2.2 jsou stavební pouze nástroje. žádné hmotnosti modelu nebo závislosti na překladu jsou odeslány s MAKRIVO. Provoz generátoru s `--cache-only` po vyplnění cache k předání bez požadavků na síť. místní pomocník nedodává nebo neprovádí kód vzdialeného modelu.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources`, běh znovu zmrazený snapshot. Použijte možnost obnovení po úpravě originálů. skryté segmenty jsou znovu použity. `--locales`Přerušené běhy tiše nenahradí neprokládanou prose za neúspěšný překlad.

Zobrazit všechny zdroje a generované soubory SHA-256 Odtisk prstů normalizovaný UTF-8 text: LF linie konce a žádný byte-objednávky. Offline ověřuje kompletní jazykové pokrytí, otisky prstů, místní odkazy, kódy příklady, tabulky / CSV struktury, zdrojové URL a nenahradené držitele místa. Odmítá stálé zdroje..

## Publikace a balíčky

Publikujte dokumentaci, generátor a kontrolor spolu. Zachovávejte původní licenční podmínky třetích stran; překladová licence analýza je informativní. Build artefakty, překlad cache a lokální orchestrální soubory nepatří do Git. Windows nainstalovat

Překlady aplikací jsou oddělené katalogy JSON `src/Aivana.Core/Localization`Dokumentace generace nemění tyto katalogy. aplikace stavět a její dokumentace by měla sdílet verzí snapshot; nově popsané experimentální provozní doba podpory může předcházet instalované balíček.

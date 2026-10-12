<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-12-local-meeting-assistant.md)

# Místní asistent setkání AI – prototyp

Stav: 12. října 2026. ForgeMind Outcome Agent Hypothesis, Bounded Meeting Flow. Žádná prokázaná konkurenční výhoda a žádná naměřená úspora času bez skutečných srovnávacích testů.

## Využití

1. Paleta příkazů → **KI Asistent schůzky**. Experiment je ve výchozím nastavení vypnutý a resetuje se během importu konfigurace nebo exportu z přenosného počítače.
2. Povolit místního asistenta. Ollama musí běžet samostatně na `127.0.0.1:11434`; musí být již nainstalován vhodný místní model. **Načtěte místní modely** a vyberte model. MAKRIVO neinstaluje modely/služby, nekontaktuje OpenAI/Cloud API ani nepoužívá klíče API. Offline konfigurace a důvěryhodnost místní služby Ollama spočívá na uživateli; deklarované vzdálené/cloudové modely jsou odmítnuty.
3. Zadejte cíl a explicitně přidejte programy/dokumenty/webové stránky. Cíl, ID zdroje, název a typ jsou přenášeny; Cílové cesty a obsah vybraných souborů nejsou dodatečně přenášeny; samotný záznam cíle je odeslán doslovně. Štítky webových stránek zahrnují pouze schéma/hostitel, úplný odkaz zůstává místní.
4. **KI vytvořit návrh**. Maximum 90 sekund, výstupní tokeny 1024, omezená velikost odezvy. Náhled změn cíle/modelu/zdroje; Storno/Zavřít ukončí požadavek. Model může navrhovat pouze vybraná ID. Neznámá ID, volné příkazy, další pole JSON a typy spustitelných dokumentů jsou odmítnuty.
5. Zkontrolujte náhled a otevřete body → **Zkontrolujte v editoru**. Výsledkem je zpočátku neuložený koncept. Nejprve uložte ve stávajícím editoru a v případě potřeby jej spusťte explicitně. Normální ověření maker, kontrola před/po, pozastavení a přerušení zůstávají zodpovědné.

Podporováno: Spouštění programů bez argumentů, otevírání explicitně vybraných místních PDF/DOCX/XLSX/PPTX/TXT/MD, otevírání webových stránek HTTP(S), ztlumení mikrofonu na výslovnou žádost. Žádná změna výchozího nastavení náhlavní soupravy, žádné zprávy, schůzky, stahování, skripty, bezplatná kliknutí nebo zadávání textu. Přání, která nelze znázornit, se zobrazí jako otevřené body, pokud je model správně rozpozná; Uživatel musí zkontrolovat, zda byly splněny technické cíle. Otevření dokumentu není automaticky technicky ověřeno.

## Srovnávací experiment

**Měření ručního nastavení · Start / End** měří kompletní ruční porovnávací tok. Předpoklady průvodce měří čas od otevření průvodce do prvního uložení v editoru; Na druhé straně `generated` obsahuje pouze dobu plánování. Neporovnávejte dvě nestejné doby trvání jako úsporu času.

Místní události jsou v konfigurační složce pod `Experiments/meeting-experiment.jsonl`: časové razítko, náhodný pokus/ID dlaždice, událost, trvání, oprava ano/ne. Žádné cílové texty, názvy zdrojů, cesty, modelové odpovědi nebo obsah dokumentu. Soubor se otočí z 2 MiB, předchozí soubor zůstane zachován. Chyby měření nesmí zrušit makra a ukládání konfigurace.

Zachycuje: ruční základní linii, generování modelu/chyby/zrušení, zahození změn/zavření náhledu, počáteční uložení a opravy v editoru a skutečné koncové stavy dlaždic z nich uložených. `run_completed` pouze znamená, že běžec dokončil; Potvrzené cílové účinky jsou v procesu testování uvedeny samostatně. Opakování stejného makra nejsou nové nezávislé pokusy o nastavení. Počáteční experiment může po restartu pokračovat, protože ID experimentu zůstává na uložené dlaždici. Duplicitní dlaždice sdílejí tento původ; skupina podle ID pokusu při hodnocení.

První experiment: 20 srovnatelné zasedací prostory nebo dva týdny s dobrovolnými testovacími uživateli; zpočátku vnitřní použití. Porovnejte čas manuálního nastavení s časem plně asistovaného nastavení; Dále vyhodnoťte míru korekcí/odmítnutí, chyby běžců a skutečné výsledky testů. Pokud nedojde ke zlepšení nebo jsou překročeny ochranné limity, deaktivujte spínač. V úložišti nebylo prokázáno žádné používání ani ochota platit. Žádný automatický telemetrický přenos nebo úprava ceny.

## Zkušební stolice

- Jádro: Limity, typy, neznámá/duplicitní ID, vyloučení skriptů, dlouhé odkazy, volitelná starší konfigurace, přihlášení k importu/exportu, měření šetřící data a jednorázové vyřazení a správné potvrzení.
- Přenos s falešným modelem: schéma Ollama JSON, cíl zpětné smyčky, žádné cesty k souboru ve výzvě, název cloudu a vzdálený alias zamítnuty, poškozená/příliš dlouhá odpověď odmítnuta, přerušeno.
- WPF-Smoke: Vykreslený dialog; Odeslání konceptu nemění uložené dlaždice, kontrola/uložení zachovává původ a zaznamenává opravu, zahození nic neuloží.
- Nezávislá recenze: Vyřazený náhled a nalezené a opravené dlouhé odkazy; Regresní testování ČERVENÁ→ZELENÁ.
- Sestavení vydání s nulovými varováními/chybami, celou základní sadou a kouřem WPF úspěšné. Zpočátku existovala kompletní sada Windows včetně změn zaměření. Další běh se opět nezdařil, protože změna popředí byla odmítnuta; test je závislý na kontextu a zůstává nezměněn. Zbývající testy Windows včetně průchodu modelu transportem s explicitně vynechaným testem úspěšnosti v popředí.
- Kontrola balíčků odhalila existující chybu v atomové výměně krátce otevřených konfiguračních souborů. Zámek čtení reprodukoval stejnou chybu odepření přístupu; omezené opakování pouze atomové výměny je zaškrtnuto ČERVENÁ→ZELENÁ. Chyby trvalého přístupu zůstávají viditelné.
- Aktuální samostatný balíček `artifacts/release-20261012-184105`: Kouření WPF bylo úspěšné po opravě paměti, včetně dialogu AI a přijetí/zamítnutí konceptu.
- Nejedná se o skutečný test modelu: místní služba Ollama při testování nereagovala. Žádná prohlášení o kvalitě modelu, latenci, sémantické úplnosti nebo úspoře času. Omezení hardwaru/ovladače/škálování předchozího vydání zůstávají zachována.

Zdroje protokolů: [Ollama Chat](https://docs.ollama.com/api/chat), [Strukturované výstupy](https://docs.ollama.com/capabilities/structured-outputs), [Oficiální typové definice včetně dat o vzdáleném modelu](https://github.com/ollama/ollama/blob/main/api/types.go).

<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-12-local-meeting-assistant.md)

# Miestny asistent stretnutia AI – prototyp

Stav: 12. októbra 2026. ForgeMind Outcome Agent Hypothesis, Bounded Meeting Flow. Žiadna preukázaná konkurenčná výhoda a žiadna nameraná úspora času bez skutočných porovnávacích testov.

## Použitie

1. Paleta príkazov → **KI Asistent stretnutia**. Experiment je predvolene vypnutý a počas importu konfigurácie alebo prenosného exportu sa resetuje.
2. Povoliť miestneho asistenta. Ollama musí bežať samostatne na `127.0.0.1:11434`; už musí byť nainštalovaný vhodný lokálny model. **Načítať miestne modely** a vybrať model. MAKRIVO neinštaluje modely/služby, nekontaktuje OpenAI/Cloud API ani nepoužíva API kľúče. Offline konfigurácia a dôveryhodnosť lokálnej služby Ollama závisí od používateľa; deklarované vzdialené/cloudové modely sú odmietnuté.
3. Zadajte cieľ a explicitne pridajte programy/dokumenty/webové stránky. Prenáša sa cieľ, ID zdroja, názov a typ; Cieľové cesty a obsah vybraných súborov sa dodatočne neprenášajú; samotné zadanie cieľa sa odošle doslovne. Štítky webových stránok zahŕňajú iba schému/hostiteľ, úplný odkaz zostáva lokálny.
4. **KI vytvoriť návrh**. Maximálny počet sekúnd 90, výstupné tokeny 1024, obmedzená veľkosť odozvy. Ukážka zmeny cieľa/modelu/zdroja; Zrušiť/Zatvoriť ukončí požiadavku. Model môže navrhovať iba vybrané ID. Neznáme ID, voľné príkazy, dodatočné polia JSON a typy spustiteľných dokumentov sú odmietnuté.
5. Skontrolujte náhľad a otvorte body → **Skontrolujte v editore**. Výsledkom je spočiatku neuložený koncept. Najprv uložte v existujúcom editore a v prípade potreby ho explicitne spustite. Normálne overenie makra, kontrola pred/po, pozastavenie a prerušenie zostávajú zodpovedné.

Podporované: Spúšťanie programov bez argumentov, otváranie explicitne vybraných lokálnych PDF/DOCX/XLSX/PPTX/TXT/MD, otváranie webových stránok HTTP(S), stlmenie mikrofónu na explicitnú žiadosť. Žiadna zmena predvoleného nastavenia náhlavnej súpravy, žiadne správy, stretnutia, sťahovanie, skripty, bezplatné kliknutia alebo zadávanie textu. Priania, ktoré nemožno znázorniť, sa zobrazia ako otvorené body, ak ich model správne rozpozná; Používateľ musí skontrolovať, či boli splnené technické ciele. Otvorenie dokumentu nie je automaticky technicky overené.

## Porovnávací experiment

**Manuálne nastavenie merania · Začiatok / Koniec** meria kompletný manuálny porovnávací tok. Predpoklady sprievodcu merajú čas od otvorenia sprievodcu po prvé uloženie v editore; Na druhej strane `generated` obsahuje iba trvanie plánovania. Neporovnávajte dve nerovnaké doby trvania ako šetrič času.

Lokálne udalosti sú v konfiguračnom priečinku pod `Experiments/meeting-experiment.jsonl`: časová pečiatka, náhodný pokus/ID dlaždice, udalosť, trvanie, oprava áno/nie. Žiadne cieľové texty, názvy zdrojov, cesty, modelové odpovede alebo obsah dokumentu. Súbor sa otáča z 2 MiB, predchádzajúci súbor sa zachová. Chyby merania nesmú prerušiť makrá a ukladanie konfigurácie.

Zaznamenáva: manuálnu základnú čiaru, generovanie modelu/chyby/prerušenie, zahodenie zmien/zatvorení náhľadu, počiatočné uloženia a opravy v editore a skutočné koncové stavy dlaždíc, ktoré sú z nich uložené. `run_completed` len znamená, že bežec dokončil; Potvrdené cieľové účinky sú uvedené samostatne v procese testovania. Opakovania toho istého makra nie sú nové nezávislé pokusy o nastavenie. Počiatočný experiment môže po reštarte pokračovať, pretože ID experimentu zostáva na uloženej dlaždici. Duplicitné dlaždice zdieľajú tento pôvod; skupina podľa ID pokusu pri hodnotení.

Prvý experiment: 20 porovnateľné konferenčné priestory alebo dva týždne s dobrovoľnými testovacími používateľmi; spočiatku vnútorné použitie. Porovnajte čas manuálneho nastavenia s časom úplne asistovaného nastavenia; Dodatočne vyhodnoťte mieru korekcie/odmietnutia, chyby bežca a skutočné výsledky testov. Ak nedôjde k zlepšeniu alebo ak sú porušené ochranné limity, deaktivujte spínač. V úložisku nebolo preukázané žiadne používanie ani ochota platiť. Žiadna automatická telemetrická prevodovka ani úprava ceny.

## Skúšobná lavica

- Jadro: Limity, typy, neznáme/duplicitné ID, vylúčenie skriptov, dlhé odkazy, voliteľná staršia konfigurácia, prihlásenie na import/export, meranie šetriace dáta a jednorazové vyradenie a správne odovzdanie.
- Prenos s falošným modelom: schéma Ollama JSON, cieľ spätnej slučky, žiadne cesty k súborom v riadku, názov cloudu a vzdialený alias odmietnutý, poškodená/predĺžená odpoveď odmietnutá, prerušenie.
- WPF-Smoke: Vykreslený dialóg; Odoslanie konceptu nemení uložené dlaždice, kontrola/uloženie zachováva pôvod a zaznamenáva opravu, zahodenie neuloží nič.
- Nezávislá recenzia: Zahodená ukážka a nájdené a opravené dlhé odkazy; Regresné testovanie ČERVENÁ→ZELENÁ.
- Vydanie zostavy s nulovými upozorneniami/chybami, celá základná sada a dym WPF bol úspešný. Pôvodne existoval kompletný balík Windows vrátane zmien zamerania. Ďalší chod opäť zlyhal, pretože zmena popredia bola odmietnutá; test je závislý od kontextu a zostáva nezmenený. Zostávajúce testy Windows vrátane úspešnej prepravy modelu s explicitne vynechaným testom úspešnosti v popredí.
- Kontrola balíkov odhalila existujúcu chybu v atómovej výmene krátko otvorených konfiguračných súborov. Zámok čítania reprodukoval rovnakú chybu odmietnutia prístupu; obmedzené opakovanie iba atómovej výmeny je zaškrtnuté ČERVENÁ→ZELENÁ. Trvalé chyby prístupu zostávajú viditeľné.
- Aktuálny samostatný balík `artifacts/release-20261012-184105`: Úspešný dym WPF po korekcii pamäte, vrátane dialógu AI a prijatia/odmietnutia konceptu.
- Nie je to skutočný test modelu: miestna služba Ollama pri testovaní nereagovala. Žiadne vyhlásenia o kvalite modelu, latencii, sémantickej úplnosti alebo úspore času. Hardvér/ovládač/obmedzenia škálovania z predchádzajúceho vydania zostávajú.

Zdroje protokolov: [Ollama Čítať](https://docs.ollama.com/api/chat), [Štruktúrované výstupy](https://docs.ollama.com/capabilities/structured-outputs), [Oficiálne definície typu vrátane dát o vzdialených modeloch](https://github.com/ollama/ollama/blob/main/api/types.go).

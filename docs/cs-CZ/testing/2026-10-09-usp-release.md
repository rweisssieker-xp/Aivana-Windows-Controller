<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-09-usp-release.md)

# Pomoc s klávesnicí, překrytí a zpětná vazba při provádění

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementována tři prioritní vylepšení ve stávající aplikaci Windows:

- Průvodce speciálními klíči: aktuální inventář zařízení/klíčů s podporou iCUE, dočasné zachycení klíče G, ověření úspěšnosti konfigurace, ověření konfliktu a neuložený návrh akce. Zachyťte vrácení zpět, zneplatnění zpětného volání ve frontě a chyby krytu obnovení normální vazby a uzavření dialogu.
- Překrytí klávesnice: fyzická referenční geometrie, vazby aktivního profilu, aktualizace živého profilu/úprav a běžné zvýraznění kláves Windows. Podržte Ctrl+Alt+F1 nebo procházejte stránku palety/klávesnice. Neaktivační, proklikávací a chybí na hlavním panelu.
- Zpětná vazba provedení: zadané fáze Čekání/Spuštění/Krok/Dokončeno/Zrušeno/Neúspěšné/Zamítnuté, postup kroků/opakování a neaktivační přípitek. Zpětná vazba nezahrnuje obsah akcí a zprávy o výjimkách. Zrušení posledního kroku nemůže ohlásit dokončení. Dokončení popisuje provedení, nikoli nezávislý stavový dotaz OBS.

Ověření:

- 24 Prošly základní kontroly, včetně řazení fází, výsledků selhání/zrušení, zrušení posledního kroku, izolace výjimek posluchače a vyloučení hodnot soukromých akcí.
- Sestavení řešení vydání proběhlo bez varování/chyb.
- Windows kontroly prošly; připojené K100 hlásilo G1–G6 a nativní konfigurace/uvolnění bylo úspěšné. Zvukové koncové body byly zkontrolovány beze změny jejich stavu.
- WPF kouř prošel se všemi rozvrženími referenčních překryvných vrstev, stálostí průvodce/katalogu/konfliktů/návrhu, konfigurací živého nativního zachycení, přesměrováním zpětného volání, odmítnutím zastaralého zpětného volání a obnovením pozastavených vazeb. Byl simulován nativní vstup zpětného volání; fyzické lidské stisknutí G-key zůstává ruční kontrolou.
- Pro provedení přípitku byla zkontrolována skutečná invariance okna popředí. Byly zkontrolovány vlastnosti interakce překrytí a zobrazení/skrytí; byly vykresleny snímky obrazovky pro průvodce, překryvnou vrstvu a přípitek.
- Jazyky 27 obsahují všechny kanonické řetězce 819; ověření offline zástupného symbolu/klíče prošlo.

Snímky a úplný výsledek GUI jsou v artefaktech/usp-win-x64/smoke. Testy používají dočasné profily; produkční nastavení jsou zálohována a kontrolována hash během nasazení.

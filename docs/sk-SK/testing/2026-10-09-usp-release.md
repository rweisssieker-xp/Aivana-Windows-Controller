<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-09-usp-release.md)

# Pomoc s klávesnicou, spätná väzba prekrytia a vykonania

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementované tri prioritné vylepšenia v existujúcej aplikácii Windows:

- Sprievodca špeciálnym kľúčom: aktuálny inventár zariadení/kľúčov s podporou iCUE, dočasné zachytenie kľúča G, overenie úspechu konfigurácie, overenie konfliktu a neuložený návrh akcie. Zaznamenajte vrátenie zmien, zrušenie spätného volania vo fronte a chyby krytu obnovenia normálnej väzby a zatvorenie dialógového okna.
- Prekrytie klávesnice: fyzická referenčná geometria, aktívne profilové väzby, živé obnovenie profilu/úpravy a bežné zvýraznenie klávesov Windows. Podržte Ctrl+Alt+F1 alebo prepínajte medzi stránkou palety/klávesnice. Neaktivuje sa, preklikáva sa a chýba na paneli úloh.
- Spätná väzba vykonania: zadané fázy Čakanie/Spustenie/Krok/Dokončené/Zrušené/Neúspešné/Odmietnuté, postup krokov/opakovaní a neaktivačný prípitok. Spätná väzba nezahŕňa obsah akcií a správy o výnimkách. Zrušenie posledného kroku nemôže nahlásiť dokončenie. Dokončenie popisuje vykonanie, nie nezávislý dotaz na stav OBS.

Overenie:

- 24 Úspešné kontroly jadra, vrátane poradia fáz, výsledkov zlyhania/zrušenia, zrušenia posledného kroku, izolácie výnimiek poslucháča a vylúčenia hodnôt súkromnej akcie.
- Zostavenie riešenia vydania prebehlo bez varovaní/chýb.
- Windows kontroly prešli; pripojené K100 hlásilo G1–G6 a natívna konfigurácia/uvoľnenie bolo úspešné. Zvukové koncové body boli skontrolované bez zmeny ich stavu.
- WPF dym prešiel so všetkými rozloženiami referenčných prekrytí, pretrvávaním sprievodcu/katalógu/konfliktov/návrhu, konfiguráciou živého natívneho zachytávania, presmerovaním spätného volania, odmietnutím spätného volania a obnovením pozastaveného viazania. Bol simulovaný natívny vstup spätného volania; fyzické ľudské stlačenie G-key zostáva manuálnou kontrolou.
- Skutočná invariancia popredia a okna bola skontrolovaná pre vykonanie prípitku. Boli skontrolované vlastnosti interakcie prekrytia a zobrazenie/skrytie; snímky obrazovky boli vykreslené pre sprievodcu, prekrytie a prípitok.
- Jazyky 27 obsahujú všetky kanonické reťazce 819; overenie offline zástupného symbolu/kľúča prebehlo.

Snímky a úplný výsledok GUI sú v artefaktoch/usp-win-x64/smoke. Testy používajú dočasné profily; produkčné nastavenia sa zálohujú a kontrolujú hash počas nasadenia.

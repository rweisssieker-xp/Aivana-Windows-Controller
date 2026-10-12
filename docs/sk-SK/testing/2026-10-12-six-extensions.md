<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-12-six-extensions.md)

# Šesť rozšírení: implementácia a testovacia lavica

Stav: 12. Október 2026, Pobočka `codex/six-extensions`. Pracovný stav, bez vydania.

## servis

Paleta príkazov otvorí položky **Nastavte pracovný priestor**, **Výrobcovia klávesníc a SDK**, **Mobilný panel**, **Rozšírenia**, **automatizácia používateľského rozhrania**, **Zaznamenajte tok používateľského rozhrania** a **Kontrola vykonania**.

- Asistent pracovného priestoru vytvára profily Workplace, Meeting, Streaming, Research a Project. Programy z Registry/Start Menu alebo File Selector, URL, Output/Scene a OBS Scene sa zobrazujú ako kroky pred uložením. Zariadenie nevykonáva žiadne akcie.
- V editore krokov **Výsledok kontroly** zapína predbežnú kontrolu a následnú kontrolu. História a mobilný panel rozlišujú skutočné potvrdenie od neoveriteľného efektu. Relatívne zmeny, klávesové skratky a zadávanie textu neposkytujú všeobecné potvrdenie účinnosti. Doklad o procese nepreukazuje technickú pripravenosť žiadosti.
- Automatizácia používateľského rozhrania využíva samostatného, ​​časovo obmedzeného pracovníka. Výber pomocou polohy myši, kontrola selektora, Invoke/Select/Toggle/Focus/SetValue/WaitState; viacnásobné zásahy, deaktivované ciele a polia hesiel sú odmietnuté. Nahrávanie zachytáva podporované kliknutia a pauzy; Textové položky sa nezaznamenávajú. Prijatie bude pred prijatím skontrolované.
- Rozšírenia: Kontrola ZIP, hash, schéma, zakázaný import, explicitná dôvera pred aktiváciou, izolovaný proces s obmedzeným IPC, export, deaktivácia, odinštalovanie, obnovenie/zrušenie predchádzajúcej verzie. Izolácia procesov nie je karanténa: zdieľané doplnky sa spúšťajú s používateľskými právami. Vzorový balík `makrivo.example.zip` obsahuje odpočítavanie a stavovú akciu. Podporované schémy predstavujú zdokumentovanú jednoduchú podmnožinu schém JSON v zdrojovom kóde; žiadne verejné trhovisko.
- Mobilný panel: vyberte lokálnu IP a zdieľané profily, otvorte HTTPS adresu, skontrolujte certifikát pomocou zobrazeného SHA-256-Fingerprints a vedome mu dôverujte, potom zadajte jednorazový šesťmiestny kód. Súkromné ​​kľúče sú chránené pomocou Windows-DPAPI; ukladajú sa iba hodnoty hash klientskeho prístupu. Certifikáty sa menia po obnovení alebo zmene IP. Firewall Windows spustí používateľ v prípade potreby. Žiadna automatická inštalácia certifikátu, presmerovanie portov alebo zmena brány firewall. Klientsky prístup je možné odvolať. Panel zobrazuje stav akcie a históriu testov; ešte to nie je univerzálna palubná doska živých senzorov.

## Klávesnice

| Výrobca | Implementácia | Obmedzenie |
|---|---|---|
| Corsair | existujúce klávesy iCUE-G a RGB | Vyžaduje sa miestne pripojenie iCUE a vhodný model |
| Logitech G | G-Key SDK a samostatné LED SDK | explicitne uvoľniť pôvodný lokálny x64-DLL; G-Key SDK neposkytuje individuálnu identitu viacerých klávesníc |
| Razer | Chroma RGB a štandardné rozpoznávanie kľúčov | Priraďte proprietárne kľúče predtým v Synapse |
| SteelSeries | GameSense-RGB a štandardné rozpoznávanie kľúčov | priraďte proprietárne kľúče predtým v GG |
| Wooting | Analógové hodnoty súvisiace so zariadením, prahové hodnoty tlaku/uvoľnenia | pôvodné miestne x64-SDK-DLL; normálny HID vstup zostáva aktívny |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat | Štandardné fyzické tlačidlá podľa zariadenia a skenovacieho kódu | Fn a proprietárne ovládacie prvky závisia od modelu |

Nie sú zahrnuté žiadne nové proprietárne knižnice DLL SDK. Adaptéry nového výrobcu zatiaľ nie sú hardvérovo overené bez vhodných fyzických zariadení.

**EExkluzívny druhý makropad nie je pripravený:** Raw Input sleduje zariadenia oddelene, ale nepotláča ich normálny vstup Windows. Produkčné vydanie vyžaduje licencovaný, podpísaný ovládač filtra overený podľa Windows 11/Secure Testovanie spúšťania a núdzového uvoľnenia, zlyhania, pozastavenia a opätovného pripojenia. Nebol nainštalovaný žiadny ovládač. Aplikácia tento limit výslovne označuje.

## Skúšky

- Vydanie zostavy: úspešné, žiadne upozornenia/chyby.
- Základná konzola: Všetky testovacie skupiny prejdú úspešne, vrátane konfliktov kľúčov dodávateľa, vrátenia doplnku a skutočných chýb vykonania v histórii testov.
- Integrácia rozšírenia: skutočný pracovník UIA s testovacou aplikáciou WPF; jedinečné vyvolanie, nejednoznačnosť, zakázané ciele, vylúčenie hesla. Proces doplnku skutočného pomenovaného potrubia, hranice protokolu, analógová hysterézia a dekódovanie Logitech ABI úspešné.
- Skutočný hostiteľ HTTPS: pripnutý certifikát, nespárovaný prístup, falošný/jedinečný kód, ochrana pôvodu, súbory prehliadača, úložisko hash a chránený kľúč boli úspešné. Chyba Schannel s efemérnymi kľúčmi objavená pri testovaní bola opravená.
- Sada Windows s explicitne vynechaným testom úspešnosti v popredí: úspešný; Zvuk/OBS/iCUE/Raw Input a skontrolovaný stav obrazovky. V predchádzajúcom spustení zlyhala celá sada, pretože bolo odmietnuté prepnutie zamerania na izolované testovacie okno. Následný chod na prototyp AI tiež zahŕňal celý balík vrátane prepínania zaostrenia a pripojenej klávesnice iCUE. Ďalší opakovaný chod opäť zlyhal v dôsledku zmeny zamerania; Kontextovo závislý test teda nie je stabilne zelený. Tento test nebol utlmený. Prístupy na čítanie DDC, ktoré na krátky čas zlyhajú, sa na obmedzený čas opakujú; jas sa nezmenil.
- WPF-Smoke: vykreslené všetky predchádzajúce a nové dialógy, nemennosť konfigurácie, editor a CRUD úspešné; Snímky obrazovky v priečinku zostavy `smoke`.
- Prehliadač: Playwright with Edge, 390×844 a 1024×768, úspešné prepojenie toku/UUID/jednorazové odoslanie/žiadne horizontálne pretečenie. API Mock; skutočný test HTTPS samostatne. Obrázky na `artifacts/mobile-browser`. Doplnok prehliadača nebol k dispozícii, a preto je bežným záložným nástrojom Playwright.
- Vykonané nezávislé preskúmanie kódu; Opravené oneskorené mobilné príkazy, chýbajúce výsledky kontroly terminálu, kontrola cesty procesu, vrátenie doplnku a reštart RGB heartbeat.

Stále otvorené: exkluzívna integrácia ovládačov, testovanie fyzického výrobcu/telefónu/tabletu v LAN, škálovanie Windows percent 150/200 a úplná kontrola prekladu všetkých nových dialógových textov. Nové dialógové texty sú prevažne nemecké; Existujúce jazykové katalógy sú zachované. Preto celková požiadavka ** nebola úplne akceptovaná**.

## Vygenerovať balík

`./tools/publish-complete.ps1 -SelfContained` vytvorí nový priečinok pod `artifacts/release-*` vrátane pracovnej plochy, pracovníka UIA, hostiteľa HTTPS, súborov prehliadača, importovateľného vzorového doplnku a poznámok k licencii runtime. Nainštalovaná aplikácia sa neprepíše. Bez `-SelfContained` musí balík zodpovedať .NET-10-Desktop-/ASP.NET-Runtimes.

Balík je lokálne testovacie prostredie, nie podpísaný maloobchodný inštalátor. Vlastné podmienky používania a distribučné práva SDK sú samostatné vydania; pozri existujúcu dokumentáciu pod `docs/licensing`.

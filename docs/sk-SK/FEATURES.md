<!-- generated-doc locale=sk-SK source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../FEATURES.md)

# MAKRIVO – Prehľad funkcií

Výrobca: **Aivana**. Stav: **12. október 2026**. Toto je stav vývoja, nie predajné alebo hardvérové ​​vydanie.

## Ovládanie Windows

- Dlaždice, globálne klávesové skratky, paleta príkazov, obľúbené položky a profily závislé od aplikácie.
- Makrá s viacerými krokmi, opakovaním, pozastavením, zrušením a náhľadom.
- Otvoriť programy, súbory a webové stránky; Používajte text, schránku a klávesové skratky.
- Usporiadajte okná, uzamknite Windows, ovládajte zvukové výstupy a mikrofóny a zmeňte jas obrazovky prostredníctvom podporovaných rozhraní.
- Integrácia OBS-WebSocket-v5 pre scény, nahrávanie, streamovanie a zvuk.
- Šablóny, Workplace Assistant, Device Discovery a Keyboard Overlay.
- Import a export konfigurácií profilov; Kontrola dizajnu a detekcia konfliktov.
- Lokálne uložené nastavenia; na každodenné použitie nie je potrebný žiadny účet.

## Pokročilé funkcie v aktuálnom zdrojovom kóde

| oblasť | Rozsah funkcií | Hranice |
| --- | --- | --- |
| Kontrola výsledkov | Predbežná kontrola, následná kontrola a história overenia | Potvrďte iba overiteľné účinky; Úspešné stlačenie klávesu nedokazuje cieľový efekt. |
| Asistent na pracovisku | Pripravte profily a procesy z vybraných programov, súborov, adries URL a ďalších akcií | Je potrebný náhľad a uloženie; nastavenie nič nevykonáva. |
| Makro nahrávanie a pracovné postupy | Nahrávanie, podmienky, gestá a lokálna synchronizácia profilu | Správanie a limity sú zdokumentované v implementačných a testovacích správach. |
| automatizácia používateľského rozhrania | Vyberte ciele, skontrolujte selektory, zaznamenávajte akcie a obmedzené klikacie sekvencie | Samostatný pracovník; nejednoznačné, zakázané a ciele hesiel sú odmietnuté; žiadne všeobecné nahrávanie textu. |
| Rozšírenia | Kontrolujte, importujte, aktivujte na základe explicitnej dôvery, exportujte a vracajte balíky späť | Izolácia procesov nie je pieskovisko; nejde o verejné trhovisko doplnkov. |
| Mobilný panel | Lokálny HTTPS panel, párovanie, vybrané profily a stav/história auditov | Skutočná akceptácia siete LAN telefónu/tabletu je stále otvorená; žiadna univerzálna prístrojová doska senzorov. |
| Adaptér výrobcu | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB a Wooting analógový adaptér | Závisí od modelu/SDK; nové adaptéry nie sú plne overené skutočným hardvérom. Nie sú pribalené žiadne nové proprietárne knižnice DLL SDK. |
| Fyzické mapovanie klávesnice | Rozpoznávanie štandardných klávesov podľa zariadenia a skenovacieho kódu | Raw Input nepotláča normálny vstup Windows. |
| Miestny asistent stretnutia AI | Voliteľný prototyp pre Ollama, LM Studio, llama.cpp a ONNX Runtime GenAI CPU vytvára obmedzené návrhy z explicitne vybraných zdrojov | Predvolene vypnuté; natívne runtime prostredie CPU bolo overené s náhodným modelom, ale kvalita plánovania ani úspora času neboli preukázané; váhy modelov nie sú pribalené. |

## Zatiaľ nie je k dispozícii ako hotová funkcia predaja

- **Výhradná druhá klávesnica/makropad:** Chýba podpísaný, licencovaný a hardvérovo otestovaný filtrovací ovládač. Bežné stlačenia klávesov môžu stále doraziť do Windows.
- Kompletná hardvérová akceptácia adaptérov všetkých výrobcov, ako aj reálne testy LAN mobilných zariadení.
- Skutočné zmiešané zmeny monitora/DPI a technické schválenie všetkých nových prekladov. Kontrolovali sa PerMonitorV2 a simulované 150/200-Prozent-Layouts; Testy skutočného zobrazenia zostávajú oddelené. Rozhranie ponúka jazyky 27; Podrobné nové texty ešte nie sú úplne jazykovo schválené.
- Podpísaný maloobchodný inštalátor, konečná EULA, overené distribučné práva SDK a produktívna integrácia nákupnej licencie/platby.

## Podrobnosti a dôkazy

- [Služobná dokumentácia](README.md)
- [Rozšírenia: funkcie, testy a otvorené hranice](testing/2026-10-12-six-extensions.md)
- [Lokálny asistent: prototyp a skúšobný stupeň](testing/2026-10-12-local-meeting-assistant.md)
- [Miestne časy a požiadavky na modelovanie](testing/2026-10-12-local-ai-runtimes.md)
- [DPI a skúška skalácie](testing/2026-10-12-dpi-scaling.md)
- [Licenčná skúška](licensing/2026-10-12-commercial-license-audit.md) – historické záznamy; nový skript Publish doplňuje návod na spustenie času. Konečný balík sa má preskúmať samostatne.
- [Predajné platformy](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Porovnávací stav; Stripe je navyše možnou platobnou platformou, pozri aktuálne podmienky poskytovateľa.

## Vytvorte kompletný vývojový balík

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Balíček obsahuje desktopovú aplikáciu, UI-Automation-Worker, mobilného hostiteľa, súbory prehliadača, ukážkový doplnok a licenčné oznámenia runtime prostredia. Artefakty zostavenia sa neukladajú do Git. Stav zdrojového kódu na GitHub ešte nie je publikovaným inštalačným programom Windows.

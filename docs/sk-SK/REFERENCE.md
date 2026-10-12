<!-- generated-doc locale=sk-SK source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../README.md)

# MAKRIVO

[Kompletná dokumentácia vo všetkých jazykoch 27](README.md) · [Úplný užívateľský manuál](HANDBOOK.md)

**podľa Aivana.** [Aktuálny inventár a obmedzenia uvoľnenia](FEATURES.md) · [Rozšírený test](testing/2026-10-12-six-extensions.md) · [Miestny asistent prototypu](testing/2026-10-12-local-meeting-assistant.md)

**Eangličtina (USA)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokálny ovládač klávesových skratiek Windows s dlaždicami v štýle Stream Deck, profilmi, makrami a detekciou klávesnice. Rozhranie podporuje jazyky 27. Na každodenné používanie nie je potrebný žiadny účet ani cloudové pripojenie.

[dokumentácia](README.md) · [Overenie](VERIFICATION.md) · [Funkcia testovania](testing/2026-10-09-usp-release.md)

## Začíname

Po zverejnení je aplikácia na `artifacts/win-x64/MAKRIVO.exe`. Udržujte celý výstupný priečinok pohromade; môžete ho skopírovať inde. Windows 11 x64 je počiatočný cieľ. Artefakty zostavy sú vylúčené z Gitu; pokladňa zdroja nezahŕňa vopred zostavený spustiteľný súbor.

Nainštalujte súpravu .NET 10 SDK špecifikovanú v `global.json` na zostavenie a spustenie zo zdroja:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publikujte samostatnú zostavu bez potreby samostatne nainštalovaného prostredia .NET runtime:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Základný pracovný postup

1. Vyberte profil a vytvorte dlaždicu akcie.
2. Nastavte jej názov, ikonu, farbu a klávesovú skratku. Kombinácie Ctrl/Alt/Shift znižujú náhodnú aktiváciu pri písaní.
3. Pridajte jeden alebo viac krokov: aplikácie, súbory, webové stránky, text, skratky, médiá, hlasitosť, prepínanie profilov alebo oneskorenia.
4. Uložiť. Zmeny zostávajú konceptom, kým sa neuložia; discard obnoví uloženú verziu. Tlačidlo Prehrať dlaždice a jeho klávesová skratka používajú rovnakú cestu spustenia.
5. Zmeňte poradie dlaždíc a krokov makier pomocou presunutia myšou alebo tlačidiel so šípkami. Šablóny zjednodušujú nastavenie. Makrá sa môžu opakovať až 100-krát. V predvolenom nastavení zrušte prostredníctvom rozhrania, ponuky na systémovej lište alebo Ctrl+Alt+Escape.

Profily špecifické pre aplikáciu používajú názov procesu bez jeho úplnej cesty. Poradie profilu prerušuje väzby; predvolený profil je záložný. V Nastaveniach musí byť povolený automatický výber profilu.

Zatvorením hlavného okna zostane aplikácia v predvolenom nastavení na systémovej lište. Dvakrát kliknite na ikonu na paneli, aby ste ju znova otvorili; na ukončenie použite ponuku zásobníka. Spustenie pomocou Windows je voliteľné a predvolene vypnuté. Klávesové skratky je možné centrálne pozastaviť.

## Paleta akcií a príkazov

Pridať akciu otvorí katalóg s možnosťou vyhľadávania s predvoľbami 90 v ôsmich kategóriách. Každá má popis a náhľad. Editor makier používa rovnaký katalóg na pridanie alebo nahradenie krokov. Výberom predvoľby v tomto pracovnom postupe editora sa vytvorí koncept bez vykonania.

Paleta príkazov sa otvorí z hlavičky alebo pomocou Ctrl+K v aplikácii. Vyhľadáva uložené akcie naprieč profilmi, predvoľby katalógu, prepínanie profilov, navigáciu, pozastavenie/obnovenie, zrušenie makra a import/export. Filtrujte podľa uložených akcií, funkcií katalógu alebo príkazov aplikácie. Klávesy so šípkami vyberú, Enter aktivuje, Ctrl+Enter upraví upraviteľný záznam a Escape sa zatvorí. Obľúbené sa zobrazia ako prvé. Príkazy katalógu pripravené na použitie sa môžu spustiť okamžite; predvoľby vyžadujúce nastavenie otvorte koncept. Nastavenia môžu definovať skratku globálnej palety. Nové konfigurácie používajú Ctrl+Shift+Space; existujúce konfigurácie nedostanú novú globálnu skratku automaticky.

Zvukové akcie nastavujú hlasitosť presne od 0 do percent 100 a stlmujú, zapínajú alebo prepínajú výstupné a mikrofónové zariadenia. Vyberte konkrétny koncový bod alebo ho ponechajte nenastavený pre predvolenú hodnotu Windows. Nedostupný uložený koncový bod vytvára chybu namiesto ovládania iného zariadenia. Obnoviť zariadenia znova načíta pripojené zariadenia.

OBS Studio používa WebSocket v5: spustenie/zastavenie/prepnutie/pozastavenie/obnovenie nahrávania; spustenie/zastavenie/prepnutie streamovania; zmeniť scény; prepnúť stlmenie vstupu. Povoľte server WebSocket v časti Nástroje v OBS a nastavte jeho koncový bod v editore. Check Connection and Load Names načíta verziu OBS, scény a vstupy bez spustenia nahrávania alebo streamu. Vyberte mená zo zoznamu alebo ich zadajte ručne. Heslá je možné uložiť v Windows Credential Manager a sú vylúčené z exportovaných konfigurácií. Nedostupné pripojenia OBS hlásia chybu.

Profily sa môžu zhodovať s názvom procesu a časťou názvu okna. Obe špecifikované podmienky sa musia zhodovať. Lock Profile zabraňuje automatickému prepínaniu relácie. Capture Conditions from Foreground využíva poslednú zameranú externú aplikáciu.

Podpora akcií s textom a schránkou `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, a `{{user}}`. Použitie dátumov `yyyy-MM-dd`; krát použiť `HH:mm`. Neznáme zástupné symboly zostávajú nezmenené. `{{profile}}` identifikuje profil spustenej akcie; `{{process}}` identifikuje kontext externej aplikácie. Obsah schránky sa číta iba vtedy, keď je použitý tento zástupný symbol. Akcia schránky zapíše text, ale neprilepí sa automaticky.

Akcie okna minimalizujú, maximalizujú, obnovujú alebo usporiadajú okno na ľavej/pravej strane pracovnej plochy monitora. Akcia systému uzamkne Windows. Tieto akcie sa spúšťajú iba vtedy, keď sú explicitne vyvolané.

Náhľad zobrazuje kroky, opakovania a premenné bez vykonania. Späť a Znova zachovajú stavy konceptu 100; textové polia ponechávajú natívne úpravy pomocou Ctrl+Z. Označte obľúbené položky v editore alebo ponuke dlaždíc a filtrujte ich na informačnom paneli. Počty vykonania sa vzťahujú iba na aktuálnu reláciu.

Akcie textu, skratiek a okien spustené z rozhrania aktivujú posledné externé cieľové okno. Paleta zobrazuje cieľový kontext. Ak sa cieľ zatvoril alebo Windows odmietne aktiváciu, vykonávanie sa zastaví so správou.

## Pomoc s klávesnicou a spätná väzba

Sprievodca špeciálnym kľúčom sa nachádza na stránke Klávesnica a na palete. Uvádza zoznam G-kľúčov hlásených iCUE. Stlačenie tlačidla detekcie ich dočasne zachytí bez vykonania priradených akcií. Normálne väzby sa vrátia po detekcii, zrušení alebo zatvorení. Funkcia katalógu a vybraný kľúč sa otvoria ako neuložený koncept; skontrolujú sa duplicitné väzby.

Prekrytie klávesnice zobrazuje vybraté referenčné rozloženie a aktívne väzby. Podržte Ctrl+Alt+F1 alebo ho prepnite z palety alebo zo stránky Klávesnica. Nezaoberá sa a necháva kliknutia myšou prejsť. Zmeny profilu a väzby sa aktualizujú, keď sú viditeľné; bežné stlačenia klávesov Windows sú zvýraznené. Ovládacie prvky špecifické pre výrobcu nemusia poskytovať ekvivalentný signál kľúča Windows.

Spätná väzba o vykonaní zobrazuje čakanie, priebeh krokov/opakovaní, dokončenie, zrušenie alebo zlyhanie bez zamerania sa alebo odkrytia zadaného textu. Commands Executed potvrdzuje vykonanie príkazu, nie nezávisle dotazovaný aktuálny stav v aplikácii tretej strany. Podrobnosti o chybe sa zobrazia v denníku aktivít.

## Zariadenia a Corsair

Corsair G1–G6 je možné priradiť priamo prostredníctvom oficiálneho iCUE SDK. Vyberte zariadenie a stlačte v editore akcií a uložte alebo pripravte koncept zo zobrazenia klávesnice K100. Väzby sa vzťahujú na vybrané zariadenie a aktívny profil; pauza ich tiež zakáže. iCUE musí byť spustený a musí povoliť prístup. Fn, ovládacie prvky koliesok, profilu a zámku iCUE nie je možné mapovať prostredníctvom tohto rozhrania G-key API.

Stránka Zariadenia obsahuje zoznam rozhraní klávesnice Windows a pravidelne sa obnovuje, keď sa zariadenia pripájajú. Fyzická klávesnica môže odhaliť viacero rozhraní HID. Štandardné globálne klávesové skratky fungujú u všetkých výrobcov.

Detekcia Corsair využíva dostupné informácie o zariadení. Ovládanie RGB vyžaduje kompatibilný hardvér so systémom iCUE a oficiálnu súpravu iCUE SDK; aplikácia hlási skutočnú dostupnosť. Ostatní výrobcovia v súčasnosti dostávajú detekciu a štandardné akcie. Mimo integrácie G-key Corsair fungujú proprietárne ovládacie prvky, keď ich Windows prijme ako bežný vstup. Aplikácia neinštaluje ovládač klávesnice.

## Jazyky

Nastavenia ponúkajú jazyky 27: nemčina, angličtina, francúzština, španielčina, taliančina, portugalčina, holandčina, poľština, čeština, slovenčina, maďarčina, rumunčina, bulharčina, gréčtina, dánčina, švédčina, nórčina, fínčina, estónčina, lotyština, litovčina, slovinčina, chorvátčina, ukrajinčina, ruština, turečtina a srbčina v latinke. Automaticky sa riadi jazykom zobrazenia Windows. Preklady sa zhromažďujú lokálne. Rozloženia, klávesové skratky a názvy vytvorené používateľom sú nezávislé od jazyka rozhrania.

Kompletná dokumentácia je k dispozícii vo všetkých jazykoch 27 prostredníctvom [Jazykový index](README.md)A to Preklady sú strojovo podporované, s originálnymi zdrojovými odkazmi a štrukturálnymi kontrolami; recenzia pôvodného reproduktora je oddelená. Aplikácia tiež prijíma regionálne kódy `en-US`, `de-DE`, `fr-FR` a `it-IT` a používa ich číselné formáty. Existujúce jazykové kódy zostávajú kompatibilné.

## Rozloženie a prieskum výrobcu

Zobrazenie klávesnice poskytuje referencie ISO-DE/ANSI-US v plnej veľkosti a bez použitia klávesov plus rozloženie RGB Corsair K100. Geometria predstavuje pozície, široké/vysoké klávesy a ISO Enter. Štandardné rozloženia sú všeobecné referencie. Rozloženie K100 sa riadi zdokumentovanými ovládacími prvkami bez nároku na milimetre presné rozmery. Vyberte si správny jazyk/geometrickú variantu: názov modelu alebo USB VID/PID neurčuje konkrétny variant.

Mapovanie rieši fyzické skenovacie kódy pomocou aktívneho rozloženia vstupu Windows. Pracovný postup pripraví koncept a zachová kontroly uloženia/konfliktov. Štandardné väzby sú globálne klávesové skratky Windows, ktoré nie sú izolované na jednej pripojenej klávesnici. Corsair G1–G6 použite integráciu iCUE špecifickú pre zariadenie. Iné proprietárne ovládacie prvky vyžadujú adaptér alebo výrobcom nakonfigurované skratky. Main Enter a numerická klávesnica Enter zdieľajú identitu virtuálneho kľúča pre globálne klávesové skratky.

V tomto prípade [Výrobca výskumu](research/tastaturhersteller-sonderfunktionen.md) a [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokrýva 74 skupiny značiek/výrobcov, rozlišujú konfigurátory, vstupné SDK a osvetlenie API. [Layout dôkazov](research/keyboard-layout-evidence.md) Model záznamov, SKU a modulové rozdiely. Výskumné pokrytie neznamená, že sa implementuje hardvérová podpora.

## Údaje

Konfigurácia je uložená v dátach lokálnej aplikácie. Import/export použite JSON. Import zobrazí ukážku konfigurácie, po potvrdení nahradí aktuálnu konfiguráciu a zakáže importované väzby, kým sa nepovolí manuálne. Obsah textových akcií sa nezhromažďuje ako denník stlačenia klávesov. Heslá správcu poverení sú oddelené od exportovaných profilov.

## Overenie

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Kontroly Windows môžu kontrolovať hardvér a dočasne nakonfigurovať/uvoľniť podporované väzby iCUE. Spustite ich bez ďalšieho ovládača pomocou týchto väzieb. Kontroly koncového bodu zvuku nemenia živú hlasitosť ani nastavenia stlmenia.

Natívny vstup nemôže obísť oprávnenia Windows. Normálne spustený ovládač nemusí odosielať vstup do aplikácie správcu. Vyhradené alebo obsadené klávesové skratky sa zobrazujú ako konflikty.

V tomto prípade [Dokumentácia index](README.md) odkaz historických návrhov, plánov, výskumov a dátum testov. Historické plány popisujú ich zaznamenaný rozsah, nie súčasnú záruku podpory funkcií.

### Šablóny pracovného toku a čakania na cieľové okno

Katalóg obsahuje šesť upravených pracovných tokov: pracovný priestor, stretnutie, streamovanie, výskum, projektový prepínač a textový snippet. `workflow.` v katalógu alebo palete príkazov. výber pracovného toku otvára nezachovaný návrh; nikdy nefunguje okamžite. Všetky kroky sú zachované pri pridávaní pracovného toku k existujúcemu makru alebo pri pridelení do Corsair nastaviť príklad URL, programové trasy, profily, a OBS Predtým, než sa zachráni.

a to **Čakajte na cieľové okno** krok akceptuje procesný názov (s alebo bez `.exe`, prípadne necitlivý titul podstring, a časový výstup 1–600,000 ms. Minimálne jeden filter je potrebný; oba filtre musia byť v súlade, keď sú dodané. makro pokračuje len po tom, čo zodpovedajúce viditeľné okno reaguje a dosahuje prednej časti. Použite konkrétny názov, keď sa viacero okien zdieľa proces. Zrušenie alebo časový výstup zastaví nasledujúce kroky. OBS WebSocket Je to pripojené.

Zobraziť jas: Hľadať **Zvýšiť jas obrazovky**, **Zníženie jasu obrazovky**alebo **obrazovka jasné na 50 percento** v katalógu akcie. relatívne akcie menia jasnosť podľa 10 percentuálne body; fixné hodnoty rozsah od 0 na 100Vnútorné displeje podporované pomocou WMI; kompatibilné externé monitory vyžadujú DDC/CI V ich menu je povolený.V predvolenom nastavení sa akcia dotýka všetkých podporovaných displejov; v editoru je možné vybrať výslovný monitor.

### Rozšírené kontroly

Otvorte paletu s **Ctrl+K, zatiaľ čo okno aplikácie sa zameriava**Hľadanie pre **pracovný dizajn**, **Záznam makra**, **Synchronizácia profilov**alebo **Kľúčová diagnostika** (formátovanie nasleduje zvolený jazyk). dizajnér pracovného toku upravuje zvolený návrh štítku: súbor / proces / podmienky okna, predné skoky počítajú a zastaviť / pokračovať v spracovaní chýb.

V kroku jasu vyberte všetky podporované displeje alebo konkrétny monitor. Nedostupný vybraný monitor spôsobí chybu, namiesto toho, aby ticho zmenil iný displej. Prístrojová doska zobrazuje živý jas, zvuk a hodnoty OBS, keď sú dostupné ich príslušné integrácie; nedostupná hodnota nie je prezentovaná ako nameraná hodnota.

V editore dlaždíc priraďte podporovaný fyzický špeciálny kláves a vyberte okamžité stlačenie, ťuknutie, podržanie alebo dvojité ťuknutie. Podržané selektory vrstiev dočasne aktivujú ďalšie mapovanie a obnovia predchádzajúcu vrstvu pri vydaní. Gestá a vrstvy sa vzťahujú na G-klávesy SDK Corsair SDK a štandardné špeciálne klávesy špecifické pre zariadenie, nie bežné globálne klávesové skratky. Zachytenie štandardných klávesov podporuje F13–F24 a multimediálne klávesy klávesnice. Vlastné ovládanie Fn, profilu a číselníka stále vyžaduje kompatibilný adaptér dodávateľa alebo mapovanie dodávateľa na štandardný kľúč. Raw Input sleduje kľúč a nepotláča jeho normálne správanie Windows. Kľúčová diagnostika vysvetľuje problémy s profilom, pauzou, pripojením a väzbou.

Synchronizácia profilu vymieňa súbory prostredníctvom priečinka, ktorý si vyberiete, vrátane priečinka spravovaného vaším existujúcim cloudovým klientom. Pred importovaním skontrolujte náhľad; spustenie importu je zakázané a odstráni mapovania špecifické pre hardvér. Revízie zabraňujú tichému prepisovaniu a zahrnutie obsahu akcie je explicitnou voľbou. MAKRIVO neposkytuje cloudové konto ani neuchováva cloudové poverenia. Podnos teraz používa vlastnú modrozelenú ikonu M MAKRIVO a pri pozastavení zobrazuje jantárový odznak pozastavenia.

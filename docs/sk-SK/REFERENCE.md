<!-- generated-doc locale=sk-SK source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../README.md)

# MAKRIVO

[Kompletná dokumentácia vo všetkých jazykoch 27](README.md) · [Úplný užívateľský manuál](HANDBOOK.md)

**podľa Aivana.** [Aktuálny súpis funkcií a obmedzenia vydania](FEATURES.md) · [Správa o testoch rozšírení](testing/2026-10-12-six-extensions.md) · [Prototyp lokálneho asistenta AI](testing/2026-10-12-local-meeting-assistant.md)

**Eangličtina (USA)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokálny ovládač klávesových skratiek Windows s dlaždicami v štýle Stream Deck, profilmi, makrami a detekciou klávesnice. Rozhranie podporuje jazyky 27. Na každodenné používanie nie je potrebný žiadny účet ani cloudové pripojenie.

[dokumentácia](README.md) · [Overenie](VERIFICATION.md) · [Správa o testoch funkcií](testing/2026-10-09-usp-release.md)

## Začíname

Po publikovaní sa aplikácia nachádza v `artifacts/win-x64/MAKRIVO.exe`. Uchovávajte celý výstupný priečinok spolu; môžete ho skopírovať inde. Počiatočnou cieľovou platformou je Windows 11 x64. Artefakty zostavenia sú vylúčené z Git; pracovná kópia zdrojového repozitára neobsahuje vopred zostavený spustiteľný súbor.

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
3. Pridajte jeden alebo viac krokov: aplikácie, súbory, weby, text, klávesové skratky, médiá, hlasitosť, prepínanie profilov alebo prestávky.
4. Uložiť. Zmeny zostávajú konceptom, kým sa neuložia; discard obnoví uloženú verziu. Tlačidlo Prehrať dlaždice a jeho klávesová skratka používajú rovnakú cestu spustenia.
5. Zmeňte poradie dlaždíc a krokov makier pomocou presunutia myšou alebo tlačidiel so šípkami. Šablóny zjednodušujú nastavenie. Makrá sa môžu opakovať až 100-krát. V predvolenom nastavení zrušte prostredníctvom rozhrania, ponuky na systémovej lište alebo Ctrl+Alt+Escape.

Profily pre konkrétne aplikácie používajú názov procesu bez celej cesty. Poradie profilov rozhoduje pri viacerých zhodách; predvolený profil sa použije, ak žiadna zhoda nie je. Automatický výber profilu treba povoliť v Nastaveniach.

Zatvorenie hlavného okna predvolene ponechá aplikáciu v oblasti oznámení. Dvojklikom na jej ikonu okno znova otvoríte; aplikáciu ukončíte z ponuky ikony. Spúšťanie s Windows je voliteľné a predvolene zakázané. Klávesové skratky možno centrálne pozastaviť.

## Paleta akcií a príkazov

Pridať akciu otvorí katalóg s možnosťou vyhľadávania s predvoľbami 90 v ôsmich kategóriách. Každá má popis a náhľad. Editor makier používa rovnaký katalóg na pridanie alebo nahradenie krokov. Výberom predvoľby v tomto pracovnom postupe editora sa vytvorí koncept bez vykonania.

Paleta príkazov sa otvára z hlavičky alebo pomocou Ctrl+K v aplikácii. Vyhľadáva uložené akcie naprieč profilmi, predvoľby katalógu, prepínanie profilov, navigáciu, pozastavenie/obnovenie, zrušenie makier a import/export. Filtrujte podľa uložených akcií, funkcií katalógu alebo príkazov aplikácie. Šípky vyberajú položku, Enter ju aktivuje, Ctrl+Enter upraví upraviteľnú položku a Escape paletu zavrie. Obľúbené položky sú prvé. Hotové príkazy katalógu možno spustiť ihneď; predvoľby vyžadujúce nastavenie otvoria koncept. V Nastaveniach možno určiť globálnu skratku palety. Nové konfigurácie používajú Ctrl+Shift+Space; existujúce konfigurácie nedostanú novú globálnu skratku automaticky.

Zvukové akcie nastavujú hlasitosť presne od 0 do 100 percent a stlmujú, obnovujú zvuk alebo prepínajú stlmenie výstupných a mikrofónových zariadení. Vyberte konkrétny koncový bod alebo ho ponechajte nenastavený pre predvolené zariadenie Windows. Nedostupný uložený koncový bod spôsobí chybu namiesto ovládania iného zariadenia. Obnovenie zariadení znova načíta pripojené zariadenia.

OBS Studio používa WebSocket v5: spustenie/zastavenie/prepnutie/pozastavenie/obnovenie nahrávania; spustenie/zastavenie/prepnutie streamovania; zmeniť scény; prepnúť stlmenie vstupu. Povoľte server WebSocket v časti Nástroje v OBS a nastavte jeho koncový bod v editore. Check Connection and Load Names načíta verziu OBS, scény a vstupy bez spustenia nahrávania alebo streamu. Vyberte mená zo zoznamu alebo ich zadajte ručne. Heslá je možné uložiť v Windows Credential Manager a sú vylúčené z exportovaných konfigurácií. Nedostupné pripojenia OBS hlásia chybu.

Profily môžu zodpovedať názvu procesu a časti názvu okna. Obe zadané podmienky musia platiť. Zámok profilu bráni automatickému prepínaniu v aktuálnej relácii. Zachytenie podmienok z popredia používa poslednú externú aplikáciu s fokusom.

Textové akcie a akcie schránky podporujú `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` a `{{user}}`. Dátumy používajú `yyyy-MM-dd`; časy používajú `HH:mm`. Neznáme zástupné symboly zostávajú nezmenené. `{{profile}}` označuje profil spustenej akcie; `{{process}}` označuje kontext externej aplikácie. Obsah schránky sa číta iba pri použití tohto zástupného symbolu. Akcia schránky zapíše text, ale automaticky ho nevloží.

Akcie okna minimalizujú, maximalizujú, obnovujú alebo usporiadajú okno na ľavej/pravej strane pracovnej plochy monitora. Akcia systému uzamkne Windows. Tieto akcie sa spúšťajú iba vtedy, keď sú explicitne vyvolané.

Náhľad ukazuje kroky, opakovania a premenné bez vykonania. Späť a Znova uchovávajú až 100 stavov konceptu; textové polia zachovávajú natívne úpravy Ctrl+Z. Označte obľúbené položky v editore alebo ponuke dlaždice a filtrujte ich na dashboarde. Počty vykonaní platia iba pre aktuálnu reláciu.

Akcie textu, skratiek a okien spustené z rozhrania aktivujú posledné externé cieľové okno. Paleta zobrazuje cieľový kontext. Ak sa cieľ zatvoril alebo Windows odmietne aktiváciu, vykonávanie sa zastaví so správou.

## Pomoc s klávesnicou a spätná väzba

Sprievodca špeciálnymi klávesmi je na stránke Klávesnica a v palete. Uvádza klávesy G hlásené iCUE. Detekcia stlačenia klávesu ich dočasne zachytí bez vykonania priradených akcií. Bežné väzby sa obnovia po detekcii, zrušení alebo zatvorení. Funkcia katalógu a vybraný kláves sa otvoria ako neuložený koncept; kontrolujú sa duplicitné väzby.

Prekrytie klávesnice ukazuje vybrané referenčné rozloženie a aktívne väzby. Podržte Ctrl+Alt+F1 alebo ho prepnite z palety či stránky Klávesnica. Nepreberá fokus a prepúšťa kliknutia myšou. Keď je viditeľné, aktualizuje zmeny profilu a väzieb; bežné stlačenia klávesov Windows sú zvýraznené. Ovládacie prvky špecifické pre výrobcu nemusia poskytovať zodpovedajúci signál klávesu Windows.

Spätná väzba vykonania ukazuje čakanie, priebeh krokov/opakovaní, dokončenie, zrušenie alebo zlyhanie bez prevzatia fokusu či odhalenia zadaného textu. Commands Executed potvrdzuje vykonanie príkazu, nie nezávisle zisťovaný živý stav aplikácie tretej strany. Podrobnosti chýb sú v denníku aktivít.

## Zariadenia a Corsair

Corsair G1–G6 možno priradiť priamo prostredníctvom oficiálneho iCUE SDK. Vyberte zariadenie a kláves v editore akcií a uložte alebo pripravte koncept zo zobrazenia klávesnice K100. Väzby platia pre vybrané zariadenie a aktívny profil; pauza ich tiež zakáže. iCUE musí bežať a povoliť prístup. Fn, koliesko iCUE a ovládacie prvky profilu a zámku nemožno mapovať cez toto G-key API.

Stránka Zariadenia uvádza rozhrania klávesníc Windows a obnovuje sa pravidelne aj pri pripojení zariadenia. Fyzická klávesnica môže poskytovať viac rozhraní HID. Štandardné globálne skratky fungujú naprieč výrobcami.

Detekcia Corsair používa dostupné informácie o zariadení. Ovládanie RGB vyžaduje kompatibilný hardvér, bežiaci iCUE a oficiálny iCUE SDK; aplikácia hlási skutočnú dostupnosť. Ostatní výrobcovia majú teraz detekciu a štandardné akcie. Mimo integrácie klávesov G Corsair fungujú proprietárne ovládacie prvky, ak ich Windows prijíma ako bežný vstup. Aplikácia neinštaluje ovládač klávesnice.

## Jazyky

Nastavenia ponúkajú 27 jazykov: nemčinu, angličtinu, francúzštinu, španielčinu, taliančinu, portugalčinu, holandčinu, poľštinu, češtinu, slovenčinu, maďarčinu, rumunčinu, bulharčinu, gréčtinu, dánčinu, švédčinu, nórčinu, fínčinu, estónčinu, lotyštinu, litovčinu, slovinčinu, chorvátčinu, ukrajinčinu, ruštinu, turečtinu a srbčinu v latinke. Automatická voľba sa riadi jazykom zobrazenia Windows. Preklady sú pribalené lokálne. Rozloženia, klávesové skratky a používateľské názvy sú nezávislé od jazyka rozhrania.

Úplná dokumentácia repozitára je dostupná vo všetkých 27 jazykoch aplikácie prostredníctvom [Jazykový index](README.md). Preklady sú strojovo podporované, obsahujú odkazy na pôvodné zdroje a prechádzajú štrukturálnymi kontrolami; revízia rodeným hovoriacim je samostatná. Aplikácia prijíma aj regionálne kódy `en-US`, `de-DE`, `fr-FR` a `it-IT` a používa ich číselné formáty. Doterajšie jazykové kódy zostávajú kompatibilné.

## Rozloženie a prieskum výrobcu

Zobrazenie klávesnice poskytuje plnohodnotné referenčné rozloženia ISO-DE/ANSI-US aj varianty bez numerického bloku a rozloženia Corsair K100 RGB. Geometria znázorňuje polohy, široké/vysoké klávesy a ISO Enter. Štandardné rozloženia sú všeobecné referencie. Rozloženia K100 vychádzajú zo zdokumentovaných ovládacích prvkov bez tvrdenia o rozmeroch presných na milimeter. Vyberte správny jazykový/geometrický variant: názov modelu ani USB VID/PID neurčujú konkrétny variant.

Mapovanie interpretuje fyzické skenovacie kódy pomocou aktívneho vstupného rozloženia Windows. Postup pripraví koncept a zachová kontroly uloženia/konfliktov. Štandardné väzby sú globálne skratky Windows, nie sú obmedzené na jednu pripojenú klávesnicu. Corsair G1–G6 používajú integráciu iCUE pre konkrétne zariadenie. Iné proprietárne ovládacie prvky vyžadujú adaptér alebo výrobcom nakonfigurované skratky. Hlavný Enter a Enter numerickej klávesnice zdieľajú pre globálne skratky rovnaký identifikátor virtuálneho klávesu.

V tomto prípade [Výrobca výskumu](research/tastaturhersteller-sonderfunktionen.md) a [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokrýva 74 skupiny značiek/výrobcov, rozlišujú konfigurátory, vstupné SDK a osvetlenie API. [Layout dôkazov](research/keyboard-layout-evidence.md) zaznamenáva rozdiely modelov, SKU a modulov. Pokrytie prieskumom neznamená implementovanú podporu hardvéru.

## Údaje

Konfigurácia je uložená v dátach lokálnej aplikácie. Import/export použite JSON. Import zobrazí ukážku konfigurácie, po potvrdení nahradí aktuálnu konfiguráciu a zakáže importované väzby, kým sa nepovolí manuálne. Obsah textových akcií sa nezhromažďuje ako denník stlačenia klávesov. Heslá správcu poverení sú oddelené od exportovaných profilov.

## Overenie

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Kontroly Windows môžu skúmať hardvér a dočasne konfigurovať/uvoľňovať podporované väzby iCUE. Spúšťajte ich bez iného ovládača používajúceho tieto väzby. Kontroly zvukových koncových bodov nemenia aktuálnu hlasitosť ani nastavenia stlmenia.

Natívny vstup nemôže obísť oprávnenia Windows. Normálne spustený ovládač nemusí odosielať vstup do aplikácie správcu. Vyhradené alebo obsadené klávesové skratky sa zobrazujú ako konflikty.

V tomto prípade [Dokumentácia index](README.md) odkazuje na historické návrhy, plány, prieskum a datované testovacie správy. Historické plány opisujú zaznamenaný rozsah, nie aktuálnu záruku podpory funkcií.

### Šablóny pracovného toku a čakania na cieľové okno

Katalóg obsahuje šesť upraviteľných pracovných postupov: pracovisko, schôdza, streamovanie, výskum, zmena projektu a textový úryvok. Vyhľadajte `workflow.` v katalógu alebo palete príkazov. Výber postupu otvorí neuložený koncept; nikdy ho ihneď nespustí. Pri pridaní postupu do existujúceho makra alebo priradení ku klávesu Corsair zostanú všetky kroky zachované. Pred uložením nastavte príklady URL, cesty programov, profily a scény OBS.

Krok **Wait for target window** prijíma názov procesu (s `.exe` alebo bez), časť názvu bez rozlíšenia veľkosti písmen a časový limit 1–600000 ms. Vyžaduje sa aspoň jeden filter; ak sú zadané oba, musia platiť oba. Makro pokračuje až po reakcii zodpovedajúceho viditeľného okna a jeho prechode do popredia. Ak proces zdieľa viac okien, použite konkrétny názov. Zrušenie alebo vypršanie limitu zastaví ďalšie kroky. Pripravenosť okna nezaručuje načítanie dokumentu ani pripojenie externej služby, napríklad OBS WebSocket.

Jas displeja: v katalógu akcií vyhľadajte **Zvýšiť jas obrazovky**, **Znížiť jas obrazovky** alebo **Jas obrazovky na 50 percent**. Relatívne akcie menia jas o 10 percentuálnych bodov; pevné hodnoty sú od 0 do 100. Podporované interné displeje používajú WMI; kompatibilné externé monitory vyžadujú DDC/CI povolené vo svojej ponuke. Predvolene akcia ovplyvní všetky podporované displeje; v editore možno vybrať konkrétny monitor. Pripojenia a doky môžu podporu obmedziť.

### Rozšírené kontroly

Otvorte paletu príkazov pomocou **Ctrl+K, keď má okno aplikácie fokus**. Vyhľadajte **Návrhár pracovných postupov**, **Záznam makra**, **Synchronizovať profily** alebo **Diagnostika klávesov** (znenie zodpovedá vybranému jazyku). Návrhár upravuje vybraný koncept dlaždice: podmienky súboru/procesu/okna, počty preskočených nasledujúcich krokov a zastavenie/pokračovanie pri chybe. Záznamník zachytáva kombinácie klávesových skratiek a prestávky, nie bežné písanie; Escape záznam zastaví. Pred prevzatím výsledok skontrolujte.

V kroku jasu vyberte všetky podporované displeje alebo konkrétny monitor. Nedostupný vybraný monitor spôsobí chybu, namiesto toho, aby ticho zmenil iný displej. Prístrojová doska zobrazuje živý jas, zvuk a hodnoty OBS, keď sú dostupné ich príslušné integrácie; nedostupná hodnota nie je prezentovaná ako nameraná hodnota.

V editore dlaždice priraďte podporovaný fyzický špeciálny kláves a vyberte okamžité stlačenie, krátke stlačenie, podržanie alebo dvojité stlačenie. Podržané voliče vrstiev dočasne aktivujú iné mapovanie a pri uvoľnení obnovia predchádzajúcu vrstvu. Gestá a vrstvy platia pre klávesy G Corsair SDK a štandardné špeciálne klávesy konkrétneho zariadenia, nie pre bežné globálne skratky. Zachytenie štandardných klávesov podporuje F13–F24 a multimediálne klávesy klávesnice. Proprietárne ovládanie Fn, profilu a otočného ovládača stále vyžaduje kompatibilný adaptér výrobcu alebo mapovanie výrobcu na štandardný kláves. Raw Input kláves sleduje a nepotláča jeho bežné správanie vo Windows. Diagnostika klávesov vysvetľuje problémy profilu, pauzy, pripojenia a väzieb.

Synchronizácia profilov vymieňa súbory cez vybraný priečinok vrátane priečinka spravovaného vaším existujúcim cloudovým klientom. Pred importom skontrolujte náhľad; importované akcie sú spočiatku zakázané a hardvérovo špecifické mapovania sa odstránia. Revízie zabraňujú tichému prepísaniu a zahrnutie obsahu akcií je výslovná voľba. MAKRIVO neposkytuje cloudový účet ani neuchováva cloudové prihlasovacie údaje. Oblasť oznámení teraz používa vlastnú tyrkysovú ikonu M MAKRIVO a pri pozastavení zobrazuje jantárovú značku pauzy.

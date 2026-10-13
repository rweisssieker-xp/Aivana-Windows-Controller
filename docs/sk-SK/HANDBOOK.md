<!-- generated-doc locale=sk-SK source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../HANDBOOK.md)

# MAKRIVO — Používateľská príručka

Výrobca: **Aivana**. Stav dokumentácie: **2026-10-12**. Táto príručka opisuje aktuálnu implementáciu v pracovnom strome. Experimentálne integrácie a funkcie závislé od hardvéru sú výslovne označené. Historické správy opisujú stav k uvedenému dátumu, nie záruku pre novšie zostavenie.

## Dokumentácia a jazyky

Voličom jazyka dokumentácie otvorte úplnú príručku, súpis funkcií, riešenie problémov, vývojové poznámky, prieskum a historické testovacie správy v ktoromkoľvek z 27 jazykov aplikácie. Generované preklady sú strojovo podporované a štrukturálne kontrolované; nie všetky prešli revíziou rodeného hovoriaceho. Technické identifikátory a spustiteľné príklady zostávajú nezmenené. Pôvodný zdroj je k dispozícii na porovnanie. Preložená analýza licencií je informatívna a nenahrádza pôvodné právne podmienky tretích strán ani konečnú zmluvu EULA.

## Inštalácia a prvé spustenie

Počiatočnou cieľovou platformou je Windows 11 x64. Uchovávajte všetky súbory a podadresáre distribúcie spolu. Spustite `MAKRIVO.exe`. Pracovná kópia zdrojového repozitára nie je inštalačný program. Samostatné balíčky obsahujú runtime prostredie .NET; balíčky závislé od frameworku vyžadujú príslušné runtime prostredia .NET 10 Desktop a ASP.NET. Pre zodpovedajúce funkcie musia byť pribalené ONNX a sprievodní hostitelia.

1. Spustite MAKRIVO a vyberte jazyk rozhrania v Nastaveniach. Automaticky nasleduje Windows. Rozloženie klávesnice je samostatné nastavenie.
2. Vyberte si profil a potom pridajte dlaždicu cez katalóg akcií s možnosťou vyhľadávania. Dajte mu rozpoznateľný názov a vyberte jeho akcie.
3. Skontrolujte kroky a v prípade potreby priraďte skratku. Vyhnite sa kombináciám, ktoré už používa Windows alebo vaša cieľová aplikácia.
4. Uložte koncept. Neuložené zmeny nie sú uloženou konfiguráciou.
5. Otestujte pomocou neškodnej akcie, ako je otvorenie dokumentu, a skontrolujte spätnú väzbu na vykonanie.

Zatvorenie hlavného okna zvyčajne ponechá aplikáciu v oblasti oznámení. Dvojklikom na jej ikonu okno znova otvoríte alebo aplikáciu ukončite z ponuky ikony. Spúšťanie s Windows je voliteľné a spočiatku zakázané. Ovládanie pauzy pozastaví väzby; ikona zobrazí značku pozastavenia. Bežiace makrá zrušte v rozhraní, ponuke ikony alebo nastavenou skratkou na zrušenie; predvolená je `Ctrl+Alt+Escape`.

## Dlaždice, akcie a paleta príkazov

Dlaždice patria k profilom a môžu obsahovať viacero usporiadaných krokov. Editor podporuje uloženie/zahodenie, náhľad, vrátenie/znovu a zmenu poradia krokov. Katalóg poskytuje predvoľby pre programy, dokumenty, adresy URL, text, schránku, skratky, médiá, zvuk, okná, systémové akcie a podporované integrácie. Predvoľba vyžadujúca parametre sa otvorí ako koncept.

Otvorte paletu pomocou `Ctrl+K`, keď má MAKRIVO fokus. Vyhľadávajte uložené akcie, predvoľby, prepínanie profilov a nástroje. Šípky vyberajú položku; Enter ju aktivuje; `Ctrl+Enter` upravuje podporovanú položku; Escape paletu zavrie. Niektoré hotové príkazy sa vykonávajú ihneď, zatiaľ čo nakonfigurované akcie vyžadujú kontrolu. Možno nastaviť samostatnú globálnu skratku palety; nové konfigurácie používajú `Ctrl+Shift+Space`.

Podpora krokov textu a schránky `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` a `{{user}}`. Formátovanie dátumu je `yyyy-MM-dd`; formátovanie času je `HH:mm`. Neznáme premenné zostávajú nezmenené. Čítanie zo schránky prebieha iba na požiadanie. Akcia schránky zapíše text bez jeho automatického prilepenia.

Akcie spustené z rozhrania môžu cieliť na poslednú externú aplikáciu. Ak cieľ zmizne alebo Windows zablokuje jeho aktiváciu v popredí, aktivujte ho ručne a skúste to znova. Úspešné doručenie stlačenia klávesu nedokazuje, že cieľ dokončil úlohu.

## Profily a nastavenie pracoviska

Profily môžu zodpovedať názvu procesu a časti názvu okna. Ak sú nastavené obe kritériá, musia platiť obe. Poradie profilov rozhoduje pri viacerých zhodách; predvolený profil sa použije, ak žiadna zhoda nie je. Automatický výber výslovne povoľte. Zámok profilu bráni automatickým zmenám v aktuálnej relácii.

Otvorte nastavenie pracoviska z ponuky Nástroje alebo palety. Vyberte programy, dokumenty, weby, cieľové zvukové zariadenie/displej alebo scénu OBS. Sprievodca pripraví profily pracoviska, schôdze, streamovania, výskumu alebo projektu. Pred uložením skontrolujte vygenerované kroky. Nastavenie pripravené akcie nevykonáva.

## Makrá, podmienky a nahrávanie

Makrá vykonávajú svoje kroky v poradí a podporujú čakania, opakovania a zrušenie. Všeobecný limit opakovania je 100. Pred spustením akcií, ktoré píšu do inej aplikácie alebo menia stav systému, použite Ukážku.

Wait for Target Window prijíma proces, časť názvu a časový limit od 1 do 600000 ms. Vyžaduje sa aspoň jeden filter. Zodpovedajúce viditeľné okno musí reagovať a prejsť do popredia, kým budú pokračovať ďalšie kroky. Pripravené okno nedokazuje, že načítavanie dokumentu alebo služby skončilo.

Editor pracovných postupov pridáva podmienky existencie súboru, bežiaceho procesu a názvu okna. Nepravdivá podmienka preskočí krok a môže preskočiť ďalšie nasledujúce kroky. Negáciou podmienky vyjadríte alternatívnu vetvu. Vedome zvoľte zastavenie alebo pokračovanie pri chybe; pri pokračovaní môžu neskorším krokom chýbať očakávané predpoklady.

Záznamník akcií zachytáva kombinácie klávesových skratiek a prestávky, nie bežné písanie. Podporuje skratky s modifikačnými klávesmi a funkčné klávesy. Escape záznam zastaví. Skontrolujte zoznam, prevezmite ho ako neuložený koncept dlaždice a potom ho výslovne uložte. Záznam kliknutí v používateľskom rozhraní je samostatný nástroj opísaný nižšie.

## Klávesnice, gestá a vrstvy

Stránka s klávesnicou uvádza rozpoznané rozhrania a referenčné rozloženia. Jedna fyzická klávesnica môže odhaliť niekoľko rozhraní. Vyberte správny jazyk a geometriu; samotný názov produktu neidentifikuje rozloženie ISO alebo ANSI.

Štandardné globálne klávesové skratky platia naprieč klávesnicami. Väzby Raw Input pre konkrétne zariadenie sledujú dané zariadenie a skenovací kód vrátane podporovaných funkčných a multimediálnych klávesov. **Raw Input nepotláča bežný vstup Windows. Výhradné použitie druhej klávesnice nie je pripravené na produkčnú prevádzku.** Stále je potrebný podpísaný, licencovaný a otestovaný backend filtrovacieho ovládača; MAKRIVO žiadny taký ovládač neinštaluje.

Podporované väzby špeciálnych klávesov môžu používať okamžité stlačenie, krátke stlačenie, podržanie alebo dvojité stlačenie. Klávesy aktivujúce vrstvu pri podržaní dočasne vyberú iné mapovanie a po uvoľnení obnovia základnú vrstvu; klávesy výberu vrstvy nespúšťajú vlastné makro. Prahové hodnoty podržania sú od 150 do 5000 ms a časové okná dvojitého stlačenia od 150 do 1000 ms. Tieto mapovania sa líšia od bežných globálnych klávesových skratiek.

Sprievodca špeciálnymi klávesmi zisťuje kompatibilné klávesy bez vykonávania ich priradených akcií počas zachytávania. Diagnostika vysvetľuje nedostupné zariadenia, pozastavené väzby, nezhody profilov a konflikty. Prekrytie zobrazuje vybrané rozloženie a väzby; predvolená skratka na zobrazenie pri podržaní je `Ctrl+Alt+F1`.

| integrácia | Požiadavky a obmedzenia |
| --- | --- |
| Corsair | Spustený iCUE, SDK prístup a kompatibilný model. G1–G6 a podporované funkcie RGB; proprietárne ovládacie prvky Fn, kolieska, zámku a profilu nie sú vo všeobecnosti odhalené pomocou G-key API. |
| Logitech G | Pôvodné lokálne x64 G-key/LED SDK knižnice vyžadujú výslovné schválenie. Udalosti G-key neidentifikujú jednoznačne niekoľko rovnakých fyzických klávesníc. |
| Razer | Chroma RGB a štandardný vstup; vlastné ovládacie prvky mapy v Synapse tam, kde je to potrebné. |
| SteelSeries | GameSense RGB a štandardný vstup; mapovať proprietárne ovládacie prvky v GG tam, kde je to potrebné. |
| Wooting | Kompatibilné pôvodné lokálne x64 SDK pre analógové hodnoty a prahové hodnoty; normálny HID vstup zostáva aktívny. |
| Ostatné klávesnice | Štandardné klávesové skratky alebo podporované fyzické väzby skenovacieho kódu. Fn a proprietárne ovládacie prvky závisia od modelu a konfigurácie dodávateľa. |

Prieskum výrobcov opisuje zdokumentované možnosti, nie implementovanú alebo otestovanú kompatibilitu s každým uvedeným zariadením. Nové adaptéry stále vyžadujú akceptačné testy na skutočnom hardvéri. Nesťahujte binárne súbory SDK z nedôveryhodných zdrojov; distribučné práva SDK sú oddelené od technickej kompatibility.

## Zvuk, displeje a OBS

Zvukové kroky nastavujú hlasitosť od 0 do 100 percent a stlmujú, obnovujú zvuk alebo prepínajú stlmenie podporovaných výstupných či mikrofónových koncových bodov. Vyberte konkrétny koncový bod alebo použite predvolené zariadenie Windows. Chýbajúci uložený cieľ spôsobí chybu namiesto tichého ovládania iného zariadenia. Po opätovnom pripojení obnovte zoznam zariadení. Ovládanie koncových bodov neznamená podporu nezávislého zmiešavača pre jednotlivé aplikácie.

Akcie displeja zvýšia/znížia jas o 10 percentuálnych bodov alebo nastavia hodnotu od 0 do 100. Interné displeje používajú podporované rozhrania WMI; externé monitory vyžadujú podporu DDC/CI a zvyčajne jej zapnutie v ponuke monitora. Doky, adaptéry a káble môžu ovládanie blokovať. Vyberte všetky podporované displeje alebo jeden monitor. Opätovné pripojenie môže zmeniť dostupné identifikátory; skontrolujte uložený cieľ. Chýbajúce odpočty sa zobrazujú ako nedostupné, nie ako namerané hodnoty.

OBS vyžaduje povolený WebSocket v5 v ponuke Nástroje OBS. Pred uložením nakonfigurujte koncový bod, otestujte pripojenie a načítajte názvy scén/vstupov. Podporované akcie zahŕňajú prepínanie scén, spustenie/zastavenie/prepnutie streamovania či nahrávania, pozastavenie/obnovenie nahrávania a stlmenie vstupu. Heslá možno ukladať do Windows Credential Manager a sú vylúčené z exportov konfigurácie. Úspešný príkaz OBS nepotvrdzuje všetky nadväzujúce účinky streamovania.

## Overenie výsledkov a história vykonávania

Pri vhodných krokoch zapnite overenie výsledku. Aplikácia rozlišuje predbežné kontroly, potvrdené účinky, zlyhania, zrušenie a účinky, ktoré nemožno overiť. Existencia procesu neznamená pripravenosť aplikácie. Relatívnym zmenám, skratkám a zadávaniu textu nemožno paušálne priznať potvrdený úspech. Keď akcia zlyhá, skontrolujte denník aktivít a históriu overenia. Pred opätovným spustením celého makra opravte chyby konfigurácie.

## Automatizácia používateľského rozhrania a nahrávanie kliknutí

Pridajte krok UI Automation, vyberte operáciu a po zobrazenej prestávke zachyťte cieľ pod ukazovateľom. Samotné zachytenie nič nevykoná. Skontrolujte selektor a vyžadujte jednoznačnú zhodu. Podporované operácie zahŕňajú `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` a `WaitState`. Polia pre heslá, zakázané ovládacie prvky a nejednoznačné selektory sa odmietajú.

Automatizácia používateľského rozhrania prebieha v obmedzenom sprievodnom procese. Závisí to od podpory a povolení prístupnosti Windows cieľovej aplikácie. Záznamník kliknutí zachytáva podporované akcie a pauzy; nezaznamenáva všeobecný textový vstup. Pred prijatím skontrolujte a overte jeho návrh. Zmeny používateľského rozhrania v externej aplikácii môžu zrušiť platnosť selektorov. Izolácia procesov obmedzuje zlyhania a zrušenie, ale nie je izolovaným priestorom zabezpečenia.

## Pluginy

Otvorte Rozšírenia a pred importom skontrolujte balíček. Kontroluje sa štruktúra balíčka, manifest a hashe. Importovaný balíček je spočiatku zakázaný. Pred povolením si prečítajte deklarované akcie a výslovne označte vydavateľa ako dôveryhodného. Doplnky bežia s vašimi používateľskými oprávneniami v samostatnom procese; tento proces **nie je sandbox**. Inštalujte iba balíčky, ktorým dôverujete.

Správca podporuje export, zakázanie, odinštalovanie a obnovenie/zahodenie predchádzajúcej verzie. Chýbajúce závislosti blokujú vyvolanie. Ukážkový balíček poskytuje akcie odpočítavania a lokálneho stavu. Podporovaná schéma je zdokumentovanou podmnožinou JSON-Schema, nie všetkými možnými funkciami JSON-Schema. Verejné trhovisko doplnkov neexistuje.

## Mobilný panel

1. Otvorte mobilný panel, vyberte lokálnu IP a explicitne vyberte profily, ktoré chcete zobraziť. Predvolená adresa spätnej slučky je dostupná len na samotnom PC; vyberte príslušnú adresu LAN pre telefón alebo tablet.
2. Spustite panel a prečítajte si jeho HTTPS adresu, odtlačok certifikátu a jednorazový šesťmiestny párovací kód.
3. Na zariadení v rovnakej dôveryhodnej sieti otvorte adresu. Porovnajte odtlačok prsta SHA-256 prostredníctvom dôveryhodného kanála a až potom dôverujte lokálnemu certifikátu. Aplikácia nenainštaluje dôveryhodný certifikát automaticky.
4. Spárujte pomocou zobrazeného kódu a otestujte jednu neškodnú dlaždicu. Pre prístup k sieti LAN môže byť potrebné manuálne povolenie brány firewall Windows. Nevystavujte port internetu.
5. Po dokončení zastavte panel alebo zrušte spárovaných klientov prostredníctvom aplikácie.

Kľúče súkromného servera sú chránené pomocou Windows DPAPI; prihlasovacie údaje klienta sú uložené ako hodnoty hash. Zmena IP alebo obnovenie certifikátu môže vyžadovať obnovenie dôvery. Panel zobrazuje akcie a stavy overenia, nie každý možný aktívny senzor. Rozloženie prehliadača a lokálne kontroly HTTPS nenahrádzajú skutočné akceptovanie siete LAN telefónu/tabletu.

## Miestny asistent stretnutia AI

Tento voliteľný experimentálny asistent je predvolene zakázaný. Otvorte ho z ponuky Nástroje, výslovne ho povoľte, vyberte runtime prostredie/model a pridajte iba prostriedky, ktoré má zohľadniť. Zadajte cieľ, vytvorte koncept, skontrolujte nevyriešené požiadavky a preneste výsledok do bežného editora. Nič sa nevykonáva automaticky; uloženie a spustenie zostávajú výslovnými akciami. Neexistuje automatické prepnutie na náhradné runtime prostredie.

| Runtime | Nastavenie |
| --- | --- |
| Ollama | Samostatná lokálna služba a lokálne nainštalovaný model na `127.0.0.1:11434`. Deklarované modely na diaľku/cloud sú odmietnuté. |
| LM Studio | Lokálny server s lokálne načítaným modelom a podporou schémy JSON na `127.0.0.1:1234`. Nepovoľujte vzdialené smerovanie pre lokálne spracovanie. |
| llama.cpp | Lokálny `llama-server` a kompatibilný model s podporou schémy JSON na `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Dôveryhodný kompatibilný lokálny priečinok textového modelu GenAI; vyberte `genai_config.json`. Inferencia používa združený sprievodný proces bez HTTP. |

Klienti HTTP používajú iba spätnú slučku, nepoužívajú proxy a nenasledujú presmerovania. MAKRIVO nedokáže spoľahlivo zistiť, či kompatibilný lokálny server interne volá vzdialenú službu. Lokálne servery vyžadujúce overenie nemožno v tejto verzii konfigurovať. Váhy modelov, Python ani lokálne inferenčné servery sa automaticky nesťahujú ani neinštalujú.

Balíčky ONNX potrebujú `tokenizer.json` a platnú šablónu chatu. Podporované podoby šablón v `tokenizer_config.json` sú reťazec `chat_template`, slovník s reťazcom `default` alebo jednoznačný zoznam pomenovaných/predvolených šablón. Ak šablóna chýba alebo má hodnotu null, ako náhradné riešenie sa použije `chat_template.jinja`. Prázdne, nejednoznačné, príliš veľké šablóny alebo šablóny nesprávneho typu sa odmietajú. Ľubovoľné súbory ONNX, embeddingové/vizuálne modely a balíčky určené iba pre GPU nie sú zameniteľné s kompatibilnými textovými modelmi GenAI. Pribalené runtime prostredie GenAI má verziu 0.17.1 a používa CPU. Používajte iba dôveryhodné modely; oddelenie procesov nie je sandbox.

Asistent podporuje vybrané programy bez argumentov, vybrané dokumenty a weby a stlmenie mikrofónu na výslovné vyžiadanie. Nevytvára správy, schôdze, ľubovoľné skripty, ľubovoľné kliknutia, sťahovanie ani prepínanie náhlavných súprav. Cieľ a názvy prostriedkov sa odosielajú do vybraného lokálneho runtime prostredia; cesty k prostriedkom a obsah súborov sa neposkytujú osobitne. Cieľ napísaný používateľom môže sám obsahovať osobné údaje. Za vlastné sieťové správanie lokálneho runtime prostredia zodpovedá používateľ.

Limit požiadavky ONNX je 90 sekúnd vrátane načítania modelu, najviac 3072 tokenov vstupu a 1024 vygenerovaných tokenov, navyše obmedzených kontextom modelu. Chybne vytvorený/skrátený JSON a neznáme ID prostriedkov sa odmietajú. Zrušením alebo zatvorením požiadavku ukončíte. Test runtime prostredia s náhodným modelom preukazuje vykonanie, nie užitočnú kvalitu plánovania alebo úsporu času. Lokálne denníky experimentov obsahujú identifikátory udalostí a časové údaje, nie text cieľa či obsah dokumentov; automaticky sa neodosielajú.

Neskoršie vyhodnotenie malého modelu Instruct poskytlo nula platných plánov v štyroch nemeckých/anglických pokusoch. Natívna inferencia fungovala, ale model nesplnil prísny kontrakt plánovania. Ide o negatívny výsledok hodnotenia kvality, nie o odporúčaný model ani preukázateľne užitočného asistenta. Presnú revíziu modelu a obmedzenia uvádza datovaná správa o teste lokálneho runtime prostredia.

## Import, export, synchronizácia a zálohovanie

Pred nahradením konfigurácie skontrolujte náhľad importu. Importované akcie sú spočiatku zakázané a prenosná synchronizácia odstráni väzby špecifické pre hardvér. Po kontrole zámerne znova priraďte zariadenia a povoľte akcie. Pred zdieľaním exportu skontrolujte text a obsah akcií; vylúčenie hesiel neznamená, že všetok obsah používateľských akcií nie je citlivý.

Synchronizácia profilov vymieňa súbory vo vybranom priečinku, prípadne spravovanom vaším existujúcim cloudovým klientom. MAKRIVO neposkytuje cloudový účet a neukladá cloudové prihlasovacie údaje. Revízie zabraňujú tichému prepísaniu. Zahrnutie obsahu akcií je výslovná voľba. Pred importom nahrádzajúcim konfiguráciu ju zálohujte. Predvolená konfigurácia je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; historický názov adresára je zachovaný kvôli kompatibilite. Pred ručným kopírovaním/obnovením aplikáciu zatvorte. Uložené schválenia SDK a prihlasovacie údaje chránené operačným systémom môže byť potrebné na inom PC vytvoriť znova.

## Riešenie problémov

| Symptóm | Čo skontrolovať |
| --- | --- |
| Skratka nerobí nič | Aplikácia je spustená, väzby nie sú pozastavené, správny aktívny profil, uložená/povolená dlaždica, konflikty skratiek a dostupný cieľ. |
| Cieľové okno sa nedá aktivovať | Aktivujte ho manuálne. Windows obmedzenia popredia a rozdiely privilégií môžu blokovať automatizáciu. |
| Špeciálny kľúč nebol rozpoznaný | Podpora služieb predajcu/SDK/modelov; nakonfigurujte proprietárne ovládacie prvky na vysielanie podporovaného štandardného kľúča, ak je to možné. |
| Druhá klávesnica tiež píše normálne | Očakáva sa so Raw Input. Výhradné potlačenie nie je implementované pre výrobu. |
| Zvuk alebo jas nie sú k dispozícii | Obnovovacie zariadenia; skontrolujte vybranú identitu, relácie koncového bodu, podporu WMI/DDC/CI a obmedzenia doku/kábla. |
| OBS offline | WebSocket v5 povolené, správny koncový bod/heslo, dostupná služba a platné názvy scén/vstupov. |
| Automatizácia používateľského rozhrania zlyhá | Vrátane sprievodného hostiteľa, jedinečný platný selektor, prístupný povolený cieľ, podporované ovládanie a povolenia. Skúste prechodný časový limit zopakovať až po kontrole cieľa. |
| Mobilný panel je nedostupný | Namiesto spätnej adresy LAN, rovnaká sieť, spustený server, schválené profily, odtlačok certifikátu/dôvera a manuálne povolenia brány firewall. |
| Plugin zlyhá | Integrita balíka, explicitný stav dôveryhodnosti/povolené, závislosti, kompatibilita protokolov a protokol vykonávania. |
| Žiadosť AI zlyhá | Opravte vybraný lokálny runtime, pripravený na službu/model, podporovanú schému JSON, priečinok dôveryhodného kompatibilného modelu a limity požiadaviek. |
| Rozloženie alebo text sa zdá byť nesprávny | Oddeľte jazyk používateľského rozhrania od geometrie klávesnice; zmeniť veľkosť okna, skontrolovať mierku zobrazenia a nahlásiť presnú stránku/jazyk/zostavu. |

Pre užitočnú správu o chybe uveďte verziu Windows, zostavenie/potvrdenie aplikácie, jazyk, škálovanie zobrazenia, model hardvéru, minimálnu sekvenciu a príslušný redigovaný denník. Pred zverejnením odstráňte osobné cesty, obsah akcií a poverenia.

## Stavba, testovanie a balenie

Použite súpravu SDK pripnutú v `global.json`. Z koreňového adresára úložiska:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Integračné testy Windows môžu otvárať izolované okná a dočasne konfigurovať/uvoľňovať podporované väzby výrobcov. Spúšťajte ich bez iného ovládača, ktorý tieto väzby vlastní. Predvolené sondy jasu/zvuku sú iba na čítanie; výslovné hardvérové testy zmeny a obnovenia stavu sú samostatné. Simulované protokoly, vykreslené snímky obrazovky a náhodné inferenčné grafy sa nesmú vydávať za skutočnú akceptáciu hardvéru alebo kvality modelu.

Kompletný publikačný skript zostaví sprievodných hostiteľov, mobilné prostriedky, ukážkový doplnok, runtime prostredie ONNX a dostupné oznámenia o závislostiach. Zachovajte jeho podadresáre. Úspešné zostavenie nie je podpísaný inštalačný program ani právne schválenie vydania. Zdrojové súbory a zostavenia sa môžu vyvíjať oddelene; porovnajte snímku dokumentácie a commit s nainštalovanou verziou.

## Licencovanie, súkromie a stav vydania

Zamýšľaný obchodný model je jednorazový nákup s trvalým používaním zakúpenej hlavnej verzie; konečná cena, počet povolených zariadení a podmienky upgradu musia byť stanovené v skutočných podmienkach predaja. Táto príručka nedokladá produkčný systém nákupu/aktivácie. Na každodenné ovládanie sa nevyžaduje cloudový účet.

Nešírte proprietárne binárne súbory SDK bez doloženého povolenia. Zachovajte licenčné oznámenia a oznámenia tretích strán pre každú dodávanú závislosť vrátane balíčkov .NET a ONNX. Váhy modelov majú vlastné podmienky. Datovaný licenčný audit predchádza niektorým novým závislostiam a nie je povolením pre tieto dodatky. Verejný repozitár sám osebe neudeľuje všeobecnú open-source licenciu.

Skutočný hardvér výrobcov, prevádzka telefónu/tabletu v sieti LAN, prechody medzi rôznymi monitormi/DPI, dlhodobé používanie, úplná jazyková revízia, výhradná podpora pomocou ovládača a užitočná kvalita plánovania AI stále vyžadujú samostatné akceptačné testy. Nikdy nevyvodzujte schválenie iba z prítomnosti funkcie.

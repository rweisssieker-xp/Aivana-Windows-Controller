<!-- generated-doc locale=sk-SK source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../HANDBOOK.md)

# MAKRIVO — Používateľská príručka

Výrobca: **Aivana**. Snímka dokumentácie: **2026-10-12**. Táto príručka popisuje súčasnú implementáciu pracovného stromu. Experimentálne integrácie a hardvérovo závislé funkcie sú explicitne identifikované. Historické správy popisujú ich dátumovú snímku, nie sú zárukou pre novšiu zostavu.

## Dokumentácia a jazyky

Pomocou voliča jazyka dokumentácie otvorte kompletnú príručku, zoznam funkcií, riešenie problémov, poznámky k vývoju, správy o výskume a historických testoch v ktoromkoľvek z aplikačných jazykov 27. Generované preklady sú podporované strojom a majú štrukturálne kontroly; nie všetky dostali recenziu rodeným hovorcom. Technické identifikátory a spustiteľné príklady zostávajú nezmenené. Pôvodný zdroj zostáva k dispozícii na porovnanie. Preložená analýza licencií je informatívna a nenahrádza pôvodné právne podmienky tretích strán ani konečnú zmluvu EULA.

## Inštalácia a prvé spustenie

Počiatočný cieľ je Windows 11 x64. Udržujte každý súbor a podadresár distribúcie spolu. Spustite `MAKRIVO.exe`. Pokladňa zdroja nie je inštalačný program. Samostatné balíky zahŕňajú ich .NET runtime; balíky závislé od rámca vyžadujú príslušné .NET 10 Desktop a ASP.NET runtime. ONNX a sprievodní hostitelia musia byť zahrnutí pre ich príslušné funkcie.

1. Spustite MAKRIVO a vyberte jazyk rozhrania v Nastaveniach. Automaticky nasleduje Windows. Rozloženie klávesnice je samostatné nastavenie.
2. Vyberte si profil a potom pridajte dlaždicu cez katalóg akcií s možnosťou vyhľadávania. Dajte mu rozpoznateľný názov a vyberte jeho akcie.
3. Skontrolujte kroky a v prípade potreby priraďte skratku. Vyhnite sa kombináciám, ktoré už používa Windows alebo vaša cieľová aplikácia.
4. Uložte koncept. Neuložené zmeny nie sú uloženou konfiguráciou.
5. Otestujte pomocou neškodnej akcie, ako je otvorenie dokumentu, a skontrolujte spätnú väzbu na vykonanie.

Zatvorením hlavného okna normálne zostane aplikácia v oblasti oznámení. Dvakrát kliknite na ikonu na paneli, aby ste ho znova otvorili, alebo na ukončenie použite ponuku na paneli. Spustenie od Windows je voliteľné a pôvodne vypnuté. Ovládací prvok pauzy pozastaví väzby; ikona zobrazuje odznak pozastavenia. Zrušenie spustených makier cez používateľské rozhranie, zásobník alebo nakonfigurovanú skratku zrušenia; predvolená hodnota je `Ctrl+Alt+Escape`.

## Dlaždice, akcie a paleta príkazov

Dlaždice patria k profilom a môžu obsahovať viacero usporiadaných krokov. Editor podporuje uloženie/zahodenie, náhľad, vrátenie/znovu a zmenu poradia krokov. Katalóg poskytuje predvoľby pre programy, dokumenty, adresy URL, text, schránku, skratky, médiá, zvuk, okná, systémové akcie a podporované integrácie. Predvoľba vyžadujúca parametre sa otvorí ako koncept.

Otvorte paletu pomocou `Ctrl+K`, zatiaľ čo MAKRIVO je zaostrené. Vyhľadajte uložené akcie, predvoľby, prepínanie profilov a nástroje. Výber klávesov so šípkami; Enter aktivuje; `Ctrl+Enter` upravuje podporovanú položku; Útek sa zatvorí. Niektoré príkazy pripravené na použitie sa vykonajú okamžite, zatiaľ čo nakonfigurované akcie vyžadujú kontrolu. Je možné nakonfigurovať samostatnú skratku globálnej palety; nové konfigurácie používajú `Ctrl+Shift+Space`.

Podpora krokov textu a schránky `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` a `{{user}}`. Formátovanie dátumu je `yyyy-MM-dd`; formátovanie času je `HH:mm`. Neznáme premenné zostávajú nezmenené. Čítanie zo schránky prebieha iba na požiadanie. Akcia schránky zapíše text bez jeho automatického prilepenia.

Akcie spustené z používateľského rozhrania môžu byť zamerané na poslednú externú aplikáciu. Ak cieľ zmizne alebo Windows blokuje aktiváciu popredia, manuálne aktivujte cieľ a skúste to znova. Úspešné doručenie kľúča nedokazuje, že cieľ splnil svoju úlohu.

## Profily a nastavenie pracoviska

Profily sa môžu zhodovať s názvom procesu a časťou názvu okna. Keď sú nastavené obe, musia sa zhodovať obe. Poradie profilu rieši súťažné zápasy; predvolený profil je záložný. Explicitne povoliť automatický výber. Lock Profile zabraňuje automatickým zmenám pre aktuálnu reláciu.

Otvorte Nastavenie pracoviska z Nástroje alebo palety. Vyberte programy, dokumenty, webové stránky, cieľ zvuku/zobrazenia alebo scénu OBS. Asistent pripravuje profily pracoviska, stretnutia, streamingu, výskumu alebo projektu. Pred uložením skontrolujte vygenerované kroky. Inštalátor nevykoná pripravené akcie.

## Makrá, podmienky a nahrávanie

Makrá vykonávajú svoje kroky v poradí a podporujú čakania, opakovania a zrušenie. Všeobecný limit opakovania je 100. Pred spustením akcií, ktoré píšu do inej aplikácie alebo menia stav systému, použite Ukážku.

Wait for Target Window akceptuje proces, podreťazec názvu a časový limit od 1 do 600,000 ms. Vyžaduje sa aspoň jeden filter. Zodpovedajúce viditeľné okno musí reagovať a stať sa popredím, kým budú pokračovať nasledujúce kroky. Pripravené okno nedokazuje, že dokument alebo služba bola dokončená.

Editor pracovného toku pridáva podmienky existencie súboru, spustenia procesu a názvu okna. Nesprávna podmienka preskočí krok a môže preskočiť ďalšie kroky vpred. Negujte podmienku na vyjadrenie alternatívnej vetvy. Zámerne vyberte stop-on-error alebo continue-on-error; pokračovanie môže ponechať neskoršie kroky bez ich očakávaných predpokladov.

Akčný rekordér zachytáva akordy skratiek a čaká, nie obyčajné písanie. Podporuje modifikačné skratky a funkčné klávesy. Escape zastaví nahrávanie. Skontrolujte zoznam a prijmite ho ako neuložený koncept dlaždíc a potom ho explicitne uložte. Zaznamenávanie kliknutí používateľského rozhrania je samostatný nástroj popísaný nižšie.

## Klávesnice, gestá a vrstvy

Stránka s klávesnicou uvádza rozpoznané rozhrania a referenčné rozloženia. Jedna fyzická klávesnica môže odhaliť niekoľko rozhraní. Vyberte správny jazyk a geometriu; samotný názov produktu neidentifikuje rozloženie ISO alebo ANSI.

Štandardné globálne klávesové skratky platia pre všetky klávesnice. Väzby Raw Input špecifické pre zariadenie sledujú konkrétne zariadenie a skenujú kód vrátane podporovaných funkčných a mediálnych kláves. **Raw Input nepotláča bežný vstup Windows. Exkluzívna druhá klávesnica nie je pripravená na výrobu.** Stále je potrebný podpísaný, licencovaný a testovaný backend ovládača filtra; žiadny nie je nainštalovaný pomocou MAKRIVO.

Podporované špeciálne klávesové väzby môžu využívať okamžité stlačenie, ťuknutie, podržanie alebo dvojité ťuknutie. Klávesy podržaných vrstiev dočasne vyberú iné mapovanie a obnovia základnú vrstvu pri uvoľnení; klávesy pre výber vrstiev nespúšťajú svoje vlastné makro. Hranice podržania sa pohybujú od 150 do 5,000 ms; dvojité poklepanie okien od 150 do 1,000 ms. Tieto mapovania sa líšia od bežných globálnych klávesových skratiek.

Sprievodca špeciálnymi kľúčmi zisťuje kompatibilné kľúče bez vykonania ich priradených akcií počas zachytávania. Diagnostika vysvetľuje nedostupné zariadenia, pozastavené väzby, nezhody profilov a konflikty. Prekrytie zobrazuje vybraté rozloženie a väzby; jeho predvolená skratka podržania je `Ctrl+Alt+F1`.

| integrácia | Požiadavky a obmedzenia |
| --- | --- |
| Corsair | Spustený iCUE, SDK prístup a kompatibilný model. G1–G6 a podporované funkcie RGB; proprietárne ovládacie prvky Fn, kolieska, zámku a profilu nie sú vo všeobecnosti odhalené pomocou G-key API. |
| Logitech G | Pôvodné lokálne x64 G-key/LED SDK knižnice vyžadujú výslovné schválenie. Udalosti G-key neidentifikujú jednoznačne niekoľko rovnakých fyzických klávesníc. |
| Razer | Chroma RGB a štandardný vstup; vlastné ovládacie prvky mapy v Synapse tam, kde je to potrebné. |
| SteelSeries | GameSense RGB a štandardný vstup; mapovať proprietárne ovládacie prvky v GG tam, kde je to potrebné. |
| Wooting | Kompatibilné pôvodné lokálne x64 SDK pre analógové hodnoty a prahové hodnoty; normálny HID vstup zostáva aktívny. |
| Ostatné klávesnice | Štandardné klávesové skratky alebo podporované fyzické väzby skenovacieho kódu. Fn a proprietárne ovládacie prvky závisia od modelu a konfigurácie dodávateľa. |

Prieskum výrobcu popisuje zdokumentované možnosti, nie implementovaná alebo testovaná kompatibilita s každým uvedeným zariadením. Nové adaptéry stále potrebujú skutočnú hardvérovú akceptáciu. Nepreberajte binárne súbory SDK z nedôveryhodných zdrojov; Distribučné práva SDK sú oddelené od technickej kompatibility.

## Zvuk, displeje a OBS

Audio kroky nastaviť objem od 0 na 100 percent a mute/unmute/toggle podporované výstupy alebo mikrofónové koncové body. Vyberte konkrétny koncový bod alebo použite Windows predvolené. chýbajúce uložené cieľ vytvára chybu namiesto tichého ovládania iného zariadenia. Odstraňovanie zariadení po prepojení. Koniec kontroly nie je nárok na nezávislú podporu mixéra na aplikáciu.

Akcie displeja zvyšujú/znižujú jas o 10 percentuálnych bodov alebo nastavte hodnotu od 0 do 100. Interné displeje používajú podporované rozhrania WMI; externé monitory vyžadujú podporu DDC/CI a zvyčajne nastavenie ponuky monitora. Doky, adaptéry a káble môžu blokovať ovládanie. Vyberte všetky podporované displeje alebo jeden monitor. Opätovné pripojenie môže zmeniť dostupné identity; skontrolujte uložený cieľ. Chýbajúce namerané hodnoty sú zobrazené ako nedostupné a nie namerané hodnoty.

OBS vyžaduje aktiváciu WebSocket v5 v nástrojoch OBS. Pred uložením nakonfigurujte koncový bod, otestujte pripojenie a načítajte názvy scén/vstupov. Podporované akcie zahŕňajú prepínanie scén, spustenie/zastavenie/prepnutie streamu/nahrávania, pozastavenie/obnovenie nahrávania a stlmenie vstupu. Heslá môžu byť uložené v Windows Credential Manager a sú vylúčené z exportov konfigurácie. Úspešný príkaz OBS nevytvára každý efekt downstream streamovania.

## Overenie výsledkov a história vykonávania

Pre vhodné kroky povoľte Overiť výsledok. Aplikácia rozlišuje predletové kontroly, potvrdené efekty, zlyhania, zrušenie a efekty, ktoré nemožno overiť. Existencia procesu nie je pripravenosť aplikácie. Relatívne zmeny, skratky a zadávanie textu nemôžu získať všeobecné vyhlásenie o úspechu. Keď akcia zlyhá, skontrolujte denník aktivít a históriu overovania. Chyby konfigurácie by mali byť opravené pred opätovným spustením celého makra.

## Automatizácia používateľského rozhrania a nahrávanie kliknutí

Pridajte krok UI Automatizácia, vyberte operáciu a zachyťte cieľ pod ukazovateľom po zobrazenom oneskorení. samotné zachytenie nefunguje nič. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` a `WaitState`. Pôvodné heslá, ovládacie prvky a nejasné selektory sú odmietnuté.

Automatizácia používateľského rozhrania prebieha v obmedzenom sprievodnom procese. Závisí to od podpory a povolení prístupnosti Windows cieľovej aplikácie. Záznamník kliknutí zachytáva podporované akcie a pauzy; nezaznamenáva všeobecný textový vstup. Pred prijatím skontrolujte a overte jeho návrh. Zmeny používateľského rozhrania v externej aplikácii môžu zrušiť platnosť selektorov. Izolácia procesov obmedzuje zlyhania a zrušenie, ale nie je izolovaným priestorom zabezpečenia.

## Pluginy

Otvorte Rozšírenia a skontrolujte balík pred jeho importovaním. Kontroluje sa štruktúra balíka, manifest a hash. Import sa spustí zakázaný. Pred povolením si prečítajte deklarované akcie a vyslovene dôverujte vydavateľovi. Pluginy bežia s vašimi používateľskými povoleniami v samostatnom procese; ten proces je **nie je sandbox**. Inštalujte iba balíky, ktorým dôverujete.

Správca podporuje export, zakázanie, odinštalovanie a obnovenie/zrušenie predchádzajúcej verzie. Vyvolanie bloku chýbajúcich závislostí. Vzorový balík poskytuje odpočítavanie a akcie miestneho stavu. Podporovaná schéma je zdokumentovaná podmnožina schémy JSON, nie každá možná funkcia schémy JSON. Neexistuje žiadny verejný trh s doplnkami.

## Mobilný panel

1. Otvorte mobilný panel, vyberte lokálnu IP a explicitne vyberte profily, ktoré chcete zobraziť. Predvolená adresa spätnej slučky je dostupná len na samotnom PC; vyberte príslušnú adresu LAN pre telefón alebo tablet.
2. Spustite panel a prečítajte si jeho HTTPS adresu, odtlačok certifikátu a jednorazový šesťmiestny párovací kód.
3. Na zariadení v rovnakej dôveryhodnej sieti otvorte adresu. Porovnajte odtlačok prsta SHA-256 prostredníctvom dôveryhodného kanála a až potom dôverujte lokálnemu certifikátu. Aplikácia nenainštaluje dôveryhodný certifikát automaticky.
4. Spárujte pomocou zobrazeného kódu a otestujte jednu neškodnú dlaždicu. Pre prístup k sieti LAN môže byť potrebné manuálne povolenie brány firewall Windows. Nevystavujte port internetu.
5. Po dokončení zastavte panel alebo zrušte spárovaných klientov prostredníctvom aplikácie.

Kľúče súkromného servera sú chránené pomocou Windows DPAPI; prihlasovacie údaje klienta sú uložené ako hodnoty hash. Zmena IP alebo obnovenie certifikátu môže vyžadovať obnovenie dôvery. Panel zobrazuje akcie a stavy overenia, nie každý možný aktívny senzor. Rozloženie prehliadača a lokálne kontroly HTTPS nenahrádzajú skutočné akceptovanie siete LAN telefónu/tabletu.

## Miestny asistent stretnutia AI

Tento voliteľný experimentálny asistent je predvolene zakázaný. Otvorte ho v nástrojoch, explicitne ho povoľte, vyberte runtime/model a pridajte len prostriedky, ktoré chcete, aby zvážil. Zadajte cieľ, vytvorte koncept, skontrolujte nevyriešené požiadavky a preneste výsledok do normálneho editora. Nič sa nevykoná automaticky; ukladanie a spustenie zostávajú explicitnými akciami. Neexistuje žiadna automatická núdzová prevádzka.

| Runtime | Nastavenie |
| --- | --- |
| Ollama | Samostatná lokálna služba a lokálne nainštalovaný model na `127.0.0.1:11434`. Deklarované modely na diaľku/cloud sú odmietnuté. |
| LM Studio | Lokálny server s lokálne načítaným modelom a podporou schémy JSON na `127.0.0.1:1234`. Nepovoľujte vzdialené smerovanie pre lokálne spracovanie. |
| llama.cpp | Lokálny `llama-server` a kompatibilný model s podporou schémy JSON na `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Dôveryhodný kompatibilný lokálny priečinok textového modelu GenAI; vyberte `genai_config.json`. Inferencia používa združený sprievodný proces bez HTTP. |

HTTP klienti používajú iba spätnú slučku, nepoužívajú proxy a nesledujú presmerovania. MAKRIVO nemôže spoľahlivo určiť, či kompatibilný lokálny server interne volá vzdialenú službu. Autentizované lokálne servery nie sú v tejto verzii konfigurovateľné. Váhy modelov, Python a lokálne inferenčné servery sa nesťahujú ani neinštalujú automaticky.

ONNX balíček potrebuje `tokenizer.json` a platný šablónový chat. podporované šablóny formulárov v `tokenizer_config.json` Je to striekačka `chat_template`Slovník s pruhom `default`, alebo nesporný názov/predvolený zoznam šablóny. ak je šablóna chýbať alebo nula, `chat_template.jinja` Používa sa ako padnutie. prázdne, nejasné, nadmerné alebo nesprávne vytlačené šablóny sú odmietnuté. ONNX súbory, modely vstavania/videnia a balíky GPU-on-one nie sú vymieňateľné s kompatibilnými GenAI Textové modely - Zväzok GenAI Runtime verzia je 0.17.1Použite iba dôveryhodné modely; procesné oddelenie nie je piesková krabička.

Asistent podporuje vybrané programy bez argumentov, vybrané dokumenty a webové stránky a na výslovnú žiadosť stlmí mikrofón. Nevytvára správy, stretnutia, ľubovoľné skripty, bezplatné kliknutia, sťahovanie ani prepínanie slúchadiel. Názvy cieľa a prostriedkov sa odošlú do vybratého lokálneho prostredia runtime; cesty k prostriedkom a obsah súboru sa nedodávajú samostatne. Cieľ zadaný používateľom môže sám o sebe obsahovať osobné údaje. Za správanie vlastnej siete lokálneho runtime zostáva zodpovedný používateľ.

Rozpočet požiadavky ONNX je 90 sekúnd vrátane načítania modelu, až po tokeny výzvy 3,072 a tokeny generované 1,024, ktoré sú tiež obmedzené kontextom modelu. Poškodený/skrátený JSON a neznáme identifikátory zdrojov sú odmietnuté. Zrušením alebo zatvorením ukončíte požiadavku. Náhodný model runtime test stanovuje realizáciu, nie užitočnú kvalitu plánovania alebo úsporu času. Lokálne denníky experimentov obsahujú identifikátory a načasovanie udalostí, nie text cieľa ani obsah dokumentu. nie sú automaticky nahrané.

Neskôr malá hodnota modelu Instrukcie vytvorila nulové platné plány v štyroch nemeckých/anglických skúškach. Native inference fungovala, ale model zlyhal prísnu plánovacie zmluvu. To je negatívny výsledok kvality, nie odporúčaný model alebo preukázaný užitočný asistent. Pozri dátumovú lokalitu testovacieho času pre presnú revíziu modelu a limity.

## Import, export, synchronizácia a zálohovanie

Pred výmenou konfigurácie skontrolujte ukážky importu. Spustenie importovaných akcií je vypnuté a prenosná synchronizácia odstráni hardvérovo špecifické väzby. Po kontrole schválne priraďte zariadenia a povoľte akcie. Pred zdieľaním exportu skontrolujte obsah textu/akcie; vylúčenie hesiel neznamená, že obsah všetkých akcií vytvorených používateľom nebude citlivý.

Synchronizácia profilu vymieňa súbory v vybranom priečinku, ktorý je opcionálne spravovaný vašim existujúcim cloudovým klientom. MAKRIVO dodáva žiadny cloud účet a ukladá žiadne cloudové overenosti. Revízie zabraňujú tichým prepisom. Zahŕňanie obsahu akcie je výslovne voľba. Zálohovať konfiguráciu pred importom náhrad. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`• historické názov adresára je zachovaný pre kompatibilitu. zatvorte aplikáciu pred manuálnou kopírovaním/obnovením. uložené schválenia SDK a OS chránené autentifikácie môžu byť potrebné na iný počítač.

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

Integračné testy Windows môžu otvárať izolované okná a dočasne konfigurovať/uvoľňovať podporované väzby dodávateľov. Spustite ich bez toho, aby tieto väzby vlastnil iný ovládač. Predvolené jasové/audio sondy sú len na čítanie; explicitné hardvérové ​​spiatočné testy sú oddelené. Falošné protokoly, vykreslené snímky obrazovky a grafy náhodných záverov nesmú byť reprezentované ako skutočné akceptovanie kvality hardvéru/modelu.

Kompletný vydavateľ zostavuje sprievodných hostiteľov, mobilné prostriedky, vzorový doplnok, runtime ONNX a dostupné upozornenia o závislosti. Ponechajte jeho podadresáre. Úspešná zostava nie je podpísaným inštalátorom ani právnym schválením vydania. Zdroj a zostavy sa môžu vyvíjať oddelene; porovnajte snímku dokumentácie a odovzdajte ju s nainštalovanou verziou.

## Licencovanie, súkromie a stav vydania

Zamýšľaný komerčný model je jednorazový nákup s trvalým používaním zakúpenej hlavnej verzie; konečná cena, povolená cena zariadenia a podmienky aktualizácie musia byť stanovené v skutočných podmienkach predaja. V tejto príručke nie je vytvorený systém nákupu/aktivácie výroby. Na každodenné ovládanie nie je potrebný žiadny cloudový účet.

Nešírte proprietárne binárne súbory SDK bez preukázaného povolenia. Zachovajte licenčné upozornenia a upozornenia tretích strán pre každú dodanú závislosť, vrátane balíkov .NET a ONNX. Modelové váhy majú svoje vlastné podmienky. Datovaný licenčný audit predchádza niektorým novým závislostiam a nie je povolením pre tieto dodatky. Verejné úložisko samo o sebe neudeľuje všeobecnú licenciu na otvorený zdroj.

Hardvér skutočného dodávateľa, prevádzka cez telefón/tablet v sieti LAN, prechody so zmiešaným monitorom/DPI, dlhodobé používanie, kompletná lingvistická kontrola, exkluzívna podpora ovládačov a užitočná kvalita plánovania AI zostávajú samostatnou akceptačnou prácou. Nikdy nevyvodzujte súhlas len z prítomnosti funkcie.

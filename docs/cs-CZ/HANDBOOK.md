<!-- generated-doc locale=cs-CZ source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../HANDBOOK.md)

# MAKRIVO — Uživatelská příručka

Výrobce: **Aivana**. Stav dokumentace: **2026-10-12**. Tato příručka popisuje aktuální implementaci v pracovním stromu. Experimentální integrace a funkce závislé na hardwaru jsou výslovně označeny. Historické zprávy popisují stav k uvedenému datu, nikoli záruku pro novější sestavení.

## Dokumentace a jazyky

Voličem jazyka dokumentace otevřete úplnou příručku, soupis funkcí, řešení problémů, vývojové poznámky, průzkum a historické testovací zprávy v kterémkoli z 27 jazyků aplikace. Generované překlady jsou strojově podporované a strukturálně kontrolované; ne všechny prošly revizí rodilého mluvčího. Technické identifikátory a spustitelné příklady zůstávají nezměněny. Původní zdroj je k dispozici pro porovnání. Přeložená analýza licencí je informativní a nenahrazuje původní právní podmínky třetích stran ani konečnou smlouvu EULA.

## Instalace a první spuštění

Počáteční cílovou platformou je Windows 11 x64. Uchovávejte všechny soubory a podadresáře distribuce pohromadě. Spusťte `MAKRIVO.exe`. Pracovní kopie zdrojového repozitáře není instalační program. Samostatné balíčky obsahují běhové prostředí .NET; balíčky závislé na frameworku vyžadují příslušná běhová prostředí .NET 10 Desktop a ASP.NET. Pro odpovídající funkce musí být přibaleny ONNX a doprovodní hostitelé.

1. Spusťte MAKRIVO a vyberte jazyk rozhraní v Nastavení. Automaticky následuje Windows. Rozložení klávesnice je samostatné nastavení.
2. Vyberte profil a poté přidejte dlaždici prostřednictvím prohledávatelného katalogu akcí. Dejte mu rozpoznatelné jméno a zvolte jeho akce.
3. Zkontrolujte kroky a v případě potřeby přiřaďte zástupce. Vyhněte se kombinacím, které již používá Windows nebo vaše cílová aplikace.
4. Uložte koncept. Neuložené změny nejsou uloženou konfigurací.
5. Otestujte pomocí neškodné akce, jako je otevření dokumentu, a zkontrolujte zpětnou vazbu k provedení.

Zavření hlavního okna obvykle ponechá aplikaci v oznamovací oblasti. Dvojklikem na její ikonu okno znovu otevřete nebo aplikaci ukončete z nabídky ikony. Spouštění s Windows je volitelné a zpočátku zakázané. Ovládání pauzy pozastaví vazby; ikona zobrazí značku pozastavení. Běžící makra zrušte v rozhraní, nabídce ikony nebo nastavenou zkratkou pro zrušení; výchozí je `Ctrl+Alt+Escape`.

## Dlaždice, akce a paleta příkazů

Dlaždice patří k profilům a mohou obsahovat více uspořádaných kroků. Editor podporuje uložení/zahození, náhled, vrácení/znovu a změnu pořadí kroků. Katalog poskytuje předvolby pro programy, dokumenty, adresy URL, text, schránku, zástupce, média, zvuk, okna, systémové akce a podporované integrace. Předvolba vyžadující parametry se otevře jako koncept.

Otevřete paletu pomocí `Ctrl+K`, když má MAKRIVO fokus. Vyhledávejte uložené akce, předvolby, přepínání profilů a nástroje. Šipky vybírají položku; Enter ji aktivuje; `Ctrl+Enter` upravuje podporovanou položku; Escape paletu zavře. Některé hotové příkazy se provádějí ihned, zatímco nakonfigurované akce vyžadují kontrolu. Lze nastavit samostatnou globální zkratku palety; nové konfigurace používají `Ctrl+Shift+Space`.

Podpora kroků textu a schránky `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` a `{{user}}`. Formátování data je `yyyy-MM-dd`; formátování času je `HH:mm`. Neznámé proměnné zůstávají nezměněny. Čtení schránky probíhá pouze na vyžádání. Akce schránky zapíše text bez jeho automatického vkládání.

Akce spuštěné z rozhraní mohou cílit na poslední externí aplikaci. Pokud cíl zmizí nebo Windows zablokuje jeho aktivaci na popředí, aktivujte jej ručně a zkuste to znovu. Úspěšné doručení stisku klávesy nedokazuje, že cíl dokončil úkol.

## Profily a nastavení pracoviště

Profily mohou odpovídat názvu procesu a části názvu okna. Pokud jsou nastavena obě kritéria, musí platit obě. Pořadí profilů rozhoduje při více shodách; výchozí profil se použije, pokud žádná shoda není. Automatický výběr výslovně povolte. Zámek profilu brání automatickým změnám v aktuální relaci.

Otevřete nastavení pracoviště z nabídky Nástroje nebo palety. Vyberte programy, dokumenty, weby, cílové zvukové zařízení/displej nebo scénu OBS. Průvodce připraví profily pracoviště, schůzky, streamování, výzkumu nebo projektu. Před uložením zkontrolujte vygenerované kroky. Nastavení připravené akce neprovádí.

## Makra, podmínky a nahrávání

Makra provádějí své kroky v pořadí a podporují čekání, opakování a zrušení. Obecný limit opakování je 100. Před spuštěním akcí, které zadávají do jiné aplikace nebo mění stav systému, použijte Náhled.

Wait for Target Window přijímá proces, část názvu a časový limit od 1 do 600000 ms. Je vyžadován alespoň jeden filtr. Odpovídající viditelné okno musí reagovat a přejít do popředí, než budou pokračovat další kroky. Připravené okno nedokazuje, že načítání dokumentu nebo služby skončilo.

Editor pracovních postupů přidává podmínky existence souboru, běžícího procesu a názvu okna. Nepravdivá podmínka přeskočí krok a může přeskočit další následující kroky. Negací podmínky vyjádříte alternativní větev. Vědomě zvolte zastavení nebo pokračování při chybě; při pokračování mohou pozdějším krokům chybět očekávané předpoklady.

Záznamník akcí zachycuje kombinace klávesových zkratek a prodlevy, nikoli běžné psaní. Podporuje zkratky s modifikačními klávesami a funkční klávesy. Escape záznam zastaví. Zkontrolujte seznam, převezměte jej jako neuložený koncept dlaždice a poté jej výslovně uložte. Záznam kliknutí v uživatelském rozhraní je samostatný nástroj popsaný níže.

## Klávesnice, gesta a vrstvy

Stránka klávesnice uvádí zjištěná rozhraní a referenční rozvržení. Jedna fyzická klávesnice může odhalit několik rozhraní. Vyberte správný jazyk a geometrii; samotný název produktu neidentifikuje rozložení ISO nebo ANSI.

Standardní globální klávesové zkratky platí napříč klávesnicemi. Vazby Raw Input pro konkrétní zařízení sledují dané zařízení a skenovací kód včetně podporovaných funkčních a multimediálních kláves. **Raw Input nepotlačuje běžný vstup Windows. Výhradní použití druhé klávesnice není připraveno pro produkční provoz.** Stále je nutný podepsaný, licencovaný a otestovaný backend filtrovacího ovladače; MAKRIVO žádný takový ovladač neinstaluje.

Podporované vazby speciálních kláves mohou používat okamžité stisknutí, krátké stisknutí, podržení nebo dvojité stisknutí. Klávesy aktivující vrstvu při podržení dočasně vyberou jiné mapování a po uvolnění obnoví základní vrstvu; klávesy výběru vrstvy nespouštějí vlastní makro. Prahové hodnoty podržení jsou od 150 do 5000 ms a časová okna dvojitého stisknutí od 150 do 1000 ms. Tato mapování se liší od běžných globálních klávesových zkratek.

Průvodce speciálními klávesami detekuje kompatibilní klávesy, aniž by během zachycení prováděl jejich přiřazené akce. Diagnostika vysvětluje nedostupná zařízení, pozastavené vazby, neshody profilů a konflikty. Překryv zobrazuje vybrané rozložení a vazby; výchozí zkratka pro zobrazení při podržení je `Ctrl+Alt+F1`.

| Integrace | Požadavky a omezení |
| --- | --- |
| Corsair | Spuštění iCUE, přístup k SDK a kompatibilní model. G1–G6 a podporované funkce RGB; proprietární ovládací prvky Fn, kolečka, zámku a profilu nejsou obecně odhaleny rozhraním G-key API. |
| Logitech G | Původní místní x64 G-key/LED SDK knihovny vyžadují výslovné schválení. Události G-key neidentifikují jednoznačně několik identických fyzických klávesnic. |
| Razer | Chroma RGB a standardní vstup; v případě potřeby mapujte vlastní ovládací prvky v Synapse. |
| SteelSeries | GameSense RGB a standardní vstup; mapovat proprietární ovládací prvky v GG, kde je potřeba. |
| Wooting | Kompatibilní originální místní x64 SDK pro analogové hodnoty a prahové hodnoty; normální HID vstup zůstává aktivní. |
| Ostatní klávesnice | Standardní klávesové zkratky nebo podporované fyzické vazby skenovacího kódu. Fn a proprietární ovládací prvky závisí na modelu a konfiguraci dodavatele. |

Průzkum výrobců popisuje zdokumentované možnosti, nikoli implementovanou nebo otestovanou kompatibilitu s každým uvedeným zařízením. Nové adaptéry stále vyžadují přejímací testy na skutečném hardwaru. Nestahujte binární soubory SDK z nedůvěryhodných zdrojů; distribuční práva SDK jsou oddělena od technické kompatibility.

## Zvuk, displeje a OBS

Zvukové kroky nastavují hlasitost od 0 do 100 procent a ztlumují, obnovují zvuk nebo přepínají ztlumení podporovaných výstupních či mikrofonních koncových bodů. Vyberte konkrétní koncový bod nebo použijte výchozí zařízení Windows. Chybějící uložený cíl způsobí chybu namísto tichého ovládání jiného zařízení. Po opětovném připojení obnovte seznam zařízení. Ovládání koncových bodů neznamená podporu nezávislého směšovače pro jednotlivé aplikace.

Akce displeje zvýší/sníží jas o 10 procentních bodů nebo nastaví hodnotu od 0 do 100. Interní displeje používají podporovaná rozhraní WMI; externí monitory vyžadují podporu DDC/CI a obvykle její zapnutí v nabídce monitoru. Doky, adaptéry a kabely mohou ovládání blokovat. Vyberte všechny podporované displeje nebo jeden monitor. Opětovné připojení může změnit dostupné identifikátory; zkontrolujte uložený cíl. Chybějící odečty se zobrazují jako nedostupné, nikoli jako naměřené hodnoty.

OBS vyžaduje povolený WebSocket v5 v nabídce Nástroje OBS. Před uložením nakonfigurujte koncový bod, otestujte připojení a načtěte názvy scén/vstupů. Podporované akce zahrnují přepínání scén, spuštění/zastavení/přepnutí streamování či nahrávání, pozastavení/obnovení nahrávání a ztlumení vstupu. Hesla lze ukládat do Windows Credential Manager a jsou vyloučena z exportů konfigurace. Úspěšný příkaz OBS nepotvrzuje všechny navazující účinky streamování.

## Ověření výsledků a historie provedení

U vhodných kroků zapněte ověření výsledku. Aplikace rozlišuje předběžné kontroly, potvrzené účinky, selhání, zrušení a účinky, které nelze ověřit. Existence procesu neznamená připravenost aplikace. Relativním změnám, zkratkám a zadávání textu nelze paušálně přiznat potvrzený úspěch. Když akce selže, zkontrolujte protokol aktivit a historii ověření. Před opětovným spuštěním celého makra opravte chyby konfigurace.

## Automatizace uživatelského rozhraní a záznam kliknutí

Přidejte krok UI Automation, vyberte operaci a po zobrazené prodlevě zachyťte cíl pod ukazatelem. Samotné zachycení nic neprovádí. Zkontrolujte selektor a vyžadujte jednoznačnou shodu. Podporované operace zahrnují `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` a `WaitState`. Pole pro hesla, zakázané ovládací prvky a nejednoznačné selektory jsou odmítnuty.

Automatizace uživatelského rozhraní běží v omezeném doprovodném procesu. Závisí to na podpoře usnadnění přístupu Windows cílové aplikace a na oprávněních. Záznamník kliknutí zachycuje podporované akce a pauzy; nezaznamenává obecný textový vstup. Zkontrolujte a ověřte jeho návrh před jeho přijetím. Změny uživatelského rozhraní v externí aplikaci mohou zneplatnit selektory. Izolace procesů omezuje pády a zrušení, ale není karanténou zabezpečení.

## Pluginy

Otevřete Rozšíření a před importem zkontrolujte balíček. Kontroluje se struktura balíčku, manifest a hashe. Importovaný balíček je zpočátku zakázán. Před povolením si přečtěte deklarované akce a výslovně označte vydavatele jako důvěryhodného. Pluginy běží s vašimi uživatelskými oprávněními v samostatném procesu; tento proces **není sandbox**. Instalujte pouze balíčky, kterým důvěřujete.

Správce podporuje export, zakázání, odinstalaci a obnovení/zahození předchozí verze. Chybějící závislosti blokují vyvolání. Ukázkový balíček poskytuje akce odpočítávání a místního stavu. Podporované schéma je zdokumentovanou podmnožinou JSON-Schema, nikoli všemi možnými funkcemi JSON-Schema. Veřejné tržiště pluginů neexistuje.

## Mobilní panel

1. Otevřete Mobile Panel, vyberte místní IP a explicitně vyberte profily, které chcete vystavit. Výchozí adresa zpětné smyčky je dostupná pouze na samotném PC; vyberte příslušnou adresu LAN pro telefon nebo tablet.
2. Spusťte panel a přečtěte si jeho HTTPS adresu, otisk certifikátu a jednorázový šestimístný párovací kód.
3. Na zařízení ve stejné důvěryhodné síti otevřete adresu. Než začnete důvěřovat místnímu certifikátu, porovnejte otisk SHA-256 prostřednictvím důvěryhodného kanálu. Aplikace neinstaluje důvěryhodný certifikát automaticky.
4. Spárujte pomocí zobrazeného kódu a vyzkoušejte jednu neškodnou destičku. Pro přístup k síti LAN může být zapotřebí ruční povolení brány firewall Windows. Nevystavujte port internetu.
5. Po dokončení zastavte panel nebo zrušte spárované klienty prostřednictvím aplikace.

Klíče soukromého serveru jsou chráněny pomocí Windows DPAPI; přihlašovací údaje klienta jsou uloženy jako hash. Změna IP nebo obnovení certifikátu může vyžadovat obnovení důvěry. Panel zobrazuje akční a ověřovací stavy, ne každý možný aktivní senzor. Rozložení prohlížeče a místní kontroly HTTPS nenahrazují skutečnou akceptaci sítě LAN telefonem/tabletem.

## Místní asistent setkání AI

Tento volitelný experimentální asistent je ve výchozím nastavení zakázán. Otevřete jej z nabídky Nástroje, výslovně jej povolte, vyberte běhové prostředí/model a přidejte jen prostředky, které má zohlednit. Zadejte cíl, vytvořte koncept, zkontrolujte nevyřešené požadavky a přeneste výsledek do běžného editoru. Nic se neprovádí automaticky; uložení a spuštění zůstávají výslovnými akcemi. Neexistuje automatické přepnutí na náhradní běhové prostředí.

| Doba běhu | Nastavení |
| --- | --- |
| Ollama | Samostatná místní služba a lokálně nainstalovaný model na `127.0.0.1:11434`. Deklarované vzdálené/cloudové modely jsou odmítnuty. |
| LM Studio | Místní server s lokálně načteným modelem a podporou schématu JSON na `127.0.0.1:1234`. Nepovolujte vzdálené směrování pro místní zpracování. |
| llama.cpp | Místní `llama-server` a kompatibilní model s podporou schématu JSON na `127.0.0.1:8080`. |
| ONNX Doba běhu GenAI CPU | Důvěryhodná kompatibilní místní složka textového modelu GenAI; vyberte `genai_config.json`. Inference používá svázaný doprovodný proces bez HTTP. |

Klienti HTTP používají pouze zpětnou smyčku, nepoužívají proxy a nenásledují přesměrování. MAKRIVO nedokáže spolehlivě zjistit, zda kompatibilní místní server interně volá vzdálenou službu. Místní servery vyžadující ověření nelze v této verzi konfigurovat. Váhy modelů, Python ani místní inferenční servery se automaticky nestahují ani neinstalují.

Balíčky ONNX potřebují `tokenizer.json` a platnou šablonu chatu. Podporované podoby šablon v `tokenizer_config.json` jsou řetězec `chat_template`, slovník s řetězcem `default` nebo jednoznačný seznam pojmenovaných/výchozích šablon. Pokud šablona chybí nebo má hodnotu null, jako náhradní řešení se použije `chat_template.jinja`. Prázdné, nejednoznačné, příliš velké šablony nebo šablony nesprávného typu jsou odmítnuty. Libovolné soubory ONNX, embeddingové/vizuální modely a balíčky určené pouze pro GPU nejsou zaměnitelné s kompatibilními textovými modely GenAI. Přibalené běhové prostředí GenAI má verzi 0.17.1 a používá CPU. Používejte pouze důvěryhodné modely; oddělení procesů není sandbox.

Asistent podporuje vybrané programy bez argumentů, vybrané dokumenty a weby a ztlumení mikrofonu na výslovné vyžádání. Nevytváří zprávy, schůzky, libovolné skripty, libovolná kliknutí, stahování ani přepínání náhlavních souprav. Cíl a názvy prostředků se odesílají do vybraného místního běhového prostředí; cesty k prostředkům a obsah souborů nejsou poskytovány zvlášť. Cíl napsaný uživatelem může sám obsahovat osobní údaje. Za vlastní síťové chování místního běhového prostředí odpovídá uživatel.

Limit požadavku ONNX je 90 sekund včetně načtení modelu, nejvýše 3072 tokenů vstupu a 1024 vygenerovaných tokenů, navíc omezených kontextem modelu. Chybně utvořený/zkrácený JSON a neznámá ID prostředků jsou odmítnuty. Zrušením nebo zavřením požadavek ukončíte. Test běhového prostředí s náhodným modelem prokazuje provedení, nikoli užitečnou kvalitu plánování nebo úsporu času. Místní protokoly experimentů obsahují identifikátory událostí a časové údaje, nikoli text cíle či obsah dokumentů; automaticky se neodesílají.

Pozdější vyhodnocení malého modelu Instruct poskytlo nula platných plánů ve čtyřech německých/anglických pokusech. Nativní inference fungovala, ale model nesplnil přísný kontrakt plánování. Jde o negativní výsledek hodnocení kvality, nikoli o doporučený model ani prokazatelně užitečného asistenta. Přesnou revizi modelu a omezení uvádí datovaná zpráva o testu místního běhového prostředí.

## Import, export, synchronizace a zálohování

Před nahrazením konfigurace zkontrolujte náhled importu. Importované akce jsou zpočátku zakázané a přenosná synchronizace odstraní vazby specifické pro hardware. Po kontrole záměrně znovu přiřaďte zařízení a povolte akce. Před sdílením exportu zkontrolujte text a obsah akcí; vyloučení hesel neznamená, že veškerý obsah uživatelských akcí není citlivý.

Synchronizace profilů vyměňuje soubory ve vybrané složce, případně spravované vaším stávajícím cloudovým klientem. MAKRIVO neposkytuje cloudový účet a neukládá cloudové přihlašovací údaje. Revize zabraňují tichému přepsání. Zahrnutí obsahu akcí je výslovná volba. Před importem nahrazujícím konfiguraci ji zálohujte. Výchozí konfigurace je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; historický název adresáře je zachován kvůli kompatibilitě. Před ručním kopírováním/obnovením aplikaci zavřete. Uložená schválení SDK a přihlašovací údaje chráněné operačním systémem může být nutné na jiném PC vytvořit znovu.

## Odstraňování problémů

| Symptom | Co zkontrolovat |
| --- | --- |
| Zkratka nic nedělá | Aplikace běží, vazby nejsou pozastaveny, správný aktivní profil, uložená/povolená dlaždice, konflikty zástupců a dostupný cíl. |
| Cílové okno nelze aktivovat | Aktivujte jej ručně. Windows omezení popředí a rozdíly v oprávněních mohou blokovat automatizaci. |
| Speciální klíč nebyl rozpoznán | Podpora služby dodavatele/SDK/modelu; nakonfigurujte proprietární ovládací prvky tak, aby vydávaly podporovaný standardní klíč, pokud je to možné. |
| Druhá klávesnice také píše normálně | Očekáváno s Raw Input. Výhradní potlačení není implementováno pro výrobu. |
| Zvuk nebo jas nejsou k dispozici | Obnovovací zařízení; zkontrolovat vybranou identitu, relace koncového bodu, podporu WMI/DDC/CI a omezení doku/kabelu. |
| OBS offline | WebSocket v5 povoleno, správný koncový bod/heslo, dostupná služba a platné názvy scén/vstupů. |
| Automatizace uživatelského rozhraní se nezdaří | Včetně doprovodného hostitele, jedinečný platný selektor, přístupný povolený cíl, podporované ovládání a oprávnění. Opakujte přechodný časový limit pouze po kontrole cíle. |
| Mobilní panel nedostupný | LAN spíše než adresa zpětné smyčky, stejná síť, běžící server, schválené profily, otisk certifikátu/důvěra a manuální oprávnění brány firewall. |
| Plugin selže | Integrita balíčku, explicitní stav důvěryhodnosti/povoleno, závislosti, kompatibilita protokolů a protokol provádění. |
| Požadavek AI se nezdaří | Opravte vybrané místní běhové prostředí, je připravena služba/model, podporované schéma JSON, důvěryhodná kompatibilní složka modelu a limity požadavků. |
| Rozložení nebo text se zdá být nesprávný | Oddělte jazyk uživatelského rozhraní od geometrie klávesnice; změnit velikost okna, zkontrolovat měřítko zobrazení a nahlásit přesnou stránku/jazyk/sestavení. |

Pro užitečnou zprávu o chybě uveďte verzi Windows, sestavení/potvrzení aplikace, jazyk, měřítko zobrazení, model hardwaru, minimální sekvenci a příslušný redigovaný protokol. Před publikováním odstraňte osobní cesty, obsah akcí a přihlašovací údaje.

## Stavba, testování a balení

Použijte sadu SDK připojenou v `global.json`. Z kořenového adresáře úložiště:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Integrační testy Windows mohou otevírat izolovaná okna a dočasně konfigurovat/uvolňovat podporované vazby výrobců. Spouštějte je bez jiného řadiče, který tyto vazby vlastní. Výchozí sondy jasu/zvuku jsou pouze pro čtení; výslovné hardwarové testy změny a obnovení stavu jsou samostatné. Simulované protokoly, vykreslené snímky obrazovky a náhodné inferenční grafy nesmějí být vydávány za skutečnou přejímku hardwaru nebo kvality modelu.

Kompletní publikační skript sestaví doprovodné hostitele, mobilní prostředky, ukázkový plugin, běhové prostředí ONNX a dostupná oznámení o závislostech. Zachovejte jeho podadresáře. Úspěšné sestavení není podepsaný instalační program ani právní schválení vydání. Zdrojové soubory a sestavení se mohou vyvíjet odděleně; porovnejte snímek dokumentace a commit s nainstalovanou verzí.

## Licencování, soukromí a stav vydání

Zamýšlený obchodní model je jednorázový nákup s trvalým používáním zakoupené hlavní verze; konečná cena, počet povolených zařízení a podmínky upgradu musí být stanoveny ve skutečných podmínkách prodeje. Tato příručka nedokládá produkční systém nákupu/aktivace. Pro každodenní ovládání není vyžadován cloudový účet.

Nešiřte proprietární binární soubory SDK bez doloženého povolení. Zachovejte licenční oznámení a oznámení třetích stran pro každou dodávanou závislost včetně balíčků .NET a ONNX. Váhy modelů mají vlastní podmínky. Datovaný licenční audit předchází některým novým závislostem a není povolením pro tyto dodatky. Veřejný repozitář sám o sobě neuděluje obecnou open-source licenci.

Skutečný hardware výrobců, provoz telefonu/tabletu v síti LAN, přechody mezi různými monitory/DPI, dlouhodobé používání, úplná jazyková revize, výhradní podpora pomocí ovladače a užitečná kvalita plánování AI stále vyžadují samostatné přejímací testy. Nikdy nevyvozujte schválení pouze z přítomnosti funkce.

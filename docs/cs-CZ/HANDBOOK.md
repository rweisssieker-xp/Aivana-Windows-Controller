<!-- generated-doc locale=cs-CZ source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../HANDBOOK.md)

# MAKRIVO — Uživatelská příručka

Výrobce: **Aivana**. Snímek dokumentace: **2026-10-12**. Tato příručka popisuje současnou implementaci pracovního stromu. Experimentální integrace a funkce závislé na hardwaru jsou explicitně identifikovány. Historické zprávy popisují jejich datovaný snímek, nejsou zárukou pro novější sestavení.

## Dokumentace a jazyky

Pomocí voliče jazyka dokumentace otevřete kompletní příručku, soupis funkcí, řešení problémů, poznámky k vývoji, zprávy o výzkumu a historických testech v kterémkoli z aplikačních jazyků 27. Generované překlady jsou strojově asistované a mají strukturální kontroly; ne všechny obdržely recenzi rodilým mluvčím. Technické identifikátory a spustitelné příklady zůstávají nezměněny. Původní zdroj zůstává k dispozici pro srovnání. Přeložená analýza licencí je informativní a nenahrazuje původní právní podmínky třetích stran ani konečnou smlouvu EULA.

## Instalace a první spuštění

Počáteční cíl je Windows 11 x64. Udržujte všechny soubory a podadresáře distribuce pohromadě. Spusťte `MAKRIVO.exe`. Pokladna zdroje není instalační program. Samostatné balíčky zahrnují jejich .NET runtime; balíčky závislé na frameworku vyžadují příslušné běhové prostředí .NET 10 Desktop a ASP.NET. ONNX a doprovodní hostitelé musí být zahrnuti pro jejich příslušné funkce.

1. Spusťte MAKRIVO a vyberte jazyk rozhraní v Nastavení. Automaticky následuje Windows. Rozložení klávesnice je samostatné nastavení.
2. Vyberte profil a poté přidejte dlaždici prostřednictvím prohledávatelného katalogu akcí. Dejte mu rozpoznatelné jméno a zvolte jeho akce.
3. Zkontrolujte kroky a v případě potřeby přiřaďte zástupce. Vyhněte se kombinacím, které již používá Windows nebo vaše cílová aplikace.
4. Uložte koncept. Neuložené změny nejsou uloženou konfigurací.
5. Otestujte pomocí neškodné akce, jako je otevření dokumentu, a zkontrolujte zpětnou vazbu k provedení.

Zavřením hlavního okna normálně zůstane aplikace v oznamovací oblasti. Poklepáním na ikonu na hlavním panelu jej znovu otevřete nebo pro ukončení použijte nabídku na hlavním panelu. Spouštění na Windows je volitelné a zpočátku je zakázáno. Ovládací prvek pauzy pozastaví vazby; ikona zobrazuje odznak pozastavení. Zrušení spouštění maker prostřednictvím uživatelského rozhraní, zásobníku nebo nakonfigurované zkratky pro zrušení; výchozí je `Ctrl+Alt+Escape`.

## Dlaždice, akce a paleta příkazů

Dlaždice patří k profilům a mohou obsahovat více uspořádaných kroků. Editor podporuje uložení/zahození, náhled, vrácení/znovu a změnu pořadí kroků. Katalog poskytuje předvolby pro programy, dokumenty, adresy URL, text, schránku, zástupce, média, zvuk, okna, systémové akce a podporované integrace. Předvolba vyžadující parametry se otevře jako koncept.

Otevřete paletu pomocí `Ctrl+K`, zatímco MAKRIVO má fokus. Vyhledávejte uložené akce, předvolby, přepínání profilů a nástroje. Výběr pomocí šipek; Enter aktivuje; `Ctrl+Enter` upravuje podporovanou položku; Útěk se zavře. Některé příkazy připravené k použití se provádějí okamžitě, zatímco nakonfigurované akce vyžadují kontrolu. Lze nakonfigurovat samostatnou zkratku globální palety; nové konfigurace používají `Ctrl+Shift+Space`.

Podpora kroků textu a schránky `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` a `{{user}}`. Formátování data je `yyyy-MM-dd`; formátování času je `HH:mm`. Neznámé proměnné zůstávají nezměněny. Čtení schránky probíhá pouze na vyžádání. Akce schránky zapíše text bez jeho automatického vkládání.

Akce spouštěné z uživatelského rozhraní mohou cílit na poslední externí aplikaci. Pokud cíl zmizí nebo Windows blokuje aktivaci popředí, aktivujte cíl ručně a zkuste to znovu. Úspěšné doručení klíče nedokazuje, že cíl splnil svůj úkol.

## Profily a nastavení pracoviště

Profily mohou odpovídat názvu procesu a části názvu okna. Když jsou nastaveny oba, musí se oba shodovat. Pořadí profilu řeší konkurenční zápasy; výchozí profil je záložní. Explicitně povolit automatický výběr. Lock Profile zabraňuje automatickým změnám pro aktuální relaci.

Otevřete Nastavení pracoviště z Nástroje nebo palety. Vyberte programy, dokumenty, webové stránky, cíl zvuku/zobrazení nebo scénu OBS. Asistent připravuje profily pracoviště, setkání, streamingu, výzkumu nebo projektu. Před uložením zkontrolujte vygenerované kroky. Instalační program neprovede připravené akce.

## Makra, podmínky a nahrávání

Makra provádějí své kroky v pořadí a podporují čekání, opakování a zrušení. Obecný limit opakování je 100. Před spuštěním akcí, které zadávají do jiné aplikace nebo mění stav systému, použijte Náhled.

Wait for Target Window přijímá proces, podřetězec názvu a časový limit od 1 do 600,000 ms. Je vyžadován alespoň jeden filtr. Odpovídající viditelné okno musí reagovat a stát se popředím, než budou pokračovat následující kroky. Připravené okno nedokazuje, že dokument nebo služba dokončila načítání.

Editor pracovních postupů přidává podmínky existence souboru, běhu procesu a názvu okna. Nesprávná podmínka přeskočí krok a může přeskočit další kroky vpřed. Negujte podmínku pro vyjádření alternativní větve. Záměrně vyberte stop-on-error nebo continue-on-error; pokračování může ponechat pozdější kroky bez jejich očekávaných předpokladů.

Akční záznamník zachycuje zkratkové akordy a čeká, ne obyčejné psaní. Podporuje modifikační zkratky a funkční klávesy. Escape zastaví nahrávání. Zkontrolujte seznam a přijměte jej jako neuložený koncept dlaždice a poté jej explicitně uložte. Záznam kliknutí uživatelského rozhraní je samostatný nástroj popsaný níže.

## Klávesnice, gesta a vrstvy

Stránka klávesnice uvádí zjištěná rozhraní a referenční rozvržení. Jedna fyzická klávesnice může odhalit několik rozhraní. Vyberte správný jazyk a geometrii; samotný název produktu neidentifikuje rozložení ISO nebo ANSI.

Standardní globální klávesové zkratky platí pro všechny klávesnice. Vazby Raw Input specifické pro zařízení sledují konkrétní zařízení a skenují kód, včetně podporovaných funkčních a mediálních kláves. **Raw Input nepotlačuje běžný vstup Windows. Exkluzivní druhá klávesnice není připravena na výrobu.** Stále je vyžadován podepsaný, licencovaný a testovaný backend ovladače filtru; žádný není nainstalován pomocí MAKRIVO.

Podporované speciální klávesové vazby mohou využívat okamžité stisknutí, klepnutí, podržení nebo dvojité klepnutí. Klávesy podržené vrstvy dočasně vyberou jiné mapování a obnoví základní vrstvu při uvolnění; klávesy pro výběr vrstvy nespouštějí vlastní makro. Prahové hodnoty přidržení se pohybují od 150 do 5,000 ms; dvojitá okna od 150 do 1,000 ms. Tato mapování se liší od běžných globálních klávesových zkratek.

Průvodce speciálními klíči detekuje kompatibilní klíče, aniž by během zachycení provedl jejich přiřazené akce. Diagnostika vysvětluje nedostupná zařízení, pozastavené vazby, neshody profilů a konflikty. Překrytí zobrazuje vybrané rozvržení a vazby; jeho výchozí klávesová zkratka je `Ctrl+Alt+F1`.

| Integrace | Požadavky a omezení |
| --- | --- |
| Corsair | Spuštění iCUE, přístup k SDK a kompatibilní model. G1–G6 a podporované funkce RGB; proprietární ovládací prvky Fn, kolečka, zámku a profilu nejsou obecně odhaleny rozhraním G-key API. |
| Logitech G | Původní místní x64 G-key/LED SDK knihovny vyžadují výslovné schválení. Události G-key neidentifikují jednoznačně několik identických fyzických klávesnic. |
| Razer | Chroma RGB a standardní vstup; v případě potřeby mapujte vlastní ovládací prvky v Synapse. |
| SteelSeries | GameSense RGB a standardní vstup; mapovat proprietární ovládací prvky v GG, kde je potřeba. |
| Wooting | Kompatibilní originální místní x64 SDK pro analogové hodnoty a prahové hodnoty; normální HID vstup zůstává aktivní. |
| Ostatní klávesnice | Standardní klávesové zkratky nebo podporované fyzické vazby skenovacího kódu. Fn a proprietární ovládací prvky závisí na modelu a konfiguraci dodavatele. |

Výzkum výrobce popisuje zdokumentované možnosti, nikoli implementovanou nebo testovanou kompatibilitu s každým uvedeným zařízením. Nové adaptéry stále vyžadují skutečné přijetí hardwaru. Nestahujte binární soubory SDK z nedůvěryhodných zdrojů; Distribuční práva SDK jsou oddělena od technické kompatibility.

## Zvuk, displeje a OBS

Audio kroky nastavení objemu 0 Na 100 procent a úhlová/úhlová/úhlová podpora výstupu nebo mikrofonu. Vyberte konkrétní koncový bod nebo použijte Windows Předběžné. chybějící uložené cíle produkuje chybu namísto tiché kontroly jiného zařízení. Odstraňte zařízení po opětovném připojení. Kontrola koncového bodu není nárok na nezávislou podporu mixéru pro aplikaci.

Akce displeje zvyšují/snižují jas o 10 procentuální body nebo nastavte hodnotu od 0 do 100. Interní displeje používají podporovaná rozhraní WMI; externí monitory vyžadují podporu DDC/CI a obvykle nastavení nabídky monitoru. Doky, adaptéry a kabely mohou blokovat ovládání. Vyberte všechny podporované displeje nebo jeden monitor. Opětovné připojení může změnit dostupné identity; zkontrolovat uložený cíl. Chybějící naměřené hodnoty jsou zobrazeny spíše jako nedostupné než naměřené hodnoty.

OBS vyžaduje WebSocket v5 povolenou v nástrojích OBS. Před uložením nakonfigurujte koncový bod, otestujte připojení a načtěte názvy scén/vstupů. Mezi podporované akce patří přepínání scén, spuštění/zastavení/přepnutí streamu/nahrávání, pozastavení/obnovení nahrávání a ztlumení vstupu. Hesla mohou být uložena v Windows Credential Manager a jsou vyloučena z exportů konfigurace. Úspěšný příkaz OBS nestanoví každý efekt streamování po proudu.

## Ověření výsledků a historie provedení

Pro vhodné kroky povolte Ověřit výsledek. Aplikace rozlišuje předletové kontroly, potvrzené efekty, selhání, zrušení a efekty, které nelze ověřit. Existence procesu není připravenost aplikace. Relativní změny, zkratky a zadávání textu nemohou získat paušální nárok na úspěch. Když se akce nezdaří, zkontrolujte protokol aktivit a historii ověření. Chyby konfigurace by měly být opraveny před opětovným spuštěním úplného makra.

## Automatizace uživatelského rozhraní a záznam kliknutí

Přidejte krok UI Automatizace, vyberte operaci a po zobrazeném zpoždění zachyťte cíl pod ukazatelem. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` a `WaitState`. Pole hesla, ovladače a nesmyslné selektory jsou odmítnuty.

Automatizace uživatelského rozhraní běží v omezeném doprovodném procesu. Závisí to na podpoře usnadnění přístupu Windows cílové aplikace a na oprávněních. Záznamník kliknutí zachycuje podporované akce a pauzy; nezaznamenává obecný textový vstup. Zkontrolujte a ověřte jeho návrh před jeho přijetím. Změny uživatelského rozhraní v externí aplikaci mohou zneplatnit selektory. Izolace procesů omezuje pády a zrušení, ale není karanténou zabezpečení.

## Pluginy

Otevřete Rozšíření a zkontrolujte balíček před importem. Kontroluje se struktura balíčku, manifest a hash. Spustí se import zakázán. Před povolením si přečtěte deklarované akce a výslovně důvěřujte vydavateli. Pluginy běží s vašimi uživatelskými oprávněními v samostatném procesu; ten proces je **není sandbox**. Instalujte pouze balíčky, kterým důvěřujete.

Správce podporuje export, zakázání, odinstalaci a obnovení/zahození předchozí verze. Chybějící vyvolání bloku závislostí. Ukázkový balíček poskytuje odpočítávání a akce místního stavu. Podporované schéma je zdokumentovaná podmnožina schématu JSON, ne všechny možné funkce schématu JSON. Neexistuje žádný veřejný trh s pluginy.

## Mobilní panel

1. Otevřete Mobile Panel, vyberte místní IP a explicitně vyberte profily, které chcete vystavit. Výchozí adresa zpětné smyčky je dostupná pouze na samotném PC; vyberte příslušnou adresu LAN pro telefon nebo tablet.
2. Spusťte panel a přečtěte si jeho HTTPS adresu, otisk certifikátu a jednorázový šestimístný párovací kód.
3. Na zařízení ve stejné důvěryhodné síti otevřete adresu. Než začnete důvěřovat místnímu certifikátu, porovnejte otisk SHA-256 prostřednictvím důvěryhodného kanálu. Aplikace neinstaluje důvěryhodný certifikát automaticky.
4. Spárujte pomocí zobrazeného kódu a vyzkoušejte jednu neškodnou destičku. Pro přístup k síti LAN může být zapotřebí ruční povolení brány firewall Windows. Nevystavujte port internetu.
5. Po dokončení zastavte panel nebo zrušte spárované klienty prostřednictvím aplikace.

Klíče soukromého serveru jsou chráněny pomocí Windows DPAPI; přihlašovací údaje klienta jsou uloženy jako hash. Změna IP nebo obnovení certifikátu může vyžadovat obnovení důvěry. Panel zobrazuje akční a ověřovací stavy, ne každý možný aktivní senzor. Rozložení prohlížeče a místní kontroly HTTPS nenahrazují skutečnou akceptaci sítě LAN telefonem/tabletem.

## Místní asistent setkání AI

Tento volitelný experimentální asistent je ve výchozím nastavení zakázán. Otevřete jej z Nástroje, explicitně jej povolte, vyberte runtime/model a přidejte pouze prostředky, které chcete, aby zvážil. Stanovte cíl, vytvořte koncept, zkontrolujte nevyřešené požadavky a přeneste výsledek do běžného editoru. Nic se neprovede automaticky; ukládání a spuštění zůstávají explicitními akcemi. Neexistuje žádná automatická záloha za běhu.

| Doba běhu | Nastavení |
| --- | --- |
| Ollama | Samostatná místní služba a lokálně nainstalovaný model na `127.0.0.1:11434`. Deklarované vzdálené/cloudové modely jsou odmítnuty. |
| LM Studio | Místní server s lokálně načteným modelem a podporou schématu JSON na `127.0.0.1:1234`. Nepovolujte vzdálené směrování pro místní zpracování. |
| llama.cpp | Místní `llama-server` a kompatibilní model s podporou schématu JSON na `127.0.0.1:8080`. |
| ONNX Doba běhu GenAI CPU | Důvěryhodná kompatibilní místní složka textového modelu GenAI; vyberte `genai_config.json`. Inference používá svázaný doprovodný proces bez HTTP. |

HTTP klienti používají pouze zpětnou smyčku, nepoužívají proxy a nesledují přesměrování. MAKRIVO nemůže spolehlivě zjistit, zda kompatibilní místní server interně volá vzdálenou službu. Ověřené místní servery nelze v této verzi konfigurovat. Hmotnosti modelů, Python a místní inferenční servery se automaticky nestahují ani neinstalují.

ONNX Pakety potřebují `tokenizer.json` a platný chatový šablona. podporované šablony v `tokenizer_config.json` Je to string `chat_template`Slovník s řádkem `default`, nebo nesporný seznam jmenovaných/podmíněných šablon. pokud šablona chybí nebo nula, `chat_template.jinja` Používá se jako padák. prázdné, ambigové, nadměrné nebo nesprávně vytisknuté šablony jsou odmítnuty. ONNX soubory, modely vkladu/vidění a balíčky GPU nejsou s kompatibilním GenAI Textové modely - Zbavení GenAI Runtime verze je 0.17.1Použijte pouze důvěryhodné modely; procesní oddělení není písečný box.

Asistent podporuje vybrané programy bez argumentů, vybrané dokumenty a webové stránky a na výslovné vyžádání ztlumí mikrofon. Nevytváří zprávy, schůzky, libovolné skripty, bezplatná kliknutí, stahování ani přepínání náhlavních souprav. Názvy cílů a prostředků jsou odeslány do vybraného místního běhového prostředí; cesty k prostředkům a obsah souborů nejsou dodávány samostatně. Cíl zadaný uživatelem může sám o sobě obsahovat osobní údaje. Vlastní síťové chování místního runtime zůstává odpovědností uživatele.

Rozpočet požadavku ONNX je 90 sekund včetně načítání modelu, až do 3,072 tokenů výzvy a 1,024 generovaných tokenů, rovněž omezených kontextem modelu. Poškozený/zkrácený JSON a neznámá ID zdrojů jsou odmítnuta. Zrušením nebo zavřením ukončíte požadavek. Náhodný model runtime test stanoví provedení, nikoli užitečnou kvalitu plánování nebo úsporu času. Lokální protokoly experimentů obsahují identifikátory a časování událostí, nikoli text cíle nebo obsah dokumentu; nejsou automaticky nahrány.

Později malá hodnocení modelu Instruct produkovala nulové platné plány ve čtyřech německých/anglických zkouškách. Native inference fungovala, ale model neuspěl přísnou plánovací smlouvu. To je negativní výsledek kvality, ne doporučený model nebo prokázaný užitečný asistent.

## Import, export, synchronizace a zálohování

Před výměnou konfigurace zkontrolujte náhledy importu. Spuštění importovaných akcí je zakázáno a přenosná synchronizace odstraní hardwarově specifické vazby. Přiřaďte zařízení a povolte akce záměrně po kontrole. Před sdílením exportu zkontrolujte obsah textu/akce; vyloučení hesel neznamená, že veškerý obsah akcí vytvořených uživatelem nebude citlivý.

Synchronizace profilu výměnu souborů v vybrané složce, volitelně jedna spravovaná vašim stávajícím cloudovým klientem. MAKRIVO neposkytuje žádný cloudový účet a nezachovává žádné cloudové autentifikace. Revize zabraňují tichým přepisům. Zahrnutí obsahu akce je výslovnou volbou. Zadejte si konfiguraci před importem náhrad. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`• historické název adresáře je zachován pro kompatibilitu.Zatáhněte aplikaci před ručně kopírováním/obnovováním.Zachovávány schválení SDK a OS-ochráněné autentifikace mohou být nutné obnovit na jiném PC.

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

Integrační testy Windows mohou otevřít izolovaná okna a dočasně nakonfigurovat/uvolnit podporované vazby dodavatele. Spusťte je, aniž by tyto vazby vlastnil jiný řadič. Výchozí jas/audio sondy jsou pouze pro čtení; explicitní hardwarové okružní testy jsou samostatné. Falešné protokoly, vykreslené snímky obrazovky a grafy náhodných odvození nesmí být reprezentovány jako skutečné přijetí hardwaru/modelové kvality.

Kompletní vydavatel sestavuje doprovodné hostitele, mobilní prostředky, vzorový plugin, běhové prostředí ONNX a dostupná oznámení o závislostech. Udržujte jeho podadresáře. Úspěšné sestavení není podepsaný instalační program nebo právní schválení vydání. Zdroj a sestavení se mohou vyvíjet samostatně; porovnejte snímek dokumentace a potvrďte s nainstalovanou verzí.

## Licencování, soukromí a stav vydání

Zamýšlený komerční model je jednorázový nákup s trvalým používáním zakoupené hlavní verze; konečná cena, povolenky na zařízení a podmínky upgradu musí být stanoveny ve skutečných podmínkách prodeje. Systém nákupu/aktivace výroby není touto příručkou stanoven. Pro každodenní ovládání není vyžadován žádný cloudový účet.

Nešiřte proprietární binární soubory SDK bez prokázaného povolení. Zachovejte licenční upozornění a upozornění třetích stran pro každou dodávanou závislost, včetně balíčků .NET a ONNX. Modelové váhy mají své vlastní podmínky. Datovaný licenční audit předchází některým novým závislostem a není povolením pro tyto dodatky. Veřejné úložiště samo o sobě neuděluje obecnou open-source licenci.

Hardware skutečných dodavatelů, provoz v síti LAN na telefonu/tabletu, přechody se smíšeným monitorem/DPI, dlouhodobé používání, kompletní jazyková revize, exkluzivní podpora ovladačů a užitečná kvalita plánování umělé inteligence zůstávají samostatnou prací při přijímání. Nikdy nevyvozujte schválení pouze z přítomnosti funkce.

<!-- generated-doc locale=cs-CZ source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: šest rozšíření produktu a adaptéry klávesnice

Datum: 2026-10-12
Stav: navržený design pro uživatelskou kontrolu; implementace nezačala.

## Cíl
Doručte všech šest požadovaných rozšíření ve stávající aplikaci Windows/WPF se zachováním stávajících profilů, přiřazení G1/G2, místním provozem a používáním bez účtu. Rozšiřte integraci klávesnice na Razer a Logitech s znovu použitelnou architekturou založenou na schopnostech pro jiné výrobce. Název výrobce nebo rozložení klávesnice nikdy nedokazuje, že jsou podporovány proprietární ovládací prvky.

## Rozhodnutí o architektuře
Rozšiřte Aivana.Core o serializovatelné smlouvy a ověřování, Aivana.Windows o Windows a adaptéry dodavatele a Aivana.Desktop s transakčními zobrazeními. Používejte samostatné doprovodné projekty pro izolované spuštění pluginu a službu místního mobilního panelu. Vyhněte se přepisování nebo jedinému privilegovanému procesu hostujícímu každé rozšíření. Stávající konfigurace se musí načíst beze změny; připojovat druhy akcí místo změny pořadí jejich trvalých hodnot výčtu.

Zvažované alternativy: monolitická implementace by propletla hardware, síť a provádění pracovních postupů; nahrazení WPF by přidalo nesouvisející migrační práci. Doporučují se modulární rozšíření.

## 1. Nezávislá makro podložka klávesnice
Uložte vazby podle identity zařízení plus fyzického skenovacího kódu a příznaků rozšířeného klíče. Přidejte explicitní pozorovací a exkluzivní režimy, zjišťování zařízení, zachycení klíče, potlačení opakování, gesta, pozastavené vrstvy a diagnostiku opětovného připojení. Zachovat hlavní/numerická klávesnice Zadejte rozdíly a otestujte německé/americké rozložení. Vazby se zaměřují na vybranou fyzickou klávesnici; nejednoznačné identity opětovného připojení vyžadují opětovný výběr, spíše než ovládání jiného zařízení.

Raw Input poskytuje pouze pozorování. Výhradní potlačení pro jednotlivá zařízení vyžaduje skutečný backend filtru klávesnice; nikdy nekorelujte globální nízkoúrovňové háky se Raw Input a nepožadujte spolehlivé potlačení specifické pro zařízení. Poskytněte hlídacího psa, nouzové uvolnění, chování při selhání a instalační/odinstalační program. Nezachycujte zabezpečený vstup z plochy. Instalace ovladače a aktivace exkluzivního režimu jsou explicitní samostatné akce uživatele.

Brána ovladače: vyberte podporovaný, řádně licencovaný a podepsaný backend kompatibilní s cílovými konfiguracemi Windows 11 nebo vyviňte/podepište vyhrazený filtr. Zachycování je kandidátem na hodnocení, nikoli vázáním na produkční závislost: jeho oficiální README popisuje testování prostřednictvím Windows 10 a odlišné komerční licencování. Dokud nebude tato brána splněna, musí být exkluzivní režim zobrazen jako nedostupný; prvek nelze prohlásit za dokončený.

Přijetí: se dvěma fyzickými klávesnicemi provádí stejná klávesa makro a nevytváří žádný běžný aplikační vstup pouze na vybraném exkluzivním zařízení; ostatní vstup z klávesnice zůstane nedotčen. Ověřte odpojení, selhání, zrušení, pozastavení/obnovení a odebrání ovladače na skutečném hardwaru.

## 2. Průvodce nastavením pracovního prostoru
Přidejte průvodce pro pracoviště, schůzky, streamování, výzkum a přepínání projektů. Objevte nainstalované aplikace, zvuková zařízení, monitory a scény OBS. Uživatelé volí cíle a vazby; zobrazit náhled výsledných kroků a ověřit chybějící požadavky. Uložte pouze po kontrole, během nastavování neprobíhají žádné akce. Vygenerované profily lze nadále upravovat ve stávajících editorech.

Přijetí: vytvořte pracovní profil bez ruční úpravy cest ke spustitelným souborům nebo ID zařízení; zrušení ponechá nastavení beze změny; chybějící software má praktické vysvětlení.

## 3. Ověřené provedení
Zaveďte kontroly před výstupem, zadané výsledky po jednotlivých krocích a volitelné dodatečné podmínky. Rozlišení vyžádaných, dokončených, ověřených, neúspěšných, vypršelých, zrušených a neověřitelných stavů. Ověřte zvuk/mikrofon, jas, připravenost procesu/okna a stav OBS s ohraničeným asynchronním čtením. Doručení zkratky/textu není důkazem, že cílová aplikace příkaz přijala. Vnější vedlejší účinky nejsou univerzálně reverzibilní; nabízet náhradu pouze tam, kde je implementována.

Přijetí: odpojené explicitní cíle nikdy tiše neustoupí; špatně pozorovaný stav nikdy nehlásí ověřený úspěch; zrušení zastaví hlasování; selhání identifikují krok a akci obnovy. Historie provádění vynechává textové užitečné zatížení a tajemství.

## 4. Pluginy a sdílené balíčky
Verzované manifesty JSON deklarují akce, schémata parametrů, poskytovatele stavu, závislosti a schopnosti. Spouštět spustitelné pluginy v samostatném procesu přes ohraničený místní IPC s handshake, termíny a zrušení. Odmítnout chybné zprávy a nekompatibilní verze; zvládnout pády bez ukončení ovladače. Prohlášení o schopnostech jsou zveřejněním a zprostředkováním, nikoli tvrzením o karanténě OS.

Poskytněte náhled exportu/importu balíčku, hash obsahu, ochranu před průchodem cesty, omezení velikosti, kontrolu verzí/závislostí, explicitní povolení a vrácení upgradu. Importované spustitelné pluginy se nesmí spustit automaticky. Zahrňte funkční příklad pluginu s komplexním postupem instalace/akce/stavu/odinstalace. Pro toto vydání není vyžadována žádná služba veřejného tržiště.

Přijetí: vytvořit, exportovat, nainstalovat a spustit skutečný příklad integrace; demonstrovat časový limit, izolaci proti selhání, selhání závislosti, odmítnutí průchodu a zakázán import.

## 5. Automatizace uživatelského rozhraní Windows
Přidejte výběr prvku a omezené akce pro vyvolání, hodnotu, výběr, přepínání, zaměření a čekání na prvek/stav. Trvalé selektory pomocí procesu, ID automatizace, typu řízení a kontextu předka; vyhněte se pouze souřadnicovému záznamu. Zobrazit počet shod selektoru a selektory testu pouze pro čtení. Pole hesla jsou ze záznamu vyloučena. Nepodporované ovládací prvky a omezení na úrovni integrity Windows zpracujte explicitně.

Rozšiřte nahrávání s přehlednými akcemi UIA, přičemž zachováte aktuální zkratkové nahrávání. Nejednoznačné selektory selžou namísto kliknutí na libovolný ovládací prvek. Normální strom přístupnosti prohlížeče není příslibem úplného automatizačního enginu DOM.

Přijetí: automatizace vyhrazené testovací aplikace se stabilními voliči napříč změnami pozice/velikosti; nejednoznačnost testu, chybějící prvky, zakázané ovládací prvky, časové limity a zrušení.

## 6. Panel místního telefonu/tabletu
Obsluhujte responzivní panel ze služby LAN. Spárujte explicitně pomocí krátkodobého jednorázového kódu a vydejte odvolatelné přihlašovací údaje chráněné místně. Použijte HTTPS s dokumentovanou důvěryhodností/připnutím místního certifikátu; žádné záložní odesílání pověření v prostém textu. Ve výchozím nastavení je zakázáno a omezuje nakonfigurovaná rozhraní. Ověření původu, ověřené příkazy, rychlostní limity a ohraničené zprávy jsou povinné.

Odhalit povolené profily/akce, aktuální stav, postup a zrušení; používejte stabilní ID a stejnou spouštěcí frontu jako desktopové a fyzické klíče. Nevystavujte svévolné příkazy, cesty ke spustitelným souborům, tajemství nebo instalaci pluginu. Poskytněte obrazovky připojení, párování a zrušení. Není zahrnut žádný cloudový účet ani internetový přenos.

Přijetí: provoz ze skutečného telefonu/tabletu na stejné LAN; nespárovaní klienti nemohou vypisovat nebo spouštět akce; odvolané tokeny selžou; duplikované/přehrávané příkazy se nespouštějí dvakrát; odpojení neprovádí tiché opakování akcí.

## Rozsah výrobce klávesnice

| Adaptér/rodina | Plánovaná integrace | Explicitní hranice |
| --- | --- | --- |
| Corsair | Zachovat aktuální vstup G-key iCUE a integraci RGB | Zůstává vyžadována detekce schopností modelu/softwaru |
| Logitech G | G-key SDK adaptér s tiskem/uvolněním a kontrolou dostupnosti za běhu; osvětlení prostřednictvím samostatné sady SDK, pokud je podporováno | Nepřirovnávat detekci G HUB k podporovanému G-key API na každém modelu |
| Razer | Adaptér Chroma osvětlení a naváděné mapování Synapse na standardní klíč plus obecný HID vstup | Chroma je rozhraní API pro osvětlení; neoznačujte jej jako nativní proprietární vstup makro-klíče |
| SteelSeries | GameSense adaptér stavu/světla plus obecný HID vstup | Odeslání události GameSense není obecné API pro zachycení klíče |
| Wooting | Analogový SDK adaptér pro podporovaný analogový vstup plus normální HID klíče | Schopnosti a podmínky distribuce SDK musí být ověřeny podle podporovaného modelu |
| Keychron/QMK/VIA | Standardní HID klíče a instrukce pro mapování firmwaru; známé rozvržení pouze s důkazy | Podpora konfigurace VIA není univerzální API pro makro klíč za běhu |
| Další standardní HID klávesnice | zjišťování a mapování fyzického klíče podle zařízení; exkluzivní režim pouze s ověřeným backendem filtru | Žádná umělá podpora Fn, číselníku nebo proprietárních kláves specifických pro daný model |

Každé zařízení zobrazuje zjištěný, pozorovaný, mapovaný stav pomocí softwaru dodavatele, nativní sady SDK, stav s výhradním ovladačem, nedostupný nebo netestovaný hardware. Zachovejte důkazy o schopnostech pomocí verzí adaptérů a testovaných identifikátorů modelu/firmwaru.

## Objednávka doručení
1. Kompatibilita konfigurace, smlouvy o způsobilosti a ověřené provedení.
2. Řízené nastavení pomocí skutečného zjišťování a existujících spouštěčů akcí.
3. UI Automation a její záznamový/testovací přípravek.
4. Hostitel zásuvného modulu, tok balíčků a příklad integrace.
5. Spárovaný LAN panel a mobilní ověření.
6. Rozšířené adaptéry dodavatele a exkluzivní backend integrace klávesnice se skutečným ověřením hardwaru.

Všech šest zůstává povinných. Podepisování/licencování ovladačů, nepřístupné sady SDK nebo chybějící testovací hardware jsou brány k vydání, nikoli důvody k nahrazení falešného chování a označení práce za dokončenou.

## Kritéria ověření a uvolnění
Spusťte existující kontroly jádra, Windows a WPF a přidejte cílené integrační testy pro každou novou hranici. Ověřte zpáteční cesty JSON a starší profily; otestovat importovaný spustitelný obsah, ověření sítě, zrušení, selhání pluginu a opětovné připojení zařízení. Zkontrolujte rozvržení pro stolní a mobilní zařízení. Samostatně ověřte vysoké měřítko Windows. Artefakty uvolnění sestavujte pouze po absolvování kontrol; v poznámkách k verzi rozlišujte implementované/testované, implementované/hardwarově netestované a externě blokované funkce. Nenahrazujte nainstalovanou aplikaci, neinstalujte ovladače, neměňte skutečné mapování klávesnice ani neotevírejte síťový přístup jako náhodný test.

## Důkazy
- Sémantika Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G vývojářské sady SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Záchytné úložiště a podmínky závislosti: https://github.com/oblitum/Interception
- Stávající inventář výrobců úložiště: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Přezkoumat rozhodnutí
Uživatel je požádán o schválení tohoto písemného návrhu před realizačním plánem. Hlavní externí závislostí je backend ovladače s exkluzivním vstupem připravený k produkci. Doporučený rozsah zachovává toto jako uzavřený výstup, spíše než jej tiše redukuje na Raw Input pozorování.

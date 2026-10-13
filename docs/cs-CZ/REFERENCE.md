<!-- generated-doc locale=cs-CZ source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../README.md)

# MAKRIVO

[Kompletní dokumentace ve všech jazycích 27](README.md) · [Úplný uživatelský manuál](HANDBOOK.md)

**Na Aivana.** [Aktuální soupis funkcí a omezení vydání](FEATURES.md) · [Zpráva o testech rozšíření](testing/2026-10-12-six-extensions.md) · [Prototyp místního asistenta AI](testing/2026-10-12-local-meeting-assistant.md)

**Eangličtina (USA)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Místní ovladač klávesových zkratek Windows s dlaždicemi, profily, makry a detekcí klávesnice ve stylu Stream Deck. Rozhraní podporuje jazyky 27. Pro každodenní použití není potřeba žádný účet ani cloudové připojení.

[Dokumentace](README.md) · [Ověření](VERIFICATION.md) · [Zpráva o testech funkcí](testing/2026-10-09-usp-release.md)

## Začínáme

Po publikování se aplikace nachází v `artifacts/win-x64/MAKRIVO.exe`. Uchovávejte celou výstupní složku pohromadě; můžete ji zkopírovat jinam. Počáteční cílovou platformou je Windows 11 x64. Artefakty sestavení jsou vyloučeny z Git; pracovní kopie zdrojového repozitáře neobsahuje předem sestavený spustitelný soubor.

Nainstalujte sadu .NET 10 SDK specifikovanou v `global.json`, abyste mohli sestavovat a spouštět ze zdroje:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publikování samostatného sestavení bez nutnosti samostatně nainstalovaného běhového prostředí .NET:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Základní pracovní postup

1. Vyberte profil a vytvořte dlaždici akce.
2. Nastavte jeho název, ikonu, barvu a klávesovou zkratku. Kombinace Ctrl/Alt/Shift snižují náhodnou aktivaci při psaní.
3. Přidejte jeden nebo více kroků: aplikace, soubory, weby, text, klávesové zkratky, média, hlasitost, přepínání profilů nebo prodlevy.
4. Uložit. Změny zůstávají konceptem, dokud nejsou uloženy; zahodit obnoví uloženou verzi. Tlačítko Přehrát dlaždice a jeho klávesová zkratka používají stejnou cestu spuštění.
5. Změňte pořadí dlaždic a kroků makra pomocí přetažení nebo tlačítek se šipkami. Šablony zjednodušují nastavení. Makra se mohou opakovat až 100 krát. Ve výchozím nastavení zrušte prostřednictvím rozhraní, nabídky na hlavním panelu nebo Ctrl+Alt+Escape.

Profily pro konkrétní aplikace používají název procesu bez celé cesty. Pořadí profilů rozhoduje při více shodách; výchozí profil se použije, pokud žádná shoda není. Automatický výběr profilu je nutné povolit v Nastavení.

Zavření hlavního okna ve výchozím nastavení ponechá aplikaci v oznamovací oblasti. Dvojklikem na její ikonu okno znovu otevřete; aplikaci ukončíte z nabídky ikony. Spouštění s Windows je volitelné a ve výchozím nastavení zakázané. Klávesové zkratky lze centrálně pozastavit.

## Paleta akcí a příkazů

Přidat akci otevře prohledávatelný katalog s předvolbami 90 v osmi kategoriích. Každá má popis a náhled. Editor maker používá stejný katalog k přidání nebo nahrazení kroků. Výběrem předvolby v tomto pracovním postupu editoru vytvoříte koncept bez provedení.

Paleta příkazů se otevírá ze záhlaví nebo pomocí Ctrl+K v aplikaci. Vyhledává uložené akce napříč profily, předvolby katalogu, přepínání profilů, navigaci, pozastavení/obnovení, zrušení maker a import/export. Filtrujte podle uložených akcí, funkcí katalogu nebo příkazů aplikace. Šipky vybírají položku, Enter ji aktivuje, Ctrl+Enter upraví upravitelnou položku a Escape paletu zavře. Oblíbené položky jsou první. Hotové příkazy katalogu lze spustit ihned; předvolby vyžadující nastavení otevřou koncept. V Nastavení lze určit globální zkratku palety. Nové konfigurace používají Ctrl+Shift+Space; stávající konfigurace nedostanou novou globální zkratku automaticky.

Zvukové akce nastavují hlasitost přesně od 0 do 100 procent a ztlumují, obnovují zvuk nebo přepínají ztlumení výstupních a mikrofonních zařízení. Vyberte konkrétní koncový bod nebo jej ponechte nenastavený pro výchozí zařízení Windows. Nedostupný uložený koncový bod způsobí chybu namísto ovládání jiného zařízení. Obnovení zařízení znovu načte připojená zařízení.

OBS Studio používá WebSocket v5: spuštění/zastavení/přepnutí/pauza/obnovení nahrávání; spustit/zastavit/přepnout streamování; měnit scény; přepnout ztlumení vstupu. Povolte server WebSocket v části Nástroje v OBS a nastavte jeho koncový bod v editoru. Check Connection and Load Names načte verzi OBS, scény a vstupy bez spuštění nahrávání nebo streamu. Vyberte jména ze seznamu nebo je zadejte ručně. Hesla lze uložit do Windows Credential Manager a jsou vyloučena z exportovaných konfigurací. Nedostupná připojení OBS hlásí chybu.

Profily mohou odpovídat názvu procesu a části názvu okna. Obě zadané podmínky musí platit. Zámek profilu brání automatickému přepínání v aktuální relaci. Zachycení podmínek z popředí používá poslední externí aplikaci s fokusem.

Textové akce a akce schránky podporují `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` a `{{user}}`. Data používají `yyyy-MM-dd`; časy používají `HH:mm`. Neznámé zástupné symboly zůstávají nezměněny. `{{profile}}` označuje profil spuštěné akce; `{{process}}` označuje kontext externí aplikace. Obsah schránky se čte pouze při použití tohoto zástupného symbolu. Akce schránky zapíše text, ale automaticky jej nevloží.

Akce okna minimalizují, maximalizují, obnovují nebo uspořádávají okno na levé/pravé straně pracovní plochy monitoru. Akce systému uzamkne Windows. Tyto akce se spouštějí pouze při explicitním vyvolání.

Náhled ukazuje kroky, opakování a proměnné bez provedení. Zpět a Znovu uchovávají až 100 stavů konceptu; textová pole zachovávají nativní úpravy Ctrl+Z. Označte oblíbené položky v editoru nebo nabídce dlaždice a filtrujte je na dashboardu. Počty provedení platí pouze pro aktuální relaci.

Akce textu, zkratky a okna spuštěné z rozhraní aktivují poslední externí cílové okno. Paleta zobrazuje cílový kontext. Pokud se cíl uzavřel nebo Windows odmítne aktivaci, provádění se zastaví se zprávou.

## Pomoc s klávesnicí a zpětná vazba

Průvodce speciálními klávesami je na stránce Klávesnice a v paletě. Uvádí klávesy G hlášené iCUE. Detekce stisku klávesy je dočasně zachytí bez provedení přiřazených akcí. Běžné vazby se obnoví po detekci, zrušení nebo zavření. Funkce katalogu a vybraná klávesa se otevřou jako neuložený koncept; kontrolují se duplicitní vazby.

Překryv klávesnice ukazuje vybrané referenční rozložení a aktivní vazby. Podržte Ctrl+Alt+F1 nebo jej přepněte z palety či stránky Klávesnice. Nepřebírá fokus a propouští kliknutí myší. Když je viditelný, aktualizuje změny profilu a vazeb; běžné stisky kláves Windows jsou zvýrazněny. Ovládací prvky specifické pro výrobce nemusí poskytovat odpovídající signál klávesy Windows.

Zpětná vazba provedení ukazuje čekání, průběh kroků/opakování, dokončení, zrušení nebo selhání bez převzetí fokusu či odhalení zadaného textu. Commands Executed potvrzuje provedení příkazu, nikoli nezávisle dotazovaný živý stav aplikace třetí strany. Podrobnosti chyb jsou v protokolu aktivit.

## Zařízení a Corsair

Corsair G1–G6 lze přiřadit přímo prostřednictvím oficiálního iCUE SDK. Vyberte zařízení a klávesu v editoru akcí a uložte nebo připravte koncept ze zobrazení klávesnice K100. Vazby platí pro vybrané zařízení a aktivní profil; pauza je také zakáže. iCUE musí běžet a povolit přístup. Fn, kolečko iCUE a ovládací prvky profilu a zámku nelze mapovat přes toto G-key API.

Stránka Zařízení uvádí rozhraní klávesnic Windows a obnovuje se pravidelně i při připojení zařízení. Fyzická klávesnice může poskytovat více rozhraní HID. Standardní globální zkratky fungují napříč výrobci.

Detekce Corsair používá dostupné informace o zařízení. Ovládání RGB vyžaduje kompatibilní hardware, běžící iCUE a oficiální iCUE SDK; aplikace hlásí skutečnou dostupnost. Ostatní výrobci mají nyní detekci a standardní akce. Mimo integraci kláves G Corsair fungují proprietární ovládací prvky, pokud je Windows přijímá jako běžný vstup. Aplikace neinstaluje ovladač klávesnice.

## Jazyky

Nastavení nabízí 27 jazyků: němčinu, angličtinu, francouzštinu, španělštinu, italštinu, portugalštinu, nizozemštinu, polštinu, češtinu, slovenštinu, maďarštinu, rumunštinu, bulharštinu, řečtinu, dánštinu, švédštinu, norštinu, finštinu, estonštinu, lotyštinu, litevštinu, slovinštinu, chorvatštinu, ukrajinštinu, ruštinu, turečtinu a srbštinu v latince. Automatická volba se řídí jazykem zobrazení Windows. Překlady jsou přibaleny místně. Rozložení, klávesové zkratky a uživatelské názvy jsou nezávislé na jazyku rozhraní.

Úplná dokumentace repozitáře je dostupná ve všech 27 jazycích aplikace prostřednictvím [Jazykový index](README.md). Překlady jsou strojově podporované, obsahují odkazy na původní zdroje a procházejí strukturálními kontrolami; revize rodilým mluvčím je samostatná. Aplikace přijímá také regionální kódy `en-US`, `de-DE`, `fr-FR` a `it-IT` a používá jejich číselné formáty. Dosavadní jazykové kódy zůstávají kompatibilní.

## Rozložení a průzkum výrobce

Zobrazení klávesnice poskytuje plnohodnotná a beznumerická referenční rozložení ISO-DE/ANSI-US a rozložení Corsair K100 RGB. Geometrie znázorňuje polohy, široké/vysoké klávesy a ISO Enter. Standardní rozložení jsou obecné reference. Rozložení K100 vycházejí ze zdokumentovaných ovládacích prvků bez tvrzení o rozměrech přesných na milimetr. Vyberte správnou jazykovou/geometrickou variantu: název modelu ani USB VID/PID neurčují konkrétní variantu.

Mapování interpretuje fyzické skenovací kódy pomocí aktivního vstupního rozložení Windows. Postup připraví koncept a zachová kontroly uložení/konfliktů. Standardní vazby jsou globální zkratky Windows, nejsou omezeny na jednu připojenou klávesnici. Corsair G1–G6 používají integraci iCUE pro konkrétní zařízení. Jiné proprietární ovládací prvky vyžadují adaptér nebo výrobcem nakonfigurované zkratky. Hlavní Enter a Enter numerické klávesnice sdílejí pro globální zkratky stejný identifikátor virtuální klávesy.

a to [Výrobce výzkumu](research/tastaturhersteller-sonderfunktionen.md) a [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokrývá 74 skupiny značek/výrobců, rozlišují konfigurátory, vstupní SDK a osvětlení API. [Layout důkazů](research/keyboard-layout-evidence.md) zaznamenává rozdíly modelů, SKU a modulů. Pokrytí průzkumem neznamená implementovanou podporu hardwaru.

## Data

Konfigurace je uložena v datech místní aplikace. Import/export použijte JSON. Import zobrazí náhled konfigurace, po potvrzení nahradí aktuální konfiguraci a zakáže importované vazby, dokud je ručně nepovolíte. Obsah textových akcí se neshromažďuje jako protokol úhozů. Hesla Credential Manager jsou oddělena od exportovaných profilů.

## Ověření

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Kontroly Windows mohou zkoumat hardware a dočasně konfigurovat/uvolňovat podporované vazby iCUE. Spouštějte je bez jiného řadiče používajícího tyto vazby. Kontroly zvukových koncových bodů nemění aktuální hlasitost ani nastavení ztlumení.

Nativní vstup nemůže obejít oprávnění Windows. Normálně spuštěný řadič nemusí odesílat vstup do aplikace správce. Rezervované nebo obsazené klávesové zkratky se objevují jako konflikty.

a to [Dokumentační index](README.md) odkazuje na historické návrhy, plány, průzkum a datované testovací zprávy. Historické plány popisují zaznamenaný rozsah, nikoli aktuální záruku podpory funkcí.

### Šablony pracovních postupů a čekání na cílové okno

Katalog obsahuje šest upravitelných pracovních postupů: pracoviště, schůzka, streamování, výzkum, změna projektu a textový úryvek. Vyhledejte `workflow.` v katalogu nebo paletě příkazů. Výběr postupu otevře neuložený koncept; nikdy jej ihned nespustí. Při přidání postupu do existujícího makra nebo přiřazení ke klávese Corsair zůstanou všechny kroky zachovány. Před uložením nastavte příklady URL, cesty programů, profily a scény OBS.

Krok **Wait for target window** přijímá název procesu (s `.exe` nebo bez), část názvu bez rozlišení velikosti písmen a časový limit 1–600000 ms. Je vyžadován alespoň jeden filtr; pokud jsou zadány oba, musí platit oba. Makro pokračuje až po reakci odpovídajícího viditelného okna a jeho přechodu do popředí. Pokud proces sdílí více oken, použijte konkrétní název. Zrušení nebo vypršení limitu zastaví další kroky. Připravenost okna nezaručuje načtení dokumentu ani připojení externí služby, například OBS WebSocket.

Jas displeje: v katalogu akcí vyhledejte **Zvýšit jas obrazovky**, **Snížit jas obrazovky** nebo **Jas obrazovky na 50 procent**. Relativní akce mění jas o 10 procentních bodů; pevné hodnoty jsou od 0 do 100. Podporované interní displeje používají WMI; kompatibilní externí monitory vyžadují DDC/CI povolené ve své nabídce. Ve výchozím nastavení akce ovlivní všechny podporované displeje; v editoru lze vybrat konkrétní monitor. Připojení a doky mohou podporu omezit.

### Rozšířené ovládání

Otevřete paletu příkazů pomocí **Ctrl+K, když má okno aplikace fokus**. Vyhledejte **Návrhář pracovních postupů**, **Záznam makra**, **Synchronizovat profily** nebo **Diagnostika kláves** (znění odpovídá vybranému jazyku). Návrhář upravuje vybraný koncept dlaždice: podmínky souboru/procesu/okna, počty přeskočených následujících kroků a zastavení/pokračování při chybě. Záznamník zachycuje kombinace klávesových zkratek a prodlevy, nikoli běžné psaní; Escape záznam zastaví. Před převzetím výsledek zkontrolujte.

V kroku jasu vyberte všechny podporované displeje nebo konkrétní monitor. Nedostupný vybraný monitor způsobí chybu, místo aby tiše změnil jiný displej. Přístrojová deska zobrazuje živý jas, zvuk a hodnoty OBS, pokud jsou k dispozici jejich příslušné integrace; nedostupná hodnota není prezentována jako naměřená hodnota.

V editoru dlaždice přiřaďte podporovanou fyzickou speciální klávesu a vyberte okamžité stisknutí, krátké stisknutí, podržení nebo dvojité stisknutí. Podržené voliče vrstev dočasně aktivují jiné mapování a při uvolnění obnoví předchozí vrstvu. Gesta a vrstvy platí pro klávesy G Corsair SDK a standardní speciální klávesy konkrétního zařízení, nikoli pro běžné globální zkratky. Zachycení standardních kláves podporuje F13–F24 a multimediální klávesy klávesnice. Proprietární ovládání Fn, profilu a otočného ovladače stále vyžaduje kompatibilní adaptér výrobce nebo mapování výrobce na standardní klávesu. Raw Input klávesu sleduje a nepotlačuje její běžné chování ve Windows. Diagnostika kláves vysvětluje problémy profilu, pauzy, připojení a vazeb.

Synchronizace profilů vyměňuje soubory přes vybranou složku, včetně složky spravované vaším stávajícím cloudovým klientem. Před importem zkontrolujte náhled; importované akce jsou zpočátku zakázané a hardwarově specifická mapování se odstraní. Revize zabraňují tichému přepsání a zahrnutí obsahu akcí je výslovná volba. MAKRIVO neposkytuje cloudový účet ani neuchovává cloudové přihlašovací údaje. Oznamovací oblast nyní používá vlastní tyrkysovou ikonu M MAKRIVO a při pozastavení zobrazuje jantarovou značku pauzy.

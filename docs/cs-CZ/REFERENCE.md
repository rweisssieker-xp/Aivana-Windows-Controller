<!-- generated-doc locale=cs-CZ source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../README.md)

# MAKRIVO

[Kompletní dokumentace ve všech jazycích 27](README.md) · [Úplný uživatelský manuál](HANDBOOK.md)

**Na Aivana.** [Aktuální inventář a omezení uvolnění](FEATURES.md) · [Rozšíření testování](testing/2026-10-12-six-extensions.md) · [Místní asistent prototypu](testing/2026-10-12-local-meeting-assistant.md)

**Eangličtina (USA)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Místní ovladač klávesových zkratek Windows s dlaždicemi, profily, makry a detekcí klávesnice ve stylu Stream Deck. Rozhraní podporuje jazyky 27. Pro každodenní použití není potřeba žádný účet ani cloudové připojení.

[Dokumentace](README.md) · [Ověření](VERIFICATION.md) · [Funkční testová zpráva](testing/2026-10-09-usp-release.md)

## Začínáme

Po zveřejnění je aplikace na `artifacts/win-x64/MAKRIVO.exe`. Udržujte celou výstupní složku pohromadě; můžete zkopírovat jinam. Windows 11 x64 je počáteční cíl. Artefakty sestavení jsou z Gitu vyloučeny; kontrola zdroje nezahrnuje předem sestavený spustitelný soubor.

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
3. Přidejte jeden nebo více kroků: aplikace, soubory, webové stránky, text, zástupce, média, svazek, přepínání profilů nebo zpoždění.
4. Uložit. Změny zůstávají konceptem, dokud nejsou uloženy; zahodit obnoví uloženou verzi. Tlačítko Přehrát dlaždice a jeho klávesová zkratka používají stejnou cestu spuštění.
5. Změňte pořadí dlaždic a kroků makra pomocí přetažení nebo tlačítek se šipkami. Šablony zjednodušují nastavení. Makra se mohou opakovat až 100 krát. Ve výchozím nastavení zrušte prostřednictvím rozhraní, nabídky na hlavním panelu nebo Ctrl+Alt+Escape.

Profily specifické pro aplikaci používají název procesu bez jeho úplné cesty. Pořadí profilů přerušuje vazby; výchozí profil je záložní. V Nastavení musí být povolen automatický výběr profilu.

Zavřením hlavního okna zůstane aplikace ve výchozím nastavení na systémové liště. Poklepáním na ikonu na hlavním panelu jej znovu otevřete; k ukončení použijte nabídku zásobníku. Spuštění pomocí Windows je volitelné a ve výchozím nastavení zakázáno. Klávesové zkratky lze centrálně pozastavit.

## Paleta akcí a příkazů

Přidat akci otevře prohledávatelný katalog s předvolbami 90 v osmi kategoriích. Každá má popis a náhled. Editor maker používá stejný katalog k přidání nebo nahrazení kroků. Výběrem předvolby v tomto pracovním postupu editoru vytvoříte koncept bez provedení.

Paleta příkazů se otevře v záhlaví nebo pomocí Ctrl+K uvnitř aplikace. Vyhledává uložené akce napříč profily, předvolby katalogu, přepínání profilů, navigaci, pozastavení/obnovení, zrušení maker a import/export. Filtrujte podle uložených akcí, funkcí katalogu nebo příkazů aplikace. Šipkami vyberte, Enter aktivuje, Ctrl+Enter upraví upravitelný záznam a Escape se zavře. Oblíbené se zobrazí jako první. Příkazy katalogu připravené k použití lze okamžitě spustit; předvolby vyžadující nastavení otevřít koncept. Nastavení může definovat globální zástupce palety. Nové konfigurace používají Ctrl+Shift+Mezerník; existující konfigurace neobdrží nové globální zástupce automaticky.

Zvukové akce nastavují hlasitost přesně od 0 do 100 procent a ztlumí, zapnou nebo přepínají výstupní a mikrofonní zařízení. Vyberte konkrétní koncový bod nebo jej ponechte nenastavený pro výchozí Windows. Nedostupný uložený koncový bod způsobí chybu namísto ovládání jiného zařízení. Obnovit zařízení znovu načte připojená zařízení.

OBS Studio používá WebSocket v5: spuštění/zastavení/přepnutí/pauza/obnovení nahrávání; spustit/zastavit/přepnout streamování; měnit scény; přepnout ztlumení vstupu. Povolte server WebSocket v části Nástroje v OBS a nastavte jeho koncový bod v editoru. Check Connection and Load Names načte verzi OBS, scény a vstupy bez spuštění nahrávání nebo streamu. Vyberte jména ze seznamu nebo je zadejte ručně. Hesla lze uložit do Windows Credential Manager a jsou vyloučena z exportovaných konfigurací. Nedostupná připojení OBS hlásí chybu.

Profily mohou odpovídat názvu procesu a části názvu okna. Obě zadané podmínky se musí shodovat. Lock Profile zabraňuje automatickému přepínání pro relaci. Zachycení podmínek z popředí využívá naposledy zaměřenou externí aplikaci.

Podpora akcí s textem a schránkou `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`a `{{user}}`. Použití dat `yyyy-MM-dd`; časy použití `HH:mm`. Neznámé zástupné symboly zůstávají nezměněny. `{{profile}}` identifikuje profil spuštěné akce; `{{process}}` identifikuje kontext externí aplikace. Obsah schránky se čte pouze při použití tohoto zástupného symbolu. Akce schránky zapíše text, ale nevloží jej automaticky.

Akce okna minimalizují, maximalizují, obnovují nebo uspořádávají okno na levé/pravé straně pracovní plochy monitoru. Akce systému uzamkne Windows. Tyto akce se spouštějí pouze při explicitním vyvolání.

Náhled zobrazuje kroky, opakování a proměnné bez provedení. Zpět a Znovu si zachovají stavy konceptu 100; textová pole zachovávají nativní úpravy pomocí Ctrl+Z. Označte oblíbené položky v nabídce editoru nebo dlaždic a filtrujte je na řídicím panelu. Počty provedení se vztahují pouze na aktuální relaci.

Akce textu, zkratky a okna spuštěné z rozhraní aktivují poslední externí cílové okno. Paleta zobrazuje cílový kontext. Pokud se cíl uzavřel nebo Windows odmítne aktivaci, provádění se zastaví se zprávou.

## Pomoc s klávesnicí a zpětná vazba

Průvodce speciální klávesou je na stránce Klávesnice a na paletě. Uvádí G-keys hlášené iCUE. Detect Key Press je dočasně zachytí, aniž by provedly jim přiřazené akce. Normální vazby se vrátí po detekci, zrušení nebo uzavření. Funkce katalogu a vybraný klíč se otevřou jako neuložený koncept; duplicitní vazby jsou kontrolovány.

Překrytí klávesnice zobrazuje vybrané referenční rozložení a aktivní vazby. Podržte Ctrl+Alt+F1 nebo jej přepněte na stránce palety nebo klávesnice. Nezaměřuje se a propouští kliknutí myší. Změny profilu a vazby se aktualizují, když jsou viditelné; běžné stisknutí klávesy Windows je zvýrazněno. Ovládací prvky specifické pro výrobce nemusí poskytovat ekvivalentní signál klíče Windows.

Zpětná vazba na provedení ukazuje čekání, postup kroků/opakování, dokončení, zrušení nebo selhání, aniž by se zaostřilo nebo odkryl zadaný text. Commands Executed potvrzuje provedení příkazu, nikoli nezávisle dotazovaný živý stav v aplikaci třetí strany. Podrobnosti o chybě se zobrazí v protokolu aktivit.

## Zařízení a Corsair

Corsair G1–G6 lze přiřadit přímo prostřednictvím oficiální sady iCUE SDK. Vyberte zařízení a klíč v editoru akcí a uložte nebo připravte koncept ze zobrazení klávesnice K100. Vazby se vztahují na vybrané zařízení a aktivní profil; pauza je také deaktivuje. iCUE musí být spuštěné a musí umožnit přístup. Fn, ovládací prvky kolečka, profilu a zámku iCUE nelze mapovat prostřednictvím tohoto rozhraní G-key API.

Stránka Zařízení uvádí rozhraní klávesnice Windows a pravidelně se obnovuje a při připojení zařízení. Fyzická klávesnice může odhalit více rozhraní HID. Standardní globální klávesové zkratky fungují napříč výrobci.

Detekce Corsair využívá dostupné informace o zařízení. Ovládání RGB vyžaduje kompatibilní hardware, běžící iCUE a oficiální iCUE SDK; aplikace hlásí skutečnou dostupnost. Ostatní výrobci v současné době dostávají detekci a standardní akce. Mimo integraci G-key Corsair fungují proprietární ovládací prvky, když je Windows přijme jako běžný vstup. Aplikace neinstaluje ovladač klávesnice.

## Jazyky

Nastavení nabízí jazyky 27: němčina, angličtina, francouzština, španělština, italština, portugalština, holandština, polština, čeština, slovenština, maďarština, rumunština, bulharština, řečtina, dánština, švédština, norština, finština, estonština, lotyština, litevština, slovinština, chorvatština, ukrajinština, ruština, turečtina a srbština v latince. Automaticky se řídí jazykem zobrazení Windows. Překlady jsou lokálně baleny. Rozvržení, klávesové zkratky a názvy vytvořené uživatelem jsou nezávislé na jazyku rozhraní.

Kompletní dokumentace je k dispozici ve všech jazycích aplikace 27 prostřednictvím [Jazykový index](README.md)A to Překlady jsou strojově podporované, s originálními odkazy a strukturálními kontrolami; recenze původního řečníka je oddělená. Aplikace také přijímá regionální kódy `en-US`, `de-DE`, `fr-FR` a `it-IT` a používá jejich číselné formáty. Existující jazykové kódy jsou kompatibilní.

## Rozložení a průzkum výrobce

Zobrazení klávesnice poskytuje reference ISO-DE/ANSI-US v plné velikosti a bez použití kláves a navíc rozložení RGB Corsair K100. Geometrie představuje pozice, široké/vysoké klávesy a ISO Enter. Standardní rozvržení jsou obecné odkazy. Rozvržení K100 se řídí zdokumentovanými ovládacími prvky bez nároku na milimetrově přesné rozměry. Vyberte správný jazyk/geometrickou variantu: název modelu nebo USB VID/PID neurčuje konkrétní variantu.

Mapování řeší fyzické skenovací kódy pomocí aktivního rozložení vstupu Windows. Pracovní postup připraví koncept a zachová kontroly uložení/konfliktů. Standardní vazby jsou globální klávesové zkratky Windows, které nejsou izolované na jedné připojené klávesnici. Corsair G1–G6 použijte integraci iCUE specifickou pro zařízení. Jiné proprietární ovládací prvky vyžadují adaptér nebo výrobcem nakonfigurované zástupce. Main Enter a numerická klávesnice Enter sdílejí identitu virtuálního klíče pro globální klávesové zkratky.

a to [Výrobce výzkumu](research/tastaturhersteller-sonderfunktionen.md) a [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokrývá 74 skupiny značek/výrobců, rozlišují konfigurátory, vstupní SDK a osvětlení API. [Layout důkazů](research/keyboard-layout-evidence.md) Modely, modely a rozdíly. Výzkumné pokrytí neznamená, že je prováděna podpora hardwaru.

## Data

Konfigurace je uložena v datech místní aplikace. Import/export použijte JSON. Import zobrazí náhled konfigurace, po potvrzení nahradí aktuální konfiguraci a zakáže importované vazby, dokud je ručně nepovolíte. Obsah textových akcí se neshromažďuje jako protokol úhozů. Hesla Credential Manager jsou oddělena od exportovaných profilů.

## Ověření

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Kontroly Windows mohou kontrolovat hardware a dočasně konfigurovat/uvolňovat podporované vazby iCUE. Spusťte je bez dalšího ovladače pomocí těchto vazeb. Kontroly koncových bodů zvuku nemění hlasitost živého vysílání ani nastavení ztlumení.

Nativní vstup nemůže obejít oprávnění Windows. Normálně spuštěný řadič nemusí odesílat vstup do aplikace správce. Rezervované nebo obsazené klávesové zkratky se objevují jako konflikty.

a to [Dokumentační index](README.md) odkaz historických návrhů, plánů, výzkumu a datovaných testovacích zpráv. Historické plány popisují jejich zaznamenaný rozsah, ne aktuální záruku podpory.

### Šablony pracovních postupů a čekání na cílové okno

Katalog obsahuje šest upravitelných pracovních postupů: pracovní prostor, schůzka, streamování, výzkum, změna projektu a úryvek textu. Vyhledejte `workflow.` v katalogu nebo na paletě příkazů. Výběrem pracovního postupu se otevře neuložený koncept; nikdy neběží okamžitě. Všechny kroky jsou zachovány při přidávání pracovního postupu do existujícího makra nebo jeho přiřazení ke klávese Corsair. Před uložením nakonfigurujte příklady URL, programové cesty, profily a scény OBS.

Krok **Wait for target window** přijímá název procesu (s nebo bez `.exe`), podřetězec názvu nerozlišující malá a velká písmena a časový limit 1–600,000 ms. Je vyžadován alespoň jeden filtr; oba filtry se musí při dodání shodovat. Makro pokračuje až poté, co odpovídající viditelné okno odpoví a dosáhne popředí. Pokud proces sdílí více oken, použijte konkrétní název. Zrušení nebo časový limit zastaví následující kroky. Připravenost okna nezaručuje, že se dokument načetl nebo že je připojena externí služba, jako je OBS WebSocket.

Jas displeje: vyhledat **Zvyšte jas obrazovky**, **Snižte jas obrazovky**nebo **Jas obrazovky při 50 procent** v katalogu akcí. Relativní akce mění jas o 10 procentní body; pevné hodnoty se pohybují od 0 na 100. Podporované interní displeje používají WMI; vyžadují kompatibilní externí monitory DDC/CI povoleno v jejich nabídce. Ve výchozím nastavení tato akce ovlivní všechny podporované displeje; v editoru lze vybrat explicitní monitor. Připojení a doky mohou omezit podporu.

### Rozšířené ovládání

Otevřete paletu příkazů pomocí **Ctrl+K, když je okno aplikace aktivní**. Hledat **Návrhář pracovních postupů**, **Záznam makra**, **Synchronizovat profily**nebo **Klíčová diagnostika** (znění odpovídá vámi zvolenému jazyku). Návrhář pracovního postupu upraví vybraný koncept dlaždice: podmínky souboru/procesu/okna, počet přeskočení vpřed a zastavení/pokračování zpracování chyb. Diktafon zachycuje akordy zkratek a čeká, ne obyčejné psaní; Escape zastaví nahrávání. Před přijetím zkontrolujte výsledek.

V kroku jasu vyberte všechny podporované displeje nebo konkrétní monitor. Nedostupný vybraný monitor způsobí chybu, místo aby tiše změnil jiný displej. Přístrojová deska zobrazuje živý jas, zvuk a hodnoty OBS, pokud jsou k dispozici jejich příslušné integrace; nedostupná hodnota není prezentována jako naměřená hodnota.

V editoru dlaždic přiřaďte podporovanou fyzickou speciální klávesu a zvolte okamžité stisknutí, klepnutí, podržení nebo dvojité klepnutí. Podržené selektory vrstev dočasně aktivují další mapování a obnoví předchozí vrstvu při vydání. Gesta a vrstvy se vztahují na G-klávesy Corsair SDK a standardní speciální klávesy specifické pro zařízení, nikoli běžné globální klávesové zkratky. Standardní zachycení kláves podporuje F13–F24 a klávesy médií na klávesnici. Proprietární ovládání Fn, profilu a číselníku stále vyžaduje kompatibilní adaptér dodavatele nebo mapování dodavatele na standardní klíč. Raw Input sleduje klíč a nepotlačuje jeho normální chování Windows. Klíčová diagnostika vysvětluje problémy profilu, pauzy, připojení a vazby.

Synchronizace profilu vyměňuje soubory prostřednictvím složky, kterou si vyberete, včetně složky spravované vaším stávajícím cloudovým klientem. Před importem zkontrolujte náhled; importy start zakázány a odstraní mapování specifická pro hardware. Revize zabraňují tichému přepsání a zahrnutí obsahu akce je explicitní volbou. MAKRIVO neposkytuje cloudový účet ani neuchovává cloudové přihlašovací údaje. Panel nyní používá vlastní modrozelenou ikonu M MAKRIVO a při pozastavení zobrazuje jantarový odznak pozastavení.

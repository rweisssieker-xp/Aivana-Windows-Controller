<!-- generated-doc locale=cs-CZ source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Návrh

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stav: 8. října 2026. Stav: ke kontrole před implementací.

## Cíl a dohodnutý rozsah

Samostatná aplikace Windows promění běžnou PC klávesnici na volně konfigurovatelný akční panel podobný stream decku. Uživatel potvrdil všechny navrhované typy akcí, makra, detekci zařízení, profily a dobré GUI. Připojené klávesnice Corsair a další klávesnice jsou rozpoznány; další funkce výrobce jsou nabízeny pouze v případě, že jsou skutečně podporovány.

Předpoklady: Německé rozhraní, lokální použití bez účtu, Windows 11 x64 jako první cíl, žádná instalace ovladače. Klávesnice zůstává použitelná pro běžné psaní. Globální mapování standardně používá klávesové zkratky s modifikátory. Jednotlivá písmena nejsou ve výchozím nastavení globálně rezervována.

## Technické rozhodnutí

Nativní C#/.NET-10-App s WPF a MVVM. V pracovním prostoru je k dispozici stabilní .NET-10-SDK. WPF umožňuje přizpůsobené desktopové rozhraní a přímou integraci Windows. Alternativou by bylo webové rozhraní se samostatnou službou Windows nebo desktopová aplikace pro více platforem; oba přinášejí do tohoto projektu Windows další provozní a komunikační složitost. Nativní varianta je tedy zvoleným základem.

Divize: Desktopový projekt pro pohledy a zásobník, hlavní projekt pro konfiguraci a logiku provádění, projekt Windows pro klávesové zkratky, vstupní simulaci a zařízení, testovací projekt pro pravidla nezávislá na platformě. Adaptéry výrobce závisí na rozhraní s pevnou schopností a jsou nezávislé na standardních akcích.

## povrch

Tmavý design s klidnými antracitovými povrchy, snadno čitelným písmem a tyrkysovými akcenty. Propojuje navigaci pro řídicí panel, profily, zařízení a nastavení. Aktivní profil, vyhledávání a přepnutí na pozastavení všech klávesových zkratek nahoře. Rozhraní zůstává použitelné při škálování 100–200 procent Windows.

Palubní deska obsahuje přizpůsobitelnou mřížku dlaždic. Každá dlaždice zobrazuje ikonu, název, klávesovou zkratku a stav provádění. Kliknutí provede stejnou akci jako klávesová zkratka. Vpravo se otevře editor s typem akce, parametry, záznamem klávesové zkratky, symbolem/barvou, aktivačním a testovacím tlačítkem. K dispozici jsou možnosti Nový, Duplikovat, Smazat a Změnit pořadí. Pohled na klávesnici schematicky ukazuje přiřazení pro standardní německé rozložení; nenárokuje si přesnou geometrii detekovaného zařízení.

Profily lze vytvářet, kopírovat, přejmenovávat a aktivovat ručně. Název procesu je volitelně přiřazen k profilu. Vhodný program v popředí aktivuje jeho profil, jinak platí globální standardní profil. Pokud existuje několik odpovídajících profilů, platí výslovně viditelné pořadí. Editor vysvětluje konflikty klávesových zkratek přímo v zadání.

## Klávesové zkratky a vstupy

Windows RegisterHotKey zvládá normální globální kombinace s potlačeným opakováním kláves. Registrován je pouze aktivní profil. Rezervované nebo již obsazené kombinace se objeví jako chyby; aplikace nehlásí, že byla úspěšně aktivována. Záznam v editoru nespouští žádné akce.

Před simulovanými vstupy jsou uvolněny modifikační klávesy spouštění nebo se čeká na jejich fyzické uvolnění. Text Unicode a kombinace kláves jsou odesílány pomocí SendInput. Každá klávesa, kterou stisknete, bude znovu uvolněna, i když ji zrušíte. Akce běží mimo vlákno uživatelského rozhraní; Změny provedení a profilu nesmí blokovat povrch.

## Akce a makra

Typy akcí: Spuštění programu s argumenty a pracovním adresářem, otevření souboru/složky přes Windows, otevření webové stránky HTTP/HTTPS, zadání textu Unicode, odeslání kombinace kláves, přehrávání médií/pauza/následující/předchozí/zastavení, zvýšení/snížení/ztlumení a přepnutí profilu. Parametry jsou ověřeny před uložením a znovu před provedením. Programy se spouštějí přímo; Argumenty nejsou implicitně interpretovány jako příkazy shellu.

Makra obsahují uspořádaný seznam pojmenovaných akcí a explicitní kroky pozastavení. Kroky lze přidávat, upravovat a přesouvat. Opakování je konečně konfigurovatelné (1–100); Nekonečné smyčky nejsou součástí první verze. Běžící makro lze ukončit pomocí rozhraní nebo vlastní klávesovou zkratkou pro zrušení. Opakované spouštění stejného přiřazení proudu nespustí paralelní běh. Různá makra se simulací vstupu jsou serializována tak, aby se jejich stisk kláves nemíchal. Pokud dojde k chybě kroku, makro se zastaví a zobrazí dotčený krok.

## Zařízení a výrobci

Inventář zařízení prostřednictvím Windows Raw Input a další informace o zařízení: zobrazovaný název, cesta k zařízení, identifikátor výrobce/produktu, pokud je k dispozici, stav připojení a zjištěné možnosti. Připojení/odpojení aktualizuje seznam bez restartování. Fyzické zařízení může mít více záznamů HID; Ty jsou seskupeny podle spolehlivých identifikátorů, jinak jsou vedeny viditelně jako samostatné položky.

Normální klávesové zkratky platí pro všechny klávesnice. Zobrazení zařízení poskytuje informace a možnosti; samostatná klávesnice s výhradně zachycenými klávesami není součástí tohoto dohodnutého režimu.

Detekce Corsair funguje také bez iCUE. Volitelný adaptér SDK iCUE poskytuje stav připojení a podporované akce osvětlení, když je přítomno SDK, iCUE a kompatibilní zařízení. Uživatelské rozhraní rozlišuje rozpoznávané, dostupné SDK, připojené a skutečně ovladatelné. SDK primárně dokumentuje ovládání LED; Proprietární G-klávesy jsou podporovány jako normální klávesové zkratky pouze tehdy, pokud Windows obdrží odpovídající vstup. Neexistuje žádný paušální příslib pro všechny speciální klíče.

Ostatní výrobci zpočátku obdrží obecnou detekci a standardní akce. Rozhraní adaptéru umožňuje pozdější integrace; nepodporované dovednosti jsou čestně zobrazeny. Hardwarové testy pro neexistující modely nejsou hlášeny jako úspěšné.

## Skladování a každodenní život

Konfigurace JSON s verzí v místním datovém adresáři aplikace. Atomové uložení s poslední platnou zálohou; nesprávné soubory nejsou tiše přepsány. Import zkontroluje formát, verzi, ID a parametry a před použitím zobrazí náhled. Importované akce lze provést pouze po explicitní aktivaci. Export obsahuje profily a přiřazení.

Nabídka zásobníku: Otevřít, výběr profilu, pozastavit klávesové zkratky, zrušit makra, ukončit. Zavřít okno se ve výchozím nastavení minimalizuje do zásobníku a poprvé to vysvětluje. Autostart je volitelný a ve výchozím nastavení vypnutý. Pouze jedna instance spravuje klávesové zkratky. Exit registruje klávesové zkratky, přeruší provádění a uvolní prostředky.

Zobrazení místní aktivity s omezenou historií úspěchu, neúspěchu a zrušení. Žádný protokol všech úhozů kláves. Obsah textové akce není v chybových zprávách uveden. Žádná cloudová služba a žádná telemetrie.

## Ošetření chyb a limity

Chybějící programy, neplatné adresy URL, obsazené klávesové zkratky, poškozené profily a nedostupné SDK výrobce dostávají specifické německé zprávy. Selhání adaptéru neovlivní standardní akce. Vstupy do aplikací s vyšším oprávněním mohou selhat kvůli limitům oprávnění Windows; Chyby jsou vysvětleny, aplikace ve výchozím nastavení nevyžaduje práva správce.

## Testování a akceptace

Automatické kontroly priority profilu, detekce konfliktů, import konfigurace, atomické úložiště, řazení maker, opakování, zrušení a šíření chyb. Windows kouřové testy pro skutečné klávesové zkratky, textové a klávesové zkratky, akce médií, inventář zařízení, zásobník a čisté ukončení. Žádné testy, které náhodně spouštějí libovolné programy nebo webové stránky.

Kontrola GUI: Vytváření a úpravy přiřazení, nahrávání bez provedení, kliknutí/klávesová zkratka se stejným efektem, zrušení makra, zpráva o konfliktu, změna profilu, zobrazení zařízení, import/export a změna měřítka. Testování hardwaru specifické pro výrobce je dokumentováno samostatně u skutečného zařízení.

Dodávka: Zdrojový kód, reprodukovatelné pokyny pro sestavení/spuštění, německý návod k použití a místní Windows-x64-Publish-Artefakt. Úspěch znamená spustitelnou aplikaci se všemi obecnými funkcemi; Funkce Corsair jsou označeny jako dostupné pouze s osvědčenou podporou SDK/zařízení.

## Technické zdroje

- Microsoft Vstup klávesnice / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Vstup: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Oficiální Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Další krok

Po přezkoumání tohoto návrhu: vytvořte konkrétní plán implementace a předložte jej k výběru implementace. V prázdné pracovní složce zatím není žádné úložiště Git; Tento dokument proto dosud nebyl potvrzen.

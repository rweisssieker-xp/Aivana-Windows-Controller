<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-12-six-extensions.md)

# Šest rozšíření: implementace a testovací stolice

Stav: 12. Říjen 2026, Obor `codex/six-extensions`. Pracovní stav, bez uvedení do výroby.

## Servis

Paleta příkazů otevře položky **Nastavte pracovní prostor**, **Výrobci klávesnic a SDK**, **Mobilní panel**, **Rozšíření**, **automatizace uživatelského rozhraní**, **Zaznamenejte tok uživatelského rozhraní** a **Kontrola provedení**.

- Workspace Assistant vytváří profily Workplace, Meeting, Streaming, Research a Project. Programy z Registry/Start Menu nebo File Selector, URL, Output/screen a OBS Scene se zobrazí jako kroky před uložením. Zařízení neprovádí žádné akce.
- V editoru kroků **Check result** zapíná předběžnou kontrolu a následnou kontrolu. Historie a mobilní panel rozlišují skutečné potvrzení od neověřitelného účinku. Relativní změny, klávesové zkratky a zadávání textu neposkytují obecné potvrzení účinnosti. Doklad o procesu neprokazuje technickou připravenost žádosti.
- UI Automation používá samostatného, ​​časově omezeného pracovníka. Výběr pomocí pozice myši, kontrola selektoru, Invoke/Select/Toggle/Focus/SetValue/WaitState; vícenásobné zásahy, deaktivované cíle a pole hesla jsou odmítnuta. Záznam zachycuje podporované akce kliknutí a pauzy; Textové položky se nezaznamenávají. Přijetí bude před přijetím zkontrolováno.
- Rozšíření: Kontrola ZIP, hash, schéma, zakázaný import, explicitní důvěra před aktivací, izolovaný proces s omezeným IPC, export, deaktivace, odinstalace, obnovení/zahození předchozí verze. Izolace procesů není sandbox: sdílené pluginy běží s uživatelskými právy. Ukázkový balíček `makrivo.example.zip` obsahuje odpočítávání a stavovou akci. Podporovaná schémata představují zdokumentovanou jednoduchou podmnožinu schémat JSON ve zdrojovém kódu; žádné veřejné tržiště.
- Mobilní panel: vyberte místní IP a sdílené profily, otevřete HTTPS adresu, zkontrolujte certifikát pomocí zobrazeného SHA-256-Fingerprints a vědomě mu důvěřujte, poté zadejte jednorázový šestimístný kód. Soukromé klíče jsou chráněny pomocí Windows-DPAPI; jsou uloženy pouze hashe klientského přístupu. Certifikáty se mění po obnovení nebo změně IP. V případě potřeby uvolní uživatel bránu firewall Windows. Žádná automatická instalace certifikátu, přesměrování portů nebo změna firewallu. Klientský přístup lze zrušit. Panel zobrazuje stav akce a historii testů; ještě to není univerzální přístrojová deska živého senzoru.

## Klávesnice

| Výrobce | Implementace | Omezení |
|---|---|---|
| Corsair | stávající klávesy iCUE-G a RGB | Je vyžadováno místní připojení iCUE a vhodný model |
| Logitech G | G-Key SDK a samostatné LED SDK | explicitně uvolnit původní místní x64-DLL; G-Key SDK neposkytuje individuální identitu více klávesnic |
| Razer | Chroma RGB a standardní rozpoznávání klíčů | Přiřaďte proprietární klíče dříve v Synapse |
| SteelSeries | GameSense-RGB a standardní rozpoznávání klíčů | přiřadit proprietární klíče dříve v GG |
| Wooting | Analogové hodnoty související se zařízením, prahové hodnoty tlaku/uvolnění | původní místní x64-SDK-DLL; normální HID vstup zůstává aktivní |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat | Standardní fyzická tlačítka podle zařízení a skenovacího kódu | Fn a proprietární ovládání závisí na modelu |

Nejsou zahrnuty žádné nové proprietární knihovny DLL SDK. Adaptéry nového výrobce ještě nejsou hardwarově ověřeny bez vhodných fyzických zařízení.

**EExkluzivní druhý makropad není připraven:** Raw Input sleduje zařízení samostatně, ale nepotlačuje jejich normální vstup Windows. Produkční vydání vyžaduje licencovaný, podepsaný ovladač filtru ověřený podle Windows 11/Secure Testování spuštění a nouzového uvolnění, selhání, pozastavení a opětovného připojení. Nebyl nainstalován žádný ovladač. Aplikace tento limit výslovně označuje.

## Zkoušky

- Sestavení vydání: úspěšné, bez varování/chyb.
- Core Console: Všechny testovací skupiny projdou, včetně konfliktů klíčů dodavatele, vrácení zásuvných modulů a skutečných chyb provádění v historii testování.
- Integrace rozšíření: skutečný pracovník UIA s testovací aplikací WPF; jedinečné vyvolání, nejednoznačnost, zakázané cíle, vyloučení hesla. Proces pluginu skutečného pojmenovaného potrubí, hranice protokolu, analogová hystereze a dekódování Logitech-ABI úspěšné.
- Skutečný hostitel HTTPS: připnutý certifikát, nepárový přístup, falešný/jedinečný kód, ochrana původu, soubory prohlížeče, úložiště hash a chráněný klíč úspěšný. Byla opravena chyba Schannel s dočasnými klíči objevená při testování.
- Sada Windows s explicitně vynechaným testem úspěšnosti v popředí: úspěšný; Zvuk/OBS/iCUE/Raw Input a stav obrazovky zkontrolován. V předchozím běhu celá sada selhala, protože bylo odmítnuto přepnutí fokusu do izolovaného testovacího okna. Následný běh na prototyp AI také zahrnoval kompletní sadu včetně přepínání zaostření a připojené klávesnice iCUE. Další opakovaný běh se opět nezdařil kvůli změně zaměření; Kontextově závislý test tedy není stabilně zelený. Tento test nebyl utlumen. Přístupy pro čtení DDC, které na krátkou dobu selžou, se po omezenou dobu opakují; jas se nezměnil.
- WPF-Smoke: vykresleny všechny předchozí a nové dialogy, neměnnost konfigurace, editor a CRUD úspěšné; Snímky obrazovky ve složce sestavení `smoke`.
- Prohlížeč: Playwright with Edge, 390×844 a 1024×768, propojení toku/UUID/jednorázové odeslání/žádné horizontální přetečení úspěšné. API Mock; skutečný test HTTPS samostatně. Obrázky na `artifacts/mobile-browser`. Zásuvný modul prohlížeče nebyl k dispozici, takže běžná záložní verze Playwright.
- Provedena nezávislá revize kódu; Opraveny zpožděné mobilní příkazy, chybějící výsledky kontroly terminálu, kontrola cesty procesu, vrácení zásuvných modulů a restart RGB heartbeatu.

Stále otevřené: exkluzivní integrace ovladačů, testování LAN fyzického výrobce/telefonu/tabletu, škálování Windows procent 150/200 a úplná kontrola překladu všech nových textů dialogů. Nové texty dialogů jsou převážně německé; Stávající jazykové katalogy jsou zachovány. Proto celkový požadavek ** nebyl zcela přijat**.

## Vygenerovat balíček

`./tools/publish-complete.ps1 -SelfContained` vytvoří novou složku pod `artifacts/release-*`, včetně plochy, pracovníka UIA, hostitele HTTPS, souborů prohlížeče, importovatelného ukázkového pluginu a poznámek k runtime licenci. Nainstalovaná aplikace se nepřepíše. Bez `-SelfContained` musí balíček odpovídat .NET-10-Desktop-/ASP.NET-Runtimes.

Balíček je místní testovací prostředí, nikoli podepsaný maloobchodní instalační program. Vlastní podmínky použití a distribuční práva SDK jsou samostatná vydání; viz stávající dokumentace pod `docs/licensing`.

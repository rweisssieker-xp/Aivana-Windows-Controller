<!-- generated-doc locale=cs-CZ source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Zkontrolujte jednorázový komerční prodej

Stav: 12. října 2026. Výrobce: Aivana.

## Výsledek

V auditované inventuře nebyla zjištěna žádná obecná povinnost zveřejnit vlastní zdrojový kód MAKRIVO. Vlastní jednorázový prodej se zdá možný, ale ještě není plně připraven. Zejména není zdokumentováno oprávnění k redistribuci pro existující Corsair-SDK-DLL. Tento přezkum nenahrazuje právní schválení.

## Technické a licenční nálezy

| komponenta | Zjištění | Požadovaný krok |
| --- | --- | --- |
| Vlastní zdrojový kód | Ve zkoumaném inventáři Git a jeho lokálně dostupné historii licenčních souborů nebyla nalezena žádná vlastní LICENCE, KOPÍROVÁNÍ nebo UPOZORNĚNÍ. | Potvrdit vlastnictví práv včetně příspěvků, přijatých fragmentů kódu a případných práv zaměstnavatele; vytvořte si vlastní podmínky použití. |
| .NET/Windows Desktop | Zkoumaný samostatný balíček pod `artifacts/makrivo-win-x64` obsahuje verzi 10.0.6. Lokální runtime balíčky obsahují licence MIT, které umožňují komerční distribuci a vyžadují zahrnutí upozornění. Ve zkoumaných výstupních složkách nebyly nalezeny žádné odpovídající soubory licencí/upozornění. | Zahrňte přesné licenční texty a příslušné informace třetích stran pro runtime skutečně dodané v prodejním balíčku. |
| Závislosti NuGet | Ve zkoumaných souborech projektu nebyly nalezeny žádné přímé položky PackageReference. Zkoumaný soubor desktopových aktiv obsahuje vlastní závislosti projektu. Runtime balíčky stále zůstávají licenčně relevantní. | Po konečném vydání znovu inventarizujte všechny projekty, tranzitivní balíčky a přibalené soubory. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, verze souboru 4.0.84.0. Lokální referenční PDF SDK obsahuje na vytištěných stránkách 35–38 EULA: použití bez licenčních poplatků, ale omezení převodu/postoupení. Nebylo jasné oprávnění předat DLL zákazníkům. | Před seskupováním získejte robustní povolení k redistribuci. Případně zkontrolujte balíček bez SDK DLL, který používá zákazníkem nainstalovaný SDK; Na to se vztahují i ​​SDK podmínky. |
| Různé balíčky | `artifacts/makrivo-win-x64` neobsahuje Corsair DLL; Obsahuje je `artifacts/win-x64`. Aplikace dynamicky hledá SDK na více místech. | Nastavte skutečný maloobchodní instalační program a zkontrolujte jeho obsah. Samotná existence balíčku bez DLL nepotvrzuje všechna integrační práva. |
| OBS | Vlastní klient WebSocket ve zkoumaném kódu C#; nebyl identifikován žádný klientský balíček OBS, binární OBS nebo převzatý kód GPL. | Pozorovaná integrace protokolu sama o sobě neospravedlňuje žádnou stanovenou povinnost GPL zveřejňovat informace. Původ jakýchkoli fragmentů, které mohly být přijaty, zbývá potvrdit. |
| Ikony aplikací | Místní skript Python kreslí geometrické symboly pomocí Pillow. Nástroj je závislý na sestavení. | Dokumentujte původ a práva všech skutečně dodaných obrázků, zvuků, písem a dalších aktiv. Samotný skript neprokazuje práva ke každému aktivu. |
| Správa licencí | Ve zkoumaném kódu aplikace není uvedena žádná úplná kontrola nákupu licence/aktivace. | V případě potřeby implementujte podepsané offline licenční soubory. Technická aktivace a právní podmínky použití jsou samostatné úkoly. |

## Rozumný model nákupu – dosud neimplementován

- jednorázová platba; Trvalé právo užívání ke zakoupené hlavní verzi.
- Jedna osoba až na dvou vlastních počítačích; Výslovně umožnit profesionální použití.
- Zahrnout opravy chyb a drobné aktualizace; Volitelně za poplatek nabídněte pozdější hlavní verze.
- Digitálně podepsaný licenční soubor, který lze zkontrolovat offline; Nikdy nedoručujte soukromé podpisové klíče s aplikací.
- Pravidlo pro hlavní verze nesmí plošně vyloučit zákonná práva a požadované aktualizace zabezpečení.

## Otevřeno před prvním prodejem

1. Corsair Ujasněte si distribuční práva nebo se připravte na prodej bez přibalené knihovny DLL.
2. Zahrňte licence a texty třetích stran pro přesné závislosti vydání a získejte je v instalačním programu.
3. Potvrďte vlastnictví práv k vašemu vlastnímu kódu a aktivům; Vytvořte EULA s jasnými pravidly pro zařízení, uživatele a upgrade.
4. U spotřebitelského prodeje zpracujte pokladnu včetně požadavků na informace a zrušení. Plošné prohlášení „žádné náhrady za stahování“ nestačí. Zánik práva na odstoupení od smlouvy v případě okamžitého dodání zboží vyžaduje zákonné požadavky.
5. Znovu zkontrolujte dokončený instalační program včetně pluginů a všech přechodných závislostí. Pracovní kopie se rozšiřuje paralelně; Tento inventář není vydáním budoucích souborů.

## GitHub

Zkoumané úložiště je veřejné. Bez licence open source se kód automaticky nestává volným pro jakékoli další použití. Stále je třeba dodržovat práva platformy GitHub pro veřejná úložiště. Pozdější přepnutí na soukromé neodstraní existující kopie nebo větvení. Během této kontroly nedošlo ke změně viditelnosti a licence.

## Důkazy a prameny

- Místní licence .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` a `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Místní oznámení třetích stran .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Místní Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; extrahovaná referenční PDF na `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT licence](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentace s EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licencování restaurací](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Aktualizace](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – právo na odstoupení od smlouvy](https://www.gesetze-im-internet.de/bgb/__356.html)

Nekontrolováno: kompletní historie externích příspěvků a řetězce práv, vydání ochranné známky, stávající prodejní pokladna, budoucí balíčky pluginů, plná právní účinnost dosud nedostupné EULA.

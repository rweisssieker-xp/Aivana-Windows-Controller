<!-- generated-doc locale=cs-CZ source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../FEATURES.md)

# MAKRIVO – Přehled funkcí

Výrobce: **Aivana**. Stav: **12. Říjen 2026**. Toto je stav vývoje, nikoli prodejní nebo hardwarová verze.

## Ovládání Windows

- Dlaždice, globální klávesové zkratky, paleta příkazů, oblíbené položky a profily závislé na aplikaci.
- Makra s více kroky, opakováním, pozastavením, zrušením a náhledem.
- Otevřít programy, soubory a webové stránky; Používejte text, schránku a klávesové zkratky.
- Uspořádejte okna, uzamkněte Windows, ovládejte zvukové výstupy a mikrofony a změňte jas obrazovky prostřednictvím podporovaných rozhraní.
- OBS-WebSocket-v5-Anbindung pro scény, nahrávání, streamování a zvuk.
- Šablony, Workplace Assistant, Device Discovery a Keyboard Overlay.
- Import a export konfigurací profilů; Kontrola návrhu a detekce konfliktů.
- Lokálně uložená nastavení; pro každodenní použití není potřeba žádný účet.

## Pokročilé funkce v aktuálním zdrojovém kódu

| oblast | Rozsah funkcí | Hranice |
| --- | --- | --- |
| Kontrola výsledků | Historie před inspekcí, po inspekci a testování | Potvrďte pouze ověřitelné účinky; Úspěšné stisknutí klávesy nedokazuje cílový efekt. |
| Asistent na pracovišti | Připravte si profily a procesy z vybraných programů, souborů, URL a dalších akcí | Je vyžadován náhled a uložení; Nastavení nedělá nic. |
| Makro záznam a pracovní postupy | Nahrávání, podmínky, gesta a synchronizace lokálního profilu | Chování a limity jsou dokumentovány v implementačních a zkušebních zprávách. |
| automatizace uživatelského rozhraní | Vyberte cíle, zkontrolujte selektory, zaznamenejte akce a omezené sekvence kliknutí | Samostatný pracovník; nejednoznačné, zakázané a cíle hesla jsou odmítnuty; žádný obecný záznam vkládání textu. |
| Rozšíření | Kontrolujte, importujte, aktivujte na základě explicitní důvěry, exportujte a vracejte balíčky | Procesní izolace není sandbox; není veřejným tržištěm pluginů. |
| Mobilní panel | Lokální HTTPS panel, párování, vybrané profily a stav/historie auditů | Skutečná akceptace LAN telefonu/tabletu je stále otevřená; žádná univerzální senzorová palubní deska. |
| Adaptér výrobce | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB a Wooting analogový adaptér | Závisí na modelu/SDK; nové adaptéry nejsou plně ověřeny skutečným hardwarem. Nejsou přibaleny žádné nové proprietární knihovny DLL SDK. |
| Fyzické mapování klávesnice | Rozpoznejte standardní klíče podle zařízení a naskenujte kód | Raw Input nepotlačuje normální vstup Windows. |
| Místní asistent setkání AI | Volitelný prototyp pro Ollama, LM Studio, llama.cpp a ONNX Runtime GenAI CPU vytváří omezené návrhy z explicitně vybraných zdrojů | Ve výchozím nastavení vypnuto; nativní běhové prostředí CPU testováno s náhodným modelem, ale žádná prokázaná kvalita plánování nebo úspora času; bez modelových závaží. |

## Zatím není k dispozici jako hotová prodejní funkce

- **EExkluzivní sekundární klávesnice/makropad:** Chybí podepsaný, licencovaný a hardwarově testovaný ovladač filtru. Normální stisk kláves může stále dosáhnout Windows.
- Kompletní hardwarová akceptace adaptérů všech výrobců a také reálné testy LAN mobilních zařízení.
- Skutečné smíšené změny monitoru/DPI a technické schválení všech nových překladů. PerMonitorV2 a simulované 150/200-Prozent-Layouts byly zkontrolovány; Testy skutečného zobrazení zůstávají oddělené. Rozhraní nabízí jazyky 27; Podrobné nové texty ještě nejsou zcela jazykově schváleny.
- Podepsaný maloobchodní instalační program, konečná smlouva EULA, ověřená práva na distribuci sady SDK a produktivní integrace nákupní licence a pokladny.

## Podrobnosti a důkazy

- [Používací dokumentace](README.md)
- [Rozšíření: funkce, testy a otevřené hranice](testing/2026-10-12-six-extensions.md)
- [Lokální asistent: prototyp a zkouška](testing/2026-10-12-local-meeting-assistant.md)
- [Místní časy a požadavky na modelování](testing/2026-10-12-local-ai-runtimes.md)
- [DPI a skalování](testing/2026-10-12-dpi-scaling.md)
- [licenční zkouška](licensing/2026-10-12-commercial-license-audit.md) – historické záznamy; nový skript Publish doplňuje návod k běhu. Závěrečný balíček je třeba zkontrolovat samostatně.
- [Distribuční platformy](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Porovnání; Stripe je navíc možnou platební platformou, viz aktuální podmínky poskytovatele.

## Vygenerujte kompletní vývojový balíček

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Balíček obsahuje desktopovou aplikaci, UI automatizačního pracovníka, mobilního hostitele, soubory prohlížeče, ukázkový plugin a poznámky k runtime licenci. Artefakty sestavení nejsou v Gitu kontrolovány. Verze zdrojového kódu GitHub zatím není publikovaným instalačním programem Windows.

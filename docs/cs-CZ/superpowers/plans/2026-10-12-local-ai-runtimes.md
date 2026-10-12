<!-- generated-doc locale=cs-CZ source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Místní běhové časy AI a zbývající kontroly

Cíl: skutečně připojit další místní běhová prostředí AI, přijímat recenze návrhu a zviditelnit otevřené verze. Uživatel schvaluje implementaci bez otázek. Stávající změny jsou zachovány.

1. Přeprava zpětné smyčky a předimplementační testy inspekce paketů ONNX; Zkontrolujte síťové adresy, výstupní limity, přerušení a neznámé zdroje.
2. Sdílené zprávy plánovače; Přijato Ollama, přidáno LM Studio (127.0.0.1:1234) a llama.cpp (127.0.0.1:8080). Žádné cloudové adresy URL ani automatické přepínání poskytovatelů. Místní servery se mohou přeposílat samy; Jejich provoz zůstává v odpovědnosti provozovatele.
3. ONNX Runtime GenAI CPU ve vlastním procesu s časovým limitem, omezeným protokolem JSON a ukončením procesu. Explicitně vyberte složku modelu; žádný model není stažen automaticky. Pouze textové modely kompatibilní se GenAI, žádné libovolné sítě .onnx.
4. WPF výběr pro runtime, model, složka ONNX; všechny poskytují stejný přísně testovaný design.
5. Testovací stolice s jasnými limity pro hardware, DPI, mobilní panel a exkluzivní makro podložku. Fyzické kontroly a podepsaný/licencovaný ovladač zde nelze nahradit softwarem.
6. Sestavení vydání, testování jádra/Windows, kouření WPF, testování pracovníků/balíčků a nezávislá kontrola. Vytvořit nový kompletní balíček; Kvalitu modelu vykazujte jako testovanou pouze v případě, že model skutečně existuje.

Výsledek: implementovány čtyři runtime, ONNX testováno nativně samostatně, volba poskytovatele a testovací stolice v nabitém kouři WPF prošla. Core/Windows suity zelené. Přezkoumejte hledání limitu Unicode se společnou konstantou a opravenou regresí. Skutečné schválení změn hardwaru/mobilu/DPI, vydání ovladače, úplné technické překlady a kvalita živých instrukcí zůstávají explicitně otevřenou prací; nevydáno jako úplné.

Pokračování: prováděné samostatné jinja a jmenované/default vzorky; typové/hraniční/falback regresní testy. 27 Katalogy doplněné. skutečný model Instruct stáhnout samostatně a SHA256 ověřeno: native CPU reference funguje, ale 0/4 Plánování případů splňuje smlouvu JSON; žádné akce prováděné. nezávislý přehled: nulový zpětný úpravou a testováním, po němž není konkrétní další zjištění. Windows- testy procházky, vydání 0 Varování a chyby.

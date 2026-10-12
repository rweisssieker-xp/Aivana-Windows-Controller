<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-12-gui-polish.md)

# Revize GUI od 12.10.2026

- Přímý přístup k „Nástrojům“ v navigaci; tři skupiny s deklarovanými akcemi pro nastavení, vytvoření a testování.
- Čtrnáct existujících oblastí dialogu používá společnou třídu dialogů WPF s prostředky hlavního okna, konzistentním písmem a umístěním u vlastníka.
- Velikost dialogu je omezena na pracovní prostor Windows; dlouhý obsah má posuvníky. Nastavení a stránky profilu lze posouvat i při nízké výšce.
- Filtr akcí se zobrazí pouze na řídicím panelu. Výběr profilu a vyhledávání mají explicitní názvy pro asistenční technologie. Popisky polí jsou kontrastnější.
- Dialogový test kouře obsahuje přehled nástrojů a rozšiřující dialogy v 560 × 440 a normální velikosti; Stávající záznamové a jazykové testy byly přizpůsobeny struktuře posouvání.

Omezení: Kompaktní okna nenahrazují skutečné testy Windows DPI s procenty 150/200. Nové německé texty ještě nebyly plně přeloženy do všech jazyků 27. Žádné nové ověřování hardwaru a žádné prohlášení o měřené použitelnosti.

Testovací stolice: Uvolněte sestavení bez varování/chyb; Prošel úplným kouřovým testem WPF, včetně kompaktních nástrojů a rozšiřujících dialogů a kontrol přepínání jazyků. git diff --kontrola prošla.

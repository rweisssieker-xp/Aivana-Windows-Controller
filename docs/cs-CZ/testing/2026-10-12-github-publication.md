<!-- generated-doc locale=cs-CZ source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Strojově podporovaný překlad. Struktura, příklady kódu a odkazy jsou kontrolovány; nejde o kontrolu rodilým mluvčím ani o právní schválení. Historické zprávy zachovávají původní data a rozsah. [Původní zdroj](../../testing/2026-10-12-github-publication.md)

# GitHub vydání stavu vývoje MAKRIVO

Stav: 12. října 2026. Vydání zdrojového kódu, nikoli podepsaná maloobchodní verze.

- Sestavení vydání řešení: úspěšné, bez varování a chyb.
- Základní zkoušky: úspěšně.
- WPF kouřový test: úspěšný; čerstvá zpráva o výsledcích v místní složce sestavení.
- První úplná sada Windows: projde okno v popředí, zvuk, obrazovka a kontrola iCUE; poté vyprší časový limit odezvy pracovníka UI Automation.
- Cílené opakování `--extensions-only`: úspěšné včetně automatizace uživatelského rozhraní, nejednoznačnosti, deaktivovaných cílů, vyloučení hesla, procesu zásuvného modulu a skutečného mobilního hostitele HTTPS. Původní časový limit nebyl zakryt změnou časového limitu; příčina nebyla dosud přesvědčivě prokázána.
- Následné úplné opětovné spuštění Windows: úspěšné včetně okna v popředí a všech kontrol rozšíření. Úspěšné opakování nevylučuje sporadické timeouty.
- Před zveřejněním nebyly v auditovaném zdrojovém inventáři nalezeny žádné typické soukromé klíče ani produktivní klíče Stripe/GitHub/AWS. Tato omezená kontrola vzorku není úplnou tajnou kontrolou.
- Artefakty sestavení a data místní orchestrace nejsou publikovány.

Rozsah funkce a zbývající snížení: [Featurní seznam](../FEATURES.md)A to Předchozí zprávy zkoušek si zachovávají svou časovou platnost; nejsou zárukou pro jakýkoliv hardware nebo desktopový kontext.

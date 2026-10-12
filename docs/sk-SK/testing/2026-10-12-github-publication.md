<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-12-github-publication.md)

# GitHub uvoľnenie stavu vývoja MAKRIVO

Stav: 12. október 2026. Vydanie zdrojového kódu, nie podpísaná maloobchodná verzia.

- Zostavenie vydania riešenia: úspešné, bez varovaní a chýb.
- Základné skúšky: úspešné.
- WPF dymový test: úspešný; čerstvá správa o výsledkoch v lokálnom priečinku zostavy.
- Prvá úplná sada Windows: prejde oknom v popredí, zvukom, obrazovkou a iCUE; potom vyprší časový limit odpovede pracovníka UI Automation.
- Cielené opakovanie `--extensions-only`: úspešné vrátane automatizácie používateľského rozhrania, nejednoznačnosti, deaktivovaných cieľov, vylúčenia hesla, procesu doplnku a skutočného mobilného hostiteľa HTTPS. Pôvodný časový limit nebol zakrytý zmenou časového limitu; príčina nebola doteraz definitívne preukázaná.
- Následné úplné opätovné spustenie Windows: úspešné vrátane okna v popredí a všetkých kontrol rozšírení. Úspešná repríza nevylučuje sporadické timeouty.
- Pred zverejnením sa v auditovanom zdrojovom inventári nenašli žiadne typické súkromné kľúče ani produktívne kľúče Stripe/GitHub/AWS. Táto obmedzená kontrola vzorky nie je úplnou tajnou kontrolou.
- Artefakty zostavy a údaje o miestnej orchestrácii nie sú zverejnené.

Rozsah funkcie a zostávajúce zľavy: [Featural zoznam](../FEATURES.md)A to Predchádzajúce skúšobné správy zachovávajú svoju časovú platnosť; nie sú zárukou pre každý hardvér alebo pracovný kontext.

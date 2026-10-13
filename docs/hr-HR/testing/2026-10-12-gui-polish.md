<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-12-gui-polish.md)

# Sljedeći članakSljedeći Članak 12.10.2026

- Ulazak u navigaciju: tri skupine s objašnjenim akcijama za uspostavljanje, stvaranje i ispitivanje.
- Četrnaest postojećih dijalognih područja koristi zajedničku razred dijaloga WPF s resursima glavnog prozora, jedinstvenog pisanja i pozicioniranja kod vlasnika.
- Veličina dijaloga ograničena je na radno područje Windows; dug sadržaj dobiva vrstice. Nastavitve i stranice profila ostaju slomljive čak i na niskoj visini.
- Filter djelovanja se pojavljuje samo na Dashboardu. Izbor profila i pretraživanje imaju jasna imena za pomoćne tehnologije. Opis područja je kontrastniji.
- Test za dijalog uključuje pregled alata i dijaloge proširenja u 560 × 440 kao i normalne veličine; postojeći testovi za snimanje i ispitivanje jezika prilagođeni su strukturi skraćenja.

Ograničenja: kompaktni prozori ne zamjenjuju stvarne testove Windows-DPI pri 150/200 posto. Novi njemački tekstovi još nisu potpuno prevedeni na svih 27 jezika. Nema nove provjere hardvera ni tvrdnje o izmjerenoj uporabljivosti.

Ispitivanje: Izgradnja izdanja bez upozorenja / pogrešaka; provedeno ispitivanje WPF smoke, uključujući kompaktne alate i dijaloge proširenja, kao i ispitivanje promjena jezika. Git diff - provjerite.

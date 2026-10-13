<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-12-gui-polish.md)

# GUI obdelava iz 12.10.2026

- Neposredni vstop v navigacijo; tri skupine z razkrito akcijo za vzpostavitev, ustvarjanje in preizkušanje.
- Štirinajst obstoječih dialognih območij uporablja skupno razred dialogov WPF z viri glavnega okna, enotnega pisanja in pozicioniranja pri lastniku.
- Velikost dialoga je omejena na delovno območje Windows; dolge vsebine dobijo vrstice. Nastavitve in profile ostanejo razširljive tudi pri nizki višini.
- Akcijski filter se prikaže samo na plošči. Izbira profila in iskanje imajo jasne imena za pomožne tehnologije. Opisi so bolj kontrastni.
- Diagramski test vključuje pregled orodij in razširitve v 560 × 440 ter normalno velikost; obstoječi testi in jezikov so bili prilagojeni strukturo prehodov.

Omejitve: kompaktna okna ne nadomestijo pravih preizkusov Windows-DPI s 150/200 odstotki. Nova nemška besedila še niso v celoti prevedena v vseh 27 jezikov. Brez novega preverjanja strojne opreme ali trditve o izmerjeni uporabnosti.

Preizkušeni položaj: Izdelava brez opozoril/grešav; preizkušena polna WPF smoke, vključno z kompaktnimi orodji in razširjenimi dialogi ter preizkušnjami za spreminjanje jezika. Git diff - preverjanje je bilo.

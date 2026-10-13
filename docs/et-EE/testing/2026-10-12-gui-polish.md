<!-- generated-doc locale=et-EE source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../testing/2026-10-12-gui-polish.md)

# GUI töötlemine alates 12.10.2026

- Otsene sissejuhatus „vahendid“ navigatsioonis; kolm rühma, millel on selgitatud tegevused, et luua ja kontrollida.
- Nelja olemasoleva dialoogi valdkonnas kasutatakse ühist WPF dialoogi klassi peamise akna ressurssidega, ühtseks kirjutamiseks ja omanikuga.
- Dialoogi suurus on piiratud Windows tööpiirkonnaga; pikad sisu saavad ringi. Seadmed ja profiilid jäävad ka madalate kõrguste korral keeratavaks.
- Aktsiafilter ilmub ainult dashboardsil. Profiili valikul ja otsingul on selged abitehnoloogiate nimed. Põhikirjeldused on kontrastlikumad.
- Dialogu-Smoke test hõlmab tööriistade ülevaadet ja laiendamisdialooge 560 × 440 ning normaalset suurust; olemasolevad salvestus- ja keeleanalüüsid on kohandatud salvestusstruktuuriga.

Piirangud: kompaktsed aknad ei asenda tegelikke Windows DPI-teste 150/200 protsendiga. Uued saksakeelsed tekstid ei ole veel täielikult tõlgitud kõigisse 27 keelde. Uut riistvaravalideerimist ega mõõdetud kasutatavuse väidet ei ole.

Testi: vabastamine ilma hoiatuste/veadeta; täielik WPF müra testi, sealhulgas kompaktne tööriistade ja laienemise dialoogid ning keelevahetus. GIT DIF - kontrollida.

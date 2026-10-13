<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-12-gui-polish.md)

# GUI perdirbimas iš 12.10.2026

- Tiesioginis įvadas į navigaciją; trys grupės su deklaruotais veiksmais, kuriomis siekiama nustatyti, kurti ir patikrinti.
- Keturiolika egzistuojančių dialogų sričių naudoja bendrą WPF dialogų klasę su pagrindinio lango ištekliais, vieninga rašta ir savininko pozicija.
- Dialogas apsiribojamas Windows darbo sritimi; ilgas turinys gaunamas perversmų juostomis. Nustatymai ir profilių puslapius galima perkelti net mažomis aukštis.
- Veiksmų filtras pasirodys tik ant „Dashboard“. Profilų parinkimas ir paieškos turi aiškius pagalbinės technologijos pavadinimus. Žemės aprašymai yra daugiau kontrastų.
- Dialogas-Smoke testas apima įrankių peržiūrą ir išplėtimo dialogus 560 × 440 ir įprastą dydį; esami įrašymo ir kalbos testai buvo pritaikyti prie spustelėjimo struktūros.

Ribos: kompaktiški langai nepakeičia tikrų Windows DPI bandymų su 150/200 procentų masteliu. Nauji vokiški tekstai dar nevisiškai išversti į visas 27 kalbas. Nėra naujo aparatūros patvirtinimo ar teiginio apie išmatuotą naudojimo patogumą.

Išbandymas: išleidimas be įspėjimų / klaidų; išsamios WPF smoke testas, įskaitant kompaktišką įrankių ir plėtros dialogą, taip pat kalbos keitimo testus. git diff - patikrinti.

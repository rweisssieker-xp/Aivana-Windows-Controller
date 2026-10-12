<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-12-responsive-layout.md)

# Atsakingas WPF paviršius

Pagrindiniai puslapiai yra nukreipti pagal naudingo turinio plotį, o ne fiksuotą lango plotį. Pagal 900 DIP turinio plokštę, diskų plokštė ir redaktorius yra tarpusavyje. Pagal 620 DIPs yra dvipusiai. Pagal 820 DIP langų plokštę, šoninis juostas yra įtrauktas. Minimalus dydis: 640 × 480 DIPs.

Profiliai ir nustatymai, taip pat klaviatūros valdymo elementai keičiasi pagal realią grandinę. Šiuo atveju esami kontroliniai elementai perkelti, o ne juos atnaujinti. Akcijų kortelės dalijasi turimais eilutėmis. Įrankių juosta nutrūksta. Klaviatūros pusė yra įmanoma pasiekti vertikaliai, fizinis klaviatūra išlaiko savo proporcijas.

Automatinis WPF-Testas: visi penki pagrindiniai puslapiai 640 × 480, 900 × 620 ir 1440 × 900; Ekranų nuotraukos kiekvienoje pusėje; juostų keitimas abiem kryptimis, peržiūrint gautą tekstą. WPF-Vienetai – tikra Windows-skalavimas 150/200 Procentas ir keli monitoriai dar nėra patvirtinti..

Bandymo būdas: išleidimas be klaidų / įspėjimų. Visiškas WPF smoke testas su visais dydžiais, spindulių keitimu, klaviatūros išdėstymu ir kalbos testais. git diff - patikrinti.

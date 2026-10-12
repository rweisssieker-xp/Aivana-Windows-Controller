<!-- generated-doc locale=et-EE source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../testing/2026-10-12-responsive-layout.md)

# Reageeriv WPF pindala

Peamised leheküljed on suunatud kasuliku sisu laiusele, mitte kindlaksmääratud akna laiusele. 900 DIPi sisu laius seisneb Dashboard ja Editor üksteisega. Alates 620 DIPs muutuvad tunnused kahekohaliseks. 820 DIPi akna laius sisaldab külgpalli. Minimaalne suurus: 640 × 480 DIPs.

Profiili ja seadete piirkonnad ning klaviatuuri juhtimiselemendid vahetavad oma kolmest vastavalt tegelikule võrgustikule. Seetõttu ümber paigutatakse olemasolevad kontrollimaterjalid selle asemel, et neid uuesti paigutada. Aktsia kaardid jagavad kättesaadavaid rida. Tööriistad katkestatakse. Tastaturu külg on vertikaalselt ligipääsetav, füüsiline klaviatuur säilitab oma proportsioonid.

Automaatne WPF-Test: kõik viies peamine lehekülg 640 × 480, 900 × 620 ja 1440 × 900; Ekraani pildid igal lehel; kolumni vahetamine mõlemas suunas, kontrollides saadud teksti. WPF-Üksused - tõeline Windows-Skalifitseerimine 150/200 Protsenti ja mitu monitorit ei ole veel valideeritud.

Testi: Release-Build ilma veadeta / hoiatused. Täielik WPF Smooke test kõigi suuruste, kolumni muutustega, klaviatuuri paigaldustega ja keele testidega. GIT DIF - kontrollida.

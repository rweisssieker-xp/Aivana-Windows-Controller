<!-- generated-doc locale=et-EE source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../testing/2026-10-09-usp-release.md)

# Keyboard abi, ülejäänud ja rakendamise tagasiside

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Rakendati olemasolevas Windows rakenduses kolm prioriteetset parandust:

- Spetsiaalne võti: reaalne iCUE toetatud seade / võti, ajutine G-tähelepanu, konfigureerimise edukuse kontrollimine, konflikti valideerimine ja salvestamata tegevuskava. Capture rollback, queued callback puudumine ja normaalne siduv taastamine katab vead ja dialoog sulgemine.
- Keyboard overlay: füüsiline võrdlus geomeetria, aktiivne profiili sidemed, live profiil / redigeerimine ja tavaline Windows võtme rõhuasetus. Hoidke Ctrl+Alt+F1 või võtke läbi palli/lõpetaja lehte. Mitteaktiivne, kliki läbi ja puudub tööriist.
- Tegevuse tagasiside: täidetud ootamine / jooksmine / samm / lõpetatud / ebaõnnestunud / tagasi lükatud etappid, samm / korduv edenemine ja mitteaktiivne toast. Tagasiside välistab tegevuse sisu ja erandite sõnumid. Viimane tühistamine ei saa aru saada. Täiendamine kirjeldab täitmist, mitte iseseisvat OBS riigiküsimust.

Kontrolli kohta:

- 24 Põhiline kontrolli läbi, sealhulgas faaside tellimuse, ebaõnnestumise / tühistamise tulemused, lõpliku sammuga tühistamine, kuulajate erand ja privaatsete tegevusväärtuste välistamine.
- Vabanemine lahendus ehitatud ilma hoiatused / vead.
- Windows kontrolli all; ühendatud K100 ⁇ G1–G6 ja kodune konfigureerimine / vabastamine õnnestus. heli lõpppunktid kontrolliti ilma nende seisundi muutmata.
- WPF suits läbi kõik viidatud ülejäänud paigaldused, võlur / kataloog / konflikti / draft püsivus, elav päritoluliikide konfigureerimine, callback edastamine, staal callback tagasilükkamine ja pausi-binding taastamine. Native callback sissepääs on simuleeritud; füüsiline inimese G-tõuend jääb käsitsi kontrolliks.
- Praegune eelvaate invarianss kontrolliti rakenduse toast. Ülevalmistamise interaktiivsed omadused ja näitus / varjund kontrolliti; ekraani pildid tehti võlurile, ülevalmistamisele ja toastile.
- 27 keeled sisaldavad kõiki 819 kanoonilisi joone; offline valideerimise / võtme.

Snapshots ja täielik GUI tulemus on artifaaktides/usp-win-x64 / suits. Testid kasutavad ajutisi profiile; tootmisseadmed on allutatud ja käivitamise ajal kontrollitud.

<!-- generated-doc locale=et-EE source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Kontrolli laiendamise kava

**Eesmärk:** Rakendada nõutud monitor, gest, töövool, salvestamine, seade diagnoos, portable profiili vahetamine ja tray parandused.

**Arhitektuur: ** Laiendage olemasolevaid Core/Windows/Desktop kihti. Puhtad seadmed ja portable konfiguratsioon loogika elavad Core'is, emakeelsed integratsioonid Windows ja modaalne disain redaktorid Desktop'is. Säilitage olemasolevad pruunid tööruumid ja paigaldatud kasutaja konfiguratsioonid.

**Spec: ** ../specs/2026-10-12-kontroller-usps-design.md

- [x] Seire teenus, sihttaseme püsivus ja sünkrooniline elatise; testimine kättesaamatud ID-d ja tühistamine.
- [x] Gesture seisundit masin ja hoitud kihtid; testid, korrata, uuesti seadistada ja olemasolevaid koheseid sidemeid.
- [x] Töövoogude tingimused, edasised osad ja vea poliitika; katsetamine ja tühistamine.
- [x] Selge lühiajaline salvestamine koos projekti läbivaatamisega; testfiltreerimine ja hook disposal.
- [x] jagatud faili profiili vahetamine läbivaatuste ja eelvaatustega; katse konfliktid ja mobiilsed kaardid.
- [x] Üldised seadmed on standardsed spetsiaalsed võtmed ja diagnoosimine; katse, duplikatsioon ja pausi käitumine.
- [x] Tray/window/executable ikoon ressursse ja pausi; kontrollida ressursse avaldatud executable.
- [x] Integreerige kõik palett/editor sisendpunktid ja lokaliseeritud tekst; käivitage Core, Windows ja WPF kontrollid.
- [x] Kirjeldage eraldi treeningkataloog, säilitage kasutaja seade ja käivitage uuesti värskendatud rakendus.

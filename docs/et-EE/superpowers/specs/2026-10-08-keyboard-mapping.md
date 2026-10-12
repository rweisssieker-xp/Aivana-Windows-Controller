<!-- generated-doc locale=et-EE source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Füüsiline nägemine Mapping

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Eesmärk

Tastaturu nägemine näitab tegevuste määramisel tegelikke klahvi positsioone. Sama laia rida ei piisa: Enter, Shift, Space, Navigation, numbrite bloki ja tootja täiendavad juhtimiselemendid erinevad.

## Rakendamine

- Kataloog eraldatud füüsiliste ID-dega, klaviatuuri geomeetria, trükitud legendid ja Windows sisekoodid.
- ISO-DE ja ANSI-US on täisformaadid ja TKL; lisaks Corsair K100 RGB, mis on tunnustatud iCUE ühendatud mudelina.
- ISO Enter koos L-vormiga, numbriblokiga, millel on kõrged Plus/Enter nupud ja laiem Null; navigatsioon ja pilved oma positsioonidesse.
- Valige mudel / paigutus. USB VID/PID ja mudeli nimi ei näita keelevarianti. Üldised profiilid ja tootja dokumendid on märgatavalt erinevad; millimeter või firmware sobivuse tagamine.
- Skannikoodi kuvamine aktiivsele Windows sisekujundusele, nii et füüsiline klõpsamine ei paneks juhuslikult teise nuppu, näiteks Y/Z vahetamisel USA ja DE vahel.
- Määrus avab projekti olemasolevas tegevuse redaktoris. Säilitamine, konfliktide kontrollimine ja tagasilükkamine jäävad olemasolevatele mehhanismidele. Kliendid ei alusta tegevust.
- Omandatud G-tunnid, profiili-/loki-tunnused ja iCUE raadi esitatakse, kuid ilma rakendatud sisendadapterita ei ole tavaline Windows hotkeys. Konfigureeritavad tootja lühikesed lühikeseid jäävad võimalikuks.
- Kogumised jäävad ülemaailmseks Windows hotkeys; seadmevaade ei tähenda seadme konkreetset survet ega üksiku füüsilise klaviatuuri eksklusiivset paigutamist.

## Tõendid

[K100-Handbuch koos juhtimiselementidega](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO deklaratsioon](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## kontrolli

Geomeetria, variandi ja sisekoodi testid; ehitused ja olemasolevad tuumad/Windows testid; WPF näitus ja kohandamine ilma tegelikku kasutaja konfigureerimist läbi viidud või muutmata. Tootjauuring dokumenteerib täiendavaid mudeli/SKU nõudeid, olenemata praegusest paigaldatud kataloogist.

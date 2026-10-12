<!-- generated-doc locale=et-EE source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Kasutuskõlblikkuse ja suutlikkuse parandamise kava

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Kasutaja nõue: parandab välimust, kasulikkust ja funktsionaalsust; eelnevalt nõutakse vajalikke väiksemaid mudeleid ja autonoomset täiendamist.

## ulatuses
Säilitage olemasolevad profiilid ja töötada Corsair SDK integreerimine. Parandage olemasolevat WPF rakendust, mitte looma eraldi prototüüpi.

- [x] Shell/dashboard töötaja: reageeriv paigutus, aktiivne navigatsioon, loetavad kontrollid, kompaktsed tegevused/staatus, tõmmata ja maha lülitamine, kasulikud tühjad seisundid ja templid. Oma MainWindow.xaml, MainWindov.xamli ja MainWind.Dashboard.cs.
- [x] Editor töötaja: MainWindow.Editor.cs ja vabatahtlik editor-ainult osalised failid. Selgitavad etapid, Save/Discard, valideerimine inline, pühendatud makro nimekiri drag-and-drop, kopeeritud sammud, usaldusväärne hotkey capture ja faili/URL väljad.
- [x] Windows töötajad: päritolu valideerimine, kindlad seadme nimed ja SDK võimeid, piiratud modifitseerijate ootused ja ohutu süstimine; oma Windows ja Windows testid.
- [x] Vanem: põhikäitumise progressi, kontrollivahendid, konfigureerimise säilitamine, integreerimise testimine, tegelikud renderiseeritud ekraani pildid, avaldamine / pakendamine ja rakenduse asendamine.

## Interfaadi leping
Kontroller pakub olemasolevaid APIs pluss rida LastStatus, bool HasRunningActions, bool IsRunning(Guid), rida GetTileStatus(Guide), rida ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeeboards, Task TestCorsairColorAsync(string), void OpenIcue(). Editor näitab bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); olemasolevad väljad/meetodi nimed jäävad kättesaadavaks osaklassides.

## Vastuvõetavus
Pole reaalse kasutaja konfigureerimist; kehtivad etappide muudatused salvestatakse üks kord, ebaõiged disainid jäävad redigeeritavaks, kustutatakse salvestatud andmed. Native hotkey aliasid valideeritud ilma süstimata testi sissepääsud seotud aknad. Tile ja macro reorder testitud; kõik vaated on töölaua suuruses; käimasolev / ebaõnnestunud / täielik seisund nähtav; reaalne SDK-seadme mudel näidatud. Ehita / testida ja avaldada suitsu pass, ja uuendatud rakendus on käivitatud.

<!-- generated-doc locale=et-EE source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Projekti

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Jätkake: 8. oktoober 2026. Status: enne rakendamist uurimiseks.

## Eesmärk ja kokkulepitud ulatus

Iseseisev Windows rakendus muudab tavalise arvutit vabalt konfigureeritavaks tegevuspaneeliks, mis sarnaneb voolu katusega. Kasutaja on kinnitanud kõik ettepanekud meetmed, makro, seadme tuvastamine, profiilid ja hea GUI. Ühendatud Corsair ja teised klaviatuurid tuvastatakse; täiendavad tootja funktsioonid pakutakse ainult siis, kui neid tegelikult toetatakse.

Eeldused: saksa pindala, kohalik kasutamine ilma kontoteta, Windows 11 x64 esimese sihtkoha, ei juhtimise install. Raamat on normaalseks kirjutamiseks kasulik. Ülemaailmsed kliendid kasutavad standardseid klaviatuuri kombinatsioone modifikaatoritega. Üksikud tähestikud ei ole standardiliselt globaalselt reserveeritud.

## Tehniline otsus

Native C#/NET-10-App koos WPF ja MVVM. Stabiilne .NET-10-SDK on saadaval tööruumis. WPF võimaldab individuaalselt kujundatud töölaua ja otsese integreerimise Windows. Alternatiiviks oleks eraldi Windows teenusega veebipõhine vahend või platvormidevaheline töölaua rakendus; mõlemad toovad selle Windows projekti jaoks täiendavat tööaega ja kommunikatsiooni keerukust. Seetõttu on kodumaine variant valitud alus.

Jaotus: Raamatuprojekt nägemuste ja traade jaoks, põhiprojekt konfigureerimise ja rakendamise loogika jaoks, Windows projekt hotkeys, sissemulatsioon ja seadmed, platvormi sõltumatute reeglite katseprojekt. Tootja adapterid on fikseeritud võimsusliides ja on standardtoimetest sõltumatud.

## Ümberpind

Tume disain, millel on rahulikud antraasiitvärvid, hästi loetav kirjandus ja turki värvi akne. Linkid Navigation Dashboard, profiilid, seadmed ja seadeid. Üle aktiivne profiil, otsing ja lülitamine kõik hotkeys. Ülevaade jääb 100–200 protsendil Windows skaleeritavaks.

Dashboard sisaldab kohandatavat kasti. Igal kaabelil on sümbol, nimi, hotkey ja täitmise seisund. Klõpsamine toimub samal viisil nagu Hotkey. Paremaks avaneb toimingu tüüp, parameetrid, hotkey salvestamine, sümbol/värv, aktiveerimine ja testi nuppu. Uued, duplikatsioonid, kustutamine ja ümberkorraldamine on saadaval. Tastaturu nägemine näitab saksa standardmääruse skeemiliselt; see ei väita tuvastatud seadme täpset geomeetriat.

Profiile saab luua, kopeerida, nimetada ja manuaalselt aktiveerida. Valikuliselt määratakse profiilile protsessi nimi. Asjakohane põhiprogramm aktiveerib selle profiili, muidu kehtib ülemaailmne standardprofiil. Mitmete sobivate profiilide puhul kehtib selgelt nähtav järjekord. Editor selgitab Hotkey konflikti otse asukohas.

## Hotkeys ja sissejuhatus

Windows RegisterHotKey töötleb tavapäraseid globaalseid kombinatsioone surutud klaviatuuri taastamisega. Registreeritakse ainult aktiivne profiil. Reserveeritud või juba registreeritud kombinatsioonid ilmnevad veadena; rakendus ei teata neid edukalt aktiveeritud. Kirjeldus redaktoris ei lahenda tegevusi.

Enne simuleeritud sissemakseid vabastatakse muutmispinge või oodatakse nende füüsilist vabastamist. Unicode teksti ja klaviatuuri kombinatsioonid saadetakse SendInputiga. Iga vajutatud nuppu vabastatakse ka katkestamise korral. Toimingud toimuvad väljaspool UI-töö; rakendamine ja profiili muutmine ei tohi peatada pindala.

## Tegevused ja makro

Tegevuste tüübid: programm algab argumentidega ja tööleht, avab faili/faili üle Windows, avab HTTP/HTTPS veebilehte, sisestab Unicode teksti, saadab klaviatuuri kombinatsiooni, meedia Play/Pause/Next/Premium/Stop, hääl kõrgem / madalam / müra ja profiili muutmine. Parameetrid kinnitatakse enne salvestamist ja uuesti enne rakendamist. Programmid käivitatakse otse; argumente ei tõlgendata juhendina.

Makroosad sisaldavad ülalnimetatud tegevuste ja selgete sammude nimekirja. Järgmised sammud saab lisada, töödelda ja edasi lükata. Korduvused on lõpuks konfigureeritavad (1–100); lõputud ei kuulu esimesele versioonile. Käimasolevat makro saab lõpetada üle pindala või oma katkestuse. Korduv väljalülitamine sama käimasoleva asukoha ei alusta paralleelselt. Erinevad sissemulatsiooniga makroid seriseeritakse nii, et nende klaviatuurid ei segaks. Juhul, kui samm on viga, peatub makro ja näitab mõjutatud sammu.

## Seadmed ja tootjad

Seadmete salvestus Windows Raw Input ja lisateave seadmete kohta: näidisnimi, seadme tee, tootja/toote tuvastamine, kui see on kättesaadav, ühenduse seisund ja tunnustatud oskused. Üle- ja uuendusteenused ajakohastavad loetelu ilma uuesti käivitamata. Füüsiline seade võib sisaldada mitut HID-i postitust; need on rühmitatud koormatavate identifitseerimiste järgi, muidu nähtavad eraldi postitusena.

Normaalsed hotkeys kehtivad klaviatuuri vaheliselt. Seadmevaade pakub teavet ja oskusi; eraldi klaviatuur, millel on eksklusiivsed nupud, ei ole osa sellest kokkulepitud režiimis.

Corsair tunnustus töötab ka ilma iCUE. Valikuline iCUE SDK adapter pakub ühenduse staatust ja toetatud valgustusmeetmeid, kui SDK, iCUE ja ühilduv seade on olemas. UI erineb tuvastatud, SDK saadaval, ühendatud ja tegelikult juhitav. SDK dokumenteerib eelkõige LED-i juhtimist; omandatud G-tunnid toetavad ainult tavapäraseid hotkeys, kui Windows saab vastavaid sisendeid. Ei ole paindlikke lubadusi kõikide spetsiaalsete rõhkude jaoks.

Teised tootjad saavad kõigepealt üldise tuvastamise ja standardtoiminguid. Adapteri liides võimaldab hilisemat integreerimist; toetamata oskused on ausalt näidatud. Puuduvate mudelite jaoks ei ole tarkvara testid olemas.

## Säilitamine ja igapäevane elu

JSON-i konfigureerimine kohaliku rakenduse andmekogumi all. Aatomiline salvestamine viimasel kehtival salvestamisel; valefailid ei ole vaikselt üle kirjutatud. Importimine kontrollib formaati, versiooni, ID-d ja parameetreid ning näitab eelvaate enne üleandmist. Impordi toimingud viiakse läbi alles pärast selget aktiveerimist. Eksport sisaldab profiile ja salvestusi.

Tray menüü: avada, profiili valik, katkestada hotkeys, katkesta makro, lõpetada. Aknad sulguvad standardse ja selgitab seda esimest korda. Autostart on valikuline ja standardne. Ainult üks institutsioon haldab Hotkeys. Lõpetamine registreerib hotkeys, katkestab tulemused ja vabastab ressursse.

Kohaliku tegevuse nägemus, millel on piiratud edusammud, vead ja katkestused. Mitte ühtegi protokolli kõigist klaviatuuritest. Tekstitegevuse sisu ei kirjuta vigu. Ei pilveteenust ega telemetria.

## Viga ja piirid

Puuduvad programmid, kehtivad URL-id, kahjustatud profiilid ja kättesaamatud tootja SDK saavad konkreetseid saksa teateid. Adapteri viga ei mõjuta standardtoimeid. Kõrgemate eelistatavate rakenduste sisestamine võib ebaõnnestuda Windows õiguse piirides; vigu selgitatakse, et rakendus ei nõua juhtimisõigusi.

## Uurimine ja vähendamine

Automatiseeritud testid profiili prioriteediks, konfliktide tuvastamiseks, konfigureerimiseks, aatomiliseks salvestamiseks, makro järjekorras, korratsiooniks, katkestamiseks ja viga edastamiseks. Windows Smoke testid reaalsete hotkeys, teksti ja klaviatuuri kombinatsioonid, meediakkumised, seadme salvestamine, traadimine ja puhas lõppemine. Mitte testid, mis juhuslikult käivitada mis tahes programmid või veebilehed.

GUI testimine: paigaldamine ja redigeerimine, rakendamata salvestamine, samaväärne klõpsamine/hotkey, konflikti teade, profiili muutmine, seadmete nägemine, importimine/ekspordimine ja skaalimine. Tootja spetsiifiline seadme kontroll dokumenteeritakse eraldi olemasoleva seadmega.

Tarne: allikas, reprodutseeritav ehitus- ja algjuhend, saksa juhend ja kohalik Windows-x64-Publish-Artefakt. Edu tähendab rakendust, mis on kättesaadav kõigiga üldiste funktsioonidega; Corsair oskused on märgistatud ainult tõestatud SDK/seadme toetusega.

## Tehnilised allikad

- Microsoft Keyboard sissejuhatus / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF sissejuhatus: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Ametlik Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Järgmine samm

Pärast selle projekti läbivaatamist: konkreetse rakenduskava koostamine ja rakenduse valimine. Tühjas töökorras ei ole veel Git-repositooriumi; seetõttu ei ole see dokument veel committed.

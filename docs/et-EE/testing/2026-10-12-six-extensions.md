<!-- generated-doc locale=et-EE source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../testing/2026-10-12-six-extensions.md)

# Kuus laiendust: rakendamine ja katsetamine

See on 12 Septembrist 2026 ja tööstusharu `codex/six-extensions` Töökoht, mitte tootmisvabastus.

## Teenindus

Komanda palett avab sissemakseid **Töökoha loomine**, **Tastatuuri tootjad ja SDKs**, **Mobiilpaneel**, **Laiendused**, **UI automatiseerimine**, **UI lõppu** ja **Rakenduskontrolli**.

- Töökoha assistent loob töökoha, kohtumiste, voogude, teadusuuringute ja projekti profiilid. Registreerimise/käivitamise menüü või faili valik, URL-id, väljavõte / ekraan ja OBS stseen on näidatud enne salvestamist sammu. Instituut ei teosta tegevust.
- Järgmises etapis avab **tulemus** eelkontrolli ja jälgimise. Läbi ja mobiilirakendused eristavad tegelikku kinnitust kontrollimatu toime. Suhtelised muudatused, lühend ja teksti sisestamine ei anna paindlikku tõhusust. Protsessi tõend ei näita rakendamise spetsialisti valmisolekut.
- UI-automaatika kasutab eraldi, ajaliselt piiratud töötaja. Valik hiire positsiooni, valiku kontrolli, Invoke/Select/Toggle/Focus/SetValue/WaitState kaudu; mitmed rünnakud, deaktiveeritud sihtmärgid ja paroolid on keeldutud. Raamat sisaldab toetatud klõpsamisi ja pausi; tekstide sissemakseid ei lõigata. Enne vastuvõtmist kontrollitakse vastuvõtu.
- Laiendused: ZIP testimine, hashing, skeem, deaktiveeritud impordi, väljendatud usaldus enne aktiveerimist, isoleeritud protsess piiratud IPC, eksportimine, deaktiivsus, deinstall, eelmise versiooni taastamine / tagasilükkamine. Protsessisolatsioon ei ole liivakasti: vabastatud pluginad töötavad kasutajate õigustega. Näidepakett `makrivo.example.zip` sisaldab arvutamist ja staatustehinguid. Toetatavad skeemid moodustavad allikakoodi dokumenteeritud lihtsat JSON-i skeemi osa; mitte avalik turg.
- Mobiilpaneel: valige kohalikud IP-d ja jagatud profiilid, avage HTTPS-aadress, kontrollida sertifikaati näidatud SHA-256-Fingerprints ja teadlikult usaldada, seejärel sisestage ühekordne kuue tähemärgi kood. Privaatsed võtmed on kaitstud Windows-DPAPI; ainult kliendi juurdepääsu hashid salvestatakse. Sertifikaadid muutuvad pärast renoveerimist või IP-vahetust. Kasutaja vabastab Windows tuletõrje vajaduse korral. Mitte automaatne sertifikaadi paigaldamine, portaali laienemine või tuletõrje muutmine. Klientide juurdepääsu saab tühistada. Panel näitab tegevuse seisundit ja katseprotsessi; see ei ole veel universaalne live-sensor.

## Tastatuuri

| Tootjad | Rakendamine | Piirangud |
|---|---|---|
| Corsair | olemasolevad iCUE G-tunnid ja RGB | kohalik iCUE ühendus ja sobiv mudel vajalik |
| Logitech G | G-Key SDK ja eraldi LED-SDK | originaal kohalik x64-DLL selgelt vabastada; G-Key SDK ei anna individuaalset identiteeti mitu klaviatuuri |
| Razer | Chroma-RGB ja standardtunnistus | Eelmine postitus Synapse |
| SteelSeries | GameSenseRGB ja standardtunnistus | eelnevalt omandatud nupud GG |
| Wooting | seadmetega seotud analoogväärtused, rõhu/lõhku puudused | originaal kohalik x64-SDK-DLL; normaalne HID sissepääs jääb aktiivseks |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Muud | Füüsilised standardtunnid seadme ja skaneerimiskodi järgi | Fn ja omandatud juhtimiselemendid |

Uute omandatud SDK-dLLid ei ole tarnitud. Uued tootja adapterid ei ole ilma sobivate füüsiliste seadmeteta veel kinnitatud.

**Ekslusiivne teine makropuud ei ole valmis: ** Raw Input jälgib seadmeid eraldi, kuid nende normaalse Windows sissevoolu ei takista. Tootmise vabastamine nõuab litsentseeritud, allkirjastatud Windows 11/Secure laeva kontrollitud filtreerimist ning kiireloomulise vabastamise, katkestamise, peatamise ja taasühendamise katset. Seal ei ole paigaldatud juhtmeid. Rakendus näitab seda piirit selgelt.

## Testid

- Release Build: edukas, null hoiatused / vead.
- Põhikonsoolid: kõik testide rühmad on edukad, sealhulgas konfliktid tootja nupudega, plugini tagasipöördumine ja tegelikud ekspordifunktsioonid testimisprotsessis.
- Laienduste integreerimine: tõeline UIA töötaja WPF testimise abil; unikaalne invokeerimine, mitmekesisus, deaktiivsed eesmärgid, parooli välistamine. Nimeeritud pipe plugini protsess, protokollipiirangud, analoogne hüsteeritus ja Logitech ABI dekodeerimine edukalt.
- Tõeline HTTPS-hostaja: kinnitatud sertifikaat, ühendamata juurdepääs, võlts / ühekordne kood, päritolu kaitse, brauseri failid, hash salvestamine ja kaitstud võtmed edukalt. Testis avastatud skanneri viga efmeeride abil parandati.
- Windows-Suit, millel on selgelt välistatud eduanalüüs: edukas; Audio/OBS/iCUE/Raw Input ja ekraani staatuse kontrollimine. eelmise käigus ei õnnestunud täielik suit pöörata tähelepanu isoleeritud katse aknale. järgnevas käigus oli ka täielik suits, sealhulgas pööramine ja ühendatud iCUE-Järgmine korduv läbiviimine ei suutnud uuesti keskenduda; kontekstipõhine test ei ole stabiilne. see test ei nõrgenenud. lühiajaline ebaõnnestunud DDC-lugu on piiratud korduv; valgus ei muudetud.
- WPF-Smoke: kõik varasemad ja uued dialoogid, konfigureerimisvariants, redigeerija ja CRUD edukalt; ekraani pildid ehituskorras `smoke`.
- Playwright koos Edge'iga, 390×844 ja 1024×768, Ühenduse vool/UUID/ühe saatmine/ei horisontaalne läbilaske edukas. API-Mock; reaalne HTTPS testi eraldi. `artifacts/mobile-browser`. Browser Plugin ei olnud kättesaadav, seega regulaarne Playwright Fallback.
- Sõltumatu koodi läbivaatamine; hilinenud mobiilseadmed, puuduvad lõppkontrolli tulemused, protsessi läbivaatamise, plugini tagasipöördumine ja RGB südame löök parandatud.

Veel avatud: eksklusiivne juhtimine, füüsiline tootja / telefon / tablett-LAN testimine, Windows skaal 150/200 protsenti ja täielik tõlge kõigi uute dialoogite. Uued dialoogid on peamiselt saksa keel; olemasolevad keelekataloogid jäävad säilitama. Seetõttu ei ole kogu nõue ** täielikult maha jäetud **.

## Pakendi tootmine

`./tools/publish-complete.ps1 -SelfContained` loob uue faili all `artifacts/release-*`, sealhulgas töölaua, UIA töötajad, HTTPS host, brauseri failid, importimine näidisplugin ja käivitamise litsentsid. Installeeritud rakendus ei ole üle kirjutatud. Ilma `-SelfContained` on pakett vajalik .NET-10-Desktop-/ASP.NET-Runtimes.

Pakett on kohalik kontroll, mitte allkirjastatud müügisüsteem. Oma kasutustingimused ja SDK-i müügiõigused on eraldi vabastused; vaadake olemasolevat dokumente allpool `docs/licensing`.

<!-- generated-doc locale=et-EE source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Kontrollikoja rakenduskava

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Agentsiaalsete töötajate puhul: kasutada paralleelset isoleeritud omandit; kasutaja palus selgelt täielikku rakendamist maksimaalselt kasulike allagentidega.

**Eesmärk: ** Saada kodune Windows pliiats heakskiidetud disain.
**Arhitektuur:** Põhikoonitus ja tühistatav rakendamine; Windows Adapterid; WPF Desktop koosseis.
**Tech Stack: ** .NET 10, WPF ja Win32 JSON. Ei maksa sõltuvust.
**Spec: ** ../specs/2026-10-08- windows-controller-design.md

## Ülemaailmsed piirangud
Saksa kasutajaliides; kohalik ladustamine; Windows x64; tõeline seadmete võimsus; ei ole klaviatuuri registreerimist; ei ole eeldatavat automaatse käivitamist.

## Ülevaade Focus
Kahjulik import ei tohi asendada salvestatud seadmeid; kinnipidavad modifitseerijad ei tohi saastuda; plugged hardware ei tohi kukkuda; sulgemine peab säilitama trai kättesaadavuse; tühistatud makroid tuleb vabastada ressursse.

## Tehted ja omandamine
- [x] Põhjagendid: src/Aivana.Kore ja testid/Aivana.Testid. Mudelid, JSON püsivus, valideerimine, profiili valik ja MacroRunner. Tehke ebaõnnestunud käitumistestid, seejärel rakendada ja teha kõik põhikatsed.
- [x] Windows agent: src/Aivana Windows RegisterHotKey, seadme inventuur, SendInput, eelprotsess ja valikulised Corsair valgustusadapterid. Kontrollige kodumaise struktuuri suurust ja registreerimise puhastamist ohutute suitsu testidega.
- [x] GUI agent: src/Aivana.Desktop/MainWindow.xaml ja .cs pluss UI assistendid ainult. Dark dashboard, täielik redaktor, profiilid, seadmed, klaviatuuri ülevaade ja makro redaktor. Üks kord, kui lepingud on olemas.
- [x] Vanem: jagatud lepingud, projektifailid, DesktopController koostis, rakenduse/tray elutsükkel, impordi/ekspordi dialoogid, dokumendid, integreerimise kontrollid, avaldamine ja rakenduse käivitamine suitsutesti.

## jagatud vahendid
Lepingud on elus Core/Models.cs. Kontrolli rakendamine elab Desktop/DesktopController.cs. UI kutsub kontrolli meetodeid selle konkreetse klassi kaudu. Agendid saavad oma ülesannete täpseid allkirju. Igaühel on eraldi failid. Vanem lahendab projektiülesandeid pärast kõikide agentide lõpetamist.

## Kontrollimine ja tarnimine
- [x] dotnet build Aivana.slnx -c vabastamine: null vigu.
- [x] dotnet jookseb -projekti testid/Aivana.Testid -c Väljaandmine: käitumisharjumused.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --sõbralikus tõelises -o artiklites/win-x64.
- [x] Published executable, inspekteerib Windows UI ja protsessi stabiilsust, kasutab turvalist profiili / redigeerimise / püsivuse voolu.
- [x] Kirjutage README ehitus- ja alustamisjuhenditega ning seadme piirangudega; salvestamine ja jäävad ainult seadme testid.

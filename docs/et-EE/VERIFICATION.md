<!-- generated-doc locale=et-EE source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../VERIFICATION.md)

# kontrolli – 8. oktoober 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Rakendus on ehitatud Windows x64 stabiilne .NET-SDK 10.0.202 ja avaldatud iseseisvalt Windows-x64-Ausgabe.

## Automaatselt kontrollitud

- Üldine ehitus: ei ole hoiatusi ega vigu.
- 19 Core käitumistestid: standardkonfiguratsioon, valideerimine, null/duplikad andmed, profiili prioriteet, importimine/ekspord ja tagasiside taastamine, makro järjekord ja korratsioon, katkestamine, topeltlahendamine, seeriaalsus, muutumatu rakenduse tülitamine ja samm-pikkus.
- Windows test: Hotkey standardiseerimine ja aliaarne, emakeelsed struktuurid, reaalne klaviatuuri seadmete nimekiri, turvaline registreerimine ja taaskasutamine, sünteetiline WM_HOTKEY kammimiseks ja katkestamiseks, deaktiveeritud kammimised sisestamise ajal.
- Iseseisev EXE algas `--smoke-test`: kõik viie WPF vaateid saab paigaldada / kopeerida / kustutada, disainid jäävad värskendamise ajal, konfigureerimine salvestatakse ja laaditakse uuesti. See test kasutab isoleeritud ajutist konfigureerimiskirja ja ei teosta teksti- ega meediatoiminguid.
- Dashboard, redaktor ja kompaktsed näitused (900 × 720) on visuaalselt kontrollitud kui tegelikud WPF rendid. Disaini säilitamine, tagasilükkamine, makro-süsteem, saksa hotkey liis, individuaalsed makromajutused ja samm-sammult edusammud on kontrollitud.

## Hardware sõltuvad piirid

Viimane kohalik läbimurre tuvastas neli Windows klaviatuuri. See arv kirjeldab HID-/Windows vahemikku, mitte tingimata neli füüsilist klaviatuuri.

Ametlik iCUE SDK on paigaldatud ja asub EXE kõrval. Live-testi ühendab iCUE ja tuvastab Corsair klaviatuuri mudeli- ja LED-andmetega. Valguse muudatused ei ole automaatselt tekkinud; mudelipõhised väljavõtted ja muud tootjad ei ole katsetatud. Üldised hotkeys kehtivad klaviatuuri vahel.

Teiste avatud kasutajaprogrammide teksti- ja meediumisseadmed ei ole süstida. Natiivsed andmestruktuurid ja viga/vaba logika on uuritud; konkreetse eesmärgiprogrammi käitumine sõltub muu hulgas selle õigustase.

## Korduvad

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI-testi tulemus ja ekraani kujutis on kontrollitud EXE kõrval `smoke`. See on mõeldud diagnostiliseks viisiks ja pärast lõpetamist ei alusta püsivat taustaprotsessi.


## Laiendatud kontrolli

Lisaks kontrollitud: profiili tingimused protsessi ja akna pealkirjad, muutujate ümberkujundamine ilma rekurssiivse laienemiseta, makrovalideerimine ilma rakendamata, uus tegevusvalideerimine ja vanade JSON-failide ühilduvus. Native testid katavad juhtimispaletti, mis sisaldab registreerimist, konflikte, WM_HOTKEY dispatch ja vabastamist, samuti puhta monitori ja vajadusel kontrollitud klipploendi juurdepääsu. WPF test avab ja rendib tegelikku palli, kontrollib tagasipöördumist / taastamist ja lemmikloomeid, kontrollib puhtaid disainid pärast rendimist ja lõpetab protsessi edukalt.

Windows blokeerimine, vahe- ja akna liikumised ei kohaldatud kasutajatele testimise ajal. Aktsia toimingud postitavad Windows taotlusi asüümselt; konkreetsel eesmärgiprogrammil võib olla oma miinimummahtu või lubatud piirid. Profiilide sulgemine ja rakendamise statistika kehtivad käimasoleva istungi suhtes.


Laiendamise uurimine 12.10.2026: 30 Põhilised testid; Windows-Adapterid, sealhulgas sissejuhatuse ajal vabastamine, kirjalikult kaitstud monitori tuvastamine ja OBS-Status-Mock on olemas. WPF kontrollib tulemuste vastuvõtmist/kiri, rekordite alustamist/kiirustamist, profiili vahetamise prognoosimist, käitumist ja sümboolseid ressursse.: 27 Kataloog koos 967 Võti; dokumentatsioon: 84 Leheküljed neljas keeles. omanikud teiste tootjate nupud Corsair ei ole katsetatud; üldine tugi hõlmab seadmepõhiseid juhtumeid F13–F24 pilvega transportimine toimub valitud sünkroniseeritud faili kaudu; importi kontrollitakse ja jääb deaktiivseks.

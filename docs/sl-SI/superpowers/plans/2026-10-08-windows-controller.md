<!-- generated-doc locale=sl-SI source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Upravni načrt za izvajanje

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Za agentične delavce: uporabite vzporedno izolirano lastnino; uporabnik je izrecno zahteval popolno izvajanje z največjimi uporabnimi podrejenci.

**Cilj: ** Dostava domačega Windows ključnega vratu v odobrenem oblikovanju.
**Arhitektura:** Ključna konfiguracija in preklicna izvedba; Windows Adapterji; WPF Desktopna sestava.
**Tehnični stack:** .Neto 10, WPF, Win32, JSON. brez plačanih odvisnosti.
**Spec:** ../specs/2026-10-08-Windows-controller-design.md

## Globalne omejitve
Nemški uporabnik; lokalno shranjevanje; Windows x64; stanje verodostojne zmogljivosti strojne opreme; brez logiranja tipkovnice; brez privzetega zagona.

## Pregled fokus
Slabo oblikovani uvoz ne sme nadomestiti shranjenih nastavitev; ohranjeni spremembe ne smejo onesnaževati proizvodnje; neplugirana oprema ne sme padati; zaprtje mora ohraniti razpoložljivost; odpovedani makri morajo sproščati vire.

## naloge in lastništvo
- [x] Core agent: src/Aivana.Core in testi/Aivana.Testi Modeli, trajnost JSON, validacija, profilna izbira in MacroRunner. Izvedite neuspešne vedenjske teste in nato izvajate vse osnovne teste.
- [x] Windows agent: src/Aivana Windows RegisterHotKey, oprema, SendInput, predogledni proces in opcijski Corsair svetlobni adapter. Preverite domače velikosti in registracijo čiščenja z varnostnimi testi za kajenje.
- [x] GUI agent: src/Aivana.Desktop/MainWindow.xaml in .cs plus UI pomočniki samo. Dark dashboard, polni urednik, profili, naprave, nastavitve, pregled tipkovnice in makro urednik. Zgradite, ko obstajajo pogodbe.
- [x] Starši: skupni pogodbe, projektne datoteke, sestava DesktopController, življenjski cikel aplikacije/izvoza, dialogi za uvoz/izvoz, dokumentacija, pregledi integracije, objavljanje in izvajanje aplikacije.

## Skupni vmesniki
Pogodbe živijo v Core/Models.cs. Vodenje upravljavca živi v Desktop/DesktopController.cs. UI imenuje metode kontrolorja skozi to konkretno razred. Agenti prejmejo natančne podpise v svojih nalogah. Vsak od njih ima ločeno datoteko. Starši rešujejo križarjenje po končanem delu vseh agentov.

## Preverjanje in dostava
- [x] dotnet build Aivana.slnx -c Izdaja: nič napak.
- [x] dotnet poteka -projekt testi/Aivana.Testi -c Izdaja: vedenjske preglede potekajo.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Začetek objavljenega izvršljivega, preverjanje Windows UI in stabilnost postopka, varno izvajanje profila / urejanja / trdnosti.
- [x] Napišite README z navodili za gradnjo/start in omejitvami za strojno opremo; zapisovalne preglede in preostali testi strojne opreme.

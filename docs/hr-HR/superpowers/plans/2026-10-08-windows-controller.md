<!-- generated-doc locale=hr-HR source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Plan provedbe kontrolora

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Za agentičke radnike: koristite paralelno izolirano vlasništvo; korisnik je izričito zatražio potpunu implementaciju s maksimalnim korisnim podagentima.

**Cilj: ** Dostavi domaći Windows hotkey panel u odobrenom dizajnu.
**Arhitektura:** Ključna konfiguracija i otkaziv izvršenje; Windows Adapter; WPF Desktop sastav.
**Tehnički stack:** .neto 10, WPF, Win32, JSON. bez plaćene ovisnosti.
**Spec:** ../specs/2026-10-08-Windows kontrolor-design.md

## Globalni ograničenja
Njemački korisnik; lokalno skladištenje; Windows x64; stanje pristojnosti hardvera; bez logiranja na tipkovnici; bez privremenog početka.

## Pregled fokusiranja
Zlonamjerni uvoz ne smije zamijeniti sačuvane postavke; zadržavani modifikatori ne smiju kontaminirati proizvodnju; neplugirana oprema ne smije padati; zatvaranje mora zadržati dostupnost traja; otkazani makri moraju osloboditi resurse.

## Zadatke i vlasništvo
- [x] Core agent: src/Aivana.Core i testovi/Aivana.Testi Modeli, JSON upornost, validacija, izbor profila i MacroRunner. Provedite neuspješne testove ponašanja, a zatim provedite i provodite sve osnovne testove.
- [x] Windows Agencija src/Aivana.Windows. RegisterHotKey, inventari uređaja, SendInput, proces prednjeg područja i opcionalni Corsair provjerite veličine domaćih struktura i registraciju čišćenja sa sigurnim testovima dima.
- [x] GUI agent: src/Aivana.Desktop/MainWindow.xaml i .cs plus korisnici samo. Dark dashboard, pun urednik, profili, postavke, pregled tipkovnice i makro urednik. Uskoro postoje ugovori.
- [x] Roditelj: zajednički ugovori, projektne datoteke, sastav DesktopController, životni ciklus aplikacije/izvoza, dijalogi za uvoz/izvoz, dokumentacija, provjere integracije, objavljivanje i rad aplikacije test dima.

## Zajednički interfejsi
Ugovori žive na Core/Models.cs. Ugradnja kontrolora živi u Desktop/DesktopController.cs. UI naziva kontrolorske metode kroz ovu konkretnu klasu. Agenti dobivaju točne potpise u svojim zadatcima. Svaki od njih posjeduje pojedinačne datoteke. Roditelj rješava neuspjehe nakon završetka svih agenata.

## Verifikacija i isporuka
- [x] dotnet build Aivana.slnx -c Izlazak: 0 pogrešaka.
- [x] dotnet run --project test/Aivana.Tests -c Release: behavioral checks pass.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Početak objavljivanja izvršljiv, ispitivanje Windows UI i stabilnost procesa, vježbanje sigurnog profila / uređivanja / trajnosti.
- [x] Napišite README s uputama za izgradnju/start i ograničenjima hardvera; snimke i preostali testovi hardvera.

<!-- generated-doc locale=sr-Latn source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Plan implementacije kontrolera

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Za agentičke radnike: koristite paralelno izolovano vlasništvo; korisnik je izričito zatražio kompletnu implementaciju sa maksimalnim korisnim subjektima.

**Cilj:** Dostavite rođendanski Windows točkovi panel u odobrenom dizajnu.
**Arhitektura:** Ključna konfiguracija i otklanjanje; Windows Adapter; WPF Desktop kompozicija.
**Tehnički stak:** .NET 10, WPF, Win32, JSON: Nema plaćene zavisnosti.
**Spec:** ../specs/2026-10-08-internet kontroler-dizajn.md

## Globalni ograničenja
Nemački UI; lokalno skladištenje; Windows x64; istinsko hardversko stanje; nema log-tajlovima; nema podrazumevanog autostarta.

## Pregled Fokus
Povređeni uvoz ne sme zameniti spasene podešavanja; držeći modifikatori ne smeju da zagađuju proizvodnju; neplagirani hardver ne sme da se sruši; zatvaranje mora da čuva dostupnost trake; otklonjeni makrosi moraju da oslobađaju resurse.

## Zadatak i vlasništvo
- [x] Core agens: src/Aivana.Core i testovi/Aivana. Modeli, JSON upornost, validacija, izbor profila i MakroRunner. Izvršite neuspešne testove ponašanja, a zatim implementirajte i sprovodite sve ključne testove.
- [x] Windows agent: src/Aivana Windows RegistarHotKey, inventar uređaja, SendInput, predgradni proces i opcionalni Corsair adapter za osvetljenje. Proverite veličine građevine i registraciju čišćenja sa bezbednim testiranjem dima.
- [x] GUI agent: src/Aivana.Desktop/MainWindow.xaml i .cs plus UI asistenti samo. Temna tastatura, puni urednik, profili, uređaji, podešavanja, pregled tastature i makro uređaj. Uspostavljaju se kada postoje ugovori.
- [x] Roditelj: zajednički ugovori, datoteke projekta, sastav DesktopKontrolera, aplikacija / traj život ciklus, uvoz / izvoz dijaloga, dokumentacija, integracija provere, objavljivanje i pokretanje aplikacije test dima.

## zajednički interfejs
Ugovor živi u Core/Models.cs. Kontroler implementacija živi u Desktop/DesktopKontroler.cs. UI poziva kontrolere metode kroz ovu konkretnu klasu. Agenti dobijaju tačne potpise u svojim zadatcima. Svaki od njih ima posebne datoteke. Roditelj rešava krozprojekat neuspjehe nakon završetka svih agenta.

## Verifikacija i isporuka
- [x] dotnet build Aivana.slnx -c Izlazak: nule greške.
- [x] dotnet run --project tests/Aivana.Tests -c Release: behavioral checks pass.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Pokretanje objavljeno izvršivo, inspektiranje Windows IU i stabilnost procesa, vežbanje bezbednog profila / uređivanja / trajnosti.
- [x] Napišite README sa uputstvima za kreiranje / pokretanje i hardverskim ograničenjima; zapisi i ostali hardverski testovi.

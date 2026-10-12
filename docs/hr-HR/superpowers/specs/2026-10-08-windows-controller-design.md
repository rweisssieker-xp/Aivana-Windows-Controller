<!-- generated-doc locale=hr-HR source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Projekt

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stanište: 8. listopad 2026. Status: ispitivanje prije provedbe.

## Cilj i dogovoreni stupanj

Neovisna aplikacija Windows čini normalnu PC tipkovnicu slobodno konfiguriranom akcijskom panelu sličnom toku. Korisnik je potvrdio sve predložene vrste akcija, makro, prepoznavanje uređaja, profili i dobar GUI. Priključene Corsair i druge tipke su prepoznate; dodatne funkcije proizvođača su dostupne samo ako su stvarno podržane.

Pretpostavke: njemačka površina, lokalna upotreba bez računa, Windows 11 x64 kao prva svrha, nema instalacije pogona. Ključ ostaje dostupan za normalno pisanje. Globalni uređaji standardno koriste kombinacije tipki s modifikatorima. Jednostavni tipki nisu standardno globalno rezervirani.

## Tehnička odluka

Native C#/NET-10-App s WPF i MVVM. Stabilni .NET-10-SDK je dostupan u radnom prostoru. WPF omogućuje individualno dizajnirani desktop i izravnu integraciju Windows. Alternativno bi bilo da je web interfejs s odvojenom uslugom Windows ili da je aplikacija za radna mjesta na platformi; oboje donose dodatnu radnu i komunikacijsku složenost za ovaj projekt Windows. Native varijanta je, dakle, izabrana osnova.

Razdvajanje: Desktop projekt za vid i traje, Core projekt za konfiguraciju i logiku izvršenja, Windows projekt za hotkeys, simulacije ulaza i uređaja, testni projekt za neovisna pravila. Proizvođači adaptera vezani su za čvrstu interfejs kapaciteta i neovisni su o standardnim akcijama.

## Površina

Mračni dizajn s mirnim antrazitskim površinama, dobro čitljivim pisanjem i turski akcentom. Poveznice za navigaciju za Dashboard, profile, uređaje i postavke. U nastavku aktivni profil, pretraživanje i prebaciti sve hotkeys. Površina ostaje dostupna na 100–200 procenta Windows stupnjeva.

Dashboard sadrži prilagodljiv raster. Svaka kutija prikazuje simbol, ime, hotkey i status izvršenja. Klikovi provode istu akciju kao i Hotkey. Na desnoj strani se otvara urednik s tipom akcije, parametrima, snimkom hotkey-a, simbolom/barvom, aktiviranjem i testnim gumbom. Novi, duplicirati, ukloniti i preusmjeriti su dostupni. Prikaz tipkovnice prikazuje shematičke postavke za njemački standardni raspored; ne tvrdi točnu geometriju prepoznatljivog uređaja.

Profili se mogu stvoriti, kopirati, prenijeti i aktivirati ručno. Opcionalno se dodjeljuje naziv procesa profilu. Primjerice, odgovarajući osnovni program aktivira svoj profil, inače se primjenjuje globalni standardni profil. Za više odgovarajućih profila primjenjuje se izričito vidljiva redoslijed. Redatelj objašnjava Hotkey sukobe izravno na lokaciji.

## Hotkeys i ulaznice

Windows RegisterHotKey obrađuje uobičajene globalne kombinacije s potisnutim ponovnim tipkama. Samo aktivni profil se registrira. Rezervirane ili već zabilježene kombinacije pojavljuju se kao pogreške; aplikacija ih ne prijavljuje kao uspješno aktivirane. Prijava u uredniku ne otkloni akcije.

Prije simuliranih ulazaka, otpuštaju se ili se očekuje da će biti fizički otpušteni. Unicode tekst i kombinacije tipki šalju se putem SendInput. Svaki sam pritisnut tipak će se ponovno osloboditi čak i u slučaju razbijanja. Akcije rade izvan UI trake; provedba i promjena profila ne smiju blokirati površinu.

## Akcije i makro

Vrste akcija: pokrenuti program s argumentima i radnim listom, otvoriti datoteku/plate preko Windows, otvoriti HTTP/HTTPS web stranicu, unicode tekst, poslati kombinaciju tipki, medija Play/Pause/Nadnji/Predsjednik/Stop, promijeniti glas više/niži/zvučni, kao i profil. Parametri se potvrđuju prije skladištenja i ponovno prije izvršenja. Programovi se izravno pokreću; argumenti se ne tumače kao zapovijedi za shell.

Makro sadrži redoviti popis navedenih akcija i jasne korake. Korake se mogu dodati, izmijeniti i odgoditi. Ponavljanja su konačno konfiguracijska (1–100); bezuvjetna slika ne pripada prvoj verziji. Radni makro može se završiti preko površine ili vlastitog raspada. Ponavljanje istog tekućeg pokretanja ne započinje paralelnim tečajem. Različiti makri s ulaznim simuliranjem serijalizirani su tako da se njihovi tipkovnici ne miješaju. U slučaju pogreške, makro se zaustavlja i prikazuje pogođeni korak.

## Uređaji i proizvođači

Proizvodni list Windows Raw Input i dodatne informacije o uređaju: ime prikaza, put prikaza, identifikacija proizvođača/proizvoda, ako je dostupna, status povezivanja i prepoznatljive sposobnosti. Slijedeći članakSlijedeći Članak Slijedeće Članak Aktualizira popis bez ponovnog pokretanja. Fizička oprema može posjedovati više HID ulazaka; oni se skupljaju prema opterećenim identifikacijama, inače su vidljivi kao odvojen ulaz.

Normalni hotkeys primjenjuju se međusobno. Vizija uređaja pruža informacije i vještine; odvojeno tipkovanje s isključivo zatvorenim tipkama nije dio ovog dogovorenog načina.

Corsair - prepoznavanje djeluje i bez iCUE. Opcionalni iCUE SDK adapter pruža stanje povezivanja i podržane svjetlosne akcije kada su prisutni SDK, iCUE i kompatibilna uređaj. UI razlikuje prepoznatljiv, SDK dostupna, povezana i stvarno upravljiva. SDK je posebno dokumentirao LED kontrolu; vlastiti G tipke podržavaju se samo kao uobičajeni hotkeys kada Windows dobiva odgovarajuće ulaznice. Ne postoji pauzna obećanja za sve zglobove.

Drugi proizvođači prvo dobivaju generičku identifikaciju i standardne akcije. Interfejs adaptera omogućuje kasnije integracije; bespodržene vještine će se iskreno prikazati. Hardver testovi za ne postojeće modele nisu navedeni kao preostali.

## Skladištenje i svakodnevni život

Konfiguriranje JSON-a u lokalnom adresaru podataka aplikacija. Nuklearna skladištenja s posljednjim važećim zaštitom; pogrešne datoteke se ne prepisuju. Uvoz provjerava formate, verzije, ID-ove i parametre te prikazuje predviđanje prije preuzimanja. Uvozne akcije mogu se provoditi tek nakon izričite aktivacije. Izvoz sadrži profile i dokumente.

Tray Menu: Otvoriti, odabrati profil, pauzirati hotkeys, razbijati makro, završiti. Prozori su minimalno zatvoreni u traju i objašnjavaju to prvi put. Početak je opcionalan i standardni. Samo jedna institucija upravlja Hotkeysom. Završetak registrira hotkeys, razbija izjave i oslobađa resurse.

Lokalni pogled na aktivnosti s ograničenim tijekom za uspjeh, pogreške i prekid. Nema protokola svih tipkovnica. Sadržaj teksta se ne piše u porukama o pogrešama. Nema oblaka i nema telemetrije.

## Pogrešno liječenje i granice

Nedostatak programa, nevažeći URL-ovi, zabilježeni hotkeys, oštećeni profili i nedostupni proizvođač SDK dobivaju konkretne njemačke poruke. Adapter pogreške ne utječu na standardne aktivnosti. Ulaznice u privilegiranim aplikacijama mogu neuspjeti na Windows dozvola granica; pogreške su objasnjeno, aplikacija ne zahtijeva standardne administratorske prava.

## Ispitivanje i smanjenje

Automatizirani testovi za prioritet profila, otkrivanje sukoba, uvoz konfiguracije, atomsko skladištenje, makro redoslijed, ponavljanje, razbijanje i propovijedanje pogrešaka. Windows Smoke testovi za stvarne hotkeys, tekst i kombinacije tipki, medija akcije, oprema inventari, trage i čisti završetak. Nema testova koji slučajno pokrenuti bilo koje programe ili web stranice.

GUI ispitivanje: postavljanje i uređivanje, snimanje bez izvođenja, klik/hotkey s istim učinkom, makro-razbijanje, priopćenje o sukobu, promjena profila, prikaz uređaja, uvoz/eksport i skaliranje. Proizvođač specifičan test hardvera je dokumentiran odvojeno s stvarno postojećim uređajem.

Dostava: izvorni kod, reproduktivna građevinska/startanska uputa, njemačka uputa za korištenje i lokalni Windows-x64-Publish-Artefakt. Uspjeh znači izvršljiva aplikacija sa svim općenitim funkcijama; Corsair vještine su označene kao dostupne samo kada je dokazano da je podrška SDK/Uređaj.

## Tehnički izvori

- Microsoft Ključni ulaz / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Uvod: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Službeni Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Sljedeći korak

Nakon razmatranja ovog projekta: izraditi konkretan plan provedbe i predložiti odabir provedbe. U praznom rasporedu još uvijek nema Git-Repository; stoga ovaj dokument još nije obuhvaćen.

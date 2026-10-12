<!-- generated-doc locale=sr-Latn source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Projekat

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stani: 8. oktobar 2026. Status: za ispitivanje pre sprovođenja.

## Cilj i dogovoreni nivo

Nezavisna aplikacija Windows čini normalnu tastaturu PC-a slobodno konfigurisanim panelom akcija sličnim potoku. Korisnik je potvrdio sve predložene vrste akcija, makros, prepoznavanje uređaja, profile i dobar GUI. Priključene tastature Corsair i druge tastature se prepoznaju; dodatne funkcije proizvođača se nude samo ako su zapravo podržane.

Pretpostavlja se da nemačka površina, lokalna upotreba bez računa, Windows 11 x64 kao prva cilj, nema instalacije vozača. Tastatura ostaje u stanju da se koristi za normalno pisanje. Globalni uređaji standardno koriste kombinacije tastera sa modifikatorima. Pojedinačni tasteri nisu rezervisani po standardu.

## Tehnička odluka

Nativan C#/NET-10-App sa WPF i MVVM. Stabilni .NET-10-SDK je dostupan u radnom prostoru. WPF omogućava individualno dizajniran desktop interfejs i direktnu integraciju Windows. Alternativa bi bila veb interfejs sa odvojenim uslugom Windows ili interplatformenska desktop aplikacija; oba donose dodatnu vreme i komunikacijsku složenost za ovaj projekat Windows. Narodna varijanta je zato izabrana osnova.

Distribucija: Desktop projekat za vizije i trake, Core projekat za konfiguraciju i logiku izvođenja, Windows projekat za hotkei, simulacije i uređaja, test projekat za platforme nezavisne pravila. Proizvođači adapteri su povezani na čvrstom interfejsu kapaciteta i nezavisni su od standardnih akcija.

## površine

Temni dizajn sa mirnim antrazitnim površinama, dobro čitanim pisanjem i turkistim akcentom. Link navigacija za tastere, profile, uređaje i podešavanja. Na vrhu aktivnog profila, pretraživanje i prekidač za pauzu svih hotkei. Površina ostaje u stanju da se koristi na 100–200 procenat Windows skalacije.

Dashboard sadrži prilagođavajući raster. Svaki kockal prikazuje simbol, ime, hotkej i status izvršenja. Klik će izvršiti istu akciju kao i Hotkei. Na desnoj strani se otvara uređaj sa tipom akcije, parametrima, snimanjem, simbolom/bojom, aktiviranjem i testiranjem. Novi, dupliranje, uklanjanje i reorganizacija su dostupni. Pogled tastature prikazuje shematske tastere za nemački standard; on ne tvrdi tačnu geometriju prepoznatog uređaja.

Profili se mogu kreirati, kopirati, preimenuvati i aktivirati ručno. Opcionalno, ime procesa se pripisuje profilu. Odgovarajući program aktivira svoj profil, inače se primenjuje globalni standardni profil. Za nekoliko odgovarajućih profila primenjuje se jasno vidljiv poredak. Redaktor objašnjava konflikte Hotkei direktno na lokaciji.

## Hotkeys i ulaznice

Windows RegistarHotKey obrađuje uobičajene globalne kombinacije sa potisnutim povraćanjem tastera. Samo aktivni profil se registruje. Rezervisane ili već registrovane kombinacije se pojavljuju kao greške; aplikacija ih ne prijavljuje kao uspešno aktivirane. Upis u uređaju ne rešava akcije.

Pre simuliranih unosa, promenljivi tasteri se oslobađaju ili se očekuje njihovo fizičko oslobađivanje. Unicode tekst i kombinacije tipkovnica se šalju sa SendInput. Svaki pritisak će se ponovo oslobađati čak i u slučaju prekida. Akcije rade izvan UI trake; izvršenje i promena profila ne bi trebalo da blokira površinu.

## Akcije i makros

Tipovi akcija: pokretanje programa sa argumentima i radnom direktorijumom, otvaranje datoteke/foltova preko Windows, otvaranje HTTP/HTTPS veb stranice, unošenje teksta, isporuka kombinacije tastera, medijski igranje/posle/prethodni/stap, prelazak zvuka visok/niži/šum, kao i profil. Parametri se validiraju pre skladištenja i ponovo pre izvršenja. Programi se pokreću direktno; argumenti se ne interpretiraju kao zapovesti šala.

Makrosi sadrže organizovan spisak navedenih akcija i eksplicitne korake. Korak se može dodati, obraditi i odložiti. Ponavljanja su konačno konfigurisana (1–100); beskrajne trake nisu u prvoj verziji. Radeći makro može biti završen preko površine ili sopstvenog prekidača. Ponovljeno pokretanje istog tekućeg pokretanja ne počinje paralelno. Različiti makrosi sa simulacijom ulaza se serializuju tako da se njihovi tasteri ne mešaju. U slučaju greške, makro se zaustavlja i prikazuje pogođeni korak.

## Uređaji i proizvođači

Uređajne informacije Windows Raw Input i dodatne informacije o uređaju: ime, put uređaja, identifikacija proizvođača / proizvoda, ako je dostupno, stanje povezivanja i prepoznate veštine. Ažuriraju listu bez ponovnog pokretanja. Fizičko uređaj može imati nekoliko HID ulaza; oni su grupirani po opterećenim identifikacijama, inače su vidljivi kao odvojeni ulazi.

Normalni hotkei se primenjuju na tastature. Pogled uređaja pruža informacije i veštine; odvojena tastatura sa isključivo uhvaćenim tasterima nije deo ovog dogovorenog režima.

Corsair - prepoznavanje funkcioniše čak i bez iCUE. Opcionalni iCUE SDK adapter pruža stanje povezivanja i podržane akcije osvetljenja kada su dostupni SDK, iCUE i kompatibilni uređaj. UI se razlikuje prepoznatljiv, SDK dostupan, povezan i zapravo upravlja. SDK uglavnom dokumentira LED kontrolu; vlasnički G tasteri su podržani samo kao i uobičajeni Hotkei kada Windows dobija odgovarajuće unose. Ne postoji pasivni obećanje za sve vrste.

Drugi proizvođači prvo dobijaju generalno otkrivanje i standardne akcije. Adapter interfejs omogućava kasnije integracije; nepodržene veštine se iskreno prikazuju. Testovi hardvera za ne postojeće modele se ne navode kao postojeći.

## Skladištenje i svakodnevni život

Konfiguracija JSON-a u lokalnom direktorijumu podataka aplikacija. Atomsko skladištenje sa poslednjim važećim rezervnim kopijama; pogrešne datoteke se ne prepisuju. Import ispituje format, verziju, identifikatore i parametre i prikazuje pregled pre preuzimanja. Uvozne akcije mogu se izvršiti tek nakon izričnog aktiviranja. Eksport sadrži profile i obloge.

Trea meni: Otvoriti, izabrati profil, prekinuti hotkei, prekidati makros, završiti. Prozori se minimalno zatvaraju u trai i objašnjavaju to prvi put. Pokretanje automobila je opcionalno i standardno. Samo jedna institucija upravlja Hotkei. Završiti registruje hotkeis, prekida izložbe i oslobađa resurse.

Lokalni pogled na aktivnosti sa ograničenim tokom za uspeh, greške i prekid. Nema protokola o svim tastaturama. Sadržaj tekstova se ne izpisuje u porukama greške. Nema oblaka i telemetre.

## Pogrešno tretiranje i granice

Nedostatak programa, nevažeći URL-a, oštećeni profili i nedostupni proizvođač SDK dobijaju konkretne nemačke poruke. Greška adaptera ne utiče na standardne akcije. Ulozi u više privilegiranim aplikacijama mogu ne uspeti na Windows dozvola; greške se objašnjavaju, aplikacija ne zahteva standardne administratorske prava.

## Ispitivanje i smanjenje

Automatizovani testovi za prioritet profila, otkrivanje konflikta, uvoz konfiguracije, atomsko skladištenje, makroregion, ponavljanje, prekidanje i propaganda grešaka. Windows Smuke testovi za stvarne kockeje, tekstualne i tastature kombinacije, medijske akcije, uređajne skladištenja, trake i čiste završetka. Nema testova koji slučajno pokreću bilo koji program ili veb stranice.

Ispitivanje GUI-a: kreiranje i uređivanje, snimanje bez izvršenja, klik/hotchei sa istim efektom, makroukidanje, poruka o konfliktu, promena profila, pogled na uređaj, uvoz/eksport i skaliranje. Proizvođač-specifična hardverska ispitivanja se dokumentiraju odvojeno sa stvarno postojećim uređajem.

Dostavljanje: izvorni kod, reprodukovani vodič za izgradnju / pokretanje, nemački vodič za upotrebu i lokalni Windows-x64-Publish-Artefakt. Uspeh znači izvršnu aplikaciju sa svim generalnim funkcijama; Corsair veštine su označene kao dostupne samo ako se dokazuje podrška SDK/uređaja.

## Tehnički izvori

- Microsoft Tabela ulazak / RegistarHotKei: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF ulazak: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Zvanični Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Sledeći korak

Nakon razmatranja ovog projekta: pripremanje konkretnog plana za implementaciju i predstavljanje za izbor implementacije. U prazan radni red još uvek nema Git-repozitorijuma; ovaj dokument nije kommitovan.

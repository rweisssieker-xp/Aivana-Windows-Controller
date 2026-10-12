<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-12-six-extensions.md)

# Šest proširenja: implementacija i stepen ispitivanja

Stani: 12. oktobar 2026, industrija `codex/six-extensions`. Radno stanje, bez otpuštanja proizvodnje.

## Usluge

Uputstvo za otvaranje ulaza **Uređivanje radnog mesta**, **Proizvođači tastatura i SDK**, **Mobilni panel**, **Proširenja**, **UI automatizacija**, **UI završetak** i **Izvršni pregled**.

- Radni asistent kreira profile za radno mesto, sastanak, striming, istraživanje i projekat. Programi iz registracije/pokretanja ili izbora datoteke, URL-a, izlaza / ekrana i OBS scene se pojavljuju kao koraci pre skladištenja. Udruženje ne sprovodi akcije.
- U redaktoru koraka **Rezultat pregleda** uključuje pregled i pregled. Tok i mobilni panel razlikuju stvarnu potvrdu od neistraživog efekta. Relativne promene, ključevi i tekstualni unos ne pružaju pasivno potvrdu o efektu. Procesni dokaz ne dokazuje profesionalnu spremnost za primenu.
- UI automatizacija koristi odvojeni, vremenski ograničen radnik. Izaberite poziciju miša, kontrolu selektora, Invoke/Select/Toggle/Focus/SetValue/WaitState; odbijanje više golova, deaktiviranih ciljeva i lozinki. Snimanje prikuplja podržane klikove i pauze; tekstovi se ne skraćuju. Pre prijema se ispituje.
- Proširenja: ZIP ispitivanje, haševi, shema, deaktivirani uvoza, izričito poverenje pre aktiviranja, izolovani proces sa ograničenim IPC, izvoz, deaktivnost, deinstalacija, obnavljanje / odbijanje prethodne verzije. Proces izolacija nije peska: podeljeni plugini rade sa korisničkim pravama. Primerni paket `makrivo.example.zip` sadrži računanje i akciju statusa. Podržane šeme formiraju dokumentisani jednostavan JSON šema u izvornom kodu; nije javni tržište.
- Mobilni panel: izaberite lokalne IP i podeljene profile, otvorite HTTPS adresu, proverite sertifikat na osnovu prikazanog SHA-256-Fingerprints i svesno poverite, a zatim unesite jedinstvenu šestopisnu kod. Privatni ključevi su zaštićeni Windows-DPAPI; čuvaju se samo haševi klijentskih pristupa. Sertifikati se menjaju nakon obnove ili promene IP-a. Uspostavljanje Windows frizure se vrši od strane korisnika ako je potrebno. Nema automatske instalacije sertifikata, proširenja porta ili promena vatrenog zida. Klijentski pristup se može povući. Panel prikazuje stanje akcije i ispitivanje; još uvek nije univerzalna živa senzorska ploča.

## tastatura

| Proizvođač | implementacija | Ograničenje |
|---|---|---|
| Corsair | postojeći iCUE-G tasteri i RGB | Potrebna je lokalna iCUE veza i odgovarajući model |
| Logitech G | G-Key SDK i odvojeni LED SDK | originalni lokalni x64-DLL izričito; G-Ključ SDK ne pruža individualnu identitet više tastatura |
| Razer | Hroma RGB i standardni tasteri | Pre nego što se ukloni u Synapse |
| SteelSeries | GameSense-RGB i standardni tasteri prepoznavanja | Prethodni Članak Uređaj vlasničkih tastera u GG |
| Wooting | Analge vrednosti povezane sa uređajem, talasi pritiska / otpada | originalni lokalni x64-SDK-DLL; normalni HID ulazak ostaje aktivan |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Drugi | Fizički standardni tasteri po uređaju i skeniranje koda | FN i vlasnički elementi u zavisnosti od modela |

Nijedan novi sopstveni SDK DLL se ne isporučuje. Novi adapteri proizvođača još nisu validirani bez odgovarajućeg fizičkog uređaja.

**Ekskluzivna druga makropada nije završena: ** Raw Input posmatra uređaje odvojeno, ali ne potiskuje njihovo normalno unošenje Windows. Proizvodnja zahteva licencirani, potpisani, pod Windows 11/Secure brod proveren filter vozač, kao i testove za hitno oslobađanje, srušenje, suspendiranje i ponovno povezivanje. Nije instaliran vozač. Aplikacija jasno ukazuje na ovu granicu.

## Ispitivanje

- Izgradnja izdanja: uspešan, nule upozorenja / greške.
- Ključna konzola: sve testove su uspešne, uključujući konflikte sa proizvođačima, plugine i stvarne greške u ispitivanju.
- Integracija proširenja: stvarni UIA radnik sa WPF test aplikacijom; jedinstvena invokacija, multi-tačnost, deaktivirani ciljevi, isključivanje lozinke. Uspešno imenovani proces plugina, protokol granice, analogna histereza i Logitech ABI dekodiranje.
- Istinski HTTPS host: ugrađeni sertifikat, nepovezan pristup, lažni / jednokratni kod, zaštita porijekla, pretraživači datoteke, hash skladištenje i zaštićeni ključ uspešan. Ispravljen je greška u skeniranju sa efemernim ključevima.
- Windows-Suit sa izričito propuštenim testom uspeha: uspešno; audio/OBS/iCUE/Raw Input Istraživanje stanja ekrana. u prethodnom trčanju, kompletni suit je neuspeo na odbijenom prebacivanju fokusa na izolovani prozor. u sledećem trčanju na II prototipu, takođe je bilo kompletnog sueta, uključujući i prebacivanje fokusa i povezane iCUE-Ključ. još jedan ponavljanje je ponovo neuspeo u menjanju fokusa; kontekstualni test nije stabilan zeleni. Ovaj test nije oslabljen. Kratko propušteni DDC pregledi su ograničeni ponavljanja; nije promenjena svetlost.
- WPF Smake: sve prethodne i nove dijaloge reducirani, konfiguracija varijante, urednik i CRUD uspešno; Screenshots u građevinar `smoke`.
- Pretraživač: Playwright sa Edge, 390×844 i 1024×768, Uspešno povezivanje / UNID / jednokratno prenošenje / nije horizontalno prenošenje. API-Mock; pravi HTTPS test odvojeno. `artifacts/mobile-browser`. Pretraživač Plugin nije dostupan, tako da je redovna Playwright Fallback.
- Nezavisni pregled koda; kasni mobilni nalozi, nedostatak terminalnog ispitivanja rezultata procesa, Plugin Rollback i ponovno pokretanje RGB srca ispravljen.

Još uvek otvoren: ekskluzivna integracija vozača, fizički proizvodi / telefon / tablet LAN testovi, Windows skaliranje 150/200 procenat i potpuni prevod svih novih dijalog tekstova. Novi dijalogni tekstovi su uglavnom engleski; postojeći jezički katalozi ostaju. Ukupno je potrebno ** i nije potpuno smanjeno **.

## Proizvodnja paketa

`./tools/publish-complete.ps1 -SelfContained` stvara novu fasciklu pod `artifacts/release-*`, uključujući desktop, UIA radnik, HTTPS host, pretraživač datoteke, importiran primjer plugin i punjenje vreme licence. Instalirana aplikacija se ne prepisuje. Bez `-SelfContained` paketa treba odgovarajuće .NET-10-Desktop-/ASP.NET-Runtimes.

Paket je lokalni test, a ne potpisani prodavac. Svoje uslove korišćenja i prava prodaje SDK su odvojene izdanja; pogledajte postojeću dokumentaciju na `docs/licensing`.

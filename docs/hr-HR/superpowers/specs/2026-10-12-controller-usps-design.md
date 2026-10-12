<!-- generated-doc locale=hr-HR source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO kontrolna proširenja

Korisnik je zatražio sve devet predloženih poboljšanja i prepoznatljivu ikonu. Postojeće konfiguracije, osobito svjetlost G1/G2, moraju ostati kompatibilne. Postojeći neobavješteni branding i dokumentacija pripadaju zajedničkom radnom prostoru i moraju se sačuvati.

## Dostava i granice

1. Akcije svjetlosti odaberite sve podržane monitore ili stabilan specifičan ID monitora. Izgubljeni konkretni ciljevi očito ne uspiju. Dashboard prikazuje trenutnu svjetlost, količinu izlaska, stanje mikrofonova i stanje OBS kroz ograničene asinkronne čitanja.
2. Posebne ključeve podržavaju hitno pritisak, dodir i dvostruki dodir, s ponavljanjem. Drži se sloj selektor privremeno mijenja aktivno mapiranje i oslobađa ga. Postojeći neposredni obveznici zadržavaju svoje ponašanje. Pauza, promjene profila, isključivanje i otkazivanje jasnih žestoka.
3. Dizajn radnog toka izmjenjuje uvjete, napredne grane i stop/continue neuspjeha politika transakcijski u rasporedu. Uvjeti pokrivaju datoteke, proces rada i naslove prozora. Nema neograničenih ciklusa.
4. Objasnjeno snimanje uhvaća kratke korake i čekanja, ignoriše ulaz i zaustavlja na Escape. Zabilježene akcije preispituju se prije usvajanja; uobičajeni tekst nije zabilježen.
5. Standardni posebni ključi za svaki uređaj od drugih proizvođača koriste se Windows Raw Input. Proizvođački ključi zahtijevaju API-ja pružatelja ili mapiranje pružatelja na standardni ključ; nepodržana funkcionalnost je iskreno prikazana. Dijagnoza objašnjava profil, pauzu, obvezujuće sukobe i stvarno stanje adaptera.
6. Izmjena profila koristi korisnički odabran folder (uključujući dosje sinhronizirane postojećim klijentima u oblaku). Revizija provjera sprječava tihe prepisivanje. Pull predviđa promjene i uvozi deaktivirane akcije s uklonjenim hardverskim mapama. Uključivanje sadržaja je izričito. Ne smiju se pohranjivati nijedna obljetnica MAKRIVO.
7. UI lanaci pripadaju postojećim 27 jezicima; DE/EN/FR/IT ostaje minimalni preispitivani dokumentiranje jezika. Prevođenje mora biti otkriveno umjesto da se tvrdi da je potpuno.
8. Priznajuća oznaka za tipkovnicu M zamjenjuje ikonu Windows. Pauzirani način ima amber pauze. Izvršni/prstni branding koristi isti znak. Tooltip uključuje stanje i profil.

## Verifikacija

Core provjere pokrivaju usklađenost, snimke, otkazivanje, vremenski postupak, radne grane / pogreške, snimke filtracije i sinhronizacije sukoba. Windows provjere pokriti čitanje samo prikaz, SDK izdanje događanja i Raw Input parsing. WPF dima provjere pokriva nove ulazne točke i resurse bez promjene stvarne svjetlosti monitora, pisanje u drugu aplikaciju ili prepisivanje korisničkih profila. Ugradnja čuva kopiju i konfiguraciju G1/G2.

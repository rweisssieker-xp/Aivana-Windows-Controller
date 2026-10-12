<!-- generated-doc locale=sr-Latn source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO kontrolne proširenja

Korisnik je zatražio sve devet predloženih poboljšanja i prepoznatljivu ikonu. Postojeće konfiguracije, posebno G1/G2 svetlost, moraju ostati kompatibilne. Postojeći neodređeni brend i dokumentacijski uređaji pripadaju zajedničkom radnom prostoru i moraju se čuvati.

## Dostavljanja i granice

1. Akcije svetlosti biraju sve podržane monitore ili stabilan specifičan identifikator monitora. Nedostatak eksplicitnih ciljeva je vidljiv. Dašbord prikazuje trenutnu sjajnost, izlazni volumen, stanje mikrofona i OBS status kroz ograničene asinhronne čitanja.
2. Specijalni ključevi podržavaju neposredni pritisak, tap, drženje i dvostruki tap, sa ponovljenim suzbijanjem. Održani selektor sloja privremeno menja aktivnu mapiranje i oslobađa ga. Postojeće veze zadržavaju njihovo ponašanje. Pauza, promene profila, otključavanje i ukidanje jasnih žestokosti.
3. Dizajner radnog toka uređuje uslove, napredne grane i politiku zaustavljanja / nastavljanja neuspjeha transakcijski u dizajnu. Uslovi pokrivaju datoteke, procese i naslove prozora. Nema neograničenih ciklusa.
4. Ekskluzivno snimanje uhvati kratke kodove i čekanja, ignoriše ubrizgane ulaze i zaustavlja na Echape. Registrovane akcije se pregledaju pre usvajanja; obični tekst se ne zapisuje.
5. Standardni specijalni ključevi po uređaju koriste drugi proizvođači Windows Raw Input. Vlasnički ključevi zahtevaju podržani provajder API ili provajder mapiranje na standardni ključ; nepodržana funkcionalnost se iskreno pokazuje. Dijagnoza objašnjava profil, pauzu, obavezni konflikti i stvarno stanje adaptera.
6. Profil razmena koristi korisnički odabrani zajednički fasciklus (uključujući i fascikle sinhronizovane postojećim klijentom u oblaku). Pregledi sprečavaju tihi prepisi. Pull predviđa promene i uvozi onemogućene akcije sa hardverskim mapiranjem uklonjenim. Uključivanje sadržaja je jasno. Nijedan oblak nije sačuvan od MAKRIVO.
7. UI trake pripadaju postojećim 27 jezičkim katalozima; DE/EN/FR/IT ostaju minimalni pregledani dokumentirani jezici. Prevodilački propust mora biti otkriven umesto da se tvrdi kompletan.
8. Prepoznavajući tastaturni znak M zamenjuje ikonu generalnog Windows trajanja. U pauzu ima amber pauzu. Izvršni / prozorski brend koristi istu marku. Alattip uključuje status i profil.

## Verifikacija

Ključni pregledi pokrivaju kompatibilnost unazad, snimak, ukidanje, raspoređivanje gestova, grane / greške radnog toka, filtriranje i sinhronizaciju konflikata. Windows pregledi obuhvataju samo čitanje prikazivanja, događaje izlaska SDK i Raw Input pasing. WPF dim pregledi uključuju nove ulazne tačke i resurse bez promene stvarne svetlosti monitora, štampanje u drugu aplikaciju ili prepisivanje korisničkih profila. Uspostavljanje čuva rezervnu kopiju i konfiguraciju G1/G2.

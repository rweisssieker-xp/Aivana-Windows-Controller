<!-- generated-doc locale=hr-HR source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: šest proširenja proizvoda i adaptera za tipkovnicu

Datum: 2026-10-12
Status: predložen dizajn za pregled korisnika; implementacija nije započela.

## Cilj
Dostavi sve šest zatraženih proširenja u postojećoj aplikaciji Windows/WPF, čuvajući postojeće profile, zadatke G1/G2, lokalno djelovanje i korištenje bez računa. Proširite integraciju tipkovnice na Razer i Logitech, s obnovljivom arhitekturom na temelju kapaciteta za druge proizvođače. Ime proizvođača ili raspored tipkovnice nikada nije dokaz da se vlastiti kontrolori podržavaju.

## Odluka o arhitekturi
proširenje Aivana.Core s serijaliziranim ugovorima i validacijom, Aivana.Windows sa Windows Prodavateljski adapteri i Aivana.Desktop s transakcijskim uređajima i pogledom na stanje. Koristite odvojene projekte za izolaciju izvršenja plugina i lokalnog mobilnog panela. Izbjegavajte ponovnu pisanje ili jedinstvenu privilegiranu procesnu hostel svaku proširenje. postojeće konfiguracije moraju biti nepromijenjene; dodajte vrste akcija umjesto da ponovite svoje trajne vrijednosti enum.

Razmatrane su alternative: monolitička implementacija omogućila bi provedbu hardvera, mreže i radnog toka; zamjena WPF dodala bi nerazmjerene migracijske poslove. Preporučuju se modularne proširenja.

## 1. Neovisna tipkovnica Macro pad
Skladište vezanosti po identitetu uređaja plus fizički kod skeniranja i proširene ključne zastave. Dodajte izričito promatranje i ekskluzivne načine, otkrivanje uređaja, ključni ulazak, ponavljanje zatvaranja, gesta, držanje slojeva i ponovno povezivanje dijagnoze. Održavajte glavne/numpad Enter razlike i testirajte njemačke/US postavke. Poveznice su usmjerene na odabranu fizičku tipkovnicu; nejasne identitete zahtijevaju ponovnu selekciju umjesto da kontroliraju drugi uređaj.

Raw Input Ekskluzivno uklanjanje po uređaju zahtijeva stvarni filter; nikada ne korelira globalne niske razine s Raw Input i zahtijevaju pouzdanu uređaj-specifično uklanjanje. pružiti nadzornik, hitno oslobađanje, neuspjeh-otvaranje ponašanja i instalator/uninstaller. Ne uhvatiti sigurni stolni ulaz..

Vozačka vrata: odaberite podržani, pravilno licencirani i potpisani backend koji je kompatibilan s ciljanim konfiguracijama Windows 11 ili razvijate/pisao posvećen filter. Interception je kandidat za procjenu, a ne posvećena ovisnost o proizvodnji: njegova službena README opisuje testiranje kroz Windows 10 i odvojeno komercijalno licenciranje. Sve dok se ova vrata ne zadovolje, ekskluzivni način mora biti prikazan kao nedostupan; značajka se ne može proglasiti punom.

Prihvaćanje: s dvjema fizičkim tipkovnicama, isti ključ izvršava makro i ne proizvodi normalni ulaz aplikacije samo na odabranom isključivom uređaju; drugi ulaz na tipkovnici ostaje neovisan. Provjerite isključivanje, nesreću, otkazivanje, suspenziju i uklanjanje vozača na stvarnom hardveru.

## 2. Uređivanje radnog prostora
Dodajte čarobnjak za radno mjesto, sastanke, prijenos, istraživanje i prijelaz projekta. Otkrijte instalirane aplikacije, audio uređaje, monitore i OBS scene. Korisnici odabiru ciljeve i obveze; predvidjeti posljednje korake i potvrditi nedostatke zahtjeva. Samo nakon pregleda, bez akcija tijekom postavke. Proizvodi koji se generiraju ostaju uređivani u postojećim urednicima.

Prihvaćanje: stvaranje radnog profila bez ručnog uređivanja izvedivih staza ili ID-a uređaja; otkazivanje ostavlja postavke nepromijenjene; nedostajuće softver ima djelotvorno objašnjenje.

## 3. Provjerena izvršenja
Uvođenje predvidljivih provjera, tipiranih rezultata po koraku i opcionalnih post uvjetima. Razlikujte zatražene, završene, provjerene, neuspješne, vremenske, otkazane i nepovratne stanja. Provjerite audio/mikrofon, svjetlost, procesnu/ prozornu spremnost i stanje OBS s ograničenim asinkronnim čitanjem. Kratka isporuka/teks nije dokaz da je ciljna aplikacija prihvatila zapovijed. Unutarnji nuspojave nisu univerzalno reverzibilni; nude kompenzaciju samo kada se primjenjuje.

Prihvaćanje: isključeni izričito ciljevi nikada ne padaju u tišini; pogrešno promatrani stanje nikada ne izvješćuje o provjereni uspjeh; otkazivanje zaustavlja ispitivanje; neuspjeh identificira korak i akciju oporavka. Povijest izvršenja propusti tekstne plaćanja i tajne.

## 4. Plugins i dijelljivi paketi
Versija JSON manifestira akcije, parametre sheme, državni pružatelji, ovisnosti i sposobnosti. Izvođenje izvršljivih plugina u odvojenom procesu preko ograničenog lokalnog IPC-a s ručnim prekidom, rokovima i otkazivanjem. Odbijanje loših poruka i neusklađenih verzija; rukovanje nesrećama bez prekida kontrolora. Izjave o sposobnostima su otkrivanje i posredovanje, a ne tvrdnja OS sandboxinga.

Pružite paket izvoza / uvoza predviđanja, sadržaj hash, zaštita putovanja, ograničenja veličine, provjere verzije / ovisnosti, izričito omogućiti i nadogradnju. Uvozni izvršljivi plugini ne bi trebali biti automatski izvršeni. Uključite radni primjer plugina s end-to-end instalacija / akcija / stanje / uninstall tok. Za ovo oslobađanje nije potrebna javna usluga.

Prihvaćanje: stvaranje, izvoz, instalacija i izvršavanje stvarne integracije; pokazati timout, izolaciju nesreća, neuspjeh ovisnosti, prekogranično odbijanje i onemogućiti uvoz.

## 5. Windows UI automatizacija
Dodajte piker elementa i ograničene akcije za poziv, vrijednost, odabir, fokusiranje i čekanje na element/država. Potrebno je nastaviti s procesom, automatizacijskim ID-om, kontrolnim tipom i kontekstom; izbjegavajte koordinatno samo snimanje. Prikazuje se broj natjecanja i testiranje natjecanja samo čitanje. Iz registracije su isključeni polja lozinke. Izričito upravljajte nepodržanim kontrolama i ograničenjima razine integriteta Windows.

Proširenje snimanja s revizibilnim djelovanjem UIA-e, zadržavajući tekuće snimanje. Ambiguozni selektor ne uspje umjesto da klikne na arbitrarnu kontrolu. Uobičajeno drvo pristupačnosti pretraživača nije obećanje punog DOM motora.

Prihvaćanje: automatizirajte posvećenu aplikaciju testiranja sa stabilnim selektorima u promjenama položaja/veličine; testne nesigurnosti, nedostatak elemenata, isključene kontrole, vremenske izlaske i otkazivanje.

## 6. Lokalni telefon / tableta
Poslužite odgovarajući panel s opt-in LAN usluge. Izričito koristite kratkotrajni kod jednog vremena i izdati povoljne potvrde zaštićene lokalno. Koristite HTTPS s dokumentiranim lokalnim certifikatom povjerenja/pinning; nema povratnih potvrda u pritužbi. Podrazumevano isključiti i ograničiti konfigurirane sučelje. Validacija izvora, autentificirane zapovijedi, ograničenja stope i ograničene poruke su obavezne.

Izložite dopuštene profile/akcije, živu situaciju, napredak i otkazivanje; koristite stabilne ID-ove i istu izvedbu kao desktop i fizičke ključeve. Ne otkrivajte voljne zapovijedi, izvršljive staze, tajne ili instalaciju plugina. Pružite povezivanje, pariranje i otkazivanje zaslona. Ne uključuje se nijedan račun u oblaku ili internet.

Prihvaćanje: djeluje s stvarnog telefona/tableta na istom LAN-u; neprijateljski klijenti ne mogu popisati ili pokrenuti akcije; otkazani tokeni ne rade; duplikirani/preizvedeno zapovijedi ne izvršavaju dvostruko; diskonekcija ne tiho povlači akcije.

## Proizvođač ključa

| Adapter / obitelj | Planirana integracija | Objasniti granice |
| --- | --- | --- |
| Corsair | Održavanje iCUE G ključ ulaz i RGB integracija | Potrebna je detekcija modela/software kapaciteta |
| Logitech G | G-key SDK adapter s tiskom/izlazakom i provjeravanjem dostupnosti; osvjetljenje kroz svoje odvojeno SDK gdje se podržava | Ne usporedite G HUB detekciju s podržanim API-om na svakom modelu |
| Razer | Adapter za osvjetljenje kroma i usmjerena Synapse-na standardni ključ, plus generski HID ulaz | Chroma je API osvjetljenja; nemojte ga označavati kao vlastiti makro ključ ulaza |
| SteelSeries | GameSense stanje/osvijetljenje adapter plus generski HID ulaz | GameSense podnošenje događaja nije opći vlasnički ključni API |
| Wooting | Analogni SDK adapter za podržani analogni ulaz, plus normalni HID ključi | Uvjeti distribucije kapaciteta i SDK-a moraju se provjeriti po podržanom modelu |
| Keychron/QMK/VIA | Standardni HID ključi i upute za kartografiranje firmware-a; poznati raspored samo s dokazima | VIA konfiguracijska podrška nije univerzalni Macro-key API |
| Ostali standardni HID tipkovnici | Fizičko otkrivanje i mapiranje po uređaju; ekskluzivni način samo s validiranim filterom | Nema izrađenog modela, posebnog Fn, dijalnog ili vlastitog ključa za podršku |

Svako uređaj prikazuje detektirani, promatrani, mapirani putem softvera dobavljača, SDK-a, isključivo za vozače, nedostupni ili hardverski status kao što je primjenjivo. Održavajte dokaze o sposobnosti s adapterskim verzijama i testiranim modelima/firmware identifikacijama.

## Dostava narudžbe
1. Konfiguracija kompatibilnosti, ugovori o kapacitetu i provjerena izvršenja.
2. Upravljanje pomoću stvarnih otkrića i postojećih izvršitelja djelovanja.
3. UI automatizacija i njezin zapisnik/test fiksiranje.
4. Plugin host, protok paketa i primjer integracije.
5. Plaćeni LAN panel i mobilna provjera.
6. Razvijeni adapteri i ekskluzivna integracija tipkovnice, s stvarnom hardverom.

Sve šest je potrebno. Potpisivanje/licencija vozača, nedostupni SDK-ovi ili odsutni testni hardver su vrata za oslobađanje, a ne razlozi za zamjenu ponašanja moka i poziv na posao.

## Kriteriji provjere i oslobađanja
Provedite postojeće jezgre, Windows i WPF provjerite i dodajte fokusirane testove integracije za svaku novu granicu. Provjerite JSON okrugle putovanja i naslijeđe profila; testirati uvezene izvršljive sadržaje, mrežnu autentifikaciju, otkazivanje, plugin nesreće i ponovno povezivanje uređaja. Provjerite desktop i mobilne postavke. Validirati visoke Windows skala odvojeno. Izgradite izdanje tek nakon provjere; razlikujte implementirane/testirane, implementirani/hardware-netestirane i vanjski blokirane funkcionalnosti u izdanjima. Nemojte zamijeniti instaliranu aplikaciju, instalirati vozače, promijeniti stvarne mape na tipkovnici ili otvoriti pristup mreži kao slučajni test.

## Dokazi
- Microsoft Raw Input Semantika: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G razvijalac SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Prihvaćanje depozita i uvjeti ovisnosti: https://github.com/oblitum/Interception
- Proizvoditeljski inventari: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Pregled odluka
Korisnik je zatražen da odobri ovaj pisani dizajn prije planiranja. Glavna vanjska ovisnost je proizvodno spremna ekskluzivna ulazna podrška. Preporučeni opseg zadržava ovo kao vrata dostupna umjesto da ga tiho smanjuje na Raw Input opažanja.

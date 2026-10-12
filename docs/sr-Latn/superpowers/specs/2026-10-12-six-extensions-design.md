<!-- generated-doc locale=sr-Latn source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: šest proširenja proizvoda i adapteri tastature

Datum: 2026-10-12
Status: predloženi dizajn za pregled korisnika; implementacija nije počela.

## Cilj
Dostavite sve šest zahtevanih proširenja u postojećoj aplikaciji Windows/WPF, sačuvajući postojeće profile, G1/G2 zadatke, lokalnu operaciju i besplatnu upotrebu. Proširite integraciju tastature na Razer i Logitech, sa ponovljivom arhitekturom zasnovanom na kapacitetu za druge proizvođače. Ime proizvođača ili raspored tastature nikada nije dokaz da se podržavaju kontrole za vlasništvo.

## Arhitekturna odluka
Proširite Aivana.Core sa serijalizovanim ugovorima i validacijom, AivanaWindows sa Windows i adapterima prodavca, i Aivana.Desktop sa transakcijskim uređajima i status pregledima. Koristite odvojene kompanije za izolovano izvršenje plagina i lokalnu uslugu mobilnog panela. Izbegavajte ponovno pisanje ili jedan privilegirani proces hosting svake proširenja. Postojeća konfiguracija mora biti nepromenjena; dodajte vrste akcija umesto da ponose svoje trajne vrednosti.

Alternative su razmatrane: monolitna implementacija bi uključila hardver, mrežu i izvršenje radnog toka; zamena WPF bi dodala nerelevantne migracione radove. Modularne proširenja se preporučuju.

## 1. Nezavisna tastatura makro pad
Skladištenje veza po identitetu uređaja plus fizički skeniranje kod i proširene ključne zname. Dodajte eksplicitne posmatranje i ekskluzivne režime, otkrivanje uređaja, ključni snimak, ponavljanje suzbijanja, gestove, drže slojeve i ponovo povezuje dijagnoze. Sačuvajte glavne / numpadne Enter razlike i testirajte nemačke / američke rasporede. Veze ciljaju izabrani fizički tastatura; dvosmislene identitete zahtevaju ponovno odabir umesto kontrole drugog uređaja.

Raw Input Ekskluzivno uklanjanje po uređaju zahteva stvarnu tastaturu filtera; nikada se ne korelira sa globalnim niskim nivoima Raw Input i tvrdi pouzdano uređaj-specifično uklanjanje. pružaju nadzor, hitne oslobađanja, neusvojeno ponašanje i instalator / neinstalator. Ne uhvatiti sigurne desktop ulaze..

Vozač vrata: izaberite podržani, pravilno licenciran i potpisan pozadinu koji je kompatibilan sa ciljnim konfiguracijama Windows 11 ili razvijte/podpišite posvećen filter. Intercepcija je kandidat za procenu, a ne posvećena zavisnost proizvodnje: njegov zvanični README opisuje testiranje kroz Windows 10 i različite komercijalne licence. Dok se ovaj vrata ne ispune, ekskluzivni režim mora biti prikazan kao nedostupan; funkcija se ne može proglasiti potpunim.

Prihvatanje: sa dva fizička tastatura, isti ključ izvršava makro i ne proizvodi normalan aplikacijski ulaz samo na izabranom ekskluzivnom uređaju; drugi ulaz tastature ostaje nepovređen. Proverite otključavanje, nesreću, otkazivanje, suspenziju/ukidanje i uklanjanje vozača na stvarnom hardveru.

## 2. Upravljanje radnom prostorom
Dodajte čarobnjak za radno mesto, sastanak, striming, istraživanje i prelazak projekta. Otkrijte instalirane aplikacije, audio uređaje, monitore i OBS scene. Korisnici biraju ciljeve i veze; predvide rezultate koraka i validiraju nedostajuće zahteve. Sačuvajte samo nakon pregleda, bez akcija koje rade tokom podešavanja. Profili koji se generišu ostaju uređeni u postojećim uređajima.

Prihvatanje: kreiranje radnog profila bez ručnog uređivanja izvršnih puteva ili identifikatora uređaja; otklanjanje ostavlja podešavanja nepromenjene; nedostajuće softver ima akcionirajuće objašnjenje.

## 3. Provereno izvršenje
Unesite preflash provere, tipovane rezultate po koraku i opcionalne uslove. Razlikuje zahtevane, završene, proverene, neuspješne, odložene, otkazane i neproverene stanja. Proverite audio/mikrofon, sjajnost, spremnost procesa/prozora i OBS stanje sa ograničenim asinhronnim čitanjima. Kratki / tekstualna isporuka nije dokaz da je ciljna aplikacija prihvatila komandu. Spoljni neželjeni efekti nisu univerzalno reverzibilni; nudi kompenzaciju samo kada je sprovedena.

Prihvatanje: isključeni eksplicitni ciljevi nikada ne padaju tiho; pogrešno posmatrano stanje nikada ne prijavljuje verifikovani uspeh; anulacija zaustavlja istraživanje; neuspjeh identifikuje korak i akciju oporavka. Istorija izvršenja propušta tekstove i tajne.

## 4. Plugini i delimični paketi
Verzionisani JSON manifestuje akcije, parametarske sheme, državne provajdere, zavisnosti i mogućnosti. Izvodite izvršne plagine u odvojenom procesu preko ograničenog lokalnog IPC-a sa ručnim prekidom, rokovima i otklanjanjem. Odbijanje pogrešnih poruka i nekompatibilne verzije; upravljanje nesrećama bez prekida kontrolera. Deklaracije o sposobnostima su otkrivanje i posredništvo, a ne zahtev za OS pesak.

Obezbedite pregled pakovanja / uvoza, haševe sadržaja, zaštitu putovanja, ograničenja veličine, provere verzije / zavisnosti, eksplicitno omogućavanje i ažuriranje. Importirani izvršni plagini ne bi trebalo da se automatski izvršavaju. Uključite radni primer plugin sa kraj-na-kraj instalacija / akcija / država / deinstalacija toka. Nije potrebna javna usluga tržišta za ovo izdavanje.

Prihvatanje: kreiranje, izvoz, instalacija i izvršenje stvarne integracije; demonstriraju vreme, izolaciju nesreće, neuspeh zavisnosti, prelazno odbacivanje i invalid uvoza.

## 5 Windows UI automatizacija
Dodajte picker elementa i ograničene akcije za pozivanje, vrednost, selekciju, fokus i čekanje elementa / države. Postoje selektori koji koriste proces, identifikator automatizacije, tip kontrole i kontekst predstava; izbegavajte koordinaciju samo snimanja. Pokazati selektor utakmice i testiranje selektora samo čitanje. Polje lozinke su isključene iz registracije. Upravljajte nepodržljivim kontrolima i ograničenjima nivoa integriteta Windows izričito.

Proširite snimanje sa pregledanim UIA akcijama, zadržavajući trenutnu snimanje. Nedvosmisleni selektori ne uspevaju umesto da kliknu na proizvoljnu kontrolu. Uobičajeno pretraživačko drvo nije obećanje za kompletan DOM automatizaciju.

Prihvatanje: automatizirajte posvećen test aplikaciju sa stabilnim selektorima kroz promene položaja / veličine; test nedvosmislenosti, nedostajuće elemente, onemogućene kontrole, vremenske izlaze i otkazivanje.

## 6. Lokalni telefon / tablet
Posluži odgovorni panel iz usluge LAN-a. Izričito koristite kratkotrajni jednokratni kod i izdajte odbijene identifikacije zaštićene lokalno. Koristite HTTPS sa dokumentiranim lokalnim sertifikatom poverenja / pinting; nema povratne informacije o isporuci u žalbenom tekstu. Definisanje i ograničavanje konfigurisanih interfejsa. Validacija porekla, autentifikovani komande, ograničenja stope i ograničene poruke su obavezne.

Izlaganje dozvoljenih profila / akcija, živog stanja, napretka i otkazivanja; koristite stabilne ID-a i isti izvod kao i desktop i fizičke ključeve. Ne izlagajte proizvoljne komande, izvršne puteve, tajne ili instalaciju plugina. Obezbedite povezivanje, parenje i povlačenje ekrana. Nije uključen oblačni račun ili internet relej.

Prihvatanje: radi sa realnog telefona / tableta na istom LAN-u; nepairani klijenti ne mogu da liste ili pokrenu akcije; otklonjeni tokeni ne uspevaju; duplirani / reprodukovani komande ne izvršavaju dvostruko; dekonnekti ne tiho povlače akcije.

## Proizvođač tastature

| Adapter / porodica | Planirana integracija | Pojasni granice |
| --- | --- | --- |
| Corsair | Sačuvajte struju iCUE G ključ ulazak i RGB integracija | Model/softverska sposobnost otkrivanja ostaje potrebna |
| Logitech G | G-key SDK adapter sa štampom/izlazakom i prodajnim vremenom; osvetljenje kroz svoje odvojeno SDK gde se podržava | Ne uravnotežujte D-HUB detekciju sa podrškom G-Ključ API na svakom modelu |
| Razer | Hroma osvetljenje adapter i vodi Synapse-na-standardni ključ mapiranje, plus generalni HID ulaz | Hroma je API osvetljenja; ne označite ga kao makro ključni ulaz |
| SteelSeries | GameSense Adapter za stanje / osvetljenje plus generalni HID ulaz | GameSense događaj podnošenje nije uobičajeni vlasnički ključni API |
| Wooting | Analogni SDK adapter za podržane analogne ulaze, plus normalne HID ključeve | Kapaciteti i uslovi distribucije SDK moraju biti provereni po podržanom modelu |
| Keychron/QMK/VIA | Standardni HID ključevi i uputstva za mapiranje firmara; poznati raspored samo sa dokazama | VIA Konfiguracija podrška nije univerzalna makro-che API |
| Drugi standardni HID tastatura | Fizičko otkrivanje i mapiranje po uređaju; ekskluzivni režim samo sa validisanim filterom | Nema proizvođenog modela-specifičnog FN, dijala ili vlasničkog ključa podrške |

Svaki uređaj prikazuje otkriven, posmatrani, mapiran kroz softver prodavca, indijanski SDK, vozač-ekskluzivni, nedostupni, ili hardver-neprobani status kao što je primenljivo. Sačuvajte dokaze kapaciteta sa adapter verzijama i testiranim identifikatorima modela / firmare.

## Ured isporuke
1. Konfiguracija kompatibilnosti, ugovora o mogućnostima i provere izvršenja.
2. Vođena podešavanja koristeći stvarne otkriće i postojeće izvršioce akcije.
3. UI Automatizacija i njegova rekordna/test fiktura.
4. Plugin hosting, pakovanje i integracija primera.
5. Plaćeni LAN panel i mobilna verifikacija.
6. Prošireni adapteri prodavca i ekskluzivna integracija tastature, sa stvarnom hardverskom validacijom.

Sve šest je potrebno. Vozač potpisivanje/licenzije, nedostupne SDK ili odsutna testiraju hardver su vrata za oslobađanje, a ne razlog za zamenu ponašanja mock i poziva rad završen.

## Kriterijumi verifikacije i oslobađanja
Izvodite postojeće jezgra, Windows i WPF proverite i dodajte fokusirane testove integracije za svaku novu granicu. Proverite JSON kružne putovanja i profile nasleđa; testirajte uvozeće izvršne sadržaje, mrežnu autentifikaciju, anulaciju, greške u plaginu i ponovo povezivanje uređaja. Ispitivanje desktop i mobilne rasporede. Validirajte visok Windows skaliranje odvojeno. Izgradite izdavanje samo nakon prolaska provera; razlikujte implementirane/testirane, implementisane/hardvarne neistražene i spolja blokirane funkcionalnosti u izveštajima o izdavanju. Nemojte zameniti instaliranu aplikaciju, instalirati vozače, menjati stvarne tastature ili otvoreni pristup mreži kao incident.

## Dokaz
- Microsoft Raw Input Semantike: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G programer SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Hroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Repozitorijum intercepcije i uslovi zavisnosti: https://github.com/oblitum/Interception
- Postojeći skladištenje proizvođača: docs/de-DE/istraživanje/tastaturherstener-soderfunkcii.md

## Pregled odluke
Korisnik je zamoljen da odobri ovaj pisani dizajn pre plana implementacije. Glavna spoljna zavisnost je proizvodnja-pripremljen ekskluzivni ulazni vozač. Preporučeni opseg zadržava ovo kao vrata isporučujuće umesto da ga tiho smanjuje na Raw Input posmatranja.

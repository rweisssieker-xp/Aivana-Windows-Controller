<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokalni asistent za II - Prototip

Stani: 12. oktobar 2026. ForgeMind hipoteza „Outcome Agent“, ograničen tok razgovora. bez dokazane konkurencijske prednosti i bez izmerenog uštede vremena bez stvarnih pokušaja usporedbe.

## Korišćenje

1. Uputstva paleta → **II asistent**. Eksperiment je izvan standarda i vraća se u slučaju uvoza konfiguracije ili izvoza portala.
2. Aktiviranje lokalnih asistenata. Ollama Mora se odvojiti `127.0.0.1:11434` radi; odgovarajući lokalni model mora biti već instaliran. **Lokalni modeli** Izaberite model. MAKRIVO ne instaliraju modele/usluge, ne kontaktiraju API / oblak i ne koriste API ključeve. Ollama-Usluge su u korisniku; deklarisani udaljeni / oblačni modeli se odbacuju.
3. Unesite cilj i dodajte programe / dokumente / veb stranice. Prenose se cilj, ID resursa, ime i tip; ciljni put i sadržaj izabranih datoteka se ne prenose dodatno; ciljni unos se bukvalno prenosi. Nazivi sajta sadrže samo šemu/host, ceo link ostaje lokalan.
4. **KI dizajn**. Maksimalno 90 Sekunde, 1024 Izlazni bokovi, ograničena veličina odgovora. Promene cilja / modela / resursa odbacuju pregled; prekidanje / zatvaranje zahteva. Model može da predloži samo izabrane ID-a. Nepoznati ID, slobodni komande, dodatne JSON polje i vrste izvođenih dokumenata su odbijeni.
5. Pregledajte i proverite otvorene tačke → **U uređaju **. Rezultat je prvo nezapaženi projekat. Sačuvajte samo u postojećem uređaju i izričito izvršite ako je potrebno. Normalna makrovalidacija, pre-/pregled, pauza i prekid ostaju odgovorni.

Podržana: pokretanje programa bez argumenata, otvaranje izričito izabranih lokalnih PDF/DOCKS/PPTKS/MD, otvajanje HTTP(S) sajtova, isključivanje mikrofona po izričitom želji. Nema standardne promene u slušalice, nema poruka, datuma, preuzimanja, skripta, slobodnih klikova ili tekstualnih podataka. Neizobrazive želje se pojavljuju kao otvorene tačke, pod uslovom da ih model pravilno prepozna; profesionalni ciljevi moraju biti ispitani od strane korisnika. Dokument nije automatski proveren.

## Poređenje eksperimenta

**Manual uređaj merenje · Početak / kraj** merenje punog ručnog poređenja. Asistentni prijem meri vreme od otvaranja asistenta do prvog skladištenja u uređaju; `generated`, međutim, sadrži samo trajanje planiranja. Nemojte uporediti dve neravnomerne trajanja kao štednju vremena.

Lokalni događaji se nalaze u rasporedu konfiguracije pod `Experiments/meeting-experiment.jsonl`: vremenski znak, slučajni ID pokušaja/pouzdanja, događaj, trajanje, korekcija da/ne. Nema ciljevih tekstova, imena resursa, puteva, odgovora na modele ili sadržaja dokumenta. Datoteka se okreće od 2 MiB, prethodni datoteka ostaje. Nemoguće je prekinuti makros i konfiguraciju.

Uključuje: ručnu bazinu, generisanje modela/pogrešnost/ukidanje, prevalentne promene/zatvaranje, prvu korekciju u uređaju i stvarne stanje za pokretač za skladištenje. `run_completed` samo znači da je pokretač završen; potvrđeni ciljni efekti su odvojeni u testu. Ponavljanja istog makrosa nisu novi nezavisni eksperimenti. Prvobitni eksperiment se može nastaviti nakon ponovnog pokretanja, jer ID pokušaja ostaje na skladištenom kocku. Dvostruke kocke dele ovaj poreklo; kada se procenjuje po ispitnoj identifikaciji.

Prvi eksperiment: 20 slične uređaje za sastanak ili dve nedelje sa dobrovoljnim korisnicima; prvo unutrašnje korišćenje. Ručno upoređivanje instalacije sa punim asistiranim instalacionim vremenom; pored toga, procenjuje korekciju / korekciju stope, greške trkača i stvarne rezultate ispitivanja. U slučaju nedostatka poboljšanja ili kršenja zaštitnih granica, deaktivirajte prekidač. Nijedna upotreba ili spremnost za plaćanje nije dokazana u registru. Nema automatskih telemetrijskih prenosa ili prilagođavanja cena.

## Ispitivanje

- Ključni: ograničenja, tipovi, nepoznati/dvostruki ID, isključivanje skripta, dugi linkovi, opcionalna konfiguracija, uvoza/eksport-ott-in, merenje za štednju podataka, kao i jedinstveno odbacivanje i ispravno prenošenje.
- Transport sa falsifikovanim modelima: Ollama JSON shema, loopback cilj, nema faze datoteke u promputu, oblačno ime i udaljene odgovore odbijena, oštećena / dugotrajna odgovora odbijeni.
- WPF-Smake: Dijalog reduciran; Dizajn prenos ne menja sačuvane kutije, pregled/shvatanje dobija poreklo i prikuplja korekciju, odbacuje ništa.
- Nezavisni pregled: odbaciti i dugi linkovi pronađeni i ispravljen; Regresion testovi RED→ZELEN.
- Izgradnja izdanja sa nule upozorenja / grešaka, ceo Core Suit i WPF Smuke uspešno. Potpuna soba Windows prvobitno je bila uključena u promenu fokusa. Ponovo je neuspeh ponovo na odbijenom prelazu; test je kontekstualni i ostaje nepromenjen. Ostale testove Windows uključujući i prevoz modela su izričito propuštene testove za uspeh.
- Ispitivanje paketa otkrilo je postojeću grešku u atomskoj razmenu kratkotrajnih konfiguracionih datoteka. Jedan prekidač je reprodukovao istu grešku pristupa; ograničeno ponavljanje samo nuklearne razmene je CERED→ZELEN. Trajne greške u pristupu ostaju vidljive.
- Trenutni samostalni paket `artifacts/release-20261012-184105`: WPF uspješno ispravljanje memorije, uključujući II dijalog i prihvatanje/odbijanje dizajna.
- Nema stvarnog testa: lokalna usluga Ollama nije odgovorila na ispit. Nema izjava o kvalitetu modela, latenciji, semantičkoj potpunosti ili štednji vremena. Ograničenja hardvera / vozača / skaliranja prethodne izdanja ostaju.

Izvori protokola: [Ollama Chat](https://docs.ollama.com/api/chat), [Strukturni izlaz](https://docs.ollama.com/capabilities/structured-outputs), [Oficijalne definicije tipa, uključujući i datoteke udaljenog modela](https://github.com/ollama/ollama/blob/main/api/types.go).

<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokalni AI savjetnik – Prototyp

Stanište: 12. listopad 2026. ForgeMind hipoteza "Outcome Agent", ograničen proces razgovora. bez dokazane konkurentne prednosti i bez mjerenog uštede vremena bez stvarnih pokušaja usporedbe.

## Korištenje

1. Zapovijedna paleta → **AI savjetnik**. Eksperiment je standardni i vraća se u slučaju uvoza konfiguracije ili izvoza.
2. Aktivirajte lokalne asistente. Ollama Mora se odvojiti `127.0.0.1:11434` rad; odgovarajući lokalni model mora biti već instaliran. **Lokalni modeli** Odabir modela. MAKRIVO Ne instalira modele/usluge, ne kontaktira OpenAI/Cloud API i ne koristi API ključeve. Ollama-Služba je kod korisnika; prijavljeni modeli za daljinu/oblak se odbijaju.
3. Upišite cilj i jasno dodajte programe / dokumente / web stranice. Cilj, ID resursa, naziv i tip se šalju; Ciljni put i sadržaj odabranih datoteka se ne šalju dodatno; Ciljno upućivanje je doslovno. Nazivi web-mjesta sadrže samo raspored/host, a cjeloviti link ostaje lokalni.
4. **Izrada AI-a**. Maksimalno 90 Sekunde, 1024 Izlazne kutije, ograničena veličina odgovora. izmjene cilja/modela/izvoda odbijaju predviđanje; otkazivanje/zaustavljanje zahtjeva. Model može predložiti samo odabrane ID-ove. Odbijeni su nepoznati ID-ovi, slobodni zapovijedi, dodatna JSON polja i izvedeno tipa dokumenata.
5. Pregledajte i provjerite otvorene točke → **U uredniku**. U početku, rezultat je nesigurni projekt. Samo u postojećem uredniku sačuvati i, ako je potrebno, izričito izvršiti. Normalna makrovalidacija, pre-pregled, pauza i prekid ostaju odgovorni.

Podržano: pokrenuti programe bez argumenata, otvoriti izričito odabrane lokalne PDF/DOCX/XLSX/PPTX/MD, otvoriti HTTP(S) web stranice, isključiti mikrofon na izričitu želju. Nema standardnih promjena slušalica, bez poruka, datuma, preuzimanja, skriptova, slobodnih klikova ili tekstova. Neizražavajuće želje pojavljuju se kao otvorene točke, pod uvjetom da ih model ispravno prepoznaje; profesionalno ispunjavanje ciljeva mora provjeriti korisnik. Otvaranje dokumenata nije automatski provjereno.

## Usporedni eksperiment

**Razmjeravanje uređaja · Početak / kraj** mjeri punu ručnu usporedbu. Pomoćni prijemovi mjere vrijeme od otvaranja asistenta do prvog skladištenja u uredniku; `generated` sadrži samo trajanje planiranja. Ne usporedite dvije nejednakosti kao štednju vremena.

Lokalni događaji nalaze se u redoslijedu konfiguracije pod `Experiments/meeting-experiment.jsonl`: vremenski znak, slučajni pokušaj/kabel ID, događaj, trajanje, ispravak da/ne. Nema ciljnih tekstova, imena resursa, staza, odgovora na modele ili sadržaja dokumenata. Knjiga se vrti od 2 MiB, prethodni datoteka ostaje. Ne smijemo prekinuti makro i konfiguracijsko skladištenje.

Sadrži: ručnu bazalinu, generaciju modela / pogrešku / otkazivanje, odbijene promjene / zatvaranje, prvobitno pohranjivanje i ispravke u uređaju, kao i stvarne završne uvjete za skladištenje ključa. `run_completed` samo znači da je Runner završen; potvrđeni ciljevi su odvojeno u tijeku ispitivanja. Ponavljanja istog makra nisu novi neovisni pokušaji. Početni eksperiment može se nastaviti nakon ponovnog pokretanja, jer ID pokušaja ostaje na pohranjenom kutiju. Duplicirani kućice dijele taj izvor; u procjeni po pokušaju ID skuplja.

Prvi eksperiment: 20 slična susreta ili dva tjedna s volonterskim korisnicima; prvo, unutarnje korištenje. Usporedite ručni vremenski uvjeti s punim pomoćnim vremenskim uvjetima; osim toga, procjenjujte korekciju/odbijanje, pogreške trkača i stvarne rezultate ispitivanja. U slučaju nedostatka poboljšanja ili kršenja zaštitnih granica isključite. Nije dokazano nikakvo korištenje ili spremnost za plaćanje u registru. Nema automatskog prijenosa ili prilagodbe cijena.

## Ispitivanje

- Core: ograničenja, tipovi, nepoznati/duplicirani ID-ovi, isključivanje skriptova, duge poveznice, opcionalna konfiguracija, uvoz/izvoz otključivanja, štednja podataka, kao i jedinstveno odbijanje i ispravan prijenos.
- Transport s lažnim modelom: Ollama JSON shema, Loopback cilj, nema prompt datoteke, ime u oblaku i udaljenosti odbijen, oštećena/preduž odgovor odbijen.
- WPF-Smoke: dijalog je smanjen; prenosi dizajna ne mijenja pohranjene kutije, recenzija/save dobiva izvor i prikuplja ispravak, odbacuje ništa.
- Neovisna pregleda: odbijanje i dug link pronađen i ispravljen; Regresion test RED→GREEN.
- Izgradnja izdanja s nula upozorenja/grešaka, cijeli Core Suite i WPF-Sljedeći Članak Smoke uspješno. Windows-Suite je u početku uključivala promjenu fokusiranja. novi tečaj ponovno je neuspješan na odbijenoj promjeni pozadine; test je kontekstalan i ostaje nepromijenjen. Windows-Ispitivanja, uključujući i prijevoz modela, provode se s izričito propisanim testom uspjeha.
- Provjera paketa otkrila je postojeću pogrešku u nuklearnoj razmjeni kratko otvorenih konfiguracijskih datoteka. Jedan blokator reprodukirao isti pristup-odbijen pogrešku; ograničeno ponavljanje samo nuklearne razmjene je RED→GREEN provjerena. Trajne pogreške u pristupu ostaju vidljive.
- Trenutačni samostalni paket `artifacts/release-20261012-184105`: WPF uspješan ispravak pamćenja, uključujući AI dijalog i prihvaćanje/odbijanje dizajna.
- Nema stvarnog testiranja modela: lokalna služba Ollama nije odgovorila na ispitivanje. Nema izjava o kvaliteti modela, latenciji, semantskoj potpuni ili uštedi vremena. Hardware/Driver/Skaliranje granice prethodnog izdanja ostaju.

Izvori za protokole: [Ollama Chat](https://docs.ollama.com/api/chat), [Struktura izlaska](https://docs.ollama.com/capabilities/structured-outputs), [Službene definicije tipa, uključujući podatke o daljinskim modelima](https://github.com/ollama/ollama/blob/main/api/types.go).

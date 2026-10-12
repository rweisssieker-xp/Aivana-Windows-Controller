<!-- generated-doc locale=sr-Latn source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../HANDBOOK.md)

# MAKRIVO — Korisničko uputstvo

Proizvođač: **Aivana**. Dokumentacija Snapshot: **2026-10-12**. Ova ručka opisuje trenutnu implementaciju radnog drveta. Eksperimentalne integracije i hardverske funkcije su izričito identifikovane. Istorijski izveštaji opisuju svoj datum, a ne garanciju za novije građevine.

## Dokumentacija i jezici

Koristite selektor jezika dokumentacije da biste otvorili kompletnu ručku, inventar funkcija, rešavanje problema, izveštaje o razvoju, istraživanje i istorijske testove na bilo kom od 27 aplikacionih jezika. Generisani prevodi su mašinski asistirani i imaju strukturne provere; nisu svi dobili pregled rodnog govornika. Tehnički identifikatori i izvršni primeri ostaju nepromenjeni. Originalni izvor ostaje dostupan za poređenje. Prevedena licencijska analiza je informativna i ne zamenjuje originalne pravne uslove treće strane ili konačnu EULA.

## Instalacija i prvi lansiranje

Prvobitni cilj je Windows 11 x64. Držite svaki datoteku i poddirektorijum distribucije zajedno. Pokretanje `MAKRIVO.exe` Izvor čeckaut nije instalator. Samokontentovani paketi uključuju svoj .NET radni vremenski period; paketi koji zavise od okvira zahtevaju odgovarajuće radni vremenske periode .NET 10 i ASP.NET. ONNX i hosteri saputnika moraju biti uključeni za njihove relevantne funkcije.

1. Pokrenite MAKRIVO i izaberite jezik interfejsa u podešavanjama. Automatski sledeći Windows Klaviranje je odvojeno podešavanje.
2. Izaberite profil, a zatim dodajte talas kroz katalog za pretragu. Dajte joj prepoznatljivo ime i izaberite njegove akcije.
3. Proverite korake i unesite kratak korak ako je potrebno. Izbegavajte kombinacije koje su već korišćene od strane Windows ili vaše ciljne aplikacije.
4. Sačuvaj ovaj projekt. Nespasene promene nisu spasena konfiguracija.
5. Testirajte bezvrednim akcijama, kao što je otvaranje dokumenta, i proverite povratne informacije o izvršenju.

Zatvaranje glavnog prozora obično ostavlja aplikaciju u području obaveštenja. Dvostruko kliknite na ikonu trajanja da biste je ponovo otvorili, ili koristite meni trajanja za izlazak. Počinje sa Windows je opcionalno i prvobitno invalidirano. Kontrola pauze suspenduje veze; ikona pokazuje znak pauze. Otkazivanje makrosa kroz UI, trai ili konfigurisani otkaz kratak; podrazumevani je `Ctrl+Alt+Escape`.

## Tile, akcije i komandna paleta

Tilovi pripadaju profilima i mogu da sadrže više naručenih koraka. Redaktor podržava sačuvanje / odbacivanje, pregled, undo /redo i korak reorganizacije. Katalog pruža presete za programe, dokumente, URL-a, tekst, klipbord, skraćenice, medije, audio, prozorke, sistemske akcije i podržane integracije. Preseta koja zahteva parametre otvara se kao nacrt.

Otvorite paletu sa `Ctrl+K` dok MAKRIVO ima fokus. Pretraživanje spasenih akcija, preseta, pretvaranja profila i alata. Izaberite ključeve; Unesite aktivira; `Ctrl+Enter` uređuje podržani element; Evakuacija se zatvara. Neke spremne komande izvršavaju odmah, dok konfigurisane akcije zahtevaju pregled. Odvojena globalna paleta može se konfigurisati; nove konfiguracije koriste `Ctrl+Shift+Space`.

Tekst i klipbord koraka podrška `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Datum formatiranja je `yyyy-MM-dd`; Vreme za formatiranje je `HH:mm`. Nepoznate promenljive ostaju nepromenjene. čitanje klipova se javlja samo kada je potrebno. akcija klipova piše tekst bez automatskog uklanjanja.

Akcije pokrenute iz II-a mogu biti usmerene na poslednju aplikaciju. Ako cilj nestane ili Windows blokira avangardno aktiviranje, ručno aktivirati cilj i povlačiti. Uspešna isporuka ključa ne dokazuje da je cilj završio svoj zadatak.

## Profili i podešavanje radnog mesta

Profili mogu odgovarati ime procesa i deo naslova prozora. Kada su obe postavljene, oba moraju da se takmiče. Profilni poredak rešava takmičenje; defolt profil je padanje. Omogućava se automatski izbor. Lock Profile sprečava automatske promene za trenutnu sesiju.

Otvoreno radno mesto iz alata ili palete. Izaberite programe, dokumente, veb-sajtove, audio / ekran cilj ili OBS scenu. Asistent priprema radno mesto, sastanak, striming, istraživanje ili profile projekta. Proverite generisane korake pre nego što uštedite. Ustav ne izvršava pripremljene akcije.

## Makros, uslove i snimanje

Makros izvršava svoje korake u redu i podržava čekanja, ponavljanja i anulacije. Opšta granica ponavljanja je 100. Koristite Pregled pre pokretanja akcija koje unose u drugu aplikaciju ili menjaju sistemski status.

Čekajte za Calcet Vindovs prihvata proces, naslov substrat i vreme izlazak od 1 do 600,000 ms. Potreban je najmanje jedan filter. Pogodni vidljiv prozor mora da odgovori i postane predgrađen pre nego što se sledeći koraci nastavljaju. Pripremljeno prozor ne dokazuje da je dokument ili usluga završena.

Redaktor radnog toka dodaje uslove datoteke, procesa i naslova. Falsifikovani uslov prolazi kroz korak i može propustiti dodatne korake napred. Odbijanje uslova za izražavanje alternativne grane. Izaberite stop-on-error ili continu-on-err namerno; nastavak može ostaviti kasnije korake bez očekivanih uslova.

Action rekorder uhvati kratke kodove i čeka, a ne obično štampanje. Podržava promenljive skraćenice i funkcione ključeve. Bežanje prestaje da snima. Pregledajte listu i usvojite je kao nezaštićeni dizajn, a zatim izričito sačuvate. UI klik snimanje je poseban alat opisan ispod.

## Ključne tabele, gestovi i slojevi

Tastatura liste otkrivenih interfejsa i referentnih rasporeda. Jedna fizička tastatura može izložiti nekoliko interfejsa. Izaberite pravi jezik i geometriju; samo ime proizvoda ne identifikuje ISO ili ANSI raspored.

Standardni globalni hotkei se primenjuju preko tastatura. Raw Input veze posmatraju određeni uređaj i skeniranje koda, uključujući funkcije podržane i medijske ključeve. **Raw Input Ne potiskuje obično Windows Ekskluzivna druga tastatura nije spremna za proizvodnju.** Potpisan, licenciran i testiran filter-driver je još uvek potreban; niko nije instaliran MAKRIVO.

Podržani specijalni ključ veze mogu koristiti odmah pritisak, tapanje, drženje ili dvostruko tapanje. Held-klajer ključevi privremeno izabrati još jedan mapiranje i vratiti osnovni sloj na oslobađanju; sloj-izbornik ključeve ne rade svoje makro. 150 da 5,000 MS; dvostruki prozor od 150 da 1,000 MS. Ovi mapi su različiti od običnih globalnih hotkeisa.

Specijalni ključ čarobnjak otkriva kompatibilne ključeve bez izvršenja njihovih dodeljenih akcija tokom uhvati. Dijagnoza objašnjava nedostupne uređaje, prekinute veze, neispravnosti profila i konflikte. Preopterećenje pokazuje izabrani raspored i veze; njegov podrazumevani kratak je `Ctrl+Alt+F1`.

| Integracija | Potrebe i ograničenja |
| --- | --- |
| Corsair | Radeći iCUE, SDK pristup i kompatibilan model. G1-G6 i podržane RGB funkcije; vlasnički Fn, točkovi, zaključak i kontrole profila obično nisu izloženi G-che API. |
| Logitech G | Originalne lokalne KSNUMKS-Ključ / LED SDK biblioteke zahtevaju izričito odobrenje. G-key događaji ne mogu jedinstveno da identifikuju nekoliko identičnih fizičkih tipkovnica. |
| Razer | Hroma RGB i standardni ulaz; mapa posebne kontrole u Synapse gde je potrebno. |
| SteelSeries | GameSense RGB i standardni ulaz; mapa vlasnički kontrole u GG gde je potrebno. |
| Wooting | Kompatibilan originalni x64 SDK za analogne vrednosti i pragove; normalni HID ulaz ostaje aktivan. |
| Drugi tastatura | Standardni hotkei ili podržani fizički skeniranje kodova. Fn i kontrole za vlasništvo zavise od modela i konfiguracije prodavca. |

Istraživanje proizvođača opisuje dokumentisane sposobnosti, ne sprovedene ili testirane kompatibilnosti sa svakim navedenim uređajima. Novi adapteri i dalje trebaju stvarnu hardversku prihvatanje. Nemojte preuzeti SDK binarne iz nesigurnih izvora; SDK distribucija prava su odvojena od tehničke kompatibilnosti.

## Audio, ekrani i OBS

Audio koraci postavljaju volumen od 0 do 100 procenata i mute/neumute/togle podržava izlaz ili mikrofon krajnje tačke. Izaberite određenu tačku ili koristite defolt Windows. Nedostatak spasenog cilja proizvodi grešku umesto da tiho kontroliše drugo uređaj. Ponovo osvetlite uređaje nakon povratka. Kontrola krajnjeg tačaka nije zahtev za nezavisnu podršku po aplikaciji.

Pokazati akcije povećanje / smanjenje svetlosti sa 10 procentnih tačaka ili postavljanje vrednosti od 0 do 100. Unutrašnji ekrani koriste podržane VMI interfejse; spoljni monitori zahtevaju podršku DDC/CI i obično njegov meni podešavanje. Dock, adapter i kabl mogu blokirati kontrolu. Izaberite sve podržane ekrane ili jedan monitor. Ponovo povezivanje može da promeni dostupne identitete; pregledanje spasenog cilja. Nedostatak čitanja pokazuje se kao nedostupna umesto merenih vrednosti.

OBS zahteva WebSocket v5 uključen u OBS alati. Postavite krajnju tačku, testirajte povezivanje i nanosite imena / ulaza pre nego što sačuvate. Podržane akcije uključuju prebacivanje scene, pokretanje / snimanje / zaustavljanje / snimanje, snimanje pauze / rezime i ulazak mute. Paroli se mogu čuvati u Windows Kreditni menadžer i isključuju se iz konfiguracije izvoza. Uspešna komanda OBS ne utvrđuje svaki efekt ispod struje.

## Rezultat verifikacije i istorija izvršenja

Moguće je proveriti rezultat za odgovarajuće korake. Aplikacija razlikuje preflage, potvrđene efekte, neuspjehe, anulacije i efekte koji se ne mogu proveriti. Proces postojanja nije spremnost za primenu. Relativne promene, skraćenja i tekst ulazak ne mogu dobiti tvrdnju o uspehu. Inspektirajte istoriju aktivnosti i verifikacije kada akcija ne uspe. Pogrešnosti konfiguracije treba ispraviti pre nego što se ukloni kompletni makro.

## UI automatizacija i klik snimanje

Dodajte korak za automatizaciju interneta, izaberite operaciju i uhvatite cilj ispod indikatora nakon što se pojavljuje kašnjenje. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` i `WaitState`. Polje lozinke, onemogućeni kontroli i nedvosmisleni selektori su odbijeni.

UI automatizacija radi u ograničenom procesu. To zavisi od podrške i dozvola za pristupačnost ciljne aplikacije Windows. Klik snimač uhvati podržane akcije i pauze; on ne zapisuje opštu tekstualnu unos. Pregledajte i validirajte svoj projekat pre nego što ga usvojite. Promene u internetu u spoljnoj aplikaciji mogu nemoguće. Proces izolacije ograničava nesreće i anulaciju, ali nije bezbednosna peska.

## Plugine

Otvorite proširenja da biste proverili paket pre nego što ga uvozite. Struktura paketa, manifest i haševi su provereni. Uvoz je počeo sa invaliditetom. Pročitajte deklarisane akcije i izričito poverite izdavaču pre nego što omogućite. Plugini rade sa vašim korisničkim dozvolima u odvojenom procesu; taj proces je **a ne pesak**. Samo instalirajte pakete koje verujete.

Menadžer podržava izvoz, deaktiviranje, deinstalaciju i obnovu / otklanjanje prethodne verzije. Nedostatak zavisnosti blokira poziv. Primer paketa obezbeđuje računanje i lokalno-državne akcije. Podržana shema je dokumentirana podseta JSON-Shema, a ne svaka moguća funkcija JSON. Ne postoji javni Plugin tržište.

## Mobilni panel

1. Otvorite mobilni panel, izaberite lokalni IP i izričito izaberite profile koje ćete izložiti. Po pravilu, obratna adresa je dostupna samo na samom PC-u; izaberite odgovarajuću LAN adresu za telefon ili tablet.
2. Pokrenite panel i pročitajte njegovu HTTPS adresu, sertifikat prstiju i jednokratni šestocifreni parovi kod.
3. Na uređaju u istoj poverenoj mreži, otvorite adresu. Poređenje prstiju SHA-256 kroz pouzdani kanal pre nego što poverite lokalnom sertifikatu. Aplikacija ne instalira pouzdani sertifikat automatski.
4. Koristite prikazan kod i testirajte jednu bezvrednu taliju. Ručna dozvola Windows može biti potrebna za LAN pristup. Nemojte izložiti port na internet.
5. Zaustavite panel kada je završen, ili povucite plaćene klijente kroz aplikaciju.

Privatni serverski ključevi su zaštićeni sa Windows DPAPI; Klientske identifikacije se čuvaju kao haševi. Promena IP ili obnavljanje sertifikata može zahtevati obnovu poverenja. Panel prikazuje stavove akcije i verifikacije, a ne sve moguće žive senzore. Pretraživač raspored i lokalni HTTPS proveri ne zamenjuju stvarnu telefon / tablet LAN prihvatanje.

## Pomoćnik lokalnog sastanaka

Ovaj opcionalni eksperimentalni asistent je deaktiviran po podrazumevanju. Otvorite ga iz Alat, eksplicitno ga aktivirajte, izaberite radno vreme / model i dodajte samo resurse koje želite da razmotrite. Stavite cilj, kreirajte nacrt, inspektirajte nerešene zahteve i prenesite rezultat u normalan urednik. Ništa se ne izvršava automatski; spasavanje i pokretanje ostaju eksplicitne akcije. Nema automatskog pada vremena.

| Rantime | Uspostavljanje |
| --- | --- |
| Ollama | Odvojena lokalna usluga i lokalno instaliran model na `127.0.0.1:11434`. Oglašeni udaljeni / oblačni modeli su odbijeni. |
| LM Studio | Lokalni server sa lokalnim modelama i podrškom za JSON shemu na `127.0.0.1:1234`. Ne dozvolite daljinsko rutiranje za lokalnu obradu. |
| llama.cpp | Lokalni `llama-server` i kompatibilan model sa podrškom za Shemu JSON na `127.0.0.1:8080`. |
| ONNX Ranjeno vreme GenAI CPU | Pouzdani kompatibilni lokalni GenAI tekstualni model fascikla; izaberite `genai_config.json`. Inferencija koristi kombinovani proces kompanije bez HTTP. |

HTTP klijenti koriste samo loupback, ne koriste proksije i ne prate preusmeravanja. MAKRIVO ne može pouzdano utvrditi da li kompatibilan lokalni server unutra poziva na udaljenu uslugu. Autentirani lokalni serveri nisu konfigurisani u ovoj verziji. Model težine, Python i lokalni inferencijski serveri se ne preuzimaju ili instaliraju automatski.

ONNX pakovanja trebaju `tokenizer.json` i važeći šablon za razgovor. Podržani obrasci šablona u `tokenizer_config.json` su rečnik `chat_template`, rečnik sa rečnikom `default`, ili nedvosmislena lista imenovanih/defolt šablona. Ako je šablon nedostajao ili nula, `chat_template.jinja` se koristi kao padanje. Prazni, nedvosmisleni, prekomerni ili pogrešno označeni šabloni se odbacuju. Arbitražni ONNX datoteke, ugrađeni/vizijski modeli i samo GPU paketi nisu zamene sa kompatibilnim GenAI tekstualnim modelima. Ugrađena verzija GenAI je 0.17.1, CPU. Koristite samo pouzdane modele; proces odvajanja nije peska.

Pomoćnik podržava izabrane programe bez argumenata, izabrane dokumente i veb stranice, a mikrofon mut kada je izričito zatraženo. To ne stvara poruke, sastanak, proizvoljne skripte, besplatne klikove, preuzimanja ili prebacivanje slušalica. Cilj i imena resursa se šalju na izabrani lokalni radni vremenski period; resursi i sadržaj datoteke se ne isporučuju odvojeno. Cilj koji je korisnik sam po sebi može da sadrži lične podatke. Ponašanje lokalnog mrežnog ponašanja ostaje odgovornost korisnika.

U tom ONNX Zahtev budžeta je 90 2 sekunde, uključujući i model, do 3,072 Brzi token i 1,024 generisani tokeni, takođe ograničeni kontekstom modela. Zloformisani / prekinuti JSON i nepoznati ID se odbacuju. Otkazivanje ili blizu završetka zahteva. Slučajni model ispitivanja određuje izvršenje, ne korisno planiranje kvalitet ili vreme štednje. Lokalni eksperimentalni zapisi sadrže identifikatore događaja i rasporede, ne cilj tekstu ili sadržaj dokumenta; oni se ne automatski preuzimaju.

Kasnije male evaluacije Instrukt-modela proizvele su nule važeće planove u četiri nemačke / engleske ispitivanja. Narodna inferencija je radila, ali model je neuspeo u strogom planiranju. Ovo je negativan rezultat kvaliteta, a ne preporučen model ili dokazano koristan asistent. Pogledajte datum lokalnog testa za tačnu reviziju modela i granice.

## Import, izvoz, sinhronizacija i rezervna kopija

Pregledajte prognoze uvoza pre nego što zamenite konfiguraciju. Importirane akcije počinju da se deaktiviraju, a prenosiva sinhronizacija uklanja hardverske specifične veze. Ponovo rasporediti uređaje i omogućiti akcije namerno nakon inspekcije. Pregledajte sadržaj teksta / akcije pre nego što podelite izvoz; isključivanje lozinke ne čini sve sadržaje akcije koje korisnik stvara neosetljivim.

Profil sinhronizacija razmjenjuje datoteke u odabranom fasciklu, opcionalno jedan upravlja vaš postojeći klijent oblaka. MAKRIVO ne isporučuje oblačni nalog i ne čuva oblačne identifikacije. Revizije sprečavaju tihi prepisi. Uključivanje akcionog sadržaja je eksplicitan izbor. Povratak konfiguracije pre uvoza zamena. Standardna konfiguracija je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; istorijsko ime direktorije se čuva za kompatibilnost. Zatvorite aplikaciju pre nego što je ručno kopirate / vratite. Skladišteni SDK odobrenja i OS-zaštićene autentifikacije mogu biti potrebne za rekreaciju na drugom računaru.

## Problemi rešavanje

| Simptomi | Šta da proverim |
| --- | --- |
| Kratko ništa ne radi | Aplikacija radi, veze nisu prekinute, pravilno aktivni profil, sačuvan/omogućen talas, konflikti kratka i dostupni cilj. |
| Ciljni prozor ne može da aktivira | Ručno ga aktivirajte. Windows pregrade ograničenja i privilegijske razlike mogu blokirati automatizaciju. |
| Specijalni ključ nije otkriven | Podrška za prodavače / SDK / model; konfigurišete kontrole za izdavanje standardnog ključa, ako je moguće. |
| Druga tastatura takođe normalno | Očekuje se da će Raw Input Ekskluzivno suzbijanje se ne primenjuje za proizvodnju. |
| Audio ili svetlost nije dostupna | Obnoviti uređaje; inspektirati izabrane identitete, sesije krajnjeg tačaka, podršku VMI/DDC/CI i ograničenja dock/kabela. |
| OBS ofline | WebSocket v5 je uključen, ispravna krajnja tačka / lozinka, dostupna usluga i važeći nazivi scene / ulaza. |
| Neuspeh automatizacije | Pridruživanje domaćin uključen, jedinstveni važeći selektor, pristupačni omogućeni cilj, podržana kontrola i dozvole. Vratite prelazni vremenski period samo nakon pregleda cilja. |
| Mobilni panel nepristupačan | LAN umesto lopback adrese, iste mreže, server radi, odobreni profili, sertifikat prstiju / poverenje i ručne dozvole za vatre. |
| Plugin ne uspe | Integritet paketa, eksplicitno poverenje / omogućeno stanje, zavisnosti, kompatibilnost protokola i izvršenje dnevnika. |
| Zahtev ne uspeva | Pravilno odabrani lokalni radni vreme, servis / model spreman, podržana JSON shema, poverena kompatibilna fascikla i ograničenja zahteva. |
| Layout ili tekst izgleda pogrešno | Odvojiti jezik korisnike od geometrije tastature; preusmeriti prozor, proveriti skalu prikazivanja i izveštavati tačnu stranicu / jezik / izgradnju. |

Za korisni izveštaj o greškama uključuje verziju Windows, aplikaciju graditi/poručiti, jezik, skaliranje prikazivanja, hardverski model, minimalnu sekvenciju i relevantno uređeni dnevnik. Uklonite lične puteve, sadržaj akcije i poverenja pre objavljivanja.

## Izgradnja, testiranje i pakovanje

Koristite SDK u `global.json`. Od repozitorija korena:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows testovi integracije mogu otvoriti izolovane prozore i privremeno konfigurisati / oslobađati podrške provajdera. Ispostavite ih bez drugog kontrolera koji poseduje ove veze. Standardni testovi svetlosti/audio su samo čitanje; eksplicitni testovi hardvera su odvojeni. Mock protokoli, renderirani snimaci i slučajni grafik ne moraju biti predstavljeni kao stvarna hardverska / model kvaliteta prihvatanja.

Potpuni izdavač sakuplja hostere kompanije, mobilne imovine, primjer plugin, radno vreme ONNX i dostupne zavisnosti. Držite svoje poddirektorije. Uspešna izgradnja nije potpisana instalacija ili odobrenje za oslobađanje. Izvor i zgrade mogu se razvijati odvojeno; uporedite snimak dokumentacije i angažovati sa instaliranom verzijom.

## Licenzije, privatnost i status oslobađanja

Predviđeni komercijalni model je jednokratna kupovina sa trajnom upotrebom kupljene velike verzije; konačna cena, pribor i uslovi za ažuriranje moraju biti postavljeni u stvarnim uslovima prodaje. Sistem kupovine / aktivacije proizvodnje nije utvrđen ovim rukovodstvom. Ne postoji potreban oblačni račun za svakodnevni kontrolu.

Ne distribuirajte vlasničke SDK binarne bez dokazanog dozvola. Sačuvajte licence i obaveštenja treće strane za svaku isporučenu zavisnost, uključujući .NET i ONNX pakete. Model težine imaju svoje uslove. Datum licence revizije prenosi neke nove zavisnosti i nije klirancija za te dodavanja. Repozitorijum koji je javni ne daje opštu licencu otvorenog izvora.

Realni hardver prodavača, telefon / tablet LAN operacija, mešani monitor / DPI tranzicije, dugotrajna upotreba, kompletna lingvistička revizija, ekskluzivna podrška vozača i korisna kvalitet AI planiranja ostaju odvojeni rad prihvatanja. Nikada ne dobijam odobrenje samo od prisustva.

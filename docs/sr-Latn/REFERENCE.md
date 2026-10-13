<!-- generated-doc locale=sr-Latn source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../README.md)

# MAKRIVO

[Potpuna dokumentacija na svih 27 jezika](README.md) · [Potpun korisnički priručnik](HANDBOOK.md)

**Autor: Aivana.** [Trenutni pregled funkcija i ograničenja izdanja](FEATURES.md) · [Izveštaj o testiranju proširenja](testing/2026-10-12-six-extensions.md) · [Prototip lokalnog AI pomoćnika](testing/2026-10-12-local-meeting-assistant.md)

** Engleski (Sjedinjene Američke Države)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalni kontroler prečica Windows sa pločicama u stilu Stream Deck, profilima, makroima i otkrivanjem tastatura. Interfejs podržava 27 jezika. Za svakodnevnu upotrebu nisu potrebni nalog ni veza sa oblakom.

[Dokumentacija](README.md) · [Verifikacija](VERIFICATION.md) · [Izveštaj o testu](testing/2026-10-09-usp-release.md)

## Počeo je

Nakon pravljenja distribucije aplikacija je na `artifacts/win-x64/MAKRIVO.exe`. Celu izlaznu fasciklu držite zajedno; možete je kopirati drugde. Početna ciljna platforma je Windows 11 x64. Rezultati izgradnje izostavljeni su iz Git; kopija izvornog koda ne sadrži unapred izgrađenu izvršnu datoteku.

Instalirajte .NET 10 SDK navedeni u `global.json` da biste izgradili i pokrenuli iz izvora:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Objavite samokontentnu izgradnju bez potrebe za odvojeno instaliranim .NET radnom vremenom:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Osnovni radni tok

1. Izaberite profil i kreirajte akciju.
2. Podesite ime, ikonu, boju i prečicu na tastaturi. Kombinacije Ctrl/Alt/Shift smanjuju nenamerno aktiviranje tokom kucanja.
3. Dodajte jedan ili više koraka: aplikacije, datoteke, veb-sajtove, tekst, prečice, medije, jačinu zvuka, promenu profila ili odlaganja.
4. Sačuvajte. Promene ostaju nacrt dok se ne sačuvaju; odbacivanje vraća sačuvanu verziju. Dugme za pokretanje pločice i njena prečica koriste isti put izvršavanja.
5. Reorganizovati talije i makro korake sa dugmenima za trljanje i bacanje ili strelicama. Modeli pojednostavljaju podešavanje. Makros može ponoviti do 100 puta. Uklonite interfejs, sistemski meni, ili Ctrl+Alt+Echape po podrazumevanju.

Profili za određene aplikacije koriste ime procesa bez pune putanje. Redosled profila razrešava jednaka poklapanja; podrazumevani profil služi kao zamena. Automatski izbor profila mora da se omogući u podešavanjima.

Zatvaranje glavnog prozora zadržava aplikaciju u sistemu po podrazumevanju. Dvostruko kliknite na ikonu trajanja da biste je ponovo otvorili; koristite meni trajanja za izlazak. Početak sa Windows je opcionalan i deaktiviran po podrazumevanju. Hotkeys se može zaustaviti.

## Akcije i komandna paleta

Dodavanje akcije otvara pretraživački katalog sa 90 presecima u osam kategorija. Svako ima opis i predviđanje. Makro uređaj koristi isti katalog da dodaju ili zamenjuju korake. Izbor preseta u ovom radnom toku uređaja stvara projekat bez izvršenja.

Paleta komandi otvara se iz zaglavlja ili pomoću Ctrl+K unutar aplikacije. Pretražuje sačuvane radnje u svim profilima, šablone kataloga, promenu profila, navigaciju, pauzu/nastavak, otkazivanje makroa i uvoz/izvoz. Filtrirajte prema sačuvanim radnjama, funkcijama kataloga ili komandama aplikacije. Tasteri sa strelicama biraju, Enter aktivira, Ctrl+Enter uređuje stavku koja može da se uređuje, a Escape zatvara. Omiljene stavke se prikazuju prve. Spremne komande kataloga mogu da se pokrenu odmah; šabloni koji zahtevaju podešavanje otvaraju nacrt. U podešavanjima može da se odredi globalna prečica palete. Nove konfiguracije koriste Ctrl+Shift+Space; postojeće konfiguracije ne dobijaju novu globalnu prečicu automatski.

Zvučne radnje precizno postavljaju jačinu zvuka od 0 do 100 procenata i isključuju, uključuju ili menjaju stanje utišavanja izlaznih i mikrofonskih uređaja. Izaberite određenu krajnju tačku ili je ostavite nepodešenu za podrazumevanu u Windows. Nedostupna sačuvana krajnja tačka izaziva grešku umesto upravljanja drugim uređajem. Osvežavanje uređaja ponovo učitava povezane uređaje.

OBS Studio koristi WebSocket v5: pokretanje/zaustavljanje/prebacivanje/pauziranje/nastavak snimanja; pokretanje/zaustavljanje/prebacivanje striminga; promenu scena; prebacivanje utišavanja ulaza. Omogućite server WebSocket u alatima OBS i podesite njegovu krajnju tačku u uređivaču. Provera veze i učitavanje imena čitaju verziju OBS, scene i ulaze bez pokretanja snimanja ili striminga. Izaberite imena sa liste ili ih unesite ručno. Lozinke mogu da se sačuvaju u Windows Credential Manager i izostavljaju se iz izvezenih konfiguracija. Nedostupne veze OBS prijavljuju grešku.

Profili mogu da odgovaraju imenu procesa i delu naslova prozora. Oba navedena uslova moraju da se poklapaju. Zaključavanje profila sprečava automatsku promenu tokom sesije. Snimanje uslova iz prvog plana koristi poslednju spoljnu aplikaciju sa fokusom.

Radnje za tekst i ostavu podržavaju `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Datumi koriste `yyyy-MM-dd`; vremena koriste `HH:mm`. Nepoznate oznake mesta ostaju nepromenjene. `{{profile}}` označava profil pokrenute radnje; `{{process}}` označava kontekst spoljne aplikacije. Sadržaj ostave čita se samo kada se koristi ta oznaka mesta. Radnja ostave upisuje tekst, ali ga ne lepi automatski.

Radnje prozora minimizuju, maksimizuju, vraćaju ili raspoređuju prozor na levu/desnu stranu radne oblasti njegovog monitora. Sistemska radnja zaključava Windows. Te radnje se izvršavaju samo kada su izričito pozvane.

Pregled prikazuje korake, ponavljanja i promenljive bez izvršavanja. Poništavanje i ponavljanje čuvaju do 100 stanja nacrta; polja za tekst zadržavaju uobičajeno uređivanje pomoću Ctrl+Z. Označite omiljene stavke u uređivaču ili meniju pločice i filtrirajte ih na kontrolnoj tabli. Brojači izvršavanja važe samo za trenutnu sesiju.

Tekstovi, skraćeni i akcije prozora pokrenuti iz interfejsa aktiviraju poslednju ciljnu prozor. Palet prikazuje taj ciljni kontekst. Ako je cilj zatvoren ili Windows odbija aktivaciju, izvršenje se zaustavlja sa porukom.

## Pomoć tastature i povratne informacije

Čarobnjak za posebne tastere nalazi se na stranici tastature i u paleti. Navodi tastere G koje prijavljuje iCUE. Otkrivanje pritiska tastera privremeno ih snima bez izvršavanja dodeljenih radnji. Uobičajene dodele vraćaju se nakon otkrivanja, otkazivanja ili zatvaranja. Funkcija kataloga i izabrani taster otvaraju se kao nesačuvan nacrt; proveravaju se duplirane dodele.

Preklopni prikaz tastature pokazuje izabrani referentni raspored i aktivne dodele. Držite Ctrl+Alt+F1 ili ga uključite/isključite iz palete ili stranice tastature. Ne preuzima fokus i propušta klikove miša. Promene profila i dodela ažuriraju se dok je vidljiv; uobičajeni pritisci tastera Windows su istaknuti. Kontrole proizvođača možda ne šalju odgovarajući signal tastera Windows.

Povratne informacije o izvršavanju prikazuju čekanje, napredak koraka/ponavljanja, završetak, otkazivanje ili grešku bez preuzimanja fokusa ili otkrivanja unetog teksta. Izvršene komande potvrđuju izvršavanje komandi, a ne nezavisno očitano stanje uživo u aplikaciji treće strane. Detalji grešaka nalaze se u dnevniku aktivnosti.

## Uređaji i Corsair

Corsair G1–G6 mogu da se dodele direktno putem zvaničnog SDK-a iCUE. Izaberite uređaj i taster u uređivaču radnji i sačuvajte ili pripremite nacrt iz prikaza tastature K100. Dodele važe za izabrani uređaj i aktivni profil; pauza ih takođe onemogućava. iCUE mora da radi i dozvoljava pristup. Fn, točkić iCUE i kontrole profila i zaključavanja ne mogu da se mapiraju putem ovog G-key API-ja.

Stranica uređaja navodi interfejse tastature Windows i periodično se obnavlja i kada se uređaji povezuju. Fizička tastatura može izložiti više HID interfejsa. Standardni globalni hotkei rade kroz proizvođače.

Otkrivanje Corsair koristi dostupne podatke o uređaju. Upravljanje RGB zahteva kompatibilan hardver, pokrenut iCUE i zvanični SDK iCUE; aplikacija prijavljuje stvarnu dostupnost. Drugi proizvođači trenutno dobijaju otkrivanje i standardne radnje. Izvan integracije Corsair G-key vlasničke kontrole rade kada ih Windows prima kao uobičajen unos. Aplikacija ne instalira drajver tastature.

## Jezici

Podešavanja nude 27 jezika: nemački, engleski, francuski, španski, italijanski, portugalski, holandski, poljski, češki, slovački, mađarski, rumunski, bugarski, grčki, danski, švedski, norveški, finski, estonski, letonski, litvanski, slovenački, hrvatski, ukrajinski, ruski, turski i srpski na latinici. Automatski izbor prati jezik prikaza Windows. Prevodi su priloženi lokalno. Rasporedi, prečice i korisnička imena su nezavisni od jezika interfejsa.

Potpuna dokumentacija repozitorijuma dostupna je na svih 27 jezika aplikacije putem [Indeks jezika](README.md). Prevodi su mašinski potpomognuti, sa vezama ka izvornom tekstu i strukturnim proverama; pregled izvornih govornika je zaseban. Aplikacija prihvata i regionalne kodove `en-US`, `de-DE`, `fr-FR` i `it-IT` i koristi njihove formate brojeva. Postojeći jezički kodovi ostaju kompatibilni.

## Dizajn i istraživanje proizvođača

Prikaz tastature nudi referentne rasporede ISO-DE/ANSI-US pune veličine i bez numeričkog dela i rasporede Corsair K100 RGB. Geometrija predstavlja položaje, široke/visoke tastere i ISO Enter. Standardni rasporedi su generičke reference. Rasporedi K100 prate dokumentovane kontrole bez tvrdnje o milimetarskoj preciznosti. Izaberite ispravnu jezičku/geometrijsku varijantu: ime modela ili USB VID/PID ne određuje konkretnu varijantu.

Mapiranje razrešava fizičke sken-kodove pomoću aktivnog rasporeda unosa Windows. Tok rada priprema nacrt i zadržava provere čuvanja/sukoba. Standardne dodele su globalne prečice Windows i nisu ograničene na jednu povezanu tastaturu. Corsair G1–G6 koriste integraciju iCUE za određeni uređaj. Ostale vlasničke kontrole zahtevaju adapter ili prečice koje je podesio proizvođač. Glavni Enter i Enter numeričkog dela dele identitet virtuelnog tastera za globalne prečice.

U tom [Proizvođač istraživanja](research/tastaturhersteller-sonderfunktionen.md) i [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokriva 74 brendova / proizvođača grupa, razlikujući konfiguratore, ulazni SDK i API osvetljenja. [Layout dokaz](research/keyboard-layout-evidence.md) beleži razlike modela, SKU-a i modula. Obuhvat istraživanja ne znači da je podrška hardvera implementirana.

## podaci

Konfiguracija se čuva u lokalnim podacima aplikacije. Uvoz/izvoz koriste JSON. Uvoz prikazuje pregled konfiguracije, nakon potvrde zamenjuje trenutnu konfiguraciju i onemogućava uvezene dodele dok se ručno ne omoguće. Sadržaj tekstualnih radnji ne prikuplja se kao dnevnik pritisaka tastera. Lozinke upravljača akreditiva odvojene su od izvezenih profila.

## Verifikacija

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows proveri mogu inspektirati hardver i privremeno konfigurisati / izdati podržane iCUE veze. Ispostavite ih bez drugog kontrolera koristeći ove veze. Audio krajnje tačke provere ne menjaju volumen ili mute podešavanja.

Nativni unos ne može da zaobiđe ovlašćenja Windows. Kontroler pokrenut na uobičajen način možda ne može da šalje unos aplikaciji pokrenutoj kao administrator. Rezervisane ili zauzete prečice prikazuju se kao sukobi.

U tom [Indeks dokumentacije](README.md) povezuju istorijske dizajne, planove, istraživanja i datum testove. Istorijski planovi opisuju svoj registrovan opseg, a ne aktuelnu garanciju podrške.

### Šabloni radnog toka i ciljni prozor čekaju

Katalog uključuje šest tokova rada koji mogu da se uređuju: radni prostor, sastanak, striming, istraživanje, promena projekta i tekstualni isečak. Potražite `workflow.` u katalogu ili paleti komandi. Izbor toka rada otvara nesačuvan nacrt; nikada se ne pokreće odmah. Svi koraci ostaju sačuvani pri dodavanju toka rada postojećem makrou ili dodeli tasteru Corsair. Podesite primere URL adresa, putanje programa, profile i scene OBS pre čuvanja.

Korak **Čekanje ciljnog prozora** prihvata ime procesa (sa `.exe` ili bez), podnisku naslova neosetljivu na velika i mala slova i vremensko ograničenje 1–600000 ms. Potreban je bar jedan filter; oba moraju da se poklapaju kada su navedena. Makro se nastavlja tek kada odgovarajući vidljivi prozor odgovori i dođe u prvi plan. Koristite određeni naslov kada više prozora deli proces. Otkazivanje ili istek vremena zaustavlja naredne korake. Spremnost prozora ne garantuje da je dokument učitan ili da je spoljna usluga poput OBS WebSocket povezana.

Sjajnost ekrana: pretraga za **Umnožavanje sjajnosti ekrana**, **Sjajnost sjaja**, ili **Sjajnosti sjaja na 50 odsto** u katalogu akcija. Relativne akcije menjaju sjaj sa 10 procentnih tačaka; fiksne vrednosti variraju od 0 do 100. Podržani unutrašnji ekrani koriste VMI; kompatibilni spoljni monitori zahtevaju DDC/CI aktiviranje u svom meniju. Po podrazumevanju, akcija utiče na sve podržane ekrane; u uređaju se može izabrati eksplicitan monitor. Priključci i dukovi mogu ograničiti podršku.

### Prošireni kontrole

Otvorite paletu komandi pomoću **Ctrl+K dok prozor aplikacije ima fokus**. Potražite **Dizajner toka rada**, **Snimanje makroa**, **Sinhronizacija profila** ili **Dijagnostika tastera** (nazivi prate izabrani jezik). Dizajner uređuje izabrani nacrt pločice: uslove datoteke/procesa/prozora, broj preskakanja unapred i zaustavljanje/nastavak nakon greške. Snimač beleži kombinacije tastera prečica i čekanja, a ne obično kucanje; Escape zaustavlja snimanje. Pregledajte rezultat pre preuzimanja.

U jednoj fazi svetlosti, izaberite sve podržane ekrane ili određeni monitor. Nedostupni izabrani monitor proizvodi grešku umesto da tiho menja drugi ekran. Tabela prikazuje živu svetlost, audio i OBS čitanja kada su njihove integracije dostupne; nemoguće čitanje se ne predstavlja kao merena vrednost.

U uređivaču pločice dodelite podržani fizički poseban taster i izaberite trenutni pritisak, kratak pritisak, držanje ili dvostruki pritisak. Tasteri za izbor sloja tokom držanja privremeno aktiviraju drugo mapiranje i vraćaju prethodni sloj pri otpuštanju. Gestovi i slojevi važe za tastere G SDK-a Corsair i standardne posebne tastere određenog uređaja, a ne obične globalne prečice. Snimanje standardnih tastera podržava F13–F24 i medijske tastere tastature. Vlasničke kontrole Fn, profila i točkića i dalje zahtevaju kompatibilan adapter proizvođača ili njegovo mapiranje na standardni taster. Raw Input prati taster i ne potiskuje njegovo uobičajeno ponašanje u Windows. Dijagnostika tastera objašnjava probleme profila, pauze, veze i dodela.

Sinhronizacija profila razmenjuje datoteke putem izabrane fascikle, uključujući fasciklu kojom upravlja postojeći klijent za oblak. Pregledajte prikaz pre uvoza; uvezene dodele početno su onemogućene i uklanjaju se mapiranja zavisna od hardvera. Revizije sprečavaju neprimetno prepisivanje, a uključivanje sadržaja radnji je izričit izbor. MAKRIVO ne pruža nalog u oblaku niti čuva akreditive za oblak. Ikona u oblasti obaveštenja sada koristi sopstvenu plavozelenu ikonu M MAKRIVO i prikazuje oznaku pauze boje ćilibara kada je pauzirana.

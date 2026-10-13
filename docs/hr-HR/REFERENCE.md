<!-- generated-doc locale=hr-HR source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../README.md)

# MAKRIVO

[Cjelovita dokumentacija na svih 27 jezika](README.md) · [Cjelovit korisnički priručnik](HANDBOOK.md)

**Autor: Aivana.** [Trenutačni pregled funkcija i ograničenja izdanja](FEATURES.md) · [Izvješće o testiranju proširenja](testing/2026-10-12-six-extensions.md) · [Prototip lokalnog AI pomoćnika](testing/2026-10-12-local-meeting-assistant.md)

** Engleski (US) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalni upravljač prečaca Windows s pločicama u stilu Stream Deck, profilima, makronaredbama i otkrivanjem tipkovnica. Sučelje podržava 27 jezika. Za svakodnevnu upotrebu nisu potrebni račun ni veza s oblakom.

[Dokumentacija](README.md) · [Verifikacija](VERIFICATION.md) · [Izvješće o testiranju](testing/2026-10-09-usp-release.md)

## Početak

Nakon izrade distribucije aplikacija se nalazi na `artifacts/win-x64/MAKRIVO.exe`. Cijelu izlaznu mapu držite zajedno; možete je kopirati drugdje. Početna ciljna platforma je Windows 11 x64. Rezultati izgradnje izostavljeni su iz Git; kopija izvornog koda ne sadrži unaprijed izgrađenu izvršnu datoteku.

Instalirajte .NET 10 SDK koji je naveden u `global.json` kako biste stvorili i pokrenuli izvor:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Objavljujte samokontrola bez potrebe za pojedinačno instaliranim .NET radnim vremenom:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Osnovni radni tok

1. Izaberite profil i stvorite akcijski list.
2. Postavite naziv, ikonu, boju i tipkovnički prečac. Kombinacije Ctrl/Alt/Shift smanjuju nenamjerno aktiviranje tijekom pisanja.
3. Dodajte jedan ili više koraka: aplikacije, datoteke, web-mjesta, tekst, prečace, medije, glasnoću, promjenu profila ili odgode.
4. Spremite. Promjene ostaju nacrt dok se ne spreme; odbacivanje vraća spremljenu verziju. Gumb za pokretanje pločice i njezin prečac koriste isti put izvršavanja.
5. Ponovno naručiti ploče i makro korake s drag-and-drop ili strela gumb. Šabloni pojednostavljuju postavke. Makro može ponoviti do 100 puta. Otkažite kroz interfejs, sustav tray meniju ili Ctrl+Alt+Escape po podrazumijevanju.

Profili za određene aplikacije koriste naziv procesa bez pune putanje. Redoslijed profila razrješava jednaka podudaranja; zadani profil služi kao zamjena. Automatski odabir profila mora se omogućiti u postavkama.

Zatvaranje glavnog prozora zadržava aplikaciju u sustavu podrazumevano. Dvostruko kliknite na njegovu ikonu za otvaranje; koristite izbornik za izlazak. Startup s Windows je opcionalan i privremeno isključen. Hotkeys se može odmoriti središnje.

## Akcije i zapovijed paleta

Dodavanje akcije otvara pretraživački katalog s 90 predviđanjima u osam kategorija. Svaki od njih ima opis i predviđanje. Makro urednik koristi isti katalog za dodavanje ili zamjenu koraka. Odabir prednastave u ovom radnom toku urednika stvara nacrt bez izvršenja.

Paleta naredbi otvara se iz zaglavlja ili pomoću Ctrl+K unutar aplikacije. Pretražuje spremljene radnje u svim profilima, predloške kataloga, promjenu profila, navigaciju, pauzu/nastavak, otkazivanje makronaredbi i uvoz/izvoz. Filtrirajte prema spremljenim radnjama, funkcijama kataloga ili naredbama aplikacije. Tipke sa strelicama odabiru, Enter aktivira, Ctrl+Enter uređuje stavku koju je moguće uređivati, a Escape zatvara. Favoriti se prikazuju prvi. Spremne naredbe kataloga mogu se odmah pokrenuti; predlošci koji zahtijevaju postavljanje otvaraju nacrt. U postavkama se može odrediti globalni prečac palete. Nove konfiguracije koriste Ctrl+Shift+Space; postojeće konfiguracije ne dobivaju novi globalni prečac automatski.

Zvučne radnje precizno postavljaju glasnoću od 0 do 100 posto te isključuju, uključuju ili prebacuju zvuk izlaznih i mikrofonskih uređaja. Odaberite određenu krajnju točku ili je ostavite nepostavljenu za zadanu u Windows. Nedostupna spremljena krajnja točka uzrokuje pogrešku umjesto upravljanja drugim uređajem. Osvježavanje uređaja ponovno učitava povezane uređaje.

OBS Studio koristi WebSocket v5: pokretanje/zaustavljanje/prebacivanje/pauziranje/nastavak snimanja; pokretanje/zaustavljanje/prebacivanje prijenosa; promjenu scena; prebacivanje utišavanja ulaza. Omogućite poslužitelj WebSocket u alatima OBS i postavite njegovu krajnju točku u uređivaču. Provjera veze i učitavanje naziva čitaju verziju OBS, scene i ulaze bez pokretanja snimanja ili prijenosa. Odaberite nazive s popisa ili ih unesite ručno. Lozinke se mogu spremiti u Windows Credential Manager i izostavljaju se iz izvezenih konfiguracija. Nedostupne veze OBS prijavljuju pogrešku.

Profili mogu odgovarati nazivu procesa i dijelu naslova prozora. Oba navedena uvjeta moraju se podudarati. Zaključavanje profila sprječava automatsku promjenu tijekom sesije. Snimanje uvjeta iz prvog plana koristi posljednju vanjsku aplikaciju s fokusom.

Radnje za tekst i međuspremnik podržavaju `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Datumi koriste `yyyy-MM-dd`; vremena koriste `HH:mm`. Nepoznata rezervirana mjesta ostaju nepromijenjena. `{{profile}}` označava profil pokrenute radnje; `{{process}}` označava kontekst vanjske aplikacije. Sadržaj međuspremnika čita se samo kada se koristi to rezervirano mjesto. Radnja međuspremnika zapisuje tekst, ali ga ne lijepi automatski.

Radnje prozora minimiziraju, maksimiziraju, vraćaju ili raspoređuju prozor na lijevu/desnu stranu radnog područja njegova monitora. Sistemska radnja zaključava Windows. Te se radnje izvršavaju samo kada su izričito pozvane.

Pretpregled prikazuje korake, ponavljanja i varijable bez izvršavanja. Poništavanje i ponavljanje čuvaju do 100 stanja nacrta; tekstni okviri zadržavaju uobičajeno uređivanje pomoću Ctrl+Z. Označite favorite u uređivaču ili izborniku pločice i filtrirajte ih na nadzornoj ploči. Brojači izvršavanja vrijede samo za trenutačnu sesiju.

Tekst, kratice i akcije prozora pokrenute iz sučelja aktiviraju posljednje vanjsko ciljne prozore. U tom kontekstu prikazuje se cilj. Ako je cilj zatvoren ili Windows odbija aktivaciju, izvršenje se zaustavlja s porukom.

## Ključna pomoć i povratak

Čarobnjak za posebne tipke nalazi se na stranici tipkovnice i u paleti. Navodi tipke G koje prijavljuje iCUE. Otkrivanje pritiska tipke privremeno ih snima bez izvršavanja dodijeljenih radnji. Uobičajene dodjele vraćaju se nakon otkrivanja, otkazivanja ili zatvaranja. Funkcija kataloga i odabrana tipka otvaraju se kao nespremljeni nacrt; provjeravaju se duplicirane dodjele.

Preklopni prikaz tipkovnice prikazuje odabrani referentni raspored i aktivne dodjele. Držite Ctrl+Alt+F1 ili ga uključite/isključite iz palete ili stranice tipkovnice. Ne preuzima fokus i propušta klikove miša. Promjene profila i dodjela ažuriraju se dok je vidljiv; uobičajeni pritisci tipki Windows su istaknuti. Kontrole proizvođača možda ne šalju jednakovrijedan signal tipke Windows.

Povratne informacije o izvršavanju prikazuju čekanje, napredak koraka/ponavljanja, dovršetak, otkazivanje ili pogrešku bez preuzimanja fokusa ili otkrivanja unesenog teksta. Izvršene naredbe potvrđuju izvršavanje naredbi, a ne neovisno očitano stanje uživo u aplikaciji treće strane. Pojedinosti pogrešaka nalaze se u zapisniku aktivnosti.

## Uređaji i Corsair

Corsair G1–G6 mogu se izravno dodijeliti putem službenog SDK-a iCUE. Odaberite uređaj i tipku u uređivaču radnji i spremite ili pripremite nacrt iz prikaza tipkovnice K100. Dodjele vrijede za odabrani uređaj i aktivni profil; pauza ih također onemogućuje. iCUE mora raditi i dopuštati pristup. Fn, kotačić iCUE te kontrole profila i zaključavanja ne mogu se mapirati putem ovog G-key API-ja.

Stranica uređaja popisuje Windows tipkovnice i periodično se osvježi i kada se uređaji povezuju. Fizička tipkovnica može izložiti više HID sučelja. Standardni globalni hotkeys rade među proizvođačima.

Otkrivanje Corsair koristi dostupne podatke o uređaju. Upravljanje RGB zahtijeva kompatibilan hardver, pokrenut iCUE i službeni SDK iCUE; aplikacija prijavljuje stvarnu dostupnost. Drugi proizvođači trenutačno dobivaju otkrivanje i standardne radnje. Izvan integracije Corsair G-key vlasničke kontrole rade kada ih Windows prima kao uobičajen unos. Aplikacija ne instalira upravljački program tipkovnice.

## jezike

Postavke nude 27 jezika: njemački, engleski, francuski, španjolski, talijanski, portugalski, nizozemski, poljski, češki, slovački, mađarski, rumunjski, bugarski, grčki, danski, švedski, norveški, finski, estonski, latvijski, litavski, slovenski, hrvatski, ukrajinski, ruski, turski i srpski na latinici. Automatski odabir prati jezik prikaza Windows. Prijevodi su priloženi lokalno. Rasporedi, prečaci i korisnički nazivi neovisni su o jeziku sučelja.

Cjelovita dokumentacija repozitorija dostupna je na svih 27 jezika aplikacije putem [Indeks jezika](README.md). Prijevodi su strojno potpomognuti, s poveznicama na izvorni tekst i strukturnim provjerama; pregled izvornih govornika zaseban je. Aplikacija prihvaća i regionalne kodove `en-US`, `de-DE`, `fr-FR` i `it-IT` i koristi njihove oblike brojeva. Postojeći jezični kodovi ostaju kompatibilni.

## Layouts i istraživanje proizvođača

Prikaz tipkovnice nudi referentne rasporede ISO-DE/ANSI-US pune veličine i bez numeričkog dijela te rasporede Corsair K100 RGB. Geometrija predstavlja položaje, široke/visoke tipke i ISO Enter. Standardni rasporedi generičke su reference. Rasporedi K100 slijede dokumentirane kontrole bez tvrdnje o milimetarskoj preciznosti. Odaberite ispravnu jezičnu/geometrijsku varijantu: naziv modela ili USB VID/PID ne određuje konkretnu varijantu.

Mapiranje razrješava fizičke kodove tipki pomoću aktivnog rasporeda unosa Windows. Tijek rada priprema nacrt i zadržava provjere spremanja/sukoba. Standardne dodjele globalni su prečaci Windows i nisu ograničene na jednu povezanu tipkovnicu. Corsair G1–G6 koriste integraciju iCUE za određeni uređaj. Ostale vlasničke kontrole zahtijevaju adapter ili prečace koje je konfigurirao proizvođač. Glavni Enter i Enter numeričkog dijela dijele identitet virtualne tipke za globalne prečace.

I to je [Proizvođač istraživanja](research/tastaturhersteller-sonderfunktionen.md) i [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokriva 74 skupine brendova/proizvođača, razlikujući konfiguracije, SDK-ove za ulazak i API-ove za osvjetljenje. [Layout dokazi](research/keyboard-layout-evidence.md) bilježi razlike modela, SKU-a i modula. Obuhvat istraživanja ne znači da je podrška hardvera implementirana.

## podaci

Konfiguracija se pohranjuje u lokalnim podacima aplikacije. Uvoz/izvoz koriste JSON. Uvoz prikazuje pretpregled konfiguracije, nakon potvrde zamjenjuje trenutačnu konfiguraciju i onemogućuje uvezene dodjele dok se ručno ne omoguće. Sadržaj tekstnih radnji ne prikuplja se kao zapis pritisaka tipki. Lozinke upravitelja vjerodajnica odvojene su od izvezenih profila.

## Verifikacija

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows provjere mogu provjeriti hardver i privremeno konfigurirati/izdvojiti podržane iCUE veze. Učinite ih bez drugog kontrolora pomoću tih veznica. Audio endpoint provjere ne mijenjaju živu količinu ili mute postavke.

Izvorni unos ne može zaobići ovlasti Windows. Upravljač pokrenut na uobičajen način možda ne može slati unos aplikaciji pokrenutoj kao administrator. Rezervirani ili zauzeti prečaci prikazuju se kao sukobi.

I to je [Indeks dokumentacije](README.md) Povezivanje povijesnih dizajna, planova, istraživanja i datiranih testnih izvješća. Povijesni planovi opisuju svoj zabilježen raspon, a ne sadašnju jamstvo podrške značajkama.

### Modeli radnog toka i ciljne prozore čekaju

Katalog uključuje šest tijekova rada koji se mogu uređivati: radni prostor, sastanak, prijenos, istraživanje, promjena projekta i tekstni isječak. Potražite `workflow.` u katalogu ili paleti naredbi. Odabir tijeka rada otvara nespremljeni nacrt; nikada se ne pokreće odmah. Svi koraci ostaju sačuvani pri dodavanju tijeka rada postojećoj makronaredbi ili dodjeli tipki Corsair. Konfigurirajte ogledne URL-ove, putanje programa, profile i scene OBS prije spremanja.

Korak **Čekanje ciljnog prozora** prihvaća naziv procesa (s `.exe` ili bez), podniz naslova neosjetljiv na velika i mala slova i vremensko ograničenje 1–600000 ms. Potreban je barem jedan filtar; oba se moraju podudarati kada su navedena. Makronaredba se nastavlja tek kada odgovarajući vidljivi prozor odgovori i dođe u prvi plan. Koristite određeni naslov kada više prozora dijeli proces. Otkazivanje ili istek vremena zaustavlja sljedeće korake. Spremnost prozora ne jamči da je dokument učitan ili da je vanjska usluga poput OBS WebSocket povezana.

Jasnost zaslona: pretraživanje **Svijetlost zaslona**, **Svijetljivost zaslona ** ili **Svijetlosti zaslona na 50 posto** u katalogu djelovanja. Relativne akcije mijenjaju svjetlost po 10 procenatnim točkama; fiksne vrijednosti rastu od 0 do 100. Podržani unutarnji zasloni koriste WMI; kompatibilni vanjski monitori zahtijevaju omogućavanje DDC/CI u njihovom meniju. Podrazumevano, akcija utječe na sve podržane zaslone; izričito se može odabrati u uredniku. Poveznice i dokovi mogu ograničiti podršku.

### proširenje kontrole

Otvorite paletu naredbi pomoću **Ctrl+K dok prozor aplikacije ima fokus**. Potražite **Dizajner tijeka rada**, **Snimanje makronaredbe**, **Sinkronizacija profila** ili **Dijagnostika tipki** (nazivi prate odabrani jezik). Dizajner uređuje odabrani nacrt pločice: uvjete datoteke/procesa/prozora, broj preskakanja unaprijed i zaustavljanje/nastavak nakon pogreške. Snimač bilježi kombinacije tipki prečaca i čekanja, a ne obično pisanje; Escape zaustavlja snimanje. Pregledajte rezultat prije preuzimanja.

U koraku svjetlosti, odaberite sve podržane zaslone ili određeni monitor. Neispunjeni odabran monitor proizvodi pogrešku umjesto da tiho mijenja drugi prikaz. Dashboard prikazuje živu svjetlost, audio i OBS čitanja kada su njihove integracije dostupne; nedostupno čitanje se ne prikazuje kao mjerenu vrijednost.

U uređivaču pločice dodijelite podržanu fizičku posebnu tipku i odaberite trenutačni pritisak, kratki pritisak, držanje ili dvostruki pritisak. Tipke za odabir sloja tijekom držanja privremeno aktiviraju drugo mapiranje i vraćaju prethodni sloj pri otpuštanju. Geste i slojevi vrijede za tipke G SDK-a Corsair i standardne posebne tipke određenog uređaja, a ne obične globalne prečace. Snimanje standardnih tipki podržava F13–F24 i medijske tipke tipkovnice. Vlasničke kontrole Fn, profila i kotačića i dalje zahtijevaju kompatibilan adapter proizvođača ili njegovo mapiranje na standardnu tipku. Raw Input prati tipku i ne potiskuje njezino uobičajeno ponašanje u Windows. Dijagnostika tipki objašnjava probleme profila, pauze, veze i dodjela.

Sinkronizacija profila razmjenjuje datoteke putem odabrane mape, uključujući mapu kojom upravlja postojeći klijent za oblak. Pregledajte pretpregled prije uvoza; uvezene dodjele početno su onemogućene i uklanjaju se mapiranja ovisna o hardveru. Revizije sprječavaju neprimjetno prepisivanje, a uključivanje sadržaja radnji izričit je izbor. MAKRIVO ne pruža račun u oblaku niti čuva vjerodajnice za oblak. Ikona u području obavijesti sada koristi vlastitu plavozelenu ikonu M MAKRIVO i prikazuje jantarnu oznaku pauze kada je pauzirana.

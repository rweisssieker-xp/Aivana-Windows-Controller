<!-- generated-doc locale=sr-Latn source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../HANDBOOK.md)

# MAKRIVO — Korisničko uputstvo

Proizvođač: **Aivana**. Stanje dokumentacije: **2026-10-12**. Ovaj priručnik opisuje trenutnu implementaciju u radnom stablu. Eksperimentalne integracije i funkcije zavisne od hardvera izričito su označene. Istorijski izveštaji opisuju stanje na navedeni datum i nisu garancija za noviju verziju izgradnje.

## Dokumentacija i jezici

Izborom jezika dokumentacije otvorite potpun priručnik, pregled funkcija, rešavanje problema, razvojne beleške, istraživanja i istorijske izveštaje o testiranju na bilo kom od 27 jezika aplikacije. Generisani prevodi su mašinski potpomognuti i strukturno provereni; nisu sve pregledali izvorni govornici. Tehnički identifikatori i izvršivi primeri ostaju nepromenjeni. Izvorni tekst ostaje dostupan za poređenje. Prevedena analiza licenci je informativna i ne zamenjuje izvorne pravne uslove trećih strana ili konačni EULA.

## Instalacija i prvi lansiranje

Početna ciljna platforma je Windows 11 x64. Sve datoteke i poddirektorijume distribucije držite zajedno. Pokrenite `MAKRIVO.exe`. Kopija izvornog koda nije instalacioni program. Samostalni paketi uključuju svoje izvršno okruženje .NET; paketi zavisni od razvojnog okvira zahtevaju odgovarajuća izvršna okruženja .NET 10 Desktop i ASP.NET. ONNX i prateći procesi moraju biti uključeni za pripadajuće funkcije.

1. Pokrenite MAKRIVO i u podešavanjima izaberite jezik interfejsa. Automatski izbor prati Windows. Raspored tastature je zasebno podešavanje.
2. Izaberite profil, pa dodajte pločicu putem kataloga radnji sa pretragom. Dajte joj prepoznatljivo ime i izaberite njene radnje.
3. Pregledajte korake i po potrebi dodelite prečicu. Izbegavajte kombinacije koje već koristi Windows ili ciljna aplikacija.
4. Sačuvajte nacrt. Nesačuvane promene nisu deo sačuvane konfiguracije.
5. Testirajte bezopasnom radnjom, kao što je otvaranje dokumenta, i proverite povratne informacije o izvršavanju.

Zatvaranje glavnog prozora obično ostavlja aplikaciju u oblasti obaveštenja. Dvaput kliknite na njenu ikonu da biste je ponovo otvorili ili koristite meni ikone za izlaz. Pokretanje uz Windows nije obavezno i početno je onemogućeno. Dugme za pauzu privremeno obustavlja dodele tastera; ikona prikazuje oznaku pauze. Aktivne makroe otkažite putem interfejsa, oblasti obaveštenja ili podešene prečice za otkazivanje; podrazumevana je `Ctrl+Alt+Escape`.

## Pločice, radnje i paleta komandi

Pločice pripadaju profilima i mogu da sadrže više koraka određenim redosledom. Uređivač podržava čuvanje/odbacivanje, pregled, poništavanje/ponavljanje i promenu redosleda koraka. Katalog nudi šablone za programe, dokumente, URL adrese, tekst, ostavu, prečice, medije, zvuk, prozore, sistemske radnje i podržane integracije. Šablon koji zahteva parametre otvara se kao nacrt.

Otvorite paletu pomoću `Ctrl+K` dok MAKRIVO ima fokus. Pretražite sačuvane radnje, šablone, promenu profila i alate. Tasteri sa strelicama biraju; Enter aktivira; `Ctrl+Enter` uređuje podržanu stavku; Escape zatvara. Neke spremne komande izvršavaju se odmah, dok podešene radnje zahtevaju pregled. Može se podesiti zasebna globalna prečica za paletu; nove konfiguracije koriste `Ctrl+Shift+Space`.

Koraci za tekst i ostavu podržavaju `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Format datuma je `yyyy-MM-dd`; format vremena je `HH:mm`. Nepoznate promenljive ostaju nepromenjene. Ostava se čita samo na zahtev. Radnja ostave upisuje tekst bez automatskog lepljenja.

Radnje pokrenute iz interfejsa mogu da ciljaju poslednju spoljnu aplikaciju. Ako cilj nestane ili Windows blokira aktiviranje u prvom planu, ručno aktivirajte cilj i pokušajte ponovo. Uspešna isporuka pritiska tastera ne dokazuje da je cilj završio zadatak.

## Profili i podešavanje radnog mesta

Profili mogu da odgovaraju imenu procesa i delu naslova prozora. Kada su postavljena oba uslova, oba moraju da se poklapaju. Redosled profila razrešava više istovremenih poklapanja; podrazumevani profil koristi se kada nema drugih poklapanja. Izričito omogućite automatski izbor. Zaključavanje profila sprečava automatske promene tokom trenutne sesije.

Otvorite podešavanje radnog mesta iz alata ili palete. Izaberite programe, dokumente, veb-sajtove, ciljni audio-uređaj/ekran ili scenu OBS. Pomoćnik priprema profile za radno mesto, sastanke, striming, istraživanje ili projekte. Pregledajte generisane korake pre čuvanja. Podešavanje ne izvršava pripremljene radnje.

## Makroi, uslovi i snimanje

Makroi izvršavaju korake redom i podržavaju čekanja, ponavljanja i otkazivanje. Opšte ograničenje ponavljanja je 100. Koristite pregled pre pokretanja radnji koje unose tekst u drugu aplikaciju ili menjaju stanje sistema.

Čekanje ciljnog prozora prihvata proces, podnisku naslova i vremensko ograničenje od 1 do 600000 ms. Potreban je bar jedan filter. Odgovarajući vidljivi prozor mora da odgovori i postane prozor u prvom planu pre nastavka narednih koraka. Spreman prozor ne dokazuje da je dokument ili usluga završila učitavanje.

Uređivač toka rada dodaje uslove za postojanje datoteke, pokrenuti proces i naslov prozora. Neispunjen uslov preskače korak i može da preskoči dodatne korake unapred. Negirajte uslov da biste izrazili alternativnu granu. Promišljeno izaberite zaustavljanje ili nastavak nakon greške; nastavak može da ostavi kasnije korake bez očekivanih preduslova.

Snimač radnji beleži kombinacije tastera za prečice i čekanja, a ne obično kucanje. Podržava prečice sa modifikatorskim tasterima i funkcijske tastere. Escape zaustavlja snimanje. Pregledajte listu i preuzmite je kao nesačuvan nacrt pločice, pa je izričito sačuvajte. Snimanje klikova interfejsa je zaseban alat opisan u nastavku.

## Tastature, gestovi i slojevi

Stranica tastature navodi otkrivene interfejse i referentne rasporede. Jedna fizička tastatura može da izloži više interfejsa. Izaberite ispravan jezik i geometriju; samo ime proizvoda ne određuje raspored ISO ili ANSI.

Standardne globalne prečice važe na svim tastaturama. Dodele Raw Input za određeni uređaj prate taj uređaj i sken-kod, uključujući podržane funkcijske i medijske tastere. **Raw Input ne potiskuje običan unos u Windows. Isključiva druga tastatura nije spremna za produkcionu upotrebu.** Još je potrebna potpisana, licencirana i testirana pozadinska komponenta sa filterskim drajverom; MAKRIVO ne instalira nijednu.

Podržane dodele posebnih tastera mogu da koriste trenutni pritisak, kratak pritisak, držanje ili dvostruki pritisak. Tasteri privremenog sloja tokom držanja biraju drugo mapiranje i vraćaju osnovni sloj pri otpuštanju; tasteri za izbor sloja ne pokreću sopstveni makro. Pragovi držanja kreću se od 150 do 5000 ms; vremenski prozori dvostrukog pritiska od 150 do 1000 ms. Ta mapiranja su odvojena od običnih globalnih prečica.

Čarobnjak za posebne tastere otkriva kompatibilne tastere bez izvršavanja dodeljenih radnji tokom snimanja. Dijagnostika objašnjava nedostupne uređaje, pauzirane dodele, nepoklapanje profila i sukobe. Preklopni prikaz pokazuje izabrani raspored i dodele; podrazumevana prečica koja se drži za prikaz je `Ctrl+Alt+F1`.

| Integracija | Potrebe i ograničenja |
| --- | --- |
| Corsair | Pokrenut iCUE, pristup SDK-u i kompatibilan model. G1–G6 i podržane RGB funkcije; vlasničke kontrole Fn, točkića, zaključavanja i profila uglavnom nisu dostupne preko G-key API-ja. |
| Logitech G | Izvorne lokalne biblioteke x64 G-key/LED SDK zahtevaju izričito odobrenje. Događaji G-key ne omogućavaju jednoznačno razlikovanje više identičnih fizičkih tastatura. |
| Razer | Chroma RGB i standardni unos; po potrebi mapirajte vlasničke kontrole u Synapse. |
| SteelSeries | GameSense RGB i standardni unos; po potrebi mapirajte vlasničke kontrole u GG. |
| Wooting | Kompatibilni izvorni lokalni SDK x64 za analogne vrednosti i pragove; uobičajeni unos HID ostaje aktivan. |
| Ostale tastature | Standardne prečice ili podržane fizičke dodele sken-kodova. Fn i vlasničke kontrole zavise od modela i podešavanja proizvođača. |

Istraživanje proizvođača opisuje dokumentovane mogućnosti, a ne implementiranu ili testiranu kompatibilnost sa svakim navedenim uređajem. Novi adapteri još zahtevaju prihvatno testiranje na stvarnom hardveru. Ne preuzimajte binarne datoteke SDK-a iz nepouzdanih izvora; prava distribucije SDK-a odvojena su od tehničke kompatibilnosti.

## Zvuk, ekrani i OBS

Zvučni koraci postavljaju jačinu zvuka od 0 do 100 procenata i isključuju, uključuju ili menjaju stanje utišavanja podržanih izlaznih ili mikrofonskih krajnjih tačaka. Izaberite određenu krajnju tačku ili podrazumevanu u Windows. Nedostupan sačuvani cilj izaziva grešku umesto neprimetnog upravljanja drugim uređajem. Nakon ponovnog povezivanja osvežite uređaje. Upravljanje krajnjim tačkama ne podrazumeva podršku za nezavisni mikser svake aplikacije.

Radnje ekrana povećavaju/smanjuju osvetljenost za 10 procentnih poena ili postavljaju vrednost od 0 do 100. Unutrašnji ekrani koriste podržane interfejse WMI; spoljni monitori zahtevaju podršku DDC/CI i obično njeno uključivanje u meniju monitora. Priključne stanice, adapteri i kablovi mogu da blokiraju upravljanje. Izaberite sve podržane ekrane ili jedan monitor. Ponovno povezivanje može da promeni dostupne identifikatore; proverite sačuvani cilj. Nedostupna očitavanja prikazuju se kao nedostupna, a ne kao izmerene vrednosti.

OBS zahteva omogućen WebSocket v5 u alatima OBS. Podesite krajnju tačku, testirajte vezu i učitajte imena scena/ulaza pre čuvanja. Podržane radnje uključuju promenu scene, pokretanje/zaustavljanje/prebacivanje striminga i snimanja, pauziranje/nastavak snimanja i utišavanje ulaza. Lozinke mogu da se čuvaju u Windows Credential Manager i izostavljaju se iz izvoza konfiguracije. Uspešna komanda OBS ne dokazuje sve dalje efekte striminga.

## Provera rezultata i istorija izvršavanja

Omogućite proveru rezultata za odgovarajuće korake. Aplikacija razlikuje prethodne provere, potvrđene efekte, greške, otkazivanje i efekte koji se ne mogu proveriti. Postojanje procesa nije spremnost aplikacije. Relativnim promenama, prečicama i unosu teksta ne može se pripisati opšte zagarantovan uspeh. Kada radnja ne uspe, pregledajte dnevnik aktivnosti i istoriju provere. Ispravite greške konfiguracije pre ponovnog pokretanja celog makroa.

## Automatizacija interfejsa i snimanje klikova

Dodajte korak automatizacije interfejsa, izaberite operaciju i nakon prikazanog odlaganja snimite cilj ispod pokazivača. Samo snimanje ništa ne izvršava. Proverite selektor i zahtevajte jednoznačno poklapanje. Podržane operacije uključuju `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` i `WaitState`. Polja lozinki, onemogućene kontrole i dvosmisleni selektori odbijaju se.

Automatizacija interfejsa radi u ograničenom pratećem procesu. Zavisi od podrške ciljne aplikacije za pristupačnost Windows i dozvola. Snimač klikova beleži podržane radnje i pauze; ne snima opšti unos teksta. Pregledajte i proverite nacrt pre preuzimanja. Promene interfejsa spoljne aplikacije mogu da ponište valjanost selektora. Izolacija procesa ograničava posledice rušenja i omogućava ograničavanje izvršavanja pri otkazivanju, ali nije bezbednosni sandbox.

## Dodaci

Otvorite proširenja i pregledajte paket pre uvoza. Proveravaju se struktura paketa, manifest i heš-vrednosti. Uvezeni dodatak početno je onemogućen. Pročitajte navedene radnje i izričito potvrdite poverenje izdavaču pre omogućavanja. Dodaci rade sa vašim korisničkim dozvolama u zasebnom procesu; taj proces **nije sandbox**. Instalirajte samo pakete kojima verujete.

Upravljač podržava izvoz, onemogućavanje, deinstalaciju i vraćanje/odbacivanje prethodne verzije. Nedostajuće zavisnosti blokiraju pozivanje. Primer paketa nudi odbrojavanje i radnje lokalnog stanja. Podržana šema je dokumentovani podskup JSON-Schema, a ne sve moguće funkcije JSON-Schema. Ne postoji javno tržište dodataka.

## Mobilni panel

1. Otvorite mobilni panel, izaberite lokalni IP i izričito izaberite profile koji će biti dostupni. Podrazumevana adresa loopback dostupna je samo na samom računaru; za telefon ili tablet izaberite odgovarajuću adresu LAN.
2. Pokrenite panel i pročitajte njegovu adresu HTTPS, otisak sertifikata i jednokratni šestocifreni kod za uparivanje.
3. Na uređaju u istoj pouzdanoj mreži otvorite adresu. Uporedite otisak SHA-256 putem pouzdanog kanala pre prihvatanja lokalnog sertifikata. Aplikacija ne instalira pouzdan sertifikat automatski.
4. Uparite uređaj prikazanim kodom i testirajte jednu bezopasnu pločicu. Za pristup preko LAN možda je potrebna ručna dozvola u Windows Firewall. Ne izlažite mrežni port internetu.
5. Zaustavite panel kada završite ili putem aplikacije opozovite pristup uparenim klijentima.

Privatni ključevi servera zaštićeni su pomoću Windows DPAPI; akreditivi klijenata čuvaju se kao heš-vrednosti. Promena IP ili obnova sertifikata može da zahteva ponovno uspostavljanje poverenja. Panel prikazuje stanja radnji i provera, a ne sve moguće senzore uživo. Provere rasporeda u pregledaču i lokalnog HTTPS ne zamenjuju prihvatno testiranje stvarnog telefona/tableta preko LAN.

## Lokalni AI pomoćnik za sastanke

Ovaj neobavezni eksperimentalni pomoćnik podrazumevano je onemogućen. Otvorite ga iz alata, izričito omogućite, izaberite izvršno okruženje/model i dodajte samo resurse koje želite da razmotri. Navedite cilj, napravite nacrt, pregledajte nerešene zahteve i prenesite rezultat u obični uređivač. Ništa se ne izvršava automatski; čuvanje i pokretanje ostaju izričite radnje. Nema automatskog prelaska na drugo izvršno okruženje.

| Izvršno okruženje | Podešavanje |
| --- | --- |
| Ollama | Zasebna lokalna usluga i lokalno instaliran model na `127.0.0.1:11434`. Deklarisani udaljeni modeli/modeli u oblaku odbijaju se. |
| LM Studio | Lokalni server sa lokalno učitanim modelom i podrškom za šemu JSON na `127.0.0.1:1234`. Ne omogućavajte udaljeno rutiranje za lokalnu obradu. |
| llama.cpp | Lokalni `llama-server` i kompatibilni model sa podrškom za šemu JSON na `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Pouzdana kompatibilna lokalna fascikla tekstualnog modela GenAI; izaberite `genai_config.json`. Izvođenje modela koristi priloženi prateći proces bez HTTP. |

Klijenti HTTP koriste samo loopback, ne koriste proksije i ne prate preusmeravanja. MAKRIVO ne može pouzdano da utvrdi da li kompatibilni lokalni server interno poziva udaljenu uslugu. Lokalni serveri sa autentifikacijom ne mogu da se podese u ovoj verziji. Težine modela, Python i lokalni serveri za izvođenje modela ne preuzimaju se niti instaliraju automatski.

Paketi ONNX zahtevaju `tokenizer.json` i važeći šablon razgovora. Podržani oblici šablona u `tokenizer_config.json` jesu string `chat_template`, rečnik sa stringom `default` ili nedvosmislena lista imenovanih/podrazumevanih šablona. Ako šablon nedostaje ili je null, kao zamena koristi se `chat_template.jinja`. Prazni, dvosmisleni, preveliki šabloni i šabloni pogrešnog tipa podataka odbijaju se. Proizvoljne datoteke ONNX, modeli vektorskih reprezentacija/vida i paketi samo za GPU nisu zamenjivi sa kompatibilnim tekstualnim modelima GenAI. Priložena verzija izvršnog okruženja GenAI je 0.17.1, CPU. Koristite samo pouzdane modele; razdvajanje procesa nije sandbox.

Pomoćnik podržava izabrane programe bez argumenata, izabrane dokumente i veb-sajtove i utišavanje mikrofona kada je izričito zatraženo. Ne pravi poruke, sastanke, proizvoljne skripte, proizvoljne klikove, preuzimanja niti prebacivanje slušalica. Cilj i imena resursa šalju se izabranom lokalnom izvršnom okruženju; putanje resursa i sadržaj datoteka ne dostavljaju se zasebno. Cilj koji korisnik unese može sam da sadrži lične podatke. Mrežno ponašanje lokalnog izvršnog okruženja ostaje odgovornost korisnika.

Vremensko ograničenje zahteva ONNX je 90 sekundi, uključujući učitavanje modela, uz najviše 3072 ulazna tokena i 1024 generisana tokena, dodatno ograničena kontekstom modela. Neispravan/odsečen JSON i nepoznati identifikatori resursa odbijaju se. Zahtev završite otkazivanjem ili zatvaranjem. Test izvršnog okruženja sa nasumičnim modelom dokazuje izvršavanje, a ne korisni kvalitet planiranja ili uštedu vremena. Lokalni dnevnici eksperimenata sadrže identifikatore događaja i vremena, a ne tekst cilja ili sadržaj dokumenata; ne šalju se automatski na server.

Naknadna procena malog modela Instruct nije proizvela nijedan važeći plan u četiri nemačka/engleska testa. Nativno izvođenje modela radilo je, ali model nije zadovoljio stroge zahteve planiranja. To je negativan rezultat kvaliteta, a ne preporuka modela ili dokazano koristan pomoćnik. Za tačnu reviziju modela i ograničenja pogledajte datirani izveštaj o testu lokalnog izvršnog okruženja.

## Uvoz, izvoz, sinhronizacija i rezervne kopije

Pregledajte prikaz uvoza pre zamene konfiguracije. Uvezene radnje početno su onemogućene, a prenosiva sinhronizacija uklanja dodele zavisne od hardvera. Nakon pregleda ponovo dodelite uređaje i promišljeno omogućite radnje. Pregledajte tekst/sadržaj radnji pre deljenja izvoza; izostavljanje lozinki ne znači da sav korisnički sadržaj radnji nema osetljive podatke.

Sinhronizacija profila razmenjuje datoteke u izabranoj fascikli, kojom po želji upravlja postojeći klijent za oblak. MAKRIVO ne pruža nalog u oblaku i ne čuva akreditive za oblak. Revizije sprečavaju neprimetno prepisivanje. Uključivanje sadržaja radnji je izričit izbor. Napravite rezervnu kopiju konfiguracije pre zamenskog uvoza. Podrazumevana konfiguracija je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; istorijsko ime direktorijuma zadržano je radi kompatibilnosti. Zatvorite aplikaciju pre ručnog kopiranja/vraćanja. Sačuvana odobrenja SDK-a i akreditive zaštićene operativnim sistemom možda treba ponovo napraviti na drugom računaru.

## Rešavanje problema

| Simptom | Šta proveriti |
| --- | --- |
| Prečica ništa ne radi | Aplikacija radi, dodele nisu pauzirane, ispravan aktivni profil, sačuvana/omogućena pločica, sukobi prečica i dostupan cilj. |
| Ciljni prozor ne može da se aktivira | Ručno ga aktivirajte. Ograničenja aktiviranja u prvom planu u Windows i razlike u ovlašćenjima mogu da blokiraju automatizaciju. |
| Poseban taster nije otkriven | Podrška usluge proizvođača/SDK-a/modela; ako je moguće, podesite vlasničke kontrole tako da šalju podržani standardni taster. |
| Druga tastatura takođe normalno unosi tekst | To je očekivano uz Raw Input. Isključivo potiskivanje unosa nije implementirano za produkcionu upotrebu. |
| Zvuk ili osvetljenost nisu dostupni | Osvežite uređaje; proverite izabrani identifikator, sesije krajnjih tačaka, podršku WMI/DDC/CI i ograničenja priključne stanice/kabla. |
| OBS ofline | Omogućen WebSocket v5, ispravna krajnja tačka/lozinka, dostupna usluga i važeća imena scena/ulaza. |
| Automatizacija interfejsa ne uspeva | Prateći proces je uključen, jednoznačan važeći selektor, pristupačan omogućen cilj, podržana kontrola i dozvole. Nakon privremenog isteka vremena pokušajte ponovo tek nakon provere cilja. |
| Mobilni panel nije dostupan | Adresa LAN umesto loopback, ista mreža, pokrenut server, odobreni profili, otisak sertifikata/poverenje i ručne dozvole zaštitnog zida. |
| Dodatak ne uspeva | Integritet paketa, izričito poverenje/omogućeno stanje, zavisnosti, kompatibilnost protokola i dnevnik izvršavanja. |
| AI zahtev ne uspeva | Ispravno izabrano lokalno izvršno okruženje, spremna usluga/model, podržana šema JSON, pouzdana kompatibilna fascikla modela i ograničenja zahteva. |
| Raspored ili tekst izgledaju pogrešno | Razlikujte jezik interfejsa od geometrije tastature; promenite veličinu prozora, proverite skaliranje ekrana i prijavite tačnu stranicu/jezik/verziju izgradnje. |

Za koristan izveštaj o grešci navedite verziju Windows, verziju izgradnje aplikacije/reviziju koda, jezik, skaliranje ekrana, model hardvera, minimalni niz koraka i relevantni dnevnik sa uklonjenim osetljivim podacima. Pre objavljivanja uklonite lične putanje, sadržaj radnji i akreditive.

## Izgradnja, testiranje i pakovanje

Koristite SDK čija je verzija određena u `global.json`. Iz korenskog direktorijuma repozitorijuma:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Integracioni testovi Windows mogu da otvore izolovane prozore i privremeno podese/oslobode podržane dodele proizvođača. Pokrenite ih bez drugog kontrolera koji koristi te dodele. Podrazumevane provere osvetljenosti/zvuka samo čitaju podatke; izričiti testovi promene i vraćanja hardverskog stanja su zasebni. Simulirani protokoli, iscrtani snimci ekrana i nasumični grafovi izvođenja modela ne smeju se predstavljati kao prihvatno testiranje stvarnog hardvera ili kvaliteta modela.

Potpuni postupak pravljenja paketa okuplja prateće procese, resurse mobilnog panela, primer dodatka, izvršno okruženje ONNX i dostupna obaveštenja o zavisnostima. Sačuvajte njegove poddirektorijume. Uspešna izgradnja nije potpisani instalacioni program niti pravno odobrenje izdavanja. Izvorni kod i verzije izgradnje mogu da se razvijaju zasebno; uporedite stanje dokumentacije i reviziju koda sa instaliranom verzijom.

## Licenciranje, privatnost i status izdanja

Predviđeni komercijalni model je jednokratna kupovina sa trajnom upotrebom kupljene glavne verzije; konačna cena, dozvoljeni broj uređaja i uslovi nadogradnje moraju se utvrditi u stvarnim uslovima prodaje. Ovaj priručnik ne uspostavlja produkcioni sistem kupovine/aktivacije. Za svakodnevno upravljanje nije potreban nalog u oblaku.

Ne distribuirajte vlasničke binarne datoteke SDK-a bez dokazane dozvole. Sačuvajte licence i obaveštenja trećih strana za svaku isporučenu zavisnost, uključujući pakete .NET i ONNX. Težine modela imaju sopstvene uslove. Datirana revizija licenci prethodi nekim novim zavisnostima i ne predstavlja odobrenje za te dodatke. Javna dostupnost repozitorijuma sama po sebi ne daje opštu licencu otvorenog koda.

Stvarni hardver proizvođača, rad telefona/tableta preko LAN, prelazi između različitih monitora/DPI, dugotrajna upotreba, potpun jezički pregled, podrška isključivog drajvera i korisni kvalitet AI planiranja ostaju predmet zasebnog prihvatnog testiranja. Nikada ne zaključujte da je nešto odobreno samo zato što funkcija postoji.

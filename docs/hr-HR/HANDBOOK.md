<!-- generated-doc locale=hr-HR source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../HANDBOOK.md)

# MAKRIVO — Korisnički priručnik

Proizvođač: **Aivana**. Stanje dokumentacije: **2026-10-12**. Ovaj priručnik opisuje trenutačnu implementaciju u radnom stablu. Eksperimentalne integracije i funkcije ovisne o hardveru izričito su označene. Povijesna izvješća opisuju stanje na navedeni datum i nisu jamstvo za noviju međuverziju.

## Dokumentacija i jezici

Odabirom jezika dokumentacije otvorite cjelovit priručnik, pregled funkcija, rješavanje problema, razvojne bilješke, istraživanja i povijesna izvješća o testiranju na bilo kojem od 27 jezika aplikacije. Generirani prijevodi strojno su potpomognuti i strukturno provjereni; nisu sve pregledali izvorni govornici. Tehnički identifikatori i izvršivi primjeri ostaju nepromijenjeni. Izvorni tekst ostaje dostupan za usporedbu. Prevedena analiza licenci informativna je i ne zamjenjuje izvorne pravne uvjete trećih strana ili konačni EULA.

## Ugradnja i prvi pokret

Početna ciljna platforma je Windows 11 x64. Sve datoteke i podmape distribucije držite zajedno. Pokrenite `MAKRIVO.exe`. Kopija izvornog koda nije instalacijski program. Samostalni paketi uključuju svoje izvršno okruženje .NET; paketi ovisni o razvojnom okviru zahtijevaju odgovarajuća izvršna okruženja .NET 10 Desktop i ASP.NET. ONNX i prateći procesi moraju biti uključeni za pripadajuće funkcije.

1. Pokrenite MAKRIVO i u postavkama odaberite jezik sučelja. Automatski odabir prati Windows. Raspored tipkovnice zasebna je postavka.
2. Odaberite profil, a zatim dodajte pločicu putem kataloga radnji s pretraživanjem. Dodijelite joj prepoznatljiv naziv i odaberite njezine radnje.
3. Pregledajte korake i prema potrebi dodijelite prečac. Izbjegavajte kombinacije koje već koristi Windows ili ciljna aplikacija.
4. Spremite nacrt. Nespremljene promjene nisu dio spremljene konfiguracije.
5. Test s neškodljivim djelovanjem, kao što je otvaranje dokumenta, i provjeriti povratne informacije.

Zatvaranje glavnog prozora obično ostavlja aplikaciju u području obavijesti. Dvokliknite njezinu ikonu da biste je ponovno otvorili ili upotrijebite izbornik ikone za izlaz. Pokretanje uz Windows nije obvezno i početno je onemogućeno. Gumb za pauzu privremeno obustavlja dodjele tipki; ikona prikazuje oznaku pauze. Aktivne makronaredbe otkažite putem sučelja, područja obavijesti ili postavljenog prečaca za otkazivanje; zadani je `Ctrl+Alt+Escape`.

## Pločice, radnje i paleta naredbi

Pločice pripadaju profilima i mogu sadržavati više koraka zadanim redoslijedom. Uređivač podržava spremanje/odbacivanje, pretpregled, poništavanje/ponavljanje i promjenu redoslijeda koraka. Katalog nudi predloške za programe, dokumente, URL-ove, tekst, međuspremnik, prečace, medije, zvuk, prozore, sistemske radnje i podržane integracije. Predložak koji zahtijeva parametre otvara se kao nacrt.

Otvorite paletu pomoću `Ctrl+K` dok MAKRIVO ima fokus. Pretražite spremljene radnje, predloške, promjenu profila i alate. Tipke sa strelicama odabiru; Enter aktivira; `Ctrl+Enter` uređuje podržanu stavku; Escape zatvara. Neke spremne naredbe izvršavaju se odmah, dok konfigurirane radnje zahtijevaju pregled. Može se postaviti zaseban globalni prečac za paletu; nove konfiguracije koriste `Ctrl+Shift+Space`.

Koraci za tekst i međuspremnik podržavaju `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Oblik datuma je `yyyy-MM-dd`; oblik vremena je `HH:mm`. Nepoznate varijable ostaju nepromijenjene. Međuspremnik se čita samo na zahtjev. Radnja međuspremnika zapisuje tekst bez automatskog lijepljenja.

Radnje pokrenute iz sučelja mogu ciljati posljednju vanjsku aplikaciju. Ako cilj nestane ili Windows blokira aktiviranje u prvom planu, ručno aktivirajte cilj i pokušajte ponovno. Uspješna isporuka pritiska tipke ne dokazuje da je cilj dovršio zadatak.

## Profili i postavljanje radnog mjesta

Profili mogu odgovarati nazivu procesa i dijelu naslova prozora. Kada su postavljena oba uvjeta, oba se moraju podudarati. Redoslijed profila razrješava više istodobnih podudaranja; zadani profil služi kao zamjena ako nema drugih podudaranja. Izričito omogućite automatski odabir. Zaključavanje profila sprječava automatske promjene tijekom trenutačne sesije.

Otvorite postavljanje radnog mjesta iz alata ili palete. Odaberite programe, dokumente, web-mjesta, ciljni audiouređaj/zaslon ili scenu OBS. Pomoćnik priprema profile za radno mjesto, sastanke, prijenos, istraživanje ili projekte. Pregledajte izrađene korake prije spremanja. Postavljanje ne izvršava pripremljene radnje.

## Makronaredbe, uvjeti i snimanje

Makronaredbe izvršavaju korake redom i podržavaju čekanja, ponavljanja i otkazivanje. Opće ograničenje ponavljanja je 100. Koristite pretpregled prije pokretanja radnji koje unose tekst u drugu aplikaciju ili mijenjaju stanje sustava.

Čekanje ciljnog prozora prihvaća proces, podniz naslova i vremensko ograničenje od 1 do 600000 ms. Potreban je barem jedan filtar. Odgovarajući vidljivi prozor mora odgovoriti i postati prozor u prvom planu prije nastavka sljedećih koraka. Spreman prozor ne dokazuje da je dokument ili usluga završila učitavanje.

Uređivač tijeka rada dodaje uvjete postojanja datoteke, pokrenutog procesa i naslova prozora. Neispunjen uvjet preskače korak i može preskočiti dodatne korake unaprijed. Negirajte uvjet da biste izrazili alternativnu granu. Promišljeno odaberite zaustavljanje ili nastavak nakon pogreške; nastavak može ostaviti kasnije korake bez očekivanih preduvjeta.

Snimač radnji bilježi kombinacije tipki prečaca i čekanja, a ne običan unos teksta. Podržava prečace s modifikatorskim tipkama i funkcijske tipke. Escape zaustavlja snimanje. Pregledajte popis i preuzmite ga kao nespremljeni nacrt pločice, a zatim ga izričito spremite. Snimanje klikova sučelja zaseban je alat opisan u nastavku.

## Tipkovnice, geste i slojevi

Stranica tipkovnice navodi otkrivena sučelja i referentne rasporede. Jedna fizička tipkovnica može izložiti više sučelja. Odaberite ispravan jezik i geometriju; sam naziv proizvoda ne određuje raspored ISO ili ANSI.

Standardni globalni prečaci vrijede na svim tipkovnicama. Dodjele Raw Input za određeni uređaj prate taj uređaj i kod tipke, uključujući podržane funkcijske i medijske tipke. **Raw Input ne potiskuje običan unos u Windows. Isključiva druga tipkovnica nije spremna za produkcijsku upotrebu.** Još je potrebna potpisana, licencirana i testirana pozadinska komponenta s filtarskim upravljačkim programom; MAKRIVO ne instalira nijednu.

Podržane dodjele posebnih tipki mogu koristiti trenutačni pritisak, kratki pritisak, držanje ili dvostruki pritisak. Tipke privremenog sloja tijekom držanja odabiru drugo mapiranje i vraćaju osnovni sloj pri otpuštanju; tipke za odabir sloja ne pokreću vlastitu makronaredbu. Pragovi držanja kreću se od 150 do 5000 ms; vremenski prozori dvostrukog pritiska od 150 do 1000 ms. Ta su mapiranja odvojena od običnih globalnih prečaca.

Čarobnjak za posebne tipke otkriva kompatibilne tipke bez izvršavanja dodijeljenih radnji tijekom snimanja. Dijagnostika objašnjava nedostupne uređaje, pauzirane dodjele, nepodudaranje profila i sukobe. Preklopni prikaz prikazuje odabrani raspored i dodjele; zadani prečac koji se drži za prikaz jest `Ctrl+Alt+F1`.

| integracija | Uvjeti i ograničenja |
| --- | --- |
| Corsair | Pokrenut iCUE, pristup SDK-u i kompatibilan model. G1–G6 i podržane funkcije RGB; vlasničke kontrole Fn, kotačića, zaključavanja i profila uglavnom nisu dostupne putem G-key API-ja. |
| Logitech G | Izvorne lokalne biblioteke x64 G-key/LED SDK zahtijevaju izričito odobrenje. Događaji G-key ne omogućuju jednoznačno razlikovanje više identičnih fizičkih tipkovnica. |
| Razer | Chroma RGB i standardni unos; prema potrebi mapirajte vlasničke kontrole u Synapse. |
| SteelSeries | GameSense RGB i standardni unos; prema potrebi mapirajte vlasničke kontrole u GG. |
| Wooting | Kompatibilni izvorni lokalni SDK x64 za analogne vrijednosti i pragove; uobičajeni unos HID ostaje aktivan. |
| Ostale tipkovnice | Standardni prečaci ili podržane fizičke dodjele kodova tipki. Fn i vlasničke kontrole ovise o modelu i postavkama proizvođača. |

Istraživanje proizvođača opisuje dokumentirane mogućnosti, a ne implementiranu ili testiranu kompatibilnost sa svakim navedenim uređajem. Novi adapteri još zahtijevaju prihvatno testiranje na stvarnom hardveru. Ne preuzimajte binarne datoteke SDK-a iz nepouzdanih izvora; prava distribucije SDK-a odvojena su od tehničke kompatibilnosti.

## Zvuk, zasloni i OBS

Zvučni koraci postavljaju glasnoću od 0 do 100 posto te isključuju, uključuju ili prebacuju zvuk podržanih izlaznih ili mikrofonskih krajnjih točaka. Odaberite određenu krajnju točku ili zadanu u Windows. Nedostupni spremljeni cilj uzrokuje pogrešku umjesto neprimjetnog upravljanja drugim uređajem. Nakon ponovnog povezivanja osvježite uređaje. Upravljanje krajnjim točkama ne podrazumijeva podršku za neovisni mikser svake aplikacije.

Radnje zaslona povećavaju/smanjuju svjetlinu za 10 postotnih bodova ili postavljaju vrijednost od 0 do 100. Unutarnji zasloni koriste podržana sučelja WMI; vanjski monitori zahtijevaju podršku DDC/CI i obično njezino uključivanje u izborniku monitora. Priključne stanice, adapteri i kabeli mogu blokirati upravljanje. Odaberite sve podržane zaslone ili jedan monitor. Ponovno povezivanje može promijeniti dostupne identifikatore; provjerite spremljeni cilj. Nedostupna očitanja prikazuju se kao nedostupna, a ne kao izmjerene vrijednosti.

OBS zahtijeva omogućen WebSocket v5 u alatima OBS. Postavite krajnju točku, testirajte vezu i učitajte nazive scena/ulaza prije spremanja. Podržane radnje uključuju promjenu scene, pokretanje/zaustavljanje/prebacivanje prijenosa i snimanja, pauziranje/nastavak snimanja i utišavanje ulaza. Lozinke se mogu pohraniti u Windows Credential Manager i izostavljaju se iz izvoza konfiguracije. Uspješna naredba OBS ne dokazuje sve daljnje učinke prijenosa.

## Provjera rezultata i povijest izvršavanja

Omogućite provjeru rezultata za prikladne korake. Aplikacija razlikuje prethodne provjere, potvrđene učinke, pogreške, otkazivanje i učinke koji se ne mogu provjeriti. Postojanje procesa nije spremnost aplikacije. Relativnim promjenama, prečacima i unosu teksta ne može se pripisati općenito zajamčen uspjeh. Kada radnja ne uspije, pregledajte zapisnik aktivnosti i povijest provjere. Ispravite pogreške konfiguracije prije ponovnog pokretanja cijele makronaredbe.

## Automatizacija sučelja i snimanje klikova

Dodajte korak automatizacije sučelja, odaberite operaciju i nakon prikazane odgode snimite cilj ispod pokazivača. Samo snimanje ništa ne izvršava. Provjerite selektor i zahtijevajte jednoznačno podudaranje. Podržane operacije uključuju `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` i `WaitState`. Polja lozinki, onemogućene kontrole i dvosmisleni selektori odbijaju se.

Automatizacija sučelja radi u ograničenom pratećem procesu. Ovisi o podršci ciljne aplikacije za pristupačnost Windows i dopuštenjima. Snimač klikova bilježi podržane radnje i stanke; ne snima opći unos teksta. Pregledajte i provjerite nacrt prije preuzimanja. Promjene sučelja vanjske aplikacije mogu poništiti valjanost selektora. Izolacija procesa ograničava posljedice rušenja i omogućuje ograničavanje izvršavanja pri otkazivanju, ali nije sigurnosni sandbox.

## Dodaci

Otvorite proširenja i pregledajte paket prije uvoza. Provjeravaju se struktura paketa, manifest i sažeci. Uvezeni dodatak početno je onemogućen. Pročitajte navedene radnje i izričito potvrdite povjerenje izdavaču prije omogućivanja. Dodaci rade s vašim korisničkim dopuštenjima u zasebnom procesu; taj proces **nije sandbox**. Instalirajte samo pakete kojima vjerujete.

Upravitelj podržava izvoz, onemogućivanje, deinstalaciju i vraćanje/odbacivanje prethodne verzije. Nedostajuće ovisnosti blokiraju pozivanje. Primjer paketa nudi odbrojavanje i radnje lokalnog stanja. Podržana shema dokumentirani je podskup JSON-Schema, a ne sve moguće značajke JSON-Schema. Ne postoji javno tržište dodataka.

## Mobilna ploča

1. Otvorite mobilnu ploču, odaberite lokalni IP i izričito odaberite profile koji će biti dostupni. Zadana adresa loopback dostupna je samo na samom računalu; za telefon ili tablet odaberite odgovarajuću adresu LAN.
2. Pokrenite ploču i pročitajte njezinu adresu HTTPS, otisak certifikata i jednokratni šesteroznamenkasti kod za uparivanje.
3. Na uređaju u istoj pouzdanoj mreži otvorite adresu. Usporedite otisak SHA-256 putem pouzdanog kanala prije prihvaćanja lokalnog certifikata. Aplikacija ne instalira pouzdan certifikat automatski.
4. Uparite uređaj prikazanim kodom i testirajte jednu bezopasnu pločicu. Za pristup putem LAN može biti potrebno ručno dopuštenje u Windows Firewall. Ne izlažite mrežni priključak internetu.
5. Zaustavite ploču kada završite ili putem aplikacije opozovite pristup uparenim klijentima.

Privatni ključevi poslužitelja zaštićeni su pomoću Windows DPAPI; vjerodajnice klijenata pohranjuju se kao sažeci. Promjena IP ili obnova certifikata može zahtijevati ponovno uspostavljanje povjerenja. Ploča prikazuje stanja radnji i provjera, a ne sve moguće senzore uživo. Provjere rasporeda u pregledniku i lokalnog HTTPS ne zamjenjuju prihvatno testiranje stvarnog telefona/tableta putem LAN.

## Lokalni pomoćnik za sastanke s umjetnom inteligencijom

Ovaj neobvezni eksperimentalni pomoćnik zadano je onemogućen. Otvorite ga iz alata, izričito omogućite, odaberite izvršno okruženje/model i dodajte samo resurse koje želite da razmotri. Navedite cilj, izradite nacrt, pregledajte neriješene zahtjeve i prenesite rezultat u obični uređivač. Ništa se ne izvršava automatski; spremanje i pokretanje ostaju izričite radnje. Nema automatskog prelaska na drugo izvršno okruženje.

| Izvršno okruženje | Postavljanje |
| --- | --- |
| Ollama | Zasebna lokalna usluga i lokalno instaliran model na `127.0.0.1:11434`. Deklarirani udaljeni modeli/modeli u oblaku odbijaju se. |
| LM Studio | Lokalni poslužitelj s lokalno učitanim modelom i podrškom za shemu JSON na `127.0.0.1:1234`. Ne omogućujte udaljeno usmjeravanje za lokalnu obradu. |
| llama.cpp | Lokalni `llama-server` i kompatibilni model s podrškom za shemu JSON na `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Pouzdana kompatibilna lokalna mapa tekstnog modela GenAI; odaberite `genai_config.json`. Izvođenje modela koristi priloženi prateći proces bez HTTP. |

Klijenti HTTP koriste samo loopback, ne koriste proxyje i ne slijede preusmjeravanja. MAKRIVO ne može pouzdano utvrditi poziva li kompatibilni lokalni poslužitelj interno udaljenu uslugu. Lokalni poslužitelji s autentifikacijom ne mogu se konfigurirati u ovoj verziji. Težine modela, Python i lokalni poslužitelji za izvođenje modela ne preuzimaju se niti instaliraju automatski.

Paketi ONNX trebaju `tokenizer.json` i valjan predložak razgovora. Podržani oblici predložaka u `tokenizer_config.json` jesu niz `chat_template`, rječnik s nizom `default` ili jednoznačan popis imenovanih/zadanih predložaka. Ako predložak nedostaje ili je null, kao zamjena koristi se `chat_template.jinja`. Prazni, dvosmisleni, preveliki predlošci i predlošci pogrešnog podatkovnog tipa odbijaju se. Proizvoljne datoteke ONNX, modeli vektorskih ugradnji/vida i paketi samo za GPU nisu zamjenjivi s kompatibilnim tekstnim modelima GenAI. Priložena verzija izvršnog okruženja GenAI jest 0.17.1, CPU. Koristite samo pouzdane modele; razdvajanje procesa nije sandbox.

Pomoćnik podržava odabrane programe bez argumenata, odabrane dokumente i web-mjesta te utišavanje mikrofona kada je izričito zatraženo. Ne stvara poruke, sastanke, proizvoljne skripte, proizvoljne klikove, preuzimanja niti prebacivanje slušalica. Cilj i nazivi resursa šalju se odabranom lokalnom izvršnom okruženju; putanje resursa i sadržaj datoteka ne dostavljaju se zasebno. Cilj koji korisnik unese može sam sadržavati osobne podatke. Mrežno ponašanje lokalnog izvršnog okruženja ostaje odgovornost korisnika.

Vremensko ograničenje zahtjeva ONNX jest 90 sekundi, uključujući učitavanje modela, uz najviše 3072 ulazna tokena i 1024 generirana tokena, dodatno ograničena kontekstom modela. Neispravan/odrezan JSON i nepoznati identifikatori resursa odbijaju se. Zahtjev završite otkazivanjem ili zatvaranjem. Test izvršnog okruženja s nasumičnim modelom dokazuje izvršavanje, a ne korisnu kvalitetu planiranja ili uštedu vremena. Lokalni zapisnici eksperimenata sadrže identifikatore događaja i vremena, a ne tekst cilja ili sadržaj dokumenata; ne šalju se automatski na poslužitelj.

Naknadna procjena malog modela Instruct nije proizvela nijedan valjan plan u četiri njemačka/engleska testa. Izvorno izvođenje modela radilo je, ali model nije zadovoljio stroge zahtjeve planiranja. To je negativan rezultat kvalitete, a ne preporuka modela ili dokazano koristan pomoćnik. Za točnu reviziju modela i ograničenja pogledajte datirano izvješće o testu lokalnog izvršnog okruženja.

## Uvoz, izvoz, sinkronizacija i sigurnosno kopiranje

Pregledajte pretpregled uvoza prije zamjene konfiguracije. Uvezene radnje početno su onemogućene, a prenosiva sinkronizacija uklanja dodjele ovisne o hardveru. Nakon pregleda ponovno dodijelite uređaje i promišljeno omogućite radnje. Pregledajte tekst/sadržaj radnji prije dijeljenja izvoza; izostavljanje lozinki ne znači da sav korisnički sadržaj radnji nema osjetljive podatke.

Sinkronizacija profila razmjenjuje datoteke u odabranoj mapi, kojom po želji upravlja postojeći klijent za oblak. MAKRIVO ne pruža račun u oblaku i ne pohranjuje vjerodajnice za oblak. Revizije sprječavaju neprimjetno prepisivanje. Uključivanje sadržaja radnji izričit je izbor. Izradite sigurnosnu kopiju konfiguracije prije zamjenskog uvoza. Zadana konfiguracija je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; povijesni naziv mape zadržan je radi kompatibilnosti. Zatvorite aplikaciju prije ručnog kopiranja/vraćanja. Pohranjena odobrenja SDK-a i vjerodajnice zaštićene operacijskim sustavom možda će trebati ponovno stvoriti na drugom računalu.

## Rješavanje problema

| Simptom | Što provjeriti |
| --- | --- |
| Prečac ništa ne radi | Aplikacija radi, dodjele nisu pauzirane, ispravan aktivni profil, spremljena/omogućena pločica, sukobi prečaca i dostupan cilj. |
| Ciljni prozor nije moguće aktivirati | Ručno ga aktivirajte. Ograničenja aktiviranja u prvom planu u Windows i razlike u ovlastima mogu blokirati automatizaciju. |
| Posebna tipka nije otkrivena | Podrška usluge proizvođača/SDK-a/modela; ako je moguće, postavite vlasničke kontrole tako da šalju podržanu standardnu tipku. |
| Druga tipkovnica također normalno upisuje tekst | To je očekivano uz Raw Input. Isključivo potiskivanje unosa nije implementirano za produkcijsku upotrebu. |
| Zvuk ili svjetlina nisu dostupni | Osvježite uređaje; provjerite odabrani identifikator, sesije krajnjih točaka, podršku WMI/DDC/CI i ograničenja priključne stanice/kabela. |
| OBS offline | Omogućen WebSocket v5, ispravna krajnja točka/lozinka, dostupna usluga i valjani nazivi scena/ulaza. |
| Automatizacija sučelja ne uspijeva | Prateći proces je uključen, jednoznačan valjan selektor, pristupačan omogućen cilj, podržana kontrola i dopuštenja. Nakon privremenog isteka vremena pokušajte ponovno tek nakon provjere cilja. |
| Mobilna ploča nije dostupna | Adresa LAN umjesto loopback, ista mreža, pokrenut poslužitelj, odobreni profili, otisak certifikata/povjerenje i ručna dopuštenja vatrozida. |
| Dodatak ne uspijeva | Integritet paketa, izričito povjerenje/omogućeno stanje, ovisnosti, kompatibilnost protokola i zapisnik izvršavanja. |
| AI zahtjev ne uspijeva | Ispravno odabrano lokalno izvršno okruženje, spremna usluga/model, podržana shema JSON, pouzdana kompatibilna mapa modela i ograničenja zahtjeva. |
| Raspored ili tekst izgledaju pogrešno | Razlikujte jezik sučelja od geometrije tipkovnice; promijenite veličinu prozora, provjerite skaliranje zaslona i prijavite točnu stranicu/jezik/međuverziju. |

Za koristan izvještaj o pogrešci navedite verziju Windows, međuverziju aplikacije/reviziju koda, jezik, skaliranje zaslona, model hardvera, minimalni slijed i relevantni zapisnik s uklonjenim osjetljivim podacima. Prije objavljivanja uklonite osobne putanje, sadržaj radnji i vjerodajnice.

## Izgradnja, testiranje i pakiranje

Koristite SDK čija je verzija određena u `global.json`. Iz korijenske mape repozitorija:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Integracijski testovi Windows mogu otvoriti izolirane prozore i privremeno konfigurirati/osloboditi podržane dodjele proizvođača. Pokrenite ih bez drugog upravljača koji koristi te dodjele. Zadane provjere svjetline/zvuka samo čitaju podatke; izričiti testovi promjene i vraćanja hardverskog stanja zasebni su. Simulirane protokole, iscrtane snimke zaslona i nasumične grafove izvođenja modela ne smije se predstavljati kao prihvatno testiranje stvarnog hardvera ili kvalitete modela.

Cjeloviti postupak izrade paketa okuplja prateće procese, sredstva mobilne ploče, primjer dodatka, izvršno okruženje ONNX i dostupne obavijesti o ovisnostima. Sačuvajte njegove podmape. Uspješna izgradnja nije potpisani instalacijski program niti pravno odobrenje izdavanja. Izvorni kod i međuverzije mogu se razvijati zasebno; usporedite stanje dokumentacije i reviziju koda s instaliranom verzijom.

## Licenciranje, privatnost i status izdanja

Predviđeni komercijalni model jest jednokratna kupnja s trajnom upotrebom kupljene glavne verzije; konačna cijena, dopušteni broj uređaja i uvjeti nadogradnje moraju se utvrditi u stvarnim uvjetima prodaje. Ovaj priručnik ne uspostavlja produkcijski sustav kupnje/aktivacije. Za svakodnevno upravljanje nije potreban račun u oblaku.

Ne distribuirajte vlasničke binarne datoteke SDK-a bez dokazanog dopuštenja. Sačuvajte licence i obavijesti trećih strana za svaku isporučenu ovisnost, uključujući pakete .NET i ONNX. Težine modela imaju vlastite uvjete. Datirana revizija licenci prethodi nekim novim ovisnostima i ne predstavlja odobrenje za te dodatke. Javna dostupnost repozitorija sama po sebi ne daje opću licencu otvorenog koda.

Stvarni hardver proizvođača, rad telefona/tableta putem LAN, prijelazi između različitih monitora/DPI, dugotrajna upotreba, cjelovit jezični pregled, podrška isključivog upravljačkog programa i korisna kvaliteta AI planiranja ostaju predmet zasebnog prihvatnog testiranja. Nikada ne zaključujte da je nešto odobreno samo zato što funkcija postoji.

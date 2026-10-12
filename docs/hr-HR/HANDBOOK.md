<!-- generated-doc locale=hr-HR source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../HANDBOOK.md)

# MAKRIVO — Korisnički priručnik

Proizvođač: **Aivana**. Dokumentarni snapshot: **2026-10-12**. Ovaj priručnik opisuje trenutnu implementaciju radnog drveta. eksperimentalne integracije i hardver-odvisne funkcije su izričito identificirane. povijesni izvještaji opisuju njihovu datiranu snimku, a ne jamstvo za novu konstrukciju.

## Dokumentacija i jezici

Koristite izbornik jezika za dokumentaciju kako biste otvorili cjeloviti priručnik, popis značajki, rješavanje problema, razvojne beleške, istraživanja i povijesne testne izvješća na bilo kojem od 27 jezika primjene. Stvoreni prijevodi su strojno pomoćni i imaju strukturne provjere; nisu svi primili pregled rodnog govornika. Tehničke identifikacije i izvršljivi primjeri ostaju nepromijenjeni. Originalni izvor ostaje dostupan za usporedbu. Prevedena analiza dozvola je informativna i ne zamjenjuje originalne pravne uvjete treće strane ili konačnu EULA.

## Ugradnja i prvi pokret

Početni cilj je Windows 11 x64. zadržite sve datoteke i poddirektorije distribucije zajedno. `MAKRIVO.exe`. Samostalni paketi uključuju svoj .NET radni vijek; paketi ovisni o okvirima zahtijevaju odgovarajući .NET 10 Desktop i ASP.NET radovi. ONNX i spremnici moraju biti uključeni za njihove značajke.

1. Počnite MAKRIVO i odaberite jezik interfejsa u Sastavima. Automatski slijedi Windows Ključna ploča je odvojeno postavljanje.
2. Izaberite profil, a zatim dodajte list kroz katalog pretraživanja. Dajte mu prepoznatljivo ime i odaberite njegove akcije.
3. Pregledajte korake i dodijelite kratko, ako je potrebno. Izbjegavajte kombinacije koje su već koristili Windows ili vaša ciljna aplikacija.
4. Sačuvaj projekt. Neuspješne promjene nisu sačuvana konfiguracija.
5. Test s neškodljivim djelovanjem, kao što je otvaranje dokumenta, i provjeriti povratne informacije.

Zatvaranje glavnog prozora obično ostavlja aplikaciju u području obavijesti. Dvostruko kliknite na njegovu ikonu za otvaranje ili koristite izbornik za izlazak. Početak s Windows je opcionalan i na početku je isključen. Kontrola pauze suspendira vezu; ikona prikazuje znak pauze. Otkazati radne makro preko UI, tray ili konfiguriranog otkazivanja kratko; privremeno je `Ctrl+Alt+Escape`.

## Tile, akcije i zapovijedna paleta

Tile pripadaju profilima i mogu sadržavati više naručenih koraka. Redatelj podržava Save/Discard, Preview, undo/redo i korak reordering. Katalog pruža prednastave za programe, dokumente, URL-ove, tekst, klipboard, kratice, medije, audio, prozore, sustavne akcije i podržane integracije. Prethodni članakPredstavci koji zahtijevaju parametre otvaraju se kao nacrt

Otvorite paletu s `Ctrl+K` dok MAKRIVO ima fokus. Pronađite sačuvane akcije, predodređene, pretvaranje profila i alate. Izaberite ključeve; Upišite aktivira; `Ctrl+Enter` uređuje podržani element; Izbjegavajte se. Neke spremne zapovijedi izvršavaju se odmah, dok konfigurirane akcije zahtijevaju pregled. Odvojeno globalno skraćivanje može se konfigurirati; nove konfiguracije koriste `Ctrl+Shift+Space`.

Tekst i klipboard koraka podrška `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Datum formatacije je `yyyy-MM-dd`; Vrijeme formiranja je `HH:mm`. Neznane promjenjive ostaju nepromijenjene. čitanje klipova događa se samo kada je zatraženo. akcija klipova piše tekst bez automatskog prilagodbe.

Akcije pokrenute iz UI-a mogu biti usmjerene na posljednju vanjsku aplikaciju. Windows blokirajte aktiviranje, ručno aktivirajte cilj i povući. uspješna isporuka ključa ne dokazuje da je cilj završio svoj zadatak.

## Profil i radno mjesto

Profili mogu odgovarati nazivu procesa i dijelu naslova prozora. Kada se oboje dogovore, moraju se međusobno međusobiti. Profilni red rješava natjecateljske utakmice; privremeni profil je pad. Automatski izbor omogućuje izričito. Lock Profile sprječava automatske promjene za trenutnu sjednicu.

Otvoreno radno mjesto iz alata ili palete. Odaberite programe, dokumente, web stranice, cilj audio/display ili scenu OBS. Pomoćnik priprema radno mjesto, sastanke, prijenos, istraživanje ili projekte. Provjerite stvorene korake prije uštede. Uređaj ne izvršava pripremljene akcije.

## Makro, uvjeti i snimanje

Macros izvršava svoje korake u redoslijedu i podržava čekanja, ponavljanja i otkazivanja. Opća granica ponavljanja je 100. Koristite Pregled prije pokretanja akcija koje se upišu u drugu aplikaciju ili promijeniti stanje sustava.

Čekanje za Ciljna prozora prihvaća proces, naslovni podrazumevanje i vremenski izlazak od 1 do 600,000 ms. Potrebno je barem jedan filter. Prikladno vidljivo prozor mora odgovoriti i postati predokret prije sljedećih koraka. Pripremljeno okno ne dokazuje da je dokument ili usluga završena.

Redatelj radnog toka dodaje uvjete za datoteku-postoje, procesno-proizvodnju i naslov prozora. Lažni uvjet skupa korak i može preći dodatne korake naprijed. Odbijanje uvjeta za izražavanje alternativne grane. Odaberite stop-on-error ili continu-on-err namjerno; nastavak može ostaviti kasnije korake bez očekivanih uvjeta.

Akcijski zapisnik uhvaća kratke riječi i čeka, a ne uobičajeno pisanje. Podržava izmjenjivače i funkcijske ključeve. Sljedeći članakSvijet prestaje snimati. Pregledajte popis i usvojite ga kao neispravan nacrt, a zatim izričito sačuvajte. UI klik snimanje je odvojeno alat opisano ispod.

## Ključne riječi, gestovi i slojevi

Na tipkovnici se nalaze popisi otkrivenih sučelja i referentnih rasporeda. Jedan fizički tipkovnica može izložiti nekoliko sučelja. Odaberite ispravan jezik i geometrij; samo ime proizvoda ne identificira ISO ili ANSI raspored.

Standardni globalni hotkeys primjenjuju se preko tipkovnica. Raw Input poveznice promatraju određeni uređaj i skeniranje koda, uključujući podržane funkcije i medijske ključeve. **Raw Input Ne potiskuje uobičajeno Windows Ekskluzivna druga tipkovnica nije spremna za proizvodnju.** Potpisan, licenciran i testiran filter-driver podrška je još uvijek potrebna; nijedan nije instaliran od strane MAKRIVO.

Podržani posebni ključni priključci mogu koristiti neposredni pritisak, tap, držanje ili dvostruki tap. ključi za pokrivanje privremeno odabiru još jedan mapiranje i vraćaju osnovni sloj na oslobađanju; ključi za odabir sloja ne rade vlastiti makro. 150 u 5,000 MS; dvostruki prozor od 150 u 1,000 MS. Ovi mape su različiti od uobičajenih globalnih hotkeya.

Poseban ključ čarobnjak otkriva kompatibilne ključeve bez izvršenja njihovih dodijeljenih akcija tijekom uhvaćanja. Dijagnoza objašnjava nedostupne uređaje, prekidane veze, nepravilnosti profila i sukobe. Površina prikazuje odabrano raspored i vezu; njezin privremeni skraćivanje je `Ctrl+Alt+F1`.

| integracija | Uvjeti i ograničenja |
| --- | --- |
| Corsair | Running iCUE, SDK pristup i kompatibilan model. G1–G6 i podržane funkcije RGB; vlastiti Fn, kotač, blok i profil kontroleri općenito nisu izloženi G-key API. |
| Logitech G | Originalna lokalna x64 G-key/LED SDK knjižnica zahtijeva izričitu odobrenje. G ključ događanja ne jedinstveno identifikuju nekoliko identičnih fizičkih tipkovnica. |
| Razer | Chroma RGB i standardni ulaz; mapna vlastita kontrola u Synapse gdje je potrebno. |
| SteelSeries | GameSense RGB i standardna ulaznica; kontrolu vlastitosti karte u GG gdje je potrebno. |
| Wooting | Kompatibilan originalni x64 SDK za analogne vrijednosti i granice; normalni HID ulaz ostaje aktivan. |
| Ostali ključevi | Standardni hotkeys ili podržani fizički skener-koda vezanosti. Fn i vlastiti kontrolori ovise o modelu i konfiguraciji dobavljača. |

Istraživanje proizvođača opisuje dokumentirane sposobnosti, ne implementiraju ili testiraju kompatibilnost sa svakim navedenim uređajem. Novi adapteri još uvijek trebaju stvarnu hardversku prihvatljivost. Ne preuzimajte SDK binare iz nesigurnih izvora; prava distribucije SDK-a su odvojena od tehničke kompatibilnosti.

## Zvuk, prikaz i OBS

Audio korake postavljaju volumen od 0 do 100 posto i mute/unmute/toggle podržan izlazak ili mikrofon krajnje točke. Izaberite određenu krajnju točku ili koristite podrazumevano Windows. Izgubljeni cilj proizvodi pogrešku umjesto da tiho kontrolira drugi uređaj. Obnova uređaja nakon ponovnog povezivanja. Kontrola završetka točke nije tvrdnja o neovisnoj podršci za aplikaciju.

Prikazuje akcije povećanje/smanjenje svjetlosti za 10 postotak bodova ili postaviti vrijednost od 0 do 100. Unutarnji zasloni koriste poduprane WMI sučelje; vanjski monitori zahtijevaju podršku DDC/CI i obično postavljaju svoj monitor-menu. Dokovi, adapteri i kabli mogu blokirati kontrolu. Odaberite sve podržane zaslone ili jedan monitor. Povratak može promijeniti dostupne identitete; pregledati sačuvani cilj. Izgubljeni čitanja pokazuju se kao nedostupni umjesto mjerenih vrijednosti.

OBS zahtijeva WebSocket v5 omogućeno u OBS Tools. Konfigurirajte krajnju točku, provjerite povezanost i nazovite scenu/izvod prije štednje. Podržane akcije uključuju prijelaz scene, protok/zapis start/stop/toggle, snimanje pauze/resume i ulazni mute. Lozinke se mogu pohraniti u Windows Credential Manager i isključeni su iz konfiguracijskih izvoza. Uspješna zapovijed OBS ne uspostavlja svaki učinak prijenosa.

## Povijest provjere i izvršenja

Pokušajte provjeriti rezultate za odgovarajuće korake. Aplikacija razlikuje preglede, potvrđene učinke, neuspjehe, otkazivanje i učinke koji se ne mogu provjeriti. Procesno postojanje nije spremnost za primjenu. Relativne promjene, skraćivanja i tekstni ulaz ne mogu dobiti zahtjev za uspjehom. Provjerite log aktivnosti i povijest provjere kada se akcija ne dogodi. Pogreške u konfiguraciji trebaju biti ispravljene prije uklanjanja cijelog makra.

## Automatizacija i snimanje klikova

Dodajte korak za automatizaciju UI-a, odaberite operaciju i uhvatite cilj ispod pokazatelja nakon što se prikazuje kašnjenje. samo snimanje ne radi ništa. provjerite selektor i zahtijevate jedinstveni meč. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` i `WaitState`. Polja lozinke, isključeni kontrolori i nejasni selektorovi su odbijeni.

UI automatizacija radi u ograničenom procesu. To ovisi o pristupačnosti i dopuštenjima ciljne aplikacije Windows. Klikni zapisnik uhvaća podržane akcije i pauze; ne zabilježi opći tekst. Pregledati i potvrditi svoj nacrt prije nego što ga usvojite. Promjene UI-a u vanjskoj aplikaciji mogu otkazati selektor. Procesna izolacija ograničava nesreće i otkazivanje, ali nije sigurnosna pećnica.

## Plugini

Otvorite proširenja kako biste provjerili paket prije uvoza. Struktura paketa, manifesti i hashovi su provjereni. Uvoz počinje s invaliditetom. Pročitajte deklarirane akcije i izričito povjerujte izdavaču prije nego što ga omogućite. Plugins rade s vašim korisničkim dozvolama u odvojenom procesu; taj je proces **ne pješčana kutija**. Samo instalirati pakete koje vjerujete.

Upravitelj podržava izvoz, deaktiviranje, uklanjanje i obnavljanje prethodne verzije. Izgubljena ovisnost blokira poziv. Primjer paketa pruža brojanje i lokalno-države akcije. Podržana shema je dokumentirani JSON-Schema, a ne svaka moguća funkcija JSON-Šema. Ne postoji javni plugin tržište.

## Mobilni panel

1. Otvorite mobilni panel, odaberite lokalni IP i izričito odaberite profile koje će se prikazati. Podrazumevana adresa je dostupna samo na samom PC-u; odaberite odgovarajuću adresu LAN-a za telefon ili tablet.
2. Počnite s panelom i pročitajte njegov HTTPS naslov, certifikacijski otisak prstiju i jednodnevni kod s šestigramima.
3. Na uređaju u istoj pouzdanoj mreži, otvorite adresu. Usporedite SHA-256 otisak prstiju kroz pouzdan kanal prije nego što se povjerujete lokalnom certifikatu. Aplikacija ne instalira povjerljiv certifikat automatski.
4. Koristite prikazani kod i testirajte jednu neopasnu traku. Za pristup LAN-u može biti potrebna ručna dozvola Windows. Nemojte stavljati port na internet.
5. Zaustavite panel nakon završetka ili povući klijenta putem aplikacije.

Privatni server ključi su zaštićeni Windows DPAPI; klijenti se pohranjuju kao hashovi. Promjena IP-a ili obnova certifikata može zahtijevati obnovu povjerenja. Panel prikazuje stanje djelovanja i provjere, a ne svaki mogući živ senzor. Browserski raspored i lokalni HTTPS provjeri ne zamjenjuju stvarno prihvaćanje telefona/tableta.

## Lokalni asistent sastanka

Ovaj opcionalni eksperimentalni asistent je isključen podrazumevanjem. Otvorite ga iz alata, izričito ga uključite, odaberite radno vrijeme/model i dodajte samo resurse koje želite da razmotrite. Postavite cilj, stvorite nacrt, provjerite nerazrijeđene zahtjeve i prenesite rezultat na normalni urednik. Ništa se ne izvršava automatski; spašavanje i vožnja ostaju izričite akcije. Ne postoji automatski pad vremena.

| Vrijeme vožnje | Uredi |
| --- | --- |
| Ollama | Odvojena lokalna usluga i lokalno instalirani model na `127.0.0.1:11434`. Izjavljeni daljinski / oblak modeli su odbijeni. |
| LM Studio | Lokalni poslužitelj s lokalnim modelom i JSON-schematom na `127.0.0.1:1234`. Nemojte omogućiti daljinsko usmjeravanje za lokalnu obradu. |
| llama.cpp | Lokalni `llama-server` i kompatibilni model s JSON-schemom na `127.0.0.1:8080`. |
| ONNX Vrijeme rada GenAI CPU | Povjereno kompatibilna lokalna GenAI tekstna modela; odaberite `genai_config.json`. Inference koristi povezani proces bez HTTP-a. |

HTTP klijenti koriste samo loopback, ne koriste proxy i ne slijede preusmjeravanja. MAKRIVO ne može pouzdano utvrditi hoće li kompatibilni lokalni server interno pozivati na daljinsku uslugu. Autentificirani lokalni serverovi nisu konfigurirani u ovoj verziji. Modelne težine, Python i lokalni inferencijski serveri se ne preuzimaju ili instaliraju automatski.

ONNX paketi zahtijevaju `tokenizer.json` i valjanu šablonu razgovora. Podržani obrasci predloga u `tokenizer_config.json` su red `chat_template`, rječnik s redom `default` ili nesvjesni naziv / podrazumevani popis predloga. Ako je šablona nestala ili nula, `chat_template.jinja` se koristi kao pad. Pravi, nesigurni, prekomjerni ili nepravilno tipirani predmeti su odbijeni. Arbitrijski ONNX datoteke, modeli ugradnje/vidljivosti i paketi GPU-a nisu razmjenjivi s kompatibilnim GenAI tekstovnim modelima. Vrijeme rada GenAI je 0.17.1, CPU. Koristite samo pouzdane modele; procesno razdvajanje nije sandbox.

Pomoćnik podržava odabrane programe bez argumenata, odabrane dokumente i web stranice, a mikrofon mute kada je izričito zatraženo. Ne stvara poruke, sastanke, arbitrarne skriptove, besplatne klikove, preuzimanja ili prijenos slušalica. Cilj i imena resursa šalju se na odabrano lokalno vrijeme rada; resursi i sadržaj datoteke nisu dodijeljeni pojedinačno. Cilj koji je postavio korisnik može sam sadržavati osobne podatke. Samostalno ponašanje lokalnog radnog vremena ostaje odgovornost korisnika.

I to je ONNX Zahtjev za proračun 90 Slijedeći članakSlijedeći Članak Model, do 3,072 Brzih tokenova i 1,024 generirani tokeni, također ograničeni kontekstom modela. Zlonamjerni/trunkirani JSON i nepoznati ID resursa su odbijeni. otkazati ili blizu završetka zahtjeva. slučajni model test rada utvrđuje izvršenje, ne korisno planiranje kvalitete ili uštede vremena. Lokalni eksperiment logovi sadrže identifikacije događaja i vremenske uvjete, ne cilj tekst ili sadržaj dokumenta; oni se ne automatski prenose.

Kasnije je mala ocjena modela Instruct proizvedena od nule valjanih planova u četiri njemačka/engleska ispitivanja. Native inference je radio, ali model je propustio strogi ugovor o planiranju. To je negativan rezultat kvalitete, a ne preporučeni model ili dokazano korisni asistent. Pogledajte datirani lokalni testni izvještaj za točnu reviziju modela i ograničenja.

## Uvoz, izvoz, sinhronizacija i kopiranje

Provjerite uvozne predviđanja prije zamjene konfiguracije. Uvezene akcije počinju isključivati, a prenosiva sinhronizacija uklanja hardverske veznice. Ponovno dodijeliti uređaje i omogućiti djelovanje namjerno nakon inspekcije. Pregled sadržaja teksta/akcije prije dijeljenja izvoza; isključivanje lozinke ne čini sve korisnički stvorene sadržaje djelovanja neosjetljivim.

Sinkronizacija profila razmjenjuje datoteke u odabranom dosjeu, opcionalno upravlja vaš postojeći klijenti u oblaku. MAKRIVO ne pruža račun u oblaku i ne pohranjuje nikakve vjerodostojnosti u oblaku Revizije sprječavaju tihe prepisivanje. Uključivanje sadržaja djelovanja je izričita odluka. Preporučljivo je vratiti konfiguraciju prije uvoza zamjene. Podrazumevana konfiguracija je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; povijesno ime direktora zadržava se za kompatibilnost. Zatvorite aplikaciju prije ručnog kopiranja/obnavljanja. Shrani SDK odobrenja i OS-zaštićene autentifikacije mogu se morati obnoviti na drugom PC-u.

## Rješavanje problema

| Simptomi | Što provjeriti |
| --- | --- |
| Kratko ne radi | Aplikacija je u tijeku, veze nisu odložene, ispravan aktivni profil, sačuvani/aktivirani tile, kratki sukobi i dostupni cilj. |
| Ciljno okno ne može biti aktivirano | Rukovno ga aktivirajte. Windows Pregranične ograničenja i privilegijske razlike mogu blokirati automatizaciju. |
| Poseban ključ nije otkrivena | Podrška za prodavatelj/SDK/model; konfigurirajte vlastite kontrolore kako biste, ako je moguće, emitirali podržani standardni ključ. |
| Drugi tip tip je također uobičajen | Očekuje se s Raw Input. Ekskluzivno uklanjanje se ne provodi za proizvodnju. |
| Audio ili svjetlost nedostupna | Obnavljajte uređaje; provjerite odabrane identitete, završne sjednice, WMI/DDC/CI podršku i ograničenja dok/kabel. |
| OBS offline | WebSocket v5 omogućena, ispravna krajnja točka/pasovnica, dosegljiva usluga i valjano ime scene/izvoda. |
| Neuspjeh automatizacije | Uključeni su hostovi, jedinstveni valjan selektor, pristupačni aktivirani cilj, podržani kontrolori i dozvole. Povratak prijelaznog vremena tek nakon preispitivanja cilja. |
| Mobilni panel nepristupa | LAN umjesto adrese za povratak, ista mreža, server radi, odobrenih profila, certifikacije otiska prstiju / povjerenja i ručne dozvole za vatrogasne zidove. |
| Plugin neuspjeha | Integritet paketa, izričito povjerenje / dopušteno stanje, ovisnosti, kompatibilnost protokola i log izvršenja. |
| Zahtjev neuspje | Pravilno odabrano lokalno vrijeme rada, spremna usluga/model, podržana JSON shema, pouzdana kompatibilna modela i granice zahtjeva. |
| Layout ili tekst izgleda pogrešno | Odvojite jezik UI-a od geometrije tipkovnice; preusmjerite prozor, provjerite razinu prikaza i prijavite točnu stranicu / jezik / gradnju. |

Za korisnu izvješću o pogrešcima uključuju se verzija Windows, aplikacija za izgradnju/obavijest, jezik, skaliranje zaslona, hardverski model, minimalna sekvencija i relevantni redigirani log. Uklonite osobne staze, sadržaj djelovanja i potvrde prije objavljivanja.

## Izgradnja, testiranje i pakiranje

Koristite SDK prikazan u `global.json`. Od korijena:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integracijski testovi mogu otvoriti izolirane prozore i privremeno konfigurirati/izlažiti podržane veznice. Izađite ih bez drugog kontrolora koji posjeduje te obveze. Podrazumevani testovi svjetlosti/audio čitaju se samo; eksplisitni testovi za okrugli put su odvojeni. Mock protokoli, snimke zaslona i slučajne grafike ne bi trebali biti prikazani kao pravi hardver/model-kvaliteta prihvaćanja.

Cjeloviti izdavač prikuplja hostove, mobilne imovine, primjerni plugin, radno vrijeme ONNX i dostupne obavijesti o ovisnosti. Držite svoje poddirektorje. Uspješna izgradnja nije potpisani instalator ili odobrenje za izdavanje. Izvor i građevine mogu se razvijati odvojeno; usporedite snimke dokumentacije i obavezite se s instaliranom verzijom.

## Licencija, privatnost i status oslobađanja

Načrtani komercijalni model je jednostavna kupnja s stalnim korištenjem kupljene glavne verzije; konačna cijena, dopuštenje uređaja i uvjeti za nadogradnju moraju biti postavljeni u stvarnim uvjetima prodaje. Proizvodni sustav kupnje/aktiviranja nije uspostavljen ovim rukovodstvom. Za svakodnevnu kontrolu nema potrebe za računom u oblaku.

Nemojte distribuirati vlastite SDK binare bez dokazane dozvole. Održavajte licencu i obavijesti trećih strana za svaku isporučenu ovisnost, uključujući .NET i ONNX pakete. Modeli imaju svoje uvjete. Datum licencije revizija predlaže neke nove ovisnosti i nije otpuštanje za te dodatke. Budući da je ured javni, on sam ne dodjeljuje opću licenciju otvorenog izvora.

Realna oprema dobavljača, operacija telefona/tableta LAN-a, miješani monitor/DPI prijelaz, dugotrajna upotreba, potpuni jezični pregled, ekskluzivna podrška za vozače i korisna kvaliteta AI planiranja ostaju odvojeno prihvaćanje. Nikada ne dobivam odobrenje samo od prisutnosti.

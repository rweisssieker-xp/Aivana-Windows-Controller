<!-- generated-doc locale=hr-HR source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../README.md)

# MAKRIVO

[Potpuna dokumentacija na svim 27 jezicima](README.md) · [Cjelovita korisnička knjiga](HANDBOOK.md)

**Naime, Aivana.** [Trenutačni inventari i ograničenja za izdavanje](FEATURES.md) · [Izvješće o proširenju](testing/2026-10-12-six-extensions.md) · [Lokalni asistent prototypa](testing/2026-10-12-local-meeting-assistant.md)

** Engleski (US) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalni kontrolor Windows s Stream Deck stilom ploča, profila, makra i detekcije tipkovnice. Interfejs podržava 27 jezike. Za svakodnevnu upotrebu nije potrebno račun ili povezivanje u oblaku.

[Dokumentacija](README.md) · [Verifikacija](VERIFICATION.md) · [Izvješće o testiranju](testing/2026-10-09-usp-release.md)

## Početak

Nakon objavljivanja, zahtjev je na `artifacts/win-x64/MAKRIVO.exe`. Držite cijeli izlazni folder zajedno; možete ga kopirati negdje drugdje. Windows 11 x64 je početni cilj. Izgrađeni predmeti su isključeni iz Git-a; izvorni pregled ne uključuje predgrađenu izvršitelju.

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
2. Nastavi svoje ime, ikonu, boju i kraticu na tipkovnici. Kombinacije Ctrl/Alt/Shift smanjuju slučajnu aktivaciju tijekom pisanja.
3. Dodajte jedan ili više koraka: aplikacije, datoteke, web stranice, tekst, kratice, medije, volumen, pretvaranje profila ili kašnjenja.
4. Spasite se. Promjene ostaju na rasporedu sve dok se ne spašava; diskard vraća sačuvanu verziju. Gumb za igranje i njegov hotkey koriste istu stazu izvršenja.
5. Ponovno naručiti ploče i makro korake s drag-and-drop ili strela gumb. Šabloni pojednostavljuju postavke. Makro može ponoviti do 100 puta. Otkažite kroz interfejs, sustav tray meniju ili Ctrl+Alt+Escape po podrazumijevanju.

Profili specifični za aplikacije koriste naziv procesa bez cijelog puta. Profile narudžbe prekida veze; privremeni profil je pad. Automatski izbor profila mora biti uključen u postavke.

Zatvaranje glavnog prozora zadržava aplikaciju u sustavu podrazumevano. Dvostruko kliknite na njegovu ikonu za otvaranje; koristite izbornik za izlazak. Startup s Windows je opcionalan i privremeno isključen. Hotkeys se može odmoriti središnje.

## Akcije i zapovijed paleta

Dodavanje akcije otvara pretraživački katalog s 90 predviđanjima u osam kategorija. Svaki od njih ima opis i predviđanje. Makro urednik koristi isti katalog za dodavanje ili zamjenu koraka. Odabir prednastave u ovom radnom toku urednika stvara nacrt bez izvršenja.

Paleta zapovijedi se otvara s naslova ili s Ctrl+K unutar aplikacije. To traži sačuvane akcije preko profila, katalog predmeta, pretvaranje profila, navigacija, pauza/resuma, makro otkazivanje, i uvoz/eksport. Filter sačuvane akcije, funkcije kataloga ili zapovijedi aplikacija. Izaberite ključeve, uključite aktivira, Ctrl+Enter uređuje uređiv ulaz, a Escape se zatvara. Najprije se pojavljuju omiljeni. Pripremljeni za upotrebu zapovijedi kataloga mogu se odmah pokrenuti; prednastavi koji zahtijevaju postavljanje otvaranje rasporeda. Sastav može definirati globalnu paletu. Nova konfiguracija koristi Ctrl+Shift+Space; postojeće konfiguracije ne dobivaju novi globalni skraćenik automatski.

Audio akcije postavljaju volumen točno od 0 do 100 posto i mute, nemute, ili toggle proizvodnje i mikrofon uređaje. Izaberite određenu krajnju točku ili je ostavite isključivo za default Windows. Nedostupan sačuvani kraj proizlazi iz pogreške umjesto kontrole drugog uređaja. Refrigerator preuzima povezane uređaje.

OBS Studio koristi WebSocket v5: start/stop/toggle/pause/resume snimanje; start/stop /toggle prijenos; promijeniti scene; toggle ulaz mute. Uključite WebSocket server pod Tools u OBS i postavite njegov kraj u uredniku. Provjerite povezivanje i preuzimanje imena čita OBS verziju, scene i ulaznice bez pokretanja snimanja ili protoka. Izaberite imena s popisa ili ih unesite ručno. Lozinke se mogu sačuvati u Windows Credential Manager i isključeni su iz izvođenih konfiguracija. Neprimjereno OBS poveznice prijavljuju pogrešku.

Profili mogu odgovarati nazivu procesa i dijelu naslova prozora. Oba uvjeta moraju biti u skladu. Lock Profile sprječava automatski prijelaz za sjednicu. Uslovi snimanja iz Foreground koriste posljednju usmjerenu vanjsku aplikaciju.

Podrška za tekst i klipboard akcije `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, i `{{user}}`. Datum korištenja `yyyy-MM-dd`; Vrijeme korištenja `HH:mm`. Neznani gradovi ostaju nepromijenjeni. `{{profile}}` Identificirajte pokrenut profil akcije; `{{process}}` Identificira vanjski kontekst aplikacije. sadržaj klipa se čita samo kada se koristi taj posjednik. akcija klipa piše tekst, ali ne automatski.

Akcije prozora smanjuju, maksimalno, obnoviti ili urediti prozor na lijevoj/pravoj strani radnog područja monitora. Sustav djelovanja blokira Windows. Takve akcije se provode samo kada su izričito pozvane.

Pregled prikazuje korake, ponavljanja i promjenjive bez izvršenja. Undo i Redo zadržavaju do 100 stanja; tekstne kutije zadržavaju native Ctrl+Z editing. Zapišite omiljene u uredniku ili tile meniju i filtrirajte ih na ploči. Izvješće o izvršenju primjenjuje se samo na trenutačnu sjednicu.

Tekst, kratice i akcije prozora pokrenute iz sučelja aktiviraju posljednje vanjsko ciljne prozore. U tom kontekstu prikazuje se cilj. Ako je cilj zatvoren ili Windows odbija aktivaciju, izvršenje se zaustavlja s porukom.

## Ključna pomoć i povratak

Specijalni ključ čarobnjaka nalazi se na stranici za tipkovnicu i u paleti. To je popis G-knjiga prijavljena od iCUE. Detect Key Press privremeno ih uhvaća bez izvršenja njihovih dodijeljenih akcija. Normalne vezane povratak nakon otkrivanja, otkazivanja ili zatvaranja. Funkcija kataloga i odabran ključ otvoreni kao neispravan nacrt; provjerene su duplikate.

Keyboard Overlay prikazuje odabranu raspored referencije i aktivne veze. Držite Ctrl+Alt+F1 ili ga uklonite s palete ili stranice za tipkovnicu. Ne uzima fokus i dopušta da mišići prođu kroz. Profili i obvezujuće promjene ažuriraju dok su vidljivi; naglašavaju se uobičajeni Windows ključni pritisci. Proizvođač-specifični kontrolori ne mogu pružiti ekvivalentni ključni signal Windows.

Izvješće o izvršenju prikazuje čekanje, korak / ponavljanje napretka, završetak, otkazivanje ili neuspjeh bez fokusiranja ili izloženosti uvrštenog teksta. Commands Executed potvrđuje izvršavanje zapovijedi, a ne ne neovisno traženo stanje u aplikaciji treće strane. Podrobnosti o pogreškom pojavljuju se u dnevniku aktivnosti.

## Uređaji i Corsair

Corsair G1–G6 Može se primjenjivati izravno putem službenog iCUE SDK. Izaberite uređaj i ključ u uredniku akcije i sačuvajte, ili pripremite nacrt iz K100 Veza se primjenjuje na odabrano uređaj i aktivni profil; pauza ih također isključuje. iCUE mora biti u tijeku i omogućiti pristup. fn, iCUE Voda, profil i blokovi ne mogu se kartografirati kroz ovu API ključ.

Stranica uređaja popisuje Windows tipkovnice i periodično se osvježi i kada se uređaji povezuju. Fizička tipkovnica može izložiti više HID sučelja. Standardni globalni hotkeys rade među proizvođačima.

Corsair detekcija koristi dostupne informacije o uređaju. RGB kontrolu zahtijeva kompatibilan hardver, radi iCUE i službeni iCUE SDK; aplikacija izvješćuje o stvarnoj dostupnosti. Ostali proizvođači trenutno dobivaju detekciju i standardne akcije. Izvan integracije Corsair G ključ, vlastiti kontrolori rade kada ih Windows primaju kao redoviti ulaz. Aplikacija ne instalira tipkovnicu.

## jezike

Nastave nude 27 jezika: njemački, engleski, francuski, španjolski, portugalski, nizozemski, češki, slovački, mađarski, bugarski, grčki, danski, švedski, norveški, finski, estonski, latvijski, litvanski, slovenski, hrvatski, ukrajinski, ruski, turski i srpski u latinskom scenariju. Automatski slijedi jezik prikazivanja Windows. Prevođenje je lokalizirano. Raspored, hotkeys i korisnički nazivi su neovisni o jeziku sučelja.

Cjelovita dokumentacija je dostupna na svim 27 jezicima prijave putem [Indeks jezika](README.md)Uspjeh Prevođenje je strojno pomoćno, s originalnim izvornim poveznicama i strukturnim kontrolama; pregled rodnog govornika je odvojen. Aplikacija također prihvaća regionalne kode `en-US`, `de-DE`, `fr-FR` i `it-IT` te koristi njihove brojeve. Postojeći jezični kodovi ostaju kompatibilni.

## Layouts i istraživanje proizvođača

Tablicni pogled pruža potpunu veličinu i bezuvjetne reference ISO-DE/ANSI-US plus Corsair K100 RGB raspored. Geometrija predstavlja pozicije, široke ključeve i ISO Enter. Standardni rasporedi su generski referencije. K100 rasporedi slijede dokumentirane kontrole bez zahtjeva za milimetarne točne dimenzije. Odaberite ispravan jezik/geometrijski varijanta: ime modela ili USB VID/PID ne određuje određenu varijantu.

Mapping rješava fizičke skeniranje kodova pomoću aktivnog rasporeda Windows. Radni tok priprema nacrt i zadrža spas / sukob provjere. Standardne veznice su globalni Windows hotkeys, ne izolacija na jednoj povezanoj tipkovnici. Corsair G1 – G6 upotreba uređaj-specifična iCUE integracija. Ostali vlastiti kontrolori zahtijevaju adapter ili proizvođač-konfiguracije. Glavni Enter i brojni tipkovnica Enter dijele virtualni ključ za globalne hotkeys.

I to je [Proizvođač istraživanja](research/tastaturhersteller-sonderfunktionen.md) i [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokriva 74 skupine brendova/proizvođača, razlikujući konfiguracije, SDK-ove za ulazak i API-ove za osvjetljenje. [Layout dokazi](research/keyboard-layout-evidence.md) Model, SKU i modula razlike. Istraživanje ne znači da se primjenjuje podrška za hardver.

## podaci

Konfiguracija se pohranjuje u lokalnim podacima. Uvoz/izvoz koristi JSON. Uvoz predviđa konfiguraciju, zamjenjuje trenutnu konfiguraciju nakon potvrde i onemogućuje uvezene veze sve dok se ručno ne aktivira. Tekstno djelovanje se ne prikuplja kao ključni log. Lozinke za upravljanje povjerenjem odvojene su od izvođenih profila.

## Verifikacija

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows provjere mogu provjeriti hardver i privremeno konfigurirati/izdvojiti podržane iCUE veze. Učinite ih bez drugog kontrolora pomoću tih veznica. Audio endpoint provjere ne mijenjaju živu količinu ili mute postavke.

Native input ne može prelaziti Windows privilegij. Uobičajeno pokrenut kontrolor ne može poslati ulaz u aplikaciju administratora. Rezervirani ili zauzeti hotkeji izgledaju kao sukobi.

I to je [Indeks dokumentacije](README.md) Povezivanje povijesnih dizajna, planova, istraživanja i datiranih testnih izvješća. Povijesni planovi opisuju svoj zabilježen raspon, a ne sadašnju jamstvo podrške značajkama.

### Modeli radnog toka i ciljne prozore čekaju

Katalog sadrži šest uređivih radnih tokova: radni prostor, sastanak, prijenos, istraživanje, projektni prijenos i tekstni snimak. Pronađite `workflow.` u katalogu ili zapovijedi. Odabir radnog toka otvara nesavjetan nacrt; nikada ne radi odmah. Svi koraki se zadržavaju prilikom dodavanja radnog toka postojećem makru ili dodjele ključa Corsair. Konfigurirajte primjerne URL-ove, programske staze, profile i scene OBS prije spašavanja.

**Čeka za ciljno okno ** korak prihvaća ime procesa (s ili bez `.exe`), slučajno neosjetljiv naslov podrijetlo, i vrijeme od 1–600,000 ms. Potrebno je najmanje jedan filter; oba filtera moraju biti usklađena kada se isporučuju. Makro se nastavlja tek nakon što odgovara vidljivo prozor i dosegne predokret. Koristite određeni naslov kada više prozora dijeli proces. Otkazivanje ili vrijeme zaustavlja sljedeće korake. Prozor spremnosti ne jamči da je dokument pun ili da je vanjska usluga kao što je OBS WebSocket povezana.

Jasnost zaslona: pretraživanje **Svijetlost zaslona**, **Svijetljivost zaslona ** ili **Svijetlosti zaslona na 50 posto** u katalogu djelovanja. Relativne akcije mijenjaju svjetlost po 10 procenatnim točkama; fiksne vrijednosti rastu od 0 do 100. Podržani unutarnji zasloni koriste WMI; kompatibilni vanjski monitori zahtijevaju omogućavanje DDC/CI u njihovom meniju. Podrazumevano, akcija utječe na sve podržane zaslone; izričito se može odabrati u uredniku. Poveznice i dokovi mogu ograničiti podršku.

### proširenje kontrole

Otvorite zapovijednu paletu s **Ctrl+K dok je prozor aplikacije fokusiran**. Potražnja za **Radni dizajn**, **Rekordni makro**, **Sinkronizacija profila**, ili **Ključna dijagnoza** (redigiranje slijedi vaš odabran jezik). dizajneri radnog toka uređuju odabranu rasporedu ploče: datoteka/proces/zrakom uvjeti, naprijed skip računati i zaustaviti/ nastaviti s rukovanjem pogreškom..

U koraku svjetlosti, odaberite sve podržane zaslone ili određeni monitor. Neispunjeni odabran monitor proizvodi pogrešku umjesto da tiho mijenja drugi prikaz. Dashboard prikazuje živu svjetlost, audio i OBS čitanja kada su njihove integracije dostupne; nedostupno čitanje se ne prikazuje kao mjerenu vrijednost.

U redateljicu tile dodijelite podržani fizički poseban ključ i odaberite hitno pritisak, tap, držanje ili dvostruki tap. Odabir sloja privremeno aktivira još jedan mapiranje i vraća prethodni sloj na oslobađanju. Gestije i slojevi primjenjuju se na Corsair SDK G ključeve i specifične ključeve za uređaj, a ne obične globalne ključeve. Standardni ključ za snimanje podržava F13–F24 i ključeve medija na tipkovnici. Proizvodni Fn, profil i dijal kontroleri još uvijek zahtijevaju kompatibilan adapter prodavatelja ili prodavatelj koji mapira standardni ključ. Raw Input posmatra ključ i ne potiskuje njegovo normalno ponašanje Windows. Ključne dijagnoze objašnjavaju profil, pauzu, povezivanje i vezane probleme.

Sinkronizacija profila razmjenjuje datoteke kroz mapu koju odaberete, uključujući mapu koju upravlja vaš postojeći klijenti u oblaku. Pregled predviđanja prije uvoza; uvoz počinje isključivati i uklanjaju hardverske kartice. Revizije sprječavaju tihe prepisivanje, a uključujući sadržaj djelovanja je izričita opcija. MAKRIVO ne pruža račun u oblaku ili zadržavaju vjerodostojnost u oblaku. Trak sada koristi svoj teal M ikonu MAKRIVO i prikazuje amber pauzu kada se zaustavi.

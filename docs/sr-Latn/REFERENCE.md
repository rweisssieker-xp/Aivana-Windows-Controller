<!-- generated-doc locale=sr-Latn source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../README.md)

# MAKRIVO

[Potpuna dokumentacija na svim 27 jezicima](README.md) · [Potpuna korisnička ručka](HANDBOOK.md)

**Uz Aivana.** [Trenutni inventar i ograničenja za oslobađanje](FEATURES.md) · [Izveštaj o proširenju](testing/2026-10-12-six-extensions.md) · [Prototip lokalnog asistenta](testing/2026-10-12-local-meeting-assistant.md)

** Engleski (Sjedinjene Američke Države)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalni kontroler Windows sa strem dek-stil pločica, profila, makros i tastature otkrivanje. Interfejs podržava 27 jezike. Nije potreban račun ili oblačna veza za svakodnevnu upotrebu.

[Dokumentacija](README.md) · [Verifikacija](VERIFICATION.md) · [Izveštaj o testu](testing/2026-10-09-usp-release.md)

## Počeo je

Nakon objavljivanja, aplikacija je na `artifacts/win-x64/MAKRIVO.exe`. Držite celu fasciklu izlaza zajedno; možete je kopirati na drugom mestu. Windows 11 x64 je početni cilj. Izgrađeni artefakti su isključeni iz Git-a; izvorni pregled ne uključuje prethodno izgrađenu izvršnu.

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
2. Postavite svoje ime, ikonu, boju i tastaturu. Ctrl/Alt/Shift kombinacije smanjuju slučajnu aktivaciju tokom pisanja.
3. Dodajte jedan ili više koraka: aplikacije, datoteke, veb stranice, tekst, skraćenice, mediji, volumen, prelazak profila, ili odlaganja.
4. Spasite se. Promene ostaju nacrt sve dok se ne spase; diskar vraća spasenu verziju. Ključ za igranje i njegov hotchei koriste isti put izvršenja.
5. Reorganizovati talije i makro korake sa dugmenima za trljanje i bacanje ili strelicama. Modeli pojednostavljaju podešavanje. Makros može ponoviti do 100 puta. Uklonite interfejs, sistemski meni, ili Ctrl+Alt+Echape po podrazumevanju.

Aplikacijski profili koriste ime procesa bez punog puta. Profilni nalog prekida veze; podrazumevani profil je pada. Automatski izbor profila mora biti uključen u podešavanja.

Zatvaranje glavnog prozora zadržava aplikaciju u sistemu po podrazumevanju. Dvostruko kliknite na ikonu trajanja da biste je ponovo otvorili; koristite meni trajanja za izlazak. Početak sa Windows je opcionalan i deaktiviran po podrazumevanju. Hotkeys se može zaustaviti.

## Akcije i komandna paleta

Dodavanje akcije otvara pretraživački katalog sa 90 presecima u osam kategorija. Svako ima opis i predviđanje. Makro uređaj koristi isti katalog da dodaju ili zamenjuju korake. Izbor preseta u ovom radnom toku uređaja stvara projekat bez izvršenja.

Komandna paleta se otvara sa naslova ili sa Ctrl+K unutar aplikacije. Pretraži spasene akcije kroz profile, kataloge, pretvaranje profila, navigaciju, pauzu/rezume, makro otklanjanje i uvoz/eksport. Filtrirajte sačuvane akcije, funkcije kataloga ili komande aplikacija. Izaberite ključeve, aktivirate Enter, Ctrl+Enter uređuje uređiv ulaz, a Escape se zatvori. Prvo se pojavljuju omiljeni. Pripremljeni za upotrebu katalog komande mogu da rade odmah; preseti koji zahtevaju podešavanje otvaraju dizajn. Podešavanja mogu definisati globalni palet kratki. Nova konfiguracija koristi Ctrl+Shift+Space; postojeće konfiguracije ne dobijaju novi globalni skraćenik automatski.

Audio akcije postavljaju tačan volumen od 0 do 100 procenata i mute, nemote ili toggle izlaza i mikrofona uređaja. Izaberite određenu krajnju tačku ili ga ostavite nepromenjenim za defolt Windows. Nedostupna spasena tačka proizvodi grešku umesto da kontroliše drugu uređaj. Refress uređaji ponovo preuzimaju povezane uređaje.

OBS Studio koristi WebSocket v5: start / zaustavljanje / pauza / rezime snimanje; start / start / trajanje; promena scene; unos tugge mute. Uključite WebSocket server pod alatima u OBS i postavite njegovu krajnju tačku u uređaju. Proverite povezivanje i preuzimanje imena čita verziju OBS, scene i ulaze bez pokretanja snimanja ili toka. Izaberite imena iz liste ili ih unesite ručno. Parove se mogu sačuvati u Windows Kreditni menadžer i isključuju se iz izvoza konfiguracija. Nedostupna OBS povezivanja prijavljuju grešku.

Profili mogu odgovarati ime procesa i deo naslova prozora. Oba određena uslova moraju biti u skladu. Lock Profile sprečava automatski prebacivanje sesije. Snimanje uslova iz Foreground koristi poslednju fokusiran spoljnu aplikaciju.

Podrška za tekstove i klipbord akcije `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, i `{{user}}`. Datum korišćenja `yyyy-MM-dd`; Vreme korišćenja `HH:mm`. Nepoznati stanovnici ostaju nepromenjeni. `{{profile}}` Identifikuje profil pokrenutog akcije; `{{process}}` Identifikuje spoljni kontekst aplikacije. sadržaj klipova se čita samo kada se koristi taj lokator. akcija klipova piše tekst, ali se ne uklapa automatski.

Akcije prozora smanjuju, maksimiziraju, obnavljaju ili uređuju prozor na levoj / desnoj strani radnog područja monitora. Sistem akcije zaključak Windows. Ove akcije se odvijaju samo kada je izričito pozvano.

Pregled prikazuje korake, ponavljanja i promenljive bez izvršenja. Undo i Redo zadržavaju do 100 stanja dizajna; tekstualne kutije održavaju uobičajeno Ctrl + Z uređivanje. Identifikujte omiljene u uređaju ili tale meni i filtrirajte ih na tasteru. Izveštaj se primenjuje samo na trenutnu sednicu.

Tekstovi, skraćeni i akcije prozora pokrenuti iz interfejsa aktiviraju poslednju ciljnu prozor. Palet prikazuje taj ciljni kontekst. Ako je cilj zatvoren ili Windows odbija aktivaciju, izvršenje se zaustavlja sa porukom.

## Pomoć tastature i povratne informacije

Specijalni ključ čarobnjak je na tastaturi i u paleti. On navodi G-Ključeve prijavljene od iCUE. Detect Key Press privremeno ih uhvati bez izvršenja njihovih dodeljenih akcija. Normalni veze se vraćaju nakon otkrivanja, otkazivanja ili zatvaranja. Funkcija kataloga i izabrani ključ se otvaraju kao nepročitani dizajn; proveravaju se duplirani veze.

Klavir Overlaj prikazuje odabrani referentni raspored i aktivne veze. Držite Ctrl+Alt+F1 ili ga uklonite sa palete ili tastature. To ne uzima fokus i omogućava mišićnim klikovima da prođu kroz. Profil i obavezne promene ažuriraju dok su vidljivi; uobičajeni Windows ključni pritisci su naglašeni. Kontrole specifične za proizvođače ne mogu da pružaju ekvivalent Windows ključni signal.

Izvršenje povratne informacije pokazuje čekanje, korak / ponavljanje napretka, završetak, otkazivanje ili neuspeh bez fokusiranja ili izlaganja uvedenog teksta. Komande izvršene potvrđuje izvršavanje zapovesti, a ne nezavisno traženo stanje u aplikaciji treće strane. Detalji grešaka se pojavljuju u dnevnici aktivnosti.

## Uređaji i Corsair

Corsair G1 – G6 može se pripisati direktno putem zvaničnog iCUE SDK. Izaberite uređaj i ključ u uređaju akcije i sačuvate, ili pripremite dizajn iz tastature K100. Veze se primenjuju na izabrani uređaj i aktivni profil; pauza ih takođe onemogućava. iCUE moraju biti pokrenuti i omogućiti pristup. Fn, iCUE točkovi, profili i kontrole za zaključak ne mogu se mapirati kroz ovaj G-che API.

Stranica uređaja navodi interfejse tastature Windows i periodično se obnavlja i kada se uređaji povezuju. Fizička tastatura može izložiti više HID interfejsa. Standardni globalni hotkei rade kroz proizvođače.

Corsair detekcija koristi dostupne informacije uređaja. RGB kontrola zahteva kompatibilan hardver, radi iCUE i zvanični iCUE SDK; aplikacija izveštava o stvarnoj dostupnosti. Drugi proizvođači trenutno dobijaju detekciju i standardne akcije. Izvan integracije Corsair G-osnova, vlasnički kontroli rade kada ih Windows primaju kao redovne ulaze. Aplikacija ne instalira tastaturu.

## Jezici

Nastroйke nude 27 jezike: Nemački, Engleski, Francuski, Španski, Italijanski, Portugalski, Holandijski, Češki, Slovački, Mađarski, Bugarski, Grčki, Danski, Švedski, Norveški, Finski, Estonski, Latinski, Litvanski, Slovenijski, Hrvatski, Ukrajinski, Ruski, Turski i Serbijski u latinskom scenariju. Automatski sledi Windows jezik prikazivanja. Prevodi su lokalni. Raspored, hotkei i korisnički imena su nezavisni od jezika interfejsa.

Potpuna dokumentacija za skladištenje dostupna je na svim 27 aplikacionim jezicima kroz [Indeks jezika](README.md)⁇   ⁇ Prevodi su mašinski asistirani, sa originalnim izvornim linkovima i strukturnim proverima; pregled rodnog govornika je odvojeno. Aplikacija takođe prihvata regionalne kodove `en-US`, `de-DE`, `fr-FR` i `it-IT` i koristi njihove brojeve. Postojeći jezički kodovi ostaju kompatibilni

## Dizajn i istraživanje proizvođača

Tastatura pruža pune veličine i nemislive reference ISO-DE/ANSI-US plus Corsair K100 RGB rasporede. Geometrija predstavlja pozicije, široke / velike ključeve i ISO Enter. Standardni rasporedi su generalni referenci. K100 rasporedi prate dokumentisane kontrole bez zahteva točnih dimenzija milimetara. Izaberite pravi jezik/geometriju: ime modela ili USB VID/PID ne utvrđuje određenu varijantu.

Mapiranje rešava fizičke skenirane kodove koristeći aktivni raspored Windows. Radni tok priprema projekat i održava spasavanje / kontrole konflikta. Standardne veze su globalni Windows hotkei, ne izolovani na jednu povezanu tastaturu. Corsair G1-G6 koristi uređaj-specifičnu iCUE integraciju. Druge kontrole za vlasništvo zahtevaju adapter ili proizvođač-konfigurisane skraćenice. Glavni Enter i brojni ključ Enter dele virtuelnu identitet za globalne hotkei.

U tom [Proizvođač istraživanja](research/tastaturhersteller-sonderfunktionen.md) i [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokriva 74 brendova / proizvođača grupa, razlikujući konfiguratore, ulazni SDK i API osvetljenja. [Layout dokaz](research/keyboard-layout-evidence.md) Model, SKU i modul razlike. Istraživački pokrivanje ne znači da se hardverska podrška implementira.

## podaci

Konfiguracija se čuva u lokalnim podacima aplikacije. Uvoz / izvoz koristi JSON. Import predviđa konfiguraciju, zamenjuje trenutnu konfiguraciju nakon potvrde i isključuje uvoze veze dok se ručno ne aktivira. Sadržaj tekstualne akcije se ne prikuplja kao ključni dnevnik. Kreditni menadžer lozinke su odvojene od izvoza profila.

## Verifikacija

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows proveri mogu inspektirati hardver i privremeno konfigurisati / izdati podržane iCUE veze. Ispostavite ih bez drugog kontrolera koristeći ove veze. Audio krajnje tačke provere ne menjaju volumen ili mute podešavanja.

Nemoguće je prevazići privilegije Windows. Obično pokrenut kontroler ne može poslati unos administratoru aplikacije. Zadržani ili zauzeti hotkei se pojavljuju kao konflikti.

U tom [Indeks dokumentacije](README.md) povezuju istorijske dizajne, planove, istraživanja i datum testove. Istorijski planovi opisuju svoj registrovan opseg, a ne aktuelnu garanciju podrške.

### Šabloni radnog toka i ciljni prozor čekaju

Katalog uključuje šest uređivanih radnog toka: radni prostor, sastanak, striming, istraživanje, prekidač projekta i tekstualni snimak. Pretraživanje `workflow.` u katalogu ili komandnom paletu. Izbor radnog toka otvara nepropušteni projekat; nikada ne radi odmah. Svi koraci se zadržavaju kada dodate radni tok postojećem makrou ili ga pripisujete ključu Corsair. Postavite primjer URL-a, programske puteve, profile i OBS scene pre nego što sačuvate.

U tom **Čekajte ciljnu prozor** Korak prihvata ime procesa (sa ili bez `.exe`), Neosetljiv za naslov, i vreme izlaza 1–600,000 MS. Potreban je najmanje jedan filter; oba filtera moraju da odgovaraju kada se isporučuju. Makro se nastavlja samo nakon što odgovarajuće vidljivo prozor i dostiže predgradu. Koristite određeni naslov kada više prozora deli proces. OBS WebSocket povezan je.

Sjajnost ekrana: pretraga za **Umnožavanje sjajnosti ekrana**, **Sjajnost sjaja**, ili **Sjajnosti sjaja na 50 odsto** u katalogu akcija. Relativne akcije menjaju sjaj sa 10 procentnih tačaka; fiksne vrednosti variraju od 0 do 100. Podržani unutrašnji ekrani koriste VMI; kompatibilni spoljni monitori zahtevaju DDC/CI aktiviranje u svom meniju. Po podrazumevanju, akcija utiče na sve podržane ekrane; u uređaju se može izabrati eksplicitan monitor. Priključci i dukovi mogu ograničiti podršku.

### Prošireni kontrole

Otvorite komandnu paletu sa **Ctrl+K dok je prozor aplikacije fokusiran**. Potraga za **Dizajner radnog toka**, **Makro rekordi**, **Sinhronizovanje profila**, ili **Ključna dijagnoza** (Redakcija sledi izabrani jezik). dizajner radnog toka uređuje izabrani dizajn: uslove datoteke / procesa / prozora, napred skoči i zaustavi / nastavi sa upravljanjem greškama. Rekorder uzima kratke kodove i čeka, a ne obično štampanje; Evakuacija zaustavlja snimanje..

U jednoj fazi svetlosti, izaberite sve podržane ekrane ili određeni monitor. Nedostupni izabrani monitor proizvodi grešku umesto da tiho menja drugi ekran. Tabela prikazuje živu svetlost, audio i OBS čitanja kada su njihove integracije dostupne; nemoguće čitanje se ne predstavlja kao merena vrednost.

U uređaju za tabele, dodeljite podržan fizički poseban ključ i izaberite neposredni pritisak, tap, drženje ili dvostruki tap. Held sloj selektori privremeno aktiviraju još jedan mapiranje i vraćaju prethodni sloj na oslobađanju. Gestove i slojevi se primenjuju na Corsair SDK G-chei i specijalne ključeve specifične za uređaj, a ne obične globalne hotkei. Standardni ključ za snimanje podržava F13–F24 i medijske ključeve tastature. Vlasnički Fn, profil i dijalni kontroli i dalje zahtevaju kompatibilan adapter prodavca ili provajder koji mapira na standardni ključ. Raw Input posmatra ključ i ne suzbija njeno normalno ponašanje Windows. Ključna dijagnoza objašnjava profil, pauzu, povezivanje i probleme vezivanja.

Sinhronizacija profila razmjenjuje datoteke kroz fasciklu koju izaberete, uključujući i fascikl koji upravlja vaš postojeći klijent u oblaku. Pregledajte pregled pre uvoza; uvozi počinju da deaktiviraju i uklanjaju hardverske mapiranje. Revizije sprečavaju tihi prepisi, a uključujući i akcioni sadržaj je eksplicitan izbor. MAKRIVO ne pruža oblačni nalog ili zadržava oblačne identifikacije. Traj sada koristi svoj ikonu Theal M MAKRIVO i pokazuje amber pauzu kada se odmori.

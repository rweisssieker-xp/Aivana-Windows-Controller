<!-- generated-doc locale=sl-SI source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../README.md)

# MAKRIVO

[Popolna dokumentacija v vseh jezikih 27](README.md) · [Popolna uporabniška knjiga](HANDBOOK.md)

**z Aivana.** [Trenutni podatki in omejitve za izdajo](FEATURES.md) · [Razširitev testnega poročila](testing/2026-10-12-six-extensions.md) · [Lokalni asistentski prototip](testing/2026-10-12-local-meeting-assistant.md)

** Angleščina (ZDA) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalni kontrolor Windows s strehom ploščic, profilov, makrov in zaznavanje tipkovnice. Vmesnik podpira 27 jezike. Za vsakodnevno uporabo ni potrebe po računu ali povezavi v oblaku.

[Dokumentacija](README.md) · [preverjanje](VERIFICATION.md) · [Funkcijska testna poročila](testing/2026-10-09-usp-release.md)

## Začetek

Po objavi je prijava na `artifacts/win-x64/MAKRIVO.exe`. Celotno izhodno mapo držite skupaj; lahko jo kopirate drugje. Windows 11 x64 je začetni cilj. Izguba predmetov je izključena iz Git; vir preverjanja ne vključuje predizvedljive.

Namestite .NET 10 SDK, ki je določen v `global.json` za gradnjo in zagon iz viru:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Objavite samostojno gradnjo brez potrebe po ločeno nameščenem .NET-u:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Osnovni delovni tok

1. Izberite profil in ustvarite akcijsko ploščo.
2. Nastavite ime, ikono, barvo in kratko tipkovnico. Kombinacije Ctrl/Alt/Shift zmanjšujejo naključno aktiviranje med vpisom.
3. Dodajte eno ali več korakov: aplikacije, datoteke, spletne strani, besedilo, skrajšane, medije, količino, preklapljanje profila ali zamude.
4. in reševati. Spremembe ostanejo osnutek, dokler ni shranjen; diskard obnovlja shranjeno različico. Tile gumb za igranje in njegov hotkey uporablja isto izvedbo poti.
5. Ponovno naročite ploščice in makro korake z gumbom za vlečenje ali puščico. Šabloni poenostavijo nastavitev. Makro lahko ponovi do 100 krat. Preklicajte prek vmesnika, sistemskega menija ali Ctrl+Alt+Escape po predvidenem načinu.

Profili, ki so specifični za aplikacijo, uporabljajo ime procesa brez polnega potovanja. Profilni ukaz razbije povezave; privzeto profil je padec. Samodejni izbor profila mora biti omogočen v nastavitvah.

Zapiranje glavnega okna ohranja aplikacijo v sistemu privzeto. Dvakrat kliknite ikono traja, da jo znova odprete; uporabite meniju traja za izhod. Začetek z Windows je opcijski in onemogočen po predvidenem načinu. Hotkeys se lahko ustavijo osredotočeno.

## Akcije in ukazna paleta

Dodajanje ukrepa odpre iskalni katalog z 90 prednastavitve v osmih kategorijah. vsaka ima opis in predogled. makro urednik uporablja isti katalog za dodajanje ali nadomestitev korakov. Izbira prednastave v tem delovnem toku urednika ustvari načrt brez izvajanja.

Poveljni paleti se odprejo iz naslova ali s Ctrl+K znotraj aplikacije. Iskanje shranjenih ukrepov na profili, katalog prednastavitve, profil spreminjanje, navigacijo, odmora/resume, makro razveljavitev in uvoz/eksport. Filter z shranjenimi dejanji, katalognimi funkcijami ali aplikacijami. Izberite vrstice, aktivirate vnos, Ctrl+Enter urejate urejljivo vnos in se zapre Escape. Najprej se pojavijo priljubljeni. Pripravljeni na uporabo katalog zapovedi lahko takoj; prednastavitve, ki zahtevajo nastavitev odpreti osnutek. Nastavitve lahko opredelijo globalno paleto. Nova konfiguracija uporablja Ctrl+Shift+Space; obstoječe konfiguracije ne prejmejo novega globalnega skrajšanja samodejno.

Zvočni ukrepi določijo volumen natančno od 0 do 100 odstotkov in mute, nemute ali toggle izhod in mikrofonske naprave. Izberite določeno končno točko ali jo zapustite za privzeto Windows. Neuporabna shranjena končna točka povzroča napako namesto nadzora druge naprave. Naprave osvežijo povezane naprave.

OBS Studio uporablja WebSocket v5: start/stop/toggle/pause/resume snemanje; start/stop /toggle pretakanje; spreminjanje prizorišča; toggle vnos mute. Omogočite strežnik WebSocket pod orodji v OBS in nastavite končno točko v uredniku. Preverjanje povezave in prenosa imena bere različico OBS, prizore in vnose brez začetka snemanja ali pretoka. Izberite imena iz seznama ali jih vnesite ročno. Besedila se lahko shranijo v Windows Credential Manager in so izključena iz izvoženih konfiguracij. Neposredne povezave OBS poročajo o napaki.

Profili se lahko ujemajo z imenom procesa in delom naslova okna. Obe pogoji se morajo ujemati. Lock Profile preprečuje samodejno preklapljanje za sejo. Uvodne pogoje iz Foreground uporabljajo zadnjo osredotočeno zunanjo aplikacijo.

Podpora za besedilo in klipboard `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, in `{{user}}`. Datum uporabe `yyyy-MM-dd`; Čas uporabe `HH:mm`. Neznanci ostanejo nespremenjeni. `{{profile}}` Ugotovite profil sproženega dela; `{{process}}` opredeljuje zunanji kontekst aplikacije. vsebina klipa se bere le, ko se ta lokacija uporablja. akcija klipa piše besedilo, vendar ne prilepi samodejno.

Akcije oken zmanjšati, povečati, obnoviti ali urediti okno na levi / desni strani delovne površine svojega monitorja. Sistemski akcijski blok Windows. Te ukrepe se izvajajo le, če so izrecno pozvani.

Pregled prikazuje korake, ponavljanja in spremenljivke brez izvajanja. Undo in Redo ohranjajo do 100 oblikovalskih stanj; besedilna škatla ohranjajo native Ctrl+Z urejanje. Označite priljubljene v urejevalniku ali meniju in jih filtrirajte na plošči. Izvedbeni račun velja le za sedanje seje.

Besedilo, skrajšanje in akcije okna, ki se začnejo iz vmesnika, aktivirajo zadnje zunanje ciljno okno. Ta paleta prikazuje ta ciljni kontekst. Če je cilj zaprt ali Windows zavrne aktivacijo, se izvedba ustavi z sporočilom.

## Ključna pomoč in povratne informacije

Special-Key Wizard je na strani in na paleti. Na seznamu G ključov je navedeno iCUE. Detect Key Press jih začasno ujame, ne da bi izvedli svoje ukrepe. Običajne vezave se vrnejo po odkrivanju, preklicu ali zaprtju. Funkcija kataloga in izbran ključ se odprejo kot nepovratni projekt; preverjajo se podvojene vezave.

Površina tipkovnice prikazuje izbrano postavitev reference in aktivne vezave. Držite Ctrl+Alt+F1 ali ga odstranite iz palete ali tipkovnice. Ne vzame osredotočenosti in omogoča, da mišične kliki prehajajo. Posodobitev profila in obveznih sprememb, medtem ko je vidna; navadni Windows ključni pritiski so poudarjeni. Proizvajalec-specifični kontrolniki ne morejo zagotavljati enakovrednega ključnega signala Windows.

Izvedbeni povratni podatki kažejo čakanje, korak / ponovitev napredka, dokončanje, preklic ali neuspeh, ne da bi se osredotočili ali izpostavili vnesenem besedilu. Commands Executed potrjuje izvedbo ukaza, ne neodvisno želeno stanje v aplikaciji tretje osebe. Podrobnosti o napaki se prikažejo v dnevniku aktivnosti.

## Naprave in Corsair

Corsair G1 – G6 se lahko dodeli neposredno prek uradnega iCUE SDK. Izberite napravo in ključ v urejevalniku ukrepov in shranite ali pripravite predlog iz pogleda na tipkovnici K100. Povezave se uporabljajo za izbrano napravo in aktivni profil; pauza jih tudi onemogoča. iCUE mora biti delovana in omogočati dostop. Fn, iCUE kolesa, profila in ključavnice ni mogoče mapirati skozi to G-klic API.

Stran naprave na seznamu Windows tipkovnice in se redno posodablja in ko se naprave povežejo. Fizična tipkovnica lahko razkriva več HID vmesnikov. Standardni globalni hotkeys delujejo med proizvajalci.

Corsair detekcija uporablja razpoložljive podatke o napravi. RGB nadzor zahteva združljivo strojno opremo, ki deluje iCUE in uradni iCUE SDK; aplikacija poroča o dejanski razpoložljivosti. Drugi proizvajalci trenutno prejemajo odkrivanje in standardne ukrepe. Zunaj integracije Corsair delujejo lastniški kontrolniki, ko jih Windows prejmejo kot redni vnos. Aplikacija ne namesti tipkovnice.

## Jeziki

Nastavitve ponujajo 27 jezikov: nemščina, angleščina, francoščina, španščina, italijanščina, portugalščina, nizozemščina, češčina, slovenščina, madžarščina, bugarščina, grščina, danščina, švedščina, norveščina, finščina, estonščina, latvijščina, litovščina, slovenska, hrvaška, ukrajinska, ruska, turška in srbščina v latinskem pismu. Samodejno sledi jeziku Windows zaslona. Prevajanje je lokalno. Nastavitve, hotkeys in uporabniško ustvarjena imena so neodvisna od jezika vmesnika.

Popolna dokumentacija je na voljo v vseh jezikih 27 [Jezikovni indeks](README.md)in Prevajanja so strojno podprta, z izvornimi povezavami in strukturnimi preverjanji; pregled domačega govornika je ločen. Aplikacija sprejme tudi regionalne kode `en-US`, `de-DE`, `fr-FR` in `it-IT` ter uporablja njihove številke. Obstoječi jezikovni kodi so združljivi.

## Raziskave in raziskave proizvajalcev

Na tipkovnici so v celoti razširjene in brezhibne reference ISO-DE/ANSI-US plus Corsair K100 RGB postavitve. Geometrija predstavlja pozicije, širok/različni ključ in ISO Enter. Standardne postavitve so splošne reference. K100 postavitve sledijo dokumentiranim nadzorom, ne da bi zahtevali natančne dimenzije milimetra. Izberite pravilno različico jezika/geometrije: ime modela ali USB VID/PID ne določa posebne različice.

Zemljevid rešuje fizične kodo skeniranja z aktivnim nastavitvijo vnosa Windows. Delovni tok pripravi osnutek in ohranja ščitnice/konflikte. Standardne vezave so globalni Windows hotkeys, ki niso izolirani na eni povezani tipkovnici. Corsair G1–G6 uporaba naprave-specific iCUE integracija. Drugi lastniški kontrolniki zahtevajo adapter ali proizvajalec-konfigurirane skrajšave. Glavni Enter in številčni tipkovnica Enter delijo virtualno ključno identiteto za globalne hotkeys.

V tem primeru [Proizvajalec raziskave](research/tastaturhersteller-sonderfunktionen.md) in [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokriva 74 skupine blagovnih znamk / proizvajalcev, ki razlikujejo konfiguracije, vnosne SDK in svetlobne API. [Layout dokazov](research/keyboard-layout-evidence.md) Model zapisov, SKU in module razlike. Raziskovalno pokritost ne pomeni, da je izvedena strojna podpora.

## Podatki

Konfiguracija je shranjena v lokalnih aplikacijah. Uvoz / izvoz uporablja JSON. Uvoz predvideva konfiguracijo, nadomešča trenutno konfiguracijo po potrditvi in onemogoča uvožene vezave, dokler ni ročno omogočena. Vsebina besedilnega delovanja ni prikazana kot ključni dnevnik. Besedila za upravljavce so ločena od izvoženih profilov.

## preverjanje

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows preverjanja lahko preverja strojno opremo in začasno konfigurira/oddajati podprte iCUE vezave. Uporabljajte jih brez drugega upravljavca, ki uporablja te vezave. Avdio končni kontrolniki ne spreminjajo v živo ali mute nastavitve.

Domači vnos ne more presegati Windows privilegij. Običajno zagonski upravljavec ne more poslati vnosov za aplikacijo upravljavca. Rezervirani ali zasedeni hotkeji se pojavijo kot konflikti.

V tem primeru [Dokumentacijski indeks](README.md) Povezava zgodovinskih načrtov, načrtov, raziskav in datiranih testnih poročil. Zgodovinski načrti opisujejo njihovo doseganje, ne pa trenutno garancijo podpore.

### Delovni tokovi in ciljno okno čakajo

Katalog vsebuje šest urejljivih delovnih tokov: delovni prostor, srečanje, pretok, raziskave, projektni preklop in besedilni snip. Poiščite `workflow.` v katalogu ali ukazni paleti. Izbira delovnega toka odpre nezahtevni načrt; nikoli ne poteka takoj. Vsi koraki se ohranijo, ko dodate delovni tok obstoječemu makru ali ga dodelite ključu Corsair. Nastavite primerne URL-je, programske poti, profili in OBS prizore pred shranjevanjem.

V tem primeru **Čakajte na ciljno okno** korak sprejme ime postopka (z ali brez `.exe`), neoznačen naslov, in časovni izhod 1–600,000 ms. najmanj en filter je potreben; oba filtra morajo ustrezati, ko so na voljo. makro se nadaljuje le, ko ustreza vidno okno in doseže predogled. Uporabite določen naslov, ko več oken delijo proces. preklic ali časovni izhod ustavi naslednje korake. OBS WebSocket je povezana.

Razsvetljava zaslona: iskanje **Vzgoja svetlosti zaslona**, **Zmanjšanje svetlosti zaslonov** ali **Vzgoje zaslona na 50 odstotkov** v katalogu delovanja. Relativna dejanja spreminjajo svetlost z 10 odstotkovnimi točkami; fiksne vrednosti se gibljejo od 0 do 100. Podprti notranji zasloni uporabljajo WMI; združljivi zunanji monitorji zahtevajo omogočanje DDC/CI v meniju. Po navadi dejanje vpliva na vse podprte zaslone; v uredniku je mogoče izbrati izrecen monitor. Povezave in doki lahko omejijo podporo.

### Razširjena kontrola

Odprite ukazno paleto z **Ctrl+K, ko je okno aplikacije osredotočeno**. Iskanje za **Delovni tok oblikovalca**, **Rekordna makro**, **Sinkronizacija profilov**, ali **Ključna diagnostika** (oblikovalec delovnega toka ureja izbran načrt ploščice: datoteke / postopka / pogoje oken, napoveduje število in stop / nadaljuje obdelavo napak..

V koraku svetlosti izberite vse podprte zaslone ali določen monitor. Na voljo izbran monitor povzroči napako namesto da tiho spremeni drugo zaslon. Ploščica prikazuje svetlost, avdio in OBS branja, ko so na voljo njihove povezave; neuporabna branja ni predstavljena kot merjena vrednost.

V urejevalniku ploščice dodelite podprt fizični posebni ključ in izberite takojšnjo tiskanje, tap, držo ali dvojno tap. Held sloj selektorji začasno aktivirajo drugo mapiranje in obnoviti prejšnji sloj na sproščanje. Gesti in sloji se uporabljajo za Corsair SDK G ključe in posebne ključe, ki so specifične za napravo, ne običajne globalne ključe. Standardni ključ ujemanja podpira F13–F24 in tipkovnice medijske ključe. Lastni Fn, profil in dial kontrolniki še vedno zahtevajo združljiv adapter ponudnika ali ponudnik, ki mapira na standardni ključ. Raw Input opazuje ključ in ne zavira njegovega normalnega vedenja Windows. Ključne diagnoze pojasnjujejo profil, odmor, povezave in vezane težave.

Sinkronizacija profila izmenja datoteke skozi mapo, ki jo izberete, vključno z mapo, s katero upravlja vaš obstoječi odjemalec v oblaku. Preverite predogled pred uvozom; uvoz se začne onemogočiti in odstranite strojno-specifične zemljevidov. Revizije preprečujejo tiho prepisovanje, vključno z vsebino dejanja pa je izrecna izbira. MAKRIVO ne zagotavlja računa v oblaku ali ohranja potrdila v oblaku. Trak zdaj uporablja lastno ikono M teal MAKRIVO in prikazuje amber odmor znak, ko je prekinjen.

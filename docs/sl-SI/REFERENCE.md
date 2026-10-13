<!-- generated-doc locale=sl-SI source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../README.md)

# MAKRIVO

[Celotna dokumentacija v vseh 27 jezikih](README.md) · [Celoten uporabniški priročnik](HANDBOOK.md)

**Avtor: Aivana.** [Trenutni pregled funkcij in omejitve izdaje](FEATURES.md) · [Poročilo o preizkusu razširitev](testing/2026-10-12-six-extensions.md) · [Prototip lokalnega pomočnika z umetno inteligenco](testing/2026-10-12-local-meeting-assistant.md)

** Angleščina (ZDA) ** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalni upravljalnik bližnjic Windows s ploščicami v slogu Stream Deck, profili, makri in zaznavanjem tipkovnic. Vmesnik podpira 27 jezikov. Za vsakodnevno uporabo ne potrebujete računa ali povezave z oblakom.

[Dokumentacija](README.md) · [preverjanje](VERIFICATION.md) · [Funkcijska testna poročila](testing/2026-10-09-usp-release.md)

## Začetek

Po pripravi distribucije je aplikacija na `artifacts/win-x64/MAKRIVO.exe`. Celotno izhodno mapo ohranite skupaj; kopirate jo lahko drugam. Začetna ciljna platforma je Windows 11 x64. Rezultati gradnje so izključeni iz Git; kopija izvorne kode ne vsebuje vnaprej zgrajene izvedljive datoteke.

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
2. Nastavite ime, ikono, barvo in bližnjico na tipkovnici. Kombinacije Ctrl/Alt/Shift zmanjšajo nenamerno aktiviranje med tipkanjem.
3. Dodajte enega ali več korakov: aplikacije, datoteke, spletna mesta, besedilo, bližnjice, predstavnost, glasnost, preklapljanje profilov ali zakasnitve.
4. Shranite. Spremembe ostanejo osnutek, dokler jih ne shranite; zavračanje obnovi shranjeno različico. Gumb za predvajanje na ploščici in njena bližnjica uporabljata isto pot izvajanja.
5. Ponovno naročite ploščice in makro korake z gumbom za vlečenje ali puščico. Šabloni poenostavijo nastavitev. Makro lahko ponovi do 100 krat. Preklicajte prek vmesnika, sistemskega menija ali Ctrl+Alt+Escape po predvidenem načinu.

Profili za posamezne aplikacije uporabljajo ime procesa brez polne poti. Vrstni red profilov razreši enakovredna ujemanja; privzeti profil je nadomestni. Samodejno izbiro profila morate omogočiti v nastavitvah.

Zapiranje glavnega okna ohranja aplikacijo v sistemu privzeto. Dvakrat kliknite ikono traja, da jo znova odprete; uporabite meniju traja za izhod. Začetek z Windows je opcijski in onemogočen po predvidenem načinu. Hotkeys se lahko ustavijo osredotočeno.

## Akcije in ukazna paleta

Dodajanje ukrepa odpre iskalni katalog z 90 prednastavitve v osmih kategorijah. vsaka ima opis in predogled. makro urednik uporablja isti katalog za dodajanje ali nadomestitev korakov. Izbira prednastave v tem delovnem toku urednika ustvari načrt brez izvajanja.

Ukazna paleta se odpre iz glave ali s Ctrl+K znotraj aplikacije. Išče shranjena dejanja v vseh profilih, prednastavitve kataloga, preklapljanje profilov, navigacijo, premor/nadaljevanje, preklic makrov in uvoz/izvoz. Filtrirajte po shranjenih dejanjih, funkcijah kataloga ali ukazih aplikacije. Smerne tipke izbirajo, Enter aktivira, Ctrl+Enter ureja element, ki ga je mogoče urejati, Escape pa zapre. Priljubljene postavke so prve. Pripravljeni ukazi kataloga se lahko izvedejo takoj; prednastavitve, ki zahtevajo nastavitev, odprejo osnutek. V nastavitvah lahko določite globalno bližnjico palete. Nove konfiguracije uporabljajo Ctrl+Shift+Space; obstoječe konfiguracije nove globalne bližnjice ne dobijo samodejno.

Zvočna dejanja nastavijo glasnost natančno od 0 do 100 odstotkov ter utišajo, vklopijo zvok ali preklopijo utišanje izhodnih in mikrofonskih naprav. Izberite določeno končno točko ali pustite nenastavljeno za privzeto napravo Windows. Nedostopna shranjena končna točka povzroči napako namesto upravljanja druge naprave. Osveževanje naprav znova naloži priključene naprave.

OBS Studio uporablja WebSocket v5: zagon/ustavitev/preklop/premor/nadaljevanje snemanja; zagon/ustavitev/preklop pretakanja; spreminjanje scen; preklop utišanja vhoda. V OBS pod orodji omogočite strežnik WebSocket in v urejevalniku nastavite njegovo končno točko. Preverjanje povezave in nalaganje imen prebere različico OBS, scene in vhode brez zagona snemanja ali pretakanja. Imena izberite s seznama ali jih vnesite ročno. Gesla lahko shranite v Windows Credential Manager in so izključena iz izvoženih konfiguracij. Nedostopne povezave OBS javijo napako.

Profili se lahko ujemajo z imenom procesa in delom naslova okna. Oba določena pogoja se morata ujemati. Zaklep profila prepreči samodejno preklapljanje med sejo. Zajem pogojev iz ospredja uporabi zadnjo zunanjo aplikacijo s fokusom.

Dejanja za besedilo in odložišče podpirajo `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` in `{{user}}`. Datumi uporabljajo `yyyy-MM-dd`; časi uporabljajo `HH:mm`. Neznana nadomestna polja ostanejo nespremenjena. `{{profile}}` označuje profil zagnanega dejanja; `{{process}}` označuje kontekst zunanje aplikacije. Vsebina odložišča se bere samo ob uporabi tega nadomestnega polja. Dejanje odložišča zapiše besedilo, vendar ga ne prilepi samodejno.

Dejanja oken minimizirajo, maksimizirajo, obnovijo ali razporedijo okno na levo/desno stran delovnega območja njegovega monitorja. Sistemsko dejanje zaklene Windows. Ta dejanja se izvedejo samo ob izrecnem klicu.

Predogled prikaže korake, ponovitve in spremenljivke brez izvajanja. Razveljavitev in ponovitev hranita do 100 stanj osnutka; besedilna polja ohranijo običajno urejanje s Ctrl+Z. Priljubljene označite v urejevalniku ali meniju ploščice in jih filtrirajte na nadzorni plošči. Števci izvajanj veljajo samo za trenutno sejo.

Besedilo, skrajšanje in akcije okna, ki se začnejo iz vmesnika, aktivirajo zadnje zunanje ciljno okno. Ta paleta prikazuje ta ciljni kontekst. Če je cilj zaprt ali Windows zavrne aktivacijo, se izvedba ustavi z sporočilom.

## Ključna pomoč in povratne informacije

Čarovnik za posebne tipke je na strani tipkovnice in v paleti. Navaja tipke G, ki jih sporoči iCUE. Zaznavanje pritiska tipke jih začasno zajame brez izvajanja dodeljenih dejanj. Običajne vezave se obnovijo po zaznavi, preklicu ali zapiranju. Funkcija kataloga in izbrana tipka se odpreta kot neshranjen osnutek; podvojene vezave se preverjajo.

Prekrivni prikaz tipkovnice kaže izbrano referenčno razporeditev in aktivne vezave. Držite Ctrl+Alt+F1 ali ga preklopite v paleti oziroma na strani tipkovnice. Ne prevzame fokusa in prepušča klike miške. Spremembe profilov in vezav se posodabljajo, ko je viden; običajni pritiski tipk Windows so poudarjeni. Kontrolniki proizvajalcev morda ne oddajajo enakovrednega signala tipke Windows.

Povratne informacije o izvajanju kažejo čakanje, napredek korakov/ponovitev, dokončanje, preklic ali napako brez prevzemanja fokusa ali razkrivanja vnesenega besedila. Izvedeni ukazi potrjujejo izvajanje ukazov, ne neodvisno pridobljenega sprotnega stanja v aplikaciji tretje osebe. Podrobnosti napak so v dnevniku dejavnosti.

## Naprave in Corsair

Corsair G1–G6 lahko neposredno dodelite prek uradnega SDK iCUE. V urejevalniku dejanj izberite napravo in tipko ter shranite ali pripravite osnutek iz prikaza tipkovnice K100. Vezave veljajo za izbrano napravo in aktivni profil; premor jih tudi onemogoči. iCUE mora delovati in dovoliti dostop. Fn, koleščka iCUE ter kontrolnikov profila in zaklepanja ni mogoče preslikati prek tega API-ja G-key.

Stran naprave na seznamu Windows tipkovnice in se redno posodablja in ko se naprave povežejo. Fizična tipkovnica lahko razkriva več HID vmesnikov. Standardni globalni hotkeys delujejo med proizvajalci.

Zaznavanje Corsair uporablja razpoložljive podatke o napravi. Upravljanje RGB zahteva združljivo strojno opremo, zagnan iCUE in uradni SDK iCUE; aplikacija sporoča dejansko razpoložljivost. Drugi proizvajalci trenutno dobijo zaznavanje in standardna dejanja. Zunaj integracije Corsair G-key lastniški kontrolniki delujejo, ko jih Windows prejme kot običajni vnos. Aplikacija ne namesti gonilnika tipkovnice.

## Jeziki

Nastavitve ponujajo 27 jezikov: nemščino, angleščino, francoščino, španščino, italijanščino, portugalščino, nizozemščino, poljščino, češčino, slovaščino, madžarščino, romunščino, bolgarščino, grščino, danščino, švedščino, norveščino, finščino, estonščino, latvijščino, litovščino, slovenščino, hrvaščino, ukrajinščino, ruščino, turščino in srbščino v latinici. Samodejna izbira sledi jeziku prikaza Windows. Prevodi so priloženi lokalno. Razporeditve, bližnjice in uporabniška imena so neodvisni od jezika vmesnika.

Celotna dokumentacija repozitorija je na voljo v vseh 27 jezikih aplikacije prek [Jezikovni indeks](README.md). Prevodi so strojno podprti, s povezavami na izvirne vire in strukturnimi preverjanji; pregled maternih govorcev je ločen. Aplikacija sprejema tudi regionalne kode `en-US`, `de-DE`, `fr-FR` in `it-IT` ter uporablja njihove oblike števil. Obstoječe jezikovne kode ostajajo združljive.

## Raziskave in raziskave proizvajalcev

Prikaz tipkovnice ponuja referenčne razporeditve ISO-DE/ANSI-US polne velikosti in brez številskega dela ter razporeditve Corsair K100 RGB. Geometrija predstavlja položaje, široke/visoke tipke in ISO Enter. Standardne razporeditve so splošne reference. Razporeditve K100 sledijo dokumentiranim kontrolnikom brez trditve o milimetrski natančnosti. Izberite pravo jezikovno/geometrijsko različico: ime modela ali USB VID/PID ne določa točne različice.

Preslikava razreši fizične kode tipk z aktivno razporeditvijo vnosa Windows. Potek dela pripravi osnutek in ohrani preverjanja shranjevanja/sporov. Standardne vezave so globalne bližnjice Windows in niso omejene na eno priključeno tipkovnico. Corsair G1–G6 uporabljajo integracijo iCUE za posamezno napravo. Drugi lastniški kontrolniki zahtevajo adapter ali bližnjice, nastavljene pri proizvajalcu. Glavni Enter in Enter številskega dela imata za globalne bližnjice isto identiteto navidezne tipke.

V tem primeru [Proizvajalec raziskave](research/tastaturhersteller-sonderfunktionen.md) in [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokriva 74 skupine blagovnih znamk / proizvajalcev, ki razlikujejo konfiguracije, vnosne SDK in svetlobne API. [Layout dokazov](research/keyboard-layout-evidence.md) beleži razlike med modeli, SKU in moduli. Raziskovalna pokritost ne pomeni implementirane podpore strojne opreme.

## Podatki

Konfiguracija je shranjena v lokalnih podatkih aplikacije. Uvoz/izvoz uporabljata JSON. Uvoz prikaže predogled konfiguracije, po potrditvi nadomesti trenutno konfiguracijo in onemogoči uvožene vezave do ročnega omogočanja. Vsebina besedilnih dejanj se ne zbira kot dnevnik pritiskov tipk. Gesla upravljalnika poverilnic so ločena od izvoženih profilov.

## preverjanje

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows preverjanja lahko preverja strojno opremo in začasno konfigurira/oddajati podprte iCUE vezave. Uporabljajte jih brez drugega upravljavca, ki uporablja te vezave. Avdio končni kontrolniki ne spreminjajo v živo ali mute nastavitve.

Nativni vnos ne more obiti pravic Windows. Običajno zagnani upravljalnik morda ne more poslati vnosa aplikaciji, zagnani kot skrbnik. Rezervirane ali zasedene bližnjice so prikazane kot spori.

V tem primeru [Dokumentacijski indeks](README.md) Povezava zgodovinskih načrtov, načrtov, raziskav in datiranih testnih poročil. Zgodovinski načrti opisujejo njihovo doseganje, ne pa trenutno garancijo podpore.

### Delovni tokovi in ciljno okno čakajo

Katalog vsebuje šest potekov dela, ki jih lahko urejate: delovni prostor, sestanek, pretakanje, raziskovanje, preklop projekta in besedilni izsek. V katalogu ali ukazni paleti iščite `workflow.`. Izbira poteka odpre neshranjen osnutek; nikoli se ne zažene takoj. Vsi koraki se ohranijo pri dodajanju poteka obstoječemu makru ali dodelitvi tipki Corsair. Pred shranjevanjem nastavite vzorčne URL-je, poti programov, profile in scene OBS.

Korak **Čakanje na ciljno okno** sprejme ime procesa (z `.exe` ali brez), podniz naslova brez razlikovanja velikih in malih črk ter časovno omejitev 1–600000 ms. Potreben je vsaj en filter; če sta navedena oba, se morata oba ujemati. Makro se nadaljuje šele, ko se ujemajoče vidno okno odzove in pride v ospredje. Pri več oknih istega procesa uporabite določen naslov. Preklic ali časovna prekoračitev ustavi naslednje korake. Pripravljenost okna ne zagotavlja, da se je dokument naložil ali da je povezana zunanja storitev, kot je OBS WebSocket.

Razsvetljava zaslona: iskanje **Vzgoja svetlosti zaslona**, **Zmanjšanje svetlosti zaslonov** ali **Vzgoje zaslona na 50 odstotkov** v katalogu delovanja. Relativna dejanja spreminjajo svetlost z 10 odstotkovnimi točkami; fiksne vrednosti se gibljejo od 0 do 100. Podprti notranji zasloni uporabljajo WMI; združljivi zunanji monitorji zahtevajo omogočanje DDC/CI v meniju. Po navadi dejanje vpliva na vse podprte zaslone; v uredniku je mogoče izbrati izrecen monitor. Povezave in doki lahko omejijo podporo.

### Razširjena kontrola

Odprite ukazno paleto s **Ctrl+K, ko ima okno aplikacije fokus**. Iščite **Oblikovalnik poteka dela**, **Snemanje makra**, **Sinhronizacija profilov** ali **Diagnostika tipk** (besedilo sledi izbranemu jeziku). Oblikovalnik ureja izbrani osnutek ploščice: pogoje datotek/procesov/oken, število korakov za preskok naprej in ustavitev/nadaljevanje ob napaki. Snemalnik zajema kombinacije bližnjic in čakanja, ne običajnega tipkanja; Escape ustavi snemanje. Pred prevzemom preglejte rezultat.

V koraku svetlosti izberite vse podprte zaslone ali določen monitor. Na voljo izbran monitor povzroči napako namesto da tiho spremeni drugo zaslon. Ploščica prikazuje svetlost, avdio in OBS branja, ko so na voljo njihove povezave; neuporabna branja ni predstavljena kot merjena vrednost.

V urejevalniku ploščic dodelite podprto fizično posebno tipko in izberite takojšen pritisk, kratek pritisk, držanje ali dvojni pritisk. Izbirniki plasti med držanjem začasno aktivirajo drugo preslikavo in ob sprostitvi obnovijo prejšnjo plast. Geste in plasti veljajo za tipke G SDK Corsair ter standardne posebne tipke posamezne naprave, ne za običajne globalne bližnjice. Zajem standardnih tipk podpira F13–F24 in predstavnostne tipke tipkovnice. Lastniški kontrolniki Fn, profila in vrtljivega gumba še vedno zahtevajo združljiv adapter proizvajalca ali njegovo preslikavo v standardno tipko. Raw Input opazuje tipko in ne prepreči običajnega vedenja Windows. Diagnostika tipk pojasni težave profilov, premora, povezave in vezav.

Sinhronizacija profilov izmenjuje datoteke prek izbrane mape, tudi mape, ki jo upravlja vaš obstoječi odjemalec za oblak. Pred uvozom preglejte predogled; uvožene vezave so sprva onemogočene in preslikave posamezne strojne opreme se odstranijo. Revizije preprečijo tiho prepisovanje, vključitev vsebine dejanj pa je izrecna izbira. MAKRIVO ne zagotavlja računa v oblaku ali hrani poverilnic za oblak. Ikona v območju za obvestila zdaj uporablja lastno modrozeleno ikono M MAKRIVO in med premorom pokaže oranžnorjavo oznako premora.

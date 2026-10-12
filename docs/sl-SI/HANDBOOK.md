<!-- generated-doc locale=sl-SI source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../HANDBOOK.md)

# MAKRIVO — Uporabniški priročnik

Proizvajalec: **Aivana**. Dokumentacija snapshot: **2026-10-12**. Ta priročnik opisuje trenutno izvajanje delovnega drevesa. eksperimentalne integracije in funkcije, ki so odvisne od strojne opreme, so izrecno identificirane. zgodovinski podatki opisujejo njihovo datirano snop, ne pa garancijo za novo gradnjo.

## Dokumentacija in jeziki

Uporabite izbiro jezika dokumentacije za odprtje celotnega priročnika, zapisa funkcij, reševanja težav, poročil o razvoju, raziskav in zgodovinskih testnih poročil v katerem koli od 27 uporabniški jeziki. ustvarjeni prevodi so strojno podprti in imajo strukturne preverjanja; niso vsi prejeli pregleda domačega govornika. tehnični identifikatorji in izvedljivi primeri ostanejo nespremenjeni. Izvirni vir je na voljo za primerjavo. Prevedena analiza licence je informativna in ne nadomešča prvotnih pravnih pogojev tretje osebe ali končnega EULA.

## Namestitev in prvi zagon

Prvi cilj je Windows 11 x64. ohranite vse datoteke in pododdelke distribucije skupaj. `MAKRIVO.exe`. Samodejni paketi vključujejo čas delovanja .NET; paketi, ki so odvisni od okvirja, zahtevajo ustrezno .NET 10 Desktop in ASP.NET izvajanje. ONNX in spremljevalci morajo biti vključeni za njihove značilnosti.

1. Začnite MAKRIVO in izberite jezik v nastavitvah. Samodejno sledi Windows Ključna plošča je ločena postavitev.
2. Izberite profil in nato dodajte vrstico skozi katalog iskanja. Dajte mu prepoznavno ime in izberite njegove dejavnosti.
3. Preverite korake in po potrebi dodelite skrajšanje. Izogibajte se kombinacijam, ki jih že uporablja Windows ali vaša ciljna aplikacija.
4. Shrani projekt Neposkrbene spremembe niso shranjene konfiguracije.
5. Preizkusite z neškodljivimi dejanji, kot je odprtje dokumenta in preverite povratne informacije o izvedbi.

Zapiranje glavnega okna običajno zapusti aplikacijo v območju obvestila. Dvojno kliknite ikono za ponovno odprtje, ali pa uporabite meniju za izhod. Začetek z Windows je prostoren in na začetku onemogočen. Kontrola odmora ustavi vezave; ikona prikazuje oznako odmora. Odstranite delovne makro prek UI, tray ali konfiguriranega odpovednika; privzeto je `Ctrl+Alt+Escape`.

## Tile, akcije in ukazna paleta

Tile pripadajo profilom in lahko vsebujejo več naročenih korakov. Upravljavec podpira shranjevanje/izogibanje, predogled, undo/redo in korak preoblikovanje. Katalog zagotavlja prednastavitve za programe, dokumente, URL-je, besedilo, klipe, skrajšane, medije, avdio, okna, sistemske akcije in podprte integracije. Prednastavitev, ki zahteva parametre, se odpre kot načrt.

Odprite paleto z `Ctrl+K` medtem ko MAKRIVO je osredotočen. Iskanje shranjenih dejanj, prednastavitve, profilov in orodij. Izberite ključe; Vpišite aktivira; `Ctrl+Enter` urejanje podprte točke; Izhod zapre. Nekatere pripravljene ukaze se izvajajo takoj, konfigurirane ukrepe pa zahtevajo pregled. Lahko konfigurirate ločeno globalno kratko paleto; nove konfiguracije uporabljajo `Ctrl+Shift+Space`.

Podpora za besedilo in clipboard korake `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` in `{{user}}`. Datum oblikovanja je `yyyy-MM-dd`; Čas oblikovanja je `HH:mm`. Neznane spremenljivke ostanejo nespremenjene.Clipboard branje se zgodi le, ko je zahtevan.Klipboard dejanje piše besedilo brez samodejnega prilagajanja.

Ukrepi, ki se začnejo s strani uporabnika, lahko usmerjajo na zadnjo zunanjo aplikacijo. Windows blokov vnaprej aktivirajo, ročno aktivirajo cilj in se vrnejo. uspešna dostava ključa ne dokaže, da je cilj končal svoje naloge.

## Profili in nastavitve delovnega mesta

Profili se lahko ujemajo z imenom procesa in delom naslova okna. Ko sta obe postavljeni, se morajo oba združiti. Profilska naročila rešujejo tekme; privzet profil je padec. omogočajo samodejno izbiro. Lock Profile preprečuje samodejne spremembe trenutne seje.

Odprte nastavitve delovnega mesta iz orodij ali palete. Izberite programe, dokumente, spletne strani, ciljno/izgledno ali sceno OBS. Pomočnik pripravi delovno mesto, srečanje, pretakanje, raziskave ali profili projekta. Preverite ustvarjene korake pred prihranki. Nastavitve ne izvajajo pripravljenih ukrepov.

## Makro, pogoji in snemanje

Macros izvaja svoje korake v redu in podpira počitke, ponovitve in preklic. Skupna omejitev ponovitve je 100. Uporabite predogled, preden izvedete dejanja, ki jih vnesete v drugo aplikacijo ali spremenite stanje sistema.

Čakajte na ciljno okno, ki sprejme proces, naslov in časovni izhod od 1 do 600,000 ms. Potrebno je vsaj en filter. Prilagodljivo vidno okno mora odgovoriti in postati predhodno, preden se nadaljujejo naslednje korake. Pripravljeno okno ne dokaže, da je dokument ali storitev končana polnjenje.

Upravljavec delovnega toka dodaja pogoje za datoteke, procesno delovanje in okno. Lažji pogoj je korak naprej in lahko premika dodatne korake naprej. Zavrnite pogoj, da izrazite alternativno podružnico. Izberite stop-on-error ali continu-on-err delno; nadaljevanje lahko zapusti poznejše korake brez njihovih pričakovanih pogojev.

Akcijski zapisnik zajema kratke kode in čaka, ne običajno pisanje. Podpira spremenljivke in funkcijske ključe. Utekanje se ustavi z snemanjem. Preverite seznam in ga sprejmete kot nezahtevni načrt, nato pa izrecno shranite. UI-klik je ločeno orodje, opisano spodaj.

## Ključne plošče, gest in sloji

Na tipkovnici so navedeni odkriti vmesniki in referenčni postavki. Ena fizična tipkovnica lahko razkriva več vmesnikov. Izberite pravilni jezik in geometrijo; samo ime izdelka ne določa ISO ali ANSI postavitve.

Standardni globalni hotkeys se uporabljajo na tipkovnicah. Raw Input povezave spremljajo določeno napravo in kodo skeniranja, vključno s podporo funkcije in medijev ključe. **Raw Input Ne zavrača običajnega Windows izključna druga tipkovnica ni pripravljena na proizvodnjo.** Potrjen, licenciran in preizkušeni filter-vodnik je še vedno potreben; nobenega MAKRIVO.

Podprte posebne ključne vezave lahko takoj pritisnete, tapete, držite ali dvakrat pritisnete. ključavnice s slojem začasno izberete drugo mapiranje in obnovite osnovno plast na sproščanje; ključavnica s slojem ne deluje svoje makro. 150 za 5,000 MS; dvoposteljna okna iz 150 za 1,000 MS. Te mapiranje se razlikuje od običajnih globalnih hotkeys.

Posebni ključni čarovnik odkrije združljive ključe, ne da bi izvajal svoje dodeljene ukrepe med ujetjem. Diagnoza pojasnjuje razpoložljive naprave, prekinjene vezave, nepravilnosti profila in konflikte. Preklapljanje prikazuje izbrano postavitev in vezave; njeno privzeto kratko je `Ctrl+Alt+F1`.

| Integracija | Zahteve in omejitve |
| --- | --- |
| Corsair | Spremljanje iCUE, dostop do SDK in združljiv model. G1–G6 in podprte funkcije RGB; lastni Fn, kolesa, ključavnice in profilne kontrole so običajno ne izpostavljeni API G ključavnic. |
| Logitech G | Originalna lokalna knjižnica x64 G-key/LED zahteva izrecno odobritev. G-slavni dogodki ne identificirajo več enakih fizičnih tipkovnic. |
| Razer | Chroma RGB in standardni vnos; zemljevidi lastniški kontrolniki v Synapse kjer je potrebno. |
| SteelSeries | GameSense RGB in standardni vnosi; mapna lastnina v GG, kjer je potrebno. |
| Wooting | Prilagodljiva originalna x64 SDK za analogne vrednosti in pragove; normalni vnos HID ostane aktiven. |
| Druge ključne plošče | Standardni hotkeys ali podprti fizični skenerji. Fn in lastniški kontrolniki so odvisni od modela in konfiguracije ponudnika. |

Raziskave proizvajalca opisujejo dokumentirane zmogljivosti, ki niso bile izvedene ali preizkušene za združljivost z vsemi navedenimi napravami. Novi adapterji še vedno potrebujejo resnično sprejemanje strojne opreme. Ne prenesite SDK binarnih virov iz nezanesljivih virov; pravice distribucije SDK so ločene od tehnične združljivosti.

## Avdio, zasloni in OBS

Zvočni koraki nastavijo volumen od 0 do 100 odstotkov in mute/unmute/toggle podprto izhod ali mikrofon končne točke. Izberite določeno končno točko ali uporabite predvideno Windows. Izgubljeni cilj povzroči napako namesto tiho nadzora nad drugo napravo. Naprave po ponovnem povezovanju. Končna točka nadzora ni zahteva za neodvisno podporo mešalnika.

Prikazuje akcije povečati / zmanjšati svetlost za 10 odstotnih točk ali določiti vrednost od 0 do 100. Notranji zasloni uporabljajo podprte WMI vmesnike; zunanji monitorji zahtevajo podporo DDC/CI in običajno nastavitev menija. Dokovi, adapterji in kabli lahko blokirajo nadzor. Izberite vse zaslone ali en monitor. Ponovna povezava lahko spremeni razpoložljive identitete; pregleda shranjene cilje. Izgubljeni bralci so prikazani kot nedostopni namesto merjenih vrednosti.

OBS zahteva WebSocket v5 omogočen v OBS orodja. Nastavite končno točko, preizkusite povezavo in polnjenje scene / vnos imena pred shranjevanjem. Podprte ukrepe vključujejo premikanje scene, tok / zapis start / stop / togo, snemanje odmora / povzetka in vnos mute. Besedila se lahko shranijo v Windows Credential Manager in so izključena iz konfiguracijskih izvozov. Uspešna ukaz OBS ne določa vsakega pretoka učinka.

## Zgodovina preverjanja in izvrševanja

Preverite rezultate za ustrezne korake. Aplikacija razlikuje predogled, potrjene učinke, napake, preklic in učinke, ki jih ni mogoče preveriti. Obstoječi proces ni pripravljenost za uporabo. Relativne spremembe, skrajšanja in vnos besedila ne morejo prejeti zahtevka za uspeh. Preverite dnevnik dejavnosti in zgodovino preverjanja, ko dejanje ne uspe. Napake v konfiguraciji je treba popraviti, preden končate celotno makro.

## UI avtomatizacija in snemanje klik

Dodajte korak za avtomatizacijo, izberite operacijo in zajemajte cilj pod ukaznikom po prikazani zamudi. samo zajemanje ne izvaja ničesar. preverite izbiro in zahtevate edinstveno tekmo. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` in `WaitState`. Polja gesla, onemogočen kontrolnik in nejasni selektorji so zavrnjeni.

UI avtomatizacija poteka v omejenem spremljevalnem procesu. Odvisno je od podporo in dovoljenj za dostopnost ciljne aplikacije Windows. Klikni zapisnik zajema podprte ukrepe in prekinitve; ne snemajo splošnega vnosa besedila. Preverite in potrdite predlog pred sprejetjem. Spremembe v zunanji aplikaciji lahko onemogočajo izbire. Procesna izolacija omejuje nesreče in preklic, vendar ni varnostna peska.

## Plugin

Odprite razširitve za preverjanje paketa pred uvozom. Struktura paketa, manifesti in hashes so preverjeni. Uvoz se začne z invalidnostjo. Preberite izjavljene ukrepe in izrecno zaupajte izdajatelju, preden omogočite. Plugins deluje z dovoljenji uporabnika v ločenem procesu; ta proces je **ne pesek**. Namestite samo pakete, ki jih zaupate.

Upravljavec podpira izvoz, onemogočanje, odstranjevanje in obnovitev/odstranjevanje prejšnje različice. Izgubljene odvisnosti blokirajo povabilo. Primerni paket zagotavlja računovodstvo in lokalno-državne ukrepe. Podprta shema je dokumentirana podsestava JSON-Schema, ne pa vsaka možna funkcija JSON-Šema. Na trgu ni javnega plugina.

## Mobilni panel

1. Odprite Mobile Panel, izberite lokalni IP in izrecno izberite profile, ki jih želite prikazati. Privzeto naslov za vrtenje je na voljo le na samem računalniku; izberite ustrezno naslov LAN za telefon ali tablico.
2. Začnite s panelom in preberite naslov HTTPS, potrdite prstni odtis in enostavno uporabite šest-digitalni kodo.
3. Na eni napravi v isti zaupani omrežju, odprite naslov. Primerjajte SHA-256 prstni odtis prek zaupanja pred zaupanjem lokalnemu certifikatu. Aplikacija ne namesti zaupanja vrednega potrdila samodejno.
4. Par uporabite prikazani kodo in preizkusite eno neškodljivo ploščo. Za dostop do LAN je morda potrebna ročna dovoljenja Windows Firewall. Ne izpostavljajte pristanišča internetu.
5. Zaustavite ploščo, ko je končana, ali prek aplikacije preklicate stranke.

Zasebni strežniki so zaščiteni z Windows DPAPI; potrdila strank so shranjena kot hash. Spreminjanje IP ali podaljšanje potrdila lahko zahteva ponovno zaupanje. Panel prikazuje stanje delovanja in preverjanja, ne vsak možen živ senzor. Razpored brskalnika in lokalni HTTPS preverjanja ne nadomeščajo resničnega sprejemanja telefona/tableta LAN.

## lokalni pomočnik

Ta opcijski eksperimentalni asistent je privzeto onemogočen. Odprite ga iz orodij, ga izrecno omogočite, izberite delovni čas/model in dodajte le vire, ki jih želite upoštevati. Nastavite cilj, ustvarite projekt, preverite neuspešne zahteve in prenesete rezultat na običajen urednik. Nič se ne izvaja samodejno; shranjevanje in delovanje ostanejo izrecni ukrepi. Ne obstaja samodejni padec časa.

| Runtime v času | nastavitev |
| --- | --- |
| Ollama | Ločen lokalni servis in lokalno nameščen model na `127.0.0.1:11434`. Izjavljeni modeli za daljavo / oblak so zavrnjeni. |
| LM Studio | Lokalni strežnik z lokalno naloženim modelom in podporo za shemo JSON na `127.0.0.1:1234`. Ne omogočite daljinskega usmerjanja za lokalno obdelavo. |
| llama.cpp | Lokalni `llama-server` in združljiv model z podporo za shemo JSON na `127.0.0.1:8080`. |
| ONNX Čas delovanja GenAI CPU | Zaupanja vredna lokalna GenAI besedilna miza; izberite `genai_config.json`. Inference uporablja povezani proces brez HTTP. |

HTTP stranke uporabljajo le loopback, ne uporabljajo proxy in ne sledijo preusmeritvam. MAKRIVO ne more zanesljivo določiti, ali združljiv lokalni strežnik internalizira daljinsko storitev. Avtentizirani lokalni strežniki v tej različici niso konfigurirani. Modelne teže, Python in lokalni inference strežniki se ne prenesejo ali namestijo samodejno.

ONNX paketi potrebujejo `tokenizer.json` in veljavni šabloni za klepet. Podprte predloge v `tokenizer_config.json` so vrstica `chat_template`, slovar z vrsticami `default` ali nepogrešljiv seznam imen/predvidenega predloga. Če je predloga odsotna ali nič, se `chat_template.jinja` uporablja kot padec. Prosti, dvomljivi, čezmerni ali nepravilno vpisani predlogi so zavrnjeni. Arbitrični ONNX datoteke, modeli vstavljanja/vidovanja in paketi samo GPU niso izmenjavljivi z združljivimi GenAI besedilnimi modeli. Zvezdna različica GenAI je 0.17.1, CPU. Uporabite samo zaupane modele; procesno ločevanje ni pesek.

Asistent podpira izbrane programe brez argumentov, izbrane dokumente in spletne strani ter mikrofon mute, ko je izrecno zahtevan. Ne ustvarja sporočil, zmenkov, samovoljnih skriptov, brezplačnih klikov, prenosov ali preklopov slušalke. Cilj in imena virov se pošljejo v izbrano lokalno časovno obdobje; poti virov in vsebine datoteke niso ločeno. Namen, ki ga določi uporabnik, lahko vsebuje osebne podatke. Obnašanje omrežja lokalnega delovnega časa ostaja odgovornost uporabnika.

V tem primeru ONNX Zahtevek proračuna je 90 sekund, vključno z modelom, do 3,072 hitro token in 1,024 ustvarjeni tokeni, ki so omejeni tudi na kontekst modela. zlonamerni/zlomljeni JSON in neznani ID-ji so zavrnjeni. preklic ali blizu zaključka zahteve. naključni model testirajo izvedbo, ne koristno načrtovanje kakovosti ali prihranek časa. Lokalni eksperimentni logi vsebujejo identifikacije dogodkov in časov, ne ciljno besedilo ali vsebino dokumenta; niso samodejno preneseni.

Kasnejša ocena malega modela Instruct je ustvarila nič veljavne načrte v štirih nemških/angleških preskusih. Native inference je delal, vendar model ni dosegel stroge pogodbe o načrtovanju. To je negativen rezultat kakovosti, ne priporočeni model ali dokazano uporaben pomočnik. Oglejte si datum lokalnega časovnega testa za natančno pregled in omejitve modela.

## Uvoz, izvoz, sinhronizacija in varnostna kopija

Preverite uvozne napovedi, preden zamenjate konfiguracijo. Uvožene ukrepe se začnejo onemogočiti, prenosna sinhronizacija pa odstrani hardware-specific povezave. Ponovite naprave in omogočite ukrepe namerno po pregledu. Pregled besedila/akcijskega vsebine pred delitvijo izvoza; izključitev gesel ne naredi vseh uporabnikov ustvarjenih vsebin občutljivih.

Sinkronizacija profila izmenjuje datoteke v izbranem mapi, opcionalno upravlja obstoječi odjemalec v oblaku. MAKRIVO ne zagotavlja nobenega računa v oblaku in ne shrani nobenih verodostojnosti v oblaku Revizija preprečuje tiho prepisovanje. Vključevanje vsebin je izrecna izbira. Nadgradite konfiguracijo pred uvozom zamenjave. Privzeta konfiguracija je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; zgodovinsko ime imenika je ohranjeno za združljivost. Zaprite aplikacijo, preden jo ročno kopirate / obnovite. Shranjene odobritve SDK in OS-zaščitene potrdila so morda potrebne za ponovno uporabo na drugem računalniku.

## reševanje težav

| Simptomi | Kaj preveriti |
| --- | --- |
| kratko nič ne dela | Uporablja se aplikacija, povezave niso prekinjene, pravilni aktivni profil, shranjena/ omogočena plošča, konflikti in razpoložljivi cilji. |
| Ciljno okno ni mogoče aktivirati | Ročno ga aktivirajte. Windows omejitve prednosti in privilegijske razlike lahko blokirajo avtomatizacijo. |
| Posebni ključ ni odkril | Podpora za prodajalce / SDK / model; konfigurirajte lastne kontrolnike, da lahko izdajo podprt standardni ključ. |
| Druga tipka je tudi običajno | Pričakovan z Raw Input. Izključno zaviranje ni izvedena za proizvodnjo. |
| Avdio ali svetlost ni na voljo | Obnovite naprave; preverite izbrane identitete, končne seje, podporo WMI/DDC/CI in omejitve dok/kabel. |
| OBS offline | WebSocket v5 omogočen, pravilna končna točka/povezava, dostopna storitev in veljavna imena prizora/vnosa. |
| Avtomatizacija ne uspe | Vključena je družinska gostiteljica, edinstven veljavni selektor, dostopna omogočena ciljna oprema, podprta nadzorna oprema in dovoljenja. Odstranite prehodni čas samo po pregledu cilja. |
| Mobilna plošča nepremičnina | LAN namesto naslovov za vrtenje, enake mreže, strežnika, odobrenih profilov, potrditev prstnih odtisov/trusta in ročnih dovoljenj za ogenj. |
| Plugin ne uspe | Celovitost paketa, izrecno zaupanje / dovoljeno stanje, odvisnosti, združljivost protokola in log izvedbe. |
| Vprašanje ne uspe | Pravilno izbrani lokalni čas delovanja, storitev/model pripravljen, podprta shema JSON, zaupanja vredna mape modelov in omejitve zahteve. |
| Razporeditev ali besedilo se zdi napačno | Ločite jezik uporabnika od geometrije tipkovnice; preoblikujte okno, preverite razsežnost zaslona in poročate o natančni strani/jezici/izdelavi. |

Za uporabno poročilo o napaki vključujejo različico Windows, gradnjo aplikacij/obvezo, jezik, razsežnost zaslona, strojni model, minimalno zaporedje in ustrezno urejeno dnevno zvezo. Odstranite osebne poti, vsebino in potrdila pred objavo.

## Gradnja, testiranje in pakiranje

Uporabite SDK, ki je nameščen v `global.json`. Od korenine do repozitorij:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integracijski testi lahko odprejo izolirane okne in začasno konfigurirajo/oddajajo podprte povezave dobavitelja. Uporabljajte jih brez drugega upravljavca, ki ima te obveznosti. Privzeto svetlost / avdio testi so samo branje; izrecno strojno krožno testiranje so ločeni. Mock protokolov, posnetkov zaslona in naključnih grafikonov ne smejo biti predstavljeni kot resnično kakovostno opremo / model.

Celoten izdajatelj združuje gostitelje, mobilne sredstva, primerni plugin, čas delovanja ONNX in razpoložljive obvestila o odvisnosti. Upoštevajte svoje pododdelke. Uspešna gradnja ni podpisano namestitev ali odobritev za izdajo. Izvor in gradnje se lahko razvijejo ločeno; primerjajte snemanje dokumentacije in se zavezujete nameščenemu različicam.

## Licenca, zasebnost in status sproščanja

Namenjeni komercialni model je enkratna nakupa s stalno uporabo kupljene velike različice; končna cena, doplačilo naprave in pogoji nadgradnje morajo biti določeni v dejanskih prodajnih pogojih. Sistem nakupa/aktiviranja proizvodnje ni določen s to priročnico. Za vsakodnevno nadzorovanje ni potrebnega računa v oblaku.

Ne distribuirajte lastnih binarnih SDK brez dokazanega dovoljenja. Ohrani licenco in obvestila tretjih oseb za vsako pošiljano odvisnost, vključno z paketom .NET in ONNX. Modelske teže imajo svoje pogoje. Datirana revizija dovoljenja prenaša nekatere nove odvisnosti in ne pomeni razrešnice za te dodatke. Podjetje, ki je javno, sam ne dodeli splošnega odprtega vira.

Resnična oprema ponudnika, delovanje telefona/tableta LAN, mešani monitor/DPI prehodov, dolgotrajna uporaba, popolna jezikovna pregleda, ekskluzivna podpora za voznike in uporabna kakovost AI načrtovanja ostane ločeno delo sprejemanja. Nikoli ne odobri samega prisotnosti.

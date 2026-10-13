<!-- generated-doc locale=sl-SI source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../HANDBOOK.md)

# MAKRIVO — Uporabniški priročnik

Proizvajalec: **Aivana**. Stanje dokumentacije: **2026-10-12**. Ta priročnik opisuje trenutno implementacijo v delovnem drevesu. Eksperimentalne integracije in funkcije, odvisne od strojne opreme, so izrecno označene. Zgodovinska poročila opisujejo stanje na navedeni datum in niso zagotovilo za novejšo gradnjo.

## Dokumentacija in jeziki

Z izbiro jezika dokumentacije odprite celoten priročnik, pregled funkcij, odpravljanje težav, razvojne zapiske, raziskave in zgodovinska poročila o preizkusih v katerem koli od 27 jezikov aplikacije. Ustvarjeni prevodi so strojno podprti in strukturno preverjeni; vseh niso pregledali materni govorci. Tehnični identifikatorji in izvedljivi primeri ostanejo nespremenjeni. Izvirni vir ostane na voljo za primerjavo. Prevedena analiza licenc je informativna in ne nadomesti izvirnih pravnih pogojev tretjih oseb ali končne pogodbe EULA.

## Namestitev in prvi zagon

Začetna ciljna platforma je Windows 11 x64. Vse datoteke in podimenike distribucije ohranite skupaj. Zaženite `MAKRIVO.exe`. Kopija izvorne kode ni namestitveni program. Samostojni paketi vključujejo izvajalno okolje .NET; paketi, odvisni od ogrodja, zahtevajo ustrezni izvajalni okolji .NET 10 Desktop in ASP.NET. Za ustrezne funkcije morajo biti vključeni ONNX in spremljevalni gostiteljski procesi.

1. Zaženite MAKRIVO in v nastavitvah izberite jezik vmesnika. Samodejna izbira sledi jeziku Windows. Razporeditev tipkovnice je ločena nastavitev.
2. Izberite profil in nato v katalogu dejanj z iskanjem dodajte ploščico. Dajte ji prepoznavno ime in izberite njena dejanja.
3. Preglejte korake in po potrebi dodelite bližnjico. Izogibajte se kombinacijam, ki jih že uporablja Windows ali ciljna aplikacija.
4. Shranite osnutek. Neshranjene spremembe niso del shranjene konfiguracije.
5. Preizkusite z neškodljivimi dejanji, kot je odprtje dokumenta in preverite povratne informacije o izvedbi.

Zapiranje glavnega okna običajno pusti aplikacijo v območju za obvestila. Dvokliknite njeno ikono, da jo znova odprete, ali uporabite meni ikone za izhod. Zagon skupaj z Windows je izbiren in sprva onemogočen. Gumb za premor začasno ustavi vezave; ikona prikaže oznako premora. Tekoče makre prekličite prek uporabniškega vmesnika, ikone v območju za obvestila ali nastavljene bližnjice za preklic; privzeta je `Ctrl+Alt+Escape`.

## Ploščice, dejanja in ukazna paleta

Ploščice pripadajo profilom in lahko vsebujejo več zaporednih korakov. Urejevalnik podpira shranjevanje/zavračanje, predogled, razveljavitev/ponovitev in spreminjanje vrstnega reda korakov. Katalog vsebuje prednastavitve za programe, dokumente, URL-je, besedilo, odložišče, bližnjice, predstavnost, zvok, okna, sistemska dejanja in podprte integracije. Prednastavitev, ki zahteva parametre, se odpre kot osnutek.

Odprite paleto s `Ctrl+K`, ko ima MAKRIVO fokus. Iščite shranjena dejanja, prednastavitve, preklapljanje profilov in orodja. Smerne tipke izbirajo; Enter aktivira; `Ctrl+Enter` ureja podprti element; Escape zapre. Nekateri pripravljeni ukazi se izvedejo takoj, nastavljena dejanja pa zahtevajo pregled. Nastavite lahko ločeno globalno bližnjico za paleto; nove konfiguracije uporabljajo `Ctrl+Shift+Space`.

Koraki za besedilo in odložišče podpirajo `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` in `{{user}}`. Oblika datuma je `yyyy-MM-dd`; oblika časa je `HH:mm`. Neznane spremenljivke ostanejo nespremenjene. Odložišče se bere samo na zahtevo. Dejanje odložišča zapiše besedilo, ne da bi ga samodejno prilepilo.

Dejanja, zagnana iz uporabniškega vmesnika, lahko ciljajo zadnjo zunanjo aplikacijo. Če cilj izgine ali Windows prepreči aktiviranje v ospredju, cilj aktivirajte ročno in poskusite znova. Uspešna dostava pritiska tipke ne dokazuje, da je cilj dokončal nalogo.

## Profili in nastavitev delovnega mesta

Profili se lahko ujemajo z imenom procesa in delom naslova okna. Če sta nastavljena oba pogoja, se morata ujemati oba. Vrstni red profilov razreši več sočasnih ujemanj; privzeti profil se uporabi, če ni drugih ujemanj. Samodejno izbiro izrecno omogočite. Zaklep profila prepreči samodejne spremembe v trenutni seji.

Odprite nastavitev delovnega mesta v orodjih ali paleti. Izberite programe, dokumente, spletna mesta, ciljno zvočno napravo/zaslon ali sceno OBS. Pomočnik pripravi profile za delovno mesto, sestanke, pretakanje, raziskovanje ali projekte. Pred shranjevanjem preglejte ustvarjene korake. Nastavitev ne izvede pripravljenih dejanj.

## Makri, pogoji in snemanje

Makri izvajajo korake po vrsti ter podpirajo čakanje, ponovitve in preklic. Splošna omejitev ponovitev je 100. Pred zagonom dejanj, ki vnašajo besedilo v drugo aplikacijo ali spreminjajo stanje sistema, uporabite predogled.

Čakanje na ciljno okno sprejme proces, podniz naslova in časovno omejitev od 1 do 600000 ms. Potreben je vsaj en filter. Ujemajoče se vidno okno se mora odzvati in postati okno v ospredju, preden se nadaljujejo naslednji koraki. Pripravljeno okno ne dokazuje, da se je dokument ali storitev dokončno naložila.

Urejevalnik poteka dela dodaja pogoje za obstoj datoteke, izvajanje procesa in naslov okna. Neizpolnjen pogoj preskoči korak in lahko preskoči dodatne korake naprej. Za alternativno vejo uporabite negacijo pogoja. Premišljeno izberite ustavitev ali nadaljevanje ob napaki; nadaljevanje lahko pusti poznejše korake brez pričakovanih predpogojev.

Snemalnik dejanj zajema kombinacije tipk za bližnjice in čakanja, ne običajnega tipkanja. Podpira bližnjice z modifikatorskimi tipkami in funkcijske tipke. Escape ustavi snemanje. Preglejte seznam in ga prevzemite kot neshranjen osnutek ploščice, nato ga izrecno shranite. Snemanje klikov uporabniškega vmesnika je ločeno orodje, opisano spodaj.

## Tipkovnice, geste in plasti

Stran tipkovnice navaja zaznane vmesnike in referenčne razporeditve. Ena fizična tipkovnica lahko izpostavi več vmesnikov. Izberite pravi jezik in geometrijo; samo ime izdelka ne določa razporeditve ISO ali ANSI.

Standardne globalne bližnjice veljajo za vse tipkovnice. Vezave Raw Input za posamezno napravo opazujejo določeno napravo in kodo tipke, vključno s podprtimi funkcijskimi in predstavnostnimi tipkami. **Raw Input ne prepreči običajnega vnosa v Windows. Izključna druga tipkovnica ni pripravljena za produkcijsko uporabo.** Še vedno je potreben podpisan, licenciran in preizkušen zaledni sistem s filtrirnim gonilnikom; MAKRIVO ne namesti nobenega.

Podprte vezave posebnih tipk lahko uporabijo takojšen pritisk, kratek pritisk, držanje ali dvojni pritisk. Tipke za začasno plast med držanjem izberejo drugo preslikavo in ob sprostitvi obnovijo osnovno plast; tipke za izbiro plasti ne izvedejo lastnega makra. Pragovi držanja segajo od 150 do 5000 ms; časovna okna dvojnega pritiska od 150 do 1000 ms. Te preslikave so ločene od običajnih globalnih bližnjic.

Čarovnik za posebne tipke zazna združljive tipke, ne da bi med zajemom izvajal dodeljena dejanja. Diagnostika pojasni nedostopne naprave, začasno ustavljene vezave, neujemanja profilov in spore. Prekrivni prikaz kaže izbrano razporeditev in vezave; privzeta bližnjica, ki jo držite za prikaz, je `Ctrl+Alt+F1`.

| Integracija | Zahteve in omejitve |
| --- | --- |
| Corsair | Zagnan iCUE, dostop do SDK in združljiv model. G1–G6 in podprte funkcije RGB; lastniški kontrolniki Fn, koleščka, zaklepanja in profilov običajno niso izpostavljeni prek API-ja G-key. |
| Logitech G | Izvirne lokalne knjižnice x64 G-key/LED SDK zahtevajo izrecno odobritev. Dogodki G-key ne omogočajo enoličnega razlikovanja več enakih fizičnih tipkovnic. |
| Razer | Chroma RGB in standardni vnos; po potrebi preslikajte lastniške kontrolnike v Synapse. |
| SteelSeries | GameSense RGB in standardni vnos; po potrebi preslikajte lastniške kontrolnike v GG. |
| Wooting | Združljiv izvirni lokalni SDK x64 za analogne vrednosti in pragove; običajni vnos HID ostane aktiven. |
| Druge tipkovnice | Standardne bližnjice ali podprte fizične vezave kod tipk. Fn in lastniški kontrolniki so odvisni od modela in nastavitev proizvajalca. |

Raziskava proizvajalcev opisuje dokumentirane zmogljivosti, ne implementirane ali preizkušene združljivosti z vsako navedeno napravo. Novi adapterji še vedno potrebujejo sprejemno preverjanje na pravi strojni opremi. Binarnih datotek SDK ne prenašajte iz nezaupanja vrednih virov; pravice do distribucije SDK so ločene od tehnične združljivosti.

## Zvok, zasloni in OBS

Zvočni koraki nastavijo glasnost od 0 do 100 odstotkov ter utišajo, vklopijo zvok ali preklopijo utišanje podprtih izhodnih oziroma mikrofonskih končnih točk. Izberite določeno končno točko ali privzeto napravo Windows. Manjkajoči shranjeni cilj povzroči napako, namesto da bi aplikacija tiho upravljala drugo napravo. Po ponovni povezavi osvežite naprave. Upravljanje končnih točk ne pomeni podpore za neodvisni mešalnik posameznih aplikacij.

Dejanja zaslona povečajo/zmanjšajo svetlost za 10 odstotnih točk ali nastavijo vrednost od 0 do 100. Vgrajeni zasloni uporabljajo podprte vmesnike WMI; zunanji monitorji zahtevajo podporo DDC/CI in običajno vklop te možnosti v meniju monitorja. Priklopne postaje, adapterji in kabli lahko preprečijo upravljanje. Izberite vse podprte zaslone ali en monitor. Ponovna povezava lahko spremeni razpoložljive identifikatorje; preverite shranjeni cilj. Manjkajoči odčitki so prikazani kot nedostopni, ne kot izmerjene vrednosti.

OBS zahteva omogočen WebSocket v5 v orodjih OBS. Nastavite končno točko, preizkusite povezavo in pred shranjevanjem naložite imena scen/vhodov. Podprta dejanja vključujejo preklapljanje scen, zagon/ustavitev/preklop pretakanja in snemanja, premor/nadaljevanje snemanja ter utišanje vhoda. Gesla lahko shranite v Windows Credential Manager in so izključena iz izvozov konfiguracije. Uspešen ukaz OBS ne dokazuje vseh nadaljnjih učinkov pretakanja.

## Preverjanje rezultatov in zgodovina izvajanja

Za primerne korake omogočite preverjanje rezultata. Aplikacija razlikuje predhodna preverjanja, potrjene učinke, napake, preklic in učinke, ki jih ni mogoče preveriti. Obstoj procesa ne pomeni pripravljenosti aplikacije. Relativnim spremembam, bližnjicam in vnosu besedila ni mogoče pripisati splošno zagotovljenega uspeha. Ob neuspehu dejanja preglejte dnevnik dejavnosti in zgodovino preverjanja. Pred ponovnim zagonom celotnega makra odpravite napake konfiguracije.

## Avtomatizacija vmesnika in snemanje klikov

Dodajte korak avtomatizacije uporabniškega vmesnika, izberite operacijo in po prikazani zakasnitvi zajemite cilj pod kazalcem. Sam zajem ne izvede ničesar. Preverite selektor in zahtevajte enolično ujemanje. Podprte operacije vključujejo `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` in `WaitState`. Polja za gesla, onemogočeni kontrolniki in dvoumni selektorji so zavrnjeni.

Avtomatizacija uporabniškega vmesnika poteka v omejenem spremljevalnem procesu. Odvisna je od podpore za dostopnost Windows v ciljni aplikaciji in dovoljenj. Snemalnik klikov zajema podprta dejanja in premore; ne snema splošnega vnosa besedila. Pred prevzemom preglejte in preverite osnutek. Spremembe vmesnika zunanje aplikacije lahko razveljavijo selektorje. Izolacija procesa omejuje posledice zrušitev in omogoča omejevanje izvajanja ob preklicu, vendar ni varnostni peskovnik.

## Vtičniki

Odprite razširitve in pred uvozom preglejte paket. Preverijo se struktura paketa, manifest in zgoščene vrednosti. Uvoženi vtičnik je sprva onemogočen. Pred omogočanjem preberite navedena dejanja in izrecno zaupajte izdajatelju. Vtičniki se izvajajo z vašimi uporabniškimi dovoljenji v ločenem procesu; ta proces **ni peskovnik**. Nameščajte samo pakete, ki jim zaupate.

Upravljalnik podpira izvoz, onemogočanje, odstranitev ter obnovitev/zavrnitev prejšnje različice. Manjkajoče odvisnosti preprečijo klic. Vzorčni paket ponuja odštevanje in dejanja lokalnega stanja. Podprta shema je dokumentirana podmnožica JSON-Schema, ne vse možne funkcije JSON-Schema. Javne tržnice vtičnikov ni.

## Mobilna plošča

1. Odprite mobilno ploščo, izberite lokalni IP in izrecno izberite profile za dostop. Privzeti povratni naslov loopback je dosegljiv samo na samem računalniku; za telefon ali tablico izberite ustrezen naslov LAN.
2. Zaženite ploščo in preberite njen naslov HTTPS, prstni odtis potrdila ter enkratno šestmestno kodo za seznanjanje.
3. Na napravi v istem zaupanja vrednem omrežju odprite naslov. Preden zaupate lokalnemu potrdilu, primerjajte prstni odtis SHA-256 prek zaupanja vrednega kanala. Aplikacija ne namesti zaupanja vrednega potrdila samodejno.
4. Seznanite napravo s prikazano kodo in preizkusite eno neškodljivo ploščico. Za dostop LAN bo morda potrebna ročna odobritev v Windows Firewall. Vrata ne smejo biti izpostavljena internetu.
5. Ko končate, ustavite ploščo ali prek aplikacije prekličite dostop seznanjenim odjemalcem.

Zasebni strežniški ključi so zaščiteni z Windows DPAPI; poverilnice odjemalcev so shranjene kot zgoščene vrednosti. Sprememba IP ali obnovitev potrdila lahko zahteva ponovno vzpostavitev zaupanja. Plošča kaže stanja dejanj in preverjanj, ne vseh možnih sprotnih senzorjev. Preverjanje postavitve v brskalniku in lokalnega HTTPS ne nadomesti sprejemnega preizkusa na pravem telefonu/tablici prek LAN.

## Lokalni pomočnik za sestanke z umetno inteligenco

Ta izbirni eksperimentalni pomočnik je privzeto onemogočen. Odprite ga v orodjih, ga izrecno omogočite, izberite izvajalno okolje/model in dodajte samo vire, ki naj jih upošteva. Navedite cilj, ustvarite osnutek, preglejte nerešene zahteve in prenesite rezultat v običajni urejevalnik. Nič se ne izvede samodejno; shranjevanje in zagon ostaneta izrecni dejanji. Samodejnega preklopa na drugo izvajalno okolje ni.

| Izvajalno okolje | Nastavitev |
| --- | --- |
| Ollama | Ločena lokalna storitev in lokalno nameščen model na `127.0.0.1:11434`. Deklarirani oddaljeni modeli/modeli v oblaku so zavrnjeni. |
| LM Studio | Lokalni strežnik z lokalno naloženim modelom in podporo za shemo JSON na `127.0.0.1:1234`. Za lokalno obdelavo ne omogočite oddaljenega usmerjanja. |
| llama.cpp | Lokalni `llama-server` in združljiv model s podporo za shemo JSON na `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Zaupanja vredna združljiva lokalna mapa besedilnega modela GenAI; izberite `genai_config.json`. Sklepanje uporablja priloženi spremljevalni proces brez HTTP. |

Odjemalci HTTP uporabljajo samo loopback, ne uporabljajo posredniških strežnikov in ne sledijo preusmeritvam. MAKRIVO ne more zanesljivo ugotoviti, ali združljiv lokalni strežnik interno kliče oddaljeno storitev. Lokalnih strežnikov z avtentikacijo v tej različici ni mogoče nastaviti. Uteži modelov, Python in lokalni strežniki za sklepanje se ne prenesejo ali namestijo samodejno.

Paketi ONNX potrebujejo `tokenizer.json` in veljavno predlogo za klepet. Podprte oblike predlog v `tokenizer_config.json` so niz `chat_template`, slovar z nizom `default` ali nedvoumen seznam poimenovanih/privzetih predlog. Če predloga manjka ali je null, se kot nadomestilo uporabi `chat_template.jinja`. Prazne, dvoumne, prevelike predloge in predloge napačnega podatkovnega tipa so zavrnjene. Poljubne datoteke ONNX, modeli za vektorske vložitve/vid in paketi samo za GPU niso zamenljivi z združljivimi besedilnimi modeli GenAI. Priložena različica izvajalnega okolja GenAI je 0.17.1, CPU. Uporabljajte samo zaupanja vredne modele; ločevanje procesov ni peskovnik.

Pomočnik podpira izbrane programe brez argumentov, izbrane dokumente in spletna mesta ter utišanje mikrofona, kadar je izrecno zahtevano. Ne ustvarja sporočil, sestankov, poljubnih skriptov, poljubnih klikov, prenosov ali preklapljanja slušalk. Cilj in imena virov se pošljejo izbranemu lokalnemu izvajalnemu okolju; poti virov in vsebina datotek se ne posredujejo posebej. Cilj, ki ga vnese uporabnik, lahko sam vsebuje osebne podatke. Omrežno vedenje lokalnega izvajalnega okolja ostaja odgovornost uporabnika.

Časovna omejitev zahteve ONNX je 90 sekund, vključno z nalaganjem modela, z največ 3072 vhodnimi žetoni in 1024 ustvarjenimi žetoni, dodatno omejenimi s kontekstom modela. Nepravilno oblikovan/odrezan JSON in neznani identifikatorji virov so zavrnjeni. Zahtevo končate s preklicem ali zapiranjem. Preizkus izvajalnega okolja z naključnim modelom dokazuje izvajanje, ne uporabne kakovosti načrtovanja ali prihranka časa. Lokalni dnevniki poskusov vsebujejo identifikatorje dogodkov in čase, ne besedila cilja ali vsebine dokumentov; ne nalagajo se samodejno na strežnik.

Poznejše vrednotenje majhnega modela Instruct ni ustvarilo nobenega veljavnega načrta v štirih nemških/angleških preizkusih. Nativno sklepanje je delovalo, vendar model ni izpolnil strogih zahtev načrtovanja. To je negativen rezultat kakovosti, ne priporočilo modela ali dokazano uporaben pomočnik. Za natančno revizijo modela in omejitve glejte datirano poročilo o preizkusu lokalnega izvajalnega okolja.

## Uvoz, izvoz, sinhronizacija in varnostno kopiranje

Pred zamenjavo konfiguracije preglejte predogled uvoza. Uvožena dejanja so sprva onemogočena, prenosna sinhronizacija pa odstrani vezave, odvisne od strojne opreme. Po pregledu znova dodelite naprave in premišljeno omogočite dejanja. Pred deljenjem izvoza preglejte vsebino besedila/dejanj; izključitev gesel ne pomeni, da vsa uporabniško ustvarjena dejanja ne vsebujejo občutljivih podatkov.

Sinhronizacija profilov izmenjuje datoteke v izbrani mapi, ki jo lahko upravlja vaš obstoječi odjemalec za oblak. MAKRIVO ne zagotavlja računa v oblaku in ne shranjuje poverilnic za oblak. Revizije preprečujejo tiho prepisovanje. Vključitev vsebine dejanj je izrecna izbira. Pred nadomestnim uvozom izdelajte varnostno kopijo konfiguracije. Privzeta konfiguracija je `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; zgodovinsko ime imenika je ohranjeno zaradi združljivosti. Pred ročnim kopiranjem/obnavljanjem zaprite aplikacijo. Shranjene odobritve SDK in poverilnice, zaščitene z operacijskim sistemom, bo morda treba na drugem računalniku ustvariti znova.

## Odpravljanje težav

| Simptom | Kaj preveriti |
| --- | --- |
| Bližnjica ne naredi ničesar | Aplikacija deluje, vezave niso začasno ustavljene, pravilen aktivni profil, shranjena/omogočena ploščica, spori bližnjic in razpoložljiv cilj. |
| Ciljnega okna ni mogoče aktivirati | Aktivirajte ga ročno. Omejitve aktiviranja v ospredju v Windows in razlike v pravicah lahko preprečijo avtomatizacijo. |
| Posebna tipka ni zaznana | Podpora storitve proizvajalca/SDK/modela; če je mogoče, nastavite lastniške kontrolnike tako, da pošljejo podprto standardno tipko. |
| Druga tipkovnica tudi običajno tipka | To je pričakovano pri Raw Input. Izključno preprečevanje vnosa ni implementirano za produkcijsko uporabo. |
| Zvok ali svetlost ni na voljo | Osvežite naprave; preverite izbrani identifikator, seje končnih točk, podporo WMI/DDC/CI in omejitve priklopne postaje/kabla. |
| OBS offline | Omogočen WebSocket v5, pravilna končna točka/geslo, dosegljiva storitev in veljavna imena scen/vhodov. |
| Avtomatizacija uporabniškega vmesnika ne uspe | Vključen spremljevalni gostiteljski proces, enoličen veljaven selektor, dostopen omogočen cilj, podprt kontrolnik in dovoljenja. Po začasni časovni prekoračitvi poskusite znova šele po pregledu cilja. |
| Mobilna plošča ni dosegljiva | Naslov LAN namesto loopback, isto omrežje, delujoč strežnik, odobreni profili, prstni odtis potrdila/zaupanje in ročna dovoljenja požarnega zidu. |
| Vtičnik ne uspe | Celovitost paketa, izrecno zaupanje/omogočeno stanje, odvisnosti, združljivost protokola in dnevnik izvajanja. |
| Zahteva umetni inteligenci ne uspe | Pravilno izbrano lokalno izvajalno okolje, pripravljena storitev/model, podprta shema JSON, zaupanja vredna združljiva mapa modela in omejitve zahtev. |
| Razporeditev ali besedilo je videti napačno | Ločite jezik vmesnika od geometrije tipkovnice; spremenite velikost okna, preverite povečavo zaslona in navedite točno stran/jezik/gradnjo. |

Za uporabno poročilo o napaki navedite različico Windows, gradnjo aplikacije/revizijo kode, jezik, povečavo zaslona, model strojne opreme, minimalno zaporedje in ustrezen dnevnik z odstranjenimi občutljivimi podatki. Pred objavo odstranite osebne poti, vsebino dejanj in poverilnice.

## Gradnja, preizkušanje in priprava paketov

Uporabite SDK, katerega različica je določena v `global.json`. Iz korenskega imenika repozitorija:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Integracijski preizkusi Windows lahko odprejo izolirana okna ter začasno nastavijo/sprostijo podprte vezave proizvajalcev. Izvajajte jih brez drugega upravljalnika, ki uporablja te vezave. Privzeta preverjanja svetlosti/zvoka samo berejo podatke; izrecni preizkusi spreminjanja in obnovitve strojne opreme so ločeni. Simuliranih protokolov, izrisanih posnetkov zaslona in naključnih grafov za sklepanje ne smete predstavljati kot sprejemno preverjanje prave strojne opreme ali kakovosti modela.

Celotni postopek priprave paketa zbere spremljevalne gostiteljske procese, sredstva za mobilno ploščo, vzorčni vtičnik, izvajalno okolje ONNX in razpoložljiva obvestila o odvisnostih. Ohranite njegove podimenike. Uspešna gradnja ni podpisan namestitveni program ali pravna odobritev izdaje. Izvorna koda in gradnje se lahko razvijajo ločeno; primerjajte stanje dokumentacije in revizijo kode z nameščeno različico.

## Licenciranje, zasebnost in stanje izdaje

Predvideni poslovni model je enkratni nakup s trajno uporabo kupljene glavne različice; končno ceno, dovoljeno število naprav in pogoje nadgradenj je treba določiti v dejanskih prodajnih pogojih. Ta priročnik ne vzpostavlja produkcijskega sistema za nakup/aktiviranje. Za vsakodnevno upravljanje račun v oblaku ni potreben.

Lastniških binarnih datotek SDK ne distribuirajte brez dokazanega dovoljenja. Ohranite licence in obvestila tretjih oseb za vsako priloženo odvisnost, vključno s paketi .NET in ONNX. Uteži modelov imajo svoje pogoje. Datirana revizija licenc je bila opravljena pred nekaterimi novimi odvisnostmi in ne pomeni odobritve teh dodatkov. Javna dostopnost repozitorija sama po sebi ne podeljuje splošne odprtokodne licence.

Prava strojna oprema proizvajalcev, delovanje telefona/tablice prek LAN, prehodi med različnimi monitorji/DPI, dolgotrajna uporaba, popoln jezikovni pregled, podpora izključnega gonilnika in uporabna kakovost načrtovanja z umetno inteligenco še vedno zahtevajo ločeno sprejemno preverjanje. Iz same prisotnosti funkcije nikoli ne sklepajte na odobritev.

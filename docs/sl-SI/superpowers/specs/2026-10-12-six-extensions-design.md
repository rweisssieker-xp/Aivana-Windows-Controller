<!-- generated-doc locale=sl-SI source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: šest razširitev izdelka in tipkovnice adapterji

Datum: 2026-10-12
Status: predlagana zasnova za pregled uporabnika; izvedba ni začela.

## Cilj
Dostava vseh šestih zahtevanih razširitev v obstoječem Windows/WPF aplikaciji, ohranjanje obstoječih profilov, G1/G2 nalog, lokalno delovanje in brezplačno uporabo. Razširite integracijo tipkovnice na Razer in Logitech, z obnovljivo arhitekturo na podlagi zmogljivosti za druge proizvajalce. Ime proizvajalca ali postavitev tipkovnice nikoli ni dokaz, da so lastniški kontrolniki podprti.

## Arhitektura odločitve
Razširitev Aivana.Core s serijaliziranimi pogodbami in validacijo, Aivana.Windows z Windows in prodajalec adapterjev, in Aivana.Desktop z transakcijskimi uredniki in pogledom na stanje. Uporabite ločene spremljevalne projekte za izolacijo vtičnika in lokalno storitev mobilnega panela. Izogibajte se ponovnemu pisanju ali enemu privilegiranemu procesu, ki gostuje vsako razširitev. Obstoječa konfiguracija mora biti nepremičnina; dodajte vrste ukrepov namesto ponovnega naročila njihovih trajnih vrednosti.

Razmislite o alternativah: monolitična implementacija bi zajemala izvajanje strojne opreme, omrežja in delovnega toka; nadomestilo WPF bi dodalo nezakonito migracijsko delo. Modularne razširitve so priporočene.

## 1. Neodvisna tipkovnica makro pad
Skladiščenje povezav po identiteti naprave plus fizični kodo skeniranja in razširjene ključe. Dodajte izrecno opazovanje in ekskluzivne načine, odkrivanje naprave, ključno ujemanje, ponavljajoče se zaviranje, gibe, obdržane plasti in ponovno povezovanje diagnostike. Ohranite glavne/numpad Enter razlike in preizkusite nemške/US postavitve. Povezave so usmerjene na izbrano fizično tipkovnico; dvomljive ponovno povezane identitete zahtevajo ponovno izbiro namesto nadzora nad drugo napravo.

Raw Input Izključno odstranjevanje na napravo zahteva dejansko tipkovnico filtrov; nikoli ne povezuje globalne nizke ravni z Raw Input in zahtevajo zanesljivo odstranitev posebne naprave. zagotavlja nadzornik, sproščanje nujnosti, neuspešno vedenje in namestnik/uninstaller. ne ujemanje varnih vnosov namizja..

Vozniška vrata: izberite podprto, pravilno licencirano in podpisano zavihek, ki je združljiv s ciljem Windows 11 konfiguracijami, ali razvite/podpišite poseben filter. Interception je kandidat za oceno, ne zavezujoče proizvodne odvisnosti: njegova uradna README opisuje testiranje skozi Windows 10 in ločeno komercialno licenco. Dokler se ta vrata ne izpolnijo, mora biti izključni način prikazan kot nedostopen; funkcija ni mogoče razglasiti za popolno.

Sprejem: z dvema fizičnimi tipkovnicami isti ključ izvaja makro in ne proizvaja običajnega vnosa aplikacije samo na izbranem ekskluzivnem napravi; drugi vnosi na tipkovnici ostanejo neuporabljeni. Preverite izklop, nesrečo, preklic, prekinitev in odstranitev voznika na dejanski strojni opremi.

## 2. Vodenje delovnega prostora
Dodajte čarovnika za delovno mesto, srečanje, pretvorbo, raziskave in projektne preklopke. Odkrijte nameščene aplikacije, avdio naprave, monitorje in scene OBS. Uporabniki izberejo cilje in obveznosti; predvidijo rezultate korakov in potrjujejo pomanjkanja zahtev. Shrani le po pregledu, brez dejanj, ki potekajo med nastavitvijo. Ustvarjeni profili ostanejo urejeni v obstoječih urednikih.

Sprejemanje: ustvarite delovni profil brez ročnega urejanja izvedljivih poti ali ID-jev naprave; razveljavitev zapusti nastavitve; pomanjkanje programske opreme ima izvedljivo razlago.

## 3. Preverjena izvršitev
Vnesite preglede, tipirane rezultate po korakih in opcijske pogoje. Razlikujte zahtevane, dokončane, preverjene, neuspešne, odložene, preklicane in nepovratne države. Preverite avdio/mikrofon, svetlost, pripravljenost procesov/stran in stanje OBS z omejenimi asinkronnimi bralci. Kratek/tekstno dostavo ni dokaz, da je ciljna aplikacija sprejela ukaz. Zunanji stranski učinki niso univerzalno reverzibilni; ponujajo nadomestilo le, če je izvedena.

Sprejemanje: izključeni izrecni cilji nikoli ne padejo nazaj; napačno opaženo stanje nikoli ne poroča o preverjenem uspehu; preklic ustavi preiskavo; neuspeh odkrije koraka in okrevanje ukrepa. Zgodovina izvrševanja pozabi besedilne plačilne naloge in skrivnosti.

## 4. Plugins in delljivi paketi
Razvrščeni JSON manifestirajo dejanja, sheme parametrov, državne ponudnike, odvisnosti in zmogljivosti. Izvedite izvedljive vtičnike v ločenem procesu preko omejenega lokalnega IPC z ročnim prekinitvijo, rokami in preklicom. Zavrnite zlonamerne sporočila in nezdružljive različice; ravnanje z nesrečami brez prekinitve upravljavca. Izjave o zmogljivosti so razkritje in posredovanje, ne pa zahteva OS sandboxinga.

Ponuja paketno izvozno/importacijsko predogled, vsebinske hashove, zaščito potovanja, omejitve velikosti, preverjanje različice/odvisnosti, izrecno omogočanje in nadgradnjo. Uvoženi izvedljivi vtičniki se ne smejo izvajati samodejno. Vključite delovni primer plugin s končnim namestitvijo / akcijo / stanje / oddajanje pretoka. Za to objavo ni potrebnih javnih storitev.

Sprejemanje: ustvarjanje, izvoz, namestitev in izvedba realnega primera integracije; prikazati časovno izolacijo, izolacijo nesreče, pomanjkanje odvisnosti, prekoračno zavrnitev in onemogočen uvoz.

## 5 Windows UI avtomatizacija
Dodajte element piker in omejene ukrepe za povabilo, vrednost, izbiro, fokusiranje in čakanje na element / državo. Vztrajajte pri izbiri, ki uporabljajo proces, ID za avtomatizacijo, tip nadzora in kontekst prednikov; izogibajte se samo koordinatnemu snemanju. Prikaži izbirnik tekmo in preizkusite izbirnike samo. Iz registracije so izključene gesla. Obravnajte nepodprte kontrole in omejitve ravni celovitosti Windows izrecno.

Razširite snemanje z pregledljivimi ukrepi UIA, ohranjanje trenutnega snemanja. Ambigujni selektorji ne uspejo namesto da bi kliknili nadomestno. Običajno brskalnik dostopnost drevesa ni obljuba polnega avtomatizacijskega motorja.

Sprejemanje: avtomatizirajte posvečeno aplikacijo za testiranje z stabilnimi selektorji na podlagi sprememb položaja/razsežnosti; testne dvomljivosti, pomanjkljivosti elementov, onemogočenih kontrol, časovnih izhodov in preklica.

## 6. Lokalni telefon / tableta
Poslužite odzivni panel iz storitve opt-in LAN. Izrecno uporabite kratkotrajni enodnevni kodo in izdajate preklicne potrdila, ki so zaščiteni lokalno. Uporabite HTTPS z dokumentiranim lokalnim potrdilom zaupanja/pinning; brez povratnih informacij, ki pošiljajo potrdila v pritožbi. Default za onemogočanje in omejitev konfiguriranih vmesnikov. Validacija izvora, preverjene ukaze, omejitve stopnje in omejena sporočila so obvezna.

Izpostavite dovoljene profile/akcije, stanje v živo, napredek in preklic; uporabite stabilne ID-je in enako izvedbo kot namizne in fizične ključe. Ne izpostavljajte arbitrarnih ukazov, izvedljivih poti, skrivnosti ali namestitve vtičnika. Zagotavlja povezavo, pariranje in preklic zaslona. Ni vključenih nobenih računov v oblaku ali internetnega omrežja.

Sprejemanje: deluje iz realnega telefona/tableta na istem LAN-u; neuporabljeni klienti ne morejo seznaniti ali izvajati dejanj; preklicani tokeni ne uspejo; dvakratni/prevajani ukazi ne delujejo; razpona tiho ne odvzame dejanj.

## Proizvajalec ključavnice

| Adapter / družina | načrtovana integracija | Razloženo mejo |
| --- | --- | --- |
| Corsair | Ohranite vstop iCUE G-klavir in integracijo RGB | Model / programska oprema je še vedno potrebna za odkrivanje zmogljivosti |
| Logitech G | G-key SDK adapter s tiskom/oddajanjem in časom delovanja; razsvetljava skozi ločeno SDK, kjer je podprta | Ne izenačite G HUB odkrivanja z podporo G-key API na vsakem modelu |
| Razer | Adapter za razsvetljavo kroma in usmerjen Synapse-na standardno ključavnico, plus splošni vnos HID | Chroma je razsvetljava API; ne označite native makro ključ vnosa |
| SteelSeries | GameSense stanje / svetlobni adapter plus splošni HID vnos | GameSense dogodek predložitev ni splošno lastniški ključ ujemanja API |
| Wooting | Analog SDK adapter za podprte analogne vnose, plus normalne ključe HID | Sposobnosti in pogoji distribucije SDK morajo biti preverjeni na podlagi podprtega modela |
| Keychron/QMK/VIA | Standardni ključi HID in navodila za mapiranje firmware; znani postavki le z dokazom | VIA konfiguracijska podpora ni univerzalni Macro-key API |
| Druge standardne tipkovnice | Fizično odkrivanje in mapiranje na napravo; ekskluzivni način samo z validirano filter | Brez izdelanega modela specifičnega Fn, dial ali lastniškega ključa |

Vsako napravo zazna, opazuje, mapira prek programske opreme ponudnika, domače SDK, izključno z vozniki, nedostopno ali nepreizkušeno stanje, kot je ustrezno. Ohranite dokaze o zmogljivosti z različicami adapterja in preizkušenimi identifikatorji modela/firmware.

## Dostava naročila
1. Konfiguracija združljivosti, pogodbe o zmogljivosti in preverjena izvedba.
2. Navodila za namestitev z uporabo resničnih odkritj in obstoječih izvajalcev.
3. UI avtomatizacija in njena posnetka/testna fiksura.
4. Plugin gostitelj, tok paketov in primer integracije.
5. Plačan LAN panel in mobilna preverjanja.
6. Razširjeni adapterji in ekskluzivna integracija tipkovnice, z dejansko validacijo strojne opreme.

Vse šest je potrebno. Podpis/licenciranje voznikov, nedostopni SDK ali odsotna testiranja so odprta vrata, ne razlogov za nadomestilo vedenja in poklicati delo popolno.

## Preverjanje in kriterije sproščanja
Izvedite obstoječe jedro, Windows in WPF preverite in dodajte osredotočen test za vsako novo mejo. Preverite JSON krožne izlete in dediščine; preizkusite uvoženo izvedljivo vsebino, omrežno preverjanje, preklicanje, napake plugin in ponovno povezovanje naprav. Preverite namizne in mobilne postavitve. Veljavno visoko Windows razsežno. Ustvarite urejene elemente šele po opravljenem preverjanju; razlikujte izvedene/testirane, izvedene/hardware-neizkušenih in zunaj blokiranih funkcionalnosti v urejanjih. Ne nadomestite nameščene aplikacije, namestite gonilnike, spremenite resnične mape na tipkovnici ali odprte omrežje kot naključnega testa.

## Dokazi
- Microsoft Raw Input semantike: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G razvijalec SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Razpoložljivost in pogoji za odvisnost: https://github.com/oblitum/Interception
- Obstoječi zalogi proizvajalca: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Pregled odločitve
Uporabnik se zahteva, da pred izvedbenim načrtom odobri ta pisni načrt. Glavna zunanja odvisnost je proizvodno pripravljen ekskluzivni vnosni pogon. Priporočeni obseg to ohranja kot vrata, namesto da ga tiho zmanjša na Raw Input opazovanja.

<!-- generated-doc locale=sl-SI source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Projekt

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stanišče: 8. Oktobra 2026. Status: preizkušanje pred izvajanjem.

## Cilj in dogovorjeni obseg

Neodvisna aplikacija Windows naredi običajno tipkovnico za računalnik na prost konfiguriran akcijski panel, ki je podoben toku. Uporabnik je potrdil vse predlagane vrste ukrepov, makro, prepoznavanje naprave, profili in dober GUI. Priključene tipke Corsair in druge tipke so prepoznane; dodatne funkcije proizvajalca so na voljo le, če so dejansko podprte.

Domneva: nemška površina, lokalna uporaba brez računa, Windows 11 x64 kot prvi cilj, brez namestitve gonilnika. Tastaturo je mogoče uporabiti za normalno pisanje. Globalni zavihki standardno uporabljajo kombinacije tipkov z modifikatorji. Posamezni tipki se ne rezervirajo v standardnem svetu.

## Tehnična odločitev

Native C#/ NET-10-App z WPF in MVVM. Stabilni .NET-10-SDK je na voljo v delovnem prostoru. WPF omogoča individualno oblikovan desktop in neposredno integracijo Windows. Možnosti bi bila spletna vmesnica z ločeno storitev Windows ali aplikacija za namizje, ki je prek platforme; obe prinašajo dodatno časovno in komunikacijsko kompleksnost za ta projekt Windows. Native varianta je zato izvoljena podlaga.

Razdelitev: Desktop projekt za poglede in traje, Core projekt za konfiguracijo in logiko izvajanja, Windows projekt za hotkeys, simulacije in naprave, testni projekt za neodvisna pravila. Proizvajalec adapterji se povežejo na trdnem vmesniku zmogljivosti in so neodvisni od standardnih ukrepov.

## Površina

Temni dizajn z mirnimi antrazitnimi površinami, dobro branim besedilom in turkičnim akcentom. Povezave za navigacijo za ploščo, profile, naprave in nastavitve. Na vrhu aktivnega profila, iskanja in preklop za odmor vseh hotkeys. Površina ostane uporabna pri 100–200 odstotku Windows.

Dashboard vsebuje prilagojeno škatlo. Vsak škatla prikazuje simbol, ime, hotkey in status izvedbe. Klik bo opravil isto dejanje kot Hotkey. Na desni se odpre urednik z vrsto ukrepa, parametri, snemanje ključa, simbol/barva, aktiviranje in testno gumb. Nova, dupliciranje, brisanje in preoblikovanje so na voljo. Prikaz tipkovnice prikazuje shematično za nemško standardno postavitev; ne trdi natančne geometrije zaznane naprave.

Profili se lahko oblikujejo, kopirajo, spremenijo in ročno aktivirajo. Izbirno je dodeljen profil. Ustrezni osnovni program aktivira svoj profil, sicer velja globalni standardni profil. Za več ustreznih profilov velja jasno vidna zaporedja. Upravljavec razloži Hotkey konflikte neposredno na lokaciji.

## Hotkeys in vnosi

Windows RegisterHotKey obdeluje običajne globalne kombinacije s pritiskom na tipko. Uporablja se samo aktivni profil. Rezervirane ali že zabeležene kombinacije se pojavijo kot napake; aplikacija jih ne poroča kot uspešno aktivirane. Vrednotenje v uredniku ne odpravlja dejanj.

Pred simuliranimi vnosi se sproščajo tipke za spremembe ali se pričakuje njihovo fizično sproščanje. Unicode besedilo in kombinacije tipkovnic se pošiljajo s SendInput. Vsak samodejno pritisnjen tipko se odpravlja tudi ob prekinitvi. Akcije potekajo zunaj omrežja; izvedba in sprememba profila ne smejo blokirati površine.

## Akcije in makro

Vrste ukrepov: Začetek programa z argumenti in delovnim seznamom, odprtje datoteke/plate preko Windows, odprtje spletne strani HTTP/HTTPS, vnos besedila Unicode, pošiljanje kombinacije tipk, medija Play/Pause/Naslednji/Predhodni/Stop, višji/nižji/stum in spreminjanje profila. Parametri se preverijo pred shranjevanjem in znova pred izvedbo. Programi se začnejo neposredno; argumenti niso implicitno razlagani kot shell zapovedi.

Makro vsebuje redni seznam navedenih dejanj in jasne premike. Koraki se lahko dodajo, urejajo in odložijo. Ponavljanja so končno konfigurirane (1–100); brezkončne vrstice niso prve različice. Tečajno makro se lahko konča preko površine ali lastnega razpona. Ponavljajoče se sproščanje istega tekočega položaja ne začne vzporedno. Različne makro z simuliranjem vnosa so serijalizirane, tako da se njihove tipke ne mešajo. V primeru napake se makro ustavi in prikaže zadevni korak.

## Naprave in proizvajalci

Oprema na Windows Raw Input in dodatne informacije o napravi: ime, pot naprave, prepoznavanje proizvajalca/proizvoda, če je na voljo, stanje povezave in prepoznane sposobnosti. Prijava/odstranitev posodablja seznam brez ponovnega zagona. Fizična naprava lahko ima več HID vnosov, ki so združeni po obremenljivih identifikatorjih, sicer je vidna kot ločena vnosa.

Normalni hotkeji se uporabljajo med tipkovnicami. Prikaz naprave zagotavlja informacije in spretnosti; ločena tipkovnica z izključno zaprtimi tipkami ni del tega dogovorjenega načina.

Corsair prepoznavanje deluje tudi brez iCUE. Opcionalni adapter iCUE zagotavlja stanje povezave in podprte razsvetljave, če so na voljo SDK, iCUE in združljiva naprava. UI razlikuje prepoznano, SDK na voljo, povezan in dejansko upravljal. SDK v glavnem dokumentira LED nadzor; lastne G tipke so podprte samo kot običajni hotkeys, če Windows prejme ustrezne vnose. Ni pa nobenih obveznosti za vse.

Drugi proizvajalci najprej prejmejo splošno prepoznavanje in standardne ukrepe. Vmesnik adapterja omogoča poznejše integracije; nepodprte veščine so pošteno prikazane. Testiranje strojne opreme za neprimerne modele ni navedeno kot obstoječe.

## Shranjevanje in vsakdanje življenje

Preoblikovana konfiguracija JSON pod lokalnim seznamom podatkov aplikacij. Atomsko shranjevanje z zadnjo veljavno varnostno varnostjo; napake datoteke ne prepišejo. Uvoz preveri obliko, različico, ID in parametre in prikaže predhodni pregled pred prevzemom. Uvožene ukrepe se lahko izvajajo šele po izrecni aktivaciji. Izvoz vsebuje profile in obloge.

Tray menija: odprite, izberite profil, prekinite hotkeys, prekiniti makro, končati. Okna zaprejo na standardni ravni in to pojasnjujejo prvič. Začetek avtomobila je opcijski in standardni. Samo ena institucija upravlja Hotkeys. Konec registracije Hotkeys, razbije izjave in sproži vire.

Lokalni pogled na dejavnost z omejenim potekom za uspeh, napake in prekinitev. Ni zapisov vseh tipkovnic. Vsebina besedilnih dejanj ni vpisana v sporočila o napaki. Ni nobenega telemetrija in nobenega oblaka.

## Napake in meje

Pomanjkanje programov, neuporabne URL-je, odkriti hotkeys, poškodovani profili in nedosegljiv proizvajalec SDK prejmejo konkretne nemške sporočila. Adapter napake ne vplivajo na standardne ukrepe. Vnosi v višjih privilegiranih aplikacijah lahko ne dosežejo dovoljenj Windows; napake so razložene, aplikacija ne zahteva upravljavskih pravic.

## Preizkus in zmanjšanje

Avtomatizirani testi za profilno prednost, prepoznavanje konfliktov, uvoz konfiguracije, atomsko shranjevanje, makro zaporedje, ponovitev, prekinitev in oglaševanje napak. Windows Smooke testi za resnične hotkeys, besedilo in tipkovnice kombinacije, medija akcije, naprave inventor, traja in čista konča. Nobenih testov, ki naključno sprožijo kakršne koli programe ali spletne strani.

Preizkušanje GUI: postavitev in urejanje, snemanje brez izvedbe, klik/hotkey z enakim učinkom, sporočilo o konfliktu, spreminjanje profila, pogled naprave, uvoz/eksport in skaliranje. Proizvajalec-specifična preiskava strojne opreme je posamično dokumentirana z dejansko obstoječo napravo.

Dostava: virni kodo, predvajalnik za gradnjo/start, nemški vodnik za uporabo in lokalni Windows-x64-Publish-Artefakt. Uspeh pomeni izvedljivo aplikacijo z vsemi splošnimi funkcijami; znanje Corsair je označeno kot na voljo le z dokazano podporo za SDK / napravo.

## Tehnični viri

- Microsoft Ključni vnos / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Vnos: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Uradni Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Naslednji korak

Po pregledu tega načrta: pripravo konkretnega načrta izvajanja in predložitev izbire izvajanja. V praznem delovnem redu še ni Git-Repository; zato ta dokument še ni bil sprejet.

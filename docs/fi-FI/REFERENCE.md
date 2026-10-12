<!-- generated-doc locale=fi-FI source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../README.md)

# MAKRIVO

[Täydellinen dokumentaatio kaikilla kielillä 27](README.md) · [Käyttäjän käsikirja](HANDBOOK.md)

**ja sen Aivana.** [Käytännön sisältö ja vapautusrajoitukset](FEATURES.md) · [Testi laajennus](testing/2026-10-12-six-extensions.md) · [Paikallinen prototyyppi](testing/2026-10-12-local-meeting-assistant.md)

**Yhdysvallat (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Paikallinen Windows hotkey -ohjain, jossa on Stream Deck-tyyli, profiilit, makro ja näppäimistö. Sisältö tukee 27 kieliä. Tilin tai pilviyhteys ei ole tarpeen jokapäiväiseen käyttöön.

[Dokumentti](README.md) · [Tarkistaminen](VERIFICATION.md) · [Testi raportti](testing/2026-10-09-usp-release.md)

## Aloittaminen

Julkaisun jälkeen hakemus on `artifacts/win-x64/MAKRIVO.exe`. Pidä koko lähtökansi yhdessä; voit kopioida sen muualle. Windows 11 x64 on alkuperäinen tavoite. Rakennettuja esineitä suljetaan pois Gitistä; lähteen tarkastus ei sisällä ennalta rakennettua täytäntöönpanoa.

Asenna .NET 10 SDK, joka on määritelty `global.json` rakentaa ja käynnistää lähde:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Julkaise omaan sisältöön perustuva rakenne ilman erillistä .NET-käyttöaikaa:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Perusteellinen työvoima

1. Valitse profiili ja luo toimintasuunnitelma.
2. Aseta nimi, kuvio, väri ja näppäimistö lyhyt. Ctrl/Alt/Shift -kombinatiot vähentävät sattumanvaraista aktivointia kirjoittamisen aikana.
3. Lisää yksi tai useampi askel: sovellukset, tiedostot, verkkosivut, teksti, lyhennökset, media, koko, profiilin vaihdon tai viivästykset.
4. Pelastaa Muutokset jäävät suunnitelmaan, kunnes tallennettu versio palautetaan. Painopiste ja sen hotkey käyttävät samaa suoratoistoa.
5. Järjestä uudelleen ja makro askeleet drag-and-drop tai laukauspainikkeilla. Mallit yksinkertaistavat asennusta. Macros voi toistaa jopa 100 kertaa. Poista käyttöliittymän, järjestelmän jäljellä olevan valikon tai Ctrl+Alt+Escape oletusarvoisesti.

Sovelluskohtaiset profiilit käyttävät prosessin nimeä ilman sen täydellistä reittiä. Profiilin tilaus rikkoo linkkejä; oletusarvoinen profiili on palautus. Automaattisen profiilin valinnan on oltava käytettävissä asetuksissa.

Pääikkunan sulkeminen pitää sovelluksen järjestelmässä oletusarvoisesti. Kaksinkertaisesti napsauta tray-ikonia uudelleen; käytä tray-valikkoa ulos. Startup with Windows on valinnainen ja käytöstä poistettu. Hotkeys voidaan keskitettyä.

## Toiminta ja komento paletti

Lisää toiminta avaa etsimättömän luettelon, jossa on 90 ennakkoluuloja kahdeksassa luokassa. Jokaisella on kuvaus ja ennuste. Makroeditor käyttää samaa luetteloa lisäämään tai korvaamaan vaiheet. Valitsemalla ennakkoasetuksen tässä muokkausvirtauksessa luodaan suunnitelma ilman täytäntöönpanoa.

Ohje paletti avautuu otsikosta tai sovelluksen sisällä olevalla Ctrl+K:llä. Se etsii tallennettuja toimintoja profiilien, luetteloiden ennakkoasetusten, profiilin vaihdon, navigoinnin, tauon/resumin, makro-takoon ja tuonnin/viennin kautta. Suodatin tallennetulla toiminnalla, luettelon toiminnoilla tai sovelluksen komentoilla. Valitse avaimet, Enter aktivoi, Ctrl+Enter muokkaa muokkaavaa sisäänkäyntiä ja Escape sulkee. Suosikkia ilmestyy ensin. Käyttövalmistettujen luetteloiden käskyjä voidaan käynnistää välittömästi; asetukset, jotka edellyttävät suunnitelman avaamista. Asetukset voivat määritellä globaalin lyhytkestoisen. Uudet asetukset käyttävät Ctrl+Shift+Spacea; olemassa olevat asetukset eivät automaattisesti saa uutta globaalia lyhennettä.

Audio-toiminnot määrittävät tilavuuden tarkasti 0 ja 100 prosenttia ja mute, unmute tai toggle tuotto ja mikrofonilaitteet. Valitse tietty loppupiste tai jätä se asetetuksi Windows. Käytettävissä oleva tallennettu loppupiste tuottaa virheen sen sijaan, että hallitsisit toista laitetta. Lataa uudelleen liitetty laite.

OBS Studio käyttää WebSocket v5: start/stop/toggle/pause/resume tallennus; start/stop /toggle streaming; muuttaa kohtauksia; toggle input mute. Sallita WebSocket -palvelin työkaluissa OBS ja aseta sen loppupiste muokkauksessa. Tarkista yhteys ja latausnimi luet OBS versiota, kohtauksia ja tuloksia aloittamatta tallennusta tai virtaa. Valitse nimet luettelosta tai kirjoita ne manuaalisesti. salasanoja voidaan tallentaa Windows ja ne on suljettu pois vientiasetuksista. Ei ole käytettävissä OBS yhteyksiä ilmoittaa virheen.

Profiilit voivat vastata prosessin nimeä ja osan ikkunan otsikosta. Molempien ehtojen on oltava yhteensopivia. Lock Profile estää istunnon automaattisen vaihtamisen. Capture Conditions Foreground käyttää viimeistä ulkoista sovellusta.

Teksti ja clipboard-toiminta `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, ja `{{user}}`. Päivän käyttö `yyyy-MM-dd`; Aikaa käyttää `HH:mm`. Tuntematon paikkakunta pysyy muuttumattomana. `{{profile}}` Tunnistaa käynnistetyn toiminnan profiili; `{{process}}` tunnistaa ulkoisen sovelluksen kontekstin.Clipboard-sisältö luetaan vain, kun kyseistä paikkaa käytetään.Klipboard-toimi kirjoittaa tekstin, mutta ei automaattisesti kiinnitä.

ikkunan toimenpiteet minimoida, maksimoida, palauttaa tai järjestää ikkunan vasemmalle / oikealle näytön työalueelle. Järjestelmän toiminta lukitsee Windows. Näitä toimenpiteitä toteutetaan vain, kun niitä on nimenomaisesti pyydetty.

Etusivu näyttää vaiheet, toistot ja muuttujat ilman suoritusta. Undo ja Redo säilyttävät jopa 100 muotoilua; tekstilaatikot säilyttävät alkuperäisen Ctrl+Z-muokkauksen. Merkitse suosikkeja muokkaajassa tai tile-valikossa ja suodattaa ne dashboardissa. Täytäntöönpanoa sovelletaan vain nykyiseen istuntoon.

Tekstin, lyhennöksen ja ikkunan toimenpiteet, jotka käynnistetään käyttöliittymästä, aktivoivat viimeisen ulkoisen kohdeikkunan. Paletti näyttää tämän kohderyhmän. Jos kohde on suljettu tai Windows kieltäytyy aktivoinnista, suoritus pysähtyy viestin kanssa.

## Keyboard tuki ja palautetta

Special-Key Wizard on sivulla ja paletissa. Se on luettelo G-tärkeistä ilmoitettu iCUE. Avainlehti pidättää heidät väliaikaisesti ilman, että he suorittavat tehtäviään. Normaalit sidokset palaavat havaitsemisen, peruuttamisen tai sulkemisen jälkeen. Katalogin toiminto ja valittu avain avataan ilman suunnitelmaa; kaksinkertaiset sidokset tarkistetaan.

Keyboard Overlay näyttää valitun viitekehyksen ja aktiiviset sidokset. Pidä Ctrl+Alt+F1 tai poista se paletista tai näppäimistöstä. Se ei ota keskittymistä ja salli hiiren klikkaukset läpi. Profiili ja sitovat muutokset päivitetään näkyvissä; tavalliset Windows avainpainikkeet korostetaan. Valmistajan yksilölliset ohjausjärjestelmät eivät saa toimittaa vastaavaa Windows avainsignaalia.

Toteutusviittaukset osoittavat odottamista, vaiheen / toistamisen edistymistä, täydentämistä, peruuttamista tai epäonnistumista ottamatta huomiota tai esittämättä kirjoitettua tekstiä. Commands Executed vahvistaa komennon täytäntöönpanoa, ei itsenäisesti pyydettyä elävää tilaa kolmannen osapuolen sovelluksessa. Virheen tiedot näkyvät toiminnan arkistossa.

## Laitteet ja Corsair

Corsair G1 – G6 voidaan määrätä suoraan virallisen iCUE SDK: n kautta. Valitse laite ja avain toimintoeditoriin ja tallenna tai valmistele esite K100 -näytöstä. Liitteet koskevat valittua laitetta ja aktiivista profiilia; pausa estää ne myös. iCUE on oltava käynnissä ja sallitaan pääsy. Fn, iCUE pyörä, profiili ja lukko ohjaus ei voi karttaa tämän G-tärkeä API.

Laitteiden sivu luettelee Windows näppäimistön käyttöliittymät ja päivittää säännöllisesti ja kun laitteet ovat yhteydessä. Fyysinen näppäimistö voi altistaa useita HID-liitäntöjä. Standardi globaali hotkeys toimii valmistajien kesken.

Corsair havainto käyttää käytettävissä olevia laitteita. RGB-ohjaus edellyttää yhteensopivaa laitteistoa, joka toimii iCUE ja virallinen iCUE SDK; sovellus raportoi todellisesta saatavuudesta. Muut valmistajat saavat tällä hetkellä havaintoja ja standardoituja toimia. Ulkopuolella Corsair G-tärkeä integrointi, omistusohjaimet toimivat, kun Windows vastaanottaa ne säännöllisenä sisäänkäynnillä. Sovellus ei asenna näppäimistöä.

## kieliä

Asetukset tarjoavat 27 kieliä: Saksa, Englanti, Ranskan, Espanjan, Italian, Portugalin, Alankomaiden, Tšekin, Slovakian, Unkarin, Bulgarian, Kreikan, Tanskan, Ruotsin, Norjan, Suomen, Viron, Latvian, Liettuan, Slovenian, Kroatian, Ukrainan, Venäjän, Turkin ja Serbian Latinalaisen kirjoituksen. Se seuraa automaattisesti Windows näytön kieltä. Käännökset ovat paikallisia. Asennukset, hotkeys ja käyttäjän luomat nimet ovat riippumattomia käyttöliittymän kielestä.

Täydellinen tallennusasiakirja on saatavilla kaikilla 27 hakemuksellisilla kielillä [Kieliindeksi](README.md)ja Käännökset ovat koneellisesti avustettuja, alkuperäisten lähteiden linkkien ja rakenteellisten tarkastusten kanssa; alkuperäisen puhujan tarkastus on erillinen. Sovellus hyväksyy myös alueelliset koodit `en-US`, `de-DE`, `fr-FR` ja `it-IT` ja käyttää niiden numeromuotoja. Nykyiset kielikoodit ovat yhteensopivia.

## Layoutit ja valmistajan tutkimus

Näytölaatikossa on täysikokoiset ja ajattelemattomat ISO-DE/ANSI-US-referenssit sekä Corsair K100 RGB-järjestelmät. Geometria edustaa paikkoja, laaja/tall avaimet ja ISO Enter. Standardijärjestelmät ovat yleisiä viittauksia. K100 järjestelmät seuraavat dokumentoituja ohjauksia ilman millimetrin tarkkoja ulottuvuuksia. Valitse oikea kieli/geometrinen variantti: mallin nimi tai USB VID/PID ei määritä erityistä varianttia.

Kartta ratkaisee fyysisen skannauksen koodit käyttämällä aktiivista Windows tulosjärjestystä. Työnkulku laatii suunnitelman ja säilyttää säästöt / konfliktin tarkastukset. Standardit ovat globaaleja Windows hotkeys, jotka eivät ole eristetty yhdelle sidoksille. Corsair G1–G6 käytetään laitteen erityistä iCUE integrointia. Muita omistusoikeuksia vaaditaan adapteri tai valmistajan määrittelemät lyhennökset. Main Enter ja numerinen avain Enter jakavat virtuaalisen avaimen identiteetin globaaleille hotkeysille.

ja sen [Valmistajan tutkimus](research/tastaturhersteller-sonderfunktionen.md) ja [CSV](research/tastaturhersteller-sonderfunktionen.csv) kattaa 74 brändi / valmistaja ryhmät, erottaa konfiguraattorit, sisäänpääsy SDK: t ja valaistus APIs. [Layout todisteita](research/keyboard-layout-evidence.md) Rekisterimalli, SKU ja moduulien ero. Tutkimus ei tarkoita, että laitteistoa tuetaan.

## Tiedot

Konfiguraatio tallennetaan paikallisiin sovellustietoihin. Tuonti ja vienti ovat JSON. Tuonti ennustaa konfigurointia, korvaa nykyisen konfiguroinnin vahvistamisen jälkeen ja poistaa tuontitunnukset manuaalisesti. Tekstitoiminnon sisältö ei kerätä avainsanoina. Luottamusjohtajien salasanoja erotetaan vientiprofiilista.

## Tarkistaminen

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows tarkastukset voivat tarkastaa laitteistoa ja tilapäisesti asettaa/lataa tuettuja iCUE sidoksia. Käynnistä niitä ilman muuta ohjaajaa käyttämällä näitä sidoksia. Äänivaihto ei muutu live-volyymiä tai mute asetuksia.

Kotimaiset tulot eivät voi ylittää Windows etuoikeuksia. Tavallisesti käynnistetty ohjain ei voi lähettää sisäänkäyntiä hallintovelvollisuuteen. Varattu tai miehitetyt hotkeys näkyvät konflikteina.

ja sen [Dokumenttiindeksi](README.md) linkit historiallisia suunnitteita, suunnitelmia, tutkimusta ja päivitetyt testitiedot. Historialliset suunnitelmat kuvaavat niiden rekisteröityä laajuutta, ei nykyistä taitoa ominaisuuksien tuesta.

### Työnkuljetusmalleja ja kohdekenttä odottaa

Kataloniassa on kuusi muokattavaa työvirtaa: työpaikka, kokous, virtaus, tutkimus, projektin vaihteisto ja teksti. `workflow.` valitsee työnkulku avaa epäsäännöllisen suunnitelman; se ei toimi välittömästi. Kaikki vaiheet säilytetään, kun lisätään työnkulku olemassa olevaan makroon tai määrätään sen Corsair Esimerkkinä URL-osoitteet, ohjelman reitit, profiilit ja OBS Tapahtumat ennen säästöä.

ja sen **Tavoitteen ikkuna** Vaihe hyväksyy prosessin nimen (mukaan tai ilman `.exe`), kohtuuttoman otsikko- ja aikarajoitus 1–600,000 ms. Vaaditaan vähintään yksi suodatin; molempien suodattimien on oltava samankaltaisia toimitettaessa. makro jatkuu vain vastaavan näkyvän ikkunan vastaamisen jälkeen ja saavuttaa etupisteen. Käytä tiettyä otsikkoa, kun useat ikkunat jakavat prosessin. Peruutus tai aikarajoitus pysähtyy seuraavien vaiheiden kanssa. ikkunan valmius ei takaa, että asiakirja on ladattava tai ulkoinen palvelu, kuten OBS WebSocket on yhteydessä.

Näytön kirkkaus: etsi **Increase näytön kirkkaus**, **Vähentää näytön kirkkautta** tai **Snäytön kirkkaus 50 prosenttia** toimenkatalogissa. Suhteelliset toimet muuttavat kirkkautta 10 prosenttiyksikköä; kiinteät arvot vaihtelevat 0 ja 100. Tuetut sisäiset näytöt käyttävät WMI:tä; yhteensopivat ulkoiset näytöt edellyttävät DDC/CI valikoimaan. Oletusarvoisesti toimenpide vaikuttaa kaikkiin tuettuihin näytöihin; nimenomainen näyttö voidaan valita muokkaajassa. Yhteydet ja tuki voivat rajoittaa tukea.

### Laajennettu kontrolli

Avaa komento paletti **Ctrl+K, kun sovellusikkuna on keskittynyt**. etsimään **Workflow suunnittelija**, **Rekisteröinti makro**, **Synkronoi profiilit**, tai **Tärkein diagnoosi** (työvirran suunnittelija muokkaa valittua levyä: tiedoston/prosessin/ikkunan ehtoja, etukäteen lasketaan ja pysähtyy/pysy virheiden käsittelyä. Rekisterin tallentaa lyhytkestoisia koodeja ja odottaa, ei tavallista kirjoittamista; Escape lopettaa tallennuksen..

Valonvaiheessa valitse kaikki tuetut näytöt tai erityinen näyttö. Saatavilla oleva valittu näyttö tuottaa virheen sijaan, että muutetaan hiljaa toista näyttöä. Ohjelmisto näyttää elävää kirkkautta, ääntä ja OBS lukemista, kun niiden integroinnit ovat saatavilla; käytettävissä oleva lukeminen ei ole mitattu arvo.

Tile Editorissa määritä tukeva fyysinen erityinen avain ja valitse välitön painaminen, painaminen, pidä tai kaksinkertainen painaminen. Held Layer -valintaohjelmat aktivoivat väliaikaisesti toisen karttauksen ja palauttavat edellisen kerroksen vapauttamisessa. Käytännöt ja kerrokset koskevat Corsair SDK G-tärkeitä ja laitteen erityisiä standardeja, ei tavallisia globaaleja hotkeyejä. Standard-key capture tukee F13–F24 ja näppäimistö avaimet. Omistettu Fn, profiili ja dial-ohjaimet vaativat edelleen yhteensopivaa toimittajaa tai toimittajaa, joka kartoittaa vakiokohdan. Raw Input tarkkailee avainta eikä estä sen normaalia käyttäytymistä Windows. Avain diagnoosi selittää profiilin, tauon, yhteyden ja sitova ongelma.

Profiilin synkronointi vaihtaa tiedostoja valitsemallasi kansiossa, mukaan lukien olemassa olevan pilvipalvelun hallinnoima kansi. Tarkista etukäteen ennen tuontia; tuonti alkaa poistaa ja poistaa laitteisto-erityisiä karttoja. Tarkistukset estävät hiljaisia ylikuormituksia, ja toimenpiteiden sisältö on nimenomainen valinta. MAKRIVO ei tarjoa pilvipalvelua tai säilyttää pilvipohjaisia tunnistuksia. Peli käyttää nyt MAKRIVO omaa tealin M -kuvaa ja näyttää amber-pauksen, kun se on tauolla.

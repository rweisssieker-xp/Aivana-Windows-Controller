<!-- generated-doc locale=fi-FI source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../README.md)

# MAKRIVO

[Koko dokumentaatio kaikilla 27 kielellä](README.md) · [Koko käyttöopas](HANDBOOK.md)

**Valmistaja Aivana.** [Nykyinen ominaisuusluettelo ja julkaisurajoitukset](FEATURES.md) · [Laajennusten testiraportti](testing/2026-10-12-six-extensions.md) · [Paikallisen AI-avustajan prototyyppi](testing/2026-10-12-local-meeting-assistant.md)

**Englanti (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Paikallinen Windows-pikanäppäinohjain, jossa on Stream Deck -tyyliset toimintoruudut, profiilit, makrot ja näppäimistöntunnistus. Käyttöliittymä tukee 27 kieltä. Päivittäiseen käyttöön ei tarvita tiliä eikä pilviyhteyttä.

[Dokumentaatio](README.md) · [Tarkistaminen](VERIFICATION.md) · [Ominaisuuksien testiraportti](testing/2026-10-09-usp-release.md)

## Aloitus

Julkaisukoosteen jälkeen sovellus on polussa `artifacts/win-x64/MAKRIVO.exe`. Pidä koko tuloskansio yhdessä; voit kopioida sen muualle. Windows 11 x64 on alkuperäinen kohdeympäristö. Koontiartefaktit on jätetty pois Git-versionhallinnasta; lähdekoodin paikallinen kopio ei sisällä valmista suoritettavaa ohjelmaa.

Asenna tiedostossa `global.json` määritetty .NET 10 SDK lähdekoodista koontia ja suorittamista varten:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Julkaise itsenäinen koonti, joka ei vaadi erikseen asennettua .NET-suoritusympäristöä:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Perustyönkulku

1. Valitse profiili ja luo toimintoruutu.
2. Aseta nimi, kuvake, väri ja pikanäppäin. Ctrl/Alt/Shift-yhdistelmät vähentävät tahattomia aktivointeja kirjoittamisen aikana.
3. Lisää yksi tai useampi vaihe: sovellukset, tiedostot, verkkosivustot, teksti, pikanäppäimet, media, äänenvoimakkuus, profiilinvaihto tai viiveet.
4. Tallenna. Muutokset ovat luonnos tallennukseen asti; hylkääminen palauttaa tallennetun version. Toimintoruudun toistopainike ja sen pikanäppäin käyttävät samaa suorituspolkua.
5. Järjestä toimintoruudut ja makrovaiheet vetämällä ja pudottamalla tai nuolipainikkeilla. Mallipohjat helpottavat määritystä. Makrot voivat toistua enintään 100 kertaa. Peruuta käyttöliittymästä, ilmoitusalueen valikosta tai oletuspikanäppäimellä Ctrl+Alt+Escape.

Sovelluskohtaiset profiilit käyttävät prosessin nimeä ilman koko polkua. Profiilien järjestys ratkaisee tasatilanteet; oletusprofiilia käytetään varavaihtoehtona. Automaattinen profiilinvalinta on otettava käyttöön asetuksissa.

Pääikkunan sulkeminen jättää sovelluksen oletusarvoisesti ilmoitusalueelle. Avaa se uudelleen kaksoisnapsauttamalla sen kuvaketta; lopeta ilmoitusalueen valikosta. Käynnistys Windows-järjestelmän mukana on valinnainen ja oletusarvoisesti pois käytöstä. Pikanäppäimet voidaan keskeyttää keskitetysti.

## Toiminnot ja komentopaletti

Toiminnon lisääminen avaa haettavan luettelon, jossa on 90 esiasetusta kahdeksassa luokassa. Jokaisella on kuvaus ja esikatselu. Makroeditori käyttää samaa luetteloa vaiheiden lisäämiseen tai korvaamiseen. Esiasetuksen valinta tässä muokkaustyönkulussa luo luonnoksen suorittamatta sitä.

Komentopaletti avautuu otsakealueelta tai sovelluksen sisällä näppäimillä Ctrl+K. Se hakee tallennettuja toimintoja eri profiileista, luettelon esiasetuksia, profiilinvaihtoa, navigointia, tauotusta ja jatkamista, makron peruutusta sekä tuontia ja vientiä. Suodata tallennettujen toimintojen, luettelotoimintojen tai sovelluskomentojen mukaan. Nuolinäppäimet valitsevat, Enter aktivoi, Ctrl+Enter muokkaa muokattavaa kohdetta ja Escape sulkee. Suosikit näkyvät ensin. Käyttövalmiit luettelokomennot voivat suorittua heti; määritystä tarvitsevat esiasetukset avaavat luonnoksen. Asetuksissa voi määrittää globaalin palettipikanäppäimen. Uusissa kokoonpanoissa se on Ctrl+Shift+Space; olemassa oleviin kokoonpanoihin ei lisätä uutta globaalia pikanäppäintä automaattisesti.

Äänitoiminnot asettavat äänenvoimakkuuden tarkasti välille 0 ja 100 prosenttia sekä mykistävät äänilähtö- ja mikrofonilaitteet, poistavat mykistyksen tai vaihtavat sen tilaa. Valitse tietty päätepiste tai jätä valinta tyhjäksi käyttääksesi Windows-oletusta. Puuttuva tallennettu päätepiste aiheuttaa virheen sen sijaan, että ohjattaisiin toista laitetta. Laitteiden päivitys lataa kytketyt laitteet uudelleen.

OBS Studio käyttää WebSocket v5 -protokollaa: tallennuksen käynnistys, pysäytys, tilan vaihto, tauotus ja jatkaminen; suoratoiston käynnistys, pysäytys ja tilan vaihto; kohtausten vaihto; tulolähteen mykistystilan vaihto. Ota WebSocket-palvelin käyttöön OBS:n työkaluissa ja määritä päätepiste editorissa. Yhteyden tarkistus ja nimien lataus lukevat OBS-version, kohtaukset ja tulolähteet käynnistämättä tallennusta tai suoratoistoa. Valitse nimet luettelosta tai syötä ne käsin. Salasanat voidaan tallentaa Windows Credential Manager -palveluun, ja ne jätetään pois kokoonpanovienneistä. OBS-yhteyden puuttuessa ilmoitetaan virhe.

Profiilin täsmäytyksessä voi käyttää prosessin nimeä ja ikkunan otsikon osaa. Molempien annettujen ehtojen on täsmättävä. Profiilin lukitus estää automaattisen vaihdon istunnon aikana. Ehtojen poiminta etualalta käyttää viimeistä aktiivista ulkoista sovellusta.

Teksti- ja leikepöytätoiminnot tukevat muuttujia `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ja `{{user}}`. Päivämäärät käyttävät muotoa `yyyy-MM-dd`; kellonajat muotoa `HH:mm`. Tuntemattomat paikkamerkit säilyvät muuttumattomina. `{{profile}}` tunnistaa käynnistetyn toiminnon profiilin; `{{process}}` tunnistaa ulkoisen sovelluksen kontekstin. Leikepöydän sisältö luetaan vain, kun kyseistä paikkamerkkiä käytetään. Leikepöytätoiminto kirjoittaa tekstiä leikepöydälle mutta ei liitä sitä automaattisesti.

Ikkunatoiminnot pienentävät, suurentavat, palauttavat tai asettelevat ikkunan näyttönsä työalueen vasemmalle tai oikealle puolelle. Järjestelmätoiminto lukitsee Windows-järjestelmän. Nämä toiminnot suoritetaan vain erikseen kutsuttaessa.

Esikatselu näyttää vaiheet, toistot ja muuttujat suorittamatta niitä. Kumoa ja Tee uudelleen säilyttävät enintään 100 luonnostilaa; tekstikentät säilyttävät alkuperäisen Ctrl+Z-muokkauksen. Merkitse suosikit editorissa tai toimintoruudun valikossa ja suodata niitä koontinäytössä. Suoritusmäärät koskevat vain nykyistä istuntoa.

Käyttöliittymästä käynnistetyt teksti-, pikanäppäin- ja ikkunatoiminnot aktivoivat viimeisen ulkoisen kohdeikkunan. Paletti näyttää kohteen kontekstin. Jos kohde on suljettu tai Windows estää aktivoinnin, suoritus pysähtyy ja näyttää viestin.

## Näppäimistön avustustoiminnot ja palaute

Erikoisnäppäinten ohjattu määritys on näppäimistösivulla ja paletissa. Se luettelee iCUE:n ilmoittamat G-näppäimet. Näppäinpainalluksen tunnistus sieppaa ne tilapäisesti suorittamatta niille määritettyjä toimintoja. Tavalliset sidonnat palaavat tunnistuksen, peruutuksen tai sulkemisen jälkeen. Luettelotoiminto ja valittu näppäin avautuvat tallentamattomana luonnoksena; päällekkäiset sidonnat tarkistetaan.

Näppäimistön peittokuva näyttää valitun viiteasettelun ja aktiiviset sidonnat. Pidä Ctrl+Alt+F1 pohjassa tai vaihda näkyvyyttä paletista tai näppäimistösivulta. Se ei ota kohdistusta ja päästää hiiren napsautukset lävitseen. Profiili- ja sidontamuutokset päivittyvät sen ollessa näkyvissä; tavalliset Windows-näppäinpainallukset korostetaan. Valmistajakohtaiset säätimet eivät välttämättä tuota vastaavaa Windows-näppäinsignaalia.

Suorituspalaute näyttää odotuksen, vaiheiden ja toistojen etenemisen, valmistumisen, peruutuksen tai epäonnistumisen ottamatta kohdistusta tai paljastamatta syötettyä tekstiä. Suoritetut komennot vahvistaa komennon suorituksen, ei erikseen kysyttyä reaaliaikaista tilaa kolmannen osapuolen sovelluksessa. Virhetiedot näkyvät toimintalokissa.

## Laitteet ja Corsair

Corsair G1–G6 voidaan määrittää suoraan virallisen iCUE SDK:n kautta. Valitse laite ja näppäin toimintoeditorissa ja tallenna tai valmistele luonnos K100 näppäimistönäkymästä. Sidonnat koskevat valittua laitetta ja aktiivista profiilia; tauotus poistaa myös ne käytöstä. iCUE:n on oltava käynnissä ja sallittava käyttö. Fn-, iCUE-rulla-, profiili- ja lukitussäätimiä ei voi määrittää tämän G-key API -rajapinnan kautta.

Laitesivu luettelee Windows-näppäimistörajapinnat ja päivittyy säännöllisesti sekä laitteiden kytkeytyessä. Fyysinen näppäimistö voi tarjota useita HID-rajapintoja. Tavalliset globaalit pikanäppäimet toimivat eri valmistajien laitteilla.

Corsair-tunnistus käyttää saatavilla olevia laitetietoja. RGB-ohjaus vaatii yhteensopivan laitteiston, käynnissä olevan iCUE:n ja virallisen iCUE SDK:n; sovellus ilmoittaa todellisen saatavuuden. Muilla valmistajilla on tällä hetkellä tunnistus ja tavalliset toiminnot. Corsair G-key -integraation ulkopuolella valmistajakohtaiset säätimet toimivat, kun Windows vastaanottaa ne tavallisena syötteenä. Sovellus ei asenna näppäimistöajuria.

## Kielet

Asetuksissa on 27 kieltä: saksa, englanti, ranska, espanja, italia, portugali, hollanti, puola, tšekki, slovakki, unkari, romania, bulgaria, kreikka, tanska, ruotsi, norja, suomi, viro, latvia, liettua, sloveeni, kroatia, ukraina, venäjä, turkki ja latinalaisin kirjaimin kirjoitettu serbia. Automaattinen valinta seuraa Windows-näyttökieltä. Käännökset toimitetaan paikallisesti mukana. Asettelut, pikanäppäimet ja käyttäjän luomat nimet ovat käyttöliittymäkielestä riippumattomia.

Tietovaraston koko dokumentaatio on saatavilla kaikilla 27 sovelluskielellä seuraavan hakemiston kautta: [kielihakemisto](README.md). Käännökset ovat koneavusteisia, ja niihin liittyy alkuperäislähteiden linkit ja rakennetarkistukset; äidinkielinen tarkistus on erillinen. Sovellus hyväksyy myös aluekoodit `en-US`, `de-DE`, `fr-FR` ja `it-IT` ja käyttää niiden numeromuotoja. Nykyiset kielikoodit säilyvät yhteensopivina.

## Asettelut ja valmistajatutkimus

Näppäimistönäkymä tarjoaa täysikokoiset ja numeronäppäimistöttömät ISO-DE/ANSI-US-viiteasettelut sekä Corsair K100 RGB -asettelut. Geometria kuvaa sijainnit, leveät ja korkeat näppäimet sekä ISO Enter -näppäimen. Tavalliset asettelut ovat yleisiä viitteitä. K100 asettelut seuraavat dokumentoituja säätimiä väittämättä mittoja millimetrintarkoiksi. Valitse oikea kieli- ja geometriaversio: mallinimi tai USB VID/PID ei osoita tiettyä versiota.

Määritys tulkitsee fyysiset skannauskoodit aktiivisen Windows-syöteasettelun avulla. Työnkulku valmistelee luonnoksen ja säilyttää tallennus- ja ristiriitatarkistukset. Tavalliset sidonnat ovat globaaleja Windows-pikanäppäimiä, eivät rajoitu yhteen kytkettyyn näppäimistöön. Corsair G1–G6 käyttää laitekohtaista iCUE-integraatiota. Muut valmistajakohtaiset säätimet vaativat sovittimen tai valmistajan määrittämiä pikanäppäimiä. Pää-Enter ja numeronäppäimistön Enter jakavat saman virtuaalinäppäintunnisteen globaaleissa pikanäppäimissä.

Tietovaraston [valmistajatutkimus](research/tastaturhersteller-sonderfunktionen.md) ja [CSV](research/tastaturhersteller-sonderfunktionen.csv) kattavat 74 tuotemerkki- ja valmistajaryhmää erottaen määritystyökalut, syöte-SDK:t ja valaistusrajapinnat. [Asettelujen lähdeaineisto](research/keyboard-layout-evidence.md) kirjaa mallien, SKU-tunnusten ja moduulien erot. Tutkimuskattavuus ei tarkoita, että laitteistotuki olisi toteutettu.

## Tiedot

Kokoonpano tallennetaan paikallisiin sovellustietoihin. Tuonti ja vienti käyttävät JSON-muotoa. Tuonti näyttää kokoonpanon esikatselun, korvaa nykyisen kokoonpanon vahvistuksen jälkeen ja poistaa tuodut sidonnat käytöstä, kunnes ne otetaan käsin käyttöön. Tekstitoimintojen sisältöä ei kerätä näppäinpainalluslokiksi. Credential Manager -salasanat ovat erillään viedyistä profiileista.

## Tarkistaminen

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows-tarkistukset voivat tutkia laitteistoa ja tilapäisesti määrittää tai vapauttaa tuettuja iCUE-sidontoja. Suorita ne ilman toista ohjainta, joka käyttää samoja sidontoja. Äänipäätepisteiden tarkistukset eivät muuta reaaliaikaista äänenvoimakkuutta tai mykistysasetuksia.

Natiivi syöte ei voi ohittaa Windows-käyttöoikeuksia. Tavallisesti käynnistetty ohjain ei välttämättä voi lähettää syötettä järjestelmänvalvojan oikeuksilla toimivaan sovellukseen. Varatut tai jo käytössä olevat pikanäppäimet näkyvät ristiriitoina.

Tietovaraston [dokumentaatiohakemisto](README.md) linkittää historialliset suunnitelmat, tutkimuksen ja päivätyt testiraportit. Historialliset suunnitelmat kuvaavat kirjattua laajuuttaan, eivät takaa ominaisuuksien nykyistä tukea.

### Työnkulkumallit ja kohdeikkunan odotus

Luettelo sisältää kuusi muokattavaa työnkulkua: työympäristö, kokous, suoratoisto, tutkimus, projektinvaihto ja tekstikatkelma. Hae merkkijonoa `workflow.` luettelosta tai komentopaletista. Työnkulun valinta avaa tallentamattoman luonnoksen; se ei koskaan suoritu heti. Kaikki vaiheet säilyvät, kun työnkulku lisätään olemassa olevaan makroon tai määritetään Corsair-näppäimelle. Määritä esimerkkien URL-osoitteet, ohjelmapolut, profiilit ja OBS-kohtaukset ennen tallennusta.

**Kohdeikkunan odotus** hyväksyy prosessin nimen (`.exe`-päätteellä tai ilman), kirjainkoosta riippumattoman otsikon osan ja aikakatkaisun 1–600000 ms. Vähintään yksi suodatin tarvitaan; molempien on täsmättävä, jos molemmat annetaan. Makro jatkuu vasta, kun vastaava näkyvä ikkuna vastaa ja on etualalla. Käytä tarkkaa otsikkoa, jos useat ikkunat jakavat saman prosessin. Peruutus tai aikakatkaisu estää seuraavien vaiheiden suorituksen. Ikkunan valmius ei takaa asiakirjan latautumista tai ulkoisen palvelun, kuten OBS WebSocket -palvelun, yhteyttä.

Näytön kirkkaus: hae toimintoluettelosta **Lisää näytön kirkkautta**, **Vähennä näytön kirkkautta** tai **Näytön kirkkaus 50 prosenttia**. Suhteelliset toiminnot muuttavat kirkkautta 10 prosenttiyksikköä; kiinteät arvot ovat välillä 0 ja 100. Tuetut sisäiset näytöt käyttävät WMI-rajapintaa; yhteensopivissa ulkoisissa näytöissä on otettava DDC/CI käyttöön niiden valikosta. Oletusarvoisesti toiminto vaikuttaa kaikkiin tuettuihin näyttöihin; editorissa voidaan valita tietty näyttö. Liitännät ja telakat voivat rajoittaa tukea.

### Laajennetut ohjaustoiminnot

Avaa komentopaletti näppäimillä **Ctrl+K, kun sovellusikkuna on aktiivinen**. Hae **Työnkulkusuunnittelija**, **Tallenna makro**, **Synkronoi profiilit** tai **Näppäindiagnostiikka** (sanamuoto noudattaa valittua kieltä). Työnkulkusuunnittelija muokkaa valittua toimintoruudun luonnosta: tiedosto-, prosessi- ja ikkunaehdot, eteenpäin ohitettavien vaiheiden määrät sekä pysäytys tai jatkaminen virhetilanteessa. Tallennin tallentaa näppäinyhdistelmiä ja odotuksia, ei tavallista kirjoittamista; Escape pysäyttää tallennuksen. Tarkista tulos ennen käyttöönottoa.

Valitse kirkkausvaiheessa kaikki tuetut näytöt tai tietty näyttö. Puuttuva valittu näyttö aiheuttaa virheen sen sijaan, että muutettaisiin huomaamatta toista näyttöä. Koontinäyttö näyttää reaaliaikaiset kirkkaus-, ääni- ja OBS-lukemat, kun vastaavat integraatiot ovat saatavilla; puuttuvaa lukemaa ei esitetä mitattuna arvona.

Määritä toimintoruutueditorissa tuettu fyysinen erikoisnäppäin ja valitse välitön painallus, napautus, pitkä painallus tai kaksoisnapautus. Pohjassa pidettävät kerrosvalitsimet aktivoivat väliaikaisesti toisen määrityksen ja palauttavat edellisen kerroksen vapautettaessa. Eleet ja kerrokset koskevat Corsair SDK G-näppäimiä ja laitekohtaisia tavallisia erikoisnäppäimiä, eivät tavallisia globaaleja pikanäppäimiä. Tavallisten näppäinten tunnistus tukee F13–F24- ja näppäimistön medianäppäimiä. Valmistajakohtaiset Fn-, profiili- ja kiertosäätimet vaativat yhä yhteensopivan valmistajasovittimen tai valmistajan määrityksen tavalliseksi näppäimeksi. Raw Input tarkkailee näppäintä eikä estä sen normaalia Windows-toimintaa. Näppäindiagnostiikka selittää profiili-, tauko-, yhteys- ja sidontaongelmat.

Profiilisynkronointi vaihtaa tiedostoja valitsemassasi kansiossa, myös olemassa olevan pilviasiakkaasi hallitsemassa kansiossa. Tarkista esikatselu ennen tuontia; tuodut määritykset ovat aluksi pois käytöstä ja laitekohtaiset määritykset poistetaan. Revisiot estävät huomaamattomat ylikirjoitukset, ja toimintosisällön sisällyttäminen on nimenomainen valinta. MAKRIVO ei tarjoa pilvitiliä eikä säilytä pilvitunnistetietoja. Ilmoitusalueella käytetään nyt MAKRIVO-sovelluksen omaa sinivihreää M-kuvaketta, ja taukotilassa siinä näkyy meripihkanvärinen taukomerkki.

<!-- generated-doc locale=fi-FI source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../HANDBOOK.md)

# MAKRIVO-käyttöopas

Valmistaja: **Aivana**. Dokumentaation tilannekuva: **2026-10-12**. Tämä opas kuvaa nykyistä työpuun toteutusta. Kokeelliset integraatiot ja laitteistosta riippuvat toiminnot on merkitty erikseen. Historialliset raportit kuvaavat päivättyä tilannekuvaansa, eivät takaa uudempien koontien toimivuutta.

## Dokumentaatio ja kielet

Käytä dokumentaation kielivalitsinta avataksesi koko oppaan, ominaisuusluettelon, vianmäärityksen, kehitysmuistiinpanot, tutkimuksen ja historialliset testiraportit millä tahansa 27 sovelluskielestä. Tuotetut käännökset ovat koneavusteisia ja rakenteellisesti tarkistettuja; kaikkia ei ole tarkistanut äidinkielinen puhuja. Tekniset tunnisteet ja suoritettavat esimerkit säilyvät muuttumattomina. Alkuperäinen lähde on edelleen saatavilla vertailua varten. Käännetty lisenssianalyysi on tiedoksi eikä korvaa alkuperäisiä kolmansien osapuolten oikeudellisia ehtoja tai lopullista EULA-sopimusta.

## Asennus ja ensimmäinen käynnistys

Alkuperäinen kohdeympäristö on Windows 11 x64. Pidä kaikki jakelupaketin tiedostot ja alihakemistot yhdessä. Käynnistä `MAKRIVO.exe`. Lähdekoodin paikallinen kopio ei ole asennusohjelma. Itsenäiset paketit sisältävät .NET-suoritusympäristönsä; suoritusympäristöstä riippuvaiset paketit vaativat asianmukaiset .NET 10 Desktop- ja ASP.NET-suoritusympäristöt. ONNX ja apuprosessien isäntäohjelmat on sisällytettävä pakettiin niitä tarvitsevia ominaisuuksia varten.

1. Käynnistä MAKRIVO ja valitse käyttöliittymän kieli asetuksista. Automaattinen valinta seuraa Windows-kieltä. Näppäimistöasettelu on erillinen asetus.
2. Valitse profiili ja lisää toimintoruutu haettavan toimintoluettelon avulla. Anna sille tunnistettava nimi ja valitse sen toiminnot.
3. Tarkista vaiheet ja määritä tarvittaessa pikanäppäin. Vältä yhdistelmiä, joita Windows tai kohdesovellus jo käyttää.
4. Tallenna luonnos. Tallentamattomat muutokset eivät kuulu tallennettuun kokoonpanoon.
5. Testaa vaarattomalla toiminnalla, kuten asiakirjan avaamisella ja tarkista suorituskyvyn palautetta.

Pääikkunan sulkeminen jättää sovelluksen tavallisesti ilmoitusalueelle. Avaa se uudelleen kaksoisnapsauttamalla ilmoitusalueen kuvaketta tai lopeta sovellus sen valikosta. Käynnistys Windows-käynnistyksen yhteydessä on valinnainen ja aluksi poistettu käytöstä. Taukopainike keskeyttää sidonnat; kuvakkeessa näkyy taukomerkki. Peruuta käynnissä olevat makrot käyttöliittymästä, ilmoitusalueelta tai määritetyllä peruutuspikanäppäimellä; oletus on `Ctrl+Alt+Escape`.

## Toimintoruudut, toiminnot ja komentopaletti

Toimintoruudut kuuluvat profiileihin ja voivat sisältää useita järjestettyjä vaiheita. Editori tukee tallennusta ja hylkäämistä, esikatselua, kumoamista ja uudelleentekemistä sekä vaiheiden uudelleenjärjestelyä. Luettelo tarjoaa esiasetuksia ohjelmille, asiakirjoille, URL-osoitteille, tekstille, leikepöydälle, pikanäppäimille, medialle, äänelle, ikkunoille, järjestelmätoiminnoille ja tuetuille integraatioille. Parametreja tarvitseva esiasetus avautuu luonnoksena.

Avaa komentopaletti näppäimillä `Ctrl+K`, kun MAKRIVO on aktiivinen. Hae tallennettuja toimintoja, esiasetuksia, profiilinvaihtoa ja työkaluja. Nuolinäppäimet valitsevat; Enter aktivoi; `Ctrl+Enter` muokkaa tuettua kohdetta; Escape sulkee. Osa käyttövalmiista komennoista suoritetaan heti, kun taas määritetyt toiminnot vaativat tarkistuksen. Erillinen globaali paletin pikanäppäin voidaan määrittää; uusissa kokoonpanoissa se on `Ctrl+Shift+Space`.

Teksti- ja leikepöytävaiheet tukevat muuttujia `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ja `{{user}}`. Päivämäärän muoto on `yyyy-MM-dd`; kellonajan muoto on `HH:mm`. Tuntemattomat muuttujat säilyvät muuttumattomina. Leikepöytää luetaan vain pyydettäessä. Leikepöytätoiminto kirjoittaa tekstiä leikepöydälle liittämättä sitä automaattisesti.

Käyttöliittymästä käynnistetyt toiminnot voivat kohdistua viimeiseen ulkoiseen sovellukseen. Jos kohde katoaa tai Windows estää etualalle tuomisen, aktivoi kohde käsin ja yritä uudelleen. Näppäinten onnistunut välitys ei todista kohdesovelluksen suorittaneen tehtäväänsä.

## Profiilit ja työympäristön määritys

Profiilin täsmäytysehtoina voidaan käyttää prosessin nimeä ja ikkunan otsikon osaa. Kun molemmat on asetettu, molempien on täsmättävä. Profiilien järjestys ratkaisee kilpailevat osumat; oletusprofiilia käytetään, jos muuta osumaa ei ole. Ota automaattinen valinta erikseen käyttöön. Profiilin lukitus estää automaattiset muutokset nykyisessä istunnossa.

Avaa työympäristön määritys työkaluista tai komentopaletista. Valitse ohjelmat, asiakirjat, verkkosivustot, ääni- tai näyttökohde tai OBS-kohtaus. Avustaja valmistelee työympäristö-, kokous-, suoratoisto-, tutkimus- tai projektiprofiileja. Tarkista luodut vaiheet ennen tallennusta. Määritys ei suorita valmisteltuja toimintoja.

## Makrot, ehdot ja tallennus

Makrot suorittavat vaiheensa järjestyksessä ja tukevat odotuksia, toistoja ja peruutusta. Yleinen toistoraja on 100. Käytä esikatselua ennen toimintoja, jotka kirjoittavat toiseen sovellukseen tai muuttavat järjestelmän tilaa.

Kohdeikkunan odotus hyväksyy prosessin, otsikon osan ja aikakatkaisun väliltä 1 ja 600000 ms. Vähintään yksi suodatin tarvitaan. Vastaavan näkyvän ikkunan on vastattava ja tultava etualalle ennen seuraavien vaiheiden jatkamista. Valmis ikkuna ei osoita, että asiakirjan tai palvelun lataus olisi päättynyt.

Työnkulkueditorissa voi asettaa ehtoja tiedoston olemassaololle, prosessin käynnissäololle ja ikkunan otsikolle. Epätosi ehto ohittaa vaiheen ja voi ohittaa myös muita myöhempiä vaiheita. Ilmaise vaihtoehtoinen haara kääntämällä ehdon totuusarvo. Valitse tarkoituksella, pysäytetäänkö suoritus virheeseen vai jatketaanko virheestä huolimatta; jatkaminen voi jättää myöhemmät vaiheet ilman niiden odottamia edellytyksiä.

Toimintotallennin tallentaa näppäinyhdistelmiä ja odotuksia, ei tavallista kirjoittamista. Se tukee muokkausnäppäimiä sisältäviä pikanäppäimiä ja funktionäppäimiä. Escape lopettaa tallennuksen. Tarkista luettelo ja ota se käyttöön tallentamattomana toimintoruudun luonnoksena; tallenna sitten erikseen. Käyttöliittymän napsautusten tallennus on erillinen, jäljempänä kuvattu työkalu.

## Näppäimistöt, eleet ja kerrokset

Näppäimistösivu luettelee havaitut laiterajapinnat ja viiteasettelut. Yksi fyysinen näppäimistö voi tarjota useita rajapintoja. Valitse oikea kieli ja geometria; pelkkä tuotenimi ei määritä ISO- tai ANSI-asettelua.

Tavalliset globaalit pikanäppäimet toimivat kaikilla näppäimistöillä. Laitekohtaiset Raw Input -sidonnat tarkkailevat tiettyä laitetta ja skannauskoodia, mukaan lukien tuetut funktio- ja medianäppäimet. **Raw Input ei estä tavallista Windows-syötettä. Yksinomaisessa käytössä oleva toinen näppäimistö ei ole tuotantovalmis.** Allekirjoitettu, lisensoitu ja testattu suodatinajuritaustajärjestelmä tarvitaan edelleen; MAKRIVO ei asenna sellaista.

Tuetut erikoisnäppäinsidonnat voivat käyttää välitöntä painallusta, napautusta, pitkää painallusta tai kaksoisnapautusta. Pohjassa pidettävä kerrosnäppäin valitsee väliaikaisesti toisen näppäinkartan ja palauttaa peruskerroksen vapautettaessa; kerroksen valintanäppäimet eivät suorita omaa makroaan. Pitkän painalluksen kynnys on 150 ja 5000 ms ja kaksoisnapautuksen aikaikkuna 150 ja 1000 ms. Nämä määritykset ovat erillisiä tavallisista globaaleista pikanäppäimistä.

Erikoisnäppäinten ohjattu määritys tunnistaa yhteensopivat näppäimet suorittamatta niille määritettyjä toimintoja tunnistuksen aikana. Diagnostiikka selittää puuttuvat laitteet, keskeytetyt sidonnat, profiilien ristiriidat ja konfliktit. Peittokuva näyttää valitun asettelun ja sidonnat; sen oletusarvoinen pohjassa pidettävä pikanäppäin on `Ctrl+Alt+F1`.

| integraatio | Vaatimukset ja rajoitukset |
| --- | --- |
| Corsair | Käynnissä oleva iCUE, SDK-käyttöoikeus ja yhteensopiva malli. G1–G6 ja tuetut RGB-toiminnot; valmistajakohtaiset Fn-, rulla-, lukitus- ja profiilisäätimet eivät yleensä ole käytettävissä G-key API -rajapinnan kautta. |
| Logitech G | Alkuperäiset paikalliset x64 G-key/LED SDK -kirjastot vaativat nimenomaisen hyväksynnän. G-key-tapahtumat eivät yksilöi useita samanlaisia fyysisiä näppäimistöjä. |
| Razer | Chroma RGB ja tavallinen syöte; määritä tarvittaessa valmistajakohtaiset säätimet Synapse-ohjelmassa. |
| SteelSeries | GameSense RGB ja tavallinen syöte; määritä tarvittaessa valmistajakohtaiset säätimet GG-ohjelmassa. |
| Wooting | Yhteensopiva alkuperäinen paikallinen x64 SDK analogisille arvoille ja kynnysarvoille; tavallinen HID-syöte pysyy aktiivisena. |
| Muut näppäimistöt | Tavalliset pikanäppäimet tai tuetut fyysisten skannauskoodien sidonnat. Fn ja valmistajakohtaiset säätimet riippuvat mallista ja valmistajan määrityksistä. |

Valmistajatutkimus kuvaa dokumentoituja ominaisuuksia, ei toteutettua tai testattua yhteensopivuutta jokaisen luetellun laitteen kanssa. Uudet sovittimet vaativat edelleen hyväksyntätestauksen todellisella laitteistolla. Älä lataa SDK-binäärejä epäluotettavista lähteistä; SDK:n jakeluoikeudet ovat eri asia kuin tekninen yhteensopivuus.

## Näytöt, äänet ja OBS

Äänivaiheet asettavat äänenvoimakkuuden välille 0 ja 100 prosenttia ja mykistävät tuettuja äänilähtöjä tai mikrofonipäätepisteitä, poistavat niiden mykistyksen tai vaihtavat mykistystilaa. Valitse tietty päätepiste tai Windows-oletuslaite. Puuttuva tallennettu kohde aiheuttaa virheen sen sijaan, että ohjattaisiin huomaamatta toista laitetta. Päivitä laiteluettelo uudelleenkytkennän jälkeen. Päätepisteen ohjaus ei tarkoita itsenäisen sovelluskohtaisen mikserin tukea.

Näyttötoiminnot lisäävät tai vähentävät kirkkautta 10 prosenttiyksikköä tai asettavat arvon välille 0 ja 100. Sisäiset näytöt käyttävät tuettuja WMI-rajapintoja; ulkoiset näytöt vaativat DDC/CI-tuen ja yleensä sen käyttöönoton näytön valikosta. Telakat, sovittimet ja kaapelit voivat estää ohjauksen. Valitse kaikki tuetut näytöt tai yksi näyttö. Uudelleenkytkentä voi muuttaa käytettävissä olevia tunnisteita; tarkista tallennettu kohde. Puuttuvat lukemat esitetään ei saatavilla -tilassa, ei mitattuina arvoina.

OBS vaatii, että WebSocket v5 on käytössä OBS:n työkaluissa. Määritä päätepiste, testaa yhteys ja lataa kohtausten sekä tulolähteiden nimet ennen tallennusta. Tuettuja toimintoja ovat kohtauksen vaihto, suoratoiston ja tallennuksen käynnistys, pysäytys ja tilan vaihto, tallennuksen tauotus ja jatkaminen sekä tulolähteen mykistys. Salasanat voidaan tallentaa Windows Credential Manager -palveluun, ja ne jätetään pois kokoonpanovienneistä. Onnistunut OBS-komento ei vahvista kaikkia suoratoistoketjun myöhempiä vaikutuksia.

## Tuloksen tarkistus ja suoritushistoria

Ota tuloksen tarkistus käyttöön soveltuville vaiheille. Sovellus erottaa ennakkotarkistukset, vahvistetut vaikutukset, epäonnistumiset, peruutuksen ja vaikutukset, joita ei voida tarkistaa. Prosessin olemassaolo ei tarkoita sovelluksen valmiutta. Suhteellisille muutoksille, pikanäppäimille ja tekstinsyötölle ei voida antaa yleistä onnistumisvahvistusta. Tarkista toimintaloki ja tarkistushistoria toiminnon epäonnistuessa. Korjaa määritysvirheet ennen koko makron suorittamista uudelleen.

## Käyttöliittymäautomaatio ja napsautusten tallennus

Lisää UI Automation -vaihe, valitse toiminto ja poimi osoittimen alla oleva kohde näytetyn viiveen jälkeen. Pelkkä poiminta ei suorita mitään. Tarkista valitsin ja vaadi yksikäsitteinen osuma. Tuetut toiminnot ovat `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ja `WaitState`. Salasanakentät, käytöstä poistetut ohjaimet ja moniselitteiset valitsimet hylätään.

Käyttöliittymäautomaatio toimii rajoitetussa apuprosessissa. Se riippuu kohdesovelluksen Windows-saavutettavuustuesta ja käyttöoikeuksista. Napsautustallennin tallentaa tuettuja toimintoja ja taukoja, ei yleistä tekstinsyöttöä. Tarkista ja validoi luonnos ennen sen käyttöönottoa. Ulkoisen sovelluksen käyttöliittymämuutokset voivat tehdä valitsimista kelvottomia. Prosessieristys rajoittaa kaatumisia ja peruutusta, mutta ei ole tietoturvahiekkalaatikko.

## Laajennukset

Avaa laajennukset ja tarkista paketti ennen tuontia. Paketin rakenne, manifesti ja tiivisteet tarkistetaan. Tuotu laajennus on aluksi poistettu käytöstä. Lue ilmoitetut toiminnot ja vahvista luottamuksesi julkaisijaan ennen käyttöönottoa. Laajennukset toimivat käyttäjäoikeuksillasi erillisessä prosessissa; tämä prosessi **ei ole hiekkalaatikko**. Asenna vain paketteja, joihin luotat.

Hallinta tukee vientiä, käytöstä poistamista, asennuksen poistamista sekä aiemman version palauttamista tai hylkäämistä. Puuttuvat riippuvuudet estävät kutsumisen. Esimerkkipaketti tarjoaa ajastimen ja paikallisen tilan toimintoja. Tuettu skeema on dokumentoitu JSON-Schema-osajoukko, ei kaikkien mahdollisten JSON-Schema-ominaisuuksien toteutus. Julkista laajennuskauppaa ei ole.

## Mobiilipaneeli

1. Avaa mobiilipaneeli, valitse paikallinen IP-osoite ja valitse erikseen näkyviin annettavat profiilit. Oletusarvoinen takaisinkytkentäosoite on käytettävissä vain itse tietokoneella; valitse sopiva LAN-osoite puhelinta tai tablettia varten.
2. Käynnistä paneeli ja lue sen HTTPS-osoite, varmenteen sormenjälki ja kertakäyttöinen kuusinumeroinen pariliitoskoodi.
3. Avaa osoite samalla luotetulla verkolla olevassa laitteessa. Vertaa SHA-256 sormenjälkeä luotetun kanavan kautta ennen paikalliseen varmenteeseen luottamista. Sovellus ei asenna luotettua varmennetta automaattisesti.
4. Muodosta pariliitos näytetyllä koodilla ja testaa yhtä vaaratonta toimintoruutua. LAN-käyttö voi vaatia Windows-palomuurin luvan antamista käsin. Älä avaa porttia internetiin.
5. Pysäytä paneeli käytön päätyttyä tai peruuta pariliitettyjen asiakkaiden käyttöoikeus sovelluksesta.

Palvelimen yksityiset avaimet suojataan Windows DPAPI:lla; asiakkaiden tunnistetiedot tallennetaan tiivisteinä. IP-osoitteen muutos tai varmenteen uusiminen voi vaatia luottamuksen vahvistamista uudelleen. Paneeli näyttää toimintojen ja tarkistusten tilat, ei kaikkia mahdollisia reaaliaikaisia antureita. Selainasettelun ja paikallisen HTTPS:n tarkistukset eivät korvaa oikean puhelimen tai tabletin LAN-hyväksyntätestausta.

## Paikallinen AI-kokousavustaja

Tämä valinnainen kokeellinen avustaja on oletusarvoisesti poistettu käytöstä. Avaa se työkaluista, ota se erikseen käyttöön, valitse suoritusympäristö ja malli sekä lisää vain resurssit, jotka haluat sen huomioivan. Ilmaise tavoite, luo luonnos, tarkista ratkaisemattomat pyynnöt ja siirrä tulos tavalliseen editoriin. Mitään ei suoriteta automaattisesti; tallennus ja suoritus edellyttävät erillisiä toimia. Automaattista siirtymistä toiseen suoritusympäristöön ei ole.

| Suoritusympäristö | Määritys |
| --- | --- |
| Ollama | Erillinen paikallinen palvelu ja paikallisesti asennettu malli `127.0.0.1:11434`. Ilmoitetut kauko- ja pilvimallit hylätään. |
| LM Studio | Paikallinen palvelin, paikallisesti ladattu malli ja JSON-skeematuki osoitteessa `127.0.0.1:1234`. Älä ota etäreititystä käyttöön paikallista käsittelyä varten. |
| llama.cpp | Paikallinen `llama-server` ja yhteensopiva JSON-skeemaa tukeva malli osoitteessa `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Luotettu yhteensopiva paikallinen GenAI-tekstimallikansio; valitse `genai_config.json`. Päättely käyttää mukana toimitettua apuprosessia ilman HTTP:tä. |

HTTP-asiakkaat käyttävät vain takaisinkytkentäosoitteita, eivät käytä välityspalvelimia eivätkä seuraa uudelleenohjauksia. MAKRIVO ei voi luotettavasti todeta, kutsuuko yhteensopiva paikallinen palvelin sisäisesti etäpalvelua. Todennusta vaativia paikallisia palvelimia ei voi määrittää tässä versiossa. Mallipainoja, Python-ympäristöä tai paikallisia päättelypalvelimia ei ladata eikä asenneta automaattisesti.

ONNX-paketit tarvitsevat tiedoston `tokenizer.json` ja kelvollisen keskustelumallipohjan. Tiedoston `tokenizer_config.json` tuetut muodot ovat merkkijonomuotoinen `chat_template`, sanakirja, jonka `default`-arvo on merkkijono, tai yksiselitteinen nimettyjen/oletusmallipohjien luettelo. Jos mallipohja puuttuu tai sen arvo on null, varavaihtoehtona käytetään tiedostoa `chat_template.jinja`. Tyhjät, moniselitteiset, liian suuret tai vääräntyyppiset mallipohjat hylätään. Mielivaltaiset ONNX-tiedostot, upotus- ja näkömallit sekä vain GPU:lle tarkoitetut paketit eivät korvaa yhteensopivia GenAI-tekstimalleja. Mukana toimitettavan GenAI-suoritusympäristön versio on 0.17.1, CPU. Käytä vain luotettuja malleja; prosessien erottaminen ei ole hiekkalaatikko.

Avustaja tukee valittuja ohjelmia ilman käynnistysargumentteja, valittuja asiakirjoja ja verkkosivustoja sekä mikrofonin mykistystä erikseen pyydettäessä. Se ei luo viestejä, tapaamisia, mielivaltaisia komentosarjoja, vapaamuotoisia napsautuksia tai latauksia eikä vaihda kuulokelaitetta. Tavoite ja resurssien nimet lähetetään valittuun paikalliseen suoritusympäristöön; resurssien polkuja ja tiedostojen sisältöä ei toimiteta erikseen. Käyttäjän kirjoittama tavoite voi itsessään sisältää henkilötietoja. Paikallisen suoritusympäristön oma verkkotoiminta jää käyttäjän vastuulle.

ONNX-pyynnön aikabudjetti on 90 sekuntia mallin lataus mukaan lukien; rajat ovat enintään 3072 kehotetokenia ja 1024 tuotettua tokenia, lisäksi mallin konteksti rajoittaa niitä. Virheellinen tai katkennut JSON ja tuntemattomat resurssitunnisteet hylätään. Lopeta pyyntö peruuttamalla tai sulkemalla. Satunnaismallilla tehty suoritusympäristötesti osoittaa suorituksen toimivuuden, ei hyödyllistä suunnittelun laatua tai ajansäästöä. Paikalliset kokeilulokit sisältävät tapahtumatunnisteita ja ajoitustietoja, eivät tavoitetekstiä tai asiakirjasisältöä; niitä ei lähetetä automaattisesti.

Myöhemmässä pienen Instruct-mallin arvioinnissa neljä saksan- ja englanninkielistä koetta tuotti nolla kelvollista suunnitelmaa. Natiivi päättely toimi, mutta malli ei täyttänyt tiukkaa suunnittelusopimusta. Tulos on kielteinen laatuhavainto, ei mallisuositus eikä osoitus avustajan hyödyllisyydestä. Tarkka malliversio ja rajoitukset löytyvät päivätystä paikallisen suoritusympäristön testiraportista.

## Tuonti, vienti, synkronointi ja varmuuskopiointi

Tarkista tuonnin esikatselu ennen kokoonpanon korvaamista. Tuodut toiminnot ovat aluksi poistettu käytöstä, ja siirrettävä synkronointi poistaa laitekohtaiset sidonnat. Määritä laitteet uudelleen ja ota toiminnot harkitusti käyttöön tarkistuksen jälkeen. Tarkista tekstien ja toimintojen sisältö ennen viennin jakamista; salasanojen pois jättäminen ei tee kaikesta käyttäjän luomasta toimintosisällöstä arkaluonteetonta.

Profiilisynkronointi vaihtaa tiedostoja valitussa kansiossa, jota voi haluttaessa hallita olemassa oleva pilviasiakas. MAKRIVO ei tarjoa pilvitiliä eikä tallenna pilvipalvelun tunnistetietoja. Revisiot estävät huomaamattomat ylikirjoitukset. Toimintosisällön sisällyttäminen edellyttää nimenomaista valintaa. Varmuuskopioi kokoonpano ennen sen korvaavaa tuontia. Oletuskokoonpano on `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; historiallinen hakemistonimi säilytetään yhteensopivuuden vuoksi. Sulje sovellus ennen manuaalista kopiointia tai palauttamista. Tallennetut SDK-hyväksynnät ja käyttöjärjestelmän suojaamat tunnistetiedot voidaan joutua luomaan uudelleen toisessa tietokoneessa.

## Ongelmien ratkaiseminen

| oireet | Mitä tarkistaa |
| --- | --- |
| Pikanäppäin ei tee mitään | Sovellus käynnissä, sidonnat eivät ole tauolla, oikea aktiivinen profiili, tallennettu ja käytössä oleva toimintoruutu, pikanäppäinristiriidat ja käytettävissä oleva kohde. |
| Tavoiteikkuna ei voi aktivoida | Aktivoi se käsin. Windows-järjestelmän etualarajoitukset ja käyttöoikeustasojen erot voivat estää automaation. |
| Erikoisnäppäintä ei tunnisteta | Valmistajan palvelun, SDK:n ja mallin tuki; määritä valmistajakohtaiset säätimet lähettämään tuettu tavallinen näppäin, jos mahdollista. |
| Toinen näppäimistö myös kirjoittaa tavallisesti | Odotettu toiminta käytettäessä Raw Input -rajapintaa. Syötteen yksinomaista estämistä ei ole toteutettu tuotantokäyttöön. |
| Ääni tai kirkkaus ei ole saatavilla | Päivitä laitteet; tarkista valittu tunniste, päätepisteistunnot, WMI/DDC/CI-tuki sekä telakka- ja kaapelirajoitukset. |
| OBS offline | WebSocket v5 käytettävissä, oikea loppupiste / salasano, saavutettavissa oleva palvelu ja voimassa oleva kohtaus / sisääntunnus. |
| Käyttöliittymäautomaatio epäonnistuu | Apuprosessin isäntäohjelma mukana, yksikäsitteinen kelvollinen valitsin, saavutettava käytössä oleva kohde, tuettu ohjain ja käyttöoikeudet. Yritä tilapäisen aikakatkaisun jälkeen uudelleen vasta tarkistettuasi kohteen. |
| Mobiilipaneeli ei ole tavoitettavissa | LAN-osoite takaisinkytkentäosoitteen sijaan, sama verkko, palvelin käynnissä, hyväksytyt profiilit, varmenteen sormenjälki ja luottamus sekä käsin annetut palomuuriluvat. |
| Plugin epäonnistuu | Paketin eheys, nimenomainen luottamus ja käyttöönottotila, riippuvuudet, protokollan yhteensopivuus ja suoritusloki. |
| AI-pyyntö epäonnistuu | Oikea valittu paikallinen suoritusympäristö, palvelun ja mallin valmius, tuettu JSON-skeema, luotettu yhteensopiva mallikansio ja pyyntörajat. |
| Asettelu tai teksti näyttää väärältä | Erota käyttöliittymäkieli näppäimistön geometriasta; muuta ikkunan kokoa, tarkista näytön skaalaus ja ilmoita tarkka sivu, kieli ja koontiversio. |

Sisällytä hyödylliseen vikailmoitukseen Windows-versio, sovelluksen koonti tai commit, kieli, näytön skaalaus, laitteistomalli, mahdollisimman lyhyt toistosekvenssi ja asianmukainen arkaluonteisista tiedoista puhdistettu loki. Poista henkilökohtaiset polut, toimintosisältö ja tunnistetiedot ennen julkaisua.

## Koonti, testaus ja paketointi

Käytä tiedostossa `global.json` kiinnitettyä SDK-versiota. Suorita tietovaraston juuresta:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows-integraatiotestit voivat avata eristettyjä ikkunoita ja määrittää tai vapauttaa tuettuja valmistajakohtaisia sidontoja tilapäisesti. Suorita ne ilman toista ohjainta, joka hallitsee samoja sidontoja. Oletusarvoiset kirkkaus- ja äänitestit ovat vain lukevia; erikseen käynnistettävät laitteiston muutos- ja palautustestit ovat erillisiä. Simuloituja protokollia, renderöityjä kuvakaappauksia tai satunnaisia päättelygraafeja ei saa esittää todellisen laitteiston tai mallin laadun hyväksyntänä.

Täydellinen paketointityökalu kokoaa apuprosessien isäntäohjelmat, mobiiliresurssit, esimerkkilaajennuksen, ONNX-suoritusympäristön ja saatavilla olevat riippuvuusilmoitukset. Säilytä alihakemistot. Onnistunut koonti ei ole allekirjoitettu asennusohjelma eikä oikeudellinen julkaisuhyväksyntä. Lähdekoodi ja koonnit voivat kehittyä erikseen; vertaa dokumentaation tilannekuvaa ja committia asennettuun versioon.

## Lisensointi, yksityisyys ja julkaisutilanne

Suunniteltu kaupallinen malli on kertamaksu ja ostetun pääversion pysyvä käyttöoikeus; lopullinen hinta, sallittu laitemäärä ja päivitysehdot on määriteltävä todellisissa myyntiehdoissa. Tämä opas ei vahvista tuotantokäyttöistä osto- tai aktivointijärjestelmää. Jokapäiväiseen ohjaukseen ei tarvita pilvitiliä.

Älä levitä valmistajakohtaisia SDK-binäärejä ilman todennettua lupaa. Säilytä lisenssit ja kolmansien osapuolten ilmoitukset kaikille toimitetuille riippuvuuksille, mukaan lukien .NET- ja ONNX-paketit. Mallipainoilla on omat ehtonsa. Päivätty lisenssitarkastus edeltää joitakin uusia riippuvuuksia eikä hyväksy näitä lisäyksiä. Tietovaraston julkisuus ei itsessään anna yleistä avoimen lähdekoodin lisenssiä.

Todellinen valmistajalaitteisto, puhelimen ja tabletin LAN-käyttö, eri näyttöjen ja DPI-asetusten väliset siirtymät, pitkäaikainen käyttö, täydellinen kielentarkistus, yksinomaisen käytön ajurituki ja hyödyllinen AI-suunnittelun laatu vaativat edelleen erillisen hyväksyntätyön. Älä koskaan päättele hyväksyntää pelkästä ominaisuuden olemassaolosta.

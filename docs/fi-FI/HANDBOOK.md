<!-- generated-doc locale=fi-FI source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../HANDBOOK.md)

# MAKRIVO — Käyttöopas

Valmistaja: **Aivana**. Dokumentti snapshot: **2026-10-12**. Tämä käsikirja kuvastaa nykyistä työpuun toteuttamista. kokeelliset integroinnit ja laitteistoon riippuvat toiminnot tunnistetaan nimenomaisesti. Historialliset raportit kuvaavat niiden päivitetyn kuvan, ei takuun uudelle rakenteelle.

## Dokumentti ja kielet

Käytä dokumenttikielen valintaohjelmaa avata täydellinen käsikirja, ominaisuus, ongelmanratkaisu, kehitysilmoitukset, tutkimus ja historialliset testitiedot jollakin 27 sovelluskielistä. Tuotetut käännökset ovat koneellisesti avustettuja ja niillä on rakenteellisia tarkastuksia; ne eivät ole kaikki saaneet alkuperäisen puhujan tarkastusta. Tekniset tunnisteet ja täytäntöönpanokelpoiset esimerkit pysyvät muuttumattomina. Alkuperäinen lähde on edelleen vertailukelpoinen. Käännetty lisenssin analyysi on informatiivinen eikä korvaa alkuperäisiä kolmannen osapuolen oikeudellisia ehtoja tai lopullista EULA:ta.

## Asennus ja ensimmäinen käynnistys

Alkuperäinen tavoite on Windows 11 x64. Pidä kaikki tiedostot ja jakeluosasto yhdessä. `MAKRIVO.exe`. Alkuperäinen tarkastus ei ole asentaja. itsekohtaiset paketit sisältävät niiden .NET-käyttöaikaa; puitteet riippuvaiset paketit vaativat asianmukaista .NET-ohjelmaa. 10 Desktop ja ASP.NET. ONNX ja kumppanit on sisällytettävä niiden ominaisuuksiin.

1. Käynnistä MAKRIVO ja valitse käyttöliittymän kieli asetuksissa. Seuraa automaattisesti Windows Keyboard -järjestelmä on erillinen järjestelmä.
2. Valitse profiili ja lisää sitten otsikoituun toimenpide-katalogiin. Anna sille tunnistettavan nimen ja valitse sen toimet.
3. Tarkista vaiheet ja määritä lyhyt lyhyt tarvittaessa. Vältä yhdistelmät, joita on jo käytetty Windows tai kohderyhmäsi.
4. Pidä suunnitelma. Muutos ei ole pelastettu.
5. Testaa vaarattomalla toiminnalla, kuten asiakirjan avaamisella ja tarkista suorituskyvyn palautetta.

Pääikkunan sulkeminen jättää yleensä hakemuksen ilmoitusalueella. Kaksinkertaisesti napsauta tray-ikonia uudelleen, tai käytä tray-menua ulos. Aloittaminen Windows on valinnaista ja aluksi poissa. Pausen hallinta keskeyttää sidokset; ikoni näyttää pausen. Poista käynnissä olevat makronet käyttäen käyttöliittymää, traiaa tai määritettyä peruuttamista lyhyt; oletusarvo on `Ctrl+Alt+Escape`.

## Tilat, toimet ja komento paletti

Tilat kuuluvat profiileihin ja voivat sisältää useita tilattuja askeleita. Toimittaja tukee Save/Discard, Preview, undo/redo ja askel uudelleenjärjestely. Katalogissa on ohjelmien, asiakirjojen, URL-osoitteiden, tekstien, clipboardin, lyhenteiden, median, äänen, ikkunoiden, järjestelmätoimintojen ja tuettujen integrointien ennakkoluuloja. Parametrit edellyttävät esityksiä, jotka avautuvat luonnokseksi.

Avaa paletti `Ctrl+K` ja MAKRIVO on keskittynyt. Etsi tallennettuja toimintoja, ennakkoluuloja, profiilin vaihtamista ja työkaluja. Valitse avaimet; Käytä aktivoi; `Ctrl+Enter` muokkaa tuettu elementti; Escape sulkee. Jotkut valmiit käskyt suoritetaan välittömästi, kun taas konfiguroidut toimet vaativat tarkistusta. Erillinen maailmanlaajuinen lyhytkesto voidaan konfiguroida; uusia konfiguraatioita käytetään `Ctrl+Shift+Space`.

Teksti ja clipboard askeleet tukea `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ja `{{user}}`. Date muotoilu on `yyyy-MM-dd`; Ajan muotoilu on `HH:mm`. Tuntematon muuttujat pysyvät muuttumattomina.Clipboard-luku tapahtuu vain pyydettäessä.Clip board-toiminta kirjoittaa tekstin automaattisesti mukauttamatta sitä.

UI: n käynnistämät toimet voivat kohdistaa viimeisen ulkoisen sovelluksen. Jos kohde katoaa tai Windows estää etukäteen aktivoinnin, aktivoi manuaalisesti kohde ja vetää. Menestyksekäs avain toimitus ei osoita, että tavoite on suorittanut tehtävänsä.

## Profileja ja työpaikkoja

Profiilit voivat vastata prosessin nimeä ja osan ikkunan otsikosta. Kun molemmat on tehty, molempien on oltava yhteisiä. Profile-tilaus ratkaisee kilpailevat ottelut; oletusarvoinen profiili on palautus. Automaattinen valinta mahdollistaa selkeästi. Lock Profile estää automaattiset muutokset nykyiselle istunnolle.

Avoin työpaikka asetukset työkaluista tai palettia. Valitse ohjelmat, asiakirjat, verkkosivut, äänen/näytön kohde tai OBS kohtaus. Avustaja valmistelee työpaikkaa, kokouksia, suoratoistoa, tutkimusta tai projektiprofiileja. Tarkista tuotetut vaiheet ennen säästöä. Asennus ei toteuta valmistettuja toimia.

## Macros, ehdot ja tallennus

Macros suorittaa askeleensa järjestyksessä ja tukee odotuksia, toistoja ja peruuttamista. Yleinen toistamisraja on 100. Käytä ennakkoa ennen toimintojen suorittamista muuhun sovellukseen tai järjestelmän tilaan muuttamiseen.

Tavoiteikkuna hyväksyy prosessin, otsikon ja aikarajan 1 ja 600,000 ms. Tarvitaan ainakin yksi suodatin. Näkyvä ikkuna vastaa ja tulee etukäteen, ennen kuin seuraavat vaiheet jatkavat. Valmis ikkuna ei osoita, että asiakirja tai palvelu on valmis latautumaan.

Työnkulun muokkaaja lisää tiedostoja, prosessit ja ikkunan otsikoita. Virheellinen tila ohjaa askeleen ja voi siirtyä eteenpäin. Kieltäytyy ehdosta ilmaista vaihtoehtoinen ryhmi. Valitse pysähdys virheeseen tai jatko virheeseen tarkoituksella; jatkaminen voi jättää myöhemmät vaiheet ilman odotettuja edellytyksiä.

Toiminto-rekisteri tallentaa lyhytkestoisia kortteja ja odottaa, ei tavallista kirjoitusta. Se tukee muokkaus lyhytkestoja ja toiminnallisia avaimia. Pelastus lopettaa tallennuksen. Tarkastele luetteloa ja hyväksy sitä säästämättömänä tyylisuunnitteluna, sitten säästää selvästi. UI-painikkeen tallennus on erillinen työkalu, joka on kuvattu alla.

## Avaimet, käskyt ja kerrokset

Näppäimistö luetteloi havaitut käyttöliittymät ja viittaukset. Yksittäinen fyysinen näppäimistö voi altistaa useita käyttöliittymää. Valitse oikea kieli ja geometria; yksin tuote nimi ei tunnista ISO tai ANSI-järjestelmää.

Standardi globaali hotkeys sovelletaan kaikkiin näppäimistöihin. laite-erityinen Raw Input linkit tarkkailevat tiettyä laitetta ja skannauskoodin, mukaan lukien tuettu toiminto ja median avaimet. **Raw Input Ei häiritse tavallista Windows 2. Yksinomainen toissijainen näppäimistö ei ole valmistettu.** allekirjoitettu, lisensoitu ja testattu suodatinpitäjä on edelleen tarpeen; ei ole asennettu MAKRIVO.

Tuki erityinen avain sidokset voivat käyttää välittömästi painamalla, painamalla, pitämällä tai kaksoispainikkeita. Held-laatikko avaimet väliaikaisesti valitsee toinen kartta ja palauttaa peruslaatikko vapautuksen; laatikko-valinta avaimet eivät käynnistä omaa makro. 150 Tähän 5,000 ms; kaksoisnäppäimet 150 Tähän 1,000 Nämä kartat eroavat tavallisista globaaleista hotkeys.

Erityisen avaimen miekka havaitsee yhteensopivia avaimia suorittamatta niiden määräämiä toimenpiteitä pidätyksen aikana. Diagnoosi selittää käytettävissä olevia laitteita, keskeytyneitä sidoksia, profiilin väärinkäytöksiä ja konflikteja. Ylityö näyttää valitun asetuksen ja sidosten; sen oletusarvoinen lyhytkesto on `Ctrl+Alt+F1`.

| integraatio | Vaatimukset ja rajoitukset |
| --- | --- |
| Corsair | Running iCUE, SDK-yhteys ja yhteensopiva malli. G1–G6 ja tuettu RGB-toimintoja; omistettu Fn, pyörä, lukko ja profiilin ohjaimet eivät yleensä altistu G-key API: lle. |
| Logitech G | Alkuperäinen x64 G-key/LED SDK -kirjastot vaativat nimenomaista hyväksyntää. G-näytöt eivät yksilöllisesti tunnista useita identtisiä fyysisiä näppäimistöjä. |
| Razer | Chroma RGB ja vakiintunut sisäänkäynti; kartta omistettuja ohjauksia Synapse tarvittaessa. |
| SteelSeries | GameSense RGB ja vakiintunut sisäänkäynti; kartta omistuksen GG tarvittaessa. |
| Wooting | Yhteensopiva alkuperäinen x64 SDK analogisiin arvoihin ja raja-arvoihin; normaali HID-tulostus pysyy aktiivisena. |
| Muut avaimet | Standard hotkeys tai tukea fyysinen skannaus koodi sidokset. Fn ja omistusoikeudet riippuvat mallista ja toimittajan konfiguraatiosta. |

Valmistajan tutkimus kuvailee dokumentoituja valmiuksia, joita ei ole toteutettu tai testattu yhteensopivuutta jokaiseen luetelluille laitteille. Uudet adapterit tarvitsevat edelleen todellista laitteiston hyväksyntää. Älä lataa SDK:tä epäluotetuista lähteistä; SDK:n jakeluoikeudet erotetaan teknisestä yhteensopivuudesta.

## Näytöt, äänet ja OBS

Audio-vaiheet määrittävät volyymin 0 ja 100 prosenttia ja mute/unmute/toggle tuettu tulos tai mikrofonin pääpisteet. Valitse tietty loppupiste tai käytä oletusta Windows. Puuttuva pelastettu tavoite tuottaa virheen sijaan, että se ohjaa hiljaa toista laitetta. Puhdista laitteet uudelleenkäynnistyksen jälkeen. Lopullinen ohjaus ei ole riippumaton sovellus sekoittimen tuki.

Näytä toimenpiteet lisätä/vähenemään kirkkautta 10 prosenttiyksikköä tai asettaa arvo 0 100. Sisäiset näytöt käyttävät tuettuja WMI-liitäntöjä; ulkoiset näytöt vaativat tukea DDC/CI ja yleensä sen näytön valinta. Kabelit ja adapterit voivat estää ohjauksen. Valitse kaikki tuetut näytöt tai yksi näyttö. Uusi yhteys voi muuttaa käytettävissä olevia identiteettejä; tarkistaa pelastetun tavoitteen. Puuttuvat lukemat osoittautuvat käytettävissä oleviksi kuin mitattuina arvoina.

OBS edellyttää WebSocket v5 käytettävissä OBS Työkalut. Aseta loppupiste, testaa yhteys ja latauspaikka / sisääntunnukset ennen tallennusta. Tukemassa olevat toimet ovat kohtauksen vaihtaminen, virta/rekordin aloittaminen/pysäytyminen/toppelu, tauon tallentaminen/resume ja sisäänpääsy mute. salasanoja voidaan tallentaa Windows Credential Managerissa ja ne ovat suljettuja konfigurointien viennistä. Menestyksekäs komento OBS ei vahvista jokaista downstream-efektiä.

## Tulosten tarkistaminen ja täytäntöönpanon historia

Voit tarkistaa tuloksen asianmukaisille toimenpiteille. Sovellus erottaa etukäteen tarkastukset, vahvistetut vaikutukset, epäonnistumiset, peruutus ja vaikutukset, joita ei voida todistaa. Menettelyn olemassaolo ei ole soveltamisvalmiutta. Suhteelliset muutokset, lyhennökset ja tekstiviestit eivät saa menestysvaatimusta. Tarkista toiminnan arkisto ja tarkistushistoria, kun toimenpide epäonnistuu. Määritysvirheitä on korjattava ennen täydellisen makron sulkemista.

## UI automaatio ja klikkaaminen

Lisää UI Automation -vaihe, valitse toimenpide ja tallenna kohde osoittautuneen viivästyksen jälkeen. tallennus yksin ei suoriteta mitään. tarkista valinta ja vaatii ainutlaatuisen matkan. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ja `WaitState`. salasanan kentät, puutteelliset ohjaimet ja epäselektiot hylätään.

UI-automaatio toimii rajoitetussa kumppanuusprosessissa. Se riippuu kohdehakemuksen Windows saatavuus tukea ja lupia. Napsautusrekisteri tallentaa tuetut toimet ja tauot; se ei tallenna yleistä tekstiä. Tarkastellaan ja hyväksytään suunnitelma ennen sen hyväksymistä. UI: n muutokset ulkoisessa sovelluksessa voivat poistaa valintoja. Prosessin eristys rajoittaa onnettomuuksia ja peruuttamista, mutta se ei ole turvakaappi.

## Plugin

Avaa laajennukset tarkistaa paketti ennen tuontia. Pakkauksen rakenne, manifesti ja hashit tarkastetaan. Tuonti alkaa vammaiseksi. Lue ilmoitetut toimenpiteet ja luota selkeästi julkaisijalle ennen sallimista. Plugins toimii käyttäjän lupien kanssa erillisessä prosessissa; tämä prosessi on **ei hiekkapuoli**. Ainoastaan paketti, johon luotat.

Ohjaus tukee aiemman version vientiä, poistoa ja palauttamista. Puuttuvat riippuvuudet estävät kutsumista. Esimerkkinä paketti tarjoaa laskun ja paikallisten toimien. Tuettu kaava on dokumentoitu JSON-kaavan alakohta, ei kaikki mahdolliset JSON-käsittelyn ominaisuudet. Julkista Plugin markkinoita ei ole.

## Mobiilipaneeli

1. Avaa Mobile Panel, valitse paikallinen IP ja valitse nimenomaisesti profiilit näyttämään. Asetuksen takaa-osoite on saavutettavissa vain tietokoneella; valitse sopiva LAN-osoite puhelimelle tai tabletille.
2. Käynnistä paneeli ja lue sen HTTPS-osoite, sertifioi sormenjäljet ja yhden käytön kuuden digitaalisen kumulatiivisen koodin.
3. Samassa luotetussa verkossa olevaan laitteeseen avaa osoite. Vertaa SHA-256 sormenjälki luotetun kanavan kautta ennen paikallisen todistuksen luottamusta. Sovellus ei asenna luotettavaa sertifikaattia automaattisesti.
4. Käytä näytetyn koodin avulla ja testaa yhden vaarattoman levyä. Manuaalinen Windows Firewall -lupaa voi tarvita LAN-yhteyden saavuttamiseksi. Älä altista porttia internetiin.
5. Pysäytä paneeli, kun se on valmis tai peruutetaan maksetut asiakkaat sovelluksen kautta.

Yksityiset palvelin avaimet ovat suojattuja Windows DPAPI; asiakasluottamus tallennetaan hashina. IP:n muuttaminen tai sertifikaatin uusiminen voi edellyttää luottamuksen uusimista. Paneeli näyttää toiminnan ja todentamisen tilat, ei kaikki mahdolliset live-sensorit. Näyttöjärjestelmä ja paikallinen HTTPS-tunnistus eivät korvaa todellista puhelin/tabletin LAN-pääsyä.

## Paikallinen avustaja

Tämä valinnainen kokeiluvälittäjä on haitallinen. Avaa se työkaluista, käytä sitä nimenomaisesti, valitse työtunnus/malli ja lisää vain resursseja, joita haluat ottaa huomioon. Aseta tavoite, luo suunnitelma, tarkistaa ratkaisemattomat pyynnöt ja siirtää tulos normaaliin toimittajaan. Mikään ei suoriteta automaattisesti; säästämistä ja toimintaa on selkeästi. Ei ole automaattista runtime fallbackia.

| Runtimeä | asennuksen |
| --- | --- |
| Ollama | Erillinen paikallinen palvelu ja paikallisesti asennettu malli `127.0.0.1:11434`. Ilmoitetut kauko- ja pilvimallit hylätään. |
| LM Studio | Paikallinen palvelin, jossa on paikallisesti ladattava malli ja JSON-järjestelmän tuki `127.0.0.1:1234`. Älä käytä kaukolähetystä paikalliseen käsittelyyn. |
| llama.cpp | Paikallinen `llama-server` ja yhteensopiva malli JSON-järjestelmän tuella `127.0.0.1:8080`. |
| ONNX Käyttöaika GenAI CPU | Luotettava yhteensopiva paikallinen GenAI tekstimalli; valitse `genai_config.json`. Inference käyttää yhdistettyä kumppanuusprosessia ilman HTTP:tä. |

HTTP-asiakkaat käyttävät vain loopbackia, eivätkä käytä proksia eikä seuraa ohjauksia. MAKRIVO ei voi luotettavasti määrittää, soittaako yhteensopiva paikallinen palvelin sisäisesti etäpalveluun. Viralliset palvelimet eivät ole tässä versiossa määritettävissä. Mallin painoja, Python ja paikallisia inferenssin palvelimia ei ladata tai asenneta automaattisesti.

ONNX paketit vaativat `tokenizer.json` ja voimassa oleva chat-mallia. Tuetut mallit `tokenizer_config.json` ovat sarja `chat_template`, sanakirja sarjan `default` tai epäselvä nimetty / oletusarvoinen malli luettelo. Jos malli puuttuu tai nolla, `chat_template.jinja` käytetään palautetta. Tyhjiä, epäselviä, liiallisia tai virheellisesti kirjoitettuja malleja hylätään. Arbitraariset ONNX tiedostot, sisällytetyt/näkökulmat ja vain GPU-paketit eivät voi vaihtaa yhteensopivia GenAI tekstimalleja. Yhdistetty versio GenAI on 0.17.1, CPU. Käytä vain luotettavia malleja; prosessin erottaminen ei ole hiekkasetti.

Avustaja tukee valittuja ohjelmia ilman väitteitä, valittuja asiakirjoja ja verkkosivustoja ja mikrofonia, kun sitä nimenomaisesti pyydetään. Se ei luo viestejä, tapaamisia, tahallisia skriptejä, ilmaisia klikkauksia, latauksia tai kuulokkeita. Tavoite ja resurssien nimet lähetetään valittuun paikalliseen käyttöaikaan; resurssit ja tiedostot eivät ole erikseen toimitettuja. Käyttäjän määrittämä tavoite voi itse sisältää henkilötietoja. Paikallisen liiketoiminnan omat käyttäytymiset ovat edelleen käyttäjän vastuulla.

ja sen ONNX Pyyntö budjetti 90 sekuntia mukaan lukien mallin lataaminen, jopa 3,072 Nopeat tokenit ja 1,024 luodaan tokenejä, myös rajoitettu malli kontekstilla. väärennettyjä/tyyppisiä JSON- ja tuntemattomia resurssi-tunnisteita hylätään. peruuttaa tai lopettaa pyyntö. satunnainen malli suoritusajan testaus määrittää suorituksen, ei hyödyllistä suunnittelun laatua tai aikaa säästää. paikalliset kokeelliset arkistot sisältävät tapahtumien tunnisteita ja aikatauluja, ei kohde tekstiä tai asiakirjan sisältöä; ne eivät automaattisesti ladata.

Myöhemmin pieni Instrut-malliarviointi tuotti nollan voimassa olevia suunnitelmia neljässä saksalaisessa / englanninkielisessä tutkimuksessa. Alkuperäinen johtopäätös toimi, mutta malli epäonnistui tiukkaan suunnitelman. Tämä on negatiivinen laatu, ei suositeltu malli tai osoitettu hyödyllinen avustaja. Katso päivitetyt paikalliset testausraportit tarkkaan mallin tarkistamiseksi ja rajoituksiksi.

## Tuonti, vienti ja synkronointi

Tarkista tuontiennusteet ennen konfiguraation korvaamista. Tuodut toimenpiteet alkavat poistaa ja kannettava synkronointi poistaa laitteistoon liittyviä sidoksia. Aseta laitteet uudelleen ja sallia toimenpiteet tarkoituksellisesti tarkastuksen jälkeen. Tarkista teksti/toiminto ennen viennin jakamista; salasanan sulkeminen ei tee kaikkia käyttäjän luomia toimenpiteitä epäherkkä.

Profiilin synkronointi vaihtaa tiedostoja valitsemassa asiakirjassa, jonka hallinnoi olemassa oleva pilvipalvelu. MAKRIVO ei tarjoa pilven tiliä eikä tallentaa pilven luottamuksellisuutta. Tarkistukset estävät hiljaisia ylikuulemisia. Toiminta sisältö on selkeä valinta. Palauta konfigurointi ennen korvausta tuontia. Oletusarvo on `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; yhteensopivuutta varten säilytetään historiallinen sarjan nimi. Sulje sovellus ennen sen kopiointia / palauttamista. Säilytetyt SDK-todistukset ja OS-suojatut tunnistukset saattavat tarvita toistamisen toisella tietokoneella.

## Ongelmien ratkaiseminen

| oireet | Mitä tarkistaa |
| --- | --- |
| Lyhyt ei tee mitään | Sovellus käynnistyy, sidoksia ei keskeytetä, oikea aktiivinen profiili, tallennettu / sallittua tiiliä, lyhennettyjä konflikteja ja käytettävissä olevia kohde. |
| Tavoiteikkuna ei voi aktivoida | Manuaalisesti aktivoi se. Windows etuoikeusrajoitukset ja etuoikeudelliset erot voivat estää automaation. |
| Erityistä avainta ei löydy | Palvelu / SDK / malli tuki; asettaa omistuksen päällystys, jotta voidaan antaa tuki standardi avain mahdollisuuksien mukaan. |
| Toinen näppäimistö on myös normaali | Odotettu Raw Input. Tuotantoa ei käytetä yksinomaisesti. |
| Ääni tai kirkkaus ei ole saatavilla | Puhdistaa laitteita; tarkistaa valitut identiteetit, loppupisteistot, WMI/DDC/CI tuki ja langat/kaapeli rajoitukset. |
| OBS offline | WebSocket v5 käytettävissä, oikea loppupiste / salasano, saavutettavissa oleva palvelu ja voimassa oleva kohtaus / sisääntunnus. |
| Automaatio epäonnistuu | Companion-hostaja on mukana, ainutlaatuinen voimassa oleva valinta, käytettävissä oleva sallittu kohde, tuettu hallinta ja luvat. Palauta siirtymäaika vasta tavoitteen tarkistamisen jälkeen. |
| Mobiilipaneeli ei ole | LAN sen sijaan, että loopback-osoite, sama verkko, palvelin käynnissä, hyväksytyt profiilit, sertifioitu sormenjälki / luottamus ja manuaalinen palomuuri lupa. |
| Plugin epäonnistuu | Paketin eheys, nimenomainen luottamus / sallittu tila, riippuvuudet, pöytäkirjan yhteensopivuus ja täytäntöönpano. |
| Pyyntö epäonnistuu | Oikea valittu paikallinen käyttöaika, palvelu/malli valmis, tuettu JSON-järjestelmä, luotettava yhteensopiva malli ja pyyntörajat. |
| Layout tai teksti näyttää väärältä | Erota käyttöliittymän kieli näppäimistöltä; tarkista ikkuna, tarkista näytön mittakaava ja raportoi tarkan sivun/kielen/rakennuksen. |

Hyödyllinen vika-raportti sisältää Windows -version, sovellusrakennuksen / komitean, kielen, näytön mittakaavan, laitteistomallin, vähimmäissekvenssin ja merkityksellisen muokkauksen. Poista henkilökohtaiset reitit, toiminnallinen sisältö ja luottamus ennen julkaisemista.

## Rakennus, testaus ja pakkaus

Käytä SDK:tä, joka on kiinnitetty `global.json`. Ruotsin juuresta:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integrointi testit voivat avata eristettyjä ikkunoita ja väliaikaisesti asettaa / vapauttaa tuettuja toimittajan sidoksia. Käynnistä ne ilman muuta ohjaajaa, joka omistaa nämä sidokset. Oletusarvoiset kirkkaus/audio-testi on luettavaa; selkeät laitteistotutkimukset ovat erillisiä. Mock-protokollat, renderoidut näytöt ja satunnaiset tulokset eivät saa olla todellisia laitteiston / mallin laadun hyväksymistä.

Täydellinen julkaisijan kokoelma kumppanit, mobiililaitteet, esimerkki plugin, ONNX toiminnan ja saatavilla riippuvuus ilmoitukset. Pidä alihankkijoita. Menestyksekäs rakenne ei ole allekirjoitettu asentaja tai laillinen vapautus hyväksyntä. Lähteet ja rakenteet voivat kehittyä erikseen; verrataan dokumentaatiota ja sitoudu asennettuun versioon.

## Lisenssi, yksityisyys ja vapautus

Suunniteltu kaupallinen malli on yksi kertainen ostos, jossa ostetun suurimman version pysyvä käyttö; lopullinen hinta, laitteen tuotto ja päivitysehdot on määriteltävä todellisissa myyntiehdoissa. Tuotanto- ja aktivointijärjestelmä ei ole tässä oppaassa. Päivittäiseen valvontaan ei tarvita pilvipalvelua.

Älä levitä omistettuja SDK binareja ilman todistettua lupaa. Säilytä lisenssit ja kolmansien osapuolten ilmoitukset jokaiselle lähetetylle riippuvuudelle, mukaan lukien .NET ja ONNX paketit. Mallin painoilla on omat ehdot. Päivitetyn lisenssin tarkastukset tuovat esiin joitakin uusia riippuvuuksia, eikä se ole näiden lisäysten selvitys. Julkinen tallennus ei itse myönnä yleistä avoimen lähdekoodin lisenssiä.

Todellinen palveluntarjoaja, puhelin/tabletti LAN-toiminta, sekoitettu monitorin/DPI-liikkeet, pitkäaikainen käyttö, täydellinen kielellinen tarkastelu, yksinomainen kuljettajan tuki ja hyödyllinen AI-suunnittelu pysyvät erillisenä hyväksyntätyönä. Älä koskaan anna hyväksyntää yksin läsnäolosta.

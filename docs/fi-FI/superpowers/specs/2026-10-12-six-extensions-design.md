<!-- generated-doc locale=fi-FI source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: kuusi tuote laajennusta ja näppäimistöä

Päivämäärä: 2026-10-12
Tilanne: ehdotettu suunnittelu käyttäjän tarkasteluun; toteutus ei ole aloitettu.

## Tavoitteena
Tarjoa kaikki kuusi pyydettyä laajennusta olemassa olevassa Windows / WPF sovelluksessa, säilyttäen olemassa olevat profiilit, G1/G2 tehtävät, paikallinen toiminta ja tili-vapaa käyttö. Laajenna näppäimistön integrointia Razer ja Logitech, jossa on toistettavissa oleva kapasiteettiarkkitehtuuri muille valmistajille. Valmistajan nimi tai näppäimistö ei ole koskaan todiste siitä, että omistettuja ohjauksia tuetaan.

## Arkkitehtuuri päätös
Laajennetaan Aivana.Ydin, jossa on todennäköiset sopimukset ja validointi, Aivana.Windows ja kanssa Windows ja toimittaja adapteri, ja Aivana.Työpöytä, jossa on liiketoimintaeditorit ja tilanäkymät. Käytä erillisiä kumppaniprojekteja erilliselle plugin-toiminnalle ja paikalliselle mobiilipaneelinpalvelulle. Vältä uudelleenkirjoittamista tai yhden etuoikeutetun prosessin jokaisen laajennuksen hallintaan. olemassa oleva konfiguraatio on ladattava muuttumattomasti; lisätä toiminnan tyypit sen sijaan, että se järjestää niiden pysyviä enum-arvoja.

Vaihtoehtoja otetaan huomioon: monolyyttinen toteutus sisällyttäisi laitteiston, verkoston ja työnkulun täytäntöönpanoon; korvaaminen WPF lisäämään epäsuorasti muuttotyötä. Modulaarisia laajennuksia suositellaan.

## 1. Riippumaton makro pad
Varastoi sidoksia laitteen identiteetin sekä fyysisen skannauskoodin ja laajennetun avaimen lippuja. Lisää nimenomaisia havaintoja ja yksinomaisia tiloja, laitteen löytämistä, avainleikkausta, toistuvaa tukahduttamista, käskyjä, pidettyjä kerroksia ja yhdistää diagnoosia. Säilytä pää/numpad Enter-erityksiä ja testaa Saksan/US-laajennuksia. Linkit kohdistuvat valittuun fyysiseen näppäimistöön; epämääräiset uudelleenyhteydet vaativat uudelleenvalintaa kuin toisen laitteen hallintaa.

Raw Input Yksinomainen laitteen poisto edellyttää todellista näppäimistösuodattimia; ei koskaan liity globaaleihin alhaisiin tasoihin Raw Input ja vaatia luotettavaa laitteen erityistä poistoa. Tarjoa tarkkailua, hätätilanteen vapauttamista, epäonnistumista ja asentajaa/uninstalleria. Älä pidä turvallista työpöydän sisäänkäyntiä. Ohjaimen asennus ja yksinomainen mode-aktivointi ovat nimenomaisesti erillisiä käyttäjän toimia.

Valitse tuettu, asianmukaisesti lisensoitu ja allekirjoitettu taulukko, joka on yhteensopiva kohderyhmän Windows 11 konfiguraatioiden kanssa, tai kehitä / allekirjoita erillinen suodatin. Interception on ehdokas arvioimaan, ei sitoutunut tuotanto riippuvuutta: sen virallinen README kuvailee testausta kautta Windows 10 ja erillinen kaupallinen lisenssi. Ennen kuin tämä portti täyttyy, yksinomainen tilanne on näytettävä käytettävissä; ominaisuutta ei voida julistaa täydelliseksi.

Hyväksyntä: kahdella fyysisella näppäimellä sama avain suorittaa makron ja ei tuota normaalia sovellusta ainoastaan valitulla yksinomaisella laitteella; muut näppäimistöt jäävät vaikuttamatta. Tarkista kytkeminen, onnettomuus, peruuttaminen, keskeytys / poistaminen ja kuljettajan poisto todellisessa laitteistossa.

## 2. Ohjattu työpaikka
Lisää taiteilijan työpaikkaan, kokouksiin, virtaamiseen, tutkimukseen ja projektin vaihtamiseen. Tutustu asennettuihin sovelluksiin, äänilaitteisiin, näytöihin ja OBS kohtauksiin. Käyttäjät valitsevat tavoitteet ja sitoumukset; ennakoida tuloksena olevat vaiheet ja vahvistaa puuttuvat vaatimukset. Tallenna vain tarkistuksen jälkeen, eikä asennuksen aikana ole toimintaa. Tuotettuja profiileja voidaan muokata olemassa olevilla julkaisijoilla.

Hyväksyminen: luo työprofiili ilman manuaalista muokkausta tai laitteen tunnisteita; peruutuslehdet ovat muuttumattomia; puuttuva ohjelmisto on toimiva selitys.

## 3. Todistettu täytäntöönpano
Sisällytä etukäteen tarkastukset, tiivistetyt vaiheen tulokset ja valinnaiset jälkeiset ehdot. Erottele pyydettyjä, täydentyneitä, vahvistettuja, epäonnistuneita, ajallisia, peruuttamattomia ja vahvistamattomia tiloja. Tarkista äänen/mikrofonin kirkkaus, prosessin/ikkunan valmius ja OBS tilanne rajoittuneilla synkronoimilla lukemilla. Lyhytkestoinen/tekstitoimitus ei ole todiste siitä, että kohderyhmä hyväksyi komennon. Ulkoiset haittavaikutukset eivät ole universaalisesti peruuttamattomia; ne tarjoavat korvausta vain, jos ne toteutetaan.

Hyväksyminen: suljetut nimenomaiset tavoitteet eivät koskaan hiljaa putoa; virheellisesti havaittu tilanne ei koskaan raportoi todistettua menestystä; peruutus lopettaa kyselyä; epäonnistuminen tunnistaa askeleen ja toipumista. Toteutumishistoria jättää tekstin kuormitukset ja salaisuudet.

## 4. Plugins ja jakautuvat paketit
Versioituja JSON-ilmoituksia ilmoitetaan toimista, parametrit, valtion tarjoajat, riippuvuudet ja kyvyt. Toteutettavat liitännät erillisessä prosessissa rajoitetun paikallisen IPC: n kautta käsin, määräaikojen ja peruuttamisen kanssa. Kieltäytyy vääristyneistä viesteistä ja yhteensopimattomista versioista; käsitellä onnettomuuksia lopettamatta ohjainta. Kykyilmoitukset ovat paljastusta ja sovittelua, ei OS Sandboxingin vaatimusta.

Tarjoa pakettien vienti/importin ennakkoarviointi, sisältöhashit, matkankuljetussuojaus, kokorajat, versio / riippuvuus tarkastukset, nimenomainen salliminen ja päivitys. Importoitujen suoritettavien plug-inien ei saa suorittaa automaattisesti. Sisältää esimerkkinä, jossa on loppu-to-end install/action/uninstall -virta. Tätä markkinointia varten ei tarvita julkista palvelua.

Hyväksyminen: luo, viedä, asentaa ja suorittaa todellinen esimerkki integroinnista; osoittaa aikarajoituksen, onnettomuuden eristäytymisen, riippuvuuden epäonnistumisen, ylityön hylkäämisen ja poikkeamattoman tuonnin.

## 5. Windows UI automaatio
Lisää elementti ja rajoitettu toimenpiteet invoke, arvo, valinta, painopiste ja odottaa elementti/valtio. Kestää valintaprosessia, automaatio-tunnuksia, ohjaus tyyppiä ja esivanhempia; välttää koordinointi-vain tallennus. Näytä valinta ja testin valinta vain lukemalla. salasanoja ei ole tallennettu. Käytä tukemattomia tarkastuksia ja Windows eheysrajoituksia nimenomaisesti.

Laajenna tallennusta tarkistettavissa olevien UIA-toimintojen kanssa, säilyttäen nykyisen lyhytkestoisen tallennuksen. Kummalliset valintaohjelmat epäonnistuvat mielivaltaisen hallinnan sijaan. Normaali selaimen saatavuus puu ei ole lupaus täydellisestä DOM-automaatiosta.

Hyväksyminen: automaattinen testisovellus, jossa on vakaat valintapaikat sijainti- ja koon muutoksilla; testin epäselvyys, puuttuvat elementit, poisto-ohjaukset, aikarajat ja peruutus.

## 6. Paikallinen puhelin/tabletti
Tarjoa reaktiivinen paneeli opt-in LAN-palvelusta. Käytä nimenomaisesti lyhyen elinikäistä yhden ajan koodia ja lähetä peruutettavat tunnistukset suojattu paikallisesti. Käytä HTTPS:tä dokumentoidun paikallisen todistuksen luottamuksella/pinningilla; ei palautetta, joka lähettää todistuksia valitustekstissä. Epäonnistunut ja rajoitettu käyttöliittymät. Alkuperävalidointi, todistetut komennot, hinnan rajaukset ja rajatut viestit ovat pakollisia.

Näytä sallitut profiilit/toimet, elävä tilanne, eteneminen ja peruuttaminen; käytä vakaita tunnisteita ja samaa suorituskykyä kuin työpöytä- ja fyysiset avaimet. Älä paljasta tahallisia komentoja, suoritettavia reittejä, salaisuuksia tai plugin-asennusta. Tarjoa yhteys, parannus ja peruutusnäytöt. Ei ole pilvipalvelua tai internet-liittymää.

Hyväksyminen: toimii todellisesta puhelimesta / tabletista samassa LAN: ssa; epäpuhtaudet eivät voi luetella tai suorittaa toimia; peruutettuja tokeneja ei ole; kaksinkertaiset/jäljennettyjä komentoja ei ole suoritettu; yhteys ei hiljaa peruuttaa toimia.

## Keyboard valmistaja

| Adapteri / Perhe | Suunniteltu integraatio | Selkeät rajat |
| --- | --- | --- |
| Corsair | Säilytä ajankohtainen iCUE G-tärkeä sisäänkäynti ja RGB integrointi | Malli/ohjelmistokapasiteetin havaitseminen on edelleen tarpeen |
| Logitech G | G-tärkeä SDK-adapteri, jossa on painostus / vapautus ja käytettävissä oleva aika; valaistus erillisen SDK: n kautta, jossa sitä tuetaan | Älä vastaa G-HUB-tunnistusta tukemaan G-key API:iin jokaisessa mallissa |
| Razer | Chroma valaistusadapteri ja ohjattu Synapse -standardin avain kartta, sekä yleinen HID-tulos | Chroma on valaistus API; älä merkitä sitä alkuperäisen makro avain |
| SteelSeries | GameSense valotonta adapteriä ja geneeristä HID:tä | GameSense tapahtuman esittely ei ole yleinen omistettu avain otti API |
| Wooting | Analoginen SDK-adapteri tuettuun analogiseen sisäänkäyntiin ja normaaliin HID-näppäimiin | Kapasiteetit ja SDK:n jakelukäytännöt on tarkistettava tuetun mallin mukaan. |
| Keychron/QMK/VIA | Standard HID-näppäimet ja firmware-karttaohjeet; tunnetut asetukset vain todisteiden kanssa | VIA Konfiguraatio tuki ei ole yleismaailmallinen makro-tärkeä API |
| Muita standardeja HID-näppäimistöjä | Fyysinen avain löytäminen ja karttaaminen; yksinomainen tilanne vain validoidulla suodattimella | Ei valmistettu malli-erityinen Fn, dial tai omistettu avain tuki |

Jokainen laite on havaittu, havaittu, kartattu toimittajan ohjelmiston, alkuperäisen SDK: n, kuljettajan yksinomainen, käytettävissä oleva tai laitteistossa testattu tila. Säilytä kapasiteetin todisteet adapteriversioiden ja testattujen mallien / firmware-tunnisteiden kanssa.

## toimitusmääräys
1. Yhteensopivuus, kapasiteettia koskevat sopimukset ja tarkistettu täytäntöönpano.
2. Ohjattu asetukset käyttämällä todellista löytöä ja olemassa olevia toimenpiteitä.
3. UI Automation ja sen rekisterin/testi kiinnitys.
4. Plugin hosting, paketin virtaus ja esimerkkinä integrointi.
5. Painettu LAN-paneeli ja mobiilivakuutus.
6. Laajennettu toimittajan adapteri ja yksinoikeusnäppäintä, jossa on todellinen laitteiston validointi.

Kaikki kuusi on tarpeen. Kuljettajan allekirjoitus/lisenssi, saatavilla olevat SDK:t tai puuttuva testilaite ovat vapautuvia ovia, ei syitä korvata mock käyttäytymistä ja kutsua työtä täydelliseksi.

## Tarkastus- ja vapautusperusteet
Käynnistä olemassa oleva ydin, Windows ja WPF tarkistaa ja lisätä keskittynyt integrointi testit jokaiselle uudelle rajalle. Tarkista JSON-kierrokset ja perintöprofiilit; testaa tuotavaa täytettävää sisältöä, verkkotunnistusta, peruuttamista, plugin-onnettomuuksia ja laitteen uudelleenyhteyttä. Tarkista työpöytä ja mobiililaitteet. Validoi korkea Windows erikseen. Rakenna julkaisu-artikkelit vain tarkastusten jälkeen; erottaa toteutettu/testi, toteutettu/laitteiston testaamaton ja ulkoisesti estetty toiminnallisuus julkaisu-ilmoituksissa. Älä korvaa asennettuja sovelluksia, asenna ohjaimia, muuttaa todellisia näppäimistöjä tai avata verkkoyhteyttä satunnaisena testinä.

## Todisteita
- Microsoft Raw Input semanttinen: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G kehittäjä SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Sisäänkäynti ja riippuvuus edellytykset: https://github.com/oblitum/Interception
- Nykyinen varasto valmistaja: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Arviointi päätökset
Käyttäjä pyydetään hyväksymään tämän kirjallisen suunnittelun ennen toteutussuunnitelmaa. Tärkein ulkoinen riippuvuus on tuotantokelpoinen yksinomainen sisäänkäyntiohjain. Suositeltu ulottuvuus säilyttää tämän ovena, eikä hiljaa vähentää sitä Raw Input havaintoon.

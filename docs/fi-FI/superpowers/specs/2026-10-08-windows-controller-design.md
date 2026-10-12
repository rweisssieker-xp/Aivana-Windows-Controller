<!-- generated-doc locale=fi-FI source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Suunnitelma

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Pysy: 8. Lokakuussa 2026. Tilanne: ennen täytäntöönpanoa.

## Tavoite ja sovittu laajuus

Itsenäinen Windows-sovellus tekee tavallisesta tietokoneesta vapaasti määritettävän toimintasuunnitelman, joka on samanlainen kuin Stream Deck. Käyttäjä on vahvistanut kaikki ehdotetut toimenpiteet, makroja, laitteiden tunnistaminen, profiilit ja hyvä GUI. Liitettyjä Corsair- ja muita painikkeita tunnistetaan; lisätoimintoja tarjotaan vain, jos niitä todella tuetaan.

Oletukset: saksalainen käyttöliittymä, paikallinen käyttö ilman tiliä, Windows 11 x64 ensisijaisena tavoitteena, ei ohjaimen asennus. Näppäimistö on käytettävissä normaaliin kirjoitukseen. Globaalit painotukset käyttävät vakiokäyttöisiä painikkeita muuttimien kanssa. Yksittäiset kirjaimelliset painikkeet eivät ole vakiintuneesti globaaleja.

## Tekninen päätös

Native C#/NET-10-App ja WPF sekä MVVM. Stabiili .NET-10-SDK on käytettävissä työpisteessä. WPF mahdollistaa yksilöllisen työpöytäliittymän ja suoran yhdentymisen Windows. Vaihtoehtoja ovat erillinen Windows-palvelun verkkosivusto tai työpöydän sovellus; molemmat tuovat lisäaikaa ja viestintäkompleksiä tähän Windows-hankkeeseen. Kotimainen vaihtoehto on valittu perusta.

Jakelu: Desktop-hanke näkymille ja traille, Core-hanke konfiguroinnille ja toteutustunnuksille, Windows hanke hotkeysille, sisäänkäynti simulointi ja laitteet, testaushanke alustan riippumattomille säännöille. Valmistajan sovittimet ovat kiinteän kapasiteetin käyttöliittymän mukaisia ja riippumattomia standarditoiminnasta.

## Ympäristö

Pimeä muotoilu, jossa on rauhallisia anthrazit-värisiä alueita, hyvin luettavaa kirjoitusta ja turkkilaista akkua. Linkit Navigointi Dashboard, profiilit, laitteet ja asetukset. Aktiivinen profiili, etsi ja vaihdin lopettaa kaikki hotkeys. pinta pysyy käytettävissä 100–200 prosentilla Windows.

Dashboard sisältää mukautettavan kaapelin. Jokainen kaakko näyttää symboli, nimi, hotkey ja suorituskyky. Klikkaa sama toiminta kuin Hotkey. Oikealla avautuu muokkaus, jossa on toiminta, parametrit, hotkey tallennus, symboli/väri, aktivointi ja testauspainiketta. Uusi, kopioida, poistaa ja muokata ovat käytettävissä. Painiketta koskeva näkymä näyttää säännöllisesti saksalaisen asennuksen asennuksia; se ei väitä tunnistetun laitteen tarkkaa geometriaa.

Profileja voidaan luoda, kopioida, nimittää ja aktivoida manuaalisesti. Vaihtoehtoisesti profiiliin luovutetaan prosessin nimi. Asianmukainen etuohjelma aktivoi sen profiilin, muuten sovelletaan globaalia standardiprofiilia. Useiden asianmukaisten profiilien kohdalla on selkeästi näkyvä järjestys. Toimittaja selittää Hotkey-konflikteja suoraan sijainnissa.

## Hotkeys ja sisäänkäynti

Windows RegisterHotKey käsittelee normaaleja globaaleja yhdistelmiä tukahduttamalla painikkeita. Vain aktiivinen profiili on rekisteröity. Varattuja tai jo tallennettuja yhdistelmiä näytetään virheinä; sovellus ei ilmoita niiden onnistuneeksi aktivoituina. Toimitus ei ratkaise toimia.

Ennen simuloituja sisäänkäyntejä vapautetaan tai niiden fyysinen vapautuminen on odotettavissa. Unicode-tekstit ja painikkeiden yhdistelmät lähetetään SendInputin avulla. Jokainen itse painettu painiketta poistetaan uudelleen myös romahduksen yhteydessä. Toimet toimivat UI: n ulkopuolella; suoritus ja profiilin muuttaminen eivät saa estää käyttöliittymää.

## Toimintaa ja makroja

Toimintatyökalut: käynnistää ohjelma argumenttien ja työlehden kanssa, avaa tiedosto/levy yli Windows, avaa HTTP/HTTPS-sivusto, kirjoita Unicode-tekstti, lähettää painikkeiden yhdistelmä, media Play/Pause/Next/Premiere/Stop, äänenvoima korkeampi/pienempi/puhu sekä profiilin vaihtaminen. Parametrit validoidaan ennen tallennusta ja uudelleen ennen suoritusta. Ohjelmat käynnistetään välittömästi; argumentteja ei tulkitse implisiittisesti shell-komentoina.

Makroissa on järjestetty luettelo mainituista toimista ja selkeät askeleet. Vaiheet voidaan lisätä, muokata ja siirtää. Toistaminen on lopulta konfiguroitavaa (1–100); loputtomat kappaleet eivät kuulu ensimmäiseen versioon. Käytössä oleva makro voidaan lopettaa pinnalla tai omalla purkamiskellolla. Toistuvia aiheuttamien samojen käynnissä olevien ei käynnistä rinnakkaista juoksua. Eri makroja, joilla on sisäänsimulointi, sarjataan, jotta niiden painikkeet eivät sekoita. Vaiheen virheessä makro pysähtyy ja osoittaa vaikuttavan vaiheen.

## Laitteet ja valmistajat

Laitteen luettelo Windows Raw Input ja lisätiedot laitteesta: Näytön nimi, laitteen reitti, valmistajan/tuotteen tunnistus, kun se on saatavilla, yhteyden tilanne ja tunnistettu taito. Aikataulu päivitetään uudelleen aloittamatta. Fyysinen laite voi olla useita HID-tiedostoja; ne on ryhmitelty kuormittavien tunnisteiden mukaan, muutoin näkyvissä erillisissä kirjoissa.

Normaali hotkeys soveltuu painopisteiden väliselle tasolle. Laitteen näkymä tarjoaa tietoja ja taitoja; erillinen painiketta, jossa on yksinomaan suljetut painikkeet, ei ole osa tätä sovittua tilaa.

Corsair tunnistus toimii myös ilman iCUE. Valinnainen iCUE SDK-adapteri tarjoaa yhteyden tilan ja tuettua valaistusta, kun SDK, iCUE ja yhteensopiva laite ovat käytettävissä. UI erottaa tunnistettu, SDK käytettävissä, yhdistetty ja tosiasiallisesti hallittavissa. SDK: n pääasiassa on LED-ohjaus; omistettuja G-painikkeita tuetaan vain tavanomaisten hotkeyn tavoin, kun Windows saa vastaavat talletukset. Ei ole mitään pakollista lupausta kaikissa paineissa.

Muut valmistajat saavat ensin yleisen tunnistamisen ja vakiotoiminnan. Adapter-liitäntä mahdollistaa myöhempien integrointien; tukemattomat taidot näkyvät rehellisesti. Ei ole olemassa olevien mallien laitteistotestiä ei ilmoiteta pysyviksi.

## Säilytys ja jokapäiväisyys

Versioitu JSON-konfiguraatio paikallisessa sovellustietokannassa. Ydinvarastointi viimeisen voimassa olevan varmuuskopion kanssa; virheelliset tiedostot eivät ole hiljaa. Tuonti tarkistaa muotoa, versiota, tunnisteita ja parametreja ja näyttää ennakoinnin ennen hankintaa. Tuotetut toimet voidaan toteuttaa vasta nimenomaisen aktivoinnin jälkeen. Vienti sisältää profiileja ja tallenteita.

Tray-valikko: Avaa, profiilin valinta, keskeyttää hotkeys, makroja, lopettaa. ikkunoita suljetaan vakiokohdassa ja selitetään ensimmäistä kertaa. Auto käynnistyy valinnaisesti ja standardiin. Vain yksi toimija hallinnoi hotkeys. Lopettaa hotkeys, rikkoo tuloksia ja vapauttaa resursseja.

Paikallinen toimintatapa, jossa on rajallinen menestys, virhe ja romahdus. Ei kaikkien kirjoitusten pöytäkirja. Tekstejä ei kirjoiteta virheviesteissä. Ei pilvipalveluita eikä telemetriaa.

## Virheiden käsittely ja rajat

Puuttuvat ohjelmat, virheelliset URL-osoitteet, varastetut hotkeys, vaurioituneet profiilit ja saavutettavissa oleva valmistaja SDK saavat konkreettisia Saksan viestejä. Adapterin virhe ei vaikuta vakiotoimiin. Sisällöt korkeammissa etuoikeudellisissa sovelluksissa voivat epäonnistua Windows -oikeusrajoilla; virheet selitetään, sovellus ei vaadi hallintoviranomaisia.

## Tutkimus ja vähentäminen

Automaattiset testit profiilin prioriteetin, konfliktin tunnistamisen, konfiguraation tuonnin, atomien tallennuksen, makrojärjestyksen, toistamisen, rikkomisen ja virheen esittämisen osalta. Windows Smoke-testit todellisille hotkeysille, tekstin ja painikkeiden yhdistelmille, mediatoiminnalle, laitteen tallennukselle, jäljitykselle ja puhtaalle loppuun. Ei testit, jotka satunnaisesti käynnistää mitään ohjelmia tai sivustoja.

GUI-testi: asennus ja muokkaus, suorittamaton tallennus, samankaltainen klikkaus/hotkey, makrohäiriö, konfliktinviesti, profiilinmuutos, laitteen näkymä, tuonti/eksportti ja asteikko. Valmistajan erityinen laitteistotesti on dokumentoitu erikseen olemassa olevalla laitteella.

Toimitus: Lähteen koodi, toistettavissa oleva rakennus / käynnistysohje, saksalainen käyttöohje ja paikallinen Windows-x64-Publish-Artefakt. Menestys tarkoittaa kaikkien yleisten toimintojen suoritettavaa sovellusta; Corsair -ominaisuudet on merkitty käytettävissä vain todistetun SDK/laitteen tuella.

## Tekniset lähteet

- Microsoft Keyboard Input / RekisteriHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Sisäänpääsy: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Virallinen Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Seuraava askel

Tämän hankkeen tarkastelun jälkeen: konkreettinen täytäntöönpanosuunnitelma ja toteutuksen valinta. Tyhjässä työjärjestyksessä ei ole vielä Git-levyä; tätä asiakirjaa ei ole vielä tehty.

<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-12-six-extensions.md)

# Kuusi laajennusta: täytäntöönpano ja tarkastelu

Pysy: 12. Lokakuussa 2026, Teollisuus `codex/six-extensions`. Työtila, ei tuotantovapautta.

## Palvelu

Komandaatti avaa kirjoitukset **Työpaikan järjestäminen**, **Painikkeiden valmistajat ja SDK**, **Mobiilipaneeli**, **Laajennukset**, **UI automaatio**, **UI:n lopputulos** ja **Täytäntöönpano**.

- Työpaikan avustaja luo profiileja työpaikalle, kokoukselle, suoratoistolle, tutkimukselle ja hankkeelle. Ohjelmat rekisteröinti / käynnistys tai tiedoston valinta, URL-osoitteet, ulos / näytön ja OBS kohtaukset näkyvät ennen tallennusta. Laitos ei tee toimia.
- Vaiheeditorissa **tulokset tarkistetaan** tarkistuksen ja jälkivalvonnan avulla. Kulku ja mobiilipaneeli erottaa todellisen vahvistuksen tarkistamattomasta vaikutuksesta. Suhteelliset muutokset, painikkeiden lyhennökset ja tekstin kirjoittaminen eivät anna vahvistusta. Menettelyn todistus ei osoita ammatillista valmiutta soveltaa.
- UI-automaatio käyttää erillistä, ajallisesti rajallista työntekijää. Valitse hiiren sijainti, valinta, Invoke/Select/Toggle/Focus/SetValue/WaitState; useita kohtia, poistettuja tavoitteita ja salasanoja hylätään. Rekisteröinti sisältää tukea klikkauksia ja taukoja; tekstin tallenteita ei lyödä. Ennen vastaanottoa tarkistetaan.
- Laajennukset: ZIP-testi, hashit, kaava, deaktivoitu tuonti, selkeä luottamus ennen aktivointia, eristetty prosessi rajoitettu IPC, vienti, deaktivaatio, poistaminen, aikaisempi versio palauttaa / hylätä. Prosessi eristyminen ei ole hiekkalaatikko: julkaistu plugins toimii käyttäjäoikeuksia. Esimerkipaketti `makrivo.example.zip` sisältää laskentatoimen ja tilastotoiminnan. Tuetut järjestelmät muodostavat dokumentoidun yksinkertaisen JSON-järjestelmän osakehyksen lähdekoodissa; ei julkista markkinoita.
- Matkapuhelinpaneeli: valitse paikallinen IP ja jaettu profiili, avaa HTTPS-osoite, tarkistaa sertifikaatti käyttäen SHA-256-Fingerprints ja luottaa tietoisesti, sitten kirjoittaa yksittäinen kuuden merkkin koodi. Yksityiset avaimet ovat suojattuja Windows-DPAPI; vain hakkerit asiakkaan pääsyä tallennetaan. Sertifikaatit muuttuvat uusimisen tai IP-vaihdon jälkeen. Käyttäjän on julkaistava Windows tulitauone tarvittaessa. Ei automaattista sertifikaatin asennusta, portin laajentumista tai Firewall-muutosta. Asiakkaan pääsy voidaan peruuttaa. Paneeli näyttää toiminnon tilan ja testausprosessin; se ei ole vielä universaali live-sensori.

## Näppäimet

| Valmistaja | täytäntöönpano | rajoitukset |
|---|---|---|
| Corsair | olemassa olevat iCUEG-painikkeet ja RGB | paikallinen iCUE yhteys ja sopiva malli |
| Logitech G | G-Key SDK ja erillinen LED SDK | alkuperäinen paikallinen x64-DLL nimenomaisesti; G-Key SDK ei tarjoa yksilöllistä identiteettiä useita painikkeita |
| Razer | Chroma-RGB ja standardin tunnistus | omistettuja painikkeita ennen Synapse |
| SteelSeries | GameSenseRGB ja vakiokäyttö | omistettuja painikkeita ennen GG: ssä |
| Wooting | laitteeseen liittyvät analogiset arvot, paine- ja laskureitit | alkuperäinen paikallinen x64-SDK-DLL; normaali HID-sisältö pysyy aktiivisena |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Lisää | Fyysiset vakiovarusteet laitteen ja skannauskoodin mukaan | Fn ja omistettuja ohjauselementtejä mallin mukaan |

Ei uusia omistettuja SDK-DLL:itä toimitetaan. Uudet valmistajat eivät ole vielä varustettu ilman sopivaa fyysistä laitetta.

**Ensimmäinen makropadi ei ole valmis: ** Raw Input havaitsee laitteita erikseen, mutta ei tukahduta niiden normaalia Windows -käyttöä. Tuotantovapaus edellyttää lisensoitua, allekirjoitettua Windows 11/Secure aluksella tarkastettua suodattimia sekä hätävapauden, romahduksen, keskeytyksen ja uudelleenliittymisen testausta. Ei ole asennettu ohjainta. Sovellus määrittää tämän rajan nimenomaisesti.

## Testit

- Release-Build: onnistunut, nolla varoituksia / virheitä.
- Core-konsoli: kaikki testiryhmät ovat onnistuneita, mukaan lukien valmistajan painikkeiden konfliktit, plug-in-pohjaiset virheet ja todellinen suoritusvirhe testien aikana.
- Laajennus: todellinen UIA-työntekijä, jossa on WPF testausohjelma; yksilöllinen invoke, monimutkaisuus, deaktivoidut tavoitteet, salasanan sulkeminen. Echter nimetty putki plugin prosessi, protokollin rajat, analoginen histeresi ja Logitech ABI-koodaus onnistunut.
- Todellinen HTTPS-hostaus: tulostettu sertifikaatti, yhteensopimattoman pääsyn, väärän / yhden kerran koodin, alkuperäsuojauksen, selaimen tiedostojen, hashin ja suojatun avaimen onnistuminen. Testissä havaittu skannausvirhe, jossa on efemaattisia avaimia, korjattiin.
- Windows-Suita, jossa on selkeästi hylätty etusijalle menestystesti: onnistunut; Audio/OBS/iCUE/Raw Input ja näytön tilaa tarkastettu. Edellisessä juoksussa täydellinen suite epäonnistui kieltäytyessä keskittymisvaihdosta eristettyyn testiikkunoon. Seuraavassa juoksussa AI-prototyyppiin oli myös täydellinen suite, mukaan lukien keskittyminen ja liitetty iCUE-Toinen toistuva kierros epäonnistui uudelleen keskittymisen yhteydessä; kontekstua koskeva testi ei ole vakaa. Tämä testi ei heikentynyt. lyhyessä ajassa epäonnistuneet DDC-lukuja toistetaan rajoitetusti; kirkkautta ei muutettu.
- WPF-Smoke: kaikki aiemmat ja uudet vuoropuhelut, konfiguraation vaihtelu, muokkaus ja CRUD menestyksekkäästi; Screenshots rakennusjärjestelmässä `smoke`.
- Käyttö: Playwright ja Edge, 390×844 ja 1024×768, Yhteysvirta/UUID/yhden lähetyksen/ei horisontaalinen lähdön onnistunut. API-Mock; todellinen HTTPS testi erikseen. `artifacts/mobile-browser`. selaimen plugin ei ollut saatavilla, joten säännöllinen Playwright Fallback.
- Riippumaton koodin tarkastelu; myöhässä matkapuhelin käskyjä, puuttuvat terminaaliset tarkastus tulokset, prosessitutkimus, plug-in rollback ja uudelleen käynnistämistä RGB sydänliikennettä korjattu.

Avoin: yksinomainen ohjaimen integrointi, fyysinen valmistaja/puhelin/tabletti-LAN-testi, Windows asteikko 150/200 prosentti ja täydellinen käännös kaikkien uusien vuoropuhelun tekstien. Uudet vuoropuhelun tekstit ovat pääasiassa englantia; olemassa olevat kielikatalogit säilytetään. Tästä syystä kokonainen vaatimus ** ei ole kokonaan poistettu **.

## Paketin tuottaminen

`./tools/publish-complete.ps1 -SelfContained` luo uuden tiedoston alla `artifacts/release-*`, mukaan lukien työpöytä, UIA-työntekijät, HTTPS-hostaja, selaimen tiedostoja, tuodaan esimerkki plugin ja Runtime lisenssit. Asennettu sovellus ei ole lueteltu. Ilman `-SelfContained` paketti tarvitsee asianmukaista .NET-10-Desktop-/ASP.NET-Runtimes.

Paketti on paikallinen testauspaikka, ei allekirjoitettu myyntiasentaja. Käyttöehdot ja myyntioikeudet ovat erillisiä julkaisuja; katso `docs/licensing` -dokumentti.

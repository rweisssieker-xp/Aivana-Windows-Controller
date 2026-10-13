<!-- generated-doc locale=fi-FI source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../MAINTENANCE.md)

# Kaikkien dokumenttikielien säilyttäminen

Dokumentaatio kattaa kaikki 27 käyttöliittymäkieltä. Tuotetut kansiot käyttävät aluekoodeja; norja vastaa koodia `nb-NO` ja latinalaisin kirjaimin kirjoitettu serbia koodia `sr-Latn`. Jokainen kansio sisältää hakemiston, oppaan, yksityiskohtaisen toimintoviitteen, ominaisuusluettelon, tarkistusmuistiinpanot, tutkimuksen CSV-tiedostoineen, historialliset suunnitelmat ja lisenssianalyysin.

## Totuuden lähde

Muokkaa juuren tiedostoa `README.md`, tiedostoja `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, tätä sivua tai alkuperäisiä tiedostoja hakemistoissa `docs/research`, `docs/testing`, `docs/superpowers` ja `docs/licensing`. Älä kirjoita historiallista raporttia uudelleen siten, että se näyttäisi validoivan myöhemmän version. Pidä toteutus, simulointi, todellisen laitteen hyväksyntä ja kaupallinen hyväksyntä erillisinä.

## Luo ja validoi

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Valinnainen verkkogeneraattori lähettää dokumentaatiotekstin Googlen julkiseen käännösrajapintaan nykyisten lokalisointityökalujen tapaan. Se ei lue käyttäjäkokoonpanoja, salaisuuksia tai mallitiedostoja. Suorita se vain dokumentaatiolle, joka soveltuu kyseiseen palveluun. Koodilohkot, rivinsisäinen koodi, URL-osoitteet, taulukkorakenne ja tekniset tunnisteet suojataan. Käännösvälimuistit ja jäädytetty lähdetilannekuva tallennetaan versionhallinnan ohittamaan hakemistoon `artifacts/documentation-translation`.

Kun verkkopalvelun käyttöä rajoitetaan pyyntötiheyden vuoksi, `tools/translate_documentation_local.py` täyttää saman välimuistin erikseen annetulla paikallisella M2M100 CTranslate2 -mallilla. Tilannekuva 2026-10-12 käyttää MIT-lisensoitua mallia facebook/m2m100_418M, muunnettuna malliksi JustFrederik/m2m_100_418m_ct2_int8 revisiossa `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 ja `SentencePiece` 0.2.2 ovat vain koontityökaluja. MAKRIVO ei sisällä mallipainoja tai käännösriippuvuuksia. Täytettyäsi välimuistin suorita generaattori valitsimella `--cache-only` ilman verkkopyyntöjä tapahtuvaa generointia varten. Paikallinen apuohjelma ei lataa eikä suorita etämallikoodia.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Ilman valitsinta `--refresh-sources` ajo jatkaa jäädytetystä tilannekuvasta. Käytä päivitysvalitsinta alkuperäisten tiedostojen muokkauksen jälkeen. Välimuistissa olevat segmentit käytetään uudelleen. Osittaisessa ajossa kielikoodit voi valita valitsimella `--locales`; viimeistele kaikki kielet ennen julkaisua. Keskeytyneet ajot eivät korvaa epäonnistunutta käännöstä huomaamatta kääntämättömällä tekstillä.

Manifesti tallentaa jokaisen lähde- ja tuotetun tiedoston normalisoidun UTF-8 tekstin SHA-256 tiivisteen: LF-rivinvaihdot ilman tavujärjestysmerkkiä. Verkkoyhteydetön tarkistin varmistaa täydellisen kielikohtaisen kattavuuden, tiivisteet, paikalliset linkit, koodiesimerkit, taulukko- ja CSV-rakenteen, lähde-URL-osoitteet sekä korvaamatta jääneet paikkamerkit. Se hylkää vanhentuneet lähteet. Rakennetarkistuksen läpäisy ei ole äidinkielinen tarkistus eikä oikeudellinen hyväksyntä. Tämä rajaus näkyy tuotetuilla sivuilla.

## Julkaisut ja paketit

Julkaise dokumentaatio, generaattori ja tarkistin yhdessä. Säilytä alkuperäiset kolmansien osapuolten lisenssiehdot; käännetty lisenssianalyysi on tiedoksi. Koontiartefaktit, käännösvälimuisti ja paikalliset orkestrointitiedostot eivät kuulu Git-versionhallintaan. Lähdedokumentaation julkaisu ei itsessään julkaise Windows-asennusohjelmaa.

Käyttäjälle näkyvät sovelluskäännökset ovat erillisiä JSON-katalogeja hakemistossa `src/Aivana.Core/Localization`. Dokumentaation generointi ei muuta niitä. Sovelluskoonnin ja sen dokumentaation tulisi vastata samaa versioitua tilannekuvaa; vasta kuvattu kokeellinen suoritusympäristötuki voi edeltää asennettua pakettia.

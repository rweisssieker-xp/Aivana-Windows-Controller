<!-- generated-doc locale=fi-FI source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../MAINTENANCE.md)

# Kaikkien dokumenttikielien säilyttäminen

Dokumenttirekisteri seuraa kaikkia 27 UI kielillä. Tuotetut tiedostot käyttävät alueellisia koodeja; Norjan kartat `nb-NO` ja Serbian Latin `sr-Latn`. Jokainen kansio sisältää indeksin, käsikirjan, yksityiskohtaisen toiminnan viittauksen, ominaisuuskertomuksen, tarkastuskertomusten, tutkimuksen mukaan lukien sen CSV, historialliset suunnitelmat ja lisenssin analysointi.

## Totuuden lähde

Edit root `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Tämä sivu tai alkuperäiset tiedostot `docs/research`, `docs/testing`, `docs/superpowers` ja `docs/licensing`. Älä kirjoita uudelleen historiallista raporttia, mikä tarkoittaa sitä, että se vahvistaa myöhemmän version. Pidä täytäntöönpano, simulaatio, todellinen laite ja kaupallinen hyväksyntä erillään.

## Luo ja validoi

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Valinnainen online-generator lähettää asiakirjoja Googlen julkiselle käännöspisteelle olemassa olevan sijaintityökalun mukaisesti. Se ei lue käyttäjän konfiguraatioita, salaisuuksia tai malli tiedostoja. Käytä sitä vain kyseiselle palvelulle sopivalla asiakirjalla. Koodit, inline-koodi, URL-osoitteet, taulukon rakenne ja tekniset tunnisteet ovat suojattuja. Käännöskassa ja jäädytetyssä lähteessä on tallennettu sivuutettu `artifacts/documentation-translation`.

Kun online-palveluntarjoaja on rajallinen, `tools/translate_documentation_local.py` täyttää samat kaapit käyttämällä nimenomaisesti toimitettua paikallista M2M100 CTranslate2 Mallia ja 2026-10-12 Snapshot käyttää MIT-lisensoitua facebook/m2m100_418M Muotoilu, muunnettu JustFrederik/m2m_100_418m_ct2_int8 Uudelleentarkastelussa `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 ja `SentencePiece` 0.2.2 ei ole mallin painoja tai käännöksen riippuvuuksia lähetetään MAKRIVO. Käynnistä generaattori `--cache-only` täyttämisen jälkeen kaasin suorittaa ilman verkko pyyntöjä. paikallinen avustaja ei lataa tai suorittaa kauko-mallin koodia.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Ilman `--refresh-sources`, kävely palauttaa jäädytetyn kuvan. Käytä uudelleenjärjestelyä alkuperäisten muokkausten jälkeen. Piilotettuja segmenttejä käytetään uudelleen. Osittainen toiminto voi valita paikalliset koodit käyttäen `--locales`; lopettaa kaikki paikalliset ennen julkaisemista. Keskeytetyt juoksut eivät hiljaa korvaa käännettyä proosia epäonnistuneelle käännökselle.

Manifesti tallentaa jokaisen lähteen ja tuottaman tiedoston SHA-256 sormenjäljet normalisoidun UTF-8 tekstin: LF-linjan päättymät ja ei byte-tilausmerkki. Offline-sekoitin tarkistaa täydellisen kielen kattavuuden, sormenjälkien, paikallisten linkkien, koodin esimerkkejä, taulukon/CSV-rakenteen, lähdekoodin URL-osoitteiden ja korvaamattoman paikanhaltijan. Se hylkää teräviä lähteitä. Rakenteellinen tarkastus ei ole alkuperäisen puhujan tarkastus tai laillinen hyväksyntä. Tuotetut sivut tuovat tämän pätevyyden näkyvästi.

## Julkaisut ja paketit

Julkaise dokumentti, generaattori ja tarkistaja yhdessä. Säilytä alkuperäiset kolmannen osapuolen lisenssit; käännetty lisenssin analyysi on informatiivinen. Rakenna esineitä, käännös- ja paikallinen orkesteritiedostoja ei kuulu Git. Alkuperäinen dokumentti ei itse julkaise Windows asentajaa.

Käyttäjäsuuntaiset sovellukset ovat erillisiä JSON-kataloita `src/Aivana.Core/Localization`. Dokumentti ei muokata näitä luetteloita. Sovelluksen rakentamisen ja sen dokumentin pitäisi jakaa versioitu snapshot; äskettäin kuvattu kokeiluaika tukee asennettuja paketteja.

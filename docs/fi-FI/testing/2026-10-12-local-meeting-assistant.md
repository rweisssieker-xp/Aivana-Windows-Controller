<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-12-local-meeting-assistant.md)

# Paikallinen AI-keskustelu - prototyyppi

Pysy: 12. Lokakuussa 2026. ForgeMind -hypoteesi ”Outcome Agent”, rajallinen kokouskulku. ei todistettu kilpailuetu eikä mitattu ajan säästö ilman todellisia vertailuyrityksiä.

## Käyttö

1. Komandaali → **KI-keskusteluavustaja** Kokeilu on vakiomuotoinen ja tapahtuu konfiguraation tuonnissa. Portaalin vienti on peruutettu.
2. Aktivoi paikalliset avustajat. Ollama Pitää erikseen `127.0.0.1:11434` Käynnistä; asianmukainen paikallinen malli on jo asennettava. **Paikalliset mallit** Valitse malli. MAKRIVO ei asenna malleja/palveluja, ei ota yhteyttä OpenAI/Cloud API:hen eikä käytä API- avaimia. Ollama-Palvelut ovat käyttäjän kanssa; ilmoitetut kauko- ja pilvimallit hylätään.
3. Kirjoita ja lisätä ohjelmia / asiakirjoja / verkkosivustoja nimenomaisesti. Tarkoitus, resurssi-ID, nimi ja tyyppi lähetetään; Tarkoituslevyä ja valittujen tiedostojen sisältöä ei lähetetä lisätietoja; Tarkoituksen kirjaimellisesti lähetetään. Verkkosivujen nimet sisältävät vain kaavion/hosta, koko linkki pysyy paikallisena.
4. **KI-suunnittelun luominen**. Maksimaalisesti 90 Sekuntia, 1024 Päätökkeet, rajoitettu vastaus. Kohde-/malli/resurssi muutokset hylkäävät ennakoinnin; Poistaminen/ sulkeminen päättyy. Malli voi ehdottaa vain valittuja tunnisteita. Tuntematon tunniste, vapaat komennot, lisät JSON-kentät ja suoritettavat asiakirjat hylätään.
5. Tarkista etukäteen ja tarkista avoimet pisteet → **Editori**. Ensinnäkin tulos on säilyttämätön suunnitelma. Säilytä vain olemassa olevaan toimittajaan ja suorittaa se nimenomaisesti tarvittaessa. Normaali makrovalidointi, ennakko- / tarkastus, tauko ja keskeytys pysyvät vastuullisina.

Tuki: Ohjelmat käynnistetään ilman argumentteja, avaa nimenomaisesti valitut paikalliset PDF/DOCX/XLSX/PPTX/MD, avaa HTTP(S) sivustot, sulje mikrofonin selkeästi toiveeseen. Ei kuulokkeita, ei viestejä, päivityksiä, latauksia, skriptejä, ilmaisia klikkauksia tai tekstiviestejä. Ei kuviteltavia toiveita ilmestyy avoimina pisteinä, jos malli tunnistaa ne oikein; käyttäjän on tarkasteltava ammatillista tavoitteiden saavuttamista. Asiakirjojen avaaminen ei ole automaattisesti tarkistettu.

## Vertaileva kokemus

**Manuaalinen asennus mitata · Aloitus / loppu** mitata täydellinen manuaalinen vertailukelpoisuus. Avustaja-annokset mittaavat ajan avustajan avaamisesta ensimmäiseen tallennukseen; `generated` sisältää kuitenkin vain suunnitteluaikaa. Kumpikaan ei voi verrata aikaa säästämiseksi.

Paikalliset tapahtumat ovat määritysjärjestyksessä `Experiments/meeting-experiment.jsonl`: ajankäyttö, sattumanvarainen kokeilu/tunnistus, tapahtuma, kesto, korjaus kyllä/ei. Ei kohdetekstejä, resurssien nimiä, reittejä, mallin vastauksia tai asiakirjojen sisältöä. Tiedosto kiertää 2 MiB, aiempi tiedosto säilyy. Mittausvirheitä ei saa keskeyttää makroja ja konfiguraatiota.

Sisältää: manuaalinen peruslinja, mallin tuottaminen/virhe/häiriö, epätoivoiset ennakointimuutokset/ sulkeminen, ensimmäisen kerran tallennettu ja korjattu muokkaus sekä tallennettujen kaapeleiden tosiasiallinen lopputila. `run_completed` tarkoittaa vain sitä, että kaapeli on valmis; vahvistetut tavoitteet ovat erikseen testitilanteessa. Saman makron toistaminen ei ole uusi riippumaton järjestely. Alkuperäinen kokeilu voidaan jatkaa uudelleenkäynnistyksen jälkeen, koska kokeilun tunnus jää tallennettuun kaapeliin. Duploidut ketjut jakavat tämän alkuperän; arvioinnissa ryhmittyy kokeilun ID: n mukaan.

Ensimmäinen kokeilu: 20 vertaileva kokouslaitos tai kaksi viikkoa vapaaehtoisten testin käyttäjien kanssa; ensimmäinen sisäinen käyttö. Vertaa manuaalinen asennusaika täydellisiin asennusaikoihin; lisäksi korjaus/korjaustaso, juoksun virhe ja todelliset testitulokset. Puuttuessa parannuksista tai suoja-alueiden loukkauksista sulje pois. Käyttöä tai maksuvalmiutta ei ole osoitettu tallennuksessa. Ei automaattista telemetriaa tai hinnoittelua.

## Testiä

- Core: rajoitukset, tyypit, tuntematon / kaksinkertaistettu ID, skripti sulkeminen, pitkät linkit, valinnainen alt-konfiguratio, tuonti / vienti-opt-in, tietojen säästämisen mittaus sekä yksittäinen hylkääminen ja oikea siirto.
- Kuljetus väärän mallin kanssa: Ollama JSON-järjestelmä, Loopback-tavoite, ei tiedostoja promptissa, pilvinen nimi ja etäisyys hylätään, vahingoittunut/pituinen vastaus hylätään.
- WPF-Smoke: vuoropuhelu vähennetty; suunnittelun siirto ei muuttaa tallennettuja kaapeleita, tarkastelu/muisti saa alkuperän ja tallentaa korjausta, hylätä tallentaa mitään.
- Riippumaton tarkastelu: etukäteen ja pitkät linkit löytyi ja korjattiin; regressiotestit RED→GREEN.
- Release-rakennus nolla varoituksia/virheitä, koko Core-Suite ja WPF Smoke onnistuneesti. Täydellinen Windows-suite koostui aluksi keskittymisen mukaan lukien. Toinen juoksu epäonnistui uudelleen kieltäytyessä taustamuutoksessa; testi on kontekstista riippuvainen ja pysyy muuttumatta. Muita Windows -testiä, mukaan lukien mallinkuljetus, suoritetaan selkeästi hylättyjen menestystesteiden avulla.
- Paketin tarkastus paljasti olemassa olevan virheen, kun avattiin lyhyen ajan konfigurointitiedostoja. Lukemisen estäjä toistaa saman pääsyn kieltäytyneen virheen; rajoitettu toistaminen vain ydinvaihto on tarkastettu RED→GREEN. Pysyviä virheitä on näkyvissä.
- Nykyinen itsenäinen paketti `artifacts/release-20261012-184105`: WPF -puute onnistuneesti muistikortin jälkeen, mukaan lukien AI-dialogi ja suunnittelun hyväksyminen / hylkääminen.
- Ei todellinen malli: paikallinen Ollama -palvelu ei vastannut tutkimukseen. Ei lausuntoja mallien laadusta, latenssista, semanttisesta täydellisyydestä tai ajanhukkasuudesta. Hardware/Driver/Scalation -rajat ovat edelleen olemassa.

pöytäkirjan lähteet: [Ollama Chat](https://docs.ollama.com/api/chat), [Rakenteelliset tulokset](https://docs.ollama.com/capabilities/structured-outputs), [viralliset tyyppisuunnittelut, mukaan lukien kauko-mallitiedot](https://github.com/ollama/ollama/blob/main/api/types.go).

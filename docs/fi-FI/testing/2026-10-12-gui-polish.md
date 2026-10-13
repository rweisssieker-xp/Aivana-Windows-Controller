<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-12-gui-polish.md)

# GUI-muuntaminen alkaen 12.10.2026

- Suora käynnistys ”työkalut” navigointi; kolme ryhmää, joilla on ilmoitettu toimenpiteitä asennukseen, luomiseen ja testaamiseen.
- Neljätoista olemassa olevaa vuoropuhelua käyttää yhteistä WPF vuoropuheluluokkaa pääikkunan resursseja, yhtenäistä kirjoitusta ja sijaintia omistajan kanssa.
- Dialogi on rajoitettu Windows -työalueeseen; pitkä sisältö saadaan kierrätyslevyihin. Asetukset ja profiilit pysyvät kierrätettävissä myös pienellä korkeudella.
- Toimintasuodattimet näkyvät vain Dashboardissa. Profiilin valinta ja etsiminen ovat nimenomaisia avustavien teknologioiden nimiä. Maan kuvaukset ovat kontrastiä.
- Dialog-Smoke-testi sisältää työkalu- ja laajennusdialogeja 560 × 440 sekä normaali koko; olemassa olevat tallentajat ja kielikokeet on räätälöity Scroll-rakenteeseen.

Rajoitukset: Pienikokoiset ikkunat eivät korvaa todellisia Windows-DPI-testejä skaalauksilla 150/200 prosenttia. Uusia saksankielisiä tekstejä ei ole vielä kokonaan käännetty kaikille 27 kielelle. Ei uutta laitteistovalidointia eikä väitettä mitatusta käytettävyydestä.

Testitilanne: Release-rakennus ilman varoituksia/virheitä; täydellinen WPF kokeilu, mukaan lukien kompakti työkalu- ja laajennusdialogeja sekä kielenvaihdon testit. Git diff - tarkistaa

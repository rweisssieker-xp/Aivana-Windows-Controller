<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-12-responsive-layout.md)

# Reaktiivinen WPF -lähde

Tärkeimmät sivut suunnataan käytettävissä olevan sisällön leveyden mukaan, ei kiinteän ikkunan leveyden mukaan. 900 DIP: n sisällön leveys on Dashboard ja Editor. 620 DIP: n alla merkinnät ovat kaksipuolisia. 820 DIP: n ikkunoiden leveys on sidosryhmä. Pienin koko: 640 × 480 DIPs.

Profili- ja asetusalueet sekä painikkeen ohjauselementit vaihtavat niiden sarakkeita todellisen verkko leveyden mukaan. Käytettävissä olevat ohjauselementit siirretään sen sijaan, että niiden sisäänkirjautuminen uudistetaan. Toimintakortit jakaavat käytettävissä olevia linjoja. Työkalut rikkoutuvat. Painikkeen sivu on pystysuorasti saavutettavissa, fyysinen painikke säilyttää suhteensa.

Automaattinen WPF-Testaa kaikki viisi pääsivua 640 × 480, 900 × 620 ja 1440 × 900; Screenshots per sivu; kolmen vaihdon molemmissa suunnoissa tarkastamalla vastaanotettua tekstiä. WPF-Yksi yksikkö, todellinen Windows-Skalaaminen 150/200 Useat ja useat näytöt eivät ole vielä vahvistettu..

Testi: Release-Build ilman virheitä/varoituksia. Täydellinen WPF Smoke-testi kaikkien kokojen, sarakkeiden muutosten, painikkeiden asettamisen ja kielikokeiden kanssa. Git diff - tarkistaa

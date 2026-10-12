<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-12-github-publication.md)

# GitHub -julkaisun julkaiseminen MAKRIVO kehitysasteesta

Pysy: 12. Lokakuussa 2026. Lähteekoodin julkaiseminen, ei allekirjoitettu myynti.

- Ratkaisun julkaiseminen: onnistunut, nolla varoituksia ja virheitä.
- Core testit: menestyksekäs
- WPF-Smoke-testi: onnistunut; tuore tulosraportti paikallisessa rakennusjärjestyksessä.
- Ensimmäinen täydellinen Windows -suita: edistykselliset ikkunoiden, äänen, näytön ja iCUE testaukset; sitten Timeout UI-automaation työpöytään.
- Tavoitteellinen toisto `--extensions-only`: menestyksekkäästi mukaan lukien UI-automaatio, monitoimivuus, poistettu tavoitteet, salasanan sulkeminen, plug-in prosessi ja todellinen HTTPS-mobiili. Alkuperäinen Timeout ei ole piilotettu ajankohdan muutoksella; syy ei ole vielä lopullisesti todistettu.
- Seuraava täydellinen Windows toisto: menestyksekkäästi, mukaan lukien edistykselliset ikkunat ja kaikki laajennukset. Menestyksekäs toistaminen ei sulje pois spontaaneja Timeoutteja.
- Tyypillisiä yksityisiä avaimia tai tuottavaa Stripe-/GitHub-/AWS-avaimia ei löydetty ennen julkaisemista tarkastetussa lähteessä. Tämä rajallinen mallintarkastus ei ole täydellinen salaisuus.
- Rakennus- ja paikallisten orkestratatietojen ei julkaista.

Toiminta ja jäljellä olevat vähennökset: [Featurlista](../FEATURES.md)ja Edelliset tarkastuskertomukset säilyttävät ajalliset voimassaolonsa; ne eivät ole mitään takuita minkä tahansa laitteiston tai työpöydän yhteydessä.

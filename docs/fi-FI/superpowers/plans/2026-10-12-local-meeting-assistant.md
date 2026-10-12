<!-- generated-doc locale=fi-FI source=docs/superpowers/plans/2026-10-12-local-meeting-assistant.md sha256=1f105661dc4244fce4110ddae1484b4ce819e2d4c6945ada84a27704d95db72a -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/plans/2026-10-12-local-meeting-assistant.md)

# Paikallinen kokousavustaja – 2026-10-12

Hyväksytään ”ok” ja olemassa oleva määräys ilman kyselyjä. Nykyinen teollisuus / muutokset. Lyhyt prototyyppi, ei automaattinen toteutus.

1. Core: valitut resurssit IDs; malli vain resurssit-IDs, mikrofonin sammuttaminen, avoimet pisteet. Tiukka koon/tyypin/tunnuksen raja; Compiler tuottaa vain Launch/OpenPath/Url/Volume. Testit ensin: tuntemattomat tunnisteet, suoritettavat asiakirjat, ei-HTTP URL-osoitteet, kopioinnit, tyhjä suunnitelmat, ominaisuudet.
2. Windows: Ollama loopback-only HTTP ilman Proxy/Redirect, JSON-järjestelmä, token-/vastausrajat, häiriöt ja 90s-Zeitlimit; ei automaattinen asennus/lataus, ei pilvi/API-avain. Fake kuljetus testejä tarkistaa kysely ja virheet; todellinen malli vain, jos paikallisesti.
3. WPF: Ohjepale, vakiomuotoinen kytkim, mallin valinta paikallisesta palvelusta, tavoite ja selkeä resurssiluettelo, ennakointi/avoin piste, purkaminen, siirtyminen olemassa olevaan suunnitteluun ilman suoritusta. Muutokset ennakoinnin jälkeen heikentävät niitä; tarkistaa profiilin tarkistaminen.
4. Kokeilu: manuaalinen pysähdys ja paikalliset tapahtumat (ilman kohde/vaihe), suunnittelun hyväksyminen/korjaus/jättäminen sekä todelliset makrotilanteet. Ei todisteita menestyksestä yksin täytty; olemassa olevien tulosten tarkastaminen erikseen.
5. Testit: Core, kuljetus, WPF -myrsky; ei live-käyttäjätoimia. Raportti osoittaa puuttuvan live-malli testi ja edelleen auki vanhoja julkaisu portteja. Ei ole olemassa kuuden prosentin julkaisu, joka on AI-kyvytön.

Ollama-pöytäkirjan lähteet: https://docs.ollama.com/api/chat ja https://docs.ollama.com/capabilities/structured-outputs (12.10.2026).

## täytäntöönpano

- Core Compiler / Feature / Tietojen säästö vihreä alkuperäisen Compile-RED: n mukaan.
- Paikallinen malliasiakas vihreä alkuperäisen Compile-RED: n mukaan; kaukana Alias, pilvinen nimi, kaava, puuttuva reitti promptissa, rikki / liian suuri ja purkaminen tarkistettu väärennettyjen kauppiaiden kanssa.
- WPF-Handoff: ei tallennettu, ei suoritettava; tarkastelu / tallennus / korjaus ja hylkääminen savua vihreää.
- Fresh-context reviewer /root/meeting_review (read-only): kaksi P2-Funde, hylätty näkymät ja pitkät linkit. Molemmat korjataan Red→GREEN regressioon. Mitään muuta tärkeää havaintoa ei ole ilmoitettu.
- Release-Build null varoituksia/virheitä, kaikki ydinryhmät ja täydellinen Windows suita ilman Skip vihreä. Kotimainen tarkastus on nyt myös vihreä; olemassa oleva iCUE-näppäimistö liitetään. Ei todellisia käyttäjäohjelmia käynnistetty malli.
- Ei Live-Ollama käytettävissä; ei ole asennettu malli/palvelua eikä käytetä pilviä/avaimia. Laatu ja ajan säästöt ovat kokeellisia.
- Selfcontained -paketti, jossa on mukana ohjelmia ja uusia käyttö- ja testausohjeita artifaktien/release-20261012-183635. Saat olemassa olevia esineitä ja muutoksia, ei häiriöitä/push.

- Paketti-Smoke osoitti pääsyn kieltäytynyt ConfigStore atomar File.Move; tuore toistaminen jäi myöhemmin kiinni olemassa olevan virheen vuoropuhelun ja lopetettiin tarkoituksellisesti. Regression kanssa FileShare.Read (Lue sallitaan, Delete-sopiminen kieltäytyy) toistaa File.Move virheen. Rajoitettu takaisin vain Win32 5/32/33 500ms RED→GREEN; pysyvästi puuttuvat oikeudet jäävät virheitä. Uusi paketti tarkastetaan uudelleen.

- Lopullinen täydellinen Windows toistaminen epäonnistuu uudelleen kieltäytyneellä keskittymisvaihdolla; ei ole uutta kompilaattorin / mallin virhettä. Muut Suite --skip-foreground-fixture vihreä. Ei ole vakaa vihreä väittää kontekstiperusteinen alkuperäinen etu-testi. Nykyinen pakettiväylä artifacts/release-20261012-184105

- Nykyinen itsenäinen paketti artifacts/release-20261012-184105: WPF-myrskyä mukaan lukien AI-dialogi, tallettamaton muokkaus, korjausten mittaaminen ja hylkääminen onnistuneesti (Exit 0). Ei muuta koodia tämän testin jälkeen.

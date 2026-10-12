<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-09-usp-release.md)

# Keyboard tuki, ylitys ja palaute

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Käytettiin kolme priorisoitua parannusta olemassa olevassa Windows sovelluksessa:

- Erityinen avain: todellinen iCUE tuettu laite / avain tallennus, tilapäinen G-tunnus, konfiguraation menestys, konfliktin validointi ja säästämätön toimintasuunnitelma. Capture rollback, queued callback invalidation ja normal-binding palautus kattaa virheet ja vuoropuhelun sulkeminen.
- Keyboard overlay: fyysinen viittaus geometria, aktiivinen profiilin sidokset, live profiili / muokkaus ja tavallinen Windows avain korostus. Pidä Ctrl+Alt+F1 tai otetaan painikkeen/näppäimen kautta. Aktivointi, klikkaaminen ja poissaolo työpöydästä.
- Toteutusviittaus: kirjoitettu Odotus / Juokseminen / Vaihe / Täytetty / Poistettu / Kieltäytynyt vaiheet, askel / toistuminen edistyminen ja ei-aktivointi. Palautteen sisältö ja poikkeus viestit. Lopullinen peruuttaminen ei voi raportoida loppuun. Täydellisyys kuvailee täytäntöönpanoa, ei itsenäistä OBS valtion kyselyä.

Tarkistukset :

- 24 Ydin tarkastukset ovat suorittaneet, mukaan lukien vaiheen tilaus, epäonnistumisen / peruuttamisen tulokset, lopullinen vaihe peruuttaminen, kuuntelijan poikkeus ja yksityisten toiminnan arvojen sulkeminen.
- Päätös on tehty ilman varoituksia/virheitä.
- Windows tarkastukset läpäisivät; yhteinen K100 ilmoitti G1–G6 ja alkuperäinen konfigurointi/lupa onnistui. Äänipisteitä tarkastettiin muuttamatta tilaa.
- WPF tupakointi kulki kaikkien viittausten ylityölaitteiden, taikuri/katalogi/konflikti/taistelu pysyvyys, live alkuperäinen tallennus konfiguraatio, callback siirto, jyrkkä callback hylkääminen ja tauko- sitoutuminen palauttaminen. Native callback -tulostus on simuloitu; fyysinen inhimillinen G-tärkeä lehdistö on manuaalinen tarkastus.
- Ajankohtainen etu-ikkunan vaihtelu tarkastettiin täytäntöönpanosta. Overlay vuorovaikutuksen ominaisuuksia ja näyttely / piilotettu; näytönkuvat tehtiin miekka, overlay ja toast.
- 27 kielet sisältävät kaikki 819 kanoniset rivit; offline sijainti / avain validointi kulunut.

Snapshots ja täydellinen GUI tulos ovat artifakteja/usp-win-x64 / savua. Testit käyttävät väliaikaisia profiileja; tuotantoasetukset tuetaan ja tarkistetaan käyttöönoton aikana.

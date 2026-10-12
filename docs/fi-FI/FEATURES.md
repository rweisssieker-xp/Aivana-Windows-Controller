<!-- generated-doc locale=fi-FI source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../FEATURES.md)

# MAKRIVO – Näytönohjaus

Valmistaja: **Aivana**. Pysy: **12. Lokakuussa 2026**. Tämä on kehitysvaihe, ei myynti- tai laitteiston vapauttaminen.

## Windows ohjaus

- Kaksit, globaalit hotkeys, komento-pallot, suosikkeja ja sovellus riippuvaisia profiileja.
- Makroja, joissa on useita askeleita, toistumisia, taukoja, purkamista ja ennakointia.
- Avaa ohjelmia, tiedostoja ja verkkosivuja; käytä tekstiä, välimuotoa ja painikkeita.
- Aseta ikkunoita, sulje Windows, hallitse äänenlähteitä ja mikrofonia sekä muuttaa näytön kirkkautta tukemien käyttöliittymien kautta.
- OBS-WebSocket-v5-Anbindung näyttelyille, tallennus, virtaaminen ja äänitys.
- Mallit, työpaikan avustaja, laitteen tunnistus ja painikkeiden ylittäminen.
- Tuonti ja vienti profiilin konfiguraatioista; suunnittelun tarkastaminen ja konfliktien tunnistaminen.
- Paikallinen tallennettu asetukset; ei tarvita tiliä päivittäiseen käyttöön.

## Lisäominaisuudet nykyisessä lähtekoodissa

| Alueella | Toiminnalliset laajuudet | Rajat |
| --- | --- | --- |
| Tuloksetarkastus | Ennaltaehkäisy, jäljitys ja testaus | Vain tarkistettavat vaikutukset vahvistavat; onnistunut painiketta ei osoita kohdennettua vaikutusta. |
| Työpaikan avustaja | Valmista profiilit ja prosessit valituista ohjelmista, tiedostoista, URL-osoitteista ja muista toimista | Ennaltaehkäisy ja tallennus on välttämätöntä; laite ei aiheuta mitään. |
| Makro ja Workflows | Rekisteröinti, olosuhteet ja paikallinen profiilin synkronointi | Käyttäytyminen ja rajat on dokumentoitu täytäntöönpano- ja testausraportteissa. |
| UI automaatio | Valitse tavoitteet, tarkista valintaohjelmat, toimenpiteet ja rajalliset klikkaukset | Erilliset työntekijät; monivaiheinen, deaktivoitu ja salasanan tavoitteet hylätään; ei yleistä tekstiä. |
| Laajennukset | Paketit tarkastellaan, tuodaan, aktivoidaan, vientiin ja palautetaan selkeästi luottamuksen mukaan | Prosessi eristyminen ei ole hiekkapuhelin; ei julkinen plug-in-markkinoita. |
| Mobiilipaneeli | Paikallinen HTTPS-paneeli, yhteys, valitut profiilit ja tilan/testihistoria | Todellinen puhelin/tabletti-LAN-lähde on vielä auki; ei universaalia sensori-näppäintä. |
| Valmistajan adapteri | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB ja Wooting-Analogadapterit | Mallin/SDK: n mukaan; uusi adapteri ei ole täysin validoitu todellisella laitteistolla. Ei uusia omistettuja SDK-DLLs yhdistetty. |
| Fyysinen painiketta | Valitse laitteen ja skannauskoodin | Raw Input ei tukahduta normaalia Windows -sisääntelyä. |
| Paikallinen AI-keskustelija | Valinnainen prototyyppi Ollama, LM Studio, llama.cpp ja ONNX Runtime GenAI CPU tuottaa rajallisia suunnitelmia nimenomaisesti valittujen resurssien | Vakiokohtaisesti; alkuperäinen CPU-testi satunnaisella mallilla, mutta ei todistettu suunnittelun laatu tai ajan säästö; ei sisälly mallin painoja. |

## Ei ole vielä myynnissä valmiina

- **Ekslioppinen toissijainen / makro: ** Lupautettu ja laitteistoon tarkistettu suodatin puuttuu. Normaalit painikkeet voivat edelleen saavuttaa Windows.
- Täydellinen laitteiston poistaminen kaikista valmistajista sekä todelliset mobiililaitteet.
- Todellinen sekoitettu monitori/DPI-muutos ja kaikkien uusien käännösten asiantuntija. PerMonitorV2 ja simuloitu 150/200-Prozent-Layouts on testattu; todelliset näytötestit pysyvät erillään. Ympäristö tarjoaa 27 kieliä; yksityiskohtaiset uudet tekstit eivät ole vielä täysin kielellisiä.
- Allekirjoitettu myyntipäällikkö, lopullinen EULA, tarkistettu SDK-myyntioikeudet ja tuottava ostolupa/vakuutus integrointi.

## Yksityiskohdat ja todisteet

- [Käyttödokumentti](README.md)
- [Laajennukset: ominaisuudet, testit ja avoimet rajat](testing/2026-10-12-six-extensions.md)
- [Paikallinen AI-avustaja: prototyyppi ja testaus](testing/2026-10-12-local-meeting-assistant.md)
- [Paikalliset AI-aika ja mallin vaatimukset](testing/2026-10-12-local-ai-runtimes.md)
- [DPI- ja asteittainen tutkimus](testing/2026-10-12-dpi-scaling.md)
- [Lisenssin tarkastus](licensing/2026-10-12-commercial-license-audit.md) – Historiallinen tallennus; uusi julkaisu täydentää Runtime-ohjeita. Lopullinen paketti tarkastetaan erikseen.
- [Myyntialustoja](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Vertailuarvo; Stripe on lisäksi mahdollinen maksuväline, katso nykyiset toimitusehdot.

## Täydellinen kehityspaketti

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Paketti sisältää työpöytä-sovelluksen, UI-automaation työnantajan, matkapuhelimen, selaimen tiedostojen, esimerkkisovelluksen ja käynnistyksen käyttöoikeusohjeen. Rakennusvirheitä ei tarkasteta Gitissä. GitHub lähdekoodi ei ole vielä julkaistu Windows asentaja.

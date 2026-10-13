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
| UI automaatio | Valitse tavoitteet, tarkista valintaohjelmat, toimenpiteet ja rajalliset klikkaukset | Erillinen työprosessi; moniselitteiset kohteet, käytöstä poistetut kohteet ja salasanakohteet hylätään; ei yleistä tekstinsyötön tallennusta. |
| Laajennukset | Paketit tarkastellaan, tuodaan, aktivoidaan, vientiin ja palautetaan selkeästi luottamuksen mukaan | Prosessi eristyminen ei ole hiekkapuhelin; ei julkinen plug-in-markkinoita. |
| Mobiilipaneeli | Paikallinen HTTPS-paneeli, yhteys, valitut profiilit ja tilan/testihistoria | Todellisen puhelimen tai tabletin LAN-hyväksyntätestaus on vielä avoinna; ei yleiskäyttöistä anturikoontinäyttöä. |
| Valmistajan adapteri | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB ja Wooting-Analogadapterit | Riippuu mallista ja SDK:sta; uusia sovittimia ei ole kokonaan validoitu todellisella laitteistolla. Uusia valmistajakohtaisia SDK-DLL-tiedostoja ei toimiteta mukana. |
| Fyysinen painiketta | Valitse laitteen ja skannauskoodin | Raw Input ei tukahduta normaalia Windows -sisääntelyä. |
| Paikallinen AI-keskustelija | Valinnainen prototyyppi Ollama, LM Studio, llama.cpp ja ONNX Runtime GenAI CPU tuottaa rajallisia suunnitelmia nimenomaisesti valittujen resurssien | Oletusarvoisesti pois käytöstä; natiivi CPU-suoritusympäristö testattu satunnaismallilla, mutta suunnittelun laatua tai ajansäästöä ei ole osoitettu; mallipainoja ei sisälly pakettiin. |

## Ei ole vielä myynnissä valmiina

- **Ekslioppinen toissijainen / makro: ** Lupautettu ja laitteistoon tarkistettu suodatin puuttuu. Normaalit painikkeet voivat edelleen saavuttaa Windows.
- Kaikkien valmistajasovittimien täydellinen laitteistohyväksyntä ja todellisten mobiililaitteiden LAN-testit.
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

Paketti sisältää työpöytäsovelluksen, UI-automaation työprosessin, mobiilipalvelimen, selaintiedostot, esimerkkilaajennuksen ja suoritusympäristön lisenssi-ilmoitukset. Koontiartefakteja ei tallenneta Git-versionhallintaan. GitHub-lähdekoodiversio ei vielä ole julkaistu Windows-asennusohjelma.

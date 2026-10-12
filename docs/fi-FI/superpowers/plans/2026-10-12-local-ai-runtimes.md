<!-- generated-doc locale=fi-FI source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Paikallinen AI-aika ja jäljellä olevat tarkastukset

Tavoite: Liitä enemmän paikallisia tietotekniikan aikavälia, saada suunnittelun tarkastusta ja tehdä avoimet julkaisuja näkyviltä. Käyttäjä lupaa toteuttaa ilman kyselyä. Nykyiset muutokset säilytetään.

1. Loopback-liikenteen ja ONNX -paketin tarkastukset ennen toteutusta; verkko-osoitteiden tarkastaminen, kustannusten rajoittaminen, purkaminen ja tuntemattomat resurssit.
2. Yhteiset suunnittelijat; Ollama Saatu, LM Studio (127.0.0.1:1234) ja llama.cpp (127.0.0.1:8080) Ei pilvipalveluita tai automaattisia palveluntarjoajien vaihtoja. paikalliset palvelimet voivat ohjata itse; niiden toiminta on edelleen näkyvissä toimijan vastuulla.
3. ONNX Runtime GenAI CPU omassa prosessissa, jossa on rajoitettu aika, JSON-protokolla ja prosessi. Valitse mallin järjestys nimenomaisesti; mitään mallia ei ladata automaattisesti. Vain GenAI yhteensopiva tekstimalli, ei mitään .onnx verkostoja.
4. WPF -valinta kestoa varten, malli, ONNX -lehti; kaikki tarjoavat saman tiukasti tarkastetun suunnittelun.
5. Tarkastuspaikka, jossa on selkeät rajat laitteistolle, DPI:lle, mobiililaitteelle ja yksinomaiselle makropanille. Fyysiset testit ja allekirjoitettu/lisensoitu ohjaus ei voi korvata ohjelmistoa.
6. Release-Build, Core/Windows -testi, WPF -myrkytys, työläisen / paketin tarkastus ja riippumaton tarkastus. Luo uusi täydellinen paketti; ilmoita mallin laatu vain todellisessa olemassa olevassa mallissa, joka on testattu.

Tuloksena: neljä käyttöaikaa toteutettu, ONNX kotimainen erikseen testattu, toimittajan valinta ja testauspaikka pakattu WPF-myrsky. Core/Windows - huoneisto on vihreä Tarkastelu Unicode korjataan yhteisen pysyvyyden ja regressioon. Todelliset laitteiston/mobiilien/DPI-vaihtoehdot, ohjaimen vapauttaminen, täydet käännökset ja live-instruktin laatu pysyvät nimenomaisesti avoimina töinä; eivät ole kokonaan käytettyjä.

Jatka: erilliset Jinja- ja nimettyjen / oletusarvoisten mallien täytäntöönpano; tyypin / rajan / Fallback-regressiotutkimukset. Työkalu navigointi 27 kataloissa täydentää. Todellinen opetusmalli erikseen ladattu ja tarkistettu SHA256: alkuperäinen CPU-sopimus toimii, mutta 0/4 suunnittelu tapaukset täyttävät JSON-sopimuksen; ei toimia. Riippumaton tarkastelu: null-testi korjattu ja testattu, ei myöhemmin konkreettisia muita havaintoja. Core ja Windows KI-testiä, julkaisu rakentaa 0 hälytykset/virheet.

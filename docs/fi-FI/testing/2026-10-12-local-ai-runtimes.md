<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-12-local-ai-runtimes.md)

# Paikalliset KI-ajat

## Palvelu

Työkalut → Avaa AI-keskusteluasiantuntija, aktivoi nimenomaisesti ja valitse AI-työaika. Julkaise resursseja, muotoile tavoitteita, luo suunnittelua ja tarkistaa toimittajaa. Yksikään toimittaja ei toteuta suunnitelmaa automaattisesti. Ei ole automaattista palveluntarjoajan vaihtamista.

| Käytön aika | Ehdotus | Yhteys |
|---|---|---|
| Ollama | Paikallinen asennettu malli | 127.0.0.1:11434 |
| LM Studio | Käytössä oleva paikallinen palvelin, jossa on paikallinen ladattava malli ja JSON-järjestelmän tuki | 127.0.0.1:1234 |
| llama.cpp | Käytössä oleva llama-palvelin paikallisen mallin ja JSON-järjestelmän tuen kanssa | 127.0.0.1:8080 |
| ONNX Käyttöaika GenAI CPU | yhteensopiva ja luotettava paikallinen GenAI tekstijärjestelmä | erillinen yhdistetty prosessi, ei HTTP |

HTTP-asiakkaat eivät käytä Proxiaa eivätkä seuraa ohjauksia. Paikalliset palvelimet voivat käyttää myös muita palveluita; erityisesti LM Link/Remote-routingia ei saa käyttää paikalliseen käsittelyyn. Tämä ei voi tunnistaa MAKRIVO yhteensopivalla käyttöliittymällä luotettavasti. Ollama Etätiedot ja pilvi-nimet hylätään. Auteeratut paikalliset palvelimet eivät ole konfiguroitavia tässä versiossa.

## ONNX

Valitse dialogissa genai_config.json. Tiedoston on sisällettävä tokenizer.json ja chat-mallin. Tukea chat_template merkki, nimetty oletusarvo (array tai sanasto) sekä erillinen chat_templa.jinja, jos ei ole sisäänkirjautunut malli tai se on nolla. Useita malleja hylätään; enintään 65536 merkkiä. Käytettävät .onnx-tiedostot, visio-/sovellusmallit tai puhdas GPU/NPU-eksportit eivät ole kokousmalleja.

Yhdistetty: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Palveluntarjoajan konfiguraatio palautetaan CPU: lle. Ei latausta eikä mallipainoja pakkauksessa. Pythonin asennusta ei tarvita. Käytä vain luotettavia mallipaketteja; erillinen prosessi on romahdus / romahdusraja, ei turvakaappi.

Rajat: 90 sekuntia, mukaan lukien mallin lataus, enintään 3072 Prompttokensit ja 1024 tuottamat tokensit, lisäksi malliin liittyvä raja; tiukasti tarkastettu JSON ja tunnetut resurssit-IDs. Virheellinen/virheellinen vastaus hylätään. Sulje / sulje käynnistetty työprosessi. Ei ilmaisia mallien käskyjä eikä ohjelmia/tiedostoja Promptin erillisessä resurssiluettelossa; käyttäjän kirjoittama kohde voi sisältää näitä tietoja.

## Tarkistaminen

- Core-Suite, Loopback-liikenteen testi simuloidulla LM-studio-/llama.cpp-/Ollama vastauksella, kaava/lähtö/resurssien tarkastus.
- Unicode-pöytäkirjan regressioon, jossa on pitkät tavoitteet ja 24 Resurssin nimet.
- Todellinen alkuperäinen CPU-sovellus virallisella tiny-random-gpt2-fp32-Testgraph microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testin malli täydentää ja byte-tokenizer mukautetaan pieni satunnainen grafiikka. Tämä satunnainen malli ei testata laatua eikä ole julkaistu.
- Toistettavissa oleva alkuperäinen koetustesti: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI -mallin järjestäjä>.
- WPF-Smoke tarkistaa kaikkien neljän tarjoajan valinnan ja testauksen; yksin valinta ei tuota mallipyyntöä.

## Todelliset vapautukset

Laitteiston/ älypuhelin/tabletin/monitorin vaihdon testaus on edelleen tarpeen. Ainoastaan painiketta suljettu pysyy ilman allekirjoitettuja, lisensoituja ja tarkastettuja suodattimia. Ei korvattu tyhjiö tai Raw Input. Keskitykseen voidaan edelleen estää Windows; virhe kutsuu nyt kohdekannalla manuaalista aktivointia. Keskeisiä tietotekniikan välineitä on lisätty kaikkiin 27 -kataloihin; yksityiskohtaiset uudet tekstit ja asiantuntijalausekkeet ovat edelleen auki. Elävää laatua, jossa on asianmukainen opetusmalli, kestävyys ja mitattu aika ei ole todistettu satunnaisella / fake-HTTP-testeillä.

## Lähteet

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Lopullinen testaus: Release-rakennus ilman varoituksia/virheitä; täydellinen ydin- ja Windows -suita, mukaan lukien kotimainen Focusfixture, HTTPS, UIA, Plugin IPC ja yhteensopiva AI-liikennätesti. Uusi paketti vapautus20261012-190618: WPF-puute (19:07 tunti), yhdistetty ONNX-CPU-sovellus testgraphin kanssa. Riippumaton tarkastelu: Unicode-pöytäkirja korjataan, ei muita konkreettisia havaintoja. Ei fyysistä valmistajan / puhelimen / monitorin vähennystä.

## Seuraava: Todellinen malli ja mallit

Lisä CPU-toiminta kanssa allama-ai/SmolLM-360M-Instruct-onnx-int4, tarkistus 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: alkuperäinen infersion. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Yhteisön vienti vain testausalueella, ei julkaisu.

Neljä todellista suunnittelutestejä (saksan/englanti, asiakirja, ohjelma, selkeä mikrofonin ääni): **0/4 Voimassa olevat suunnitelmat**; JsonException tiukassa vastauskokeessa. 63501, 6376, 72118 ja 6774 Tämä pieni malli ei täytä suunnittelussopimusta eikä sitä suositella sopivaksi. raportti: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Testit --evaluate-onnx <mallin järjestäjä> <raportti>. suorituskyvyn ominaisuus ei osoita mallin laatua.

Työkalu-navigointi ja testausraportti on käännetty kaikkiin 27 kielitaulukkoihin. Pitkät tekstit ja kieli pysyvät auki. Mallin regressio tutkii myös vääriä JSON-tyyppejä, tyhjiä / monikäyttöisiä / ylikuormittuja malleja ja nolla Jinja-palaa.

Päivitetty paketti: artifacts/release-20261012-201134 Pakkaettu WPF-pyyhkeet kestivät 12.10.2026 ympärillä 20:12; kotimainen CPU-infersio erillisellä instruktin testimallilla yhdistetyssä työläisessä. Core ja Windows KI-testiä; Rakenna ilman varoituksia/virheitä. Mallin laadun jatkaminen 0/4.

<!-- generated-doc locale=et-EE source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Kohalikud uuringud ja ülejäänud uuringud

Eesmärk: tõepoolest ühendada täiendavaid kohalikke AI-tööaegu, saada projekteerimist ja teha avatud avaldusi nähtavaks. Kasutaja lubab rakendamist ilma küsimusteta. Praegused muudatused jäävad säilitama.

1. Loopback transpordi ja ONNX pakendi kontroll enne rakendamist; võrgustike aadressid, kulude piirid, katkestamine ja tundmatute ressursside kontrollimine.
2. Ühine planeerimise uudised; Ollama Saadud, LM Studio (127.0.0.1:1234) ja llama.cpp (127.0.0.1:8080) Täiendavad. ei ole pilve URL-d ega automaatsed tarnija vahetused. kohalikud serverid saavad ise edastada; nende toimimine jääb nähtavaks operatori vastutuseks.
3. ONNX Runtime GenAI CPU oma protsessiga, piiratud JSON-protokoll ja protsessi katkestamine. Valige mudeli järjekord selgelt; mudel ei alla laadita automaatselt. Ainult GenAI ühilduvad tekstimudelid, ei ole mingeid .onnx võrke.
4. WPF valik kestus, mudel, ONNX kotid; kõik pakuvad sama rangelt kontrollitud disain.
5. Testimiskeskus, millel on selged piirid hardware, DPI, mobiilirakenduse ja eksklusiivse makro. Füüsilised testid ja allkirjastatud/litsentsitud juhid ei saa asendada tarkvara abil.
6. Release-Build, Core/Windows testid, WPF müra, töötaja/pakendi kontroll ja sõltumatu ülevaade. Uue täieliku pakendi loomine; mudeli kvaliteet teatatakse ainult olemasoleva mudeli puhul, kui see on testitud.

Tulemus: neli käivitusaega, ONNX eraldi testitud, pakkuja valik ja katsetamine pakendatud WPF. Core/Windows rohelised kotid Ülevaade Unicode piirid korrastatud ühise konstantsuse ja regressioon. Tõeline hardware/mobiilne/DPI vahetus, juhtimise vabastamine, täieliku tõlke ja elava instruktsiooni kvaliteet jäävad selgelt avatud tööks; mitte täielikult kulutatud.

Järgmine: eraldi Jinja ja nimetatud/default näidised rakendatud; tüüp/piir/fallback regressioonide kontrollimine läbi. Tööriistade navigatsioon täiendatakse 27 kataloogides. Tõeline juhend mudel eraldi alla laaditud ja kontrollitud SHA256: native CPU infektsioon töötab, kuid 0/4 planeerimise juhtumid vastavad JSON lepingule; ei toimu tegevusi. Sõltumatu ülevaade: null-testi ja pärast seda konkreetseid täiendavaid avastusi. Core ja Windows KI testid läbi, Release-Build 0 hoiatused / vead.

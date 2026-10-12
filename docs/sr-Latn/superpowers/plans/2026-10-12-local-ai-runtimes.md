<!-- generated-doc locale=sr-Latn source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokalni II vremena i preostale testove

Cilj: da se poveže više lokalnih II vremena, da se dobije ispitivanje dizajna i da se vidljive otvorene izdanja. Korisnik omogućava implementaciju bez pitanja. Postojeće promene ostaju.

1. Testovi za loopback prevoz i ONNX testiranje paketa pre implementacije; proveravanje mrežnih adresa, ograničenja troškova, otpada i nepoznatih resursa.
2. Zajednički planirač; Ollama Dobijeni, LM Studio (127.0.0.1:1234) i llama.cpp (127.0.0.1:8080) Nema oblačnih URL-a ili automatske promene provajdera. lokalni serveri mogu sami da prenose; čija operacija ostaje vidljiva odgovornost operatora.
3. ONNX Runtejm GenAI CPU u sopstvenom procesu sa vremenskim ograničenjem, ograničenim JSON protokolom i procesom. Izbor reda modela izričito; nijedan model se automatski preuzima. Samo GenAI kompatibilni tekstovi, bez bilo koje .onk mreže.
4. WPF izbor za trajanje, model, ONNX papir; svi pružaju isti strogo proveren dizajn.
5. Test sa jasnim granicama za hardver, DPI, mobilni panel i ekskluzivne makropade. Fizičke testove i potpisani / licencirani vozač ovde se ne mogu zameniti softverom.
6. Izgradnja izdanja, Core/Windows testovi, Smuke WPF, Radnički/paket pregled i nezavisni pregled. Kreiranje novog kompletnog paketa; kvalitet modela samo ako je stvarno postojeći model kao testiran.

Rezultat: implementirani četiri vremena, ONNX nacistički testiran odvojeno, izbor ponuđača i ispitivanje u pakovanom WPF gljivice. Core / Windows suet zeleni. Revizionisanje jedinstvene granice sa zajedničkim konstantom i regresijom. Istinski hardver / mobilni / DPI razmjene, diskovi, kompletne prevodi i kvalitet živog instrukta ostaju eksplicitno otvorena rad; ne kao potpuno isplaćeni.

Nastavite: sprovedeni su odvojeni džinga i imenovani/defolt obrasci; prošao je test regresije tipa/granice/fallback. Instrument-navigacija u 27 katalozi dopunjeni. Istinski model instrukcije je posebno preuzeta i proverena SHA256: indijanski CPU reference radi, ali 0/4 slučajevi planiranja su u skladu sa JSON ugovorom; nije izvršena akcija. Nezavisni pregled: null-fallback ispravljen i testiran, a zatim nema konkretnih dodatnih otkrića. Testovi Core i Windows II prošao, Izgradnja 0 upozorenja / greške.

<!-- generated-doc locale=hr-HR source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokalni časovi II-a i preostali ispiti

Cilj: stvarno povezati daljnje lokalne vremenske uvjete, dobiti testove i vidjeti otvorene publikacije. Korisnik ovlaštena provedba bez upita. Trenutačne promjene ostaju.

1. Testovi za loopback prijevoz i ispitivanje paketa ONNX prije provedbe; provjeravanje mrežnih adresa, ograničenja troškova, razbijanja i nepoznatih resursa.
2. Zajednički planeri; Ollama Dobiveni, LM Studio (127.0.0.1:1234) i llama.cpp (127.0.0.1:8080) Nema URL-a u oblaku ili automatske promjene pružatelja. lokalni serveri mogu samostalno voditi; njihovo djelovanje ostaje vidljiva odgovornost operatera.
3. ONNX Runtime GenAI CPU u vlastitom procesu s vremenskim ograničenjem, JSON protokola i procesnih prekida. Izaberite redoslijed modela; nijedan model se automatski ne preuzima. Samo GenAI kompatibilni tekstovi, bez bilo koje .onnx mreže.
4. WPF izbor za trajanje, model, ONNX raspored; svi pružaju isti strogo provjereni dizajn.
5. Testni prostor s jasnim granicama za hardver, DPI, mobilni panel i ekskluzivni makropad. Fizičke ispite i potpisani/licencirani vozač ovdje se ne mogu zamijeniti softverom.
6. Release-Build, Core/Windows testovi, WPF smoke, Worker/Packing test i neovisna pregleda. Stvaranje novog punog paketa; kvaliteta modela prijaviti samo u stvarnom postojećem modelu kao testiran.

Rezultati: provedena četiri razdoblja rada, ONNX nativno testirano, izbor dobavljača i ispitivanje u pakiranom WPF smoke. Core/Windows suite je zelena. Recenzija Unicode granice ispravljena zajedničkim konstantom i regresijom. Prava razmjena hardvera / mobitela / DPI-a, oslobađanje pogona, cjeloviti prevodi i kvaliteta živih instrumenata ostaju izričito otvoreni rad; ne kao potpuno ispušten.

Nastavak: implementiraju se odvojenim Jinja i imenovanim/default predlozima; prošli su testovi regresije tipa/graničnog/falbacka. Navigacija alata u 27 katalogima dopunjeno. Pravi model instrukta je odvojeno preuzeta i provjerena SHA256: native CPU reference radi, ali 0/4 slučajevi planiranja ispunjavaju ugovor JSON; nema djelovanja. Neovisni pregled: null-Fallback ispravljen i testiran, nakon čega nema konkretnih dodatnih otkrića. Core i Windows testiranja AI, Release-Build 0 upozorenja / pogreške.

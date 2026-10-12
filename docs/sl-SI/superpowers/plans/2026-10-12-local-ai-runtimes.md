<!-- generated-doc locale=sl-SI source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokalni časovni razpisi in preostali pregledi

Cilj: dejansko povezati več lokalnih časov AI, pridobiti pregled zasnove in vidne odprte publikacije. Uporabnik dovoljuje izvajanje brez povpraševanja. Obstoječe spremembe ostanejo.

1. Preizkušnje za prenos in preizkušanje paketov ONNX pred izvajanjem; preverjanje omrežnih naslovov, omejitev porabe, odpoved in neznanih virov.
2. Skupni načrtovalci; Ollama Pridobljeno, LM Studio (127.0.0.1:1234) in llama.cpp (127.0.0.1:8080) Dodajanje. nobenih URL-jev v oblaku ali samodejne spremembe ponudnikov. lokalni strežniki lahko sami usmerjajo; njihova delovanja ostanejo vidna odgovornost operaterja.
3. ONNX Runtime GenAI CPU v lastnem procesu z časovno omejeno, JSON protokol in procesni prekinitev. Izberite razvrstitev modela; noben model se samodejno prenese. Samo GenAI združljivi besedilni modeli, brez katere koli .onnx omrežja.
4. WPF izbira za trajanje, model, ONNX ploščice; vsi zagotavljajo enako strogo preverjeno zasnovo.
5. Preizkusni prostor z jasnimi meji za strojno opremo, DPI, mobilne plošče in ekskluzivne makro. Fizične preglede in podpisani/licencirani pogon se ne morejo zamenjati s programsko opremo.
6. Release-Build, Core/Windows testov, WPF smoke, Worker/Packing pregledi in neodvisni pregled. Ustvarite nov celoten paket; poročati o kakovosti modela le v primeru dejansko obstoječega modela kot testiran.

Rezultat: izvedena štiri obdobja, ONNX nativno preizkušena ločeno, izbira ponudnika in preizkušanje v pakiranem WPF smoke. Core/Windows - svečke so zelene. Pregledne meje Unicode z skupno konstantno in regresijo popravljeno. Resnične zamenjave strojne opreme/mobile/DPI, sproščanje gonilnikov, popolna prevoda in kakovost živih instrumentov ostanejo izrecno odprta dela; ne kot popolna.

Nadaljevanje: izvedena ločena jinja in imenovana/izmenjena predloga; preživel je tip/granični/falback regresijski test. Navigacija orodij v 27 katalogih dopolnjuje. Resnični model navodila ločeno prenesli in preverili SHA256: native CPU reference deluje, vendar 0/4 načrtovanje primerov izpolnjuje pogodbo JSON; nobenih ukrepov. Neodvisna pregleda: null padec popravljen in testiran, potem pa ni konkretnih nadaljnjih ugotovitev. Core in Windows-KI testov, Release-Build 0 opozorila / napake.

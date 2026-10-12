<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokalni II vremena

## Usluge

Instrumenti → Otvoriti II asistent, eksplicitno aktivirati i izabrati II vreme. Podelite resurse, formulišete cilj, kreirajte dizajn i proverite u uređaju. Nijedan provajder ne izvršava projekat automatski. Nema automatske promene.

| Vreme trajanja | Predlog | povezivanje |
|---|---|---|
| Ollama | lokalno instaliran model | 127.0.0.1:11434 |
| LM Studio | Lokalni server sa lokalnim model i podršku za JSON shemu | 127.0.0.1:1234 |
| llama.cpp | Falma server sa lokalnim model i podršku JSON sheme | 127.0.0.1:8080 |
| ONNX Runtejm GenAI CPU | kompatibilni, pouzdani lokalni GenAI tekstualni model | Odvojeni povezani proces, a ne HTTP |

HTTP klijenti ne koriste proxy i ne prate smernice. Lokalni serveri mogu da se odnose na druge usluge; posebno LM link / daljinski routing ne može da se koristi za lokalnu obradu. Ovo MAKRIVO ne može pouzdano da otkrije kompatibilan interfejs. Ollama udaljeni podaci i imena oblaka su dodatno odbijeni. Autentifikovani lokalni serveri nisu konfigurisani u ovoj verziji.

## ONNX

U dijalogu izaberite genai_config.json. Papke moraju da sadrže tokenizer.json i šablon za razgovor. Podržava se chat_template kao znak lanca, imenovan podrazumevani šablon (arej ili rečnik) kao i odvojeni chat_t template.jinja, ako nema inline šablona ili je nula. Nedostatak brojnih obrasca je odbijen; maksimalno 65536 znakova. Popularni .onk datoteke, vizuelni / embeding modeli ili čisti GPU / NPU izvoz nisu modeli za sastanak.

Kombinovano: Microsoft.ML.OnkRuntajGeAI 0.17.1, CPU Konfiguracija provajdera se vraća na CPU. Nema preuzimanja i nema težine u paketu. Nema potrebe za Python instaliranjem. Koristite samo pouzdane pakete modela; odvojeni proces je granica za srušenje / srušenje, a ne bezbednosni peškir.

Ograničenja: 90 sekundi uključujući model, maksimum 3072 tokena i 1024 generisanih tokena, dodatni model kontekst granica; strogo provereni JSON i poznati ID resursa. Odgovor je odbijen / odbijen. Zatvaranje/ukidanje završava započeti proces radnika. Nema slobodnih zapovesti za model i nema programa / fajlova u odvojenom listu resursa; ciljni tekst koji je napisao korisnik može da sadrži takve podatke.

## Provera

- Core Suite, Loopback Transport Tests with Simulated LM Studio/llama.cpp/Ollama Reactions, Shema/Otvaranje/Istraživanje resursa.
- Unikalna regresija protokola sa maksimalnim dugim ciljevima i imenima resursa 24.
- Istinski indijanski CPU reference sa zvaničnim tiny-random-gpt2-fp32-Testgraph iz Microsoft/onkruntej-genai, Komit 5c19002edce50bea3a5c7eefd8fe5269e8d19. Test šablon je dopunjen i bite tokenizator prilagođen na mali slučajni grafik. Ovaj slučajni model ne testira kvalitet i nije u izdanju.
- Reproduktivni test za pokretanje: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-Model Order>
- WPF-Smoke ispituje izbor svih četiri provajdera i ispitivanje; izbor sam ne stvara zahtev za model.

## Ostali stvarni otpuštaj

Testovi za promenu hardvera / pametnog telefona / tableta / monitora i dalje su potrebni. Ekskluzivno pritisak tastature ostaje blokiran bez potpisanih, licenciranih i proverenih filtera. Ne zamenjuje se gumenim vozačima ili Raw Input. Promena fokusa se i dalje može blokirati Windows; greška se sada zove ručno aktiviranje ciljnog prozora. Centralni II elementi su dodati prevode u svim 27 katalozima; detaljni novi tekstovi i stručni prevodi su još uvek otvoreni. Živa kvalitet sa odgovarajućim instrukcionim modelima, trajnom upotrebom i merenim vremenom nije dokazana slučajnim / falsifikovanim HTTP testovima.

## Izvori

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Finalni test: Izgradnja izdanja bez upozorenja / grešaka; potpuni nuklearni i Windows suit, uključujući indijanski fokus fiksiranje, HTTPS, UIA, Plugin IPC i kompatibilne II transportne testove. Novi paket izlazak20261012-190618: WPF-Smuke (19:07 sat), povezan ONNX CPU inferens sa testagraf je izdržao. Nezavisni pregled: Unikalni protokol granica ispravljen, nema više konkretnih otkrića. Nema fizičkog proizvođača / telefona / monitora od toga.

## Nastavljanje: stvarni model kvaliteta i obrasci

Dodatni CPU pokretanje sa allama-ai/SmolLM-360M-Instruct-onnx-int4, revizija 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: indijanski inferencija. Modellgewichte-SHA256: 48e9c4ef12f7f995ka11fc7020b39990a27bc67cC0fe116067676a000a04429. Eksport zajednice samo u testu, a ne u izdanju.

Četiri stvarne testove planiranja (germanski/angliski, dokument, program, eksplicitna mikrofonska zvučna slagalica): **0/4 Važeći planovi**; JsonException u slučaju strogog ispitivanja. 63501, 6376, 72118 i 6774 Ovaj mali model ne ispunjava ugovor o planiranju i ne preporučuje se kao odgovarajući. izveštaj: artifakt/lokal-ai-ocena/smolm360-rezultat.: Windows-Testovi --ocenite-onks <modelski raspored> <reportažni put>. funkcija trajanja ne dokazuje kvalitet modela.

Instrumentalni navigacija i izveštaji o ispitivanju su dodatno prevedeni u svim 27 jezičkim katalozima. Dugotrajni tekstovi i jezički prekid ostaju otvoreni. Regresija šablona takođe ispituje lažne JSON tipove, prazne / višestruke / dugotrajne šablone i nule sa Jinga Fallback.

Ažurirani paket: artifakti / release-20261012-201134 Pakovani WPF su izdržljivi na 12.10.2026 oko 20:12; indijanski CPU inferens sa odvojenim instrukt test model u kombinovanom radniku. Testovi Core i Windows II su prošli; Izgradnja bez upozorenja / grešaka. Model planiranja kvaliteta nastavlja 0/4.

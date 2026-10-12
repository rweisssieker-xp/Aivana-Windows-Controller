<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokalni vremenski uvjeti

## Služba

Otvorite alat za razgovor s AI-om, izričito aktivirajte i odaberite vrijeme trajanja. Razdvajanje resursa, postavljanje cilja, stvaranje dizajna i provjera u uredniku. Niti jedan program ne provodi projekt automatski. Ne postoji automatski promjena.

| Vrijeme rada | Predviđanje | Povezivanje |
|---|---|---|
| Ollama | Lokalno instalirani model | 127.0.0.1:11434 |
| LM Studio | lokalni server s lokalnim modelom i JSON-ovim sustavom podrške | 127.0.0.1:1234 |
| llama.cpp | Grafički server s lokalnim modelom i JSON-ovim sustavom | 127.0.0.1:8080 |
| ONNX vrijeme rada GenAI CPU | kompatibilan, pouzdan lokalni model GenAI | Odvojeni proces, a ne HTTP |

HTTP klijenti ne koriste proxy i ne slijede smjernice. Lokalni poslužitelji mogu pristupiti drugim uslugama, a posebno LM Link/Remote Routing se ne može koristiti za lokalnu obradu. To ne može vjerodostojno prepoznati MAKRIVO kroz kompatibilni sučelje. Ollama daljinski podaci i imena u oblaku dodatno se odbijaju. Ovjereni lokalni server nije konfiguriran u ovoj verziji.

## ONNX

U dijalogu odaberite genai_config.json. Dosje mora sadržavati tokenizer.json i šablonu za razgovor. Podržava se chat_template kao znakovni lanac, nazvan default predlog (Array ili Dictionary) kao i odvojen chat_t template.jinja ako nema inline predlog ili je nula. Odbijeni su višestruki uzorci; maksimalno 65536 znakova. Popularni .onnx datoteke, modeli vizualnih/kombinativnih ili neto GPU/NPU izvoza nisu susretni modeli.

Uključeno: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Konfiguracija pružatelja se vraća na CPU. Nema preuzimanja i nema težine u paketu. Nema potrebe za Python instaliranjem. Koristite samo pouzdane pakete modela; odvojen proces je ograničenje razbijanja, a ne sigurnosna sandbox.

Ograničenja: 90 sekundi, uključujući modelno punjenje, maksimalni 3072 tokeni i 1024 generirani tokeni, dodatni kontekstni granica modela; strogo provjereni JSON i poznati ID resursa. Pogrešan/odbijeni odgovor je odbijen. Završetak/odbacivanje završava pokrenut radni proces. Nema slobodnih zapovijedi modela i programa/vrata datoteke u odvojenom popisu resursa Prompta; ciljni tekst koji je napisao korisnik može sadržavati takve podatke.

## Verifikacija

- Core-Suite, Loopback transportni test s simuliranim LM-studio/llama.cpp/Ollama odgovorima, raspored/pregled resursa.
- Unicode regresija s maksimalnim dugim ciljevima i 24 imena resursa.
- Prava native CPU referenca s službenim tiny-random-gpt2-fp32-Testgraph iz microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testna predloga dopunjuje i prilagođava tokenizator na malom slučajnom grafiku. Ovaj slučajni model ne provjerava kvalitetu i nije u izdanju.
- Reproduktivni natječajni test: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-modeli>.
- WPF-Smoke provjerava izbor svih četiri pružatelja i razinu ispitivanja; samo izbor ne stvara potražnju za modelom.

## Ostali prazni otpuštanja

I dalje su potrebni testovi za promjenu hardvera/smartfona/tableta/monitora. Ekskluzivno zatvaranje tipkovnice ostaje blokirano bez potpisane, licencirane i provjerene filtre. Ne zamjenjuje se gumom ili Raw Input. Promjena fokusiranja i dalje može biti blokirana Windows; sada se pogreška naziva ručno aktiviranje ciljne prozore. Središnji elementi AI-a dodani su prijevodima u svim 27 katalogima; detaljni novi tekstovi i stručni prijevodi još uvijek su otvoreni. Životni kvalitet s odgovarajućim modelom instrukcije, trajnim radom i mjerenim vremenskim dobitkom nije dokazano slučajnim modelom/fake HTTP testiranjem.

## izvori

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Konačni test: izbacivanje bez upozorenja / pogrešaka; potpuni Core i Windows suiti, uključujući Native Focus Fixture, HTTPS, UIA, Plugin IPC i kompatibilne testove prijevoza AI. Novi paket oslobađanja20261012-190618: WPF smoke (19:07 sat), povezan ONNX CPU reference s test graf. Neovisni pregled: Unicode protokola granica ispravljena, nema više konkretnih otkrića. Nema fizičkog proizvođača / telefona / monitora od toga.

## Sljedeći članak: Pravi modeli i kvalitete

Dodatni procesor s allama-ai/SmolLM-360M-Instruct-onnx-int4, revizija 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native infertility. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Izvoz zajednice samo u testnom području, a ne u izdanju.

Četiri stvarne testove planiranja (na njemačkom/engleskom jeziku, dokument, program, izričito mikrofonski zvuk): **0/4 Valjani planovi**; U svakom slučaju, JsonException u slučaju stroge ispitivanja odgovora. 63501, 6376, 72118 i 6774 ms. Nema akcija. ovaj mali model ne ispunjava ugovor o planiranju i ne preporučuje se kao prikladno. izvješće: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Testovi --evaluate-onnx <Modelni red> <Raportni put>. funkcija trajanja ne dokazuje kvalitetu modela.

Navigacija alata i izvješća o ispitivanju dodatno su prevedeni u svim 27 jezikovnim katalogima. Dugački tekstovi i jezični prijem ostaju otvoreni. Regresija predloga također provjerava lažne JSON vrste, prazne/mjestačne/prolazeće predale i nula s Jinja-Fallback.

Aktualizirani paket: Artifacts/release- 20261012-201134 Pakirani WPF smoke su se nalazili na 12.10.2026 oko 20:12; native CPU reference s odvojenim testnim modelom u kombiniranom radniku. Core i Windows testovi su prošli; Izgraditi bez upozorenja / pogreške. Kvaliteta planiranja nastavlja se 0/4.

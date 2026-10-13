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
| ONNX Runtime GenAI CPU | kompatibilan, pouzdan lokalni model GenAI | Odvojeni proces, a ne HTTP |

HTTP klijenti ne koriste proxy i ne slijede smjernice. Lokalni poslužitelji mogu pristupiti drugim uslugama, a posebno LM Link/Remote Routing se ne može koristiti za lokalnu obradu. To ne može vjerodostojno prepoznati MAKRIVO kroz kompatibilni sučelje. Ollama daljinski podaci i imena u oblaku dodatno se odbijaju. Ovjereni lokalni server nije konfiguriran u ovoj verziji.

## ONNX

U dijaloškom okviru odaberite genai_config.json. Mapa mora sadržavati tokenizer.json i predložak razgovora. Podržani su chat_template kao niz, imenovani predložak default (polje ili rječnik) te zasebni chat_template.jinja ako nema ugrađenog predloška ili je null. Dvosmisleni predlošci odbijaju se; najviše 65536 znakova. Proizvoljne datoteke .onnx, modeli vida/vektorskih ugradnji ili izvozi samo za GPU/NPU nisu modeli za sastanke.

Priloženo: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Konfiguracija pružatelja vraća se na CPU. Nema preuzimanja ni težina modela u paketu. Instalacija Python nije potrebna. Koristite samo pouzdane pakete modela; zaseban proces granica je za rušenje/otkazivanje, a ne sigurnosni sandbox.

Ograničenja: 90 sekundi uključujući učitavanje modela, najviše 3072 ulazna tokena i 1024 generirana tokena, dodatno ograničeno kontekstom modela; strogo provjeren JSON i poznati identifikatori resursa. Neispravan/odrezan odgovor odbacuje se. Zatvaranje/otkazivanje završava pokrenuti radni proces. Nema proizvoljnih naredbi modela ni programa/putanja datoteka u zasebnom popisu resursa upita; korisnički tekst cilja može sam sadržavati takve podatke.

## Verifikacija

- Core-Suite, Loopback transportni test s simuliranim LM-studio/llama.cpp/Ollama odgovorima, raspored/pregled resursa.
- Regresijski test protokola Unicode s ciljevima najveće dopuštene duljine i 24 naziva resursa.
- Stvarno izvorno izvođenje na CPU sa službenim testnim grafom tiny-random-gpt2-fp32 iz microsoft/onnxruntime-genai, revizija 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Dodan je testni predložak, a bajtni tokenizator prilagođen malom nasumičnom grafu. Taj nasumični model ne provjerava kvalitetu i nije u izdanju.
- Reproduktivni natječajni test: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-modeli>.
- WPF-Smoke provjerava izbor svih četiri pružatelja i razinu ispitivanja; samo izbor ne stvara potražnju za modelom.

## Ostali prazni otpuštanja

I dalje su potrebni testovi za promjenu hardvera/smartfona/tableta/monitora. Ekskluzivno zatvaranje tipkovnice ostaje blokirano bez potpisane, licencirane i provjerene filtre. Ne zamjenjuje se gumom ili Raw Input. Promjena fokusiranja i dalje može biti blokirana Windows; sada se pogreška naziva ručno aktiviranje ciljne prozore. Središnji elementi AI-a dodani su prijevodima u svim 27 katalogima; detaljni novi tekstovi i stručni prijevodi još uvijek su otvoreni. Životni kvalitet s odgovarajućim modelom instrukcije, trajnim radom i mjerenim vremenskim dobitkom nije dokazano slučajnim modelom/fake HTTP testiranjem.

## izvori

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Konačno stanje provjere: izgradnja Release bez upozorenja/pogrešaka; cijeli skupovi testova Core i Windows uspješni, uključujući izvorno testno okruženje fokusa, HTTPS, UIA, Plugin-IPC i kompatibilne AI transportne testove. Novi paket release-20261012-190618: WPF-Smoke uspješan (19:07), priloženo ONNX CPU izvođenje s testnim grafom uspješno. Neovisni pregled: ispravljena granica protokola Unicode, bez daljnjih konkretnih nalaza. Iz toga se ne izvodi fizičko prihvatno testiranje uređaja proizvođača/telefona/monitora.

## Nastavak: stvarna kvaliteta modela i predlošci

Dodatni CPU test s allama-ai/SmolLM-360M-Instruct-onnx-int4, revizija 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: izvorno izvođenje modela uspjelo je. SHA256 težina modela: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Izvoz zajednice samo je u testnom području, a ne u izdanju.

Četiri stvarna testa planiranja (njemački/engleski, dokument, program, izričito utišavanje mikrofona): **0/4 valjana plana**; svaki put JsonException pri strogoj provjeri odgovora. Vremena izvođenja 63501, 6376, 72118 i 6774 ms. Nijedna radnja nije izvršena. Ovaj mali model ne zadovoljava zahtjeve planiranja i ne preporučuje se kao prikladan. Izvješće: artifacts/local-ai-evaluation/smollm360-results.json. Reprodukcija: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Funkcioniranje izvršnog okruženja ne dokazuje kvalitetu modela.

Navigacija alata i izvješće provjere dodatno su prevedeni u svih 27 jezičnih kataloga. Dugi tekstovi i jezično odobrenje ostaju otvoreni. Regresijski test predložaka provjerava i pogrešne tipove JSON, prazne/dvosmislene/preduge predloške i null sa zamjenskim predloškom Jinja.

Ažurirani paket: artifacts/release-20261012-201134. Zapakirani WPF-Smoke uspješno je provjeren 12.10.2026 u 20:12; izvorno CPU izvođenje zasebnog testnog modela Instruct u priloženom radnom procesu uspjelo je. Testovi Core i Windows za AI uspjeli su; izgradnja bez upozorenja/pogrešaka. Kvaliteta planiranja modela i dalje je 0/4.

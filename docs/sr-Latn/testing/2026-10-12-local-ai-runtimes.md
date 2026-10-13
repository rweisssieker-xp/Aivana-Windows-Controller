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
| ONNX Runtime GenAI CPU | kompatibilni, pouzdani lokalni GenAI tekstualni model | Odvojeni povezani proces, a ne HTTP |

HTTP klijenti ne koriste proxy i ne prate smernice. Lokalni serveri mogu da se odnose na druge usluge; posebno LM link / daljinski routing ne može da se koristi za lokalnu obradu. Ovo MAKRIVO ne može pouzdano da otkrije kompatibilan interfejs. Ollama udaljeni podaci i imena oblaka su dodatno odbijeni. Autentifikovani lokalni serveri nisu konfigurisani u ovoj verziji.

## ONNX

U dijalogu izaberite genai_config.json. Fascikla mora da sadrži tokenizer.json i šablon razgovora. Podržani su chat_template kao string, imenovani šablon default (niz ili rečnik) i zaseban chat_template.jinja ako nema ugrađenog šablona ili je null. Dvosmisleni šabloni se odbijaju; najviše 65536 znakova. Proizvoljne datoteke .onnx, modeli vida/vektorskih reprezentacija ili izvozi samo za GPU/NPU nisu modeli za sastanke.

Priloženo: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Konfiguracija pružaoca vraća se na CPU. Nema preuzimanja ni težina modela u paketu. Instalacija Python nije potrebna. Koristite samo pouzdane pakete modela; zaseban proces je granica za rušenje/otkazivanje, a ne bezbednosni sandbox.

Ograničenja: 90 sekundi uključujući učitavanje modela, najviše 3072 ulazna tokena i 1024 generisana tokena, dodatno ograničeno kontekstom modela; strogo proveren JSON i poznati identifikatori resursa. Neispravan/odsečen odgovor se odbacuje. Zatvaranje/otkazivanje završava pokrenuti radni proces. Nema proizvoljnih komandi modela ni programa/putanja datoteka u zasebnoj listi resursa upita; korisnički tekst cilja može sam da sadrži takve podatke.

## Provera

- Core Suite, Loopback Transport Tests with Simulated LM Studio/llama.cpp/Ollama Reactions, Shema/Otvaranje/Istraživanje resursa.
- Regresioni test protokola Unicode sa ciljevima najveće dozvoljene dužine i 24 imena resursa.
- Stvarno nativno izvođenje na CPU sa zvaničnim testnim grafom tiny-random-gpt2-fp32 iz microsoft/onnxruntime-genai, revizija 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Dodat je testni šablon, a bajtni tokenizator prilagođen malom nasumičnom grafu. Taj nasumični model ne proverava kvalitet i nije u izdanju.
- Reproduktivni test za pokretanje: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI-Model Order>
- WPF-Smoke ispituje izbor svih četiri provajdera i ispitivanje; izbor sam ne stvara zahtev za model.

## Ostali stvarni otpuštaj

Testovi za promenu hardvera / pametnog telefona / tableta / monitora i dalje su potrebni. Ekskluzivno pritisak tastature ostaje blokiran bez potpisanih, licenciranih i proverenih filtera. Ne zamenjuje se gumenim vozačima ili Raw Input. Promena fokusa se i dalje može blokirati Windows; greška se sada zove ručno aktiviranje ciljnog prozora. Centralni II elementi su dodati prevode u svim 27 katalozima; detaljni novi tekstovi i stručni prevodi su još uvek otvoreni. Živa kvalitet sa odgovarajućim instrukcionim modelima, trajnom upotrebom i merenim vremenom nije dokazana slučajnim / falsifikovanim HTTP testovima.

## Izvori

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Konačno stanje provere: izgradnja Release bez upozorenja/grešaka; celi skupovi testova Core i Windows uspešni, uključujući nativno testno okruženje fokusa, HTTPS, UIA, Plugin-IPC i kompatibilne AI transportne testove. Novi paket release-20261012-190618: WPF-Smoke uspešan (19:07), priloženo ONNX CPU izvođenje sa testnim grafom uspešno. Nezavisni pregled: ispravljena granica protokola Unicode, bez daljih konkretnih nalaza. Iz toga se ne izvodi fizičko prihvatno testiranje uređaja proizvođača/telefona/monitora.

## Nastavak: stvarni kvalitet modela i šabloni

Dodatni CPU test sa allama-ai/SmolLM-360M-Instruct-onnx-int4, revizija 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: nativno izvođenje modela je uspelo. SHA256 težina modela: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Izvoz zajednice je samo u testnom području, a ne u izdanju.

Četiri stvarna testa planiranja (nemački/engleski, dokument, program, izričito utišavanje mikrofona): **0/4 važećih planova**; svaki put JsonException pri strogoj proveri odgovora. Vremena izvođenja 63501, 6376, 72118 i 6774 ms. Nijedna radnja nije izvršena. Ovaj mali model ne zadovoljava zahteve planiranja i ne preporučuje se kao prikladan. Izveštaj: artifacts/local-ai-evaluation/smollm360-results.json. Reprodukcija: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Funkcionisanje izvršnog okruženja ne dokazuje kvalitet modela.

Navigacija alata i izveštaj provere dodatno su prevedeni u svih 27 jezičkih kataloga. Dugi tekstovi i jezičko odobrenje ostaju otvoreni. Regresioni test šablona proverava i pogrešne tipove JSON, prazne/dvosmislene/preduge šablone i null sa zamenskim šablonom Jinja.

Ažurirani paket: artifacts/release-20261012-201134. Zapakovani WPF-Smoke uspešno je proveren 12.10.2026 u 20:12; nativno CPU izvođenje zasebnog testnog modela Instruct u priloženom radnom procesu je uspelo. Testovi Core i Windows za AI su uspeli; izgradnja bez upozorenja/grešaka. Kvalitet planiranja modela i dalje je 0/4.

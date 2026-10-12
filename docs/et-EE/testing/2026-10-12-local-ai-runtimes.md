<!-- generated-doc locale=et-EE source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../testing/2026-10-12-local-ai-runtimes.md)

# Kohalikud AI-tunnid

## Teenindus

Tööriistad → Avatud AI-sõna assistent, selgesõnaliselt aktiveerida ja valida AI-tähtaeg. Ressursside jagamine, eesmärgi vormistamine, disain ja redigeeri kontrollimine. Ükski pakkuja ei tööta automaatselt. Automaatne tarnija vahetamine ei ole.

| Käivitusajaga | Eeltingimus | ühendus |
|---|---|---|
| Ollama | Kohalik paigaldatud mudel | 127.0.0.1:11434 |
| LM Studio | kohalik server, millel on kohalik laaditud mudel ja JSONi skeemi tugi | 127.0.0.1:1234 |
| llama.cpp | käimasolev llama server, millel on kohalik mudel ja JSONi skeemi tugi | 127.0.0.1:8080 |
| ONNX käivitusaeg GenAI CPU | Ühilduv, usaldusväärne kohalik GenAI teksti mudel | Eraldi seotud protsess, mitte HTTP |

HTTP kliendid ei kasuta proksid ja ei jälgi edasisid suuniseid. Kohalikud serverid võivad kasutada ka teisi teenuseid; eelkõige LM Link/Remote Routing ei tohi kasutada kohapealseks töötlemiseks. See ei saa MAKRIVO ühilduva liidese kaudu usaldusväärselt tuvastada. Ollama kaugandmed ja pilvnimed jäetakse välja. Autentsed kohalikud serverid ei ole selles versioonis konfigureeritavad.

## ONNX

Dialoogis valige genai_config.json. Kaardil peab olema tokenizer.json ja vestlusvormi. Toetatakse chat_template kui märgikett, nimetatud standardvormi (array või sõnaraamat) ja eraldi chat_t template.jinja, kui see ei ole olemas või see on null. Mitmekülgsed näidised on keeldutud; maksimaalselt 65536 märgid. Populaarsed .onnx failid, nägemis- ja kaasamudelid või puhtad GPU/NPU ekspordid ei ole kohtumismudelid.

Ühendatud: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Pakkuja konfigureerimine tagastatakse CPU-le. Ei ole allalaadimist ega mudeli kaalu pakendis. Ei ole vaja Python installimist. Kasutage ainult usaldusväärseid mudelipakete; eraldi protsess on katkestamise/katkestamise piir, mitte turvavöö.

Piirangud: 90 sekundid, mis hõlmavad mudeli laadimist, maksimaalselt 3072 promptooke ja 1024 genereeritud tokenid, lisaks mudeli konteksti piir; rangelt kontrollitud JSON ja tuntud ressursside ID. Vabastatud/vabastatud vastus on keeldutud. Sulgemine/lülitamine lõpetab algatatud tööprotsessi. Ei ole vaba mudeli käsku ega programmi/faili teed Prompti eraldi ressursside loendis; kasutaja poolt kirjutatud sihttekst võib selliseid andmeid ise sisaldada.

## Kontrollimine

- Core-Suite, Loopback transpordi testid simuleeritud LM-studio-/llama.cpp-/Ollama vastused, skeem/lülitus/ressursside kontrollimine.
- Unicode-protokollregressioon maksimaalsete eesmärkide ja 24 ressursside nimedega.
- Tõeline native CPU infosüsteem ametliku tiny-random-gpt2-fp32-Testgraph Microsofti/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testiproovi täiendamine ja byte tokenizer kohandatud väikese juhuslik graaf. See juhuslik mudel ei kontrolli kvaliteeti ja ei ole vabastamisel.
- Reproduktiivne native koormuse testi: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI mudeli režiim>.
- WPF-Smoke kontrollib kõiki nelja pakkuja valikut ja kontrollitavat seisundit; valik üksi ei tekita mudeli nõudlust.

## Ülejäänud tõelised vabadused

Hardware/Smartphone/Tablet/Monitor muutus on jätkuvalt vajalik. Eksklusiivne klaviatuuri lülitamine jääb ilma allkirjastatud, litsentseeritud ja kontrollitud filtreerimata. Ei asendata kummituste või Raw Input abil. Keskendumismuutust saab jätkuvalt blokeerida Windows; viga nimetatakse nüüd sihtpinda manuaalseks aktiveerimiseks. Kesksed AI-eelemendid on lisatud kõigis 27 kataloogides tõlked; üksikasjalikud uued tekstid ja spetsialiseerunud tõlke puudused on veel avatud. Live-kvaliteet, millel on asjakohane juhendi mudel, püsiv töötlemine ja mõõdetud aja kasum ei ole tõestatud juhuslikuks mudeliks / võltsitud HTTP-testideks.

## Allikad

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Lõplik test: Release-Build ilma hoiatuste/veadeta; täielik Core ja Windows suites, sealhulgas native Focus Fixture, HTTPS, UIA, Plugin IPC ja ühilduvad AI transpordi testid. Uus pakett vabastamine20261012-190618: WPF-Smoke kestis (19:07 tund), ühendatud ONNX-CPU infektsioon testgraph kestis. Sõltumatu ülevaade: Unicode protokoll piirmäära parandatud, ei ole rohkem konkreetseid avastusi. Sellest ei tulene füüsiline tootja/telefon/monitor.

## Järgmine: tõeline mudel kvaliteet ja mudelid

Täiendav CPU käivitamine allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native infertsioon. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Ühenduse eksportimine ainult katsetamispiirkonnas, mitte vabastamisel.

Nelja tegelikku planeerimistesti ( saksa/inglise, dokumendi, programmi, selge mikrofon): **0/4 kehtivad plaanid**; JsonException rangelt vastuse kontrollimisel. 63501, 6376, 72118 ja 6774 See väike mudel ei vasta planeerimislepingule ja seda ei soovitata sobivaks. aruanded: artifacts/local-ai-evaluation/smollm360-results.json.: Windows-Testid --evaluate-onnx <Modeli režiim> <Raportti tee>..

Tööriistade navigatsioon ja testide aruanded on tõlgitud kõigis 27 keelekataloogides. Pikad tekstid ja keelekogumised jäävad avatuks. Kaardimängude regressioon kontrollib ka vale JSON-tüüpe, tühja/mõõduka/pikkuse mudelid ja null Jinja Fallbackiga.

Uuendatud pakett: artifaktid/release-20261012-201134 Pakendatud WPF paksud 12.10.2026 umbes 20:12; kodumaine CPU infektsioon koos eraldi Instruct testimudel ühendatud töötaja. Core ja Windows KI testid läbi; ehitada ilma hoiatuste/veadeta. Mudeli kvaliteet jätkub 0/4.

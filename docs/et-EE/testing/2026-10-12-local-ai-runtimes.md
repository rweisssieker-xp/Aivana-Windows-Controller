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

Valige dialoogis genai_config.json. Kaust peab sisaldama tokenizer.json faili ja vestlusmalli. Toetatud on chat_template stringina, nimega default-mall (massiiv või sõnastik) ning eraldi chat_template.jinja, kui sisseehitatud mall puudub või on null. Mitmetähenduslikud mallid lükatakse tagasi; piirang on 65536 märki. Suvalised .onnx-failid, nägemis-/vektorimudelid või ainult GPU-/NPU-ekspordid ei ole koosolekumudelid.

Ühendatud: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Pakkuja konfigureerimine tagastatakse CPU-le. Ei ole allalaadimist ega mudeli kaalu pakendis. Ei ole vaja Python installimist. Kasutage ainult usaldusväärseid mudelipakete; eraldi protsess on katkestamise/katkestamise piir, mitte turvavöö.

Piirangud: 90 sekundid, mis hõlmavad mudeli laadimist, maksimaalselt 3072 promptooke ja 1024 genereeritud tokenid, lisaks mudeli konteksti piir; rangelt kontrollitud JSON ja tuntud ressursside ID. Vabastatud/vabastatud vastus on keeldutud. Sulgemine/lülitamine lõpetab algatatud tööprotsessi. Ei ole vaba mudeli käsku ega programmi/faili teed Prompti eraldi ressursside loendis; kasutaja poolt kirjutatud sihttekst võib selliseid andmeid ise sisaldada.

## Kontrollimine

- Core-Suite, Loopback transpordi testid simuleeritud LM-studio-/llama.cpp-/Ollama vastused, skeem/lülitus/ressursside kontrollimine.
- Unicode-protokolli regressioonitest maksimaalse pikkusega eesmärkide ja 24 ressursinimega.
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

Lõplik kontrolliseis: Release-Build ilma hoiatuste/vigadeta; täielikud Core ja Windows testikomplektid läbitud, sealhulgas natiivse fookuse testkeskkond, HTTPS, UIA, Plugin-IPC ja ühilduvate AI-transpordiliideste testid. Uus pakett release-20261012-190618: WPF-Smoke läbitud (19:07), kaasasoleva ONNX-CPU inferents testgraafiga läbitud. Sõltumatu ülevaatus: Unicode-protokolli piir parandatud, muid konkreetseid leide ei olnud. Sellest ei järeldata füüsilise tootjariistvara/telefoni/monitori heakskiitu.

## Järgmine: tõeline mudel kvaliteet ja mudelid

Täiendav CPU-käivitus mudeliga allama-ai/SmolLM-360M-Instruct-onnx-int4, revisjon 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: natiivne inferents läbitud. Mudelikaalude SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Kogukonna eksport on ainult testialas, mitte väljalaskes.

Neli tegelikku planeerimistesti (saksa/inglise keeles, dokument, programm, selgesõnaline mikrofoni vaigistamine): **0/4 kehtivat plaani**; iga kord tekkis vastuse rangel kontrollimisel JsonException. Käitusajad olid 63501, 6376, 72118 ja 6774 ms. Ühtegi toimingut ei käivitatud. See väike mudel ei vasta planeerimislepingule ja seda ei soovitata sobivana. Aruanne: artifacts/local-ai-evaluation/smollm360-results.json. Kordamine: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Käituskeskkonna toimimine ei tõenda mudeli kvaliteeti.

Tööriistade navigeerimine ja kontrolliaruanne tõlgiti lisaks kõigis 27 keelekataloogis. Pikad tekstid ja keeleline heakskiit on endiselt lõpetamata. Mallide regressioonitest kontrollib ka valesid JSON-tüüpe, tühje/mitmetähenduslikke/liiga pikki malle ning null-väärtust koos Jinja-varulahendusega.

Uuendatud pakett: artifacts/release-20261012-201134. Pakendatud WPF-Smoke läbitud 12.10.2026 kell 20:12; natiivne CPU-inferents eraldi Instruct-testmudeliga komplektis olevas abiprotsessis läbitud. Core ja Windows-KI testid läbitud; ehitus ilma hoiatuste/vigadeta. Mudeli planeerimiskvaliteet endiselt 0/4.

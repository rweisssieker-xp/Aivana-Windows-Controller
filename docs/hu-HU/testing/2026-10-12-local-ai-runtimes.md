<!-- generated-doc locale=hu-HU source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../testing/2026-10-12-local-ai-runtimes.md)

# Helyi KI-idő

## Szolgáltatás

Eszközök → AI megbeszélés asszisztens megnyitása, kifejezetten aktiválja és kiválasztja az AI időtartamát. erőforrások közzététele, a célok meghatározása, a tervezés létrehozása és a szerkesztőben történő vizsgálata. Nincs szolgáltató, amely automatikusan végrehajtja a tervezést. Nincs automatikus szolgáltatócsere.

| Folytatási idő | Előfeltételek | Kapcsolatok |
|---|---|---|
| Ollama | Helyi telepített modell | 127.0.0.1:11434 |
| LM Studio | működő helyi szerverek, helyi töltött modell és JSON rendszer támogatása | 127.0.0.1:1234 |
| llama.cpp | működő llama szerver a helyi modell és a JSON rendszer támogatásával | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | kompatibilis, megbízható helyi GenAI szövegmodellrendszer | Külön összefüggő folyamat, nem HTTP |

A HTTP-kliensek nem használnak proxy-t, és nem követnek továbbításokat. a helyi szerverek még további szolgáltatásokat is igényelhetnek; különösen az LM Link/Remote Routing nem használható helyi feldolgozáshoz. MAKRIVO a kompatibilis felület nem megbízható. Ollama-Távoli adatokat és felhőneveket adnak ki. Az akkreditált helyi szerverek ebben a verzióban nem konfigurálhatók.

## ONNX

A párbeszédablakban válassza a genai_config.json fájlt. A mappának tartalmaznia kell a tokenizer.json fájlt és egy csevegősablont. Támogatott a szöveges chat_template, egy névvel rendelkező default-sablon (tömb vagy szótár), továbbá külön chat_template.jinja, ha a beágyazott sablon hiányzik vagy null. A többértelmű sablonokat elutasítja; legfeljebb 65536 karakter. Tetszőleges .onnx fájlok, képfeldolgozó/beágyazási modellek vagy kizárólag GPU-/NPU-exportok nem értekezleti modellek.

Összefüggésben: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. A szolgáltató konfigurációja a CPU-ra helyezkedik vissza. Nincs letöltés és a modell súlyai a csomagban. Nincs Python telepítés szükséges. Csak megbízható modellcsomagok használata; A külön folyamat egy bukás/baleset határ, nincs biztonsági szendvics.

Határok: 90 másodperc, beleértve a modell töltés, maximális 3072 Prompttokens és 1024 generált tokens, kiegészítő modell kontextus határa; szigorúan ellenőrzött JSON és ismert erőforrás-azonosító. hibás/visszatérített válasz elutasítják. zárja/visszatérítse be a megkezdett munkafolyamatot. Nincs szabad modell parancs és nincs program/fájl út a Promptt külön erőforráslistájában; a felhasználó által írt cél szöveg magában foglalhatja az ilyen adatokat.

## Ellenőrzés

- Core-Suite, Loopback szállítási tesztek szimulált LM stúdió-/llama.cpp-/Ollama válaszokkal, diagram / indulás / erőforrásvizsgálat.
- Unicode-protokollregresszió maximális hosszúságú célokkal és 24 erőforrásnévvel.
- Valódi ősi CPU-bejegyzés hivatalos tiny-random-gpt2-fp32-Testgraph a microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. tesztmintát kiegészítve és a bajt tokenizer a kis véletlen grafikon. Ez a véletlen modell nem vizsgálja a minőséget, és nem a kiadásban.
- Reproduktív hazai futó teszt: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI modellek>.
- WPF-Smoke vizsgálja a négy szolgáltató kiválasztását és a vizsgálati állapotot; a kiválasztás egyedül nem termel modellkérelmet.

## A fennmaradó igazi szabadság

A hardver/smartphone/tabletta/monitor cseréje továbbra is szükséges. A billentyűzet kizárólagos megnyomása aláírt, engedélyezett és ellenőrzött szűrővezetők nélkül marad. Nem helyettesíthető a ködvezetőkkel vagy Raw Input. A fókuszcsere továbbra is blokkolható Windows; a hiba most a cél ablak manuális aktiválását jelenti. A központi AI-t a fordítások minden 27 katalógusban hozzáadhatók; részletes új szövegek és szakmai fordítási hiányosságok még mindig nyitva állnak.

## Források

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Végső ellenőrzési állapot: Release-build figyelmeztetés és hiba nélkül; a teljes Core- és Windows-tesztcsomag sikeres, beleértve a natív fókuszfixture-t, HTTPS-t, UIA-t, Plugin-IPC-t és a kompatibilis KI-átviteli teszteket. Új csomag release-20261012-190618: WPF-Smoke sikeres (19:07), a mellékelt ONNX-CPU-következtetés tesztgráffal sikeres. Független felülvizsgálat: a Unicode-protokollhatár javítva, további konkrét megállapítás nélkül. Ebből nem következik fizikai gyártói hardveres, telefonos vagy monitoros átvétel.

## Folytatás: Valódi modellminőség és minták

További CPU-futtatás az allama-ai/SmolLM-360M-Instruct-onnx-int4 modellel, revízió: 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: a natív következtetés sikeres. Modellsúlyok Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. A közösségi export csak a tesztterületen szerepel, a kiadásban nem.

Négy valódi tervezési teszt (német/angol, dokumentum, program, kifejezett mikrofon-némítás): **0/4 érvényes terv**; mindegyiknél JsonException a szigorú válaszellenőrzés során. Futásidők: 63501, 6376, 72118 és 6774 ms. Nem hajtottak végre műveleteket. Ez a kis modell nem teljesíti a tervezési szerződést, és nem ajánlott megfelelő modellként. Jelentés: artifacts/local-ai-evaluation/smollm360-results.json. Reprodukció: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. A futtatókörnyezet működése nem igazolja a modell minőségét.

Az eszköznavigációt és az ellenőrzési jelentést ezenfelül mind a 27 nyelvi katalógusban lefordították. A hosszú szövegek és a nyelvi jóváhagyás még hátravannak. A sablonregresszió a hibás JSON-típusokat, az üres/többértelmű/túl hosszú sablonokat és a null értéket is ellenőrzi Jinja-tartalékkal.

Frissített csomag: artifacts/release-20261012-201134. A csomagolt WPF-Smoke 12.10.2026 napján 20:12-kor sikeres; a natív CPU-következtetés a külön Instruct-tesztmodellel sikeres a mellékelt munkafolyamatban. A Core és Windows-KI tesztek sikeresek; a build figyelmeztetés és hiba nélkül készült. A modell tervezési minősége továbbra is 0/4.

<!-- generated-doc locale=hu-HU source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Helyi KI-idő és maradék vizsgálatok

Cél: további helyi AI futtatási idő valóban csatlakoztatni, tervezési vizsgálatot kapni, és a nyitott kiadásokat láthatóvá tenni. A felhasználók engedélyezik a végrehajtást kérések nélkül.

1. A Loopback szállítás és a ONNX csomagvizsgálat tesztelése a végrehajtás előtt; a hálózati címek, a kibocsátás korlátai, a törések és az ismeretlen erőforrások ellenőrzése.
2. Közös tervező üzenetek; Ollama kap, LM Studio (127.0.0.1:1234) és llama.cpp (127.0.0.1:8080) kiegészíti. Nincs felhő URL-k vagy automatikus szolgáltató váltás. A helyi szerverek továbbíthatják magukat; azok működése láthatóan a szolgáltató felelőssége.
3. ONNX Runtime GenAI CPU a saját folyamat korlátozott időtartamú, korlátozott JSON protokoll és folyamatmegszakítás. modellek sorrend kifejezetten kiválasztva; nincs modell automatikusan letölthető. Csak GenAI kompatibilis szövegmodellek, nincs .onnx hálózat.
4. WPF kiválasztás a futamideje, modell, ONNX lapok; mindegyik ugyanazt a szigorúan ellenőrzött tervezés.
5. A hardver, a DPI, a mobilpanel és az exkluzív makropad számára egyértelmű korlátokkal rendelkező teszttartalom.A fizikai tesztelés és az aláírt/licencelt vezérlő nem helyettesíthető a szoftverrel.
6. Release-Build, Core/Windows tesztelés, WPF-Smoke, Worker/Package tesztelés és független felülvizsgálat. Új teljes csomag létrehozása; a modell minősége csak a valósan meglévő modell tesztelés.

Az eredmény: négy futamideje végrehajtott, ONNX nativ külön tesztelt, a szállító kiválasztása és a teszttartás a csomagolt WPF szivattyú. Core/Windows szivattyú zöld. felülvizsgálati egyedi korlátok közös állandó és regresszió. Valódi hardver/mobile/DPI váltás, vezérlő-kiadás, teljes szakmai fordítások és élő beállítások minőség maradnak kifejezetten nyitott munka; nem teljes mértékben kiadott.

Folytatás: külön jinja és kijelölt/default minták végrehajtott; típus/határ/felszabadulás regressziós vizsgálatok. eszköz navigáció kiegészíti a 27 katalógusok. valódi oktatási modell külön letöltve és ellenőrzött SHA256: a szülői CPU-információ működik, de 0/4 tervezési esetek megfelelnek a JSON szerződés; nem hajt végre cselekvéseket. független felülvizsgálat: null-felszabadulás kijavított és tesztelt, majd nem konkrét további találatok. Core és Windows KI-tesztek, kiadása 0 figyelmeztetések/hibák.

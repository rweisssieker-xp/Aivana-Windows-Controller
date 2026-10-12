<!-- generated-doc locale=sv-SE source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokala KI-tider och återstående tester

Målet är att faktiskt ansluta fler lokala AI-tider, få utformningskontroll och göra öppna publikationer synliga. Användaren tillåter genomförande utan förfrågningar. De befintliga förändringarna förblir.

1. Test för Loopback-transport och ONNX-paketkontroll före genomförandet; kontrollera nätverksadresser, utgifter, avbrott och okända resurser.
2. Gemensamma planerare; Ollama Hålls, LM Studio (127.0.0.1:1234) och llama.cpp (127.0.0.1:8080) Inga molnURL eller automatiska leverantörsväxlingar. Lokala servrar kan själv styra; deras drift förblir synlig ansvar för operatören.
3. ONNX Runtime GenAI CPU i egen process med tidsgräns, begränsad JSON-protokoll och processavbrott. Välj exakt modellordningen; ingen modell laddas ner automatiskt. Endast GenAI kompatibla textmodeller, ingen .onnx-nätverk.
4. WPF-val för löptid, modell, ONNX-filer; alla levererar samma noggrant granskade design.
5. Testplats med tydliga gränser för hårdvara, DPI, mobilpanel och exklusiv makro. Fysiska tester och en undertecknad/licensierad drivrutin kan inte ersättas med programvara här.
6. Release-Build, Core/Windows Test, WPF Smoke, Worker/Package Test och oberoende granskning. Skapa ett nytt komplett paket; rapportera modellkvalitet endast för den faktiska befintliga modellen som testat.

Resultatet: Fyra löptidar genomförda, ONNX nativ separat testad, leverantörsval och teststatus i förpackningen WPF-Rökning - Core/Windows-Suiter grön. recensioner Unicode gränser med gemensam konstant och regression åtgärdade. verkliga maskinvaru-/mobile/DPI bytesavdrag, drivrutiner, fullständig professionell översättning och live-instruktion kvalitet förblir uttryckligt öppet arbete; inte som helt utfärdat.

Fortsätt: separata Jinja- och namngivna/default-mönster genomförts; typ-/gräns-/fallback-regressionstester har genomgått. Navigation verktyg kompletteras i 27 kataloger. Verkliga instruktionsmodell separat nedladdade och kontrollerade SHA256: inhemska CPU-inferens fungerar, men 0/4 planeringsfall uppfyller JSON-avtalet; inga åtgärder utförs. Oberoende granskning: noll fallback korrigeras och testas, varefter inga konkreta ytterligare fynd. Core och Windows-KI-tester passerade, Release-Build 0 Varningar/fel.

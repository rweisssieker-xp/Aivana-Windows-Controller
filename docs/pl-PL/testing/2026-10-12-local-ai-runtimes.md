<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokalne środowiska wykonawcze AI

## Serwis

Narzędzia → Otwórz asystenta spotkań AI, aktywuj go jawnie i wybierz środowisko wykonawcze AI. Uwolnij zasoby, sformułuj cel, utwórz wersję roboczą i sprawdź w edytorze. Żaden dostawca nie uruchamia projektu automatycznie. Nie ma automatycznej zmiany dostawcy.

| Czas trwania | Wymaganie | połączenie |
|---|---|---|
| Ollama | model instalowany lokalnie | 127.0.0.1:11434 |
| LM Studio | działający serwer lokalny z lokalnie ładowanym modelem i obsługą schematu JSON | 127.0.0.1:1234 |
| llama.cpp | działający serwer lamy z obsługą modelu lokalnego i schematu JSON | 127.0.0.1:8080 |
| ONNX Środowisko wykonawcze GenAI Procesor | kompatybilny, zaufany lokalny folder modelu tekstowego GenAI | oddzielny proces w pakiecie, bez protokołu HTTP |

Klienci HTTP nie korzystają z serwerów proxy i nie korzystają z przekierowań. Lokalne serwery mogą same adresować dodatkowe usługi; W szczególności LM Link/Remote Routing nie może być używany do przetwarzania lokalnego. MAKRIVO nie może tego wiarygodnie wykryć za pomocą kompatybilnego interfejsu. Ollama zdalne metadane i nazwy chmur są dodatkowo odrzucane. W tej wersji nie można skonfigurować uwierzytelnionych serwerów lokalnych.

## ONNX

W dialogie genai_config.json wybierz. folder musi zawierać tokenizer.json i szablon czatu. chat_template jest obsługiwany jako łańcuch znaków, określony szablon podanego (Array lub Słownik) oraz oddzielny chat_template.jinja, jeśli nie ma szablonu w trybie lub jest to zero. 65536 Znaki. popularne pliki .onnx, modele wizji/włączenia lub czyste eksporty GPU/NPU nie są modelami spotkań.

W pakiecie: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, procesor. Konfiguracja dostawcy została zresetowana do procesora. Bez pobierania i bez ciężarów modeli w pakiecie. Nie wymaga instalacji Pythona. Używaj tylko pakietów zaufanych modeli; oddzielny proces stanowi granicę awarii/przerwania, a nie bezpieczną piaskownicę.

Limity: 90 sekundy obejmujące ładowanie modelu, maksymalna liczba tokenów podpowiedzi 3072 i wygenerowanych tokenów 1024, dodatkowy limit kontekstu modelu; rygorystycznie sprawdzane JSON i znane identyfikatory zasobów. Nieprawidłowa/skrócona odpowiedź zostanie odrzucona. Zamknij/Anuluj kończy rozpoczęty proces roboczy. Żadnych darmowych poleceń modelowych i żadnych programów/ścieżek plików na osobnej liście zasobów zachęty; tekst docelowy napisany przez użytkownika może sam zawierać takie dane.

## Weryfikacja

- Pakiet podstawowy, testy transportu pętli zwrotnej z symulowanymi odpowiedziami LM-Studio/llama.cpp/Ollama, sprawdzanie schematu/przerwania/zasobu.
- Regresja protokołu Unicode z maksymalnie długimi celami i nazwami zasobów 24.
- Prawdziwe natywne wnioskowanie o procesorze z oficjalnym tiny-random-gpt2-fp32-Testgraph z Microsoft/onnxruntime-genai, zatwierdź 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Dodano szablon testowy i dostosowano tokenizator bajtów do małego losowego wykresu. Ten losowy model nie sprawdza jakości i nie jest dostępny w sprzedaży.
- Powtarzalny natywny test środowiska wykonawczego: Tools/check-onnx-runtime.ps1 -ModelDirectory <folder modelu GenAI>.
- WPF-Smoke sprawdza wybór wszystkich czterech dostawców i stanowisko testowe; Sam wybór nie generuje żądania modelu.

## Pozostałe realne akcje

Nadal wymagane jest przetestowanie wymiany sprzętu/smartfona/tabletu/monitora. Ekskluzywne tłumienie klawiatury pozostaje zablokowane bez podpisanego, licencjonowanego i zweryfikowanego sterownika filtra. Nie zastąpione przez fikcyjne sterowniki lub Raw Input. Zmiana ostrości może być nadal blokowana przez Windows; błąd wspomina teraz o ręcznej aktywacji okna docelowego. Dodano tłumaczenia do centralnego sterowania AI we wszystkich katalogach 27; Szczegółowe nowe teksty i zatwierdzenie tłumaczeń technicznych są nadal otwarte. Jakość na żywo przy odpowiednim modelu instrukcji, ciągła praca i zmierzona oszczędność czasu nie są potwierdzane przez losowe modele/fałszywe testy HTTP.

## Źródła

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Test końcowy: wydanie kompilacji bez ostrzeżeń/błędów; Przeszedł pełny rdzeń i pakiet Windows, w tym natywne urządzenia fokusowe, HTTPS, UIA, wtyczki IPC i kompatybilne testy transportu AI. Nowa wersja pakietu-20261012-190618: WPF przeszła dym (zegar 19:07), w pakiecie ONNX wnioskowanie procesora z wykresem testowym przeszło. Niezależna recenzja: poprawiono limit protokołu Unicode, brak innych konkretnych ustaleń. Nie wynika z tego żadna fizyczna akceptacja producenta/telefonu/monitora.

## Kontynuacja: prawdziwa jakość i modele

Dodatkowe biegi CPU allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native Inference przetrwał. Modellgewichte-SHA25648e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429. eksportowanie społeczności tylko w strefie testowej, nie w wydaniu.

Cztery prawdziwe testy planowania (w języku niemieckim/ang. document, program, wyraźny mikrofon): **0/4 Istotne plany**; JsonException w przypadku rygorystycznego badania odpowiedzi. 63501, 6376, 72118 i 6774 ms. Nie wykonuje się żadnych działań. ten mały model nie spełnia umowy planowania i nie jest zalecany jako odpowiedni. raport: artifacts/local-ai-evaluation/smollm360-results.json. Windows-Test --evaluate-onnx <modelowy porządek> <porządek raportu>. funkcja czasu pracy nie potwierdza jakości modelu.

Narzędzia nawigacji i raporty testowe zostały dodatkowo w każdym 27 Tłumaczenie katalogów językowych. długie teksty i odbiór językowy pozostają otwarte. regresja szablonów sprawdza również fałszywe typy JSON, puste/wiele-długie/długie szablony i zero z Jinja Fallback.

Aktualizacja pakietu: Artifacts/Release-20261012-201134Pakiet WPFSmoke na 12.10.2026 O 20:12 przetrwał; native CPU referencyjnie z oddzielnym modelem testowym Instruct w połączonym Worker. Windows-Test KI przeszedł; buduje bez ostrzeżeń/błędu. jakość planowania modelu kontynuować 0/4.

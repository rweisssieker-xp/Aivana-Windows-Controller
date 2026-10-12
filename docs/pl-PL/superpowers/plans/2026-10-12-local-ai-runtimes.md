<!-- generated-doc locale=pl-PL source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokalne środowiska wykonawcze AI i pozostałe kontrole

Cel: faktycznie podłączyć dodatkowe lokalne środowiska wykonawcze AI, otrzymywać recenzje projektów i udostępniać widoczne wersje otwarte. Użytkownik autoryzuje wdrożenie bez pytań. Istniejące zmiany zostają zachowane.

1. Transport pętli zwrotnej i testy kontroli pakietów przed wdrożeniem ONNX; Sprawdź adresy sieciowe, limity wyjściowe, przerwanie i nieznane zasoby.
2. Udostępnione wiadomości planisty; Ollama otrzymano, LM Studio (127.0.0.1:1234) i llama.cpp (127.0.0.1:8080) dodano. Żadnych adresów URL w chmurze ani automatycznego przełączania dostawców. Lokalne serwery mogą same się przekazywać; Za ich obsługę odpowiada operator.
3. ONNX Runtime GenAI CPU we własnym procesie z limitem czasu, ograniczonym protokołem JSON i zakończeniem procesu. Jawnie wybierz folder modelu; żaden model nie jest pobierany automatycznie. Tylko modele tekstowe kompatybilne z GenAI, bez dowolnych sieci .onnx.
4. WPF wybór czasu wykonania, modelu, folderu ONNX; wszystkie zapewniają tę samą rygorystycznie przetestowaną konstrukcję.
5. Stanowisko testowe z wyraźnymi ograniczeniami dla sprzętu, DPI, panelu mobilnego i ekskluzywnego panelu makro. Kontrole fizyczne i podpisany/licencjonowany sterownik nie mogą zostać tutaj zastąpione przez oprogramowanie.
6. Kompilacja wydania, testowanie rdzenia/Windows, dym WPF, testowanie procesów roboczych/pakietów i niezależny przegląd. Utwórz nowy kompletny pakiet; Jakość modelu należy zgłaszać tylko w postaci przetestowanej, jeśli model faktycznie istnieje.

Wynik: wdrożono cztery runtime, ONNX przetestowano natywnie oddzielnie, wybór dostawcy i stanowiska testowego w upakowanym dymie WPF przebiegły pomyślnie. Zestawy Core/Windows zielone. Przejrzyj znalezienie limitu Unicode ze wspólną stałą i naprawioną regresją. Prawdziwe zatwierdzenia zmian w sprzęcie/urządzeniu mobilnym/DPI, wydanie sterowników, pełne tłumaczenia techniczne i jakość instrukcji na żywo pozostają jawną pracą otwartą; nie wydano jako kompletne.

Kontynuacja: wdrożone oddzielne wzorce Jinja i nazwane/default; przechodziły testy regresji typu/grzebienia/falback. 27 Katalogy uzupełnione. prawdziwy model instrukcji pobrane oddzielnie i SHA256 sprawdzone: native CPU referencje działa, ale 0/4 Przypadki planowania spełniają umowę JSON; nie wykonuje się żadnych działań. niezależny przegląd: zero-fallback poprawione i przetestowane, po czym nie ma konkretnych dalszych odkryć. Windows- testy KI, budowa 0 Ostrzeżenia i błędy.

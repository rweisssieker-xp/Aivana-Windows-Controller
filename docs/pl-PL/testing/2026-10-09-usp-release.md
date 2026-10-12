<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-09-usp-release.md)

# Pomoc dotycząca klawiatury, nakładka i informacje zwrotne dotyczące wykonania

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Wdrożono trzy priorytetowe ulepszenia w istniejącej aplikacji Windows:

- Kreator klucza specjalnego: aktualna inwentaryzacja urządzeń/kluczy obsługiwanych przez iCUE, tymczasowe przechwytywanie klucza G, weryfikacja powodzenia konfiguracji, sprawdzanie konfliktów i niezapisana wersja robocza akcji. Przechwytywanie wycofywania, unieważniania wywołań zwrotnych w kolejce i przywracania normalnego wiązania obejmuje błędy i zamykanie okna dialogowego.
- Nakładka klawiatury: fizyczna geometria referencyjna, powiązania aktywnego profilu, odświeżanie profilu/edycji na żywo i zwykłe podświetlanie klawiszy Windows. Przytrzymaj Ctrl+Alt+F1 lub przełącz się pomiędzy stroną palety/klawiatury. Nieaktywny, klikalny i nieobecny na pasku zadań.
- Informacja zwrotna o wykonaniu: wpisane fazy Oczekiwanie/Uruchomienie/Krok/Zakończono/Anulowano/Niepowodzenie/Odrzucono, postęp kroku/powtórzenia i nieaktywujący toast. Opinia nie obejmuje treści działań ani komunikatów o wyjątkach. Anulowanie ostatniego kroku nie może zgłosić zakończenia. Zakończenie opisuje wykonanie, a nie niezależne zapytanie o stan OBS.

Weryfikacja:

- 24 Podstawowe kontrole zakończone sukcesem, w tym kolejność faz, wyniki awarii/anulowania, anulowanie w ostatnim kroku, izolacja wyjątku odbiornika i wykluczenie wartości akcji prywatnych.
- Wydanie kompilacji rozwiązania przeszło bez ostrzeżeń/błędów.
- Windows kontrola zaliczona; podłączony K100 zgłosił G1–G6 i natywna konfiguracja/wydanie powiodło się. Punkty końcowe audio zostały sprawdzone bez zmiany ich stanu.
- WPF dymek przeszedł ze wszystkimi referencyjnymi układami nakładek, kreatorem/katalogiem/konfliktami/trwałością wersji roboczej, konfiguracją przechwytywania natywnego na żywo, przekazywaniem wywołań zwrotnych, odrzucaniem nieaktualnych wywołań zwrotnych i przywracaniem wstrzymanych powiązań. Symulowano natywne wejście wywołania zwrotnego; fizyczne naciśnięcie klawisza G przez człowieka pozostaje sprawdzeniem ręcznym.
- Rzeczywista niezmienność okna pierwszego planu została sprawdzona dla toastu wykonania. Sprawdzono właściwości interakcji nakładki oraz pokaż/ukryj; wyrenderowano zrzuty ekranu dla kreatora, nakładki i tostu.
- Języki 27 zawierają wszystkie ciągi kanoniczne 819; Weryfikacja symbolu zastępczego/klucza offline przebiegła pomyślnie.

Migawki i pełny wynik GUI znajdują się w artefaktach/usp-win-x64/smoke. Testy korzystają z profili tymczasowych; Podczas wdrażania tworzone są kopie zapasowe ustawień produkcyjnych i sprawdzane pod kątem skrótu.

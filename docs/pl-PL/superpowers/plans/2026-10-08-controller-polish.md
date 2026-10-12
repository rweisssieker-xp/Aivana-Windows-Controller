<!-- generated-doc locale=pl-PL source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plan poprawy użyteczności i możliwości kontrolera

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Żądanie użytkownika: poprawa wyglądu, użyteczności i funkcjonalności; uprzednia autoryzacja wymaga maksymalnie przydatnych subagentów w przypadku niezbędnych mniejszych modeli i samodzielnego uzupełniania.

## Zakres
Zachowaj istniejące profile i działającą integrację Corsair SDK. Ulepsz istniejącą aplikację WPF zamiast tworzyć osobny prototyp.

- [x] Pracownik powłoki/dashboardu: responsywny układ, aktywna nawigacja, czytelne elementy sterujące, kompaktowa aktywność/stan, zmiana kolejności kafelków metodą „przeciągnij i upuść”, pomocne puste stany i szablony. Posiadaj pliki MainWindow.xaml, MainWindow.xaml.cs i MainWindow.Dashboard.cs.
- [x] Pracownik redaktora: MainWindow.Editor.cs i opcjonalne pliki częściowe tylko dla redaktora. Jawne edycje etapowe, zapisywanie/odrzucanie, weryfikacja w trybie inline, dedykowana lista makr z funkcją „przeciągnij i upuść”, zduplikowane kroki, niezawodne przechwytywanie skrótów klawiszowych i pola plików/adresów URL.
- [x] Windows worker: natywna walidacja, solidne nazwy urządzeń i możliwości SDK, ograniczone oczekiwania modyfikatorów i bezpieczne wstrzykiwanie; własne testy Windows i Windows.
- [x] Nadrzędny: postęp wykonywania rdzenia, interfejsy kontrolera, zachowanie konfiguracji, testowanie integracji, aktualnie wyrenderowane zrzuty ekranu, publikacja/pakowanie i wymiana działającej aplikacji.

## Umowa interfejsowa
Kontroler udostępnia istniejące interfejsy API oraz ciąg LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), void OpenIcue(). Edytor udostępnia bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); istniejące nazwy pól/metod pozostają dostępne w klasach częściowych.

## Akceptacja
Brak resetowania rzeczywistej konfiguracji użytkownika; ważne zmiany etapowe zapisz raz, nieprawidłowe wersje robocze pozostają do edycji, odrzucenie przywraca zapisane dane. Natywne aliasy skrótów zostały sprawdzone bez wprowadzania danych testowych do niepowiązanych okien. Przetestowano zmianę kolejności płytek i makr; wszystkie widoki są renderowane w rozmiarach pulpitu; widoczny status działania/awarii/kompletności; wyświetlony rzeczywisty model urządzenia SDK. Kompilacja/testowanie i opublikowanie przepustki dymnej oraz uruchomienie zaktualizowanej aplikacji.

<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-12-dpi-scaling.md)

# Kontrola DPI

MAKRIVO deklaruje PerMonitorV2 w osadzonym manifeście aplikacji. Test WPF wykorzystuje GetWindowDpiAwarenessContext i AreDpiAwarenessContextsEqual do sprawdzenia rzeczywistego efektywnego kontekstu okna głównego.

Dodatkowe testy układu/siatki symulują procenty 150 i 200 na wyświetlaczu 1920 × 1080 z rezerwą na pasku zadań: wymiary okna logicznego odpowiednio 1280 × 693 960 × 520 dipy. Wszystkie pięć stron głównych jest renderowanych jako PNG w rozdzielczości 144 lub 192 DPI. Pliki nazywane są skalą symulowaną-150-* lub skalą symulowaną-200-*.

Testy te nie zmieniają żadnych ustawień Windows. Nie sprawdzają prawdziwego przełączania monitorów WM_DPICHANGED, mieszanej konfiguracji monitorów i rzeczywistej ostrości na innym wyświetlaczu. Testy skalowania fizycznego pozostają otwarte.

Ostrzeżenie analizatora WinForms WFO0003 zostało wyłączone wyłącznie dla tego projektu startowego WPF: WinForms służy do integracji obszaru powiadomień, WPF steruje uruchamianiem; DPI jest ustawiane poprzez manifest przed utworzeniem okna.

Dokumentacja Microsoft: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Kontrola wizualna: Deska rozdzielcza przełącza się również na wyświetlanie piętrowe pod wysokością okna 650 DIP, dzięki czemu mapy i edytor nie są wciśnięte w zbyt mały obszar środkowy.

Stanowisko testowe: wydanie wersji bez błędów/ostrzeżeń; Przeszedł pełny test dymu WPF, w tym rzeczywisty PerMonitorV2-Fensterkontext i symulowany 150/200-Prozent-Renderings. Po regulacji wysokości 200-Prozent-Dashboardaufnahme został poddany kontroli wzrokowej. git diff -- sprawdź pomyślnie.

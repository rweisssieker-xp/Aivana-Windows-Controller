<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-12-gui-polish.md)

# Wersja GUI z 12.10.2026

- Bezpośredni dostęp do „Narzędzi” w nawigacji; trzy grupy z zadeklarowanymi działaniami dotyczącymi konfiguracji, tworzenia i testowania.
- Czternaście istniejących obszarów dialogowych wykorzystuje wspólną klasę dialogową WPF z zasobami okna głównego, spójną czcionką i położeniem u właściciela.
- Rozmiar okna dialogowego jest ograniczony do obszaru roboczego Windows; długa treść otrzymuje paski przewijania. Ustawienia i strony profilowe można przewijać nawet na małej wysokości.
- Filtr akcji pojawia się tylko na pulpicie nawigacyjnym. Wybór profilu i wyszukiwanie mają wyraźne nazwy dla technologii wspomagających. Etykiety pól są bardziej kontrastowe.
- Dialogowy test dymu zawiera przegląd narzędzi i okna dialogowe rozszerzeń w 560 × 440 i normalnym rozmiarze; Istniejące testy rejestratora i języka zostały dostosowane do struktury scrolla.

Ograniczenia: małe okna nie zastępują rzeczywistych testów Windows-DPI przy 150/200 procent. Nowe teksty niemieckie nie zostały jeszcze w pełni przetłumaczone na wszystkie 27 języków. Brak nowej walidacji sprzętu i twierdzeń o zmierzonej użyteczności.

Stanowisko testowe: wydanie kompilacji bez ostrzeżeń/błędów; Przeszedł pełny test dymu WPF, w tym kompaktowe okna dialogowe narzędzi i rozszerzeń oraz kontrole przełączania języków. git diff -- sprawdź pomyślnie.

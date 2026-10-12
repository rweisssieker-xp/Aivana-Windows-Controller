<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-12-responsive-layout.md)

# Responsywny interfejs WPF

Strony główne są tworzone w oparciu o użyteczną szerokość treści, a nie stałą szerokość okna. W obszarze szerokości zawartości 900 DIPs pulpit nawigacyjny i edytor znajdują się jeden pod drugim. W sekcji 620 DIP najważniejsze dane znajdują się w dwóch kolumnach. Pasek boczny jest zwinięty pod szerokością okna 820 DIPs. Minimalny rozmiar: 640 × 480 DIP.

Panele profili i ustawień oraz elementy sterujące klawiatury przełączają kolumny w oparciu o rzeczywistą szerokość siatki. Istniejące elementy sterujące zostają uporządkowane zamiast tworzyć nowe wpisy. Karty akcji dzielą dostępną szerokość linii. Paski narzędzi pękają. Bok klawiatury jest osiągalny w pionie, klawiatura fizyczna zachowuje swoje proporcje.

Test automatyczny WPF: wszystkie pięć stron głównych pod adresem 640 × 480, 900 × 620 i 1440 × 900; Zrzuty ekranu na stronę; Zmiana kolumny w obu kierunkach ze sprawdzeniem otrzymanego tekstu. DIP to logiczne jednostki WPF. Prawdziwe skalowanie Windows w procentach 150/200 i wiele monitorów nie zostało jeszcze sprawdzone.

Stanowisko testowe: Wydaj kompilację bez błędów/ostrzeżeń. Kompletny test dymu WPF przeszedł pomyślnie ze wszystkimi rozmiarami, zmianami kolumn, układami klawiatury i sprawdzeniem języka. git diff -- sprawdź pomyślnie.

<!-- generated-doc locale=pl-PL source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../MAINTENANCE.md)

# Utrzymanie wszystkich języków dokumentacji

Zestaw dokumentacji obejmuje wszystkie 27 języków interfejsu. Wygenerowane foldery używają kodów regionalnych; język norweski odpowiada `nb-NO`, a serbski zapisany alfabetem łacińskim — `sr-Latn`. Każdy folder zawiera indeks, podręcznik, szczegółowy opis akcji, spis funkcji, uwagi dotyczące weryfikacji, badania wraz z plikiem CSV, plany historyczne i analizę licencji.

## Źródło prawdy

Edytuj główny `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, tę stronę lub oryginalne pliki w `docs/research`, `docs/testing`, `docs/superpowers` i `docs/licensing`. Nie przerabiaj raportu historycznego tak, aby sugerował weryfikację późniejszej wersji. Rozróżniaj implementację, symulację, odbiór na rzeczywistym urządzeniu i zatwierdzenie komercyjne.

## Generuj i weryfikuj

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Opcjonalny generator online wysyła tekst dokumentacji do publicznego punktu końcowego tłumaczenia Google, zgodnie z istniejącymi narzędziami lokalizacji. Nie odczytuje konfiguracji użytkownika, sekretów ani plików modeli. Uruchamiaj go tylko dla dokumentacji odpowiedniej dla tej usługi. Bloki kodu, kod w tekście, adresy URL, struktura tabel i identyfikatory techniczne są chronione. Pamięć podręczna tłumaczeń i zamrożona migawka źródeł są przechowywane w ignorowanym katalogu `artifacts/documentation-translation`.

Gdy dostawca online ogranicza liczbę żądań, `tools/translate_documentation_local.py` wypełnia tę samą pamięć podręczną przy użyciu jawnie dostarczonego lokalnego modelu M2M100 CTranslate2. Migawka z 2026-10-12 używa modelu facebook/m2m100_418M na licencji MIT, przekonwertowanego jako JustFrederik/m2m_100_418m_ct2_int8 w rewizji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 i `SentencePiece` 0.2.2 to narzędzia używane wyłącznie podczas budowania. Z MAKRIVO nie są dostarczane wagi modeli ani zależności tłumaczenia. Po wypełnieniu pamięci podręcznej uruchom generator z `--cache-only`, aby wygenerować dokumenty bez żądań sieciowych. Lokalny skrypt pomocniczy nie pobiera ani nie wykonuje zdalnego kodu modeli.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources` uruchomienie wznawia pracę na zamrożonej migawce. Po edycji oryginałów użyj opcji odświeżenia. Segmenty z pamięci podręcznej są wykorzystywane ponownie. Częściowe uruchomienie może wybrać kody języków przez `--locales`; przed publikacją ukończ wszystkie języki. Przerwane uruchomienia nie zastępują po cichu nieudanego tłumaczenia nieprzetłumaczonym tekstem.

Manifest zapisuje każdy plik źródłowy i wygenerowany wraz z odciskami SHA-256 znormalizowanego tekstu UTF-8: zakończenia wierszy LF i brak znacznika kolejności bajtów. Kontroler offline sprawdza pełne pokrycie każdego języka, odciski, linki lokalne, przykłady kodu, strukturę tabel/CSV, źródłowe adresy URL i niezastąpione symbole zastępcze. Odrzuca nieaktualne źródła. Pozytywny wynik kontroli strukturalnej nie jest weryfikacją przez rodzimego użytkownika języka ani zatwierdzeniem prawnym. Wygenerowane strony wyraźnie pokazują to zastrzeżenie.

## Publikacja i pakiety

Publikuj dokumentację, generator i kontroler razem. Zachowaj oryginalne warunki licencji stron trzecich; przetłumaczona analiza licencji ma charakter informacyjny. Artefakty kompilacji, pamięć podręczna tłumaczeń i lokalne pliki orkiestracji nie należą do Git. Wydanie dokumentacji źródłowej samo w sobie nie publikuje instalatora Windows.

Tłumaczenia interfejsu aplikacji są osobnymi katalogami JSON w `src/Aivana.Core/Localization`. Generowanie dokumentacji nie modyfikuje tych katalogów. Kompilacja aplikacji i jej dokumentacja powinny odpowiadać tej samej wersjonowanej migawce; nowo opisane eksperymentalne wsparcie środowiska wykonawczego może wyprzedzać zainstalowany pakiet.

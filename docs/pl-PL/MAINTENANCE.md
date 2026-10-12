<!-- generated-doc locale=pl-PL source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../MAINTENANCE.md)

# Utrzymanie wszystkich języków dokumentacji

Dokumenty dokumentalne są zgodne ze wszystkimi 27 Języki UI. generowane foldery używają kodów regionalnych; mapy norweskie do `nb-NO` Serbii latynowej `sr-Latn`Każdy folder zawiera indeks, podręcznik, szczegółowe odniesienia do działania, zapas funkcji, notatki weryfikacyjne, badania, w tym CSV, plany historyczne i analiza licencji.

## Źródło prawdy

Edit  Koło `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, ta strona, lub oryginalne pliki poniżej `docs/research`, `docs/testing`, `docs/superpowers` i `docs/licensing`Nie pisz historię, aby oznaczać, że jest ona potwierdzona w późniejszej wersji. utrzymać wdrażanie, symulacje, akceptację prawdziwego urządzenia i komercyjną zgodę oddzielnie.

## Generuj i weryfikuj

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Opcjonalny generator internetowy wysyła prozę dokumentacji do publicznego punktu tłumaczenia Google, zgodnie z istniejącym narzędziem lokalizacji. nie czyta konfiguracji użytkownika, sekretów lub plików modelu. tylko go uruchomić na dokumentacji odpowiedniej dla tej usługi. bloky kodowe, kody w linii, adresy URL, struktura tabeli i identyfikatory techniczne są chronione. `artifacts/documentation-translation`.

Gdy dostawca internetowy jest ograniczony, `tools/translate_documentation_local.py` Wypełnić tę samą cache za pomocą wyraźnie dostarczonego lokalu M2M100 CTranslate2 Modelka – The 2026-10-12 Snapshot wykorzystuje licencję MIT facebook/m2m100_418M Modele, które są konwertowane jako JustFrederik/m2m_100_418m_ct2_int8 w rewizji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 i `SentencePiece` 0.2.2 nie są tylko narzędzia. żadne wagi modelu lub zależności tłumaczeń są wysyłane z MAKRIVO. Wykonaj generator z `--cache-only` po wypełnieniu cache, aby przekazać bez żądań sieciowych. lokalny asystent nie pobiera ani nie wykonuje kodu modelu zdalnego.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources`, uruchomienie ponownie zamarza. użyj opcji odświeżania po edycji oryginałów. ukryte segmenty są ponownie wykorzystywane. `--locales`Zmiany nie zastępują cicho nieprzekładanej prozy za nieudane tłumaczenie.

Manifest rejestruje każdy źródło i generowany plik z SHA-256 odcisków ust normalizowanego UTF-8 tekstu: końcówki linii LF i bez znaku zamówienia bytu. Kontroler offline sprawdza pełne pokrycie językowe, odciski palców, lokalne linki, przykłady kodów, strukturę tabeli/CSV, adresy źródłowe i niewymieszane lokalizacje. Odrzuca źródła. Przekazujący kontrolę strukturalną nie jest przeglądem lub zatwierdzeniem prawnym mówcy. Generowane strony przynoszą tę kwalifikację widoczne.

## Publikacja i pakiety

Publikuj dokumentację, generator i kontroler razem. zachowuj oryginalne warunki licencji osób trzecich; tłumaczone analizy licencji są informacyjne. budowanie artyfikacji, cache tłumaczenia i pliki lokalnej orchestracji nie należą do Git. Windows Instalator jest

Tłumaczenia aplikacji są oddzielnymi katalogami JSON pod `src/Aivana.Core/Localization`Generacja dokumentacji nie zmienia tych katalogów. aplikacja buduje i jej dokumentacja powinna udostępniać w wersji zdjęcie; niedawno opisany czas pracy eksperymentalny może poprzedzają zainstalowany pakiet.

<!-- generated-doc locale=pl-PL source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../VERIFICATION.md)

# Egzamin – 8. Październik 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Aplikacja była w toku Windows x64 ze stabilnym zestawem SDK .NET 10.0.202 zbudowany i niezależny Windows-x64-Ausgabe opublikowany.

## Sprawdzane automatycznie

- Ogólna wersja: bez ostrzeżeń i błędów.
- 19 Podstawowe testy zachowania: konfiguracja domyślna, walidacja, dane zerowe/zduplikowane, priorytet profilu, import/eksport i przywracanie kopii zapasowej, zamawianie i ponawianie makr, przerywanie, podwójne wyzwalanie, serializacja, niezmienna migawka wykonania i błąd kroku.
- Windows sprawdzenie: normalizacja klawiszy skrótu i aliasy, natywne rozmiary struktur, rzeczywista lista urządzeń klawiaturowych, bezpieczna rejestracja i ponowne udostępnianie, syntetyczny WM_HOTKEY do kafelkowania i anulowania, wyłączona rejestracja kafelków podczas symulacji wprowadzania.
- Samodzielny plik EXE został uruchomiony za pomocą `--smoke-test`: renderowanych jest wszystkie pięć widoków WPF, można tworzyć/kopiować/usuwać kafelki, wersje robocze są zachowywane podczas odświeżania, konfiguracja jest zapisywana i ładowana ponownie. Ten test wykorzystuje izolowany tymczasowy katalog konfiguracyjny i nie wykonuje żadnych działań związanych z tekstem ani multimediami.
- Pulpit nawigacyjny, edytor i kompaktowa reprezentacja (900 × 720) zostały sprawdzone wizualnie jako rzeczywiste renderingi WPF. Sprawdzane jest zapisywanie wersji roboczej, odrzucanie, kolejność płytek/makr, niemieckie aliasy skrótów, anulowanie poszczególnych makr i postęp kroków.

## Limity zależne od sprzętu

Podczas ostatniego uruchomienia lokalnego wykryto cztery interfejsy klawiatury Windows. Liczba ta opisuje interfejsy HID/Windows, a nie koniecznie cztery klawiatury fizyczne.

Oficjalny pakiet SDK iCUE jest zainstalowany i znajduje się obok pliku EXE. Test na żywo łączy się z iCUE i wykrywa klawiaturę Corsair z informacjami o modelu i diodach LED. Zmiany oświetlenia nie zostały wywołane automatycznie; Klucze specjalne zależne od modelu i klucze innych producentów nie są testowane pod kątem sprzętu. Ogólne skróty klawiaturowe mają zastosowanie na wszystkich klawiaturach.

Wprowadzane dane tekstowe/multimedialne nie zostały wprowadzone do innych otwartych programów użytkownika. Sprawdzono natywne struktury danych i logikę błędów/wydań; Zachowanie w konkretnym programie docelowym zależy między innymi od jego poziomu uprawnień.

## Powtórz

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Test GUI zapisuje wynik i zrzut ekranu w pliku `smoke` obok testowanego pliku EXE. Jest przeznaczony jako tryb diagnostyczny i po zakończeniu nie uruchamia trwałej instancji w tle.


## Zaawansowane sterowanie

Dodatkowo sprawdzone: warunki profilu z tytułem procesu i okna, zamiana zmiennych bez rozwijania rekurencyjnego, podgląd makra bez wykonania, walidacja nowej akcji i kompatybilność wsteczna starych plików JSON. Natywne kontrole obejmują paletę poleceń z rejestracją, konfliktami, wysyłaniem i zwalnianiem WM_HOTKEY, a także czystym podziałem monitorów i dostępem do odczytu schowka na żądanie. Test WPF otwiera i renderuje rzeczywistą paletę, sprawdza cofanie/ponawianie i ulubione, sprawdza czyste wersje robocze po renderowaniu i pomyślnie kończy proces.

WindowsZZZ, zmiany w schowku i ruch okien nie zostały zastosowane w programach użytkownika podczas testowania. Publikuj akcje okna Żądania Windows asynchronicznie; Konkretny program docelowy może mieć własne minimalne rozmiary lub limity autoryzacji. Statystyki blokady profilu i wykonania dotyczą bieżącej sesji.


Egzamin uzupełniający z 12.10.2026: 30 egzaminy podstawowe zaliczone; Adapter Windows obejmujący zwolnienie podczas wstrzykiwania sygnału wejściowego, wykrywanie monitora tylko do odczytu i zaliczenie próbne stanu OBS. WPF sprawdza zatwierdzanie/anulowanie przepływu, uruchamianie/zatrzymywanie rejestratora, podgląd wymiany profili, gesty i zasoby symboli. Lokalizacja: katalogi 27 każdy z kluczami 967; Dokumentacja: 84 strony w czterech językach. Zastrzeżone przyciski inne niż Corsair nie zostały przetestowane sprzętowo; Obsługa ogólna obejmuje standardowe zdarzenia specyficzne dla urządzenia F13–F24 i klucze multimedialne. Transport w chmurze odbywa się za pośrednictwem wybranego zsynchronizowanego folderu; Importy są sprawdzane i pozostają wyłączone.

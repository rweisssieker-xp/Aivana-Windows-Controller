<!-- generated-doc locale=pl-PL source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Plan wdrożenia kontrolera Windows

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> W przypadku pracowników agentowych: użyj równoległej własności izolowanej; użytkownik wyraźnie zażądał pełnej implementacji z maksymalną liczbą przydatnych podagentów.

**Cel:** Dostarcz natywny panel skrótów Windows w zatwierdzonym projekcie.
**Architektura:** Konfiguracja rdzenia i wykonanie z możliwością anulowania; Windows adaptery; WPF kompozycja pulpitu.
**Stos technologiczny:** .NET 10, WPF, Win32, JSON. Brak płatnych zależności.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Ograniczenia globalne
Niemiecki interfejs użytkownika; lokalne przechowywanie; Windowsx64; prawdziwy stan możliwości sprzętu; brak logowania klawiatury; brak domyślnego autostartu.

## Recenzja Fokusa
Źle sformułowane importy nie mogą zastępować zapisanych ustawień; trzymane modyfikatory nie mogą zanieczyszczać wyników; odłączony sprzęt nie może ulec awarii; zamykanie musi zapewniać dostępność tacy; anulowane makra muszą zwolnić zasoby.

## Zadania i własność
- [x] Agent podstawowy: src/Aivana.Core i testy/Aivana.Tests. Modele, trwałość JSON, walidacja, wybór profilu i MacroRunner. Przeprowadź nieudane testy behawioralne, a następnie zaimplementuj i uruchom wszystkie podstawowe testy.
- [x] Windows agent: src/Aivana.Windows. RegisterHotKey, inwentaryzacja urządzeń, SendInput, proces na pierwszym planie i opcjonalny adapter oświetlenia Corsair. Sprawdź natywne rozmiary struktur i oczyszczenie rejestracji za pomocą bezpiecznych testów dymu.
- [x] Agent GUI: src/Aivana.Desktop/MainWindow.xaml i .cs plus tylko pomocnicy interfejsu użytkownika. Ciemny pulpit nawigacyjny, pełny edytor, profile, urządzenia, ustawienia, przegląd klawiatury i edytor makr. Buduj, gdy istnieją umowy.
- [x] Element nadrzędny: wspólne kontrakty, pliki projektu, skład DesktopControllera, cykl życia aplikacji/tacy, okna dialogowe importu/eksportu, dokumentacja, kontrole integracji, test dymu publikowania i uruchamiania aplikacji.

## Wspólne interfejsy
Kontrakty znajdują się w pliku Core/Models.cs. Implementacja kontrolera znajduje się w pliku Desktop/DesktopController.cs. Interfejs użytkownika wywołuje metody kontrolera za pośrednictwem tej konkretnej klasy. Agenci otrzymują dokładne podpisy przy swoich zadaniach. Każdy ma osobne pliki. Rodzic rozwiązuje awarie między projektami po zakończeniu pracy wszystkich agentów.

## Weryfikacja i dostawa
- [x] kompilacja dotnet Aivana.slnx -c Wersja: zero błędów.
- [x] dotnet run --project testy/Aivana.Tests -c Wydanie: testy behawioralne poszły pomyślnie.
- [x] dotnet public src/Aivana.Desktop -c Release -r win-x64 --samodzielny true -o artefakty/win-x64.
- [x] Uruchom opublikowany plik wykonywalny, sprawdź interfejs użytkownika Windows i stabilność procesu, ćwicz bezpieczny przepływ profilu/edycji/trwałości.
- [x] Zapisz plik README z instrukcjami kompilacji/uruchomienia i ograniczeniami sprzętowymi; kontrole rekordów i pozostałe testy wyłącznie sprzętu.

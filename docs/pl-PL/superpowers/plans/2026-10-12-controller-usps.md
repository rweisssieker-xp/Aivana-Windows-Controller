<!-- generated-doc locale=pl-PL source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Plan wdrożenia rozszerzenia kontrolera

**Cel:** Wdrożenie żądanego monitora, gestów, przepływu pracy, nagrywania, diagnostyki urządzeń, wymiany profili przenośnych i ulepszeń tac.

**Architecture:** Rozszerz istniejące warstwy Core/Windows/Pulpit. Maszyny czystego stanu i przenośna logika konfiguracji działają w Core, natywne integracje w Windows i modalne edytory wersji roboczej w Desktop. Zachowaj istniejące zmiany w obszarze roboczym i wdrożoną konfigurację użytkownika.

**Spec:** ../specs/2026-10-12-kontroler-usps-design.md

- [x] Usługa monitorowania, trwałość celu i asynchroniczny status działania; przetestuj niedostępne identyfikatory i anulowanie.
- [x] Maszyna stanu gestów i utrzymywane warstwy; przetestuj krawędzie, powtórz, zresetuj i istniejące bezpośrednie powiązania.
- [x] Warunki przepływu pracy, gałęzie forward i polityka błędów; testowe migawki i anulowanie.
- [x] Wyraźne nagranie skrótu z recenzją wersji roboczej; filtrowanie testowe i usuwanie haczyków.
- [x] Wymiana profili w folderach współdzielonych z kontrolą wersji i podglądami; konflikty testowe i mapowania przenośne.
- [x] Ogólne standardowe klucze specjalne i diagnostyka dla każdego urządzenia; testowanie analizy, zduplikowane mapowania i zachowanie wstrzymywania.
- [x] Zasoby ikon zasobnika/okna/pliku wykonywalnego i wstrzymany wygląd; sprawdź zasoby w opublikowanym pliku wykonywalnym.
- [x] Zintegruj wszystkie punkty wejścia palety/edytora i zlokalizowany tekst; uruchom testy Core, Windows i WPF.
- [x] Opublikuj w oddzielnym katalogu pomostowym, zachowaj ustawienia użytkownika i uruchom ponownie zaktualizowaną aplikację.

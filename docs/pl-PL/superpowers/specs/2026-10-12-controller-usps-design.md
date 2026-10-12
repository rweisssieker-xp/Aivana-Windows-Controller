<!-- generated-doc locale=pl-PL source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# Rozszerzenia sterujące MAKRIVO

Użytkownik poprosił o wszystkie dziewięć proponowanych ulepszeń i rozpoznawalną ikonę w zasobniku. Istniejące konfiguracje, w szczególności jasność G1/G2, muszą pozostać kompatybilne. Istniejące niezatwierdzone zmiany w brandingu i dokumentacji należą do udostępnionego obszaru roboczego i muszą zostać zachowane.

## Elementy dostarczane i granice

1. Akcje dotyczące jasności wybierają wszystkie obsługiwane monitory lub stabilny określony identyfikator monitora. Brak wyraźnych celów kończy się widocznym niepowodzeniem. Panel pokazuje aktualną jasność, głośność wyjściową, stan mikrofonu i status OBS poprzez ograniczone odczyty asynchroniczne.
2. Klawisze specjalne obsługują natychmiastowe naciśnięcie, dotknięcie, przytrzymanie i dwukrotne dotknięcie z funkcją tłumienia powtórzeń. Przytrzymany selektor warstw tymczasowo zmienia aktywne mapowanie, a zwolnienie go przywraca. Istniejące bezpośrednie powiązania zachowują swoje zachowanie. Wstrzymaj, zmiany profilu, rozłączenia i anulowanie wyczyść oczekujące gesty.
3. Projektant przepływu pracy edytuje warunki, rozgałęzienia przesyłania dalej i zasady zatrzymywania/kontynuowania awarii transakcyjnie w wersji roboczej kafelka. Warunki obejmują pliki, uruchomione procesy i tytuły okien. Żadnych nieograniczonych cykli skoków.
4. Jawne nagrywanie przechwytuje akordy skrótów i czeka, ignoruje wprowadzone dane wejściowe i zatrzymuje się po Escape. Zarejestrowane działania są przeglądane przed przyjęciem; zwykły tekst nie jest rejestrowany.
5. Standardowe klucze specjalne innych producentów dla poszczególnych urządzeń wykorzystują Windows Raw Input. Klucze zastrzeżone wymagają obsługiwanego interfejsu API dostawcy lub mapowania dostawcy na klucz standardowy; nieobsługiwana funkcjonalność jest pokazana uczciwie. Diagnoza wyjaśnia profil, pauzę, konflikty powiązań i aktualny stan adaptera.
6. Wymiana profili wykorzystuje wybrany przez użytkownika folder współdzielony (w tym foldery synchronizowane przez istniejącego klienta chmury). Sprawdzanie wersji zapobiega cichym nadpisaniom. Wyciąganie podglądów zmian i importowanie wyłączonych działań z usuniętymi mapowaniami sprzętu. Włączenie treści akcji jest jednoznaczne. MAKRIVO nie przechowuje żadnych danych uwierzytelniających w chmurze.
7. Ciągi interfejsu użytkownika należą do istniejących katalogów języka 27; DE/EN/FR/IT pozostają minimalnymi sprawdzanymi językami dokumentacji. Należy raczej ujawnić luki w tłumaczeniu niż twierdzić, że są kompletne.
8. Rozpoznawalny turkusowy znak na klawiaturze M zastępuje ogólną ikonę na pasku zadań Windows. Tryb wstrzymania jest oznaczony bursztynową plakietką pauzy. Znakowanie plików wykonywalnych/okien używa tego samego znaku. Etykietka zawiera stan i profil.

## Weryfikacja

Podstawowe kontrole obejmują kompatybilność wsteczną, migawki, anulowanie, synchronizację gestów, gałęzie/błędy przepływu pracy, filtrowanie rejestratora i konflikty synchronizacji. Kontrole Windows obejmują wyliczenie w trybie tylko do odczytu, zdarzenia wydania SDK i analizę Raw Input. WPF kontrole dymu obejmują nowe punkty wejścia i zasoby bez zmiany rzeczywistej jasności monitora, wpisywania do innej aplikacji lub nadpisywania profili użytkowników. Wdrożenie zachowuje kopię zapasową i konfigurację G1/G2.

<!-- generated-doc locale=pl-PL source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: sześć rozszerzeń produktów i adapterów klawiaturowych

Data: 2026-10-12
Status: proponowany projekt do sprawdzenia przez użytkownika; realizacja nie została rozpoczęta.

## Cel
Dostarcz wszystkie sześć żądanych rozszerzeń w istniejącej aplikacji Windows/WPF, zachowując istniejące profile, przypisania G1/G2, obsługę lokalną i korzystanie bez konta. Rozszerz integrację klawiatury na Razer i Logitech, korzystając z architektury opartej na możliwościach wielokrotnego użytku dla innych producentów. Nazwa producenta lub układ klawiatury nigdy nie jest dowodem na to, że obsługiwane są zastrzeżone elementy sterujące.

## Decyzja architektoniczna
Rozszerz Aivana.Core o serializowane kontrakty i walidację, Aivana.Windows o Windows i adaptery dostawców oraz Aivana.Desktop o edytory transakcyjne i widoki statusu. Użyj oddzielnych projektów towarzyszących do izolowanego wykonywania wtyczek i usługi lokalnego panelu mobilnego. Unikaj przepisywania lub pojedynczego uprzywilejowanego procesu hostującego każde rozszerzenie. Istniejąca konfiguracja musi zostać załadowana bez zmian; dodawaj rodzaje akcji zamiast zmieniać kolejność ich utrwalonych wartości wyliczeniowych.

Rozważane alternatywy: implementacja monolityczna splątałaby wykonanie sprzętu, sieci i przepływu pracy; zastąpienie WPF spowodowałoby dodanie niepowiązanych prac związanych z migracją. Zalecane są rozszerzenia modułowe.

## 1. Niezależna klawiatura makro pad
Przechowuj powiązania według tożsamości urządzenia oraz fizycznego kodu skanowania i flag rozszerzonych kluczy. Dodaj tryby jawnej obserwacji i wyłączności, wykrywanie urządzeń, przechwytywanie klawiszy, tłumienie powtórzeń, gesty, wstrzymane warstwy i ponownie podłącz diagnostykę. Zachowaj klawiaturę główną/numeryczną Wprowadź rozróżnienia i przetestuj układy niemieckie/amerykańskie. Powiązania są przeznaczone dla wybranej klawiatury fizycznej; niejednoznaczne tożsamości ponownego połączenia wymagają ponownego wyboru, a nie kontrolowania innego urządzenia.

Raw Input zapewnia wyłącznie obserwację. Wyłączne tłumienie dla poszczególnych urządzeń wymaga rzeczywistego zaplecza filtra klawiatury; nigdy nie łącz globalnych haków niskiego poziomu z Raw Input i żądaj niezawodnego tłumienia specyficznego dla urządzenia. Zapewnij watchdog, odblokowanie awaryjne, zachowanie przy otwieraniu awaryjnym i instalator/deinstalator. Nie przechwytuj bezpiecznych danych wejściowych z pulpitu. Instalacja sterownika i aktywacja w trybie wyłączności są odrębnymi działaniami użytkownika.

Bramka sterownika: wybierz obsługiwany, odpowiednio licencjonowany i podpisany backend zgodny z docelowymi konfiguracjami Windows 11 lub opracuj/podpisz dedykowany filtr. Przechwytywanie jest kandydatem do oceny, a nie zatwierdzoną zależnością produkcyjną: jego oficjalny plik README opisuje testowanie za pomocą Windows 10 i odrębną licencję komercyjną. Dopóki ta bramka nie zostanie osiągnięta, tryb wyłączny musi być wyświetlany jako niedostępny; funkcji nie można uznać za ukończoną.

Akceptacja: w przypadku dwóch klawiatur fizycznych ten sam klawisz wykonuje makro i nie powoduje normalnego wprowadzania danych przez aplikację tylko na wybranym ekskluzywnym urządzeniu; inne dane wejściowe z klawiatury pozostają niezmienione. Sprawdź rozłączenie, awarię, anulowanie, zawieszenie/wznowienie i usunięcie sterowników na rzeczywistym sprzęcie.

## 2. Konfiguracja obszaru roboczego z przewodnikiem
Dodaj kreatora do pracy, spotkań, transmisji strumieniowej, badań i przełączania projektów. Odkryj zainstalowane aplikacje, urządzenia audio, monitory i sceny OBS. Użytkownicy wybierają cele i powiązania; wyświetl podgląd uzyskanych kroków i zweryfikuj brakujące wymagania. Zapisz dopiero po sprawdzeniu, bez żadnych działań podczas konfiguracji. Wygenerowane profile można nadal edytować w istniejących edytorach.

Akceptacja: utwórz profil roboczy bez ręcznej edycji ścieżek wykonywalnych lub identyfikatorów urządzeń; anulowanie pozostawia ustawienia bez zmian; brakujące oprogramowanie ma praktyczne wyjaśnienie.

## 3. Sprawdzone wykonanie
Wprowadź kontrole wstępne, wpisane wyniki poszczególnych kroków i opcjonalne warunki końcowe. Rozróżnij stany żądane, zakończone, zweryfikowane, zakończone niepowodzeniem, przekroczone limit czasu, anulowane i niemożliwe do sprawdzenia. Sprawdź dźwięk/mikrofon, jasność, gotowość procesu/okna i stan OBS za pomocą ograniczonych odczytów asynchronicznych. Dostarczenie skrótu/tekstu nie jest dowodem na to, że aplikacja docelowa zaakceptowała polecenie. Zewnętrzne skutki uboczne nie są powszechnie odwracalne; oferować rekompensatę tylko wtedy, gdy jest ona wdrożona.

Akceptacja: odłączone, wyraźne cele nigdy po cichu nie wycofują się; źle zaobserwowany stan nigdy nie zgłasza zweryfikowanego sukcesu; anulowanie zatrzymuje odpytywanie; awarie identyfikują krok i działania naprawcze. Historia wykonywania pomija ładunki tekstowe i wpisy tajne.

## 4. Wtyczki i pakiety, które można udostępniać
Wersjonowane manifesty JSON deklarują akcje, schematy parametrów, dostawców stanu, zależności i możliwości. Uruchamiaj wykonywalne wtyczki w oddzielnym procesie przez ograniczony lokalny IPC z uzgadnianiem, terminami i anulowaniem. Odrzuć zniekształcone wiadomości i niezgodne wersje; obsługiwać awarie bez zamykania kontrolera. Deklaracje możliwości stanowią ujawnienie i mediację, a nie twierdzenie o piaskownicy systemu operacyjnego.

Zapewnij podgląd eksportu/importu pakietów, skróty zawartości, ochronę przed przejściem ścieżki, limity rozmiaru, kontrolę wersji/zależności, jawne włączenie i wycofywanie aktualizacji. Zaimportowane wtyczki wykonywalne nie mogą być uruchamiane automatycznie. Dołącz działającą przykładową wtyczkę z kompleksowym przebiegiem instalacji/akcji/stanu/dezinstalacji. Ta wersja nie wymaga żadnej publicznej usługi rynkowej.

Akceptacja: utwórz, wyeksportuj, zainstaluj i wykonaj prawdziwą przykładową integrację; zademonstrować przekroczenie limitu czasu, izolację awarii, błąd zależności, odrzucenie przejścia i wyłączony import.

## 5. Windows Automatyzacja interfejsu użytkownika
Dodaj selektor elementów i ograniczone akcje dla wywołania, wartości, zaznaczenia, przełączania, fokusu i oczekiwania na element/stan. Utrzymuj selektory przy użyciu procesu, identyfikatora automatyzacji, typu kontroli i kontekstu przodka; unikaj rejestrowania wyłącznie współrzędnych. Pokaż liczbę dopasowań selektora i przetestuj selektory w trybie tylko do odczytu. Pola haseł są wyłączone z rejestracji. W sposób jawny obsługuj nieobsługiwane elementy sterujące i ograniczenia poziomu integralności Windows.

Rozszerzaj nagrywanie o możliwe do sprawdzenia akcje UIA, zachowując bieżące nagrywanie skrótów. Niejednoznaczne selektory zawodzą zamiast klikać dowolną kontrolkę. Zwykłe drzewo dostępności przeglądarki nie gwarantuje pełnego silnika automatyzacji DOM.

Akceptacja: zautomatyzuj dedykowaną aplikację testową ze stabilnymi selektorami w przypadku zmian pozycji/rozmiaru; niejednoznaczność testu, brakujące elementy, wyłączone elementy sterujące, przekroczenia limitu czasu i anulowanie.

## 6. Lokalny panel telefonu/tabletu
Obsługuj responsywny panel z opcjonalnej usługi LAN. Połącz jawnie za pomocą krótkotrwałego kodu jednorazowego i wydawaj odwołalne poświadczenia chronione lokalnie. Używaj protokołu HTTPS z udokumentowanym zaufaniem/przypinaniem certyfikatu lokalnego; brak możliwości awaryjnego wysyłania danych uwierzytelniających w postaci zwykłego tekstu. Domyślnie wyłączone i ograniczające skonfigurowane interfejsy. Sprawdzanie pochodzenia, uwierzytelnione polecenia, limity szybkości i komunikaty ograniczone są obowiązkowe.

Ujawnij dozwolone profile/działania, stan działania, postęp i anuluj; używaj stabilnych identyfikatorów i tej samej kolejki wykonania, co klucze stacjonarne i fizyczne. Nie ujawniaj dowolnych poleceń, ścieżek plików wykonywalnych, sekretów ani instalacji wtyczek. Zapewnij ekrany połączenia, parowania i anulowania. Nie obejmuje konta w chmurze ani przekaźnika internetowego.

Akceptacja: obsługa z prawdziwego telefonu/tabletu w tej samej sieci LAN; niesparowani klienci nie mogą wyświetlać ani uruchamiać akcji; unieważnione tokeny kończą się niepowodzeniem; zduplikowane/odtworzone polecenia nie są wykonywane dwukrotnie; rozłączenie nie powoduje cichego ponawiania działań.

## Zakres producenta klawiatury

| Adapter/rodzina | Planowana integracja | Wyraźna granica |
| --- | --- | --- |
| Corsair | Zachowaj obecne wejście iCUE G-key i integrację RGB | Nadal wymagane jest wykrywanie możliwości modelu/oprogramowania |
| Logitech G | Adapter G-key SDK z kontrolą dostępności wciśnięcia/zwolnienia i czasu działania; oświetlenie za pośrednictwem osobnego zestawu SDK, jeśli jest obsługiwany | Nie utożsamiaj wykrywania G HUB z obsługiwanym API G-key w każdym modelu |
| Razer | Adapter oświetlenia Chroma i przewodnik Synapse do standardowego mapowania klawiszy oraz ogólne wejście HID | Chroma to interfejs API oświetlenia; nie oznaczaj tego jako natywne, zastrzeżone wejście klawisza makra |
| SteelSeries | GameSense Adapter stanu/oświetlenia plus ogólne wejście HID | Przesyłanie zdarzeń GameSense nie jest ogólnym, zastrzeżonym interfejsem API do przechwytywania kluczy |
| Wooting | Adapter analogowego SDK do obsługiwanego wejścia analogowego plus normalne klucze HID | Możliwości i warunki dystrybucji SDK muszą zostać zweryfikowane dla każdego obsługiwanego modelu |
| Keychron/QMK/VIA | Standardowe klucze HID i instrukcje mapowania oprogramowania sprzętowego; znane układy tylko z dowodami | Obsługa konfiguracji VIA nie jest uniwersalnym interfejsem API kluczy makr środowiska wykonawczego |
| Inne standardowe klawiatury HID | Wykrywanie i mapowanie klucza fizycznego na urządzeniu; tryb wyłączny tylko ze zweryfikowanym zapleczem filtra | Brak fabrycznej obsługi Fn, pokrętła lub zastrzeżonego klawisza dla konkretnego modelu |

Każde urządzenie pokazuje stan wykrycia, zaobserwowania, mapowania za pomocą oprogramowania dostawcy, natywnego zestawu SDK, statusu wyłącznie sterownika, niedostępnego lub nieprzetestowanego sprzętowo, zależnie od przypadku. Zachowaj dowody możliwości dzięki wersji adaptera i przetestowanym identyfikatorom modelu/oprogramowania sprzętowego.

## Zamówienie dostawy
1. Zgodność konfiguracji, kontrakty dotyczące możliwości i zweryfikowane wykonanie.
2. Konfiguracja z przewodnikiem przy użyciu prawdziwego wykrywania i istniejących wykonawców akcji.
3. UI Automation i jego urządzenie rejestrujące/testujące.
4. Host wtyczek, przepływ pakietów i przykładowa integracja.
5. Sparowany panel LAN i weryfikacja mobilna.
6. Rozszerzone adaptery dostawców i wyjątkowa integracja zaplecza klawiatury z rzeczywistą walidacją sprzętu.

Wszystkie sześć nadal są potrzebne. Podpisywanie/licencjonowanie sterowników, niedostępne zestawy SDK lub brak sprzętu testowego stanowią bramy do wydania, a nie powody, aby zastąpić pozorowane zachowanie i uznać, że praca została ukończona.

## Kryteria weryfikacji i zwolnienia
Uruchom istniejące testy rdzenia, Windows i WPF i dodaj ukierunkowane testy integracyjne dla każdej nowej granicy. Weryfikuj podróże w obie strony JSON i starsze profile; przetestuj zaimportowaną zawartość wykonywalną, uwierzytelnienie sieci, anulowanie, awarie wtyczek i ponowne podłączenie urządzenia. Sprawdź układy komputerów stacjonarnych i urządzeń mobilnych. Sprawdź oddzielnie wysokie skalowanie Windows. Twórz artefakty wersji dopiero po przejściu kontroli; rozróżnij w uwagach do wersji funkcjonalność wdrożoną/przetestowaną, wdrożoną/nieprzetestowaną sprzętowo i zewnętrznie zablokowaną. Nie wymieniaj zainstalowanej aplikacji, nie instaluj sterowników, nie zmieniaj rzeczywistych mapowań klawiatury ani nie otwieraj dostępu do sieci w ramach przypadkowego testu.

## Dowód
- Microsoft Raw Input semantyka: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech Pakiety SDK dla programistów G: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK — często zadawane pytania: https://developer.razer.com/works-with-chroma/chroma-faq/
- Repozytorium przechwytywania i warunki zależności: https://github.com/oblitum/Interception
- Istniejący spis producentów repozytoriów: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Przejrzyj decyzje
Użytkownik proszony jest o zatwierdzenie tego pisemnego projektu przed planem wdrożenia. Główną zależnością zewnętrzną jest gotowy do produkcji backend sterownika z wyłącznym wejściem. Zalecany zakres zachowuje to jako zamknięty wynik, zamiast po cichu redukować go do obserwacji Raw Input.

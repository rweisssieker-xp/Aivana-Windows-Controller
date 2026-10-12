<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-12-six-extensions.md)

# Sześć rozszerzeń: stanowisko wdrożeniowe i testowe

Stan: 12. Październik 2026, Oddział `codex/six-extensions`. Stan roboczy, brak wydania produkcyjnego.

## Serwis

Paleta poleceń otwiera wpisy **Set workstation**, **Producent klawiatury i SDKs**, **Panel mobilny**, **ERozszerzenia**, **UI automatyzacja**, **UI przepływ rekordów** i **EWykonanie test**.

- Asystent Workspace tworzy profile miejsca pracy, spotkań, transmisji strumieniowej, badań i projektów. Programy z rejestru/menu Start lub selektora plików, adresy URL, dane wyjściowe/ekran i scena OBS są wyświetlane jako kroki przed zapisaniem. Obiekt nie prowadzi żadnych działań.
- W edytorze kroków **Check wynik** włącza kontrolę wstępną i końcową. Historia i panel mobilny odróżniają faktyczne potwierdzenie od nieweryfikowalnego efektu. Zmiany względne, skróty klawiaturowe i wprowadzanie tekstu nie dają ogólnego potwierdzenia skuteczności. Dowód procesu nie świadczy o technicznej gotowości aplikacji.
- UI Automation korzysta z oddzielnego, ograniczonego czasowo procesu roboczego. Wybór za pomocą pozycji myszy, sprawdzenie selektora, Invoke/Select/Toggle/Focus/SetValue/WaitState; wielokrotne trafienia, wyłączone cele i pola haseł są odrzucane. Nagrywanie rejestruje obsługiwane działania i pauzy po kliknięciu; Wpisy tekstowe nie są rejestrowane. Wstęp zostanie sprawdzony przed przyjęciem.
- Rozszerzenia: inspekcja ZIP, skróty, schemat, wyłączony import, jawne zaufanie przed aktywacją, izolowany proces z ograniczonym IPC, eksport, dezaktywacja, odinstalowanie, przywrócenie/odrzucenie poprzedniej wersji. Izolacja procesów to nie piaskownica: współdzielone wtyczki działają z uprawnieniami użytkownika. Przykładowy pakiet `makrivo.example.zip` zawiera odliczanie i akcję statusową. Obsługiwane schematy reprezentują udokumentowany prosty podzbiór schematu JSON w kodzie źródłowym; żadnego publicznego targowiska.
- Panel mobilny: wybierz lokalny adres IP i profile współdzielone, otwórz adres HTTPS, sprawdź certyfikat za pomocą wyświetlonego SHA-256-Fingerprints i świadomie mu zaufaj, a następnie wprowadź jednorazowy sześciocyfrowy kod. Klucze prywatne chronione są za pomocą Windows-DPAPI; zapisywane są tylko skróty dostępu klienta. Certyfikaty zmieniają się po odnowieniu lub zmianie adresu IP. Zapora sieciowa Windows jest zwalniana przez użytkownika w razie potrzeby. Brak automatycznej instalacji certyfikatu, przekierowania portów lub zmiany zapory ogniowej. Dostęp klienta może zostać odwołany. Panel pokazuje status akcji i historię testów; nie jest to jeszcze uniwersalny pulpit nawigacyjny z czujnikami na żywo.

## Klawiatury

| Producent | Wdrożenie | Ograniczenie |
|---|---|---|
| Corsair | istniejące klawisze iCUE-G i RGB | Wymagane jest lokalne połączenie iCUE i odpowiedni model |
| Logitech G | G-Key SDK i oddzielny SDK LED | jawnie zwolnij oryginalny lokalny x64-DLL; G-Key SDK nie zapewnia indywidualnej tożsamości wielu klawiatur |
| Razer | Chroma RGB i standardowe rozpoznawanie klawiszy | Przypisz klucze własne wcześniej w Synapse |
| SteelSeries | GameSense-RGB i standardowe rozpoznawanie kluczy | przypisać klucze własnościowe wcześniej w GG |
| Wooting | Wartości analogowe związane z urządzeniem, progi ciśnienia/uwalniania | oryginalny lokalny x64-SDK-DLL; normalne wejście HID pozostaje aktywne |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/inne | Standardowe przyciski fizyczne według urządzenia i kodu skanowania | Fn i zastrzeżone elementy sterujące zależą od modelu |

Nie uwzględniono żadnych nowych, zastrzeżonych bibliotek DLL pakietu SDK. Adaptery nowego producenta nie zostały jeszcze sprawdzone pod kątem sprzętu bez odpowiednich urządzeń fizycznych.

**E Ekskluzywny drugi makropad nie jest gotowy: ** Raw Input obserwuje urządzenia osobno, ale nie blokuje ich normalnego wejścia Windows. Wersja produkcyjna wymaga licencjonowanego, podpisanego sterownika filtra zweryfikowanego w ramach testów Windows 11/Secure Uruchomienie i wydanie awaryjne, awaria, zawieszenie i ponowne podłączenie. Nie zainstalowano żadnego sterownika. Aplikacja wyraźnie zaznacza ten limit.

## Egzaminy

- Kompilacja wydania: pomyślna, zero ostrzeżeń/błędów.
- Konsola podstawowa: Wszystkie grupy testowe przechodzą pomyślnie, łącznie z konfliktami kluczy dostawców, wycofywaniem wtyczek i rzeczywistymi błędami wykonania w historii testów.
- Integracja rozszerzenia: prawdziwy pracownik UIA z aplikacją testową WPF; unikalne wywołanie, niejednoznaczność, wyłączone cele, wykluczenie hasła. Prawdziwy proces wtyczki nazwanej potoku, granice protokołów, histereza analogowa i pomyślne dekodowanie Logitech ABI.
- Prawdziwy host HTTPS: przypięty certyfikat, dostęp niesparowany, fałszywy/unikalny kod, ochrona pochodzenia, pliki przeglądarki, przechowywanie skrótów i klucz chroniony pomyślnie. Naprawiono błąd Schannel z kluczami tymczasowymi wykryty podczas testów.
- Pakiet Windows z wyraźnie pominiętym testem powodzenia pierwszego planu: udany; Sprawdzono stan audio/OBS/iCUE/Raw Input i ekranu. W poprzednim uruchomieniu pełny zestaw nie powiódł się, ponieważ odmówiono przełączenia fokusu na izolowane okno testowe. Kolejne uruchomienie prototypu AI obejmowało również pełny pakiet obejmujący przełączanie ostrości i podłączoną klawiaturę iCUE. Kolejne powtórzenie ponownie nie powiodło się z powodu zmiany ostrości; Test zależny od kontekstu nie jest zatem stabilnym kolorem zielonym. Test ten nie został osłabiony. Dostęp do odczytu DDC, który zakończy się niepowodzeniem przez krótki czas, jest powtarzany przez ograniczony czas; nie zmieniono jasności.
- WPF-Smoke: wyrenderowano wszystkie poprzednie i nowe okna dialogowe, niezmienność konfiguracji, edytor i CRUD powiodły się; Zrzuty ekranu w folderze kompilacji `smoke`.
- Przeglądarka: Playwright with Edge, 390×844 i 1024×768, przepływ sprzęgający/UUID/wysyłanie jednorazowe/brak pomyślnego przepełnienia poziomego. Próba API; prawdziwy test HTTPS osobno. Zdjęcia pod adresem `artifacts/mobile-browser`. Wtyczka do przeglądarki nie była dostępna, stąd regularne awarie Playwright.
- Przeprowadzono niezależny przegląd kodu; Naprawiono opóźnione polecenia mobilne, brakujące wyniki kontroli terminala, sprawdzanie ścieżki procesu, przywracanie wtyczek i restart pulsu RGB.

Nadal otwarte: ekskluzywna integracja sterowników, testowanie sieci LAN fizycznego producenta/telefonu/tabletu, skalowanie Windows 150/200 procentowo i pełne sprawdzanie tłumaczenia wszystkich nowych tekstów dialogowych. Nowe teksty dialogów są głównie niemieckie; Istniejące katalogi językowe zostają zachowane. W związku z tym ogólny wymóg ** nie został w pełni zaakceptowany**.

## Wygeneruj pakiet

`./tools/publish-complete.ps1 -SelfContained` tworzy nowy folder pod `artifacts/release-*`, zawierający pulpit, proces roboczy UIA, host HTTPS, pliki przeglądarki, importowalną przykładową wtyczkę i uwagi licencyjne środowiska wykonawczego. Zainstalowana aplikacja nie zostanie nadpisana. Bez `-SelfContained` pakiet wymaga dopasowania .NET-10-Desktop-/ASP.NET-Runtimes.

Pakiet jest lokalnym środowiskiem testowym, a nie podpisanym instalatorem detalicznym. Własne Warunki użytkowania i prawa do dystrybucji pakietu SDK stanowią osobne wydania; zobacz istniejącą dokumentację pod `docs/licensing`.

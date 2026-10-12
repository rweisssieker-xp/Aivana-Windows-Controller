<!-- generated-doc locale=pl-PL source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Plan wdrożenia rozszerzeń

> **Dla pracowników agentowych: ** WYMAGANA POD-UMIEJĘTNOŚĆ: Używaj supermocy:wykonywanie planów do natywnego wykonywania lub supermocy:rozwój oparty na subagentach po wybraniu przez użytkownika delegowania. Śledź kroki za pomocą pól wyboru.

**Cel:** Zapewnij sześć zintegrowanych rozszerzeń i opartą na możliwościach obsługę klawiatur wielu producentów.

**AArchitecture:** Zachowaj separację rdzenia / Windows / WPF. Wprowadź izolowane procesy wtyczek i usług mobilnych z wąskim uwierzytelnionym IPC; całe wykonanie przechodzi przez istniejący kontroler i kolejkę MacroRunner. Adaptery sprzętowe reklamują wykryte możliwości, zamiast sugerować wsparcie dla całej marki.

**Tech Stack:** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, udokumentowane SDK dostawcy, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (zatwierdzony przez użytkownika).

## Ograniczenia globalne

- Istniejąca konfiguracja musi zostać załadowana bez zmian; dodawaj rodzaje akcji zamiast zmieniać kolejność ich utrwalonych wartości wyliczeniowych.
- Zachowaj istniejące profile, przypisania G1/G2, obsługę lokalną i korzystanie bez konta.
- Raw Input zapewnia wyłącznie obserwację. Wyłączne tłumienie dla poszczególnych urządzeń wymaga rzeczywistego zaplecza filtra klawiatury.
- Zaimportowane wtyczki wykonywalne nie mogą być uruchamiane automatycznie.
- Domyślnie wyłączone i ograniczające skonfigurowane interfejsy dla usługi mobilnej; brak zastępczego uwierzytelniania w postaci zwykłego tekstu.
- Dostarczenie skrótu/tekstu nie jest dowodem na to, że aplikacja docelowa zaakceptowała polecenie.
- Nie wymieniaj zainstalowanej aplikacji, nie instaluj sterowników, nie zmieniaj rzeczywistych mapowań klawiatury ani nie otwieraj dostępu do sieci w ramach przypadkowego testu.
- Zachowaj niepowiązane niezatwierdzone zmiany. Nie zatwierdzaj istniejących udostępnionych zmian w ramach tej funkcji. Jeśli zażądano zatwierdzenia, przygotuj tylko niezależnie przeglądalne fragmenty funkcji.

## Recenzja Fokusa

1. Zduplikuj identyczne modele klawiatur: niejednoznaczne ponowne połączenie musi wymagać ponownego wyboru (Zadania 10–12).
2. Anulowanie podczas oczekiwania na UIA/dostawcę/wtyczkę/sieć: brak odroczonego działania po anulowaniu (Zadania 1, 4, 6, 8).
3. Edycja użytkownika, gdy otwarty jest kreator lub podgląd pakietu: niedopasowanie wersji musi uniemożliwiać ciche nadpisanie (Zadania 3, 7).
4. Język i układ fizyczny różnią się: selektory i powiązania kodu skanowania nie mogą zależeć od zlokalizowanych nazw wyświetlanych (Zadania 4, 10).
5. Ponowna próba sieci i restart wtyczki: polecenia nie mogą być wykonywane dwukrotnie w trybie cichym (Zadania 6, 8).

## Umowy wspólne i kolejność zależności

Zadania 1–3 ustalają wykonanie i konfigurację. Zadania 4–5 budują UIA. Zadania 6–7 dostarczają wtyczki. Zadania 8–9 zapewniają dostęp mobilny. Zadania 10–12 dostarczają adaptery klawiaturowe i integrację filtrów. Zadanie 13 sprawdza kompletny produkt.

Każde zadanie przebiega zgodnie z cyklem testu negatywnego → minimalna implementacja → cyklu testu pozytywnego. Zadanie jest ukończone tylko wtedy, gdy jest ono osiągalne poprzez aplikację, a jego kontrole zakończyły się pomyślnie. Same rusztowania, próbne adaptery i dokumentacja nie spełniają wymagań dostawy.

### Zadanie 1: Weryfikacja typu i zgodna trwałość

**Pliki:** Utwórz `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; zmodyfikować `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfejsy: ** Generują `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` i `VerifyAsync(ActionStep, CancellationToken)`, oba zwracają `Task<ActionVerificationResult>`. Dodaj opcjonalny serializowalny `VerificationOptions` z włączoną flagą i limitem czasu (100–60000 ms), domyślnie wyłączony. Publikuj wpisane wyniki poszczególnych kroków, w tym identyfikator uruchomienia, identyfikator kafelka i indeks kroku bez ładunków akcji.

- [ ] Kontrole zapisu: starszy kod JSON zachowuje istniejące wartości numeryczne akcji; brak opcji uniemożliwia weryfikację; nieprawidłowy limit czasu nie sprawdzany; błąd wstępnej inspekcji uniemożliwia wywołanie modułu wykonującego; nieudany warunek końcowy nigdy nie daje potwierdzonego sukcesu; nieobsługiwany wykonawca jest niemożliwy do sprawdzenia; anulowanie powoduje zatrzymanie weryfikacji i kolejnych kroków.
- [ ] Uruchom `dotnet run --project tests/Aivana.Tests -c Release`; potwierdzić, że nowe kontrole zakończyły się niepowodzeniem przed wdrożeniem.
- [ ] Wdrażaj kontrakty, walidację, klonowanie/zachowywanie migawek i integrację z MacroRunnerem. Przestrzegaj zasad zatrzymania/kontynuowania OnError; zachowaj istniejących subskrybentów opinii o wykonaniu.
- [ ] Uruchom podstawowe testy; wymagać zaliczenia wszystkich kontroli.

### Zadanie 2: Windows dostawcy weryfikacji i interfejs wyników

**Pliki:** Utwórz `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; modyfikować `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, testować punkty wejścia.

**Interfejsy: ** WindowsActionVerification implementuje dostawcę zadania 1 korzystając z istniejących klientów audio, wyświetlacza, procesu/okna i OBS. Dostępność i identyfikatory testów przed lotem; weryfikacja porównuje pożądany stan po ograniczonym odpytywaniu. Twórz migawki wyników kontrolera wykorzystywane w widoku historii WPF.

- [ ] Dodaj nieudane testy dla niedostępnych jawnych identyfikatorów audio/wyświetlaczy, przekroczenia limitu czasu OBS, niedopasowanego obserwowanego stanu, nieznanych efektów skrótów, pominięcia wrażliwego ładunku i anulowanego odpytywania przy użyciu wstrzykiwalnych dostawców odczytu.
- [ ] Zaimplementuj weryfikację z rzeczywistym odczytem dla głośności bezwzględnej, wyciszenia, jasności bezwzględnej, gotowości procesu/okna i obsługiwanych poleceń OBS. Akcje względne/przełączające wymagają przechwyconego stanu wstępnego; w przeciwnym razie zgłoś Nieweryfikowalne.
- [ ] Dodaj kontrolę weryfikacji krok po kroku i historię bezpiecznego wykonania pokazującą stan, nieudane wymaganie i tekst odzyskiwania.
- [ ] Uruchom testy core/Windows i WPF smoke z dostawcami stanu wtrysku; nie ma prawdziwej jasności ani zapisów audio.

### Zadanie 3: Tworzenie obszaru roboczego z przewodnikiem

**Pliki:** Utwórz `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; modyfikować punkty wejścia palety i dashboardu.

**Interfejsy:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; wybór zawiera identyfikator szablonu, wybrane ścieżki plików wykonywalnych, adresy URL, identyfikatory audio/wyświetlania, scenę OBS i wybrane powiązania. Discovery zwraca nazwanych kandydatów ze stabilnymi identyfikatorami i dostępnością. Wersja robocza rekordów w celu wykrycia konfliktów.

- [ ] Dodaj nieudane testy dla wszystkich pięciu żądanych rodzin kreatorów, niezależnie wygenerowane kroki, brakujące wymagane cele, anuluj bez mutacji i zapisz przed zmienioną wersją.
- [ ] Odkrywaj aplikacje poprzez zarejestrowane ścieżki aplikacji i skróty w menu Start; urządzenia za pośrednictwem istniejących dostawców; sceny poprzez zapytanie tylko do odczytu OBS. Zachowaj możliwość ręcznego wyboru dla nieodkrytych aplikacji.
- [ ] Implementuj Wybierz przepływ pracy → Wybierz cele → Wybierz elementy sterujące → Podgląd/weryfikacja → Zapisz. Generuj edytowalne profile i nigdy nie uruchamiaj ich podczas instalacji.
- [ ] Dodaj aktualne kontrole dymu kreatora WPF pod kątem zakończenia/anulowania/konfliktu; uruchom wszystkie testy.

### Zadanie 4: Selektory UIA i akcje ograniczone

**Pliki:** Utwórz `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modyfikowanie modeli, sprawdzanie poprawności konfiguracji i WinActionExecutor.

**Interfejsy:** Dołącz `ActionKind.UiAutomation`; `UiElementSelector` przechowuje proces, identyfikator automatyzacji, typ sterowania i uporządkowane kryteria przodków. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` zwraca `Task`; operacje Invoke, SetValue, Select, Toggle, Focus i WaitState. Rozdzielczość selektora musi zwrócić dokładnie jeden kwalifikujący się element.

- [ ] Dodaj testy zakończone niepowodzeniem w dedykowanym oknie testowym w celu relokacji/zmiany rozmiaru, zduplikowanych dopasowań, brakujących elementów, nieobsługiwanych wzorców, wyłączonych kontrolek, wartości haseł, etykiet Unicode i przekroczenia limitu czasu/anulowania.
- [ ] Zaimplementuj UIA na dedykowanym procesie roboczym z ograniczonymi żądaniami i ukierunkowaniem na procesy. Potencjalnie zawieszeni dostawcy działają za granicą pracowników, którą można ponownie uruchomić; rezygnacja z żądania nie może pozostawić skutków ubocznych w kolejce.
- [ ] Wyklucz wartości haseł; zgłaszaj ograniczenia dostępu/integralności zamiast je omijać.
- [ ] Przeprowadź kontrole Windows przy użyciu izolowanych urządzeń; dołącz tylko wartości wyliczeniowe i ponownie uruchom starsze sprawdzanie trwałości.

### Zadanie 5: Selektor UIA i sprawdzone nagranie

**Pliki:** Utwórz `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modyfikować katalog akcji, integrację edytora i rejestratora; utwórz kontrolę dymu UIA WPF.

**Interfejsy:** Picker zwraca `UiElementSelector`; rejestrator zwraca proponowane kroki UIA i nigdy nie zapisuje bezpośrednio do zapisanego profilu. Komenda selektora testu zwraca liczbę dopasowań bez wywołania.

- [ ] Dodaj kontrole zakończone niepowodzeniem: selektor wyklucza MAKRIVO i przechwytywanie wartości hasła; zapisz/anuluj pozostawia istniejący profil bez zmian; niejednoznaczna liczba dopasowań raportów podglądu; zaakceptowanie zarejestrowanych kroków tworzy edytowalną wersję roboczą.
- [ ] Zaimplementuj podświetlanie/selektor elementów, parametry specyficzne dla operacji, podgląd selektora i okno przeglądu rejestratora. Zachowaj istniejące zachowanie rejestratora skrótów.
- [ ] Dodaj wpisy do katalogu i dostęp do palety; sprawdź błędy edytora przed zapisaniem.
- [ ] Przeprowadź kontrolę dymu Windows i WPF oprawy; sprawdź zrzuty ekranu selektora/edytora.

### Zadanie 6: Izolowany host i protokół wtyczki

**Pliki:** Utwórz `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; zmodyfikuj rozwiązanie i wysłanie akcji.

**Interfejsy:** Version-1 Manifest określa identyfikator/wersję pakietu, punkt wejścia, schematy parametrów, akcje, dostawców stanu i deklaracje możliwości. Komunikaty IPC zawierają wersję protokołu, unikalny identyfikator żądania, metodę, parametry JSON i wpisany wynik. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` zwraca wpisany wynik. Dołącz `ActionKind.Plugin`.

- [ ] Dodaj nieudane testy dla niezgodnego protokołu, zniekształconej/nadwymiarowej wiadomości (>1 MiB), zduplikowanego identyfikatora żądania, anulowania, awarii, procesu nieodpowiadającego i brakującej akcji.
- [ ] Zaimplementuj IPC potoku nazwanego tego samego użytkownika z drugim uzgadnianiem 10 i konfigurowalnym limitem czasu wykonania ograniczonym do 60 sekund. Zakończ niereagującego pracownika; nie odtwarzaj automatycznie akcji w locie po ponownym uruchomieniu.
- [ ] Egzekwuj schematy i dozwolone usługi kontrolerów. Wyjaśnij zaufanie do plików wykonywalnych; izolacja procesów nie jest piaskownicą systemu operacyjnego.
- [ ] Przeprowadź testy podstawowe i testy integracyjne w porównaniu z rzeczywistymi procesami wtyczek testowych.

### Zadanie 7: Pakiety wtyczek i przykładowa integracja

**Pliki:** Utwórz `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, testy pakietów; modyfikować wpisy palety/ustawień.

**Interfejsy:** Usługa pakietowa udostępnia `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` i `UninstallAsync(packageId)`. Sprawdzony pakiet rejestruje skróty, zależności, wersję i zadeklarowane możliwości.

- [ ] Dodaj nieudane testy dotyczące przechodzenia przez zip, ścieżek bezwzględnych, ucieczki dowiązania symbolicznego/ponownej analizy, zduplikowanych wpisów, rozszerzonych danych >100 MiB, niezgodności skrótu, obniżenia wersji, nieaktualnego przeglądu i wyłączonego importu.
- [ ] Zaimplementuj podgląd i jawne włączenie, błędy zależności, instalację etapową i wycofywanie aktualizacji; rozładować pracownika przed usunięciem.
- [ ] Dostarcz prawdziwą przykładową wtyczkę zwracającą konfigurowalną akcję odliczania i stan na żywo poprzez IPC, w tym anulowanie; spakuj/eksportuj/zainstaluj/odinstaluj za pomocą interfejsu użytkownika.
- [ ] Uruchom testy pakietu/hosta i kompleksowe kontrole WPF. Przed włączeniem sprawdź, czy nie uruchomiono żadnego zaimportowanego pliku wykonywalnego.

### Zadanie 8: Usługa mobilna HTTPS i parowanie

**Files:** Utwórz `src/Aivana.MobileHost/` Projekt ASP.NET Core, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, testy integracyjne; zmodyfikować rozwiązanie i kontroler.

**Interfejsy:** Usługa mobilna komunikuje się ze sterownikiem poprzez IPC tego samego użytkownika. Trasy: parowanie, dozwolone profile/akcje, uwierzytelnione wywołanie akcji, anulowanie i status WebSocket. Wywołanie zawiera unikalny identyfikator polecenia; kontroler jest właścicielem ograniczonej deduplikacji. Dane uwierzytelniające można odwołać i przechowywać za pomocą zabezpieczenia Windows.

- [ ] Dodaj testy zakończone niepowodzeniem dla niesparowanego wyliczenia/wykonania, wygasłego kodu, ponownie użytego kodu, unieważnionego tokena, nieprawidłowego pochodzenia, zduplikowanego polecenia, przewymiarowanej ramki i limitów powodzi.
- [ ] Zaimplementuj odbiornik obsługujący tylko HTTPS, interfejsy wybrane przez użytkownika, lokalne instrukcje dotyczące udostępniania certyfikatów/zaufania, jednorazowe kody parowania 6-cyfrowe wygasające po 120 sekundach, maksymalnie pięć prób na kod i unieważnienie tokena. Przechowuj zaszyfrowane losowe tokeny; nigdy nie loguj poświadczeń.
- [ ] Zaimplementuj identyfikatory stabilnych akcji z listy dozwolonych i istniejące routing kolejek; odrzuć dowolną powłokę, ścieżki i polecenia instalacji pakietu. Skonfiguruj jawne limity szybkości na klienta (polecenia 10/s, seria 30) i limit wiadomości 64 KiB.
- [ ] Sprawdź testy integracji pętli zwrotnej bez dodawania reguł zapory sieciowej lub włączania dostępu do sieci LAN; testowe zamknięcie i trwałość tokenu.

### Zadanie 9: Sterowanie usługą panelu mobilnego i komputera stacjonarnego

**Pliki:** Utwórz panel responsywny `src/Aivana.MobileHost/wwwroot/` i `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; dodaj skrypty weryfikacyjne przeglądarki/urządzenia mobilnego i pokrycie dymem WPF.

**Interfejsy:** Konsumuj zadanie 8 Tylko trasy. Wyświetl siatkę akcji dotykowych, selektor profili, aktualne wartości/postęp, anulowanie, stan połączenia i parowanie. Pulpit kontroluje uruchamianie/zatrzymywanie usług, instrukcje dotyczące adresu/certyfikatu, dozwolone profile i unieważnianie sparowanych klientów.

- [ ] Dodaj nieudaną kontrolę integracji pod kątem przepływu uwierzytelniania, nieaktualnych/brakujących identyfikatorów akcji, ponownego łączenia bez powtarzania akcji, unieważnienia klienta i zatrzymania usługi.
- [ ] Zaimplementuj dostępny, responsywny interfejs użytkownika, korzystając z zasobów lokalnych, bez skryptów stron trzecich i zależności internetowych. Tokeny nigdy nie pojawiają się w adresach URL.
- [ ] Przetestuj układ przeglądarki pod adresem 360/768/1280 CSS px; sprawdź aktualne zrzuty ekranu. Weryfikację za pomocą prawdziwego telefonu/tabletu można przeprowadzić dopiero po wyraźnym włączeniu przez użytkownika usługi LAN.
- [ ] Dokumentuj i zgłaszaj weryfikację sprzętu mobilnego niezależnie od emulacji przeglądarki.

### Zadanie 10: Możliwości urządzenia i obserwacja za pomocą zwykłego klawisza

**Pliki:** Utwórz `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modyfikować RawKeyboardBindingService, KeyboardBinding, walidator i diagnostykę urządzenia; dodaj testy.

**Interfejsy: ** `IKeyboardAdapter` udostępnia migawki możliwości, stabilną tożsamość urządzenia i fizyczne krawędzie kluczy (kod skanowania, rozszerzone flagi, naciśnięcie/zwolnienie). Tryby rozróżniają obserwację, mapowane standardowe wejście, natywny SDK i wyłącznie sterownik. Istniejące specjalne powiązania klucza wirtualnego zachowują swoją reprezentację i zachowanie migracji.

- [ ] Dodaj nieudane testy dla starych powiązań, wprowadź rozróżnienia, niezależność języka/układu, powtarzające się awarie, odłączaj podczas trzymania, niejednoznaczność zduplikowanego modelu i odrzucenie wstrzykiwanego sygnału wejściowego.
- [ ] Rozszerz obserwację na zwykłe klucze fizyczne bez ich pomijania. Dodaj jawne przechwytywanie i ponownie podłącz obsługę tożsamości.
- [ ] Pokaż rzeczywiste możliwości adaptera i status nieprzetestowanego sprzętu w interfejsie użytkownika urządzenia; nigdy nie wnioskuj o obsłudze Fn/dial na podstawie nazw marek.
- [ ] Uruchom testy core/Windows/WPF z symulowanymi raportami i wyliczeniem urządzeń tylko do odczytu.

### Zadanie 11: Adaptery producenta

**Files:** Utwórz osobne pliki pod `src/Aivana.Windows/KeyboardAdapters/` dla LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog i ogólnych wskazówek QmkVia; zmodyfikuj rejestr/interfejs użytkownika i dodaj kontrole protokołu adaptera.

**Interfejsy:** Konsumowanie zadania 10 kontrakty adapterów. Biblioteki natywne ładują się tylko ze zweryfikowanych lokalizacji aplikacji/dostawców; Brak pakietu SDK jest funkcją niedostępną. Możliwości oświetlenia i wejścia są oddzielne.

- [ ] Przed wdrożeniem każdego adaptera sprawdź aktualne oficjalne nagłówki SDK, warunki redystrybucji, zależności oprogramowania i obsługiwane modele; źródła nagrań i dokładne wersje.
- [ ] Dodaj testy zakończone niepowodzeniem dla braku SDK/niewłaściwej architektury, naciśnięcia/zwolnienia/powtórzenia zdarzenia, rozłączenia, zniekształconej odpowiedzi i nieobsługiwanego urządzenia. Używaj możliwych do wstrzykiwania abstrakcji wywołań natywnych do kontroli deterministycznych.
- [ ] Zaimplementuj oddzielnie wejście Logitech G-key i obsługiwane oświetlenie; Razer Podświetlenie Chroma plus Synapse kreator mapowania standardowych klawiszy; SteelSeries GameSense stan/oświetlenie; Wooting odpytywanie analogowe z konfigurowalnymi progami/histerezą; QMK/VIA wskazówki dotyczące konfiguracji klucza standardowego. Nie żądaj zastrzeżonych danych wejściowych z interfejsów API oświetlenia.
- [ ] Sprawdź kompatybilne rzeczywiste urządzenia, jeśli są dostępne. Oznacz inne jako wdrożone/nieprzetestowane sprzętowo, za pomocą dowodów na model/oprogramowanie sprzętowe; brakujący użyteczny SDK to ujawniona bramka zewnętrzna.

### Zadanie 12: Ekskluzywny backend filtra klawiatury

**Files:** Utwórz `src/Aivana.Windows/KeyboardFilter/`, źródło instalatora pod `tools/keyboard-filter/`, testy integracji backendu i dokumentacja wdrożenia. Jeśli żaden odpowiedni licencjonowany backend nie przejdzie oceny, wymagany jest dedykowany projekt podpisanego sterownika.

**Interfejsy:** Raporty backendu o stanie zainstalowanym/kompatybilnym/podpisanym; eksponuje start/stop przechwytywania specyficzny dla urządzenia, krawędzie klawiszy i zwolnienie awaryjne. Oddzielny watchdog przywraca przejście w przypadku utraty kontrolera. Klawiatura główna domyślnie nigdy nie staje się wyłączna.

- [ ] Oceń prawo jazdy kandydata, pochodzenie binarne z podpisem i aktualną kompatybilność rozruchu Windows 11/Secure. Zapisz decyzję typu „tak/nie”. Brak instalacji sterowników podczas zwykłych testów.
- [ ] Napisz testy kontraktowe dotyczące wyboru docelowego filtra, otwarcia awaryjnego, wygaśnięcia pulsu, utraty IPC, niejednoznaczności ponownego połączenia, pauzy/anulowania, zwolnienia przytrzymanego klawisza i utylizacji.
- [ ] Zaimplementuj backend, wyraźny interfejs wyboru/aktywacji, wydanie awaryjne i instalator/deinstalator. Nie zastępuj heurystyki czasowej haka niskiego poziomu/Raw Input.
- [ ] Walidacja na odtwarzalnej maszynie testowej z dwiema klawiaturami: wybrany klawisz wyłączony tylko na wybranym urządzeniu; główne pisanie niezmienione; awaria/zawieszenie/odinstalowanie przywraca dane wejściowe.
- [ ] Zakończenie produkcji bramki na podstawie podpisanego/licencjonowanego backendu i testów fizycznych. Jeśli zewnętrzne podpisywanie/licencjonowanie jest niedostępne, nie ukończ zadania, korzystając z dokładnych dowodów zależności.

### Zadanie 13: Zintegrowana weryfikacja, tłumaczenia i pakiet wydań

**Pliki:** Dodaj `docs/testing/2026-10-12-six-extensions.md`; zaktualizować odpowiednią dokumentację DE/EN/FR/IT, katalogi lokalizacyjne i punkty wejścia do kontroli dymu. Zachowaj inne zmiany w dokumentacji.

- [ ] Zintegruj wszystkie punkty wejścia i migawki; kompletuj tłumaczenia, korzystając z istniejącego przepływu pracy w katalogu języków 27 i ujawniaj niesprawdzone tłumaczenia.
- [ ] Uruchom `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` i `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Sprawdź zrzuty ekranu z komputerów stacjonarnych/mobilnych i zweryfikuj procent 100/150/200 skalowania Windows, podróże konfiguracyjne, cykl życia wtyczki, anulowanie sieci i ponowne podłączenie prawdziwej klawiatury.
- [ ] Opublikuj w nowym katalogu artefaktów za pomocą `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; zawierać pliki binarne usług towarzyszących/hosta oraz informacje o licencji. Nie nadpisuj zainstalowanej aplikacji.
- [ ] Zgłoś ukończone testy, adaptery nieprzetestowane sprzętowo i zewnętrzne bramki wydania. Zanim całe żądanie zostanie uznane za zakończone, wszystkie sześć elementów musi spełnić kryteria akceptacji.

## Zaplanuj samoocenę

Wszystkie sześć sekcji specyfikacji odnosi się do Zadań 1–12; Zadanie 13 obejmuje lokalizację, zintegrowaną weryfikację i wydanie. Raportowanie gotowości, serializacja wykonania, starsza konfiguracja, tajemnice, zaufanie pakietów i niejednoznaczność urządzeń fizycznych mają wyraźnych właścicieli i testy. Zewnętrzne sterowniki/SDK/bramki sprzętowe pozostają niekompletne, dopóki nie zostaną udowodnione, a nie policzone jako sukces oprogramowania.

## Wybór wykonania do sprawdzenia przez użytkownika

Zalecane: natywne wykonanie na tym czacie, zadanie po zadaniu, ponieważ większość modułów ma wspólne umowy, a bieżący obszar roboczy zawiera znaczną część istniejących zmian. Wykonanie delegowane jest dostępne, jeśli użytkownik tak wybierze. Przejrzyj ten plan i wybierz metodę wykonania przed wdrożeniem, zgodnie z wymaganiami przepływu pracy podczas pisania planów.

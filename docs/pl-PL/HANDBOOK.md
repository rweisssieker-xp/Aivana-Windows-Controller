<!-- generated-doc locale=pl-PL source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../HANDBOOK.md)

# MAKRIVO — Podręcznik użytkownika

Producent: **Aivana**. Stan dokumentacji: **2026-10-12**. Ten podręcznik opisuje bieżącą implementację w drzewie roboczym. Integracje eksperymentalne i funkcje zależne od sprzętu są wyraźnie oznaczone. Raporty historyczne opisują stan z określonej daty i nie stanowią gwarancji dla nowszej kompilacji.

## Dokumentacja i języki

Użyj selektora języka dokumentacji, aby otworzyć pełny podręcznik, spis funkcji, rozwiązywanie problemów, notatki rozwojowe, badania i historyczne raporty z testów w dowolnym z 27 języków aplikacji. Wygenerowane tłumaczenia są wspomagane maszynowo i sprawdzane strukturalnie; nie wszystkie zostały zweryfikowane przez rodzimych użytkowników języka. Identyfikatory techniczne i wykonywalne przykłady pozostają niezmienione. Oryginalne źródło pozostaje dostępne do porównania. Przetłumaczona analiza licencji jest informacyjna i nie zastępuje oryginalnych warunków prawnych stron trzecich ani ostatecznej umowy EULA.

## Instalacja i pierwsze uruchomienie

Pierwszą platformą docelową jest Windows 11 x64. Zachowaj wszystkie pliki i podkatalogi dystrybucji razem. Uruchom `MAKRIVO.exe`. Kopia repozytorium źródłowego nie jest instalatorem. Pakiety samodzielne zawierają środowisko wykonawcze .NET; pakiety zależne od środowiska wymagają odpowiednich środowisk .NET 10 Desktop i ASP.NET. ONNX i hosty towarzyszące muszą być dołączone, aby ich funkcje działały.

1. Uruchom MAKRIVO i wybierz język interfejsu w Ustawieniach. Automatyczny podąża za Windows. Układ klawiatury to osobne ustawienie.
2. Wybierz profil, a następnie dodaj kafelek poprzez przeszukiwalny katalog akcji. Nadaj mu rozpoznawalną nazwę i wybierz jego działania.
3. Przejrzyj kroki i w razie potrzeby przypisz skrót. Unikaj kombinacji już używanych przez Windows lub aplikację docelową.
4. Zapisz wersję roboczą. Niezapisane zmiany nie są zapisaną konfiguracją.
5. Przetestuj, wykonując nieszkodliwą czynność, taką jak otwarcie dokumentu, i sprawdź informacje zwrotne o wykonaniu.

Zamknięcie okna głównego zwykle pozostawia aplikację w obszarze powiadomień. Kliknij dwukrotnie jej ikonę w tym obszarze, aby ponownie otworzyć okno, lub użyj menu ikony, aby zakończyć aplikację. Uruchamianie wraz z Windows jest opcjonalne i początkowo wyłączone. Przycisk pauzy wstrzymuje przypisania; ikona pokazuje oznaczenie pauzy. Anuluj działające makra w interfejsie, w menu ikony lub skonfigurowanym skrótem anulowania; domyślny to `Ctrl+Alt+Escape`.

## Kafelki, akcje i paleta poleceń

Płytki należą do profili i mogą zawierać wiele uporządkowanych kroków. Edytor obsługuje zapisywanie/odrzucanie, podgląd, cofanie/ponawianie i zmianę kolejności kroków. Katalog zawiera gotowe ustawienia programów, dokumentów, adresów URL, tekstu, schowka, skrótów, multimediów, dźwięku, okien, działań systemowych i obsługiwanych integracji. Ustawienie wstępne wymagające parametrów otwiera się jako wersja robocza.

Otwórz paletę za pomocą `Ctrl+K`, gdy MAKRIVO ma fokus. Wyszukuj zapisane akcje, ustawienia wstępne, przełączanie profili i narzędzia. Strzałki wybierają element; Enter go aktywuje; `Ctrl+Enter` edytuje obsługiwany element; Escape zamyka paletę. Niektóre gotowe polecenia wykonują się natychmiast, a skonfigurowane akcje wymagają sprawdzenia. Można skonfigurować osobny globalny skrót palety; nowe konfiguracje używają `Ctrl+Shift+Space`.

Obsługa kroków tekstu i schowka `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Format daty to `yyyy-MM-dd`; formatowanie czasu to `HH:mm`. Nieznane zmienne pozostają niezmienione. Odczyt ze schowka następuje tylko na żądanie. Akcja schowka zapisuje tekst bez automatycznego wklejania go.

Akcje uruchamiane z interfejsu mogą kierować się do ostatniej aplikacji zewnętrznej. Jeśli cel zniknie lub Windows zablokuje przeniesienie go na pierwszy plan, aktywuj go ręcznie i spróbuj ponownie. Pomyślne dostarczenie naciśnięcia klawisza nie dowodzi, że cel wykonał zadanie.

## Profile i konfiguracja miejsca pracy

Profile mogą dopasowywać nazwę procesu i część tytułu okna. Jeśli ustawiono oba kryteria, oba muszą być spełnione. Kolejność profili rozstrzyga między równoczesnymi dopasowaniami; profil domyślny jest używany, gdy nie ma dopasowania. Wybór automatyczny należy jawnie włączyć. Blokada profilu zapobiega automatycznym zmianom w bieżącej sesji.

Otwórz konfigurację miejsca pracy z menu Narzędzia lub palety. Wybierz programy, dokumenty, witryny, docelowe urządzenie dźwiękowe/wyświetlacz lub scenę OBS. Asystent przygotowuje profile pracy, spotkań, streamingu, badań lub projektu. Przed zapisaniem sprawdź wygenerowane kroki. Konfiguracja nie wykonuje przygotowanych akcji.

## Makra, warunki i nagrywanie

Makra wykonują swoje kroki w określonej kolejności i obsługują oczekiwania, powtórzenia i anulowanie. Ogólny limit powtórzeń to 100. Użyj podglądu przed uruchomieniem akcji, które wpisują do innej aplikacji lub zmieniają stan systemu.

Wait for Target Window przyjmuje proces, fragment tytułu i limit czasu od 1 do 600000 ms. Wymagany jest co najmniej jeden filtr. Dopasowane widoczne okno musi odpowiadać i znaleźć się na pierwszym planie, zanim kolejne kroki będą kontynuowane. Gotowe okno nie dowodzi, że dokument lub usługa zakończyły ładowanie.

Edytor przepływu pracy dodaje warunki istnienia pliku, działania procesu i tytułu okna. Niespełniony warunek pomija krok i może pominąć kolejne kroki. Zaneguj warunek, aby wyrazić alternatywną gałąź. Świadomie wybierz zatrzymanie lub kontynuację po błędzie; kontynuacja może pozostawić późniejsze kroki bez wymaganych warunków wstępnych.

Rejestrator akcji przechwytuje kombinacje klawiszy skrótów i okresy oczekiwania, a nie zwykłe wpisywanie tekstu. Obsługuje skróty z klawiszami modyfikującymi i klawisze funkcyjne. Escape zatrzymuje nagrywanie. Sprawdź listę, przyjmij ją jako niezapisaną wersję roboczą kafelka, a następnie jawnie zapisz. Nagrywanie kliknięć interfejsu jest osobnym narzędziem opisanym poniżej.

## Klawiatury, gesty i warstwy

Strona klawiatury zawiera listę wykrytych interfejsów i układów referencyjnych. Pojedyncza klawiatura fizyczna może udostępniać kilka interfejsów. Wybierz właściwy język i geometrię; sama nazwa produktu nie identyfikuje układu ISO lub ANSI.

Standardowe globalne skróty klawiszowe działają na wszystkich klawiaturach. Przypisania Raw Input dla konkretnego urządzenia obserwują to urządzenie i kod skanowania, w tym obsługiwane klawisze funkcyjne i multimedialne. **Raw Input nie blokuje zwykłego wejścia Windows. Wyłączna obsługa drugiej klawiatury nie jest gotowa do użytku produkcyjnego.** Nadal potrzebny jest podpisany, licencjonowany i przetestowany backend sterownika filtrującego; MAKRIVO nie instaluje żadnego takiego sterownika.

Obsługiwane przypisania klawiszy specjalnych mogą używać natychmiastowego naciśnięcia, krótkiego naciśnięcia, przytrzymania lub podwójnego naciśnięcia. Klawisze aktywujące warstwę podczas przytrzymania tymczasowo wybierają inne mapowanie i przywracają warstwę podstawową po zwolnieniu; klawisze wyboru warstwy nie uruchamiają własnego makra. Progi przytrzymania wynoszą od 150 do 5000 ms, a przedziały rozpoznawania podwójnego naciśnięcia od 150 do 1000 ms. Te mapowania różnią się od zwykłych globalnych skrótów klawiszowych.

Kreator klawiszy specjalnych wykrywa zgodne klawisze bez wykonywania przypisanych do nich akcji podczas przechwytywania. Diagnostyka wyjaśnia niedostępność urządzeń, wstrzymane przypisania, niezgodności profili i konflikty. Nakładka pokazuje wybrany układ i przypisania; domyślnym skrótem wyświetlania podczas przytrzymania jest `Ctrl+Alt+F1`.

| Integracja | Wymagania i ograniczenia |
| --- | --- |
| Corsair | Uruchamianie iCUE, dostęp do SDK i kompatybilny model. G1–G6 i obsługiwane funkcje RGB; zastrzeżone elementy sterujące Fn, kołem, blokadą i profilem zazwyczaj nie są ujawniane przez interfejs API G-key. |
| Logitech G | Oryginalne lokalne biblioteki SDK G-key/LED x64 wymagają wyraźnej zgody. Zdarzenia klawisza G nie identyfikują jednoznacznie kilku identycznych klawiatur fizycznych. |
| Razer | Chroma RGB i standardowe wejście; w razie potrzeby zmapuj zastrzeżone kontrole w Synapse. |
| SteelSeries | GameSense RGB i wejście standardowe; mapuj zastrzeżone kontrole w GG tam, gdzie jest to potrzebne. |
| Wooting | Kompatybilny oryginalny lokalny zestaw SDK x64 dla wartości analogowych i progów; normalne wejście HID pozostaje aktywne. |
| Inne klawiatury | Standardowe klawisze skrótu lub obsługiwane powiązania fizycznego kodu skanowania. Fn i zastrzeżone elementy sterujące zależą od modelu i konfiguracji dostawcy. |

Badania producentów opisują udokumentowane możliwości, a nie zaimplementowaną lub przetestowaną zgodność z każdym wymienionym urządzeniem. Nowe adaptery nadal wymagają testów odbiorczych na rzeczywistym sprzęcie. Nie pobieraj plików binarnych SDK z niezaufanych źródeł; prawa do dystrybucji SDK są odrębne od zgodności technicznej.

## Audio, wyświetlacze i OBS

Kroki dźwiękowe ustawiają głośność od 0 do 100 procent oraz wyciszają, włączają dźwięk lub przełączają wyciszenie obsługiwanych urządzeń wyjściowych lub mikrofonów. Wybierz konkretny punkt końcowy albo domyślne urządzenie Windows. Brak zapisanego urządzenia docelowego powoduje błąd zamiast cichego sterowania innym urządzeniem. Po ponownym podłączeniu odśwież listę urządzeń. Sterowanie punktami końcowymi nie oznacza obsługi niezależnego miksera dla poszczególnych aplikacji.

Akcje wyświetlaczy zwiększają/zmniejszają jasność o 10 punktów procentowych lub ustawiają wartość od 0 do 100. Wyświetlacze wewnętrzne używają obsługiwanych interfejsów WMI; monitory zewnętrzne wymagają obsługi DDC/CI i zwykle włączenia tej opcji w menu monitora. Stacje dokujące, adaptery i kable mogą blokować sterowanie. Wybierz wszystkie obsługiwane wyświetlacze lub jeden monitor. Ponowne podłączenie może zmienić dostępne identyfikatory; sprawdź zapisany cel. Brak odczytu jest oznaczany jako niedostępność, a nie jako zmierzona wartość.

OBS wymaga włączenia WebSocket v5 w menu Narzędzia OBS. Przed zapisaniem skonfiguruj punkt końcowy, przetestuj połączenie i wczytaj nazwy scen/wejść. Obsługiwane akcje obejmują przełączanie scen, rozpoczęcie/zatrzymanie/przełączanie streamingu lub nagrywania, wstrzymanie/wznowienie nagrywania i wyciszenie wejścia. Hasła można przechowywać w Windows Credential Manager i są wykluczone z eksportów konfiguracji. Pomyślne polecenie OBS nie potwierdza wszystkich dalszych skutków streamingu.

## Weryfikacja wyników i historia realizacji

Włącz weryfikację wyniku dla odpowiednich kroków. Aplikacja rozróżnia kontrole wstępne, potwierdzone skutki, błędy, anulowanie i skutki niemożliwe do zweryfikowania. Istnienie procesu nie oznacza gotowości aplikacji. Zmian względnych, skrótów i wprowadzania tekstu nie można zbiorczo uznawać za potwierdzony sukces. Gdy akcja się nie powiedzie, sprawdź dziennik aktywności i historię weryfikacji. Popraw błędy konfiguracji przed ponownym uruchomieniem całego makra.

## Automatyzacja interfejsu użytkownika i nagrywanie kliknięć

Dodaj krok UI Automation, wybierz operację i po wyświetlonym opóźnieniu przechwyć cel pod wskaźnikiem. Samo przechwycenie niczego nie wykonuje. Sprawdź selektor i wymagaj jednoznacznego dopasowania. Obsługiwane operacje to `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` i `WaitState`. Pola haseł, wyłączone kontrolki i niejednoznaczne selektory są odrzucane.

Automatyzacja interfejsu użytkownika działa w ograniczonym procesie towarzyszącym. Zależy to od obsługi dostępności i uprawnień Windows aplikacji docelowej. Rejestrator kliknięć rejestruje obsługiwane akcje i pauzy; nie rejestruje ogólnego wprowadzania tekstu. Przejrzyj i zatwierdź projekt przed jego przyjęciem. Zmiany interfejsu użytkownika w aplikacji zewnętrznej mogą unieważnić selektory. Izolacja procesów ogranicza awarie i anulowanie, ale nie jest piaskownicą bezpieczeństwa.

## Wtyczki

Otwórz Rozszerzenia, aby sprawdzić pakiet przed importem. Sprawdzane są struktura pakietu, manifest i skróty kryptograficzne. Zaimportowany pakiet jest początkowo wyłączony. Przed włączeniem przeczytaj zadeklarowane akcje i jawnie zaufaj wydawcy. Wtyczki działają z uprawnieniami użytkownika w osobnym procesie; ten proces **nie jest piaskownicą**. Instaluj tylko pakiety, którym ufasz.

Menedżer obsługuje eksport, wyłączanie, odinstalowywanie oraz przywracanie/odrzucanie poprzedniej wersji. Brakujące zależności blokują wywołanie. Przykładowy pakiet udostępnia akcje odliczania i stanu lokalnego. Obsługiwany schemat jest udokumentowanym podzbiorem JSON-Schema, a nie zbiorem wszystkich funkcji JSON-Schema. Nie ma publicznego rynku wtyczek.

## Panel mobilny

1. Otwórz Panel mobilny, wybierz lokalny adres IP i wyraźnie wybierz profile do ujawnienia. Domyślny adres pętli zwrotnej jest osiągalny tylko na samym komputerze; wybierz odpowiedni adres LAN dla telefonu lub tabletu.
2. Uruchom panel i odczytaj jego adres HTTPS, odcisk palca certyfikatu oraz jednorazowy sześciocyfrowy kod parowania.
3. Otwórz adres na urządzeniu w tej samej zaufanej sieci. Porównaj odcisk palca SHA-256 za pośrednictwem zaufanego kanału, zanim zaufasz lokalnemu certyfikatowi. Aplikacja nie instaluje automatycznie zaufanego certyfikatu.
4. Sparuj za pomocą wyświetlonego kodu i przetestuj jedną nieszkodliwą płytkę. Aby uzyskać dostęp do sieci LAN, może być potrzebne ręczne zezwolenie Windows Firewall. Nie udostępniaj portu internetowi.
5. Zatrzymaj panel po zakończeniu lub odwołaj sparowanych klientów za pośrednictwem aplikacji.

Prywatne klucze serwera są chronione Windows DPAPI; dane uwierzytelniające klienta są przechowywane jako skróty. Zmiana adresu IP lub odnowienie certyfikatu może wymagać odnowienia zaufania. Panel pokazuje stany akcji i weryfikacji, a nie każdy możliwy czujnik na żywo. Układ przeglądarki i lokalne kontrole HTTPS nie zastępują rzeczywistej akceptacji sieci LAN telefonu/tabletu.

## Lokalny asystent spotkań AI

Ten opcjonalny eksperymentalny asystent jest domyślnie wyłączony. Otwórz go w menu Narzędzia, jawnie włącz, wybierz środowisko wykonawcze/model i dodaj tylko zasoby, które ma uwzględnić. Określ cel, utwórz wersję roboczą, sprawdź nierozstrzygnięte żądania i przenieś wynik do zwykłego edytora. Nic nie wykonuje się automatycznie; zapisanie i uruchomienie nadal wymagają jawnych działań. Nie ma automatycznego przełączenia na inne środowisko wykonawcze.

| Czas wykonania | Konfiguracja |
| --- | --- |
| Ollama | Oddzielna usługa lokalna i model instalowany lokalnie pod adresem `127.0.0.1:11434`. Zadeklarowane modele zdalne/w chmurze są odrzucane. |
| LM Studio | Serwer lokalny z lokalnie ładowanym modelem i obsługą schematu JSON pod adresem `127.0.0.1:1234`. Nie włączaj zdalnego routingu do przetwarzania lokalnego. |
| llama.cpp | Lokalny `llama-server` i kompatybilny model z obsługą schematu JSON pod adresem `127.0.0.1:8080`. |
| ONNX Środowisko wykonawcze GenAI Procesor | Zaufany, kompatybilny lokalny folder modelu tekstowego GenAI; wybierz `genai_config.json`. Wnioskowanie korzysta z dołączonego procesu towarzyszącego bez protokołu HTTP. |

Klienci HTTP używają wyłącznie adresów pętli zwrotnej, nie korzystają z serwerów proxy i nie podążają za przekierowaniami. MAKRIVO nie może wiarygodnie ustalić, czy zgodny serwer lokalny wewnętrznie wywołuje usługę zdalną. W tej wersji nie można skonfigurować lokalnych serwerów wymagających uwierzytelnienia. Wagi modeli, Python i lokalne serwery wnioskowania nie są automatycznie pobierane ani instalowane.

Pakiety ONNX wymagają `tokenizer.json` i prawidłowego szablonu czatu. Obsługiwane postacie szablonu w `tokenizer_config.json` to ciąg znaków `chat_template`, słownik z ciągiem znaków `default` albo jednoznaczna lista szablonów nazwanych/domyślnych. Jeśli szablonu brakuje lub ma wartość null, jako rozwiązanie zastępcze używany jest `chat_template.jinja`. Szablony puste, niejednoznaczne, zbyt duże lub nieprawidłowego typu są odrzucane. Dowolne pliki ONNX, modele osadzeń/wizyjne i pakiety przeznaczone wyłącznie dla GPU nie są zamienne ze zgodnymi modelami tekstowymi GenAI. Dołączone środowisko wykonawcze GenAI ma wersję 0.17.1 i działa na CPU. Używaj tylko zaufanych modeli; oddzielenie procesów nie jest piaskownicą.

Asystent obsługuje wybrane programy bez argumentów, wybrane dokumenty i witryny oraz wyciszenie mikrofonu na wyraźne żądanie. Nie tworzy wiadomości, spotkań, dowolnych skryptów, dowolnych kliknięć, pobrań ani przełączania zestawów słuchawkowych. Cel i nazwy zasobów są wysyłane do wybranego lokalnego środowiska wykonawczego; ścieżki zasobów i zawartość plików nie są podawane osobno. Cel wpisany przez użytkownika może sam zawierać dane osobowe. Użytkownik odpowiada za własne zachowanie sieciowe lokalnego środowiska wykonawczego.

Limit żądania ONNX wynosi 90 sekund, wliczając ładowanie modelu, do 3072 tokenów wejściowych i 1024 tokenów wygenerowanych, z dodatkowym ograniczeniem przez kontekst modelu. Nieprawidłowy/ucięty JSON i nieznane identyfikatory zasobów są odrzucane. Anuluj lub zamknij, aby zakończyć żądanie. Test środowiska wykonawczego z modelem losowym potwierdza wykonanie, a nie użyteczną jakość planowania lub oszczędność czasu. Lokalne dzienniki eksperymentów zawierają identyfikatory zdarzeń i dane czasowe, a nie tekst celu ani zawartość dokumentów; nie są automatycznie przesyłane.

Późniejsza ocena małego modelu Instruct dała zero prawidłowych planów w czterech próbach w języku niemieckim/angielskim. Natywne wnioskowanie działało, ale model nie spełnił ścisłego kontraktu planowania. To negatywny wynik oceny jakości, a nie zalecany model ani dowód użyteczności asystenta. Dokładną rewizję modelu i ograniczenia podano w datowanym raporcie z testów lokalnego środowiska wykonawczego.

## Import, eksport, synchronizacja i tworzenie kopii zapasowych

Przed zastąpieniem konfiguracji sprawdź podgląd importu. Zaimportowane akcje są początkowo wyłączone, a synchronizacja przenośna usuwa przypisania zależne od sprzętu. Po sprawdzeniu świadomie przypisz urządzenia i włącz akcje. Przed udostępnieniem eksportu sprawdź tekst i treść akcji; pominięcie haseł nie oznacza, że cała treść akcji utworzonych przez użytkownika jest niewrażliwa.

Synchronizacja profili wymienia pliki w wybranym folderze, opcjonalnie zarządzanym przez dotychczasowego klienta chmury. MAKRIVO nie zapewnia konta w chmurze i nie przechowuje danych uwierzytelniających do chmury. Rewizje zapobiegają cichemu nadpisywaniu. Dołączenie treści akcji wymaga świadomego wyboru. Przed importem zastępującym konfigurację wykonaj jej kopię zapasową. Domyślny plik konfiguracji to `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; historyczną nazwę katalogu zachowano dla zgodności. Zamknij aplikację przed ręcznym kopiowaniem/przywracaniem pliku. Zapisane zgody na użycie SDK oraz dane uwierzytelniające chronione przez system operacyjny mogą wymagać ponownego utworzenia na innym komputerze.

## Rozwiązywanie problemów

| Objaw | Co sprawdzić |
| --- | --- |
| Skrót nic nie daje | Aplikacja działa, powiązania nie zostały wstrzymane, popraw aktywny profil, zapisano/włączono kafelek, konflikty skrótów i dostępny cel. |
| Nie można aktywować okna docelowego | Aktywuj go ręcznie. Windows Ograniczenia pierwszego planu i różnice w uprawnieniach mogą blokować automatyzację. |
| Nie wykryto klucza specjalnego | Obsługa usług dostawcy/SDK/modelu; skonfiguruj zastrzeżone elementy sterujące, aby, jeśli to możliwe, emitować obsługiwany klucz standardowy. |
| Druga klawiatura również pisze normalnie | Oczekiwane w przypadku Raw Input. Wyłączne tłumienie nie jest wdrażane w produkcji. |
| Dźwięk lub jasność są niedostępne | Odśwież urządzenia; sprawdź wybraną tożsamość, sesje punktów końcowych, obsługę WMI/DDC/CI i ograniczenia stacji dokującej/kabla. |
| OBS offline | Włączono WebSocket v5, poprawny punkt końcowy/hasło, osiągalna usługa i prawidłowe nazwy scen/wejść. |
| Automatyzacja interfejsu użytkownika kończy się niepowodzeniem | Dołączony host towarzyszący, unikalny prawidłowy selektor, dostępny włączony cel, obsługiwana kontrola i uprawnienia. Ponów próbę przejściowego limitu czasu dopiero po sprawdzeniu celu. |
| Panel mobilny nieosiągalny | Adres LAN zamiast adresu pętli zwrotnej, ta sama sieć, działający serwer, zatwierdzone profile, odcisk palca/zaufanie certyfikatu i ręczne uprawnienia zapory ogniowej. |
| Wtyczka nie działa | Integralność pakietu, jawny stan zaufania/włączenia, zależności, zgodność protokołów i dziennik wykonywania. |
| Żądanie AI nie powiodło się | Popraw wybrane lokalne środowisko wykonawcze, gotowość usługi/modelu, obsługiwany schemat JSON, zaufany zgodny folder modelu i limity żądań. |
| Układ lub tekst wydają się nieprawidłowe | Oddziel język interfejsu użytkownika od geometrii klawiatury; zmień rozmiar okna, sprawdź skalę wyświetlania i podaj dokładną stronę/język/kompilację. |

Aby uzyskać przydatny raport o błędzie, podaj wersję Windows, kompilację/zatwierdzenie aplikacji, język, skalowanie wyświetlania, model sprzętu, minimalną sekwencję i odpowiedni zredagowany dziennik. Przed opublikowaniem usuń ścieżki osobiste, treść akcji i dane uwierzytelniające.

## Budowanie, testowanie i pakowanie

Użyj pakietu SDK przypiętego w `global.json`. Z katalogu głównego repozytorium:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Testy integracyjne Windows mogą otwierać izolowane okna i tymczasowo konfigurować/zwalniać obsługiwane przypisania producentów. Uruchamiaj je bez innego kontrolera zajmującego te przypisania. Domyślne próby jasności/dźwięku są tylko do odczytu; jawne sprzętowe testy zmiany i przywrócenia stanu są osobne. Atrapy protokołów, renderowane zrzuty ekranu i losowe grafy wnioskowania nie mogą być przedstawiane jako rzeczywisty odbiór sprzętu lub jakości modelu.

Kompletny skrypt publikacji składa hosty towarzyszące, zasoby mobilne, przykładową wtyczkę, środowisko wykonawcze ONNX i dostępne informacje o zależnościach. Zachowaj jego podkatalogi. Pomyślna kompilacja nie jest podpisanym instalatorem ani prawnym zatwierdzeniem wydania. Źródła i kompilacje mogą rozwijać się osobno; porównaj migawkę dokumentacji i commit z zainstalowaną wersją.

## Status licencji, prywatności i wydania

Planowany model komercyjny to jednorazowy zakup z bezterminowym użytkowaniem zakupionej wersji głównej; ostateczną cenę, limit urządzeń i warunki aktualizacji muszą określać rzeczywiste warunki sprzedaży. Ten podręcznik nie potwierdza istnienia produkcyjnego systemu zakupu/aktywacji. Do codziennego sterowania nie jest wymagane konto w chmurze.

Nie rozpowszechniaj zastrzeżonych plików binarnych SDK bez udokumentowanego pozwolenia. Zachowaj informacje o licencji i zawiadomienia stron trzecich dla każdej dostarczanej zależności, w tym pakietów .NET i ONNX. Wagi modeli mają własne warunki. Datowany audyt licencji poprzedza niektóre nowe zależności i nie jest pozwoleniem na ich dodanie. Publiczne repozytorium samo w sobie nie udziela ogólnej licencji open source.

Rzeczywisty sprzęt producentów, obsługa telefonu/tabletu w sieci LAN, przejścia między monitorami o różnych parametrach/DPI, długotrwałe użytkowanie, pełna weryfikacja językowa, wyłączna obsługa przez sterownik i użyteczna jakość planowania AI nadal wymagają osobnych testów odbiorczych. Nigdy nie wnioskuj o zatwierdzeniu na podstawie samej obecności funkcji.

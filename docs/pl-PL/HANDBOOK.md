<!-- generated-doc locale=pl-PL source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../HANDBOOK.md)

# MAKRIVO — Podręcznik użytkownika

Producent: **Aivana**. Zrzut dokumentacji: **2026-10-12**. W tym podręczniku opisano bieżącą implementację drzewa roboczego. Integracje eksperymentalne i funkcje zależne od sprzętu są wyraźnie identyfikowane. Raporty historyczne opisują ich przestarzałą migawkę, a nie gwarancję nowszej wersji.

## Dokumentacja i języki

Użyj selektora języka dokumentacji, aby otworzyć pełną instrukcję, spis funkcji, rozwiązywanie problemów, notatki dotyczące rozwoju, badania i raporty z testów historycznych w dowolnym języku aplikacji 27. Generowane tłumaczenia są wspomagane maszynowo i podlegają kontroli strukturalnej; nie wszystkie otrzymały recenzję od native speakera. Identyfikatory techniczne i przykłady plików wykonywalnych pozostają niezmienione. Oryginalne źródło pozostaje dostępne dla porównania. Przetłumaczona analiza licencji ma charakter informacyjny i nie zastępuje oryginalnych warunków prawnych strony trzeciej ani ostatecznej umowy EULA.

## Instalacja i pierwsze uruchomienie

Początkowym celem jest Windows 11 x64. Trzymaj każdy plik i podkatalog dystrybucji razem. Uruchom `MAKRIVO.exe`. Pobranie źródła nie jest instalatorem. Niezależne pakiety obejmują środowisko wykonawcze .NET; pakiety zależne od platformy wymagają odpowiednich środowisk wykonawczych .NET 10 Desktop i ASP.NET. Aby uwzględnić odpowiednie funkcje, należy uwzględnić ONNX i hosty towarzyszące.

1. Uruchom MAKRIVO i wybierz język interfejsu w Ustawieniach. Automatyczny podąża za Windows. Układ klawiatury to osobne ustawienie.
2. Wybierz profil, a następnie dodaj kafelek poprzez przeszukiwalny katalog akcji. Nadaj mu rozpoznawalną nazwę i wybierz jego działania.
3. Przejrzyj kroki i w razie potrzeby przypisz skrót. Unikaj kombinacji już używanych przez Windows lub aplikację docelową.
4. Zapisz wersję roboczą. Niezapisane zmiany nie są zapisaną konfiguracją.
5. Przetestuj, wykonując nieszkodliwą czynność, taką jak otwarcie dokumentu, i sprawdź informacje zwrotne o wykonaniu.

Zamknięcie okna głównego zwykle powoduje pozostawienie aplikacji w obszarze powiadomień. Kliknij dwukrotnie ikonę na pasku zadań, aby go ponownie otworzyć, lub użyj menu na pasku zadań, aby wyjść. Rozpoczęcie od Windows jest opcjonalne i początkowo wyłączone. Kontrola pauzy zawiesza powiązania; ikona przedstawia plakietkę pauzy. Anuluj uruchamianie makr za pośrednictwem interfejsu użytkownika, zasobnika lub skonfigurowanego skrótu anulowania; wartość domyślna to `Ctrl+Alt+Escape`.

## Kafelki, akcje i paleta poleceń

Płytki należą do profili i mogą zawierać wiele uporządkowanych kroków. Edytor obsługuje zapisywanie/odrzucanie, podgląd, cofanie/ponawianie i zmianę kolejności kroków. Katalog zawiera gotowe ustawienia programów, dokumentów, adresów URL, tekstu, schowka, skrótów, multimediów, dźwięku, okien, działań systemowych i obsługiwanych integracji. Ustawienie wstępne wymagające parametrów otwiera się jako wersja robocza.

Otwórz paletę za pomocą `Ctrl+K`, podczas gdy MAKRIVO ma fokus. Wyszukaj zapisane działania, ustawienia wstępne, przełączanie profili i narzędzia. Klawisze strzałek wybierz; Enter aktywuje; `Ctrl+Enter` edytuje obsługiwany element; Ucieczka zostaje zamknięta. Część gotowych poleceń jest wykonywana natychmiastowo, natomiast skonfigurowane akcje wymagają sprawdzenia. Można skonfigurować oddzielny skrót do palety globalnej; nowe konfiguracje wykorzystują `Ctrl+Shift+Space`.

Obsługa kroków tekstu i schowka `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Format daty to `yyyy-MM-dd`; formatowanie czasu to `HH:mm`. Nieznane zmienne pozostają niezmienione. Odczyt ze schowka następuje tylko na żądanie. Akcja schowka zapisuje tekst bez automatycznego wklejania go.

Akcje uruchamiane z interfejsu użytkownika mogą być ukierunkowane na ostatnią aplikację zewnętrzną. Jeśli cel zniknie lub Windows blokuje aktywację pierwszego planu, aktywuj cel ręcznie i spróbuj ponownie. Pomyślne dostarczenie klucza nie oznacza, że ​​cel wykonał swoje zadanie.

## Profile i konfiguracja miejsca pracy

Profile mogą dopasowywać nazwę procesu i część tytułu okna. Gdy oba są ustawione, oba muszą pasować. Kolejność profili rozstrzyga konkurencyjne mecze; domyślnym profilem jest profil rezerwowy. Włącz jawnie wybór automatyczny. Zablokuj profil zapobiega automatycznym zmianom w bieżącej sesji.

Otwórz Ustawienia miejsca pracy z poziomu narzędzi lub palety. Wybierz programy, dokumenty, strony internetowe, obiekt docelowy audio/wyświetlający lub scenę OBS. Asystent przygotowuje profile miejsca pracy, spotkań, streamingu, badań czy projektów. Przed zapisaniem sprawdź wygenerowane kroki. Instalator nie wykonuje przygotowanych działań.

## Makra, warunki i nagrywanie

Makra wykonują swoje kroki w określonej kolejności i obsługują oczekiwania, powtórzenia i anulowanie. Ogólny limit powtórzeń to 100. Użyj podglądu przed uruchomieniem akcji, które wpisują do innej aplikacji lub zmieniają stan systemu.

Wait for Target Window akceptuje proces, podciąg tytułu i limit czasu od 1 do 600,000 ms. Wymagany jest co najmniej jeden filtr. Dopasowane widoczne okno musi odpowiedzieć i stać się pierwszym planem, zanim będą kontynuowane kolejne kroki. Gotowe okno nie jest dowodem zakończenia ładowania dokumentu lub usługi.

Edytor przepływu pracy dodaje warunki istnienia pliku, uruchomionego procesu i tytułu okna. Fałszywy warunek pomija ten krok i może pominąć dodatkowe kroki do przodu. Zaneguj warunek wyrażenia alternatywnej gałęzi. Celowo wybierz zatrzymanie w przypadku błędu lub kontynuację w przypadku błędu; kontynuacja może spowodować pozostawienie późniejszych kroków bez oczekiwanych warunków wstępnych.

Rejestrator akcji rejestruje akordy skrótów i czeka, a nie zwykłe pisanie. Obsługuje skróty modyfikujące i klawisze funkcyjne. Ucieczka zatrzymuje nagrywanie. Przejrzyj listę i zaakceptuj ją jako niezapisaną wersję roboczą kafelków, a następnie zapisz jawnie. Nagrywanie kliknięć w interfejsie użytkownika to osobne narzędzie opisane poniżej.

## Klawiatury, gesty i warstwy

Strona klawiatury zawiera listę wykrytych interfejsów i układów referencyjnych. Pojedyncza klawiatura fizyczna może udostępniać kilka interfejsów. Wybierz właściwy język i geometrię; sama nazwa produktu nie identyfikuje układu ISO lub ANSI.

Standardowe globalne skróty klawiszowe mają zastosowanie na wszystkich klawiaturach. Powiązania Raw Input specyficzne dla urządzenia obserwują określone urządzenie i kod skanowania, w tym obsługiwane klawisze funkcyjne i multimedialne. **Raw Input nie blokuje zwykłego wejścia Windows. Ekskluzywna druga klawiatura nie jest gotowa do produkcji.** Nadal wymagany jest podpisany, licencjonowany i przetestowany sterownik filtra; żaden nie jest instalowany przez MAKRIVO.

Obsługiwane powiązania klawiszy specjalnych umożliwiają natychmiastowe naciśnięcie, dotknięcie, przytrzymanie lub dwukrotne dotknięcie. Wstrzymane klawisze warstwy tymczasowo wybierają inne mapowanie i przywracają warstwę bazową po zwolnieniu; klawisze wyboru warstw nie uruchamiają własnego makra. Progi wstrzymania wahają się od 150 do 5,000 ms; kliknij dwukrotnie okna od 150 do 1,000 ms. Te mapowania różnią się od zwykłych globalnych skrótów klawiszowych.

Kreator kluczy specjalnych wykrywa kompatybilne klucze bez wykonywania przypisanych do nich działań podczas przechwytywania. Diagnostyka wyjaśnia niedostępne urządzenia, wstrzymane powiązania, niezgodności profili i konflikty. Nakładka pokazuje wybrany układ i wiązania; jego domyślny skrót wstrzymania to `Ctrl+Alt+F1`.

| Integracja | Wymagania i ograniczenia |
| --- | --- |
| Corsair | Uruchamianie iCUE, dostęp do SDK i kompatybilny model. G1–G6 i obsługiwane funkcje RGB; zastrzeżone elementy sterujące Fn, kołem, blokadą i profilem zazwyczaj nie są ujawniane przez interfejs API G-key. |
| Logitech G | Oryginalne lokalne biblioteki SDK G-key/LED x64 wymagają wyraźnej zgody. Zdarzenia klawisza G nie identyfikują jednoznacznie kilku identycznych klawiatur fizycznych. |
| Razer | Chroma RGB i standardowe wejście; w razie potrzeby zmapuj zastrzeżone kontrole w Synapse. |
| SteelSeries | GameSense RGB i wejście standardowe; mapuj zastrzeżone kontrole w GG tam, gdzie jest to potrzebne. |
| Wooting | Kompatybilny oryginalny lokalny zestaw SDK x64 dla wartości analogowych i progów; normalne wejście HID pozostaje aktywne. |
| Inne klawiatury | Standardowe klawisze skrótu lub obsługiwane powiązania fizycznego kodu skanowania. Fn i zastrzeżone elementy sterujące zależą od modelu i konfiguracji dostawcy. |

Badania producenta opisują udokumentowane możliwości, nie zaimplementowaną lub przetestowaną kompatybilność z każdym wymienionym urządzeniem. Nowe adaptery nadal wymagają prawdziwej akceptacji sprzętu. Nie pobieraj plików binarnych SDK z niezaufanych źródeł; Prawa do dystrybucji pakietu SDK są niezależne od zgodności technicznej.

## Audio, wyświetlacze i OBS

Zestawienie głosów z 0 do 100 procent i mute/unmute/toggle poparte wyjście lub mikrofon końcowy. Wybierz określony punkt końcowy lub użyj Windows domyślnie. brakuje zarejestrowany cel powoduje błąd zamiast cicho kontrolować inne urządzenie. odśwież urządzenia po ponownym podłączeniu. kontrolę punktów końcowych nie jest roszczeniem niezależnego wsparcia na aplikację.

Działania wyświetlacza zwiększają/zmniejszają jasność o punkty procentowe 10 lub ustawiają wartość od 0 do 100. Wyświetlacze wewnętrzne korzystają z obsługiwanych interfejsów WMI; monitory zewnętrzne wymagają obsługi DDC/CI i zwykle jego ustawienia w menu monitora. Doki, adaptery i kable mogą blokować kontrolę. Wybierz wszystkie obsługiwane wyświetlacze lub jeden monitor. Ponowne połączenie może zmienić dostępne tożsamości; przejrzyj zapisany cel. Brakujące odczyty są wyświetlane jako wartości niedostępne, a nie zmierzone.

OBS wymaga włączenia WebSocket v5 w OBS Tools. Skonfiguruj punkt końcowy, przetestuj połączenie i załaduj nazwy scen/wejść przed zapisaniem. Obsługiwane akcje obejmują przełączanie scen, rozpoczynanie/zatrzymywanie/przełączanie strumienia/nagrywania, wstrzymywanie/wznawianie nagrywania i wyciszanie wejścia. Hasła można przechowywać w Windows Credential Manager i są wyłączone z eksportu konfiguracji. Pomyślne polecenie OBS nie ustanawia każdego efektu przesyłania strumieniowego w dół.

## Weryfikacja wyników i historia realizacji

Włącz opcję Sprawdź wynik dla odpowiednich kroków. Aplikacja rozróżnia kontrole przed lotem, skutki potwierdzone, awarie, anulowanie i skutki, których nie można zweryfikować. Istnienie procesu nie jest gotowością aplikacji. Zmiany względne, skróty i wprowadzanie tekstu nie mogą być traktowane jako całkowite powodzenie. Sprawdź dziennik aktywności i historię weryfikacji, jeśli akcja się nie powiedzie. Błędy konfiguracji należy poprawić przed ponownym uruchomieniem całego makra.

## Automatyzacja interfejsu użytkownika i nagrywanie kliknięć

Dodaj krok automatyzacji interfejsu, wybierz operację i złapać cel poniżej wskaźnika po wyświetlonej opóźnieniu. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` i `WaitState`. Pole hasła, zablokowane kontrolki i niejasne selektory są odrzucone.

Automatyzacja interfejsu użytkownika działa w ograniczonym procesie towarzyszącym. Zależy to od obsługi dostępności i uprawnień Windows aplikacji docelowej. Rejestrator kliknięć rejestruje obsługiwane akcje i pauzy; nie rejestruje ogólnego wprowadzania tekstu. Przejrzyj i zatwierdź projekt przed jego przyjęciem. Zmiany interfejsu użytkownika w aplikacji zewnętrznej mogą unieważnić selektory. Izolacja procesów ogranicza awarie i anulowanie, ale nie jest piaskownicą bezpieczeństwa.

## Wtyczki

Otwórz rozszerzenia, aby sprawdzić pakiet przed jego zaimportowaniem. Sprawdzana jest struktura pakietu, manifest i skróty. Rozpoczęcie importu jest wyłączone. Przed włączeniem przeczytaj zadeklarowane działania i wyraźnie zaufaj wydawcy. Wtyczki działają z uprawnieniami użytkownika w oddzielnym procesie; ten proces to **nie piaskownica**. Instaluj tylko zaufane pakiety.

Menedżer obsługuje eksport, wyłączanie, odinstalowywanie i przywracanie/odrzucanie poprzedniej wersji. Brakujące zależności blokują wywołanie. Przykładowy pakiet udostępnia akcje odliczające i stan lokalny. Obsługiwany schemat to udokumentowany podzbiór schematu JSON, a nie każda możliwa funkcja schematu JSON. Nie ma publicznego rynku wtyczek.

## Panel mobilny

1. Otwórz Panel mobilny, wybierz lokalny adres IP i wyraźnie wybierz profile do ujawnienia. Domyślny adres pętli zwrotnej jest osiągalny tylko na samym komputerze; wybierz odpowiedni adres LAN dla telefonu lub tabletu.
2. Uruchom panel i odczytaj jego adres HTTPS, odcisk palca certyfikatu oraz jednorazowy sześciocyfrowy kod parowania.
3. Otwórz adres na urządzeniu w tej samej zaufanej sieci. Porównaj odcisk palca SHA-256 za pośrednictwem zaufanego kanału, zanim zaufasz lokalnemu certyfikatowi. Aplikacja nie instaluje automatycznie zaufanego certyfikatu.
4. Sparuj za pomocą wyświetlonego kodu i przetestuj jedną nieszkodliwą płytkę. Aby uzyskać dostęp do sieci LAN, może być potrzebne ręczne zezwolenie Windows Firewall. Nie udostępniaj portu internetowi.
5. Zatrzymaj panel po zakończeniu lub odwołaj sparowanych klientów za pośrednictwem aplikacji.

Prywatne klucze serwera są chronione Windows DPAPI; dane uwierzytelniające klienta są przechowywane jako skróty. Zmiana adresu IP lub odnowienie certyfikatu może wymagać odnowienia zaufania. Panel pokazuje stany akcji i weryfikacji, a nie każdy możliwy czujnik na żywo. Układ przeglądarki i lokalne kontrole HTTPS nie zastępują rzeczywistej akceptacji sieci LAN telefonu/tabletu.

## Lokalny asystent spotkań AI

Ten opcjonalny asystent eksperymentalny jest domyślnie wyłączony. Otwórz go w Narzędziach, włącz go jawnie, wybierz środowisko wykonawcze/model i dodaj tylko te zasoby, które chcesz uwzględnić. Określ cel, utwórz wersję roboczą, sprawdź nierozwiązane prośby i prześlij wynik do zwykłego redaktora. Nic nie jest wykonywane automatycznie; zapisywanie i uruchamianie pozostają jawnymi działaniami. Nie ma automatycznego przywracania czasu działania.

| Czas wykonania | Konfiguracja |
| --- | --- |
| Ollama | Oddzielna usługa lokalna i model instalowany lokalnie pod adresem `127.0.0.1:11434`. Zadeklarowane modele zdalne/w chmurze są odrzucane. |
| LM Studio | Serwer lokalny z lokalnie ładowanym modelem i obsługą schematu JSON pod adresem `127.0.0.1:1234`. Nie włączaj zdalnego routingu do przetwarzania lokalnego. |
| llama.cpp | Lokalny `llama-server` i kompatybilny model z obsługą schematu JSON pod adresem `127.0.0.1:8080`. |
| ONNX Środowisko wykonawcze GenAI Procesor | Zaufany, kompatybilny lokalny folder modelu tekstowego GenAI; wybierz `genai_config.json`. Wnioskowanie korzysta z dołączonego procesu towarzyszącego bez protokołu HTTP. |

Klienci HTTP korzystają wyłącznie z pętli zwrotnej, nie korzystają z serwerów proxy i nie korzystają z przekierowań. MAKRIVO nie może wiarygodnie ustalić, czy zgodny serwer lokalny wewnętrznie wywołuje usługę zdalną. W tej wersji nie można konfigurować uwierzytelnionych serwerów lokalnych. Wagi modeli, Python i lokalne serwery wnioskowania nie są automatycznie pobierane ani instalowane.

ONNX Pakiety potrzebne `tokenizer.json` i ważny szablon czatu. obsługiwane formularze szablonu w `tokenizer_config.json` Jest to strona `chat_template`Słownik z struną `default`, lub niepodzielny listę nazw/default szablonu. jeśli szablon jest brakuje lub zero, `chat_template.jinja` Puste, niejasne, nadmierne lub nieprawidłowo wpisane szablony są odrzucane. ONNX pliki, modele wbudowane/widoczne oraz pakiety GPU nie są wymienione z kompatybilnymi GenAI Wzór tekstów - Związek GenAI Wersja Runtime jest 0.17.1Użyj tylko zaufanych modeli; procesy oddzielające nie są pudełkiem piaskowym.

Asystent obsługuje wybrane programy bez argumentów, wybrane dokumenty i strony internetowe oraz wyciszenie mikrofonu na wyraźne żądanie. Nie tworzy wiadomości, spotkań, dowolnych skryptów, bezpłatnych kliknięć, pobrań ani przełączania słuchawek. Nazwy celów i zasobów są wysyłane do wybranego lokalnego środowiska wykonawczego; ścieżki zasobów i zawartość plików nie są dostarczane oddzielnie. Cel wpisany przez użytkownika może sam w sobie zawierać dane osobowe. Za zachowanie sieciowe lokalnego środowiska wykonawczego odpowiada użytkownik.

Budżet żądań ONNX wynosi 90 sekund, w tym ładowanie modelu, maksymalnie 3,072 tokeny podpowiedzi i wygenerowane tokeny 1,024, również ograniczone kontekstem modelu. Źle sformułowany/skrócony JSON i nieznane identyfikatory zasobów są odrzucane. Anuluj lub zamknij, aby zakończyć żądanie. Test czasu wykonania modelu losowego ustala wykonanie, a nie użyteczną jakość planowania lub oszczędność czasu. Lokalne dzienniki eksperymentów zawierają identyfikatory i momenty zdarzeń, a nie tekst celu czy treść dokumentu; nie są one przesyłane automatycznie.

Później mała ocena modelu Instruct wyprodukowała zero ważnych planów w czterech niemieckich/ang. Native inference działał, ale model nie spełnił rygorystycznej umowy planowania. To jest negatywny wynik jakości, nie zalecany model lub udowodniony pomocnik. Zobacz datowany lokalny raport testowy czasu pracy dla dokładnej oceny modelu i ograniczeń.

## Import, eksport, synchronizacja i tworzenie kopii zapasowych

Przed wymianą konfiguracji przejrzyj podglądy importu. Zaimportowane akcje uruchamiają się wyłączone, a przenośna synchronizacja usuwa powiązania specyficzne dla sprzętu. Zmień przypisanie urządzeń i celowo włącz działania po kontroli. Przejrzyj treść tekstu/działania przed udostępnieniem eksportu; wykluczenie haseł nie powoduje, że cała treść akcji utworzona przez użytkownika staje się niewrażliwa.

Synchronizacja profilu wymienia pliki w wybranym folderze, opcjonalnie zarządzane przez istniejącego klienta chmury. MAKRIVO nie dostarcza konta w chmurze i nie przechowuje żadnych potwierdzających w chmurze. Revisions zapobiega cichym przesłaniom. Włączanie treści działania jest wyraźnym wyborem. Zapisz konfigurację przed importowaniem zastępczym. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`• historyczne nazwy adresatu są zachowane dla zgodności. zamknij aplikację przed ręcznym kopiowaniem/rejestrowanym. przechowywane zatwierdzenia SDK i chronione przez system OS potrzebować odtwarzania na innym komputerze.

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

Testy integracyjne Windows mogą otwierać izolowane okna i tymczasowo konfigurować/zwalniać obsługiwane powiązania dostawców. Uruchom je bez innego kontrolera posiadającego te powiązania. Domyślne sondy jasności/dźwięku są tylko do odczytu; jawne testy sprzętowe w obie strony są oddzielne. Próbne protokoły, renderowane zrzuty ekranu i wykresy losowych wniosków nie mogą być przedstawiane jako akceptacja rzeczywistej jakości sprzętu/modelu.

Kompletny wydawca składa się z hostów towarzyszących, zasobów mobilnych, przykładowej wtyczki, środowiska wykonawczego ONNX i dostępnych informacji o zależnościach. Zachowaj jego podkatalogi. Pomyślna kompilacja nie oznacza podpisanego instalatora ani legalnej zgody na wydanie. Źródło i kompilacje mogą ewoluować oddzielnie; porównaj migawkę dokumentacji i zatwierdź z zainstalowaną wersją.

## Status licencji, prywatności i wydania

Zamierzony model komercyjny to jednorazowy zakup z możliwością wieczystego korzystania z zakupionej wersji głównej; ostateczna cena, limit urządzeń i warunki aktualizacji muszą być określone w rzeczywistych warunkach sprzedaży. W niniejszym podręczniku nie określono systemu zakupów/aktywacji produkcyjnej. Do codziennej kontroli nie jest wymagane konto w chmurze.

Nie rozpowszechniaj zastrzeżonych plików binarnych SDK bez potwierdzonego pozwolenia. Zachowaj informacje o licencji i informacjach od innych firm dla każdej dostarczonej zależności, w tym pakietów .NET i ONNX. Wagi modeli mają swoje własne warunki. Przestarzały audyt licencji poprzedzający niektóre nowe zależności i nie stanowi zezwolenia na te dodatki. Repozytorium, które jest publiczne, samo w sobie nie przyznaje ogólnej licencji typu open source.

Sprzęt prawdziwego dostawcy, obsługa sieci LAN na telefonie/tablecie, zmiany między monitorami/DPI, długotrwałe użytkowanie, pełna recenzja językowa, ekskluzywna obsługa sterowników i użyteczna jakość planowania AI pozostają odrębnymi pracami akceptacyjnymi. Nigdy nie wnioskuj o aprobacie na podstawie samej obecności obiektu.

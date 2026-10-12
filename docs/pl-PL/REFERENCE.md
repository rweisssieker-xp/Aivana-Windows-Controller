<!-- generated-doc locale=pl-PL source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../README.md)

# MAKRIVO

[Pełna dokumentacja we wszystkich 27 językach](README.md) · [Pełny podręcznik użytkownika](HANDBOOK.md)

**przez Aivana.** [Aktualne informacje o inwentaryzacji i ograniczeniach](FEATURES.md) · [Raport rozszerzenia](testing/2026-10-12-six-extensions.md) · [Lokalny asystent](testing/2026-10-12-local-meeting-assistant.md)

**Angielski (amerykański)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalny kontroler klawiszy skrótu Windows z kafelkami, profilami, makrami i wykrywaniem klawiatury w stylu Stream Deck. Interfejs obsługuje języki 27. Do codziennego użytku nie jest wymagane żadne konto ani połączenie z chmurą.

[Dokumentacja](README.md) · [Weryfikacja](VERIFICATION.md) · [Raport testowy](testing/2026-10-09-usp-release.md)

## Rozpoczęcie

Po opublikowaniu wniosek znajduje się pod adresem `artifacts/win-x64/MAKRIVO.exe`. Trzymaj cały folder wyjściowy razem; możesz skopiować go gdzie indziej. Windows 11 x64 jest początkowym celem. Artefakty kompilacji są wyłączone z Git; pobranie źródła nie obejmuje wstępnie skompilowanego pliku wykonywalnego.

Zainstaluj pakiet .NET 10 SDK określony w `global.json`, aby kompilować i uruchamiać ze źródła:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Opublikuj samodzielną kompilację bez konieczności instalowania oddzielnie środowiska wykonawczego .NET:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Podstawowy przepływ pracy

1. Wybierz profil i utwórz kafelek akcji.
2. Ustaw jego nazwę, ikonę, kolor i skrót klawiaturowy. Kombinacje Ctrl/Alt/Shift zmniejszają liczbę przypadkowych aktywacji podczas pisania.
3. Dodaj jeden lub więcej kroków: aplikacje, pliki, strony internetowe, tekst, skróty, multimedia, głośność, przełączanie profili lub opóźnienia.
4. Zapisz. Zmiany pozostają wersją roboczą do czasu zapisania; odrzucanie przywraca zapisaną wersję. Przycisk Odtwórz i jego klawisz skrótu korzystają z tej samej ścieżki wykonania.
5. Zmień kolejność kafelków i kroków makr za pomocą przycisków „przeciągnij i upuść” lub strzałek. Szablony upraszczają konfigurację. Makra można powtarzać maksymalnie razy 100. Anuluj za pomocą interfejsu, menu zasobnika systemowego lub domyślnie Ctrl+Alt+Escape.

Profile specyficzne dla aplikacji używają nazwy procesu bez pełnej ścieżki. Kolejność profili zrywa więzi; domyślnym profilem jest profil rezerwowy. Automatyczny wybór profilu musi być włączony w Ustawieniach.

Zamknięcie okna głównego domyślnie powoduje pozostawienie aplikacji w zasobniku systemowym. Kliknij dwukrotnie ikonę na pasku zadań, aby ją ponownie otworzyć; użyj menu zasobnika, aby wyjść. Uruchomienie za pomocą Windows jest opcjonalne i domyślnie wyłączone. Klawisze skrótu można wstrzymywać centralnie.

## Paleta akcji i poleceń

Dodaj akcję otwiera katalog z możliwością przeszukiwania, zawierający gotowe ustawienia 90 w ośmiu kategoriach. Każdy posiada opis i podgląd. Edytor makr używa tego samego katalogu do dodawania lub zastępowania kroków. Wybranie ustawienia wstępnego w tym procesie edytora powoduje utworzenie wersji roboczej bez wykonania.

Paleta poleceń otwiera się z nagłówka lub po naciśnięciu Ctrl+K w aplikacji. Przeszukuje zapisane działania w profilach, ustawieniach katalogów, przełączaniu profili, nawigacji, pauzie/wznawianiu, anulowaniu makr i imporcie/eksportowaniu. Filtruj według zapisanych działań, funkcji katalogu lub poleceń aplikacji. Klawisze strzałek wybierają, Enter aktywuje, Ctrl+Enter edytuje edytowalny wpis, a Escape zamyka. Ulubione pojawiają się jako pierwsze. Gotowe do użycia polecenia katalogu można uruchomić natychmiast; ustawienia wstępne wymagające konfiguracji otwórz wersję roboczą. W ustawieniach można zdefiniować globalny skrót do palety. Nowe konfiguracje używają Ctrl+Shift+Spacja; istniejące konfiguracje nie otrzymują automatycznie nowego globalnego skrótu.

Akcje dźwiękowe umożliwiają precyzyjne ustawienie głośności w zakresie od 0 do 100 oraz wyciszenie, włączenie lub przełączenie urządzeń wyjściowych i mikrofonowych. Wybierz konkretny punkt końcowy lub pozostaw go nieskonfigurowany dla wartości domyślnej Windows. Niedostępny zapisany punkt końcowy powoduje błąd zamiast kontrolować inne urządzenie. Opcja Odśwież urządzenia powoduje ponowne załadowanie podłączonych urządzeń.

OBS Studio używa WebSocket v5: start/stop/przełączenie/wstrzymanie/wznowienie nagrywania; uruchamianie/zatrzymywanie/przełączanie przesyłania strumieniowego; zmieniać sceny; przełącz wyciszenie wejścia. Włącz serwer WebSocket w obszarze Narzędzia w OBS i ustaw jego punkt końcowy w edytorze. Sprawdź połączenie i załaduj nazwy odczytuje wersję OBS, sceny i wejścia bez rozpoczynania nagrywania lub strumienia. Wybierz nazwy z listy lub wprowadź je ręcznie. Hasła można zapisać w Windows Credential Manager i są wyłączone z eksportowanych konfiguracji. Niedostępne połączenia OBS zgłaszają błąd.

Profile mogą dopasowywać nazwę procesu i część tytułu okna. Obydwa określone warunki muszą być zgodne. Zablokuj profil zapobiega automatycznemu przełączaniu sesji. Przechwytywanie warunków z pierwszego planu korzysta z ostatniej aktywnej aplikacji zewnętrznej.

Obsługiwane są akcje tekstowe i schowka `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Daty używają `yyyy-MM-dd`; razy używaj `HH:mm`. Nieznane symbole zastępcze pozostają niezmienione. `{{profile}}` identyfikuje profil rozpoczętej akcji; `{{process}}` identyfikuje kontekst aplikacji zewnętrznej. Zawartość schowka jest odczytywana tylko wtedy, gdy używany jest ten symbol zastępczy. Akcja schowka zapisuje tekst, ale nie wkleja go automatycznie.

Akcje okna minimalizują, maksymalizują, przywracają lub ustawiają okno po lewej/prawej stronie obszaru roboczego monitora. Akcja systemowa blokuje Windows. Te akcje są uruchamiane tylko wtedy, gdy zostaną jawnie wywołane.

Podgląd pokazuje kroki, powtórzenia i zmienne bez wykonania. Cofnij i Ponów zachowują maksymalnie stany wersji roboczej 100; pola tekstowe zachowują natywną edycję Ctrl+Z. Zaznacz ulubione w edytorze lub menu kafelkowym i filtruj je na pulpicie nawigacyjnym. Liczba wykonań dotyczy tylko bieżącej sesji.

Akcje tekstowe, skróty i okna uruchamiane z interfejsu aktywują ostatnie zewnętrzne okno docelowe. Paleta wyświetla kontekst docelowy. Jeśli cel został zamknięty lub Windows odmówi aktywacji, wykonanie zostanie zatrzymane z komunikatem.

## Pomoc i informacje zwrotne dotyczące klawiatury

Kreator klawiszy specjalnych znajduje się na stronie Klawiatura i na palecie. Zawiera listę klawiszy G zgłoszonych przez iCUE. Wykryj klawisz Naciśnięcie tymczasowo przechwytuje je bez wykonywania przypisanych im działań. Normalne powiązania powracają po wykryciu, anulowaniu lub zamknięciu. Funkcja katalogu i wybrany klucz otwierają się jako niezapisana wersja robocza; sprawdzane są zduplikowane powiązania.

Nakładka klawiatury pokazuje wybrany układ referencyjny i aktywne powiązania. Przytrzymaj Ctrl+Alt+F1 lub przełącz go z palety lub strony Klawiatura. Nie skupia uwagi i pozwala na przechodzenie kliknięć myszą. Zmiany profilu i powiązań są aktualizowane, gdy są widoczne; zwykłe naciśnięcia klawiszy Windows są podświetlone. Sterowanie specyficzne dla producenta może nie zapewniać równoważnego sygnału klucza Windows.

Informacje zwrotne o wykonaniu pokazują oczekiwanie, postęp kroku/powtórzenia, zakończenie, anulowanie lub niepowodzenie bez skupiania uwagi lub ujawniania wprowadzonego tekstu. Wykonanie poleceń potwierdza wykonanie polecenia, a nie niezależnie sprawdzany stan na żywo w aplikacji innej firmy. Szczegóły błędu pojawiają się w dzienniku aktywności.

## Urządzenia i Corsair

Corsair G1–G6 można przypisać bezpośrednio poprzez oficjalny pakiet SDK iCUE. Wybierz urządzenie i wpisz w edytorze akcji i zapisz lub przygotuj wersję roboczą z widoku klawiatury K100. Powiązania dotyczą wybranego urządzenia i aktywnego profilu; pauza również je wyłącza. iCUE musi być uruchomiony i zezwalać na dostęp. Fn, za pomocą tego interfejsu API G-key nie można mapować elementów sterujących kołem, profilem i blokadą iCUE.

Strona Urządzenia zawiera listę interfejsów klawiatury Windows i jest odświeżana okresowo oraz po podłączeniu urządzeń. Klawiatura fizyczna może udostępniać wiele interfejsów HID. Standardowe globalne skróty klawiszowe działają u różnych producentów.

Wykrywanie Corsair wykorzystuje dostępne informacje o urządzeniu. Sterowanie RGB wymaga kompatybilnego sprzętu, działającego iCUE i oficjalnego pakietu SDK iCUE; aplikacja raportuje faktyczną dostępność. Inni producenci obecnie otrzymują wykrywanie i standardowe działania. Poza integracją Corsair G-key, zastrzeżone elementy sterujące działają, gdy Windows odbiera je jako zwykłe dane wejściowe. Aplikacja nie instaluje sterownika klawiatury.

## Języki

Ustawienia oferują 27 języki: niemiecki, angielski, francuski, hiszpański, włoski, portugalski, holenderski, polski, czeski, słowacki, węgierski, rumuński, bułgarski, grecki, duński, szwedzki, norweski, fiński, estoński, łotewski, litewski, słoweński, chorwacki, ukraiński, rosyjski, turecki i serbski w alfabecie łacińskim. Tryb automatyczny obsługuje język wyświetlania Windows. Tłumaczenia są pakowane lokalnie. Układy, skróty klawiszowe i nazwy utworzone przez użytkownika są niezależne od języka interfejsu.

Pełna dokumentacja magazynu dostępna jest we wszystkich językach aplikacji 27 poprzez [Indeks językowy](README.md)i Tłumaczenia są obsługiwane przez maszynę, z oryginalnymi linkami źródłowymi i kontrolami strukturalnymi; recenzja mówcy jest oddzielona. Aplikacja akceptuje również kody regionalne `en-US`, `de-DE`, `fr-FR` i `it-IT` oraz wykorzystuje ich formaty numerowe. Istniejące kody językowe pozostają zgodne.

## Układy i badania producentów

Widok klawiatury zapewnia pełnowymiarowe i pozbawione kluczy numerycznych odniesienia ISO-DE/ANSI-US oraz układy Corsair K100 RGB. Geometria reprezentuje pozycje, szerokie/wysokie klawisze i ISO Enter. Układy standardowe są odniesieniami ogólnymi. Układy K100 są zgodne z udokumentowaną kontrolą bez podawania wymiarów z dokładnością do milimetra. Wybierz właściwy wariant języka/geometrii: nazwa modelu lub USB VID/PID nie określa konkretnego wariantu.

Mapowanie rozwiązuje fizyczne kody skanowania przy użyciu aktywnego układu wejściowego Windows. Przepływ pracy przygotowuje wersję roboczą i zachowuje kontrole zapisu/konfliktu. Standardowe powiązania to globalne klawisze skrótu Windows, nie izolowane dla jednej podłączonej klawiatury. Corsair G1–G6 wykorzystuje integrację specyficzną dla urządzenia iCUE. Inne zastrzeżone elementy sterujące wymagają adaptera lub skrótów skonfigurowanych przez producenta. Główny Enter i klawiatura numeryczna Enter mają wspólną tożsamość klucza wirtualnego dla globalnych skrótów klawiszowych.

W tym [Badania producentów](research/tastaturhersteller-sonderfunktionen.md) i [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokrywa 74 grupy marki / producentów, rozróżniając konfiguratory, SDK wejściowe i API oświetleniowe. [Layout dowodów](research/keyboard-layout-evidence.md) Model rejestracyjny, SKU i różnice modułów. Badania nie oznaczają wdrożenia wsparcia sprzętowego.

## Dane

Konfiguracja jest przechowywana w lokalnych danych aplikacji. Importuj/eksportuj za pomocą JSON. Importuj wyświetla podgląd konfiguracji, zastępuje bieżącą konfigurację po potwierdzeniu i wyłącza importowane powiązania do czasu ręcznego włączenia. Treści związane z działaniami tekstowymi nie są gromadzone w postaci dziennika naciśnięć klawiszy. Hasła menedżera poświadczeń są oddzielone od eksportowanych profili.

## Weryfikacja

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Kontrole Windows mogą sprawdzać sprzęt i tymczasowo konfigurować/wypuszczać obsługiwane powiązania iCUE. Uruchom je bez innego kontrolera, korzystając z tych powiązań. Kontrole punktu końcowego audio nie zmieniają głośności na żywo ani ustawień wyciszenia.

Dane wejściowe natywne nie mogą ominąć uprawnień Windows. Normalnie uruchomiony kontroler może nie wysyłać danych wejściowych do aplikacji administratora. Zarezerwowane lub zajęte klawisze skrótu pojawiają się jako konflikty.

W tym [Indeks dokumentacji](README.md) Linki historyczne, plany, badania i datowane raporty testowe. Plany historyczne opisują ich zarejestrowany zakres, a nie obecną gwarancję wsparcia funkcji.

### Szablony przepływu pracy i oczekiwania w oknie docelowym

Katalog zawiera sześć edytowalnych przepływów pracy: obszar roboczy, spotkanie, transmisja strumieniowa, badania, zmiana projektu i fragment tekstu. Wyszukaj `workflow.` w katalogu lub palecie poleceń. Wybranie przepływu pracy otwiera niezapisaną wersję roboczą; nigdy nie działa natychmiast. Wszystkie kroki zostają zachowane w przypadku dodania przepływu pracy do istniejącego makra lub przypisania go do klucza Corsair. Przed zapisaniem skonfiguruj przykładowe adresy URL, ścieżki programów, profile i sceny OBS.

Krok **Wait for target window** akceptuje nazwę procesu (z `.exe` lub bez), podciąg tytułu niewrażliwy na wielkość liter oraz limit czasu 1–600,000 ms. Wymagany jest co najmniej jeden filtr; oba filtry muszą pasować do siebie w momencie dostawy. Makro jest kontynuowane dopiero po odpowiedzi odpowiedniego widocznego okna i wysunięciu się na pierwszy plan. Użyj określonego tytułu, gdy wiele okien współużytkuje proces. Anulowanie lub przekroczenie limitu czasu powoduje zatrzymanie kolejnych kroków. Gotowość okna nie gwarantuje załadowania dokumentu lub podłączenia usługi zewnętrznej takiej jak OBS WebSocket.

Jasność wyświetlacza: wyszukaj **Zwiększ jasność ekranu**, **Zmniejsz jasność ekranu** lub **Jasność ekranu przy 50 procent** w katalog akcji. Działania względne zmieniają jasność o punkty procentowe 10; wartości stałe wahają się od 0 do 100. Obsługiwane wyświetlacze wewnętrzne korzystają z WMI; kompatybilne monitory zewnętrzne wymagają włączenia opcji DDC/CI w swoim menu. Domyślnie akcja dotyczy wszystkich obsługiwanych wyświetlaczy; w edytorze można wybrać wyraźny monitor. Połączenia i doki mogą ograniczać wsparcie.

### Rozszerzone sterowanie

Otwórz paletę poleceń, naciskając **Ctrl+K, gdy okno aplikacji ma fokus**. Wyszukaj **Projektant przepływu pracy**, **Nagraj makro**, **Synchronizuj profile** lub **Key diagnostyka** (sformułowanie jest zgodne z wybranym językiem). Projektant przepływu pracy edytuje wybraną wersję roboczą kafelków: warunki pliku/procesu/okna, liczbę pominięć do przodu i zatrzymanie/kontynuowanie obsługi błędów. Dyktafon rejestruje akordy skrótów i czeka, a nie zwykłe pisanie; Ucieczka zatrzymuje nagrywanie. Przejrzyj wynik przed jego przyjęciem.

Na etapie jasności wybierz wszystkie obsługiwane wyświetlacze lub konkretny monitor. Niedostępny wybrany monitor powoduje błąd zamiast cichej zmiany innego wyświetlacza. Pulpit nawigacyjny pokazuje jasność na żywo, odczyty dźwięku i OBS, jeśli dostępne są ich odpowiednie integracje; niedostępny odczyt nie jest prezentowany jako wartość zmierzona.

W edytorze kafelków przypisz obsługiwany fizyczny klawisz specjalny i wybierz natychmiastowe naciśnięcie, dotknięcie, przytrzymanie lub dwukrotne dotknięcie. Przytrzymane selektory warstw tymczasowo aktywują inne mapowanie i przywracają poprzednią warstwę po zwolnieniu. Gesty i warstwy dotyczą klawiszy G zestawu Corsair SDK oraz standardowych klawiszy specjalnych specyficznych dla urządzenia, a nie zwykłych globalnych klawiszy skrótu. Przechwytywanie klawiszy standardowych obsługuje klawisze multimedialne F13–F24 i klawiaturę. Zastrzeżone elementy sterujące Fn, profilami i pokrętłami nadal wymagają kompatybilnego adaptera dostawcy lub mapowania dostawcy na standardowy klucz. Raw Input obserwuje klucz i nie blokuje jego normalnego zachowania Windows. Kluczowa diagnostyka wyjaśnia problemy z profilem, pauzą, połączeniem i wiązaniem.

Synchronizacja profili umożliwia wymianę plików za pośrednictwem wybranego folderu, w tym folderu zarządzanego przez istniejącego klienta w chmurze. Przejrzyj podgląd przed importem; imports start jest wyłączony i usuwa mapowania specyficzne dla sprzętu. Wersje zapobiegają cichym nadpisaniom, a uwzględnienie treści akcji jest jawnym wyborem. MAKRIVO nie udostępnia konta w chmurze ani nie przechowuje danych uwierzytelniających w chmurze. Taca korzysta teraz z turkusowej ikony M MAKRIVO i po wstrzymaniu wyświetla bursztynową plakietkę pauzy.

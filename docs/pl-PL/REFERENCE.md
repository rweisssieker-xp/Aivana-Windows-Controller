<!-- generated-doc locale=pl-PL source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../README.md)

# MAKRIVO

[Pełna dokumentacja we wszystkich 27 językach](README.md) · [Pełny podręcznik użytkownika](HANDBOOK.md)

**przez Aivana.** [Aktualny spis funkcji i ograniczenia wydania](FEATURES.md) · [Raport z testów rozszerzeń](testing/2026-10-12-six-extensions.md) · [Prototyp lokalnego asystenta AI](testing/2026-10-12-local-meeting-assistant.md)

**Angielski (amerykański)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Lokalny kontroler klawiszy skrótu Windows z kafelkami, profilami, makrami i wykrywaniem klawiatury w stylu Stream Deck. Interfejs obsługuje języki 27. Do codziennego użytku nie jest wymagane żadne konto ani połączenie z chmurą.

[Dokumentacja](README.md) · [Weryfikacja](VERIFICATION.md) · [Raport z testów funkcji](testing/2026-10-09-usp-release.md)

## Rozpoczęcie

Po wykonaniu publikacji aplikacja znajduje się w `artifacts/win-x64/MAKRIVO.exe`. Zachowaj cały folder wynikowy razem; możesz skopiować go w inne miejsce. Pierwszą platformą docelową jest Windows 11 x64. Artefakty kompilacji są wykluczone z Git; kopia repozytorium źródłowego nie zawiera gotowego pliku wykonywalnego.

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
3. Dodaj co najmniej jeden krok: aplikacje, pliki, witryny, tekst, skróty, multimedia, głośność, przełączanie profili lub opóźnienia.
4. Zapisz. Zmiany pozostają wersją roboczą do czasu zapisania; odrzucanie przywraca zapisaną wersję. Przycisk Odtwórz i jego klawisz skrótu korzystają z tej samej ścieżki wykonania.
5. Zmień kolejność kafelków i kroków makr za pomocą przycisków „przeciągnij i upuść” lub strzałek. Szablony upraszczają konfigurację. Makra można powtarzać maksymalnie razy 100. Anuluj za pomocą interfejsu, menu zasobnika systemowego lub domyślnie Ctrl+Alt+Escape.

Profile zależne od aplikacji używają nazwy procesu bez pełnej ścieżki. Kolejność profili rozstrzyga między równoczesnymi dopasowaniami; profil domyślny jest używany w razie braku dopasowania. Automatyczny wybór profilu musi być włączony w Ustawieniach.

Zamknięcie okna głównego domyślnie pozostawia aplikację w obszarze powiadomień. Kliknij dwukrotnie jej ikonę w tym obszarze, aby ponownie otworzyć okno; zakończ aplikację z menu ikony. Uruchamianie wraz z Windows jest opcjonalne i domyślnie wyłączone. Skróty klawiszowe można centralnie wstrzymać.

## Paleta akcji i poleceń

Dodaj akcję otwiera katalog z możliwością przeszukiwania, zawierający gotowe ustawienia 90 w ośmiu kategoriach. Każdy posiada opis i podgląd. Edytor makr używa tego samego katalogu do dodawania lub zastępowania kroków. Wybranie ustawienia wstępnego w tym procesie edytora powoduje utworzenie wersji roboczej bez wykonania.

Paletę poleceń otwiera się z nagłówka lub skrótem Ctrl+K w aplikacji. Wyszukuje zapisane akcje we wszystkich profilach, ustawienia wstępne katalogu, przełączanie profili, nawigację, pauzę/wznowienie, anulowanie makr i import/eksport. Filtruj według zapisanych akcji, funkcji katalogu lub poleceń aplikacji. Strzałki wybierają element, Enter go aktywuje, Ctrl+Enter edytuje wpis możliwy do edycji, a Escape zamyka paletę. Ulubione są pierwsze. Gotowe polecenia katalogu mogą wykonać się natychmiast; ustawienia wymagające konfiguracji otwierają wersję roboczą. W Ustawieniach można zdefiniować globalny skrót palety. Nowe konfiguracje używają Ctrl+Shift+Space; istniejące nie otrzymują nowego globalnego skrótu automatycznie.

Akcje dźwiękowe ustawiają głośność dokładnie od 0 do 100 procent oraz wyciszają, przywracają dźwięk lub przełączają wyciszenie urządzeń wyjściowych i mikrofonów. Wybierz konkretny punkt końcowy albo pozostaw wybór pusty, aby użyć domyślnego urządzenia Windows. Niedostępność zapisanego punktu końcowego powoduje błąd zamiast sterowania innym urządzeniem. Odśwież urządzenia ponownie wczytuje podłączone urządzenia.

OBS Studio używa WebSocket v5: start/stop/przełączenie/wstrzymanie/wznowienie nagrywania; uruchamianie/zatrzymywanie/przełączanie przesyłania strumieniowego; zmieniać sceny; przełącz wyciszenie wejścia. Włącz serwer WebSocket w obszarze Narzędzia w OBS i ustaw jego punkt końcowy w edytorze. Sprawdź połączenie i załaduj nazwy odczytuje wersję OBS, sceny i wejścia bez rozpoczynania nagrywania lub strumienia. Wybierz nazwy z listy lub wprowadź je ręcznie. Hasła można zapisać w Windows Credential Manager i są wyłączone z eksportowanych konfiguracji. Niedostępne połączenia OBS zgłaszają błąd.

Profile mogą dopasowywać nazwę procesu i część tytułu okna. Oba podane warunki muszą być spełnione. Blokada profilu zapobiega automatycznemu przełączaniu w bieżącej sesji. Przechwytywanie warunków z pierwszego planu używa ostatniej zewnętrznej aplikacji mającej fokus.

Akcje tekstowe i schowka obsługują `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` i `{{user}}`. Daty używają formatu `yyyy-MM-dd`, a godziny `HH:mm`. Nieznane symbole zastępcze pozostają niezmienione. `{{profile}}` identyfikuje profil uruchomionej akcji; `{{process}}` identyfikuje kontekst aplikacji zewnętrznej. Zawartość schowka jest odczytywana tylko przy użyciu tego symbolu zastępczego. Akcja schowka zapisuje tekst, ale nie wkleja go automatycznie.

Akcje okna minimalizują, maksymalizują, przywracają lub ustawiają okno po lewej/prawej stronie obszaru roboczego monitora. Akcja systemowa blokuje Windows. Te akcje są uruchamiane tylko wtedy, gdy zostaną jawnie wywołane.

Podgląd pokazuje kroki, powtórzenia i zmienne bez wykonywania. Cofnij i Ponów zachowują do 100 stanów wersji roboczej; pola tekstowe zachowują natywną edycję Ctrl+Z. Oznacz ulubione w edytorze lub menu kafelka i filtruj je na pulpicie. Liczniki wykonań dotyczą tylko bieżącej sesji.

Akcje tekstowe, skróty i okna uruchamiane z interfejsu aktywują ostatnie zewnętrzne okno docelowe. Paleta wyświetla kontekst docelowy. Jeśli cel został zamknięty lub Windows odmówi aktywacji, wykonanie zostanie zatrzymane z komunikatem.

## Pomoc i informacje zwrotne dotyczące klawiatury

Kreator klawiszy specjalnych znajduje się na stronie Klawiatura i w palecie. Pokazuje klawisze G zgłaszane przez iCUE. Wykrywanie naciśnięcia klawisza tymczasowo je przechwytuje bez wykonywania przypisanych akcji. Normalne przypisania powracają po wykryciu, anulowaniu lub zamknięciu. Funkcja katalogu i wybrany klawisz otwierają się jako niezapisana wersja robocza; sprawdzane są zduplikowane przypisania.

Nakładka klawiatury pokazuje wybrany układ referencyjny i aktywne przypisania. Przytrzymaj Ctrl+Alt+F1 lub przełącz jej widoczność z palety albo strony Klawiatura. Nie przejmuje fokusu i przepuszcza kliknięcia myszy. Gdy jest widoczna, aktualizuje zmiany profilu i przypisań; zwykłe naciśnięcia klawiszy Windows są podświetlane. Elementy sterujące producenta mogą nie dostarczać równoważnego sygnału klawisza Windows.

Informacje o wykonaniu pokazują oczekiwanie, postęp kroków/powtórzeń, zakończenie, anulowanie lub błąd bez przejmowania fokusu i ujawniania wpisanego tekstu. Commands Executed potwierdza wykonanie polecenia, a nie stan aplikacji strony trzeciej niezależnie odczytany na żywo. Szczegóły błędów znajdują się w dzienniku aktywności.

## Urządzenia i Corsair

Corsair G1–G6 można przypisać bezpośrednio przez oficjalny iCUE SDK. Wybierz urządzenie i klawisz w edytorze akcji i zapisz lub przygotuj wersję roboczą z widoku klawiatury K100. Przypisania dotyczą wybranego urządzenia i aktywnego profilu; pauza również je wyłącza. iCUE musi działać i zezwalać na dostęp. Fn, kółka iCUE oraz elementów sterujących profilem i blokadą nie można mapować przez ten interfejs G-key API.

Strona Urządzenia wyświetla interfejsy klawiatur Windows i odświeża je okresowo oraz po podłączeniu urządzeń. Jedna fizyczna klawiatura może udostępniać wiele interfejsów HID. Standardowe globalne skróty działają u różnych producentów.

Wykrywanie Corsair używa dostępnych informacji o urządzeniu. Sterowanie RGB wymaga zgodnego sprzętu, działającego iCUE i oficjalnego iCUE SDK; aplikacja podaje rzeczywistą dostępność. Inni producenci obecnie otrzymują wykrywanie i standardowe akcje. Poza integracją klawiszy G Corsair zastrzeżone elementy sterujące działają, gdy Windows odbiera je jako zwykłe wejście. Aplikacja nie instaluje sterownika klawiatury.

## Języki

Ustawienia oferują 27 języków: niemiecki, angielski, francuski, hiszpański, włoski, portugalski, niderlandzki, polski, czeski, słowacki, węgierski, rumuński, bułgarski, grecki, duński, szwedzki, norweski, fiński, estoński, łotewski, litewski, słoweński, chorwacki, ukraiński, rosyjski, turecki i serbski zapisany alfabetem łacińskim. Tryb automatyczny podąża za językiem wyświetlania Windows. Tłumaczenia są dołączone lokalnie. Układy, skróty i nazwy utworzone przez użytkownika są niezależne od języka interfejsu.

Pełna dokumentacja repozytorium jest dostępna we wszystkich 27 językach aplikacji przez [Indeks językowy](README.md). Tłumaczenia są wspomagane maszynowo, zawierają linki do źródeł i podlegają kontroli strukturalnej; weryfikacja przez rodzimego użytkownika języka jest osobnym etapem. Aplikacja akceptuje także kody regionalne `en-US`, `de-DE`, `fr-FR` i `it-IT` oraz używa ich formatów liczb. Dotychczasowe kody języków pozostają zgodne.

## Układy i badania producentów

Widok klawiatury udostępnia referencyjne układy ISO-DE/ANSI-US pełnowymiarowe i bez bloku numerycznego oraz układy Corsair K100 RGB. Geometria przedstawia położenia, szerokie/wysokie klawisze i ISO Enter. Układy standardowe są ogólnymi wzorcami. Układy K100 odzwierciedlają udokumentowane elementy sterujące bez deklaracji dokładności wymiarów do milimetra. Wybierz poprawny wariant języka/geometrii: nazwa modelu lub USB VID/PID nie określa konkretnego wariantu.

Mapowanie interpretuje fizyczne kody skanowania przy użyciu aktywnego układu wejściowego Windows. Przepływ przygotowuje wersję roboczą i zachowuje kontrole zapisu/konfliktów. Standardowe przypisania są globalnymi skrótami Windows, nie ograniczają się do jednej podłączonej klawiatury. Corsair G1–G6 używają integracji iCUE dla konkretnego urządzenia. Inne zastrzeżone elementy sterujące wymagają adaptera lub skrótów skonfigurowanych przez producenta. Główny Enter i Enter bloku numerycznego mają ten sam identyfikator klawisza wirtualnego dla globalnych skrótów.

W tym [Badania producentów](research/tastaturhersteller-sonderfunktionen.md) i [CSV](research/tastaturhersteller-sonderfunktionen.csv) pokrywa 74 grupy marki / producentów, rozróżniając konfiguratory, SDK wejściowe i API oświetleniowe. [Layout dowodów](research/keyboard-layout-evidence.md) dokumentuje różnice modeli, SKU i modułów. Zakres badań nie oznacza zaimplementowanej obsługi sprzętu.

## Dane

Konfiguracja jest przechowywana w lokalnych danych aplikacji. Importuj/eksportuj za pomocą JSON. Importuj wyświetla podgląd konfiguracji, zastępuje bieżącą konfigurację po potwierdzeniu i wyłącza importowane powiązania do czasu ręcznego włączenia. Treści związane z działaniami tekstowymi nie są gromadzone w postaci dziennika naciśnięć klawiszy. Hasła menedżera poświadczeń są oddzielone od eksportowanych profili.

## Weryfikacja

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Kontrole Windows mogą sprawdzać sprzęt i tymczasowo konfigurować/zwalniać obsługiwane przypisania iCUE. Uruchamiaj je, gdy żaden inny kontroler nie używa tych przypisań. Kontrole punktów końcowych audio nie zmieniają bieżącej głośności ani ustawień wyciszenia.

Dane wejściowe natywne nie mogą ominąć uprawnień Windows. Normalnie uruchomiony kontroler może nie wysyłać danych wejściowych do aplikacji administratora. Zarezerwowane lub zajęte klawisze skrótu pojawiają się jako konflikty.

W tym [Indeks dokumentacji](README.md) zawiera linki do historycznych projektów, planów, badań i datowanych raportów z testów. Plany historyczne opisują odnotowany w nich zakres, a nie aktualną gwarancję obsługi funkcji.

### Szablony przepływu pracy i oczekiwania w oknie docelowym

Katalog zawiera sześć edytowalnych przepływów pracy: miejsce pracy, spotkanie, streaming, badania, zmiana projektu i fragment tekstu. Wyszukaj `workflow.` w katalogu lub palecie poleceń. Wybór przepływu otwiera niezapisaną wersję roboczą; nigdy nie uruchamia go natychmiast. Wszystkie kroki pozostają zachowane po dodaniu przepływu do istniejącego makra lub przypisaniu go do klawisza Corsair. Przed zapisaniem skonfiguruj przykładowe adresy URL, ścieżki programów, profile i sceny OBS.

Krok **Wait for target window** przyjmuje nazwę procesu (z `.exe` lub bez), fragment tytułu bez rozróżniania wielkości liter i limit czasu 1–600000 ms. Wymagany jest co najmniej jeden filtr; jeśli podano oba, oba muszą być spełnione. Makro jest kontynuowane dopiero, gdy dopasowane widoczne okno odpowiada i znajdzie się na pierwszym planie. Użyj konkretnego tytułu, gdy wiele okien należy do tego samego procesu. Anulowanie lub przekroczenie limitu zatrzymuje kolejne kroki. Gotowość okna nie gwarantuje załadowania dokumentu ani połączenia z usługą zewnętrzną, taką jak OBS WebSocket.

Jasność wyświetlacza: wyszukaj **Zwiększ jasność ekranu**, **Zmniejsz jasność ekranu** lub **Jasność ekranu na 50 procent** w katalogu akcji. Akcje względne zmieniają jasność o 10 punktów procentowych; wartości stałe obejmują zakres od 0 do 100. Obsługiwane wyświetlacze wewnętrzne używają WMI; zgodne monitory zewnętrzne wymagają włączenia DDC/CI w swoim menu. Domyślnie akcja obejmuje wszystkie obsługiwane wyświetlacze; w edytorze można wybrać konkretny monitor. Połączenia i stacje dokujące mogą ograniczać obsługę.

### Rozszerzone sterowanie

Otwórz paletę poleceń za pomocą **Ctrl+K, gdy okno aplikacji ma fokus**. Wyszukaj **Projektant przepływu pracy**, **Nagraj makro**, **Synchronizuj profile** lub **Diagnostyka klawiszy** (nazwy zależą od wybranego języka). Projektant przepływu pracy edytuje wybraną wersję roboczą kafelka: warunki pliku/procesu/okna, liczbę pomijanych kolejnych kroków i zatrzymanie/kontynuację po błędzie. Rejestrator przechwytuje kombinacje klawiszy skrótów i okresy oczekiwania, a nie zwykłe pisanie; Escape zatrzymuje nagrywanie. Sprawdź wynik przed przyjęciem.

Na etapie jasności wybierz wszystkie obsługiwane wyświetlacze lub konkretny monitor. Niedostępny wybrany monitor powoduje błąd zamiast cichej zmiany innego wyświetlacza. Pulpit nawigacyjny pokazuje jasność na żywo, odczyty dźwięku i OBS, jeśli dostępne są ich odpowiednie integracje; niedostępny odczyt nie jest prezentowany jako wartość zmierzona.

W edytorze kafelka przypisz obsługiwany fizyczny klawisz specjalny i wybierz natychmiastowe naciśnięcie, krótkie naciśnięcie, przytrzymanie lub podwójne naciśnięcie. Przytrzymywane selektory warstw tymczasowo aktywują inne mapowanie i przywracają poprzednią warstwę po zwolnieniu. Gesty i warstwy dotyczą klawiszy G obsługiwanych przez Corsair SDK i standardowych klawiszy specjalnych konkretnego urządzenia, a nie zwykłych globalnych skrótów. Przechwytywanie standardowych klawiszy obsługuje F13–F24 i klawisze multimedialne klawiatury. Zastrzeżone elementy Fn, profilu i pokrętła nadal wymagają zgodnego adaptera producenta lub mapowania producenta na standardowy klawisz. Raw Input obserwuje klawisz i nie blokuje jego normalnego zachowania w Windows. Diagnostyka klawiszy wyjaśnia problemy profilu, pauzy, połączenia i przypisań.

Synchronizacja profili wymienia pliki przez wybrany folder, w tym folder zarządzany przez dotychczasowego klienta chmury. Przed importem sprawdź podgląd; zaimportowane akcje są początkowo wyłączone, a mapowania zależne od sprzętu są usuwane. Rewizje zapobiegają cichemu nadpisywaniu, a dołączenie treści akcji wymaga jawnego wyboru. MAKRIVO nie zapewnia konta w chmurze i nie przechowuje danych uwierzytelniających do chmury. Ikona w obszarze powiadomień używa teraz własnej turkusowej litery M MAKRIVO i pokazuje bursztynowe oznaczenie pauzy po wstrzymaniu.

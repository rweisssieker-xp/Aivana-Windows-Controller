<!-- generated-doc locale=pl-PL source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../research/obs-integration.md)

# Integracja OBS Studio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana wykorzystuje protokół obs-websocket v5 OBS Studio przez ClientWebSocket; nie instaluje biblioteki klienta OBS innej firmy. Protokół otwiera się serwerem Hello (kod operacji 0), odpowiada za pomocą identyfikacji (kod operacji 1), czeka na Identified (kod operacji 2), wysyła żądanie umieszczone na białej liście (kod operacji 6), a następnie dopasowuje swoją odpowiedź na żądanie (kod operacji 7) według identyfikatora żądania. Uwierzytelnianie hasłem następuje po udokumentowanym dwuetapowym obliczeniu wyzwania SHA-256 OBS.

Włącz **Tools → WebSocket Ustawienia serwera** w OBS. Domyślny punkt końcowy to **ws://127.0.0.1:4455**. Ustaw pole akcji **OBS-WebSocket-Adresse** dla innego punktu końcowego. Zdalny punkt końcowy musi używać wss://; niezaszyfrowany ws:// jest akceptowany tylko w przypadku adresu zwrotnego. Punkt końcowy może również zawierać ścieżkę lub port niestandardowy. Aivana odrzuca osadzone poświadczenia adresu URL.

Gdy OBS ma włączone uwierzytelnianie, zapisz jego hasło z pliku OBS redaktor akcji. Aivana przechowuje go w bieżącym Windows Menedżera poświadczeń użytkownika jako ogólne poświadczenie o nazwie **Aivana.OBS:<znormalizowany punkt końcowy>**. Nigdy nie jest zapisywany w pliku akcji lub ustawień. W przypadku wdrożeń zarządzanych **AIVANA_OBS_HASŁO** pozostaje dostępne jako rozwiązanie awaryjne, gdy punkt końcowy nie ma zapisanego hasła. OBS mogą pozostać nieuwierzytelnione, w takim przypadku żadne z nich nie jest potrzebne. Unikaj argumentów wiersza poleceń dla hasła, ponieważ można sprawdzić wiersze poleceń procesu.

Katalog sterowników i obsługa konfiguracji **StartRecord**, **StopRecord**, **ToggleRecord**, **PauzaNagrywanie**, **WznówNagrywanie**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** i **Przełącz wejścieWyciszenie**. Pierwsze osiem nie przyjmuje żadnych argumentów. SetCurrentProgramScene używa nazwy sceny edytora; ToggleInputMute używa swojej nazwy wejściowej. Klient protokołu akceptuje tę samą listę dozwolonych.

Każda akcja otrzymuje nowe połączenie z ograniczeniem sekundowym 10 i bez automatycznego ponawiania. Ma to znaczenie w przypadku poleceń przełączających: po wysłaniu żądania przekroczenie limitu czasu lub zerwanie połączenia może pozostawić wynik nieznany, a ponowna próba może ponownie przełączyć stan. Niepowodzenie żądań OBS jest zwracane wraz z kodem statusu i (jeśli jest podany) komentarzem serwera.

Odniesienia do protokołu:

- [OBS WebSocket specyfikacja protokołu v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket formuła weryfikacji i opkody wiadomości](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket Wdrożenie wniosku o wejście medialnego i akceptowane wartości enum](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Notatki do protokołu i nazwy operacji sprawdzono z dokumentacją projektową OBS na 2026-10-08.

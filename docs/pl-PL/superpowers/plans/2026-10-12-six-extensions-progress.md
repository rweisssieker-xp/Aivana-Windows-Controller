<!-- generated-doc locale=pl-PL source=docs/superpowers/plans/2026-10-12-six-extensions-progress.md sha256=aaa5db1c77f8f730220146a230868b9df95d87653b48708f49aa1418db56fa5d -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/plans/2026-10-12-six-extensions-progress.md)

# Księga wykonawcza: 2026-10-12-sześć-rozszerzeń
Autoryzowana przez użytkownika implementacja natywna bez dalszych pytań.
Orzeczenie: użyj nowej gałęzi codex/six-extensions w bieżącej kasie, aby zachować istniejące niezatwierdzone funkcje; świeże drzewo robocze pominęłoby zależności. Podczas testów nie ma potrzeby instalowania ani zmieniania sterowników.
Preflight: kreator kanałów weryfikacyjnych/UIA/wtyczka/widoki wyników mobilnych; możliwości producenta obsługują mapowanie klawiatury i zaplecze sterowników. Istniejące wartości wyliczeniowe muszą pozostać stabilne.
Zadanie 1: w trakcie; wprowadzono nieudany test weryfikacyjny.

Zadania 1–3: wdrożono weryfikację umów/dostawcy/edytora/historii i kreatora; podstawowe kontrole zaliczone; Akceptacja interfejsu użytkownika jest wciąż rozszerzana.
Zadania 4–5: zaimplementowano izolowanego pracownika UIA, pickera i rejestratora sprawdzonych kliknięć; Testy rzeczywistego urządzenia przeszły pomyślnie wywołanie/niejednoznaczność/wyłączenie/wyłączenie hasła. Znaleziono/naprawiono obcięcie nazwy procesu z kropkami.
Zadania 6–7: przegląd pakietu/import/wyłączenie włączenia/eksportu/odinstalowania i zaimplementowano izolowany plik wykonywalny IPC; prawdziwe próbki przepustek IPC. Brak publicznego rynku.
Zadania 8–9: Panel HTTPS, parowanie, hashowane dane uwierzytelniające, chroniony klucz TLS i zaimplementowano IPC na liście dozwolonych; Oczekiwanie na weryfikację HTTP/mobilną.
Zadania 10–11: fizyczne kody skanujące, zaimplementowano adaptery analogowe Logitech G SDK i Wooting oraz protokoły RGB; nie testowano żadnego kompatybilnego sprzętu dostawcy poza istniejącym Corsair.
Zadanie 12: Pozostała brama zewnętrzna: podpisana/licencjonowana przez producenta Windows 11 ekskluzywny filtr niedostępny. Nie zastępuj fałszywego tłumienia.
Orzeczenie: buduj towarzyszy przy użyciu celów MSBuild; opublikuj wszystkie hosty razem, aby umożliwić korzystanie z aplikacji UIA/mobile.
Orzeczenie: brak zastrzeżonej, redystrybucyjnej biblioteki SDK DLL w pakiecie bez zweryfikowanych praw; jawne lokalne zatwierdzenie zestawu SDK miesza wybrany plik binarny.

Recenzja końcowa: świeży recenzent /root/final_review dokonał przeglądu bieżącego drzewa roboczego, planu/specyfikacji i bramek wydań. Brak zmian recenzenta.
Wersja ostateczna: naprawiono spóźnione polecenia dyspozytora mobilnego poprzez zastosowanie anulowania żądań przy wysyłce i wywołaniu zwrotnym.
Wersja ostateczna: naprawiono brakującą awarię executora/stan terminala weryfikacji anulowania; regresja niepowodzeń wykonania CZERWONY → ZIELONY; Zachowany stan limitu czasu przed inspekcją.
Wersja ostateczna: naprawiono weryfikację procesu opartego wyłącznie na nazwie podstawowej poprzez porównanie bezwzględnej ścieżki MainModule lub raportowanie nieweryfikowalności.
Wersja ostateczna: poprawiony cykl życia wtyczki z kontrolą przywracania/odrzucania; nowa kompilacja testu API CZERWONY → ZIELONY; wyłączenie anuluje aktywnych pracowników; brakujące zależności odrzucają wywołanie.
Wersja ostateczna: naprawiono restart pulsu w przypadku nieudanych sesji Razer/GameSense; brak dostępnego testu fizycznego urządzenia RGB.
Wersja ostateczna: dodano weryfikację konfliktów gestów/warstw dostawcy, kompilacja testowa CZERWONY → ZIELONY.
Finał: prawdziwy HTTPS CZERWONY → ZIELONY: Schannel odrzuca efemeryczne klucze serwera; użyj tymczasowego kontenera kluczy użytkownika i trwałego PFX chronionego DPAPI, zerowego zarządzanego PFX w postaci zwykłego tekstu.
Wersja ostateczna: Budżet działań UIA obejmuje ograniczony dodatek dla pracowników na rozpoczęcie działalności; pracownik zawsze zabijany w przypadku nieprawidłowego wyjścia.
Wynik końcowy: odtworzenie przejściowego błędu odczytu DDC (pusta migawka, a następnie stabilny ten sam identyfikator monitora); ograniczona ponowna próba odczytu przechodzi oryginalny test stabilnego wyliczenia bez zapisywania jasności.
Zadanie 13: kompilacja wydania 0 ostrzeżenia/błędy; pakiet podstawowy zielony; Windows zielony zestaw z wyraźnym --pomiń-urządzenie-pierwszego planu; oryginalny pełny pakiet ma natywną awarię aktywacji pierwszego planu, zachowany. Prawdziwe UIA, wtyczka IPC i HTTPS sprawdzają kolor zielony. WPF dymny zielony. Układy telefonów i tabletów Edge/Playwright zielone z próbą API, rzeczywisty protokół HTTPS osobno zielony. Pełny, samodzielny pakiet wygenerowany przez Tools/publish-complete.ps1 z powiadomieniami o hostach/assets/example/runtime.
Orzeczenie: zakaz łączenia/zatwierdzania mieszanych, istniejących wcześniej edycji użytkownika. Pozostaw zmiany i rozgałęzienia na miejscu. Brak menu końcowego, ponieważ użytkownik nie zażądał żadnych pytań, a kryteria pełnej wersji nie zostały spełnione.
Orzeczenie: Zadanie 12 niekompletne: podpisany/licencjonowany filtr produkcyjny i możliwe do odzyskania testy sprzętu z dwiema klawiaturami są niedostępne. Brak zainstalowanego sterownika. Nie żądano wyłącznego tłumienia.
Orzeczenie: akceptacja sprzętu, akceptacja prawdziwej mobilnej sieci LAN, wysokie DPI 150/200 i całkowicie nowe tłumaczenia pozostają niezweryfikowane; szczegółowe ujawnienie docs/testing/2026-10-12-six-extensions.md. Ogólna prośba nie została w pełni zaakceptowana pomimo działających dodatków do oprogramowania.

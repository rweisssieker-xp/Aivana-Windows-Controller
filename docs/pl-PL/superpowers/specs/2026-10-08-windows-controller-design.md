<!-- generated-doc locale=pl-PL source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Wersja robocza

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stan: 8. Październik 2026. Status: do przeglądu przed wdrożeniem.

## Cel i uzgodniony zakres

Samodzielna aplikacja Windows zamienia zwykłą klawiaturę komputera PC w dowolnie konfigurowalny panel akcji, podobny do odtwarzacza strumieniowego. Użytkownik potwierdził wszystkie sugerowane typy akcji, makra, wykrywanie urządzeń, profile i dobry GUI. Rozpoznawane są podłączone Corsair i inne klawiatury; dodatkowe funkcje producenta są oferowane tylko wtedy, gdy są faktycznie obsługiwane.

Założenia: interfejs niemiecki, użytkowanie lokalne bez konta, Windows 11 x64 jako pierwszy cel, brak instalacji sterowników. Klawiatura pozostaje użyteczna do normalnego pisania. Domyślnie w mapowaniach globalnych używane są skróty klawiaturowe z modyfikatorami. Klawisze poszczególnych liter nie są domyślnie zarezerwowane globalnie.

## Decyzja techniczna

Natywny C#/.NET-10-App z WPF i MVVM. Stabilny plik .NET-10-SDK jest dostępny w obszarze roboczym. WPF umożliwia spersonalizowany interfejs pulpitu i bezpośrednią integrację Windows. Alternatywą byłby interfejs sieciowy z oddzielną usługą Windows lub wieloplatformowa aplikacja komputerowa; oba wnoszą dodatkową złożoność czasu wykonania i komunikacji do tego projektu Windows. Wybraną podstawą jest zatem wariant natywny.

Dział: Projekt desktopowy dla widoków i zasobnika, projekt podstawowy dla logiki konfiguracji i wykonania, projekt Windows dla skrótów klawiszowych, symulacji wprowadzania danych i urządzeń, projekt testowy dla reguł niezależnych od platformy. Adaptery producenta zależą od interfejsu o stałych możliwościach i są niezależne od standardowych działań.

## powierzchnia

Ciemny design ze spokojnymi powierzchniami w kolorze antracytu, czytelną czcionką i turkusowymi akcentami. Linki do nawigacji w panelu, profilach, urządzeniach i ustawieniach. Aktywny profil, wyszukaj i przełącz, aby wstrzymać wszystkie klawisze skrótu u góry. Interfejs pozostaje użyteczny przy skalowaniu 100–200 procent Windows.

Pulpit nawigacyjny zawiera konfigurowalną siatkę kafelków. Każdy kafelek pokazuje ikonę, nazwę, klawisz skrótu i ​​status wykonania. Kliknięcie powoduje tę samą akcję co klawisz skrótu. Po prawej stronie otwiera się edytor z typem akcji, parametrami, nagrywaniem skrótów klawiszowych, symbolem/kolorem, przyciskiem aktywacji i testu. Dostępne są opcje Nowe, Duplikuj, Usuń i Zmień kolejność. Widok klawiatury pokazuje schematycznie przypisania dla standardowego układu niemieckiego; nie podaje dokładnej geometrii wykrytego urządzenia.

Profile można tworzyć, kopiować, zmieniać nazwy i aktywować ręcznie. Nazwa procesu jest opcjonalnie przypisana do profilu. Odpowiedni program pierwszoplanowy aktywuje swój profil, w przeciwnym razie obowiązuje globalny profil standardowy. Jeżeli istnieje kilka pasujących profili, obowiązuje wyraźnie widoczna kolejność. Edytor wyjaśnia konflikty skrótów klawiszowych bezpośrednio w zadaniu.

## Skróty klawiszowe i wejścia

Windows RegisterHotKey obsługuje normalne kombinacje globalne z wyłączonym powtarzaniem klawiszy. Rejestrowany jest tylko aktywny profil. Zarezerwowane lub już zajęte kombinacje pojawiają się jako błędy; aplikacja nie zgłasza, że ​​została pomyślnie aktywowana. Nagranie w edytorze nie powoduje żadnych działań.

Przed symulowanymi wejściami zwalniane są wyzwalające klawisze modyfikujące lub czekają na fizyczne zwolnienie. Tekst Unicode i kombinacje klawiszy są wysyłane za pomocą SendInput. Każdy naciśnięty klawisz zostanie ponownie zwolniony, nawet jeśli anulujesz. Akcje działają poza wątkiem interfejsu użytkownika; Zmiany wykonania i profilu nie mogą blokować powierzchni.

## Akcje i makra

Typy akcji: Uruchom program z argumentami i katalogiem roboczym, otwórz plik/folder przez Windows, otwórz witrynę HTTP/HTTPS, wprowadź tekst Unicode, wyślij kombinację klawiszy, odtwarzanie multimediów/pauza/następny/poprzedni/zatrzymaj, zwiększanie/zmniejszanie głośności/wyciszanie i przełączanie profilu. Parametry są sprawdzane przed zapisaniem i ponownie przed wykonaniem. Programy uruchamiane są bezpośrednio; Argumenty nie są domyślnie interpretowane jako polecenia powłoki.

Makra zawierają uporządkowaną listę nazwanych akcji i jawne kroki pauzy. Kroki można dodawać, edytować i przenosić. Wreszcie można skonfigurować powtórzenia (1–100); Nieskończone pętle nie są częścią pierwszej wersji. Działające makro można zakończyć za pomocą interfejsu lub własnego skrótu anulowania. Powtarzające się wyzwalanie tego samego przyporządkowania prądu nie rozpoczyna biegu równoległego. Różne makra z symulacją wprowadzania danych są serializowane, dzięki czemu ich naciśnięcia klawiszy nie mieszają się. Jeżeli wystąpi błąd kroku, makro zatrzyma się i wyświetli krok, którego to dotyczy.

## Urządzenia i producenci

Inwentaryzacja urządzeń poprzez Windows Raw Input i dodatkowe informacje o urządzeniu: nazwa wyświetlana, ścieżka urządzenia, identyfikator producenta/produktu, jeśli jest dostępny, stan połączenia i wykryte możliwości. Podłączanie/odłączanie aktualizuje listę bez ponownego uruchamiania. Urządzenie fizyczne może mieć wiele wpisów HID; Są one pogrupowane według wiarygodnych identyfikatorów, w przeciwnym razie są przechowywane w widoczny sposób jako osobne wpisy.

Normalne klawisze skrótu mają zastosowanie na wszystkich klawiaturach. Widok urządzenia dostarcza informacji i możliwości; oddzielna klawiatura z wyłącznie przechwyconymi klawiszami nie jest częścią tego uzgodnionego trybu.

Wykrywanie Corsair działa również bez iCUE. Opcjonalny adapter iCUE SDK zapewnia stan połączenia i obsługiwane działania świetlne, gdy obecne są SDK, iCUE i kompatybilne urządzenie. Interfejs użytkownika rozróżnia rozpoznane, dostępne SDK, podłączone i faktycznie kontrolowane. SDK przede wszystkim dokumentuje sterowanie diodami LED; Zastrzeżone klawisze G są obsługiwane jak zwykłe klawisze skrótu tylko wtedy, gdy Windows otrzyma odpowiednie dane wejściowe. Nie ma ogólnej obietnicy dotyczącej wszystkich kluczy specjalnych.

Inni producenci początkowo otrzymują ogólne wykrywanie i standardowe działania. Interfejs adaptera umożliwia późniejsze integracje; umiejętności, które nie są wspierane, są uczciwie prezentowane. Testy sprzętowe dla nieistniejących modeli nie są zgłaszane jako zaliczone.

## Przechowywanie i życie codzienne

Wersjonowana konfiguracja JSON w lokalnym katalogu danych aplikacji. Zapis atomowy z ostatnią ważną kopią zapasową; nieprawidłowe pliki nie są dyskretnie nadpisywane. Import sprawdza format, wersję, identyfikatory i parametry oraz wyświetla podgląd przed zastosowaniem. Zaimportowane akcje można wykonać dopiero po wyraźnej aktywacji. Eksport zawiera profile i przypisania.

Menu zasobnika: Otwórz, wybierz profil, wstrzymaj klawisze skrótu, anuluj makra, wyjdź. Zamknij okno domyślnie minimalizuje do zasobnika i wyjaśnia to za pierwszym razem. Autostart jest opcjonalny i domyślnie wyłączony. Tylko jedna instancja zarządza skrótami klawiszowymi. Exit rejestruje skróty klawiszowe, przerywa wykonywanie i zwalnia zasoby.

Widok aktywności lokalnej z ograniczoną historią sukcesów, niepowodzeń i anulowań. Brak dziennika wszystkich naciśnięć klawiszy. Treść akcji tekstowej nie jest podana w komunikatach o błędach. Brak usługi w chmurze i brak telemetrii.

## Obsługa błędów i ograniczenia

Brakujące programy, nieprawidłowe adresy URL, zajęte klawisze skrótów, uszkodzone profile i nieosiągalny zestaw SDK producenta otrzymują określone komunikaty w języku niemieckim. Awaria adaptera nie ma wpływu na standardowe działania. Wpisy do aplikacji o wyższym uprzywilejowaniu mogą się nie powieść ze względu na limity uprawnień Windows; Błędy są wyjaśnione, aplikacja domyślnie nie wymaga uprawnień administratora.

## Testowanie i akceptacja

Zautomatyzowane sprawdzanie priorytetu profilu, wykrywanie konfliktów, import konfiguracji, przechowywanie atomowe, porządkowanie makr, ponowne próby, anulowanie i propagację błędów. Windows testy dymne dla prawdziwych skrótów klawiszowych, skrótów tekstowych i klawiaturowych, akcji multimedialnych, inwentarza urządzeń, zasobnika i czystego wyjścia. Żadnych testów, które przypadkowo uruchamiają dowolne programy lub strony internetowe.

Kontrola GUI: tworzenie i edytowanie przypisań, nagrywanie bez wykonywania, klikanie/klawisz skrótu z tym samym efektem, anulowanie makra, komunikat o konflikcie, zmiana profilu, widok urządzenia, import/eksport i skalowanie. Testy sprzętu specyficzne dla producenta są dokumentowane oddzielnie dla rzeczywistego urządzenia.

Dostawa: kod źródłowy, powtarzalne instrukcje budowania/uruchamiania, niemiecka instrukcja obsługi i lokalna Windows-x64-Publish-Artefakt. Sukces oznacza wykonywalną aplikację ze wszystkimi ogólnymi funkcjami; Corsair-Możliwości są oznaczone jako dostępne tylko ze sprawdzoną obsługą SDK/urządzenia.

## Źródła techniczne

- Microsoft Wejście z klawiatury/rejestracjaHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Wejście: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Oficjalny pakiet SDK Corsair iCUE: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Następny krok

Po zapoznaniu się z tym projektem: utwórz konkretny plan wdrożenia i przedstaw go do wyboru wdrożenia. W pustym folderze roboczym nie ma jeszcze repozytorium Git; Dlatego też dokument ten nie został jeszcze zatwierdzony.

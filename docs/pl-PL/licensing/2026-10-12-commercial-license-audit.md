<!-- generated-doc locale=pl-PL source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Sprawdź, czy nie ma jednorazowej sprzedaży komercyjnej

Stan: 12. Październik 2026. Producent: Aivana.

## Wynik

W skontrolowanym inwentarzu nie stwierdzono ogólnego obowiązku ujawniania własnego kodu źródłowego MAKRIVO. Własna jednorazowa sprzedaż wydaje się możliwa, ale nie jest jeszcze w pełni przygotowana. W szczególności nie udokumentowano zezwolenia na redystrybucję istniejącej biblioteki Corsair-SDK-DLL. Ta recenzja nie zastępuje zgody prawnej.

## Ustalenia techniczne i licencyjne

| komponent | Ustalenia | Wymagany krok |
| --- | --- | --- |
| Własny kod źródłowy | W zbadanym spisie Git i jego lokalnie dostępnej historii plików licencyjnych nie znaleziono żadnej własnej LICENCJI, KOPIOWANIA ani POWIADOMIENIA. | Potwierdź własność praw, w tym składek, przyjętych fragmentów kodu i ewentualnych praw pracodawcy; stwórz własne warunki użytkowania. |
| Pulpit .NET/Windows | Badany pakiet samodzielny pod `artifacts/makrivo-win-x64` zawiera wersję 10.0.6. Lokalne pakiety wykonawcze zawierają licencje MIT, które umożliwiają dystrybucję komercyjną i wymagają dołączenia powiadomień. W zbadanych folderach wyjściowych nie znaleziono odpowiednich plików licencji/powiadomień. | Dołącz dokładne teksty licencji i odpowiednie informacje stron trzecich dotyczące środowiska wykonawczego faktycznie dostarczonego w pakiecie sprzedażnym. |
| Zależności NuGet | W zbadanych plikach projektu nie znaleziono bezpośrednich wpisów PackageReference. Badany plik zasobów pulpitu zawiera własne zależności projektu. Pakiety wykonawcze nadal pozostają istotne dla licencji. | Po ostatecznym wydaniu dokonaj ponownej inwentaryzacji wszystkich projektów, pakietów przejściowych i dołączonych plików. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, wersja pliku 4.0.84.0. Lokalny plik PDF z dokumentacją SDK zawiera na drukowanych stronach 35–38 umowę EULA: użytkowanie bez opłat licencyjnych, ale z ograniczeniami dotyczącymi przenoszenia/cesji. Nie było wyraźnego zezwolenia na przekazywanie biblioteki DLL klientom. | Przed łączeniem uzyskaj solidne pozwolenie na redystrybucję. Alternatywnie sprawdź pakiet bez biblioteki SDK DLL, która korzysta z pakietu SDK zainstalowanego przez klienta; Obowiązują również w tym przypadku warunki pakietu SDK. |
| Różne pakiety | `artifacts/makrivo-win-x64` nie zawiera biblioteki DLL Corsair; `artifacts/win-x64` je zawiera. Aplikacja dynamicznie wyszukuje SDK w wielu lokalizacjach. | Ustaw rzeczywisty instalator detaliczny i sprawdź jego zawartość. Samo istnienie pakietu bez biblioteki DLL nie potwierdza wszystkich praw do integracji. |
| OBS | Własny klient WebSocket w badanym kodzie C#; nie zidentyfikowano pakietu klienta OBS, pliku binarnego OBS lub przyjętego kodu GPL. | Zaobserwowana integracja protokołów sama w sobie nie uzasadnia żadnego ustanowionego obowiązku ujawniania informacji na podstawie licencji GPL. Pochodzenie wszelkich fragmentów, które mogły zostać zaadoptowane, pozostaje do potwierdzenia. |
| Ikony aplikacji | Lokalny skrypt Python rysuje symbole geometryczne za pomocą Pillow. Narzędzie jest zależnością kompilacji. | Dokumentuj pochodzenie i prawa wszystkich faktycznie dostarczonych obrazów, dźwięków, czcionek i innych zasobów. Sam scenariusz nie potwierdza praw do każdego zasobu. |
| Zarządzanie licencjami | W badanym kodzie aplikacji nie zidentyfikowano pełnej kontroli licencji/aktywacji zakupu. | W razie potrzeby zaimplementuj podpisane pliki licencji offline. Aktywacja techniczna i warunki prawne użytkowania to odrębne zadania. |

## Rozsądny model zakupów – jeszcze nie wdrożony

- płatność jednorazowa; Stałe prawo do użytkowania zakupionej wersji głównej.
- Jedna osoba na maksymalnie dwóch własnych komputerach; Wyraźnie zezwalaj na profesjonalne zastosowanie.
- Dołącz poprawki błędów i drobne aktualizacje; Opcjonalnie oferuj późniejsze wersje główne za opłatą.
- Podpisany cyfrowo plik licencji, który można sprawdzić w trybie offline; Nigdy nie dostarczaj prywatnych kluczy do podpisu wraz z aplikacją.
- Prawa prawne i wymagane aktualizacje zabezpieczeń nie mogą być całkowicie wyłączone na mocy zasady wersji głównej.

## Otwarte przed pierwszą sprzedażą

1. Corsair Wyjaśnij prawa do dystrybucji lub przygotuj się do sprzedaży bez dołączonej biblioteki SDK DLL.
2. Dołącz teksty licencji i innych firm dotyczące dokładnych zależności wersji i odbierz je w instalatorze.
3. Potwierdź własność praw do własnego kodu i zasobów; Utwórz umowę EULA z jasnymi zasadami dotyczącymi urządzenia, użytkownika i aktualizacji.
4. W przypadku sprzedaży konsumenckiej zajmij się realizacją transakcji, w tym wymaganiami informacyjnymi i anulowaniem. Ogólne oświadczenie „brak zwrotu pieniędzy za pobranie” nie wystarczy. Wygaśnięcie prawa odstąpienia od umowy w przypadku natychmiastowego udostępnienia towaru wymaga spełnienia wymogów prawa.
5. Sprawdź ponownie gotowy instalator, łącznie z wtyczkami i wszystkimi zależnościami przechodnimi. Kopia robocza jest rozwijana równolegle; Spis ten nie stanowi wydania przyszłych plików.

## GitHub

Badane repozytorium jest publiczne. Bez licencji open source kod nie staje się automatycznie bezpłatny do dalszego wykorzystania. Należy nadal przestrzegać praw platformy GitHub do repozytoriów publicznych. Późniejsze przejście na prywatny nie powoduje usunięcia istniejących kopii ani rozwidleń. Widoczność i licencja nie uległy zmianie podczas tego przeglądu.

## Dowody i źródła

- Lokalne licencje .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` i `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Lokalne uwagi dotyczące stron trzecich .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Lokalny zestaw SDK Corsair: `artifacts/dependencies/iCUESDK_4.0.84.zip`; wyodrębniony plik referencyjny PDF pod adresem `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 Licencja MIT](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair Dokumentacja SDK z EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licencjonowanie magazynów](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Aktualizacje](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – prawo odwołania](https://www.gesetze-im-internet.de/bgb/__356.html)

Niesprawdzone: pełna historia wkładów zewnętrznych i łańcuchy praw, wydanie znaku towarowego, istniejąca kasa sprzedaży, przyszłe pakiety wtyczek, pełna skuteczność prawna jeszcze niedostępnej umowy EULA.

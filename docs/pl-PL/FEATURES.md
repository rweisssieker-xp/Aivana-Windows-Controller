<!-- generated-doc locale=pl-PL source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../FEATURES.md)

# MAKRIVO – przegląd funkcji

Producent: **Aivana**. Stan: **12. Październik 2026**. To jest stan rozwoju, a nie sprzedaż czy wydanie sprzętu.

## Sterowanie Windows

- Kafelki, globalne skróty klawiszowe, paleta poleceń, ulubione i profile zależne od aplikacji.
- Wieloetapowe, powtarzanie, wstrzymywanie, anulowanie i podgląd makr.
- Otwarte programy, pliki i strony internetowe; Używaj tekstu, schowka i skrótów klawiaturowych.
- Uporządkuj okna, zablokuj Windows, kontroluj wyjścia audio i mikrofony oraz zmieniaj jasność ekranu za pomocą obsługiwanych interfejsów.
- Integracja OBS-WebSocket-v5 dla scen, nagrywania, streamingu i dźwięku.
- Szablony, Asystent miejsca pracy, wykrywanie urządzeń i nakładka klawiatury.
- Import i eksport konfiguracji profili; Przegląd projektu i wykrywanie konfliktów.
- Ustawienia zapisane lokalnie; do codziennego użytku nie jest wymagane konto.

## Zaawansowane funkcje w bieżącym kodzie źródłowym

| obszar | Zakres funkcji | Granice |
| --- | --- | --- |
| Kontrola wyników | Kontrola wstępna, kontrola po wykonaniu i historia weryfikacji | Potwierdzaj tylko weryfikowalne efekty; Pomyślne naciśnięcie klawisza nie potwierdza efektu docelowego. |
| Asystent miejsca pracy | Przygotuj profile i procesy z wybranych programów, plików, adresów URL i innych działań | Wymagane są podgląd i zapisanie; konfiguracja nie wykonuje żadnych akcji. |
| Nagrywanie makr i przepływy pracy | Nagrywanie, warunki, gesty i synchronizacja profili lokalnych | Zachowanie i ograniczenia są dokumentowane w raportach z wdrożenia i testów. |
| Automatyzacja interfejsu użytkownika | Wybierz cele, sprawdź selektory, nagrywaj akcje i ograniczone sekwencje kliknięć | Oddzielny pracownik; cele niejednoznaczne, wyłączone i hasła są odrzucane; brak ogólnego nagrywania wprowadzania tekstu. |
| Rozszerzenia | Sprawdzaj, importuj, aktywuj w oparciu o jawne zaufanie, eksportuj i wycofuj pakiety | Izolacja procesu nie jest piaskownicą; nie jest publicznym rynkiem wtyczek. |
| Panel mobilny | Lokalny panel HTTPS, parowanie, wybrane profile i historia statusu/audytu | Prawdziwa akceptacja sieci LAN telefonu/tabletu jest nadal otwarta; brak uniwersalnej deski rozdzielczej z czujnikami. |
| Adapter producenta | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB i adapter analogowy Wooting | Zależne od modelu/SDK; nowe adaptery nie są w pełni sprawdzone z rzeczywistym sprzętem. Brak dołączonych nowych, zastrzeżonych bibliotek DLL SDK. |
| Mapowanie klawiatury fizycznej | Rozpoznawanie standardowych klawiszy na podstawie urządzenia i kodu skanowania | Raw Input nie blokuje normalnego wejścia Windows. |
| Lokalny asystent spotkań AI | Opcjonalny prototyp dla Ollama, LM Studio, llama.cpp i ONNX Runtime GenAI CPU tworzy ograniczone projekty z wyraźnie wybranych zasobów | Domyślnie wyłączony; natywne środowisko wykonawcze CPU sprawdzono z modelem losowym, lecz nie wykazano jakości planowania ani oszczędności czasu; wagi modeli nie są dołączone. |

## Nie jest jeszcze dostępna jako gotowa funkcja sprzedaży

- **Wyłączna obsługa drugiej klawiatury/makropada:** Brakuje podpisanego, licencjonowanego i przetestowanego sprzętowo sterownika filtrującego. Zwykłe naciśnięcia klawiszy nadal mogą docierać do Windows.
- Pełna akceptacja sprzętowa adapterów wszystkich producentów oraz testy sieci LAN w rzeczywistych urządzeniach mobilnych.
- Prawdziwe mieszane zmiany monitora/DPI i zatwierdzenie techniczne wszystkich nowych tłumaczeń. sprawdzono PerMonitorV2 i symulowany 150/200-Prozent-Layouts; Testy rzeczywistego wyświetlacza pozostają odrębne. Interfejs oferuje języki 27; Szczegółowe nowe teksty nie zostały jeszcze w pełni zatwierdzone językowo.
- Podpisany instalator detaliczny, ostateczna umowa EULA, zweryfikowane prawa do dystrybucji pakietu SDK oraz integracja licencji/kasy produktywnego zakupu.

## Szczegóły i dowody

- [Dokumenty obsługi](README.md)
- [Rozszerzenia: funkcje, testy i otwarte granice](testing/2026-10-12-six-extensions.md)
- [Lokalny asystent inteligencji: prototyp i poziom testów](testing/2026-10-12-local-meeting-assistant.md)
- [Lokalne terminy i wymagania dotyczące modelu](testing/2026-10-12-local-ai-runtimes.md)
- [Testy DPI i skalowania](testing/2026-10-12-dpi-scaling.md)
- [Przegląd licencji](licensing/2026-10-12-commercial-license-audit.md) – historyczne nagrywanie; nowy scenariusz publikacji uzupełnia wskazówki dotyczące biegu. Ostateczny pakiet należy zbadać oddzielnie.
- [Platforma sprzedaży](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – stan porównawczy; Stripe jest dodatkowo możliwą platformą płatniczą, zobacz aktualne warunki dostawców.

## Wygeneruj kompletny pakiet programistyczny

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Pakiet zawiera aplikację desktopową, proces roboczy UI-Automation-Worker, host mobilny, pliki przeglądarki, przykładową wtyczkę i informacje o licencjach środowiska wykonawczego. Artefakty kompilacji nie są dodawane do Git. Stan kodu źródłowego na GitHub nie jest jeszcze opublikowanym instalatorem Windows.

<!-- generated-doc locale=pl-PL source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Widok klawiatury fizycznej do mapowania

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cel

Widok klawiatury ma na celu pokazanie rzeczywistych pozycji klawiszy podczas przypisywania akcji. Linie o tej samej szerokości nie wystarczą: Enter, Shift, Spacja, Nawigacja, klawiatura numeryczna i dodatkowe elementy sterujące producenta różnią się.

## Wdrożenie

- Katalog z wydzielonymi identyfikatorami fizycznymi, geometrią w kluczowych jednostkach, drukowanymi legendami i kodami wejściowymi Windows.
- ISO-DE i ANSI-US, każdy w pełnym formacie i TKL; dodatkowo Corsair K100 RGB, który został rozpoznany jako model podłączony poprzez iCUE.
- ISO Enter w kształcie litery L, klawiatura numeryczna z wysokimi klawiszami plus/enter i szerokim zerem; Nawigacja i strzałki w swoich własnych pozycjach.
- Jawnie wybierz model/układ. USB VID/PID i nazwa modelu nie potwierdzają wariantu językowego. Profile ogólne i profile wzorowane na dokumentach producenta są wyraźnie inaczej oznakowane; brak skali milimetrowej i gwarancji kompatybilności oprogramowania sprzętowego.
- Mapuj kod skanowy na aktywny układ wejściowy Windows, aby fizyczne kliknięcie klawisza nie spowodowało przypadkowego zajęcia innego klawisza, np. Y/Z podczas przełączania między US i DE.
- Zadanie otwiera wersję roboczą w istniejącym edytorze akcji. Zapisywanie, sprawdzanie konfliktów i odrzucanie pozostają istniejącymi mechanizmami. Kliknięcia nie inicjują działań.
- Zastrzeżone klawisze G, klawisze profilu/blokady i koło iCUE są reprezentowane, ale nie są wyświetlane jako normalne klawisze skrótu Windows bez zaimplementowanego adaptera wejściowego. Konfigurowalne skróty producenta pozostają możliwą drogą.
- Przydziały pozostają globalne. Skróty klawiszowe Windows; widok urządzenia nie oznacza wyłączenia specyficznego dla urządzenia ani wyłącznego przypisania pojedynczej klawiatury fizycznej.

## Dowód

[K100-Handbuch z elementami obsługi](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair Deklaracja ANSI / ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Egzamin

testowanie geometrii, wariantów i kodu wejściowego; Kompiluje i istniejące testy core/Windows; WPF dym do wyświetlania i mapowania bez wykonywania lub zmiany rzeczywistej konfiguracji użytkownika. Badania producenta dokumentują dodatkowe wymagania dotyczące modelu/SKU, niezależnie od aktualnie wdrażanego katalogu układów.

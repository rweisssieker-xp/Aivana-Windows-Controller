<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-12-github-publication.md)

# GitHub wydanie statusu rozwoju MAKRIVO

Stan: 12. Październik 2026. Wydanie kodu źródłowego, a nie podpisana wersja detaliczna.

- Kompilacja wersji rozwiązania: pomyślna, zero ostrzeżeń i błędów.
- Egzaminy podstawowe: pomyślne.
- WPF próba dymna: pomyślna; raport świeżych wyników w lokalnym folderze kompilacji.
- Pierwszy pełny pakiet Windows: okno na pierwszym planie, dźwięk, ekran i karta kontroli iCUE; wówczas upłynie limit czasu odpowiedzi pracownika automatyzacji interfejsu użytkownika.
- Ukierunkowana ponowna próba `--extensions-only`: pomyślna, w tym automatyzacja interfejsu użytkownika, niejednoznaczność, wyłączone cele, wykluczenie hasła, proces wtyczki i prawdziwy mobilny host HTTPS. Pierwotny limit czasu nie został przesłonięty zmianą limitu czasu; przyczyna nie została jeszcze ostatecznie udowodniona.
- Kolejne pełne ponowne uruchomienie Windows: pomyślne, łącznie z sprawdzeniem okna pierwszego planu i wszystkich rozszerzeń. Pomyślna powtórka nie wyklucza sporadycznych przerw w grze.
- Przed publikacją w kontrolowanym inwentarzu źródłowym nie znaleziono typowych kluczy prywatnych ani produktywnych kluczy Stripe/GitHub/AWS. Ta ograniczona kontrola próbki nie jest całkowicie tajną kontrolą.
- Artefakty kompilacji i dane lokalnej aranżacji nie są publikowane.

Zakres funkcjonowania i pozostałe odliczenia: [Featurystyka](../FEATURES.md). Wcześniejsze raporty testowe utrzymują swoją ważność w czasie; nie są gwarancją dla każdego sprzętu lub kontekstu biurowego.

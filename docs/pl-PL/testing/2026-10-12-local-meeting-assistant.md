<!-- generated-doc locale=pl-PL source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Tłumaczenie wspomagane maszynowo. Struktura, przykłady kodu i linki są sprawdzane; nie oznacza to weryfikacji przez rodzimego użytkownika języka ani zatwierdzenia prawnego. Raporty historyczne zachowują pierwotne daty i zakres. [Oryginalne źródło](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokalny asystent spotkań AI – prototyp

Stan: 12. Październik 2026. Hipoteza agenta wynikowego ForgeMind, ograniczony przepływ spotkań. Żadna udowodniona przewaga konkurencyjna i żadna zmierzona oszczędność czasu bez prawdziwych testów porównawczych.

## Użycie

1. Paleta poleceń → **KI Asystent spotkania**. Eksperyment jest domyślnie wyłączony i resetowany podczas importu konfiguracji lub eksportu przenośnego.
2. Włącz asystenta lokalnego. Ollama musi działać osobno na `127.0.0.1:11434`; musi być już zainstalowany odpowiedni model lokalny. **Załaduj modele lokalne** i wybierz model. MAKRIVO nie instaluje modeli/usług, nie kontaktuje się z OpenAI/Cloud API ani nie używa kluczy API. Konfiguracja offline i wiarygodność lokalnej usługi Ollama zależy od użytkownika; zadeklarowane modele zdalne/w chmurze są odrzucane.
3. Wprowadź miejsce docelowe i wyraźnie dodaj programy/dokumenty/strony internetowe. Przesyłany jest cel, identyfikator zasobu, nazwa i typ; Ścieżki docelowe i zawartość wybranych plików nie są dodatkowo przesyłane; sam wpis miejsca docelowego jest wysyłany dosłownie. Etykiety witryn internetowych obejmują tylko schemat/host, pełny link pozostaje lokalny.
4. **KI utwórz wersję roboczą**. Maksymalna liczba sekund 90, tokeny wyjściowe 1024, ograniczony rozmiar odpowiedzi. Zmiany celu/modelu/zasobu odrzuć podgląd; Anuluj/Zamknij kończy żądanie. Model może sugerować tylko wybrane identyfikatory. Nieznane identyfikatory, bezpłatne polecenia, dodatkowe pola JSON i typy dokumentów wykonywalnych są odrzucane.
5. Sprawdź podgląd i punkty otwarte → **Sprawdź w edytorze**. Rezultatem jest początkowo niezapisana wersja robocza. Najpierw zapisz w istniejącym edytorze i wykonaj go jawnie, jeśli to konieczne. Normalna walidacja makr, kontrola przed/po, wstrzymanie i przerwanie pozostają odpowiedzialne.

Obsługiwane: uruchamiaj programy bez argumentów, otwieraj wyraźnie wybrane lokalne pliki PDF/DOCX/XLSX/PPTX/TXT/MD, otwieraj strony internetowe HTTP(S), wycisz mikrofon na wyraźne żądanie. Żadnych domyślnych zmian zestawu słuchawkowego, żadnych wiadomości, spotkań, plików do pobrania, skryptów, bezpłatnych kliknięć i wprowadzania tekstu. Życzenia, których nie da się przedstawić, pojawiają się jako punkty otwarte, jeśli model prawidłowo je rozpozna; Użytkownik musi sprawdzić, czy cele techniczne zostały spełnione. Otwarcie dokumentu nie jest automatycznie weryfikowane technicznie.

## Eksperyment porównawczy

**Pomiar konfiguracji ręcznej · Początek/Koniec** mierzy cały proces ręcznego porównywania. Założenia kreatora mierzą czas od otwarcia kreatora do pierwszego zapisania w edytorze; Natomiast `generated` zawiera jedynie czas planowania. Nie porównuj dwóch nierównych czasów trwania w celu zaoszczędzenia czasu.

Zdarzenia lokalne znajdują się w folderze konfiguracyjnym pod `Experiments/meeting-experiment.jsonl`: znacznik czasu, losowy identyfikator próby/płytki, zdarzenie, czas trwania, korekta tak/nie. Brak tekstów docelowych, nazw zasobów, ścieżek, odpowiedzi modelowych i treści dokumentów. Plik jest obracany z 2 MiB, poprzedni plik zostaje zachowany. Błędy pomiarowe nie mogą przerywać makr i zapisywania konfiguracji.

Przechwytuje: ręczną linię bazową, generowanie/błędy/przerwanie modelu, odrzucanie zmian/zamknięć podglądu, początkowe zapisy i poprawki w edytorze oraz rzeczywiste stany końcowe kanału wlewowego dla zapisanych z nich płytek. `run_completed` oznacza po prostu, że biegacz ukończył; Potwierdzone efekty docelowe są wymienione osobno w procesie testowym. Powtórzenia tego samego makra nie są nowymi, niezależnymi próbami konfiguracji. Początkowy eksperyment można kontynuować po ponownym uruchomieniu, ponieważ identyfikator eksperymentu pozostaje na zapisanym kafelku. Zduplikowane kafelki mają to samo pochodzenie; grupuj według identyfikatora próby podczas oceny.

Pierwszy eksperyment: 20 porównywalne obiekty konferencyjne lub dwa tygodnie z ochotniczymi użytkownikami testowymi; początkowo do użytku wewnętrznego. Porównaj czas ręcznej konfiguracji z czasem pełnej wspomaganej konfiguracji; Dodatkowo oceń współczynnik korekt/odrzuceń, błędy biegacza i rzeczywiste wyniki testów. W przypadku braku poprawy lub przekroczenia granic ochronnych należy dezaktywować przełącznik. W repozytorium nie wykazano żadnego użycia ani chęci zapłaty. Brak automatycznej transmisji telemetrycznej i korekty cen.

## Stanowisko testowe

- Rdzeń: limity, typy, nieznane/zduplikowane identyfikatory, wykluczenie skryptów, długie linki, opcjonalna starsza konfiguracja, zgoda na import/eksport, pomiar oszczędzający dane oraz jednorazowe odrzucenie i prawidłowe zatwierdzenie.
- Transport z fałszywym modelem: schemat Ollama JSON, miejsce docelowe pętli zwrotnej, brak ścieżek plików w wierszu poleceń, nazwa chmury i zdalny alias odrzucone, odrzucona uszkodzona/nadmierna odpowiedź, przerwanie.
- WPF-Smoke: Renderowane okno dialogowe; Przesyłanie wersji roboczej nie powoduje zmiany zapisanych płytek, przeglądanie/zapisywanie pozwala zachować pochodzenie i korektę zapisów, odrzucenie niczego nie zapisuje.
- Niezależna recenzja: odrzucony podgląd oraz znalezione i poprawione długie linki; Testowanie regresyjne CZERWONY → ZIELONY.
- Wydanie kompilacji z zerowymi ostrzeżeniami/błędami, całym pakietem podstawowym i dymem WPF powiodło się. Początkowo istniał kompletny pakiet Windows, obejmujący zmiany ostrości. Kolejne uruchomienie ponownie nie powiodło się, ponieważ odmówiono zmiany pierwszego planu; test jest zależny od kontekstu i pozostaje niezmieniony. Pozostałe testy Windows obejmujące przepustkę transportową modelu z wyraźnym pominięciem testu powodzenia pierwszego planu.
- Kontrola pakietów ujawniła istniejący błąd w atomowej wymianie krótko otwartych plików konfiguracyjnych. Blokada odczytu powielała ten sam błąd odmowy dostępu; zaznaczone jest ograniczone powtarzanie tylko wymiany atomowej CZERWONY → ZIELONY. Trwałe błędy dostępu pozostają widoczne.
- Aktualny samodzielny pakiet `artifacts/release-20261012-184105`: WPF dym powiódł się po korekcie pamięci, włączając okno dialogowe AI i akceptację/odrzucenie wersji roboczej.
- Nie jest to prawdziwy test modelu: lokalna usługa Ollama nie odpowiedziała podczas testowania. Brak oświadczeń na temat jakości modelu, opóźnień, kompletności semantycznej lub oszczędności czasu. Ograniczenia sprzętowe/sterownika/skalowania z poprzedniej wersji pozostają.

Źródła protokołów: [Ollama Rozmowa](https://docs.ollama.com/api/chat), [Structured Outputs](https://docs.ollama.com/capabilities/structured-outputs), [Oficjalne definicje typu, w tym dane o modelu zdalnym](https://github.com/ollama/ollama/blob/main/api/types.go).

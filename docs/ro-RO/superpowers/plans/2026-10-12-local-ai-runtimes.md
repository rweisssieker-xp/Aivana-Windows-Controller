<!-- generated-doc locale=ro-RO source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Timpurile de IA locale și testele reziduale

Obiectiv: să conecteze mai multe orele de funcționare a AI locale, să primească examinări de proiectare și să facă publicări deschise vizibile. utilizatorii autoriza implementarea fără întrebări. modificările existente rămân în continuare.

1. Testele pentru transportul de întoarcere și ONNX de verificare a pachetelor înainte de implementare; adresa de rețea, limitele de cheltuieli, degradarea și resursele necunoscute.
2. Noutăți comune de planificator; Ollama primite, LM Studio127.0.0.1:1234 și llama.cpp127.0.0.1:8080. Nu există URL-uri cloud sau schimbări de furnizor automat. serverele locale pot transmite în sine; funcționarea lor rămâne vizibilă responsabilitatea operatorului.
3. ONNX Runtime GenAI CPU în procesul propriu cu limită de timp, protocolul JSON limitat și procesul de retragere. selectați în mod explicit ordinea de model; nici un model nu este descărcat automat. numai GenAI modele de text compatibile, nici o rețea .onnx.
4. WPF selecție pentru durata de funcționare, model, ONNX dosare; toate furnizează același proiect strict testat.
5. Stațiunea de testare cu limite clare pentru hardware, DPI, panouri mobile și macropaduri exacte. testele fizice și un driver semnat/licențiat nu pot fi înlocuite de software.
6. Creați un nou pachet complet, testele de bază/Windows, WPF, testele de lucru/pacet și recenziile independente; raportează calitatea modelului numai în cazul modelului existent ca fiind testat.

Rezultatul: patru perioade de funcționare implementate, ONNX nativ testat separat, selecția furnizorului și testarea a trecut în smoke WPF ambalate. Core/Windows suite de culoare verde. Revizuirea limitelor unice cu constantă comună și regresie corectată. Adevărata schimbare de hardware/mobile/DPI, eliberarea driverului, traducerile complete și calitatea instructului live rămân lucrări deschise explicit; nu sunt cheltuite complet.

Continuare: Implementate modele separate Jinja și denumite/default; au trecut testele de regresie tip/limită/falback. Navigație de instrumente în 27 cataloguri completate. Model de instrucțiune reală descărcat separat și verificat SHA256: native CPU referință funcționează, dar 0/4 cazuri de planificare îndeplinesc contractul JSON; nu se efectuează acțiuni. Revizuire independentă: zero-Fallback corectat și testat, după care nu mai multe descoperiri concrete. Testele de bază și Windows au trecut, ediția de lansare 0 avertismentele/erocurile.

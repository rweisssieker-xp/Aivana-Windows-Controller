<!-- generated-doc locale=lt-LT source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Vietos žvalgybos laikotarpis ir likusios egzaminos

Tikslas: iš tikrųjų prijungti daugiau vietinių žvalgybos laikotarpių, gauti dizaino patikrinimą ir matyti atvirus leidimus. Vartotojas leidžia įgyvendinti be užklausų. Esami pokyčiai išlieka.

1. Prieš įgyvendinimą atliekami „Loopback“ perdavimo ir ONNX paketo patikrinimai; tinklo adresai, išlaidų apribojimai, nutekėjimai ir nežinomi šaltiniai.
2. Bendros planuojančios naujienos; Ollama Gauti, LM Studio (127.0.0.1:1234) ir llama.cpp (127.0.0.1:8080) Papildyti. nėra debesų URL arba automatiškai keisti tiekėjo. vietiniai serveriai gali perduoti patys; jų veikimas išlieka akivaizdžiai operatorius atsakomybė.
3. ONNX Veikimo laikas GenAI CPU savarankiškai su laiko apribojimu, ribotu JSON protokolu ir proceso nutraukimu. Išsamiai pasirinkti modelio tvarkaraštį; nėra automatiškai atsisiunčiamas modelis. Tik GenAI suderinami teksto modeliai, jokių .onnx tinklų.
4. WPF pasirinkimas, modelis, ONNX plokštės; visi tie patys griežtai patikrinti dizainas.
5. Testas su aiškiomis aparatūros, DPI, mobiliojo plokštumo ir išskirtinio makropado ribomis. Fiziniai testai ir pasirašytas / licencijaujamas vairuotojas čia negali būti pakeisti programine įranga.
6. Release-Build, Core/Windows testai, WPF nuovargis, darbininkų / paketo patikrinimas ir nepriklausomas peržiūra. Sukurkite naują pilną paketą; modelio kokybė nurodyta tik esant egzistuojančiam modeliui kaip išbandyta.

Rezultatas: keturių laikotarpių įgyvendinta, atskirai išbandyta ONNX, pardavėjo pasirinkimas ir bandymo būdas pakuotėje WPF. Pagrindinis / Windows plokštelės žalios spalvos. Apžvalga ištaisyti Unicode su bendra konstanta ir regresijos. Tikroji aparatūros / mobiliųjų / DPI keitimas, vairuotojo išleidimas, visi specialūs vertimai ir gyvas instrukcijos kokybė lieka aiškiai atviras darbas; ne kaip visiškai išleistas.

Toliau: atskirų Jinja ir paskirti / numatyta pavyzdžiai įgyvendinti; tipo / ribos / Fallback regresijos patikrinimai buvo atlikti. Įrankis navigacija 27 katalogai papildyti. Tikras instruktavimo modelis atsisiųsti atskirai ir patikrinti SHA256: gimtoji CPU ataskaita veikia, bet 0/4 planavimo atvejai atitinka JSON sutartį; jokių veiksmų. Nepriklausomas apžvalga: nulinis atsiliepimas ištaisyta ir išbandyta, po to nėra konkrečių tolesnių rezultatų. Pagrindiniai ir Windows KI testai praėjo, išleidimo konstrukcija 0 įspėjimai / klaidos.

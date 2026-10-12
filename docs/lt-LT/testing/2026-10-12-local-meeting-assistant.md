<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-12-local-meeting-assistant.md)

# Vietinis intelektinės žinios patarėjas – prototipas

Standas: 12. Spalio mėn. 2026. „ForgeMind“ hipotezė „Outcome Agent“, ribotas susitikimų srautas. be įrodytų konkurencinių privalumų ir be realių bandymų sutaupyti laiko.

## Naudojimas

1. Įsakymo paletė → **KI konsultacinė pagalba**. Eksperimentas yra standartinis ir atliekamas konfigūracijos metu arba Portalo eksportas atšauktas.
2. Lokalių padėjėjų aktyvavimas. Ollama Reikia atskirai `127.0.0.1:11434` veikia; tinkamas vietinis modelis turi būti įrengtas. **Vietiniai modeliai** Pasirinkite modelį. MAKRIVO Nenaudoti modelių / paslaugų, nesusiję su OpenAI / debesijos API ir nenaudoja API raktų. Ollama-Paslaugos yra naudotojui; deklaruotos nuotolinės / debesijos modeliai yra atmesti.
3. Įveskite tikslą ir aiškiai pridėkite programas / dokumentus / svetaines. Siunčiamas tikslas, išteklių ID, pavadinimas ir tipas; tikslinis kelias ir pasirinktų failų turinys nėra papildomai siunčiami; tikslinis įrašas pats yra žodžiu siunčiamas. Svetainės pavadinimai apima tik schemą/hostą, visa nuoroda lieka vietine.
4. **Sukurkite AI projektą**. Maksimalus 90 Sekundų, 1024 Išleidimo dėžutės, ribotas atsakymo dydis. tikslinės / modelio / išteklių pakeitimai atmeta išankstinį rodiklį; nutraukimas / uždarymo užklausos. modelis gali pasiūlyti tik atrinktas ID. Nežinomi ID, nemokami įsakymai, papildomi JSON laukai ir vykdomų dokumentų tipai atmetami.
5. Iš anksto ir peržiūrėti atvirus taškus → **Į redaktorių peržiūrėti**. Iš pradžių rezultatas yra neatsargus projektas. Išsaugokite tik esamą redaktorių ir, jei reikia, aiškiai atlikite. Normalus makrovalidavimas, peržiūra, pertrauka ir pertrauka lieka atsakingi.

Pagalba: Programos paleisti be argumentų, atidaryti aiškiai pasirinktą vietinį PDF/DOCX/XLSX/PPTX/MD, atidaryti HTTP(S) svetaines, išreikšti norą uždaryti mikrofoną. Nėra standartinių klaviatūrų keitimo, jokių pranešimų, pasimatymų, atsisiuntimo, skripto, nemokamų spustelėjimų ar teksto įrašų. Neįsivaizduojami pageidavimai pasirodo kaip atviros taškos, jei modelis juos teisingai atpažįsta; profesionalus tikslas turi būti patikrintas vartotojui. Dokumentų atvėrimas nėra automatiškai patikrintas.

## Palyginimo eksperimentas

**Manualinis nustatymas · Pradžia / pabaiga** matuoja visą rankinį lyginimą. Pagalbiniai priėmimai matuoja laiką nuo asistento atidarymo iki pirmojo saugojimo redaktoriuje; `generated`, priešingai, apima tik planavimo laiką. Nereikia palyginti dviejų skirtingų laikotarpių kaip laiko taupymo.

Vietiniai įvykiai konfigūracijos tvarkaraštyje yra `Experiments/meeting-experiment.jsonl`: laikrodis, atsitiktinis bandymas/kachelis ID, įvykis, trukmė, korekcija taip/ne. Nėra tikslinių tekstų, išteklių pavadinimų, maršrutų, modelių atsakymų ar dokumentų turinio. Failas sukasi nuo 2 MiB, ankstesnis failas išlieka. Matavimo klaidos negali būti nutraukiamos makro ir konfigūracijos saugojimo.

Įtraukta: rankinė bazinė linija, modelio generavimas / klaidos / nutraukimas, atkaklūs išankstiniai pakeitimai / uždarymas, pirmą kartą saugomi ir pataisos redaktoriuje, taip pat faktinės „Runner“ pabaigos iš jo saugomų dėmių. `run_completed` tik tai reiškia, kad „Runer“ yra užbaigtas; patvirtinti tiksliniai rezultatai yra atskiri bandymo metu. To paties makro pasikartojimai nėra nauji nepriklausomi eksperimentai. Pradinis eksperimentas gali būti tęsiamas po naujo paleidimo, nes bandymo ID lieka saugomame kaktus. Dupliuotos šaknys dalijasi šia kilmė; vertinant pagal bandymą grupuoti ID.

Pirmasis eksperimentas: 20 panašios susitikimo įstaigos arba dvi savaites su savanoriais; pirmasis vidaus naudojimas. Palyginkite rankinius įrengimo laiką su visais padėjusiais įrengimo laikais; papildomai įvertinkite korekcijos / iškraipymo koeficientą, bėgimo klaidas ir realius bandymų rezultatus. Jei nėra tobulinimo ar pažeidimo apsauginių ribų išjungti. Nėra naudojimo ar mokėjimo pasirengimo registruoti. Nėra automatinio telemetrijos perdavimo ar kainų koregavimo.

## Bandymų stotis

- Pagrindiniai: apribojimai, tipai, nežinomi / padvigubinti ID, skriptų išjungimas, ilgos nuorodos, alternatyvi „Alt“ konfigūracija, importo / eksporto atjungimas, duomenų taupymas ir vienkartinis atmetimas ir teisingas perdavimas.
- Transporto su klaidinga modelis: Ollama JSON schema, Loopback tikslas, nėra failų pjovimo, debesų pavadinimas ir nuotolinis atsakymas, pažeistas / pernelyg ilgas atsakymas, nutraukimas.
- WPF-Smoke: dialogas sumažintas; dizaino perdavimas nesikeičia saugomų dėmių, apžvalga / saugojimas gauna kilmę ir užfiksuoja korekciją, atsisakyti nieko.
- Nepriklausomas apžvalga: atsisakyti ir ilgus ryšius rasti ir ištaisyti; Regresijos bandymai RED→GREEN.
- Išleidimas su nulinėmis įspėjimais / klaidomis, visa pagrindinė plokštė ir sėkmingas WPF smoke. Iš pradžių visa Windows suita buvo įskaitant dėmesio keitimąsi. Dar vienas bėgimas vėl nesėkmingas atsisakytame etape; bandymas priklauso nuo konteksto ir lieka nekeičiamas. Kiti Windows bandymai, įskaitant modelio transportą, yra aiškiai atmesti išankstinio rezultato testai.
- Pakuotės patikrinimas atskleidė esamą klaidą, kai atvirai atidarytos konfigūracijos failai keičiami atominiu būdu. Skaitymo blokadas atkūrė tą patį prieigos-neigta klaida; ribotas pasikartojimas tik atominio mainų yra RED→GREEN patikrinti. Nuolatinės prieigos klaidos lieka matomos.
- Dabartinis savarankiškas paketas `artifacts/release-20261012-184105`: WPF smoke po atminties korekcijos sėkmingai, įskaitant AI dialogą ir projekto priėmimą/atmetimą.
- Nėra tikro modelio: vietinė Ollama paslauga atsakė į egzaminą. Nėra pareiškimų apie modelio kokybę, latentumą, semantinį vientisumą ar laiko taupymą. Įrangos / variklio / skalavimo ribos išlieka.

Protokolų šaltiniai: [Ollama Pokalbiai](https://docs.ollama.com/api/chat), [Struktūriniai ištekliai](https://docs.ollama.com/capabilities/structured-outputs), [oficialios tipo apibrėžtys, įskaitant nuotolinio modelio duomenis](https://github.com/ollama/ollama/blob/main/api/types.go).

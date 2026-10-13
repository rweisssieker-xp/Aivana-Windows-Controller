<!-- generated-doc locale=lt-LT source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../MAINTENANCE.md)

# Visos dokumentų kalbos

Dokumentacija apima visas 27 sąsajos kalbas. Sugeneruotų aplankų pavadinimuose naudojami regioniniai kodai; norvegų kalbai atitinka `nb-NO`, serbų kalbai lotyniškais rašmenimis — `sr-Latn`. Kiekviename aplanke yra rodyklė, vadovas, išsamus veiksmų žinynas, funkcijų sąrašas, tikrinimo pastabos, tyrimai su CSV, istoriniai planai ir licencijavimo analizė.

## Tiesos šaltinis

Edit šaknis `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Šiame puslapyje arba originaliuose failuose `docs/research`, `docs/testing`, `docs/superpowers` ir `docs/licensing`. Neatsirašykite istorinio pranešimo, kad jis patvirtintų vėlesnę versiją. išlaikyti įgyvendinimą, simuliavimą, realią prietaisą ir komercinį patvirtinimą atskirai.

## Gaminti ir patvirtinti

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Pasirinktas internetinis generatorius siunčia dokumentų prozę į "Google" viešąjį vertimo tašką, laikydamasis esamo vietos įrankio. Jis neskaito naudotojo konfigūracijos, paslapčių ar modelio failų. Išleiskite tik tokią paslaugą tinkamą dokumentą. Kodų blokai, įrašo kodas, URL, lentelės struktūra ir techniniai identifikatoriai yra apsaugoti. Vertimo slapukai ir užšaldytas šaltinis yra saugomi pagal ignoruotas `artifacts/documentation-translation`.

Kai internetinis paslaugų teikėjas yra ribotas, `tools/translate_documentation_local.py` užpildo tą patį atsargą, naudodamas aiškiai pateiktą vietinį M2M100 CTranslate2 modelį. 2026-10-12 nuotrauka naudoja MIT licenciją facebook/m2m100_418M modelį, konvertuojamą kaip JustFrederik/m2m_100_418m_ct2_int8 peržiūrint `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 ir `SentencePiece` 0.2.2 yra vien statybos įrankiai. Nėra modelio svorio ar vertimo priklausomybės siunčiamos su MAKRIVO. Įveskite generatorių su `--cache-only` užpildydami slaptažodį be tinklo prašymų. Vietinis pagalbininkas neleidžia atsisiųsti ar paleisti nuotolinio modelio kodo.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Be `--refresh-sources`, bėgimas atnaujina savo užšaldytą nuotrauką. Naudokite atnaujinimo variantą po originalo redagavimo. Paslėpti segmentai vėl naudojami. Iš dalies paleidimas gali pasirinkti vietinius kodus naudojant `--locales`; užbaigti visus vietinius prieš publikavimą. Sustabdytos rungtynės tyliai nepakeičia nepaaiškinto prozo nesėkmingu vertimu.

Kiekvienas šaltinis ir sukurtas failas su SHA-256 Pirštų atspaudai normalizuojami UTF-8 tekstas: LF linijos galai ir be byto užsakymo ženklo. „offline“ patikrinėjas tikrina visą kiekvienos kalbos apibūdinimą, pirštų atspaudus, vietinius nuorodas, kodo pavyzdžius, lentelę / CSV struktūrą, šaltinio URL ir nepakeistus vietos turėtojus. Jis atmeta stalinį šaltinį..

## Publikavimas ir paketas

Išleiskite dokumentą, generatorių ir patikrinimą kartu. Išsaugokite originalias trečiosios šalies licencijos sąlygas; vertimo licencijos analizė yra informatyvi. Statybos artefaktai, vertimo cache ir vietos orkestravimo failai nėra Git. Šaltinis-dokumentacijos leidimas pats nepaaiškina Windows įrenginio.

Naudotojo atžvilgiu programos vertimai yra atskiri JSON katalogai pagal `src/Aivana.Core/Localization`. Dokumentų karta nesikeičia šių katalogų. Programos kūrimas ir jo dokumentacija turėtų dalytis versija; naujai aprašyta eksperimentinė parama gali būti prieš įdiegtą paketą.

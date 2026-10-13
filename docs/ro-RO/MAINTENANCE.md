<!-- generated-doc locale=ro-RO source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../MAINTENANCE.md)

# Întreținerea tuturor limbilor documentației

Inventarul documentației acoperă toate cele 27 de limbi ale interfeței. Folderele generate folosesc coduri regionale; norvegiana corespunde `nb-NO`, iar sârba cu alfabet latin `sr-Latn`. Fiecare folder conține index, manual, referință detaliată a acțiunilor, inventar de funcții, note de verificare, cercetare inclusiv CSV, planuri istorice și analiză de licențiere.

## Sursă de adevăr

Editați rădăcina `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Această pagină sau fișierele originale `docs/research`, `docs/testing`, `docs/superpowers` şi `docs/licensing`. Nu scrie un raport istoric pentru a implica validarea unei versiuni ulterioare. menține implementarea, simularea, acceptarea de dispozitive reale și aprobarea comercială distinctă.

## Generarea și validarea

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Generatorul online opțional trimite textul documentației către punctul public de traducere Google, urmând instrumentele de localizare existente. Nu citește configurația utilizatorului, secretele sau fișierele modelelor. Rulați-l numai pe documentație adecvată acelui serviciu. Blocurile de cod, codul inline, URL-urile, structura tabelelor și identificatorii tehnici sunt protejate. Cache-ul traducerilor și un instantaneu înghețat al surselor sunt stocate în `artifacts/documentation-translation`, ignorat de Git.

Când furnizorul online limitează rata cererilor, `tools/translate_documentation_local.py` completează același cache cu un model local M2M100 CTranslate2 furnizat explicit. Instantaneul 2026-10-12 folosește modelul facebook/m2m100_418M cu licență MIT, convertit ca JustFrederik/m2m_100_418m_ct2_int8 la revizia `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 și `SentencePiece` 0.2.2 sunt instrumente numai pentru construire. Nu sunt distribuite ponderi de model sau dependențe de traducere cu MAKRIVO. Rulați generatorul cu `--cache-only` după completarea cache-ului pentru generare fără cereri de rețea. Instrumentul auxiliar local nu descarcă și nu execută cod de model la distanță.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Fără `--refresh-sources`, execuția continuă de la instantaneul surselor înghețat anterior. Folosiți opțiunea de actualizare după editarea originalelor. Segmentele din cache sunt reutilizate. O execuție parțială poate selecta codurile de limbă cu `--locales`; finalizați toate limbile înainte de publicare. Execuțiile întrerupte nu înlocuiesc pe ascuns o traducere eșuată cu text netradus.

Manifestul înregistrează fiecare fișier sursă și generat cu amprente SHA-256 ale textului UTF-8 normalizat: sfârșituri de linie LF și fără marcaj de ordine a octeților. Verificatorul offline verifică acoperirea completă pentru fiecare limbă, amprentele, linkurile locale, exemplele de cod, structura tabelelor/CSV, URL-urile sursă și substituenții neînlocuiți. Respinge sursele neactualizate. Trecerea verificării structurale nu este o revizuire de către un vorbitor nativ sau o aprobare juridică. Paginile generate afișează vizibil această precizare.

## Publicații și pachete

Publicați documentul, generatorul și checkerul împreună. Păstrați termenii de licență de la terți; analiza licenței traduse este informativă. Articole de construcție, cache de traducere și fișiere de orchestrare locale nu aparțin Git. O ediție de documentare de sursă nu publică în sine un instalator Windows.

Traducerile interfeței aplicației sunt cataloage JSON separate în `src/Aivana.Core/Localization`. Generarea documentației nu modifică aceste cataloage. Versiunea compilată și documentația ar trebui să aibă același instantaneu versionat; suportul experimental nou descris pentru mediul de execuție poate preceda pachetul instalat.

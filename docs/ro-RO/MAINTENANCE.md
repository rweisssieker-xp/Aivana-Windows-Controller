<!-- generated-doc locale=ro-RO source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../MAINTENANCE.md)

# Mâncarea tuturor limbajelor de documentare

Inventarea documentelor urmează toate 27 limbi de utilizare a UI. Folderile generate folosesc coduri regionale; hărți norvegiană la `nb-NO` și latină la `sr-Latn`. Fiecare folder conține un index, manual, referință detaliată de acțiune, inventari de caracteristici, note de verificare, cercetare inclusiv CSV, planuri istorice și analize de licență.

## Sursă de adevăr

Editați rădăcina `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Această pagină sau fișierele originale `docs/research`, `docs/testing`, `docs/superpowers` şi `docs/licensing`. Nu scrie un raport istoric pentru a implica validarea unei versiuni ulterioare. menține implementarea, simularea, acceptarea de dispozitive reale și aprobarea comercială distinctă.

## Generarea și validarea

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Generatorul online opțional trimite proza de documentare la punctul final public de traducere al Google, în funcție de instrumentul de localizare existent. Nu citește configurația utilizatorului, secretele sau fișierele de modelare. Folosește doar documentele potrivite pentru acest serviciu. Blocurile de cod, codul inline, URL-urile, structura tabelului și identificatorii tehnici sunt protejați. Cache-urile de traducere și o imagine de surse înghețate sunt stocate sub ignorate `artifacts/documentation-translation`.

Atunci când furnizorul on-line este rată limitată, `tools/translate_documentation_local.py` umple același cache folosind un model local explicit M2M100 CTranslate2. 2026-10-12 utilizează modelul licențiat de MIT facebook/m2m100_418M, convertit ca JustFrederik/m2m_100_418m_ct2_int8 la revizuire `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 și `SentencePiece` 0.2.2 sunt doar instrumente de construcție. Nu sunt trimise greutăți de model sau dependențe de traducere cu MAKRIVO. Executați generatorul cu `--cache-only` după umplerea cache-ului pentru a furniza fără cereri de rețea. Ajutorul local nu descarcă sau execută codul de model la distanță.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Fără `--refresh-sources`, un curs își reînnoiește fotografiile înghețate. Utilizați opțiunea de refresh după editarea originalelor. Se reutilizează segmentele ascunse. Un curs parțial poate selecta coduri locale folosind `--locales`; terminarea tuturor locurilor înainte de publicare. Runsurile întrerupte nu înlocuiesc în tăcere proza neinterpretată pentru o traducere eșuată.

Manifestul înregistrează fiecare sursă și fișier generat cu SHA-256 degetele din textul normalizat UTF-8: LF liniile de terminare și fără semnul de comanda. Checkerul offline verifică acoperirea completă pe limbă, amprentele, link-urile locale, exemplele de cod, structura tabelului/CSV, URL-urile de sursă și locațiile neînlocuite. El respinge sursele de fier. O verificare structurală care trece nu este o revizuire a vorbitorului nativ sau aprobarea legală. Paginile generate poartă această calificare vizibil.

## Publicații și pachete

Publicați documentul, generatorul și checkerul împreună. Păstrați termenii de licență de la terți; analiza licenței traduse este informativă. Articole de construcție, cache de traducere și fișiere de orchestrare locale nu aparțin Git. O ediție de documentare de sursă nu publică în sine un instalator Windows.

Traducerile de aplicații sunt cataloguri JSON separate sub `src/Aivana.Core/Localization`. Generația de documentare nu modifică aceste cataloguri. O aplicație construită și documentația sa ar trebui să împărtășească o imagine de scurtă durată; suportul experimental recent descrise poate precede un pachet instalat.

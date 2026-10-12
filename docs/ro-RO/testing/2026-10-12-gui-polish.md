<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-12-gui-polish.md)

# GUI de prelucrare 12.10.2026

- Intrarea directă „instrumente” în navigație; trei grupuri cu acțiuni declarate pentru a instala, crea și verifica.
- Patruzeci de zone de dialog existente utilizează o clasă de dialog comună WPF cu resursele ferestrei principale, unitatea de scris și poziționarea la proprietar.
- Dimensiunea dialogului este limitată la domeniul de lucru Windows; conținutul lung va primi linii de scroll. setările și paginile de profil rămân scrollable chiar și la înălțime mică.
- Filtrul de acțiune apare numai pe panou. selecția de profil și căutarea au nume explicite pentru tehnologii de asistență. descrierile de câmp sunt mai contraste.
- Testul de dialog-smoke include o imagine de ansamblu a instrumentelor și dialoguri de extindere în 560 × 440 și dimensiuni normale; testele de înregistrare și de limbă existente au fost adaptate la structura de scanare.

Limite: Fereastra compactă nu este un substitut pentru testele reale Windows cu 150/200 procente. noile texte germane nu sunt încă complet traduse pentru toate 27 limbi.

Stația de testare: Crearea de lansare fără avertize/eroc; a trecut printr-un test complet de smoke WPF, inclusiv dialoguri compacte cu instrumente și extinderi, precum și testele de schimbare a limbii.

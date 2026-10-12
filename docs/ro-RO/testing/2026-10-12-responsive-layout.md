<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-12-responsive-layout.md)

# Responsive WPF suprafață

Paginile principale se concentrează pe lățimea de conținut util, nu pe o lățime fixă a ferestrei. Dintre 900 de conținut DIP se află un dashboard și un editor unul pe altul. Sub 620 DIPs sunt numere duble. În 820 lățimea ferestrei DIP este încorporată linia laterală. Dimensiunea minimă: 640 × 480 DIPs.

Profilurile și setările, precum și elementele de control ale tastaturii își schimbă coloanele în funcție de lățimea reală a gridului. Aceste elemente de control sunt redistribuite în loc să le reîncărcați. Cardurile de acțiune împărtășesc gama de linii disponibile. Liniile de instrumente se prăbușesc. Pagina de tastatură este accesibilă verticală, tastatura fizică își păstrează proporțiile.

Testare automată WPF: toate cele cinci pagini principale la 640 × 480, 900 × 620 și 1440 × 900; capturi de ecran pentru fiecare pagină; schimbarea coloanelor în ambele direcții cu testarea textului primit. DIP-urile sunt unități logice WPF Scalarea reală Windows la 150/200 procente și mai mulți monitori nu sunt încă validate.

Testarea: Creați fără erori/avizuri. testarea completă a smoke-ului WPF cu toate dimensiunile, schimbările de coloane, layout-ul tastaturilor și testările de limbă. git diff --check a trecut.
